using NUnit.Framework;
using System.Windows.Forms;
using System.Numerics;
using System.Drawing;
using System.Linq;

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

        [Test]
        public void FigureSaves_WhenHoldButtonDown()
        {
            var game = new GameModel(new Size(10, 20));
            game.StartGame();
            Assert.AreEqual(game.HoldedFallingFigureType, null);
            game.KeyDown(Keys.Tab);
            Assert.AreNotEqual(game.HoldedFallingFigureType, null);
        }

        [Test]
        public void ExitEventInvoke_WhenEscapeButtonDown()
        {
            var game = new GameModel(new Size(10, 20));
            game.StartGame();
            var f = false;
            game.Exit += (sender, args) => f = true;
            game.KeyDown(Keys.Escape);
            Assert.AreEqual(f, true);
        }

        [Test]
        public void GameGetBlockFromField_WorksCorrect()
        {
            var game = new GameModel(new Size(10, 20));
            var figure = Tetromino.CreateFigure(FigureType.O, new Vector(0, Block.Size * 2));
            game.StartGame();
            game.AddToGameField(figure);
            var blockFromField = game.GetBlocksFromField();
            foreach (var block in figure.Blocks)
                Assert.AreEqual(blockFromField.Contains(block), true);
        }
    }
}
 