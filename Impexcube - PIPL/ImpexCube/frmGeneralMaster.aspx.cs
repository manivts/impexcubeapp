using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data;
using VTS.ImpexCube.Data;
using System.Text;
using System.Data.SqlClient;

namespace ImpexCube
{
    public partial class frmGeneralMaster : System.Web.UI.Page
    {
        string strconn = (string)ConfigurationManager.AppSettings["ConnectionDashboard"];
        CommonDL objCommonDL = new CommonDL();
        int Result = 0;
        string AutoQuery = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack == false)
            {
                Label pagename;
                //cntname = (ContentPlaceHolder)Master.FindControl("");
                pagename = (Label)Master.FindControl("lblName");
                pagename.Text = Request.QueryString["MODE"] + " Master"; ;

                Session["BranchMode"] = "";
                //lblMaster.Text = Request.QueryString["MODE"] + " Master";
                string keyname = "";
                if (Request.QueryString["MODE"] == "AirLine")
                {
                    keyname = "AL";
                    GridLoad();
                }
                else if (Request.QueryString["MODE"] == "CFS")
                {
                    keyname = "CF";
                    GridLoad();
                }
                else if (Request.QueryString["MODE"] == "ShippingLine")
                {
                    keyname = "SH";
                    GridLoad();
                }
                else if (Request.QueryString["MODE"] == "FF")
                {
                    keyname = "FF";
                    GridLoad();
                }
                else if (Request.QueryString["MODE"] == "Customer")
                {
                    ChkKAM1.Visible = true;
                    ChkKAM2.Visible = true;
                    ddlKAM1.Visible = true;
                    ddlKAM2.Visible = true;
                    keyname = "CU";
                    GridLoad();
                    lblAccountsGroup.Visible = true;
                    ddlAccountGroup.Visible = true;
                }
                else if (Request.QueryString["MODE"] == "Consignor")
                {
                    keyname = "CR";
                    GridLoad();
                }
                else if (Request.QueryString["MODE"] == "Accounts")
                {
                    keyname = "AC";
                    GridLoadAll();
                    lblAccountsGroup.Visible = true;
                    ddlAccountGroup.Visible = true;
                }
                Session["keyname"] = "";
                Session["keyname"] = keyname;
                AutoGenerate(keyname);
                btnUpdate.Visible = false;
                btnBranchAdd.Visible = false;
                filldropdown();
                
                hdnCommonMaster.Value = string.Empty;
            }
        }
        protected void Page_PreInit(object sender, EventArgs e)
        {
            if (Request.QueryString["MODE"] == "Accounts")
            {
                this.Page.MasterPageFile = "~/Accounts/MainMaster.Master";
            }
        }
        private void AutoGenerate(string keyname)
        {

            AutoQuery = "select keycode from [M_AutoGenerate] where Keyname ='" + keyname + "'";
            int auto = 0;
            DataSet autods = objCommonDL.GetDataSet(AutoQuery);
            if (autods.Tables["Table"].Rows.Count != 0)
            {
                DataRowView dr = autods.Tables["Table"].DefaultView[0];
                auto = Convert.ToInt32(dr["keycode"].ToString());
                auto = auto + 1;
                txtAcountCode.Text = keyname + auto.ToString();
            }
        }
        private void Updatesno(string keyname, string keycode)
        {
            AutoQuery = "update M_AutoGenerate set keycode='" + keycode + "' where Keyname='" + keyname + "'";
            objCommonDL.ExecuteNonQuery(AutoQuery);
        }
        public void filldropdown()
        {
            DataSet dsCountry = new DataSet();
            string quer1 = "select * from M_Country";
            dsCountry = objCommonDL.GetDataSet(quer1);
            ddlCountry.DataSource = dsCountry;
            ddlCountry.DataTextField = "CountryName";
            ddlCountry.DataValueField = "CountryCode";
            ddlCountry.DataBind();
            ddlCountry.Items.Insert(0, new ListItem("~Select~", "0"));

            DataSet dsCurrency = new DataSet();
            string quer2 = "select * from [M_Currency]";
            dsCurrency = objCommonDL.GetDataSet(quer2);
            ddlCurrency.DataSource = dsCurrency;
            ddlCurrency.DataTextField = "CurrencyShortName";
            ddlCurrency.DataValueField = "CurrencyShortName";
            ddlCurrency.DataBind();
            ddlCurrency.Items.Insert(0, new ListItem("~Select~", "0"));

            DataSet dsAccountsGroup = new DataSet();
            string quer3 = "select [GroupCode],[GroupName] from [M_AccountsGroup]";
            dsAccountsGroup = objCommonDL.GetDataSet(quer3);
            ddlAccountGroup.DataSource = dsAccountsGroup;
            ddlAccountGroup.DataTextField = "GroupName";
            ddlAccountGroup.DataValueField = "GroupName";
            ddlAccountGroup.DataBind();
            ddlAccountGroup.Items.Insert(0, new ListItem("~Select~", "0"));
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            string Message = string.Empty;
            try
            {
                InsertAccountMaster();
                InsertAccountDetails();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "redirect", "alert('Saved Successfully'); window.location.href='frmGeneralMaster.aspx?MODE=" + (string)Request.QueryString["MODE"] + "';", true);
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('DataBase Error: " + ex.Message + "');", true);
            }
        }
        public void InsertAccountMaster()
        {
            string CreatedBy = (string)Session["USER-NAME"];
            string CreatedDate = DateTime.Now.ToString();

            StringBuilder Query = new StringBuilder();
            string Message = string.Empty;
            DataSet ds = new DataSet();
            try
            {
                using (SqlConnection con = new SqlConnection(strconn))
                {
                    con.Open();
                    Query.Append("INSERT INTO [M_AccountMaster] (AccountCode, AccountName, ShortName,AccountType, Prefix, BranchId, BranchName, Address1, Address2,");
                    Query.Append("Address3, City, State, Pincode, PhoneNo, Mobile, EmailID, Website, PANNo, TINNo, CSTNo, VATNo, STaxNo, ContactPerson, Country,");
                    Query.Append("Countrycode, LST, IncomeTaxNo,IECode,PaymentPeriod,CreditLimit,Currency,Acc_group,ADCode,CreatedBy,CreatedDate,KAM1,KAM2,KAM1Name,KAM2Name,InvSeqNo,OpeningBalance,DRCR,RefName,BillWiseOn,TallyAccountName)");
                    Query.Append("values(@AccountCode, @AccountName, @ShortName,@AccountType, @Prefix,@BranchId,@BranchName,@Address1,@Address2,@Address3,@City,@State,@Pincode,@PhoneNo,@Mobile,@EmailID,@Website,@PANNo,@TINNo,@CSTNo,@VATNo,");
                    Query.Append("@STaxNo,@ContactPerson,@Country,@Countrycode,@LST,@IncomeTaxNo,@IECode,@PaymentPeriod,@CreditLimit,@Currency,@Acc_group,@ADCode,@CreatedBy,@CreatedDate,@KAM1,@KAM2,@KAM1Name,@KAM2Name,@InvSeqNo,@OpeningBalance,@DRCR,@RefName,@BillWiseOn,@TallyAccountName)");
                    SqlCommand cmd = new SqlCommand(Query.ToString(), con);
                    cmd.Parameters.AddWithValue("@AccountCode", "" + txtAcountCode.Text + "");
                    cmd.Parameters.AddWithValue("@AccountName", "" + txtName.Text + "");
                    cmd.Parameters.AddWithValue("@ShortName", "" + txtShortName.Text + "");
                    cmd.Parameters.AddWithValue("@AccountType", "" + Request.QueryString["MODE"] + "");
                    cmd.Parameters.AddWithValue("@Prefix", "" + txtPrefix.Text + "");
                    cmd.Parameters.AddWithValue("@BranchId", "" + txtBranchId.Text + "");
                    cmd.Parameters.AddWithValue("@BranchName", "" + txtBranchName.Text + "");
                    cmd.Parameters.AddWithValue("@Address1", "" + txtAddress1.Text + "");
                    cmd.Parameters.AddWithValue("@Address2", "" + txtAddress2.Text + "");
                    cmd.Parameters.AddWithValue("@Address3", "" + txtAddress3.Text + "");
                    cmd.Parameters.AddWithValue("@City", "" + txtCity.Text + "");
                    cmd.Parameters.AddWithValue("@State", "" + txtState.Text + "");
                    cmd.Parameters.AddWithValue("@Pincode", "" + txtPinCode.Text + "");
                    cmd.Parameters.AddWithValue("@PhoneNo", "" + txtPhoneNo.Text + "");
                    cmd.Parameters.AddWithValue("@Mobile", "" + txtMobileNo.Text + "");
                    cmd.Parameters.AddWithValue("@EmailID", "" + txtEmailId.Text + "");
                    cmd.Parameters.AddWithValue("@Website", "" + txtWebsite.Text + "");
                    cmd.Parameters.AddWithValue("@PANNo", "" + txtPANNo.Text + "");
                    cmd.Parameters.AddWithValue("@TINNo", "" + txtTINno.Text + "");
                    cmd.Parameters.AddWithValue("@CSTNo", "" + txtCSTNo.Text + "");
                    cmd.Parameters.AddWithValue("@VATNo", "" + txtTINno.Text + "");
                    cmd.Parameters.AddWithValue("@STaxNo", "" + txtSTaxNo.Text + "");
                    cmd.Parameters.AddWithValue("@ContactPerson", "" + txtContactPerson.Text + "");
                    cmd.Parameters.AddWithValue("@Country", "" + ddlCountry.SelectedItem.Text + "");
                    cmd.Parameters.AddWithValue("@Countrycode", "" + ddlCountry.SelectedValue + "");
                    cmd.Parameters.AddWithValue("@LST", "" + txtTINno.Text + "");
                    cmd.Parameters.AddWithValue("@IncomeTaxNo", "" + txtIncomeTaxNo.Text + "");
                    cmd.Parameters.AddWithValue("@IECode", "" + txtIECode.Text + "");
                    cmd.Parameters.AddWithValue("@PaymentPeriod", "" + ddlPaymentPeriod.SelectedValue + "");
                    cmd.Parameters.AddWithValue("@CreditLimit", "" + txtCreditLimit.Text + "");
                    cmd.Parameters.AddWithValue("@Currency", "" + ddlCurrency.SelectedValue + "");
                    cmd.Parameters.AddWithValue("@Acc_group", "" + ddlAccountGroup.SelectedValue + "");
                    if (txtOpeninBalance.Text == "")
                    {
                        txtOpeninBalance.Text = "0.00";
                    }
                    cmd.Parameters.AddWithValue("@OpeningBalance", "" + Convert.ToDouble(txtOpeninBalance.Text) + "");
                    cmd.Parameters.AddWithValue("@DRCR", "" + ddlCRDR.SelectedItem.Text + "");
              cmd.Parameters.AddWithValue("@RefName", "" + txtRefName.Text + "");
                    cmd.Parameters.AddWithValue("@BillWiseOn", "" + ChkCostCenter.Checked + "");
                    cmd.Parameters.AddWithValue("@TallyAccountName", "" + txtTallyAccountName.Text + "");
 //OpeningBalance,DRCR,RefName,BillWiseOn,TallyAccountName

                    cmd.Parameters.AddWithValue("@ADCode", "" + txtADCode.Text + "");
                    cmd.Parameters.AddWithValue("@CreatedBy", "" + CreatedBy + "");
                    cmd.Parameters.AddWithValue("@CreatedDate", "" + CreatedDate + "");
                    cmd.Parameters.AddWithValue("@KAM1", "" + ChkKAM1.Checked + "");
                    cmd.Parameters.AddWithValue("@KAM2", "" + ChkKAM2.Checked + "");
                    cmd.Parameters.AddWithValue("@KAM1Name", "" + ddlKAM1.SelectedValue + "");
                    cmd.Parameters.AddWithValue("@KAM2Name", "" + ddlKAM2.SelectedValue + "");
                    cmd.Parameters.AddWithValue("@InvSeqNo", "" + txtinvseqno.Text + "");
                    Result = cmd.ExecuteNonQuery();
                    con.Close();
                }
                if (Result == 1)
                {
                    string number = txtAcountCode.Text;
                    string num = number.Remove(0, 2);
                    Updatesno((string)Session["keyname"], num);
                }
            }
            catch (SqlException ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert(' " + ex.Message + " ');", true);
            }
        }
        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            string ModifiedBy = (string)Session["USER-NAME"];
            string ModifiedDate = DateTime.Now.ToString();

            StringBuilder Query = new StringBuilder();
            string Message = string.Empty;
            DataSet ds = new DataSet();
            try
            {
                using (SqlConnection con = new SqlConnection(strconn))
                {
                    Query.Append("UPDATE [M_AccountMaster] SET AccountCode=@AccountCode, AccountName=@AccountName, ShortName=@ShortName,AccountType=@AccountType,");
                    Query.Append("Prefix=@Prefix, BranchId=@BranchId, BranchName=@BranchName, Address1=@Address1, Address2=@Address2,Address3=@Address3, City=@City, State=@State,");
                    Query.Append("Pincode=@Pincode,PhoneNo=@PhoneNo,Mobile=@Mobile,EmailID=@EmailID,Website=@Website,PANNo=@PANNo, TINNo=@TINNo, CSTNo=@CSTNo,");
                    Query.Append("STaxNo=@STaxNo,ContactPerson=@ContactPerson,Country=@Country,Countrycode=@Countrycode,IncomeTaxNo=@IncomeTaxNo,IECode=@IECode,");
                    Query.Append("PaymentPeriod=@PaymentPeriod,CreditLimit=@CreditLimit,Currency=@Currency,Acc_group=@Acc_group,ADCode=@ADCode,KAM1=@KAM1,KAM2=@KAM2,");
                    Query.Append("KAM1Name=@KAM1Name,KAM2Name=@KAM2Name,ModifiedBy=@ModifiedBy,ModifiedDate=@ModifiedDate,InvSeqNo=@InvSeqNo ,OpeningBalance=@OpeningBalance,DRCR=@DRCR,RefName=@RefName,BillWiseOn=@BillWiseOn,TallyAccountName=@TallyAccountName WHERE TransId=@TransId");
                    con.Open();
                    SqlCommand cmd = new SqlCommand(Query.ToString(), con);

                    cmd.Parameters.AddWithValue("@AccountCode", txtAcountCode.Text);
                    cmd.Parameters.AddWithValue("@AccountName", txtName.Text);
                    cmd.Parameters.AddWithValue("@ShortName", txtShortName.Text);
                    cmd.Parameters.AddWithValue("@AccountType", Request.QueryString["MODE"]);
                    cmd.Parameters.AddWithValue("@Prefix", txtPrefix.Text);
                    cmd.Parameters.AddWithValue("@BranchId", txtBranchId.Text);
                    cmd.Parameters.AddWithValue("@BranchName", txtBranchName.Text);
                    cmd.Parameters.AddWithValue("@Address1", txtAddress1.Text);
                    cmd.Parameters.AddWithValue("@Address2", txtAddress2.Text);
                    cmd.Parameters.AddWithValue("@Address3", txtAddress3.Text);
                    cmd.Parameters.AddWithValue("@City", txtCity.Text);
                    cmd.Parameters.AddWithValue("@State", txtState.Text);
                    cmd.Parameters.AddWithValue("@Pincode", txtPinCode.Text);
                    cmd.Parameters.AddWithValue("@PhoneNo", txtPhoneNo.Text);
                    cmd.Parameters.AddWithValue("@Mobile", txtMobileNo.Text);
                    cmd.Parameters.AddWithValue("@EmailID", txtEmailId.Text);
                    cmd.Parameters.AddWithValue("@Website", txtWebsite.Text);
                    cmd.Parameters.AddWithValue("@PANNo", txtPANNo.Text);
                    cmd.Parameters.AddWithValue("@TINNo", txtTINno.Text);
                    cmd.Parameters.AddWithValue("@CSTNo", txtCSTNo.Text);
                    cmd.Parameters.AddWithValue("@STaxNo", txtSTaxNo.Text);
                    cmd.Parameters.AddWithValue("@ContactPerson", txtContactPerson.Text);
                    cmd.Parameters.AddWithValue("@Country", ddlCountry.SelectedItem.Text);
                    cmd.Parameters.AddWithValue("@Countrycode", ddlCountry.SelectedValue);
                    cmd.Parameters.AddWithValue("@IncomeTaxNo", txtIncomeTaxNo.Text);
                    cmd.Parameters.AddWithValue("@IECode", txtIECode.Text);
                    cmd.Parameters.AddWithValue("@PaymentPeriod", ddlPaymentPeriod.SelectedValue);
                    cmd.Parameters.AddWithValue("@CreditLimit", txtCreditLimit.Text);
                    cmd.Parameters.AddWithValue("@Currency", ddlCurrency.SelectedValue);
                    cmd.Parameters.AddWithValue("@Acc_group",  ddlAccountGroup.SelectedValue);
                    cmd.Parameters.AddWithValue("@ADCode", txtADCode.Text );
                    cmd.Parameters.AddWithValue("@ModifiedBy", ModifiedBy);
                    cmd.Parameters.AddWithValue("@ModifiedDate", ModifiedDate);
                    cmd.Parameters.AddWithValue("@TransId", hdnCommonMaster.Value);
                    cmd.Parameters.AddWithValue("@KAM1", ChkKAM1.Checked );
                    cmd.Parameters.AddWithValue("@KAM2", ChkKAM2.Checked);
                    cmd.Parameters.AddWithValue("@KAM1Name",  ddlKAM1.SelectedValue);
                    cmd.Parameters.AddWithValue("@KAM2Name", ddlKAM2.SelectedValue );
                    cmd.Parameters.AddWithValue("@InvSeqNo",  txtinvseqno.Text );
                    if (txtOpeninBalance.Text == "")
                    {
                        txtOpeninBalance.Text = "0.00";
                    }
                    cmd.Parameters.AddWithValue("@OpeningBalance", "" + Convert.ToDouble(txtOpeninBalance.Text) + "");
                    cmd.Parameters.AddWithValue("@DRCR", "" + ddlCRDR.SelectedItem.Text + "");
                    cmd.Parameters.AddWithValue("@RefName", "" + txtRefName.Text + "");
                    cmd.Parameters.AddWithValue("@BillWiseOn", "" + ChkCostCenter.Checked + "");
                    cmd.Parameters.AddWithValue("@TallyAccountName", "" + txtTallyAccountName.Text + "");
                 

                    Result = cmd.ExecuteNonQuery();
                    con.Close();
                }
                if (Result == 1)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "redirect", "alert('Updated Successfully'); window.location.href='frmGeneralMaster.aspx?MODE=" + (string)Request.QueryString["MODE"] + "';", true);
                }
            }
            catch (SqlException ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('DataBase Error: " + ex.Message + "');", true);
            }
        }
        public void InsertAccountDetails()
        {
            string CreatedBy = (string)Session["USER-NAME"];
            string CreatedDate = DateTime.Now.ToString();

            StringBuilder Query = new StringBuilder();
            string Message = string.Empty;
            DataSet ds = new DataSet();
            try
            {
                using (SqlConnection con = new SqlConnection(strconn))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("INSERT INTO [M_AccountDetails] (AccountCode, AccountName, AddressCode,BranchId, BranchName, Address1, Address2,Address3, City, State, Pincode, PhoneNo, Mobile, EmailID, Website, PANNo, TINNo, CSTNo, VATNo, STaxNo,ContactPerson,Country, Countrycode, LST, IncomeTaxNo,IECode,Acc_group,ADCode,CreatedBy,CreatedDate,OpeningBalance,DRCR,RefName,BillWiseOn,TallyAccountName) values(@AccountCode, @AccountName, @AddressCode,@BranchId,@BranchName,@Address1,@Address2,@Address3,@City,@State,@Pincode,@PhoneNo,@Mobile,@EmailID,@Website,@PANNo,@TINNo,@CSTNo,@VATNo,@STaxNo,@ContactPerson,@Country,@Countrycode,@LST,@IncomeTaxNo,@IECode,@Acc_group,@ADCode,@CreatedBy,@CreatedDate,@OpeningBalance,@DRCR,@RefName,@BillWiseOn,@TallyAccountName)", con);

                    cmd.Parameters.AddWithValue("@AccountCode", "" + txtAcountCode.Text + "");
                    cmd.Parameters.AddWithValue("@AccountName", "" + txtName.Text + "");
                    cmd.Parameters.AddWithValue("@AddressCode", "" + txtBranchId.Text + "");
                    //cmd.Parameters.AddWithValue("@AccountType", "" + Request.QueryString["MODE"] + "");
                    cmd.Parameters.AddWithValue("@Acc_group", "" + ddlAccountGroup.SelectedItem.Text + "");
                    cmd.Parameters.AddWithValue("@BranchId", "" + txtBranchId.Text + "");
                    cmd.Parameters.AddWithValue("@BranchName", "" + txtBranchName.Text + "");
                    cmd.Parameters.AddWithValue("@Address1", "" + txtAddress1.Text + "");
                    cmd.Parameters.AddWithValue("@Address2", "" + txtAddress2.Text + "");
                    cmd.Parameters.AddWithValue("@Address3", "" + txtAddress3.Text + "");
                    cmd.Parameters.AddWithValue("@City", "" + txtCity.Text + "");
                    cmd.Parameters.AddWithValue("@State", "" + txtState.Text + "");
                    cmd.Parameters.AddWithValue("@Pincode", "" + txtPinCode.Text + "");
                    cmd.Parameters.AddWithValue("@PhoneNo", "" + txtPhoneNo.Text + "");
                    cmd.Parameters.AddWithValue("@Mobile", "" + txtMobileNo.Text + "");
                    cmd.Parameters.AddWithValue("@EmailID", "" + txtEmailId.Text + "");
                    cmd.Parameters.AddWithValue("@Website", "" + txtWebsite.Text + "");
                    cmd.Parameters.AddWithValue("@PANNo", "" + txtPANNo.Text + "");
                    cmd.Parameters.AddWithValue("@TINNo", "" + txtTINno.Text + "");
                    cmd.Parameters.AddWithValue("@CSTNo", "" + txtCSTNo.Text + "");
                    cmd.Parameters.AddWithValue("@VATNo", "" + txtTINno.Text + "");
                    cmd.Parameters.AddWithValue("@STaxNo", "" + txtSTaxNo.Text + "");
                    cmd.Parameters.AddWithValue("@ContactPerson", "" + txtContactPerson.Text + "");
                    cmd.Parameters.AddWithValue("@Country", "" + ddlCountry.SelectedItem.Text + "");
                    cmd.Parameters.AddWithValue("@Countrycode", "" + ddlCountry.SelectedValue + "");
                    cmd.Parameters.AddWithValue("@LST", "" + txtTINno.Text + "");
                    cmd.Parameters.AddWithValue("@IncomeTaxNo", "" + txtIncomeTaxNo.Text + "");
                    cmd.Parameters.AddWithValue("@IECode", "" + txtIECode.Text + "");
                    cmd.Parameters.AddWithValue("@ADCode", "" + txtADCode.Text + "");
                    cmd.Parameters.AddWithValue("@CreatedBy", "" + CreatedBy + "");
                    cmd.Parameters.AddWithValue("@CreatedDate", "" + CreatedDate + "");
                    if (txtOpeninBalance.Text == "")
                    {
                        txtOpeninBalance.Text = "0.00";
                    }
                    cmd.Parameters.AddWithValue("@OpeningBalance", "" + Convert.ToDouble(txtOpeninBalance.Text) + "");
                    cmd.Parameters.AddWithValue("@DRCR", "" + ddlCRDR.SelectedItem.Text + "");
                    cmd.Parameters.AddWithValue("@RefName", "" + txtRefName.Text + "");
                    cmd.Parameters.AddWithValue("@BillWiseOn", "" + ChkCostCenter.Checked + "");
                    cmd.Parameters.AddWithValue("@TallyAccountName", "" + txtTallyAccountName.Text + "");
                    Result = cmd.ExecuteNonQuery();
                }
                if (Result == 1)
                {
                    btnBranchAdd.Text = "Add Branch";
                    BranchClear();
                    GridLoad();
                    BranchGridLoad();
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Branch Added Successfully'); ", true);
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('DataBase Error: " + ex.Message + "');", true);
            }
        }
        //public void InsertAccountDetails()
        //{
        //    string CreatedBy = (string)Session["USER-NAME"];
        //    string CreatedDate = DateTime.Now.ToString();

        //    StringBuilder Query = new StringBuilder();
        //    string Message = string.Empty;
        //    DataSet ds = new DataSet();
        //    try
        //    {
        //        using (SqlConnection con = new SqlConnection(strconn))
        //        { 
        //        con.Open();
        //        SqlCommand cmd = new SqlCommand("INSERT INTO [M_AccountDetails] (AccountCode, AccountName, AddressCode,BranchId, BranchName, Address1, Address2,Address3, City, State, Pincode, PhoneNo, Mobile, EmailID, Website, PANNo, TINNo, CSTNo, VATNo, STaxNo,ContactPerson,Country, Countrycode, LST, IncomeTaxNo,IECode,Acc_group,ADCode,CreatedBy,CreatedDate) values(@AccountCode, @AccountName, @AddressCode,@BranchId,@BranchName,@Address1,@Address2,@Address3,@City,@State,@Pincode,@PhoneNo,@Mobile,@EmailID,@Website,@PANNo,@TINNo,@CSTNo,@VATNo,@STaxNo,@ContactPerson,@Country,@Countrycode,@LST,@IncomeTaxNo,@IECode,@Acc_group,@ADCode,@CreatedBy,@CreatedDate)", con);

        //        cmd.Parameters.AddWithValue("@AccountCode", "" + txtAcountCode.Text + "");
        //        cmd.Parameters.AddWithValue("@AccountName", "" + txtName.Text + "");
        //        cmd.Parameters.AddWithValue("@AddressCode", "" + txtBranchId.Text + "");
        //        cmd.Parameters.AddWithValue("@AccountType", "" + Request.QueryString["MODE"] + "");
        //        cmd.Parameters.AddWithValue("@Acc_group", "" + ddlAccountGroup.SelectedValue + "");
        //        cmd.Parameters.AddWithValue("@BranchId", "" + txtBranchId.Text + "");
        //        cmd.Parameters.AddWithValue("@BranchName", "" + txtBranchName.Text + "");
        //        cmd.Parameters.AddWithValue("@Address1", "" + txtAddress1.Text + "");
        //        cmd.Parameters.AddWithValue("@Address2", "" + txtAddress2.Text + "");
        //        cmd.Parameters.AddWithValue("@Address3", "" + txtAddress3.Text + "");
        //        cmd.Parameters.AddWithValue("@City", "" + txtCity.Text + "");
        //        cmd.Parameters.AddWithValue("@State", "" + txtState.Text + "");
        //        cmd.Parameters.AddWithValue("@Pincode", "" + txtPinCode.Text + "");
        //        cmd.Parameters.AddWithValue("@PhoneNo", "" + txtPhoneNo.Text + "");
        //        cmd.Parameters.AddWithValue("@Mobile", "" + txtMobileNo.Text + "");
        //        cmd.Parameters.AddWithValue("@EmailID", "" + txtEmailId.Text + "");
        //        cmd.Parameters.AddWithValue("@Website", "" + txtWebsite.Text + "");
        //        cmd.Parameters.AddWithValue("@PANNo", "" + txtPANNo.Text + "");
        //        cmd.Parameters.AddWithValue("@TINNo", "" + txtTINno.Text + "");
        //        cmd.Parameters.AddWithValue("@CSTNo", "" + txtCSTNo.Text + "");
        //        cmd.Parameters.AddWithValue("@VATNo", "" + txtTINno.Text + "");
        //        cmd.Parameters.AddWithValue("@STaxNo", "" + txtSTaxNo.Text + "");
        //        cmd.Parameters.AddWithValue("@ContactPerson", "" + txtContactPerson.Text + "");
        //        cmd.Parameters.AddWithValue("@Country", "" + ddlCountry.SelectedItem.Text + "");
        //        cmd.Parameters.AddWithValue("@Countrycode", "" + ddlCountry.SelectedValue + "");
        //        cmd.Parameters.AddWithValue("@LST", "" + txtTINno.Text + "");
        //        cmd.Parameters.AddWithValue("@IncomeTaxNo", "" + txtIncomeTaxNo.Text + "");
        //        cmd.Parameters.AddWithValue("@IECode", "" + txtIECode.Text + "");
        //        cmd.Parameters.AddWithValue("@ADCode", "" + txtADCode.Text + "");
        //        cmd.Parameters.AddWithValue("@CreatedBy", "" + CreatedBy + "");
        //        cmd.Parameters.AddWithValue("@CreatedDate", "" + CreatedDate + "");
        //        Result = cmd.ExecuteNonQuery();
        //        }
        //        if (Result == 1)
        //        {
        //            btnBranchAdd.Text = "Add Branch";
        //            BranchClear();
        //            GridLoad();
        //            BranchGridLoad();
        //            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Branch Added Successfully'); ", true);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('DataBase Error: " + ex.Message + "');", true);
        //    }
        //}
        public void GridLoad()
        {
            string quer = string.Empty;
            DataSet ds = new DataSet();
            quer = "select TransId,AccountName,AccountType,BranchName,Country,City,State from M_AccountMaster where [AccountType]='" + Request.QueryString["MODE"] + "' order by TransId desc";
            ds = objCommonDL.GetDataSet(quer);
            if (ds.Tables["Table"].Rows.Count != 0)
            {
                gvDetails.DataSource = ds;
                gvDetails.DataBind();
            }
            else
            {
                gvDetails.DataSource = null;
                gvDetails.DataBind();
            }
        }
        public void GridLoadAll()
        {
            string quer = string.Empty;
            DataSet ds = new DataSet();
            quer = "select Top(10) TransId,AccountName,AccountType,BranchName,Country,City,State from M_AccountMaster  order by TransId desc";
            ds = objCommonDL.GetDataSet(quer);
            if (ds.Tables["Table"].Rows.Count != 0)
            {
                gvDetails.DataSource = ds;
                gvDetails.DataBind();
            }
            else
            {
                gvDetails.DataSource = null;
                gvDetails.DataBind();
            }
        }

        protected void gvDetails_SelectedIndexChanged1(object sender, EventArgs e)
        {
            Session["BranchMode"] = "";
            Session["BranchMode"] = "Save";
            btnSave.Visible = false;
            btnUpdate.Visible = true;
            hdnCommonMaster.Value = gvDetails.SelectedRow.Cells[1].Text;
            DataSet ds = new DataSet();
            string quer = "select * from M_AccountMaster where [TransId] ='" + hdnCommonMaster.Value + "'";
            ds = objCommonDL.GetDataSet(quer);
            if (ds.Tables["Table"].Rows.Count != 0)
            {
                btnBranchAdd.Text = "Add Branch";
                DataRowView dr = ds.Tables["Table"].DefaultView[0];
                hdnCommonMaster.Value = dr["TransId"].ToString();
                txtAcountCode.Text = dr["AccountCode"].ToString();
                //lblMaster.Text = dr["AccountType"].ToString() + " Master";
                txtName.Text = dr["AccountName"].ToString();
                txtShortName.Text = dr["ShortName"].ToString();
                txtPrefix.Text = dr["Prefix"].ToString();
                txtBranchId.Text = dr["BranchId"].ToString();
                txtBranchName.Text = dr["BranchName"].ToString();
                txtAddress1.Text = dr["Address1"].ToString();
                txtAddress2.Text = dr["Address2"].ToString();
                txtAddress3.Text = dr["Address3"].ToString();
                txtCity.Text = dr["City"].ToString();
                txtState.Text = dr["State"].ToString();
                txtPinCode.Text = dr["Pincode"].ToString();
                txtPhoneNo.Text = dr["PhoneNo"].ToString();
                txtMobileNo.Text = dr["Mobile"].ToString();
                txtEmailId.Text = dr["EmailID"].ToString();
                txtWebsite.Text = dr["Website"].ToString();
                txtPANNo.Text = dr["PANNo"].ToString();
                txtTINno.Text = dr["TINNo"].ToString();
                txtCSTNo.Text = dr["CSTNo"].ToString();
                txtSTaxNo.Text = dr["STaxNo"].ToString();
                txtContactPerson.Text = dr["ContactPerson"].ToString();
                txtADCode.Text = dr["ADCode"].ToString();
                if ((dr["InvSeqNo"]) != null)
                {
                    chkinvseqno.Checked = true;
                    txtinvseqno.Text = dr["InvSeqNo"].ToString();
                }
                if ((dr["CountryCode"] != DBNull.Value) && (dr["CountryCode"] != ""))
                {
                    ddlCountry.SelectedValue = dr["CountryCode"].ToString();
                }
                txtCountryCode.Text = dr["Countrycode"].ToString();
                txtIncomeTaxNo.Text = dr["IncomeTaxNo"].ToString();
                txtIECode.Text = dr["IECode"].ToString();
                if ((dr["PaymentPeriod"] != DBNull.Value) && (dr["PaymentPeriod"] != ""))
                {
                    ddlPaymentPeriod.SelectedItem.Text = dr["PaymentPeriod"].ToString();
                }
                txtCreditLimit.Text = dr["CreditLimit"].ToString();
                if ((dr["Currency"] != DBNull.Value) && (dr["Currency"] != ""))
                {
                    ddlCurrency.SelectedValue = dr["Currency"].ToString();
                }
                if ((dr["Acc_group"] != DBNull.Value) && (dr["Acc_group"] != ""))
                {
                    ddlAccountGroup.SelectedValue = dr["Acc_group"].ToString();
                }
                MainBranchEnable();
            }
            BranchGridLoad();
            btnBranchAdd.Visible = true;
        }

        private void BranchGridLoad()
        {
            DataSet ds1 = new DataSet();
            string branchquer = "select * from [M_AccountDetails] where [AccountCode]='" + txtAcountCode.Text + "' ";
            ds1 = objCommonDL.GetDataSet(branchquer);
            if (ds1.Tables["Table"].Rows.Count != 0)
            {
                gvBranchDetails.DataSource = ds1;
                gvBranchDetails.DataBind();
            }
            else
            {
                gvBranchDetails.DataSource = null;
                gvBranchDetails.DataBind();
            }
            int branchid = ds1.Tables["Table"].Rows.Count;
            txtBranchId.Text = branchid.ToString();
        }

        protected void btnNew_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/frmGeneralMaster.aspx?MODE=" + (string)Request.QueryString["MODE"] + "");
        }

        protected void btnDiscard_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/HomePage.aspx");
        }

        private void Clear()
        {
            txtName.Text = "";
            txtShortName.Text = "";
            txtPrefix.Text = "";
            txtBranchId.Text = "";
            txtBranchName.Text = "";
            txtAddress1.Text = "";
            txtAddress2.Text = "";
            txtAddress3.Text = "";
            txtCity.Text = "";
            txtState.Text = "";
            txtPinCode.Text = "";
            txtPhoneNo.Text = "";
            txtMobileNo.Text = "";
            txtEmailId.Text = "";
            txtWebsite.Text = "";
            txtPANNo.Text = "";
            txtTINno.Text = "";
            txtCSTNo.Text = "";
            txtSTaxNo.Text = "";
            txtContactPerson.Text = "";
            ddlCountry.SelectedIndex = 0;
            txtCountryCode.Text = "";
            txtIncomeTaxNo.Text = "";
            txtIECode.Text = "";
            txtCreditLimit.Text = "";
            txtADCode.Text = "";
            ddlPaymentPeriod.SelectedIndex = 0;
            ddlCurrency.SelectedIndex = 0;
        }

        protected void btnBranchAdd_Click(object sender, EventArgs e)
        {
            if ((string)Session["BranchMode"] == "Save" || (string)Session["BranchMode"] == null)
            {
                InsertAccountDetails();
            }
            else if ((string)Session["BranchMode"] == "Update")
            {
                UpdateBranchDetails();
            }
        }
        private void UpdateBranchDetails()
        {
            string ModifiedBy = (string)Session["USER-NAME"];
            string ModifiedDate = DateTime.Now.ToString();
            StringBuilder Query = new StringBuilder();
            string Message = string.Empty;
            DataSet ds = new DataSet();
            try
            {
                using (SqlConnection con = new SqlConnection(strconn))
                {
                    Query.Append("UPDATE [M_AccountDetails] SET ");
                    Query.Append("AccountCode=@AccountCode,AccountName=@AccountName,AddressCode=@AddressCode,BranchId=@BranchId, BranchName=@BranchName,");
                    Query.Append("Address1=@Address1, Address2=@Address2,Address3=@Address3, City=@City, State=@State,");
                    Query.Append("Pincode=@Pincode,PhoneNo=@PhoneNo,Mobile=@Mobile,EmailID=@EmailID,Website=@Website,PANNo=@PANNo,TINNo=@TINNo,CSTNo=@CSTNo,");
                    Query.Append("STaxNo=@STaxNo,ContactPerson=@ContactPerson,Country=@Country,Countrycode=@Countrycode,IncomeTaxNo=@IncomeTaxNo,IECode=@IECode,ADCode=@ADCode,");
                    Query.Append("ModifiedBy=@ModifiedBy,ModifiedDate=@ModifiedDate,OpeningBalance=@OpeningBalance,DRCR=@DRCR,RefName=@RefName,BillWiseOn=@BillWiseOn,TallyAccountName=@TallyAccountName,Acc_group=@Acc_group WHERE TransId=@TransId");
                    con.Open();
                    SqlCommand cmd = new SqlCommand(Query.ToString(), con);
                    cmd.Parameters.AddWithValue("@AccountCode", "" + txtAcountCode.Text + "");
                    cmd.Parameters.AddWithValue("@AccountName", "" + txtName.Text + "");
                    cmd.Parameters.AddWithValue("@AddressCode", "" + txtBranchId.Text + "");

                    cmd.Parameters.AddWithValue("@BranchId", txtBranchId.Text);
                    cmd.Parameters.AddWithValue("@BranchName", txtBranchName.Text);
                    cmd.Parameters.AddWithValue("@Address1", txtAddress1.Text);
                    cmd.Parameters.AddWithValue("@Address2", txtAddress2.Text);
                    cmd.Parameters.AddWithValue("@Address3", txtAddress3.Text);
                    cmd.Parameters.AddWithValue("@City", txtCity.Text);
                    cmd.Parameters.AddWithValue("@State", txtState.Text);
                    cmd.Parameters.AddWithValue("@Pincode", txtPinCode.Text);
                    cmd.Parameters.AddWithValue("@PhoneNo", txtPhoneNo.Text);
                    cmd.Parameters.AddWithValue("@Mobile", txtMobileNo.Text);
                    cmd.Parameters.AddWithValue("@EmailID", txtEmailId.Text);
                    cmd.Parameters.AddWithValue("@Website", txtWebsite.Text);
                    cmd.Parameters.AddWithValue("@PANNo", txtPANNo.Text);
                    cmd.Parameters.AddWithValue("@TINNo", txtTINno.Text);
                    cmd.Parameters.AddWithValue("@CSTNo", txtCSTNo.Text);
                    cmd.Parameters.AddWithValue("@STaxNo", txtSTaxNo.Text);
                    cmd.Parameters.AddWithValue("@ContactPerson", txtContactPerson.Text);
                    cmd.Parameters.AddWithValue("@Country", ddlCountry.SelectedItem.Text);
                    cmd.Parameters.AddWithValue("@Countrycode", ddlCountry.SelectedValue);
                    cmd.Parameters.AddWithValue("@IncomeTaxNo", txtIncomeTaxNo.Text);
                    cmd.Parameters.AddWithValue("@IECode", txtIECode.Text);
                    cmd.Parameters.AddWithValue("@ADCode", "" + txtADCode.Text + "");
                    cmd.Parameters.AddWithValue("@ModifiedBy", ModifiedBy);
                    cmd.Parameters.AddWithValue("@ModifiedDate", ModifiedDate);
                    if (txtOpeninBalance.Text == "")
                    {
                        txtOpeninBalance.Text = "0.00";
                    }
                    cmd.Parameters.AddWithValue("@OpeningBalance", "" + Convert.ToDouble(txtOpeninBalance.Text) + "");
                    cmd.Parameters.AddWithValue("@DRCR", "" + ddlCRDR.SelectedItem.Text + "");
                    cmd.Parameters.AddWithValue("@RefName", "" + txtRefName.Text + "");
                    cmd.Parameters.AddWithValue("@BillWiseOn", "" + ChkCostCenter.Checked + "");
                    cmd.Parameters.AddWithValue("@TallyAccountName", "" + txtTallyAccountName.Text + "");
                    cmd.Parameters.AddWithValue("@Acc_group", "" + ddlAccountGroup.SelectedItem.Text  + "");
                    cmd.Parameters.AddWithValue("@TransId", hdnBranchMaster.Value);

                    Result = cmd.ExecuteNonQuery();
                    con.Close();
                }
                if (Result == 1)
                {
                    Session["BranchMode"] = "";
                    Session["BranchMode"] = "Save";
                    btnBranchAdd.Text = "Add Branch";
                    GridLoad();
                    BranchGridLoad();
                    BranchClear();
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Updated Successfully'); ", true);
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('DataBase Error: " + ex.Message + "');", true);
            }
        }
        //private void UpdateBranchDetails()
        //{
        //    string ModifiedBy = (string)Session["USER-NAME"];
        //    string ModifiedDate = DateTime.Now.ToString();

        //    StringBuilder Query = new StringBuilder();
        //    string Message = string.Empty;
        //    DataSet ds = new DataSet();
        //    try
        //    {
        //        using (SqlConnection con = new SqlConnection(strconn))
        //        {
        //            Query.Append("UPDATE [M_AccountDetails] SET ");
        //            Query.Append("BranchId=@BranchId, BranchName=@BranchName, Address1=@Address1, Address2=@Address2,Address3=@Address3, City=@City, State=@State,");
        //            Query.Append("Pincode=@Pincode,PhoneNo=@PhoneNo,Mobile=@Mobile,EmailID=@EmailID,Website=@Website,PANNo=@PANNo,TINNo=@TINNo,CSTNo=@CSTNo,");
        //            Query.Append("STaxNo=@STaxNo,ContactPerson=@ContactPerson,Country=@Country,Countrycode=@Countrycode,IncomeTaxNo=@IncomeTaxNo,IECode=@IECode,ADCode=@ADCode,");
        //            Query.Append("ModifiedBy=@ModifiedBy,ModifiedDate=@ModifiedDate WHERE TransId=@TransId");
        //            con.Open();
        //            SqlCommand cmd = new SqlCommand(Query.ToString(), con);
        //            cmd.Parameters.AddWithValue("@BranchId", txtBranchId.Text);
        //            cmd.Parameters.AddWithValue("@BranchName", txtBranchName.Text);
        //            cmd.Parameters.AddWithValue("@Address1", txtAddress1.Text);
        //            cmd.Parameters.AddWithValue("@Address2", txtAddress2.Text);
        //            cmd.Parameters.AddWithValue("@Address3", txtAddress3.Text);
        //            cmd.Parameters.AddWithValue("@City", txtCity.Text);
        //            cmd.Parameters.AddWithValue("@State", txtState.Text);
        //            cmd.Parameters.AddWithValue("@Pincode", txtPinCode.Text);
        //            cmd.Parameters.AddWithValue("@PhoneNo", txtPhoneNo.Text);
        //            cmd.Parameters.AddWithValue("@Mobile", txtMobileNo.Text);
        //            cmd.Parameters.AddWithValue("@EmailID", txtEmailId.Text);
        //            cmd.Parameters.AddWithValue("@Website", txtWebsite.Text);
        //            cmd.Parameters.AddWithValue("@PANNo", txtPANNo.Text);
        //            cmd.Parameters.AddWithValue("@TINNo", txtTINno.Text);
        //            cmd.Parameters.AddWithValue("@CSTNo", txtCSTNo.Text);
        //            cmd.Parameters.AddWithValue("@STaxNo", txtSTaxNo.Text);
        //            cmd.Parameters.AddWithValue("@ContactPerson", txtContactPerson.Text);
        //            cmd.Parameters.AddWithValue("@Country", ddlCountry.SelectedItem.Text);
        //            cmd.Parameters.AddWithValue("@Countrycode", ddlCountry.SelectedValue);
        //            cmd.Parameters.AddWithValue("@IncomeTaxNo", txtIncomeTaxNo.Text);
        //            cmd.Parameters.AddWithValue("@IECode", txtIECode.Text);
        //            cmd.Parameters.AddWithValue("@ADCode", "" + txtADCode.Text + "");
        //            cmd.Parameters.AddWithValue("@ModifiedBy", ModifiedBy);
        //            cmd.Parameters.AddWithValue("@ModifiedDate", ModifiedDate);
        //            cmd.Parameters.AddWithValue("@TransId", hdnBranchMaster.Value);

        //            Result = cmd.ExecuteNonQuery();
        //            con.Close();
        //        }
        //        if (Result == 1)
        //        {
        //            Session["BranchMode"] = "";
        //            Session["BranchMode"] = "Save";
        //            btnBranchAdd.Text = "Add Branch";
        //            GridLoad();
        //            BranchGridLoad();
        //            BranchClear();
        //            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Updated Successfully'); ", true);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('DataBase Error: " + ex.Message + "');", true);
        //    }
        //}

        protected void gvBranchDetails_SelectedIndexChanged(object sender, EventArgs e)
        {
            Session["BranchMode"] = "";
            Session["BranchMode"] = "Update";
            MainBranchDisable();
            btnSave.Visible = false;
            btnUpdate.Visible = false;
            hdnBranchMaster.Value = gvBranchDetails.SelectedRow.Cells[1].Text;
            DataSet ds = new DataSet();
            string quer = "select * from M_AccountDetails where [TransId] ='" + hdnBranchMaster.Value + "'";
            ds = objCommonDL.GetDataSet(quer);
            if (ds.Tables["Table"].Rows.Count != 0)
            {
                DataRowView dr = ds.Tables["Table"].DefaultView[0];
                hdnBranchMaster.Value = dr["TransId"].ToString();
                txtBranchId.Text = dr["BranchId"].ToString();
                txtBranchName.Text = dr["BranchName"].ToString();
                txtAddress1.Text = dr["Address1"].ToString();
                txtAddress2.Text = dr["Address2"].ToString();
                txtAddress3.Text = dr["Address3"].ToString();
                txtCity.Text = dr["City"].ToString();
                txtState.Text = dr["State"].ToString();
                txtPinCode.Text = dr["Pincode"].ToString();
                txtPhoneNo.Text = dr["PhoneNo"].ToString();
                txtMobileNo.Text = dr["Mobile"].ToString();
                txtEmailId.Text = dr["EmailID"].ToString();
                txtWebsite.Text = dr["Website"].ToString();
                txtPANNo.Text = dr["PANNo"].ToString();
                txtTINno.Text = dr["TINNo"].ToString();
                txtCSTNo.Text = dr["CSTNo"].ToString();
                txtSTaxNo.Text = dr["STaxNo"].ToString();
                txtCountryCode.Text = dr["Countrycode"].ToString();
                txtIncomeTaxNo.Text = dr["IncomeTaxNo"].ToString();
                txtIECode.Text = dr["IECode"].ToString();
                txtADCode.Text = dr["ADCode"].ToString();

                txtOpeninBalance.Text = dr["OpeningBalance"].ToString();
                ddlCRDR.SelectedValue = dr["DRCR"].ToString();
                txtTallyAccountName.Text = dr["TallyAccountName"].ToString();
                txtRefName.Text = dr["RefName"].ToString();
                ChkCostCenter.Checked = Convert.ToBoolean(dr["CostCenter"].ToString());


                if ((dr["CountryCode"] != DBNull.Value) && (dr["CountryCode"] != ""))
                {
                    ddlCountry.SelectedValue = dr["CountryCode"].ToString();
                }
                btnBranchAdd.Text = "Update Branch";
            }
        }

        private void MainBranchDisable()
        {
            txtAcountCode.Enabled = false;
            ddlPaymentPeriod.Enabled = false;
            txtName.Enabled = false;
            txtCreditLimit.Enabled = false;
            txtShortName.Enabled = false;
            ddlCurrency.Enabled = false;
            txtPrefix.Enabled = false;
            txtContactPerson.Enabled = false;
        }

        private void MainBranchEnable()
        {
            txtAcountCode.Enabled = false;
            ddlPaymentPeriod.Enabled = true;
            txtName.Enabled = true;
            txtCreditLimit.Enabled = true;
            txtShortName.Enabled = true;
            ddlCurrency.Enabled = true;
            txtPrefix.Enabled = true;
            txtContactPerson.Enabled = true;
        }

        private void BranchClear()
        {
            txtBranchName.Text = "";
            txtAddress1.Text = "";
            txtAddress2.Text = "";
            txtAddress3.Text = "";
            txtCity.Text = "";
            txtState.Text = "";
            txtPinCode.Text = "";
            txtPhoneNo.Text = "";
            txtMobileNo.Text = "";
            txtEmailId.Text = "";
            txtWebsite.Text = "";
            txtPANNo.Text = "";
            txtTINno.Text = "";
            txtCSTNo.Text = "";
            txtSTaxNo.Text = "";
            ddlCountry.SelectedIndex = 0;
            txtCountryCode.Text = "";
            txtIncomeTaxNo.Text = "";
            txtIECode.Text = "";
            txtADCode.Text = "";
            txtBranchId.Text = Convert.ToString(gvBranchDetails.Rows.Count + 1);
        }

        protected void ddlCountry_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtCountryCode.Text = ddlCountry.SelectedValue;
            txtPANNo.Focus();
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            if (Request.QueryString["MODE"] == "Accounts")
            {
                if (txtSearch.Text != "")
                {
                    string quer = string.Empty;
                    DataSet ds = new DataSet();
                    quer = "select TransId,AccountName,Acc_group,BranchName,Country,City,State from M_AccountMaster where AccountName like '%" + txtSearch.Text + "%' Or Acc_group like '%" + txtSearch.Text + "%' order by TransId desc";
                    ds = objCommonDL.GetDataSet(quer);
                    if (ds.Tables["Table"].Rows.Count != 0)
                    {
                        gvDetails.DataSource = ds;
                        gvDetails.DataBind();
                    }
                    else
                    {
                        gvDetails.DataSource = null;
                        gvDetails.DataBind();
                    }
                }
            }
            else
            {
                string quer = string.Empty;
                DataSet ds = new DataSet();
                quer = "select TransId,AccountName,AccountType,BranchName,Country,City,State from M_AccountMaster where [AccountType]='" + Request.QueryString["MODE"] + "' And AccountName like '%" + txtSearch.Text + "%' order by TransId desc";
                ds = objCommonDL.GetDataSet(quer);
                if (ds.Tables["Table"].Rows.Count != 0)
                {
                    gvDetails.DataSource = ds;
                    gvDetails.DataBind();
                }
                else
                {
                    gvDetails.DataSource = null;
                    gvDetails.DataBind();
                }
            
            }
        }

       

      
       

    }
}