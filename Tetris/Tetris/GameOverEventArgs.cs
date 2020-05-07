namespace Tetris
{
    public class GameOverEventArgs
    {
        public readonly string Message;

        public GameOverEventArgs(string message)
        {
            Message = message;
        }
    }
}
