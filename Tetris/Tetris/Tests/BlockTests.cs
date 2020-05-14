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

        [Test]
        public void BlockMoveTo_ReturnsCorrectPosition()
        {
            var block = new Block(Vector.Zero, Brushes.White);
            block.MoveTo(Direction.Right);
            Assert.AreEqual(block.Position, Vector.Zero + new Vector(Block.Size, 0));
            block.MoveTo(Direction.Left);
            Assert.AreEqual(block.Position, Vector.Zero);
            block.MoveTo(Direction.Up);
            Assert.AreEqual(block.Position, Vector.Zero + new Vector(0, -Block.Size));
            block.MoveTo(Direction.Down);
            Assert.AreEqual(block.Position, Vector.Zero);
        }
    }
}
