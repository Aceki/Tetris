using System;
using System.Drawing;
using System.Numerics;

namespace Tetris
{
    public static class Tetromino
    {
        private static Random rnd = new Random();
        public static Brush[] AllowedBrushes = new[]
        {
            Brushes.Red,
            Brushes.Aqua,
            Brushes.Green,
            Brushes.Orange,
            Brushes.Purple
        };

        public static Figure CreateFigure(FigureType type, Vector position)
        {
            var figureArray = new Block[4];
            var brush = GetRandomBrush();
            switch (type)
            {
                case FigureType.I: 
                        figureArray[0] = new Block(position, brush);
                        figureArray[1] = new Block(figureArray[0], new Vector(0, Block.Size), brush);
                        figureArray[2] = new Block(figureArray[0], new Vector(0, -Block.Size), brush);
                        figureArray[3] = new Block(figureArray[0], new Vector(0, -Block.Size * 2), brush);
                        return new Figure(figureArray, type);
                case FigureType.T:
                        figureArray[0] = new Block(position, brush);
                        figureArray[1] = new Block(figureArray[0], new Vector(Block.Size, 0), brush);
                        figureArray[2] = new Block(figureArray[0], new Vector(-Block.Size, 0), brush);
                        figureArray[3] = new Block(figureArray[0], new Vector(0, Block.Size), brush);
                        return new Figure(figureArray, type);
                case FigureType.J:
                        figureArray[0] = new Block(position, brush);
                        figureArray[1] = new Block(figureArray[0], new Vector(0, Block.Size), brush);
                        figureArray[2] = new Block(figureArray[0], new Vector(0, -Block.Size), brush);
                        figureArray[3] = new Block(figureArray[0], new Vector(-Block.Size, Block.Size), brush);
                        return new Figure(figureArray, type);
                case FigureType.L:
                        figureArray[0] = new Block(position, brush);
                        figureArray[1] = new Block(figureArray[0], new Vector(0, Block.Size), brush);
                        figureArray[2] = new Block(figureArray[0], new Vector(0, -Block.Size), brush);
                        figureArray[3] = new Block(figureArray[0], new Vector(Block.Size, Block.Size), brush);
                        return new Figure(figureArray, type);
                case FigureType.O:
                        figureArray[0] = new Block(position, brush);
                        figureArray[1] = new Block(figureArray[0], new Vector(Block.Size, 0), brush);
                        figureArray[2] = new Block(figureArray[0], new Vector(0, Block.Size), brush);
                        figureArray[3] = new Block(figureArray[0], new Vector(Block.Size, Block.Size), brush);
                        return new Figure(figureArray, type);
                case FigureType.S:
                        figureArray[0] = new Block(position, brush);
                        figureArray[1] = new Block(figureArray[0], new Vector(0, Block.Size), brush);
                        figureArray[2] = new Block(figureArray[0], new Vector(Block.Size, 0), brush);
                        figureArray[3] = new Block(figureArray[0], new Vector(-Block.Size, Block.Size), brush);
                        return new Figure(figureArray, type);
                case FigureType.Z:
                        figureArray[0] = new Block(position, brush);
                        figureArray[1] = new Block(figureArray[0], new Vector(0, Block.Size), brush);
                        figureArray[2] = new Block(figureArray[0], new Vector(-Block.Size, 0), brush);
                        figureArray[3] = new Block(figureArray[0], new Vector(Block.Size, Block.Size), brush);
                        return new Figure(figureArray, type);
                default: throw new Exception("This figure type isn't exists!");
            }
        }

        public static Brush GetRandomBrush()
            => AllowedBrushes[rnd.Next(0, AllowedBrushes.Length)];

        public static FigureType GetRandomType()
            => (FigureType)rnd.Next(0, 7);

        public static Figure CreateRandomFigure(Vector position)
            => CreateFigure(GetRandomType(), position);
    }
}
