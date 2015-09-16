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

using System.IO;
using System.Reflection;
using Outlook = Microsoft.Office.Interop.Outlook;
using iTextSharp.text;
using MySql;
using MySql.Data.MySqlClient;
using iTextSharp.text.pdf;
//--for CrystalReports's ReportDocument.
using CrystalDecisions.CrystalReports.Engine;

namespace ImpexCube.Billing
{
    public partial class CrystalReport : System.Web.UI.Page
    {
      
        string strImpex = (string)ConfigurationManager.ConnectionStrings["Constr"].ConnectionString;
        #region
        ReportDocument rptObject = new ReportDocument();
        DataSet dataSetName = new DataSet();
        DataSet dsObject = new DataSet();
        private string[] dsTableName;
        string companyname = "";
        string CHANo = "";
        string STRegno = "";
        string address = "";
        string address1 = "";
        string branchname = "";
        #endregion
        public void Page_Pre_init(object sender, EventArgs e)
        {

        }
        public void Page_Init(object sender, EventArgs e)
        {

        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack == false)
            {
            }
            Bills();
        }
        protected void Bills()
        {
            string forMat = "PD";
            String invNo = (string)Session["INVOICECTR"];
            string bill = (string)Session["BILLTYPE"];
            if (Session["PRINTALL"] != null)
                GetReports();
            else
                GetReports(invNo, bill, forMat);
        }

        protected void GetReports()
        {
            Session["PRINTALL"] = null;
            string fType = "PD";
            string fDate = (string)Session["FD"];
            string tDate = (string)Session["TD"];
            string BType = (string)Session["BILLTYPE"];
            string shpType = (string)Session["shpType"];
            string pName = (string)Session["pname"];
            string sqlStmts = "";
            sqlStmts = "exec spBillsAll '" + pName + "','" + BType + "','" + shpType + "','" + fDate + "','" + tDate + "'";
            ReportDocument rptObject = new ReportDocument();
            DataSet dataSetName = new DataSet();
            DataSet dsObject = new DataSet();
            dsTableName = new string[1];
            dsTableName[0] = "spBillsAll";
            ConnectionReports.FillDataSet(sqlStmts, dataSetName, dsTableName);
            dsObject.ReadXmlSchema(Request.PhysicalApplicationPath + @"Billing/XSD/dsInvoiceAll.xsd");
            if (BType == "SB")
                rptObject.Load(Request.PhysicalApplicationPath + @"Billing/Reports/Invoice.rpt");
            else
                rptObject.Load(Request.PhysicalApplicationPath + @"Billing/Reports/DebitNote.rpt");
            SqlConnection conn = new SqlConnection(strImpex);
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
                branchname = row["ReportBranchName"].ToString();
            }
            ConnectionReports.ShowReportWithParameter(rptObject, dataSetName, new string[] { companyname, CHANo, STRegno, address, address1, branchname }, fType);
        }

        protected void GetReports(string INO, string bill, string fType)
        {
            string sqlStmts = "exec spBills '" + INO + "'," + bill;
            ReportDocument rptObject = new ReportDocument();
            DataSet dataSetName = new DataSet();
            DataSet dsObject = new DataSet();
            dsTableName = new string[1];
            dsTableName[0] = "spBills";
            ConnectionReports.FillDataSet(sqlStmts, dataSetName, dsTableName);
            dsObject.ReadXmlSchema(Request.PhysicalApplicationPath + @"Billing/XSD/dsBills.xsd");
            if (bill == "SB")
                rptObject.Load(Request.PhysicalApplicationPath + @"Billing/Reports/CrystalReportSTAX.rpt");
            else
                rptObject.Load(Request.PhysicalApplicationPath + @"Billing/Reports/CrystalReportDB.rpt");
            SqlConnection conn = new SqlConnection(strImpex);
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
                branchname = row["ReportBranchName"].ToString();
            }
            if ((string)Session["MAILBUTTON"] == "OK")
            {
                rptObject.SetDataSource(dataSetName);
                rptObject.SetDatabaseLogon("sa", "123", "vts-sdu-2", "PIPL");
                //CrystalReportViewer1.ReportSource = rptObject;
                //CrystalReportViewer1.DataBind();
                GetAttached(rptObject, INO);
            }
            else
            {
                ConnectionReports.ShowReportWithParameter(rptObject, dataSetName, new string[] { companyname, CHANo, STRegno, address, address1, branchname }, fType);
            }
        }
        public DataSet GetSPBills(string ino, string Btype)
        {
            SqlConnection conn = new SqlConnection(strImpex);
            conn.Open();
            SqlCommand cmd = new SqlCommand("spBills", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@invNO", ino);
            cmd.Parameters.AddWithValue("@bType", Btype);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds, "DD");
            return ds;
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
                    Session["PDFINV"] = pdfNo;
                    DiskFileDestinationOptions diskOpts = new DiskFileDestinationOptions();
                    ((DiskFileDestinationOptions)RecDoc.ExportOptions.DestinationOptions).DiskFileName = Server.MapPath("~/PDF/" + pdfNo + ".pdf");
                    Session["AttachmentPath"] = ((DiskFileDestinationOptions)RecDoc.ExportOptions.DestinationOptions).DiskFileName.ToString();
                    //export the report to PDF rather than displaying the report in a viewer
                    RecDoc.Export();
                    // There are other format options available such as Word, Excel, CVS, and HTML in the ExportFormatType Enum given by crystal reports
                    Session["Maill"] = "AttachThePDF";
                    Session["MAILBUTTON"] = "";
                   
                }
            }
        }
       
        protected void BtnExport_Click(object sender, EventArgs e)
        {
        }
        protected void GetMailOnline(string mTo)
        {
            string path = (string)Session["AttachmentPath"];
            string mf = (string)Session["USER-NAME"];
            Session["mTO"] = mTo;
            Session["MessSubj"] = "Re: Billing Report";
            Response.Redirect("sendMailPIPL.aspx", false);
        }
       
       
    }
}