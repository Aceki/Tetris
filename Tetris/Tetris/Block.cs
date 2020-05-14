using System.Drawing;
using System.Numerics;

namespace Tetris
{
    public class Block
    {
        public const int Size = 20;
        public Vector Position { get; private set; }
        public Brush Brush { get; private set; }

        public Block(Vector position, Brush brush)
        {
            Position = position;
            Brush = brush;
        }

        public Block(Block parent, Vector offset, Brush brush)
            : this(parent.Position + offset, brush) { }

        public void MoveTo(Direction direction)
        {
            var moveOffset = Vector.Zero;
            switch (direction)
            {
                case Direction.Right:
                    moveOffset = new Vector(Block.Size, 0);
                    break;
                case Direction.Left:
                    moveOffset = new Vector(-Block.Size, 0);
                    break;
                case Direction.Down:
                    moveOffset = new Vector(0, Block.Size);
                    break;
                case Direction.Up:
                    moveOffset = new Vector(0, -Block.Size);
                    break;
                default:
                    break;
            }
            Position += moveOffset;
        }

        public void SetTo(Vector position)
        {
            Position = position;
        }
    }
}
