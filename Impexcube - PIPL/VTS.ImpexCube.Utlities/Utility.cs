// -----------------------------------------------------------------------
// <copyright file="Utility.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace VTS.ImpexCube.Utlities
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
        public static int GetNextNoContra(string custCode, string strCon, string FYear, string branch)
        {
            SqlConnection cnn = new SqlConnection(strCon);
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
        public static int GetAddAutoNo(string custCode, string FYear, string branch)
        {
            SqlConnection cnn = new SqlConnection(GetConnectionString());
            cnn.Open();
            string query = "Select Keycode from M_AutoGenerate where keyname='" + custCode + "' And FYear='" + FYear + "' And BranchCode='" + branch + "'";
            SqlDataAdapter da = new SqlDataAdapter(query, cnn);
            DataSet ds = new DataSet();
            da.Fill(ds, "AutoNum");
            cnn.Close();
            DataRowView row = ds.Tables["AutoNum"].DefaultView[0];
            int Keyno = Convert.ToInt32(row["keycode"].ToString());
            int grn = Keyno + 1;
            return grn;
        }
        public static int UpdateAutoNo(string custCode, int key, string strCon)
        {
            int result = new int();
            SqlConnection cnn = new SqlConnection(strCon);
            cnn.Open();
            string query = "Update M_AutoGenerate set KeyCode = '" + key + "' where keyName = '" + custCode + "'";
            SqlCommand cmd = new SqlCommand(query, cnn);
            SqlDataAdapter da = new SqlDataAdapter();

            cmd.CommandText = query;
            cmd.Connection = cnn;
            da.SelectCommand = cmd;
            result = cmd.ExecuteNonQuery();
            cnn.Close();
            return result;
        }


        public int InsertJobLog(string username, string jobno)
        {
            int obj = new int();
            string query = "";
            string SystemName = System.Environment.MachineName;
            string clientIPAddress = System.Net.Dns.GetHostAddresses(SystemName).GetValue(0).ToString();
            string ip = HttpContext.Current.Request.UserHostAddress;
            string date = DateTime.Now.ToString();
            SqlConnection con = new SqlConnection(sqlconn);
            con.Open();
            query = "insert into M_ImpexCubeJobLog (UserID,UserName,JobNo,InTime,Login,SystemName,SystemIPAddress) values " +
            " ('" + username + "','" + username + "','" + jobno + "','" + date + "','" + true + "','" + SystemName + "','" + clientIPAddress + "')";
            SqlCommand cmd = new SqlCommand(query, con);
            obj=cmd.ExecuteNonQuery();
            con.Close();
            return obj;
        }
        public int UpdateJobLog(string username)
        {
            int obj = 0;
            try
            {
                string query = "";
                string date = DateTime.Now.ToString();
                SqlConnection con = new SqlConnection(sqlconn);
                con.Open();
                query = "update  M_ImpexCubeJobLog set OutTime='" + date + "',Login='" + false + "' where UserName ='" + username + "'  ";
                SqlCommand cmd = new SqlCommand(query, con);
                obj = cmd.ExecuteNonQuery();
                con.Close();
            }
            catch
            {
            }
            return obj;
        }
        public string SelectJobLog(string username, string jobno)
        {
           string query = "";
           string obj = "";
           SqlConnection con = new SqlConnection(sqlconn);
            con.Open();
            query = "Select * from M_ImpexCubeJobLog where JobNo='" + jobno + "' And Login=1";
            SqlDataAdapter da = new SqlDataAdapter(query, con);
            DataSet ds = new DataSet();
            da.Fill(ds, "Table");
            con.Close();
            if (ds.Tables["Table"].Rows.Count > 0)
            {
                DataRowView view = ds.Tables["Table"].DefaultView[0];
                string usname = view["UserName"].ToString();
                if (usname == username)
                {
                    obj = "NoJob";
                }
                else
                {
                    obj = usname;
                }
            }
            else
            {
                obj = "NoJob";
            }
            return obj;
        }
        public static string GetNextAccountAutoNo(string custCode, string FYear, string branch)
        {
            SqlConnection cnn = new SqlConnection(GetConnectionString());
            cnn.Open();
            string query = "Select Keycode from M_AutoGenerate where keyname='" + custCode + "'";
            SqlDataAdapter da = new SqlDataAdapter(query, cnn);
            DataSet ds = new DataSet();
            da.Fill(ds, "AutoNum");
            cnn.Close();
            string grn = "";
            if (ds.Tables["AutoNum"].Rows.Count != 0)
            {
            DataRowView row = ds.Tables["AutoNum"].DefaultView[0];
            grn = custCode + "/" + branch + "/" + FYear + "/" + row["keycode"].ToString();
            }
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


        public string JobInvSeqNO(string jobNo)
        {
            int invseq = 0;
            string totinv = "";
            string Sub = string.Empty;
            string Invseqno;
            try
            {
                SqlConnection conn2 = new SqlConnection(sqlconn);
                conn2.Open();
                string strQuery = "select InvSeqNo  from T_JobCreation where JobNo='" + jobNo + "'";
                SqlDataAdapter da2 = new SqlDataAdapter(strQuery, conn2);
                DataSet ds2 = new DataSet();
                da2.Fill(ds2, "jobNo");
                conn2.Close();

                if (ds2.Tables["jobNo"].Rows.Count != 0)
                {
                    DataRowView row = ds2.Tables["jobNo"].DefaultView[0];
                    Invseqno = row["InvSeqNo"].ToString();
                    Sub = Invseqno;
                }
                //return Invseqno;
            }
            catch (Exception ex)
            {
            }
            return Sub;
        }

        public string InvSeqNO(string ImporterName, string VchType, string JobNo)
        {
            int invseq = 0;
            string totinv = "";
            try
            {
                SqlConnection conn2 = new SqlConnection(sqlconn);
                conn2.Open();
                string strQuery = "select Rno, IecCode,iectype from M_SPLRunningNo where AccountName='" + ImporterName + "' and  IecCode='" + VchType + "'";
                SqlDataAdapter da2 = new SqlDataAdapter(strQuery, conn2);
                DataSet ds2 = new DataSet();
                da2.Fill(ds2, "INVSEQNO");
                conn2.Close();
                if (ds2.Tables["INVSEQNO"].Rows.Count != 0)
                {
                    DataRowView row = ds2.Tables["INVSEQNO"].DefaultView[0];
                    if (VchType == "SB")
                    {
                        int InNo = Convert.ToInt32(row["Rno"].ToString());
                        string IecType = row["iectype"].ToString();
                        invseq = Convert.ToInt32(InNo) + 1;
                        totinv = Convert.ToString(IecType) + Convert.ToString(invseq);
                    }
                    else
                    {
                        //string  InNo =  row["Rno"].ToString();
                        string IecType = row["iectype"].ToString();
                        string join = JobInvSeqNO(JobNo);
                        string tes = string.Empty;
                        if (IecType == "ATL/DB/")
                        {
                            tes = "8";
                        }
                        else if (IecType == "ATL/DEM/")
                        {
                            tes = "9";
                        }
                        //InNo = join;
                        totinv = Convert.ToString(IecType) + tes + join;
                    }
                }
            }
            catch (Exception ex)
            {
            }
            return totinv;
        }

        public void InvSeqNOSave(string ImporterName, string VchType)
        {
            SqlConnection conn2 = new SqlConnection(sqlconn);
            conn2.Open();
            string strQuery = "select Rno, IecCode from M_SPLRunningNo where AccountName='" + ImporterName + "' and  IecCode='" + VchType + "'";
            SqlDataAdapter da2 = new SqlDataAdapter(strQuery, conn2);
            DataSet ds2 = new DataSet();
            da2.Fill(ds2, "INVSEQNO");
            conn2.Close();
            if (ds2.Tables["INVSEQNO"].Rows.Count != 0)
            {
                DataRowView row = ds2.Tables["INVSEQNO"].DefaultView[0];
                int InNo = Convert.ToInt32(row["Rno"].ToString());
                int invseq = Convert.ToInt32(InNo) + 1;
                SqlConnection conn3 = new SqlConnection(sqlconn);
                conn3.Open();
                string strQuery1 = "update  M_SPLRunningNo set Rno = '" + invseq + "'  where AccountName='" + ImporterName + "' and  IecCode='" + VchType + "'";
                SqlCommand cmd = new SqlCommand(strQuery1, conn3);
                cmd.ExecuteNonQuery();
                //if (VchType == "SB")
                //{
                //    SqlCommand cmd1 = new SqlCommand(strQuery2, conn3);
                //    cmd1.ExecuteNonQuery();
                //}
                conn3.Close();
            }
        }


        public void JobSeqNOSave(string JobNo, string SeqNo)
        {
            SqlConnection conn3 = new SqlConnection(sqlconn);
            conn3.Open();
            string strQuery1 = "update  T_JobCreation set InvSeqNo = '" + SeqNo + "'  where JobNo='" + JobNo + "'";
            SqlCommand cmd = new SqlCommand(strQuery1, conn3);
            cmd.ExecuteNonQuery();
            conn3.Close();
            //if (VchType == "SB")
            //{
            //    SqlCommand cmd1 = new SqlCommand(strQuery2, conn3);
            //    cmd1.ExecuteNonQuery();
            //}
        }
       
    }
}
