using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace Queries
{
    class SqlQueries
    {
        SqlConnection conn;
        SqlCommand cmd;
        SqlDataReader rdr;

        public SqlQueries()
        {
            conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
        }

        public void Connect()
        {
            conn.Open();
        }

        public void Disconnect()
        {
            conn.Close();
        }

        public void SetCommand(string query)
        {
            cmd = new SqlCommand(query,conn);
        }

        public DataTable RunSelectQuery()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            return dt;
        }

        public Boolean CheckUserLogin
            (string password)
        {
            rdr = cmd.ExecuteReader();
            if(rdr.Read() && rdr["Password"].ToString().Equals(password))
            {
                
                rdr.Close();
                return true;
            }
                rdr.Close();
                return false;
        }

        

        public void RunInsertQuery()
        {
            cmd.ExecuteNonQuery();
        }

        public void RunUpdateQuery()
        {
            cmd.ExecuteNonQuery();
        }

        public void AddParameters(string query, object parameter)
        {
            cmd.Parameters.AddWithValue(query, parameter);
        }

         public int ExecuteNonQuery()
        {
            return cmd.ExecuteNonQuery();
        }
    }
}
