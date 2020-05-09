using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tetris
{
    public class GameModelController
    {
        public GameModel Model { get; set; }
        
        public GameModelController(GameModel model)
        {
            Model = model;
        }

        public void KeyDown(object sender, KeyEventArgs args)
            => Model.KeyDown(args.KeyCode);
    }
}
