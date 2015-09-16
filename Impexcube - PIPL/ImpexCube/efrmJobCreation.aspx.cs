using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VTS.ImpexCube.Business;
using System.Data;
using VTS.ImpexCube.Data;

namespace ImpexCube
{
    public partial class efrmJobCreation : System.Web.UI.Page
    {
        EMJobCreationBL objEMJobCreation = new EMJobCreationBL();
        //VTS.ImpexCube.Business.EMJobCreationBL objJobNo = new VTS.ImpexCube.Business.EMJobCreationBL();
        VTS.ImpexCube.Business.ETExporterBL objETExporter = new VTS.ImpexCube.Business.ETExporterBL();
        VTS.ImpexCube.Business.InvoiceDetailsBL invBL = new VTS.ImpexCube.Business.InvoiceDetailsBL();
        CommonDL objCommonDL = new CommonDL();
        int result=0;
        string keycode = string.Empty;
        string ExJobNo = string.Empty;
        string JobNo = "";

        protected void Page_Load(object sender, EventArgs e)
        {

            string Fyear = Session["Fyear"].ToString();
            if (IsPostBack == false)
            {
                if (Request.QueryString["Mode"] == "" || Request.QueryString["Mode"] == null)
                {
                    ExJobNo = (string)Session["ExJobNo"];
                }
                else if (Request.QueryString["Mode"] == "Direct")
                {
                    ExJobNo = string.Empty;
                }
                else if (Request.QueryString["Mode"] == "New Job")
                {

                    DataSet ds = objEMJobCreation.JobNo();
                    DataRowView row = ds.Tables[0].DefaultView[0];
                    int jobno = Convert.ToInt32(row["keycode"]);
                    jobno = jobno + 1;
                    txtJobNo.Text = jobno.ToString();
                    txtJobDate.Text = System.DateTime.Now.ToString("dd/MM/yyyy");
                    // GridLoad(JobNo);

                    //BindCurrency();
                    //BindBondType();
                    // GetImportDetails();
                    div1.Visible = true;
                    div2.Visible = false;
                }

                else if (Request.QueryString["Mode"] == "Search Job")
                {                    
                    div1.Visible = false;
                    div2.Visible = true;
                    GridLoad(JobNo);
                    //GridLoad(Fyear);
                    //GetJobDetails();
                }
                //GridLoad();
                //BindJobCreation();
                //BindDropdown();
                //if (ExJobNo == "" || ExJobNo == null || ExJobNo == "~Select~")
                //{
                //    DataSet ds = objEMJobCreation.JobNo();
                //    DataRowView row = ds.Tables[0].DefaultView[0];
                //    int EJobNo = Convert.ToInt16(row["keycode"]);
                //    EJobNo = EJobNo + 1;
                //    txtJobNo.Text = EJobNo.ToString();
                //}
                //else
                //{
                   
                //}
                btnUpdate.Visible = false;         
            }
            
        }

        private void GetJobDetails()
        {
            string JobNo = (string)Session["ExJobNo"];
            txtJobNo.Text = (string)Session["ExJobNo"];
            DataSet ds = new DataSet();
            ds = objEMJobCreation.GetData(JobNo);
            if (ds.Tables[0].Rows.Count != 0)
            {
                btnUpdate.Visible = true;
                btnSave.Visible = false;
                DataRowView row = ds.Tables[0].DefaultView[0];
                txtJobDate.Text = row["JobDate"].ToString();
                txtJobDate.Text = row["JobReceivedOn"].ToString();
                if (row["TransportMode"] != DBNull.Value)
                {
                    ddlTransportMode.SelectedIndex = ddlTransportMode.Items.IndexOf(ddlTransportMode.Items.FindByText(row["TransportMode"].ToString()));
                }
                //if (row["CustomHouse"] != DBNull.Value)
                //{
                //    ddlCustomHouse.SelectedIndex = ddlCustomHouse.Items.IndexOf(ddlCustomHouse.Items.FindByText(row["CustomHouse"].ToString()));
                //}
                if (row["Filling"] != DBNull.Value)
                {
                    ddlFilling.SelectedIndex = ddlFilling.Items.IndexOf(ddlFilling.Items.FindByText(row["Filling"].ToString()));
                }
            }
            else
            {
                btnUpdate.Visible = false;
                btnSave.Visible = true;
            }
        }

        public void BindDropdown()
        {
            DataSet ds1 = invBL.GetCurrencyDetails();
            if (ds1.Tables["Invoice"].Rows.Count != 0)
            {
                ddlCurrency.DataSource = ds1;
                ddlCurrency.DataTextField = "CurrencyShortName";
                ddlCurrency.DataValueField = "CurrencyShortName";
                ddlCurrency.DataBind();
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            JobSave();
            ExporterSave();
        }

        private void JobSave()
        {
            string CreatedBy = (string)Session["USER-NAME"];
            string CreatedDate = System.DateTime.Now.ToString();
            string JobNo = txtJobNo.Text;
            string JobDate = txtJobDate.Text;
            string JobReceivedOn = txtJobDate.Text;
            string TransportMode = ddlTransportMode.SelectedValue;
            string CustomHouse = ddlCustomHouse.SelectedValue;
            string Filling = ddlFilling.SelectedItem.Text;
            string Fyear = (string)Session["FYear"];
            string TotNoInv = txtTotalNoOfInvoice.Text;
            string TotInValue = txtTotalInvoiceValue.Text;
            string Currency = ddlCurrency.SelectedValue;

            int result = objEMJobCreation.JobCreationSave(JobNo, JobDate, JobReceivedOn, TransportMode, CustomHouse, Filling,TotNoInv,TotInValue,Currency, CreatedBy, CreatedDate, Fyear);
            if (result == 1)
            {

                DataSet ds = objEMJobCreation.JobNo();
                DataRowView row = ds.Tables["Table"].DefaultView[0];
                int keycode = Convert.ToInt32(row["keycode"]);
                keycode = keycode + 1;
                objEMJobCreation.updateautono(keycode);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "redirect", "alert('Saved Successfully'); window.location.href='efrmJobCreation.aspx?mode=New Job';", true);
            }
        }

        private void ExporterSave()
        {
            string JobNo = txtJobNo.Text;
            string ExporterName = txtExporter.Text;
            string ExporterAddress1 = txtExporterAddress.Text;
            string BranchSno = txtBranchSNo.Text;
            string StateProvince = txtStateProvince.Text;
            string IECodeNo = txtIECode.Text;
            string Consignee = txtConsignee.Text;
            string ConsigneeAddress = txtConsigneeAddress.Text;
            string ConsigneeCountry = txtCosigneeCountry.Text;
            bool IsBuyer = cbBuyer.Checked;
            string Buyer = txtBuyer.Text;
            string BuyerAddress = txtBuyerAddress.Text;
            string ExporterRefNo = txtExporterRefNo.Text;
            string ExporterRefDate = txtExporterRefDate.Text;
            string ExporterType = ddlExporterType.SelectedValue;
            string SBNo = txtSBNo.Text;
            string SBDate = txtSBDate.Text;
            string RBIApprNo = txtRbiNo.Text;
            string RBIApprDate = txtRbiDate.Text;
            bool IsGRWaived = cbGRWaived.Checked;
            string GRNo = txtWavierNo.Text;
            string GRDate = txtWavierNoExtn.Text;
            string RBIWaiverNo = txtRbiWavierNo.Text;
            string RBIWavierDate = txtRbiWavierExtn.Text;
            string BankDealer = txtBankDealer.Text;
            string ACNo = txtAccNo.Text;
            string BankDealerCode = txtBankDealerCode.Text;
            string EPZCode = txtEpzCode.Text;
            string Notify = txtNotify.Text;
            string Address1 = txtAddressExtn.Text;
            string CreatedBy = (string)Session["USER-NAME"];
            string CreatedDate = System.DateTime.Now.ToString();
            result = objETExporter.ExporterSave(JobNo, ExporterName, "", ExporterAddress1, BranchSno, StateProvince, IECodeNo, Consignee,
                                   ConsigneeAddress, "", ConsigneeCountry, IsBuyer, Buyer, BuyerAddress, BuyerAddress, ExporterRefNo,
                                   ExporterRefDate, ExporterType, SBNo, SBDate, RBIApprNo, RBIApprDate, IsGRWaived, GRNo, GRDate, RBIWaiverNo,
                                   RBIWavierDate, BankDealer, ACNo, BankDealerCode, EPZCode, Notify, "", Address1, CreatedBy, CreatedDate);
            if (result == 1)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Saved successfully');", true);
            }
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            JobUpdate();
            ExporterUpdate();
        }

        private void JobUpdate()
        {
            string ModifiedBy = (string)Session["USER-NAME"];
            string ModifiedDate = System.DateTime.Now.ToString();
            string JobNo = txtJobNo.Text;
            string JobDate = txtJobDate.Text;
            string JobReceivedOn = txtJobDate.Text;
            string TransportMode = ddlTransportMode.SelectedValue;
            string CustomHouse = ddlCustomHouse.SelectedValue;
            string Filling = ddlFilling.SelectedValue;
            string TotNoInv = txtTotalNoOfInvoice.Text;
            string TotInValue = txtTotalInvoiceValue.Text;
            string Currency = ddlCurrency.SelectedValue;
            objEMJobCreation.JobCreationUpdate(JobNo, JobDate, JobReceivedOn, TransportMode, CustomHouse, Filling,TotNoInv,TotInValue,Currency,ModifiedBy, ModifiedDate);



            ScriptManager.RegisterStartupScript(this, this.GetType(), "redirect", "alert('Updated Successfully'); window.location.href='efrmJobCreation.aspx?mode=New';", true);
        }

        private void ExporterUpdate()
        {
            string JobNo = txtJobNo.Text;
            string ExporterName = txtExporter.Text;
            string ExporterAddress1 = txtExporterAddress.Text;
            string BranchSno = txtBranchSNo.Text;
            string StateProvince = txtStateProvince.Text;
            string IECodeNo = txtIECode.Text;
            string Consignee = txtConsignee.Text;
            string ConsigneeAddress = txtConsigneeAddress.Text;
            string ConsigneeCountry = txtCosigneeCountry.Text;
            bool IsBuyer = cbBuyer.Checked;
            string Buyer = txtBuyer.Text;
            string BuyerAddress1 = txtBuyerAddress.Text;
            string ExporterRefNo = txtExporterRefNo.Text;
            string ExporterRefDate = txtExporterRefDate.Text;
            string ExporterType = ddlExporterType.SelectedItem.Text;
            string SBNo = txtSBNo.Text;
            string SBDate = txtSBDate.Text;
            string RBIApprNo = txtRbiNo.Text;
            string RBIApprDate = txtRbiDate.Text;
            bool IsGRWaived = cbGRWaived.Checked;
            string GRNo = txtWavierNo.Text;
            string GRDate = txtWavierNoExtn.Text;
            string RBIWaiverNo = txtRbiWavierNo.Text;
            string RBIWavierDate = txtRbiWavierExtn.Text;
            string BankDealer = txtBankDealer.Text;
            string ACNo = txtAccNo.Text;
            string BankDealerCode = txtBankDealerCode.Text;
            string EPZCode = txtEpzCode.Text;
            string Notify = txtNotify.Text;
            string Address1 = txtAddressExtn.Text;
            string ModifiedBy = (string)Session["USER-NAME"];
            string ModifiedDate = System.DateTime.Now.ToString();
            result = objETExporter.ExporterUpdate(JobNo, ExporterName, "", ExporterAddress1, BranchSno, StateProvince, IECodeNo, Consignee,
                               ConsigneeAddress, "", ConsigneeCountry, IsBuyer, Buyer, BuyerAddress1, BuyerAddress1, ExporterRefNo,
                               ExporterRefDate, ExporterType, SBNo, SBDate, RBIApprNo, RBIApprDate, IsGRWaived, GRNo, GRDate, RBIWaiverNo,
                               RBIWavierDate, BankDealer, ACNo, BankDealerCode, EPZCode, Notify, "", Address1, ModifiedBy, ModifiedDate);


            ScriptManager.RegisterStartupScript(this, this.GetType(), "redirect", "alert('Updated successfully');", true);
        }

        protected void gvJobCreation_SelectedIndexChanged(object sender, EventArgs e)
        {
            JobSelect(sender,e);
            ExporterSelect();
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            JobNo = txtSearch.Text;
            GridLoad(JobNo);
        }

        private void JobSelect(object sender, EventArgs e)
        {
            btnUpdate.Visible = true;
            btnSave.Visible = false;
            string JobNo = gvJobCreation.SelectedRow.Cells[1].Text;
            txtJobNo.Text = gvJobCreation.SelectedRow.Cells[1].Text;
            AssignJobDetails(txtJobNo.Text,sender,e);
        }

        protected void btnNew_Click(object sender, EventArgs e)
        {
            Response.Redirect("efrmJobCreation.aspx?Mode=New Job");
        }

        private void JobChange(object sender, EventArgs e)
        {
            btnUpdate.Visible = true;
            btnSave.Visible = false;
            //string JobNo = gvJobCreation.SelectedRow.Cells[1].Text;
            //txtJobNo.Text = gvJobCreation.SelectedRow.Cells[1].Text;
            AssignJobDetails(txtJobNo.Text, sender, e);
        }


        private void AssignJobDetails(string JobNo,object sender,EventArgs e)
        {
            DataSet ds = new DataSet();
            ds = objEMJobCreation.GetData(JobNo);
            if (ds.Tables["Table"].Rows.Count != 0)
            {
                DataRowView row = ds.Tables["Table"].DefaultView[0];
                txtJobDate.Text = row["JobDate"].ToString();
                txtJobDate.Text = row["JobReceivedOn"].ToString();
                if (row["TransportMode"] != DBNull.Value)
                {
                    ddlTransportMode.SelectedValue = row["TransportMode"].ToString();
                }
                if (row["CustomHouse"] != DBNull.Value)
                {
                    if (ddlTransportMode.SelectedValue == "~Select~")
                    {
                        ddlCustomHouse.SelectedIndex = 0;
                    }
                    else
                    {
                        ddlTransportMode_SelectedIndexChanged(sender, e);
                        ddlCustomHouse.SelectedValue = row["CustomHouse"].ToString();
                    }
                }
                //if (row["Filling"] != DBNull.Value)
                //{
                //    ddlFilling.SelectedIndex = ddlFilling.Items.IndexOf(ddlFilling.Items.FindByText(row["Filling"].ToString()));
                //}
                //txtTotalNoOfInvoice.Text = row["TotalNoofInvoice"].ToString();
                //txtTotalInvoiceValue.Text = row["TotalInvoiceValue"].ToString();
                //ddlCurrency.SelectedValue = row["Currency"].ToString();
            }
        }

        protected void btnSearchJob_Click(object sender, EventArgs e)
        {            
                    JobChange(sender,e);
                    ExporterChange();                   
        }

        private void ExporterSelect()
        {
            div2.Visible = false;
            div1.Visible = true;
            string JobNo = gvJobCreation.SelectedRow.Cells[1].Text;
            AssignExpDetails(JobNo);
        }

        private void ExporterChange()
        {                       
            AssignExpDetails(txtJobNo.Text);
        }

        private void AssignExpDetails(string JobNo)
        {
            DataSet ds = new DataSet();
            ds = objETExporter.GetExportData(JobNo);

            if (ds.Tables[0].Rows.Count != 0)
            {
                DataRowView row = ds.Tables["Table"].DefaultView[0];
                txtExporter.Text = row["ExporterName"].ToString();
                txtExporterAddress.Text = row["ExporterAddress1"].ToString();
                txtBranchSNo.Text = row["BranchSno"].ToString();
                txtStateProvince.Text = row["StateProvince"].ToString();
                txtIECode.Text = row["IECodeNo"].ToString();
                txtConsignee.Text = row["Consignee"].ToString();
                txtConsigneeAddress.Text = row["ConsigneeAddress"].ToString();
                txtCosigneeCountry.Text = row["ConsigneeCountry"].ToString();
                cbBuyer.Checked = false;
                if (row["IsBuyer"] != DBNull.Value)
                {
                    bool IsBuyer = Convert.ToBoolean(row["IsBuyer"]);
                    cbBuyer.Checked = IsBuyer;
                }

                txtBuyer.Text = row["Buyer"].ToString();
                txtBuyerAddress.Text = row["BuyerAddress"].ToString();
                txtExporterRefDate.Text = row["ExporterRefDate"].ToString();
                txtExporterRefNo.Text = row["ExporterRefNo"].ToString();
                if (row["ExporterType"] != DBNull.Value)
                {
                    ddlExporterType.SelectedIndex = ddlExporterType.Items.IndexOf(ddlExporterType.Items.FindByText(row["ExporterType"].ToString()));
                }
                txtSBNo.Text = row["SBNo"].ToString();
                txtSBDate.Text = row["SBDate"].ToString();
                txtRbiNo.Text = row["RBIApprNo"].ToString();
                txtRbiDate.Text = row["RBIApprDate"].ToString();
                cbGRWaived.Checked = false;
                if (row["IsGRWaived"] != DBNull.Value)
                {
                    bool IsGRWaived = Convert.ToBoolean(row["IsGRWaived"]);
                    cbGRWaived.Checked = IsGRWaived;
                }

                txtWavierNo.Text = row["GRNo"].ToString();
                txtWavierNoExtn.Text = row["GRDate"].ToString();
                txtRbiWavierNo.Text = row["RBIWaiverNo"].ToString();
                txtRbiWavierExtn.Text = row["RBIWavierDate"].ToString();
                txtBankDealer.Text = row["BankDealer"].ToString();
                txtAccNo.Text = row["ACNo"].ToString();
                txtBankDealerCode.Text = row["BankDealerCode"].ToString();
                txtEpzCode.Text = row["EPZCode"].ToString();
                txtNotify.Text = row["Notify"].ToString();
                txtAddressExtn.Text = row["Address1"].ToString();
                txtBankDealerCode.Text = row["BankDealerCode"].ToString();
                txtEpzCode.Text = row["EPZCode"].ToString();
                txtNotify.Text = row["Notify"].ToString();
            }
        }

        public void BindJobCreation()
        {
            string Fyear = string.Empty;
            DataSet ds = objEMJobCreation.GetGridData(Fyear);
        }
            
        public void GridLoad(string jobno)
         {
            //string Fyear = (string)Session["FYear"];
            DataSet ds = objEMJobCreation.GetData(JobNo);
            gvJobCreation.DataSource = ds;
            gvJobCreation.DataBind();         
        }

        protected void btnClose_Click(object sender, EventArgs e)
        {
            Session["ExJobNo"] = string.Empty;
            Response.Redirect("~/HomePage.aspx");
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Session["ExJobNo"] = string.Empty;
            Response.Redirect("efrmJobCreation.aspx");
        }

        protected void bntForward_Click(object sender, EventArgs e)
        {
            Session["ExJobNo"] = txtJobNo.Text;
            Response.Redirect("efrmShipmentMain.aspx");
        }

        protected void ddlTransportMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddlCustomHouse.Enabled = true;
            string Query = "Select * from M_Custom Where Mode ='"+ddlTransportMode.SelectedValue+"' ";
            DataSet ds = objCommonDL.GetDataSet(Query);
            ddlCustomHouse.Items.Clear();
            ddlCustomHouse.DataSource = ds;
            ddlCustomHouse.DataTextField = "custom";
            ddlCustomHouse.DataValueField = "custom";
            ddlCustomHouse.DataBind();
            ddlCustomHouse.Items.Insert(0, new ListItem("~Select~", "0"));
        }

        //protected void txtSearchExp_TextChanged(object sender, EventArgs e)
        //{
        //    string Query = "Select Distinct AccountName,Address1,State,Search from View_AccountMaster where Search = '" + txtSearchExp.Text + "'";
        //    DataSet ds = objCommonDL.GetDataSet(Query);
        //    if (ds.Tables["Table"].Rows.Count != 0)
        //    {
        //        DataRowView row = ds.Tables["Table"].DefaultView[0];
        //        txtExporter.Text = row["AccountName"].ToString();
        //        txtExporterAddress.Text = row["Address1"].ToString();
        //        txtStateProvince.Text = row["State"].ToString();
        //    }
        //    else
        //    {
        //        txtExporter.Text = txtSearchExp.Text.ToString();
        //    }
        //}

        protected void txtSearchNotify_TextChanged(object sender, EventArgs e)
        {

            string Query = "Select Distinct AccountName,Address1,State,Search from View_AccountMaster where Search = '" + txtSearchNotify.Text + "'";
            DataSet ds = objCommonDL.GetDataSet(Query);
            if (ds.Tables["Table"].Rows.Count != 0)
            {
                DataRowView row = ds.Tables["Table"].DefaultView[0];
                txtNotify.Text = row["AccountName"].ToString();
                txtAddressExtn.Text = row["Address1"].ToString();
                //txtStateProvince.Text = row["State"].ToString();
            }
            else
            {
                txtNotify.Text = txtSearchNotify.Text.ToString();
            }
        }

        protected void txtSearchBuyer_TextChanged(object sender, EventArgs e)
        {
            string Query = "Select Distinct AccountName,Address1,State,Search from View_AccountMaster where Search = '" + txtSearchBuyer.Text + "'";
            DataSet ds = objCommonDL.GetDataSet(Query);
            if (ds.Tables["Table"].Rows.Count != 0)
            {
                DataRowView row = ds.Tables["Table"].DefaultView[0];
                txtBuyer.Text = row["AccountName"].ToString();
                txtBuyerAddress.Text = row["Address1"].ToString();
                //txtStateProvince.Text = row["State"].ToString();
            }
            else
            {
                txtBuyer.Text = txtSearchBuyer.Text.ToString();
            }
        }

        protected void txtSearchConsignee_TextChanged(object sender, EventArgs e)
        {
            string Query = "Select Distinct AccountName,Address1,State,Country,Search from View_AccountMaster where Search = '" + txtSearchConsignee.Text + "'";
            DataSet ds = objCommonDL.GetDataSet(Query);
            if (ds.Tables["Table"].Rows.Count != 0)
            {
                DataRowView row = ds.Tables["Table"].DefaultView[0];
                txtConsignee.Text = row["AccountName"].ToString();
                txtCosigneeCountry.Text = row["Country"].ToString();
                txtConsigneeAddress.Text = row["Address1"].ToString();
            }
            else
            {
                //txtConsignee.Text = txtSearchExp.Text.ToString();
            }
        }

        protected void txtRbiWavierExtn_TextChanged(object sender, EventArgs e)
        {

        }
    }
}