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
    public partial class JournalDetails : System.Web.UI.Page
    {
        string strconn = (string)ConfigurationManager.ConnectionStrings["Constr"].ConnectionString;
        string sqlQuery;
        string fDate="";
        string tDate="";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                       
            SqlConnection conn = new SqlConnection(strconn);
            sqlQuery = "Select VoucherNo,Convert(Varchar(12),VoucherDate,103) As VoucherDate,BranchCode,Narration,Approved,sum(Amount) as Amount,convert(int,VchNo) as VchNo from View_JournalDetails group by VoucherNo,VoucherDate,BranchCode,Narration,Approved,VchNo order by VchNo Desc";
            conn.Open();
            SqlDataAdapter da = new SqlDataAdapter(sqlQuery, conn);
            DataSet ds = new DataSet();
            da.Fill(ds, "SQLTABLE");
            conn.Close();
            if (ds.Tables["SQLTABLE"].Rows.Count != 0)
            {
                gvJournalDetails.DataSource = ds;
                gvJournalDetails.DataBind();
            }
            }
        }

        protected void lnkNew_Click(object sender, EventArgs e)
        {
           // Session["mode"] = "New";            
            Response.Redirect("~/Accounts/Journal.aspx?mode=New");            
        }

        protected void gvJournalDetails_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (gvJournalDetails.SelectedRow.Cells[1].Text != null)
            {
                //Session["mode"] = "Edit";
                Session["JournalDetails"] = gvJournalDetails.SelectedRow.Cells[2].Text;
                Response.Redirect("~/Accounts/Journal.aspx?mode=Edit");
            }
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
                sqlQuery = "Select Distinct VoucherNo,Convert(Varchar(12),VoucherDate,103) As VoucherDate,BranchCode,Narration,Approved,sum(Amount) as Amount,convert(int,VchNo) as VchNo from View_JournalDetails where DrCr='Dr' And VoucherDate between '" + fDate + "' and '" + tDate + "'group by VoucherNo,VoucherDate,BranchCode,Narration,Approved,VchNo order by VchNo Desc";
            }
            else if (key != "" && fDate != "" && tDate != "")
            {
                sqlQuery = "Select Distinct VoucherNo,Convert(Varchar(12),VoucherDate,103) As VoucherDate,BranchCode,Narration,Approved,sum(Amount) as Amount,convert(int,VchNo) as VchNo from View_JournalDetails where DrCr='Dr' And VoucherDate between '" + fDate + "' and '" + tDate + "' And VoucherNo='" + key + "' Or DrCr='Dr' And VoucherDate between '" + fDate + "' and '" + tDate + "' And BranchCode='" + key + "'group by VoucherNo,VoucherDate,BranchCode,Narration,Approved,VchNo order by VchNo Desc";
            }
            else if (key != "" && fDate == "" && tDate == "")
            {
                sqlQuery = "Select Distinct VoucherNo,Convert(Varchar(12),VoucherDate,103) As VoucherDate,BranchCode,Narration,Approved,sum(Amount) as Amount,convert(int,VchNo) as VchNo from View_JournalDetails where DrCr='Dr' And VoucherNo='" + key + "' Or DrCr='Dr' And BranchCode='" + key + "'group by VoucherNo,VoucherDate,BranchCode,Narration,Approved,VchNo order by VchNo Desc";
            }
            else if (key == "" && fDate == "" && tDate == "")
            {
                sqlQuery = "Select Distinct VoucherNo,Convert(Varchar(12),VoucherDate,103) As VoucherDate,BranchCode,Narration,Approved,sum(Amount) as Amount,convert(int,VchNo) as VchNo  from View_JournalDetails where DrCr='Dr' group by VoucherNo,VoucherDate,BranchCode,Narration,VchNo,Approved order by VchNo Desc";
            }
            
            SqlConnection conn = new SqlConnection(strconn);
            conn.Open();
            SqlDataAdapter da = new SqlDataAdapter(sqlQuery, conn);
            DataSet ds = new DataSet();
            da.Fill(ds, "SQLTABLE");
            conn.Close();
            if (ds.Tables["SQLTABLE"].Rows.Count != 0)
            {
                //SELECT CONVERT(VARCHAR(8), GETDATE(), 3) AS [DD/MM/YY]
                gvJournalDetails.DataSource = ds;
                gvJournalDetails.DataBind();
            }
            else
            {
                //Response.Write("<script>alert('Records Not Found')</script>");
                ClassMsg.Show("Job Number Does Not Exist");
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
            string vchno = gvJournalDetails.Rows[gv1].Cells[2].Text.ToString();
            Session["VchNo"] = vchno;
            //Response.Redirect("frmPrint_Journal.aspx");
        }
        
    }
}
