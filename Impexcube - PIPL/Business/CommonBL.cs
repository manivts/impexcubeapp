// -----------------------------------------------------------------------
// <copyright file="CommonBL.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace VTS.ImpexCube.Business
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Data;
    using VTS.ImpexCube.Data;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class CommonBL
    {
        VTS.ImpexCube.Data.CommonDL obj = new VTS.ImpexCube.Data.CommonDL();
        public DataSet GetJobNo()
        {
            return obj.GetJobNo();
        }
        public DataSet GetExportJobNo()
        {
            return obj.GetExportJobNo();
        }
        public DataSet GetInvNo(string InvNo)
        {
            return obj.GetInvNo(InvNo);
        }
        public DataSet GetProductType()
        {
            return obj.GetProductType();
        }
        public DataSet GetRITCNo()
        {
            return obj.GetRITCNo();
        }
        public DataSet GetUnit()
        {
            return obj.GetUnit();
        }
        public DataSet GetJobImportShipment(string JobNo)
        {
            return obj.GetJobImportShipment(JobNo);
        }

        public DataSet GetJobDetails(string JobNo)
        {
            return obj.GetJobDetails(JobNo);
        }
        public DataSet GetInvoiceDetails(string JobNo,string InvNo)
        {
            return obj.GetInvoiceDetails(JobNo, InvNo);
        }
        public DataSet GetProductPopup(string ProductName)
        {
            return obj.GetProductPopup(ProductName);
        }

        //public DataSet GetConsignorPopup(string ConsName,string mode)
        //{
        //    return obj.GetConsignorPopup(ConsName,mode);
        //}
        public DataSet GetProductDuty(string RITCCode)
        {
            return obj.GetProductDuty(RITCCode);
        }
        public DataSet GetAssBCDCVD(string TransId)
        {
            return obj.GetAssBCDCVD(TransId);
        }
        public string GetImpExchangeRate(string CurrencyShortName)
        {
            return obj.GetImpExchangeRate(CurrencyShortName);
        }

        public DataSet GetState()
        {
            return obj.GetState();
        }

        public DataSet GetCountryDetails()
        {
            return obj.GetCountryDetails();
        }

        public DataSet GetCurrencyDetails()
        {
            return obj.GetCurrencyDetails();
        }
        public DataSet CheckProduct(string RITCNo)
        {
            return obj.CheckProduct(RITCNo);
        }
        public DataSet GetDataSet(string Query)
        {
            return obj.GetDataSet(Query);
        }
    }
}
