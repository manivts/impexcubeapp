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

public partial class frmBillingSummary : System.Web.UI.Page
{
   

    string strconn = (string)ConfigurationManager.ConnectionStrings["Constr"].ConnectionString;
    //string strPIPL = (string)ConfigurationManager.AppSettings["ConnectionImpex"];
    //string strconn = (string)ConfigurationSettings.AppSettings["ConnectionImpex"];
    #region
    Double Total = 0;
    Double totalAG = 0;
    Double totalST = 0;
    Double totalINV = 0;
    Double totalCFS = 0;
    Double totalDO = 0;
    Double totalADV = 0;
    Double totalDB = 0;
    Double totalGT = 0;
    string RinvNo = "";
    #endregion
    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack == false)
        {
            DivTag.Visible = false;
           
            string fy = (string)Session["FinancialYear"];
            getSDate(fy);
            GetSummaryId(fy);
            txtPName.Enabled = false;
            

            SqlConnection conn = new SqlConnection(strconn);
            string sqlQuery = "select  * from AppDetails";
            SqlDataAdapter da = new SqlDataAdapter(sqlQuery, conn);
            DataSet ds = new DataSet();
            da.Fill(ds, "name");

            if (ds.Tables["name"].Rows.Count != 0)
            {
                DataRowView row = ds.Tables["name"].DefaultView[0];
                lblShortName.Text = row["ShortName"].ToString();
             
            }

        }
    }
    private void GetSummaryId(string fy)
    {
        string InNo = "";
        string iType = "SUM";
        SqlConnection conn2 = new SqlConnection(strconn);
        string strQuery = "select * from M_RunningNo where iectype='" + iType + "' and Fyear='" + fy + "'";
        SqlDataAdapter da2 = new SqlDataAdapter(strQuery, conn2);

        DataSet ds2 = new DataSet();
        da2.Fill(ds2, "INVOICE");
        Int32 InID = 0;
        if (ds2.Tables["INVOICE"].Rows.Count != 0)
        {
            DataRowView row = ds2.Tables["INVOICE"].DefaultView[0];
            InNo = row["rno"].ToString();
            InID = Convert.ToInt32(InNo) + 1;
        }
        txtSummaryID.Text = InID.ToString();
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
            txtFrom.Text = sDATES.ToShortDateString();
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


        string pName = txtPName.Text;
        string jno = txtJobNo.Text;

        if (chkJobNo.Checked == true)
        {
            if (jno != "" || jno != string.Empty)
            {
                //Query = "select i.TransId as TransId,j.DBTransId as Trans,i.invoicedate,i.JOBNO,i.invoice as INV,j.invoice as DN,j.less_advance from  M_iec_invoiceNew i,M_iec_debit j where i.jobno=j.jobno and i.jobno='" + jno + "' and (i.stsID is null or j.stsID is null) order by i.invoiceDate";
                Query = "select i.TransId as TransId,i.invoicedate,i.JOBNO,i.invoice as INV,i.less_advance,i.impRemark from  M_iec_invoiceNew i where i.jobno='" + jno + "' and i.stsID is null order by i.invoiceDate";
                Report(Query);
            }
            else
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Incorrect Job Number');", true);

        }
        else
        {
            if (fdate != "" && tdate != "" && pName == "")
            {
                //Query = "select i.TransId as TransId,j.DBTransId as Trans,i.invoicedate,i.JOBNO,i.invoice as INV,j.invoice as DN,j.less_advance from  M_iec_invoiceNew i,M_iec_debit j where i.jobno=j.jobno and i.invoiceDate  >=  '" + fdate + "' and i.invoiceDate  <= '" + tdate + "' and (i.stsID is null or j.stsID is null) order by i.invoiceDate";
                Query = "select i.TransId as TransId,i.invoicedate,i.JOBNO,i.invoice as INV,i.less_advance,i.impRemark from  M_iec_invoiceNew i where i.invoiceDate  >=  '" + fdate + "' and i.invoiceDate  <= '" + tdate + "' and i.stsID is null  order by i.invoiceDate";
                Report(Query);
            }
            else if (fdate != "" && tdate != "" && pName != "")
            {
                //Query = "select i.TransId as TransId,j.DBTransId as Trans,i.invoicedate,i.JOBNO,i.invoice as INV,j.invoice as DN,j.less_advance from  M_iec_invoiceNew i,M_iec_debit j where i.jobno=j.jobno and i.invoiceDate  >= '" + fdate + "' and i.invoiceDate  <= '" + tdate + "' and i.compName='" + pName + "' and (i.stsID is null or j.stsID is null) order by i.invoiceDate";
                Query = "select i.TransId as TransId,i.invoicedate,i.JOBNO,i.invoice as INV,i.less_advance,i.impRemark from  M_iec_invoiceNew i where i.invoiceDate  >= '" + fdate + "' and i.invoiceDate  <= '" + tdate + "' and i.compName='" + pName + "' and i.stsID is null order by i.invoiceDate";
                Report(Query);
            }
            else
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Incorrect Dates');", true);

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
        SqlConnection conn = new SqlConnection(strconn);
        conn.Open();
        try
        {
            SqlCommand com = new SqlCommand(Cmd, conn);
            SqlDataAdapter da = new SqlDataAdapter(com);
            DataSet ds = new DataSet();
            da.Fill(ds, "PARTY");
            if (ds.Tables["PARTY"].Rows.Count != 0)
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
    protected void update(string sqlQuery)
    {
        SqlConnection conn = new SqlConnection(strconn);
       
        conn.Open();
        SqlCommand cmd = new SqlCommand(sqlQuery, conn);
        SqlDataAdapter da = new SqlDataAdapter();
        cmd.CommandText = sqlQuery;
        cmd.Connection = conn;
        da.SelectCommand = cmd;


        int result = cmd.ExecuteNonQuery();
    }
    protected void btnExport_Click(object sender, EventArgs e)
    {
        try
        {
            string fy = (string)Session["FinancialYear"];
            string InNo = "";
            string iType = "SUM";
            string SBNO = "";
            //string DBNO = "";
            string SBTrans = "";
            //string DBTrans = "";
            //string trans = "";
            string transid = "";
            string billno = "";
            //string dbno = "";
            SqlConnection conn2 = new SqlConnection(strconn);
            string strQuery = "select * from M_RunningNo where iectype='" + iType + "' and Fyear='" + fy + "'";
            SqlDataAdapter da2 = new SqlDataAdapter(strQuery, conn2);

            DataSet ds2 = new DataSet();
            da2.Fill(ds2, "INVOICE");
            Int32 InID = 0;
            if (ds2.Tables["INVOICE"].Rows.Count != 0)
            {
                DataRowView row = ds2.Tables["INVOICE"].DefaultView[0];
                InNo = row["rno"].ToString();
                InID = Convert.ToInt32(InNo) + 1;
            }

            string Status = txtPName.Text;
            if (chkIMP.Checked == false)
                Status = "Common" + Convert.ToString(InID);
            else
            {
                Status = txtPName.Text;
                string[] cmp = Status.Split(' ');
                Status = cmp[0] + Convert.ToString(InID);
            }
            int i = 0;
            foreach (GridViewRow row in gvReport.Rows)
            {
                CheckBox chkBxSelect = (CheckBox)row.FindControl("chkBxSelect");
                if (chkBxSelect.Checked)
                {
                    //trans = row.Cells[15].Text;
                    transid = row.Cells[20].Text;
                    billno = row.Cells[3].Text;
                    //dbno = row.Cells[7].Text;

                  
                    //DBNO = DBNO + "'" + dbno + "',";
                    SBNO = SBNO + "'" + billno + "',";
                    SBTrans = SBTrans + "'" + transid + "',";
                    //DBTrans = DBTrans + "'" + trans + "',";
                }
            }



            //DBNO = DBNO.TrimEnd(',');
            SBNO = SBNO.TrimEnd(',');
            SBTrans = SBTrans.TrimEnd(',');
            //DBTrans = DBTrans.TrimEnd(',');

            if ( SBNO == "''" && SBTrans == "''" )
            {
               // DBNO = "";
                SBNO = "";
                SBTrans = "";
                //DBTrans = "";
            }


            if (SBNO != "" && SBTrans != "")
            {
                string dates = DateTime.Now.ToString("yyyy-MM-dd 00:00:00");
                string Query = "update M_iec_invoicenew set SummaryStatus='" + Status + "' ,stsID=" + InID + ",summaryDate='" + dates + "' where TransId in( " + SBTrans + ") and invoice in (" + SBNO + ")";
                update(Query);
                //Query = "update M_iec_debit set SummaryStatus='" + Status + "' ,stsID=" + txtSummaryID.Text + ",summaryDate='" + dates + "' where DBTransId in (" + DBTrans + ") and invoice in (" + DBNO + ")";
                //update(Query);
                string sqlQuery = "update M_RunningNo set rno=" + InID + " where iecType='" + iType + "' and fyear='" + fy + "'";
                update(sqlQuery);
              
                ScriptManager.RegisterStartupScript(this, this.GetType(), "redirect", "alert('Summary Report has been Generated Successfully..'); window.location.href='frmBillingSummary.aspx';", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Please select the invoice to be updated');", true);
            }


        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('" + ex.Message + "');", true);
        }

    }
    protected void gvReport_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            string Query = "";
            Double stax = 0;
            Double edCS = 0;
            Double shCS = 0;
            Double inval = 0;
            Double chgAmt = 0;
            Double TotCharge = 0;
            Double taxVal = 0;
          

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (e.Row.Cells[1].Text != "&nbsp;")
                {
                    DateTime bDate = Convert.ToDateTime(e.Row.Cells[1].Text);
                    e.Row.Cells[1].Text = bDate.ToString("dd/MM/yyyy");
                }
                string jobno = e.Row.Cells[2].Text;
             
                Label lAgency = (Label)e.Row.FindControl("lblAgency");
                Label lstax = (Label)e.Row.FindControl("lblStax");
                Label litotal = (Label)e.Row.FindControl("lbliTotal");

                Label lblCFS = (Label)e.Row.FindControl("lblCFS");
                Label lblSTaxCFS = (Label)e.Row.FindControl("lblSTaxCFS");
                Label lblDO = (Label)e.Row.FindControl("lblDO");
                Label lblSTaxDO = (Label)e.Row.FindControl("lblSTaxDO");

                Label lblAAI = (Label)e.Row.FindControl("lblAAI");
                Label lblSTaxAAI = (Label)e.Row.FindControl("lblSTaxAAI");
                Label lblSurveyFees = (Label)e.Row.FindControl("lblSurveyFees");
                Label lblSTaxSurveyFees = (Label)e.Row.FindControl("lblSTaxSurveyFees");
                Label lblCustomDuty = (Label)e.Row.FindControl("lblCustomDuty");
                Label lblEmbassyCharges = (Label)e.Row.FindControl("lblEmbassyCharges");

                Label lbldTtotal = (Label)e.Row.FindControl("lbldTtotal");
                Label lblgTotal = (Label)e.Row.FindControl("lblgTotal");

                string invno = e.Row.Cells[3].Text;
                Query = "select * from M_iec_invoiceNew i,T_iec_invoiceNew_dtl j where i.invoice=j.invoice and  i.invoice='" + invno + "' ";

                DataSet ds = new DataSet();
                ds = GetData(Query);
                if (ds.Tables["invc"].Rows.Count != 0)
                {
                    if (invno != RinvNo)
                    {
                        DataTable dt = ds.Tables[0];
                        foreach (DataRow row in dt.Rows)
                        {
                            stax = Convert.ToDouble(row["Service_tax"].ToString());
                            edCS = Convert.ToDouble(row["Edu_Cess"].ToString());
                            shCS = Convert.ToDouble(row["SEC_Chess"].ToString());
                            taxVal = stax + edCS + shCS;
                            string chrg = row["charge_desc"].ToString();
                            string chgamount = row["amount"].ToString();
                            if (chgamount == "" || chgamount == "&nbsp;")
                                chgamount = "0";
                            chgAmt = Convert.ToDouble(chgamount);
                           
                            if (chrg.StartsWith("Agency charges"))
                            {
                                TotCharge += chgAmt;
                                lAgency.Text = chgAmt.ToString("#0.00");
                                
                            }
                            if (chrg.StartsWith("CFS Charges"))
                            {
                                TotCharge += chgAmt;
                                lblCFS.Text = chgAmt.ToString("#0.00");
                            }
                            if (chrg.StartsWith("Service Tax On CFS Charges"))
                            {
                                TotCharge += chgAmt;
                                lblSTaxCFS.Text = chgAmt.ToString("#0.00");
                            }
                            if (chrg.StartsWith("DO CHARGES"))
                            {
                                TotCharge += chgAmt;
                                lblDO.Text = chgAmt.ToString("#0.00");
                            }
                            if (chrg.StartsWith("Service Tax On Do Charges"))
                            {
                                TotCharge += chgAmt;
                                lblSTaxDO.Text = chgAmt.ToString("#0.00");
                            }

                            if (chrg.StartsWith("AAI Charges"))
                            {
                                TotCharge += chgAmt;
                                lblAAI.Text = chgAmt.ToString("#0.00");
                            }
                            if (chrg.StartsWith("Service Tax On AAI Charges"))
                            {
                                TotCharge += chgAmt;
                                lblSTaxAAI.Text = chgAmt.ToString("#0.00");
                            }

                            if (chrg.StartsWith("Survey Fees"))
                            {
                                TotCharge += chgAmt;
                                lblSurveyFees.Text = chgAmt.ToString("#0.00");
                            }
                            if (chrg.StartsWith("Service Tax On Survey Fees"))
                            {
                                TotCharge += chgAmt;
                                lblSTaxSurveyFees.Text = chgAmt.ToString("#0.00");
                            }

                            if (chrg.StartsWith("Custom Duty"))
                            {
                                TotCharge += chgAmt;
                                lblCustomDuty.Text = chgAmt.ToString("#0.00");
                            }
                            if (chrg.StartsWith("Embassy Charges"))
                            {
                                TotCharge += chgAmt;
                                lblEmbassyCharges.Text = chgAmt.ToString("#0.00");
                            }
                           
                            inval = TotCharge + taxVal;
                            lstax.Text = taxVal.ToString("#0.00");
                            litotal.Text = inval.ToString("#0.00");
                          
                            RinvNo = invno;
                        }
                    }
                    else
                    {
                        stax = 0;
                        edCS = 0;
                        shCS = 0;
                        chgAmt = 0;
                    }
                }
                if(litotal.Text!="")
                Total = Total + Convert.ToDouble(litotal.Text);
            }
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                e.Row.Cells[17].Text = Total.ToString("#0.00");
                //e.Row.Cells[5].Text = totalST.ToString("#0.00");
                //e.Row.Cells[6].Text = totalINV.ToString("#0.00");
                //e.Row.Cells[7].Text = totalCFS.ToString("#0.00");
                //e.Row.Cells[9].Text = totalDO.ToString("#0.00");
                //e.Row.Cells[10].Text = totalADV.ToString("#0.00");
                //e.Row.Cells[11].Text = totalDB.ToString("#0.00");
                //e.Row.Cells[12].Text = totalGT.ToString("#0.00");

            }
        }

        catch (Exception ex)
        {
            Response.Write(ex.Message);
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
            txtPName.Text = "";

        }
    }
    protected void txtSummaryID_TextChanged(object sender, EventArgs e)
    {
        string ChkSummary = "Select * from M_iec_debit where stsID='" + txtSummaryID.Text + "'";
        DataSet ChkDs = GetData(ChkSummary);
        if (ChkDs.Tables["invc"].Rows.Count != 0)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Summary Id has been already Generated..');", true);
        }
    }
   
}
