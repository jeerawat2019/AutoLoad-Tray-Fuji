using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Ai_PCSystem.File
{
    public class FPath : FBase
    {
        public FPath(string sApplicationName)
        {
            this.ApplicationName = sApplicationName;          
        }

        public FPath(string sApplicationName, string sMainFolderAppName, string sSubFolderAppName) : this(sApplicationName)
        {
            this.MainFolderAppName = sMainFolderAppName;
            ///
            this.SubFolderAppName = sSubFolderAppName;
        }

        public override List<string> AllFolderApp
        {

            get
            {
                if (string.IsNullOrEmpty(this.ApplicationName)) return null;                   

                int ind = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly()
                    .Location).IndexOf(this.ApplicationName);
                ///
                string dirPath = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly()
                    .Location).Substring(0, ind);
                ///
               return new List<string>(Directory.EnumerateDirectories(dirPath));
            }
            
        }
        public  string GetPathXmlConfig
        {
            get
            {
                if (string.IsNullOrEmpty(MainFolderAppName) || string.IsNullOrEmpty(FileName)) return null;

                var PathResult = (from d in AllFolderApp
                        where d.IndexOf(MainFolderAppName) != -1
                        select d).Single();
                ///
                List<string> dirsResult = new List<string>(Directory.EnumerateDirectories(PathResult + SubFolderAppName));
                ///
                return (from d in dirsResult
                        where d.IndexOf(FileName) != -1
                        select d).Single();
            }
        }
    }
}
