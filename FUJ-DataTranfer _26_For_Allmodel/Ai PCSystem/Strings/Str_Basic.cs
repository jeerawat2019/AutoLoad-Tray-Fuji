using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ai_PCSystem.Strings
{
    public class Str_Basic
    {
        //string text = "This is an example string and my data is here";
        //string data = getBetween(text, "my", "is");
        /// <summary>
        /// 
        /// </summary>
        /// <param name="strSource"></param>
        /// <param name="strStart"></param>
        /// <param name="strEnd"></param>
        /// <returns></returns>
        public static string getBetween(string strSource, string strStart, string strEnd)
        {
            const int kNotFound = -1;

            var startIdx = strSource.IndexOf(strStart);
            if (startIdx != kNotFound)
            {
                startIdx += strStart.Length;
                var endIdx = strSource.IndexOf(strEnd, startIdx);
                if (endIdx > startIdx)
                {
                    return strSource.Substring(startIdx, endIdx - startIdx);
                }
            }
            return String.Empty;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="strSource"></param>
        /// <param name="strStart"></param>
        /// <param name="strEnd"></param>
        /// <param name="strReplace"></param>
        /// <returns></returns>
        public static string ReplaceTextBetween(string strSource, string strStart, string strEnd, string strReplace)
        {
            int Start, End, strSourceEnd;
            if (strSource.Contains(strStart) && strSource.Contains(strEnd))
            {
                Start = strSource.IndexOf(strStart, 0) + strStart.Length;
                End = strSource.IndexOf(strEnd, Start);
                strSourceEnd = strSource.Length - 1;

                string strToReplace = strSource.Substring(Start, End - Start);
                string newString = string.Concat(strSource.Substring(0, Start), strReplace, strSource.Substring(Start + strToReplace.Length, strSourceEnd - Start));
                return newString;
            }
            else
            {
                return string.Empty;
            }
        }
    }
}
