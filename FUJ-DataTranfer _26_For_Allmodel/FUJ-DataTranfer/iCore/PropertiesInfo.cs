using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace FUJ_DataTranfer.iCore
{
    public class PropertiesInfo
    {
        //Ai_Product.Product.PartResult PartResult = null;
        /// <summary>
        /// https://dotnetfiddle.net/PvKRH0
        /// </summary>
        /// <param name="muiltiLine"></param>
        //public static List<string> GetResultPart(Ai_Product.Product.PartResult PartResult)
        //{
        //    List<string> listResultPart = new List<string>();
        //    ///
        //    if (PartResult == null)
        //        return null;
        //    ///
        //    foreach (var properties in typeof(Ai_Product.Product.PartResult).GetProperties())
        //    {
        //        ///  
        //        if (properties.DeclaringType.Name == PartResult.GetType().BaseType.Name)
        //        {

        //        }
        //        else
        //        {
        //            ///
        //            foreach (var item in properties.GetValue(PartResult).GetType().GetProperties())
        //            {
        //                if (item.PropertyType.Name == "List`1") 
        //                    {                            
        //                    ///
        //                    var type = properties.GetValue(PartResult, null);
        //                    ///
        //                    var justRes = (System.Collections.IList)item.GetValue(type);
        //                    ///                    
        //                    foreach (var res in justRes) {
        //                        ///
        //                        var result = (Ai_Product.Product.ItemResult) res;
        //                        listResultPart.Add(result.Data);
        //                        listResultPart.Add(result.Judge);
        //                        listResultPart.Add(result.Name);
        //                    }
        //                }
        //                else
        //                    listResultPart.Add(GetPropertyValue(PartResult, properties.Name + "." + item.Name).ToString());
        //                ///
        //            }
        //        }
        //    }
        //    return listResultPart;
        //}
        public static string GetResultPart(Ai_Product.Product.PartResult PartResult)
        {
            //List<string> listResultPart = new List<string>();
            string listResultPart = null;
            ///
            if (PartResult == null)
                return null;
            ///
            foreach (var properties in typeof(Ai_Product.Product.PartResult).GetProperties()) {
                ///  
                if (properties.DeclaringType.Name == PartResult.GetType().BaseType.Name) {

                }
                else {
                    ///
                    foreach (var item in properties.GetValue(PartResult).GetType().GetProperties()) {
                        ///
                        if (item.PropertyType.Name == "List`1") {
                            ///
                            var type = properties.GetValue(PartResult, null);
                            ///
                            var justRes = (System.Collections.IList)item.GetValue(type);
                            ///  
                            int i = 0;
                            foreach (var res in justRes) {
                                ///
                                var result = (Ai_Product.Product.ItemResult)res;
                                ///
                                if (justRes.Count - 1 == i) {
                                    ///
                                    listResultPart += string.Format("{0},{1},{2}", result.Name, result.Judge, result.Data); //end
                                }
                                else {
                                    listResultPart += string.Format("{0},{1},{2},", result.Name, result.Judge, result.Data);
                                }
                                i++;
                            }
                        }
                        else
                            listResultPart += string.Format("{0},",GetPropertyValue(PartResult, properties.Name + "." + item.Name).ToString());
                        ///
                    }
                }
            }
            return listResultPart;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="muiltiLine"></param>
        public static void SetResultPart(Ai_Product.Product.PartResult PartResult,string strResultList)
        {
            ///
            if (PartResult == null || string.IsNullOrEmpty(strResultList))
                return;
            ///
            string[] arrResult = strResultList.Split(','); int i = 0;
            ///
            foreach (var properties in typeof(Ai_Product.Product.PartResult).GetProperties())
            {
                ///  
                if (properties.DeclaringType.Name == PartResult.GetType().BaseType.Name) 
                {
                  //Check Base class
                }
                else
                {
                    ///
                    foreach (var item in properties.GetValue(PartResult).GetType().GetProperties())
                    {
                        object obj = new object();
                        ///
                        if (item.PropertyType.Name == "List`1")
                        {
                            ///
                            List<Ai_Product.Product.ItemResult> U = new List<Ai_Product.Product.ItemResult>();
                            ///
                            int j = 40;
                            ///
                            int k = (arrResult.Count()-j)/3;
                            ///
                            for (int l = 0; l < k; l++)
                            {
                                U.Add( new Ai_Product.Product.ItemResult()
                                {
                                    Name = arrResult[j++],Judge = arrResult[j++],Data = arrResult[j++]                                    
                                });
                            }
                            obj = U;
                        }
                        else
                        {
                            obj = arrResult[i];
                        }
                        ///
                        var type = properties.GetValue(PartResult, null);
                        ///
                        var subproperties = type.GetType().GetProperty(item.Name);
                        ///
                        subproperties.SetValue(type, obj, null);
                        ///
                        i++;
                    }
                }
              
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="src"></param>
        /// <param name="propName"></param>
        /// <returns></returns>
        public static object GetPropertyValue(object src, string propName)
        {
            if (src == null) throw new ArgumentException("Value cannot be null.", "src");
            if (propName == null) throw new ArgumentException("Value cannot be null.", "propName");

            if (propName.Contains("."))//complex type nested
            {
                var temp = propName.Split(new char[] { '.' }, 2);
                ///
                return GetPropertyValue(GetPropertyValue(src, temp[0]), temp[1]);
            }
            else {
                var prop = src.GetType().GetProperty(propName);
                ///
                return prop != null ? prop.GetValue(src, null) : null;
            }
        }
      
    }
}
