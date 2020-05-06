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
        public readonly Point PlayAreaOffset;

        public GameModelDrawer(GameModel scene)
        {
            PlayAreaOffset = new Point(195, 0);
            GridColor = Color.FromArgb(0x1A1919FF);
            Scene = scene;
        }

        public void Draw(object sender, PaintEventArgs args)
        {
            var graphics = args.Graphics;
            graphics.Clear(Color.Black);
            DrawGrid(graphics);
            DrawNextFigureContainer(graphics);
            DrawLineScore(graphics);
            DrawBlocks(graphics, Scene.GetBlocksFromField());
        }

        public void DrawGrid(Graphics graphics)
        {
            for (var i = 0; i < Scene.GameFieldSize.Width; i++)
                graphics.DrawLine(new Pen(GridColor), new Point(PlayAreaOffset.X + Block.Size * i, PlayAreaOffset.Y), new Point(PlayAreaOffset.X + Block.Size * i, PlayAreaOffset.Y + Block.Size * Scene.GameFieldSize.Height));
            for (var i = 0; i < Scene.GameFieldSize.Height; i++)
                graphics.DrawLine(new Pen(GridColor), new Point(PlayAreaOffset.X, PlayAreaOffset.Y + Block.Size * i), new Point(PlayAreaOffset.X + Block.Size * Scene.GameFieldSize.Width, PlayAreaOffset.Y + Block.Size * i));
        }

        public void DrawLineScore(Graphics graphics)
        {
            graphics.DrawString("LINES", new Font(FontFamily.GenericMonospace, 30), Brushes.White, new Point(PlayAreaOffset.X - Block.Size * 8, PlayAreaOffset.Y));
            graphics.DrawString(Scene.LinesScore.ToString(), new Font(FontFamily.GenericMonospace, 30), Brushes.White, new Point(PlayAreaOffset.X - Block.Size * 7, PlayAreaOffset.Y + Block.Size * 2));
        }

        public void DrawNextFigureContainer(Graphics graphics)
        {
            var nextFigureRect = new Rectangle(new Point(PlayAreaOffset.X + Block.Size * 12, PlayAreaOffset.Y + Block.Size * 2), new Size(Block.Size * 5, Block.Size * 5));
            graphics.DrawString("NEXT", new Font(FontFamily.GenericMonospace, 30), Brushes.White, new Point(nextFigureRect.X - 4, nextFigureRect.Y - Block.Size * 2));
            graphics.DrawRectangle(Pens.White, PlayAreaOffset.X, PlayAreaOffset.Y, Block.Size * Scene.GameFieldSize.Width, Block.Size * Scene.GameFieldSize.Height);
            graphics.DrawRectangle(Pens.White, nextFigureRect);
            foreach (var block in Tetromino.CreateFigure(Scene.NextFallingFigureType, new Vector2(nextFigureRect.X + Block.Size * 2, nextFigureRect.Y + Block.Size * 2)).Blocks)
                DrawBlock(graphics, Brushes.Aqua, new Point((int)block.Position.X, (int)block.Position.Y), new Size(Block.Size, Block.Size));
        }

        public void DrawBlocks(Graphics graphics, IEnumerable<Block> blocks)
        {
            foreach (var block in blocks)
                DrawBlock(graphics, block.Brush, new Point((int)block.Position.X + PlayAreaOffset.X, (int)block.Position.Y + PlayAreaOffset.Y), new Size(Block.Size, Block.Size));
        }

        public void DrawBlock(Graphics graphics, Brush brush, Point position, Size size)
        {
            graphics.FillRectangle(brush, new Rectangle(position, size));
        }
    }
}
