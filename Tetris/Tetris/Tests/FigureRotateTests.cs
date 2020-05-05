﻿using NUnit.Framework;
using System.Drawing;
using System.Linq;

namespace Tetris.Tests
{
    [TestFixture]
    class FigureRotateTests
    {
        [Test]
        public void Figure_ShouldNotRotate_WhenNearBorder()
        {
            var game = new GameScene(new Size(10, 20));
            var figure = Tetromino.CreateFigure(FigureType.S, new Point(0, 0));
            game.Start();
            Assert.AreEqual(game.CanRotateFigure(figure), false);
        }

        [Test]
        public void FigureTypeO_ShouldNotRotate()
        {
            var figure = Tetromino.CreateFigure(FigureType.O, new Point(Block.Size * 2, Block.Size * 2));
            var startBlocksPositions = figure.Blocks.Select(b => b.Position).ToArray();
            figure.Rotate();
            for(var i = 0; i < figure.Blocks.Length; i++)
            {
                Assert.AreEqual(startBlocksPositions[i], figure.Blocks[i].Position);
            }
        }
    }
}
