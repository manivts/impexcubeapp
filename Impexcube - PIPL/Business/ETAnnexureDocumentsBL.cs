using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VTS.ImpexCube.Data;
using System.Data;
namespace VTS.ImpexCube.Business
{
    public class ETAnnexureDocumentsBL
    {
        ETAnnexureDocumentsDL ETAnnexureDocumentsDL = new ETAnnexureDocumentsDL();
        public int  SaveAnnexureDocuments(string JobNo, string Sno, string DocumentName, string CreatedBy, string CreatedDate)
        {
           return ETAnnexureDocumentsDL.SaveAnnexureDocuments(JobNo, Sno, DocumentName, CreatedBy, CreatedDate);
        }
        public int UpdateAnnexureDocuments(string JobNo, string Sno, string DocumentName, string ModifiedBy, string ModifiedDate)
        {
            return ETAnnexureDocumentsDL.UpdateAnnexureDocuments(JobNo, Sno, DocumentName, ModifiedBy, ModifiedDate);
        }
        public DataSet GetAnnexureData(string Sno, string JobNo)
        {
            return ETAnnexureDocumentsDL.GetAnnexureData(Sno, JobNo);
        }
        public DataSet GetAnnexureData()
        {
            return ETAnnexureDocumentsDL.GetAnnexureData();
        }
        public DataSet GetAnnexure(string JobNo)
        {
            return ETAnnexureDocumentsDL.GetAnnexure(JobNo);
        }

    }
}
