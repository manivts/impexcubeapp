using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
using System.Data.OleDb;
using CrystalDecisions.Shared;
using System.Net.Mail;
using System.Net;
using Outlook = Microsoft.Office.Interop.Outlook;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;
using CrystalDecisions.CrystalReports.Engine;

namespace ImpexCube
{
    public partial class CryInvoiceReport : System.Web.UI.Page
    {
        string strconn = (string)ConfigurationManager.ConnectionStrings["Constr"].ConnectionString;

        //string strConn = ConfigurationManager.AppSettings["ConnectionOLEDB"];
        //string strconn = ConfigurationManager.AppSettings["ConnectionImpex"];
        #region
        ReportDocument rptObject = new ReportDocument();
        DataSet dataSetName = new DataSet();
        DataSet dsObject = new DataSet();
        private string[] dsTableName;
        //private string[] subRptName;
        string companyname = "";
        string CHANo = "";
        string STRegno = "";
        string address = "";
        string address1 = "";
        string branchname = "";
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
          
            if (IsPostBack == false)
            {
               
            }

            string forMat = "PD";
            String invNo = (string)Session["INVOICECTR"];
            string bill = (string)Session["BILLTYPE"];
            GetReports(invNo, bill, forMat);

        }
        public DataSet GetData()
        {
            DataSet ds = new DataSet();
            try
            {
                //String invNo = (string)Session["INVOICECTR"];
                //String strCmd = "";
              
                //string bill = (string)Session["BILLTYPE"];
                //if (bill == "SB")
                //    strCmd = "Select * from iec_invoiceNew where invoice='" + invNo + "'";
                //else
                //    strCmd = "Select * from iec_debit where invoice='" + invNo + "'";
                //OleDbConnection sqlConn = new OleDbConnection(strConn);
              
                //OleDbDataAdapter da = new OleDbDataAdapter(strCmd, sqlConn);

                ////--this statement is very important, here the table name should 
                ////--match with the XML Schema table name 
                //da.Fill(ds, "CryQuote");

            }
            catch (Exception Error)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('" + Error.Message + "');", true);
            }
            return ds;
        }
        protected void GetReports(string INO, string bill, string forMat)
        {

            string sqlStmts = "exec spInvoice '" + INO + "'," + bill;

        
            ReportDocument rptObject = new ReportDocument();
            DataSet dataSetName = new DataSet();
            DataSet dsObject = new DataSet();
            dsTableName = new string[1];
            dsTableName[0] = "spInvoice";
            ConnectionReports.FillDataSet(sqlStmts, dataSetName, dsTableName);

            dsObject.ReadXmlSchema(Request.PhysicalApplicationPath + @"XSD/dsInvoice.xsd");

            if (bill == "SB")
                rptObject.Load(Request.PhysicalApplicationPath + @"Reports/InvoiceReport.rpt");
            else
                rptObject.Load(Request.PhysicalApplicationPath + @"Reports/DebitNoteReport.rpt");

            //Sub Report Name
         
            if ((string)Session["BranchShortName"] == "CHN")
            {
                branchname = "and subject to Chennai jurisdiction only.";
            }
            else if ((string)Session["BranchShortName"] == "MUM")
            {
                branchname = "and subject to Mumbai jurisdiction only.";
            }
            else
            {
                branchname = "and subject to Delhi jurisdiction only.";
            }

            SqlConnection conn = new SqlConnection(strconn);
            string sqlQuery = "select  * from AppDetails";
            SqlDataAdapter da = new SqlDataAdapter(sqlQuery, conn);
            DataSet ds = new DataSet();
            da.Fill(ds, "name");

            if (ds.Tables["name"].Rows.Count != 0)
            {
                DataRowView row = ds.Tables["name"].DefaultView[0];
                companyname = row["CompanyName"].ToString();
                CHANo = row["CHANo"].ToString();
                STRegno = row["STRegNo"].ToString();
                address = row["address"].ToString();
                address1 = row["address1"].ToString();
            }

            ConnectionReports.ShowReportWithParameter(rptObject, dataSetName, new string[] { companyname, CHANo, STRegno, address, address1, branchname }, forMat);

        }
        protected void CrystalReportValue()
        {
          
            //String ReptFile;
            //String invNo = (string)Session["INVOICECTR"];
            //Session["InvoiceNum"] = invNo;

            //String strCmd = "";
     
            //string bill = (string)Session["BILLTYPE"];
            //Session["TypeOfBill"] = bill;
            //if (bill == "SB")
            //    strCmd = "Select * from iec_invoiceNew where invoice='" + invNo + "'";
            //else
            //    strCmd = "Select * from iec_debit where invoice='" + invNo + "'";
            //OleDbConnection sqlConn = new OleDbConnection(strConn);
       
            //OleDbDataAdapter da = new OleDbDataAdapter(strCmd, sqlConn);
            //DataSet ds = new DataSet();
            ////--this statement is very important, here the table name should 
            ////--match with the XML Schema table name 
            //da.Fill(ds, "CryQuote");
         
            //if (bill == "SB")
            //    ReptFile = Server.MapPath("~\\Reports\\InvoiceReport.rpt");
            //else
            //    ReptFile = Server.MapPath("~\\Reports\\DebitNoteReport.rpt");
      

            //ReportDocument mdt = new ReportDocument();
            //mdt.Load(ReptFile);
            //mdt.SetDataSource(ds.Tables[0]);
         
            //mdt.SetDatabaseLogon("sa", "123", "version6\\sqlexpress", "PIPL");

            ////--Binding report with CrystalReportViewer

            //CrystalReportViewer1.ReportSource = mdt;
            //CrystalReportViewer1.DataBind();

            //if ((string)Session["MAILBUTTON"] == "OK")
            //{
            //    GetAttached(mdt, invNo);
            //}

        }
        protected void GetAttached(ReportDocument mdt, string invNo)
        {
            if (Session["Maill"] != null)
            {
                if (Session["Maill"].ToString() == "SendMaill")
                {
                    ReportDocument RecDoc = mdt;
                    Response.ClearContent();
                    Response.ClearHeaders();

                    ExportOptions exportOpts = RecDoc.ExportOptions;
                    exportOpts.ExportFormatType = ExportFormatType.PortableDocFormat;
                    exportOpts.ExportDestinationType = ExportDestinationType.DiskFile;
                    exportOpts.DestinationOptions = new DiskFileDestinationOptions();
                    string pdfNo = invNo.Replace("/", "-");
                    // Set the disk file options.
                    DiskFileDestinationOptions diskOpts = new DiskFileDestinationOptions();
                    ((DiskFileDestinationOptions)RecDoc.ExportOptions.DestinationOptions).DiskFileName = Server.MapPath("~/PDF/" + pdfNo + ".pdf");
                    Session["AttachmentPath"] = ((DiskFileDestinationOptions)RecDoc.ExportOptions.DestinationOptions).DiskFileName.ToString();
                    //export the report to PDF rather than displaying the report in a viewer
                    RecDoc.Export();


                    // There are other format options available such as Word, Excel, CVS, and HTML in the ExportFormatType Enum given by crystal reports
                    Session["Maill"] = "AttachThePDF";
                    Session["MAILBUTTON"] = "";
                    Response.Redirect(Session["PageName"].ToString());

                }
            }

          
        }
        protected void BtnExport_Click(object sender, EventArgs e)
        {
            string forMat = drTpye.SelectedValue;
            String invNo = (string)Session["INVOICECTR"];
            string bill = (string)Session["BILLTYPE"];
            GetReports(invNo, bill, forMat);
        }
    }
}