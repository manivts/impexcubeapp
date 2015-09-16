using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
namespace AccountsManagement
{
    public partial class ConvertToLedger : System.Web.UI.Page
    {
        public int count = 0;
        string strconn = (string)ConfigurationManager.ConnectionStrings["Constr"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["mode"] == "New")
                {
                    SqlConnection con = new SqlConnection(strconn);
                    con.Open();
                    DataSet ds = new DataSet();
                    //string query = "SELECT Distinct CompName,address1,BranchId,CompName As AccountName,Substring(CompName,0,6) As ShortName FROM View_CustomerAndCharges WHERE CCCompleted=0";
                    //string query = "SELECT Distinct CompName,address1,BranchId,CompName As AccountName,Substring(CompName,0,6) As ShortName FROM View_AccountMaster_SundryDebtors WHERE AccountName is null And BranchId is null";
                    string query = "SELECT Distinct CompName,address1,BranchId,CompName As AccountName,Substring(CompName,0,6)  As ShortName FROM View_AccountMaster_SundryDebtors WHERE AccountName is null And BranchId is not null";
                    SqlDataAdapter sd = new SqlDataAdapter(query, con);
                    sd.Fill(ds, "data");
                    con.Close();

                    GridLedger.DataSource = ds;
                    GridLedger.DataBind();
                    count = GridLedger.Rows.Count;
                    if (count > 0)
                    {
                        btnSave.Visible = true;
                    }
                    else
                    {
                        btnSave.Visible = false;
                        txtNote.Text = "No Record Found";
                    }
                }
                else if (Request.QueryString["mode"] == "Edit")
                {
                    SqlConnection con = new SqlConnection(strconn);
                    con.Open();
                    DataSet ds = new DataSet();
                    string query = "SELECT Distinct CompName,address1,BranchId,CompName As AccountName,Substring(CompName,0,6)  As ShortName FROM View_AccountMaster_SundryDebtors WHERE AccountName is null And BranchId is not null";
                    SqlDataAdapter sd = new SqlDataAdapter(query, con);
                    sd.Fill(ds, "data");
                    con.Close();

                    GridLedger.DataSource = ds;
                    GridLedger.DataBind();
                    count = GridLedger.Rows.Count;
                    if (count > 0)
                    {
                        btnSave.Visible = true;
                    }
                    else
                    {
                        btnSave.Visible = false;
                        txtNote.Text = "No Record Found";
                    }
                }
            }
        }
        public int CheckLedgerName(string CompName, string BranchId)
        {
            SqlConnection con = new SqlConnection(strconn);
            con.Open();
            DataSet ds = new DataSet();

            string query = "SELECT * FROM M_AccountMaster_SundryDebtors WHERE CompName='" + CompName + "' And BranchId='" + BranchId + "'";
            SqlDataAdapter sd = new SqlDataAdapter(query, con);
            sd.Fill(ds, "data");
            con.Close();
            int i = ds.Tables["data"].Rows.Count;
            return i;


        }
        private void Store(string CompName, string Address1, string BranchId, string AccountName, string ShortName, string CreatedBy, DateTime CreatedDate, string ModifiedBy, DateTime ModifiedDate)
        {
            try
            {
                int j = CheckLedgerName(CompName, BranchId);
                if (j == 0)
                {
                    SqlConnection conn = new SqlConnection(strconn);
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("Insert into M_AccountMaster_SundryDebtors(CompName,Address1,BranchId,AccountName,ShortName,CreatedBy,CreatedDate,ModifiedBy,ModifiedDate)values(@CompName,@Address1,@BranchId,@AccountName,@ShortName,@CreatedBy,@CreatedDate,@ModifiedBy,@ModifiedDate)", conn);
                    cmd.Parameters.AddWithValue("@CompName", CompName);
                    Address1 = Address1.Replace("\n", "");
                    Address1 = Address1.Replace("\r", "");
                    cmd.Parameters.AddWithValue("@Address1", Address1);
                    BranchId = BranchId.Replace("&nbsp;", "");
                    cmd.Parameters.AddWithValue("@BranchId", BranchId);
                    cmd.Parameters.AddWithValue("@AccountName", AccountName);
                    cmd.Parameters.AddWithValue("@ShortName", ShortName);
                    cmd.Parameters.AddWithValue("@CreatedBy", CreatedBy);
                    cmd.Parameters.AddWithValue("@CreatedDate", CreatedDate);
                    cmd.Parameters.AddWithValue("@ModifiedBy", ModifiedBy);
                    cmd.Parameters.AddWithValue("@ModifiedDate", ModifiedDate);
                    int i = cmd.ExecuteNonQuery();
                    conn.Close();
                    //if (i > 0)
                    //{
                    //    AccountMaster(CompName,Address1,BranchId,"INDIA","IN",AccountName, ShortName, "Sundry Debtors", true, false, false, true, CreatedBy, CreatedDate, ModifiedBy, ModifiedDate);
                    //    AccountDetails(CompName, Address1, BranchId, "INDIA", "IN", AccountName, ShortName, "Sundry Debtors", true, false, false, true, CreatedBy, CreatedDate, ModifiedBy, ModifiedDate);
                    //}
                }
            }
            catch
            {
            }
        }
        private void AccountMaster(string CompName,string Address1,string BranchId,string Country,string CountryCode,string accname, string shortname, string grpname, bool app, bool completed, bool Costcenter, bool billref, string CreatedBy, DateTime CreatedDate, string ModifiedBy, DateTime ModifiedDate)
        {
            try
            {
                SqlConnection conn = new SqlConnection(strconn);
                conn.Open();
                SqlCommand cmd = new SqlCommand("Insert into M_AccountMaster(AccountCode,AccountName,Acc_Group,ShortName,CostCenter,BillWiseOn,Approved,Completed,CreatedBy,CreatedDate,ModifiedBy,ModifiedDate)values(@AccountCode,@AccountName,@Acc_Group,@ShortName,@CostCenter,@BillWiseOn,@Approved,@Completed,@CreatedBy,@CreatedDate,@ModifiedBy,@ModifiedDate)", conn);
                cmd.Parameters.AddWithValue("@AccountCode", accname);
                cmd.Parameters.AddWithValue("@AccountName", accname);
                cmd.Parameters.AddWithValue("@Acc_Group", grpname);
                cmd.Parameters.AddWithValue("@ShortName", shortname);
                cmd.Parameters.AddWithValue("@CostCenter", Costcenter);
                cmd.Parameters.AddWithValue("@BillWiseOn", billref);
                cmd.Parameters.AddWithValue("@Approved", app);
                cmd.Parameters.AddWithValue("@Completed", completed);
                // Country, Countrycode
                // BranchId, BranchName, Address1
                //TallyAccountName, RefName,
                cmd.Parameters.AddWithValue("@CreatedBy", CreatedBy);
                cmd.Parameters.AddWithValue("@CreatedDate", CreatedDate);
                cmd.Parameters.AddWithValue("@ModifiedBy", ModifiedBy);
                cmd.Parameters.AddWithValue("@ModifiedDate", ModifiedDate);
                cmd.ExecuteNonQuery();
                conn.Close();
            }
            catch
            {
            }
        }
        private void AccountDetails(string CompName, string Address1, string BranchId, string Country, string CountryCode, string accname, string shortname, string grpname, bool app, bool completed, bool Costcenter, bool billref, string CreatedBy, DateTime CreatedDate, string ModifiedBy, DateTime ModifiedDate)
    {
            try
            {
            SqlConnection conn = new SqlConnection(strconn);
            conn.Open();
            SqlCommand cmd=new SqlCommand ("Insert into M_AccountDetails(AccountCode,AccountName,Acc_Group,ShortName,CostCenter,BillWiseOn,Approved,Completed,CreatedBy,CreatedDate,ModifiedBy,ModifiedDate)values(@AccountCode,@AccountName,@Acc_Group,@ShortName,@CostCenter,@BillWiseOn,@Approved,@Completed,@CreatedBy,@CreatedDate,@ModifiedBy,@ModifiedDate)",conn);
            cmd.Parameters.AddWithValue("@AccountCode",accname);
            cmd.Parameters.AddWithValue("@AccountName",accname);
            cmd.Parameters.AddWithValue("@Acc_Group",grpname);
            cmd.Parameters.AddWithValue("@ShortName",shortname);
            cmd.Parameters.AddWithValue("@CostCenter",Costcenter);
            cmd.Parameters.AddWithValue("@BillWiseOn",billref);
            cmd.Parameters.AddWithValue("@Approved",app);
            cmd.Parameters.AddWithValue("@Completed",completed);
            cmd.Parameters.AddWithValue("@CreatedBy",CreatedBy);
            cmd.Parameters.AddWithValue("@CreatedDate",CreatedDate);
            cmd.Parameters.AddWithValue("@ModifiedBy",ModifiedBy);
            cmd.Parameters.AddWithValue("@ModifiedDate", ModifiedDate);
            cmd.ExecuteNonQuery();
            conn.Close();
            }
            catch
            {
            }
    }
   
        protected void btnSave_Click(object sender, EventArgs e)
        {
         int i = 0;
            foreach (GridViewRow dr in GridLedger.Rows)
            {
                string compname = GridLedger.Rows[i].Cells[0].Text.ToString();
                string address1 = GridLedger.Rows[i].Cells[1].Text.ToString();
                string BranchId = GridLedger.Rows[i].Cells[2].Text.ToString();
                TextBox Ledger = (TextBox)dr.FindControl("txtledger");
                TextBox shorts = (TextBox)dr.FindControl("txtshort");
                if (Ledger.Text != "")
                {
                    Store(compname, address1, BranchId, Ledger.Text, shorts.Text, (string)Session["UserName"], DateTime.Now, (string)Session["UserName"], DateTime.Now);
                }
                i++;
            }
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Successfully Saved')", true);
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
      
    }
}