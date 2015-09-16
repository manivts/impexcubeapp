using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
namespace VTS.ImpexCube.Data
{
    public class ETInvoiceDL
    {
        CommonDL CommonDL = new CommonDL();

        public int SaveInvoiceDetails(string JobNo, string InvoiceNo, string InvoiceDate, string TOI, string Currency, string CurrencyRate, string InvoiceValue, string ProductValue, string InvoiceINRAmount, string CreatedBy, string CreatedDate)
        {
            string Message = string.Empty;
            int Result = 0;
            StringBuilder Query = new StringBuilder();
            try
            {
                Query.Append("INSERT INTO [E_T_Invoice] ");
                Query.Append("([JobNo],[InvoiceNo],[InvoiceDate],[TOI],[Currency],[CurrencyRate],[InvoiceValue],[ProductValue],[InvoiceINRAmount],[CreatedBy],[CreatedDate])");
                Query.Append("VALUES");
                Query.Append("('" + JobNo + "','" + InvoiceNo + "','" + InvoiceDate + "','" + TOI + "','" + Currency + "','" + CurrencyRate + "','" + InvoiceValue + "','" + ProductValue + "','" + InvoiceINRAmount + "','" + CreatedBy + "','" + CreatedDate + "')");

                Result = CommonDL.ExecuteNonQuery(Query.ToString());
            }
            catch (Exception ex)
            {
                string mSG = ex.Message;
            }
            return Result;
        }

        public int UpdateInvoiceDetails(string ID, string JobNo, string InvoiceNo, string InvoiceDate, string TOI, string Currency, string CurrencyRate, string InvoiceValue, string ProductValue, string InvoiceINRAmount, string ModifiedBy, string ModifiedDate)
        {
            string Message = string.Empty;
            StringBuilder Query = new StringBuilder();
            int Result = 0;
            try
            {
                Query.Append(" UPDATE [E_T_Invoice] SET ");
                Query.Append(" [InvoiceNo] = '" + InvoiceNo + "',[InvoiceDate] = '" + InvoiceDate + "',[TOI] = '" + TOI + "',[Currency] = '" + Currency + "',[CurrencyRate] = '" + CurrencyRate + "',[InvoiceValue] = '" + InvoiceValue + "',[ProductValue] = '" + ProductValue + "', [InvoiceINRAmount] = '" + InvoiceINRAmount + "' ,[ModifiedBy] = '" + ModifiedBy + "',[ModifiedDate] = '" + ModifiedDate + "' ");
                Query.Append(" where  [JobNo]='" + JobNo + "' and ID = '" + ID + "'");
                Result = CommonDL.ExecuteNonQuery(Query.ToString());
            }
            catch (Exception ex)
            {
                string mSG = ex.Message;
            }
            return Result;
        }

        public int Update_Invoice_Freigth_Insurance(string JobNo,string InvoiceNo, string UnitPriceIncludes, string ShowFOBIn, string FreightCurrency, string FreightExRate, 
                                                     string FreightRate, string FreightAmount, string FreightINRAmount,
                                                     string InsuranceCurrency, string InsuranceExRate, string InsuranceRate, string InsuranceAmount, string InsuranceINRAmount,
                                                     string DiscountCurrency, string DiscountExRate, string DiscountRate, string DiscountAmount, string DiscountINRAmount,
                                                     string CommissionCurrency, string CommissionExRate, string CommissionRate, string CommissionAmount, string CommissionINRAmount,
                                                     string OtherDeductionCurrency,string OtherDeductionExRate, string OtherDeductionRate,string OtherDeductionAmount, string OtherDeductionINRAmount,
                                                     string PackingFOBChargesCurrency,string PackingFOBChargesExRate,string PackingFOBChargesRate,string ModifiedBy,string ModifiedDate)
        {
            string Message = string.Empty;
            int Result = 0;
            StringBuilder Query = new StringBuilder();
            try
            {
                Query.Append(" UPDATE [E_T_Invoice] SET ");                
                Query.Append("[UnitPriceIncludes] = '" + UnitPriceIncludes + "',[ShowFOBIn] = '" + ShowFOBIn + "',[FreightCurrency] = '" + FreightCurrency + "',[FreightExRate] = '" + FreightExRate + "',");
                Query.Append("[FreightRate] = '" + FreightRate + "',[FreightAmount] = '" + FreightAmount + "',[FreightINRAmount]='" + FreightINRAmount + "',[InsuranceCurrency] = '" + InsuranceCurrency + "',[InsuranceExRate] = '" + InsuranceExRate + "',[InsuranceRate] = '" + InsuranceRate + "',");
                Query.Append("[InsuranceAmount] = '" + InsuranceAmount + "', [InsuranceINRAmount]='" + InsuranceINRAmount + "',[DiscountCurrency] = '" + DiscountCurrency + "',[DiscountExRate] = '" + DiscountExRate + "',[DiscountRate] = '" + DiscountRate + "',[DiscountAmount] = '" + DiscountAmount + "',[DiscountINRAmount]='" + DiscountINRAmount + "',");
                Query.Append("[CommissionCurrency] = '" + CommissionCurrency + "',[CommissionExRate] = '" + CommissionExRate + "',[CommissionRate] = '" + CommissionRate + "',[CommissionAmount] = '" + CommissionAmount + "', [CommissionINRAmount] = '" + CommissionINRAmount + "',[OtherDeductionCurrency] = '" + OtherDeductionCurrency + "',");
                Query.Append("[OtherDeductionExRate] = '" + OtherDeductionExRate + "',[OtherDeductionRate] = '" + OtherDeductionRate + "',[OtherDeductionAmount] = '" + OtherDeductionAmount + "', [OtherDeductionINRAmount] = '" + OtherDeductionINRAmount + "' ,[PackingFOBChargesCurrency] = '" + PackingFOBChargesCurrency + "',[PackingFOBChargesExRate] = '" + PackingFOBChargesExRate + "',");
                Query.Append("[PackingFOBChargesRate] = '" + PackingFOBChargesRate + "',[ModifiedBy] = '" + ModifiedBy + "',[ModifiedDate] = '" + ModifiedDate + "' ");
                Query.Append(" where  [JobNo]='" + JobNo + "' and InvoiceNo='" + InvoiceNo + "'");

                Result = CommonDL.ExecuteNonQuery(Query.ToString());
            }
            catch (Exception ex)
            {
                string mSG = ex.Message;
            }
            return Result;
        }


        public int UpdateOtherInfoInvoice(string JobNo,string InvoiceNo, string ExportContractNo, string ExportContractDate, string NatureOfPayment, string PaymentPeriod, string ModifiedBy, string ModifiedDate)                                
        {
            string Message = string.Empty;
            int Result = 0;
            StringBuilder Query = new StringBuilder();
            try
            {
                Query.Append(" UPDATE [E_T_Invoice] SET ");
                Query.Append(" [ExportContractNo] = '" + ExportContractNo + "',[ExportContractDate] = '" + ExportContractDate + "',[NatureOfPayment] = '" + NatureOfPayment + "',");
                Query.Append("[PaymentPeriod] = '" + PaymentPeriod + "',[ModifiedBy] = '" + ModifiedBy + "',[ModifiedDate] = '" + ModifiedDate + "' ");
                Query.Append(" where  [JobNo]='" + JobNo + "' and InvoiceNo='" + InvoiceNo + "' ");
                Result = CommonDL.ExecuteNonQuery(Query.ToString());
            }
            catch (Exception ex)
            {
                string mSG = ex.Message;
            }
            return Result;
        }

        public DataSet GetInvoiceDetails(string JobNo,string InvoiceNo)
        {
            StringBuilder Query = new StringBuilder();
            string Message = string.Empty;
            
            DataSet ds = new DataSet();
            try
            {
                Query.Append("Select ");
                Query.Append("[ID],[JobNo],[InvoiceNo],[InvoiceDate],[TOI],[Currency],[CurrencyRate],[InvoiceValue],[ProductValue],[InvoiceINRAmount],[UnitPriceIncludes],[ShowFOBIn],[FreightCurrency],[FreightExRate],");
                Query.Append("[FreightRate],[FreightAmount],[FreightINRAmount],[InsuranceCurrency],[InsuranceExRate],[InsuranceRate],[InsuranceAmount],[InsuranceINRAmount],[DiscountCurrency],[DiscountExRate],");
                Query.Append("[DiscountRate],[DiscountAmount],[DiscountINRAmount],[CommissionCurrency],[CommissionExRate],[CommissionRate],[CommissionAmount],[CommissionINRAmount],[OtherDeductionCurrency],[OtherDeductionExRate],");
                Query.Append("[OtherDeductionRate],[OtherDeductionAmount],[OtherDeductionINRAmount],[PackingFOBChargesCurrency],[PackingFOBChargesExRate],[PackingFOBChargesRate],[ExportContractNo],[ExportContractDate],");
                Query.Append("[NatureOfPayment],[PaymentPeriod],[CreatedBy],[CreatedDate],[ModifiedBy],[ModifiedDate] ");
                Query.Append(" FROM [E_T_Invoice]  where JobNo='" + JobNo + "' and InvoiceNo='" + InvoiceNo + "'");

                ds = CommonDL.GetDataSet(Query.ToString());
            }
            catch (Exception ex)
            {
                Message = ex.Message;
            }
            return ds;
        }

        public DataSet GetInvoiceJobDetails(string JobNo)
        {
            StringBuilder Query = new StringBuilder();
            string Message = string.Empty;

            DataSet ds = new DataSet();
            try
            {
                Query.Append("Select ");
                Query.Append("[ID],[JobNo],[InvoiceNo],[InvoiceDate],[TOI],[Currency],[CurrencyRate],[InvoiceValue],[ProductValue],[InvoiceINRAmount],[UnitPriceIncludes],[ShowFOBIn],[FreightCurrency],[FreightExRate],");
                Query.Append("[FreightRate],[FreightAmount],[FreightINRAmount],[InsuranceCurrency],[InsuranceExRate],[InsuranceRate],[InsuranceAmount],[InsuranceINRAmount],[DiscountCurrency],[DiscountExRate],");
                Query.Append("[DiscountRate],[DiscountAmount],[DiscountINRAmount],[CommissionCurrency],[CommissionExRate],[CommissionRate],[CommissionAmount],[CommissionINRAmount],[OtherDeductionCurrency],[OtherDeductionExRate],");
                Query.Append("[OtherDeductionRate],[OtherDeductionAmount],[OtherDeductionINRAmount],[PackingFOBChargesCurrency],[PackingFOBChargesExRate],[PackingFOBChargesRate],[ExportContractNo],[ExportContractDate],");
                Query.Append("[NatureOfPayment],[PaymentPeriod],[CreatedBy],[CreatedDate],[ModifiedBy],[ModifiedDate] ");
                Query.Append(" FROM [E_T_Invoice]  where JobNo='" + JobNo + "'");

                ds = CommonDL.GetDataSet(Query.ToString());
            }
            catch (Exception ex)
            {
                Message = ex.Message;
            }
            return ds;
        }

        public int UpdateAnnexure(string JobNo, string InvoiceNo, string IECodeOfEOU, string BranchSNo, string ExaminationDate,
                string ExaminingOfficer, string ExaminingOfficerDesignation, string SupervisingOfficer,
                string SupervisingOfficerDesignation, string Commissionerate, string Division, string Range,
                string VerifiedbyExaminingOfficer, string SampleForwarded, string SealNumber)
        {
            string Message = string.Empty;
            int Result = 0;          
            StringBuilder Query = new StringBuilder();
            try
            {
                Query.Append(" UPDATE [E_T_Invoice] SET ");
                Query.Append(" [IECodeOfEOU] = '" + IECodeOfEOU + "',[BranchSNo] = '" + BranchSNo + "',[ExaminationDate] = '" + ExaminationDate + "',[ExaminingOfficer] = '" + ExaminingOfficer + "',");
                Query.Append(" [ExaminingOfficerDesignation] = '" + ExaminingOfficerDesignation + "',[SupervisingOfficer] = '" + SupervisingOfficer + "',[SupervisingOfficerDesignation] = '" + SupervisingOfficerDesignation + "',");
                Query.Append(" [Commissionerate] = '" + Commissionerate + "',[Division] = '" + Division + "', [Range] = '" + Range + "',[VerifiedbyExaminingOfficer] = '" + VerifiedbyExaminingOfficer + "',");
                Query.Append(" [SampleForwarded] = '" + SampleForwarded + "',[SealNumber] = '" + SealNumber + "' where [JobNo] = '" + JobNo + "' and [InvoiceNo] = '" + InvoiceNo + "'");
                Result = CommonDL.ExecuteNonQuery(Query.ToString());
            }
            catch (Exception ex)
            {
                string mSG = ex.Message;
            }
            return Result;
        }

        public DataSet SelectAnnexure(string JobNo, string InvoiceNo)
        {
            StringBuilder Query = new StringBuilder();
            string Message = string.Empty;

            DataSet ds = new DataSet();
            try
            {
                Query.Append(" Select [IECodeOfEOU],[BranchSNo],[ExaminationDate],[ExaminingOfficer],[ExaminingOfficerDesignation],");
                Query.Append(" [SupervisingOfficer],[SupervisingOfficerDesignation],[Commissionerate],[Division],[Range],[VerifiedbyExaminingOfficer],");
                Query.Append(" [SampleForwarded],[SealNumber] from [E_T_Invoice]  where [JobNo] = '" + JobNo + "' and [InvoiceNo] = '" + InvoiceNo + "' ");
                ds = CommonDL.GetDataSet(Query.ToString());
            }
            catch (Exception ex)
            {
                string mSG = ex.Message;
            }
            return ds;
        }

        
        //public void SaveInvoice(string JobNo,string InvoiceNo,string InvoiceDate,string TOI,string Currency,string CurrencyRate,string InvoiceValue,string ProductValue,string UnitPriceIncludes,
        //                        string ShowFOBIn,string FreightCurrency,string FreightExRate,string FreightRate,string FreightAmount,string InsuranceCurrency,string InsuranceExRate,
        //                        string InsuranceRate,string InsuranceAmount,string DiscountCurrency,string DiscountExRate,string DiscountRate,string DiscountAmount,
        //                        string CommissionCurrency,string CommissionExRate,string CommissionRate,string CommissionAmount,string OtherDeductionCurrency,string OtherDeductionExRate,
        //                        string OtherDeductionRate,string OtherDeductionAmount,string PackingFOBChargesCurrency,string PackingFOBChargesExRate,string PackingFOBChargesRate,string ExportContractNo,
        //                        string ExportContractDate,string NatureOfPayment,string PaymentPeriod,string CreatedBy,string CreatedDate,string ModifiedBy,string ModifiedDate)
        //{
        //    string Message = string.Empty;
        //    StringBuilder Query = new StringBuilder();
        //    try
        //    {
        //        Query.Append("INSERT INTO [E_T_Invoice]");
        //        Query.Append("([JobNo],[InvoiceNo],[InvoiceDate],[TOI],[Currency],[CurrencyRate],[InvoiceValue],[ProductValue],[UnitPriceIncludes],[ShowFOBIn],[FreightCurrency],[FreightExRate],");
        //        Query.Append("[FreightRate],[FreightAmount],[InsuranceCurrency],[InsuranceExRate],[InsuranceRate],[InsuranceAmount],[DiscountCurrency],[DiscountExRate],");
        //        Query.Append("[DiscountRate],[DiscountAmount],[CommissionCurrency],[CommissionExRate],[CommissionRate],[CommissionAmount],[OtherDeductionCurrency],[OtherDeductionExRate],");
        //        Query.Append("[OtherDeductionRate],[OtherDeductionAmount],[PackingFOBChargesCurrency],[PackingFOBChargesExRate],[PackingFOBChargesRate],[ExportContractNo],");
        //        Query.Append("[ExportContractDate],[NatureOfPayment],[PaymentPeriod],[CreatedBy],[CreatedDate])");
        //        Query.Append("VALUES");
        //        Query.Append("('" + JobNo + "','" + InvoiceNo + "','" + InvoiceDate + "','" + TOI + "','" + Currency + "','" + CurrencyRate + "','" + InvoiceValue + "','" + ProductValue + "','" + UnitPriceIncludes + "','" + ShowFOBIn + "','" + FreightCurrency + "','" + FreightExRate + "',");
        //        Query.Append("'" + FreightRate + "','" + FreightAmount + "','" + InsuranceCurrency + "','" + InsuranceExRate + "','" + InsuranceRate + "','" + InsuranceAmount + "','" + DiscountCurrency + "','" + DiscountExRate + "',");
        //        Query.Append("'" + DiscountRate + "','" + DiscountAmount + "','" + CommissionCurrency + "','" + CommissionExRate + "','" + CommissionRate + "','" + CommissionAmount + "','" + OtherDeductionCurrency + "','" + OtherDeductionExRate + "',");
        //        Query.Append("'" + OtherDeductionRate + "','" + OtherDeductionAmount + "','" + PackingFOBChargesCurrency + "','" + PackingFOBChargesExRate + "','" + PackingFOBChargesRate + "','" + ExportContractNo + "',");
        //        Query.Append("'" + ExportContractDate + "','" + NatureOfPayment + "','" + PaymentPeriod + "','" + CreatedBy + "','" + CreatedDate + "')");
        //        CommonDL.ExecuteNonQuery(Query.ToString());
              
        //    }
        //    catch (Exception ex)
        //    {
        //        string mSG = ex.Message;
        //    }
        //}

        //public void SaveUpdate(string JobNo, string InvoiceNo, string InvoiceDate, string TOI, string Currency, string CurrencyRate, string InvoiceValue, string ProductValue, string UnitPriceIncludes,
        //                        string ShowFOBIn, string FreightCurrency, string FreightExRate, string FreightRate, string FreightAmount, string InsuranceCurrency, string InsuranceExRate,
        //                        string InsuranceRate, string InsuranceAmount, string DiscountCurrency, string DiscountExRate, string DiscountRate, string DiscountAmount,
        //                        string CommissionCurrency, string CommissionExRate, string CommissionRate, string CommissionAmount, string OtherDeductionCurrency, string OtherDeductionExRate,
        //                        string OtherDeductionRate, string OtherDeductionAmount, string PackingFOBChargesCurrency, string PackingFOBChargesExRate, string PackingFOBChargesRate, string ExportContractNo,
        //                        string ExportContractDate, string NatureOfPayment, string PaymentPeriod, string ModifiedBy, string ModifiedDate)
        //{
        //    string Message = string.Empty;
        //    StringBuilder Query = new StringBuilder();
        //    try
        //    {
        //        Query.Append(" UPDATE [E_T_Invoice] SET ");
        //        Query.Append(" [JobNo] = '" + JobNo + "',[InvoiceNo] = '" + InvoiceNo + "',[InvoiceDate] = '" + InvoiceDate + "',[TOI] = '" + TOI + "',[Currency] = '" + Currency + "',[CurrencyRate] = '" + CurrencyRate + "',[InvoiceValue] = '" + InvoiceValue + "',");
        //        Query.Append("[ProductValue] = '" + ProductValue + "',[UnitPriceIncludes] = '" + UnitPriceIncludes + "',[ShowFOBIn] = '" + ShowFOBIn + "',[FreightCurrency] = '" + FreightCurrency + "',[FreightExRate] = '" + FreightExRate + "',");
        //        Query.Append("[FreightRate] = '" + FreightRate + "',[FreightAmount] = '" + FreightAmount + "',[InsuranceCurrency] = '" + InsuranceCurrency + "',[InsuranceExRate] = '" + InsuranceExRate + "',[InsuranceRate] = '" + InsuranceRate + "',");
        //        Query.Append("[InsuranceAmount] = '" + InsuranceAmount + "',[DiscountCurrency] = '" + DiscountCurrency + "',[DiscountExRate] = '" + DiscountExRate + "',[DiscountRate] = '" + DiscountRate + "',[DiscountAmount] = '" + DiscountAmount + "',");
        //        Query.Append("[CommissionCurrency] = '" + CommissionCurrency + "',[CommissionExRate] = '" + CommissionExRate + "',[CommissionRate] = '" + CommissionRate + "',[CommissionAmount] = '" + CommissionAmount + "',[OtherDeductionCurrency] = '" + OtherDeductionCurrency + "',");
        //        Query.Append("[OtherDeductionExRate] = '" + OtherDeductionExRate + "',[OtherDeductionRate] = '" + OtherDeductionRate + "',[OtherDeductionAmount] = '" + OtherDeductionAmount + "',[PackingFOBChargesCurrency] = '" + PackingFOBChargesCurrency + "',[PackingFOBChargesExRate] = '" + PackingFOBChargesExRate + "',");
        //        Query.Append("[PackingFOBChargesRate] = '" + PackingFOBChargesRate + "',[ExportContractNo] = '" + ExportContractNo + "',[ExportContractDate] = '" + ExportContractDate + "',[NatureOfPayment] = '" + NatureOfPayment + "',");
        //        Query.Append("[PaymentPeriod] = '" + PaymentPeriod + "',[ModifiedBy] = '" + ModifiedBy + "',[ModifiedDate] = '" + ModifiedDate + "'");

        //        CommonDL.ExecuteNonQuery(Query.ToString());
        //    }
        //    catch (Exception ex)
        //    {
        //        string mSG = ex.Message;
        //    }
        //}
    }
}
