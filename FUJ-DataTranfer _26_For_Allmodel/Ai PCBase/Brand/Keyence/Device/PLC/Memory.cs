using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Ai_PCInterface.Brand.Keyence.Device.PLC
{
    class Memmory : KV_Convert.CVGetToValve
    {

        //private int[] axDBLenghtConvert = null;
        /// <summary>
        /// 
        /// </summary>
        //private List<int> axDBGetMemType { get => _axDBGetMemType; set => _axDBGetMemType = value; }
        /// <summary>
        /// 
        /// </summary>
        public Memmory()
        {
            ///
            //axDBLenghtConvert = new int[] {2,2,2,20,1,1,2,2,2,2,2};
            ///
            //axDBGetMemType = new List<int>();
        }
        public Dictionary<string, string> mDBPerformance { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public static int Item = 0;
        private List<int> _axDBGetMemType;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="axDBPerformance"></param>
        public void DataConvert(AxDATABUILDERAXLibLB.AxDBDeviceManager axDBPerformance)
        {
            //mDBPerformance = new Dictionary<string, string>();
            //axDBGetMemType = new List<int>();

            //for (int i = 0; i < axDBPerformance.Devices.Count; i++)

            //    axDBGetMemType.Add(axDBPerformance.Devices[1 + i].ValueRead);

            //mDBPerformance.Add("Totol QTY"  ,   this.WValueToSignedText(axDBGetMemType[0], axDBGetMemType[1]));
            //mDBPerformance.Add("OK QTY"     ,   this.WValueToSignedText(axDBGetMemType[2], axDBGetMemType[3]));
            //mDBPerformance.Add("NG QTY"     ,   this.WValueToSignedText(axDBGetMemType[4], axDBGetMemType[5]));
            //mDBPerformance.Add("No", (Item +=1).ToString());
            //mDBPerformance.Add("Station"    ,   this.ValveToUnsignedText(axDBGetMemType[6]));
            //mDBPerformance.Add("Barcord"    ,   this.FCV_ASCII(axDBGetMemType.GetRange(10, 20).ToArray()));
            //mDBPerformance.Add("Justment"   ,   this.ValveToUnsignedText(axDBGetMemType[30]));
            //mDBPerformance.Add("CW MAX torque(mN・m)", this.WValueToFloatText(axDBGetMemType[32], axDBGetMemType[33]));
            //mDBPerformance.Add("CW T1(mN.m)",      this.WValueToFloatText(axDBGetMemType[34], axDBGetMemType[35]));
            //mDBPerformance.Add("CW T2(mN.m)",      this.WValueToFloatText(axDBGetMemType[36], axDBGetMemType[37]));
            //mDBPerformance.Add("CW Average",      this.WValueToFloatText(axDBGetMemType[38], axDBGetMemType[39]));
            //mDBPerformance.Add("CCW MAX torque(mN・m)", this.WValueToFloatText(axDBGetMemType[40], axDBGetMemType[41]));
            //mDBPerformance.Add("CCW T1(mN.m)", this.WValueToFloatText(axDBGetMemType[42], axDBGetMemType[43]));
            //mDBPerformance.Add("CCW T2(mN.m)", this.WValueToFloatText(axDBGetMemType[44], axDBGetMemType[45]));
            //mDBPerformance.Add("CCW Average", this.WValueToFloatText(axDBGetMemType[46], axDBGetMemType[47]));

        }
    }
}
