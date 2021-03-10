using System;
using System.Diagnostics;
using System.IO;
using System.Text.Json;
using System.Windows.Controls;
using System.Windows.Forms;

namespace TrackzamClient
{
    class StorageDirectoryManager
    {
        public StorageDirectoryManager(TextBlock currentDirectoryTextBlock, System.Windows.Controls.Button changeDirectoryButton)
        {
            _currentDirectoryTextBlock = currentDirectoryTextBlock;
            _changeDirectoryButton = changeDirectoryButton;
            _directory = SessionManager.storageFolder;
            try
            {
                if (!File.Exists(SessionManager.pathToConfig))
                {

                    File.WriteAllText(SessionManager.pathToConfig, SessionManager.GetConfigString());
                }
                else
                {
                    // load config
                    using (StreamReader r = new StreamReader(SessionManager.pathToConfig))
                    {
                        string json = r.ReadToEnd();
                        using JsonDocument config = JsonDocument.Parse(json);
                        var root = config.RootElement;
                        _directory = root.GetProperty("StorageDir").ToString();
                        SessionManager.storageFolder = _directory;
                        UIManager.UpdateTextBlockText(_currentDirectoryTextBlock, "Current dir: " + _directory);
                        File.WriteAllText(SessionManager.pathToConfig, SessionManager.GetConfigString());
                    }
                }
            }
            catch (JsonException e)
            {
                File.WriteAllText(SessionManager.pathToConfig, SessionManager.GetConfigString());

                UIManager.UpdateTextBlockText(_currentDirectoryTextBlock, "Current dir: " + _directory);
                Debug.WriteLine(e.ToString());
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
            
        }
        private static string _directory;
        public string GetDirectory()
        {
            return _directory;
        }

        private static TextBlock _currentDirectoryTextBlock;
        private System.Windows.Controls.Button _changeDirectoryButton;
        public static void ChangeFolder()
        {
            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
            if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    _directory = folderBrowserDialog.SelectedPath;
                    SessionManager.storageFolder = _directory;
                    UIManager.UpdateTextBlockText(_currentDirectoryTextBlock, "Current dir: " + _directory);
                    File.WriteAllText(SessionManager.pathToConfig, SessionManager.GetConfigString());
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
            }
        }


    }
}
