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
    public partial class frmImportNFormRegister : System.Web.UI.Page
    {
        string con = (string)ConfigurationManager.ConnectionStrings["Constr"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                FillJob();
            }
        }

        public void FillJob()
        {
            SqlConnection conn = new SqlConnection(con);
            conn.Open();
            string Query = "Select Distinct JobNo from T_JobCreation ";
            SqlDataAdapter da = new SqlDataAdapter(Query, conn);
            DataSet ds = new DataSet();
            da.Fill(ds, "data");
            if (ds.Tables["data"].Rows.Count != 0)
            {
                ddlJobNo.DataSource = ds;
                ddlJobNo.DataTextField = "";
                ddlJobNo.DataValueField = "";
                ddlJobNo.DataBind();
            }
        }

        protected void btnGenerate_Click(object sender, EventArgs e)
        {
            StringBuilder StrQuery = new StringBuilder();

           

            SqlConnection conn = new SqlConnection(con);
            conn.Open();

            //if (TransMode != "")
            //{
            //    StrQuery.Append(" and [Customer Name] ='" + TransMode + "'  ");
            //}
            //if (Transporter != "")
            //{
            //    StrQuery.Append(" and [Customer Name] ='" + Transporter + "'  ");
            //}
            //if (Branch != "")
            //{
            //    StrQuery.Append(" and [Customer Name] ='" + Branch + "'  ");
            //}
            //if (Imp != "")
            //{
            //    StrQuery.Append(" and [Customer Name] ='" + Imp + "'  ");
            //}
            //if (Date != "")
            //{
            //    StrQuery.Append(" and [Customer Name] ='" + Date + "'  ");
            //}

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