using System;
using System.Drawing;

namespace Tetris
{
    static class Tetromino
    {
        private static Random rnd = new Random();

        public static Figure CreateFigure(FigureType type, Point position)
        {
            var fArray = new Cube[4];
            var brush = GetRandomBrush();
            switch (type)
            {
                case FigureType.I: 
                    {
                        fArray[0] = new Cube(position, brush);
                        fArray[1] = new Cube(fArray[0], new Point(0, Cube.Size), brush);
                        fArray[2] = new Cube(fArray[0], new Point(0, -Cube.Size), brush);
                        fArray[3] = new Cube(fArray[2], new Point(0, -Cube.Size), brush);
                        return new Figure(fArray, type);
                    };
                case FigureType.T:
                    {
                        fArray[0] = new Cube(position, brush);
                        fArray[1] = new Cube(fArray[0], new Point(Cube.Size, 0), brush);
                        fArray[2] = new Cube(fArray[0], new Point(-Cube.Size, 0), brush);
                        fArray[3] = new Cube(fArray[0], new Point(0, Cube.Size), brush);
                        return new Figure(fArray, type);
                    }
                case FigureType.J:
                    {
                        fArray[0] = new Cube(position, brush);
                        fArray[1] = new Cube(fArray[0], new Point(0, Cube.Size), brush);
                        fArray[2] = new Cube(fArray[0], new Point(0, -Cube.Size), brush);
                        fArray[3] = new Cube(fArray[2], new Point(Cube.Size, 0), brush);
                        return new Figure(fArray, type);
                    }
                case FigureType.L:
                    {
                        fArray[0] = new Cube(position, brush);
                        fArray[1] = new Cube(fArray[0], new Point(0, Cube.Size), brush);
                        fArray[2] = new Cube(fArray[0], new Point(0, -Cube.Size), brush);
                        fArray[3] = new Cube(fArray[2], new Point(-Cube.Size, 0), brush);
                        return new Figure(fArray, type);
                    }
                case FigureType.O:
                    {
                        fArray[0] = new Cube(position, brush);
                        fArray[1] = new Cube(fArray[0], new Point(Cube.Size, 0), brush);
                        fArray[2] = new Cube(fArray[0], new Point(0, Cube.Size), brush);
                        fArray[3] = new Cube(fArray[2], new Point(Cube.Size, 0), brush);
                        return new Figure(fArray, type);
                    }
                case FigureType.S:
                    {
                        fArray[0] = new Cube(position, brush);
                        fArray[1] = new Cube(fArray[0], new Point(0, Cube.Size), brush);
                        fArray[2] = new Cube(fArray[0], new Point(Cube.Size, 0), brush);
                        fArray[3] = new Cube(fArray[1], new Point(-Cube.Size, 0), brush);
                        return new Figure(fArray, type);
                    }
                case FigureType.Z:
                    {
                        fArray[0] = new Cube(position, brush);
                        fArray[1] = new Cube(fArray[0], new Point(0, Cube.Size), brush);
                        fArray[2] = new Cube(fArray[0], new Point(-Cube.Size, 0), brush);
                        fArray[3] = new Cube(fArray[1], new Point(Cube.Size, 0), brush);
                        return new Figure(fArray, type);
                    }
                default: throw new Exception("This figure type isn't exists!");
            }
        }

        public static Brush GetRandomBrush()
        {
            var bArray = new Brush[] 
            { 
                Brushes.Red, 
                Brushes.Aqua, 
                Brushes.Green, 
                Brushes.Orange 
            };
            return bArray[rnd.Next(0, bArray.Length)];
        }

        public static Figure CreateRandomFigure(Point position)
            => CreateFigure((FigureType)rnd.Next(0, 7), position);
    }
}
