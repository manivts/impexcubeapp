using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace ImpexCube
{
    public partial class frmImpJobSearch : System.Web.UI.Page
    {
        string strconn = (string)ConfigurationManager.ConnectionStrings["Constr"].ConnectionString;
        VTS.ImpexCube.Business.JobStageUpdateBL objJobStage = new VTS.ImpexCube.Business.JobStageUpdateBL();
        protected void Page_Load(object sender, EventArgs e)
        {
            Label pagename;
            pagename = (Label)Master.FindControl("lblName");
            pagename.Text = "Job Search";
            
            if (!Page.IsPostBack)
            {
                ddlkeyowrdfill();
            }
        }

        public void ddlkeyowrdfill()
        {
            DataSet ds = new DataSet();
            string Query = "select distinct keyword from M_ReportField";
            SqlConnection sqlConn = new SqlConnection(strconn);
            sqlConn.Open();
            SqlDataAdapter da = new SqlDataAdapter(Query, sqlConn);
            da.Fill(ds, "key");
            sqlConn.Close();
            if (ds.Tables["key"].Rows.Count != 0)
            {
                ddlkeyword.DataSource = ds;
                ddlkeyword.DataTextField = "Keyword";
                ddlkeyword.DataValueField = "keyword"; 
                ddlkeyword.DataBind();
            }
            //sqlConn.Close();
        }

        protected void btnsearch_Click1(object sender, EventArgs e)
        {
            if (txtkeyword.Text != "")
            {
                SqlConnection con = new SqlConnection(strconn);
                con.Open();
                string quer = string.Empty;
                DataSet ds = new DataSet();
                quer = "select JobNo,JobReceivedDate,Importer from View_JobImporterShipment where " + ddlkeyword.SelectedValue + " LIKE '%" + txtkeyword.Text + "%'";
                SqlDataAdapter da = new SqlDataAdapter(quer, con);
                da.Fill(ds, "data");
                con.Close();
                if (ds.Tables["data"].Rows.Count != 0)
                {
                    gvsearch.DataSource = ds;
                    gvsearch.DataBind();
                }

                else
                {
                    gvsearch.DataSource = null;
                    gvsearch.DataBind();
                    //Response.Write("<script>alert('Please give the correct keyword and No')</script>");
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Please give the correct keyword and No')", true);   
                }
            }
            else
            {
               // Response.Write("<script LANGUAGE='JavaScript' >alert('Sorry please fill the field')</script>");
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Sorry please fill the field')", true);
            }

        }
    }
}