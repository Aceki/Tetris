using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;

namespace Tetris
{
    public class GameModelSounds
    {
        public readonly SoundPlayer Player;

        public GameModel Model { get; set; }

        private const string ambientLocation = @"ambient.wav";
        private const string gameoverLocation = @"gameover.wav";

        public GameModelSounds(GameModel model)
        {
            Model = model;
            Player = new SoundPlayer();
        }

        /// <summary>
        /// Выполняет привязку звуков к событиям.
        /// </summary>
        public void Connect()
        {
            Model.Start += (sender, args) =>
            {
                Player.SoundLocation = ambientLocation;
                Player.PlayLooping();
            };
            Model.GameOver += (sender, args) =>
            {
                Player.SoundLocation = gameoverLocation;
                Player.Play();
            };
        }
    }
}
