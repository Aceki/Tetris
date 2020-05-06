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
            => rootBlock.MoveTo(direction);

        public void Rotate()
        {
            if (Type == FigureType.O)
                return;
            for (var i = 1; i < 4; i++)
            {
                var x = (int)(Blocks[i].Offset.X * Math.Cos(RotateAngle) - Blocks[i].Offset.Y * Math.Sin(RotateAngle));
                var y = (int)(Blocks[i].Offset.X * Math.Sin(RotateAngle) + Blocks[i].Offset.Y * Math.Cos(RotateAngle));
                Blocks[i].Offset = new Vector(x, y);
            }
        }
    }
}
