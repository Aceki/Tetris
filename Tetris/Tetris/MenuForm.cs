using System.Drawing;
using System.Windows.Forms;

namespace Tetris
{
    public partial class MenuForm : Form
    {
        private Timer updateTimer = new Timer();
        private Timer graphicTimer = new Timer();
        private GameScene gameScene;

        public MenuForm()
        {
            InitializeComponent();
            gameScene = new GameScene(new Size(10, 20));
            gameScene.GameOver += (sender, args) => MessageBox.Show(args.Message, "Game over", MessageBoxButtons.OK);
            gameScene.GameOver += (sender, args) => gameScene.Start();
            updateTimer.Interval = 300;
            graphicTimer.Interval = 1;
            updateTimer.Tick += (s, args) => gameScene.Update();
            graphicTimer.Tick += (s, args) => Invalidate();
            Paint += (s, args) => gameScene.Draw(args.Graphics);
            KeyDown += gameScene.KeyDown;
            Load += (sender, args) => gameScene.Start();
            updateTimer.Start();
            graphicTimer.Start();
        }

        //TODO: Сделать управление независимым от таймера. V
        //TODO: Выделить отдельную сущность для управления.
    }
}
