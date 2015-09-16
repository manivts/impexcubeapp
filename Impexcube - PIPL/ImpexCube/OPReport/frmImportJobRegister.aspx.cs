using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using VTS.ImpexCube.Utlities;
using System.Configuration;

namespace ImpexCube.OPReport
{
    public partial class frmImportJobRegister : System.Web.UI.Page
    {
        string con = (string)ConfigurationManager.ConnectionStrings["Constr"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                FillTransMode();
                FillJobStatus();
                FillBranch();
            }
        }

        public void FillTransMode()
        {
            SqlConnection conn = new SqlConnection(con);
            conn.Open();
            string Query = " ";
            SqlDataAdapter da = new SqlDataAdapter(Query, conn);
            DataSet ds = new DataSet();
            da.Fill(ds, "data");
            if (ds.Tables["data"].Rows.Count != 0)
            {
                ddlTransportMode.DataSource = ds;
                ddlTransportMode.DataTextField = "";
                ddlTransportMode.DataValueField = "";
                ddlTransportMode.DataBind();
            }
        }
        public void FillJobStatus()
        {
            SqlConnection conn = new SqlConnection(con);
            conn.Open();
            string Query = " ";
            SqlDataAdapter da = new SqlDataAdapter(Query, conn);
            DataSet ds = new DataSet();
            da.Fill(ds, "data");
            if (ds.Tables["data"].Rows.Count != 0)
            {
                ddlJobStatus.DataSource = ds;
                ddlJobStatus.DataTextField = "";
                ddlJobStatus.DataValueField = "";
                ddlJobStatus.DataBind();
            }
        }
        public void FillBranch()
        {
            SqlConnection conn = new SqlConnection(con);
            conn.Open();
            string Query = " ";
            SqlDataAdapter da = new SqlDataAdapter(Query, conn);
            DataSet ds = new DataSet();
            da.Fill(ds, "data");
            if (ds.Tables["data"].Rows.Count != 0)
            {
                ddlBranch.DataSource = ds;
                ddlBranch.DataTextField = "";
                ddlBranch.DataValueField = "";
                ddlBranch.DataBind();
            }
        }

        protected void btnGenerate_Click(object sender, EventArgs e)
        {
            StringBuilder StrQuery = new StringBuilder();

            string TransMode=ddlTransportMode.SelectedValue;
            string JobStatus=ddlJobStatus.SelectedValue;
            string Branch=ddlBranch.SelectedValue;
            string Imp=txtImporter.Text;
            string Date=txtDate.Text;

            SqlConnection conn = new SqlConnection(con);
            conn.Open();

            if (TransMode != "")
            {
                StrQuery.Append(" and [Customer Name] ='" + TransMode + "'  ");
            }
            if (JobStatus != "")
            {
                StrQuery.Append(" and [Customer Name] ='" + JobStatus + "'  ");
            }
            if (Branch != "")
            {
                StrQuery.Append(" and [Customer Name] ='" + Branch + "'  ");
            }
            if (Imp != "")
            {
                StrQuery.Append(" and [Customer Name] ='" + Imp + "'  ");
            }
            if (Date != "")
            {
                StrQuery.Append(" and [Customer Name] ='" + Date + "'  ");
            }

            string Query = " ";

            SqlDataAdapter da = new SqlDataAdapter(Query, conn);
            DataSet ds = new DataSet();
            da.Fill(ds, "data");
            if (ds.Tables["data"].Rows.Count != 0)
            {               
                gvImporterJob.DataSource = ds;
                gvImporterJob.DataBind();
            }

        }

        
    }
}