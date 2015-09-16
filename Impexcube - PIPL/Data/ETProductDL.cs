using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
namespace VTS.ImpexCube.Data
{

    public class ETProductDL
    {
        string Message = string.Empty;
        CommonDL CommonDL = new CommonDL();

        public int save(string JobNo, string InvoiceNo, string Description, string RITCCode, double Quantity, string QuantityUnit, double UnitPrice, string UnitPriceCurrency, string Per, string PerUnit, double Amount, string AmountCurrency, string EximCode, string
                      NFEICategory, double AlternateQty, string AlternateQtyUnit, string PMVCurrency, string PMVCalcMethod, double PMVCalcMethodRate, double PMVUnitRate, string PMVUnit, double TotalPMV, string TotalPMVUnit, string RewardItem, string
                      STRCode, string CessDuty, string ExportDutyNotn, double ExportDutyRate, double ExportDutyAmount, string ExportDutyUnit, double ExportDutyQty, string CessNotn, double CessRate, double CessAmount, string CessUnit, double
                      CessTariffValue, string CessTariffValueUnit, double CessQty, string CessDesc, string OthDutyCessNotn, double OthDutyCessRate, double OthDutyCessAmount, string OthDutyCessUnit, double OthDutyCessQty, string
                      OthDutyCessDesc, string ThirdCessNotn, double ThirdCessRate, double ThirdCessAmount, string ThirdCessUnit, double ThirdCessQty, string ThirdCessDesc, string CENVATCertiNo, string CENVATDate, string CENVATValidUpto, string
                      CENVATCExOffCode, string CENVATAssCode, string ReExportItem, string BENo, string BEDate, double QuantityExported, string QuantityExportedUnit, string InvoiceSNo, string ItemSNo, string TechnicalDetails, string
                      ImportPortCode, string BEItemDesc, string OtherIdenPara, string AgainstExpOblig, string ObligNo, double DrawbackAmtclaimed, double QuantityImported, string QuantityImportedUnit, string ItemUnUsed, string
                      CommissionerPermi, double AssessableValue, string BoardNo, string BoardDate, double TotalDutyPaid, string TotalDutyPaidDate, string MODVATAvailed, string MODVATReversed, string Accessories, string ThirdPartyEXP, string
                      Manufacturer, string IECode, string BranchSNo, string ThirdPartyEXPAddress, string ThirdPartyEXPAddress1, string CreatedBy, string CreatedDate)
        {
            StringBuilder Query = new StringBuilder();
            int Result = 0;
            try
            {
                Query.Append("INSERT INTO [E_T_Product]");
                Query.Append(" (JobNo, InvoiceNo, Description, RITCCode, Quantity, QuantityUnit, UnitPrice, UnitPriceCurrency, Per, PerUnit, Amount, AmountCurrency, EximCode, ");
                Query.Append(" NFEICategory, AlternateQty, AlternateQtyUnit, PMVCurrency, PMVCalcMethod, PMVCalcMethodRate, PMVUnitRate, PMVUnit, TotalPMV, TotalPMVUnit, RewardItem, ");
                Query.Append("STRCode, CessDuty, ExportDutyNotn, ExportDutyRate, ExportDutyAmount, ExportDutyUnit, ExportDutyQty, CessNotn, CessRate, CessAmount, CessUnit, ");
                Query.Append(" CessTariffValue, CessTariffValueUnit, CessQty, CessDesc, OthDutyCessNotn, OthDutyCessRate, OthDutyCessAmount, OthDutyCessUnit, OthDutyCessQty, ");
                Query.Append(" OthDutyCessDesc, ThirdCessNotn, ThirdCessRate, ThirdCessAmount, ThirdCessUnit, ThirdCessQty, ThirdCessDesc, CENVATCertiNo, CENVATDate, CENVATValidUpto,");
                Query.Append("CENVATCExOffCode, CENVATAssCode, ReExportItem, BENo, BEDate, QuantityExported, QuantityExportedUnit, InvoiceSNo, ItemSNo, TechnicalDetails, ");
                Query.Append(" ImportPortCode, BEItemDesc, OtherIdenPara, AgainstExpOblig, ObligNo, DrawbackAmtclaimed, QuantityImported, QuantityImportedUnit, ItemUnUsed, ");
                Query.Append("CommissionerPermi, AssessableValue, BoardNo, BoardDate, TotalDutyPaid, TotalDutyPaidDate, MODVATAvailed, MODVATReversed, Accessories, ThirdPartyEXP, ");
                Query.Append(" Manufacturer, IECode, BranchSNo, ThirdPartyEXPAddress, ThirdPartyEXPAddress1, CreatedBy, CreatedDate)");
                Query.Append("Values(");

                Query.Append("'" + JobNo + "','" + InvoiceNo + "','" + Description + "','" + RITCCode + "','" + Quantity + "','" + QuantityUnit + "','" + UnitPrice + "','" + UnitPriceCurrency + "','" + Per + "','" + PerUnit + "','" + Amount + "',");
                Query.Append("'" + AmountCurrency + "','" + EximCode + "','" + NFEICategory + "','" + AlternateQty + "','" + AlternateQtyUnit + "','" + PMVCurrency + "','" + PMVCalcMethod + "','" + PMVCalcMethodRate + "','" + PMVUnitRate + "',");
                Query.Append("'" + PMVUnit + "','" + TotalPMV + "','" + TotalPMVUnit + "','" + RewardItem + "','" + STRCode + "','" + CessDuty + "','" + ExportDutyNotn + "','" + ExportDutyRate + "','" + ExportDutyAmount + "','" + ExportDutyUnit + "',");
                Query.Append("'" + ExportDutyQty + "','" + CessNotn + "','" + CessRate + "','" + CessAmount + "','" + CessUnit + "','" + CessTariffValue + "','" + CessTariffValueUnit + "','" + CessQty + "','" + CessDesc + "','" + OthDutyCessNotn + "',");
                Query.Append("'" + OthDutyCessRate + "','" + OthDutyCessAmount + "','" + OthDutyCessUnit + "','" + OthDutyCessQty + "','" + OthDutyCessDesc + "','" + ThirdCessNotn + "','" + ThirdCessRate + "','" + ThirdCessAmount + "',");
                Query.Append("'" + ThirdCessUnit + "','" + ThirdCessQty + "','" + ThirdCessDesc + "','" + CENVATCertiNo + "','" + CENVATDate + "','" + CENVATValidUpto + "','" + CENVATCExOffCode + "','" + CENVATAssCode + "','" + ReExportItem + "',");
                Query.Append("'" + BENo + "','" + BEDate + "','" + QuantityExported + "','" + QuantityExportedUnit + "','" + InvoiceSNo + "','" + ItemSNo + "','" + TechnicalDetails + "','" + ImportPortCode + "','" + BEItemDesc + "',");
                Query.Append("'" + OtherIdenPara + "','" + AgainstExpOblig + "','" + ObligNo + "','" + DrawbackAmtclaimed + "','" + QuantityImported + "','" + QuantityImportedUnit + "','" + ItemUnUsed + "','" + CommissionerPermi + "',");
                Query.Append("'" + AssessableValue + "','" + BoardNo + "','" + BoardDate + "','" + TotalDutyPaid + "','" + TotalDutyPaidDate + "','" + MODVATAvailed + "','" + MODVATReversed + "','" + Accessories + "','" + ThirdPartyEXP + "',");
                Query.Append("'" + Manufacturer + "','" + IECode + "','" + BranchSNo + "','" + ThirdPartyEXPAddress + "','" + ThirdPartyEXPAddress1 + "','" + CreatedBy + "','" + CreatedDate + "')");

                Result = CommonDL.ExecuteNonQuery(Query.ToString());

            }
            catch (Exception e)
            {
                Message = e.Message;
            }
            return Result;
        }

        public int update(string JobNo, string InvoiceNo, string Description, string RITCCode, double Quantity, string QuantityUnit, double UnitPrice, string UnitPriceCurrency, string Per, string PerUnit, double Amount, string AmountCurrency, string EximCode, string
                      NFEICategory, double AlternateQty, string AlternateQtyUnit, string PMVCurrency, string PMVCalcMethod, double PMVCalcMethodRate, double PMVUnitRate, string PMVUnit, double TotalPMV, string TotalPMVUnit, string RewardItem, string
                      STRCode, string CessDuty, string ExportDutyNotn, double ExportDutyRate, double ExportDutyAmount, string ExportDutyUnit, double ExportDutyQty, string CessNotn, double CessRate, double CessAmount, string CessUnit, double
                      CessTariffValue, string CessTariffValueUnit, double CessQty, string CessDesc, string OthDutyCessNotn, double OthDutyCessRate, double OthDutyCessAmount, string OthDutyCessUnit, double OthDutyCessQty, string
                      OthDutyCessDesc, string ThirdCessNotn, double ThirdCessRate, double ThirdCessAmount, string ThirdCessUnit, double ThirdCessQty, string ThirdCessDesc, string CENVATCertiNo, string CENVATDate, string CENVATValidUpto, string
                      CENVATCExOffCode, string CENVATAssCode, string ReExportItem, string BENo, string BEDate, double QuantityExported, string QuantityExportedUnit, string InvoiceSNo, string ItemSNo, string TechnicalDetails, string
                      ImportPortCode, string BEItemDesc, string OtherIdenPara, string AgainstExpOblig, string ObligNo, double DrawbackAmtclaimed, double QuantityImported, string QuantityImportedUnit, string ItemUnUsed, string
                      CommissionerPermi, double AssessableValue, string BoardNo, string BoardDate, double TotalDutyPaid, string TotalDutyPaidDate, string MODVATAvailed, string MODVATReversed, string Accessories, string ThirdPartyEXP, string
                      Manufacturer, string IECode, string BranchSNo, string ThirdPartyEXPAddress, string ThirdPartyEXPAddress1, string ModifiedBy, string ModifiedDate)
        {
            StringBuilder Query = new StringBuilder();
            int Result = 0;
            try
            {
                Query.Append("UPDATE [E_T_Product] ");
                Query.Append("SET [JobNo] = '" + JobNo + "',[InvoiceNo] = '" + InvoiceNo + "',[Description] = '" + Description + "',[RITCCode] = '" + RITCCode + "',");
                Query.Append("[Quantity] = '" + Quantity + "',[QuantityUnit] = '" + QuantityUnit + "',[UnitPrice] = '" + UnitPrice + "',[UnitPriceCurrency] = '" + UnitPriceCurrency + "',");
                Query.Append(" [Per] = '" + Per + "',[PerUnit] = '" + PerUnit + "',[Amount] = '" + Amount + "',[AmountCurrency] = '" + AmountCurrency + "',[EximCode] = '" + EximCode + "',[NFEICategory] = '" + NFEICategory + "',");
                Query.Append("[AlternateQty] = '" + AlternateQty + "',[AlternateQtyUnit] = '" + AlternateQtyUnit + "',[PMVCurrency] = '" + PMVCurrency + "',[PMVCalcMethod] = '" + PMVCalcMethod + "',[PMVCalcMethodRate] = '" + PMVCalcMethodRate + "',");
                Query.Append(" [PMVUnitRate] = '" + PMVUnitRate + "',[PMVUnit] = '" + PMVUnit + "',[TotalPMV] = '" + TotalPMV + "',[TotalPMVUnit] = '" + TotalPMVUnit + "',[RewardItem] = '" + RewardItem + "',[STRCode] = '" + STRCode + "',[CessDuty] = '" + CessDuty + "',[ExportDutyNotn] = '" + ExportDutyNotn + "',");
                Query.Append("[ExportDutyRate] = '" + ExportDutyRate + "',[ExportDutyAmount] = '" + ExportDutyAmount + "',[ExportDutyUnit] = '" + ExportDutyUnit + "',");
                Query.Append("[ExportDutyQty] = '" + ExportDutyQty + "',[CessNotn] = '" + CessNotn + "',[CessRate] = '" + CessRate + "',[CessAmount] = '" + CessAmount + "',[CessUnit] = '" + CessUnit + "',[CessTariffValue] = '" + CessTariffValue + "',");
                Query.Append(" [CessTariffValueUnit] = '" + CessTariffValueUnit + "',[CessQty] = '" + CessQty + "',[CessDesc] = '" + CessDesc + "',[OthDutyCessNotn] = '" + OthDutyCessNotn + "',[OthDutyCessRate] = '" + OthDutyCessRate + "',[OthDutyCessAmount] = '" + OthDutyCessAmount + "',");
                Query.Append("   [OthDutyCessUnit] = '" + OthDutyCessUnit + "',[OthDutyCessQty] = '" + OthDutyCessQty + "',[OthDutyCessDesc] = '" + OthDutyCessDesc + "',[ThirdCessNotn] = '" + ThirdCessNotn + "',[ThirdCessRate] = '" + ThirdCessRate + "',[ThirdCessAmount] = '" + ThirdCessAmount + "'");
                Query.Append(",[ThirdCessUnit] = '" + ThirdCessUnit + "',[ThirdCessQty] = '" + ThirdCessQty + "'");
                Query.Append(",[ThirdCessDesc] = '" + ThirdCessDesc + "',[CENVATCertiNo] = '" + CENVATCertiNo + "',[CENVATDate] = '" + CENVATDate + "',[CENVATValidUpto] = '" + CENVATValidUpto + "',[CENVATCExOffCode] = '" + CENVATCExOffCode + "'");
                Query.Append(",[CENVATAssCode] = '" + CENVATAssCode + "',[ReExportItem] = '" + ReExportItem + "',[BENo] = '" + BENo + "',[BEDate] = '" + BEDate + "'");
                Query.Append(",[QuantityExported] = '" + QuantityExported + "',[QuantityExportedUnit] = '" + QuantityExportedUnit + "',[InvoiceSNo] = '" + InvoiceSNo + "'");
                Query.Append(",[ItemSNo] = '" + ItemSNo + "',[TechnicalDetails] = '" + TechnicalDetails + "',[ImportPortCode] = '" + ImportPortCode + "',[BEItemDesc] = '" + BEItemDesc + "'");
                Query.Append(",[OtherIdenPara] = '" + OtherIdenPara + "',[AgainstExpOblig] = '" + AgainstExpOblig + "',[ObligNo] = '" + ObligNo + "',[DrawbackAmtclaimed] = '" + DrawbackAmtclaimed + "'");
                Query.Append(",[QuantityImported] = '" + QuantityImported + "',[QuantityImportedUnit] = '" + QuantityImportedUnit + "',[ItemUnUsed] = '" + ItemUnUsed + "',[CommissionerPermi] = '" + CommissionerPermi + "'");
                Query.Append(",[AssessableValue] = '" + AssessableValue + "',[BoardNo] = '" + BoardNo + "',[BoardDate] = '" + BoardDate + "',[TotalDutyPaid] = '" + TotalDutyPaid + "',[TotalDutyPaidDate] = '" + TotalDutyPaidDate + "'");
                Query.Append(",[MODVATAvailed] = '" + MODVATAvailed + "',[MODVATReversed] = '" + MODVATReversed + "',[Accessories] = '" + Accessories + "',[ThirdPartyEXP] = '" + ThirdPartyEXP + "'");
                Query.Append(",[Manufacturer] = '" + Manufacturer + "',[IECode] = '" + IECode + "',[BranchSNo] = '" + BranchSNo + "',[ThirdPartyEXPAddress] = '" + ThirdPartyEXPAddress + "'");
                Query.Append(",[ThirdPartyEXPAddress1] = '" + ThirdPartyEXPAddress1 + "',[ModifiedBy] = '" + ModifiedBy + "',[ModifiedDate] = '" + ModifiedDate + "'");

                Result = CommonDL.ExecuteNonQuery(Query.ToString());
            }

            catch (Exception e)
            {
                Message = e.Message;
            }
            return Result;
        }

        public DataSet GetProductData(string JobNo)
        {
            StringBuilder Query = new StringBuilder();
            string Message = string.Empty;
            DataSet ds = new DataSet();
            try
            {
                Query.Append(" Select [JobNo],[InvoiceNo],[Description],[RITCCode],[Quantity],[QuantityUnit],[UnitPrice],[UnitPriceCurrency],[Per],[PerUnit],");
                Query.Append("[Amount],[AmountCurrency],[EximCode],[NFEICategory],[AlternateQty],[AlternateQtyUnit],[PMVCurrency],[PMVCalcMethod],[PMVCalcMethodRate],");
                Query.Append("[PMVUnitRate],[PMVUnit],[TotalPMV],[TotalPMVUnit],[RewardItem],[STRCode],[CessDuty],[ExportDutyNotn],[ExportDutyRate],[ExportDutyAmount],");
                Query.Append("[ExportDutyUnit],[ExportDutyQty],[CessNotn],[CessRate],[CessAmount],[CessUnit],[CessTariffValue],[CessTariffValueUnit],[CessQty],");
                Query.Append("[CessDesc],[OthDutyCessNotn],[OthDutyCessRate],[OthDutyCessAmount],[OthDutyCessUnit],[OthDutyCessQty],[OthDutyCessDesc],[ThirdCessNotn],");
                Query.Append("[ThirdCessRate],[ThirdCessAmount],[ThirdCessUnit],[ThirdCessQty],[ThirdCessDesc],[CENVATCertiNo],[CENVATDate],[CENVATValidUpto],");
                Query.Append("[CENVATCExOffCode],[CENVATAssCode],[ReExportItem],[BENo],[BEDate],[QuantityExported],[QuantityExportedUnit],[InvoiceSNo],[ItemSNo],");
                Query.Append("[TechnicalDetails],[ImportPortCode],[BEItemDesc],[OtherIdenPara],[AgainstExpOblig],[ObligNo],[DrawbackAmtclaimed],[QuantityImported],");
                Query.Append("[QuantityImportedUnit],[ItemUnUsed],[CommissionerPermi],[AssessableValue],[BoardNo],[BoardDate],[TotalDutyPaid],[TotalDutyPaidDate],");
                Query.Append("[MODVATAvailed],[MODVATReversed],[Accessories],[ThirdPartyEXP],[Manufacturer],[IECode],[BranchSNo],[ThirdPartyEXPAddress],[ThirdPartyEXPAddress1],[CreatedBy],[CreatedDate],[ModifiedBy],[ModifiedDate] ");


                Query.Append(" FROM [E_T_Product]  where JobNo='" + JobNo + "'");

                ds = CommonDL.GetDataSet(Query.ToString());
            }
            catch (Exception ex)
            {
                Message = ex.Message;
            }
            return ds;
        }

        public int mainsave(string JobNo, string InvoiceNo, string Code, string family, string Description, string RITCCode, double Quantity, string QuantityUnit, double UnitPrice, string UnitPriceCurrency, string Per, string PerUnit, double Amount, string AmountCurrency, string CreatedBy, string CreatedDate)
        {
            StringBuilder Query = new StringBuilder();
            int Result = 0;
            try
            {
                Query.Append("INSERT INTO [E_T_Product]");
                Query.Append(" (JobNo, InvoiceNo, ProductCode, ProductFamily, Description, RITCCode, Quantity, QuantityUnit, UnitPrice, UnitPriceCurrency, Per, PerUnit, Amount, AmountCurrency, CreatedBy, CreatedDate)");
                Query.Append("Values(");
                Query.Append("'" + JobNo + "','" + InvoiceNo + "', '" + Code + "', '" + family + "' ,'" + Description + "','" + RITCCode + "','" + Quantity + "','" + QuantityUnit + "','" + UnitPrice + "','" + UnitPriceCurrency + "','" + Per + "','" + PerUnit + "','" + Amount + "',");
                Query.Append("'" + AmountCurrency + "','" + CreatedBy + "','" + CreatedDate + "')");

                Result = CommonDL.ExecuteNonQuery(Query.ToString());
            }
            catch (Exception e)
            {
                Message = e.Message;
            }
            return Result;
        }

        public int mainupdate(string productid, string JobNo, string InvoiceNo, string Code, string family, string Description, string RITCCode, double Quantity, string QuantityUnit, double UnitPrice, string UnitPriceCurrency, string Per, string PerUnit, double Amount, string AmountCurrency, string ModifiedBy, string ModifiedDate)
        {
             StringBuilder Query = new StringBuilder();
            int Result = 0;
            try
            {
                Query.Append("UPDATE [E_T_Product] ");
                Query.Append("SET [JobNo] = '" + JobNo + "',[InvoiceNo] = '" + InvoiceNo + "',[ProductCode] = '" + Code + "', [ProductFamily] = '" + family + "'  ,[Description] = '" + Description + "',[RITCCode] = '" + RITCCode + "',");
                Query.Append("[Quantity] = '" + Quantity + "',[QuantityUnit] = '" + QuantityUnit + "',[UnitPrice] = '" + UnitPrice + "',[UnitPriceCurrency] = '" + UnitPriceCurrency + "',");
                Query.Append(" [Per] = '" + Per + "',[PerUnit] = '" + PerUnit + "',[Amount] = '" + Amount + "',[AmountCurrency] = '" + AmountCurrency + "',[ModifiedBy] = '" + ModifiedBy + "',[ModifiedDate] = '" + ModifiedDate + "'");
                Query.Append(" where [ID]='" + productid + "'");
                Result = CommonDL.ExecuteNonQuery(Query.ToString());

            }
            catch (Exception e) 
            {
                Message = e.Message;
            }
            return Result;
        }

        public int UpdateGeneral(string ProductID,string JobNo, string InvoiceNo, string EximCode, string NFEICategory, double AlternateQty, string AlternateQtyUnit, string PMVCurrency, string PMVCalcMethod,
                                 double PMVCalcMethodRate, string PMVUnit, double PMVUnitRate, double TotalPMV, string TotalPMVUnit, bool RewardItem,string STRCode, string ModifiedBy, string ModifiedDate)
        {
            StringBuilder Query = new StringBuilder();
            int Result = 0;
            try
            {
                Query.Append("UPDATE [E_T_Product] ");
                Query.Append("SET [EximCode] = '" + EximCode + "',[NFEICategory] = '" + NFEICategory + "',[AlternateQty] = '" + AlternateQty + "',[AlternateQtyUnit] = '" + AlternateQtyUnit + "',");
                Query.Append("[PMVCurrency] = '" + PMVCurrency + "',[PMVCalcMethod] = '" + PMVCalcMethod + "',[PMVCalcMethodRate] = '" + PMVCalcMethodRate + "',[PMVUnit] = '" + PMVUnit + "',");
                Query.Append(" [PMVUnitRate] = '" + PMVUnitRate + "',[TotalPMV] = '" + TotalPMV + "',[TotalPMVUnit] = '" + TotalPMVUnit + "',[ModifiedBy] = '" + ModifiedBy + "',[ModifiedDate] = '" + ModifiedDate + "',[RewardItem]='" + RewardItem + "',[STRCode]='"+STRCode+"' ");
                Query.Append(" where [ID]='" + ProductID + "'");
                Result = CommonDL.ExecuteNonQuery(Query.ToString());

            }
            catch (Exception e)
            {
                Message = e.Message;
            }
            return Result;
        }

        public int UpdateCessExpDuty(string ProductID, string JobNo, string InvoiceNo, string ExportDutyNotn, double ExportDutyRate, double ExportDutyAmount, string ExportDutyUnit, double ExportDutyQty, string CessNotn, 
                                 double CessRate, double CessAmount,string CessUnit, double CessTariffValue, string CessTariffValueUnit, double CessQty, string CessDesc, 
                                 string OthDutyCessNotn, double OthDutyCessRate, double OthDutyCessAmount, string OthDutyCessUnit,double OthDutyCessQty, string OthDutyCessDesc, 
                                 string ThirdCessNotn, double ThirdCessRate, double ThirdCessAmount,string ThirdCessUnit,double ThirdCessQty,string ThirdCessDesc,string CENVATCertiNo,
                                 string CENVATDate, string CENVATValidUpto, string CENVATCExOffCode, string CENVATAssCode, string ModifiedBy, string ModifiedDate)

        {
            StringBuilder Query = new StringBuilder();
            int Result = 0;
            try
            {
                Query.Append("UPDATE [E_T_Product]  SET ");
                Query.Append("ExportDutyNotn ='"+ ExportDutyNotn+"',ExportDutyRate ='"+ ExportDutyRate+"',ExportDutyAmount ='"+ExportDutyAmount+"',ExportDutyUnit ='"+ ExportDutyUnit+"',"); ;
                Query.Append("ExportDutyQty ='"+ ExportDutyQty+"',CessNotn ='"+ CessNotn+"',CessRate ='"+ CessRate+"',CessAmount='"+ CessAmount+"',CessUnit ='"+ CessUnit+"',");
                Query.Append("CessTariffValue ='"+ CessTariffValue+"',CessTariffValueUnit ='"+ CessTariffValueUnit+"',CessQty ='"+ CessQty+"',CessDesc ='"+ CessDesc+"',OthDutyCessNotn ='"+ OthDutyCessNotn+"',");
                Query.Append("OthDutyCessRate ='"+ OthDutyCessRate+"',OthDutyCessAmount ='"+ OthDutyCessAmount+"',OthDutyCessUnit ='"+ OthDutyCessUnit+"',OthDutyCessQty ='"+ OthDutyCessQty+"',");
                Query.Append("OthDutyCessDesc ='"+ OthDutyCessDesc+"',ThirdCessNotn ='"+ ThirdCessNotn+"',ThirdCessRate ='"+ ThirdCessRate+"',ThirdCessAmount ='"+ ThirdCessAmount+"',ThirdCessUnit ='"+ ThirdCessUnit+"',");
                Query.Append("ThirdCessQty ='"+  ThirdCessQty+"',ThirdCessDesc ='"+ ThirdCessDesc+"',CENVATCertiNo ='"+ CENVATCertiNo+"',CENVATDate ='"+ CENVATDate+"',CENVATValidUpto ='"+ CENVATValidUpto+"',");
                Query.Append("CENVATCExOffCode ='" + CENVATCExOffCode + "',CENVATAssCode ='" + CENVATAssCode + "',[ModifiedBy]='" + ModifiedBy + "',[ModifiedDate]='" + ModifiedDate + "' ");
                Query.Append(" where [ID]='" + ProductID + "'");



                Query.Append("");
                Result = CommonDL.ExecuteNonQuery(Query.ToString());

            }
            catch (Exception e)
            {
                Message = e.Message;
            }
            return Result;
        }

        public int UpdateReExport(string ProductID, string JobNo, string InvoiceNo, string BENo, string BEDate, double QuantityExported, string QuantityExportedUnit, string InvoiceSNo, string ItemSNo,
                                 string TechnicalDetails, string ImportPortCode, string BEItemDesc, string OtherIdenPara, bool AgainstExpOblig, string ObligNo, double DrawbackAmtclaimed,
                                 double QuantityImported, string QuantityImportedUnit, bool ItemUnUsed, bool CommissionerPermi, double AssessableValue, string BoardNo,
                                 string BoardDate, double TotalDutyPaid, string TotalDutyPaidDate, bool MODVATAvailed, bool MODVATReversed, string ModifiedBy, string ModifiedDate)

        {
            StringBuilder Query = new StringBuilder();
            int Result = 0;
            try
            {
                Query.Append("UPDATE [E_T_Product] SET ");
                Query.Append("BENo ='"+ BENo+"',BEDate ='"+ BEDate+"',QuantityExported ='"+ QuantityExported+"',QuantityExportedUnit ='"+ QuantityExportedUnit+"',InvoiceSNo ='"+ InvoiceSNo+"',");
                Query.Append("ItemSNo='"+ ItemSNo+"',TechnicalDetails ='"+ TechnicalDetails+"',ImportPortCode ='"+ ImportPortCode+"',BEItemDesc ='"+ BEItemDesc+"',OtherIdenPara ='"+ OtherIdenPara+"',");
                Query.Append("AgainstExpOblig ='"+ AgainstExpOblig+"',ObligNo ='"+ ObligNo+"',DrawbackAmtclaimed ='"+ DrawbackAmtclaimed+"',QuantityImported ='"+ QuantityImported+"',QuantityImportedUnit ='"+ QuantityImportedUnit+"',");
                Query.Append("ItemUnUsed ='"+ ItemUnUsed+"',CommissionerPermi ='"+ CommissionerPermi+"',AssessableValue ='"+ AssessableValue+"',BoardNo ='"+ BoardNo+"',");
                Query.Append("BoardDate ='" + BoardDate + "',TotalDutyPaid ='" + TotalDutyPaid + "',TotalDutyPaidDate='" + TotalDutyPaidDate + "',MODVATAvailed ='" + MODVATAvailed + "',MODVATReversed ='" + MODVATReversed + "',[ModifiedBy]='" + ModifiedBy + "',[ModifiedDate]='" + ModifiedDate + "' ");
                Query.Append(" where [ID]='" + ProductID + "'");
                Result = CommonDL.ExecuteNonQuery(Query.ToString());

            }
            catch (Exception e)
            {
                Message = e.Message;
            }
            return Result;
        }

        public int UpdateOtherDetails(string ProductID,string JobNo, string InvoiceNo, string Accessories, bool ThirdPartyEXP, string Manufacturer, string IECode, string BranchSNo, string ThirdPartyEXPAddress, string ThirdPartyEXPAddress1, string ModifiedBy, string ModifiedDate)
        {
            StringBuilder Query = new StringBuilder();
            int Result = 0;
            try
            {
                Query.Append("UPDATE [E_T_Product] SET ");
                Query.Append("Accessories ='" + Accessories + "',ThirdPartyEXP ='" + ThirdPartyEXP + "',Manufacturer ='" + Manufacturer + "',");
                Query.Append("IECode ='" + IECode + "',BranchSNo ='" + BranchSNo + "',ThirdPartyEXPAddress  ='" + ThirdPartyEXPAddress + "',ThirdPartyEXPAddress1 ='" + ThirdPartyEXPAddress1 + "',[ModifiedBy]='" + ModifiedBy + "',[ModifiedDate]='" + ModifiedDate + "' ");
                Query.Append(" where [ID]='" + ProductID + "'");
                Result = CommonDL.ExecuteNonQuery(Query.ToString());
            }
            catch (Exception e)
            {
                Message = e.Message;
            }
            return Result;
        }

        public int DEPBSave(string JobNo, string InvoiceNo, string ProductDesc, bool DEPBItem, string ProductGroup, string RateListNo, double DEPBRate, double DEPBQty,
            string DEPBUnit, string CAPValue, string StdIONorms, bool DEPBCredit, string CreatedBy, string CreatedDate)
        {
            StringBuilder Query = new StringBuilder();
            int Result = 0;
            try
            {
                Query.Append("INSERT INTO [E_T_Product_DEPB]");
                Query.Append(" ( JobNo, InvoiceNo, ProdDescription, DEPBItem, ProductGroup, RateListNo, DEPBRate, DEPBQty, DEPBUnit, CAPValue, StdIONorms, DEPBCredit, CreatedBy, CreatedDate)");
                Query.Append("Values(");
                Query.Append("'" + JobNo + "','" + InvoiceNo + "', '" + ProductDesc + "', '" + DEPBItem + "' ,'" + ProductGroup + "','" + RateListNo + "','" + DEPBRate + "','" + DEPBQty + "',");
                Query.Append("'" + DEPBUnit + "','"+CAPValue+"','" + StdIONorms + "','" + DEPBCredit + "','" + CreatedBy + "','" + CreatedDate + "')");

                Result = CommonDL.ExecuteNonQuery(Query.ToString());
            }
            catch (Exception e)
            {
                Message = e.Message;
            }
            return Result;
        }

        public int DEPBUpdate(string JobNo, string InvoiceNo, string ProductDesc, bool DEPBItem, string ProductGroup, string RateListNo, double DEPBRate, double DEPBQty,
         string DEPBUnit, string CAPValue, string StdIONorms, bool DEPBCredit, string ModifiedBy, string ModifiedDate)
        {
            StringBuilder Query = new StringBuilder();
            int Result = 0;
            try
            {
                Query.Append("Update [E_T_Product_DEPB] Set ");
                Query.Append("DEPBItem='" + DEPBItem + "', ProductGroup='" + ProductGroup + "', RateListNo='" + RateListNo + "', DEPBRate='" + DEPBRate + "', DEPBQty='" + DEPBQty + "', ");
                Query.Append("DEPBUnit='" + DEPBUnit + "', CAPValue='" + CAPValue + "', StdIONorms='" + StdIONorms + "', DEPBCredit='" + DEPBCredit + "',ModifiedBy='" + ModifiedBy + "', ModifiedDate='" + ModifiedDate + "'");
                Query.Append(" Where JobNo ='" + JobNo + "' And InvoiceNo='" + InvoiceNo + "' And ProdDescription='" + ProductDesc + "'");
                Result = CommonDL.ExecuteNonQuery(Query.ToString());
            }
            catch (Exception e)
            {
                Message = e.Message;
            }
            return Result;
        }


        public int DEPBCreditSave(string JobNo, string InvoiceNo, string ProductDesc,string ProductGroup, string RateListNo, double DEPBRate, double DEPBQty, string DEPBUnit,
          string QtyPercent, string CAPValue,string CreatedBy, string CreatedDate)
        {
            StringBuilder Query = new StringBuilder();
            int Result = 0;
            try
            {
                Query.Append("INSERT INTO [E_T_Product_DEPBCredit]");
                Query.Append(" ( JobNo, InvoiceNo, ProdDescription,ProductGroup, RateListNo, DEPBRate, DEPBQty, DEPBUnit,QtyPercent, CAPValue,CreatedBy, CreatedDate)");
                Query.Append("Values(");
                Query.Append("'" + JobNo + "','" + InvoiceNo + "', '" + ProductDesc + "','" + ProductGroup + "','" + RateListNo + "','" + DEPBRate + "','" + DEPBQty + "','" + DEPBUnit + "',");
                Query.Append("'"+QtyPercent+"','" + CAPValue + "','" + CreatedBy + "','" + CreatedDate + "')");

                Result = CommonDL.ExecuteNonQuery(Query.ToString());
            }
            catch (Exception e)
            {
                Message = e.Message;
            }
            return Result;
        }

        public int DEPBCreditUpdate(string ID ,string JobNo, string InvoiceNo, string ProductDesc, string ProductGroup, string RateListNo, double DEPBRate, double DEPBQty, string DEPBUnit,
          string QtyPercent, string CAPValue, string ModifiedBy, string ModifiedDate)
        {
            StringBuilder Query = new StringBuilder();
            int Result = 0;
            try
            {
                Query.Append("Update [E_T_Product_DEPBCredit] Set ");
                Query.Append(" ProductGroup='" + ProductGroup + "', RateListNo='" + RateListNo + "', DEPBRate='" + DEPBRate + "', DEPBQty='" + DEPBQty + "', ");
                Query.Append("DEPBUnit='" + DEPBUnit + "',QtyPercent='"+QtyPercent+"', CAPValue='" + CAPValue + "',ModifiedBy='" + ModifiedBy + "', ModifiedDate='" + ModifiedDate + "'");
                Query.Append(" Where ID='"+ID+"'");
                Result = CommonDL.ExecuteNonQuery(Query.ToString());
            }
            catch (Exception e)
            {
                Message = e.Message;
            }
            return Result;
        }

        public int DrawbackSave(string JobNo, string InvoiceNo, string ProductDesc, bool DBKItem, string DBKSNO, string FOBvalue, string Qty, string Unit,
         string DBKUnder, string DBKDesc, double DBKRate, double DBKCap, string DBKUnit, double DBKAmount, string DBKAmountDesc, string CreatedBy, string CreatedDate)
        {
            StringBuilder Query = new StringBuilder();
            int Result = 0;
            try
            {
                Query.Append("INSERT INTO [E_T_Product_Drawback]");
                Query.Append(" (  JobNo, InvoiceNo, ProdDescription, DBKItem, DBKSNO, FOBvalue, Qty, Unit, DBKUnder, DBKDesc, DBKRate, DBKCap, DBKUnit, DBKAmount, DBKAmountDesc, CreatedBy, CreatedDate)");
                Query.Append("Values(");
                Query.Append("'" + JobNo + "','" + InvoiceNo + "', '" + ProductDesc + "','" + DBKItem + "','" + DBKSNO + "','" + FOBvalue + "','" + Qty + "','" + Unit + "',");
                Query.Append("'" + DBKUnder + "','" + DBKDesc + "','" + DBKRate + "','" + DBKCap + "','" + DBKUnit + "','" + DBKAmount + "','" + DBKAmountDesc + "','" + CreatedBy + "','" + CreatedDate + "')");

                Result = CommonDL.ExecuteNonQuery(Query.ToString());
            }
            catch (Exception e)
            {
                Message = e.Message;
            }
            return Result;
        }

        public int DrawbackUpdate(string JobNo, string InvoiceNo, string ProductDesc, bool DBKItem, string DBKSNO, string FOBvalue, string Qty, string Unit,
       string DBKUnder, string DBKDesc, double DBKRate, double DBKCap, string DBKUnit, double DBKAmount, string DBKAmountDesc, string ModifiedBy, string ModifiedDate)
        {
            StringBuilder Query = new StringBuilder();
            int Result = 0;
            try
            {
                Query.Append("Update [E_T_Product_Drawback] Set");
                Query.Append(" JobNo='" + JobNo + "',InvoiceNo='" + InvoiceNo + "', ProdDescription='" + ProductDesc + "',DBKItem='" + DBKItem + "',DBKSNO='" + DBKSNO + "', ");
                Query.Append("FOBvalue='" + FOBvalue + "',Qty='" + Qty + "',Unit='" + Unit + "',DBKUnder='" + DBKUnder + "',DBKDesc='" + DBKDesc + "',DBKRate='" + DBKRate + "',");
                Query.Append("DBKCap='" + DBKCap + "',DBKUnit='" + DBKUnit + "',DBKAmount='" + DBKAmount + "',DBKAmountDesc='" + DBKAmountDesc + "', ModifiedBy='" + ModifiedBy + "', ModifiedDate='" + ModifiedDate + "'");
                Query.Append(" Where JobNo ='" + JobNo + "' And InvoiceNo='" + InvoiceNo + "' And ProdDescription='" + ProductDesc + "'");
                Result = CommonDL.ExecuteNonQuery(Query.ToString());
            }
            catch (Exception e)
            {
                Message = e.Message;
            }
            return Result;
        }

        public int DrawbackMaterialsSave(string JobNo, string InvoiceNo, string ProductDesc, string SNO, string Description, string ExciseDBKRate, string CustomDBKRate,
        string Qty, string Unit,string CreatedBy, string CreatedDate)
        {
            StringBuilder Query = new StringBuilder();
            int Result = 0;
            try
            {
                Query.Append("INSERT INTO [E_T_Product_DrawbackMaterials]");
                Query.Append(" (  JobNo, InvoiceNo, ProdDescription, SNO, Description, ExciseDBKRate, CustomDBKRate, Qty, Unit, CreatedBy, CreatedDate)");

                Query.Append("Values(");
                Query.Append("'" + JobNo + "','" + InvoiceNo + "', '" + ProductDesc + "','" + SNO + "','" + Description + "','" + ExciseDBKRate + "','" + CustomDBKRate + "','" + Qty + "','" + Unit + "',");
                Query.Append("'" + CreatedBy + "','" + CreatedDate + "')");

                Result = CommonDL.ExecuteNonQuery(Query.ToString());
            }
            catch (Exception e)
            {
                Message = e.Message;
            }
            return Result;
        }


        public int DrawbackMaterialsUpdate(string ID, string JobNo, string InvoiceNo, string ProductDesc, string SNO, string Description, string ExciseDBKRate, string CustomDBKRate,
        string Qty, string Unit, string ModifiedBy, string ModifiedDate)
        {
            StringBuilder Query = new StringBuilder();
            int Result = 0;
            try
            {
                Query.Append("Update [E_T_Product_DrawbackMaterials] SET ");
                Query.Append("Description='" + Description + "',ExciseDBKRate='" + ExciseDBKRate + "',CustomDBKRate='" + CustomDBKRate + "',Qty='" + Qty + "',Unit='" + Unit + "',ModifiedBy='" + ModifiedBy + "',ModifiedDate='"+ModifiedDate+"'");

                Query.Append("Where ID ='" + ID+ "'");
                Result = CommonDL.ExecuteNonQuery(Query.ToString());
            }
            catch (Exception e)
            {
                Message = e.Message;
            }
            return Result;
        }

        public int EPCGSave(string JobNo, string InvoiceNo, string ProductDesc, bool EPCGItem, string EDIRegnNo, string EDIRegnDate, string CreatedBy, string CreatedDate)
        {
            StringBuilder Query = new StringBuilder();
            int Result = 0;
            try
            {
                Query.Append("INSERT INTO [E_T_Product_EPCG]");
                Query.Append(" (JobNo, InvoiceNo, ProdDescription, EPCGItem, EDIRegnNo, EDIRegnDate, CreatedBy, CreatedDate)");

                Query.Append("Values(");
                Query.Append("'" + JobNo + "','" + InvoiceNo + "', '" + ProductDesc + "','" + EPCGItem + "','" + EDIRegnNo + "','" + EDIRegnDate + "',");
                Query.Append("'" + CreatedBy + "','" + CreatedDate + "')");

                Result = CommonDL.ExecuteNonQuery(Query.ToString());
            }
            catch (Exception e)
            {
                Message = e.Message;
            }
            return Result;
        }

        public int EPCGUpdate(string ID,string JobNo, string InvoiceNo, string ProductDesc, bool EPCGItem, string EDIRegnNo, string EDIRegnDate, string ModifiedBy, string ModifiedDate)
        {
            StringBuilder Query = new StringBuilder();
            int Result = 0;
            try
            {
                Query.Append("Update [E_T_Product_EPCG] SET ");
                Query.Append(" EPCGItem='" + EPCGItem + "',EDIRegnNo='" + EDIRegnNo + "',EDIRegnDate='" + EDIRegnDate + "'");
                Query.Append("Where ID = '"+ID+"'");
                
                Result = CommonDL.ExecuteNonQuery(Query.ToString());
            }
            catch (Exception e)
            {
                Message = e.Message;
            }
            return Result;
        }

        public int EPCGItemsSave(string JobNo, string InvoiceNo, string ProductDesc, string EDIRegnNo,string ItemSnoExport,string ExportQtyUnderLicence,string ImportSNO,string ImportItemSNO,string ImportDesc,string ImportQty,string ImportUnit ,string ImportType,string CreatedBy,string CreatedDate)
        {
            StringBuilder Query = new StringBuilder();
            int Result = 0;
            try
            {
                Query.Append("INSERT INTO [E_T_Product_EPCGItems]");
                Query.Append(" (JobNo, InvoiceNo, ProdDescription, EDIRegnNo, ItemSnoExport, ExportQtyUnderLicence, ImportSNO, ImportItemSNO, ImportDesc, ImportQty, ImportUnit, ImportType, CreatedBy, CreatedDate");
                Query.Append(")Values(");
                Query.Append("'" + JobNo + "','" + InvoiceNo + "', '" + ProductDesc + "','" + EDIRegnNo + "','" + ItemSnoExport + "','" + ExportQtyUnderLicence + "','" + ImportSNO + "','" + ImportItemSNO + "','" + ImportDesc + "','" + ImportQty + "','" + ImportUnit + "','" + ImportType + "',");
                Query.Append("'" + CreatedBy + "','" + CreatedDate + "')");
                Result = CommonDL.ExecuteNonQuery(Query.ToString());
            }
            catch (Exception e)
            {
                Message = e.Message;
            }
            return Result;
        }

        public int EPCGItemsUpdate(string ID,string JobNo, string InvoiceNo, string ProductDesc, string EDIRegnNo, string ItemSnoExport, string ExportQtyUnderLicence, string ImportSNO, string ImportItemSNO, string ImportDesc, string ImportQty, string ImportUnit, string ImportType, string CreatedBy, string CreatedDate)
        {
            StringBuilder Query = new StringBuilder();
            int Result = 0;
            try
            {
                Query.Append("Update [E_T_Product_EPCGItems] SET");
                Query.Append(" JobNo='" + JobNo + "',InvoiceNo='" + InvoiceNo + "',ProdDescription='" + ProductDesc + "',EDIRegnNo='" + EDIRegnNo + "',ItemSnoExport='" + ItemSnoExport + "',ExportQtyUnderLicence='" + ExportQtyUnderLicence + "',ImportSNO='" + ImportSNO + "',ImportItemSNO='" + ImportItemSNO + "',ImportDesc='" + ImportDesc + "',ImportQty='" + ImportQty + "',ImportUnit='" + ImportUnit + "',ImportType='" + ImportType + "',CreatedBy='" + CreatedBy + "',CreatedDate='" + CreatedDate + "'");
                Query.Append("Where ID = '" + ID + "'");
                Result = CommonDL.ExecuteNonQuery(Query.ToString());
            }
            catch (Exception e)
            {
                Message = e.Message;
            }
            return Result;
        }

        }
    }

