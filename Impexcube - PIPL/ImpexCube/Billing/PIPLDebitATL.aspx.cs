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
using MySql;
using MySql.Data.MySqlClient;


public partial class PIPLDebitATL : System.Web.UI.Page 
{
    VTS.ImpexCube.Utlities.Utility InvSequence = new VTS.ImpexCube.Utlities.Utility();
   // string strconn="Provider=Microsoft.Jet.OLEDB.4.0; Data Source=D:\\PIPL\\Classification.mdb; User Id=admin; Password=";
   // string strconn = (string)ConfigurationManager.AppSettings["ConnectionString"];
    string strPIPL = (string)ConfigurationManager.AppSettings["ConnectionImpex"];
    string strconn = (string)ConfigurationManager.AppSettings["ConnectionImpex"];
    string strconn1 = (string)ConfigurationManager.AppSettings["ConnectionVisual"];
    string strconnJSU = (string)ConfigurationManager.AppSettings["ConnectionJobStages"];


    #region
    private string strCName;
    private string InNo;
    private string InCode;
    private string invoice;
    private Int32 InID;
    string fyear = "";
    string CNTRNO = "";
    int flag = 0;
    int invFlag = 0;
    DateTime blDate;
    DateTime hblDate;

    Double Gross = 0;
    Double GrossTot = 0;
    Double total;
    Double bal;
    #endregion
    protected void Page_Load(object sender, EventArgs e)
    {
       // Response.Write("<script> alert('" + strconn + "')</script>");
       
        fyear=(string)Session["FinancialYear"];

        if (IsPostBack == false)
        {
            TallyAccountName();
            Session["VGUID"] = Guid.NewGuid().ToString();
            //Panel2.Enabled = false;
            GetXML();
            string lfyear = (string)Session["Lfyear"];
            form1.EnableViewState = false;
            chk.Text = lfyear;
            Submit.Enabled = false;
            btnMail.Enabled = false;
            preview.Enabled = false;

            //preview0.Visible = false;
            TrAddr.Visible=false;
            TrAddr1.Visible=false;
            Session["RBMODE"] = "IMP";
            Session["Invoice"] = "Debit Note";

            lblUser.Text = (string)Session["USER-NAME"];
            //lblDate.Text = System.DateTime.Now.ToString("dd-MMM-yyyy");
            //lblTime.Text = DateTime.Now.ToLongTimeString();
            if (lblUser.Text == "")
            {
                Response.Redirect("~/PIPL.aspx");
            }

            Session["IECName"] = "";
            Session["IECAdd1"] = "";
            Session["IECAdd2"] = "";
            Session["IECCity"] = "";
            try
            {
                drJobNo.DataSource = GetData(fyear);
                drJobNo.DataValueField = "jobsno";
                drJobNo.DataTextField = "jobsno";
                drJobNo.DataBind();
                drJobNo.Items.Insert(0, new ListItem("~Select~", "0"));
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('" + ex.Message + "');", true);
            }
            rbBill.SelectedValue = "DP";
            TextBoxOnBlur();
            txtCompName.Text = (string)Session["IECName"];
            //txtAddr.Text = (string)Session["IECName"];
            txtAdd1.Text = (string)Session["IECAdd1"];
            //txtAdd2.Text = (string)Session["IECAdd2"];
            txtCity.Text = (string)Session["IECCity"];
            
            string LNA = (string)Session["Invoice"];
          //  BtnPrint.Visible = false;
            string dates = DateTime.Now.ToString("dd/MM/yyyy");
            
            invDate.Text = dates;
            if ((string)Session["Invoice"] == "Invoice")
            {
                lblIName.Text = "INVOICE";
                lblINumber.Text = "INVOICE NO.:";
                //InvoiceGenerated();
            }
            else
            {
                string tp = "ATLDEM";
                lblIName.Text = "APOLLO DEMURRAGE DEBIT NOTE - IMPORTS";
                lblINumber.Text = "DB NO.:";
                DebitNoteGenerated(tp);
            }
            if ((string)Session["RBMODE"] == "EXP")
            {
                Label16.Text = "SB NO/DT.";
                Label17.Text = "Item Exported";
                Label19.Text = "FOB Value";
            }

            SqlConnection conn = new SqlConnection(strPIPL);
            string sqlQuery = "select  * from AppDetails";
            SqlDataAdapter da = new SqlDataAdapter(sqlQuery, conn);
            DataSet ds = new DataSet();
            da.Fill(ds, "name");

            if (ds.Tables["name"].Rows.Count != 0)
            {
                DataRowView row = ds.Tables["name"].DefaultView[0];
                lblCompName.Text = row["CompanyName"].ToString();
                //lblshortname1.Text = row["ShortName"].ToString();
                //lblshortname2.Text = row["ShortName"].ToString();
            }

        }

    }
    
    public DataSet GetData(string fy)
    {
        MySqlConnection conn1 = new MySqlConnection(strconn1);
        conn1.Open();
        string sqlStatement1 = "select *  from iworkreg where job_no like '%" + fy + "%'  and party_code='atli' order by jobsno";
        MySqlDataAdapter da1 = new MySqlDataAdapter(sqlStatement1, conn1);
        DataSet ds1 = new DataSet();
        da1.Fill(ds1, "ijobno");
        conn1.Close();
        return ds1;
    }
    protected void TextBoxOnBlur()
    {
        //ppaid1.Attributes.Add("onblur", "javascript:calculate();");
        //ppaid2.Attributes.Add("onblur", "javascript:calculate();");
        //ppaid3.Attributes.Add("onblur", "javascript:calculate();");
        //ppaid4.Attributes.Add("onblur", "javascript:calculate();");
        //ppaid5.Attributes.Add("onblur", "javascript:calculate();");
        //ppaid6.Attributes.Add("onblur", "javascript:calculate();");
        //ppaid7.Attributes.Add("onblur", "javascript:calculate();");
        //ppaid8.Attributes.Add("onblur", "javascript:calculate();");
        //ppaid9.Attributes.Add("onblur", "javascript:calculate();");
        //ppaid10.Attributes.Add("onblur", "javascript:calculate();");

       
        //subPaidTotal.Attributes.Add("onblur", "javascript:calculate();");

        //amt1.Attributes.Add("onblur", "javascript:Totalcalculate();");
        //amt2.Attributes.Add("onblur", "javascript:Totalcalculate();");
        //amt3.Attributes.Add("onblur", "javascript:Totalcalculate();");
        //amt4.Attributes.Add("onblur", "javascript:Totalcalculate();");
        //amt5.Attributes.Add("onblur", "javascript:Totalcalculate();");
        //amt6.Attributes.Add("onblur", "javascript:Totalcalculate();");
        //amt7.Attributes.Add("onblur", "javascript:Totalcalculate();");
        //amt8.Attributes.Add("onblur", "javascript:Totalcalculate();");
        //amt9.Attributes.Add("onblur", "javascript:Totalcalculate();");
        //amt10.Attributes.Add("onblur", "javascript:Totalcalculate();");

        LessAd.Attributes.Add("onblur", "javascript:LessADvance();");

        //txtPhone.Attributes.Add("onKeyUp", "javascript:return RestrictInt(this.value);");

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
                conn.Close();
                if (ds.Tables["iworkreg"].Rows.Count != 0)
                {
                    Submit.Enabled = true;
                    DataRowView row = ds.Tables["iworkreg"].DefaultView[0];
                    string jobNo = row["job_no"].ToString();
                    txtJobNo.Text = jobNo;
                    txtAssValue.Text = row["tot_ass_vl"].ToString();
                    txtCustomDuty.Text = row["tot_duty"].ToString();
                    string item = row["inv_dtl"].ToString();
                    item = item.Replace("'", " ");
                    txtImpotItem.Text = item;
                    string pcode = row["party_code"].ToString();

                    string sType = row["transport_mode"].ToString();
                    Session["TransportMode"] = sType;
                    Session["PCODE"] = pcode;
                    if (sType == "A")
                        lblIName.Text = "APOLLO DEMURRAGE DEBIT NOTE " + " - AIR SHIPMENT";
                    else
                        lblIName.Text = "APOLLO DEMURRAGE DEBIT NOTE " + " - SEA SHIPMENT";

                    string sqlQuery1 = "select *  from ishp_dtl where job_no='" + jobNo + "'";
                    MySqlDataAdapter da1 = new MySqlDataAdapter(sqlQuery1, conn);
                    DataSet ds1 = new DataSet();
                    da1.Fill(ds1, "ishp");
                    conn.Close();
                    if (ds1.Tables["ishp"].Rows.Count == 0)
                        Response.Write("<script>alert('There is no shipment information for given job number')</script>");
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
                    if (ds2.Tables["ijobs"].Rows.Count == 0)
                        Response.Write("<script>alert('There is no Job Position information for given job number')</script>");
                    else
                    {
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
                            pref = snos + "x" + cSize + " Ft - " + cTyp;
                        //}
                        //else
                        //    pref = cTyp;
                        //lblIName.Text = "DE - IMPORTS" + " - SEA SHIPMENT " + pref;
                        txtNCNTR.Text = pref;

                        //ContNo.Text = cno;
                    }
                    //end stype
                    string sqlQuery4 = "select *  from prt_mast m,prt_addr a " +
                                       "where m.party_code=a.party_code and  m.party_code='" + pcode + "'";
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
                        try
                        {
                            string VchType = "DB";
                            txtInvSeqNo.Text = Convert.ToString(InvSequence.InvSeqNO(row4["party_name"].ToString(), VchType, txtJobNo.Text));
                        }
                        catch
                        {
                        }

                        string addr = row4["address"].ToString();
                        addr = addr.Replace("'", " ");
                        Session["addr"] = addr;
                        string city = row4["city"].ToString();
                        string pin = row4["pin"].ToString();
                        Session["Pin"] = pin;
                        txtCity.Text = city;
                        Session["state"] = row4["state"].ToString();
                        //Session["state"] = txtAdd2.Text;
                        Session["Phone"] = row4["tel_no"].ToString();
                        txtSubParty.Text = "";
                        rbBill.Visible = false;
                    }
                    else
                    {
                        //Third party Addr
                        rbBill.Visible = true;
                        //  trBill.Visible = true;
                        txtSubParty.Text = row4["party_name"].ToString();
                        SqlConnection connCT = new SqlConnection(strconn);
                        string QueryCT = "select * from contract_mst cm,contract_addr cs  " +
                                         " where cm.customer_code=cs.customer_code and cm.customer_code='" + cCode + "'";
                        SqlDataAdapter daCT = new SqlDataAdapter(QueryCT, connCT);
                        DataSet dsCT = new DataSet();
                        daCT.Fill(dsCT, "Contrst");
                        if (dsCT.Tables["Contrst"].Rows.Count == 0)
                        {
                            MySqlConnection connCTN = new MySqlConnection(strconn1);
                            connCTN.Open();
                            string QueryCTN = "select * from party_group where group_id='" + cCode + "'";
                            MySqlDataAdapter daCTN = new MySqlDataAdapter(QueryCTN, connCTN);
                            DataSet dsCTN = new DataSet();
                            daCTN.Fill(dsCTN, "ContrstN");
                            connCTN.Close();
                            DataRowView rowN = dsCTN.Tables["ContrstN"].DefaultView[0];

                            txtCompName.Text = rowN["groupName"].ToString();

                            Session["pName"] = row4["party_name"].ToString();

                            string pName = txtCompName.Text;
                            // GridView3.Visible = false;
                            // BtnContract_Submit.Visible = false;
                            // lblContr.Text = "CONTRACT INFORMATION FOR " + pName.ToUpper();

                            //Session["IECName"] = row["iec_name"].ToString();
                            string addr11 = row4["address"].ToString();
                            string city = row4["city"].ToString();
                            string pin = row4["pin"].ToString();
                            Session["addr"] = addr11;
                            Session["Pin"] = pin;
                            txtCity.Text = city;
                            Session["state"] = row4["state"].ToString();
                            //Session["state"] = txtAdd2.Text;
                            Session["Phone"] = row4["tel_no"].ToString();
                            //TrAddr.Visible = true;
                            //TrAddr1.Visible = true;
                            //GrdADDRSCROLL.Visible = true;
                            GrdPaddr.Visible = true;
                        }
                        else
                        {
                            DataRowView rowCT = dsCT.Tables["Contrst"].DefaultView[0];

                            txtCompName.Text = rowCT["customer_name"].ToString();
                            Session["pName"] = rowCT["customer_name"].ToString();
                            string pName = txtCompName.Text;

                            //GridView3.Visible = false;
                            //BtnContract_Submit.Visible = false;
                            //lblContr.Text = "CONTRACT INFORMATION FOR " + pName.ToUpper();

                            string addr11 = rowCT["address"].ToString();
                            string city = rowCT["city"].ToString();
                            string pin = rowCT["pin"].ToString();
                            Session["addr"] = addr11;
                            Session["Pin"] = pin;
                            txtCity.Text = city;
                            Session["state"] = rowCT["state"].ToString();
                            //Session["state"] = txtAdd2.Text;
                            Session["Phone"] = rowCT["tel_no"].ToString();
                        }


                    }
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
                    //txtAddr.Text = ADDRS;
                    txtAdd1.Text = addr1 ;
                  
                    trMain.Visible = false;
                    TrAddr.Visible = true;
                    TrAddr1.Visible = true;

                    GrdPaddr.DataSource = PartyAddr(pcode);
                    GrdPaddr.DataBind();
                    GrdPaddr.Visible = true;
                    GrdADDRSCROLL.Visible = true;
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

    public DataSet GetDataSQL(string Query)
    {
        SqlConnection conn = new SqlConnection(strconn);
        SqlDataAdapter da = new SqlDataAdapter(Query, conn);
        DataSet ds = new DataSet();
        da.Fill(ds, "SQLtable");
        return ds;
    }
    protected void DebitNoteGenerated(string iType)
    {
        SqlConnection conn2 = new SqlConnection(strconn);
        string strQuery = "select * from iec_rno where iectype='" + iType + "' and Fyear='" + fyear + "'";
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
            Response.Write("<script>alert('Debit has not Found for Given Financial Year')</script>");

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
        string tp = "ATLDEM";
        string jobNo = txtJobNo.Text;
        //lblIName.Text = "DEBIT NOTE";
        //lblINumber.Text = "DEBIT NO.:";
        try
        {
            //DebitNoteGenerated(tp);
            if (txtCompName.Text == "" || txtJobNo.Text == "")
            {
                Response.Write("<script>alert('Please Give the Invoice Details')</script>");
                txtCompName.Focus();
            }
            else
            {
                btnMail.Visible = true;
                string Query = "select * from iec_debit where jobno = '" + jobNo + "'";

                DataSet ds = GetDataSQL(Query);

                //if (ds.Tables["SQLtable"].Rows.Count == 0)
                //{
                if (invFlag == 0)
                    PIPLInovice();
                //}
              
            }
            balance1.Visible = false;
            preview.Visible = true;
            //preview0.Visible = true ;
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('" + ex.Message + "');", true);
        }

        //        }
        //        else
        //        {
        //            Response.Write("<script>alert('Please Select Tally Sub Party Name')</script>");
        //        }
        //    }
        //    else
        //    {
        //        GetTransaction();
        //        string tp = "ATLDEM";
        //        string jobNo = txtJobNo.Text;
        //        //lblIName.Text = "DEBIT NOTE";
        //        //lblINumber.Text = "DEBIT NO.:";
        //        try
        //        {
        //            //DebitNoteGenerated(tp);
        //            if (txtCompName.Text == "" || txtJobNo.Text == "")
        //            {
        //                Response.Write("<script>alert('Please Give the Invoice Details')</script>");
        //                txtCompName.Focus();
        //            }
        //            else
        //            {
        //                btnMail.Visible = true;
        //                string Query = "select * from iec_debit where jobno = '" + jobNo + "'";

        //                DataSet ds = GetDataSQL(Query);

        //                //if (ds.Tables["SQLtable"].Rows.Count == 0)
        //                //{
        //                if (invFlag == 0)
        //                    PIPLInovice();
        //                //}

        //            }
        //            balance1.Visible = false;
        //            preview.Visible = true;
        //            //preview0.Visible = true ;
        //        }
        //        catch (Exception ex)
        //        {
        //            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('" + ex.Message + "');", true);
        //        }

        //    }

        //}
        //else
        //{
        //    Response.Write("<script>alert('Please Select Tally Account Name')</script>");
        //}
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
            InCode = "ATLDEM";
        }
        string dates = invDate.Text;
        //string dates = lblDate.Text;
        string[] DT = dates.Split('/');
        dates = DT[2] + "-" + DT[1] + "-" + DT[0];

        string EntryDate = System.DateTime.Now.ToString("yyyy-MM-dd");
      
        double st = Convert.ToDouble(SubTotal.Text);
      
        double la = Convert.ToDouble(LessAd.Text);
        double nt = Convert.ToDouble(BalanceDue.Text);
        string ino = Session["InvNo"].ToString();
        Int32 invno = Convert.ToInt32(ino);
        //string suffix = txtSuffix.Text;
        string Notes = txtNote.Text;

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


        string subparty = ddlTallySubPartyName.SelectedItem.Text;
        if (subparty == "~Select~")
        {
            subparty = "";
        }
        string InvSeqNo = txtInvSeqNo.Text;
        SqlConnection conn = new SqlConnection(strconn);
        string sqlQuery = " insert into IEC_Debit(invoice,invoiceDate,compName,Address1,address2,City,pincode,state," +
                          " phone,partyReff,jobNo,BLNo,BENoDate,importitem,Quantity,Ass_value,Container_no,Custom_Duty," +
                          " subTotal,Grand_total,less_advance,Net_total,sub_party,Nettotal_words,invoiceType,Mode,invoiceNo,"+
                          "entryBy,eDate,fyear,TransportMode,notes,impRemark,interRemark,VGUID,BranchID,TallyAccountName,TallySubPartyName,InvSeqNo,SubPartyAddr) values('" + lblInvNo.Text + "','" + dates + "','" + txtCompName.Text + "'," +
                          " '" + txtAdd1.Text + "','" + (string)Session["state"] + "','" + txtCity.Text + "','" + Session["Pin"] + "','" + Session["state"] + "','" + (string)Session["Phone"] + "','" + txtParty_Reff.Text + "'," +
                          " '" + txtJobNo.Text + "','" + txtBLNo.Text + "','" + txtBENo.Text + "','" + txtImpotItem.Text + "','" + txtQty.Text + "'," +
                          " '" + txtAssValue.Text + "','" + txtNCNTR.Text + "','" + txtCustomDuty.Text + "'," + st + "," +
                          " " + st + "," + la + "," + nt + ",'" + txtSubParty.Text + "','" + txtRupees.Text + "','" + InCode + "'," +
                          "'" + Session["RBMODE"] + "'," + invno + ",'" + lblUser.Text + "','" + EntryDate + "','" + (string)Session["FinancialYear"] + "'," +
                          "'" + (string)Session["TransportMode"] + "','" + Notes + "','" + impRK + "','" + intRK + "','" + (string)Session["VGUID"] + "','" + (string)Session["BranchID"] + "','" + ddlTallyAccountName.SelectedItem.Text + "','" + subparty + "','" + InvSeqNo + "','" + txtSubPartyAddr.Text + "')";
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
                DebitDTL(lblInvNo.Text);

                if (flag != 0)
                {
                    try
                    {
                        string VchType = "DB";
                        InvSequence.InvSeqNOSave(txtCompName.Text, VchType);
                    }
                    catch
                    {
                    }
                    //updateRNO(invno, InCode, fyear);
                    //string Query = "update ijob_pos set db_note_no='" + lblInvNo.Text + "',db_date='" + dates + "' where job_no='" + txtJobNo.Text + "'";
                    //GetCommand(Query, strconn1);
                    //GetCommand(Query, strconnJSU);
                    invFlag = 1;
                    //// update Bill status
                    //string blQuery = "update impjobstage set date='" + dates + "' where job_no='" + txtJobNo.Text + "' and job_stage='BDate'";
                    //string blQueryJSU = "update iworkreg_dtl set date='" + dates + "' where job_no='" + txtJobNo.Text + "' and job_stage='BDate'";
                   
                    //string billstatus = "update iworkreg_jobstatus set status_job='Y' where job_no='" + txtJobNo.Text + "'";
                    //GetCommand(billstatus, strconnJSU);
                    
                    //GetCommand(blQuery, strconn1);
                    //GetCommand(blQueryJSU, strconnJSU);

                    Response.Write("<script>" + "alert('Debit Note has successfully Generated');" + "</script>");
                }
            }
           // BtnPrint.Visible = true;

            Submit.Enabled = false;
            btnMail.Enabled = true;
            preview.Enabled = true;

            //BtnStandard.Visible = false;

        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('" + ex.Message + "');", true);
        }
    }
    protected void DebitDTL(string sbNo)
    {
        int count = 1;

        foreach (GridViewRow ROW in GridView1.Rows)
        {
            TextBox amt = (TextBox)ROW.FindControl("amt1");
            TextBox chrg = (TextBox)ROW.FindControl("txtDetails");
            TextBox recpt = (TextBox)ROW.FindControl("txtRecpt");
            ////TextBox st = (TextBox)ROW.FindControl("totamt1");
            //CheckBox chk = (CheckBox)ROW.FindControl("chkSTAX");
            // amt.Attributes.Add("onblur", "javascript:Totalcalculate();");
            string amount = amt.Text;
            string Charge_desc = chrg.Text;
            string Receipt = recpt.Text;
            //string sTAXval = "";
            //Double sssTax = Convert.ToDouble(sTAXval);
            if (amount == "")
                amount = "0.00";
            if (amount != "0.00" && Charge_desc != "")
            {
                
                string Query = "insert into iec_debit_DTL(invoice,sno,charge_desc,receipt,amount) " +
                               "values('" + sbNo + "'," + count + ",'" + Charge_desc + "','" + Receipt + "'," + amount + ")";
                GetCommandIMP(Query);
                count = count + 1;
                flag = 1;
            }
            //GrossTot = GrossTot + sssTax;
        }

        //string strQuery = "update iec_invoiceNew set service_Tax=" + GrossTot + " where invoice='" + sbNo + "'";
        //GetCommandIMP(strQuery);

        if (flag != 1)
        {
            string Query = "delete from iec_debit where invoice='" + sbNo + "'";
            GetCommandIMP(Query);
        }
    }
    protected void GetCommandIMP(string sqlQuery)
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
    protected void GetCommand(string Query, string connSTR)
    {

        MySqlConnection conn = new MySqlConnection(connSTR);
        conn.Open();
        MySqlCommand cmd = new MySqlCommand(Query, conn);
        cmd.CommandText = Query;
        cmd.Connection = conn;
        int res = cmd.ExecuteNonQuery();
       
    }
    protected void updateRNO(int ino, string iType,string fy)
    {
        SqlConnection conn = new SqlConnection(strconn);
        string sqlQuery = "update iec_rno set rno=" + ino + " where iecType='" + iType + "' and fyear='" + fy +"' ";
        conn.Open();
        SqlCommand cmd = new SqlCommand(sqlQuery, conn);
        SqlDataAdapter da = new SqlDataAdapter();
        cmd.CommandText = sqlQuery;
        cmd.Connection = conn;
        da.SelectCommand = cmd;


        int result = cmd.ExecuteNonQuery();
    }
    

   
    protected void BtnClose_Click(object sender, EventArgs e)
    {
        txtCompName.Text = "";
        txtAdd1.Text = "";
        //txtAdd2.Text = "";
        //txtAddr.Text = "";
        Session["IECName"] = "";
        Session["IECAdd1"] = "";
        Session["IECAdd2"] = "";
        Session["IECCity"] = "";
    }
    protected void LKRupees_Click(object sender, EventArgs e)
    {

        txtRupees.Text = RsConvert.rupees(Convert.ToInt64(balance1.Text));
        Submit.Focus();
    }
    protected void preview_Click(object sender, EventArgs e)
    {
        Session["BILLTYPE"] = "DB";
        String sno = (string)Session["INVOICECTR"];
        Session["InvNoRep"] = sno;

        string strQuery = "select * from iec_debit where invoice='" + lblInvNo.Text + "' and contr_code is null and particular1 is not null";
        SqlConnection conn = new SqlConnection(strconn);

        SqlDataAdapter da = new SqlDataAdapter(strQuery, conn);
        DataSet ds = new DataSet();
        da.Fill(ds, "table");
        if (ds.Tables["table"].Rows.Count == 0)
            ScriptManager.RegisterStartupScript(this, this.GetType(), "popup", "window.open('../Billing/CryInvoiceReportCTR.aspx','_blank','menubar=no, scrollbars=yes, toolbar=no, location=no, resizable=yes, left=20, top=20');", true);
            //Response.Redirect("CryInvoiceReportCTR.aspx");
        else
            ScriptManager.RegisterStartupScript(this, this.GetType(), "popup", "window.open('CryInvoiceReport.aspx','_blank','menubar=no, scrollbars=yes, toolbar=no, location=no, resizable=yes, left=20, top=20');", true);

            //Response.Redirect("CryInvoiceReport.aspx");   
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
                string sqlQuery = "select *  from prt_addr where party_code='" + pcode + "' and addr_num=" + NO + "";
                MySqlDataAdapter da = new MySqlDataAdapter(sqlQuery, conn);
                DataSet ds = new DataSet();
                da.Fill(ds, "addr");
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
                if ((string)Session["BranchID"] == "")
                {
                    Session["BranchID"] = "0";
                }
                //txtAdd2.Text = state + " " + pin;
                txtAdd1.Text =addr1;
                Session["Phone"] = row["tel_no"].ToString();
               

            }
        }
         GrdADDRSCROLL.Visible=false;
         GrdPaddr.Visible=false;
         TrAddr.Visible=false;
trMain.Visible = true;
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
                               "where m.party_code=a.party_code and  m.party_code='" + pcode + "' order by addr_num";
        MySqlDataAdapter da4 = new MySqlDataAdapter(sqlQuery4, conn);
        DataSet ds4 = new DataSet();
        da4.Fill(ds4, "prtMast");
        conn.Close();
        return ds4;
    }
    protected void rbBill_SelectedIndexChanged(object sender, EventArgs e)
    {
        string pcode = (string)Session["PCODE"];
        string BiilType = rbBill.SelectedValue;
        if (BiilType == "DP")
        {
            GrdADDRSCROLL.Visible = true;
            GrdPaddr.DataSource = PartyAddr(pcode);
            GrdPaddr.DataBind();
            GrdPaddr.Visible = true;
            TrAddr.Visible = true;
            TrAddr1.Visible = true;
            Panel2.Visible = true;
            txtSubParty.Text = "";
        }
        else
        {
            GrdADDRSCROLL.Visible = false;
            //GrdPaddr.DataSource = PartyAddr(pcode);
            //GrdPaddr.DataBind();
            GrdPaddr.Visible = false;
            TrAddr.Visible = false;
            TrAddr1.Visible = false;
            Panel2.Visible = true;
            trMain.Visible = true;
        }
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
            //TextBox txtTOT = (TextBox)row.FindControl("totamt1");
            //CheckBox chk = (CheckBox)row.FindControl("chkSTAX");
            if (txt.Text == "")
                txt.Text = "0";
            
                //Double totSEr = Convert.ToDouble(txtTOT.Text);
                Double tot = Convert.ToDouble(txt.Text);
                total = total + tot;
                txt.Text = tot.ToString("#0.00#");
                //txtTOT.Text = totSEr.ToString("#0.00#");
            
        }


        SubTotal.Text = total.ToString("#0.00#");
       
        GetPERCENT();

       
    }
    protected void GetPERCENT()
    {
        string BILL = "DB";
        Gross = Convert.ToDouble(SubTotal.Text);
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
        //Submit.Focus();

    }
    protected void GridView1_RowDataBond(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            TextBox amt = (TextBox)e.Row.FindControl("amt1");
            if (amt.Text == "")
                amt.Text = "0.00";
            //if (amt.Text == "0")
            //    amt.ForeColor = Color.White;
            Gross = Gross + Convert.ToDouble(amt.Text);
        }
        SubTotal.Text = Gross.ToString("#0.00#");
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
    protected void LessAd_TextChanged(object sender, EventArgs e)
    {
        txtRupees.Text = RsConvert.rupees(Convert.ToInt64(balance1.Text));
        Submit.Focus();
    }
    protected void drJobNo_TextChanged(object sender, EventArgs e)
    {
        string jno = drJobNo.SelectedValue;
        string jobNo = "";
        MySqlConnection connM = new MySqlConnection(strconn1);
        connM.Open();
        string sqlQueryM = "";
        //if (chk.Checked == true)
        //    sqlQueryM = "select *  from iworkreg where jobsno='" + jno + "' and job_no like '%" + (string)Session["Lfyear"] + "%'";
        //else
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
        SqlConnection conn = new SqlConnection(strconn);
        string Query = "select * from iec_debit where jobno = '" + jobNo + "'";
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
    protected void chk_CheckedChanged(object sender, EventArgs e)
    {

        if (chk.Checked == true)
            fyear = (string)Session["Lfyear"];
        else
            fyear = (string)Session["FinancialYear"];
        drJobNo.DataSource = GetData(fyear);
        drJobNo.DataValueField = "jobsno";
        drJobNo.DataTextField = "jobsno";
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
        Session["BILLTYPE"] = "DB";
        Session["INVOICECTR"] = sno;
        if (Session["Maill"] == null)
        {
            Session["JOBNO"] = txtJobNo.Text;
            Session["MAILBUTTON"] = "OK";
            Session["PageName"] = "PIPLInvoiceStax.aspx";
            Session["Maill"] = "SendMaill";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "popup", "window.open('CryInvoiceReportSTax.aspx','_blank','menubar=no, scrollbars=no, toolbar=no, location=no, resizable=no,height=650,width=700, left=20, top=20');", true);

            //Response.Redirect("CryInvoiceReportStax.aspx");
        }

    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        Session["BasicInformation"] = drJobNo.SelectedValue + "~" + txtCompName.Text + "~" + txtJobNo.Text + "~" + txtSubParty.Text + "~" + (string)Session["BLNo"] + "~" + txtAdd1.Text + "~" + (string)Session["BENo"] + "~" + txtAdd1.Text + "~" + (string)Session["ImpotItem"] + "~" + txtCity.Text + "~" + (string)Session["QTY"] + "~" + (string)Session["state"] + "~" + (string)Session["Pin"] + "~" + txtAssValue.Text + "~" + (string)Session["Phone"] + "~" + txtNote.Text + "~" + txtParty_Reff.Text + "~" + (string)Session["CustomDuty"];
        Session["CompanyName"] = txtCompName.Text;
        ScriptManager.RegisterStartupScript(this, this.GetType(), "Error", "window.open('PopUp.aspx','_blank','width=600,height=250, menubar=no, scrollbars=yes, toolbar=no, location=no, resizable=yes, left=350, top=200, Right=200=, bottom=200');", true);

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