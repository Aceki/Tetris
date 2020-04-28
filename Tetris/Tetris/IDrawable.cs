using System.Drawing;

namespace Tetris
{
    interface IDrawable
    {
        void Draw(Graphics graphics);
        Point Position { get; }
    }
}
