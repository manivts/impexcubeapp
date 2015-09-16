// -----------------------------------------------------------------------
// <copyright file="FreightDetailsBL.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace VTS.ImpexCube.Business
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class FreightDetailsBL
    {       
        VTS.ImpexCube.Data.FreightDetailsDL frghtDL = new VTS.ImpexCube.Data.FreightDetailsDL();

        public int InsertFreightDetails(string invoiceno, double freightcurrency, double freightRate, double freightAmount, double insurancecurrency, double insuranceRate, double insuranceAmount, double discountcurrency, double discountRate, double discountAmount,
            int createdBy, DateTime createdDate, int modifiedBy, DateTime modifiedDate)
        {
            return frghtDL.InsertFreightDetails(invoiceno, freightcurrency, freightRate, freightAmount, insurancecurrency, insuranceRate, insuranceAmount, discountcurrency, discountRate, discountAmount, createdBy, createdDate, modifiedBy, modifiedDate);
        }

    }
}
