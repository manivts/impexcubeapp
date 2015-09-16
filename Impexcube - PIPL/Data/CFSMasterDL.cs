using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;


namespace VTS.ImpexCube.Data
{
    public class CFSMasterDL
    {
        string con = (string)ConfigurationManager.ConnectionStrings["Constr"].ConnectionString;
        public int InsertCFSMaster(string cfsname, string address, string contact, string email, string favor)
        {
            int result = new int();
            string insertcfsmaster="Insert into M_CFSMaster(CfsName,Address,ContactPerson,Email,Infavorof) "+
                " Values('" + cfsname + "','"+address+"','"+contact+"','"+email+"','"+favor+"') ";
            SqlConnection sqlConn = new SqlConnection(con);
            sqlConn.Open();
            SqlCommand cmd = new SqlCommand(insertcfsmaster, sqlConn);
            SqlDataAdapter da = new SqlDataAdapter();

            cmd.CommandText = insertcfsmaster;
            cmd.Connection = sqlConn;
            da.SelectCommand = cmd;
            result = cmd.ExecuteNonQuery();
            sqlConn.Close();
            return result;
        }

        public DataSet SelectCFSMaster()
        {
            DataSet ds = new DataSet();
            try
            {
                string query = "Select TransId as [Id], CfsName as [CFS],Address,ContactPerson as [Contact],Email,Infavorof as [Favor] From M_CFSMaster";
                SqlConnection sqlConn = new SqlConnection(con);
                sqlConn.Open();
                SqlDataAdapter da = new SqlDataAdapter(query,sqlConn);
                da.Fill(ds, "cfs");
                sqlConn.Close();
            }
            catch (Exception ex)
            {
                string Msg = ex.Message.ToString();
            }
            return ds;
        }

        public int UpdateCFSMaster(int id, string cfsname, string address, string contact, string email, string favor)
        {
            int result = new int();
            string updatecfsmaster = "Update M_CFSMaster set CfsName ='" + cfsname + "' ,Address='" + address + "',ContactPerson='" + contact + "',"+
                "Email='" + email + "',Infavorof='" + favor + "' where TransId='"+id+"'";
                

            SqlConnection sqlConn = new SqlConnection(con);
            sqlConn.Open();
            SqlCommand cmd = new SqlCommand(updatecfsmaster, sqlConn);
            SqlDataAdapter da = new SqlDataAdapter();

            cmd.CommandText = updatecfsmaster;
            cmd.Connection = sqlConn;
            da.SelectCommand = cmd;
            result = cmd.ExecuteNonQuery();
            sqlConn.Close();
            return result;
        }

    }
}
