using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace ImpexCube.OPReport
{
    public partial class frmPendingJobs : System.Web.UI.Page
    {
        string strcon = (string)ConfigurationManager.ConnectionStrings["Constr"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            GetBranchDetails();
        }

        public void GetBranchDetails()
        {
            SqlConnection conn = new SqlConnection();
            conn.Open();
            string qry = "";
            SqlDataAdapter da = new SqlDataAdapter(qry, conn);
            DataSet ds = new DataSet();
            da.Fill(ds, "DATA");
            ddlbranch.DataSource = ds;
            ddlbranch.DataTextField = "";
            ddlbranch.DataValueField = "";
            ddlbranch.DataBind();
            ddlbranch.Items.Insert(0, new ListItem("~Select~", "0"));

        }
        public static string removespace(string dates)
        {
            string[] dat = dates.Split(' ');
            string da = dat[0];
            return da;
        }
        public static string ChangeDate(string date)
        {
            string[] dates = date.Split('/');
            string da = dates[0] + '/' + dates[1] + '/' + dates[2];
            return removespace(da);
        }
        private static string datereplace(string date)
        {
            string[] a = date.Split('-');
            date = a[1] + "-" + a[0] + "-" + a[2];
            return date;
        }
        protected void btngenerate_Click(object sender, EventArgs e)
        {
            StringBuilder Query = new StringBuilder();
            string fromd = txtdate.Text.Replace('/', '-');

            DateTime fromdate = DateTime.ParseExact(fromd, "dd-MM-yyyy", null).Date;
            string fromdates = datereplace(fromdate.ToString("dd-MM-yyyy"));
            if (txtdate.Text != "")
            {
                Query.Append(" and  ='" + fromdates + "'");
            }
            if (txtparty.Text != "")
            {
                Query.Append(" and  = '" + txtparty.Text + "'");
            }

            if (ddlbranch.SelectedItem.Text != "-Select-")
            {
                Query.Append(" and  = '" + ddlbranch.SelectedItem.Text + "'");
            }

            if (ddltransmode.SelectedItem.Text != "-Select-")
            {
                Query.Append(" and  = '" + ddltransmode.SelectedItem.Text + "'");
            }

            if (ddljobtype.SelectedItem.Text != "-Select-")
            {
                Query.Append(" and  = '" + ddljobtype.SelectedItem.Text + "'");
            }

            string WhereQry = Query.ToString();
            string Qry = " where completed=0 and  1=1";
            Qry = Qry + WhereQry;
            SqlConnection conn = new SqlConnection(strcon);
            conn.Open();


            SqlDataAdapter da = new SqlDataAdapter(Qry, conn);
            DataSet ds = new DataSet();
            da.Fill(ds, "Table");
            if (ds.Tables[0].Rows.Count != 0)
            {
                conn.Close();
                gvPendingJobs.DataSource = ds;
                gvPendingJobs.DataBind();
                gvPendingJobs.Visible = true;
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "JavaScript", "alert('No Data Found');", true);
                gvPendingJobs.Visible = false;
            }
        }
    }
}