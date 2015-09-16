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
//using MySql;
//using MySql.Data.MySqlClient;
using System.IO;
using System.Drawing;

public partial class frmContractInvoiceExp : System.Web.UI.Page
{
    string strImpex = (string)ConfigurationManager.ConnectionStrings["Constr"].ConnectionString;

    //string strPIPL = (string)ConfigurationManager.AppSettings["ConnectionImpex"];
    //string strImpexJSU = (string)ConfigurationManager.AppSettings["ConnectionJobStages"];

    //string strImpex = (string)ConfigurationManager.AppSettings["ConnectionVisual"];
    //string strImpex = (string)ConfigurationManager.AppSettings["ConnectionImpex"];
    Double Gross = 0;
    Double total;
    Double bal;
    Double minVal;
    Double maxVal;
    Double Wt = 1;
    Double totalAmt;
    int invFlag = 0;
    
    string cUNIT = "";
    string cProduct = "";
   
    DateTime blDate;
    DateTime hblDate;

    string strCharge = "";
    string pName = "";
    string MODE = "EXP";
    Double vSTax;
    Double vECess;
    Double vSHECess;
   
    Double GrossTot = 0;
  
    Double gSTAX;
    Double gECess;
    Double gSHECess;
    string fyear = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack == false)
        {
            TallyAccountName();
            Session["VGUID"] = Guid.NewGuid().ToString();
            rbInvoice.SelectedValue = "SB";
            GenerateBillNo();
            Session["PageLoad"] = null;

           
            preview.Enabled = false;
            btnMail.Enabled = false;

           
            string lfyear = (string)Session["Lfyear"];

            chk.Text = lfyear;

            lblUser.Text = (string)Session["USER-NAME"];
            Session["CustomerType"] = "Import";
            string dates = DateTime.Now.ToString("dd/MM/yyyy");

            invDate.Text = dates;

            string strQuery = "select * from M_ServiceMaster where fyear='" + fyear + "' order by fyear desc";
            drServiceTax.DataSource = GetDataSQL(strQuery);
            drServiceTax.DataTextField = "sTax";
            drServiceTax.DataValueField = "serviceTax";
            drServiceTax.DataBind();

            
            if (lblUser.Text == "")
            {
                Response.Redirect("~/PIPL.aspx");
            }
          
            Session["MAXVAL"] = "0";
            Session["OmaxVAL"] = "0";
            
            GrdPaddr.Visible = false;
            TrAddr.Visible = false;
            TrAddr1.Visible = false;
           

            tblINV.Visible = true;
            tblContr.Visible = false;
            
            string FY = (string)Session["FinancialYear"];
            Session["FY"] = FY;
            //string Query = "select * from eworkreg where job_no like '%" + FY + "%' order by job_no";
           
            try
            {
                drJobNo.DataSource = GetDataJNO(FY);
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

            
            txtParty_Reff.Attributes.Add("onblur", "javascript:InvoiceValue();");

           

        }

      

    }
    public DataSet GetDataJNO(string fy)
    {
        SqlConnection conn1 = new SqlConnection(strImpex);
        string sqlStatement1 = "select *  from E_M_JobCreation  order by jobno";
        SqlDataAdapter da1 = new SqlDataAdapter(sqlStatement1, conn1);

        DataSet ds1 = new DataSet();
        da1.Fill(ds1, "ijobno");
        return ds1;

    }
    protected void drJobNo_TextChanged(object sender, EventArgs e)
    {
        string jno = drJobNo.SelectedValue;
        string jobNo = "";

        SqlConnection connM = new SqlConnection(strImpex);
        string sqlQueryM = "";
        if (chk.Checked == true)
            sqlQueryM = "select *  from E_M_JobCreation where jobno='" + jno + "' ";
        else
            sqlQueryM = "select *  from E_M_JobCreation where jobno='" + jno + "' ";

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

        string BILL = rbInvoice.SelectedValue;
      
        string Query = "";


        SqlConnection conn = new SqlConnection(strImpex);
        if (BILL == "SB")
            Query = "select * from M_iec_invoicenew where jobno = '" + jobNo + "'";
        else
            Query = "select * from M_iec_debit where jobno = '" + jobNo + "'";
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
    //public DataSet GetData(string Query)
    //{
    //    MySqlConnection conn = new MySqlConnection(strImpex);
    //    MySqlDataAdapter da = new MySqlDataAdapter(Query, conn);
    //    DataSet ds = new DataSet();
    //    da.Fill(ds, "MySQLtable");
    //    return ds;
    //}
    public DataSet GetDataSQL(string Query)
    {
        SqlConnection conn = new SqlConnection(strImpex);
        SqlDataAdapter da = new SqlDataAdapter(Query, conn);
        DataSet ds = new DataSet();
        da.Fill(ds, "SQLtable");
        return ds;
    }

    //protected void drCustomer_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //}
    protected void BtnStandard_Click(object sender, EventArgs e)
    {
        TallyAccountName();
        Session["Company"] = "Std";
       
        string jno = drJobNo.SelectedValue;
        string Bill = rbInvoice.SelectedValue;
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

                SqlConnection conn = new SqlConnection(strImpex);
                string sqlQuery = "";
                if (chk.Checked == true)    
                    sqlQuery = "select *  from E_M_JobCreation where jobno='" + jno + "' ";
                else
                    sqlQuery = "select *  from E_M_JobCreation where jobno='" + jno + "' ";
                SqlDataAdapter da = new SqlDataAdapter(sqlQuery, conn);
                DataSet ds = new DataSet();
                da.Fill(ds, "iworkreg");

                if (ds.Tables["iworkreg"].Rows.Count != 0)
                {
                    DataRowView row = ds.Tables["iworkreg"].DefaultView[0];
                    string jobNo = row["jobno"].ToString();
                    txtJobNo.Text = jobNo;

                    txtAssValue.Text = row["FOBValue"].ToString();
                    txtCustomDuty.Text = row["TotalDuty"].ToString();

                    string item = row["InvoiceDetail"].ToString();
                    item = item.Replace("'", " ");
                    txtImpotItem.Text = item;

                    string pcode = row["ExporterName"].ToString();
                    string paddr = row["ExporterAddress1"].ToString();
                    Session["PCODE"] = pcode;
                    Session["PBranch"] = paddr;

                    //  Get Party Address
                    GrdPaddr.DataSource = PartyAddr(pcode);
                    GrdPaddr.DataBind();

                    string shpType = row["TransportMode"].ToString();
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
                    string sqlQuery1 = "select *  from E_T_Shipment where jobno='" + jobNo + "'";
                    SqlDataAdapter da1 = new SqlDataAdapter(sqlQuery1, conn);
                    DataSet ds1 = new DataSet();
                    da1.Fill(ds1, "ishp");
                    if (ds1.Tables["ishp"].Rows.Count == 0)
                        Response.Write("<script>alert('There is no Shipment Details for Given Jobs')</script>");
                    else
                    {
                        DataRowView row1 = ds1.Tables["ishp"].DefaultView[0];
                        string bl = row1["MBLNo"].ToString();
                        string BLDate = row1["MBLDate"].ToString();

                        string hbl = row1["HBLNo"].ToString();
                        string HBLDate = row1["HBLDate"].ToString();

                        if (BLDate != "" || BLDate != string.Empty)
                            blDate = Convert.ToDateTime(BLDate);
                        if (HBLDate != "" || HBLDate != string.Empty)
                            hblDate = Convert.ToDateTime(HBLDate);
                        if (BLDate != "" || BLDate != string.Empty)
                            txtBLNo.Text = bl + " dt." + blDate.ToString("dd/MM/yyyy");
                        else
                            txtBLNo.Text = hbl + " dt." + hblDate.ToString("dd/MM/yyyy");
                        string pkg = row1["TotalNoofPkgs"].ToString();
                        string pkg_unit = row1["TotalNoofPkgsUnit"].ToString();
                        string gross = row1["GrossWeight"].ToString();
                        string gross_unit = row1["GrossWeightUnit"].ToString();
                        Session["GRossWT"] = gross;
                        pkg = pkg.Replace(".000", "");
                        gross = gross.Replace(".000", "");
                       
                        txtQty.Text = pkg + " " + pkg_unit + "/" + gross + " " + gross_unit;
                    }


                    string sqlQueryQTY = "select count(jobno) as QTY  from E_T_Container where jobno='" + jobNo + "'";
                    SqlDataAdapter daQTY = new SqlDataAdapter(sqlQueryQTY, conn);
                    DataSet dsQTY = new DataSet();
                    daQTY.Fill(dsQTY, "iContrQTY");
                    if (dsQTY.Tables["iContrQTY"].Rows.Count != 0)
                    {
                        DataRowView rowQTY = dsQTY.Tables["iContrQTY"].DefaultView[0];
                        string QTY = rowQTY["QTY"].ToString();
                        Session["QTY"] = QTY;
                    }
                    string sqlQuery3 = "select *  from E_T_Container where jobno='" + jobNo + "' order by ID";
                    SqlDataAdapter da3 = new SqlDataAdapter(sqlQuery3, conn);
                    DataSet ds3 = new DataSet();
                    da3.Fill(ds3, "iContr");
                    if (ds3.Tables["iContr"].Rows.Count != 0)
                    {
                        DataRowView row3 = ds3.Tables["iContr"].DefaultView[0];
                        string cno = row3["ContainerNo"].ToString();
                        string cTyp = row3["Type"].ToString();
                        string cSize = row3["Size"].ToString();
                        string snos = row3["ID"].ToString();
                        Session["Contr_size"] = cSize;
                        Session["Contr_Type"] = cTyp;
                       
                        string pref = "";
                       
                            pref = snos + "x" + cSize + " Ft - " + cTyp;
                    
                            txtNCNTR.Text = pref;
                            
                    }
                    else
                    {
                        Session["Contr_size"] = "";
                        Session["Contr_Type"] = "";
                    }

                    //start Direct party info
                    //string sqlQuery4 = "select *  from prt_mast m,prt_addr a " +
                    //                   "where m.party_code=a.party_code and  m.party_code='" + pcode + "' and a.addr_code='" + paddr + "'";
                    string sqlQuery4 = "select *  from M_AccountMaster where AccountName ='" + paddr + "' ";
                    SqlDataAdapter da4 = new SqlDataAdapter(sqlQuery4, conn);
                    DataSet ds4 = new DataSet();
                    da4.Fill(ds4, "prtMast");
                    DataRowView row4 = ds4.Tables["prtMast"].DefaultView[0];
                    string cCode = row4["group_id"].ToString();
                    Session["cCode"] = cCode;
                    if (cCode == "")
                    {
                        txtCompName.Text = row4["AccountName"].ToString();
                        Session["pName"] = row4["AccountName"].ToString();

                        pName = txtCompName.Text;
                        GridView3.Visible = false;
                        BtnContract_Submit.Visible = false;
                        lblContr.Text = "CONTRACT INFORMATION FOR " + pName.ToUpper();


                        string addr1 = row4["Address1"].ToString();
                        string city = row4["City"].ToString();
                        string pin = row4["Pincode"].ToString();
                        addr1 = addr1.Replace("'", " ");
                        Session["addr"] = addr1;
                        Session["Pin"] = pin;
                        txtCity.Text = city;
                        Session["state"] = row4["State"].ToString();

                        Session["Phone"] = row4["PhoneNo"].ToString();
                        TrAddr.Visible = true;
                        TrAddr1.Visible = true;
                        GrdADDRSCROLL.Visible = true;
                        GrdPaddr.Visible = true;
                    }
                    else
                    {
                        //Third party Addr
                        trBill.Visible = true;
                        txtSubParty.Text = row4["AccountName"].ToString();
                        SqlConnection connCT = new SqlConnection(strImpex);
                        //string QueryCT = "select * from contract_mst cm,contract_addr cs  " +
                        //                 " where cm.customer_code=cs.customer_code and cm.customer_code='" + cCode + "'";
                        string QueryCT = "select *  from M_AccountDetails where AccountName ='" + cCode + "'";
                        SqlDataAdapter daCT = new SqlDataAdapter(QueryCT, connCT);
                        DataSet dsCT = new DataSet();
                        daCT.Fill(dsCT, "Contrst");
                        if (dsCT.Tables["Contrst"].Rows.Count == 0)
                        {

                            txtCompName.Text = row4["AccountName"].ToString();
                            Session["pName"] = row4["AccountName"].ToString();

                            pName = txtCompName.Text;
                            GridView3.Visible = false;
                            BtnContract_Submit.Visible = false;
                            lblContr.Text = "CONTRACT INFORMATION FOR " + pName.ToUpper();


                            string addr1 = row4["Address1"].ToString();
                            string city = row4["City"].ToString();
                            string pin = row4["Pincode"].ToString();
                            Session["addr"] = addr1;
                            Session["Pin"] = pin;
                            txtCity.Text = city;

                            Session["state"] = row4["State"].ToString();

                            Session["Phone"] = row4["PhoneNo"].ToString();
                            TrAddr.Visible = true;
                            TrAddr1.Visible = true;
                            GrdADDRSCROLL.Visible = true;
                            GrdPaddr.Visible = true;
                        }
                        else
                        {
                            DataRowView rowCT = dsCT.Tables["Contrst"].DefaultView[0];

                            txtCompName.Text = rowCT["AccountName"].ToString();
                            Session["pName"] = rowCT["AccountName"].ToString();
                            pName = txtCompName.Text;

                            GridView3.Visible = false;
                            BtnContract_Submit.Visible = false;
                            lblContr.Text = "CONTRACT INFORMATION FOR " + pName.ToUpper();

                            string addr1 = rowCT["Address1"].ToString();
                            string city = rowCT["City"].ToString();
                            string pin = rowCT["Pincode"].ToString();
                            Session["addr"] = addr1;
                            Session["Pin"] = pin;
                            txtCity.Text = city;

                            Session["state"] = rowCT["State"].ToString();

                            Session["Phone"] = rowCT["PhoneNo"].ToString();
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
                   
                    txtAdd1.Text = addrs1;
                    //Start contract info     
                    // To Get Contract information for the selected Customers....
                    string status = "ACTIVE";
                  

                    tblINV.Visible = false;
                    tblContr.Visible = true;
                    //string strQuery1 = "select * from contract_mst where customer_name='" + pName + "' and contr_status='" + status + "'";
                    string strQuery1 = "";
                    if ((string)Session["Contr_Type"] == "LCL")
                    {
                        strQuery1 = "select distinct customername,QuoteNo from M_Quote where customername='" + pName + "' and Type='" + (string)Session["Contr_Type"] + "' ";
                    }
                    else if ((string)Session["Contr_Type"] == "FCL")
                    {
                        if ((string)Session["Contr_size"] == "20")
                        {
                            strQuery1 = "select distinct customername,QuoteNo from M_Quote where customername='" + pName + "' and Type='20Feet' ";
                        }
                        else if ((string)Session["Contr_size"] == "40")
                        {
                            strQuery1 = "select distinct customername,QuoteNo from M_Quote where customername='" + pName + "' and Type='40Feet' ";
                        }
                    }
                    else
                    {
                        strQuery1 = "select distinct customername,QuoteNo from M_Quote where customername='" + pName + "' and Type='AIR' ";
                    }

                    GridView2.DataSource = GetDataSQL(strQuery1);
                    GridView2.DataBind();

                   
                    GridView3.Visible = false;
                    GridScroll.Visible = false;
                   
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
        SqlConnection con = new SqlConnection(strImpex);
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
      

    }
    protected void Submit_Click(object sender, EventArgs e)
    {
       
        string fy = (string)Session["FinancialYear"];
        string btable = "";
       
        string jobNo = txtJobNo.Text;
        string BILL = rbInvoice.SelectedValue;
     
        string strQuery = "";
        if (BILL == "SB")
        {

            btable = "M_iec_invoicenew";
            strQuery = "select * from M_RunningNo where iectype='SB' and Fyear='" + fy + "'";
          
        }
        else
        {
            btable = "iec_debit";
            strQuery = "select * from M_RunningNo where iectype='DB' and Fyear='" + fy + "'";
           
        }
        SqlConnection cnn = new SqlConnection(strImpex);
        SqlDataAdapter da = new SqlDataAdapter(strQuery, cnn);
        DataSet ds = new DataSet();
        da.Fill(ds, "Contract");
        if (ds.Tables["Contract"].Rows.Count != 0)
        {
            DataRowView row = ds.Tables["Contract"].DefaultView[0];
            Int32 INO = Convert.ToInt32(row["rno"].ToString());
            string InCode = row["iecCode"].ToString();

            Session["InvNo"] = INO + 1;
            Session["InCode"] = InCode;

          
            btnMail.Visible = false;
        if(invFlag==0)
            PIPLInovice();



            
        }
        else
            Response.Write("<script>alert('Invoice has not Found for Given Financial Year')</script>");
      


    }
    protected void PIPLInovice()
    {

        string dates = System.DateTime.Now.ToString("dd/MM/yyyy");
        string[] DT = dates.Split('/');
        dates = DT[2] + "-" + DT[1] + "-" + DT[0];

        string EntryDate = System.DateTime.Now.ToString("yyyy-MM-dd");
        string sbNo = "";
        string InCode = "";
        string Bill_Mst = "";
        string Bill_Dtl = "";
        string cmp = (string)Session["CMP"];
      
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
        string BILL = rbInvoice.SelectedValue;
        Session["BILLTYPE"] = BILL;
        sbNo = (string)Session["InCode"] + "/" + invno;
        if (BILL == "SB")
        {
            Bill_Mst = "M_IEC_InvoiceNew";
            Bill_Dtl = "T_iec_invoiceNew_DTL";
        
            InCode = "SB";

        }
        else
        {
            Bill_Mst = "M_IEC_debit";
            Bill_Dtl = "T_iec_debit_DTL";
          
            InCode = "DB";
        }
        string subparty = ddlTallySubPartyName.SelectedItem.Text;
        if (subparty == "~Select~")
        {
            subparty = "";
        }
        string CID = (string)Session["ContractID"];

        string sqlQuery = " insert into " + Bill_Mst + "(invoice,invoiceDate,compName,Address1,address2,City,pincode,state," +
                          " phone,partyReff,jobNo,BLNo,BENoDate,importitem,Quantity,Ass_value,Container_no,Custom_Duty," +
                          " subTotal,subtotalTax,STaxPercent,service_Tax,edu_cess,sec_chess," +
                          " Grand_total,less_advance,Net_total,sub_party,Nettotal_words,invoiceType,invoiceNo,Mode,contr_code,entryBy,eDate,fyear,TransportMode,suffix,notes,VGUID,BranchID,TallyAccountName,TallySubPartyName) values('" + sbNo + "','" + dates + "','" + txtCompName.Text + "'," +
                          " '" + txtAdd1.Text + "','" + Session["state"] + "','" + txtCity.Text + "','" + Session["Pin"] + "','" + Session["state"] + "','" + Session["Phone"] + "','" + txtParty_Reff.Text + "'," +
                          " '" + txtJobNo.Text + "','" + txtBLNo.Text + "','" + txtBENo.Text + "','" + txtImpotItem.Text + "','" + txtQty.Text + "'," +
                          " '" + txtAssValue.Text + "','" + txtNCNTR.Text  + "','" + txtCustomDuty.Text + "'," + st + "," + stTax + "," + staxp + "," +
                          " " + stax + "," + ec + "," + shc + "," + gt + "," + la + "," + nt + ",'" + txtSubParty.Text + "','" + txtRupees.Text + "'," +
                          "'" + InCode + "'," + invno + ",'" + MODE + "','" + CID + "','" + lblUser.Text + "','" + EntryDate + "','" + (string)Session["FinancialYear"] + "'," +
                          "'" + (string)Session["TransportMode"] + "','" + suffix + "','" + Notes + "','" + (string)Session["VGUID"] + "','" + (string)Session["BranchID"] + "','" + ddlTallyAccountName.SelectedItem.Text + "','" + subparty + "')";


        invFlag = 1;
        GetCommand(sqlQuery);

        int count = 1;
        foreach (GridViewRow ROW in GridView1.Rows)
        {
            TextBox amt = (TextBox)ROW.FindControl("amt1");
            TextBox receipt = (TextBox)ROW.FindControl("txtRECPT");
            CheckBox chk = (CheckBox)ROW.FindControl("chkSTAX");
          
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
            if (amount != "0.00")
            {
                string Query = "insert into " + Bill_Dtl + "(invoice,sno,charge_desc,receipt,amount,serviceTax) " +
                               "values('" + sbNo + "'," + count + ",'" + Charge_desc + "','" + receiptVal + "'," + amount + ",'" + sTAXval + "')";
                GetCommand(Query);
                count = count + 1;
            }

        }
        string fy = (string)Session["FinancialYear"];
        updateRNO(invno, InCode, fy);
        if (InCode == "SB")
        {
            //string Query = "update ejob_pos set bill_no='" + sbNo + "',bill_date='" + dates + "' where job_no='" + txtJobNo.Text + "'";
            //GetCommandMy(Query, strImpex);
           
            Response.Write("<script>" + "alert('Invoice has successfully Generated');" + "</script>");
        }
        else
        {
            //string Query = "update ejob_pos set db_note_no='" + sbNo + "',db_date='" + dates + "' where job_no='" + txtJobNo.Text + "'";
            //GetCommandMy(Query, strImpex);
           
            Response.Write("<script>" + "alert('Debit Note has successfully Generated');" + "</script>");
            Submit.Enabled = false;
            preview.Enabled = true ;
            btnMail.Enabled =  true;
        }
        // update Bill status
        //string blQuery = "update expjobstage set date='" + dates + "' where job_no='" + txtJobNo.Text + "' and job_stage='BDate'";
        //string blQueryJSU = "update iworkreg_dtl set date='" + dates + "' where job_no='" + txtJobNo.Text + "' and job_stage='BDate'";

        //GetCommandMy(blQuery, strImpex);
        
       
        BtnStandard.Visible = false;
        balance1.Visible = false;
        
        Session["INVOICECTR"] = sbNo;
        lblBill.Text = sbNo;
        
    }
    protected void updateRNO(int ino, string iType, string fy)
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
          
            string ActualRate = "";
            string FixedRate = "";
            string MinRate = "";
            string VarRate = "";
            string MaxRate = "";
            string varAmt = "";
            string varUnit = "";
            string CID = (string)Session["ContractID"];
            string assValue = txtAssValue.Text;
            string GWT = (string)Session["GRossWT"];
           

            string shpType = Session["shpType"].ToString();
            string cSize = Session["Contr_size"].ToString();
            string cType = Session["Contr_Type"].ToString();
            string status = "ACTIVE";
            pName = Session["pName"].ToString();
            if (shpType != "Air")
            {
                if (cType == "LCL")
                    shpType = "LCL";
                else
                {
                    if (cSize == "20")
                        shpType = "20Feet";
                    else if (cSize == "40")
                        shpType = "40Feet";
                }


            }

            string BILL = rbInvoice.SelectedValue;
            

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                TextBox amt = (TextBox)e.Row.FindControl("amt1");
                CheckBox chk = (CheckBox)e.Row.FindControl("chkSTAX");
                if (BILL != "SB")
                    chk.Enabled = false;
               
                string sno = e.Row.Cells[0].Text;
                string Charge_desc = e.Row.Cells[2].Text;
                string Query = "select * from M_Quote where CustomerName='" + pName + "' and Description='" + Charge_desc + "' and Type='" + shpType + "' ";
                //string Query = "select * from contract_mst m,contract_dtl s " +
                //           "where m.contr_code=s.contr_code and m.customer_name='" + pName + "' and  " +
                //           "m.contr_status='" + status + "' and s.charge_desc='" + Charge_desc + "' and " +
                //           "m.contr_code='" + CID + "' and s.sno in (" + sno + ")";
                SqlConnection cnn = new SqlConnection(strImpex);
                SqlDataAdapter da = new SqlDataAdapter(Query, cnn);
                DataSet ds = new DataSet();
                da.Fill(ds, "Contract");
                if (ds.Tables["Contract"].Rows.Count != 0)
                {
                    DataRowView row = ds.Tables["contract"].DefaultView[0];
                    CUnits = row["unit"].ToString();
                    ActualRate = row["ActualRate"].ToString();
                    FixedRate = row["FixRate"].ToString();
                    MinRate = row["MinRate"].ToString();
                    VarRate = row["VarRate"].ToString();
                    MaxRate = row["MaxRate"].ToString();
                    varAmt = row["VarRate"].ToString();
                    varUnit = row["VarType"].ToString();
                    SB = "YES";
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

                        if (ActualRate != "")
                        {
                            AMT = Convert.ToDouble(ActualRate) * Wt;
                            amt.Text = AMT.ToString("#0.00#");
                        }
                        else if (FixedRate != "")
                        {
                            AMT = Convert.ToDouble(FixedRate) * Wt;
                            amt.Text = AMT.ToString("#0.00#");
                        }
                        else
                        {
                            if (varAmt == "" || varAmt == "0")
                                varAmt = "1";

                            if (varUnit == "PER Kg")
                            {
                                string gWt = (string)Session["GRossWT"];
                                totalAmt = Convert.ToDouble(gWt) * Convert.ToDouble(varAmt);
                            }

                            //string Pro = "Variable";
                            //string varAmt = "";
                            //string varUnit = "";

                            //string QueryPro = "select * from M_Quote where QuoteNo='" + CID + "' and Description='" + Charge_desc + "' ";
                            //SqlConnection cnnPro = new SqlConnection(strImpex);
                            //SqlDataAdapter daPro = new SqlDataAdapter(QueryPro, cnnPro);
                            //DataSet dsPro = new DataSet();
                            //daPro.Fill(dsPro, "ContractPRO");
                            //if (dsPro.Tables["ContractPRO"].Rows.Count != 0)
                            //{
                            //    DataRowView RowPro = dsPro.Tables["ContractPRO"].DefaultView[0];

                            //}

                            string QueryPro1 = "select * from M_Quote where QuoteNo='" + CID + "' and Description='" + Charge_desc + "' and Type='" + shpType + "'  ";
                            SqlConnection cnnPro1 = new SqlConnection(strImpex);
                            SqlDataAdapter daPro1 = new SqlDataAdapter(QueryPro1, cnnPro1);
                            DataSet dsPro1 = new DataSet();
                            daPro1.Fill(dsPro1, "ContractPRO1");
                            DataTable dtPro1 = dsPro1.Tables[0];
                            foreach (DataRow rowPro in dtPro1.Rows)
                            {
                                CUnits = rowPro["unit"].ToString();
                                ActualRate = rowPro["ActualRate"].ToString();
                                FixedRate = rowPro["FixRate"].ToString();
                                MinRate = rowPro["MinRate"].ToString();
                                VarRate = rowPro["VarRate"].ToString();
                                MaxRate = rowPro["MaxRate"].ToString();
                                varAmt = rowPro["VarRate"].ToString();
                                varUnit = rowPro["VarType"].ToString();

                                // Amount = rowPro["MinRate"].ToString();
                                string product = "Minimum";

                                if (Amount == "")
                                    Amount = "1";
                                AMT = Convert.ToDouble(Amount);

                                if (MinRate != "")
                                {
                                    AMT = Convert.ToDouble(MinRate);
                                    if (totalAmt < AMT)
                                    {
                                        amt.Text = AMT.ToString("#0.00#");
                                        Session["Omin"] = amt.Text;
                                    }
                                    minVal = AMT;
                                }
                                else if (MaxRate != "")
                                {
                                    AMT = Convert.ToDouble(MaxRate);
                                    if (totalAmt > AMT)
                                    {
                                        amt.Text = AMT.ToString("#0.00#");
                                        Session["Omax"] = amt.Text;
                                    }
                                    maxVal = AMT;
                                    Session["OmaxVAL"] = "1";
                                }
                                else if (VarRate != "" && VarRate != "0")
                                {
                                    AMT = Convert.ToDouble(VarRate);
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
                        if (ActualRate != "")
                        {
                            AMT = Convert.ToDouble(Amount) * Wt;
                            amt.Text = AMT.ToString("#0.00#");
                        }
                        else if (FixedRate != "")
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
                                string Query1 = "select * from M_Quote where QuoteNo='" + CID + "' and Description='" + Charge_desc + "' and Type='" + shpType + "'  ";
                                //string Query1 = "select * from contract_mst m,contract_dtl s " +
                                //       "where m.contr_code=s.contr_code and m.customer_name='" + pName + "' and  " +
                                //       "m.contr_status='" + status + "' and s.charge_desc='" + Charge_desc + "' and " +
                                //       "m.contr_code='" + CID + "' order by s.product";
                                SqlConnection cnn1 = new SqlConnection(strImpex);
                                SqlDataAdapter da1 = new SqlDataAdapter(Query1, cnn1);
                                DataSet ds1 = new DataSet();
                                da1.Fill(ds1, "agch");
                                DataTable dt = ds1.Tables[0];
                                foreach (DataRow Row in dt.Rows)
                                {
                                    CUnits = Row["unit"].ToString();
                                    ActualRate = Row["ActualRate"].ToString();
                                    FixedRate = Row["FixRate"].ToString();
                                    MinRate = Row["MinRate"].ToString();
                                    VarRate = Row["VarRate"].ToString();
                                    MaxRate = Row["MaxRate"].ToString();
                                    varAmt = Row["VarRate"].ToString();
                                    varUnit = Row["VarType"].ToString();

                                    //Amount = Row["" + shpType + ""].ToString();
                                    //string product = Row["product"].ToString();

                                    //if (Amount == "")
                                    //    Amount = "0";
                                    //AMT = Convert.ToDouble(Amount) * Wt;



                                    if (MinRate != "")
                                    {
                                        AMT = Convert.ToDouble(MinRate) * Wt;
                                        if (gVAL < AMT)
                                        {
                                            amt.Text = AMT.ToString("#0.00#");
                                            Session["min"] = amt.Text;
                                        }
                                        minVal = AMT;
                                    }
                                    else if (MaxRate != "")
                                    {
                                        AMT = Convert.ToDouble(MaxRate) * Wt;
                                        if (gVAL > AMT)
                                        {
                                            amt.Text = AMT.ToString("#0.00#");
                                            Session["max"] = amt.Text;
                                        }
                                        maxVal = AMT;
                                        Session["MAXVAL"] = "1";
                                    }
                                    else if (VarRate != "")
                                    {
                                        AMT = Convert.ToDouble(VarRate) * Wt;
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
               
                ////Calculate Service Tax
               
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

    protected void GetPERCENT()
    {
        string BILL = rbInvoice.SelectedValue;
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
    protected void GetChargeVariable(string pName, string status, string Charge_desc, string CID, string shpType, string GWT)
    {
        //start variable condition
        GetVariable(pName, status, Charge_desc, CID, shpType);
        string amt = "";
        string Amount = "";
        Double AMT;
      
        string vers = Session["Variable"].ToString();
        if (vers == "")
            vers = "1";
        Double veriablePercentage = Convert.ToDouble(vers);
        Double GrossWT = Convert.ToDouble(GWT);
        Double gVAL = GrossWT * veriablePercentage;

        string Query1 = "select * from contract_mst m,contract_dtl s " +
               "where m.contr_code=s.contr_code and m.customer_name='" + pName + "' and  " +
               "m.contr_status='" + status + "' and s.charge_desc='" + Charge_desc + "' and " +
               "m.contr_code='" + CID + "'";
        SqlConnection cnn1 = new SqlConnection(strImpex);
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
        string Query1 = "select * from M_Quote where QuoteNo='" + CID + "' and Description='" + Charge_desc + "' and Type='" + shpType + "'  ";
        //string Query1 = "select * from contract_mst m,contract_dtl s " +
        //             "where m.contr_code=s.contr_code and m.customer_name='" + pName + "' and  " +
        //             "m.contr_status='" + status + "' and s.charge_desc='" + Charge_desc + "' and " +
        //             "m.contr_code='" + CID + "' and s.product='" + product + "'";
        SqlConnection cnn1 = new SqlConnection(strImpex);
        SqlDataAdapter da1 = new SqlDataAdapter(Query1, cnn1);
        DataSet ds1 = new DataSet();
        da1.Fill(ds1, "agch");
        if (ds1.Tables["agch"].Rows.Count != 0)
        {
            DataRowView row = ds1.Tables["agch"].DefaultView[0];
            string amt = row["VarRate"].ToString();
            Session["Variable"] = amt;
        }
        else
        {
            string Query2 = "select * from M_Quote where QuoteNo='" + CID + "' and Description='" + Charge_desc + "' and Type='" + shpType + "'  ";
            //string Query2 = "select * from contract_mst m,contract_dtl s " +
            //         "where m.contr_code=s.contr_code and m.customer_name='" + pName + "' and  " +
            //         "m.contr_status='" + status + "' and s.charge_desc='" + Charge_desc + "' and " +
            //         "m.contr_code='" + CID + "'";
            SqlConnection cnn2 = new SqlConnection(strImpex);
            SqlDataAdapter da2 = new SqlDataAdapter(Query2, cnn2);
            DataSet ds2 = new DataSet();
            da2.Fill(ds2, "DescVal");
            if (ds2.Tables["DescVal"].Rows.Count != 0)
            {
                DataRowView Row = ds2.Tables["DescVal"].DefaultView[0];
                string ActualRate = Row["ActualRate"].ToString();
                string FixedRate = Row["FixRate"].ToString();
                string MinRate = Row["MinRate"].ToString();
                string VarRate = Row["VarRate"].ToString();
                string MaxRate = Row["MaxRate"].ToString();
                string amt1 = "";
                if (ActualRate != "")
                {
                    amt1 = ActualRate;
                }
                else if (FixedRate != "")
                {
                    amt1 = FixedRate;
                }
                else if (MinRate != "")
                {
                    amt1 = FixedRate;
                }
                else if (VarRate != "")
                {
                    amt1 = FixedRate;
                }
                else if (MaxRate != "")
                {
                    amt1 = FixedRate;
                }
                //string amt1 = Row["" + shpType + ""].ToString();
                Session["Variable"] = amt1;
            }
        }
    }
    protected void amt1_TextChanged(object sender, EventArgs e)
    {
        GetTransaction();


    }
    protected void LKRupees_Click(object sender, EventArgs e)
    {
        txtRupees.Text = RsConvert.rupees(Convert.ToInt64(balance1.Text));
        Submit.Focus();
    }
    protected void preview_Click(object sender, EventArgs e)
    {
      
        string format = "PD";
        Session["BillFormat"] = format;
        //ScriptManager.RegisterStartupScript(this, this.GetType(), "popup", "window.open('../Billing/CryInvoiceReportSTax.aspx','_blank','menubar=no, scrollbars=yes, toolbar=no, location=no, resizable=yes, left=20, top=20');", true);
        Session["InvNo"] = lblBill.Text;
        string rep = (string)Session["InvNo"];
        string sub = rep.Substring(4, 2);
        if (sub == "SB")
        {
            Response.Redirect("../frmImpInvoiceReport.aspx");
        }
        else
        {
            Response.Redirect("../frmDebit.aspx");
        }
    }
    protected void GridView2_SelectedIndexChanged(object sender, EventArgs e)
    {
        GridView3.Visible = true;
        GridScroll.Visible = true;
        string CID = Convert.ToString(GridView2.SelectedDataKey.Value);
        string Bill = "YES";
       
        string BType = rbInvoice.SelectedValue;
        if (BType == "")
            BType = "SB";
        string Query = "";
        if ((string)Session["Contr_Type"] == "LCL")
        {
            Query = "select * from M_Quote where QuoteNo='" + CID + "' and Type='" + (string)Session["Contr_Type"] + "' order by ID ";
        }
        else if ((string)Session["Contr_Type"] == "FCL")
        {
            if ((string)Session["Contr_size"] == "20")
            {
                Query = "select * from M_Quote where QuoteNo='" + CID + "' and  Type='20Feet' order by ID ";
            }
            else if ((string)Session["Contr_size"] == "40")
            {
                Query = "select * from M_Quote where QuoteNo='" + CID + "' and  Type='40Feet'  order by ID ";
            }
        }
        else
        {
            Query = "select * from M_Quote where QuoteNo='" + CID + "' and  Type='AIR' order by ID  ";
        }


       // string Query = "select * from contract_dtl where contr_code='" + CID + "' and " + BType + "='" + Bill + "' order by sno";

        GridView3.DataSource = GetDataSQL(Query);
        GridView3.DataBind();
        Session["ContractID"] = CID;
        BtnContract_Submit.Visible = true;
    }
    protected void BtnContract_Submit_Click(object sender, EventArgs e)
    {
        Submit.Enabled = true;
        Session["PageLoad"] = "1";
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
            
            txtSubParty.Text = "";
        }
        
    }

    //protected void PartyADDR(string pcode, string paddr)
    //{
    //    MySqlConnection conn = new MySqlConnection(strImpex);
    //    string sqlQuery4 = "select *  from prt_mast m,prt_addr a " +
    //                              "where m.party_code=a.party_code and  m.party_code='" + pcode + "' and a.addr_code='" + paddr + "'";
    //    MySqlDataAdapter da4 = new MySqlDataAdapter(sqlQuery4, conn);
    //    DataSet ds4 = new DataSet();
    //    da4.Fill(ds4, "prtMast");
    //    DataRowView row4 = ds4.Tables["prtMast"].DefaultView[0];
    //    txtCompName.Text = row4["party_name"].ToString();
    //    Session["pName"] = row4["party_name"].ToString();
    //    string pName = txtCompName.Text;
    //    txtAdd1.Text = row4["address"].ToString();
    //    string city = row4["city"].ToString();
    //    string pin = row4["pin"].ToString();
    //    Session["Pin"] = pin;
    //    txtCity.Text = city;
    //    Session["state"] = row4["state"].ToString();
    //    Session["Phone"] = row4["tel_no"].ToString();
    //}

    protected void GetContractInfo(string sel)
    {
        string CID = (string)Session["ContractID"];
        string AssValue = txtAssValue.Text;
        pName = Session["pName"].ToString();
        string status = "ACTIVE";
        string strchargeVal = sel.TrimEnd(',');
        Session["strChargeVal"] = strchargeVal;
        string strQuery = "select * from M_Quote where QuoteNo='" + CID + "' and CustomerName='" + pName + "'  and ID in(" + strchargeVal + ") ";
       // string strQuery = "select * from contract_mst m,contract_dtl s where m.contr_code=s.contr_code and m.contr_code='" + CID + "' and m.customer_name='" + pName + "' and m.contr_status='" + status + "' and s.sno in(" + strchargeVal + ")";
        SqlConnection cnn = new SqlConnection(strImpex);
        SqlDataAdapter daS = new SqlDataAdapter(strQuery, cnn);
        DataSet dsS = new DataSet();
        daS.Fill(dsS, "Contract");
        if (dsS.Tables["Contract"].Rows.Count != 0)
        {
           // GridView1.DataSource = GetDataSQL(strQuery);
            GridView1.DataSource = dsS;
            GridView1.DataBind();
            GridView1.Visible = true;
           
        }
    }
    protected void GridView3_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        string shpType = Session["shpType"].ToString();
        string cSize = Session["Contr_size"].ToString();
        string cType = Session["Contr_Type"].ToString();
        string Bill = rbInvoice.SelectedValue;
        

        //if (shpType != "AIR")
        //{
        //    if (cType == "FCL")
        //    {
        //        if (cSize == "20")
        //            shpType = "ft20";
        //        else if (cSize == "40")
        //            shpType = "ft40";
        //    }
        //    else if (cType == "LCL")
        //        shpType = "LCL";
        //    else
        //        shpType = "break_bulk";
        //}



        //if (e.Row.RowType == DataControlRowType.Header)
        //{

        //    if (Bill == "SB")
        //    {
        //        e.Row.Cells[10].Visible = true;
        //        e.Row.Cells[11].Visible = false;
        //    }
        //    else
        //    {
        //        e.Row.Cells[10].Visible = false;
        //        e.Row.Cells[11].Visible = true;
        //    }
        //    switch (shpType)
        //    {
        //        case "AIR":
        //            {
        //                e.Row.Cells[5].Visible = true;
        //                e.Row.Cells[6].Visible = false;
        //                e.Row.Cells[7].Visible = false;
        //                e.Row.Cells[8].Visible = false;
        //                e.Row.Cells[9].Visible = false;
        //                break;
        //            }
        //        case "break_bulk":
        //            {
        //                e.Row.Cells[5].Visible = false;
        //                e.Row.Cells[6].Visible = true;
        //                e.Row.Cells[7].Visible = false;
        //                e.Row.Cells[8].Visible = false;
        //                e.Row.Cells[9].Visible = false;
        //                break;
        //            }
        //        case "LCL":
        //            {
        //                e.Row.Cells[5].Visible = false;
        //                e.Row.Cells[6].Visible = false;
        //                e.Row.Cells[7].Visible = true;
        //                e.Row.Cells[8].Visible = false;
        //                e.Row.Cells[9].Visible = false;
        //                break;
        //            }

        //        case "ft20":
        //            {
        //                e.Row.Cells[5].Visible = false;
        //                e.Row.Cells[6].Visible = false;
        //                e.Row.Cells[7].Visible = false;
        //                e.Row.Cells[8].Visible = true;
        //                e.Row.Cells[9].Visible = false;
        //                break;
        //            }
        //        case "ft40":
        //            {
        //                e.Row.Cells[5].Visible = false;
        //                e.Row.Cells[6].Visible = false;
        //                e.Row.Cells[7].Visible = false;
        //                e.Row.Cells[8].Visible = false;
        //                e.Row.Cells[9].Visible = true;
        //                break;
        //            }

        //    }
        //}
        //if (e.Row.RowType == DataControlRowType.DataRow)
        //{

        //    CheckBox chklist = (CheckBox)e.Row.FindControl("chk");
        //    string charge = e.Row.Cells[2].Text;
        //    string product = e.Row.Cells[3].Text;
        //    e.Row.Cells[1].ForeColor = Color.White;


        //    if (charge == "Agency charges" && (product == "Variable" || product == "Maximum"))
        //    {
               
        //        chklist.Enabled = false;
               
        //    }

        //    if (Bill == "SB")
        //    {
        //        e.Row.Cells[10].Visible = true;
        //        e.Row.Cells[11].Visible = false;
        //    }
        //    else
        //    {
        //        e.Row.Cells[10].Visible = false;
        //        e.Row.Cells[11].Visible = true;
        //    }
        //    switch (shpType)
        //    {
        //        case "AIR":
        //            {
        //                e.Row.Cells[5].Visible = true;
        //                e.Row.Cells[6].Visible = false;
        //                e.Row.Cells[7].Visible = false;
        //                e.Row.Cells[8].Visible = false;
        //                e.Row.Cells[9].Visible = false;
        //                break;
        //            }

        //        case "break_bulk":
        //            {
        //                e.Row.Cells[5].Visible = false;
        //                e.Row.Cells[6].Visible = true;
        //                e.Row.Cells[7].Visible = false;
        //                e.Row.Cells[8].Visible = false;
        //                e.Row.Cells[9].Visible = false;
        //                break;
        //            }
        //        case "LCL":
        //            {
        //                e.Row.Cells[5].Visible = false;
        //                e.Row.Cells[6].Visible = false;
        //                e.Row.Cells[7].Visible = true;
        //                e.Row.Cells[8].Visible = false;
        //                e.Row.Cells[9].Visible = false;
        //                break;
        //            }
        //        case "ft20":
        //            {
        //                e.Row.Cells[5].Visible = false;
        //                e.Row.Cells[6].Visible = false;
        //                e.Row.Cells[7].Visible = false;
        //                e.Row.Cells[8].Visible = true;
        //                e.Row.Cells[9].Visible = false;
        //                break;
        //            }
        //        case "ft40":
        //            {
        //                e.Row.Cells[5].Visible = false;
        //                e.Row.Cells[6].Visible = false;
        //                e.Row.Cells[7].Visible = false;
        //                e.Row.Cells[8].Visible = false;
        //                e.Row.Cells[9].Visible = true;
        //                break;
        //            }
        //    }
        //}


    }
    protected void LessAd_TextChanged(object sender, EventArgs e)
    {
       
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
            Response.Write("<script>window.close();</script>");
           

            Response.Redirect("~/pimpex.aspx", false);//All one has to do is set the endResponse property of Response.Redirect to be false.
            // avoid for thread abort exception error
        }
        catch (Exception ex)
        {
            Response.Write("<script>alert('" + ex.Message + "')</script>");

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
                SqlConnection conn = new SqlConnection(strImpex);
                string sqlQuery = "select *  from M_AccountDetails where Accountcode='" + pcode + "' and BranchId=" + NO + "";
               // string sqlQuery = "select *  from prt_addr where party_code='" + pcode + "' and addr_num=" + NO + "";
                SqlDataAdapter da = new SqlDataAdapter(sqlQuery, conn);
                DataSet ds = new DataSet();
                da.Fill(ds, "addr");
                

                DataRowView row = ds.Tables["addr"].DefaultView[0];
                string addr1 = row["Address1"].ToString();
                string city = row["City"].ToString();
                string state = row["State"].ToString();
                string pin = row["Pincode"].ToString();
                Session["addr"] = addr1;
                Session["city"] = city;
                Session["state"] = state;
                Session["Pin"] = pin;
                Session["BCODE"] = NO;
                txtCity.Text = city;
                Session["BranchID"] = row["BranchId"].ToString();
                
                txtAdd1.Text = addr1;
                Session["Phone"] = row["PhoneNo"].ToString();


            }
        }
        GrdADDRSCROLL.Visible = false;
        GrdPaddr.Visible = false;
        TrAddr.Visible = false;
        TrAddr1.Visible = false;
      
    }
    public DataSet PartyAddr(string pcode)
    {
        SqlConnection conn = new SqlConnection(strImpex);
        conn.Open();
        string sqlQuery4 = "select *  from M_AccountDetails " +
                               "where AccountName='" + pcode + "' ";
        SqlDataAdapter da4 = new SqlDataAdapter(sqlQuery4, conn);
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
        }
        else
        {
            GrdADDRSCROLL.Visible = false;
            
            GrdPaddr.Visible = false;
            TrAddr.Visible = false;
            TrAddr1.Visible = false;
        }
    }
    protected void chkSTAX_CheckedChanged(object sender, EventArgs e)
    {
        GetTransaction();
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
    protected void txtJNO_TextChanged(object sender, EventArgs e)
    {

        GetFundRequest();
        string jobNo = drJobNo.SelectedValue;
        string Query = "";
        string bill = rbInvoice.SelectedValue;
        if (bill == "SB")
            Query = "select * from M_iec_invoicenew where jobno = '" + jobNo + "'";
        else
            Query = "select * from M_iec_debit where jobno = '" + jobNo + "'";
        SqlConnection conn = new SqlConnection(strImpex);

        SqlDataAdapter da = new SqlDataAdapter(Query, conn);

        DataSet ds = new DataSet();
        da.Fill(ds, "bill");
        if (ds.Tables["bill"].Rows.Count != 0)
        {
           
            DataRowView row = ds.Tables["bill"].DefaultView[0];
            string eXISTbILL = row["invoice"].ToString();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Given jobs has already billing. The Bill No. " + eXISTbILL + "');", true);
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

    protected void drServiceTax_SelectedIndexChanged(object sender, EventArgs e)
    {
        GetTransaction();
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
        drJobNo.DataValueField = "jobno";
        drJobNo.DataTextField = "jobno";
        drJobNo.DataBind();
        drJobNo.Items.Insert(0, new ListItem("~Select~", "0"));

    }
    protected void rbInvoice_SelectedIndexChanged(object sender, EventArgs e)
    {
        GenerateBillNo();
    }
    protected void GenerateBillNo()
    {
       
        string fy = (string)Session["FinancialYear"];

       
        string jobNo = txtJobNo.Text;
        string BILL = rbInvoice.SelectedValue;
     
        string strQuery = "";
        string sbNo = "";

        if (BILL == "SB")
        {
         
            sbNo = (string)Session["BranchShortName"] + "/SB/";

            strQuery = "select * from M_RunningNo where iectype like '" + BILL + "' and Fyear='" + fy + "'";
           

            lblIName.Text = "CONTRACT INVOICE - EXPORTS";
        }
        else
        {
            
            sbNo = (string)Session["BranchShortName"] + "/DB/";

            strQuery = "select * from M_RunningNo where iectype like '" + BILL + "' and Fyear='" + fy + "'";
         
            lblIName.Text = "CONTRACT DEBIT NOTE - EXPORTS";
        }
        SqlConnection cnn = new SqlConnection(strImpex);
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
            Session["JOBNO"] = txtJobNo.Text;
            Session["MAILBUTTON"] = "OK";
            Session["PageName"] = "PIPLInvoiceStax.aspx";
            Session["Maill"] = "SendMaill";
            ScriptManager.RegisterStartupScript(this, this.GetType(), "popup", "window.open('CryInvoiceReportSTax.aspx','_blank','menubar=no, scrollbars=yes, toolbar=no, location=no, resizable=yes, left=20, top=20');", true);

           
        }

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
           
            txtNote.Text =txtNote.Text+"-"+ "Supplier Inv No :" + supplier.TrimStart(',');
            txtNote.Text = txtNote.Text.TrimStart('-');
        }
        else
        {
           txtNote.Text = "";
        }
    }


}
