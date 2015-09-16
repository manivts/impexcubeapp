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
using MySql;
using MySql.Data.MySqlClient;
using System.IO;
using System.Drawing;

public partial class frmContractInvoiceATL : System.Web.UI.Page
{
    string strconnJSU = (string)ConfigurationManager.AppSettings["ConnectionJobStages"];

    string strconn1 = (string)ConfigurationManager.AppSettings["ConnectionVisual"];
    string strconn = (string)ConfigurationManager.AppSettings["ConnectionImpex"];
    Double Gross=0;
    Double total;
    Double bal;
    Double minVal;
    Double maxVal;
    Double Wt = 1;
    Double totalAmt;
    string CNTRNO = "";
    //DateTime blDate;
    string cUNIT = "";
    string cProduct = "";
//    int i = 0;
    DateTime blDate;
    DateTime hblDate;

    string strCharge = "";
    string pName = "";
    string MODE="IMP";
    Double vSTax;
    Double vECess;
    Double vSHECess;
    //Double Gross = 0;
    Double GrossTot = 0;
    //Double total;
    //Double bal;
    Double gSTAX;
    int invFlag =0;
    Double gECess;
    Double gSHECess;
    //string fyear = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack == false)
        {
            TallyAccountName();
            Session["VGUID"] = Guid.NewGuid().ToString();
            chkBills.Items[0].Selected = true;
            chkBills.Items[1].Selected = true;
            chkBills.Items[2].Selected = true;
            Session["PageLoad"] = null;
            //rbInvoice.SelectedValue = "SB";
            GenerateBillNo();
            //tdr1.Visible = false;
            //tdr2.Visible = false;
            //tdr3.Visible = false;
            Submit.Enabled = false;
            preview.Enabled = false;
            btnMail.Enabled = false;

            //btnSBT.Visible = false;
            string lfyear = (string)Session["Lfyear"];
            //GetXML();
            chk.Text = lfyear;
            //btnMail.Visible = false;

            lblUser.Text = (string)Session["USER-NAME"];
            Session["CustomerType"] = "Import";


            string strQuery = "select * from tbl_serviceMaster order by fyear desc";
            drServiceTax.DataSource = GetDataSQL(strQuery);
            drServiceTax.DataTextField = "sTax";
            drServiceTax.DataValueField = "serviceTax";
            drServiceTax.DataBind();
            string dates = DateTime.Now.ToString("dd/MM/yyyy");

            invDate.Text = dates;
            //lblDate.Text = System.DateTime.Now.ToString("dd-MMM-yyyy");
            //lblDate.Text = System.DateTime.Now.ToString("dd/MM/yyyy");
            //lblTime.Text = DateTime.Now.ToLongTimeString();
            if (lblUser.Text == "")
            {
                Response.Redirect("~/pimpex.aspx");
            }
            //preview.Visible=false;
            //preview0.Visible = false;
            Session["MAXVAL"] = "0";
            Session["OmaxVAL"] = "0";
            //trBill.Visible = false;
            GrdPaddr.Visible=false;
            TrAddr.Visible=false;
            TrAddr1.Visible=false;
         
            
            tblINV.Visible = true;
            tblContr.Visible = false;
            //string FY = (string)Session["FinancialYear"];
            string FY = (string)Session["FinancialYear"];
            Session["FY"] = FY;
            string Query = "select * from iworkreg where job_no like '%" + FY + "%' order by job_no";

            SqlConnection conn = new SqlConnection(strconn);
            string sqlQuery = "select  * from AppDetails";
            SqlDataAdapter da = new SqlDataAdapter(sqlQuery, conn);
            DataSet ds = new DataSet();
            da.Fill(ds, "name");

            if (ds.Tables["name"].Rows.Count != 0)
            {
                DataRowView row = ds.Tables["name"].DefaultView[0];

                llbHead.Text = row["companyname"].ToString();

            }
            try
            {
                drJobNo.DataSource = GetDataJNO(FY);
                drJobNo.DataValueField = "jobsno";
                drJobNo.DataTextField = "jobsno";
                drJobNo.DataBind();
                drJobNo.Items.Insert(0, new ListItem("~Select~", "0"));
                rbBill.SelectedValue = "DP";
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('" + ex.Message + "');", true);
            }

            ////if ((string)Session["PAGE"] != "")
            ////{
            ////    string Message = Session["PAGE"].ToString();
            ////    Response.Write("<script>alert('" + Message + "')</script>");
            ////}
            //txtPhone.Attributes.Add("onKeyUp", "javascript:return RestrictInt(this.value);");
            txtParty_Reff.Attributes.Add("onblur", "javascript:InvoiceValue();");

           // LessAd.Attributes.Add("onblur", "javascript:CallServiceTax();");
        }
       
      //  ServiceTax.Attributes.Add("onblur", "javascript:CallServiceTax();");
        
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
    public DataSet GetDataJNO(string fy)
    {
        MySqlConnection conn1 = new MySqlConnection(strconn1);
        conn1.Open();
        string sqlStatement1 = "select *  from iworkreg where job_no like '%" + fy + "%' and party_code='ATLI' order by jobsno";
        MySqlDataAdapter da1 = new MySqlDataAdapter(sqlStatement1, conn1);

        DataSet ds1 = new DataSet();
        da1.Fill(ds1, "ijobno");
        conn1.Close();
        return ds1;

    }
    protected void drJobNo_TextChanged(object sender, EventArgs e)
    {
        string jno = drJobNo.SelectedValue;
        string jobNo = "";

        MySqlConnection connM = new MySqlConnection(strconn1);
        connM.Open();
        string sqlQueryM = "";
        if (chk.Checked == true)
            sqlQueryM = "select *  from iworkreg where jobsno='" + jno + "' and job_no like '%" + (string)Session["Lfyear"] + "%'";
        else
        sqlQueryM = "select *  from iworkreg where jobsno='" + jno + "' and job_no like '%" + (string)Session["FinancialYear"] + "%'";

        MySqlDataAdapter daM = new MySqlDataAdapter(sqlQueryM, connM);
        try
        {
            DataSet dsM = new DataSet();
            daM.Fill(dsM, "iworkreg");
            connM.Close();
            DataRowView rowM = dsM.Tables["iworkreg"].DefaultView[0];
            jobNo = rowM["job_no"].ToString();
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('" + ex.Message + "');", true);
        }

        string BILL = "SB";
       // string bType = "";
        string Query="";
       

        SqlConnection conn = new SqlConnection(strconn);
         if (BILL == "SB")
              Query = "select * from iec_invoicenew where jobno = '" + jobNo + "'";
        else
             Query = "select * from iec_debit where jobno = '" + jobNo + "'";
        SqlDataAdapter da = new SqlDataAdapter(Query, conn);

        DataSet ds = new DataSet();
        da.Fill(ds, "bill");
        if (ds.Tables["bill"].Rows.Count != 0)
        {
            
            //BtnStandard.Enabled = false;
            DataRowView row = ds.Tables["bill"].DefaultView[0];
            string eXISTbILL = row["invoice"].ToString();
            //ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Given jobs has already billing. The Bill No. " + eXISTbILL + "');", true);
            //drJobNo.Attributes.Add("onclientclick", "javascript:return are you sure want to continue ???");
            ScriptManager.RegisterStartupScript(this, this.GetType(), "confirm", "confirm('Given jobs has already billing. The Bill No. " + eXISTbILL + " . Do you want Continue...?');", true);
            //ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "javascript:if(confirm('Are you sure you want to delete?') == false) return false;", true); 

            //if (Page.Request["confirm"] == "OK")
            //BtnStandard.Enabled = false;
            //ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "javascript:if(confirm('Sucessfully Saved') == false) return false;", true);
            //ScriptManager.RegisterClientScriptBlock(this,this.GetType(),"confirm","return confirm('Are you sure');",true);
            //BtnStandard.Enabled = true;
            //ClientScriptManager csm = Page.ClientScript;
            //csm.RegisterStartupScript(typeof(Page), "Info", "alert('You can't delete the table.The table details in Purchase Order table')", false);
        }


    }
    public DataSet GetData(string Query)
    {
        MySqlConnection conn = new MySqlConnection(strconn1);
        conn.Open();
        MySqlDataAdapter da = new MySqlDataAdapter(Query, conn);
        DataSet ds = new DataSet();
        da.Fill(ds, "MySQLtable");
        conn.Close();
        return ds;
    }
    public DataSet GetDataSQL(string Query)
    {
        SqlConnection conn = new SqlConnection(strconn);
      
        SqlDataAdapter da = new SqlDataAdapter(Query, conn);
        DataSet ds = new DataSet();
        da.Fill(ds, "SQLtable");
       
        return ds;
    }
   
    protected void drCustomer_SelectedIndexChanged(object sender, EventArgs e)
    {
        //string fy = "2006-2008";
        //string pCode = drCustomer.SelectedValue;
        //string shpType = rbType.SelectedValue;
        //if (shpType == "")
        //    shpType = "A";
        //else if (shpType != "A")
        //    shpType = "S";
        //string Query = "select * from iworkreg where party_code='" + pCode + "' and transport_mode='" + shpType + "'";
        //drJobNo.DataSource = GetData(Query);
        //drJobNo.DataTextField = "job_no";
        //drJobNo.DataValueField = "job_no";
        //drJobNo.DataBind();
        //drJobNo.Items.Insert(0, new ListItem("~select~", "0"));

    }
    protected void BtnStandard_Click(object sender, EventArgs e)
    {
        TallyAccountName();
        Session["Company"] = "Std";
      //strCName = txtCompName.Text;
        string jno = drJobNo.SelectedValue;
        string Bill = "SB";
        Session["BILL"] = Bill;
        string FY = (string)Session["FinancialYear"];
        

        if (jno == "0")
        {
            Response.Write("<script>alert('Select Job Number')</script>");
        }
        else
        {
            try
            {

                MySqlConnection conn = new MySqlConnection(strconn1);
                conn.Open();
                string sqlQuery = "";
                if (chk.Checked == true)
                    sqlQuery = "select *  from iworkreg where jobsno='" + jno + "' and job_no like '%" + (string)Session["Lfyear"] + "%'";
                else
                    sqlQuery = "select *  from iworkreg where jobsno='" + jno + "' and job_no like '%" + (string)Session["FinancialYear"] + "%'";
                MySqlDataAdapter da = new MySqlDataAdapter(sqlQuery, conn);
                DataSet ds = new DataSet();
                da.Fill(ds, "iworkreg");

                if (ds.Tables["iworkreg"].Rows.Count != 0)
                {
                    //Submit.Enabled = true;
                    DataRowView row = ds.Tables["iworkreg"].DefaultView[0];
                    string jobNo = row["job_no"].ToString();
                    txtJobNo.Text = jobNo;
                    //txtJNO.Text = jobNo;
                    txtAssValue.Text = row["tot_ass_vl"].ToString();
                    txtCustomDuty.Text = row["tot_duty"].ToString();

                    string item = row["inv_dtl"].ToString();
                    item = item.Replace("'", " ");
                    txtImpotItem.Text = item;

                    string pcode = row["party_code"].ToString();
                    string paddr = row["party_addr"].ToString();
                    Session["pin"] = pcode;
                    Session["PCODE"] = pcode;
                    Session["PBranch"] = paddr;

                    //  Get Party Address
                    GrdPaddr.DataSource = PartyAddr(pcode);
                    GrdPaddr.DataBind();

                    string shpType = row["transport_mode"].ToString();
                    Session["TransportMode"] = shpType;
                    if (shpType == "A")
                    {
                        shpType = "AIR";
                        Session["shpType"] = shpType;
                    }
                    else
                    {
                        shpType = "SEA";
                        Session["shpType"] = shpType;
                    }
                    string sqlQuery1 = "select *  from ishp_dtl where job_no='" + jobNo + "'";
                    MySqlDataAdapter da1 = new MySqlDataAdapter(sqlQuery1, conn);
                    DataSet ds1 = new DataSet();
                    da1.Fill(ds1, "ishp");
                    conn.Close();
                    if (ds1.Tables["ishp"].Rows.Count == 0)
                        Response.Write("<script>alert('There is no Shipment Details for Given Jobs')</script>");
                    else
                    {
                        DataRowView row1 = ds1.Tables["ishp"].DefaultView[0];
                        string bl = row1["mawb_no"].ToString();
                        string BLDate = row1["mawb_date"].ToString();

                        string hbl = row1["hawb_no"].ToString();
                        string HBLDate = row1["hawb_date"].ToString();

                        if (BLDate != "" || BLDate != string.Empty)
                            blDate = Convert.ToDateTime(BLDate);
                        if (HBLDate != "" || HBLDate != string.Empty)
                            hblDate = Convert.ToDateTime(HBLDate);
                        if (BLDate != "" || BLDate != string.Empty)
                            txtBLNo.Text = bl + " dt." + blDate.ToString("dd/MM/yyyy");
                        else
                            txtBLNo.Text = hbl + " dt." + hblDate.ToString("dd/MM/yyyy");
                        string pkg = row1["no_of_pkg"].ToString();
                        string pkg_unit = row1["pkg_unit"].ToString();
                        string gross = row1["gross_wt"].ToString();
                        string gross_unit = row1["gross_unit"].ToString();
                        Session["GRossWT"] = gross;
                        pkg = pkg.Replace(".000", "");
                        gross = gross.Replace(".000", "");
                        //txtBLNo.Text = bl + " dt." + blDate.ToString("dd/MM/yyyy");
                        txtQty.Text = pkg + " " + pkg_unit + "/" + gross + " " + gross_unit;
                    }
                    string sqlQuery2 = "select *  from ijob_Pos where job_no='" + jobNo + "'";
                    MySqlDataAdapter da2 = new MySqlDataAdapter(sqlQuery2, conn);
                    DataSet ds2 = new DataSet();
                    da2.Fill(ds2, "ijobs");
                    conn.Close();
                    DataRowView row2 = ds2.Tables["ijobs"].DefaultView[0];
                    string be = row2["be_no"].ToString();
                    string bedate = row2["be_date"].ToString();
                    if (bedate == "")
                    {
                        txtBENo.Text = be + " dt." + bedate;
                    }
                    else
                    {
                        DateTime beDate = Convert.ToDateTime(bedate);

                        txtBENo.Text = be + " dt." + beDate.ToString("dd/MM/yyyy");
                    }

                    string sqlQueryQTY = "select count(job_no) as QTY  from impcontdet where job_no='" + jobNo + "'";
                    MySqlDataAdapter daQTY = new MySqlDataAdapter(sqlQueryQTY, conn);
                    DataSet dsQTY = new DataSet();
                    daQTY.Fill(dsQTY, "iContrQTY");
                    conn.Close();
                    if (dsQTY.Tables["iContrQTY"].Rows.Count != 0)
                    {
                        DataRowView rowQTY = dsQTY.Tables["iContrQTY"].DefaultView[0];
                        string QTY = rowQTY["QTY"].ToString();
                        Session["QTY"] = QTY;
                    }
                    string sqlQuery3 = "select *  from impcontdet where job_no='" + jobNo + "' order by sr_no";
                    MySqlDataAdapter da3 = new MySqlDataAdapter(sqlQuery3, conn);
                    DataSet ds3 = new DataSet();
                    da3.Fill(ds3, "iContr");
                    conn.Close();
                    // stype changes on 19.05.2012
                    //start  stype
                    if (ds3.Tables["iContr"].Rows.Count != 0)
                    {
                        DataTable dt3 = ds3.Tables[0];
                        string cno = "";
                        string cTyp = "";
                        string cSize = "";
                        string snos = "";
                        foreach (DataRow row3 in dt3.Rows)
                        {
                            snos = row3["sr_no"].ToString();
                            cno = row3["cont_no"].ToString();
                            cTyp = row3["cont_type"].ToString();
                            cSize = row3["cont_size"].ToString();
                            CNTRNO = CNTRNO + cno + ",";
                        }
                        txtNote.Text = CNTRNO.TrimEnd(',');
                        //ContNo.Text = cno;
                        string pref = "";
                        //if (cTyp == "FCL")
                        //{
                            pref =snos + "x" + cSize + " Ft - " + cTyp;
                        //}
                        //else
                        //    pref = cTyp;
                        //lblIName.Text = "INVOICE - IMPORTS" + " - SEA SHIPMENT " + pref;
                        txtNCNTR.Text = pref;
                        Session["Contr_size"]=cSize;
                        Session["Contr_Type"] = cTyp;
                        //ContNo.Text = cno;
                    }
                    //end stype
                    else
                    {
                        Session["Contr_size"] = "";
                        Session["Contr_Type"] = "";
                    }

                    //start Direct party info
                    string sqlQuery4 = "select *  from prt_mast m,prt_addr a " +
                                       "where m.party_code=a.party_code and  m.party_code='" + pcode + "' and a.addr_code='" + paddr + "'";
                    MySqlDataAdapter da4 = new MySqlDataAdapter(sqlQuery4, conn);
                    DataSet ds4 = new DataSet();
                    da4.Fill(ds4, "prtMast");
                    conn.Close();
                    DataRowView row4 = ds4.Tables["prtMast"].DefaultView[0];
                    string cCode = row4["group_id"].ToString();
                    Session["cCode"] = cCode;
                    if (cCode == "")
                    {
                        txtCompName.Text = row4["party_name"].ToString();
                        Session["pName"] = row4["party_name"].ToString();

                        pName = txtCompName.Text;
                        GridView3.Visible = false;
                        BtnContract_Submit.Visible = false;
                        lblContr.Text = "CONTRACT INFORMATION FOR " + pName.ToUpper();

                        //Session["IECName"] = row["iec_name"].ToString();
                        string addr1 = row4["address"].ToString();
                        string city = row4["city"].ToString();
                        string pin = row4["pin"].ToString();
                        addr1 = addr1.Replace("'", " ");
                        Session["addr"] = addr1;
                        Session["Pin"] = pin;
                        txtCity.Text = city;
                        Session["state"] = row4["state"].ToString();
                        //Session["state"] = txtAdd2.Text;
                        Session["Phone"] = row4["tel_no"].ToString();
                        TrAddr.Visible = true;
                        TrAddr1.Visible = true;
                        GrdADDRSCROLL.Visible = true;
                        GrdPaddr.Visible = true;
                    }
                    else
                    {
                        //Third party Addr
                        trBill.Visible = true;
                        txtSubParty.Text = row4["party_name"].ToString();
                        SqlConnection connCT = new SqlConnection(strconn);
                        string QueryCT = "select * from contract_mst cm,contract_addr cs  " +
                                         " where cm.customer_code=cs.customer_code and cm.customer_code='" + cCode + "'";
                        SqlDataAdapter daCT = new SqlDataAdapter(QueryCT, connCT);
                        DataSet dsCT = new DataSet();
                        daCT.Fill(dsCT, "Contrst");
                        if (dsCT.Tables["Contrst"].Rows.Count == 0)
                        {

                            txtCompName.Text = row4["party_name"].ToString();
                            Session["pName"] = row4["party_name"].ToString();

                            pName = txtCompName.Text;
                            GridView3.Visible = false;
                            BtnContract_Submit.Visible = false;
                            lblContr.Text = "CONTRACT INFORMATION FOR " + pName.ToUpper();

                            //Session["IECName"] = row["iec_name"].ToString();
                            string addr1 = row4["address"].ToString();
                            string city = row4["city"].ToString();
                            string pin = row4["pin"].ToString();
                            Session["addr"] = addr1;
                            Session["pin"] = pin;
                            txtCity.Text = city;
                            //txtPin.Text = pin;
                            Session["state"] = row4["state"].ToString();
                            //Session["state"] = txtAdd2.Text;
                            Session["Phone"] = row4["tel_no"].ToString();
                            TrAddr.Visible = true;
                            TrAddr1.Visible = true;
                            GrdADDRSCROLL.Visible = true;
                            GrdPaddr.Visible = true;
                        }
                        else
                        {
                            DataRowView rowCT = dsCT.Tables["Contrst"].DefaultView[0];

                            txtCompName.Text = rowCT["customer_name"].ToString();
                            Session["pName"] = rowCT["customer_name"].ToString();
                            pName = txtCompName.Text;

                            GridView3.Visible = false;
                            BtnContract_Submit.Visible = false;
                            lblContr.Text = "CONTRACT INFORMATION FOR " + pName.ToUpper();

                            string addr1 = rowCT["address"].ToString();
                            string city = rowCT["city"].ToString();
                            string pin = rowCT["pin"].ToString();
                            Session["addr"] = addr1;
                            Session["Pin"] = pin;
                            txtCity.Text = city;
                            //txtPin.Text = pin;
                            Session["state"] = rowCT["state"].ToString();
                            //Session["state"] = txtAdd2.Text;
                            Session["Phone"] = rowCT["tel_no"].ToString();
                        }


                    }
                    //end Direct party Info
                    int ii = 0;
                    int j = 0;
                    int k = 1;

                    string addr = Session["addr"].ToString();
                    string[] strCC = addr.Split(',');
                    foreach (string strThisCC in strCC)
                    {
                        ii = ii + 1;


                    }
                    j = ii / 2;
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
                    //txtAddr.Text = ADDRS;
                    txtAdd1.Text = addr;
                    //Start contract info     
                    // To Get Contract information for the selected Customers....
                    string status = "ACTIVE";
                    //  string pName = drCustomer.SelectedItem.Text;

                    tblINV.Visible = false;
                    tblContr.Visible = true;
                    string strQuery1 = "select * from contract_mst where customer_name='" + pName + "' and contr_status='" + status + "'";

                    GridView2.DataSource = GetDataSQL(strQuery1);
                    GridView2.DataBind();

                    //GrdPaddr.Visible = true;
                    // GridView2.Visible = false;
                    GridView3.Visible = false;
                    GridScroll.Visible = false;
                    // GetContractInfo(strQuery);
                }

                else
                {
                    Response.Write("<script>alert('Not Found Records')</script>");
                    tblINV.Visible = true;
                    tblContr.Visible = false;
                }


            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('" + ex.Message + "');", true);
            }
            

    //End Contract info
        }
    }

    public void TallyAccountName()
    {
        SqlConnection con = new SqlConnection(strconn);
        con.Open();
        string query = "select AccountCode, AccountName from AccountMaster where Acc_group = 'Sundry Debtors' ";
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

    protected void ServiceTax_TextChanged(object sender, EventArgs e)
    {
        //ServiceTax.Attributes.Add("onblur", "javascript:CallServiceTax();");
      // GetPERCENT();

    }
    protected void Submit_Click(object sender, EventArgs e)
    {
        //if (ddlTallyAccountName.SelectedItem.Text != "~Select~")
        //{
        //    if (txtSubParty.Text != "")
        //    {
        //        if (ddlTallySubPartyName.SelectedItem.Text != "~Select~")
        //        {
        GetTransaction();
      //  string pName = drCustomer.SelectedItem.Text;
        string fy = (string)Session["FinancialYear"];
        //string btable = "";
        //trBillNO.Visible = true;
        string jobNo = txtJobNo.Text;
        string BILL = "SB";
        string bTp = "";
        string strQuery="";
        if (BILL == "SB")
        {
            bTp = "ATLSB";
            //btable = "iec_invoicenew";
            strQuery = "select * from iec_rno where iectype='" + bTp + "' and Fyear='" + fy + "'";
            // bType = "Invoice";
        }
        else
        {
            bTp = "ATLDB";
            //btable = "iec_debit";
            strQuery = "select * from iec_rno where iectype='" + bTp + "' and Fyear='" + fy + "'";
            // bType = "Debit Note";
        }
            SqlConnection cnn = new SqlConnection(strconn);
            cnn.Open();
            SqlDataAdapter da = new SqlDataAdapter(strQuery, cnn);
            DataSet ds = new DataSet();
            da.Fill(ds, "Contract");
            cnn.Close();
            if (ds.Tables["Contract"].Rows.Count != 0)
            {
                DataRowView row = ds.Tables["Contract"].DefaultView[0];
                Int32 INO = Convert.ToInt32(row["rno"].ToString());
                string invCode = row["iecCode"].ToString();
                Session["InCode"] = invCode;
                Session["InvNo"] = INO + 1;
                if (invFlag == 0)
                   PIPLInovice();
                   
            }
            else
                Response.Write("<script>alert('Invoice has not Found for Given Financial Year')</script>");
        //        }
        //        else
        //        {
        //            Response.Write("<script>alert('Please Select Tally Sub Party Name')</script>");
        //        }
        //    }
        //    else
        //    {
        //        GetTransaction();
        //        //  string pName = drCustomer.SelectedItem.Text;
        //        string fy = (string)Session["FinancialYear"];
        //        //string btable = "";
        //        //trBillNO.Visible = true;
        //        string jobNo = txtJobNo.Text;
        //        string BILL = "SB";
        //        string bTp = "";
        //        string strQuery = "";
        //        if (BILL == "SB")
        //        {
        //            bTp = "ATLSB";
        //            //btable = "iec_invoicenew";
        //            strQuery = "select * from iec_rno where iectype='" + bTp + "' and Fyear='" + fy + "'";
        //            // bType = "Invoice";
        //        }
        //        else
        //        {
        //            bTp = "ATLDB";
        //            //btable = "iec_debit";
        //            strQuery = "select * from iec_rno where iectype='" + bTp + "' and Fyear='" + fy + "'";
        //            // bType = "Debit Note";
        //        }
        //        SqlConnection cnn = new SqlConnection(strconn);
        //        cnn.Open();
        //        SqlDataAdapter da = new SqlDataAdapter(strQuery, cnn);
        //        DataSet ds = new DataSet();
        //        da.Fill(ds, "Contract");
        //        cnn.Close();
        //        if (ds.Tables["Contract"].Rows.Count != 0)
        //        {
        //            DataRowView row = ds.Tables["Contract"].DefaultView[0];
        //            Int32 INO = Convert.ToInt32(row["rno"].ToString());
        //            string invCode = row["iecCode"].ToString();
        //            Session["InCode"] = invCode;
        //            Session["InvNo"] = INO + 1;
        //            if (invFlag == 0)
        //                PIPLInovice();

        //        }

        //        else
        //            Response.Write("<script>alert('Invoice has not Found for Given Financial Year')</script>");
        //    }

        //}
        //else
        //{
        //    Response.Write("<script>alert('Please Select Tally Account Name')</script>");
        //}

          
        
    }
    protected void CreateDebitNote(string sbNo, string dates, string InCode, int invno, string MODE, string CID, string EntryDate)
    {
        string sqlQuery = " insert into IEC_debit(invoice,invoiceDate,compName,Address1,address2,City,pincode,state," +
                         " phone,partyReff,jobNo,BLNo,BENoDate,importitem,Quantity,Ass_value,Container_no,Custom_Duty," +
                         " sub_party,invoiceType,invoiceNo,Mode,contr_code,entryBy,eDate,fyear,TransportMode,notes,VGUID,BranchID) values('" + sbNo + "','" + dates + "','" + txtCompName.Text + "'," +
                         " '" + txtAdd1.Text + "','" + Session["state"] + "','" + txtCity.Text + "','" + Session["pin"] + "','" + Session["state"] + "','" + Session["Phone"] + "','" + txtParty_Reff.Text + "'," +
                         " '" + txtJobNo.Text + "','" + txtBLNo.Text + "','" + txtBENo.Text + "','" + txtImpotItem.Text + "','" + txtQty.Text + "'," +
                         " '" + txtAssValue.Text + "','" + txtNCNTR.Text + "','" + txtCustomDuty.Text + "','" + txtSubParty.Text + "'," +
                         "'" + InCode + "'," + invno + ",'" + MODE + "','" + CID + "','" + lblUser.Text + "','" + EntryDate + "','" + (string)Session["FinancialYear"] + "'," +
                         "'" + (string)Session["TransportMode"] + "','" + txtNote.Text + "','" + (string)Session["VGUID"] + "','" + (string)Session["BranchID"] + "')";
       
        string sqlQueryDtl = "insert into IEC_debit_dtl(invoice,sno,serviceTax) values('" + sbNo + "','1','N')";

        GetCommand(sqlQuery);
        GetCommand(sqlQueryDtl);
    }
    protected void GetRNO(string strQuery)
    {
        SqlConnection cnn = new SqlConnection(strconn);
        cnn.Open();
            SqlDataAdapter da = new SqlDataAdapter(strQuery, cnn);
            DataSet ds = new DataSet();
            da.Fill(ds, "Running");
            cnn.Close();
            if (ds.Tables["Running"].Rows.Count != 0)
            {
                DataRowView row = ds.Tables["Running"].DefaultView[0];
                Int32 INO = Convert.ToInt32(row["rno"].ToString());
                INO = INO + 1;
                Session["RNOSS"] = "C/" + Convert.ToString(INO);
                Session["rnos"] = INO;

            }
    }
    protected void PIPLInovice()
    {

        string dates = invDate.Text;
        string[] DT = dates.Split('/');
        dates = DT[2] + "-" + DT[1] + "-" + DT[0];

        string EntryDate = System.DateTime.Now.ToString("yyyy-MM-dd");
        string sbNo = "";
        string InCode = "";
        string Bill_Mst= "";
        string Bill_Dtl = "";
        string cmp = (string)Session["CMP"];
       // double pst = Convert.ToDouble(subPaidTotal.Text);
        double st = Convert.ToDouble(SubTotal.Text);
        double stTax = Convert.ToDouble(SubTotalTax.Text);
        GetServiceTax(cmp);
        double staxp = Convert.ToDouble(vSTax);
        double stax = Convert.ToDouble(sTax.Text);
        double ec = Convert.ToDouble(EdCess.Text);
        double shc = Convert.ToDouble(SHCess.Text);
        double gt = Convert.ToDouble(Totals.Text);
        double la = Convert.ToDouble(LessAd.Text);
        double nt = Convert.ToDouble(BalanceDue.Text);
        string ino = Session["InvNo"].ToString();
        Int32 invno = Convert.ToInt32(ino);
        string suffix = txtSuffix.Text;
        string Notes = txtNote.Text;
        string BILL = "SB";


        string impItem = txtImpotItem.Text;
        impItem = impItem.Replace("'", " ");
        txtImpotItem.Text = impItem;

        string ADDRESS = txtAdd1.Text;
        ADDRESS = ADDRESS.Replace("'", " ");
        txtAdd1.Text = ADDRESS;
        string pREFF = txtParty_Reff.Text;
        pREFF = pREFF.Replace("'", " ");
        txtParty_Reff.Text = pREFF;

        string impRK = txtimpRemark.Text;
        string intRK = txtIndentRemark.Text;
        impRK = impRK.Replace("'", " ");
        intRK = intRK.Replace("'", " ");

        sbNo = (string)Session["InCode"] + "/" + invno;

        Session["BILLTYPE"] = BILL;
        if (BILL == "SB")
        {
            Bill_Mst = "IEC_InvoiceNew";
            Bill_Dtl = "iec_invoiceNew_DTL";
            //sbNo = "C/" + invno;
            InCode = "ATLSB";
            
        }
        //else
        //{
        //    Bill_Mst = "IEC_debit";
        //    Bill_Dtl = "iec_debit_DTL";
        //    sbNo = "C/" + invno;
        //    InCode = "ATLDB";
        //}
        string subparty = ddlTallySubPartyName.SelectedItem.Text;
        if (subparty == "~Select~")
        {
            subparty = "";
        }
        string CID = (string)Session["ContractID"];
       
        string sqlQuery = " insert into " + Bill_Mst + "(invoice,invoiceDate,compName,Address1,address2,City,pincode,state," +
                          " phone,partyReff,jobNo,BLNo,BENoDate,importitem,Quantity,Ass_value,Container_no,Custom_Duty," +
                          " subTotal,subtotalTax,STaxPercent,service_Tax,edu_cess,sec_chess," +
                          " Grand_total,less_advance,Net_total,sub_party,Nettotal_words,invoiceType,invoiceNo,Mode,contr_code,entryBy,eDate,fyear,TransportMode,suffix,notes,impRemark,interRemark,BranchID,VGUID,TallyAccountName,TallySubPartyName) values('" + sbNo + "','" + dates + "','" + txtCompName.Text + "'," +
                          " '" + txtAdd1.Text + "','" + Session["state"] + "','" + txtCity.Text + "','" + Session["pin"] + "','" + Session["state"] + "','" + Session["Phone"] + "','" + txtParty_Reff.Text + "'," +
                          " '" + txtJobNo.Text + "','" + txtBLNo.Text + "','" + txtBENo.Text + "','" + txtImpotItem.Text + "','" + txtQty.Text + "'," +
                          " '" + txtAssValue.Text + "','" + txtNCNTR.Text + "','" + txtCustomDuty.Text + "'," + st + "," + stTax + "," + staxp + "," +
                          " " + stax + "," + ec + "," + shc + "," + gt + "," + la + "," + nt + ",'" + txtSubParty.Text + "','" + txtRupees.Text + "'," +
                          "'" + InCode + "'," + invno + ",'" + MODE + "','" + CID + "','" + lblUser.Text + "','" + EntryDate + "','" + (string)Session["FinancialYear"] + "'," +
                          "'" + (string)Session["TransportMode"] + "','" + suffix + "','" + Notes + "','" + impRK + "','" + intRK + "','" + (string)Session["BranchID"] + "','" + (string)Session["VGUID"] + "','" + ddlTallyAccountName.SelectedItem.Text + "','" + subparty + "')";

        

            GetCommand(sqlQuery);

            int count = 1;
            foreach (GridViewRow ROW in GridView1.Rows)
            {
                TextBox amt = (TextBox)ROW.FindControl("amt1");
                TextBox receipt = (TextBox)ROW.FindControl("txtRECPT");
                CheckBox chk = (CheckBox)ROW.FindControl("chkSTAX");
                // amt.Attributes.Add("onblur", "javascript:Totalcalculate();");
                string amount = amt.Text;
                string sTAXval = "";
                string receiptVal = receipt.Text;
                string Charge_desc = ROW.Cells[2].Text;
                if (amount == "")
                    amount = "0.00";
                if (chk.Checked)
                    sTAXval = "Y";
                else
                    sTAXval = "N";
                if(amount!="0.00")
                {
                    string Query = "insert into " + Bill_Dtl + "(invoice,sno,charge_desc,receipt,amount,serviceTax) " +
                                   "values('" + sbNo + "'," + count + ",'" + Charge_desc + "','" + receiptVal + "'," + amount + ",'" + sTAXval + "')";
                    GetCommand(Query);
                    count = count + 1;
                }
                
            }
            if (InCode == "ATLSB")
            {
                string iCode = "";
                //string fy = (string)Session["FinancialYear"];
                string fyear = (string)Session["FinancialYear"];
                Session["invNo"] = invno;
                Session["inCode"] = InCode;


                Session["INVOICECTR"] = sbNo;
                string Query = "update ijob_pos set bill_no='" + sbNo + "',bill_date='" + dates + "' where job_no='" + txtJobNo.Text + "'";
                GetCommandMy(Query, strconn1);
                //GetCommandMy(Query, strconnJSU);
                //lblMess.Text = "Invoice has successfully Generated";
                Response.Write("<script>" + "alert('Invoice has successfully Generated');" + "</script>");
                if (chkBills.Items[1].Selected)
                {
                    //To Create Debit Note & Extra Bills for Apollo Tyres
                    iCode = "ATLDB";
                    //btable = "iec_debit";
                    string strQuery = "select * from iec_rno where iectype='" + iCode + "' and Fyear='" + fyear + "'";
                    GetRNO(strQuery);
                    sbNo = (string)Session["RNOSS"];
                    invno = (int)Session["rnos"];
                    CreateDebitNote(sbNo, dates, iCode, invno, MODE, CID, EntryDate);
                    //string fy = (string)Session["FinancialYear"];
                    updateRNO(invno, iCode, fyear);
                    string Query1 = "update ijob_pos set db_note_no='" + sbNo + "',db_date='" + dates + "' where job_no='" + txtJobNo.Text + "'";
                    GetCommandMy(Query1, strconn1);
                }
                else
                {
                    iCode = "ATLDB";
                    //btable = "iec_debit";
                    string strQuery = "select * from iec_rno where iectype='" + iCode + "' and Fyear='" + fyear + "'";
                    GetRNO(strQuery);
                    invno = (int)Session["rnos"];
                    updateRNO(invno, iCode, fyear);
                }
                //ATL Demurrage 
                if (chkBills.Items[2].Selected)
                {
                    iCode = "ATLDEM";
                    //btable = "iec_debit";
                    string strQuery = "select * from iec_rno where iectype='" + iCode + "' and Fyear='" + fyear + "'";
                    GetRNO(strQuery);
                    sbNo = (string)Session["RNOSS"];
                    invno = (int)Session["rnos"];
                    CreateDebitNote(sbNo, dates, iCode, invno, MODE, CID, EntryDate);
                    //string fy = (string)Session["FinancialYear"];
                    updateRNO(invno, iCode, fyear);
                }
                else
                {
                    iCode = "ATLDEM";
                    //btable = "iec_debit";
                    string strQuery = "select * from iec_rno where iectype='" + iCode + "' and Fyear='" + fyear + "'";
                    GetRNO(strQuery);
                    invno = (int)Session["rnos"];
                    updateRNO(invno, iCode, fyear);
                }

                //ATLSB Running No Updates
                invno =(int)Session["invNo"];
                InCode=(string)Session["inCode"]; 
                updateRNO(invno, InCode, fyear);
            }
            else
            {
                string Query = "update ijob_pos set db_note_no='" + sbNo + "',db_date='" + dates + "' where job_no='" + txtJobNo.Text + "'";
                GetCommandMy(Query, strconn1);
                //GetCommandMy(Query, strconnJSU);
                //lblMess.Text = "Debit Note has successfully Generated";
                Response.Write("<script>" + "alert('Debit Note has successfully Generated');" + "</script>");
            }
          // update Bill status
            string blQuery = "update impjobstage set date='" + dates + "' where job_no='" + txtJobNo.Text + "' and job_stage='BDate'";
            string blQueryJSU = "update iworkreg_dtl set date='" + dates + "' where job_no='" + txtJobNo.Text + "' and job_stage='BDate'";

            string billstatus = "update iworkreg_jobstatus set bill_no='" + sbNo + "',bill_date='" + dates + "',bill_amt='" + nt + "',status_job='Y' where job_no='" + txtJobNo.Text + "'";

            GetCommandMy(billstatus, strconnJSU);

            GetCommandMy(blQuery, strconn1);
            //GetCommandMy(blQueryJSU, strconnJSU);
            //tdr1.Visible = true  ;
            //tdr2.Visible = true;
            //tdr3.Visible = true;
            // BtnPrint.Visible = true;
            invFlag = 1;
           
            BtnStandard.Visible = false;
            balance1.Visible = false;
           
            
            lblBill.Text = sbNo;
       
           
            //preview0.Visible = true;

            Submit.Enabled = false;
            preview.Enabled = true;
            btnMail.Enabled = true;
        
    }
    protected void updateRNO(int ino, string iType,string fy)
    {
        SqlConnection conn = new SqlConnection(strconn);
        conn.Open();
        string sqlQuery = "update iec_rno set rno=" + ino + " where iecType='" + iType + "' and fyear='" + fy + "'";
      
        SqlCommand cmd = new SqlCommand(sqlQuery, conn);
        SqlDataAdapter da = new SqlDataAdapter();
        cmd.CommandText = sqlQuery;
        cmd.Connection = conn;
        da.SelectCommand = cmd;
      
        int result = cmd.ExecuteNonQuery();
    }
    protected void GetCommandMy(string Query, string connSTR)
    {

        MySqlConnection conn = new MySqlConnection(connSTR);
        conn.Open();
        MySqlCommand cmd = new MySqlCommand(Query, conn);
        cmd.CommandText = Query;
        cmd.Connection = conn;
        int res = cmd.ExecuteNonQuery();
      
    }
    protected void GetCommand(string sqlQuery)
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
    protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (Session["PageLoad"] != null)
        {
            string SB = "";
            string DB = "";
            string Amount = "";
            Double ass;
            Double AMT;
            string CUnits = "";
            string CID = (string)Session["ContractID"];
            string assValue = txtAssValue.Text;
            string GWT = (string)Session["GRossWT"];
            // string pName = drCustomer.SelectedItem.Text;

            string shpType = Session["shpType"].ToString();
            string cSize = Session["Contr_size"].ToString();
            string cType = Session["Contr_Type"].ToString();
            string status = "ACTIVE";
            pName = Session["pName"].ToString();
            if (shpType != "AIR")
            {
                if (cType == "FCL")
                {
                    if (cSize == "20")
                        shpType = "ft20";
                    else if (cSize == "40")
                        shpType = "ft40";
                }
                else if (cType == "LCL")
                    shpType = "LCL";
                else
                    shpType = "break_bulk";
            }

            string BILL = "SB";
            // string strQuery = "";

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                TextBox amt = (TextBox)e.Row.FindControl("amt1");
                string Charge_desc = e.Row.Cells[2].Text;
                
                CheckBox chk = (CheckBox)e.Row.FindControl("chkSTAX");
                if (BILL != "SB")
                    chk.Enabled = false;
                // amt.Attributes.Add("onblur", "javascript:Totalcalculate();");
                string sno = e.Row.Cells[0].Text;
                //string Charge_desc = chrg.Text;
                // string sno =(string)Session["strChargeVal"];
                string Query = "select * from contract_mst m,contract_dtl s " +
                           "where m.contr_code=s.contr_code and m.customer_name='" + pName + "' and  " +
                           "m.contr_status='" + status + "' and s.charge_desc='" + Charge_desc + "' and " +
                           "m.contr_code='" + CID + "' and s.sno in (" + sno + ")";
                SqlConnection cnn = new SqlConnection(strconn);
                SqlDataAdapter da = new SqlDataAdapter(Query, cnn);
                DataSet ds = new DataSet();
                da.Fill(ds, "Contract");
                if (ds.Tables["Contract"].Rows.Count != 0)
                {
                    DataRowView row = ds.Tables["contract"].DefaultView[0];
                    Amount = row["" + shpType + ""].ToString();
                    CUnits = row["unit"].ToString();
                    cProduct = row["product"].ToString();
                    SB = row["SB"].ToString();
                    DB = row["DB"].ToString();

                    if (Amount == "")
                        Amount = "0";
                }

                if (SB == "YES" || DB == "YES")
                {
                    //To check Contract values
                    if (CUnits == "PER Kg")
                    {
                        string gWt = (string)Session["GRossWT"];
                        Wt = Convert.ToDouble(gWt);

                    }
                    else if (CUnits == "PER Contr")
                    {
                        string qty = (string)Session["QTY"];
                        Wt = Convert.ToDouble(qty);
                    }
                    else if (CUnits == "PER TON")
                    {
                        string gWt = (string)Session["GRossWT"];
                        Wt = Convert.ToDouble(gWt) / 1000;
                    }
                    else
                        Wt = 1;


                    // Charge Description values
                    if (Charge_desc != "Agency charges")
                    {
                        //GetChargeVariable(pName, status, Charge_desc, CID, shpType, GWT);
                        //amt.Text = (string)Session["ChargeAmount"];
                        if (cProduct == "At Actual")
                        {
                            AMT = Convert.ToDouble(Amount) * Wt;
                            amt.Text = AMT.ToString("#0.00#");
                        }
                        else if (cProduct == "Fixed")
                        {
                            AMT = Convert.ToDouble(Amount) * Wt;
                            amt.Text = AMT.ToString("#0.00#");
                        }
                        else
                        {
                            string Pro = "Variable";
                            string varAmt = "";
                            string varUnit = "";

                            string QueryPro = "select * from contract_dtl where contr_code='" + CID + "' and charge_desc='" + Charge_desc + "' and product='" + Pro + "' ";
                            SqlConnection cnnPro = new SqlConnection(strconn);
                            SqlDataAdapter daPro = new SqlDataAdapter(QueryPro, cnnPro);
                            DataSet dsPro = new DataSet();
                            daPro.Fill(dsPro, "ContractPRO");
                            if (dsPro.Tables["ContractPRO"].Rows.Count != 0)
                            {
                                DataRowView RowPro = dsPro.Tables["ContractPRO"].DefaultView[0];
                                varAmt = RowPro["" + shpType + ""].ToString();
                                varUnit = RowPro["unit"].ToString();

                                if (varAmt == "" || varAmt == "0")
                                    varAmt = "1";

                                if (varUnit == "PER Kg")
                                {
                                    string gWt = (string)Session["GRossWT"];
                                    totalAmt = Convert.ToDouble(gWt) * Convert.ToDouble(varAmt);

                                }

                            }




                            string QueryPro1 = "select * from contract_dtl where contr_code='" + CID + "' and charge_desc='" + Charge_desc + "'  ";
                            SqlConnection cnnPro1 = new SqlConnection(strconn);
                            SqlDataAdapter daPro1 = new SqlDataAdapter(QueryPro1, cnnPro1);
                            DataSet dsPro1 = new DataSet();
                            daPro1.Fill(dsPro1, "ContractPRO1");
                            DataTable dtPro1 = dsPro1.Tables[0];
                            foreach (DataRow rowPro in dtPro1.Rows)
                            {
                                Amount = rowPro["" + shpType + ""].ToString();
                                string product = rowPro["product"].ToString();

                                if (Amount == "")
                                    Amount = "1";
                                AMT = Convert.ToDouble(Amount);
                                //amt.Text = AMT.ToString("#0.00#");


                                if (product == "Minimum")
                                {
                                    if (totalAmt < AMT)
                                    {
                                        amt.Text = AMT.ToString("#0.00#");
                                        Session["Omin"] = amt.Text;
                                    }
                                    minVal = AMT;
                                }
                                else if (product == "Maximum")
                                {
                                    if (totalAmt > AMT)
                                    {
                                        amt.Text = AMT.ToString("#0.00#");
                                        Session["Omax"] = amt.Text;

                                    }
                                    maxVal = AMT;
                                    Session["OmaxVAL"] = "1";
                                }
                                else if (product == "Variable")
                                {
                                    if ((string)Session["OmaxVAL"] == "0")
                                    {
                                        if (totalAmt < minVal)
                                            amt.Text = (string)Session["Omin"];
                                        else
                                            amt.Text = totalAmt.ToString("#0.00#");
                                    }
                                    else
                                    {
                                        if (totalAmt < minVal)
                                            amt.Text = (string)Session["Omin"];
                                        else if (totalAmt > maxVal)
                                            amt.Text = (string)Session["Omax"];
                                        else
                                            amt.Text = totalAmt.ToString("#0.00#");
                                        Session["OmaxVAL"] = "0";
                                    }
                                }

                            }

                        }


                    }
                    else
                    {
                        if (cProduct == "At Actual")
                        {
                            AMT = Convert.ToDouble(Amount) * Wt;
                            amt.Text = AMT.ToString("#0.00#");
                        }
                        else if (cProduct == "Fixed")
                        {
                            AMT = Convert.ToDouble(Amount) * Wt;
                            amt.Text = AMT.ToString("#0.00#");
                        }
                        else
                        {
                            //start variable condition
                            GetVariable(pName, status, Charge_desc, CID, shpType);

                            ass = Convert.ToDouble(assValue);
                            string vers = Session["Variable"].ToString();
                            if (vers == "")
                            {
                                Response.Write("<script>alert('variables are not assigned please check contract details  ')</script>");

                            }
                            else
                            {
                                Double veriablePercentage = Convert.ToDouble(vers);
                                Double gVAL = ass / 100 * veriablePercentage;

                                string Query1 = "select * from contract_mst m,contract_dtl s " +
                                       "where m.contr_code=s.contr_code and m.customer_name='" + pName + "' and  " +
                                       "m.contr_status='" + status + "' and s.charge_desc='" + Charge_desc + "' and " +
                                       "m.contr_code='" + CID + "' order by s.product";
                                SqlConnection cnn1 = new SqlConnection(strconn);
                                SqlDataAdapter da1 = new SqlDataAdapter(Query1, cnn1);
                                DataSet ds1 = new DataSet();
                                da1.Fill(ds1, "agch");
                                DataTable dt = ds1.Tables[0];
                                foreach (DataRow Row in dt.Rows)
                                {
                                    Amount = Row["" + shpType + ""].ToString();
                                    string product = Row["product"].ToString();

                                    if (Amount == "")
                                        Amount = "0";
                                    AMT = Convert.ToDouble(Amount) * Wt;


                                    //if (product == "At Actual")
                                    //    amt.Text = AMT.ToString("#0.00#");
                                    //else if (product == "Fixed")
                                    //    amt.Text = AMT.ToString("#0.00#");
                                    if (product == "Minimum")
                                    {
                                        if (gVAL < AMT)
                                        {
                                            amt.Text = AMT.ToString("#0.00#");
                                            Session["min"] = amt.Text;
                                        }
                                        minVal = AMT;
                                    }
                                    else if (product == "Maximum")
                                    {
                                        if (gVAL > AMT)
                                        {
                                            amt.Text = AMT.ToString("#0.00#");
                                            Session["max"] = amt.Text;

                                        }
                                        maxVal = AMT;
                                        Session["MAXVAL"] = "1";
                                    }
                                    else if (product == "Variable")
                                    {
                                        if ((string)Session["MAXVAL"] == "0")
                                        {
                                            if (gVAL < minVal)
                                                amt.Text = (string)Session["min"];
                                            else
                                                amt.Text = gVAL.ToString("#0.00#");
                                        }
                                        else
                                        {
                                            if (gVAL < minVal)
                                                amt.Text = (string)Session["min"];
                                            else if (gVAL > maxVal)
                                                amt.Text = (string)Session["max"];
                                            else
                                                amt.Text = gVAL.ToString("#0.00#");
                                            Session["MAXVAL"] = "0";
                                        }
                                    }


                                }
                                //end charge details 
                            }
                        }
                    }
                }
                //end Contract value
                if (amt.Text == "")
                    amt.Text = "0";
                Gross = Gross + Convert.ToDouble(amt.Text);
                e.Row.Cells[0].ForeColor = Color.White;
            }

            SubTotal.Text = Gross.ToString("#0.00#");
            string cmp = (string)Session["CMP"];
            if (BILL == "SB")
            {
                Double RserTax = 0;
                Double REcess = 0;
                Double RScess = 0;

                sTax.Text = RserTax.ToString("#0.00#");
                EdCess.Text = REcess.ToString("#0.00#");
                SHCess.Text = RScess.ToString("#0.00#");

                Double NetAmt = Gross;
                Double ld = Convert.ToDouble(LessAd.Text);
                Totals.Text = NetAmt.ToString("#0.00#");
                bal = NetAmt - ld;
                LessAd.Text = ld.ToString("#0.00#");

            }
            else
            {
                Double NetAmt = Gross;
                Totals.Text = NetAmt.ToString("#0.00#");
                bal = Convert.ToDouble(SubTotal.Text) - Convert.ToDouble(LessAd.Text);
                
            }
            Double balanceAmount = Math.Round(bal);

            balance1.Text = balanceAmount.ToString();
            BalanceDue.Text = balanceAmount.ToString("#0.00#");

            txtRupees.Text = RsConvert.rupees(Convert.ToInt64(balance1.Text));
            Submit.Focus();
        }

    }
    protected void GetServiceTax(string cmp)
    {
        string sVal = drServiceTax.SelectedValue;
        SqlConnection conn2 = new SqlConnection(strconn);
        //string strQuery = "select * from tbl_serviceMaster where company='" + cmp + "'";
        string strQuery = "select * from tbl_serviceMaster where serviceTax='" + sVal + "'";

        SqlDataAdapter da2 = new SqlDataAdapter(strQuery, conn2);
        DataSet ds2 = new DataSet();
        da2.Fill(ds2, "SERVICETAX");


        DataRowView row = ds2.Tables["SERVICETAX"].DefaultView[0];
        Double stax = Convert.ToDouble(row["serviceTax"].ToString());
        Double Ecess = Convert.ToDouble(row["ecess"].ToString());
        Double SHEcess = Convert.ToDouble(row["shecess"].ToString());
        //Double STAX =(stax + Ecess + SHEcess);
        vSTax = stax;
        vECess = Ecess;
        vSHECess = SHEcess;
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
        //Submit.Focus();

    }
    protected void GetChargeVariable(string pName, string status, string Charge_desc, string CID, string shpType, string GWT)
    {
        //start variable condition
        GetVariable(pName, status, Charge_desc, CID, shpType);
        string amt = "";
        string Amount="";
        Double AMT;
     //   ass = Convert.ToDouble(assValue);
        string vers = Session["Variable"].ToString();
        if (vers == "")
            vers = "1";
        Double veriablePercentage = Convert.ToDouble(vers);
        Double GrossWT=Convert.ToDouble(GWT);
        Double gVAL = GrossWT  * veriablePercentage;

        string Query1 = "select * from contract_mst m,contract_dtl s " +
               "where m.contr_code=s.contr_code and m.customer_name='" + pName + "' and  " +
               "m.contr_status='" + status + "' and s.charge_desc='" + Charge_desc + "' and " +
               "m.contr_code='" + CID + "'";
        SqlConnection cnn1 = new SqlConnection(strconn);
        SqlDataAdapter da1 = new SqlDataAdapter(Query1, cnn1);
        DataSet ds1 = new DataSet();
        da1.Fill(ds1, "agch");
        DataTable dt = ds1.Tables[0];
        foreach (DataRow Row in dt.Rows)
        {
            Amount = Row["" + shpType + ""].ToString();
            string product = Row["product"].ToString();

            if (Amount == "")
                Amount = "0";
            AMT = Convert.ToDouble(Amount);


            if (product == "At Actual")
                amt = AMT.ToString("#0.00#");

            else if (product == "Minimum")
            {
                if (gVAL < AMT)
                {
                    amt = AMT.ToString("#0.00#");
                    Session["chgMin"] = amt;
                }
                minVal = AMT;
            }
            else if (product == "Maximum")
            {
                if (gVAL > AMT)
                {
                    amt = AMT.ToString("#0.00#");
                    Session["chgMax"] = amt;
                }
                maxVal = AMT;
                Session["MAXVAL"] = "1";

            }
            else if (product == "Variable")
            {
                if ((string)Session["MAXVAL"] == "0")
                {
                    if (gVAL < minVal)
                        amt = (string)Session["chgMin"];
                    else
                        amt = gVAL.ToString("#0.00#");
                }
                else
                {
                    if (gVAL < minVal)
                        amt = (string)Session["chgMin"];
                    else if (gVAL > maxVal)
                        amt = (string)Session["chgMax"];
                    else
                        amt = gVAL.ToString("#0.00#");
                    Session["MAXVAL"] = "0";
                }
            }

            Session["ChargeAmount"] = amt;
        }
        //end charge details 
    }
    protected void GetVariable(string pName, string status, string Charge_desc, string CID, string shpType)
    {
        string product = "Variable";
        string Query1 = "select * from contract_mst m,contract_dtl s " +
                     "where m.contr_code=s.contr_code and m.customer_name='" + pName + "' and  " +
                     "m.contr_status='" + status + "' and s.charge_desc='" + Charge_desc + "' and " +
                     "m.contr_code='" + CID + "' and s.product='" + product + "'";
        SqlConnection cnn1 = new SqlConnection(strconn);
        SqlDataAdapter da1 = new SqlDataAdapter(Query1, cnn1);
        DataSet ds1 = new DataSet();
        da1.Fill(ds1, "agch");
        if (ds1.Tables["agch"].Rows.Count != 0)
        {
            DataRowView row = ds1.Tables["agch"].DefaultView[0];
            string amt = row["" + shpType + ""].ToString();
            Session["Variable"] = amt;
        }
        else
        {
            string Query2 = "select * from contract_mst m,contract_dtl s " +
                     "where m.contr_code=s.contr_code and m.customer_name='" + pName + "' and  " +
                     "m.contr_status='" + status + "' and s.charge_desc='" + Charge_desc + "' and " +
                     "m.contr_code='" + CID + "'";
            SqlConnection cnn2 = new SqlConnection(strconn);
            SqlDataAdapter da2 = new SqlDataAdapter(Query2, cnn2);
            DataSet ds2 = new DataSet();
            da2.Fill(ds2, "DescVal");
            if (ds2.Tables["DescVal"].Rows.Count != 0)
            {
                DataRowView Row = ds2.Tables["DescVal"].DefaultView[0];
                string amt1 = Row["" + shpType + ""].ToString();
                Session["Variable"] = amt1;
            }
        }
    }
    protected void amt1_TextChanged(object sender, EventArgs e)
    {
       
        

    }
    protected void LKRupees_Click(object sender, EventArgs e)
    {
        txtRupees.Text = RsConvert.rupees(Convert.ToInt64(balance1.Text));
        Submit.Focus();
    }
    protected void preview_Click(object sender, EventArgs e)
    {
        //Response.Write("<script>alert('Invoice Printed')</script>");
        //String sno = (string)Session["INVOICECTR"];
        //Session["InvNoRep"] = sno;
        //Response.Redirect("CryInvoiceReportCTR.aspx");
        //Response.Redirect("CryInvoiceReportSTax.aspx");
        string format = "PD";
            Session["BillFormat"]=format;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "popup", "window.open('../Billing/CryInvoiceReportSTax.aspx','_blank','menubar=no, scrollbars=yes, toolbar=no, location=no, resizable=yes, left=20, top=20');", true);

    }
    protected void GridView2_SelectedIndexChanged(object sender, EventArgs e)
    {
        GridView3.Visible = true;
        GridScroll.Visible = true;
        string CID = Convert.ToString(GridView2.SelectedDataKey.Value);
        string Bill = "YES";
       // string Query = "select * from contract_dtl where contr_code='" + CID + "'";
        string BType = "SB";
        if (BType == "")
            BType = "SB";
        string Query = "select * from contract_dtl where contr_code='" + CID + "' and " + BType + "='" + Bill + "' order by sno";

        GridView3.DataSource = GetDataSQL(Query);
        GridView3.DataBind();
        Session["ContractID"] = CID;
        BtnContract_Submit.Visible = true;
    }
    protected void BtnContract_Submit_Click(object sender, EventArgs e)
    {
        Submit.Enabled = true;
        Session["PageLoad"] = "2";
        foreach (GridViewRow Row in GridView3.Rows)
        {
            CheckBox chkCharge = (CheckBox)Row.FindControl("chk");
            string units = Row.Cells[4].Text;
            cUNIT = cUNIT + units + ",";
            if (chkCharge.Checked)
            {
                string strSNO = Row.Cells[1].Text;
                strCharge = strCharge + "" + strSNO + ",";
            }
        }
        Session["UNITS"] = cUNIT.TrimEnd(',');
        if (strCharge != "")
        {
            tblContr.Visible = false;
            tblINV.Visible = true;
            GetContractInfo(strCharge);
        }
        string BiilType = rbBill.SelectedValue;
        if (BiilType == "DP")
        {
            //string pcode = Session["PCODE"].ToString();
            //string paddr = Session["PBranch"].ToString();
           // PartyADDR(pcode, paddr);
            txtSubParty.Text = "";
        }
        //btnSBT.Visible = true;
    }
   
    protected void PartyADDR(string pcode, string paddr)
    {
        MySqlConnection conn = new MySqlConnection(strconn1);
        conn.Open();
         string sqlQuery4 = "select *  from prt_mast m,prt_addr a " +
                                   "where m.party_code=a.party_code and  m.party_code='" + pcode + "' and a.addr_code='" + paddr + "'";
                MySqlDataAdapter da4 = new MySqlDataAdapter(sqlQuery4, conn);
                DataSet ds4 = new DataSet();
                da4.Fill(ds4, "prtMast");
                conn.Close();
                DataRowView row4 = ds4.Tables["prtMast"].DefaultView[0];
                //string cCode = row4["group_id"].ToString();
                //Session["cCode"] = cCode;
               
                    txtCompName.Text = row4["party_name"].ToString();
                    Session["pName"] = row4["party_name"].ToString();
                    
                   string pName = txtCompName.Text;
                    //GridView3.Visible = false;
                   // BtnContract_Submit.Visible = false;
                   // lblContr.Text = "CONTRACT INFORMATION FOR " + pName.ToUpper();

                    //Session["IECName"] = row["iec_name"].ToString();
                    txtAdd1.Text = row4["address"].ToString();
                    string city = row4["city"].ToString();
                    string pin = row4["pin"].ToString();
                  //  Session["addr"] = addr1;
                    Session["Pin"] = pin;
                    txtCity.Text = city;
                    Session["state"] = row4["state"].ToString();
                    //Session["state"] = txtAdd2.Text;
                    Session["Phone"] = row4["tel_no"].ToString();
    }
    protected void GetContractInfo(string sel)
    {
        string CID = (string)Session["ContractID"];
        string AssValue = txtAssValue.Text;
        pName = Session["pName"].ToString();

            string status="ACTIVE";
       
           // string pName = drCustomer.SelectedItem.Text;
            string strchargeVal = sel.TrimEnd(',');
            Session["strChargeVal"] = strchargeVal;
            string strQuery = "select * from contract_mst m,contract_dtl s where m.contr_code=s.contr_code and m.contr_code='" + CID + "' and m.customer_name='" + pName + "' and m.contr_status='" + status + "' and s.sno in(" + strchargeVal + ")";
            SqlConnection cnn = new SqlConnection(strconn);
            SqlDataAdapter daS = new SqlDataAdapter(strQuery, cnn);
            DataSet dsS = new DataSet();
            daS.Fill(dsS, "Contract");
            if (dsS.Tables["Contract"].Rows.Count != 0)
            {
                
                GridView1.DataSource = GetDataSQL(strQuery);
                GridView1.DataBind();
             //GridView1.Visible = true;
            //gvTbl.Visible = true;
            //gvTBL1.Visible = true;
            }
    }
    protected void GridView3_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        string shpType = Session["shpType"].ToString();
        string cSize = Session["Contr_size"].ToString();
        string cType = Session["Contr_Type"].ToString();
        string Bill = "SB";
        //string CID = (string)Session["ContractID"];
        //string Query = "";

        if (shpType != "AIR")
        {
            if (cType == "FCL")
            {
                if (cSize == "20")
                    shpType = "ft20";
                else if (cSize == "40")
                    shpType = "ft40";
            }
            else if (cType == "LCL")
                shpType = "LCL";
            else
                shpType = "break_bulk";
        }
        


        if (e.Row.RowType == DataControlRowType.Header)
        {
            
            if (Bill == "SB")
            {
                e.Row.Cells[10].Visible = true;
                e.Row.Cells[11].Visible = false;
            }
            else
            {
                e.Row.Cells[10].Visible = false;
                e.Row.Cells[11].Visible = true;
            }
            switch (shpType)
            {
                case "AIR":
                    {
                        e.Row.Cells[5].Visible = true;
                        e.Row.Cells[6].Visible = false;
                        e.Row.Cells[7].Visible = false;
                        e.Row.Cells[8].Visible = false;
                        e.Row.Cells[9].Visible = false;
                        break;
                    }
                case "break_bulk":
                    {
                        e.Row.Cells[5].Visible = false;
                        e.Row.Cells[6].Visible = true;
                        e.Row.Cells[7].Visible = false;
                        e.Row.Cells[8].Visible = false;
                        e.Row.Cells[9].Visible = false;
                        break;
                    }
                case "LCL":
                    {
                        e.Row.Cells[5].Visible = false;
                        e.Row.Cells[6].Visible = false;
                        e.Row.Cells[7].Visible = true;
                        e.Row.Cells[8].Visible = false;
                        e.Row.Cells[9].Visible = false;
                        break;
                    }
                
                case "ft20":
                    {
                        e.Row.Cells[5].Visible = false;
                        e.Row.Cells[6].Visible = false;
                        e.Row.Cells[7].Visible = false;
                        e.Row.Cells[8].Visible = true;
                        e.Row.Cells[9].Visible = false;
                        break;
                    }
                case "ft40":
                    {
                        e.Row.Cells[5].Visible = false;
                        e.Row.Cells[6].Visible = false;
                        e.Row.Cells[7].Visible = false;
                        e.Row.Cells[8].Visible = false;
                        e.Row.Cells[9].Visible = true;
                        break;
                    }

            }
        }
        if(e.Row.RowType==DataControlRowType.DataRow)
        {
           
            CheckBox chklist = (CheckBox)e.Row.FindControl("chk");
            string charge = e.Row.Cells[2].Text;
            string product=e.Row.Cells[3].Text;
            e.Row.Cells[1].ForeColor = Color.White;

            
            if (charge == "Agency charges" && (product=="Variable" || product=="Maximum"))
            {
                //i = i + 1;
                //if (i > 1)
                //{
                    chklist.Enabled = false;
               // }
            }
            
            if (Bill == "SB")
            {
                e.Row.Cells[10].Visible = true;
                e.Row.Cells[11].Visible = false;
            }
            else
            {
                e.Row.Cells[10].Visible = false;
                e.Row.Cells[11].Visible = true;
            }
            switch (shpType)
            {
                case "AIR":
                    {
                        e.Row.Cells[5].Visible = true;
                        e.Row.Cells[6].Visible = false;
                        e.Row.Cells[7].Visible = false;
                        e.Row.Cells[8].Visible = false;
                        e.Row.Cells[9].Visible = false;
                        break;
                    }
               
                case "break_bulk":
                    {
                        e.Row.Cells[5].Visible = false;
                        e.Row.Cells[6].Visible = true;
                        e.Row.Cells[7].Visible = false;
                        e.Row.Cells[8].Visible = false;
                        e.Row.Cells[9].Visible = false;
                        break;
                    }
                case "LCL":
                    {
                        e.Row.Cells[5].Visible = false;
                        e.Row.Cells[6].Visible = false;
                        e.Row.Cells[7].Visible = true;
                        e.Row.Cells[8].Visible = false;
                        e.Row.Cells[9].Visible = false;
                        break;
                    }
                case "ft20":
                    {
                        e.Row.Cells[5].Visible = false;
                        e.Row.Cells[6].Visible = false;
                        e.Row.Cells[7].Visible = false;
                        e.Row.Cells[8].Visible = true;
                        e.Row.Cells[9].Visible = false;
                        break;
                    }
                case "ft40":
                    {
                        e.Row.Cells[5].Visible = false;
                        e.Row.Cells[6].Visible = false;
                        e.Row.Cells[7].Visible = false;
                        e.Row.Cells[8].Visible = false;
                        e.Row.Cells[9].Visible = true;
                        break;
                    }
            }
            
        }

       
    }
    protected void LessAd_TextChanged(object sender, EventArgs e)
    {
        //LessAd.Attributes.Add("onblur", "javascript:CallServiceTax();");
        txtRupees.Text = RsConvert.rupees(Convert.ToInt64(balance1.Text));
        Submit.Focus();
    }
    protected void BtnClose_Click(object sender, EventArgs e)
    {
        tblINV.Visible = true;
        tblContr.Visible = false;
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
            // avoid for thread abort exception error
        }
        catch (Exception ex)
        {
            Response.Write("<script>alert('" + ex.Message + "')</script>");

        }
    }
    protected void Menu2_MenuItemClick(object sender, MenuEventArgs e)
    {
        MenuItem m1 = this.Menu2.FindItem("Exit");
        // MenuItem m2 = this.Menu2.FindItem("Enquiry OUT");
        MenuItem m3 = this.Menu2.FindItem("Renewal Contract");
        MenuItem m4 = this.Menu2.FindItem("Edit Contract");

        //MenuItem m3 = this.Menu2.FindItem("Out By Rererence");
        switch (e.Item.Value)
        {
            case "Exit":
                Response.Write("<script>window.close();</script>");
                break;
            case "Renewal Contract":
                Session["CONTRACT"] = "Renewal";
                Response.Redirect("~/frmContractEdit.aspx", false);
                break;
            case "Edit Contract":
                Session["CONTRACT"] = "Edit";
                Response.Redirect("~/frmContractEdit.aspx", false);
                break;



        }
    }
    protected void GrdPaddr_SelectedIndexChanged(object sender, EventArgs e)
    {

        for (int i = 0; i < GrdPaddr.Rows.Count; i++)
        {
            if (GrdPaddr.SelectedIndex == i)
            {
                string NO = Convert.ToString(GrdPaddr.SelectedDataKey.Value);
                string pcode = GrdPaddr.Rows[i].Cells[0].Text;
                MySqlConnection conn = new MySqlConnection(strconn1);
                conn.Open();
                string sqlQuery = "select *  from prt_addr where party_code='" + pcode + "' and addr_num=" + NO + "";
                MySqlDataAdapter da = new MySqlDataAdapter(sqlQuery, conn);
                DataSet ds = new DataSet();
                da.Fill(ds, "addr");
                conn.Close();
                // DataSet ds = new DataSet();
                //dsPAddr = ba.GetPartyAddr(pcode, NO); // pcode , no variables to pass to BA

                DataRowView row = ds.Tables["addr"].DefaultView[0];
                string addr1 = row["address"].ToString();
                string city = row["city"].ToString();
                string state = row["state"].ToString();
                string pin = row["pin"].ToString();
                Session["addr"] = addr1;
                Session["city"] = city;
                Session["state"] = state;
                Session["Pin"] = pin;
                Session["BCODE"] = NO;
                txtCity.Text = city;
                Session["BranchID"] = row["ADDR_NUM"].ToString();
                //txtAdd2.Text = state + " " + pin;
                txtAdd1.Text =addr1;
                Session["Phone"] = row["tel_no"].ToString();
               

            }
        }
         GrdADDRSCROLL.Visible=false;
         GrdPaddr.Visible=false;
         TrAddr.Visible=false;
         TrAddr1.Visible=false;
        //tblAddr.Visible = false;
        //tblMst.Visible = true;
        //BtnSubmit.Enabled = true;
        //txtContrName.Enabled=true;
        //txtApproved.Enabled=true;
        //txtFrom.Enabled=true;
        //txtTo.Enabled=true;

       // GridView1.Visible = true;
       // GetXML();
    }
    public DataSet PartyAddr(string pcode)
    {
        MySqlConnection conn = new MySqlConnection(strconn1);
        conn.Open();
        string sqlQuery4 = "select *  from prt_mast m,prt_addr a " +
                               "where m.party_code=a.party_code and  m.party_code='" + pcode + "'";
        MySqlDataAdapter da4 = new MySqlDataAdapter(sqlQuery4, conn);
        DataSet ds4 = new DataSet();
        da4.Fill(ds4, "prtMast");
        conn.Close();
        return ds4;
    }
    protected void rbBill_SelectedIndexChanged(object sender, EventArgs e)
    {
       
        string pcode=(string)Session["PCODE"];
        string BiilType = rbBill.SelectedValue;
        if (BiilType == "DP")
        {
            GrdADDRSCROLL.Visible=true;
             GrdPaddr.DataSource = PartyAddr(pcode);
             GrdPaddr.DataBind();
            GrdPaddr.Visible=true;
            TrAddr.Visible=true;
            TrAddr1.Visible=true;
        }
        else
        {
             GrdADDRSCROLL.Visible=false;
             //GrdPaddr.DataSource = PartyAddr(pcode);
             //GrdPaddr.DataBind();
            GrdPaddr.Visible=false;
            TrAddr.Visible=false;
            TrAddr1.Visible=false;
        }
    }
    protected void chkSTAX_CheckedChanged(object sender, EventArgs e)
    {
       // GetTransaction();
    }
    protected void GetTransaction()
    {
        string cmp = (string)Session["CMP"];
        foreach (GridViewRow row in GridView1.Rows)
        {

            TextBox txt = (TextBox)row.FindControl("amt1");
            //TextBox txtTOT = (TextBox)row.FindControl("totamt1");
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

                //Double rdTot = Math.Round(Tot);
                //txtTOT.Text = rdTot.ToString("#0.00");
                //Double Sto = Convert.ToDouble(txtTOT.Text);
                GrossTot = GrossTot + amt0;
                gSTAX = gSTAX + aStax;
                gECess = gECess + aECess;
                gSHECess = gSHECess + aSHECess;
            }
            else
            {
                //Double totSEr = Convert.ToDouble(txtTOT.Text);
                Double tot = Convert.ToDouble(txt.Text);
                total = total + tot;
                txt.Text = tot.ToString("#0.00#");
                //txtTOT.Text = totSEr.ToString("#0.00#");
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
        //lblsVal.Text = "ON " + GrossTot;
 
        //lblSTAX.Text = "Service Tax @10.00% ON " + GrossTot;
        GetPERCENT();

        //Gross = 0;
        //total = 0;
    }
    protected void txtJNO_TextChanged(object sender, EventArgs e)
    {

        //string jno = txtJNO.Text;
        string jobNo = drJobNo.SelectedValue;
        string Query="";
        string bill = "SB";
        if(bill=="SB")
            Query = "select * from iec_invoicenew where jobno = '" + jobNo + "'";
        else
            Query = "select * from iec_debit where jobno = '" + jobNo + "'";
        SqlConnection conn = new SqlConnection(strconn);
         
        SqlDataAdapter da = new SqlDataAdapter(Query, conn);
        
            DataSet ds = new DataSet();
            da.Fill(ds, "bill");
            if (ds.Tables["bill"].Rows.Count != 0)
            {
                //BtnStandard.Enabled = false;
                DataRowView row = ds.Tables["bill"].DefaultView[0];
                string eXISTbILL = row["invoice"].ToString();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Given jobs has already billing. The Bill No. " + eXISTbILL + "');", true);
                //drJobNo.Attributes.Add("onclientclick", "javascript:return are you sure want to continue ???");
                //ScriptManager.RegisterStartupScript(this, this.GetType(), "confirm", "confirm('Given jobs has already billing. The Bill No. " + eXISTbILL + " . Do you want Continue...?');", true);
                //ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "javascript:if(confirm('Are you sure you want to delete?') == false) return false;", true); 

                //if (Page.Request["confirm"] == "OK")
                //BtnStandard.Enabled = false;
                //ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "javascript:if(confirm('Sucessfully Saved') == false) return false;", true);
                //ScriptManager.RegisterClientScriptBlock(this,this.GetType(),"confirm","return confirm('Are you sure');",true);
                //BtnStandard.Enabled = true;
                //ClientScriptManager csm = Page.ClientScript;
                //csm.RegisterStartupScript(typeof(Page), "Info", "alert('You can't delete the table.The table details in Purchase Order table')", false);
            }
            
        
    
    }

    protected void drServiceTax_SelectedIndexChanged(object sender, EventArgs e)
    {
        //GetTransaction();
    }
    protected void chk_CheckedChanged(object sender, EventArgs e)
    {
        string fyjs = "";
        if (chk.Checked == true)
        {
            fyjs = (string)Session["Lfyear"];
            Session["FY"] = fyjs;

        }
        else
        {
            fyjs = (string)Session["FinancialYear"];
            Session["FY"] = fyjs;
        }

        drJobNo.DataSource = GetDataJNO(fyjs);
        drJobNo.DataValueField = "jobsno";
        drJobNo.DataTextField = "jobsno";
        drJobNo.DataBind();
        drJobNo.Items.Insert(0, new ListItem("~Select~", "0"));
       
    }
    protected void rbInvoice_SelectedIndexChanged(object sender, EventArgs e)
    {
        GenerateBillNo();

    }
    protected void GenerateBillNo()
    {
        //  string pName = drCustomer.SelectedItem.Text;
        string fy = (string)Session["FinancialYear"];

        //trBillNO.Visible = true;
        string jobNo = txtJobNo.Text;
        string BILL = "SB";
        string bType = "";
        string strQuery = "";
        string sbNo = "";

        if (BILL == "SB")
        {
            bType = "ATLSB";
            sbNo = "C/";
            //InCode = "SB";
            //btable = "iec_invoicenew";
            strQuery = "select * from iec_rno where iectype='" + bType  + "' and Fyear='" + fy + "'";
            // bType = "Invoice";
            lblIName.Text = "APOLLO CONTRACT INVOICE - IMPORTS";
        }
        else
        {
            bType = "ATLDB";
            sbNo = "C/";
            //InCode = "DB";
            //btable = "iec_debit";
            strQuery = "select * from iec_rno where iectype='" + bType  + "' and Fyear='" + fy + "'";
            // bType = "Debit Note";
            lblIName.Text = "APOLLO CONTRACT DEBIT NOTE - IMPORTS";
        }
        SqlConnection cnn = new SqlConnection(strconn);
        SqlDataAdapter da = new SqlDataAdapter(strQuery, cnn);
        DataSet ds = new DataSet();
        da.Fill(ds, "Contract");
        if (ds.Tables["Contract"].Rows.Count != 0)
        {
            DataRowView row = ds.Tables["Contract"].DefaultView[0];
            Int32 INO = Convert.ToInt32(row["rno"].ToString());
            Session["InvNo"] = INO + 1;
            INO = INO + 1;
            lblBill.Text = sbNo + Convert.ToString(INO);

            //string Query = "select * from " + btable + " where jobno = '" + jobNo + "'";
            //DataSet dstab = GetDataSQL(Query);
        }
    }
    protected void drJobNo_SelectedIndexChanged(object sender, EventArgs e)
    {

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
            ScriptManager.RegisterStartupScript(this, this.GetType(), "popup", "window.open('CryInvoiceReportSTax.aspx','_blank','menubar=no, scrollbars=no, toolbar=no, location=no,resizable=no,height=650,width=700, left=20, top=20');", true);

            //Response.Redirect("CryInvoiceReportStax.aspx");
        }

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
            string query = "select distinct inv_no from iinv_dtl " +
                                  " where job_no = '" + txtJobNo.Text + "'";
            MySqlConnection con = new MySqlConnection(strconn1);
            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = query;
            cmd.Connection = con;
            MySqlDataAdapter dAdapter = new MySqlDataAdapter(cmd);
            DataTable dtConsr = new DataTable();
            dAdapter.Fill(dtConsr);
            string supplier = "";
            foreach (DataRow dtRow in dtConsr.Rows)
            {
                supplier = supplier + "," + dtRow[0].ToString();
            }
            // txtSupplierInvNo.Text = supplier.TrimStart(',');
            txtimpRemark.Text = "Supplier Inv No :" + supplier.TrimStart(',');
        }
        else
        {
            txtimpRemark.Text = "";
        }
    }
}
