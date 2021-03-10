using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Win32;
using System.Security.Cryptography;
using System.Linq;


namespace TrackzamClient
{
    sealed class UserLogin
    {
        private const string userRoot = @"HKEY_CURRENT_USER\TrachzamData\";
        // TODO store = private , store_pass / user -> public

        // TODO store the username too
        public static void StoreInfo(string keyName , string info)
        {
            string regeditDir = userRoot + "\\" + keyName;

            if (info == null)
            {
                // TODO handle the not hashing case
                Console.WriteLine("Failed to hash");
            }

            Registry.SetValue(regeditDir, keyName, info);
        }

        public static string GetInfo(string keyName)
        {
            // TODO try failedMessage = null (check docs if it is nullable)
            string failedMessage = "Failed";
            string storedPass = (string) Registry.GetValue(userRoot + "\\" + keyName, keyName, failedMessage);

            if (storedPass == failedMessage)
            {
                // TODO handle failed retrieve
                Console.WriteLine("Failed to retrieve");
                return null;
            }
            
            return storedPass;
        }

        private static byte[] Encrypt(string text)
        {
            byte[] stringBytes = Encoding.Default.GetBytes(text);
            // 1 way crypt
            System.Security.Cryptography.SHA256Managed sha = new System.Security.Cryptography.SHA256Managed();
            byte[] hashed = sha.ComputeHash(stringBytes);
            return hashed;
        }

        //TODO DELETE
        //THIS Function IS FOR TESTING THE PASSWORD STORING AND COMPARSION
        public static bool Test()
        {

            string pass = "bla_bla";
            StoreInfo("UserPass", pass);
            string ans = GetInfo("UserPass");

            return pass == ans;
        }
    }
}
