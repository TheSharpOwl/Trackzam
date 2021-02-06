using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Collections;

namespace TrackzamClient
{
    class Keylogger
    {
        protected string logDir = "";
        public void setPath(string path)
        {
            //System.IO.FileInfo fi = null;
            //try
            //{
            //    fi = new System.IO.FileInfo(dir);
            //}
            //catch (ArgumentException) { Console.WriteLine("ArgumentException"); }
            //catch (System.IO.PathTooLongException) { Console.WriteLine("PathTooLongException"); }
            //catch (NotSupportedException) { Console.WriteLine("NotSupportedException"); }
            //if (ReferenceEquals(fi, null))
            //{
            //    // file name is not valid
            //    Console.WriteLine("not a valid file name");
            //}
            //else
            //{
            //    Console.WriteLine("valid file name");
            //    // file name is valid... May check for existence by calling fi.Exists.
            //}

            if(Directory.Exists(path))
            {
                Console.WriteLine("Valid path");
            }
            else
            {
                Console.WriteLine("Doesn't exist");
            }
        }
    }
}
