using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace VTS.ImpexCube.Business
{
    public class JobCreatiobBL
    {
        VTS.ImpexCube.Data.JobCreationDL JbCrtDL = new VTS.ImpexCube.Data.JobCreationDL();

        public DataSet GetCountry()
        {
            return JbCrtDL.GetCountry();
        }

        public DataSet GetPort()
        {
            return JbCrtDL.GetPort();
        }

        public DataSet GetPortCode(string PortCode)
        {
            return JbCrtDL.GetPortCode(PortCode);
        }
    }
}
