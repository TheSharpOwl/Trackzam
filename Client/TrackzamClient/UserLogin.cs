using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackzamClient
{
    class UserLogin
    {
        // TODO add a hash function and pass the hash to the string
        // TODO store the username too
        public void StorePass(string pass)
        {
            const string userRoot = "HKEY_CURRENT_USER";
            const string subkey = "TrackzamData";
            const string keyName = userRoot + "\\" + subkey;
            Registry.SetValue(keyName, "PassTest", "let's try a string");
        }

        public string GetPass()
        {
            string ans = (string)Registry.GetValue(keyName, "PassTest", "Failed");
            return ans;
        }
    }
}
