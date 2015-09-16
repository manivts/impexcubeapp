using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using Microsoft.Reporting.WebForms;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace ImpexCube
{
    public partial class efrmPrintCheckList : System.Web.UI.Page
    {
        VTS.ImpexCube.Business.CommonBL obj1 = new VTS.ImpexCube.Business.CommonBL();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                JobNo();
                ddlJobNo.SelectedValue = (string)Session["JobNo"];
                ObjectDataSource1.FilterExpression = "JobNo='" + (string)Session["JobNo"] + "'";
                ObjectDataSource2.FilterExpression = "JobNo='" + (string)Session["JobNo"] + "'";
                //  ObjectDataSource2.FilterExpression = "InvoiceNo='" + (string)Session["InvoiceNo"] + "'";
                ObjectDataSource1.DataBind();
                ReportViewer1.LocalReport.SubreportProcessing += new SubreportProcessingEventHandler(SetSubDataSource);
                ReportViewer1.LocalReport.Refresh();
            }
        }
        public void SetSubDataSource(object sender, SubreportProcessingEventArgs e)
        {
            e.DataSources.Add(new ReportDataSource("DataSetExpChecklistProduct", "ObjectDataSource2"));
        }

        protected void ddlJobNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            Session["JobNo"] = ddlJobNo.SelectedValue;
            ObjectDataSource1.FilterExpression = "JobNo='" + (string)Session["JobNo"] + "'";
            ObjectDataSource2.FilterExpression = "JobNo='" + (string)Session["JobNo"] + "'";
           // ObjectDataSource2.FilterExpression = "InvoiceNo='" + (string)Session["InvoiceNo"] + "'";
            ObjectDataSource1.DataBind();
            ReportViewer1.LocalReport.SubreportProcessing += new SubreportProcessingEventHandler(SetSubDataSource);
            ReportViewer1.LocalReport.Refresh();
        }
         
        public void JobNo()
        {
            DataSet dt = obj1.GetExportJobNo();
            ddlJobNo.DataSource = dt;
            ddlJobNo.DataValueField = "JobNo";
            ddlJobNo.DataTextField = "JobNo";
            ddlJobNo.DataBind();
        }
    }
}