using System;
using System.Drawing;

namespace Tetris
{
    class Cube
    {
        public const int Size = 20;

        private Point position;
        public Point Position 
        {   get
            {
                if (HasParent)
                    position = new Point(Parent.Position.X + Offset.X, Parent.Position.Y + Offset.Y);
                return position;
            }
            private set
            {
                position = value;
            } 
        }
        public Brush Brush { get; private set; }
        public Cube Parent { get; set; }

        public bool HasParent => Parent != null;

        public Point Offset = Point.Empty;

        public Cube(Cube cube)
        {
            Position = cube.Position;
            Brush = cube.Brush;
        }

        public Cube(Point position, Brush brush)
        {
            Position = position;
            Brush = brush;
        }

        public Cube(Cube parent, Point offset, Brush brush) 
            : this(new Point(parent.Position.X + offset.X, parent.Position.Y + offset.Y), brush)
        {
            Parent = parent;
            this.Offset = offset;
        }

        public void MoveTo(Direction direction)
        {
            if (HasParent) 
                return;
            switch (direction)
            {
                case Direction.Right:
                    Position = new Point(Position.X + Cube.Size, Position.Y);
                    break;
                case Direction.Left:
                    Position = new Point(Position.X - Cube.Size, Position.Y);
                    break;
                case Direction.Down:
                    Position = new Point(Position.X, Position.Y + Cube.Size);
                    break;
                case Direction.Up:
                    Position = new Point(Position.X, Position.Y - Cube.Size);
                    break;
                default:
                    break;
            }
        }

        public void Draw(Graphics graphics)
        {
            graphics.FillRectangle(Brush, new Rectangle(Position, new Size(Size, Size)));
        }
    }
}
