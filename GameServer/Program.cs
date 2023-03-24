using System;
using System.Threading;

namespace GameServer
{
    class Program
    {
        private static bool isRunning = false;

        static void Main(string[] args)
        {
            Console.Title = "Game Server";
            isRunning = true;

            Thread mainThread = new Thread(new ThreadStart(MainThread));
            mainThread.Start();

            Server.Start(50, 29950);
        }

        /// <summary>
        /// Runs the game loop.
        /// </summary>
        private static void MainThread()
        {
            Console.WriteLine("Main thread started. Running at {} ticks per second.");
        }
    }
}
