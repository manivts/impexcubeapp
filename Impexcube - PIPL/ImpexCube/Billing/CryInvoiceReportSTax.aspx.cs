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
using Microsoft.Reporting.WebForms;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Outlook = Microsoft.Office.Interop.Outlook;
using iTextSharp.text;
using MySql;
using MySql.Data.MySqlClient;
using iTextSharp.text.pdf;

using iTextSharp.text.xml.xmp;
//--for CrystalReports's ReportDocument.
using CrystalDecisions.CrystalReports.Engine;
using System.Diagnostics;
public partial class CryInvoiceReportSTax : System.Web.UI.Page 
{
    //string strConn = ConfigurationManager.AppSettings["ConnectionOLEDB"];
    //string strconn = ConfigurationManager.AppSettings["ConnectionImpex"];
    //string strconn1 = (string)ConfigurationManager.AppSettings["ConnectionVisual"];
    //string strconnJSU = (string)ConfigurationManager.AppSettings["ConnectionJobStages"];
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
        //else
          // GetReports(invNo, bill, forMat);
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
    protected void GetReports()
    {
        Session["PRINTALL"] = null;
        string fType = "PD";
        string fDate = (string)Session["FD"];
        string tDate = (string)Session["TD"];
        string BType=(string)Session["BILLTYPE"];
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
        string path = @"E:\c#\pdf";
        if (!(Directory.Exists(path)))
        {
            Directory.CreateDirectory(path);
        }
        string[] filePaths = Directory.GetFiles(path);
        foreach (string filePath in filePaths)
        {
            File.Delete(filePath);
        }
        if (dataSetName.Tables[0].Rows.Count != 0)
        {
            int i = 0;
            string invoiceno = "";
            foreach (DataRow obj1 in dataSetName.Tables[0].Rows)
            {
                invoiceno = obj1["invoice"].ToString();
                Warning[] warnings;
                string[] streamIds;
                string mimeType = "application/pdf";
                string encoding = string.Empty;
                string  extension = "pdf";
                //ObjectDataSource1.FilterExpression = "invoice='" + invoiceno + "'";//" +(string)Session["InvNo"]+ "
                //ObjectDataSource1.DataBind();
                //ObjectDataSource2.DataBind();
                //ReportViewer1.LocalReport.Refresh();
               SqlConnection con = new SqlConnection(strImpex);
               con.Open();
               string qry = "Select * from View_Invoice where invoice='" + invoiceno + "'"; 
               SqlDataAdapter da = new SqlDataAdapter(qry, con);
               DataSet dsData = new DataSet();
               da.Fill(dsData, "data");

               string qry1 = "Select * from View_appdetails";
               SqlDataAdapter da1 = new SqlDataAdapter(qry1, con);
               DataSet dsData1 = new DataSet();
               da1.Fill(dsData1, "data");
               con.Close();
               ReportDataSource rdsAct = new ReportDataSource("DataSet1", dsData.Tables[0]);
               ReportDataSource rdsAct1 = new ReportDataSource("DataSet2", dsData1.Tables[0]);
                ReportViewer viewer = new ReportViewer();
                viewer.LocalReport.Refresh();
                viewer.LocalReport.ReportPath = "ImpInvoice.rdlc"; //This is your rdlc name.
                viewer.LocalReport.DataSources.Add(rdsAct);
                viewer.LocalReport.DataSources.Add(rdsAct1);// Add  datasource here         
                string deviceInfo = "<DeviceInfo>" +
                                "  <OutputFormat>PDF</OutputFormat>" +

                                "  <HumanReadablePDF>True</HumanReadablePDF>" +
                                "</DeviceInfo>";

                byte[] reportBytes = viewer.LocalReport.Render(
             "PDF", deviceInfo, out mimeType, out encoding,
             out extension,
             out streamIds, out warnings);

                Stream resFilestream = new MemoryStream(reportBytes);       //convert the source origByteArray to a stream
                FileStream stream = new FileStream((@"E:\c#\pdf\" + i + "." + extension), FileMode.Create);
                //BinaryWriter bw = new BinaryWriter(stream);
                byte[] ba = new byte[resFilestream.Length];      //create a copy of the byte array, and fill the copy (necessary?)
                resFilestream.Read(ba, 0, ba.Length);
                resFilestream.Close();
                stream.Write(ba, 0, ba.Length);
                //br.Close();               //is this a accident duplicate from the next line?                                          
                stream.Close();
                i++;
            }
            string pathss = @"E:\c#\pdf";
            if ((Directory.Exists(pathss)))
            {
                string[] a = Directory.GetFiles(pathss);
                MergePDFs(@"E:\c#\pdf\out.pdf", a);

            }
            string filepath = (@"E:\c#\pdf\out.pdf");
            System.Diagnostics.Process.Start(filepath);
        }
    }
    private void MergePDFs(string outPutFilePath, params string[] filesPaths)
    {
        List<PdfReader> readerList = new List<PdfReader>();
       

        foreach (string filePath in filesPaths)
        {


            PdfReader pdfReader = new PdfReader(filePath);
            readerList.Add(pdfReader);

        }
        //Define a new output document and its size, type
        Document document = new Document(PageSize.A4, 5, 5, 5, 0);
        //Create blank output pdf file and get the stream to write on it.
        PdfWriter writer = PdfWriter.GetInstance(document, new FileStream(outPutFilePath, FileMode.Create, FileAccess.Write, FileShare.None));
        document.Open();

        foreach (PdfReader reader in readerList)
        {
            for (int i = 1; i <= reader.NumberOfPages; i++)
            {
                PdfImportedPage page = writer.GetImportedPage(reader, i);
                document.Add(iTextSharp.text.Image.GetInstance(page));

                // document.Add(new Paragraph("Hello World"));
            }
        }
        document.Close();
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
                //string pageName = (string)Session["PageName"] + "?invNo=" + (string)Session["INVOICECTR"];
                //Response.Redirect(pageName, false);
                //GetSendMail();
            }
        }
    }
    //protected void GetSendMail()
    //{
    //    if (Session["Maill"] != null)
    //    {
    //        if (Session["Maill"].ToString() == "AttachThePDF")
    //        {
    //            try
    //            {
    //                string invo = (string)Session["INVOICECTR"];
    //                string jno = (string)Session["JOBNO"];
    //                string Query = "select * from iworkreg i ,prt_addr j where i.party_code=j.party_code and i.party_addr=j.addr_code and i.job_no='" + jno + "'";
    //                string email = "";
    //                DataSet ds = GetDataMy(Query);
    //                DataTable dt = ds.Tables[0];
    //                foreach (DataRow row in dt.Rows)
    //                {
    //                    email = row["email"].ToString();
    //                }
    //                if (email == "")
    //                    email = "support@vts.in";
    //                //Online mail
    //                GetMailOnline(email);
    //                Session["Maill"] = null;
    //            }
    //            catch (Exception Error)
    //            {
    //                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('" + Error.Message + "');", true);
    //            }
    //        }
    //    }
    //}
    protected void BtnExport_Click(object sender, EventArgs e)
    {
    }
    protected void GetMailOnline(string mTo)
    {
        string path = (string)Session["AttachmentPath"];
        string mf = (string)Session["USER-NAME"];
        Session["mTO"] = mTo;
        Session["MessSubj"] = "Re: Billing Report" ;
        Response.Redirect("sendMailPIPL.aspx",false);
    }
    //public DataSet GetDataMy(string Query)
    //{
    //    MySqlConnection conn1 = new MySqlConnection(strconn1);
    //    MySqlDataAdapter da1 = new MySqlDataAdapter(Query, conn1);
    //    DataSet ds1 = new DataSet();
    //    da1.Fill(ds1, "datas");
    //    return ds1;
    //}
    protected void GetMailOutlook(string mTo)
    {
    }
}
