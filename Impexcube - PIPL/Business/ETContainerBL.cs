using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VTS.ImpexCube.Data;
using System.Data;
namespace VTS.ImpexCube.Business
{
    public class ETContainerBL
    {
        ETContainerDL ETContainerDL = new ETContainerDL();
        public int SaveContainer(string JobNo, string ContainerNo, string SealNo, string SealDate, string Type, string NoofPktsStuffed, string CreatedBy, string CreatedDate)
        {
            return ETContainerDL.SaveContainer(JobNo, ContainerNo, SealNo, SealDate, Type, NoofPktsStuffed, CreatedBy, CreatedDate);
        }

        public int UpdateContainer(string ID,string JobNo, string ContainerNo, string SealNo, string SealDate, string Type, string NoofPktsStuffed, string ModifiedBy, string ModifiedDate)
        {
            return ETContainerDL.UpdateContainer(ID,JobNo, ContainerNo, SealNo, SealDate, Type, NoofPktsStuffed, ModifiedBy, ModifiedDate);
        }

        public DataSet GetContainerData(string JobNo)
        {
            return ETContainerDL.GetContainerData(JobNo);
        }
    }
}
