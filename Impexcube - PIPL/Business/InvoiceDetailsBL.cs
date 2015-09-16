// -----------------------------------------------------------------------
// <copyright file="InvoiceDetailsBL.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace VTS.ImpexCube.Business
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class InvoiceDetailsBL
    {
        VTS.ImpexCube.Data.InvoiceDetailsDL invDL = new VTS.ImpexCube.Data.InvoiceDetailsDL();

        public DataSet GetInvoiceDetails(string JobNo)
        {
            return invDL.GetInvoiceDetails(JobNo);
        }

        public DataSet GetJobNo()
        {
            return invDL.GetJobNo();
        }

        public DataSet GetCurrencyDetails()
        {
            return invDL.GetCurrencyDetails();
        }

        public DataSet GetCountryDetails()
        {
            return invDL.GetCountryDetails();
        }

        public DataSet GetInvoiceDetailsByNo(string invoiceNo)
        {
            return invDL.GetInvoiceDetailsByNo(invoiceNo);
        }

        public int InsertInvoiceDetails(
            string JobNo, 
            string invoiceNo, 
            string invoiceDate, 
            string Terms, 
            string FreightType, 
            string PaymentTerm, 
            string NatureofTrans,
            string currency, 
            string exchgrates, 
            string prodvalues, 
            //string FreightTy, 
            //string FreightTyCurrency, 
            //string FreightTyExRate, 
            //string FreightTyAmount, 
            //string FreightTyAmountINR,
            //string InsuranceType, 
            //string InsuranceTyCurrency, 
            //string InsuranceTyExRate, 
            //string InsuranceTyAmount, 
            //string InsuranceTyAmountINR,
            //string MiscellaneousType, string MiscellaneousTyCurrency, string MiscellaneousTyExRate, string MiscellaneousTyAmt, string MiscellaneousTyAmtINR,
            string createdBy, 
            DateTime createdDate, 
            string modifiedBy, 
            DateTime modifiedDate)
        {
            return invDL.InsertInvoiceDetails(
                JobNo, 
                invoiceNo,
                invoiceDate, 
                Terms, 
                FreightType, 
                PaymentTerm, 
                NatureofTrans,
                currency,
                exchgrates,
                prodvalues,
                //FreightTy,
                //FreightTyCurrency,  
                //FreightTyExRate,  
                //FreightTyAmount,  
                //FreightTyAmountINR,
                //InsuranceType,  
                //InsuranceTyCurrency,  
                //InsuranceTyExRate,  
                //InsuranceTyAmount,  
                //InsuranceTyAmountINR,
                //MiscellaneousType, MiscellaneousTyCurrency, MiscellaneousTyExRate, MiscellaneousTyAmt, MiscellaneousTyAmtINR,
                createdBy, 
                createdDate, 
                modifiedBy, 
                modifiedDate);
        }

        public int UpdateFreightDetails(string FreightCurrency, double FreightExchangeRate, double FreightRate, double FreightAmount, string InsuranceCurrency, double InsuranceExchangeRate,
        double InsuranceRate, double InsuranceAmount, string DiscountCurrency, double DiscountExchangeRate, double DiscountRate, double DiscountAmount,
            string MisCurrency, double MisExchRate, double MisRate, double MisAmount, string AgencyCurrency, double AgencyExchRate,
          double AgencyRate, double AgencyAmount, string LoadingCurrency, double LoadingExchRate, double LoadingRate, double LoadingAmount, string SaleCondition, string Remarks,
             string HighSeaCurrency, double HighSeaExRate, double HighSeaRate, double HighSeaAmt, double HighSeaAmtINR,
            string JobNo, string InvoiceNo, string modifiedBy, DateTime modifiedDate,
                   double FreightTyExRate,
double FreightTyAmount,

double InsuranceTyExRate,
double InsuranceTyAmount,
 double MiscellaneousTyExRate,
double MiscellaneousTyAmt)
        {
            return invDL.UpdateFreightDetails(FreightCurrency, FreightExchangeRate, FreightRate, FreightAmount, InsuranceCurrency, InsuranceExchangeRate, InsuranceRate, InsuranceAmount, DiscountCurrency, DiscountExchangeRate, DiscountRate, DiscountAmount,
                MisCurrency, MisExchRate, MisRate, MisAmount, AgencyCurrency, AgencyExchRate, AgencyRate, AgencyAmount, LoadingCurrency, LoadingExchRate, LoadingRate, LoadingAmount, SaleCondition, Remarks,
                  HighSeaCurrency, HighSeaExRate, HighSeaRate, HighSeaAmt, HighSeaAmtINR,
                JobNo, InvoiceNo, modifiedBy, modifiedDate,
                FreightTyExRate,
FreightTyAmount,
InsuranceTyExRate,
InsuranceTyAmount,
MiscellaneousTyExRate,
MiscellaneousTyAmt);
        }

        public int UpdateInvoiceDetails(string invoiceNo, string invoiceDate, string Terms, string FreightType, string PaymentTerm, string NatureofTrans, string currency, 
            string exchgrates, string prodvalues,string ProductINR,string modifiedBy, DateTime modifiedDate, string InvoiceID)
        {

            return invDL.UpdateInvoiceDetails(invoiceNo, invoiceDate, Terms, FreightType, PaymentTerm, NatureofTrans, currency, exchgrates, prodvalues,ProductINR,
                  modifiedBy, modifiedDate, InvoiceID);
        }

        public int UpdateConsignorDetails(string ConsignorNameAddress, string ConsignorCountry, string SellerNameAddress, string SellerCountry, string BrokerNameAddress, string BrokerCountry, string JobNo, string InvoiceNo, string modifiedBy, DateTime modifiedDate, string ConsignorName, string SellerName, string BrokerName)
        {
            return invDL.UpdateConsignorDetails(ConsignorNameAddress, ConsignorCountry,SellerNameAddress, SellerCountry, BrokerNameAddress, BrokerCountry,JobNo, InvoiceNo,modifiedBy,modifiedDate,ConsignorName,SellerName,BrokerName);
        }

        public int UpdateRelationSVBDetails(string BuyerSeller, string Relation, string Base, string Condition, string SVB, string SVBRefNo, string SVBRefDate, string CustomHouse, string LoadingOn,
           double AssableLoadingRate, string AssableStatus, double DutyLoadingRate, string DutyStatus, string JobNo, string InvoiceNo, string modifiedBy, DateTime modifiedDate)
        {
            return invDL.UpdateRelationSVBDetails(BuyerSeller, Relation, Base, Condition, SVB, SVBRefNo, SVBRefDate, CustomHouse, LoadingOn, AssableLoadingRate, AssableStatus, DutyLoadingRate, DutyStatus, JobNo, InvoiceNo, modifiedBy, modifiedDate);
        }

        public int UpdateOtherDetails(string ContractNo, string ContractDate, string LCNo, string LCDate, string ValuationMethod, string NoofProduct, bool SinglePO, string PONo, string PODate, string JobNo, string InvoiceNo, string modifiedBy, DateTime modifiedDate)
        {
            return invDL.UpdateOtherDetails(ContractNo, ContractDate, LCNo, LCDate, ValuationMethod,NoofProduct,SinglePO,PONo,PODate, JobNo, InvoiceNo, modifiedBy, modifiedDate);
        }

        public int UpdateMiscellaneousDetails(string MisCurrency, double MisExchRate, double MisRate, double MisAmount, string AgencyCurrency, double AgencyExchRate,
          double AgencyRate, double AgencyAmount, string LoadingCurrency, double LoadingExchRate, double LoadingRate, double LoadingAmount, string SaleCondition, string Remarks,
          string JobNo, string InvoiceNo, string modifiedBy, DateTime modifiedDate)
        {
            return invDL.UpdateMiscellaneousDetails(MisCurrency, MisExchRate, MisRate, MisAmount, AgencyCurrency, AgencyExchRate, AgencyRate, AgencyAmount, LoadingCurrency, LoadingExchRate, LoadingRate, LoadingAmount, SaleCondition, Remarks, JobNo, InvoiceNo, modifiedBy, modifiedDate);
        }

        public int UpdateOthers(string InvoiceOtherChargesId,string ChargeType, string Currency, double ExchRate, double ChargeAmount,
          string JobNo, string InvoiceNo, string modifiedBy, DateTime modifiedDate,double AmountINR)
        {
            return invDL.UpdateOthers(InvoiceOtherChargesId, ChargeType, Currency, ExchRate, ChargeAmount, JobNo, InvoiceNo, modifiedBy, modifiedDate, AmountINR);
        }



        public int InsertOthers(string ChargeType, string Currency, double ExchRate, double ChargeAmount,
            string JobNo, string InvoiceNo, string modifiedBy, DateTime modifiedDate,double AmountINR)
        {
            return invDL.InsertOthers(ChargeType, Currency, ExchRate, ChargeAmount, JobNo, InvoiceNo, modifiedBy, modifiedDate, AmountINR);
        }

        public DataSet GetOtherCharges(string JobNo, string InvoiceNO)
        {
            return invDL.GetOtherCharges(JobNo, InvoiceNO);
        }
        public DataSet GetSumOtherCharges(string JobNo, string InvoiceNO)
        {
            return invDL.GetSumOtherCharges(JobNo, InvoiceNO);
        }
        public DataSet GetInvoiceDtl(string JobNo, string InvoiceNO)
        {
            return invDL.GetInvoiceDtl(JobNo, InvoiceNO);
        }
        public DataSet GetInvoiceFIM(string JobNo)
        {
            return invDL.GetInvoiceFIM(JobNo);
        }

        public DataSet GetChargeType()
        {
            return invDL.GetChargeType();
        }
        public int DeleteInvoiceDetails(string ProductId,string  InvNo, string JobNo)
        {
            return invDL.DeleteInvoiceDetails(ProductId,InvNo,JobNo);
        }

        public DataSet GetJobDetails(string JobNo)
        {
            return invDL.GetJobDetails(JobNo);
        }
        public DataSet GetSVBDetails(string Consignee, string Consignor)
        {
            return invDL.GetSVBDetails(Consignee, Consignor);
        }

        public int DeleteOtherCharges(string TransId)
        {
            return invDL.DeleteOtherCharges(TransId);
        }
    }
}
