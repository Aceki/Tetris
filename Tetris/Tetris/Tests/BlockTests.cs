using NUnit.Framework;
using System.Drawing;
using System.Numerics;

namespace Tetris.Tests
{
    [TestFixture]
    class BlockTests
    {
        [Test]
        public void BlockWithParent_ReturnsCorrectPosition()
        {
            var parentBlock = new Block(new Vector(1, 2), Brushes.White);
            var childBlock = new Block(parentBlock, new Vector(1, 1), Brushes.White);
            Assert.AreEqual(childBlock.Position.X, 2);
            Assert.AreEqual(childBlock.Position.Y, 3);
        }
    }
}
