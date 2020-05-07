using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Ai_PCSystem.File
{
    
    #region EXCEPTION_HANDLING
    /// <summary>
    /// Exceptions thrown by errors within the FTDI class.
    /// </summary>
    [global::System.Serializable]
    public class FException : Exception
    {
        /// <summary>
        /// 
        /// </summary>
        public FException() { }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        public FException(string message) : base(message) { }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="inner"></param>
        public FException(string message, Exception inner) : base(message, inner) { }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="info"></param>
        /// <param name="context"></param>
        protected FException(
        System.Runtime.Serialization.SerializationInfo info,
        System.Runtime.Serialization.StreamingContext context)
            : base(info, context) { }
    }
    #endregion
    
}
