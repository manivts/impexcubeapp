using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;

namespace VTS.ImpexCube.Data
{
    public class JobCreationDL
    {
        string strconn = (string)ConfigurationManager.ConnectionStrings["Constr"].ConnectionString;

        public DataSet GetCountry()
        {
            StringBuilder Query = new StringBuilder();
            DataSet ds = new DataSet();
            try
            {
                Query.Append("SELECT Country FROM Country_Master");

                string Qry = Query.ToString();
                SqlConnection sqlConn = new SqlConnection(strconn);
                sqlConn.Open();
                SqlDataAdapter da = new SqlDataAdapter(Qry, sqlConn);

                da.Fill(ds, "Contry");
                sqlConn.Close();
            }
            catch (Exception ex)
            {
                string Msg = ex.Message.ToString();
            }
            return ds;
        }

        public DataSet GetPort()
        {
            StringBuilder Query = new StringBuilder();
            DataSet ds = new DataSet();
            try
            {
                Query.Append("SELECT ([Port_Name]+ '|' +[Port_Code]) as Port,[Port_Code] FROM Port_Mst");

                string Qry = Query.ToString();
                SqlConnection sqlConn = new SqlConnection(strconn);
                sqlConn.Open();
                SqlDataAdapter da = new SqlDataAdapter(Qry, sqlConn);

                da.Fill(ds, "PortDtl");
                sqlConn.Close();
            }
            catch (Exception ex)
            {
                string Msg = ex.Message.ToString();
            }
            return ds;
        }

        public DataSet GetPortCode(string PortCode)
        {
            StringBuilder Query = new StringBuilder();
            DataSet ds = new DataSet();
            try
            {
                Query.Append("SELECT [Port_Code] FROM Port_Mst where [Port_Code] = '" + PortCode + "' ");

                string Qry = Query.ToString();
                SqlConnection sqlConn = new SqlConnection(strconn);
                sqlConn.Open();
                SqlDataAdapter da = new SqlDataAdapter(Qry, sqlConn);

                da.Fill(ds, "PortCode");
                sqlConn.Close();
            }
            catch (Exception ex)
            {
                string Msg = ex.Message.ToString();
            }
            return ds;
        }

    }
}
