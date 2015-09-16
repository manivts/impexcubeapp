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
public partial class frmPrintInvoice : System.Web.UI.Page
{
    //string strImpex = (string)ConfigurationManager.AppSettings["ConnectionImpex"];
    string strImpex = (string)ConfigurationManager.ConnectionStrings["Constr"].ConnectionString;
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
            txtBE.Enabled = false;
            txtPname.Enabled = false;
            txtInvoiceNo.Enabled = false;
           
        }
    }
    protected void BtnSubmit_Click(object sender, EventArgs e)
    {
        string iNO = txtInvoiceNo.Text;
        string Bill = rbBill.SelectedValue;
        string shpType = rbSHp.SelectedValue;
        string BeNo = txtBE.Text;
        string impName = txtPname.Text;
        string Query = "";
        string fy = (string)Session["FinancialYear"];
       
        if (Bill == "SB")
        {
            if (chkBill.Checked == true && chkBE.Checked == false && chkImp.Checked == false)
                Query = "select * from M_iec_invoicenew where invoice='" + iNO + "' and mode='" + shpType + "' ";
            else if (chkBill.Checked == true && chkBE.Checked == true && chkImp.Checked == false)
                Query = "select * from M_iec_invoicenew where invoice='" + iNO + "' and BEnoDate like '%" + BeNo + "%' and mode='" + shpType + "' ";
            else if (chkBill.Checked == true && chkBE.Checked == false && chkImp.Checked == true)
                Query = "select * from M_iec_invoicenew where invoice='" + iNO + "' and compName like '" + impName + "%' and mode='" + shpType + "' ";
            else if (chkBill.Checked == true && chkBE.Checked == true && chkImp.Checked == true)
                Query = "select * from M_iec_invoicenew where invoice='" + iNO + "' and BEnoDate like '%" + BeNo + "%' and compName like '" + impName + "%' and mode='" + shpType + "' ";
            else if (chkBill.Checked == false && chkBE.Checked == true && chkImp.Checked == true)
                Query = "select * from M_iec_invoicenew where BEnoDate like '%" + BeNo + "%' and compName like '" + impName + "%' and mode='" + shpType + "' ";
            else if (chkBill.Checked == false && chkBE.Checked == false && chkImp.Checked == true)
                Query = "select * from M_iec_invoicenew where  compName like '" + impName + "%' and mode='" + shpType + "' ";
            else if (chkBill.Checked == false && chkBE.Checked == true && chkImp.Checked == false)
                Query = "select * from M_iec_invoicenew where BEnoDate like '%" + BeNo + "%' and mode='" + shpType + "' ";
            else
                Query = "select * from M_iec_invoicenew where fyear like '%" + fy + "%' and mode='" + shpType + "' ";


        }
        else
        {
           
            if (chkBill.Checked == true && chkBE.Checked == false && chkImp.Checked == false)
                Query = "select * from M_iec_debit where invoice='" + iNO + "' and mode='" + shpType + "' ";
            else if (chkBill.Checked == true && chkBE.Checked == true && chkImp.Checked == false)
                Query = "select * from M_iec_debit where invoice='" + iNO + "' and BEnoDate like '%" + BeNo + "%' and mode='" + shpType + "' ";
            else if (chkBill.Checked == true && chkBE.Checked == false && chkImp.Checked == true)
                Query = "select * from M_iec_debit where invoice='" + iNO + "' and compName like '" + impName + "%' and mode='" + shpType + "' ";
            else if (chkBill.Checked == true && chkBE.Checked == true && chkImp.Checked == true)
                Query = "select * from M_iec_debit where invoice='" + iNO + "' and BEnoDate like '%" + BeNo + "%' and compName like '" + impName + "%' and mode='" + shpType + "' ";
            else if (chkBill.Checked == false && chkBE.Checked == true && chkImp.Checked == true)
                Query = "select * from M_iec_debit where BEnoDate like '%" + BeNo + "%' and compName like '" + impName + "%' and mode='" + shpType + "' ";
            else if (chkBill.Checked == false && chkBE.Checked == false && chkImp.Checked == true)
                Query = "select * from M_iec_debit where  compName like '" + impName + "%' and mode='" + shpType + "' ";
            else if (chkBill.Checked == false && chkBE.Checked == true && chkImp.Checked == false)
                Query = "select * from M_iec_debit where BEnoDate like '%" + BeNo + "%' and mode='" + shpType + "' ";
            else
                Query = "select * from M_iec_debit where fyear like '%" + fy + "%' and mode='" + shpType + "' ";

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
    protected void chkBill_CheckedChanged(object sender, EventArgs e)
    {
        if (chkBill.Checked == true)
            txtInvoiceNo.Enabled = true;
        else
            txtInvoiceNo.Enabled = false;
    }
    protected void chkBE_CheckedChanged(object sender, EventArgs e)
    {
        if (chkBE.Checked == true)
            txtBE.Enabled = true;
        else
            txtBE.Enabled = false;
        
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

        Session["InvNo"] = gvReport.SelectedDataKey.Value.ToString();;
        string rep = (string)Session["InvNo"];
        string sub = rep.Substring(4, 2);
        if (sub == "SB")
        {
            Response.Redirect("../frmImpInvoiceReport.aspx");
        }
        else
        {
            Response.Redirect("../frmDebit.aspx");
        }

        //Session["gSTP"]=rbSHp.SelectedValue;
        //Session["gBill"]=rbBill.SelectedValue;
        //string iNO = gvReport.SelectedDataKey.Value.ToString();
        //string strQuery = "";
        //string Bill = rbBill.SelectedValue;
      
        //Session["INVOICECTR"] = iNO;
        //Session["BILLTYPE"] = Bill;

        //SqlConnection conn = new SqlConnection(strImpex);
        //if (Bill == "SB")
        //    strQuery = "select * from M_iec_invoicenew where invoice='" + iNO + "'";
        //else
        //    strQuery = "select * from M_iec_debit where invoice='" + iNO + "' ";
        //SqlDataAdapter da = new SqlDataAdapter(strQuery, conn);
        //DataSet ds = new DataSet();
        //da.Fill(ds, "table");
        //if (ds.Tables["table"].Rows.Count == 0)
        //    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Incorrect Bill Number');", true);
        //else
        //    GetBill(Bill, iNO);
    }
    protected void gvReport_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
           
            if (e.Row.Cells[2].Text != "&nbsp;")
            {
                DateTime bDate = Convert.ToDateTime(e.Row.Cells[2].Text);
                e.Row.Cells[2].Text = bDate.ToString("dd/MM/yyyy");
            }
        }
    }
    protected void GetBill(string Btype, string invNo)
    {
        string strQuery = "";

        if (Btype != "")
        {

           
            if (Btype == "SB")
                strQuery = "select * from M_iec_invoicenew where invoice='" + invNo + "' and contr_code is null and particular1 is not null";
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
    protected void GetTransactionReports(string invNo)
    {
        string strQuery = "select * from M_iec_invoicenew where invoice='" + invNo + "' and subTotalTax is not null";

        SqlConnection conn = new SqlConnection(strImpex);

        SqlDataAdapter da = new SqlDataAdapter(strQuery, conn);
        DataSet ds = new DataSet();
        da.Fill(ds, "table");
        if (ds.Tables["table"].Rows.Count == 0)

            ScriptManager.RegisterStartupScript(this, this.GetType(), "popup", "window.open('CryInvoiceReportCTR.aspx','_blank','menubar=no, scrollbars=yes, toolbar=no, location=no, resizable=yes, left=20, top=20');", true);
        else
            ScriptManager.RegisterStartupScript(this, this.GetType(), "popup", "window.open('CryInvoiceReportSTax.aspx','_blank','menubar=no, scrollbars=yes, toolbar=no, location=no, resizable=yes, left=20, top=20');", true);

    }
}
