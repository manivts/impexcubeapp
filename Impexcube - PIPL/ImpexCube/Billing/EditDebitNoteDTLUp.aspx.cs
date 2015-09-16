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
using System.IO;
using System.Text;

public partial class EditDebitNoteDTLUp : System.Web.UI.Page 
{
  

    //string strconn = (string)ConfigurationManager.AppSettings["ConnectionImpex"];
    //string strconn1 = (string)ConfigurationManager.AppSettings["ConnectionVisual"];
    //string strconnJSU = (string)ConfigurationManager.AppSettings["ConnectionJobStages"];

    string strImpex = (string)ConfigurationManager.ConnectionStrings["Constr"].ConnectionString;
    private string fJOBNO = "";
    Double fsTotal;
    Double BHamt;
    Double Gross = 0;
    Double GrossTot = 0;
    Double total;
    Double bal;
    Double allTotal;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack == false)
        {
            TextBoxOnBlur();
            string ino = Request.QueryString["invNo"];
            TallyAccountName();

            SearchInvoice(ino);
            if (balance1.Text == "")
                balance1.Text = "0";
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
       
       
        LessAd.Attributes.Add("onblur", "javascript:LessADvance();");
        
    }
    public DataSet GetData(string InT)
    {

        SqlConnection conn = new SqlConnection(strImpex);
        string sqlStatement = "select invoice from M_IEC_Debit where invoiceType='" + InT + "'";
        SqlDataAdapter da = new SqlDataAdapter(sqlStatement, conn);
        DataSet ds = new DataSet();
        da.Fill(ds, "iec");

        return ds;

    }
    protected void Submit_Click(object sender, EventArgs e)
    {
        GetTransaction();
        string invNo = txtInvoiceNo.Text;
        string eBy = (string)Session["USER-NAME"];
        string dates = invDate.Text;
        string[] DT = dates.Split('/');
        dates = DT[2] + "-" + DT[1] + "-" + DT[0];

        string EntryDate = DateTime.Now.ToString();
        string notes = txtNote.Text;

        string suffix = txtSuffix.Text;
        SqlConnection conn = new SqlConnection(strImpex);

        string impRK = txtimpRemark.Text;
        string intRK = txtIndentRemark.Text;
        impRK = impRK.Replace("'", " ");
        intRK = intRK.Replace("'", " ");
        string InvSeqNo = txtInvSeqNo.Text;
        string sqlQuery = " Update M_IEC_Debit set suffix='" + suffix + "',compName='" + txtCompName.Text + "',pincode='" + Session["pin"] + " '," +
                          " Address1='" + txtAdd1.Text + "',Address2='" + Session["state"] + "',City='" + txtCity.Text + "',phone='" + Session["Phone"] + "'," +
                          " partyReff='" + txtParty_Reff.Text + "',jobNo='" + txtJobNo.Text + "',BLNo='" + txtBLNo.Text + "',BENoDate='" + txtBENo.Text + " dt." + txtBEDate.Text + "'," +
                          " importItem='" + txtImpotItem.Text + "',notes='" + notes + "',Quantity='" + txtQty.Text + "',Ass_value='" + txtAssValue.Text + "',Container_no='" + txtNCNTR.Text  + "'," +
                          " Custom_duty='" + txtCustomDuty.Text + "', subTotal='" + SubTotal.Text + "',grand_total='" + SubTotal.Text + "', " +
                          " less_advance='" + LessAd.Text + "',Net_total='" + BalanceDue.Text + "',sub_party='" + txtSubParty.Text + "',Nettotal_words='" + hdnRuppees.Value + "'," +
                          "impRemark='" + impRK + "',interRemark='" + intRK + "',TallyAccountName='" + ddlTallyAccountName.SelectedItem.Text + "',TallySubPartyName='" + ddlTallySubPartyName.SelectedItem.Text + "',InvSeqNo='" + InvSeqNo + "',SubPartyAddr='" + txtSubPartyAddr.Text + "' ,ModifiedBy='" + eBy + "' ,ModifiedDate='" + DateTime.Now + "' where invoice='" + invNo + "'";  
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
            }
             Response.Write("<script>" + "alert('Debit Note has successfully Updated');" + "</script>");
           
             Submit.Visible = false;
             balance1.Visible = false;
        }
        catch (Exception ex)
        {
            lblResult.Text = ex.Message;
            lblResult.Visible = true;
        }

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


    protected void SearchInvoice(string INo)
    {
       
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
            string sqlQuery = "select * from M_IEC_Debit where invoice='" + INo + "' ";
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
                txtCompName.Text = "";
                txtCustomDuty.Text = "";
                txtImpotItem.Text = "";
               
                txtJobNo.Text = "";
                txtParty_Reff.Text = "";
            
                txtQty.Text = "";
                txtRupees.Text = "";
                txtSubParty.Text = "";
                txtNCNTR.Text = "";
                SubTotal.Text = "0.00";
                
                LessAd.Text = "0.00";
                BalanceDue.Text = "0.00";

                Response.Write("<script>alert('Please Give the Correct Debit Note Number ')</script>");

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
                catch (Exception ex)
                {
                }

                string Imp_item = row["importitem"].ToString();
                string Qty = row["Quantity"].ToString();
                string AssValue = row["ass_value"].ToString();
                string ContainerNo = row["Container_no"].ToString();
                string CustomsDuty = row["Custom_duty"].ToString();
                string Mode = row["Mode"].ToString();
                string impRK = row["impRemark"].ToString();
                string intRK = row["interRemark"].ToString();

                txtInvSeqNo.Text = row["InvSeqNo"].ToString();

                DateTime iDT = Convert.ToDateTime(InDate);
                invDate.Text = iDT.ToString("dd/MM/yyyy");


                string suffix = row["suffix"].ToString();
                string notes = row["notes"].ToString();

                string Rupees = row["NetTotal_words"].ToString();

                string ats = row["SubTotal"].ToString();
                string Less_Advance = row["less_advance"].ToString();
                string net_total = row["net_total"].ToString();

                ddlTallyAccountName.SelectedItem.Text = row["TallyAccountName"].ToString();
                ddlTallySubPartyName.SelectedItem.Text = row["TallySubPartyName"].ToString();

                if (Mode == "IMP")
                {
                    lblInvoice.Text = "DEBIT NOTE - IMPORTS";
                    lblinvNumber.Text = "DEBIT NO.:";
                    Label16.Text = "BE NO./DT.";
                    Label19.Text = "Ass. Value";
                }
                else
                {
                    Label17.Text = "Item Exported";
                    lblInvoice.Text = "DEBIT NOTE - EXPORTS";
                    lblinvNumber.Text = "DEBIT NO.:";
                    Label16.Text = "SB NO./DT.";
                    Label19.Text = "FOB Value";
                }
                Mode = "";
                //Assin Value to the data field

                txtNote.Text = notes;
                txtSuffix.Text = suffix;
                txtRupees.Text = Rupees;
                
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
                SubTotal.Text = ats;

                txtimpRemark.Text = impRK;
                txtIndentRemark.Text = intRK;

                LessAd.Text = Less_Advance;
                BalanceDue.Text = net_total;
                balance1.Text = net_total;
                if (net_total == "")
                    net_total = "0";
                txtRupees.Text = RsConvert.rupees(Convert.ToInt64(net_total));
                invoiceDTL(INo);
            }
        }
    }
    public DataSet GetDataINVDETL(string ino)
    {

        SqlConnection conn = new SqlConnection(strImpex);
        string sqlStatement = "select * from T_IEC_debit_dtl where invoice='" + ino + "' order by sno";
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
            string Query = "insert into T_IEC_debit_dtl(sno,invoice,charge_desc,amount) values(1,'" + invNO +"','AAI',0)";
            GetCommand(Query);
            GridView1.DataSource = GetDataINVDETL(invNO);
            GridView1.DataBind();
        }
        else
        {
            GridView1.DataSource = GetDataINVDETL(invNO);
            GridView1.DataBind();
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
        Session["BILLTYPE"] = "DB";
        Session["INVOICECTR"] = sno;

        string strQuery = "select * from M_iec_debit where invoice='" + txtInvoiceNo.Text + "' and contr_code is null and particular1 is not null";
        SqlConnection conn = new SqlConnection(strImpex);

        SqlDataAdapter da = new SqlDataAdapter(strQuery, conn);
        DataSet ds = new DataSet();
        da.Fill(ds, "table");
        if (ds.Tables["table"].Rows.Count == 0)
            ScriptManager.RegisterStartupScript(this, this.GetType(), "popup", "window.open('../Billing/CryInvoiceReportCTR.aspx','_blank','menubar=no, scrollbars=yes, toolbar=no, location=no, resizable=yes, left=20, top=20');", true);
           
        else
            ScriptManager.RegisterStartupScript(this, this.GetType(), "popup", "window.open('CryInvoiceReport.aspx','_blank','menubar=no, scrollbars=yes, toolbar=no, location=no, resizable=yes, left=20, top=20');", true);

           
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
            if (amt.Text == "")
                amt.Text = "0.00";
            else
            {
                Double amts = Convert.ToDouble(amt.Text);
                amt.Text = amts.ToString("#0.00#");
            }
            
            Gross = Gross + Convert.ToDouble(amt.Text);
        }
        SubTotal.Text = Gross.ToString("#0.00#");
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
          
            CheckBox chk = (CheckBox)row.FindControl("chkSTAX");
            if (txt.Text == "")
                txt.Text = "0";
            
                Double amt0 = Convert.ToDouble(txt.Text);
               
                GrossTot = GrossTot + amt0;
                
        }
        foreach (GridViewRow Row in GridView1.NewRows)
        {
            TextBox txt = (TextBox)Row.FindControl("amt1");
           
            CheckBox chk = (CheckBox)Row.FindControl("chkSTAX");
            if (txt.Text == "")
                txt.Text = "0";
            
                Double amt0 = Convert.ToDouble(txt.Text);
                txt.Text = amt0.ToString("#0.00#");
                total = total + amt0;
                
        }

        SubTotal.Text = total.ToString("#0.00#");
       
        Double gTotals = total + GrossTot;
        SubTotal.Text = gTotals.ToString("#0.00#");
        
        GetPERCENT();

    }
    protected void GetPERCENT()
    {
        string BILL = "DB";
        Gross = Convert.ToDouble(SubTotal.Text);
        if (LessAd.Text == "")
            LessAd.Text = "0";
        if (BILL == "SB")
        {

            Double NetAmt = Gross;
            SubTotal.Text = NetAmt.ToString("#0.00#");
            bal = NetAmt - Convert.ToDouble(LessAd.Text);


        }
        else
        {
            Double NetAmt = Gross;
            SubTotal.Text = NetAmt.ToString("#0.00#");
            bal = Convert.ToDouble(SubTotal.Text) - Convert.ToDouble(LessAd.Text);

        }
        Double balanceAmount = Math.Round(bal);

        balance1.Text = balanceAmount.ToString();
        BalanceDue.Text = balanceAmount.ToString("#0.00#");

        txtRupees.Text = RsConvert.rupees(Convert.ToInt64(balance1.Text));
       

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

            
            string desc = txtCharge.Text;
            
            string recpt = txtRecpt.Text;
            string amt = txtAmt.Text;
            if (desc == "")
                amt = "";
            

            if (desc != "" && amt != "0.00" || amt != string.Empty)
            {
                string Query = "update T_iec_debit_DTL set charge_desc='" + desc + "'," +
                    "narration='" + recpt + "',sno='" + lsno.Text  + "',amount='" + amt + "' " +
                    "where invoice='" + invNO + "' and sno='" + lsno.Text + "'";
                GetCommand(Query);
                sno = sno + 1;
            }
            else
            {
                string Query = "delete from T_iec_debit_DTL where invoice='" + invNO + "' and sno='" + lsno.Text + "'";
                GetCommand(Query);
                sno = sno + 1;
            }



        }
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

         

            string desc = txtCharge.Text;
           
            string recpt = txtRecpt.Text;
            string amt = txtAmt.Text;

           
            if (amt == "")
                amt = "0.00";
            if (desc != "" && amt != "0.00")
            {

                oldRow = oldRow + 1;

                string Query = "insert into T_iec_debit_DTL(invoice,sno,charge_desc,narration,amount) " +
                               "values('" + invNO + "'," + oldRow + ",'" + desc + "','" + recpt + "'," + amt + ")";
                GetCommand(Query);

            }


        }
    }
    protected void GetCommand(string sqlQuery)
    {
        try
        {
            SqlConnection conn = new SqlConnection(strImpex);
            conn.Open();
            SqlCommand cmd = new SqlCommand(sqlQuery, conn);
            SqlDataAdapter da = new SqlDataAdapter();
            cmd.CommandText = sqlQuery;
            cmd.Connection = conn;
            da.SelectCommand = cmd;


            int result = cmd.ExecuteNonQuery();
            conn.Close();
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('" + ex.Message + "');", true);
        }
    }

    protected void LessAd_TextChanged(object sender, EventArgs e)
    {
        txtRupees.Text = RsConvert.rupees(Convert.ToInt64(balance1.Text));
        Submit.Focus();
    }
    protected void preview_Click(object sender, EventArgs e)
    {
       //Button1_Click(sender, e);
        Session["InvNo"] = txtInvoiceNo.Text;
        string rep = (string)Session["InvNo"];
        string sub = rep.Substring(4, 2);
        if (sub == "SB")
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "popup", "window.open('../frmImpInvoiceReport.aspx','_blank','width=1120,height=720, menubar=no, scrollbars=yes, toolbar=no, location=no, resizable=no, left=10, top=120');", true);
       // Response.Redirect("../frmImpInvoiceReport.aspx");
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "popup", "window.open('../frmDebit.aspx','_blank','width=1120,height=720, menubar=no, scrollbars=yes, toolbar=no, location=no, resizable=no, left=10, top=120');", true);
       // Response.Redirect("../frmDebit.aspx");
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
        Session["BILLTYPE"] = "DB";
        Session["INVOICECTR"] = sno;
        if (Session["Maill"] == null)
        {
            Session["MAILBUTTON"] = "OK";
            Session["PageName"] = "EditDebitNoteDTLUp.aspx";
            Session["Maill"] = "SendMaill";
          
            ScriptManager.RegisterStartupScript(this, this.GetType(), "popup", "window.open('CryInvoiceReportSTax.aspx','_blank','menubar=no, scrollbars=no, toolbar=no, location=no, resizable=no,height=650,width=700, left=20, top=20');", true);

        }
   
    }
    protected void BtnExit_Click(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(this, this.GetType(), "popup", "window.close();", true);

    }


    protected void BtnExit_Click1(object sender, EventArgs e)
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





        sqlQuery = "select * from M_iec_debit i where i.jobno='" + jno + "' ";





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
        
            string iTYPE = "DB";
            string sqlQueryM = "";
            //Master Records
            SqlConnection connM = new SqlConnection(strImpex);

            strMST = "M_iec_debit";
            strDTL = "T_iec_debit_dtl";

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
                     "select * from iec_debit j where j.jobno='" + jno + "' and j.compName ='" + txtCompName.Text + "' order by invoiceDate";



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
                //Master Records
                SqlConnection connM = new SqlConnection(strImpex);
                if (iTYPE == "ATLSB")
                {
                    strMST = "M_iec_invoicenew";
                    strDTL = "T_iec_invoicenew_dtl";
                }
                else
                {
                    strMST = "iec_debit";
                    strDTL = "iec_debit_dtl";
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
                    invNO = rowM["INVOICENO"].ToString();
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

                    //Naration fields
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
                        InvoiceType = "sales";
                        tw.Write(InvoiceType + ","); tw.Write(invNO + ","); tw.Write(dates + ","); tw.Write(refer + ",");
                        tw.Write(pName + ","); tw.Write(","); tw.Write(refer + ","); tw.Write(allTotal + ","); tw.Write(Naration + ","); tw.Write("\n");

                    }
                    //TRANSACTIONS 


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