using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
namespace VTS.ImpexCube.Data
{
 
    public class ETAnnexureDocumentsDL
    {
        CommonDL CommonDL = new CommonDL();
        public int SaveAnnexureDocuments(string JobNo, string Sno, string DocumentName, string CreatedBy, string CreatedDate)
        {
            string Message = string.Empty;
            StringBuilder Query = new StringBuilder();
            int Result = 0;
            try
            {
                Query.Append(" INSERT INTO [E_T_AnnexureDocuments] ");
                Query.Append("([JobNo],[Sno],[DocumentName],[CreatedBy],[CreatedDate])");
                Query.Append("VALUES");
                Query.Append("('"+JobNo + "','"+Sno +"','" +DocumentName +"','"+CreatedBy +"','"+ CreatedDate+"')");
                Result = CommonDL.ExecuteNonQuery(Query.ToString());
            }
            catch (Exception ex)
            {
                Message = ex.Message;
            }
            return Result;
        }
        public int UpdateAnnexureDocuments(string JobNo, string Sno, string DocumentName, string ModifiedBy, string ModifiedDate)
        {
            string Message = string.Empty;
            StringBuilder Query = new StringBuilder();
            int Result = 0;
            try
            {
                Query.Append(" UPDATE [E_T_AnnexureDocuments] ");
                Query.Append(" SET [Sno] ='"+Sno +"',[DocumentName] ='"+ DocumentName+"',[ModifiedBy] ='"+ ModifiedBy+"',[ModifiedDate] ='"+ ModifiedDate+"' ");
                Query.Append(" where [JobNo]='" + JobNo + "'");
                Result = CommonDL.ExecuteNonQuery(Query.ToString());
            }
            catch (Exception ex)
            {
                Message = ex.Message;
            }
            return Result;
        }

        public DataSet GetAnnexureData(string Sno, string JobNo)
        {
            StringBuilder Query = new StringBuilder();
            string Message = string.Empty;
            DataSet ds = new DataSet();
            try
            {
                Query.Append("SELECT [ID],[JobNo],[Sno],[DocumentName],[CreatedBy],[CreatedDate],[ModifiedBy],[ModifiedDate] ");
                Query.Append("FROM [E_T_AnnexureDocuments] where [Sno]='" + Sno + "' and [JobNo]='" + JobNo + "'");

                ds = CommonDL.GetDataSet(Query.ToString());
            }
            catch (Exception ex)
            {
                Message = ex.Message;
            }
            return ds;
        }

        public DataSet GetAnnexureData()
        {
            StringBuilder Query = new StringBuilder();
            string Message = string.Empty;
            DataSet ds = new DataSet();
            try
            {
                Query.Append("SELECT distinct JobNo,Sno,DocumentName FROM E_T_AnnexureDocuments ");
                ds = CommonDL.GetDataSet(Query.ToString());
            }
            catch (Exception ex)
            {
                Message = ex.Message;
            }
            return ds;
        }
        public DataSet GetAnnexure(string JobNo)
        {
            StringBuilder Query = new StringBuilder();
            string Message = string.Empty;
            DataSet ds = new DataSet();
            try
            {
                Query.Append("SELECT [ID],[JobNo],[Sno],[DocumentName],[CreatedBy],[CreatedDate],[ModifiedBy],[ModifiedDate] ");
                Query.Append("FROM [E_T_AnnexureDocuments] where [JobNo]='" + JobNo + "'");

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
