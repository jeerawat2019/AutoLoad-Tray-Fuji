using Ai_PCSystem.Strings;
using PCBase_Interface.Brand.Misubishi.Divice.PLC;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Diagnostics;

namespace FUJ_DataTranfer.State_Machine.Station
{
    public class PickAndPlace : App.AppBase
    {
        private Thread mStationThread;
        ///
        /// <summary>
        /// 
        /// </summary>
        private string ThreadStationName;
        /// <summary>
        /// 
        /// </summary>
        delegate void Station1_Stop();
        /// <summary>
        /// 
        /// </summary>
        private ManualResetEvent StationResetEvent = new ManualResetEvent(true);
        /// <summary>
        /// 
        /// </summary>
        private AF_Monitoring.Logging_Data.Logs_Manager mLogsManager = null;
        ///// <summary>
        ///// 
        ///// </summary>
        private List<Ai_Product.Product.PartResult> mPartList = null;
        /// <summary>
        /// 
        /// </summary>
        public bool mInterLockStateDisplay = true;
        /// <summary>
        /// 
        /// </summary>
        //private StateMC mStateMC = StateMC.StationInitial;
        /// <summary>
        /// 
        /// </summary>
        //private PLC.PLC_Manager mPLC_Manager = null;
        /// <summary>
        /// 
        /// </summary>
        public List<string> mList2DCodeFormPLC = null;
        /// <summary>
        /// 
        /// </summary>
        private int CountGetPart2DCode { get; set; }
        /// <summary>
        /// 
        /// </summary>
        
        //public string GetPartTotal
        //{
        //    get
        //    {
        //        return PLC.PLC_Manager.PartTotal(Q02UCDP_MemConfig_Tray["GetPartTotal"]);
        //    }

        //}
        ///// <summary>
        ///// 
        ///// </summary>
        //public string GetPartModel
        //{
        //    get
        //    {
        //        return PLC.PLC_Manager.GetPartModel(Q02UCDP_MemConfig_Tray["GetPartModel"]);
        //    }

        //}
        /// <summary>
        /// 
        /// </summary>
        public PickAndPlace()
        {

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sName"></param>
        public PickAndPlace(string sName) : base(sName)
        {
            ThreadStationName = sName;
            ///
        }
        
        /// <summary>
        /// 
        /// </summary>
        public override void StationInitialized()
        {
            ///
            PStateMC = StateMC.StopState;
            ///
        }
        /// <summary>
        /// </summary>
        /// </summary>
        public override void InitializeIDReferences()
        {
            ///
            mStationThread = new Thread(new ThreadStart(AutoRunState)) { IsBackground = true, Priority = ThreadPriority.Normal };            
            ///
            mStationThread.Name = ThreadStationName;
            ///
            mLogsManager = new AF_Monitoring.Logging_Data.Logs_Manager();
            ///                      
            base.StationInitialized();
        }
        /// <summary>
        /// 
        /// </summary>
        private void AutoRunState()
        {
            ///
            mLogsManager.InputResultPath = InputResultPath;//@"C:\InputTrayResult";
            ///                                             
            mLogsManager.OutputResultPath = OutputResultPath;//@"C:\OutputTrayResult";//IntegrateData
            ///
            while (StationResetEvent.WaitOne()) {
                try {
                    if (PLC_Commu.IsNetworkConnect)
                    {
                        ///
                        StateMachine();
                    }
                    else
                        throw new Exception("Network can not found!");
                    ///
                }
                catch (Exception ex) {
                    ///
                }
                finally {
                    ///
                }
                //
                Thread.Sleep(50);
            }
        }
        public enum StateMC
        {
            [Str_Enum.StringValueAttribute("StopState")]
            StopState = 0,
            [Str_Enum.StringValueAttribute("StationInitial")]
            StationInitial = 1,
            [Str_Enum.StringValueAttribute("IsTrayInPosition.")]
            IsTrayInPosition = 2,
            [Str_Enum.StringValueAttribute("IsConfOnRead_2DCode=On")]
            IsConfOnRead_2DCode = 3,
            [Str_Enum.StringValueAttribute("ReadData2DCode.")]
            ReadData2DCode = 4,
            [Str_Enum.StringValueAttribute("IsConfOffRead_2DCode=Off")]
            IsConfOffRead_2DCode = 5,
            [Str_Enum.StringValueAttribute("IsTrayOutPosition")]
            GetResultCounter = 6,
            [Str_Enum.StringValueAttribute("SetResultToPLC=Off")]
            SetResultToPLC = 7,
            [Str_Enum.StringValueAttribute("IsConfSetResultToPLC")]
            IsConfSetResultToPLC = 8,
            [Str_Enum.StringValueAttribute("IsTrayOutPosition")]
            IsTrayOutPosition = 9,
            [Str_Enum.StringValueAttribute("DataCalculate")]
            DataCalculate = 10,
        }
        /// <summary>
        /// 
        /// </summary>
        public PLC.iError Error = PLC.iError.Normal;
        /// <summary>
        /// 
        /// </summary> 
        public bool Select_Shot1 { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public bool Select_Shot2 { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public bool Select_Shot3 { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string PositionShot1 { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string PositionShot2 { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string PositionShot3 { get; set; }
        /// <summary>
        /// 
        /// </summary>
        List<string> strAllResults;
        /// <summary>
        /// 
        /// </summary>
        public int loopRead2D { get; set; }
        private void StateMachine()
        {
            Select_Shot1 = FUJ_DataTranfer.Properties.Settings.Default.SelectShot1;
            ///
            Select_Shot2 = FUJ_DataTranfer.Properties.Settings.Default.SelectShot2;
            ///
            Select_Shot3 = FUJ_DataTranfer.Properties.Settings.Default.SelectShot3;
            ///
            PositionShot1 = FUJ_DataTranfer.Properties.Settings.Default.NumberPosition_Shot1;
            /////
            PositionShot2 = FUJ_DataTranfer.Properties.Settings.Default.NumberPosition_Shot2;
            /////
            PositionShot3 = FUJ_DataTranfer.Properties.Settings.Default.NumberPosition_Shot3;
            ////
            ///
            //------------------------------------new loop ------------------------------//
            bool result = false;
            //----------------------------------------------------------------------------//
            ///           
            Error = PLC.iError.Normal;
            ///
            switch (PStateMC) {
                case StateMC.StopState:
                    ///
                    PStateMC = StateMC.StationInitial;
                    ///
                    break;
                case StateMC.StationInitial:
                    /// Clear Data Index Position Work in PLC.
                    PStateMC = StateMC.IsTrayInPosition;
                    ///
                    break;
                case StateMC.IsTrayInPosition:           
                    ///
                    PLC.PLC_Manager.SetConfRead2DCode(Q02UCDP_MemConfig_Tray["SetConfRead2DCode"], false);
                    ///
                    PLC.PLC_Manager.SetStatusProcess(Q02UCDP_MemConfig_Tray["SetStatusProcess"], true);
                    ///
                    CountGetPart2DCode = 0;
                    ///
                    mList2DCodeFormPLC = new List<string>();
                    ///
                    PStateMC = StateMC.IsConfOnRead_2DCode;
                    ///
                    break;
                case StateMC.IsConfOnRead_2DCode:
                    ///                    
                    result = false;
                    ///
                    Error = PLC.PLC_Manager.IsData2DReady(Q02UCDP_MemConfig_Tray["IsData2DReady"], out result);
                    ///
                    if (Error != PLC.iError.Normal)
                    {
                        PLC.PLC_Manager.SetPCErrorToPLC(Q02UCDP_MemConfig_Tray["SetPCErrorToPLC"], Error);
                        //Set Error to PLC
                        break;
                    }
                    if (result)
                    ///
                    {
                        PStateMC = StateMC.ReadData2DCode; PLC.PLC_Manager.SetStatusProcess(Q02UCDP_MemConfig_Tray["SetStatusProcess"], false);
                    }                                    
                    else
                    {
                        PStateMC = StateMC.IsConfOnRead_2DCode;
                    }
                    //Thread.Sleep(50);
                    break;
                case StateMC.ReadData2DCode:
                    ///
                    var strListValue = new List<string>();
                    ///
                    Thread.Sleep(0);
                    //Get All data shot 1-3  result 450 word 
                    Error = PLC.PLC_Manager.GetData2DCode(Q02UCDP_MemConfig_Tray["GetData2DCode"], out strListValue);
                    ///
                    if (Error != PLC.iError.Normal)
                    {
                        PLC.PLC_Manager.SetPCErrorToPLC(Q02UCDP_MemConfig_Tray["SetPCErrorToPLC"], Error);
                        //Set Error to PLC
                        break;
                    }
                    ///
                    strAllResults = strListValue;
                    ///
                    PLC.PLC_Manager.SetConfRead2DCode(Q02UCDP_MemConfig_Tray["IsData2DReady"], false);
                    ///
                    PStateMC = StateMC.DataCalculate;
                    ///
                    break;
                case StateMC.DataCalculate:
                    ///
                    Error = DataCalculate(strAllResults);
                    ///
                    if (Error != PLC.iError.Normal)
                    {
                        PLC.PLC_Manager.SetPCErrorToPLC(Q02UCDP_MemConfig_Tray["SetPCErrorToPLC"], Error);
                        //Set Error to PLC
                        break;
                    }
                    ///
                    loopRead2D = 1;
                    ///
                    PStateMC = StateMC.SetResultToPLC;
                    ///
                    break;
              
                case StateMC.GetResultCounter:
                    ///
                    PStateMC = StateMC.SetResultToPLC;
                    ///
                    break;
                case StateMC.SetResultToPLC:
                   
                    ///
                    if (mList2DCodeFormPLC[0] != "NG-ERROR" && mList2DCodeFormPLC[0] != "ERROR") 
                    {
                        List<string> partlists;
                        ///
                        partlists =  (mLogsManager.ReadCsv(mList2DCodeFormPLC[0],BypassDeleteFileName));//"FTP"(EnableServer == false) ? DownLoadFileCsV(mList2DCodeFormPLC[0]) :
                        ///
                        if (partlists != null) {
                            ///
                            mInterLockStateDisplay = false;
                            ///
                            CsvPartList(partlists);
                            ///
                            PLC.PLC_Manager.SetResultPartToPLC(Q02UCDP_MemConfig_Tray["SetResultPartToPLC"], true);
                            ///
                            PStateMC = StateMC.IsConfSetResultToPLC;
                        }
                        else {
                            Error = PLC.iError.CSVFileNotFound;
                            ///
                            PLC.PLC_Manager.SetPCErrorToPLC(Q02UCDP_MemConfig_Tray["SetPCErrorToPLC"], Error);
                            ///
                            PStateMC = StateMC.StationInitial;
                            ///
                            break;
                        }
                    }
                    else {

                        Error = PLC.iError.FirstWork2DFail;
                        ///
                        PLC.PLC_Manager.SetPCErrorToPLC(Q02UCDP_MemConfig_Tray["SetPCErrorToPLC"], Error);
                        //PStateMC = StateMC.StationInitial;
                        PStateMC = StateMC.IsTrayInPosition;
                        ///
                        ResetState();
                        ////Set Error to PLC
                        break;
                        ///PStateMC = StateMC.StopState;
                    }
                    break;

                case StateMC.IsConfSetResultToPLC:
                    ///
                    Error = PLC.PLC_Manager.IsConfSetResultPartToPLC(Q02UCDP_MemConfig_Tray["IsConfSetResultPartToPLC"], out result);
                    ///
                    if (Error != PLC.iError.Normal)
                    {
                        PStateMC = StateMC.StationInitial;
                        ///
                        PLC.PLC_Manager.SetPCErrorToPLC(Q02UCDP_MemConfig_Tray["SetPCErrorToPLC"], Error);
                        //Set Error to PLC
                        break;
                    }
                    ///
                    if (result) 
                        ///
                        PStateMC = StateMC.IsTrayOutPosition;
                    ///
                    else 
                        ///
                        PStateMC = StateMC.IsConfSetResultToPLC;             
                        /// 
                    if (loopRead2D == 1) 
                        /// 
                        loopRead2D = 2;

                    break;
                case StateMC.IsTrayOutPosition:
                    ///
                    mInterLockStateDisplay = false;
                    ///
                    PLC.PLC_Manager.SetResultPartToPLC(Q02UCDP_MemConfig_Tray["SetResultPartToPLC"],false); ///17/2/2020
                    ///
                    Error = PLC.PLC_Manager.IsTrayOutPosition(Q02UCDP_MemConfig_Tray["IsTrayOutPosition"], out result);///17/2/2020
                    ///
                    if(Error != PLC.iError.Normal)
                    {
                        PStateMC = StateMC.StationInitial;
                        ///
                        PLC.PLC_Manager.SetPCErrorToPLC(Q02UCDP_MemConfig_Tray["SetPCErrorToPLC"], Error);
                        //Set Error to PLC
                        break;
                    }
                    if (result) {
                        ///
                        if (DumpResultParts(mLogsManager)) {
                            ///
                            PLC.PLC_Manager.SetPCErrorToPLC(Q02UCDP_MemConfig_Tray["SetPCErrorToPLC"], PLC.iError.DumpResultCSV);
                        }
                        ///
                        PLC.PLC_Manager.SetResultPartToPLC(Q02UCDP_MemConfig_Tray["IsTrayOutPosition"], false);
                        ///
                        PStateMC = StateMC.StopState;
                        ///
                        loopRead2D = 0;
                        ///
                        break;                        
                    }
                    else {
                        PStateMC = StateMC.IsTrayOutPosition;
                    }
                    Thread.Sleep(50);
                    ///
                    break;
                    ///
                default:

                    break;
            }
        }

        private PLC.iError DataCalculate(List<string> strListValue)
        {
            try
            {
              
                //if(Select_Shot1 == true )
                //{
                if (strListValue.Count > int.Parse(FUJ_DataTranfer.Properties.Settings.Default.IndexData))
                {
                    ///
                    string[] Removedata = PositionShot1.Split(',');
                    ///
                    if (Removedata.Length > 1)
                    {

                        for (int i = 0; i < Removedata.Length; i++)
                        {
                            string DataRemove = Removedata[i];
                            ///
                            strListValue.RemoveAt(int.Parse(DataRemove));
                        }
                    }

                }
                if (strListValue.Count == int.Parse(FUJ_DataTranfer.Properties.Settings.Default.IndexData))
                {
                    foreach (var item in strListValue)
                        ///
                        mList2DCodeFormPLC.Add((string)item);

                }
                return PLC.iError.Normal;
            }
            catch (Exception ex)
            {
                Error = PLC.iError.CalculateError;
                ///
                return Error;
            }           
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="data2Dcode"></param>
        /// <returns></returns>
        internal List<string> DownLoadFileCsV( string data2Dcode)
        {
            if (string.IsNullOrEmpty(data2Dcode))
                throw new Exception("Error Data 2DCode");
            ///
            var ftpUri = string.Format("ftp://{0}/{1}", IServer.HostNameByIP, IServer.SubFolderInput);
            ///
            string[] result;
            ///
            PLC.iError Error = iNetwork.GetFileList(new Uri(ftpUri),IServer.UserName,IServer.Password,out result);
            ///
            if(Error != PLC.iError.Normal)
            {
                PLC.PLC_Manager.SetPCErrorToPLC(Q02UCDP_MemConfig_Tray["SetPCErrorToPLC"], Error);return null;
            }
            ///
            var strCsvName = result.First(x => (x).Contains(data2Dcode) == true);
            ///
            var ftpUriCsv = string.Format("ftp://{0}/{1}", IServer.HostNameByIP, strCsvName);
            ///
            List<string> strList;
            ///
            Error = iNetwork.FTPDownLoadDataServer(new Uri(ftpUriCsv), IServer.UserName,IServer.Password,out strList);
            ///
            if(Error != PLC.iError.Normal)
            {
                PLC.PLC_Manager.SetPCErrorToPLC(Q02UCDP_MemConfig_Tray["SetPCErrorToPLC"], Error);return null;
            }
            if (BypassServer == false) {
                //Error = iNetwork.DeleteFileOnFtpServer(new Uri(ftpUriCsv), IServer.UserName, IServer.Password);

                //if (Error != PLC.iError.Normal) {
                //    PLC.PLC_Manager.SetPCErrorToPLC(Q02UCDP_MemConfig_Tray["SetPCErrorToPLC"], Error); return null;
                //}
                mLogsManager.DeleteCsv();
            }
            ///
            return strList;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="filename"></param>
        internal void UpLoadFileCsV(string data2DCode)
        {
            if (string.IsNullOrEmpty(data2DCode))
                throw new Exception("Error file name");
            ///
            var ftpPath = string.Format("ftp://{0}/{1}", IServer.HostNameByIP, IServer.SubFolderOutput);
            ///
            PLC.iError Error = iNetwork.FTPUpLoadDataServer(new Uri(ftpPath), IServer.UserName, IServer.Password, data2DCode, mLogsManager.OutputResultPath);
            ///
            if(Error != PLC.iError.Normal)
            {
                PLC.PLC_Manager.SetPCErrorToPLC(Q02UCDP_MemConfig_Tray["SetPCErrorToPLC"], Error); return;
            }
            ///
            return;
        }
        /// <summary>
        /// 
        /// </summary>
        private void CsvPartList(List<string> partlists)
        {
            try {

                CreatrParts(partlists);
                ///
                var objListOrder = mPartList.OrderBy(order => int.Parse(order.trayInput.AVITrayPosition)).ThenBy(order => order.trayInput.Piece2DCode).ToList();
                ///
                mPartList = objListOrder;
                ///
                ConvertResultToPLC(mPartList);//
                ///
                //DumpResultParts(mLogsManager); TEST
                ///
                GetPartListCsv = mPartList;
                ///
                PLC.PLC_Manager.PCWriteDataResultToPLC(Q02UCDP_MemConfig_Tray["PCWriteDataResultToPLC"]);
                ///
                PLC.PLC_Manager.SetResultPartToPLC(Q02UCDP_MemConfig_Tray["SetResultPartToPLC"], true);//write bit datacomp.
            }
            catch (Exception ex) {

                Error = PLC.iError.CSVFileNotFound;
                ///
                PLC.PLC_Manager.SetPCErrorToPLC(Q02UCDP_MemConfig_Tray["SetPCErrorToPLC"], Error);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private bool DumpResultParts(AF_Monitoring.Logging_Data.Logs_Manager mLogsManager)
        {
            try {

                StringBuilder strResultBuilder = new StringBuilder();
                ///
                if (mLogsManager == null || string.IsNullOrEmpty(mList2DCodeFormPLC[0]))//mList2DCodeFormPLC[0]
                    return false;
                int i = 0;
                ///
                List<string> value;//= new List<string>();

                PLC.iError Error = PLC.PLC_Manager.GetDataResultFormPLC(Q02UCDP_MemConfig_Tray["GetDataResultFormPLC"], out value);
                ///
                if (Error != PLC.iError.Normal) {
                    ///
                    PLC.PLC_Manager.SetPCErrorToPLC(Q02UCDP_MemConfig_Tray["SetPCErrorToPLC"], Error);
                    ///
                    return false;
                }
                mPartList.ForEach(x =>
                {
                ///
                strResultBuilder.Append(FUJ_DataTranfer.iCore.PropertiesInfo.GetResultPart(x));
                ///
                strResultBuilder.Append("," + value.ToArray()[i].ToString());//Str_Enum.StringEnum.GetStringValue()
                                                                             ///
                strResultBuilder.Append("\r\n");
                ///
                i++;
                });
                ///
                if (mPartList == null)
                    return false;
                ///
                mLogsManager.DumpResultCsv(strResultBuilder, mList2DCodeFormPLC[0]);
                ///
                return false;
            }
            catch (Exception) {

                return true;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        private void ConvertResultToPLC(List<Ai_Product.Product.PartResult> mPartList)
        {
            if (mPartList == null)
                return;
            ///
            PLC.PLC_Manager.ListPCSetResultToPLC = new List<string>();
            //int i = 0;
            ///
            mPartList.ForEach(x =>
            {
                x.PartStatus = (x.trayInput.JudgeTotal == "OK") ? Ai_Product.Ingredient.PartStatus.OK : Ai_Product.Ingredient.PartStatus.NG;
                ///
                if (x.PartStatus == Ai_Product.Ingredient.PartStatus.OK)
                {
                    ///
                    if (mList2DCodeFormPLC == null || x.trayInput.Piece2DCode == null)
                    {

                        x.PartStatus = Ai_Product.Ingredient.PartStatus.ErrorPosc;
                    }
                    else
                    {
                        ///
                        if (string.IsNullOrEmpty(mList2DCodeFormPLC[int.Parse(x.trayInput.AVITrayPosition) - 1]) || string.IsNullOrEmpty(x.trayInput.Piece2DCode))

                            x.PartStatus = Ai_Product.Ingredient.PartStatus.ErrorPosc;

                        else
                        {
                            if ((mList2DCodeFormPLC[int.Parse(x.trayInput.AVITrayPosition) - 1]) == "NG-ERROR")

                                x.PartStatus = Ai_Product.Ingredient.PartStatus.Error2DCode;
                            else
                            {
                                x.PartStatus = ((x.trayInput.Piece2DCode == mList2DCodeFormPLC[int.Parse(x.trayInput.AVITrayPosition) - 1])) ? Ai_Product.Ingredient.PartStatus.OK : Ai_Product.Ingredient.PartStatus.ErrorPosc;
                            }
                        }
                    }
                }
                if((mList2DCodeFormPLC[int.Parse(x.trayInput.AVITrayPosition) - 1]) == "NG-ERROR" && x.PartStatus == Ai_Product.Ingredient.PartStatus.NG)
                {
                    x.PartStatus = Ai_Product.Ingredient.PartStatus.Error2DCode;
                }
                if ((mList2DCodeFormPLC[int.Parse(x.trayInput.AVITrayPosition) - 1]) == "NG-ERROR" && string.IsNullOrEmpty(x.trayInput.Piece2DCode))
                {
                    x.PartStatus = Ai_Product.Ingredient.PartStatus.Error2DCode;
                }
                    ///
                    PLC.PLC_Manager.ListPCSetResultToPLC.Add(((int)x.PartStatus).ToString());

            });
        }
       
        /// <summary>
        /// 5
        /// </summary>
        /// <param name="resultList"></param>
        private void CreatrParts(List<string> resultList)
        {
            mPartList = new List<Ai_Product.Product.PartResult>();
            ///
            if (resultList == null)
                return;
            ///
            resultList.ForEach(x =>
            {
                Ai_Product.Product.PartResult mPart = new Ai_Product.Product.PartResult();
                ///
                FUJ_DataTranfer.iCore.PropertiesInfo.SetResultPart(mPart, x.ToString());
                ///
                mPartList.Add(mPart);
            });
        }      
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public bool StationNetWorkConnect()
        {
            try {
                ///
                return true;
            }
            catch (Exception) {
                ///
                return false;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public void ThreadStation_Start()
        {
            try
            {
                Console.WriteLine("Start State machine");
                ///
                PStateMC = StateMC.StopState;
                ///
                if (mStationThread.ThreadState == (System.Threading.ThreadState.Background | System.Threading.ThreadState.Unstarted))
                ///
                {
                    mStationThread.Start();
                    ///
                    StationNetWorkConnect();
                    ///
                    StationResetEvent.Set();
                }
                else if (mStationThread.ThreadState == (System.Threading.ThreadState.Background | System.Threading.ThreadState.WaitSleepJoin))
                {
                    ///
                    StationResetEvent.Set();
                }

            }
            catch (Exception)
            {

            }
        }
        /// <summary>
        /// 
        /// </summary>
        public void Station_Stop()
        {
            PLC.PLC_Manager.SetStatusProcess(Q02UCDP_MemConfig_Tray["SetStatusProcess"], false);
            ///
            StationResetEvent.Reset();        
        }
       
        /// <summary>
        /// 
        /// </summary>
        public bool ResetState()
        {
            Console.WriteLine("Reset State machine");
            ///
            Station_Stop();
            ///
            Thread.Sleep(100);
            ///
            PLC.PLC_Manager.SetConfRead2DCode(Q02UCDP_MemConfig_Tray["SetConfRead2DCode"], false);
            ///
            PLC.PLC_Manager.SetResultPartToPLC(Q02UCDP_MemConfig_Tray["SetResultPartToPLC"], false);
            ///
            return  (mStationThread.ThreadState == System.Threading.ThreadState.Stopped) ? true : false;
        }
        /// <summary>
        /// 
        /// </summary>
        public void ReadCsv()
        {
   
            PLC.PLC_Manager.SetCsvFileNameDownloadToPLC("W400", "jeerawat preecharnuk",10);
            ///
            PLC.PLC_Manager.SetCsvFileNameUploadToPLC("W420","", 20);
            ///      
            PLC.PLC_Manager.SetCsvFileNameDownloadToPLC("W440", "", 10);
            ///
            PLC.PLC_Manager.SetCsvFileNameUploadToPLC("W460", "", 20);
        }
        public void DumpCsv()
        {
            DumpResultParts(mLogsManager);
        }
        public void ThreadStation_Stop() => mStationThread.Abort();
        /// <summary>
        /// 
        /// </summary>
        public void ThreadStation_Exit() => Environment.Exit(0);
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
    }
}
