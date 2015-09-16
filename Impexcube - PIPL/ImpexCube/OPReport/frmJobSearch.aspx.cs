using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using System.Drawing;
using System.Data.SqlClient;
using VTS.ImpexCube.Data;
using System.Text;
using VTS.ImpexCube.Business;
namespace ImpexCube.OPReport
{
    public partial class frmJobSearch : System.Web.UI.Page
    {
        string strcon = (string)ConfigurationManager.ConnectionStrings["Constr"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                JobSearch();
            }
        }

        public void JobSearch()
        {
            SqlConnection conn = new SqlConnection(strcon);
            conn.Open();
            string qry = "select KeyCode,KeyName from M_SearchMaster";
            SqlDataAdapter da = new SqlDataAdapter(qry, conn);
            DataSet ds = new DataSet();
            da.Fill(ds, "SQLTABLE");
            ddlJobSearch.DataSource = ds;
            ddlJobSearch.DataTextField = "KeyName";
            ddlJobSearch.DataValueField = "KeyCode";
            ddlJobSearch.DataBind();
            ddlJobSearch.Items.Insert(0, new ListItem("~Select~", "0"));
        }

        protected void ddlJobSearch_SelectedIndexChanged(object sender, EventArgs e)
        {
            lbljobsearch.Text = ddlJobSearch.SelectedItem.Text;
            if (ddlJobSearch.SelectedValue != "6")
            {
                Panel1.Visible = false;
               }
            else
            {
                Panel1.Visible = true;
            }
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
            string fromd = txtfrom.Text.Replace('/', '-');
            string tod = txtto.Text.Replace('/', '-');
            DateTime fromdate = DateTime.ParseExact(fromd, "dd-MM-yyyy", null).Date;
            DateTime todate = DateTime.ParseExact(tod, "dd-MM-yyyy", null).Date;
            string fromdates = datereplace(fromdate.ToString("dd-MM-yyyy"));
            string todates = datereplace(todate.ToString("dd-MM-yyyy"));

            if (txtfrom.Text != "")
            {
                Query.Append(" and  ='" + fromdates + "'");
            }
            if (txtto.Text != "")
            {
                Query.Append(" and  ='" + todates + "'");
            }
            if (ddlJobSearch.SelectedItem.Text != "")
            {
                Query.Append(" and  = '" + ddlJobSearch.SelectedItem.Text + "'");
            }

            if (txtjobsearch.Text != "")
            {
                Query.Append(" and ='" + txtjobsearch.Text + "'");
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
                gvJobSearch.DataSource = ds;
                gvJobSearch.DataBind();
                gvJobSearch.Visible = true;
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "JavaScript", "alert('No Data Found');", true);
                gvJobSearch.Visible = false;
            }
        }
    }
}