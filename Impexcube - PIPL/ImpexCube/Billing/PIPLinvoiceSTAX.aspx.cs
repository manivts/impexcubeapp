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
using Microsoft.Office.Interop.Outlook;
using Exception = System.Exception;
using System.Web.Services;
using System.Collections.Generic;

public partial class PIPLinvoiceSTAX : System.Web.UI.Page 
{
    VTS.ImpexCube.Utlities.Utility InvSequence = new VTS.ImpexCube.Utlities.Utility();
   // string strconn="Provider=Microsoft.Jet.OLEDB.4.0; Data Source=D:\\PIPL\\Classification.mdb; User Id=admin; Password=";
 //   string strconn = (string)ConfigurationManager.AppSettings["ConnectionString"];
    //string strconn = (string)ConfigurationManager.AppSettings["ConnectionImpex"];
    //string strconn1 = (string)ConfigurationManager.AppSettings["ConnectionVisual"];
    //string strconnJSU = (string)ConfigurationManager.AppSettings["ConnectionJobStages"];

    string strImpex = (string)ConfigurationManager.ConnectionStrings["Constr"].ConnectionString;
      
        string cUNIT = "";
        string strCharge = "";
        public string partyname = "";
        string pName = "";
        Double Wt = 1;
        Double totalAmt;
        Double minVal;
        Double maxVal;
        DataSet ds1 = new DataSet();

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
       fyear = (string)Session["FinancialYear"];
   
       
        if (IsPostBack == false)
        {
            //TallyAccountName();
            Session["VGUID"] = Guid.NewGuid().ToString();
            llbHead.Text = (string)Session["companyname"];
            string lfyear = (string)Session["Lfyear"];
            Session["FYEARBill"] = fyear;
            chk.Text = lfyear;
            //view Gridview for transaction tables
            GetXML();
            tblGrid.Visible = false;
            //Submit.Enabled = false;
            btnMail.Enabled = false;
            preview.Enabled = false;
            ExportTally.Enabled = false;
            string strQuery = "select * from M_ServiceMaster where fyear='" + fyear + "' order by fyear desc";
            drServiceTax.DataSource = GetDataSQL(strQuery);
            drServiceTax.DataTextField = "sTax";
            drServiceTax.DataValueField = "serviceTax";
            drServiceTax.DataBind();

            Session["RBMODE"] = "IMP";
            TrAddr.Visible=false;
            TrAddr1.Visible=false;
            Session["Invoice"] = "Invoice";
            Session["IECName"] = "";
            Session["IECAdd1"] = "";
            Session["IECAdd2"] = "";
            Session["IECCity"] = "";
            Session["Pin"] = "";
            Session["Phone"] = "";
            //try
            //{
            //    //rbBill.SelectedValue = "DP";
            //}
            //catch (Exception ex)
            //{
            //    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('" + ex.Message + "');", true);
            //}
            txtCompName.Text = (string)Session["IECName"];
            txtAdd1.Text = (string)Session["IECAdd1"];
            txtCity.Text = (string)Session["IECCity"];
            string Head = Session["RBMODE"].ToString();
            if (Request.QueryString["mode"] == "Contract")
            {
                lblIName.Text = "CONTRACT INVOICE - IMPORTS";
            }
            else
            {
                lblIName.Text = "INVOICE - IMPORTS";
            }

           
            string LNA = (string)Session["Invoice"];
            string dates = DateTime.Now.ToString("dd/MM/yyyy");
            invDate.Text = dates;
            //if ((string)Session["Invoice"] == "Invoice")
            //{
                string tp = "SB";
                lblINumber.Text = "INV. NO.:";
                InvoiceGenerated(tp);
            //}
            //else
            //{
            //    lblINumber.Text = "DEBIT NO.:";
            //}
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
           
            txtNCNTR.Text  = Values[15];
            txtParty_Reff.Text = Values[16];
            txtCustomDuty.Text = Values[17];
            txtNote.Text = Values[18];
        
        }
    }
    protected void Button1_Click(object sender, EventArgs e)
    {
        Session["BasicInformation"] = txtJNO.Text + "~" + txtCompName.Text + "~" + txtJobNo.Text + "~" + txtSubParty.Text + "~" + (string)Session["BLNo"] + "~" + txtAdd1.Text + "~" + (string)Session["BENo"] + "~" + txtAdd1.Text + "~" + txtImpotItem.Text + "~" + txtCity.Text + "~" + txtQty.Text  + "~" + (string)Session["state"] + "~" + (string)Session["Pin"] + "~" + txtAssValue.Text + "~" + (string)Session["Phone"] + "~" + txtNCNTR.Text + "~" + txtParty_Reff.Text + "~" + txtCustomDuty.Text + "~" + txtNote.Text;
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
            string strQuery = "SELECT * FROM T_iec_invoiceNew_DTL where invoice='" + InvNumber + "' order by sno";
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
            Submit.Enabled = true;
        }
    }
   
    protected void GetXML()
    {
        DataSet dss = new DataSet();
        dss.ReadXml(Server.MapPath("XML\\inv.xml"));
        {
            GridView1.DataSource = dss.Tables[1];
           // GridView1.DataMember = "Detail";
            GridView1.DataBind();
            

        }
        //foreach (GridViewRow row in GridView1.Rows)
        //{

        //    CheckBox chk = (CheckBox)row.FindControl("chkSTAX");
        //    chk.Enabled = false;
        //}
    }
  
    //public DataSet GetData(string fy)
    //{
    //    MySqlConnection conn1 = new MySqlConnection(strconn1);
    //    conn1.Open();
    //    string sqlStatement1 = "select *  from iworkreg i,ijob_pos j where i.job_no=j.job_no " +
    //                           "and i.job_no like '%" + fy + "%' and j.bill_date is null order by i.jobsno";

    //    MySqlDataAdapter da1 = new MySqlDataAdapter(sqlStatement1, conn1);
        
    //        DataSet ds1 = new DataSet();
    //        da1.Fill(ds1, "ijobno");
    //        conn1.Close();
    //        return ds1;
       
    //}
 
    protected void BtnStandard_Click(object sender, EventArgs e)
    {
        if (Request.QueryString["mode"] == "Contract")
        {
            ContractInvoiceGO();
            //ScriptManager.RegisterStartupScript(Page, typeof(Page), "OpenWindow", "window.open('/popup.aspx');", true);
           // string queryString = "popup.aspx";
           // string newWin = "window.open('" + queryString + "');";
            //ClientScript.RegisterStartupScript(this.GetType(), "pop", newWin, true);
           // string partyname = txtCompName.Text;
            //ScriptManager.RegisterStartupScript(this, this.GetType(), "javascript", "branchpopup('" + partyname + "');", true);
           
        }
        else
        {
            DirectInvoiceGO();
        }
    }

    public void DirectInvoiceGO()
    {
     //   TallyAccountName();
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
                //Submit.Enabled = true;
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
                    txtAssValue.Text = row["TotalAssval"].ToString();
                    txtCustomDuty.Text = row["TotalDuty"].ToString();
                    //string item = row["inv_dtl"].ToString();
                    //item = item.Replace("'", " ");
                    //Session["ImpotItem"] = item;
                    //txtCustomDuty.Text = (string)Session["CustomDuty"];
                    //txtImpotItem.Text = (string)Session["ImpotItem"];
                    //string pcode = row["party_code"].ToString();
                    string sType = row["mode"].ToString();
                    Session["TransportMode"] = sType;
                    GetItemImport(jobNo);
                    //Session["PCODE"] = pcode;
                    if (sType == "Air")
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
                    //    //DateTime beDate = Convert.ToDateTime(bedate);
                    //   bedate = row["bedate"].ToString();
                    //   Session["BENo"] = be + " dt." + bedate;
                    //}
                    txtBENo.Text = be.Trim();
                    txtBEDate.Text = bedate;
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

                    string sqlQuery3 = "select *  from T_ShipmentContainerInfo where jobno='" + jobNo + "' order by TransId";
                    conn.Open();
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
                            //snos = row3["sr_no"].ToString();
                            cno = row3["ContainerNo"].ToString();
                            cTyp = row3["LoadType"].ToString();
                            cSize = row3["ContainerType"].ToString();
                            CNTRNO = CNTRNO + cno + ",";
                        }
                        txtNote.Text = CNTRNO.TrimEnd(',');
                        Session["NOTE"] = txtNote.Text;

                        string pref = "";

                        //pref = snos + "x" + cSize + " Ft - " + cTyp;
                        pref = cSize + " Ft - " + cTyp;
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
                    DataRowView row4 = ds4.Tables["prtMast"].DefaultView[0];
                    // string cCode = row4["group_id"].ToString();
                    //Session["cCode"] = cCode;
                    //if (cCode == "")
                    //{Session["BranchID"]
                    Session["BranchID"] = row4["BranchSno"].ToString();
                    if ((string)Session["BranchID"] == "")
                    {
                        Session["BranchID"] = "0";
                    }
                    txtCompName.Text = row4["Importer"].ToString();
                    try
                    {
                        string VchType = "SB";
                        txtInvSeqNo.Text = Convert.ToString(InvSequence.InvSeqNO(row4["Importer"].ToString(), VchType, txtJNO.Text));
                    }
                    catch
                    {
                    }
                    Session["SubParty"] = row4["Importer"].ToString();
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
                    //rbBill.Visible = false;

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
                    //GetSQLJOBS(txtJNO.Text);
                    //Submit.Enabled = true;
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

    //protected void GrdPaddr_SelectedIndexChanged(object sender, EventArgs e)
    //{

    //    for (int i = 0; i < GrdPaddr.Rows.Count; i++)
    //    {
    //        if (GrdPaddr.SelectedIndex == i)
    //        {
    //            string NO = Convert.ToString(GrdPaddr.SelectedDataKey.Value);
    //            string pcode = GrdPaddr.Rows[i].Cells[0].Text;
    //            SqlConnection conn = new SqlConnection(strImpex);
    //            conn.Open();
    //            string sqlQuery = "select *  from M_AccountDetails where Accountcode='" + pcode + "' and BranchId=" + NO + "";
    //            SqlDataAdapter da = new SqlDataAdapter(sqlQuery, conn);
    //            DataSet ds = new DataSet();
    //            da.Fill(ds, "addr");
    //            conn.Close();

    //            DataRowView row = ds.Tables["addr"].DefaultView[0];
                
    //            string addr1 = row["Address1"].ToString();
    //            string city = row["City"].ToString();
    //            string state = row["State"].ToString();
    //            string pin = row["Pincode"].ToString();
    //            Session["addr"] = addr1;
    //            Session["city"] = city;
    //            Session["state"] = state;
    //            Session["Pin"] = pin;
    //            Session["BCODE"] = NO;
    //            txtCity.Text = city;
    //            Session["compname"] = row["BranchName"].ToString();
    //            Session["BranchID"] = row["BranchId"].ToString();
    //            txtAdd1.Text = addr1;
    //            Session["Phone"] = row["PhoneNo"].ToString();
    //            //ContractInvoiceDesc();

    //        }
    //    }
    //    GrdADDRSCROLL.Visible = false;
    //    GrdPaddr.Visible = false;
    //    trMain.Visible = true;
    //    TrAddr.Visible = true;
    //    TrAddr1.Visible = true;

    //}

    //public void ContractInvoiceDesc()
    //{
    //    SqlConnection conn = new SqlConnection(strImpex);
    //    conn.Open();
    //    string sqlQuery = "select Description from M_Quote where CustomerName = '" + Session["compname"] + "' ";
    //    SqlDataAdapter da = new SqlDataAdapter(sqlQuery,conn);
    //    DataSet ds = new DataSet();
    //    da.Fill(ds,"Details");
    //    conn.Close();

    //    for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
    //    {
    //        TextBox Particulars = (TextBox)GridView1.Rows[i].FindControl("txtDetails");
    //        Particulars.Text = ds.Tables[0].Rows[i]["Description"].ToString();
    //    }
    //}


    protected void ContractInvoiceGO()
    {
       // TallyAccountName();
        Session["Company"] = "Std";

        string jno = txtJNO.Text;
        //string Bill = rbInvoice.SelectedValue;
        //Session["BILL"] = Bill;
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
                conn.Open();
                string sqlQuery = "";
                if (chk.Checked == true)
                    sqlQuery = "select *  from View_ImportJobStatusUpdate where jobno='" + jno + "' ";
                else
                    sqlQuery = "select *  from View_ImportJobStatusUpdate where jobno='" + jno + "' ";
                SqlDataAdapter da = new SqlDataAdapter(sqlQuery, conn);
                DataSet ds = new DataSet();
                da.Fill(ds, "iworkreg");

                conn.Close();
                if (ds.Tables["iworkreg"].Rows.Count != 0)
                {
                    //Submit.Enabled = true;
                    DataRowView row = ds.Tables["iworkreg"].DefaultView[0];
                    string jobNo = row["jobno"].ToString();
                    txtJobNo.Text = jobNo;

                    txtAssValue.Text = row["TotalAssVal"].ToString();
                    txtCustomDuty.Text = row["TotalDuty"].ToString();

                    string item = row["InvoiceDetail"].ToString();
                    item = item.Replace("'", " ");
                    txtImpotItem.Text = item;

                    string pcode = row["Importer"].ToString();
                    string paddr = row["Importer"].ToString();//here it goes wrong 
                    Session["pin"] = pcode;
                    Session["PCODE"] = pcode;
                    Session["PBranch"] = paddr;
                    GetItemImport(jobNo);

                    //  Get Party Address
                    //GrdPaddr.DataSource = PartyAddr(pcode);
                    //GrdPaddr.DataBind();

                    string shpType = row["Mode"].ToString();
                    Session["TransportMode"] = shpType;
                    if (shpType == "Air")
                    {
                        shpType = "Air";
                        Session["shpType"] = shpType;
                    }
                    else if (shpType == "Sea")
                    {
                        shpType = "SEA";
                        Session["shpType"] = shpType;
                    }
                    //string sqlQuery1 = "select *  from ishp_dtl where job_no='" + jobNo + "'";
                    //MySqlDataAdapter da1 = new MySqlDataAdapter(sqlQuery1, conn);
                    //DataSet ds1 = new DataSet();
                    //da1.Fill(ds1, "ishp");
                    //conn.Close();
                    //if (ds1.Tables["ishp"].Rows.Count == 0)
                    //    Response.Write("<script>alert('There is no Shipment Details for Given Jobs')</script>");
                    //else
                    //{
                    //DataRowView row = ds1.Tables["ishp"].DefaultView[0];
                    string bl = row["MasterBLNo"].ToString();
                    string BLDate = row["MasterBLDate"].ToString();

                    string hbl = row["HouseBLNo"].ToString();
                    string HBLDate = row["HouseBLDate"].ToString();

                    if (BLDate != "" || BLDate != string.Empty)
                        blDate = Convert.ToDateTime(BLDate);
                    if (HBLDate != "" || HBLDate != string.Empty)
                        hblDate = Convert.ToDateTime(HBLDate);
                    if (BLDate != "" || BLDate != string.Empty)
                        txtBLNo.Text = bl + " dt." + blDate.ToString("dd/MM/yyyy");
                    else
                        txtBLNo.Text = hbl + " dt." + hblDate.ToString("dd/MM/yyyy");
                    string pkg = row["NoOfPackages"].ToString();
                    string pkg_unit = row["PackagesUnit"].ToString();
                    string gross = row["GrossWeight"].ToString();
                    string gross_unit = row["GrossWeightUnit"].ToString();
                    Session["GRossWT"] = gross;
                    pkg = pkg.Replace(".000", "");
                    gross = gross.Replace(".000", "");

                    txtQty.Text = pkg + " " + pkg_unit + "/" + gross + " " + gross_unit;
                    // }
                    //string sqlQuery2 = "select *  from ijob_Pos where job_no='" + jobNo + "'";
                    //MySqlDataAdapter da2 = new MySqlDataAdapter(sqlQuery2, conn);
                    //DataSet ds2 = new DataSet();
                    //da2.Fill(ds2, "ijobs");
                    //conn.Close();
                    //DataRowView row2 = ds2.Tables["ijobs"].DefaultView[0];
                    string be = row["BENo"].ToString();
                    txtBENo.Text = row["BENo"].ToString();
                    Session["be"] = row["BENo"].ToString();
                    string bedate = row["BEDate"].ToString();


                    if (bedate == "")
                    {
                        txtBEDate.Text = "";
                    }
                    else
                    {
                        DateTime beDate = Convert.ToDateTime(bedate);
                        txtBEDate.Text = beDate.ToString("dd/MM/yyyy");

                    }
                  

                    string sqlQueryQTY = "select count(jobno) as QTY  from T_ShipmentContainerInfo where jobno='" + jobNo + "'";
                    SqlDataAdapter daQTY = new SqlDataAdapter(sqlQueryQTY, conn);
                    DataSet dsQTY = new DataSet();
                    daQTY.Fill(dsQTY, "iContrQTY");
                    conn.Close();
                    if (dsQTY.Tables["iContrQTY"].Rows.Count != 0)
                    {
                        DataRowView rowQTY = dsQTY.Tables["iContrQTY"].DefaultView[0];
                        string QTY = rowQTY["QTY"].ToString();
                        Session["QTY"] = QTY;
                    }
                    string sqlQuery3 = "select *  from T_ShipmentContainerInfo where jobno='" + jobNo + "' order by TransId";
                    SqlDataAdapter da3 = new SqlDataAdapter(sqlQuery3, conn);
                    DataSet ds3 = new DataSet();
                    da3.Fill(ds3, "iContr");
                    conn.Close();

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
                            snos = row3["ShipTransID"].ToString();
                            cno = row3["ContainerNo"].ToString();
                            cTyp = row3["LoadType"].ToString();
                            cSize = row3["Container"].ToString();
                            CNTRNO = CNTRNO + cno + ",";
                        }
                        txtNote.Text = CNTRNO.TrimEnd(',');

                        string pref = "";
                        //pref =snos + "x" + cSize + " Ft - " + cTyp;
                        pref = cSize + " Ft - " + cTyp;
                        txtNCNTR.Text = pref;
                        Session["Contr_size"] = cSize;
                        Session["Contr_Type"] = cTyp;

                    }
                    //end stype
                    else
                    {
                        Session["Contr_size"] = "";
                        Session["Contr_Type"] = "";
                    }

                    //start Direct party info
                    //string sqlQuery4 = "select *  from prt_mast m,prt_addr a " +
                    //                   "where m.party_code=a.party_code and  m.party_code='" + pcode + "' and a.addr_code='" + paddr + "'";
                    string sqlQuery4 = "select *  from M_AccountMaster where AccountName ='" + paddr + "'  ";
                    SqlDataAdapter da4 = new SqlDataAdapter(sqlQuery4, conn);
                    DataSet ds4 = new DataSet();
                    da4.Fill(ds4, "prtMast");
                    conn.Close();
                    //if(ds.Tables["prtMast"].Rows.Count!=0)
                    DataRowView row4 = ds4.Tables["prtMast"].DefaultView[0];
                    string cCode = row4["UnderAccName"].ToString();
                    Session["cCode"] = cCode;
                    if (cCode == "")
                    {
                        txtCompName.Text = row4["AccountName"].ToString();
                        Session["pName"] = row4["AccountName"].ToString();

                        pName = txtCompName.Text;
                        GridView2.Visible = false;
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
                        GridScroll.Visible = false;
                        GridView3.Visible = false;
                        //TrAddr.Visible = true;
                        TrAddr1.Visible = true;
                        //tblInv.Visible = false;
                        BtnContract_Submit.Visible = false;    
                        GrdADDRSCROLL.Visible = false;
                        GrdPaddr.Visible = false;
                      
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
                            Session["pin"] = pin;
                            txtCity.Text = city;

                            Session["state"] = row4["State"].ToString();

                            Session["Phone"] = row4["PhoneNo"].ToString();
                            TrAddr.Visible = true;

                            tblInv.Visible = true;
                                                        //TrAddr1.Visible = true;
                            //GrdADDRSCROLL.Visible = true;
                            //GrdPaddr.Visible = true;
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

                    txtAdd1.Text = addr;
                    //Start contract info     
                    // To Get Contract information for the selected Customers....
                    //string status = "ACTIVE";

                    trMain.Visible = false;
                    tblGrid.Visible = true;
                    //tblContr.Visible = true;
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

                    MainForm.Visible = false;
                    Tr1.Visible = false;
                    GridView2.DataSource = GetDataSQL(strQuery1);
                    GridView2.DataBind();

                    SubForm.Visible = true;
                    tblGrid.Visible = true;
                    trGrid.Visible = true;
                    GridView2.Visible = true;

                    GridView3.Visible = false;
                    GridScroll.Visible = false;

                    GrdADDRSCROLL.Visible = false;
                    GrdPaddr.Visible = false;

                }

                else
                {
                    Response.Write("<script>alert('Not Found Records')</script>");
                    trMain.Visible = true;
                    //tblContr.Visible = false;
                }


            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('" + ex.Message + "');", true);
            }


            //End Contract info
        }

        if (txtSubParty.Text != "")
        {
            ddlTallySubPartyName.Enabled = true;
        }
    }
    public class showbranchclass {
        public string blnumber { get; set; }
    }

    [WebMethod]
    public static showbranchclass[] showbranch(string pcode) //Get BL No
    {
        string strconn = (string)ConfigurationSettings.AppSettings["ConnectionVTS"];
        List<showbranchclass> Detail = new List<showbranchclass>();
        string SelectString = "select * from M_AccountDetails where AccountName='" + pcode + "'";
        SqlConnection cn = new SqlConnection(strconn);
        SqlCommand cmd = new SqlCommand(SelectString, cn);
        cn.Open();
        SqlDataAdapter da = new SqlDataAdapter(cmd);
        DataTable dtGetData = new DataTable();
        da.Fill(dtGetData);
        foreach (DataRow dtRow in dtGetData.Rows)
        {
            showbranchclass DataObj = new showbranchclass();
            DataObj.blnumber = dtRow["blnumber"].ToString();
            Detail.Add(DataObj);
        }
        return Detail.ToArray();
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
            Submit.Enabled = false;
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
                string Query = "select * from M_iec_invoiceNew where jobno = '" + jobNo + "' and invoice = '" + lblInvNo.Text + "'";

                DataSet ds = GetDataSQL(Query); 

                if (ds.Tables["SQLtable"].Rows.Count == 0)
                {
                    if (invFlag == 0)
                    {
                        PIPLInovice();
                    }
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
        //string iType="SB";
        //InvoiceGenerated(iType);
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
        string ADDRESS = txtAdd1.Text.Trim(' ','\r','\n');
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

        string subparty = ddlTallySubPartyName.SelectedItem.Text;
        if (subparty == "~Select~")
        {
            subparty = "";
        }
        string RuppeesWords = hdnRuppees.Value;
        string InvSeqNo = txtInvSeqNo.Text;

        SqlConnection conn = new SqlConnection(strImpex);
        string sqlQuery = " insert into M_iec_invoiceNew(invoice,invoiceDate,compName,Address1,address2,City,pincode,state," +
                          " phone,partyReff,jobNo,BLNo,BENoDate,importitem,Quantity,Ass_value,Container_no,Custom_Duty,subTotal,subTotalTax,staxPercent,Service_tax,Edu_Cess,SEC_Chess," +
                          " Grand_total,less_advance,Net_total,sub_party,Nettotal_words,invoiceType,invoiceNo,Mode,entryBy,eDate,fyear,TransportMode,suffix,notes,impRemark,interRemark,VGUID,BranchID,TallyAccountName,TallySubPartyName,InvSeqNo,SubPartyAddr) values('" + lblInvNo.Text + "','" + dates + "','" + txtCompName.Text.Replace("'", "") + "'," +
                          " '" + ADDRESS.Replace("'"," ") + "','" + (string)Session["state"] + "','" + txtCity.Text + "','" + (string)Session["Pin"] + "','" + (string)Session["state"] + "','" + (string)Session["Phone"] + "','" + pREFF + "'," +
                          " '" + txtJobNo.Text + "','" + txtBLNo.Text  + "','" + txtBENo.Text+" dt."+txtBEDate.Text  + "','" + impItem  + "','" + txtQty.Text  + "'," +
                          " '" + txtAssValue.Text + "','" + txtNCNTR.Text.Replace("'", "") + "','" + txtCustomDuty.Text + "'," + st + "," + stTax + ",'" + staxper + "'," + stax + "," + ec + "," + shc + "," +
                          " " + gt + "," + la + "," + nt + ",'" + txtSubParty.Text.Replace("'", "") + "','" + RuppeesWords + "','" + InCode + "'," + invno + ",'" + (string)Session["RBMODE"] + "','" + (string)Session["USER-NAME"] + "'," +
                          "'" + EntryDate + "','" + (string)Session["FinancialYear"] + "','" + (string)Session["TransportMode"] + "','" + suffix + "','" + Notes + "','" + impRK + "','" + intRK + "','" + (string)Session["VGUID"] + "','" + (string)Session["BranchID"] + "','" + ddlTallyAccountName.SelectedItem.Text + "','" + subparty.Replace("'", "") + "','" + InvSeqNo + "','" + txtSubPaartAddr.Text.Replace("'", "") + "')";
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
                updateRNO(invno, InCode, fyear);
                string strQuery1 = "Update T_JobCreation set status_job='Y', BENo = '" + Session["be"] + "', BEDate = '" + txtBEDate.Text + "',TotalAssVal = '" + txtAssValue.Text + "',TotalDuty = '" + txtCustomDuty.Text + "'  where JobNo='" + txtJNO.Text + "'";
                GetCommandIMP(strQuery1);
                try
                {
                    string VchType = "SB";
                    InvSequence.InvSeqNOSave(txtCompName.Text, VchType);
                    string jobseq = txtInvSeqNo.Text;
                    string InvSeque = string.Empty;
                    if (jobseq.Length==12)
                    {
                         InvSeque = txtInvSeqNo.Text.Substring(8, 4);
                    }
                    else if (jobseq.Length == 13)
                    {
                         InvSeque = txtInvSeqNo.Text.Substring(9, 4);
                    }
                    InvSequence.JobSeqNOSave(txtJobNo.Text, InvSeque);
                }
                catch
                {
                }
                //string billstatus = "update iworkreg_jobstatus set  bill_no='" + lblInvNo.Text + "',bill_date='" + dates + "',bill_amt='"+nt+"',status_job='Y' where job_no='" + txtJobNo.Text + "'";
                                   
                //GetCommand(billstatus, strconnJSU);

                if (flag != 0)
                {
                    invFlag = 1;
                    Response.Write("<script>" + "alert('Invoice has successfully Generated');" + "</script>");
                    BtnStandard.Visible = false;
                    Submit.Enabled = false;
                    btnMail.Enabled = true;
                    preview.Enabled = true;
                    ExportTally.Enabled = true;
                    balance1.Visible = false;
                    New.Visible = true;
                }
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('" + ex.Message + "');", true);
        }
    }

    protected void New_Click(object sender, EventArgs e)
          {
        if (Request.QueryString["mode"] == "Contract")
        {
            BtnStandard.Visible = true;
            Response.Redirect("~/Billing/PIPLinvoiceSTAX.aspx?mode=Contract");
        }
        else
        {
            BtnStandard.Visible = true;
            Response.Redirect("~/Billing/PIPLinvoiceSTAX.aspx?");
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
            string  Narration = Narrat.Text;
            double ServiceTaxPercent = Convert.ToDouble(ServiceTaxPer.SelectedValue);
            double ServiceTaxAmount = Convert.ToDouble(ServiceTaxAmt.Text);
            string amount = amt.Text;
            string Charge_desc = chrg.Text;
            string Receipt = recpt.Text;
            string sTAXval = string.Empty;
            if (ServiceTaxPer.SelectedValue =="0")
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
                string Query = "insert into T_iec_invoiceNew_DTL(invoice,sno,charge_desc,receipt,amount,serviceTax,Narration,ServiceTaxPercent,ServiceTaxAmount) " +
                               "values('" + sbNo + "'," + count + ",'" + Charge_desc.Replace("'", "") + "','" + Receipt.Replace("'", "") + "'," + amount + ",'" + sTAXval + "','" + Narration.Replace("'", "") + "','" + ServiceTaxPercent + "','" + ServiceTaxAmount + "')";
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
    protected void updateRNO(int ino,string iType,string fy)
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

    protected void preview_Click(object sender, EventArgs e)
    {
        String sno = (string)Session["INVOICECTR"];
        Session["BILLTYPE"] = "SB";
        Session["InvNoRep"] = sno;
        Session["InvNo"] = lblInvNo.Text;
        string strQuery = "SELECT   SUM(ServiceTaxAmount) AS ServiceTaxAmount  FROM   T_iec_invoiceNew_DTL WHERE  (invoice = '" + lblInvNo.Text + "')";
        //string strQuery = "select * from M_iec_invoiceNew where invoice='" + lblInvNo.Text + "' and contr_code is null and particular1 is not null";
         SqlConnection conn = new SqlConnection(strImpex);
         conn.Open();
        SqlDataAdapter da = new SqlDataAdapter(strQuery, conn);
        DataSet ds = new DataSet();
        da.Fill(ds, "table");
        DataRowView row = ds.Tables["table"].DefaultView[0];
        int StaxAmt = Convert.ToInt32(row["ServiceTaxAmount"]);
        conn.Close();
        if (StaxAmt > 0)
        {
           // Response.Redirect("../frmImpInvoiceReport.aspx");

            ScriptManager.RegisterStartupScript(this, this.GetType(), "popup", "window.open('../frmImpInvoiceReport.aspx','_blank','width=1120,height=720, menubar=no, scrollbars=yes, toolbar=no, location=no, resizable=no, left=10, top=120');", true);
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "popup", "window.open('../InvCumDebit.aspx','_blank','width=1120,height=720, menubar=no, scrollbars=yes, toolbar=no, location=no, resizable=no, left=10, top=120');", true);
            //Response.Redirect("../InvCumDebit.aspx");
        }
        preview.Visible = true;
    }
    protected void ServiceTax_TextChanged(object sender, EventArgs e)
    {
    }
   
   // protected void GrdPaddr_SelectedIndexChanged(object sender, EventArgs e)
    //{

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
        //        txtAdd1.Text =addr1;
        //        Session["Phone"] = row["tel_no"].ToString();
        //        Session["BranchID"] = row["ADDR_NUM"].ToString();

        //    }
        //}
        // GrdADDRSCROLL.Visible=false;
        // GrdPaddr.Visible=false;
        // TrAddr.Visible=false;
        // Button1.Visible= trMain.Visible=true;
        // TrAddr1.Visible=false;
        
   // }
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

    //protected void rbBill_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    string pcode = (string)Session["PCODE"];
    //    string BiilType = rbBill.SelectedValue;
    //    if (BiilType == "DP")
    //    {
    //        GrdADDRSCROLL.Visible = true;
    //        //GrdPaddr.DataSource = PartyAddr(pcode);
    //        //GrdPaddr.DataBind();
    //        //GrdPaddr.Visible = true;
    //        TrAddr.Visible = true;
    //        TrAddr1.Visible = true;
    //        Panel2.Visible = true;
    //        txtSubParty.Text = "";
    //    }
    //    else
    //    {
    //        GrdADDRSCROLL.Visible = false;
    //        GrdPaddr.Visible = false;
    //        TrAddr.Visible = false;
    //        TrAddr1.Visible = false;
    //        Panel2.Visible = true;
    //        trMain.Visible = true;
    //    }
    //}
    //protected void GridView1_RowDataBond(object sender, GridViewRowEventArgs e)
    //{
    //    if (e.Row.RowType == DataControlRowType.DataRow)
    //    {
    //        TextBox amt = (TextBox)e.Row.FindControl("amt1");
    //        if (amt.Text == "")
    //            amt.Text = "0.00";
    //        SubTotal.Text = Gross.ToString("#0.00#");
    //        DropDownList ddlStax = (DropDownList)e.Row.FindControl("ddlStax");
    //        string strQuery = "select * from M_ServiceMaster where fyear='" + fyear + "' order by fyear desc";
    //        ddlStax.DataSource = GetDataSQL(strQuery);
    //        ddlStax.DataTextField = "sTax";
    //        ddlStax.DataValueField = "serviceTax";
    //        ddlStax.DataBind();
    //    }

    ////     protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
    ////{
    ////    try
    ////    {
    ////        if (Session["PageLoad"] != null)
    ////        {
    ////            string SB = "";
    ////            string DB = "";
    ////            string Amount = "";
    ////            Double ass;
    ////            Double AMT;
    ////            string CUnits = "";
    ////            string ActualRate = "";
    ////            string FixedRate = "";
    ////            string MinRate = "";
    ////            string VarRate = "";
    ////            string MaxRate = "";
    ////            string varAmt = "";
    ////            string varUnit = "";
    ////            string CID = (string)Session["ContractID"];
    ////            string assValue = txtAssValue.Text;
    ////            string GWT = (string)Session["GRossWT"];

    ////            string shpType = Session["shpType"].ToString();
    ////            string cSize = Session["Contr_size"].ToString();
    ////            string cType = Session["Contr_Type"].ToString();
    ////            string status = "ACTIVE";
    ////            pName = Session["pName"].ToString();

    ////            //if (e.Row.RowType == DataControlRowType.DataRow)
    ////            //{
    ////            //    for (int i = 0; i < ds1.Tables[0].Rows.Count; i++)
    ////            //    {
    ////            //        TextBox Particulars = (TextBox)e.Row.FindControl("txtDetails");
    ////            //        Particulars.Text = ds1.Tables[0].Rows[i]["Description"].ToString();
    ////            //    }


    ////                if (shpType != "Air")
    ////                {
    ////                    if (cType == "LCL")
    ////                        shpType = "LCL";
    ////                    else
    ////                    {

    ////                    }
    ////                    {
    ////                        if (cSize == "20")
    ////                            shpType = "20Feet";
    ////                        else if (cSize == "40")
    ////                            shpType = "40Feet";
    ////                    }


    ////                }

    ////                string BILL = rbInvoice.SelectedValue;

    ////                if (e.Row.RowType == DataControlRowType.DataRow)
    ////                {
    ////                    TextBox amt = (TextBox)e.Row.FindControl("amt1");
    ////                    string Charge_desc = e.Row.Cells[3].Text;

    ////                    //CheckBox chk = (CheckBox)e.Row.FindControl("chkSTAX");
    ////                    //if (BILL != "SB")
    ////                    //    chk.Enabled = false;

    ////                    string sno = e.Row.Cells[0].Text;

    ////                    string Query = "select * from M_Quote where CustomerName='" + pName + "' and Description='" + Charge_desc + "' and Type='" + shpType + "' ";
    ////                    SqlConnection cnn = new SqlConnection(strImpex);
    ////                    SqlDataAdapter da = new SqlDataAdapter(Query, cnn);
    ////                    DataSet ds = new DataSet();
    ////                    da.Fill(ds, "Contract");

    ////                    if (ds.Tables["Contract"].Rows.Count != 0)
    ////                    {
    ////                        DataRowView row = ds.Tables["contract"].DefaultView[0];

    ////                        CUnits = row["unit"].ToString();
    ////                        ActualRate = row["ActualRate"].ToString();
    ////                        FixedRate = row["FixRate"].ToString();
    ////                        MinRate = row["MinRate"].ToString();
    ////                        VarRate = row["VarRate"].ToString();
    ////                        MaxRate = row["MaxRate"].ToString();
    ////                        varAmt = row["VarRate"].ToString();
    ////                        varUnit = row["VarType"].ToString();
    ////                        //if (ActualRate != "")
    ////                        //{
    ////                        //    cProduct = "At Actual";
    ////                        //}
    ////                        //if (FixedRate != "")
    ////                        //{
    ////                        //    cProduct = "Fixed";
    ////                        //}

    ////                        SB = "YES";
    ////                        if (Amount == "")
    ////                            Amount = "0";
    ////                    }

    ////                    if (SB == "YES" || DB == "YES")
    ////                    {
    ////                        //To check Contract values
    ////                        if (CUnits == "PER Kg")
    ////                        {
    ////                            string gWt = (string)Session["GRossWT"];
    ////                            Wt = Convert.ToDouble(gWt);
    ////                        }
    ////                        else if (CUnits == "PER Contr")
    ////                        {
    ////                            string qty = (string)Session["QTY"];
    ////                            Wt = Convert.ToDouble(qty);
    ////                        }
    ////                        else if (CUnits == "PER TON")
    ////                        {
    ////                            string gWt = (string)Session["GRossWT"];
    ////                            Wt = Convert.ToDouble(gWt) / 1000;
    ////                        }
    ////                        else
    ////                            Wt = 1;

    ////                        // Charge Description values
    ////                        if (Charge_desc != "Agency charges")
    ////                        {

    ////                            if (ActualRate != "")
    ////                            {
    ////                                AMT = Convert.ToDouble(ActualRate) * Wt;
    ////                                amt.Text = AMT.ToString("#0.00#");
    ////                            }
    ////                            else if (FixedRate != "")
    ////                            {
    ////                                AMT = Convert.ToDouble(FixedRate) * Wt;
    ////                                amt.Text = AMT.ToString("#0.00#");
    ////                            }
    ////                            else
    ////                            {
    ////                                if (varAmt == "" || varAmt == "0")
    ////                                    varAmt = "1";

    ////                                if (varUnit == "PER Kg")
    ////                                {
    ////                                    string gWt = (string)Session["GRossWT"];
    ////                                    totalAmt = Convert.ToDouble(gWt) * Convert.ToDouble(varAmt);
    ////                                }

    ////                                //string Pro = "Variable";
    ////                                //string varAmt = "";
    ////                                //string varUnit = "";

    ////                                //string QueryPro = "select * from M_Quote where QuoteNo='" + CID + "' and Description='" + Charge_desc + "' ";
    ////                                //SqlConnection cnnPro = new SqlConnection(strImpex);
    ////                                //SqlDataAdapter daPro = new SqlDataAdapter(QueryPro, cnnPro);
    ////                                //DataSet dsPro = new DataSet();
    ////                                //daPro.Fill(dsPro, "ContractPRO");
    ////                                //if (dsPro.Tables["ContractPRO"].Rows.Count != 0)
    ////                                //{
    ////                                //    DataRowView RowPro = dsPro.Tables["ContractPRO"].DefaultView[0];

    ////                                //}

    ////                                string QueryPro1 = "select * from M_Quote where QuoteNo='" + CID + "' and Description='" + Charge_desc + "' and Type='" + shpType + "'  ";
    ////                                SqlConnection cnnPro1 = new SqlConnection(strImpex);
    ////                                SqlDataAdapter daPro1 = new SqlDataAdapter(QueryPro1, cnnPro1);
    ////                                DataSet dsPro1 = new DataSet();
    ////                                daPro1.Fill(dsPro1, "ContractPRO1");
    ////                                DataTable dtPro1 = dsPro1.Tables[0];
    ////                                foreach (DataRow rowPro in dtPro1.Rows)
    ////                                {
    ////                                    CUnits = rowPro["unit"].ToString();
    ////                                    ActualRate = rowPro["ActualRate"].ToString();
    ////                                    FixedRate = rowPro["FixRate"].ToString();
    ////                                    MinRate = rowPro["MinRate"].ToString();
    ////                                    VarRate = rowPro["VarRate"].ToString();
    ////                                    MaxRate = rowPro["MaxRate"].ToString();
    ////                                    varAmt = rowPro["VarRate"].ToString();
    ////                                    varUnit = rowPro["VarType"].ToString();

    ////                                    // Amount = rowPro["MinRate"].ToString();
    ////                                    string product = "Minimum";

    ////                                    if (Amount == "")
    ////                                        Amount = "1";
    ////                                    AMT = Convert.ToDouble(Amount);

    ////                                    if (MinRate != "")
    ////                                    {
    ////                                        AMT = Convert.ToDouble(MinRate);
    ////                                        if (totalAmt < AMT)
    ////                                        {
    ////                                            amt.Text = AMT.ToString("#0.00#");
    ////                                            Session["Omin"] = amt.Text;
    ////                                        }
    ////                                        minVal = AMT;
    ////                                    }
    ////                                    else if (MaxRate != "")
    ////                                    {
    ////                                        AMT = Convert.ToDouble(MaxRate);
    ////                                        if (totalAmt > AMT)
    ////                                        {
    ////                                            amt.Text = AMT.ToString("#0.00#");
    ////                                            Session["Omax"] = amt.Text;
    ////                                        }
    ////                                        maxVal = AMT;
    ////                                        Session["OmaxVAL"] = "1";
    ////                                    }
    ////                                    else if (VarRate != "" && VarRate != "0")
    ////                                    {
    ////                                        AMT = Convert.ToDouble(VarRate);
    ////                                        if ((string)Session["OmaxVAL"] == "0")
    ////                                        {
    ////                                            if (totalAmt < minVal)
    ////                                                amt.Text = (string)Session["Omin"];
    ////                                            else
    ////                                                amt.Text = totalAmt.ToString("#0.00#");
    ////                                        }
    ////                                        else
    ////                                        {
    ////                                            if (totalAmt < minVal)
    ////                                                amt.Text = (string)Session["Omin"];
    ////                                            else if (totalAmt > maxVal)
    ////                                                amt.Text = (string)Session["Omax"];
    ////                                            else
    ////                                                amt.Text = totalAmt.ToString("#0.00#");
    ////                                            Session["OmaxVAL"] = "0";
    ////                                        }
    ////                                    }

    ////                                }

    ////                            }


    ////                        }
    ////                        else
    ////                        {
    ////                            if (ActualRate != "")
    ////                            {

    ////                                AMT = Convert.ToDouble(Amount) * Wt;
    ////                                amt.Text = AMT.ToString("#0.00#");
    ////                            }
    ////                            else if (FixedRate != "")
    ////                            {
    ////                                AMT = Convert.ToDouble(Amount) * Wt;
    ////                                amt.Text = AMT.ToString("#0.00#");
    ////                            }
    ////                            else
    ////                            {
    ////                                //start variable condition
    ////                                GetVariable(pName, status, Charge_desc, CID, shpType);

    ////                                ass = Convert.ToDouble(assValue);
    ////                                string vers = Session["Variable"].ToString();
    ////                                if (vers == "")
    ////                                {
    ////                                    Response.Write("<script>alert('variables are not assigned please check contract details  ')</script>");

    ////                                }
    ////                                else
    ////                                {
    ////                                    Double veriablePercentage = Convert.ToDouble(vers);
    ////                                    Double gVAL = ass / 100 * veriablePercentage;
    ////                                    string Query1 = "select * from M_Quote where QuoteNo='" + CID + "' and Description='" + Charge_desc + "' and Type='" + shpType + "'  ";
    ////                                    //string Query1 = "select * from contract_mst m,contract_dtl s " +
    ////                                    //       "where m.contr_code=s.contr_code and m.customer_name='" + pName + "' and  " +
    ////                                    //       "m.contr_status='" + status + "' and s.charge_desc='" + Charge_desc + "' and " +
    ////                                    //       "m.contr_code='" + CID + "' order by s.product";
    ////                                    SqlConnection cnn1 = new SqlConnection(strImpex);
    ////                                    SqlDataAdapter da1 = new SqlDataAdapter(Query1, cnn1);
    ////                                    DataSet ds1 = new DataSet();
    ////                                    da1.Fill(ds1, "agch");
    ////                                    DataTable dt = ds1.Tables[0];
    ////                                    foreach (DataRow Row in dt.Rows)
    ////                                    {
    ////                                        CUnits = Row["unit"].ToString();
    ////                                        ActualRate = Row["ActualRate"].ToString();
    ////                                        FixedRate = Row["FixRate"].ToString();
    ////                                        MinRate = Row["MinRate"].ToString();
    ////                                        VarRate = Row["VarRate"].ToString();
    ////                                        MaxRate = Row["MaxRate"].ToString();
    ////                                        varAmt = Row["VarRate"].ToString();
    ////                                        varUnit = Row["VarType"].ToString();

    ////                                        //Amount = Row["" + shpType + ""].ToString();
    ////                                        //string product = Row["product"].ToString();

    ////                                        //if (Amount == "")
    ////                                        //    Amount = "0";
    ////                                        //AMT = Convert.ToDouble(Amount) * Wt;

    ////                                        if (MinRate != "")
    ////                                        {
    ////                                            AMT = Convert.ToDouble(MinRate) * Wt;
    ////                                            if (gVAL < AMT)
    ////                                            {
    ////                                                amt.Text = AMT.ToString("#0.00#");
    ////                                                Session["min"] = amt.Text;
    ////                                            }
    ////                                            minVal = AMT;
    ////                                        }
    ////                                        else if (MaxRate != "")
    ////                                        {
    ////                                            AMT = Convert.ToDouble(MaxRate) * Wt;
    ////                                            if (gVAL > AMT)
    ////                                            {
    ////                                                amt.Text = AMT.ToString("#0.00#");
    ////                                                Session["max"] = amt.Text;
    ////                                            }
    ////                                            maxVal = AMT;
    ////                                            Session["MAXVAL"] = "1";
    ////                                        }
    ////                                        else if (VarRate != "")
    ////                                        {
    ////                                            AMT = Convert.ToDouble(VarRate) * Wt;
    ////                                            if ((string)Session["MAXVAL"] == "0")
    ////                                            {
    ////                                                if (gVAL < minVal)
    ////                                                    amt.Text = (string)Session["min"];
    ////                                                else
    ////                                                    amt.Text = gVAL.ToString("#0.00#");
    ////                                            }
    ////                                            else
    ////                                            {
    ////                                                if (gVAL < minVal)
    ////                                                    amt.Text = (string)Session["min"];
    ////                                                else if (gVAL > maxVal)
    ////                                                    amt.Text = (string)Session["max"];
    ////                                                else
    ////                                                    amt.Text = gVAL.ToString("#0.00#");
    ////                                                Session["MAXVAL"] = "0";
    ////                                            }
    ////                                        }
    ////                                    }
    ////                                    //end charge details 
    ////                                }
    ////                            }
    ////                        }
    ////                    }
    ////                    //end Contract value
    ////                    if (amt.Text == "")
    ////                        amt.Text = "0";
    ////                    Gross = Gross + Convert.ToDouble(amt.Text);
    ////                    e.Row.Cells[0].ForeColor = Color.White;
    ////                }

    ////                SubTotal.Text = Gross.ToString("#0.00#");
    ////                string cmp = (string)Session["CMP"];
    ////                if (BILL == "SB")
    ////                {
    ////                    Double RserTax = 0;
    ////                    Double REcess = 0;
    ////                    Double RScess = 0;

    ////                    sTax.Text = RserTax.ToString("#0.00#");
    ////                    EdCess.Text = REcess.ToString("#0.00#");
    ////                    SHCess.Text = RScess.ToString("#0.00#");

    ////                    Double NetAmt = Gross;
    ////                    Double ld = Convert.ToDouble(LessAd.Text);
    ////                    Totals.Text = NetAmt.ToString("#0.00#");
    ////                    bal = NetAmt - ld;
    ////                    LessAd.Text = ld.ToString("#0.00#");

    ////                }
    ////                else
    ////                {
    ////                    Double NetAmt = Gross;
    ////                    Totals.Text = NetAmt.ToString("#0.00#");
    ////                    bal = Convert.ToDouble(SubTotal.Text) - Convert.ToDouble(LessAd.Text);

    ////                }
    ////                Double balanceAmount = Math.Round(bal);

    ////                balance1.Text = balanceAmount.ToString();
    ////                BalanceDue.Text = balanceAmount.ToString("#0.00#");

    ////                txtRupees.Text = RsConvert.rupees(Convert.ToInt64(balance1.Text));
    ////                Submit.Focus();
    ////            }
    ////        }
        
    ////    catch (Exception ex)
    ////    {
    ////        Response.Redirect(ex.Message);
    ////    }

    //}



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
                 string amt1 = "";
                 if (ds2.Tables["DescVal"].Rows.Count != 0)
                 {
                     DataRowView Row = ds2.Tables["DescVal"].DefaultView[0];
                     string ActualRate = Row["ActualRate"].ToString();
                     string FixedRate = Row["FixRate"].ToString();
                     string MinRate = Row["MinRate"].ToString();
                     string VarRate = Row["VarRate"].ToString();
                     string MaxRate = Row["MaxRate"].ToString();

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
    protected void  GetServiceTax(string cmp)
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
        vSHECess=SHEcess;
    }

    protected void txtJNO_TextChanged(object sender, EventArgs e)
    {
        GetFundRequest();
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
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Job number not exist. Create job in Job creation page');", true);
                }

            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('" + ex.Message + "');", true);
            }
       
            
        
    }

    public void GetFundRequest()
    {
        string query = "select jobno,RequestAmt,ApprovedAmt,PaymentAmt,PaymentStatus from T_FundRequest where jobno='" + txtJNO.Text + "'";
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

    protected void GetSQLJOBS(string jobNo)
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
            ScriptManager.RegisterStartupScript(this, this.GetType(), "confirm", "confirm('Given job has already billing. The Bill No. " + eXISTbILL + " . You are generating invoice for the same job again');", true);
            //Submit.Enabled = false;            
        }
        else
        {
            Submit.Enabled = true;
        }
    }
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
    protected void Menu2_MenuItemClick(object sender, MenuEventArgs e)
    {
    }

    protected void ExportTally_Click(object sender, EventArgs e)
    {
      //  GetFSIO();
    }
    //protected void GetFSIO()
    //{
    //    string jno = txtJNO.Text;
    //    string sqlQuerySTR = "";
    //    string sqlQuery = "";
    //    string strMST = "";
    //    string strDTL = "";
    //    string invNO = "";
    //    string Party = "";
    //    string dates = "";
    //    string InvoiceType = "";
    //    string nTotal = "";
    //    string refer = "";
    //    string Naration = "";
    //    string jNo = "";
    //    string file = string.Empty;
    //    string billtype = "";
    //    string datetime = "";
    //    string serverPath = "";
    //    string genFile = "";
    //    string dATE = "";
    //    sqlQuery = "select * from iec_invoicenew i where i.jobno='" + jno + "' ";
    //    if (sqlQuery != "")
    //    {
    //        datetime = DateTime.Now.Day.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Year.ToString() + "-" + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString();
    //        serverPath = Server.MapPath("~") + "\\" + "CSV";
    //        genFile = billtype + datetime;
    //        dATE = DateTime.Now.Day.ToString() + "." + DateTime.Now.Month.ToString() + "." + DateTime.Now.Year.ToString();
    //        if (Directory.Exists(serverPath))
    //        {
    //            string PartyNameDirectory = serverPath + "\\" + "Tally" + "\\" + dATE;
    //            if (Directory.Exists(PartyNameDirectory))
    //            {
    //                file = PartyNameDirectory + "\\" + billtype + datetime + ".csv";
    //            }
    //            else
    //            {
    //                Directory.CreateDirectory(PartyNameDirectory);
    //                file = PartyNameDirectory + "\\" + billtype + datetime + ".csv";
    //            }
    //        }
    //        else
    //        {
    //            Directory.CreateDirectory(serverPath);
    //            string PartyNameDirectory = serverPath + "\\" + "Tally" + "\\" + dATE;
    //            if (Directory.Exists(PartyNameDirectory))
    //            {
    //                file = PartyNameDirectory + "\\" + billtype + datetime + ".csv"; ;
    //            }
    //            else
    //            {
    //                Directory.CreateDirectory(PartyNameDirectory);
    //                file = PartyNameDirectory + "\\" + billtype + datetime + ".csv"; ;
    //            }
    //        }
    //        Session["FILEPATH"] = file;
    //        FileStream fs = new FileStream(file, FileMode.Create, FileAccess.Write, FileShare.Read);
    //        StreamWriter sw = new StreamWriter(fs);
    //        TextWriter tw = sw;
    //        string pName = "";
    //        Double amt = 0;
    //        string jnos = "";
    //        Double STAX = 0;
    //        tw.Write("Voucher Type" + ","); tw.Write("Invoice No" + ","); tw.Write("Date" + ","); tw.Write("Ref" + ","); tw.Write("Dr Account" + ","); tw.Write("Cr.Account" + ",");
    //        tw.Write("Cost Center" + ","); tw.Write("Amount" + ","); tw.Write("Narration\n");
    //        jNo = (string)Session["IINVNO"];
    //        string iTYPE = "SB";
    //        string sqlQueryM = "";
    //            //Master Records
    //            SqlConnection connM = new SqlConnection(strImpex);
    //                strMST = "M_iec_invoicenew";
    //                strDTL = "T_iec_invoicenew_dtl";
    //            sqlQueryM = "select INVOICE,invoiceDate,jobNo,invoiceType,compName,invoiceNo,Grand_Total,service_tax + edu_cess + sec_chess as STAX," +
    //                          "blno , beNoDate , importItem , Quantity , partyReff  " +
    //                          "from " + strMST + "  where invoice='" + jNo + "'";
    //            SqlDataAdapter daM = new SqlDataAdapter(sqlQueryM, connM);
    //            DataSet dsM = new DataSet();
    //            daM.Fill(dsM, "INVOICEMST");
    //            if (dsM.Tables["INVOICEMST"].Rows.Count != 0)
    //            {
    //                DataRowView rowM = dsM.Tables["INVOICEMST"].DefaultView[0];
    //                string BillDate = rowM["invoiceDate"].ToString();
    //                invNO = rowM["INVOICEno"].ToString();
    //                jnos = rowM["jobNo"].ToString();
    //                Party = rowM["compName"].ToString();
    //                string NRef = rowM["invoiceNo"].ToString();
    //                InvoiceType = rowM["invoiceType"].ToString();
    //                nTotal = rowM["Grand_Total"].ToString();
    //                pName = Party.Replace(",", " ");
    //                //Naration fields
    //                string blno = " AWB/BL.NO.:" + rowM["blno"].ToString();
    //                string beNo = " BE.NO.:" + rowM["beNoDate"].ToString();
    //                string impItem = rowM["importItem"].ToString();
    //                impItem = impItem.Replace(",", " ");
    //                string qty = rowM["Quantity"].ToString();
    //                string pRef = rowM["partyReff"].ToString();
    //                string serTAX = rowM["STAX"].ToString();
    //                if (serTAX == "")
    //                    serTAX = "0";
    //                STAX = Convert.ToDouble(serTAX);
    //                string[] iDate = BillDate.Split('-');
    //                dates = iDate[2] + "-" + iDate[1] + "-" + iDate[0];
    //                if (jnos.StartsWith("IMP") == true)
    //                    jnos = jnos.Substring(4, 5);
    //                // Start CSV Header Text
    //                //Party = Party.Substring(0, 4);
    //                string[] pn = Party.Split(' ');
    //                Party = pn[0];
    //                if (pn[0].Length < 3)
    //                    Party = pn[0] + pn[1];

    //                refer = jnos + " / " + Party;
    //                Naration = "JOBNO:" + jnos + "/" + blno + "/" + beNo + "/ " + impItem + "/ " + qty + "/ " + pRef;
    //                BHamt = BHamt + amt;
    //                // END CSV Header Text
    //                if (iTYPE == "SB" || iTYPE == "ATLSB" || iTYPE == "EXPSB")
    //                {
    //                    InvoiceType = "sales";
    //                    tw.Write(InvoiceType + ","); tw.Write(invNO + ","); tw.Write(dates + ","); tw.Write(refer + ",");
    //                    tw.Write(pName + ","); tw.Write(","); tw.Write(refer + ","); tw.Write(nTotal + ","); tw.Write(Naration + ","); tw.Write("\n");
    //                }
    //                else
    //                {
    //                    InvoiceType = "Debit Note";
    //                    tw.Write(InvoiceType + ","); tw.Write(invNO + ","); tw.Write(dates + ","); tw.Write(refer + ",");
    //                    tw.Write(","); tw.Write(pName + ","); tw.Write(refer + ","); tw.Write(nTotal + ","); tw.Write(Naration + ","); tw.Write("\n");
    //                }
    //            }
    //            //Transaction Records 
    //            SqlConnection connSTR = new SqlConnection(strImpex);
    //            sqlQuerySTR = "select sno,invoice,charge_desc,amount from " + strDTL + "  " +
    //                          "where invoice='" + jNo + "' order by sno";
    //            SqlDataAdapter daSTR = new SqlDataAdapter(sqlQuerySTR, connSTR);
    //            DataSet dsSTR = new DataSet();
    //            daSTR.Fill(dsSTR, "INVOICEDTL");
    //            DataTable dtSTR = dsSTR.Tables[0];
    //            if (dsSTR.Tables["INVOICEDTL"].Rows.Count != 0)
    //            {
    //                int i = 1;
    //                int length = dtSTR.Rows.Count;
    //                BHamt = 0;
    //                foreach (DataRow rowSTR in dtSTR.Rows)
    //                {
    //                    string sno = rowSTR["sno"].ToString();
    //                    amt = Convert.ToDouble(rowSTR["amount"].ToString());
    //                    fsTotal = fsTotal + amt;
    //                    string ino = rowSTR["invoice"].ToString();
    //                    string desc = rowSTR["charge_desc"].ToString();
    //                    desc = desc.Replace(",", " ");
    //                    if (pName == "BHARAT HEAVY ELECTRICALS LIMITED ")
    //                    {
    //                        BHamt = BHamt + amt;
    //                        if (iTYPE == "SB" || iTYPE == "ATLSB" || iTYPE == "EXPSB")
    //                        {
    //                            if (i == length)
    //                            {
    //                                tw.Write(InvoiceType + ","); tw.Write(invNO + ","); tw.Write(dates + ","); tw.Write(refer + ",");
    //                                tw.Write(",");
    //                                tw.Write("Agency charges" + ","); tw.Write(refer + ","); tw.Write(BHamt + ","); tw.Write(Naration + ","); tw.Write("\n");
    //                            }
    //                        }
    //                        else
    //                        {
    //                            if (i == length)
    //                            {
    //                                tw.Write(InvoiceType + ","); tw.Write(invNO + ","); tw.Write(dates + ","); tw.Write(refer + ",");
    //                                tw.Write("Agency charges" + ","); tw.Write(pName + ","); tw.Write(refer + ","); tw.Write(BHamt + ","); tw.Write(Naration + ","); tw.Write("\n");
    //                            }
    //                        }

    //                    }

    //                    else if (pName == "BHARAT HEAVY PLATE AND VESSELS LTD")
    //                    {
    //                        BHamt = BHamt + amt;
    //                        if (iTYPE == "SB" || iTYPE == "ATLSB" || iTYPE == "EXPSB")
    //                        {
    //                            if (i == length)
    //                            {
    //                                tw.Write(InvoiceType + ","); tw.Write(invNO + ","); tw.Write(dates + ","); tw.Write(refer + ",");
    //                                tw.Write(",");
    //                                tw.Write("Agency charges" + ","); tw.Write(refer + ","); tw.Write(BHamt + ","); tw.Write(Naration + ","); tw.Write("\n");
    //                            }
    //                        }
    //                        else
    //                        {
    //                            if (i == length)
    //                            {
    //                                tw.Write(InvoiceType + ","); tw.Write(invNO + ","); tw.Write(dates + ","); tw.Write(refer + ",");

    //                                tw.Write("Agency charges" + ","); tw.Write(pName + ","); tw.Write(refer + ","); tw.Write(BHamt + ","); tw.Write(Naration + ","); tw.Write("\n");
    //                            }
    //                        }

    //                    }
    //                    else
    //                    {

    //                        if (iTYPE == "SB" || iTYPE == "ATLSB" || iTYPE == "EXPSB")
    //                        {
    //                            if (ino != fJOBNO)
    //                            {
    //                                tw.Write(InvoiceType + ","); tw.Write(invNO + ","); tw.Write(dates + ","); tw.Write(refer + ",");
    //                                tw.Write(",");
    //                                tw.Write(desc + ","); tw.Write(refer + ","); tw.Write(amt + ","); tw.Write(Naration + ","); tw.Write("\n");
    //                                fJOBNO = ino;
    //                            }
    //                            else
    //                            {

    //                                tw.Write(InvoiceType + ","); tw.Write(invNO + ","); tw.Write(dates + ","); tw.Write(refer + ",");
    //                                tw.Write(",");
    //                                tw.Write(desc + ","); tw.Write(refer + ","); tw.Write(amt + ","); tw.Write(Naration + ","); tw.Write("\n");
    //                            }
    //                        }
    //                        else
    //                        {
    //                            if (ino != fJOBNO)
    //                            {
    //                                tw.Write(InvoiceType + ","); tw.Write(invNO + ","); tw.Write(dates + ","); tw.Write(refer + ",");

    //                                tw.Write(desc + ","); tw.Write(","); tw.Write(refer + ","); tw.Write(amt + ","); tw.Write(Naration + ","); tw.Write("\n");
    //                                fJOBNO = ino;
    //                            }
    //                            else
    //                            {

    //                                tw.Write(InvoiceType + ","); tw.Write(invNO + ","); tw.Write(dates + ","); tw.Write(refer + ",");

    //                                tw.Write(desc + ","); tw.Write(","); tw.Write(refer + ","); tw.Write(amt + ","); tw.Write(Naration + ","); tw.Write("\n");
    //                            }
    //                        }
    //                    }

    //                    if (i == length)
    //                    {
    //                        if (iTYPE == "SB" || iTYPE == "ATLSB" || iTYPE == "EXPSB")
    //                        {
    //                            tw.Write(InvoiceType + ","); tw.Write(invNO + ","); tw.Write(dates + ","); tw.Write(refer + ",");
    //                            tw.Write(",");
    //                            tw.Write("Service Tax" + ","); tw.Write(refer + ","); tw.Write(STAX + ","); tw.Write(Naration + ","); tw.Write("\n");
    //                            fsTotal = fsTotal + STAX;
    //                        }
    //                        fsTotal = 0;
    //                    }
    //                    i = i + 1;
    //                }
    //            }
    //        tw.Flush();
    //        tw.Close();
    //        string sGenName = file;
    //        string sFileName = file;
    //        string fdATE = DateTime.Now.Day.ToString() + "." + DateTime.Now.Month.ToString() + "." + DateTime.Now.Year.ToString();
    //        System.IO.FileStream fss = null;
    //        fss = System.IO.File.Open(Server.MapPath("CSV/Tally/" + fdATE + "/" +
    //                genFile + ".csv"), System.IO.FileMode.Open);
    //        byte[] btFile = new byte[fss.Length];
    //        fss.Read(btFile, 0, Convert.ToInt32(fss.Length));
    //        fss.Close();
    //        FileInfo filep = new FileInfo(sGenName);
    //        string newPath = Path.GetFileName(sGenName);
    //        Response.AddHeader("Content-disposition", "attachment; filename=" + newPath);
    //        Response.ContentType = "application/ms-excel";
    //        Response.BinaryWrite(btFile);
    //        Response.End();
    //        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Billing Details has been Generated....');", true);
    //    }
    //    else
    //        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Please select Bill type....');", true);
    //}
    protected void BtnExit_Click(object sender, EventArgs e)
    {
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
            txtimpRemark.Text = "Supplier Inv No :" + supplier.TrimStart(',');
        }
        else
        {
            txtimpRemark.Text = "";
        }
    }

    protected void txtSubParty_TextChanged1(object sender, EventArgs e)
    {
        string SubPartyQuery = "select AccountName,Address1,City,State  from M_AccountMaster where AccountName like '" + txtSubParty.Text.Replace("'", "") +  "%'  and Acc_Group like '" + txtCompName.Text.Replace("'", "") + "%'" ;
        SqlConnection con = new SqlConnection(strImpex);
        con.Open();
        SqlDataAdapter da5 = new SqlDataAdapter(SubPartyQuery, con);   
        DataSet ds5 = new DataSet();
        da5.Fill(ds5, "SubParty");
        if (ds5.Tables["SubParty"].Rows.Count != 0)
        {
            DataRowView row = ds5.Tables["SubParty"].DefaultView[0];
            txtSubParty.Text = row["AccountName"].ToString().Trim().Replace("'", "");
            txtSubPaartAddr.Text = row["Address1"].ToString().Trim().Replace("'", "") + "," + row["City"].ToString().Trim() + "," + row["State"].ToString().Trim();
        }
        else
        {
            Response.Write("<script>alert('Sub Party Name is not available. Please create in Master page')</script>");
        }
    }

        protected void GridView2_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridView2.Visible = false;
            GrdADDRSCROLL.Visible = false;
            GrdPaddr.Visible = false;
            
           // GridScroll.Visible = true;
            string CID = Convert.ToString(GridView2.SelectedDataKey.Value);
            string Bill = "YES";

            //string BType = rbInvoice.SelectedValue;
            //if (BType == "")
            //    BType = "SB";
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


            //if((string)Session["shpType"]=="Air" )
            //{
            //    Query = "select * from M_Quote where QuoteNo='" + CID + "' and type='" + (string)Session["shpType"] + "'  order by ID";
            //}
            //else
            //{
            //    Query = "select * from M_Quote where QuoteNo='" + CID + "' and type<>'Air' order by ID";
            //}
            GridView3.DataSource = GetDataSQL(Query);
            GridView3.DataBind();
            GridScroll.Visible = true;
            GridView3.Visible = true;
            Session["ContractID"] = CID;
            BtnContract_Submit.Visible = true;
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

        protected void GridView3_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            string shpType = Session["shpType"].ToString();
            string cSize = Session["Contr_size"].ToString();
            string cType = Session["Contr_Type"].ToString();
            //string Bill = rbInvoice.SelectedValue;

            if (shpType != "Air")
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
                //if (Bill == "SB")
                //{
                //    e.Row.Cells[10].Visible = true;
                //    e.Row.Cells[11].Visible = false;
                //}
                //else
                //{
                //    e.Row.Cells[10].Visible = false;
                //    e.Row.Cells[11].Visible = true;
                //}
                //switch (shpType)
                //{
                //    case "Air":
                //        {
                //            e.Row.Cells[5].Visible = true;
                //            e.Row.Cells[6].Visible = false;
                //            e.Row.Cells[7].Visible = false;
                //            e.Row.Cells[8].Visible = false;
                //            e.Row.Cells[9].Visible = false;
                //            break;
                //        }
                //    case "break_bulk":
                //        {
                //            e.Row.Cells[5].Visible = false;
                //            e.Row.Cells[6].Visible = true;
                //            e.Row.Cells[7].Visible = false;
                //            e.Row.Cells[8].Visible = false;
                //            e.Row.Cells[9].Visible = false;
                //            break;
                //        }
                //    case "LCL":
                //        {
                //            e.Row.Cells[5].Visible = false;
                //            e.Row.Cells[6].Visible = false;
                //            e.Row.Cells[7].Visible = true;
                //            e.Row.Cells[8].Visible = false;
                //            e.Row.Cells[9].Visible = false;
                //            break;
                //        }

                //    case "ft20":
                //        {
                //            e.Row.Cells[5].Visible = false;
                //            e.Row.Cells[6].Visible = false;
                //            e.Row.Cells[7].Visible = false;
                //            e.Row.Cells[8].Visible = true;
                //            e.Row.Cells[9].Visible = false;
                //            break;
                //        }
                //    case "ft40":
                //        {
                //            e.Row.Cells[5].Visible = false;
                //            e.Row.Cells[6].Visible = false;
                //            e.Row.Cells[7].Visible = false;
                //            e.Row.Cells[8].Visible = false;
                //            e.Row.Cells[9].Visible = true;
                //            break;
                //        }

                //}
            }
            //if(e.Row.RowType==DataControlRowType.DataRow)
            //{
            //    CheckBox chklist = (CheckBox)e.Row.FindControl("chk");
            //    string charge = e.Row.Cells[2].Text;
            //    string product=e.Row.Cells[3].Text;
            //    e.Row.Cells[1].ForeColor = Color.White;
            //    if (charge == "Agency charges" && (product=="Variable" || product=="Maximum"))
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
        protected void GridView3_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void GetContractInfo(string sel)
        {
            string CID = (string)Session["ContractID"];
            string AssValue = txtAssValue.Text;
            pName = Session["pName"].ToString();
            string status = "ACTIVE";
            string strchargeVal = sel.TrimEnd(',');
            Session["strChargeVal"] = strchargeVal;
            string strQuery = "select Description as charge_desc from M_Quote where QuoteNo='" + CID + "' and CustomerName='" + pName + "'  and ID in(" + strchargeVal + ") ";
            SqlConnection cnn = new SqlConnection(strImpex);
            SqlDataAdapter daS = new SqlDataAdapter(strQuery, cnn);
            DataSet dsS = new DataSet();
            daS.Fill(dsS, "Contract");
            if (dsS.Tables["Contract"].Rows.Count != 0)
            {
                DataRowView rw=dsS.Tables["Contract"].DefaultView[0];
                string test = rw["charge_desc"].ToString();
         
                for (int i = 0; i < dsS.Tables[0].Rows.Count; i++)
                {
                    TextBox Particulars = (TextBox)GridView1.Rows[i].FindControl("txtDetails");
                    Particulars.Text = dsS.Tables[0].Rows[i]["charge_desc"].ToString();

                }

                MainForm.Visible = true;
                tblInv.Visible = true;
                trMain.Visible = true;
                Panel2.Visible = true;
                SubForm.Visible = false;
           
             
            }
        }

        protected void BtnClose_Click(object sender, EventArgs e)
        {
            //tblINV.Visible = true;
            //tblContr.Visible = false;
        }

        protected void BtnContract_Submit_Click(object sender, EventArgs e)
        {
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
                //tblContr.Visible = false;
                //tblINV.Visible = true;
                GetContractInfo(strCharge);
                //GetSQLJOBS(txtJNO.Text);
            }
            //string BiilType = rbBill.SelectedValue;
            // if (BiilType == "DP")
            //{

            //                txtSubParty.Text = "";
            // }

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
                    conn.Open();
                    string sqlQuery = "select *  from M_AccountDetails where Accountcode='" + pcode + "' and BranchId=" + NO + "";
                    SqlDataAdapter da = new SqlDataAdapter(sqlQuery, conn);
                    DataSet ds = new DataSet();
                    da.Fill(ds, "addr");
                    conn.Close();

                    DataRowView row = ds.Tables["addr"].DefaultView[0];
                    string pname = row["AccountName"].ToString();
                    string addr1 = row["Address1"].ToString();
                    string city = row["City"].ToString();
                    string state = row["State"].ToString();
                    string pin = row["Pincode"].ToString();
                    Session["addr"] = addr1;
                    Session["city"] = city;
                    Session["state"] = state;
                    Session["Pin"] = pin;
                    Session["BCODE"] = NO;
                    //txtCity.Text = city;
                    Session["BranchID"] = row["BranchId"].ToString();
                    //txtAdd1.Text = addr1;
                    Session["Phone"] = row["PhoneNo"].ToString();


                    FillQuote(pname, NO);

                }
            }
            //GrdADDRSCROLL.Visible = false;
            GrdPaddr.Visible = false;
            GridView2.Visible = true;
            BtnContract_Submit.Visible = false;
            
            //TrAddr.Visible = false;
            //TrAddr1.Visible = false;bt
             
        }

        public void FillQuote(string pname, string NO)
        {

            SqlConnection conn = new SqlConnection(strImpex);
            conn.Open();
            string sqlQuery = "select distinct QuoteNo,CustomerName from M_Quote where customername= '" + pname + "' ";
            SqlDataAdapter da = new SqlDataAdapter(sqlQuery, conn);
            DataSet ds = new DataSet();
            da.Fill(ds, "addr");
            conn.Close();

            GridView2.DataSource = ds;
            GridView2.DataBind();
            GrdADDRSCROLL.Visible = false;
            Tr1.Visible = false;
                  
            GrdPaddr.Visible = false;
            GridView2.Visible = true;

        }
        protected void rbInvoice_SelectedIndexChanged(object sender, EventArgs e)
        {
          //  GenerateBillNo();

        }
        //protected void GenerateBillNo()
        //{
        //    string fy = (string)Session["FinancialYear"];

        //    string jobNo = txtJobNo.Text;
        //    string BILL = rbInvoice.SelectedValue;
        //    string strQuery = "";
        //    string sbNo = "";
        //    if (BILL == "SB")
        //    {
        //        sbNo = (string)Session["BranchShortName"] + "/SB/";
        //        strQuery = "select * from M_RunningNo where iectype='" + BILL + "' and Fyear='" + fy + "'";
        //        lblIName.Text = "CONTRACT INVOICE - IMPORTS";
        //    }
        //    else
        //    {
        //        sbNo = (string)Session["BranchShortName"] + "/DB/";
        //        strQuery = "select * from M_RunningNo where iectype='" + BILL + "' and Fyear='" + fy + "'";
        //        lblIName.Text = "CONTRACT DEBIT NOTE - IMPORTS";
        //    }
        //    SqlConnection cnn = new SqlConnection(strImpex);
        //    SqlDataAdapter da = new SqlDataAdapter(strQuery, cnn);
        //    DataSet ds = new DataSet();
        //    da.Fill(ds, "Contract");
        //    if (ds.Tables["Contract"].Rows.Count != 0)
        //    {
        //        DataRowView row = ds.Tables["Contract"].DefaultView[0];
        //        Int32 INO = Convert.ToInt32(row["rno"].ToString());
        //        Session["InvNo"] = INO + 1;
        //        INO = INO + 1;
        //        lblInvNo.Text = sbNo + Convert.ToString(INO);

        //    }
        //}

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
                //TrAddr1.Visible = true;
            }
            else
            {
                GrdADDRSCROLL.Visible = false;

                GrdPaddr.Visible = false;
                TrAddr.Visible = false;
                //TrAddr1.Visible = false;
            }
        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                TextBox amt = (TextBox)e.Row.FindControl("amt1");
                if (amt.Text == "")
                    amt.Text = "0.00";
                SubTotal.Text = Gross.ToString("#0.00#");
                DropDownList ddlStax = (DropDownList)e.Row.FindControl("ddlStax");
                string strQuery = "select * from M_ServiceMaster where fyear='" + fyear + "' order by fyear desc";
                ddlStax.DataSource = GetDataSQL(strQuery);
                ddlStax.DataTextField = "sTax";
                ddlStax.DataValueField = "serviceTax";
                ddlStax.DataBind();
            }
        }

        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
  

    }

  
