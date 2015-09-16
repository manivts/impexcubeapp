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

public partial class BillingReport : System.Web.UI.Page
{
    //string strconn = (string)ConfigurationSettings.AppSettings["ConnectionImpex"];
    string strImpex = (string)ConfigurationManager.ConnectionStrings["Constr"].ConnectionString;
    Double Total;
    Double TotalG;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack == false)
        {
            DivTag.Visible = false;
            string fy = (string)Session["FinancialYear"];
            getSDate(fy);
            txtPName.Enabled = false;
            gvReport0.Visible=false;
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
            txtFrom.Text = sDATES.ToString("MM/dd/yyyy");
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
        if (tdate == "")
            tdate = fdate;

        DT = fdate.Split('/');
        dt = DT[2] + "-" + DT[1] + "-" + DT[0];
        fdate = dt;

        DT = tdate.Split('/');
        dt = DT[2] + "-" + DT[1] + "-" + DT[0];
        tdate = dt;

        string Btype = drpBType.SelectedValue;
        string pName = txtPName.Text;
        string jno = txtJobNo.Text;
        if (Btype == "SB" || Btype == "ATLSB" || Btype == "EXPSB")
        {
            if (chkJobNo.Checked == true)
            {
                if (jno != "" || jno != string.Empty)
                {
                    Query = "select * from M_iec_invoiceNew where (jobno='" + jno + "') order by invoiceno,invoiceDate";
                    Report(Query);
                }
                else
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Incorrect Job Number');", true);

            }
            else
            {
                if (fdate != "" && tdate != "" && pName == "")
                {
                    Query = "select * from M_iec_invoicenew where invoiceDate  between '" + fdate + "' and '" + tdate + "' and invoiceType='" + Btype + "' order by invoiceno,invoiceDate";
                    Report(Query);
                }
                else if (fdate != "" && tdate != "" && pName != "")
                {
                    Query = "select * from M_iec_invoicenew where invoiceDate  between '" + fdate + "' and '" + tdate + "' and compName='" + pName + "' and invoiceType='" + Btype + "' order by invoiceno,invoiceDate";
                    Report(Query);
                }
                else
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Incorrect Dates');", true);


            }

        }
        else if (Btype == "DB" || Btype == "ATLDB" || Btype == "CD" || Btype == "ATLDEM" || Btype == "ATLDEPB" || Btype == "EXPDB")
        {
            if (chkJobNo.Checked == true)
            {
                if (jno != "" || jno != string.Empty)
                {
                    Query = "select * from M_iec_debit where (jobno='" + jno + "') order by invoiceno,invoiceDate";
                    Report(Query);
                }
                else
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Incorrect Job Number');", true);

            }
            else
            {
                if (fdate != "" && tdate != "" && pName == "")
                {
                    Query = "select * from M_iec_debit where invoiceDate  between '" + fdate + "' and '" + tdate + "' and invoiceType='" + Btype + "' order by invoiceno,invoiceDate";
                    Report(Query);
                }
                else if (fdate != "" && tdate != "" && pName != "")
                {
                    Query = "select * from M_iec_debit where invoiceDate  between '" + fdate + "' and '" + tdate + "' and compName='" + pName + "' and invoiceType='" + Btype + "' order by invoiceno,invoiceDate";
                    Report(Query);
                }
                else
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Incorrect Dates');", true);
            }


        }
        else if (Btype == "IA")
        {
            if (chkJobNo.Checked == true)
            {
                if (jno != "" || jno != string.Empty)
                {
                    Query = "select * from M_iec_invoicenew where (jobno='" + jno + "') order by invoiceno,invoiceDate";
                    Report(Query);
                }
                else
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Incorrect Job Number');", true);

            }
            else
            {
                if (fdate != "" && tdate != "" && pName == "")
                {
                    Query = "select * from M_iec_invoicenew where invoiceDate  between '" + fdate + "' and '" + tdate + "' order by invoiceno,invoiceDate";
                    Report(Query);
                }
                else if (fdate != "" && tdate != "" && pName != "")
                {
                    Query = "select * from M_iec_invoicenew where invoiceDate  between '" + fdate + "' and '" + tdate + "' and compName='" + pName + "' order by invoiceno,invoiceDate ";
                    Report(Query);
                }
                else
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Incorrect Dates');", true);


            }
        }
        else if (Btype == "DA")
        {
            if (chkJobNo.Checked == true)
            {
                if (jno != "" || jno != string.Empty)
                {
                    Query = "select * from M_iec_debit where (jobno='" + jno + "') order by invoiceno,invoiceDate";
                    Report(Query);
                }
                else
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Incorrect Job Number');", true);

            }
            else
            {
                if (fdate != "" && tdate != "" && pName == "")
                {
                    Query = "select * from M_iec_debit where invoiceDate  between '" + fdate + "' and '" + tdate + "' order by invoiceno, invoiceDate ";
                    Report(Query);
                }
                else if (fdate != "" && tdate != "" && pName != "")
                {
                    Query = "select * from M_iec_debit where invoiceDate  between '" + fdate + "' and '" + tdate + "' and compName='" + pName + "' order by invoiceno,invoiceDate";
                    Report(Query);
                }
                else
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Incorrect Dates');", true);
            }
        }
        else
        {
            // Union Query for Party 
            if (chkJobNo.Checked == true)
            {
                Query = "select * from M_iec_debit i where i.jobno='" + jno + "' order by i.invoiceno,i.invoiceDate  union " +
                        "select * from M_iec_invoicenew j where j.jobno='" + jno + "' order by j.invoiceno,j.invoiceDate";
                Report(Query);
            }
            else
            {
                if (pName == "" && Btype =="0")
                {
                    Query = "select * from M_iec_debit i where i.invoiceDate between '" + fdate + "' and '" + tdate + "' " +
                             "order by i.invoiceno,i.invoiceDate  union " +
                             "select * from M_iec_invoicenew j where j.invoiceDate between '" + fdate + "' and '" + tdate + "' " +
                             "order by j.invoiceno,j.invoiceDate ";
                    Report(Query);

                }
                else
                {
                    Query = "select * from M_iec_debit i where i.invoiceDate between '" + fdate + "' and '" + tdate + "' " +
                          "and  i.compName ='" + pName + "' order by i.invoiceno,i.invoiceDate  union " +
                          "select * from M_iec_invoicenew j where j.invoiceDate between '" + fdate + "' and '" + tdate + "' " +
                          "and j.compName ='" + pName + "' order by j.invoiceno,j.invoiceDate";
                    Report(Query);
                }
            }
        }
          
        Session["BillTY"] = null;
    }
  
    
    protected void chkJobNo_CheckedChanged(object sender, EventArgs e)
    {

        if (chkJobNo.Checked == true)
        {
            txtJobNo.Enabled = true;
            txtJobNo.Text = "";
        }
        else if (chkJobNo.Checked == false)
        {
            txtJobNo.Enabled = false;
           
        }
    }
    protected void Report(string Cmd)
    {
        SqlConnection conn = new SqlConnection(strImpex);
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
                gvReport0.DataSource = ds;
                gvReport0.DataBind();
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
    protected void drpBType_SelectedIndexChanged(object sender, EventArgs e)
    {
        Session["BillTY"] = drpBType.SelectedValue;
    }
    protected void btnExport_Click(object sender, EventArgs e)
    {
        txtFrom.Text = txtJobNo.Text = txtPName.Text = txtTo.Text = "";
        chkJobNo.Checked = false;
        drpBType.SelectedValue = "0";
        txtJobNo.Enabled = false;
        gvReport.Visible=false;
        gvReport0.Visible=true;
        GridViewExportDet.ExportExcell("Billing.xls", gvReport0);
        gvReport.Visible=true;
        gvReport0.Visible=false;
    }
   
    
    protected void gvReport_SelectedIndexChanged(object sender, EventArgs e)
    {
        Session["BillNo"] = gvReport.SelectedRow.Cells[1].Text.ToString();
        Session["BillType"] = gvReport.SelectedRow.Cells[12].Text.ToString();
        ScriptManager.RegisterStartupScript(this, this.GetType(), "Error", "window.open('frmPIPLinvoice.aspx','_blank','width=800,height=900, menubar=no, scrollbars=yes, toolbar=no, location=no, resizable=No, left=50, top=75');", true);

    }
    protected void gvReport_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        string Query = "";
        string stotal = "";
        string stotalTax = "";
        string bType = drpBType.SelectedValue;
       
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string bill = e.Row.Cells[12].Text;
            if (e.Row.Cells[2].Text != "&nbsp;")
            {
                DateTime bDate = Convert.ToDateTime(e.Row.Cells[2].Text);
                e.Row.Cells[2].Text = bDate.ToString("dd/MM/yyyy");
            }
            string invno = gvReport.DataKeys[e.Row.RowIndex].Value.ToString();
         
            if (bill == "SB" || bill == "ATLSB")
                Query = "select * from M_iec_invoicenew where invoice='" + invno + "'";
            else
                Query = "select * from M_iec_debit where invoice='" + invno + "'";
            DataSet ds = new DataSet();
            ds = GetData(Query);
            if (ds.Tables["invc"].Rows.Count != 0)
            {
                DataRowView row = ds.Tables["invc"].DefaultView[0];
                stotal = row["subtotal"].ToString();
                stotalTax = row["subtotalTax"].ToString();
            }

            if (stotal == "")
                stotal = "0";
            if (stotalTax == "")
                stotalTax = "0";

            Double subamt = Convert.ToDouble(stotal) + Convert.ToDouble(stotalTax);
            e.Row.Cells[13].Text = subamt.ToString("#0.00");

           
            if (bType == "SB" || bType == "ATLSB" || bType == "IA")
            {
                if (e.Row.Cells[14].Text == "" || e.Row.Cells[14].Text == "&nbsp;")
                    e.Row.Cells[14].Text = "0";
                if (e.Row.Cells[15].Text == "" || e.Row.Cells[15].Text == "&nbsp;")
                    e.Row.Cells[15].Text = "0";
                if (e.Row.Cells[16].Text == "" || e.Row.Cells[16].Text == "&nbsp;")
                    e.Row.Cells[16].Text = "0";
                if (e.Row.Cells[17].Text == "" || e.Row.Cells[17].Text == "&nbsp;")
                    e.Row.Cells[17].Text = "0";
                if (e.Row.Cells[18].Text == "" || e.Row.Cells[18].Text == "&nbsp;")
                    e.Row.Cells[18].Text = "0";
               

                Double Samt = Convert.ToDouble(e.Row.Cells[14].Text);
                e.Row.Cells[14].Text = Samt.ToString("#0.00");

                Double Eamt = Convert.ToDouble(e.Row.Cells[15].Text);
                e.Row.Cells[15].Text = Eamt.ToString("#0.00");

                Double SHamt = Convert.ToDouble(e.Row.Cells[16].Text);
                e.Row.Cells[16].Text = SHamt.ToString("#0.00");
            }
            else if (bType == "0")
            {
                e.Row.Cells[13].Visible = true;
                e.Row.Cells[14].Visible = true;
                e.Row.Cells[15].Visible = true;
                e.Row.Cells[16].Visible = true;
            }
            else
            {
                e.Row.Cells[13].Visible = false;
                e.Row.Cells[14].Visible = false;
                e.Row.Cells[15].Visible = false;
                e.Row.Cells[16].Visible = false;
            }

            
            if (e.Row.Cells[17].Text == "" || e.Row.Cells[17].Text == "&nbsp;")
                e.Row.Cells[17].Text = "0";
            if (e.Row.Cells[18].Text == "" || e.Row.Cells[18].Text == "&nbsp;")
                e.Row.Cells[18].Text = "0";
            if (e.Row.Cells[19].Text == "" || e.Row.Cells[19].Text == "&nbsp;")
                e.Row.Cells[19].Text = "0";
            Double Gamt = Convert.ToDouble(e.Row.Cells[17].Text);
            e.Row.Cells[17].Text = Gamt.ToString("#0.00");

            Double Aamt = Convert.ToDouble(e.Row.Cells[18].Text);
            e.Row.Cells[18].Text = Aamt.ToString("#0.00");
           
            Double amt = Convert.ToDouble(e.Row.Cells[19].Text);
            e.Row.Cells[19].Text = amt.ToString("#0.00");
            Total = Total + amt;

        }
        if (e.Row.RowType == DataControlRowType.Header)
        {
            
            if (bType == "DB" || bType == "ATLDB" || bType == "DA")
            {
                e.Row.Cells[13].Visible = false;
                e.Row.Cells[14].Visible = false;
                e.Row.Cells[15].Visible = false;
                e.Row.Cells[16].Visible = false;
            }
            else if (bType == "0")
            {
                e.Row.Cells[13].Visible = true;
                e.Row.Cells[14].Visible = true;
                e.Row.Cells[15].Visible = true;
                e.Row.Cells[16].Visible = true;
            }
        }
        if (e.Row.RowType == DataControlRowType.Footer)
        {
            
            e.Row.Cells[19].Text = Total.ToString("#0.00");
            e.Row.Cells[18].Text = "Total Rs.";
            if (bType == "DB" || bType == "ATLDB" || bType == "DA")
            {
                e.Row.Cells[13].Visible = false;
                e.Row.Cells[14].Visible = false;
                e.Row.Cells[15].Visible = false;
                e.Row.Cells[16].Visible = false;
            }
            else if (bType == "0")
            {
                e.Row.Cells[13].Visible = true;
                e.Row.Cells[14].Visible = true;
                e.Row.Cells[15].Visible = true;
                e.Row.Cells[16].Visible = true;
            }
        }
    }
    public DataSet GetData(string sqlQuery)
    {
        SqlConnection conn = new SqlConnection(strImpex);
        SqlDataAdapter da = new SqlDataAdapter(sqlQuery, conn);
        DataSet ds = new DataSet();
        da.Fill(ds, "invc");
        return ds;
    }
    protected void gvReport0_RowDataBound(object sender, GridViewRowEventArgs e)
    {
         string Query="";
         string stotal = "";
         string stotalTax = "";
         string bType = drpBType.SelectedValue;
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lbltotal = (Label)e.Row.FindControl("lblTotal");
            if (e.Row.Cells[1].Text != "&nbsp;")
            {
                DateTime bDate = Convert.ToDateTime(e.Row.Cells[1].Text);
                e.Row.Cells[1].Text = bDate.ToString("dd/MM/yyyy");
            }
            string invno = gvReport0.DataKeys[e.Row.RowIndex].Value.ToString();
            
            if(bType=="SB" || bType=="ATLSB")
               Query = "select * from M_iec_invoicenew where invoice='" + invno + "'";
            else
               Query = "select * from M_iec_debit where invoice='" + invno + "'";
            DataSet ds = new DataSet();
            ds=GetData(Query);
            if (ds.Tables["invc"].Rows.Count != 0)
            {
                DataRowView row = ds.Tables["invc"].DefaultView[0];
                stotal = row["subtotal"].ToString();
                stotalTax = row["subtotalTax"].ToString();
            }

            if (stotal == "")
                stotal = "0";
            if (stotalTax == "")
                stotalTax = "0";

            Double subamt = Convert.ToDouble(stotal) + Convert.ToDouble(stotalTax);
            lbltotal.Text = subamt.ToString("#0.00");

            if (bType == "SB" || bType == "ATLSB" || bType == "IA")
            {
                
                if (e.Row.Cells[13].Text == "" || e.Row.Cells[13].Text == "&nbsp;")
                    e.Row.Cells[13].Text = "0";
                if (e.Row.Cells[14].Text == "" || e.Row.Cells[14].Text == "&nbsp;")
                    e.Row.Cells[14].Text = "0";
                if (e.Row.Cells[15].Text == "" || e.Row.Cells[15].Text == "&nbsp;")
                    e.Row.Cells[15].Text = "0";
               

               
               
                Double Samt = Convert.ToDouble(e.Row.Cells[13].Text);
                e.Row.Cells[13].Text = Samt.ToString("#0.00");

                Double Eamt = Convert.ToDouble(e.Row.Cells[14].Text);
                e.Row.Cells[14].Text = Eamt.ToString("#0.00");

                Double SHamt = Convert.ToDouble(e.Row.Cells[15].Text);
                e.Row.Cells[15].Text = SHamt.ToString("#0.00");

            }
            else if (bType == "0")
            {
                e.Row.Cells[12].Visible = true;
                e.Row.Cells[13].Visible = true;
                e.Row.Cells[14].Visible = true;
                e.Row.Cells[15].Visible = true;
            }
            else
            {
                e.Row.Cells[12].Visible = false;
                e.Row.Cells[13].Visible = false;
                e.Row.Cells[14].Visible = false;
                e.Row.Cells[15].Visible = false;
            }

            if (e.Row.Cells[16].Text == "" || e.Row.Cells[16].Text == "&nbsp;")
                e.Row.Cells[16].Text = "0";
            if (e.Row.Cells[17].Text == "" || e.Row.Cells[17].Text == "&nbsp;")
                e.Row.Cells[17].Text = "0";
            if (e.Row.Cells[18].Text == "" || e.Row.Cells[18].Text == "&nbsp;")
                e.Row.Cells[18].Text = "0";

            Double Gamt = Convert.ToDouble(e.Row.Cells[16].Text);
            e.Row.Cells[16].Text = Gamt.ToString("#0.00");
           
            Double Aamt = Convert.ToDouble(e.Row.Cells[17].Text);
            e.Row.Cells[17].Text = Aamt.ToString("#0.00");
            
            Double amt = Convert.ToDouble(e.Row.Cells[18].Text);
            e.Row.Cells[18].Text = amt.ToString("#0.00");

            TotalG = TotalG + amt;

        }
        if (e.Row.RowType == DataControlRowType.Header)
        {
           
            if (bType == "DB" || bType == "ATLDB" || bType == "DA")
            {
                e.Row.Cells[12].Visible = false;
                e.Row.Cells[13].Visible = false;
                e.Row.Cells[14].Visible = false;
                e.Row.Cells[15].Visible = false;
            }
            else if (bType == "0")
            {
                e.Row.Cells[12].Visible = true;
                e.Row.Cells[13].Visible = true;
                e.Row.Cells[14].Visible = true;
                e.Row.Cells[15].Visible = true;
            }
        }
        if (e.Row.RowType == DataControlRowType.Footer)
        {
           
            if (bType == "DB" || bType == "ATLDB" || bType == "DA")
            {
                e.Row.Cells[12].Visible = false;
                e.Row.Cells[13].Visible = false;
                e.Row.Cells[14].Visible = false;
                e.Row.Cells[15].Visible = false;
            }
            else if (bType == "0")
            {
                e.Row.Cells[12].Visible = true;
                e.Row.Cells[13].Visible = true;
                e.Row.Cells[14].Visible = true;
                e.Row.Cells[15].Visible = true;
            }
            e.Row.Cells[18].Text = Total.ToString("#0.00");
            e.Row.Cells[17].Text = "Total Rs.";
        }
    }

    protected void ExportPDF_Click(object sender, EventArgs e)
    {
        string fDate = txtFrom.Text;
        string tDate = txtTo.Text;
        string bType = drpBType.SelectedValue;
        Session["fDate"] = fDate;
        Session["tDate"] = tDate;
        Session["BILLTYPE"] = bType;
        
        Session["PNAME"] = txtPName.Text;
        Page.ClientScript.RegisterStartupScript(this.GetType(), "open", "window.open('CrySBReg.aspx?startDT=" + fDate + "&endDT=" + tDate + "','','menubar=no, scrollbars=yes, toolbar=no, location=no, resizable=yes,left=0,top=0');", true);

        

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

        }
    }
}
