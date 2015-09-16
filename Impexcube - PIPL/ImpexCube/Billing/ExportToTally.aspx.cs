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
using System.IO;
using System.Text;
using System.Diagnostics;
using Excel = Microsoft.Office.Interop.Excel;
public partial class ExportToTally : System.Web.UI.Page
{
    string strconn = (string)ConfigurationManager.ConnectionStrings["Constr"].ConnectionString;
    //string strconn = (string)ConfigurationManager.AppSettings["ConnectionImpex"];
    private string iJOBNO = "";
    private string fJOBNO = "";
    
    Double Total;
    Double nTotal;
    Double fsTotal;
    Double sTotal;
    Double sT=0;
    Double totVal;
    Double BHamt;
    Double allTotal;
    string JNO;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack == false)
        {
           
            if (chkJNO.Checked == true)
                txtJNO.Enabled = true;
            else
                txtJNO.Enabled = false;
            string fy = (string)Session["FinancialYear"];
            txtPName.Enabled = false;
            getSDate(fy);
           
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
            txtFrom.Text = sDATES.ToString("MM/dd/yyyy");
            txtTo.Text = System.DateTime.Now.ToString("dd/MM/yyyy");
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('" + ex.Message + "');", true);
        }
    }
    protected void BtnSearch_Click(object sender, EventArgs e)
    {
        //// to get value from File stream IO function
        string pName = "";
        if (chkJNO.Checked)
        {
            string jno = txtJNO.Text;
            string Query = "select * from M_iec_invoicenew where jobno='" + jno + "'";
            DataSet ds = GetData(Query);
            DataTable dt = ds.Tables[0];
            foreach (DataRow row in dt.Rows)
            {
                pName = row["compName"].ToString();
                
            }
        }
        else
            pName = txtPName.Text;

        if (pName == "APOLLO TYRES LIMITED")
            GetAPOLLO();
        else
            GetFSIO();
    }

   

    protected void GetFSIO()
    {
        string fDate = txtFrom.Text;
        string tDate = txtTo.Text;
        string BType = drBill.SelectedValue;
        string jno = txtJNO.Text;

        string sMM = fDate.Substring(3, 2);
        string sDD = fDate.Substring(0, 2);
        string sYY = fDate.Substring(6, 4);
        fDate = sMM + "/" + sDD + "/" + sYY;


        string eMM = tDate.Substring(3, 2);
        string eDD = tDate.Substring(0, 2);
        string eYY = tDate.Substring(6, 4);
        tDate = eMM + "/" + eDD + "/" + eYY;

        string sqlQuerySTR = "";
        string sqlQuery = "";
       
        string strMST="";
        string strDTL="";

        string invNO = "";
        string Party = "";
        string dates = "";
        string InvoiceType="";
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

        DateTime fd = Convert.ToDateTime(fDate);
        DateTime td = Convert.ToDateTime(tDate);
        DataTable dt = new DataTable();

        string FD = fd.ToString("yyyy-MM-dd");
        string TD = td.ToString("yyyy-MM-dd");
        if (tDate == "")
            tDate = fDate;
      

        if (BType == "0")
        {
            if (chkJNO.Checked == false && chkIMP.Checked == true)
            {
                sqlQuery = "select * from M_iec_invoicenew i where i.invoiceDate between '" + FD + "' and '" + TD + "' " +
                         "and  i.compName ='" + txtPName.Text + "'  union " +
                         "select * from M_iec_debit j where j.invoiceDate between '" + FD + "' and '" + TD + "' " +
                         "and j.compName ='" + txtPName.Text + "' order by invoiceDate ";
            }
            else if (chkJNO.Checked == true && chkIMP.Checked == false)
            {
                sqlQuery = "select * from M_iec_invoicenew i where i.jobno='" + jno + "' union " +
                        "select * from M_iec_debit j where j.jobno='" + jno + "' order by invoiceDate";
            }
            else if (chkJNO.Checked == true && chkIMP.Checked == true)
            {
                sqlQuery = "select * from M_iec_invoicenew i where i.jobno='" + jno + "'  and i.compName ='" + txtPName.Text + "' union " +
                        "select * from M_iec_debit j where j.jobno='" + jno + "' and j.compName ='" + txtPName.Text + "' order by invoiceDate";
            }
           
        }
        else
        {

            if (BType == "SB" || BType == "ATLSB" || BType == "EXPSB" || BType == "CD")
            {
                strMST = "M_iec_invoicenew";
                strDTL = "T_iec_invoicenew_dtl";
            }
            else if (BType == "DB" || BType == "ATLDB" || BType == "EXPDB")
            {
                strMST = "M_iec_debit";
                strDTL = "T_iec_debit_dtl";
            }
                if (chkJNO.Checked == false && chkIMP.Checked == false)
                    sqlQuery = "select * from " + strMST + " where invoiceDate between '" + FD + "' and '" + TD + "' and invoiceType='" + BType + "'  order by invoice";
                else if (chkJNO.Checked == true && chkIMP.Checked == false)
                    sqlQuery = "select * from " + strMST + " where jobNo='" + jno + "'";
                else if (chkJNO.Checked == false && chkIMP.Checked == true)
                    sqlQuery = "select * from " + strMST + " where invoiceDate between '" + FD + "' and '" + TD + "' and compName='" + txtPName.Text + "' and invoiceType='" + BType + "' order by invoice";
                else if (chkJNO.Checked == true && chkIMP.Checked == true)
                    sqlQuery = "select * from " + strMST + " where jobNo='" + jno + "' and compName='" + txtPName.Text + "'";
            
        }
            SqlConnection conn = new SqlConnection(strconn);
            SqlDataAdapter da = new SqlDataAdapter(sqlQuery, conn);
            DataSet ds = new DataSet();

            if (sqlQuery != "")
            {
                da.Fill(ds, "INVOICES");
                dt = ds.Tables[0];

                if (BType == "0")
                    billtype = "Billing";
                else if (BType == "SB" || BType == "ATLSB" || BType == "EXPSB")
                    billtype = "SalesBill";
                else
                    billtype = "DebitNote";

                datetime = DateTime.Now.Day.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Year.ToString() + "-" + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() +  DateTime.Now.Second.ToString();
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

                foreach (DataRow row in dt.Rows)
                {
                    jNo = row["invoice"].ToString();
                  
                    string iTYPE = row["invoiceType"].ToString();
                    string sqlQueryM = "";
                    //Master Records
                    SqlConnection connM = new SqlConnection(strconn);
                    if (iTYPE == "SB" || iTYPE == "ATLSB" || iTYPE == "EXPSB")
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
                        invNO = rowM["INVOICENo"].ToString();
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
                        {
                            if (pn.Length > 1)
                            {
                                Party = pn[0] + pn[1];
                            }
                            else
                            {
                                Party = pn[0];
                            }
                        }
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
                    SqlConnection connSTR = new SqlConnection(strconn);
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
                            string amtRS = rowSTR["amount"].ToString();
                            if (amtRS == "")
                                amtRS = "0";
                            amt = Convert.ToDouble(amtRS);
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
                                        tw.Write("Agency charges" + ","); tw.Write(refer + ","); tw.Write(BHamt + ","); tw.Write(Naration + ","); tw.Write("\n");
                                      
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
                                        tw.Write("Agency charges" + ","); tw.Write(refer + ","); tw.Write(BHamt + ","); tw.Write(Naration + ","); tw.Write("\n");
                                       
                                    }
                                }
                                else
                                {
                                    if (i == length)
                                    {
                                        tw.Write(InvoiceType + ","); tw.Write(invNO + ","); tw.Write(dates + ","); tw.Write(refer + ",");

                                        tw.Write("Agency charges" + ","); tw.Write(pName + ","); tw.Write(refer + ","); tw.Write(BHamt + ","); tw.Write(Naration + ","); tw.Write("\n");
                                        
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

                                        tw.Write(desc + ","); tw.Write(","); tw.Write(refer  + ","); tw.Write(amt + ","); tw.Write(Naration + ","); tw.Write("\n");
                                        fJOBNO = ino;
                                    }
                                    else
                                    {

                                        tw.Write(InvoiceType + ","); tw.Write(invNO + ","); tw.Write(dates + ","); tw.Write(refer + ",");

                                        tw.Write(desc + ","); tw.Write(","); tw.Write(refer  + ","); tw.Write(amt + ","); tw.Write(Naration + ","); tw.Write("\n");
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

                }
                tw.Flush();
                tw.Close();

                string sGenName = file;
                string sFileName = file;
                string fdATE = DateTime.Now.Day.ToString() + "." + DateTime.Now.Month.ToString() + "." + DateTime.Now.Year.ToString();

              

                System.IO.FileStream fss = null;

                fss = System.IO.File.Open(Server.MapPath("~/CSV/Tally/" + fdATE + "/" +
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
        string fDate = txtFrom.Text;
        string tDate = txtTo.Text;
        string BType = drBill.SelectedValue;
        string jno = txtJNO.Text;

        string sMM = fDate.Substring(3, 2);
        string sDD = fDate.Substring(0, 2);
        string sYY = fDate.Substring(6, 4);
        fDate = sMM + "/" + sDD + "/" + sYY;


        string eMM = tDate.Substring(3, 2);
        string eDD = tDate.Substring(0, 2);
        string eYY = tDate.Substring(6, 4);
        tDate = eMM + "/" + eDD + "/" + eYY;

       

        string file = string.Empty;
        string billtype = "";
        string datetime = "";
        string serverPath = "";
        string genFile = "";
        string dATE = "";
        string sqlQuery = "";
        DateTime fd = Convert.ToDateTime(fDate);
        DateTime td = Convert.ToDateTime(tDate);
        DataTable dt = new DataTable();

        string FD = fd.ToString("yyyy-MM-dd");
        string TD = td.ToString("yyyy-MM-dd");
        if (tDate == "")
            tDate = fDate;
       
        
            if (chkJNO.Checked == false && chkIMP.Checked == true)
            {
                sqlQuery = "select * from M_iec_invoicenew i where i.invoiceDate between '" + FD + "' and '" + TD + "' " +
                         "and  i.compName ='" + txtPName.Text + "'  union " +
                         "select * from M_iec_debit j where j.invoiceDate between '" + FD + "' and '" + TD + "' " +
                         "and j.compName ='" + txtPName.Text + "' order by i.invoiceDate,i.jobno ";
            }
            else if (chkJNO.Checked == true && chkIMP.Checked == true)
            {
                sqlQuery = "select * from M_iec_invoicenew i where i.jobno='" + jno + "'  and i.compName ='" + txtPName.Text + "' union " +
                        "select * from M_iec_debit j where j.jobno='" + jno + "' and j.compName ='" + txtPName.Text + "' order by invoiceDate";
            }
            else if (chkJNO.Checked == true && chkIMP.Checked == false)
            {
                sqlQuery = "select * from M_iec_invoicenew i where i.jobno='" + jno + "' union " +
                        "select * from M_iec_debit j where j.jobno='" + jno + "' order by invoiceDate";
            }
       
       
        SqlConnection conn = new SqlConnection(strconn);
        SqlDataAdapter da = new SqlDataAdapter(sqlQuery, conn);
        DataSet ds = new DataSet();

        if (sqlQuery != "")
        {
            da.Fill(ds, "INVOICES");
            dt = ds.Tables[0];

            if (BType == "0")
                billtype = "Billing";
            else if (BType == "SB" )
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

           
            tw.Write("Voucher Type" + ","); tw.Write("Invoice No" + ","); tw.Write("Date" + ","); tw.Write("Ref" + ","); tw.Write("Dr Account" + ","); tw.Write("Cr.Account" + ",");

          
            tw.Write("Cost Center" + ","); tw.Write("Amount" + ","); tw.Write("Narration\n");
            
           
            
                if (chkJNO.Checked == false && chkIMP.Checked == true)
                {
                    string strQuery = "select distinct jobno from M_iec_invoicenew where invoiceDate between '" + FD + "' and '" + TD + "' " +
                                 "and  compName ='" + txtPName.Text + "'  ";
                    DataSet dsJNO = GetData(strQuery);
                    DataTable dtJNO = dsJNO.Tables[0];

                    foreach (DataRow rowJNO in dtJNO.Rows)
                    {
                       string ijobno = rowJNO["jobno"].ToString();
                       string Query = "select * from M_iec_invoicenew i where i.jobno='" + ijobno + "' union " +
                                      "select * from M_iec_debit j where j.jobno='" + ijobno + "' order by invoiceDate";
                       DataSet dsJ = GetData(Query);
                       DataTable dtJ = dsJ.Tables[0];
                       GetExportTally(dtJ,tw);
                    }
                }
                else
                    GetExportTally(dt,tw);
           
              
            
            
            tw.Flush();
            tw.Close();
            //chnims120751158
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
    protected void GetExportTally(DataTable dt,TextWriter tw)
    {
        string pName = "";
        Double amt = 0;
        string jnos = "";
        Double STAX = 0;
        string iTYPE = "";
        string blno = "";
        string beNo = "";
        string impItem = "";
        string qty = "";
        string pRef = "";
        string serTAX = "";
        int i = 0;
        int length = 0;
        string jjNOS = "";
        Double agCharges = 0;

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
        string desc = "";
        string ino = "";
        foreach (DataRow row in dt.Rows)
        {
            jNo = row["invoice"].ToString();
           
            iTYPE = row["invoiceType"].ToString();
            jobNO = row["jobno"].ToString();
            string sqlQueryM = "";
           
            //Master Records
            SqlConnection connM = new SqlConnection(strconn);
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
                    invNO = rowM["INVOICENo"].ToString();
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

                //Start CSV Header Text
               

                string[] pn = Party.Split(' ');
                Party = pn[0];
                if (pn[0].Length < 3)
                    Party = pn[0] + pn[1];

            }


            //Transaction Records 
            SqlConnection connSTR = new SqlConnection(strconn);
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


                     ino = rowSTR["invoice"].ToString();
                     desc = rowSTR["charge_desc"].ToString();
                    desc = desc.Replace(",", " ");
                    BHamt = BHamt + amt;

                   
                    i = i + 1;
                }



            }

        }


        if (pName == "APOLLO TYRES LIMITED")
        {
            refer = jnos + " / " + Party;
            Naration = "JOBNO:" + jnos + "/" + blno + "/" + beNo + "/ " + impItem + "/ " + qty + "/ " + pRef;
            
            // END CSV Header Text

            if (iTYPE == "ATLSB" || iTYPE == "ATLDB" || iTYPE == "ATLDEM")
            {
                InvoiceType = "ATL sales";
                tw.Write(InvoiceType + ","); tw.Write(invNO + ","); tw.Write(dates + ","); tw.Write(refer + ",");
                tw.Write(pName + ","); tw.Write(","); tw.Write(refer + ","); tw.Write(allTotal + ","); tw.Write(Naration + ","); tw.Write("\n");

                //Charge Detals

                tw.Write(InvoiceType + ","); tw.Write(invNO + ","); tw.Write(dates + ","); tw.Write(refer + ",");
                tw.Write(",");
                tw.Write("Agency charges" + ","); tw.Write(refer + ","); tw.Write(BHamt + ","); tw.Write(Naration + ","); tw.Write("\n");

                //Service Tax Details
                tw.Write(InvoiceType + ","); tw.Write(invNO + ","); tw.Write(dates + ","); tw.Write(refer + ",");
                tw.Write(",");
                tw.Write("Service Tax" + ","); tw.Write(refer + ","); tw.Write(STAX + ","); tw.Write(Naration + ","); tw.Write("\n");
                fsTotal = fsTotal + STAX;
            }
 

            fsTotal = 0;
            STAX = 0;
            allTotal = 0;
            BHamt = 0;

        }
    }
    protected void GetGridFunction()
    {
        string fDate = txtFrom.Text;
        string tDate = txtTo.Text;
        string BType = drBill.SelectedValue;
        string jno = txtJNO.Text;

        string sqlQuery = "";

        if (tDate == "")
            tDate = fDate;
        if (chkJNO.Checked == false)
        {

            if (BType == "0")
                Response.Write("<script>alert('Please Select Bill Type')</script>");
            else if (BType == "SB")
                sqlQuery = "select * from M_iec_invoicenew m,T_iec_invoicenew_dtl s where m.invoice=s.invoice and  m.invoiceDate between '" + fDate + "' and '" + tDate + "' order by m.invoice";
            else
                sqlQuery = "select * from M_iec_debit m,T_iec_debit_dtl s where m.invoice=s.invoice and  m.invoiceDate between '" + fDate + "' and '" + tDate + "' order by m.invoice";

        }
        else
        {
            if (jno == "" || jno == string.Empty)
                Response.Write("<script>alert('Please Give Correct Job No')</script>");
            else
            {
                if (BType == "0")
                    Response.Write("<script>alert('Please Select Bill Type')</script>");
                else if (BType == "SB")
                    sqlQuery = "select * from M_iec_invoicenew m,T_iec_invoicenew_dtl s where m.invoice=s.invoice and  m.jobNo='" + jno + "' ";
                else
                    sqlQuery = "select * from M_iec_debit m,T_iec_debit_dtl s where m.invoice=s.invoice and  m.jobNo='" + jno + "'";
            }
        }
        if (sqlQuery != "")
        {
            Grdiworkreg.Visible = true;
            Grdiworkreg.DataSource = GetData(sqlQuery);
            Grdiworkreg.DataBind();
        }
    }
    public DataSet GetData(string Query)
    {
        
            SqlConnection conn = new SqlConnection(strconn);
            SqlDataAdapter da = new SqlDataAdapter(Query, conn);
            DataSet ds = new DataSet();
            da.Fill(ds, "bill");
            return ds;
            
        
    }

    protected void Grdiworkreg_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        string bill = drBill.SelectedValue;
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
        
            string iNO = Grdiworkreg.DataKeys[e.Row.RowIndex].Values[0].ToString();
            string invNO = e.Row.Cells[3].Text;
            
            if (invNO != iJOBNO)
            {
                if (bill == "SB")
                {
                    GetServiceTax(iNO);
                }
                if (e.Row.Cells[0].Text != "")
                {
                    DateTime iDate = Convert.ToDateTime(e.Row.Cells[0].Text);
                    e.Row.Cells[0].Text = iDate.ToString("dd.MM.yyyy");
                }
                Double amt = Convert.ToDouble(e.Row.Cells[4].Text);
                e.Row.Cells[4].Text = amt.ToString("#0.00");
                totVal = amt;
                Double amt1 = Convert.ToDouble(e.Row.Cells[6].Text);
                e.Row.Cells[6].Text = amt1.ToString("#0.00");
                Double  isTotal =Convert.ToDouble(e.Row.Cells[6].Text);
                sTotal = sTotal + isTotal;
                string jno = e.Row.Cells[1].Text;
                string pName = e.Row.Cells[2].Text;
                string narr = e.Row.Cells[7].Text;
                narr = narr.TrimEnd('/');

                jno = jno.Substring(4, 5);
                pName = pName.Substring(0, 5);
                e.Row.Cells[1].Text = jno + "/" + pName;
                iJOBNO = invNO;
            }
            else
            {

                e.Row.Cells[0].Text = "";
                e.Row.Cells[1].Text = "";
                e.Row.Cells[2].Text = "";
                e.Row.Cells[3].Text = "";
                e.Row.Cells[4].Text = "";
                e.Row.Cells[7].Text = "";
                Double amt1 = Convert.ToDouble(e.Row.Cells[6].Text);
                e.Row.Cells[6].Text = amt1.ToString("#0.00");
               Double iTotal=Convert.ToDouble(e.Row.Cells[6].Text);
               Total = Total + iTotal;

               
               
            }
            nTotal = Total + sTotal;
        }
        if (e.Row.RowType == DataControlRowType.Footer)
        {
          
            if (bill == "SB")
            {
                GridViewRow row = new GridViewRow(-1, -1, DataControlRowType.DataRow, DataControlRowState.Normal);
                //Add the two Columns
                row.Cells.AddRange(CreateCells());
                //get a reference to the table that holds this row
                Table tbl = (e.Row.Parent as Table);
                //Add the row at the end of the list, but before the footer.
                tbl.Rows.AddAt(Grdiworkreg.Rows.Count + 1, row);
            }
                //Don't forget to account for any changes in the footer. Since we added a row to show the tax,
                //that tax must also be accounted for in our footer. Calculating the orderTotal and the tax
                //is an exercise for the reader.
                Label lbl;
                lbl = (Label)e.Row.FindControl("lblTotal");
             
            
            Double NETTot = nTotal + sT;
            e.Row.Cells[6].Text = NETTot.ToString("#0.00");

            e.Row.Cells[4].Text = totVal.ToString("#0.00");

            e.Row.Cells[4].HorizontalAlign = HorizontalAlign.Right;

            e.Row.Cells[6].HorizontalAlign = HorizontalAlign.Right;
            e.Row.Cells[3].Text = "TOTAL";
            e.Row.Cells[5].Text = "TOTAL";
        }
    }
    protected void GetServiceTax(string jNO)
    {
        try
        {
            SqlConnection conn = new SqlConnection(strconn);
            string Query = "select * from M_iec_invoiceNew where invoice='" + jNO + "'";
            SqlDataAdapter da = new SqlDataAdapter(Query, conn);
            DataSet ds = new DataSet();
            da.Fill(ds, "inv");
            if (ds.Tables["inv"].Rows.Count != 0)
            {
                DataRowView row = ds.Tables["inv"].DefaultView[0];
                string sTax = row["service_tax"].ToString();
                string eCess = row["edu_cess"].ToString();
                string shCess = row["sec_chess"].ToString();
                if (eCess == "")
                    eCess = "0";
                if (shCess == "")
                    shCess = "0";
                Double ServiceTax = Convert.ToDouble(sTax) + Convert.ToDouble(eCess) + Convert.ToDouble(shCess);
            
                Session["SERVICETAX"] = ServiceTax;
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('" + ex.Message + "');", true);
        }

    }
    private TableCell[] CreateCells()
    {

        TableCell[] cells = new TableCell[8];
        TableCell cell;
        Label lbl;

        //The new item column
        cell = new TableCell();
        lbl = new Label();
        lbl.Text = "";
        cell.Controls.Add(lbl);
        cells[0] = cell;

        //The 2 item column
        cell = new TableCell();
        lbl = new Label();
        lbl.Text = "";
        cell.Controls.Add(lbl);
        cells[1] = cell;

        //The 3 item column
        cell = new TableCell();
        lbl = new Label();
        lbl.Text = "";
        cell.Controls.Add(lbl);
        cells[2] = cell;

        //The 4 item column
        cell = new TableCell();
        lbl = new Label();
        lbl.Text = "";
        cell.Controls.Add(lbl);
        cells[3] = cell;

        //The 5item column
        cell = new TableCell();
        lbl = new Label();
        lbl.Text = "";
        cell.Controls.Add(lbl);
        cells[4] = cell;

        //The order item column
        cell = new TableCell();
        lbl = new Label();
        lbl.Text = "Service Tax";
        cell.Controls.Add(lbl);
        cells[5] = cell;

        //The price column
        cell = new TableCell();
        lbl = new Label();
        lbl.Font.Bold = false;
        
        
        sT = (Double)Session["SERVICETAX"];
       lbl.Text = sT.ToString("#0.00");
       
        cell.HorizontalAlign = HorizontalAlign.Right;
        cell.Controls.Add(lbl);
        cells[6] = cell;

       //The 5item column
        cell = new TableCell();
        lbl = new Label();
        lbl.Text = "";
        cell.Controls.Add(lbl);
        cells[7] = cell;

        return cells;
    }
    protected void BtnExport_Click(object sender, EventArgs e)
    {
        string sysDates = DateTime.Now.ToString("dd-MMM-yyyy");
        string FileName = "BillRPT" + sysDates;
        string strFileName = FileName + ".xls";
        BtnSearch_Click(sender, e);
        GridViewExportDet.ExportExcell(strFileName, Grdiworkreg);
    }
    protected void drBill_SelectedIndexChanged(object sender, EventArgs e)
    {
        string BT = drBill.SelectedValue;
        Session["BillTY"] = BT;
        Grdiworkreg.Visible = false;
    }
    protected void chkJNO_CheckedChanged(object sender, EventArgs e)
    {
        string BT = drBill.SelectedValue;
        Session["BillTY"] = BT;
        if (chkJNO.Checked == true)
        {
            txtJNO.Enabled = true;
            Session["eParty"] = txtPName.Text;
        }
        else
        {
            Session["eParty"] = txtPName.Text;
            txtJNO.Enabled = false;
            txtJNO.Text = "";
        }
        Grdiworkreg.Visible = false;
    }

    public string GetServerPath(string PartyName)
    {
        string file = string.Empty;
        string datetime = DateTime.Now.Year.ToString() + "-" + DateTime.Now.Month.ToString() + "-" + DateTime.Now.Day.ToString() + "-" + DateTime.Now.Hour.ToString() + "-" + DateTime.Now.Minute.ToString() + "-" + DateTime.Now.Second.ToString();
        string serverPath = Server.MapPath("~") + "\\" + "JSR";

        if (Directory.Exists(serverPath))
        {
            string PartyNameDirectory = serverPath + "\\" + PartyName;
            if (Directory.Exists(PartyNameDirectory))
            {
                file = PartyNameDirectory + "\\" + PartyName + datetime + ".xls";
            }
            else
            {
                Directory.CreateDirectory(PartyNameDirectory);
                file = PartyNameDirectory + "\\" + PartyName + datetime + ".xls";
            }

        }
        else
        {
            Directory.CreateDirectory(serverPath);
            string PartyNameDirectory = serverPath + "\\" + PartyName;
            if (Directory.Exists(PartyNameDirectory))
            {
                file = PartyNameDirectory + "\\" + PartyName + datetime + ".xls"; ;
            }
            else
            {
                Directory.CreateDirectory(PartyNameDirectory);
                file = PartyNameDirectory + "\\" + PartyName + datetime + ".xls"; ;
            }

        }
        return file;
    }
    protected void chkIMP_CheckedChanged(object sender, EventArgs e)
    {
        string BT = drBill.SelectedValue;
        Session["BillTY"] = BT;
        if (chkIMP.Checked == true)
        {
          
            Session["eParty"] = txtPName.Text;
            txtPName.Enabled = true;
            txtPName.Text = "";
        }
        else if (chkIMP.Checked == false)
        {
            Session["eParty"] = ""; ;
            
            txtPName.Enabled = false;
            txtPName.Text = "";
        }
    }


    protected void ExportTally_Click(object sender, EventArgs e)
    {
        System.Diagnostics.Process process1 = new System.Diagnostics.Process();
        try
        {

           
           //VTS
            process1.StartInfo.FileName = "C:\\Users\\windows\\Desktop\\RKS Data\\Release\\ExportMagic.exe";
            //PIPL
           
            process1.Start();
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('" + ex.Message + "');", true);

        }
    }
}
