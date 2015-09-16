using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Text;
using System.Configuration;
using System.IO;
using ImpexCube.Accounts;

namespace AccountsManagement
{
    public partial class ReceiptReport : System.Web.UI.Page
    {
        Master ms = new Master();
        string strconn = (string)ConfigurationManager.ConnectionStrings["Constr"].ConnectionString;
        private static string ReportExcel;

        string From;
        string To;
        //string GrpName;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack == false)
            {
            //    AccountCr();
            //    AccountDr();
            //    GroupName();
                //Narration();
            }       
        }

       // private void Narration()
        //{
        //    string query = "Select Narration from Receipt";
        //    SqlConnection conn = new SqlConnection(strconn);
        //    conn.Open();
        //    SqlDataAdapter da = new SqlDataAdapter(query, conn);
        //    DataSet ds = new DataSet();
        //    da.Fill(ds, "SQLTABLE");
        //    conn.Close();
        //    ddlNarration.DataSource = ds;
        //    ddlNarration.DataTextField = "Narration";
        //    ddlNarration.DataBind();
        //}

        //private void GroupName()
        //{
        //    string query = "Select GroupCode,GroupName from AccountsGroup";
        //    SqlConnection conn = new SqlConnection(strconn);
        //    conn.Open();
        //    SqlDataAdapter da = new SqlDataAdapter(query, conn);
        //    DataSet ds = new DataSet();
        //    da.Fill(ds, "SQLTABLE");
        //    conn.Close();
        //    ddlGroupName.DataSource = ds;
        //    ddlGroupName.DataTextField = "GroupName";
        //    ddlGroupName.DataValueField = "GroupCode";
        //    ddlGroupName.DataBind();
        //}

        //private void AccountDr()
        //{
        //    DataSet ds = new DataSet();
        //    ds = ms.Account();

        //    ddlAccountDr.DataSource = ds;
        //    ddlAccountDr.DataTextField = "AccountName";
        //    ddlAccountDr.DataValueField = "AccountCode";
        //    ddlAccountDr.DataBind();
        //}

        //private void AccountCr()
        //{
        //    DataSet ds = new DataSet();
        //    ds = ms.Account();

        //    ddlAccountCr.DataSource = ds;
        //    ddlAccountCr.DataTextField = "AccountName";
        //    ddlAccountCr.DataValueField = "AccountCode";
        //    ddlAccountCr.DataBind();
        //}

        //protected void btnGetReport_Click(object sender, EventArgs e)
        //{
        //    StringBuilder Query = new StringBuilder();
        //    string condition = string.Empty;

        //    if (ddlGroupName.SelectedIndex != 0)
        //    {
        //        string GN = ddlGroupName.SelectedValue.ToString();
        //        GetGroupName(GN);
        //        Query.Append(" and [GroupName]='" + GrpName + "'");
        //    }
        //    if (ddlNarration.SelectedIndex != 0)
        //    {
        //        Query.Append(" and [Narration]= '" + ddlNarration.SelectedValue + "'");
        //    }
        //    if (ddlAccountDr.SelectedIndex != 0)
        //    {
        //        Query.Append(" and [Acc_DrCode]= '" + ddlAccountDr.SelectedValue + "'");
        //    }
        //    if (ddlAccountCr.SelectedIndex != 0)
        //    {
        //        Query.Append(" and  [Acc_CrCode]= '" + ddlAccountCr.SelectedValue + "'");
        //    }


        //    string fromdate = txtFrom.Text;
        //    string todate = txtTo.Text;
        //    if (fromdate != "")
        //    {
        //        Query.Append(" and  [VoucherDate]>= '" + frmdatesplit(fromdate) + "'");
        //    }
        //    if (todate != "")
        //    {
        //        Query.Append(" and  [VoucherDate]<= '" + frmdatesplit(todate) + "'");
        //    }

        //    condition = Query.ToString();

        //    try
        //    {
        //        string qry = string.Empty;
        //        qry = "Select Distinct VoucherNo,Convert(Varchar(12),VoucherDate,103) As [Voucher Date],[Acc_DrName],[Acc_CrName],Chq_No,Convert(Varchar(12),Chq_Date,103) As [Cheque Date],Narration,[GroupName],Amount from View_Receipt where  1=1 " + condition + "";
        //        Session["SqlQuery"] = qry;
        //        SqlConnection conn = new SqlConnection(strconn);
        //        conn.Open();
        //        SqlDataAdapter da = new SqlDataAdapter(qry, conn);
        //        DataSet ds = new DataSet();
        //        DataTable dt = new DataTable();
        //        da.Fill(ds, "SQLTABLE");
        //        dt = ds.Tables["SQLTable"];
        //        Session["DataTable"] = dt;
        //        conn.Close();
        //        if (ds.Tables["SQLTABLE"].Rows.Count != 0)
        //        {
        //            gvReceiptReport.DataSource = ds;
        //            gvReceiptReport.DataBind();
        //            gvReceiptReport.Visible = true;
        //        }
        //        else
        //        {
        //            Response.Write("<script>alert('Records Not Found')</script>");
        //            gvReceiptReport.DataSource = null;
        //            gvReceiptReport.DataBind();
        //        }
        //    }
        //    catch (Exception ex)
        //    {

        //    }
        //}

        //private void GetGroupName(string GN)
        //{
        //    string query = "Select GroupName from AccountsGroup where GroupCode = '" + GN + "'";
        //    SqlConnection conn = new SqlConnection(strconn);
        //    conn.Open();
        //    SqlDataAdapter da = new SqlDataAdapter(query, conn);
        //    DataSet ds = new DataSet();
        //    da.Fill(ds, "SQLTABLE");
        //    conn.Close();
        //    if (ds.Tables["SQLTABLE"].Rows.Count != 0)
        //    {
        //        DataRowView row = ds.Tables["SQLTABLE"].DefaultView[0];
        //        GrpName = row["GroupName"].ToString();
        //    }
        //}

        //protected void ddlGroupName_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    string GrCode = ddlGroupName.SelectedValue.ToString();
        //    string query = "Select AccountName,Acc_Group from AccountMaster where Acc_Group = '" + GrCode + "'";
        //    SqlConnection conn = new SqlConnection(strconn);
        //    conn.Open();
        //    SqlDataAdapter da = new SqlDataAdapter(query, conn);
        //    DataSet ds = new DataSet();
        //    da.Fill(ds, "SQLTABLE");
        //    conn.Close();
        //    if (ds.Tables["SQLTABLE"].Rows.Count != 0)
        //    {
        //        ddlAccountCr.DataSource = ds;
        //        ddlAccountCr.DataTextField = "AccountName";
        //        ddlAccountCr.DataValueField = "Acc_Group";
        //        ddlAccountCr.DataBind();
        //    }
        //    else
        //    {
        //        ddlAccountCr.DataSource = null;
        //        ddlAccountCr.DataBind();
        //    }
        //}

        //private string frmdatesplit(string frmdate)
        //{
        //    string[] frmdate1 = frmdate.Split('/');
        //    string frmdate2 = frmdate1[1] + '/' + frmdate1[0] + '/' + frmdate1[2];
        //    return frmdate2;
        //}

        protected void btnExcel_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dt = (DataTable)Session["ReceiptReport"];
                if (dt.Rows.Count != 0)
                {
                    gvReceiptReport.AllowPaging = false;
                    gvReceiptReport.DataSource = dt;
                    gvReceiptReport.DataBind();
                    string na = "ReceiptReport" + ".xls";
                    string ExcelExport = na;

                    Export(ExcelExport, gvReceiptReport);
                }
                else
                {
                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "Key", "alert('No Records Found EXCEL Report Cannot be generated!.');", true);

                }
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

        protected void gvReceiptReport_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (gvReceiptReport.SelectedRow.Cells[1].Text != null)
            {
                Session["VchNo"] = gvReceiptReport.SelectedRow.Cells[1].Text.ToString();
                Response.Redirect("PrintReceiptRegister.aspx");
            }
        }

        protected void btnExit_Click(object sender, EventArgs e)
        {
            Response.Redirect("MainMenu.aspx");
        }

        protected void gvReceiptReport_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            SqlConnection conn = new SqlConnection(strconn);
            conn.Open();
            string qry = "";
            if (ddlStatus.SelectedItem.Text == "Cash")
            {
                qry = "select Distinct VchNo,convert(varchar(10),VoucherDate,103) as VoucherDate,AccountCrName,AccountDrName,AmountDr from View_T_ReceiptDetails where VoucherDate between '" + From + "' and '" + To + "' and  VCH_Type='Receipt'";
            }
            else if (ddlStatus.SelectedItem.Text == "Bank")
            {
                qry = "select Distinct VchNo,convert(varchar(10),VoucherDate,103) as VoucherDate,AccountCrName,AccountDrName,Chq_No,convert(varchar(10),Chq_Date,103) as Chq_Date,AmountDr from View_T_ReceiptDetails where  VoucherDate between '" + From + "' and '" + To + "' and  VCH_Type='Receipt'";
            }
            else if (ddlStatus.SelectedItem.Text == "All")
            {
                qry = "select Distinct VchNo,convert(varchar(10),VoucherDate,103) as VoucherDate,AccountCrName,AccountDrName,Chq_No,convert(varchar(10),Chq_Date,103) as Chq_Date,AmountDr from View_T_ReceiptDetails where VoucherDate between '" + From + "' and '" + To + "' and  VCH_Type='Receipt'";
            }
            SqlDataAdapter da = new SqlDataAdapter(qry, conn);
            DataSet ds = new DataSet();
            da.Fill(ds, "SQLTABLE");
            conn.Close();
            if (ds.Tables["SQLTABLE"].Rows.Count != 0)
            {
                gvReceiptReport.DataSource = ds;
                gvReceiptReport.DataBind();
                gvReceiptReport.PageIndex = e.NewPageIndex;
                gvReceiptReport.DataBind();
            }
            else
            {
                Response.Write("<script>alert('Records Not Found')</script>");
                gvReceiptReport.DataSource = null;
                gvReceiptReport.DataBind();
            }
        }

        protected void gvReceiptReport_Sorting(object sender, GridViewSortEventArgs e)
        {
            string sortingDirection = string.Empty;
            if (dir == SortDirection.Ascending)
            {
                dir = SortDirection.Descending;
                sortingDirection = "Desc";
            }
            else
            {
                dir = SortDirection.Ascending;
                sortingDirection = "Asc";
            }

            DataView sortedView = new DataView(BindGrid());
            sortedView.Sort = e.SortExpression + " " + sortingDirection;
            gvReceiptReport.DataSource = sortedView;
            gvReceiptReport.DataBind();
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

        public SortDirection dir
        {
            get
            {
                if (ViewState["dirState"] == null)
                {
                    ViewState["dirState"] = SortDirection.Ascending;
                }
                return (SortDirection)ViewState["dirState"];
            }
            set
            {
                ViewState["dirState"] = value;
            }
        }

        protected void btnGetReport_Click(object sender, EventArgs e)
        {
            StringBuilder Query = new StringBuilder();
            string condition = string.Empty;

            if (ddlStatus.SelectedIndex != 0)
            {
                string GN = ddlStatus.SelectedValue.ToString();
                Query.Append(" and [GroupName]='" + GN + "'");
            }
            //if (ddlNarration.SelectedIndex != 0)
            //{
            //    Query.Append(" and [Narration]= '" + ddlNarration.SelectedValue + "'");
            //}
            //if (ddlAccountDr.SelectedIndex != 0)
            //{
            //    Query.Append(" and [Account_Dr_Code]= '" + ddlAccountDr.SelectedValue + "'");
            //}
            //if (ddlAccountCr.SelectedIndex != 0)
            //{
            //    Query.Append(" and  [Account_Cr_Code]= '" + ddlAccountCr.SelectedValue + "'");
            //}


            string fromdate = txtFrom.Text;
            string todate = txtTo.Text;
            string FD = txtFrom.Text;
            string TD = txtTo.Text;
            if (FD != "" && TD != "")
            {
                From = frmdatesplit(FD);
                To = frmdatesplit(TD);
            }
            else
            {
                From = "";
                To = "";
            }


            //string key = txtKeyword.Text;
            string tilldate = DateTime.Now.ToString("MM-dd-yyyy");
            if (fromdate != "")
            {
                Query.Append(" and  [VoucherDate]>= '" + frmdatesplit(fromdate) + "'");
            }
            else
            {
                ClassMsg.Show("Please Select From Date");
            }


            if (todate != "")
            {
                Query.Append(" and  [VoucherDate]<= '" + frmdatesplit(todate) + "'");
            }
            else
            {
                ClassMsg.Show("Please Select To Date");
            }
            condition = Query.ToString();
          

            try
            {
                string qry = string.Empty;
                if (ddlStatus.SelectedItem.Text == "Cash")
                {
                    qry = "select Distinct VchNo,convert(varchar(10),VoucherDate,103) as VoucherDate,AccountCrName,AccountDrName,AmountDr from View_T_ReceiptDetails where VoucherDate between '" + From + "' and '" + To + "' and  VCH_Type='Receipt'";
                }
                else if (ddlStatus.SelectedItem.Text == "Bank")
                {
                    qry = "select Distinct VchNo,convert(varchar(10),VoucherDate,103) as VoucherDate,AccountCrName,AccountDrName,Chq_No,convert(varchar(10),Chq_Date,103) as Chq_Date,AmountDr from View_T_ReceiptDetails where  VoucherDate between '" + From + "' and '" + To + "' and  VCH_Type='Receipt'";
                }
                else if (ddlStatus.SelectedItem.Text == "All")
                {
                    qry = "select Distinct VchNo,convert(varchar(10),VoucherDate,103) as VoucherDate,AccountCrName,AccountDrName,Chq_No,convert(varchar(10),Chq_Date,103) as Chq_Date,AmountDr from View_T_ReceiptDetails where VoucherDate between '" + From + "' and '" + To + "' and  VCH_Type='Receipt'";
                }
                Session["SqlQuery"] = qry;
                SqlConnection conn = new SqlConnection(strconn);
                conn.Open();
                SqlDataAdapter da = new SqlDataAdapter(qry, conn);
                DataSet ds = new DataSet();
                da.Fill(ds, "SQLTABLE");
                DataTable dt = ds.Tables["SQLTABLE"];
                Session["ReceiptReport"] = dt;
                conn.Close();
                if (ds.Tables["SQLTABLE"].Rows.Count != 0)
                {
                    gvReceiptReport.DataSource = ds;
                    gvReceiptReport.DataBind();
                    gvReceiptReport.Visible = true;
                }
                else
                {
                    Response.Write("<script>alert('Records Not Found')</script>");
                    gvReceiptReport.DataSource = null;
                    gvReceiptReport.DataBind();
                }
            }
            catch (Exception ex)
            {

            }
        }

        private string frmdatesplit(string frmdate)
        {
            string[] frmdate1 = frmdate.Split('/');
            string frmdate2 = frmdate1[1] + '/' + frmdate1[0] + '/' + frmdate1[2];
            return frmdate2;
        }


        protected void ddlStatus_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

    }
}