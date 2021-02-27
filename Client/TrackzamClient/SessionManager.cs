using System;
using System.Text;
using System.Threading;

namespace TrackzamClient
{
    public class SessionManager
    {
        public bool IsSessionInProgress;
        public SessionManager()
        {
            IsSessionInProgress = false;
            _windowLogger = new ActiveWindowLoggerClass();
            _audioRecorder = new AudioRecorder();
            _keylogger = new Keylogger();
            _mouselogger = new Mouselogger();
            _videoRecorder = new VideoRecorder();
        }
        
        public void StartNewSession()
        {
            string myDocumentsFolder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            System.IO.Directory.CreateDirectory(myDocumentsFolder+"/Trackzam");
            _sessionFolderPath = System.IO.Directory.CreateDirectory(myDocumentsFolder+"/Trackzam/"+TrackzamTimer.GetNowString()).FullName;
            
            _audioRecorder.StartRecord(_sessionFolderPath);
            _keylogger.Start(_sessionFolderPath);
            _mouselogger.Start(_sessionFolderPath);
            _windowLogger.StartLogging(_sessionFolderPath);
            _videoRecorder.StartRecording(_sessionFolderPath);
            IsSessionInProgress = true;
        }

        public void EndSession()
        {
            if (!IsSessionInProgress) return;
            
            IsSessionInProgress = false;
            
            _audioRecorder.StopRecording();
            _keylogger.Stop();
            _mouselogger.Stop();
            _windowLogger.StopLogging();
            _videoRecorder.StopRecording();
            
            System.Diagnostics.Process.Start("explorer.exe", _sessionFolderPath);
            
            DataSender.SetIPAdress("34.71.243.7");
            DataSender.SendAudioLogs(_sessionFolderPath+"/audioVolume.txt");
            DataSender.SendKeyboardLogs(_sessionFolderPath+"/keyboard.txt");
            DataSender.SendMouseLogs(_sessionFolderPath+"/mouse.txt");
            DataSender.SendWindowLogs(_sessionFolderPath+"/activeWindow.txt");
            
            Console.WriteLine("Sent");
        }
        
        private string _sessionFolderPath;
        private ActiveWindowLoggerClass _windowLogger;
        private Keylogger _keylogger;
        private Mouselogger _mouselogger;
        private AudioRecorder _audioRecorder;
        private VideoRecorder _videoRecorder;
    }
}
