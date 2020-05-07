//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:2.0.50727.9040
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

[assembly: System.Reflection.AssemblyVersion("1.0.0.0")]
[assembly: System.Windows.Forms.AxHost.TypeLibraryTimeStamp("29/11/2561 14:28:40")]

namespace AxActUtlTypeLib {
    
    
    [System.Windows.Forms.AxHost.ClsidAttribute("{63885648-1785-41a4-82d5-c578d29e4da8}")]
    [System.ComponentModel.DesignTimeVisibleAttribute(true)]
    [System.ComponentModel.DefaultEvent("OnDeviceStatus")]
    public class AxActUtlType : System.Windows.Forms.AxHost {
        
        private ActUtlTypeLib.IActUtlType ocx;
        
        private AxActUtlTypeEventMulticaster eventMulticaster;
        
        private System.Windows.Forms.AxHost.ConnectionPointCookie cookie;
        
        public AxActUtlType() : 
                base("63885648-1785-41a4-82d5-c578d29e4da8") {
        }
        
        [System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Hidden)]
        [System.Runtime.InteropServices.DispIdAttribute(1)]
        public virtual int ActLogicalStationNumber {
            get {
                if ((this.ocx == null)) {
                    throw new System.Windows.Forms.AxHost.InvalidActiveXStateException("ActLogicalStationNumber", System.Windows.Forms.AxHost.ActiveXInvokeKind.PropertyGet);
                }
                return this.ocx.ActLogicalStationNumber;
            }
            set {
                if ((this.ocx == null)) {
                    throw new System.Windows.Forms.AxHost.InvalidActiveXStateException("ActLogicalStationNumber", System.Windows.Forms.AxHost.ActiveXInvokeKind.PropertySet);
                }
                this.ocx.ActLogicalStationNumber = value;
            }
        }
        
        [System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Hidden)]
        [System.Runtime.InteropServices.DispIdAttribute(20)]
        public virtual string ActPassword {
            get {
                if ((this.ocx == null)) {
                    throw new System.Windows.Forms.AxHost.InvalidActiveXStateException("ActPassword", System.Windows.Forms.AxHost.ActiveXInvokeKind.PropertyGet);
                }
                return this.ocx.ActPassword;
            }
            set {
                if ((this.ocx == null)) {
                    throw new System.Windows.Forms.AxHost.InvalidActiveXStateException("ActPassword", System.Windows.Forms.AxHost.ActiveXInvokeKind.PropertySet);
                }
                this.ocx.ActPassword = value;
            }
        }
        
        public event _IActUtlTypeEvents_OnDeviceStatusEventHandler OnDeviceStatus;
        
        public virtual int Open() {
            if ((this.ocx == null)) {
                throw new System.Windows.Forms.AxHost.InvalidActiveXStateException("Open", System.Windows.Forms.AxHost.ActiveXInvokeKind.MethodInvoke);
            }
            int returnValue = ((int)(this.ocx.Open()));
            return returnValue;
        }
        
        public virtual int Close() {
            if ((this.ocx == null)) {
                throw new System.Windows.Forms.AxHost.InvalidActiveXStateException("Close", System.Windows.Forms.AxHost.ActiveXInvokeKind.MethodInvoke);
            }
            int returnValue = ((int)(this.ocx.Close()));
            return returnValue;
        }
        
        public virtual int GetCpuType(out string szCpuName, out int lplCpuCode) {
            if ((this.ocx == null)) {
                throw new System.Windows.Forms.AxHost.InvalidActiveXStateException("GetCpuType", System.Windows.Forms.AxHost.ActiveXInvokeKind.MethodInvoke);
            }
            int returnValue = ((int)(this.ocx.GetCpuType(out szCpuName, out lplCpuCode)));
            return returnValue;
        }
        
        public virtual int SetCpuStatus(int lOperation) {
            if ((this.ocx == null)) {
                throw new System.Windows.Forms.AxHost.InvalidActiveXStateException("SetCpuStatus", System.Windows.Forms.AxHost.ActiveXInvokeKind.MethodInvoke);
            }
            int returnValue = ((int)(this.ocx.SetCpuStatus(lOperation)));
            return returnValue;
        }
        
        public virtual int ReadDeviceBlock(string szDevice, int lSize, out int lplData) {
            if ((this.ocx == null)) {
                throw new System.Windows.Forms.AxHost.InvalidActiveXStateException("ReadDeviceBlock", System.Windows.Forms.AxHost.ActiveXInvokeKind.MethodInvoke);
            }
            int returnValue = ((int)(this.ocx.ReadDeviceBlock(szDevice, lSize, out lplData)));
            return returnValue;
        }
        
        public virtual int WriteDeviceBlock(string szDevice, int lSize, ref int lplData) {
            if ((this.ocx == null)) {
                throw new System.Windows.Forms.AxHost.InvalidActiveXStateException("WriteDeviceBlock", System.Windows.Forms.AxHost.ActiveXInvokeKind.MethodInvoke);
            }
            int returnValue = ((int)(this.ocx.WriteDeviceBlock(szDevice, lSize, ref lplData)));
            return returnValue;
        }
        
        public virtual int ReadDeviceRandom(string szDeviceList, int lSize, out int lplData) {
            if ((this.ocx == null)) {
                throw new System.Windows.Forms.AxHost.InvalidActiveXStateException("ReadDeviceRandom", System.Windows.Forms.AxHost.ActiveXInvokeKind.MethodInvoke);
            }
            int returnValue = ((int)(this.ocx.ReadDeviceRandom(szDeviceList, lSize, out lplData)));
            return returnValue;
        }
        
        public virtual int WriteDeviceRandom(string szDeviceList, int lSize, ref int lplData) {
            if ((this.ocx == null)) {
                throw new System.Windows.Forms.AxHost.InvalidActiveXStateException("WriteDeviceRandom", System.Windows.Forms.AxHost.ActiveXInvokeKind.MethodInvoke);
            }
            int returnValue = ((int)(this.ocx.WriteDeviceRandom(szDeviceList, lSize, ref lplData)));
            return returnValue;
        }
        
        public virtual int ReadBuffer(int lStartIO, int lAddress, int lSize, out short lpsData) {
            if ((this.ocx == null)) {
                throw new System.Windows.Forms.AxHost.InvalidActiveXStateException("ReadBuffer", System.Windows.Forms.AxHost.ActiveXInvokeKind.MethodInvoke);
            }
            int returnValue = ((int)(this.ocx.ReadBuffer(lStartIO, lAddress, lSize, out lpsData)));
            return returnValue;
        }
        
        public virtual int WriteBuffer(int lStartIO, int lAddress, int lSize, ref short lpsData) {
            if ((this.ocx == null)) {
                throw new System.Windows.Forms.AxHost.InvalidActiveXStateException("WriteBuffer", System.Windows.Forms.AxHost.ActiveXInvokeKind.MethodInvoke);
            }
            int returnValue = ((int)(this.ocx.WriteBuffer(lStartIO, lAddress, lSize, ref lpsData)));
            return returnValue;
        }
        
        public virtual int GetClockData(out short lpsYear, out short lpsMonth, out short lpsDay, out short lpsDayOfWeek, out short lpsHour, out short lpsMinute, out short lpsSecond) {
            if ((this.ocx == null)) {
                throw new System.Windows.Forms.AxHost.InvalidActiveXStateException("GetClockData", System.Windows.Forms.AxHost.ActiveXInvokeKind.MethodInvoke);
            }
            int returnValue = ((int)(this.ocx.GetClockData(out lpsYear, out lpsMonth, out lpsDay, out lpsDayOfWeek, out lpsHour, out lpsMinute, out lpsSecond)));
            return returnValue;
        }
        
        public virtual int SetClockData(short sYear, short sMonth, short sDay, short sDayOfWeek, short sHour, short sMinute, short sSecond) {
            if ((this.ocx == null)) {
                throw new System.Windows.Forms.AxHost.InvalidActiveXStateException("SetClockData", System.Windows.Forms.AxHost.ActiveXInvokeKind.MethodInvoke);
            }
            int returnValue = ((int)(this.ocx.SetClockData(sYear, sMonth, sDay, sDayOfWeek, sHour, sMinute, sSecond)));
            return returnValue;
        }
        
        public virtual int SetDevice(string szDevice, int lData) {
            if ((this.ocx == null)) {
                throw new System.Windows.Forms.AxHost.InvalidActiveXStateException("SetDevice", System.Windows.Forms.AxHost.ActiveXInvokeKind.MethodInvoke);
            }
            int returnValue = ((int)(this.ocx.SetDevice(szDevice, lData)));
            return returnValue;
        }
        
        public virtual int GetDevice(string szDevice, out int lplData) {
            if ((this.ocx == null)) {
                throw new System.Windows.Forms.AxHost.InvalidActiveXStateException("GetDevice", System.Windows.Forms.AxHost.ActiveXInvokeKind.MethodInvoke);
            }
            int returnValue = ((int)(this.ocx.GetDevice(szDevice, out lplData)));
            return returnValue;
        }
        
        public virtual int CheckDeviceString(string szDevice, int lCheckType, int lSize, out int lplDeviceType, out string lpszDeviceName, out int lplDeviceNumber, out int lplDeviceRadix) {
            if ((this.ocx == null)) {
                throw new System.Windows.Forms.AxHost.InvalidActiveXStateException("CheckDeviceString", System.Windows.Forms.AxHost.ActiveXInvokeKind.MethodInvoke);
            }
            int returnValue = ((int)(this.ocx.CheckDeviceString(szDevice, lCheckType, lSize, out lplDeviceType, out lpszDeviceName, out lplDeviceNumber, out lplDeviceRadix)));
            return returnValue;
        }
        
        public virtual int EntryDeviceStatus(string szDeviceList, int lSize, int lMonitorCycle, ref int lplData) {
            if ((this.ocx == null)) {
                throw new System.Windows.Forms.AxHost.InvalidActiveXStateException("EntryDeviceStatus", System.Windows.Forms.AxHost.ActiveXInvokeKind.MethodInvoke);
            }
            int returnValue = ((int)(this.ocx.EntryDeviceStatus(szDeviceList, lSize, lMonitorCycle, ref lplData)));
            return returnValue;
        }
        
        public virtual int FreeDeviceStatus() {
            if ((this.ocx == null)) {
                throw new System.Windows.Forms.AxHost.InvalidActiveXStateException("FreeDeviceStatus", System.Windows.Forms.AxHost.ActiveXInvokeKind.MethodInvoke);
            }
            int returnValue = ((int)(this.ocx.FreeDeviceStatus()));
            return returnValue;
        }
        
        public virtual int ReadDeviceBlock2(string szDevice, int lSize, out short lpsData) {
            if ((this.ocx == null)) {
                throw new System.Windows.Forms.AxHost.InvalidActiveXStateException("ReadDeviceBlock2", System.Windows.Forms.AxHost.ActiveXInvokeKind.MethodInvoke);
            }
            int returnValue = ((int)(this.ocx.ReadDeviceBlock2(szDevice, lSize, out lpsData)));
            return returnValue;
        }
        
        public virtual int WriteDeviceBlock2(string szDevice, int lSize, ref short lpsData) {
            if ((this.ocx == null)) {
                throw new System.Windows.Forms.AxHost.InvalidActiveXStateException("WriteDeviceBlock2", System.Windows.Forms.AxHost.ActiveXInvokeKind.MethodInvoke);
            }
            int returnValue = ((int)(this.ocx.WriteDeviceBlock2(szDevice, lSize, ref lpsData)));
            return returnValue;
        }
        
        public virtual int ReadDeviceRandom2(string szDeviceList, int lSize, out short lpsData) {
            if ((this.ocx == null)) {
                throw new System.Windows.Forms.AxHost.InvalidActiveXStateException("ReadDeviceRandom2", System.Windows.Forms.AxHost.ActiveXInvokeKind.MethodInvoke);
            }
            int returnValue = ((int)(this.ocx.ReadDeviceRandom2(szDeviceList, lSize, out lpsData)));
            return returnValue;
        }
        
        public virtual int WriteDeviceRandom2(string szDeviceList, int lSize, ref short lpsData) {
            if ((this.ocx == null)) {
                throw new System.Windows.Forms.AxHost.InvalidActiveXStateException("WriteDeviceRandom2", System.Windows.Forms.AxHost.ActiveXInvokeKind.MethodInvoke);
            }
            int returnValue = ((int)(this.ocx.WriteDeviceRandom2(szDeviceList, lSize, ref lpsData)));
            return returnValue;
        }
        
        public virtual int GetDevice2(string szDevice, out short lpsData) {
            if ((this.ocx == null)) {
                throw new System.Windows.Forms.AxHost.InvalidActiveXStateException("GetDevice2", System.Windows.Forms.AxHost.ActiveXInvokeKind.MethodInvoke);
            }
            int returnValue = ((int)(this.ocx.GetDevice2(szDevice, out lpsData)));
            return returnValue;
        }
        
        public virtual int SetDevice2(string szDevice, short sData) {
            if ((this.ocx == null)) {
                throw new System.Windows.Forms.AxHost.InvalidActiveXStateException("SetDevice2", System.Windows.Forms.AxHost.ActiveXInvokeKind.MethodInvoke);
            }
            int returnValue = ((int)(this.ocx.SetDevice2(szDevice, sData)));
            return returnValue;
        }
        
        public virtual int Connect() {
            if ((this.ocx == null)) {
                throw new System.Windows.Forms.AxHost.InvalidActiveXStateException("Connect", System.Windows.Forms.AxHost.ActiveXInvokeKind.MethodInvoke);
            }
            int returnValue = ((int)(this.ocx.Connect()));
            return returnValue;
        }
        
        public virtual int Disconnect() {
            if ((this.ocx == null)) {
                throw new System.Windows.Forms.AxHost.InvalidActiveXStateException("Disconnect", System.Windows.Forms.AxHost.ActiveXInvokeKind.MethodInvoke);
            }
            int returnValue = ((int)(this.ocx.Disconnect()));
            return returnValue;
        }
        
        protected override void CreateSink() {
            try {
                this.eventMulticaster = new AxActUtlTypeEventMulticaster(this);
                this.cookie = new System.Windows.Forms.AxHost.ConnectionPointCookie(this.ocx, this.eventMulticaster, typeof(ActUtlTypeLib._IActUtlTypeEvents));
            }
            catch (System.Exception ) {
            }
        }
        
        protected override void DetachSink() {
            try {
                this.cookie.Disconnect();
            }
            catch (System.Exception ) {
            }
        }
        
        protected override void AttachInterfaces() {
            try {
                this.ocx = ((ActUtlTypeLib.IActUtlType)(this.GetOcx()));
            }
            catch (System.Exception ) {
            }
        }
        
        internal void RaiseOnOnDeviceStatus(object sender, _IActUtlTypeEvents_OnDeviceStatusEvent e) {
            if ((this.OnDeviceStatus != null)) {
                this.OnDeviceStatus(sender, e);
            }
        }
    }
    
    public delegate void _IActUtlTypeEvents_OnDeviceStatusEventHandler(object sender, _IActUtlTypeEvents_OnDeviceStatusEvent e);
    
    public class _IActUtlTypeEvents_OnDeviceStatusEvent {
        
        public string szDevice;
        
        public int lData;
        
        public int lReturnCode;
        
        public _IActUtlTypeEvents_OnDeviceStatusEvent(string szDevice, int lData, int lReturnCode) {
            this.szDevice = szDevice;
            this.lData = lData;
            this.lReturnCode = lReturnCode;
        }
    }
    
    [System.Runtime.InteropServices.ClassInterface(System.Runtime.InteropServices.ClassInterfaceType.None)]
    public class AxActUtlTypeEventMulticaster : ActUtlTypeLib._IActUtlTypeEvents {
        
        private AxActUtlType parent;
        
        public AxActUtlTypeEventMulticaster(AxActUtlType parent) {
            this.parent = parent;
        }
        
        public virtual void OnDeviceStatus(string szDevice, int lData, int lReturnCode) {
            _IActUtlTypeEvents_OnDeviceStatusEvent ondevicestatusEvent = new _IActUtlTypeEvents_OnDeviceStatusEvent(szDevice, lData, lReturnCode);
            this.parent.RaiseOnOnDeviceStatus(this.parent, ondevicestatusEvent);
        }
    }
    
    [System.Windows.Forms.AxHost.ClsidAttribute("{29de4b06-c429-420a-b3b7-bf7dc62649b2}")]
    [System.ComponentModel.DesignTimeVisibleAttribute(true)]
    [System.ComponentModel.DefaultEvent("OnDeviceStatus")]
    public class AxActMLUtlType : System.Windows.Forms.AxHost {
        
        private ActUtlTypeLib.IActMLUtlType ocx;
        
        private AxActMLUtlTypeEventMulticaster eventMulticaster;
        
        private System.Windows.Forms.AxHost.ConnectionPointCookie cookie;
        
        public AxActMLUtlType() : 
                base("29de4b06-c429-420a-b3b7-bf7dc62649b2") {
        }
        
        [System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Hidden)]
        [System.Runtime.InteropServices.DispIdAttribute(16)]
        public virtual object ActLogicalStationNumber {
            get {
                if ((this.ocx == null)) {
                    throw new System.Windows.Forms.AxHost.InvalidActiveXStateException("ActLogicalStationNumber", System.Windows.Forms.AxHost.ActiveXInvokeKind.PropertyGet);
                }
                return this.ocx.ActLogicalStationNumber;
            }
            set {
                if ((this.ocx == null)) {
                    throw new System.Windows.Forms.AxHost.InvalidActiveXStateException("ActLogicalStationNumber", System.Windows.Forms.AxHost.ActiveXInvokeKind.PropertySet);
                }
                this.ocx.ActLogicalStationNumber = value;
            }
        }
        
        [System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Hidden)]
        [System.Runtime.InteropServices.DispIdAttribute(19)]
        public virtual object ActPassword {
            get {
                if ((this.ocx == null)) {
                    throw new System.Windows.Forms.AxHost.InvalidActiveXStateException("ActPassword", System.Windows.Forms.AxHost.ActiveXInvokeKind.PropertyGet);
                }
                return this.ocx.ActPassword;
            }
            set {
                if ((this.ocx == null)) {
                    throw new System.Windows.Forms.AxHost.InvalidActiveXStateException("ActPassword", System.Windows.Forms.AxHost.ActiveXInvokeKind.PropertySet);
                }
                this.ocx.ActPassword = value;
            }
        }
        
        public event _IActMLUtlTypeEvents_OnDeviceStatusEventHandler OnDeviceStatus;
        
        public virtual object Open() {
            if ((this.ocx == null)) {
                throw new System.Windows.Forms.AxHost.InvalidActiveXStateException("Open", System.Windows.Forms.AxHost.ActiveXInvokeKind.MethodInvoke);
            }
            object returnValue = ((object)(this.ocx.Open()));
            return returnValue;
        }
        
        public virtual object Close() {
            if ((this.ocx == null)) {
                throw new System.Windows.Forms.AxHost.InvalidActiveXStateException("Close", System.Windows.Forms.AxHost.ActiveXInvokeKind.MethodInvoke);
            }
            object returnValue = ((object)(this.ocx.Close()));
            return returnValue;
        }
        
        public virtual object GetDevice(object varDevice, out object lpvarData) {
            if ((this.ocx == null)) {
                throw new System.Windows.Forms.AxHost.InvalidActiveXStateException("GetDevice", System.Windows.Forms.AxHost.ActiveXInvokeKind.MethodInvoke);
            }
            object returnValue = ((object)(this.ocx.GetDevice(varDevice, out lpvarData)));
            return returnValue;
        }
        
        public virtual object SetDevice(object varDevice, object varData) {
            if ((this.ocx == null)) {
                throw new System.Windows.Forms.AxHost.InvalidActiveXStateException("SetDevice", System.Windows.Forms.AxHost.ActiveXInvokeKind.MethodInvoke);
            }
            object returnValue = ((object)(this.ocx.SetDevice(varDevice, varData)));
            return returnValue;
        }
        
        public virtual object ReadDeviceBlock(object varDevice, object varSize, out object lpvarData) {
            if ((this.ocx == null)) {
                throw new System.Windows.Forms.AxHost.InvalidActiveXStateException("ReadDeviceBlock", System.Windows.Forms.AxHost.ActiveXInvokeKind.MethodInvoke);
            }
            object returnValue = ((object)(this.ocx.ReadDeviceBlock(varDevice, varSize, out lpvarData)));
            return returnValue;
        }
        
        public virtual object WriteDeviceBlock(object varDevice, object varSize, object varData) {
            if ((this.ocx == null)) {
                throw new System.Windows.Forms.AxHost.InvalidActiveXStateException("WriteDeviceBlock", System.Windows.Forms.AxHost.ActiveXInvokeKind.MethodInvoke);
            }
            object returnValue = ((object)(this.ocx.WriteDeviceBlock(varDevice, varSize, varData)));
            return returnValue;
        }
        
        public virtual object ReadDeviceRandom(object varDeviceList, object varSize, out object lpvarData) {
            if ((this.ocx == null)) {
                throw new System.Windows.Forms.AxHost.InvalidActiveXStateException("ReadDeviceRandom", System.Windows.Forms.AxHost.ActiveXInvokeKind.MethodInvoke);
            }
            object returnValue = ((object)(this.ocx.ReadDeviceRandom(varDeviceList, varSize, out lpvarData)));
            return returnValue;
        }
        
        public virtual object WriteDeviceRandom(object varDeviceList, object varSize, object varData) {
            if ((this.ocx == null)) {
                throw new System.Windows.Forms.AxHost.InvalidActiveXStateException("WriteDeviceRandom", System.Windows.Forms.AxHost.ActiveXInvokeKind.MethodInvoke);
            }
            object returnValue = ((object)(this.ocx.WriteDeviceRandom(varDeviceList, varSize, varData)));
            return returnValue;
        }
        
        public virtual object ReadBuffer(object varStartIO, object varAddress, object varReadSize, out object lpvarData) {
            if ((this.ocx == null)) {
                throw new System.Windows.Forms.AxHost.InvalidActiveXStateException("ReadBuffer", System.Windows.Forms.AxHost.ActiveXInvokeKind.MethodInvoke);
            }
            object returnValue = ((object)(this.ocx.ReadBuffer(varStartIO, varAddress, varReadSize, out lpvarData)));
            return returnValue;
        }
        
        public virtual object WriteBuffer(object varStartIO, object varAddress, object varWriteSize, object varData) {
            if ((this.ocx == null)) {
                throw new System.Windows.Forms.AxHost.InvalidActiveXStateException("WriteBuffer", System.Windows.Forms.AxHost.ActiveXInvokeKind.MethodInvoke);
            }
            object returnValue = ((object)(this.ocx.WriteBuffer(varStartIO, varAddress, varWriteSize, varData)));
            return returnValue;
        }
        
        public virtual object GetCpuType(out object lpvarCpuName, out object lpvarCpuCode) {
            if ((this.ocx == null)) {
                throw new System.Windows.Forms.AxHost.InvalidActiveXStateException("GetCpuType", System.Windows.Forms.AxHost.ActiveXInvokeKind.MethodInvoke);
            }
            object returnValue = ((object)(this.ocx.GetCpuType(out lpvarCpuName, out lpvarCpuCode)));
            return returnValue;
        }
        
        public virtual object SetCpuStatus(object varOperation) {
            if ((this.ocx == null)) {
                throw new System.Windows.Forms.AxHost.InvalidActiveXStateException("SetCpuStatus", System.Windows.Forms.AxHost.ActiveXInvokeKind.MethodInvoke);
            }
            object returnValue = ((object)(this.ocx.SetCpuStatus(varOperation)));
            return returnValue;
        }
        
        public virtual object GetClockData(out object lpvarYear, out object lpvarMonth, out object lpvarDay, out object lpvarDayOfWeek, out object lpvarHour, out object lpvarMinute, out object lpvarSecond) {
            if ((this.ocx == null)) {
                throw new System.Windows.Forms.AxHost.InvalidActiveXStateException("GetClockData", System.Windows.Forms.AxHost.ActiveXInvokeKind.MethodInvoke);
            }
            object returnValue = ((object)(this.ocx.GetClockData(out lpvarYear, out lpvarMonth, out lpvarDay, out lpvarDayOfWeek, out lpvarHour, out lpvarMinute, out lpvarSecond)));
            return returnValue;
        }
        
        public virtual object SetClockData(object varYear, object varMonth, object varDay, object varDayOfWeek, object varHour, object varMinute, object varSecond) {
            if ((this.ocx == null)) {
                throw new System.Windows.Forms.AxHost.InvalidActiveXStateException("SetClockData", System.Windows.Forms.AxHost.ActiveXInvokeKind.MethodInvoke);
            }
            object returnValue = ((object)(this.ocx.SetClockData(varYear, varMonth, varDay, varDayOfWeek, varHour, varMinute, varSecond)));
            return returnValue;
        }
        
        public virtual object EntryDeviceStatus(object varDeviceList, object varSize, object varMonitorCycle, object varData) {
            if ((this.ocx == null)) {
                throw new System.Windows.Forms.AxHost.InvalidActiveXStateException("EntryDeviceStatus", System.Windows.Forms.AxHost.ActiveXInvokeKind.MethodInvoke);
            }
            object returnValue = ((object)(this.ocx.EntryDeviceStatus(varDeviceList, varSize, varMonitorCycle, varData)));
            return returnValue;
        }
        
        public virtual object FreeDeviceStatus() {
            if ((this.ocx == null)) {
                throw new System.Windows.Forms.AxHost.InvalidActiveXStateException("FreeDeviceStatus", System.Windows.Forms.AxHost.ActiveXInvokeKind.MethodInvoke);
            }
            object returnValue = ((object)(this.ocx.FreeDeviceStatus()));
            return returnValue;
        }
        
        public virtual object ReadDeviceBlock2(object varDevice, object varSize, out object lpvarData) {
            if ((this.ocx == null)) {
                throw new System.Windows.Forms.AxHost.InvalidActiveXStateException("ReadDeviceBlock2", System.Windows.Forms.AxHost.ActiveXInvokeKind.MethodInvoke);
            }
            object returnValue = ((object)(this.ocx.ReadDeviceBlock2(varDevice, varSize, out lpvarData)));
            return returnValue;
        }
        
        public virtual object WriteDeviceBlock2(object varDevice, object varSize, object varData) {
            if ((this.ocx == null)) {
                throw new System.Windows.Forms.AxHost.InvalidActiveXStateException("WriteDeviceBlock2", System.Windows.Forms.AxHost.ActiveXInvokeKind.MethodInvoke);
            }
            object returnValue = ((object)(this.ocx.WriteDeviceBlock2(varDevice, varSize, varData)));
            return returnValue;
        }
        
        public virtual object ReadDeviceRandom2(object varDeviceList, object varSize, out object lpvarData) {
            if ((this.ocx == null)) {
                throw new System.Windows.Forms.AxHost.InvalidActiveXStateException("ReadDeviceRandom2", System.Windows.Forms.AxHost.ActiveXInvokeKind.MethodInvoke);
            }
            object returnValue = ((object)(this.ocx.ReadDeviceRandom2(varDeviceList, varSize, out lpvarData)));
            return returnValue;
        }
        
        public virtual object WriteDeviceRandom2(object varDeviceList, object varSize, object varData) {
            if ((this.ocx == null)) {
                throw new System.Windows.Forms.AxHost.InvalidActiveXStateException("WriteDeviceRandom2", System.Windows.Forms.AxHost.ActiveXInvokeKind.MethodInvoke);
            }
            object returnValue = ((object)(this.ocx.WriteDeviceRandom2(varDeviceList, varSize, varData)));
            return returnValue;
        }
        
        public virtual object GetDevice2(object varDevice, out object lpvarData) {
            if ((this.ocx == null)) {
                throw new System.Windows.Forms.AxHost.InvalidActiveXStateException("GetDevice2", System.Windows.Forms.AxHost.ActiveXInvokeKind.MethodInvoke);
            }
            object returnValue = ((object)(this.ocx.GetDevice2(varDevice, out lpvarData)));
            return returnValue;
        }
        
        public virtual object SetDevice2(object varDevice, object varData) {
            if ((this.ocx == null)) {
                throw new System.Windows.Forms.AxHost.InvalidActiveXStateException("SetDevice2", System.Windows.Forms.AxHost.ActiveXInvokeKind.MethodInvoke);
            }
            object returnValue = ((object)(this.ocx.SetDevice2(varDevice, varData)));
            return returnValue;
        }
        
        protected override void CreateSink() {
            try {
                this.eventMulticaster = new AxActMLUtlTypeEventMulticaster(this);
                this.cookie = new System.Windows.Forms.AxHost.ConnectionPointCookie(this.ocx, this.eventMulticaster, typeof(ActUtlTypeLib._IActMLUtlTypeEvents));
            }
            catch (System.Exception ) {
            }
        }
        
        protected override void DetachSink() {
            try {
                this.cookie.Disconnect();
            }
            catch (System.Exception ) {
            }
        }
        
        protected override void AttachInterfaces() {
            try {
                this.ocx = ((ActUtlTypeLib.IActMLUtlType)(this.GetOcx()));
            }
            catch (System.Exception ) {
            }
        }
        
        internal void RaiseOnOnDeviceStatus(object sender, _IActMLUtlTypeEvents_OnDeviceStatusEvent e) {
            if ((this.OnDeviceStatus != null)) {
                this.OnDeviceStatus(sender, e);
            }
        }
    }
    
    public delegate void _IActMLUtlTypeEvents_OnDeviceStatusEventHandler(object sender, _IActMLUtlTypeEvents_OnDeviceStatusEvent e);
    
    public class _IActMLUtlTypeEvents_OnDeviceStatusEvent {
        
        public object varDevice;
        
        public object varData;
        
        public object varReturnCode;
        
        public _IActMLUtlTypeEvents_OnDeviceStatusEvent(object varDevice, object varData, object varReturnCode) {
            this.varDevice = varDevice;
            this.varData = varData;
            this.varReturnCode = varReturnCode;
        }
    }
    
    [System.Runtime.InteropServices.ClassInterface(System.Runtime.InteropServices.ClassInterfaceType.None)]
    public class AxActMLUtlTypeEventMulticaster : ActUtlTypeLib._IActMLUtlTypeEvents {
        
        private AxActMLUtlType parent;
        
        public AxActMLUtlTypeEventMulticaster(AxActMLUtlType parent) {
            this.parent = parent;
        }
        
        public virtual void OnDeviceStatus(object varDevice, object varData, object varReturnCode) {
            _IActMLUtlTypeEvents_OnDeviceStatusEvent ondevicestatusEvent = new _IActMLUtlTypeEvents_OnDeviceStatusEvent(varDevice, varData, varReturnCode);
            this.parent.RaiseOnOnDeviceStatus(this.parent, ondevicestatusEvent);
        }
    }
}
