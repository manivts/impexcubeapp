using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;

namespace VTS.ImpexCube.Data
{
    public class ShippingLineMasterDL
    {
        string con = (string)ConfigurationManager.ConnectionStrings["Constr"].ConnectionString;

        public int InsertShipingMaster(string shipper, string address, string contact, string email, string favor)
        {
            int result = new int();
            string query = "Insert into M_ShippingLineMaster(ShipperName,Address,ContactPerson,Email,Infavorof) " +
                " Values('" + shipper + "','" + address + "','" + contact + "','" + email + "','" + favor + "') ";

            SqlConnection sqlConn = new SqlConnection(con);
            sqlConn.Open();
            SqlCommand cmd = new SqlCommand(query, sqlConn);
            SqlDataAdapter da = new SqlDataAdapter();

            cmd.CommandText = query;
            cmd.Connection = sqlConn;
            da.SelectCommand = cmd;
            result = cmd.ExecuteNonQuery();
            sqlConn.Close();
            return result;
        }

        public DataSet SelectShipingMaster()
        {
            DataSet ds = new DataSet();
            try
            {
                string query = "Select TransId as [Id], ShipperName as [shipper],Address,ContactPerson as [Contact],Email,Infavorof as [Favor] From M_ShippingLineMaster";
                SqlConnection sqlConn = new SqlConnection(con);
                sqlConn.Open();
                SqlDataAdapter da = new SqlDataAdapter(query, sqlConn);
                da.Fill(ds, "shipper");
                sqlConn.Close();
            }
            catch (Exception ex)
            {
                string Msg = ex.Message.ToString();
            }
            return ds;
        }

        public int UpdateShipingMaster(int id, string shipper, string address, string contact, string email, string favor)
        {
            int result = new int();
            string updatecfsmaster = "Update M_ShippingLineMaster set ShipperName ='" + shipper + "' ,Address='" + address + "',ContactPerson='" + contact + "'," +
                "Email='" + email + "',Infavorof='" + favor + "' where TransId='" + id + "'";


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
