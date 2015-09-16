
// -----------------------------------------------------------------------
// <copyright file="FundRequest.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace VTS.ImpexCube.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Configuration;
    using System.Data;
    using System.Data.SqlClient;
    using MySql.Data.MySqlClient;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class FundRequest
    {
        string strcon = (string)ConfigurationManager.ConnectionStrings["Constr"].ConnectionString;
        //string strconn = (string)ConfigurationManager.AppSettings["connectionJSU"];
        //string strconnVi = (string)ConfigurationManager.AppSettings["ConnectionVisual"];
        //string strPIPL = (string)ConfigurationManager.AppSettings["ConnectionImpex"];

        public DataSet jobno(string jobno)
        {
            DataSet ds = new DataSet();
            try
            {
                string Query = "select distinct JOBNO from T_JobCreation order by jobno ";

                SqlConnection con = new SqlConnection(strcon);
                con.Open();
                SqlDataAdapter sd = new SqlDataAdapter(Query, con);
                sd.Fill(ds, "jobno");
                con.Close();
            }
            catch (Exception ex)
            {
                string Msg = ex.Message.ToString();
            }
            return ds;
        }

        public DataSet Expjobno(string jobno)
        {
            DataSet ds = new DataSet();
            try
            {
                string Query = "select distinct JOBNO from E_M_JobCreation order by jobno";

                SqlConnection con = new SqlConnection(strcon);
                con.Open();
                SqlDataAdapter sd = new SqlDataAdapter(Query, con);
                sd.Fill(ds, "jobno");
                con.Close();
            }
            catch (Exception ex)
            {
                string Msg = ex.Message.ToString();
            }
            return ds;
        }


        public DataSet ImporterName(string jobno)
        {
            DataSet ds = new DataSet();
            try
            {
                string Query = "select Importer from T_Importer where jobno='" + jobno + "' ";

                SqlConnection con = new SqlConnection(strcon);
                con.Open();
                SqlDataAdapter da = new SqlDataAdapter(Query, con);

                da.Fill(ds, "importer");
                con.Close();
            }
            catch (Exception ex)
            {
                string Msg = ex.Message.ToString();
            }
            return ds;
        }

        public DataSet ExportName(string jobno)
        {
            DataSet ds = new DataSet();
            try
            {
                string Query = "SELECT p.party_name FROM eworkreg e,prt_mast p where e.party_code=p.party_code and job_no='" + jobno + "' ";

                MySqlConnection con = new MySqlConnection(strcon);
                con.Open();
                MySqlDataAdapter da = new MySqlDataAdapter(Query, con);

                da.Fill(ds, "Export");
                con.Close();
            }
            catch (Exception ex)
            {
                string Msg = ex.Message.ToString();
            }
            return ds;
        }

        public int InsertFundRequest(string FundRequestNo, string FundRequestDate, string Jobno, string Importer, string RequestAmt, string RequestDate,
            string MOP, string RequestBy, string Purpose, string UserRemarks, string approved, string completed, string Cfsname, string Shipping)
        {
            int result = new int();
            string insertFundRequest = "Insert Into T_FundRequest(FundRequestNo,FundRequestDate,JobNo,ImporterName,RequestAmt,RequestDate,MOP,RequestBy,Purpose,UserRemarks,Approved,Completed,CfsName,ShippingName)" +
                "Values ('" + FundRequestNo + "','" + frmdatesplit(FundRequestDate) + "','" + Jobno + "','" + Importer + "','" + RequestAmt + "','" + frmdatesplit(RequestDate) + "'," +
                " '" + MOP + "', '" + RequestBy + "', '" + Purpose + "' , '" + ConvertAmp(UserRemarks) + "'," + approved + ", " + completed + ",'" + Cfsname + "','" + Shipping + "')";

            SqlConnection sqlConn = new SqlConnection(strcon);
            sqlConn.Open();
            SqlCommand cmd = new SqlCommand(insertFundRequest, sqlConn);
            SqlDataAdapter da = new SqlDataAdapter();

            cmd.CommandText = insertFundRequest;
            cmd.Connection = sqlConn;
            da.SelectCommand = cmd;
            result = cmd.ExecuteNonQuery();
            sqlConn.Close();
            return result;
        }

        private string frmdatesplit(string frmdate)
        {
            string[] frmdate1 = frmdate.Split('/');
            string frmdate2 = frmdate1[1] + '/' + frmdate1[0] + '/' + frmdate1[2];
            return frmdate2;
        }

        public DataSet SelectedFundRequest(string fundNo)
        {
            DataSet ds = new DataSet();
            try
            {
                string Query = "select FundRequestNo as [Request No],Convert(Varchar(12),FundRequestDate,103) As [Fund Date],JobNo,ImporterName As [Customer], RequestAmt as [Amount], Convert(Varchar(12),RequestDate,103) As [Request Date],"+
                    " MOP as [Payment],RequestBy, Purpose, UserRemarks as [Remarks], ApprovedAmt as [ApdAmt], Convert(Varchar(12),ApprovalDate,103) As [App Date], ApprovalRemarks as [Apl Remarks], Approved, Completed,CfsName,ShippingName from T_FundRequest where FundRequestNo='" + fundNo + "' and Active=0";

                SqlConnection sqlConn = new SqlConnection(strcon);
                sqlConn.Open();
                SqlDataAdapter da = new SqlDataAdapter(Query, sqlConn);

                da.Fill(ds, "FundDetails");
                sqlConn.Close();
            }   
            catch (Exception ex)
            {
                string Msg = ex.Message.ToString();
            }
            return ds;
        }

        public DataSet SelectedPendingRequest(string fundNo)
        {
            DataSet ds = new DataSet();
            try
            {
                string Query = "select FundRequestNo as [Request No],Convert(Varchar(12),FundRequestDate,103) As [Fund Date],JobNo,ImporterName As [Customer], RequestAmt as [Amount], Convert(Varchar(12),RequestDate,103) As [Request Date]," +
                    " MOP as [Payment],RequestBy, UserRemarks as [Remarks],Approved, Convert(Varchar(12),ApprovalDate,103) As [Approved Date],ApprovedAmt as [Approved Amount]," +
                    " ApprovalRemarks as [Approval Remarks], PaymentAmt as [PayAmount], PaymentMode as [PayMode],PaymentStatus as [status], PayRemarks, Completed from T_FundRequest where FundRequestNo='" + fundNo + "' and Active=0";

                SqlConnection sqlConn = new SqlConnection(strcon);
                sqlConn.Open();
                SqlDataAdapter da = new SqlDataAdapter(Query, sqlConn);

                da.Fill(ds, "FundDetails");
                sqlConn.Close();
            }
            catch (Exception ex)
            {
                string Msg = ex.Message.ToString();
            }
            return ds;
        }

        public DataSet GridLoad(string jobno)
        {
            DataSet ds = new DataSet();
            try
            {
                string Query = "select FundRequestNo as [Request No],Convert(Varchar(12),RequestDate,103) As [Request Date],JobNo,ImporterName As [Customer],RequestAmt as [Amount] from T_FundRequest where JobNo='" + jobno + "' and Approved = 0 and Completed = 0 and Active=0 ";

                SqlConnection sqlConn = new SqlConnection(strcon);
                sqlConn.Open();
                SqlDataAdapter da = new SqlDataAdapter(Query, sqlConn);

                da.Fill(ds, "fund");
                sqlConn.Close();
            }
            catch (Exception ex)
            {
                string Msg = ex.Message.ToString();
            }
            return ds;
        }

        public DataSet ApprovedGrid()
        {
            DataSet ds = new DataSet();
            try
            {
                string Query = "select FundRequestNo as [Request No],Convert(Varchar(12),RequestDate,103) As [Request Date],JobNo,ImporterName As [Customer],ApprovedAmt as [Amount] from T_FundRequest where Approved = 1 and Completed = 0 and Active=0";

                SqlConnection sqlConn = new SqlConnection(strcon);
                sqlConn.Open();
                SqlDataAdapter da = new SqlDataAdapter(Query, sqlConn);

                da.Fill(ds, "fund");
                sqlConn.Close();
            }
            catch (Exception ex)
            {
                string Msg = ex.Message.ToString();
            }
            return ds;
        }

        public int UpdateFundRequest(string FundRequestNo, string FundRequestDate, string Jobno, string Importer, string RequestAmt, string RequestDate,
            string MOP, string RequestBy, string Purpose, string UserRemarks, string Cfsname, string ShippingName)
        {
            int result = new int();
            string updateFundRequest = "Update T_FundRequest Set FundRequestNo='" + FundRequestNo + "',FundRequestDate='" + frmdatesplit(FundRequestDate) + "',JobNo='" + Jobno + "'," +
                        " ImporterName='" + Importer + "',RequestAmt='" + RequestAmt + "',RequestDate='" + frmdatesplit(RequestDate) + "',MOP='" + MOP + "',RequestBy='" + RequestBy + "'," +
                        " Purpose='" + Purpose + "', UserRemarks='" + ConvertAmp(UserRemarks) + "',CfsName='" + Cfsname + "',ShippingName='" + ShippingName + "' where  FundRequestNo='" + FundRequestNo + "'";
            SqlConnection sqlConn = new SqlConnection(strcon);
            sqlConn.Open();
            SqlCommand cmd = new SqlCommand(updateFundRequest, sqlConn);
            SqlDataAdapter da = new SqlDataAdapter();

            cmd.CommandText = updateFundRequest;
            cmd.Connection = sqlConn;
            da.SelectCommand = cmd;
            result = cmd.ExecuteNonQuery();
            sqlConn.Close();
            return result;
        }

        public int UpdateApproveRequest(string FundRequestNo, string approved, string approvedDate, string ApdAmt,string AmountFrom, string Remarks, string completed,string Active)
        {
            int result = new int();
            string updateFundRequest = "Update T_FundRequest Set Approved=" + approved + ",ApprovalDate='" + frmdatesplit(approvedDate) + "',ApprovedAmt='" + ApdAmt  + "'," +
                        " ApprovalAmtFrom= '" + AmountFrom + "' ,ApprovalRemarks='" + ConvertAmp(Remarks) + "' , Completed =" + completed + " , Active =" + Active + " where  FundRequestNo='" + FundRequestNo + "'";
            SqlConnection sqlConn = new SqlConnection(strcon);
            sqlConn.Open();
            SqlCommand cmd = new SqlCommand(updateFundRequest, sqlConn);
            SqlDataAdapter da = new SqlDataAdapter();

            cmd.CommandText = updateFundRequest;
            cmd.Connection = sqlConn;
            da.SelectCommand = cmd;
            result = cmd.ExecuteNonQuery();
            sqlConn.Close();
            return result;
        }

        public DataSet ApprovedFundRequest(string fundNo)
        {
            DataSet ds = new DataSet();
            try
            {
                string Query = "select FundRequestNo as [Request No],Convert(Varchar(12),FundRequestDate,103) As [Fund Date],JobNo,ImporterName As [Customer], RequestAmt as [Amount], Convert(Varchar(12),RequestDate,103) As [Request Date]," +
                    " MOP as [Payment],RequestBy,Purpose, UserRemarks as [Remarks],Approved, Convert(Varchar(12),ApprovalDate,103) As [Approved Date],ApprovedAmt as [Approved Amount],"+
                    " ApprovalAmtFrom as [AmountFrom],ApprovalRemarks as [Approval Remarks],PaymentAmt as [PayAmt], PaymentMode as [PayMode], PaymentStatus as [PayStatus], ChequeDDNo,"+
                    " DDChequeDate, BankName, DrewBank, PayRemarks , PayBalance as [Balance],Completed,CfsName,ShippingName from T_FundRequest where FundRequestNo='" + fundNo + "' and Active=0 ";

                SqlConnection sqlConn = new SqlConnection(strcon);
                sqlConn.Open();
                SqlDataAdapter da = new SqlDataAdapter(Query, sqlConn);

                da.Fill(ds, "FundDetails");
                sqlConn.Close();
            }
            catch (Exception ex)
            {
                string Msg = ex.Message.ToString();
            }
            return ds;
        }

        public DataSet PartialAmtBalance(string fundNo)
        {
            DataSet ds = new DataSet();
            try
            {
                string Query = "select FundRequestNo as [Request No],ApprovedAmt as [Actual],PaymentAmt as [Paid], PaymentMode as [Mode], PaymentStatus as [Status], PayBalance as [Balance] from T_FundRequest where FundRequestNo='" + fundNo + "' and Active=0";

                SqlConnection sqlConn = new SqlConnection(strcon);
                sqlConn.Open();
                SqlDataAdapter da = new SqlDataAdapter(Query, sqlConn);

                da.Fill(ds, "FundDetails");
                sqlConn.Close();
            }
            catch (Exception ex)
            {
                string Msg = ex.Message.ToString();
            }
            return ds;
        }

        public DataSet ManagerApprovedList()
        {
            DataSet ds = new DataSet();
            try
            {
                string Query = "select FundRequestNo as [Request No],Convert(Varchar(12),FundRequestDate,103) As [Date],JobNo,ImporterName As [Customer],PaymentAmt as [Amount] from T_FundRequest where Approved = 1 and Completed = 1 and Active=0";

                SqlConnection sqlConn = new SqlConnection(strcon);
                sqlConn.Open();
                SqlDataAdapter da = new SqlDataAdapter(Query, sqlConn);

                da.Fill(ds, "fund");
                sqlConn.Close();
            }
            catch (Exception ex)
            {
                string Msg = ex.Message.ToString();
            }
            return ds;
        }

        public int UpdateApprovedList(string FundRequestNo, string PayMode, string chqdd, string chqdate, string bank, string drewbank, string PayAmt, string status, string balance, string Remarks, string completed)
        {
            int result = new int();
            try
            {
              
                string cheqdate = string.Empty;
                if (chqdate != "")
                {
                    cheqdate = frmdatesplit(chqdate);
                }
                else
                    cheqdate = chqdate;
                string updateFundRequest = "Update T_FundRequest Set PaymentMode='" + PayMode + "',ChequeDDNo='" + chqdd + "', DDChequeDate= '" + cheqdate + "' ," +
                            " BankName ='" + bank + "', DrewBank='" + drewbank + "' ,PaymentAmt='" + PayAmt + "'," +
                            " PaymentStatus='" + status + "', PayBalance = " + balance + ", PayRemarks='" + ConvertAmp(Remarks) + "' , Completed =" + completed + " where  FundRequestNo='" + FundRequestNo + "'  ";
                SqlConnection sqlConn = new SqlConnection(strcon);
                sqlConn.Open();
                SqlCommand cmd = new SqlCommand(updateFundRequest, sqlConn);
                SqlDataAdapter da = new SqlDataAdapter();

                cmd.CommandText = updateFundRequest;
                cmd.Connection = sqlConn;
                da.SelectCommand = cmd;
                result = cmd.ExecuteNonQuery();
                sqlConn.Close();
              
            }
            catch (Exception ex)
            { 
            
            }
            return result;
        }

        public DataSet PendingGridLoad()
        {
            DataSet ds = new DataSet();
            try
            {
                string Query = "select FundRequestNo as [Request No],Convert(Varchar(12),RequestDate,103) As [Request Date],JobNo,ImporterName As [Customer],RequestAmt as [Amount] from T_FundRequest where Approved= 0 and Completed = 0 and Active=0 ";

                SqlConnection sqlConn = new SqlConnection(strcon);
                sqlConn.Open();
                SqlDataAdapter da = new SqlDataAdapter(Query, sqlConn);

                da.Fill(ds, "fund");
                sqlConn.Close();
            }
            catch (Exception ex)
            {
                string Msg = ex.Message.ToString();
            }
            return ds;
        }

        public DataSet ApprovalHistory(string jobno)
        {
            DataSet ds = new DataSet();
            try
            {
                string Query = "select FundRequestNo as [Request No],Convert(Varchar(12),RequestDate,103) As [Request Date],JobNo,ImporterName As [Customer],RequestAmt as [Amount],"+
                    "RequestBy as [Request By],Approved,Completed,PaymentStatus as [Status],PaymentAmt as PayAmt from T_FundRequest where JobNo = '" + jobno + "' and Active=0 " +
                    "Group By FundRequestNo,JobNo,ImporterName,RequestAmt,RequestDate,RequestBy,Approved,Completed,PaymentStatus,PaymentAmt Order By RequestDate";

                SqlConnection sqlConn = new SqlConnection(strcon);
                sqlConn.Open();
                SqlDataAdapter da = new SqlDataAdapter(Query, sqlConn);

                da.Fill(ds, "fund");
                sqlConn.Close();
            }
            catch (Exception ex)
            {
                string Msg = ex.Message.ToString();
            }
            return ds;
        }

        public DataSet FundRequestHistory()
        {
            DataSet ds = new DataSet();
            try
            {
                string Query = "select FundRequestNo as [Request No],Convert(Varchar(12),RequestDate,103) As [Request Date],JobNo,ImporterName As [Customer],RequestAmt as [Amount]," +
                    "RequestBy as [Request By],Approved,Completed,PaymentStatus as [Status],PaymentAmt as PayAmt from T_FundRequest where Active=0 " +
                    "Group By FundRequestNo,JobNo,ImporterName,RequestAmt,RequestDate,RequestBy,Approved,Completed,PaymentStatus,PaymentAmt Order By RequestDate";

                SqlConnection sqlConn = new SqlConnection(strcon);
                sqlConn.Open();
                SqlDataAdapter da = new SqlDataAdapter(Query, sqlConn);

                da.Fill(ds, "fund");
                sqlConn.Close();
            }
            catch (Exception ex)
            {
                string Msg = ex.Message.ToString();
            }
            return ds;
        }

        public DataSet FundRequestPending(string user)
        {
            DataSet ds = new DataSet();
            try
            {
                string Query = "select FundRequestNo as [Request No],Convert(Varchar(12),RequestDate,103) As [Request Date],JobNo,ImporterName As [Customer],RequestAmt as [Amount]," +
                    "RequestBy as [Request By],Approved as [Approved Status],Completed as [Accounts Status],PaymentStatus as [Payment Status],PaymentAmt as PayAmt from T_FundRequest where Completed = 0  and Active=0 " +
                    "Group By FundRequestNo,JobNo,ImporterName,RequestAmt,RequestDate,RequestBy,Approved,Completed,PaymentStatus,PaymentAmt Order By RequestDate";


                SqlConnection sqlConn = new SqlConnection(strcon);
                sqlConn.Open();
                SqlDataAdapter da = new SqlDataAdapter(Query, sqlConn);

                da.Fill(ds, "fund");
                sqlConn.Close();
            }
            catch (Exception ex)
            {
                string Msg = ex.Message.ToString();
            }
            return ds;
        }

        public DataSet Selectedpurpose(string purpose)
        {
            DataSet ds = new DataSet();
            try
            {
                string Query = "Select * from T_FundPurpose where PurposeType='" + purpose + "'";

                SqlConnection sqlConn = new SqlConnection(strcon);
                sqlConn.Open();
                SqlDataAdapter da = new SqlDataAdapter(Query, sqlConn);

                da.Fill(ds, "purposeFor");
                sqlConn.Close();
            }
            catch (Exception ex)
            {
                string Msg = ex.Message.ToString();
            }
            return ds;
        }

        public static string ConvertAmp(string st)
        {
            string res = "";
            int i;
            for (i = 0; (i <= (st.Length - 1)); i++)
            {
                if (st[i].ToString() == "&")
                {
                    res = (res + "&amp;");
                }
                else if ((st[i].ToString() == "'"))
                {
                    res = (res + "''");
                }
                else
                {
                    res = (res + st[i].ToString());
                }
            }
            return res;
        }

        public DataSet SelectCFSName()
        {
            DataSet ds = new DataSet();
            try
            {
                string Query = "Select CfsName from M_CFSMaster ";

                SqlConnection sqlConn = new SqlConnection(strcon);
                sqlConn.Open();
                SqlDataAdapter da = new SqlDataAdapter(Query, sqlConn);

                da.Fill(ds, "cfsmaster");
                sqlConn.Close();
            }
            catch (Exception ex)
            {
                string Msg = ex.Message.ToString();
            }
            return ds;
        }

        public DataSet SelectShippingName()
        {
            DataSet ds = new DataSet();
            try
            {
                string Query = "Select ShipperName from M_ShippingLineMaster ";

                SqlConnection sqlConn = new SqlConnection(strcon);
                sqlConn.Open();
                SqlDataAdapter da = new SqlDataAdapter(Query, sqlConn);

                da.Fill(ds, "ShippingMaster");
                sqlConn.Close();
            }
            catch (Exception ex)
            {
                string Msg = ex.Message.ToString();
            }
            return ds;
        }

        public DataSet BankName()
        {
            DataSet ds = new DataSet();
            try
            {
                string Query = "select AccountCode,AccountName from M_AccountMaster where Acc_Group='Bank Accounts' ";

                SqlConnection sqlConn = new SqlConnection(strcon);
                sqlConn.Open();
                SqlDataAdapter da = new SqlDataAdapter(Query, sqlConn);

                da.Fill(ds, "bank");
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
