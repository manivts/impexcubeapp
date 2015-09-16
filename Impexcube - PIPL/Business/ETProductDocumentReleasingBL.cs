using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
namespace VTS.ImpexCube.Business
{
    public class ETProductDocumentReleasingBL
    {
        VTS.ImpexCube.Data.ETProductDocumentReleasingDL ObjETProductDocumentReleasingDL = new VTS.ImpexCube.Data.ETProductDocumentReleasingDL();

        public int  save(string JobNo, string InvoiceNo,  string DocType, string Description, string AgencyCode, string AgencyName, string DocumentName, string CreatedBy, string CreatedDate)
        {
            return ObjETProductDocumentReleasingDL.save(JobNo, InvoiceNo,  DocType, Description, AgencyCode, AgencyName, DocumentName, CreatedBy, CreatedDate);
        }

        public int update(string DocId,string JobNo, string InvoiceNo,  string DocType, string Description, string AgencyCode, string AgencyName, string DocumentName, string ModifiedBy, string ModifiedDate)
        {
            return ObjETProductDocumentReleasingDL.update(DocId,JobNo, InvoiceNo, DocType, Description, AgencyCode, AgencyName, DocumentName, ModifiedBy, ModifiedDate);
        }

        public DataSet GetProductDocumentReleasingData(string JobNo, string InvoiceNo)
        {
            return ObjETProductDocumentReleasingDL.GetProductDocumentReleasingData(JobNo, InvoiceNo);
        }
    }
}
