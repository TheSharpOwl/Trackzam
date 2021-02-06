using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Collections;

namespace TrackzamClient
{
    // TODO replace Console.Write messages later when GUI works
    class Keylogger
    {
        protected string logDir = "";
        public void setPath(string path)
        {
            if (Directory.Exists(path))
            {
                logDir = path;
                return;
            }

            try
            {
                DirectoryInfo di = Directory.CreateDirectory(path);
                Console.WriteLine("Created the new dir!");
                logDir = path;
            }
            catch(UnauthorizedAccessException) { Console.WriteLine("Access Denied"); }
            catch(PathTooLongException) { Console.WriteLine("The specified path, file name, or both exceed the system-defined maximum length."); }
            catch(ArgumentException) { Console.WriteLine("Invalid Path"); }
            catch (DirectoryNotFoundException) { Console.WriteLine("Invalid Path"); }
            catch(Exception e)
            {
                Console.WriteLine("Error!");
            }
        }
        public string getPath()
        {
            return logDir;
        }
    }
}
