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
            Console.WriteLine($"Main thread started. Running at {Constants.TICKS_PER_SEC} ticks per second.");
            DateTime _nextTick = DateTime.Now;

            while (isRunning)
            {
                while (_nextTick < DateTime.Now)
                {
                    GameLogic.Update();

                    _nextTick = _nextTick.AddMilliseconds(Constants.MS_PER_TICK);

                    if (_nextTick > DateTime.Now)
                    {
                        Thread.Sleep(_nextTick - DateTime.Now);
                    }
                }
            }
        }
    }
}
