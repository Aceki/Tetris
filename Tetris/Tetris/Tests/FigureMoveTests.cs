using NUnit.Framework;
using System.Drawing;
using System.Numerics;
using System.Linq;

namespace Tetris
{
    [TestFixture]
    class FigureMoveTests
    {
        [Test]
        public void FigureMoveToRight_ShouldMoveFigureToRight()
        {
            var figure = Tetromino.CreateFigure(FigureType.O, new Vector2(0, 0));
            var startPositions = figure.Blocks.Select(b => b.Position).ToArray();
            figure.MoveTo(Direction.Right);
            for(var i = 0; i < figure.Blocks.Length; i++)
            {
                Assert.AreEqual(startPositions[i].X + Block.Size, figure.Blocks[i].Position.X);
                Assert.AreEqual(startPositions[i].Y, figure.Blocks[i].Position.Y);
            }
        }

        [Test]
        public void FigureMoveToLeft_ShouldMoveFigureToLeft()
        {
            var figure = Tetromino.CreateFigure(FigureType.O, new Vector2(0, 0));
            var startPositions = figure.Blocks.Select(b => b.Position).ToArray();
            figure.MoveTo(Direction.Left);
            for (var i = 0; i < figure.Blocks.Length; i++)
            {
                Assert.AreEqual(startPositions[i].X - Block.Size, figure.Blocks[i].Position.X);
                Assert.AreEqual(startPositions[i].Y, figure.Blocks[i].Position.Y);
            }
        }

        [Test]
        public void FigureMoveToDown_ShouldMoveFigureToDown()
        {
            var figure = Tetromino.CreateFigure(FigureType.O, new Vector2(0, 0));
            var startPositions = figure.Blocks.Select(b => b.Position).ToArray();
            figure.MoveTo(Direction.Down);
            for (var i = 0; i < figure.Blocks.Length; i++)
            {
                Assert.AreEqual(startPositions[i].X, figure.Blocks[i].Position.X);
                Assert.AreEqual(startPositions[i].Y + Block.Size, figure.Blocks[i].Position.Y);
            }
        }

        [Test]
        public void FigureMoveToUp_ShouldMoveFigureToUp()
        {
            var figure = Tetromino.CreateFigure(FigureType.O, new Vector2(0, 0));
            var startPositions = figure.Blocks.Select(b => b.Position).ToArray();
            figure.MoveTo(Direction.Up);
            for (var i = 0; i < figure.Blocks.Length; i++)
            {
                Assert.AreEqual(startPositions[i].X, figure.Blocks[i].Position.X);
                Assert.AreEqual(startPositions[i].Y - Block.Size, figure.Blocks[i].Position.Y);
            }
        }

        [Test]
        public void FigureMoveToLeft_ShouldNotLeaveGameField()
        {
            var fieldSize = new Size(10, 20);
            var game = new GameModel(fieldSize);
            game.Start();
            var figure = Tetromino.CreateFigure(FigureType.O, new Vector2(0, 0));
            Assert.AreEqual(game.CanMoveFigureTo(Direction.Left, figure), false);
        }

        [Test]
        public void FigureMoveToRight_ShouldNotLeaveGameField()
        {
            var fieldSize = new Size(10, 20);
            var game = new GameModel(fieldSize);
            game.Start();
            var figure = Tetromino.CreateFigure(FigureType.O, new Vector2(Block.Size * 8, 0));
            Assert.AreEqual(game.CanMoveFigureTo(Direction.Right, figure), false);
        }

        [Test]
        public void FigureMoveToDown_ShouldNotLeaveGameField()
        {
            var fieldSize = new Size(10, 20);
            var game = new GameModel(fieldSize);
            game.Start();
            var figure = Tetromino.CreateFigure(FigureType.O, new Vector2(0, Block.Size * 18));
            Assert.AreEqual(game.CanMoveFigureTo(Direction.Down, figure), false);
        }
    }
}
