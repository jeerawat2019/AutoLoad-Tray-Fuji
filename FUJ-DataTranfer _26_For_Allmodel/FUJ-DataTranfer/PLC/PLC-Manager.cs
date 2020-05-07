using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Diagnostics;

using PCBase_Interface.Brand.Misubishi.Divice.PLC;

namespace FUJ_DataTranfer.PLC
{
    public class PLC_Manager //: App.AppBase
    {
        //public static PLC_Commu PLC_Commu { get; set; } = new PLC_Commu();
        /// <summary>
        /// 
        /// </summary>
        //public static Dictionary<string, string> Q02UCDP_MemConfig_Tray { get; set; }
        /// <summary>
        /// <summary>
        /// 
        /// </summary>
        public static bool IsPlcConnect
        {
            get;
            set;
               // return (PLC_Builder.AxActOpen(mPLC_Commu) != 0) ? false : true;
        }
        /// <summary>
        /// Read Data 2DCode From PLC Start Address D7000..D7150
        /// </summary>
        /// 
     
        public static iError GetData2DCode(string GetData2DCode ,out List<string> Result)
        {
            Result = null;

            try
            {
                lock (objLock) {
                    ///

                    string sValue;
                    string Datatrim;
                    ///
                    if (!IsPlcConnect)
                        return iError.IsPlcConnect;
                    ///
                    List<string> listValue = new List<string>();
                    ///
                    PLC_Builder.ReadMuiltiWordData(600, GetData2DCode, out sValue);//450 Word [1Data = 2D-Code =>11 WORD]
                    //string[] ListData;
                    Datatrim = sValue.Trim(new char[] {'\0',','} );               

                    //SimulationData2DCode  BEFORE \r  Test \0
                    Result = Datatrim.Split(new char[] { '\r'}).ToList().Select(x =>
                    {
                        if (string.IsNullOrEmpty(x) || x.Contains("NGERROR") || x.Contains("[null]") || x.Contains("ERROR"))
                            return "NG-ERROR";
                        return x.Substring(0, x.IndexOf(':'));
                    }).ToList();
                  
                 
                    ///
                    return iError.Normal; 
                }
            }
            catch (Exception)
            {
               return iError.GetData2DCode; 
                
            }

        }
        //Input Zone 1 Form PLC Start Address D8600..D8609
        public static iError IsData2DReady(string IsData2DReady,out bool result)
        {
            result = false;
            ///
            Thread.Sleep(50);
            try
            {
                lock (objLock) {
                    ///
                    int i = 1;
                    if (!IsPlcConnect)
                        return iError.IsPlcConnect;
                    string[] strOut;
                    ///
                    PLC_Builder.ReadDeviceRandom2(new System.Windows.Forms.TextBox() { Text = IsData2DReady }, i.ToString(), out strOut);
                    ///
                    result = (int.Parse(strOut[i - 1]) == 1) ? true : false;
                    ///
                    return iError.Normal; 
                }
            }
            catch (Exception)
            {

                return iError.IsData2DReady;
            }
        }

        public static iError IsConfSetResultPartToPLC( string IsConfSetResultPartToPLC,out bool result)
        {
            result = false;
            ///
            Thread.Sleep(50);
            try
            {
                lock (objLock) {
                    ///
                    int i = 1;
                    if (!IsPlcConnect)
                        return iError.IsPlcConnect;
                    ///
                    string[] strOut;
                    ///
                    PLC_Builder.ReadDeviceRandom2(new System.Windows.Forms.TextBox() { Text = IsConfSetResultPartToPLC }, i.ToString(), out strOut);
                    ///
                    result = (int.Parse(strOut[i - 1]) == 1) ? true : false;
                    ///
                    return iError.Normal; 
                }
            }
            catch (Exception)
            {

                return iError.IsConfSetResultPartToPLC;
            }
           
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="IsTrayInPosition"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        public static iError IsTrayInPosition(string IsTrayInPosition , out bool result)
        {
            result = false;
            ///
            Thread.Sleep(50);
            try
            {
                lock (objLock) {
                    ///
                    int i = 1;
                    if (!IsPlcConnect)
                        return iError.IsPlcConnect;
                    ///
                    string[] strOut;
                    ///
                    PLC_Builder.ReadDeviceRandom2(new System.Windows.Forms.TextBox() { Text = IsTrayInPosition }, i.ToString(), out strOut);
                    ///
                    result = (int.Parse(strOut[i - 1]) == 1) ? true : false;
                    ///
                    return iError.Normal; 
                }
            }
            catch (Exception)
            {

                return iError.IsTrayInPosition;
            }
           
        }
        public static iError IsTrayOutPosition(string IsTrayOutPosition , out bool result)
        {
            result = false;
            ///
            Thread.Sleep(50);
            ///
            try
            {
                lock (objLock) {

                    int i = 1;
                    if (!IsPlcConnect)
                        return iError.IsPlcConnect;
                    ///
                    string[] strOut;
                    ///
                    PLC_Builder.ReadDeviceRandom2(new System.Windows.Forms.TextBox() { Text = IsTrayOutPosition }, i.ToString(), out strOut);
                    ///
                    result = (int.Parse(strOut[i - 1]) == 1) ? true : false;
                    ///
                    return iError.Normal; 
                }
            }
            catch (Exception)
            {

                return iError.IsTrayOutPosition;
            }
           
        }
        /// Write DataResult Start Address :D + 49 = D
        /// <summary>
        /// 
        /// </summary>
        public static iError GetDataResultFormPLC(string GetDataResultFormPLC , out List<string> result)
        {
            result = null;
            try
            {
                lock (objLock) {
                    ///
                    int i = 0;
                    if (!IsPlcConnect)
                        return iError.IsPlcConnect;
                    ///
                    string[] strOut; string sAddress = null;
                    ///
                    int j = GetDataResultFormPLC.IndexOf('D');
                    ///
                    var address = GetDataResultFormPLC.Substring(j + 1, GetDataResultFormPLC.Length - (j + 1));
                    ///
                    ListPCSetResultToPLC.ForEach((x) =>
                        {
                            if (i != ListPCSetResultToPLC.Count() - 1) {
                                sAddress += string.Format("{0}" + "{1}" + "\n", "D", (int.Parse(address) + i).ToString());
                            }
                            else {
                                sAddress += string.Format("{0}" + "{1}", "D", (int.Parse(address) + i).ToString());
                            }
                            i++;
                        });
                    ///
                    //PLC_Builder.ReadDeviceRandom2(new System.Windows.Forms.TextBox() { Text = sAddress }, "", out strOut);
                    PLC_Builder.ReadDeviceRandom2(new System.Windows.Forms.TextBox() { Text = sAddress }, FUJ_DataTranfer.Properties.Settings.Default.IndexData, out strOut);

                    result = strOut.Select(x =>
                     {
                         if (int.Parse(x) == 1) {
                             return "OK";
                         }
                         else if (int.Parse(x) == 2) { return "NG"; }
                         else if (int.Parse(x) == 3) { return "ERR 2DCODE"; }
                         else if (int.Parse(x) == 4) { return "ERR POSC."; }
                         else if (int.Parse(x) == 5) { return "ERR PICKUP"; }
                         else
                             return "NONE";
                    ///
                }).ToList();

                    return iError.Normal; 
                }
            }
            catch (Exception)
            {

                return iError.GetDataResultFormPLC;
            }
        }

        internal static iError SetStatusProcess(string SetConfStartProcess, bool status)
        {
            try
            {
                lock (objLock)
                {
                    ///         
                    if (!IsPlcConnect)
                        return iError.IsPlcConnect;
                    ///
                    var result = (status == true) ? 1 : 0;
                    ///
                    PLC_Builder.WriteDeviceRandom2(SetConfStartProcess, "1", new System.Windows.Forms.TextBox() { Text = result.ToString() });
                    ///
                    return iError.Normal;
                }
            }
            catch (Exception)
            {
                return iError.SetConfRead2DCode;

            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="v"></param>
        /// <returns></returns>
        internal static iError GetPartTotal(string sPartTotal, out string result)
        {
            result = null;
            ///
            string[] strOut; int i = 1;

            try
            {
                lock (objLock) {

                    if (string.IsNullOrEmpty(sPartTotal))
                        throw new Exception("PartTotal:> IsNullOrEmpty");
                    ///
                    PLC_Builder.ReadDeviceRandom2(new System.Windows.Forms.TextBox() { Text = sPartTotal }, i.ToString(), out strOut);
                    ///
                    result = strOut[i - 1];
                    ///
                    return iError.Normal; 
                }
            }
            catch (Exception)
            {

                return iError.GetPartTotal;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="v"></param>
        /// <returns></returns>
        internal static iError GetPartModel(string sGetPartModels,out string[] result)
        {
            result = null;
            ///
            int i = 1;
            ///
            try {
                lock (objLock) {

                    if (string.IsNullOrEmpty(sGetPartModels))
                        throw new Exception("GetPartModels:> IsNullOrEmpty");
                    ///         
                    if (!IsPlcConnect)
                        return iError.IsPlcConnect;
                    ///
                    PLC_Builder.ReadDeviceRandom2(new System.Windows.Forms.TextBox() { Text = sGetPartModels }, i.ToString(), out result);//20 word size
                                                                                   ///
                    return iError.Normal; 
                }
            }
            catch (Exception)
            {

                return iError.GetPartModel;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="p"></param>
        /// <param name="result"></param>
        internal static iError GetPLCStatusAutoRun(string sGetPLCStatusAutoRun, out string result)
        {
            result = null;
            ///
            string[] strOut; int i = 1;

            try {
                lock (objLock) {

                    if (string.IsNullOrEmpty(sGetPLCStatusAutoRun))
                        throw new Exception("PartTotal:> IsNullOrEmpty");
                    ///
                    PLC_Builder.ReadDeviceRandom2(new System.Windows.Forms.TextBox() { Text = sGetPLCStatusAutoRun }, i.ToString(), out strOut);
                    ///
                    result = strOut[i - 1];
                    ///
                    return iError.Normal;
                }
            }
            catch (Exception) {

                return iError.GetPartTotal;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="p"></param>
        /// <param name="result"></param>
        internal static iError GetConfStatusMode(string sGetPLCStatusAutoRun, out string result)
        {
            result = null;
            ///
            string[] strOut; int i = 1;

            try {
                lock (objLock) {

                    if (string.IsNullOrEmpty(sGetPLCStatusAutoRun))
                        throw new Exception("PartTotal:> IsNullOrEmpty");
                    ///
                    PLC_Builder.ReadDeviceRandom2(new System.Windows.Forms.TextBox() { Text = sGetPLCStatusAutoRun }, i.ToString(), out strOut);
                    ///
                    result = strOut[i - 1];
                    ///
                    return iError.Normal;
                }
            }
            catch (Exception) {

                return iError.GetPartTotal;
            }
        }
        public static iError SetConfStatusAuto(string SetConfRead2DCode, bool status)
        {

            try {
                lock (objLock) {
                    ///         
                    if (!IsPlcConnect)
                        return iError.IsPlcConnect;
                    ///
                    var result = (status == true) ? 9 : 0;
                    ///
                    PLC_Builder.WriteDeviceRandom2(SetConfRead2DCode, "1", new System.Windows.Forms.TextBox() { Text = result.ToString() });
                    ///
                    return iError.Normal;
                }
            }
            catch (Exception) {
                return iError.SetConfRead2DCode;

            }

            //}
        }
        //Output Zone 1 Form PC Start Address D8610..D8619
        public static iError SetConfRead2DCode(string SetConfRead2DCode ,bool status)
        {

            try
            {
                lock (objLock) {
                    ///         
                    if (!IsPlcConnect)
                        return iError.IsPlcConnect;
                    ///
                    var result = (status == true) ? 1 : 0;
                    ///
                    PLC_Builder.WriteDeviceRandom2(SetConfRead2DCode, "1", new System.Windows.Forms.TextBox() { Text = result.ToString() });
                    ///
                    return iError.Normal; 
                }
            }
            catch (Exception)
            {
                return iError.SetConfRead2DCode;
                
            }
                
            //}
        }
        public static iError SetResultPartToPLC(string SetResultPartToPLC,bool status)
        {
            try
            {
                lock (objLock) {
                    ///         
                    if (!IsPlcConnect)
                        return iError.IsPlcConnect;
                    //set
                    //{
                    var result = (status == true) ? 1 : 0;

                    PLC_Builder.WriteDeviceRandom2(SetResultPartToPLC, "1", new System.Windows.Forms.TextBox() { Text = result.ToString() });
                    //}
                    return iError.Normal; 
                }
            }
            catch (Exception)
            {

                return iError.SetResultPartToPLC;
            }
        }

        public static iError SetPCErrorToPLC(string SetPCErrorToPLC, iError iError)
        {

            try
            {
                lock (objLock) {
                    ///
                    var idec = (int)iError;
                    ///         
                    if (!IsPlcConnect)
                        return iError.IsPlcConnect;
                    ///
                    //var result = (status == true) ? 1 : 0;
                    ///
                    PLC_Builder.WriteDeviceRandom2(SetPCErrorToPLC, "1", new System.Windows.Forms.TextBox() { Text = idec.ToString() });
                    ///
                    return iError.Normal; 
                }
            }
            catch (Exception)
            {
                return iError.IsPlcConnect;

            }

            //}
        }
        public static iError SetCsvFileNameDownloadToPLC(string WordStart = "D1000", string strWriteWord = "", int ELEMENT_SIZE_WORD = 40)
        {
            try {
                lock (objLock) {
                    ///         
                    if (!IsPlcConnect)
                        return iError.IsPlcConnect;
                    //set
                    //{
                    //var result = (status == true) ? 1 : 0;

                    PLC_Builder.WriteMuiltiWordData(WordStart, strWriteWord, ELEMENT_SIZE_WORD);
                    //}
                    return iError.Normal;
                }
            }
            catch (Exception) {

                return iError.SetResultPartToPLC;
            }
        }
        public static iError SetCsvFileNameUploadToPLC(string WordStart = "D1000", string strWriteWord = "", int ELEMENT_SIZE_WORD = 40)
        {
            try
            {
                lock (objLock)
                {
                    ///         
                    if (!IsPlcConnect)
                        return iError.IsPlcConnect;
                    //set
                    //{
                    //var result = (status == true) ? 1 : 0;

                    PLC_Builder.WriteMuiltiWordData(WordStart, strWriteWord, ELEMENT_SIZE_WORD);
                    //}
                    return iError.Normal;
                }
            }
            catch (Exception)
            {

                return iError.SetResultPartToPLC;
            }
        }
        public static List<string> ListPCSetResultToPLC
        {
            get;
            internal set;
        }

      

        ///// <summary>
        ///// 
        ///// </summary>
        //public override void StationInitialized()
        //{
        //    base.StationInitialized();
        //    ///
        //    mPLC_Commu = new PLC_Commu("Q03CPU", "PLC Data Builder");
        //    mPLC_Commu.Id = "1";
        //    mPLC_Commu.IPAddress = "192.168.1.20";
        //    mPLC_Commu.mActSupportMsgClass = new ActSupportMsgLib.ActSupportMsgClass();
        //    mPLC_Commu.mActUtlTypeClass = new ActUtlTypeLib.ActUtlTypeClass();
        //    //mPLC_Commu.mActMLProgTypeClass = new ActProgTypeLib.ActMLProgTypeClass();
        //    mPLC_Commu.iStstionNumber = 5;
        //    mPLC_Commu.mActUtlTypeClass.ActLogicalStationNumber = 0;

        //}

        public static void PLC_Connect(PLC_Commu PLC_Commu)
        {
            IsPlcConnect = ( PLC_Builder.AxActOpen(PLC_Commu) == 0) ? true :false;
        }
        private static object objLock = new object(); 
        /// <summary>
        /// 
        /// </summary>
        public static iError PC_LinkCommu(string PC_LinkCommu)
        {
            int i = 1;
            ///
            try
            {
                lock (objLock) {

                    string[] strOut;
                    ///
                    if (!IsPlcConnect)
                        return iError.IsPlcConnect;
                    ///
                    PLC_Builder.ReadDeviceRandom2(new System.Windows.Forms.TextBox() { Text = PC_LinkCommu }, i.ToString(), out strOut);
                    ///
                    var result = (int.Parse(strOut[i - 1]) > 0) ? 0 : 1;
                    ///
                    PLC_Builder.WriteDeviceRandom2(PC_LinkCommu, i.ToString(), new System.Windows.Forms.TextBox() { Text = result.ToString() });
                    ///
                    Thread.Sleep(0);
                    ///
                    return iError.Normal; 
                }
            }
            catch (Exception ex)
            {
                return iError.PC_LinkCommu;
                
            }
        }
        /// Write DataResult Start Address :D8620 + 49 = D8669
        /// <summary>
        /// 
        /// </summary>
        public static iError PCWriteDataResultToPLC(string PCWriteDataResultToPLC)
        {
            string sAddress = null, sValue = null;

            try
            {
                lock (objLock) {
                    int i = 0;
                    int j = PCWriteDataResultToPLC.IndexOf('D');
                    var address = PCWriteDataResultToPLC.Substring(j + 1, PCWriteDataResultToPLC.Length - (j + 1));
                    ///

                    ///
                    if (!IsPlcConnect)
                        return iError.IsPlcConnect;
                    ///
                    ListPCSetResultToPLC.ForEach((x) =>
                    {
                        if (i != ListPCSetResultToPLC.Count() - 1) {
                            sAddress += string.Format("{0}" + "{1}" + "\n", "D", (int.Parse(address) + i).ToString());
                            sValue += string.Format("{0}" + "\n", x);
                        }
                        else {
                            sAddress += string.Format("{0}" + "{1}", "D", (int.Parse(address) + i).ToString());
                            sValue += string.Format("{0}", x);
                        }
                        i++;
                    });
                    ///
                    PLC_Builder.WriteDeviceRandom2(sAddress, ListPCSetResultToPLC.Count().ToString(), new System.Windows.Forms.TextBox() { Text = sValue });
                    ///
                    return iError.Normal; 
                }
            }
            catch (Exception)
            {

                return iError.PCWriteDataResultToPLC;
            }
        }
       
        public static string SimulationData2DCode()
        {
            StringBuilder strBuilder = new StringBuilder();
           
            strBuilder.Append("THA93931AES212864:001:\r");
            strBuilder.Append("THA93931AES212864:002:\r");
            strBuilder.Append("THA93931AES212864:003:\r");
            strBuilder.Append("THA93931AES212864:004:\r");
            strBuilder.Append("THA93931AES212864:005:\r");
            strBuilder.Append("NG-ERROR\r");
            strBuilder.Append("THA93931AES212864:007:\r");
            strBuilder.Append("THA93931AES212864:008:\r");
            strBuilder.Append("THA93931AES212864:009:\r");
            strBuilder.Append("THA93931AES212864:010:\r");
            strBuilder.Append("THA93931AES212864:011:\r");
            strBuilder.Append("THA93931AES212864:012:\r");
            strBuilder.Append("THA93931AES212864:013:\r");
            strBuilder.Append("THA93931AES212864:014:\r");
            strBuilder.Append("THA93931AES212864:015:\r");
            strBuilder.Append("THA93931AES212864:016:\r");
            strBuilder.Append("THA93931AES212864:017:\r");
            strBuilder.Append("THA93931AES212864:018:\r");
            //strBuilder.Append("THA93931AES212864:019:\r");
            //strBuilder.Append("THA93931AES212864:020:\r");
            //strBuilder.Append("THA93931AES212864:021:\r");
            //strBuilder.Append("THA93931AES212864:022:\r");
            //strBuilder.Append("THA93931AES212864:023:\r");
            //strBuilder.Append("THA93931AES212864:024:\r");
            //strBuilder.Append("THA93931AES212864:025:\r");
            //strBuilder.Append("THA93931AES212864:026:\r");
            //strBuilder.Append("THA93931AES212864:027:\r");
            //strBuilder.Append("THA93931AES212864:028:\r");
            //strBuilder.Append("THA93931AES212864:029:\r");
            //strBuilder.Append("THA93931AES212864:030:\r");
            //strBuilder.Append("THA93931AES212864:031:\r");
            //strBuilder.Append("THA93931AES212864:032:\r");
            //strBuilder.Append("THA93931AES212864:033:\r");
            //strBuilder.Append("THA93931AES212864:034:\r");
            //strBuilder.Append("THA93931AES212864:035:\r");
            //strBuilder.Append("THA93931AES212864:036:\r");
            //strBuilder.Append("THA93931AES212864:037:\r");
            //strBuilder.Append("THA93931AES212864:038:\r");
            //strBuilder.Append("THA93931AES212864:039:\r");
            //strBuilder.Append("THA93931AES212864:040:\r");
            //strBuilder.Append("THA93931AES212864:041:\r");
            //strBuilder.Append("THA93931AES212864:042:\r");
            //strBuilder.Append("THA93931AES212864:043:\r");
            //strBuilder.Append("THA93931AES212864:044:\r");
            //strBuilder.Append("THA93931AES212864:045:\r");
            //strBuilder.Append("THA93931AES212864:046:\r");
            //strBuilder.Append("THA93931AES212864:047:\r");
            //strBuilder.Append("THA93931AES212864:048:\r");
            //strBuilder.Append("THA93931AES212864:049:\r");
         
            return strBuilder.ToString();
        }
    }
}
