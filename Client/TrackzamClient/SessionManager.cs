using System;
using System.IO;

namespace TrackzamClient
{
    public class SessionManager
    {
        //Indicates whether some session is in progress or not 
        public bool IsSessionInProgress;
        public SessionManager()
        {
            IsSessionInProgress = false;
            _windowLogger = new ActiveWindowLoggerClass();
            _audioRecorder = new AudioRecorder(8000, 16, 1000);
            _keylogger = new Keylogger();
            _mouseLogger = new Mouselogger();
            _videoRecorder = new VideoRecorder(1,4);
        }

        /// <summary>
        ///  Starts new recording session:
        ///  Creates a dedicated directory for a new session
        ///  Starts all recording modules
        ///  Shows an error windows in case of exception with the message of that exception
        /// </summary>
        public void StartNewSession()
        {
            try
            {
                if (!Directory.Exists(ConfigManager.StorageDirectory + "/Trackzam"))
                    Directory.CreateDirectory(ConfigManager.StorageDirectory + "/Trackzam");
                _sessionFolderPath = Directory.CreateDirectory(ConfigManager.StorageDirectory + "/Trackzam/" + TrackzamTimer.GetNowString()).FullName;

                _audioRecorder.StartRecord(_sessionFolderPath);
                _keylogger.Start(_sessionFolderPath);
                _mouseLogger.Start(_sessionFolderPath);
                _windowLogger.StartLogging(_sessionFolderPath);
                _videoRecorder.StartRecording(_sessionFolderPath);
                IsSessionInProgress = true;
                _startTime = TrackzamTimer.GetTimestampString();
            }
            catch (Exception e)
            {
                UIManager.ShowMessage(e.Message);
            }
        }

        /// <summary>
        /// Ends recording session,
        /// stops all recording modules,
        /// tells DataSender to start sending files
        /// </summary>
        public void EndSession()
        {
            if (!IsSessionInProgress) return;

            IsSessionInProgress = false;
            try
            {

                _audioRecorder.StopRecording();
                _keylogger.Stop();
                _mouseLogger.Stop();
                _windowLogger.StopLogging();
                _videoRecorder.StopRecording();

                System.Diagnostics.Process.Start("explorer.exe", _sessionFolderPath);

                DataSender.SetIPAdress(ConfigManager.ServerIP);
                DataSender.SendVideoFile(_sessionFolderPath + "/videoCapture.mp4", _startTime);
                DataSender.SendKeyboardLogs(_sessionFolderPath + "/keyboard.txt");
                DataSender.SendMouseLogs(_sessionFolderPath + "/mouse.txt");
                DataSender.SendWindowLogs(_sessionFolderPath + "/activeWindow.txt");
                DataSender.SendAudioLogs(_sessionFolderPath + "/audioVolume.txt");
                DataSender.SendAudioFile(_sessionFolderPath + "/microphone.wav");
            }
            catch (Exception e)
            {
                UIManager.ShowMessage(e.Message);
            }
            
            Console.WriteLine("Sent");
        }

        private string _sessionFolderPath;
        private readonly ActiveWindowLoggerClass _windowLogger;
        private readonly Keylogger _keylogger;
        private readonly Mouselogger _mouseLogger;
        private readonly AudioRecorder _audioRecorder;
        private readonly VideoRecorder _videoRecorder;
        private string _startTime;
    }
}
