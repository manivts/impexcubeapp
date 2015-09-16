using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VTS.ImpexCube.Utlities;
using System.Data;

namespace ImpexCube
{
    public partial class frmFundRequest : System.Web.UI.Page
    {
        VTS.ImpexCube.Business.FundRequest objFundRequest = new VTS.ImpexCube.Business.FundRequest();

        string keyname = "FR";
        string cfsname = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            
            if (IsPostBack == false)
            {
                cfs.Visible = false;
                Shipping.Visible = false;
                txtRequiredDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                //string formID = "Fund Request";
                
                //Authenticate.Forms(formID);
                //string Validate = (string)Session["DISABLE"];
                //if (Validate == "True")
                //{

                    if (Request.QueryString["mode"] == "New")
                    {
                        string fyear = (string)Session["FYear"];
                        txtReqBy.Text = (string)Session["USER-NAME"];
                        lblFundReqDate.Text = System.DateTime.Now.ToString("dd/MM/yyyy");
                        lblFundRequest.Text = keyname + "/" + fyear+ "/" + Convert.ToString(Utility.GetNextAutoNo(keyname, 0, Utility.GetConnectionString()));
                        Session["Keycode"] = Convert.ToString(Utility.GetNextAutoNo(keyname, 0, Utility.GetConnectionString()));
                        BindJobnO();
                        ControlEnable();
                       
                    }
                    else if (Request.QueryString["mode"] == "Edit")
                    {
                        btnPrint.Visible = true;
                        BindJobnO();
                        EditFundRequest();
                        GridFundRequest();
                        GridApprovedList();
                        btnUpdate.Visible = true;
                        btnSave.Visible = false;
                    }
                //}
                //else
                //{
                //    ScriptManager.RegisterStartupScript(this, this.GetType(), "redirect", "alert('You have not Authorized for this Page'); window.location.href='HomePage.aspx';", true);
                  
                //}

            }
        }

        private void ControlEnable()
        {
            txtRemarks.Enabled = false;
            txtReqAmount.Enabled = false;
            txtReqBy.Enabled = false;
            txtRequiredDate.Enabled = false;
            ddlPurpose.Enabled = false;
            ddlModeOfPayment.Enabled = false;
        }

        private void EditFundRequest()
        {
            DataSet ds = objFundRequest.SelectedFundRequest((string)Session["FundDetails"]);
            if (ds.Tables["FundDetails"].Rows.Count != 0)
            {
                DataRowView dr = ds.Tables["FundDetails"].DefaultView[0];                
                lblFundRequest.Text = dr["Request No"].ToString();
                lblFundReqDate.Text = dr["Fund Date"].ToString();
                txtRequiredDate.Text = dr["Request Date"].ToString();
                txtReqBy.Text = dr["RequestBy"].ToString();
                txtReqAmount.Text = dr["Amount"].ToString();
                lblImporter.Text = dr["Customer"].ToString();
                txtRemarks.Text = dr["Remarks"].ToString();
                

                ddlJobNo.SelectedIndex = ddlJobNo.Items.IndexOf(ddlJobNo.Items.FindByText(dr["JobNo"].ToString()));
               // ddlJobNo.Enabled = false;
                ddlModeOfPayment.SelectedIndex = ddlModeOfPayment.Items.IndexOf(ddlModeOfPayment.Items.FindByText(dr["Payment"].ToString()));
                
                string PaymentType = ddlModeOfPayment.SelectedValue;
                DataSet fds = objFundRequest.Selectedpurpose(PaymentType);

                ddlPurpose.DataSource = fds;
                ddlPurpose.DataTextField = "Purpose";
                ddlPurpose.DataValueField = "Purpose";
                ddlPurpose.DataBind();
                ddlPurpose.Items.Insert(0, new ListItem("~Select~", "0"));

                ddlPurpose.SelectedIndex = ddlPurpose.Items.IndexOf(ddlPurpose.Items.FindByText(dr["Purpose"].ToString()));
                ddlCFSName.DataSource = ds;
                ddlCFSName.DataTextField = "CfsName";
                ddlCFSName.DataValueField = "CfsName";
                ddlCFSName.DataBind();
                ddlCFSName.Items.Insert(0, new ListItem("~Select~", "0"));
                ddlCFSName.SelectedIndex = ddlCFSName.Items.IndexOf(ddlCFSName.Items.FindByText(dr["CfsName"].ToString()));
                string purpose = ddlPurpose.SelectedItem.Text;
                if (purpose == "C-CFS" || purpose == "CFS Charges")
                {
                    BindCFSName();
                }
                else if (purpose == "S-S.LINE" || purpose == "S-S.Line")
                {
                    BindShippingName();
                }
                else
                {

                    cfs.Visible = false;
                    Shipping.Visible = false;
                }
            }
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
                divOverAll.Visible = true;
            }
            else
            {
                divOverAll.Visible = false;
                gvApprovedList.DataSource = null;
                gvApprovedList.DataBind();
            }

        }

        private void BindJobnO()
        {
            if (rbImpExp.SelectedValue == "Imp")
            {
                DataSet ds = objFundRequest.jobno((string)Session["FYear"]);
                if (ds.Tables["jobno"].Rows.Count != 0)
                {
                    ddlJobNo.DataSource = ds;
                    ddlJobNo.DataTextField = "JobNo";
                    ddlJobNo.DataValueField = "JobNo";
                    ddlJobNo.DataBind();
                    ddlJobNo.Items.Insert(0, new ListItem("-Select-", "0"));
                }
                else
                {
                    ddlJobNo.DataSource = null;
                    ddlJobNo.DataBind();
                }
            }
            else if(rbImpExp.SelectedValue=="Exp")
            {
                DataSet ds = objFundRequest.Expjobno((string)Session["FYear"]);
                if (ds.Tables["jobno"].Rows.Count != 0)
                {
                    ddlJobNo.DataSource = ds;
                    ddlJobNo.DataTextField = "JobNo";
                    ddlJobNo.DataValueField = "JobNo";
                    ddlJobNo.DataBind();
                    ddlJobNo.Items.Insert(0, new ListItem("-Select-", "0"));
                }
                else
                {
                    ddlJobNo.DataSource = null;
                    ddlJobNo.DataBind();
                }
            }


        }

        protected void btnSave_Click(object sender, EventArgs e)
        {

            if (Request.QueryString["mode"] == "New")
            {
                string fyear = (string)Session["FYear"];
                txtReqBy.Text = (string)Session["USER-NAME"];
                lblFundReqDate.Text = System.DateTime.Now.ToString("dd/MM/yyyy");
                lblFundRequest.Text = keyname + "/" + fyear + "/" + Convert.ToString(Utility.GetNextAutoNo(keyname, 0, Utility.GetConnectionString()));
                Session["Keycode"] = Convert.ToString(Utility.GetNextAutoNo(keyname, 0, Utility.GetConnectionString()));
               // BindJobnO();
               // ControlEnable();
            }
            btnSave.Enabled = false;
            cfsname = ddlCFSName.SelectedValue;
            string shippingname = ddlShippingName.SelectedValue;
            if (txtRequiredDate.Text != "")
            {
                string apd = "0";
                string Completed = "0";
                int Update = new int();
                int result = objFundRequest.InsertFundRequest(lblFundRequest.Text, lblFundReqDate.Text, ddlJobNo.SelectedItem.Text, lblImporter.Text, txtReqAmount.Text,
                    txtRequiredDate.Text, ddlModeOfPayment.SelectedValue, txtReqBy.Text, ddlPurpose.SelectedItem.Text, txtRemarks.Text, apd, Completed, cfsname, shippingname);
                if (result == 1)
                {
                    cfsname = string.Empty;
                    string keycode = (string)Session["Keycode"];
                    Update = Utility.UpdateAutoNo(keyname, Convert.ToInt32(keycode), Utility.GetConnectionString());
                    //GridFundRequest();
                    btnSave.Enabled = false;
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "redirect", "alert('Saved Successfully'); window.location.href='frmFundRequest.aspx?mode=New';", true);
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Please Select Request Date');", true);
            }
            
        }

        private void GridFundRequest()
        {
            DataSet ds = objFundRequest.GridLoad((string)Session["jobno"]);
            if (ds.Tables["fund"].Rows.Count != 0)
            {
                gvFundRequest.DataSource = ds;
                gvFundRequest.DataBind();

                int i = 0;
                DataSet gds = objFundRequest.GridLoad((string)Session["jobno"]);
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
                divHistory.Visible = true;
            }
            else
            {
                gvFundRequest.DataSource = ds;
                gvFundRequest.DataBind();
                divHistory.Visible = false;
            }
        }

        protected void ddlJobNo_SelectedIndexChanged(object sender, EventArgs e)
        {            
            txtRemarks.Enabled = true;
            txtReqAmount.Enabled = true;
            txtReqBy.Enabled = true;
            txtRequiredDate.Enabled = true;
            ddlModeOfPayment.Enabled = true;
            ddlPurpose.Enabled = true;

            string jobno = ddlJobNo.SelectedItem.Text;
           
                if (jobno != "-Select-")
                {
                    if (rbImpExp.SelectedValue == "Imp")
                    {
                        Session["jobno"] = jobno;
                        DataSet ds = objFundRequest.ImporterName(jobno);
                        if (ds.Tables["importer"].Rows.Count != 0)
                        {
                            DataRowView dr = ds.Tables["importer"].DefaultView[0];
                            lblImporter.Text = dr["Importer"].ToString();
                            txtRequiredDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                            txtRemarks.Text = string.Empty;
                            txtReqAmount.Text = string.Empty;
                            ddlModeOfPayment.SelectedIndex = 0;
                            ddlPurpose.SelectedIndex = 0;
                        }
                    }
                    else
                    {
                        Session["jobno"] = jobno;
                        DataSet ds = objFundRequest.ExportName(jobno);
                        if (ds.Tables["Export"].Rows.Count != 0)
                        {
                            DataRowView dr = ds.Tables["Export"].DefaultView[0];
                            lblImporter.Text = dr["party_name"].ToString();
                            txtRequiredDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                            txtRemarks.Text = string.Empty;
                            txtReqAmount.Text = string.Empty;
                            ddlModeOfPayment.SelectedIndex = 0;
                            ddlPurpose.SelectedIndex = 0;
                        }
                    }
                    GridFundRequest();
                    GridApprovedList();
                    btnSave.Enabled = true;
                }
                else
                {
                    ControlEnable();
                    divHistory.Visible = false;
                    divOverAll.Visible = false;
                    lblImporter.Text = string.Empty;
                    btnSave.Enabled = false;
                }
           
           
        }

        protected void gvFundRequest_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (gvFundRequest.SelectedRow.Cells[1].Text != null)
            {
                Session["FundDetails"] = gvFundRequest.SelectedRow.Cells[1].Text.ToString();
                Session["jobno"] = gvFundRequest.SelectedRow.Cells[2].Text.ToString();
                Response.Redirect("frmFundRequest.aspx?mode=Edit"); 
            }
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            int result = objFundRequest.UpdateFundRequest(lblFundRequest.Text, lblFundReqDate.Text, ddlJobNo.SelectedItem.Text, lblImporter.Text, txtReqAmount.Text,
                txtRequiredDate.Text, ddlModeOfPayment.SelectedValue, txtReqBy.Text, ddlPurpose.SelectedItem.Text, txtRemarks.Text, ddlCFSName.SelectedValue,ddlShippingName.SelectedValue);
            if (result == 1)
            {   
                //GridFundRequest();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "redirect", "alert('Updated Successfully'); window.location.href='frmFundRequest.aspx?mode=New';", true);
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("frmFundRequest.aspx?mode=New");
        }

        protected void ddlModeOfPayment_SelectedIndexChanged(object sender, EventArgs e)
        {
            string PaymentType = ddlModeOfPayment.SelectedValue;
            DataSet ds = objFundRequest.Selectedpurpose(PaymentType);

            ddlPurpose.DataSource = ds;
            ddlPurpose.DataTextField = "Purpose";
            ddlPurpose.DataValueField = "Purpose";
            ddlPurpose.DataBind();
            ddlPurpose.Items.Insert(0, new ListItem("~Select~", "0"));


            string purpose = ddlPurpose.SelectedItem.Text;
            if (purpose == "C-CFS" || purpose=="CFS Charges")
            {
                BindCFSName();
            }
            else if (purpose == "S-S.LINE" || purpose == "S-S.Line")
            {
                BindShippingName();
            }
            else
            {

                cfs.Visible = false;
                Shipping.Visible = false;
            }
        }

        protected void ddlPurpose_SelectedIndexChanged(object sender, EventArgs e)
        {           
            string purpose = ddlPurpose.SelectedItem.Text;
            if (purpose == "C-CFS" || purpose == "CFS Charges")
            {
                BindCFSName();
                
                Shipping.Visible = false;
            }
            else if (purpose == "S-S.LINE" || purpose == "S-S.Line")
            {
                BindShippingName();
                cfs.Visible = false;
                
            }
            else
            {

                cfs.Visible = false;
                Shipping.Visible = false;

            }
        }

        private void BindCFSName()
        {
            DataSet ds = objFundRequest.SelectCFSName();
            if (ds.Tables["cfsmaster"].Rows.Count != 0)
            {
                ddlCFSName.DataSource = ds;
                ddlCFSName.DataTextField = "CfsName";
                ddlCFSName.DataValueField = "CfsName";
                ddlCFSName.DataBind();
                ddlCFSName.Items.Insert(0, new ListItem("-Select-", "0"));
                cfs.Visible = true;
            }
            else
            {
                ddlCFSName.DataSource = null;
                ddlCFSName.DataBind();
            }
        }

        private void BindShippingName()
        {
            DataSet ds = objFundRequest.SelectShippingName();
            if (ds.Tables["ShippingMaster"].Rows.Count != 0)
            {
                ddlShippingName.DataSource = ds;
                ddlShippingName.DataTextField = "ShipperName";
                ddlShippingName.DataValueField = "ShipperName";
                ddlShippingName.DataBind();
                ddlShippingName.Items.Insert(0, new ListItem("-Select-", "0"));
                Shipping.Visible = true;
            }
            else
            {
                ddlCFSName.DataSource = null;
                ddlCFSName.DataBind();
            }
        }

        protected void rbImpExp_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindJobnO();
        }

        protected void btnPrint_Click(object sender, EventArgs e)
        {
            Session["FundRqNo"] = string.Empty;
            Session["FundRqNo"] = lblFundRequest.Text;
            Response.Redirect("frmFundPrint.aspx");
        }

      

    }
}