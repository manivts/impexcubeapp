using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;

namespace ImpexCube
{
    public partial class frmUserAuthorization : System.Web.UI.Page
    {
        string strconn = (string)ConfigurationManager.ConnectionStrings["Constr"].ConnectionString;
        string EID = "";
        string Query = "";
        string branch = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (IsPostBack == false)
                {
                    string formID = "User Authorization";
                    Authenticate.Forms(formID);
                    string Validate = (string)Session["DISABLE"];
                    if (Validate == "True")
                    {
                        DropUsername();
                        GridFormLoad();
                        string uName = (string)Session["UserName"];
                        branch = Session["BranchCode"].ToString();
                        plUser.Visible = true;
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "redirect", "alert('You have not Authorized for this Page'); window.location.href='HomePage.aspx';", true);
                    }

                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('DataBase Error: " + ex.Message + "');", true);
            }
        }

        private void GridFormLoad()
        {
            SqlConnection conn = new SqlConnection(strconn);
            string query = "Select FormShortName From FormMaster";
            conn.Open();
            SqlDataAdapter da = new SqlDataAdapter(query, conn);
            DataSet ds = new DataSet();
            da.Fill(ds, "FormName");
            conn.Close();
            if (ds.Tables["FormName"].Rows.Count != 0)
            {
                GrdForms.DataSource = ds;
                GrdForms.DataBind();
            }
            else
            {
                GrdForms.DataSource = null;
                GrdForms.DataBind();
            }
        }

        private void DropUsername()
        {
            SqlConnection conn = new SqlConnection(strconn);
            string query = "Select Roles From UserRoles";
            conn.Open();
            SqlDataAdapter da = new SqlDataAdapter(query, conn);
            DataSet ds = new DataSet();
            da.Fill(ds, "Name");

            if (ds.Tables["Name"].Rows.Count != 0)
            {
                ddlUserType.DataSource = ds;
                ddlUserType.DataTextField = "Roles";
                ddlUserType.DataBind();
                ddlUserType.Items.Insert(0, new ListItem("-Select-"));

            }
        }

        private void GetCommandQuery(string sqlQuery)
        {
            SqlConnection conn = new SqlConnection(strconn);
            conn.Open();
            SqlCommand cmd = new SqlCommand(sqlQuery, conn);
            SqlDataAdapter da = new SqlDataAdapter();
            cmd.CommandText = sqlQuery;
            cmd.Connection = conn;
            da.SelectCommand = cmd;
            int result = cmd.ExecuteNonQuery();
            conn.Close();
        }                    

        protected void BtnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                int Count = 0;
                string FormName = string.Empty;
                // Update user Authority using for loops

                foreach (GridViewRow row1 in GrdForms.Rows)
                {
                    CheckBox chkRead = (CheckBox)row1.FindControl("chkRead");
                    CheckBox chkWrite = (CheckBox)row1.FindControl("chkWrite");
                    CheckBox chkApproval = (CheckBox)row1.FindControl("chkApproval");
                    FormName = row1.Cells[0].Text.ToString();

                    string read;
                    string write;
                    string approval;

                    if (chkRead.Checked == true)
                    {
                        read = "1";
                    }
                    else
                    {
                        read = "0";
                    }

                    if (chkApproval.Checked == true)
                    {
                        approval = "1";
                        write = "1";
                    }
                    else
                    {
                        approval = "0";
                    }

                    branch = Session["BranchCode"].ToString();

                    if (chkWrite.Checked == true)
                    {
                        write = "1";
                        chkRead.Enabled = false;
                    }
                    else
                    {
                        write = "0";
                    }

                    //To Insert User Authority
                    branch = (string)Session["BranchCode"];
                    string Query = "Insert Into UserAuthorizationForms (UserRole,CompanyCode,BranchName,BranchCode,FormName,[Read],Write,Approval," +
                        "CreatedBy,CreatedDate,ModifiedBy,ModifiedDate) Values ('" + ddlUserType.SelectedItem.Text + "','" + (string)Session["Company"] + "'," +
                        "'" + branch + "','" + branch + "','" + FormName + "'," + read + "," + write + "," + approval + ",'" + (string)Session["UserName"] + "','" + DateTime.Now + "','" + (string)Session["Username"] + "','" + DateTime.Now + "')";
                    GetCommandQuery(Query);
                    Count = Count + 1;
                }
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Saved Successfully');", true);
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('DataBase Error: " + ex.Message + "');", true);
            }
        }

        protected void ddlUserType_SelectedIndexChanged(object sender, EventArgs e)
        {
            string usertype = ddlUserType.SelectedItem.Text;
            string query = "Select FormName,[Read],Write,Approval from UserAuthorizationForms where UserRole = '" + usertype + "' ";
            SqlConnection conn = new SqlConnection(strconn);
            conn.Open();
            SqlDataAdapter da = new SqlDataAdapter(query, conn);
            DataSet ds = new DataSet();
            da.Fill(ds, "FormName");
            conn.Close();
        }
    }
}