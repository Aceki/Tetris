using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tetris
{
    public partial class MyForm : Form
    {
        public List<IUpdatable> GameObjects = new List<IUpdatable>();

        private Timer timer = new Timer();

        public MyForm()
        {
            InitializeComponent();
            timer.Interval = 50;
            timer.Tick += (s, args) => Update();
            timer.Tick += (s, args) => Invalidate();
            InitializeGame();
        }

        

        public void InitializeGame()
        {
            var rnd = new Random();
            for(var i = 0; i < 30; i++)
                GameObjects.Add(Tetromino.CreateRandomFigure(new Point(rnd.Next(0, this.ClientSize.Width), rnd.Next(0, this.ClientSize.Height))));


            Paint += (s, args) => args.Graphics.Clear(Color.Black);
            Paint += (s, args) => Draw(args.Graphics);
            timer.Start();
        }


        public void Update()
        {
            for(var i = 0; i < GameObjects.Count; i++)
            {
                GameObjects[i].Update();
                if (GameObjects[i] is IDrawable && (GameObjects[i] as IDrawable).Position.Y > this.ClientSize.Height)
                {
                    GameObjects.Remove(GameObjects[i]);
                    GameObjects.Add(Tetromino.CreateRandomFigure(new Point(new Random().Next(0, this.ClientSize.Width), 0)));
                }  
            }
        }

        public void Draw(Graphics graphics)
        {
            foreach (var obj in GameObjects)
                if (obj is IDrawable)
                    (obj as IDrawable).Draw(graphics);
        }
    }
}
