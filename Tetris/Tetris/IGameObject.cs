using System;
using System.Drawing;

namespace Tetris
{
    public interface IGameObject
    {
        Point Position { get; }
        void Update();
        void Draw(Graphics graphics);
    }
}
