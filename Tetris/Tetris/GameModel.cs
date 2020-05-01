using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using System.Windows.Forms;

namespace Tetris
{
    class GameModel
    {
        public int Scores { get; set; }
        public Figure FallingFigure;

        private Size playFieldSize;
        private Cube[,] gameField;

        public GameModel(Size playFieldSize)
        {
            if (playFieldSize.Width % Cube.Size != 0 || playFieldSize.Height % Cube.Size != 0)
                throw new ArgumentException();
            this.playFieldSize = playFieldSize;
            gameField = new Cube[playFieldSize.Width / Cube.Size, playFieldSize.Height / Cube.Size];
            FallingFigure = Tetromino.CreateFigure(FigureType.I, new Point(Cube.Size * 2, Cube.Size));
        }

        public bool InBounds(Point point)
        {
            if (point.X > playFieldSize.Width || point.X < 0)
                return false;
            if (point.Y > playFieldSize.Height)
                return false;
            return true;
        }

        public bool CanMoveFigureTo(Direction direction, Figure figure) //TODO: Фигуры проходят сквозь друг друга.
        {
            var offset = Point.Empty;
            switch(direction)
            {
                case Direction.Left:
                    offset = new Point(-Cube.Size, 0); 
                    break;
                case Direction.Down:
                    offset = new Point(0, Cube.Size);
                    break;
                case Direction.Right:
                    offset = new Point(Cube.Size, 0);
                    break;
            }
            foreach(var block in figure.Cubes)
            {
                var newBlockPosition = new Point(block.Position.X + offset.X, block.Position.Y + offset.Y);
                var newBlockFieldPosition = GetFieldPoint(newBlockPosition);
                if (newBlockFieldPosition.X < 0 || newBlockFieldPosition.X >= 10 || newBlockFieldPosition.Y >= 20)
                    return false;
                if (gameField[newBlockFieldPosition.X, newBlockFieldPosition.Y] != null)
                    return false;
            }
            return true;
        }

        public void Update()
        {
            if(CanMoveFigureTo(Direction.Down, FallingFigure))
                FallingFigure.MoveTo(Direction.Down);
            else
            {
                AddToGameField(FallingFigure);
                FallingFigure = Tetromino.CreateRandomFigure(new Point(Cube.Size * 3, Cube.Size * 2));
                return;
            }
            foreach(var floorNumber in GetFloorsToRemove())
            {
                RemoveFloor(floorNumber);
                LowerBlocks(floorNumber);
            }
        }

        public void KeyDown(object sender, KeyEventArgs args)
        {
            switch(args.KeyCode)
            {
                case Keys.D:
                    if(CanMoveFigureTo(Direction.Right, FallingFigure))
                        FallingFigure.MoveTo(Direction.Right);
                    break;
                case Keys.A:
                    if (CanMoveFigureTo(Direction.Left, FallingFigure))
                        FallingFigure.MoveTo(Direction.Left);
                    break;
                case Keys.W:
                    FallingFigure.Rotate();
                    break;
                case Keys.S:
                    if (CanMoveFigureTo(Direction.Down, FallingFigure))
                        FallingFigure.MoveTo(Direction.Down);
                    break;
                case Keys.Space:
                    while (CanMoveFigureTo(Direction.Down, FallingFigure))
                        FallingFigure.MoveTo(Direction.Down);
                    break;
                default:
                    break;
            }
        }

        public IEnumerable<int> GetFloorsToRemove()
        {
            for (var y = 19; y >= 0; y--)
            {
                var x = 0;
                for (;x < 10; x++)
                    if (gameField[x, y] == null)
                        break;
                if (x == 10)
                    yield return y;
            }
                
        }

        public void RemoveFloor(int floorNumber)
        {
            for (var x = 0; x < 10; x++)
                gameField[x, floorNumber] = null;
        }

        public void LowerBlocks(int floorNumber)
        {
            for (var y = floorNumber; y >= 0; y--)
                for (var x = 0; x < 10; x++)
                    if (gameField[x, y] != null)
                    {
                        gameField[x, y].MoveTo(Direction.Down);
                        gameField[x, y + 1] = new Cube(gameField[x, y]);
                        gameField[x, y] = null;
                    }
                        
        }

        public void AddToGameField(Figure figure)
        {
            foreach (var block in figure.Cubes)
            {
                var fieldPoint = GetFieldPoint(block.Position);
                block.Parent = null;
                gameField[fieldPoint.X, fieldPoint.Y] = block;
            }
        }

        public Point GetFieldPoint(Point point)
        {
            var fieldPoint = new Point(point.X / Cube.Size, point.Y / Cube.Size);
            return fieldPoint;
        }

        public void Draw(Graphics graphics)
        {
            FallingFigure.Draw(graphics);
            foreach (var c in gameField)
                if (c != null)
                    c.Draw(graphics);
        }
    }
}
