using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.IO;

namespace AccountsManagement
{
    public partial class DayBook : System.Web.UI.Page
    {
        string strconn = (string)ConfigurationManager.ConnectionStrings["Constr"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            lblReceipt.Visible = false;
            lblPayment.Visible = false;
            lblSales.Visible = false;
            lblJournal.Visible = false;

        }
        protected void Sales_Report()
        {
            string Date = txtDate.Text;
            SqlConnection conn = new SqlConnection(strconn);
            conn.Open();
            string qry = string.Empty;
            qry = "select Distinct InvoiceNo,convert(varchar(10),invoiceDate,103) as VoucherDate,AccName,Net_Total from View_GeneralLedger where invoiceDate= '" + datesplit1(Date) + "'";
            
            SqlDataAdapter da = new SqlDataAdapter(qry, conn);
            DataSet ds = new DataSet();
            da.Fill(ds, "SQLTABLE");
            DataTable dt = ds.Tables["SQLTABLE"];
            Session["Sales"] = dt;
            conn.Close();
            Session["SalesRowCount"] = ds.Tables["SQLTABLE"].Rows.Count;
            if (ds.Tables["SQLTABLE"].Rows.Count != 0)
            {
                gvDayBook_Sales.DataSource = ds;
                gvDayBook_Sales.DataBind();
                gvDayBook_Sales.Visible = true;
                lblSales.Visible = true;
            }
        
        }
        private string datesplit1(string Date)
        {
            string[] date1 = Date.Split('/');
            string date2 = date1[2] + '-' + date1[1] + '-' + date1[0];
            return date2;
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
        protected void Payment_Report()
        {
            string Date = txtDate.Text;
            SqlConnection conn = new SqlConnection(strconn);
            conn.Open();
            string qry = string.Empty;
            qry = "select Distinct VchNo,convert(varchar(12),VoucherDate,103) as VoucherDate,AccountCrName,AccountDrName,AmountDr from View_T_PaymentDetails where VoucherDate= '" + datesplit(Date) + "'";

            SqlDataAdapter da = new SqlDataAdapter(qry, conn);
            DataSet ds = new DataSet();
            da.Fill(ds, "SQLTABLE");
            DataTable dt = ds.Tables["SQLTABLE"];
            Session["Payment"] = dt;
            conn.Close();
            Session["PaymentRowCount"] = ds.Tables["SQLTABLE"].Rows.Count;
            if (ds.Tables["SQLTABLE"].Rows.Count != 0)
            {
                gvDayBook_Payment.DataSource = ds;
                gvDayBook_Payment.DataBind();
                gvDayBook_Payment.Visible = true;
                lblPayment.Visible = true;
            }
            //else
            //{
            //    Response.Write("<script>alert('Records Not Found')</script>");
            //    gvDayReport_Sales.DataSource = null;
            //    gvDayReport_Sales.DataBind();
            //}
        }
       
        public DataTable BindGrid_Payment()
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
        protected void Receipt_Report()
        {
            string Date = txtDate.Text;
            SqlConnection conn = new SqlConnection(strconn);
            conn.Open();
            string qry = string.Empty;
            qry = "select Distinct VchNo,convert(varchar(10),VoucherDate,103) as VoucherDate,AccountCrName,AccountDrName,AmountDr from View_T_ReceiptDetails where VoucherDate= '" + datesplit(Date) + "'";

            SqlDataAdapter da = new SqlDataAdapter(qry, conn);
            DataSet ds = new DataSet();
            da.Fill(ds, "SQLTABLE");
            DataTable dt = ds.Tables["SQLTABLE"];
            Session["Receipt"] = dt;
            conn.Close();
            Session["ReceiptRowCount"] = ds.Tables["SQLTABLE"].Rows.Count;
            if (ds.Tables["SQLTABLE"].Rows.Count != 0)
            {
                gvDayBook_Receipt.DataSource = ds;
                gvDayBook_Receipt.DataBind();
                gvDayBook_Receipt.Visible = true;
                lblReceipt.Visible=true;

            }
            //else
            //{
            //    Response.Write("<script>alert('Records Not Found')</script>");
            //    gvDayReport_Sales.DataSource = null;
            //    gvDayReport_Sales.DataBind();
            //}
        }
        public DataTable BindGrid_Receipt()
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
        protected void Journal_Report()
        {
            string Date = txtDate.Text;
            SqlConnection conn = new SqlConnection(strconn);
            conn.Open();
            string qry = string.Empty;
            qry = "select Distinct VchNo,convert(varchar(10),VoucherDate,103) as VoucherDate,AccountCode,Amount from View_JournalDetails where VoucherDate= '" + datesplit(Date) + "'";

            SqlDataAdapter da = new SqlDataAdapter(qry, conn);
            DataSet ds = new DataSet();
            da.Fill(ds, "SQLTABLE");
            DataTable dt = ds.Tables["SQLTABLE"];
            Session["Journal"] = dt;
            conn.Close();
            Session["JournalRowCount"] = ds.Tables["SQLTABLE"].Rows.Count;
            if (ds.Tables["SQLTABLE"].Rows.Count != 0)
            {
                gvDayBook_Journal.DataSource = ds;
                gvDayBook_Journal.DataBind();
                gvDayBook_Journal.Visible = true;
                lblJournal.Visible=true;
            }
            //else
            //{
            //    Response.Write("<script>alert('Records Not Found')</script>");
            //    gvDayReport_Sales.DataSource = null;
            //    gvDayReport_Sales.DataBind();
            //}
        }
        public DataTable BindGrid_Journal()
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
        private string datesplit(string Date)
        {
            string[] date1 = Date.Split('/');
            string date2 = date1[1] + '/' + date1[0] + '/' + date1[2];
            return date2;
        }

protected void btnGetRport_Click(object sender, EventArgs e)
{
    Sales_Report();
    Payment_Report();
    Receipt_Report();
    Journal_Report();
    int rc1 = (int)Session["SalesRowCount"];
    int rc2 = (int)Session["PaymentRowCount"];
    int rc3 = (int)Session["ReceiptRowCount"];
    int rc4 = (int)Session["JournalRowCount"];
    int rc = rc1 + rc2 + rc3 + rc4;
    if (rc == 0)
    {
        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Records not found');", true);
    }
}
protected void btnExportExcel_Click(object sender, EventArgs e)
{
    try
    {


        Response.Clear();
        Response.Buffer = true;
        Response.AddHeader("content-disposition", "attachment;filename= Report.xls");
        Response.Charset = "";
        Response.ContentType = "application/vnd.ms-excel";
        System.IO.StringWriter stringWrite = new System.IO.StringWriter();
        System.Web.UI.HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);
        Panel2.RenderControl(htmlWrite);
        Response.Output.Write(stringWrite.ToString());
        Response.Flush();
        Response.End();


    }
    catch (Exception)
    {

    }

}

private void Export(string fileName, GridView gv)
{
    gv.HeaderRow.Cells[0].Visible = false;
    HttpContext.Current.Response.Clear();
    HttpContext.Current.Response.AddHeader("content-disposition", string.Format("attachment; filename={0}", fileName));
    HttpContext.Current.Response.ContentType = "application/ms-excel";

    using (StringWriter sw = new StringWriter())
    {
        using (HtmlTextWriter htw = new HtmlTextWriter(sw))
        {
            //  Create a form to contain the grid
            Table table = new Table();
            table.GridLines = gv.GridLines;

            //  add the header row to the table
            if (gv.HeaderRow != null)
            {
                PrepareControlForExport(gv.HeaderRow);
                table.Rows.Add(gv.HeaderRow);
            }

            //  add each of the data rows to the table
            foreach (GridViewRow row in gv.Rows)
            {
                PrepareControlForExport(row);
                row.Cells[0].Visible = false;
                table.Rows.Add(row);
            }

            //  add the footer row to the table
            if (gv.FooterRow != null)
            {
                PrepareControlForExport(gv.FooterRow);
                table.Rows.Add(gv.FooterRow);
            }

            //  render the table into the htmlwriter
            table.RenderControl(htw);

            //  render the htmlwriter into the response
            HttpContext.Current.Response.Write(sw.ToString());
            HttpContext.Current.Response.End();
        }
    }
}

private void PrepareControlForExport(Control control)
{
    for (int i = 0; i < control.Controls.Count; i++)
    {
        Control current = control.Controls[i];
        if (current is LinkButton)
        {
            control.Controls.Remove(current);
            control.Controls.AddAt(i, new LiteralControl((current as LinkButton).Text));
        }
        else if (current is ImageButton)
        {
            control.Controls.Remove(current);
            //control.Controls.AddAt(i, new LiteralControl((current as ImageButton).AlternateText));
        }
        else if (current is HyperLink)
        {
            control.Controls.Remove(current);
            control.Controls.AddAt(i, new LiteralControl((current as HyperLink).Text));
        }
        else if (current is DropDownList)
        {
            control.Controls.Remove(current);
            control.Controls.AddAt(i, new LiteralControl((current as DropDownList).SelectedItem.Text));
        }
        else if (current is CheckBox)
        {
            control.Controls.Remove(current);
            control.Controls.AddAt(i, new LiteralControl((current as CheckBox).Checked ? "True" : "False"));
        }

        if (current.HasControls())
        {
            PrepareControlForExport(current);
        }
    }
}

protected void gvDayBook_Payment_DataBound(object sender, EventArgs e)
{
    double TotalAmt = 0;
    int i = 0;
    //gvDayBook_Payment.FooterRow.Cells[0].Text = "Payment Report";
    foreach (GridViewRow gv in gvDayBook_Payment.Rows)
    {
        string amt = gvDayBook_Payment.Rows[i].Cells[4].Text;
        TotalAmt = TotalAmt + Convert.ToDouble(amt);
        i++;
    }
    if (gvDayBook_Payment.Rows.Count != 0)
    {
        gvDayBook_Payment.FooterRow.Cells[3].Text = "Total Amounts";
        gvDayBook_Payment.FooterRow.Cells[4].Text = Convert.ToString(TotalAmt);
    }
}

protected void gvDayBook_Sales_DataBound(object sender, EventArgs e)
{
    double TotalAmt = 0;
    int i = 0;
    //gvDayBook_Sales.FooterRow.Cells[0].Text = "Sales Report";
    foreach (GridViewRow gv in gvDayBook_Sales.Rows)
    {
        string amt = gvDayBook_Sales.Rows[i].Cells[3].Text;
        TotalAmt = TotalAmt + Convert.ToDouble(amt);
        i++;
    }
    if (gvDayBook_Sales.Rows.Count != 0)
    {
        gvDayBook_Sales.FooterRow.Cells[2].Text = "Total Amounts";
        gvDayBook_Sales.FooterRow.Cells[3].Text = Convert.ToString(TotalAmt);
    }
}

protected void gvDayBook_Receipt_DataBound(object sender, EventArgs e)
{
    double TotalAmt = 0;
    int i = 0;
    //gvDayBook_Receipt.FooterRow.Cells[0].Text = "Receipt Report";
    foreach (GridViewRow gv in gvDayBook_Receipt.Rows)
    {
        string amt = gvDayBook_Receipt.Rows[i].Cells[4].Text;
        TotalAmt = TotalAmt + Convert.ToDouble(amt);
        i++;
    }
    if (gvDayBook_Receipt.Rows.Count != 0)
    {
        gvDayBook_Receipt.FooterRow.Cells[3].Text = "Total Amounts";
        gvDayBook_Receipt.FooterRow.Cells[4].Text = Convert.ToString(TotalAmt);
    }
}

protected void gvDayBook_Journal_DataBound(object sender, EventArgs e)
{
    double TotalAmt = 0;
    int i = 0;
    //gvDayBook_Journal.FooterRow.Cells[0].Text = "Journal Report";
    foreach (GridViewRow gv in gvDayBook_Journal.Rows)
    {
        string amt = gvDayBook_Journal.Rows[i].Cells[3].Text;
        TotalAmt = TotalAmt + Convert.ToDouble(amt);
        i++;
    }
    if (gvDayBook_Journal.Rows.Count != 0)
    {
        gvDayBook_Journal.FooterRow.Cells[2].Text = "Total Amounts";
        gvDayBook_Journal.FooterRow.Cells[3].Text = Convert.ToString(TotalAmt);
    }
}

protected void btnExit_Click(object sender, EventArgs e)
{
    Response.Redirect("MainMenu.aspx");
}
public override void VerifyRenderingInServerForm(Control control)
{
    return;
}

    }
}