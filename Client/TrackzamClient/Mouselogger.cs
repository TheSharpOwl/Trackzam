using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace TrackzamClient
{
    class Mouselogger
    {
        public Mouselogger()
        {
            _proc = HookCallback;
        }

        public void Start(string path)
        {
            SetPath(path);
            _writer = new StreamWriter(_logDir, true);
            _isRecording = true;
            lastMousePos.x = lastMousePos.y = -1;
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

        public bool SetPath(string path)
        {
            if (Directory.Exists(path))
            {
                _logDir = path + "\\Mouselog.txt";
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


        private delegate IntPtr LowLevelMouseProc(int nCode, IntPtr wParam, IntPtr lParam);

        private IntPtr SetHook(LowLevelMouseProc proc)
        {
            using (Process curProcess = Process.GetCurrentProcess())
            using (ProcessModule curModule = curProcess.MainModule)
            {
                return SetWindowsHookEx(WH_MOUSE_LL, proc,
                    GetModuleHandle(curModule.ModuleName), 0);
            }
        }

        private IntPtr HookCallback(int nCode, IntPtr wParam, IntPtr lParam)
        {
            if (nCode >= 0)
            {
                MSLLHOOKSTRUCT hookStruct = (MSLLHOOKSTRUCT)Marshal.PtrToStructure(lParam, typeof(MSLLHOOKSTRUCT));
                string typeMsg = "";
                string timeMsg = TrackzamTimer.GetNowClockString();

                switch ((MouseMessages)wParam)
                {
                    case MouseMessages.WM_LBUTTONDOWN: // left mouse click is down
                        typeMsg = "Left Click";
                        break;
                    case MouseMessages.WM_RBUTTONDOWN: // right mouse click is down
                        typeMsg = "Right Click";
                        break;
                    case MouseMessages.WM_MOUSEWHEEL: // mouse wheel is being used
                        typeMsg = "Wheel";
                        break;
                    case MouseMessages.WM_MOUSEMOVE: // mouse is moving
                        typeMsg = "Moving";
                        break;
                }
                if(typeMsg == "Moving")
                {
                    if (lastMousePos.x < 0)
                        lastMousePos = hookStruct.pt;

                    if (dist(lastMousePos, hookStruct.pt) < throttle * throttle)
                    {
                        return CallNextHookEx(_hookID, nCode, wParam, lParam);
                    }
                    else
                    {
                        lastMousePos = hookStruct.pt;
                    }

                }
                if(!String.IsNullOrEmpty(typeMsg))
                    _writer.WriteLine("{0} {1},{2} {3}", typeMsg, hookStruct.pt.x, hookStruct.pt.y, timeMsg);
            }

            return CallNextHookEx(_hookID, nCode, wParam, lParam);
        }

        // dll Imporrts
        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr SetWindowsHookEx(int idHook,
        LowLevelMouseProc lpfn, IntPtr hMod, uint dwThreadId);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool UnhookWindowsHookEx(IntPtr hhk);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr CallNextHookEx(IntPtr hhk, int nCode,
            IntPtr wParam, IntPtr lParam);

        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr GetModuleHandle(string lpModuleName);



        // class members
        private enum MouseMessages
        {
            WM_LBUTTONDOWN = 0x0201,
            WM_LBUTTONUP = 0x0202,
            WM_MOUSEMOVE = 0x0200,
            WM_MOUSEWHEEL = 0x020A,
            WM_RBUTTONDOWN = 0x0204,
            WM_RBUTTONUP = 0x0205
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct POINT
        {
            public int x;
            public int y;
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct MSLLHOOKSTRUCT
        {
            public POINT pt;
            public uint mouseData;
            public uint flags;
            public uint time;
            public IntPtr dwExtraInfo;
        }

        private int dist(POINT first, POINT second)
        {
            return (first.x - second.x) * (first.x - second.x) + (first.y - second.y) * (first.y - second.y);
        }

        protected string _logDir = "";
        private StreamWriter _writer;
        private LowLevelMouseProc _proc;
        private IntPtr _hookID = IntPtr.Zero;
        private const int WH_MOUSE_LL = 14;
        private bool _isRecording = false;
        private POINT lastMousePos;
        private const int throttle = 1000;

    }
}
