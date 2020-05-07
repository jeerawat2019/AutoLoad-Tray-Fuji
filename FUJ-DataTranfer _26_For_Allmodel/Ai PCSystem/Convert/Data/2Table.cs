using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Ai_PCSystem.Converts.Data
{
    public class ToTable
    {
       

        public static DataTable ConvertTo<T>(IList<T> list)
        {
            DataTable table = CreateTable<T>();
            Type entityType = typeof(T);
            PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(entityType);

            foreach (T item in list)
            {
                DataRow row = table.NewRow();

                foreach (PropertyDescriptor prop in properties)
                {
                    row[prop.Name] = prop.GetValue(item);
                }

                table.Rows.Add(row);
            }

            return table;
        }

        public static DataTable ConvertTo2<T>(IList<T> list)
        {
            DataTable table = CreateTable<T>();
            Type entityType = typeof(T);
            PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(entityType);

            foreach (T item in list)
            {
                DataRow row = table.NewRow();

                foreach (PropertyDescriptor prop in properties)
                {
                    row[prop.Name] = prop.GetValue(item);
                }

                table.Rows.Add(row);
            }

            return table;
        }
        public static IList<T> ConvertTo<T>(IList<DataRow> rows)
        {
            IList<T> list = null;

            if (rows != null)
            {
                list = new List<T>();

                foreach (DataRow row in rows)
                {
                    T item = CreateItem<T>(row);
                    list.Add(item);
                }
            }

            return list;
        }

        public static IList<T> ConvertTo<T>(DataTable table)
        {
            if (table == null)
            {
                return null;
            }

            List<DataRow> rows = new List<DataRow>();

            foreach (DataRow row in table.Rows)
            {
                rows.Add(row);
            }

            return ConvertTo<T>(rows);
        }

        public static T CreateItem<T>(DataRow row)
        {
            T obj = default(T);
            if (row != null)
            {
                obj = Activator.CreateInstance<T>();

                foreach (DataColumn column in row.Table.Columns)
                {
                    PropertyInfo prop = obj.GetType().GetProperty(column.ColumnName);
                    try
                    {
                        object value = row[column.ColumnName];
                        prop.SetValue(obj, value, null);
                    }
                    catch
                    {
                        // You can log something here
                        throw;
                    }
                }
            }

            return obj;
        }

        public static DataTable CreateTable<T>()
        {
            Type entityType = typeof(T);
            DataTable table = new DataTable(entityType.Name);
            PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(entityType);

            foreach (PropertyDescriptor prop in properties)
            {
                table.Columns.Add(prop.Name, prop.PropertyType);
            }

            return table;
        }
        /////
        ///*Converts DataTable To List*/
        //public static List<TSource> ToList<TSource>(this DataTable dataTable) where TSource : new()
        //{
        //    var dataList = new List<TSource>();

        //    const BindingFlags flags = BindingFlags.Public | BindingFlags.Instance | BindingFlags.NonPublic;
        //    var objFieldNames = (from PropertyInfo aProp in typeof(TSource).GetProperties(flags)
        //                         select new
        //                         {
        //                             Name = aProp.Name,
        //                             Type = Nullable.GetUnderlyingType(aProp.PropertyType) ??
        //                 aProp.PropertyType
        //                         }).ToList();
        //    var dataTblFieldNames = (from DataColumn aHeader in dataTable.Columns
        //                             select new
        //                             {
        //                                 Name = aHeader.ColumnName,
        //                                 Type = aHeader.DataType
        //                             }).ToList();
        //    var commonFields = objFieldNames.Intersect(dataTblFieldNames).ToList();

        //    foreach (DataRow dataRow in dataTable.AsEnumerable().ToList())
        //    {
        //        var aTSource = new TSource();
        //        foreach (var aField in commonFields)
        //        {
        //            PropertyInfo propertyInfos = aTSource.GetType().GetProperty(aField.Name);
        //            var value = (dataRow[aField.Name] == DBNull.Value) ?
        //            null : dataRow[aField.Name]; //if database field is nullable
        //            propertyInfos.SetValue(aTSource, value, null);
        //        }
        //        dataList.Add(aTSource);
        //    }
        //    return dataList;
        //}
    }
    
}
