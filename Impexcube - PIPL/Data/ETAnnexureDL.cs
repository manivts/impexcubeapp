using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
namespace VTS.ImpexCube.Data
{
    public class ETAnnexureDL
    {
        CommonDL CommonDL = new CommonDL();

        public int AnnexureSave(string JobNo, string IECodeOfEOU, string BranchSNo, string ExaminationDate, string ExaminingOfficer, string ExaminingOfficerDesignation, string SupervisingOfficer, 
                         string SupervisingOfficerDesignation, string Commissionerate, string Division,string Range,string VerifiedbyExaminingOfficer,string SampleForwarded,string SealNumber,string CreatedBy,string CreatedDate)
        {
            StringBuilder Query = new StringBuilder();
            string Message = string.Empty;
            int Result=0;
            try
            {
                Query.Append("INSERT INTO [E_T_Annexure]");
                Query.Append("([JobNo],[IECodeOfEOU],[BranchSNo],[ExaminationDate] ,[ExaminingOfficer],[ExaminingOfficerDesignation],[SupervisingOfficer],[SupervisingOfficerDesignation],");
                Query.Append("[Commissionerate],[Division],[Range],[VerifiedbyExaminingOfficer],[SampleForwarded],[SealNumber],[CreatedBy],[CreatedDate])");
                Query.Append("Values(");
                Query.Append("'" + JobNo + "','" + IECodeOfEOU + "','" + BranchSNo + "','" + ExaminationDate + "','" + ExaminingOfficer + "','" + ExaminingOfficerDesignation + "','" + SupervisingOfficer + "','" + SupervisingOfficerDesignation + "',");
                Query.Append("'" + Commissionerate + "','" + Division + "','" + Range + "','" + VerifiedbyExaminingOfficer + "','" + SampleForwarded + "','" + SealNumber + "','" + CreatedBy + "','" + CreatedDate + "')");
                Result = CommonDL.ExecuteNonQuery(Query.ToString());
            }
            catch (Exception ex)
            {
                Message = ex.Message;
            }
            return Result;
        }

        public int AnnexureUpdate(string JobNo, string IECodeOfEOU, string BranchSNo, string ExaminationDate, string ExaminingOfficer, string ExaminingOfficerDesignation, string SupervisingOfficer,
                         string SupervisingOfficerDesignation, string Commissionerate, string Division, string Range, string VerifiedbyExaminingOfficer, string SampleForwarded, string SealNumber, string ModifiedBy, string ModifiedDate)
        {
            StringBuilder Query = new StringBuilder();
            string Message = string.Empty;
             int Result=0;
            try
            {
                Query.Append("UPDATE [E_T_Annexure] ");
                Query.Append(" SET [IECodeOfEOU] = '" + IECodeOfEOU + "',[BranchSNo] = '" + BranchSNo + "',[ExaminationDate] = '" + ExaminationDate + "',[ExaminingOfficer] = '" + ExaminingOfficer + "',[ExaminingOfficerDesignation] = '" + ExaminingOfficerDesignation + "',[SupervisingOfficer] = '" + SupervisingOfficer + "',");
                Query.Append("[SupervisingOfficerDesignation] = '" + SupervisingOfficerDesignation + "',[Commissionerate] = '" + Commissionerate + "',[Division] = '" + Division + "',[Range] = '" + Range + "',[VerifiedbyExaminingOfficer] = '" + VerifiedbyExaminingOfficer + "',[SampleForwarded] = '" + SampleForwarded + "',[SealNumber] = '" + SealNumber + "',[ModifiedBy] ='" + ModifiedBy + "',[ModifiedDate] ='" + ModifiedDate + "' ");
                Query.Append(" where [JobNo]='" + JobNo + "' ");
                CommonDL.ExecuteNonQuery(Query.ToString());
                Result = CommonDL.ExecuteNonQuery(Query.ToString());
            }
            catch (Exception ex)
            {
                Message = ex.Message;
            }
              return Result;
        }

        public DataSet GetAnnexureData(string JobNo)
        {
            StringBuilder Query = new StringBuilder();
            DataSet ds = new DataSet();
            string Message = string.Empty;
            try
            {
                Query.Append(" SELECT [ID],[JobNo],[IECodeOfEOU],[BranchSNo],[ExaminationDate],[ExaminingOfficer],[ExaminingOfficerDesignation],[SupervisingOfficer],");
                Query.Append(" [SupervisingOfficerDesignation],[Commissionerate],[Division],[Range],[VerifiedbyExaminingOfficer],[SampleForwarded],[SealNumber],[CreatedBy],[CreatedDate],[ModifiedBy],[ModifiedDate] ");
                Query.Append(" FROM [E_T_Annexure] where [JobNo]='" + JobNo + "'");
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
