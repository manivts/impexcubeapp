using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
using System.Net.Mail;
using System.Net;
using System.Reflection;
using Outlook = Microsoft.Office.Interop.Outlook;
//--for CrystalReports's ReportDocument.
//using CrystalDecisions.CrystalReports.Engine;
using System.IO;
using System.Text;
public partial class EditInvoiceStaxUp : System.Web.UI.Page 
{
    
    //string strconn = (string)ConfigurationManager.AppSettings["ConnectionImpex"];
    //string strconn1 = (string)ConfigurationManager.AppSettings["ConnectionVisual"];
    //string strconnJSU = (string)ConfigurationManager.AppSettings["ConnectionJobStages"];

    string strImpex = (string)ConfigurationManager.ConnectionStrings["Constr"].ConnectionString;
    #region
    private string fJOBNO = "";
    Double fsTotal;
    Double BHamt;
    Double allTotal;
    //private string strCName;
    //private string InNo;
    //private string InCode;
    //private string invoice;
    //private Int32 InID;
    //int flag = 0;
    //string fyear = "";
    Double Gross = 0;
    Double GrossTot = 0;
    Double total;
    Double bal;
    Double vSTax;
    Double vECess;
    Double vSHECess;
    Double gSTAX;
    Double gECess;
    Double gSHECess;
    string fyear = "";
    //Int32 paid_Total;
    //Int32 Amt_Total;
    #endregion
    protected void Page_Load(object sender, EventArgs e)
    {
        AutoCompleteExtender1.ContextKey = txtCompName.Text;
        fyear = (string)Session["FinancialYear"];
        if (IsPostBack == false)
        {
           string ino= Request.QueryString["invNo"];
           TallyAccountName();
         
            //TextBoxOnBlur();
           
             txtRupees.Text = RsConvert.rupees(Convert.ToInt64(BalanceDue.Text));
            

             if (Session["Maill"] != null)
             {
                 if (Session["Maill"].ToString() == "AttachThePDF")
                 {
                     if (Session["InvoiceNum"] != null)
                     {

                         SearchInvoice((string)Session["InvoiceNum"]);
                         txtInvoiceNo.Text = (string)Session["InvoiceNum"];
                     }
                     else
                         SearchInvoice(ino);
                 }
                 
             }
             else
             {
                 SearchInvoice(ino);
             }
             if (Session["Maill"] != null)
             {
                 if (Session["Maill"].ToString() == "AttachThePDF")
                 {
                     btnMail_Click(sender, e);
                     Session["Maill"] = null;
                 }
             }
             
             txtRupees.Text = RsConvert.rupees(Convert.ToInt64(balance1.Text));
             txtInvoiceNo.Text = ino;

        }
    }

    public void TallyAccountName()
    {
        SqlConnection con = new SqlConnection(strImpex);
        con.Open();
        string query = "select AccountCode, AccountName from M_AccountMaster where Acc_group = 'Sundry Debtors' ";
        SqlDataAdapter da = new SqlDataAdapter(query, con);
        DataSet ds = new DataSet();
        da.Fill(ds, "sqlquery");
        if (ds.Tables["sqlquery"].Rows.Count != 0)
        {
            ddlTallyAccountName.DataSource = ds;
            ddlTallyAccountName.DataTextField = "AccountName";
            ddlTallyAccountName.DataValueField = "AccountCode";
            ddlTallyAccountName.DataBind();

            ddlTallySubPartyName.DataSource = ds;
            ddlTallySubPartyName.DataTextField = "AccountName";
            ddlTallySubPartyName.DataValueField = "AccountCode";
            ddlTallySubPartyName.DataBind();
        }

    }

    protected void TextBoxOnBlur()
    {
       
        LessAd.Attributes.Add("onblur", "javascript:CallServiceTax();");

        

    }
    public DataSet GetData(string InT)
    {

        SqlConnection conn = new SqlConnection(strImpex);
        string sqlStatement = "select invoice from M_IEC_InvoiceNew where invoiceType='" + InT + "'";
        SqlDataAdapter da = new SqlDataAdapter(sqlStatement, conn);
        DataSet ds = new DataSet();
        da.Fill(ds, "iec");

        return ds;

    }
    protected void Submit_Click(object sender, EventArgs e)
    {
        GetTransaction();
        string invNo = txtInvoiceNo.Text;
        Session["INVOICECTR"] = invNo;

        string dates = invDate.Text;
        string[] DT = dates.Split('/');
        dates = DT[2] + "-" + DT[1] + "-" + DT[0];

      
        string EntryDate = DateTime.Now.ToString();
        string suffix = txtSuffix.Text;
        string notes = txtNote.Text;
        SqlConnection conn = new SqlConnection(strImpex);
        string eBy = (string)Session["USER-NAME"];

        string impRK = txtimpRemark.Text;
        string intRK = txtIndentRemark.Text;
        impRK = impRK.Replace("'", " ");
        intRK = intRK.Replace("'", " ");
        string InvSeqNo = txtInvSeqNo.Text;
        

        string sqlQuery = " Update M_IEC_InvoiceNew set suffix='" + suffix + "',compName='" + txtCompName.Text + "',pincode='" + Session["pin"] + " '," +
                          " Address1='" + txtAdd1.Text + "',Address2='" + Session["state"] + "',City='" + txtCity.Text + "',phone='" + Session["Phone"] + "'," +
                          " partyReff='" + txtParty_Reff.Text + "',jobNo='" + txtJobNo.Text + "',BLNo='" + txtBLNo.Text + "',BENoDate='" + txtBENo.Text + " dt." + txtBEDate.Text + "'," +
                          " importItem='" + txtImpotItem.Text + "',notes='" + notes + "',Quantity='" + txtQty.Text + "',Ass_value='" + txtAssValue.Text + "',Container_no='" + txtNCNTR.Text + "'," +
                          " Custom_duty='" + txtCustomDuty.Text + "',subTotal='" + SubTotal.Text + "',subTotalTax='" + SubTotalTax.Text + "', service_Tax='" + sTax.Text + "'," +
                          " edu_cess='" + EdCess.Text + "', sec_chess='" + SHCess.Text + "',Grand_total='" + Totals.Text + "', less_advance='" + LessAd.Text + "'," +
                          " Net_total='" + BalanceDue.Text + "',sub_party='" + txtSubParty.Text + "',Nettotal_words='" + txtRupees.Text + "',"+
                          " impRemark='" + impRK + "',interRemark='" + intRK + "',TallyAccountName='" + ddlTallyAccountName.SelectedItem.Text + "',TallySubPartyName='" + ddlTallySubPartyName.SelectedItem.Text + "',InvSeqNo='" + InvSeqNo + "',SubPartyAddr='" + txtSubPartyAddr.Text + "',ModifiedBy='" + eBy + "' ,ModifiedDate='" + DateTime.Now + "' where invoice='" + invNo + "'";      
        try
        {

            conn.Open();
            SqlCommand cmd = new SqlCommand(sqlQuery, conn);
            SqlDataAdapter da = new SqlDataAdapter();
            cmd.CommandText = sqlQuery;
            cmd.Connection = conn;
            da.SelectCommand = cmd;
            

            int result = cmd.ExecuteNonQuery();
            if (result == 1)
            {
                UpdateGridview(invNo);
                InsertNewGridViewRow(invNo);
                string strQuery1 = "Update T_JobCreation set status_job='Y', BENo = '" + txtBENo.Text + "',BEDate = '" + txtBEDate.Text + "',TotalAssVal = '" + txtAssValue.Text + "',TotalDuty = '" + txtCustomDuty.Text + "'  where JobNo='" + txtJobNo.Text + "'";
                GetCommandIMP(strQuery1);
            }
             Response.Write("<script>" + "alert('Invoice successfully Updated');" + "</script>");
            
             Submit.Visible = false;
             balance1.Visible = false;
        }
        catch (Exception ex)
        {
            lblResult.Text = ex.Message;
            lblResult.Visible = true;
        }

    }

    
    protected void GetCommandIMP(string sqlQuery)
    {
        SqlConnection conn = new SqlConnection(strImpex);
        conn.Open();
        SqlCommand cmd = new SqlCommand(sqlQuery, conn);
        SqlDataAdapter da = new SqlDataAdapter();
        cmd.CommandText = sqlQuery;
        cmd.Connection = conn;
        da.SelectCommand = cmd;
        int result = cmd.ExecuteNonQuery();
    }
    //protected void GetCommandMy(string Query, string connSTR)
    //{

    //    MySqlConnection conn = new MySqlConnection(connSTR);
    //    conn.Open();
    //    MySqlCommand cmd = new MySqlCommand(Query, conn);
    //    cmd.CommandText = Query;
    //    cmd.Connection = conn;
    //    int res = cmd.ExecuteNonQuery();
    //    conn.Close();
    //}
    public DataSet GetDataSet(string sqlStatement1)
    {
        SqlConnection conn1 = new SqlConnection(strImpex);
       
        SqlDataAdapter da1 = new SqlDataAdapter(sqlStatement1, conn1);

        DataSet ds1 = new DataSet();
        da1.Fill(ds1, "datas");
        return ds1;

    }   
    protected void SearchInvoice(string INo)
    {

        string strQuery = "select * from M_ServiceMaster order by fyear desc";
        drServiceTax.DataSource = GetDataSet(strQuery);
        drServiceTax.DataTextField = "sTax";
        drServiceTax.DataValueField = "serviceTax";
        drServiceTax.DataBind();
        Session["IINVNO"] = INo;
        Session["INO"] = INo;
        Session["INVOICECTR"] = INo;
        if (INo == "-1")
        {
            Response.Write("<script>alert('Select Jobs Number')</script>");
            txtInvoiceNo.Focus();

        }
        else
        {
            SqlConnection conn = new SqlConnection(strImpex);
            string sqlQuery = "select * from M_IEC_InvoiceNew where invoice='" + INo + "'";
            SqlDataAdapter da = new SqlDataAdapter(sqlQuery, conn);
            DataSet ds = new DataSet();
            da.Fill(ds, "EditInvoice");

            if (ds.Tables["EditInvoice"].Rows.Count == 0)
            {
                txtAdd1.Text = "";
               
                txtAssValue.Text = "";
                txtBENo.Text = "";
                txtBLNo.Text = "";
                txtCity.Text = "";
                txtNCNTR.Text = "";
                txtCompName.Text = "";
                txtCustomDuty.Text = "";
                txtImpotItem.Text = "";
                txtJobNo.Text = "";
                txtParty_Reff.Text = "";
                
                txtQty.Text = "";
                txtRupees.Text = "";
                txtSubParty.Text = "";
         
                SubTotal.Text = "0.00";
                sTax.Text = "0.00";
                EdCess.Text = "0.00";
                SHCess.Text = "0.00";
                Totals.Text = "0.00";
                LessAd.Text = "0.00";
                BalanceDue.Text = "0.00";
               
                Response.Write("<script>alert('Please Give Correct Invoice Number ')</script>");
                
            }
            else
            {
                DataRowView row = ds.Tables["EditInvoice"].DefaultView[0];
                string InNo = row["Invoice"].ToString();
                string InDate = row["invoiceDate"].ToString();
                string CmpName = row["CompName"].ToString();
                string SubParty = row["sub_party"].ToString();
                string Addr1 = row["address1"].ToString();
                string Addr2 = row["address2"].ToString();
                string City = row["City"].ToString();
                string phone = row["phone"].ToString();
                string Mode = row["Mode"].ToString();

                string PReff = row["partyReff"].ToString();
                string JobNo = row["jobno"].ToString();
                string BLNo = row["BLNo"].ToString();
                string BENoDate = row["BENoDate"].ToString();

                try
                {
                    string[] BeSplit = BENoDate.Split(new string[] { "dt" }, StringSplitOptions.RemoveEmptyEntries);
                    if ((BeSplit.Length != 0) && (BeSplit.Length != 1))
                    {
                        string BeNo = BeSplit[0].Trim();
                        string BeDate = BeSplit[1].TrimStart('.').Trim();
                        txtBENo.Text = BeNo.Trim();
                        txtBEDate.Text = BeDate.Trim();
                    }
                }
                catch(Exception ex)
                {
                }
                string Imp_item = row["importitem"].ToString();
                string Qty = row["Quantity"].ToString();
                string AssValue = row["ass_value"].ToString();
                string ContainerNo = row["Container_no"].ToString();
                string CustomsDuty = row["Custom_duty"].ToString();

                DateTime iDT = Convert.ToDateTime(InDate);
                invDate.Text = iDT.ToString("dd/MM/yyyy");

                string suffix = row["suffix"].ToString();
                string notes = row["notes"].ToString();

                string taxAmount = row["subTotalTax"].ToString();
                string NonTaxAmount = row["subTotal"].ToString();
               
                string taxpercent = row["STaxpercent"].ToString();
                
                string ServTax = row["service_tax"].ToString();
                string ECess = row["edu_cess"].ToString();
                string SCess = row["sec_chess"].ToString();
                string Gr_Total = row["grand_total"].ToString();
                string Less_Advance = row["less_advance"].ToString();
                string net_total = row["net_total"].ToString();
                string Rupees = row["NetTotal_words"].ToString();

                string impRK = row["impRemark"].ToString();
                string intRK = row["interRemark"].ToString();

                txtInvSeqNo.Text = row["InvSeqNo"].ToString();

                ddlTallyAccountName.SelectedItem.Text = row["TallyAccountName"].ToString();
                ddlTallySubPartyName.SelectedItem.Text = row["TallySubPartyName"].ToString();
                if (Mode == "IMP")
                {
                    Label16.Text = "BE NO./DT.";
                    Label19.Text = "Ass. Value";
                    lblInvoice.Text = "INVOICE - IMPORTS";
                    lblinvNumber.Text = "INVOICE NO.:";
                }
                else
                {
                    Label17.Text = "Item Exported";
                    Label16.Text = "SB NO./DT.";
                    Label19.Text = "FOB Value";
                    lblInvoice.Text = "INVOICE - EXPORTS";
                    lblinvNumber.Text = "INVOICE NO.:";
                }
                Mode = "";

               
                txtimpRemark.Text = impRK;
                txtIndentRemark.Text = intRK;

                txtSubParty.Text = SubParty;
                txtSubPartyAddr.Text = row["SubPartyAddr"].ToString();
                txtCompName.Text = CmpName;
                
                txtAdd1.Text = Addr1;
                Session["state"] = Addr2;
                txtCity.Text = City;
                Session["Phone"] = phone;
                txtParty_Reff.Text = PReff;
                txtJobNo.Text = JobNo;
                txtBLNo.Text = BLNo;
                //txtBENo.Text = BENoDate;
                txtImpotItem.Text = Imp_item;
                txtQty.Text = Qty;
                txtAssValue.Text = AssValue;
                txtCustomDuty.Text = CustomsDuty;
                txtNCNTR.Text = ContainerNo;
                
                drServiceTax.SelectedValue = taxpercent;

                txtNote.Text = notes;
                txtSuffix.Text = suffix;


               
                SubTotal.Text = NonTaxAmount;
                SubTotalTax.Text = taxAmount;

              
                sTax.Text = ServTax;
                EdCess.Text = ECess;
                SHCess.Text = SCess;
                Totals.Text = Gr_Total;
                LessAd.Text = Less_Advance;
                BalanceDue.Text = net_total;
                balance1.Text = net_total;
                txtRupees.Text = RsConvert.rupees(Convert.ToInt64(balance1.Text));
                invoiceDTL(INo);

            }

        }
    }
    public DataSet GetDataINVDETL(string ino)
    {
        SqlConnection conn = new SqlConnection(strImpex);
        string sqlStatement = "select invoice, sno, charge_desc, receipt, amount, Narration, ServiceTax, CAST(ServiceTaxPercent AS int) AS ServiceTaxPercent, ServiceTaxAmount, Approved,Completed from T_IEC_InvoiceNew_dtl where invoice='" + ino + "' order by sno";
        SqlDataAdapter da = new SqlDataAdapter(sqlStatement, conn);
        DataSet ds = new DataSet();
        da.Fill(ds, "iec");
        return ds;
    }
    protected void invoiceDTL(string invNO)
    {
         DataSet ds = GetDataINVDETL(invNO);
         if (ds.Tables["iec"].Rows.Count == 0)
         {
             string Query = "insert into T_iec_invoiceNew_DTL(sno,invoice,charge_desc,amount) values(1,'" + invNO + "','AAI',0)";
             GetCommand(Query);
             GridView1.DataSource = GetDataINVDETL(invNO);
             GridView1.DataBind();
         }
         else
         {
             GridView1.DataSource = GetDataINVDETL(invNO);
             GridView1.DataBind();
         }
        GETGridVal(invNO);
    }
    protected void GETGridVal(string ino)
    {
        foreach (GridViewRow row in GridView1.Rows)
        {
            Label txtSNO = (Label)row.FindControl("lblsno");
            TextBox txtCharge = (TextBox)row.FindControl("txtDetails");
            TextBox txtRCPT = (TextBox)row.FindControl("txtRecpt");
            DropDownList ServiceTaxPer = (DropDownList)row.FindControl("ddlStax");
            TextBox ServiceTaxAmt = (TextBox)row.FindControl("txtStaxAmt");
            //CheckBox chk = (CheckBox)row.FindControl("chkSTAX");
            try
            {
                string sno = txtSNO.Text;
                string Query = "select  invoice, sno, charge_desc, receipt, amount, Narration, ServiceTax, CAST(ServiceTaxPercent AS int) AS ServiceTaxPercent, ServiceTaxAmount, Approved,Completed from T_iec_invoiceNew_DTL where invoice='" + ino + "' and sno='" + sno + "'";
                SqlConnection conn = new SqlConnection(strImpex);
                SqlDataAdapter da = new SqlDataAdapter(Query, conn);
                DataSet ds = new DataSet();
                da.Fill(ds, "INVDET");
                if (ds.Tables["INVDET"].Rows.Count != 0)
                {                 
                DataRowView ROW = ds.Tables["INVDET"].DefaultView[0];
                string STAXPer = ROW["ServiceTaxPercent"].ToString();
                DropDownList ddlStax = (DropDownList)row.FindControl("ddlStax");
                string strQuery = "select * from M_ServiceMaster where fyear='" + fyear + "' order by fyear desc";
                ddlStax.DataSource = GetDataSQL(strQuery);
                ddlStax.DataTextField = "sTax";
                ddlStax.DataValueField = "sTax";
                ddlStax.DataBind();
                ServiceTaxPer.SelectedValue = STAXPer;
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('" + ex.Message + "');", true);
            }

        }
        foreach (GridViewRow row in GridView1.NewRows)
        {
            TextBox amt = (TextBox)row.FindControl("amt1");
            DropDownList ddlStax = (DropDownList)row.FindControl("ddlStax");
            TextBox staxamt = (TextBox)row.FindControl("txtStaxAmt");
            string strQuery = "select * from M_ServiceMaster where fyear='" + fyear + "' order by fyear desc";
            ddlStax.DataSource = GetDataSQL(strQuery);
            ddlStax.DataTextField = "sTax";
            ddlStax.DataValueField = "sTax";
            ddlStax.DataBind();
            if (staxamt.Text == "")
            {
                staxamt.Text = "0.00";
            }
            if (amt.Text == "")
            {
                amt.Text = "0.00";
            }
        }
    }
    protected void BtnClose_Click(object sender, EventArgs e)
    {
        txtCompName.Text = "";
        txtAdd1.Text = "";
       
    }
    protected void LKRupees_Click(object sender, EventArgs e)
    {
        txtRupees.Text = RsConvert.rupees(Convert.ToInt64(balance1.Text));
        Submit.Focus();
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        String sno = (string)Session["INVOICECTR"];
        Session["BILLTYPE"] = "SB";
        Session["InvNoRep"] = sno;


        string strQuery = "select * from M_iec_invoiceNew where invoice='" + sno  + "' and contr_code is null and particular1 is not null";
        SqlConnection conn = new SqlConnection(strImpex);

        SqlDataAdapter da = new SqlDataAdapter(strQuery, conn);
        DataSet ds = new DataSet();
        da.Fill(ds, "table");
        if (ds.Tables["table"].Rows.Count == 0)
            ScriptManager.RegisterStartupScript(this, this.GetType(), "popup", "window.open('../frmImpInvoiceReport.aspx','_blank','width=1120,height=720, menubar=no, scrollbars=yes, toolbar=no, location=no, resizable=no, left=10, top=120');", true);
          
        else
            ScriptManager.RegisterStartupScript(this, this.GetType(), "popup", "window.open('../frmImpInvoiceReport.aspx','_blank','width=1120,height=720, menubar=no, scrollbars=yes, toolbar=no, location=no, resizable=no, left=10, top=120');", true);

           
    }
    protected void ServiceTax_TextChanged(object sender, EventArgs e)
    {
        

    }
    protected void LB_Logout_Click(object sender, EventArgs e)
    {
        try
        {
            FormsAuthentication.SignOut();
            Session["USER-NAME"] = "";
            Session.Abandon();
            Session.Clear();

            Response.Redirect("~/PIPL.aspx", false);//All one has to do is set the endResponse property of Response.Redirect to be false.
            
        }
        catch (Exception ex)
        {
            Response.Write("<script>alert('" + ex.Message + "')</script>");

        }
    }
  
    protected void GridView1_RowDataBond(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            TextBox amt = (TextBox)e.Row.FindControl("amt1");
            TextBox narration = (TextBox)e.Row.FindControl("txtNarration");
            TextBox particular = (TextBox)e.Row.FindControl("txtDetails");
            //TextBox amt = (TextBox)e.Row.FindControl("amt1");
            if (amt.Text == "")
                amt.Text = "0.00";
            Gross = Gross + Convert.ToDouble(amt.Text);
            if ((amt.Text == "") && (particular.Text == ""))
            { 
            DropDownList ddlStax = (DropDownList)e.Row.FindControl("ddlStax");
            string strQuery = "select * from M_ServiceMaster where fyear='" + fyear + "' order by fyear desc";
            ddlStax.DataSource = GetDataSQL(strQuery);
            ddlStax.DataTextField = "sTax";
            ddlStax.DataValueField = "sTax";
            ddlStax.DataBind();
            }
        }
        
    }
    public DataSet GetDataSQL(string Query)
    {
        SqlConnection conn = new SqlConnection(strImpex);
        conn.Open();
        SqlDataAdapter da = new SqlDataAdapter(Query, conn);
        DataSet ds = new DataSet();
        da.Fill(ds, "SQLtable");
        conn.Close();
        return ds;
    }

    protected void amt1_TextChanged(object sender, EventArgs e)
    {
       
    }
    protected void GetTransaction()
    {
        string cmp = (string)Session["CMP"];
        foreach (GridViewRow row in GridView1.Rows)
        {

            TextBox txt = (TextBox)row.FindControl("amt1");
           
            //CheckBox chk = (CheckBox)row.FindControl("chkSTAX");
            if (txt.Text == "")
                txt.Text = "0";
                //if (chk.Checked)
                //{
                //    GetServiceTax(cmp);
                //    Double amt0 = Convert.ToDouble(txt.Text);
                //    Double aStax = amt0 / 100 * vSTax;
                //    Double aECess = aStax / 100 * vECess;
                //    Double aSHECess = aStax / 100 * vSHECess;

               
                //    GrossTot = GrossTot + amt0;
                //    gSTAX = gSTAX + aStax;
                //    gECess = gECess + aECess;
                //    gSHECess = gSHECess + aSHECess;
                //}
                //else
                //{
               
                //    Double tot = Convert.ToDouble(txt.Text);
                //    total = total + tot;
                //    txt.Text = tot.ToString("#0.00#");
                
                //}
        }
        foreach (GridViewRow Row in GridView1.NewRows)
        {
            TextBox txt = (TextBox)Row.FindControl("amt1");
          
            //CheckBox chk = (CheckBox)Row.FindControl("chkSTAX");
            if (txt.Text == "")
                txt.Text = "0";
            //if (chk.Checked)
            //{
            //    GetServiceTax(cmp);
            //    Double amt0 = Convert.ToDouble(txt.Text);
            //    Double aStax = amt0 / 100 * vSTax;
            //    Double aECess = aStax / 100 * vECess;
            //    Double aSHECess = aStax / 100 * vSHECess;

            //    txt.Text = amt0.ToString("#0.00#");
              
            //    GrossTot = GrossTot + amt0;
            //    gSTAX = gSTAX + aStax;
            //    gECess = gECess + aECess;
            //    gSHECess = gSHECess + aSHECess;
            //}
            //else
            //{
               
            //    Double tot = Convert.ToDouble(txt.Text);
            //    total = total + tot;
            //    txt.Text = tot.ToString("#0.00#");
                
            //}
        }

        //gSTAX = Math.Round(gSTAX);
        //SubTotal.Text = total.ToString("#0.00#");
        //SubTotalTax.Text = GrossTot.ToString("#0.00#");
        //sTax.Text = gSTAX.ToString("#0.00#");

        //gECess = Math.Round(gECess);
        //gSHECess = Math.Round(gSHECess);

        //EdCess.Text = gECess.ToString("#0.00#");
        //SHCess.Text = gSHECess.ToString("#0.00#");
        //Double gTotals = total + GrossTot + gSTAX + gSHECess + gECess;
        //Totals.Text = gTotals.ToString("#0.00#");
       

       // GetPERCENT();
    }
    protected void GetPERCENT()
    {
        string BILL = "SB";
        Gross = Convert.ToDouble(Totals.Text);
        if (BILL == "SB")
        {
            
            Double NetAmt = Gross;
            Totals.Text = NetAmt.ToString("#0.00#");
            bal = NetAmt - Convert.ToDouble(LessAd.Text);

           
        }
        else
        {
            Double NetAmt = Gross;
            Totals.Text = NetAmt.ToString("#0.00#");
            bal = Convert.ToDouble(Totals.Text) - Convert.ToDouble(LessAd.Text);
           
        }
        Double balanceAmount = Math.Round(bal);

        balance1.Text = balanceAmount.ToString();
        BalanceDue.Text = balanceAmount.ToString("#0.00#");

        txtRupees.Text = RsConvert.rupees(Convert.ToInt64(balance1.Text));
       

    }
    protected void chkSTAX_CheckedChanged(object sender, EventArgs e)
    {
       
    }
    protected void GetServiceTax(string cmp)
    {
        string sVal = drServiceTax.SelectedValue;
        try
        {
            SqlConnection conn2 = new SqlConnection(strImpex);

            string strQuery = "select * from M_ServiceMaster where serviceTax='" + sVal + "'";

            SqlDataAdapter da2 = new SqlDataAdapter(strQuery, conn2);
            DataSet ds2 = new DataSet();
            da2.Fill(ds2, "SERVICETAX");


            DataRowView row = ds2.Tables["SERVICETAX"].DefaultView[0];
            Double stax = Convert.ToDouble(row["serviceTax"].ToString());
            Double Ecess = Convert.ToDouble(row["ecess"].ToString());
            Double SHEcess = Convert.ToDouble(row["shecess"].ToString());
            
            vSTax = stax;
            vECess = Ecess;
            vSHECess = SHEcess;
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('" + ex.Message + "');", true);
        }
    }
    protected void UpdateGridview(string invNO)
    {
        int sno = 1;

        foreach (GridViewRow row in GridView1.Rows)
        {

            TextBox txtCharge = (TextBox)row.FindControl("txtDetails");
            TextBox txtRecpt = (TextBox)row.FindControl("txtRecpt");
            TextBox txtAmt = (TextBox)row.FindControl("amt1");
            Label lsno = (Label)row.FindControl("lblsno");
            TextBox Narrat = (TextBox)row.FindControl("txtNarration");
            DropDownList ServiceTaxPer = (DropDownList)row.FindControl("ddlStax");
            TextBox ServiceTaxAmt = (TextBox)row.FindControl("txtStaxAmt");
            ServiceTaxAmt.Enabled = true;
            //CheckBox chk = (CheckBox)ROW.FindControl("chkSTAX");
            string Narration = Narrat.Text;
            double ServiceTaxPercent = Convert.ToDouble(ServiceTaxPer.SelectedValue);
            double ServiceTaxAmount = Convert.ToDouble(ServiceTaxAmt.Text);

           // CheckBox chk = (CheckBox)row.FindControl("chkSTAX");

            string desc = txtCharge.Text;
            string recpt = txtRecpt.Text;
            string amt = txtAmt.Text;
            string sTAXval = string.Empty;
            if (ServiceTaxPer.SelectedValue == "0")
            {
                sTAXval = "N";
            }
            else
            {
                sTAXval = "Y";
            }
            //if (chk.Checked == true)
            //    sTAXval = "Y";
            //else
            //    sTAXval = "N";
            if (desc == "")
                amt = "";
            if (desc != "" && amt != "0.00" || amt != string.Empty )
            {
                string Query = "update T_iec_invoiceNew_DTL set charge_desc='" + desc + "'," +
                    "receipt='" + recpt + "',sno='" + lsno.Text + "',amount='" + amt + "',serviceTax='" + sTAXval + "',Narration='" + Narration + "',ServiceTaxPercent='" + ServiceTaxPercent + "',ServiceTaxAmount='" + ServiceTaxAmount + "'" +
                    "where invoice='" + invNO + "' and sno='" + lsno.Text + "'";
               GetCommand(Query);
                sno = sno + 1;
            }
            else
            {
                string Query = "delete from T_iec_invoiceNew_DTL where invoice='" + invNO + "' and sno='" + lsno.Text + "'";
                GetCommand(Query);
                sno = sno + 1;
            }
        }
    }

    public void UpdateInvoiceDetails(string desc, string recpt, Int32 slno, decimal amt, string sTAXval, string Narration, double service_Tax, 
        double ServiceTaxPercent, string invNO)
    {
        SqlConnection condetails = new SqlConnection(strImpex);
        condetails.Open();
        SqlCommand cmd = new SqlCommand("spUpdateBills", condetails);//Update Invoice Details Table7
        cmd.CommandType = CommandType.StoredProcedure;

        cmd.Parameters.Add(new SqlParameter("@charge_desc", desc));
        cmd.Parameters.Add(new SqlParameter("@receipt", recpt));
        cmd.Parameters.Add(new SqlParameter("@sno", slno));
        cmd.Parameters.Add(new SqlParameter("@amount", amt));
        cmd.Parameters.Add(new SqlParameter("@serviceTax", sTAXval));
        cmd.Parameters.Add(new SqlParameter("@Narration", Narration));
        cmd.Parameters.Add(new SqlParameter("@ServiceTaxPercent", service_Tax));
        cmd.Parameters.Add(new SqlParameter("@ServiceTaxAmount", ServiceTaxPercent));
        cmd.Parameters.Add(new SqlParameter("@ServiceTaxAmount", invNO));
        cmd.Parameters.Add(new SqlParameter("@BillType", "Invoice"));
        cmd.ExecuteNonQuery();
        condetails.Close();
    }
    protected void InsertNewGridViewRow(string invNO)
    {
        

        int nwRow = GridView1.NewRowCount;
        int oldRow = GridView1.Rows.Count;
        
        
        foreach (GridViewRow row in GridView1.NewRows)
        {


            TextBox txtCharge = (TextBox)row.FindControl("txtDetails");
            TextBox txtRecpt = (TextBox)row.FindControl("txtRecpt");
            TextBox txtAmt = (TextBox)row.FindControl("amt1");
            Label lsno = (Label)row.FindControl("lblsno");
            TextBox Narrat = (TextBox)row.FindControl("txtNarration");
            DropDownList ServiceTaxPer = (DropDownList)row.FindControl("ddlStax");
            TextBox ServiceTaxAmt = (TextBox)row.FindControl("txtStaxAmt");
            ServiceTaxAmt.Enabled = true;
            //CheckBox chk = (CheckBox)ROW.FindControl("chkSTAX");
            string Narration = Narrat.Text;
            double ServiceTaxPercent = Convert.ToDouble(ServiceTaxPer.SelectedValue);
            double ServiceTaxAmount = Convert.ToDouble(ServiceTaxAmt.Text);
            //CheckBox chk = (CheckBox)row.FindControl("chkSTAX");

            string desc = txtCharge.Text;
            string recpt = txtRecpt.Text;
            string amt = txtAmt.Text;
            string sTAXval = string.Empty;
            if (ServiceTaxPer.SelectedValue == "0")
            {
                sTAXval = "N";
            }
            else
            {
                sTAXval = "Y";
            }
            //if (chk.Checked == true)
            //    sTAXval = "Y";
            //else
            //    sTAXval = "N";
            if (amt == "")
                amt = "0.00";
            if (desc != "" && amt != "0.00")
            {
                oldRow = oldRow + 1;

                string Query = "insert into T_iec_invoiceNew_DTL(invoice,sno,charge_desc,receipt,amount,serviceTax,Narration,ServiceTaxPercent,ServiceTaxAmount) " +
                               "values('" + invNO + "'," + oldRow + ",'" + desc + "','" + recpt + "','" + amt + "','" + sTAXval + "','" + Narration + "','" + ServiceTaxPercent + "','" + ServiceTaxAmount + "')";
                GetCommand(Query);
            }
        }
    }
    protected void GetCommand(string sqlQuery)
    {
        SqlConnection conn = new SqlConnection(strImpex);
        conn.Open();
        SqlCommand cmd = new SqlCommand(sqlQuery, conn);
        SqlDataAdapter da = new SqlDataAdapter();
        cmd.CommandText = sqlQuery;
        cmd.Connection = conn;
        da.SelectCommand = cmd;


        int result = cmd.ExecuteNonQuery();
    }
    protected void drServiceTax_SelectedIndexChanged(object sender, EventArgs e)
    {
        
    }
    protected void preview_Click(object sender, EventArgs e)
    {
        //Button1_Click(sender, e);

        String sno = (string)Session["INVOICECTR"];
        Session["BILLTYPE"] = "SB";
        Session["InvNoRep"] = sno;

        Session["InvNo"] = txtInvoiceNo.Text;
        string rep = (string)Session["InvNo"];
        string sub = rep.Substring(4, 2);
        if (sub == "SB")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "popup", "window.open('../frmImpInvoiceReport.aspx','_blank','width=1120,height=720, menubar=no, scrollbars=yes, toolbar=no, location=no, resizable=no, left=10, top=120');", true);
          //  Response.Redirect("../frmImpInvoiceReport.aspx");
        }
        else
        {

            Response.Redirect("../frmDebit.aspx");
        }
    }
    
    protected void btnMail_Click(object sender, EventArgs e)
    {
        try
        {
            OpenMail();
        }//end of try block
        catch (Exception Error)
        {
            if (Error.Message == "Operation aborted (Exception from HRESULT: 0x80004004 (E_ABORT))")
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Please Cheack, Whether Outlook Express Was Configured In Your System Or Not?.Please Open Your Outlook Express Then Click Ok. ');", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('" + Error.Message + "');", true);
            }
        }
    }//end of Email Method
    protected void OpenMail()
    {

        String sno = (string)Session["INVOICECTR"];
        Session["BILLTYPE"] = "SB";
        Session["INVOICECTR"] = sno;
        if (Session["Maill"] == null)
        {
            Session["MAILBUTTON"] = "OK";
            Session["PageName"] = "EditInvoiceStaxUp.aspx";
            Session["Maill"] = "SendMaill";
            
            ScriptManager.RegisterStartupScript(this, this.GetType(), "popup", "window.open('CryInvoiceReportSTax.aspx','_blank','menubar=no, scrollbars=no, toolbar=no, location=no, resizable=no,height=650,width=700, left=20, top=20');", true);

           
        }
       
    }
    protected void GetMailOnline(string mTo)
    {
        string jnos = txtJobNo.Text;

     
        string path = (string)Session["AttachmentPath"];

        string mf = (string)Session["USER-NAME"];
        string mt = "a.karna@vts.in";
        string msub = "Re: Billing";
        string mmsg = "Please find attached herewith for " + jnos + " of Inovice";
        SendMail(mf, mt, mmsg, msub, path);
    }
    protected void SendMail(string mFrom, string mTo, string mMessage, string mSubject, string mAttached)
    {
        try
        {
            MailMessage message = new MailMessage();
            message.From = new MailAddress(mFrom);
           

            message.Subject = mSubject;
            message.Body = mMessage;
            if (mTo != "" || mTo != string.Empty)
            {
                string[] strTo = mTo.Split(';');
                foreach (string strThisTo in strTo)
                {
                    strThisTo.Trim();
                    message.To.Add(strThisTo);
                }
            }


           

            if (mAttached != "" | mAttached != string.Empty)
            {
                string[] strAtt = mAttached.Split(',');
                foreach (string strThisMatt in strAtt)
                {
                    strThisMatt.Trim();
                    Attachment atts = new Attachment(strThisMatt);
                    message.Attachments.Add(atts);
                }
            }
            SmtpClient mySmtpClient = new SmtpClient("smtp.bizmail.yahoo.com", 587);
            mySmtpClient.Credentials = new System.Net.NetworkCredential("apps@vts.in", "VtsApps");
            mySmtpClient.Send(message);
        }
        catch (FormatException ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('" + ex.Message + "');", true);

           
        }
    }

    
     protected void LessAd_TextChanged(object sender, EventArgs e)
    {
        txtRupees.Text = RsConvert.rupees(Convert.ToInt64(balance1.Text));
        Submit.Focus();
    
    }
     protected void BtnExit_Click(object sender, EventArgs e)
     {
         ScriptManager.RegisterStartupScript(this, this.GetType(), "popup", "window.close();", true);

     }
     protected void ExportTally_Click(object sender, EventArgs e)
     {

         string pName = txtCompName.Text;
         if (pName == "APOLLO TYRES LIMITED")
             GetAPOLLO();
         else
             GetFSIO();
     }
     protected void GetFSIO()
     {

         string jno = txtJobNo.Text;


         string sqlQuerySTR = "";
         string sqlQuery = "";

         string strMST = "";
         string strDTL = "";

         string invNO = "";
         string Party = "";
         string dates = "";
         string InvoiceType = "";
         string nTotal = "";
         string refer = "";
         string Naration = "";
         string jNo = "";

         string file = string.Empty;
         string billtype = "";
         string datetime = "";
         string serverPath = "";
         string genFile = "";
         string dATE = "";





         sqlQuery = "select * from M_iec_invoicenew i where i.jobno='" + jno + "' ";





         if (sqlQuery != "")
         {



             datetime = DateTime.Now.Day.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Year.ToString() + "-" + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString();
             serverPath = Server.MapPath("~") + "\\" + "CSV";
             genFile = billtype + datetime;

             dATE = DateTime.Now.Day.ToString() + "." + DateTime.Now.Month.ToString() + "." + DateTime.Now.Year.ToString();
             

             if (Directory.Exists(serverPath))
             {
                 string PartyNameDirectory = serverPath + "\\" + "Tally" + "\\" + dATE;
                 if (Directory.Exists(PartyNameDirectory))
                 {
                     file = PartyNameDirectory + "\\" + billtype + datetime + ".csv";
                 }
                 else
                 {
                     Directory.CreateDirectory(PartyNameDirectory);
                     file = PartyNameDirectory + "\\" + billtype + datetime + ".csv";
                 }

             }
             else
             {
                 Directory.CreateDirectory(serverPath);
                 string PartyNameDirectory = serverPath + "\\" + "Tally" + "\\" + dATE;
                 if (Directory.Exists(PartyNameDirectory))
                 {
                     file = PartyNameDirectory + "\\" + billtype + datetime + ".csv"; ;
                 }
                 else
                 {
                     Directory.CreateDirectory(PartyNameDirectory);
                     file = PartyNameDirectory + "\\" + billtype + datetime + ".csv"; ;
                 }

             }

             Session["FILEPATH"] = file;
             FileStream fs = new FileStream(file, FileMode.Create, FileAccess.Write, FileShare.Read);
             StreamWriter sw = new StreamWriter(fs);
             TextWriter tw = sw;

             string pName = "";
             Double amt = 0;
             string jnos = "";
             Double STAX = 0;
             tw.Write("Voucher Type" + ","); tw.Write("Invoice No" + ","); tw.Write("Date" + ","); tw.Write("Ref" + ","); tw.Write("Dr Account" + ","); tw.Write("Cr.Account" + ",");

           
             tw.Write("Cost Center" + ","); tw.Write("Amount" + ","); tw.Write("Narration\n");


             jNo = (string)Session["IINVNO"];
          
             string iTYPE = "SB";
             string sqlQueryM = "";
             //Master Records
             SqlConnection connM = new SqlConnection(strImpex);

             strMST = "M_iec_invoicenew";
             strDTL = "T_iec_invoicenew_dtl";

             sqlQueryM = "select INVOICE,invoiceDate,jobNo,invoiceType,Sub_party,compName,invoiceNo,Grand_Total,service_tax + edu_cess + sec_chess as STAX," +
                           "blno , beNoDate , importItem , Quantity , partyReff  " +
                           "from " + strMST + "  where invoice='" + jNo + "'";
             SqlDataAdapter daM = new SqlDataAdapter(sqlQueryM, connM);
             DataSet dsM = new DataSet();
             daM.Fill(dsM, "INVOICEMST");

             if (dsM.Tables["INVOICEMST"].Rows.Count != 0)
             {
                 DataRowView rowM = dsM.Tables["INVOICEMST"].DefaultView[0];
                 string BillDate = rowM["invoiceDate"].ToString();
                 invNO = rowM["INVOICEno"].ToString();
                 jnos = rowM["jobNo"].ToString();
                 Party = rowM["compName"].ToString();
                 string NRef = rowM["invoiceNo"].ToString();
                 InvoiceType = rowM["invoiceType"].ToString();
                 nTotal = rowM["Grand_Total"].ToString();
                
                 string subPName = rowM["Sub_party"].ToString();
                 if (subPName != "")
                     Party = subPName;
                 
                 pName = Party.Replace(",", " ");

                 //Naration fields
                 string blno = " AWB/BL.NO.:" + rowM["blno"].ToString();
                 string beNo = " BE.NO.:" + rowM["beNoDate"].ToString();
                 string impItem = rowM["importItem"].ToString();
                 impItem = impItem.Replace(",", " ");

                 string qty = rowM["Quantity"].ToString();
                 string pRef = rowM["partyReff"].ToString();
                 string serTAX = rowM["STAX"].ToString();


                 if (serTAX == "")
                     serTAX = "0";
                 STAX = Convert.ToDouble(serTAX);


                 string[] iDate = BillDate.Split('-');
                 dates = iDate[2] + "-" + iDate[1] + "-" + iDate[0];

                 if (jnos.StartsWith("IMP") == true)
                     jnos = jnos.Substring(4, 5);

                 // Start CSV Header Text
                

                 string[] pn = Party.Split(' ');
                 Party = pn[0];
                 if (pn[0].Length < 3)
                     Party = pn[0] + pn[1];

                 refer = jnos + " / " + Party;
                 Naration = "JOBNO:" + jnos + "/" + blno + "/" + beNo + "/ " + impItem + "/ " + qty + "/ " + pRef;
                 BHamt = BHamt + amt;
                 // END CSV Header Text

                 if (iTYPE == "SB" || iTYPE == "ATLSB" || iTYPE == "EXPSB")
                 {
                     InvoiceType = "sales";
                     tw.Write(InvoiceType + ","); tw.Write(invNO + ","); tw.Write(dates + ","); tw.Write(refer + ",");
                     tw.Write(pName + ","); tw.Write(","); tw.Write(refer + ","); tw.Write(nTotal + ","); tw.Write(Naration + ","); tw.Write("\n");
                 }
                 else
                 {
                     InvoiceType = "Debit Note";
                     tw.Write(InvoiceType + ","); tw.Write(invNO + ","); tw.Write(dates + ","); tw.Write(refer + ",");
                     tw.Write(","); tw.Write(pName + ","); tw.Write(refer + ","); tw.Write(nTotal + ","); tw.Write(Naration + ","); tw.Write("\n");
                 }
             }
             //Transaction Records 
             SqlConnection connSTR = new SqlConnection(strImpex);
             sqlQuerySTR = "select sno,invoice,charge_desc,amount from " + strDTL + "  " +
                           "where invoice='" + jNo + "' order by sno";
             SqlDataAdapter daSTR = new SqlDataAdapter(sqlQuerySTR, connSTR);
             DataSet dsSTR = new DataSet();
             daSTR.Fill(dsSTR, "INVOICEDTL");
             DataTable dtSTR = dsSTR.Tables[0];
             if (dsSTR.Tables["INVOICEDTL"].Rows.Count != 0)
             {
                 int i = 1;
                 int length = dtSTR.Rows.Count;
                 BHamt = 0;

                 foreach (DataRow rowSTR in dtSTR.Rows)
                 {
                     string sno = rowSTR["sno"].ToString();
                     amt = Convert.ToDouble(rowSTR["amount"].ToString());
                     fsTotal = fsTotal + amt;

                     string ino = rowSTR["invoice"].ToString();
                     string desc = rowSTR["charge_desc"].ToString();
                     desc = desc.Replace(",", " ");

                     if (pName == "BHARAT HEAVY ELECTRICALS LIMITED ")
                     {
                         BHamt = BHamt + amt;
                         if (iTYPE == "SB" || iTYPE == "ATLSB" || iTYPE == "EXPSB")
                         {
                             if (i == length)
                             {
                                 tw.Write(InvoiceType + ","); tw.Write(invNO + ","); tw.Write(dates + ","); tw.Write(refer + ",");
                                 tw.Write(",");
                                 tw.Write("Service Charges" + ","); tw.Write(refer + ","); tw.Write(BHamt + ","); tw.Write(Naration + ","); tw.Write("\n");
                             }
                         }
                         else
                         {
                             if (i == length)
                             {
                                 tw.Write(InvoiceType + ","); tw.Write(invNO + ","); tw.Write(dates + ","); tw.Write(refer + ",");
                                 tw.Write("Service Charges" + ","); tw.Write(pName + ","); tw.Write(refer + ","); tw.Write(BHamt + ","); tw.Write(Naration + ","); tw.Write("\n");       
                             }
                         }
                     }

                     else if (pName == "BHARAT HEAVY PLATE AND VESSELS LTD")
                     {
                         BHamt = BHamt + amt;
                         if (iTYPE == "SB" || iTYPE == "ATLSB" || iTYPE == "EXPSB")
                         {
                             if (i == length)
                             {
                                 tw.Write(InvoiceType + ","); tw.Write(invNO + ","); tw.Write(dates + ","); tw.Write(refer + ",");
                                 tw.Write(",");
                                 tw.Write("Service Charges" + ","); tw.Write(refer + ","); tw.Write(BHamt + ","); tw.Write(Naration + ","); tw.Write("\n");
                             }
                         }
                         else
                         {
                             if (i == length)
                             {
                                 tw.Write(InvoiceType + ","); tw.Write(invNO + ","); tw.Write(dates + ","); tw.Write(refer + ",");
                                 tw.Write("Service Charges" + ","); tw.Write(pName + ","); tw.Write(refer + ","); tw.Write(BHamt + ","); tw.Write(Naration + ","); tw.Write("\n");
                             }
                         }
                     }
                     else
                     {
                         if (iTYPE == "SB" || iTYPE == "ATLSB" || iTYPE == "EXPSB")
                         {
                             if (ino != fJOBNO)
                             {
                                 tw.Write(InvoiceType + ","); tw.Write(invNO + ","); tw.Write(dates + ","); tw.Write(refer + ",");
                                 tw.Write(",");
                                 tw.Write(desc + ","); tw.Write(refer + ","); tw.Write(amt + ","); tw.Write(Naration + ","); tw.Write("\n");
                                 fJOBNO = ino;
                             }
                             else
                             {
                                 tw.Write(InvoiceType + ","); tw.Write(invNO + ","); tw.Write(dates + ","); tw.Write(refer + ",");
                                 tw.Write(",");
                                 tw.Write(desc + ","); tw.Write(refer + ","); tw.Write(amt + ","); tw.Write(Naration + ","); tw.Write("\n");
                             }
                         }
                         else
                         {
                             if (ino != fJOBNO)
                             {
                                 tw.Write(InvoiceType + ","); tw.Write(invNO + ","); tw.Write(dates + ","); tw.Write(refer + ",");
                                 tw.Write(desc + ","); tw.Write(","); tw.Write(refer + ","); tw.Write(amt + ","); tw.Write(Naration + ","); tw.Write("\n");
                                 fJOBNO = ino;
                             }
                             else
                             {
                                 tw.Write(InvoiceType + ","); tw.Write(invNO + ","); tw.Write(dates + ","); tw.Write(refer + ",");
                                 tw.Write(desc + ","); tw.Write(","); tw.Write(refer + ","); tw.Write(amt + ","); tw.Write(Naration + ","); tw.Write("\n");
                             }
                         }
                     }

                     if (i == length)
                     {
                         if (iTYPE == "SB" || iTYPE == "ATLSB" || iTYPE == "EXPSB")
                         {
                             tw.Write(InvoiceType + ","); tw.Write(invNO + ","); tw.Write(dates + ","); tw.Write(refer + ",");
                             tw.Write(",");
                             tw.Write("Service Tax" + ","); tw.Write(refer + ","); tw.Write(STAX + ","); tw.Write(Naration + ","); tw.Write("\n");
                             fsTotal = fsTotal + STAX;
                         }
                         fsTotal = 0;
                     }
                   
                     i = i + 1;
                 }
             }

             tw.Flush();
             tw.Close();
             string sGenName = file;
             string sFileName = file;
             string fdATE = DateTime.Now.Day.ToString() + "." + DateTime.Now.Month.ToString() + "." + DateTime.Now.Year.ToString();

             System.IO.FileStream fss = null;

             fss = System.IO.File.Open(Server.MapPath("CSV/Tally/" + fdATE + "/" +
                     genFile + ".csv"), System.IO.FileMode.Open);
             byte[] btFile = new byte[fss.Length];
             fss.Read(btFile, 0, Convert.ToInt32(fss.Length));
             fss.Close();

             FileInfo filep = new FileInfo(sGenName);
             string newPath = Path.GetFileName(sGenName);
             Response.AddHeader("Content-disposition", "attachment; filename=" + newPath);
             Response.ContentType = "application/ms-excel";
             Response.BinaryWrite(btFile);
             Response.End();

             ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Billing Details has been Generated....');", true);
         }
         else
             ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Please select Bill type....');", true);
     }
     protected void GetAPOLLO()
     {
         string jno = txtJobNo.Text;
         Double nTotal = 0;
         string sqlQuerySTR = "";
         string sqlQuery = "";
         string jobNO = "";
         string jobsno = "";
         string strMST = "";
         string strDTL = "";
         string invNO = "";
         string Party = "";
         string dates = "";
         string InvoiceType = "";
         string refer = "";
         string Naration = "";
         string jNo = "";
         string file = string.Empty;
         string billtype = "";
         string datetime = "";
         string serverPath = "";
         string genFile = "";
         string dATE = "";
         string BType = "ATLSB";
       
            
            sqlQuery = "select * from M_iec_invoicenew i where i.jobno='" + jno + "'  and i.compName ='" + txtCompName.Text + "' union " +
                         "select * from M_iec_debit j where j.jobno='" + jno + "' and j.compName ='" + txtCompName.Text + "' order by invoiceDate";
             
         SqlConnection conn = new SqlConnection(strImpex);
         SqlDataAdapter da = new SqlDataAdapter(sqlQuery, conn);
         DataSet ds = new DataSet();
         DataTable dt = new DataTable(); 
         if (sqlQuery != "")
         {
             da.Fill(ds, "INVOICES");
             dt = ds.Tables[0];

             if (BType == "0")
                 billtype = "Billing";
             else if (BType == "ATLSB" || BType == "ATLDB" || BType == "ATLDEM")
                 billtype = "SalesBill";
             else
                 billtype = "DebitNote";

             datetime = DateTime.Now.Day.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Year.ToString() + "-" + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString();
             serverPath = Server.MapPath("~") + "\\" + "CSV";
             genFile = billtype + datetime;

             dATE = DateTime.Now.Day.ToString() + "." + DateTime.Now.Month.ToString() + "." + DateTime.Now.Year.ToString();
             
             if (Directory.Exists(serverPath))
             {
                 string PartyNameDirectory = serverPath + "\\" + "Tally" + "\\" + dATE;
                 if (Directory.Exists(PartyNameDirectory))
                 {
                     file = PartyNameDirectory + "\\" + billtype + datetime + ".csv";
                 }
                 else
                 {
                     Directory.CreateDirectory(PartyNameDirectory);
                     file = PartyNameDirectory + "\\" + billtype + datetime + ".csv";
                 }
             }
             else
             {
                 Directory.CreateDirectory(serverPath);
                 string PartyNameDirectory = serverPath + "\\" + "Tally" + "\\" + dATE;
                 if (Directory.Exists(PartyNameDirectory))
                 {
                     file = PartyNameDirectory + "\\" + billtype + datetime + ".csv"; ;
                 }
                 else
                 {
                     Directory.CreateDirectory(PartyNameDirectory);
                     file = PartyNameDirectory + "\\" + billtype + datetime + ".csv"; ;
                 }
             }

             Session["FILEPATH"] = file;
             FileStream fs = new FileStream(file, FileMode.Create, FileAccess.Write, FileShare.Read);
             StreamWriter sw = new StreamWriter(fs);
             TextWriter tw = sw;

             string pName = "";
             Double amt = 0;
             string jnos = "";
             Double STAX = 0;
             tw.Write("Voucher Type" + ","); tw.Write("Invoice No" + ","); tw.Write("Date" + ","); tw.Write("Ref" + ","); tw.Write("Dr Account" + ","); tw.Write("Cr.Account" + ",");
             tw.Write("Cost Center" + ","); tw.Write("Amount" + ","); tw.Write("Narration\n");

             string iTYPE = "";
             string blno = "";
             string beNo = "";
             string impItem = "";
             string qty = "";
             string pRef = "";
             string serTAX = "";
             int i = 0;
             int length = 0;

             Double agCharges = 0;
             foreach (DataRow row in dt.Rows)
             {
                 jNo = row["invoice"].ToString();
           
                 iTYPE = row["invoiceType"].ToString();
                 jobNO = row["jobno"].ToString();
                 string sqlQueryM = "";
               
                 SqlConnection connM = new SqlConnection(strImpex);
                 if (iTYPE == "ATLSB")
                 {
                     strMST = "M_iec_invoicenew";
                     strDTL = "T_iec_invoicenew_dtl";
                 }
                 else
                 {
                     strMST = "M_iec_debit";
                     strDTL = "T_iec_debit_dtl";
                 }
                 sqlQueryM = "select INVOICE,invoiceDate,jobNo,invoiceType,Sub_party,compName,invoiceNo,Grand_Total,service_tax + edu_cess + sec_chess as STAX," +
                               "blno , beNoDate , importItem , Quantity , partyReff  " +
                               "from " + strMST + "  where invoice='" + jNo + "'";
                 SqlDataAdapter daM = new SqlDataAdapter(sqlQueryM, connM);
                 DataSet dsM = new DataSet();
                 daM.Fill(dsM, "INVOICEMST");

                 if (dsM.Tables["INVOICEMST"].Rows.Count != 0)
                 {
                     DataRowView rowM = dsM.Tables["INVOICEMST"].DefaultView[0];
                     string BillDate = rowM["invoiceDate"].ToString();
                     if (iTYPE == "ATLSB")
                     {
                         invNO = rowM["INVOICEno"].ToString();
                     }
                     jnos = rowM["jobNo"].ToString();
                     Party = rowM["compName"].ToString();
                     string NRef = rowM["invoiceNo"].ToString();
                     InvoiceType = rowM["invoiceType"].ToString();
                     string nTOL = rowM["Grand_Total"].ToString();
                     if (nTOL == "")
                         nTOL = "0";
                     nTotal = Convert.ToDouble(nTOL);
                     allTotal = allTotal + nTotal;
                     string subPName = rowM["Sub_party"].ToString();
                     if (subPName != "")
                         Party = subPName;

                     pName = Party.Replace(",", " ");
                     blno = " AWB/BL.NO.:" + rowM["blno"].ToString();
                     beNo = " BE.NO.:" + rowM["beNoDate"].ToString();
                     impItem = rowM["importItem"].ToString();
                     impItem = impItem.Replace(",", " ");

                     qty = rowM["Quantity"].ToString();
                     pRef = rowM["partyReff"].ToString();

                     if (iTYPE == "ATLSB")
                     {
                         serTAX = rowM["STAX"].ToString();

                         if (serTAX == "")
                             serTAX = "0";
                         STAX = Convert.ToDouble(serTAX);
                     }

                     string[] iDate = BillDate.Split('-');
                     dates = iDate[2] + "-" + iDate[1] + "-" + iDate[0];

                     if (jnos.StartsWith("IMP") == true)
                         jnos = jnos.Substring(4, 5);

                     string[] pn = Party.Split(' ');
                     Party = pn[0];
                     if (pn[0].Length < 3)
                         Party = pn[0] + pn[1];
                 }


                 //Transaction Records 
                 SqlConnection connSTR = new SqlConnection(strImpex);
                 sqlQuerySTR = "select sno,invoice,charge_desc,amount from " + strDTL + "  " +
                               "where invoice='" + jNo + "' order by sno";
                 SqlDataAdapter daSTR = new SqlDataAdapter(sqlQuerySTR, connSTR);
                 DataSet dsSTR = new DataSet();
                 daSTR.Fill(dsSTR, "INVOICEDTL");
                 DataTable dtSTR = dsSTR.Tables[0];
                 if (dsSTR.Tables["INVOICEDTL"].Rows.Count != 0)
                 {
                     i = 1;
                     length = dtSTR.Rows.Count;
                    

                     foreach (DataRow rowSTR in dtSTR.Rows)
                     {

                         string sno = rowSTR["sno"].ToString();
                         string amtRS = rowSTR["amount"].ToString();
                         if (amtRS == "")
                             amtRS = "0";
                         amt = Convert.ToDouble(amtRS);
                         fsTotal = fsTotal + amt;


                         string ino = rowSTR["invoice"].ToString();
                         string desc = rowSTR["charge_desc"].ToString();
                         desc = desc.Replace(",", " ");
                         BHamt = BHamt + amt;
                         i = i + 1;
                     }
                 }
             }

             if (jnos != fJOBNO)
             {
                 if (pName == "APOLLO TYRES LIMITED")
                 {
                     refer = jnos + " / " + Party;
                     Naration = "JOBNO:" + jnos + "/" + blno + "/" + beNo + "/ " + impItem + "/ " + qty + "/ " + pRef;
                    
                     if (iTYPE == "ATLSB" || iTYPE == "ATLDB" || iTYPE == "ATLDEM")
                     {
                         InvoiceType = "ATL sales";
                         tw.Write(InvoiceType + ","); tw.Write(invNO + ","); tw.Write(dates + ","); tw.Write(refer + ",");
                         tw.Write(pName + ","); tw.Write(","); tw.Write(refer + ","); tw.Write(allTotal + ","); tw.Write(Naration + ","); tw.Write("\n");
                     }
                     if (iTYPE == "ATLSB" || iTYPE == "ATLDB" || iTYPE == "ATLDEM")
                     {

                         tw.Write(InvoiceType + ","); tw.Write(invNO + ","); tw.Write(dates + ","); tw.Write(refer + ",");
                         tw.Write(",");
                         tw.Write("Agency Charges" + ","); tw.Write(refer + ","); tw.Write(BHamt + ","); tw.Write(Naration + ","); tw.Write("\n");
                     }
                     if (iTYPE == "ATLSB" || iTYPE == "ATLDB" || iTYPE == "ATLDEM")
                     {
                         tw.Write(InvoiceType + ","); tw.Write(invNO + ","); tw.Write(dates + ","); tw.Write(refer + ",");
                         tw.Write(",");
                         tw.Write("Service Tax" + ","); tw.Write(refer + ","); tw.Write(STAX + ","); tw.Write(Naration + ","); tw.Write("\n");
                         fsTotal = fsTotal + STAX;
                     }
                     fsTotal = 0;
                 }
                 fJOBNO = jnos;
             }
             tw.Flush();
             tw.Close();
            
             string sGenName = file;
             string sFileName = file;
             string fdATE = DateTime.Now.Day.ToString() + "." + DateTime.Now.Month.ToString() + "." + DateTime.Now.Year.ToString();

             System.IO.FileStream fss = null;

             fss = System.IO.File.Open(Server.MapPath("CSV/Tally/" + fdATE + "/" +
                     genFile + ".csv"), System.IO.FileMode.Open);
             byte[] btFile = new byte[fss.Length];
             fss.Read(btFile, 0, Convert.ToInt32(fss.Length));
             fss.Close();

             FileInfo filep = new FileInfo(sGenName);
             string newPath = Path.GetFileName(sGenName);
             Response.AddHeader("Content-disposition", "attachment; filename=" + newPath);
             Response.ContentType = "application/ms-excel";
             Response.BinaryWrite(btFile);
             Response.End();

             ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Billing Details has been Generated....');", true);
         }
         else
             ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Please select Bill type....');", true);
     }
     protected void btncalculate_Click(object sender, EventArgs e)
     {
         GetTransaction();
     }
     

     protected void chkSupplierInvNo_CheckedChanged(object sender, EventArgs e)
     {
         if (chkSupplierInvNo.Checked)
         {
             string query = "select distinct invoiceno from T_InvoiceDetails " +
                                   " where jobno = '" + txtJobNo.Text + "'";
             SqlConnection con = new SqlConnection(strImpex);
             SqlCommand cmd = new SqlCommand();
             cmd.CommandType = CommandType.Text;
             cmd.CommandText = query;
             cmd.Connection = con;
             SqlDataAdapter dAdapter = new SqlDataAdapter(cmd);
             DataTable dtConsr = new DataTable();
             dAdapter.Fill(dtConsr);
             string supplier = "";
             foreach (DataRow dtRow in dtConsr.Rows)
             {
                 supplier = supplier + "," + dtRow[0].ToString();
             }
             txtimpRemark.Text = "Supplier Inv No :" + supplier.TrimStart(',');
         }
         else
         {
             txtimpRemark.Text = "";
         }
     }

     protected void txtSubParty_TextChanged(object sender, EventArgs e)
     {
         string SubPartyQuery = "select AccountName,Address1,City,State  from M_AccountMaster where AccountName ='" + txtSubParty.Text + "' and Acc_Group='" + txtCompName.Text + "' ";
         SqlConnection con = new SqlConnection(strImpex);
         con.Open();
         SqlDataAdapter da5 = new SqlDataAdapter(SubPartyQuery, con);
         DataSet ds5 = new DataSet();
         da5.Fill(ds5, "SubParty");
         if (ds5.Tables["SubParty"].Rows.Count != 0)
         {
             DataRowView row = ds5.Tables["SubParty"].DefaultView[0];
             txtSubParty.Text = row["AccountName"].ToString().Trim();
             txtSubPartyAddr.Text = row["Address1"].ToString().Trim() + "," + row["City"].ToString().Trim() + "," + row["State"].ToString().Trim();
         }
         else
         {
             //txtSubParty.Text = "";
             //txtSubPartyAddr.Text = "";
         }
     }
}