using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace VTS.ImpexCube.Business
{
 
    
    public class JobImportCreationBL
    {
        VTS.ImpexCube.Data.JobImportCreationDL objJobImportCreation = new VTS.ImpexCube.Data.JobImportCreationDL();

        public int insertJobImportCreation(string JobNo, string JobDate, string ShipmentType, string TypeofShipment, string ImpxExpName, string ImpExpCode, string ImpExpBranchCode,
           string ImpExpAddress, string ImpExpClassType, string PortOfOrigin, string OriginPortCode, string OriginStateCode, string OriginCountryCode, string PortOfDestination,
           string DestinationPortCode, string DestinationStateCode, string DestinationCountryCode, string InvoiceNo, string InvoiceDate, string ConsigneeAddress, string ForeignExchangeBankCode,string FYear, string CreatedBy, string CreatedDate, string ModifiedBy, string ModifiedDate)
        {

            return objJobImportCreation.insertJobImportCreation(JobNo, JobDate, ShipmentType, TypeofShipment, ImpxExpName, ImpExpCode, ImpExpBranchCode,
                ImpExpAddress, ImpExpClassType, PortOfOrigin, OriginPortCode, OriginStateCode, OriginCountryCode, PortOfDestination, DestinationPortCode,
                DestinationStateCode, DestinationCountryCode, InvoiceNo, InvoiceDate, ConsigneeAddress, ForeignExchangeBankCode, FYear, CreatedBy, CreatedDate, ModifiedBy, ModifiedDate);
        }
        public DataSet GridJobCreation()
        {

            return objJobImportCreation.GridJobCreation();
        }

        public DataSet GridJobNo()
        {
            return objJobImportCreation.GridJobNo();
        }

        public DataSet SelectJobNo(string JobNo)
        {
            return objJobImportCreation.SelectJobNo(JobNo);
        }

        public DataSet SelectJobCreated(string jobDate)
        {
            return objJobImportCreation.SelectJobCreated(jobDate);
        }

        public int UpdateJobImportCreation(string JobNo, string JobDate, string ShipmentType, string TypeofShipment, string ImpxExpName, string ImpExpCode, string ImpExpBranchCode,
         string ImpExpAddress, string ImpExpClassType, string PortOfOrigin, string OriginPortCode, string OriginStateCode, string OriginCountryCode, string PortOfDestination,
         string DestinationPortCode, string DestinationStateCode, string DestinationCountryCode, string InvoiceNo, string InvoiceDate, string ConsigneeAddress, string ForeignExchangeBankCode, string ModifiedBy, string ModifiedDate, string TransId)
        {

            return objJobImportCreation.UpdateJobImportCreation(JobNo, JobDate, ShipmentType, TypeofShipment, ImpxExpName, ImpExpCode, ImpExpBranchCode, ImpExpAddress, ImpExpClassType,
                PortOfOrigin, OriginPortCode, OriginStateCode, OriginCountryCode, PortOfDestination, DestinationPortCode, DestinationStateCode, DestinationCountryCode, InvoiceNo, InvoiceDate, ConsigneeAddress, ForeignExchangeBankCode, ModifiedBy, ModifiedDate,TransId);

        }
    }
}
