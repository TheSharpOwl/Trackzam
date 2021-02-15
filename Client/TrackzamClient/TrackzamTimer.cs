using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackzamClient
{
    class TrackzamTimer
    {
        public static string GetNowString()
        {
            DateTime timeNow = DateTime.Now;
            string ms = timeNow.Millisecond.ToString();
            // the name of the file should be in this format
            return timeNow.ToString("yyyy-MM-dd-HH-mm-ss-") + ms.ToString();
        }
    }
}
