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
using System.Text;


namespace ImpexCube.OPReport
{
    public partial class frmInbondExbondReport : System.Web.UI.Page
    {
        string strcon = (string)ConfigurationManager.ConnectionStrings["Constr"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            getbranch();
        }

        public void getbranch()
        {
            SqlConnection conn = new SqlConnection(strcon);
            conn.Open();
            string qry = "";
            SqlDataAdapter da = new SqlDataAdapter(qry, conn);
            DataSet ds = new DataSet();
            da.Fill(ds, "SQLTABLE");
            ddlbranch.DataSource = ds;
            ddlbranch.DataTextField = "";
            ddlbranch.DataValueField = "";
            ddlbranch.DataBind();
            ddlbranch.Items.Insert(0, new ListItem("~Select~", "0"));
        }
        protected void gvInvoiceExport_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {
                e.Row.HorizontalAlign = HorizontalAlign.Center;
                GridViewRow gvrow = new GridViewRow(3, 5, DataControlRowType.Header, DataControlRowState.Insert);
                TableCell cell0 = new TableCell();
                cell0.Text = "Inbond Register";
                cell0.HorizontalAlign = HorizontalAlign.Center;

                cell0.RowSpan = 3;
                gvrow.Cells.Add(cell0);
                gvInbondExbond.Controls[0].Controls.AddAt(0, gvrow);

                e.Row.HorizontalAlign = HorizontalAlign.Center;
                GridViewRow gvrow1 = new GridViewRow(6, 10, DataControlRowType.Header, DataControlRowState.Insert);
                TableCell cell1 = new TableCell();
                cell1.Text = "Exbond Register";
                cell1.HorizontalAlign = HorizontalAlign.Center;

                cell1.RowSpan = 5;
                gvrow.Cells.Add(cell1);
                gvInbondExbond.Controls[0].Controls.AddAt(0, gvrow);
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
            string fromd = txtDate.Text.Replace('/', '-');
            
            DateTime fromdate = DateTime.ParseExact(fromd, "dd-MM-yyyy", null).Date;                        
            string fromdates = datereplace(fromdate.ToString("dd-MM-yyyy"));
            if (txtDate.Text != "")
            {
                Query.Append(" and  ='" + fromdates + "'");
            }
            if (txtimporter.Text != "")
            {
                Query.Append(" and  = '" + txtimporter.Text + "'");
            }

            if (txtjobno.Text != "")
            {
                Query.Append(" and  = '" + txtjobno.Text + "'");
            }
            if (ddlbranch.SelectedItem.Text != "-Select-")
            {
                Query.Append(" and  = '" + ddlbranch.SelectedItem.Text + "'");
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
                gvInbondExbond.DataSource = ds;
                gvInbondExbond.DataBind();
                gvInbondExbond.Visible = true;
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "JavaScript", "alert('No Data Found');", true);
                gvInbondExbond.Visible = false;
            }
        }
    }
}