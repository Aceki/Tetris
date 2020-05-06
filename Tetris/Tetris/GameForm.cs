using System.Drawing;
using System.Windows.Forms;

namespace Tetris
{
    public partial class GameForm : Form
    {
        public GameModel Model { get; private set; }
        public GameModelDrawer Drawer { get; private set; }

        public GameForm()
        {
            InitializeComponent();
            Model = new GameModel(new Size(10, 20));
            Drawer = new GameModelDrawer(Model);
            var updateTimer = new Timer();
            var graphicTimer = new Timer();
            Model.GameOver += (sender, args) => MessageBox.Show(args.Message, "Game over", MessageBoxButtons.OK);
            Model.GameOver += (sender, args) => Model.Start();
            Model.Exit += (sender, args) =>
            {
                var result = ShowExitMessage();
                if (result == DialogResult.Yes)
                {
                    updateTimer.Dispose();
                    graphicTimer.Dispose();
                    this.Close();
                }
            };
            this.FormClosed += (sender, args) =>
            {
                updateTimer.Dispose();
                graphicTimer.Dispose();
            };
            updateTimer.Interval = 300;
            graphicTimer.Interval = 1;
            updateTimer.Tick += (s, args) => Model.Update();
            graphicTimer.Tick += (s, args) => Invalidate();
            Paint += Drawer.Draw;
            KeyDown += Model.KeyDown;
            Load += (sender, args) => Model.Start();
            updateTimer.Start();
            graphicTimer.Start();
        }

        public DialogResult ShowExitMessage()
            => MessageBox.Show("Вы действительно хотите закончить игру?", "Exit", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
    }
}
