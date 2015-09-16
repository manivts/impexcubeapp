using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using VTS.ImpexCube.Business;
using VTS.ImpexCube.Utlities;
using System.Data.SqlClient;
using System.Configuration;
using System.Text;


namespace ImpexCube
{
    public partial class frmJobCreation : System.Web.UI.Page
    {
        string strconn = (string)ConfigurationManager.ConnectionStrings["Constr"].ConnectionString;
        VTS.ImpexCube.Business.JobCreationBAL objJobNo = new VTS.ImpexCube.Business.JobCreationBAL();
        VTS.ImpexCube.Business.InvoiceDetailsBL invBL = new VTS.ImpexCube.Business.InvoiceDetailsBL();
        VTS.ImpexCube.Business.ShipmentBL objShipment = new VTS.ImpexCube.Business.ShipmentBL();
        VTS.ImpexCube.Business.Importer objimporter = new VTS.ImpexCube.Business.Importer();
        VTS.ImpexCube.Business.CommonBL obj1 = new VTS.ImpexCube.Business.CommonBL();
        VTS.ImpexCube.Utlities.Utility joblog = new VTS.ImpexCube.Utlities.Utility();
        string JobNo = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack == false)
            {
                TabPanel3.Visible = false;
                Label pagename;
                pagename = (Label)Master.FindControl("lblName");
                pagename.Text = "Job Creation";
                if (Request.QueryString["Mode"] == "Edit Job")
                {
                    Panel1.Visible = true;
                    Panel2.Visible = false;
                    btnsave.Visible = false;
                    btnUpdate.Visible = true;
                    JobImportDetails(Request.QueryString["JobMode"]);//ImpShortName,HighShortName
                }
                else if (Request.QueryString["Mode"] == "New Job")
                {

                    DataSet ds = objJobNo.JobNo();
                    DataRowView row = ds.Tables["JobNo"].DefaultView[0];
                    int jobno = Convert.ToInt32(row["keycode"]);
                    jobno = jobno + 1;
                    txtjno.Text = jobno.ToString();
                    txtJobReceivedDate.Text = System.DateTime.Now.ToString("dd/MM/yyyy");
                    // GridLoad(JobNo);

                    BindCurrency();
                    BindBondType();
                    // GetImportDetails();
                    Panel1.Visible = true;
                    Panel2.Visible = false;
                }
                else if (Request.QueryString["Mode"] == "Search Job")
                {
                    Panel1.Visible = false;
                    Panel2.Visible = true;
                    GridLoad(JobNo);
                }
            }
        }

        private void BindBondType()
        {
            DataSet ds = objJobNo.SelectBondType();
            if (ds.Tables["SelectBond"].Rows.Count != 0)
            {
                ddlBondType.DataSource = ds;
                ddlBondType.DataTextField = "BondType";
                ddlBondType.DataValueField = "BondCode";
                ddlBondType.DataBind();
                ddlBondType.Items.Insert(0, new ListItem("~Select~", "0"));

                ddlCerType.DataSource = ds;
                ddlCerType.DataTextField = "BondType";
                ddlCerType.DataValueField = "BondCode";
                ddlCerType.DataBind();
                ddlCerType.Items.Insert(0, new ListItem("~Select~", "0"));
            }
        }
        private void BindCustomHouse(string AirSea)
        {
            ddlCustom.Items.Clear();
            ddlRegisteredAt.Items.Clear();
            DataSet ds = objJobNo.BindCustomHouse(AirSea);
            if (ds.Tables["SelectCustom"].Rows.Count != 0)
            {
                ddlCustom.DataSource = ds;
                ddlCustom.DataTextField = "Port";
                ddlCustom.DataValueField = "UNECECode";
                ddlCustom.DataBind();
                ddlCustom.Items.Insert(0, new ListItem("~Select~", "0"));

                ddlRegisteredAt.DataSource = ds;
                ddlRegisteredAt.DataTextField = "Port";
                ddlRegisteredAt.DataValueField = "UNECECode";
                ddlRegisteredAt.DataBind();
                ddlRegisteredAt.Items.Insert(0, new ListItem("~Select~", "0"));
            }
        }
        public void BindCurrency()
        {
            DataSet ds = invBL.GetCurrencyDetails();
            if (ds.Tables["Invoice"].Rows.Count != 0)
            {
                ddlCurrency.DataSource = ds;
                ddlCurrency.DataTextField = "CurrencyShortName";
                ddlCurrency.DataValueField = "CurrencyShortName";
                ddlCurrency.DataBind();
            }
        }

        public void GridLoad(string JobNo)
        {
            DataSet ds = objJobNo.JobNoList(JobNo);
            gvJobNo.DataSource = ds;
            gvJobNo.DataBind();
        }


        protected void btnsave_Click(object sender, EventArgs e)
        {
            string Chklisttype = string.Empty;
            if (ddlBEType.SelectedValue == "H")
            {
               Chklisttype = "Check List-BILL OF ENTRY FOR HOME CONSUMPTION";
            }
            else if(ddlBEType.SelectedValue == "W")
            {
            Chklisttype="CheckList - BILL OF ENTRY FOR WAREHOUSING";
            }
            else if (ddlBEType.SelectedValue == "X")
            {
                Chklisttype="CheckList - BILL OF ENTRY FOR EX BOND CLEARANCE";
            }
            int result = objJobNo.insert(txtjno.Text, txtJobReceivedDate.Text, ddlMode.SelectedValue, ddlCustom.SelectedValue, ddlBEType.SelectedValue, ddlDocFillingStatus.SelectedValue, ddlFilling.SelectedValue, txtBENo.Text, txtBEDate.Text, (string)Session["FYear"], (string)Session["USER-NAME"], txtTotalNoOfInvoice.Text, txtTotalInvoiceValue.Text, ddlCurrency.SelectedValue, ChkClearanceBond.Checked, ddlCustom.SelectedItem.Text, Chklisttype);
            if (result > 1)
            {
                UpdateImporterDetails();
                int resultupdate = objJobNo.update(txtjno.Text);
                Session["JobNo"] = txtjno.Text;
                btnsave.Visible = false;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "redirect", "alert('Saved Successfully'); window.location.href='frmJobCreation.aspx?mode=New';", true);
                //ScriptManager.RegisterStartupScript(this, this.GetType(), "redirect", "alert('Saved Successfully'); window.location.href='efrmJobCreation.aspx?mode=New';", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "redirect", "alert('Please Check the Job No!');", true);
            }
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            string Chklisttype = string.Empty;
            if (ddlBEType.SelectedValue == "H")
            {
                Chklisttype = "Check List-BILL OF ENTRY FOR HOME CONSUMPTION";
            }
            else if (ddlBEType.SelectedValue == "W")
            {
                Chklisttype = "CheckList - BILL OF ENTRY FOR WAREHOUSING";
            }
            else if (ddlBEType.SelectedValue == "X")
            {
                Chklisttype = "CheckList - BILL OF ENTRY FOR EX BOND CLEARANCE";
            }
            int result = objJobNo.Update(txtjno.Text, txtJobReceivedDate.Text, ddlMode.SelectedValue, ddlCustom.SelectedValue, ddlBEType.SelectedValue, ddlDocFillingStatus.SelectedValue, ddlFilling.SelectedValue, txtBENo.Text, txtBEDate.Text, txtTotalNoOfInvoice.Text, txtTotalInvoiceValue.Text, ddlCurrency.SelectedValue, ChkClearanceBond.Checked, (string)Session["jobid"], ddlCustom.SelectedItem.Text, Chklisttype);
            if (result == 1)
            {
                UpdateImporterDetails();
                Session["JobNo"] = txtjno.Text;
                btnUpdate.Visible = false;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "redirect", "alert('Successfully Update');", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "redirect", "alert('Please Check the Job No!');", true);
            }
        }

        protected void btnClose_Click(object sender, EventArgs e)
        {
            Response.Redirect("frmJobCreation.aspx?Mode=New Job");
        }
        protected void gvJobNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                Panel1.Visible = true;
                Panel2.Visible = false;
                btnUpdate.Visible = true;
                btnsave.Visible = false;
                BindCurrency();
                string jobno = gvJobNo.SelectedRow.Cells[2].Text;
                JobImportDetails(jobno);
            }
            catch (Exception ex)
            {
                string Message = ex.Message;
            }
        }

        //public void SaveImportDetails()

        //{
        //    string SingleCons;
        //    if (chkCon.Checked == true)
        //    {
        //        SingleCons = "1";
        //    }
        //    else
        //    {
        //        SingleCons = "0";
        //    }
        //    string HighSea;
        //    if (ChkHSS.Checked==true)
        //        {
        //             HighSea = "1";
        //        }
        //        else
        //        {
        //             HighSea = "0";
        //        }

        //    string CkUnderSec46 = "";
        //    string CkKachha = "";
        //    string CkUnderSec48 = "";
        //    string CkFirstChk = "";
        //    string CkGreen = "No";
        //    if (ChkUnderSec46.Checked)
        //    {
        //        CkUnderSec46 = "Yes";
        //    }
        //    else
        //    {
        //        CkUnderSec46 = "No";
        //    }
        //    if (ChkFirstChk.Checked)
        //    {
        //        CkFirstChk = "Yes";
        //    }
        //    else
        //    {
        //        CkFirstChk = "No";
        //    }
        //    if (ChkKachha.Checked)
        //    {
        //        CkKachha = "Yes";
        //    }
        //    else
        //    {
        //        CkKachha = "No";
        //    }
        //    if (ChkUnderSec48.Checked)
        //    {
        //        CkUnderSec48 = "Yes";
        //    }
        //    else
        //    {
        //        CkUnderSec48 = "No";
        //    }
        //        string ImporterCode="";
        //        string  ImporterType=ddlImpType.SelectedValue;
        //        string ImpShortName=lblShortName.Text;
        //        string HighShortName = txtSelerShortName.Text;
        //        string ZipCode = lblZipCode.Text;
        //        //ImpShortName,HighShortName
        //    int result = objimporter.insert(txtjno.Text, txtImporter.Text, lblIECodeNo.Text, lblImpBranchNo.Text, lblAddress.Text, lblCity.Text, lblStateImp.Text,
        //     txtImporterRefNo.Text, txtBEHeading.Text,txtConsignor.Text,txtCnrShortName.Text, txtAddress.Text, txtCity.Text, txtCountry.Text, txtSellerName.Text, txtSelerShortName.Text,
        //    lblIECodeNoHigh.Text, lblSellerBranchNo.Text, lblAddressHigh.Text, lblCityHigh.Text, lblStateHigh.Text, lblZipCodeHigh.Text, SingleCons, HighSea, CkUnderSec46,
        //    lblunderSec46.Text, CkKachha, lblKachha.Text, CkUnderSec48, lblUnderSec48.Text, CkFirstChk, lblFirstChk.Text, CkGreen, CkGreen, ImporterCode, ImporterType, ImpShortName, HighShortName, ZipCode);
        //    if (result == 1)
        //    {
        //        //btnSaveImp.Visible = false;
        //        string mess = "Successfully Saved";
        //        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('" + mess + "');", true);
        //    }
        //}

        public void clear()
        {

            txtImporter.Text = "";
            lblIECodeNo.Text = "";
            lblImpBranchNo.Text = "";
            lblAddress.Text = "";
            lblCity.Text = "";
            lblStateImp.Text = "";
            txtImporterRefNo.Text = "";
            txtBEHeading.Text = "";
            //chkSingleConsignor.Checked = false;
            txtConsignor.Text = "";
            txtAddress.Text = "";
            txtCountry.Text = "";
            //chkHighSeaSale.Checked = false;
            lblSellerBranchNo.Text = "";
            lblIECodeNoHigh.Text = "";
            lblSellerBranchNo.Text = "";
            lblAddressHigh.Text = "";
            lblCityHigh.Text = "";
            lblStateHigh.Text = "";
            lblZipCodeHigh.Text = "";
            lblunderSec46.Text = "";
            lblFirstChk.Text = "";
            lblKachha.Text = "";
            lblUnderSec48.Text = "";
            txtSellerName.Text = "";
            lblShortName.Text = "";
            lblZipCode.Text = "";
            txtCnrShortName.Text = "";
            txtSelerShortName.Text = "";
            txtBENo.Text = "";
            txtBEDate.Text = "";
            txtTotalInvoiceValue.Text = "0";
            txtTotalNoOfInvoice.Text = "0";
            txtCity.Text = "";
            ChkUnderSec46.Checked = false;
            ChkFirstChk.Checked = false;
            ChkKachha.Checked = false;
            ChkUnderSec48.Checked = false;
            ChkClearanceBond.Checked = false;
            ddlFilling.SelectedIndex = 0;
            ddlMode.SelectedIndex = 0;
            ddlCustom.SelectedIndex = 0;
            ddlCurrency.SelectedIndex = 0;
            ddlDocFillingStatus.SelectedIndex = 0;
            ddlBEType.SelectedIndex = 0;
        }

        public void UpdateImporterDetails()
        {
            //btnSave.Visible =true ;
            //btnUpdate.Visible = false;
            string SingleCons;
            if (chkCon.Checked == true)
            {
                SingleCons = "1";
            }
            else
            {
                SingleCons = "0";
            }
            string HighSea;
            if (ChkHSS.Checked == true)
            {
                HighSea = "1";
            }
            else
            {
                HighSea = "0";
            }
            string CkUnderSec46 = "";
            string CkKachha = "";
            string CkUnderSec48 = "";
            string CkFirstChk = "";
            string CkGreen = "";

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
            if (ChkGreenChannel.Checked)
            {
                CkGreen = "Yes";
            }
            else
            {
                CkGreen = "No";
            }

            int result = objimporter.Update(txtjno.Text, txtImporter.Text, ddlImpType.SelectedValue, lblIECodeNo.Text, lblImpBranchNo.Text, lblAddress.Text, lblCity.Text, lblStateImp.Text,
               txtImporterRefNo.Text, txtBEHeading.Text, txtConsignor.Text, txtCnrShortName.Text, txtAddress.Text, txtCity.Text, txtCountry.Text, txtSellerName.Text, txtSelerShortName.Text,
               lblIECodeNoHigh.Text, lblSellerBranchNo.Text, lblAddressHigh.Text, lblCityHigh.Text, lblStateHigh.Text, lblZipCodeHigh.Text, SingleCons,
               HighSea, CkUnderSec46, lblunderSec46.Text, CkKachha, lblKachha.Text, CkUnderSec48, lblUnderSec48.Text, CkFirstChk, lblFirstChk.Text, CkGreen, CkGreen, lblZipCode.Text, lblZipCode.Text);
        }

        protected void btnShipment_Click(object sender, EventArgs e)
        {
            //Session["JobNo"] = txtjno.Text;
            //string jobno =(string) Session["JobNo"];
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

        protected void ChkUnderSec46_CheckedChanged(object sender, EventArgs e)
        {
            if (ChkUnderSec46.Checked == true)
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

        public void JobImportDetails(string JobNo)
        {
            try
            {
                DataSet dsJobDetails = objJobNo.SelectJobNo(JobNo);
                if (dsJobDetails.Tables["SelectJobNo"].Rows.Count != 0)
                {
                    DataRowView rv = dsJobDetails.Tables["SelectJobNo"].DefaultView[0];
                    txtjno.Text = rv["JobNo"].ToString();//JobNo
                    Session["JobNo"] = rv["JobNo"].ToString();
                    Session["jobid"] = rv["JobCreationId"].ToString();
                    txtJobReceivedDate.Text = rv["JobReceivedDate"].ToString();//JobReceivedDate
                    //ddlDocFillingStatus.SelectedValue = rv["DocFillingStatus"].ToString();//DocFillingStatus
                    ddlMode.SelectedValue = rv["Mode"].ToString();//Mode

                    string AirSea = "";
                    if (ddlMode.SelectedValue == "Air")
                        AirSea = "A";
                    else
                        AirSea = "S";
                    BindCustomHouse(AirSea);
                    //ddlFilling.SelectedValue = rv["Filling"].ToString();//Filling
                    ddlCustom.SelectedValue = rv["Custom"].ToString();//Custom
                    ddlBEType.SelectedValue = rv["BEType"].ToString();//BEType
                    txtBENo.Text = rv["BENo"].ToString();//BENo
                    txtBEDate.Text = rv["BEDate"].ToString();//BEDate
                    //txtTotalInvoiceValue.Text = rv["TotalInvoiceValue"].ToString();//TotalInvoiceValue
                    //txtTotalNoOfInvoice.Text = rv["TotalNoofInvoice"].ToString();//TotalNoofInvoice
                    //ddlCurrency.SelectedValue = rv["Currency"].ToString();//Currency
                    //if (rv["BEType"].ToString() == "X")
                    //{
                    //    btnsave.Visible = true;
                    //    tcBondCertification.Visible = true;
                    //    TabPanel3.Visible = true;
                    //    BindExBond(rv["JobNo"].ToString());
                    //    btnExBondUpdate.Visible = false;
                    //}

                    //ChkClearanceBond.Checked = Convert.ToBoolean(rv["BondApply"].ToString());//BondApply
                    //BindBondType();
                    //if (Convert.ToBoolean(rv["BondApply"].ToString()) == true)
                    //{
                    //    tcBondCertification.Visible = true;
                    //    BindGridBondType(JobNo);
                    //    BindGridCertification(JobNo);
                    //}
                    //else if (rv["BEType"].ToString() == "X")
                    //{
                    //    tcBondCertification.Visible = true;
                    //}
                    //else
                    //{
                    //    tcBondCertification.Visible = false;
                    //}
                }
                GetImportDetails(JobNo);//Importer Details
            }
            catch (Exception ex)
            {
                string message = ex.Message;
            }
        }

        public void GetImportDetails(string JobNo)
        {
            try
            {
                DataSet ds = objimporter.GetImporterDetails(txtjno.Text);
                if (ds.Tables["ImportDetails"].Rows.Count != 0)
                {
                    // btnSave.Visible = false;
                    // btnUpdate.Visible = true;
                    DataRowView row = ds.Tables["ImportDetails"].DefaultView[0];
                    // Session["ImporterID"] = row["ImporterId"].ToString();
                    txtImporter.Text = row["Importer"].ToString();//Importer
                    lblIECodeNo.Text = row["IECodeNo"].ToString();//IECodeNo
                    lblImpBranchNo.Text = row["BranchSno"].ToString();//BranchSno
                    lblAddress.Text = row["Address"].ToString();//Address
                    lblCity.Text = row["City"].ToString();//City
                    lblStateImp.Text = row["State"].ToString();//State
                    txtImporterRefNo.Text = row["ImporterRefNo"].ToString();//ImporterRefNo
                    txtBEHeading.Text = row["BEHeading"].ToString();//BEHeading

                    string SingleConsignor = row["SingleConsignor"].ToString();//SingleConsignor
                    chkCon.Checked = Convert.ToBoolean(row["SingleConsignor"].ToString());

                    txtConsignor.Text = row["Consignor"].ToString();//Consignor
                    txtCnrShortName.Text = row["ConsignorShName"].ToString();//ConsignorShortName
                    txtAddress.Text = row["ConsignorAddress"].ToString();//ConsignorAddress
                    txtCity.Text = row["ConsignorCity"].ToString();//ConsignorCity
                    txtCountry.Text = row["ConsignorCountry"].ToString();//ConsignorCountry

                    string HighSeaSale = row["HighSeaSale"].ToString();//HighSeaSale
                    ChkHSS.Checked = Convert.ToBoolean(row["HighSeaSale"].ToString());

                    txtSellerName.Text = row["SellerName"].ToString();//SellerName
                    txtSelerShortName.Text = row["ShortSellerName"].ToString();//ShortSellerName
                    lblIECodeNoHigh.Text = row["HighIECode"].ToString();//HighIECode
                    lblSellerBranchNo.Text = row["HighBranchSno"].ToString();//HighBranchSno
                    lblAddressHigh.Text = row["HighAddress"].ToString();//HighAddress
                    lblCityHigh.Text = row["HighCity"].ToString();//HighCity
                    lblStateHigh.Text = row["HighState"].ToString();//HighState
                    lblZipCodeHigh.Text = row["HighZipCode"].ToString();//HighZipCode
                    lblunderSec46.Text = row["underSec46"].ToString();//underSec46
                    lblFirstChk.Text = row["FirstChk"].ToString();//FirstChk
                    lblKachha.Text = row["Kachha"].ToString();//Kachha
                    lblUnderSec48.Text = row["UnderSec48"].ToString();//UnderSec48
                    lblGreenChannel.Text = row["GreenChannel"].ToString();//GreenChannel

                    string unsec46 = row["ChkUnderSec46"].ToString();//ChkUnderSec46
                    string fischk = row["ChkFirstChk"].ToString();//ChkFirstChk
                    string kacha = row["ChkKachha"].ToString();//ChkKachha
                    string unsec48 = row["ChkUnderSec48"].ToString();//ChkUnderSec48
                    string Greench = row["ChkGreen"].ToString();//ChkGreen

                    ddlImpType.SelectedValue = row["ImporterType"].ToString();//ImporterType
                    lblShortName.Text = row["ImpShortName"].ToString();//ImpShortName
                    lblZipCode.Text = row["ZipCode"].ToString();//ZipCode
                    // txtSelerShortName.Text = row["HighShortName"].ToString();//HighShortName

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
                    if (Greench == "Yes")
                    {
                        ChkGreenChannel.Checked = true;
                    }
                }
            }
            catch (Exception ex)
            {
                string message = ex.Message;
            }
        }

        protected void gvBondType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (gvBondType.SelectedRow.Cells[2].Text != null)
            {
                lblSave.Text = "Update";
                //btnBondSave.Visible = false;
                //btnBondUpdate.Visible = true;
                Session["BondId"] = gvBondType.SelectedRow.Cells[1].Text;
                ddlBondType.SelectedValue = gvBondType.SelectedRow.Cells[3].Text;
                txtBondNo.Text = gvBondType.SelectedRow.Cells[4].Text;
                ddlRegisteredAt.SelectedValue = gvBondType.SelectedRow.Cells[5].Text;
            }
        }
        protected void ChkClearanceBond_CheckedChanged(object sender, EventArgs e)
        {
            if (ChkClearanceBond.Checked == true && ddlBEType.SelectedValue == "X")
            {
                tcBondCertification.Visible = true;
                TabPanel1.Visible = true;
                TabPanel2.Visible = true;
                TabPanel3.Visible = true;

            }

            else if (ChkClearanceBond.Checked == true && (ddlBEType.SelectedValue == "W" || ddlBEType.SelectedValue == "H"))
            {
                tcBondCertification.Visible = true;
                TabPanel1.Visible = true;
                TabPanel2.Visible = true;
                TabPanel3.Visible = false;

            }
            else if (ChkClearanceBond.Checked == false && ddlBEType.SelectedValue == "X")
            {
                tcBondCertification.Visible = true;
                TabPanel1.Visible = false;
                TabPanel2.Visible = false;
                TabPanel3.Visible = true;
            }
            else if (ChkClearanceBond.Checked == false && ddlBEType.SelectedValue != "X" && (ddlBEType.SelectedValue == "H" || ddlBEType.SelectedValue == "W"))
            {
                tcBondCertification.Visible = false;

            }

        }


        protected void btnBondSave_Click(object sender, EventArgs e)
        {
            int result = 0;
            try
            {
                string jobno = string.Empty;
                string bondtype = string.Empty;
                string bondno = string.Empty;
                string registeredat = string.Empty;
                jobno = txtjno.Text;
                bondtype = ddlBondType.SelectedValue;
                bondno = txtBondNo.Text;
                registeredat = ddlRegisteredAt.SelectedValue;
                if (bondtype == "0" || bondno == "" || registeredat == "")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Please fill the vaild fields');", true);
                }
                else
                {
                    result = objJobNo.InsertBondType(jobno, bondtype, bondno, registeredat);
                    if (result == 1)
                    {
                        BindGridBondType(jobno);
                        ClearBondTypes();
                        lblSave.Text = "Save";
                        //btnBondSave.Visible = true;
                        //btnBondUpdate.Visible = false;
                    }
                }
            }
            catch
            {

            }
        }

        private void ClearBondTypes()
        {
            ddlBondType.SelectedValue = "0";
            txtBondNo.Text = string.Empty;
        }
        private void BindGridBondType(string jobno)
        {
            DataSet ds = objJobNo.SelectBond(jobno);

            if (ds.Tables["data"].Rows.Count != 0)
            {
                gvBondType.DataSource = ds;
                gvBondType.DataBind();
            }
        }

        protected void btnBondUpdate_Click(object sender, EventArgs e)
        {
            int result = 0;
            try
            {
                string id = (string)Session["BondId"];
                string jobno = string.Empty;
                string bondtype = string.Empty;
                string bondno = string.Empty;
                string registeredat = string.Empty;
                jobno = txtjno.Text;
                bondtype = ddlBondType.SelectedValue;
                bondno = txtBondNo.Text;
                registeredat = ddlRegisteredAt.SelectedValue;

                if (bondtype == "0" || bondno == "" || registeredat == "")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Please fill the vaild fields');", true);
                }
                else
                {
                    result = objJobNo.UpdateBondType(id, jobno, bondtype, bondno, registeredat);
                    if (result == 1)
                    {
                        BindGridBondType(jobno);
                        ClearBondTypes();
                        lblSave.Text = "Save";
                        btnBondSave.Visible = true;
                        btnBondUpdate.Visible = false;
                    }
                }
            }
            catch
            {

            }
        }

        protected void gvCertification_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (gvCertification.SelectedRow.Cells[3].Text != null)
            {
                btnCertificationSave.Visible = false;
                btnCertificationUpdate.Visible = true;

                Session["CertificationId"] = gvCertification.SelectedRow.Cells[2].Text;

                txtCertiNo.Text = gvCertification.SelectedRow.Cells[4].Text;
                if (txtCertiNo.Text == "&nbsp;")
                {
                    txtCertiNo.Text = "";
                }
                ddlCerType.SelectedValue = gvCertification.SelectedRow.Cells[5].Text;
                txtCertiDate.Text = gvCertification.SelectedRow.Cells[6].Text;
                if (txtCertiDate.Text == "&nbsp;")
                {
                    txtCertiDate.Text = "";
                }
                txtCommisionRate.Text = gvCertification.SelectedRow.Cells[7].Text;
                if (txtCommisionRate.Text == "&nbsp;")
                {
                    txtCommisionRate.Text = "";
                }
                txtDivision.Text = gvCertification.SelectedRow.Cells[8].Text;
                txtCommisionRate.Text = gvCertification.SelectedRow.Cells[7].Text;
                if (txtDivision.Text == "&nbsp;")
                {
                    txtDivision.Text = "";
                }
                txtRange.Text = gvCertification.SelectedRow.Cells[9].Text;
                if (txtRange.Text == "&nbsp;")
                {
                    txtRange.Text = "";
                }

            }
        }

        protected void btnCertificationSave_Click(object sender, EventArgs e)
        {
            int result = 0;
            try
            {
                btnCertificationUpdate.Visible = false;
                string jobno = string.Empty;
                string certificationNo = string.Empty;
                string Certificationtype = string.Empty;
                string date = string.Empty;
                string rate = string.Empty;
                string division = string.Empty;
                string range = string.Empty;
                jobno = txtjno.Text;
                certificationNo = txtCertiNo.Text;
                Certificationtype = ddlCerType.SelectedValue;
                date = txtCertiDate.Text;
                rate = txtCommisionRate.Text;
                division = txtDivision.Text;
                range = txtRange.Text;

                if (certificationNo == "" || Certificationtype == "" || date == "")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Please fill the vaild fields');", true);
                }
                else
                {
                    result = objJobNo.InsertBondCertification(jobno, certificationNo, Certificationtype, date, rate, division, range);
                    if (result == 1)
                    {
                        BindGridCertification(jobno);
                        ClearCertification();
                        btnCertificationSave.Visible = true;
                        btnCertificationUpdate.Visible = false;
                    }
                }
            }
            catch
            {

            }
        }

        private void ClearCertification()
        {
            txtCertiNo.Text = txtCertiDate.Text = txtCommisionRate.Text = txtDivision.Text = txtRange.Text = string.Empty;
        }

        private void BindGridCertification(string jobno)
        {
            DataSet ds = objJobNo.SelectCertification(jobno);

            if (ds.Tables["data"].Rows.Count != 0)
            {
                gvCertification.DataSource = ds;
                gvCertification.DataBind();
            }
            else
            {
                gvCertification.DataSource = "";
                gvCertification.DataBind();
            }
        }

        protected void btnCertificationUpdate_Click(object sender, EventArgs e)
        {
            int result = 0;
            try
            {
                string id = (string)Session["CertificationId"];
                string jobno = string.Empty;
                string certificationNo = string.Empty;
                string Certificationtype = string.Empty;
                string date = string.Empty;
                string rate = string.Empty;
                string division = string.Empty;
                string range = string.Empty;
                jobno = txtjno.Text;
                certificationNo = txtCertiNo.Text;
                Certificationtype = ddlCerType.SelectedValue;
                date = txtCertiDate.Text;
                rate = txtCommisionRate.Text;
                division = txtDivision.Text;
                range = txtRange.Text;

                if (certificationNo == "" || Certificationtype == "" || date == "")
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Please fill the vaild fields');", true);
                }
                else
                {
                    result = objJobNo.UpdateBondCertification(id, jobno, certificationNo, Certificationtype, date, rate, division, range);
                    if (result == 1)
                    {
                        BindGridCertification(jobno);
                        ClearCertification();
                        btnCertificationSave.Visible = true;
                        btnCertificationUpdate.Visible = false;
                    }
                }
            }
            catch
            {

            }
        }

        protected void btnNewjob_Click(object sender, EventArgs e)
        {
            Response.Redirect("frmJobCreation.aspx?Mode=New Job");
        }

        protected void btnExit_Click(object sender, EventArgs e)
        {
            Response.Redirect("frmJobCreation.aspx?Mode=Search Job");
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            JobNo = txtSearch.Text;
            GridLoad(JobNo);
        }

        protected void btnSearchJob_Click(object sender, EventArgs e)
        {
            try
            {
                DataSet ds = objJobNo.JobNoList(txtjno.Text);
                if (ds.Tables["JobNoList"].Rows.Count != 0)
                {
                    JobImportDetails(txtjno.Text);
                    btnUpdate.Visible = true;
                    btnsave.Visible = false;
                }
                else
                {
                    btnUpdate.Visible = false;
                    btnsave.Visible = true;
                    clear();
                    gvBondType.DataBind();
                    gvCertification.DataBind();
                }
            }
            catch
            {
            }
        }

        protected void ddlMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            string AirSea = "";
            if (ddlMode.SelectedValue == "Air")
                AirSea = "A";
            else
                AirSea = "S";
            BindCustomHouse(AirSea);
        }

        protected void chkCon_CheckedChanged(object sender, EventArgs e)
        {
            if (chkCon.Checked == true)
            {
                txtConsignor.Enabled = true;
                txtCnrShortName.Enabled = true;
                txtCity.Enabled = true;
                txtCountry.Enabled = true;
            }
            else
            {
                txtConsignor.Enabled = false;
                txtCnrShortName.Enabled = false;
                txtCity.Enabled = false;
                txtCountry.Enabled = false;
            }
        }

        protected void ChkHSS_CheckedChanged(object sender, EventArgs e)
        {
            if (ChkHSS.Checked == true)
            {
                txtSellerName.Enabled = true;
                txtSelerShortName.Enabled = true;
                lblSellerBranchNo.Enabled = true;
                lblIECodeNoHigh.Enabled = true;
                lblAddressHigh.Enabled = true;
                lblCityHigh.Enabled = true;
                lblStateHigh.Enabled = true;
                lblZipCodeHigh.Enabled = true;
            }
            else
            {
                txtSellerName.Enabled = false;
                txtSelerShortName.Enabled = false;
                lblSellerBranchNo.Enabled = false;
                lblIECodeNoHigh.Enabled = false;
                lblAddressHigh.Enabled = false;
                lblCityHigh.Enabled = false;
                lblStateHigh.Enabled = false;
                lblZipCodeHigh.Enabled = false;
            }
        }

        protected void btnDelete_Click(object sender, ImageClickEventArgs e)
        {
            ImageButton btndel = sender as ImageButton;
            GridViewRow row = (GridViewRow)btndel.NamingContainer;
            string TransId = row.Cells[2].Text;
            string jobno = row.Cells[3].Text;
            int i = objJobNo.DeleteBondCertification(TransId);
            if (i == 1)
            {
                BindGridCertification(jobno);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "redirect", "alert('Deleted Successfully');", true);
            }
        }

        protected void ddlBEType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ChkClearanceBond.Checked == true && ddlBEType.SelectedValue == "X")
            {
                tcBondCertification.Visible = true;
                TabPanel1.Visible = true;
                TabPanel2.Visible = true;
                TabPanel3.Visible = true;
            }
            else if (ChkClearanceBond.Checked == true && (ddlBEType.SelectedValue == "W" || ddlBEType.SelectedValue == "H"))
            {
                tcBondCertification.Visible = true;
                TabPanel1.Visible = true;
                TabPanel2.Visible = true;
                TabPanel3.Visible = false;
            }
            else if (ChkClearanceBond.Checked == false && ddlBEType.SelectedValue == "X")
            {
                tcBondCertification.Visible = true;
                TabPanel1.Visible = false;
                TabPanel2.Visible = false;
                TabPanel3.Visible = true;
            }
            else if (ChkClearanceBond.Checked == false && ddlBEType.SelectedValue != "X" && (ddlBEType.SelectedValue == "H" || ddlBEType.SelectedValue == "W"))
            {
                tcBondCertification.Visible = false;
            }
        }

        protected void btnEXBondSave_Click(object sender, EventArgs e)
        {
            int result = 0;
            try
            {
                string JobNo, ExBondFDate, ExBondTDate, EXBondJobNo, EXBondBLNO, EXBondBLDate, EXBondNo, EXBondDate, EXBondExpiryDate, EXWareHouse, ExCode;
                JobNo = txtjno.Text;
                ExBondFDate = txtbondingfrom.Text;
                ExBondTDate = txtbondingto.Text;
                EXBondJobNo = txtbondingjobno.Text;
                EXBondBLNO = txtbondblno.Text;
                EXBondBLDate = txtdatebillno.Text;
                EXBondNo = txtExBondNo.Text;
                EXBondDate = txtbondnodate.Text;
                EXBondExpiryDate = txtexpirydate.Text;
                EXWareHouse = txtwarehouse.Text;
                ExCode = txtcode.Text;

                result = objJobNo.InbondJobCopy(EXBondJobNo, txtjno.Text);
                int resultupdate = objJobNo.update(txtjno.Text);

                if (resultupdate != 0)
                {
                    result = objJobNo.InsertEXBond(JobNo, ExBondFDate, ExBondTDate, EXBondJobNo, EXBondBLNO, EXBondBLDate, EXBondNo, EXBondDate, EXBondExpiryDate, EXWareHouse, ExCode);
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "redirect", "alert('InBond Saved Successfully'); window.location.href='frmJobCreation.aspx?JobMode=" + txtjno.Text + "&Mode=Edit Job';", true);
                   // Response.Redirect("frmJobCreation.aspx?JobMode=" + txtjno.Text + "&Mode=Edit Job");
                }

                    //result = objJobNo.InsertEXBond(JobNo, ExBondFDate, ExBondTDate, EXBondJobNo, EXBondBLNO, EXBondBLDate, EXBondNo, EXBondDate, EXBondExpiryDate, EXWareHouse, ExCode);
                    //if (result == 1)
                    //{
                    //    BindExBond(JobNo);
                    //    ClearEXBond();
                    //}
                //}
            }
            catch(Exception ex)
            {
                string Message = ex.Message;
            }
        }

        protected void btnExBondUpdate_Click(object sender, EventArgs e)
        {
            int result = 0;
            try
            {
                int TransId = new int();
                TransId = (Int32)Session["ExBondTransId"];
                string JobNo, ExBondFDate, ExBondTDate, EXBondJobNo, EXBondBLNO, EXBondBLDate, EXBondNo, EXBondDate, EXBondExpiryDate, EXWareHouse, ExCode;
                JobNo = txtjno.Text;
                ExBondFDate = txtbondingfrom.Text;
                ExBondTDate = txtbondingto.Text;
                EXBondJobNo = txtbondingjobno.Text;
                EXBondBLNO = txtbondblno.Text;
                EXBondBLDate = txtdatebillno.Text;
                EXBondNo = txtExBondNo.Text;
                EXBondDate = txtbondnodate.Text;
                EXBondExpiryDate = txtexpirydate.Text;
                EXWareHouse = txtwarehouse.Text;
                ExCode = txtcode.Text;
                //if (ExBondFDate == "" || EXBondNo == "" || EXBondExpiryDate == "")
                //{
                //    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Please fill the vaild fields');", true);
                //}
                //else
                //{
                    result = objJobNo.UpdateEXBond(TransId, JobNo, ExBondFDate, ExBondTDate, EXBondJobNo, EXBondBLNO, EXBondBLDate, EXBondNo, EXBondDate, EXBondExpiryDate, EXWareHouse, ExCode);
                    if (result == 1)
                    {
                        BindExBond(JobNo);
                        ClearEXBond();
                    }
               // }
            }
            catch (Exception ex)
            {

            }
        }

        private void BindExBond(string jobno)
        {
            DataSet ds = objJobNo.SelectEXBond(jobno);

            if (ds.Tables["data"].Rows.Count != 0)
            {
                gvEXBond.DataSource = ds;
                gvEXBond.DataBind();
            }
            else
            {
                gvEXBond.DataSource = "";
                gvEXBond.DataBind();
            }
        }

        private void ClearEXBond()
        {
             txtbondingfrom.Text = "";
             txtbondingto.Text = "";
             txtbondingjobno.Text = "";
             txtbondblno.Text = "";
             txtdatebillno.Text = "";
             txtExBondNo.Text = "";
             txtbondnodate.Text = "";
             txtexpirydate.Text = "";
             txtwarehouse.Text = "";
             txtcode.Text = "";
        }

        protected void gvEXBond_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnEXBondSave.Visible = false;
            btnExBondUpdate.Visible=true;
            int TransId =Convert.ToInt32(gvEXBond.SelectedRow.Cells[1].Text);
            Session["ExBondTransId"] = TransId;
            string EXQuery = "select * from  T_ImportExBondDetails where TransId = '" + TransId + "'";
            DataSet ds = obj1.GetDataSet(EXQuery);
            if (ds.Tables["Table"].Rows.Count != 0)
            {
                DataRowView rv = ds.Tables["Table"].DefaultView[0];
                txtbondingfrom.Text = rv["ExBondFDate"].ToString();
                txtbondingto.Text = rv["ExBondTDate"].ToString();
                txtbondingjobno.Text = rv["EXBondJobNo"].ToString();
                txtbondblno.Text = rv["EXBondBLNO"].ToString();
                txtdatebillno.Text = rv["EXBondBLDate"].ToString();
                txtExBondNo.Text = rv["EXBondNo"].ToString();
                txtbondnodate.Text = rv["EXBondDate"].ToString();
                txtexpirydate.Text = rv["EXBondExpiryDate"].ToString();
                txtwarehouse.Text = rv["EXWareHouse"].ToString();
                txtcode.Text = rv["ExCode"].ToString();
            }
        }

        protected void txtbondingjobno_TextChanged(object sender, EventArgs e)
        {
            string EXQuery = "select BENo,BEDate from  T_JobCreation where JobNo = '" + txtbondingjobno.Text + "'";
            DataSet ds = obj1.GetDataSet(EXQuery);
            if (ds.Tables["Table"].Rows.Count != 0)
            {
                DataRowView rv = ds.Tables["Table"].DefaultView[0];
                txtbondblno.Text = rv["BENo"].ToString();
                txtdatebillno.Text = rv["BEDate"].ToString();
            }
        }

        protected void txtImporter_TextChanged(object sender, EventArgs e)
        {

        }

    }
}