using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;
using VTS.ImpexCube.Business;
using VTS.ImpexCube.Data;

namespace ImpexCube
{
    public partial class ProductLoad : System.Web.UI.Page
    {
        string strcon = (string)ConfigurationManager.ConnectionStrings["Constr"].ConnectionString;
        VTS.ImpexCube.Business.ProductDetailsBL obj = new VTS.ImpexCube.Business.ProductDetailsBL();
        VTS.ImpexCube.Business.CommonBL obj1 = new VTS.ImpexCube.Business.CommonBL();
        CommonDL objCommonDL = new CommonDL();
        string FY = "";
        string keyname = "PRC";

        protected void Page_Load(object sender, EventArgs e)
        {
            Label pagename;
            pagename = (Label)Master.FindControl("lblName");
            pagename.Text = "Product Load For SEW";

            FY = (string)Session["FYear"];
            if (!IsPostBack)
            {
                BindJobNo();
            }
        }

        private void BindJobNo()
        {
            string Query = "Select Distinct JobNo From T_InvoiceDetails ";
            DataSet ds = GetDataMy(Query);
            if (ds.Tables["data"].Rows.Count != 0)
            {
                ddlJobNo.DataSource = ds;
                ddlJobNo.DataTextField = "JobNo";
                ddlJobNo.DataValueField = "JobNo";
                ddlJobNo.DataBind();
                ddlJobNo.Items.Insert(0, new ListItem("~Select~", "0"));
            }
            else
            {
                ddlJobNo.DataSource = null;
                ddlJobNo.DataBind();
            }
        }

        protected int GetCommandMy(string Query)
        {
            SqlConnection conn = new SqlConnection(strcon);
            conn.Open();
            SqlCommand cmd = new SqlCommand(Query, conn);
            cmd.CommandText = Query;
            cmd.Connection = conn;
            int res = cmd.ExecuteNonQuery();
            conn.Close();
            return res;
        }

        public DataSet GetDataMy(string Query)
        {
            DataSet ds = new DataSet();
            try
            {
                SqlConnection con = new SqlConnection(strcon);
                con.Open();
                SqlDataAdapter sd = new SqlDataAdapter(Query, con);
                sd.Fill(ds, "data");
                con.Close();
            }
            catch (Exception ex)
            {
                string Msg = ex.Message.ToString();
            }
            return ds;
        }

        protected void ddlJobNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetInvoiceDetails();
            GetInvoiceNo();
        }

        private void GetInvoiceDetails()
        {
            string query = "Select Distinct Invoicenumber From M_ExcelRead where JobNo = '" + ddlJobNo.SelectedValue + "'";
            DataSet ds = GetData(query);
            if (ds.Tables["data"].Rows.Count != 0)
            {
                gvInvoice.DataSource = ds;
                gvInvoice.DataBind();
            }
            else
            {
                gvInvoice.DataSource = null;
                gvInvoice.DataBind();
            }
        }

        private void GetInvoiceNo()
        {
            try
            {
                string Query = "Select Distinct InvoiceDetailsId,invoiceno  From T_invoiceDetails where JobNo = '" + ddlJobNo.SelectedValue + "' order by InvoiceDetailsId asc";
                DataSet ds = GetDataMy(Query);

                if (ds.Tables["data"].Rows.Count != 0)
                {
                    ddlInvoice.DataSource = ds;
                    ddlInvoice.DataTextField = "invoiceno";
                    ddlInvoice.DataValueField = "invoiceno";
                    ddlInvoice.DataBind();
                    ddlInvoice.Items.Insert(0, new ListItem("~Select~", "0"));
                }
                else
                {
                    ddlInvoice.DataSource = null;
                    ddlInvoice.DataBind();
                }
            }
            catch (Exception ex)
            {
                string Message = ex.Message;
            }
        }

        protected void ddlInvoice_SelectedIndexChanged(object sender, EventArgs e)
        {
            GetProductDetails();
            btnSave.Visible = true;
        }

        private void GetProductDetails()
        {
            string query = "Select [JobNo],[Invoicenumber],[InvoiceItem],[DeliveryNumber],[PurchaseOrderNumber],[PurchaseOrderItem],[Material],[Description]," +
                "[Quantity],[QuantityUnit],[NetValue],[TotalValue],[ValueUnit],[TotalWeight],[WeightUnit],[CommodityCode],[CountryOfOrigin],[Preference]," +
                "[MateriaDescription],[InvoiceSrNo],[ProductCode],[UnitPrice],[CommodityCode],[Addition],[SEWRitc],[POSrNo] From M_ExcelRead Where Invoicenumber = '" + ddlInvoice.SelectedItem.Text + "' and JobNo = '" + ddlJobNo.SelectedValue + "' ";
            DataSet ds = GetData(query);
            if (ds.Tables["data"].Rows.Count != 0)
            {
                gvProductDetails.DataSource = ds;
                gvProductDetails.DataBind();
                btnSave.Enabled = true;
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('No Records found for selected invoice');", true);
                gvProductDetails.DataSource = null;
                gvProductDetails.DataBind();
                btnSave.Enabled = false;
            }
        }

        public DataSet GetData(string Query)
        {
            DataSet ds = new DataSet();
            try
            {
                SqlConnection sqlConn = new SqlConnection(strcon);
                sqlConn.Open();
                SqlDataAdapter da = new SqlDataAdapter(Query, sqlConn);
                da.Fill(ds, "data");
                sqlConn.Close();
            }
            catch (Exception ex)
            {
                string Msg = ex.Message.ToString();
            }
            return ds;
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (gvProductDetails.Rows.Count != 0)
            {
                string Mode = string.Empty;
                try
                {
                    int i = 0;
                    string AutoQuery = "select Mode from T_JobCreation where JobNo ='" + ddlJobNo.SelectedValue + "'";
                    int auto = 0;
                    DataSet autods = objCommonDL.GetDataSet(AutoQuery);
                    if (autods.Tables["Table"].Rows.Count != 0)
                    {
                        DataRowView dr = autods.Tables["Table"].DefaultView[0];
                        Mode = dr["Mode"].ToString();
                    }
                    string DelQuery = "Delete From T_Product Where JobNo='" + ddlJobNo.SelectedValue + "' AND InvoiceNo='"+ddlInvoice.SelectedValue +"' ";
                    int result = GetCommand(DelQuery);
                    foreach (GridViewRow row in gvProductDetails.Rows)
                    {
                        string ProductCode = "";
                        ProductCode = row.Cells[15].Text;
                        string query = string.Empty;
                        string productno = row.Cells[16].Text;
                        string desc = row.Cells[3].Text;
                        string quantity = string.Empty;
                        string unit = string.Empty;
                        string unitprice = row.Cells[16].Text;
                        string Amount = string.Empty;
                        string weight = string.Empty;
                        string weightunit = string.Empty;
                        string RITC;
                        string POSrNo = row.Cells[18].Text;;
                        Amount = row.Cells[10].Text;
                        unitprice = row.Cells[16].Text;
                        TextBox txt = (TextBox)gvProductDetails.Rows[i].Cells[5].FindControl("txtRITCNO");
                        if (txt.Visible == false)
                        {
                            RITC = row.Cells[4].Text;
                        }
                        else
                        {
                            RITC = txt.Text;
                        }
                        int ProductSNo = Convert.ToInt32(row.Cells[2].Text);
                        string CountryofOrigin = row.Cells[14].Text;

                        //Assable Value Calculation
                        double invamt = 0;
                        double freight = 0;
                        double insu = 0;
                        double misc = 0;
                        double agency = 0;
                        double AddlChrg = 0;
                        double Exrate = 0;
                        string PoNo = row.Cells[7].Text;
                        string PoDate = "";

                        DataSet ds = obj1.GetInvoiceDetails(ddlJobNo.SelectedValue, ddlInvoice.SelectedValue);
                        if (ds.Tables["Table"].Rows.Count != 0)
                        {
                            DataRowView invrow = ds.Tables["Table"].DefaultView[0];
                            invamt = Convert.ToDouble(invrow["InvoiceProductINRValues"].ToString());
                            Exrate = Convert.ToDouble(invrow["InvoiceExchangeRates"].ToString());
                            freight = Convert.ToDouble(invrow["FreightINRAmount"].ToString());
                            insu = Convert.ToDouble(invrow["InsuranceINRAmount"].ToString());
                            misc = Convert.ToDouble(invrow["MisINRAmount"].ToString());
                            AddlChrg = Convert.ToDouble(invrow["HighSeaAmtINR"].ToString());
                            agency = Convert.ToDouble(invrow["AgencyINRAmount"].ToString());
                        }

                        double amt = Convert.ToDouble(Amount);
                        string RitcNo = RITC;
                        double ProdValue = amt * Exrate;
                        double airfright = ProdValue * 20 / 100;
                        double FreightAmount = (freight / invamt) * ProdValue;
                        double insuAmount = (insu / invamt) * ProdValue;
                        double miscAmount = (misc / invamt) * ProdValue;
                        double agencyAmount = (agency / invamt) * ProdValue;
                        double FreightAmount1 = FreightAmount / Exrate;

                        double insuAmount1 = insuAmount / Exrate;
                        double miscAmount1 = miscAmount / Exrate;
                        double agencyAmount1 = agencyAmount / Exrate;
                        double AddlChrgHS = (AddlChrg / invamt) * ProdValue;
                        double AddlChrgHS1 = AddlChrgHS / Exrate;
                        double totamt = 0;

                        if (Mode == "Air")
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
                        double AssValue = totamt + loadingcharge;

                        //Duty Calculation
                        double BCDRate = 0.00;
                        double CVDRate = 0.00;
                        double MRPRate = 0.00;

                        string AddlExNotn = "";
                        string AddlEXSNo = "";

                        string ExCVDNotn = "";
                        string ExCVDSlNo = "";
                        double EXCVDRate = 4;

                        DataSet ds1 = obj.GetBCDRTA(RITC);
                        if (ds1.Tables["Table"].Rows.Count != 0)
                        {
                            DataRowView row1 = ds1.Tables["Table"].DefaultView[0];
                            BCDRate = Convert.ToDouble(row1["RTA"].ToString());
                        }

                        DataSet ds2 = obj.GetCVDRTA(RITC);
                        if (ds2.Tables["Table"].Rows.Count != 0)
                        {
                            DataRowView row2 = ds2.Tables["Table"].DefaultView[0];
                            CVDRate = Convert.ToDouble(row2["CVDRTA"].ToString());
                        }

                        //MRP
                        DataSet ds3 = obj.GetMRPRTA(RITC);
                        if (ds3.Tables["Table"].Rows.Count != 0)
                        {
                            DataRowView row3 = ds3.Tables["Table"].DefaultView[0];
                            MRPRate = Convert.ToDouble(row3["ABETRTA"].ToString());
                        }

                        string chapter = RITC.Substring(0, 2);
                        DataSet ds4 = obj.GetCVDUserNoti(chapter);
                        if (ds4.Tables["Table"].Rows.Count != 0)
                        {
                            DataRowView row4 = ds4.Tables["Table"].DefaultView[0];
                            AddlExNotn = row4["Notification"].ToString();
                            AddlEXSNo = row4["SerialNo"].ToString();
                            CVDRate = Convert.ToDouble(row4["Duty"].ToString());
                        }
                        DataSet ds5 = obj.GetSADUserNoti(chapter);
                        if (ds5.Tables["Table"].Rows.Count != 0)
                        {
                            DataRowView row5 = ds5.Tables["Table"].DefaultView[0];
                            ExCVDNotn = row5["Notification"].ToString();
                            ExCVDSlNo = row5["SerialNo"].ToString();
                            EXCVDRate = Convert.ToDouble(row5["Duty"].ToString());
                        }

                        //Duty Calculation

                        string CTHNo = RITC;
                        string RateType = "S";
                        string BasicDutyFlag = "Plus";
                        double BasicDutyRate = BCDRate;
                        string BasicDutyUnit = "";
                        double BasicDutyAmount = 0;


                        double AddlExRate = CVDRate;
                        string AddlExFlag = "Plus";
                        double AddlExAmt = 0;
                        string AddlExUnit = "";
                        string MRPDuty = "";
                        string MRPSNo = "";
                        string MRPUnit = "";
                        double MRPAbatement = Convert.ToDouble(MRPRate);

                        MRPDuty = "0";
                        MRPSNo = "";
                        MRPUnit = "";
                        MRPAbatement = Convert.ToDouble(MRPRate);


                        string PolicyPara = "1";
                        string PolicyYear = "1";
                        double EduCessRate = 2;
                        double SHECessRate = 1;

                        //Calcualtoin Based On Conversion PCS TO KGS <---> ViceVersa
                        RadioButtonList measuretype = row.FindControl("rbMeasurementType") as RadioButtonList;

                        string Type = "DUTIABLE";
                        double qty = 0;

                        if (measuretype.SelectedValue == "Quantity")
                        {
                            quantity = row.Cells[8].Text;
                            unit = row.Cells[9].Text;
                            weight = row.Cells[12].Text;
                            weightunit = row.Cells[13].Text;
                            qty = Convert.ToDouble(quantity);
                            desc = row.Cells[3].Text + row.Cells[17].Text;
                        }
                        else
                        {
                            quantity = row.Cells[12].Text;
                            unit = row.Cells[13].Text;
                            weight = row.Cells[8].Text;
                            weightunit = row.Cells[9].Text;
                            qty = Convert.ToDouble(weight);
                            double test = Convert.ToDouble(row.Cells[10].Text) / Convert.ToDouble(row.Cells[12].Text);
                            unitprice = Convert.ToString(test);
                            desc = row.Cells[3].Text + "(" + row.Cells[8].Text + " " + row.Cells[9].Text + " @ " + row.Cells[11].Text + " " + row.Cells[16].Text + "/" + row.Cells[9].Text + ")" + row.Cells[17].Text;
                        }

                        double BCDTax = BasicDutyRate;
                        double BCD = (AssValue * BCDTax) / 100;

                        double BasDutyAmtQty = (qty * BasicDutyAmount);
                        double TotBasicDutyAmt = BCD + BasDutyAmtQty;

                        double CVDTax = AddlExRate;
                        double CVD = (AssValue + BCD) * CVDTax / 100;
                        double CVDDutyAmtQty = (qty * AddlExAmt);
                        double TotalCVDAmt = CVD + CVDDutyAmtQty;

                        //EduCessRate,SHECessRate
                        double ExEduCessAmount = (CVD) * EduCessRate / 100;
                        double ExSHECessAmount = (CVD) * SHECessRate / 100;

                        double EduCessAmount = (BCD * EduCessRate) / 100;
                        double SHECessAmount = (BCD * SHECessRate) / 100;

                        double SADAmt = ((AssValue + BCD + CVD + ExEduCessAmount + ExSHECessAmount + EduCessAmount + SHECessAmount) * EXCVDRate) / 100;
                        double TotalDutyAmt = TotBasicDutyAmt + TotalCVDAmt + ExEduCessAmount + ExSHECessAmount + EduCessAmount + SHECessAmount + SADAmt;// +CESSDutyAmt;
                        string ExEduCessNotn = "013/2012";
                        string ExEduCessSlNo = "1";
                        string CreatedBy = (string)Session["USER-NAME"];
                        string CreatedDate = Convert.ToString(DateTime.Now);
                        string ModifiedBy = (string)Session["USER-NAME"];
                        string ModifiedDate = Convert.ToString(DateTime.Now);

                        //***********************************************************************

                        // Inert Product
                        int Result = 0;
                        StringBuilder Query = new StringBuilder();
                        SqlConnection con = new SqlConnection(strcon);
                        con.Open();
                        Query.Append("INSERT INTO [T_Product] (JobNo, InvSrNo, InvoiceNo, ProductFamily, ProductCode, ProductSNo, ProductDesc,GenericDesc, POSrNo,PONo, PODate, ProType, Qty, Unit, UnitPrice, Amount, AQty1, AQty1Unit, AQty2,");
                        Query.Append("AQty2Unit, MasterProduct, PolicyPara, PolicyYear, CTHNo, RITCNo, RateType, BasicDutyRate, BasicDutyFlag, BasicDutyAmount, BasicDutyUnit, EduCessRate, ");
                        Query.Append("EduCessAmount, SHECessRate, SHECessAmount, CETNo, MRPDuty, MRPSNo, MRP, MRPUnit, MRPAbatement, AddlExNotn,AddlEXSLNo,AddlExRate, AddlExFlag, AddlExAmount, AddlExUnit,ExEduCessNotn,ExEduCessSlNo, ");
                        Query.Append("ExEduCessAmount, ExSHECessAmount, ExCVDNotn, ExCVDSlNo, EXCVDRate, CountryofOrigin, ProdAmt, ProdAmtRs, Freight, Insurance, AgencyCharge, ");
                        Query.Append("Miscellaneous, LandingChrg, AssableValue, TotalValue, SVBLdg, BasDutyAmtPer, BasDutyAmtQty, ");
                        Query.Append("TotBasicDutyAmt, CVDDutyAmtPer, CVDDutyAmtQty, TotalCVDAmt, SADAmt, TotalDutyAmt, CreatedBy, CreatedDate, ModifiedBy, ModifiedDate)");

                        Query.Append("values(@JobNo,@InvSrNo,@InvoiceNo,@ProductFamily,@ProductCode,@ProductSNo,@ProductDesc,@GenericDesc,@POSrNo,@PONo,@PODate,@ProType,@Qty,@Unit,@UnitPrice,@Amount,@AQty1,@AQty1Unit,@AQty2,");
                        Query.Append("@AQty2Unit,@MasterProduct,@PolicyPara,@PolicyYear,@CTHNo,@RITCNo,@RateType,@BasicDutyRate,@BasicDutyFlag,@BasicDutyAmount,@BasicDutyUnit,@EduCessRate, ");
                        Query.Append("@EduCessAmount,@SHECessRate,@SHECessAmount,@CETNo,@MRPDuty,@MRPSNo,@MRP,@MRPUnit,@MRPAbatement,@AddlExNotn,@AddlEXSLNo,@AddlExRate, @AddlExFlag, @AddlExAmount, @AddlExUnit,@ExEduCessNotn,@ExEduCessSlNo, ");
                        Query.Append("@ExEduCessAmount,@ExSHECessAmount,@ExCVDNotn,@ExCVDSlNo,@EXCVDRate,@CountryofOrigin,@ProdAmt,@ProdAmtRs,@Freight,@Insurance,@AgencyCharge, ");
                        Query.Append("@Miscellaneous,@LandingChrg,@AssableValue,@TotalValue,@SVBLdg,@BasDutyAmtPer,@BasDutyAmtQty, ");
                        Query.Append("@TotBasicDutyAmt,@CVDDutyAmtPer,@CVDDutyAmtQty,@TotalCVDAmt,@SADAmt,@TotalDutyAmt,@CreatedBy,@CreatedDate,@ModifiedBy,@ModifiedDate)");
                        SqlCommand cmd = new SqlCommand(Query.ToString(), con);

                        cmd.Parameters.AddWithValue("@JobNo", ddlJobNo.SelectedValue);
                        cmd.Parameters.AddWithValue("@InvSrNo", ProductSNo);
                        cmd.Parameters.AddWithValue("@InvoiceNo", ddlInvoice.SelectedValue);
                        cmd.Parameters.AddWithValue("@ProductFamily", "");
                        cmd.Parameters.AddWithValue("@ProductCode", ProductCode);
                        cmd.Parameters.AddWithValue("@ProductSNo", ProductSNo);
                        cmd.Parameters.AddWithValue("@ProductDesc", desc);
                        //if (desc.Length <= 10)
                        //{
                        //    cmd.Parameters.AddWithValue("@GenericDesc", desc.Substring(0, 58));
                        //}
                        //else
                        //{
                            cmd.Parameters.AddWithValue("@GenericDesc", desc.Substring(0,6));
                      //  }
                            cmd.Parameters.AddWithValue("@POSrNo", POSrNo);
                        cmd.Parameters.AddWithValue("@PONo", PoNo);
                        cmd.Parameters.AddWithValue("@PODate", PoDate);
                        cmd.Parameters.AddWithValue("@ProType", Type);

                        cmd.Parameters.AddWithValue("@Qty", quantity);
                        cmd.Parameters.AddWithValue("@Unit", unit);
                        cmd.Parameters.AddWithValue("@UnitPrice", unitprice);
                        cmd.Parameters.AddWithValue("@Amount", Amount);
                        cmd.Parameters.AddWithValue("@AQty1", weight);
                        cmd.Parameters.AddWithValue("@AQty1Unit", weightunit);
                        cmd.Parameters.AddWithValue("@AQty2", 0);
                        cmd.Parameters.AddWithValue("@AQty2Unit", "");
                        cmd.Parameters.AddWithValue("@MasterProduct", "");
                        cmd.Parameters.AddWithValue("@PolicyPara", PolicyPara);

                        cmd.Parameters.AddWithValue("@PolicyYear", PolicyYear);
                        cmd.Parameters.AddWithValue("@CTHNo", CTHNo);
                        cmd.Parameters.AddWithValue("@RITCNo", RITC);
                        cmd.Parameters.AddWithValue("@RateType", RateType);
                        cmd.Parameters.AddWithValue("@BasicDutyRate", BasicDutyRate);
                        cmd.Parameters.AddWithValue("@BasicDutyFlag", BasicDutyFlag);
                        cmd.Parameters.AddWithValue("@BasicDutyAmount", BasicDutyAmount);
                        cmd.Parameters.AddWithValue("@BasicDutyUnit", BasicDutyUnit);
                        cmd.Parameters.AddWithValue("@EduCessRate", EduCessRate);
                        cmd.Parameters.AddWithValue("@EduCessAmount", EduCessAmount);

                        cmd.Parameters.AddWithValue("@SHECessRate", SHECessRate);
                        cmd.Parameters.AddWithValue("@SHECessAmount", SHECessAmount);
                        cmd.Parameters.AddWithValue("@CETNo", RITC);
                        cmd.Parameters.AddWithValue("@MRPDuty", MRPDuty);
                        cmd.Parameters.AddWithValue("@MRPSNo", MRPSNo);
                        cmd.Parameters.AddWithValue("@MRP", 0);
                        cmd.Parameters.AddWithValue("@MRPUnit", MRPUnit);
                        cmd.Parameters.AddWithValue("@MRPAbatement", MRPAbatement);

                        cmd.Parameters.AddWithValue("@AddlExNotn", AddlExNotn);
                        cmd.Parameters.AddWithValue("@AddlEXSLNo", AddlEXSNo);

                        cmd.Parameters.AddWithValue("@AddlExRate", AddlExRate);
                        cmd.Parameters.AddWithValue("@AddlExFlag", AddlExFlag);

                        cmd.Parameters.AddWithValue("@AddlExAmount", AddlExAmt);
                        cmd.Parameters.AddWithValue("@AddlExUnit", AddlExUnit);


                        cmd.Parameters.AddWithValue("@ExEduCessNotn", ExEduCessNotn);
                        cmd.Parameters.AddWithValue("@ExEduCessSlNo", ExEduCessSlNo);
                        cmd.Parameters.AddWithValue("@ExEduCessAmount", ExEduCessAmount);
                        cmd.Parameters.AddWithValue("@ExSHECessAmount", ExSHECessAmount);
                        cmd.Parameters.AddWithValue("@ExCVDNotn", ExCVDNotn);
                        cmd.Parameters.AddWithValue("@ExCVDSlNo", ExCVDSlNo);
                        cmd.Parameters.AddWithValue("@EXCVDRate", EXCVDRate);
                        cmd.Parameters.AddWithValue("@CountryofOrigin", CountryofOrigin);
                        cmd.Parameters.AddWithValue("@ProdAmt", 0);
                        cmd.Parameters.AddWithValue("@ProdAmtRs", ProdValue);
                        cmd.Parameters.AddWithValue("@Freight", FreightAmount1);

                        cmd.Parameters.AddWithValue("@Insurance", insuAmount1);
                        cmd.Parameters.AddWithValue("@AgencyCharge", agencyAmount1);
                        cmd.Parameters.AddWithValue("@Miscellaneous", miscAmount1);
                        cmd.Parameters.AddWithValue("@LandingChrg", loadingcharge);
                        cmd.Parameters.AddWithValue("@AssableValue", AssValue);
                        cmd.Parameters.AddWithValue("@TotalValue", 0);
                        cmd.Parameters.AddWithValue("@SVBLdg", 0);
                        cmd.Parameters.AddWithValue("@BasDutyAmtPer", BCD);
                        cmd.Parameters.AddWithValue("@BasDutyAmtQty", BasDutyAmtQty);
                        cmd.Parameters.AddWithValue("@TotBasicDutyAmt", TotBasicDutyAmt);

                        cmd.Parameters.AddWithValue("@CVDDutyAmtPer", CVD);
                        cmd.Parameters.AddWithValue("@CVDDutyAmtQty", CVDDutyAmtQty);
                        cmd.Parameters.AddWithValue("@TotalCVDAmt", TotalCVDAmt);
                        cmd.Parameters.AddWithValue("@SADAmt", SADAmt);
                        cmd.Parameters.AddWithValue("@TotalDutyAmt", TotalDutyAmt);
                        cmd.Parameters.AddWithValue("@CreatedBy", CreatedBy);
                        cmd.Parameters.AddWithValue("@CreatedDate", CreatedDate);
                        cmd.Parameters.AddWithValue("@ModifiedBy", ModifiedBy);
                        cmd.Parameters.AddWithValue("@ModifiedDate", ModifiedDate);
                        Result = cmd.ExecuteNonQuery();
                        con.Close();
                        i++;
                    }
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "redirect", "alert('Saved Successfully'); window.location.href='ProductLoad.aspx';", true);
                }
                catch (Exception ex)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert(' " + ex.Message + " ');", true);
                }
            }
        }

        protected void gvProductDetails_RowCreated(object sender, GridViewRowEventArgs e)
        {
            GridViewRow row = e.Row;
            // Intitialize TableCell list
            List<TableCell> columns = new List<TableCell>();
            foreach (DataControlField column in gvProductDetails.Columns)
            {
                //Get the first Cell /Column
                TableCell cell = row.Cells[0];
                // Then Remove it after
                row.Cells.Remove(cell);
                //And Add it to the List Collections
                columns.Add(cell);
            }
            // Add cells
            row.Cells.AddRange(columns.ToArray());
        }

        protected void gvProductDetails_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            DataRowView drview = e.Row.DataItem as DataRowView;

            if (e.Row.RowType == DataControlRowType.Header)
            {
                //Header
            }
            else
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    int RITCNO = Convert.ToInt32(DataBinder.Eval(e.Row.DataItem, "CommodityCode"));
                    string SEWRitc = Convert.ToString(DataBinder.Eval(e.Row.DataItem, "SEWRitc"));
                    string query = "SELECT  [RITCNo]  FROM [RITCCode] where RITCNo= '" + SEWRitc + "'";
                    DataSet ds = GetData(query);
                    if (ds.Tables["data"].Rows.Count == 0)
                    {
                        TextBox txtRITCNO = (TextBox)e.Row.FindControl("txtRITCNO");
                        txtRITCNO.BorderColor = Color.Red;
                        txtRITCNO.Text = SEWRitc;
                    }
                    else
                    {
                        TextBox txtRITCNO = (TextBox)e.Row.FindControl("txtRITCNO");
                        txtRITCNO.Text = SEWRitc;
                       // txtRITCNO.Visible = false;
                    }
                }
            }
        }

        public int GetCommand(string Query)
        {
            SqlConnection con = new SqlConnection(strcon);
            con.Open();
            SqlDataAdapter da = new SqlDataAdapter();
            SqlCommand cmd = new SqlCommand(Query, con);
            cmd.CommandText = Query;
            cmd.Connection = con;
            da.SelectCommand = cmd;
            int result = cmd.ExecuteNonQuery();
            con.Close();
            return result;
        }
    }
}