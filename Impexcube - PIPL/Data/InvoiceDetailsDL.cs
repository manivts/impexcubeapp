// -----------------------------------------------------------------------
// <copyright file="InvoiceDetailsDL.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace VTS.ImpexCube.Data
{
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Data;
    using System.Data.SqlClient;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class InvoiceDetailsDL
    {
        string strconn = (string)ConfigurationManager.ConnectionStrings["Constr"].ConnectionString;

        public DataSet GetInvoiceDetails(string JobNo)
        {
             
            StringBuilder Query = new StringBuilder();
            SqlConnection sqlConn = new SqlConnection(strconn);
            DataSet ds = new DataSet();
            try
            {
                Query.Append("SELECT InvoiceDetailsId,InvoiceNo,InvoiceDate As [Date],InvoiceTerms As [Terms],InvoiceFreightType As [FreightType],");
                Query.Append("InvoicePaymentTerms As [PaymentTerms],InvoiceNatureofTrans As [TransMode],InvoiceCurrency As [Currency],InvoiceExchangeRates As [ExchRates],");
                Query.Append("InvoiceProductValues As [Amount],InvoiceProductINRValues As [AmountINR] FROM T_InvoiceDetails where Jobno='" + JobNo + "' ");

                string Qry = Query.ToString();                
                sqlConn.Open();
                SqlDataAdapter da = new SqlDataAdapter(Qry, sqlConn);

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

        public DataSet GetJobNo()
        {
            DataSet ds = new DataSet();
            try
            {
                string Query = "Select Distinct JobNo from T_JobCreation order By JobNo DESC";

                SqlConnection sqlConn = new SqlConnection(strconn);
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

        public DataSet GetCurrencyDetails()
        {
            
            SqlConnection sqlConn = new SqlConnection(strconn);
            DataSet ds = new DataSet();
            try
            {
                string Query = "select CurrencyShortName from M_Currency";

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

        public DataSet GetCountryDetails()
        {

            SqlConnection sqlConn = new SqlConnection(strconn);
            DataSet ds = new DataSet();
            try
            {
                string Query = "select CountryName from M_Country";

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

        public int InsertInvoiceDetails(
            string JobNo, string invoiceNo, string invoiceDate, string Terms, string FreightType, string PaymentTerm, string NatureofTrans,
            string currency, string exchgrates, string prodvalues,
            //string FreightTy, string FreightTyCurrency, string FreightTyExRate, string FreightTyAmount, string FreightTyAmountINR,
            // string InsuranceType, string InsuranceTyCurrency, string InsuranceTyExRate, string InsuranceTyAmount, string InsuranceTyAmountINR, 
            //string MiscellaneousType,string MiscellaneousTyCurrency,string MiscellaneousTyExRate,string MiscellaneousTyAmt,string MiscellaneousTyAmtINR,
            string createdBy, DateTime createdDate, string modifiedBy, DateTime modifiedDate)
        {
            SqlConnection sqlConn = new SqlConnection(strconn);
            int result = new int();
            try
            {
                double exch = Convert.ToDouble(exchgrates);
                double prodval = Convert.ToDouble(prodvalues);
                double proinrval = exch * prodval;
                string query = "INSERT INTO T_invoiceDetails (JobNo,InvoiceNo, InvoiceDate, InvoiceTerms, InvoiceFreightType, InvoicePaymentTerms, InvoiceNatureofTrans, " +
                    " InvoiceCurrency,InvoiceExchangeRates, InvoiceProductValues,InvoiceProductINRValues, " +
                    " CreatedBy, CreatedDate, ModifiedBy, ModifiedDate) values ('" + JobNo + "','" + invoiceNo + "', " +
                    " '" + invoiceDate + "', '" + Terms + "', '" + FreightType + "','" + PaymentTerm + "','" + NatureofTrans + "','" + currency + "','" + exch + "', " +
                    " '" + prodval + "','" + proinrval + "', "+
                    " '" + createdBy + "','" + createdDate + "','" + modifiedBy + "','" + modifiedDate + "') ";
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

        private string Frmdatesplit(string invdate)
        {
            string[] invdate1 = invdate.Split('/');
            string invdate2 = invdate1[1] + '/' + invdate1[0] + '/' + invdate1[2];
            return invdate2;
        }

        public DataSet GetInvoiceDetailsByNo(string invoiceNo)
        {
            StringBuilder Query = new StringBuilder();
            SqlConnection sqlConn = new SqlConnection(strconn);
            DataSet ds = new DataSet();
            try
            {
                Query.Append("SELECT InvoiceNo,InvoiceDate,InvoiceTerms,InvoiceFreightType,InvoicePaymentTerms,InvoiceNatureofTrans,InvoiceCurrency,InvoiceExchangeRates,InvoiceProductValues FROM T_InvoiceDetails where InvoiceNo='" + invoiceNo + "'");

                string Qry = Query.ToString();
                sqlConn.Open();
                SqlDataAdapter da = new SqlDataAdapter(Qry, sqlConn);

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

        public int UpdateFreightDetails(string FreightCurrency, double FreightExchangeRate, double FreightRate, double FreightAmount, string InsuranceCurrency, double InsuranceExchangeRate,
           double InsuranceRate, double InsuranceAmount, string DiscountCurrency, double DiscountExchangeRate, double DiscountRate, double DiscountAmount,
           string MisCurrency, double MisExchRate, double MisRate, double MisAmount, string AgencyCurrency, double AgencyExchRate,
           double AgencyRate, double AgencyAmount, string LoadingCurrency, double LoadingExchRate, double LoadingRate, double LoadingAmount, string SaleCondition, string Remarks,
            string HighSeaCurrency,double HighSeaExRate,double HighSeaRate,double HighSeaAmt,double HighSeaAmtINR,
           string JobNo, string InvoiceNo, string modifiedBy, DateTime modifiedDate,
double FreightTyExRate,
double FreightTyAmount,
double InsuranceTyExRate,
double InsuranceTyAmount,
double MiscellaneousTyExRate,
double MiscellaneousTyAmt)
        
        { 
        // FreightCurrency, FreightExchangeRate, FreightRate, FreightAmount, InsuranceCurrency, InsuranceExchangeRate, 
                  //    InsuranceRate, InsuranceAmount, DiscountCurrency, DiscountExchangeRate, DiscountRate, DiscountAmount

            SqlConnection sqlConn = new SqlConnection(strconn);
            int result = new int();

            try
            {
                string query = "update T_invoiceDetails set FreightCurrency='" + FreightCurrency + "', FreightExchangeRate='" + FreightExchangeRate + "', FreightRate='" + FreightRate + "', FreightAmount='" + FreightAmount + "', FreightINRAmount='" + FreightExchangeRate * FreightAmount + "', " +
                    " InsuranceCurrency='" + InsuranceCurrency + "', InsuranceExchangeRate='" + InsuranceExchangeRate + "',InsuranceRate='" + InsuranceRate + "', InsuranceAmount='" + InsuranceAmount + "',InsuranceINRAmount='" + InsuranceExchangeRate*InsuranceAmount + "', DiscountCurrency='" + DiscountCurrency + "', " +
                    " DiscountExchangeRate='" + DiscountExchangeRate + "', DiscountRate='" + DiscountRate + "', DiscountAmount='" + DiscountAmount + "',DiscountINRAmount='" + DiscountExchangeRate*DiscountAmount + "', "+
                    " MisCurrency='" + MisCurrency + "', MisExchRate='" + MisExchRate + "', MisRate='" + MisRate + "', MisAmount='" + MisAmount + "',MisINRAmount='" + MisExchRate * MisAmount + "', AgencyCurrency='" + AgencyCurrency + "', " +
                    " AgencyExchRate='" + AgencyExchRate + "', AgencyRate='" + AgencyRate + "', AgencyAmount='" + AgencyAmount + "',AgencyINRAmount='" + AgencyExchRate * AgencyAmount + "', LoadingCurrency='" + LoadingCurrency + "', LoadingExchRate='" + LoadingExchRate + "', LoadingRate='" + LoadingRate + "', " +
                    " HighSeaCurrency='"+HighSeaCurrency+"',HighSeaExRate='"+HighSeaExRate+"',HighSeaRate='"+HighSeaRate+"',HighSeaAmt='"+HighSeaAmt+"',HighSeaAmtINR='"+HighSeaAmtINR+"', " +
                    " LoadingAmount='" + LoadingAmount + "', SaleCondition='" + SaleCondition + "', Remarks='" + Remarks + "',ModifiedBy='" + modifiedBy + "',ModifiedDate='" + modifiedDate + "' ,"+
                    " FreightTyExRate='" + FreightTyExRate + "', FreightTyAmount='" + FreightTyAmount + "', InsuranceTyExRate='" + InsuranceTyExRate + "', InsuranceTyAmount='" + InsuranceTyAmount + "' ,MiscellaneousTyExRate='" + MiscellaneousTyExRate + "' , MiscellaneousTyAmt='" + MiscellaneousTyAmt + "' where JobNo='" + JobNo + "' and InvoiceNo='" + InvoiceNo + "' ";
                   

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

        public int UpdateInvoiceDetails(string invoiceNo, string invoiceDate, string Terms, string FreightType, string PaymentTerm, string NatureofTrans, string currency, 
            string exchgrates, string prodvalues,string ProductINR,string modifiedBy, DateTime modifiedDate,string InvoiceID)
        {

            SqlConnection sqlConn = new SqlConnection(strconn);
            int result = new int();

            try
            {
                string query = "update T_invoiceDetails set InvoiceNo='"+invoiceNo+"', InvoiceDate='"+invoiceDate+"', InvoiceTerms='"+Terms+"', InvoiceFreightType='"+FreightType+"', "+
                    " InvoicePaymentTerms='" + PaymentTerm + "', InvoiceNatureofTrans='" + NatureofTrans + "',InvoiceCurrency='" + currency + "',InvoiceExchangeRates='" + exchgrates + "', InvoiceProductValues='" + prodvalues + "',InvoiceProductINRValues='" + ProductINR + "', " +
                    " FreightType='" + FreightType + "', InsuranceType='" + FreightType + "', MiscellaneousType='" + FreightType + "', ModifiedBy='" + modifiedBy + "', ModifiedDate='" + modifiedDate + "' where  InvoiceDetailsId='" + InvoiceID + "' ";

                //,FreightTyCurrency='"+FreightTyCurrency+"',FreightTyExRate='"+FreightTyExRate+"',FreightTyAmount='"+FreightTyAmount+"',FreightTyAmountINR='"+FreightTyAmountINR+"', "+
                //    " InsuranceType='" + InsuranceType + "',InsuranceTyCurrency='" + InsuranceTyCurrency + "',InsuranceTyExRate='" + InsuranceTyExRate + "',InsuranceTyAmount='" + InsuranceTyAmount + "',InsuranceTyAmountINR='" + InsuranceTyAmountINR + "', " +
                //    " MiscellaneousType='" + MiscellaneousType + "',MiscellaneousTyCurrency='" + MiscellaneousTyCurrency + "',MiscellaneousTyExRate='" + MiscellaneousTyExRate + "',MiscellaneousTyAmt='" + MiscellaneousTyAmt + "',MiscellaneousTyAmtINR='" + MiscellaneousTyAmtINR + "'
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



        public int UpdateConsignorDetails(string ConsignorNameAddress, string ConsignorCountry, string SellerNameAddress, string SellerCountry, string BrokerNameAddress, string BrokerCountry, string JobNo, string InvoiceNo, string modifiedBy, DateTime modifiedDate, string ConsignorName, string SellerName, string BrokerName)
        {

            SqlConnection sqlConn = new SqlConnection(strconn);
            int result = new int();

            try
            {
                string query = "update T_invoiceDetails set  ConsignorNameAddress='"+ConsignorNameAddress+"', ConsignorCountry='"+ConsignorCountry+"', SellerNameAddress='"+SellerNameAddress+"', SellerCountry='"+SellerCountry+"', "+
                        "  BrokerNameAddress='" + BrokerNameAddress + "', BrokerCountry='" + BrokerCountry + "',ModifiedBy='" + modifiedBy + "', ModifiedDate='" + modifiedDate + "',ConsignorName='" + ConsignorName + "',SellerName='" + SellerName + "',BrokerName='" + BrokerName + "'  where JobNo='" + JobNo + "' and InvoiceNo='" + InvoiceNo + "' ";

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

      
        public int UpdateRelationSVBDetails(string BuyerSeller, string Relation, string Base, string Condition, string SVB, string SVBRefNo, string SVBRefDate, string CustomHouse, string LoadingOn,
            double AssableLoadingRate, string AssableStatus, double DutyLoadingRate, string DutyStatus, string JobNo, string InvoiceNo, string modifiedBy, DateTime modifiedDate)
        {

            SqlConnection sqlConn = new SqlConnection(strconn);
            int result = new int();

            try
            {
                
                string query = "update T_invoiceDetails set  BuyerSeller='"+BuyerSeller+"', Relation='"+Relation+"', Base='"+Base+"', Condition='"+Condition+"',"+
                    " SVB='"+SVB+"', SVBRefNo='"+SVBRefNo+"', SVBRefDate='"+SVBRefDate+"', CustomHouse='"+CustomHouse+"', LoadingOn='"+LoadingOn+"', AssableLoadingRate='"+AssableLoadingRate+"',"+
                        " AssableStatus='" + AssableStatus + "', DutyLoadingRate='" + DutyLoadingRate + "', DutyStatus='" + DutyStatus + "'," +
                    " ModifiedBy='" + modifiedBy + "', ModifiedDate='" + modifiedDate + "'  where JobNo='" + JobNo + "' and InvoiceNo='" + InvoiceNo + "' ";

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

       
        public int UpdateOtherDetails(string ContractNo, string ContractDate, string LCNo, string LCDate, string ValuationMethod,string NoofProduct,bool SinglePO,string PONo,string PODate ,string JobNo, string InvoiceNo, string modifiedBy, DateTime modifiedDate)
        {

            SqlConnection sqlConn = new SqlConnection(strconn);
            int result = new int();

            try
            {
                string query = "update T_invoiceDetails set ContractNo='" + ContractNo + "', ContractDate='" + ContractDate + "', LCNo='" + LCNo + "', LCDate='" + LCDate + "', "+
                    " ValuationMethod='" + ValuationMethod + "',NoofProduct='" + NoofProduct + "',SinglePO='" + SinglePO + "',PONo='" + PONo + "',PODate='"+PODate+"',  " +
                    " ModifiedBy='" + modifiedBy + "', ModifiedDate='" + modifiedDate + "'  where JobNo='" + JobNo + "' and InvoiceNo='" + InvoiceNo + "' ";

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

          
        public int UpdateMiscellaneousDetails(string MisCurrency, double MisExchRate,double MisRate,double MisAmount,string AgencyCurrency,double AgencyExchRate,
            double AgencyRate, double AgencyAmount,string LoadingCurrency,double LoadingExchRate,double LoadingRate,double LoadingAmount,string SaleCondition,string Remarks,
            string JobNo, string InvoiceNo, string modifiedBy, DateTime modifiedDate)
        {

            SqlConnection sqlConn = new SqlConnection(strconn);
            int result = new int();

            try
            {
                string query = "update T_invoiceDetails set MisCurrency='" + MisCurrency + "', MisExchRate='" + MisExchRate + "', MisRate='" + MisRate + "', MisAmount='" + MisAmount + "',MisINRAmount='" + MisExchRate * MisAmount + "', AgencyCurrency='" + AgencyCurrency + "', " +
                    " AgencyExchRate='" + AgencyExchRate + "', AgencyRate='" + AgencyRate + "', AgencyAmount='" + AgencyAmount + "',AgencyAmount='" + AgencyExchRate*AgencyAmount + "', LoadingCurrency='" + LoadingCurrency + "', LoadingExchRate='" + LoadingExchRate + "', LoadingRate='" + LoadingRate + "', " +
                    "  LoadingAmount='" + LoadingAmount + "', SaleCondition='" + SaleCondition + "', Remarks='" + Remarks + "', " +
                    " ModifiedBy='" + modifiedBy + "', ModifiedDate='" + modifiedDate + "'  where JobNo='" + JobNo + "' and InvoiceNo='" + InvoiceNo + "' ";

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


        public int InsertOthers(string ChargeType, string Currency, double ExchRate, double ChargeAmount,
            string JobNo, string InvoiceNo, string modifiedBy, DateTime modifiedDate, double AmountINR)
        {

            SqlConnection sqlConn = new SqlConnection(strconn);
            int result = new int();

            try
            {
                string query = "insert into T_InvoiceOtherCharges( JobNo, InvoiceNo, ChargeType, Currency, ExchRate, ChargeAmount, CreatedBy, CreatedDate, ModifiedBy,ModifiedDate,AmountINR)" +
                    " values('" + JobNo + "','" + InvoiceNo + "','" + ChargeType + "','" + Currency + "','" + ExchRate + "','" + ChargeAmount + "','" + modifiedBy + "','" + modifiedDate + "','" + modifiedBy + "','" + modifiedDate + "','" + AmountINR + "')";

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

        public int UpdateOthers(string InvoiceOtherChargesId, string ChargeType,string Currency,double ExchRate,double ChargeAmount,
            string JobNo, string InvoiceNo, string modifiedBy, DateTime modifiedDate, double AmountINR)
        {

            SqlConnection sqlConn = new SqlConnection(strconn);
            int result = new int();

            try
            {

                string query = "update T_InvoiceOtherCharges set ChargeType='" + ChargeType + "', Currency='" + Currency + "', ExchRate='" + ExchRate + "', ChargeAmount='" + ChargeAmount + "'," +
                    " ModifiedBy='" + modifiedBy + "', ModifiedDate='" + modifiedDate + "',AmountINR='" + AmountINR + "'  where InvoiceOtherChargesId='" + InvoiceOtherChargesId + "' ";

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

        public DataSet GetOtherCharges(string JobNo,string InvoiceNO)
        {
            DataSet ds = new DataSet();
            try
            {
                string Query = "Select  InvoiceOtherChargesId, ChargeType, Currency, ExchRate, ChargeAmount,AmountINR from T_InvoiceOtherCharges where jobno='"+JobNo+"' and InvoiceNo='"+InvoiceNO+"' ";

                SqlConnection sqlConn = new SqlConnection(strconn);
                sqlConn.Open();
                SqlDataAdapter da = new SqlDataAdapter(Query, sqlConn);

                da.Fill(ds, "Charges");
                sqlConn.Close();
            }
            catch (Exception ex)
            {
                string Msg = ex.Message.ToString();
            }
            return ds;
        }

        public DataSet GetSumOtherCharges(string JobNo, string InvoiceNO)
        {
            DataSet ds = new DataSet();
            try
            {
                string Query = "Select Sum(ChargeAmount) As ChargeAmount from T_InvoiceOtherCharges where jobno='" + JobNo + "' and InvoiceNo='" + InvoiceNO + "' ";

                SqlConnection sqlConn = new SqlConnection(strconn);
                sqlConn.Open();
                SqlDataAdapter da = new SqlDataAdapter(Query, sqlConn);
                da.Fill(ds, "Charges");
                sqlConn.Close();
            }
            catch (Exception ex)
            {
                string Msg = ex.Message.ToString();
            }
            return ds;
        }
        public DataSet GetInvoiceFIM(string JobNo)
        {
            DataSet ds = new DataSet();
            try
            {
                string Query = "SELECT TOP (1) InsuranceCurrency , FreightCurrency , MisCurrency ,FreightExchangeRate, InsuranceExchangeRate, MisExchRate, FreightTyExRate, FreightTyAmount, InsuranceTyExRate, InsuranceTyAmount, MiscellaneousTyExRate, MiscellaneousTyAmt FROM  T_InvoiceDetails where jobno='" + JobNo + "'";

                SqlConnection sqlConn = new SqlConnection(strconn);
                sqlConn.Open();
                SqlDataAdapter da = new SqlDataAdapter(Query, sqlConn);

                da.Fill(ds, "InvoiceFIM");
                sqlConn.Close();
            }
            catch (Exception ex)
            {
                string Msg = ex.Message.ToString();
            }
            return ds;
        }
        public DataSet GetInvoiceDtl(string JobNo, string InvoiceNO)
        {
            DataSet ds = new DataSet();
            try
            {
                string Query = "Select * from T_InvoiceDetails where jobno='" + JobNo + "' and InvoiceNo='" + InvoiceNO + "' ";

                SqlConnection sqlConn = new SqlConnection(strconn);
                sqlConn.Open();
                SqlDataAdapter da = new SqlDataAdapter(Query, sqlConn);

                da.Fill(ds, "InvoiceDtl");
                sqlConn.Close();
            }
            catch (Exception ex)
            {
                string Msg = ex.Message.ToString();
            }
            return ds;
        }
        public DataSet GetChargeType()
        {

            SqlConnection sqlConn = new SqlConnection(strconn);
            DataSet dsCharge = new DataSet();
            try
            {
                string ChargeQuery = "select charge_code,charge_desc,cCode from M_Charge";

                sqlConn.Open();
                SqlDataAdapter da = new SqlDataAdapter(ChargeQuery, sqlConn);

                da.Fill(dsCharge, "Invoice");
            }
            catch (Exception ex)
            {
                string Msg = ex.Message.ToString();
            }
            finally
            {
                sqlConn.Close();
            }
            return dsCharge;
        }
        public int DeleteInvoiceDetails(string InvoiceID, string InvNo, string JobNo)
        {
            SqlConnection sqlConn = new SqlConnection(strconn);
            int result = new int();
            try
            {
                string Query = "Delete  from  T_InvoiceDetails where InvoiceDetailsId='" + InvoiceID + "';Delete from T_Product where JobNo='" + JobNo + "' And InvoiceNo='" + InvNo + "'";
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

        public DataSet GetJobDetails(string JobNo)
        {
            DataSet ds = new DataSet();
            try
            {
                string Query = "Select JobNo,Consignor,ConsignorAddress,ConsignorCountry from T_Importer where JobNo='" + JobNo + "' ";

                SqlConnection sqlConn = new SqlConnection(strconn);
                sqlConn.Open();
                SqlDataAdapter da = new SqlDataAdapter(Query, sqlConn);

                da.Fill(ds, "Consignor");
                sqlConn.Close();
            }
            catch (Exception ex)
            {
                string Msg = ex.Message.ToString();
            }
            return ds;
        }
        public DataSet GetSVBDetails(string Consignee, string Consignor)
        {
            DataSet ds = new DataSet();
            try
            {
                string Query = "SELECT BuyerSellerRelated,Relation,Base,Condition,SVBLoad,SVBRefOn,SVBRefDate,CustomHouse,LoadingOn,LoadingRateAssb,LoadingRateAssbStatus,LoadingRateDuty,LoadingRateDutyStatus FROM M_SVBRef Where ConsignorName='" + Consignor + "' And ConsigneeName='" + Consignee + "'";
                SqlConnection sqlConn = new SqlConnection(strconn);
                sqlConn.Open();
                SqlDataAdapter da = new SqlDataAdapter(Query, sqlConn);
                da.Fill(ds, "SVB");
                sqlConn.Close();
            }
            catch (Exception ex)
            {
                string Msg = ex.Message.ToString();
            }
            return ds;
        }

        public int DeleteOtherCharges(string TransId)
        {
            SqlConnection sqlConn = new SqlConnection(strconn);
            int result = new int();
            try
            {
                string Query = "Delete  from  T_InvoiceOtherCharges where InvoiceOtherChargesId='" + TransId + "'";
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

    }
}
