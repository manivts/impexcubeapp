using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

namespace ImpexCube
{
    public partial class frmFRAccountsPayment : System.Web.UI.Page
    {
        string con = (string)ConfigurationManager.ConnectionStrings["Constr"].ConnectionString;
        VTS.ImpexCube.Business.FundRequest objFundRequest = new VTS.ImpexCube.Business.FundRequest();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack == false)
            {
                Pendinggridload();
                ApprovedGridload();
                gvApprovedList.Visible = true;
            }
        }

        public void ApprovedGridload()
        {
            DataSet ds = new DataSet();
            string Query = "SELECT [FundRequestNo],[JobNo],[ImporterName],[ChequeDDNo],[DDChequeDate],[DrewBank] FROM [T_FundRequest] where AccountsPaymentEntry = 1 ";

            SqlConnection sqlConn = new SqlConnection(con);
            sqlConn.Open();
            SqlDataAdapter da = new SqlDataAdapter(Query, sqlConn);
            da.Fill(ds, "fund");
            gvApprovedList.DataSource = ds;
            gvApprovedList.DataBind();
            sqlConn.Close();
        }

        public void Pendinggridload()
        {
            DataSet ds = new DataSet();
            string Query = "SELECT [FundRequestNo],[JobNo],[ImporterName],[ApprovedAmt] FROM [T_FundRequest] where AccountsPaymentEntry = 0 and Completed = 1 ";

            SqlConnection sqlConn = new SqlConnection(con);
            sqlConn.Open();
            SqlDataAdapter da = new SqlDataAdapter(Query, sqlConn);
            da.Fill(ds, "fund");
            gvPendingrequest.DataSource = ds;
            gvPendingrequest.DataBind();
            sqlConn.Close();
       }

        protected void gvPendingrequest_SelectedIndexChanged(object sender, EventArgs e)
        {

           // int i = 0;
            string fjobno = gvPendingrequest.SelectedRow.Cells[1].Text.ToString();
            DataSet ds = objFundRequest.ApprovedFundRequest(fjobno);
            if (ds.Tables["FundDetails"].Rows.Count != 0)
            {
                DataRowView dr = ds.Tables["FundDetails"].DefaultView[0];
                lblJobNo.Text = dr["JobNo"].ToString();
                lblModeOfPayment.Text = dr["Payment"].ToString();
                //lblPaymentMode.Text = dr["PaymentMode"].ToString();
                lblFundRequestNo.Text = dr["Request No"].ToString();
                lblFundReqDate.Text = dr["Fund Date"].ToString();
                lblRequiredDate.Text = dr["Request Date"].ToString();
                lblReqBy.Text = dr["RequestBy"].ToString();
                lblReqAmount.Text = dr["Amount"].ToString();
                lblCustomerName.Text = dr["Customer"].ToString();
                lblRemark.Text = dr["Remarks"].ToString();
                lblApprovedAmount.Text = dr["Approved Amount"].ToString();
                //txtPaymentAmount.Text = dr["Approved Amount"].ToString();
                hdnApprovalAmount.Value = dr["Approved Amount"].ToString();
                lblPurpose.Text = dr["Purpose"].ToString();
                lblPaymentMode.Text = dr["PayMode"].ToString();
                lblPaymentStatus.Text = dr["PayStatus"].ToString();
                lblPaymentAmount.Text = dr["PayAmt"].ToString();
                lblBankName.Text = dr["BankName"].ToString();
                if (lblPurpose.Text == "C-CFS" || lblPurpose.Text == "CFS Charges")
                {
                    lblCFS.Visible = true;
                    lblCFSName.Visible = true;
                    lblShipping.Visible = false;
                    lblShippingName.Visible = false;
                    lblCFSName.Text = dr["CfsName"].ToString();
                }
                else if (lblPurpose.Text == "S-S.LINE" || lblPurpose.Text == "S-S.Line")
                {
                    lblShipping.Visible = true;
                    lblShippingName.Visible = true;
                    lblCFS.Visible = false;
                    lblCFSName.Visible = false;
                    lblShippingName.Text = dr["ShippingName"].ToString();
                }
                else
                {
                    lblCFS.Visible = false;
                    lblCFSName.Visible = false;
                    lblShipping.Visible = false;
                    lblShippingName.Visible = false;
                }
                lblCFSName.Text = dr["CfsName"].ToString();
                lblApprovalRemark.Text = dr["Approval Remarks"].ToString();
                lblApprovalDate.Text = dr["Approved Date"].ToString();
                lblApprovedAmountFrom.Text = dr["AmountFrom"].ToString();

                string payamt = dr["PayAmt"].ToString();
                string PayMode = dr["PayMode"].ToString();
                string PayStatus = dr["PayStatus"].ToString();
                string ChequeDDNo = dr["ChequeDDNo"].ToString();
                string DDChequeDate = dr["DDChequeDate"].ToString();
                string BankName = dr["BankName"].ToString();
                string DrewBank = dr["DrewBank"].ToString();
                string PayRemarks = dr["PayRemarks"].ToString();
                string balance = dr["Balance"].ToString();
                hdnPayBalance.Value = dr["Balance"].ToString();
                hdnPayPaid.Value = payamt;
                Session["Paid"] = payamt;
                Session["Actual"] = lblApprovedAmount.Text;
                if (payamt != "")
                {
                    Session["Payment"] = "Partial";
                    Session["PayBalance"] = balance;
                    Session["Paid"] = payamt;
                }
                txtAccountRemarks.Text = PayRemarks;
                string Apd = dr["Approved"].ToString();
                string completed = dr["Completed"].ToString();
                if (completed == "True")
                {
                    chkComplete.Checked = true;
                }
                else if (completed == "False")
                {
                    chkComplete.Checked = false;
                }
                if (Apd == "True")
                {
                    chkApproved.Checked = true;
                }
                else if (Apd == "False")
                {
                    chkApproved.Checked = false;
                }
            }

            if (lblModeOfPayment.Text == "Cash")
            {
                rwPaymentMode.Visible = false;
                rwBankName.Visible = false;
            }
            else if (lblModeOfPayment.Text == "Cheque")
            {
                rwPaymentMode.Visible = true;
                rwBankName.Visible = true;
            }
            else if (lblModeOfPayment.Text == "Demand Draft")
            {
                rwPaymentMode.Visible = true;
                rwBankName.Visible = true;
            }
        }

        private string frmdatesplit(string frmdate)
        {
            string[] frmdate1 = frmdate.Split('/');
            string frmdate2 = frmdate1[1] + '/' + frmdate1[0] + '/' + frmdate1[2];
            return frmdate2;
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            string paymentmode = lblPaymentMode.Text;
            string FundRequestNo = lblFundRequestNo.Text.ToString();
            string drawinfavor = txtDrewBank.Text.ToString();
            string chqddno = txtChqDDNo.Text.ToString();
            string chqdddate = txtChqDDdate.Text.ToString();
            int result;
            int  accountsentry = 1;
            string updateFundRequest;

            if (paymentmode == "Cash")
            {
                updateFundRequest = "Update T_FundRequest Set AccountsPaymentEntry ='" + accountsentry + "' where FundRequestNo='" + FundRequestNo + "'";
            }
            else
            {
                updateFundRequest = "Update T_FundRequest Set AccountsPaymentEntry ='" + accountsentry + "', DrewBank = '" + drawinfavor + "'," +
                   "ChequeDDNo = ' " + chqddno + " ',DDChequeDate = '" + frmdatesplit(chqdddate) + "' where FundRequestNo='" + FundRequestNo + "'";
            }         
            SqlConnection sqlConn = new SqlConnection(con);
            sqlConn.Open();
            SqlCommand cmd = new SqlCommand(updateFundRequest, sqlConn);
            SqlDataAdapter da = new SqlDataAdapter();
            cmd.CommandText = updateFundRequest;
            cmd.Connection = sqlConn;
            da.SelectCommand = cmd;
            result = cmd.ExecuteNonQuery();
            sqlConn.Close();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "redirect", "alert('Successfully Updated'); window.location.href='frmFRAccountsPayment.aspx?mode=New';", true);

        }
    }
}