using System;
using System.Collections.Generic;
using System.Web;
using System.Data.SqlClient;
using System.Data;

namespace eXpenseService_APIGetURL
{
    public class DataAccess
    {
        public bool TestConnectSQL(string ConnectionString)
        {
            bool ret = false;
            using (SqlConnection objSql = new SqlConnection(ConnectionString))
            {
                try
                {
                    if (objSql.State == System.Data.ConnectionState.Closed)
                    {
                        objSql.Open();
                        //System.Windows.Forms.MessageBox.Show("Connection OK.");
                        ret = true;
                    }
                }
                catch (Exception ex)
                {
                    //System.Windows.Forms.MessageBox.Show("Connection Error. Error Msg: " + ex.Message);
                    ret = false;
                }
            }

            return ret;
        }


        public DataSet SqlSelectCommand(string command, string ConnectionString)
        {
            using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
            {
                DataSet ds = new DataSet();
                if (sqlConnection.State == System.Data.ConnectionState.Closed)
                {
                    sqlConnection.Open();
                }

                SqlDataAdapter adapter = new SqlDataAdapter();
                adapter.SelectCommand = new SqlCommand(command, sqlConnection);
                adapter.Fill(ds);

                return ds;
            }
        }
        public DataTable SqlSelectCommand_table(string command, string ConnectionString)
        {
            using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
            {
                DataTable dt = new DataTable();
                if (sqlConnection.State == System.Data.ConnectionState.Closed)
                {
                    sqlConnection.Open();
                }

                SqlDataAdapter adapter = new SqlDataAdapter();
                adapter.SelectCommand = new SqlCommand(command, sqlConnection);
                adapter.Fill(dt);

                return dt;
            }
        }


        public bool SqlExcuteCommand(string command, string ConnectionString)
        {
            bool ret = false;
            using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
            {
                sqlConnection.Open();
                SqlCommand cmd = new SqlCommand(command, sqlConnection);
                int x = cmd.ExecuteNonQuery();

                if (x > 0)
                {
                    ret = true;
                }
                else
                {
                    ret = false;
                }

                sqlConnection.Close();
            }

            return ret;
        }

        public bool SqlExcuteCommand(string command, string ConnectionString, ref int numberOfRecords)
        {
            bool ret = false;
            numberOfRecords = 0;
            using (SqlConnection sqlConnection = new SqlConnection(ConnectionString))
            {
                sqlConnection.Open();
                SqlCommand cmd = new SqlCommand(command, sqlConnection);
                numberOfRecords = cmd.ExecuteNonQuery();

                if (numberOfRecords > 0)
                {
                    ret = true;
                }
                else
                {
                    ret = false;
                }

                sqlConnection.Close();
            }

            return ret;
        }

    }
}