using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ai_PCInterface.Brand.Keyence.Device.PLC
{
    #region EXCEPTION_HANDLING
    /// <summary>
    /// Exceptions thrown by errors within the FTDI class.
    /// </summary>
    [global::System.Serializable]
    public class KV_COMException : Exception
    {
        /// <summary>
        /// 
        /// </summary>
        public KV_COMException() { }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        public KV_COMException(string message) : base(message) { }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="inner"></param>
        public KV_COMException(string message, Exception inner) : base(message, inner) { }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="info"></param>
        /// <param name="context"></param>
        protected KV_COMException(
        System.Runtime.Serialization.SerializationInfo info,
        System.Runtime.Serialization.StreamingContext context)
            : base(info, context) { }
    }
    #endregion
}
