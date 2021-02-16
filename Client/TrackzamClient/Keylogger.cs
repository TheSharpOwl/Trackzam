using System;
using System.IO;
using System.Windows;
using System.Windows.Input;


namespace TrackzamClient
{
    public class Keylogger
    {
        public Keylogger(Window window)
        {
            Keyboard.AddKeyDownHandler(window, OnKeyDown);
            _isRecording = false;
        }

        // for clients read-only
        public string Path => _logDir;

        public void Start(string path)
        {
            SetPath(path);
            string fileName = "\\KeyLog.txt";
            _writer = new StreamWriter(_logDir + fileName);
            _isRecording = true;
        }

        public void Stop()
        {
            _writer.Close();
            _isRecording = false;
        }
        
        private void OnKeyDown(object sender, KeyEventArgs keyEventArgs)
        {
            if (_isRecording)
            {
                _writer.WriteLine("< {0} {1} >", keyEventArgs.Key.ToString(), TrackzamTimer.GetNowString());
            }
        }
        
        // returns true if the path is set successfuly without errors
        private bool SetPath(string path)
        {
            if (Directory.Exists(path))
            {
                _logDir = path;
                return true;
            }

            try
            {
                DirectoryInfo di = Directory.CreateDirectory(path);
                Console.WriteLine("Created the new dir!");
                _logDir = path;
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
        
        private string _logDir = "";
        private StreamWriter _writer;
        private bool _isRecording;
    }
}
