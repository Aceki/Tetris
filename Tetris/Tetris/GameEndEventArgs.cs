﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
