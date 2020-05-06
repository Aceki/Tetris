using System.Drawing;
using System.Numerics;

namespace Tetris
{
    public class Block
    {
        public const int Size = 20;

        private Vector2 position;
        public Vector2 Position
        {
            get
            {
                if (HasParent)
                    position = Parent.Position + Offset;
                return position;
            }
            private set
            {
                position = value;
            }
        }
        public Brush Brush { get; private set; }
        public Block Parent { get; set; }
        public Vector2 Offset { get; set; }

        public bool HasParent
            => Parent != null;

        public Block(Vector2 position, Brush brush)
        {
            Position = position;
            Brush = brush;
        }

        public Block(Block parent, Vector2 offset, Brush brush)
            : this(parent.Position + offset, brush)
        {
            Parent = parent;
            this.Offset = offset;
        }

        public void MoveTo(Direction direction)
        {
            if (HasParent)
                return;
            var moveOffset = Vector2.Zero;
            switch (direction)
            {
                case Direction.Right:
                    moveOffset = new Vector2(Block.Size, 0);
                    break;
                case Direction.Left:
                    moveOffset = new Vector2(-Block.Size, 0);
                    break;
                case Direction.Down:
                    moveOffset = new Vector2(0, Block.Size);
                    break;
                case Direction.Up:
                    moveOffset = new Vector2(0, -Block.Size);
                    break;
                default:
                    break;
            }
            Position += moveOffset;
        }
    }
}
