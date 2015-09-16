using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data;
using System.Text;
using System.Data.SqlClient;

namespace ImpexCube.Accounts
{
    public partial class frmGeneralMaster : System.Web.UI.Page
    {
        string strconn = (string)ConfigurationManager.AppSettings["ConnectionDashboard"];
        int Result = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack == false)
            {
                Label pagename;
                pagename = (Label)Master.FindControl("lblName");
                pagename.Text = "Accounts Master"; 
                GridLoadAll();
                filldropdown();
                hdnCommonMaster.Value = string.Empty;
                panelContent.Visible = false;
            }

            //txtSearch.Attributes.Add("onkeyup", "return filter();");
            //string SearchText = Request.QueryString["SearchText"];

            //if (SearchText != null)
            //{
            //    SqlConnection con = new SqlConnection(strconn);
            //    con.Open();               
            //    SqlCommand cmd = new SqlCommand("IC_FILTER_ACCOUNTS",con);
            //    cmd.Connection = con;
            //    cmd.CommandType = CommandType.StoredProcedure;
            //    cmd.CommandText = "IC_FILTER_ACCOUNTS";
            //    cmd.Parameters.Add("@SearchText", SearchText);

            //    DataSet ds = new DataSet();
            //    SqlDataAdapter da = new SqlDataAdapter(cmd);

            //    da.SelectCommand = cmd;
            //    da.Fill(ds, "Accounts");

            //    if (ds.Tables["Accounts"].Rows.Count != 0)
            //    {
            //        gvDetails.DataSource = ds;
            //        gvDetails.DataBind();
            //    }
            //    else
            //    {
            //        gvDetails.DataSource = null;
            //        gvDetails.DataBind();
            //    }
            //}

        }
        protected void Page_PreInit(object sender, EventArgs e)
        {
            if (Request.QueryString["MODE"] == "Accounts")
            {
                this.Page.MasterPageFile = "~/Accounts/MainMaster.Master";
            }
        }


        public void filldropdown()
        {
            DataSet dsCountry = new DataSet();
            string quer1 = "select CountryName,CountryCode from M_Country";
            dsCountry =GetDataSet(quer1);
            ddlCountry.DataSource = dsCountry;
            ddlCountry.DataTextField = "CountryName";
            ddlCountry.DataValueField = "CountryCode";
            ddlCountry.DataBind();
            ddlCountry.Items.Insert(0, new ListItem("~Select~", "0"));

            DataSet dsCurrency = new DataSet();
            string quer2 = "select CurrencyShortName from [M_Currency]";
            dsCurrency = GetDataSet(quer2);
            ddlCurrency.DataSource = dsCurrency;
            ddlCurrency.DataTextField = "CurrencyShortName";
            ddlCurrency.DataValueField = "CurrencyShortName";
            ddlCurrency.DataBind();
            ddlCurrency.Items.Insert(0, new ListItem("~Select~", "0"));

            DataSet dsAccountsGroup = new DataSet();
            string quer3 = "select GroupCode,GroupName from [M_AccountsGroup]";
            dsAccountsGroup = GetDataSet(quer3);
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
               // ScriptManager.RegisterStartupScript(this, this.GetType(), "redirect", "alert('Saved Successfully'); window.location.href='frmGeneralMaster.aspx?MODE=" + (string)Request.QueryString["MODE"] + "';", true);
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
                    Query.Append("Countrycode, LST, IncomeTaxNo,IECode,PaymentPeriod,CreditLimit,Currency,Acc_group,ADCode,CreatedBy,CreatedDate,KAM1,KAM2,KAM1Name,KAM2Name,OpeningBalance,DRCR,RefName,BillWiseOn,TallyAccountName)");
                    Query.Append("values(@AccountCode, @AccountName, @ShortName,@AccountType, @Prefix,@BranchId,@BranchName,@Address1,@Address2,@Address3,@City,@State,@Pincode,@PhoneNo,@Mobile,@EmailID,@Website,@PANNo,@TINNo,@CSTNo,@VATNo,");
                    Query.Append("@STaxNo,@ContactPerson,@Country,@Countrycode,@LST,@IncomeTaxNo,@IECode,@PaymentPeriod,@CreditLimit,@Currency,@Acc_group,@ADCode,@CreatedBy,@CreatedDate,@KAM1,@KAM2,@KAM1Name,@KAM2Name,@OpeningBalance,@DRCR,@RefName,0,@TallyAccountName)");

                    Query.Append("");
                    SqlCommand cmd = new SqlCommand(Query.ToString(), con);
                    cmd.Parameters.AddWithValue("@AccountCode", "Test");
                    cmd.Parameters.AddWithValue("@AccountName",  txtName.Text );
                    cmd.Parameters.AddWithValue("@ShortName", txtShortName.Text );
                    cmd.Parameters.AddWithValue("@AccountType",  "Accounts" );
                    cmd.Parameters.AddWithValue("@Prefix", "");
                    cmd.Parameters.AddWithValue("@BranchId", txtBranchSrNo.Text);
                    cmd.Parameters.AddWithValue("@BranchName", txtName.Text);
                    cmd.Parameters.AddWithValue("@Address1",  txtAddress1.Text );
                    cmd.Parameters.AddWithValue("@Address2",  txtAddress2.Text );
                    cmd.Parameters.AddWithValue("@Address3", txtAddress3.Text );
                    cmd.Parameters.AddWithValue("@City", txtCity.Text );
                    cmd.Parameters.AddWithValue("@State",  txtState.Text );
                    cmd.Parameters.AddWithValue("@Pincode",  txtPinCode.Text );
                    cmd.Parameters.AddWithValue("@PhoneNo",  txtPhoneNo.Text );
                    cmd.Parameters.AddWithValue("@Mobile", txtMobileNo.Text );
                    cmd.Parameters.AddWithValue("@EmailID", txtEmailId.Text );
                    cmd.Parameters.AddWithValue("@Website", txtWebsite.Text );
                    cmd.Parameters.AddWithValue("@PANNo",  txtPANNo.Text );
                    cmd.Parameters.AddWithValue("@TINNo",  txtTINno.Text );
                    cmd.Parameters.AddWithValue("@CSTNo",  txtCSTNo.Text );
                    cmd.Parameters.AddWithValue("@VATNo",  txtTINno.Text );
                    cmd.Parameters.AddWithValue("@STaxNo",  txtSTaxNo.Text );
                    cmd.Parameters.AddWithValue("@ContactPerson",  txtContactPerson.Text );
                    cmd.Parameters.AddWithValue("@Country",  ddlCountry.SelectedItem.Text );
                    cmd.Parameters.AddWithValue("@Countrycode", ddlCountry.SelectedValue );
                    cmd.Parameters.AddWithValue("@LST",  txtTINno.Text );
                    cmd.Parameters.AddWithValue("@IncomeTaxNo", txtIncomeTaxNo.Text );
                    cmd.Parameters.AddWithValue("@IECode", txtIECode.Text );
                    cmd.Parameters.AddWithValue("@PaymentPeriod",  ddlPaymentPeriod.SelectedValue );
                    cmd.Parameters.AddWithValue("@CreditLimit",  txtCreditLimit.Text);
                    cmd.Parameters.AddWithValue("@Currency",  ddlCurrency.SelectedValue );
                    cmd.Parameters.AddWithValue("@Acc_group",  ddlAccountGroup.SelectedValue);
                    cmd.Parameters.AddWithValue("@ADCode", txtADCode.Text );
                    cmd.Parameters.AddWithValue("@CreatedBy", CreatedBy);
                    cmd.Parameters.AddWithValue("@CreatedDate", CreatedDate);
                    cmd.Parameters.AddWithValue("@KAM1", "");
                    cmd.Parameters.AddWithValue("@KAM2", "");
                    cmd.Parameters.AddWithValue("@KAM1Name", "");
                    cmd.Parameters.AddWithValue("@KAM2Name", "");
                    cmd.Parameters.AddWithValue("@OpeningBalance", 0);
                    cmd.Parameters.AddWithValue("@DRCR", ddlCRDR.SelectedValue);
                    cmd.Parameters.AddWithValue("@RefName", txtRefName.Text);
                    cmd.Parameters.AddWithValue("@TallyAccountName",txtTallyAccountName.Text);
                    
                    Result = cmd.ExecuteNonQuery();
                    con.Close();
                }
                if (Result == 1)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Saved Successfully');", true);
                   //Message
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
                    Query.Append("PaymentPeriod=@PaymentPeriod,CreditLimit=@CreditLimit,Currency=@Currency,Acc_group=@Acc_group,ADCode=@ADCode,ModifiedBy=@ModifiedBy,ModifiedDate=@ModifiedDate WHERE TransId=@TransId");
                    con.Open();
                    SqlCommand cmd = new SqlCommand(Query.ToString(), con);

                    cmd.Parameters.AddWithValue("@AccountCode", "");
                    cmd.Parameters.AddWithValue("@AccountName", txtName.Text);
                    cmd.Parameters.AddWithValue("@ShortName", txtShortName.Text);
                    cmd.Parameters.AddWithValue("@AccountType", Request.QueryString["MODE"]);
                    cmd.Parameters.AddWithValue("@Prefix", "");
                    cmd.Parameters.AddWithValue("@BranchId", txtBranchSrNo.Text);
                    cmd.Parameters.AddWithValue("@BranchName", txtName.Text);
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
                    cmd.Parameters.AddWithValue("@Acc_group", "" + ddlAccountGroup.SelectedValue + "");
                    cmd.Parameters.AddWithValue("@ADCode", "" + txtADCode.Text + "");
                    cmd.Parameters.AddWithValue("@ModifiedBy", ModifiedBy);
                    cmd.Parameters.AddWithValue("@ModifiedDate", ModifiedDate);
                    cmd.Parameters.AddWithValue("@TransId", hdnCommonMaster.Value);

                    Result = cmd.ExecuteNonQuery();
                    con.Close();
                }
                if (Result == 1)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Updated Successfully');", true);
                   // ScriptManager.RegisterStartupScript(this, this.GetType(), "redirect", "alert('Updated Successfully'); window.location.href='frmGeneralMaster.aspx?MODE=" + (string)Request.QueryString["MODE"] + "';", true);
                }
            }
            catch (SqlException ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('DataBase Error: " + ex.Message + "');", true);
            }
        }
           
         public void GridLoadAll()
        {
            string quer = string.Empty;
            DataSet ds = new DataSet();
            quer = "select Top(10) TransId,AccountName from M_AccountMaster  order by TransId desc";
            ds = GetDataSet(quer);
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
            btnSave.Visible = false;
            btnUpdate.Visible = true;
            panelContent.Visible = true;
            hdnCommonMaster.Value = gvDetails.SelectedRow.Cells[1].Text;
            DataSet ds = new DataSet();
            string quer = "select * from M_AccountMaster where [TransId] ='" + hdnCommonMaster.Value + "'";
            ds = GetDataSet(quer);
            if (ds.Tables["Table"].Rows.Count != 0)
            {
              //  btnBranchAdd.Text = "Add Branch";
                DataRowView dr = ds.Tables["Table"].DefaultView[0];
                hdnCommonMaster.Value = dr["TransId"].ToString();
               // txtAcountCode.Text = dr["AccountCode"].ToString();
                //lblMaster.Text = dr["AccountType"].ToString() + " Master";
                txtName.Text = dr["AccountName"].ToString();
                txtShortName.Text = dr["ShortName"].ToString();
                //txtPrefix.Text = dr["Prefix"].ToString();
                txtBranchSrNo.Text = dr["BranchId"].ToString();
                //txtBranchName.Text = dr["BranchName"].ToString();
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
                //if ((dr["CountryCode"] != DBNull.Value) && (dr["CountryCode"] != ""))
                //{
                try
                {
                    ddlCountry.SelectedValue = dr["CountryCode"].ToString();
                }
                catch
                {
                }
               // }
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
            }
        }

     
        protected void btnNew_Click(object sender, EventArgs e)
        {
            Clear();
            btnSave.Visible = true;
            btnUpdate.Visible = false;
            panelContent.Visible = true;
            //Response.Redirect("~/Accounts/frmGeneralMaster.aspx");
            
        }

        protected void btnDiscard_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Accounts/MainMenu.aspx");
        }

        private void Clear()
        {
            txtName.Text = "";
            txtShortName.Text = "";
            ddlAccountGroup.SelectedIndex = 0;
            ddlCurrency.SelectedIndex = 0;
            ddlPaymentPeriod.SelectedIndex = 0;
            txtCreditLimit.Text = "";
            txtTallyAccountName.Text = "";
            txtMobileNo.Text = "";
            txtAddress1.Text = "";
            txtAddress2.Text = "";
            txtAddress3.Text = "";
            txtPhoneNo.Text = "";
            txtBranchSrNo.Text = "";
            txtEmailId.Text = "";
            txtWebsite.Text = "";
            txtCity.Text = "";
            txtADCode.Text = "";
            txtState.Text = "";
            txtTINno.Text = "";
            txtPinCode.Text = "";
            txtCSTNo.Text = "";
            ddlCountry.SelectedIndex = 0;
            txtPANNo.Text = "";
            txtCountryCode.Text = "";
            txtSTaxNo.Text = "";
            txtContactPerson.Text = "";
            txtIECode.Text = "";
            txtIncomeTaxNo.Text = "";
            txtOpeninBalance.Text = "";
            ddlCRDR.SelectedIndex=0;
            txtContactPerson.Text = "";
            txtRefName.Text = "";

            txtSearch.Text = "";
            
        }
        //private void BranchClear()
        //{
        //    txtAddress1.Text = "";
        //    txtAddress2.Text = "";
        //    txtAddress3.Text = "";
        //    txtCity.Text = "";
        //    txtState.Text = "";
        //    txtPinCode.Text = "";
        //    txtPhoneNo.Text = "";
        //    txtMobileNo.Text = "";
        //    txtEmailId.Text = "";
        //    txtWebsite.Text = "";
        //    txtPANNo.Text = "";
        //    txtTINno.Text = "";
        //    txtCSTNo.Text = "";
        //    txtSTaxNo.Text = "";
        //    ddlCountry.SelectedIndex = 0;
        //    txtCountryCode.Text = "";
        //    txtIncomeTaxNo.Text = "";
        //    txtIECode.Text = "";
        //    txtADCode.Text = "";
        //}

        protected void ddlCountry_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtCountryCode.Text = ddlCountry.SelectedValue;
            txtPANNo.Focus();
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
                    string quer = string.Empty;
                    DataSet ds = new DataSet();
                    quer = "select Top(10) TransId,AccountName from M_AccountMaster where AccountName like '" + txtSearch.Text + "%' order by TransId desc";
                    ds = GetDataSet(quer);
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
        public int ExecuteNonQuery(string Query)
        {
            int Result = 0;
            try
            {
                SqlConnection con = new SqlConnection(strconn);
                con.Open();
                SqlCommand cmd = new SqlCommand(Query.ToString(), con);
                Result = cmd.ExecuteNonQuery();
                con.Close();
            }
            catch (Exception ex)
            {
                string Msg = ex.Message;
            }
            return Result;
        }
        public DataSet GetDataSet(string Query)
        {
            DataSet ds = new DataSet();
      
           try
            {
               
                SqlConnection Conn = new SqlConnection(strconn);
                Conn.Open();
                SqlDataAdapter da = new SqlDataAdapter(Query, Conn);
                da.Fill(ds, "Table");
                Conn.Close();
            }
            catch (Exception ex)
            {
                string Msg = ex.Message;
            }
            return ds;
        }

        protected void ddlPaymentPeriod_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}