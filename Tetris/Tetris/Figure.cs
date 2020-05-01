using System.Drawing;
using System;
using System.Windows.Forms;
using System.Linq;
using System.Collections.Generic;

namespace Tetris
{
    class Figure
    {
        public readonly FigureType Type;

        public Point Position => Cubes.OrderBy(c => c.Position.Y).Last().Position;

        public Cube[] Cubes;

        private Cube rootCube;

        public Figure(Cube[] cubes, FigureType type)
        {
            rootCube = cubes[0];
            this.Cubes = cubes;
            Type = type;
        }

        public void MoveTo(Direction direction)
            => rootCube.MoveTo(direction);

        public void Rotate()
        {
            if (Type == FigureType.O)
                return;
            var angle = Math.PI / 2d;
            var newOffsets = new Point[4];
            for (var i = 1; i < 4; i++)
            {
                var x = (int)(Cubes[i].Offset.X * Math.Cos(angle) - Cubes[i].Offset.Y * Math.Sin(angle));
                var y = (int)(Cubes[i].Offset.X * Math.Sin(angle) + Cubes[i].Offset.Y * Math.Cos(angle));
                newOffsets[i] = new Point(x, y);
            }
            for (var i = 1; i < 4; i++)
            {
                Cubes[i].Offset = newOffsets[i];
            }
        }

        public void Draw(Graphics graphics)
        {
            foreach (var block in Cubes)
                block.Draw(graphics);
        }
    }
}
