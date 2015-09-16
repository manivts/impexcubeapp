using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Data.SqlClient;
public partial class frmPrintInvoiceAll : System.Web.UI.Page
{
    //string strconn = (string)ConfigurationManager.AppSettings["ConnectionImpex"];
    string strImpex = (string)ConfigurationManager.ConnectionStrings["Constr"].ConnectionString;
    private string invoiceNo = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack == false)
        {
             
            rbSHp.SelectedValue = "IMP";
            rbBill.SelectedValue = "SB";
            string bill = rbBill.SelectedValue;
            string shp = rbSHp.SelectedValue;
            Session["shp"] = shp;
            Session["EdBill"] = bill;
          
            txtPname.Enabled = false;
            string fy = (string)Session["FinancialYear"];
            getSDate(fy);
           
        }
    }
    protected void getSDate(string fy)
    {
        try
        {
            SqlConnection conn = new SqlConnection(strImpex);
            string Query = "select * from M_RunningNo where fyear='" + fy + "'";
            SqlDataAdapter da = new SqlDataAdapter(Query, conn);
            DataSet ds = new DataSet();
            da.Fill(ds, "FYEAR");
            DataRowView row = ds.Tables["FYEAR"].DefaultView[0];
            DateTime sDATES = Convert.ToDateTime(row["sDATE"].ToString());
            txtFrom.Text = sDATES.ToShortDateString();
            txtTo.Text = System.DateTime.Now.ToString("dd/MM/yyyy");
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('" + ex.Message + "');", true);
        }
    }
    protected void BtnSubmit_Click(object sender, EventArgs e)
    {
       
        string[] DT;
        string dt = "";

        string Query = "";
        string fdate = txtFrom.Text;
        string tdate = txtTo.Text;


        if (tdate == "")
            tdate = fdate;

        DT = fdate.Split('/');
        dt = DT[2] + "-" + DT[1] + "-" + DT[0];
        fdate = dt;

        DT = tdate.Split('/');
        dt = DT[2] + "-" + DT[1] + "-" + DT[0];
        tdate = dt;
        string Bill = rbBill.SelectedValue;
        string shpType = rbSHp.SelectedValue;
      
        string impName = txtPname.Text;
      
        string fy = (string)Session["FinancialYear"];
     
        if (Bill == "SB")
        {
            if (fdate != "" && tdate != ""  && chkImp.Checked == false)
                Query = "select * from M_iec_invoicenew where invoiceDate  between '" + fdate + "' and '" + tdate + "' and mode='" + shpType + "'";
            else if (fdate != "" && tdate != "" && chkImp.Checked == true)
                Query = "select * from M_iec_invoicenew where invoiceDate  between '" + fdate + "' and '" + tdate + "' and mode='" + shpType + "' and compName like '" + impName + "%'";
           
        }
        else
        {
          
            if (fdate != "" && tdate != "" && chkImp.Checked == false)
                Query = "select * from M_iec_debit where invoiceDate  between '" + fdate + "' and '" + tdate + "' and mode='" + shpType + "'";
            else if (fdate != "" && tdate != "" && chkImp.Checked == true)
                Query = "select * from M_iec_debit where invoiceDate  between '" + fdate + "' and '" + tdate + "' and mode='" + shpType + "' and compName like '" + impName + "%'";

        }
        if (Query != "")
        {
            gvReport.DataSource = GetData(Query);
            gvReport.DataBind();
        }
        else
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Please Give Bill No...');", true);

       


    }
    public DataSet GetData(string sqlQuery)
    {
        SqlConnection conn = new SqlConnection(strImpex);
        SqlDataAdapter da = new SqlDataAdapter(sqlQuery, conn);
        DataSet ds = new DataSet();
        da.Fill(ds, "invc");
        return ds;
    }
    protected void rbBill_SelectedIndexChanged(object sender, EventArgs e)
    {
        string bill = rbBill.SelectedValue;
        string shp = rbSHp.SelectedValue;
        Session["shp"] = shp;
    
        Session["EdBill"] = bill;
    }
    protected void rbSHp_SelectedIndexChanged(object sender, EventArgs e)
    {
        string bill = rbBill.SelectedValue;
        string shp = rbSHp.SelectedValue;
        Session["shp"] = shp;
       
        Session["EdBill"] = bill;
    }
   
    protected void chkImp_CheckedChanged(object sender, EventArgs e)
    {
        if (chkImp.Checked)
            txtPname.Enabled = true;
        else
            txtPname.Enabled = false;

  }
    protected void gvReport_SelectedIndexChanged(object sender, EventArgs e)
    {
        Session["gSTP"]=rbSHp.SelectedValue;
        Session["gBill"]=rbBill.SelectedValue;
        string iNO = gvReport.SelectedDataKey.Value.ToString();
        string strQuery = "";
        string Bill = rbBill.SelectedValue;
       
        Session["INVOICECTR"] = iNO;
        Session["BILLTYPE"] = Bill;

        SqlConnection conn = new SqlConnection(strImpex);
        if (Bill == "SB")
            strQuery = "select * from M_iec_invoiceNew where invoice='" + iNO + "'";
        else
            strQuery = "select * from M_iec_debit where invoice='" + iNO + "' ";
        SqlDataAdapter da = new SqlDataAdapter(strQuery, conn);
        DataSet ds = new DataSet();
        da.Fill(ds, "table");
        if (ds.Tables["table"].Rows.Count == 0)
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Incorrect Bill Number');", true);
        else
            GetBill(Bill, iNO);
    }
    protected void gvReport_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
           
            if (e.Row.Cells[1].Text != "&nbsp;")
            {
                DateTime bDate = Convert.ToDateTime(e.Row.Cells[1].Text);
                e.Row.Cells[1].Text = bDate.ToString("dd/MM/yyyy");
            }
        }
    }
    protected void GetBill(string Btype, string invNo)
    {
        string strQuery = "";

        if (Btype != "")
        {
            if (Btype == "SB")
                strQuery = "select * from M_iec_invoiceNew where invoice='" + invNo + "' and contr_code is null and particular1 is not null";
            else
                strQuery = "select * from M_iec_debit where invoice='" + invNo + "' and contr_code is null and particular1 is not null";
            SqlConnection conn = new SqlConnection(strImpex);

            SqlDataAdapter da = new SqlDataAdapter(strQuery, conn);
            try
            {

                DataSet ds = new DataSet();
                da.Fill(ds, "table");
                if (ds.Tables["table"].Rows.Count == 0)
                {
                    if (Btype == "SB")
                        GetTransactionReports(invNo);
                    else
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "popup", "window.open('WebForm2.aspx','_blank','menubar=no, scrollbars=yes, toolbar=no, location=no, resizable=yes, left=20, top=20');", true);
                }
                else
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "popup", "window.open('CryInvoiceReport.aspx','_blank','menubar=no, scrollbars=yes, toolbar=no, location=no, resizable=yes, left=20, top=20');", true);

               
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('" + ex.Message + "');", true);
            }
        }
        else
            Response.Write("<script>alert('Please select Bill  type')</script>");
    }
    protected void GetTransactionReports(string invNo)
    {
        string strQuery = "select * from M_iec_invoiceNew where invoice='" + invNo + "' and subTotalTax is not null";

        SqlConnection conn = new SqlConnection(strImpex);

        SqlDataAdapter da = new SqlDataAdapter(strQuery, conn);
        DataSet ds = new DataSet();
        da.Fill(ds, "table");
        if (ds.Tables["table"].Rows.Count == 0)
            ScriptManager.RegisterStartupScript(this, this.GetType(), "popup", "window.open('CryInvoiceReportCTR.aspx','_blank','menubar=no, scrollbars=yes, toolbar=no, location=no, resizable=yes, left=20, top=20');", true);
        else
            ScriptManager.RegisterStartupScript(this, this.GetType(), "popup", "window.open('CryInvoiceReportSTax.aspx','_blank','menubar=no, scrollbars=yes, toolbar=no, location=no, resizable=yes, left=20, top=20');", true);
    }
    protected void BtnPDF_Click(object sender, EventArgs e)
    {
        string fDate = txtFrom.Text;
        string tDate = txtTo.Text;
        string[] FD = fDate.Split('/');
        fDate = FD[2] + "-" + FD[1] + "-" + FD[0];
        Session["PRINTALL"] = "True";
        string[] TD = tDate.Split('/');
        tDate = TD[2] + "-" + TD[1] + "-" + TD[0];

        Session["FD"] = fDate;
        Session["TD"] = tDate;
        string shpType = rbSHp.SelectedValue;
        string Btype = rbBill.SelectedValue;
        if (chkImp.Checked)
            Session["pname"] = txtPname.Text;
        else
            Session["pname"] = null;

        Session["BILLTYPE"] = Btype;
        Session["shpType"] = shpType;
        //Response.Redirect("../frmImpInvoiceReport.aspx");
        ScriptManager.RegisterStartupScript(this, this.GetType(), "popup", "window.open('WebForm2.aspx','_blank','menubar=no, scrollbars=yes, toolbar=no, location=no, resizable=yes, left=20, top=20');", true);
    }
}
