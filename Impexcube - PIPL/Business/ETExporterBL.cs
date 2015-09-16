using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VTS.ImpexCube.Data;
using System.Data;
namespace VTS.ImpexCube.Business
{
    public class ETExporterBL
    {
        ETExporterDL ETExporterDL = new ETExporterDL();

        public int ExporterSave(string JobNo, string ExporterName, string ExporterAddress, string ExporterAddress1, string BranchSno, string StateProvince, string IECodeNo, string Consignee,
                                 string ConsigneeAddress, string ConsigneeAddress1, string ConsigneeCountry,bool IsBuyer, string Buyer, string BuyerAddress, string BuyerAddress1, string ExporterRefNo,
                                 string ExporterRefDate, string ExporterType, string SBNo, string SBDate, string RBIApprNo, string RBIApprDate,bool IsGRWaived, string GRNo, string GRDate, string RBIWaiverNo,
                                 string RBIWavierDate, string BankDealer, string ACNo, string BankDealerCode, string EPZCode, string Notify, string Address, string Address1, string CreatedBy, string CreatedDate)
        {
            return ETExporterDL.ExporterSave(JobNo, ExporterName, ExporterAddress, ExporterAddress1, BranchSno, StateProvince, IECodeNo, Consignee,
                                  ConsigneeAddress, ConsigneeAddress1, ConsigneeCountry,IsBuyer, Buyer, BuyerAddress, BuyerAddress1, ExporterRefNo,
                                  ExporterRefDate, ExporterType, SBNo, SBDate, RBIApprNo, RBIApprDate,IsGRWaived, GRNo, GRDate, RBIWaiverNo,
                                  RBIWavierDate, BankDealer, ACNo, BankDealerCode, EPZCode, Notify, Address, Address1, CreatedBy, CreatedDate);
        }

        public int ExporterUpdate(string JobNo, string ExporterName, string ExporterAddress, string ExporterAddress1, string BranchSno, string StateProvince, string IECodeNo, string Consignee,
                              string ConsigneeAddress, string ConsigneeAddress1, string ConsigneeCountry, bool IsBuyer,string Buyer, string BuyerAddress, string BuyerAddress1, string ExporterRefNo,
                              string ExporterRefDate, string ExporterType, string SBNo, string SBDate, string RBIApprNo, string RBIApprDate,bool IsGRWaived, string GRNo, string GRDate, string RBIWaiverNo,
                              string RBIWavierDate, string BankDealer, string ACNo, string BankDealerCode, string EPZCode, string Notify, string Address, string Address1, string ModifiedBy, string ModifiedDate)
        {
            return ETExporterDL.ExporterUpdate(JobNo, ExporterName, ExporterAddress, ExporterAddress1, BranchSno, StateProvince, IECodeNo, Consignee,
                               ConsigneeAddress, ConsigneeAddress1, ConsigneeCountry,IsBuyer, Buyer, BuyerAddress, BuyerAddress1, ExporterRefNo,
                               ExporterRefDate, ExporterType, SBNo, SBDate, RBIApprNo, RBIApprDate,IsGRWaived, GRNo, GRDate, RBIWaiverNo,
                               RBIWavierDate, BankDealer, ACNo, BankDealerCode, EPZCode, Notify, Address, Address1, ModifiedBy, ModifiedDate);
        }

        public DataSet GetExportData(string JobNo)
        {
            return ETExporterDL.GetExportData(JobNo);
        }

    }
}
