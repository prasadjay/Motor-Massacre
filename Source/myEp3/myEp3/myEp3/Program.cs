using System;
using System.Windows.Forms;

namespace myEp3
{

#if WINDOWS
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        /// 

        

        static void Main(string[] args)
        {
            using (Game1 game = new Game1())
            {
                game.Run();
            }

        }
    }
#endif
}

