using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using VTS.ImpexCube.Data;

namespace VTS.ImpexCube.Business
{
    public class ETAnnexureBL
    {
        ETAnnexureDL ETAnnexureDL = new ETAnnexureDL();
        public int AnnexureSave(string JobNo, string IECodeOfEOU, string BranchSNo, string ExaminationDate, string ExaminingOfficer, string ExaminingOfficerDesignation, string SupervisingOfficer,
                        string SupervisingOfficerDesignation, string Commissionerate, string Division, string Range, string VerifiedbyExaminingOfficer, string SampleForwarded, string SealNumber, string CreatedBy, string CreatedDate)
        {
            return ETAnnexureDL.AnnexureSave(JobNo, IECodeOfEOU, BranchSNo, ExaminationDate, ExaminingOfficer, ExaminingOfficerDesignation, SupervisingOfficer,
                         SupervisingOfficerDesignation, Commissionerate, Division, Range, VerifiedbyExaminingOfficer, SampleForwarded, SealNumber, CreatedBy, CreatedDate);
        }

        public int AnnexureUpdate(string JobNo, string IECodeOfEOU, string BranchSNo, string ExaminationDate, string ExaminingOfficer, string ExaminingOfficerDesignation, string SupervisingOfficer,
                         string SupervisingOfficerDesignation, string Commissionerate, string Division, string Range, string VerifiedbyExaminingOfficer, string SampleForwarded, string SealNumber, string ModifiedBy, string ModifiedDate)
        {
            return ETAnnexureDL.AnnexureUpdate(JobNo, IECodeOfEOU, BranchSNo, ExaminationDate, ExaminingOfficer, ExaminingOfficerDesignation, SupervisingOfficer,
                         SupervisingOfficerDesignation, Commissionerate, Division, Range, VerifiedbyExaminingOfficer, SampleForwarded, SealNumber, ModifiedBy, ModifiedDate);
        }

        public DataSet GetAnnexureData(string JobNo)
        {
            return ETAnnexureDL.GetAnnexureData(JobNo);
        }
    }
}
