using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MyoKun.SharpCraft.Core;

namespace MyoKun.SharpCraft
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Random rand = new Random();

            int UserIndex = 1;
            string Username = "Player" + rand.Next(1000);

            for (int x = 0; x < args.Length; x++)
            {
                if (args[x] == "--username")
                {
                    UserIndex = x + 1;
                    Username = args[UserIndex];
                    break;
                }
            }

            Sharpcraft sharpcraft = new Sharpcraft(Username);

            sharpcraft.Run();
        }
    }
}
