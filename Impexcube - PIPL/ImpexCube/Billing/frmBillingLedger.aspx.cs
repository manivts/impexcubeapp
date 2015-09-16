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

public partial class frmBillingLedger : System.Web.UI.Page
{
   // string strconn = (string)ConfigurationSettings.AppSettings["ConnectionImpex"];

    string strconn = (string)ConfigurationManager.ConnectionStrings["Constr"].ConnectionString;
    #region
    Double totaledu=0;
    Double totalST=0;
    Double totalINV=0;
    Double totalshe=0;
    Double totalAMT = 0;
    string servceID = "";
    #endregion
    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack == false)
        {
            tr1.Visible = false;
            rbSHp.SelectedValue = "IMP";
            rbBill.SelectedValue = "SB";
            DivTag.Visible = false;
          
            string fy = (string)Session["FinancialYear"];
            getSDate(fy);
            txtPName.Enabled = false;
            txtLedger.Enabled = false;
            
        }
    }
    protected void getSDate(string fy)
    {
        try
        {
            SqlConnection conn = new SqlConnection(strconn);
            string Query = "select * from M_RunningNo where fyear='" + fy + "'";
            SqlDataAdapter da = new SqlDataAdapter(Query, conn);
            DataSet ds = new DataSet();
            da.Fill(ds, "FYEAR");
            DataRowView row = ds.Tables["FYEAR"].DefaultView[0];
            DateTime sDATES = Convert.ToDateTime(row["sDATE"].ToString());
            txtFrom.Text = sDATES.ToString("dd/MM/yyyy");
            txtTo.Text = System.DateTime.Now.ToString("dd/MM/yyyy");
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('" + ex.Message + "');", true);
        }
    }
    protected void Btn_search_Click(object sender, EventArgs e)
    {

        
        string[] DT;
        string dt = "";

        string Query = "";
        string fdate = txtFrom.Text;
        string tdate = txtTo.Text;
        string Bill = rbBill.SelectedValue;
        string shipment = rbSHp.SelectedValue;
        tr1.Visible = true;
        
        if (tdate == "")
            tdate = fdate;

        DT = fdate.Split('/');
        dt = DT[2] + "-" + DT[1] + "-" + DT[0];
        fdate = dt;

        DT = tdate.Split('/');
        dt = DT[2] + "-" + DT[1] + "-" + DT[0];
        tdate = dt;
        string billDTL = "";
        string billMst = "";
        string billMstDTL = "";
        string pName = txtPName.Text;
        string Ledger=txtLedger.Text;
        string staxPer = "";
        string ViewName = "";
        if (Bill == "SB")
        {
            ViewName = "View_Invoice";
            //billDTL = "T_iec_invoiceNew_dtl";
            //billMst = "M_iec_invoiceNew";
            //billMstDTL = "M_iec_invoiceNew i,T_iec_invoiceNew_dtl j";
            if (ddlServiceTax.SelectedValue != "~Select~")
            {
                staxPer = "and servicetaxpercent=" + ddlServiceTax.SelectedValue;
            }
        }
        else
        {
            ViewName = "View_DebitNote";
            //billDTL = "T_iec_debit_dtl";
            //billMst = "M_iec_debit";
            //billMstDTL = "M_iec_debit i, T_iec_debit_dtl j";
        }
        //string QueryT = "select * from " + billDTL + " where  charge_desc like '" + Ledger + "%' ";
        //DataSet dsT = GetData(QueryT);
        //DataTable dtT = dsT.Tables[0];
       
        //foreach (DataRow rowT in dtT.Rows)
        //{
        //    servceID = rowT["serviceTax"].ToString();

          
        //}
            
                if (pName == "" && Ledger=="")
                {
                    Query = "SELECT InvDate, jobno, invoice, compName, SUM(ServiceTaxAmount) AS SERVICETAXAMOUNT,Edu_cess,SEC_Chess,SUM(ServiceTaxAmount)+Edu_cess+SEC_Chess  as Total FROM  "+ ViewName+" " +
                        " where InvDate  between '" + fdate + "' and '" + tdate + "' and mode='" + shipment + "' " + staxPer + " " +
                        " GROUP BY InvDate, jobno, invoice, compName,Edu_cess,SEC_Chess  ORDER BY invoice,InvDate";
                    //Query = "select * from  " + billMst + "  where invoiceDate  between '" + fdate + "' and '" + tdate + "' " +
                    //         " and invoiceType like '%" + Bill + "%' and mode='" + shipment + "'    order by invoiceDate";
                    ServiceReport(Query);
                    ServiceFooterCalculation();
                }
                else if (pName != "" && Ledger=="")
                {
                    Query = "SELECT InvDate, jobno, invoice, compName, SUM(ServiceTaxAmount) AS SERVICETAXAMOUNT,Edu_cess,SEC_Chess,SUM(ServiceTaxAmount)+Edu_cess+SEC_Chess  as Total FROM  " + ViewName + " " +
                       " where InvDate  between '" + fdate + "' and '" + tdate + "' and mode='" + shipment + "' " + staxPer + " AND compName='" + pName + "' " +
                       " GROUP BY InvDate, jobno, invoice, compName,Edu_cess,SEC_Chess  ORDER BY invoice,InvDate";
                    //Query = "select * from  " + billMst + "  where invoiceDate  between '" + fdate + "' and '" + tdate + "' " +
                    //    " and invoiceType like '%" + Bill + "%' and mode='" + shipment + "' and compName='" + pName + "' " + staxPer + "  order by invoiceDate";
                    ServiceReport(Query);
                    ServiceFooterCalculation();
                }
                else if (pName != "" && Ledger != "")
                {
                    Query = "SELECT InvDate, jobno, invoice, compName, SUM(ServiceTaxAmount) AS SERVICETAXAMOUNT,Edu_cess,SEC_Chess,SUM(ServiceTaxAmount)+Edu_cess+SEC_Chess  as Total,Amount FROM  " + ViewName + " " +
                      " where InvDate  between '" + fdate + "' and '" + tdate + "' and mode='" + shipment + "' " + staxPer + " AND compName='" + pName + "' AND charge_desc like '" + Ledger + "%' " +
                      " GROUP BY InvDate, jobno, invoice, compName,Edu_cess,SEC_Chess,Amount  ORDER BY invoice,InvDate";
                    //Query = "select * from  " + billMstDTL  + "  where i.invoice=j.invoice and i.invoiceDate  between '" + fdate + "' and '" + tdate + "' " +
                    //    " and i.invoiceType like '%" + Bill + "%' and i.mode='" + shipment + "' and i.compName='" + pName + "' and j.charge_desc like '" + Ledger + "%' order by i.invoiceDate";
                    Report(Query);
                    FooterCalculation();
                    gvService.DataBind();
                }
                else if (pName == "" && Ledger != "")
                {
                    Query = "SELECT InvDate, jobno, invoice, compName, SUM(ServiceTaxAmount) AS SERVICETAXAMOUNT,Edu_cess,SEC_Chess,SUM(ServiceTaxAmount)+Edu_cess+SEC_Chess  as Total,Amount FROM  " + ViewName + " " +
                     " where InvDate  between '" + fdate + "' and '" + tdate + "' and mode='" + shipment + "' " + staxPer + "  AND charge_desc like '" + Ledger + "%' " +
                     " GROUP BY InvDate, jobno, invoice, compName,Edu_cess,SEC_Chess,Amount  ORDER BY invoice,InvDate";
                    //Query = "select * from  " + billMstDTL + "  where i.invoice=j.invoice and i.invoiceDate  between '" + fdate + "' and '" + tdate + "' " +
                    //    " and i.invoiceType like '%" + Bill + "%' and i.mode='" + shipment + "' and j.charge_desc like '" + Ledger + "%' order by i.invoiceDate";
                    Report(Query);
                    FooterCalculation();
                    gvService.DataBind();
                }
                else
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Incorrect Dates');", true);

                //FooterCalculation();
        Session["BillTY"] = null;
    }

    public void FooterCalculation()
    {
        try
        {
            double a = 0;
            double b = 0;
            double c = 0;
            double d = 0;
            double e = 0;
            int i = 0;
            foreach (GridViewRow gv in gvReport.Rows)
            {
                string amt = gvReport.Rows[i].Cells[5].Text;
                string amt1 = gvReport.Rows[i].Cells[6].Text;
                string amt3 = gvReport.Rows[i].Cells[7].Text;
                string amt4 = gvReport.Rows[i].Cells[8].Text;
                string amt5 = gvReport.Rows[i].Cells[9].Text;
              

                a = a + Convert.ToDouble(amt);
                b = b + Convert.ToDouble(amt1);
                c = c + Convert.ToDouble(amt3);
                d = d + Convert.ToDouble(amt4);
                e = d + Convert.ToDouble(amt5); 
                i++;
            }
            gvReport.FooterRow.Cells[5].Text = a.ToString("#0.00");
            gvReport.FooterRow.Cells[6].Text = b.ToString("#0.00"); 
            gvReport.FooterRow.Cells[7].Text = c.ToString("#0.00"); 
            gvReport.FooterRow.Cells[8].Text = d.ToString("#0.00");
            gvReport.FooterRow.Cells[9].Text = e.ToString("#0.00");

        }
        catch (Exception ex)
        {
            string Message = ex.Message;
        }
    }


    protected void chkJobNo_CheckedChanged(object sender, EventArgs e)
    {

       
    }
    protected void Report(string Cmd)
    {
        SqlConnection conn = new SqlConnection(strconn);
        conn.Open();
        try
        {
            SqlCommand com = new SqlCommand(Cmd, conn);
            SqlDataAdapter da = new SqlDataAdapter(com);
            DataSet ds = new DataSet();
            da.Fill(ds, "PARTY");
            if (ds.Tables[0].Rows.Count != 0)
            {
                gvReport.DataSource = ds;
                gvReport.DataBind();

                DivTag.Visible = true;
                gvReport.Visible = true;
            }
            else
            {
                gvReport.Visible = false;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Records not found for given values');", true);
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('" + ex.Message + "');", true);
        }
    }



    protected void ServiceReport(string Cmd)
    {
        gvReport.DataBind();
        SqlConnection conn = new SqlConnection(strconn);
        conn.Open();
        try
        {
            SqlCommand com = new SqlCommand(Cmd, conn);
            SqlDataAdapter da = new SqlDataAdapter(com);
            DataSet ds = new DataSet();
            da.Fill(ds, "PARTY");
            if (ds.Tables[0].Rows.Count != 0)
            {
                gvService.DataSource = ds;
                gvService.DataBind();

                DivTag.Visible = false;
                gvService.Visible = true;
            }
            else
            {
                gvService.Visible = false;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Records not found for given values');", true);
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('" + ex.Message + "');", true);
        }
    }

    public void ServiceFooterCalculation()
    {
        try
        {
            double a = 0;
            double b = 0;
            double c = 0;
            double d = 0;
            double e = 0;
            int i = 0;
            foreach (GridViewRow gv in gvService.Rows)
            {
                string amt = gvService.Rows[i].Cells[5].Text;
                string amt1 = gvService.Rows[i].Cells[6].Text;
                string amt3 = gvService.Rows[i].Cells[7].Text;
                string amt4 = gvService.Rows[i].Cells[8].Text;
                //string amt5 = gvReport.Rows[i].Cells[9].Text;


                a = a + Convert.ToDouble(amt);
                b = b + Convert.ToDouble(amt1);
                c = c + Convert.ToDouble(amt3);
                d = d + Convert.ToDouble(amt4);
               // e = d + Convert.ToDouble(amt5);
                i++;
            }
            gvService.FooterRow.Cells[5].Text = a.ToString("#0.00");
            gvService.FooterRow.Cells[6].Text = b.ToString("#0.00");
            gvService.FooterRow.Cells[7].Text = c.ToString("#0.00");
            gvService.FooterRow.Cells[8].Text = d.ToString("#0.00");
            //gvReport.FooterRow.Cells[9].Text = e.ToString("#0.00");

        }
        catch (Exception ex)
        {
            string Message = ex.Message;
        }
    }


    protected void btnExport_Click(object sender, EventArgs e)
    {

        if (txtPName.Text == "" && txtLedger.Text == "")
        {
            GridViewExportDet.ExportExcell("LedgerDetails.xls", gvService);
        }
        else if (txtPName.Text != "" && txtLedger.Text == "")
        {
            GridViewExportDet.ExportExcell("LedgerDetails.xls", gvService);
        }
        else
        {
            GridViewExportDet.ExportExcell("LedgerDetails.xls", gvReport);
        }
    }
    protected void gvReport_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        string Query = "";
        string bill = rbBill.SelectedValue;
        string billDTL="";
        
        string ledgerName=txtLedger.Text;
        int i = 1;
        string staxPer = "";
        Double stax = 0;
        Double edCS = 0;
        Double shCS = 0;
        Double inval = 0;
        Double SPER = 0;
      
        Double chgAmt = 0;
       
       
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if (e.Row.Cells[9].Text == "")
            {
                e.Row.Cells[9].Visible = false;
            }

            //i = i + 1;
           
            //if (e.Row.Cells[1].Text != "&nbsp;")
            //{
            //    DateTime bDate = Convert.ToDateTime(e.Row.Cells[1].Text);
            //    e.Row.Cells[1].Text = bDate.ToString("dd/MM/yyyy");
            //}
            
            //string jobno = e.Row.Cells[2].Text;
            //int j = 0;
            //string[] jno = jobno.Split('/');
            //foreach (string strThisTo in jno)
            //{
            //    j = j + 1;
            //}
            //if(j>=2)
            //    e.Row.Cells[2].Text = jno[1];
            //else
            //    e.Row.Cells[2].Text = jno[0];
            //string billno = e.Row.Cells[3].Text;
            //if (bill == "SB")
            //{
            //    Query = "select * from M_iec_invoiceNew where invoice='" + billno + "'";
            //    DataSet ds1 = GetData(Query);
            //    DataTable dt1 = ds1.Tables[0];
            //    foreach (DataRow ROW in dt1.Rows)
            //    {
            //        staxPer = ROW["STaxPercent"].ToString();
            //    }
            //    billDTL = "T_iec_invoiceNew_dtl";
            //}
            //else
            //    billDTL = "T_iec_debit_dtl";
            //Query = "select * from " + billDTL + " where invoice='" + billno + "' and charge_desc like '" + ledgerName + "%' ";
            //DataSet ds = GetData(Query);
            //DataTable dt = ds.Tables[0];
           
            //foreach (DataRow row in dt.Rows)
            //{
            //    servceID = row["serviceTax"].ToString();
            //    e.Row.Cells[5].Text = row["amount"].ToString();
            //}
            //if (ledgerName == "")
            //{
            //    if (bill == "SB" && servceID=="Y")
            //    {

            //        stax = Convert.ToDouble(e.Row.Cells[6].Text);
            //        edCS = Convert.ToDouble(e.Row.Cells[7].Text);
            //        shCS = Convert.ToDouble(e.Row.Cells[8].Text);
            //        inval = stax + edCS + shCS;


            //        totalST = totalST + stax;
            //        totaledu = totaledu + edCS;
            //        totalshe = totalshe + shCS;

            //        totalINV = totalINV + inval;

            //        e.Row.Cells[6].Text = stax.ToString("#0.00");
            //        e.Row.Cells[7].Text = edCS.ToString("#0.00");
            //        e.Row.Cells[8].Text = shCS.ToString("#0.00");

            //        e.Row.Cells[9].Text = inval.ToString("#0.00");
            //    }
            //    else
            //    {
            //        stax = 0;
            //        edCS = 0;
            //        shCS = 0;
            //        e.Row.Cells[6].Text = stax.ToString("#0.00");
            //        e.Row.Cells[7].Text = edCS.ToString("#0.00");
            //        e.Row.Cells[8].Text = shCS.ToString("#0.00");
            //    }
            //}
            //else
            //{

            //    string amt = e.Row.Cells[5].Text;
            //    if (amt == "" || amt == string.Empty)
            //        amt = "0";
            //    Double AMT = Convert.ToDouble(amt);
            //    totalAMT = totalAMT + AMT;

            //    e.Row.Cells[5].Text=AMT.ToString("#0.00");
            //    if (bill == "SB" && servceID == "Y")
            //    {
            //        Double stPer = Convert.ToDouble(staxPer);
            //        Double STAX = (AMT * stPer / 100);
            //        Double EDU = STAX * 2 / 100;
            //        Double SHE = EDU / 2;


            //        totalST = totalST + STAX;
            //        totaledu = totaledu + EDU;
            //        totalshe = totalshe + SHE;
            //        inval = AMT + STAX + EDU + SHE;
            //        totalINV = totalINV + inval;

            //        e.Row.Cells[6].Text = STAX.ToString("#0.00");
            //        e.Row.Cells[7].Text = EDU.ToString("#0.00");
            //        e.Row.Cells[8].Text = SHE.ToString("#0.00");
            //    }
            //    else
            //    {

            //        stax = 0;
            //        edCS = 0;
            //        shCS = 0;
            //        e.Row.Cells[6].Text = stax.ToString("#0.00");
            //        e.Row.Cells[7].Text = edCS.ToString("#0.00");
            //        e.Row.Cells[8].Text = shCS.ToString("#0.00");
            //        inval = AMT;
            //        totalINV = totalINV + inval;

                    
            //    }
            //    e.Row.Cells[9].Text = inval.ToString("#0.00");
            //}
            //if (txtLedger.Text == "")
            //    e.Row.Cells[5].Visible = false;
          
        }
        if (e.Row.RowType == DataControlRowType.Header)
        {
            if (e.Row.Cells[9].Text == "Amount")
            {
                if (txtLedger.Text == "")
                {
                    e.Row.Cells[9].Visible = false;
                }
            }
            //e.Row.Cells[5].Text = ledgerName;
            //if (txtLedger.Text == "")
            //    e.Row.Cells[5].Visible = false;
            //if (bill == "SB" && servceID == "Y")
            //{
            //    e.Row.Cells[6].Visible = true;
            //    e.Row.Cells[7].Visible = true;
            //    e.Row.Cells[8].Visible = true;
            //}
            //else 
            //{
            //    e.Row.Cells[6].Visible = true;
            //    e.Row.Cells[7].Visible = true;
            //    e.Row.Cells[8].Visible = true;
            //}
        }
        if (e.Row.RowType == DataControlRowType.Footer)
        {


            if (bill == "SB" && servceID == "Y")
            {
               
                e.Row.Cells[6].Text = totalST.ToString("#0.00");
                e.Row.Cells[7].Text = totaledu.ToString("#0.00");
                e.Row.Cells[8].Text = totalshe.ToString("#0.00");
                e.Row.Cells[9].Text = totalINV.ToString("#0.00");
                e.Row.Cells[6].Visible = true;
                e.Row.Cells[7].Visible = true;
                e.Row.Cells[8].Visible = true;
            }
            else
            {
                e.Row.Cells[6].Visible = true;
                e.Row.Cells[7].Visible = true;
                e.Row.Cells[8].Visible = true;
                e.Row.Cells[6].Text = totalST.ToString("#0.00");
                e.Row.Cells[7].Text = totaledu.ToString("#0.00");
                e.Row.Cells[8].Text = totalshe.ToString("#0.00");
                e.Row.Cells[9].Text = totalINV.ToString("#0.00");
            }
            if (txtLedger.Text == "")
                e.Row.Cells[5].Visible = false;
            else
            {
                e.Row.Cells[5].Text = totalAMT.ToString("#0.00"); 
            }
           
            
        }
       
       
    }
    public DataSet GetData(string sqlQuery)
    {
        SqlConnection conn = new SqlConnection(strconn);
        SqlDataAdapter da = new SqlDataAdapter(sqlQuery, conn);
        DataSet ds = new DataSet();
        da.Fill(ds, "invc");
        return ds;
    }
   
    protected void ExportPDF_Click(object sender, EventArgs e)
    {
        string fDate = txtFrom.Text;
        string tDate = txtTo.Text;
        
        Session["fDate"] = fDate;
        Session["tDate"] = tDate;
      
        
        Session["PNAME"] = txtPName.Text;
      
            Page.ClientScript.RegisterStartupScript(this.GetType(), "open", "window.open('CrySTax.aspx?startDT=" + fDate + "&endDT=" + tDate + "','','menubar=no, scrollbars=yes, toolbar=no, location=no, resizable=yes,left=0,top=0');", true);
      
     

    }
    protected void chkIMP_CheckedChanged(object sender, EventArgs e)
    {
        if (chkIMP.Checked == true)
        {
            Session["BillTY"] = "SB";

            txtPName.Enabled = true;
            txtPName.Text = "";
        }
        else if (chkIMP.Checked == false)
        {
            Session["BillTY"] = null;
            txtPName.Enabled = false;
            txtPName.Text = "";
        }
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

    protected void chkLedger_CheckedChanged(object sender, EventArgs e)
    {
        if (chkLedger.Checked == true)
        {
            Session["BillTY"] = "SB";
            Session["PNAME"] = txtPName.Text;
            txtLedger.Enabled = true;
          
        }
        else if (chkLedger.Checked == false)
        {
            Session["BillTY"] = null;
            Session["PNAME"] = txtPName.Text;
            txtLedger.Enabled = false;
            txtLedger.Text = "";
           
        }
    }
}
