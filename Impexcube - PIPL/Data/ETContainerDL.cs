using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
namespace VTS.ImpexCube.Data
{
    public class ETContainerDL
    {
        CommonDL CommonDL = new CommonDL();
        public int SaveContainer(string JobNo, string ContainerNo, string SealNo, string SealDate, string Type, string NoofPktsStuffed, string CreatedBy, string CreatedDate)
        {
            string Message = string.Empty;
            int Result = 0;
            StringBuilder Query = new StringBuilder();
            try
            {
                Query.Append(" INSERT INTO [E_T_Container] ");
                Query.Append("([JobNo],[ContainerNo],[SealNo],[SealDate],[Type],[NoofPktsStuffed],[CreatedBy],[CreatedDate])");
                Query.Append("VALUES");
                Query.Append("('"+ JobNo+"','"+ContainerNo +"','"+SealNo +"','"+ SealDate+"','"+ Type+"','"+NoofPktsStuffed +"','"+CreatedBy +"','"+ CreatedDate+"')");
                Result = CommonDL.ExecuteNonQuery(Query.ToString());
            }
            catch (Exception ex)
            {
                Message = ex.Message;
            }
            return Result;
        }

        public int UpdateContainer(string ID,string JobNo, string ContainerNo, string SealNo, string SealDate, string Type, string NoofPktsStuffed, string ModifiedBy, string ModifiedDate)
        {
            string Message = string.Empty;
            StringBuilder Query = new StringBuilder();
            int Result = 0;
            try
            {
                Query.Append(" UPDATE [E_T_Container] ");
                Query.Append("SET [ContainerNo] ='" + ContainerNo + "',[SealNo] ='" + SealNo + "',[SealDate] ='" + SealDate + "',[Type] ='" + Type + "',[NoofPktsStuffed] ='" + NoofPktsStuffed + "',[ModifiedBy] ='" + ModifiedBy + "',[ModifiedDate] ='" + ModifiedDate + "'");
                Query.Append(" where [ID]='" + ID + "'");
                Result = CommonDL.ExecuteNonQuery(Query.ToString());
            }
            catch (Exception ex)
            {
                Message = ex.Message;
            }
            return Result;
        }

        public DataSet GetContainerData(string JobNo)
        {
            StringBuilder Query = new StringBuilder();
            string Message = string.Empty;
            DataSet ds = new DataSet();
            try
            {
                Query.Append("SELECT [ID],[JobNo],[ContainerNo],[SealNo],[SealDate],[Type],[NoofPktsStuffed],[CreatedBy],[CreatedDate],[ModifiedBy],[ModifiedDate] ");
                Query.Append("FROM [E_T_Container] where [JobNo]='" + JobNo + "'");

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
