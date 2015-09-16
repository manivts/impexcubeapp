using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml.Linq;
using System.Data.SqlClient;

namespace ImpexCube.Accounts
{
    public partial class ReceiptDetails : System.Web.UI.Page
    {
        string strconn = (string)ConfigurationManager.ConnectionStrings["Constr"].ConnectionString;
        string sqlQuery;
        string fDate="";
        string tDate="";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                GridLoad();
                //string dates = DateTime.Now.ToString("dd/MM/yyyy");
                txtFrom.Text = string.Empty;
                txtTo.Text = string.Empty;
                txtKeyword.Text = string.Empty;

            }   
        }

        private void GridLoad()
        {
            sqlQuery = "Select Distinct VoucherNo As [Voucher No],Convert(Varchar(12),VoucherDate,103) As [Voucher Date],AccountDrName As [Account Name],Narration,Approved,AmountDr as Amount,convert(int,VchNo) as VchNo from View_T_ReceiptDetails Order By VchNo DESC";
            Session["sqlQuery"] = sqlQuery;
            SqlConnection conn = new SqlConnection(strconn);
            conn.Open();
            SqlDataAdapter da = new SqlDataAdapter(sqlQuery, conn);
            DataSet ds = new DataSet();
            da.Fill(ds, "SQLTABLE");
            conn.Close();
            if (ds.Tables["SQLTABLE"].Rows.Count != 0)
            {   
                gvReceiptDetail.DataSource = ds;
                gvReceiptDetail.DataBind();
            }
            else
            {                
                gvReceiptDetail.DataSource = null;
                gvReceiptDetail.DataBind();

                txtFrom.Text = string.Empty;
                txtTo.Text = string.Empty;
                txtKeyword.Text = string.Empty;
            }
        }

        protected void gvReceiptDetail_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (gvReceiptDetail.SelectedRow.Cells[1].Text != null)
            {
                Session["mode"] = "Edit";
                Session["ReceiptDetails"] = gvReceiptDetail.SelectedRow.Cells[2].Text.ToString();
                Response.Redirect("~/Accounts/Receipt.aspx?mode=Edit");
            }
        }

        protected void lnkNew_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Accounts/Receipt.aspx?mode=New");            
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            string FD = txtFrom.Text;
            string TD = txtTo.Text;
            if (FD != "" && TD != "")
            {
                fDate = frmdatesplit(FD);
                tDate = frmdatesplit(TD);
            }
            else
            {
                fDate = "";
                tDate = "";
            }            

            string key = txtKeyword.Text;
            string tilldate = DateTime.Now.ToString("MM-dd-yyyy");


            if (key == "" && fDate != "" && tDate != "")
            {
                sqlQuery = "Select Distinct VoucherNo As [Voucher No],Convert(Varchar(12),VoucherDate,103) As [Voucher Date],AccountDrName As [Account Name],Narration,Approved,AmountDr as Amount,convert(int,VchNo) as VchNo from View_T_ReceiptDetails where VoucherDate between '" + fDate + "' and '" + tDate + "' group by VoucherNo,VoucherDate,AccountDrName,Approved,Narration,AmountDr,VchNo order by VchNo Desc";
            }
            else if (key != "" && fDate != "" && tDate != "")
            {
                sqlQuery = "Select Distinct VoucherNo As [Voucher No],Convert(Varchar(12),VoucherDate,103) As [Voucher Date],AccountDrName As [Account Name],Narration,Approved,AmountDr as Amount,convert(int,VchNo) as VchNo from View_T_ReceiptDetails where VoucherDate between '" + fDate + "' and '" + tDate + "' And VoucherNo='" + key + "' Or VoucherDate between '" + fDate + "' and '" + tDate + "' And AccountDrCode='" + key + "' group by VoucherNo,VoucherDate,AccountDrName,Narration,Approved,AmountDr,VchNo order by VchNo Desc";
            }
            else if (key != "" && fDate == "" && tDate == "")
            {
                sqlQuery = "Select Distinct VoucherNo As [Voucher No],Convert(Varchar(12),VoucherDate,103) As [Voucher Date],AccountDrName As [Account Name],Narration,Approved,AmountDr as Amount,convert(int,VchNo) as VchNo from View_T_ReceiptDetails where VoucherNo='" + key + "' Or AccountDrCode='" + key + "' group by VoucherNo,VoucherDate,AccountDrName,Narration,Approved,AmountDr,VchNo order by VchNo Desc";
            }
            else if (key == "" && fDate == "" && tDate == "")
            {
                sqlQuery = "Select Distinct VoucherNo As [Voucher No],Convert(Varchar(12),VoucherDate,103) As [Voucher Date],AccountDrName As [Account Name],Narration,Approved,AmountDr as Amount,convert(int,VchNo) as VchNo from View_T_ReceiptDetails group by VoucherNo,VoucherDate,AccountDrName,Narration,Approved,AmountDr,VchNo  order by VchNo Desc";
            }

            Session["sqlQuery"] = sqlQuery;
            SqlConnection conn = new SqlConnection(strconn);
            conn.Open();
            SqlDataAdapter da = new SqlDataAdapter(sqlQuery, conn);
            DataSet ds = new DataSet();
            da.Fill(ds, "SQLTABLE");
            conn.Close();
            if (ds.Tables["SQLTABLE"].Rows.Count != 0)
            {
                //SELECT CONVERT(VARCHAR(8), GETDATE(), 3) AS [DD/MM/YY]
                gvReceiptDetail.DataSource = ds;
                gvReceiptDetail.DataBind();
            }
            else
            {
                
                //Response.Write("<script>alert('Records Not Found')</script>");
                ClassMsg.Show("Job Number Does Not Exist");
                gvReceiptDetail.DataSource = null;
                gvReceiptDetail.DataBind();

                txtFrom.Text = string.Empty;
                txtTo.Text = string.Empty;
                txtKeyword.Text = string.Empty;
            }
        }

        protected void gvReceiptDetail_Sorting(object sender, GridViewSortEventArgs e)
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
            gvReceiptDetail.DataSource = sortedView;
            gvReceiptDetail.DataBind();
        }

        public DataTable BindGrid()
        {
            string sqlQuery = (string)Session["sqlQuery"];

            SqlConnection conn = new SqlConnection(strconn);
            conn.Open();
            SqlDataAdapter da = new SqlDataAdapter(sqlQuery, conn);
            DataTable dt = new DataTable();
            da.Fill(dt);
            conn.Close();
            return dt;
        }

        private string frmdatesplit(string frmdate)
        {
            string[] frmdate1 = frmdate.Split('/');
            string frmdate2 = frmdate1[1] + '/' + frmdate1[0] + '/' + frmdate1[2];
            return frmdate2;
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

        protected void gvReceiptDetail_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            string sqlQuery = (string)Session["sqlQuery"];
            SqlConnection conn = new SqlConnection(strconn);
            conn.Open();
            SqlDataAdapter da = new SqlDataAdapter(sqlQuery, conn);
            DataSet ds = new DataSet();
            da.Fill(ds, "SQLTABLE");
            conn.Close();
            gvReceiptDetail.DataSource = ds;
            gvReceiptDetail.DataBind();
            gvReceiptDetail.PageIndex = e.NewPageIndex;
            gvReceiptDetail.DataBind();
        }

        protected void btnPrint_Click(object sender, ImageClickEventArgs e)
        {
            GridViewRow gv = (sender as ImageButton).NamingContainer as GridViewRow;
            int gv1 = gv.RowIndex;
            string vchno = gvReceiptDetail.Rows[gv1].Cells[2].Text.ToString();
            Session["VchNo"] = vchno;
            Response.Redirect("frmPrint_Receipt.aspx"); 
        }
    }
}
