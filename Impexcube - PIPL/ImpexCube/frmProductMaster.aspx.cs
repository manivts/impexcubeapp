using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using System.Configuration;
using VTS.ImpexCube.Data;

namespace ImpexCube
{
    public partial class frmProductMaster : System.Web.UI.Page
    {
        string strcon = (string)ConfigurationManager.ConnectionStrings["Constr"].ConnectionString;
        VTS.ImpexCube.Business.ProductDetailsBL obj = new VTS.ImpexCube.Business.ProductDetailsBL();
        VTS.ImpexCube.Business.ShipmentBL objShipment = new VTS.ImpexCube.Business.ShipmentBL();
        CommonDL objCommonDL = new CommonDL();
        int Result = 0;
        
        protected void Page_Load(object sender, EventArgs e)
        {

            if (IsPostBack == false)
            {
                pnlProductDeatils.Visible = false;
                pnlSearch.Visible = false;
                Label pagename;
                pagename = (Label)Master.FindControl("lblName");
                pagename.Text = "Product Master";
                DropCountry();
                Session["ProMode"] ="Save";
                btnSave.Text = "Save";
            }
        }

        private void DropCountry()
        {
            DataSet ds = objShipment.GetCountry();
            if (ds.Tables["country"].Rows.Count != 0)
            {
                ddlcountryorigin.DataSource = ds;
                ddlcountryorigin.DataTextField = "CountryName";
                ddlcountryorigin.DataValueField = "CountryCode";
                ddlcountryorigin.DataBind();
            }
            else
            {
                ddlcountryorigin.DataSource = null;
                ddlcountryorigin.DataBind();
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if ((string)Session["ProMode"] == "Save")
            { 
            SaveProduct();
            }
            else if((string)Session["ProMode"] == "Update")
            {
            UpdateProduct();
            }
        }

        private void SaveProduct()
        {
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
                    Query.Append("SUR_NOTN,SUR_SNO,SURCHARGE,SAPTA_Notn,SAPTA_SNo,Createdby,CreatedDate)");

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
                    cmd.Parameters.AddWithValue("@ProductCode", txtProCode.Text);
                    cmd.Parameters.AddWithValue("@ProductDesc", txtProName.Text);
                    cmd.Parameters.AddWithValue("@RITCNo", txtRITC.Text);
                    //General
                    cmd.Parameters.AddWithValue("@GEN_DESC", txtgenericdesc.Text);
                    cmd.Parameters.AddWithValue("@ACCESSORY", txtaccessories.Text);
                    cmd.Parameters.AddWithValue("@MANUFACTURER", txtmanufacturer.Text);
                    cmd.Parameters.AddWithValue("@END_USE", endcase.Text);
                    cmd.Parameters.AddWithValue("@BRAND", brand.Text);
                    cmd.Parameters.AddWithValue("@MODEL", txtmodel.Text);
                    cmd.Parameters.AddWithValue("@CNTRY_ORIG", ddlcountryorigin.SelectedItem.Text);
                    //Duty Calculation

                    cmd.Parameters.AddWithValue("@CTHNo", txtCTH.Text);
                    cmd.Parameters.AddWithValue("@CETNo", txtCETNo.Text);
                    //Basci Duty
                    cmd.Parameters.AddWithValue("@BASDuty", txtBasicDutyRate.Text);
                    cmd.Parameters.AddWithValue("@BASSNo", txtBasicDutySno.Text);
                    cmd.Parameters.AddWithValue("@BASNotn", txtBasicDutyNotn.Text);
                    cmd.Parameters.AddWithValue("@BASDFlag", txtBasicDutyFlag.Text);
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
                    cmd.Parameters.AddWithValue("@HLTH_DFlag", txtExHealthCessFlag.Text);
                    cmd.Parameters.AddWithValue("@HLTH_Amt", txtExHealthCessAmount.Text);
                    cmd.Parameters.AddWithValue("@HLTH_Unit", txtExHealthCessUnit.Text);
                    //Cess & Notn
                    cmd.Parameters.AddWithValue("@CESS_NOTN", txtExCessNotn.Text);
                    cmd.Parameters.AddWithValue("@CESS_SNO", txtExCessSlNo.Text);
                    cmd.Parameters.AddWithValue("@CESS_DUTY", txtExCessRate.Text);
                    cmd.Parameters.AddWithValue("@CESS_DFLAG", txtExCessFlag.Text);
                    cmd.Parameters.AddWithValue("@CESS_AMT", txtExCessAmount.Text);
                    cmd.Parameters.AddWithValue("@CESS_UNIT", txtExCessUnit.Text);
                    //SAD Notn. & Duty

                    //Addl Notn

                    //NCD
                    cmd.Parameters.AddWithValue("@NCD_Notn", txtNCDNotn.Text);
                    cmd.Parameters.AddWithValue("@NCD_SNo", txtNCDSNo.Text);
                    cmd.Parameters.AddWithValue("@NCD_Rate", txtNCDRate.Text);
                    cmd.Parameters.AddWithValue("@NCD_DFlag", txtNCDFlag.Text);
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
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "redirect", "alert('Saved Successfully'); window.location.href='frmProductMaster.aspx';", true);
                }
            }
            catch (SqlException ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert(' " + ex.Message + " ');", true);
            }
        }

        private void UpdateProduct()
        {
            string ModifiedBy = (string)Session["USER-NAME"];
            string ModifiedDate = DateTime.Now.ToString();

            StringBuilder Query = new StringBuilder();

            string Message = string.Empty;
            try
            {
                using (SqlConnection con = new SqlConnection(strcon))
                {
                    con.Open();
                    Query.Append("Update [M_Product] SET ProductCode=@ProductCode,ProductDesc=@ProductDesc,RITCNo=@RITCNo,GEN_DESC=@GEN_DESC,ACCESSORY=@ACCESSORY,MANUFACTURER=@MANUFACTURER,END_USE=@END_USE,BRAND=@BRAND,MODEL=@MODEL,CNTRY_ORIG=@CNTRY_ORIG,");
                    Query.Append("CTHNo=@CTHNo,CETNo=@CETNo,BASDuty=@BASDuty,BASSNo=@BASSNo,BASNotn=@BASNotn,BASDFlag=@BASDFlag,BASAmt=@BASAmt,BASUnit=@BASUnit,AddlDuty_NOTN=@AddlDuty_NOTN,AddlDuty_SNO=@AddlDuty_SNO,AddlDuty_RATE=@AddlDuty_RATE,");
                    Query.Append("MRPSNo=@MRPSNo,MRP=@MRP,MRPUnit=@MRPUnit,Abatement=@Abatement,CVDNotn=@CVDNotn,CVDSNo=@CVDSNo,CVDAmt=@CVDAmt,POLICYPARA=@POLICYPARA,POLICY_YR=@POLICY_YR,");
                    Query.Append("EDU_CESS_NOTN=@EDU_CESS_NOTN,EDU_CESS_SNO=@EDU_CESS_SNO,EDU_CESS_RATE=@EDU_CESS_RATE,SHE_CESS_NOTN=@SHE_CESS_NOTN,SHE_CESS_SNO=@SHE_CESS_SNO,SHE_CESS_RATE=@SHE_CESS_RATE,");
                    Query.Append("EDU_CESS_RATE_EXC=@EDU_CESS_RATE_EXC,SHE_CESS_RATE_EXC=@SHE_CESS_RATE_EXC,");
                    Query.Append("HLTH_Notn=@HLTH_Notn,HLTH_SNo=@HLTH_SNo,HLTH_Rate=@HLTH_Rate,HLTH_DFlag=@HLTH_DFlag,HLTH_Amt=@HLTH_Amt,HLTH_Unit=@HLTH_Unit,");
                    Query.Append("CESS_NOTN=@CESS_NOTN,CESS_SNO=@CESS_SNO,CESS_DUTY=@CESS_DUTY,CESS_DFLAG=@CESS_DFLAG,CESS_AMT=@CESS_AMT,CESS_UNIT=@CESS_UNIT,");
                    Query.Append("NCD_Notn=@NCD_Notn,NCD_SNo=@NCD_SNo,NCD_Rate=@NCD_Rate,NCD_DFlag=@NCD_DFlag,NCD_Amt=@NCD_Amt,NCD_Unit=@NCD_Unit,");
                    Query.Append("SUR_NOTN=@SUR_NOTN,SUR_SNO=@SUR_SNO,SURCHARGE=@SURCHARGE,SAPTA_Notn=@SAPTA_Notn,SAPTA_SNo=@SAPTA_SNo,ModifiedBy=@ModifiedBy,ModifiedDate=@ModifiedDate where [TransId] ='" + (string)Session["ProTransID"] + "' ");

                    SqlCommand cmd = new SqlCommand(Query.ToString(), con);
                    //Main
                    cmd.Parameters.AddWithValue("@ProductCode", txtProCode.Text);
                    cmd.Parameters.AddWithValue("@ProductDesc", txtProName.Text);
                    cmd.Parameters.AddWithValue("@RITCNo", txtRITC.Text);
                    //General
                    cmd.Parameters.AddWithValue("@GEN_DESC", txtgenericdesc.Text);
                    cmd.Parameters.AddWithValue("@ACCESSORY", txtaccessories.Text);
                    cmd.Parameters.AddWithValue("@MANUFACTURER", txtmanufacturer.Text);
                    cmd.Parameters.AddWithValue("@END_USE", endcase.Text);
                    cmd.Parameters.AddWithValue("@BRAND", brand.Text);
                    cmd.Parameters.AddWithValue("@MODEL", txtmodel.Text);
                    cmd.Parameters.AddWithValue("@CNTRY_ORIG", ddlcountryorigin.SelectedItem.Text);
                    //Duty Calculation

                    cmd.Parameters.AddWithValue("@CTHNo", txtCTH.Text);
                    cmd.Parameters.AddWithValue("@CETNo", txtCETNo.Text);
                    //Basci Duty
                    cmd.Parameters.AddWithValue("@BASDuty", txtBasicDutyRate.Text);
                    cmd.Parameters.AddWithValue("@BASSNo", txtBasicDutySno.Text);
                    cmd.Parameters.AddWithValue("@BASNotn", txtBasicDutyNotn.Text);
                    cmd.Parameters.AddWithValue("@BASDFlag", txtBasicDutyFlag.Text);
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
                    cmd.Parameters.AddWithValue("@HLTH_DFlag", txtExHealthCessFlag.Text);
                    cmd.Parameters.AddWithValue("@HLTH_Amt", txtExHealthCessAmount.Text);
                    cmd.Parameters.AddWithValue("@HLTH_Unit", txtExHealthCessUnit.Text);
                    //Cess & Notn
                    cmd.Parameters.AddWithValue("@CESS_NOTN", txtExCessNotn.Text);
                    cmd.Parameters.AddWithValue("@CESS_SNO", txtExCessSlNo.Text);
                    cmd.Parameters.AddWithValue("@CESS_DUTY", txtExCessRate.Text);
                    cmd.Parameters.AddWithValue("@CESS_DFLAG", txtExCessFlag.Text);
                    cmd.Parameters.AddWithValue("@CESS_AMT", txtExCessAmount.Text);
                    cmd.Parameters.AddWithValue("@CESS_UNIT", txtExCessUnit.Text);
                    //SAD Notn. & Duty

                    //Addl Notn

                    //NCD
                    cmd.Parameters.AddWithValue("@NCD_Notn", txtNCDNotn.Text);
                    cmd.Parameters.AddWithValue("@NCD_SNo", txtNCDSNo.Text);
                    cmd.Parameters.AddWithValue("@NCD_Rate", txtNCDRate.Text);
                    cmd.Parameters.AddWithValue("@NCD_DFlag", txtNCDFlag.Text);
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
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "redirect", "alert('Saved Successfully'); window.location.href='frmProductMaster.aspx';", true);
                }
            }
            catch (SqlException ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert(' " + ex.Message + " ');", true);
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            string quer = string.Empty;
            DataSet ds = new DataSet();
            quer = "select TransId,ProductCode,ProductDesc,RITCNo,CETNo,CTHNo from M_Product where ((ProductDesc LIKE '%" + txtSearch.Text + "%') OR (ProductCode LIKE '%" + txtSearch.Text + "%'))";
            ds = objCommonDL.GetDataSet(quer);
            if (ds.Tables["Table"].Rows.Count != 0)
            {
                gvProduct.DataSource = ds;
                gvProduct.DataBind();
            }
            else
            {
                gvProduct.DataSource = null;
                gvProduct.DataBind();
            }
        }

        protected void gvProduct_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnSave.Text = "Update";
            Session["ProMode"] = "Update";
            Session["ProTransID"] = gvProduct.SelectedRow.Cells[1].Text.ToString();
            pnlProductDeatils.Visible = true;
            pnlSearch.Visible = false;
            DataSet ds = new DataSet();
            string quer = "select * from M_Product where [TransId] ='" + (string)Session["ProTransID"] + "'";
            ds = objCommonDL.GetDataSet(quer);
            if (ds.Tables["Table"].Rows.Count != 0)
            {
                DataRowView dr = ds.Tables["Table"].DefaultView[0];

                	txtProCode.Text = dr["ProductCode"].ToString();
                    txtProName.Text = dr["ProductDesc"].ToString();
                    txtRITC.Text= dr["RITCNo"].ToString();
                    //General
                    txtgenericdesc.Text= dr["GEN_DESC"].ToString();
                    txtaccessories.Text= dr["ACCESSORY"].ToString();
                    txtmanufacturer.Text= dr["MANUFACTURER"].ToString();
                    endcase.Text= dr["END_USE"].ToString();
                    brand.Text= dr["BRAND"].ToString();
                    txtmodel.Text= dr["MODEL"].ToString();
                    ddlcountryorigin.SelectedItem.Text= dr["CNTRY_ORIG"].ToString();
                    //Duty Calculation

                    txtCTH.Text= dr["CTHNo"].ToString();
                    txtCETNo.Text= dr["CETNo"].ToString();
                    //Basci Duty
                    txtBasicDutyRate.Text= dr["BASDuty"].ToString();
                    txtBasicDutySno.Text= dr["BASSNo"].ToString();
                    txtBasicDutyNotn.Text= dr["BASNotn"].ToString();
                    txtBasicDutyFlag.Text= dr["BASDFlag"].ToString();
                    txtBasicDutyAmount.Text= dr["BASAmt"].ToString();
                    txtBasicDutyUnit.Text= dr["BASUnit"].ToString();
                    //Addl Duty(Exsise Duty)-
                    txtAddlExNotn.Text= dr["AddlDuty_NOTN"].ToString();
                    txtAddlExSlNo.Text= dr["AddlDuty_SNO"].ToString();
                    txtAddlExRate.Text= dr["AddlDuty_RATE"].ToString();
                    //AddlDuty_NOTN", txtAddlExFlag.Text= dr[""].ToString();
                    //AddlDuty_NOTN", txtBasicDutyUnit.Text= dr[""].ToString();
                    //AddlDuty_NOTN", txtBasicDutyUnit.Text= dr[""].ToString();
                    //MRP Duty 
                    txtMRPSNo.Text= dr["MRPSNo"].ToString();
                    txtMRP.Text= dr["MRP"].ToString();
                    txtMRPUnit.Text= dr["MRPUnit"].ToString();
                    txtMRPAbatement.Text= dr["Abatement"].ToString();
                    //CVD(Sub section-5)-
                    txtExCVDNotn.Text= dr["CVDNotn"].ToString();
                    txtExCVDSlNo.Text= dr["CVDSNo"].ToString();
                    txtEXCVDRate.Text= dr["CVDAmt"].ToString();
                    txtpolicy.Text= dr["POLICYPARA"].ToString();
                    txtpyear.Text= dr["POLICY_YR"].ToString();
                    //Education Cess-
                    txtEducessNotn.Text= dr["EDU_CESS_NOTN"].ToString();
                    txtEduCessSNo.Text= dr["EDU_CESS_SNO"].ToString();
                    txtEducessRate.Text= dr["EDU_CESS_RATE"].ToString();
                    txtSHECessNotn.Text= dr["SHE_CESS_NOTN"].ToString();
                    txtSHECessSNo.Text= dr["SHE_CESS_SNO"].ToString();
                    txtSHECessRate.Text= dr["SHE_CESS_RATE"].ToString();

                    //Other Duty

                    //Educational Cess-
                    txtExEduCessRate.Text= dr["EDU_CESS_RATE_EXC"].ToString();
                    txtExSHECessRate.Text= dr["SHE_CESS_RATE_EXC"].ToString();

                    //Addl Duty of Excice(GSI).

                    //Spl.Excise Duty(sched-II)

                    //Addl Excise Duty(TTA)

                    //Health Cess
                    txtExHealthCessNotn.Text= dr["HLTH_Notn"].ToString();
                    txtExHealthCessSlNo.Text= dr["HLTH_SNo"].ToString();
                    txtExHealthCessRate.Text= dr["HLTH_Rate"].ToString();
                    txtExHealthCessFlag.Text= dr["HLTH_DFlag"].ToString();
                    txtExHealthCessAmount.Text= dr["HLTH_Amt"].ToString();
                    txtExHealthCessUnit.Text= dr["HLTH_Unit"].ToString();
                    //Cess & Notn
                    txtExCessNotn.Text= dr["CESS_NOTN"].ToString();
                    txtExCessSlNo.Text= dr["CESS_SNO"].ToString();
                    txtExCessRate.Text= dr["CESS_DUTY"].ToString();
                    txtExCessFlag.Text= dr["CESS_DFLAG"].ToString();
                    txtExCessAmount.Text= dr["CESS_AMT"].ToString();
                    txtExCessUnit.Text= dr["CESS_UNIT"].ToString();
                    //SAD Notn. & Duty
                    
                    //Addl Notn

                    //NCD
                    txtNCDNotn.Text= dr["NCD_Notn"].ToString();
                    txtNCDSNo.Text= dr["NCD_SNo"].ToString();
                    txtNCDRate.Text= dr["NCD_Rate"].ToString();
                    txtNCDFlag.Text= dr["NCD_DFlag"].ToString();
                    txtNCDAmount.Text= dr["NCD_Amt"].ToString();
                    txtNCDUnit.Text= dr["NCD_Unit"].ToString();
                    //Surcharge & Notn
                    txtSurNotn.Text= dr["SUR_NOTN"].ToString();
                    txtSurSno.Text= dr["SUR_SNO"].ToString();
                    txtSurRate.Text= dr["SURCHARGE"].ToString();
                    //SAPTA Notn
                    txtSAPTNotn.Text= dr["SAPTA_Notn"].ToString();
                    txtSAPTSno.Text= dr["SAPTA_SNo"].ToString();

            }
        }

        protected void rbPro_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (rbPro.SelectedItem.Text=="New Product")
            {
                pnlProductDeatils.Visible = true;
                pnlSearch.Visible = false;
            }
            else if (rbPro.SelectedItem.Text == "Search Product")
            {
                pnlSearch.Visible = true;
                pnlProductDeatils.Visible = false;
            }
        }  

    }
}