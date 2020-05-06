using System;
using System.Collections.Generic;
using System.Windows;
using System.Drawing;
using System.Windows.Forms;
using System.Numerics;

namespace Tetris
{
    public class GameModel
    {
        public readonly Size GameFieldSize;
        public FigureType? HoldedFallingFigureType { get; private set; }
        public FigureType NextFallingFigureType { get; private set; }
        public int LinesScore { get; set; }
        public bool GameOnPause { get; set; }
        public bool GameIsOver { get; private set; }
        public event GameOverEventHandler GameOver;
        public event EventHandler Exit;
        
        private Block[,] gameField;
        
        private Figure fallingFigure;

        public GameModel(Size gameFieldSize)
        {
            this.GameFieldSize = gameFieldSize;
        }

        public void Start()
        {
            GameIsOver = false;
            LinesScore = 0;
            HoldedFallingFigureType = null;
            gameField = new Block[GameFieldSize.Width, GameFieldSize.Height];
            fallingFigure = Tetromino.CreateRandomFigure(new Vector(2 * Block.Size, -Block.Size));
            NextFallingFigureType = Tetromino.GetRandomType();
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
                var x = (int)(figure.Blocks[i].Offset.X * Math.Cos(Figure.RotateAngle) - figure.Blocks[i].Offset.Y * Math.Sin(Figure.RotateAngle)) + figure.Blocks[0].Position.X;
                var y = (int)(figure.Blocks[i].Offset.X * Math.Sin(Figure.RotateAngle) + figure.Blocks[i].Offset.Y * Math.Cos(Figure.RotateAngle)) + figure.Blocks[0].Position.Y;
                var fieldPoint = GetOnFieldPoint(new Vector(x, y));
                if (!InBounds(fieldPoint) || fieldPoint.Y < 0 || gameField[(int)fieldPoint.X, (int)fieldPoint.Y] != null)
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
                fallingFigure = Tetromino.CreateFigure(NextFallingFigureType, new Vector(Block.Size * 3, -Block.Size));
                NextFallingFigureType = Tetromino.GetRandomType();
                return;
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
            LinesScore++;
            for (var x = 0; x < GameFieldSize.Width; x++)
                gameField[x, floorNumber] = null;
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
                    OnGameEnd();
                    break;
                }
                block.Parent = null;
                gameField[(int)fieldPoint.X, (int)fieldPoint.Y] = block;
            }
        }

        public Vector GetOnFieldPoint(Vector point)
            => point / Block.Size;

        #region Control
        public void KeyDown(object sender, KeyEventArgs args)
        {
            switch (args.KeyCode)
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
                    if (HoldedFallingFigureType != null)
                    {
                        var temp = fallingFigure.Type;
                        fallingFigure = Tetromino.CreateFigure(HoldedFallingFigureType.Value, new Vector(Block.Size * 3, -Block.Size));
                        HoldedFallingFigureType = temp;
                    }
                    else
                    {
                        HoldedFallingFigureType = fallingFigure.Type;
                        fallingFigure = Tetromino.CreateFigure(NextFallingFigureType, new Vector(Block.Size * 3, -Block.Size));
                        NextFallingFigureType = Tetromino.GetRandomType();
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
        #endregion

        public IEnumerable<Block> GetBlocksFromField()
        {
            foreach (var block in fallingFigure.Blocks)
                yield return block;
            foreach (var block in gameField)
                if (block != null)
                    yield return block;
        }

        private void OnGameEnd()
        {
            GameIsOver = true;
            if(GameOver != null)
                GameOver.Invoke(this, new GameOverEventArgs("You lose!"));
        }

        private void OnExit()
        {
            GameOnPause = true;
            if (Exit != null)
                Exit.Invoke(this, new EventArgs());
            GameOnPause = false;
        }
    }
}
