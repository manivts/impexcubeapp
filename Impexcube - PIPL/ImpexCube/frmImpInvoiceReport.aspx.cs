using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Reporting.Common;
using Microsoft.Reporting.WebForms;
//using System.Collections;
//using System.IO;
using System.Reflection;

namespace ImpexCube
{
    public partial class frmImpInvoiceReport : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

                ObjectDataSource1.FilterExpression = "invoice='" + (string)Session["InvNo"] + "'";//" +(string)Session["InvNo"]+ "
                ObjectDataSource1.DataBind();
                ObjectDataSource2.DataBind();
                ReportViewer1.LocalReport.Refresh();
               // CreatePDF("Invoice");
            }
        }
        public void DisableUnwantedExportFormats(LocalReport rvServer)
        {
            RenderingExtension[] extensio = rvServer.ListRenderingExtensions();

            foreach (RenderingExtension extension in rvServer.ListRenderingExtensions())
            {
                if (extension.Name == "XML" || extension.Name == "WORD" || extension.Name == "MHTML" || extension.Name == "EXCEL" || extension.Name == "Excel" || extension.Name == "CSV")
                {
                    ReflectivelySetVisibilityFalse(extension);
                }
            }
        }


        private void ReflectivelySetVisibilityFalse(RenderingExtension extension)
        {
            FieldInfo info = extension.GetType().GetField("m_isVisible", BindingFlags.NonPublic | BindingFlags.Instance);


            if (info != null)
            {
                info.SetValue(extension, false);
            }
        }

        protected void ReportViewer1_PreRender(object sender, EventArgs e)
        {
            DisableUnwantedExportFormats(ReportViewer1.LocalReport);
        }

        //private void CreatePDF(string fileName)
        //{
        //    // Setup DataSet
        //    ImpexCube.ImpInvoiceTableAdapters.View_InvoiceTableAdapter ds = new ImpexCube.ImpInvoiceTableAdapters.View_InvoiceTableAdapter();


        //    // Create Report DataSource
        //     ReportDataSource rds = new ReportDataSource("MyDataSourceName", ds);

        //    // Variables
        //    Warning[] warnings;
        //    string[] streamIds;
        //    string mimeType = string.Empty;
        //    string encoding = string.Empty;
        //    string extension = string.Empty;


        //    // Setup the report viewer object and get the array of bytes
        //    ReportViewer viewer = new ReportViewer();
        //    viewer.ProcessingMode = ProcessingMode.Local;
        //    viewer.LocalReport.ReportPath = "ImpInvoice.rdlc";


        //    byte[] bytes = viewer.LocalReport.Render("PDF", null, out mimeType, out encoding, out extension, out streamIds, out warnings);


        //    // Now that you have all the bytes representing the PDF report, buffer it and send it to the client.
        //    Response.Buffer = true;
        //    Response.Clear();
        //    Response.ContentType = mimeType;
        //    Response.AddHeader("content-disposition", "attachment; filename=" + fileName + "." + extension);
        //    Response.BinaryWrite(bytes); // create the file
        //    Response.Flush(); // send it to the client to download
        //}
    }
}