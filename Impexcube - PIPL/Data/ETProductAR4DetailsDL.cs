using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
namespace VTS.ImpexCube.Data
{

    public class ETProductAR4DetailsDL
    {
        string Message = string.Empty;
        CommonDL CommonDL = new CommonDL();

        public int save(string JobNo, string InvoiceNo,  string AR4No, string AR4Date, string Commissionerate, string Division, string Range, string Remark, string CreatedBy, string CreatedDate)
        {
            StringBuilder Query = new StringBuilder();
            int Result = 0;
            try
            {
                Query.Append("INSERT INTO [E_T_ProductAR4Details] ");
                Query.Append("([JobNo],[InvoiceNo],[AR4No],[AR4Date],[Commissionerate],[Division],[Range],[Remark],[CreatedBy],[CreatedDate])");
                Query.Append(" VALUES(");
                Query.Append("'" + JobNo + "','" + InvoiceNo + "','" + AR4No + "','" + AR4Date + "','" + Commissionerate + "',");
                Query.Append("'" + Division + "','" + Range + "','" + Remark + "','" + CreatedBy + "','" + CreatedDate + "')");

                Result = CommonDL.ExecuteNonQuery(Query.ToString());
            }
            catch (Exception e)
            {
                Message = e.Message;

            }
            return Result;
        }

        public int update(string ARId,string JobNo, string InvoiceNo,  string AR4No, string AR4Date, string Commissionerate, string Division, string Range, string Remark, string ModifiedBy, string ModifiedDate)        
        {
            StringBuilder Query = new StringBuilder();
            int Result = 0;
            try
            {
                Query.Append("UPDATE [E_T_ProductAR4Details] SET ");
                Query.Append(" [InvoiceNo] ='" + InvoiceNo + "',[AR4No] ='" + AR4No + "',[AR4Date] ='" + AR4Date + "',");
                Query.Append("[Commissionerate] ='" + Commissionerate + "',[Division] ='" + Division + "',[Range] ='" + Range + "',[Remark] ='" + Remark + "',");
                Query.Append("[ModifiedBy] ='" + ModifiedBy + "',[ModifiedDate] ='" + ModifiedDate + "' ");
                Query.Append(" where [ID] ='" + ARId + "'");
                Result = CommonDL.ExecuteNonQuery(Query.ToString());
            }
            catch (Exception e)
            {
                Message = e.Message;
            }
            return Result;
        }

        public DataSet GetProductAR4Data(string JobNo,string InvoiceNo)
        {
            StringBuilder Query = new StringBuilder();
            string Message = string.Empty;
          
            DataSet ds = new DataSet();
            try
            {
                Query.Append("Select [ID],[JobNo],[InvoiceNo],[AR4No],[AR4Date],[Commissionerate],[Division],[Range],[Remark],[CreatedBy],[CreatedDate],[ModifiedBy] ");
                Query.Append(" FROM [E_T_ProductAR4Details]  where JobNo='" + JobNo + "' and InvoiceNo = '" +InvoiceNo+"'");

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
