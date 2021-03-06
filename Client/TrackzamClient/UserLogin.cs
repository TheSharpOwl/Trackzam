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

        // TODO store the username too
        public static void StorePass(byte[] hashedPass, string keyName = "UserPass")
        {
            string fullKeyName = userRoot + "\\" + keyName;

            if (hashedPass == null)
            {
                // TODO handle the not hashing case
                Console.WriteLine("Failed to hash");
            }

            Registry.SetValue(fullKeyName, keyName, hashedPass);
        }

        // TODO make it return the hash not a string
        public static byte[] GetStoredPass(string keyName)
        {
            byte[] failBytes = Encoding.Default.GetBytes("Failed");
            byte[] hashedPass = (byte[])Registry.GetValue(userRoot + "\\" + keyName, keyName, failBytes);

            string message = Encoding.Default.GetString(hashedPass);
            if (message == "Failed")
            {
                // TODO handle failed retrieve
                Console.WriteLine("Failed to retrieve");
            }

            return hashedPass;
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
            byte[] encPass = UserLogin.Encrypt(pass);
            UserLogin.StorePass(encPass);
            byte[] TheStoredStuff = UserLogin.GetStoredPass("UserPass");
            byte[] anotherEnc = UserLogin.Encrypt(pass);

            return anotherEnc.SequenceEqual(encPass);
        }
    }
}
