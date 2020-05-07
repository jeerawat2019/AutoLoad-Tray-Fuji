using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace PCBase_Interface.Brand.Misubishi.Divice.PLC
{
    public class PLC_Commu : PLC_Base
    {
        /// <summary>
        /// Part Id
        /// </summary>
        [Category("Communication"), Browsable(true), Description("mAxActMLUtlType")]
        public ActUtlTypeLib.ActUtlTypeClass mActUtlTypeClass = null;
        /// <summary>
        /// Part Id
        /// </summary>
        [Category("Communication"), Browsable(true), Description("ActMLSupportMsg")]
        public ActSupportMsgLib.ActSupportMsgClass mActSupportMsgClass = null;
        //[Category("Communication"), Browsable(true), Description("ActMLSupportMsg")]
        //public ActProgTypeLib.ActMLProgTypeClass mActMLProgTypeClass = null;
        /// <summary>
        /// Part Id
        /// </summary>
        [Category("Communication"), Browsable(true), Description("Ping")]
        private Ping mPing = null;
        /// <summary>
        /// Part Id
        /// </summary>
        [Category("Communication"), Browsable(true), Description("iStstionNumber")]
        public int iStstionNumber
        {
            get;
            set;
        } = 5;
        /// <summary>
        /// Part Id
        /// </summary>
        [Category("Communication"), Browsable(true), Description("IPAddress")]
        public string IPAddress
        {
            get;
            set;
        } = "192.168.0.70";
        /// <summary>
        /// Part Id
        /// </summary>
        [Category("Communication"), Browsable(true), Description("IsNetworkConnect")]
        public bool IsNetworkConnect
        {
            get
            {
                if (string.IsNullOrEmpty(IPAddress))
                    throw new PLC_Exception("PLC IP Address found!");
               
                mPing = new Ping();
                ///
                PingReply pingReply = mPing.Send(IPAddress, 10000);
                ///
                Status = pingReply.Status.ToString() != "Success" ? Error.CommuFail : Error.Normal;
                ///
                return (Status == Error.Normal) ? true : false;

            }
        }
        /// <summary>
        /// 
        /// </summary>
        [Category("Communication"), Browsable(true), Description("IPAddress")]
        public PLC_Commu()
        {

        }
        [Category("Communication"), Browsable(true), Description("IPAddress")]
        public PLC_Commu(string id, string name):base(name)
        {

        }
        [Category("Communication"), Browsable(true), Description("Initialization")]
        public override void Initialization()
        {
            base.Initialization();
        }
       
    }
}
