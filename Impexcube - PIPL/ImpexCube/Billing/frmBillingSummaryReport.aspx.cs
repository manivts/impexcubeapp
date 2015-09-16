﻿using System;
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

public partial class frmBillingSummaryReport : System.Web.UI.Page
{
    //string strPIPL = (string)ConfigurationManager.AppSettings["ConnectionImpex"];
    //string strconn = (string)ConfigurationSettings.AppSettings["ConnectionImpex"];

    string strconn = (string)ConfigurationManager.ConnectionStrings["Constr"].ConnectionString;
    #region
    Double Total = 0;
    //Double totalAG=0;
    //Double totalST=0;
    //Double totalINV=0;
    //Double totalCFS=0;
    //Double totalDO=0;
    //Double totalADV=0;
    //Double totalDB=0;
    //Double totalGT=0;
    string RinvNo = "";
    #endregion
    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack == false)
        {
            DivTag.Visible = false;
            trRow.Visible = false;
           
            string fy = (string)Session["FinancialYear"];
            getSDate(fy);
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
        string RPT = drSummary.SelectedValue;
      
        
                if (fdate != "" && tdate != "" && pName != "")
                {
                    if (RPT != "")
                    {
                       //Query = "select i.invoicedate,i.JOBNO,i.invoice as INV,j.invoice as DN,j.less_advance " +
                       //     "from  M_iec_invoiceNew i,M_iec_debit j where i.jobno=j.jobno and i.invoiceDate  between '" + fdate + "' and '" + tdate + "' " +
                       //     "and i.compName='" + pName + "' and i.summarystatus='" + drSummary.SelectedValue + "' and i.stsID is not null and j.stsID is not null order by i.invoiceDate";

                        Query = "select i.invoicedate,i.JOBNO,i.invoice as INV,i.less_advance,i.impRemark " +
                         "from  M_iec_invoiceNew i where i.invoiceDate  >=  '" + fdate + "' and i.invoiceDate  <= '" + tdate + "' " +
                         "and i.compName='" + pName + "' and i.summarystatus='" + drSummary.SelectedValue + "' and i.stsID is not null order by i.invoiceDate";

                        Report(Query);
                    }
                    else
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Select Summary ID');", true);
                }
                else
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Please Select Customer Name');", true);

        Session["BillTY"] = null;
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
                trRow.Visible = true;
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
       
        GridViewExportDet.ExportExcell("BillingSummary.xls", gvReport);
      
        
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
                if (litotal.Text != "")
                    Total = Total + Convert.ToDouble(litotal.Text);

            }
            if (e.Row.RowType == DataControlRowType.Footer)
            {
                e.Row.Cells[18].Text = Total.ToString("#0.00");
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

    protected void txtPName_TextChanged(object sender, EventArgs e)
    {
        string pName=txtPName.Text;
        string Query = "select distinct summarystatus from M_iec_invoiceNew where compname like '" + pName + "%' and stsid is not null ";
        
        drSummary.DataSource = GetData(Query);
        drSummary.DataTextField = "summarystatus";
        drSummary.DataValueField = "summarystatus";
        drSummary.DataBind();
    }
}
