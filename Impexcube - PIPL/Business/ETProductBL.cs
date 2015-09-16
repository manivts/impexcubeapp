using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
namespace VTS.ImpexCube.Business
{
    public class ETProductBL
    {
        VTS.ImpexCube.Data.ETProductDL objETProductDL = new VTS.ImpexCube.Data.ETProductDL();

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
            return objETProductDL.save(JobNo, InvoiceNo, Description, RITCCode, Quantity, QuantityUnit, UnitPrice, UnitPriceCurrency, Per, PerUnit, Amount, AmountCurrency, EximCode,
                     NFEICategory, AlternateQty, AlternateQtyUnit, PMVCurrency, PMVCalcMethod, PMVCalcMethodRate, PMVUnitRate, PMVUnit, TotalPMV, TotalPMVUnit, RewardItem,
                     STRCode, CessDuty, ExportDutyNotn, ExportDutyRate, ExportDutyAmount, ExportDutyUnit, ExportDutyQty, CessNotn, CessRate, CessAmount, CessUnit,
                     CessTariffValue, CessTariffValueUnit, CessQty, CessDesc, OthDutyCessNotn, OthDutyCessRate, OthDutyCessAmount, OthDutyCessUnit, OthDutyCessQty,
                     OthDutyCessDesc, ThirdCessNotn, ThirdCessRate, ThirdCessAmount, ThirdCessUnit, ThirdCessQty, ThirdCessDesc, CENVATCertiNo, CENVATDate, CENVATValidUpto,
                     CENVATCExOffCode, CENVATAssCode, ReExportItem, BENo, BEDate, QuantityExported, QuantityExportedUnit, InvoiceSNo, ItemSNo, TechnicalDetails,
                     ImportPortCode, BEItemDesc, OtherIdenPara, AgainstExpOblig, ObligNo, DrawbackAmtclaimed, QuantityImported, QuantityImportedUnit, ItemUnUsed,
                     CommissionerPermi, AssessableValue, BoardNo, BoardDate, TotalDutyPaid, TotalDutyPaidDate, MODVATAvailed, MODVATReversed, Accessories, ThirdPartyEXP,
                     Manufacturer, IECode, BranchSNo, ThirdPartyEXPAddress, ThirdPartyEXPAddress1, CreatedBy, CreatedDate);
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
            return objETProductDL.update(JobNo, InvoiceNo, Description, RITCCode, Quantity, QuantityUnit, UnitPrice, UnitPriceCurrency, Per, PerUnit, Amount, AmountCurrency, EximCode, 
                     NFEICategory,  AlternateQty,  AlternateQtyUnit,  PMVCurrency,  PMVCalcMethod,  PMVCalcMethodRate,  PMVUnitRate,  PMVUnit,  TotalPMV,  TotalPMVUnit,  RewardItem, 
                     STRCode,  CessDuty,  ExportDutyNotn,  ExportDutyRate,  ExportDutyAmount,  ExportDutyUnit,  ExportDutyQty,  CessNotn,  CessRate,  CessAmount,  CessUnit, 
                     CessTariffValue,  CessTariffValueUnit,  CessQty,  CessDesc,  OthDutyCessNotn,  OthDutyCessRate,  OthDutyCessAmount,  OthDutyCessUnit,  OthDutyCessQty, 
                     OthDutyCessDesc,  ThirdCessNotn,  ThirdCessRate,  ThirdCessAmount,  ThirdCessUnit,  ThirdCessQty,  ThirdCessDesc,  CENVATCertiNo,  CENVATDate,  CENVATValidUpto, 
                     CENVATCExOffCode,  CENVATAssCode,  ReExportItem,  BENo,  BEDate,  QuantityExported,  QuantityExportedUnit,  InvoiceSNo,  ItemSNo,  TechnicalDetails, 
                     ImportPortCode,  BEItemDesc,  OtherIdenPara,  AgainstExpOblig,  ObligNo,  DrawbackAmtclaimed,  QuantityImported,  QuantityImportedUnit,  ItemUnUsed, 
                     CommissionerPermi,  AssessableValue,  BoardNo,  BoardDate,  TotalDutyPaid,  TotalDutyPaidDate,  MODVATAvailed,  MODVATReversed,  Accessories,  ThirdPartyEXP, 
                     Manufacturer,  IECode,  BranchSNo,  ThirdPartyEXPAddress,  ThirdPartyEXPAddress1,  ModifiedBy,  ModifiedDate);
        }

        public DataSet GetProductData(string JobNo)
        {
            return objETProductDL.GetProductData(JobNo);
        }


        public int mainsave(string JobNo, string InvoiceNo, string Code, string family, string Description, string RITCCode, double Quantity, string QuantityUnit, double UnitPrice, string UnitPriceCurrency, string Per, string PerUnit, double Amount, string AmountCurrency, string CreatedBy, string CreatedDate)
        {
            return objETProductDL.mainsave( JobNo,  InvoiceNo, Code, family,  Description,  RITCCode,  Quantity,  QuantityUnit,  UnitPrice,  UnitPriceCurrency,  Per,  PerUnit,  Amount,  AmountCurrency,  CreatedBy,  CreatedDate);
        }

        public int mainupdate(string productid ,string JobNo, string InvoiceNo, string Code, string family, string Description, string RITCCode, double Quantity, string QuantityUnit, double UnitPrice, string UnitPriceCurrency, string Per, string PerUnit, double Amount, string AmountCurrency, string ModifiedBy, string ModifiedDate)
        {
            return objETProductDL.mainupdate(productid, JobNo,  InvoiceNo, Code, family,  Description,  RITCCode,  Quantity,  QuantityUnit,  UnitPrice,  UnitPriceCurrency,  Per,  PerUnit,  Amount,  AmountCurrency,  ModifiedBy,  ModifiedDate);
        }

        public int UpdateGeneral(string ProductID,string JobNo, string InvoiceNo, string EximCode, string NFEICategory, double AlternateQty, string AlternateQtyUnit, string PMVCurrency, string PMVCalcMethod,
                                double PMVCalcMethodRate, string PMVUnit, double PMVUnitRate, double TotalPMV, string TotalPMVUnit, bool RewardItem, string STRCode, string ModifiedBy, string ModifiedDate)
        {
            return objETProductDL.UpdateGeneral(ProductID,JobNo, InvoiceNo, EximCode, NFEICategory, AlternateQty, AlternateQtyUnit, PMVCurrency, PMVCalcMethod,
                                PMVCalcMethodRate, PMVUnit, PMVUnitRate, TotalPMV, TotalPMVUnit, RewardItem, STRCode, ModifiedBy, ModifiedDate);
        }


        public int UpdateCessExpDuty(string ProductID, string JobNo, string InvoiceNo, string ExportDutyNotn, double ExportDutyRate, double ExportDutyAmount, string ExportDutyUnit, double ExportDutyQty, string CessNotn, 
                                 double CessRate, double CessAmount,string CessUnit, double CessTariffValue, string CessTariffValueUnit, double CessQty, string CessDesc, 
                                 string OthDutyCessNotn, double OthDutyCessRate, double OthDutyCessAmount, string OthDutyCessUnit,double OthDutyCessQty, string OthDutyCessDesc, 
                                 string ThirdCessNotn, double ThirdCessRate, double ThirdCessAmount,string ThirdCessUnit,double ThirdCessQty,string ThirdCessDesc,string CENVATCertiNo,
                                 string CENVATDate, string CENVATValidUpto, string CENVATCExOffCode, string CENVATAssCode, string ModifiedBy, string ModifiedDate)
        {
            return objETProductDL.UpdateCessExpDuty(ProductID, JobNo,  InvoiceNo,  ExportDutyNotn,  ExportDutyRate,  ExportDutyAmount,  ExportDutyUnit,  ExportDutyQty,  CessNotn, 
                                  CessRate,  CessAmount, CessUnit,  CessTariffValue,  CessTariffValueUnit,  CessQty,  CessDesc, 
                                  OthDutyCessNotn,  OthDutyCessRate,  OthDutyCessAmount,  OthDutyCessUnit, OthDutyCessQty,  OthDutyCessDesc, 
                                  ThirdCessNotn,  ThirdCessRate,  ThirdCessAmount, ThirdCessUnit, ThirdCessQty, ThirdCessDesc, CENVATCertiNo,
                                  CENVATDate,  CENVATValidUpto,  CENVATCExOffCode,  CENVATAssCode,  ModifiedBy,  ModifiedDate);

        }
        public int UpdateReExport(string ProductID,string JobNo, string InvoiceNo, string BENo, string BEDate, double QuantityExported, string QuantityExportedUnit, string InvoiceSNo, string ItemSNo,
                                 string TechnicalDetails, string ImportPortCode, string BEItemDesc, string OtherIdenPara, bool AgainstExpOblig, string ObligNo, double DrawbackAmtclaimed,
                                 double QuantityImported, string QuantityImportedUnit, bool ItemUnUsed, bool CommissionerPermi, double AssessableValue, string BoardNo,
                                 string BoardDate, double TotalDutyPaid, string TotalDutyPaidDate, bool MODVATAvailed, bool MODVATReversed, string ModifiedBy, string ModifiedDate)
        {
            return objETProductDL.UpdateReExport(ProductID, JobNo,  InvoiceNo,  BENo,  BEDate,  QuantityExported,  QuantityExportedUnit,  InvoiceSNo,  ItemSNo,
                                  TechnicalDetails,  ImportPortCode,  BEItemDesc,  OtherIdenPara,  AgainstExpOblig,  ObligNo,  DrawbackAmtclaimed,
                                  QuantityImported,  QuantityImportedUnit,  ItemUnUsed,  CommissionerPermi,  AssessableValue,  BoardNo,
                                  BoardDate,  TotalDutyPaid,  TotalDutyPaidDate,  MODVATAvailed,  MODVATReversed,  ModifiedBy,  ModifiedDate);
        }

        public int UpdateOtherDetails(string ProductID,string JobNo, string InvoiceNo, string Accessories, bool ThirdPartyEXP, string Manufacturer, string IECode, string BranchSNo, string ThirdPartyEXPAddress, string ThirdPartyEXPAddress1, string ModifiedBy, string ModifiedDate)
        {
            return objETProductDL.UpdateOtherDetails(ProductID, JobNo,  InvoiceNo,  Accessories,  ThirdPartyEXP,  Manufacturer,  IECode,  BranchSNo,  ThirdPartyEXPAddress,  ThirdPartyEXPAddress1,  ModifiedBy,  ModifiedDate);
        }

         public int DEPBSave(string JobNo, string InvoiceNo, string ProductDesc, bool DEPBItem, string ProductGroup, string RateListNo, double DEPBRate, double DEPBQty,
            string DEPBUnit, string CAPValue, string StdIONorms, bool DEPBCredit, string CreatedBy, string CreatedDate)
        {
            return objETProductDL.DEPBSave(JobNo, InvoiceNo, ProductDesc,  DEPBItem, ProductGroup, RateListNo,  DEPBRate,  DEPBQty,
            DEPBUnit, CAPValue, StdIONorms,  DEPBCredit, CreatedBy, CreatedDate);

        }

         public int DEPBUpdate(string JobNo, string InvoiceNo, string ProductDesc, bool DEPBItem, string ProductGroup, string RateListNo, double DEPBRate, double DEPBQty,
          string DEPBUnit, string CAPValue, string StdIONorms, bool DEPBCredit, string ModifiedBy, string ModifiedDate)
         {
             return objETProductDL.DEPBUpdate(JobNo, InvoiceNo, ProductDesc, DEPBItem, ProductGroup, RateListNo, DEPBRate, DEPBQty,
            DEPBUnit, CAPValue, StdIONorms, DEPBCredit, ModifiedBy, ModifiedDate);
         }

         public int DEPBCreditSave(string JobNo, string InvoiceNo, string ProductDesc, string ProductGroup, string RateListNo, double DEPBRate, double DEPBQty, string DEPBUnit,
         string QtyPercent, string CAPValue, string CreatedBy, string CreatedDate)
         {
             return objETProductDL.DEPBCreditSave( JobNo,  InvoiceNo,  ProductDesc,  ProductGroup,  RateListNo, DEPBRate,DEPBQty,  DEPBUnit,
          QtyPercent,  CAPValue,  CreatedBy,  CreatedDate);
         }

         public int DEPBCreditUpdate(string ID, string JobNo, string InvoiceNo, string ProductDesc, string ProductGroup, string RateListNo, double DEPBRate, double DEPBQty, string DEPBUnit,
         string QtyPercent, string CAPValue, string ModifiedBy, string ModifiedDate)
         {
             return objETProductDL.DEPBCreditUpdate(ID ,JobNo, InvoiceNo, ProductDesc, ProductGroup, RateListNo, DEPBRate, DEPBQty, DEPBUnit,
          QtyPercent, CAPValue, ModifiedBy, ModifiedDate);
         }

         public int DrawbackSave(string JobNo, string InvoiceNo, string ProductDesc, bool DBKItem, string DBKSNO, string FOBvalue, string Qty, string Unit,
         string DBKUnder, string DBKDesc, double DBKRate, double DBKCap, string DBKUnit, double DBKAmount, string DBKAmountDesc, string CreatedBy, string CreatedDate)
         {
             return objETProductDL.DrawbackSave(JobNo,InvoiceNo,ProductDesc,DBKItem,DBKSNO,FOBvalue,Qty,Unit,DBKUnder,DBKDesc,DBKRate,DBKCap,DBKUnit,DBKAmount,DBKAmountDesc,CreatedBy,CreatedDate);
         }

         public int DrawbackUpdate(string JobNo, string InvoiceNo, string ProductDesc, bool DBKItem, string DBKSNO, string FOBvalue, string Qty, string Unit,
      string DBKUnder, string DBKDesc, double DBKRate, double DBKCap, string DBKUnit, double DBKAmount, string DBKAmountDesc, string ModifiedBy, string ModifiedDate)
         {
             return objETProductDL.DrawbackUpdate(JobNo, InvoiceNo, ProductDesc, DBKItem, DBKSNO, FOBvalue, Qty, Unit, DBKUnder, DBKDesc, DBKRate, DBKCap, DBKUnit, DBKAmount, DBKAmountDesc, ModifiedBy, ModifiedDate);
         }

         public int DrawbackMaterialsSave(string JobNo, string InvoiceNo, string ProductDesc, string SNO, string Description, string ExciseDBKRate, string CustomDBKRate,
        string Qty, string Unit, string CreatedBy, string CreatedDate)
         {
             return objETProductDL.DrawbackMaterialsSave(JobNo,InvoiceNo,ProductDesc,SNO,Description,ExciseDBKRate,CustomDBKRate,
             Qty,Unit,CreatedBy,CreatedDate);
         }

         public int DrawbackMaterialsUpdate(string ID, string JobNo, string InvoiceNo, string ProductDesc, string SNO, string Description, string ExciseDBKRate, string CustomDBKRate,
        string Qty, string Unit, string ModifiedBy, string ModifiedDate)
         {
             return objETProductDL.DrawbackMaterialsUpdate(ID,JobNo, InvoiceNo, ProductDesc, SNO, Description, ExciseDBKRate, CustomDBKRate,
            Qty, Unit, ModifiedBy, ModifiedDate);
         }

         public int EPCGSave(string JobNo, string InvoiceNo, string ProductDesc, bool EPCGItem, string EDIRegnNo, string EDIRegnDate, string CreatedBy, string CreatedDate)
         {
             return objETProductDL.EPCGSave(JobNo, InvoiceNo, ProductDesc, EPCGItem, EDIRegnNo, EDIRegnDate, CreatedBy, CreatedDate);
         }

         public int EPCGUpdate(string ID, string JobNo, string InvoiceNo, string ProductDesc, bool EPCGItem, string EDIRegnNo, string EDIRegnDate, string ModifiedBy, string ModifiedDate)
         {
             return objETProductDL.EPCGUpdate(ID, JobNo, InvoiceNo, ProductDesc, EPCGItem, EDIRegnNo, EDIRegnDate, ModifiedBy, ModifiedDate);
         }

         public int EPCGItemsSave(string JobNo, string InvoiceNo, string ProductDesc, string EDIRegnNo, string ItemSnoExport, string ExportQtyUnderLicence, string ImportSNO, string ImportItemSNO, string ImportDesc, string ImportQty, string ImportUnit, string ImportType, string CreatedBy, string CreatedDate)
         {
             return objETProductDL.EPCGItemsSave(JobNo, InvoiceNo, ProductDesc, EDIRegnNo, ItemSnoExport, ExportQtyUnderLicence, ImportSNO, ImportItemSNO, ImportDesc, ImportQty, ImportUnit, ImportType, CreatedBy, CreatedDate);
         }

         public int EPCGItemsUpdate(string ID, string JobNo, string InvoiceNo, string ProductDesc, string EDIRegnNo, string ItemSnoExport, string ExportQtyUnderLicence, string ImportSNO, string ImportItemSNO, string ImportDesc, string ImportQty, string ImportUnit, string ImportType, string CreatedBy, string CreatedDate)
         {
             return objETProductDL.EPCGItemsUpdate(ID,JobNo, InvoiceNo, ProductDesc, EDIRegnNo, ItemSnoExport, ExportQtyUnderLicence, ImportSNO, ImportItemSNO, ImportDesc, ImportQty, ImportUnit, ImportType, CreatedBy, CreatedDate);
         }
    }
}
