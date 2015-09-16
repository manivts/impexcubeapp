// -----------------------------------------------------------------------
// <copyright file="CommonDL.cs" company="">
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
    public class CommonDL
    {
        string strcon = (string)ConfigurationManager.ConnectionStrings["Constr"].ConnectionString;
        public DataSet GetJobNo()
        {
            DataSet ds = new DataSet();
            try
            {
                string Query = "Select JobNo from T_JobCreation order By JobNo DESC";
                SqlConnection sqlConn = new SqlConnection(strcon);
                sqlConn.Open();
                SqlDataAdapter da = new SqlDataAdapter(Query, sqlConn);
                da.Fill(ds, "jobno");
                sqlConn.Close();
            }
            catch (Exception ex)
            {
                string Msg = ex.Message.ToString();
            }
            return ds;
        }
        public DataSet GetExportJobNo()
        {
            DataSet ds = new DataSet();
            try
            {
                string Query = "Select JobNo from E_M_JobCreation order By JobNo DESC";
                SqlConnection sqlConn = new SqlConnection(strcon);
                sqlConn.Open();
                SqlDataAdapter da = new SqlDataAdapter(Query, sqlConn);
                da.Fill(ds, "jobno");
                sqlConn.Close();
            }
            catch (Exception ex)
            {
                string Msg = ex.Message.ToString();
            }
            return ds;
        }
        public DataSet GetInvNo(string JobNo)
        {
            DataSet ds = new DataSet();
            try
            {
                string Query = "Select InvoiceNo from T_invoiceDetails where JobNo='" + JobNo + "'";
                SqlConnection sqlConn = new SqlConnection(strcon);
                sqlConn.Open();
                SqlDataAdapter da = new SqlDataAdapter(Query, sqlConn);
                da.Fill(ds, "InvNo");
                sqlConn.Close();
            }
            catch (Exception ex)
            {
                string Msg = ex.Message.ToString();
            }
            return ds;
        }
        public DataSet GetProductType()
        {
            DataSet ds = new DataSet();
            try
            {
                string Query = "Select ProductDutyType from M_ProductDutyType";
                SqlConnection sqlConn = new SqlConnection(strcon);
                sqlConn.Open();
                SqlDataAdapter da = new SqlDataAdapter(Query, sqlConn);
                da.Fill(ds, "ProType");
                sqlConn.Close();
            }
            catch (Exception ex)
            {
                string Msg = ex.Message.ToString();
            }
            return ds;
        }
        public DataSet GetRITCNo()
        {
            DataSet ds = new DataSet();
            try
            {
                string Query = "Select Distinct RITCNo from M_Product";
                SqlConnection sqlConn = new SqlConnection(strcon);
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
        public DataSet GetUnit()
        {
            DataSet ds = new DataSet();
            try
            {
                string Query = "Select Distinct UnitShort from M_Unit";
                SqlConnection sqlConn = new SqlConnection(strcon);
                sqlConn.Open();
                SqlDataAdapter da = new SqlDataAdapter(Query, sqlConn);
                da.Fill(ds, "Unit");
                sqlConn.Close();
            }
            catch (Exception ex)
            {
                string Msg = ex.Message.ToString();
            }
            return ds;
        }
        
        public DataSet GetJobImportShipment( string JobNo)
        {
            DataSet ds = new DataSet();
            try
            {
                string Query = "SELECT DISTINCT JobNo, JobReceivedDate, Mode, Custom, BEType, DocFillingStatus, BENo, BEDate, ShipmentPort AS Portofshipment, ShipmentCountry AS CountryofShipment, CountryOrigin AS CountryofOrgin, Currency, TotalInvoiceValue, Consignor, ConsignorAddress, ConsignorCity, ConsignorCountry, CountryOriginCode,Importer FROM   View_JobImporterShipment where JobNo='" + JobNo + "'";
                SqlConnection sqlConn = new SqlConnection(strcon);
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

        public DataSet GetJobDetails(string JobNo)
        {
            DataSet ds = new DataSet();
            try
            {
                string Query = "SELECT DISTINCT JobNo, JobReceivedDate, Mode, Custom, BEType, DocFillingStatus,  BENo, BEDate FROM T_JobCreation where JobNo='" + JobNo + "'";
                SqlConnection sqlConn = new SqlConnection(strcon);
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
        public DataSet GetInvoiceDetails(string JobNo,string InvNo)
        {
            DataSet ds = new DataSet();
            try
            {
                string Query = "SELECT InvoiceDate, InvoiceTerms, InvoiceFreightType, InvoicePaymentTerms, InvoiceNatureofTrans, InvoiceCurrency, InvoiceExchangeRates, InvoiceProductValues,FreightAmount,InsuranceAmount,MisINRAmount,AgencyINRAmount,InvoiceProductINRValues, FreightINRAmount, InsuranceINRAmount,DiscountINRAmount,NoofProduct,PONo,PODate,HighSeaAmtINR FROM  T_InvoiceDetails where JobNo='" + JobNo + "' And InvoiceNo='" + InvNo + "'";
                SqlConnection sqlConn = new SqlConnection(strcon);
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

    public DataSet GetProductPopup(string ProductName)
        {
            DataSet ds = new DataSet();
            try
            {
                string Query = "Select ProductCode,ProductDesc,RITCNo from M_Product where ProductDesc like '%" + ProductName + "%' or ProductCode like '%" + ProductName + "%'";
                SqlConnection sqlConn = new SqlConnection(strcon);
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
    public DataSet GetProductDuty(string RITCCode)
    {
        DataSet ds = new DataSet();
        try
        {
            string Query = "SELECT ITCLocation,ITCHS_CODE,POLICYPARA,POLICY_YR FROM  M_Product where RITCNo='" + RITCCode + "'";
            SqlConnection sqlConn = new SqlConnection(strcon);
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
    //public DataSet GetConsignorPopup(string ConsName, string mode)
    //{
    //    DataSet ds = new DataSet();
    //    try
    //    {
    //        string Query = "";
    //        if (mode == "cnsr")
    //        {
    //            Query = "Select Distinct M_Consignor.ConsignorCode,M_Consignor.ConsignorName,M_ConsignorAddress.Address,  M_Country.CountryName from   M_Consignor Inner Join M_ConsignorAddress On M_Consignor.ConsignorCode= M_ConsignorAddress.ConsignorCode Inner Join  M_Country on M_ConsignorAddress.CountryCode=M_Country.CountryCode where ConsignorName like '%" + ConsName + "%'";
    //            //Query = "SELECT ShortName, AccountName,IECode,  BranchId,  Address1,State FROM M_AccountMaster where AccountType='Consignor'And  AccountName  like '%" + ConsName + "%'";
    //        }
    //        else if (mode == "seller")
    //        {
    //            Query = "Select Distinct M_Consignor.ConsignorCode,M_Consignor.ConsignorName,M_ConsignorAddress.Address,  M_Country.CountryName from   M_Consignor Inner Join M_ConsignorAddress On M_Consignor.ConsignorCode= M_ConsignorAddress.ConsignorCode Inner Join  M_Country on M_ConsignorAddress.CountryCode=M_Country.CountryCode where ConsignorName like '%" + ConsName + "%'";
    //            //Query = "SELECT ShortName, AccountName,IECode,  BranchId,  Address1,State FROM M_AccountMaster where AccountType='Consignee'And  AccountName  like '%" + ConsName + "%'";
    //        }
    //        else if (mode == "agent")
    //        {
    //            Query = "Select Distinct M_Consignor.ConsignorCode,M_Consignor.ConsignorName,M_ConsignorAddress.Address,  M_Country.CountryName from   M_Consignor Inner Join M_ConsignorAddress On M_Consignor.ConsignorCode= M_ConsignorAddress.ConsignorCode Inner Join  M_Country on M_ConsignorAddress.CountryCode=M_Country.CountryCode where ConsignorName like '%" + ConsName + "%'";
    //            //Query = "SELECT ShortName, AccountName,IECode,  BranchId,  Address1,State FROM M_AccountMaster where AccountType='Consignor'And  AccountName  like '%" + ConsName + "%'";
    //        }
    //        else if (mode == "imp" || mode == "high")
    //        {
    //            Query = "SELECT Distinct PartyCode, PartyName, IeCodeNo,  BranchSno, Address,City,State, ZipCode,CommericalTaxState, CommericalTaxType, CommericalTaxRegnNo FROM View_M_Importer where PartyName like '%" + ConsName + "%'";
    //            //Query = "SELECT ShortName, AccountName,IECode,  BranchId,  Address1,State FROM M_AccountMaster where AccountType='Customer'And  AccountName  like '%" + ConsName + "%'";
    //        }
    //        else if (mode == "Exp")
    //        {
    //            Query = "SELECT Distinct PartyCode, PartyName, IeCodeNo,  BranchSno, Address,State  FROM View_M_Importer where PartyName like '%" + ConsName + "%'";
    //            //Query = "SELECT ShortName, AccountName,IECode,  BranchId,  Address1,State FROM M_AccountMaster where AccountType='Customer'And  AccountName  like '%" + ConsName + "%'";
    //        }
    //        else if (mode == "ExpCnsr")
    //        {
    //            Query = "Select Distinct M_Consignor.ConsignorCode,M_Consignor.ConsignorName,M_ConsignorAddress.Address,  M_Country.CountryName from   M_Consignor Inner Join M_ConsignorAddress On M_Consignor.ConsignorCode= M_ConsignorAddress.ConsignorCode Inner Join  M_Country on M_ConsignorAddress.CountryCode=M_Country.CountryCode where ConsignorName like '%" + ConsName + "%'";
    //            //Query = "SELECT ShortName, AccountName,IECode,  BranchId,  Address1,State FROM M_AccountMaster where AccountType='Consignor'And  AccountName  like '%" + ConsName + "%'";
    //        }
    //        else if (mode == "ExpBuyer")
    //        {
    //            Query = "Select Distinct M_Consignor.ConsignorCode,M_Consignor.ConsignorName,M_ConsignorAddress.Address,  M_Country.CountryName from   M_Consignor Inner Join M_ConsignorAddress On M_Consignor.ConsignorCode= M_ConsignorAddress.ConsignorCode Inner Join  M_Country on M_ConsignorAddress.CountryCode=M_Country.CountryCode where ConsignorName like '%" + ConsName + "%'";
    //            //Query = "SELECT ShortName, AccountName,IECode,  BranchId,  Address1,State FROM M_AccountMaster where AccountType='Consignor'And  AccountName  like '%" + ConsName + "%'";
    //        }
    //        else if (mode == "Notify")
    //        {
    //            Query = "SELECT Distinct PartyCode, PartyName, IeCodeNo,  BranchSno, Address,State  FROM View_M_Importer where PartyName like '%" + ConsName + "%'";
    //            //Query = "SELECT ShortName, AccountName,IECode,  BranchId,  Address1,State FROM M_AccountMaster where AccountType='Customer'And  AccountName  like '%" + ConsName + "%'";
    //        }
    //        else if (mode == "Product")
    //        {
    //            Query = "SELECT Distinct PartyCode, PartyName, IeCodeNo,  BranchSno, Address,State  FROM View_M_Importer where PartyName like '%" + ConsName + "%'";
    //            //Query = "SELECT ShortName, AccountName,IECode,  BranchId,  Address1,State FROM M_AccountMaster where AccountType='Customer'And  AccountName  like '%" + ConsName + "%'";
    //        }
    //        SqlConnection sqlConn = new SqlConnection(strcon);
    //        sqlConn.Open();
    //        SqlDataAdapter da = new SqlDataAdapter(Query, sqlConn);
    //        da.Fill(ds, "Table");
    //        sqlConn.Close();
    //    }
    //    catch (Exception ex)
    //    {
    //        string Msg = ex.Message.ToString();
    //    }
    //    return ds;
    //}
    public DataSet GetAssBCDCVD(string TransId)
    {
        DataSet ds = new DataSet();
        try
        {
            string Query = "Select  ProdAmtRs, Freight, Insurance, AgencyCharge, Miscellaneous,LandingChrg, AssableValue,BasDutyAmtPer,CVDDutyAmtPer,SADAmt,EduCessRate,SHECessRate,EduCessAmount,SHECessAmount, ExEduCessRate,ExSHECessRate from  T_Product where ProductID='" + TransId + "'";//JobNo='" + JobNo + "' And InvoiceNo='" + InvNo + "'";
            SqlConnection sqlConn = new SqlConnection(strcon);
            sqlConn.Open();
            SqlDataAdapter da = new SqlDataAdapter(Query, sqlConn);
            da.Fill(ds, "InvNo");
            sqlConn.Close();
        }
        catch (Exception ex)
        {
            string Msg = ex.Message.ToString();
        }
        return ds;
    }
    public string GetImpExchangeRate(string CurrencyShortName)
    {
        string ExRate = "";
        try
        {
            DataSet ds = new DataSet();
            string Query = "Select IMPCurrencyRate from M_Currency where CurrencyShortName='" + CurrencyShortName + "'";
            SqlConnection sqlConn = new SqlConnection(strcon);
            sqlConn.Open();
            SqlDataAdapter da = new SqlDataAdapter(Query, sqlConn);
            da.Fill(ds, "ExchRate");
            sqlConn.Close();
            DataRowView row = ds.Tables["ExchRate"].DefaultView[0];
            ExRate = row["IMPCurrencyRate"].ToString();
        }
        catch (Exception ex)
        {
            string Msg = ex.Message.ToString();
        }
        return ExRate;
    }
    public int ExecuteNonQuery(string Query)
    {
        int Result = 0;
        try
        {
            SqlConnection con = new SqlConnection(strcon);
            con.Open();
            SqlCommand cmd = new SqlCommand(Query.ToString(), con);
            Result = cmd.ExecuteNonQuery();
            con.Close();
        }
        catch (Exception ex)
        {
            string Msg = ex.Message;
        }
        return Result;
    }
    public DataSet GetDataSet(string Query)
    {
        DataSet ds = new DataSet();
        try
        {
            SqlConnection sqlConn = new SqlConnection(strcon);
            sqlConn.Open();
            SqlDataAdapter da = new SqlDataAdapter(Query, sqlConn);
            da.Fill(ds, "Table");
            sqlConn.Close();
        }
        catch (Exception ex)
        {
            string Msg = ex.Message;
        }
        return ds;
    }
    public DataSet GetState()
    {
        DataSet ds = new DataSet();

        try
        {
            string Query = "select Distinct statename from M_State";

            ds = GetDataSet(Query);
        }
        catch (Exception ex)
        {
            string Msg = ex.Message;
        }
        return ds;
    }
    public DataSet GetCountryDetails()
    {

        SqlConnection sqlConn = new SqlConnection(strcon);
        DataSet ds = new DataSet();
        try
        {
            string Query = "select CountryName from M_Country Order By CountryName";

            sqlConn.Open();
            SqlDataAdapter da = new SqlDataAdapter(Query, sqlConn);

            da.Fill(ds, "Country");
        }
        catch (Exception ex)
        {
            string Msg = ex.Message.ToString();
        }
        finally
        {
            sqlConn.Close();
        }
        return ds;
    }
    public DataSet GetCurrencyDetails()
    {

        SqlConnection sqlConn = new SqlConnection(strcon);
        DataSet ds = new DataSet();
        try
        {
            string Query = "select Distinct CurrencyShortName from M_Currency";

            sqlConn.Open();
            SqlDataAdapter da = new SqlDataAdapter(Query, sqlConn);

            da.Fill(ds, "Invoice");
        }
        catch (Exception ex)
        {
            string Msg = ex.Message.ToString();
        }
        finally
        {
            sqlConn.Close();
        }
        return ds;
    }
    public DataSet CheckProduct(string RITCNo)
    {
        DataSet ds = new DataSet();
        try
        {
            string Query = "SELECT RITCNo FROM  M_Product where RITCNo='" + RITCNo + "'";
            SqlConnection sqlConn = new SqlConnection(strcon);
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
}
}
