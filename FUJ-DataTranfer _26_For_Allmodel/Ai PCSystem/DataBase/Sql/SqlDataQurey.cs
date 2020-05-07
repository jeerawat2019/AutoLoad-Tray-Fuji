using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Ai_PCSystem.DataBase.Sql
{
    partial class SqlDBCreator
    {
        private static SqlConnection Conn;
        private static SqlCommand Com = new SqlCommand();
        private static SqlTransaction tr;
        private static SqlDataReader dr;
        private static StringBuilder sb = new StringBuilder();
        /// <summary>
        /// 
        /// </summary>
        public static void ConnectSql()
        {
            try
            {

                //string strConn = ConfigurationManager.ConnectionStrings["Northwind"].ConnectionString;
                Conn = new SqlConnection();
                if (Conn.State == ConnectionState.Open)
                {
                    Conn.Close();
                }
                Conn.ConnectionString = strConn2;
                Conn.Open();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString() + " Data base sql can't connection !!!", "ผลการตรวจสอบ", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        private static object DataSql = new object();
        /// <summary>
        /// 
        /// </summary>
        /// <param name="strTableName"></param>
        /// <param name="result"></param>
        public static SqlResultData InsertInto(string strTableName, Dictionary<string, string> result)//,string removeid = null
        {
            try
            {
                lock (DataSql)
                {
                    //if(removeid == "RemoveID") 
                    //    {
                    //    result.Remove("ID");
                    //}
                    if (string.IsNullOrEmpty(strTableName))
                        throw new Exception("");
                    if (result == null)
                        throw new Exception("");
                    else
                    {
                        #region InsertInto Qurey Command

                        tr = Conn.BeginTransaction();
                        sb.Remove(0, sb.Length);
                        //sb.Append("INSERT INTO STATION123 (ST1_B_H2D_CODE,ST1_TIME_IN)");
                        sb.Append("INSERT INTO " + strTableName + " " + "(");
                        result.ToList().ForEach(x =>
                        {
                            if(x.Key !="ID")
                                sb.Append(x.Key + ",");
                        });
                        sb.Remove(sb.Length - 1, 1);
                        sb.Append(")");
                        result.ToList();
                        //sb.Append(" VALUES (@ST1_B_H2D_CODE, @ST1_TIME_IN)");
                        sb.Append(" VALUES (");
                        result.ToList().ForEach(x =>
                        {
                            if (x.Key != "ID")
                                sb.Append("@" + x.Key + ",");
                        });
                        sb.Remove(sb.Length - 1, 1);
                        sb.Append(")");
                        string sqlAdd;
                        sqlAdd = sb.ToString();
                        Com.CommandText = sqlAdd;
                        Com.CommandType = CommandType.Text;
                        Com.Connection = Conn;
                        Com.Transaction = tr;
                        Com.Parameters.Clear();
                        result.ToList().ForEach(x =>
                        {
                            if (x.Key != "ID")
                                Com.Parameters.Add("@" + x.Key, SqlDbType.NVarChar).Value = x.Value;
                        });
                        //Com.Parameters.Add("@ST1_B_H2D_CODE", SqlDbType.NVarChar).Value = "12345";
                        Com.ExecuteNonQuery();
                        tr.Commit();

                        #endregion
                    }
                }

            }
            catch (Exception ex)
            {
                tr.Rollback();
                return new SqlResultData() { Exception = ex ,ExceptionStatus = true};

            }
            return new SqlResultData() { ExceptionStatus = false };

        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static SqlResultData UpdateData(string strTableName, Dictionary<string, string> results)
        {
            DataTable dtStation = null;

            tr = Conn.BeginTransaction();
            try
            {
                if (string.IsNullOrEmpty(strTableName))
                    throw new Exception("");
                if (results == null)
                    throw new Exception("");
                else
                {
                    #region UpdateData Sqlserver 

                    sb.Remove(0, sb.Length);
                    sb.Append("UPDATE " + strTableName);
                    //sb.Append(" SET CompanyName=@CompanyName");
                    sb.Append(" SET ");
                    results.ToList().ForEach(x =>
                    {

                        sb.Append(x.Key + "=@" + x.Key + ",");
                    });
                    sb.Remove(sb.Length - 1, 1);
                    //sb.Append(" WHERE (CustomerID=@CustomerID)");
                    sb.Append(" WHERE (" + results.ToList()[0].Key + "=@" + results.ToList()[0].Key + ")");
                    ///
                    string sqlEdit;
                    sqlEdit = sb.ToString();
                    Com.CommandText = sqlEdit;
                    Com.CommandType = CommandType.Text;
                    Com.Connection = Conn;
                    Com.Transaction = tr;
                    Com.Parameters.Clear();
                    ///
                    results.ToList().ForEach(x =>
                    {
                        Com.Parameters.Add("@" + x.Key, SqlDbType.NVarChar).Value = x.Value;
                    });

                    int result;
                    result = Com.ExecuteNonQuery();
                    if (result == 0)
                    {
                        tr.Rollback();
                    }
                    else
                    {
                        tr.Commit();
                    }
                    #endregion
                }
            }
            catch (Exception ex)
            {
                tr.Rollback();
                return new SqlResultData() { Exception = ex , ExceptionStatus = true };
            }

            return new SqlResultData() { DataTable = dtStation , ExceptionStatus = false };//(dtStation != null)? dtStation : null;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="str2Dcode"></param>
        /// <param name="strTableName"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        public static SqlResultData SelectBy2DCode(string str2Dcode, string strTableName, Dictionary<string, string> result)
        {
            DataTable dtStation = null;
            try
            {
                lock (DataSql)
                {

                    if (str2Dcode.Trim() == "")
                        ///
                        throw new Exception("Data 2DCode Exception");
                    if (string.IsNullOrEmpty(strTableName))
                        ///
                        throw new Exception("Can't name DataTable");
                    if (result == null)
                        ///
                        throw new Exception("Can't Data result");
                    else
                    {
                        #region Select By 2DCode Qurey Command
                        //
                        sb.Remove(0, sb.Length);
                        //sb.Append("SELECT CustomerID,ContactName,Address");
                        sb.Append("SELECT ");
                        result.ToList().ForEach(x =>
                        {
                            sb.Append(x.Key + ",");
                        });
                        sb.Remove(sb.Length - 1, 1);
                        //sb.Append(" FROM Customers");
                        sb.Append(" FROM " + strTableName);
                        //sb.Append(" WHERE (CustomerID=@CustomerID)");
                        sb.Append(" WHERE (" + result.ToList()[0].Key + "=@" + result.ToList()[0].Key + ")");
                        string sqlCustomer = sb.ToString();
                        Com.CommandType = CommandType.Text;
                        Com.CommandText = sqlCustomer;
                        Com.Parameters.Clear();
                        Com.Parameters.Add("@" + result.ToList()[0].Key, SqlDbType.NVarChar).Value = str2Dcode.Trim();

                        Com.Connection = Conn;
                        dr = Com.ExecuteReader();
                        if (dr.HasRows)
                        {
                            dtStation = new DataTable();
                            dtStation.Load(dr);
                        }
                        else
                        {
                            //MessageBox.Show("รหัสที่ป้อน ไม่มี !!!", "ผลการตรวจสอบ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            //throw new Exception("Can't data in table SqlServer function SelectBy2DCode");
                        }
                        #endregion
                    }

                }
            }
            catch (Exception ex)
            {
                return new SqlResultData() { Exception = ex , ExceptionStatus = true };
            }
            finally { dr.Close(); }

            return new SqlResultData() { DataTable = dtStation , ExceptionStatus = false };//(dtStation != null)? dtStation : null;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="strTableName"></param>
        /// <returns></returns>
        public static SqlResultData SelectByTableName(string strDateTime, string strTableName, Dictionary<string, string> result)
        {
            DataTable dtStation = null;
            try
            {
                lock (DataSql)
                {
                    if (strTableName.Trim() == "")
                        //
                        throw new Exception("Can't Defile table name Exception");
                    else
                    {
                        #region Select By 2DCode Qurey Command
                        //
                        sb.Remove(0, sb.Length);
                        //sb.Append("SELECT CustomerID,ContactName,Address");
                        sb.Append("SELECT ");
                        result.ToList().ForEach(x =>
                        {
                            sb.Append(x.Key + ",");
                        });
                        sb.Remove(sb.Length - 1, 1);
                        //sb.Append(" FROM Customers");
                        sb.Append(" FROM " + strTableName);
                        //sb.Append(" WHERE (CustomerID=@CustomerID)");
                        sb.Append(" WHERE (" + result.ToList()[1].Key + "=@" + result.ToList()[1].Key + ")");
                        string sqlCustomer = sb.ToString();
                        Com.CommandType = CommandType.Text;
                        Com.CommandText = sqlCustomer;
                        Com.Parameters.Clear();
                        Com.Parameters.Add("@" + result.ToList()[1].Key, SqlDbType.NVarChar).Value = strDateTime.Trim();

                        Com.Connection = Conn;
                        dr = Com.ExecuteReader();
                        if (dr.HasRows)
                        {
                            dtStation = new DataTable();
                            dtStation.Load(dr);
                        }
                        else
                        {
                            throw new Exception("Can't data in table SqlServer function SelectByTableName");
                        }
                        #endregion
                    }
                }
            }
            catch (Exception ex)
            {
                return new SqlResultData() { Exception = ex , ExceptionStatus = true };

            }
            finally { dr.Close(); }
            ///
            return new SqlResultData() { DataTable = dtStation , ExceptionStatus = false };// (dtStation != null) ? dtStation : null;
        }
        /// <summary>
        /// 
        /// </summary>
        public enum TimeChennel { No1 = 1, No8 = 8 ,No74 = 74};
        /// <summary>
        /// 
        /// </summary>
        /// <param name="strDate"></param>
        /// <param name="strTableName"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        public static SqlResultData SelectTableByDate(string strDate, string strTableName, Dictionary<string, string> result, TimeChennel timeChennel)
        {
            DataTable dtStation = null;
            try
            {
                lock (DataSql)
                {
                    if (strTableName.Trim() == "")
                        ///
                        throw new Exception("Can't Defile table name Exception");
                    else
                    {
                        #region Select By strDate Qurey Command
                        sb.Remove(0, sb.Length);
                        sb.Append(" SELECT *");
                        sb.Append(" FROM " + strTableName);//strTableName
                        sb.Append(" WHERE (" + result.ToList()[(int)timeChennel].Key + " LIKE '%" + strDate + "_%')");//strDate
                        string sqlCustomer = sb.ToString();

                        //DataTable dtCustomer;
                        Com = Conn.CreateCommand();
                        Com.CommandType = CommandType.Text;
                        Com.CommandText = sqlCustomer;
                        Com.Connection = Conn;
                        dr = Com.ExecuteReader();
                        if (dr.HasRows)
                        {
                            dtStation = new DataTable();
                            dtStation.Load(dr);

                        }
                        else
                        {

                            throw new Exception("Can't data in table SqlServer function selectbydate");
                        }
                        dr.Close();
                        #endregion
                    }
                }
            }
            catch (Exception ex)
            {
                return new SqlResultData() { Exception = ex , ExceptionStatus = true };

            }
            finally { dr.Close(); }
            ///
            return new SqlResultData() { DataTable = dtStation, ExceptionStatus = false };//(dtStation != null) ? dtStation : null;
        }
        /// <summary>
        /// 
        /// </summary>
        public class QuereDataFillter
        {
            public static SqlResultData SelectFrom(string strTableName, string searchkey, string datasearch)
            {
                DataTable dtStation = null;
                try
                {
                    lock (DataSql)
                    {
                        if (string.IsNullOrEmpty(strTableName))
                            ///
                            throw new Exception("Can't name DataTable");
                        ///
                        else
                        {
                            #region Select By 2DCode Qurey Command
                            //
                            sb.Remove(0, sb.Length);
                            //sb.Append("SELECT CustomerID,ContactName,Address");
                            sb.Append("SELECT *");
                            ///
                            sb.Append(" FROM " + strTableName);
                            ///
                            sb.Append(" WHERE (" + searchkey + "=@" + searchkey + ")");
                            string sqlCustomer = sb.ToString();
                            Com.CommandType = CommandType.Text;
                            Com.CommandText = sqlCustomer;
                            Com.Parameters.Clear();
                            Com.Parameters.Add("@" + searchkey, SqlDbType.NVarChar).Value = datasearch.Trim();

                            Com.Connection = Conn;
                            dr = Com.ExecuteReader();
                            if (dr.HasRows)
                            {
                                dtStation = new DataTable();
                                dtStation.Load(dr);
                            }
                            else
                            {
                                //MessageBox.Show("รหัสที่ป้อน ไม่มี !!!", "ผลการตรวจสอบ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                throw new Exception("Can't data in table SqlServer function SelectBy 2D Code");
                            }
                            #endregion
                        }

                    }
                }
                catch (Exception ex)
                {
                    return new SqlResultData() { Exception = ex , ExceptionStatus = true };
                }
                finally { dr.Close(); }

                return new SqlResultData() { DataTable = dtStation , ExceptionStatus = false };//(dtStation != null)? dtStation : null;

            }
            /// <summary>
            /// 
            /// </summary>
            /// <param name="strTableName"></param>
            /// <param name="stateDate"></param>
            /// <param name="endDate"></param>
            /// <returns></returns>
            public static SqlResultData SelectBetween(string strTableName, string stateDate, string endDate)
            {
                DataTable dtStation = null;
                try
                {
                    lock (DataSql)
                    {
                        if (string.IsNullOrEmpty(strTableName))
                            ///
                            throw new Exception("Can't name DataTable");

                        if (string.IsNullOrEmpty(stateDate) || string.IsNullOrEmpty(endDate))
                            ///
                            throw new Exception("Can't name formate dateTime");
                        ///
                        else
                        {
                            #region Select By 2DCode Qurey Command
                            //
                            sb.Remove(0, sb.Length);
                            //sb.Append("SELECT CustomerID,ContactName,Address");
                            sb.Append("SELECT *");
                            ///
                            sb.Append(" FROM " + strTableName);
                            ///
                            sb.Append(" WHERE (" + "ST7_TIME_ASSY_FINISH >= @Start" + " AND "+ " ST7_TIME_ASSY_FINISH <= @End" +  ")");
                            string sqlCustomer = sb.ToString();
                            Com.CommandType = CommandType.Text;
                            Com.CommandText = sqlCustomer;
                            Com.Parameters.Clear();
                            Com.Parameters.Add("@" + "Start", SqlDbType.NVarChar).Value = stateDate;
                            Com.Parameters.Add("@" + "End", SqlDbType.NVarChar).Value = endDate;
                            ///
                            Com.Connection = Conn;
                            dr = Com.ExecuteReader();
                            if (dr.HasRows)
                            {
                                dtStation = new DataTable();
                                dtStation.Load(dr);
                            }
                            else
                            {
                              
                                throw new Exception("Can't data in table SqlServer function Select By Date");
                            }
                            #endregion
                        }

                    }
                }
                catch (Exception ex)
                {
                    return new SqlResultData() { Exception = ex , ExceptionStatus = true };
                }
                finally { dr.Close(); }

                return new SqlResultData() { DataTable = dtStation , ExceptionStatus = false};//(dtStation != null)? dtStation : null;

            }
        }
    }
    /// <summary>
    /// 
    /// </summary>
    public class SqlResultData
    {
        /// <summary>
        /// 
        /// </summary>
        public DataTable DataTable = new DataTable();
        /// <summary>
        /// 
        /// </summary>
        public Exception Exception = new Exception();
        /// <summary>
        /// 
        /// </summary>
        public bool ExceptionStatus { get; internal set; }
    }

}
