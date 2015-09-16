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
using System.IO;
using System.Net.Mail;
using System.Net.Mime;
using System.Reflection;
using System.Drawing;// to use Missing.Value
using Microsoft.Office;
using Microsoft.Office.Core;
using System.Runtime.InteropServices;
using Outlook = Microsoft.Office.Interop.Outlook;
using MySql.Data.MySqlClient;

namespace ImpexCube
{
    public partial class frmFileUpload : System.Web.UI.Page
    {
      
        private string FilePath = "";

        string strconn = (string)ConfigurationManager.ConnectionStrings["Constr"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack == false)
            {
                if (Request.QueryString["Mode"] == "Import")
                {
                    BindImportJobno();
                }
                else if (Request.QueryString["Mode"] == "Export")
                {
                    BindExportJobno();
                }
               
            }
        }

        private void BindImportJobno()
        {
            string jobno = (string)Session["FYear"];
            DataSet ds = new DataSet();
            try
            {
                string Query = "select JOBNO from T_JobCreation where Completed=1";

                SqlConnection con = new SqlConnection(strconn);
                con.Open();
                SqlDataAdapter sd = new SqlDataAdapter(Query, con);
                sd.Fill(ds, "jobno");
                con.Close();

                if (ds.Tables["jobno"].Rows.Count != 0)
                {
                    ddlJobNo.DataSource = ds;
                    ddlJobNo.DataTextField = "JobNo";
                    ddlJobNo.DataValueField = "JobNo";
                    ddlJobNo.DataBind();
                    ddlJobNo.Items.Insert(0, new ListItem("~Select~", "0"));
                }
                else
                {
                    ddlJobNo.DataSource = null;
                    ddlJobNo.DataBind();
                }
            }
            catch (Exception ex)
            {
                string Msg = ex.Message.ToString();
            }           
        }
        private void BindExportJobno()
        {
            string jobno = (string)Session["FYear"];
            DataSet ds = new DataSet();
            try
            {
                string Query = "select JobNo from E_M_JobCreation";

                SqlConnection con = new SqlConnection(strconn);
                con.Open();
                SqlDataAdapter sd = new SqlDataAdapter(Query, con);
                sd.Fill(ds, "jobno");
                con.Close();

                if (ds.Tables["jobno"].Rows.Count != 0)
                {
                    ddlJobNo.DataSource = ds;
                    ddlJobNo.DataTextField = "JobNo";
                    ddlJobNo.DataValueField = "JobNo";
                    ddlJobNo.DataBind();
                    ddlJobNo.Items.Insert(0, new ListItem("~Select~", "0"));
                }
                else
                {
                    ddlJobNo.DataSource = null;
                    ddlJobNo.DataBind();
                }
            }
            catch (Exception ex)
            {
                string Msg = ex.Message.ToString();
            }
        }
      
        protected void btnDownLoad_Click(object sender, EventArgs e)
        {
            try
            {
                if (Request.QueryString["Mode"] == "Import")
                {
                    string strFileName = ddlJobNo.SelectedValue;
                    strFileName = "00" + strFileName + "14";
                    string strFileExtension = ".be";
                    Response.AddHeader("content-disposition", "attachment;filename=" + strFileName + "" + strFileExtension);
                    //Response.TransmitFile(Server.MapPath("~/TempFile/BEFile/FATFile/" + ddlJobNo.SelectedValue + ".be"));
                    Response.TransmitFile(Server.MapPath("~/TempFile/" + strFileName + ".be"));
                    Response.End();
                }
                else if (Request.QueryString["Mode"] == "Export")
                {
                    string strFileName = ddlJobNo.SelectedValue;
                    strFileName = "00" + strFileName + "14";
                    string strFileExtension = ".sb";
                    Response.AddHeader("content-disposition", "attachment;filename=" + strFileName + "" + strFileExtension);
                    Response.TransmitFile(Server.MapPath("~/TempFile/" + ddlJobNo.SelectedValue + ".sb"));
                    Response.End();
                }
                
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('BE File is not generated for the Job no " + ddlJobNo.SelectedValue + "');", true);
                //Response.Write("BE File is not generated for the Job no " + ddlJobNo.SelectedValue + "");
            }
        }

    }
}