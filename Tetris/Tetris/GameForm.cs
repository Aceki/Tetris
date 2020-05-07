using System.Drawing;
using System.Media;
using System.Windows.Forms;
using System.Media;

namespace Tetris
{
    public partial class GameForm : Form
    {
        public GameModel Model { get; private set; }
        public GameModelDrawer Drawer { get; private set; }
        public GameModelSounds Sounds { get; private set; }
        public SoundPlayer MusicPlayer { get; private set; }

        public GameForm()
        {
            InitializeComponent();
            MusicPlayer = new SoundPlayer(@"ambient.wav");
            Model = new GameModel(new Size(10, 20));
            Drawer = new GameModelDrawer(Model);
            Sounds = new GameModelSounds(Model);
            Sounds.Connect();
            var updateTimer = new Timer();
            var graphicTimer = new Timer();
            Model.GameOver += (sender, args) => MessageBox.Show(args.Message, "Game over", MessageBoxButtons.OK);
            Model.GameOver += (sender, args) => Model.StartGame();
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
            //Model.FloorRemoved += (sender, args) =>
            //{
            //    var p = new SoundPlayer(@"C:\Users\aceki\Desktop\brick.wav");
            //    p.PlaySync();
            //};
            this.FormClosed += (sender, args) =>
            {
                updateTimer.Dispose();
                graphicTimer.Dispose();
                MusicPlayer.Stop();
            };
            updateTimer.Interval = 300;
            graphicTimer.Interval = 1;
            updateTimer.Tick += (s, args) => Model.Update();
            graphicTimer.Tick += (s, args) => Invalidate();
            Paint += Drawer.Draw;
            KeyDown += Model.KeyDown;
            Load += (sender, args) => Model.StartGame();
            updateTimer.Start();
            graphicTimer.Start();
        }

        public DialogResult ShowExitMessage()
            => MessageBox.Show("Вы действительно хотите закончить игру?", "Exit", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
    }
}
