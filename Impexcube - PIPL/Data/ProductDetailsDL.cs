// -----------------------------------------------------------------------
// <copyright file="ProductDetailsDL.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace VTS.ImpexCube.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Data;
    using System.Data.SqlClient;
    using System.Configuration;
    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class ProductDetailsDL
    {
        string strconn = (string)ConfigurationManager.ConnectionStrings["Constr"].ConnectionString;

        // Insert   Update    And   Load the product
        public int InsertProductDetails(string JobNo, string InvoiceNo, string ProductFamily, string ProductCode, string ProductDesc, string ProType, double Qty, string Unit, double UnitPrice, double Amount, string RitcNo, double ProdValue, double FreightAmount, double insuAmount, double miscAmount, double agencyAmount, double loadingcharge, double assvalue, string CreatedBy, string CreatedDate, int slno)
        {
            StringBuilder Query = new StringBuilder();
            int result = new int();
            object obj = new object();
            SqlConnection con = new SqlConnection(strconn);//Freight,Insurance,AgencyCharge,Miscellaneous,LandingChrg,AssableValue
            con.Open();
            Query.Append("Insert Into T_Product(JobNo,InvoiceNo,ProductFamily,ProductCode,ProductDesc,ProType,Qty,Unit,UnitPrice,Amount,RITCNo,ProdAmtRs,Freight,Insurance,Miscellaneous,AgencyCharge,LandingChrg,AssableValue,CreatedBy,CreatedDate,ProductSNo)");
           // Query.Append("Values('" + JobNo + "','" + InvoiceNo + "','" + ProductFamily + "','" + ProductCode + "','" + ProductDesc + "','" + ProType + "','" + Qty + "','" + Unit + "','" + UnitPrice + "','" + Amount + "','" + RitcNo + "','" + ProdValue + "','" + FreightAmount + "','" + insuAmount + "','" + miscAmount + "','" + agencyAmount + "','" + loadingcharge + "','" + assvalue + "','" + CreatedBy + "','" + CreatedDate + "') SELECT SCOPE_IDENTITY()");
            Query.Append("Values(@JobNo,@InvoiceNo,@ProductFamily,@ProductCode,@ProductDesc,@ProType,@Qty,@Unit,@UnitPrice,@Amount,@RITCNo,@ProdAmtRs,@Freight,@Insurance,@Miscellaneous,@AgencyCharge,@LandingChrg,@AssableValue,@CreatedBy,@CreatedDate,@slno) SELECT SCOPE_IDENTITY()");
           
            //@JobNo,@InvoiceNo,@ProductFamily,@ProductCode,@ProductDesc,@ProType,@Qty,@Unit,@UnitPrice,@Amount,@RITCNo,@ProdAmtRs,@Freight,@Insurance,@Miscellaneous,@AgencyCharge,@LandingChrg,@AssableValue,@CreatedBy,@CreatedDate
            SqlCommand cmd = new SqlCommand(Query.ToString(), con);
            cmd.Parameters.AddWithValue("@JobNo",JobNo);
                cmd.Parameters.AddWithValue("@InvoiceNo",InvoiceNo);
                cmd.Parameters.AddWithValue("@ProductFamily",ProductFamily);
                cmd.Parameters.AddWithValue("@ProductCode",ProductCode);
                cmd.Parameters.AddWithValue("@ProductDesc",ProductDesc);
                cmd.Parameters.AddWithValue("@ProType",ProType);
                cmd.Parameters.AddWithValue("@Qty",Qty);
                cmd.Parameters.AddWithValue("@Unit",Unit);
                cmd.Parameters.AddWithValue("@UnitPrice",UnitPrice);
                cmd.Parameters.AddWithValue("@Amount",Amount);
                cmd.Parameters.AddWithValue("@RITCNo",RitcNo);
                cmd.Parameters.AddWithValue("@ProdAmtRs",ProdValue);
                cmd.Parameters.AddWithValue("@Freight",FreightAmount);
                cmd.Parameters.AddWithValue("@Insurance",insuAmount);
               cmd.Parameters.AddWithValue("@Miscellaneous",miscAmount);
                cmd.Parameters.AddWithValue("@AgencyCharge",agencyAmount);
                cmd.Parameters.AddWithValue("@LandingChrg",loadingcharge);
                cmd.Parameters.AddWithValue("@AssableValue",assvalue);
                cmd.Parameters.AddWithValue("@CreatedBy",CreatedBy);
                cmd.Parameters.AddWithValue("@CreatedDate", CreatedDate);
                cmd.Parameters.AddWithValue("@slno", slno);
           // result = cmd.ExecuteNonQuery();
            obj = cmd.ExecuteScalar();
            result = Convert.ToInt16(obj);
            return result;
        }
        public int UpdateProductDetails(string ProductID, string ProductFamily, string ProductCode, string ProductDesc, string ProType, double Qty, string Unit, double UnitPrice, double Amount, string RitcNo, double ProdValue, double FreightAmount, double insuAmount, double miscAmount, double agencyAmount, double loadingcharge, double assvalue, int slno)
        {
            StringBuilder Query = new StringBuilder();
            int result = new int();
           // object obj = new object();
            SqlConnection con = new SqlConnection(strconn);
            Query.Append("Update T_Product Set ProductFamily=@ProductFamily, ProductCode=@ProductCode,ProductDesc=@ProductDesc,ProType=@ProType,Qty=@Qty,Unit=@Unit,UnitPrice=@UnitPrice,Amount=@Amount,RITCNo=@RITCNo,ProdAmtRs=@ProdAmtRs,Freight=@Freight,Insurance=@Insurance,Miscellaneous=@Miscellaneous,AgencyCharge=@AgencyCharge,LandingChrg=@LandingChrg,AssableValue=@AssableValue,ProductSNo=@slno where ProductID='" + ProductID + "'");
            con.Open(); 
            SqlCommand cmd = new SqlCommand(Query.ToString(), con);
            cmd.Parameters.AddWithValue("@ProductFamily", ProductFamily);
            cmd.Parameters.AddWithValue("@ProductCode", ProductCode);
            cmd.Parameters.AddWithValue("@ProductDesc", ProductDesc);
            cmd.Parameters.AddWithValue("@ProType", ProType);
            cmd.Parameters.AddWithValue("@Qty", Qty);
            cmd.Parameters.AddWithValue("@Unit", Unit);
            cmd.Parameters.AddWithValue("@UnitPrice", UnitPrice);
            cmd.Parameters.AddWithValue("@Amount", Amount);
            cmd.Parameters.AddWithValue("@RITCNo", RitcNo);
            cmd.Parameters.AddWithValue("@ProdAmtRs", ProdValue);
            cmd.Parameters.AddWithValue("@Freight", FreightAmount);
            cmd.Parameters.AddWithValue("@Insurance", insuAmount);
            cmd.Parameters.AddWithValue("@Miscellaneous", miscAmount);
            cmd.Parameters.AddWithValue("@AgencyCharge", agencyAmount);
            cmd.Parameters.AddWithValue("@LandingChrg", loadingcharge);
            cmd.Parameters.AddWithValue("@AssableValue", assvalue);
            cmd.Parameters.AddWithValue("@slno", slno);
            result = cmd.ExecuteNonQuery();
            //obj = cmd.ExecuteScalar();
           // result = Convert.ToInt16(obj);
            return result;
        }
        public DataSet loadproductgrid(string InvNo,string JobNo)
        {
            DataSet ds = new DataSet();
            string qry = "Select ProductID,ProductDesc,ProType,Qty,Unit,UnitPrice,Amount,ProductCode,ProductFamily,ProductSNo from T_Product where InvoiceNo='" + InvNo + "' And JobNo='" + JobNo + "' Order By ProductSNo ASC";
            SqlConnection con = new SqlConnection(strconn);
            con.Open();
            SqlDataAdapter da=new SqlDataAdapter(qry,con);
            da.Fill(ds, "Table");
            con.Close();
            return ds;
        }

        //Update And Read the Main page of duty

        //public int UpdateMain(string ITCLocation, string ITCHSCode, string PolicyPara, string PolicyYear, double Loading, string LoadTerms, double LoadRate, double LoadAmount, string Jobno, string InvoiceNo, string ProductDesc)
        //{
        //    string Query = "Update T_Product set ITCLocation='" + ITCLocation + "'," +
        //        " ITCHSCode='" + ITCHSCode + "', PolicyPara='" + PolicyPara + "', PolicyYear='" + PolicyYear + "',Loading='" + Loading + "', LodingOn='" + LoadTerms + "', LoadingRate='" + LoadRate + "', LoadingAmount='" + LoadAmount + "' where JobNo='" + Jobno + "' and InvoiceNo='" + InvoiceNo + "' And ProductDesc='" + ProductDesc + "'";
        //    int result = new int();
        //    SqlConnection con = new SqlConnection(strconn);
        //    con.Open();
        //    SqlCommand cmd = new SqlCommand(Query, con);
        //    result = cmd.ExecuteNonQuery();
        //    return result;
        //}

        //public DataSet ReadMain(int ProductID)
        //{
        //    DataSet ds = new DataSet();
        //    string qry = "Select ITCLocation,ITCHSCode,PolicyPara,PolicyYear,RITCNo,AssableValue from T_Product where ProductID='" + ProductID + "'";
        //    SqlConnection con = new SqlConnection(strconn);
        //    con.Open();
        //    SqlDataAdapter da = new SqlDataAdapter(qry, con);
        //    da.Fill(ds, "Table");
        //    con.Close();
        //    return ds;
        //}

        //Read the Duty data in Sql Table
        public DataSet ReadDuty(int ProductID)
        {
            DataSet ds = new DataSet();
            string qry = "Select  * from T_Product where ProductID='" + ProductID + "'";
            //string qry = "Select  EximSchCode, EximSchDesc, SchNoten, SchNotenUnit, SchNotenDesc, CTHNo, RITCNo, RateType, BasicDutyNotn, BasicDutySno, BasicDuty, BasicDutyRate," +
            //          " BasicDutyFlag, BasicDutyAmount, BasicDutyUnit, AddlNotn, AddlSNo, EduCessNotn, EduCessSNo, EduCessRate, SHECess, SHECessSno, SHECessRate, NCDNotn," +
            //          " NCDSno, NCDRate, NCDDFlag, NCDAmount, NCDUnit, NCDRule, SurNotn, SurSno, SurRate, SAPTANotn, SAPTASNo, SAPTADesc, TariffValNotn, TariffValSNo," +
            //          " TariffUnitQty, TariffUnit, TariffRate, TariffAmount from T_Product where ProductID='" + ProductID + "'";
            SqlConnection con = new SqlConnection(strconn);
            con.Open();
            SqlDataAdapter da = new SqlDataAdapter(qry, con);
            da.Fill(ds, "Table");
            con.Close();
            return ds;
        }
        //Read the Other Duty data in Sql Table
        //public DataSet ReadOtherDuty(int ProductID)
        //{
        //    DataSet ds = new DataSet();
        //    string qry = "Select   CETNo, MRPDuty, MRPSNo, MRP, MRPUnit, MRPAbatement, AddlExNotn, AddlExSlNo, AddlExRate, AddlExFlag, AddlExAmount, AddlExUnit,"+
        //              " ExEduCessRate, ExSHECessRate, ExCVDNotn, ExCVDSlNo, EXCVDRate, ExGSIAddlDutyNotn, ExGSIAddlDutySlNo, ExGSIAddlDutyRate, ExGSIAddlDutyFlag, "+
        //              " ExGSIAddlDutyAmount, ExGSIAddlDutyUnit, ExSPLExDutyNotn, ExSPLExDutySlNo, ExSPLExDutyRate, ExSPLExDutyFlag, ExSPLExDutyAmount, ExSPLExDutyUnit, "+
        //              " ExTTAAddlDutyNotn, ExTTAAddlDutySlNo, ExTTAAddlDutyRate, ExTTAAddlDutyFlag, ExTTAAddlDutyAmount, ExTTAAddlDutyUnit, ExHealthCessNotn, "+
        //              " ExHealthCessSlNo, ExHealthCessRate, ExHealthCessFlag, ExHealthCessAmount, ExHealthCessUnit, ExCessNotn, ExCessSlNo, ExCessRate, ExCessFlag, "+
        //              " ExCessAmount, ExCessUnit, ExSADNotn, ExSADSlno, ExSADRate,BasDutyAmtPer from T_Product where ProductID='" + ProductID + "'";
        //    SqlConnection con = new SqlConnection(strconn);
        //    con.Open();
        //    SqlDataAdapter da = new SqlDataAdapter(qry, con);
        //    da.Fill(ds, "Table");
        //    con.Close();
        //    return ds;
        //}


        //Read Generic Desc
        //public DataSet ReadGenericDesc(int ProductID)
        //{
        //    DataSet ds = new DataSet();
        //    string qry = "Select GenericDesc,Manufacturer,Brand,Model,Accessories,EndUse,CountryofOrigin  " +
        //              " from T_Product where ProductID='" + ProductID + "'";
        //    SqlConnection con = new SqlConnection(strconn);
        //    con.Open();
        //    SqlDataAdapter da = new SqlDataAdapter(qry, con);
        //    da.Fill(ds, "Table");
        //    con.Close();
        //    return ds;
        //}



        //update Generic Desc
        public int UpdateGenericDesc(string GenericDesc, string Manufacturer, string Brand, string Model, string Accessories, string EndUse, string CountryofOrigin, string Jobno, string InvoiceNo, string ProductDesc, string ProductID, double AQty1, string AQty1Unit)
        {
            string Query = "Update T_Product set GenericDesc='"+GenericDesc+"', Manufacturer='"+Manufacturer+"', Brand='"+Brand+"', Model='"+Model+"', "+
                           " Accessories='" + Accessories + "', EndUse='" + EndUse + "', CountryofOrigin='" + CountryofOrigin + "',AQty1='" + AQty1 + "',AQty1Unit='" + AQty1Unit + "'  where ProductID='" + ProductID + "'";
            int result = new int();
            SqlConnection con = new SqlConnection(strconn);
            con.Open();
            SqlCommand cmd = new SqlCommand(Query, con);
            result = cmd.ExecuteNonQuery();
            return result;
        }

       
        //update Duty

        public int UpdateDuty(string EximSchCode, string EximSchDesc, string SchNoten, string SchNotenUnit, string SchNotenDesc, string CTHNo, string CETNo,
          string RateType, string BasicDutyNotn, string BasicDutySno, string BasicDutyFlag, double BasicDutyRate, string BasicDutyUnit,
          string AddlExNotn, string AddlExSlNo, double AddlExRate, string AddlExFlag, double AddlExAmount, string AddlExUnit,
          string MRPDuty, string MRPSNo, double MRP, string MRPUnit, double MRPAbatement, string ExCVDNotn, string ExCVDSlNo, double EXCVDRate, string PolicyPara, string PolicyYear,
          string EduCessNotn, string EduCessSNo, double EduCessRate, string SHECess, string SHECessSno, double SHECessRate,
          string Jobno, string InvoiceNo, string ProductDesc, double bcd, double EduCessAmount, double SHECessAmount, double CVD, double SADAmt, double ExEduCessAmount, 
            double ExSHECessAmount, string ITCLocation, string ITCHSCode, string ProductID, string SAPTANotn, string SAPTASNo, string SAPTADesc, string PoNo, string PoDate, 
            double CVDDutyAmtQty, double BasDutyAmtQty, double BasicDutyAmount, double TotBasicDutyAmt, double TotalCVDAmt,
            string ExCessNotn, string ExCessSlNo, double ExCessRate, string ExCessFlag, double ExCessAmount, string ExCessUnit, double TotalDutyAmt, string ExEduCessNotn, string ExEduCessSNo, double ExEduCessRate, double ExSHECessRate, double CESSDutyAmt)
        {
            string Query = "Update T_Product set EximSchCode='"+EximSchCode+"', EximSchDesc='"+EximSchDesc+"', SchNoten='"+SchNoten+"', SchNotenUnit='"+SchNotenUnit+"', SchNotenDesc='"+SchNotenDesc+"', "+
                " CTHNo='" + CTHNo + "', CETNo='" + CETNo + "', RateType='" + RateType + "', BasicDutyNotn='" + BasicDutyNotn + "', BasicDutySno='" + BasicDutySno + "', BasicDutyFlag='" + BasicDutyFlag + "', BasicDutyRate='" + BasicDutyRate + "'," +
                " BasicDutyUnit='" + BasicDutyUnit + "', AddlExNotn='" + AddlExNotn + "',AddlExSlNo='" + AddlExSlNo + "', AddlExRate='" + AddlExRate + "', AddlExFlag='" + AddlExFlag + "', AddlExAmount='" + AddlExAmount + "', AddlExUnit='" + AddlExUnit + "',   " +
                " MRPDuty='" + MRPDuty + "', MRPSNo='" + MRPSNo + "', MRP='" + MRP + "', MRPUnit='" + MRPUnit + "', MRPAbatement='" + MRPAbatement + "', "+
                " ExCVDNotn='" + ExCVDNotn + "', ExCVDSlNo='" + ExCVDSlNo + "', EXCVDRate='" + EXCVDRate + "',PolicyPara='" + PolicyPara + "',PolicyYear='" + PolicyYear + "', " +
                " EduCessNotn='" + EduCessNotn + "', EduCessSNo='" + EduCessSNo + "', EduCessRate='" + EduCessRate + "',EduCessAmount='" + EduCessAmount + "' ,SHECess='" + SHECess + "', SHECessSno='" + SHECessSno + "', SHECessRate='" + SHECessRate + "',SHECessAmount='" + SHECessAmount + "', " +
                " BasDutyAmtPer='" + bcd + "',CVDDutyAmtPer='" + CVD + "',SADAmt='" + SADAmt + "',ExEduCessAmount='" + ExEduCessAmount + "', ExSHECessAmount='" + ExSHECessAmount + "'  , ITCLocation='" + ITCLocation + "' , ITCHSCode='" + ITCHSCode + "',SAPTANotn='" + SAPTANotn + "', " +
                " SAPTASNo='" + SAPTASNo + "', SAPTADesc='" + SAPTADesc + "', PONo='" + PoNo + "',PODate='" + PoDate + "',CVDDutyAmtQty='" + CVDDutyAmtQty + "',BasDutyAmtQty='" + BasDutyAmtQty + "', BasicDutyAmount='" + BasicDutyAmount + "',TotBasicDutyAmt='" + TotBasicDutyAmt + "',TotalCVDAmt='" + TotalCVDAmt + "'," +
                " ExCessNotn='"+ExCessNotn+"', ExCessSlNo='"+ExCessSlNo+"', ExCessRate='"+ExCessRate+"', ExCessFlag='"+ExCessFlag+"', ExCessAmount='"+ExCessAmount+"', ExCessUnit='"+ExCessUnit+"', TotalDutyAmt='"+TotalDutyAmt+"',"+
                " ExEduCessNotn='" + ExEduCessNotn + "',ExEduCessSlNo='" + ExEduCessSNo + "',ExEduCessRate='" + ExEduCessRate + "',ExSHECessRate='" + ExSHECessRate + "' ,CESSDutyAmt='" + CESSDutyAmt + "'" +
                " where ProductId='" + ProductID + "'";//JobNo='" + Jobno + "' and InvoiceNo='" + InvoiceNo + "' And ProductDesc='" + ProductDesc + "'";
            int result = new int();
            SqlConnection con = new SqlConnection(strconn);
            con.Open();
            SqlCommand cmd = new SqlCommand(Query, con);
            result = cmd.ExecuteNonQuery();
            return result;
        }

        public int UpdateEXDuty(double ExEduCessRate, double ExSHECessRate, string ExGSIAddlDutyNotn, string ExGSIAddlDutySlNo,
           double ExGSIAddlDutyRate, string ExGSIAddlDutyFlag, double ExGSIAddlDutyAmount, string ExGSIAddlDutyUnit, string ExSPLExDutyNotn, string ExSPLExDutySlNo, double ExSPLExDutyRate,
           string ExSPLExDutyFlag, double ExSPLExDutyAmount, string ExSPLExDutyUnit, string ExTTAAddlDutyNotn, string ExTTAAddlDutySlNo, double ExTTAAddlDutyRate, string ExTTAAddlDutyFlag,
           double ExTTAAddlDutyAmount, string ExTTAAddlDutyUnit, string ExHealthCessNotn, string ExHealthCessSlNo, double ExHealthCessRate, string ExHealthCessFlag, double ExHealthCessAmount,
           string ExHealthCessUnit, string ExSADNotn, string ExSADSlno, double ExSADRate,
     string AddlNotn, string AddlSno, string NCDNotn, string NCDSno, double NCDRate, string NCDDFlag, double NCDAmount, string NCDUnit, string NCDRule, string SurNotn, string SurSno,
      double SurRate, string SAPTANotn, string SAPTASNo, string SAPTADesc, string TariffValNotn, string TariffValSNo, double TariffUnitQty, string TariffUnit, double TariffRate,
      double TariffAmount, string Jobno, string InvoiceNo, string ProductDesc, string TransId)
        {
            SqlConnection sqlConn = new SqlConnection(strconn);
            int result = new int();
            try
            {
                string query = "update T_Product set ExEduCessRate='" + ExEduCessRate + "', " +
                " ExSHECessRate='" + ExSHECessRate + "',ExGSIAddlDutyNotn='" + ExGSIAddlDutyNotn + "', ExGSIAddlDutySlNo='" + ExGSIAddlDutySlNo + "', " +
                " ExGSIAddlDutyRate='" + ExGSIAddlDutyRate + "', ExGSIAddlDutyFlag='" + ExGSIAddlDutyFlag + "', ExGSIAddlDutyAmount='" + ExGSIAddlDutyAmount + "', " +
                " ExGSIAddlDutyUnit='" + ExGSIAddlDutyUnit + "', ExSPLExDutyNotn='" + ExSPLExDutyNotn + "', ExSPLExDutySlNo='" + ExSPLExDutySlNo + "', ExSPLExDutyRate='" + ExSPLExDutyRate + "'," +
                " ExSPLExDutyFlag='" + ExSPLExDutyFlag + "', ExSPLExDutyAmount='" + ExSPLExDutyAmount + "', ExSPLExDutyUnit='" + ExSPLExDutyUnit + "', ExTTAAddlDutyNotn='" + ExTTAAddlDutyNotn + "', " +
                " ExTTAAddlDutySlNo='" + ExTTAAddlDutySlNo + "', ExTTAAddlDutyRate='" + ExTTAAddlDutyRate + "', ExTTAAddlDutyFlag='" + ExTTAAddlDutyFlag + "', ExTTAAddlDutyAmount='" + ExTTAAddlDutyAmount + "'," +
                " ExTTAAddlDutyUnit='" + ExTTAAddlDutyUnit + "', ExHealthCessNotn='" + ExHealthCessNotn + "', ExHealthCessSlNo='" + ExHealthCessSlNo + "', " +
                " ExHealthCessRate='" + ExHealthCessRate + "', ExHealthCessFlag='" + ExHealthCessFlag + "', ExHealthCessAmount='" + ExHealthCessAmount + "', ExHealthCessUnit='" + ExHealthCessUnit + "', " +
                " ExSADNotn='" + ExSADNotn + "', ExSADSlno='" + ExSADSlno + "', ExSADRate='" + ExSADRate + "' ," +
                " AddlNotn='"+AddlNotn+"', AddlSno='"+AddlSno+"', NCDNotn='"+NCDNotn+"', NCDSno='"+NCDSno+"', NCDRate='"+NCDRate+"', NCDDFlag='"+NCDDFlag+"', NCDAmount='"+NCDAmount+"',"+
                " NCDUnit='"+NCDUnit+"', NCDRule='"+NCDRule+"',  SurNotn='"+SurNotn+"', SurSno='"+SurSno+"', SurRate='"+SurRate+"', SAPTANotn='"+SAPTANotn+"', "+
                " SAPTASNo='" + SAPTASNo + "', SAPTADesc='" + SAPTADesc + "',TariffValNotn='" + TariffValNotn + "',TariffValSNo='" + TariffValSNo + "',TariffUnitQty='" + TariffUnitQty + "',TariffUnit='" + TariffUnit + "',TariffRate='" + TariffRate + "',TariffAmount='" + TariffAmount + "' " +
                " where ProductId='" + TransId + "'";//JobNo='" + Jobno + "' and InvoiceNo='" + InvoiceNo + "' And ProductDesc='" + ProductDesc + "'";

                sqlConn.Open();
                SqlCommand cmd = new SqlCommand(query, sqlConn);
                result = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                string Msg = ex.Message.ToString();
            }
            finally
            {
                sqlConn.Close();
            }
            return result;
        }

        public int InsertITCLicence(string LicenceNo, string LicenceDate, string Quantity, string DebitValue, string RANumber,
         string RADate, string RegPort, string Jobno, string InvoiceNo, string ProductDesc)
        {
            SqlConnection sqlConn = new SqlConnection(strconn);
            int result = new int();
            try
            {
                string query = "insert into T_ProductITCLicence(JobNo, InvoiceNo,  ProductDesc, LicenceNo, LicenceDate, Quantity, DebitValue, RANumber, RADate, RegPort)" +
                " values('" + Jobno + "', '" + InvoiceNo + "', '" + ProductDesc + "', '" + LicenceNo + "', '" + LicenceDate + "', '" + Quantity + "','" + DebitValue + "', '" + RANumber + "', '" + RADate + "','" + RegPort + "')";

                sqlConn.Open();
                SqlCommand cmd = new SqlCommand(query, sqlConn);

                result = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                string Msg = ex.Message.ToString();
            }
            finally
            {
                sqlConn.Close();
            }
            return result;
        }

        public int UpdateITCLicence(string LicenceNo, string LicenceDate, string Quantity, string DebitValue, string RANumber,
            string RADate, string RegPort, string ID)
        {
            SqlConnection sqlConn = new SqlConnection(strconn);
            int result = new int();

            try
            {
                string query = "update T_ProductITCLicence set LicenceNo='" + LicenceNo + "', LicenceDate='" + LicenceDate + "', " +
                    " Quantity='" + Quantity + "', DebitValue='" + DebitValue + "', RANumber='" + RANumber + "', RADate='" + RADate + "', RegPort='" + RegPort + "' " +
                     " where ITCLicenceID='" + ID + "' ";

                sqlConn.Open();
                SqlCommand cmd = new SqlCommand(query, sqlConn);

                result = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                string Msg = ex.Message.ToString();
            }
            finally
            {
                sqlConn.Close();
            }
            return result;
        }
        public DataSet GetProductDetails(string ProductID)
        {
            DataSet ds = new DataSet();
            try
            {
                string Query = "SELECT ProductID, JobNo, InvoiceNo, ProductCode, ProductSNo, ProductDesc, PONo, PODate, ProType, Qty, Unit, UnitPrice, Amount,ProdAmtRs,ProductFamily,RITCNo,AssableValue,TotalDutyAmt FROM  T_Product where ProductID='" + ProductID + "'";
                SqlConnection sqlConn = new SqlConnection(strconn);
                sqlConn.Open();
                SqlDataAdapter da = new SqlDataAdapter(Query, sqlConn);
                da.Fill(ds, "Table");
                sqlConn.Close();
            }
            catch (Exception ex)
            {
                string Msg = ex.Message.ToString();
            }
            return ds;
        }

        public DataSet GetITCLicNo(string Jobno, string InvoiceNo, string ProductDesc)
        {
            DataSet ds = new DataSet();
            string qry = "Select  ITCLicenceID as ID,LicenceNo, LicenceDate, Quantity, DebitValue, RANumber, RADate, RegPort from T_ProductITCLicence where JobNo='" + Jobno + "' and InvoiceNo='" + InvoiceNo + "' And ProductDesc='" + ProductDesc + "'";
            SqlConnection con = new SqlConnection(strconn);
            con.Open();
            SqlDataAdapter da = new SqlDataAdapter(qry, con);
            da.Fill(ds, "ITCLic");
            con.Close();
            return ds;
        }
        public int InsertPrevBEDetails(string PrevBENo, string PrevBEDate, string UnitValue, double UnitRate, string CustomHouse,
        string Jobno, string InvoiceNo, string ProductDesc)
        {
            SqlConnection sqlConn = new SqlConnection(strconn);
            int result = new int();
            try
            {
                string query = "insert into  T_ProductPrevBEDetails(JobNo, InvoiceNo,  ProductDesc, PrevBENo, PrevBEDate, UnitValue, UnitRate, CustomHouse)" +
                " values('" + Jobno + "', '" + InvoiceNo + "', '" + ProductDesc + "', '" + PrevBENo + "', '" + PrevBEDate + "', '" + UnitValue + "','" + UnitRate + "', '" + CustomHouse + "')";

                sqlConn.Open();
                SqlCommand cmd = new SqlCommand(query, sqlConn);

                result = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                string Msg = ex.Message.ToString();
            }
            finally
            {
                sqlConn.Close();
            }
            return result;
        }

        public DataSet GetPrevBEDetails(string Jobno, string InvoiceNo, string ProductDesc)
        {
            DataSet ds = new DataSet();
            string qry = "Select  PrevBEID as ID,PrevBENo, PrevBEDate, UnitValue, UnitRate, CustomHouse from T_ProductPrevBEDetails where JobNo='" + Jobno + "' and InvoiceNo='" + InvoiceNo + "' And ProductDesc='" + ProductDesc + "'";
            SqlConnection con = new SqlConnection(strconn);
            con.Open();
            SqlDataAdapter da = new SqlDataAdapter(qry, con);
            da.Fill(ds, "PrevBE");
            con.Close();
            return ds;
        }

        public int UpdatePrevBEDetails(string PrevBENo, string PrevBEDate, string UnitValue, double UnitRate, string CustomHouse, string ID)
        {
            SqlConnection sqlConn = new SqlConnection(strconn);
            int result = new int();

            try
            {

                string query = " Update T_ProductPrevBEDetails set PrevBENo='" + PrevBENo + "', PrevBEDate='" + PrevBEDate + "', " +
                               " UnitValue='" + UnitValue + "', UnitRate='" + UnitRate + "', CustomHouse='" + CustomHouse + "' " +
                               " where PrevBEID='" + ID + "' ";
                sqlConn.Open();
                SqlCommand cmd = new SqlCommand(query, sqlConn);

                result = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                string Msg = ex.Message.ToString();
            }
            finally
            {
                sqlConn.Close();
            }
            return result;
        }
        public DataSet GetProductDutyPer(string RITCNo)
        {
            DataSet ds = new DataSet();
            try
            {
                string Query = "SELECT BASNotn,BASDuty, BASAmt, BASUnit,CVDNotn, CVDDuty, CVDAmt, CVDUnit,MRP, MRPUnit, Abatement,EDU_CESS_NOTN, EDU_CESS_SNO, EDU_CESS_RATE,SHE_CESS_NOTN, SHE_CESS_SNO, SHE_CESS_RATE,AddlDuty_NOTN, AddlDuty_SNO, AddlDuty_RATE,ACVDDuty, ACS2_DUTY, SCVD_DUTY, CESS_DUTY, NCD_Rate,HLTH_Rate,POLICYPARA,POLICY_YR FROM  M_Product where RITCNo='" + RITCNo + "'";
                SqlConnection sqlConn = new SqlConnection(strconn);
                sqlConn.Open();
                SqlDataAdapter da = new SqlDataAdapter(Query, sqlConn);
                da.Fill(ds, "Table");
                sqlConn.Close();
            }
            catch (Exception ex)
            {
                string Msg = ex.Message.ToString();
            }
            return ds;
        }
        public DataSet GetBCDRTA(string RITCNo)
        {
            DataSet ds = new DataSet();
            try
            {
                string Query = "Select RTA from M_BCD_Duty where CTH='" + RITCNo + "'";
                SqlConnection sqlConn = new SqlConnection(strconn);
                sqlConn.Open();
                SqlDataAdapter da = new SqlDataAdapter(Query, sqlConn);
                da.Fill(ds, "Table");
                sqlConn.Close();
            }
            catch (Exception ex)
            {
                string Msg = ex.Message.ToString();
            }
            return ds;
        }
        public DataSet GetCVDRTA(string RITCNo)
        {
            DataSet ds = new DataSet();
            try
            {
                string Query = "Select CVDRTA from M_CVD_Duty where CETH='" + RITCNo + "'";
                SqlConnection sqlConn = new SqlConnection(strconn);
                sqlConn.Open();
                SqlDataAdapter da = new SqlDataAdapter(Query, sqlConn);
                da.Fill(ds, "Table");
                sqlConn.Close();
            }
            catch (Exception ex)
            {
                string Msg = ex.Message.ToString();
            }
            return ds;
        }
        public DataSet GetCVDUserNoti(string SubRITCNo)
        {
            DataSet ds = new DataSet();
            try
            {
                string Query = "SELECT DISTINCT  DutyName, ChapterNo, Notification, SerialNo, Duty, EffectiveDate FROM M_UserNotification where DutyName='CVD' And ChapterNo='" + SubRITCNo + "' OR DutyName='CVD' And ChapterNo='All'";
                SqlConnection sqlConn = new SqlConnection(strconn);
                sqlConn.Open();
                SqlDataAdapter da = new SqlDataAdapter(Query, sqlConn);
                da.Fill(ds, "Table");
                sqlConn.Close();
            }
            catch (Exception ex)
            {
                string Msg = ex.Message.ToString();
            }
            return ds;
        }
        public DataSet GetSADUserNoti(string SubRITCNo)
        {
            DataSet ds = new DataSet();
            try
            {
                string Query = "SELECT DISTINCT  DutyName, ChapterNo, Notification, SerialNo, Duty, EffectiveDate FROM M_UserNotification where DutyName='SAD' And ChapterNo='" + SubRITCNo + "' OR DutyName='SAD' And ChapterNo='All'";
                SqlConnection sqlConn = new SqlConnection(strconn);
                sqlConn.Open();
                SqlDataAdapter da = new SqlDataAdapter(Query, sqlConn);
                da.Fill(ds, "Table");
                sqlConn.Close();
            }
            catch (Exception ex)
            {
                string Msg = ex.Message.ToString();
            }
            return ds;
        }
        public DataSet GetMRPRTA(string RITCNo)
        {
            DataSet ds = new DataSet();
            try
            {
                string Query = "Select ABETRTA from  M_RSP_Duty where CETH='" + RITCNo + "'";
                SqlConnection sqlConn = new SqlConnection(strconn);
                sqlConn.Open();
                SqlDataAdapter da = new SqlDataAdapter(Query, sqlConn);
                da.Fill(ds, "Table");
                sqlConn.Close();
            }
            catch (Exception ex)
            {
                string Msg = ex.Message.ToString();
            }
            return ds;
        }
        public int DeleteProductDetails(string ProductID)
        {
            SqlConnection sqlConn = new SqlConnection(strconn);
            int result = new int();
            try
            {
                string Query = "Delete  from  T_Product where ProductID='" + ProductID + "'";
                sqlConn.Open();
                SqlCommand cmd = new SqlCommand(Query, sqlConn);
                result = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                string Msg = ex.Message.ToString();
            }
            finally
            {
                sqlConn.Close();
            }
            return result;
        }
        public DataSet GetScheme(string ID)
        {
            DataSet ds = new DataSet();
            try
            {
                string Query = "SELECT ID, EDIRegNo, Date, JobNo, InvoiceNo, ItemSno, ItemSnoinLic, SchemeLicNo, SchemeLicDate, SchemeType, CIFValue, Qty, Unit, ValueDebited, RegPort FROM  T_Schemes where ID='" + ID + "'";


                SqlConnection sqlConn = new SqlConnection(strconn);
                sqlConn.Open();
                SqlDataAdapter da = new SqlDataAdapter(Query, sqlConn);
                da.Fill(ds, "Table");
                sqlConn.Close();
            }
            catch (Exception ex)
            {
                string Msg = ex.Message.ToString();
            }
            return ds;
        }

        public int UpdateSchemeDetails(string ID, string EDIRegNo, string Date, string ItemSno, string ItemSnoinLic,
            string SchemeLicNo, string SchemeLicDate, string SchemeType, string CIFValue, string Qty, string Unit,
            string ValueDebited, string RegPort)
        {
            SqlConnection sqlConn = new SqlConnection(strconn);
            int result = new int();

            try
            {

                string query = " Update [T_Schemes] set EDIRegNo='" + EDIRegNo + "', Date='" + Date + "', " +
                               " ItemSno='" + ItemSno + "', ItemSnoinLic='" + ItemSnoinLic + "', SchemeLicNo='" + SchemeLicNo + "', " +
                               " SchemeLicDate='" + SchemeLicDate + "', SchemeType='" + SchemeType + "', CIFValue='" + CIFValue + "', " +
                               " Qty='" + Qty + "', Unit='" + Unit + "', ValueDebited='" + ValueDebited + "', RegPort='" + RegPort + "' " +
                               " where ID='" + ID + "' ";
                sqlConn.Open();
                SqlCommand cmd = new SqlCommand(query, sqlConn);

                result = cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                string Msg = ex.Message.ToString();
            }
            finally
            {
                sqlConn.Close();
            }
            return result;
        }


        public DataSet GetProductMaster(string ProductName)
        {
            string Query = "select distinct ProductCode,ProductDesc,RITCNo from M_Product where ProductDesc like '"+ProductName+"' ";
            SqlConnection con = new SqlConnection(strconn);
            SqlDataAdapter da = new SqlDataAdapter(Query,con);
            DataSet ds = new DataSet();
            da.Fill(ds,"product");
            return ds;
        }

        public DataSet GetProductMasterCode(string Productcode)
        {
            string Query = "select distinct ProductCode,ProductDesc,RITCNo from M_Product where ProductCode like '" + Productcode + "' ";
            SqlConnection con = new SqlConnection(strconn);
            SqlDataAdapter da = new SqlDataAdapter(Query, con);
            DataSet ds = new DataSet();
            da.Fill(ds, "product");
            return ds;
        }


    }
}
