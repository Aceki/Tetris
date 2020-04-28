using System.Drawing;
using System;

namespace Tetris
{
    class Figure : IUpdatable, IDrawable 
    {
        public readonly FigureType Type;

        public Point Position => cubes[0].Position;

        private Cube[] cubes;

        public Figure(Cube[] cubes, FigureType type)
        {
            this.cubes = cubes;
            Type = type;
        }

        public void Rotate()
        {
            if (Type == FigureType.O)
                return;
            var angle = Math.PI / 2d;
            for(var i = 1; i < cubes.Length; i++)
                cubes[i].Offset = new Point((int)(cubes[i].Offset.X * Math.Cos(angle) - cubes[i].Offset.Y * Math.Sin(angle)),
                    (int)(cubes[i].Offset.X * Math.Sin(angle) + cubes[i].Offset.Y * Math.Cos(angle)));
        }

        public void Update()
        {
            foreach (var c in cubes)
                c.Update();
            Rotate();
        }

        public void Draw(Graphics graphics)
        {
            foreach (var c in cubes)
                c.Draw(graphics);
        }
    }
}
