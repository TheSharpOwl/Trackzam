using System;
using System.Text;
using Microsoft.Win32;


namespace TrackzamClient
{
    sealed class InfoSaver
    {
        /// <summary>
        /// the path to store info at in regedit
        /// </summary>
        private const string USER_ROOT = @"HKEY_CURRENT_USER\SOFTWARE\Trackzam\";

        public static string AuthToken => Convert.ToBase64String(Encoding.GetEncoding("ISO-8859-1").GetBytes(GetUser() + ":" + GetPass()));

        
        /// <summary>
        /// stores user info : username, password, email into regedit
        /// </summary>
        /// <param name="userName"> username string </param>
        /// <param name="password"> user password string </param>
        /// <param name="email"> email for the user </param>
        public static void StoreUserInfo(string userName, string password, string email)
        {
            StoreInfo("username", userName);
            StoreInfo("pass", password);
            StoreInfo("email", email);
        }

        /// <returns> true if there is saved user info stored in the regedit </returns>
        public static bool UserIsStored()
        {
            return GetInfo("username") != null && GetInfo("pass") != null;
        }
        
        /// <returns> stored username </returns>
        public static string GetUser()
        {
            return GetInfo("username");
        }
       

        /// <returns>  return the stored password in the regedit </returns>
        public static string GetPass()
        {
            return GetInfo("pass");
        }

        /// <returns> stored email in the regedit </returns>
        public static string GetEmail()
        {
            return GetInfo("email");
        }

        /// <summary>
        ///  stores information string in the regedit with a keyname
        /// </summary>
        /// <param name="keyName"> the keyname to use in regedit </param>
        /// <param name="info"> the info to be stored in the regedit </param>
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

        /// <summary>
        /// returns the info stored by a keyname
        /// </summary>
        /// <param name="keyName"> keyname in the regedit which holds the desired value </param>
        /// <returns> info stored in the registery with keyname </returns>
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
