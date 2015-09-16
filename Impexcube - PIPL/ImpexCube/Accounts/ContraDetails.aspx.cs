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
    public partial class ContraDetails : System.Web.UI.Page
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
            }
        }

        private void GridLoad()
        {
            sqlQuery = "Select VoucherNo As [Voucher No],Convert(Varchar(12),VoucherDate,103) As [Voucher Date],Acc_CrCode As [Account Code],Narration,sum(Amount) as Amount,convert(int,VchNo) as VchNo from View_Contra group by VoucherNo,VoucherDate,Acc_CrCode,Narration,VchNo order by VchNo DESC";

            SqlConnection conn = new SqlConnection(strconn);
            conn.Open();
            SqlDataAdapter da = new SqlDataAdapter(sqlQuery, conn);
            DataSet ds = new DataSet();
            da.Fill(ds, "SQLTABLE");
            conn.Close();
            if (ds.Tables["SQLTABLE"].Rows.Count != 0)
            {
                gvContraDetails.DataSource = ds;
                gvContraDetails.DataBind();
            }
            else
            {
                //Response.Write("<script>alert('Records Not Found')</script>");
                gvContraDetails.DataSource = null;
                gvContraDetails.DataBind();
            }
        }

        protected void lnkNew_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Accounts/ContraEntry.aspx?mode=New");
        }

        protected void gvContraDetails_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (gvContraDetails.SelectedRow.Cells[1].Text != null)
            {
                Session["ContraDetails"] = gvContraDetails.SelectedRow.Cells[2].Text.ToString();
                Response.Redirect("~/Accounts/ContraEntry.aspx?mode=Edit");
            }
        }

        protected void lnkExit_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Accounts/MainMenu.aspx");
        }
        protected void btnPrint_Click(object sender, ImageClickEventArgs e)
        {
            GridViewRow gv = (sender as ImageButton).NamingContainer as GridViewRow;
            int gv1 = gv.RowIndex;
            string vchno = gvContraDetails.Rows[gv1].Cells[2].Text.ToString();
            Session["VchNo"] = vchno;
            //Response.Redirect("frmPrint_Journal.aspx");
        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            string FD = txtFrom.Text;
            string TD = txtTo.Text;           
            if (FD != "" && TD != "")
            {
                string sMM = FD.Substring(3, 2);
                string sDD = FD.Substring(0, 2);
                string sYY = FD.Substring(6, 4);
                FD = sMM + "/" + sDD + "/" + sYY;



                string eMM = TD.Substring(3, 2);
                string eDD = TD.Substring(0, 2);
                string eYY = TD.Substring(6, 4);
                TD = eMM + "/" + eDD + "/" + eYY;


                DateTime fd = Convert.ToDateTime(FD);
                DateTime td = Convert.ToDateTime(TD);

                fDate = fd.ToString("MM-dd-yyyy");
                tDate = td.ToString("MM-dd-yyyy");
            }          


            string key = txtKeyword.Text;
           


            if (key == "" && fDate != "" && tDate != "")
            {
                sqlQuery = "Select Distinct VoucherNo As [Voucher No],Convert(Varchar(12),VoucherDate,103) As [Voucher Date],Acc_CrCode As [Account Code],Narration,sum(Amount) as Amount,convert(int,VchNo) as VchNo from View_Contra where VoucherDate between '" + fDate + "' and '" + tDate + "'group by VoucherNo,VoucherDate,Acc_CrCode,Narration,VchNo order by VchNo Desc ";
            }
            else if (key != "" && fDate != "" && tDate != "")
            {
                sqlQuery = "Select Distinct VoucherNo As [Voucher No],Convert(Varchar(12),VoucherDate,103) As [Voucher Date],Acc_CrCode As [Account Code],Narration,sum(Amount) as Amount,convert(int,VchNo) as VchNo from View_Contra where VoucherDate between '" + fDate + "' and '" + tDate + "' And VoucherNo='" + key + "' Or VoucherDate between '" + fDate + "' and '" + tDate + "' And Acc_CrCode='" + key + "'group by VoucherNo,VoucherDate,Acc_CrCode,Narration,VchNo order by VchNo Desc";
            }
            else if (key != "" && fDate == "" && tDate == "")
            {
                sqlQuery = "Select Distinct VoucherNo As [Voucher No],Convert(Varchar(12),VoucherDate,103) As [Voucher Date],Acc_CrCode As [Account Code],Narration,sum(Amount) as Amount,convert(int,VchNo) as VchNo from View_Contra where VoucherNo='" + key + "' Or Acc_CrCode='" + key + "'group by VoucherNo,VoucherDate,Acc_CrCode,Narration,VchNo order by VchNo Desc";
            }
            else if (key == "" && fDate == "" && tDate == "")
            {
                sqlQuery = "Select Distinct VoucherNo As [Voucher No],Convert(Varchar(12),VoucherDate,103) As [Voucher Date],Acc_CrCode As [Account Code],Narration,sum(Amount) as Amount,convert(int,VchNo) as VchNo from View_Contra group by VoucherNo,VoucherDate,Acc_CrCode,Narration,VchNo order by VchNo Desc";
            }


            
            SqlConnection conn = new SqlConnection(strconn);
            conn.Open();
            SqlDataAdapter da = new SqlDataAdapter(sqlQuery, conn);
            DataSet ds = new DataSet();
            da.Fill(ds, "SQLTABLE");
            conn.Close();
            if (ds.Tables["SQLTABLE"].Rows.Count != 0)
            {   
                gvContraDetails.DataSource = ds;
                gvContraDetails.DataBind();
            }
            else
            {
                //Response.Write("<script>alert('Records Not Found')</script>");
                ClassMsg.Show("Job Number Does Not Exist");
            }
        }
    }
}
