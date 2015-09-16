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
public partial class frmEditInvoice : System.Web.UI.Page
{
   // string strconn = (string)ConfigurationManager.AppSettings["ConnectionImpex"];
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
            txtJobNo.Enabled = false;
            
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
        string jno = txtJobNo.Text;
        string fy = (string)Session["FinancialYear"];
        
        if (Bill == "SB")
        {
            if(chkBill.Checked == true && chkBE.Checked == false && chkImp.Checked == false)
                Query = "select * from M_iec_invoicenew where invoice='" + iNO + "' and fyear like '%" + fy + "%' and mode='" + shpType + "' ";
            else if (chkBill.Checked == true && chkBE.Checked == true && chkImp.Checked == false)
                Query = "select * from M_iec_invoicenew where invoice='" + iNO + "' and fyear like '%" + fy + "%' and BEnoDate like '%" + BeNo + "%' and mode='" + shpType + "' ";
            else if (chkBill.Checked == true && chkBE.Checked == false && chkImp.Checked == true)
                Query = "select * from M_iec_invoicenew where invoice='" + iNO + "' and fyear like '%" + fy + "%' and compName like '" + impName + "%' and mode='" + shpType + "' ";
            else if (chkBill.Checked == true && chkBE.Checked == true && chkImp.Checked == true)
                Query = "select * from M_iec_invoicenew where invoice='" + iNO + "' and fyear like '%" + fy + "%' and BEnoDate like '%" + BeNo + "%' and compName like '" + impName + "%' and mode='" + shpType + "' ";
            else if (chkBill.Checked == false && chkBE.Checked == true && chkImp.Checked == true)
                Query = "select * from M_iec_invoicenew where BEnoDate like '%" + BeNo + "%' and fyear like '%" + fy + "%'  and compName like '" + impName + "%' and mode='" + shpType + "' ";
            else if (chkBill.Checked == false && chkBE.Checked == false && chkImp.Checked == true)
                Query = "select * from M_iec_invoicenew where  compName like '" + impName + "%' and fyear like '%" + fy + "%'  and mode='" + shpType + "' ";
            else if (chkBill.Checked == false && chkBE.Checked == true && chkImp.Checked == false)
                Query = "select * from M_iec_invoicenew where BEnoDate like '%" + BeNo + "%' and fyear like '%" + fy + "%' and mode='" + shpType + "' ";
            else
                Query = "select * from M_iec_invoicenew where fyear like '%" + fy + "%' and mode='" + shpType + "' ";


        }
        else
        {
            
            if (chkBill.Checked == true && chkBE.Checked == false && chkImp.Checked == false)
                Query = "select * from M_iec_debit where invoice='" + iNO + "' and fyear like '%" + fy + "%' and  mode='" + shpType + "' ";
            else if (chkBill.Checked == true && chkBE.Checked == true && chkImp.Checked == false)
                Query = "select * from M_iec_debit where invoice='" + iNO + "' and fyear like '%" + fy + "%' and BEnoDate like '%" + BeNo + "%' and mode='" + shpType + "'";
            else if (chkBill.Checked == true && chkBE.Checked == false && chkImp.Checked == true)
                Query = "select * from M_iec_debit where invoice='" + iNO + "' and fyear like '%" + fy + "%' and compName like '" + impName + "%' and mode='" + shpType + "'";
            else if (chkBill.Checked == true && chkBE.Checked == true && chkImp.Checked == true)
                Query = "select * from M_iec_debit where invoice='" + iNO + "' and fyear like '%" + fy + "%' and BEnoDate like '%" + BeNo + "%' and compName like '" + impName + "%' and mode='" + shpType + "'";
            else if (chkBill.Checked == false && chkBE.Checked == true && chkImp.Checked == true)
                Query = "select * from M_iec_debit where BEnoDate like '%" + BeNo + "%' and fyear like '%" + fy + "%' and compName like '" + impName + "%' and mode='" + shpType + "' ";
            else if (chkBill.Checked == false && chkBE.Checked == false && chkImp.Checked == true)
                Query = "select * from M_iec_debit where  compName like '" + impName + "%' and fyear like '%" + fy + "%' and mode='" + shpType + "'";
            else if (chkBill.Checked == false && chkBE.Checked == true && chkImp.Checked == false)
                Query = "select * from M_iec_debit where BEnoDate like '%" + BeNo + "%' and fyear like '%" + fy + "%' and mode='" + shpType + "' ";
            else
                Query = "select * from M_iec_debit where fyear like '%" + fy + "%' and mode='" + shpType + "' ";

        }
        if (chkJobs.Checked)
        {
            if (Bill == "SB")
                Query = "select * from M_iec_invoicenew where fyear like '%" + fy + "%' and jobno like '%" + jno + "%'";
            else
                Query = "select * from M_iec_debit where fyear like '%" + fy + "%' and jobno like '%" + jno + "%' ";
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
        DataSet ds = new DataSet();
        try
        {
            SqlConnection conn = new SqlConnection(strImpex);
            conn.Open();
            SqlDataAdapter da = new SqlDataAdapter(sqlQuery, conn);
            da.Fill(ds, "invc");
            conn.Close();
        }
        catch (Exception ex)
        {
            string Message = ex.Message;
        }
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

        string sqlQuery = "";
        Session["gSTP"]=rbSHp.SelectedValue;
        Session["gBill"]=rbBill.SelectedValue;
        string iNO = gvReport.SelectedDataKey.Value.ToString();
        if (rbBill.SelectedValue == "SB")
        {
            sqlQuery = "select Completed,OldJB from M_iec_invoiceNew where invoice='" + iNO + "' ";
        }
        else
        {
            sqlQuery = "select Completed,OldJB from M_iec_debit where invoice='" + iNO + "' ";
        }
        SqlConnection conn = new SqlConnection(strImpex);
        conn.Open();
        SqlDataAdapter da = new SqlDataAdapter(sqlQuery, conn);
        DataSet ds = new DataSet();
        da.Fill(ds, "invc");
        conn.Close();
        DataRowView row = ds.Tables["invc"].DefaultView[0];
        string comp = row["Completed"].ToString();
        bool CheckJob = Convert.ToBoolean(row["OldJB"]);
        if (comp == "False")
        {
            string Bill = rbBill.SelectedValue;
            string BillCode = gvReport.SelectedRow.Cells[9].Text;

            string linkQuery = "select * from M_Link  ";
            SqlConnection conn1 = new SqlConnection(strImpex);
            conn1.Open();
            SqlDataAdapter da1 = new SqlDataAdapter(linkQuery, conn1);
            DataSet ds1 = new DataSet();
            da1.Fill(ds1, "Link");
            conn.Close();
            DataRowView row1 = ds1.Tables["Link"].DefaultView[0];
            string InvOld = row1["InvOld"].ToString();
            string InvNew = row1["InvNew"].ToString();
            string DebOld = row1["DebOld"].ToString();
            string DebNew = row1["DebNew"].ToString();

            if (Bill == "SB")
            {
                if (CheckJob == true)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "popup", "window.open('" + InvOld + "?invNo=" + iNO + "','_blank','width=1120,height=720, menubar=no, scrollbars=yes, toolbar=no, location=no, resizable=no, left=10, top=120');", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "popup", "window.open('EditInvoiceStaxUp.aspx?invNo=" + iNO + "','_blank','width=1120,height=720, menubar=no, scrollbars=yes, toolbar=no, location=no, resizable=no, left=10, top=120');", true);
                }
            }
            else
            {
                if (CheckJob == true)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "popup", "window.open('" + DebOld + "?invNo=" + iNO + "','_blank','width=1120,height=720, menubar=no, scrollbars=yes, toolbar=no, location=no, resizable=no, left=10, top=120');", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "popup", "window.open('EditDebitNoteDTLUp.aspx?invNo=" + iNO + "','_blank','width=1120,height=720, menubar=no, scrollbars=yes, toolbar=no, location=no, resizable=no, left=10, top=120');", true);
                }
            }
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Billing has been closed for the invoice no "+iNO+". Please contact Accounts Department for edit the Bill');", true);
        }   
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
    protected void chkJobs_CheckedChanged(object sender, EventArgs e)
    {
        if (chkJobs.Checked)
            txtJobNo.Enabled = true;
        else
        {
            txtJobNo.Enabled = false;
            txtJobNo.Text = "";
        }
    }
}
