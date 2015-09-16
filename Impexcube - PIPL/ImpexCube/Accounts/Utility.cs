// -----------------------------------------------------------------------
// <copyright file="Utility.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace ImpexCube.Accounts
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Configuration;
    using System.Web;
    using System.Web.Security;
    using System.Web.UI;
    using System.Web.UI.WebControls;
    using System.Web.UI.WebControls.WebParts;
    using System.Web.UI.HtmlControls;
    using System.Data;
    using System.Data.SqlClient;
    using System.Diagnostics;
    using System.Net;
    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class Utility
    {
        string sqlconn = (string)ConfigurationManager.ConnectionStrings["Constr"].ConnectionString;
        public static string GetConnectionString()
        {
            return (string)ConfigurationManager.ConnectionStrings["Constr"].ConnectionString;
        }
        public static int GetNextAutoNo(string custCode,int key, string strCon)
        {
            SqlConnection cnn = new SqlConnection(GetConnectionString());
            cnn.Open();
            string query = "select * from M_AutoGenerate where keyName='" + custCode + "'";
            SqlDataAdapter da = new SqlDataAdapter(query, cnn);
            DataSet ds = new DataSet();
            da.Fill(ds, "AutoNum");
            cnn.Close();
           
            if (ds.Tables["AutoNum"].Rows.Count != 0)
            {
                DataRowView row = ds.Tables["AutoNum"].DefaultView[0];
                 key = Convert.ToInt16(row["keycode"]);
                key = key + 1;
            }
            return key;
        }
        public static int GetNextNo(string custCode)//, string strCon, string FYear, string branch)
        {
            SqlConnection cnn = new SqlConnection(GetConnectionString());
            cnn.Open();
            string query = "Select MAX(CONVERT(int, VchNo)) AS VchNo  from View_T_PaymentDetails where vchtype='" + custCode + "'";// And FinanceYear='" + FYear + "' And BranchCode='" + branch + "'";
            SqlDataAdapter da = new SqlDataAdapter(query, cnn);
            DataSet ds = new DataSet();
            da.Fill(ds, "AutoNum");
            cnn.Close();
            DataRowView row = ds.Tables["AutoNum"].DefaultView[0];
            int grn = Convert.ToInt32(row["VchNo"].ToString());
            return grn;
        }
        public static string GetNextAutoNo(string custCode)//, string strCon, string FYear, string branch)
        {
            SqlConnection cnn = new SqlConnection(GetConnectionString());
            cnn.Open();
            string query = "Select Keycode from M_AutoGenerate where keyname='" + custCode + "'";// And FYear='" + FYear + "' And BranchCode='" + branch + "'";
            SqlDataAdapter da = new SqlDataAdapter(query, cnn);
            DataSet ds = new DataSet();
            da.Fill(ds, "AutoNum");
            cnn.Close();
            DataRowView row = ds.Tables["AutoNum"].DefaultView[0];
            string grn = custCode + "/CHN/13-14/" + row["keycode"].ToString(); // + branch + "/" + FYear + "/" + row["keycode"].ToString();
            return grn;
        }
        public static int GetNextNoReceipt(string custCode)//, string strCon, string FYear, string branch)
        {
            SqlConnection cnn = new SqlConnection(GetConnectionString());
            cnn.Open();
            string query = "Select MAX(CONVERT(int, VchNo)) AS VchNo from View_T_ReceiptDetails";// where  FinanceYear='" + FYear + "' And BranchCode='" + branch + "'";
            SqlDataAdapter da = new SqlDataAdapter(query, cnn);
            DataSet ds = new DataSet();
            da.Fill(ds, "AutoNum");
            cnn.Close();
            DataRowView row = ds.Tables["AutoNum"].DefaultView[0];
            int grn = Convert.ToInt32(row["VchNo"].ToString());
            return grn;
        }
        public static int GetNextNoJournal(string custCode)//, string strCon, string FYear, string branch)
        {
            SqlConnection cnn = new SqlConnection(GetConnectionString());
            cnn.Open();
            string query = "Select MAX(CONVERT(int, VchNo)) AS VchNo from View_JournalDetails";// where  FinanceYear='" + FYear + "' And BranchCode='" + branch + "'";
            SqlDataAdapter da = new SqlDataAdapter(query, cnn);
            DataSet ds = new DataSet();
            da.Fill(ds, "AutoNum");
            cnn.Close();
            DataRowView row = ds.Tables["AutoNum"].DefaultView[0];
            int grn = Convert.ToInt32(row["VchNo"].ToString());
            return grn;
        }
        public static int GetNextNoContra(string custCode)// string strCon, string FYear, string branch)
        {
            SqlConnection cnn = new SqlConnection(GetConnectionString());
            cnn.Open();
            string query = "Select MAX(CONVERT(int, VchNo)) AS VchNo from View_Contra";// where  FinanceYear='" + FYear + "' And BranchCode='" + branch + "'";
            SqlDataAdapter da = new SqlDataAdapter(query, cnn);
            DataSet ds = new DataSet();
            da.Fill(ds, "AutoNum");
            cnn.Close();
            DataRowView row = ds.Tables["AutoNum"].DefaultView[0];
            int grn = Convert.ToInt32(row["VchNo"].ToString());
            return grn;
        }
        public static int GetAddAutoNo(string custCode)//, string strCon, string FYear, string branch)
        {
            SqlConnection cnn = new SqlConnection(GetConnectionString());
            cnn.Open();
            string query = "Select Keycode from M_AutoGenerate where keyname='" + custCode + "'";// And FYear='" + FYear + "' And BranchCode='" + branch + "'";
            SqlDataAdapter da = new SqlDataAdapter(query, cnn);
            DataSet ds = new DataSet();
            da.Fill(ds, "AutoNum");
            cnn.Close();
            DataRowView row = ds.Tables["AutoNum"].DefaultView[0];
            int Keyno = Convert.ToInt32(row["keycode"].ToString());
            int grn = Keyno + 1;
            return grn;
        }
       
        public static bool userentryform(string strCon, string formname, string username)
        {
            string query = "SELECT UserEntryForm  FROM UserAuthorizationForms where UserAuthorizationForm = '" + formname + "' and UserName = '" + username + "'";
            SqlConnection conn = new SqlConnection(strCon);
            conn.Open();
            SqlCommand cmd = new SqlCommand(query, conn);
            SqlDataReader dr = cmd.ExecuteReader();
            dr.Read();
            bool userform = Convert.ToBoolean(dr["UserEntryForm"].ToString());
            return userform;
        }
        public static bool ApprovalEntryForm(string strCon, string formname, string username)
        {
            string query = "SELECT ApprovalEntryForm  FROM UserAuthorizationForms where UserAuthorizationForm = '" + formname + "' and UserName = '" + username + "'";
            SqlConnection conn = new SqlConnection(strCon);
            conn.Open();
            SqlCommand cmd = new SqlCommand(query, conn);
            SqlDataReader dr = cmd.ExecuteReader();
            dr.Read();
            bool approvalform = Convert.ToBoolean(dr["ApprovalEntryForm"].ToString());
            return approvalform;
        }
       
    }
}
