using System;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.IO;

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
            _mouseLogger = new Mouselogger();
            _videoRecorder = new VideoRecorder(1);
        }
        
        public void StartNewSession()
        {
            string myDocumentsFolder = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
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
            string IPAdress = "34.71.243.7";
            string PathToConfig = Path.Combine(Environment.CurrentDirectory, "Config.json");


            try
            {
                // if file doesn't exist then create default
                if (!File.Exists(PathToConfig))
                {
                    string defaultContent = "{" + Environment.NewLine +
                        "ServerIP: \"34.71.243.7\"" + Environment.NewLine +
                        "}";
                    File.WriteAllText(PathToConfig, defaultContent);
                }
                else
                {
                    // load config
                    using (StreamReader r = new StreamReader(PathToConfig))
                    {
                        string json = r.ReadToEnd();
                        using JsonDocument config = JsonDocument.Parse(json);
                        var root = config.RootElement;
                        IPAdress = root.GetProperty("ServerIP").ToString();
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

            DataSender.SetIPAdress(IPAdress);
            DataSender.SendAudioLogs(_sessionFolderPath+"/audioVolume.txt");
            DataSender.SendKeyboardLogs(_sessionFolderPath+"/keyboard.txt");
            DataSender.SendMouseLogs(_sessionFolderPath+"/mouse.txt");
            DataSender.SendWindowLogs(_sessionFolderPath+"/activeWindow.txt");
            
            Console.WriteLine("Sent");
        }
        
        private string _sessionFolderPath;
        private readonly ActiveWindowLoggerClass _windowLogger;
        private readonly Keylogger _keylogger;
        private readonly Mouselogger _mouseLogger;
        private readonly AudioRecorder _audioRecorder;
        private readonly VideoRecorder _videoRecorder;
    }
}
