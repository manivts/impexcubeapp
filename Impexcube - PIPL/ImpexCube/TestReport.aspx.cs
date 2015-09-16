using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace ImpexCube
{
    public partial class TestReport : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

               // DataSet dsActivityReport = new DataSet();



                string reportName = null;
                string parameter1 = null;

                if (this.Request.QueryString["ReportName"] != null)
                    reportName = "ImpInvoice";//this.Request.QueryString["ReportName"];

                if (this.Request.QueryString["Parameter1"] != null)
                    parameter1 = "Invoice";//this.Request.QueryString["Parameter1"];

                if (reportName != null)
                {
                    ShowReport(reportName, parameter1);
                }
            }
        }
        private void ShowReport(string Report_Name, string Parameter1)
        {
//E:\MMK\Application\PIPL Application\ImpexCube\ImpexCube\ImpexCube\ImpInvoice.rdlc
            //path for your reports
            string path = HttpContext.Current.Server.MapPath("~/");

            ReportViewer1.Reset(); //important
            ReportViewer1.ProcessingMode = Microsoft.Reporting.WebForms.ProcessingMode.Local;
            Microsoft.Reporting.WebForms.LocalReport r = ReportViewer1.LocalReport;
            //r.ReportPath = path + Report_Name + ".rdlc";
            r.ReportPath = path + Report_Name + ".rdlc";
            Microsoft.Reporting.WebForms.ReportDataSource rds;

            // fill data parameters table so we can use it on reports
            //DataTable dtReports = new  ReportsDataSet.ReportsDataTable();
            DataTable dtReports = new ImpInvoice.View_InvoiceDataTable();
            DataRow drReports = dtReports.NewRow();
            drReports["ReportName"] = Report_Name;
            drReports["ReportDate"] = DateTime.Now;
            drReports["Parameter1"] = Parameter1;
            dtReports.Rows.Add(drReports);

            // add parameters table to report data source
            rds = new Microsoft.Reporting.WebForms.ReportDataSource();
            rds.Name = "ReportsDataSet_Reports";
            rds.Value = dtReports;
            r.DataSources.Add(rds);


            //if (Report_Name == "CustomersReport")
            //{
                //ReportsDataSet.CustomersDataTable dtCustomers = new ReportsDataSet.CustomersDataTable();
                //ReportsDataSetTableAdapters.CustomersTableAdapter ad = new ReportsDataSetTableAdapters.CustomersTableAdapter();
                ImpInvoice.View_InvoiceDataTable dtCustomers = new ImpInvoice.View_InvoiceDataTable();
                ImpInvoiceTableAdapters.View_InvoiceTableAdapter ad = new ImpInvoiceTableAdapters.View_InvoiceTableAdapter();
                ad.Fill(dtCustomers);

                rds = new Microsoft.Reporting.WebForms.ReportDataSource();
                rds.Name = "ReportsDataSet_Customers";
                rds.Value = dtCustomers;
                r.DataSources.Add(rds);
           // }
        }
    }
}