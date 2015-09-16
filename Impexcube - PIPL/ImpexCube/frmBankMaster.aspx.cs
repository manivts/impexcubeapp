using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Data.SqlClient;
using System.Collections.Generic;
using VTS.ImpexCube.Data;
using System.Text;

namespace ImpexCube
{
    public partial class frmBankMaster : System.Web.UI.Page
    {
        string strconn = (string)ConfigurationManager.AppSettings["ConnectionDashboard"];
        CommonDL objCommonDL = new CommonDL();
        int Result = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack == false)
            {
                btnUpdate.Visible = false;
                filldropdown();
                GridLoad();
            }   
        }

        public void filldropdown()
        {
            DataSet dsType = new DataSet();
            string quer1 = "select distinct AccountType from [M_AccountMaster]";
            dsType = objCommonDL.GetDataSet(quer1);
            ddlType.DataSource = dsType;
            ddlType.DataTextField = "AccountType";
            ddlType.DataValueField = "AccountType";
            ddlType.DataBind();
            ddlType.Items.Insert(0, new ListItem("~Select~", "0"));       

            DataSet dsCurrency = new DataSet();
            string quer2 = "select * from [M_Currency]";
            dsCurrency = objCommonDL.GetDataSet(quer2);
            ddlCurrency.DataSource = dsCurrency;
            ddlCurrency.DataTextField = "CurrencyShortName";
            ddlCurrency.DataValueField = "CurrencyShortName";
            ddlCurrency.DataBind();
            ddlCurrency.Items.Insert(0, new ListItem("~Select~", "0"));  
        }

        protected void ddlType_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataSet dsName = new DataSet();
            string quer1 = "select AccountName, AccountType,AccountCode from [M_AccountMaster] where AccountType='" +ddlType.SelectedValue+ "'";
            dsName = objCommonDL.GetDataSet(quer1);
            ddlName.DataSource = dsName;
            ddlName.DataTextField = "AccountName";
            ddlName.DataValueField = "AccountCode";
            ddlName.DataBind();
            ddlName.Items.Insert(0, new ListItem("~Select~", "0"));   
        }

        public void GridLoad()
        {
            DataSet ds = new DataSet();
            string quer = "select * from M_BankMaster";
            ds = objCommonDL.GetDataSet(quer);
            if (ds.Tables["Table"].Rows.Count != 0)
            {
                gvBankDetails.DataSource = ds;
                gvBankDetails.DataBind();
            }
            else
            {
                gvBankDetails.DataSource = null;
                gvBankDetails.DataBind();
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            string CreatedBy = (string)Session["USER-NAME"];
            string CreatedDate = DateTime.Now.ToString();

            StringBuilder Query = new StringBuilder();
            string Message = string.Empty;
            DataSet ds = new DataSet();
            try
            {
                Query.Append("INSERT INTO [M_BankMaster] (");
                Query.Append("AccountCode,AccountName,AccountType,BankName,BranchName,Address,Country,City,PinCode,AccNo,IFSCCode,IBanNo,SwiftCode,Currency,CreatedBy,CreatedDate)");
                Query.Append("Values(");
                Query.Append("@AccountCode,@AccountName,@AccountType,@BankName,@BranchName,@Address,@Country,@City,@PinCode,@AccNo,@IFSCCode,@IBanNo,@SwiftCode,@Currency,@CreatedBy,@CreatedDate)");
                using (SqlConnection con = new SqlConnection(strconn))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand(Query.ToString(), con);
                    cmd.Parameters.AddWithValue("@AccountCode", ddlName.SelectedValue);
                    cmd.Parameters.AddWithValue("@AccountName", ddlName.SelectedItem.Text);
                    cmd.Parameters.AddWithValue("@AccountType", ddlType.SelectedItem.Text);
                    cmd.Parameters.AddWithValue("@BankName", txtBranchName.Text);
                    cmd.Parameters.AddWithValue("@BranchName", txtBranchName.Text);
                    cmd.Parameters.AddWithValue("@Address", txtAddress.Text);
                    cmd.Parameters.AddWithValue("@Country", txtCountry.Text);
                    cmd.Parameters.AddWithValue("@City", txtCity.Text);
                    cmd.Parameters.AddWithValue("@PinCode", txtPinCode.Text);
                    cmd.Parameters.AddWithValue("@AccNo", txtAccNo.Text);
                    cmd.Parameters.AddWithValue("@IFSCCode", txtIFSCCode.Text);
                    cmd.Parameters.AddWithValue("@IBanNo", txtIBanNo.Text);
                    cmd.Parameters.AddWithValue("@SwiftCode", txtSwiftCode.Text);
                    cmd.Parameters.AddWithValue("@Currency", ddlCurrency.SelectedValue);
                    cmd.Parameters.AddWithValue("@CreatedBy",  CreatedBy);
                    cmd.Parameters.AddWithValue("@CreatedDate", CreatedDate);

                    Result = cmd.ExecuteNonQuery();
                    con.Close();
                    if (Result == 1)
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "redirect", "alert('Saved Successfully'); window.location.href='frmBankMaster.aspx';", true);
                    }
                }
            }
            catch (SqlException ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('DateBase Error :  "+ ex.Message +" ');", true);
            }
        }

        protected void gvBankDetails_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                btnSave.Visible = false;
                btnUpdate.Visible = true;
                hdnBankMaster.Value = gvBankDetails.SelectedRow.Cells[1].Text;
                DataSet ds = new DataSet();
                string quer = "select * from M_BankMaster where [TransId] ='" + hdnBankMaster.Value + "'";
                ds = objCommonDL.GetDataSet(quer);
                if (ds.Tables["Table"].Rows.Count != 0)
                {
                    DataRowView dr = ds.Tables["Table"].DefaultView[0];
                    hdnBankMaster.Value = dr["TransId"].ToString();

                    ddlType.SelectedValue = dr["AccountType"].ToString();
                    ddlType_SelectedIndexChanged(sender, e);
                    ddlName.SelectedIndex = ddlName.Items.IndexOf(ddlName.Items.FindByText(dr["AccountName"].ToString()));
                    txtBankName.Text = dr["BankName"].ToString();
                    txtBranchName.Text = dr["BranchName"].ToString();
                    txtAddress.Text = dr["Address"].ToString();
                    txtCountry.Text = dr["Country"].ToString();
                    txtCity.Text = dr["City"].ToString();
                    txtPinCode.Text = dr["PinCode"].ToString();
                    txtAccNo.Text = dr["AccNo"].ToString();
                    txtIFSCCode.Text = dr["IFSCCode"].ToString();
                    txtIBanNo.Text = dr["IBanNo"].ToString();
                    txtSwiftCode.Text = dr["SwiftCode"].ToString();
                    ddlCurrency.SelectedValue = dr["Currency"].ToString();
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('DateBase Error :  " + ex.Message + " ');", true);
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
                Query.Append("UPDATE [M_BankMaster]");
                Query.Append("SET ");
                Query.Append("AccountCode=@AccountCode,AccountName=@AccountName,");
                Query.Append("AccountType=@AccountType,BankName=@BankName,BranchName=@BranchName,Address=@Address,Country=@Country,City=@city,");
                Query.Append("PinCode=@PinCode,AccNo=@AccNo,IFSCCode=@IFSCCode,IBanNo=@IBanNo,SwiftCode=@SwiftCode,Currency=@Currency,");
                Query.Append("ModifiedBy=@ModifiedBy,ModifiedDate=@ModifiedDate");
                Query.Append(" WHERE TransId=@TransID");

                using (SqlConnection con = new SqlConnection(strconn))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand(Query.ToString(), con);
                    cmd.Parameters.AddWithValue("@AccountCode", ddlName.SelectedValue);
                    cmd.Parameters.AddWithValue("@AccountName", ddlName.SelectedItem.Text);
                    cmd.Parameters.AddWithValue("@AccountType", ddlType.SelectedItem.Text);
                    cmd.Parameters.AddWithValue("@BankName", txtBranchName.Text);
                    cmd.Parameters.AddWithValue("@BranchName", txtBranchName.Text);
                    cmd.Parameters.AddWithValue("@Address", txtAddress.Text);
                    cmd.Parameters.AddWithValue("@Country", txtCountry.Text);
                    cmd.Parameters.AddWithValue("@City", txtCity.Text);
                    cmd.Parameters.AddWithValue("@PinCode", txtPinCode.Text);
                    cmd.Parameters.AddWithValue("@AccNo", txtAccNo.Text);
                    cmd.Parameters.AddWithValue("@IFSCCode", txtIFSCCode.Text);
                    cmd.Parameters.AddWithValue("@IBanNo", txtIBanNo.Text);
                    cmd.Parameters.AddWithValue("@SwiftCode", txtSwiftCode.Text);
                    cmd.Parameters.AddWithValue("@Currency", ddlCurrency.SelectedValue);
                    cmd.Parameters.AddWithValue("@ModifiedBy", ModifiedBy);
                    cmd.Parameters.AddWithValue("@ModifiedDate", ModifiedDate);
                    cmd.Parameters.AddWithValue("@TransID", hdnBankMaster.Value);

                    Result = cmd.ExecuteNonQuery();
                    con.Close();
                }
                if (Result == 1)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "redirect", "alert('Updated Successfully'); window.location.href='frmBankMaster.aspx';", true);
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('DateBase Error :  " + ex.Message + " ');", true);
            }
        }

        protected void btnNew_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/frmBankMaster.aspx");
        }

        protected void btnDiscard_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/HomePage.aspx");
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
                string quer = string.Empty;
                DataSet ds = new DataSet();
                quer = "select TransId,AccountName,AccountType,BankName,Currency,AccNo,City from M_BankMaster where AccountName like '%" + txtSearch.Text + "%' order by TransId desc";
                ds = objCommonDL.GetDataSet(quer);
                if (ds.Tables["Table"].Rows.Count != 0)
                {
                    gvBankDetails.DataSource = ds;
                    gvBankDetails.DataBind();
                }
                else
                {
                    gvBankDetails.DataSource = null;
                    gvBankDetails.DataBind();
                }
        }
       
    }
}