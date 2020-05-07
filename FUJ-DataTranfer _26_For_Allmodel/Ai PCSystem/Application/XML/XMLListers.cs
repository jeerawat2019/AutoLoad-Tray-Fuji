using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.Data;
using System.Windows.Forms;

namespace _File.ClassLibrary
{
    public class XMLLister
    {
        /// <summary>
        /// 
        /// </summary>
        private static List<string> butADV = new List<string>();
        /// <summary>
        /// 
        /// </summary>
        private static List<string> butRTN = new List<string>();

        /// <summary>
        /// 
        /// </summary>
        public static List<string> DescButAdvance
        {
            get { return butADV; }
            set { butADV = new List<string>(value); }
        }
        /// <summary>
        /// 
        /// </summary>
        public static List<string> DescButReturn
        {
            get { return butRTN; }
            set { butRTN = new List<string>(value); }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="fileName"></param>
        /// <returns></returns>
        public static List<string> readerXMLFile(string fileName)
        {
            XmlTextReader reader = new XmlTextReader(fileName);
            List<string> strList = new List<string>();

            while (reader.Read())
            {
                switch (reader.NodeType)
                {
                    case XmlNodeType.Element: // The node is an element.
                        //Console.Write("<" + reader.Name);
                        //Console.WriteLine(">");
                        break;
                    case XmlNodeType.Text: //Display the text in each element.
                        strList.Add(reader.Value);
                        break;
                    case XmlNodeType.EndElement: //Display the end of the element.
                        //Console.Write("</" + reader.Name);
                        //Console.WriteLine(">");
                        break;
                }
            }
            return strList;
        }
        //DataSet dataSet = new DataSet();
        //dataSet.ReadXml(@"C:\Books\Books.xml");
        //dataGridView1.DataSource = dataSet.Tables[0];
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static DataTable readXMLM_OPManualIO(String Path)
        {

            XMLLister.DescButAdvance.Clear();
            XMLLister.DescButReturn.Clear();

            DataTable dt = new DataTable();
            XDocument doc = XDocument.Load(Path);
            var records = (from data in doc.Root.Elements("Section")
                           select data);
            if (records != null)
            {

                dt.Columns.Add("NO.", typeof(string));
                dt.Columns.Add("DESCRIPTION", typeof(string));
              
                int count = 0;

                foreach (var item in records)
                {
                    DataRow dr = dt.NewRow();
                    dr["NO."] = (string)item.Element("NO.");
                    dr["DESCRIPTION"] = (string)item.Element("DESCRIPTION");
                    
                    butADV.Add((string)item.Element("ADV."));
                    butRTN.Add((string)item.Element("RTN."));
                    dt.Rows.Add(dr);
                    count++;
                }
            }
            return dt;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Path"></param>
        /// <returns></returns>
        public static List<string> readXMLM_OPMonitor(String Path)
        {

            List<string> dt = new List<string>();
            XDocument doc = XDocument.Load(Path);
            var records = (from data in doc.Root.Elements("Section")
                           select data);
            if (records != null)
            {
                //int count = 0;

                foreach (var item in records)
                {
                  
                    dt.Add((string)item.Element("DESCRIPTION"));
                  
                }
            }
            return dt;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Path"></param>
        /// <returns></returns>
        public static DataTable readXMLM_Configs(String Path)
        {

            DataTable dt = new DataTable();
            XDocument doc = XDocument.Load(Path);

            var records = (from data in doc.Root.Elements("Country")
                           select data);
            if (records != null)
            {

                dt.Columns.Add("NO.", typeof(string));
                dt.Columns.Add(new DataColumn("CHECK", typeof(bool)));
                dt.Columns.Add("DESCRIPTION", typeof(string));
                dt.Columns.Add("DATA", typeof(string));
                dt.Columns.Add("UNIT", typeof(string));
                int count = 0;

                foreach (var item in records)
                {
                    DataRow dr = dt.NewRow();
                    dr["NO."] = (string)item.Element("NO.");
                    dr["CHECK"] = true;// (string)item.Element("CHECK");
                    dr["DESCRIPTION"] = (string)item.Element("DESC");
                    dr["DATA"] = (string)item.Element("DATA");
                    dr["UNIT"] = (string)item.Element("UNIT");
                    dt.Rows.Add(dr);
                    count++;
                }
            }

            return dt;
        }
    }
}
