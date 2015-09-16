using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
namespace VTS.ImpexCube.Data
{
    public class ETExporterDL
    {
        CommonDL CommonDL = new CommonDL();
        public int ExporterSave(string JobNo, string ExporterName, string ExporterAddress, string ExporterAddress1, string BranchSno, string StateProvince, string IECodeNo, string Consignee,
                                 string ConsigneeAddress, string ConsigneeAddress1, string ConsigneeCountry,bool IsBuyer, string Buyer, string BuyerAddress, string BuyerAddress1, string ExporterRefNo,
                                 string ExporterRefDate, string ExporterType, string SBNo, string SBDate, string RBIApprNo, string RBIApprDate,bool IsGRWaived, string GRNo, string GRDate, string RBIWaiverNo,
                                 string RBIWavierDate, string BankDealer, string ACNo, string BankDealerCode, string EPZCode, string Notify, string Address, string Address1, string CreatedBy, string CreatedDate)
        {
            string Message = string.Empty;
            StringBuilder Query = new StringBuilder();
            int Result = 0;
            try
            {
                Query.Append(" INSERT INTO [E_T_Exporter] ");
                Query.Append("([JobNo],[ExporterName],[ExporterAddress],[ExporterAddress1],[BranchSno],[StateProvince],[IECodeNo],[Consignee],[ConsigneeAddress],[ConsigneeAddress1],");
                Query.Append("[ConsigneeCountry],[IsBuyer],[Buyer],[BuyerAddress],[BuyerAddress1],[ExporterRefNo],[ExporterRefDate],[ExporterType],[SBNo],[SBDate],[RBIApprNo],[RBIApprDate],[IsGRWaived],");
                Query.Append("[GRNo],[GRDate],[RBIWaiverNo],[RBIWavierDate],[BankDealer],[ACNo],[BankDealerCode],[EPZCode],[Notify],[Address],[Address1],[CreatedBy],[CreatedDate])");
                Query.Append("Values");
                Query.Append("('" + JobNo + "','" + ExporterName + "','" + ExporterAddress + "','" + ExporterAddress1 + "','" + BranchSno + "','" + StateProvince + "','" + IECodeNo + "','" + Consignee + "','" + ConsigneeAddress + "','" + ConsigneeAddress1 + "',");
                Query.Append("'" + ConsigneeCountry + "','" + IsBuyer + "','" + Buyer + "','" + BuyerAddress + "','" + BuyerAddress1 + "','" + ExporterRefNo + "','" + ExporterRefDate + "','" + ExporterType + "','" + SBNo + "','" + SBDate + "','" + RBIApprNo + "','" + RBIApprDate + "','" + IsGRWaived + "',");
                Query.Append("'" + GRNo + "','" + GRDate + "','" + RBIWaiverNo + "','" + RBIWavierDate + "','" + BankDealer + "','" + ACNo + "','" + BankDealerCode + "','" + EPZCode + "','" + Notify + "','" + Address + "','" + Address1 + "','" + CreatedBy + "','" + CreatedDate + "')");
                Result = CommonDL.ExecuteNonQuery(Query.ToString());

            }
            catch (Exception ex)
            {
                Message = ex.Message;
            }
            return Result;
        }

        public int ExporterUpdate(string JobNo, string ExporterName, string ExporterAddress, string ExporterAddress1, string BranchSno, string StateProvince, string IECodeNo, string Consignee,
                               string ConsigneeAddress, string ConsigneeAddress1, string ConsigneeCountry, bool IsBuyer,string Buyer, string BuyerAddress, string BuyerAddress1, string ExporterRefNo,
                               string ExporterRefDate, string ExporterType, string SBNo, string SBDate, string RBIApprNo, string RBIApprDate,bool IsGRWaived, string GRNo, string GRDate, string RBIWaiverNo,
                               string RBIWavierDate, string BankDealer, string ACNo, string BankDealerCode, string EPZCode, string Notify, string Address, string Address1, string ModifiedBy, string ModifiedDate)
        {
            string Message = string.Empty;
            StringBuilder Query = new StringBuilder();
            int Result = 0;
            try
            {
                Query.Append("UPDATE [E_T_Exporter] SET ");
                Query.Append("[ExporterName] = '" + ExporterName + "',[ExporterAddress] = '" + ExporterAddress + "',[ExporterAddress1] = '" + ExporterAddress1 + "',[BranchSno] = '" + BranchSno + "',");
                Query.Append("[StateProvince] = '" + StateProvince + "',[IECodeNo] = '" + IECodeNo + "',[Consignee] = '" + Consignee + "',[ConsigneeAddress] = '" + ConsigneeAddress + "',[ConsigneeAddress1] = '" + ConsigneeAddress1 + "',");
                Query.Append("[ConsigneeCountry] = '" + ConsigneeCountry + "',[IsBuyer] = '" + IsBuyer + "',[Buyer] = '" + Buyer + "',[BuyerAddress] = '" + BuyerAddress + "',[BuyerAddress1] = '" + BuyerAddress1 + "',[ExporterRefNo] = '" + ExporterRefNo + "',");
                Query.Append("[ExporterRefDate] = '" + ExporterRefDate + "',[ExporterType] = '" + ExporterType + "',[SBNo] = '" + SBNo + "',[SBDate] = '" + SBDate + "',[RBIApprNo] = '" + RBIApprNo + "',");
                Query.Append("[RBIApprDate] = '" + RBIApprDate + "',[IsGRWaived] = '" + IsGRWaived + "',[GRNo] = '" + GRNo + "',[GRDate] = '" + GRDate + "',[RBIWaiverNo] = '" + RBIWaiverNo + "',[RBIWavierDate] = '" + RBIWavierDate + "',[BankDealer] = '" + BankDealer + "',");
                Query.Append("[ACNo] = '" + ACNo + "',[BankDealerCode] = '" + BankDealerCode + "',[EPZCode] = '" + EPZCode + "',[Notify] = '" + Notify + "',[Address] = '" + Address + "',[Address1] = '" + Address1 + "',");
                Query.Append("[ModifiedBy] = '" + ModifiedBy + "',[ModifiedDate] = '" + ModifiedDate + "' ");

                Query.Append(" where [JobNo]='" + JobNo + "'");

                Result = CommonDL.ExecuteNonQuery(Query.ToString());
            }
            catch (Exception ex)
            {
                Message = ex.Message;
            }
            return Result;
        }

        public DataSet GetExportData(string JobNo)
        {
            StringBuilder Query = new StringBuilder();
            string Message = string.Empty;
            DataSet ds = new DataSet();
            try
            {
                Query.Append("Select [JobNo],[ExporterName],[ExporterAddress],[ExporterAddress1],[BranchSno],[StateProvince],[IECodeNo],[Consignee],[ConsigneeAddress],");
                Query.Append("[ConsigneeAddress1],[ConsigneeCountry],[IsBuyer],[Buyer],[BuyerAddress],[BuyerAddress1],[ExporterRefNo],[ExporterRefDate],[ExporterType],");
                Query.Append("[SBNo],[SBDate],[RBIApprNo],[RBIApprDate],[IsGRWaived],[GRNo],[GRDate],[RBIWaiverNo],[RBIWavierDate],[BankDealer],[ACNo],[BankDealerCode],[EPZCode],");
                Query.Append("[Notify],[Address],[Address1]");

                Query.Append(" FROM [E_T_Exporter]  where JobNo='" + JobNo + "'");

                ds =  CommonDL.GetDataSet(Query.ToString());
            }
            catch (Exception ex)
            {
                Message = ex.Message;
            }
            return ds;
        }
    }
}
