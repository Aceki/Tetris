namespace Tetris
{
    public class GameOverEventArgs
    {
        public string Message { get; private set; }

        public GameOverEventArgs(string message)
        {
            Message = message;
        }
    }
}
