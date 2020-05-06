using NUnit.Framework;
using System.Drawing;
using System.Numerics;

namespace Tetris.Tests
{
    [TestFixture]
    class TetrominoTests
    {
        [Test]
        public void Tetromino_CreatesCorrectFigures()
        {
            for(var i = 0; i < 7; i++)
            {
                var figureType = (FigureType)i;
                var figure = Tetromino.CreateFigure(figureType, new Vector(0, 0));
                Assert.AreEqual(figure.Type, figureType);
            }
        }
    }
}
