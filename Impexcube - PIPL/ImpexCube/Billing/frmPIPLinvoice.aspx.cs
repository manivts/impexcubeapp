using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Collections;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.OleDb;
using System.Drawing;
using MySql;
using MySql.Data.MySqlClient;
using System.Data.SqlClient;
using System.Web.Handlers;
using System.IO;

namespace ImpexCube.Billing
{
    public partial class frmPIPLinvoice : System.Web.UI.Page
    {
        string strcon = (string)ConfigurationManager.ConnectionStrings["Constr"].ConnectionString;
        //string strconn = (string)ConfigurationManager.AppSettings["ConnectionImpex"];
        //string strconn1 = (string)ConfigurationManager.AppSettings["ConnectionVisual"];


        protected void Page_Load(object sender, EventArgs e)
        {

            if (IsPostBack == false)
            {
                string Bill = (string)Session["BillNo"];
                lblBillno.Text = Bill;

                string BILLType = (string)Session["BillType"];

                if (BILLType == "SB" || BILLType == "ATLSB")
                {
                    lblIName.Text = "INVOICE";
                    PIPLInvoice(Bill);
                }
                else
                {
                    lblIName.Text = "DEBIT NOTE";
                    PIPLDebit(Bill);

                }
                SqlConnection conn = new SqlConnection(strcon);
                string sqlQuery = "select  * from Branch";
                SqlDataAdapter da = new SqlDataAdapter(sqlQuery, conn);
                DataSet ds = new DataSet();
                da.Fill(ds, "Table");

                if (ds.Tables["Table"].Rows.Count != 0)
                {
                    DataRowView row = ds.Tables["Table"].DefaultView[0];
                    lblCompName.Text = row["CompanyName"].ToString();
                    lblCHANO.Text = row["CHANo"].ToString();
                    lblSTRegno.Text = row["STRegNo"].ToString();
                    lbladdress.Text = row["address"].ToString();
                    lbladdress1.Text = row["address1"].ToString();
                    lblTele.Text = row["TelePhoneNo"].ToString();
                    lblCompName1.Text = row["CompanyName"].ToString();
                    lblBranchName.Text = row["ReportBranchName"].ToString();
                }

            }

        }
        protected void PIPLInvoice(string invNo)
        {
            try
            {
                SqlConnection conn = new SqlConnection(strcon);
                string Query = "select * from M_iec_invoiceNew where invoice ='" + invNo + "'";
                SqlDataAdapter da = new SqlDataAdapter(Query, conn);
                DataSet ds = new DataSet();
                da.Fill(ds, "SB");
                if (ds.Tables["SB"].Rows.Count != 0)
                {


                    DataRowView row = ds.Tables["SB"].DefaultView[0];
                    string contr = row["contr_code"].ToString();
                    Session["CONTRACT"] = contr;
                    lblnote.Text = row["notes"].ToString();
                    lblDAte.Text = row["invoiceDate"].ToString();
                    txtCompName.Text = row["compName"].ToString();
                    txtAddr.Text = row["address1"].ToString();
                    txtAdd1.Text = row["address2"].ToString();
                    txtCity.Text = row["city"].ToString() + "  " + row["pincode"].ToString();
                    txtAdd2.Text = row["state"].ToString();
                    txtPhone.Text = row["phone"].ToString();
                    txtParty_Reff.Text = row["partyReff"].ToString();
                    txtJobNo.Text = row["jobno"].ToString();
                    txtBLNo.Text = row["blno"].ToString();
                    txtBENo.Text = row["BEnoDate"].ToString();
                    txtImpotItem.Text = row["importItem"].ToString();
                    txtQty.Text = row["Quantity"].ToString();
                    txtAssValue.Text = row["ass_Value"].ToString();
                    ContNo.Text = row["Container_No"].ToString();
                    txtCustomDuty.Text = row["Custom_Duty"].ToString();
                    txtSubParty.Text = row["Sub_party"].ToString();
                    string detail = row["particular1"].ToString();


                    Double tot = Convert.ToDouble(row["SubTotalTax"].ToString());
                    Double totPaid = Convert.ToDouble(row["SubTotal"].ToString());
                    Double st = Convert.ToDouble(row["STaxPercent"].ToString());
                    Double stax = Convert.ToDouble(row["Service_tax"].ToString());
                    Double stEDU = Convert.ToDouble(row["Edu_Cess"].ToString());
                    Double stSEC = Convert.ToDouble(row["SEC_Chess"].ToString());
                    Double stGT = Convert.ToDouble(row["Grand_Total"].ToString());
                    Double stLA = Convert.ToDouble(row["Less_Advance"].ToString());
                    Double stNT = Convert.ToDouble(row["Net_Total"].ToString());

                    lblintrmks.Text = row["interRemark"].ToString();

                    subPaidTotal.Text = totPaid.ToString("#0.00");
                    SubTotal.Text = tot.ToString("#0.00");
                    ServiceTax.Text = st.ToString("#0.00");
                    sTax.Text = stax.ToString("#0.00");
                    EdCess.Text = stEDU.ToString("#0.00");
                    SHCess.Text = stSEC.ToString("#0.00");
                    Totals.Text = stGT.ToString("#0.00");
                    LessAd.Text = stLA.ToString("#0.00");
                    BalanceDue.Text = stNT.ToString("#0.00");
                    txtRupees.Text = row["NetTotal_Words"].ToString();
                    //  txtSubParty.Text=row["Sub_party"].ToString();

                    if (contr == "" && contr == string.Empty && detail != "")
                    {
                        string stal = row["paid_subtotal"].ToString();
                        if (stal == "")
                            stal = "0";
                        Double sTotal = Convert.ToDouble(stal);
                        subPaidTotal.Text = sTotal.ToString("#0.00");
                        Detail1.Text = row["particular1"].ToString(); Detail2.Text = row["particular2"].ToString(); Detail3.Text = row["particular3"].ToString(); Detail4.Text = row["particular4"].ToString(); Detail5.Text = row["particular5"].ToString(); Detail6.Text = row["particular6"].ToString(); Detail7.Text = row["particular7"].ToString(); Detail8.Text = row["particular8"].ToString(); Detail9.Text = row["particular9"].ToString();
                        Rcpt1.Text = row["Receipt1"].ToString(); Rcpt2.Text = row["Receipt2"].ToString(); Rcpt3.Text = row["Receipt3"].ToString(); Rcpt4.Text = row["Receipt4"].ToString(); Rcpt5.Text = row["Receipt5"].ToString(); Rcpt6.Text = row["Receipt6"].ToString(); Rcpt7.Text = row["Receipt7"].ToString(); Rcpt8.Text = row["Receipt8"].ToString(); Rcpt9.Text = row["Receipt9"].ToString();

                        Double ppd1 = Convert.ToDouble(row["Paid_Party1"].ToString()); Double ppd2 = Convert.ToDouble(row["Paid_Party2"].ToString()); Double ppd3 = Convert.ToDouble(row["Paid_Party3"].ToString()); Double ppd4 = Convert.ToDouble(row["Paid_Party4"].ToString()); Double ppd5 = Convert.ToDouble(row["Paid_Party5"].ToString()); Double ppd6 = Convert.ToDouble(row["Paid_Party6"].ToString()); Double ppd7 = Convert.ToDouble(row["Paid_Party7"].ToString()); Double ppd8 = Convert.ToDouble(row["Paid_Party8"].ToString()); Double ppd9 = Convert.ToDouble(row["Paid_Party9"].ToString());
                        ppaid1.Text = ppd1.ToString("#0.00"); ppaid2.Text = ppd2.ToString("#0.00"); ppaid3.Text = ppd3.ToString("#0.00"); ppaid4.Text = ppd4.ToString("#0.00"); ppaid5.Text = ppd5.ToString("#0.00"); ppaid6.Text = ppd6.ToString("#0.00"); ppaid7.Text = ppd7.ToString("#0.00"); ppaid8.Text = ppd8.ToString("#0.00"); ppaid9.Text = ppd9.ToString("#0.00");

                        Double at1 = Convert.ToDouble(row["Amount1"].ToString()); Double at2 = Convert.ToDouble(row["Amount2"].ToString()); Double at3 = Convert.ToDouble(row["Amount3"].ToString()); Double at4 = Convert.ToDouble(row["Amount4"].ToString()); Double at5 = Convert.ToDouble(row["Amount5"].ToString()); Double at6 = Convert.ToDouble(row["Amount6"].ToString()); Double at7 = Convert.ToDouble(row["Amount7"].ToString()); Double at8 = Convert.ToDouble(row["Amount8"].ToString()); Double at9 = Convert.ToDouble(row["Amount9"].ToString());
                        amt1.Text = at1.ToString("#0.00"); amt2.Text = at2.ToString("#0.00"); amt3.Text = at3.ToString("#0.00"); amt4.Text = at4.ToString("#0.00"); amt5.Text = at5.ToString("#0.00"); amt6.Text = at6.ToString("#0.00"); amt7.Text = at7.ToString("#0.00"); amt8.Text = at8.ToString("#0.00"); amt9.Text = at9.ToString("#0.00");
                        GetNullValue();
                        //ppaid1.Text=row["Paid_Party1"].ToString();ppaid2.Text=row["Paid_Party2"].ToString();ppaid3.Text=row["Paid_Party3"].ToString();ppaid4.Text=row["Paid_Party4"].ToString();ppaid5.Text=row["Paid_Party5"].ToString();ppaid6.Text=row["Paid_Party6"].ToString();ppaid7.Text=row["Paid_Party7"].ToString();ppaid8.Text=row["Paid_Party8"].ToString();ppaid9.Text=row["Paid_Party9"].ToString();
                        //amt1.Text=row["Amount1"].ToString();amt2.Text=row["Amount2"].ToString();amt3.Text=row["Amount3"].ToString();amt4.Text=row["Amount4"].ToString();amt5.Text=row["Amount5"].ToString();amt6.Text=row["Amount6"].ToString();amt7.Text=row["Amount7"].ToString();amt8.Text=row["Amount8"].ToString();amt9.Text=row["Amount9"].ToString();
                    }
                    else
                    {
                        rHEAD.Visible = false;
                        TblMST.Visible = false;
                        string sqlQuery = "select *  from T_iec_invoicenew_dtl where invoice='" + invNo + "' order by sno ";

                        GridView1.DataSource = GetINVOICE(sqlQuery);
                        GridView1.DataBind();
                    }

                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('" + ex.Message + "');", true);
            }

        }
        public DataSet GetINVOICE(string sqlQuery)
        {
            SqlConnection conn = new SqlConnection(strcon);

            SqlDataAdapter da4 = new SqlDataAdapter(sqlQuery, conn);
            DataSet ds4 = new DataSet();
            da4.Fill(ds4, "INV");
            return ds4;
        }
        protected void PIPLDebit(string invNo)
        {
            try
            {
                SqlConnection conn = new SqlConnection(strcon);
                string Query = "select * from M_IEC_DEBIT where invoice ='" + invNo + "'";
                SqlDataAdapter da = new SqlDataAdapter(Query, conn);
                DataSet ds = new DataSet();
                da.Fill(ds, "SB");
                if (ds.Tables["SB"].Rows.Count != 0)
                {


                    DataRowView row = ds.Tables["SB"].DefaultView[0];
                    string contr = row["contr_code"].ToString();
                    Session["CONTRACT"] = contr;
                    lblnote.Text = row["notes"].ToString();
                    lblDAte.Text = row["invoiceDate"].ToString();
                    txtCompName.Text = row["compName"].ToString();
                    txtAddr.Text = row["address1"].ToString();
                    txtAdd1.Text = row["address2"].ToString();
                    txtCity.Text = row["city"].ToString() + "  " + row["pincode"].ToString();
                    txtAdd2.Text = row["state"].ToString();
                    txtPhone.Text = row["phone"].ToString();
                    txtParty_Reff.Text = row["partyReff"].ToString();
                    txtJobNo.Text = row["jobno"].ToString();
                    txtBLNo.Text = row["blno"].ToString();
                    txtBENo.Text = row["BEnoDate"].ToString();
                    txtImpotItem.Text = row["importItem"].ToString();
                    txtQty.Text = row["Quantity"].ToString();
                    txtAssValue.Text = row["ass_Value"].ToString();
                    ContNo.Text = row["Container_No"].ToString();
                    txtCustomDuty.Text = row["Custom_Duty"].ToString();
                    txtSubParty.Text = row["Sub_party"].ToString();
                    string detail = row["particular1"].ToString();

                    Double tot = Convert.ToDouble(row["SubTotal"].ToString());

                    Double stGT = Convert.ToDouble(row["Grand_Total"].ToString());
                    Double stLA = Convert.ToDouble(row["Less_Advance"].ToString());
                    Double stNT = Convert.ToDouble(row["Net_Total"].ToString());


                    SubTotal.Text = tot.ToString("#0.00");
                    ServiceTax.Visible = sTax.Visible = EdCess.Visible = SHCess.Visible = false;
                    Label29.Visible = Label30.Visible = Label31.Visible = Label7.Visible = false;
                    Totals.Text = stGT.ToString("#0.00");
                    LessAd.Text = stLA.ToString("#0.00");
                    BalanceDue.Text = stNT.ToString("#0.00");
                    txtRupees.Text = row["NetTotal_Words"].ToString();
                    //  txtSubParty.Text=row["Sub_party"].ToString();

                    if (contr == "" && contr == string.Empty && detail != "")
                    {
                        string stal = row["paid_subtotal"].ToString();
                        if (stal == "")
                            stal = "0";
                        Double sTotal = Convert.ToDouble(stal);
                        subPaidTotal.Text = sTotal.ToString("#0.00");
                        Detail1.Text = row["particular1"].ToString(); Detail2.Text = row["particular2"].ToString(); Detail3.Text = row["particular3"].ToString(); Detail4.Text = row["particular4"].ToString(); Detail5.Text = row["particular5"].ToString(); Detail6.Text = row["particular6"].ToString(); Detail7.Text = row["particular7"].ToString(); Detail8.Text = row["particular8"].ToString(); Detail9.Text = row["particular9"].ToString();
                        Rcpt1.Text = row["Receipt1"].ToString(); Rcpt2.Text = row["Receipt2"].ToString(); Rcpt3.Text = row["Receipt3"].ToString(); Rcpt4.Text = row["Receipt4"].ToString(); Rcpt5.Text = row["Receipt5"].ToString(); Rcpt6.Text = row["Receipt6"].ToString(); Rcpt7.Text = row["Receipt7"].ToString(); Rcpt8.Text = row["Receipt8"].ToString(); Rcpt9.Text = row["Receipt9"].ToString();

                        Double ppd1 = Convert.ToDouble(row["Paid_Party1"].ToString()); Double ppd2 = Convert.ToDouble(row["Paid_Party2"].ToString()); Double ppd3 = Convert.ToDouble(row["Paid_Party3"].ToString()); Double ppd4 = Convert.ToDouble(row["Paid_Party4"].ToString()); Double ppd5 = Convert.ToDouble(row["Paid_Party5"].ToString()); Double ppd6 = Convert.ToDouble(row["Paid_Party6"].ToString()); Double ppd7 = Convert.ToDouble(row["Paid_Party7"].ToString()); Double ppd8 = Convert.ToDouble(row["Paid_Party8"].ToString()); Double ppd9 = Convert.ToDouble(row["Paid_Party9"].ToString());
                        ppaid1.Text = ppd1.ToString("#0.00"); ppaid2.Text = ppd2.ToString("#0.00"); ppaid3.Text = ppd3.ToString("#0.00"); ppaid4.Text = ppd4.ToString("#0.00"); ppaid5.Text = ppd5.ToString("#0.00"); ppaid6.Text = ppd6.ToString("#0.00"); ppaid7.Text = ppd7.ToString("#0.00"); ppaid8.Text = ppd8.ToString("#0.00"); ppaid9.Text = ppd9.ToString("#0.00");

                        Double at1 = Convert.ToDouble(row["Amount1"].ToString()); Double at2 = Convert.ToDouble(row["Amount2"].ToString()); Double at3 = Convert.ToDouble(row["Amount3"].ToString()); Double at4 = Convert.ToDouble(row["Amount4"].ToString()); Double at5 = Convert.ToDouble(row["Amount5"].ToString()); Double at6 = Convert.ToDouble(row["Amount6"].ToString()); Double at7 = Convert.ToDouble(row["Amount7"].ToString()); Double at8 = Convert.ToDouble(row["Amount8"].ToString()); Double at9 = Convert.ToDouble(row["Amount9"].ToString());
                        amt1.Text = at1.ToString("#0.00"); amt2.Text = at2.ToString("#0.00"); amt3.Text = at3.ToString("#0.00"); amt4.Text = at4.ToString("#0.00"); amt5.Text = at5.ToString("#0.00"); amt6.Text = at6.ToString("#0.00"); amt7.Text = at7.ToString("#0.00"); amt8.Text = at8.ToString("#0.00"); amt9.Text = at9.ToString("#0.00");
                        //amt1.Text=row["Amount1"].ToString();amt2.Text=row["Amount2"].ToString();amt3.Text=row["Amount3"].ToString();amt4.Text=row["Amount4"].ToString();amt5.Text=row["Amount5"].ToString();amt6.Text=row["Amount6"].ToString();amt7.Text=row["Amount7"].ToString();amt8.Text=row["Amount8"].ToString();amt9.Text=row["Amount9"].ToString();
                        GetNullValue();
                    }
                    else
                    {
                        rHEAD.Visible = false;
                        TblMST.Visible = false;
                        string sqlQuery = "select *  from T_IEC_DEBIT_dtl where invoice='" + invNo + "' order by sno ";

                        GridView1.DataSource = GetINVOICE(sqlQuery);
                        GridView1.DataBind();
                    }
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('" + ex.Message + "');", true);
            }

        }
        protected void GetNullValue()
        {
            if (amt1.Text == "0.00")
                amt1.Text = "";
            if (amt2.Text == "0.00")
                amt2.Text = "";
            if (amt3.Text == "0.00")
                amt3.Text = "";
            if (amt4.Text == "0.00")
                amt4.Text = "";
            if (amt5.Text == "0.00")
                amt5.Text = "";
            if (amt6.Text == "0.00")
                amt6.Text = "";
            if (amt7.Text == "0.00")
                amt7.Text = "";
            if (amt8.Text == "0.00")
                amt8.Text = "";
            if (amt9.Text == "0.00")
                amt9.Text = "";

            if (ppaid1.Text == "0.00")
                ppaid1.Text = "";
            if (ppaid2.Text == "0.00")
                ppaid2.Text = "";
            if (ppaid3.Text == "0.00")
                ppaid3.Text = "";
            if (ppaid4.Text == "0.00")
                ppaid4.Text = "";
            if (ppaid5.Text == "0.00")
                ppaid5.Text = "";
            if (ppaid6.Text == "0.00")
                ppaid6.Text = "";
            if (ppaid7.Text == "0.00")
                ppaid7.Text = "";
            if (ppaid8.Text == "0.00")
                ppaid8.Text = "";
            if (ppaid9.Text == "0.00")
                ppaid9.Text = "";
        }
        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Double amt = Convert.ToDouble(e.Row.Cells[2].Text);
                e.Row.Cells[2].Text = amt.ToString("#0.00");
            }
        }
        protected void Print_Click(object sender, EventArgs e)
        {
            Session["billPOP"] = Panel2;
            //ClientScript.RegisterStartupScript(this.GetType(), "onclick", "<script language=javascript>window.open('frmPrintInvoice.aspx','PrintMe','height=900px,width=750px,scrollbars=1,left=100');</script>");
            Control ctrl = (Control)Session["billPOP"];

            PrintHelper.PrintWebControl(ctrl);
        }
    }
}