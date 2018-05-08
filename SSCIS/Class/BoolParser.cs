using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SSCIS.Class
{
    /// <summary>
    /// Bool values parser class
    /// </summary>
    public static class BoolParser
    {
        private static readonly string[] _trueValues = new string[] { "T", "t", "1", "True", "true" };

        /// <summary>
        /// Parses string into bool value
        /// </summary>
        /// <param name="s">String</param>
        /// <returns>Bool value</returns>
        public static bool Parse(string s)
        {
            if (s == null)
            {
                return false;
            }
            foreach (string trueVal in _trueValues)
            {
                if (trueVal.Equals(s))
                {
                    return true;
                }
            }
            return false;
        }
    }
}