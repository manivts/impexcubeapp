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


public partial class PIPLDebit : System.Web.UI.Page 
{
    VTS.ImpexCube.Utlities.Utility InvSequence = new VTS.ImpexCube.Utlities.Utility();
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
    string be;


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
        AutoCompleteExtender1.ContextKey = txtCompName.Text;

        fyear=(string)Session["FinancialYear"];
        if (IsPostBack == false)
        {
            //TallyAccountName();
            Session["VGUID"] = Guid.NewGuid().ToString();
            llbHead.Text = (string)Session["companyname"];
            Session["FYEARBill"] = fyear;
            GetXML();
            string lfyear = (string)Session["Lfyear"];
            chk.Text = lfyear;
            //Submit.Enabled = false;
            btnMail.Enabled = false;
            preview.Enabled = false;
            tblGrid.Visible = false;
            //TrAddr.Visible=false;
            //TrAddr1.Visible=false;
            Session["RBMODE"] = "IMP";
            Session["Invoice"] = "Debit Note";
            Session["IECName"] = "";
            Session["IECAdd1"] = "";
            Session["IECAdd2"] = "";
            Session["IECCity"] = "";
            //rbBill.SelectedValue = "DP";
            //TextBoxOnBlur();
            txtCompName.Text = (string)Session["IECName"];
            txtAdd1.Text = (string)Session["IECAdd1"];
            txtCity.Text = (string)Session["IECCity"];
            string LNA = (string)Session["Invoice"];
            string dates = DateTime.Now.ToString("dd/MM/yyyy");
            invDate.Text = dates;

            if (Request.QueryString["mode"] == "Contract")
            {
            
                lblIName.Text = "CONTRACT DEBIT NOTE - IMPORTS";
            
            }
            else
            {
                lblIName.Text = "DIRECT DEBIT - IMPORTS";
            }

            string tp = "DB";
            lblINumber.Text = "DB NO.:";
            DebitNoteGenerated(tp);

            //if ((string)Session["Invoice"] == "Invoice")
            //{
            //    lblIName.Text = "INVOICE";
            //    lblINumber.Text = "INVOICE NO.:";
            //}
            //else
            //{
            //    string tp = "DB";
            //    lblIName.Text = "DEBIT NOTE - IMPORTS";
            //    lblINumber.Text = "DB NO.:";
            //    DebitNoteGenerated(tp);
            //}
            if ((string)Session["RBMODE"] == "EXP")
            {
                Label16.Text = "SB NO/DT.";
                Label17.Text = "Item Exported";
                Label19.Text = "FOB Value";
            }
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
        TrAddr1.Visible = false;

        GrdPaddr.Visible = false;
        GridView2.Visible = true;

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

    protected void GridView2_SelectedIndexChanged(object sender, EventArgs e)
    {
        GridView2.Visible = false;
        GrdADDRSCROLL.Visible = false;
        GrdPaddr.Visible = false;

        // GridScroll.Visible = true;
        string CID = Convert.ToString(GridView2.SelectedDataKey.Value);
        //string Bill = "YES";

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
    
    public DataSet GetData(string fy)
    {
        SqlConnection conn1 = new SqlConnection(strImpex);
        conn1.Open();
        string sqlStatement1 = "select *  from T_JobCreation  order by jobno";
        SqlDataAdapter da1 = new SqlDataAdapter(sqlStatement1, conn1);
        DataSet ds1 = new DataSet();
        da1.Fill(ds1, "ijobno");
        conn1.Close();
        return ds1;
    }
    protected void TextBoxOnBlur()
    {
        //LessAd.Attributes.Add("onblur", "javascript:LessADvance();");
    }
    protected void BtnStandard_Click(object sender, EventArgs e)
    {

        if (Request.QueryString["mode"] == "Contract")
        {
            ContractDebitGo();
        }
        else
        {
            DirectDebitGo();
        }

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


    protected void ContractDebitGo()
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
                        GridView2.Visible = true;
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
                        //tblDebit.Visible = true;
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

                            tblDebit.Visible = true;
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

                    //tblDebit.Visible = true;
                    //tblGrid.Visible = true;
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
                    Tr2.Visible = false;
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
    public void DirectDebitGo()

    {
       // TallyAccountName();
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
                SqlConnection conn = new SqlConnection(strImpex);
                conn.Open();
                string sqlQuery = "";
                if (chk.Checked == true)
                    sqlQuery = "select *  from T_JobCreation where jobno='" + jno + "' ";
                else
                    sqlQuery = "select *  from T_JobCreation where jobno='" + jno + "' ";
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
                    txtAssValue.Text = row["TotalAssval"].ToString();
                    txtCustomDuty.Text = row["TotalDuty"].ToString();
                    //txtAssValue.Text = row["tot_ass_vl"].ToString();
                    //txtCustomDuty.Text = row["tot_duty"].ToString();
                    //string item = row["inv_dtl"].ToString();
                    //item = item.Replace("'", " ");
                    //txtImpotItem.Text = item;
                    //string pcode = row["party_code"].ToString();
                    GetItemImport(jobNo);
                    string sType = row["mode"].ToString();
                    Session["TransportMode"] = sType;
                    //Session["PCODE"] = pcode;
                    if (sType == "A")
                        lblIName.Text = "DEBIT NOTE - IMPORTS" + " - AIR SHIPMENT";
                    else
                        lblIName.Text = "DEBIT NOTE - IMPORTS" + " - SEA SHIPMENT";

                    //string be = row["beno"].ToString();
                   // string bedate = row["bedate"].ToString();
                    string be = row["beno"].ToString();
                    string bedate = row["bedate"].ToString();

                    txtBENo.Text = be.Trim();
                    txtBEDate.Text = bedate;
                    //if (bedate == "")
                    //{
                    //    Session["BENo"] = be + " dt." + bedate;
                    //}
                    //else
                    //{
                    //    bedate = row["bedate"].ToString();
                    //    Session["BENo"] = be + " dt." + bedate;
                    //    //DateTime beDate = Convert.ToDateTime(bedate);
                    //    //Session["BENo"] = be + " dt." + beDate.ToString("dd/MM/yyyy");
                    //}
                     //txtBENo.Text = (string)Session["BENo"];
                    string sqlQuery1 = "select *  from T_ShipmentDetails where jobno='" + jobNo + "'";
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
                    string sqlQuery3 = "select *  from T_ShipmentContainerInfo where jobno='" + jobNo + "' order by TransId";
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
                        string pref = "";
                        pref = cSize + " Ft - " + cTyp;
                        txtNCNTR.Text = pref;
                    }
                    string sqlQuery4 = "select *  from T_Importer " +
                                       "where  jobno='" + txtJNO.Text + "' ";
                    SqlDataAdapter da4 = new SqlDataAdapter(sqlQuery4, conn);
                    DataSet ds4 = new DataSet();
                    da4.Fill(ds4, "prtMast");
                    conn.Close();
                    DataRowView row4 = ds4.Tables["prtMast"].DefaultView[0];
                    txtCompName.Text = row4["Importer"].ToString();
                    Session["BranchID"] = row4["BranchSno"].ToString();
                    if ((string)Session["BranchID"] == "")
                    {
                        Session["BranchID"] = "0";
                    }
                    try
                    {
                        string VchType = string.Empty;
                        if (ChkDM.Checked)
                        {
                             VchType = "DM";
                        }
                        else
                        {
                             VchType = "DB";
                        }
                        txtInvSeqNo.Text = Convert.ToString(InvSequence.InvSeqNO(row4["Importer"].ToString(), VchType, txtJNO.Text));
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
                    txtAdd1.Text = addr1 ;
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
    protected void DebitNoteGenerated(string iType)
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
            Response.Write("<script>alert('Debit has not Found for Given Financial Year')</script>");

    }
    protected void Submit_Click(object sender, EventArgs e)
    {
       
       // GetTransaction();
        string tp = "DB";
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
            //Response.Write("<script>" + "alert('Debit Note has successfully Generated');" + "</script>");
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
      
        double la = Convert.ToDouble(LessAd.Text);
        double nt = Convert.ToDouble(BalanceDue.Text);
        string ino = Session["InvNo"].ToString();
        Int32 invno = Convert.ToInt32(ino);
        string suffix = txtSuffix.Text;
        string Notes = txtNote.Text;

        string impItem = txtImpotItem.Text;
        impItem = impItem.Replace("'", " ");
        txtImpotItem.Text = impItem;

        string ADDRESS = txtAdd1.Text.Trim(' ', '\r', '\n');
        ADDRESS = ADDRESS.Replace("'", " ");
        txtAdd1.Text = ADDRESS;
        string pREFF = txtParty_Reff.Text;
        pREFF = pREFF.Replace("'", " ");
        txtParty_Reff.Text = pREFF;

        string compname = txtCompName.Text;
        compname = compname.Replace("'"," ");
        txtCompName.Text = compname;

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
        SqlConnection conn = new SqlConnection(strImpex);
        string sqlQuery = " insert into M_IEC_Debit(invoice,invoiceDate,compName,Address1,address2,City,pincode,state," +
                          " phone,partyReff,jobNo,BLNo,BENoDate,importitem,Quantity,Ass_value,Container_no,Custom_Duty," +
                          " subTotal,Grand_total,less_advance,Net_total,sub_party,Nettotal_words,invoiceType,Mode,invoiceNo,"+
                          "entryBy,eDate,fyear,TransportMode,suffix,notes,impRemark,interRemark,VGUID,BranchID,TallyAccountName,TallySubPartyName,InvSeqNo,SubPartyAddr) values('" + lblInvNo.Text + "','" + dates + "','" + compname.Replace("'", " ") + "'," +
                          " '" + txtAdd1.Text.Replace("'", "") + "','" + (string)Session["state"] + "','" + txtCity.Text.Replace("'", "") + "','" + Session["Pin"] + "','" + Session["state"] + "','" + (string)Session["Phone"] + "','" + txtParty_Reff.Text.Replace("'", "") + "'," +
                          " '" + txtJobNo.Text + "','" + txtBLNo.Text + "','" + txtBENo.Text + " dt." + txtBEDate.Text + "','" + txtImpotItem.Text.Replace("'", "") + "','" + txtQty.Text + "'," +
                          " '" + txtAssValue.Text + "','" + txtNCNTR.Text + "','" + txtCustomDuty.Text + "'," + st + "," +
                          " " + st + "," + la + "," + nt + ",'" + txtSubParty.Text.Replace("'", "") + "','" + hdnRuppees.Value + "','" + InCode + "'," +
                          "'" + Session["RBMODE"] + "'," + invno + ",'" + (string)Session["USER-NAME"] + "','" + EntryDate + "','" + (string)Session["FinancialYear"] + "'," +
                          "'" + (string)Session["TransportMode"] + "','" + suffix + "','" + Notes + "','" + impRK + "','" + intRK + "','" + (string)Session["VGUID"] + "','" + (string)Session["BranchID"] + "','" + ddlTallyAccountName.SelectedItem.Text + "','" + subparty + "','" + InvSeqNo + "','" + txtSubPartyAddr.Text + "')";
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
                    updateRNO(invno, InCode, fyear);
                    try
                    {
                        string strQuery1 = "Update T_JobCreation set status_job='Y', BENo = '" + Session["be"] + "',BEDate = '" + txtBEDate.Text + "',TotalAssVal = '" + txtAssValue.Text + "',TotalDuty = '" + txtCustomDuty.Text + "'  where JobNo='" + txtJNO.Text + "'";
                        GetCommandIMP(strQuery1);
                        string VchType = string.Empty;
                        if (ChkDM.Checked)
                        {
                             VchType = "DM";
                        }
                        else
                        {
                             VchType = "DB";
                        }
                        InvSequence.InvSeqNOSave(txtCompName.Text, VchType);
                    }
                    catch
                    {
                    }
                    //string Query = "update ijob_pos set db_note_no='" + lblInvNo.Text + "',db_date='" + dates + "' where job_no='" + txtJobNo.Text + "'";
                    //GetCommand(Query, strconn1);
                    //GetCommand(Query, strconnJSU);
                    invFlag = 1;
                   
                    Response.Write("<script>" + "alert('Debit Note has successfully Generated');" + "</script>");
                }
            }


            Submit.Enabled = false;
            BtnStandard.Visible = false;
            btnMail.Enabled = true;
            preview.Enabled = true;
            New.Visible = true;

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
        Session["InvNo"] = lblInvNo.Text;
        string strQuery = "select * from M_iec_debit where invoice='" + lblInvNo.Text + "' and contr_code is null and particular1 is not null";
        SqlConnection conn = new SqlConnection(strImpex);

        SqlDataAdapter da = new SqlDataAdapter(strQuery, conn);
        DataSet ds = new DataSet();
        da.Fill(ds, "table");
        //if (ds.Tables["table"].Rows.Count == 0)
        //    ScriptManager.RegisterStartupScript(this, this.GetType(), "popup", "window.open('../Billing/CryInvoiceReportCTR.aspx','_blank','menubar=no, scrollbars=yes, toolbar=no, location=no, resizable=yes, left=20, top=20');", true);
          
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
            // avoid for thread abort exception error
        }
        catch (Exception ex)
        {
            Response.Write("<script>alert('" + ex.Message + "')</script>");

        }
    }
    protected void Menu2_MenuItemClick(object sender, MenuEventArgs e)
    {
      
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
        //string BiilType = rbBill.SelectedValue;
        //if (BiilType == "DP")
        //{
        //    //GrdADDRSCROLL.Visible = true;
        //    //GrdPaddr.DataSource = PartyAddr(pcode);
        //    //GrdPaddr.DataBind();
        //    //GrdPaddr.Visible = true;
        //    //TrAddr.Visible = true;
        //    //TrAddr1.Visible = true;
        //    //Panel2.Visible = true;
        //    txtSubParty.Text = "";
        //}
        //else
        //{
        //    GrdADDRSCROLL.Visible = false;
           
        //    GrdPaddr.Visible = false;
        //    TrAddr.Visible = false;
        //    TrAddr1.Visible = false;
        //    Panel2.Visible = true;
        //    trMain.Visible = true;
        //}
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
        }
        //string BiilType = rbBill.SelectedValue;
        // if (BiilType == "DP")
        //{

        //                txtSubParty.Text = "";
        // }

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
            DataRowView rw = dsS.Tables["Contract"].DefaultView[0];
            string test = rw["charge_desc"].ToString();

            for (int i = 0; i < dsS.Tables[0].Rows.Count; i++)
            {
                TextBox Particulars = (TextBox)GridView1.Rows[i].FindControl("txtDetails");
                Particulars.Text = dsS.Tables[0].Rows[i]["charge_desc"].ToString();

            }

            MainForm.Visible = true;
            trMain.Visible = true;
            tblDebit.Visible = true;      
            Panel2.Visible = true;
            SubForm.Visible = false;


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

    protected void txtJNO_TextChanged(object sender, EventArgs e)
    {
        string jno = txtJNO.Text;
        string jobNo = "";
        SqlConnection connM = new SqlConnection(strImpex);
        connM.Open();
        string sqlQueryM = "";

        sqlQueryM = "select *  from T_JobCreation where jobno='" + jno + "' ";
        SqlDataAdapter daM = new SqlDataAdapter(sqlQueryM, connM);
        try
        {
            DataSet dsM = new DataSet();
            daM.Fill(dsM, "iworkreg");
            connM.Close();
            DataRowView rowM = dsM.Tables["iworkreg"].DefaultView[0];
            jobNo = rowM["jobno"].ToString();
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
        Session["BasicInformation"] = txtJNO.Text + "~" + txtCompName.Text + "~" + txtJobNo.Text + "~" + txtSubParty.Text + "~" + (string)Session["BLNo"] + "~" + txtAdd1.Text + "~" + (string)Session["BENo"] + "~" + txtAdd1.Text + "~" + (string)Session["ImpotItem"] + "~" + txtCity.Text + "~" + (string)Session["QTY"] + "~" + (string)Session["state"] + "~" + (string)Session["Pin"] + "~" + txtAssValue.Text + "~" + (string)Session["Phone"] + "~" + txtNote.Text + "~" + txtParty_Reff.Text + "~" + (string)Session["CustomDuty"];
        Session["CompanyName"] = txtCompName.Text;
        ScriptManager.RegisterStartupScript(this, this.GetType(), "Error", "window.open('PopUp.aspx','_blank','width=600,height=250, menubar=no, scrollbars=yes, toolbar=no, location=no, resizable=yes, left=350, top=200, Right=200=, bottom=200');", true);

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
        string SubPartyQuery = "select AccountName,Address1,City,State  from M_AccountMaster where AccountName ='" + txtSubParty.Text.Replace("'","") + "' and Acc_Group='" + txtCompName.Text.Replace("'","") + "' ";
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
            Response.Write("<script>alert('Sub Party Name is not available. Please create in Master page')</script>");
        }
    }

    protected void txtAdd1_TextChanged(object sender, EventArgs e)
    {

    }

    protected void New_Click(object sender, EventArgs e)

        {
        if (Request.QueryString["mode"] == "Contract")
        {
            BtnStandard.Visible = true;
            Response.Redirect("~/Billing/PIPLDebit.aspx?mode=Contract");
        }
        else
        {
            BtnStandard.Visible = true;
            Response.Redirect("~/Billing/PIPLDebit.aspx?");
        }
        
    }
      
}