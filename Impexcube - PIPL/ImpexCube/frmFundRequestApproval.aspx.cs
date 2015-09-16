using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace ImpexCube
{
    public partial class frmFundRequestApproval : System.Web.UI.Page
    {
        VTS.ImpexCube.Business.FundRequest objFundRequest = new VTS.ImpexCube.Business.FundRequest();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack == false)
            {
                //string formID = "Fund Request Oper Manager";
                //Authenticate.Forms(formID);
                //string Validate = (string)Session["DISABLE"];
                //if (Validate == "True")
                //{
                GridFundRequest();
                ControlEnable();
                txtApprovedDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                btnSave.Enabled = false;
                GridPendingList();
                //}
                //else
                //{
                //    ScriptManager.RegisterStartupScript(this, this.GetType(), "redirect", "alert('You have not Authorized for this Page'); window.location.href='HomePage.aspx';", true);

                //}
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
            }
        }

        private void ControlEnable()
        {
            chkApproved.Enabled = false;
            txtApprovedDate.Enabled = false;
            txtApprovedAmount.Enabled = false;
            txtApprovalRemarks.Enabled = false;
        }

        private void GridApprovedList()
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
                        PayStatus.Text = "Pending";
                        Payment.Text = "Pending";
                    }
                    i++;
                }
                divHistory.Visible = true;
            }
            else
            {
                gvApprovedList.DataSource = null;
                gvApprovedList.DataBind();
            }

        }

        private void GridFundRequest()
        {
            DataSet ds = objFundRequest.PendingGridLoad();
            if (ds.Tables["fund"].Rows.Count != 0)
            {
                gvFundRequest.DataSource = ds;
                gvFundRequest.DataBind();

                int i = 0;
                DataSet gds = objFundRequest.PendingGridLoad();
                DataTable gdt = gds.Tables["fund"];

                foreach (DataRow row in gdt.Rows)
                {
                    DataRowView dr = gds.Tables["fund"].DefaultView[i];
                    string requestno = dr["Request No"].ToString();
                    string jobno = dr["JobNo"].ToString();
                    string importer = dr["Customer"].ToString();
                    string amount = dr["Amount"].ToString();
                    string rqdate = dr["Request Date"].ToString();

                    gvFundRequest.Rows[i].Cells[1].Text = requestno;
                    gvFundRequest.Rows[i].Cells[2].Text = jobno;
                    gvFundRequest.Rows[i].Cells[3].Text = importer;
                    gvFundRequest.Rows[i].Cells[4].Text = amount;
                    gvFundRequest.Rows[i].Cells[5].Text = rqdate;                    
                    i++;
                }
            }
            else
            {
                gvFundRequest.DataSource = ds;
                gvFundRequest.DataBind();
            }
        }

        protected void gvFundRequest_SelectedIndexChanged(object sender, EventArgs e)
        {
            chkApproved.Enabled = true;
            chkReject.Enabled = true;
            txtApprovedDate.Enabled = true;
            txtApprovedDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
            txtApprovedAmount.Enabled = true;
            txtApprovalRemarks.Enabled = true;

            Session["mode"] = string.Empty;
            if (gvFundRequest.SelectedRow.Cells[1].Text != null)
            {
                Session["mode"] = "Save";
                Session["FundDetails"] = gvFundRequest.SelectedRow.Cells[1].Text.ToString();
                Session["jobno"] = gvFundRequest.SelectedRow.Cells[2].Text.ToString();
                ApprovalFundRequest();
                GridApprovedList();
                btnSave.Enabled = true;
            }
        }

        private void ApprovalFundRequest()
        {
            chkApproved.Enabled = true;
            txtApprovedAmount.Enabled = true;
            txtApprovedDate.Enabled = true;
            ddlAmountFrom.Enabled = true;
            txtApprovalRemarks.Enabled = true;


            DataSet ds = objFundRequest.SelectedFundRequest((string)Session["FundDetails"]);
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
                string apdamt = dr["ApdAmt"].ToString();
                if (apdamt == "")
                {
                    txtApprovedAmount.Text = dr["Amount"].ToString();
                }
                else
                {
                    txtApprovedAmount.Text = apdamt;
                }
                lblCustomerName.Text = dr["Customer"].ToString();
                lblRemark.Text = dr["Remarks"].ToString();
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
                string ApdAmt = dr["ApdAmt"].ToString();
                string ApdDate = dr["App Date"].ToString();
                string ApdRemarks = dr["Apl Remarks"].ToString();
               

                if (ApdAmt == "")
                {
                     txtApprovalRemarks.Text = string.Empty;
                }
                else
                {
                    txtApprovalRemarks.Text = ApdRemarks;
                  //  txtApprovedAmount.Text = ApdAmt;
                    txtApprovedDate.Text = ApdDate;
                }

                string apd = dr["Approved"].ToString();

                if (apd == "True")
                {
                    chkApproved.Checked = true;
                }
                else if (apd == "False")
                {
                    chkApproved.Checked = false;
                }                
            }
        }

        private void ApprovedFundRequest()
        {
            DataSet ds = objFundRequest.ApprovedFundRequest((string)Session["FundDetails"]);
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
                txtApprovedAmount.Text = dr["Approved Amount"].ToString();
                txtApprovalRemarks.Text = dr["Approval Remarks"].ToString();
                txtApprovedDate.Text = dr["Approved Date"].ToString();
                string Apd = dr["Approved"].ToString();
                string completed = dr["Completed"].ToString();

                if (Apd == "True")
                {
                    chkApproved.Checked = true;
                }
                else if(Apd == "False")
                {
                    chkApproved.Checked = false;
                }
                
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            string apd = string.Empty;
            string completed = string.Empty;
            string Active = string.Empty;
            if(chkReject.Checked==true)
            {
            Active="1";
            }
            else
            {
            Active="0";
            }
            if(chkApproved.Checked == true)
            {
                apd = "1";
                completed = "0";
            }
            else
            {
                apd = "0";
                completed = "0";
            }
            if (txtApprovedAmount.Text == "")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "redirect", "alert('Please fill the amount approved field')", true);
            }
            else
            {
                if ((string)Session["mode"] == "Save")
                {
                    int result = objFundRequest.UpdateApproveRequest(lblFundRequestNo.Text, apd, txtApprovedDate.Text, txtApprovedAmount.Text, ddlAmountFrom.SelectedItem.Text, txtApprovalRemarks.Text, completed,Active);
                    if (result == 1)
                    {
                        GridFundRequest();
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "redirect", "alert('Saved Successfully'); window.location.href='frmFundRequestApproval.aspx';", true);
                    }
                }
                else if ((string)Session["mode"] == "Update")
                {
                    int result = objFundRequest.UpdateApproveRequest(lblFundRequestNo.Text, apd, txtApprovedDate.Text, txtApprovedAmount.Text, ddlAmountFrom.SelectedItem.Text, txtApprovalRemarks.Text, completed,Active);
                    if (result == 1)
                    {
                        GridFundRequest();
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "redirect", "alert('Update Successfully'); window.location.href='frmFundRequestApproval.aspx';", true);
                    }
                }
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("frmFundRequestApproval.aspx");
        }

        protected void chkReject_CheckedChanged(object sender, EventArgs e)
        {
            if (chkReject.Checked == true)
            {
                chkApproved.Enabled = false;
                txtApprovedAmount.Text = "0";
                txtApprovedAmount.Enabled = false;
                txtApprovedDate.Enabled = false;
                ddlAmountFrom.Enabled = false;
            }
            else
            {
                chkApproved.Enabled = true;
                txtApprovedAmount.Text = lblReqAmount.Text;
                txtApprovedAmount.Enabled = true;
                txtApprovedDate.Enabled = true;
                ddlAmountFrom.Enabled = true;
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
                ApprovalFundRequest();
                GridApprovedList();
                btnSave.Enabled = true;
            }
        }

    }
}