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

using System.Data.SqlClient;
using System.Windows;
using System.IO;
using System.Text;
public partial class frmMutipleBillingJobUpdate : System.Web.UI.Page
{
   
    //string strconn = (string)ConfigurationManager.AppSettings["ConnectionImpex"];
    //string strconn1 = (string)ConfigurationManager.AppSettings["ConnectionVisual"];
    //string strconnJSU = (string)ConfigurationManager.AppSettings["ConnectionJobStages"];

    string strImpex = (string)ConfigurationManager.ConnectionStrings["Constr"].ConnectionString;
    #region
    private string fJOBNO = "";
    Double fsTotal;
    Double BHamt;
    private string strCName;
    private string InNo;
    private string InCode;
    private string invoice;
    private Int32 InID;
    string CNTRNO = "";
    //private string eXISTbILL;
    DateTime blDate;
    DateTime hblDate;
    int flag = 0;
    int invFlag = 0;
    string fyear = "";
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
    #endregion
    protected void Page_Load(object sender, EventArgs e)
    {

        fyear = (string)Session["FinancialYear"];

        if (IsPostBack == false)
        {
            dvmain.Visible = false;
            string lfyear = (string)Session["Lfyear"];
            Session["FYEARBill"] = fyear;

            chk.Text = lfyear;
            GetXML();
            Submit.Enabled = false;
            btnMail.Enabled = false;
            preview.Enabled = false;
            ExportTally.Enabled = false;

            string strQuery = "select * from M_ServiceMaster where fyear='" + fyear + "' order by fyear desc";
            drServiceTax.DataSource = GetDataSQL(strQuery);
            drServiceTax.DataTextField = "sTax";
            drServiceTax.DataValueField = "serviceTax";
            drServiceTax.DataBind();


            Session["RBMODE"] = "IMP";
            TrAddr.Visible = false;
            TrAddr1.Visible = false;
           
            Session["Invoice"] = "Invoice";

       
           
            Session["IECName"] = "";
            Session["IECAdd1"] = "";
            Session["IECAdd2"] = "";
            Session["IECCity"] = "";
            Session["Pin"] = "";
            Session["Phone"] = "";
         
            try
            {
                
                rbBill.SelectedValue = "DP";
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('" + ex.Message + "');", true);
            }
         
            txtCompName.Text = (string)Session["IECName"];
           
            txtAdd1.Text = (string)Session["IECAdd1"];
           
            txtCity.Text = (string)Session["IECCity"];
           
            string Head = Session["RBMODE"].ToString();
            if (Head == "EXP")
            {
               
            }
            else
            {
                lblIName.Text = "INVOICE - IMPORTS";
                
            }

            string LNA = (string)Session["Invoice"];
            string dates = DateTime.Now.ToString("dd/MM/yyyy");

            invDate.Text = dates;
            if ((string)Session["Invoice"] == "Invoice")
            {
               
                string tp = "SB";
                lblINumber.Text = "INV. NO.:";
                InvoiceGenerated(tp);
            }
            else
            {
              
                lblINumber.Text = "DEBIT NO.:";
               
            }
            if ((string)Session["RBMODE"] == "EXP")
            {
                
            }

            if (Session["page"] != null)
            {
                if ((string)Session["page"] == "popup")
                {
                    FillBasicInformation();
                    FillDesc();
                    GetTransaction();
                    Button1.Visible = true;
                }
            }
        }

    }
    protected void FillBasicInformation()
    {
        if (Session["BasicInformation"] != null)
        {
            string BasicValue = (string)Session["BasicInformation"];
            string[] Values = BasicValue.Split('~');
            txtJNO.Text = Values[0];
            txtCompName.Text = Values[1];
            txtJobNo.Text = Values[2];
            txtSubParty.Text = Values[3];
            txtBLNo.Text = Values[4];
           
            txtBENo.Text = Values[6];
            txtAdd1.Text = Values[7];
            txtImpotItem.Text = Values[8];
            txtCity.Text = Values[9];
            txtQty.Text = Values[10];
          
            txtAssValue.Text = Values[13];
            
            txtNCNTR.Text = Values[15];
            txtParty_Reff.Text = Values[16];
            txtCustomDuty.Text = Values[17];
            txtNote.Text = Values[18];

        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        Session["BasicInformation"] = txtJNO.Text + "~" + txtCompName.Text + "~" + txtJobNo.Text + "~" + txtSubParty.Text + "~" + (string)Session["BLNo"] + "~" + txtAdd1.Text + "~" + (string)Session["BENo"] + "~" + txtAdd1.Text + "~" + txtImpotItem.Text + "~" + txtCity.Text + "~" + txtQty.Text + "~" + (string)Session["state"] + "~" + (string)Session["Pin"] + "~" + txtAssValue.Text + "~" + (string)Session["Phone"] + "~" + txtNCNTR.Text + "~" + txtParty_Reff.Text + "~" + txtCustomDuty.Text + "~" + txtNote.Text;
        Session["CompanyName"] = txtCompName.Text;
        ScriptManager.RegisterStartupScript(this, this.GetType(), "Error", "window.open('PopUp.aspx','_blank','width=600,height=250, menubar=no, scrollbars=yes, toolbar=no, location=no, resizable=yes, left=350, top=200, Right=200=, bottom=200');", true);

    }
    protected void FillDesc()
    {
        if (Session["InvoiceNumber"] != null)
        {
            string InvNumber = (string)Session["InvoiceNumber"];
            DataSet ds1 = new DataSet();

            SqlConnection conn2 = new SqlConnection(strImpex);
            string strQuery = "SELECT * FROM T_iec_invoicenew_dtl where invoice='" + InvNumber + "' order by sno";
            SqlDataAdapter da2 = new SqlDataAdapter(strQuery, conn2);
            da2.Fill(ds1, "Invoice");
            if (ds1.Tables["Invoice"].Rows.Count != 0)
            {
                int i = 0;
                foreach (GridViewRow row2 in GridView1.Rows)
                {
                    TextBox desc = (TextBox)row2.FindControl("txtDetails");
                    TextBox Recpt = (TextBox)row2.FindControl("txtRecpt");
                    CheckBox chkSTAX = (CheckBox)row2.FindControl("chkSTAX");
                    TextBox amt = (TextBox)row2.FindControl("amt1");
                    DataRowView dr = ds1.Tables["Invoice"].DefaultView[i];
                    desc.Text = dr["charge_desc"].ToString();
                    Recpt.Text = dr["receipt"].ToString();
                    amt.Text = dr["amount"].ToString();
                    string serTAX = dr["ServiceTax"].ToString();
                    if (serTAX == "N")
                    {
                        chkSTAX.Checked = false;
                    }
                    else
                    {
                        chkSTAX.Checked = true;
                    }


                    if (i < (Convert.ToInt32(ds1.Tables["Invoice"].Rows.Count) - 1))
                    {
                        i++;
                    }
                    else
                    {
                        break;
                    }
                }
            }
            Session["InvoiceNumber"] = null;
            Session["page"] = null;
            Session["BasicInformation"] = null;
            Submit.Enabled = true;

        }
    }

    protected void GetXML()
    {
        DataSet ds = new DataSet();
        ds.ReadXml(Server.MapPath("XML\\inv.xml"));
        {

            GridView1.DataSource = ds;
            GridView1.DataMember = "Detail";
            DataBind();


        }
        foreach (GridViewRow row in GridView1.Rows)
        {

            CheckBox chk = (CheckBox)row.FindControl("chkSTAX");
            chk.Enabled = false;
        }
    }

    //public DataSet GetData(string fy)
    //{
    //    SqlConnection conn1 = new SqlConnection(strImpex);
    //    conn1.Open();
    //    string sqlStatement1 = "select *  from iworkreg i,ijob_pos j where i.job_no=j.job_no " +
    //                           "and i.job_no like '%" + fy + "%' and j.bill_date is null order by i.jobsno";

    //    SqlDataAdapter da1 = new SqlDataAdapter(sqlStatement1, conn1);

    //    DataSet ds1 = new DataSet();
    //    da1.Fill(ds1, "ijobno");
    //    conn1.Close();
    //    return ds1;

    //}
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
    protected void BtnStandard_Click(object sender, EventArgs e)
    {

        Session["Company"] = "Std";
        strCName = txtCompName.Text;
        string jno = txtJNO.Text;
        if (jno == "0")
        {

            Response.Write("<script>alert('Select Job Number')</script>");
            
        }
        else
        {
            try
            {
                Submit.Enabled = true;
                SqlConnection conn = new SqlConnection(strImpex);
                conn.Open();
                string sqlQuery = "";
              
                string fyjs = "";
                if (chk.Checked == true)
                    fyjs = (string)Session["Lfyear"];
                else
                    fyjs = (string)Session["FinancialYear"];
                sqlQuery = "select *  from T_JobCreation where jobno='" + jno + "' ";
                SqlDataAdapter da = new SqlDataAdapter(sqlQuery, conn);
                DataSet ds = new DataSet();
                da.Fill(ds, "iworkreg");
                conn.Close();
                if (ds.Tables["iworkreg"].Rows.Count != 0)
                {
                    DataRowView row = ds.Tables["iworkreg"].DefaultView[0];
                    string jobNo = row["jobno"].ToString();
                  
                    txtJobNo.Text = jobNo;
                    //txtAssValue.Text = row["tot_ass_vl"].ToString();
                    //Session["CustomDuty"] = row["tot_duty"].ToString();
                    //string item = row["inv_dtl"].ToString();
                    //item = item.Replace("'", " ");
                    //Session["ImpotItem"] = item;
                    //txtCustomDuty.Text = (string)Session["CustomDuty"];
                    //txtImpotItem.Text = (string)Session["ImpotItem"];
                    //string pcode = row["party_code"].ToString();
                    string sType = row["mode"].ToString();
                    Session["TransportMode"] = sType;
                    //Session["PCODE"] = pcode;
                    if (sType == "A")
                        lblIName.Text = "INVOICE - IMPORTS" + " - AIR SHIPMENT";
                    else
                        lblIName.Text = "INVOICE - IMPORTS" + " - SEA SHIPMENT";
                    string be = row["beno"].ToString();
                    string bedate = row["bedate"].ToString();
                    if (bedate == "")
                    {
                        Session["BENo"] = be + " dt." + bedate;
                    }
                    else
                    {
                        DateTime beDate = Convert.ToDateTime(bedate);

                        Session["BENo"] = be + " dt." + beDate.ToString("dd/MM/yyyy");
                    }
                    txtBENo.Text = (string)Session["BENo"];
                    string sqlQuery1 = "select *  from T_ShipmentDetails where jobno='" + jobNo + "'";
                    conn.Open();
                    SqlDataAdapter da1 = new SqlDataAdapter(sqlQuery1, conn);

                    DataSet ds1 = new DataSet();
                    da1.Fill(ds1, "ishp");
                    conn.Close();
                    if (ds1.Tables["ishp"].Rows.Count == 0)
                        Response.Write("<script>alert('There is no shipment information for given job number')</script>");
                    else
                    {
                        DataRowView row1 = ds1.Tables["ishp"].DefaultView[0];

                        string bl = row1["MasterBLNo"].ToString();
                        string BLDate = row1["MasterBLDate"].ToString();

                        string hbl = row1["HouseBLNo"].ToString();
                        string HBLDate = row1["HouseBLDate"].ToString();


                        if (BLDate != "" || BLDate != string.Empty)
                            Session["BLNo"] = bl + " dt." + BLDate;
                        else
                            Session["BLNo"] = hbl + " dt." + HBLDate;

                        string pkg = row1["NoOfPackages"].ToString();
                        string pkg_unit = row1["PackagesUnit"].ToString();
                        string gross = row1["GrossWeight"].ToString();
                        string gross_unit = row1["GrossWeightUnit"].ToString();
                        pkg = pkg.Replace(".000", "");
                        gross = gross.Replace(".000", "");
                        txtBLNo.Text = (string)Session["BLNo"];
                        Session["QTY"] = pkg + " " + pkg_unit + "/" + gross + " " + gross_unit;
                        txtQty.Text = (string)Session["QTY"];
                    }
                    //string sqlQuery2 = "select *  from ijob_Pos where job_no='" + jobNo + "'";
                    //conn.Open();
                    //MySqlDataAdapter da2 = new MySqlDataAdapter(sqlQuery2, conn);
                    //DataSet ds2 = new DataSet();
                    //da2.Fill(ds2, "ijobs");
                    //conn.Close();
                    //if (ds2.Tables["ijobs"].Rows.Count == 0)
                    //    Response.Write("<script>alert('There is no Job Position information for given job number')</script>");
                    //else
                    //{
                    //    DataRowView row2 = ds2.Tables["ijobs"].DefaultView[0];
                    //    string be = row2["be_no"].ToString();
                    //    string bedate = row2["be_date"].ToString();
                    //    if (bedate == "")
                    //    {
                    //        Session["BENo"] = be + " dt." + bedate;
                    //    }
                    //    else
                    //    {
                    //        DateTime beDate = Convert.ToDateTime(bedate);

                    //        Session["BENo"] = be + " dt." + beDate.ToString("dd/MM/yyyy");
                    //    }
                    //}
                    string sqlQuery3 = "select *  from T_ShipmentContainerInfo where jobno='" + jobNo + "' order by TransId";
                    conn.Open();
                    SqlDataAdapter da3 = new SqlDataAdapter(sqlQuery3, conn);
                    DataSet ds3 = new DataSet();
                    da3.Fill(ds3, "iContr");
                    conn.Close();
                 
                    txtBENo.Text = (string)Session["BENo"];
                    if (ds3.Tables["iContr"].Rows.Count != 0)
                    {
                        DataTable dt3 = ds3.Tables[0];
                        string cno = "";
                        string cTyp = "";
                        string cSize = "";
                        string snos = "";
                        foreach (DataRow row3 in dt3.Rows)
                        {
                            //snos = row3["sr_no"].ToString();
                            cno = row3["ContainerNo"].ToString();
                            cTyp = row3["LoadType"].ToString();
                            cSize = row3["ContainerType"].ToString();
                            CNTRNO = CNTRNO + cno + ",";
                        }
                        txtNote.Text = CNTRNO.TrimEnd(',');
                        Session["NOTE"] = txtNote.Text;
                       
                        string pref = "";
                       
                        pref =  cSize + " Ft - " + cTyp;
                       
                        txtNCNTR.Text = pref;

                        
                    }
                    //end stype

                    string sqlQuery4 = "select *  from T_Importer " +
                                     "where jobno='" + txtJNO.Text + "' ";
                    conn.Open();
                    SqlDataAdapter da4 = new SqlDataAdapter(sqlQuery4, conn);
                    DataSet ds4 = new DataSet();
                    da4.Fill(ds4, "prtMast");
                    conn.Close();
                    if (ds4.Tables["prtMast"].Rows.Count != 0)
                    {
                        DataRowView row4 = ds4.Tables["prtMast"].DefaultView[0];
                        //string cCode = row4["group_id"].ToString();
                        //Session["cCode"] = cCode;
                        //if (cCode == "")
                        //{
                        txtCompName.Text = row4["Importer"].ToString();
                        string addr = row4["Address"].ToString();
                        addr = addr.Replace("'", " ");
                        Session["addr"] = addr;
                        string city = row4["City"].ToString();
                        string pin = row4["ZipCode"].ToString();
                        Session["Pin"] = pin;

                        txtCity.Text = city;
                        Session["state"] = row4["State"].ToString();

                        // Session["Phone"] = row4["tel_no"].ToString();
                        txtSubParty.Text = "";
                        rbBill.Visible = false;
                        //}
                        //else
                        //{
                        //    //Third party Addr
                        //    rbBill.Visible = true;

                        //    txtSubParty.Text = row4["party_name"].ToString();
                        //    SqlConnection connCT = new SqlConnection(strImpex);
                        //    string QueryCT = "select * from contract_mst cm,contract_addr cs  " +
                        //                     " where cm.customer_code=cs.customer_code and cm.customer_code='" + cCode + "'";
                        //    SqlDataAdapter daCT = new SqlDataAdapter(QueryCT, connCT);
                        //    DataSet dsCT = new DataSet();
                        //    daCT.Fill(dsCT, "Contrst");
                        //    if (dsCT.Tables["Contrst"].Rows.Count == 0)
                        //    {
                        //        MySqlConnection connCTN = new MySqlConnection(strImpex);
                        //        connCTN.Open();
                        //        string QueryCTN = "select * from party_group where group_id='" + cCode + "'";
                        //        MySqlDataAdapter daCTN = new MySqlDataAdapter(QueryCTN, connCTN);
                        //        DataSet dsCTN = new DataSet();
                        //        daCTN.Fill(dsCTN, "ContrstN");
                        //        connCTN.Close();
                        //        DataRowView rowN = dsCTN.Tables["ContrstN"].DefaultView[0];

                        //        txtCompName.Text = rowN["groupName"].ToString();

                        //        Session["pName"] = row4["party_name"].ToString();

                        //        string pName = txtCompName.Text;


                        //        string addr11 = row4["address"].ToString();
                        //        string city = row4["city"].ToString();
                        //        string pin = row4["pin"].ToString();
                        //        Session["addr"] = addr11;
                        //        Session["Pin"] = pin;
                        //        txtCity.Text = city;
                        //        Session["state"] = row4["state"].ToString();

                        //        Session["Phone"] = row4["tel_no"].ToString();

                        //        GrdPaddr.Visible = true;
                        //    }
                        //    else
                        //    {
                        //        DataRowView rowCT = dsCT.Tables["Contrst"].DefaultView[0];

                        //        txtCompName.Text = rowCT["customer_name"].ToString();
                        //        Session["pName"] = rowCT["customer_name"].ToString();
                        //        string pName = txtCompName.Text;


                        //        string addr11 = rowCT["address"].ToString();
                        //        string city = rowCT["city"].ToString();
                        //        string pin = rowCT["pin"].ToString();
                        //        Session["addr"] = addr11;
                        //        Session["Pin"] = pin;
                        //        txtCity.Text = city;
                        //        Session["state"] = rowCT["state"].ToString();

                        //        Session["Phone"] = rowCT["tel_no"].ToString();
                        //    }

                        //}

                        int i = 0;
                        int j = 0;
                        int k = 1;
                        string addr1 = (string)Session["addr"];
                        string[] strCC = addr1.Split(',');
                        foreach (string strThisCC in strCC)
                        {
                            i = i + 1;


                        }
                        j = i / 2;
                        string addrs = "";
                        string addrs1 = "";
                        foreach (string strThisCC in strCC)
                        {
                            if (k < j)
                                addrs += strThisCC + ",";
                            else
                                addrs1 += strThisCC + ",";
                            k = k + 1;
                        }
                        string ADDRS = addrs;
                        string ADDRS1 = addrs1.TrimEnd(',');

                        txtAdd1.Text = addr1;

                        //trMain.Visible = false;
                        //TrAddr.Visible = true;
                        //TrAddr1.Visible = true;

                        //GrdPaddr.DataSource = PartyAddr(pcode);
                        //GrdPaddr.DataBind();
                        //GrdPaddr.Visible = true;
                        //GrdADDRSCROLL.Visible = true;
                    }
                }

                else
                {
                    Response.Write("<script>alert('Not Found Records')</script>");

                }

            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('" + ex.Message + "');", true);
            }

        }

    }

    protected void GETJOBS(string jobNo)
    {
        SqlConnection conn = new SqlConnection(strImpex);
        conn.Open();
        string Query = "select * from M_iec_invoicenew where jobno = '" + jobNo + "'";
        SqlDataAdapter da = new SqlDataAdapter(Query, conn);

        DataSet ds = new DataSet();
        da.Fill(ds, "bill");
        conn.Close();
        if (ds.Tables["bill"].Rows.Count != 0)
        {
            DataRowView row = ds.Tables["bill"].DefaultView[0];
            string eXISTbILL = row["invoice"].ToString();
          

            ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "alert('Given jobs has already billing. The Bill No. " + eXISTbILL + "' . Do you want Continue ?');", true);
        }
    }
    protected void InvoiceGenerated(string iType)
    {
        SqlConnection conn2 = new SqlConnection(strImpex);
        conn2.Open();
        string strQuery = "select * from M_RunningNo where iectype='" + iType + "' and Fyear='" + fyear + "'";
        SqlDataAdapter da2 = new SqlDataAdapter(strQuery, conn2);

        DataSet ds2 = new DataSet();
        da2.Fill(ds2, "INVOICE");
        conn2.Close();
        if (ds2.Tables["INVOICE"].Rows.Count != 0)
        {
            DataRowView row = ds2.Tables["INVOICE"].DefaultView[0];
            InNo = row["rno"].ToString();
            InCode = row["iecCode"].ToString();

            InID = Convert.ToInt32(InNo) + 1;
            Session["InvNo"] = InID;
            invoice = InCode + "/" + InID;
            lblInvNo.Text = invoice;
          
        }
        else
            Response.Write("<script>alert('Invoice has not Found for Given Financial Year')</script>");
    }

    protected void Submit_Click(object sender, EventArgs e)
    {
        GetTransaction();
        string tp = "SB";
        string jobNo = txtJobNo.Text;
        
        try
        {

            InvoiceGenerated(tp);
            if (txtCompName.Text == "" || txtJobNo.Text == "")
            {
                Response.Write("<script>alert('Please Give the Invoice Details')</script>");
                txtCompName.Focus();
            }
            else
            {


                string Query = "select * from M_iec_invoicenew where jobno = '" + jobNo + "'";

                DataSet ds = GetDataSQL(Query);

                if (ds.Tables["SQLtable"].Rows.Count == 0)
                {
                    if (invFlag == 0)
                            PIPLInovice();
                   
                }
                else
                    Response.Write("<script>alert('Invoice has been Generated Already....')</script>");


            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('" + ex.Message + "');", true);
        }

    }

    protected void PIPLInovice()
    {
        int result =0;
        string inv = "";
        Session["INVOICECTR"] = lblInvNo.Text;
        if ((string)Session["Invoice"] == "Invoice")
        {
            InCode = "SB";
        }
        else
        {
            InCode = "DB";
        }
        string dates = invDate.Text;
       
        string[] DT = dates.Split('/');
        dates = DT[2] + "-" + DT[1] + "-" + DT[0];

        string EntryDate = DateTime.Now.ToString();

        double st = Convert.ToDouble(SubTotal.Text);
        double stTax = Convert.ToDouble(SubTotalTax.Text);
        double stax = Convert.ToDouble(sTax.Text);
        double ec = Convert.ToDouble(EdCess.Text);
        double shc = Convert.ToDouble(SHCess.Text);
        double gt = Convert.ToDouble(Totals.Text);
        double la = Convert.ToDouble(LessAd.Text);
        double nt = Convert.ToDouble(BalanceDue.Text);

        string impItem = txtImpotItem.Text;
        impItem = impItem.Replace("'", " ");
        string ADDRESS = txtAdd1.Text;
        ADDRESS = ADDRESS.Replace("'", " ");
        string pREFF = txtParty_Reff.Text;
        pREFF = pREFF.Replace("'", " ");

        string impRK = txtimpRemark.Text;
        string intRK = txtIndentRemark.Text;
        impRK = impRK.Replace("'", " ");
        intRK = intRK.Replace("'", " ");

        string ino = Session["InvNo"].ToString();
        Int32 invno = Convert.ToInt32(ino);
        string staxper = drServiceTax.SelectedValue;
        string suffix = txtSuffix.Text;
        string Notes = txtNote.Text;


        for (int i = 0; i <= lbjobno.Items.Count - 1; i++)
        {

        string jobno = lbjobno.Items[i].Text;
       // string[] jno = jobno.Split('/');
        string jn = jobno;

         inv = lblInvNo.Text + "-" + jn;

        
         if (txtJNO.Text == jn)
         {
             SqlConnection conn = new SqlConnection(strImpex);
             string sqlQuery = " insert into M_IEC_InvoiceNew(invoice,invoiceDate,compName,Address1,address2,City,pincode,state," +
                               " phone,partyReff,jobNo,BLNo,BENoDate,importitem,Quantity,Ass_value,Container_no,Custom_Duty,subTotal,subTotalTax,staxPercent,Service_tax,Edu_Cess,SEC_Chess," +
                               " Grand_total,less_advance,Net_total,sub_party,Nettotal_words,invoiceType,invoiceNo,Mode,entryBy,eDate,fyear,TransportMode,suffix,notes,impRemark,interRemark) values('" + inv + "','" + dates + "','" + txtCompName.Text + "'," +
                               " '" + ADDRESS + "','" + (string)Session["state"] + "','" + txtCity.Text + "','" + (string)Session["Pin"] + "','" + (string)Session["state"] + "','" + (string)Session["Phone"] + "','" + pREFF + "'," +
                               " '" + jobno + "','" + txtBLNo.Text + "','" + txtBENo.Text + "','" + impItem + "','" + txtQty.Text + "'," +
                               " '" + txtAssValue.Text + "','" + txtNCNTR.Text + "','" + txtCustomDuty.Text + "'," + st + "," + stTax + ",'" + staxper + "'," + stax + "," + ec + "," + shc + "," +
                               " " + gt + "," + la + "," + nt + ",'" + txtSubParty.Text + "','" + txtRupees.Text + "','" + InCode + "'," + invno + ",'" + (string)Session["RBMODE"] + "','" + (string)Session["USER-NAME"] + "'," +
                               "'" + EntryDate + "','" + (string)Session["FinancialYear"] + "','" + (string)Session["TransportMode"] + "','" + suffix + "','" + Notes + "','" + impRK + "','" + intRK + "')";
             conn.Open();
             SqlCommand cmd = new SqlCommand(sqlQuery, conn);
             SqlDataAdapter da = new SqlDataAdapter();
             cmd.CommandText = sqlQuery;
             cmd.Connection = conn;
             da.SelectCommand = cmd;


             result = cmd.ExecuteNonQuery();

            
         }
         else
         {
             SqlConnection conn = new SqlConnection(strImpex);
             string sqlQuery = " insert into M_IEC_InvoiceNew(invoice,invoiceDate,compName,Address1,address2,City,pincode,state," +
                    " phone,partyReff,jobNo,BLNo,BENoDate,importitem,Quantity,Ass_value,Container_no,Custom_Duty,subTotal,subTotalTax,staxPercent,Service_tax,Edu_Cess,SEC_Chess," +
                    " Grand_total,less_advance,Net_total,sub_party,Nettotal_words,invoiceType,invoiceNo,Mode,entryBy,eDate,fyear,TransportMode,suffix,notes,impRemark,interRemark) values('" + inv + "','" + dates + "','" + txtCompName.Text + "'," +
                    " '" + ADDRESS + "','" + (string)Session["state"] + "','" + txtCity.Text + "','" + (string)Session["Pin"] + "','" + (string)Session["state"] + "','" + (string)Session["Phone"] + "','" + pREFF + "'," +
                    " '" + jobno + "','" + txtBLNo.Text + "','" + txtBENo.Text + "','" + impItem + "','" + txtQty.Text + "'," +
                    " '" + txtAssValue.Text + "','" + txtNCNTR.Text + "','" + txtCustomDuty.Text + "'," + 0 + "," + 0 + ",'" + 0 + "'," + 0 + "," + 0 + "," + 0 + "," +
                    " " + 0 + "," + 0 + "," + 0 + ",'" + txtSubParty.Text + "','" + txtRupees.Text + "','" + InCode + "'," + invno + ",'" + (string)Session["RBMODE"] + "','" + (string)Session["USER-NAME"] + "'," +
                    "'" + EntryDate + "','" + (string)Session["FinancialYear"] + "','" + (string)Session["TransportMode"] + "','" + suffix + "','" + Notes + "','" + impRK + "','" + intRK + "')";
            
             conn.Open();
             SqlCommand cmd = new SqlCommand(sqlQuery, conn);
             SqlDataAdapter da = new SqlDataAdapter();
             cmd.CommandText = sqlQuery;
             cmd.Connection = conn;
             da.SelectCommand = cmd;


             result = cmd.ExecuteNonQuery();

            
         }
      

        }
            if (result == 1)
            {
                invoiceDTL(inv);
                updateRNO(invno, InCode, fyear);

                //string billstatus = "update iworkreg_jobstatus set status_job='Y' where job_no='" + txtJobNo.Text + "'";
                //GetCommand(billstatus, strconnJSU);

                if (flag != 0)
                {
                    invFlag = 1;

                 

                    BtnStandard.Visible = false;
                    Submit.Enabled = false;
                    btnMail.Enabled = true;
                    preview.Enabled = true;
                    ExportTally.Enabled = true;
                   
                    balance1.Visible = false;

                }
            }
         
             
        Response.Write("<script>" + "alert('Invoice has successfully Generated');" + "</script>");
        }
        
    protected void invoiceDTL(string sbNo)
    {
        int count = 1;

        foreach (GridViewRow ROW in GridView1.Rows)
        {
            TextBox amt = (TextBox)ROW.FindControl("amt1");
            TextBox chrg = (TextBox)ROW.FindControl("txtDetails");
            TextBox recpt = (TextBox)ROW.FindControl("txtRecpt");
           
            CheckBox chk = (CheckBox)ROW.FindControl("chkSTAX");
           
            string amount = amt.Text;
            string Charge_desc = chrg.Text;
            string Receipt = recpt.Text;
            string sTAXval = "";
          
            if (amount == "")
                amount = "0.00";
            if (amount != "0.00" && Charge_desc != "")
            {
                if (chk.Checked)
                    sTAXval = "Y";
                else
                    sTAXval = "N";
                string Query = "insert into T_iec_invoiceNew_DTL(invoice,sno,charge_desc,receipt,amount,serviceTax) " +
                               "values('" + sbNo + "'," + count + ",'" + Charge_desc + "','" + Receipt + "'," + amount + ",'" + sTAXval + "')";
                GetCommandIMP(Query);
                count = count + 1;
                flag = 1;
                Session["IINVNO"] = sbNo;
            }
           
        }

      
        if (flag != 1)
        {
            string Query = "delete from M_iec_invoiceNew where invoice='" + sbNo + "'";
            GetCommandIMP(Query);
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
    //protected void GetCommand(string Query, string connSTR)
    //{
    //    MySqlConnection conn = new MySqlConnection(connSTR);
    //    conn.Open();
    //    MySqlCommand cmd = new MySqlCommand(Query, conn);
    //    cmd.CommandText = Query;
    //    cmd.Connection = conn;
    //    int res = cmd.ExecuteNonQuery();
    //}

    protected void updateRNO(int ino, string iType, string fy)
    {
        SqlConnection conn = new SqlConnection(strImpex);
        string sqlQuery = "update M_RunningNo set rno=" + ino + " where iecType='" + iType + "' and fyear='" + fy + "'";
        conn.Open();
        SqlCommand cmd = new SqlCommand(sqlQuery, conn);
        SqlDataAdapter da = new SqlDataAdapter();
        cmd.CommandText = sqlQuery;
        cmd.Connection = conn;
        da.SelectCommand = cmd;


        int result = cmd.ExecuteNonQuery();
    }
    protected void LKRupees_Click(object sender, EventArgs e)
    {

        txtRupees.Text = RsConvert.rupees(Convert.ToInt64(balance1.Text));
        Submit.Focus();
    }
    protected void preview_Click(object sender, EventArgs e)
    {
        String sno = (string)Session["INVOICECTR"];
        Session["BILLTYPE"] = "SB";
        Session["InvNoRep"] = sno;


        string strQuery = "select * from M_iec_invoiceNew where invoice='" + lblInvNo.Text + "' and contr_code is null and particular1 is not null";
        SqlConnection conn = new SqlConnection(strImpex);
        conn.Open();
        SqlDataAdapter da = new SqlDataAdapter(strQuery, conn);
        DataSet ds = new DataSet();
        da.Fill(ds, "table");
        conn.Close();
        if (ds.Tables["table"].Rows.Count == 0)
            ScriptManager.RegisterStartupScript(this, this.GetType(), "popup", "window.open('CryInvoiceReportSTax.aspx','_blank','menubar=no, scrollbars=yes, toolbar=no, location=no, resizable=yes, left=20, top=20');", true);
    
        else
            ScriptManager.RegisterStartupScript(this, this.GetType(), "popup", "window.open('CryInvoiceReport.aspx','_blank','menubar=no, scrollbars=yes, toolbar=no, location=no, resizable=yes, left=20, top=20');", true);
        preview.Visible = true;
      
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

    protected void GrdPaddr_SelectedIndexChanged(object sender, EventArgs e)
    {

        //for (int i = 0; i < GrdPaddr.Rows.Count; i++)
        //{
        //    if (GrdPaddr.SelectedIndex == i)
        //    {
        //        string NO = Convert.ToString(GrdPaddr.SelectedDataKey.Value);
        //        string pcode = GrdPaddr.Rows[i].Cells[0].Text;
        //        MySqlConnection conn = new MySqlConnection(strImpex);
        //        conn.Open();
        //        string sqlQuery = "select *  from prt_addr where party_code='" + pcode + "' and addr_num=" + NO + "";
        //        MySqlDataAdapter da = new MySqlDataAdapter(sqlQuery, conn);
        //        DataSet ds = new DataSet();
        //        da.Fill(ds, "addr");
        //        conn.Close();
              
        //        DataRowView row = ds.Tables["addr"].DefaultView[0];
        //        string addr1 = row["address"].ToString();
        //        string city = row["city"].ToString();
        //        string state = row["state"].ToString();
        //        string pin = row["pin"].ToString();
        //        Session["addr"] = addr1;
        //        Session["city"] = city;
        //        Session["state"] = state;
        //        Session["Pin"] = pin;
        //        Session["BCODE"] = NO;
        //        txtCity.Text = city;
               
        //        txtAdd1.Text = addr1;
        //        Session["Phone"] = row["tel_no"].ToString();


        //    }
        //}
        //GrdADDRSCROLL.Visible = false;
        //GrdPaddr.Visible = false;
        //TrAddr.Visible = false;

        //Button1.Visible = trMain.Visible = true;
        //TrAddr1.Visible = false;
      
    }
    //public DataSet PartyAddr(string pcode)
    //{
    //    MySqlConnection conn = new MySqlConnection(strImpex);
    //    conn.Open();
    //    string sqlQuery4 = "select *  from prt_mast m,prt_addr a " +
    //                           "where m.party_code=a.party_code and  m.party_code='" + pcode + "' order by addr_num";
    //    MySqlDataAdapter da4 = new MySqlDataAdapter(sqlQuery4, conn);
    //    DataSet ds4 = new DataSet();
    //    da4.Fill(ds4, "prtMast");
    //    conn.Close();
    //    return ds4;
    //}

    protected void rbBill_SelectedIndexChanged(object sender, EventArgs e)
    {
        string pcode = (string)Session["PCODE"];
        string BiilType = rbBill.SelectedValue;
        if (BiilType == "DP")
        {
            //GrdADDRSCROLL.Visible = true;
            //GrdPaddr.DataSource = PartyAddr(pcode);
            //GrdPaddr.DataBind();
            //GrdPaddr.Visible = true;
            //TrAddr.Visible = true;
            //TrAddr1.Visible = true;
            //Panel2.Visible = true;
            txtSubParty.Text = "";
        }
        else
        {
            GrdADDRSCROLL.Visible = false;
            
            GrdPaddr.Visible = false;
            TrAddr.Visible = false;
            TrAddr1.Visible = false;
            Panel2.Visible = true;
            trMain.Visible = true;
        }
    }
    protected void GridView1_RowDataBond(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            TextBox amt = (TextBox)e.Row.FindControl("amt1");
            if (amt.Text == "")
                amt.Text = "0.00";
           
        }
        SubTotal.Text = Gross.ToString("#0.00#");
    }
    public DataSet GetCharge()
    {
        SqlConnection conn = new SqlConnection(strImpex);
        conn.Open();
        string Query = "select * from charge_mst";
        SqlDataAdapter da = new SqlDataAdapter(Query, conn);
        DataSet ds = new DataSet();
        da.Fill(ds, "table");
        conn.Close();
        return ds;
    }
    protected void amt1_TextChanged(object sender, EventArgs e)
    {


        foreach (GridViewRow row in GridView1.Rows)
        {

            CheckBox chk = (CheckBox)row.FindControl("chkSTAX");
            chk.Enabled = true;
        }

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
            if (chk.Checked)
            {
                GetServiceTax(cmp);
                Double amt0 = Convert.ToDouble(txt.Text);
                Double aStax = amt0 / 100 * vSTax;
                Double aECess = aStax / 100 * vECess;
                Double aSHECess = aStax / 100 * vSHECess;

               
                GrossTot = GrossTot + amt0;
                gSTAX = gSTAX + aStax;
                gECess = gECess + aECess;
                gSHECess = gSHECess + aSHECess;
            }
            else
            {
             
                Double tot = Convert.ToDouble(txt.Text);
                total = total + tot;
                txt.Text = tot.ToString("#0.00#");
                
            }
        }

        gSTAX = Math.Round(gSTAX);
        SubTotal.Text = total.ToString("#0.00#");
        SubTotalTax.Text = GrossTot.ToString("#0.00#");
        sTax.Text = gSTAX.ToString("#0.00#");

        gECess = Math.Round(gECess);
        gSHECess = Math.Round(gSHECess);

        EdCess.Text = gECess.ToString("#0.00#");
        SHCess.Text = gSHECess.ToString("#0.00#");
        Double gTotals = total + GrossTot + gSTAX + gSHECess + gECess;
        Totals.Text = gTotals.ToString("#0.00#");
      
        GetPERCENT();

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
    protected void LessAd_TextChanged(object sender, EventArgs e)
    {
        txtRupees.Text = RsConvert.rupees(Convert.ToInt64(balance1.Text));
        Submit.Focus();
    }
    protected void chkSTAX_CheckedChanged(object sender, EventArgs e)
    {
        
    }
    protected void GetServiceTax(string cmp)
    {
        string sVal = drServiceTax.SelectedValue;

        SqlConnection conn2 = new SqlConnection(strImpex);
        conn2.Open();

        string strQuery = "select * from M_ServiceMaster where serviceTax='" + sVal + "'";
        SqlDataAdapter da2 = new SqlDataAdapter(strQuery, conn2);
        DataSet ds2 = new DataSet();
        da2.Fill(ds2, "SERVICETAX");
        conn2.Close();

        DataRowView row = ds2.Tables["SERVICETAX"].DefaultView[0];
        Double stax = Convert.ToDouble(row["serviceTax"].ToString());
        Double Ecess = Convert.ToDouble(row["ecess"].ToString());
        Double SHEcess = Convert.ToDouble(row["shecess"].ToString());
       
        vSTax = stax;
        vECess = Ecess;
        vSHECess = SHEcess;
    }

    protected void txtJNO_TextChanged(object sender, EventArgs e)
    {
        string jno = txtJNO.Text;
        string jobNo = "";

        SqlConnection connM = new SqlConnection(strImpex);
        connM.Open();
        string sqlQueryM = "";
        if (chk.Checked == true)
            sqlQueryM = "select *  from T_JobCreation where jobno='" + jno + "' ";
        else
            sqlQueryM = "select *  from T_JobCreation where jobno='" + jno + "' ";

        SqlDataAdapter daM = new SqlDataAdapter(sqlQueryM, connM);
        try
        {
            DataSet dsM = new DataSet();
            daM.Fill(dsM, "iworkreg");

            connM.Close();
            if (dsM.Tables["iworkreg"].Rows.Count != 0)
            {
                DataRowView rowM = dsM.Tables["iworkreg"].DefaultView[0];
                jobNo = rowM["jobno"].ToString();
                GetSQLJOBS(jobNo);
            }

        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('" + ex.Message + "');", true);
        }
       


    }

    protected void GetSQLJOBS(string jobNo)
    {
        SqlConnection conn = new SqlConnection(strImpex);
        conn.Open();
        string Query = "select * from iec_invoicenew where jobno = '" + jobNo + "'";
        SqlDataAdapter da = new SqlDataAdapter(Query, conn);

        DataSet ds = new DataSet();
        da.Fill(ds, "bill");
        conn.Close();
        if (ds.Tables["bill"].Rows.Count != 0)
        {
           
            DataRowView row = ds.Tables["bill"].DefaultView[0];
            string eXISTbILL = row["invoice"].ToString();
         
            ScriptManager.RegisterStartupScript(this, this.GetType(), "confirm", "confirm('Given jobs has already billing. The Bill No. " + eXISTbILL + " . Do you want Continue...?');", true);
        }
    }


    /* Change new coding from 28/03/2012*/


    protected void drServiceTax_SelectedIndexChanged(object sender, EventArgs e)
    {
       
    }
    protected void chk_CheckedChanged(object sender, EventArgs e)
    {
        if (chk.Checked == true)
            fyear = (string)Session["Lfyear"];
        else
            fyear = (string)Session["FinancialYear"];

        Session["FYEARBill"] = fyear;
       
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
            Session["JOBNO"] = txtJobNo.Text;
            Session["MAILBUTTON"] = "OK";
            Session["PageName"] = "PIPLInvoiceStax.aspx";
            Session["Maill"] = "SendMaill";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "popup", "window.open('CryInvoiceReportSTax.aspx','_blank','menubar=no, scrollbars=yes, toolbar=no, location=no, resizable=no,height=650,width=700, left=20, top=20');", true);

            
        }

    }
   
    protected void ExportTally_Click(object sender, EventArgs e)
    {
        GetFSIO();
    }
    protected void GetFSIO()
    {

        string jno = txtJNO.Text;


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

            sqlQueryM = "select INVOICE,invoiceDate,jobNo,invoiceType,compName,invoiceNo,Grand_Total,service_tax + edu_cess + sec_chess as STAX," +
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
    protected void BtnExit_Click(object sender, EventArgs e)
    {
        Response.Redirect("index.aspx");
    }
    protected void btncalculate_Click(object sender, EventArgs e)
    {
        GetTransaction();
    }
    protected void txtadd_Click(object sender, EventArgs e)
    {
        SqlConnection cnn = new SqlConnection(strImpex);
        cnn.Open();
        string query = "select * from T_JobCreation where jobno like '%" + txtjobno1.Text + "%'";
        SqlDataAdapter da = new SqlDataAdapter(query, cnn);
        DataSet ds = new DataSet();
        da.Fill(ds, "jobno");
        if (ds.Tables["jobno"].Rows.Count != 0)
        {
            DataRowView row = ds.Tables["jobno"].DefaultView[0];
            string jno = row["jobno"].ToString();
            if (txtjobno1.Text != "")
            {
                lbjobno.Items.Insert(0, jno);
                txtjobno1.Focus();
            }
            else
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Please enter the job no');", true);
           
        }
        else
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Please enter the correct Job no');", true);
        txtjobno1.Focus();
    }
    protected void btnexit1_Click(object sender, EventArgs e)
    {
        Response.Redirect("index.aspx");
    }
    protected void btnsubmit_Click(object sender, EventArgs e)
    {

        if ((lbjobno.Items.Count == 0))
        {
            Response.Write("<script>alert('Please add Job no') </script>");

        }
        else
        {

            int i = lbjobno.Items.Count - 1;
            string job = lbjobno.Items[i].Text;
            //string[] jno = { job };
            txtJNO.Text = job;

            dvmain.Visible = true;
            tbmultiplejob.Visible = false;
        }

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
}