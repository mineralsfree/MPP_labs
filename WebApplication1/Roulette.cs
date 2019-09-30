using System;
using System.Threading;
using System.Threading.Tasks;

namespace WebApplication1
{
    public class Roulette
    {
        private static Roulette _roulette;

        public delegate Task SendNumber(int num);

        public event SendNumber OnRoll;

        //private static Semaphore _semaphore;
        public void AddBet(Bet bet)
        {
        }

        public void Start()
        {
            var rand = new Random();
            while (true)
            {
                var init = rand.Next(0, 36);
                OnRoll?.Invoke(init);
                Thread.Sleep(15000);
            }
        }
    }
}