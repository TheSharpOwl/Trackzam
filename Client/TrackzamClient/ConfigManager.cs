using System;
using System.IO;
using System.Text.Json;
using System.Web;
using System.Windows.Controls;
using System.Windows.Forms;

namespace TrackzamClient
{
    class ConfigManager
    {
        public static string StorageDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
        public static string ServerIP = "34.71.243.7";

        public static void RetrieveConfigData(TextBlock currentDirectoryTextBlock)
        {
            _currentDirectoryTextBlock = currentDirectoryTextBlock;
            
            try
            {
                if (!File.Exists(_configPath))
                {
                    RewriteConfigFile();
                }
                else
                {
                    using (StreamReader r = new StreamReader(_configPath))
                    {
                        string json = r.ReadToEnd();
                        using JsonDocument config = JsonDocument.Parse(json);
                        var root = config.RootElement;
                        StorageDirectory = root.GetProperty("StorageDir").ToString();
                        ServerIP = root.GetProperty("ServerIP").ToString();
                    }
                    
                }
                UIManager.UpdateTextBlockText(_currentDirectoryTextBlock, "Current dir: " + StorageDirectory);
            }
            catch (Exception e)
            {
                UIManager.ShowMessage(e.Message);
            }
        }
        public static void SetStorageDirectory()
        {
            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
            if (folderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    StorageDirectory = folderBrowserDialog.SelectedPath;
                    UIManager.UpdateTextBlockText(_currentDirectoryTextBlock, "Current dir: " + StorageDirectory);
                    RewriteConfigFile();
                }
                catch (Exception e)
                {
                    UIManager.ShowMessage(e.Message);
                }
            }
        }
        
        private static void RewriteConfigFile()
        {
            File.WriteAllText(_configPath, ConfigString);
        }

        private static string ConfigString => "{" + Environment.NewLine +
                                              "\"ServerIP\": \""+ ServerIP + "\"" + "," + Environment.NewLine +
                                              "\"StorageDir\": \"" + HttpUtility.JavaScriptStringEncode(StorageDirectory) + "\"" + Environment.NewLine +
                                              "}";
        
        private static TextBlock _currentDirectoryTextBlock;
        private static string _configPath => Path.Combine(Environment.CurrentDirectory, "Config.json");
    }
}
