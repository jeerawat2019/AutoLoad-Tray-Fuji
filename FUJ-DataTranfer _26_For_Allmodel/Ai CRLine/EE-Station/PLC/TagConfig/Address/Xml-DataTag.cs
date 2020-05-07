using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Diagnostics;
using System.Data;

namespace Ai_Machine.EE_Station.PLC.TagConfig.Address
{
    public class Parameter : XmlModel.Xml_Config_Base
    {
        /// <summary>
        /// 
        /// </summary>
        public List<Xml_Config.xParameter> PLCTage { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public Parameter()
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="station_XML"></param>
        public Parameter(List<Xml_Config.xParameter> station_XML) : base(station_XML)
        {
           
        }
        /// <summary>
        /// 
        /// </summary>
        public class ConfigFileStation_XML
        {
            //Monitor_Base
            public static List<Xml_Config.xParameter> GetConfigResultList(string path)
            {
                XElement xmlDoc = XElement.Load(path + "\\TagConfig.xml");
                var station = from stn in xmlDoc.Descendants("DETIAL")
                              select new Xml_Config.xParameter
                              {
                                  
                                  ID = Convert.ToInt32(stn.Element("ID").Value),
                                  Description = stn.Element("DESCRIPTION").Value,
                                  Value = stn.Element("VALUE").Value,
                                  Unit = stn.Element("UNIT").Value,
                                  Status = Convert.ToBoolean(stn.Element("STATUS").Value),
                                  DataType = stn.Element("TYPE").Value,

                                  Memorys = new Xml_Config.xMemory()
                                  {
                                      Address = new List<Xml_Config.xAddress>(from add in stn.Descendants("ADDRESS")
                                                                                select new Xml_Config.xAddress
                                                                                {
                                                                                    Start = add.Element("START").Value,
                                                                                    End = add.Element("END").Value,
                                                                                    Lenght = Convert.ToInt32(add.Element("LENGHT").Value)
                                                                                })
                                  }
                              };
                return station.ToList();
            }
            /// <summary>
            /// 
            /// </summary>
            /// <param name="station_xml"></param>
            /// <returns></returns>
            public static Dictionary<string, string> GetDitionary(List<Xml_Config.xParameter> station_xml)
            {
                var mDesc = from item in station_xml

                            select new XmlModel.Xml_Config_Base.Xml_Config.xParameter
                            {
                                Description = item.Description,

                            }.Description;//.ToDictionary(x => x, x => "");

                return mDesc.ToDictionary(x => x, x => "");
            }

        }
    }
}
