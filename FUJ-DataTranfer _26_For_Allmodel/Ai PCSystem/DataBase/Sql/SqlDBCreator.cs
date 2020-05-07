using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Windows.Forms;


namespace Ai_PCSystem.DataBase.Sql
{
    public partial class SqlDBCreator
    {       
        private static string strConn2 = @"Data Source=DESKTOP-TVF2A5G\SQLEXPRESS;Initial Catalog=dbCRLine;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        //private static string strConn2 = @"Data Source=DESKTOP-AIL7AGQ\SQLEXPRESS;Initial Catalog=dbCRLine;Integrated Security=True;Connect Timeout=200; pooling=True; Max Pool Size=200; Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        //private static string strConn2 = @"Data Source=DESKTOP-FFRLQ9K\SQLEXPRESS;Initial Catalog=dbCRLine;Integrated Security=True;Connect Timeout=200; pooling=True; Max Pool Size=200; Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
        #region Static Methods      
        /// <summary>
        /// 
        /// </summary>
        /// <param name="schema"></param>
        /// <returns></returns>
        public static string[] GetPrimaryKeys(DataTable schema)
        {
            List<string> keys = new List<string>();
       
            foreach (DataRow column in schema.Rows)
            {
                if (schema.Columns.Contains("IsKey") && (bool)column["IsKey"])

                    keys.Add(column["ColumnName"].ToString());
            }
            return keys.ToArray();

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        /// <param name="columnSize"></param>
        /// <param name="numericPrecision"></param>
        /// <param name="numericScale"></param>
        /// <returns></returns>
        // Return T-SQL data type definition, based on schema definition for a column
        public static string SQLGetType(object type, int columnSize, int numericPrecision, int numericScale)
        {
            switch (type.ToString())
            {
                case "System.String":
                    return "VARCHAR(" + ((columnSize == -1) ? 255 : columnSize) + ")";
                case "System.Decimal":
                    if (numericScale > 0)
                        return "REAL";
                    else if (numericPrecision > 10)
                        return "BIGINT";
                    else
                        return "INT";
                case "System.Double":
                case "System.Single":
                    return "REAL";
                case "System.Int64":
                    return "BIGINT";
                case "System.Int16":
                case "System.Int32":
                    return "INT";
                case "System.DateTime":
                    return "DATETIME";
                default:

                    throw new Exception(type.ToString() + " not implemented.");

            }

        }
        // Overload based on row from schema table
        /// <summary>
        /// 
        /// </summary>
        /// <param name="schemaRow"></param>
        /// <returns></returns>
        public static string SQLGetType(DataRow schemaRow)

        {
            return SQLGetType(schemaRow["DataType"],

                                int.Parse(schemaRow["ColumnSize"].ToString()),

                                int.Parse(schemaRow["NumericPrecision"].ToString()),

                                int.Parse(schemaRow["NumericScale"].ToString()));
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="column"></param>
        /// <returns></returns>
        // Overload based on DataColumn from DataTable type
        public static string SQLGetType(DataColumn column)
        {
            return SQLGetType(column.DataType, column.MaxLength, 10, 2);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="tableName"></param>
        public static void deletesqltable( string tableName)
        {            
            string sqltable = "DROP TABLE " + tableName;

            InsertQuery(sqltable, strConn2);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="schema"></param>
        /// <param name="primaryKeys"></param>
        /// <returns></returns>
        public static string GetCreateSQL(string tableName, DataTable schema, int[] primaryKeys)
        {
            string sql = "CREATE TABLE " + tableName + " (\n";
            // columns
            foreach (DataRow column in schema.Rows)
            {
                if (!(schema.Columns.Contains("IsHidden") && (bool)column["IsHidden"]))

                    sql += column["ColumnName"].ToString() + " " + SQLGetType(column) + ",\n";
            }
            sql = sql.TrimEnd(new char[] { ',', '\n' }) + "\n";
            // primary keys
            string pk = "CONSTRAINT PK_" + tableName + " PRIMARY KEY CLUSTERED (";

            bool hasKeys = (primaryKeys != null && primaryKeys.Length > 0);

            if (hasKeys)
            {
                // user defined keys
                foreach (int key in primaryKeys)
                {
                    pk += schema.Rows[key]["ColumnName"].ToString() + ", ";
                }
            }
            else
            {                
                // check schema for keys
                string keys = string.Join(", ", GetPrimaryKeys(schema));

                pk += keys;

                hasKeys = keys.Length > 0;
            }
            pk = pk.TrimEnd(new char[] { ',', ' ', '\n' }) + ")\n";

            if (hasKeys) sql += pk;

            sql += ")";

            //InsertQuery(sql, strConn);

            return sql;
        }
        ///\r ก็คือสั่งให้ cursor return กลับไปยังต้นบรรทัด รหัส ascii = 10
        ///\n ก็คือให้ขึ้นบรรทัดใหม่ รหัส ascii = 13
        /// <summary>
        /// 
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="table"></param>
        /// <returns></returns>
        public static string GetCreateFromDataTableSQL(string tableName, DataTable table)
        {
            string sql = "CREATE TABLE [" + tableName + "] (\n";
            // columns
            foreach (DataColumn column in table.Columns)
            {
                sql += "[" + column.ColumnName + "] " + SQLGetType(column) + ",\n";
            }
            sql = sql.TrimEnd(new char[] { ',', '\n' }) + "\n";
            // primary keys
            if (table.PrimaryKey.Length > 0)
            {
                sql += "CONSTRAINT [PK_" + tableName + "] PRIMARY KEY CLUSTERED (";

                foreach (DataColumn column in table.PrimaryKey)
                {
                    sql += "[" + column.ColumnName + "],";
                }
                sql = sql.TrimEnd(new char[] { ',' }) + "))\n";
            }
            return sql;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="tablename"></param>
        #endregion
        public static void CreateNewDataBase()
        {
            //your connection string
            // your query:
            var query = GetDbCreationQuery();
            //
            DropDataBase("dbCR_line21");
            //
            var conn = new SqlConnection(strConn2);
            var command = new SqlCommand(query, conn);

            try
            {
                conn.Open();
                command.ExecuteNonQuery();
                MessageBox.Show("Database is created successfully", "MyProgram",
                                MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            finally
            {
                if ((conn.State == ConnectionState.Open))
                {
                    conn.Close();
                    conn.Dispose();
                    conn = null;
                }
            }
        }
        public static Int32 DropDataBase(string DbName)
        {           
            Int32 result = 0;
            try
            {
               
                //SqlConnection con = new SqlConnection();
                //con.ConnectionString = strConn;

                //String sqlCommandText = "DROP DATABASE [" + DbName + "]";
                using (SqlConnection con = new SqlConnection(strConn2))
                {
                    con.Open();
                    String sqlCommandText = @"ALTER DATABASE " + DbName + @" SET SINGLE_USER WITH ROLLBACK IMMEDIATE;DROP DATABASE [" + DbName + "]";
                    SqlCommand sqlCommand = new SqlCommand(sqlCommandText, con);
                    sqlCommand.ExecuteNonQuery();
                }
                //if (con.State == ConnectionState.Closed)
                //{
                //    con.Open();
                //    SqlConnection.ClearPool(con);
                //    con.ChangeDatabase("master");
                //    SqlCommand sqlCommand = new SqlCommand(sqlCommandText, con);
                //    sqlCommand.ExecuteNonQuery();
                //}
                //else
                //{
                //    con.ChangeDatabase("master");
                //    SqlCommand sqlCommand = new SqlCommand(sqlCommandText, con);
                //    sqlCommand.ExecuteNonQuery();
                //}
                //con.Close();
                //con.Dispose();
                //result = 1;
            }
            catch (Exception ex)
            {
                result = 0;
            }
            return result;
        }
        //https://www.dofactory.com/reference/connection-strings
        //https://www.youtube.com/watch?v=-KdqdWTNO7Q
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static string GetDbCreationQuery2()
        {
            String str;
            SqlConnection myConn = new SqlConnection("Server=localhost;Integrated security=SSPI;database=master");

            str = "CREATE DATABASE MyDatabase ON PRIMARY " +
                "(NAME = MyDatabase_Data, " +
                "FILENAME = 'C:\\MyDatabaseData.mdf', " +
                "SIZE = 2MB, MAXSIZE = 10MB, FILEGROWTH = 10%) " +
                "LOG ON (NAME = MyDatabase_Log, " +
                "FILENAME = 'C:\\MyDatabaseLog.ldf', " +
                "SIZE = 1MB, " +
                "MAXSIZE = 5MB, " +
                "FILEGROWTH = 10%)";

            SqlCommand myCommand = new SqlCommand(str, myConn);
            try
            {
                myConn.Open();
                myCommand.ExecuteNonQuery();
                MessageBox.Show("DataBase is Created Successfully", "MyProgram", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.ToString(), "MyProgram", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            finally
            {
                if (myConn.State == ConnectionState.Open)
                {
                    myConn.Close();
                }
            }
            return str;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static string GetDbCreationQuery()
        {
            // your db name
            string dbName = "dbCR_line21";
            // path to your db files:
            // ensure that the directory exists and you have read write permission.
            string[] files = { Path.Combine("C:\\dbCR_Line", dbName + ".mdf"),
                       Path.Combine("C:\\dbCR_Line", dbName + ".ldf") };

            // db creation query:
            // note that the data file and log file have different logical names
            string query = "CREATE DATABASE " + dbName +
                " ON PRIMARY" +
                " (NAME = " + dbName + "_data," +
                " FILENAME = '" + files[0] + "'," +
                " SIZE = 3MB," +
                " MAXSIZE = 10MB," +
                " FILEGROWTH = 10%)" +

                " LOG ON" +
                " (NAME = " + dbName + "_log," +
                " FILENAME = '" + files[1] + "'," +
                " SIZE = 1MB," +
                " MAXSIZE = 5MB," +
                " FILEGROWTH = 10%)" +
                ";";

            return query;
        }
        public static void createsqltables(DataTable dt, string tablename)
        {
            
            string table = "";
            table += "IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[" + tablename + "]') AND type in (N'U'))";
            table += "BEGIN ";
            table += GetCreateFromDataTableSQL(tablename, dt);
            table += "END";
            InsertQuery(table, strConn2);
            CopyData(strConn2, dt, tablename);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="tablename"></param>
        public static void createsqltable(DataTable dt, string tablename)
        {

            string table = "";
            table += "IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[" + tablename + "]') AND type in (N'U'))";
            table += "BEGIN ";
            table += "create table " + tablename + "";
            table += "(";
            for (int i = 0; i < dt.Columns.Count; i++)
            {
                if (i != dt.Columns.Count - 1)
                    table += dt.Columns[i].ColumnName + " " + "varchar(max)" + ",";
                else
                    table += dt.Columns[i].ColumnName + " " + "varchar(max)";
            }
            table += ") ";
            table += "END";
            InsertQuery(table, strConn2);
            CopyData(strConn2, dt, tablename);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="qry"></param>
        /// <param name="connection"></param>
        public static void InsertQuery(string qry, string connection)
        {
            SqlConnection _connection = new SqlConnection(connection);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = qry;
            cmd.Connection = _connection;
            try
            {
                _connection.Open();
                cmd.ExecuteNonQuery();
                _connection.Close();
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.ToString(), "MyProgram", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            finally
            {
                if (_connection.State == ConnectionState.Open)
                {
                    _connection.Close();
                    _connection.Dispose();
                    _connection = null;
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="connStr"></param>
        /// <param name="dt"></param>
        /// <param name="tablename"></param>
        public static void CopyData(string connStr, DataTable dt, string tablename)
        {
            using (SqlBulkCopy bulkCopy =
            new SqlBulkCopy(connStr, SqlBulkCopyOptions.TableLock))
            {
                bulkCopy.DestinationTableName = tablename;
                bulkCopy.WriteToServer(dt);
            }
        }
    }
    
}
