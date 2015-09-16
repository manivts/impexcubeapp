using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace ImpexCube
{
    public class ExchangeRate
    {
        string strcon = (string)ConfigurationManager.ConnectionStrings["Constr"].ConnectionString;
        public string GetImpExchangeRate(string CurrencyShortName)
        {
            string ExRate = "";
            try
            {
                DataSet ds = new DataSet();
                string Query = "Select IMPCurrencyRate from M_Currency where CurrencyShortName='" + CurrencyShortName + "'";
                SqlConnection sqlConn = new SqlConnection(strcon);
                sqlConn.Open();
                SqlDataAdapter da = new SqlDataAdapter(Query, sqlConn);
                da.Fill(ds, "ExchRate");
                sqlConn.Close();
                DataRowView row = ds.Tables["ExchRate"].DefaultView[0];
                ExRate = row["IMPCurrencyRate"].ToString();
            }
            catch (Exception ex)
            {
                string Msg = ex.Message.ToString();
            }
            return ExRate;
        }
    }
}