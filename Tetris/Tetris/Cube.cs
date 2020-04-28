using System;
using System.Drawing;

namespace Tetris
{
    class Cube : IUpdatable, IDrawable
    {
        public const int Size = 10;

        public Point Position { get; private set; }
        public Brush Brush { get; private set; }
        public Cube Parent { get; private set; }

        public bool HasParent => Parent != null;

        public Point Offset;

        public Cube(Point position, Brush color)
        {
            Position = position;
            Brush = color;
        }

        public Cube(Cube parent, Point offset, Brush brush) 
            : this(new Point(parent.Position.X + offset.X, parent.Position.Y + offset.Y), brush)
        {
            Parent = parent;
            this.Offset = offset;
        }

        public void Update()
        {
            if (HasParent)
                Position = new Point(Parent.Position.X + Offset.X, Parent.Position.Y + Offset.Y);
            else
                Position = new Point(Position.X, Position.Y + Size);
        }

        public void Draw(Graphics graphics)
        {
            graphics.FillRectangle(Brush, new Rectangle(Position, new Size(Size, Size)));
        }
    }
}
