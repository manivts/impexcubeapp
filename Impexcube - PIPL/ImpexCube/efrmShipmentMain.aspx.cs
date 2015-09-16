using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using System.Drawing;
using VTS.ImpexCube.Data;
using VTS.ImpexCube.Business;

namespace ImpexCube
{
    public partial class efrmShipmentMain : System.Web.UI.Page
    {
        VTS.ImpexCube.Business.ETShipmentBL objETShipment = new VTS.ImpexCube.Business.ETShipmentBL();
        CommonDL objCommonDL = new CommonDL();
        string strcon = (string)ConfigurationManager.ConnectionStrings["Constr"].ConnectionString;
        int result;
        string ExJobNo = string.Empty;
        EMJobCreationBL objEMJobCreation = new EMJobCreationBL();
        string Mode = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {            
            if (!IsPostBack)
            {
                if (Request.QueryString["Mode"] == "" || Request.QueryString["Mode"] == null)
                {
                    ExJobNo = (string)Session["ExJobNo"];
                }
                else if(Request.QueryString["Mode"] == "Direct")
                {
                    ExJobNo = string.Empty;
                }
                MultiView1.ActiveViewIndex = 0;
                JobNo();
                GetStateName();
                Unit();
                FillDropDown();
                GetMode();                
                if (ExJobNo == "" || ExJobNo == null || ExJobNo == "~Select~")
                {
                    btnUpdate.Visible = false;
                    btnSave1.Visible = false;
                    btnSave2.Visible = false;
                    btnSave3.Visible = false;
                }
                else
                {
                    GetJobDetails();
                }
            }
        }

        private void FillDropDown()
        {
            string Query = "SELECT CountryName,CountryCode FROM M_Country";
            DataSet ds = objCommonDL.GetDataSet(Query);
            ddlDesCountry.DataSource = ds;
            ddlDesCountry.DataTextField = "CountryName";
            ddlDesCountry.DataValueField = "CountryCode";
            ddlDesCountry.DataBind();

            ddlDisCountry.DataSource = ds;
            ddlDisCountry.DataTextField = "CountryName";
            ddlDisCountry.DataValueField = "CountryCode";
            ddlDisCountry.DataBind();
        }

        public void GetMode()
        {
            if (lblMode.Text == "Sea")
            {
                lblAirlineVoyage.Text = "Voyage No.";
                txtAirlineCode.Visible = false;
                txtAirline.Visible = false;
                txtVoyageNo.Visible = true;

                lblShippingFlightNo.Text = "Shipping Line";
                txtShippingLine.Visible = true;
                txtAirline.Visible = false;
                txtAirlineCode.Visible = false;

                txtFlightNo.Visible = false;
                txtFlightDate.Visible = false;

                lblVessel.Visible = true;
                txtVesselDate.Visible = true;
                txtSailingDate.Visible = true;

                lblMBLNo.Text = "MBL No/Date";
                lblHBLNo.Text = "HBL No/Date";

                lblNoofContainer.Text = "No.Of Containers";

            }
            else if (lblMode.Text == "Air")
            {
                lblAirlineVoyage.Text = "Airline";
                txtAirlineCode.Visible = true;
                txtAirline.Visible = true;
                txtVoyageNo.Visible = false;

                lblShippingFlightNo.Text = "Flight No/Date";
                txtShippingLine.Visible = false;
                txtAirline.Visible = true;
                txtAirlineCode.Visible = true;

                txtFlightNo.Visible = true;
                txtFlightDate.Visible = true;

                lblVessel.Visible = false;
                txtVesselDate.Visible = false;
                txtSailingDate.Visible = false;

                lblMBLNo.Text = "MAWB No/Date";
                lblHBLNo.Text = "HAWB No/Date";

                lblNoofContainer.Text = "Pkts in MAWB";
            }
        }

        private void GetJobDetails()
        {            
            ddlJobnoshipmain.SelectedValue = ExJobNo;
            DataSet ds = new DataSet();
            string query1 = "select JobNo,DischargeCountry,DischargePort,DestinationCountry,  DestinationPort,  VoyageNo,  ShippingLine,  VesselNo,SailingDate, "+
                " EGMNo,  EGMDate,  MBLNo,  MBLDate,  HBLNo,  HBLDate,  PreCarriageby,  PlaceofReceipt,StateOfOrigin,  AnnexureCDetails,  NatureofCargo,  TotalNoofPkgs, "+
                " TotalNoofPkgsUnit,  LoosePkgs,  NoofContainers,GrossWeight,  GrossWeightUnit,  NetWeight,  NetWeightUnit,  MarksNos,GoodsStuffedAt, SampleAccompanied, "+
                " FactoryAddress, SealType, SealNo, AgencyName,QCertNoDate, ExportTradeControl, TypeofShipment, ExportUnder, SBType, SBHeading, SBbottomarea,BuyersOrderNo, "+
                " BuyersOrderDate, OtherReferences, TermsofDeliveryPayment, OriginCountry, InvoiceHeader,JobDate,TransportMode,CustomHouse,AirlineCode,Airline,FlightNo,FlightDate  " +
                " from View_Export_Shipment where JobNo like '%" + ddlJobnoshipmain.SelectedValue + "%'";
            ds = objCommonDL.GetDataSet(query1);
            if (ds.Tables["Table"].Rows.Count != 0)
            {
                jobbind();
                DataRowView row = ds.Tables["Table"].DefaultView[0];
                if (row["NatureofCargo"] != DBNull.Value)
                {
                    ddlNatureofCorgo.SelectedIndex = ddlNatureofCorgo.Items.IndexOf(ddlNatureofCorgo.Items.FindByText(row["NatureofCargo"].ToString()));
                }
                lblJobReceivedDate.Text = row["JobDate"].ToString();
                lblCustom.Text = row["CustomHouse"].ToString();
                lblMode.Text = row["TransportMode"].ToString();
                ddlDisCountry.SelectedValue = row["DischargeCountry"].ToString();
                ddlDisPort.SelectedValue = row["DischargePort"].ToString();
                txtpkgs1.Text = row["TotalNoofPkgs"].ToString();
                ddlTotalUnit.SelectedIndex = ddlTotalUnit.Items.IndexOf(ddlTotalUnit.Items.FindByText(row["TotalNoofPkgsUnit"].ToString()));
                ddlDesCountry.SelectedValue = row["DestinationCountry"].ToString();
                txtLoosePkgs.Text = row["LoosePkgs"].ToString();
                ddlDesPort.SelectedValue = row["DestinationPort"].ToString();
                txtNoofContainer.Text = row["NoofContainers"].ToString();
                txtVoyageNo.Text = row["VoyageNo"].ToString();
                txtShippingLine.Text = row["ShippingLine"].ToString();
                txtGrossWeight1.Text = row["GrossWeight"].ToString();
                ddlGrossUnit.SelectedIndex = ddlGrossUnit.Items.IndexOf(ddlGrossUnit.Items.FindByText(row["GrossWeightUnit"].ToString()));
                txtVesselDate.Text = row["VesselNo"].ToString();
                txtSailingDate.Text = row["SailingDate"].ToString();
                txtNetWeight1.Text = row["NetWeight"].ToString();
                ddlNetUnit.SelectedIndex = ddlNetUnit.Items.IndexOf(ddlNetUnit.Items.FindByText(row["NetWeightUnit"].ToString()));
                txtEGMNO.Text = row["EGMNo"].ToString();
                txtEGMDate.Text = row["EGMDate"].ToString();
                txtMarksnos.Text = row["MarksNos"].ToString();
                txtMBLNO.Text = row["MBLNo"].ToString();
                txtMBLDate.Text = row["MBLDate"].ToString();
                txtHBLNo.Text = row["HBLNo"].ToString();
                txtHBLDate.Text = row["HBLDate"].ToString();
                txtPreCarriage.Text = row["PreCarriageby"].ToString();
                txtPlcereceipt.Text = row["PlaceofReceipt"].ToString();
                if (row["StateOfOrigin"] != DBNull.Value)
                {
                    ddlStateofOrigin.SelectedIndex = ddlStateofOrigin.Items.IndexOf(ddlStateofOrigin.Items.FindByText(row["StateOfOrigin"].ToString()));
                }
                bool chkannex = Convert.ToBoolean(row["AnnexureCDetails"]);
                chkAnnexture.Checked = chkannex;
                if (row["GoodsStuffedAt"] != DBNull.Value)
                {
                    ddlGoodsStuffedat.SelectedIndex = ddlGoodsStuffedat.Items.IndexOf(ddlGoodsStuffedat.Items.FindByText(row["GoodsStuffedAt"].ToString()));
                }
                chkSampleAccom.Checked = false;
                if (row["SampleAccompanied"] != DBNull.Value)
                {
                    bool chksample = Convert.ToBoolean(row["SampleAccompanied"]);
                    chkSampleAccom.Checked = chksample;
                }                
                txtFactoryAddress.Text = row["FactoryAddress"].ToString();
                if (row["SealType"] != DBNull.Value)
                {
                    ddlSealType.SelectedIndex = ddlSealType.Items.IndexOf(ddlSealType.Items.FindByText(row["SealType"].ToString()));
                }
                txtSealNo.Text = row["SealNo"].ToString();
                txtAgencyName.Text = row["AgencyName"].ToString();                
                txtCertnoDate.Text = row["QCertNoDate"].ToString();
                txtExportControl.Text = row["ExportTradeControl"].ToString();
                if (row["TypeofShipment"] != DBNull.Value)
                {
                    ddlTypefShipment.SelectedIndex = ddlTypefShipment.Items.IndexOf(ddlTypefShipment.Items.FindByText(row["TypeofShipment"].ToString()));
                }
                if (row["ExportUnder"] != DBNull.Value)
                {
                    ddlExportUnder.SelectedIndex = ddlExportUnder.Items.IndexOf(ddlExportUnder.Items.FindByText(row["ExportUnder"].ToString()));
                }
                if (row["SBType"] != DBNull.Value)
                {
                    ddlSBtype.SelectedIndex = ddlSBtype.Items.IndexOf(ddlSBtype.Items.FindByText(row["SBType"].ToString()));
                }
                txtSBHeading.Text = row["SBHeading"].ToString();
                txtSBBottomArea.Text = row["SBbottomarea"].ToString();               
                txtbuyerorder1.Text = row["BuyersOrderNo"].ToString();
                txtbuyerorder2.Text = row["BuyersOrderDate"].ToString();
                txtOtherreferences.Text = row["OtherReferences"].ToString();
                txtDeliveryPayment.Text = row["TermsofDeliveryPayment"].ToString();
                txtOriginCountry.Text = row["OriginCountry"].ToString();
                txtInvoiceHeader.Text = row["InvoiceHeader"].ToString();

                txtAirline.Text = row["Airline"].ToString();
                txtAirlineCode.Text = row["AirlineCode"].ToString();
                txtFlightNo.Text = row["FlightNo"].ToString();
                txtFlightDate.Text = row["FlightDate"].ToString();

                btnUpdate.Visible = true;                                
                btnSave.Visible = false;
                btnUpdatestuff.Visible = true;
                btnUpdateInvPrint.Visible = true;
                btnUpdateShipbillprint.Visible = true;
                btnSave1.Visible = false;
                btnSave2.Visible = false;
                btnSave3.Visible = false;
                GetContainerDetails();
            }
            else
            {
                DataSet exds = new DataSet();
                exds = objEMJobCreation.GetData(ddlJobnoshipmain.SelectedValue);
                if (exds.Tables[0].Rows.Count != 0)
                {
                    btnUpdate.Visible = true;
                    btnSave.Visible = false;
                    DataRowView rw = exds.Tables[0].DefaultView[0];
                    lblJobReceivedDate.Text = rw["JobDate"].ToString();
                    lblMode.Text = rw["TransportMode"].ToString();
                    lblCustom.Text = rw["CustomHouse"].ToString();
                }
                MultiView1.ActiveViewIndex = 0;
                btnSave.Visible = true;
                btnUpdate.Visible = false;
                btnUpdatestuff.Visible = true;
                btnUpdateInvPrint.Visible = true;
                btnUpdateShipbillprint.Visible = true;
                btnSave1.Visible = false;
                btnSave2.Visible = false;
                btnSave3.Visible = false;
            }

        }

        public void Unit()
        {
            DataSet dt = objCommonDL.GetUnit();
            ddlGrossUnit.DataSource = dt;
            ddlGrossUnit.DataValueField = "UnitShort";
            ddlGrossUnit.DataTextField = "UnitShort";
            ddlGrossUnit.DataBind();
            ddlGrossUnit.Items.Insert(0, new ListItem("~Select~", "0"));

            ddlNetUnit.DataSource = dt;
            ddlNetUnit.DataValueField = "UnitShort";
            ddlNetUnit.DataTextField = "UnitShort";
            ddlNetUnit.DataBind();
            ddlNetUnit.Items.Insert(0, new ListItem("~Select~", "0"));

            ddlTotalUnit.DataSource = dt;
            ddlTotalUnit.DataValueField = "UnitShort";
            ddlTotalUnit.DataTextField = "UnitShort";
            ddlTotalUnit.DataBind();
            ddlTotalUnit.Items.Insert(0, new ListItem("~Select~", "0"));
        }

        public void GetStateName()
        {
            DataSet ds = objCommonDL.GetState();
            ddlStateofOrigin.DataSource = ds;
            ddlStateofOrigin.DataTextField = "statename";
            ddlStateofOrigin.DataValueField = "statename";
            ddlStateofOrigin.DataBind();
            ddlStateofOrigin.Items.Insert(0, new ListItem("~Select~", "0"));
        }

        protected void btnMain_Click(object sender, EventArgs e)
        {
            MultiView1.ActiveViewIndex = 0;
        }

        protected void btnDuty_Click(object sender, EventArgs e)
        {
            MultiView1.ActiveViewIndex = 1;
        }

        protected void btnGenDesc_Click(object sender, EventArgs e)
        {
            MultiView1.ActiveViewIndex = 2;
        }

        protected void btnITC_Click(object sender, EventArgs e)
        {
            MultiView1.ActiveViewIndex = 3;          
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {            
            //SaveMain
           string JobNo=ddlJobnoshipmain.SelectedValue;
           string DischargeCountry=ddlDisCountry.SelectedValue;
           string NatureofCargo=ddlNatureofCorgo.SelectedValue;
           string DischargePort=ddlDisPort.SelectedValue;
           string TotalNoofPkgs=txtpkgs1.Text;
           string TotalNoofPkgsUnit = ddlTotalUnit.SelectedValue;
           string DestinationCountry=ddlDesCountry.SelectedValue;
           string LoosePkgs=txtLoosePkgs.Text;
           string DestinationPort=ddlDesPort.SelectedValue;
           string NoofContainers=txtNoofContainer.Text;
           string VoyageNo=txtVoyageNo.Text;
           string ShippingLine=txtShippingLine.Text;
           string GrossWeight=txtGrossWeight1.Text;
           string GrossWeightUnit=ddlGrossUnit.SelectedValue;
           string VesselNo=txtVesselDate.Text;
           string SailingDate=txtSailingDate.Text;
           string NetWeight=txtNetWeight1.Text;
           string NetWeightUnit = ddlNetUnit.SelectedValue;
           string EGMNo=txtEGMNO.Text;
           string EGMDate=txtEGMDate.Text;
           string MarksNos=txtMarksnos.Text;
           string MBLNo=txtMBLNO.Text;
           string MBLDate=txtMBLDate.Text;
           string HBLNo=txtHBLNo.Text;
           string HBLDate=txtHBLDate.Text;
           string PreCarriageby=txtPreCarriage.Text;
           string PlaceofReceipt=txtPlcereceipt.Text;
           string StateOfOrigin=ddlStateofOrigin.SelectedValue;
           string AnnexureCDetails = chkAnnexture.Checked.ToString();
           string CreatedBy = (string)Session["USER-NAME"];
           string CreatedDate = System.DateTime.Now.ToString();

           string Airlinecode = txtAirlineCode.Text;
           string Airline = txtAirline.Text;
           string FlightNo = txtFlightNo.Text;
           string FlightDate = txtFlightDate.Text;

           result=objETShipment.SaveShipmentMain(JobNo, DischargeCountry, DischargePort, DestinationCountry, DestinationPort, VoyageNo, ShippingLine, VesselNo,
                      SailingDate,  EGMNo,  EGMDate,  MBLNo,  MBLDate,  HBLNo,  HBLDate,  PreCarriageby,  PlaceofReceipt,
                      StateOfOrigin,  AnnexureCDetails,  NatureofCargo,  TotalNoofPkgs,  TotalNoofPkgsUnit,  LoosePkgs,  NoofContainers,
                      GrossWeight,  GrossWeightUnit,  NetWeight,  NetWeightUnit,  MarksNos,Airlinecode,Airline,FlightNo,FlightDate,  CreatedBy,  CreatedDate);
           if (result == 1)
           {
               btnSave.Visible = false;
               btnUpdate.Visible = true;
               ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Saved successfully');", true);  
           }
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {//update main
           string JobNo=ddlJobnoshipmain.SelectedValue;
           string DischargeCountry=ddlDisCountry.SelectedValue;
           string NatureofCargo=ddlNatureofCorgo.SelectedValue;
           string DischargePort=ddlDisPort.SelectedValue;
           string TotalNoofPkgs=txtpkgs1.Text;
           string TotalNoofPkgsUnit = ddlTotalUnit.SelectedValue;
           string DestinationCountry=ddlDesCountry.SelectedValue;
           string LoosePkgs=txtLoosePkgs.Text;
           string DestinationPort=ddlDesPort.SelectedValue;
           string NoofContainers=txtNoofContainer.Text;
           string VoyageNo=txtVoyageNo.Text;
           string ShippingLine=txtShippingLine.Text;
           string GrossWeight=txtGrossWeight1.Text;
           string GrossWeightUnit = ddlGrossUnit.SelectedValue;
           string VesselNo=txtVesselDate.Text;
           string SailingDate=txtSailingDate.Text;
           string NetWeight=txtNetWeight1.Text;
           string NetWeightUnit = ddlNetUnit.SelectedValue;
           string EGMNo=txtEGMNO.Text;
           string EGMDate=txtEGMDate.Text;
           string MarksNos=txtMarksnos.Text;
           string MBLNo=txtMBLNO.Text;
           string MBLDate=txtMBLDate.Text;
           string HBLNo=txtHBLNo.Text;
           string HBLDate=txtHBLDate.Text;
           string PreCarriageby=txtPreCarriage.Text;
           string PlaceofReceipt=txtPlcereceipt.Text;
           string StateOfOrigin=ddlStateofOrigin.SelectedValue;
           string AnnexureCDetails=chkAnnexture.Checked.ToString();
           string ModifiedBy = (string)Session["USER-NAME"];
           string ModifiedDate = System.DateTime.Now.ToString();

           string Airlinecode = txtAirlineCode.Text;
           string Airline = txtAirline.Text;
           string FlightNo = txtFlightNo.Text;
           string FlightDate = txtFlightDate.Text;

           result=objETShipment.UpdateShipmentMain(JobNo, DischargeCountry, DischargePort, DestinationCountry, DestinationPort, VoyageNo, ShippingLine, VesselNo,
                     SailingDate, EGMNo, EGMDate, MBLNo, MBLDate, HBLNo, HBLDate, PreCarriageby, PlaceofReceipt,
                     StateOfOrigin, AnnexureCDetails, NatureofCargo, TotalNoofPkgs, TotalNoofPkgsUnit, LoosePkgs, NoofContainers,
                     GrossWeight, GrossWeightUnit, NetWeight, NetWeightUnit, MarksNos, Airlinecode, Airline, FlightNo, FlightDate, ModifiedBy, ModifiedDate);
           if (result == 1)
           {
               ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Updated successfully');", true);
           }           
        }

        protected void btnUpdatestuff_Click(object sender, EventArgs e)
        {
            //goods stuffing details
            string JobNo = ddlJobnoshipmain.SelectedValue;
            string GoodsStuffedAt = ddlGoodsStuffedat.SelectedValue;
            string SampleAccompanied = chkSampleAccom.Checked.ToString();
            string FactoryAddress = txtFactoryAddress.Text;
            string SealType = ddlSealType.SelectedValue;
            string SealNo = txtSealNo.Text;
            string AgencyName = txtAgencyName.Text;
            string ModifiedBy = (string)Session["USER-NAME"];
            string ModifiedDate = System.DateTime.Now.ToString();
            result = objETShipment.UpdateShipmentStuffingDetails(JobNo, GoodsStuffedAt, SampleAccompanied, FactoryAddress, SealType, SealNo, AgencyName, ModifiedBy, ModifiedDate);
            if (result == 1)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Saved successfully');", true);  
            }
        }
            
        protected void btnUpdateShipbillprint_Click(object sender, EventArgs e)
        {
            //ShippingBillPrint
            string JobNo=ddlJobnoshipmain.SelectedValue;
            string QCertNoDate=txtCertnoDate.Text;
            string ExportTradeControl=txtExportControl.Text;
            string TypeofShipment=ddlTypefShipment.SelectedValue;
            string ExportUnder=ddlExportUnder.SelectedValue;
            string SBType=ddlSBtype.SelectedValue;
            string SBHeading=txtSBHeading.Text;
            string SBbottomarea=txtSBBottomArea.Text;
            string ModifiedBy = (string)Session["USER-NAME"];
            string ModifiedDate = System.DateTime.Now.ToString();
            result=objETShipment.UpdateShipmentBillPrinting(JobNo, QCertNoDate, ExportTradeControl, TypeofShipment, ExportUnder, SBType, SBHeading, SBbottomarea, ModifiedBy, ModifiedDate);
            if (result == 1)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Saved successfully');", true);  
            }
        }
        
        protected void btnUpdateInvPrint_Click(object sender, EventArgs e)
        {
            //invoice Print
            string JobNo = ddlJobnoshipmain.SelectedValue;
            string BuyersOrderNo=txtbuyerorder1.Text;
           string BuyersOrderDate=txtbuyerorder2.Text;
           string OtherReferences=txtOtherreferences.Text;
           string TermsofDeliveryPayment=txtDeliveryPayment.Text;
           string OriginCountry=txtOriginCountry.Text;
           string InvoiceHeader=txtInvoiceHeader.Text;
           string ModifiedBy = (string)Session["USER-NAME"];
           string ModifiedDate = System.DateTime.Now.ToString();
           result=objETShipment.UpdateShipmentInvoicePrinting(JobNo, BuyersOrderNo, BuyersOrderDate, OtherReferences, TermsofDeliveryPayment, OriginCountry, InvoiceHeader, ModifiedBy, ModifiedDate);
           if (result == 1)
           {
               ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Saved successfully');", true);   
           }           
        }

        protected void ddlJobnoshipmain_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblCustom.Text = string.Empty;
            lblMode.Text = string.Empty;
            DataSet ds = new DataSet();
            string query1 = "select JobNo,DischargeCountry,DischargePort,DestinationCountry,  DestinationPort,  VoyageNo,  ShippingLine,  VesselNo,SailingDate,  EGMNo,  EGMDate,  MBLNo,  MBLDate,  HBLNo,  HBLDate,  PreCarriageby,  PlaceofReceipt,StateOfOrigin,  AnnexureCDetails,  NatureofCargo,  TotalNoofPkgs,  TotalNoofPkgsUnit,  LoosePkgs,  NoofContainers,GrossWeight,  GrossWeightUnit,  NetWeight,  NetWeightUnit,  MarksNos,GoodsStuffedAt, SampleAccompanied, FactoryAddress, SealType, SealNo, AgencyName,QCertNoDate, ExportTradeControl, TypeofShipment, ExportUnder, SBType, SBHeading, SBbottomarea,BuyersOrderNo, BuyersOrderDate, OtherReferences, TermsofDeliveryPayment, OriginCountry, InvoiceHeader, AirlineCode, Airline, FlightNo, FlightDate from E_T_Shipment where JobNo like '%" + ddlJobnoshipmain.SelectedValue + "%'";
            ds = objCommonDL.GetDataSet(query1);
            if (ds.Tables["Table"].Rows.Count != 0)
            {
                jobbind();
                DataRowView row = ds.Tables["Table"].DefaultView[0];
                if (row["NatureofCargo"] != DBNull.Value)
                {
                    ddlNatureofCorgo.SelectedIndex = ddlNatureofCorgo.Items.IndexOf(ddlNatureofCorgo.Items.FindByText(row["NatureofCargo"].ToString()));
                }
                if (row["DischargeCountry"] != DBNull.Value)
                {
                    ddlDisCountry.SelectedValue = row["DischargeCountry"].ToString();
                }
                if (row["DischargePort"] != DBNull.Value)
                {
                    ddlDisCountry_SelectedIndexChanged(sender, e);
                    ddlDisPort.SelectedValue = row["DischargePort"].ToString();
                }
                txtpkgs1.Text = row["TotalNoofPkgs"].ToString();
                ddlTotalUnit.SelectedIndex = ddlTotalUnit.Items.IndexOf(ddlTotalUnit.Items.FindByText(row["TotalNoofPkgsUnit"].ToString()));
                if (row["DestinationCountry"] != DBNull.Value)
                {
                    ddlDesCountry.SelectedValue = row["DestinationCountry"].ToString();
                }
                txtLoosePkgs.Text = row["LoosePkgs"].ToString();
                if (row["DestinationPort"] != DBNull.Value)
                {
                    ddlDesCountry_SelectedIndexChanged(sender, e);
                    ddlDesPort.SelectedValue = row["DestinationPort"].ToString();
                }
                txtNoofContainer.Text = row["NoofContainers"].ToString();
                txtVoyageNo.Text = row["VoyageNo"].ToString();
                txtShippingLine.Text = row["ShippingLine"].ToString();
                txtGrossWeight1.Text = row["GrossWeight"].ToString();
                ddlGrossUnit.SelectedIndex = ddlGrossUnit.Items.IndexOf(ddlGrossUnit.Items.FindByText(row["GrossWeightUnit"].ToString()));
                txtVesselDate.Text = row["VesselNo"].ToString();
                txtSailingDate.Text = row["SailingDate"].ToString();
                txtNetWeight1.Text = row["NetWeight"].ToString();
                ddlNetUnit.SelectedIndex = ddlNetUnit.Items.IndexOf(ddlNetUnit.Items.FindByText(row["NetWeightUnit"].ToString()));
                txtEGMNO.Text = row["EGMNo"].ToString();
                txtEGMDate.Text = row["EGMDate"].ToString();
                txtMarksnos.Text = row["MarksNos"].ToString();
                txtMBLNO.Text = row["MBLNo"].ToString();
                txtMBLDate.Text = row["MBLDate"].ToString();
                txtHBLNo.Text = row["HBLNo"].ToString();
                txtHBLDate.Text = row["HBLDate"].ToString();
                txtPreCarriage.Text = row["PreCarriageby"].ToString();
                txtPlcereceipt.Text = row["PlaceofReceipt"].ToString();
                if (row["StateOfOrigin"] != DBNull.Value)
                {
                    ddlStateofOrigin.SelectedIndex = ddlStateofOrigin.Items.IndexOf(ddlStateofOrigin.Items.FindByText(row["StateOfOrigin"].ToString()));
                }
                bool chkannex = Convert.ToBoolean(row["AnnexureCDetails"]);
                chkAnnexture.Checked = chkannex;
                if (row["GoodsStuffedAt"] != DBNull.Value)
                {
                    ddlGoodsStuffedat.SelectedIndex = ddlGoodsStuffedat.Items.IndexOf(ddlGoodsStuffedat.Items.FindByText(row["GoodsStuffedAt"].ToString()));
                }
                chkSampleAccom.Checked = false;
                if (row["SampleAccompanied"] != DBNull.Value)
                {
                    bool chksample = Convert.ToBoolean(row["SampleAccompanied"]);
                    chkSampleAccom.Checked = chksample;
                }                
                txtFactoryAddress.Text = row["FactoryAddress"].ToString();
                if (row["SealType"] != DBNull.Value)
                {
                    ddlSealType.SelectedIndex = ddlSealType.Items.IndexOf(ddlSealType.Items.FindByText(row["SealType"].ToString()));
                }
                txtSealNo.Text = row["SealNo"].ToString();
                txtAgencyName.Text = row["AgencyName"].ToString();                
                txtCertnoDate.Text = row["QCertNoDate"].ToString();
                txtExportControl.Text = row["ExportTradeControl"].ToString();
                if (row["TypeofShipment"] != DBNull.Value)
                {
                    ddlTypefShipment.SelectedIndex = ddlTypefShipment.Items.IndexOf(ddlTypefShipment.Items.FindByText(row["TypeofShipment"].ToString()));
                }
                if (row["ExportUnder"] != DBNull.Value)
                {
                    ddlExportUnder.SelectedIndex = ddlExportUnder.Items.IndexOf(ddlExportUnder.Items.FindByText(row["ExportUnder"].ToString()));
                }
                if (row["SBType"] != DBNull.Value)
                {
                    ddlSBtype.SelectedIndex = ddlSBtype.Items.IndexOf(ddlSBtype.Items.FindByText(row["SBType"].ToString()));
                }
                txtSBHeading.Text = row["SBHeading"].ToString();
                txtSBBottomArea.Text = row["SBbottomarea"].ToString();               
                txtbuyerorder1.Text = row["BuyersOrderNo"].ToString();
                txtbuyerorder2.Text = row["BuyersOrderDate"].ToString();
                txtOtherreferences.Text = row["OtherReferences"].ToString();
                txtDeliveryPayment.Text = row["TermsofDeliveryPayment"].ToString();
                txtOriginCountry.Text = row["OriginCountry"].ToString();
                txtInvoiceHeader.Text = row["InvoiceHeader"].ToString();
                txtAirline.Text = row["Airline"].ToString();
                txtAirlineCode.Text = row["AirlineCode"].ToString();
                txtFlightNo.Text = row["FlightNo"].ToString();
                txtFlightDate.Text = row["FlightDate"].ToString();

                btnUpdate.Visible = true;                                
                btnSave.Visible = false;
                btnUpdatestuff.Visible = true;
                btnUpdateInvPrint.Visible = true;
                btnUpdateShipbillprint.Visible = true;
                btnSave1.Visible = false;
                btnSave2.Visible = false;
                btnSave3.Visible = false;
                GetMode();
                GetContainerDetails();
            }
            else
            {
                jobbind();
                Clear();
                clearGS();
                clearSP();
                clearIP();
                MultiView1.ActiveViewIndex = 0;
                btnSave.Visible = true;
                btnUpdate.Visible = false;
                btnUpdatestuff.Visible=false;
                btnUpdateInvPrint.Visible = false;
                btnUpdateShipbillprint.Visible = false;
                btnSave1.Visible = true;
                btnSave2.Visible = true;
                btnSave3.Visible = true;
                GetMode();
                GetContainerDetails();
            }
        }

        private void GetContainerDetails()
        {
            DataSet ds = new DataSet();
            ds = objETShipment.SelectContainer(ddlJobnoshipmain.SelectedValue);
            if (ds.Tables["Table"].Rows.Count != 0)
            {
                gvContainerDetails.DataSource = ds;
                gvContainerDetails.DataBind();
            }
            else
            {
                gvContainerDetails.DataSource = null;
                gvContainerDetails.DataBind();
            }
        }
        
        public void JobNo()
        {
            string quer = "select * from E_M_JobCreation";            
            DataSet ds = new DataSet();
            ds = objCommonDL.GetDataSet(quer);
            if (ds.Tables["Table"].Rows.Count != 0)
            {
                ddlJobnoshipmain.DataSource = ds;
                ddlJobnoshipmain.DataTextField = "JobNo";
                ddlJobnoshipmain.DataValueField = "JobNo";
                ddlJobnoshipmain.DataBind();
            }
        }

        public void jobbind()
        {

            string query1 = "select JobNo,JobDate,TransportMode,CustomHouse from E_M_JobCreation where JobNo like '%" + ddlJobnoshipmain.SelectedValue + "%'";            
            DataSet ds = new DataSet();
            ds = objCommonDL.GetDataSet(query1);
            if (ds.Tables["Table"].Rows.Count != 0)
            {
                DataRowView row = ds.Tables["Table"].DefaultView[0];
                ddlJobnoshipmain.SelectedValue = row["JobNo"].ToString();
                lblJobReceivedDate.Text = row["JobDate"].ToString();
                lblCustom.Text = row["CustomHouse"].ToString();
                lblMode.Text = row["TransportMode"].ToString();
            }
    
        }

        public void Clear()
        { 
            ddlNatureofCorgo.SelectedValue = "0";
                txtpkgs1.Text =  "";
                ddlTotalUnit.SelectedValue = "0";
                txtLoosePkgs.Text =  "";
                txtNoofContainer.Text =  "";
                txtVoyageNo.Text =  "";
                txtShippingLine.Text =  "";
                txtGrossWeight1.Text = "";
                ddlGrossUnit.SelectedValue = "0";
                txtVesselDate.Text =  "";
                txtSailingDate.Text =  "";
                txtNetWeight1.Text =  "";
                ddlNetUnit.SelectedValue = "0";
                txtEGMNO.Text =  "";
                txtEGMDate.Text =  "";
                txtMarksnos.Text = "";
                txtMBLNO.Text =  "";
                txtMBLDate.Text =  "";
                txtHBLNo.Text =  "";
                txtHBLDate.Text =  "";
                txtPreCarriage.Text =  "";
                txtPlcereceipt.Text =  "";
                chkAnnexture.Checked = false;
            ddlStateofOrigin.SelectedValue = "0";

            txtAirline.Text = "";
            txtAirlineCode.Text = "";
            txtFlightNo.Text = "";
            txtFlightDate.Text = "";
        }

        private void clearGS()
        {
                ddlGoodsStuffedat.SelectedValue = "0";                
                txtFactoryAddress.Text =  "";
                ddlSealType.SelectedValue = "0";
                txtSealNo.Text =  "";
                chkSampleAccom.Checked = false;
                txtAgencyName.Text =  ""; 
        }

        private void clearSP()
        {
                txtCertnoDate.Text =  "";
                txtExportControl.Text =  "";
                ddlTypefShipment.SelectedValue = "0";
                ddlExportUnder.SelectedValue = "0";
                ddlSBtype.SelectedValue = "0";
                txtSBHeading.Text =  "";
                txtSBBottomArea.Text =  "";   
        }

        private void clearIP()
        {
                txtbuyerorder1.Text =  "";
                txtbuyerorder2.Text =  "";
                txtOtherreferences.Text = "";
                txtDeliveryPayment.Text =  "";
                txtOriginCountry.Text =  "";
                txtInvoiceHeader.Text = "";
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Session["ExJobNo"] = string.Empty;
            Response.Redirect("~/efrmShipmentMain.aspx");
        }

        protected void btnCancel1_Click(object sender, EventArgs e)
        {
            Session["ExJobNo"] = string.Empty;
            Response.Redirect("~/efrmShipmentMain.aspx");
        }

        protected void btnCancel2_Click(object sender, EventArgs e)
        {
            Session["ExJobNo"] = string.Empty;
            Response.Redirect("~/efrmShipmentMain.aspx");
        }

        protected void btnCancel3_Click(object sender, EventArgs e)
        {
            Session["ExJobNo"] = string.Empty;
            Response.Redirect("~/efrmShipmentMain.aspx");
        }

        protected void btnClose3_Click(object sender, EventArgs e)
        {
            Session["ExJobNo"] = string.Empty;
            Response.Redirect("~/HomePage.aspx");
        }

        protected void btnClose2_Click(object sender, EventArgs e)
        {
            Session["ExJobNo"] = string.Empty;
            Response.Redirect("~/HomePage.aspx");
        }

        protected void btnClose1_Click(object sender, EventArgs e)
        {
            Session["ExJobNo"] = string.Empty;
            Response.Redirect("~/HomePage.aspx");
        }

        protected void btnClose_Click(object sender, EventArgs e)
        {
            Session["ExJobNo"] = string.Empty;
            Response.Redirect("~/HomePage.aspx");
        }

        protected void btnReturn_Click(object sender, EventArgs e)
        {
            Session["ExJobNo"] = ddlJobnoshipmain.SelectedValue;
            Response.Redirect("efrmJobCreation.aspx");
        }

        protected void btnForward_Click(object sender, EventArgs e)
        {
            Session["ExJobNo"] = ddlJobnoshipmain.SelectedValue;
            Response.Redirect("efrmInvoiceExport.aspx");
        }

        protected void btnSave3_Click(object sender, EventArgs e)
        {

        }

        protected void btnSave1_Click(object sender, EventArgs e)
        {

        }

        protected void ddlDisCountry_SelectedIndexChanged(object sender, EventArgs e)
        {
            string Query = "SELECT PortId, PortCode, PortName, CountryCode, UneceCode FROM M_Port where countrycode='" + ddlDisCountry.SelectedValue + "'";
            DataSet ds = objCommonDL.GetDataSet(Query);
            if (ds.Tables["Table"].Rows.Count != 0)
            {
                ddlDisPort.Items.Clear();
                ddlDisPort.DataSource = ds;
                ddlDisPort.DataTextField = "PortName";
                ddlDisPort.DataValueField = "PortCode";
                ddlDisPort.DataBind();
                ddlDisPort.Items.Insert(0, new ListItem("~Select~", "0"));
            }
            else
            {
                ddlDisPort.Items.Clear();
                ddlDisPort.DataSource = null;
                ddlDisPort.DataBind();
            }
        }

        protected void ddlDesCountry_SelectedIndexChanged(object sender, EventArgs e)
        {
            string Query = "SELECT PortId, PortCode, PortName, CountryCode, UneceCode FROM M_Port where countrycode='" + ddlDesCountry.SelectedValue + "'";
            DataSet ds = objCommonDL.GetDataSet(Query);
            if (ds.Tables["Table"].Rows.Count != 0)
            {
                ddlDesPort.Items.Clear();
                ddlDesPort.DataSource = ds;
                ddlDesPort.DataTextField = "PortName";
                ddlDesPort.DataValueField = "PortCode";
                ddlDesPort.DataBind();
                ddlDesPort.Items.Insert(0, new ListItem("~Select~", "0"));
            }
            else
            {
                ddlDesPort.Items.Clear();
                ddlDesPort.DataSource = null;
                ddlDesPort.DataBind();
            }
        }

        protected void btnContainerDetails_Click(object sender, EventArgs e)
        {
            MultiView1.ActiveViewIndex = 4;
        }

        protected void gvContainerDetails_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (gvContainerDetails.SelectedRow.Cells[1].Text != "")
            {
                btnAddd.Visible = false;
                btnContainerUpdate.Visible = true;
                Session["TransID"] = gvContainerDetails.SelectedRow.Cells[1].Text;
                txtContainerNo.Text = gvContainerDetails.SelectedRow.Cells[3].Text;
                txtContainerSeal.Text = gvContainerDetails.SelectedRow.Cells[4].Text;
                txtSealDate.Text = gvContainerDetails.SelectedRow.Cells[5].Text;
                txtSize.Text = gvContainerDetails.SelectedRow.Cells[6].Text;
                txtType.Text = gvContainerDetails.SelectedRow.Cells[7].Text;
                txtPacketStuffed.Text = gvContainerDetails.SelectedRow.Cells[8].Text;
                txtTransporter.Text = gvContainerDetails.SelectedRow.Cells[9].Text;
            }
        }

        protected void btnAddd_Click(object sender, EventArgs e)
        {
            int Result = 0;
            string jobno = ddlJobnoshipmain.SelectedValue;
            string containerno = txtContainerNo.Text;
            string sealno = txtContainerSeal.Text;
            string sealdate = txtSealDate.Text;
            string size = txtSize.Text;
            string type = txtType.Text;
            string packets = txtPacketStuffed.Text;
            string transporter = txtTransporter.Text;
            try
            {
                if (containerno != "")
                {
                    Result = objETShipment.SaveContainer(jobno, containerno, sealno, sealdate, size, type, packets, transporter,
                        (string)Session["USER-NAME"], DateTime.Now.ToString(), (string)Session["USER-NAME"], DateTime.Now.ToString());

                    if (Result == 1)
                    {
                        GetContainerDetails();
                        ClearContainerDetails();
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Saved Successfully');", true);
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Please enter the container no');", true);
                }
            }
            catch(Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('" + ex.Message + "');", true);  
            }
        }

        private void ClearContainerDetails()
        {
            txtContainerNo.Text = txtContainerSeal.Text = txtSealDate.Text = txtSize.Text = txtType.Text = txtPacketStuffed.Text = txtTransporter.Text = string.Empty;            
        }

        protected void btnContainerUpdate_Click(object sender, EventArgs e)
        {
            int Result = 0;
            string Id = (string)Session["TransID"];
            string jobno = ddlJobnoshipmain.SelectedValue;
            string containerno = txtContainerNo.Text;
            string sealno = txtContainerSeal.Text;
            string sealdate = txtSealDate.Text;
            string size = txtSize.Text;
            string type = txtType.Text;
            string packets = txtPacketStuffed.Text;
            string transporter = txtTransporter.Text;
            try
            {
                Result = objETShipment.UpdateContainer(Id,jobno, containerno, sealno, sealdate, size, type, packets, transporter,
                    (string)Session["USER-NAME"], DateTime.Now.ToString());

                if (Result == 1)
                {
                    btnAddd.Visible = true;
                    btnContainerUpdate.Visible = false;
                    GetContainerDetails();
                    ClearContainerDetails();
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Updated Successfully');", true);
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('" + ex.Message + "');", true);
            }
        }

        protected void btnMainCancel_Click(object sender, EventArgs e)
        {
            btnAddd.Visible = true;
            btnContainerUpdate.Visible = false;
            ClearContainerDetails();            
        }            
    }
}