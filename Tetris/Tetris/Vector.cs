using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;
using System.Drawing;

namespace Tetris
{
    public struct Vector
    {
        public static Vector Zero
            => new Vector(0, 0);

        public int X { get; set; }
        public int Y { get; set; }

        public Vector(int x, int y)
        {
            X = x;
            Y = y;
        }

        public static Vector operator +(Vector a, Vector b)
            => new Vector(a.X + b.X, a.Y + b.Y);

        public static Vector operator -(Vector a, Vector b)
            => new Vector(a.X - b.X, a.Y - b.Y);

        public static Vector operator *(Vector a, int number)
            => new Vector(a.X * number, a.Y * number);

        public static Vector operator /(Vector a, int number)
            => new Vector(a.X / number, a.Y / number);

        public static implicit operator Point(Vector vector)
            => new Point(vector.X, vector.Y);

        public int GetLength()
            => (int)Math.Sqrt(X * X + Y * Y);

        public void Rotate(double angle)
        {
            var x = (int)(X * Math.Cos(angle) - Y * Math.Sin(angle));
            var y = (int)(X * Math.Sin(angle) + Y * Math.Cos(angle));
            X = x;
            Y = y;
        }
    }
}
