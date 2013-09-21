using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Practices.EnterpriseLibrary.Logging;

using OpenTK;
using OpenTK.Graphics;
using OpenTK.Input;

namespace MyoKun.SharpCraft.Core
{
    public class Sharpcraft
    {
        public string Username;
        public LogWriter Writer = new LogWriterFactory().Create();
        private Sharpcraft Instance;

        public SharpcraftWindow Window;

        public Boolean Running;

        public Sharpcraft(string Username)
        {
            Instance = this;
            this.Username = Username;
            Writer.Write("Username: " + Username, "INFO");
            Console.WriteLine(Username);
        }

        private void StartGame()
        {
            Window = new SharpcraftWindow(800, 600, "Sharpcraft", Instance);
        }

        private void RunGameLoop()
        {

        }

        protected void handleKey(object sender, KeyboardKeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                this.StopGame(0x00);
            }
        }

        public void StopGame(int ExitCode)
        {
            Window.Exit();
            Writer.Write("Sharpcraft exited with code " + ExitCode, ExitCode != 0x00 ? "SEVERE" : "FINE");
            Environment.Exit(ExitCode);
            System.GC.Collect();
        }

        public void Run()
        {
            this.Running = true;

            try
            {
                this.StartGame();
            }
            catch (Exception e)
            {
                Writer.Write(e.ToString(), "SEVERE");
                this.StopGame(0x01);
            }

            try
            {
                while (this.Running)
                {
                    if (this.Running)
                    {
                        try
                        {
                            //this.RunGameLoop();
                        }
                        catch (OutOfMemoryException e)
                        {
                            Writer.Write(e.ToString(), "SEVERE");
                            this.StopGame(0x02);
                        }

                        continue;
                    }
                }
            }
            catch (Exception e)
            {
                Writer.Write(e.ToString(), "SEVERE");
            }
        }
    }
}
