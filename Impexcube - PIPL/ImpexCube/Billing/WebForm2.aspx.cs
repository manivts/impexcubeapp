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
    public partial class WebForm2 : System.Web.UI.Page
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
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Bills();
            }
           
        }
      
        protected void Bills()
        {
            string forMat = "PD";
            string invNo = (string)Session["INVOICECTR"];
            string bill = (string)Session["BILLTYPE"];
            if (Session["PRINTALL"] != null)
                GetReports();

            // GetReports(invNo, bill, forMat);
        }

        protected void GetReports()
        {
            // Session["PRINTALL"] = null;
            string fType = "PD";
            string fDate = (string)Session["FD"];
            string tDate = (string)Session["TD"];
            string BType = (string)Session["BILLTYPE"];
            string shpType = (string)Session["shpType"];
            string pName = (string)Session["pname"];
            string sqlStmts = "";

            ReportDocument rptDoc = new ReportDocument();
            if (BType == "SB")
            {
                ImpexCube.Billing.XSD.printall ds = new ImpexCube.Billing.XSD.printall(); // .xsd file name
                DataTable dt = new DataTable();
                dt.TableName = "Crystal Report Example";
                sqlStmts = "exec spBillsAll '" + pName + "','" + BType + "','" + shpType + "','" + fDate + "','" + tDate + "'";
                ReportDocument rptObject = new ReportDocument();
                DataSet dataSetName = new DataSet();
                dsTableName = new string[1];
                dsTableName[0] = "spBillsAll";

                ConnectionReports.FillDataSet(sqlStmts, dataSetName, dsTableName);

                dt = dataSetName.Tables[0];
                ds.Tables[0].Merge(dt);
                rptDoc.Load(Server.MapPath("CryInvoice.rpt"));
               //rptDoc.Load(@"E:\RAJESH\Live Application\Delhi And Chennai Billing Application\ImpexCube\ImpexCube\Billing\CryInvoice.rpt");
                //rptDoc.Load(@"C:\inetpub\wwwroot\pub impexcryst\Billing\CryInvoice.rpt");
            
                rptDoc.SetDataSource(ds);
                MemoryStream oStream; // using System.IO
                oStream = (MemoryStream)
                rptDoc.ExportToStream(
                CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
                Response.Clear();
                Response.Buffer = true;
                Response.ContentType = "application/pdf";
                Response.BinaryWrite(oStream.ToArray());
                Response.End();
               //CrystalReportViewer1.ReportSource = rptDoc;
                //string[] a = new string[2];
                //ConnectionReports.ShowReportWithParameter(rptDoc, ds, a,fType);
            }
            else
            {
                ImpexCube.Billing.XSD.dsdebit ds = new ImpexCube.Billing.XSD.dsdebit(); // .xsd file name
                DataTable dt = new DataTable();

                dt.TableName = "Crystal Report Example";
                sqlStmts = "exec spBillsAll '" + pName + "','" + BType + "','" + shpType + "','" + fDate + "','" + tDate + "'";


                ReportDocument rptObject = new ReportDocument();
                DataSet dataSetName = new DataSet();
                dsTableName = new string[1];
                dsTableName[0] = "spBillsAll";

                ConnectionReports.FillDataSet(sqlStmts, dataSetName, dsTableName);

                dt = dataSetName.Tables[0];
                ds.Tables[0].Merge(dt);

                rptDoc.Load(Server.MapPath("CryDebitNote.rpt"));

              //  rptDoc.Load(Server.MapPath("CryDebitNote.rpt"));
                //rptDoc.Load(@"C:\inetpub\wwwroot\pub impexcryst\Billing\CryDebitNote.rpt");
                rptDoc.SetDataSource(ds);
                MemoryStream oStream; // using System.IO
                oStream = (MemoryStream)
                rptDoc.ExportToStream(
                CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
                Response.Clear();
                Response.Buffer = true;
                Response.ContentType = "application/pdf";
                Response.BinaryWrite(oStream.ToArray());
                Response.End();
                //CrystalReportViewer1.ReportSource = rptDoc;
                //string[] a = new string[2];
                //ConnectionReports.ShowReportWithParameter(rptDoc, ds,a, fType);
            }

        }
    
    }
}