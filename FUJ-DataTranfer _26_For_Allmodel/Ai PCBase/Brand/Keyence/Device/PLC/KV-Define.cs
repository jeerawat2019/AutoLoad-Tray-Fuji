using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ai_PCInterface.Brand.Keyence.Device.PLC
{
    public class KV_Define
    {
       
        public enum KV_Status : short
        {
            PLC_SUCCESS = 0,
        }
        public enum KV_Error
        {
            PLC_NO_ERROR = 0,
            PLC_DBCOMMANAGER_ERR,
            PLC_WRITEDEVICE_ERR,
            PLC_READDEVICE_ERR,
            PLC_READTEXT_ERR,
            PLC_FORMATE_ERR,
        };
    }
}
