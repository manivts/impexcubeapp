using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using VTS.ImpexCube.Data;
using VTS.ImpexCube.Business;

namespace ImpexCube
{
	public partial class efrmProductExport : System.Web.UI.Page
	{
		CommonDL objCommonDL = new CommonDL();
		ETProductBL objETProductBL = new ETProductBL();
		ETProductDocumentReleasingBL objETProductDocumentReleasingBL = new ETProductDocumentReleasingBL();
		ETProductAR4DetailsBL objETProductAR4DetailsBL = new ETProductAR4DetailsBL();
		ETProductQuotaBL objETProductQuotaBL = new ETProductQuotaBL();
		VTS.ImpexCube.Business.ProductDetailsBL obj = new VTS.ImpexCube.Business.ProductDetailsBL();

		#region GlobalDeclaration
		int result;
		string ProductID = string.Empty;

		string JobNo = string.Empty;
		string InvoiceNo = string.Empty;
		string code = string.Empty;
		string family = string.Empty;
		string Description = string.Empty;
		string RITCCode = string.Empty;
		double Quantity = 0.00;
		string QuantityUnit = string.Empty;
		double UnitPrice = 0.00;
		string UnitPriceCurrency = string.Empty;
		string Per = string.Empty;
		string PerUnit = string.Empty;
		double Amount =   0.00;
		string AmountCurrency = string.Empty;
		string EximCode = string.Empty;
		string NFEICategory = string.Empty;
		double AlternateQty = 0.00;
		string AlternateQtyUnit = string.Empty;
		string PMVCurrency = string.Empty;
		string PMVCalcMethod = string.Empty;
		double PMVCalcMethodRate =   0.00;
		double PMVUnitRate  = 0.00;
		string PMVUnit = string.Empty;
		double TotalPMV  = 0.00;
		string TotalPMVUnit = string.Empty;
		bool RewardItem;
		string STRCode = string.Empty;
		string CessDuty = string.Empty;
		string ExportDutyNotn = string.Empty;
		double ExportDutyRate  = 0.00;
		double ExportDutyAmount  = 0.00;
		string ExportDutyUnit = string.Empty;
		double ExportDutyQty = 0.00;
		string CessNotn = string.Empty;
		double CessRate  = 0.00;
		double CessAmount  = 0.00;
		string CessUnit = string.Empty;
		double CessTariffValue  = 0.00;
		string CessTariffValueUnit = string.Empty;
		double CessQty  = 0.00;
		string CessDesc = string.Empty;
		string OthDutyCessNotn = string.Empty;
		double OthDutyCessRate = 0.00;
		double OthDutyCessAmount  = 0.00;
		string OthDutyCessUnit = string.Empty;
		double OthDutyCessQty  = 0.00;
		string OthDutyCessDesc = string.Empty;
		string ThirdCessNotn = string.Empty;
		double ThirdCessRate  = 0.00;
		double ThirdCessAmount  = 0.00;
		string ThirdCessUnit = string.Empty;
		double ThirdCessQty = 0.00;
		string ThirdCessDesc = string.Empty;
		string CENVATCertiNo = string.Empty;
		string CENVATDate = string.Empty;
		string CENVATValidUpto = string.Empty;
		string CENVATCExOffCode = string.Empty;
		string CENVATAssCode = string.Empty;
		bool ReExportItem;
		string BENo = string.Empty;
		string BEDate = string.Empty;
		double QuantityExported  = 0.00;
		string QuantityExportedUnit = string.Empty;
		string InvoiceSNo = string.Empty;
		string ItemSNo = string.Empty;
		string TechnicalDetails = string.Empty;
		string ImportPortCode = string.Empty;
		string BEItemDesc = string.Empty;
		string OtherIdenPara = string.Empty;
		bool AgainstExpOblig;
		string ObligNo = string.Empty;
		double DrawbackAmtclaimed  = 0.00;
		double QuantityImported  = 0.00;
		string QuantityImportedUnit = string.Empty;
		bool ItemUnUsed;
		bool CommissionerPermi;
		double AssessableValue  = 0.00;
		string BoardNo = string.Empty;
		string BoardDate = string.Empty;
		double TotalDutyPaid  = 0.00;
		string TotalDutyPaidDate = string.Empty;
		bool MODVATAvailed;
		bool MODVATReversed;
		string Accessories = string.Empty;
		bool ThirdPartyEXP;
		string Manufacturer = string.Empty;
		string IECode = string.Empty;
		string BranchSNo = string.Empty;
		string ThirdPartyEXPAddress = string.Empty;
		string ThirdPartyEXPAddress1 = string.Empty;
		string CreatedBy = string.Empty;
		string CreatedDate = string.Empty;
		string ModifiedBy = string.Empty;
		string ModifiedDate = string.Empty;

		//DocumentReleasingTable

		string ProdDescription = string.Empty;
		string DocType = string.Empty;
		string GeneralDescription = string.Empty;
		string AgencyCode = string.Empty;
		string AgencyName = string.Empty;
		string DocumentName = string.Empty;

		//AR4

		string AR4ProdDescription = string.Empty;
		string AR4No = string.Empty;
		string AR4Date = string.Empty;
		string Commissionerate = string.Empty;
		string Division = string.Empty;
		string Range = string.Empty;
		string Remark = string.Empty;

		//ProductQouta

		string QuotaCertificateNo = string.Empty;
		string Agency = string.Empty;
		string ExpiryDate = string.Empty;
		string QuotaQuantity = string.Empty;
		string Unit = string.Empty;

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
					Session["ExInvoice"] = string.Empty;
				}
				filldropdown();				
				if (ExJobNo == "" || ExJobNo == null || ExJobNo == "~Select~")
				{
					//btnUpdate.Visible = false;
				   // btnUpdategeneral.Visible = false;
				   // btnupdateq.Visible = false;
				   // btnUpdAr4.Visible = false;
				   // btnAddd.Visible = true;
				  //  btnaddGenral.Visible = true;
				   // btnaddq.Visible = true;
					Session["ID"] = string.Empty;
				}
				else
				{                    
				   // btnUpdate.Visible = true;
				   // btnUpdategeneral.Visible = true;
				   // btnupdateq.Visible = true;
				   // btnUpdAr4.Visible = true;
				  //  btnAddd.Visible = false;
					//btnaddGenral.Visible = false;
					//btnaddq.Visible = false;
					Session["ID"] = string.Empty;
					GetJobDetails();
				}
			}
		}

		private void GetJobDetails()
		{
			ddlJobNo.SelectedValue = ExJobNo;
			string InvNo = (string)Session["ExInvoice"];
			FillInvoiceNo();
			ddlInvNo.SelectedValue = InvNo;
			DataSet ds = new DataSet();
			string quer = "select  *  from [E_T_Invoice] where InvoiceNo='" + ddlInvNo.SelectedValue + "' and JobNo='" + ddlJobNo.SelectedValue + "'";
			ds = objCommonDL.GetDataSet(quer);
			if (ds.Tables["Table"].Rows.Count != 0)
			{
				DataRowView dr = ds.Tables["Table"].DefaultView[0];
				lblInvDate.Text = dr["InvoiceDate"].ToString();
				lblInvAmt.Text = dr["InvoiceValue"].ToString();
				lblCurrency.Text = dr["Currency"].ToString();
				lblExRate.Text = dr["CurrencyRate"].ToString();
				lblFrie.Text = dr["FreightRate"].ToString();
				lblIns.Text = dr["InsuranceRate"].ToString();
				productgridload();
				GetJobNo();
			}
			else
			{                
				FillInvoiceNo();
				GetJobNo();
			}
		}

		private void FillInvoiceNo()
		{
			DataSet ds = new DataSet();
			string quer = "select  ID,JobNo, InvoiceNo  from [E_T_Invoice] where jobno='" + ddlJobNo.SelectedValue + "'";
			ds = objCommonDL.GetDataSet(quer);
			if (ds.Tables["Table"].Rows.Count != 0)
			{
				ddlInvNo.DataSource = ds;
				ddlInvNo.DataTextField = "InvoiceNo";
				ddlInvNo.DataValueField = "InvoiceNo";
				ddlInvNo.DataBind();
				ddlInvNo.Items.Insert(0, new ListItem("~Select~", "0"));
			}
			else
			{
				btnUpdate.Visible = false;
				btnUpdategeneral.Visible = false;
				btnupdateq.Visible = false;
				btnUpdAr4.Visible = false;
				btnAddd.Visible = true;
				btnaddGenral.Visible = true;
				btnaddq.Visible = true;
			}
		}

		private void GetJobNo()
		{            
			DataSet ds = new DataSet();
			string quer = "select * from E_M_JobCreation where JobNo = '" + ddlJobNo.SelectedValue + "' ";
			ds = objCommonDL.GetDataSet(quer);
			if (ds.Tables["Table"].Rows.Count != 0)
			{
				DataRowView row = ds.Tables["Table"].DefaultView[0];
				lblJobDate.Text = row["JobDate"].ToString();
				lblMode.Text = row["TransportMode"].ToString();
				lblCustom.Text = row["CustomHouse"].ToString();
			}
		}

		public void filldropdown()
		{
			DataSet ds = new DataSet();
			string quer = "select * from E_M_JobCreation ";
			ds = objCommonDL.GetDataSet(quer);
			ddlJobNo.DataSource = ds;
			ddlJobNo.DataTextField = "JobNo";
			ddlJobNo.DataValueField = "JobNo";
			ddlJobNo.DataBind();

			//Currency
			DataSet dsCurrrency = objCommonDL.GetCurrencyDetails();
			ddlCurrency.DataSource = dsCurrrency;
			ddlCurrency.DataTextField = "CurrencyShortName";
			ddlCurrency.DataValueField = "CurrencyShortName";
			ddlCurrency.DataBind();

			ddluirpriz1.DataSource = dsCurrrency;
			ddluirpriz1.DataTextField = "CurrencyShortName";
			ddluirpriz1.DataValueField = "CurrencyShortName";
			ddluirpriz1.DataBind();

			ddlAmount1.DataSource = dsCurrrency;
			ddlAmount1.DataTextField = "CurrencyShortName";
			ddlAmount1.DataValueField = "CurrencyShortName";
			ddlAmount1.DataBind();

			ddlpmvunit1.DataSource = dsCurrrency;
			ddlpmvunit1.DataTextField = "CurrencyShortName";
			ddlpmvunit1.DataValueField = "CurrencyShortName";
			ddlpmvunit1.DataBind();

			ddltotalpmv1.DataSource = dsCurrrency;
			ddltotalpmv1.DataTextField = "CurrencyShortName";
			ddltotalpmv1.DataValueField = "CurrencyShortName";
			ddltotalpmv1.DataBind();

			DataSet dsUnit = objCommonDL.GetUnit();
			ddlquan1.DataSource = dsUnit;
			ddlquan1.DataTextField = "UnitShort";
			ddlquan1.DataValueField = "UnitShort";
			ddlquan1.DataBind();

			ddlper1.DataSource = dsUnit;
			ddlper1.DataTextField = "UnitShort";
			ddlper1.DataValueField = "UnitShort";
			ddlper1.DataBind();

			ddlquantityimport4.DataSource = dsUnit;
			ddlquantityimport4.DataTextField = "UnitShort";
			ddlquantityimport4.DataValueField = "UnitShort";
			ddlquantityimport4.DataBind();

			ddlquanexp2.DataSource = dsUnit;
			ddlquanexp2.DataTextField = "UnitShort";
			ddlquanexp2.DataValueField = "UnitShort";
			ddlquanexp2.DataBind();

			ddlAlternateQtyUnit.DataSource = dsUnit;
			ddlAlternateQtyUnit.DataTextField = "UnitShort";
			ddlAlternateQtyUnit.DataValueField = "UnitShort";
			ddlAlternateQtyUnit.DataBind();

			ddlexpdutyrate0.DataSource = dsUnit;
			ddlexpdutyrate0.DataTextField = "UnitShort";
			ddlexpdutyrate0.DataValueField = "UnitShort";
			ddlexpdutyrate0.DataBind();

			ddlcessrs.DataSource = dsUnit;
			ddlcessrs.DataTextField = "UnitShort";
			ddlcessrs.DataValueField = "UnitShort";
			ddlcessrs.DataBind();

			ddlothdutyrs.DataSource = dsUnit;
			ddlothdutyrs.DataTextField = "UnitShort";
			ddlothdutyrs.DataValueField = "UnitShort";
			ddlothdutyrs.DataBind();

			ddlthirdcessrs.DataSource = dsUnit;
			ddlthirdcessrs.DataTextField = "UnitShort";
			ddlthirdcessrs.DataValueField = "UnitShort";
			ddlthirdcessrs.DataBind();

			ddlcessvalue.DataSource = dsUnit;
			ddlcessvalue.DataTextField = "UnitShort";
			ddlcessvalue.DataValueField = "UnitShort";
			ddlcessvalue.DataBind();

			ddlunitq.DataSource = dsUnit;
			ddlunitq.DataTextField = "UnitShort";
			ddlunitq.DataValueField = "UnitShort";
			ddlunitq.DataBind();

			ddlDEPBUnit.DataSource = dsUnit;
			ddlDEPBUnit.DataTextField = "UnitShort";
			ddlDEPBUnit.DataValueField = "UnitShort";
			ddlDEPBUnit.DataBind();

			ddlDEPBCRUnit.DataSource = dsUnit;
			ddlDEPBCRUnit.DataTextField = "UnitShort";
			ddlDEPBCRUnit.DataValueField = "UnitShort";
			ddlDEPBCRUnit.DataBind();

            ddlDBKMUnit.DataSource = dsUnit;
            ddlDBKMUnit.DataTextField = "UnitShort";
            ddlDBKMUnit.DataValueField = "UnitShort";
            ddlDBKMUnit.DataBind();

            EXIM();
		}

		protected void btnproduct_Click(object sender, EventArgs e)
		{
			View1.Visible = true;
			View2.Visible = false;
			View3.Visible = false;
			View4.Visible = false;
			View5.Visible = false;
			View6.Visible = false;
		}

		protected void btnCessexpduty_Click(object sender, EventArgs e)
		{
			View1.Visible = false;
			View2.Visible = true;
			View3.Visible = false;
			View4.Visible = false;
			View5.Visible = false;
			View6.Visible = false;
		}

		protected void btnquota_Click(object sender, EventArgs e)
		{
			View1.Visible = false;
			View2.Visible = false;
			View3.Visible = true;
			View4.Visible = false;
			View5.Visible = false;
			View6.Visible = false;
		}

		protected void btnAr4det_Click(object sender, EventArgs e)
		{
			View1.Visible = false;
			View2.Visible = false;
			View3.Visible = false;
			View4.Visible = true;
			View5.Visible = false;
			View6.Visible = false;
		}

		protected void btnreexp_Click(object sender, EventArgs e)
		{
			View1.Visible = false;
			View2.Visible = false;
			View3.Visible = false;
			View5.Visible = true;
			View4.Visible = false;
			View6.Visible = false;
		}

		protected void btnotherdet_Click(object sender, EventArgs e)
		{
			View1.Visible = false;
			View2.Visible = false;
			View3.Visible = false;
			View4.Visible = false;
			View5.Visible = false;
			View6.Visible = true;
		}

		protected void ddlJobNo_SelectedIndexChanged(object sender, EventArgs e)
		{
			FillInvoiceNo();
			GetJobNo();
		}

		protected void ddlInvNo_SelectedIndexChanged(object sender, EventArgs e)
		{
			invoicefill();
			productgridload();
            ClearDEPB();
            ClearDrawback();
            ClearEDI();
            ClearEDIItems();
            ClearMainDEPB();
            ClearMainDrawback();
		}

		private void productgridload()
		{
			DataSet ds = new DataSet();
			string quer = "select  *  from [E_T_Product] where InvoiceNo='" + ddlInvNo.SelectedValue + "' and JobNo='"+ddlJobNo.SelectedValue +"'";
			ds = objCommonDL.GetDataSet(quer);
			if (ds.Tables["Table"].Rows.Count != 0)
			{
				gvProductExp.DataSource = ds;
				gvProductExp.DataBind();
			}
			else
			{
				gvProductExp.DataSource = null;
				gvProductExp.DataBind();
                gvDEPB.DataBind();
                gvDrawback.DataBind();
                gvEBCG.DataBind();
			}
		}

		private void invoicefill()
		{
			DataSet ds = new DataSet();
			string quer = "select  *  from [E_T_Invoice] where InvoiceNo='" + ddlInvNo.SelectedValue + "' and JobNo='" + ddlJobNo.SelectedValue + "'";
			ds = objCommonDL.GetDataSet(quer);
			if (ds.Tables["Table"].Rows.Count != 0)
			{
				DataRowView dr = ds.Tables["Table"].DefaultView[0];
				lblInvDate.Text = dr["InvoiceDate"].ToString();
				lblInvAmt.Text = dr["InvoiceValue"].ToString();
				lblCurrency.Text = dr["Currency"].ToString();
				lblExRate.Text = dr["CurrencyRate"].ToString();
				lblFrie.Text = dr["FreightRate"].ToString();
				lblIns.Text = dr["InsuranceRate"].ToString();
			}
			else
			{
                gvDEPB.DataBind();
                gvDrawback.DataBind();
                gvEBCG.DataBind();
			}
		}

		protected void btnAddd_Click(object sender, EventArgs e)
		{
			if (txtNoProducts.Text != "")
			{                
				DataSet ds = new DataSet();
				string query = "select  *  from [E_T_Product] where InvoiceNo='" + ddlInvNo.SelectedValue + "' and JobNo='" + ddlJobNo.SelectedValue + "'";
				ds = objCommonDL.GetDataSet(query);
				int NoofProducts = Convert.ToInt32(txtNoProducts.Text);
				int count = ds.Tables["Table"].Rows.Count;
				if (count < NoofProducts)
				{
					JobNo = ddlJobNo.SelectedValue;
					InvoiceNo = ddlInvNo.SelectedValue;
					code = txtProductCode.Text;
					family = txtProductFamily.Text;
					Description = txtDesc.Text;
					RITCCode = txtRitccode.Text;
					Quantity = Convert.ToDouble(txtQuan.Text);
					QuantityUnit = ddlquan1.SelectedValue;
					UnitPrice = Convert.ToDouble(txtunitpric.Text);
					UnitPriceCurrency = ddluirpriz1.SelectedValue;
					Per = txtper.Text;
					PerUnit = ddlper1.SelectedValue;
					Amount = Convert.ToDouble(txtamount.Text);
					AmountCurrency = ddlAmount1.SelectedValue;
					CreatedBy = (string)Session["USER-NAME"];
					CreatedDate = DateTime.Now.ToString();

					result = objETProductBL.mainsave(JobNo, InvoiceNo, code, family, Description, RITCCode, Quantity, QuantityUnit, UnitPrice, UnitPriceCurrency, Per, PerUnit, Amount, AmountCurrency, CreatedBy, CreatedDate);
					productgridload();

					Clear();

					if (result == 1)
					{
						ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Saved Successfully')", true);
					}
				}
				else
				{
					ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('No of Product exist')", true);
				}
			}
			else
			{
				ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Please enter no of products for the invoice')", true);
			}
		}

		private void Clear()
		{
			txtProductCode.Text = "";
			txtProductFamily.Text = "";
			txtDesc.Text = "";
			txtRitccode.Text = "";
			txtQuan.Text = "";
			ddlquan1.SelectedIndex = 0;
			txtunitpric.Text = "";
			ddluirpriz1.SelectedIndex = 0;
			txtper.Text = "";
			ddlper1.SelectedIndex = 0;
			txtamount.Text = "";
			ddlAmount1.SelectedIndex = 0;
		}

		protected void btnUpdate_Click(object sender, EventArgs e)
		{
			ProductID = (string)Session["ProductID"];
			JobNo = ddlJobNo.SelectedValue;
			InvoiceNo = ddlInvNo.SelectedValue;
			Description = txtDesc.Text;
			code = txtProductCode.Text;
			family = txtProductFamily.Text;
			RITCCode = txtRitccode.Text;
			Quantity = Convert.ToDouble(txtQuan.Text);
			QuantityUnit = ddlquan1.SelectedValue;
			UnitPrice = Convert.ToDouble(txtunitpric.Text);
			UnitPriceCurrency = ddluirpriz1.SelectedValue;
			Per = txtper.Text;
			PerUnit = ddlper1.SelectedValue;
			Amount = Convert.ToDouble(txtamount.Text);
			AmountCurrency = ddlAmount1.SelectedValue;
			ModifiedBy = (string)Session["USER-NAME"];
			ModifiedDate = DateTime.Now.ToString();
			result = objETProductBL.mainupdate(ProductID, JobNo, InvoiceNo, code, family, Description, RITCCode, Quantity, QuantityUnit, UnitPrice, UnitPriceCurrency, Per, PerUnit, Amount, AmountCurrency, ModifiedBy, ModifiedDate);
			Clear();
			productgridload();

			if (result == 1)
			{
				ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Updated Successfully')", true);
			}
			btnUpdate.Visible = false;
			btnAddd.Visible = true;
		}

		protected void gvProductExp_SelectedIndexChanged(object sender, EventArgs e)
		{
			btnAddd.Visible = false;
			btnUpdate.Visible = true;
			Session["ProductID"] = gvProductExp.SelectedRow.Cells[1].Text;
			JobNo = ddlJobNo.SelectedValue;
			InvoiceNo = gvProductExp.SelectedRow.Cells[2].Text;
			DataSet ds = new DataSet();
			string quer = "select  *  from [E_T_Product] where ID='" + (string)Session["ProductID"] + "' ";
			ds = objCommonDL.GetDataSet(quer);
            if (ds.Tables["Table"].Rows.Count != 0)
            {
                DataRowView dr = ds.Tables["Table"].DefaultView[0];
                JobNo = dr["JobNo"].ToString();
                InvoiceNo = dr["InvoiceNo"].ToString();
                txtProductCode.Text = dr["ProductCode"].ToString();
                txtProductFamily.Text = dr["ProductFamily"].ToString();
                txtDesc.Text = dr["Description"].ToString();
                txtRitccode.Text = dr["RITCCode"].ToString();
                txtQuan.Text = dr["Quantity"].ToString();
                ddlquan1.SelectedItem.Text = dr["QuantityUnit"].ToString();
                txtunitpric.Text = dr["UnitPrice"].ToString();
                ddluirpriz1.SelectedItem.Text = dr["UnitPriceCurrency"].ToString();
                txtper.Text = dr["Per"].ToString();
                ddlper1.SelectedItem.Text = dr["PerUnit"].ToString();
                txtamount.Text = dr["Amount"].ToString();
                ddlAmount1.SelectedItem.Text = dr["AmountCurrency"].ToString();

                if (dr["EximCode"] != DBNull.Value)
                {
                    ddlExim.SelectedItem.Text = dr["EximCode"].ToString();
                }
                if (dr["NFEICategory"] != DBNull.Value)
                {
                    ddlnfeicode.SelectedItem.Text = dr["NFEICategory"].ToString();
                }
                txtAlternateQty.Text = dr["AlternateQty"].ToString();
                if (dr["AlternateQtyUnit"] != DBNull.Value)
                {
                    ddlAlternateQtyUnit.SelectedItem.Text = dr["AlternateQtyUnit"].ToString();
                }
                if (dr["PMVCurrency"] != DBNull.Value)
                {
                    ddlCurrency.SelectedItem.Text = dr["PMVCurrency"].ToString();
                }
                if (dr["PMVCalcMethod"] != DBNull.Value)
                {
                    ddlcalcmethd.SelectedItem.Text = dr["PMVCalcMethod"].ToString();
                }
                txtcalcmethd.Text = dr["PMVCalcMethodRate"].ToString();
                txtpmvunit.Text = dr["PMVUnitRate"].ToString();
                if (dr["PMVUnit"] != DBNull.Value && (dr["PMVUnit"].ToString() != string.Empty))
                {
                    ddlpmvunit1.SelectedItem.Text = dr["PMVUnit"].ToString();
                }

                txttotalpmv.Text = dr["TotalPMV"].ToString();


                if (dr["TotalPMVUnit"] != DBNull.Value && (dr["TotalPMVUnit"].ToString() != string.Empty))
                {
                    ddltotalpmv1.SelectedItem.Text = dr["TotalPMVUnit"].ToString();
                }




                if (dr["RewardItem"] != DBNull.Value)
                {
                    chkReward.Checked = Convert.ToBoolean(dr["RewardItem"]);
                }

                txtstrcode.Text = dr["STRCode"].ToString();

                if (dr["ExportDutyNotn"] != DBNull.Value)
                {
                    ddlExpcessduty.SelectedItem.Text = dr["ExportDutyNotn"].ToString();
                }
                txtexpcessdutyrate.Text = dr["ExportDutyRate"].ToString();
                txtexpdutyper0.Text = dr["ExportDutyAmount"].ToString();

                if (dr["ExportDutyUnit"] != DBNull.Value)
                {
                    ddlexpdutyrate0.SelectedItem.Text = dr["ExportDutyUnit"].ToString();
                }
                txtexpqtydutycessuty.Text = dr["ExportDutyQty"].ToString();

                if (dr["CessNotn"] != DBNull.Value)
                {
                    ddlcesscessduty.SelectedItem.Text = dr["CessNotn"].ToString();
                }
                txtcessdutyrte.Text = dr["CessRate"].ToString();
                txtcessper.Text = dr["CessAmount"].ToString();
                if (dr["CessTariffValueUnit"] != DBNull.Value)
                {
                    ddlcessrs.SelectedItem.Text = dr["CessTariffValueUnit"].ToString();
                }
                txtcesstariff.Text = dr["CessTariffValue"].ToString();
                if (dr["CessTariffValueUnit"] != DBNull.Value)
                {
                    ddlcessvalue.SelectedItem.Text = dr["CessTariffValueUnit"].ToString();
                }
                txtcessqtycessduty.Text = dr["CessQty"].ToString();
                txtcesscessdesc.Text = dr["CessDesc"].ToString();

                if (dr["OthDutyCessNotn"] != DBNull.Value)
                {
                    ddlothcessduty.SelectedItem.Text = dr["OthDutyCessNotn"].ToString();
                }
                txtothcessdutyrate.Text = dr["OthDutyCessRate"].ToString();
                txtothdutyper.Text = dr["OthDutyCessAmount"].ToString();
                if (dr["OthDutyCessUnit"] != DBNull.Value)
                {
                    ddlothdutyrs.SelectedItem.Text = dr["OthDutyCessUnit"].ToString();
                }
                txtothqtyforcess1.Text = dr["OthDutyCessQty"].ToString();
                txtothcessdesc1.Text = dr["OthDutyCessDesc"].ToString();
                if (dr["ThirdCessNotn"] != DBNull.Value)
                {
                    ddlthirdcess.SelectedItem.Text = dr["ThirdCessNotn"].ToString();
                }
                oththirdcessdutyrate.Text = dr["ThirdCessRate"].ToString();
                txtthirdcessper.Text = dr["ThirdCessAmount"].ToString();
                if (dr["ThirdCessUnit"] != DBNull.Value)
                {
                    ddlthirdcessrs.SelectedItem.Text = dr["ThirdCessUnit"].ToString();
                }
                txtthirdqtyforcess1.Text = dr["ThirdCessQty"].ToString();
                txtthirdcessdesc0.Text = dr["ThirdCessDesc"].ToString();
                txtcertnum.Text = dr["CENVATCertiNo"].ToString();
                txtcenvatdate.Text = dr["CENVATDate"].ToString();
                txtvalidupto.Text = dr["CENVATValidUpto"].ToString();
                txtcexofccode.Text = dr["CENVATCExOffCode"].ToString();
                txtAssessee.Text = dr["CENVATAssCode"].ToString();

                txtbenumber1.Text = dr["BENo"].ToString();
                txtbenumdate.Text = dr["BEDate"].ToString();
                txtquanexp3.Text = dr["QuantityExported"].ToString();
                if (dr["QuantityExportedUnit"] != DBNull.Value)
                {
                    ddlquanexp2.SelectedItem.Text = dr["QuantityExportedUnit"].ToString();
                }
                txtinvoicesno.Text = dr["InvoiceSNo"].ToString();
                txttemsno.Text = dr["ItemSNo"].ToString();
                txttechnicaldet.Text = dr["TechnicalDetails"].ToString();
                txtimportportcode.Text = dr["ImportPortCode"].ToString();
                txtbeitemdesc.Text = dr["BEItemDesc"].ToString();
                txtothIdenParam1.Text = dr["OtherIdenPara"].ToString();
                if (dr["AgainstExpOblig"] != DBNull.Value)
                {

                    chkagaistoblig.Checked = Convert.ToBoolean(dr["AgainstExpOblig"]);
                }
                txtOblinum0.Text = dr["ObligNo"].ToString();
                txtdraAmtClaim.Text = dr["DrawbackAmtclaimed"].ToString();
                txtquantityimport3.Text = dr["QuantityImported"].ToString();
                if (dr["QuantityImportedUnit"] != DBNull.Value)
                {
                    ddlquantityimport4.SelectedItem.Text = dr["QuantityImportedUnit"].ToString();
                }
                if (dr["ItemUnUsed"] != DBNull.Value)
                {
                    chkitemunused.Checked = Convert.ToBoolean(dr["ItemUnUsed"]);
                }
                if (dr["CommissionerPermi"] != DBNull.Value)
                {
                    chkcommpermisn.Checked = Convert.ToBoolean(dr["CommissionerPermi"]);
                }
                txtassessibleval1.Text = dr["AssessableValue"].ToString();
                txtboardnum1.Text = dr["BoardNo"].ToString();
                txtboarddate1.Text = dr["BoardDate"].ToString();
                txttotaldutypaid.Text = dr["TotalDutyPaid"].ToString();
                txttotdutaiddate.Text = dr["TotalDutyPaidDate"].ToString();
                if (dr["MODVATAvailed"] != DBNull.Value)
                {
                    chkmodvatavail.Checked = Convert.ToBoolean(dr["MODVATAvailed"]);
                }
                if (dr["MODVATReversed"] != DBNull.Value)
                {
                    chkmodvatreserved.Checked = Convert.ToBoolean(dr["MODVATReversed"]);
                }
                txtotherRemarks.Text = dr["Accessories"].ToString();
                if (dr["ThirdPartyEXP"] != DBNull.Value)
                {
                    chkthirdparty.Checked = Convert.ToBoolean(dr["ThirdPartyEXP"]);
                }
                txtManufacture.Text = dr["Manufacturer"].ToString();
                txtotheriecode.Text = dr["IECode"].ToString();
                txtothBranchsno.Text = dr["BranchSNo"].ToString();
                //if (dr["ThirdPartyEXPAddress"] != DBNull.Value)
                //{
                //    ddlotherAddress.SelectedValue = dr["ThirdPartyEXPAddress"].ToString();
                //}
                txtotheraddress.Text = dr["ThirdPartyEXPAddress1"].ToString();

                quotagridload();
                documentgridload();
                ar4gridload();
                DEPBSelect();
                DEPBCreditSelect();
                DrawbackSelect();
                DrawbackMaterialSelect();
                EDISelect();
                EPCGitemsSelect();
            }
            else
            {
                ClearDEPB();
                ClearDrawback();
                ClearEDI();
                ClearEDIItems();
                ClearMainDEPB();
                ClearMainDrawback();
            }
		}

		protected void btngeneralsave_Click(object sender, EventArgs e)
		{
			try
			{
				ProductID = (string)Session["ProductID"];
				JobNo = ddlJobNo.SelectedValue;
				InvoiceNo = ddlInvNo.SelectedValue;
				EximCode = ddlExim.SelectedValue;
				NFEICategory = ddlnfeicode.SelectedValue;
				AlternateQty = Convert.ToDouble(txtAlternateQty.Text);
				AlternateQtyUnit = ddlAlternateQtyUnit.SelectedValue;
				PMVCurrency = ddlCurrency.SelectedValue;
				PMVCalcMethod = ddlcalcmethd.SelectedValue;
				PMVCalcMethodRate = Convert.ToDouble(txtcalcmethd.Text);
				PMVUnitRate = Convert.ToDouble(txtpmvunit.Text);
				PMVUnit = ddlpmvunit1.SelectedValue;
				TotalPMV = Convert.ToDouble(txttotalpmv.Text);
				TotalPMVUnit = ddltotalpmv1.SelectedValue;
				RewardItem = chkReward.Checked;
				STRCode = txtstrcode.Text;
				ModifiedBy = (string)Session["USER-NAME"];
				ModifiedDate = DateTime.Now.ToString();

				result = objETProductBL.UpdateGeneral(ProductID, JobNo, InvoiceNo, EximCode, NFEICategory, AlternateQty, AlternateQtyUnit, PMVCurrency, PMVCalcMethod,
									PMVCalcMethodRate, PMVUnit, PMVUnitRate, TotalPMV, TotalPMVUnit, RewardItem, STRCode, ModifiedBy, ModifiedDate);
				if (result == 1)
				{
					ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Saved Successfully')", true);
					GeneralClear();
				}
			}
			catch (Exception ex)
			{
			}
		}

		private void DEPBSelect()
		{
            try
            {
                JobNo = ddlJobNo.SelectedValue;
                InvoiceNo = ddlInvNo.SelectedValue;
                DataSet ds = new DataSet();
                string quer = "select  *  from [E_T_Product_DEPB]  Where JobNo ='" + JobNo + "' And InvoiceNo='" + InvoiceNo + "' and ProdDescription='" + txtDesc.Text + "'";
                ds = objCommonDL.GetDataSet(quer);
                if (ds.Tables["Table"].Rows.Count != 0)
                {
                    DataRowView dr = ds.Tables["Table"].DefaultView[0];
                    chkDEPB.Checked = Convert.ToBoolean(dr["DEPBItem"]);
                    txtProductGroup.Text = dr["ProductGroup"].ToString();
                    txtRateListSrNo.Text = dr["RateListNo"].ToString();
                    txtDEPBRate.Text = dr["DEPBRate"].ToString();
                    txtDEPBQty.Text = dr["DEPBQty"].ToString();
                    ddlDEPBUnit.SelectedItem.Text = dr["DEPBUnit"].ToString();
                    txtCapValue.Text = dr["CAPValue"].ToString();
                    txtStdIONorms.Text = dr["StdIONorms"].ToString();
                    chkDEPBCredit.Checked = Convert.ToBoolean(dr["DEPBCredit"]);
                    btnDEPBSave.Visible = false;
                    btnDEPBUpdate.Visible = true;
                }
                else
                {
                    btnDEPBSave.Visible=true;
                    btnDEPBUpdate.Visible = false;
                }
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }
		}

		private void GeneralClear()
		{
			ddlExim.SelectedIndex = 0;
			ddlnfeicode.SelectedIndex = 0;
			txtAlternateQty.Text = "";
			ddlAlternateQtyUnit.SelectedIndex = 0;
			ddlCurrency.SelectedIndex = 0;
			ddlcalcmethd.SelectedIndex = 0;
			txtcalcmethd.Text = "";
			txtpmvunit.Text = "";
			ddlpmvunit1.SelectedIndex = 0;
			txttotalpmv.Text = "";
			ddltotalpmv1.SelectedIndex = 0;
			chkReward.Checked = false;
			txtstrcode.Text = "";
		}

		protected void btnaddGenral_Click(object sender, EventArgs e)
		{
			JobNo = ddlJobNo.SelectedValue;
			InvoiceNo = ddlInvNo.SelectedValue;
			DocType = ddldoctype.SelectedValue;
			GeneralDescription = txtdescr.Text;
			AgencyCode = txtagencyco.Text;
			AgencyName = txtagencyname.Text;
			DocumentName = txtDocname.Text;
			CreatedBy = (string)Session["USER-NAME"];
			CreatedDate = DateTime.Now.ToString();

			objETProductDocumentReleasingBL.save(JobNo, InvoiceNo, DocType, GeneralDescription, AgencyCode, AgencyName, DocumentName, CreatedBy, CreatedDate);

			documentgridload();

			DocumentClear();
		}

		private void DocumentClear()
		{
			ddldoctype.SelectedIndex = 0;
			txtdescr.Text = "";
			txtagencyco.Text = "";
			txtagencyname.Text = "";
			txtDocname.Text = "";
		}

		private void documentgridload()
		{
			JobNo = ddlJobNo.SelectedValue;
			DataSet ds = objETProductDocumentReleasingBL.GetProductDocumentReleasingData(JobNo, InvoiceNo);
			if (ds.Tables["Table"].Rows.Count != 0)
			{
				gvprodgen.DataSource = ds;
				gvprodgen.DataBind();
			}
			else
			{
				gvprodgen.DataSource = null;
				gvprodgen.DataBind();
			}
		}

		protected void btncesssav_Click(object sender, EventArgs e)
		{
			ProductID = (string)Session["ProductID"];
			JobNo = ddlJobNo.SelectedValue;
			InvoiceNo = ddlInvNo.SelectedValue;
			ExportDutyNotn = ddlExpcessduty.SelectedValue;
			ExportDutyRate = Convert.ToDouble(txtexpcessdutyrate.Text);
			ExportDutyAmount = Convert.ToDouble(txtexpdutyper0.Text);
			ExportDutyUnit = ddlexpdutyrate0.SelectedValue;
			ExportDutyQty = Convert.ToDouble(txtexpqtydutycessuty.Text);
			CessNotn = ddlcesscessduty.SelectedValue;
			CessRate = Convert.ToDouble(txtcessdutyrte.Text);
			CessAmount = Convert.ToDouble(txtcessper.Text);
			CessUnit = ddlcessrs.SelectedValue;
			CessTariffValue = Convert.ToDouble(txtcesstariff.Text);
			CessTariffValueUnit = ddlcessvalue.SelectedValue;
			CessQty = Convert.ToDouble(txtcessqtycessduty.Text);
			CessDesc = txtcesscessdesc.Text;
			OthDutyCessNotn = ddlothcessduty.SelectedValue;
			OthDutyCessRate = Convert.ToDouble(txtothcessdutyrate.Text);
			OthDutyCessAmount = Convert.ToDouble(txtothdutyper.Text);
			OthDutyCessUnit = ddlothdutyrs.SelectedValue;
			OthDutyCessQty = Convert.ToDouble(txtothqtyforcess1.Text);
			OthDutyCessDesc = txtothcessdesc1.Text;
			ThirdCessNotn = ddlthirdcess.SelectedValue;
			ThirdCessRate = Convert.ToDouble(oththirdcessdutyrate.Text);
			ThirdCessAmount = Convert.ToDouble(txtthirdcessper.Text);
			ThirdCessUnit = ddlthirdcessrs.SelectedValue; 
			ThirdCessQty = Convert.ToDouble(txtthirdqtyforcess1.Text);
			ThirdCessDesc = txtthirdcessdesc0.Text;
			CENVATCertiNo = txtcertnum.Text;
			CENVATDate = txtcenvatdate.Text;
			CENVATValidUpto = txtvalidupto.Text;
			CENVATCExOffCode = txtcexofccode.Text;
			CENVATAssCode = txtAssessee.Text;
			ModifiedBy = (string)Session["USER-NAME"];
			ModifiedDate = DateTime.Now.ToString();

			result = objETProductBL.UpdateCessExpDuty(ProductID, JobNo, InvoiceNo, ExportDutyNotn, ExportDutyRate, ExportDutyAmount, ExportDutyUnit, ExportDutyQty, CessNotn,
								  CessRate, CessAmount, CessUnit, CessTariffValue, CessTariffValueUnit, CessQty, CessDesc,
								  OthDutyCessNotn, OthDutyCessRate, OthDutyCessAmount, OthDutyCessUnit, OthDutyCessQty, OthDutyCessDesc,
								  ThirdCessNotn, ThirdCessRate, ThirdCessAmount, ThirdCessUnit, ThirdCessQty, ThirdCessDesc, CENVATCertiNo,
								  CENVATDate, CENVATValidUpto, CENVATCExOffCode, CENVATAssCode, ModifiedBy, ModifiedDate);
			if (result == 1)
			{
				ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Saved Successfully')", true);
			}

		}

		protected void btnrexpsave_Click(object sender, EventArgs e)
		{
			ProductID = (string)Session["ProductID"];
			JobNo = ddlJobNo.SelectedValue;
			InvoiceNo = ddlInvNo.SelectedValue;
			BENo = txtbenumber1.Text;
			BEDate = txtbenumdate.Text;
			QuantityExported = Convert.ToDouble(txtquanexp3.Text);
			QuantityExportedUnit = ddlquanexp2.SelectedValue;
			InvoiceSNo = txtinvoicesno.Text;
			ItemSNo = txttemsno.Text;
			TechnicalDetails = txttechnicaldet.Text;
			ImportPortCode = txtimportportcode.Text;
			BEItemDesc = txtbeitemdesc.Text;
			OtherIdenPara = txtothIdenParam1.Text;
			AgainstExpOblig = chkagaistoblig.Checked;
			ObligNo = txtOblinum0.Text;
			DrawbackAmtclaimed = Convert.ToDouble(txtdraAmtClaim.Text);
			QuantityImported = Convert.ToDouble(txtquantityimport3.Text);
			QuantityImportedUnit = ddlquantityimport4.SelectedValue;
			ItemUnUsed = chkitemunused.Checked;
			CommissionerPermi = chkcommpermisn.Checked;
			AssessableValue = Convert.ToDouble(txtassessibleval1.Text);
			BoardNo = txtboardnum1.Text;
			BoardDate = txtboarddate1.Text;
			TotalDutyPaid = Convert.ToDouble(txttotaldutypaid.Text);
			TotalDutyPaidDate = txttotdutaiddate.Text;
			MODVATAvailed = chkmodvatavail.Checked;
			MODVATReversed = chkmodvatreserved.Checked;
			ModifiedBy = (string)Session["USER-NAME"];
			ModifiedDate = DateTime.Now.ToString();

			result = objETProductBL.UpdateReExport( ProductID, JobNo, InvoiceNo, BENo, BEDate, QuantityExported, QuantityExportedUnit, InvoiceSNo, ItemSNo,
								  TechnicalDetails, ImportPortCode, BEItemDesc, OtherIdenPara, AgainstExpOblig, ObligNo, DrawbackAmtclaimed,
								  QuantityImported, QuantityImportedUnit, ItemUnUsed, CommissionerPermi, AssessableValue, BoardNo,
								  BoardDate, TotalDutyPaid, TotalDutyPaidDate, MODVATAvailed, MODVATReversed, ModifiedBy, ModifiedDate);

			if (result == 1)
			{
				ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Saved Successfully')", true);
			}

		}

		protected void btnothsave_Click(object sender, EventArgs e)
		{
			ProductID = (string)Session["ProductID"];
			JobNo = ddlJobNo.SelectedValue;
			InvoiceNo = ddlInvNo.SelectedValue;
			Accessories = txtotherRemarks.Text;
			ThirdPartyEXP = chkthirdparty.Checked;
			Manufacturer = txtManufacture.Text;
			IECode = txtotheriecode.Text;
			BranchSNo = txtothBranchsno.Text;
			//ThirdPartyEXPAddress = ddlotherAddress.SelectedValue;
			ThirdPartyEXPAddress1 = txtotheraddress.Text;
			ModifiedBy = (string)Session["USER-NAME"];
			ModifiedDate = DateTime.Now.ToString();

			result = objETProductBL.UpdateOtherDetails( ProductID, JobNo, InvoiceNo, Accessories, ThirdPartyEXP, Manufacturer, IECode, BranchSNo, ThirdPartyEXPAddress, ThirdPartyEXPAddress1, ModifiedBy, ModifiedDate);

			if (result == 1)
			{
				ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Saved Successfully')", true);
			}   
		}

		protected void btnAddar4_Click(object sender, EventArgs e)
		{
		   JobNo = ddlJobNo.SelectedValue;
		   InvoiceNo = ddlInvNo.SelectedValue;
		   AR4No =  txtar4no.Text;
		   AR4Date = txtar4date.Text;
		   Commissionerate = txtcommisionrate.Text;
		   Division = txtdivision.Text;
		   Range = txtrange.Text;
		   Remark = txtremarkss.Text;
		   CreatedBy = (string)Session["USER-NAME"];
		   CreatedDate = DateTime.Now.ToString();

		   result = objETProductAR4DetailsBL.save(JobNo, InvoiceNo, AR4No, AR4Date, Commissionerate, Division, Range, Remark, CreatedBy, CreatedDate);
		   if (result == 1)
		   {
			   ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Saved Successfully')", true);
			   AR4Clear();

		   }

		   ar4gridload();
		}

		private void AR4Clear()
		{
			txtar4no.Text = "";
			txtar4date.Text = "";
			txtcommisionrate.Text = "";
			txtdivision.Text = "";
			txtrange.Text = "";
			txtremarkss.Text = "";
		}

		private void ar4gridload()
		{
			DataSet ds = objETProductAR4DetailsBL.GetProductAR4Data(JobNo, InvoiceNo);
			if (ds.Tables["Table"].Rows.Count != 0)
			{
				gvAr4Details.DataSource = ds;
				gvAr4Details.DataBind();
			}
			else
			{
				gvAr4Details.DataSource = null;
				gvAr4Details.DataBind();
			}
		}

		protected void gvAr4Details_SelectedIndexChanged(object sender, EventArgs e)
		{
			hdnAr4.Value = gvAr4Details.SelectedRow.Cells[1].Text;
			JobNo = ddlJobNo.SelectedValue;
			InvoiceNo = ddlInvNo.SelectedValue;
			DataSet ds = new DataSet();
			string quer = "select  *  from [E_T_ProductAR4Details] where ID ='" + hdnAr4.Value + "' ";
			ds = objCommonDL.GetDataSet(quer);
			if (ds.Tables["Table"].Rows.Count != 0)
			{
				DataRowView dr = ds.Tables["Table"].DefaultView[0];
				JobNo = dr["JobNo"].ToString();
				txtar4no.Text = dr["AR4No"].ToString();
				txtar4date.Text = dr["AR4Date"].ToString();
				txtcommisionrate.Text = dr["Commissionerate"].ToString();
				txtdivision.Text = dr["Division"].ToString();
				txtrange.Text = dr["Range"].ToString();
				txtremarkss.Text = dr["Remark"].ToString();
				btnAddar4.Visible = false;
				btnUpdAr4.Visible = true;
			}
		}

		protected void btnUpdAr4_Click(object sender, EventArgs e)
		{
			
			JobNo = ddlJobNo.SelectedValue;
			InvoiceNo = ddlInvNo.SelectedValue;
			AR4No = txtar4no.Text;
			AR4Date = txtar4date.Text;
			Commissionerate = txtcommisionrate.Text;
			Division = txtdivision.Text;
			Range = txtrange.Text;
			Remark = txtremarkss.Text;
			ModifiedBy = (string)Session["USER-NAME"];
			ModifiedDate = DateTime.Now.ToString();

			result = objETProductAR4DetailsBL.update(hdnAr4.Value, JobNo, InvoiceNo, AR4No, AR4Date, Commissionerate, Division, Range, Remark, ModifiedBy, ModifiedDate);

			if (result == 1)
			{
				ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Updated Successfully')", true);
				AR4Clear();
			}
			ar4gridload();
			btnUpdAr4.Visible = false;
			btnAddar4.Visible = true;
		}

		protected void btnaddq_Click(object sender, EventArgs e)
		{
			JobNo = ddlJobNo.SelectedValue;
			InvoiceNo = ddlInvNo.SelectedValue;
			QuotaCertificateNo = txtquotacert.Text;
			Agency = ddlagencyq.SelectedValue;
			ExpiryDate = txtexpdateq.Text;
			QuotaQuantity = txtquantityq.Text;
			Unit = ddlunitq.SelectedValue;
			CreatedBy = (string)Session["USER-NAME"];
			CreatedDate = DateTime.Now.ToString();
			result = objETProductQuotaBL.save(JobNo, InvoiceNo, QuotaCertificateNo, Agency, ExpiryDate, QuotaQuantity, Unit, CreatedBy, CreatedDate);

			if (result == 1)
			{
				ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Saved Successfully')", true);
				QuotaClear();
			}

			quotagridload();
		}

		private void QuotaClear()
		{
			txtquotacert.Text = "";
			ddlagencyq.SelectedIndex = 0;
			txtexpdateq.Text = "";
			txtquantityq.Text = "";
			ddlunitq.SelectedValue= "0";
		}

		private void quotagridload()
		{
			DataSet ds = new DataSet();
			string quer = "select  *  from [E_T_ProductQuota] where jobno='" +ddlJobNo.SelectedValue + "' and  InvoiceNo='" + ddlInvNo.SelectedValue + "'";
			ds = objCommonDL.GetDataSet(quer);
			if (ds.Tables["Table"].Rows.Count != 0)
			{
				gvproductquota.DataSource = ds;
				gvproductquota.DataBind();
			}
			else
			{
				gvproductquota.DataSource = null;
				gvproductquota.DataBind();
			}
		}

		protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
		{
			JobNo = ddlJobNo.SelectedValue;
			InvoiceNo = gvProductExp.SelectedRow.Cells[2].Text;
			DataSet ds = new DataSet();
			string quer = "select  *  from [E_T_ProductQuota] where JobNo ='" + JobNo + "' and InvoiceNo = '" + InvoiceNo + "'";
			ds = objCommonDL.GetDataSet(quer);
			if (ds.Tables["Table"].Rows.Count != 0)
			{
				DataRowView dr = ds.Tables["Table"].DefaultView[0];
				JobNo = dr["JobNo"].ToString();
				DocType = ddldoctype.SelectedValue;
				QuotaCertificateNo = txtquotacert.Text;
				Agency = ddlagencyq.SelectedValue;
				ExpiryDate = txtexpdateq.Text;
				QuotaQuantity = txtquantityq.Text;
				Unit = ddlunitq.SelectedValue;           
			}
		}

		protected void btnupdateq_Click(object sender, EventArgs e)
		{
			JobNo = ddlJobNo.SelectedValue;
			InvoiceNo = ddlInvNo.SelectedValue;
			DocType = ddldoctype.SelectedValue;
			QuotaCertificateNo = txtquotacert.Text;
			Agency = ddlagencyq.SelectedValue;
			ExpiryDate = txtexpdateq.Text;
			QuotaQuantity = txtquantityq.Text;
			Unit = ddlunitq.SelectedValue;
			ModifiedBy = (string)Session["USER-NAME"];
			ModifiedDate = DateTime.Now.ToString();

			result = objETProductQuotaBL.update(hdnQuota.Value, JobNo, InvoiceNo, QuotaCertificateNo, Agency, ExpiryDate, QuotaQuantity, Unit, ModifiedBy, ModifiedDate);

			if (result == 1)
			{
				ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Updated Successfully')", true);
				QuotaClear();
			}
			quotagridload();
			btnupdateq.Visible =false ;
			btnaddq.Visible = true;
		   
		}

		protected void btnUpdategeneral_Click(object sender, EventArgs e)
		{
			string DocId = hdnDoc.Value;
			JobNo = ddlJobNo.SelectedValue;
			InvoiceNo = ddlInvNo.SelectedValue;
			DocType = ddldoctype.SelectedValue;
			GeneralDescription = txtdescr.Text;
			AgencyCode = txtagencyco.Text;
			AgencyName = txtagencyname.Text;
			DocumentName = txtDocname.Text;
			CreatedBy = (string)Session["USER-NAME"];
			CreatedDate = DateTime.Now.ToString();

			result = objETProductDocumentReleasingBL.update(DocId,JobNo, InvoiceNo, DocType, GeneralDescription, AgencyCode, AgencyName, DocumentName, CreatedBy, CreatedDate);

			if (result == 1)
			{
				ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Updated Successfully')", true);
				//DocumentClear();
			}

			DataSet ds = objETProductDocumentReleasingBL.GetProductDocumentReleasingData(JobNo, InvoiceNo);
			if (ds.Tables["Table"].Rows.Count != 0)
			{
				gvprodgen.DataSource = ds;
				gvprodgen.DataBind();
			}
			else
			{
				gvprodgen.DataSource = null;
				gvprodgen.DataBind();
			}
			btnaddGenral.Visible =true ;
			btnUpdategeneral.Visible = false;
			DocumentClear();
		}

		protected void gvprodgen_SelectedIndexChanged(object sender, EventArgs e)
		{
			btnaddGenral.Visible = false;
			btnUpdategeneral.Visible = true;
			hdnDoc.Value = gvprodgen.SelectedRow.Cells[1].Text;
			JobNo = ddlJobNo.SelectedValue;
			InvoiceNo = ddlInvNo.SelectedValue;
			DataSet ds = new DataSet();
			string quer = "select  *  from [E_T_ProductDocumentReleasing] where ID ='" + hdnDoc.Value + "' ";
			ds = objCommonDL.GetDataSet(quer);
			if (ds.Tables["Table"].Rows.Count != 0)
			{
				DataRowView dr = ds.Tables["Table"].DefaultView[0];
				ddldoctype.SelectedValue = dr["DocType"].ToString();
				txtdescr.Text = dr["Description"].ToString();
				txtagencyco.Text = dr["AgencyCode"].ToString();
				txtagencyname.Text = dr["AgencyName"].ToString();
				txtDocname.Text = dr["DocumentName"].ToString();
			}
		}

		protected void gvproductquota_SelectedIndexChanged(object sender, EventArgs e)
		{
			btnupdateq.Visible = true;
			btnaddq.Visible = false;
			hdnQuota.Value = gvproductquota.SelectedRow.Cells[1].Text;
			JobNo = ddlJobNo.SelectedValue;
			InvoiceNo = ddlInvNo.SelectedValue;
			DataSet ds = new DataSet();
			string quer = "select  *  from [E_T_ProductQuota] where ID = '" + hdnQuota.Value + "'";
			ds = objCommonDL.GetDataSet(quer);
			if (ds.Tables["Table"].Rows.Count != 0)
			{
				DataRowView dr = ds.Tables["Table"].DefaultView[0];
				ddlagencyq.SelectedValue = dr["Agency"].ToString();
				txtquotacert.Text = dr["QuotaCertificateNo"].ToString();
				txtexpdateq.Text = dr["ExpiryDate"].ToString();
				txtquantityq.Text = dr["Quantity"].ToString();
				ddlunitq.SelectedValue = dr["Unit"].ToString();
			}
		}

		protected void btnReturn_Click(object sender, EventArgs e)
		{
			Session["ExJobNo"] = ddlJobNo.SelectedValue;
			Response.Redirect("efrmInvoiceExport.aspx");
		}

		protected void btnMainCancel_Click(object sender, EventArgs e)
		{
			Clear();
			btnAddd.Visible = true;
			btnUpdate.Visible = false;
		}

		protected void btnCheckList_Click(object sender, EventArgs e)
		{
			Session["JobNo"] = ddlJobNo.SelectedValue;
			Session["InvoiceNo"] = ddlInvNo.SelectedValue;
			Response.Redirect("efrmPrintCheckList.aspx");
		}

		protected void txtProductCode_TextChanged(object sender, EventArgs e)
		{
			try
			{
				DataSet ds = obj.GetProductMasterCode(txtProductCode.Text);
				if (ds.Tables["product"].Rows.Count != 0)
				{
					DataRowView row = ds.Tables["product"].DefaultView[0];
					txtDesc.Text = row["ProductDesc"].ToString();
					txtRitccode.Text = row["RITCNo"].ToString();                    
				}
			}
			catch
			{
			}
		}

		protected void txtDesc_TextChanged(object sender, EventArgs e)
		{
			try
			{

				DataSet ds = obj.GetProductMaster(txtDesc.Text);
				if (ds.Tables["product"].Rows.Count != 0)
				{
					DataRowView row = ds.Tables["product"].DefaultView[0];
					txtProductCode.Text = row["ProductCode"].ToString();
					txtRitccode.Text = row["RITCNo"].ToString();                    
				}
			}
			catch
			{
			}
		}

		protected void btnDEPBSave_Click(object sender, EventArgs e)
		{
			JobNo = ddlJobNo.SelectedValue;
			InvoiceNo = ddlInvNo.SelectedValue;
			string ProductDesc = txtDesc.Text;
			bool  DEPBItem = chkDEPB.Checked;
			string ProductGroup = txtProductGroup.Text;
			string RateListNo = txtRateListSrNo.Text;
			double DEPBRate = Convert.ToDouble(txtDEPBRate.Text);
			double DEPBQty = Convert.ToDouble(txtDEPBQty.Text);
			string DEPBUnit = ddlDEPBUnit.SelectedItem.Text;
			string CAPValue = txtCapValue.Text;
			string StdIONorms = txtStdIONorms.Text;
			bool DEPBCredit = chkDEPBCredit.Checked;
			CreatedBy = (string)Session["USER-NAME"];
			CreatedDate = DateTime.Now.ToString();

			result = objETProductBL.DEPBSave(JobNo, InvoiceNo, ProductDesc, DEPBItem, ProductGroup, RateListNo, DEPBRate, DEPBQty,
			DEPBUnit, CAPValue, StdIONorms, DEPBCredit, CreatedBy, CreatedDate);
		  
			if (result == 1)
			{
				ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Saved Successfully')", true);
			}

		}

		protected void btnDEPBUpdate_Click(object sender, EventArgs e)
		{
			JobNo = ddlJobNo.SelectedValue;
			InvoiceNo = ddlInvNo.SelectedValue;
			string ProductDesc = txtDesc.Text;
			bool DEPBItem = chkDEPB.Checked;
			string ProductGroup = txtProductGroup.Text;
			string RateListNo = txtRateListSrNo.Text;
			double DEPBRate = Convert.ToDouble(txtDEPBRate.Text);
			double DEPBQty = Convert.ToDouble(txtDEPBQty.Text);
			string DEPBUnit = ddlDEPBUnit.SelectedItem.Text;
			string CAPValue = txtCapValue.Text;
			string StdIONorms = txtStdIONorms.Text;
			bool DEPBCredit = chkDEPBCredit.Checked;
			ModifiedBy = (string)Session["USER-NAME"];
			ModifiedDate = DateTime.Now.ToString();

			 result = objETProductBL.DEPBUpdate(JobNo, InvoiceNo, ProductDesc, DEPBItem, ProductGroup, RateListNo, DEPBRate, DEPBQty,
				DEPBUnit, CAPValue, StdIONorms, DEPBCredit, ModifiedBy, ModifiedDate);

			if (result == 1)
			{
				ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Updated Successfully')", true);
			}
		}

		protected void btnDEPBCRAdd_Click(object sender, EventArgs e)
		{
			JobNo = ddlJobNo.SelectedValue;
			InvoiceNo = ddlInvNo.SelectedValue;
			string ProductDesc = txtDesc.Text;
			string ProductGroup = txtDEPBCRProgroup.Text;
			string RateListNo = txtDEPBCRRateList.Text;
			double DEPBRate = Convert.ToDouble(txtDEPBCRRate.Text);
			double DEPBQty = Convert.ToDouble(txtDEPBCRPerQty.Text);
			string DEPBUnit = ddlDEPBCRUnit.SelectedItem.Text;
			string QtyPercent = txtDEPBCRPerQty.Text;
			string CAPValue = txtDEPBCRCapValue.Text;
			CreatedBy = (string)Session["USER-NAME"];
			CreatedDate = DateTime.Now.ToString();

			result = objETProductBL.DEPBCreditSave(JobNo, InvoiceNo, ProductDesc, ProductGroup, RateListNo, DEPBRate, DEPBQty, DEPBUnit, QtyPercent, CAPValue, CreatedBy, CreatedDate);

			if (result == 1)
			{
				ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Saved Successfully')", true);
				DEPBCreditSelect();
                ClearDEPB();
			}
		}   
		 
		private void DEPBCreditSelect()
		{
            try
            {
                DataSet ds = new DataSet();
                string quer = "select   ID, JobNo, InvoiceNo, ProdDescription, ProductGroup, RateListNo, DEPBRate, DEPBQty, DEPBUnit, QtyPercent, CAPValue from [E_T_Product_DEPBCredit]  Where JobNo ='" + JobNo + "' And InvoiceNo='" + InvoiceNo + "' and ProdDescription='" + txtDesc.Text + "'";
                ds = objCommonDL.GetDataSet(quer);
                if (ds.Tables["Table"].Rows.Count != 0)
                {
                    gvDEPB.DataSource = ds;
                    gvDEPB.DataBind();
                }
                else
                {
                    gvDEPB.DataSource = null;
                    gvDEPB.DataBind();
                }
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }
		}

		protected void gvDEPB_SelectedIndexChanged(object sender, EventArgs e)
		{
            btnDEPBCRUpdate.Visible = true;
            btnDEPBCRAdd.Visible = false;
			Session["DEPBCRID"] = gvDEPB.SelectedRow.Cells[1].Text;
			DataSet ds = new DataSet();
			string quer = "select   ID, JobNo, InvoiceNo, ProdDescription, ProductGroup, RateListNo, DEPBRate, DEPBQty, DEPBUnit, QtyPercent, CAPValue from [E_T_Product_DEPBCredit]  where ID ='" + (string)Session["DEPBCRID"] + "' ";
			ds = objCommonDL.GetDataSet(quer);
			if (ds.Tables["Table"].Rows.Count != 0)
			{
				DataRowView dr = ds.Tables["Table"].DefaultView[0];
				txtDEPBCRProgroup.Text = dr["ProductGroup"].ToString();
				txtDEPBCRRateList.Text = dr["RateListNo"].ToString();
				txtDEPBCRRate.Text = dr["DEPBRate"].ToString();
				txtDEPBCRPerQty.Text = dr["DEPBQty"].ToString();
				ddlDEPBCRUnit.SelectedItem.Text = dr["DEPBUnit"].ToString();
				txtDEPBCRCapValue.Text = dr["CAPValue"].ToString();
			}
		}

		protected void btnDEPBCRUpdate_Click(object sender, EventArgs e)
		{
			string ID = (string)Session["DEPBCRID"];
			JobNo = ddlJobNo.SelectedValue;
			InvoiceNo = ddlInvNo.SelectedValue;
			string ProductDesc = txtDesc.Text;
			string ProductGroup = txtDEPBCRProgroup.Text;
			string RateListNo = txtDEPBCRRateList.Text;
			double DEPBRate = Convert.ToDouble(txtDEPBCRRate.Text);
			double DEPBQty = Convert.ToDouble(txtDEPBCRPerQty.Text);
			string DEPBUnit = ddlDEPBCRUnit.SelectedItem.Text;
			string QtyPercent = txtDEPBCRPerQty.Text;
			string CAPValue = txtDEPBCRCapValue.Text;
			CreatedBy = (string)Session["USER-NAME"];
			CreatedDate = DateTime.Now.ToString();

			result = objETProductBL.DEPBCreditUpdate(ID,JobNo, InvoiceNo, ProductDesc, ProductGroup, RateListNo, DEPBRate, DEPBQty, DEPBUnit, QtyPercent, CAPValue, CreatedBy, CreatedDate);

			if (result == 1)
			{
				ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Updated Successfully')", true);
				DEPBCreditSelect();
                ClearDEPB();
			}
		}

		protected void btnDBKSave_Click(object sender, EventArgs e)
		{
			JobNo = ddlJobNo.SelectedValue;
			InvoiceNo = ddlInvNo.SelectedValue;
			string ProductDesc = txtDesc.Text;
			bool DBKItem = chkDrawback.Checked;
			string DBKSNO = ddlDbkSrNo.SelectedItem.Text;
			string FOBvalue = txtFobValue.Text;
			string Qty = txtDrawQuantity.Text;
			string Unit = ddlDrawUnit.SelectedItem.Text;
			string DBKUnder = rbDBKUnder.SelectedValue;
			string DBKDesc = txtDBKDesc.Text;
			double DBKRate =Convert.ToDouble(txtDBKRate.Text);
			double DBKCap = Convert.ToDouble(txtDBKCap1.Text);
			string  DBKUnit = txtDBKCap1.Text;
			double DBKAmount = Convert.ToDouble(txtDBKAmount.Text);
			string DBKAmountDesc = txtDBKAmtDesc.Text;
			CreatedBy = (string)Session["USER-NAME"];
			CreatedDate = DateTime.Now.ToString();

			result = objETProductBL.DrawbackSave(JobNo, InvoiceNo, ProductDesc, DBKItem, DBKSNO, FOBvalue, Qty, Unit, DBKUnder, DBKDesc, DBKRate, DBKCap, DBKUnit, DBKAmount, DBKAmountDesc, CreatedBy, CreatedDate);

			if (result == 1)
			{
				ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Saved Successfully')", true);
			}
		}

        private void DrawbackSelect()
        {
            try
            {
                JobNo = ddlJobNo.SelectedValue;
                InvoiceNo = ddlInvNo.SelectedValue;
                DataSet ds = new DataSet();
                string quer = "select  *  from [E_T_Product_Drawback]  Where JobNo ='" + JobNo + "' And InvoiceNo='" + InvoiceNo + "' and ProdDescription='" + txtDesc.Text + "'";
                ds = objCommonDL.GetDataSet(quer);
                if (ds.Tables["Table"].Rows.Count != 0)
                {
                    DataRowView dr = ds.Tables["Table"].DefaultView[0];
                    chkDrawback.Checked = Convert.ToBoolean(dr["DBKItem"]);
                    ddlDbkSrNo.SelectedItem.Text = dr["DBKSNO"].ToString();
                    txtFobValue.Text = dr["FOBValue"].ToString();
                    txtDrawQuantity.Text = dr["Qty"].ToString();
                    ddlDrawUnit.SelectedItem.Text = dr["Unit"].ToString();
                    rbDBKUnder.SelectedValue = dr["DBKUnder"].ToString();
                    txtDBKDesc.Text = dr["DBKDesc"].ToString();
                    txtDBKRate.Text = dr["DBKRate"].ToString();
                    txtDBKCap1.Text = dr["DBKCap"].ToString();
                    txtDBKCap1.Text = dr["DBKUnit"].ToString();
                    txtDBKAmount.Text = dr["DBKAmount"].ToString();
                    txtDBKAmtDesc.Text = dr["DBKAmountDesc"].ToString();
                    btnDBKSave.Visible = false;
                    btnDBKUpdate.Visible = true;
                }
                else
                {
                    btnDBKSave.Visible=true;
                    btnDBKUpdate.Visible = false;
                }
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }
        }

		protected void btnDBKUpdate_Click(object sender, EventArgs e)
		{
			JobNo = ddlJobNo.SelectedValue;
			InvoiceNo = ddlInvNo.SelectedValue;
			string ProductDesc = txtDesc.Text;
			bool DBKItem = chkDrawback.Checked;
			string DBKSNO = ddlDbkSrNo.SelectedItem.Text;
			string FOBvalue = txtFobValue.Text;
			string Qty = txtDrawQuantity.Text;
			string Unit = ddlDrawUnit.SelectedItem.Text;
			string DBKUnder = rbDBKUnder.SelectedValue;
			string DBKDesc = txtDBKDesc.Text;
			double DBKRate = Convert.ToDouble(txtDBKRate.Text);
			double DBKCap = Convert.ToDouble(txtDBKCap1.Text);
			string DBKUnit = txtDBKCap1.Text;
			double DBKAmount = Convert.ToDouble(txtDBKAmount.Text);
			string DBKAmountDesc = txtDBKAmtDesc.Text;
			ModifiedBy = (string)Session["USER-NAME"];
			ModifiedDate = DateTime.Now.ToString();

			result =  objETProductBL.DrawbackUpdate(JobNo, InvoiceNo, ProductDesc, DBKItem, DBKSNO, FOBvalue, Qty, Unit, DBKUnder, DBKDesc, DBKRate, DBKCap, DBKUnit, DBKAmount, DBKAmountDesc, ModifiedBy, ModifiedDate);

			if (result == 1)
			{
				ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Saved Successfully')", true);
			}
		}

        protected void btnAddDBKMaterials_Click(object sender, EventArgs e)
        {
            JobNo = ddlJobNo.SelectedValue;
            InvoiceNo = ddlInvNo.SelectedValue;
            string ProductDesc = txtDesc.Text;
            string SNO	= txtDBKMDesc.Text;
            string Description	= txtDBKMDesc.Text;
            string ExciseDBKRate = txtDBKMEXRate.Text;
            string CustomDBKRate =	txtDBKMCRate.Text;
            string Qty	= txtDBKMQty.Text;
            string Unit = ddlDBKMUnit.SelectedItem.Text;
            CreatedBy = (string)Session["USER-NAME"];
            CreatedDate = DateTime.Now.ToString();

            result = objETProductBL.DrawbackMaterialsSave(JobNo, InvoiceNo, ProductDesc, SNO, Description, ExciseDBKRate, CustomDBKRate,Qty, Unit, CreatedBy, CreatedDate);

            if (result == 1)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Saved Successfully')", true);
                DrawbackMaterialSelect();
                ClearDrawback();
            }
        }

        private void DrawbackMaterialSelect()
        {
            try
            {
                JobNo = ddlJobNo.SelectedValue;
                InvoiceNo = ddlInvNo.SelectedValue;
                DataSet dsDBK = new DataSet();
                string quer = "select  ID,JobNo, InvoiceNo, ProdDescription, SNO, Description, ExciseDBKRate, CustomDBKRate, Qty, Unit  from [E_T_Product_DrawbackMaterials]  Where JobNo ='" + JobNo + "' And InvoiceNo='" + InvoiceNo + "' and ProdDescription='" + txtDesc.Text + "'";
                dsDBK = objCommonDL.GetDataSet(quer);
                if (dsDBK.Tables["Table"].Rows.Count != 0)
                {
                    gvDrawback.DataSource = dsDBK;
                    gvDrawback.DataBind();
                }
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }
        }

        protected void gvDrawback_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnUpdateDBKMaterials.Visible = true;
            btnAddDBKMaterials.Visible = false;
            Session["DrawBackID"] = gvDrawback.SelectedRow.Cells[1].Text;
            DataSet ds = new DataSet();
            string quer = "select  JobNo, InvoiceNo, ProdDescription, SNO, Description, ExciseDBKRate, CustomDBKRate, Qty, Unit  from [E_T_Product_DrawbackMaterials] Where ID='" + (string)Session["DrawBackID"] + "'";
            ds = objCommonDL.GetDataSet(quer);
            if (ds.Tables["Table"].Rows.Count != 0)
            {
                DataRowView dr = ds.Tables["Table"].DefaultView[0];
                txtDBKMDesc.Text = dr["Description"].ToString();
                txtDBKMEXRate.Text = dr["ExciseDBKRate"].ToString();
                txtDBKMCRate.Text = dr["CustomDBKRate"].ToString();
                txtDBKMQty.Text = dr["Qty"].ToString();
                ddlDBKMUnit.SelectedItem.Text = dr["Unit"].ToString();
            }
        }

        protected void btnUpdateDBKMaterials_Click(object sender, EventArgs e)
        {
            string ID = (string)Session["DrawBackID"];
            JobNo = ddlJobNo.SelectedValue;
            InvoiceNo = ddlInvNo.SelectedValue;
            string ProductDesc = txtDesc.Text;
            string SNO = txtDBKMDesc.Text;
            string Description = txtDBKMDesc.Text;
            string ExciseDBKRate = txtDBKMEXRate.Text;
            string CustomDBKRate = txtDBKMCRate.Text;
            string Qty = txtDBKMQty.Text;
            string Unit = ddlDBKMUnit.SelectedItem.Text;
            CreatedBy = (string)Session["USER-NAME"];
            CreatedDate = DateTime.Now.ToString();

            result = objETProductBL.DrawbackMaterialsUpdate(ID, JobNo, InvoiceNo, ProductDesc, SNO, Description, ExciseDBKRate, CustomDBKRate,
            Qty, Unit, ModifiedBy, ModifiedDate);

            if (result == 1)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Updated Successfully')", true);
                DrawbackMaterialSelect();
                ClearDrawback();
                btnUpdateDBKMaterials.Visible = false;
                btnAddDBKMaterials.Visible = true;
            }
        }

        protected void btnAddEDI_Click(object sender, EventArgs e)
        {
            JobNo = ddlJobNo.SelectedValue;
            InvoiceNo = ddlInvNo.SelectedValue;
            string ProductDesc = txtDesc.Text;
            bool EPCGItem = chkEPCG.Checked;
            string EDIRegnNo = txtEDIRegNo.Text;
            string EDIRegnDate = txtEDIDate.Text;
            CreatedBy = (string)Session["USER-NAME"];
            CreatedDate = DateTime.Now.ToString();

            result = objETProductBL.EPCGSave(JobNo, InvoiceNo, ProductDesc, EPCGItem, EDIRegnNo, EDIRegnDate, CreatedBy, CreatedDate);
            if (result == 1)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Saved Successfully')", true);
                EDISelect();
                ClearEDI();
            }

        }

        private void EDISelect()
        {
            try
            {
                JobNo = ddlJobNo.SelectedValue;
                InvoiceNo = ddlInvNo.SelectedValue;
                DataSet dsEDI = new DataSet();
                string quer = "select   ID, JobNo, InvoiceNo, ProdDescription, EPCGItem, EDIRegnNo, EDIRegnDate  from [E_T_Product_EPCG]  Where JobNo ='" + JobNo + "' And InvoiceNo='" + InvoiceNo + "' and ProdDescription='" + txtDesc.Text + "'";
                dsEDI = objCommonDL.GetDataSet(quer);
                if (dsEDI.Tables["Table"].Rows.Count != 0)
                {
                    gvEDI.DataSource = dsEDI;
                    gvEDI.DataBind();
                }
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }
        }

        protected void gvEDI_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                btnEDIUpdate.Visible = true;
                btnAddEDI.Visible = false;
                Session["EDIID"] = gvEDI.SelectedRow.Cells[1].Text;
                DataSet ds = new DataSet();
                string quer = "select   ID, JobNo, InvoiceNo, ProdDescription, EPCGItem, EDIRegnNo, EDIRegnDate  from [E_T_Product_EPCG] Where ID='" + (string)Session["EDIID"] + "'";
                ds = objCommonDL.GetDataSet(quer);
                if (ds.Tables["Table"].Rows.Count != 0)
                {
                    DataRowView dr = ds.Tables["Table"].DefaultView[0];
                    chkEPCG.Checked = Convert.ToBoolean(dr["EPCGItem"].ToString());
                    txtEDIRegNo.Text = dr["EDIRegnNo"].ToString();
                    txtEDIDate.Text = dr["EDIRegnDate"].ToString();
                }
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }
        }

        protected void btnEDIUpdate_Click(object sender, EventArgs e)
        {
            string ID = (string)Session["EDIID"];
            JobNo = ddlJobNo.SelectedValue;
            InvoiceNo = ddlInvNo.SelectedValue;
            string ProductDesc = txtDesc.Text;
            bool EPCGItem = chkEPCG.Checked;
            string EDIRegnNo = txtEDIRegNo.Text;
            string EDIRegnDate = txtEDIDate.Text;
            CreatedBy = (string)Session["USER-NAME"];
            CreatedDate = DateTime.Now.ToString();

            result = objETProductBL.EPCGUpdate(ID, JobNo, InvoiceNo, ProductDesc, EPCGItem, EDIRegnNo, EDIRegnDate, CreatedBy, CreatedDate);
            if (result == 1)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Updated Successfully')", true);
                EDISelect();
                ClearEDI();
                btnEDIUpdate.Visible = false;
                btnAddEDI.Visible = true;
            }
        }

        protected void btnAddEBCG_Click(object sender, EventArgs e)
        {
            JobNo = ddlJobNo.SelectedValue;
            InvoiceNo = ddlInvNo.SelectedValue;
            string ProductDesc = txtDesc.Text;
            string EDIRegnNo = txtEDIRegNo.Text;
            string ItemSnoExport = txtExpPartE.Text;
            string ExportQtyUnderLicence = txtExpQtyUnderLic.Text;
            string ImportItemSNO = txtItempartc.Text;
            string ImportSNO = txtItempartc.Text;
            string ImportDesc = txtEPCGDes.Text;
            string ImportQty = txtEPCGQuantity.Text;
            string ImportUnit = txtEPCGUnit.Text;
            string ImportType = txtEPCGType.Text;
            CreatedBy = (string)Session["USER-NAME"];
            CreatedDate = DateTime.Now.ToString();

            result = objETProductBL.EPCGItemsSave(JobNo, InvoiceNo, ProductDesc, EDIRegnNo, ItemSnoExport, ExportQtyUnderLicence, ImportSNO, ImportItemSNO, ImportDesc, ImportQty, ImportUnit, ImportType, CreatedBy, CreatedDate);
            if (result == 1)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Saved Successfully')", true);
                EPCGitemsSelect();
                ClearEDIItems();
            }
        }

        private void EPCGitemsSelect()
        {
            try
            {
                JobNo = ddlJobNo.SelectedValue;
                InvoiceNo = ddlInvNo.SelectedValue;
                DataSet dsEDI = new DataSet();
                string quer = "SELECT ID, JobNo, InvoiceNo, ProdDescription, EDIRegnNo, ItemSnoExport, ExportQtyUnderLicence, ImportSNO, ImportItemSNO, ImportDesc, ImportQty, ImportUnit, ImportType from [E_T_Product_EPCGItems]  Where JobNo ='" + JobNo + "' And InvoiceNo='" + InvoiceNo + "' and ProdDescription='" + txtDesc.Text + "'";
                dsEDI = objCommonDL.GetDataSet(quer);
                if (dsEDI.Tables["Table"].Rows.Count != 0)
                {
                    gvEBCG.DataSource = dsEDI;
                    gvEBCG.DataBind();
                }
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }
        }

        protected void btnUpdateEBCG_Click(object sender, EventArgs e)
        {
            string ID = (string)Session["EPCGItemsID"];
            JobNo = ddlJobNo.SelectedValue;
            InvoiceNo = ddlInvNo.SelectedValue;
            string ProductDesc = txtDesc.Text;
            string EDIRegnNo = txtEDIRegNo.Text;
            string ItemSnoExport = txtExpPartE.Text;
            string ExportQtyUnderLicence = txtExpQtyUnderLic.Text;
            string ImportItemSNO = txtItempartc.Text;
            string ImportSNO = txtItempartc.Text;
            string ImportDesc = txtEPCGDes.Text;
            string ImportQty = txtEPCGQuantity.Text;
            string ImportUnit = txtEPCGUnit.Text;
            string ImportType = txtEPCGType.Text;
            CreatedBy = (string)Session["USER-NAME"];
            CreatedDate = DateTime.Now.ToString();

            result = objETProductBL.EPCGItemsUpdate(ID,JobNo, InvoiceNo, ProductDesc, EDIRegnNo, ItemSnoExport, ExportQtyUnderLicence, ImportSNO, ImportItemSNO, ImportDesc, ImportQty, ImportUnit, ImportType, CreatedBy, CreatedDate);
            if (result == 1)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Updated Successfully')", true);
                EPCGitemsSelect();
                ClearEDIItems();
                btnUpdateEBCG.Visible = false;
                btnAddEBCG.Visible = true;
            }
        }

        protected void gvEBCG_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                btnUpdateEBCG.Visible = true;
                btnAddEBCG.Visible = false;
                Session["EPCGItemsID"] = gvEBCG.SelectedRow.Cells[1].Text;
                DataSet ds = new DataSet();
                string quer = " Select ID, JobNo, InvoiceNo, ProdDescription, EDIRegnNo, ItemSnoExport, ExportQtyUnderLicence, ImportSNO, ImportItemSNO, ImportDesc, ImportQty, ImportUnit, ImportType from [E_T_Product_EPCGItems] Where ID ='" + (string)Session["EPCGItemsID"] + "'";
                ds = objCommonDL.GetDataSet(quer);
                if (ds.Tables["Table"].Rows.Count != 0)
                {
                    DataRowView dr = ds.Tables["Table"].DefaultView[0];
                    txtExpPartE.Text = dr["ItemSnoExport"].ToString();
                    txtExpQtyUnderLic.Text = dr["ExportQtyUnderLicence"].ToString();
                    txtItempartc.Text = dr["ImportSNO"].ToString();
                    txtEPCGDes.Text = dr["ImportDesc"].ToString();
                    txtEPCGQuantity.Text = dr["ImportQty"].ToString();
                    txtEPCGUnit.Text = dr["ImportUnit"].ToString();
                    txtEPCGType.Text = dr["ImportType"].ToString();
                }
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }
        }

        protected void btnDEPB_Click(object sender, EventArgs e)
        {
            View1.Visible = false;
            View2.Visible = false;
            View3.Visible = false;
            View4.Visible = false;
            View5.Visible = false;
            View6.Visible = false;
            view7.Visible = true;
            view8.Visible = false;
            view9.Visible = false;
        }

        protected void btnDBK_Click(object sender, EventArgs e)
        {
            View1.Visible = false;
            View2.Visible = false;
            View3.Visible = false;
            View4.Visible = false;
            View5.Visible = false;
            View6.Visible = false;
            view7.Visible = false;
            view8.Visible = true;
            view9.Visible = false;
        }

        protected void btnEPCG_Click(object sender, EventArgs e)
        {
            View1.Visible = false;
            View2.Visible = false;
            View3.Visible = false;
            View4.Visible = false;
            View5.Visible = false;
            View6.Visible = false;
            view7.Visible = false;
            view8.Visible = false;
            view9.Visible = true;
        }

        private void ClearDEPB()
        {
            txtDEPBCRProgroup.Text = "";
            txtDEPBCRRateList.Text = "";
            txtDEPBCRRate.Text = "";
            ddlDEPBCRUnit.SelectedItem.Text = "~Select~";
            txtDEPBCRCapValue.Text = "";
            txtDEPBCRPerQty.Text = "0.00";
        }

        private void ClearDrawback()
        {
            txtDBKMDesc.Text = "";
            txtDBKMEXRate.Text = "0.00";
            txtDBKMCRate.Text = "0.00";
            txtDBKMQty.Text = "0.00";
            ddlDBKMUnit.SelectedItem.Text = "~Select~";
        }

        private void ClearEDI()
        {
            txtEDIRegNo.Text = "";
            txtEDIDate.Text = "";
        }
        private void ClearEDIItems()
        {
            txtExpPartE.Text = "";
            txtExpQtyUnderLic.Text = "";
            txtItempartc.Text = "";
            txtEPCGDes.Text = "";
            txtEPCGQuantity.Text = "";
            txtEPCGUnit.Text = "";
            txtEPCGType.Text = "";
        }

        private void ClearMainDEPB()
        {
            txtProductGroup.Text = "";
            txtRateListSrNo.Text = "";
            txtDEPBRate.Text = "0.00";
            txtDEPBQty.Text = "0.00";
            ddlDEPBUnit.SelectedItem.Text = "~Select~";
            txtCapValue.Text = "";
            txtStdIONorms.Text = "";
        }
        private void ClearMainDrawback()
        {
            ddlDbkSrNo.SelectedItem.Text = "~Select~";
            txtFobValue.Text = "";
            txtDrawQuantity.Text = "";
            ddlDrawUnit.SelectedItem.Text = "~Select~";
            txtDBKDesc.Text = "";
            txtDBKRate.Text = "0.00";
            txtDBKCap1.Text = "0.00";
            txtDBKAmount.Text = "0.00";
            txtDBKAmtDesc.Text = "";
        }

        private void EXIM()
        {

            DataSet dsExim = new DataSet();
            string quer = "Select Distinct EXIM_Code+'-'+Scheme As EXIM_Code,ApplicableExpSchemes from M_EximSchm where ApplicableExpSchemes<>''";
            dsExim = objCommonDL.GetDataSet(quer);
            if (dsExim.Tables["Table"].Rows.Count != 0)
            {
                ddlExim.DataSource = dsExim;
                ddlExim.DataTextField = "EXIM_Code";
                ddlExim.DataValueField = "ApplicableExpSchemes";
                ddlExim.DataBind();
            }
        }

        protected void ddlExim_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtEximDesc.Text = ddlExim.SelectedValue.ToString();
            string Value = ddlExim.SelectedValue;
            string[] scheme = Value.Split(',');
          

            //DrawBack Only
            if ((Value == "Drawback (DBK)") || (Value == "Drawback and Advance Licence") || (Value == "Drawback and DFRC") || (Value == "Drawback and JBG") ||
                (Value == "Drawback and Diamond Imprest licence") || (Value == "Drawback and EOU/EPZ/SEZ"))
            {
                btnDEPB.Visible = false;
                btnEPCG.Visible = false;
                btnDBK.Visible = true;
                NFEI.Style.Add("display", "none");
            }
            //EPCG Only
            else if (Value == "EPCG and Advance licence" || (Value == "Concessional duty EPCG scheme") || (Value == "Zero duty EPCG scheme") ||
                (Value == "Drawback and concessional duty EPCG") || (Value == "EPCG and DFRC") || (Value == "EPCG and JBG") || (Value == "EPCG and Diamond imprest Licence") ||
                (Value == "EPCG and Replenishment Licences") || (Value == "EPCG AND DFIA"))
            {
                btnDEPB.Visible = false;
                btnEPCG.Visible = true;
                btnDBK.Visible = false;
                NFEI.Style.Add("display", "none");
            }
            //DEPB Only
            else if ((Value == "DEPB-post exports") || (Value == "DEPB-post exports"))
            {
                btnDEPB.Visible = true;
                btnEPCG.Visible = false;
                btnDBK.Visible = false;
                NFEI.Style.Add("display", "none");
            }
            //Drawback and DEPB
            else if ((Value == "Drawback and Pre export DEPB") || (Value == "Drawback and post export DEPB"))
            {
                btnDEPB.Visible = true;
                btnEPCG.Visible = false;
                btnDBK.Visible = true;
                NFEI.Style.Add("display", "none");
            }
            //EPCG and DEPB
            else if ((Value == "EPCG and DEPB (Post exports)") || (Value == "EPCG and DEPB (Pre-exports)"))
            {
                btnDEPB.Visible = true;
                btnEPCG.Visible = true;
                btnDBK.Visible = false;
                NFEI.Style.Add("display", "none");
            }
            //Drawback and EPCG
            else if ((Value == "Drawback and zero duty EPCG") || (Value == "Drawback and concessional duty EPCG") || (Value == "EPCG, DRAWBACK AND DEEC") ||
                (Value == "EPCG, DRAWBACK AND DFRC") || (Value == "EPCG, DRAWBACK AND JOBBING") || (Value == "EPCG, DRAWBACK AND DIAMOND IMPREST LICENCE") ||
                (Value == "EPCG, DRAWBACK AND DFIA"))
            {
                btnDEPB.Visible = false;
                btnEPCG.Visible = true;
                btnDBK.Visible = true;
                NFEI.Style.Add("display", "none");
            }
            //Drawback , EPCG  , DEPB
            else if (Value == "EPCG, DRAWBACK AND DEPB POST EXPORT")
            {
                btnDEPB.Visible = true;
                btnEPCG.Visible = true;
                btnDBK.Visible = true;
                NFEI.Style.Add("display", "none");
            }

            else if (Value == "NFEI")
            {
                btnDEPB.Visible = false;
                btnEPCG.Visible = false;
                btnDBK.Visible = false;
                NFEI.Style.Add("display", "table-row");
            }
            else
            {
                btnDEPB.Visible = false;
                btnEPCG.Visible = false;
                btnDBK.Visible = false;
                NFEI.Style.Add("display", "none");
            }
            btnEPCG.Visible = true;
        }
	}
}