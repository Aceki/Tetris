using System.Drawing;
using System;
using System.Windows.Forms;
using System.Linq;
using System.Numerics;

namespace Tetris
{
    public class Figure
    {
        public const double RotateAngle = Math.PI / 2;
        public readonly FigureType Type;

        public Vector Position
            => Blocks[0].Position;

        public Block[] Blocks;

        private Block rootBlock;

        public Figure(Block[] blocks, FigureType type)
        {
            rootBlock = blocks[0];
            this.Blocks = blocks;
            Type = type;
        }

        public void MoveTo(Direction direction)
        {
            foreach (var b in Blocks)
                b.MoveTo(direction);
        }

        public void Rotate()
        {
            if (Type == FigureType.O)
                return;
            for (var i = 1; i < 4; i++)
            {
                var offset = Blocks[i].Position - rootBlock.Position;
                offset.Rotate(RotateAngle);
                Blocks[i].SetTo(rootBlock.Position + offset);
            }
        }
    }
}
