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



public partial class PIPLDebitCD : System.Web.UI.Page 
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
    string fyear = "";
    string CNTRNO = "";
    int flag = 0;
    int invFlag =0;
    DateTime blDate;
    DateTime hblDate;
    Double Gross = 0;
    Double GrossTot = 0;
    Double total;
    Double bal;
    #endregion
    protected void Page_Load(object sender, EventArgs e)
    {
       
        fyear=(string)Session["FinancialYear"];

        if (IsPostBack == false)
        {
            TallyAccountName();
            Session["VGUID"] = Guid.NewGuid().ToString();
            GetXML();
            string lfyear = (string)Session["Lfyear"];
            
            chk.Text = lfyear;
            Submit.Enabled = false;
            btnMail.Enabled = false;
            preview.Enabled = false;

            TrAddr.Visible=false;
            TrAddr1.Visible=false;
            Session["RBMODE"] = "IMP";
            Session["Invoice"] = "Debit Note";

            Session["IECName"] = "";
            Session["IECAdd1"] = "";
            Session["IECAdd2"] = "";
            Session["IECCity"] = "";
            try
            {
                drJobNo.DataSource = GetData(fyear);
                drJobNo.DataValueField = "jobno";
                drJobNo.DataTextField = "jobno";
                drJobNo.DataBind();
                drJobNo.Items.Insert(0, new ListItem("~Select~", "0"));
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('" + ex.Message + "');", true);
            }
            rbBill.SelectedValue = "DP";
            //TextBoxOnBlur();
            txtCompName.Text = (string)Session["IECName"];
            txtAdd1.Text = (string)Session["IECAdd1"];
            txtCity.Text = (string)Session["IECCity"];
            
            string LNA = (string)Session["Invoice"];
            string dates = DateTime.Now.ToString("dd/MM/yyyy");
            
            invDate.Text = dates;
            if ((string)Session["Invoice"] == "Invoice")
            {
                lblIName.Text = "INVOICE";
                lblINumber.Text = "INVOICE NO.:";
            }
            else
            {
                string tp = "CD";
                lblIName.Text = "CUSTOM DUTY DEBIT NOTE - IMPORTS";
                lblINumber.Text = "DB NO.:";
                DebitNoteGenerated(tp);
            }
            if ((string)Session["RBMODE"] == "EXP")
            {
                Label16.Text = "SB NO/DT.";
                Label17.Text = "Item Exported";
                Label19.Text = "FOB Value";
            }

            //SqlConnection conn = new SqlConnection(strPIPL);
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
    
    public DataSet GetData(string fy)
    {
        SqlConnection conn1 = new SqlConnection(strImpex);
        string sqlStatement1 = "select *  from T_JobCreation  order by jobno";
        SqlDataAdapter da1 = new SqlDataAdapter(sqlStatement1, conn1);
        DataSet ds1 = new DataSet();
        da1.Fill(ds1, "ijobno");
        return ds1;
    }
    protected void TextBoxOnBlur()
    {
       

       // LessAd.Attributes.Add("onblur", "javascript:LessADvance();");

       

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


                SqlConnection conn = new SqlConnection(strImpex);
                string sqlQuery = "";
                if (chk.Checked == true)
                    sqlQuery = "select *  from T_JobCreation where jobno='" + jno + "' ";
                else
                    sqlQuery = "select *  from T_JobCreation where jobno='" + jno + "' ";
                SqlDataAdapter da = new SqlDataAdapter(sqlQuery, conn);
                DataSet ds = new DataSet();
                da.Fill(ds, "iworkreg");

                if (ds.Tables["iworkreg"].Rows.Count != 0)
                {
                    Submit.Enabled = true;
                    DataRowView row = ds.Tables["iworkreg"].DefaultView[0];
                    string jobNo = row["jobno"].ToString();
                    txtJobNo.Text = jobNo;
                    //txtAssValue.Text = row["tot_ass_vl"].ToString();
                    //txtCustomDuty.Text = row["tot_duty"].ToString();
                    //string item = row["inv_dtl"].ToString();
                    //item = item.Replace("'", " ");
                    //txtImpotItem.Text = item;
                    //string pcode = row["party_code"].ToString();

                    string sType = row["mode"].ToString();
                    Session["TransportMode"] = sType;
                   // Session["PCODE"] = pcode;
                    if (sType == "A")
                        lblIName.Text = "CUSTOM DUTY DEBIT NOTE - IMPORTS" + " - AIR SHIPMENT";
                    else
                        lblIName.Text = "CUSTOM DEBIT NOTE - IMPORTS" + " - SEA SHIPMENT";

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

                        //if (BLDate != "" || BLDate != string.Empty)
                        //    blDate = Convert.ToDateTime(BLDate);
                        //if (HBLDate != "" || HBLDate != string.Empty)
                        //    hblDate = Convert.ToDateTime(HBLDate);
                        if (BLDate != "" || BLDate != string.Empty)
                            txtBLNo.Text = bl + " dt." + BLDate;
                        else
                            txtBLNo.Text = hbl + " dt." + HBLDate;

                        string pkg = row1["NoOfPackages"].ToString();
                        string pkg_unit = row1["PackagesUnit"].ToString();
                        string gross = row1["GrossWeight"].ToString();
                        string gross_unit = row1["GrossWeightUnit"].ToString();
                        pkg = pkg.Replace(".000", "");
                        gross = gross.Replace(".000", "");

                        txtQty.Text = pkg + " " + pkg_unit + "/" + gross + " " + gross_unit;
                    }
                    //string sqlQuery2 = "select *  from ijob_Pos where job_no='" + jobNo + "'";
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
                    //        txtBENo.Text = be + " dt." + bedate;
                    //    }
                    //    else
                    //    {
                    //        DateTime beDate = Convert.ToDateTime(bedate);

                    //        txtBENo.Text = be + " dt." + beDate.ToString("dd/MM/yyyy");
                    //    }
                    //}
                    string sqlQuery3 = "select *  from T_ShipmentContainerInfo where jobno='" + jobNo + "' ";
                    SqlDataAdapter da3 = new SqlDataAdapter(sqlQuery3, conn);
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
                            // snos = row3["sr_no"].ToString();
                            cno = row3["ContainerNo"].ToString();
                            cTyp = row3["LoadType"].ToString();
                            cSize = row3["ContainerType"].ToString();
                            CNTRNO = CNTRNO + cno + ",";
                        }
                        txtNote.Text = CNTRNO.TrimEnd(',');
                        
                        string pref = "";
                     
                            pref =  cSize + " Ft - " + cTyp;
                       
                        txtNCNTR.Text = pref;

                        
                    }
                    //end stype
                    string sqlQuery4 = "select *  from T_Importer " +
                                       "where jobno='" + drJobNo.SelectedValue + "' ";
                    SqlDataAdapter da4 = new SqlDataAdapter(sqlQuery4, conn);
                    DataSet ds4 = new DataSet();
                    da4.Fill(ds4, "prtMast");
                    conn.Close();
                    DataRowView row4 = ds4.Tables["prtMast"].DefaultView[0];

                    //string cCode = row4["group_id"].ToString();
                    //Session["cCode"] = cCode;
                    //if (cCode == "")
                    //{
                    txtCompName.Text = row4["Importer"].ToString();
                    try
                    {
                        string VchType = "DB";
                        txtInvSeqNo.Text = Convert.ToString(InvSequence.InvSeqNO(row4["Importer"].ToString(), VchType, txtJobNo.Text));
                    }
                    catch
                    {
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
                    //    //Third party Addr
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
                    //        // GridView3.Visible = false;
                    //        // BtnContract_Submit.Visible = false;
                    //        // lblContr.Text = "CONTRACT INFORMATION FOR " + pName.ToUpper();

                    //        //Session["IECName"] = row["iec_name"].ToString();
                    //        string addr11 = row4["address"].ToString();
                    //        string city = row4["city"].ToString();
                    //        string pin = row4["pin"].ToString();
                    //        Session["addr"] = addr11;
                    //        Session["Pin"] = pin;
                    //        txtCity.Text = city;
                    //        Session["state"] = row4["state"].ToString();
                    //        //Session["state"] = txtAdd2.Text;
                    //        Session["Phone"] = row4["tel_no"].ToString();
                    //        //TrAddr.Visible = true;
                    //        //TrAddr1.Visible = true;
                    //        //GrdADDRSCROLL.Visible = true;
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
                  
                    txtAdd1.Text = addr1 ;
                  
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

    public DataSet GetDataSQL(string Query)
    {
        SqlConnection conn = new SqlConnection(strImpex);
        SqlDataAdapter da = new SqlDataAdapter(Query, conn);
        DataSet ds = new DataSet();
        da.Fill(ds, "SQLtable");
        return ds;
    }
    protected void DebitNoteGenerated(string iType)
    {
        SqlConnection conn2 = new SqlConnection(strImpex);
        //string strQuery = "select * from M_RunningNo where iectype='" + iType + "' and Fyear='" + fyear + "'";
        string strQuery = "select * from M_RunningNo where iectype='" + iType + "' ";
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
       
        //GetTransaction();
        string tp = "CD";
        string jobNo = txtJobNo.Text;
      
        try
        {
            DebitNoteGenerated(tp);
            if (txtCompName.Text == "" || txtJobNo.Text == "")
            {
                Response.Write("<script>alert('Please Give the Invoice Details')</script>");
                txtCompName.Focus();
            }
            else
            {
                btnMail.Visible = true;
                string Query = "select * from M_iec_debit where jobno = '" + jobNo + "'";

                DataSet ds = GetDataSQL(Query);

               
                if (invFlag == 0)
                    PIPLInovice();
                
              
            }
            balance1.Visible = false;
            preview.Visible = true;
          
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
            InCode = "CD";
        }
        string dates = invDate.Text;
        
        string[] DT = dates.Split('/');
        dates = DT[2] + "-" + DT[1] + "-" + DT[0];

        string EntryDate = DateTime.Now.ToString();
      
        double st = Convert.ToDouble(SubTotal.Text);
      
        double la = Convert.ToDouble(LessAd.Text);
        double nt = Convert.ToDouble(BalanceDue.Text);
        string ino = Session["InvNo"].ToString();
        Int32 invno = Convert.ToInt32(ino);
        string suffix = txtSuffix.Text;
        string Notes = txtNote.Text;

        string pName = txtCompName.Text;
        pName = pName.Replace("'", " ");

        string impItem = txtImpotItem.Text;
        impItem = impItem.Replace("'", " ");
        txtImpotItem.Text = impItem;

        string ADDRESS = txtAdd1.Text;
        ADDRESS = ADDRESS.Replace("'", " ");
        txtAdd1.Text = ADDRESS;
        string pREFF = txtParty_Reff.Text;
        pREFF = pREFF.Replace("'", " ");
        txtParty_Reff.Text = pREFF;

        string subparty = ddlTallySubPartyName.SelectedItem.Text;
        if (subparty == "~Select~")
        {
            subparty = "";
        }
        string InvSeqNo = txtInvSeqNo.Text;
        SqlConnection conn = new SqlConnection(strImpex);
        string sqlQuery = " insert into M_IEC_Debit(invoice,invoiceDate,compName,Address1,address2,City,pincode,state," +
                          " phone,partyReff,jobNo,BLNo,BENoDate,importitem,Quantity,Ass_value,Container_no,Custom_Duty," +
                          " subTotal,Grand_total,less_advance,Net_total,sub_party,Nettotal_words,invoiceType,Mode,invoiceNo,"+
                          "entryBy,eDate,fyear,TransportMode,suffix,notes,VGUID,BranchID,TallyAccountName,TallySubPartyName,InvSeqNo,SubPartyAddr) values('" + lblInvNo.Text + "','" + dates + "','" + pName + "'," +
                          " '" + txtAdd1.Text + "','" + (string)Session["state"] + "','" + txtCity.Text + "','" + Session["Pin"] + "','" + Session["state"] + "','" + (string)Session["Phone"] + "','" + txtParty_Reff.Text + "'," +
                          " '" + txtJobNo.Text + "','" + txtBLNo.Text + "','" + txtBENo.Text + "','" + txtImpotItem.Text + "','" + txtQty.Text + "'," +
                          " '" + txtAssValue.Text + "','" + txtNCNTR.Text + "','" + txtCustomDuty.Text + "'," + st + "," +
                          " " + st + "," + la + "," + nt + ",'" + txtSubParty.Text + "','" + hdnRuppees.Value + "','" + InCode + "'," +
                          "'" + Session["RBMODE"] + "'," + invno + ",'" + (string)Session["USER-NAME"] + "','" + EntryDate + "','" + (string)Session["FinancialYear"] + "'," +
                          "'" + (string)Session["TransportMode"] + "','" + suffix + "','" + Notes + "','" + (string)Session["VGUID"] + "','" + (string)Session["BranchID"] + "','" + ddlTallyAccountName.SelectedItem.Text + "','" + subparty + "','" + InvSeqNo + "','" + txtSubPartyAddr.Text + "')";

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
                    invFlag = 1;
                    updateRNO(invno, InCode, fyear);

                    try
                    {
                        string VchType = "DB";
                        InvSequence.InvSeqNOSave(txtCompName.Text, VchType);
                    }
                    catch
                    {
                    }
                    Response.Write("<script>" + "alert('Debit Note has successfully Generated');" + "</script>");
                }
            }
          
            Submit.Enabled = false;
            btnMail.Enabled = true;
            preview.Enabled = true;

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
          
            string amount = amt.Text;
            string Charge_desc = chrg.Text;
            string Receipt = recpt.Text;
           
            if (amount == "")
                amount = "0.00";
            if (amount != "0.00" && Charge_desc != "")
            {

                string Query = "insert into T_iec_debit_DTL(invoice,sno,charge_desc,Narration,amount) " +
                               "values('" + sbNo + "'," + count + ",'" + Charge_desc + "','" + Receipt + "'," + amount + ")";
                GetCommandIMP(Query);
                count = count + 1;
                flag = 1;
            }
           
        }

        if (flag != 1)
        {
            string Query = "delete from M_iec_debit where invoice='" + sbNo + "'";
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
    protected void updateRNO(int ino, string iType,string fy)
    {
        SqlConnection conn = new SqlConnection(strImpex);
        string sqlQuery = "update M_RunningNo set rno=" + ino + " where iecType='" + iType + "' and fyear='" + fy + "' ";
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

        string strQuery = "select * from M_iec_debit where invoice='" + lblInvNo.Text + "' and contr_code is null and particular1 is not null";
        SqlConnection conn = new SqlConnection(strImpex);

        SqlDataAdapter da = new SqlDataAdapter(strQuery, conn);
        DataSet ds = new DataSet();
        da.Fill(ds, "table");
        //if (ds.Tables["table"].Rows.Count == 0)
        //    ScriptManager.RegisterStartupScript(this, this.GetType(), "popup", "window.open('/Billing/CryInvoiceReportCTR.aspx','_blank','menubar=no, scrollbars=yes, toolbar=no, location=no, resizable=yes, left=20, top=20');", true);
         
        //else
        //    ScriptManager.RegisterStartupScript(this, this.GetType(), "popup", "window.open('CryInvoiceReport.aspx','_blank','menubar=no, scrollbars=yes, toolbar=no, location=no, resizable=yes, left=20, top=20');", true);
        Session["InvNo"] = lblInvNo.Text;
        //string rep = (string)Session["InvNo"];
        //string sub = rep.Substring(4, 2);
        //if (sub == "SB")
        //{
        //Response.Redirect("../frmImpInvoiceReport.aspx");
        //}
        //else
        //{
            Response.Redirect("../frmDebit.aspx");
        //}
            
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
    protected void Menu2_MenuItemClick(object sender, MenuEventArgs e)
    {
      
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
        // trMain.Visible = true;
        // TrAddr1.Visible=false;
       
    }
    //public DataSet PartyAddr(string pcode)
    //{
    //    MySqlConnection conn = new MySqlConnection(strconn1);
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
            TrAddr.Visible = true;
            TrAddr1.Visible = true;
            Panel2.Visible = true;
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
    protected void amt1_TextChanged(object sender, EventArgs e)
    {
       
    }
    protected void GetTransaction()
    {
        string cmp = (string)Session["CMP"];
        foreach (GridViewRow row in GridView1.Rows)
        {

            TextBox txt = (TextBox)row.FindControl("amt1");
            
            if (txt.Text == "")
                txt.Text = "0";
            
              
                Double tot = Convert.ToDouble(txt.Text);
                total = total + tot;
                txt.Text = tot.ToString("#0.00#");
                
            
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
       

    }
    protected void GridView1_RowDataBond(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            TextBox amt = (TextBox)e.Row.FindControl("amt1");
            if (amt.Text == "")
                amt.Text = "0.00";
           
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
        SqlConnection connM = new SqlConnection(strImpex);
        string sqlQueryM = "";
        if (chk.Checked == true)
            sqlQueryM = "select *  from T_JobCreation where jobno='" + jno + "' ";
        //sqlQueryM = "select *  from eworkreg where jobsno='" + jno + "' and job_no like '%" + (string)Session["Lfyear"] + "%'";
        else
            sqlQueryM = "select *  from T_JobCreation where jobno='" + jno + "' ";
        //sqlQueryM = "select *  from eworkreg where jobsno='" + jno + "' and job_no like '%" + (string)Session["FinancialYear"] + "%'";
        SqlDataAdapter daM = new SqlDataAdapter(sqlQueryM, connM);
        try
        {
            DataSet dsM = new DataSet();
            daM.Fill(dsM, "iworkreg");
            if (dsM.Tables["iworkreg"].Rows.Count != 0)
            {
                DataRowView rowM = dsM.Tables["iworkreg"].DefaultView[0];
                jobNo = rowM["jobno"].ToString();
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('" + ex.Message + "');", true);
        }
        SqlConnection conn = new SqlConnection(strImpex);
        string Query = "select * from M_iec_debit where jobno = '" + jobNo + "'";
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
        Session["BILLTYPE"] = "DB";
        Session["INVOICECTR"] = sno;
        if (Session["Maill"] == null)
        {
            Session["JOBNO"] = txtJobNo.Text;
            Session["MAILBUTTON"] = "OK";
            Session["PageName"] = "PIPLInvoiceStax.aspx";
            Session["Maill"] = "SendMaill";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "popup", "window.open('CryInvoiceReportSTax.aspx','_blank','menubar=no, scrollbars=no, toolbar=no, location=no, resizable=no,height=650,width=700, left=20, top=20');", true);

            
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
            string query = "select distinct InvoiceNo from T_InvoiceDetails " +
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

            txtNote.Text = txtNote.Text + "-" + "Supplier Inv No :" + supplier.TrimStart(',');
            txtNote.Text = txtNote.Text.TrimStart('-');
        }
        else
        {
            txtNote.Text = "";
        }
    }
}