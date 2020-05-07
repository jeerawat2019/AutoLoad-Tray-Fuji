using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Ai_Machine.XmlModel
{
    /// <summary>
    /// 
    /// </summary>
    public class Xml_Config_Base //: Ai_PCInterface.Brand.Keyence.Device.PLC.KV_5500
    {
        /// <summary>
        /// 
        /// </summary>
        public Xml_Config_Base() { }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="station_xml"></param>
        public Xml_Config_Base(List<Xml_Config.xParameter> station_xml)
        {
            Station_XmlList = station_xml;
        }
        /// <summary>
        /// 
        /// </summary>
        public List<Xml_Config.xParameter> Station_XmlList { get; set; } = new List<Xml_Config.xParameter>();
        /// <summary>
        /// 
        /// </summary>
        //public Dictionary<string, string> StationResult { get; set; } = new Dictionary<string, string>();
        /// <summary>
        /// 
        /// </summary>
        //public System.Data.DataSet DataXmlToCreateSqlTable { get; set; } = new System.Data.DataSet();
        /// <summary>
        /// 
        /// </summary>
        public class Xml_Config
        {           
             /// <summary>
             /// 
             /// </summary>
            public class xParameter : Base
            {
                [XmlIgnore]
                [Category("Indexing"), Browsable(true), Description("Is end station index")]
                public bool Status
                {
                    get;
                    set;
                }
                [XmlIgnore]
                [Category("Indexing"), Browsable(true), Description("Is end station index")]
                public string DataType
                {
                    get;
                    set;
                }
                [XmlIgnore]
                [Category("Indexing"), Browsable(true), Description("Is end station index")]
                public xMemory Memorys
                {
                    get;
                    set;
                }
                [XmlIgnore]
                [Category("Indexing"), Browsable(true), Description("Is end station index")]
                public string Unit
                {
                    get;
                    set;
                }
                [XmlIgnore]
                [Category("Indexing"), Browsable(true), Description("Is end station index")]
                public string Value
                {
                    get;
                    set;
                }
            }
            /// <summary>
            /// 
            /// </summary>
            public class xMemory
            {
                public List<xAddress> Address = new List<xAddress>();
            }
            /// <summary>
            /// 
            /// </summary>
            public class xAddress
            {
                [XmlIgnore]
                [Category("Indexing"), Browsable(true), Description("Is end station index")]
                public string Start
                {
                    get;
                    set;
                }
                [XmlIgnore]
                [Category("Indexing"), Browsable(true), Description("Is end station index")]
                public string End
                {
                    get;
                    set;
                }
                [XmlIgnore]
                [Category("Indexing"), Browsable(true), Description("Is end station index")]
                public int Lenght
                {
                    get;
                    set;
                }

            }
        }
        
    }
    public class Base
    {
        [XmlIgnore]
        [Category("Indexing"), Browsable(true), Description("Is end station index")]
        public int ID
        {
            get;
            set;
        }
        [XmlIgnore]
        [Category("Indexing"), Browsable(true), Description("Is end station index")]
        public string Description
        {
            get;
            set;
        }
        [XmlIgnore]
        [Category("Indexing"), Browsable(true), Description("Is end station index")]
        public string Name
        {
            get;
            set;
        }
    }
}