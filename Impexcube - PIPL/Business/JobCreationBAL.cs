// -----------------------------------------------------------------------
// <copyright file="JobCreationBAL.cs" company="">
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
    public class JobCreationBAL
    {
        VTS.ImpexCube.Data.JobCreationDAL objJobNo = new VTS.ImpexCube.Data.JobCreationDAL();
        public DataSet JobNo()
        {
            return objJobNo.JobNo();
        }
        public DataSet Importer(string jobNo)
        {
            return objJobNo.Importer(jobNo);
        }
        //lblJobNo.Text,txtJobReceivedDate.Text,ddlMode.SelectedValue,ddlCustom.SelectedValue, ddlBEType.SelectedValue, ddlDocFillingStatus.SelectedValue,
        //ddlFilling.SelectedValue, txtBENo.Text,txtBEDate.Text

        public int insert(string Jobno, string JobReceDate, string Mode, string custom, string BEtype, string docfilling, string filling, string BENo, string BEdate, string Fyear, string username, string TotalNoofInvoice, string TotalInvoiceValue, string Currency, bool BondApply, string CustomName, string Chklisttype)
        {
            return objJobNo.insert(Jobno, JobReceDate, Mode, custom, BEtype, docfilling, filling, BENo, BEdate, Fyear, username, TotalNoofInvoice, TotalInvoiceValue, Currency, BondApply, CustomName, Chklisttype);
        }

        public int Update(string Jobno, string JobReceDate, string Mode, string custom, string BEtype, string docfilling, string filling, string BENo, string BEdate, string TotalNoofInvoice, string TotalInvoiceValue, string Currency, bool BondApply, string jobid, string CustomName, string Chklisttype)
        {
            return objJobNo.Update(Jobno, JobReceDate, Mode, custom, BEtype, docfilling, filling, BENo, BEdate, TotalNoofInvoice, TotalInvoiceValue, Currency, BondApply,jobid, CustomName, Chklisttype);
        }

        public int UpdateAssable(string Jobno, double AssableValue,double TotInvAmt, double TotDutyAmt)
        {
            return objJobNo.UpdateAssable(Jobno, AssableValue,TotInvAmt, TotDutyAmt);
        }
        public int update(string keycode)
        {
            return objJobNo.updateautono(keycode);
        }

        public DataSet JobNoList(string JobNo)
        {
            return objJobNo.JobNoList(JobNo);
        }

        public DataSet SelectJobNo(string JobNo)
        {
            return objJobNo.SelectJobNo(JobNo);
        }

        public DataSet SelectBondType()
        {
            return objJobNo.SelectBondType();
        }
        public DataSet BindCustomHouse(string AirSea)
        {
            return objJobNo.BindCustomHouse(AirSea);
        }

        public int InsertBondType(string JobNo, string BondType, string BondNumber, string RegisteredDate)
        {
            return objJobNo.InsertBondType(JobNo, BondType, BondNumber, RegisteredDate);
        }

        public int UpdateBondType(string Id, string JobNo, string BondType, string BondNumber, string RegisteredDate)
        {
            return objJobNo.UpdateBondType(Id, JobNo, BondType, BondNumber, RegisteredDate);
        }

        public DataSet SelectBond(string JobNo)
        {
            return objJobNo.SelectBond(JobNo);
        }
        public int InsertBondCertification(string JobNo, string CertificateNo, string CertificateType, string CertificateDate, string Commissionerrate, string Division, string Range)
        {
            return objJobNo.InsertBondCertification(JobNo, CertificateNo, CertificateType, CertificateDate, Commissionerrate, Division, Range);
        }

        public int UpdateBondCertification(string Id, string JobNo, string CertificateNo, string CertificateType, string CertificateDate, string Commissionerrate, string Division, string Range)
        {
            return objJobNo.UpdateBondCertification(Id, JobNo, CertificateNo, CertificateType, CertificateDate, Commissionerrate, Division, Range);
        }
        public int DeleteBondCertification(string Id)
        {
            return objJobNo.DeleteBondCertification(Id);
        }

        public DataSet SelectCertification(string JobNo)
        {
            return objJobNo.SelectCertification(JobNo);
        }

        public int InsertEXBond(string JobNo, string ExBondFDate, string ExBondTDate, string EXBondJobNo, string EXBondBLNO, string EXBondBLDate, string EXBondNo, string EXBondDate, string EXBondExpiryDate, string EXWareHouse, string ExCode)
        {
            return objJobNo.InsertEXBond( JobNo,  ExBondFDate,  ExBondTDate,  EXBondJobNo,  EXBondBLNO,  EXBondBLDate,  EXBondNo,  EXBondDate,  EXBondExpiryDate,  EXWareHouse,  ExCode);
        }

        public int UpdateEXBond(int TransId, string JobNo, string ExBondFDate, string ExBondTDate, string EXBondJobNo, string EXBondBLNO, string EXBondBLDate, string EXBondNo, string EXBondDate, string EXBondExpiryDate, string EXWareHouse, string ExCode)
        {
            return objJobNo.UpdateEXBond(TransId, JobNo, ExBondFDate, ExBondTDate, EXBondJobNo, EXBondBLNO, EXBondBLDate, EXBondNo, EXBondDate, EXBondExpiryDate, EXWareHouse, ExCode);
        }
        public DataSet SelectEXBond(string JobNo)
        {
            return objJobNo.SelectEXBond(JobNo);
        }

        public int InbondJobCopy(string OldJobNo, string DuplicateJobNo)
        {
            return objJobNo.InbondJobCopy(OldJobNo, DuplicateJobNo);
        }
    }
}
