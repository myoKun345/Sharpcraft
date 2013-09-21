using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using OpenTK;
using OpenTK.Graphics;
using OpenTK.Input;

namespace MyoKun.SharpCraft.Core
{
    public class SharpcraftWindow : GameWindow
    {
        private Sharpcraft Game;

        public SharpcraftWindow(int width, int height, string title, Sharpcraft game)
            : base(width, height, new GraphicsMode(32, 0, 0, 4), title)
        {
            this.Game = game;
            Keyboard.KeyDown += handleKey;
        }

        private void handleKey(object sender, OpenTK.Input.KeyboardKeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                Game.StopGame(0x00);
            }
        }
    }
}
