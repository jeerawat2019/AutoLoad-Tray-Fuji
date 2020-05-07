//@Author R.Yavuz

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml;

namespace XMLister
{
    /// <summary>
    /// Helps you creating XML/XAML Files.
    /// </summary>
    public class XamlBase
    {
        /// <summary>
        /// Returns a Tuple(bool, string) with the boolean being "true" if the creation was a success and "false" if it wasn't. In this case, the string contains the exception.
        /// Will be written like Xmlwriter.WriteElementString(Nodelist (0), Contentlist (0))
        /// </summary>
        /// <param name="Nodelist">A (sorted) Collection of Nodes</param>
        /// <param name="Contentlist">A Collection of Strings, every Element in this List has its counterpart in the Nodelist</param>
        /// <param name="Dest">Destination Filename</param>
        /// <param name="Startelement">Your Start/Endelement</param>
        /// <param name="xms">You NEED to put a XmlWriterSettings Item in here, if this is null the default settings will be Indent = true, NewLineOnAttributes = true</param>
        /// <returns>A Tuple(bool, string)</returns>
        public Tuple<bool, string> Write(List<string> Nodelist, List<string> Contentlist, string Dest, string Startelement, XmlWriterSettings xms)
        {
            try
            {
                if (Contentlist.Count != Nodelist.Count)
                {
                    return Tuple.Create<bool, string>(false, "Your Nodelist does not match the Contentlist!");
                }
                XmlWriter wr = XmlWriter.Create(Dest, xms);
               

                int x = Nodelist.Count;
                int i = 0;

                wr.WriteStartElement(Startelement);
                while (i <= x-1)
                {
                    wr.WriteElementString(Nodelist.ElementAt(i), Contentlist.ElementAt(i));
                    i++;
                }
                wr.WriteEndElement();
                wr.Close();
               
                return Tuple.Create<bool, string>(true, "Success");
            }
            catch (Exception ex)
            {
                return Tuple.Create<bool, string>(false, ex.ToString());
               
            }
        }




        /// <summary>
        /// Same as Write(string List, string, string, XmlWriterSettings), but without the settings (thanks to John Brett / Codeproject.com)
        /// </summary>
        /// <param name="Nodelist"></param>
        /// <param name="Contentlist"></param>
        /// <param name="Dest"></param>
        /// <param name="Startelement"></param>
        /// <returns></returns>
        public Tuple<bool, string> Write(List<string> Nodelist, List<string> Contentlist, string Dest, string Startelement)
        {
            try
            {
                if (Contentlist.Count != Nodelist.Count)
                {
                    return Tuple.Create<bool, string>(false, "Your Nodelist does not match the Contentlist!");
                }
                XmlWriterSettings xms = new XmlWriterSettings();
                xms.Indent = true;
                xms.NewLineOnAttributes = true;

                XmlWriter wr = XmlWriter.Create(Dest, xms);


                int x = Nodelist.Count;
                int i = 0;

                wr.WriteStartElement(Startelement);
                while (i <= x - 1)
                {
                    wr.WriteElementString(Nodelist.ElementAt(i), Contentlist.ElementAt(i));
                    i++;
                }
                wr.WriteEndElement();
                wr.Close();

                return Tuple.Create<bool, string>(true, "Success");
            }
            catch (Exception ex)
            {
                return Tuple.Create<bool, string>(false, ex.ToString());

            }
        }



        /// <summary>
        /// Read Xml-File and return ElementContent as String
        /// Returns Tuple(bool , List of Strings)
        /// If it doesn't work:
        /// Tuple (false, List.First is the Exception)
        /// </summary>
        /// <param name="Source">File Location</param>
        /// <returns>Tuple(bool, List of Strings)</returns>
        public Tuple<bool, List<string>> Read(string Source)
        {

            List<string> Contentlist = null;
            bool Success = false;
            try
            {


                XmlReader re = XmlReader.Create(Source);
                if (re.Read() == true)
                {
                    re.ReadStartElement();
                }
                while (re.Read())
                {
                    try
                    {
                        Contentlist.Add(re.ReadContentAsString());
                    }
                    catch (Exception ext) //################# Could not find a way for better error handling
                    {
                        try
                        {
                            Contentlist.Add(re.ReadElementContentAsString());
                        }
                        catch (Exception ext2)
                        {
                            Contentlist.Add(re.ReadString());
                        }
                    }
                }
                re.Close();
                Success = true;


                return Tuple.Create(Success, Contentlist);
            }
            catch (Exception ex)
            {
                Success = false;
                Contentlist.Add(ex.ToString());

                return Tuple.Create(Success, Contentlist);
            }
        }
    }
}
