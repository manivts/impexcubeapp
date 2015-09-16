using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Collections.Generic;
using System.Data.SqlClient;
using VTS.ImpexCube.Data;

namespace ImpexCube
{
    public partial class frmShipment : System.Web.UI.Page
    {
        VTS.ImpexCube.Business.ShipmentBL objShipment = new  VTS.ImpexCube.Business.ShipmentBL();
        VTS.ImpexCube.Business.CommonBL obj1 = new VTS.ImpexCube.Business.CommonBL();
        VTS.ImpexCube.Utlities.Utility joblog = new VTS.ImpexCube.Utlities.Utility();
        CommonDL objCommonDL = new CommonDL();
        Label pagename;
        string strconn = (string)ConfigurationManager.AppSettings["strcon"];

        protected void Page_Load(object sender, EventArgs e)
        {
            Label pagename;
            pagename = (Label)Master.FindControl("lblName");
            pagename.Text = "Shipment";
            if (IsPostBack == false)
            {
                if (Request.QueryString["Mode"] == "Direct")
                {
                    DropJobNo();
                    DropCountry();
                    DropState();
                    Panel2.Visible = false;
                    //DropAgent();
                    //DropCFS();
                    Unit();
                    //DropFFName();
                    DropContainerType();
                    //DropShipingLine();
                }
                else if (Request.QueryString["Mode"] == "Import" || Request.QueryString["JobMode"]!=null)
                {
                    Session["ShipmentID"] = "";
                    Session["mode"] = "";
                    DropJobNo();
                    DropState();
                    //DropShipingLine();
                    //DropAgent();
                    //DropCFS();
                    DropCountry();
                    ddlJobNo.SelectedValue=(string)Session["JobNo"];
                    Unit();
                    History();
                   // DropFFName();
                    pagename = (Label)Master.FindControl("lblName");
                    if (pagename.Text == "Sea Shipment")
                    {
                        Panel2.Visible = true;
                    }
                    else
                    {
                        Panel2.Visible = false;
                    }
                   // Panel2.Visible = false;
                    DropContainerType();
                }
                else
                    Response.Redirect("frmLogin.aspx");
            }
        }

        public void GridLoad()
        {

            DataSet ds = objShipment.GetShipmentDetailsGrid(ddlJobNo.SelectedValue);
            if (ds.Tables["jobno"].Rows.Count != 0)
            {
                  gvShipmentDetails.DataSource = ds;
                  gvShipmentDetails.DataBind();

                  if (lblMode.Text == "Air")
                  {
                      
                      gvShipmentDetails.Columns[2].HeaderText = "AirLine";
                      gvShipmentDetails.Columns[3].HeaderText = "Flight No";
                  }
                  else
                  {
                      gvShipmentDetails.Columns[2].HeaderText = "Vessel Name";
                      gvShipmentDetails.Columns[3].HeaderText = "Shipping Name";

                  }
              }

        }

        public void Unit()
        {
            DataSet dt = obj1.GetUnit();
            ddlGrossWeight.DataSource = dt;
            ddlGrossWeight.DataValueField = "UnitShort";
            ddlGrossWeight.DataTextField = "UnitShort";
            ddlGrossWeight.DataBind();

            ddlPackages.DataSource = dt;
            ddlPackages.DataValueField = "UnitShort";
            ddlPackages.DataTextField = "UnitShort";
            ddlPackages.DataBind();

            ddlNetWeight.DataSource = dt;
            ddlNetWeight.DataValueField = "UnitShort";
            ddlNetWeight.DataTextField = "UnitShort";
            ddlNetWeight.DataBind();
        }

      private void DropJobNo()
        {
            DataSet ds = objShipment.GetJobNo();
            if (ds.Tables["jobno"].Rows.Count != 0)
            {
                ddlJobNo.DataSource = ds;
                ddlJobNo.DataTextField = "JobNo";
                ddlJobNo.DataValueField = "JobNo";
                ddlJobNo.DataBind();
               //ddlJobNo.Items.Insert(0, new ListItem("-Select-", "0"));
            }
            else
            {
                ddlJobNo.DataSource = null;
                ddlJobNo.DataBind();
            }
        }

      private void DropState()
      {
          DataSet ds = objShipment.GetState();
          if (ds.Tables["State"].Rows.Count != 0)
          {
              ddlComState.DataSource = ds;
              ddlComState.DataTextField = "StateName";
              ddlComState.DataValueField = "StateCode";
              ddlComState.DataBind();
             // ddlComState.Items.Insert(0, new ListItem("~Select~", "0"));
          }
          else
          {
              ddlJobNo.DataSource = null;
              ddlJobNo.DataBind();
          }
      }
      private void DropContainerType()
      {
          DataSet ds = objShipment.GetContainerType();
          if (ds.Tables["Container"].Rows.Count != 0)
          {
              ddlContainerType.DataSource = ds;
              ddlContainerType.DataTextField = "Containertype";
              ddlContainerType.DataValueField = "Containertype";
              ddlContainerType.DataBind();
              string Containerdd = ddlContainerType.SelectedValue;
              //'ddlContainerType.Items.Insert(0, new ListItem("-Select-", "0"));
          }
          else
          {
              ddlContainerType.DataSource = null;
              ddlContainerType.DataBind();
          }
      }

      private void DropCountry()
      {
          DataSet ds = objShipment.GetCountry();
          if (ds.Tables["country"].Rows.Count != 0)
          {
              txtCountryOfShipment.DataSource = ds;
              txtCountryOfShipment.DataTextField = "CountryName";
              txtCountryOfShipment.DataValueField = "CountryCode";
              txtCountryOfShipment.DataBind();

              txtCountryOfOrigin.DataSource = ds;
              txtCountryOfOrigin.DataTextField = "CountryName";
              txtCountryOfOrigin.DataValueField = "CountryCode";
              txtCountryOfOrigin.DataBind();
          }
          else
          {
              txtCountryOfShipment.DataSource = null;
              txtCountryOfShipment.DataBind();

              txtCountryOfOrigin.DataSource = null;
              txtCountryOfOrigin.DataBind();
          }
      }
      protected void txtCountryOfShipment_SelectedIndexChanged(object sender, EventArgs e)
      {
          DropPort(txtCountryOfShipment.SelectedValue);
         

      }
      private void DropPort(string shipctry)
      {
          //txtPortofShipment.Enabled = true;
          DataSet ds = objShipment.GetPort(shipctry);
          if (ds.Tables["port"].Rows.Count != 0)
          {
              txtPortofShipment.Items.Clear();
              txtPortofShipment.Items.Insert(0, new ListItem("~Select~", "0"));

              txtPortofShipment.DataSource = ds;
              txtPortofShipment.DataTextField = "PortName";
              txtPortofShipment.DataValueField = "PortCode";
              txtPortofShipment.DataBind();
          }
          else
          {
              txtPortofShipment.DataSource = null;
              txtPortofShipment.DataBind();
          }
      }
      private void GetPortUneceCode(string Country, string portCode)
      {
          DataSet ds = objShipment.GetPort(Country, portCode);
          if (ds.Tables["port"].Rows.Count != 0)
          {
              DataRowView row = ds.Tables["port"].DefaultView[0];
              txtportunececode.Text=row["UneceCode"].ToString();
          }
      }
 
        protected void btnSave_Click(object sender, EventArgs e)
        {
            int result = new int();
            try
            {
                double Tft;
                double Fft;
                if (txt20FtContainer.Text != "")
                {
                    Tft = Convert.ToDouble(txt20FtContainer.Text);
                }
                else
                {
                    Tft = 0;
                }
                if (txt40ftContainer.Text != "")
                {
                    Fft = Convert.ToDouble(txt40ftContainer.Text);
                }
                else
                {
                    Fft = 0;
                }
                string portname = txtPortofShipment.SelectedItem.Text;
                //string[] port=portname.Split('~');
                //portname = port[0].ToString();
                string ShipmentPortUneceCode = txtportunececode.Text; //port[2].ToString();
                string date = DateTime.Now.ToString();
                if ((string)Session["mode"] == "" || (string)Session["mode"] == null)
                {
                    /////////Commercial Tax Insert////////////
                    if (!string.IsNullOrEmpty(txtComTaxNo.Text))
                    {
                        objShipment.InsertCommercialTax(ddlJobNo.SelectedValue, ddlComState.SelectedValue,
                       ddlComState.SelectedItem.Text, ddlComType.SelectedValue, txtComTaxNo.Text,
                       (string)Session["USER-NAME"], date, (string)Session["USER-NAME"], date);
                    }
                   
                    result = objShipment.InsertShipmentDetails(ddlJobNo.SelectedValue, lblJobDate.Text, txtCountryOfShipment.SelectedItem.Text, portname, txtCountryOfOrigin.SelectedItem.Text, txtVesselName.Text, txtVoyageNo.Text,
                        txtTransit.Text, txtETA.Text, txtInwardDate.Text, txtShippingLine.Text, txtLocalIGMNo.Text, txtLocalIGMDate.Text, txtMABLNo.Text, txtMABLDate.Text, txtHABLNo.Text, txtHABLDate.Text, txtGatewayIGMNo.Text, txtGatewayIGMDate.Text, txtLineNo.Text,
                        txtPort.Text, Tft, Fft, txtGrossWeight.Text, ddlGrossWeight.SelectedValue, txtPackages.Text, ddlPackages.SelectedValue, txtSTC.Text, ddlSTC.SelectedValue, txtSTC2.Text, ddlSTC2.SelectedValue,
                        txtCFSName.Text, txtMarksNos.Text, txtNetWeight.Text, ddlNetWeight.SelectedValue, (string)Session["USER-NAME"], date, (string)Session["USER-NAME"], date, txtCountryOfShipment.SelectedValue,
                        txtPortofShipment.SelectedValue, txtCountryOfOrigin.SelectedValue, txtAgentName.Text, txtFFName.Text, ShipmentPortUneceCode);
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Saved Successfully');", true);
                }
                else if ((string)Session["mode"] == "Edit")
                {

                    if (!string.IsNullOrEmpty(txtComTaxNo.Text))
                    {
                        objShipment.UpdateCommercialTax(ddlJobNo.SelectedValue, ddlComState.SelectedValue,
                       ddlComState.SelectedItem.Text, ddlComType.SelectedValue, txtComTaxNo.Text,
                       (string)Session["USER-NAME"], date);
                    }

                    result = objShipment.UpdateShipmentDetails((string)Session["ShipmentID"], ddlJobNo.SelectedValue, lblJobDate.Text, txtCountryOfShipment.SelectedItem.Text, portname, txtCountryOfOrigin.SelectedItem.Text, txtVesselName.Text, txtVoyageNo.Text,
                        txtTransit.Text, txtETA.Text, txtInwardDate.Text, txtShippingLine.Text, txtLocalIGMNo.Text, txtLocalIGMDate.Text, txtMABLNo.Text, txtMABLDate.Text, txtHABLNo.Text, txtHABLDate.Text, txtGatewayIGMNo.Text, txtGatewayIGMDate.Text, txtLineNo.Text,
                        txtPort.Text, Tft, Fft, txtGrossWeight.Text, ddlGrossWeight.SelectedValue, txtPackages.Text, ddlPackages.SelectedValue, txtSTC.Text, ddlSTC.SelectedValue,
                        txtSTC2.Text, ddlSTC2.SelectedValue, txtCFSName.Text, txtMarksNos.Text, txtNetWeight.Text, ddlNetWeight.SelectedValue, (string)Session["USER-NAME"], date,
                        txtCountryOfShipment.SelectedValue, txtPortofShipment.SelectedValue, txtCountryOfOrigin.SelectedValue, txtAgentName.Text, txtFFName.Text, ShipmentPortUneceCode);
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Updated Successfully');", true);
                }
                if (result == 1)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "javascript", "accopen();", true);
                    GridLoad();
                    Clear();
                    Session["mode"] = null;
                    btnSave.Text = "Save";
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('DataBase Error: " + ex.Message + "');", true);
            }
        }

        protected void ddlJobNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            Clear();
            //string jobno =   ddlJobNo.SelectedItem.Text;
            //string username = (string)Session["USER-NAME"];
            //string joblogret = joblog.SelectJobLog(username, jobno);
            //if (joblogret == "NoJob")
            //{
            //    joblog.UpdateJobLog(username);
            //    int jb = joblog.InsertJobLog(username, jobno);
            //    if (jb == 1)
            //    {
                    History();
            //    }
            //}
            //else
            //{
            //    string mess = "This Job can be used  " + joblogret;
            //    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('" + mess + "');", true);
            //}
        }

        public void History()
        {
            try
            {
                if (ddlJobNo.SelectedItem.Text != "~Select~")
                {
                    DataSet ds = objShipment.SelectJobNo(ddlJobNo.SelectedValue);
                    Label pagename;
                    pagename = (Label)Master.FindControl("lblName");
                    if (ds.Tables["jobno"].Rows.Count != 0)
                    {
                        GetJobDetails();
                       // GetShipmentContInfo();
                        if (ds.Tables["jobno"].Rows.Count == 1)
						{
                            DataRowView rv = ds.Tables["jobno"].DefaultView[0];
                            string ShipmentId = rv["TransId"].ToString();//JobNo
                            GetShipmentDetails(ShipmentId);
                            Session["ShipmentID"] = ShipmentId;
                            GetShipmentContInfo();
                            btnSave.Text = "Update";
                        }
                        else
                        {
                            GridLoad();
                        }

                        if (lblMode.Text == "Air")
                        {
                            pagename.Text = "Air Shipment";
                            gvShipmentDetails.Columns[2].HeaderText = "AirLine";
                            gvShipmentDetails.Columns[3].HeaderText = "AirLine";
                            lbl20.Visible = false;
                            lbl40.Visible = false;
                            txt20FtContainer.Visible = false;
                            txt40ftContainer.Visible = false;
                        }
                        else
                        {
                            pagename.Text = "Sea Shipment";
                            gvShipmentDetails.Columns[2].HeaderText = "Vessel Name";
                            gvShipmentDetails.Columns[3].HeaderText = "Shipping Line";
                            lbl20.Visible = true;
                            lbl40.Visible = true;
                            txt20FtContainer.Visible = true;
                            txt40ftContainer.Visible = true;
                        }
                    }
                    else
                    {
                        GetJobDetails();
                        if (lblMode.Text == "Air")
                        {
                            pagename.Text = "Air Shipment";
                            gvShipmentDetails.Columns[2].HeaderText = "AirLine";
                            gvShipmentDetails.Columns[3].HeaderText = "AirLine";
                            lbl20.Visible = false;
                            lbl40.Visible = false;
                            txt20FtContainer.Visible = false;
                            txt40ftContainer.Visible = false;
                        }
                        else
                        {
                            pagename.Text = "Sea Shipment";
                            gvShipmentDetails.Columns[2].HeaderText = "Vessel Name";
                            gvShipmentDetails.Columns[3].HeaderText = "Shipping Line";
                            lbl20.Visible = true;
                            lbl40.Visible = true;
                            txt20FtContainer.Visible = true;
                            txt40ftContainer.Visible = true;
                        }
                        //GetJobSummary();
                    }
                }
              //  Panel2.Visible = false;
            }
            catch(Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('DataBase Error: " + ex.Message + "');", true);
            }
        }

        private void GetShipmentContInfo()
        {
            DataSet ds = objShipment.GetJobShipmentContainerInfo(ddlJobNo.SelectedValue, (string)Session["ShipmentID"]);
            if (ds.Tables["jobno"].Rows.Count != 0)
            {
                gvContainerInfo.DataSource = ds;
                gvContainerInfo.DataBind();
                Panel2.Visible = true;
            }
            else
            {
                gvContainerInfo.DataSource = null;
                gvContainerInfo.DataBind();
            }
        }

        private void GetShipmentDetails(string id)
        {
            try{
            Session["mode"] = "Edit";
            DataSet ds = objShipment.GetShipmentDetails(id);
            if (ds.Tables["jobno"].Rows.Count != 0)
            {
                DataRowView row = ds.Tables["jobno"].DefaultView[0];
                if (lblMode.Text == "Air")
                {
                    //DropAirLine();
                    lblShipLine.Text = "AirLine";
                    lblVessel.Text = "Flight No";
                    lblCFSName.Text = "Terminal Name";
                    lblInwardDate.Text = "Flight/Inward Date";
                    lblVesselNo.Visible = false;
                    txtVoyageNo.Visible = false;
                    Panel2.Visible = false;
                    gvShipmentDetails.Columns[2].HeaderText = "AirLine";
                    gvShipmentDetails.Columns[3].HeaderText = "Flight No";
                    lbl20.Visible = false;
                    lbl40.Visible = false;
                    txt20FtContainer.Visible = false;
                    txt40ftContainer.Visible = false;
                    pagename = (Label)Master.FindControl("lblName");
                    pagename.Text = "Air Shipment";
                }
                else
                {
                    lblShipLine.Text = "Shipping Line";
                    lblVessel.Text = "Vessel Name";
                    lblInwardDate.Text = "GLD/Inward Date";
                    lblCFSName.Text = "CFS Name";
                    lblVesselNo.Visible = true;
                    txtVoyageNo.Visible = true;
                    //DropShipingLine();
                    Panel2.Visible = true;
                    gvShipmentDetails.Columns[2].HeaderText = "Vessel Name";
                    gvShipmentDetails.Columns[3].HeaderText = "Shipping Name";
                    gvShipmentDetails.Columns[4].HeaderText = "Local IGM NO";
                    lbl20.Visible = true;
                    lbl40.Visible = true;
                    txt20FtContainer.Visible = true;
                    txt40ftContainer.Visible = true;
                    pagename = (Label)Master.FindControl("lblName");
                    pagename.Text = "Sea Shipment";
                }
                txtVesselName.Text = row["VesselName"].ToString();
                txtVoyageNo.Text = row["VoyageNo"].ToString();
                txtETA.Text = row["ETA"].ToString();
                txtInwardDate.Text = row["GLDInwardDate"].ToString();
                txtLocalIGMNo.Text = row["LocalIGMNo"].ToString();
                txtMABLNo.Text = row["MasterBLNo"].ToString();
                txtHABLNo.Text = row["HouseBLNo"].ToString();
                txtFFName.Text = row["FFName"].ToString();
                txtCFSName.Text = row["CFSName"].ToString();

                //txtCountryOfOrigin.SelectedItem.Text = row["CountryOrigin"].ToString();
                txtCountryOfOrigin.SelectedValue = row["CountryOriginCode"].ToString();
               // txtCountryOfShipment.SelectedItem.Text = row["ShipmentCountry"].ToString();
                txtCountryOfShipment.SelectedValue = row["ShipmentCountryCode"].ToString();
                //DropPort(row["ShipmentCountryCode"].ToString());
                DropPort(txtCountryOfShipment.SelectedValue);
                //txtPortofShipment.Items.Insert(0, new ListItem("~Select~", "0"));
                //txtPortofShipment.SelectedItem.Text = row["ShipmentPort"].ToString();
                txtPortofShipment.SelectedValue = row["ShipmentPortCode"].ToString();
                txtportcode.Text= row["ShipmentPortCode"].ToString();
                txtportunececode.Text = row["ShipmentUneceCode"].ToString();

                txtAgentName.Text = row["AgentName"].ToString();
                txtMarksNos.Text = row["MarksNos"].ToString();
                txtGatewayIGMNo.Text = row["GatewayIGMNo"].ToString();
                txtGatewayIGMDate.Text = row["GatewayIGMDate"].ToString();
                txtGrossWeight.Text = row["GrossWeight"].ToString();
                txtPackages.Text = row["NoOfPackages"].ToString();
                txt20FtContainer.Text = row["Container20Feet"].ToString();
                txt40ftContainer.Text = row["Container40Feet"].ToString();
                txtLocalIGMDate.Text = row["LocalIGMDate"].ToString();
                txtMABLDate.Text = row["MasterBLDate"].ToString();
                txtHABLDate.Text = row["HouseBLDate"].ToString();
                //txtGatewayIGMDate.Text = row["HouseBLDate"].ToString();
                ddlGrossWeight.SelectedValue = row["GrossWeightUnit"].ToString();
                ddlPackages.SelectedValue = row["PackagesUnit"].ToString();
                txtShippingLine.Text = row["ShippingLine"].ToString();
                txtNetWeight.Text = row["NetWeight"].ToString();
                ddlNetWeight.SelectedValue = row["NetUint"].ToString();
                txtLineNo.Text = row["ShipLineNo"].ToString();


                ViewCommercial();



            }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('DataBase Error: " + ex.Message + "');", true);
            }
        }


        private void ViewCommercial()
        {
            string query = "select Ctax_RegNo,Ctax_StateCode,Ctax_Type from T_CommercialTax Where JobNo  = '" + ddlJobNo.SelectedValue + "'";
            DataSet ds = objCommonDL.GetDataSet(query);
            if (ds.Tables["Table"].Rows.Count != 0)
            {
                DataRowView rv = ds.Tables["Table"].DefaultView[0];
                txtComTaxNo.Text = rv["Ctax_RegNo"].ToString();
                ddlComState.SelectedValue = rv["Ctax_StateCode"].ToString();
                ddlComType.SelectedValue = rv["Ctax_Type"].ToString();
            }
        }
   private void ClearContainer()
   {
       ddlContainerType.SelectedIndex = 0;
       txtContainerNo.Text = "";
       txtSealNo.Text = "";
       ddlLoadType.SelectedIndex = 0;
   }

        private void BindGrid()
        {
            DataSet ds = objShipment.GetJobShipmentContainerInfo(ddlJobNo.SelectedValue, (string)Session["ShipmentID"]);
            if (ds.Tables["jobno"].Rows.Count != 0)
            {
                try
                {
                    gvContainerInfo.DataSource = ds;
                    gvContainerInfo.DataBind();
                }
                catch(Exception ex )
                {
                    lblError.Text = ex.Message;
                }
            }
            else
            {
                gvContainerInfo.DataSource = null;
                gvContainerInfo.DataBind();
            }
        }

        protected void gvContainerInfo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (gvContainerInfo.SelectedRow.Cells[2].Text != null)
            {
                Session["cmode"] = "Edit";
                Session["Id"] = gvContainerInfo.SelectedRow.Cells[2].Text;
                ddlContainerType.SelectedIndex = ddlContainerType.Items.IndexOf(ddlContainerType.Items.FindByText(gvContainerInfo.SelectedRow.Cells[3].Text));
                if (gvContainerInfo.SelectedRow.Cells[4].Text != "" || gvContainerInfo.SelectedRow.Cells[4].Text != "&nbsp;")
                {
                    txtContainerNo.Text = gvContainerInfo.SelectedRow.Cells[4].Text;
                }
                else
                {
                    txtContainerNo.Text = "";
                }
                if (gvContainerInfo.SelectedRow.Cells[5].Text != "" && gvContainerInfo.SelectedRow.Cells[5].Text != "&nbsp;")
                {
                    txtSealNo.Text = gvContainerInfo.SelectedRow.Cells[5].Text;
                }
                else
                {
                    txtSealNo.Text = "";
                }
                ddlLoadType.SelectedIndex = ddlLoadType.Items.IndexOf(ddlLoadType.Items.FindByText(gvContainerInfo.SelectedRow.Cells[6].Text));
                btnAdd.Text = "Update";
            }
        }

        protected void btnImport_Click(object sender, EventArgs e)
        {
            //string jobno = ddlJobNo.SelectedItem.Text;
            //string username = (string)Session["USER-NAME"];
            //string joblogret = joblog.SelectJobLog(username, jobno);
            //if (joblogret == "NoJob")
            //{
                //joblog.UpdateJobLog(username);
                //int jb = joblog.InsertJobLog(username, jobno);
                //if (jb == 1)
                //{
                    Response.Redirect("frmJobCreation.aspx?JobMode="+ddlJobNo.SelectedItem.Text+"&Mode=Edit Job");
                //}
            //}
            //else
            //{
            //    string mess = "This Job can be used  " + joblogret;
            //    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('" + mess + "');", true);
            //}
            

        }

        protected void btnInvoice_Click(object sender, EventArgs e)
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
                    Response.Redirect("frmInvoiceDetails.aspx?JobMode=Shipment");
            //    }
            //}
            //else
            //{
            //    string mess = "This Job can be used  " + joblogret;
            //    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('" + mess + "');", true);
            //}

        }

        public void GetJobDetails()
        {
            DataSet ds = obj1.GetJobImportShipment(ddlJobNo.SelectedValue);
            if (ds.Tables["Table"].Rows.Count != 0)
            {
                DataRowView row = ds.Tables["Table"].DefaultView[0];
                lblJobDate.Text = row["JobReceivedDate"].ToString();
                lblMode.Text = row["Mode"].ToString();
                lblCustom.Text = row["Custom"].ToString();
                lblBeType.Text = row["BEType"].ToString();
                lblStatus.Text = row["DocFillingStatus"].ToString();
                if (row["Mode"].ToString() == "Air")
                {
                    lblShipLine.Text = "AirLine";
                    lblVessel.Text = "Flight No";
                    lblInwardDate.Text = "Flight/Inward Date";
                    lblVesselNo.Visible = false;
                    txtVoyageNo.Visible = false;
                    Panel2.Visible = false;
                }
                else
                {
                    lblShipLine.Text = "Shipping Line";
                    lblVessel.Text = "Vessel Name";
                    lblInwardDate.Text = "GLD/Inward Date";
                    lblVesselNo.Visible = true;
                    txtVoyageNo.Visible = true;
                    Panel2.Visible = true;
                    gvShipmentDetails.Columns[2].HeaderText = "Vessel Name";
                    gvShipmentDetails.Columns[3].HeaderText = "Shipping Line";
                    gvShipmentDetails.Columns[4].HeaderText = "Local IGM NO";
                   
                }
            }
        }

        protected void gvShipmentDetails_SelectedIndexChanged(object sender, EventArgs e)
        {
            Session["ShipmentID"] = gvShipmentDetails.SelectedRow.Cells[2].Text;
            GetShipmentDetails((string)Session["ShipmentID"]);
            GetShipmentContInfo();
            btnSave.Text = "Update";
            ViewCommercial();
        }

        protected void btnDelete_Click(object sender, ImageClickEventArgs e)
        {
            ImageButton btndel = sender as ImageButton;
            GridViewRow row = (GridViewRow)btndel.NamingContainer;
            string TransID = row.Cells[2].Text;
            int i = objShipment.DeleteShipment(TransID);
            if (i == 1)
            {
                GridLoad();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "redirect", "alert('Deleted Successfully');", true);
            }
        }


        //private void DropFFName()
        //{
        //    string AccountType = "FF";
        //    DataSet ds = objShipment.GetFFName(AccountType);
        //    if (ds.Tables["Dataset"].Rows.Count != 0)
        //    {
        //        ddlFFName.DataSource = ds;
        //        ddlFFName.DataTextField = "AccountName";
        //        ddlFFName.DataValueField = "AccountName";
        //        ddlFFName.DataBind();
        //    }
        //}

        protected void btnContainerDelete_Click(object sender, ImageClickEventArgs e)
        {
            ImageButton btndel = sender as ImageButton;
            GridViewRow row = (GridViewRow)btndel.NamingContainer;
            string TransID = row.Cells[2].Text;
            int i = objShipment.DeleteContainerShipment(TransID);
            if (i == 1)
            {
                BindGrid();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "redirect", "alert('Deleted Successfully');", true);
            }
        }

        private void Clear()
        {
            txtCountryOfOrigin.SelectedValue = "~Select~";
            txtCountryOfShipment.SelectedValue = "~Select~";
            txtPortofShipment.SelectedIndex = txtPortofShipment.Items.IndexOf(txtPortofShipment.Items.FindByText("~Select~"));
            txtVesselName.Text = "";
            txtVoyageNo.Text = "";
            txtETA.Text = "";
            txtAgentName.Text = "";
            txtShippingLine.Text = "";
            txtCFSName.Text = "";
            txtFFName.Text = "";
            txtInwardDate.Text = "";
            txtPackages.Text = "";
            ddlPackages.SelectedIndex = ddlPackages.Items.IndexOf(ddlPackages.Items.FindByText("~Select~"));
            txtGatewayIGMNo.Text = "";
            txtGatewayIGMDate.Text = "";
            txtLocalIGMDate.Text = "";
            txtGrossWeight.Text = "";
            ddlGrossWeight.SelectedIndex = ddlGrossWeight.Items.IndexOf(ddlGrossWeight.Items.FindByText("~Select~"));
            txtNetWeight.Text = "";
            ddlNetWeight.SelectedIndex = ddlNetWeight.Items.IndexOf(ddlNetWeight.Items.FindByText("~Select~"));
            txtLocalIGMNo.Text = "";
            txtMABLNo.Text = "";
            txtMABLDate.Text = "";
            txtMarksNos.Text = "";
            txtHABLNo.Text = "";
            txtHABLDate.Text = "";
            txt20FtContainer.Text = "";
            txt40ftContainer.Text = "";
            txtPortofShipment.Items.Clear();
            txtPortofShipment.Items.Insert(0, new ListItem("~Select~", "0"));
            txtLineNo.Text = "";
            txtportcode.Text = "";
            txtportunececode.Text = "";
            gvShipmentDetails.DataBind();
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            int result = new int();
            int conresult = new int();
            conresult = objShipment.containerdts(ddlContainerType.SelectedItem.Text.Substring(0, 2), Convert.ToInt32((string)Session["ShipmentID"]));
            try
            {
                string date = DateTime.Now.ToString();
                if ((string)Session["cmode"] == "" || (string)Session["cmode"] == null)
                {
                    if ((Convert.ToInt32(txt20FtContainer.Text) > conresult) && ddlContainerType.SelectedItem.Text.Substring(0, 2) == "20")
                    {
                        result = objShipment.InsertShipmentContainerInfo((string)Session["ShipmentID"], ddlJobNo.SelectedValue, lblJobDate.Text,
                            ddlContainerType.SelectedItem.Text.Substring(0, 2), ddlContainerType.SelectedItem.Text, txtContainerNo.Text, txtSealNo.Text,
                            ddlLoadType.SelectedValue, (string)Session["USER-NAME"], date, (string)Session["USER-NAME"], date);
                    }
                    else if ((Convert.ToInt32(txt40ftContainer.Text) > conresult) && ddlContainerType.SelectedItem.Text.Substring(0, 2) == "40")
                    {
                        result = objShipment.InsertShipmentContainerInfo((string)Session["ShipmentID"], ddlJobNo.SelectedValue, lblJobDate.Text,
                              ddlContainerType.SelectedItem.Text.Substring(0, 2), ddlContainerType.SelectedItem.Text, txtContainerNo.Text, txtSealNo.Text,
                              ddlLoadType.SelectedValue, (string)Session["USER-NAME"], date, (string)Session["USER-NAME"], date);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Exceeds No Of Container');", true);
                    }
                }
                else if ((string)Session["cmode"] == "Edit")
                {
                    int id = Convert.ToInt32(Session["Id"]);
                    result = objShipment.UpdateShipmentContainerInfo(id, ddlJobNo.SelectedValue, lblJobDate.Text, ddlContainerType.SelectedItem.Text.Substring(0, 2),
                        ddlContainerType.SelectedItem.Text, txtContainerNo.Text, txtSealNo.Text,
                        ddlLoadType.SelectedValue, (string)Session["USER-NAME"], date);

                }
                if (result == 1)
                {
                    BindGrid();
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Container Details Successfully saved');", true);
                    ClearContainer();
                    btnAdd.Text = "Save";
                    Session["cmode"] = null;
                }
            }
            catch (Exception ex)
            {
                string Msg = ex.Message.ToString();
                lblError.Text = Msg;
            }
        }

        protected void txtPortofShipment_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtportcode.Text = "";
            txtportunececode.Text = "";
            txtportcode.Text = txtPortofShipment.SelectedValue;
            GetPortUneceCode(txtCountryOfShipment.SelectedValue, txtPortofShipment.SelectedValue);
        }

        protected void btnNew_Click(object sender, EventArgs e)
        {
            Clear();
            btnSave.Text = "Save";
            Session["mode"] = "";
            Panel2.Visible = false;
            GridLoad();
            txtPackages.Text = "0";
        }
    }
}