using System;
using System.Text;
using Microsoft.Win32;


namespace TrackzamClient
{
    sealed class InfoSaver
    {
        private const string USER_ROOT = @"HKEY_CURRENT_USER\SOFTWARE\Trackzam\";
        public static string AuthToken => Convert.ToBase64String(Encoding.GetEncoding("ISO-8859-1").GetBytes(GetUser() + ":" + GetPass()));
        public static void StoreUserInfo(string userName, string password, string email)
        {
            StoreInfo("username", userName);
            StoreInfo("pass", password);
            StoreInfo("email", email);
        }
        public static bool UserIsStored()
        {
            return GetInfo("username") != null && GetInfo("pass") != null;
        }
        public static string GetUser()
        {
            return GetInfo("username");
        }

        public static string GetPass()
        {
            return GetInfo("pass");
        }
        public static string GetEmail()
        {
            return GetInfo("email");
        }
        private static void StoreInfo(string keyName , string info)
        {
            string regeditDir = USER_ROOT + keyName;

            if (info == null)
            {
                UIManager.ShowMessage("Internal error");
                return;
            }

            Registry.SetValue(regeditDir, keyName, info);
        }

        private static string GetInfo(string keyName)
        {
            // TODO try failedMessage = null (check docs if it is nullable)
            string failedMessage = "Failed";
            string storedPass = (string) Registry.GetValue(USER_ROOT + "\\" + keyName, keyName, failedMessage);

            if (storedPass == failedMessage)
            {
                UIManager.ShowMessage("Registry access error");
                return null;
            }
            
            return storedPass;
        }
    }
}
