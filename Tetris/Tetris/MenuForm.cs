using System.Drawing;
using System.Windows.Forms;

namespace Tetris
{
    public partial class MenuForm : Form
    {
        private Timer timer = new Timer();
        private GameModel gameModel;

        public MenuForm()
        {
            InitializeComponent();
            gameModel = new GameModel(new Size(this.ClientSize.Width, this.ClientSize.Height));
            timer.Interval = 200;
            timer.Tick += (s, args) => gameModel.Update();
            timer.Tick += (s, args) => Invalidate();
            Paint += (s, args) => args.Graphics.Clear(Color.Black);
            Paint += (s, args) => gameModel.Draw(args.Graphics);
            this.KeyDown += gameModel.KeyDown;
            timer.Start();
        } 
    }
}
