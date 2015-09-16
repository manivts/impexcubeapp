using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
namespace VTS.ImpexCube.Business
{
    public class ETProductQuotaBL
    {
        VTS.ImpexCube.Data.ETProductQuotaDL ETProductQuotaDL = new VTS.ImpexCube.Data.ETProductQuotaDL();

        public int save(string JobNo, string InvoiceNo,  string QuotaCertificateNo, string Agency, string ExpiryDate, string Quantity, string Unit, string CreatedBy, string CreatedDate)
        {
            return ETProductQuotaDL.save(JobNo, InvoiceNo, QuotaCertificateNo, Agency, ExpiryDate, Quantity, Unit, CreatedBy, CreatedDate);           
        }

        public int update(string QuotaID,string JobNo, string InvoiceNo,  string QuotaCertificateNo, string Agency, string ExpiryDate, string Quantity, string Unit, string ModifiedBy, string ModifiedDate)
        {
            return ETProductQuotaDL.update(QuotaID,JobNo, InvoiceNo, QuotaCertificateNo, Agency, ExpiryDate, Quantity, Unit, ModifiedBy, ModifiedDate);
        }
        public DataSet GetProductQuotaData(string JobNo, string InvoiceNo)
        {
            return ETProductQuotaDL.GetProductQuotaData(JobNo,InvoiceNo);
        }
    }
}
