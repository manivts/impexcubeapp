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
    public partial class PaymentDetails : System.Web.UI.Page
    {
        string strconn = (string)ConfigurationManager.ConnectionStrings["Constr"].ConnectionString;
        string sqlQuery;
        string fDate="";
        string tDate="";

        protected void Page_Load(object sender, EventArgs e)
        {            
            if (!Page.IsPostBack)
            {                
                if (Request.QueryString["mode"] == "Cash")
                {
                    lblPaymentDetails.Text = "Cash Payment Details";
                    Session["Mode"] = "Cash Payment";
                    GridLoad();
               }
                else if (Request.QueryString["mode"] == "Bank")
                {
                    lblPaymentDetails.Text = "Bank Payment Details";
                    Session["Mode"] = "Bank Payment"; 
                    GridLoad();
                   
                }
                else if (Request.QueryString["mode"] == "Contra")
                {

                    lblPaymentDetails.Text = "Contra Details";

                    Session["Mode"] = "Contra";

                    GridLoad();

                }
                              
                txtFrom.Text = string.Empty;
                txtTo.Text = string.Empty;
                txtKeyword.Text = string.Empty;
            }
        }

        private void GridLoad()
        {
            sqlQuery = "Select Distinct VoucherNo As [Voucher No],Convert(Varchar(12),VoucherDate,103) As [Voucher Date],AccountCrName As [Account Name],Approved,AmountCr as Amount,convert(int,VchNo) as VchNo from View_T_PaymentDetails where VCH_type= '" + (string)Session["Mode"] + "' Order By VchNo Desc";// group by VoucherNo,VoucherDate,Acc_CrCode,Approved order by VoucherDate DESC";
            SqlConnection conn = new SqlConnection(strconn);
            conn.Open();
            SqlDataAdapter da = new SqlDataAdapter(sqlQuery, conn);
            DataSet ds = new DataSet();
            da.Fill(ds, "SQLTABLE");
            conn.Close();
            if (ds.Tables["SQLTABLE"].Rows.Count != 0)
            {   
                gvPaymentDetail.DataSource = ds;
                gvPaymentDetail.DataBind();
                Session["sqlQuery"] = sqlQuery;
            }
            else
            {
                //Response.Write("<script>alert('Records Not Found')</script>");
                gvPaymentDetail.DataSource = null;
                gvPaymentDetail.DataBind();
                txtFrom.Text = string.Empty;
                txtTo.Text = string.Empty;
                txtKeyword.Text = string.Empty;
            }

        }

        protected void gvPaymentDetail_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (gvPaymentDetail.SelectedRow.Cells[2].Text != null)
            {
            Session["PaymentDetails"] = gvPaymentDetail.SelectedRow.Cells[2].Text.ToString();
            //bool userform = Utility.userentryform(strconn, "Payment Entry", (string)Session["UserName"]);
           // bool approvalform = Utility.ApprovalEntryForm(strconn, "Payment Entry", (string)Session["UserName"]);
            //if (userform == true || approvalform == true)
            //{
                if (Request.QueryString["mode"] == "Cash")
                {
                    Response.Redirect("~/Accounts/Payment.aspx?mode=CashEdit");
                }
                else if (Request.QueryString["mode"] == "Bank")
                {
                    Response.Redirect("~/Accounts/Payment.aspx?mode=BankEdit");
                }
                else if (Request.QueryString["mode"] == "Contra")
                {
                    Response.Redirect("~/Accounts/Payment.aspx?mode=ContraEdit");
                }

 
              
            //}
            //else
            //    ClassMsg.Show("Not Approved");
  
            }          
            
        }


        protected void lnkNew_Click(object sender, EventArgs e)
        {
           // bool userform = Utility.userentryform(strconn, "Payment Entry", (string)Session["UserName"]);
         //   bool approvalform = Utility.ApprovalEntryForm(strconn, "Payment Entry", (string)Session["UserName"]);
                //if (userform == true || approvalform == true)
                //{
                    if (Request.QueryString["mode"] == "Cash")
                    {
                        Response.Redirect("~/Accounts/Payment.aspx?mode=CashNew");
                    }
                    else if (Request.QueryString["mode"] == "Bank")
                    {
                        Response.Redirect("~/Accounts/Payment.aspx?mode=BankNew");
                    }
                    else if (Request.QueryString["mode"] == "Contra")
                    {
                        Response.Redirect("~/Accounts/Payment.aspx?mode=ContraNew");
                    }

 
               // }
               // else
               //ClassMsg.Show("Not Approved");
            
    
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

                if (key == "" && fDate != "" && tDate != "")
                {
                    sqlQuery = "Select Distinct VoucherNo As [Voucher No],Convert(Varchar(12),VoucherDate,103) As [Voucher Date],AccountCrName As [Account Name],Approved,AmountCr as Amount,convert(int,VchNo) as VchNo from View_T_PaymentDetails where VoucherDate between '" + fDate + "' and '" + tDate + "' and VCH_type='" + (string)Session["Mode"] + "' ";
                }
                else if (key != "" && fDate != "" && tDate != "")
                {
                    sqlQuery = "Select Distinct VoucherNo As [Voucher No],Convert(Varchar(12),VoucherDate,103) As [Voucher Date],AccountCrName As [Account Name],Approved,AmountCr as Amount,convert(int,VchNo) as VchNo from View_T_PaymentDetails where VoucherDate between '" + fDate + "' and '" + tDate + "'and VoucherNo like '%" + key + "%' and VCH_type= '" + (string)Session["Mode"] + "'  order by VchNo DESC";
                }
                else if (key != "" && fDate == "" && tDate == "")
                {
                    sqlQuery = "Select Distinct VoucherNo As [Voucher No],Convert(Varchar(12),VoucherDate,103) As [Voucher Date],AccountCrName As [Account Name],Approved,AmountCr as Amount,convert(int,VchNo) as VchNo from View_T_PaymentDetails where VoucherNo like '%" + key + "%' and VCH_type= '" + (string)Session["Mode"] + "'  order by VchNo DESC";
                }
                else if (key == "" && fDate == "" && tDate == "")
                {
                    sqlQuery = "Select Distinct VoucherNo As [Voucher No],Convert(Varchar(12),VoucherDate,103) As [Voucher Date],AccountCrName As [Account Name],Approved,AmountCr as Amount,convert(int,VchNo) as VchNo from View_T_PaymentDetails where VCH_type= '" + (string)Session["Mode"] + "'  order by VchNo DESC";
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
                    gvPaymentDetail.DataSource = ds;
                    gvPaymentDetail.DataBind();
                }
                else
                {

                    //Response.Write("<script>alert('Records Not Found')</script>");
                    ClassMsg.Show("Job Number Does Not Exist");
                    gvPaymentDetail.DataSource = null;
                    gvPaymentDetail.DataBind();
                    txtFrom.Text = string.Empty;
                    txtTo.Text = string.Empty;
                    txtKeyword.Text = string.Empty;
                }
            }
        
        

        protected void gvPaymentDetail_RowDataBound(object sender, GridViewRowEventArgs e)
        {
           
        }        

        protected void gvPaymentDetail_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            string sqlQuery = (string)Session["sqlQuery"];
            SqlConnection conn = new SqlConnection(strconn);
            conn.Open();
            SqlDataAdapter da = new SqlDataAdapter(sqlQuery, conn);
            DataSet ds = new DataSet();
            da.Fill(ds, "SQLTABLE");
            conn.Close();
            gvPaymentDetail.DataSource = ds;
            gvPaymentDetail.DataBind();
            gvPaymentDetail.PageIndex = e.NewPageIndex;
            gvPaymentDetail.DataBind();
        }

        protected void gvPaymentDetail_Sorting(object sender, GridViewSortEventArgs e)
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
            gvPaymentDetail.DataSource = sortedView;
            gvPaymentDetail.DataBind();
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

        protected void btnPrint_Click(object sender, ImageClickEventArgs e)
        {
            GridViewRow gv = (sender as ImageButton).NamingContainer as GridViewRow;
            int gv1 = gv.RowIndex;
            string vchno = gvPaymentDetail.Rows[gv1].Cells[2].Text.ToString();
            Session["VchNo"] = vchno;
           // Response.Redirect("frmPrint_Payment.aspx");    
        }

        protected void lnkExit_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Accounts/MainMenu.aspx");
        }
    }
}
