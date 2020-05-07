using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Ai_PCSystem.File
{
    public class FDirs 
    {
        private static FPath mPath = new FPath("AF-Monitoring", "Ai CRLine", "\\DataBase\\Xml Data");       
        /// <summary>
        /// 
        /// </summary>
        /// <param name="station_config_name"></param>
        /// <returns></returns>
        public static string GetPathConfigDataResultXML(string station_config_name)
        {
            try
            {
                mPath.FileName = station_config_name;
                ///
                return mPath.GetPathXmlConfig;
            }
            catch (UnauthorizedAccessException UAEx)
            {
                Console.WriteLine(UAEx.Message); return null;
            }
            catch (PathTooLongException PathEx)
            {
                Console.WriteLine(PathEx.Message);return null;
            }
        }     
    }
}