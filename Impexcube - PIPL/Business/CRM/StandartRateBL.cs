using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace VTS.ImpexCube.Business.CRM
{
   
    public class StandartRateBL
    {
        VTS.ImpexCube.Data.CRM.StandardRateDL objStandardRate = new VTS.ImpexCube.Data.CRM.StandardRateDL();


        public int InsertStandardRate(string Description, string twentyFeetUnit, string twentyFeetRate, string FourtyFeetUnit, string FourtyFeetRate,
            string LCLUnit, string LCLRate, string FCLUnit, string FCLRate, string CreatedBy, string CreatedDate)
        {
            return objStandardRate.InsertStandardRate(Description, twentyFeetUnit, twentyFeetRate, FourtyFeetUnit, FourtyFeetRate, LCLUnit, LCLRate, FCLUnit, FCLRate, CreatedBy, CreatedDate);
        }

        public DataSet LoadDescription()
        {
            return objStandardRate.LoadDescription();
        }

        public DataSet GridLoad()
        {
            return objStandardRate.GridLoad();
        }

        public int UpdateStandardRate(string Description, string twentyFeetUnit, string twentyFeetRate, string FourtyFeetUnit, string FourtyFeetRate,
          string LCLUnit, string LCLRate, string FCLUnit, string FCLRate, string ModifiedBy, string ModifiedDate, string ID)
        {

            return objStandardRate.UpdateStandardRate(Description, twentyFeetUnit, twentyFeetRate, FourtyFeetUnit, FourtyFeetRate, LCLUnit, LCLRate, FCLUnit, FCLRate,ModifiedBy,ModifiedDate,ID);
        }

    }
}
