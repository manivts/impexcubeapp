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

public partial class PIPLinvoiceSTAXTrans : System.Web.UI.Page 
{
    VTS.ImpexCube.Utlities.Utility InvSequence = new VTS.ImpexCube.Utlities.Utility();
  
    //string strPIPL = (string)ConfigurationManager.AppSettings["ConnectionImpex"];
    //string strconn = (string)ConfigurationManager.AppSettings["ConnectionImpex"];
    //string strconn1 = (string)ConfigurationManager.AppSettings["ConnectionVisual"];
    //string strconnJSU = (string)ConfigurationManager.AppSettings["ConnectionJobStages"];

    string strImpex = (string)ConfigurationManager.ConnectionStrings["Constr"].ConnectionString;
    #region
    private string strCName;
    private string InNo;
    private string InCode;
    private string invoice;
    private Int32 InID;
    string CNTRNO = "";
    //private string eXISTbILL;
    DateTime blDate;
    DateTime hblDate;
    int flag=0;
    int invFlag = 0;
    string fyear="";
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
       
       
        fyear=(string)Session["FinancialYear"];
       
        if (IsPostBack == false)
        {
            TallyAccountName();
            Session["VGUID"] = Guid.NewGuid().ToString();
            string lfyear = (string)Session["Lfyear"];
            
            chk.Text = lfyear;
           
            GetXML();
            Submit.Enabled = false;
            btnMail.Enabled = false;
            preview.Enabled = false;

            string strQuery = "select * from M_ServiceMaster where fyear='" + fyear + "' order by fyear desc";
            drServiceTax.DataSource = GetDataSQL(strQuery);
            drServiceTax.DataTextField = "sTax";
            drServiceTax.DataValueField = "serviceTax";
            drServiceTax.DataBind();

            Session["RBMODE"] = "IMP";
            TrAddr.Visible=false;
            TrAddr1.Visible=false;
          
            Session["Invoice"] = "Invoice";

            //lblUser.Text = (string)Session["USER-NAME"];
           
            //if (lblUser.Text == "")
            //{
            //    Response.Redirect("~/PIPL.aspx");
            //}
            Session["IECName"] = "";
            Session["IECAdd1"] = "";
            Session["IECAdd2"] = "";
            Session["IECCity"] = "";
            Session["Pin"] = "";
            Session["Phone"] = "";
           
            try
            {
                drJobNo.DataSource = GetData(fyear);
                drJobNo.DataValueField = "jobno";
                drJobNo.DataTextField = "jobno";
                drJobNo.DataBind();
                drJobNo.Items.Insert(0, new ListItem("~Select~", "0"));
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
          
            lblIName.Text = "INVOICE - IMPORTS";
              
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
            //SqlConnection conn = new SqlConnection(strImpex);
            //string sqlQuery = "select  * from AppDetails";
            //SqlDataAdapter da = new SqlDataAdapter(sqlQuery, conn);
            //DataSet ds = new DataSet();
            //da.Fill(ds, "name");
            //if (ds.Tables["name"].Rows.Count != 0)
            //{
            //    DataRowView row = ds.Tables["name"].DefaultView[0];
            //    lblCompName.Text = row["CompanyName"].ToString();
            //}
        }

    }
    protected void FillBasicInformation()
    {
        if (Session["BasicInformation"] != null)
        {
            string BasicValue = (string)Session["BasicInformation"];
            string[] Values = BasicValue.Split('~');
            drJobNo.SelectedValue = Values[0];
            txtCompName.Text = Values[1];
            txtJobNo.Text = Values[2];
            txtSubParty.Text = Values[3];
           
            txtAdd1.Text = Values[7];
           
            txtCity.Text = Values[9];
          
            txtAssValue.Text = Values[13];
           
            txtParty_Reff.Text = Values[16];
           
        
        }
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
                    //CheckBox chkSTAX = (CheckBox)row2.FindControl("chkSTAX");
                    TextBox amt = (TextBox)row2.FindControl("amt1");
                    DataRowView dr = ds1.Tables["Invoice"].DefaultView[i];
                    desc.Text =  dr["charge_desc"].ToString();
                    Recpt.Text = dr["receipt"].ToString();
                    amt.Text = dr["amount"].ToString();
                    string serTAX=dr["ServiceTax"].ToString();
                    //if (serTAX  == "N")
                    //{
                    //    chkSTAX.Checked = false;
                    //}
                    //else 
                    //{
                    //    chkSTAX.Checked = true;
                    //}
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
            Session["BasicInformation"]=null;

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
    }

    public DataSet GetData(string fy)
    {
        SqlConnection conn1 = new SqlConnection(strImpex);
        string sqlStatement1 = "select *  from T_JobCreation  order by jobno";
        SqlDataAdapter da1 = new SqlDataAdapter(sqlStatement1, conn1);
        DataSet ds1 = new DataSet();
        da1.Fill(ds1, "ijobno");
        return ds1;
    }
    public DataSet GetDataSQL(string Query)
    {
        SqlConnection conn = new SqlConnection(strImpex);
        SqlDataAdapter da = new SqlDataAdapter(Query, conn);
        DataSet ds = new DataSet();
        da.Fill(ds, "SQLtable");
        return ds;
    }
    protected void BtnStandard_Click(object sender, EventArgs e)
    {
        TallyAccountName();
        Session["Company"] = "Std";
        strCName = txtCompName.Text;
        string jno = drJobNo.SelectedValue;
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

                if (ds.Tables["iworkreg"].Rows.Count != 0)
                {
                    DataRowView row = ds.Tables["iworkreg"].DefaultView[0];
                    string jobNo = row["jobno"].ToString();
                   
                    txtJobNo.Text = jobNo;
                    txtAssValue.Text = row["TotalAssval"].ToString();
                    txtCustomDuty.Text = row["TotalDuty"].ToString();
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
                    GetItemImport(jobNo);
                   // Session["PCODE"] = pcode;
                    if (sType == "A")
                        lblIName.Text = "INVOICE - IMPORTS" + " - AIR SHIPMENT";
                    else
                        lblIName.Text = "INVOICE - IMPORTS" + " - SEA SHIPMENT";

                    string be = row["beno"].ToString();
                    string bedate = row["bedate"].ToString();
                    //if (bedate == "")
                    //{
                    //    Session["BENo"] = be + " dt." + bedate;
                    //}
                    //else
                    //{
                    //    bedate = row["bedate"].ToString();
                    //    Session["BENo"] = be + " dt." + bedate;
                    //    //Session["BENo"] = be + " dt." + beDate.ToString("dd/MM/yyyy");
                    //}
                    txtBENo.Text = be;
                    txtBEDate.Text = bedate;
                    string sqlQuery1 = "select *  from T_ShipmentDetails where jobno='" + jobNo + "'";
                    conn.Open();
                    SqlDataAdapter da1 = new SqlDataAdapter(sqlQuery1, conn);

                    DataSet ds1 = new DataSet();
                    da1.Fill(ds1, "ishp");
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
                    //MySqlDataAdapter da2 = new MySqlDataAdapter(sqlQuery2, conn);
                    //DataSet ds2 = new DataSet();
                    //da2.Fill(ds2, "ijobs");
                    //if (ds2.Tables["ijobs"].Rows.Count == 0)
                    //    Response.Write("<script>alert('There is no Job Position information for given job number')</script>");
                    //else
                    //{
                    //    DataRowView row2 = ds2.Tables["ijobs"].DefaultView[0];
                    //    string be = row2["be_no"].ToString();
                    //    string bedate = row2["be_date"].ToString();
                    //    if (bedate == "")
                    //    {
                    //       Session["BENo"] = be + " dt." + bedate;
                    //    }
                    //    else
                    //    {
                    //        DateTime beDate = Convert.ToDateTime(bedate);

                    //        Session["BENo"] = be + " dt." + beDate.ToString("dd/MM/yyyy");
                    //    }
                    //}
                    string sqlQuery3 = "select *  from T_ShipmentContainerInfo where jobno='" + jobNo + "' order by TransId";
                   
                    SqlDataAdapter da3 = new SqlDataAdapter(sqlQuery3, conn);
                    DataSet ds3 = new DataSet();
                    da3.Fill(ds3, "iContr");
                  
                    //txtBENo.Text = ;
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
                        
                        string pref = "";
                       
                            pref = cSize + " Ft - " + cTyp;
                     
                       txtNCNTR.Text = pref;

                     
                    }


                    string sqlQuery4 = "select *  from T_Importer " +
                                    "where jobno='" + drJobNo.SelectedValue + "' ";
                   
                    SqlDataAdapter da4 = new SqlDataAdapter(sqlQuery4, conn);
                    DataSet ds4 = new DataSet();
                    da4.Fill(ds4, "prtMast");
                    DataRowView row4 = ds4.Tables["prtMast"].DefaultView[0];
                    //string cCode = row4["group_id"].ToString();
                    //Session["cCode"] = cCode;
                    //if (cCode == "")
                    //{
                    txtCompName.Text = row4["Importer"].ToString();
                    Session["BranchID"] = row4["BranchSno"].ToString();
                    if ((string)Session["BranchID"] == "")
                    {
                        Session["BranchID"] = "0";
                    }
                    try
                    {
                    string VchType="SB";
                    txtInvSeqNo.Text = Convert.ToString(InvSequence.InvSeqNO(row4["Importer"].ToString(),VchType,drJobNo.SelectedItem.Text));
                    }
                    catch{
                    }
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
                       
                    //    rbBill.Visible = true;
                       
                    //    txtSubParty.Text = row4["party_name"].ToString();
                    //    SqlConnection connCT = new SqlConnection(strconn);
                    //    string QueryCT = "select * from contract_mst cm,contract_addr cs  " +
                    //                     " where cm.customer_code=cs.customer_code and cm.customer_code='" + cCode + "'";
                    //    SqlDataAdapter daCT = new SqlDataAdapter(QueryCT, connCT);
                    //    DataSet dsCT = new DataSet();
                    //    daCT.Fill(dsCT, "Contrst");
                    //    if (dsCT.Tables["Contrst"].Rows.Count == 0)
                    //    {
                    //        MySqlConnection connCTN = new MySqlConnection(strconn1);
                    //        string QueryCTN = "select * from party_group where group_id='" + cCode + "'";
                    //        MySqlDataAdapter daCTN = new MySqlDataAdapter(QueryCTN, connCTN);
                    //        DataSet dsCTN = new DataSet();
                    //        daCTN.Fill(dsCTN, "ContrstN");
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

    public void GetItemImport(string Jobno)
    {
        SqlConnection conn = new SqlConnection(strImpex);
        string sqlQuery = "select ProductDesc  from T_product where jobno='" + Jobno + "' ";
        SqlDataAdapter da = new SqlDataAdapter(sqlQuery, conn);
        DataSet ds = new DataSet();
        da.Fill(ds, "ProductDesc");
        conn.Close();
        if (ds.Tables["ProductDesc"].Rows.Count != 0)
        {
            DataRowView row = ds.Tables["ProductDesc"].DefaultView[0];
            string Product = row["ProductDesc"].ToString();
            txtImpotItem.Text = Product;
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

    protected void GETJOBS(string jobNo)
    {
        SqlConnection conn = new SqlConnection(strImpex);
        string Query = "select * from M_iec_invoicenew where jobno = '" + jobNo + "'";
        SqlDataAdapter da = new SqlDataAdapter(Query, conn);

        DataSet ds = new DataSet();
        da.Fill(ds, "bill");
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
        string strQuery = "select * from M_RunningNo where iectype like '" + iType + "' and Fyear='" + fyear + "'";
        SqlDataAdapter da2 = new SqlDataAdapter(strQuery, conn2);
        
        DataSet ds2 = new DataSet();
        da2.Fill(ds2, "INVOICE");

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


                string Query = "select * from M_iec_invoiceNew where jobno = '" + jobNo + "'";

                DataSet ds = GetDataSQL(Query);

              
                int i=ds.Tables["SQLtable"].Rows.Count;
                if (i >= 1 && i <= 3)
                {
                    if (invFlag == 0)
                        PIPLInovice();
                }
               
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('" + ex.Message + "');", true);
        }
      
    }
     
    protected void PIPLInovice()
    {


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

        string pName = txtCompName.Text;
        pName = pName.Replace("'", " ");

        string ADDRESS = txtAdd1.Text;
        ADDRESS = ADDRESS.Replace("'", " ");
        string pREFF = txtParty_Reff.Text;
        pREFF = pREFF.Replace("'", " ");

        string subName = txtSubParty.Text;
        subName = subName.Replace("'", " ");

        string ino = Session["InvNo"].ToString();
        Int32 invno = Convert.ToInt32(ino);
        string staxper = drServiceTax.SelectedValue;
        string suffix = txtSuffix.Text;
        string Notes = txtNote.Text;

        string subparty = ddlTallySubPartyName.SelectedItem.Text;
        if (subparty == "~Select~")
        {
            subparty = "";
        }
        string InvSeqNo = txtInvSeqNo.Text;
        SqlConnection conn = new SqlConnection(strImpex);
        string sqlQuery = " insert into M_IEC_InvoiceNew(invoice,invoiceDate,compName,Address1,address2,City,pincode,state," +
                          " phone,partyReff,jobNo,BLNo,BENoDate,importitem,Quantity,Ass_value,Container_no,Custom_Duty,subTotal,subTotalTax,staxPercent,Service_tax,Edu_Cess,SEC_Chess," +
                          " Grand_total,less_advance,Net_total,sub_party,Nettotal_words,invoiceType,invoiceNo,Mode,entryBy,eDate,fyear,TransportMode,suffix,notes,VGUID,BranchID,TallyAccountName,TallySubPartyName,InvSeqNo,SubPartyAddr) values('" + lblInvNo.Text + "','" + dates + "','" + pName + "'," +
                          " '" + ADDRESS + "','" + (string)Session["state"] + "','" + txtCity.Text + "','" + (string)Session["Pin"] + "','" + (string)Session["state"] + "','" + (string)Session["Phone"] + "','" + pREFF + "'," +
                          " '" + txtJobNo.Text + "','" + txtBLNo.Text + "','" + txtBENo.Text + " dt." + txtBEDate.Text + "','" + impItem + "','" + txtQty.Text + "'," +
                          " '" + txtAssValue.Text + "','" + txtNCNTR.Text + "','" + txtCustomDuty.Text + "'," + st + "," + stTax + ",'" + staxper + "'," + stax + "," + ec + "," + shc + "," +
                          " " + gt + "," + la + "," + nt + ",'" + subName + "','" + txtRupees.Text + "','" + InCode + "'," + invno + ",'" + (string)Session["RBMODE"] + "','" + (string)Session["USER-NAME"] + "'," +
                          "'" + EntryDate + "','" + (string)Session["FinancialYear"] + "','" + (string)Session["TransportMode"] + "','" + suffix + "','" + Notes + "','" + (string)Session["VGUID"] + "','" + (string)Session["BranchID"] + "','" + ddlTallyAccountName.SelectedItem.Text + "','" + subparty + "','" + InvSeqNo + "','" + txtSubPartyAddr.Text + "')";

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
                invoiceDTL(lblInvNo.Text);
                if (flag != 0)
                {
                    string strQuery1 = "Update T_JobCreation set status_job='Y', BENo = '" + txtBENo.Text + "',BEDate = '" + txtBEDate.Text + "',TotalAssVal = '" + txtAssValue.Text + "',TotalDuty = '" + txtCustomDuty.Text + "'  where JobNo='" + drJobNo.SelectedValue + "'";
                    GetCommandIMP(strQuery1);
                    updateRNO(invno, InCode,fyear);
                    try
                    {
                        string VchType = "SB";
                        InvSequence.InvSeqNOSave(txtCompName.Text, VchType);
                    }
                    catch
                    {
                    }

                
                    //string Query = "update ijob_pos set bill_no='" + lblInvNo.Text + "',bill_date='" + dates + "' where job_no='" + txtJobNo.Text + "'";
                    //GetCommand(Query, strconn1);
                   
                    invFlag = 1;
                    
                    Response.Write("<script>" + "alert('Invoice has successfully Generated');" + "</script>");

                    BtnStandard.Visible = false;
                    Submit.Enabled = false;
                    btnMail.Enabled = true;
                    preview.Enabled = true;
                 
                    balance1.Visible = false;
                   
                }
            }
           
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('" + ex.Message + "');", true);
        }
    }
    protected void invoiceDTL(string sbNo)
    {
        int count = 1;

        foreach (GridViewRow ROW in GridView1.Rows)
        {
            TextBox amt = (TextBox)ROW.FindControl("amt1");
            TextBox chrg = (TextBox)ROW.FindControl("txtDetails");
            TextBox recpt = (TextBox)ROW.FindControl("txtRecpt");
            TextBox Narrat = (TextBox)ROW.FindControl("txtNarration");
            DropDownList ServiceTaxPer = (DropDownList)ROW.FindControl("ddlStax");
            TextBox ServiceTaxAmt = (TextBox)ROW.FindControl("txtStaxAmt");
            string Narration = Narrat.Text;
            double ServiceTaxPercent = Convert.ToDouble(ServiceTaxPer.SelectedValue);
            double ServiceTaxAmount = Convert.ToDouble(ServiceTaxAmt.Text);
            //CheckBox chk = (CheckBox)ROW.FindControl("chkSTAX");
           
            string amount = amt.Text;
            string Charge_desc = chrg.Text;
            string Receipt = recpt.Text;
            string sTAXval = string.Empty;
            if (ServiceTaxPer.SelectedValue == "0")
            {
                sTAXval = "N";
            }
            else
            {
                sTAXval = "Y";
            }
            if (amount == "")
                amount = "0.00";
            if (amount != "0.00" && Charge_desc !="")
            {
                //if (chk.Checked)
                //    sTAXval = "Y";
                //else
                //    sTAXval = "N";
                string Query = "insert into T_iec_invoiceNew_DTL(invoice,sno,charge_desc,receipt,amount,serviceTax,Narration,ServiceTaxPercent,ServiceTaxAmount) " +
                                "values('" + sbNo + "'," + count + ",'" + Charge_desc.Replace("'", "") + "','" + Receipt.Replace("'", "") + "'," + amount + ",'" + sTAXval + "','" + Narration.Replace("'", "") + "','" + ServiceTaxPercent + "','" + ServiceTaxAmount + "')";
                GetCommandIMP(Query);
                count = count + 1;
                flag = 1;
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
    //    conn.Close();
    //}
    
    protected void updateRNO(int ino,string iType,string fy)
    {
        SqlConnection conn = new SqlConnection(strImpex);
        string sqlQuery = "update M_RunningNo set rno=" + ino + " where iecType = '" + iType + "' and fyear='" + fy + "'";
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
        Session["InvNo"] = lblInvNo.Text;
        
         string strQuery = "select * from M_iec_invoiceNew where invoice='" + lblInvNo.Text + "' and contr_code is null and particular1 is not null";
         SqlConnection conn = new SqlConnection(strImpex);

        SqlDataAdapter da = new SqlDataAdapter(strQuery, conn);
        DataSet ds = new DataSet();
        da.Fill(ds, "table");
        if (ds.Tables["table"].Rows.Count == 0)
            //Response.Redirect("../frmImpInvoiceReport.aspx");
            ScriptManager.RegisterStartupScript(this, this.GetType(), "popup", "window.open('../frmImpInvoiceReport.aspx','_blank','width=1120,height=720, menubar=no, scrollbars=yes, toolbar=no, location=no, resizable=no, left=10, top=120');", true);
            //ScriptManager.RegisterStartupScript(this, this.GetType(), "popup", "window.open('../Billing/CryInvoiceReportSTax.aspx','_blank','menubar=no, scrollbars=yes, toolbar=no, location=no, resizable=yes, left=20, top=20');", true);
        else
            ScriptManager.RegisterStartupScript(this, this.GetType(), "popup", "window.open('../frmImpInvoiceReport.aspx','_blank','width=1120,height=720, menubar=no, scrollbars=yes, toolbar=no, location=no, resizable=no, left=10, top=120');", true);
          //  Response.Redirect("../frmImpInvoiceReport.aspx");
            //ScriptManager.RegisterStartupScript(this, this.GetType(), "popup", "window.open('CryInvoiceReport.aspx','_blank','menubar=no, scrollbars=yes, toolbar=no, location=no, resizable=yes, left=20, top=20');", true);
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

            Response.Redirect("~/pimpex.aspx", false);//All one has to do is set the endResponse property of Response.Redirect to be false.
            
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
        //        MySqlConnection conn = new MySqlConnection(strconn1);
        //        string sqlQuery = "select *  from prt_addr where party_code='" + pcode + "' and addr_num=" + NO + "";
        //        MySqlDataAdapter da = new MySqlDataAdapter(sqlQuery, conn);
        //        DataSet ds = new DataSet();
        //        da.Fill(ds, "addr");
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
        //        Session["BranchID"] = row["ADDR_NUM"].ToString();
        //        txtAdd1.Text =addr1;
        //        Session["Phone"] = row["tel_no"].ToString();
        //    }
        //}
        // GrdADDRSCROLL.Visible=false;
        // GrdPaddr.Visible=false;
        // TrAddr.Visible=false;
        // Button1.Visible= trMain.Visible=true;
        // TrAddr1.Visible=false;
       
    }
    //public DataSet PartyAddr(string pcode)
    //{
    //    MySqlConnection conn = new MySqlConnection(strconn1);
    //    string sqlQuery4 = "select *  from prt_mast m,prt_addr a " +
    //                           "where m.party_code=a.party_code and  m.party_code='" + pcode + "' order by addr_num";
    //    MySqlDataAdapter da4 = new MySqlDataAdapter(sqlQuery4, conn);
    //    DataSet ds4 = new DataSet();
    //    da4.Fill(ds4, "prtMast");
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
          
            Gross = Gross + Convert.ToDouble(amt.Text);
            DropDownList ddlStax = (DropDownList)e.Row.FindControl("ddlStax");
            string strQuery = "select * from M_ServiceMaster where fyear='" + fyear + "' order by fyear desc";
            ddlStax.DataSource = GetDataSQL(strQuery);
            ddlStax.DataTextField = "sTax";
            ddlStax.DataValueField = "serviceTax";
            ddlStax.DataBind();
        }
        SubTotal.Text = Gross.ToString("#0.00#");
    }
    public DataSet GetCharge()
    {
        SqlConnection conn = new SqlConnection(strImpex);
        string Query = "select * from M_Charge";
        SqlDataAdapter da = new SqlDataAdapter(Query, conn);
        DataSet ds = new DataSet();
        da.Fill(ds, "table");
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
            //    if (chk.Checked)
            //    {
            //        GetServiceTax(cmp);
            //        Double amt0 = Convert.ToDouble(txt.Text);
            //        Double aStax = amt0 / 100 * vSTax;
            //        Double aECess = aStax / 100 * vECess;
            //        Double aSHECess = aStax / 100 * vSHECess;


            //        GrossTot = GrossTot + amt0;
            //        gSTAX = gSTAX + aStax;
            //        gECess = gECess + aECess;
            //        gSHECess = gSHECess + aSHECess;
            //    }
            //    else
            //    {

            //        Double tot = Convert.ToDouble(txt.Text);
            //        total = total + tot;
            //        txt.Text = tot.ToString("#0.00#");

            //    }
            //}

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
            //GetPERCENT();
        }

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
        vSHECess=SHEcess;
    }
    
    protected void drJobNo_TextChanged(object sender, EventArgs e)
    {
        GetFundRequest();
        string jno = drJobNo.SelectedValue;
        string jobNo = "";
        
           SqlConnection connM = new SqlConnection(strImpex);
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

                DataRowView rowM = dsM.Tables["iworkreg"].DefaultView[0];
                jobNo = rowM["jobno"].ToString();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('" + ex.Message + "');", true);
            }
        SqlConnection conn = new SqlConnection(strImpex);
        string Query = "select * from M_iec_invoicenew where jobno = '" + jobNo + "'";
        SqlDataAdapter da = new SqlDataAdapter(Query, conn);
        
            DataSet ds = new DataSet();
            da.Fill(ds, "bill");
            if (ds.Tables["bill"].Rows.Count != 0)
            {
               
                DataRowView row = ds.Tables["bill"].DefaultView[0];
                string eXISTbILL = row["invoice"].ToString();
               
                ScriptManager.RegisterStartupScript(this, this.GetType(), "confirm", "confirm('Given jobs has already billing. The Bill No. " + eXISTbILL + " . Do you want Continue...?');", true);
               
            }
            
        
    }

    public void GetFundRequest()
    {
        string query = "select jobno,RequestAmt,ApprovedAmt,PaymentAmt,PaymentStatus from T_FundRequest where jobno='" + drJobNo.SelectedValue + "'";
        SqlConnection con = new SqlConnection(strImpex);
        con.Open();
        DataSet ds = new DataSet();
        SqlDataAdapter da = new SqlDataAdapter(query, con);
        da.Fill(ds, "Fund");
        con.Close();
        if (ds.Tables["Fund"].Rows.Count != 0)
        {
            gvFundrequest.DataSource = ds;
            gvFundrequest.DataBind();
        }
    }



    protected void Button1_Click(object sender, EventArgs e)
    {
        Session["BasicInformation"] = drJobNo.SelectedValue + "~" + txtCompName.Text + "~" + txtJobNo.Text + "~" + txtSubParty.Text + "~" + (string)Session["BLNo"] + "~" + txtAdd1.Text + "~" + (string)Session["BENo"] + "~" + txtAdd1.Text + "~" + (string)Session["ImpotItem"] + "~" + txtCity.Text + "~" + (string)Session["QTY"] + "~" + (string)Session["state"] + "~" + (string)Session["Pin"] + "~" + txtAssValue.Text + "~" + (string)Session["Phone"] + "~" + txtNote.Text + "~" + txtParty_Reff.Text + "~" + (string)Session["CustomDuty"];
        Session["CompanyName"] = txtCompName.Text;
        ScriptManager.RegisterStartupScript(this, this.GetType(), "Error", "window.open('PopUp.aspx','_blank','width=600,height=250, menubar=no, scrollbars=yes, toolbar=no, location=no, resizable=yes, left=350, top=200, Right=200=, bottom=200');", true);
    
    }
   
  
   
    protected void drServiceTax_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetTransaction();
    }
    protected void chk_CheckedChanged(object sender, EventArgs e)
    {
        if (chk.Checked == true)
           fyear = (string)Session["Lfyear"];
        else
            fyear = (string)Session["FinancialYear"];
      
        drJobNo.DataSource = GetData(fyear);
        drJobNo.DataValueField = "jobno";
        drJobNo.DataTextField = "jobno";
        drJobNo.DataBind();
        drJobNo.Items.Insert(0, new ListItem("~Select~", "0"));
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
    protected void Menu2_MenuItemClick(object sender, MenuEventArgs e)
    {
       
    }


    protected void Button2_Click(object sender, EventArgs e)
    {
        ScriptManager.RegisterStartupScript(this, this.GetType(), "popup", "window.close();", true);

    }
    protected void btncalculate_Click(object sender, EventArgs e)
    {
        GetTransaction();
    }
    protected void txtSubParty_TextChanged(object sender, EventArgs e)
    {
        if (txtSubParty.Text != "")
        {
            ddlTallySubPartyName.Enabled = true;
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
            //txtimpRemark.Text = "Supplier Inv No :" + supplier.TrimStart(',');
        }
        else
        {
            //txtimpRemark.Text = "";
        }
    }

    protected void txtSubParty_TextChanged1(object sender, EventArgs e)
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