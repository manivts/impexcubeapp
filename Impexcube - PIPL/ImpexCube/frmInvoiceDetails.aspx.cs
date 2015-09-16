using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Drawing;

namespace ImpexCube
{
    public partial class frmInvoiceDetails : System.Web.UI.Page
    {
        VTS.ImpexCube.Business.InvoiceDetailsBL invBL = new VTS.ImpexCube.Business.InvoiceDetailsBL();
        VTS.ImpexCube.Business.CommonBL obj1 = new VTS.ImpexCube.Business.CommonBL();
        VTS.ImpexCube.Utlities.Utility joblog = new VTS.ImpexCube.Utlities.Utility();

        protected void Page_Load(object sender, EventArgs e)
        {
            Label pagename;
            pagename = (Label)Master.FindControl("lblName");
            pagename.Text = "Invoice";
            if (IsPostBack == false)
            {
               // Mode=Shipment
                hdnEditValue.Value = "0";
                if (Request.QueryString["Mode"] == "Direct")
                {
                    BindCurrency();
                    DropJobNo();
                    BindCountry();
                    //BindGridInvoice();
                }
                else if (Request.QueryString["JobMode"] == "Shipment")
                {
                   
                    DropJobNo();
                    ddlJobNo.SelectedValue = (string)Session["JobNo"];
                    BindCurrency();
                    BindCountry();
                    GetJobDetails();
                    BindGridInvoice();
                }
                else
                    Response.Redirect("frmLogin.aspx");
            }
        }

        private void DropJobNo()
        {
            DataSet ds = invBL.GetJobNo();
            if (ds.Tables["jobno"].Rows.Count != 0)
            {
                ddlJobNo.DataSource = ds;
                ddlJobNo.DataTextField = "JobNo";
                ddlJobNo.DataValueField = "JobNo";
                ddlJobNo.DataBind();
               // ddlJobNo.Items.Insert(0, new ListItem("-Select-", "0"));

            }
            else
            {
                ddlJobNo.DataSource = null;
                ddlJobNo.DataBind();
            }
        }

        private void BindCountry()
        {
            DataSet ds = invBL.GetCountryDetails();
            if (ds.Tables["Country"].Rows.Count != 0)
            {
                ddlConsignorCountry.DataSource = ds;
                ddlConsignorCountry.DataTextField = "CountryName";
                ddlConsignorCountry.DataValueField = "CountryName";
                ddlConsignorCountry.DataBind();

                ddlSellerCountry.DataSource = ds;
                ddlSellerCountry.DataTextField = "CountryName";
                ddlSellerCountry.DataValueField = "CountryName";
                ddlSellerCountry.DataBind();

                ddlBrokerCountry.DataSource = ds;
                ddlBrokerCountry.DataTextField = "CountryName";
                ddlBrokerCountry.DataValueField = "CountryName";
                ddlBrokerCountry.DataBind();
                // ddlJobNo.Items.Insert(0, new ListItem("-Select-", "0"));

            }
            else
            {
                ddlConsignorCountry.DataSource = null;
                ddlConsignorCountry.DataBind();

                ddlSellerCountry.DataSource = null;
                ddlSellerCountry.DataBind();

                ddlBrokerCountry.DataSource = null;
                ddlBrokerCountry.DataBind();
            }
        }

        private void BindGridInvoice()
        {
            DataSet ds = invBL.GetInvoiceDetails(ddlJobNo.SelectedValue);

            if (ds.Tables["Invoice"].Rows.Count != 0)
            {
                gvInvoiceDetails.DataSource = ds;
                gvInvoiceDetails.DataBind();
                gvInvoiceDetails.Visible = true;
                footertotrow();

                btnAddInvoice.Visible = false;
                btnUpdateInvoice.Visible = false;
               // ClearInvoiceDetails();
                ClearFreightDetails();
                ClearOtherCharges();
               // ClearConsignor();
                ClearRelation();
                ClearOtherDetails();
                ClearMiscellaneous();
            }
            else
            {
                gvInvoiceDetails.DataSource = null;
                gvInvoiceDetails.DataBind();

                ClearInvoiceDetails();
                ClearFreightDetails();
                ClearOtherCharges();
               // ClearConsignor();
                ClearRelation();
                ClearOtherDetails();
                ClearMiscellaneous();
            }
        }

        public void BindCurrency()
        {
            DataSet ds = invBL.GetCurrencyDetails();
            DataSet dsChargeType = new DataSet();
            dsChargeType = invBL.GetChargeType();
            if (ds.Tables["Invoice"].Rows.Count != 0)
            {
                ddlInvoiceCurrency.DataSource = ds;
                ddlInvoiceCurrency.DataTextField = "CurrencyShortName";
                ddlInvoiceCurrency.DataValueField = "CurrencyShortName";
                ddlInvoiceCurrency.DataBind();

                ddlFreightDetails.DataSource = ds;
                ddlFreightDetails.DataTextField = "CurrencyShortName";
                ddlFreightDetails.DataValueField = "CurrencyShortName";
                ddlFreightDetails.DataBind();
                //ddlFreightDetails.Items.Insert(0, new ListItem("~Select~", "0"));

                ddlInsurance.DataSource = ds;
                ddlInsurance.DataTextField = "CurrencyShortName";
                ddlInsurance.DataValueField = "CurrencyShortName";
                ddlInsurance.DataBind();
               // ddlInsurance.Items.Insert(0, new ListItem("~Select~", "0"));

                ddlDiscount.DataSource = ds;
                ddlDiscount.DataTextField = "CurrencyShortName";
                ddlDiscount.DataValueField = "CurrencyShortName";
                ddlDiscount.DataBind();
               // ddlDiscount.Items.Insert(0, new ListItem("~Select~", "0"));

                ddlChargeCurrency.DataSource = ds;
                ddlChargeCurrency.DataTextField = "CurrencyShortName";
                ddlChargeCurrency.DataValueField = "CurrencyShortName";
                ddlChargeCurrency.DataBind();
               // ddlChargeCurrency.Items.Insert(0, new ListItem("~Select~", "0"));

                ddlMiscellameous.DataSource = ds;
                ddlMiscellameous.DataTextField = "CurrencyShortName";
                ddlMiscellameous.DataValueField = "CurrencyShortName";
                ddlMiscellameous.DataBind();
              //  ddlMiscellameous.Items.Insert(0, new ListItem("~Select~", "0"));

                ddlAgency.DataSource = ds;
                ddlAgency.DataTextField = "CurrencyShortName";
                ddlAgency.DataValueField = "CurrencyShortName";
                ddlAgency.DataBind();
               // ddlAgency.Items.Insert(0, new ListItem("~Select~", "0"));

                ddlLoading.DataSource = ds;
                ddlLoading.DataTextField = "CurrencyShortName";
                ddlLoading.DataValueField = "CurrencyShortName";
                ddlLoading.DataBind();
    
                ddlHighSea.DataSource = ds;
                ddlHighSea.DataTextField = "CurrencyShortName";
                ddlHighSea.DataValueField = "CurrencyShortName";
                ddlHighSea.DataBind();

                //Charge Master
                ddlChargeType.DataSource = dsChargeType;
                ddlChargeType.DataTextField = "charge_desc";
                ddlChargeType.DataValueField = "charge_desc";
                ddlChargeType.DataBind();

             
            }
           
        }

        public int FOBUpdate()
        {
                double frghtexchgRate = Convert.ToDouble(txtFreightExchange.Text);
                double frghtrate = Convert.ToDouble(txtFreightRate.Text);
                double frghtamount = Convert.ToDouble(txtFreightAmount.Text);
                double insexchgRate = Convert.ToDouble(txtInsuranceExchange.Text);
                double insrate = Convert.ToDouble(txtInsuranceRate.Text);
                double insamount = Convert.ToDouble(txtInsuranceAmount.Text);
                double disexchgRate = Convert.ToDouble(txtDiscountExchange.Text);
                double disrate = Convert.ToDouble(txtDiscountRate.Text);
                double disamount = Convert.ToDouble(txtDiscountAmount.Text);

                double misExchange = Convert.ToDouble(txtMiscellameousExchange.Text);
                double misRate = Convert.ToDouble(txtMiscellameousRate.Text);
                double misAmount = Convert.ToDouble(txtMiscellameousAmount.Text);
                double AgencyExchange = Convert.ToDouble(txtAgencyExchange.Text);
                double AgencyRate = Convert.ToDouble(txtAgencyRate.Text);
                double AgencyAmount = Convert.ToDouble(txtAgencyAmount.Text);
                double LoadingExchange = Convert.ToDouble(txtLoadingExchange.Text);
                double LoadingRate = Convert.ToDouble(txtLoadingRate.Text);
                double LoadingAmount = Convert.ToDouble(txtLoadingAmount.Text);

                double HighSeaExchange = Convert.ToDouble(txtHighExRate.Text);
                double HighSeaRate = Convert.ToDouble(txtHighRate.Text);
                double HighSeaAmount = Convert.ToDouble(txtHighAmt.Text);
                double HighSeaAmountINR = Convert.ToDouble(txtHighAmtINR.Text);

                double FreightTyExRate = Convert.ToDouble(txtFrightTotalRate.Text);
                double FreightTyAmount = Convert.ToDouble(txtFreightTotalAmount.Text);
                double InsuranceTyExRate = Convert.ToDouble(txtInsuranceTotalRate.Text);
                double InsuranceTyAmount = Convert.ToDouble(txtInsuranceTotalAmount.Text);
                double MiscellaneousTyExRate = Convert.ToDouble(txtMisTotalRate.Text);
                double MiscellaneousTyAmt = Convert.ToDouble(txtMiscelTotalAmount.Text);
                         
            
                int Result = invBL.UpdateFreightDetails(ddlFreightDetails.SelectedValue, frghtexchgRate, frghtrate, frghtamount, ddlInsurance.SelectedValue, insexchgRate, insrate, insamount, ddlDiscount.SelectedValue, disexchgRate, disrate, disamount,
                    ddlMiscellameous.SelectedValue, misExchange, misRate, misAmount, ddlAgency.SelectedValue,
                    AgencyExchange, AgencyRate, AgencyAmount, ddlLoading.SelectedValue, LoadingExchange, LoadingRate, LoadingAmount, txtSaleCondition.Text, txtotherRelevant.Text,
                    ddlHighSea.SelectedValue, HighSeaExchange, HighSeaRate, HighSeaAmount, HighSeaAmountINR,
                    ddlJobNo.SelectedValue, txtInvoiceNo.Text, (string)Session["USER-NAME"], DateTime.Now,
                    FreightTyExRate,
                    FreightTyAmount,
                    InsuranceTyExRate,
                    InsuranceTyAmount,
                    MiscellaneousTyExRate,
                    MiscellaneousTyAmt
                    );
                return Result;
        }

        protected void btnSaveFreight_Click(object sender, EventArgs e)
        {
           
            if (ddlJobNo.SelectedValue != "~Select~")
            {
                if (txtInvoiceNo.Text != "")
                {
                    int Result = FOBUpdate();
                    if (Result == 1)
                    {
                        lblmsg.ForeColor = Color.Green;
                        lblmsg.Text = "Freight details saved successfully";
                        //Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "alert('Freight details saved successfully');", true);
                    }
                    else
                    {
                        Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "alert('Please the check values again');", true);
                    }
                }

                else
                {
                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "alert('Please Enter the Invoice No');", true);
                }
            }

            else
            {
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "alert('Please Select job No');", true);
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

                    Session["JobNo"] = ddlJobNo.SelectedValue;
                    GetJobDetails();
                    BindGridInvoice();
                    ClearFreightDetailsAll();
                    SVBRefDetails();
                    //ClearType();
            //    }
            //}
            //else
            //{
            //    string mess = "This Job can be used  " + joblogret;
            //    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('" + mess + "');", true);
            //}
        }
        
        protected void gvInvoiceDetails_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlJobNo.SelectedValue != "~Select~")
            {
                hdnEditValue.Value = "1";
                btnUpdateInvoice.Visible = true;
                btnAddInvoice.Visible = false;

                Session["InvoiceID"] = gvInvoiceDetails.SelectedRow.Cells[2].Text;
                txtInvoiceNo.Text = gvInvoiceDetails.SelectedRow.Cells[3].Text;
                Session["InvoiceNo"] = txtInvoiceNo.Text;
                txtDate.Text = gvInvoiceDetails.SelectedRow.Cells[4].Text;
                ddlTermsofInvoice.SelectedValue = gvInvoiceDetails.SelectedRow.Cells[5].Text;
                ddlFreightTy.SelectedValue = gvInvoiceDetails.SelectedRow.Cells[6].Text;
                ddlPayment.SelectedValue = gvInvoiceDetails.SelectedRow.Cells[7].Text;
                ddlTrans.SelectedValue = gvInvoiceDetails.SelectedRow.Cells[8].Text;
                ddlInvoiceCurrency.SelectedValue = gvInvoiceDetails.SelectedRow.Cells[9].Text;
                txtExchange.Text = gvInvoiceDetails.SelectedRow.Cells[10].Text;
                txtProductValues.Text = gvInvoiceDetails.SelectedRow.Cells[11].Text;
                txtProductINRValues.Text = gvInvoiceDetails.SelectedRow.Cells[12].Text;

                SingleFOB();
                InvoiceDetails();                
                DisableTotalFOB();

                PanelFreight.Visible = false;
            }


            else
            {
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "alert('Please Select job No');", true);
            }

        }

        public void SingleFOB()
        {
            try
            {
                DataSet ds = invBL.GetInvoiceFIM(ddlJobNo.SelectedValue);
                if (ds.Tables["InvoiceFIM"].Rows.Count != 0)
                {
                   if (txtTotInv.Text == "" || txtTotInv.Text == "0.00")
                    {
                        ScriptManager.RegisterStartupScript(Page, Page.GetType(), "red", "alert('Please Enter the Total Invoice Value');", true);                                      
                        txtTotInv.Focus();
                    }             
                                        
                    DataRowView row = ds.Tables["InvoiceFIM"].DefaultView[0];
                    txtMisTotalRate.Text = row["MiscellaneousTyExRate"].ToString();
                    txtMiscelTotalAmount.Text = row["MiscellaneousTyAmt"].ToString();

                    txtFrightTotalRate.Text = row["FreightTyExRate"].ToString();
                    txtFreightTotalAmount.Text = row["FreightTyAmount"].ToString();

                    txtInsuranceTotalRate.Text = row["InsuranceTyExRate"].ToString();
                    txtInsuranceTotalAmount.Text = row["InsuranceTyAmount"].ToString();
                    
                    ddlFreightDetails.SelectedValue = row["FreightCurrency"].ToString();                    
                    ddlMiscellameous.SelectedValue = row["MisCurrency"].ToString();
                    ddlInsurance.SelectedValue = row["InsuranceCurrency"].ToString();

                    txtFreightExchange.Text = row["FreightExchangeRate"].ToString();
                    txtMiscellameousExchange.Text = row["MisExchRate"].ToString();
                    txtInsuranceExchange.Text = row["InsuranceExchangeRate"].ToString();   
                    
                   // FOBCalculation();
                    
                }
            }
            catch
            { 
            }
        }

        public void InvoiceDetails()
        {
            try
            {
                DataSet ds1 = invBL.GetJobDetails(ddlJobNo.SelectedValue);
                DataSet ds = invBL.GetInvoiceDtl(ddlJobNo.SelectedValue, txtInvoiceNo.Text);
                if (ds.Tables["InvoiceDtl"].Rows.Count != 0)
                {
                    DataRowView row = ds.Tables["InvoiceDtl"].DefaultView[0];

                    BindCurrency();
                    //Freight & Insurance details
                    string FreightDetails = row["FreightCurrency"].ToString();
                    if (FreightDetails != "")
                    {
                        ddlFreightDetails.SelectedValue = FreightDetails;
                        txtFreightExchange.Text = row["FreightExchangeRate"].ToString();
                        txtFreightRate.Text = row["FreightRate"].ToString();
                        txtFreightAmount.Text = row["FreightAmount"].ToString();
                        txtFreightINRAmount.Text = row["FreightINRAmount"].ToString();
                    }
                    string Insurance = row["InsuranceCurrency"].ToString();
                    if (Insurance != "")
                    {
                        ddlInsurance.SelectedValue = Insurance;
                        txtInsuranceExchange.Text = row["InsuranceExchangeRate"].ToString();
                        txtInsuranceRate.Text = row["InsuranceRate"].ToString();
                        txtInsuranceAmount.Text = row["InsuranceAmount"].ToString();
                        txtInsuranceINRAmount.Text = row["InsuranceINRAmount"].ToString();

                    }
                    string Discount = row["DiscountCurrency"].ToString();
                    if (Discount != "")
                    {
                        ddlDiscount.SelectedValue = Discount;
                        txtDiscountExchange.Text = row["DiscountExchangeRate"].ToString();
                        txtDiscountRate.Text = row["DiscountRate"].ToString();
                        txtDiscountAmount.Text = row["DiscountAmount"].ToString();
                        txtDiscountINRAmount.Text = row["DiscountINRAmount"].ToString();
                    }

                    string MisCurrency = row["MisCurrency"].ToString();
                    if (MisCurrency != "")
                    {
                        ddlMiscellameous.SelectedValue = MisCurrency;
                        txtMiscellameousExchange.Text = row["MisExchRate"].ToString();
                        txtMiscellameousRate.Text = row["MisRate"].ToString();
                        txtMiscellameousAmount.Text = row["MisAmount"].ToString();
                        txtMiscellameousINRAmount.Text = row["MisINRAmount"].ToString();
                    }

                    //Other Charges
                    BindGridCharge();

                    //Consignor

                    if (row["ConsignorName"].ToString() == "")
                    {
                        if (ds1.Tables["Consignor"].Rows.Count != 0)
                        {
                            DataRowView row1 = ds1.Tables["Consignor"].DefaultView[0];
                            txtConsignorName.Text = row1["Consignor"].ToString();
                            txtConsignor.Text = row1["ConsignorAddress"].ToString();
                            string ConsignorCountry = row1["ConsignorCountry"].ToString();
                            if (ConsignorCountry != "")
                            {
                                ddlConsignorCountry.SelectedValue = ConsignorCountry.ToUpper();
                                
                            }
                        }
                    }
                    else
                    {
                        txtConsignorName.Text = row["ConsignorName"].ToString();
                        if (row["ConsignorNameAddress"].ToString() != "")
                        {
                            txtConsignor.Text = row["ConsignorNameAddress"].ToString();
                        }
                        if (row["ConsignorCountry"].ToString() != "")
                        {
                            string ConsignorCountry = row["ConsignorCountry"].ToString();
                            if (ConsignorCountry != "")
                            {
                                ddlConsignorCountry.SelectedValue = ConsignorCountry.ToUpper();
                            }
                        }
                    }
                                       
                    //Seller Details
                    txtSeller.Text = row["SellerName"].ToString();
                    txtSellerName.Text = row["SellerNameAddress"].ToString();
                    string SellerCountry = row["SellerCountry"].ToString();
                    if (SellerCountry != "")
                    {
                        ddlSellerCountry.SelectedValue = SellerCountry;
                    }
                    //Overseas Agent
                    txtBrokerName.Text = row["BrokerNameAddress"].ToString();
                    string BrokerCountry = row["BrokerCountry"].ToString();
                    if (BrokerCountry != "")
                    {
                        ddlBrokerCountry.SelectedValue = BrokerCountry;
                    }
                    //Relation & SVB Details
                    string Buyerseller = row["BuyerSeller"].ToString();
                    if (Buyerseller == "True")
                    {
                        chkBuyer.Checked = true;
                        txtRelation.Enabled = true;
                        txtRelationBase.Enabled = true;
                        txtRelationCondition.Enabled = true;

                        txtRelation.Text = row["Relation"].ToString();
                        txtRelationBase.Text = row["Base"].ToString();
                        txtRelationCondition.Text = row["Condition"].ToString();
                    }
                    string SVB = row["SVB"].ToString();
                    if (SVB == "True")
                    {
                        chkSVB.Checked = true;

                        txtSVBRelation.Enabled = true;
                        txtSVBDate.Enabled = true;
                        txtCustomHouse.Enabled = true;
                        ddlLoadingOn.Enabled = true;
                        txtLoadingRateAssbl.Enabled = true;
                        ddlLoadingAssblStatus.Enabled = true;
                        txtLoadingDuty.Enabled = true;
                        ddlLoadingDutyStatus.Enabled = true;

                        txtSVBRelation.Text = row["SVBRefNo"].ToString();
                        txtSVBDate.Text = row["SVBRefDate"].ToString();
                        txtCustomHouse.Text = row["CustomHouse"].ToString();
                        string LoadingOn = row["LoadingOn"].ToString();
                        if (LoadingOn != "")
                        {
                            ddlLoadingOn.SelectedValue = LoadingOn;
                        }
                        txtLoadingRateAssbl.Text = row["AssableLoadingRate"].ToString();
                        string AssableStatus = row["AssableStatus"].ToString();
                        if (AssableStatus != "")
                        {
                            ddlLoadingAssblStatus.SelectedValue = AssableStatus;
                        }
                        txtLoadingDuty.Text = row["DutyLoadingRate"].ToString();
                        string DutyStatus = row["DutyStatus"].ToString();
                        if (DutyStatus != "")
                        {
                            ddlLoadingDutyStatus.SelectedValue = DutyStatus;
                        }
                    }
                    //Other Details
                    txtNoofProd.Text = row["NoofProduct"].ToString();
                    txtPONo.Text = row["PONo"].ToString();
                    txtPODate.Text = row["PODate"].ToString();
                    chkSinglePO.Checked = Convert.ToBoolean(row["SinglePO"]);

                    txtContractNo.Text = row["ContractNo"].ToString();
                    txtContractDate.Text = row["ContractDate"].ToString();
                    txtLCNo.Text = row["LCNo"].ToString();
                    txtLCDate.Text = row["LCDate"].ToString();
                    string ValuationMethod = row["ValuationMethod"].ToString();
                    if (ValuationMethod != "")
                    {
                        ddlValuation.SelectedValue = ValuationMethod;
                    }
                    //Miscellaneous
                   
                    string AgencyCurrency = row["AgencyCurrency"].ToString();
                    if (AgencyCurrency != "")
                    {
                        ddlAgency.SelectedValue = AgencyCurrency;
                    }
                    string LoadingCurrency = row["LoadingCurrency"].ToString();
                    if (LoadingCurrency != "")
                    {
                        ddlLoading.SelectedValue = LoadingCurrency;
                    }
                  
                    txtAgencyExchange.Text = row["AgencyExchRate"].ToString();
                    txtAgencyRate.Text = row["AgencyRate"].ToString();
                    txtAgencyAmount.Text = row["AgencyAmount"].ToString();
                    txtLoadingExchange.Text = row["LoadingExchRate"].ToString();
                    txtLoadingRate.Text = row["LoadingRate"].ToString();
                    txtLoadingAmount.Text = row["LoadingAmount"].ToString();
                    txtSaleCondition.Text = row["SaleCondition"].ToString();
                    txtotherRelevant.Text = row["Remarks"].ToString();

                    string HighSeaCurrency= row["HighSeaCurrency"].ToString();
                    if (HighSeaCurrency != "")
                    {
                        ddlHighSea.SelectedValue = HighSeaCurrency;
                    }
                    else
                    {
                        ddlHighSea.SelectedValue = "~Select~";
                    }
                    txtHighExRate.Text = row["HighSeaExRate"].ToString();
                    txtHighRate.Text = row["HighSeaRate"].ToString();
                    txtHighAmt.Text = row["HighSeaAmt"].ToString();
                    txtHighAmtINR.Text = row["HighSeaAmtINR"].ToString();

                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('DataBase Error: " + ex.Message + "');", true);
            }
        }
        
        public void ClearInvoiceDetails()
        {
            txtInvoiceNo.Text = "";
            txtDate.Text = "";
            ddlTermsofInvoice.SelectedValue = "~Select~";
            //ddlFreightTy.SelectedValue = "~Select~";
            //ddlPayment.SelectedValue = "~Select~";
            //ddlTrans.SelectedValue = "~Select~";
            ddlInvoiceCurrency.SelectedValue = "~Select~";
            txtExchange.Text ="0";
            txtProductValues.Text = "0";
            txtProductINRValues.Text = "0";
            //txtProductValue.Text = "0";
        }

        public void ClearFreightDetails()
        {
            ////Freight & Insurance details
            //ddlFreightDetails.SelectedValue = "~Select~";
            //ddlInsurance.SelectedValue = "~Select~";
            //ddlDiscount.SelectedValue = "~Select~";
            //ddlMiscellameous.SelectedValue = "~Select~";
            //ddlAgency.SelectedValue = "~Select~";
            //ddlLoading.SelectedValue = "~Select~";
            //txtFreightExchange.Text = "0";
            txtFreightRate.Text = "0";
            txtFreightAmount.Text = "0";
            //txtInsuranceExchange.Text = "0";
            txtInsuranceRate.Text = "0";
            txtInsuranceAmount.Text = "0";
            //txtDiscountExchange.Text = "0";
            txtDiscountRate.Text = "0";
            txtDiscountAmount.Text = "0";

            //txtMiscellameousExchange.Text = "0";
            txtMiscellameousAmount.Text = "0";
            txtMiscellameousRate.Text = "0";
            //txtMiscellameousINRAmount.Text = "0";

            ////txtAgencyExchange.Text = "0";
            //txtAgencyRate.Text = "0";
            //txtAgencyAmount.Text = "0";
            //// txtAgencyINRAmount.Text = "0";
            //txtLoadingExchange.Text = "0";
            //txtLoadingRate.Text = "0";
            //txtLoadingAmount.Text = "0";
            ////txtLoadingINRAmount.Text = "0";

            //txtSaleCondition.Text = "";
            //txtotherRelevant.Text = "";
            //txtFreightINRAmount.Text = "0";
            //txtInsuranceINRAmount.Text = "0";
            //txtDiscountINRAmount.Text = "0";
            //txtMiscellameousINRAmount.Text = "0";
            //txtAgencyINRAmount.Text = "0";
            //txtLoadingINRAmount.Text = "0";


            //ddlHighSea.SelectedValue = "~Select~";
            //txtHighExRate.Text = "0";
            //txtHighRate.Text = "0";
            //txtHighAmt.Text = "0";
            //txtHighAmtINR.Text = "0";

            //txtFrightTotalRate.Text = "0";
            //txtFreightTotalAmount.Text = "0";
            //txtInsuranceTotalRate.Text = "0";
            //txtInsuranceTotalAmount.Text = "0";
            //txtMisTotalRate.Text = "0";
            //txtMiscelTotalAmount.Text = "0";

        }

        public void ClearFreightDetailsAll()
        {
            //Freight & Insurance details
            ddlFreightDetails.SelectedValue = "~Select~";
            ddlInsurance.SelectedValue = "~Select~";
            ddlDiscount.SelectedValue = "~Select~";
            ddlMiscellameous.SelectedValue = "~Select~";
            ddlAgency.SelectedValue = "~Select~";
            ddlLoading.SelectedValue = "~Select~";
            txtFreightExchange.Text ="0";
            txtFreightRate.Text = "0";
            txtFreightAmount.Text = "0";
            txtInsuranceExchange.Text ="0";
            txtInsuranceRate.Text = "0";
            txtInsuranceAmount.Text ="0";
            txtDiscountExchange.Text = "0";
            txtDiscountRate.Text = "0";
            txtDiscountAmount.Text = "0";

            txtMiscellameousExchange.Text = "0";
            txtMiscellameousAmount.Text = "0";
            txtMiscellameousRate.Text = "0";
            //txtMiscellameousINRAmount.Text = "0";

            txtAgencyExchange.Text = "0";
            txtAgencyRate.Text = "0";
            txtAgencyAmount.Text = "0";
           // txtAgencyINRAmount.Text = "0";
            txtLoadingExchange.Text = "0";
            txtLoadingRate.Text = "0";
            txtLoadingAmount.Text = "0";
            //txtLoadingINRAmount.Text = "0";

            txtSaleCondition.Text = "";
            txtotherRelevant.Text = "";
            txtFreightINRAmount.Text = "0";
            txtInsuranceINRAmount.Text = "0";
            txtDiscountINRAmount.Text = "0";
            txtMiscellameousINRAmount.Text = "0";
            txtAgencyINRAmount.Text = "0";
            txtLoadingINRAmount.Text = "0";


            ddlHighSea.SelectedValue = "~Select~";
            txtHighExRate.Text = "0";
            txtHighRate.Text = "0";
            txtHighAmt.Text ="0";
            txtHighAmtINR.Text ="0";

            txtFrightTotalRate.Text="0";
            txtFreightTotalAmount.Text="0";
            txtInsuranceTotalRate.Text="0";
            txtInsuranceTotalAmount.Text="0";
            txtMisTotalRate.Text="0";
            txtMiscelTotalAmount.Text = "0";
            
        }

        public void ClearOtherCharges()
        {
            //Other Charges
            ddlChargeType.SelectedValue = "~Select~";
            ddlChargeCurrency.SelectedValue = "~Select~";
            txtRate.Text = "0";
            txtAmount.Text = "0";
            //txtProductValue.Text = "";
            //txtOtherCharges.Text = "";
            //txtInvoiceChoices.Text = "";
            gvOtherCharges.DataSource = null;
            gvOtherCharges.DataBind();
        }

        public void ClearConsignor()
        {
            //Consignor
            txtConsignor.Text = "";
            ddlConsignorCountry.SelectedValue = "~Select~";
            txtSellerName.Text = "";
            ddlSellerCountry.SelectedValue = "~Select~";
            txtBrokerName.Text = "";
            ddlBrokerCountry.SelectedValue = "~Select~";
        }

        public void ClearRelation()
        {
            //Relation & SVB Details
            chkBuyer.Checked = false;
            txtRelation.Text = "";
            txtRelationBase.Text = "";
            txtRelationCondition.Text = "";
            chkSVB.Checked = false;
            txtSVBRelation.Text = "";
            txtSVBDate.Text = "";
            txtCustomHouse.Text = "";
            ddlLoadingOn.SelectedValue = "~Select~";
            txtLoadingRateAssbl.Text = "0";
            ddlLoadingAssblStatus.SelectedValue = "~Select~";
            txtLoadingDuty.Text = "0";
            ddlLoadingDutyStatus.SelectedValue = "~Select~";
        }

        public void ClearOtherDetails()
        {
            //Other Details
            txtContractNo.Text = "";
            txtContractDate.Text = "";
            txtLCNo.Text = "";
            txtLCDate.Text = "";
           // ddlValuation.SelectedValue = "~Select~";
        }

        public void ClearMiscellaneous()
        {
            //Miscellaneous
            ddlMiscellameous.SelectedValue = "~Select~";
            ddlAgency.SelectedValue = "~Select~";
            ddlLoading.SelectedValue = "~Select~";
            txtMiscellameousExchange.Text = "0";
            txtMiscellameousRate.Text = "0";
            txtMiscellameousAmount.Text = "0";
            txtAgencyExchange.Text = "0";
            txtAgencyRate.Text = "0";
            txtAgencyAmount.Text = "0";
            txtLoadingExchange.Text = "0";
            txtLoadingRate.Text = "0";
            txtLoadingAmount.Text = "0";
            txtSaleCondition.Text = "0";
            txtotherRelevant.Text = "0";
        }

        protected void btnSaveConsignor_Click(object sender, EventArgs e)
        {
            if (ddlJobNo.SelectedValue != "~Select~")
            {
                if (txtInvoiceNo.Text != "")
                {
                   
                    int Result = invBL.UpdateConsignorDetails(txtConsignor.Text, ddlConsignorCountry.SelectedValue, txtSellerName.Text, ddlSellerCountry.SelectedValue, txtBrokerName.Text, ddlBrokerCountry.SelectedValue, ddlJobNo.SelectedValue, txtInvoiceNo.Text, (string)Session["USER-NAME"], DateTime.Now,txtConsignorName.Text,txtSeller.Text,txtBroker.Text);

                    if (Result == 1)
                    {
                        SVBRefDetails();
                        SVBUpdate();
                        lblmsg.ForeColor = Color.Green;
                        lblmsg.Text = "Consignor details saved successfully";
                        //Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "alert('Consignor details saved successfully');", true);
                    }
                    else
                    {
                        Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "alert('Please the check values again');", true);
                    }
                }

                else
                {
                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "alert('Please Enter the Invoice No');", true);
                }
            }

            else
            {
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "alert('Please Select job No');", true);
            }
        }

        protected void chkBuyer_CheckedChanged(object sender, EventArgs e)
        {
            if (chkBuyer.Checked == true)
            {
                txtRelation.Enabled = true;
                txtRelationBase.Enabled = true;
                txtRelationCondition.Enabled = true;
            }
            else
            {
                txtRelation.Enabled = false;
                txtRelationBase.Enabled = false;
                txtRelationCondition.Enabled = false;
            }

        }

        protected void chkSVB_CheckedChanged(object sender, EventArgs e)
        {
            if (chkSVB.Checked == true)
            {
                txtSVBRelation.Enabled = true;
                txtSVBDate.Enabled = true;
                txtCustomHouse.Enabled = true;
                ddlLoadingOn.Enabled = true;
                txtLoadingRateAssbl.Enabled = true;
                ddlLoadingAssblStatus.Enabled = true;
                txtLoadingDuty.Enabled = true;
                ddlLoadingDutyStatus.Enabled = true;

            }
            else
            {
                txtSVBRelation.Enabled = false;
                txtSVBDate.Enabled = false;
                txtCustomHouse.Enabled = false;
                ddlLoadingOn.Enabled = false;
                txtLoadingRateAssbl.Enabled = false;
                ddlLoadingAssblStatus.Enabled = false;
                txtLoadingDuty.Enabled = false;
                ddlLoadingDutyStatus.Enabled = false;
            }
        }

        protected void btnSaveRelation_Click(object sender, EventArgs e)
        {
            if (ddlJobNo.SelectedValue != "~Select~")
            {
                if (txtInvoiceNo.Text != "")
                {
                    int Result = SVBUpdate();
                    if (Result == 1)
                    {
                        lblmsg.ForeColor = Color.Green;
                        lblmsg.Text = "Relation and SVB details saved successfully";
                       // Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "alert('Relation and SVB details successfully saved');", true);
                    }
                    else
                    {
                        Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "alert('Please the check values again');", true);
                    }
                }

                else
                {
                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "alert('Please Enter the Invoice No');", true);
                }
            }

            else
            {
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "alert('Please Select job No');", true);
            }
        }

        public int SVBUpdate()
         {
                    string Buyer="";
                    string SVB="";
                    double assloadrate = Convert.ToDouble(txtLoadingRateAssbl.Text);
                    double dutyloadrate = Convert.ToDouble(txtLoadingDuty.Text);
                    if(chkBuyer.Checked==true)
                    {
                    Buyer="1";
                    }
                    else
                    {
                    Buyer="0"; 
                    }
                     if(chkSVB.Checked==true)
                    {
                    SVB="1";
                    }
                    else
                    {
                    SVB="0"; 
                    }
                    
                    int Result = invBL.UpdateRelationSVBDetails(Buyer,txtRelation.Text,txtRelationBase.Text,txtRelationCondition.Text,SVB,txtSVBRelation.Text,txtSVBDate.Text,txtCustomHouse.Text,
                        ddlLoadingOn.SelectedValue, assloadrate, ddlLoadingAssblStatus.SelectedValue, dutyloadrate, ddlLoadingDutyStatus.SelectedValue, ddlJobNo.SelectedValue, txtInvoiceNo.Text, (string)Session["USER-NAME"], DateTime.Now);
                    return Result;
           }

        protected void btnSaveOtherDetails_Click(object sender, EventArgs e)
        {
            if (ddlJobNo.SelectedValue != "~Select~")
            {
                if (txtInvoiceNo.Text != "")
                {
                    
                    int Result = invBL.UpdateOtherDetails(txtContractNo.Text, txtContractDate.Text, txtLCNo.Text, txtLCDate.Text, ddlValuation.SelectedValue,txtNoofProd.Text,chkSinglePO.Checked,txtPONo.Text,txtPODate.Text,
                        ddlJobNo.SelectedValue, txtInvoiceNo.Text, (string)Session["USER-NAME"], DateTime.Now);

                    if (Result == 1)
                    {
                        lblmsg.ForeColor = Color.Green;
                        lblmsg.Text = "Other details saved successfully ";
                       // Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "alert('Other details successfully saved');", true);
                    }
                    else
                    {
                        Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "alert('Please the check values again');", true);
                    }
                }

                else
                {
                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "alert('Please Enter the Invoice No');", true);
                }
            }

            else
            {
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "alert('Please Select job No');", true);
            }
        }

        protected void btnSaveMiscellenaus_Click(object sender, EventArgs e)
        {
            if (ddlJobNo.SelectedValue != "~Select~")
            {
                if (txtInvoiceNo.Text != "")
                {
                    double misExchange = Convert.ToDouble(txtMiscellameousExchange.Text);
                    double misRate = Convert.ToDouble(txtMiscellameousRate.Text);
                    double misAmount = Convert.ToDouble(txtMiscellameousAmount.Text);
                    double AgencyExchange = Convert.ToDouble(txtAgencyExchange.Text);
                    double AgencyRate = Convert.ToDouble(txtAgencyRate.Text);
                    double AgencyAmount = Convert.ToDouble(txtAgencyAmount.Text);
                    double LoadingExchange = Convert.ToDouble(txtLoadingExchange.Text);
                    double LoadingRate = Convert.ToDouble(txtLoadingRate.Text);
                    double LoadingAmount = Convert.ToDouble(txtLoadingAmount.Text);

                   
                    int Result = invBL.UpdateMiscellaneousDetails(ddlMiscellameous.SelectedValue,misExchange,misRate,misAmount,ddlAgency.SelectedValue,
                        AgencyExchange,AgencyRate,AgencyAmount,ddlLoading.SelectedValue,LoadingExchange,LoadingRate,LoadingAmount,txtSaleCondition.Text,txtotherRelevant.Text,
                         ddlJobNo.SelectedValue, txtInvoiceNo.Text, (string)Session["USER-NAME"], DateTime.Now);

                    if (Result == 1)
                    {
                        lblmsg.ForeColor = Color.Green;
                        lblmsg.Text = "Miscellaneous details saved successfully ";
                        //Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "alert('Miscellaneous details saved successfully ');", true);
                    }
                    else
                    {
                        Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "alert('Please the check values again');", true);
                    }
                }

                else
                {
                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "alert('Please Enter the Invoice No');", true);
                }
            }

            else
            {
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "alert('Please Select job No');", true);
            }
        }

        private void BindGridCharge()
        {
            DataSet ds = invBL.GetOtherCharges(ddlJobNo.SelectedValue,txtInvoiceNo.Text);

            if (ds.Tables["Charges"].Rows.Count != 0)
            {
                gvOtherCharges.DataSource = ds;
                gvOtherCharges.DataBind();
                gvOtherCharges.Visible = true;
            }
            else
            {
                gvOtherCharges.DataSource = null;
                gvOtherCharges.DataBind();
            }
            OtherChargesFooter();
        }

        protected void gvOtherCharges_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnOtherCharges.Visible = true;
            btnAddOtherCharges.Visible = false;

            Session["OtherCharges"] = gvOtherCharges.SelectedRow.Cells[2].Text;
            ddlChargeType.SelectedValue=gvOtherCharges.SelectedRow.Cells[3].Text;
            ddlChargeCurrency.SelectedValue = gvOtherCharges.SelectedRow.Cells[4].Text;
            txtRate.Text = gvOtherCharges.SelectedRow.Cells[5].Text;
            txtAmount.Text = gvOtherCharges.SelectedRow.Cells[6].Text;
            txtAmountINR.Text = gvOtherCharges.SelectedRow.Cells[7].Text;
        }

        protected void btnFreightDetails_Click(object sender, EventArgs e)
        {
            PanelOtherDetails.Visible =false;
            PanelFreight.Visible = true;
            Panel2.Visible = false;
            Panel3.Visible = false;
            Panel4.Visible = false;
            if (ddlFreightTy.SelectedValue == "Single")
            {
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myFunction", "TotalFOBRate('" + txtTotInv.ClientID + "','" + txtFreightTotalAmount.ClientID + "','" + txtFrightTotalRate.ClientID + "','" + txtFreightINRAmount.ClientID + "','" + txtFreightAmount.ClientID + "','" + txtFreightRate.ClientID + "','" + txtFreightExchange.ClientID + "', '" + txtExchange.ClientID + "','" + txtProductValues.ClientID + "'); TotalFOBMisRate('" + txtTotInv.ClientID + "','" + txtMiscelTotalAmount.ClientID + "','" + txtMisTotalRate.ClientID + "','" + txtMiscellameousINRAmount.ClientID + "','" + txtMiscellameousAmount.ClientID + "','" + txtMiscellameousRate.ClientID + "','" + txtMiscellameousExchange.ClientID + "', '" + txtExchange.ClientID + "','" + txtProductValues.ClientID + "'); TotalFOBRate('" + txtTotInv.ClientID + "','" + txtInsuranceTotalAmount.ClientID + "','" + txtInsuranceTotalRate.ClientID + "','" + txtInsuranceINRAmount.ClientID + "','" + txtInsuranceAmount.ClientID + "','" + txtInsuranceRate.ClientID + "','" + txtInsuranceExchange.ClientID + "','" + txtExchange.ClientID + "','" + txtProductValues.ClientID + "');", true);                
            }
        }

        protected void btnOtherChargesVisible_Click(object sender, EventArgs e)
        {
            PanelOtherDetails.Visible = true;
            PanelFreight.Visible = false;
            Panel2.Visible = false;
            Panel3.Visible = false;
            Panel4.Visible = false;
            
        }

        protected void btnProduct_Click(object sender, EventArgs e)
        {
            Session["InvoiceNo"] = txtInvoiceNo.Text;
            Response.Redirect("frmProductMainPage.aspx?Mode=Invoice");
            //Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "alert(' Under Construction');", true);
        }

        protected void btnConsignor_Click(object sender, EventArgs e)
        {
         
            //MultiView1.ActiveViewIndex = 2;
            PanelOtherDetails.Visible = false;
            PanelFreight.Visible = false;
            Panel2.Visible = true;
            Panel3.Visible = false;
            Panel4.Visible = false;
          
        }

        protected void btnRelationSVBDetails_Click(object sender, EventArgs e)
        {
            //MultiView1.ActiveViewIndex = 3;
            PanelOtherDetails.Visible = false;
            PanelFreight.Visible = false;
            Panel2.Visible = false;
            Panel3.Visible = true;
            Panel4.Visible = false;
                  }

        protected void btnOtherDetails_Click(object sender, EventArgs e)
        {
            //MultiView1.ActiveViewIndex =4;
            PanelOtherDetails.Visible = false;
            PanelFreight.Visible = false;
            Panel2.Visible = false;
            Panel3.Visible = false;
            Panel4.Visible = true;
           
        }

        protected void btnCancelFreight_Click(object sender, EventArgs e)
        {
            ClearFreightDetails();
        }

        protected void btnCancelOtherCharges_Click(object sender, EventArgs e)
        {
            ddlChargeType.SelectedValue = "~Select~";
            ddlChargeCurrency.SelectedValue = "~Select~";
            txtRate.Text = "0";
            txtAmount.Text = "0";
        }

        protected void btnCancelConsignor_Click(object sender, EventArgs e)
        {
            ClearConsignor();
        }

        protected void btnCancelRelation_Click(object sender, EventArgs e)
        {
            ClearRelation();
        }

        protected void btnCancelOtherDetails_Click(object sender, EventArgs e)
        {
            ClearOtherDetails();
        }

        protected void btnCanelMiscellenaus_Click(object sender, EventArgs e)
        {
            ClearMiscellaneous();
        }

        public void GetJobDetails()
        {
            try
            {
                DataSet ds = obj1.GetJobImportShipment(ddlJobNo.SelectedValue);
                if (ds.Tables["Table"].Rows.Count != 0)
                {
                    DataRowView row = ds.Tables["Table"].DefaultView[0];
                    lblJobDate.Text = row["JobReceivedDate"].ToString();
                    lblMode.Text = row["Mode"].ToString();
                    lblCurrency.Text = row["Currency"].ToString();
                    lblCustom.Text = row["Custom"].ToString();
                    lblBeType.Text = row["BEType"].ToString();
                    lblStatus.Text = row["DocFillingStatus"].ToString();
                    //lblBeDate.Text = row["BENo"].ToString();
                    //lblBeNo.Text = row["BEDate"].ToString();
                    ddlInvoiceCurrency.SelectedValue = Convert.ToString(row["Currency"]);
                    if (Convert.ToString(row["TotalInvoiceValue"]) == "" || Convert.ToString(row["TotalInvoiceValue"]) == "0.00")
                    {
                        txtTotInv.Text = "0.00";                     
                        txtTotInv.Focus();
                    }
                    else
                    {
                        txtTotInv.Text = Convert.ToString(row["TotalInvoiceValue"]);
                    }
                    //Consignor, ConsignorAddress, ConsignorCity, ConsignorCountry
                    txtConsignorName.Text = Convert.ToString(row["Consignor"]);
                    txtConsignor.Text = Convert.ToString(row["ConsignorAddress"]);
                    if((row["ConsignorCountry"]!=null) && (row["ConsignorCountry"]!=""))
                    {
                    ddlConsignorCountry.SelectedValue = Convert.ToString(row["ConsignorCountry"]).ToUpper();
                    }

                    lblImporter.Text = Convert.ToString(row["Importer"]);
                    //Importer
                }
            }
            catch
            {
            }
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

                    Response.Redirect("frmShipment.aspx?Mode=Import");
            //    }
            //}
            //else
            //{
            //    string mess = "This Job can be used  " + joblogret;
            //    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('" + mess + "');", true);
            //}
        }

        protected void btnCheckList_Click(object sender, EventArgs e)
        {
            Response.Redirect("frmPrintCheckList.aspx");
        }

        protected void btnNew_Click(object sender, EventArgs e)
        {
            BindCurrency();
            txtInvoiceNo.Text = "";
            txtDate.Text = "";
            ddlTermsofInvoice.SelectedIndex = 0;
           // ddlFreightTy.SelectedIndex = 0;
            //ddlPayment.SelectedIndex = 0;
            //ddlTrans.SelectedIndex = 0;
            ddlInvoiceCurrency.SelectedIndex = ddlInvoiceCurrency.Items.IndexOf(ddlInvoiceCurrency.Items.FindByText("~Select~"));
           // ddlInvoiceCurrency.SelectedValue = lblCurrency.Text;
            txtExchange.Text = "";
            txtProductValues.Text = "";
            txtProductINRValues.Text = "";
            btnAddInvoice.Visible = true;
            btnUpdateInvoice.Visible = false;
        }

        protected void btnDelete_Click(object sender, ImageClickEventArgs e)
        {
            ImageButton btndel = sender as ImageButton;
            GridViewRow row = (GridViewRow)btndel.NamingContainer;
            string TransId = row.Cells[2].Text;
            string InvNo = row.Cells[3].Text;
            string JobNo = ddlJobNo.SelectedValue;
            int i = invBL.DeleteInvoiceDetails(TransId, InvNo, JobNo);
            if (i >= 1)
            {
                BindGridInvoice();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "redirect", "alert('Deleted Successfully');", true);
            }
        }
       
        public void footertotrow()
        {
            try
            {
                double TotalAmt = 0;
                double TotalINRAmt = 0;
                int i = 0;
                foreach (GridViewRow gv in gvInvoiceDetails.Rows)
                {
                    string amt = gvInvoiceDetails.Rows[i].Cells[11].Text;
                    string inramt = gvInvoiceDetails.Rows[i].Cells[12].Text;
                    TotalAmt = TotalAmt + Convert.ToDouble(amt);
                    TotalINRAmt = TotalINRAmt + Convert.ToDouble(inramt);
                    i++;
                }
                gvInvoiceDetails.FooterRow.Cells[10].Text = "Total Amount ";
                gvInvoiceDetails.FooterRow.Cells[11].Text = Convert.ToString(TotalAmt);
                gvInvoiceDetails.FooterRow.Cells[12].Text = Convert.ToString(TotalINRAmt);
                Session["TotalINRAmt"] = TotalAmt;
                
            }
            catch (Exception ex)
            {

            }
        }

        //public void FOBCalculation()
        //{
        //    try
        //    {
        //        if (ddlFreightTy.SelectedValue == "Single")
        //        {
        //            //txtTotInv.Text = "0";

        //            //txtFreightExchange.Text = "0";
        //            //txtFreightRate.Text = "0";
        //            //txtFreightAmount.Text = "0";
        //            //txtInsuranceExchange.Text = "0";
        //            //txtInsuranceRate.Text = "0";
        //            //txtInsuranceAmount.Text = "0";

        //            //txtMiscellameousExchange.Text = "0";
        //            //txtMiscellameousAmount.Text = "0";
        //            //txtMiscellameousRate.Text = "0";
        //            ////txtMiscellameousINRAmount.Text = "0";

        //            ////txtAgencyExchange.Text = "0";
        //            ////txtAgencyRate.Text = "0";
        //            ////txtAgencyAmount.Text = "0";
        //            ////// txtAgencyINRAmount.Text = "0";
        //            ////txtLoadingExchange.Text = "0";
        //            ////txtLoadingRate.Text = "0";
        //            ////txtLoadingAmount.Text = "0";
        //            //////txtLoadingINRAmount.Text = "0";


        //            ////txtFreightINRAmount.Text = "0";
        //            ////txtInsuranceINRAmount.Text = "0";
        //            ////txtDiscountINRAmount.Text = "0";
        //            ////txtMiscellameousINRAmount.Text = "0";



        //            ////ddlHighSea.SelectedValue = "~Select~";
        //            ////txtHighExRate.Text = "0";
        //            ////txtHighRate.Text = "0";
        //            ////txtHighAmt.Text = "0";
        //            ////txtHighAmtINR.Text = "0";

        //            //txtFrightTotalRate.Text = "0";
        //            //txtFreightTotalAmount.Text = "0";
        //            //txtInsuranceTotalRate.Text = "0";
        //            //txtInsuranceTotalAmount.Text = "0";
        //            //txtMisTotalRate.Text = "0";
        //            //txtMiscelTotalAmount.Text = "0";


        //            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "red", "alert('Please Enter the Total Invoice Value');", true);
                   
        //            //ScriptManager.RegisterStartupScript(Page, Page.GetType(), "myFunction", "TotalFOBRate('" + txtTotInv.ClientID + "','" + txtFreightTotalAmount.ClientID + "','" + txtFrightTotalRate.ClientID + "','" + txtFreightINRAmount.ClientID + "','" + txtFreightAmount.ClientID + "','" + txtFreightRate.ClientID + "','" + txtFreightExchange.ClientID + "', '" + txtExchange.ClientID + "','" + txtProductValues.ClientID + "');", true);
        //            //PanelFreight.Visible = false;                    
                    

        //            //double InvTotalAmnt = 0.00;
        //            //double InvProdAmnt = 0.00;
        //            //double InvProdExRate = 0.00;

        //            //double TotInvAmntinINR = 0.00;
        //            //double SingInvAmntinINR = 0.00;
        //            //double FreightAmntinINR = 0.00;
               
        //            //double MiscTotAmnt = 0.00;
        //            //double MiscTotRate = 0.00;
        //            //double MiscInvAmntINR = 0.00;
        //            //double MiscInvAmntUSD = 0.00;
        //            //double MiscInvRate = 0.00;
        //            //double MiscExRate = 0.00;

        //            //double FreightTotAmnt = 0.00;
        //            //double FreightTotRate = 0.00;
        //            //double FrieghtInvAmntINR = 0.00;
        //            //double FrieghtInvAmntUSD = 0.00;
        //            //double FrieghtInvRate = 0.00;
        //            //double FrieghtExRate = 0.00;


        //            //double InsurTotAmnt = 0.00;
        //            //double InsurTotRate = 0.00;
        //            //double InsurInvAmntINR = 0.00;
        //            //double InsurInvAmntUSD = 0.00;
        //            //double InsurInvRate = 0.00;
        //            //double InsurExRate = 0.00;
                    
        //            //InvTotalAmnt = Convert.ToDouble(txtTotInv.Text);
        //            //FreightTotAmnt = Convert.ToDouble(txtFreightTotalAmount.Text);
        //            //FreightTotRate = Convert.ToDouble(txtFrightTotalRate.Text);
        //            //FrieghtInvAmntINR = Convert.ToDouble(txtFreightINRAmount.Text);
        //            //FrieghtInvAmntUSD = Convert.ToDouble(txtFreightAmount.Text);
        //            //FrieghtInvRate = Convert.ToDouble(txtFreightRate.Text);
        //            //FrieghtExRate = Convert.ToDouble(txtFreightExchange.Text);

        //            //InvProdAmnt = Convert.ToDouble(txtProductValues.Text);
        //            //InvProdExRate = Convert.ToDouble(txtExchange.Text);

                    
        //            //TotInvAmntinINR =  InvTotalAmnt * InvProdExRate;

        //            //SingInvAmntinINR =  InvProdAmnt * InvProdExRate;

        //            //FreightAmntinINR = FreightTotAmnt * FrieghtExRate;


        //            //FreightTotRate =  (FreightAmntinINR / TotInvAmntinINR) * 100;

        //            //FrieghtInvAmntINR =  (FreightAmntinINR / TotInvAmntinINR) * SingInvAmntinINR;

        //            //FrieghtInvAmntUSD = FrieghtInvAmntINR / FrieghtExRate; 

        //            //FrieghtInvRate = (FreightAmntinINR / SingInvAmntinINR) * 100;


        //            //txtFrightTotalRate.Text = Convert.ToString(FreightTotRate);
        //            //txtFreightINRAmount.Text = Convert.ToString(FrieghtInvAmntINR);
        //            //txtFreightAmount.Text = Convert.ToString(FrieghtInvAmntUSD);
        //            //txtFreightRate.Text = Convert.ToString(FrieghtInvRate);


        //            //if (Convert.ToDouble(txtFreightExchange.Text) != 0)
        //            //{
        //            //    txtFreightAmount.Text = Convert.ToString((((Convert.ToDouble(txtFreightTotalAmount.Text) * Convert.ToDouble(txtFreightExchange.Text)) / (Convert.ToDouble(txtTotInv.Text) * Convert.ToDouble(txtExchange.Text))) * (Convert.ToDouble(txtProductValues.Text) * Convert.ToDouble(txtExchange.Text))) / Convert.ToDouble(txtFreightExchange.Text));
        //            //    txtFreightRate.Text = txtFrightTotalRate.Text;
        //            //    txtFreightINRAmount.Text = Convert.ToString(Convert.ToDouble(txtFreightExchange.Text) * Convert.ToDouble(txtFreightAmount.Text));
        //            //}
        //            //// txtInsuranceExchange.Text = "0";
        //            //if (Convert.ToDouble(txtInsuranceExchange.Text) != 0)
        //            //{
        //            //    txtInsuranceAmount.Text = Convert.ToString((((Convert.ToDouble(txtInsuranceTotalAmount.Text) * Convert.ToDouble(txtInsuranceExchange.Text)) / (Convert.ToDouble(txtTotInv.Text) * Convert.ToDouble(txtExchange.Text))) * (Convert.ToDouble(txtProductValues.Text) * Convert.ToDouble(txtExchange.Text))) / Convert.ToDouble(txtInsuranceExchange.Text));
        //            //    txtInsuranceRate.Text = txtInsuranceTotalRate.Text;
        //            //    txtInsuranceINRAmount.Text = Convert.ToString(Convert.ToDouble(txtInsuranceExchange.Text) * Convert.ToDouble(txtInsuranceAmount.Text));
        //            //}
        //            //// txtMiscellameousExchange.Text = "0";
        //            //if (Convert.ToDouble(txtMiscellameousExchange.Text) != 0)
        //            //{
        //            //    txtMiscellameousAmount.Text = Convert.ToString((((Convert.ToDouble(txtMiscelTotalAmount.Text) * Convert.ToDouble(txtMiscellameousExchange.Text)) / (Convert.ToDouble(txtTotInv.Text) * Convert.ToDouble(txtExchange.Text))) * (Convert.ToDouble(txtProductValues.Text) * Convert.ToDouble(txtExchange.Text))) / Convert.ToDouble(txtMiscellameousExchange.Text));
        //            //    txtMiscellameousRate.Text = txtMisTotalRate.Text;
        //            //    txtMiscellameousINRAmount.Text = Convert.ToString(Convert.ToDouble(txtMiscellameousExchange.Text) * Convert.ToDouble(txtMiscellameousAmount.Text));
        //            //}

        //        }
        //    }
        //    catch
        //    {
        //    }
        //}
        
        public void AddInvoiceDetails()
        {
            try
            {
                int Result = invBL.InsertInvoiceDetails(
                           ddlJobNo.SelectedValue,//Job No
                           txtInvoiceNo.Text, //Invoice No
                           txtDate.Text, //Invoice Date
                           ddlTermsofInvoice.SelectedValue,//Terms of Invoice
                           ddlFreightTy.SelectedValue,//Freight Type
                           ddlPayment.SelectedValue,//Payment Type
                           ddlTrans.SelectedValue,//Based on Transaction
                           ddlInvoiceCurrency.SelectedValue, //Invoice Currency
                           txtExchange.Text,//Exchange Rate
                           txtProductValues.Text,//Product Value
                           (string)Session["USER-NAME"],
                           DateTime.Now,
                           (string)Session["USER-NAME"],
                           DateTime.Now);
                if (Result == 1)
                {
                    Session["InvoiceNo"] = txtInvoiceNo.Text;
                    Session["JobNo"] = ddlJobNo.SelectedValue;
                    //save Consignor Details
                    invBL.UpdateConsignorDetails(txtConsignor.Text, ddlConsignorCountry.SelectedValue, 
                        txtSellerName.Text, ddlSellerCountry.SelectedValue, txtBrokerName.Text, 
                        ddlBrokerCountry.SelectedValue, ddlJobNo.SelectedValue, txtInvoiceNo.Text, 
                        (string)Session["USER-NAME"], DateTime.Now, txtConsignorName.Text, txtSeller.Text, txtBroker.Text);
                    DisableTotalFOB();

                    //FOBCalculation();
                    FOBUpdate();

                    lblmsg.ForeColor = Color.Green;
                    lblmsg.Text = "Invoice details saved successfully";
                    // Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "alert('Invoice details saved successfully');", true);
                }
                else
                {
                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "alert('Please the check values again');", true);
                }
                BindGridInvoice();
            }
            catch
            {
            }
        }

        protected void btnAddInvoice_Click(object sender, EventArgs e)
        {
            if (ddlJobNo.SelectedValue != "~Select~")
            {
                if (txtInvoiceNo.Text != "")
                {
                    double TotalInvoiceValue = Convert.ToDouble(txtTotInv.Text);
                    double InvoiceValue = Convert.ToDouble(txtProductValues.Text);
                    if (ddlFreightTy.SelectedValue == "Single")
                    {
                        if (TotalInvoiceValue >= InvoiceValue)
                        {
                            AddInvoiceDetails();
                            SVBUpdate();
                            //ddlMiscellameous.SelectedValue = ddlInvoiceCurrency.SelectedValue;
                            //ddlFreightDetails .SelectedValue = ddlInvoiceCurrency.SelectedValue;
                            //ddlInsurance.SelectedValue = ddlInvoiceCurrency.SelectedValue;
                        }
                        else
                        {
                            lblmsg.ForeColor = Color.Red;
                            lblmsg.Text = "Please Enter Total Invoice Value";
                            //Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "alert('Please Enter Total Invoice Value');", true);
                        }
                    }
                    else
                    {
                        AddInvoiceDetails();
                        SVBUpdate();
                    }
                }
                     else
                    {
                        Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "alert('Please Enter the Invoice No');", true);
                    }
            }
            else
            {
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "alert('Please Select job No');", true);
            }
        }
       
        public void DisableTotalFOB()
        {
            if (ddlFreightTy.SelectedValue == "Separate")
            {
                txtFreightTotalAmount.Enabled = false;
                txtFrightTotalRate.Enabled = false;
                txtInsuranceTotalAmount.Enabled = false;
                txtInsuranceTotalRate.Enabled = false;
                txtMiscelTotalAmount.Enabled = false;
                txtMisTotalRate.Enabled = false;
                txtMiscellameousRate.Enabled = true;
                txtMiscellameousAmount.Enabled = true;
                txtMiscellameousINRAmount.Enabled = true;
                txtFreightRate.Enabled = true;
                txtFreightAmount.Enabled = true;
                txtFreightINRAmount.Enabled = true;
                txtInsuranceRate.Enabled = true;
                txtInsuranceAmount.Enabled = true;
                txtInsuranceINRAmount.Enabled = true;
            }
            else
            {
                txtFreightTotalAmount.Enabled = true;
                txtFrightTotalRate.Enabled = true;
                txtInsuranceTotalAmount.Enabled = true;
                txtInsuranceTotalRate.Enabled = true;
                txtMiscelTotalAmount.Enabled = true;
                txtMisTotalRate.Enabled = true;

                txtMiscellameousRate.Enabled = false;
                txtMiscellameousAmount.Enabled = false;
                txtMiscellameousINRAmount.Enabled = false;

                txtFreightRate.Enabled = false;
                txtFreightAmount.Enabled = false;
                txtFreightINRAmount.Enabled = false;

                txtInsuranceRate.Enabled = false;
                txtInsuranceAmount.Enabled = false;
                txtInsuranceINRAmount.Enabled = false;
            }
        }

        public void UpdateInvoioceDetails()
        {
            try
            {
                int Result = invBL.UpdateInvoiceDetails(txtInvoiceNo.Text, txtDate.Text, ddlTermsofInvoice.SelectedValue, ddlFreightTy.SelectedValue, ddlPayment.SelectedValue,
                    ddlTrans.SelectedValue, ddlInvoiceCurrency.SelectedValue, txtExchange.Text, txtProductValues.Text, txtProductINRValues.Text,
                     (string)Session["USER-NAME"], DateTime.Now, (string)Session["InvoiceID"]);
            
                if (Result == 1)
                {
                    DisableTotalFOB();
                    //FOBCalculation();
                    FOBUpdate();


                    lblmsg.ForeColor = Color.Green;
                    lblmsg.Text = "Invoice details update successfully";
                    //Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "alert('Invoice details update successfully ');", true);
                }
                else
                {
                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "alert('Please the check values again');", true);
                }
                BindGridInvoice();
            }
            catch
            {
            }
        }

        protected void btnUpdateInvoice_Click(object sender, EventArgs e)
        {
            if (ddlJobNo.SelectedValue != "~Select~")
            {
                if (txtInvoiceNo.Text != "")
                {
                    double TotalInvoiceValue = Convert.ToDouble(txtTotInv.Text);
                    double InvoiceValue = Convert.ToDouble(txtProductValues.Text);
                    if (ddlFreightTy.SelectedValue == "Single")
                    {
                        if (TotalInvoiceValue >= InvoiceValue)
                        {

                            UpdateInvoioceDetails();
                            SVBUpdate();
                        }
                        else
                        {
                            lblmsg.ForeColor = Color.Red;
                            lblmsg.Text = "Please Enter Total Invoice Value";
                            //Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "alert('Please Enter Total Invoice Value');", true);
                        }
                    }
                    else
                    {
                        UpdateInvoioceDetails();
                        SVBUpdate();
                    }
                }
                else
                {
                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "alert('Please Enter the Invoice No');", true);
                }
            }
            else
            {
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "alert('Please Select job No');", true);
            }
        }

        protected void btnOtherCharges_Click(object sender, EventArgs e)
        {
            if (ddlJobNo.SelectedValue != "~Select~")
            {
                if (txtInvoiceNo.Text != "")
                {
                    btnAddOtherCharges.Visible = false;
                    btnOtherCharges.Visible = true;
                    double ExchangeRate = Convert.ToDouble(txtRate.Text);
                    double amount = Convert.ToDouble(txtAmount.Text);
                    double amountinr = Convert.ToDouble(txtAmountINR.Text);

                    int Result = invBL.UpdateOthers((string)Session["OtherCharges"], ddlChargeType.SelectedValue, ddlChargeCurrency.SelectedValue, ExchangeRate, amount, ddlJobNo.SelectedValue, txtInvoiceNo.Text, (string)Session["USER-NAME"], DateTime.Now, amountinr);
                    BindGridCharge();

                    if (Result == 1)
                    {
                        lblmsg.ForeColor = Color.Green;
                        lblmsg.Text = "Other details saved successfully ";

                        //DataSet ds = invBL.GetSumOtherCharges(ddlJobNo.SelectedValue, txtInvoiceNo.Text);
                        //DataRowView row = ds.Tables["Charges"].DefaultView[0];
                        //txtOtherCharges.Text = row["ChargeAmount"].ToString();
                        //txtProductValue.Text = txtProductINRValues.Text;
                    }
                    else
                    {
                        Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "alert('Please the check values again');", true);
                    }
                }

                else
                {
                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "alert('Please Enter the Invoice No');", true);
                }
            }
            else
            {
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "alert('Please Select job No');", true);
            }
        }

        protected void btnAddOtherCharges_Click(object sender, EventArgs e)
        {
            if (ddlJobNo.SelectedValue != "~Select~")
            {
                if (txtInvoiceNo.Text != "")
                {
                    double ExchangeRate = Convert.ToDouble(txtRate.Text);
                    double amount = Convert.ToDouble(txtAmount.Text);
                    double amountinr=Convert.ToDouble(txtAmountINR.Text);

                    int Result = invBL.InsertOthers(ddlChargeType.SelectedValue, ddlChargeCurrency.SelectedValue, ExchangeRate, amount, ddlJobNo.SelectedValue, txtInvoiceNo.Text, (string)Session["USER-NAME"], DateTime.Now, amountinr);
                    BindGridCharge();
                    if (Result == 1)
                    {
                        lblmsg.ForeColor = Color.Green;
                        lblmsg.Text = "Other details saved successfully ";
                        // Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "alert('Other details successfully saved');", true);
                    }
                    else
                    {
                        Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "alert('Please the check values again');", true);
                    }
                }

                else
                {
                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "alert('Please Enter the Invoice No');", true);
                }
            }

            else
            {
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "alert('Please Select job No');", true);
            }
        }

        public void OtherChargesFooter()
        {
            try
            {
                double TotalAmt = 0;
                double TotalAmtINR = 0;
                int i = 0;
                foreach (GridViewRow gv in gvOtherCharges.Rows)
                {
                    string amt = gvOtherCharges.Rows[i].Cells[6].Text;
                    string amt1 = gvOtherCharges.Rows[i].Cells[7].Text;
                    TotalAmt = TotalAmt + Convert.ToDouble(amt);
                    TotalAmtINR = TotalAmtINR + Convert.ToDouble(amt1);
                    i++;
                }
                gvOtherCharges.FooterRow.Cells[4].Text = "Total Amount ";
                gvOtherCharges.FooterRow.Cells[5].Text = Convert.ToString(TotalAmt);
                gvOtherCharges.FooterRow.Cells[6].Text = Convert.ToString(TotalAmtINR);
                //txtMiscelTotalAmount.Text = Convert.ToString(TotalAmtINR);
                lblOtherCharges.Text = Convert.ToString(TotalAmtINR);
            }
            catch (Exception ex)
            {

            }
        }

        public void SVBRefDetails()
        {
            try
            {
                ClearRelation();
                string Consignor = txtConsignorName.Text;
                string Consignee = lblImporter.Text;
                DataSet ds = invBL.GetSVBDetails(Consignee, Consignor);
                if (ds.Tables["SVB"].Rows.Count != 0)
                {
                    DataRowView row = ds.Tables["SVB"].DefaultView[0];
                    chkBuyer.Checked = Convert.ToBoolean(row["BuyerSellerRelated"].ToString());
                    txtRelation.Text = row["Relation"].ToString();
                    txtRelationBase.Text = row["Base"].ToString();
                    txtRelationCondition.Text = row["Condition"].ToString();
                    chkSVB.Checked = Convert.ToBoolean(row["SVBLoad"].ToString());
                    txtSVBRelation.Text = row["SVBRefOn"].ToString();
                    txtSVBDate.Text = row["SVBRefDate"].ToString();
                    txtCustomHouse.Text = row["CustomHouse"].ToString();
                    if (row["LoadingOn"].ToString() == "Assessable")
                    {
                        ddlLoadingOn.SelectedValue = "A";
                    }
                    else if (row["LoadingOn"].ToString() == "Duty")
                    {
                        ddlLoadingOn.SelectedValue = "D";
                    }
                    else if (row["LoadingOn"].ToString() == "Assessable &amp; Duty")
                    {
                        ddlLoadingOn.SelectedValue = "B";
                    }
                    else
                    {
                        ddlLoadingOn.SelectedValue = "~Select~";
                    }
                    //ddlLoadingOn.SelectedValue = row["LoadingOn"].ToString();
                    txtLoadingRateAssbl.Text = row["LoadingRateAssb"].ToString();
                    if (row["LoadingRateAssbStatus"].ToString() == "Provisional")
                    {
                        ddlLoadingAssblStatus.SelectedValue = "P";
                    }
                    else if (row["LoadingRateAssbStatus"].ToString() == "Final")
                    {
                        ddlLoadingAssblStatus.SelectedValue = "F";
                    }
                    else
                    {
                        ddlLoadingAssblStatus.SelectedValue = "~Select~";
                    }
                  //  ddlLoadingAssblStatus.SelectedValue = row["LoadingRateAssbStatus"].ToString();
                    txtLoadingDuty.Text = row["LoadingRateDuty"].ToString();
                    if (row["LoadingRateDutyStatus"].ToString() == "Provisional")
                    {
                        ddlLoadingDutyStatus.SelectedValue = "P";
                    }
                    else if (row["LoadingRateDutyStatus"].ToString() == "Final")
                    {
                        ddlLoadingDutyStatus.SelectedValue = "F";
                    }
                    else
                    {
                        ddlLoadingDutyStatus.SelectedValue = "~Select~";
                    }
                }
              
            }
            catch (Exception ex)
            {
            }

           
        }

        protected void btnothDelete_Click(object sender, ImageClickEventArgs e)
        {
            ImageButton btnothDelete = sender as ImageButton;
            GridViewRow row = (GridViewRow)btnothDelete.NamingContainer;
            string TransId = row.Cells[2].Text;
            int i = invBL.DeleteOtherCharges(TransId);
            if (i == 1)
            {
                BindGridCharge();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "redirect", "alert('Deleted Successfully');", true);
            }
        }

        protected void btnSVBCheck_Click(object sender, EventArgs e)
        {
            SVBRefDetails();
        }

        protected void btnConsCheck_Click(object sender, EventArgs e)
        {

            try
            {
                DataSet ds = obj1.GetJobImportShipment(ddlJobNo.SelectedValue);
                if (ds.Tables["Table"].Rows.Count != 0)
                {
                    DataRowView row = ds.Tables["Table"].DefaultView[0];
                    txtConsignorName.Text = Convert.ToString(row["Consignor"]);
                    txtConsignor.Text = Convert.ToString(row["ConsignorAddress"]);

                    ddlConsignorCountry.SelectedValue = Convert.ToString(row["ConsignorCountry"]).ToUpper();
                }
            }
            catch
            {
            }
        }

       }
}