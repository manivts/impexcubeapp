// -----------------------------------------------------------------------
// <copyright file="FundRequest.cs" company="">
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

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class FundRequest
    {
        VTS.ImpexCube.Data.FundRequest objFundRequest = new VTS.ImpexCube.Data.FundRequest();

        public DataSet jobno(string jobno)
        {
            return objFundRequest.jobno(jobno);
        }
        
        public DataSet Expjobno(string jobno)
        {
            return objFundRequest.Expjobno(jobno);
        }

        public DataSet ImporterName(string jobno)
        {
            return objFundRequest.ImporterName(jobno);
        }

        public DataSet ExportName(string jobno)
        {
            return objFundRequest.ExportName(jobno);
        }
        public int InsertFundRequest(string FundRequestNo, string FundRequestDate, string Jobno, string Importer, string RequestAmt, string RequestDate,
            string MOP, string RequestBy, string Purpose, string UserRemarks, string approved, string completed, string Cfsname, string ShippingName)
        {
            return objFundRequest.InsertFundRequest(FundRequestNo, FundRequestDate, Jobno, Importer, RequestAmt, RequestDate, MOP, RequestBy, Purpose, UserRemarks, approved, completed,Cfsname,ShippingName);
        }

        public DataSet SelectedFundRequest(string fundNo)
        {
            return objFundRequest.SelectedFundRequest(fundNo);
        }

        public DataSet ManagerApprovedList()
        {
            return objFundRequest.ManagerApprovedList();
        }

        public DataSet GridLoad(string jobno)
        {
            return objFundRequest.GridLoad(jobno);
        }

        public DataSet ApprovedGrid()
        {
            return objFundRequest.ApprovedGrid();
        }

        public int UpdateFundRequest(string FundRequestNo, string FundRequestDate, string Jobno, string Importer, string RequestAmt, string RequestDate,
            string MOP, string RequestBy, string Purpose, string UserRemarks, string Cfsname, string ShippingName)
        {
            return objFundRequest.UpdateFundRequest(FundRequestNo, FundRequestDate, Jobno, Importer, RequestAmt, RequestDate, MOP, RequestBy, Purpose, UserRemarks, Cfsname,ShippingName);
        }

        public int UpdateApproveRequest(string FundRequestNo, string approved, string approvedDate, string ApdAmt, string AmountFrom, string Remarks,string completed,string Active)
        {
            return objFundRequest.UpdateApproveRequest(FundRequestNo, approved, approvedDate, ApdAmt, AmountFrom, Remarks, completed,Active);
        }

        public DataSet ApprovedFundRequest(string fundNo)
        {
            return objFundRequest.ApprovedFundRequest(fundNo);
        }

        public DataSet PartialAmtBalance(string fundNo)
        {
            return objFundRequest.PartialAmtBalance(fundNo);
        }

        public int UpdateApprovedList(string FundRequestNo, string PayMode, string chqdd, string chqdate, string bank, string drewbank, string PayAmt, string status, string balance, string Remarks, string completed)
        {
          
            return objFundRequest.UpdateApprovedList(FundRequestNo, PayMode, chqdd, chqdate, bank, drewbank, PayAmt, status, balance, Remarks, completed);
            
        }

        public DataSet SelectedPendingRequest(string fundNo)
        {
            return objFundRequest.SelectedPendingRequest(fundNo);
        }

        public DataSet PendingGridLoad()
        {
            return objFundRequest.PendingGridLoad();
        }

        public DataSet ApprovalHistory(string jobno)
        {
            return objFundRequest.ApprovalHistory(jobno);
        }

        public DataSet BankName()
        {
            return objFundRequest.BankName();
        }

        public DataSet FundRequestHistory()
        {
            return objFundRequest.FundRequestHistory();
        }

        public DataSet FundRequestPending(string user)
        {
            return objFundRequest.FundRequestPending(user);
        }

        public DataSet Selectedpurpose(string Purpose)
        {
            return objFundRequest.Selectedpurpose(Purpose);
        }

        public DataSet SelectCFSName()
        {
            return objFundRequest.SelectCFSName();
        }
        public DataSet SelectShippingName()
        {
            return objFundRequest.SelectShippingName();
        }
    }
}
