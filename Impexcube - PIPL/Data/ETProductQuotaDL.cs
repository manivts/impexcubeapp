using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
namespace VTS.ImpexCube.Data
{
 
    public class ETProductQuotaDL
    {
        string Message = string.Empty;
        CommonDL CommonDL = new CommonDL();

        public int save(string JobNo,
           string InvoiceNo,
           string QuotaCertificateNo,
           string Agency, 
           string ExpiryDate, 
           string Quantity, 
           string Unit, 
           string CreatedBy, 
           string CreatedDate)
        {
            StringBuilder Query = new StringBuilder();
            int Result = 0;
            try 
            {
                Query.Append("INSERT INTO E_T_ProductQuota ");
                Query.Append("([JobNo],[InvoiceNo],[QuotaCertificateNo],[Agency],[ExpiryDate],[Quantity],[Unit],[CreatedBy],[CreatedDate])");
                Query.Append("VALUES(");
                Query.Append("'" + JobNo + "','" + InvoiceNo + "','" + QuotaCertificateNo + "','" + Agency + "','" + ExpiryDate + "',");
                Query.Append("'" + Quantity + "','" + Unit + "','" + CreatedBy + "','" + CreatedDate + "')");

                Result = CommonDL.ExecuteNonQuery(Query.ToString());

            }
            catch (Exception e)
            {
                Message = e.Message;

            }
            return Result;
        }

        public int update(string QuotaID,string JobNo,
           string InvoiceNo,
           string QuotaCertificateNo,
           string Agency,
           string ExpiryDate,
           string Quantity,
           string Unit,
           string ModifiedBy,
           string ModifiedDate)
        {
            int Result = 0;
            try
            {

                StringBuilder Query = new StringBuilder();
                Query.Append("UPDATE [E_T_ProductQuota] SET ");
                Query.Append("[JobNo] = '" + JobNo + "',[InvoiceNo] = '" + InvoiceNo + "',[QuotaCertificateNo] = '" + QuotaCertificateNo + "',[Agency] = '" + Agency + "',");
                Query.Append("[ExpiryDate] = '" + ExpiryDate + "',[Quantity] = '" + Quantity + "',[Unit] = '" + Unit + "',[ModifiedBy] = '" + ModifiedBy + "',[ModifiedDate] = '" + ModifiedDate + "' ");
                Query.Append("Where [ID]='" + QuotaID + "'");

                Result = CommonDL.ExecuteNonQuery(Query.ToString());
            }
            catch (Exception e)
            {
                Message = e.Message;
            }
            return Result;
        }

        public DataSet GetProductQuotaData(string JobNo,string InvoiceNo)
        {
            StringBuilder Query = new StringBuilder();
            string Message = string.Empty;

            DataSet ds = new DataSet();
            try
            {
                Query.Append("SELECT [ID],[JobNo],[InvoiceNo],[Sno],[QuotaCertificateNo],[Agency],[ExpiryDate],[Quantity],[Unit],");
                Query.Append("[CreatedBy],[CreatedDate],[ModifiedBy],[ModifiedDate] from [E_T_ProductQuota] where [JobNo]='" + JobNo + "' and [InvoiceNo] = '"+ InvoiceNo+"'");

                ds = CommonDL.GetDataSet(Query.ToString());
            }
            catch (Exception ex)
            {
                Message = ex.Message;
            }
            return ds;
        }

    }
}
