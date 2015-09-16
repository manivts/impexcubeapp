using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;


namespace VTS.ImpexCube.Business
{

  
    public class JobStageUpdateBL
    {
        VTS.ImpexCube.Data.JobStageUpdateDL objJobStage = new VTS.ImpexCube.Data.JobStageUpdateDL();

        public DataSet SelectStage()
        {
            return objJobStage.SelectStage();
        }

        public DataSet SelectStageStatus(int StageId)
        {
            return objJobStage.SelectStageStatus(StageId);
        }

        public DataSet SearchJobStatusList(string DocFrom, string To, string Importer, string Jobno, string stage, string status)
        {
            return objJobStage.SearchJobStatusList(DocFrom, To, Importer, Jobno, stage, status);
        }

        public DataSet SearchJobStatuspending()
        {
            return objJobStage.SearchJobStatuspending();
        }

        public DataSet GetJobStatusList(string jobno)
        {
            return objJobStage.GetJobStatusList(jobno);
        }

        public int InsertJobStatusMail(string jobno, string stage, string status, string from, string to, string cc, string subject, string comment,string MailAttach,
            string createdby, string createdDate, string modifiedby, string modifiedDate)
        {
            return objJobStage.InsertJobStatusMail(jobno, stage, status, from, to, cc, subject, comment,MailAttach, createdby, createdDate, modifiedby, modifiedDate);
        }

        public int InsertJodStageStatus(string jobno, string importer, string stage, string status, string statusdate, string remarks, string modified)
        {
            return objJobStage.InsertJodStageStatus(jobno, importer, stage, status, statusdate, remarks, modified);
        }

        public int UpdateJodStageStatus(int Id, string jobno, string importer, string stage, string status, string statusdate, string remarks)
        {
            return objJobStage.UpdateJodStageStatus(Id, jobno, importer, stage, status, statusdate, remarks);
        }

        public DataSet SelectJobStageDetails(int id)
        {
            return objJobStage.SelectJobStageDetails(id);
        }

        public DataSet GetStageId(string stage)
        {
            return objJobStage.GetStageId(stage);
        }

        public DataSet GetStageStatusDetails(string jobno, string stage, string status)
        {
            return objJobStage.GetStageStatusDetails(jobno, stage, status);
        }

        public int ModifyJobStageDetails(string jobno, string modified)
        {
            return objJobStage.ModifyJobStageDetails(jobno, modified);
        }

        public DataSet SelectJobDetails(string jobno)
        {
            return objJobStage.SelectJobDetails(jobno);
        }
        public int UpdateBEDeatils(string JobNo, string BENo, string BEDate)
        {
            return objJobStage.UpdateBEDeatils(JobNo,BENo,BEDate);
        }


        public DataSet SelectJobNoBeNo(string Jobno)
        {
            return objJobStage.SelectJobNoBeNo(Jobno);
        }
    }
}
