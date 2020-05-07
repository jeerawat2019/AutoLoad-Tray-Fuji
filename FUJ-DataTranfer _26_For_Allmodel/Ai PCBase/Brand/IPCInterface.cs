using System.Collections.Generic;

namespace Ai_PCInterface.Brand.Keyence.Device.PLC
{
    public interface IKV_Base
    {
        bool Enable { get; set; }
        //string IPAddress { get; set; }
        Dictionary<string, string> StationResult { get; set; }

        //void InitializeStation();
    }
}