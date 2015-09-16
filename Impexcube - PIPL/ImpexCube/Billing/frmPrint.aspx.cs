using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;

public partial class frmPrint : System.Web.UI.Page
{
    //string strConn = ConfigurationManager.AppSettings["ConnectionImpex"];
    string strImpex = (string)ConfigurationManager.ConnectionStrings["Constr"].ConnectionString;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack == false)
        {
           
        }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
     {
        string strQuery = "";
        string Bill = txtInvNo.Text;
        string Btype = rbBill.SelectedValue;
        Session["INVOICECTR"] = Bill;
        Session["BILLTYPE"] = Btype;

        SqlConnection conn = new SqlConnection(strImpex);
        if (Btype == "SB")
                strQuery = "select * from M_iec_invoiceNew where invoice='" + Bill + "'";
            else
                strQuery = "select * from M_iec_debit where invoice='" + Bill + "' ";
         SqlDataAdapter da = new SqlDataAdapter(strQuery, conn);
         DataSet ds = new DataSet();
         da.Fill(ds, "table");
         //if (ds.Tables["table"].Rows.Count == 0)
         //    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Incorrect Bill Number');", true);
         //else
         //    GetBill(Btype, Bill);
         if (Btype == "SB")
         {
             Session["InvNo"] = txtInvNo.Text;
             string rep = (string)Session["InvNo"];
             string sub = rep.Substring(4, 2);
             if (sub == "SB")
             {
                     Response.Redirect("../frmImpInvoiceReport.aspx");
             }
             //else
             //{
             //Response.Redirect("frmDebit.aspx");
             //}
         }
         else
         {
             Session["InvNo"] = txtInvNo.Text;
             string rep = (string)Session["InvNo"];
             string sub = rep.Substring(4, 2);
             if (sub == "DB")
             {                
                 Response.Redirect("../frmDebit.aspx");
             }
         }
    }
    protected void GetBill(string Btype,string Bill)
    {
        string strQuery = "";

        if (Btype != "")
        {
            if (Btype == "SB")
                strQuery = "select * from M_iec_invoiceNew where invoice='" + Bill + "' and contr_code is null and particular1 is not null";
            else
                strQuery = "select * from M_iec_debit where invoice='" + Bill + "' and contr_code is null and particular1 is not null";
            SqlConnection conn = new SqlConnection(strImpex);

            SqlDataAdapter da = new SqlDataAdapter(strQuery, conn);
            try
            {

                DataSet ds = new DataSet();
                da.Fill(ds, "table");
                if (ds.Tables["table"].Rows.Count == 0)
                {
                    if (Btype == "SB")
                        GetTransactionReports(Bill);
                    else
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "popup", "window.open('CryInvoiceReportCTR.aspx','_blank','menubar=no, scrollbars=yes, toolbar=no, location=no, resizable=yes, left=20, top=20');", true);

                       

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
    protected void GetTransactionReports(string bill)
    {
        string strQuery = "select * from M_iec_invoiceNew where invoice='" + bill + "' and subTotalTax is not null";

            SqlConnection conn = new SqlConnection(strImpex);
            SqlDataAdapter da = new SqlDataAdapter(strQuery, conn);
            DataSet ds = new DataSet();
            da.Fill(ds, "table");
            if (ds.Tables["table"].Rows.Count == 0)
                ScriptManager.RegisterStartupScript(this, this.GetType(), "popup", "window.open('CryInvoiceReportCTR.aspx','_blank','menubar=no, scrollbars=yes, toolbar=no, location=no, resizable=yes, left=20, top=20');", true);
            else
                ScriptManager.RegisterStartupScript(this, this.GetType(), "popup", "window.open('CryInvoiceReportSTax.aspx','_blank','menubar=no, scrollbars=yes, toolbar=no, location=no, resizable=yes, left=20, top=20');", true);

    }
    protected void rbBill_SelectedIndexChanged(object sender, EventArgs e)
    {
        string bill = rbBill.SelectedValue;
        string shp = rbSHp.SelectedValue;
        Session["shp"] = shp;
       
        Session["PrintBill"] = bill;
    }

    protected void rbSHp_SelectedIndexChanged(object sender, EventArgs e)
    {
        string bill = rbBill.SelectedValue;
        string shp = rbSHp.SelectedValue;
        Session["shp"] = shp;
     
        Session["PrintBill"] = bill;
    }
}
