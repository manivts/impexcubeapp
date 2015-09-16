using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace VTS.ImpexCube.Business.CRM
{
   
    public class frmQuoteBL
    {
        VTS.ImpexCube.Data.CRM.QuoteDL objQuote = new VTS.ImpexCube.Data.CRM.QuoteDL();


        public int InsertQuote(string Customer, string Description, string twentyFeetUnit, string twentyFeetRate, string FourtyFeetUnit, string FourtyFeetRate,
          string LCLUnit, string LCLRate, string FCLUnit, string FCLRate, string CreatedBy, string CreatedDate)
        {
            return objQuote.InsertQuote(Customer,Description, twentyFeetUnit, twentyFeetRate, FourtyFeetUnit, FourtyFeetRate, LCLUnit, LCLRate, FCLUnit, FCLRate, CreatedBy, CreatedDate);
        }

        public DataSet LoadDescription()
        {
            return objQuote.LoadDescription();
        }

        public DataSet GridLoad(string customer)
        {
            return objQuote.GridLoad(customer);
        }

        public DataSet LoadCustomer()
        {
            return objQuote.LoadCustomer();
        }

        public int UpdateQuote(string Description, string twentyFeetUnit, string twentyFeetRate, string FourtyFeetUnit, string FourtyFeetRate,
            string LCLUnit, string LCLRate, string FCLUnit, string FCLRate, string ModifiedBy, string ModifiedDate, string ID)
        {
            return objQuote.UpdateQuote(Description, twentyFeetUnit, twentyFeetRate, FourtyFeetUnit, FourtyFeetRate, LCLUnit, LCLRate, FCLUnit, FCLRate, ModifiedBy, ModifiedDate, ID);
        }

        public DataSet StandardRate(string Charge)
        {
            return objQuote.StandardRate(Charge);
        }


    }
}
