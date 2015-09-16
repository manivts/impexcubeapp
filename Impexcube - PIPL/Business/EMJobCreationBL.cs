using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using VTS.ImpexCube.Data;
using System.Data;
namespace VTS.ImpexCube.Business
{
    public class EMJobCreationBL
    {
        EMJobCreationDL EMJobCreationDL = new EMJobCreationDL();

        public int JobCreationSave(string JobNo, string JobDate, string JobReceivedOn, string TransportMode, string CustomHouse, string Filling,string TotNoInv,string TotInvValue,string Currency, string CreatedBy, string CreatedDate, string Fyear)
        {
            return EMJobCreationDL.JobCreationSave(JobNo, JobDate, JobReceivedOn, TransportMode, CustomHouse, Filling, TotNoInv, TotInvValue, Currency, CreatedBy, CreatedDate, Fyear);
        }

        public int JobCreationUpdate(string JobNo, string JobDate, string JobReceivedOn, string TransportMode, string CustomHouse, string Filling,string TotNoInv,string TotInvValue,string Currency ,string ModifiedBy, string ModifiedDate)
        {
            return EMJobCreationDL.JobCreationUpdate(JobNo, JobDate, JobReceivedOn, TransportMode, CustomHouse, Filling,TotNoInv, TotInvValue, Currency, ModifiedBy, ModifiedDate);
        }

        public DataSet GetData(string JobNo)
        {
            return EMJobCreationDL.GetData(JobNo);
        }

        public DataSet GetGridData(string fyear)
        {
            return EMJobCreationDL.GetGridData(fyear);
        }

        public DataSet JobNo()
        {
            return EMJobCreationDL.JobNo();
        }

        public DataSet CustomHouse()
        {
            return EMJobCreationDL.CustomHouse(); 
        }

        public int updateautono(int keycode)
        {
            return EMJobCreationDL.updateautono(keycode);
        }

    }
}
