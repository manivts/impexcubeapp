using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.IO;
using System.Collections;
using System.Globalization;
using System.Data.Common;
using System.Data;
using System.Data.SqlClient;
using VTS.ImpexCube.Utlities;

namespace ImpexCube
{
    public partial class frmFundRequestAccount : System.Web.UI.Page
    {
        VTS.ImpexCube.Business.FundRequest objFundRequest = new VTS.ImpexCube.Business.FundRequest();
        VTS.ImpexCube.Data.CommonDL obj1 = new VTS.ImpexCube.Data.CommonDL();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack == false)
            {
                Session["Payment"] = string.Empty;
                Session["VGUID"] = Guid.NewGuid().ToString();
                rwPaymentMode.Visible = false;
                rwBankName.Visible = false;                
                Session["jobno"] = string.Empty;
                Session["PayAmt"] = string.Empty;
                GridPendingList();
                GridApprovedList();

                DataSet hds = objFundRequest.BankName();
                if (hds.Tables["bank"].Rows.Count != 0)
                {
                    txtBankName.DataSource = hds;
                    txtBankName.DataTextField = "AccountName";
                    txtBankName.DataValueField = "AccountCode";
                    txtBankName.DataBind();
                    txtBankName.Items.Insert(0, new ListItem("-Select-", "0"));
                }
                btnSave.Enabled = false;
                Session["Balance"] = "0.00";
                Session["PayBalance"] = "0.00";
                //}
                //else
                //{
                //    ScriptManager.RegisterStartupScript(this, this.GetType(), "redirect", "alert('You have not Authorized for this Page'); window.location.href='HomePage.aspx';", true);

                //}

            }
          
        }

        private void GridApprovedList()
        {
            if ((string)Session["jobno"] != "")
            {
                GridSessionJobno();
            }
            else
            {
                GridOverallHistory();
            }
        }

        private void GridOverallHistory()
        {
            DataSet hds = objFundRequest.FundRequestHistory();
            if (hds.Tables["fund"].Rows.Count != 0)
            {
                gvApprovedList.DataSource = hds;
                gvApprovedList.DataBind();

                int i = 0;
                DataSet ahds = objFundRequest.FundRequestHistory();
                DataTable hdt = ahds.Tables["fund"];

                foreach (DataRow hrow in hdt.Rows)
                {
                    DataRowView dr = hds.Tables["fund"].DefaultView[i];
                    string requestno = dr["Request No"].ToString();
                    string jobno = dr["JobNo"].ToString();
                    string importer = dr["Customer"].ToString();
                    string amount = dr["Amount"].ToString();
                    string rqdate = dr["Request Date"].ToString();
                    string apd = dr["Approved"].ToString();
                    string completed = dr["Completed"].ToString();
                    string status = dr["Status"].ToString();
                    string payamt = dr["PayAmt"].ToString();
                    string reqby = dr["Request By"].ToString();

                    Label approved = (Label)gvApprovedList.Rows[i].FindControl("lblManagerApproval");
                    Label Complete = (Label)gvApprovedList.Rows[i].FindControl("lblAccountsApproval");
                    Label PayStatus = (Label)gvApprovedList.Rows[i].FindControl("lblPaymentStatus");
                    Label Payment = (Label)gvApprovedList.Rows[i].FindControl("lblPayAmt");

                    gvApprovedList.Rows[i].Cells[0].Text = requestno;
                    gvApprovedList.Rows[i].Cells[1].Text = jobno;
                    gvApprovedList.Rows[i].Cells[2].Text = importer;
                    gvApprovedList.Rows[i].Cells[3].Text = amount;
                    gvApprovedList.Rows[i].Cells[4].Text = rqdate;
                    gvApprovedList.Rows[i].Cells[5].Text = reqby;
                    if (apd == "True")
                    {
                        approved.Text = "Approved";
                    }
                    else if (apd == "False")
                    {
                        approved.Text = "Pending";
                    }
                    if (completed == "True")
                    {
                        Complete.Text = "Approved";
                        PayStatus.Text = status;
                        Payment.Text = payamt;
                    }
                    else if (completed == "False")
                    {
                        Complete.Text = "Pending";
                        if (status == "" && payamt == "")
                        {
                            PayStatus.Text = "Pending";
                            Payment.Text = "Pending";
                        }
                        else
                        {
                            PayStatus.Text = status;
                            Payment.Text = payamt;
                        }
                    }
                    i++;
                }
                divOverAll.Visible = true;
            }
        }

        private void GridSessionJobno()
        {
            DataSet ds = objFundRequest.ApprovalHistory((string)Session["jobno"]);
            if (ds.Tables["fund"].Rows.Count != 0)
            {
                gvApprovedList.DataSource = ds;
                gvApprovedList.DataBind();

                int i = 0;
                DataSet gds = objFundRequest.ApprovalHistory((string)Session["jobno"]);
                DataTable gdt = gds.Tables["fund"];

                foreach (DataRow row in gdt.Rows)
                {
                    DataRowView dr = gds.Tables["fund"].DefaultView[i];
                    string requestno = dr["Request No"].ToString();
                    string jobno = dr["JobNo"].ToString();
                    string importer = dr["Customer"].ToString();
                    string amount = dr["Amount"].ToString();
                    string rqdate = dr["Request Date"].ToString();
                    string apd = dr["Approved"].ToString();
                    string completed = dr["Completed"].ToString();
                    string status = dr["Status"].ToString();
                    string payamt = dr["PayAmt"].ToString();
                    string reqby = dr["Request By"].ToString();

                    Label approved = (Label)gvApprovedList.Rows[i].FindControl("lblManagerApproval");
                    Label Complete = (Label)gvApprovedList.Rows[i].FindControl("lblAccountsApproval");
                    Label PayStatus = (Label)gvApprovedList.Rows[i].FindControl("lblPaymentStatus");
                    Label Payment = (Label)gvApprovedList.Rows[i].FindControl("lblPayAmt");

                    gvApprovedList.Rows[i].Cells[0].Text = requestno;
                    gvApprovedList.Rows[i].Cells[1].Text = jobno;
                    gvApprovedList.Rows[i].Cells[2].Text = importer;
                    gvApprovedList.Rows[i].Cells[3].Text = amount;
                    gvApprovedList.Rows[i].Cells[4].Text = rqdate;
                    gvApprovedList.Rows[i].Cells[5].Text = reqby;
                    if (apd == "True")
                    {
                        approved.Text = "Approved";
                    }
                    else if (apd == "False")
                    {
                        approved.Text = "Pending";
                    }
                    if (completed == "True")
                    {
                        Complete.Text = "Approved";
                        PayStatus.Text = status;
                        Payment.Text = payamt;
                    }
                    else if (completed == "False")
                    {
                        Complete.Text = "Pending";
                        if (status != "" && payamt != "")
                        {
                            PayStatus.Text = status;
                            Payment.Text = payamt;
                        }
                        else
                        {
                            PayStatus.Text = "Pending";
                            Payment.Text = "Pending";
                        }
                    }
                    i++;
                }
                divOverAll.Visible = true;
            }
            else
            {
                GridOverallHistory();
            }
        }

        private void GridPendingList()
        {
            DataSet ds = objFundRequest.ApprovedGrid();
            if (ds.Tables["fund"].Rows.Count != 0)
            {
                gvPendingrequest.DataSource = ds;
                gvPendingrequest.DataBind();

                int i = 0;
                DataSet gds = objFundRequest.ApprovedGrid();
                DataTable gdt = gds.Tables["fund"];

                foreach (DataRow row in gdt.Rows)
                {
                    DataRowView dr = gds.Tables["fund"].DefaultView[i];
                    string requestno = dr["Request No"].ToString();
                    string jobno = dr["JobNo"].ToString();
                    string importer = dr["Customer"].ToString();
                    string amount = dr["Amount"].ToString();
                    string rqdate = dr["Request Date"].ToString();

                    gvPendingrequest.Rows[i].Cells[1].Text = requestno;
                    gvPendingrequest.Rows[i].Cells[2].Text = jobno;
                    gvPendingrequest.Rows[i].Cells[3].Text = importer;
                    gvPendingrequest.Rows[i].Cells[4].Text = amount;
                    gvPendingrequest.Rows[i].Cells[5].Text = rqdate;
                    i++;
                }                
            }            
            else
            {
                gvPendingrequest.DataSource = null;
                gvPendingrequest.DataBind();
                lblResult.Text = "No Pending List";
                lblResult.Visible = true;
            }
        }        

        protected void gvPendingrequest_SelectedIndexChanged(object sender, EventArgs e)
        {
            Session["mode"] = string.Empty;
            if (gvPendingrequest.SelectedRow.Cells[1].Text != null)
            {
                Session["mode"] = "Save";
                Session["FundDetails"] = gvPendingrequest.SelectedRow.Cells[1].Text.ToString();
                Session["jobno"] = gvPendingrequest.SelectedRow.Cells[2].Text.ToString();
                PendingFundRequest();
                GridApprovedList();
                btnSave.Enabled = true;
            }
        } 

        private void PendingFundRequest()
        {
            DataSet ds = objFundRequest.ApprovedFundRequest((string)Session["FundDetails"]);
            if (ds.Tables["FundDetails"].Rows.Count != 0)
            {
                DataRowView dr = ds.Tables["FundDetails"].DefaultView[0];
                lblJobNo.Text = dr["JobNo"].ToString();
                lblModeOfPayment.Text = dr["Payment"].ToString();
                ddlPaymentMode.SelectedIndex = ddlPaymentMode.Items.IndexOf(ddlPaymentMode.Items.FindByText(dr["Payment"].ToString()));
                //ddlPaymentMode.SelectedValue = dr["Payment"].ToString();
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
                    PaymentInfo.Visible = true;
                    Session["Payment"] = "Partial";
                    ddlPaymentMode.SelectedValue = PayMode;
                    ddlPaymentMode.AppendDataBoundItems = false;
                    if (ddlPaymentMode.SelectedItem.Text == "Cheque" || ddlPaymentMode.SelectedItem.Text == "Demand Draft")
                    {
                        rwPaymentMode.Visible = true;
                        rwBankName.Visible = true;                        
                        txtChqDDNo.Text = ChequeDDNo;
                        txtChqDDdate.Text = DDChequeDate;
                        txtBankName.SelectedItem.Text = BankName;
                        txtDrewBank.Text = DrewBank;
                    }
                    else
                    {
                        rwPaymentMode.Visible = false;
                        rwBankName.Visible = false;                        
                        txtChqDDNo.Text = string.Empty;
                        txtChqDDdate.Text = string.Empty;
                        txtBankName.SelectedValue = "-Select-";
                        txtDrewBank.Text = string.Empty;
                    }                    
                    Session["PayBalance"] = balance;
                    Session["Paid"] = payamt;
                    GetFunDetails();
                    gvPartialBalance.Visible = true;

                    //ddlPaymentStatus.SelectedValue = PayStatus;
                    //ddlPaymentStatus.AppendDataBoundItems = false;
                    //if (ddlPaymentStatus.SelectedItem.Text == "Partial Payment")
                    //{
                    //    txtPaymentAmount.Text = string.Empty;
                    //    string apdamt = lblApprovedAmount.Text;
                    //    lblActual.Text = "Actual Amount :" + apdamt;
                    //    lblActual.Visible = true;
                        
                    //    lblPaid.Text = "Paid:" + payamt;
                        
                    //    lblPaid.Visible= true;
                    //    lblBalance.Text = "Balance :" + balance;
                        
                    //    lblBalance.Visible = true;
                    //}
                    //else
                    //{
                    //    txtPaymentAmount.Text = payamt;
                    //    lblPaid.Visible = false;
                    //    lblActual.Visible = false;
                    //    lblBalance.Visible = false;
                    //}
                    txtAccountRemarks.Text = PayRemarks;
                }
                else
                {
                    txtAccountRemarks.Text = lblActual.Text = lblPaid.Text = lblBalance.Text = string.Empty;
                    lblPaid.Visible = false;
                    lblActual.Visible = false;
                    lblBalance.Visible = false;
                    //ddlPaymentMode.SelectedIndex = 0;
                    PaymentInfo.Visible = false;
                    gvPartialBalance.Visible = false;
                }

                string Apd = dr["Approved"].ToString();
                string completed = dr["Completed"].ToString();

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

        private void GetFunDetails()
        {
            DataSet ds = objFundRequest.PartialAmtBalance((string)Session["FundDetails"]);
            if (ds.Tables["FundDetails"].Rows.Count != 0)
            {
                gvPartialBalance.DataSource = ds;
                gvPartialBalance.DataBind();                
            }
            else
            {
                gvPartialBalance.DataSource = null;
                gvPartialBalance.DataBind();                
            }
        }

        private void ApprovedFundRequest()
        {
            DataSet ds = objFundRequest.SelectedPendingRequest((string)Session["FundDetails"]);
            if (ds.Tables["FundDetails"].Rows.Count != 0)
            {
                DataRowView dr = ds.Tables["FundDetails"].DefaultView[0];
                lblJobNo.Text = dr["JobNo"].ToString();
                lblModeOfPayment.Text = dr["Payment"].ToString();
                lblFundRequestNo.Text = dr["Request No"].ToString();
                lblFundReqDate.Text = dr["Fund Date"].ToString();
                lblRequiredDate.Text = dr["Request Date"].ToString();
                lblReqBy.Text = dr["RequestBy"].ToString();
                lblReqAmount.Text = dr["Amount"].ToString();
                lblCustomerName.Text = dr["Customer"].ToString();
                lblRemark.Text = dr["Remarks"].ToString();
                lblApprovedAmount.Text = dr["Approved Amount"].ToString();
                lblApprovalRemark.Text = dr["Approval Remarks"].ToString();
                lblApprovalDate.Text = dr["Approved Date"].ToString();
                txtPaymentAmount.Text = dr["PayAmount"].ToString();
                txtAccountRemarks.Text = dr["PayRemarks"].ToString();
                string paymode = dr["PayMode"].ToString();
                string status = dr["status"].ToString();
                string Apd = dr["Approved"].ToString();
                string completed = dr["Completed"].ToString();

                ddlPaymentMode.SelectedItem.Text = paymode;
                ddlPaymentStatus.SelectedItem.Text = status;

                if (Apd == "True")
                {
                    chkApproved.Checked = true;
                    chkApproved.Enabled = false;
                }
                else if (Apd == "False")
                {
                    chkApproved.Checked = false;                    
                }

                if (completed == "True")
                {
                    chkComplete.Checked = true;
                }
                else if (completed == "False")
                {
                    chkComplete.Checked = false;
                }
            }




        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (chkAccounts.Checked == true)
            {
                accounts();
            }
            SaveApprovedRequest();
        }

        public void accounts()
        {
            string FYear = (string)Session["FYear"];
            string BranchCode = (string)Session["BranchShortName"];
            string custcode = "";
            string AccountCrCode="";
            string AccountCrName = "";
            string PaymentType = "";
            if (txtBankName.SelectedValue == "")
            {
                AccountCrCode = ddlPaymentMode.SelectedValue;
                AccountCrName = ddlPaymentMode.SelectedValue;
                PaymentType = "Cash Payment";
                custcode = "CP";
            }
            else
            {
                AccountCrCode = txtBankName.SelectedValue;
                AccountCrName = txtBankName.SelectedItem.Text;
                PaymentType = "Bank Payment";
                custcode = "BP";
            }
            int slno = Utility.GetAddAutoNo(custcode, FYear, BranchCode);
            string qry1 = "Update M_AutoGenerate set KeyCode=" + slno + " where KeyName='" + custcode + "' And FYear='" + FYear + "' And BranchCode='" + BranchCode + "'";
            obj1.ExecuteNonQuery(qry1);
            String VoucherNo = Utility.GetNextAccountAutoNo(custcode, FYear, BranchCode);
            //Payment Master
            string Query = "Insert into T_PaymentMaster(VoucherNo,VoucherDate,BranchCode,CompanyCode,AccountCrCode,AccountCrName,AmountCr,Chq_No,Chq_Date,Narration,VCH_Type,VGUID,Approved,Completed,CreatedBy,CreatedDate,ModifiedBy,ModifiedDate,FinanceYear)" +
                           "values('" + VoucherNo + "','" + frmdatesplit(txtChqDDdate.Text) + "','" + BranchCode + "','" + (string)Session["CompanyCode"] + "','" + AccountCrCode + "','" + AccountCrName + "'," +
                           " " + txtPaymentAmount.Text + ",'" + txtChqDDNo.Text + "','" + frmdatesplit(txtChqDDdate.Text) + "','" + txtAccountRemarks.Text + "','" + PaymentType + "'','" + (string)Session["VGUID"] + "'," +
                           " 0,0,'" + (string)Session["UserName"] + "','" + DateTime.Now + "','" + (string)Session["UserName"] + "','" + DateTime.Now + "','" + FYear + "')";
            obj1.ExecuteNonQuery(Query);

            //Payment Details
            string Query1 = "Insert into T_PaymentDetails(VoucherNo,VoucherDate,AccountDrCode,AccountDrName,AmountDr,MethodOfAdj,Reference,CostCenter,CreatedBy,CreatedDate,ModifiedBy,ModifiedDate,VGUID)" +
                                        "values('" + VoucherNo + "','" + frmdatesplit(txtChqDDdate.Text) + "','" + lblCustomerName.Text + "','" + lblCustomerName.Text + "'," + txtPaymentAmount.Text + ", " +
                                        " 'OnAccount','" + lblFundRequestNo.Text + "','" + lblJobNo.Text + "','" + (string)Session["UserName"] + "','" + DateTime.Now + "','" + (string)Session["UserName"] + "','" + DateTime.Now + "','" + (string)Session["VGUID"] + "')";
            obj1.ExecuteNonQuery(Query1);
        }

        private string frmdatesplit(string frmdate)
        {
            string frmdate2 = "";
            if (frmdate != "")
            {
                string[] frmdate1 = frmdate.Split('/');
                 frmdate2 = frmdate1[1] + '/' + frmdate1[0] + '/' + frmdate1[2];
            }
             return frmdate2;
        }

        private void SaveApprovedRequest()
        {
            try
            {
                string completed = string.Empty;
                if (chkComplete.Checked == true)
                {
                    completed = "1";
                }
                else if (chkComplete.Checked == false)
                {
                    completed = "0";
                }
                if (ddlPaymentStatus.SelectedItem.Text == "-Select-")
                {
                    if ((string)Session["PayAmt"] == "")
                    {
                        if (txtPaymentAmount.Text != "")
                        {
                            Session["PayAmt"] = txtPaymentAmount.Text;
                        }
                        else
                        {
                            Session["PayAmt"] = "0";
                        }
                    }
                }
                if ((string)Session["mode"] == "Save")
                {
                    int result = objFundRequest.UpdateApprovedList(lblFundRequestNo.Text, ddlPaymentMode.SelectedItem.Text, txtChqDDNo.Text, txtChqDDdate.Text,
                         txtBankName.SelectedItem.Text, txtDrewBank.Text, txtPaymentAmount.Text, ddlPaymentStatus.SelectedItem.Text, (string)Session["Balance"], txtAccountRemarks.Text, completed);
                    if (result == 1)
                    {
                        GridPendingList();
                        if (chkAccounts.Checked == true)
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "redirect", "alert('Saved and update in Accounts Successfully'); window.location.href='frmFundRequestAccount.aspx';", true);
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "redirect", "alert('Saved Successfully'); window.location.href='frmFundRequestAccount.aspx';", true);
                        }
                    }
                }
                else if ((string)Session["mode"] == "Update")
                {
                    int result = objFundRequest.UpdateApprovedList(lblFundRequestNo.Text, ddlPaymentMode.SelectedItem.Text, txtChqDDNo.Text, txtChqDDdate.Text,
                        txtBankName.SelectedItem.Text, txtDrewBank.Text, txtPaymentAmount.Text, ddlPaymentStatus.SelectedItem.Text, (string)Session["Balance"], txtAccountRemarks.Text, completed);
                    if (result == 1)
                    {

                        ScriptManager.RegisterStartupScript(this, this.GetType(), "redirect", "alert('Update Successfully'); window.location.href='frmFundRequestAccount.aspx';", true);
                    }
                }
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }
        }

        protected void ddlPaymentMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlPaymentMode.SelectedItem.Text == "Cheque" || ddlPaymentMode.SelectedItem.Text == "Demand Draft")
            {
                rwPaymentMode.Visible = true;
                rwBankName.Visible = true;                
            }
            if (ddlPaymentMode.SelectedItem.Text == "Cash" || ddlPaymentMode.SelectedItem.Text == "-Select-")
            {
                rwPaymentMode.Visible = false;
                rwBankName.Visible = false;                
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("frmFundRequestAccount.aspx");
        }

        protected void txtPaymentAmount_TextChanged(object sender, EventArgs e)
        {
            double amt = 0;
            double Paramt = 0;
            Session["Balance"] = string.Empty;
            if (txtPaymentAmount.Text == "")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "redirect", "alert('Please enter the payment amount');", true);
            }
            else
            {
                if (ddlPaymentStatus.SelectedItem.Text == "Partial Payment")
                {
                    if ((string)Session["Payment"] != "Partial")
                    {
                        double Apdamt = Convert.ToDouble(lblApprovedAmount.Text);
                        Paramt = Convert.ToDouble(txtPaymentAmount.Text);

                        amt = Apdamt - Paramt;
                        if (amt == 0)
                        {
                            lblBalance.Text = "Balance: " + amt.ToString();
                            chkComplete.Checked = true;
                            chkComplete.Enabled = false;
                            ddlPaymentStatus.SelectedValue = "1";
                            lblBalance.Visible = true;
                            Session["Balance"] = amt.ToString();
                            Session["PayAmt"] = txtPaymentAmount.Text;
                        }
                        else
                        {
                            lblBalance.Text = "Balance: " + amt.ToString();
                            chkComplete.Checked = false;
                            chkComplete.Enabled = false;
                            lblBalance.Visible = true;
                            Session["Balance"] = amt.ToString();
                            Session["PayAmt"] = txtPaymentAmount.Text;
                        }
                    }
                    else if ((string)Session["Payment"] == "Partial")
                    {
                        Paramt = Convert.ToDouble(txtPaymentAmount.Text);
                        double balance = Convert.ToDouble(Session["PayBalance"]);

                        amt = balance - Paramt;
                        lblBalance.Text = "Balance: " + amt.ToString();
                        if (amt == 0)
                        {
                            chkComplete.Checked = true;
                            chkComplete.Enabled = false;
                            ddlPaymentStatus.SelectedValue = "1";
                        }
                        else if (amt != 0)
                        {
                            chkComplete.Checked = false;
                            chkComplete.Enabled = false;
                            ddlPaymentStatus.SelectedValue = "2";
                        }
                        lblBalance.Visible = true;
                        Session["Balance"] = amt.ToString();
                        double LastBalance = Convert.ToDouble(Session["Paid"]);
                        double paid = LastBalance + Paramt;
                        Session["PayAmt"] = paid.ToString();
                    }
                }
                else if (ddlPaymentStatus.SelectedItem.Text == "Full Payment")
                {
                    Paramt = Convert.ToDouble(txtPaymentAmount.Text);
                    if ((string)Session["Payment"] == "Partial")
                    {
                        double balance = Convert.ToDouble(Session["PayBalance"]);
                        double payamt = Convert.ToDouble(Session["Paid"]);
                        amt = balance - Paramt;
                        if (amt == 0)
                        {
                            lblBalance.Text = "Balance: " + amt.ToString();
                            Session["Balance"] = amt.ToString();
                            Session["PayAmt"] = Session["Actual"];
                            chkComplete.Enabled = false;
                            chkComplete.Checked = true;
                            ddlPaymentStatus.SelectedValue = "1";
                            lblBalance.Visible = true;
                        }
                        else
                        {
                            double lastpay = Paramt + payamt;
                            lblBalance.Text = "Balance: " + amt.ToString();
                            Session["Balance"] = amt.ToString();
                            Session["PayAmt"] = lastpay.ToString();
                            chkComplete.Enabled = false;
                            chkComplete.Checked = false;
                            ddlPaymentStatus.SelectedValue = "2";
                            lblBalance.Visible = true;
                        }
                    }
                    else
                    {
                        double actual = Convert.ToDouble(Session["Actual"]);
                        amt = actual - Paramt;
                        if (amt == 0)
                        {
                            lblBalance.Text = "Balance: " + amt.ToString();
                            Session["Balance"] = amt.ToString();
                            Session["PayAmt"] = txtPaymentAmount.Text;
                            chkComplete.Enabled = false;
                            chkComplete.Checked = true;
                            lblBalance.Visible = true;
                            ddlPaymentStatus.SelectedValue = "1";
                        }
                        else
                        {
                            lblBalance.Text = "Balance: " + amt.ToString();
                            Session["Balance"] = amt.ToString();
                            chkComplete.Enabled = false;
                            chkComplete.Checked = false;
                            lblBalance.Visible = true;
                            ddlPaymentStatus.SelectedValue = "2";
                        }
                    }
                }
            }
        }

    }
}