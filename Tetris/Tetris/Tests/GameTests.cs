using NUnit.Framework;
using System.Numerics;
using System.Drawing;

namespace Tetris.Tests
{
    [TestFixture]
    class GameTests
    {
        [Test]
        public void GameOver_WhenPlaceBlockOnTop()
        {
            var game = new GameModel(new Size(10,20));
            var figure = Tetromino.CreateFigure(FigureType.O, new Vector(0, -Block.Size * 2));
            game.StartGame();
            game.AddToGameField(figure);
            Assert.AreEqual(game.GameIsOver, true);
        }
    }
}
 