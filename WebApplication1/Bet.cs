using System;
using System.Linq;

namespace WebApplication1
{
    public class Bet
    {
        private int Value { get; set; }
        private byte[] Numbers { get; set; }
        private int _potentialWin;

        public
            Bet()
        {
        }

        Bet(int Value, byte[] Numbers)
        {
            this.Value = Value;
            this.Numbers = Numbers;
        }

       public int GetWin(int num)
       {
           Console.WriteLine("length {0}",Numbers.Length);
           
           Console.WriteLine("val  {0}", Value);
           Console.WriteLine(36 / Numbers.Length);
            _potentialWin = 36 / Numbers.Length * Value;
            return Array.Exists(Numbers, e => e == num) ? _potentialWin : 0;
        }
    }
}