﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrackzamClient
{
    class TrackzamTimer
    {
        /// <returns> Current time in "yyyy-MM-dd-HH-mm-ss-" format  </returns>
        public static string GetNowString()
        {
            DateTime timeNow = DateTime.Now;
            string ms = timeNow.Millisecond.ToString();
            return timeNow.ToString("yyyy-MM-dd-HH-mm-ss-") + ms.ToString();
        }

        /// <returns> Current time in "yyyy/MM/dd HH:mm:ss" format  </returns>
        public static string GetTimestampString()
        {
            DateTime timeNow = DateTime.Now;
            return timeNow.Day+"/"+timeNow.Month+"/"+timeNow.Year+timeNow.ToString(" hh:mm:ss");
        }
    }
}
