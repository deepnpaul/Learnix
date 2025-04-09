using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace DNP_OLSM_DS
{
    public class DBAccess : IDisposable
    {

        public DBAccess()
        {
        }

        protected SqlCommand oCommand;
        protected SqlDataAdapter oDataAdapter;
        public static string? gConnectionString;
        private string gError;

       

        public void SetConnectionstring(string? connectionString)
        {
            gConnectionString = connectionString;
        }


        public void Dispose() { }

        public SqlConnection DBConnection()
        {
            SqlConnection oConnection;
            oConnection = new SqlConnection(gConnectionString);
            oConnection.Open();
            return oConnection;
        }

        public DataSet getDataSet(string pSqlQry)
        {
            DataSet oDataSet = new();
            oCommand = new SqlCommand();
            try
            {
                oCommand.Connection = DBConnection();
                oCommand.CommandText = pSqlQry;
                oCommand.CommandType = CommandType.Text;
                oDataAdapter = new SqlDataAdapter(oCommand);
                oDataAdapter.Fill(oDataSet);
            }
            catch (Exception ex)
            {
            }
            finally
            {
                oCommand.Connection.Close();
            }
            return oDataSet;
        }

        public DataSet getDataSet(string StoreProdure, SqlParameter[] Parameters)
        {
            DataSet oDataSet = new();
            oDataAdapter = new System.Data.SqlClient.SqlDataAdapter();
            oCommand = new SqlCommand();
            try
            {
                oCommand.Connection = DBConnection();
                oCommand.CommandText = StoreProdure;
                oCommand.CommandType = CommandType.StoredProcedure;
                foreach (SqlParameter p in Parameters)
                {
                    oCommand.Parameters.Add(p);
                }
                oDataAdapter = new SqlDataAdapter(oCommand);
                oDataAdapter.Fill(oDataSet);
            }
            catch (Exception ex)
            {

            }
            finally
            {
                oCommand.Connection.Close();
            }
            return oDataSet;
        }

        public void ExecuteNonQuery(string StoreProdure, SqlParameter[] Parameters)
        {
            oCommand = new SqlCommand();
            try
            {
                oCommand.Connection = DBConnection();
                oCommand.CommandText = StoreProdure;
                oCommand.CommandType = CommandType.StoredProcedure;
                foreach (SqlParameter p in Parameters)
                {
                    oCommand.Parameters.Add(p);
                }

                oCommand.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
            }
            finally
            {
                oCommand.Parameters.Clear();
                oCommand.Connection.Close();
            }
        }

        public Int32 ExecuteNonQuerywithReturnID(string StoreProdure, SqlParameter[] Parameters)
        {
            oCommand = new SqlCommand();
            Int32 id = 0;
            try
            {
                oCommand.Connection = DBConnection();
                oCommand.CommandText = StoreProdure;
                oCommand.CommandType = CommandType.StoredProcedure;
                foreach (SqlParameter p in Parameters)
                {
                    oCommand.Parameters.Add(p);
                }

                //oCommand.ExecuteNonQuery();
                id = (int)oCommand.ExecuteScalar();
            }
            catch (Exception ex)
            {
            }
            finally
            {
                oCommand.Parameters.Clear();
                oCommand.Connection.Close();
            }

            return id;
        }

        public DataTable GetDataTable(string pSql)
        {
            DataSet oDataSet = new();
            oCommand = new SqlCommand();
            try
            {
                oCommand.Connection = DBConnection();
                oCommand.CommandText = pSql;
                oCommand.CommandType = CommandType.Text;
                oDataAdapter = new SqlDataAdapter(oCommand);
                oDataAdapter.Fill(oDataSet);
            }
            catch (Exception ex)
            {

            }
            finally
            {
                oCommand.Connection.Close();
            }
            return oDataSet.Tables[0];
        }

        public Boolean ExeNonQuery(string pSqlQry)
        {
            oCommand = new SqlCommand();
            int i = 0;
            Boolean Flag = false;
            try
            {
                oCommand.Connection = DBConnection();
                oCommand.CommandText = pSqlQry;
                oCommand.CommandType = CommandType.Text;

                i = oCommand.ExecuteNonQuery();
                if (i > 0)
                {
                    Flag = true;
                }
                else
                {
                    Flag = false;
                }
            }

            catch (Exception ex)
            {
                Flag = false;
            }
            finally
            {
                oCommand.Connection.Close();
            }
            return Flag;
        }

        public SqlDataReader getDataReader(string pSqlQry)
        {
            SqlDataReader oDataReader = null;
            oCommand = new SqlCommand();
            try
            {
                oCommand.Connection = DBConnection();
                oCommand.CommandText = pSqlQry;
                oCommand.CommandType = CommandType.Text;
                oDataReader = oCommand.ExecuteReader();

            }
            catch (Exception ex)
            {

            }
            finally
            {
                oCommand.Connection.Close();
            }
            return oDataReader;
        }

        public string DBqueryExecuteScaler(string pSqlQry)
        {
            string oResult = null;
            oCommand = new SqlCommand();
            try
            {
                oCommand.Connection = DBConnection();
                oCommand.CommandText = pSqlQry;
                oCommand.CommandType = CommandType.Text;
                oResult = oCommand.ExecuteScalar().ToString();
            }
            catch (Exception ex)
            {

            }
            finally
            {
                oCommand.Connection.Close();
            }

            return oResult;
        }


        public Boolean ExecuteNonQueryWithImage(string pSql, Hashtable param)
        {
            Boolean Flag = false;
            SqlCommand oCommand = new();
            int i = 0;

            try
            {
                oCommand.Connection = DBConnection();
                oCommand.CommandText = pSql;
                if (param.Count > 0)
                {
                    foreach (string key in param.Keys)
                    {
                        oCommand.Parameters.AddWithValue("@" + key, param[key]);
                    }
                }

                i = oCommand.ExecuteNonQuery();
                if (i > 0)
                {
                    Flag = true;
                }
                else
                {
                    Flag = false;
                }
            }
            catch (Exception ex)
            {
                Flag = false;
            }
            finally
            {
                oCommand.Connection.Close();
            }
            return Flag;
        }

       
    }
}
