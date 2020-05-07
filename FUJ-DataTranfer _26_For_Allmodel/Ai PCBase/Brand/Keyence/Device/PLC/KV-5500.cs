using AxDATABUILDERAXLibLB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ai_PCInterface.Brand.Keyence.Device.PLC
{
    public class KV_5500 : CommManager
    {
        public KV_5500():base()
        {          
            
        }
        
        /// <summary>
        /// 
        /// </summary>
        public class KV_Status
        {
            public PLC PLCs { get; set; }
           
            public class PLC
            {
                public bool Commu { set; get; }
            }
          
        }
        /// <summary>
        /// 
        /// </summary>
        public class KV_Process
        {
            //public bool Enable { set; get; }

        }
        /// <summary>
        /// 
        /// </summary>
        public class KV_Operation
        {
            //public bool Enable { set; get; }
        }
        /// <summary>
        /// 
        /// </summary>
        public class KV_Alarm
        {
            //public bool Enable { set; get; }
        }
    }
}
