﻿using System;
using System.Collections.Generic;
using System.Numerics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using NUnit.Framework.Constraints;

namespace Tetris
{
    public class GameModelDrawer
    {
        public readonly Color GridColor;
        public readonly Vector PlayAreaPosition;
        public readonly Color FieldsBackGroundColor;
        public GameModel Model { get; set; }
        
        public GameModelDrawer(GameModel scene)
        {
            PlayAreaPosition = new Vector(200, 0);
            FieldsBackGroundColor = Color.FromArgb(230, 0, 0, 0);
            GridColor = Color.FromArgb(0x30af19ff);
            Model = scene;
        }

        public void Draw(object sender, PaintEventArgs args)
        {
            
            var graphics = args.Graphics;
            DrawPlayArea(graphics, Model.GameFieldSize.Width, Model.GameFieldSize.Height, PlayAreaPosition);
            DrawGameFieldBounds(graphics, PlayAreaPosition);
            DrawNextFigureContainer(graphics, PlayAreaPosition + new Vector(Block.Size * 12, Block.Size * 3));
            DrawHoldFigureContainer(graphics, PlayAreaPosition + new Vector(Block.Size * 12, Block.Size * 11));
            DrawScore(graphics, PlayAreaPosition + new Vector(-Block.Size * 8, Block.Size*6));
            DrawLineScore(graphics, PlayAreaPosition + new Vector(-Block.Size * 8, Block.Size));
            DrawBlocks(graphics, Model.GetBlocksFromField(), PlayAreaPosition);
        }

        public void DrawPlayArea(Graphics graphics, int verticalCount, int horizontalCount, Vector position)
        {
            graphics.FillRectangle(new SolidBrush(FieldsBackGroundColor), new Rectangle(position, new Size(Model.GameFieldSize.Width * Block.Size, Model.GameFieldSize.Height * Block.Size))); ;
            for (var i = 0; i < verticalCount; i++)
                graphics.DrawLine(new Pen(GridColor), position + new Vector(Block.Size * i, 0), position + new Vector(Block.Size * i, Block.Size * horizontalCount));
            for (var i = 0; i < horizontalCount; i++)
                graphics.DrawLine(new Pen(GridColor), position + new Vector(0, Block.Size * i), position + new Vector(Block.Size * verticalCount, Block.Size * i));
        }

        public void DrawScore(Graphics graphics, Vector position)
        {
            graphics.DrawString("SCORE", new Font(FontFamily.GenericMonospace, 30), Brushes.White, (Point)position);
            graphics.DrawString(Model.Score.ToString(), new Font(FontFamily.GenericMonospace, 30), Brushes.White, (Point)(position + new Vector(Block.Size, Block.Size * 2)));
        }

        public void DrawLineScore(Graphics graphics, Vector position)
        {
            graphics.DrawString("LINES", new Font(FontFamily.GenericMonospace, 30), Brushes.White, (Point)position);
            graphics.DrawString(Model.LinesScore.ToString(), new Font(FontFamily.GenericMonospace, 30), Brushes.White, (Point)(position + new Vector(Block.Size, Block.Size * 2)));
        }

        public void DrawNextFigureContainer(Graphics graphics, Vector position)
        {
            var nextFigureRect = new Rectangle((Point)position, new Size(Block.Size * 5, Block.Size * 5));
            graphics.DrawString("NEXT", new Font(FontFamily.GenericMonospace, 30), Brushes.White, (Point)new Vector(nextFigureRect.X - 4, nextFigureRect.Y - Block.Size * 2));
            graphics.FillRectangle(new SolidBrush(FieldsBackGroundColor), nextFigureRect);
            graphics.DrawRectangle(Pens.White, nextFigureRect);
            foreach (var block in Tetromino.CreateFigure(Model.NextFallingFigureType, new Vector(nextFigureRect.X + Block.Size * 2, nextFigureRect.Y + Block.Size * 2)).Blocks)
                DrawBlock(graphics, Brushes.Aqua, new Vector(block.Position.X, block.Position.Y), new Size(Block.Size, Block.Size));
        }

        public void DrawHoldFigureContainer(Graphics graphics, Vector position)
        {
            var holdFigureRect = new Rectangle((Point)position, new Size(Block.Size * 5, Block.Size * 5));
            graphics.DrawString("HOLD", new Font(FontFamily.GenericMonospace, 30), Brushes.White, (Point)new Vector(holdFigureRect.X - 4, holdFigureRect.Y - Block.Size * 2));
            graphics.FillRectangle(new SolidBrush(FieldsBackGroundColor), holdFigureRect);
            graphics.DrawRectangle(Pens.White, holdFigureRect);
            if(Model.HoldedFallingFigureType != null)
                foreach (var block in Tetromino.CreateFigure(Model.HoldedFallingFigureType.Value, new Vector(holdFigureRect.X + Block.Size * 2, holdFigureRect.Y + Block.Size * 2)).Blocks)
                    DrawBlock(graphics, Brushes.Orange, new Vector(block.Position.X, block.Position.Y), new Size(Block.Size, Block.Size));
        }

        public void DrawGameFieldBounds(Graphics graphics, Vector position)
        {
            graphics.DrawRectangle(Pens.White, position.X, position.Y, Block.Size * Model.GameFieldSize.Width, Block.Size * Model.GameFieldSize.Height);
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
