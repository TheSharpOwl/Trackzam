using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;


namespace TrackzamClient
{
    public class Keylogger
    {  
        [DllImport("user32.dll")]
        public static extern int GetAsyncKeyState(Int32 i);

        protected string logDir = "";
        private StreamWriter writer;
        private DispatcherTimer timer;

        public Keylogger(Window window)
        {
            Keyboard.AddKeyDownHandler(window, OnKeyDown);
            isRecording = false;
        }

        // for clients read-only
        public string Path
        {
            get => logDir;
        }

        public void Start(string path)
        {
            SetPath(path);
            string fileName = "\\" + TrackzamTimer.GetNowString();
            writer = new StreamWriter(logDir + fileName);
            isRecording = true;
        }

        public void Stop()
        {
            writer.WriteLine("//Closed the Window//");
            writer.Close();
        }
        
        private void OnKeyDown(object sender, KeyEventArgs keyEventArgs)
        {
            if (isRecording)
            {
                writer.WriteLine("< {0} {1} >", keyEventArgs.Key.ToString(), TrackzamTimer.GetNowString());
            }
        }
        
        // returns true if the path is set successfuly without errors
        private bool SetPath(string path)
        {
            if (Directory.Exists(path))
            {
                logDir = path;
                return true;
            }

            try
            {
                DirectoryInfo di = Directory.CreateDirectory(path);
                Console.WriteLine("Created the new dir!");
                logDir = path;
                return true;
            }
            catch (UnauthorizedAccessException) { Console.WriteLine("Access Denied"); return false; }
            catch (PathTooLongException) { Console.WriteLine("The specified path, file name, or both exceed the system-defined maximum length."); return false; }
            catch (ArgumentException) { Console.WriteLine("Invalid Path"); return false; }
            catch (DirectoryNotFoundException) { Console.WriteLine("Invalid Path"); return false; }
            catch (Exception e)
            {
                Console.WriteLine("Error!");
                return false;
            }

        }

        private bool isRecording;
    }
}
