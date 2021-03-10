using System;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.IO;
using System.Web;

namespace TrackzamClient
{
    public class SessionManager
    {
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
        
        public void StartNewSession()
        {
            string myDocumentsFolder = storageFolder;
            System.IO.Directory.CreateDirectory(myDocumentsFolder+"/Trackzam");
            _sessionFolderPath = System.IO.Directory.CreateDirectory(myDocumentsFolder+"/Trackzam/"+TrackzamTimer.GetNowString()).FullName;
            
            _audioRecorder.StartRecord(_sessionFolderPath);
            _keylogger.Start(_sessionFolderPath);
            _mouseLogger.Start(_sessionFolderPath);
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
            _mouseLogger.Stop();
            _windowLogger.StopLogging();
            _videoRecorder.StopRecording();
            
            System.Diagnostics.Process.Start("explorer.exe", _sessionFolderPath);

            //getting config data

            try
            {
                // if file doesn't exist then create default
                if (!File.Exists(pathToConfig))
                {
                    File.WriteAllText(pathToConfig, GetConfigString());
                }
                else
                {
                    // load config
                    using (StreamReader r = new StreamReader(pathToConfig))
                    {
                        string json = r.ReadToEnd();
                        using JsonDocument config = JsonDocument.Parse(json);
                        var root = config.RootElement;
                        _serverIP = root.GetProperty("ServerIP").ToString();
                    }
                }
            }
            catch (JsonException e)
            {
                //TODO "Json format exception"
            }
            catch (FileLoadException e)
            {
                //TODO "Can't read file"
            }
            catch (Exception e)
            {
                //TODO "Exception"
            }

            DataSender.SetIPAdress(_serverIP);
            DataSender.SendAudioLogs(_sessionFolderPath+"/audioVolume.txt");
            DataSender.SendKeyboardLogs(_sessionFolderPath+"/keyboard.txt");
            DataSender.SendMouseLogs(_sessionFolderPath+"/mouse.txt");
            DataSender.SendWindowLogs(_sessionFolderPath+"/activeWindow.txt");
            
            Console.WriteLine("Sent");
        }

        public static string GetConfigString()
        {
            return "{" + Environment.NewLine +
                        "\"ServerIP\": \""+ _serverIP + "\"" + "," + Environment.NewLine +
                        "\"StorageDir\": \"" + HttpUtility.JavaScriptStringEncode(storageFolder) + "\"" + Environment.NewLine +
                        "}";
        }

        public static string pathToConfig = Path.Combine(Environment.CurrentDirectory, "Config.json");
        public static string storageFolder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        private string _sessionFolderPath;
        private static string _serverIP = "34.71.243.7";
        private readonly ActiveWindowLoggerClass _windowLogger;
        private readonly Keylogger _keylogger;
        private readonly Mouselogger _mouseLogger;
        private readonly AudioRecorder _audioRecorder;
        private readonly VideoRecorder _videoRecorder;
        
    }
}
