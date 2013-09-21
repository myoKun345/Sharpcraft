using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Practices.EnterpriseLibrary.Logging;

namespace MyoKun.SharpCraft.Core
{
    public class Sharpcraft
    {
        public string Username;
        public LogWriter Writer = new LogWriterFactory().Create();
        private Sharpcraft Instance;

        public Boolean Running;

        public Sharpcraft(string Username)
        {
            Instance = this;
            this.Username = Username;
            Writer.Write("Username: " + Username, "INFO");
            Console.WriteLine(Username);
        }

        public void Run()
        {
            this.Running = true;

            try
            {
                //this.StartGame();
            }
            catch (Exception e)
            {
                Writer.Write(e.ToString(), "SEVERE");
            }

            try
            {
                while (this.Running)
                {
                    if (this.Running)
                    {
                        try
                        {
                            //this.RunGameLoop;
                        }
                        catch (OutOfMemoryException e)
                        {
                            Writer.Write(e.ToString(), "SEVERE");
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
