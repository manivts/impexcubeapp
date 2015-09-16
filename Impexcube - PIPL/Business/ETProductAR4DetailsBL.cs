using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
namespace VTS.ImpexCube.Business
{
    public class ETProductAR4DetailsBL
    {
        VTS.ImpexCube.Data.ETProductAR4DetailsDL ObjETProductA4DetailsBL = new VTS.ImpexCube.Data.ETProductAR4DetailsDL();

        public int  save(string JobNo,string InvoiceNo,string AR4No,string AR4Date,string Commissionerate,string Division,string Range,string Remark,string CreatedBy,string CreatedDate)
        {
            return ObjETProductA4DetailsBL.save(JobNo, InvoiceNo,  AR4No, AR4Date, Commissionerate, Division, Range, Remark, CreatedBy, CreatedDate);
        }

        public int update(string ARId,string JobNo, string InvoiceNo,  string AR4No, string AR4Date, string Commissionerate, string Division, string Range, string Remark, string ModifiedBy, string ModifiedDate)
        {
            return ObjETProductA4DetailsBL.update(ARId,JobNo, InvoiceNo, AR4No, AR4Date, Commissionerate, Division, Range, Remark, ModifiedBy, ModifiedDate);
        }

        public DataSet GetProductAR4Data(string JobNo,string InvoiceNo)
        {
            return ObjETProductA4DetailsBL.GetProductAR4Data(JobNo, InvoiceNo);
        }
    }
}
