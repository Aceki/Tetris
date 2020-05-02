using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Tetris
{
    public class Game
    {
        public int Scores { get; set; }
        public bool GameIsOver { get; private set; }
        public event GameOverEventHandler GameOver;

        private Size gameFieldSize;
        private Block[,] gameField;
        private Figure fallingFigure;

        public Game(Size gameFieldSize)
        {
            this.gameFieldSize = gameFieldSize;
        }

        public void Start()
        {
            GameIsOver = false;
            Scores = 0;
            gameField = new Block[gameFieldSize.Width, gameFieldSize.Height];
            fallingFigure = Tetromino.CreateRandomFigure(new Point(2 * Block.Size, -Block.Size));
        }

        public bool InBounds(Point point)
        {
            return point.X >= 0 && point.X < gameFieldSize.Width && point.Y < gameFieldSize.Height;
        }

        public bool CanMoveFigureTo(Direction direction, Figure figure)
        {
            var offset = Point.Empty;
            switch (direction)
            {
                case Direction.Left:
                    offset = new Point(-Block.Size, 0);
                    break;
                case Direction.Down:
                    offset = new Point(0, Block.Size);
                    break;
                case Direction.Right:
                    offset = new Point(Block.Size, 0);
                    break;
                case Direction.Up:
                    offset = new Point(0, Block.Size);
                    break;
            }
            foreach (var block in figure.Blocks)
            {
                var fieldPosition = GetFieldPoint(new Point(block.Position.X + offset.X, block.Position.Y + offset.Y));
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
                var fieldPoint = GetFieldPoint(new Point(x, y));
                if (!InBounds(fieldPoint) || fieldPoint.Y < 0 || gameField[fieldPoint.X, fieldPoint.Y] != null)
                    return false;
            }
            return true;
        }

        public void Update()
        {
            if (GameIsOver) 
                return;
            if (CanMoveFigureTo(Direction.Down, fallingFigure))
                fallingFigure.MoveTo(Direction.Down);    
            else
            {
                AddToGameField(fallingFigure);
                fallingFigure = Tetromino.CreateRandomFigure(new Point(Block.Size * 3, -Block.Size));
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
            for (var y = gameFieldSize.Height - 1; y >= 0; y--)
            {
                var x = 0;
                for (; x < gameFieldSize.Width; x++)
                    if (gameField[x, y] == null)
                        break;
                if (x == gameFieldSize.Width)
                    yield return y;
            }
        }

        public void RemoveFloor(int floorNumber)
        {
            Scores++;
            for (var x = 0; x < gameFieldSize.Width; x++)
                gameField[x, floorNumber] = null;
        }

        public void LowerBlocks(int floorNumber)
        {
            for (var y = floorNumber; y >= 0; y--)
                for (var x = 0; x < gameFieldSize.Width; x++)
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
                var fieldPoint = GetFieldPoint(block.Position);
                if(fieldPoint.Y < 0)
                {
                    OnGameEnd();
                    break;
                }
                block.Parent = null;
                gameField[fieldPoint.X, fieldPoint.Y] = block;
            }
        }

        public Point GetFieldPoint(Point point)
            => new Point(point.X / Block.Size, point.Y / Block.Size);

        public void Draw(Graphics graphics)
        {
            fallingFigure.Draw(graphics);
            foreach (var block in gameField)
                if (block != null)
                    block.Draw(graphics);
        }

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
                case Keys.Space:
                    while (CanMoveFigureTo(Direction.Down, fallingFigure))
                        fallingFigure.MoveTo(Direction.Down);
                    break;
                default:
                    break;
            }
        }
        #endregion

        private void OnGameEnd()
        {
            GameIsOver = true;
            if(GameOver != null)
                GameOver.Invoke(this, new GameOverEventArgs("You lose!"));
        }
    }
}
