using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Tetris
{
    public class GameModel
    {
        public readonly Vector FallingFigureSpawnPosition = new Vector(Block.Size * 3, -Block.Size);
        public readonly Size GameFieldSize;
        public FigureType? HoldedFallingFigureType { get; private set; }
        public FigureType NextFallingFigureType { get; private set; }
        public int Score { get; private set; }
        public int LinesScore { get; private set; }
        public bool GameOnPause { get; set; }
        public bool GameIsOver { get; private set; }
        public event GameOverEventHandler GameOver;
        public event FloorRemovedEventHandler FloorRemoved;
        public event EventHandler Start;
        public event EventHandler Exit;
        public event EventHandler FigureLanded;

        private Block[,] gameField;
        private Figure fallingFigure;

        private bool holdButtonAlredyUse;

        public GameModel(Size gameFieldSize)
        {
            this.GameFieldSize = gameFieldSize;
        }

        public void StartGame()
        {
            holdButtonAlredyUse = false;
            GameIsOver = false;
            Score = 0;
            LinesScore = 0;
            HoldedFallingFigureType = null;
            gameField = new Block[GameFieldSize.Width, GameFieldSize.Height];
            fallingFigure = Tetromino.CreateRandomFigure(FallingFigureSpawnPosition);
            NextFallingFigureType = Tetromino.GetRandomType();
            OnGameStart();
        }

        public bool InBounds(Vector point)
        {
            return point.X >= 0 && point.X < GameFieldSize.Width && point.Y < GameFieldSize.Height;
        }

        public bool CanMoveFigureTo(Direction direction, Figure figure)
        {
            var offset = Vector.Zero;
            switch (direction)
            {
                case Direction.Left:
                    offset = new Vector(-Block.Size, 0);
                    break;
                case Direction.Down:
                    offset = new Vector(0, Block.Size);
                    break;
                case Direction.Right:
                    offset = new Vector(Block.Size, 0);
                    break;
                case Direction.Up:
                    offset = new Vector(0, Block.Size);
                    break;
            }
            foreach (var block in figure.Blocks)
            {
                var fieldPosition = GetOnFieldPoint(block.Position + offset);
                if (!InBounds(fieldPosition))
                    return false;
                if (fieldPosition.Y >= 0 && gameField[fieldPosition.X, fieldPosition.Y] != null)
                    return false;
            }
            return true;
        }

        public bool CanRotateFigure(Figure figure)
        {
            for (var i = 1; i < 4; i++)
            {
                var offset = figure.Blocks[i].Position - figure.Blocks[0].Position;
                offset.Rotate(Figure.RotateAngle);
                offset += figure.Blocks[0].Position;
                var fieldPoint = GetOnFieldPoint(offset);
                if (!InBounds(fieldPoint) || fieldPoint.Y < 0 || gameField[fieldPoint.X, fieldPoint.Y] != null)
                    return false;
            }
            return true;
        }

        public void Update()
        {
            if (GameIsOver || GameOnPause)
                return;
            if (CanMoveFigureTo(Direction.Down, fallingFigure))
                fallingFigure.MoveTo(Direction.Down);
            else
            {
                AddToGameField(fallingFigure);
                OnFigureLanding();
            }
            RemoveCompletedFloors();
        }

        public void RemoveCompletedFloors()
        {
            foreach (var floorNumber in GetFloorsToRemove())
            {
                RemoveFloor(floorNumber);
                LowerBlocks(floorNumber);
            }
        }

        public IEnumerable<int> GetFloorsToRemove()
        {
            for (var y = GameFieldSize.Height - 1; y >= 0; y--)
            {
                var x = 0;
                for (; x < GameFieldSize.Width; x++)
                    if (gameField[x, y] == null)
                        break;
                if (x == GameFieldSize.Width)
                    yield return y;
            }
        }

        public void RemoveFloor(int floorNumber)
        {
            for (var x = 0; x < GameFieldSize.Width; x++)
                gameField[x, floorNumber] = null;
            OnFloorRemoved(floorNumber);
        }

        public void LowerBlocks(int floorNumber)
        {
            for (var y = floorNumber; y >= 0; y--)
                for (var x = 0; x < GameFieldSize.Width; x++)
                    if (gameField[x, y] != null)
                    {
                        gameField[x, y].MoveTo(Direction.Down);
                        gameField[x, y + 1] = gameField[x, y];
                        gameField[x, y] = null;
                    }
        }

        public void AddToGameField(Figure figure)
        {
            foreach (var block in figure.Blocks)
            {
                var fieldPoint = GetOnFieldPoint(block.Position);
                if (fieldPoint.Y < 0)
                {
                    OnGameOver();
                    break;
                }
                block.Parent = null;
                gameField[fieldPoint.X, fieldPoint.Y] = block;
            }
        }

        public Vector GetOnFieldPoint(Vector point)
            => point / Block.Size;

        public void KeyDown(Keys key)
        {
            switch (key)
            {
                case Keys.D:
                    if (CanMoveFigureTo(Direction.Right, fallingFigure))
                        fallingFigure.MoveTo(Direction.Right);
                    break;
                case Keys.A:
                    if (CanMoveFigureTo(Direction.Left, fallingFigure))
                        fallingFigure.MoveTo(Direction.Left);
                    break;
                case Keys.W:
                    if (CanRotateFigure(fallingFigure))
                        fallingFigure.Rotate();
                    break;
                case Keys.S:
                    if (CanMoveFigureTo(Direction.Down, fallingFigure))
                        fallingFigure.MoveTo(Direction.Down);
                    break;
                case Keys.Tab:
                    if (!holdButtonAlredyUse)
                    {
                        if (HoldedFallingFigureType != null)
                        {
                            var temp = fallingFigure.Type;
                            fallingFigure = Tetromino.CreateFigure(HoldedFallingFigureType.Value, FallingFigureSpawnPosition);
                            HoldedFallingFigureType = temp;
                        }
                        else
                        {
                            HoldedFallingFigureType = fallingFigure.Type;
                            fallingFigure = Tetromino.CreateFigure(NextFallingFigureType, FallingFigureSpawnPosition);
                        }
                        NextFallingFigureType = Tetromino.GetRandomType();
                        holdButtonAlredyUse = true;
                    }
                    break;
                case Keys.Space:
                    while (CanMoveFigureTo(Direction.Down, fallingFigure))
                        fallingFigure.MoveTo(Direction.Down);
                    break;
                case Keys.Escape:
                    OnExit();
                    break;
                default:
                    break;
            }
        }

        public IEnumerable<Block> GetBlocksFromField()
        {
            foreach (var block in fallingFigure.Blocks)
                yield return block;
            foreach (var block in gameField)
                if (block != null)
                    yield return block;
        }

        private void IncreaseLinesScore(int number)
        {
            LinesScore += number;
        }

        private void IncreaseScore(int number)
        {
            Score += number;
        }

        private void OnGameOver()
        {
            GameIsOver = true;
            if (GameOver != null)
                GameOver.Invoke(this, new GameOverEventArgs() { Message = "You lose!", Score = Score, LinesScore = LinesScore});
        }
        
        private void OnGameStart()
        {
            if (Start != null)
                Start.Invoke(this, new EventArgs());
        }

        private void OnExit()
        {
            GameOnPause = true;
            if (Exit != null)
                Exit.Invoke(this, new EventArgs());
            GameOnPause = false;
        }

        private void OnFigureLanding()
        {
            IncreaseScore(10);
            fallingFigure = Tetromino.CreateFigure(NextFallingFigureType, FallingFigureSpawnPosition);
            NextFallingFigureType = Tetromino.GetRandomType();
            holdButtonAlredyUse = false;
            if (FigureLanded != null)
                FigureLanded.Invoke(this, new EventArgs());
        }

        private void OnFloorRemoved(int floorNumber)
        {
            IncreaseLinesScore(1);
            IncreaseScore(100);
            if (FloorRemoved != null)
                FloorRemoved.Invoke(this, new FloorRemovedEventArgs() { FloorNumber = floorNumber });
        }
    }
}
