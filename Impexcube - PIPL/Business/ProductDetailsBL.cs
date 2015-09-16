// -----------------------------------------------------------------------
// <copyright file="ProductDetailsBL.cs" company="">
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
    public class ProductDetailsBL
    {
        VTS.ImpexCube.Data.ProductDetailsDL obj = new VTS.ImpexCube.Data.ProductDetailsDL();
        public int InsertProductDetals(string JobNo, string InvoiceNo, string ProductFamily, string ProductCode, string ProductDesc, string ProType, double Qty, string Unit, double UnitPrice, double Amount, string RitcNo, double ProdValue, double FreightAmount, double insuAmount, double miscAmount, double agencyAmount, double loadingcharge, double assvalue, string CreatedBy, string CreatedDate, int slno)
        {
            return obj.InsertProductDetails(JobNo, InvoiceNo, ProductFamily, ProductCode, ProductDesc, ProType, Qty, Unit, UnitPrice, Amount, RitcNo, ProdValue, FreightAmount, insuAmount, miscAmount, agencyAmount, loadingcharge, assvalue, CreatedBy, CreatedDate, slno);
        }
        public int UpdateProductDetails(string ProductID, string ProductFamily, string ProductCode, string ProductDesc, string ProType, double Qty, string Unit, double UnitPrice, double Amount, string RitcNo, double ProdValue, double FreightAmount, double insuAmount, double miscAmount, double agencyAmount, double loadingcharge, double assvalue, int slno)
        {
            return obj.UpdateProductDetails(ProductID, ProductFamily, ProductCode, ProductDesc, ProType, Qty, Unit, UnitPrice, Amount, RitcNo, ProdValue, FreightAmount, insuAmount, miscAmount, agencyAmount, loadingcharge, assvalue, slno);
        }
        public DataSet loadproductgrid(string InvNo, string JobNo)
        {
            return obj.loadproductgrid(InvNo,JobNo);
        }
        //public int UpdateMain( string ITCLocation, string ITCHSCode, string PolicyPara, string PolicyYear,double Loading,string LoadTerms,double LoadRate,double LoadAmount, string Jobno, string InvoiceNo, string ProductDesc)
        //{
        //    return obj.UpdateMain(ITCLocation, ITCHSCode, PolicyPara, PolicyYear, Loading, LoadTerms, LoadRate, LoadAmount, Jobno, InvoiceNo, ProductDesc);

        //}
        //public DataSet ReadMain(int ProductID)
        //{
        //    return obj.ReadMain(ProductID);
        //}
        public DataSet ReadDuty(int ProductID)
        {
            return obj.ReadDuty(ProductID);
        }
        //public DataSet ReadOtherDuty(int ProductID)
        //{
        //    return obj.ReadOtherDuty(ProductID);
        //}

        //public DataSet ReadGenericDesc(int ProductID)
        //{
        //    return obj.ReadGenericDesc(ProductID);
        //}

        public int UpdateGenericDesc(string GenericDesc, string Manufacturer, string Brand, string Model, string Accessories, string EndUse, string CountryofOrigin, string Jobno, string InvoiceNo, string ProductDesc, string ProductID, double AQty1, string AQty1Unit)
        {
            return obj.UpdateGenericDesc(GenericDesc, Manufacturer, Brand, Model, Accessories, EndUse, CountryofOrigin, Jobno, InvoiceNo, ProductDesc, ProductID,AQty1, AQty1Unit);
        }

                //    EximSchCode,EximSchDesc ,SchNoten,SchNotenUnit,SchNotenDesc,CTHNo,CETNo,
        //RateType,BasicDutyNotn,BasicDutySno,BasicDutyRate,BasicDutyFlag,BasicDutyAmount,
                //BasicDutyUnit,AddlExNotn,AddlExSlNo,AddlExRate,AddlExFlag,AddlExAmount,AddlExUnit,MRPDuty,MRPSNo,MRP,MRPUnit,MRPAbatement,ExCVDNotn,ExCVDSlNo,EXCVDRate
                //PolicyPara,PolicyYear,EduCessNotn,EduCessSNo,EduCessRate,EduCessAmount,SHECess,SHECessSno,SHECessRate

        public int UpdateDuty(string EximSchCode, string EximSchDesc, string SchNoten, string SchNotenUnit, string SchNotenDesc, string CTHNo, string CETNo,
           string RateType, string BasicDutyNotn, string BasicDutySno, string BasicDutyFlag, double BasicDutyRate, string BasicDutyUnit,
           string AddlExNotn, string AddlExSlNo, double AddlExRate, string AddlExFlag, double AddlExAmount, string AddlExUnit,
           string MRPDuty, string MRPSNo, double MRP, string MRPUnit, double MRPAbatement, string ExCVDNotn, string ExCVDSlNo, double EXCVDRate,string PolicyPara,string PolicyYear,
           string EduCessNotn, string EduCessSNo, double EduCessRate, string SHECess, string SHECessSno, double SHECessRate,
           string Jobno, string InvoiceNo, string ProductDesc, double bcd, double EduCessAmount, double SHECessAmount, double CVD, double SADAmt, double ExEduCessAmount, 
           double ExSHECessAmount, string ITCLocation, string ITCHSCode, string ProductID, string SAPTANotn, string SAPTASNo, string SAPTADesc, string PoNo, string PoDate,
            double CVDDutyAmtQty, double BasDutyAmtQty, double BasicDutyAmount, double TotBasicDutyAmt, double TotalCVDAmt,
            string ExCessNotn, string ExCessSlNo, double ExCessRate, string ExCessFlag, double ExCessAmount, string ExCessUnit, double TotalDutyAmt, string ExEduCessNotn, string ExEduCessSNo, double ExEduCessRate, double ExSHECessRate, double CESSDutyAmt)
        {
            return obj.UpdateDuty(EximSchCode, EximSchDesc, SchNoten,  SchNotenUnit, SchNotenDesc, CTHNo, CETNo, RateType, BasicDutyNotn, BasicDutySno, BasicDutyFlag, BasicDutyRate, BasicDutyUnit, AddlExNotn, AddlExSlNo,
                AddlExRate, AddlExFlag, AddlExAmount, AddlExUnit, MRPDuty, MRPSNo, MRP, MRPUnit, MRPAbatement, ExCVDNotn, ExCVDSlNo, EXCVDRate, PolicyPara, PolicyYear, EduCessNotn, EduCessSNo,
                EduCessRate, SHECess, SHECessSno, SHECessRate, Jobno, InvoiceNo, ProductDesc, bcd, EduCessAmount, SHECessAmount, CVD, SADAmt, ExEduCessAmount, ExSHECessAmount,
                 ITCLocation, ITCHSCode, ProductID, SAPTANotn, SAPTASNo, SAPTADesc, PoNo, PoDate, CVDDutyAmtQty, BasDutyAmtQty, BasicDutyAmount, TotBasicDutyAmt, TotalCVDAmt,
                     ExCessNotn, ExCessSlNo, ExCessRate, ExCessFlag, ExCessAmount, ExCessUnit, TotalDutyAmt, ExEduCessNotn, ExEduCessSNo, ExEduCessRate, ExSHECessRate, CESSDutyAmt);
        }

        public int UpdateEXDuty(double ExEduCessRate, double ExSHECessRate, string ExGSIAddlDutyNotn, string ExGSIAddlDutySlNo,
                 double ExGSIAddlDutyRate, string ExGSIAddlDutyFlag, double ExGSIAddlDutyAmount, string ExGSIAddlDutyUnit, string ExSPLExDutyNotn, string ExSPLExDutySlNo, double ExSPLExDutyRate,
                 string ExSPLExDutyFlag, double ExSPLExDutyAmount, string ExSPLExDutyUnit, string ExTTAAddlDutyNotn, string ExTTAAddlDutySlNo, double ExTTAAddlDutyRate, string ExTTAAddlDutyFlag,
                 double ExTTAAddlDutyAmount, string ExTTAAddlDutyUnit, string ExHealthCessNotn, string ExHealthCessSlNo, double ExHealthCessRate, string ExHealthCessFlag, double ExHealthCessAmount,
                 string ExHealthCessUnit,string ExSADNotn, string ExSADSlno, double ExSADRate,
           string AddlNotn,string AddlSno,string NCDNotn,string NCDSno,double NCDRate,string NCDDFlag,double NCDAmount,string NCDUnit,string NCDRule,string SurNotn,string SurSno,
            double SurRate, string SAPTANotn, string SAPTASNo, string SAPTADesc, string TariffValNotn, string TariffValSNo, double TariffUnitQty, string TariffUnit, double TariffRate,
            double TariffAmount, string Jobno, string InvoiceNo, string ProductDesc, string TransId)
        {
            return obj.UpdateEXDuty(ExEduCessRate, ExSHECessRate,ExGSIAddlDutyNotn, ExGSIAddlDutySlNo, ExGSIAddlDutyRate, ExGSIAddlDutyFlag, ExGSIAddlDutyAmount,
        ExGSIAddlDutyUnit, ExSPLExDutyNotn, ExSPLExDutySlNo, ExSPLExDutyRate, ExSPLExDutyFlag, ExSPLExDutyAmount, ExSPLExDutyUnit, ExTTAAddlDutyNotn,
        ExTTAAddlDutySlNo, ExTTAAddlDutyRate, ExTTAAddlDutyFlag, ExTTAAddlDutyAmount, ExTTAAddlDutyUnit, ExHealthCessNotn, ExHealthCessSlNo,
        ExHealthCessRate, ExHealthCessFlag, ExHealthCessAmount, ExHealthCessUnit,
        ExSADNotn, ExSADSlno, ExSADRate, AddlNotn, AddlSno, NCDNotn, NCDSno, NCDRate, NCDDFlag, NCDAmount, NCDUnit, NCDRule, SurNotn, SurSno, SurRate, SAPTANotn,
        SAPTASNo, SAPTADesc, TariffValNotn, TariffValSNo, TariffUnitQty, TariffUnit, TariffRate, TariffAmount, Jobno, InvoiceNo, ProductDesc, TransId);
        }

        public int InsertITCLicence(string LicenceNo, string LicenceDate, string Quantity, string DebitValue, string RANumber,
         string RADate, string RegPort, string Jobno, string InvoiceNo, string ProductDesc)
        {
            return obj.InsertITCLicence(LicenceNo, LicenceDate, Quantity, DebitValue, RANumber, RADate, RegPort, Jobno, InvoiceNo, ProductDesc);
        }


        public int UpdateITCLicence(string LicenceNo, string LicenceDate, string Quantity, string DebitValue, string RANumber,
           string RADate, string RegPort, string ID)
        {
            return obj.UpdateITCLicence(LicenceNo, LicenceDate, Quantity, DebitValue, RANumber, RADate, RegPort, ID);
        }

        public DataSet GetProductDetails(string ProductID)
        {
            return obj.GetProductDetails(ProductID);
        }

        public DataSet GetITCLicNo(string InvoiceNo, string Jobno, string ProductDesc)
        {
            return obj.GetITCLicNo(InvoiceNo, Jobno, ProductDesc);
        }
        public int InsertPrevBEDetails(string PrevBENo, string PrevBEDate, string UnitValue, double UnitRate, string CustomHouse,
         string Jobno, string InvoiceNo, string ProductDesc)
        {
            return obj.InsertPrevBEDetails(PrevBENo, PrevBEDate, UnitValue, UnitRate, CustomHouse, Jobno, InvoiceNo, ProductDesc);
        }

        public DataSet GetPrevBEDetails(string Jobno, string InvoiceNo, string ProductDesc)
        {
            return obj.GetPrevBEDetails(Jobno, InvoiceNo, ProductDesc);
        }

        public int UpdatePrevBEDetails(string PrevBENo, string PrevBEDate, string UnitValue, double UnitRate, string CustomHouse, string ID)
        {
            return obj.UpdatePrevBEDetails(PrevBENo, PrevBEDate, UnitValue, UnitRate, CustomHouse, ID);
        }
        public DataSet GetBCDRTA(string RITCNo)
        {
            return obj.GetBCDRTA(RITCNo);
        }
        public DataSet GetCVDRTA(string RITCNo)
        {
            return obj.GetCVDRTA(RITCNo);
        }
        public DataSet GetCVDUserNoti(string SubRITCNo)
        {
            return obj.GetCVDUserNoti(SubRITCNo);
        }
        public DataSet GetSADUserNoti(string SubRITCNo)
        {
            return obj.GetSADUserNoti(SubRITCNo);
        }
        public DataSet GetMRPRTA(string RITCNo)
        {
            return obj.GetMRPRTA(RITCNo);
        }
        public DataSet GetProductDutyPer(string RITCNo)
        {
            return obj.GetProductDutyPer(RITCNo);
        }
        public int DeleteProductDetails(string ProductId)
        {
            return obj.DeleteProductDetails(ProductId);
        }
        public DataSet GetScheme(string ID)
        {
            return obj.GetScheme(ID);
        }
        public int UpdateSchemeDetails(string ID, string EDIRegNo, string Date, string ItemSno, string ItemSnoinLic,
           string SchemeLicNo, string SchemeLicDate, string SchemeType, string CIFValue, string Qty, string Unit,
           string ValueDebited, string RegPort)
        {
            return obj.UpdateSchemeDetails(ID, EDIRegNo, Date, ItemSno, ItemSnoinLic, SchemeLicNo, SchemeLicDate, SchemeType, CIFValue, Qty, Unit, ValueDebited, RegPort);
        }


        public DataSet GetProductMaster(string ProductName)
        {
            return obj.GetProductMaster(ProductName);
        }

        public DataSet GetProductMasterCode(string Productcode)
        {
            return obj.GetProductMasterCode(Productcode);
        }
    }
}
