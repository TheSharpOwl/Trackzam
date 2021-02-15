using System;
using System.Text;

namespace TrackzamClient
{
    public class SessionManager
    {
        public SessionManager()
        {
            _sessionName = new StringBuilder();
        }

        public void StartNewSession(ActiveWindowLoggerClass windowLogger, Keylogger keylogger, AudioRecorder audioRecorder)
        {
            _windowLogger = windowLogger;
            _audioRecorder = audioRecorder;
            _keylogger = keylogger;
            
            DateTime timeNow = DateTime.Now;
            _sessionName.Clear();
            _sessionName.Append(timeNow.Year).Append('-').Append(timeNow.Month).Append('-').Append(timeNow.Day)
                .Append('-').Append(timeNow.Hour).Append('-').Append(timeNow.Minute).Append('-')
                .Append(timeNow.Second).Append('-').Append(timeNow.Millisecond).Append('-');
            
            string myDocymentsFolder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            System.IO.Directory.CreateDirectory(myDocymentsFolder+"/Trackzam");
            _sessionFolderPath = System.IO.Directory.CreateDirectory(myDocymentsFolder+"/Trackzam/"+_sessionName).FullName;
        }

        public void EndSession()
        {
            System.Diagnostics.Process.Start("explorer.exe", _sessionFolderPath);
        }

        private StringBuilder _sessionName;
        private string _sessionFolderPath;
        private ActiveWindowLoggerClass _windowLogger;
        private Keylogger _keylogger;
        private AudioRecorder _audioRecorder;
    }
}