using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VTS.ImpexCube.Data;
using System.Data;
namespace VTS.ImpexCube.Business
{
    public class ETInvoiceBL
    {
        ETInvoiceDL ETInvoiceDL = new ETInvoiceDL();


        public int SaveInvoiceDetails(string JobNo, string InvoiceNo, string InvoiceDate, string TOI, string Currency, string CurrencyRate, string InvoiceValue, string ProductValue, string InvoiceINRAmount, string CreatedBy, string CreatedDate)
        {
            return ETInvoiceDL.SaveInvoiceDetails(JobNo, InvoiceNo, InvoiceDate, TOI, Currency, CurrencyRate, InvoiceValue, ProductValue, InvoiceINRAmount, CreatedBy, CreatedDate);        
        }

        public int UpdateInvoiceDetails(string ID, string JobNo, string InvoiceNo, string InvoiceDate, string TOI, string Currency, string CurrencyRate, string InvoiceValue, string ProductValue, string InvoiceINRAmount, string ModifiedBy, string ModifiedDate)
        {
            return ETInvoiceDL.UpdateInvoiceDetails(ID, JobNo, InvoiceNo, InvoiceDate, TOI, Currency, CurrencyRate, InvoiceValue, ProductValue, InvoiceINRAmount, ModifiedBy, ModifiedDate);
        }

        public int Update_Invoice_Freigth_Insurance(string JobNo,string InvoiceNo, string UnitPriceIncludes, string ShowFOBIn, string FreightCurrency, string FreightExRate, string FreightRate, string FreightAmount, string FreightINRAmount,
                                                    string InsuranceCurrency, string InsuranceExRate, string InsuranceRate, string InsuranceAmount, string InsuranceINRAmount,
                                                    string DiscountCurrency, string DiscountExRate, string DiscountRate, string DiscountAmount, string DiscountINRAmount,
                                                    string CommissionCurrency, string CommissionExRate, string CommissionRate, string CommissionAmount, string CommissionINRAmount,
                                                    string OtherDeductionCurrency, string OtherDeductionExRate, string OtherDeductionRate, string OtherDeductionAmount, string OtherDeductionINRAmount,
                                                    string PackingFOBChargesCurrency, string PackingFOBChargesExRate, string PackingFOBChargesRate, string ModifiedBy, string ModifiedDate)
        {
            return ETInvoiceDL.Update_Invoice_Freigth_Insurance(JobNo, InvoiceNo, UnitPriceIncludes, ShowFOBIn, FreightCurrency, FreightExRate, FreightRate, FreightAmount,FreightINRAmount,
                                                     InsuranceCurrency, InsuranceExRate, InsuranceRate, InsuranceAmount,InsuranceINRAmount,
                                                     DiscountCurrency, DiscountExRate, DiscountRate, DiscountAmount,DiscountINRAmount,
                                                     CommissionCurrency, CommissionExRate, CommissionRate, CommissionAmount,CommissionINRAmount,
                                                     OtherDeductionCurrency, OtherDeductionExRate, OtherDeductionRate, OtherDeductionAmount,OtherDeductionINRAmount,
                                                     PackingFOBChargesCurrency, PackingFOBChargesExRate, PackingFOBChargesRate, ModifiedBy, ModifiedDate);
        }

        public int UpdateOtherInfoInvoice(string JobNo, string InvoiceNo, string ExportContractNo, string ExportContractDate, string NatureOfPayment, string PaymentPeriod, string ModifiedBy, string ModifiedDate)
        {
            return ETInvoiceDL.UpdateOtherInfoInvoice(JobNo,InvoiceNo,ExportContractNo, ExportContractDate, NatureOfPayment, PaymentPeriod, ModifiedBy, ModifiedDate);
        }

        public DataSet GetInvoiceDetails(string JobNo,string InvoiceNo)
        {
            return ETInvoiceDL.GetInvoiceDetails(JobNo, InvoiceNo);
        }

        public DataSet SelectAnnexure(string JobNo, string InvoiceNo)
        {
            return ETInvoiceDL.SelectAnnexure(JobNo, InvoiceNo);
        }

        public DataSet GetInvoiceJobDetails(string JobNo)
        {
            return ETInvoiceDL.GetInvoiceJobDetails(JobNo);
        }

        public int UpdateAnnexure(string JobNo, string InvoiceNo, string IECodeOfEOU, string BranchSNo, string ExaminationDate,
                string ExaminingOfficer, string ExaminingOfficerDesignation, string SupervisingOfficer,
                string SupervisingOfficerDesignation, string Commissionerate, string Division, string Range,
                string VerifiedbyExaminingOfficer, string SampleForwarded, string SealNumber)
        {
            return ETInvoiceDL.UpdateAnnexure(JobNo, InvoiceNo, IECodeOfEOU, BranchSNo, ExaminationDate, ExaminingOfficer, ExaminingOfficerDesignation,
                SupervisingOfficer, SupervisingOfficerDesignation, Commissionerate, Division, Range, VerifiedbyExaminingOfficer, SampleForwarded, SealNumber);
        }

        //public void SaveInvoice(string JobNo, string InvoiceNo, string InvoiceDate, string TOI, string Currency, string CurrencyRate, string InvoiceValue, string ProductValue, string UnitPriceIncludes,
        //                        string ShowFOBIn, string FreightCurrency, string FreightExRate, string FreightRate, string FreightAmount, string InsuranceCurrency, string InsuranceExRate,
        //                        string InsuranceRate, string InsuranceAmount, string DiscountCurrency, string DiscountExRate, string DiscountRate, string DiscountAmount,
        //                        string CommissionCurrency, string CommissionExRate, string CommissionRate, string CommissionAmount, string OtherDeductionCurrency, string OtherDeductionExRate,
        //                        string OtherDeductionRate, string OtherDeductionAmount, string PackingFOBChargesCurrency, string PackingFOBChargesExRate, string PackingFOBChargesRate, string ExportContractNo,
        //                        string ExportContractDate, string NatureOfPayment, string PaymentPeriod, string CreatedBy, string CreatedDate, string ModifiedBy, string ModifiedDate)
        //{
        //    ETInvoiceDL.SaveInvoice(JobNo, InvoiceNo, InvoiceDate, TOI, Currency, CurrencyRate, InvoiceValue, ProductValue, UnitPriceIncludes,
        //                         ShowFOBIn, FreightCurrency, FreightExRate, FreightRate, FreightAmount, InsuranceCurrency, InsuranceExRate,
        //                         InsuranceRate, InsuranceAmount, DiscountCurrency, DiscountExRate, DiscountRate, DiscountAmount,
        //                         CommissionCurrency, CommissionExRate, CommissionRate, CommissionAmount, OtherDeductionCurrency, OtherDeductionExRate,
        //                         OtherDeductionRate, OtherDeductionAmount, PackingFOBChargesCurrency, PackingFOBChargesExRate, PackingFOBChargesRate, ExportContractNo,
        //                         ExportContractDate, NatureOfPayment, PaymentPeriod, CreatedBy, CreatedDate, ModifiedBy, ModifiedDate);
        //}

        //public void SaveUpdate(string JobNo, string InvoiceNo, string InvoiceDate, string TOI, string Currency, string CurrencyRate, string InvoiceValue, string ProductValue, string UnitPriceIncludes,
        //                       string ShowFOBIn, string FreightCurrency, string FreightExRate, string FreightRate, string FreightAmount, string InsuranceCurrency, string InsuranceExRate,
        //                       string InsuranceRate, string InsuranceAmount, string DiscountCurrency, string DiscountExRate, string DiscountRate, string DiscountAmount,
        //                       string CommissionCurrency, string CommissionExRate, string CommissionRate, string CommissionAmount, string OtherDeductionCurrency, string OtherDeductionExRate,
        //                       string OtherDeductionRate, string OtherDeductionAmount, string PackingFOBChargesCurrency, string PackingFOBChargesExRate, string PackingFOBChargesRate, string ExportContractNo,
        //                       string ExportContractDate, string NatureOfPayment, string PaymentPeriod, string ModifiedBy, string ModifiedDate)
        //{
        //    ETInvoiceDL.SaveUpdate(JobNo, InvoiceNo, InvoiceDate, TOI, Currency, CurrencyRate, InvoiceValue, ProductValue, UnitPriceIncludes,
        //                         ShowFOBIn, FreightCurrency, FreightExRate, FreightRate, FreightAmount, InsuranceCurrency, InsuranceExRate,
        //                         InsuranceRate, InsuranceAmount, DiscountCurrency, DiscountExRate, DiscountRate, DiscountAmount,
        //                         CommissionCurrency, CommissionExRate, CommissionRate, CommissionAmount, OtherDeductionCurrency, OtherDeductionExRate,
        //                         OtherDeductionRate, OtherDeductionAmount, PackingFOBChargesCurrency, PackingFOBChargesExRate, PackingFOBChargesRate, ExportContractNo,
        //                         ExportContractDate, NatureOfPayment, PaymentPeriod, ModifiedBy, ModifiedDate);
        //}
    }
}
