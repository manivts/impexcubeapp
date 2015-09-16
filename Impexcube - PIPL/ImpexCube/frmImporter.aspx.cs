using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace ImpexCube
{
    public partial class frmImporter : System.Web.UI.Page
    {
        VTS.ImpexCube.Business.ShipmentBL objShipment = new VTS.ImpexCube.Business.ShipmentBL();
        VTS.ImpexCube.Business.Importer objimporter = new VTS.ImpexCube.Business.Importer();
        VTS.ImpexCube.Business.CommonBL obj1 = new VTS.ImpexCube.Business.CommonBL();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["Mode"] == "JobCreation")
                {
                    string jno=(string)Session["JobNo"];
                    DropJobNo();
                    DropCountry();
                    ddlJobNo.SelectedValue = jno;
                    GetJobDetails(jno);
                    GetImportDetails();
                }
                else if (Request.QueryString["Mode"] == "Direct")
                {
                    DropJobNo();
                    DropCountry();
                }
                else
                    Response.Redirect("frmLogin.aspx");

            }
        }

        private void DropJobNo()
        {
            DataSet ds = obj1.GetJobNo();
            if (ds.Tables["jobno"].Rows.Count != 0)
            {
                ddlJobNo.DataSource = ds;
                ddlJobNo.DataTextField = "JobNo";
                ddlJobNo.DataValueField = "JobNo";
                ddlJobNo.DataBind();
            }
        }
        public void branchdetails()
        {
            DataSet ds = objimporter.branchdetails(txtImporter.Text, lblImpBranchNo.Text);//ddlBranchSno.SelectedValue);
          
            if (ds.Tables["branchsno"].Rows.Count != 0)
            {
                DataRowView row = ds.Tables["branchsno"].DefaultView[0];
                lblIECodeNo.Text = row["IECodeNo"].ToString();
                lblAddress.Text = row["Address"].ToString();
                lblCity.Text = row["City"].ToString();
                lblStateImp.Text = row["State"].ToString();
                lblZipCode.Text = row["Zipcode"].ToString();
                lblState.Text = row["CommericalTaxState"].ToString();
                lblTaxType.Text = row["CommericalTaxType"].ToString();
                lblRegnNo.Text = row["CommericalTaxRegnNo"].ToString();
            }
            else
            {
                lblIECodeNo.Text = "";
                lblAddress.Text = "";
                lblCity.Text = "";
                lblStateImp.Text = "";
                lblZipCode.Text = "";
                lblState.Text = "";
                lblTaxType.Text = "";
                lblRegnNo.Text = "";
               
            }
        }

        public void branchdetailsHigh()
        {
            DataSet ds = objimporter.branchdetails(txtSellerName.Text,lblSellerBranchNo.Text);//ddlSellerName.SelectedValue, ddlBranchSnoHigh.SelectedValue);

            if (ds.Tables["branchsno"].Rows.Count != 0)
            {
                DataRowView row = ds.Tables["branchsno"].DefaultView[0];
            
                if (chkHighSeaSale.Checked == true)
                {
                    lblAddressHigh.Text = row["Address"].ToString();
                    lblCityHigh.Text = row["City"].ToString();
                    lblStateHigh.Text = row["State"].ToString();
                    lblZipCodeHigh.Text = row["Zipcode"].ToString();
                }

            }
            else
            {
               
                if (chkHighSeaSale.Checked == true)
                {
                    lblAddressHigh.Text = "";
                    lblCityHigh.Text = "";
                    lblStateHigh.Text = "";
                    lblZipCodeHigh.Text = "";
                }
            }
        }

        public void JobHistory()
        {
            DataSet ds = objimporter.jobno(ddlJobNo.SelectedValue);

        if (ds.Tables["jobno"].Rows.Count != 0)
        {
            DataRowView row = ds.Tables["jobno"].DefaultView[0];
            //lblJobReceivedDate.Text =Convert.ToDateTime( row["JobReceivedDate"]).ToString("dd/MM/yyyy");
            lblJobReceivedDate.Text = row["JobReceivedDate"].ToString();
            lblMode.Text = row["Mode"].ToString();
            lblCustom.Text = row["Custom"].ToString();
            lblBEType.Text = row["BEType"].ToString();
            lblDocFillingStatus.Text = row["DocFillingStatus"].ToString();
            
            lblBENo.Text = row["BENo"].ToString();
            lblBEDate.Text = row["BEDate"].ToString();
           
        }

        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            string SingleCons = "";
            if (chkSingleConsignor.Checked)
            {
                SingleCons = "1";
            }
            

            string HighSea = "";
            if (chkHighSeaSale.Checked)
            {
                HighSea = "1";
            }
            string CkUnderSec46 = "";
            string CkKachha = "";
            string CkUnderSec48 = "";
            string CkFirstChk = "";
            string CkGreen = "No";


            if (ChkUnderSec46.Checked)
            {
                CkUnderSec46 = "Yes";
            }
            else
            {
                CkUnderSec46 = "No";
            }
            if (ChkFirstChk.Checked)
            {
                CkFirstChk = "Yes";
            }
            else
            {
                CkFirstChk = "No";
            }
            if (ChkKachha.Checked)
            {
                CkKachha = "Yes";
            }
            else
            {
                CkKachha = "No";
            }
            if (ChkUnderSec48.Checked)
            {
                CkUnderSec48 = "Yes";
            }
            else
            {
                CkUnderSec48 = "No";
            }

            //int result = objimporter.insert(ddlJobNo.SelectedValue, txtImporter.Text, lblIECodeNo.Text, lblImpBranchNo.Text, lblAddress.Text, lblCity.Text, lblState.Text, 
            //    lblZipCode.Text, txtImporterRefNo.Text, txtPortofShipment.SelectedItem.Text ,txtCountryOfOrigin.SelectedItem.Text , txtCountryOfShipment.SelectedItem.Text , txtBEHeading.Text, 
            //    lblState.Text,lblTaxType.Text,lblRegnNo.Text,txtConsignor.Text,txtAddress.Text,txtCity.Text,txtCountry.Text,lblSellerBranchNo.Text,
            //    lblIECodeNoHigh.Text, lblSellerBranchNo.Text, lblAddressHigh.Text, lblCityHigh.Text, lblStateHigh.Text, lblZipCodeHigh.Text, SingleCons, HighSea, CkUnderSec46, lblunderSec46.Text, CkKachha, lblKachha.Text, CkUnderSec48, lblUnderSec48.Text, CkFirstChk, lblFirstChk.Text, CkGreen, CkGreen);
            
         
            //if (result == 1)
            //{
            //    btnSave.Visible = false;
            //    string mess = "Successfully Saved";
            //    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('" + mess + "');", true);
            //}

        }

        protected void ddlBranchSno_SelectedIndexChanged(object sender, EventArgs e)
        {
            branchdetails();
        }

        protected void ddlSellerName_SelectedIndexChanged(object sender, EventArgs e)
        {
            //branchsnoHigh();
        }

        protected void ddlBranchSnoHigh_SelectedIndexChanged(object sender, EventArgs e)
        {
            branchdetailsHigh();
        }
        protected void btnJobCreation_Click(object sender, EventArgs e)
        {
            Response.Redirect("frmJobCreation.aspx");
        }
        
        protected void ddlJobNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            clear();
            GetJobDetails(ddlJobNo.SelectedValue);
            GetImportDetails();
            if (txtImporter.Text == "")
            {
                btnSave.Visible = true;
                btnUpdate.Visible = false;
            }
            else{
                btnSave.Visible = false;
                btnUpdate.Visible = true;
            }
        }

        public void GetJobDetails(string jobname)
        {
            DataSet ds = obj1.GetJobDetails(jobname);
            if (ds.Tables["Table"].Rows.Count != 0)
            {
                DataRowView row = ds.Tables["Table"].DefaultView[0];
                lblJobReceivedDate.Text = row["JobReceivedDate"].ToString();
                lblMode.Text = row["Mode"].ToString();
                lblCustom.Text = row["Custom"].ToString();
                lblBEType.Text = row["BEType"].ToString();
                lblDocFillingStatus.Text = row["DocFillingStatus"].ToString();
                lblBEDate.Text = row["BENo"].ToString();
                lblBENo.Text = row["BEDate"].ToString();
            }
        }
        public void clear()
        {
            txtImporter.Text = "";
            lblIECodeNo.Text = "";
            lblImpBranchNo.Text = "";
            lblAddress.Text = "";
            lblCity.Text = "";
            lblStateImp.Text = "";
            lblZipCode.Text = "";
            txtImporterRefNo.Text = "";

            txtBEHeading.Text = "";

            lblState.Text = "";
            lblTaxType.Text = "";
            lblRegnNo.Text = "";
            chkSingleConsignor.Checked = false;
            txtConsignor.Text = "";
            txtAddress.Text ="";
           
            txtCountry.Text = "";
            chkHighSeaSale.Checked = false;
            lblSellerBranchNo.Text = "";
            lblIECodeNoHigh.Text = "";
            lblSellerBranchNo.Text = "";
            lblAddressHigh.Text = "";
            lblCityHigh.Text = "";
            lblStateHigh.Text = "";
            lblZipCodeHigh.Text = "";

            lblunderSec46.Text = "";
            lblFirstChk.Text = "";
            lblKachha.Text ="";
            lblUnderSec48.Text = "";
            ChkUnderSec46.Checked = false;
            ChkFirstChk.Checked = false;
            ChkKachha.Checked = false;
            ChkUnderSec48.Checked = false;
           
        }
        public void GetImportDetails()
        {
            try
            {
                DataSet ds = objimporter.GetImporterDetails(ddlJobNo.SelectedValue);
                if (ds.Tables["ImportDetails"].Rows.Count != 0)
                {
                   // btnSave.Visible = false;
                   // btnUpdate.Visible = true;
                    DataRowView row = ds.Tables["ImportDetails"].DefaultView[0];
                    Session["ImporterID"] = row["ImporterId"].ToString();
                    txtImporter.Text = row["Importer"].ToString();
                    lblIECodeNo.Text = row["IECodeNo"].ToString();
                    lblImpBranchNo.Text = row["BranchSno"].ToString();
                    lblAddress.Text = row["Address"].ToString();
                    lblCity.Text = row["City"].ToString();
                    lblStateImp.Text = row["State"].ToString();
                    lblZipCode.Text = row["ZipCode"].ToString();
                    txtImporterRefNo.Text = row["ImporterRefNo"].ToString();

                    

                    txtBEHeading.Text = row["BEHeading"].ToString();

                    lblState.Text = row["CommericalState"].ToString();
                    lblTaxType.Text = row["CommericalTaxType"].ToString();
                    lblRegnNo.Text = row["CommericalRegnNo"].ToString();

                    string SingleConsignor = row["SingleConsignor"].ToString();
                    if (SingleConsignor == "True")
                    {
                        chkSingleConsignor.Checked = true;
                    }
                    txtConsignor.Text = row["Consignor"].ToString();
                    txtAddress.Text = row["ConsignorAddress"].ToString();
                    txtCity.Text = row["ConsignorCity"].ToString();
                    txtCountry.Text = row["ConsignorCountry"].ToString();
                    string HighSeaSale = row["HighSeaSale"].ToString();
                    if (HighSeaSale == "True")
                    {
                        chkHighSeaSale.Checked = true;
                    }
                    lblSellerBranchNo.Text = row["SellerName"].ToString();
                    lblIECodeNoHigh.Text = row["HighIECode"].ToString();
                    lblSellerBranchNo.Text = row["HighBranchSno"].ToString();
                    lblAddressHigh.Text = row["HighAddress"].ToString();
                    lblCityHigh.Text = row["HighCity"].ToString();
                    lblStateHigh.Text = row["HighState"].ToString();
                    lblZipCodeHigh.Text = row["HighZipCode"].ToString();

                    lblunderSec46.Text = row["underSec46"].ToString();
                    lblFirstChk.Text = row["FirstChk"].ToString();
                    lblKachha.Text = row["Kachha"].ToString();
                    lblUnderSec48.Text = row["UnderSec48"].ToString();
                    string unsec46 = row["ChkUnderSec46"].ToString();
                    string fischk = row["ChkFirstChk"].ToString();
                    string kacha = row["ChkKachha"].ToString();
                    string unsec48 = row["ChkUnderSec48"].ToString();

                    if (unsec46 == "Yes")
                    {
                        ChkUnderSec46.Checked = true;
                    }
                    if (fischk == "Yes")
                    {
                        ChkFirstChk.Checked = true;
                    }
                    if (kacha == "Yes")
                    {
                        ChkKachha.Checked = true;
                    }
                    if (unsec48 == "Yes")
                    {
                        ChkUnderSec48.Checked = true;
                    }
                    DropCountry();
                    txtCountryOfOrigin.SelectedItem.Text = row["CountryofOrgin"].ToString();
                    txtCountryOfShipment.SelectedItem.Text = row["CountryofShipment"].ToString();

                    //GetCountryCode

                    string countryCode = objShipment.GetCountryCode(txtCountryOfShipment.SelectedItem.Text);
                    DropPort(countryCode);
                    txtPortofShipment.SelectedItem.Text = row["Portofshipment"].ToString();

                }
            }
            catch
            {
            }

        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            //btnSave.Visible =true ;
            //btnUpdate.Visible = false;
            string SingleCons = "";
            if (chkSingleConsignor.Checked)
            {
                SingleCons = "1";
            }
            string HighSea = "";
            if (chkHighSeaSale.Checked)
            {
                HighSea = "1";
            }

            string CkUnderSec46 = "";
            string CkKachha = "";
            string CkUnderSec48 = "";
            string CkFirstChk = "";
            string CkGreen = "No";


            if (ChkUnderSec46.Checked)
            {
                CkUnderSec46 = "Yes";
            }
            else
            {
                CkUnderSec46 = "No";
            }
            if (ChkFirstChk.Checked)
            {
                CkFirstChk = "Yes";
            }
            else
            {
                CkFirstChk = "No";
            }
            if (ChkKachha.Checked)
            {
                CkKachha = "Yes";
            }
            else
            {
                CkKachha = "No";
            }
            if (ChkUnderSec48.Checked)
            {
                CkUnderSec48 = "Yes";
            }
            else
            {
                CkUnderSec48 = "No";
            }

            //int result = objimporter.Update(txtImporter.Text, lblIECodeNo.Text, lblImpBranchNo.Text, lblAddress.Text, lblCity.Text, lblState.Text,
            //   lblZipCode.Text, txtImporterRefNo.Text, txtPortofShipment.SelectedItem.Text , txtCountryOfOrigin.SelectedItem.Text , txtCountryOfShipment.SelectedItem.Text , txtBEHeading.Text,
            //   lblState.Text, lblTaxType.Text, lblRegnNo.Text, txtConsignor.Text, txtAddress.Text, txtCity.Text, txtCountry.Text, lblSellerBranchNo.Text,
            //   lblIECodeNoHigh.Text, lblSellerBranchNo.Text, lblAddressHigh.Text, lblCityHigh.Text, lblStateHigh.Text, lblZipCodeHigh.Text, (string)Session["ImporterID"], SingleCons, HighSea, CkUnderSec46, lblunderSec46.Text, CkKachha, lblKachha.Text, CkUnderSec48, lblUnderSec48.Text, CkFirstChk, lblFirstChk.Text, CkGreen, CkGreen);


            //if (result == 1)
            //{
            //    string mess = "Successfully Update";
            //    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('" + mess + "');", true);
            //}
        }

        protected void btnShipment_Click(object sender, EventArgs e)
        {
            Session["JobNo"] = ddlJobNo.SelectedValue;
            Response.Redirect("frmShipment.aspx?Mode=Import");
        }
        private void DropPort(string shipctry)
        {
            DataSet ds = objShipment.GetPort(shipctry);
            if (ds.Tables["port"].Rows.Count != 0)
            {
                txtPortofShipment.DataSource = ds;
                txtPortofShipment.DataTextField = "PortName";
                txtPortofShipment.DataValueField = "PortName";
                txtPortofShipment.DataBind();
                txtPortofShipment.Items.Insert(0, new ListItem("-Select-", "0"));
            }
            else
            {
                txtPortofShipment.DataSource = null;
                txtPortofShipment.DataBind();
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
                txtCountryOfShipment.Items.Insert(0, new ListItem("-Select-", "0"));

                txtCountryOfOrigin.DataSource = ds;
                txtCountryOfOrigin.DataTextField = "CountryName";
                txtCountryOfOrigin.DataValueField = "CountryCode";
                txtCountryOfOrigin.DataBind();
                txtCountryOfOrigin.Items.Insert(0, new ListItem("-Select-", "0"));
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

        protected void ChkUnderSec46_CheckedChanged(object sender, EventArgs e)
        {
            if (ChkUnderSec46.Checked==true)
            {
                lblunderSec46.Enabled = true;
            }
            else
            {
                lblunderSec46.Enabled = false;
            }
        }

        protected void ChkFirstChk_CheckedChanged(object sender, EventArgs e)
        {
            if (ChkFirstChk.Checked == true)
            {
                lblFirstChk.Enabled = true;
            }
            else
            {
                lblFirstChk.Enabled = false;
            }
        }

       
        protected void ChkKachha_CheckedChanged(object sender, EventArgs e)
        {

            if (ChkKachha.Checked == true)
            {
                lblKachha.Enabled = true;
            }
            else
            {
                lblKachha.Enabled = false;
            }
        }

        protected void ChkUnderSec48_CheckedChanged(object sender, EventArgs e)
        {
            if (ChkUnderSec48.Checked == true)
            {
                lblUnderSec48.Enabled = true;
            }
            else
            {
                lblUnderSec48.Enabled = false;
            }
        }

        protected void btnNew_Click(object sender, EventArgs e)
        {
            //Response.Redirect("http://122.165.65.50/PiplBilling/index.aspx?uName=" + user + "&Branch=" + branch + "&BranchShortName=" + branchshort + "&UserType=" + usertype + "&CMP=" + cmp + "&Mail=" + mail + "&Fyear=" + Fyear + "&EmpId=" + userName, false);
        }

        protected void btnSavetomast_Click(object sender, EventArgs e)
        {
            DataSet ds = objimporter.checksave(txtImporter.Text);
            if (ds.Tables["branchsno"].Rows.Count == 0)
            {              

                int result = objimporter.insertmast(txtImporter.Text,txtImporter.Text.Substring(0,3), lblIECodeNo.Text, txtImporterRefNo.Text);
                int result1 = objimporter.insertaddmast(txtImporter.Text.Substring(0,3),lblImpBranchNo.Text, lblAddress.Text, lblCity.Text, lblStateImp.Text, lblZipCode.Text, lblState.Text, lblTaxType.Text, lblRegnNo.Text);
                if (result == 1 && result1 == 1)
                {
                    string mess = "Successfully Saved";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('" + mess + "');", true);
                }
            }
            else
            {
                DataRowView row = ds.Tables["branchsno"].DefaultView[0];
                String partyCod = row["PartyCode"].ToString();

                int result = objimporter.updatemast(txtImporter.Text, lblIECodeNo.Text, txtImporterRefNo.Text);
                int result1 = objimporter.updateaddmast(lblImpBranchNo.Text, lblAddress.Text, lblCity.Text, lblStateImp.Text, lblZipCode.Text, lblState.Text, lblTaxType.Text, lblRegnNo.Text, partyCod);
                if (result == 1 && result1>=1)
                {
                    string mess = "Successfully Updated";
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('" + mess + "');", true);
                }
            }
        }

        
        
    }
}