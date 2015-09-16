// -----------------------------------------------------------------------
// <copyright file="JobSyncBL.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace VTS.ImpexCube.Business
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Data;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class JobSyncBL
    {
        VTS.ImpexCube.Data.JobSyncDL objJobSync = new VTS.ImpexCube.Data.JobSyncDL();

        public DataSet GetJobDetails(string jobno)
        {
            return objJobSync.GetJobDetails(jobno);
        }

        public DataSet GetIworkDetails(string jobno)
        {
            return objJobSync.GetIworkDetails(jobno);
        }
        public DataSet GetIShipDetails(string jobno)
        {
            return objJobSync.GetIShipDetails(jobno);
        }
        public DataSet GetIJobPosDetails(string jobno)
        {
            return objJobSync.GetIJobPosDetails(jobno);
        }

        public DataSet GetPartyDetails()
        {
            return objJobSync.GetPartyDetails();
        }
        public DataSet GetIWorkRegDetails(string FYear)
        {
            return objJobSync.GetIWorkRegDetails(FYear);
        }


        public DataSet SelectJobNo(string jobno)
        {
            return objJobSync.SelectJobNo(jobno);
        }


        public DataSet SelectIworkJobNo(string jobno)
        {
            return objJobSync.SelectIworkJobNo(jobno);
        }

        public DataSet SelectpartyCode(string PARTY_CODE)
        {
            return objJobSync.SelectpartyCode(PARTY_CODE);
        }

        public DataSet SelectIworkReg(string Job_no)
        {
            return objJobSync.SelectIworkReg(Job_no);
        }

        public int InsertJobDetails(string jobno, string JobReceived, string partycode, string partyname, string Mode,
            string invdtl, string betype, string createdby, string createddate, string modifiedby, string modifieddate)
        {
            return objJobSync.InsertJobDetails(jobno, JobReceived, partycode, partyname, Mode, invdtl, betype, createdby, createddate, modifiedby, modifieddate);
        }

        public int InsertIworkDetails(string job_no, string job_date, string comp_jobStage, string comp_jobDate, string comp_remark, string pend_jobStage, string pend_remark,
            string party_code, string jobsno, string INV_DTL, string cont_orig, string ETA, string MAWB_NO, string MAWB_DATE, string NO_OF_PKG,
            string PKG_UNIT, string GROSS_WT, string GROSS_UNIT, string Carrier, string BE_NO, string BE_DATE, string PARTY_NAME, string Status_job, string transport_mode, string bill_no, string org_doc_date,string bill_date)
        {
            return objJobSync.InsertIworkDetails(job_no, job_date, comp_jobStage, comp_jobDate, comp_remark, pend_jobStage, pend_remark, party_code, jobsno, INV_DTL, cont_orig,
                ETA, MAWB_NO, MAWB_DATE, NO_OF_PKG, PKG_UNIT, GROSS_WT, GROSS_UNIT, Carrier, BE_NO, BE_DATE, PARTY_NAME, Status_job, transport_mode, bill_no, org_doc_date, bill_date);
        }

        public int UpdateIworkDetails(string job_no, string job_date, string comp_jobStage, string comp_jobDate, string comp_remark, string pend_jobStage, string pend_remark,
      string party_code, string jobsno, string INV_DTL, string cont_orig, string ETA, string MAWB_NO, string MAWB_DATE, string NO_OF_PKG,
      string PKG_UNIT, string GROSS_WT, string GROSS_UNIT, string Carrier, string BE_NO, string BE_DATE, string PARTY_NAME, string Status_job, string transport_mode, string bill_no, string org_doc_date,string bill_date)
        {
            return objJobSync.UpdateIworkDetails(job_no, job_date, comp_jobStage, comp_jobDate, comp_remark, pend_jobStage, pend_remark, party_code, jobsno, INV_DTL,
                cont_orig, ETA, MAWB_NO, MAWB_DATE, NO_OF_PKG, PKG_UNIT, GROSS_WT, GROSS_UNIT, Carrier, BE_NO, BE_DATE, PARTY_NAME, Status_job, transport_mode, bill_no, org_doc_date,bill_date);
        }

        public int UpdateIShipDetails(string hawb_no, string hawb_date, string job_no, string GLD)
        {
            return objJobSync.UpdateIShipDetails(hawb_no, hawb_date, job_no, GLD);
        }

        public int UpdateIJobPosDetails(string DB_NOTE_NO, string DB_DATE, string job_no)
        {
            return objJobSync.UpdateIJobPosDetails(DB_NOTE_NO, DB_DATE, job_no);
        }

        public int UpdatePartyName(string PARTY_CODE, string PARTY_NAME, string GROUP_ID)
        {
            return objJobSync.UpdatePartyName(PARTY_CODE, PARTY_NAME, GROUP_ID);
        }
        public int UpdateIWorkReg(string JOB_NO, string BE_TYPE, string TOT_ASS_VL, string TOT_DUTY, string doc_received_date)
        {
            return objJobSync.UpdateIWorkReg(JOB_NO, BE_TYPE, TOT_ASS_VL, TOT_DUTY, doc_received_date);
        }

        public int InsertPartyName(string PARTY_CODE, string PARTY_NAME, string GROUP_ID)
        {
            return objJobSync.InsertPartyName(PARTY_CODE, PARTY_NAME, GROUP_ID);
        }

        public int UpdateJobDetails(string jobno, string JobReceived, string partycode, string partyname, string Mode,
            string invdtl, string betype, string modifiedby, string modifieddate)
        {
            return objJobSync.UpdateJobDetails(jobno,JobReceived, partycode, partyname, Mode, invdtl, betype, modifiedby, modifieddate);
        }

        public int UpdateJobStatus(string jobno, string Status_Job)
        {
            return objJobSync.UpdateJobStatus(jobno,Status_Job);
        }

        public DataSet GetIPurchaseDetails(string FYear)
        {
            return objJobSync.GetIPurchaseDetails(FYear);
        }

        public DataSet GetIProductDetails(string FYear)
        {
            return objJobSync.GetIProductDetails(FYear);
        }

        public DataSet GetSProductDetails()
        {
            return objJobSync.GetSProductDetails();
        }

        public DataSet GetIInvoiceDetails(string FYear)
        {
            return objJobSync.GetIInvoiceDetails(FYear);
        }

        public DataSet GetIInvoiceCharges(string FYear)
        {
            return objJobSync.GetIInvoiceCharges(FYear);
        }

        public DataSet GetSInvoiceDetails()
        {
            return objJobSync.GetSInvoiceDetails();
        }

        public DataSet GetSInvoiceCharges()
        {
            return objJobSync.GetSInvoiceCharges();
        }

        public DataSet SelectProductDetails(string jobNo)
        {
            return objJobSync.SelectProductDetails(jobNo);
        }

        public DataSet SelectInvoiceDetails(string jobNo)
        {
            return objJobSync.SelectInvoiceDetails(jobNo);
        }

        public DataSet SelectIPurchaseDetails(string jobno)
        {
            return objJobSync.SelectIPurchaseDetails(jobno);
        }

    }
}
