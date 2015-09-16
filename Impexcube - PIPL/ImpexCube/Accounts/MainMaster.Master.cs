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

namespace ImpexCube.Accounts
{
    public partial class MainMaster : System.Web.UI.MasterPage
    {
        string strconn = (string)ConfigurationManager.ConnectionStrings["Constr"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                    if ((string)Session["USER-NAME"] != null)
                {
                    #region 
                    string username = (string)Session["USER-NAME"];
                    string company = (string)Session["CMP"];
                    string branch = (string)Session["ZONE"];
                    #endregion
                    #region
                    Session["Username"] = username;
                    Session["CompanyCode"] = company;
                    Session["BranchCode"] = branch;
                    Session["CompanyName"] = "Professional Impex Pvt Ltd";
                    #endregion
                   // GetCompany();
                }
                else
                {
                    if ((string)Session["Username"] == "" || (string)Session["Username"] == string.Empty)
                    {

                    }
                }
                
               
            }
        }

       
       
        protected void lbChangepass_Click1(object sender, EventArgs e)
        {
            try
            {
                Response.Write("<script>window.open('ChangePassword.aspx', '_blank','width=510,height=300, menubar=no, scrollbars=no, toolbar=no, location=no, resizable=no, left=400, top=280');</script>");
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('DataBase Error: " + ex.Message + "');", true);
            }
        }

        protected void TreeView1_SelectedNodeChanged(object sender, EventArgs e)
        {

        }

        protected void LinkButtonSignOut_Click(object sender, EventArgs e)
        {

        }       
    }
}
