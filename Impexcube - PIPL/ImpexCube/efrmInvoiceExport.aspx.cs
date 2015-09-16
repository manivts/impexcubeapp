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
using VTS.ImpexCube.Data;

namespace ImpexCube
{
    public partial class efrmInvoiceExport : System.Web.UI.Page
    {
        ETInvoiceBL objETInvoiceBL = new ETInvoiceBL();
        CommonDL objCommonDL = new CommonDL();

        #region GlobalDeclaration

        string JobNo = string.Empty;
        string InvoiceNo = string.Empty;
        string InvoiceDate = string.Empty;
        string TOI = string.Empty;
        string Currency = string.Empty;
        string CurrencyRate = string.Empty;
        string InvoiceValue = string.Empty;
        string ProductValue = string.Empty;
        string InvoiceINRAmount = string.Empty;
        string UnitPriceIncludes = string.Empty;
        string ShowFOBIn = string.Empty;
        string FreightCurrency = string.Empty;
        string FreightExRate = string.Empty;
        string FreightRate = string.Empty;
        string FreightAmount = string.Empty;
        string FreightINRAmount = string.Empty;
        string InsuranceCurrency = string.Empty;
        string InsuranceExRate = string.Empty;
        string InsuranceRate = string.Empty;
        string InsuranceAmount = string.Empty;
        string InsuranceINRAmount = string.Empty;
        string DiscountCurrency = string.Empty;
        string DiscountExRate = string.Empty;
        string DiscountRate = string.Empty;
        string DiscountAmount = string.Empty;
        string DiscountINRAmount = string.Empty;
        string CommissionCurrency = string.Empty;
        string CommissionExRate = string.Empty;
        string CommissionRate = string.Empty;
        string CommissionAmount = string.Empty;
        string CommissionINRAmount = string.Empty;
        string OtherDeductionCurrency = string.Empty;
        string OtherDeductionExRate = string.Empty;
        string OtherDeductionRate = string.Empty;
        string OtherDeductionAmount = string.Empty;
        string OtherDeductionINRAmount = string.Empty;
        string PackingFOBChargesCurrency = string.Empty;
        string PackingFOBChargesExRate = string.Empty;
        string PackingFOBChargesRate = string.Empty;
        string ExportContractNo = string.Empty;
        string ExportContractDate = string.Empty;
        string NatureOfPayment = string.Empty;
        string PaymentPeriod = string.Empty;
        string CreatedBy = string.Empty;
        string CreatedDate = string.Empty;
        string ModifiedBy = string.Empty;
        string ModifiedDate = string.Empty;
        string ExJobNo = string.Empty;

        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["Mode"] == "" || Request.QueryString["Mode"] == null)
                {
                    ExJobNo = (string)Session["ExJobNo"];
                }
                else if (Request.QueryString["Mode"] == "Direct")
                {
                    ExJobNo = string.Empty;
                }
                filldropdown();
                BindCurrency();
                if (ExJobNo == "" || ExJobNo == null || ExJobNo == "~Select~")
                {
                    gvInvoiceExport.Visible = true;
                    btnUpdate.Visible = false;
                }
                else
                {
                    btnAdd.Visible = true;
                    btnUpdate.Visible = false;
                    GetJobDetails();
                }
            }
        }

        private void GetJobDetails()
        {
            ddlJobNo.SelectedValue = ExJobNo;
            DataSet ds = new DataSet();
            ds = objETInvoiceBL.GetInvoiceJobDetails(ddlJobNo.SelectedValue);
            if (ds.Tables["Table"].Rows.Count != 0)
            {
                gvInvoiceExport.DataSource = ds;
                gvInvoiceExport.DataBind();
                GetJobNo();
            }
            else
            {
                gvInvoiceExport.DataSource = null;
                gvInvoiceExport.DataBind();
                GetJobNo();
            }
        }

        public void BindCurrency()
        {
            DataSet ds = objCommonDL.GetCurrencyDetails();

            if (ds.Tables["Invoice"].Rows.Count != 0)
            {
                ddlInvoiceCurrency.DataSource = ds;
                ddlInvoiceCurrency.DataTextField = "CurrencyShortName";
                ddlInvoiceCurrency.DataValueField = "CurrencyShortName";
                ddlInvoiceCurrency.DataBind();

                ddlshoefob.DataSource = ds;
                ddlshoefob.DataTextField = "CurrencyShortName";
                ddlshoefob.DataValueField = "CurrencyShortName";
                ddlshoefob.DataBind();


                ddlFreight.DataSource = ds;
                ddlFreight.DataTextField = "CurrencyShortName";
                ddlFreight.DataValueField = "CurrencyShortName";
                ddlFreight.DataBind();

                ddlInsurace.DataSource = ds;
                ddlInsurace.DataTextField = "CurrencyShortName";
                ddlInsurace.DataValueField = "CurrencyShortName";
                ddlInsurace.DataBind();

                ddlDiscountcurr.DataSource = ds;
                ddlDiscountcurr.DataTextField = "CurrencyShortName";
                ddlDiscountcurr.DataValueField = "CurrencyShortName";
                ddlDiscountcurr.DataBind();

                ddlCommCurr.DataSource = ds;
                ddlCommCurr.DataTextField = "CurrencyShortName";
                ddlCommCurr.DataValueField = "CurrencyShortName";
                ddlCommCurr.DataBind();

                ddlOthDedcurr.DataSource = ds;
                ddlOthDedcurr.DataTextField = "CurrencyShortName";
                ddlOthDedcurr.DataValueField = "CurrencyShortName";
                ddlOthDedcurr.DataBind();

                ddlPackFobCurr.DataSource = ds;
                ddlPackFobCurr.DataTextField = "CurrencyShortName";
                ddlPackFobCurr.DataValueField = "CurrencyShortName";
                ddlPackFobCurr.DataBind();

                ////Charge Master
                //ddlChargeType.DataSource = dsChargeType;
                //ddlChargeType.DataTextField = "charge_desc";
                //ddlChargeType.DataValueField = "charge_desc";
                //ddlChargeType.DataBind();
            }

        }

        public void filldropdown()
        {
            DataSet ds = new DataSet();
            string quer = "select * from E_M_JobCreation";
            ds = objCommonDL.GetDataSet(quer);
            ddlJobNo.DataSource = ds;
            ddlJobNo.DataTextField = "JobNo";
            ddlJobNo.DataValueField = "JobNo";
            ddlJobNo.DataBind();
        }

        protected void btnFreightInsure_Click(object sender, EventArgs e)
        {
            PanelOtherInfo.Visible = false;
            PanelFreight.Visible = true;
        }

        protected void btnOtherInfo_Click(object sender, EventArgs e)
        {
            PanelOtherInfo.Visible = true;
            PanelFreight.Visible = false;
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                if (ddlJobNo.SelectedItem.Text != "~Select~")
                {
                    JobNo = ddlJobNo.SelectedItem.Text;
                    InvoiceNo = txtinvno.Text;
                    InvoiceDate = txtdate.Text;
                    TOI = ddlTermsofInvoice.SelectedItem.Text;
                    Currency = ddlInvoiceCurrency.SelectedItem.Text;
                    CurrencyRate = txtexcrate.Text;
                    InvoiceValue = txtinvval.Text;
                    ProductValue = txtprodval.Text;
                    InvoiceINRAmount = txtAmountinINR.Text;
                    CreatedBy = (string)Session["USER-NAME"];
                    CreatedDate = DateTime.Now.ToString();

                    int result = objETInvoiceBL.SaveInvoiceDetails(JobNo, InvoiceNo, InvoiceDate, TOI, Currency, CurrencyRate, InvoiceValue, ProductValue, InvoiceINRAmount, CreatedBy, CreatedDate);
                    ionvoicegrid();

                    Clear();
                    if (result == 1)
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Saved Successfully'); ", true);
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Please Select the JobNo..'); ", true);
                }
            }

            catch (Exception ex)
            {
            }
        }

        private void Clear()
        {
            txtinvno.Text = "";
            txtdate.Text = "";
            txtexcrate.Text = "";
            txtinvval.Text = "";
            txtprodval.Text = "";
            txtAmountinINR.Text = "";
            ddlTermsofInvoice.SelectedIndex = 0;
            ddlInvoiceCurrency.SelectedIndex = 0;
        }

        public void ionvoicegrid()
        {
            //DataSet ds = objETInvoiceBL.GetInvoiceDetails(JobNo,InvoiceNo);
            //Written by Zameer for Gridview Binding
            DataSet ds = objETInvoiceBL.GetInvoiceJobDetails(JobNo);

            if (ds.Tables["Table"].Rows.Count != 0)
            {
                gvInvoiceExport.DataSource = ds;
                gvInvoiceExport.DataBind();
            }
        }

        protected void gvInvoiceExport_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                btnAdd.Visible = false;
                btnUpdate.Visible = true;
                InvoiceNo = gvInvoiceExport.SelectedRow.Cells[1].Text.ToString();
                JobNo = ddlJobNo.SelectedItem.Text;
                DataSet ds = objETInvoiceBL.GetInvoiceDetails(JobNo, InvoiceNo);
                if (ds.Tables["Table"].Rows.Count != 0)
                {
                    DataRowView dr = ds.Tables["Table"].DefaultView[0];
                    hdnInvoice.Value = dr["ID"].ToString();
                    txtinvno.Text = dr["InvoiceNo"].ToString();


                    txtdate.Text = dr["InvoiceDate"].ToString();
                    ddlTermsofInvoice.SelectedIndex = ddlTermsofInvoice.Items.IndexOf(ddlTermsofInvoice.Items.FindByText(dr["TOI"].ToString()));
                    ddlInvoiceCurrency.SelectedIndex = ddlInvoiceCurrency.Items.IndexOf(ddlInvoiceCurrency.Items.FindByText(dr["Currency"].ToString()));

                    lblCurrency.Text = dr["Currency"].ToString();
                    lblExRate.Text = dr["CurrencyRate"].ToString();
                    lblInvNo.Text = dr["InvoiceNo"].ToString();
                    lblInvValue.Text = dr["InvoiceValue"].ToString();

                    //ddlTermsofInvoice.SelectedItem.Text = dr["TOI"].ToString();
                    //ddlInvoiceCurrency.SelectedItem.Text = dr["Currency"].ToString();
                    txtexcrate.Text = dr["CurrencyRate"].ToString();
                    txtinvval.Text = dr["InvoiceValue"].ToString();
                    txtprodval.Text = dr["ProductValue"].ToString();
                    txtAmountinINR.Text = dr["InvoiceINRAmount"].ToString();

                    if (dr["UnitPriceIncludes"] != DBNull.Value)
                    {
                        ddlUnitprice.SelectedItem.Text = dr["UnitPriceIncludes"].ToString();
                    }
                    if (dr["ShowFOBIn"] != DBNull.Value)
                    {
                        ddlshoefob.SelectedItem.Text = dr["ShowFOBIn"].ToString();
                    }

                    if (dr["FreightCurrency"] != DBNull.Value)
                    {
                        ddlFreight.SelectedItem.Text = dr["FreightCurrency"].ToString();
                        txtfreight1.Text = dr["FreightExRate"].ToString();
                        txtfreight2.Text = dr["FreightRate"].ToString();
                        txtfreighamount.Text = dr["FreightAmount"].ToString();
                        txtfreighamountINR.Text = dr["FreightINRAmount"].ToString();
                    }

                    if (dr["InsuranceCurrency"] != DBNull.Value)
                    {
                        ddlInsurace.SelectedItem.Text = dr["InsuranceCurrency"].ToString();
                        txtinsurance1.Text = dr["InsuranceExRate"].ToString();
                        txtinsurerate.Text = dr["InsuranceRate"].ToString();
                        txtinsureamount.Text = dr["InsuranceAmount"].ToString();
                        txtinsureamountINR.Text = dr["InsuranceINRAmount"].ToString();
                    }

                    if (dr["DiscountCurrency"] != DBNull.Value)
                    {
                        ddlDiscountcurr.SelectedItem.Text = dr["DiscountCurrency"].ToString();
                        txtDiscExcRate.Text = dr["DiscountExRate"].ToString();
                        txtDiscRate.Text = dr["DiscountRate"].ToString();
                        txtDiscAmount.Text = dr["DiscountAmount"].ToString();
                        txtDiscAmountINR.Text = dr["DiscountINRAmount"].ToString();
                    }


                    if (dr["CommissionCurrency"] != DBNull.Value)
                    {
                        ddlCommCurr.SelectedItem.Text = dr["CommissionCurrency"].ToString();
                        txtCommExcRate.Text = dr["CommissionExRate"].ToString();
                        txtCommRate.Text = dr["CommissionRate"].ToString();
                        txtCommAmount.Text = dr["CommissionAmount"].ToString();
                        txtCommAmountINR.Text = dr["CommissionINRAmount"].ToString();
                    }



                    if (dr["OtherDeductionCurrency"] != DBNull.Value)
                    {
                        ddlOthDedcurr.SelectedValue = dr["OtherDeductionCurrency"].ToString();
                        txtOthDedExcRate.Text = dr["OtherDeductionExRate"].ToString();
                        txtOthDedRate.Text = dr["OtherDeductionRate"].ToString();
                        txtOthDedAmount.Text = dr["OtherDeductionAmount"].ToString();
                        txtOthDedAmountINR.Text = dr["OtherDeductionINRAmount"].ToString();
                    }


                    if (dr["PackingFOBChargesCurrency"] != DBNull.Value)
                    {
                        ddlPackFobCurr.SelectedItem.Text = dr["PackingFOBChargesCurrency"].ToString();
                        txtPackFobExcRate.Text = dr["PackingFOBChargesExRate"].ToString();
                        txtPacFobRate.Text = dr["PackingFOBChargesRate"].ToString();
                    }

                    txtExpContNoDt.Text = dr["ExportContractNo"].ToString();
                    txtExpContNoDt1.Text = dr["ExportContractDate"].ToString();
                    txtNatureOfPaym.Text = dr["NatureOfPayment"].ToString();
                    txtPaymPerd.Text = dr["PaymentPeriod"].ToString();

                    double INRAmount = 0.00;
                    double ProductValue = 0.00;
                    string Amount = lblExRate.Text;
                    double value = 0.00;
                    if (Amount != "")
                        INRAmount = Convert.ToDouble(Amount);
                    if (txtprodval.Text != "")
                        ProductValue = Convert.ToDouble(txtprodval.Text);

                    if (ProductValue != 0 && INRAmount != 0)
                        value = (INRAmount * ProductValue);

                    txtAmountinINR.Text = String.Format("{0:0.00}", value);
                }
                GetAnnexureDetails(JobNo, InvoiceNo);
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
            }
        }

        private void GetAnnexureDetails(string JobNo, string InvoiceNo)
        {
            DataSet ds = new DataSet();
            ds= objETInvoiceBL.SelectAnnexure(JobNo, InvoiceNo);
            if (ds.Tables["Table"].Rows.Count != 0)
            {
                DataRowView row = ds.Tables["Table"].DefaultView[0];
                txtEDUCode.Text = row["IECodeOfEOU"].ToString();
                txtBranchSlNo.Text = row["BranchSNo"].ToString();
                txtExaminationDate.Text = row["ExaminationDate"].ToString();
                txtExaminingOfficier.Text = row["ExaminingOfficer"].ToString();
                txtExamineDesignation.Text = row["ExaminingOfficerDesignation"].ToString();
                txtSupervisingOfficier.Text = row["SupervisingOfficer"].ToString();
                txtSupervisingDesgn.Text = row["SupervisingOfficerDesignation"].ToString();
                txtCommissionerate.Text = row["Commissionerate"].ToString();
                txtDivision.Text = row["Division"].ToString();
                txtRange.Text = row["Range"].ToString();
                txtSealNumber.Text = row["SealNumber"].ToString();
                string verification = row["VerifiedbyExaminingOfficer"].ToString();
                string forwarded = row["SampleForwarded"].ToString();

                if (verification == "True")
                {
                    chkExaminer.Checked = true;
                }
                else if (verification == "False" || verification == null || verification == "")
                {
                    chkExaminer.Checked = false;
                }

                if (forwarded == "True")
                {
                    chkSample.Checked = true;
                }
                else if (forwarded == "False" || forwarded == null || forwarded == "")
                {
                    chkSample.Checked = false;
                }
            }
            else
            {
                clearAnnexure();
            }
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                JobNo = ddlJobNo.SelectedItem.Text;
                InvoiceNo = txtinvno.Text;
                InvoiceDate = txtdate.Text;
                TOI = ddlTermsofInvoice.SelectedItem.Text;
                Currency = ddlInvoiceCurrency.SelectedItem.Text;
                CurrencyRate = txtexcrate.Text;
                InvoiceValue = txtinvval.Text;
                ProductValue = txtprodval.Text;
                InvoiceINRAmount = txtAmountinINR.Text;
                ModifiedBy = (string)Session["USER-NAME"];
                ModifiedDate = DateTime.Now.ToString();

                int result = objETInvoiceBL.UpdateInvoiceDetails(hdnInvoice.Value, JobNo, InvoiceNo, InvoiceDate, TOI, Currency, CurrencyRate, InvoiceValue, ProductValue, InvoiceINRAmount, ModifiedBy, ModifiedDate);
                ionvoicegrid();
                //Clear();
                btnAdd.Visible = true;
                btnUpdate.Visible = false;
                if (result == 1)
                {
                    //ScriptManager.RegisterStartupScript(this, this.GetType(), "redirect", "alert('Saved Successfully'); window.location.href='efrmInvoiceExport.aspx';", true);
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Updated Successfully'); ", true);
                }
            }
            catch (Exception ex)
            {
            }
        }

        protected void ddlJobNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            JobNo = ddlJobNo.SelectedItem.Text;
            DataSet ds = new DataSet();
            ds = objETInvoiceBL.GetInvoiceJobDetails(JobNo);
            if (ds.Tables["Table"].Rows.Count != 0)
            {
                gvInvoiceExport.DataSource = ds;
                gvInvoiceExport.DataBind();
                GetJobNo();                
            }
            else
            {
                gvInvoiceExport.DataSource = null;
                gvInvoiceExport.DataBind();
                GetJobNo();                
            }
        }

        private void GetJobNo()
        {
            JobNo = ddlJobNo.SelectedItem.Text;
            DataSet ds = new DataSet();
            string quer = "select * from E_M_JobCreation where jobno='" + JobNo + "'";
            ds = objCommonDL.GetDataSet(quer);
            if (ds.Tables["Table"].Rows.Count != 0)
            {
                DataRowView dr = ds.Tables["Table"].DefaultView[0];
                lblJobDate.Text = dr["JobDate"].ToString();
                lblCustom.Text = dr["CustomHouse"].ToString();
                lblMode.Text = dr["TransportMode"].ToString();
                txtTotalInvoice.Text = dr["TotalInvoiceValue"].ToString();
            }
        }

        protected void btnSave4_Click(object sender, EventArgs e)
        {
            if (ddlJobNo.SelectedItem.Text != "~Select~" || txtinvno.Text != "")
            {
                //if (txtinvno.Text != "")
                //{
                JobNo = ddlJobNo.SelectedItem.Text;
                InvoiceNo = txtinvno.Text;
                UnitPriceIncludes = ddlUnitprice.SelectedItem.Text;
                ShowFOBIn = ddlshoefob.SelectedItem.Text;
                FreightCurrency = ddlFreight.SelectedItem.Text;
                FreightExRate = txtfreight1.Text;
                FreightRate = txtfreight2.Text;
                FreightAmount = txtfreighamount.Text;
                FreightINRAmount = txtfreighamountINR.Text;
                InsuranceCurrency = ddlInsurace.SelectedItem.Text;
                InsuranceExRate = txtinsurance1.Text;
                InsuranceRate = txtinsurerate.Text;
                InsuranceAmount = txtinsureamount.Text;
                InsuranceINRAmount = txtinsureamountINR.Text;
                DiscountCurrency = ddlDiscountcurr.SelectedItem.Text;
                DiscountExRate = txtDiscExcRate.Text;
                DiscountRate = txtDiscRate.Text;
                DiscountAmount = txtDiscAmount.Text;
                DiscountINRAmount = txtDiscAmountINR.Text;
                CommissionCurrency = ddlCommCurr.SelectedItem.Text;
                CommissionExRate = txtCommExcRate.Text;
                CommissionRate = txtCommRate.Text;
                CommissionAmount = txtCommAmount.Text;
                CommissionINRAmount = txtCommAmountINR.Text;
                OtherDeductionCurrency = ddlOthDedcurr.SelectedItem.Text;
                OtherDeductionExRate = txtOthDedExcRate.Text;
                OtherDeductionRate = txtOthDedRate.Text;
                OtherDeductionAmount = txtOthDedAmount.Text;
                OtherDeductionINRAmount = txtOthDedAmountINR.Text;
                PackingFOBChargesCurrency = ddlPackFobCurr.SelectedItem.Text;
                PackingFOBChargesExRate = txtPackFobExcRate.Text;
                PackingFOBChargesRate = txtPacFobRate.Text;

                ModifiedBy = (string)Session["USER-NAME"];
                ModifiedDate = DateTime.Now.ToString();

                int result = objETInvoiceBL.Update_Invoice_Freigth_Insurance(JobNo, InvoiceNo, UnitPriceIncludes, ShowFOBIn, FreightCurrency, FreightExRate, FreightRate, FreightAmount, FreightINRAmount,
                                                        InsuranceCurrency, InsuranceExRate, InsuranceRate, InsuranceAmount, InsuranceINRAmount,
                                                        DiscountCurrency, DiscountExRate, DiscountRate, DiscountAmount, DiscountINRAmount,
                                                        CommissionCurrency, CommissionExRate, CommissionRate, CommissionAmount, CommissionINRAmount,
                                                        OtherDeductionCurrency, OtherDeductionExRate, OtherDeductionRate, OtherDeductionAmount, OtherDeductionINRAmount,
                                                        PackingFOBChargesCurrency, PackingFOBChargesExRate, PackingFOBChargesRate, ModifiedBy, ModifiedDate);
                if (result == 1)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Saved Successfully')", true);
                }
                // }
            }
        }

        protected void btnSave5_Click(object sender, EventArgs e)
        {
            JobNo = ddlJobNo.SelectedItem.Text;
            InvoiceNo = txtinvno.Text;
            ExportContractNo = txtExpContNoDt.Text;
            ExportContractDate = txtExpContNoDt1.Text;
            NatureOfPayment = txtNatureOfPaym.Text;
            PaymentPeriod = txtPaymPerd.Text;

            ModifiedBy = (string)Session["USER-NAME"];
            ModifiedDate = DateTime.Now.ToString();

            int result = objETInvoiceBL.UpdateOtherInfoInvoice(JobNo, InvoiceNo, ExportContractNo, ExportContractDate, NatureOfPayment, PaymentPeriod, ModifiedBy, ModifiedDate);

            if (result == 1)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Saved Successfully')", true);
            }
        }

        protected void btnproduct_Click(object sender, EventArgs e)
        {
            if (txtinvno.Text != "")
            {
                Session["ExJobNo"] = ddlJobNo.SelectedValue;
                Session["ExInvoice"] = txtinvno.Text;
                Response.Redirect("efrmProductExport.aspx");
            }
            else
            {
                Session["ExJobNo"] = ddlJobNo.SelectedValue;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "redirect", "alert('Please select the invoice no'); window.location.href='efrmInvoiceExport.aspx';", true);
            }
        }

        protected void btnReturn_Click(object sender, EventArgs e)
        {
            Session["ExJobNo"] = ddlJobNo.SelectedValue;
            Response.Redirect("efrmShipmentMain.aspx");
        }

        protected void btnNew_Click(object sender, EventArgs e)
        {
            BindCurrency();
            btnAdd.Visible = true;
            btnUpdate.Visible = false;
            ////txtInvoiceNo.Text = "";
            ////txtDate.Text = "";
            ////ddlTermsofInvoice.SelectedIndex = 0;
            ////ddlFreightTy.SelectedIndex = 0;
            ////ddlPayment.SelectedIndex = 0;
            ////ddlTrans.SelectedIndex = 0;
            ////ddlInvoiceCurrency.SelectedValue = lblCurrency.Text;
            ////txtExchange.Text = "";
            ////txtProductValues.Text = "";
            ////txtProductINRValues.Text = "";
            ////btnAddInvoice.Visible = true;
            ////btnUpdateInvoice.Visible = false;
        }

        protected void txtExpContNoDt_TextChanged(object sender, EventArgs e)
        {

        }

        protected void btnAnnexure_Click(object sender, EventArgs e)
        {
            PanelOtherInfo.Visible = false;
            PanelFreight.Visible = false;
            PanelAnnexure.Visible = true;
        }

        protected void btnAnnexureSave_Click(object sender, EventArgs e)
        {
            string educode = txtEDUCode.Text;
            string BranchSNo = txtBranchSlNo.Text;
            string ExaminationDate = txtExaminationDate.Text;
            string ExaminingOfficier = txtExaminingOfficier.Text;
            string ExamineDesignation = txtExamineDesignation.Text;
            string SupervisingOfficier = txtSupervisingOfficier.Text;
            string SupervisingDesgn = txtSupervisingDesgn.Text;
            string Commissionerate = txtCommissionerate.Text;
            string Division = txtDivision.Text;
            string Range = txtRange.Text;
            string SealNumber = txtSealNumber.Text;
            string VerifiedbyExaminingOfficer = string.Empty;
            string SampleForwarded = string.Empty;            
            if(chkExaminer.Checked)
            {
                VerifiedbyExaminingOfficer = "1";
            }
            else 
            {
                VerifiedbyExaminingOfficer = "0";
            }

            if(chkSample.Checked)
            {
                SampleForwarded = "1";                
            }
            else 
            {
                SampleForwarded = "0";                
            }

            if (ddlJobNo.SelectedValue != "0" && txtinvno.Text != "")
            {
                try
                {
                    int result = objETInvoiceBL.UpdateAnnexure(ddlJobNo.SelectedValue, txtinvno.Text, educode, BranchSNo, ExaminationDate, ExaminingOfficier, ExamineDesignation,
                        SupervisingOfficier, SupervisingDesgn, Commissionerate, Division, Range, VerifiedbyExaminingOfficer, SampleForwarded, SealNumber);
                    if (result == 1)
                    {                        
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Saved Successfully'); ", true);
                    }
                }
                catch (Exception ex)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('" + ex.Message + "'); ", true);
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Please select the job no and invoice'); ", true);
            }
        }

        private void clearAnnexure()
        {
            txtEDUCode.Text = txtBranchSlNo.Text = txtExaminationDate.Text = txtExaminingOfficier.Text = txtExamineDesignation.Text = txtSupervisingOfficier.Text = txtSupervisingDesgn.Text = txtCommissionerate.Text = txtDivision.Text = txtRange.Text = txtSealNumber.Text = string.Empty;
            chkExaminer.Checked = false;
            chkSample.Checked = false;
        }

        protected void btnAnnexureCancel_Click(object sender, EventArgs e)
        {
            clearAnnexure();
        }

    }
}