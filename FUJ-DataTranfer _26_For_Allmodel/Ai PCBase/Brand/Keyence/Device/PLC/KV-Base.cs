using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using AxDATABUILDERAXLibLB;
using DATABUILDERAXLibLB;

namespace Ai_PCInterface.Brand.Keyence.Device.PLC
{
    public class KV_Base : IKV_Base
    {
        public bool Enable { set; get; } = true;
        /// <summary>
        /// 
        /// </summary>
        public string IPAddress { set; get; } = "192.168.0.10";
        /// <summary>
        /// 
        /// </summary>
        protected KV_Base() {}
        /// <summary>
        /// 
        /// </summary>
        private Dictionary<string, string> mStationResult = new Dictionary<string, string>();
        /// <summary>
        /// 
        /// </summary>
        private  KV_Convert.CVSetToValve mCVSetToValves;

        private  KV_Convert.CVGetToValve mCVGetToValues;
        /// <summary>
        /// 
        /// </summary>
        public Dictionary<string, string> StationResult
        {
            get{ return (mStationResult == null) ? new Dictionary<string, string>() : mStationResult;}
            set{ mStationResult = value;}
        }
        /// <summary>
        /// 
        /// </summary>
        public KV_Convert.CVSetToValve CVSetToValves
        {
            get { return (mCVSetToValves == null) ? new KV_Convert.CVSetToValve() : mCVSetToValves; }
            set { mCVSetToValves = value; }
        }
        /// <summary>
        /// 
        /// </summary>
        public KV_Convert.CVGetToValve CVGetToValves
        {
            get { return (mCVGetToValues == null) ? new KV_Convert.CVGetToValve() : mCVGetToValues; }
            set { mCVGetToValues = value; }
        }
        /// 
        /// </summary>
        protected virtual void InitializeIDReferences()
        {
            InitializeStation();
        }
        public virtual void InitializeStation()
        {

        }
    }
    public class CommManager : KV_Base
    {
        /// <summary>
        /// 
        /// </summary>
        public AxDBCommManager AxDBCommManager { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public DBPlcDevice DBPlcDevice { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public CommManager():base()
        {
            
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="AxDBDeviceManager"></param>
        public CommManager(AxDATABUILDERAXLibLB.AxDBCommManager AxDBCommManager)
        {
          
            if (!AxDBCommManager.Active || AxDBCommManager == null)
                return;
            this.AxDBCommManager = AxDBCommManager;

        }
        /// <summary>
        /// 
        /// </summary>
        public KV_Define.KV_Status WriteDevice(string address, int ivalue)
        {

            try
            {
                if (!this.AxDBCommManager.Active || this.AxDBCommManager == null)
                    throw new KV_COMException(" KV_Define.KV_State WriteDevice: => object is null or paramiter not value");


                this.AxDBCommManager.WriteDevice(DBPlcDevice, address, ivalue);
                return new KV_Define.KV_Status() {/* KV_ErrorPLC = KV_Define.KV_Error.PLC_NO_ERROR*/ };
            }
            catch (KV_COMException PLCEx)
            {

                return new KV_Define.KV_Status() {/* KV_ErrorPLC = KV_Define.KV_Error.PLC_DBCOMMANAGER_ERR, PLCExceptionMss = PLCEx*/ };
            }
            catch (Exception Ex)
            {
                return new KV_Define.KV_Status() { /*KV_ErrorPLC = KV_Define.KV_Error.PLC_WRITEDEVICE_ERR, ExceptionMss = Ex */};
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="strNo"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public KV_Define.KV_Status WriteText(string address ="0", int lenght = 0,string strValue = "")
        {
            try
            {
                if (!this.AxDBCommManager.Active || this.AxDBCommManager == null )
                    throw new KV_COMException("KV_Define.KV_State ReadText: => object is null or paramiter not value");

                this.AxDBCommManager.WriteText(DBPlcDevice, address, lenght, strValue);
                return new KV_Define.KV_Status() { /*KV_ErrorPLC = KV_Define.KV_Error.PLC_NO_ERROR*/ };
            }
            catch (KV_COMException PLCEx)
            {

                return new KV_Define.KV_Status() { /*KV_ErrorPLC = KV_Define.KV_Error.PLC_DBCOMMANAGER_ERR, PLCExceptionMss = PLCEx */};
            }
            catch (Exception Ex)
            {
                return new KV_Define.KV_Status() { /*KV_ErrorPLC = KV_Define.KV_Error.PLC_WRITEDEVICE_ERR, ExceptionMss = Ex */};
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public KV_Define.KV_Status ReadDevice(string address ,out int value)
        {
            value = 0;
            try
            {
                if (!this.AxDBCommManager.Active || this.AxDBCommManager == null)
                    throw new KV_COMException("KV_Define.KV_State ReadDevice: => object is null or paramiter not value");

                value = this.AxDBCommManager.ReadDevice(DBPlcDevice, address);
                return new KV_Define.KV_Status() { /*KV_ErrorPLC = KV_Define.KV_Error.PLC_NO_ERROR*/ };
            }
            catch (KV_COMException PLCEx)
            {

                return new KV_Define.KV_Status() { /*KV_ErrorPLC = KV_Define.KV_Error.PLC_DBCOMMANAGER_ERR, PLCExceptionMss = PLCEx*/ };
            }
            catch (Exception Ex)
            {
                return new KV_Define.KV_Status() { /*KV_ErrorPLC = KV_Define.KV_Error.PLC_WRITEDEVICE_ERR, ExceptionMss = Ex*/ };
            }
        }
        public KV_Define.KV_Status ReadText(string address, int lenght, out string strout)
        {
            strout = string.Empty;
            try
            {
                if (!this.AxDBCommManager.Active || this.AxDBCommManager == null || !ExtensionMethods.IsNumeric(address))
                    throw new KV_COMException("KV_Define.KV_State ReadText: => object is null or paramiter not value");


                this.AxDBCommManager.ReadText(DBPlcDevice, address, lenght, out strout);
                return new KV_Define.KV_Status() { /*KV_ErrorPLC = KV_Define.KV_Error.PLC_NO_ERROR*/ };
            }
            catch (KV_COMException PLCEx)
            {

                return new KV_Define.KV_Status() { /*KV_ErrorPLC = KV_Define.KV_Error.PLC_DBCOMMANAGER_ERR, PLCExceptionMss = PLCEx */};
            }
            catch (Exception Ex)
            {
                return new KV_Define.KV_Status() { /*KV_ErrorPLC = KV_Define.KV_Error.PLC_WRITEDEVICE_ERR, ExceptionMss = Ex*/ };
            }
        }
    }
    public static class ExtensionMethods
    {
        /// <summary>
        /// Returns true if string could represent a valid number, including decimals and local culture symbols
        /// </summary>
        public static bool IsNumeric(this string s)
        {
            decimal d;
            return decimal.TryParse(s, System.Globalization.NumberStyles.Any, System.Globalization.CultureInfo.CurrentCulture, out d);
        }

        /// <summary>
        /// Returns true only if string is wholy comprised of numerical digits
        /// </summary>
        public static bool IsNumbersOnly(this string s)
        {
            if (s == null || s == string.Empty)
                return false;

            foreach (char c in s)
            {
                if (c < '0' || c > '9') // Avoid using .IsDigit or .IsNumeric as they will return true for other characters
                    return false;
            }

            return true;
        }
    }
}