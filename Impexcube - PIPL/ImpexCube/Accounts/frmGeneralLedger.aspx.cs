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

namespace AccountsManagement
{
    public partial class frmGeneralLedger : System.Web.UI.Page
    {
        string strconn = (string)ConfigurationManager.ConnectionStrings["Constr"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            lblOBalance.Visible = false;
            lblClosing.Visible = false;

            if (IsPostBack == false)
            {
                
            }
        }

        protected void btnGetReport_Click(object sender, EventArgs e)
        {
            StringBuilder Query = new StringBuilder();
            string condition = string.Empty;

            string LedgerName = txtLedgerName.Text;
            string fromdate = txtFrom.Text;
            string todate = txtTo.Text;
            if (fromdate != "")
            {
                Query.Append(" and  [invoiceDate]>= '" + frmdatesplit(fromdate) + "'");
                OpBalance();
                ClBalance();
            }
            if (todate != "")
            {
                Query.Append(" and  [invoiceDate]<= '" + frmdatesplit(todate) + "'");
                //ClBalance();
            }
            if (LedgerName != "")
            {
                Query.Append(" and [AccName] = '" + LedgerName + "' ");
            }

            condition = Query.ToString();
            SqlConnection conn = new SqlConnection(strconn);
            //SqlConnection conn1 = new SqlConnection(strconn);
            //try
            //{
            string qry = string.Empty;
            qry = "Select Distinct InvoiceNo,Convert(Varchar(12),invoiceDate,103) As [Invoice Date],InvoiceType, Net_Total as Amount,drcr from View_GeneralLedger where invoiceDate between '" + frmdatesplit(fromdate) + "' and '" + frmdatesplit(todate) + "' and AccName = '" + LedgerName + "'  ";// where 1=1 " + condition + " ";
            Session["SqlQuery"] = qry;
            conn.Open();
            SqlDataAdapter da = new SqlDataAdapter(qry, conn);
            DataSet ds = new DataSet();
            da.Fill(ds, "SQLTABLE");
            DataTable dt = ds.Tables["SQLTABLE"];
            conn.Close();
            dt.Columns.Add();
            dt.Columns.Add();
            dt.Columns.Add();
            dt.Columns.Add();
            dt.Columns[5].ColumnName = "opening";
            dt.Columns[6].ColumnName = "Dr/Cr-op";
            dt.Columns[7].ColumnName = "closing";
            dt.Columns[8].ColumnName = "Dr/Cr-cl";

            Session["LedgerReport"] = dt;
            double opbal = 0.00;
            double opening = 0.00;
            string drcr = "";
            opbal = Convert.ToDouble(lblOBalance.Text);
            if (opbal > 0)
            {
                opening = opbal;
                drcr = "DR";
            }
            else
            {
                opening = (-1) * (opbal);
                drcr = "CR";
            }

            if (dt.Rows.Count == 0)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Records not found');", true);
            }
            else
            {
                dt.Rows[0]["opening"] = opbal;
                dt.Rows[0]["Dr/Cr-op"] = drcr;


                int i = 0;
                foreach (DataRow row in dt.Rows)
                {
                    if (i == 0)
                    {
                        if (dt.Rows[0]["Dr/Cr-op"].ToString() == "DR" && dt.Rows[0]["drcr"].ToString() == "Dr")
                        {
                            dt.Rows[0]["closing"] = Convert.ToDouble(dt.Rows[0]["Opening"]) + Convert.ToDouble(dt.Rows[0]["Amount"]);

                        }
                        else if (dt.Rows[0]["Dr/Cr-op"].ToString() == "DR" && dt.Rows[0]["drcr"].ToString() == "Cr")
                        {
                            dt.Rows[0]["closing"] = Convert.ToDouble(dt.Rows[0]["Opening"]) - Convert.ToDouble(dt.Rows[0]["Amount"]);
                        }
                        else if (dt.Rows[0]["Dr/Cr-op"].ToString() == "CR" && dt.Rows[0]["drcr"].ToString() == "Cr")
                        {
                            dt.Rows[0]["closing"] = Convert.ToDouble(dt.Rows[0]["Opening"]) + Convert.ToDouble(dt.Rows[0]["Amount"]);
                        }

                        else if (dt.Rows[0]["Dr/Cr-op"].ToString() == "CR" && dt.Rows[0]["drcr"].ToString() == "Dr")
                        {
                            dt.Rows[0]["closing"] = Convert.ToDouble(dt.Rows[0]["Amount"]) - Convert.ToDouble(dt.Rows[0]["Opening"]);

                        }
                        if (Convert.ToDouble(dt.Rows[0]["closing"].ToString()) > 0)
                        {
                            dt.Rows[0]["closing"] = dt.Rows[0]["closing"].ToString();
                            dt.Rows[0]["Dr/Cr-cl"] = "DR";
                        }
                        else
                        {
                            dt.Rows[0]["closing"] = ((-1) * Convert.ToDouble(dt.Rows[0]["closing"].ToString()));
                            dt.Rows[0]["Dr/Cr-cl"] = "CR";
                        }
                    }
                    else
                    {
                        dt.Rows[i]["opening"] = dt.Rows[i - 1]["closing"];
                        dt.Rows[i]["Dr/Cr-op"] = dt.Rows[i - 1]["Dr/Cr-cl"];
                        if (dt.Rows[i]["Opening"].ToString() != "" && dt.Rows[i]["Amount"].ToString() != "")
                        {

                            if (dt.Rows[i]["Dr/Cr-op"].ToString() == "DR" && dt.Rows[i]["drcr"].ToString() == "Dr")
                            {
                                dt.Rows[i]["closing"] = Convert.ToDouble(dt.Rows[i]["Opening"]) + Convert.ToDouble(dt.Rows[i]["Amount"]);

                            }
                            else if (dt.Rows[i]["Dr/Cr-op"].ToString() == "DR" && dt.Rows[i]["drcr"].ToString() == "Cr")
                            {
                                dt.Rows[i]["closing"] = Convert.ToDouble(dt.Rows[i]["Opening"]) - Convert.ToDouble(dt.Rows[i]["Amount"]);
                            }
                            else if (dt.Rows[i]["Dr/Cr-op"].ToString() == "CR" && dt.Rows[i]["drcr"].ToString() == "Cr")
                            {
                                dt.Rows[i]["closing"] = Convert.ToDouble(dt.Rows[i]["Opening"]) + Convert.ToDouble(dt.Rows[i]["Amount"]);
                            }

                            else if (dt.Rows[i]["Dr/Cr-op"].ToString() == "CR" && dt.Rows[i]["drcr"].ToString() == "Dr")
                            {
                                dt.Rows[i]["closing"] = Convert.ToDouble(dt.Rows[i]["Amount"]) - Convert.ToDouble(dt.Rows[i]["Opening"]);

                            }
                            if (Convert.ToDouble(dt.Rows[i]["closing"].ToString()) > 0)
                            {
                                dt.Rows[i]["closing"] = dt.Rows[i]["closing"].ToString();
                                dt.Rows[i]["Dr/Cr-cl"] = "DR";
                            }
                            else
                            {
                                dt.Rows[i]["closing"] = ((-1) * Convert.ToDouble(dt.Rows[i]["closing"].ToString()));
                                dt.Rows[i]["Dr/Cr-cl"] = "CR";
                            }

                        }
                        
                    }
                    i++;
                }
                    dt.AcceptChanges();
                    gvGeneralLedgerReport.DataSource = dt;
                    gvGeneralLedgerReport.DataBind();
               
            }
            
        }
        public void OpBalance()
        {
            string LedgerName = txtLedgerName.Text;
            string fromdate = txtFrom.Text;
            string todate = txtTo.Text;
            SqlConnection conn = new SqlConnection(strconn);
            string qry = string.Empty;
            qry = "Select Sum(Net_Total) As Amount from View_GeneralLedger where invoiceDate <= '" + frmdatesplit(fromdate) + "'  and AccName = '" + LedgerName + "'  and drcr='Dr'";
            Session["SqlQuery"] = qry;
            conn.Open();
            SqlDataAdapter da = new SqlDataAdapter(qry, conn);
            DataSet ds = new DataSet();
            da.Fill(ds, "SQLTABLE");
            DataTable dt = ds.Tables["SQLTABLE"];
            conn.Close();
            DataRowView rw = ds.Tables["SQLTABLE"].DefaultView[0];
            if (ds.Tables[0].Rows.Count >1)
            {
                lblOBalance.Text = rw["Amount"].ToString();
            }
           //lblOBalance.Visible = true;
           //lblClosing.Visible = true;
        }
        public void ClBalance()
        {
            string LedgerName = txtLedgerName.Text;
            string fromdate = txtFrom.Text;
            string todate = txtTo.Text;
            SqlConnection conn = new SqlConnection(strconn);
            string qry = string.Empty;
            qry = "Select Sum(Net_Total) As Amount from View_GeneralLedger where invoiceDate <= '" + frmdatesplit(fromdate) + "'  and AccName = '" + LedgerName + "' and  drcr='Cr'";
            Session["SqlQuery"] = qry;
            conn.Open();
            SqlDataAdapter da = new SqlDataAdapter(qry, conn);
            DataSet ds = new DataSet();
            da.Fill(ds, "SQLTABLE");
            DataTable dt = ds.Tables["SQLTABLE"];
            conn.Close();
            DataRowView rw = ds.Tables["SQLTABLE"].DefaultView[0];
            if (ds.Tables[0].Rows.Count >1)
            {
                lblClosing.Text = rw["Amount"].ToString();
            }
            lblOBalance.Text = (Convert.ToDouble(lblOBalance.Text) - Convert.ToDouble(lblClosing.Text)).ToString();
            //lblClosingBalance .Visible = true;
           // lblClosing.Visible = true;
           
        }
        private string frmdatesplit(string frmdate)
        {
            string[] frmdate1 = frmdate.Split('/');
            string frmdate2 = frmdate1[2] + '/' + frmdate1[1] + '/' + frmdate1[0];
            return frmdate2;
        }

        protected void btnExcel_Click(object sender, EventArgs e)
        {

        }
        public DataTable BindGrid()
        {
            string sqlQuery = (string)Session["SqlQuery"];

            SqlConnection conn = new SqlConnection(strconn);
            conn.Open();
            SqlDataAdapter da = new SqlDataAdapter(sqlQuery, conn);
            DataTable dt = new DataTable();
            da.Fill(dt);
            conn.Close();
            return dt;
        }

        protected void btnExit_Click(object sender, EventArgs e)
        
            {
                Response.Redirect("MainMenu.aspx");
            }

        protected void gvGeneralLedgerReport_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void gvGeneralLedgerReport_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            string fromdate = txtFrom.Text;
            string todate = txtTo.Text;
            string LedgerName = txtLedgerName.Text;
            SqlConnection conn = new SqlConnection(strconn);
            string qry = string.Empty;
            qry = "Select Distinct InvoiceNo,Convert(Varchar(12),invoiceDate,103) As [Invoice Date],InvoiceType, Net_Total as Amount,drcr from View_GeneralLedger where invoiceDate between '" + frmdatesplit(fromdate) + "' and '" + frmdatesplit(todate) + "' and AccName = '" + LedgerName + "'  ";
            conn.Open();
            SqlDataAdapter da = new SqlDataAdapter(qry, conn);
            DataSet ds = new DataSet();
            da.Fill(ds, "SQLTABLE");
            DataTable dt = ds.Tables["SQLTABLE"];
            conn.Close();
            dt.Columns.Add();
            dt.Columns.Add();
            dt.Columns.Add();
            dt.Columns.Add();
            dt.Columns[5].ColumnName = "opening";
            dt.Columns[6].ColumnName = "Dr/Cr-op";
            dt.Columns[7].ColumnName = "closing";
            dt.Columns[8].ColumnName = "Dr/Cr-cl";
            Session["LedgerReport"] = dt;
            double opbal = 0.00;
            double opening = 0.00;
            string drcr = "";
            opbal = Convert.ToDouble(lblOBalance.Text);
            if (opbal > 0)
            {

                opening = opbal;
                drcr = "DR";
            }
            else
            {
                opening = (-1) * (opbal);
                drcr = "CR";
            }

            if (dt.Rows.Count == 0)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Records not found');", true);
            }
            else
            {
                dt.Rows[0]["opening"] = opbal;
                dt.Rows[0]["Dr/Cr-op"] = drcr;


                int i = 0;
                foreach (DataRow row in dt.Rows)
                {
                    if (i == 0)
                    {
                        if (dt.Rows[0]["Dr/Cr-op"].ToString() == "DR" && dt.Rows[0]["drcr"].ToString() == "Dr")
                        {
                            dt.Rows[0]["closing"] = Convert.ToDouble(dt.Rows[0]["Opening"]) + Convert.ToDouble(dt.Rows[0]["Amount"]);

                        }
                        else if (dt.Rows[0]["Dr/Cr-op"].ToString() == "DR" && dt.Rows[0]["drcr"].ToString() == "Cr")
                        {
                            dt.Rows[0]["closing"] = Convert.ToDouble(dt.Rows[0]["Opening"]) - Convert.ToDouble(dt.Rows[0]["Amount"]);
                        }
                        else if (dt.Rows[0]["Dr/Cr-op"].ToString() == "CR" && dt.Rows[0]["drcr"].ToString() == "Cr")
                        {
                            dt.Rows[0]["closing"] = Convert.ToDouble(dt.Rows[0]["Opening"]) + Convert.ToDouble(dt.Rows[0]["Amount"]);
                        }

                        else if (dt.Rows[0]["Dr/Cr-op"].ToString() == "CR" && dt.Rows[0]["drcr"].ToString() == "Dr")
                        {
                            dt.Rows[0]["closing"] = Convert.ToDouble(dt.Rows[0]["Amount"]) - Convert.ToDouble(dt.Rows[0]["Opening"]);

                        }
                        if (Convert.ToDouble(dt.Rows[0]["closing"].ToString()) > 0)
                        {
                            dt.Rows[0]["closing"] = dt.Rows[0]["closing"].ToString();
                            dt.Rows[0]["Dr/Cr-cl"] = "DR";
                        }
                        else
                        {
                            dt.Rows[0]["closing"] = ((-1) * Convert.ToDouble(dt.Rows[0]["closing"].ToString()));
                            dt.Rows[0]["Dr/Cr-cl"] = "CR";
                        }
                    }
                    else
                    {
                        dt.Rows[i]["opening"] = dt.Rows[i - 1]["closing"];
                        dt.Rows[i]["Dr/Cr-op"] = dt.Rows[i - 1]["Dr/Cr-cl"];
                        if (dt.Rows[i]["Opening"].ToString() != "" && dt.Rows[i]["Amount"].ToString() != "")
                        {

                            if (dt.Rows[i]["Dr/Cr-op"].ToString() == "DR" && dt.Rows[i]["drcr"].ToString() == "Dr")
                            {
                                dt.Rows[i]["closing"] = Convert.ToDouble(dt.Rows[i]["Opening"]) + Convert.ToDouble(dt.Rows[i]["Amount"]);

                            }
                            else if (dt.Rows[i]["Dr/Cr-op"].ToString() == "DR" && dt.Rows[i]["drcr"].ToString() == "Cr")
                            {
                                dt.Rows[i]["closing"] = Convert.ToDouble(dt.Rows[i]["Opening"]) - Convert.ToDouble(dt.Rows[i]["Amount"]);
                            }
                            else if (dt.Rows[i]["Dr/Cr-op"].ToString() == "CR" && dt.Rows[i]["drcr"].ToString() == "Cr")
                            {
                                dt.Rows[i]["closing"] = Convert.ToDouble(dt.Rows[i]["Opening"]) + Convert.ToDouble(dt.Rows[i]["Amount"]);
                            }

                            else if (dt.Rows[i]["Dr/Cr-op"].ToString() == "CR" && dt.Rows[i]["drcr"].ToString() == "Dr")
                            {
                                dt.Rows[i]["closing"] = Convert.ToDouble(dt.Rows[i]["Amount"]) - Convert.ToDouble(dt.Rows[i]["Opening"]);

                            }
                            if (Convert.ToDouble(dt.Rows[i]["closing"].ToString()) > 0)
                            {
                                dt.Rows[i]["closing"] = dt.Rows[i]["closing"].ToString();
                                dt.Rows[i]["Dr/Cr-cl"] = "DR";
                            }
                            else
                            {
                                dt.Rows[i]["closing"] = ((-1) * Convert.ToDouble(dt.Rows[i]["closing"].ToString()));
                                dt.Rows[i]["Dr/Cr-cl"] = "CR";
                            }

                        }

                    }
                    i++;
                }
                if (ds.Tables["SQLTABLE"].Rows.Count != 0)
                {
                    gvGeneralLedgerReport.DataSource = ds;
                    gvGeneralLedgerReport.DataBind();
                    gvGeneralLedgerReport.PageIndex = e.NewPageIndex;
                    gvGeneralLedgerReport.DataBind();
                }
                else
                {
                    Response.Write("<script>alert('Records Not Found')</script>");
                    gvGeneralLedgerReport.DataSource = null;
                    gvGeneralLedgerReport.DataBind();
                }
            }



            //public  SqlConnection conn { get; set; }}

        }
    }
}