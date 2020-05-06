using NUnit.Framework;
using System.Numerics;
using System.Drawing;

namespace Tetris.Tests
{
    [TestFixture]
    class FigureTests
    {
        [Test]
        public void FigureBlocks_HasParent()
        {
            for(var i = 0; i < 7; i++)
            {
                var figure = Tetromino.CreateFigure((FigureType)i, new Vector2(0, 0));
                for (var j = 1; j < figure.Blocks.Length; j++)
                {
                    Assert.AreEqual(figure.Blocks[j].HasParent, true);
                }
            }
        }

        [Test]
        public void FigureRootBlock_HasNotParent()
        {
            for (var i = 0; i < 7; i++)
            {
                var figure = Tetromino.CreateFigure((FigureType)i, new Vector2(0, 0));
                Assert.AreEqual(figure.Blocks[0].HasParent, false);
            }
        }
    }
}
