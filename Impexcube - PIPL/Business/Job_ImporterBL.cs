using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using VTS.ImpexCube.Data;

namespace VTS.ImpexCube.Business
{
    public class Job_ImporterBL
    {
        VTS.ImpexCube.Data.Job_ImporterDL JbImpDL = new VTS.ImpexCube.Data.Job_ImporterDL();

        public DataSet GetImporterDet()
        {
            return JbImpDL.GetImporterDet();
        }

        public DataSet GetImporter_ID(string ImpID)
        {
            return JbImpDL.GetImporterDet_ID(ImpID);
        }

        public int InsertJobImport(string JobNo, string DocRecd, string TransMode, string JobCity, string JobPort, string JobBE, string JobOPDoc, string ImpName, string ImpAddress,
            string ImpCity, string ImpContact, string ImpState, string ImpPortal, string ImpEmail, string ImpPhone, string ImpFax, string ImpBin, string ImpIECode)
        {
            return JbImpDL.InsertJobImport(JobNo,DocRecd,TransMode, JobCity, JobPort, JobBE, JobOPDoc, ImpName, ImpAddress, ImpCity,ImpContact, ImpState, ImpPortal,ImpEmail,ImpPhone, ImpFax, ImpBin, ImpIECode);
        }
    }
}
