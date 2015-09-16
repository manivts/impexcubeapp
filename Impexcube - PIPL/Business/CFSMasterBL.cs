using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace VTS.ImpexCube.Business
{
    public class CFSMasterBL
    {
        VTS.ImpexCube.Data.CFSMasterDL objCFSMaster = new VTS.ImpexCube.Data.CFSMasterDL();

        public int InsertCFSMaster(string cfsname, string address, string contact, string email, string favor)
        {
            return objCFSMaster.InsertCFSMaster(cfsname, address, contact, email, favor);
        }

        public DataSet SelectCFSMaster()
        {
            return objCFSMaster.SelectCFSMaster();
        }

        public int UpdateCFSMaster(int id, string cfsname, string address, string contact, string email, string favor)
        {
            return objCFSMaster.UpdateCFSMaster(id,cfsname,address,contact,email,favor);
        }
    }
}
