using System;
using System.Collections.Generic;
using System.Numerics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tetris
{
    public class GameModelDrawer
    {
        public readonly Color GridColor;
        public GameModel Scene { get; set; }
        public readonly Vector PlayAreaPosition;

        public GameModelDrawer(GameModel scene)
        {
            PlayAreaPosition = new Vector(195, 0);
            GridColor = Color.FromArgb(0x1A1919FF);
            Scene = scene;
        }

        public void Draw(object sender, PaintEventArgs args)
        {
            var graphics = args.Graphics;
            graphics.Clear(Color.Black);
            DrawGrid(graphics, Scene.GameFieldSize.Width, Scene.GameFieldSize.Height, PlayAreaPosition);
            DrawGameFieldBounds(graphics, PlayAreaPosition);
            DrawNextFigureContainer(graphics, PlayAreaPosition + new Vector(Block.Size * 12, Block.Size * 2));
            DrawLineScore(graphics, PlayAreaPosition);
            DrawBlocks(graphics, Scene.GetBlocksFromField(), PlayAreaPosition);
        }

        public void DrawGrid(Graphics graphics, int verticalCount, int horizontalCount, Vector position) //TODO: TODO
        {
            for (var i = 0; i < verticalCount; i++)
                graphics.DrawLine(new Pen(GridColor), position + new Vector(Block.Size * i, 0), position + new Vector(Block.Size * i, Block.Size * horizontalCount));
            for (var i = 0; i < horizontalCount; i++)
                graphics.DrawLine(new Pen(GridColor), position + new Vector(0, Block.Size * i), position + new Vector(Block.Size * verticalCount, Block.Size * i));
        }

        public void DrawLineScore(Graphics graphics, Vector position)
        {
            graphics.DrawString("LINES", new Font(FontFamily.GenericMonospace, 30), Brushes.White, (Point)(position - new Vector(Block.Size * 8, 0)));
            graphics.DrawString(Scene.LinesScore.ToString(), new Font(FontFamily.GenericMonospace, 30), Brushes.White, (Point)(position - new Vector(Block.Size * 7, -Block.Size * 2)));
        }

        public void DrawNextFigureContainer(Graphics graphics, Vector position)
        {
            var nextFigureRect = new Rectangle((Point)position, new Size(Block.Size * 5, Block.Size * 5));
            graphics.DrawString("NEXT", new Font(FontFamily.GenericMonospace, 30), Brushes.White, (Point)new Vector(nextFigureRect.X - 4, nextFigureRect.Y - Block.Size * 2));
            graphics.DrawRectangle(Pens.White, nextFigureRect);
            foreach (var block in Tetromino.CreateFigure(Scene.NextFallingFigureType, new Vector(nextFigureRect.X + Block.Size * 2, nextFigureRect.Y + Block.Size * 2)).Blocks)
                DrawBlock(graphics, Brushes.Aqua, new Vector(block.Position.X, block.Position.Y), new Size(Block.Size, Block.Size));
        }

        public void DrawGameFieldBounds(Graphics graphics, Vector position)
        {
            graphics.DrawRectangle(Pens.White, position.X, position.Y, Block.Size * Scene.GameFieldSize.Width, Block.Size * Scene.GameFieldSize.Height);
        }

        public void DrawBlocks(Graphics graphics, IEnumerable<Block> blocks, Vector position)
        {
            foreach (var block in blocks)
                DrawBlock(graphics, block.Brush, position + new Vector(block.Position.X, block.Position.Y), new Size(Block.Size, Block.Size));
        }

        public void DrawBlock(Graphics graphics, Brush brush, Vector position, Size size)
        {
            graphics.FillRectangle(brush, new Rectangle(position, size));
        }
    }
}
