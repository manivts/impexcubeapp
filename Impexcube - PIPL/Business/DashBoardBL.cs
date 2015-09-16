using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;


namespace VTS.ImpexCube.Business
{
   

    public class DashBoardBL
    {
        VTS.ImpexCube.Data.DashBoardDL objDashBoard = new VTS.ImpexCube.Data.DashBoardDL();

        public DataSet SelectReportDetails()
        {
            return objDashBoard.SelectReportDetails();
        }

        public DataSet SelectBillingDelivery(string stage, string status, string fyear)
        {
            return objDashBoard.SelectBillingDelivery(stage, status, fyear);
        }

        public int InsertDashboardDetails(string JobNo, string JobStage, string JobStatus, int NoofDays, int NoofCount, string Grade)
        {
            return objDashBoard.InsertDashboardDetails(JobNo, JobStage, JobStatus, NoofDays, NoofCount, Grade);
        }

        public DataSet SelectBillingDeliveryDashboard(string fyear)
        {
            return objDashBoard.SelectBillingDeliveryDashboard(fyear);
        }

        public int DeletDashboardDetails()
        {
            return objDashBoard.DeletDashboardDetails();
        }

        public DataSet SelectImpJob(string Uid, string Uname, string Grade)
        {
            return objDashBoard.SelectImpJob(Uid, Uname, Grade);
        }

        public DataSet SelectImpShipMent(string Uid, string Uname, string Grade)
        {
            return objDashBoard.SelectImpShipMent(Uid, Uname, Grade);
        }
        public DataSet SelectImpInvoice(string Uid, string Uname, string Grade)
        {
            return objDashBoard.SelectImpInvoice(Uid, Uname, Grade);
        }

        public DataSet SelectImpProduct(string Uid, string Uname, string Grade)
        {
            return objDashBoard.SelectImpProduct(Uid, Uname, Grade);
        }
        public DataSet SelectBEFile(string Uid, string Uname, string Grade)
        {
            return objDashBoard.SelectBEFile(Uid, Uname, Grade);
        }
        public DataSet SelectExpJob(string Uid, string Uname, string Grade)
        {
            return objDashBoard.SelectExpJob(Uid, Uname, Grade);
        }
        public DataSet SelectExpShipMent(string Uid, string Uname, string Grade)
        {
            return objDashBoard.SelectExpShipMent(Uid, Uname, Grade);
        }

        public DataSet SelectExpInvoice(string Uid, string Uname, string Grade)
        {
            return objDashBoard.SelectExpInvoice(Uid, Uname, Grade);
        }

        public DataSet SelectExpProduct(string Uid, string Uname, string Grade)
        {
            return objDashBoard.SelectExpProduct(Uid, Uname, Grade);
        }

        public DataSet SelectSBFile(string Uid, string Uname, string Grade)
        {
            return objDashBoard.SelectSBFile(Uid, Uname, Grade);
        }

        public DataSet SelectOperation(string Uid, string Uname, string Grade)
        {
            return objDashBoard.SelectOperation(Uid, Uname, Grade);
        }

        public DataSet SelectAccounts(string Uid, string Uname, string Grade)
        {
            return objDashBoard.SelectAccounts(Uid, Uname, Grade);
        }

        public DataSet ViewImpJob(string Uid, string Uname, string Grade)
        {
            return objDashBoard.ViewImpJob(Uid, Uname, Grade);
        }

        public DataSet ViewImpShipMent(string Uid, string Uname, string Grade)
        {
            return objDashBoard.ViewImpShipMent(Uid, Uname, Grade);
        }

        public DataSet ViewImpInvoice(string Uid, string Uname, string Grade)
        {
            return objDashBoard.ViewImpInvoice(Uid, Uname, Grade);
        }

        public DataSet ViewImpProduct(string Uid, string Uname, string Grade)
        {
            return objDashBoard.ViewImpProduct(Uid, Uname, Grade);
        }

        public DataSet ViewExpJob(string Uid, string Uname, string Grade)
        {
            return objDashBoard.ViewExpJob(Uid, Uname, Grade);
        }

        public DataSet ViewExpShipMent(string Uid, string Uname, string Grade)
        {
            return objDashBoard.ViewExpShipMent(Uid, Uname, Grade);
        }

        public DataSet ViewExpInvoice(string Uid, string Uname, string Grade)
        {
            return objDashBoard.ViewExpInvoice(Uid, Uname, Grade);
        }

        public DataSet ViewExpProduct(string Uid, string Uname, string Grade)
        {
            return objDashBoard.ViewExpProduct(Uid, Uname, Grade);
        }
    }
}
