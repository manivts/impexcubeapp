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
    public partial class frmDeveloperGuide : System.Web.UI.Page
    {
        string strconn = (string)ConfigurationManager.AppSettings["Constr"];
        CommonDL objCommonDL = new CommonDL();
        int Result = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                filldropdown();
                txttransdate.Text = System.DateTime.Now.ToString("dd/MM/yyyy");
            }
        }

        public void filldropdown()
        {
            string MenuQuery = "select Distinct GroupName from M_FormName ";
            DataSet dsMenu = new DataSet();
            dsMenu = objCommonDL.GetDataSet(MenuQuery);

            if (dsMenu.Tables["Table"].Rows.Count != 0)
            {
                ddlmodulename.DataSource = dsMenu;
                ddlmodulename.DataTextField = "GroupName";
                ddlmodulename.DataValueField = "GroupName";
                ddlmodulename.DataBind();
                ddlmodulename.Items.Insert(0, new ListItem("~Select~", "0"));   
            }

            
        }

        protected void ddlmodulename_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataSet dsName = new DataSet();
            string quer1 = "select Distinct FormName from M_FormName  where GroupName='" + ddlmodulename.SelectedValue + "'";
            dsName = objCommonDL.GetDataSet(quer1);
            ddlformname.DataSource = dsName;
            ddlformname.DataTextField = "FormName";
            ddlformname.DataValueField = "FormName";
            ddlformname.DataBind();
            ddlformname.Items.Insert(0, new ListItem("~Select~", "0"));   
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
                Query.Append("INSERT INTO [T_DeveloperGuide] (");
                Query.Append("TransDate,DeveloperName,AlocateBy,ModuleName,FormName,Description,CreatedBy,CreatedDate)");
                Query.Append("Values(");
                Query.Append("@TransDate,@DeveloperName,@AlocateBy,@ModuleName,@FormName,@Description,@CreatedBy,@CreatedDate)");
                using (SqlConnection con = new SqlConnection(strconn))
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand(Query.ToString(), con);
                    cmd.Parameters.AddWithValue("@TransDate", txttransdate.Text);
                    cmd.Parameters.AddWithValue("@DeveloperName", txtdevname.Text);
                    cmd.Parameters.AddWithValue("@AlocateBy", txtallotedby.Text);
                    cmd.Parameters.AddWithValue("@ModuleName", ddlmodulename.SelectedValue);
                    cmd.Parameters.AddWithValue("@FormName", ddlformname.SelectedValue);
                    cmd.Parameters.AddWithValue("@Description", txtdesc.Text);                    
                    cmd.Parameters.AddWithValue("@CreatedBy", CreatedBy);
                    cmd.Parameters.AddWithValue("@CreatedDate", CreatedDate);

                    Result = cmd.ExecuteNonQuery();
                    con.Close();
                    if (Result == 1)
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "redirect", "alert('Saved Successfully'); window.location.href='frmDeveloperGuide.aspx';", true);
                    }
                }
            }
            catch (SqlException ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('DateBase Error :  " + ex.Message + " ');", true);
            }
        }
    }
}