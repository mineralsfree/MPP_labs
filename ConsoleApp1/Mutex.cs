
using System;
using System.Threading;

namespace ConsoleApp1
{
    public class Mutex
    {
        private int value;    
       public Mutex(int intialValue)
        {
            if (intialValue > 1 || intialValue < 0)
            {
                throw new ArgumentOutOfRangeException();
            }

            this.value = intialValue;
        }

       public void Unlock()
       {
           while (Interlocked.CompareExchange(ref this.value, 0, 1) == 0)
           {
               Thread.Sleep(10);
           } 
       }

       public void Lock()
       {
           while (Interlocked.CompareExchange(ref this.value, 1, 0) == 1)
           {
               Thread.Sleep(10);
           }
       }
    }
}