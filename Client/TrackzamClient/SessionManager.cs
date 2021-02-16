using System;
using System.Text;

namespace TrackzamClient
{
    public class SessionManager
    {
        public bool IsSessionInProgress;
        public SessionManager(MainWindow mainWindow)
        {
            _mainWindow = mainWindow;
            IsSessionInProgress = false;
            _windowLogger = new ActiveWindowLoggerClass();
            _audioRecorder = new AudioRecorder();
            _keylogger = new Keylogger(mainWindow);
        }
        
        public void StartNewSession()
        {
            string myDocumentsFolder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            System.IO.Directory.CreateDirectory(myDocumentsFolder+"/Trackzam");
            _sessionFolderPath = System.IO.Directory.CreateDirectory(myDocumentsFolder+"/Trackzam/"+TrackzamTimer.GetNowString()).FullName;
            
            _audioRecorder.StartRecord(_sessionFolderPath);
            _keylogger.Start(_sessionFolderPath);
            _windowLogger.StartLogging(_sessionFolderPath);
            IsSessionInProgress = true;
        }

        public void EndSession()
        {
            if (IsSessionInProgress)
            {
                _audioRecorder.StopRecording();
                _keylogger.Stop();
                _windowLogger.StopLogging();
                System.Diagnostics.Process.Start("explorer.exe", _sessionFolderPath);   
            }
        }
        
        private string _sessionFolderPath;
        private ActiveWindowLoggerClass _windowLogger;
        private Keylogger _keylogger;
        private AudioRecorder _audioRecorder;
        private MainWindow _mainWindow;
    }
}
