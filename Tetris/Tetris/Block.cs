using System.Drawing;

namespace Tetris
{
    public class Block
    {
        public const int Size = 20;

        private Point position;
        public Point Position
        {
            get
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
        public Block Parent { get; set; }
        public Point Offset { get; set; }

        public bool HasParent
            => Parent != null;

        public Block(Point position, Brush brush)
        {
            Position = position;
            Brush = brush;
        }

        public Block(Block parent, Point offset, Brush brush)
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
                    Position = new Point(Position.X + Block.Size, Position.Y);
                    break;
                case Direction.Left:
                    Position = new Point(Position.X - Block.Size, Position.Y);
                    break;
                case Direction.Down:
                    Position = new Point(Position.X, Position.Y + Block.Size);
                    break;
                case Direction.Up:
                    Position = new Point(Position.X, Position.Y - Block.Size);
                    break;
                default:
                    break;
            }
        }
    }
}
