using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;

namespace TrackzamClient
{
    public class ActiveWindowLoggerClass
    {
        private bool IsLogging = false;
        private String directory;
        private List<String> _logs;

        /// <summary>
        /// Function to start logging active window
        /// </summary>
        /// <param name="dir"> directory where to save log file </param>
        public void StartLogging(String dir)
        {
            directory = dir;
            IsLogging = true;
            _logs = new List<String>();
        }

        /// <summary>
        /// Function to stop logging active window
        /// </summary>
        public void StopLogging()
        {
            if (!IsLogging)
            {
                return;
            }
            IsLogging = false;

            // go through all logs and write to file
            StringBuilder outputStringBuilder = new StringBuilder();            
            foreach (var item in _logs)
            {
                outputStringBuilder.Append(item);
                outputStringBuilder.Append("\r\n");
            }

            String filepath = directory + "\\activeWindow.txt";

            if(!Directory.Exists(directory)){
                Directory.CreateDirectory(directory);
            }
            File.WriteAllText(filepath, outputStringBuilder.ToString());
        }

        private void AddLogItem(String newLog) 
        {
            _logs.Add(newLog);
        }



        /// <summary>
        /// Constructor for ActiveWindowLoggerClass
        /// </summary>
        public ActiveWindowLoggerClass()
        {
            
            dele = new WinEventDelegate(WinEventProc);
            IntPtr m_hhook = SetWinEventHook(EVENT_SYSTEM_FOREGROUND, EVENT_SYSTEM_FOREGROUND, IntPtr.Zero, dele, 0, 0, WINEVENT_OUTOFCONTEXT);
        }
        WinEventDelegate dele = null;

        delegate void WinEventDelegate(IntPtr hWinEventHook, uint eventType, IntPtr hwnd, int idObject, int idChild, uint dwEventThread, uint dwmsEventTime);

        [DllImport("user32.dll")]
        static extern IntPtr SetWinEventHook(uint eventMin, uint eventMax, IntPtr hmodWinEventProc, WinEventDelegate lpfnWinEventProc, uint idProcess, uint idThread, uint dwFlags);

        private const uint WINEVENT_OUTOFCONTEXT = 0;
        private const uint EVENT_SYSTEM_FOREGROUND = 3;

        [DllImport("user32.dll")]
        static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll")]
        static extern int GetWindowText(IntPtr hWnd, StringBuilder text, int count);

        private string GetActiveWindowTitle()
        {
            const int nChars = 256;
            IntPtr handle = IntPtr.Zero;
            StringBuilder Buff = new StringBuilder(nChars);
            handle = GetForegroundWindow();

            if (GetWindowText(handle, Buff, nChars) > 0)
            {
                return Buff.ToString();
            }
            return null;
        }

        private void WinEventProc(IntPtr hWinEventHook, uint eventType, IntPtr hwnd, int idObject, int idChild, uint dwEventThread, uint dwmsEventTime)
        {
            if (IsLogging)
            {
                String newLogString = TrackzamTimer.GetTimestampString() + " " + GetActiveWindowTitle();
                AddLogItem(newLogString);

            }

            
        }

    }
}
