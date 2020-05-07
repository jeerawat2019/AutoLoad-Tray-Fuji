using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FUJ_DataTranfer
{
    public class iNetwork
    {
        private static object obj = new object();
        /// <summary>
        /// 
        /// </summary>
        /// <param name="PathResult"></param>
        public static PLC.iError FTPUpLoadDataServer(Uri serverUri, string username, string login, string filename, string InputResultPath)
        {
            lock (obj) {
                try {
                    if (string.IsNullOrEmpty(filename))
                        return PLC.iError.InputParameterNull;
                    ///
                    var data = GetCsvFileName(filename, InputResultPath);

                    if (data.Length <= 0)
                        throw new Exception("Error Get file name");
                    ///
                    var num = data.LastIndexOf("\\") + 2;
                    ///
                    var str = data.Substring(num, data.Length - num);
                    //ftpIP = ftpIP + data2DCode + ".csv";
                    // Get the object used to communicate with the server.ftp://192.168.1.100/THA939318D221286Z
                    FtpWebRequest request = (FtpWebRequest)WebRequest.Create(serverUri + "/" + str);//"ftp://192.168.1.10/Shippingfolder"
                    request.Method = WebRequestMethods.Ftp.UploadFile;
                    // This example assumes the FTP site uses anonymous logon.
                    request.Credentials = new NetworkCredential(username, login);
                    // Copy the contents of the file to the request stream.
                    byte[] fileContents;
                    using (StreamReader sourceStream = new StreamReader(data)) {
                        fileContents = Encoding.UTF8.GetBytes(sourceStream.ReadToEnd());
                    }

                    using (Stream requestStream = request.GetRequestStream()) {
                        requestStream.Write(fileContents, 0, fileContents.Length);
                    }

                    using (FtpWebResponse response = (FtpWebResponse)request.GetResponse()) {
                        Console.WriteLine($"Upload File Complete, status {response.StatusDescription}");
                    }
                    return PLC.iError.Normal;
                }
                catch (Exception ex) {

                    return PLC.iError.FTPUpLoadDataServer;
                    //MessageBox.Show( ex.Message.ToString(), "FTP UpLoad Data Server Error.");
                } 
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="data2DCode"></param>
        /// <returns></returns>
        internal static string GetCsvFileName(string data2DCode,string InputResultPath)
        {
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
        /// <summary>
        /// 
        /// </summary>
        public static PLC.iError FTPDownLoadDataServer(Uri serverUri, string username, string login,out List<string> result)
        {
            result = null;
            lock (obj) {
                try {
                    if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(login))
                        ///
                        return PLC.iError.InputParameterNull;
                    ///ftpPath = ftpPath + data2DCode + ".csv";
                    ///Get the object used to communicate with the server.ftp://192.168.1.100/THA939318D221286Z
                    FtpWebRequest request = (FtpWebRequest)WebRequest.Create(serverUri);//"ftp://192.168.1.10/AVITotalfolder/THA939318D221286Z.csv"
                    request.Method = WebRequestMethods.Ftp.DownloadFile;
                    /// This example assumes the FTP site uses anonymous logon.
                    request.Credentials = new NetworkCredential(username, login);
                    ///
                    FtpWebResponse response = (FtpWebResponse)request.GetResponse();
                    ///
                    Stream responseStream = response.GetResponseStream();
                    ///
                    StreamReader reader = new StreamReader(responseStream);
                    ///
                    result = new List<string>();
                    ///
                    while (!reader.EndOfStream) {

                        //MuiltiLine.Add(reader.ReadLine());
                        result.Add(reader.ReadLine());
                    }
                    ///
                    Console.WriteLine($"Download Complete, status {response.StatusDescription}");
                    ///
                    reader.Close();
                    ///
                    response.Close();
                    ///
                    return PLC.iError.Normal;

                }
                catch (Exception ex) {

                    return PLC.iError.FTPDownLoadDataServer;
                    //MessageBox.Show(ex.Message.ToString(), "FTP Download Data Server Error."); return null;
                } 
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static PLC.iError GetFileList(Uri serverUri,string username,string login,out string[] result)
        {
           
            StringBuilder strBuilder = new StringBuilder();
            WebResponse response = null;
            StreamReader reader = null;

            lock (obj) {
                try {
                    FtpWebRequest request = (FtpWebRequest)WebRequest.Create(serverUri);//"ftp://192.168.1.10/AVITotalfolder"
                    request.UseBinary = true;
                    request.Method = WebRequestMethods.Ftp.ListDirectory;
                    //request.Credentials = new NetworkCredential(ftpUserName, ftpPassWord);
                    request.Credentials = new NetworkCredential(username, login);
                    request.KeepAlive = false;
                    request.UsePassive = false;
                    response = request.GetResponse();
                    reader = new StreamReader(response.GetResponseStream());
                    string line = reader.ReadLine();
                    while (line != null) {
                        strBuilder.Append(line);
                        strBuilder.Append("\n");
                        line = reader.ReadLine();
                    }
                    strBuilder.Remove(strBuilder.ToString().LastIndexOf('\n'), 1);
                    result = strBuilder.ToString().Split('\n');
                    return PLC.iError.Normal;
                }
                catch (Exception ex) {
                    if (reader != null) {
                        reader.Close();
                    }
                    if (response != null) {
                        response.Close();
                    }
                    result = null; return PLC.iError.GetFileList;
                    //MessageBox.Show(ex.Message, "FTP Get File List Server Error");
                } 
            }
        }

        public static void Download(string file)
        {
            try {
                string uri = "ftp://192.168.1.10/InputofAutoAVI" + "/" + file;
                Uri serverUri = new Uri(uri);
                if (serverUri.Scheme != Uri.UriSchemeFtp) {
                    return;
                }
                FtpWebRequest request = (FtpWebRequest)WebRequest.Create("ftp://192.168.1.10/InputofAutoAVI" + "/" + file);
                request.UseBinary = true;
                request.Method = WebRequestMethods.Ftp.DownloadFile;
                //request.Credentials = new NetworkCredential(ftpUserName, ftpPassWord);
                request.Credentials = new NetworkCredential("MainServer", "1234");
                request.KeepAlive = false;
                request.UsePassive = false;
                FtpWebResponse response = (FtpWebResponse)request.GetResponse();
                Stream responseStream = response.GetResponseStream();
                FileStream writeStream = new FileStream("C:\\InputTrayResult" + "\\" + file, FileMode.Create);
                int Length = 2048;
                Byte[] buffer = new Byte[Length];
                int bytesRead = responseStream.Read(buffer, 0, Length);
                while (bytesRead > 0) {
                    writeStream.Write(buffer, 0, bytesRead);
                    bytesRead = responseStream.Read(buffer, 0, Length);
                }
                writeStream.Close();
                response.Close();
            }
            catch (WebException wEx) {
                MessageBox.Show(wEx.Message, "Download Error");
            }
            catch (Exception ex) {
                MessageBox.Show(ex.Message, "Download Error");
            }
        }
        public static PLC.iError DeleteFileOnFtpServer(Uri serverUri, string ftpUsername, string ftpPassword)
        {
            lock (obj) {
                try {
                    // 
                    if (serverUri.Scheme != Uri.UriSchemeFtp) {
                        return PLC.iError.InputParameterNull;
                    }
                    // Get the object used to communicate with the server.
                    FtpWebRequest request = (FtpWebRequest)WebRequest.Create(serverUri);//"ftp://192.168.1.10/AVITotalfolder/THA939318D221286Z.csv"
                    request.Credentials = new NetworkCredential(ftpUsername, ftpPassword);
                    request.Method = WebRequestMethods.Ftp.DeleteFile;

                    FtpWebResponse response = (FtpWebResponse)request.GetResponse();
                    //Console.WriteLine("Delete status: {0}", response.StatusDescription);
                    response.Close();
                    return PLC.iError.Normal;
                }
                catch (Exception ex) {
                    //MessageBox.Show(ex.Message, "Delete File On Ftp Server Error");
                    return PLC.iError.DeleteFileOnFtpServer;
                } 
            }
        }
       
    }
}
