
using System;
using System.Threading;

namespace ConsoleApp1
{
    public class Mutex
    {
        private int _value;    
       public Mutex(int initialValue)
        {
            if (initialValue > 1 || initialValue < 0)
            {
                throw new ArgumentOutOfRangeException();
            }

            _value = initialValue;
        }

       public void Unlock()
       {
           while (Interlocked.CompareExchange(ref _value, 1, 0) == 1)
           {
               Thread.Sleep(10);
           } 
       }

       public void Lock()
       {
           int inval = _value;
           while (Interlocked.CompareExchange(ref _value, 0, 1) == inval)
           {
               Thread.Sleep(100);
           }
       }
    }
}