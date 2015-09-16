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
//--for CrystalReports's ReportDocument.
using CrystalDecisions.CrystalReports.Engine;

public partial class CryInvoiceReportCTR : System.Web.UI.Page 
{

   // string strConn = ConfigurationManager.AppSettings["ConnectionOLEDB"];
    //string strconn = ConfigurationManager.AppSettings["ConnectionImpex"];
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
        string forMat = "PD";
        String invNo = (string)Session["INVOICECTR"];
        string bill = (string)Session["BILLTYPE"];
        GetReports(invNo, bill, forMat);
   
    }
    //public DataSet GetData()
    //{
    //    String invNo = (string)Session["INVOICECTR"];
    //    String strCmd = "";
       
    //    string bill = (string)Session["BILLTYPE"];
    //    if (bill == "SB")
    //        strCmd = "select * from iec_invoiceNEW mst ,iec_invoiceNEW_DTL dtl where mst.invoice=dtl.invoice and mst.invoice='" + invNo + "' order by dtl.sno";
    //    else
    //        strCmd = "select * from iec_debit mst ,iec_debit_DTL dtl where mst.invoice=dtl.invoice and mst.invoice='" + invNo + "' order by dtl.sno";
        
    //    OleDbConnection sqlConn = new OleDbConnection(strConn);
       
    //    OleDbDataAdapter da = new OleDbDataAdapter(strCmd, sqlConn);
    //    DataSet ds = new DataSet();
    //    //--this statement is very important, here the table name should 
    //    //--match with the XML Schema table name 
    //    da.Fill(ds, "CryQuote");
    //    return ds;
    //}
    protected void GetReports(string INO,string bill,string forMat)
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
            rptObject.Load(Request.PhysicalApplicationPath + @"Billing/Reports/CrystalReportSB.rpt");
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


        ConnectionReports.ShowReportWithParameter(rptObject, dataSetName, new string[] { companyname, CHANo, STRegno, address, address1, branchname }, forMat);

    }
    protected void CrystalReportValue()
    {
        try
        {
            String invNo = (string)Session["INVOICECTR"];
            string bill = (string)Session["BILLTYPE"];
            SqlConnection conn = new SqlConnection(strImpex);
            conn.Open();
            SqlCommand cmd = new SqlCommand("spBills", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@invNO", invNo);
            cmd.Parameters.AddWithValue("@bType", bill);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds, "DD");
            conn.Close();
            String ReptFile;

            
            if (bill == "SB")
                ReptFile = Server.MapPath("~\\Reports\\CrystalReportSB.rpt");
            else
                ReptFile = Server.MapPath("~\\Reports\\CrystalReportDB.rpt");
           
            ReportDocument mdt = new ReportDocument();
            mdt.Load(ReptFile);
            mdt.SetDataSource(ds.Tables[0]);
           
            mdt.SetDatabaseLogon("sa", "123", "version6\\sqlexpress", "PIPL");

            //--Binding report with CrystalReportViewer

            //CrystalReportViewer1.ReportSource = mdt;
            //CrystalReportViewer1.DataBind();
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('" + ex.Message + "');", true);
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
