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
    public partial class frmUserRoles : System.Web.UI.Page
    {
        string con = (string)ConfigurationManager.ConnectionStrings["Constr"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            //string uName = (string)Session["UserName"];
            if (IsPostBack == false)
            {
                string formID = "User Role";
                Authenticate.Forms(formID);
                string Validate = (string)Session["DISABLE"];
                if (Validate == "True")
                {
                    var modifiydate = DateTime.Now;
                    var createdate = DateTime.Now;
                    showgrid();
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "redirect", "alert('You have not Authorized for this Page'); window.location.href='HomePage.aspx';", true);
                }
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            string userroles = txtUserRoleName.Text;
           
            SqlConnection sqlConn = new SqlConnection(con);
            sqlConn.Open();
            string query = "Insert into UserRoles(Roles,CreatedBy,CreatedDate,ModifiedBy,ModifiedDate)  Values('" + userroles + "','" + (string)Session["Username"] + "','" + DateTime.Now + "','" + (string)Session["Username"] + "','" + DateTime.Now + "') ";
            SqlCommand cmd = new SqlCommand(query, sqlConn);  
            cmd.ExecuteNonQuery();
            sqlConn.Close();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "redirect", "alert('Saved Successfully'); window.location.href='frmUserRoles.aspx';", true);
            showgrid();
            
        }

        public void showgrid()
        {
            SqlConnection conn = new SqlConnection(con);
            string query1 = "select TransId as [Serial No],Roles from  UserRoles";
            SqlDataAdapter da = new SqlDataAdapter(query1, con);
            DataSet ds = new DataSet();
            da.Fill(ds, "UserRoles");
            grdUserRole.DataSource = ds;
            grdUserRole.DataBind();
        
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("frmUserRoles.aspx");
        }
    }
}