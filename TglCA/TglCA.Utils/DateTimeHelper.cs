﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TglCA.Utils
{
    public static class DateTimeHelper
    {
        /// <summary>
        /// Converts a given DateTime into a Unix timestamp
        /// </summary>
        /// <param name="value">Any DateTime</param>
        /// <returns>The given DateTime in Unix timestamp format</returns>
        public static long ToUnixTimestamp(this DateTime value)
        {
            return (long)Math.Truncate(value.ToUniversalTime().Subtract(new DateTime(1970, 1, 1)).TotalSeconds);
        }

        /// <summary>
        /// Gets a Unix timestamp representing the current moment
        /// </summary>
        /// <param name="ignored">Parameter ignored</param>
        /// <returns>Now expressed as a Unix timestamp</returns>
        public static long UnixTimestampNow()
        {
            return (long)Math.Truncate(DateTime.UtcNow.Subtract(new DateTime(1970, 1, 1)).TotalSeconds);
        }
    }
}
