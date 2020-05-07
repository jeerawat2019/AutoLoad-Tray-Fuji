using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Ai_Product.Ingredient;
using Ai_PCSystem;
using System.Text.RegularExpressions;
using System.Net;

namespace AF_Monitoring.Logging_Data
{
    public class Logs_Manager
    {
        public enum Header { Write, NotWrite };
        /// <summary>
        /// 
        /// </summary>
        public string OutputResultPath { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string InputResultPath { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string _currentFile { get; private set; }
        /// <summary>
        /// 
        /// </summary>
        public string FileName { get; private set; }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public Logs_Manager() { }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="fileName"></param>
        public Logs_Manager(string fileName)
        {
            this.FileName = fileName;
        }
        /// <summary>
        /// 
        /// </summary>
        //private List<string> _header = new List<string>();
        public bool? IsDirFile(string path)
        {
            bool? result = null;

            if (Directory.Exists(path) || File.Exists(path))
            {
                // get the file attributes for file or directory
                var fileAttr = File.GetAttributes(path);

                if (fileAttr.HasFlag(FileAttributes.Directory))
                    result = true;
                else
                    result = false;
            }

            return result;
        }
       
        /// <summary>
        /// 
        /// </summary>
        public void CreateResultFile(int total, string str2DCode = null)
        {
            lock (this) {
                //IsDirFile(fullPath);
                if (str2DCode == null)
                    this._currentFile = string.Format("{0}-{1}.csv", "ASY-Line01", DateTime.Now.ToString("ddMMyyyy"));//ddMMyyyy_HHmmss
                else
                    this._currentFile = string.Format("{0}_{1}_{2}.csv", str2DCode, total.ToString(), DateTime.Now.ToString("yyyyMMddHHmmss"));//ddMMyyyy_HHmmss

                if (this.OutputResultPath != "" && this.OutputResultPath != null) {
                    if (!Directory.Exists(this.OutputResultPath)) {
                        Directory.CreateDirectory(this.OutputResultPath);
                    }
                    //if (!File.Exists(string.Format("{0}", this.ResultPath)))
                    var fullPath = string.Format(@"{0}\\{1}", this.OutputResultPath, this._currentFile);
                    ///
                    if (!File.Exists(string.Format("{0}", fullPath))) {
                        ///
                        File.Create(fullPath).Dispose();
                    }
                }

                return; 
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public void CreateResultFile(string str2DCode = null)
        {
            lock (this) {
                //IsDirFile(fullPath);
                if (str2DCode == null)
                    this._currentFile = string.Format("{0}-{1}.csv", "ASY-Line01", DateTime.Now.ToString("ddMMyyyy"));//ddMMyyyy_HHmmss
                else
                    this._currentFile = string.Format("{0}-{1}.csv", str2DCode, DateTime.Now.ToString("ddMMyyyy_HHmmss"));//ddMMyyyy_HHmmss

                if (this.OutputResultPath != "" && this.OutputResultPath != null) {
                    if (!Directory.Exists(this.OutputResultPath)) {
                        Directory.CreateDirectory(this.OutputResultPath);
                    }
                    //if (!File.Exists(string.Format("{0}", this.ResultPath)))
                    var fullPath = string.Format(@"{0}\\{1}", this.OutputResultPath, this._currentFile);
                    ///
                    if (!File.Exists(string.Format("{0}", fullPath))) {
                        ///
                        File.Create(fullPath).Dispose();
                    }
                }

                return; 
            }
        }
        /// 
        /// </summary>
        public void AppendDataHeader()
        {
            lock (this) {
                // Dump data to current file
                var filePath = string.Format("{0}\\{1}", this.OutputResultPath, this._currentFile);
                //
                if (File.Exists(filePath)) {
                    if (File.ReadAllLines(filePath).Count() <= 0) {
                        var csv = new StringBuilder();
                        ///
                        //Assembly.CRLine21.Commond.PartSupply.StuctureStation.StationResult.ToList().ForEach(x => csv.Append(string.Format("{0},", x.Key)));
                        /////
                        //Assembly.CRLine21.Commond.CR_Line21.ToList().ForEach(x =>
                        //{
                        //    x.StuctureStation.StationResult.ToList().ForEach(K => csv.Append(string.Format("{0},", K.Key)));
                        //});
                        ///
                        File.AppendAllText(filePath, csv.ToString() + Environment.NewLine);
                    }
                }
                return; 
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="strip"></param>
       
        /// <summary>
        /// 
        /// </summary>
        /// <param name="strip"></param>
        public void AppendResultData(Dictionary<string, string> datarow, Header header, string str2DCode = null)
        {
            lock (this) {
                if (str2DCode == null)
                    this._currentFile = string.Format("{0}-{1}.csv", "ASY-Line01", DateTime.Now.ToString("ddMMyyyy"));
                else
                    this._currentFile = string.Format("{0}-{1}.csv", "ASY-Line01", str2DCode);
                // Dump data to current file
                var filePath = string.Format("{0}\\{1}", this.OutputResultPath, this._currentFile);
                ///
                if (File.Exists(filePath)) {
                    if (header == Header.Write)
                        ///
                        this.AppendDataHeader();
                }
                else {
                    if (str2DCode == null)
                ///
                {
                        CreateResultFile(); this.AppendDataHeader();
                    }
                    else {
                        CreateResultFile(str2DCode); this.AppendDataHeader();
                    }
                }
                ///
                File.AppendAllText(filePath, DumpCSV(datarow.Values.ToList()) + Environment.NewLine);
                ///
                return; 
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="total"></param>
        /// <param name="strResult"></param>
        /// <param name="str2DCode"></param>
        public void AppendResultData(int total, string strResult, string str2DCode = null)
        {
            lock (this) {
                //IsDirFile(fullPath);
                if (str2DCode == null)
                    this._currentFile = string.Format("{0}-{1}.csv", "ASY-Line01", DateTime.Now.ToString("ddMMyyyy"));//ddMMyyyy_HHmmss
                else
                    this._currentFile = string.Format("{0}_{1}_{2}.csv", str2DCode, "SHIPPING", DateTime.Now.ToString("yyyy.MM.dd.HH.mm.ss"));//ddMMyyyy_HHmmss
                                                                                                                                           // Dump data to current file
                var filePath = string.Format("{0}\\{1}", string.Format(this.OutputResultPath), this._currentFile);
                ///      

                //if (!File.Exists(string.Format("{ 0}", this.ResultPath)))
                FullPath = string.Format(@"{0}\{1}", OutputResultPath, this._currentFile);//string.Format(@"\\192.168.201.1\avi\RGOZ-888-test\IntegrateData")
                                                                                          ///
                if (!File.Exists(string.Format("{0}", FullPath))) {
                    ///
                    File.Create(FullPath).Dispose();
                }
                ///
                File.AppendAllText(filePath, strResult);
                ///
                return;
                //} 
            }
        }
        public string FullPath { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="datarow"></param>
        /// <returns></returns>
        private string DumpCSV(List<string> datarow)
        {
            var csv = new StringBuilder();
            ///
            datarow.ForEach(x => csv.Append(string.Format("{0},", x)));
            ///
            return csv.ToString();
        }

        /// <summary>
        /// 
        /// </summary>
        public List<string> ReadCsv(string data2DCode,bool bypassMode)
        {
            lock (this) {
                if (string.IsNullOrEmpty(data2DCode))
                    return null;

                ///
                try
                {
                    List<string> MuiltiLine = new List<string>();

                    if (!Directory.Exists(InputResultPath))
                    {

                        DirectoryInfo di = Directory.CreateDirectory(InputResultPath); return null;
                    }
                    else
                    {
                        ///
                        string[] dirs = Directory.GetFiles(InputResultPath, "*.csv");
                        if (dirs.Length == 0)
                            return null;
                        ///
                        strPath = dirs.First(x => (Path.GetFileName(x).Contains(data2DCode) == true));
                        ///

                        using (var reader = new StreamReader(strPath))
                        {
                            while (!reader.EndOfStream)
                            {
                                MuiltiLine.Add(reader.ReadLine());
                            }
                            reader.Close();
                            ///
                            if (!bypassMode)
                                DeletePath(strPath);
                        }
                        //DeleteCsv();
                        return MuiltiLine;
                    }
                }
                catch (Exception ex)
                {
                    //MessageBox.Show("[ Logs-Manager ReadCsv] : Server Download File Error", ex.ToString());
                    return null;
                }
            }
        }
        private void DeletePath(string path)
        {
            try
            {
                FileInfo finfo = new FileInfo(path);

                //DirectoryInfo di = finfo.Directory;

                //if (di.Exists)
                //    File.Delete(path);
                if (finfo.Attributes == FileAttributes.Directory)
                {
                    Directory.Delete(path, true);
                }
                else if (finfo.Attributes == FileAttributes.Archive)
                {
                    File.Delete(path);
                }
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        private string strPath { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public void DeleteCsv()
        {
            if (Directory.Exists(InputResultPath)) {

                System.IO.Directory.Delete(strPath, true);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="data2DCode"></param>
        /// <returns></returns>
        public List<string> ThisReadCsv(string data2DCode)
        {
            lock (this) {
                try {
                    if (string.IsNullOrEmpty(data2DCode))
                        return null;
                    //\\192.168.201.1\avi\RGOZ-888-test\IntegrateDataToPLC\THA94660MK32128AX_TOTAL.csv
                    FileInfo fi = new FileInfo(string.Format("{0}\\{1}.csv", InputResultPath, data2DCode));
                    ///
                    List<string> MuiltiLine = new List<string>();
                    ///
                    //Open file for Read\Write
                    FileStream fs = fi.Open(FileMode.OpenOrCreate, FileAccess.Read, FileShare.Read);

                    //Create object of StreamReader by passing FileStream object on which it needs to operates on
                    StreamReader sr = new StreamReader(fs);

                    //Use ReadToEnd method to read all the content from file
                    //string fileContent = sr.ReadToEnd();

                    //using (var reader = new StreamReader(strPath)) {
                    while (!sr.EndOfStream) {
                        MuiltiLine.Add(sr.ReadLine());
                    }
                    sr.Close();
                    //
                    fs.Close();
                    //}
                    return MuiltiLine;
                    //}
                }
                catch (Exception ex) {

                    throw new Exception(ex.Message);
                } 
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="strResultBuilder"></param>
        /// <param name="data2DCode"></param>
        internal void DumpResultCsv(StringBuilder strResultBuilder,string data2DCode)
        {
            if (string.IsNullOrEmpty(data2DCode))
                ///
                data2DCode = "None2DCode";

            var intCount = Regex.Matches(strResultBuilder.ToString(), Environment.NewLine).Count;
            ///
            AppendResultData(intCount, strResultBuilder.ToString(), data2DCode);
        }
       
        /// <summary>
        /// 
        /// </summary>
        /// <param name="data2DCode"></param>
        /// <returns></returns>
        internal string GetCsvFileName(string data2DCode)
        {
            lock (this) {
                if (string.IsNullOrEmpty(data2DCode))
                    return null;

                List<string> MuiltiLine = new List<string>();
                // Determine whether the directory exists.
                if (!Directory.Exists(InputResultPath)) {

                    DirectoryInfo di = Directory.CreateDirectory(InputResultPath); return null;
                }
                else {
                    ///
                    string[] dirs = Directory.GetFiles(InputResultPath, "*.csv");
                    if (dirs.Length == 0)
                        return null;
                    ///
                    return dirs.First(x => (Path.GetFileName(x).Contains(data2DCode) == true));
                } 
            }
        }

    }
}
