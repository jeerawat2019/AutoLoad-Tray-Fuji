using FUJ_DataTranfer.State_Machine.Station;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using ActUtlTypeLib;
using PCBase_Interface.Brand.Misubishi.Divice.PLC;
using System.Timers;
using FUJ_DataTranfer.PLC;
using System.Net.NetworkInformation;

namespace FUJ_DataTranfer
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private PickAndPlace mDataBuilderTray_R, mDataBuilderTray_L = null;
        /// <summary>
        /// 
        /// </summary>
        private PLC_Commu mPLC_Commu = null;
        /// <summary>
        /// 
        /// </summary>
        public iServer iServer = null;
        /// <summary>
        /// 
        /// </summary>
        public System.Timers.Timer tMain = null;
        /// <summary>
        /// 
        /// </summary>
        ///private Dictionary<string, string> Q02UCDP_MemConfig_Tray1 = null;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_Load(object sender, EventArgs e) 
        {
            SetComport();
            ///
            ///
            mDataBuilderTray_R = new PickAndPlace("Data Builder Tray Right");
            ///
            mDataBuilderTray_R.PropertyChanged += MDataBuilderTray_R_PropertyChanged;
            /////
            mDataBuilderTray_R.PLC_Commu = mPLC_Commu;
            ///
            //mDataBuilderTray_R.IService = iServer;
            ///
            SetMemConfigTray_R();
            ///
            /// MODULE ROBOT LEFT
            ///
            mDataBuilderTray_L = new PickAndPlace("Data Builder Tray Left");
            ///
            mDataBuilderTray_L.PropertyChanged += MDataBuilderTray_L_PropertyChanged;
            /////
            mDataBuilderTray_L.PLC_Commu = mPLC_Commu;
            ///
            SetMemConfigTray_L();
            ///
            PLC.PLC_Manager.IsPlcConnect = (PLC_Builder.AxActOpen(mDataBuilderTray_L.PLC_Commu) == 0) ? true : false;
            ///
            tMain = new System.Timers.Timer(TimeSpan.FromMinutes(0.01).TotalMilliseconds); // set the time (5 min in this case)
            ///
            tMain.AutoReset = true;
            ///
            tMain.Elapsed += new System.Timers.ElapsedEventHandler(MainProcess_Starting);
            /////
            tMain.Start();
            /// <summary>
            /// 
            /// </summary>
            iServer = new iServer();
            ///
            iServiceConfig();
            ///
            SetEnableComponent();
            ///
            rthStop_R.BackColor = Color.GreenYellow;
            /////
            rthStop_L.BackColor = Color.GreenYellow;
            ///
            //mDataBuilderTray_R.InputResultPath = mDataBuilderTray_L.InputResultPath = string.Format("\\192.168.201.1\avi\RGOZ-888-test\IntegrateDataToPLC");//@"C:\InputTrayResult";
            /////
            //mDataBuilderTray_R.OutputResultPath = mDataBuilderTray_L.OutputResultPath = string.Format(@"\\192.168.201.1\avi\RGOZ-888-test\IntegrateData");//@"C:\OutputTrayResult";//IntegrateData
            ///
            txtDisHost.Text = txtHostName.Text;
            ///
            LoadConfig();
        }

        private void LoadConfig()
        {
            FUJ_DataTranfer.Properties.Settings.Default.Reload();
            ///
            txtHostName.Text = FUJ_DataTranfer.Properties.Settings.Default.HostName;
            ///
            txtSubPath.Text = FUJ_DataTranfer.Properties.Settings.Default.PathInput;
            ///
            txtSubFolderInput.Text = FUJ_DataTranfer.Properties.Settings.Default.SubFolderInput;
            ///
            txtSubPathOut.Text = FUJ_DataTranfer.Properties.Settings.Default.PathOutput;
            ///
            txtSubPathFolderout.Text = FUJ_DataTranfer.Properties.Settings.Default.SubFolderOutput;
            ///
            chbBypassPartView.Checked = FUJ_DataTranfer.Properties.Settings.Default.ByPassPartView;
            ///
            chbByPassFileName.Checked = FUJ_DataTranfer.Properties.Settings.Default.ByPassFileNameDelete;
            ///
            chbByPassServer.Checked = FUJ_DataTranfer.Properties.Settings.Default.ByPassServer;
            ///
            txtIP_Machine.Text = FUJ_DataTranfer.Properties.Settings.Default.IpMachine;
            /// Save Position Data In Tray
            chk_Shot1.Checked=FUJ_DataTranfer.Properties.Settings.Default.SelectShot1;
            ///
            chk_Shot2.Checked=FUJ_DataTranfer.Properties.Settings.Default.SelectShot2;
            ///
            chk_Shot3.Checked=FUJ_DataTranfer.Properties.Settings.Default.SelectShot3;
            ///
            txt_Shot1.Text = FUJ_DataTranfer.Properties.Settings.Default.NumberPosition_Shot1;
            ///
            txt_Shot2.Text = FUJ_DataTranfer.Properties.Settings.Default.NumberPosition_Shot2;
            ///
            Txt_Shot3.Text = FUJ_DataTranfer.Properties.Settings.Default.NumberPosition_Shot3;
            ///
            txt_IndexData.Text =FUJ_DataTranfer.Properties.Settings.Default.IndexData;
            ///
            FUJ_DataTranfer.Properties.Settings.Default.Save();

        }


        /// <summary>
        /// 
        /// </summary>
        private void SetEnableComponent()
        {
            txtHostName.Enabled = true;
            txtLog_On.Enabled = false;
            txtNumPart.Enabled = false;
            txtServer_Name.Enabled = false;
            txtPartModel.Enabled = false;
            txtPartName.Enabled = false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MDataBuilderTray_L_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            try {
                var result = (PickAndPlace)sender;
                ///
                mPartTray_L = new Ai_Product.Ingredient.Model.Tray();
                ///
                if (result.mInterLockStateDisplay == false) {
                    ///
                    if (result.PStateMC == PickAndPlace.StateMC.SetResultToPLC) {

                        if (chbBypassPartView.Checked == false) {
                            ///
                            partTary_L.ClearPartTrayComponent();
                            ///
                            partTary_L.CreateTrayComponent(mPartTray_L, "InputTray2", result.GetPartListCsv);
                            ///
                        }
                    }
                    ///
                    if (result.PStateMC == PickAndPlace.StateMC.SetResultToPLC) {
                        ///
                        partTary_L.AddResult2DCodeData(result.mList2DCodeFormPLC);
                        ///
                        result.mInterLockStateDisplay = true;
                    }
                    
                }
                if(result.PStateMC == PickAndPlace.StateMC.IsTrayInPosition) {
                    ///
                    partTary_L.ClearPart2DCode();
                }
                DisplayStatusError(result.Error);
                ///
                UpdateStateMachine_L(result);
                ///
                MachineDisplayState_L(result.PStateMC);

            }
            catch (Exception) {

                throw;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="pStateMC"></param>
        private void MachineDisplayState_L(PickAndPlace.StateMC pStateMC)
        {
            if (this.InvokeRequired) {
                this.Invoke(new MethodInvoker(() => { MachineDisplayState_L(pStateMC); }));
            }
            else {
                tslStateDisplay_L.Text = Ai_PCSystem.Strings.Str_Enum.StringEnum.GetStringValue(pStateMC);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private void SetMemConfigTray_L()
        {
            mDataBuilderTray_L.Q02UCDP_MemConfig_Tray = new Dictionary<string, string>()
            {
                { "IsData2DReady","D8500" },//1Word
                { "IsConfSetResultPartToPLC","D8501" },//1Word
                { "IsTrayInPosition","D8502" },//1Word
                { "IsTrayOutPosition","D8503" },//1Word

                { "SetConfRead2DCode","D8510" },//1Word
                { "SetResultPartToPLC","D8511" },//1Word
                //{ "PC_LinkCommu","D8612" },//1Word
                { "SetPCErrorToPLC","D8765"},
                { "PCWriteDataResultToPLC","D8600"},//49Word
                { "GetDataResultFormPLC","D8800" },//49Word
                { "GetData2DCode","ZR97000" },//450Word GetData2DCode TEST  R1000 REAL R101000

                { "SetNameFileDownLoad_L","W400"},//10 Word
                { "SetNameFileUpLoad_L","W420"},//20 Word
                { "SetStatusProcess","D8767" }
            };
        }
        private Dictionary<string, string> MemConfig_Product = new Dictionary<string, string>()
        {
                { "GetPartTotal","D8760"},//1Word 
                { "GetPartModel","D8761"},//20Word
               
        };
        private Dictionary<string, string> MemConfig_Machine = new Dictionary<string, string>()
        {
                {"PC_LinkCommu","D8762" },//1Word
                //{"GetPLCStatusAutoRun","D8763" },//1Word
                { "GetPLCStatusAutoRun_L","D8763" },//1Word
                { "SetConfGetStatusMachine_L","D8764" },//1Word
                { "GetPLCStatusAutoRun_R","D18763" },//1Word  TEST D2001 REAL D18763
                { "SetConfGetStatusMachine_R","D18764" },//1Word TEST D1000 REAL 18764 
                
               
              
        };
        /// <summary>
        /// 
        /// </summary>
        private void SetMemConfigTray_R()
        {
            mDataBuilderTray_R.Q02UCDP_MemConfig_Tray = new Dictionary<string, string>()
            {
                { "IsData2DReady","D18500" },//1Word TEST D5000 REAL 18500
                { "IsConfSetResultPartToPLC","D18501" },//1Word
                { "IsTrayInPosition","D18502" },//1Word
                { "IsTrayOutPosition","D18503" },//1Word

                { "SetConfRead2DCode","D18510" },//1Word TEST D5500 REAL 18510 
                { "SetResultPartToPLC","D18511" },//1Word
                //{ "PC_LinkCommu","D612" },//1Word
                { "SetPCErrorToPLC","D18765"},
                { "PCWriteDataResultToPLC","D18600"},//49Word
                { "GetDataResultFormPLC","D18800" },//49Word
                { "GetData2DCode","ZR97600" },//450Word  TEST R1000 GetData2DCode REAL R104000 //600 Word ZR97600
                
                { "SetNameFileDownLoad_R","W440"},//10 Word
                { "SetNameFileUpLoad_R","W460"},//20 Word
                { "SetStatusProcess","D18767" }
            };
        }

        /// <summary>
        /// 
        /// </summary>
        private void SetComport()
        {
            mPLC_Commu = new PLC_Commu("Q02CPU", "PLC Data Builder");
            mPLC_Commu.Id = "1";
            mPLC_Commu.IPAddress = FUJ_DataTranfer.Properties.Settings.Default.IpMachine;
            //mPLC_Commu.IPAddress = "192.168.0.70";
            mPLC_Commu.mActSupportMsgClass = new ActSupportMsgLib.ActSupportMsgClass();
            mPLC_Commu.mActUtlTypeClass = new ActUtlTypeLib.ActUtlTypeClass();
            mPLC_Commu.iStstionNumber = 5;
            //mPLC_Commu.iStstionNumber = 6;
            mPLC_Commu.mActUtlTypeClass.ActLogicalStationNumber = 0;
        }
        /// <summary>
        /// 
        /// </summary>
        private void iServiceConfig()
        {
            //iServer.HostNameByIP = txtHostName.Text;
            //iServer.Password = txtLogOn.Text;
            //iServer.SubFolderInput = txtSubFolderInput.Text;
            //iServer.SubFolderOutput = txttxtSubFolderOutput.Text;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MDataBuilderTray_R_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            try {
                var result = (PickAndPlace)sender;
                ///
                mPartTray_R = new Ai_Product.Ingredient.Model.Tray();
                ///
                if (result.mInterLockStateDisplay == false) {
                    ///
                    if (result.PStateMC == PickAndPlace.StateMC.SetResultToPLC) {
                        ///
                        if (chbBypassPartView.Checked == false) {
                            ///
                            partTary_R.ClearPartTrayComponent();
                            ///
                            partTary_R.CreateTrayComponent(mPartTray_R, "InputTray1", result.GetPartListCsv);
                            ///
                        }
                    }
                    ///
                    if (result.PStateMC == PickAndPlace.StateMC.SetResultToPLC) {
                        ///partTary_R.ClearPart2DCode();
                        ///
                        partTary_R.AddResult2DCodeData(result.mList2DCodeFormPLC);
                        ///
                        result.mInterLockStateDisplay = true;
                    }
                  
                }
                if (result.PStateMC == PickAndPlace.StateMC.IsTrayInPosition) {//StationInitial
                    ///
                    partTary_R.ClearPart2DCode( );
                }
                DisplayStatusError(result.Error);
                /////
                UpdateStateMachine_R(result);
                ///
                MachineDisplayState_R(result.PStateMC);
            }
            catch (Exception) {

                throw;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="pStateMC"></param>
        private void MachineDisplayState_R(PickAndPlace.StateMC pStateMC)
        {
            if (this.InvokeRequired) {
                this.Invoke(new MethodInvoker(() => { MachineDisplayState_R(pStateMC); }));
            }
            else {
                tslStat.Text = Ai_PCSystem.Strings.Str_Enum.StringEnum.GetStringValue(pStateMC);
            }
        }
        private void DisplayStatusError(iError error)
        {
            try {
                switch (error) {
                    case iError.CSVFileNotFound:
                        DisplayDescriptionError("CSVFileNotFound");
                        break;
                    case iError.DeleteFileOnFtpServer:
                        DisplayDescriptionError("DeleteFileOnFtpServer");
                        break;
                    case iError.FirstWork2DFail:
                        DisplayDescriptionError("FirstWork2DFail");
                        break;
                    case iError.FTPDownLoadDataServer:
                        DisplayDescriptionError("FTPDownLoadDataServer");
                        break;
                    case iError.FTPUpLoadDataServer:
                        DisplayDescriptionError("FTPUpLoadDataServer");
                        break;
                    case iError.GetData2DCode:
                        DisplayDescriptionError("GetData2DCode");
                        break;
                    case iError.GetDataResultFormPLC:
                        DisplayDescriptionError("GetDataResultFormPLC");
                        break;
                    case iError.GetFileList:
                        DisplayDescriptionError("GetFileList");
                        break;
                    case iError.GetPartModel:
                        DisplayDescriptionError("GetPartModel");
                        break;
                    case iError.GetPartTotal:
                        DisplayDescriptionError("GetPartTotal");
                        break;
                    case iError.InputParameterNull:
                        DisplayDescriptionError("InputParameterNull");
                        break;
                    case iError.IsConfSetResultPartToPLC:
                        DisplayDescriptionError("IsConfSetResultPartToPLC");
                        break;
                    case iError.IsData2DReady:
                        DisplayDescriptionError("IsData2DReady");
                        break;
                    case iError.IsPlcConnect:
                        DisplayDescriptionError("IsPlcConnect");
                        break;
                    case iError.IsTrayInPosition:
                        DisplayDescriptionError("IsTrayInPosition");
                        break;
                    case iError.IsTrayOutPosition:
                        DisplayDescriptionError("IsTrayOutPosition");
                        break;
                    case iError.Normal:
                        DisplayDescriptionError("Normal");
                        break;
                    case iError.PCWriteDataResultToPLC:
                        DisplayDescriptionError("PCWriteDataResultToPLC");
                        break;
                    case iError.PC_LinkCommu:
                        DisplayDescriptionError("PC_LinkCommu");
                        break;
                    case iError.SetConfRead2DCode:
                        DisplayDescriptionError("SetConfRead2DCode");
                        break;
                    case iError.SetResultPartToPLC:
                        DisplayDescriptionError("SetResultPartToPLC");
                        break;
                }
            }
            catch (Exception) {

                throw;
            }

        }
        //private delegate void _delDisplayEvent(string strParam);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="description"></param>
        public void DisplayDescriptionError(string description)
        {
            try {
                if (this.InvokeRequired) {
                    this.Invoke(new MethodInvoker(() => { DisplayDescriptionError(description); }));
                }
                else {
                    tslErrorDesc.Text = description;
                }
            }
            catch (Exception) {
                throw;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="result"></param>
        private void UpdateStateMachine_R(PickAndPlace result)
        {
            try {
                if (this.InvokeRequired) {
                    this.Invoke(new MethodInvoker(() => { UpdateStateMachine_R(result); }));
                }
                else {

                    switch (result.PStateMC) {
                        case PickAndPlace.StateMC.StationInitial:
                            rthStop_R.BackColor = Color.GreenYellow;
                            rthStop_R.Refresh();
                            break;
                        case PickAndPlace.StateMC.IsTrayInPosition:
                            rthTrayInPos_R.BackColor = Color.GreenYellow;
                            rthTrayInPos_R.Refresh();
                            break;
                        case PickAndPlace.StateMC.ReadData2DCode:
                            rthRead2DCode_R.BackColor = Color.GreenYellow;
                            rthRead2DCode_R.Refresh();
                            break;
                        case PickAndPlace.StateMC.SetResultToPLC:
                            rthTrayOutPos_R.BackColor = Color.GreenYellow;
                            rthTrayOutPos_R.Refresh();
                            break;
                        case PickAndPlace.StateMC.IsTrayOutPosition:

                            Res_R_StateDisplay();
                            break;
                        default:

                            break;
                    }
                    //case : 
                }
            }
            catch (Exception) {

                throw;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private void Res_R_StateDisplay()
        {
            try {
                if (this.InvokeRequired) {
                    this.Invoke(new MethodInvoker(() => { Res_R_StateDisplay(); }));
                }
                else {

                    rthStop_R.BackColor = Color.GreenYellow;
                    rthDumpResult_R.BackColor = Color.White;
                    rthRead2DCode_R.BackColor = Color.White;
                    rthTrayInPos_R.BackColor = Color.White;
                    rthTrayOutPos_R.BackColor = Color.White;
                }
            }
            catch (Exception) {

                throw;
            }
        }
        //private delegate void _delUpdateStateMachine(PickAndPlace value);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="result"></param>
        private void UpdateStateMachine_L(PickAndPlace result)
        {
            try {
                if (this.InvokeRequired) {
                    this.Invoke(new MethodInvoker(() => { UpdateStateMachine_L(result); }));
                }
                else {

                    switch (result.PStateMC) {
                        case PickAndPlace.StateMC.StationInitial:
                            rthStop_L.BackColor = Color.GreenYellow;
                            rthStop_L.Refresh();
                            break;
                        case PickAndPlace.StateMC.IsTrayInPosition:
                            rthTrayInPos_L.BackColor = Color.GreenYellow;
                            rthTrayInPos_L.Refresh();
                            break;
                        case PickAndPlace.StateMC.ReadData2DCode:
                            rthRead2DCode_L.BackColor = Color.GreenYellow;
                            rthRead2DCode_L.Refresh();
                            break;
                        case PickAndPlace.StateMC.SetResultToPLC:
                            rthTrayOutPos_L.BackColor = Color.GreenYellow;
                            rthTrayOutPos_L.Refresh();
                            break;
                        case PickAndPlace.StateMC.IsTrayOutPosition:

                            Res_L_StateDisplay();
                            break;
                        default:

                            break;
                    }
                }
            }
            catch (Exception) {

                throw;
            }
                //case : 
            
        }
        private delegate void _delRes_StateDisplay();
        /// <summary>
        /// 
        /// </summary>
        private void Res_L_StateDisplay()
        {
            try {
                if (this.InvokeRequired) {
                    this.Invoke(new MethodInvoker(() => { Res_L_StateDisplay(); }));
                }
                else {

                    rthStop_L.BackColor = Color.GreenYellow;
                    rthDumpResult_L.BackColor = Color.White;
                    rthRead2DCode_L.BackColor = Color.White;
                    rthTrayInPos_L.BackColor = Color.White;
                    rthTrayOutPos_L.BackColor = Color.White;
                }
            }
            catch (Exception) {

                throw;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        Ai_Product.Ingredient.Model.Tray mPartTray_R, mPartTray_L = null;

        private void BtnUpLoad_Click(object sender, EventArgs e)
        {
            //mDataBuilderTray_L.IServer = iServer;
            //Sample
            //mDataBuilderTray_L.UpLoadFileCsV("THA939318D221286Z");

            //SetPathCsvFile();
            //else (EnableServer == True)Get Sever by Notebook
            //mDataBuilderTray_L.ReadCsv();
            //mDataBuilderTray_R.ReadCsv();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnDownLoad_Click(object sender, EventArgs e)
        {
            //mDataBuilderTray_L.IServer = iServer;
            //Sample
            //mDataBuilderTray_L.DownLoadFileCsV("THA939318D221286Z");
         
            //SetPathCsvFile();
            //else (EnableServer == True)Get Sever by Notebook
            //mDataBuilderTray_L.DumpCsv();
            //mDataBuilderTray_R.DumpCsv();
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnResModuleR_Click(object sender, EventArgs e)
        {
            try {
                if (mDataBuilderTray_R.ResetState()) {

                    btnStartTray_R.BackColor = Color.Transparent;
                    btnResModuleR.BackColor = Color.GreenYellow;
                }
                ///
                Res_R_StateDisplay();
            }
            catch (Exception) {

                throw;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnResModuleL_Click(object sender, EventArgs e)
        {
            try {

                if (mDataBuilderTray_L.ResetState()) {

                    btnStartTray_L.BackColor = Color.Transparent;
                    btnResModuleL.BackColor = Color.GreenYellow;
                }
                ///
                Res_L_StateDisplay();
            }
            catch (Exception) {

                throw;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MainProcess_Starting(object sender, EventArgs e)
        {
            try {

                if (this.InvokeRequired) {
                    this.BeginInvoke(new EventHandler(MainProcess_Starting), new object[] { sender, e });
                    ///
                    return;
                }
                if (string.IsNullOrEmpty(txtHostName.Text.Trim()))
                    return;
                ///
                if (!PingNetwork(txtHostName.Text.Trim())) {
                    //
                    //Addition condition alarm code
                    //
                    tMain.Stop();
                    //
                    MessageBox.Show("Ping IP Server not found!", "Error !"); Application.Exit();
                    //
                }
                ///
                tslDateTime.Text = DateTime.Now.ToString("dddd, dd MMMM yyyy HH:mm:ss");
                ///
                PLC.PLC_Manager.PC_LinkCommu(MemConfig_Machine["PC_LinkCommu"]);
                ///
                //StartAuto_Module_L(sender, e);
                ///
                //StartAuto_Module_R(sender, e);
            }
            catch (Exception ex) {

                tMain.Stop();
                //
                MessageBox.Show("Ping IP Server not found!", "Error !");Application.Exit();
            }
        }
        private bool PingNetwork(string ip)
        {
            if (string.IsNullOrEmpty(ip))
                return false;
            Ping ping = new Ping();
            ///
            PingReply pingresult = ping.Send(ip);
            ///
            return (pingresult.Status.ToString() == "Success") ? true : false;
        }
        /// <summary>
        /// /
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void StartAuto_Module_L(object sender, EventArgs e)
        {
            try {
            //    ///
                string status = null;                    
                PLC.PLC_Manager.GetConfStatusMode(MemConfig_Machine["SetConfGetStatusMachine_L"], out status);
                if (status == "1")
                {
                    mDataBuilderTray_L.ResetState();
                    System.Threading.Thread.Sleep(100);
                    BtnStartTray_L_Click(sender, e);
                    while (status != "0")
                    {
                        PLC.PLC_Manager.GetConfStatusMode(MemConfig_Machine["SetConfGetStatusMachine_L"], out status);
                    }

                }
            }
            catch (Exception) {
                throw;
            }
        }
        private void StartAuto_Module_R(object sender, EventArgs e)
        {
            try {
                //
                string status = null;
                //                           
                PLC.PLC_Manager.GetConfStatusMode(MemConfig_Machine["SetConfGetStatusMachine_R"], out status);
              
                //if (status == "1")
                //{
                    mDataBuilderTray_R.ResetState();
                    System.Threading.Thread.Sleep(100);
                    BtnStartTray_R_Click(sender, e);
                    while (status != "0") {
                        PLC.PLC_Manager.GetConfStatusMode(MemConfig_Machine["SetConfGetStatusMachine_R"], out status);
                    }
                   
                //}
           
            }
            catch (Exception) {

                throw;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnStartTray_L_Click(object sender, EventArgs e)
        {
            try {
                ///
                SetPathCsvFile();
                ///
                if (PLC.PLC_Manager.IsPlcConnect) {
                    ///
                    {
                        string[] partmodel = null;
                        ///
                        string parttotal = null;
                        ///
                        PLC.iError Error = PLC.PLC_Manager.GetPartModel(MemConfig_Product["GetPartModel"], out partmodel);
                        ///
                        Error = PLC.PLC_Manager.GetPartTotal(MemConfig_Product["GetPartTotal"], out parttotal);
                        ///
                        DisplayPartModel(partmodel[0], parttotal);
                        ///
                        FUJ_DataTranfer.Properties.Settings.Default.Save();
                        ///
                        mDataBuilderTray_L.IServer = iServer;
                        ///
                        mDataBuilderTray_L.ResetState();
                        ///
                        mDataBuilderTray_L.ThreadStation_Start();
                        ///
                        PLC.PLC_Manager.SetConfStatusAuto(MemConfig_Machine["SetConfGetStatusMachine_L"], true);
                        ///
                        btnStartTray_L.BackColor = Color.GreenYellow;
                        ///
                        btnResModuleL.BackColor = Color.Transparent;
                    }
                }
                else
                    MessageBox.Show("", "");
            }
            catch (Exception) {

                throw;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chbByPassServer_CheckedChanged(object sender, EventArgs e)
        {
            if (this.InvokeRequired)
            {
                this.BeginInvoke(new EventHandler(MainProcess_Starting), new object[] { sender, e });
                ///
                return;
            }
            ///
            mDataBuilderTray_R.BypassServer = mDataBuilderTray_L.BypassServer = (chbByPassServer.Checked) ? true :false;
            ///

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chbByPassFileName_CheckedChanged(object sender, EventArgs e)
        {
            if (this.InvokeRequired)
            {
                this.BeginInvoke(new EventHandler(MainProcess_Starting), new object[] { sender, e });
                ///
                return;
            }
            ///
            mDataBuilderTray_R.BypassDeleteFileName = mDataBuilderTray_L.BypassDeleteFileName = (chbByPassFileName.Checked) ? true : false;
        }
        //private delegate void _delDisplayPartModel(string value1, string value2);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="partmodel"></param>
        /// <param name="parttotol"></param>
        private void DisplayPartModel(string partmodel,string parttotol)
        {
            try {
                if (this.InvokeRequired) {
                    this.Invoke(new MethodInvoker(() => { DisplayPartModel(partmodel, parttotol); }));
                }
                else {

                    txtPartModel.Text = parttotol;
                    ///
                    txtNumPart.Text = partmodel;
                    ///
                    //FUJ_DataTranfer.Properties.Settings.Default.IndexData = partmodel;
                    //txt_IndexData.Text = partmodel;
                }
            }
            catch (Exception) {

                throw;
            }
        }
        public bool StateRestartProgram { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnSaveConfig_Click(object sender, EventArgs e)
        {
            if (chk_Shot1.CheckState == CheckState.Checked && txt_Shot1.Text == "" && chk_Shot3.CheckState == CheckState.Checked && Txt_Shot3.Text == "")
            {
                MessageBox.Show("Please input Your Position remove in Tray Shot1 && Shot2 ", "Caption");
                StateRestartProgram = false;
            }
            else
            {
                
                if (chk_Shot1.CheckState == CheckState.Checked && txt_Shot1.Text == "")
                {
                MessageBox.Show("Please input Your Position remove in Tray Shot1 ", "Caption");
                StateRestartProgram = false;
                }
                else
                {
                    FUJ_DataTranfer.Properties.Settings.Default.SelectShot1 = chk_Shot1.Checked;
                    ///
                    FUJ_DataTranfer.Properties.Settings.Default.NumberPosition_Shot1 = txt_Shot1.Text;
                    StateRestartProgram = true;
                }
                if (chk_Shot3.CheckState == CheckState.Checked && Txt_Shot3.Text == "")
                {
                    MessageBox.Show("Please input Your Position remove  Position Tray", "Caption");
                    StateRestartProgram = false;
                }
                else
                {
                    FUJ_DataTranfer.Properties.Settings.Default.SelectShot3 = chk_Shot3.Checked;
                    ///
                    FUJ_DataTranfer.Properties.Settings.Default.NumberPosition_Shot3 = Txt_Shot3.Text;
                    StateRestartProgram = true;
                }

            }
            ///
            FUJ_DataTranfer.Properties.Settings.Default.HostName = txtHostName.Text;
            ///
            FUJ_DataTranfer.Properties.Settings.Default.PathInput = txtSubPath.Text;
            ///
            FUJ_DataTranfer.Properties.Settings.Default.SubFolderInput = txtSubFolderInput.Text;
            ///
            FUJ_DataTranfer.Properties.Settings.Default.PathOutput = txtSubPathOut.Text;
            ///
            FUJ_DataTranfer.Properties.Settings.Default.SubFolderOutput = txtSubPathFolderout.Text;
            ///
            FUJ_DataTranfer.Properties.Settings.Default.IpMachine = txtIP_Machine.Text;
            ///
            FUJ_DataTranfer.Properties.Settings.Default.ByPassPartView = chbBypassPartView.Checked;
            ///
            FUJ_DataTranfer.Properties.Settings.Default.ByPassFileNameDelete = chbByPassFileName.Checked;
            ///
            FUJ_DataTranfer.Properties.Settings.Default.ByPassServer = chbByPassServer.Checked;
            ///
            /// Save Position Data In Tray
            /// 
            FUJ_DataTranfer.Properties.Settings.Default.SelectShot2 = chk_Shot2.Checked;
            ///
            FUJ_DataTranfer.Properties.Settings.Default.NumberPosition_Shot2 = txt_Shot2.Text;
            ///
            FUJ_DataTranfer.Properties.Settings.Default.IndexData = txt_IndexData.Text;
            ///         
            
            ///
            if (StateRestartProgram == true) {
                FUJ_DataTranfer.Properties.Settings.Default.Save();

                MessageBox.Show("Confirm Save..", "Caption");
                MessageBox.Show("Program Restart Automatic Plese Wait.....", "Caption");
                Application.Restart(); }
            

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            mDataBuilderTray_R.ThreadStation_Exit();
            ///
            mDataBuilderTray_L.ThreadStation_Exit();
            ///
            FUJ_DataTranfer.Properties.Settings.Default.Save();
        }

        private void chk_Shot1_CheckedChanged(object sender, EventArgs e)
        {
            if(chk_Shot1.CheckState == CheckState.Unchecked)
            {
                txt_Shot1.Text = "";
               
            }
        }

        private void chk_Shot3_CheckedChanged(object sender, EventArgs e)
        {
            if(chk_Shot3.CheckState == CheckState.Unchecked)
            {
                Txt_Shot3.Text = "";
            }
            
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnStartTray_R_Click(object sender, EventArgs e)
        {
            try {
                ///
                SetPathCsvFile();
                ///
                if (PLC.PLC_Manager.IsPlcConnect) {
                    ///
                    string[] partmodel = null; string parttotal = null;
                    ///
                    PLC.iError Error = PLC.PLC_Manager.GetPartModel(MemConfig_Product["GetPartModel"], out partmodel);
                    ///
                    Error = PLC.PLC_Manager.GetPartTotal(MemConfig_Product["GetPartTotal"], out parttotal);
                    ///
                    DisplayPartModel(partmodel[0], parttotal);
                    ///
                    FUJ_DataTranfer.Properties.Settings.Default.Save();
                    ///                   
                    mDataBuilderTray_R.IServer = iServer;
                    ///
                    mDataBuilderTray_R.ResetState();
                    ///
                    mDataBuilderTray_R.ThreadStation_Start(); 
                    ///
                    PLC.PLC_Manager.SetConfStatusAuto(MemConfig_Machine["SetConfGetStatusMachine_R"], true);
                    ///
                    btnStartTray_R.BackColor = Color.GreenYellow;
                    ///
                    btnResModuleR.BackColor = Color.Transparent;
                }
                else
                    MessageBox.Show("", "");

            }
            catch (Exception) {

                throw;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        
        public void SetPathCsvFile()
        {
            try {
                var PathInput = string.Empty;
                //
                var PathOutput = string.Empty;
                ///
                if (!chbByPassServer.Checked)// By Pass server = false
                {
                    ///@"\\192.168.201.1\avi\RGOZ-888-test\IntegrateDataToPLC"
                     PathInput = string.Format(@"\\{0}\{1}\{2}\", txtHostName.Text, txtSubPath.Text, txtSubFolderInput.Text);
                    ///
                     PathOutput = string.Format(@"\\{0}\{1}\{2}\", txtHostName.Text, txtSubPathOut.Text, txtSubPathFolderout.Text);
                }else

                { ///@"avi\RGOZ-888-test\IntegrateDataToPLC"
                    PathInput = string.Format(@"C:\{0}\{1}\", txtSubPath.Text, txtSubFolderInput.Text);
                    ///
                    PathOutput = string.Format(@"C:\{0}\{1}\", txtSubPathOut.Text, txtSubPathFolderout.Text);
                }
                ///
                if (!string.IsNullOrEmpty(PathInput) && !string.IsNullOrEmpty(PathOutput))
                {
                    mDataBuilderTray_R.InputResultPath = mDataBuilderTray_L.InputResultPath = PathInput;
                    ///
                    mDataBuilderTray_R.OutputResultPath = mDataBuilderTray_L.OutputResultPath = PathOutput;
                }
                else
                    MessageBox.Show("Message", "Input and Output Path Error.");
              
            }
            catch (Exception) {

                throw;
            }
        }

    }
}
