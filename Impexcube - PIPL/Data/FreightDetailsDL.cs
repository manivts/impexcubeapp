// -----------------------------------------------------------------------
// <copyright file="FreightDetailsDL.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace VTS.ImpexCube.Data
{
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Data.SqlClient;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class FreightDetailsDL
    {
        string strconn = (string)ConfigurationManager.ConnectionStrings["Constr"].ConnectionString;

        public int InsertFreightDetails(string invoiceno, double freightcurrency, double freightRate, double freightAmount, double insurancecurrency, double insuranceRate, double insuranceAmount, double discountcurrency, double discountRate, double discountAmount,
            int createdBy, DateTime createdDate, int modifiedBy, DateTime modifiedDate)
        {
            StringBuilder Query = new StringBuilder();
            SqlConnection sqlConn = new SqlConnection(strconn);
            int result = new int();

            try
            {
                Query.Append("INSERT INTO T_FreightDetails (InvoiceNo, FreightCurrency, FreightRate, FreightAmount, InsuranceCurrency, InsuranceRate, InsuranceAmount,");
                Query.Append(" DiscountCurrency, DiscountRate, DiscountAmount, CreatedBy, CreatedDate, ModifiedBy, ModifiedDate)");
                Query.Append("VALUES (");
                Query.Append(" '" + invoiceno + "'," + freightcurrency + ", " + freightRate + ", " + freightAmount + "," + insurancecurrency + "," + insuranceRate + "," + insuranceAmount + "," + discountcurrency + ",");
                Query.Append(" " + discountRate + ", " + discountAmount + " , " + createdBy + ",'" + createdDate + "'," + modifiedBy + ",'" + modifiedDate + "' )");

                sqlConn.Open();
                SqlCommand cmd = new SqlCommand(Query.ToString(), sqlConn);
                SqlDataAdapter da = new SqlDataAdapter();

                cmd.CommandText = Query.ToString();
                cmd.Connection = sqlConn;
                da.SelectCommand = cmd;
                result = cmd.ExecuteNonQuery();               

            }
            catch (Exception ex)
            {
                string Msg = ex.Message.ToString();
            }
            finally
            {
                sqlConn.Close();
            }
            return result;
        }
    }
}
