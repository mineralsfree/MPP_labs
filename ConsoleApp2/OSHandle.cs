using System;
using System.Runtime.InteropServices;

namespace ConsoleApp2
{
    public class OSHandle:IDisposable
    {       
        [DllImport("kernel32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
        private static extern bool CloseHandle(IntPtr handle);
        private IntPtr _handle { get; set; }
        
        public void Finalize()
        {
            if (_handle != IntPtr.Zero)
            {
                bool isClosed = CloseHandle(_handle);
                if (!isClosed)
                {
                    Console.WriteLine("Attempt to close handle which can't be closed'");
                    throw new Exception("This handle can't be closed'");
                }
                Console.WriteLine("Handle " + _handle.ToInt64() + " was closed");
            }
        }
        public void Dispose()
        {
            throw new NotImplementedException();
        }
      
    }
}