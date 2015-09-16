using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.Collections;
using System.Configuration;

namespace ImpexCube
{
    public partial class UserAuthentication : System.Web.UI.Page
    {
        string strconn = (string)ConfigurationManager.AppSettings["connectionstring"];
        string UserName = string.Empty;
        protected void Page_Load(object sender, EventArgs e)
        {
            UserName = Session["UserName"].ToString();
            if (!IsPostBack)
            {               
                LoadUserType();
            }
        }


        public void LoadUserType()
        {
            string Query = "Select Roles,CreatedBy from [UserRoles]";
            DataSet dsUser = new DataSet();
            dsUser = GetDataset(Query);
            ddlUserType.DataSource = dsUser;
            ddlUserType.DataValueField = "Roles";
            ddlUserType.DataTextField = "Roles";
            ddlUserType.DataBind();

            string Query1 = "select FormName,FormDesc,FormShortName from [FormMaster]";
            DataSet dsFormName = new DataSet();
            dsFormName = GetDataset(Query1);
            grdUserForms.DataSource = dsFormName;
            grdUserForms.DataBind();
        }


        public DataSet GetDataset(string Query)
        {
            DataSet ds = new DataSet();
            SqlConnection sqlConn = new SqlConnection(strconn);
            sqlConn.Open();
            SqlDataAdapter daa = new SqlDataAdapter(Query, sqlConn);
            daa.Fill(ds, "Dataset");
            sqlConn.Close();
            return ds;
        }

        public string ExecuteNonQuery(string Query)
        {
            string Msg = string.Empty;
            int result = 0;
            try
            {
                SqlConnection conn = new SqlConnection(strconn);
                conn.Open();
                SqlCommand cmd = new SqlCommand(Query, conn);
                SqlDataAdapter da = new SqlDataAdapter();
                cmd.CommandText = Query.ToString();
                cmd.Connection = conn;
                da.SelectCommand = cmd;
                result = cmd.ExecuteNonQuery();
                conn.Close();

            }
            catch (Exception ex)
            {
                Msg = ex.Message;
            }
            return Msg;
        }

        protected void Btnsave_Click(object sender, ImageClickEventArgs e)
        {        
            string UserNAme=ddlUserType.SelectedValue.ToString();
            int i=0;
            string Msg = string.Empty;
            foreach (GridViewRow grr in grdUserForms.Rows)
            {
                  string BranchName = string.Empty;
                  string ComapanyCode = string.Empty;
                  string BranchCode = string.Empty;
                  string FormNAme = grdUserForms.Rows[i].Cells[0].Text.ToString();
                  string FormDesc = grdUserForms.Rows[i].Cells[1].Text.ToString();
                  
                  CheckBox chkRead = (CheckBox)grr.FindControl("chkRead");
                  CheckBox chkWrite = (CheckBox)grr.FindControl("chkWrite");
                  CheckBox chkApproval = (CheckBox)grr.FindControl("chkApproval");

                  bool Read = false;
                  bool Write = false;
                  bool Approval = false;

                  Read = chkRead.Checked;
                  Write = chkWrite.Checked;
                  Approval = chkApproval.Checked;

                  StringBuilder Qry = new StringBuilder();
                  //Qry.Append("INSERT INTO [User_Authority]([UserName],[FormName],[Read],[Write],[Approval],[CreateBy],[CreateOn])");
                  //Qry.Append("VALUES(");
                  //Qry.Append("'" + UserNAme + "','" + FormNAme + "','" + Read + "','" + Write + "','" + Approval + "','" + UserName + "',getdate())");

                  Qry.Append("INSERT INTO [UserAuthorizationForms]");
                  Qry.Append("([UserRole],[CompanyCode],[BranchName],[BranchCode],[FormName],[Read],[Write],[Approval],[CreatedBy],[CreatedDate])");
                  Qry.Append("VALUES(");
                  Qry.Append("'" + UserNAme + "','" + ComapanyCode + "','" + BranchName + "','" + BranchName + "','" + FormNAme + "','" + Read + "','" + Write + "','" + Approval + "','" + UserName + "',getdate())");

                  Msg = ExecuteNonQuery(Qry.ToString());
                  i++;
            }
            ScriptManager.RegisterStartupScript(this, this.GetType(), "redirect", "alert('Saved Sucessfully....'); window.location.href='UserAuthentication.aspx';", true);
        }
    }
}