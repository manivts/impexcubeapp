using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using System.Drawing;
using VTS.ImpexCube.Data;
using VTS.ImpexCube.Business;

namespace ImpexCube
{
    public partial class efrmExporterDetails : System.Web.UI.Page
    {
        VTS.ImpexCube.Business.ETExporterBL objETExporter = new VTS.ImpexCube.Business.ETExporterBL();
        CommonDL objCommonDL = new CommonDL();
        int result;
        string ExJobNo = string.Empty;
        string strcon = (string)ConfigurationManager.ConnectionStrings["Constr"].ConnectionString;
        EMJobCreationBL objEMJobCreation = new EMJobCreationBL();

        protected void Page_Load(object sender, EventArgs e)
        {            
            if (!IsPostBack)
            {
                if (Request.QueryString["Mode"] == "" || Request.QueryString["Mode"] == null)
                {
                    ExJobNo = (string)Session["ExJobNo"];
                }
                else if (Request.QueryString["Mode"] == "Direct")
                {
                    ExJobNo = string.Empty;
                }                
                JobNo();
                if (ExJobNo == "" || ExJobNo == null || ExJobNo == "~Select~")
                {
                    btnUpdate.Visible = false;
                    btnSave.Visible = true;
                }
                else
                {
                    GetJobDetails();
                }
            }
        }

        private void GetJobDetails()
        {
            ddlJobnoExporter.SelectedValue = ExJobNo;
            string JobNo = ddlJobnoExporter.SelectedItem.Text;
            DataSet ds = new DataSet();
            ds = objETExporter.GetExportData(JobNo);

            if (ds.Tables[0].Rows.Count != 0)
            {
                DataRowView row = ds.Tables["Table"].DefaultView[0];
                //ddlJobnoExporter.SelectedItem.Text = row["JobNo"].ToString();
                txtExporter.Text = row["ExporterName"].ToString();
                //if (row["ExporterAddress"] != DBNull.Value)
                //{
                //    ddlExporterAddress.SelectedIndex = ddlExporterAddress.Items.IndexOf(ddlExporterAddress.Items.FindByText(row["ExporterAddress"].ToString()));
                //    //ddlExporterAddress.SelectedValue = row["ExporterAddress"].ToString();
                //}                
                txtExporterAddress.Text = row["ExporterAddress1"].ToString();
                txtBranchSNo.Text = row["BranchSno"].ToString();
                txtStateProvince.Text = row["StateProvince"].ToString();
                txtIECode.Text = row["IECodeNo"].ToString();
                txtConsignee.Text = row["Consignee"].ToString();
                txtConsigneeAddress.Text = row["ConsigneeAddress"].ToString();
                //if (row["ConsigneeAddress1"] != DBNull.Value)
                //{
                //    ddlConsigneeAddress.SelectedIndex = ddlConsigneeAddress.Items.IndexOf(ddlConsigneeAddress.Items.FindByText(row["ConsigneeAddress1"].ToString()));
                //    //ddlConsigneeAddress.SelectedValue = row["ConsigneeAddress1"].ToString();
                //}
                txtCosigneeCountry.Text = row["ConsigneeCountry"].ToString();
                cbBuyer.Checked = false;
                if (row["IsBuyer"] != DBNull.Value)
                {
                    bool IsBuyer = Convert.ToBoolean(row["IsBuyer"]);
                    cbBuyer.Checked = IsBuyer;
                }

                txtBuyer.Text = row["Buyer"].ToString();
                //if (row["BuyerAddress"] != DBNull.Value)
                //{
                //    ddlBuyerAddress.SelectedIndex = ddlBuyerAddress.Items.IndexOf(ddlBuyerAddress.Items.FindByText(row["BuyerAddress"].ToString()));
                //    //ddlBuyerAddress.SelectedValue = row["BuyerAddress"].ToString();
                //}
                txtBuyerAddress.Text = row["BuyerAddress1"].ToString();
                txtExporterRefDate.Text = row["ExporterRefDate"].ToString();
                txtExporterRefNo.Text = row["ExporterRefNo"].ToString();
                if (row["ExporterType"] != DBNull.Value)
                {
                    ddlExporterType.SelectedIndex = ddlExporterType.Items.IndexOf(ddlExporterType.Items.FindByText(row["ExporterType"].ToString()));
                    //ddlExporterType.SelectedValue = row["ExporterType"].ToString();
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
                //ddlAddress.SelectedValue = row["Address"].ToString();
                txtAddressExtn.Text = row["Address1"].ToString();
                txtBankDealerCode.Text = row["BankDealerCode"].ToString();
                txtEpzCode.Text = row["EPZCode"].ToString();
                txtNotify.Text = row["Notify"].ToString();

                btnUpdate.Visible = true;
                btnSave.Visible = false;
                jobbind();
            }
            else
            {
                DataSet exds = new DataSet();
                exds = objEMJobCreation.GetData(JobNo);
                if (exds.Tables[0].Rows.Count != 0)
                {
                    btnUpdate.Visible = true;
                    btnSave.Visible = false;
                    DataRowView rw = exds.Tables[0].DefaultView[0];
                    lblJobReceivedDate.Text = rw["JobDate"].ToString();
                    lblMode.Text = rw["TransportMode"].ToString();
                    lblCustom.Text = rw["CustomHouse"].ToString();                    
                }
                btnUpdate.Visible = false;
                btnSave.Visible = true;
            }
        }
       
        protected void btnSave_Click(object sender, EventArgs e)
        {
            string JobNo = ddlJobnoExporter.SelectedItem.Text;
            string ExporterName = txtExporter.Text;
            //string ExporterAddress = ddlExporterAddress.SelectedValue;
            string ExporterAddress1 = txtExporterAddress.Text;
            string BranchSno = txtBranchSNo.Text;
            string StateProvince = txtStateProvince.Text;
            string IECodeNo = txtIECode.Text;
            string Consignee = txtConsignee.Text;
            string ConsigneeAddress = txtConsigneeAddress.Text;
            //string ConsigneeAddress1 = ddlConsigneeAddress.SelectedValue;
            string ConsigneeCountry = txtCosigneeCountry.Text;
            bool IsBuyer = cbBuyer.Checked;
            string Buyer = txtBuyer.Text;
            string BuyerAddress = txtBuyerAddress.Text;
           // string BuyerAddress1 = ddlBuyerAddress.SelectedValue;
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
            //string Address = ddlAddress.SelectedValue;
            string Address1 = txtAddressExtn.Text;
            string CreatedBy = (string)Session["USER-NAME"];
            string CreatedDate = System.DateTime.Now.ToString();
            result = objETExporter.ExporterSave(JobNo, ExporterName, "", ExporterAddress1, BranchSno, StateProvince, IECodeNo, Consignee,
                                   ConsigneeAddress, "", ConsigneeCountry,IsBuyer, Buyer, BuyerAddress, "", ExporterRefNo,
                                   ExporterRefDate, ExporterType, SBNo, SBDate, RBIApprNo, RBIApprDate,IsGRWaived, GRNo, GRDate, RBIWaiverNo,
                                   RBIWavierDate, BankDealer, ACNo, BankDealerCode, EPZCode, Notify, "", Address1, CreatedBy, CreatedDate);
            if (result == 1)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Saved successfully');", true);
            }            
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            string JobNo = ddlJobnoExporter.SelectedItem.Text;
            string ExporterName = txtExporter.Text;
           // string ExporterAddress = ddlExporterAddress.SelectedItem.Text;
            string ExporterAddress1 = txtExporterAddress.Text;
            string BranchSno = txtBranchSNo.Text;
            string StateProvince = txtStateProvince.Text;
            string IECodeNo = txtIECode.Text;
            string Consignee = txtConsignee.Text;
            string ConsigneeAddress = txtConsigneeAddress.Text;
           // string ConsigneeAddress1 = ddlConsigneeAddress.SelectedItem.Text;
            string ConsigneeCountry = txtCosigneeCountry.Text;
            bool IsBuyer = cbBuyer.Checked;
            string Buyer = txtBuyer.Text;
            //string BuyerAddress =ddlBuyerAddress.SelectedValue;
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
           // string Address = ddlAddress.SelectedValue;
            string Address1 = txtAddressExtn.Text;            
           string ModifiedBy = (string)Session["USER-NAME"];
           string ModifiedDate = System.DateTime.Now.ToString();
           result = objETExporter.ExporterUpdate(JobNo, ExporterName, "", ExporterAddress1, BranchSno, StateProvince, IECodeNo, Consignee,
                              ConsigneeAddress, "", ConsigneeCountry, IsBuyer,Buyer, "", BuyerAddress1, ExporterRefNo,
                              ExporterRefDate, ExporterType, SBNo, SBDate, RBIApprNo, RBIApprDate, IsGRWaived,GRNo, GRDate, RBIWaiverNo,
                              RBIWavierDate, BankDealer, ACNo, BankDealerCode, EPZCode, Notify, "", Address1, ModifiedBy, ModifiedDate);
           
           
               ScriptManager.RegisterStartupScript(this, this.GetType(), "redirect", "alert('Updated successfully');", true);
               
               btnSave.Visible = false;
               btnUpdate.Visible = true;
        }

        public void JobNo()
        {
            DataSet ds = new DataSet();
            string quer = "select * from E_M_JobCreation";
            ds = objCommonDL.GetDataSet(quer);
            if (ds.Tables["Table"].Rows.Count != 0)
            {
                ddlJobnoExporter.DataSource = ds;
                ddlJobnoExporter.DataTextField = "JobNo";
                ddlJobnoExporter.DataValueField = "JobNo";
                ddlJobnoExporter.DataBind();
                ddlJobnoExporter.Items.Insert(0, new ListItem("~Select~", "0"));
            }
        }

        public void jobbind()
        {
            DataSet ds = new DataSet();
            string query1 = "select JobNo,JobDate,CustomHouse,TransportMode from E_M_JobCreation where JobNo = '" + ddlJobnoExporter.SelectedValue + "'";
            ds = objCommonDL.GetDataSet(query1);
            if (ds.Tables["Table"].Rows.Count != 0)
            {
                DataRowView row = ds.Tables["Table"].DefaultView[0];
                ddlJobnoExporter.SelectedValue = row["JobNo"].ToString();
                lblJobReceivedDate.Text = row["JobDate"].ToString();
                lblMode.Text = row["TransportMode"].ToString();
                lblCustom.Text = row["CustomHouse"].ToString();
            }

        }

        protected void ddlJobnoExporter_SelectedIndexChanged(object sender, EventArgs e)
        {
            string JobNo = ddlJobnoExporter.SelectedItem.Text;
            DataSet ds = new DataSet();
            ds = objETExporter.GetExportData(JobNo);

            if (ds.Tables[0].Rows.Count != 0)
            {
                DataRowView row = ds.Tables["Table"].DefaultView[0];
                //ddlJobnoExporter.SelectedItem.Text = row["JobNo"].ToString();
                txtExporter.Text = row["ExporterName"].ToString();
                //if (row["ExporterAddress"] != DBNull.Value)
                //{
                //    ddlExporterAddress.SelectedIndex = ddlExporterAddress.Items.IndexOf(ddlExporterAddress.Items.FindByText(row["ExporterAddress"].ToString()));                    
                //    //ddlExporterAddress.SelectedValue = row["ExporterAddress"].ToString();
                //}
                txtExporterAddress.Text = row["ExporterAddress1"].ToString();
                txtBranchSNo.Text = row["BranchSno"].ToString();
                txtStateProvince.Text = row["StateProvince"].ToString();
                txtIECode.Text = row["IECodeNo"].ToString();
                txtConsignee.Text = row["Consignee"].ToString();
                txtConsigneeAddress.Text = row["ConsigneeAddress"].ToString();
                //if (row["ConsigneeAddress1"] != DBNull.Value)
                //{
                //    ddlConsigneeAddress.SelectedIndex = ddlConsigneeAddress.Items.IndexOf(ddlConsigneeAddress.Items.FindByText(row["ConsigneeAddress1"].ToString()));                    
                //    //ddlConsigneeAddress.SelectedValue = row["ConsigneeAddress1"].ToString();
                //}
                txtCosigneeCountry.Text = row["ConsigneeCountry"].ToString();
                cbBuyer.Checked = false;
                if (row["IsBuyer"] != DBNull.Value)
                {
                    bool IsBuyer = Convert.ToBoolean(row["IsBuyer"]);
                    cbBuyer.Checked = IsBuyer;
                }  
                
                txtBuyer.Text = row["Buyer"].ToString();
                //if (row["BuyerAddress"]!= DBNull.Value)
                //{
                //    ddlBuyerAddress.SelectedIndex = ddlBuyerAddress.Items.IndexOf(ddlBuyerAddress.Items.FindByText(row["BuyerAddress"].ToString()));                    
                //    //ddlBuyerAddress.SelectedValue = row["BuyerAddress"].ToString();
                //}
                txtBuyerAddress.Text = row["BuyerAddress1"].ToString();
                txtExporterRefDate.Text = row["ExporterRefDate"].ToString();
                txtExporterRefNo.Text = row["ExporterRefNo"].ToString();
                if (row["ExporterType"] != DBNull.Value)
                {
                    ddlExporterType.SelectedIndex = ddlExporterType.Items.IndexOf(ddlExporterType.Items.FindByText(row["ExporterType"].ToString()));                    
                    //ddlExporterType.SelectedValue = row["ExporterType"].ToString();
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
                //ddlAddress.SelectedValue = row["Address"].ToString();
                txtAddressExtn.Text = row["Address1"].ToString();
                txtBankDealerCode.Text = row["BankDealerCode"].ToString();
                txtEpzCode.Text = row["EPZCode"].ToString();
                txtNotify.Text = row["Notify"].ToString();


                btnUpdate.Visible = true;
                btnSave.Visible = false;
                jobbind();
            }
            else
            {
                jobbind();
                clear();
                
            }
        }

        protected void clear()
        {
            //ddlJobnoExporter.SelectedValue = "0";
            //lblJobReceivedDate.Text = "";
            txtExporter.Text ="";
            //ddlExporterAddress.SelectedValue = "~Select~";
            txtExporterAddress.Text = "";
            txtBranchSNo.Text = "";
            txtStateProvince.Text =""; 
            txtIECode.Text ="";
            txtConsignee.Text ="";
            txtConsigneeAddress.Text ="";
            //ddlConsigneeAddress.SelectedValue = "~Select~"; 
            txtCosigneeCountry.Text = "";
            cbBuyer.Checked = false;
            txtBuyer.Text ="";
            //ddlBuyerAddress.Text = "~Select~"; 
            txtBuyerAddress.Text = "";
            txtExporterRefDate.Text = "";
            txtExporterRefNo.Text = "";
            ddlExporterType.SelectedValue = "~Select~"; 
            txtSBNo.Text = "";
            txtSBDate.Text = "";
            txtRbiNo.Text = "";
            txtRbiDate.Text = "";
            cbGRWaived.Checked = false;
            txtWavierNo.Text = "";
            txtWavierNoExtn.Text =""; 
            txtRbiWavierNo.Text = "";
            txtRbiWavierExtn.Text = "";
            txtBankDealer.Text = "";
            txtAccNo.Text = "";
            txtBankDealerCode.Text =""; 
            txtEpzCode.Text ="";
            txtNotify.Text = "";
            //  ddlAddress.SelectedValue = "~Select~";
            txtAddressExtn.Text =""; 
            txtBankDealerCode.Text =""; 
            txtEpzCode.Text = "";
            txtNotify.Text = "";

        }

        protected void ddlExporterType_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Session["ExJobNo"] = string.Empty;
            Response.Redirect("efrmExporterDetails.aspx");
        }

        protected void btnClose_Click(object sender, EventArgs e)
        {
            Session["ExJobNo"] = string.Empty;
            Response.Redirect("~/HomePage.aspx");
        }

        protected void txtSBNo_TextChanged(object sender, EventArgs e)
        {

        }

        protected void txtConsignee_TextChanged(object sender, EventArgs e)
        {

        }

        protected void btnReturn_Click(object sender, EventArgs e)
        {
            Session["ExJobNo"] = ddlJobnoExporter.SelectedItem.Text;
            Response.Redirect("efrmJobCreation.aspx");
        }

        protected void btnForward_Click(object sender, EventArgs e)
        {
            Session["ExJobNo"] = ddlJobnoExporter.SelectedItem.Text;
            Response.Redirect("efrmShipmentMain.aspx");
        }
      
    }
}