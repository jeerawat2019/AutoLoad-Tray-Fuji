using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FUJ_DataTranfer
{
    public class iServer
    {
        ///
        public string HostNameByIP { get; set; } = "192.168.0.100";//Default
        /// <summary>
        /// 
        /// </summary>
        public string SubFolderInput { get; set; } = "AVITotalfolder";
        /// <summary>
        /// 
        /// </summary>
        public string SubFolderOutput { get; set; } = "Shippingfolder";
        /// <summary>
        /// 
        /// </summary>
        public string PathInput { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string PathOutput { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string UserName { get; set; } = "test1111";
        /// <summary>
        /// 
        /// </summary>
        public string Password { get; set; } = "12345";



    }
}
