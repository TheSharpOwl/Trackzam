using RestSharp;
using System;

namespace TrackzamClient
{
    public class UserLogin
    {
        public bool IsLoggedIn;

        public bool RetrieveLoginStatus()
        {
            if (InfoSaver.UserIsStored())
            {
                var result = TryLogin(InfoSaver.GetUser(), InfoSaver.GetPass(), InfoSaver.GetEmail());
                IsLoggedIn = result;
                return result;
            }
            else
            {
                IsLoggedIn = false;
                return false;
            }
        }
        
        public bool TryLogin(string login, string password, string email)
        {
            try
            {

                var client = new RestClient("http://" + ConfigManager.ServerIP + ":8000/api/check_user");
                var request = new RestRequest(Method.POST);
                request.AddHeader("cache-control", "no-cache");
                string authtoken = System.Convert.ToBase64String(System.Text.Encoding.GetEncoding("ISO-8859-1").GetBytes(login + ":" + password));
                request.AddHeader("authorization", "Basic " + authtoken);
                IRestResponse response = client.Execute(request);

                if (response.Content == "{\"message\":\"Credentials are correct.\"}")
                {
                    InfoSaver.StoreUserInfo(login, password, email);
                    IsLoggedIn = true;
                    return true;
                }
                return false;
            }
            catch (Exception e)
            {
                UIManager.ShowMessage(e.Message);
                return false;
            }
        }
    }
}