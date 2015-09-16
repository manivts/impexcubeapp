using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using VTS.ImpexCube.Business;
using System.Drawing;
using System.IO;
using System.Text;

namespace ImpexCube
{
    public partial class frmPRoductMainPage : System.Web.UI.Page
    {
        string strcon = (string)ConfigurationManager.ConnectionStrings["Constr"].ConnectionString;
        VTS.ImpexCube.Business.ShipmentBL objShipment = new VTS.ImpexCube.Business.ShipmentBL();
        VTS.ImpexCube.Business.ProductDetailsBL obj = new VTS.ImpexCube.Business.ProductDetailsBL();
        VTS.ImpexCube.Business.CommonBL obj1 = new VTS.ImpexCube.Business.CommonBL();
        VTS.ImpexCube.Utlities.Utility joblog = new VTS.ImpexCube.Utlities.Utility();
        static DataSet dsrun = new DataSet();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                Label pagename;
                pagename = (Label)Master.FindControl("lblName");
                pagename.Text = "Product";
                
                if (!IsPostBack)
                {
                    if (Request.QueryString["Mode"] == "Direct")
                    {
                        JobNo();
                        ProductType();
                        Unit();
                        DropCountry();
                        btnUpdate.Visible = false;
                        btnadd.Visible = true;
                        Session["ProductID"] = "";
                        divProduct.Visible = false;
                        btnbckProduct.Visible = false;
                    }
                    else if (Request.QueryString["Mode"] == "Invoice")
                    {
                        ProductType();
                        Unit();
                        JobNo();
                        DropCountry();
                        ddlJobNo.SelectedValue = (string)Session["JobNo"];
                        InvNo(ddlJobNo.SelectedValue);
                        ddlInvNo.SelectedValue = (string)Session["InvoiceNo"];
                        GetJobDetails(ddlJobNo.SelectedValue);
                        GetInvoiceDetails(ddlJobNo.SelectedValue, ddlInvNo.SelectedValue);
                        LoadGrid(ddlInvNo.SelectedValue, ddlJobNo.SelectedValue);
                        divProduct.Visible = true;
                        btnUpdate.Visible = false;
                        btnadd.Visible = true;
                        footertotrow();
                        Label MJobNo = (Label)Master.FindControl("lblJobNo");
                        MJobNo.Text = "IMP/" + (string)Session["JobNo"] + "/" + (string)Session["FYear"];
                      }
                    else
                        Response.Redirect("frmLogin.aspx");
                }
            }
            catch
            {
            }
        }

        public void JobNo()
        {
            DataSet dt = obj1.GetJobNo();
            ddlJobNo.DataSource = dt;
            ddlJobNo.DataValueField = "JobNo";
            ddlJobNo.DataTextField = "JobNo";
            ddlJobNo.DataBind();
        }

        public void InvNo(string JobNo)
        {
            DataSet dt = obj1.GetInvNo(JobNo);
            if (dt.Tables["InvNo"].Rows.Count != 0)
            {
                ddlInvNo.DataSource = dt;
                ddlInvNo.DataValueField = "InvoiceNo";
                ddlInvNo.DataTextField = "InvoiceNo";
                ddlInvNo.DataBind();
            }
        }
        // Call the product Type
        public void ProductType()
        {
            DataSet dt = obj1.GetProductType();
            ddltype.DataSource = dt;
            ddltype.DataValueField = "ProductDutyType";
            ddltype.DataTextField = "ProductDutyType";
            ddltype.DataBind();

        }

        public void Unit()
        {
            DataSet dt = obj1.GetUnit();
            ddlUnit.DataSource = dt;
            ddlUnit.DataValueField = "UnitShort";
            ddlUnit.DataTextField = "UnitShort";
            ddlUnit.DataBind();

        }

        private void DropCountry()
        {
            DataSet ds = objShipment.GetCountry();
            if (ds.Tables["country"].Rows.Count != 0)
            {
                ddlcountryorigin .DataSource = ds;
                ddlcountryorigin.DataTextField = "CountryName";
                ddlcountryorigin.DataValueField = "CountryCode";
                ddlcountryorigin.DataBind();
            }
        }

        public void samp(int a)
        {
           
           
        }
        public void samp(out int a)
        {
            a = 10;

        }
        protected void btnadd_Click(object sender, EventArgs e)
        {
            try
            {
                
                double invamt = Convert.ToDouble(lblInvAmt.Text);
                double freight = Convert.ToDouble(lblFrie.Text);
                double insu = Convert.ToDouble(lblIns.Text);

                double OthCharges = OtherCharges(ddlJobNo.SelectedValue,ddlInvNo.SelectedValue);
                double misc = Convert.ToDouble(lblMisc.Text) + OthCharges;
                double agency = Convert.ToDouble(lblAgen.Text);
                //if ((string)Session["ProductID"] == "" || (string)Session["ProductID"] == null)
                //{
                    double qty = Convert.ToDouble(txtqty.Text);
                    double unitprice = Convert.ToDouble(txtunitprice.Text);
                    double amt = Convert.ToDouble(txtamount.Text);
                    //string RitcNo = (string)Session["RITCCode"];
                    string RitcNo = txtRITC.Text;
                    double Exrate = Convert.ToDouble(lblExRate.Text);
                    double ProdValue = amt * Exrate;


                    double FreightAmount = (freight / invamt) * ProdValue;
                    double insuAmount = (insu / invamt) * ProdValue;
                    double miscAmount = (misc / invamt) * ProdValue;
                    double agencyAmount = (agency / invamt) * ProdValue;

                    //FreightAmount = FreightAmount + miscAmount;
                    double airfright = (ProdValue+miscAmount) * 20 / 100;
                   
                    double FreightAmount1 = FreightAmount / Exrate;
                    double insuAmount1 = insuAmount / Exrate;
                    double miscAmount1 = miscAmount / Exrate;
                    double agencyAmount1 = agencyAmount / Exrate;

                    double AddlChrg = Convert.ToDouble(lblAdlChrg.Text);
                    double AddlChrgHS = (AddlChrg / invamt) * ProdValue;
                    double AddlChrgHS1 = AddlChrgHS / Exrate;

                //*********************** Assable Value Calculation ******************************
                    //airfright = (ProdValue + miscAmount) * 20 / 100;
                    //if AIR airfright >= FreightAmount)
                    //Assable Value =Product Value+Freight Amount+Insurance Amount+Mis Amount+Agency Charge+Aditional Charge
                    //Loading Charge=Assable Value*1/100
                    //Assable Value=Assable Value+Loading Charge
                    double totamt = 0;
                    if (lblMode.Text == "Air")
                    {
                        if (airfright >= FreightAmount)
                        {
                            totamt = ProdValue + FreightAmount + insuAmount + miscAmount + agencyAmount + AddlChrgHS;
                        }
                        else
                        {
                            totamt = ProdValue + airfright + insuAmount + miscAmount + agencyAmount + AddlChrgHS;
                        }
                    }
                    else
                    {
                        totamt = ProdValue + FreightAmount + insuAmount + miscAmount + agencyAmount + AddlChrgHS;
                    }
                    double loadingcharge = totamt / 100;
                    double assvalue = totamt + loadingcharge;
                    Session["AssValue"] = assvalue;
                //***********************************************************************************
                    Session["SchemeName"] = ddltype.SelectedValue;
                    string CreatedBy = (string)Session["USER-NAME"];
                    string createddate = Convert.ToString(DateTime.Now);
                    Session["SchemeName"] = ddltype.SelectedValue;
                    if ((string)Session["SchemeName"] != "DUTIABLE")
                    {
                        btnSch.Text = (string)Session["SchemeName"];
                        btnSch.Visible = true;
                    }
                    else
                    {
                        btnSch.Text = "Scheme";
                        btnSch.Visible = false;
                    }
                    int slno = GridView1.Rows.Count + 1;
                    int result = obj.InsertProductDetals(ddlJobNo.SelectedValue, ddlInvNo.SelectedValue, txtProductFamily.Text, txtProductCode.Text, txtpro.Text, ddltype.SelectedValue, qty, ddlUnit.SelectedValue, unitprice, amt, RitcNo, ProdValue, FreightAmount1, insuAmount1, miscAmount1, agencyAmount1, loadingcharge, assvalue, CreatedBy, createddate, slno);
                    LoadGrid(ddlInvNo.SelectedValue, ddlJobNo.SelectedValue);
                    footertotrow();
                    Session["ProductID"] = result;
                   // productmain();
                    txtCTH.Text =txtRITC.Text;
                    txtCETNo.Text = txtRITC.Text;
                    btnadd.Enabled = false;
                    btnsaveGeneric_Click(sender, e);
                  // GetProductDutyPer();
                    btnsaveCustom_Click(sender, e);
                    if (result == 0)
                    {
                        ClassMsg.Show("Kindly veryfy the data");
                    }
                    else
                    {
                        lblmsg.ForeColor = Color.Green;
                        lblmsg.Text = "Successfully Saved Product  " + result;
                    }
            }
            catch(Exception ex)
            {
                string Message = ex.Message;
            }
        }
        public void footertotrow()
        {
            try
            {
                double TotalAmt = 0;
                int i = 0;
                foreach (GridViewRow gv in GridView1.Rows)
                {
                    string amt = GridView1.Rows[i].Cells[9].Text;
                    TotalAmt = TotalAmt + Convert.ToDouble(amt);
                    i++;
                }
                GridView1.FooterRow.Cells[4].Text = "Total Amount ";
                GridView1.FooterRow.Cells[9].Text = Convert.ToString(TotalAmt);
               
            }
            catch (Exception ex)
            {

            }
        }
        protected void ddlJobNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            //string jobno =   ddlJobNo.SelectedItem.Text;
            //string username = (string)Session["USER-NAME"];
            //string joblogret = joblog.SelectJobLog(username, jobno);
            //if (joblogret == "NoJob")
            //{
            //    joblog.UpdateJobLog(username);
            //    int jb = joblog.InsertJobLog(username, jobno);
            //    if (jb == 1)
            //    {
                    try
                    {
                        lblJobDate.Text = "";
                        lblMode.Text = "";
                        lblCustom.Text = "";


                        lblInvDate.Text = "";
                        lblInvAmt.Text = "";
                        lblTerms.Text = "";
                        lblExRate.Text = "";
                        lblCurrency.Text = "";
                        lblFrie.Text = "";
                        lblIns.Text = "";
                        lblMisc.Text = "";
                        lblAgen.Text = "";
                        lblNoofProduct.Text = "";
                        txtPONo.Text = "";
                        txtPODate.Text = "";

                        ddlInvNo.Items.Clear();
                        ddlInvNo.Items.Insert(0, new ListItem("~Select~", "~Select~"));
                        
                        GetJobDetails(ddlJobNo.SelectedValue);
                        InvNo(ddlJobNo.SelectedValue);
                        LoadGrid(ddlInvNo.SelectedItem.Text, ddlJobNo.SelectedValue);

                        Label MJobNo = (Label)Master.FindControl("lblJobNo");
                        MJobNo.Text = "IMP/"+ ddlJobNo.SelectedValue+"/"+(string)Session["FYear"];
                    }
                    catch (Exception ex)
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('DataBase Error: " + ex.Message + "');", true);
                    }
            //    }
            //}
            //else
            //{
            //    string mess = "This Job can be used  " + joblogret;
            //    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('" + mess + "');", true);
            //}
        }
        protected void ddlInvNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                lblInvDate.Text = "";
                lblInvAmt.Text = "";
                lblTerms.Text = "";
                lblExRate.Text = "";
                lblCurrency.Text = "";
                lblFrie.Text = "";
                lblIns.Text = "";
                lblMisc.Text = "";
                lblAgen.Text = "";
                lblNoofProduct.Text = "";
                txtPONo.Text = "";
                txtPODate.Text = "";

                divProduct.Visible = true;
                btnbckProduct.Visible = false;
                Panel1.Visible = false;
                Panel2.Visible = false;
                Panel3.Visible = false;
                Panel4.Visible = false;
                Panel5.Visible = false;
                Panel6.Visible = false;
                btnNew.Enabled = true;

                LoadGrid(ddlInvNo.SelectedItem.Text, ddlJobNo.SelectedValue);
                GetInvoiceDetails(ddlJobNo.SelectedValue, ddlInvNo.SelectedValue);
                footertotrow();
                divProduct.Visible = true;
            }
            catch (Exception ex)
            {
            }
        }
        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {

                btnUpdate.Visible = true;
                btnadd.Visible = false;
                ProductType();
                Unit();
                int productid=Convert.ToInt16(GridView1.SelectedRow.Cells[3].Text);
                Session["ProductID"] = GridView1.SelectedRow.Cells[3].Text;
                Session["slno"] = GridView1.SelectedRow.Cells[2].Text;
                DataSet ds = obj.GetProductDetails((string)Session["ProductID"]);
                if (ds.Tables["Table"].Rows.Count != 0)
                {
                    DataRowView row = ds.Tables["Table"].DefaultView[0];
                    lblProduct.Text = row["ProductDesc"].ToString();
                    txtpro.Text = row["ProductDesc"].ToString();
                    ddltype.SelectedValue = row["ProType"].ToString();
                    Session["SchemeName"] = row["ProType"].ToString();
                    txtqty.Text = row["Qty"].ToString();
                    ddlUnit.SelectedValue = row["Unit"].ToString();
                    txtunitprice.Text = row["UnitPrice"].ToString();
                    txtamount.Text = row["Amount"].ToString();
                    txtINRAmount.Text = row["ProdAmtRs"].ToString();
                    txtProductCode.Text = row["ProductCode"].ToString();
                    txtProductFamily.Text = row["ProductFamily"].ToString();
                    txtRITC.Text = row["RITCNo"].ToString();
                    lblAssableValue.Text = row["AssableValue"].ToString(); //AssableValue
                    lblDutyAmount.Text = row["TotalDutyAmt"].ToString(); //TotalDutyAmt
                    if ((string)Session["SchemeName"] != "DUTIABLE")
                    {
                        btnSch.Text = (string)Session["SchemeName"];
                        btnSch.Visible = true;
                    }
                    else
                    {
                        btnSch.Text = "Scheme";
                        btnSch.Visible = false;
                    }
                }
                SetProductdetails(productid);
                
                gridbind();
                double bcdvalue = Convert.ToDouble(txtBasicDutyRate.Text);
                //double cvd
                if (bcdvalue == 0)
                {
                    GetProductDutyPer();
                }
                GridView1.SelectedRow.Focus();
               // GetProductDutyPer();
               // ScriptManager.RegisterClientScriptBlock(Page, this.GetType(), "CallJS", "afterpostback();", true);
            }
            catch
            {
            }
        }

        public void LoadGrid(string InvNo, string JobNo)
        {
            DataSet dt = obj.loadproductgrid(InvNo, JobNo);
            if (dt.Tables["Table"].Rows.Count != 0)
            {
                GridView1.DataSource = dt;
                GridView1.DataBind();
            }
            else
            {
                GridView1.DataSource = null;
                GridView1.DataBind();
            }
            
        }
        private void clear()
        {
            btnUpdate.Visible = false;
            btnadd.Visible = true;
            btnadd.Enabled = true;
            txtpro.Text = "";
            //txtqty.Text = "";
           // txtunitprice.Text = "";
            txtamount.Text = "";
            txtProductCode.Text = "";
           // txtProductFamily.Text = "";
            Session["ProductID"] = "";
            Session["SchemeName"] = "";
            btnSch.Text = "Scheme";
            btnSch.Visible = false;
            
        }
        public void GetJobDetails(string JobNo)
        {
            try
            {
                    DataSet ds = obj1.GetJobImportShipment(JobNo);
                    if (ds.Tables["Table"].Rows.Count != 0)
                    {
                        DataRowView row = ds.Tables["Table"].DefaultView[0];
                        lblJobDate.Text = row["JobReceivedDate"].ToString();
                        lblMode.Text = row["Mode"].ToString();
                        lblCustom.Text = row["Custom"].ToString();
                        ddlcountryorigin.SelectedValue = row["CountryOriginCode"].ToString();
                    }
            }
            catch
            {
            }
        }
        public void GetInvoiceDetails(string JobNo, string InvNo)
        {
            try
            {
                DataSet ds = obj1.GetInvoiceDetails(JobNo, InvNo);
                if (ds.Tables["Table"].Rows.Count != 0)
                {
                    DataRowView row = ds.Tables["Table"].DefaultView[0];
                    lblInvDate.Text = row["InvoiceDate"].ToString();
                    lblInvAmt.Text = row["InvoiceProductINRValues"].ToString();
                    lblTerms.Text = row["InvoiceTerms"].ToString();
                    lblExRate.Text = row["InvoiceExchangeRates"].ToString();
                    lblCurrency.Text = row["InvoiceCurrency"].ToString();
                    lblFrie.Text = row["FreightINRAmount"].ToString();
                    lblIns.Text = row["InsuranceINRAmount"].ToString();
                    lblMisc.Text = row["MisINRAmount"].ToString();
                    lblAdlChrg.Text = row["HighSeaAmtINR"].ToString();
                    lblAgen.Text = row["AgencyINRAmount"].ToString();
                    lblNoofProduct.Text = row["NoofProduct"].ToString();//NoofProduct
                    txtPONo.Text = row["PONo"].ToString();
                    txtPODate.Text = row["PODate"].ToString();
                }
            }
            catch
            {
            }
        }
        //Get the Master Product Tax Details.
        public void GetProductDutyPer()//GetProductDuty
        {
            try
            {
                txtCTH.Text = txtRITC.Text;
                txtCETNo.Text = txtRITC.Text;
                //DataSet ds = obj.GetProductDutyPer(txtRITC.Text);
                //if (ds.Tables["Table"].Rows.Count != 0)
                //{
                //    DataRowView row = ds.Tables["Table"].DefaultView[0];
                //    //txtBasicDutyNotn.Text = row["BASNotn"].ToString();
                //    //// txtBasicDutySno.Text = row["BasicDutySno"].ToString();
                //    //txtBasicDutyRate.Text = row["BASDuty"].ToString();
                //    //txtEducessNotn.Text = row["EDU_CESS_NOTN"].ToString();
                //    //txtEduCessSNo.Text = row["EDU_CESS_SNO"].ToString();
                //    //txtEducessRate.Text = row["EDU_CESS_RATE"].ToString();
                //    //txtSHECessNotn.Text = row["SHE_CESS_NOTN"].ToString();
                //    //txtSHECessSNo.Text = row["SHE_CESS_SNO"].ToString();
                //    //txtSHECessRate.Text = row["SHE_CESS_RATE"].ToString();
                //    //txtAddlExNotn.Text = row["CVDNotn"].ToString();
                //    ////txtAddlExSlNo.Text = row["CVDDuty"].ToString();
                //    //txtAddlExRate.Text = row["CVDDuty"].ToString();
                //    //txtExCVDNotn.Text = row["AddlDuty_NOTN"].ToString();
                //    //txtExCVDSlNo.Text = row["AddlDuty_SNO"].ToString();
                //    //txtEXCVDRate.Text = row["AddlDuty_RATE"].ToString();
                //    //POLICYPARA,POLICY_YR
                //    txtpolicy.Text = row["POLICYPARA"].ToString();
                //    txtpyear.Text = row["POLICY_YR"].ToString();
                //}
                txtEducessRate.Text = "2";
                txtSHECessRate.Text = "1";
                txtExCVDNotn.Text = "019/2006";
                txtEXCVDRate.Text = "4";

                txtBasicDutyRate.Text = "0";
                txtBasicDutyAmount.Text = "0";
                txtAddlExRate.Text = "0";
                txtAddlExAmount.Text = "0";
                txtMRP.Text = "0";
                txtMRPAbatement.Text = "0";
                //txtEXCVDRate.Text = "0";
                //txtEducessRate.Text = "0";
                //txtSHECessRate.Text = "0";
                txtExEduCessRate.Text = "0";
                txtExSHECessRate.Text = "0";
                txtExCessRate.Text = "0";
                txtExCessAmount.Text = "0";

                DataSet ds1 = obj.GetBCDRTA(txtRITC.Text);
                if (ds1.Tables["Table"].Rows.Count != 0)
                {
                    DataRowView row1 = ds1.Tables["Table"].DefaultView[0];
                    txtBasicDutyRate.Text = row1["RTA"].ToString();
                }
                DataSet ds2 = obj.GetCVDRTA(txtRITC.Text);
                if (ds2.Tables["Table"].Rows.Count != 0)
                {
                    DataRowView row2 = ds2.Tables["Table"].DefaultView[0];
                    txtAddlExRate.Text = row2["CVDRTA"].ToString();
                }
                string chapter = txtRITC.Text.Substring(0, 2);
                DataSet ds3 = obj.GetCVDUserNoti(chapter);
                if (ds3.Tables["Table"].Rows.Count != 0)
                {
                    DataRowView row3 = ds3.Tables["Table"].DefaultView[0];
                    txtAddlExNotn.Text = row3["Notification"].ToString();
                    txtAddlExSlNo.Text = row3["SerialNo"].ToString();
                    txtAddlExRate.Text = row3["Duty"].ToString();
                }
                DataSet ds4 = obj.GetSADUserNoti(chapter);
                if (ds4.Tables["Table"].Rows.Count != 0)
                {
                    DataRowView row4 = ds4.Tables["Table"].DefaultView[0];
                    txtExCVDNotn.Text = row4["Notification"].ToString();
                    txtExCVDSlNo.Text = row4["SerialNo"].ToString();
                    txtEXCVDRate.Text = row4["Duty"].ToString();
                }
            }
            catch
            {
            }
        }
        public void SetProductdetails(int productid)
        {
            try
            {
                DataSet ds = obj.ReadDuty(productid);
                if (ds.Tables["Table"].Rows.Count != 0)
                {
                    DataRowView rv = ds.Tables["Table"].DefaultView[0];
                    txtgenericdesc.Text = rv["GenericDesc"].ToString();
                    txtmanufacturer.Text = rv["Manufacturer"].ToString();
                    brand.Text = rv["Brand"].ToString();
                    txtmodel.Text = rv["Model"].ToString();
                    txtaccessories.Text = rv["Accessories"].ToString();
                    string CountryOrigin = rv["CountryofOrigin"].ToString();
                    if (CountryOrigin.Length == 2)
                    {
                        ddlcountryorigin.SelectedValue = rv["CountryofOrigin"].ToString();
                    }
                    endcase.Text = rv["EndUse"].ToString();
                    if (rv["CETNo"].ToString() != "")
                    {
                        txtCETNo.Text = rv["CETNo"].ToString();
                        txtCTH.Text = rv["CTHNo"].ToString();
                        txtRITC.Text = rv["RITCNo"].ToString();
                    }
                    string MRPDuty = rv["MRPDuty"].ToString();
                    if (MRPDuty == "True")
                    {
                        chkMRPDuty.Checked = true;
                    }
                    else
                    {
                        chkMRPDuty.Checked = false;
                    }
                    txtMRPSNo.Text = rv["MRPSNo"].ToString();
                    txtMRP.Text = rv["MRP"].ToString();
                    txtMRPUnit.Text = rv["MRPUnit"].ToString();
                    txtMRPAbatement.Text = rv["MRPAbatement"].ToString();
                    txtAddlExNotn.Text = rv["AddlExNotn"].ToString();
                    txtAddlExSlNo.Text = rv["AddlExSlNo"].ToString();
                    txtAddlExRate.Text = rv["AddlExRate"].ToString();
                    ddlAddlExFlag.SelectedValue = rv["AddlExFlag"].ToString();
                    txtAddlExUnit.Text = rv["AddlExUnit"].ToString();
                    txtAddlExAmount.Text  = rv["AddlExAmount"].ToString();

                    txtExEduCessNotn.Text =  rv["ExEduCessNotn"].ToString();
                    txtExEduCessslNo.Text = rv["ExEduCessSlNo"].ToString();
                    txtExEduCessRate.Text = rv["ExEduCessRate"].ToString();
                    txtExSHECessRate.Text = rv["ExSHECessRate"].ToString();
                    txtExCVDNotn.Text = rv["ExCVDNotn"].ToString();
                    txtExCVDSlNo.Text = rv["ExCVDSlNo"].ToString();
                    txtEXCVDRate.Text = rv["EXCVDRate"].ToString();
                    txtExGSIAddlDutyNotn.Text = rv["ExGSIAddlDutyNotn"].ToString();
                    txtExGSIAddlDutySlNo.Text = rv["ExGSIAddlDutySlNo"].ToString();
                    txtExGSIAddlDutyRate.Text = rv["ExGSIAddlDutyRate"].ToString();
                    ddlExGSIAddlDutyFlag.SelectedValue = rv["ExGSIAddlDutyFlag"].ToString();
                    txtExGSIAddlDutyAmount.Text = rv["ExGSIAddlDutyAmount"].ToString();
                    txtExGSIAddlDutyUnit.Text = rv["ExGSIAddlDutyUnit"].ToString();
                    txtExSPLExDutyNotn.Text = rv["ExSPLExDutyNotn"].ToString();
                    txtExSPLExDutySlNo.Text = rv["ExSPLExDutySlNo"].ToString();

                    txtpolicy.Text = rv["PolicyPara"].ToString();
                    txtpyear.Text = rv["PolicyYear"].ToString();
                    Session["RITCCode"] = rv["RITCNo"].ToString();
                    Session["AssValue"] = rv["AssableValue"].ToString();
                    txtPONo.Text = rv["PONo"].ToString();
                    txtPODate.Text = rv["PoDate"].ToString();

                    txtExSPLExDutyRate.Text = rv["ExSPLExDutyRate"].ToString();
                    ddlExSPLExDutyFlag.SelectedValue = rv["ExSPLExDutyFlag"].ToString();
                    txtExSPLExDutyAmount.Text = rv["ExSPLExDutyAmount"].ToString();
                    txtExSPLExDutyUnit.Text = rv["ExSPLExDutyUnit"].ToString();
                    txtExTTAAddlDutyNotn.Text = rv["ExTTAAddlDutyNotn"].ToString();
                    txtExTTAAddlDutySlNo.Text = rv["ExTTAAddlDutySlNo"].ToString();
                    txtExTTAAddlDutyRate.Text = rv["ExTTAAddlDutyRate"].ToString();
                    ddlExTTAAddlDutyFlag.SelectedValue = rv["ExTTAAddlDutyFlag"].ToString();
                    txtExTTAAddlDutyAmount.Text = rv["ExTTAAddlDutyAmount"].ToString();
                    txtExTTAAddlDutyUnit.Text = rv["ExTTAAddlDutyUnit"].ToString();
                    txtExHealthCessNotn.Text = rv["ExHealthCessNotn"].ToString();
                    txtExHealthCessSlNo.Text = rv["ExHealthCessSlNo"].ToString();
                    txtExHealthCessRate.Text = rv["ExHealthCessRate"].ToString();
                    ddlExHealthCessFlag.SelectedValue = rv["ExHealthCessFlag"].ToString();
                    txtExHealthCessAmount.Text = rv["ExHealthCessAmount"].ToString();
                    txtExHealthCessUnit.Text = rv["ExHealthCessUnit"].ToString();
                    txtExCessNotn.Text = rv["ExCessNotn"].ToString();
                    txtExCessSlNo.Text = rv["ExCessSlNo"].ToString();
                    txtExCessRate.Text = rv["ExCessRate"].ToString();
                    ddlExCessFlag.SelectedValue = rv["ExCessFlag"].ToString();
                    txtExCessAmount.Text = rv["ExCessAmount"].ToString();
                    txtExCessUnit.Text = rv["ExCessUnit"].ToString();
                    txtExSADNotn.Text = rv["ExSADNotn"].ToString();
                    txtExSADSlno.Text = rv["ExSADSlno"].ToString();
                    txtExSADRate.Text = rv["ExSADRate"].ToString();


                    txtEXIM.Text = rv["EximSchCode"].ToString();
                    txtEximSchemeDesc.Text = rv["EximSchDesc"].ToString();
                    txtSchemeNotn.Text = rv["SchNoten"].ToString();
                    txtSchemeUnit.Text = rv["SchNotenUnit"].ToString();
                    txtSchemeDesc.Text = rv["SchNotenDesc"].ToString();

                    ddlRateType.SelectedValue = rv["RateType"].ToString();
                    txtBasicDutyNotn.Text = rv["BasicDutyNotn"].ToString();
                    txtBasicDutySno.Text = rv["BasicDutySno"].ToString();
                    txtBasicDutyRate.Text = rv["BasicDutyRate"].ToString();
                    ddlBasicDutyFlag.SelectedValue = rv["BasicDutyFlag"].ToString();
                    txtBasicDutyAmount.Text = rv["BasicDutyAmount"].ToString();
                    txtBasicDutyUnit.Text = rv["BasicDutyUnit"].ToString();
                    txtAddlNotn.Text = rv["AddlNotn"].ToString();
                    txtAddlNotnSno.Text = rv["AddlSNo"].ToString();
                    txtEducessNotn.Text = rv["EduCessNotn"].ToString();
                    txtEduCessSNo.Text = rv["EduCessSNo"].ToString();
                    txtEducessRate.Text = rv["EduCessRate"].ToString();
                    txtSHECessSNo.Text = rv["SHECessSno"].ToString();
                    txtSHECessNotn.Text = rv["SHECess"].ToString();
                    txtSHECessRate.Text = rv["SHECessRate"].ToString();
                    txtNCDNotn.Text = rv["NCDNotn"].ToString();
                    txtNCDSNo.Text = rv["NCDSno"].ToString();
                    txtNCDRate.Text = rv["NCDRate"].ToString();
                    ddlNCDFlag.SelectedValue = rv["NCDDFlag"].ToString();
                    txtNCDAmount.Text = rv["NCDAmount"].ToString();
                    txtNCDUnit.Text = rv["NCDUnit"].ToString();
                    txtNCDRule.Text = rv["NCDRule"].ToString();
                    txtSurNotn.Text = rv["SurNotn"].ToString();
                    txtSurSno.Text = rv["SurSno"].ToString();
                    txtSurRate.Text = rv["SurRate"].ToString();
                    txtSAPTNotn.Text = rv["SAPTANotn"].ToString();
                    txtSAPTSno.Text = rv["SAPTASNo"].ToString();
                    txtSAPTDesc.Text = rv["SAPTADesc"].ToString();
                    txtTarrifNotn.Text = rv["TariffValNotn"].ToString();
                    txtTraiffSno.Text = rv["TariffValSNo"].ToString();
                    txtTarriffUnitQty.Text = rv["TariffUnitQty"].ToString();
                    txtTraiffUnit.Text = rv["TariffUnit"].ToString();
                    txttraiffRate.Text = rv["TariffRate"].ToString();
                    txttraiffAmount.Text = rv["TariffAmount"].ToString();
                    txtAltqty.Text = rv["AQty1"].ToString();
                    if ((rv["AQty1Unit"].ToString() != null) && (rv["AQty1Unit"].ToString() != ""))
                    {
                        ddlAltUnit.SelectedValue = rv["AQty1Unit"].ToString();
                    }
                }
            }
            catch(Exception ex )
            {
                string Message = ex.Message;
            }
        }
        protected void btnsaveGeneric_Click(object sender, EventArgs e)
        {
            try
            {
                string ProductID =(string)Session["ProductID"].ToString();
                string Jobno = ddlJobNo.SelectedValue;
                string InvoiceNo = ddlInvNo.SelectedItem.Text;
                string ProductDesc = lblProduct.Text;
                double AQty1 = 0.00;
                if (txtAltqty.Text != "")
                {
                     AQty1 = Convert.ToDouble(txtAltqty.Text);
                }
                string AQty1Unit = ddlAltUnit.SelectedValue;
                int result = obj.UpdateGenericDesc(txtgenericdesc.Text, txtmanufacturer.Text, brand.Text, txtmodel.Text, txtaccessories.Text, endcase.Text, ddlcountryorigin.SelectedValue, Jobno, InvoiceNo, ProductDesc, ProductID, AQty1, AQty1Unit);
                if (result == 1)
                {
                    lblmsg.ForeColor = Color.Green;
                    lblmsg.Text = "Generic details saved successfully ";
                }
                else
                {
                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "alert('Please the check values again');", true);
                }
            }
            catch (Exception ex)
            {
            }
        }

        protected void btnsaveCustom_Click(object sender, EventArgs e)
        {
            try
            {
                string ProductID = (string)Session["ProductID"].ToString();
                string Jobno = ddlJobNo.SelectedValue;
                string InvoiceNo = ddlInvNo.SelectedItem.Text;
                string ProductDesc = lblProduct.Text;
                string EximSchCode = txtEXIM.Text;
                string EximSchDesc = txtEximSchemeDesc.Text;
                string SchNoten = txtSchemeNotn.Text;
                string SchNotenUnit = txtSchemeUnit.Text;
                string SchNotenDesc = txtSchemeDesc.Text;
                string CTHNo = txtCTH.Text;
                string RateType = ddlRateType.SelectedValue;
                double qty = Convert.ToDouble(txtqty.Text);
                string ITCLocation = "";// txtITCLocation.Text;
                string ITCHSCode = "";// txtITCCHSCode.Text;
                string PoNo = txtPONo.Text;
                string PoDate = txtPODate.Text;
                string PolicyPara = txtpolicy.Text;
                string PolicyYear = txtpyear.Text;


                //Other duty calculation     **************  UnKnown Field *******************
                string ExSADNotn = txtExSADNotn.Text;
                string ExSADSlno = txtExSADSlno.Text;
                double ExSADRate = Convert.ToDouble(txtExSADRate.Text);
                string AddlSno = txtAddlNotnSno.Text;

                //*******************Duty Calculation****************************
                double AssValue = Convert.ToDouble(Session["AssValue"]);

                //1. **************** SAPTA   ***************************
                string SAPTANotn = txtSAPTNotn.Text;
                string SAPTASNo = txtSAPTSno.Text;
                string SAPTADesc = txtSAPTDesc.Text;
                double SAPTADutyAmount = 0;
                double SAPTADutyAmountQty = 0;

                //2. **************** BCD *************************************************
                string BasicDutyNotn = txtBasicDutyNotn.Text;
                string BasicDutySno = txtBasicDutySno.Text;
                double BasicDutyRate = Convert.ToDouble(txtBasicDutyRate.Text);
                string BasicDutyFlag = ddlBasicDutyFlag.SelectedValue;
                string BasicDutyUnit = txtBasicDutyUnit.Text;
                double BasicDutyAmount = Convert.ToDouble(txtBasicDutyAmount.Text);
                double BCDTax = BasicDutyRate;

                if (SAPTADesc != "")
                {
                    SAPTADutyAmount = (((AssValue * BCDTax) / 100) * Convert.ToDouble(SAPTADesc)) / 100;
                    SAPTADutyAmountQty = (((qty * BasicDutyAmount)) * Convert.ToDouble(SAPTADesc)) / 100;
                }
                double BCD = ((AssValue * BCDTax) / 100) - SAPTADutyAmount;
                double BasDutyAmtQty = (qty * BasicDutyAmount) - SAPTADutyAmountQty;
                double TotBasicDutyAmt = BCD + BasDutyAmtQty;

                //3. **************** MRP *************************************************
                string MRPSNo = txtMRPSNo.Text;
                double MRP = 0.00;
                if (txtMRP.Text != "")
                {
                    MRP = Convert.ToDouble(txtMRP.Text);
                }
                else
                {
                    MRP = 0.00;
                }
                string MRPUnit = txtMRPUnit.Text;
                double MRPAbatement = 0.00;
                if (txtMRPAbatement.Text != "")
                {
                    MRPAbatement = Convert.ToDouble(txtMRPAbatement.Text);
                }
                double MRPAmount = 0;
                string MRPDuty = "";
                if (chkMRPDuty.Checked == true)
                {
                    MRPDuty = "1";
                    MRPAmount = ((qty * MRP) - ((qty * MRP) * MRPAbatement / 100));
                }
                else
                {
                    MRPDuty = "0";
                }

                //3. **************** CVD *************************************************
                string AddlNotn = txtAddlExNotn.Text;
                string AddlSNo = txtAddlExSlNo.Text;//txtAddlExSlNo
                double AddlExRate = Convert.ToDouble(txtAddlExRate.Text);
                string AddlFlag = ddlAddlExFlag.SelectedValue;
                double AddlAmt = Convert.ToDouble(txtAddlExAmount.Text);
                string AddlUnit = txtAddlExUnit.Text;

                double CVDTax = AddlExRate;//AddlExRate
                double CVD = 0;
                if (MRPDuty == "0" || MRPAmount==0)//MRPDuty,MRPAmount
                {
                    CVD = (AssValue + TotBasicDutyAmt) * CVDTax / 100;
                }
                else
                {
                    CVD = (MRPAmount) * CVDTax / 100;
                }
                double CVDDutyAmtQty = (qty * AddlAmt);
                double TotalCVDAmt = CVD + CVDDutyAmtQty;

                //4. **************** CVD Edu Cess ****************************************
                //CVD Ex. Edu Cess And Sec Edu Cess 15.07.2014 As per Custom change
                string ExEduCessNotn = txtExEduCessNotn.Text;
                string ExEduCessSNo = txtExEduCessslNo.Text;
                double ExEduCessRate = 0.00;
                if (txtExEduCessRate.Text != "")
                {
                    ExEduCessRate = Convert.ToDouble(txtExEduCessRate.Text);
                }
                double ExEduCessAmount = TotalCVDAmt * ExEduCessRate / 100;

                //5. **************** CVD SEC Cess ****************************************
                double ExSHECessRate = 0.00;
                if (txtExSHECessRate.Text != "")
                {
                    ExSHECessRate = Convert.ToDouble(txtExSHECessRate.Text);
                }
                double ExSHECessAmount = TotalCVDAmt * ExSHECessRate / 100;

                //6. **************** Additional Duty (Sche II) ***************************
                string ExSPLExDutyNotn = txtExSPLExDutyNotn.Text;
                string ExSPLExDutySlNo = txtExSPLExDutySlNo.Text;
                double ExSPLExDutyRate = Convert.ToDouble(txtExSPLExDutyRate.Text);
                string ExSPLExDutyFlag = ddlExSPLExDutyFlag.SelectedValue;
                double ExSPLExDutyAmount = Convert.ToDouble(txtExSPLExDutyAmount.Text);
                string ExSPLExDutyUnit = txtExSPLExDutyUnit.Text;
                double AddSchIIDutyAmount = 0.00;
                AddSchIIDutyAmount = (AssValue + TotBasicDutyAmt) * ExSPLExDutyRate / 100;

                //7. **************** Custom Edu Cess  ***************************
                //BCD Edu Cess And Sec Edu Cess
                string EduCessNotn = txtEducessNotn.Text;
                string EduCessSNo = txtEduCessSNo.Text;
                double EduCessRate = 0.00;
                if (txtEducessRate.Text != "")
                {
                    EduCessRate = Convert.ToDouble(txtEducessRate.Text);
                }
                //double EduCessAmount = (BCD * EduCessRate) / 100;//TotBasicDutyAmt
                double EduCessAmount = (TotBasicDutyAmt + TotalCVDAmt + AddSchIIDutyAmount) * EduCessRate / 100;
               
                //8. **************** Custom SEC Cess ***************************
                string SHECess = txtSHECessNotn.Text;
                string SHECessSno = txtSHECessSNo.Text;
                double SHECessRate = 0.00;
                if (txtSHECessRate.Text != "")
                {
                    SHECessRate = Convert.ToDouble(txtSHECessRate.Text);
                }
                //double SHECessAmount = (BCD * SHECessRate) / 100;
                double SHECessAmount = (TotBasicDutyAmt + TotalCVDAmt + AddSchIIDutyAmount) * SHECessRate / 100;

                //8. **************** SAD (Additional Duty) ***************************
                string ExCVDNotn = txtExCVDNotn.Text;
                string ExCVDSlNo = txtExCVDSlNo.Text;
                double EXCVDRate = 0.00;
                if (txtEXCVDRate.Text != "")
                {
                    EXCVDRate = Convert.ToDouble(txtEXCVDRate.Text);
                }
                double SADAmt = ((AssValue + TotBasicDutyAmt + TotalCVDAmt +AddSchIIDutyAmount+ ExEduCessAmount + ExSHECessAmount + EduCessAmount + SHECessAmount) * EXCVDRate) / 100;//EXCVDRate

                //8. **************** CESS ***************************
                string ExCessNotn = txtExCessNotn.Text;
                string ExCessSlNo = txtExCessSlNo.Text;
                double ExCessRate = Convert.ToDouble(txtExCessRate.Text);
                double ExCessPerAmount = 0;
                string ExCessFlag = ddlExCessFlag.SelectedValue;
                double ExCessAmount = Convert.ToDouble(txtExCessAmount.Text);
                string ExCessUnit = txtExCessUnit.Text;
                double ExCessQtyAmount = qty * ExCessAmount;
                double CESSDutyAmt = ExCessPerAmount + ExCessQtyAmount;

               
                //8. **************** Additional Duty(GSI)   ***************************
                string ExGSIAddlDutyNotn = txtExGSIAddlDutyNotn.Text;
                string ExGSIAddlDutySlNo = txtExGSIAddlDutySlNo.Text;
                double ExGSIAddlDutyRate = Convert.ToDouble(txtExGSIAddlDutyRate.Text);
                string ExGSIAddlDutyFlag = ddlExGSIAddlDutyFlag.SelectedValue;
                double ExGSIAddlDutyAmount = Convert.ToDouble(txtExGSIAddlDutyAmount.Text);
                string ExGSIAddlDutyUnit = txtExGSIAddlDutyUnit.Text;


                //8. **************** Additional Duty(TTA)   ***************************
                string ExTTAAddlDutyNotn = txtExTTAAddlDutyNotn.Text;
                string ExTTAAddlDutySlNo = txtExTTAAddlDutySlNo.Text;
                double ExTTAAddlDutyRate = Convert.ToDouble(txtExTTAAddlDutyRate.Text);
                string ExTTAAddlDutyFlag = ddlExTTAAddlDutyFlag.SelectedValue;
                double ExTTAAddlDutyAmount = Convert.ToDouble(txtExTTAAddlDutyAmount.Text);
                string ExTTAAddlDutyUnit = txtExTTAAddlDutyUnit.Text;


                //8. **************** Health Cess   ***************************
                string ExHealthCessNotn = txtExHealthCessNotn.Text;
                string ExHealthCessSlNo = txtExHealthCessSlNo.Text;
                double ExHealthCessRate = Convert.ToDouble(txtExHealthCessRate.Text);
                string ExHealthCessFlag = ddlExHealthCessFlag.SelectedValue;
                double ExHealthCessAmount = Convert.ToDouble(txtExHealthCessAmount.Text);
                string ExHealthCessUnit = txtExHealthCessUnit.Text;

                //8. **************** NCD  ***************************
                string NCDNotn = txtNCDNotn.Text;
                string NCDSno = txtNCDSNo.Text;
                double NCDRate = Convert.ToDouble(txtNCDRate.Text);
                string NCDDFlag = ddlNCDFlag.SelectedValue;
                double NCDAmount = Convert.ToDouble(txtNCDAmount.Text);
                string NCDUnit = txtNCDUnit.Text;
                string NCDRule = txtNCDRule.Text;
                //No Calculation
               
                //8. **************** Surcharge  ***************************
                string SurNotn = txtSurNotn.Text;
                string SurSno = txtSurSno.Text;
                double SurRate = Convert.ToDouble(txtSurRate.Text);
                //No Calculation

                //8. **************** TarrifValue  ***************************
                string TariffValNotn = txtTarrifNotn.Text;
                string TariffValSNo = txtTraiffSno.Text;
                double TariffUnitQty = Convert.ToDouble(txtTarriffUnitQty.Text);
                string TariffUnit = txtTraiffUnit.Text;
                double TariffRate = Convert.ToDouble(txttraiffRate.Text);
                double TariffAmount = Convert.ToDouble(txttraiffAmount.Text);
                //No Calculation

                //8. **************** Total Duty Calculation  ***************************
                double TotalDutyAmt = TotBasicDutyAmt + TotalCVDAmt +AddSchIIDutyAmount+ ExEduCessAmount + ExSHECessAmount + EduCessAmount + SHECessAmount + SADAmt + CESSDutyAmt;
                lblAssableValue.Text = Convert.ToString(AssValue);
                lblDutyAmount.Text = Convert.ToString(TotalDutyAmt);
                Session["TotalDuty"] = TotalDutyAmt;
                //***********************************************************************

                int Result = obj.UpdateDuty(EximSchCode, EximSchDesc, SchNoten, SchNotenUnit, SchNotenDesc, CTHNo, txtCETNo.Text, RateType, BasicDutyNotn, BasicDutySno,
                          BasicDutyFlag, BasicDutyRate, BasicDutyUnit, AddlNotn, AddlSNo, AddlExRate, AddlFlag, AddlAmt, AddlUnit, MRPDuty, MRPSNo, MRP, MRPUnit, MRPAbatement,
                          ExCVDNotn, ExCVDSlNo, EXCVDRate, PolicyPara, PolicyYear, EduCessNotn, EduCessSNo, EduCessRate, SHECess, SHECessSno, SHECessRate,
                          Jobno, InvoiceNo, ProductDesc, BCD, EduCessAmount, SHECessAmount, CVD, SADAmt, ExEduCessAmount, ExSHECessAmount, ITCLocation, ITCHSCode, ProductID, 
                          SAPTANotn, SAPTASNo, SAPTADesc, PoNo, PoDate, CVDDutyAmtQty, BasDutyAmtQty, BasicDutyAmount, TotBasicDutyAmt, TotalCVDAmt,
                          ExCessNotn, ExCessSlNo, ExCessRate, ExCessFlag, ExCessAmount, ExCessUnit, TotalDutyAmt, ExEduCessNotn, ExEduCessSNo, ExEduCessRate, ExSHECessRate, CESSDutyAmt);

                int Result1 = obj.UpdateEXDuty(ExEduCessRate, ExSHECessRate, ExGSIAddlDutyNotn, ExGSIAddlDutySlNo, ExGSIAddlDutyRate, ExGSIAddlDutyFlag, ExGSIAddlDutyAmount,
                        ExGSIAddlDutyUnit, ExSPLExDutyNotn, ExSPLExDutySlNo, ExSPLExDutyRate, ExSPLExDutyFlag, ExSPLExDutyAmount, ExSPLExDutyUnit, ExTTAAddlDutyNotn,
                        ExTTAAddlDutySlNo, ExTTAAddlDutyRate, ExTTAAddlDutyFlag, ExTTAAddlDutyAmount, ExTTAAddlDutyUnit, ExHealthCessNotn, ExHealthCessSlNo,
                        ExHealthCessRate, ExHealthCessFlag, ExHealthCessAmount, ExHealthCessUnit,ExSADNotn, ExSADSlno, ExSADRate, AddlNotn, AddlSno, NCDNotn, 
                        NCDSno, NCDRate, NCDDFlag, NCDAmount, NCDUnit, NCDRule, SurNotn, SurSno, SurRate, SAPTANotn, SAPTASNo, SAPTADesc,
                        TariffValNotn, TariffValSNo, TariffUnitQty, TariffUnit, TariffRate, TariffAmount, Jobno, InvoiceNo, ProductDesc, ProductID);
                if (Result == 1)
                {
                    lblmsg.ForeColor = Color.Green;
                    lblmsg.Text = "Duty details saved successfully ";
                }
                else
                {
                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "alert('Please the check values again');", true);
                }
            }
            catch
            {
            
            }
        }
       
        protected void btnsaveExc_Click(object sender, EventArgs e)
        {
            try
            {
                string Jobno = ddlJobNo.SelectedValue;
                string InvoiceNo = ddlInvNo.SelectedItem.Text;
                string TransId = (string)Session["ProductID"];
                double AssValue = 0;
                double BCD = 0;
                double EduCess = 0;
                double SecEduCess = 0;

                DataSet ds = obj1.GetAssBCDCVD(TransId);
                if (ds.Tables["InvNo"].Rows.Count != 0)
                {
                    DataRowView rv = ds.Tables["InvNo"].DefaultView[0];
                    AssValue = Convert.ToDouble(rv["AssableValue"].ToString());
                    BCD = Convert.ToDouble(rv["BasDutyAmtPer"].ToString());
                    EduCess = Convert.ToDouble(rv["EduCessAmount"].ToString());
                    SecEduCess = Convert.ToDouble(rv["SHECessAmount"].ToString());
                }
                string ProductDesc = lblProduct.Text;
                //txtExSPLExDutyRate
                double ExEduCessRate = Convert.ToDouble(txtExEduCessRate.Text);
                double ExSHECessRate = Convert.ToDouble(txtExSHECessRate.Text);
                string ExGSIAddlDutyNotn = txtExGSIAddlDutyNotn.Text;
                string ExGSIAddlDutySlNo = txtExGSIAddlDutySlNo.Text;
                double ExGSIAddlDutyRate = Convert.ToDouble(txtExGSIAddlDutyRate.Text);
                string ExGSIAddlDutyFlag = ddlExGSIAddlDutyFlag.SelectedValue;
                double ExGSIAddlDutyAmount = Convert.ToDouble(txtExGSIAddlDutyAmount.Text);
                string ExGSIAddlDutyUnit = txtExGSIAddlDutyUnit.Text;
                string ExSPLExDutyNotn = txtExSPLExDutyNotn.Text;
                string ExSPLExDutySlNo = txtExSPLExDutySlNo.Text;
                double ExSPLExDutyRate = Convert.ToDouble(txtExSPLExDutyRate.Text);
                string ExSPLExDutyFlag = ddlExSPLExDutyFlag.SelectedValue;
                double ExSPLExDutyAmount = Convert.ToDouble(txtExSPLExDutyAmount.Text);
                string ExSPLExDutyUnit = txtExSPLExDutyUnit.Text;
                string ExTTAAddlDutyNotn = txtExTTAAddlDutyNotn.Text;
                string ExTTAAddlDutySlNo = txtExTTAAddlDutySlNo.Text;
                double ExTTAAddlDutyRate = Convert.ToDouble(txtExTTAAddlDutyRate.Text);
                string ExTTAAddlDutyFlag = ddlExTTAAddlDutyFlag.SelectedValue;
                double ExTTAAddlDutyAmount = Convert.ToDouble(txtExTTAAddlDutyAmount.Text);
                string ExTTAAddlDutyUnit = txtExTTAAddlDutyUnit.Text;
                string ExHealthCessNotn = txtExHealthCessNotn.Text;
                string ExHealthCessSlNo = txtExHealthCessSlNo.Text;
                double ExHealthCessRate = Convert.ToDouble(txtExHealthCessRate.Text);
                string ExHealthCessFlag = ddlExHealthCessFlag.SelectedValue;
                double ExHealthCessAmount = Convert.ToDouble(txtExHealthCessAmount.Text);
                string ExHealthCessUnit = txtExHealthCessUnit.Text;
                
                string ExSADNotn = txtExSADNotn.Text;
                string ExSADSlno = txtExSADSlno.Text;
                double ExSADRate = Convert.ToDouble(txtExSADRate.Text);

                string AddlNotn = txtAddlNotn.Text;
                string AddlSno = txtAddlNotnSno.Text;
                string NCDNotn = txtNCDNotn.Text;
                string NCDSno = txtNCDSNo.Text;
                double NCDRate = Convert.ToDouble(txtNCDRate.Text);
                string NCDDFlag = ddlNCDFlag.SelectedValue;
                double NCDAmount = Convert.ToDouble(txtNCDAmount.Text);
                string NCDUnit = txtNCDUnit.Text;
                string NCDRule = txtNCDRule.Text;
                string SurNotn = txtSurNotn.Text;
                string SurSno = txtSurSno.Text;
                double SurRate = Convert.ToDouble(txtSurRate.Text);
                string SAPTANotn = txtSAPTNotn.Text;
                string SAPTASNo = txtSAPTSno.Text;
                string SAPTADesc = txtSAPTDesc.Text;
                string TariffValNotn = txtTarrifNotn.Text;
                string TariffValSNo = txtTraiffSno.Text;
                double TariffUnitQty = Convert.ToDouble(txtTarriffUnitQty.Text);
                string TariffUnit = txtTraiffUnit.Text;
                double TariffRate = Convert.ToDouble(txttraiffRate.Text);
                double TariffAmount = Convert.ToDouble(txttraiffAmount.Text);

               //   double CVDTax = AddlExRate;//AddlExRate
                //double CVD = (AssValue + BCD) * CVDTax / 100;
                //double ExEduCessAmount = (CVD * 2) / 100;
               // double ExSHECessAmount = (CVD * 1) / 100;
                //double SADAmt = ((AssValue + BCD + CVD + ExEduCessAmount + ExSHECessAmount + EduCess + SecEduCess) * EXCVDRate) / 100;//EXCVDRate

                int Result = obj.UpdateEXDuty(ExEduCessRate,ExSHECessRate, ExGSIAddlDutyNotn, ExGSIAddlDutySlNo, ExGSIAddlDutyRate, ExGSIAddlDutyFlag, ExGSIAddlDutyAmount,
                 ExGSIAddlDutyUnit, ExSPLExDutyNotn, ExSPLExDutySlNo, ExSPLExDutyRate, ExSPLExDutyFlag, ExSPLExDutyAmount, ExSPLExDutyUnit, ExTTAAddlDutyNotn,
                 ExTTAAddlDutySlNo, ExTTAAddlDutyRate, ExTTAAddlDutyFlag, ExTTAAddlDutyAmount, ExTTAAddlDutyUnit, ExHealthCessNotn, ExHealthCessSlNo,
                 ExHealthCessRate, ExHealthCessFlag, ExHealthCessAmount, ExHealthCessUnit,
                 ExSADNotn, ExSADSlno, ExSADRate,AddlNotn,AddlSno,NCDNotn,NCDSno,NCDRate,NCDDFlag,NCDAmount,NCDUnit,NCDRule,SurNotn,SurSno,SurRate,SAPTANotn,SAPTASNo,SAPTADesc,
                 TariffValNotn, TariffValSNo, TariffUnitQty, TariffUnit, TariffRate, TariffAmount, Jobno, InvoiceNo, ProductDesc, TransId);

                if (Result == 1)
                {
                    lblmsg.ForeColor = Color.Green;
                    lblmsg.Text = "Other Duty details saved successfully ";
                    
                }
                else
                {
                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "alert('Please the check values again');", true);
                }
            }
            catch
            {
            }
        }

        protected void btnITCLicAdd_Click(object sender, EventArgs e)
        {
            try
            {
                string Jobno = ddlJobNo.SelectedValue;
                string InvoiceNo = ddlInvNo.SelectedItem.Text;
                string ProductDesc = lblProduct.Text;
                int result = obj.InsertITCLicence(txtLicenceNo.Text, txtLicenceDate.Text, txtQuantity.Text, txtDebitValue.Text, txtRANumber.Text, txtRADate.Text, txtRegPort.Text, Jobno, InvoiceNo, ProductDesc);
                BindITCLicNo();
                if (result == 1)
                {
                    lblmsg.ForeColor = Color.Green;
                    lblmsg.Text = "ITC details saved successfully ";
                    ClearITC();

                }
                else
                {
                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "alert('Please the check values again');", true);
                }
            }
            catch
            {
            }
        }

        public void BindITCLicNo()
        {
            try
            {
                string Jobno = ddlJobNo.SelectedValue;
                string InvoiceNo = ddlInvNo.SelectedItem.Text;
                string ProductDesc = lblProduct.Text;
                DataSet ds = obj.GetITCLicNo(Jobno, InvoiceNo, ProductDesc);

                if (ds.Tables["ITCLic"].Rows.Count != 0)
                {
                    gvITCLicDetails.DataSource = ds;
                    gvITCLicDetails.DataBind();
                }
                else
                {
                    gvITCLicDetails.DataBind();
                }
            }
            catch
            {
            }
        }

        public void ClearITC()
        {
            txtLicenceNo.Text ="";
            txtLicenceDate.Text ="";
            txtQuantity.Text = "";
            txtDebitValue.Text = "";
            txtRANumber.Text = "";
            txtRADate.Text = "";
            txtRegPort.Text = "";
        }

        protected void btnITCLicUpdate_Click(object sender, EventArgs e)
        {
            btnITCLicUpdate.Visible = false;
            btnITCLicAdd.Visible = true;

            int result = obj.UpdateITCLicence(txtLicenceNo.Text, txtLicenceDate.Text, txtQuantity.Text, txtDebitValue.Text, txtRANumber.Text, txtRADate.Text, txtRegPort.Text, (string)Session["ITCLicNoID"]);
            BindITCLicNo();
            if (result == 1)
            {
                lblmsg.ForeColor = Color.Green;
                lblmsg.Text = "ITC details Updated successfully ";
                ClearITC();
               
            }
            else
            {
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "alert('Please the check values again');", true);
            }
        }

        protected void gvITCLicDetails_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnITCLicUpdate.Visible = true;
            btnITCLicAdd.Visible = false;
            Session["ITCLicNoID"] = gvITCLicDetails.SelectedRow.Cells[1].Text;
            txtLicenceNo.Text = gvITCLicDetails.SelectedRow.Cells[2].Text;
            txtLicenceDate.Text = gvITCLicDetails.SelectedRow.Cells[3].Text;
            txtQuantity.Text = gvITCLicDetails.SelectedRow.Cells[4].Text;
            txtDebitValue.Text = gvITCLicDetails.SelectedRow.Cells[5].Text;
            txtRANumber.Text = gvITCLicDetails.SelectedRow.Cells[6].Text;
            txtRADate.Text = gvITCLicDetails.SelectedRow.Cells[7].Text;
            txtRegPort.Text = gvITCLicDetails.SelectedRow.Cells[8].Text;
        }

        protected void btnPrevBEDetails_Click(object sender, EventArgs e)
        {
            string Jobno = ddlJobNo.SelectedValue;
            string InvoiceNo = ddlInvNo.SelectedItem.Text;
            string ProductDesc = lblProduct.Text;
            double UnitRate = Convert.ToDouble(txtUnitRate.Text);
            int result = obj.InsertPrevBEDetails(txtPrevBENo.Text, txtPrevBEDate.Text, ddlUnitValue.SelectedValue, UnitRate, txtCustomHouse.Text, Jobno, InvoiceNo, ProductDesc);
            BindPrevBEDetails();
            if (result == 1)
            {
                lblmsg.ForeColor = Color.Green;
                lblmsg.Text = "ReImport details saved successfully ";
                ClearPreReimport();
            }
            else
            {
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "alert('Please the check values again');", true);
            }
        }

        public void BindPrevBEDetails()
        {
            string Jobno = ddlJobNo.SelectedValue;
            string InvoiceNo = ddlInvNo.SelectedItem.Text;
            string ProductDesc = lblProduct.Text;
            DataSet ds = obj.GetPrevBEDetails(Jobno, InvoiceNo, ProductDesc);
            if (ds.Tables["PrevBE"].Rows.Count != 0)
            {
                gvPrevBEDetails.DataSource = ds;
                gvPrevBEDetails.DataBind();
            }
            else
            {
                gvPrevBEDetails.DataBind();
            }
        }

        public void ClearPreReimport()
        {
            txtPrevBENo.Text ="";
            txtPrevBEDate.Text ="";
            ddlUnitValue.SelectedValue = "~Select~";
            txtUnitRate.Text = "";
            txtCustomHouse.Text = "";
        }

        protected void gvPrevBEDetails_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnPrevBEDetails.Visible = false;
            btnPrevBEUpdate.Visible = true;
            Session["PrevBEID"] = gvPrevBEDetails.SelectedRow.Cells[1].Text;
            txtPrevBENo.Text = gvPrevBEDetails.SelectedRow.Cells[2].Text;
            txtPrevBEDate.Text = gvPrevBEDetails.SelectedRow.Cells[3].Text;
            ddlUnitValue.SelectedValue = gvPrevBEDetails.SelectedRow.Cells[4].Text;
            txtUnitRate.Text = gvPrevBEDetails.SelectedRow.Cells[5].Text;
            txtCustomHouse.Text = gvPrevBEDetails.SelectedRow.Cells[6].Text;
        }

        protected void btnPrevBEUpdate_Click(object sender, EventArgs e)
        {
            btnPrevBEDetails.Visible = true;
            btnPrevBEUpdate.Visible = false;
            double UnitRate = Convert.ToDouble(txtUnitRate.Text);
            int result = obj.UpdatePrevBEDetails(txtPrevBENo.Text, txtPrevBEDate.Text, ddlUnitValue.SelectedValue, UnitRate, txtCustomHouse.Text, (string)Session["PrevBEID"]);
            BindPrevBEDetails();
            if (result == 1)
            {
                lblmsg.ForeColor = Color.Green;
                lblmsg.Text = "ReImport details Updated successfully ";
                ClearPreReimport();
            }
            else
            {
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "alert('Please the check values again');", true);
            }
        }

        protected void btnMain_Click(object sender, EventArgs e)
        {
            divProduct.Visible = false;
            btnbckProduct.Visible = true;
            Panel1.Visible = true;
            Panel2.Visible = false;
            Panel3.Visible = false;
            Panel4.Visible = false;
            Panel5.Visible = false;
            Panel6.Visible = false;
            btnNew.Enabled = false;

            //MultiView1.ActiveViewIndex = 0;
        }

        protected void btnDuty_Click(object sender, EventArgs e)
        {
            divProduct.Visible = false;
            btnbckProduct.Visible = true;
            Panel1.Visible = false;
            Panel2.Visible = true;
            Panel3.Visible = false;
            Panel4.Visible = false;
            Panel5.Visible = false;
            Panel6.Visible = false;
            btnNew.Enabled = false;
      
            //MultiView1.ActiveViewIndex = 1;
            //txtRITC.Text = (string)Session["RITCCode"];
            //txtCTH.Text = (string)Session["RITCCode"];

        }

        protected void btnITC_Click(object sender, EventArgs e)
        {
            divProduct.Visible = false;
            btnbckProduct.Visible = true;
            Panel1.Visible = false;
            Panel2.Visible = false;
            Panel3.Visible = false;
            Panel4.Visible = true;
            Panel5.Visible = false;
            Panel6.Visible = false;
            btnNew.Enabled = false;
        }


        protected void btnOtherDuty_Click(object sender, EventArgs e)
        {
            divProduct.Visible = false;
            btnbckProduct.Visible = true;
            Panel1.Visible = false;
            Panel2.Visible = false;
            Panel3.Visible = true;
            Panel4.Visible = false;
            Panel5.Visible = false;
            Panel6.Visible = false;
            btnNew.Enabled = false;
           // MultiView1.ActiveViewIndex = 2;
           // txtCETNo.Text = (string)Session["RITCCode"];
        }

        protected void btnSch_Click(object sender, EventArgs e)
        {
            divProduct.Visible = false;
            btnbckProduct.Visible = true;
            LblScheme.Text = ddltype.SelectedValue;
            txtSchemeType.Text = ddltype.SelectedValue;
            txtValueDebited.Text = Convert.ToString(Session["TotalDuty"]);
            Panel1.Visible = false;
            Panel2.Visible = false;
            Panel3.Visible = false;
            Panel4.Visible = false;
            Panel5.Visible = false;
            Panel6.Visible = true;
            btnNew.Enabled = false;

        }

        protected void btnPre_Click(object sender, EventArgs e)
        {
            divProduct.Visible = false;
            btnbckProduct.Visible = true;
            Panel1.Visible = false;
            Panel2.Visible = false;
            Panel3.Visible = false;
            Panel4.Visible = false;
            Panel5.Visible = true;
            Panel6.Visible = false;
            btnNew.Enabled = false;
        }


        protected void btnReturn_Click(object sender, EventArgs e)
        {
            //string jobno =   ddlJobNo.SelectedItem.Text;
            //string username = (string)Session["USER-NAME"];
            //string joblogret = joblog.SelectJobLog(username, jobno);
            //if (joblogret == "NoJob")
            //{
            //    joblog.UpdateJobLog(username);
            //    int jb = joblog.InsertJobLog(username, jobno);
            //    if (jb == 1)
            //    {
             Session["JobNo"]= ddlJobNo.SelectedValue;
                    Response.Redirect("frmInvoiceDetails.aspx?JobMode=Shipment");
            //    }
            //}
            //else
            //{
            //    string mess = "This Job can be used  " + joblogret;
            //    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('" + mess + "');", true);
            //}
        }

        protected void btnExit_Click(object sender, EventArgs e)
        {
            Response.Redirect("frmProductMainPage.aspx?Mode=Direct");
        }

        protected void txtamount_TextChanged(object sender, EventArgs e)
        {
            try
            {
                double amt = Convert.ToDouble(txtamount.Text) / Convert.ToDouble(txtqty.Text);
                txtunitprice.Text = Convert.ToString(amt);
                double totamt = Convert.ToDouble(txtamount.Text) * Convert.ToDouble(lblExRate.Text);
                txtINRAmount.Text = Convert.ToString(totamt);
            }
            catch
            {
            }
        }
        protected void txtunitprice_TextChanged(object sender, EventArgs e)
        {
            try
            {
                double amt = Convert.ToDouble(txtunitprice.Text) * Convert.ToDouble(txtqty.Text);
                txtamount.Text = Convert.ToString(amt);
                double totamt = amt * Convert.ToDouble(lblExRate.Text);
                txtINRAmount.Text = Convert.ToString(totamt);
            }
            catch
            {
            }
        }

        protected void GridView1_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GridViewRow row = GridView1.Rows[e.NewEditIndex];
            int id = Convert.ToInt32(row.Cells[1].Text);
            Session["ProductID"] = id;
        }

        protected void btnCheckList_Click(object sender, EventArgs e)
        {
            Session["JobNo"] = ddlJobNo.SelectedValue;
            Response.Redirect("frmPrintCheckList.aspx");
        }
       
        protected void txtRITC_TextChanged(object sender, EventArgs e)
        {
           GetProductDutyPer();
        }
        protected void txtCTH_TextChanged(object sender, EventArgs e)
        {
           // Session["RITCCode"] = txtCTH.Text;
           // productmain();
            txtRITC.Text= txtCTH.Text;
            GetProductDutyPer();
        }

        protected void txtCETNo_TextChanged(object sender, EventArgs e)
        {
            try
            {
                DataSet ds2 = obj.GetCVDRTA(txtCETNo.Text);
                if (ds2.Tables["Table"].Rows.Count != 0)
                {
                    DataRowView row2 = ds2.Tables["Table"].DefaultView[0];
                    txtAddlExRate.Text = row2["CVDRTA"].ToString();
                }
                //DataSet ds = obj.GetProductDutyPer(txtCETNo.Text);
                //if (ds.Tables["Table"].Rows.Count != 0)
                //{
                //    DataRowView row = ds.Tables["Table"].DefaultView[0];

                //    txtAddlExNotn.Text = row["CVDNotn"].ToString();
                //    //txtAddlExSlNo.Text = row["CVDDuty"].ToString();
                //    txtAddlExRate.Text = row["CVDDuty"].ToString();

                //    txtExCVDNotn.Text = row["AddlDuty_NOTN"].ToString();
                //    txtExCVDSlNo.Text = row["AddlDuty_SNO"].ToString();
                //    txtEXCVDRate.Text = row["AddlDuty_RATE"].ToString();

                //    //txtAddlExNotn.Text = row["ExCVDNotn"].ToString();
                //    //txtAddlExSlNo.Text = row["ExCVDSlNo"].ToString();
                //    //txtAddlExRate.Text = row["EXCVDRate"].ToString();

                //    //txtExCVDNotn.Text = row["AddlExNotn"].ToString();
                //    //txtExCVDSlNo.Text = row["AddlExSlNo"].ToString();
                //    //txtEXCVDRate.Text = row["AddlExRate"].ToString();

                //}
            }
            catch
            {
            }
        }
        private void gridbind()
        {
            DataSet ds = new DataSet();
            string Query = "select * from T_Schemes where JobNo='"+ddlJobNo.SelectedValue +"' And InvoiceNo='"+ddlInvNo.SelectedValue +"' And ProDesc='"+ txtpro.Text +"'";
            SqlConnection sqlConn1 = new SqlConnection(strcon);
            sqlConn1.Open();
            SqlDataAdapter da1 = new SqlDataAdapter(Query, sqlConn1);
            da1.Fill(ds, "Scheme");
            sqlConn1.Close();
            gvScheme.DataSource = ds;
            gvScheme.DataBind();
        }
        public void Schemeclear()
        {
            txtEDIRegNo.Text = "";
            txtDate.Text = "";
            txtSchemeLicNo.Text = "";
            txtItemSnoinLic.Text = "";
            txtSchemeLicDate.Text = "";
            txtSchemeType.Text = "";
            txtCifValue.Text = "";
            txtschQty.Text = "";
            txtUnit.Text = "";
            txtValueDebited.Text = "";
            txtSchemeRegPort.Text = "";
        }
        protected void btnAddScheme_Click(object sender, EventArgs e)
        {
            try
            {
                string EDIRegNo = string.Empty;
                string Date = string.Empty;
                string ItemSnoinLic = string.Empty;
                string SchemeLicNo = string.Empty;
                string SchemeLicDate = string.Empty;
                string SchemeType = string.Empty;
                string CIFValue = string.Empty;
                string Qty = string.Empty;
                string Unit = string.Empty;
                string ValueDebited = string.Empty;
                string RegPort = string.Empty;

                EDIRegNo = txtEDIRegNo.Text;
                Date = txtDate.Text;
                ItemSnoinLic = txtItemSnoinLic.Text;
                SchemeLicNo = txtSchemeLicNo.Text;
                SchemeLicDate = txtSchemeLicDate.Text;
                SchemeType = txtSchemeType.Text;
                CIFValue = txtCifValue.Text;
                Qty = txtschQty.Text;
                Unit = txtUnit.Text;
                ValueDebited = txtValueDebited.Text;
                RegPort = txtSchemeRegPort.Text;

                int result;
                string insertscheme = "Insert into [T_Schemes]([JobNo],[InvoiceNo],[ProDesc],[EDIRegNo],[EDIDate],[ItemSnoinLic],[SchemeLicNo],[SchemeLicDate],[SchemeType],[CIFValue],[Qty],[Unit],[ValueDebited],[RegPort]) " +
                    " Values('" + ddlJobNo.SelectedItem.Text + "','" + ddlInvNo.SelectedItem.Text + "','" + txtpro.Text + "','" + EDIRegNo + "','" + Date + "','" + ItemSnoinLic + "','" + SchemeLicNo + "','" + SchemeLicDate + "','" + SchemeType + "','" + CIFValue + "','" + Qty + "','" + Unit + "','" + ValueDebited + "','" + RegPort + "') ";

                SqlConnection sqlConn = new SqlConnection(strcon);
                sqlConn.Open();
                SqlCommand cmd = new SqlCommand(insertscheme, sqlConn);
                SqlDataAdapter da = new SqlDataAdapter();

                cmd.CommandText = insertscheme;
                cmd.Connection = sqlConn;
                da.SelectCommand = cmd;
                result = cmd.ExecuteNonQuery();
                sqlConn.Close();
                gridbind();
                if (result == 1)
                {
                    Schemeclear();
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "redirect", "alert('Added Successfully');", true);
                }
            }
            catch (Exception ex)
            {
            }
        }

        protected void btnDelete_Click(object sender, ImageClickEventArgs e)
        {

            ImageButton btndel = sender as ImageButton;
            GridViewRow row = (GridViewRow)btndel.NamingContainer;
            string TransId = row.Cells[3].Text;
            int i = obj.DeleteProductDetails(TransId);
            if (i == 1)
            {
                LoadGrid(ddlInvNo.SelectedItem.Text, ddlJobNo.SelectedValue);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "redirect", "alert('Deleted Successfully');", true);
            }
        }
        protected void btnCopy_Click(object sender, ImageClickEventArgs e)
        {
            clear();
            ProductType();
            Unit();
            ImageButton btncpy = sender as ImageButton;
            GridViewRow row1 = (GridViewRow)btncpy.NamingContainer;
            string TransId = row1.Cells[3].Text;
            DataSet ds = obj.GetProductDetails(TransId);
            if (ds.Tables["Table"].Rows.Count != 0)
            {
                DataRowView row = ds.Tables["Table"].DefaultView[0];
                txtpro.Text = row["ProductDesc"].ToString();
                ddltype.SelectedValue = row["ProType"].ToString();
                txtqty.Text = row["Qty"].ToString();
                ddlUnit.SelectedValue = row["Unit"].ToString();
                txtunitprice.Text = row["UnitPrice"].ToString();
                txtamount.Text = row["Amount"].ToString();
                txtINRAmount.Text = row["ProdAmtRs"].ToString();
                txtProductCode.Text = row["ProductCode"].ToString();
                txtProductFamily.Text = row["ProductFamily"].ToString();
                txtRITC.Text = row["RITCNo"].ToString();
            }

            SetProductdetails(Convert.ToInt32(TransId));
            
        }
        protected void txtJobNo_TextChanged(object sender, EventArgs e)
        {
           
            InvNo(ddlJobNo.SelectedValue);
        }
      
        protected void btnBEFile_Click(object sender, EventArgs e)
        {
            Session["JobNo"] = ddlJobNo.SelectedValue;
            Response.Redirect("frmBEFile.aspx");
            
        }
       

        protected void btnNew_Click(object sender, EventArgs e)
        {
            clear();
        }

       
        protected void gvScheme_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnAddScheme.Visible = false;
            btnUpdateScheme.Visible = true;
            Session["SchemeID"] = gvScheme.SelectedRow.Cells[1].Text;
            DataSet ds = obj.GetScheme((string)Session["SchemeID"]);
            if (ds.Tables["Table"].Rows.Count != 0)
            {
                DataRowView rv = ds.Tables["Table"].DefaultView[0];

                txtEDIRegNo.Text = rv.Row["EDIRegNo"].ToString();
                txtDate.Text = rv.Row["Date"].ToString();
                txtItemSnoinLic.Text = rv.Row["ItemSnoinLic"].ToString();
                txtSchemeLicNo.Text = rv.Row["SchemeLicNo"].ToString();
                txtSchemeLicDate.Text = rv.Row["SchemeLicDate"].ToString();
                txtSchemeType.Text = rv.Row["SchemeType"].ToString();
                txtCifValue.Text = rv.Row["CIFValue"].ToString();
                txtschQty.Text = rv.Row["Qty"].ToString();
                txtUnit.Text = rv.Row["Unit"].ToString();
                txtValueDebited.Text = rv.Row["ValueDebited"].ToString();
                txtSchemeRegPort.Text = rv.Row["RegPort"].ToString();
            }
        }

        protected void btnUpdateScheme_Click(object sender, EventArgs e)
        {
            btnAddScheme.Visible = true;
            btnUpdateScheme.Visible = false;
            int result = obj.UpdateSchemeDetails((string)Session["SchemeID"], txtEDIRegNo.Text, txtDate.Text, "", txtItemSnoinLic.Text, txtSchemeLicNo.Text, txtSchemeLicDate.Text,
                txtSchemeType.Text, txtCifValue.Text, txtschQty.Text, txtUnit.Text, txtValueDebited.Text, txtSchemeRegPort.Text);
            if (result == 1)
            {
                lblmsg.ForeColor = Color.Green;
                lblmsg.Text = "Scheme details Updated successfully ";
                gridbind();
                Schemeclear();
            }
            else
            {
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "alert('Please the check values again');", true);
            }
        }

        protected void txtpro_TextChanged(object sender, EventArgs e)
        {
            try
            {
                DataSet ds = obj.GetProductMaster(txtpro.Text);
                if (ds.Tables["product"].Rows.Count != 0)
                {
                    DataRowView row = ds.Tables["product"].DefaultView[0];
                    txtProductCode.Text = row["ProductCode"].ToString();
                    txtRITC.Text = row["RITCNo"].ToString();
                    txtCETNo.Text = row["RITCNo"].ToString();
                    txtCTH.Text = row["RITCNo"].ToString();
                }
                GetProductDutyPer();
            }
            catch
            {
            }
        }

        protected void txtProductCode_TextChanged(object sender, EventArgs e)
        {
            try
            {
                DataSet ds = obj.GetProductMasterCode(txtProductCode.Text);
                if (ds.Tables["product"].Rows.Count != 0)
                {
                    DataRowView row = ds.Tables["product"].DefaultView[0];
                    txtpro.Text = row["ProductDesc"].ToString();
                    txtRITC.Text = row["RITCNo"].ToString();
                    txtCETNo.Text = row["RITCNo"].ToString();
                    txtCTH.Text = row["RITCNo"].ToString();
                }
            }
            catch
            {
            }
        }

        public double  OtherCharges(string JobNo,string InvoiceNo)
        {
            double OthCharges = 0;
            try
            {
                SqlConnection conn = new SqlConnection(strcon);
                conn.Open();
               // SELECT DISTINCT JobNo, AmountINR FROM            View_InvoiceOtherCharges
                string qry = "select Sum(AmountINR) as AmountINR from T_InvoiceOtherCharges where JobNo = '" + JobNo + "' And InvoiceNo='" + InvoiceNo + "'";
                SqlDataAdapter da = new SqlDataAdapter(qry, conn);
                DataSet ds = new DataSet();
                da.Fill(ds, "DATA");
                if (ds.Tables["DATA"].Rows.Count != 0)
                {
                    DataRowView row1 = ds.Tables["DATA"].DefaultView[0];
                    if (row1["AmountINR"] != DBNull.Value)
                    {
                        OthCharges = Convert.ToDouble(row1["AmountINR"].ToString());
                    }
                    else
                    {
                        OthCharges = 0.00;
                    }
                }
                conn.Close();
            }
            catch
            {
            }
            return OthCharges;
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                double invamt = Convert.ToDouble(lblInvAmt.Text);
                double freight = Convert.ToDouble(lblFrie.Text);
                double insu = Convert.ToDouble(lblIns.Text);
                double OthCharges = OtherCharges(ddlJobNo.SelectedValue, ddlInvNo.SelectedValue);
                double misc = Convert.ToDouble(lblMisc.Text) + OthCharges;
                double agency = Convert.ToDouble(lblAgen.Text);

                double qty = Convert.ToDouble(txtqty.Text);
                double unitprice = Convert.ToDouble(txtunitprice.Text);
                double amt = Convert.ToDouble(txtamount.Text);

                string ProductID = (string)Session["ProductID"];
                string RitcNo = txtRITC.Text; ;//(string)Session["RITCCode"];
                double Exrate = Convert.ToDouble(lblExRate.Text);
                double ProdValue = amt * Exrate;
                //double airfright = ProdValue * 20 / 100;

                double FreightAmount = (freight / invamt) * ProdValue;
                double insuAmount = (insu / invamt) * ProdValue;
                double miscAmount = (misc / invamt) * ProdValue;
                double agencyAmount = (agency / invamt) * ProdValue;

                //FreightAmount = FreightAmount + miscAmount;
                double airfright = (ProdValue + miscAmount) * 20 / 100;

                double FreightAmount1 = FreightAmount / Exrate;
                double insuAmount1 = insuAmount / Exrate;
                double miscAmount1 = miscAmount / Exrate;
                double agencyAmount1 = agencyAmount / Exrate;

                double AddlChrg = Convert.ToDouble(lblAdlChrg.Text);
                double AddlChrgHS = (AddlChrg / invamt) * ProdValue;
                double AddlChrgHS1 = AddlChrgHS / Exrate;
                double totamt = 0;


                if (lblMode.Text == "Air")
                {
                    if (airfright >= FreightAmount)
                    {
                        totamt = ProdValue + FreightAmount + insuAmount + miscAmount + agencyAmount + AddlChrgHS;
                    }
                    else
                    {
                        totamt = ProdValue + airfright + insuAmount + miscAmount + agencyAmount + AddlChrgHS;
                    }
                }
                else
                {
                    totamt = ProdValue + FreightAmount + insuAmount + miscAmount + agencyAmount + AddlChrgHS;
                }

                //double totamt = 0;

                //if (lblMode.Text == "Air")
                //{
                //    if (airfright >= FreightAmount)
                //    {
                //        totamt = ProdValue + FreightAmount + insuAmount + miscAmount + agencyAmount;
                //    }
                //    else
                //    {
                //        totamt = ProdValue + airfright + insuAmount + miscAmount + agencyAmount;
                //    }
                //}
                //else
                //{
                //    totamt = ProdValue + FreightAmount + insuAmount + miscAmount + agencyAmount;
                //}
                double loadingcharge = totamt / 100;
                double assvalue = totamt + loadingcharge;
                Session["AssValue"] = assvalue;
              //  row["ProType"].ToString();
                string ModifiedBy = (string)Session["USER-NAME"];
                string ModifiedDate = Convert.ToString(DateTime.Now);

                Session["SchemeName"] = ddltype.SelectedValue;
                if ((string)Session["SchemeName"] != "DUTIABLE")
                {
                    btnSch.Text = (string)Session["SchemeName"];
                    btnSch.Visible = true;
                }
                else
                {
                    btnSch.Text = "Scheme";
                    btnSch.Visible = false;
                }
                int slno = Convert.ToInt16((string)Session["slno"]);
                int result = obj.UpdateProductDetails(ProductID, txtProductFamily.Text, txtProductCode.Text, txtpro.Text, ddltype.SelectedValue, qty, ddlUnit.SelectedValue, unitprice, amt, RitcNo, ProdValue, FreightAmount1, insuAmount1, miscAmount1, agencyAmount1, loadingcharge, assvalue, slno);
                LoadGrid(ddlInvNo.SelectedValue, ddlJobNo.SelectedValue);
                footertotrow();
                btnsaveCustom_Click(sender, e);
                btnsaveExc_Click(sender, e);
                lblmsg.ForeColor = Color.Green;
                lblmsg.Text = "Successfully Saved Product : " + ProductID;
            }
            catch (Exception ex)
            {
            }
        }

        protected void btnbckProduct_Click(object sender, EventArgs e)
        {
            divProduct.Visible = true;
            btnbckProduct.Visible = false;
            Panel1.Visible = false;
            Panel2.Visible = false;
            Panel3.Visible = false;
            Panel4.Visible = false;
            Panel5.Visible = false;
            Panel6.Visible = false;
            btnNew.Enabled = true;
        }

        protected void btnUpdateProduct_Click(object sender, EventArgs e)
        {
            try
            {
                DataSet ds = obj1.CheckProduct(txtRITC.Text);
                if (ds.Tables["Table"].Rows.Count != 0)
                {
                    UpdateProduct();
                }
                else
                {
                    SaveProduct();
                }
            }
            catch
            {
            }
        }

        private void SaveProduct()
        {
            int Result = 0;
            string CreatedBy = (string)Session["USER-NAME"];
            string CreatedDate = DateTime.Now.ToString();

            StringBuilder Query = new StringBuilder();

            string Message = string.Empty;
            try
            {
                using (SqlConnection con = new SqlConnection(strcon))
                {
                    
                    con.Open();
                    Query.Append("INSERT INTO [M_Product] (ProductCode,ProductDesc,RITCNo,GEN_DESC,ACCESSORY,MANUFACTURER,END_USE,BRAND,MODEL,CNTRY_ORIG,");
                    Query.Append("CTHNo,CETNo,BASDuty,BASSNo,BASNotn,BASDFlag,BASAmt,BASUnit,AddlDuty_NOTN,AddlDuty_SNO,AddlDuty_RATE,");
                    Query.Append("MRPSNo,MRP,MRPUnit,Abatement,CVDNotn,CVDSNo,CVDAmt,POLICYPARA,POLICY_YR,");
                    Query.Append("EDU_CESS_NOTN,EDU_CESS_SNO,EDU_CESS_RATE,SHE_CESS_NOTN,SHE_CESS_SNO,SHE_CESS_RATE,");
                    Query.Append("EDU_CESS_RATE_EXC,SHE_CESS_RATE_EXC,");
                    Query.Append("HLTH_Notn,HLTH_SNo,HLTH_Rate,HLTH_DFlag,HLTH_Amt,HLTH_Unit,");
                    Query.Append("CESS_NOTN,CESS_SNO,CESS_DUTY,CESS_DFLAG,CESS_AMT,CESS_UNIT,");
                    Query.Append("NCD_Notn,NCD_SNo,NCD_Rate,NCD_DFlag,NCD_Amt,NCD_Unit,");
                    Query.Append("SUR_NOTN,SUR_SNO,SURCHARGE,SAPTA_Notn,SAPTA_SNo,Createdby,CreatedDate");

                    Query.Append("values(@ProductCode,@ProductDesc,@RITCNo,@GEN_DESC,@ACCESSORY,@MANUFACTURER,@END_USE,@BRAND,@MODEL,@CNTRY_ORIG,");
                    Query.Append("@CTHNo,@CETNo,@BASDuty,@BASSNo,@BASNotn,@BASDFlag,@BASAmt,@BASUnit,@AddlDuty_NOTN,@AddlDuty_SNO,@AddlDuty_RATE,");
                    Query.Append("@MRPSNo,@MRP,@MRPUnit,@Abatement,@CVDNotn,@CVDSNo,@CVDAmt,@POLICYPARA,@POLICY_YR,");
                    Query.Append("@EDU_CESS_NOTN,@EDU_CESS_SNO,@EDU_CESS_RATE,@SHE_CESS_NOTN,@SHE_CESS_SNO,@SHE_CESS_RATE,");
                    Query.Append("@EDU_CESS_RATE_EXC,@SHE_CESS_RATE_EXC,");
                    Query.Append("@HLTH_Notn,@HLTH_SNo,@HLTH_Rate,@HLTH_DFlag,@HLTH_Amt,@HLTH_Unit,");
                    Query.Append("@CESS_NOTN,@CESS_SNO,@CESS_DUTY,@CESS_DFLAG,@CESS_AMT,@CESS_UNIT,");
                    Query.Append("@NCD_Notn,@NCD_SNo,@NCD_Rate,@NCD_DFlag,@NCD_Amt,@NCD_Unit,");
                    Query.Append("@SUR_NOTN,@SUR_SNO,@SURCHARGE,@SAPTA_Notn,@SAPTA_SNo,@Createdby,@CreatedDate)");

                   
                    SqlCommand cmd = new SqlCommand(Query.ToString(), con);
                    //Main
                    cmd.Parameters.AddWithValue("@ProductCode", txtProductCode.Text);
                    cmd.Parameters.AddWithValue("@ProductDesc", txtpro.Text);
                    cmd.Parameters.AddWithValue("@RITCNo", txtRITC.Text);
                    //General
                    cmd.Parameters.AddWithValue("@GEN_DESC", txtgenericdesc.Text);
                    cmd.Parameters.AddWithValue("@ACCESSORY", txtaccessories.Text);
                    cmd.Parameters.AddWithValue("@MANUFACTURER", txtmanufacturer.Text);
                    cmd.Parameters.AddWithValue("@END_USE", endcase.Text);
                    cmd.Parameters.AddWithValue("@BRAND", brand.Text);
                    cmd.Parameters.AddWithValue("@MODEL", txtmodel.Text);
                    cmd.Parameters.AddWithValue("@CNTRY_ORIG", ddlcountryorigin.SelectedValue);
                    //Duty Calculation

                    cmd.Parameters.AddWithValue("@CTHNo", txtCTH.Text);
                    cmd.Parameters.AddWithValue("@CETNo", txtCETNo.Text);
                    //Basci Duty
                    cmd.Parameters.AddWithValue("@BASDuty", txtBasicDutyRate.Text);
                    cmd.Parameters.AddWithValue("@BASSNo", txtBasicDutySno.Text);
                    cmd.Parameters.AddWithValue("@BASNotn", txtBasicDutyNotn.Text);
                    cmd.Parameters.AddWithValue("@BASDFlag", ddlBasicDutyFlag.SelectedValue);
                    cmd.Parameters.AddWithValue("@BASAmt", txtBasicDutyAmount.Text);
                    cmd.Parameters.AddWithValue("@BASUnit", txtBasicDutyUnit.Text);
                    //Addl Duty(Exsise Duty)-
                    cmd.Parameters.AddWithValue("@AddlDuty_NOTN", txtAddlExNotn.Text);
                    cmd.Parameters.AddWithValue("@AddlDuty_SNO", txtAddlExSlNo.Text);
                    cmd.Parameters.AddWithValue("@AddlDuty_RATE", txtAddlExRate.Text);
                    //cmd.Parameters.AddWithValue("@AddlDuty_NOTN", txtAddlExFlag.Text);
                    //cmd.Parameters.AddWithValue("@AddlDuty_NOTN", txtBasicDutyUnit.Text);
                    //cmd.Parameters.AddWithValue("@AddlDuty_NOTN", txtBasicDutyUnit.Text);
                    //MRP Duty 
                    cmd.Parameters.AddWithValue("@MRPSNo", txtMRPSNo.Text);
                    cmd.Parameters.AddWithValue("@MRP", txtMRP.Text);
                    cmd.Parameters.AddWithValue("@MRPUnit", txtMRPUnit.Text);
                    cmd.Parameters.AddWithValue("@Abatement", txtMRPAbatement.Text);
                    //CVD(Sub section-5)-
                    cmd.Parameters.AddWithValue("@CVDNotn", txtExCVDNotn.Text);
                    cmd.Parameters.AddWithValue("@CVDSNo", txtExCVDSlNo.Text);
                    cmd.Parameters.AddWithValue("@CVDAmt", txtEXCVDRate.Text);
                    cmd.Parameters.AddWithValue("@POLICYPARA", txtpolicy.Text);
                    cmd.Parameters.AddWithValue("@POLICY_YR", txtpyear.Text);
                    //Education Cess-
                    cmd.Parameters.AddWithValue("@EDU_CESS_NOTN", txtEducessNotn.Text);
                    cmd.Parameters.AddWithValue("@EDU_CESS_SNO", txtEduCessSNo.Text);
                    cmd.Parameters.AddWithValue("@EDU_CESS_RATE", txtEducessRate.Text);
                    cmd.Parameters.AddWithValue("@SHE_CESS_NOTN", txtSHECessNotn.Text);
                    cmd.Parameters.AddWithValue("@SHE_CESS_SNO", txtSHECessSNo.Text);
                    cmd.Parameters.AddWithValue("@SHE_CESS_RATE", txtSHECessRate.Text);

                    //Other Duty

                    //Educational Cess-
                    cmd.Parameters.AddWithValue("@EDU_CESS_RATE_EXC", txtExEduCessRate.Text);
                    cmd.Parameters.AddWithValue("@SHE_CESS_RATE_EXC", txtExSHECessRate.Text);

                    //Addl Duty of Excice(GSI).

                    //Spl.Excise Duty(sched-II)

                    //Addl Excise Duty(TTA)

                    //Health Cess
                    cmd.Parameters.AddWithValue("@HLTH_Notn", txtExHealthCessNotn.Text);
                    cmd.Parameters.AddWithValue("@HLTH_SNo", txtExHealthCessSlNo.Text);
                    cmd.Parameters.AddWithValue("@HLTH_Rate", txtExHealthCessRate.Text);
                    cmd.Parameters.AddWithValue("@HLTH_DFlag", ddlExHealthCessFlag.SelectedValue);
                    cmd.Parameters.AddWithValue("@HLTH_Amt", txtExHealthCessAmount.Text);
                    cmd.Parameters.AddWithValue("@HLTH_Unit", txtExHealthCessUnit.Text);
                    //Cess & Notn
                    cmd.Parameters.AddWithValue("@CESS_NOTN", txtExCessNotn.Text);
                    cmd.Parameters.AddWithValue("@CESS_SNO", txtExCessSlNo.Text);
                    cmd.Parameters.AddWithValue("@CESS_DUTY", txtExCessRate.Text);
                    cmd.Parameters.AddWithValue("@CESS_DFLAG", ddlExCessFlag.SelectedValue);
                    cmd.Parameters.AddWithValue("@CESS_AMT", txtExCessAmount.Text);
                    cmd.Parameters.AddWithValue("@CESS_UNIT", txtExCessUnit.Text);
                    //SAD Notn. & Duty

                    //Addl Notn

                    //NCD
                    cmd.Parameters.AddWithValue("@NCD_Notn", txtNCDNotn.Text);
                    cmd.Parameters.AddWithValue("@NCD_SNo", txtNCDSNo.Text);
                    cmd.Parameters.AddWithValue("@NCD_Rate", txtNCDRate.Text);
                    cmd.Parameters.AddWithValue("@NCD_DFlag", ddlNCDFlag.SelectedValue);
                    cmd.Parameters.AddWithValue("@NCD_Amt", txtNCDAmount.Text);
                    cmd.Parameters.AddWithValue("@NCD_Unit", txtNCDUnit.Text);
                    //Surcharge & Notn
                    cmd.Parameters.AddWithValue("@SUR_NOTN", txtSurNotn.Text);
                    cmd.Parameters.AddWithValue("@SUR_SNO", txtSurSno.Text);
                    cmd.Parameters.AddWithValue("@SURCHARGE", txtSurRate.Text);
                    //SAPTA Notn
                    cmd.Parameters.AddWithValue("@SAPTA_Notn", txtSAPTNotn.Text);
                    cmd.Parameters.AddWithValue("@SAPTA_SNo", txtSAPTSno.Text);
                    //cmd.Parameters.AddWithValue("@SAPTA_Desc", txtSAPTDesc.Text);

                    //Tarrif Value Notn
                    cmd.Parameters.AddWithValue("@CreatedBy", CreatedBy);
                    cmd.Parameters.AddWithValue("@CreatedDate", CreatedDate);
                    Result = cmd.ExecuteNonQuery();
                    con.Close();
                }
                if (Result == 1)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "redirect", "alert('Saved Successfully');", true);
                }
            }
            catch (SqlException ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert(' " + ex.Message + " ');", true);
            }
        }

        private void UpdateProduct()
        {
            int Result = 0;
            string ModifiedBy = (string)Session["USER-NAME"];
            string ModifiedDate = DateTime.Now.ToString();

            StringBuilder Query = new StringBuilder();

            string Message = string.Empty;
            try
            {
                using (SqlConnection con = new SqlConnection(strcon))
                {
                    con.Open();
                    Query.Append("Update [M_Product] SET ProductCode=@ProductCode,GEN_DESC=@GEN_DESC,ACCESSORY=@ACCESSORY,MANUFACTURER=@MANUFACTURER,END_USE=@END_USE,BRAND=@BRAND,MODEL=@MODEL,CNTRY_ORIG=@CNTRY_ORIG,");
                    Query.Append("CTHNo=@CTHNo,CETNo=@CETNo,BASDuty=@BASDuty,BASSNo=@BASSNo,BASNotn=@BASNotn,BASDFlag=@BASDFlag,BASAmt=@BASAmt,BASUnit=@BASUnit,AddlDuty_NOTN=@AddlDuty_NOTN,AddlDuty_SNO=@AddlDuty_SNO,AddlDuty_RATE=@AddlDuty_RATE,");
                    Query.Append("MRPSNo=@MRPSNo,MRP=@MRP,MRPUnit=@MRPUnit,Abatement=@Abatement,CVDNotn=@CVDNotn,CVDSNo=@CVDSNo,CVDAmt=@CVDAmt,POLICYPARA=@POLICYPARA,POLICY_YR=@POLICY_YR,");
                    Query.Append("EDU_CESS_NOTN=@EDU_CESS_NOTN,EDU_CESS_SNO=@EDU_CESS_SNO,EDU_CESS_RATE=@EDU_CESS_RATE,SHE_CESS_NOTN=@SHE_CESS_NOTN,SHE_CESS_SNO=@SHE_CESS_SNO,SHE_CESS_RATE=@SHE_CESS_RATE,");
                    Query.Append("EDU_CESS_RATE_EXC=@EDU_CESS_RATE_EXC,SHE_CESS_RATE_EXC=@SHE_CESS_RATE_EXC,");
                    Query.Append("HLTH_Notn=@HLTH_Notn,HLTH_SNo=@HLTH_SNo,HLTH_Rate=@HLTH_Rate,HLTH_DFlag=@HLTH_DFlag,HLTH_Amt=@HLTH_Amt,HLTH_Unit=@HLTH_Unit,");
                    Query.Append("CESS_NOTN=@CESS_NOTN,CESS_SNO=@CESS_SNO,CESS_DUTY=@CESS_DUTY,CESS_DFLAG=@CESS_DFLAG,CESS_AMT=@CESS_AMT,CESS_UNIT=@CESS_UNIT,");
                    Query.Append("NCD_Notn=@NCD_Notn,NCD_SNo=@NCD_SNo,NCD_Rate=@NCD_Rate,NCD_DFlag=@NCD_DFlag,NCD_Amt=@NCD_Amt,NCD_Unit=@NCD_Unit,");
                    Query.Append("SUR_NOTN=@SUR_NOTN,SUR_SNO=@SUR_SNO,SURCHARGE=@SURCHARGE,SAPTA_Notn=@SAPTA_Notn,SAPTA_SNo=@SAPTA_SNo,ModifiedBy=@ModifiedBy,ModifiedDate=@ModifiedDate where RITCNo='" + txtpro.Text + "'");

                    SqlCommand cmd = new SqlCommand(Query.ToString(), con);
                    //Main
                    cmd.Parameters.AddWithValue("@ProductCode", txtProductCode.Text);
                    cmd.Parameters.AddWithValue("@ProductDesc", txtpro.Text);
                    cmd.Parameters.AddWithValue("@RITCNo", txtpro.Text);
                    //General
                    cmd.Parameters.AddWithValue("@GEN_DESC", txtgenericdesc.Text);
                    cmd.Parameters.AddWithValue("@ACCESSORY", txtaccessories.Text);
                    cmd.Parameters.AddWithValue("@MANUFACTURER", txtmanufacturer.Text);
                    cmd.Parameters.AddWithValue("@END_USE", endcase.Text);
                    cmd.Parameters.AddWithValue("@BRAND", brand.Text);
                    cmd.Parameters.AddWithValue("@MODEL", txtmodel.Text);
                    cmd.Parameters.AddWithValue("@CNTRY_ORIG", ddlcountryorigin.SelectedValue);
                    //Duty Calculation

                    cmd.Parameters.AddWithValue("@CTHNo", txtCTH.Text);
                    cmd.Parameters.AddWithValue("@CETNo", txtCETNo.Text);
                    //Basci Duty
                    cmd.Parameters.AddWithValue("@BASDuty", txtBasicDutyRate.Text);
                    cmd.Parameters.AddWithValue("@BASSNo", txtBasicDutySno.Text);
                    cmd.Parameters.AddWithValue("@BASNotn", txtBasicDutyNotn.Text);
                    cmd.Parameters.AddWithValue("@BASDFlag", ddlBasicDutyFlag.SelectedValue);
                    cmd.Parameters.AddWithValue("@BASAmt", txtBasicDutyAmount.Text);
                    cmd.Parameters.AddWithValue("@BASUnit", txtBasicDutyUnit.Text);
                    //Addl Duty(Exsise Duty)-
                    cmd.Parameters.AddWithValue("@AddlDuty_NOTN", txtAddlExNotn.Text);
                    cmd.Parameters.AddWithValue("@AddlDuty_SNO", txtAddlExSlNo.Text);
                    cmd.Parameters.AddWithValue("@AddlDuty_RATE", txtAddlExRate.Text);
                    //cmd.Parameters.AddWithValue("@AddlDuty_NOTN", txtAddlExFlag.Text);
                    //cmd.Parameters.AddWithValue("@AddlDuty_NOTN", txtBasicDutyUnit.Text);
                    //cmd.Parameters.AddWithValue("@AddlDuty_NOTN", txtBasicDutyUnit.Text);
                    //MRP Duty 
                    cmd.Parameters.AddWithValue("@MRPSNo", txtMRPSNo.Text);
                    cmd.Parameters.AddWithValue("@MRP", txtMRP.Text);
                    cmd.Parameters.AddWithValue("@MRPUnit", txtMRPUnit.Text);
                    cmd.Parameters.AddWithValue("@Abatement", txtMRPAbatement.Text);
                    //CVD(Sub section-5)-
                    cmd.Parameters.AddWithValue("@CVDNotn", txtExCVDNotn.Text);
                    cmd.Parameters.AddWithValue("@CVDSNo", txtExCVDSlNo.Text);
                    cmd.Parameters.AddWithValue("@CVDAmt", txtEXCVDRate.Text);
                    cmd.Parameters.AddWithValue("@POLICYPARA", txtpolicy.Text);
                    cmd.Parameters.AddWithValue("@POLICY_YR", txtpyear.Text);
                    //Education Cess-
                    cmd.Parameters.AddWithValue("@EDU_CESS_NOTN", txtEducessNotn.Text);
                    cmd.Parameters.AddWithValue("@EDU_CESS_SNO", txtEduCessSNo.Text);
                    cmd.Parameters.AddWithValue("@EDU_CESS_RATE", txtEducessRate.Text);
                    cmd.Parameters.AddWithValue("@SHE_CESS_NOTN", txtSHECessNotn.Text);
                    cmd.Parameters.AddWithValue("@SHE_CESS_SNO", txtSHECessSNo.Text);
                    cmd.Parameters.AddWithValue("@SHE_CESS_RATE", txtSHECessRate.Text);

                    //Other Duty

                    //Educational Cess-
                    cmd.Parameters.AddWithValue("@EDU_CESS_RATE_EXC", txtExEduCessRate.Text);
                    cmd.Parameters.AddWithValue("@SHE_CESS_RATE_EXC", txtExSHECessRate.Text);

                    //Addl Duty of Excice(GSI).

                    //Spl.Excise Duty(sched-II)

                    //Addl Excise Duty(TTA)

                    //Health Cess
                    cmd.Parameters.AddWithValue("@HLTH_Notn", txtExHealthCessNotn.Text);
                    cmd.Parameters.AddWithValue("@HLTH_SNo", txtExHealthCessSlNo.Text);
                    cmd.Parameters.AddWithValue("@HLTH_Rate", txtExHealthCessRate.Text);
                    cmd.Parameters.AddWithValue("@HLTH_DFlag", ddlExHealthCessFlag.SelectedValue);
                    cmd.Parameters.AddWithValue("@HLTH_Amt", txtExHealthCessAmount.Text);
                    cmd.Parameters.AddWithValue("@HLTH_Unit", txtExHealthCessUnit.Text);
                    //Cess & Notn
                    cmd.Parameters.AddWithValue("@CESS_NOTN", txtExCessNotn.Text);
                    cmd.Parameters.AddWithValue("@CESS_SNO", txtExCessSlNo.Text);
                    cmd.Parameters.AddWithValue("@CESS_DUTY", txtExCessRate.Text);
                    cmd.Parameters.AddWithValue("@CESS_DFLAG", ddlExCessFlag.SelectedValue);
                    cmd.Parameters.AddWithValue("@CESS_AMT", txtExCessAmount.Text);
                    cmd.Parameters.AddWithValue("@CESS_UNIT", txtExCessUnit.Text);
                    //SAD Notn. & Duty

                    //Addl Notn

                    //NCD
                    cmd.Parameters.AddWithValue("@NCD_Notn", txtNCDNotn.Text);
                    cmd.Parameters.AddWithValue("@NCD_SNo", txtNCDSNo.Text);
                    cmd.Parameters.AddWithValue("@NCD_Rate", txtNCDRate.Text);
                    cmd.Parameters.AddWithValue("@NCD_DFlag", ddlNCDFlag.SelectedValue);
                    cmd.Parameters.AddWithValue("@NCD_Amt", txtNCDAmount.Text);
                    cmd.Parameters.AddWithValue("@NCD_Unit", txtNCDUnit.Text);
                    //Surcharge & Notn
                    cmd.Parameters.AddWithValue("@SUR_NOTN", txtSurNotn.Text);
                    cmd.Parameters.AddWithValue("@SUR_SNO", txtSurSno.Text);
                    cmd.Parameters.AddWithValue("@SURCHARGE", txtSurRate.Text);
                    //SAPTA Notn
                    cmd.Parameters.AddWithValue("@SAPTA_Notn", txtSAPTNotn.Text);
                    cmd.Parameters.AddWithValue("@SAPTA_SNo", txtSAPTSno.Text);
                    //cmd.Parameters.AddWithValue("@SAPTA_Desc", txtSAPTDesc.Text);

                    //Tarrif Value Notn

                    cmd.Parameters.AddWithValue("@ModifiedBy", ModifiedBy);
                    cmd.Parameters.AddWithValue("@ModifiedDate", ModifiedDate);
                    Result = cmd.ExecuteNonQuery();
                    con.Close();
                }
                if (Result == 1)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "redirect", "alert('Saved Successfully');", true);
                }
            }
            catch (SqlException ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert(' " + ex.Message + " ');", true);
            }
        }

        

              
    }
}