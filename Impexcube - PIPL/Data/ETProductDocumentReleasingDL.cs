using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
namespace VTS.ImpexCube.Data
{


    public class ETProductDocumentReleasingDL
    {
        string Message = string.Empty;
        CommonDL CommonDL = new CommonDL();

        public int save(string JobNo,
           string InvoiceNo,
          
           string DocType,
           string Description, 
           string AgencyCode, 
           string AgencyName, 
           string DocumentName, 
           string CreatedBy, 
           string CreatedDate)
           
        {
            StringBuilder Query = new StringBuilder();
            int Result = 0;
            try 
            {
                Query.Append("INSERT INTO [E_T_ProductDocumentReleasing] ");
                Query.Append("(JobNo, InvoiceNo,  DocType, Description, AgencyCode, AgencyName, DocumentName, CreatedBy, CreatedDate)");
                Query.Append("VALUES(");
                Query.Append("'" + JobNo + "','" + InvoiceNo + "','" + DocType + "','" + Description + "','" + AgencyCode + "',");
                Query.Append("'" + AgencyName + "','" + DocumentName + "','" + CreatedBy + "','" + CreatedDate + "')");

                Result = CommonDL.ExecuteNonQuery(Query.ToString());

            }
            catch (Exception e)
            {
                Message = e.Message;
            }
            return Result;
        }

        public int update(string DocId,string JobNo,
           string InvoiceNo,
         
           string DocType,
           string Description,
           string AgencyCode,
           string AgencyName,
           string DocumentName,
           string ModifiedBy,
           string ModifiedDate)
        {
            int Result = 0;
            try
            {
               
                StringBuilder Query = new StringBuilder();
                Query.Append("UPDATE [E_T_ProductDocumentReleasing] SET ");
                Query.Append(" [InvoiceNo] = '" + InvoiceNo + "',[DocType] = '" + DocType + "',[Description] = '" + Description + "',");
                Query.Append("[AgencyCode] = '" + AgencyCode + "',[AgencyName] = '" + AgencyName + "',[DocumentName] = '" + DocumentName + "',[ModifiedBy] = '" + ModifiedBy + "',[ModifiedDate] = '" + ModifiedDate + "' ");
                Query.Append(" Where [JobNo] = '" + JobNo + "' and [InvoiceNo] = '" + InvoiceNo + "' and ID='" + DocId +"' ");

                Result = CommonDL.ExecuteNonQuery(Query.ToString());
            }
            catch (Exception e)
            {
                Message = e.Message;
            }
            return Result;

        }

        public DataSet GetProductDocumentReleasingData(string JobNo, string InvoiceNo)
        {
            StringBuilder Query = new StringBuilder();
            string Message = string.Empty;

            DataSet ds = new DataSet();
            try
            {
                Query.Append("Select [ID],[JobNo],[InvoiceNo],[SNo],[DocType],[Description],[AgencyCode],[AgencyName],[DocumentName],[CreatedBy],[CreatedDate]");
                Query.Append(",[ModifiedBy],[ModifiedDate] from [E_T_ProductDocumentReleasing] where [JobNo]='" + JobNo + "'and [InvoiceNo] = '" + InvoiceNo + "'");

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
