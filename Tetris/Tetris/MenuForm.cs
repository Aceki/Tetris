using System.Drawing;
using System.Windows.Forms;

namespace Tetris
{
    public partial class MenuForm : Form
    {
        private Timer timer = new Timer();
        private Game gameModel;

        public MenuForm()
        {
            InitializeComponent();
            gameModel = new Game(new Size(10, 20));
            gameModel.GameOver += (sender, args) => MessageBox.Show(args.Message, "Game over", MessageBoxButtons.OK);
            timer.Interval = 200;
            timer.Tick += (s, args) => gameModel.Update();
            timer.Tick += (s, args) => Invalidate();
            Paint += (s, args) => args.Graphics.Clear(Color.Black);
            Paint += (s, args) => gameModel.Draw(args.Graphics);
            KeyDown += gameModel.KeyDown;
            Load += (sender, args) => gameModel.Start();
            timer.Start();
        } 
    }
}
