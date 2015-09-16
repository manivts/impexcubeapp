using System;
using System.Collections;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Xml.Linq;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections.Generic;
using System.Text;
//using MySql;
//using MySql.Data;
//using MySql.Data.MySqlClient;


/// <summary>
/// Summary description for AutoComplete
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
[System.Web.Script.Services.ScriptService]

public class AutoComplete : System.Web.Services.WebService
{
    [WebMethod(EnableSession = true)]
    public string[] GetJobCustomer(string prefixText)
    {
        DataSet ds = new DataSet();
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Constr"].ConnectionString);
        //string strSql = "SELECT distinct ImpExpName FROM M_JobImportCreation WHERE ImpExpName LIKE '" + prefixText + "%' ";
        string strSql = "select distinct Importer from T_importer WHERE Importer LIKE '" + prefixText + "%' ";
        SqlCommand sqlComd = new SqlCommand(strSql, conn);
        conn.Open();
        SqlDataAdapter sqlAdpt = new SqlDataAdapter();
        sqlAdpt.SelectCommand = sqlComd;

        sqlAdpt.Fill(ds);

        string[] cntName = new string[ds.Tables[0].Rows.Count];

        int i = 0;

        try
        {
            foreach (DataRow rdr in ds.Tables[0].Rows)
            {
                cntName.SetValue(rdr["Importer"].ToString(), i);
                i++;
            }
        }
        catch { }
        finally
        {
            conn.Close();

        }
        return cntName;

    }

    [WebMethod(EnableSession = true)]
    public string[] GetJobNo(string prefixText)// Used in frmMultipleBillingJobUpdate.aspx
    {
        DataSet ds = new DataSet();
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Constr"].ConnectionString);

        string strSql = "SELECT distinct JobNo FROM T_JobCreation WHERE JobNo LIKE '%" + prefixText + "%'  order by JobNo ";
        //string strSql = "select distinct JobNo from M_JobImportCreation where JobNo LIKE '%" + prefixText + "%' and  FYear like '%" + (string)Session["FYear"] + "%'";
        SqlCommand sqlComd = new SqlCommand(strSql, conn);
        conn.Open();
        SqlDataAdapter sqlAdpt = new SqlDataAdapter();
        sqlAdpt.SelectCommand = sqlComd;

        sqlAdpt.Fill(ds);

        string[] Jobno = new string[ds.Tables[0].Rows.Count];

        int i = 0;

        try
        {
            foreach (DataRow rdr in ds.Tables[0].Rows)
            {
                Jobno.SetValue(rdr["JobNo"].ToString(), i);
                i++;
            }
        }
        catch { }
        finally
        {
            conn.Close();

        }

        return Jobno;


    }

    [WebMethod(EnableSession = true)]
    public string[] GetJobNoIworkreg(string prefixText)//Used in frmJobCancel.aspx
    {
        DataSet ds = new DataSet();
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Constr"].ConnectionString);

        string strSql = "SELECT distinct Job_No FROM iworkreg_jobstatus WHERE Job_No LIKE '%" + prefixText + "%' and Job_No like '%" + (string)Session["FYear"] + "%' order by Job_No ";
        //string strSql = "select distinct JobNo from M_JobImportCreation where JobNo LIKE '%" + prefixText + "%' and  FYear like '%" + (string)Session["FYear"] + "%'";
        SqlCommand sqlComd = new SqlCommand(strSql, conn);
        conn.Open();
        SqlDataAdapter sqlAdpt = new SqlDataAdapter();
        sqlAdpt.SelectCommand = sqlComd;

        sqlAdpt.Fill(ds);

        string[] Jobno = new string[ds.Tables[0].Rows.Count];

        int i = 0;

        try
        {
            foreach (DataRow rdr in ds.Tables[0].Rows)
            {
                Jobno.SetValue(rdr["Job_No"].ToString(), i);
                i++;
            }
        }
        catch { }
        finally
        {
            conn.Close();

        }
        return Jobno;
    }

    [WebMethod]
    public string[] GetChargeMaster(string prefixText)
    {
        DataSet ds = new DataSet();
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Constr"].ConnectionString);
        string strSql = "SELECT Distinct charge_desc FROM M_Charge WHERE charge_desc LIKE '" + prefixText + "%' ";
        // string strSql = "SELECT iec_name FROM i_iec_stand union select iec_name from i_iec_nonStand where iec_name like '" + prefixText + "%' ";
        SqlCommand sqlComd = new SqlCommand(strSql, conn);
        conn.Open();
        SqlDataAdapter sqlAdpt = new SqlDataAdapter();
        sqlAdpt.SelectCommand = sqlComd;

        sqlAdpt.Fill(ds);

        string[] cntName = new string[ds.Tables[0].Rows.Count];

        int i = 0;

        try
        {
            foreach (DataRow rdr in ds.Tables[0].Rows)
            {
                cntName.SetValue(rdr["charge_desc"].ToString(), i);
                i++;
            }
        }
        catch { }
        finally
        {
            conn.Close();
        }
        return cntName;
    }


    [WebMethod(EnableSession = true)]
    public string[] GetInvNoEDIT(string prefixText)
    {
        string fyear = (string)Session["FinancialYear"];
        string Mode = (string)Session["shp"];
        string Bill = (string)Session["EdBill"];
        string strSql = "";
        DataSet ds = new DataSet();
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Constr"].ConnectionString);
        if (Bill == "SB")
            strSql = "SELECT invoice FROM M_iec_invoiceNew WHERE invoice LIKE '%" + prefixText + "%' and fyear='" + fyear + "' and mode='" + Mode + "'   order by invoice";
        else
            strSql = "SELECT invoice FROM M_iec_debit WHERE invoice LIKE '%" + prefixText + "%' and fyear='" + fyear + "' and mode='" + Mode + "'   order by invoice";
        SqlCommand sqlComd = new SqlCommand(strSql, conn);
        conn.Open();
        SqlDataAdapter sqlAdpt = new SqlDataAdapter();
        sqlAdpt.SelectCommand = sqlComd;

        sqlAdpt.Fill(ds);

        string[] cntName = new string[ds.Tables[0].Rows.Count];

        int i = 0;

        try
        {
            foreach (DataRow rdr in ds.Tables[0].Rows)
            {
                cntName.SetValue(rdr["invoice"].ToString(), i);
                i++;
            }
        }
        catch { }
        finally
        {
            conn.Close();

        }
        return cntName;
    }

    [WebMethod(EnableSession = true)]
    public string[] GetPartyEDIT(string prefixText)
    {
        string fyear = (string)Session["FinancialYear"];
        string Mode = (string)Session["shp"];
        string Bill = (string)Session["EdBill"];
        string strSql = "";
        DataSet ds = new DataSet();
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Constr"].ConnectionString);
        if (Bill == "SB")
            strSql = "SELECT distinct compName FROM M_iec_invoiceNew WHERE compName LIKE '" + prefixText + "%' and fyear='" + fyear + "' and mode='" + Mode + "' ";
        else
            strSql = "SELECT distinct compName FROM M_iec_debit WHERE compName LIKE '" + prefixText + "%' and fyear='" + fyear + "' and mode='" + Mode + "' ";
        SqlCommand sqlComd = new SqlCommand(strSql, conn);
        conn.Open();
        SqlDataAdapter sqlAdpt = new SqlDataAdapter();
        sqlAdpt.SelectCommand = sqlComd;

        sqlAdpt.Fill(ds);

        string[] cntName = new string[ds.Tables[0].Rows.Count];

        int i = 0;

        try
        {
            foreach (DataRow rdr in ds.Tables[0].Rows)
            {
                cntName.SetValue(rdr["compName"].ToString(), i);
                i++;
            }
        }
        catch { }
        finally
        {
            conn.Close();

        }

        return cntName;
    }

    [WebMethod(EnableSession = true)]
    public string[] GetInvJobNoEdit(string prefixText)
    {
        string fyear = (string)Session["FinancialYear"];
        string Mode = (string)Session["shp"];
        string Bill = (string)Session["EdBill"];
        string strSql = "";
        DataSet ds = new DataSet();
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Constr"].ConnectionString);
        if (Bill == "SB")
            strSql = "SELECT distinct jobno FROM M_iec_invoiceNew WHERE jobno LIKE '%" + prefixText + "%' and fyear='" + fyear + "' and mode='" + Mode + "' ";
        else
            strSql = "SELECT distinct jobno FROM M_iec_debit WHERE jobno LIKE '%" + prefixText + "%' and fyear='" + fyear + "' and mode='" + Mode + "' ";
        SqlCommand sqlComd = new SqlCommand(strSql, conn);
        conn.Open();
        SqlDataAdapter sqlAdpt = new SqlDataAdapter();
        sqlAdpt.SelectCommand = sqlComd;

        sqlAdpt.Fill(ds);

        string[] cntName = new string[ds.Tables[0].Rows.Count];

        int i = 0;

        try
        {
            foreach (DataRow rdr in ds.Tables[0].Rows)
            {
                cntName.SetValue(rdr["jobno"].ToString(), i);
                i++;
            }
        }
        catch { }
        finally
        {
            conn.Close();

        }

        return cntName;
    }

    [WebMethod(EnableSession = true)]
    public string[] GetCompany(string prefixText)
    {
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Constr"].ConnectionString);
        conn.Open();
        DataSet ds = new DataSet();
        string Command = "";
        string code = (string)Session["BillTY"];
        if (Session["BillTY"] != null)
        {

            if (code == "SB" || code == "ATLSB")
            {
                Command = "SELECT distinct(compName) FROM M_iec_invoiceNew where compName like '" + prefixText + "%'";
            }
            else if (code == "DB" || code == "ATLDB" || code == "CD")
            {
                Command = "SELECT distinct(compName) FROM M_iec_debit where compName like '" + prefixText + "%'";
            }
        }

        SqlCommand Cmd = new SqlCommand(Command, conn);
        SqlDataAdapter da = new SqlDataAdapter(Cmd);
        da.Fill(ds);
        string[] PortCode = new string[ds.Tables[0].Rows.Count];
        int i = 0;
        foreach (DataRow rdr in ds.Tables[0].Rows)
        {
            PortCode.SetValue(rdr["compName"].ToString(), i);
            i++;

        }
        conn.Close();

        return PortCode;

    }

    [WebMethod(EnableSession = true)]
    public string[] GetInvJobNo(string prefixText)
    {
        string fyear = (string)Session["FinancialYear"];

        string strSql = "";
        DataSet ds = new DataSet();
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Constr"].ConnectionString);


        strSql = "SELECT jobno FROM M_iec_invoiceNew WHERE jobno LIKE '" + prefixText + "%' and fyear='" + fyear + "' " +
         "union SELECT jobno FROM M_iec_debit WHERE jobno LIKE '" + prefixText + "%' and fyear='" + fyear + "' order by jobno";
        SqlCommand sqlComd = new SqlCommand(strSql, conn);
        conn.Open();
        SqlDataAdapter sqlAdpt = new SqlDataAdapter();
        sqlAdpt.SelectCommand = sqlComd;

        sqlAdpt.Fill(ds);

        string[] cntName = new string[ds.Tables[0].Rows.Count];

        int i = 0;

        try
        {
            foreach (DataRow rdr in ds.Tables[0].Rows)
            {
                cntName.SetValue(rdr["jobno"].ToString(), i);
                i++;
            }
        }
        catch { }
        finally
        {
            conn.Close();

        }

        return cntName;

    }
    [WebMethod]
    public string[] GetRITCCode(string prefixText)
    {
        DataSet ds = new DataSet();
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Constr"].ConnectionString);
        //string strSql = "Select Distinct RITCNo from M_Product WHERE RITCNo LIKE '%" + prefixText + "%'";
        string strSql = "Select Distinct RITCNo from RITCCode WHERE RITCNo LIKE '" + prefixText + "%'";
        SqlCommand sqlComd = new SqlCommand(strSql, conn);
        conn.Open();
        SqlDataAdapter sqlAdpt = new SqlDataAdapter();
        sqlAdpt.SelectCommand = sqlComd;
        sqlAdpt.Fill(ds);
        string[] cntName = new string[ds.Tables[0].Rows.Count];
        int i = 0;
        try
        {
            foreach (DataRow rdr in ds.Tables[0].Rows)
            {
                cntName.SetValue(rdr["RITCNo"].ToString(), i);
                i++;
            }
        }
        catch { }
        finally
        {
            conn.Close();
        }
        return cntName;
    }
    [WebMethod]
    public string[] GetNotification(string prefixText)
    {
        DataSet ds = new DataSet();
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Constr"].ConnectionString);
        string strSql = "Select Distinct BCDNotn,SLNO from View_Notn WHERE BCDNotn LIKE '%" + prefixText + "%' Order By SLNO";
        //string strSql = "Select BasicDutyNotn,BasicDutySno from T_Product where RITCNo WHERE RITCNo LIKE '%" + prefixText + "%'";
        SqlCommand sqlComd = new SqlCommand(strSql, conn);
        conn.Open();
        SqlDataAdapter sqlAdpt = new SqlDataAdapter();
        sqlAdpt.SelectCommand = sqlComd;
        sqlAdpt.Fill(ds);
        string[] cntName = new string[ds.Tables[0].Rows.Count];
        int i = 0;
        try
        {
            foreach (DataRow rdr in ds.Tables[0].Rows)
            {
                cntName.SetValue(rdr["BCDNotn"].ToString(), i);
                i++;
            }
        }
        catch { }
        finally
        {
            conn.Close();

        }
        return cntName;
    }

    [WebMethod]
    public string[] GetSAPTANotification(string prefixText)
    {
        DataSet ds = new DataSet();
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Constr"].ConnectionString);
        string strSql = "Select  Notification + '     |  ' + SerialNo + '     |  ' + CONVERT(Varchar(10), DutyRate, 103) AS SAPTANotification from M_SAPTANotification WHERE Notification + '     |  ' + SerialNo + '     |  ' + CONVERT(Varchar(10), DutyRate, 103) LIKE '%" + prefixText + "%'";
        SqlCommand sqlComd = new SqlCommand(strSql, conn);
        conn.Open();
        SqlDataAdapter sqlAdpt = new SqlDataAdapter();
        sqlAdpt.SelectCommand = sqlComd;
        sqlAdpt.Fill(ds);
        string[] cntName = new string[ds.Tables[0].Rows.Count];
        int i = 0;
        try
        {
            foreach (DataRow rdr in ds.Tables[0].Rows)
            {
                cntName.SetValue(rdr["SAPTANotification"].ToString(), i);
                i++;
            }
        }
        catch { }
        finally
        {
            conn.Close();

        }
        return cntName;
    }

    [WebMethod]
    public string[] GetEduNotif(string prefixText)
    {
        DataSet ds = new DataSet();
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Constr"].ConnectionString);
        string strSql = "Select Distinct Distinct Notn from M_Notn  WHERE Notn LIKE '%" + prefixText + "%' And NOTN_TYPE='C'";
        // string strSql = "Select EduCessNotn,EduCessSNo from T_Product where RITCNo WHERE RITCNo LIKE '%" + prefixText + "%'";
        SqlCommand sqlComd = new SqlCommand(strSql, conn);
        conn.Open();
        SqlDataAdapter sqlAdpt = new SqlDataAdapter();
        sqlAdpt.SelectCommand = sqlComd;
        sqlAdpt.Fill(ds);
        string[] cntName = new string[ds.Tables[0].Rows.Count];
        int i = 0;
        try
        {
            foreach (DataRow rdr in ds.Tables[0].Rows)
            {
                cntName.SetValue(rdr["Notn"].ToString(), i);
                i++;
            }
        }
        catch { }
        finally
        {
            conn.Close();

        }
        return cntName;
    }
    [WebMethod]
    public string[] GetCVDNotif(string prefixText)
    {
        DataSet ds = new DataSet();
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Constr"].ConnectionString);
        string strSql = "Select Distinct Notn from M_Notn where Notn LIKE '%" + prefixText + "%' And NOTN_TYPE='E'";
        //  string strSql = "Select ExCVDNotn,ExCVDSlNo from T_Product where RITCNo LIKE '%" + prefixText + "%'";
        SqlCommand sqlComd = new SqlCommand(strSql, conn);
        conn.Open();
        SqlDataAdapter sqlAdpt = new SqlDataAdapter();
        sqlAdpt.SelectCommand = sqlComd;
        sqlAdpt.Fill(ds);
        string[] cntName = new string[ds.Tables[0].Rows.Count];
        int i = 0;
        try
        {
            foreach (DataRow rdr in ds.Tables[0].Rows)
            {
                cntName.SetValue(rdr["Notn"].ToString(), i);
                i++;
            }
        }
        catch { }
        finally
        {
            conn.Close();

        }
        return cntName;
    }
    [WebMethod]
    public string[] GetAddNotif(string prefixText)
    {
        DataSet ds = new DataSet();
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Constr"].ConnectionString);
        string strSql = "Select Distinct Notn from M_Notn where Notn LIKE '%" + prefixText + "%' And NOTN_TYPE='C'";
        // string strSql = "Select AddlExNotn,AddlExSlNo from T_Product where RITCNo LIKE '%" + prefixText + "%'";
        SqlCommand sqlComd = new SqlCommand(strSql, conn);
        conn.Open();
        SqlDataAdapter sqlAdpt = new SqlDataAdapter();
        sqlAdpt.SelectCommand = sqlComd;
        sqlAdpt.Fill(ds);
        string[] cntName = new string[ds.Tables[0].Rows.Count];
        int i = 0;
        try
        {
            foreach (DataRow rdr in ds.Tables[0].Rows)
            {
                cntName.SetValue(rdr["Notn"].ToString(), i);
                i++;
            }
        }
        catch { }
        finally
        {
            conn.Close();

        }
        return cntName;
    }
    [WebMethod]
    public string[] GetEXIMCode(string prefixText)
    {
        DataSet ds = new DataSet();
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Constr"].ConnectionString);
        string strSql = "Select Distinct EXIM_Code+Scheme As EXIM_Code from M_EximSchm where EXIM_Code LIKE '%" + prefixText + "%' And ApplicableImpSchemes<>''";
        SqlCommand sqlComd = new SqlCommand(strSql, conn);
        conn.Open();
        SqlDataAdapter sqlAdpt = new SqlDataAdapter();
        sqlAdpt.SelectCommand = sqlComd;
        sqlAdpt.Fill(ds);
        string[] cntName = new string[ds.Tables[0].Rows.Count];
        int i = 0;
        try
        {
            foreach (DataRow rdr in ds.Tables[0].Rows)
            {
                cntName.SetValue(rdr["EXIM_Code"].ToString(), i);
                i++;
            }
        }
        catch { }
        finally
        {
            conn.Close();
        }
        return cntName;
    }

    [WebMethod(EnableSession = true)]
    public string[] GetSchemeCode(string prefixText)
    {
        string SchemeName = (string)Session["SchemeName"];
        DataSet ds = new DataSet();
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Constr"].ConnectionString);
        //string strSql = "Select Distinct NOTN+SLNo+RTA+CVD_RTA+AddlDuty2005_RTA As NOTN from M_SchemeNotn where NOTN LIKE '%" + prefixText + "%' And SchemeName='" + SchemeName + "'";
        string strSql = "Select Distinct NOTN+' '+SLNo+' BasicDuty:'+Convert(Varchar(10),RTA)+ 'CVD:' +Convert(Varchar(10),CVD_RTA)+'AddlDuty:'+Convert(Varchar(10),AddlDuty2005_RTA) As NOTN from M_SchemeNotn where NOTN LIKE '%" + prefixText + "%' And SchemeName='" + SchemeName + "' And Notn+SLNo+Convert(Varchar(10),RTA)+Convert(Varchar(10),CVD_RTA)+Convert(Varchar(10),AddlDuty2005_RTA)!=''";
        SqlCommand sqlComd = new SqlCommand(strSql, conn);
        conn.Open();
        SqlDataAdapter sqlAdpt = new SqlDataAdapter();
        sqlAdpt.SelectCommand = sqlComd;
        sqlAdpt.Fill(ds);
        string[] cntName = new string[ds.Tables[0].Rows.Count];
        int i = 0;
        try
        {
            foreach (DataRow rdr in ds.Tables[0].Rows)
            {
                cntName.SetValue(rdr["NOTN"].ToString(), i);
                i++;
            }
        }
        catch { }
        finally
        {
            conn.Close();
        }
        return cntName;
    }
    [WebMethod]
    public string[] GetSearchAccountName(string prefixText)
    {
        DataSet ds = new DataSet();
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Constr"].ConnectionString);
        string strSql = "Select Distinct Search from View_AccountMaster where Search LIKE '%" + prefixText + "%'";
        SqlCommand sqlComd = new SqlCommand(strSql, conn);
        conn.Open();
        SqlDataAdapter sqlAdpt = new SqlDataAdapter();
        sqlAdpt.SelectCommand = sqlComd;
        sqlAdpt.Fill(ds);
        string[] cntName = new string[ds.Tables[0].Rows.Count];
        int i = 0;
        try
        {
            foreach (DataRow rdr in ds.Tables[0].Rows)
            {
                cntName.SetValue(rdr["Search"].ToString(), i);
                i++;
            }
        }
        catch { }
        finally
        {
            conn.Close();

        }
        return cntName;
    }
    //Search Customer
    [WebMethod]
    public string[] GetSearchCustomer(string prefixText)
    {
        DataSet ds = new DataSet();
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Constr"].ConnectionString);
        string strSql = "Select Distinct Search from View_AccountMaster where  Search LIKE '%" + prefixText + "%'";
        SqlCommand sqlComd = new SqlCommand(strSql, conn);
        conn.Open();
        SqlDataAdapter sqlAdpt = new SqlDataAdapter();
        sqlAdpt.SelectCommand = sqlComd;
        sqlAdpt.Fill(ds);
        string[] cntName = new string[ds.Tables[0].Rows.Count];
        int i = 0;
        try
        {
            foreach (DataRow rdr in ds.Tables[0].Rows)
            {
                cntName.SetValue(rdr["Search"].ToString(), i);
                i++;
            }
        }
        catch { }
        finally
        {
            conn.Close();

        }
        return cntName;
    }
    //Search Consignor
    [WebMethod]
    public string[] GetSearchConsignor(string prefixText)
    {
        DataSet ds = new DataSet();
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Constr"].ConnectionString);
        string strSql = "Select Distinct Search from View_AccountMaster where AccountType = 'Consignor' And  Search LIKE '%" + prefixText + "%'";
        //Select Distinct AccountName from View_AccountMaster where AccountType = 'Consignor' And  AccountName LIKE '%" + prefixText + "%'";
        SqlCommand sqlComd = new SqlCommand(strSql, conn);
        conn.Open();
        SqlDataAdapter sqlAdpt = new SqlDataAdapter();
        sqlAdpt.SelectCommand = sqlComd;
        sqlAdpt.Fill(ds);
        string[] cntName = new string[ds.Tables[0].Rows.Count];
        int i = 0;
        try
        {
            foreach (DataRow rdr in ds.Tables[0].Rows)
            {
                cntName.SetValue(rdr["Search"].ToString(), i);
                i++;
            }
        }
        catch { }
        finally
        {
            conn.Close();

        }
        return cntName;
    }

    [WebMethod]
    public string[] GetProductName(string prefixText)
    {
        DataSet ds = new DataSet();
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Constr"].ConnectionString);
        string strSql = "Select Distinct ProductDesc from M_Product where ProductDesc LIKE '%" + prefixText + "%'";
        // SqlCommand sqlComd = new SqlCommand(strSql, conn);
        conn.Open();
        SqlDataAdapter sqlAdpt = new SqlDataAdapter(strSql, conn);
        // sqlAdpt.SelectCommand = sqlComd;
        sqlAdpt.Fill(ds);
        string[] cntName = new string[ds.Tables[0].Rows.Count];
        int i = 0;
        try
        {
            foreach (DataRow rdr in ds.Tables[0].Rows)
            {
                cntName.SetValue(rdr["ProductDesc"].ToString(), i);
                i++;
            }
        }
        catch { }
        finally
        {
            conn.Close();

        }
        return cntName;
    }

    [WebMethod]
    public string[] GetProductCode(string prefixText)
    {
        DataSet ds = new DataSet();
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Constr"].ConnectionString);
        string strSql = "Select Distinct ProductCode from M_Product where ProductCode LIKE '%" + prefixText + "%'";
        // SqlCommand sqlComd = new SqlCommand(strSql, conn);
        conn.Open();
        SqlDataAdapter sqlAdpt = new SqlDataAdapter(strSql, conn);
        // sqlAdpt.SelectCommand = sqlComd;
        sqlAdpt.Fill(ds);
        string[] cntName = new string[ds.Tables[0].Rows.Count];
        int i = 0;
        try
        {
            foreach (DataRow rdr in ds.Tables[0].Rows)
            {
                cntName.SetValue(rdr["ProductCode"].ToString(), i);
                i++;
            }
        }
        catch { }
        finally
        {
            conn.Close();

        }
        return cntName;
    }

    [WebMethod(EnableSession = true)]
    public string[] GetBill(string prefixText)
    {
        string strSql;
        string fyear = (string)Session["FinancialYear"];
        string Mode = (string)Session["shp"];
        string BTYPE = (string)Session["PrintBill"];
        DataSet ds = new DataSet();
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Constr"].ConnectionString);
        if (BTYPE == "SB")
        {
            strSql = "SELECT invoice FROM M_iec_invoiceNew WHERE invoice LIKE '%" + prefixText + "%' and mode='" + Mode + "' and fyear='" + fyear + "' order by invoiceNo ";
        }
        else
        {
            strSql = "SELECT invoice FROM M_iec_Debit WHERE invoice LIKE '%" + prefixText + "%' and mode='" + Mode + "' and fyear='" + fyear + "' order by invoiceNo ";

        }
        SqlCommand sqlComd = new SqlCommand(strSql, conn);
        conn.Open();
        SqlDataAdapter sqlAdpt = new SqlDataAdapter();
        sqlAdpt.SelectCommand = sqlComd;

        sqlAdpt.Fill(ds);

        string[] cntName = new string[ds.Tables[0].Rows.Count];

        int i = 0;

        try
        {
            foreach (DataRow rdr in ds.Tables[0].Rows)
            {
                cntName.SetValue(rdr["invoice"].ToString(), i);
                i++;
            }
        }
        catch { }
        finally
        {
            conn.Close();

        }

        return cntName;

    }

    [WebMethod(EnableSession = true)]
    public string[] GetCompanyTally(string prefixText)
    {
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Constr"].ConnectionString);
        // string MysqlConn = (string)ConfigurationSettings.AppSettings["ConnectionImpex"];
        // SqlConnection conn = new SqlConnection(Conn);
        conn.Open();
        DataSet ds = new DataSet();
        string Command = "";
        string bType = (string)Session["BillTY"];
        if (bType != null)
        {
            string code = (string)Session["BillTY"];
            if (code == "SB" || code == "ATLSB" || code == "0")
            {
                Command = "SELECT distinct(compName) FROM M_iec_invoiceNew where compName like '" + prefixText + "%'";
            }
            else if (code == "DB" || code == "ATLDB" || code == "ATLDEM" || code == "CD")
            {
                Command = "SELECT distinct(compName) FROM M_iec_debit where compName like '" + prefixText + "%'";
            }
        }
        SqlCommand Cmd = new SqlCommand(Command, conn);
        SqlDataAdapter da = new SqlDataAdapter(Cmd);
        da.Fill(ds);
        string[] PortCode = new string[ds.Tables[0].Rows.Count];
        int i = 0;
        foreach (DataRow rdr in ds.Tables[0].Rows)
        {
            PortCode.SetValue(rdr["compName"].ToString(), i);
            i++;

        }
        conn.Close();

        return PortCode;

    }

    [WebMethod(EnableSession = true)]
    public string[] GetBillJNOTALLY(string prefixText)
    {
        string strSql;
        string pN = "";
        string fy = (string)Session["FinancialYear"];
        string BTYPE = Session["BillTY"].ToString();
        string pname = (string)Session["eParty"];
        if (pname != "")
            pN = " and compName='" + pname + "'";
        DataSet ds = new DataSet();
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Constr"].ConnectionString);
        //SqlConnection conn = new SqlConnection(ConfigurationManager.AppSettings["ConnectionImpex"]);
        if (BTYPE == "SB" || BTYPE == "ATLSB" || BTYPE == "EXPSB" || BTYPE == "0")
        {
            strSql = "SELECT  jobNo FROM M_iec_invoiceNew i,T_iec_invoicenew_dtl j WHERE i.invoice=j.invoice and  i.jobNo LIKE '%" + prefixText + "%' " + pN + " and fyear='" + fy + "' order by i.invoiceNo ";
        }
        else
        {
            strSql = "SELECT  jobNo FROM M_iec_Debit i,T_iec_debit_dtl j WHERE i.invoice=j.invoice and i.jobNo LIKE '%" + prefixText + "%' " + pN + " and fyear='" + fy + "' order by i.invoiceNo ";

        }
        SqlCommand sqlComd = new SqlCommand(strSql, conn);
        conn.Open();
        SqlDataAdapter sqlAdpt = new SqlDataAdapter();
        sqlAdpt.SelectCommand = sqlComd;

        sqlAdpt.Fill(ds);

        string[] cntName = new string[ds.Tables[0].Rows.Count];

        int i = 0;

        try
        {
            foreach (DataRow rdr in ds.Tables[0].Rows)
            {
                cntName.SetValue(rdr["jobNo"].ToString(), i);
                i++;
            }
        }
        catch { }
        finally
        {
            conn.Close();

        }

        return cntName;

    }

    [WebMethod(EnableSession = true)]
    public string[] GetCompanyS(string prefixText)
    {
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Constr"].ConnectionString);
        //string MysqlConn = (string)ConfigurationSettings.AppSettings["ConnectionImpex"];
        //SqlConnection conn = new SqlConnection(MysqlConn);
        conn.Open();
        DataSet ds = new DataSet();
        string Command = "";
        string code = (string)Session["BillTY"];
        if (Session["BillTY"] != null)
        {

            if (code == "SB" || code == "ATLSB")
            {
                Command = "SELECT distinct(compName) FROM M_iec_invoiceNew where compName like '" + prefixText + "%' and stsID is not null";
            }
            else if (code == "DB" || code == "ATLDB" || code == "CD")
            {
                Command = "SELECT distinct(compName) FROM M_iec_debit where compName like '" + prefixText + "%' and stsID is not null";
            }
        }

        SqlCommand Cmd = new SqlCommand(Command, conn);
        SqlDataAdapter da = new SqlDataAdapter(Cmd);
        da.Fill(ds);
        string[] PortCode = new string[ds.Tables[0].Rows.Count];
        int i = 0;
        foreach (DataRow rdr in ds.Tables[0].Rows)
        {
            PortCode.SetValue(rdr["compName"].ToString(), i);
            i++;

        }
        conn.Close();

        return PortCode;

    }

    [WebMethod(EnableSession = true)]
    public string[] GetLedger(string prefixText)
    {

        string strSql = "";
        string pn = "";
        string pName = (string)Session["PNAME"];
        string Bill = (string)Session["EdBill"];
        if (pName != "")
        pn = "and i.compName='" + pName + "'";

        DataSet ds = new DataSet();
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Constr"].ConnectionString);
        if (Bill == "DB")
            strSql = "SELECT distinct j.charge_desc FROM M_iec_debit i, T_iec_debit_dtl j WHERE i.invoice=j.invoice and  j.charge_desc LIKE '" + prefixText + "%' " + pn + " order by j.charge_desc";
        else
            strSql = "SELECT distinct j.charge_desc FROM M_iec_invoiceNew i,T_iec_invoiceNew_dtl j WHERE i.invoice=j.invoice and j.charge_desc LIKE '" + prefixText + "%' " + pn + " order by j.charge_desc";

        SqlCommand sqlComd = new SqlCommand(strSql, conn);
        conn.Open();
        SqlDataAdapter sqlAdpt = new SqlDataAdapter();
        sqlAdpt.SelectCommand = sqlComd;

        sqlAdpt.Fill(ds);
        string[] cntName = new string[ds.Tables[0].Rows.Count];

        int i = 0;

        try
        {
            foreach (DataRow rdr in ds.Tables[0].Rows)
            {
                cntName.SetValue(rdr["charge_desc"].ToString(), i);
                i++;
            }
        }
        catch { }
        finally
        {
            conn.Close();

        }
        return cntName;

    }
    [WebMethod]
    public string[] GetCustomer(string prefixText)
    {
        DataSet ds = new DataSet();
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Constr"].ConnectionString);
        string strSql = "Select Distinct Search from View_AccountMaster where   AccountType IN ('Customer') And  Search LIKE '%" + prefixText + "%'";
        SqlCommand sqlComd = new SqlCommand(strSql, conn);
        conn.Open();
        SqlDataAdapter sqlAdpt = new SqlDataAdapter();
        sqlAdpt.SelectCommand = sqlComd;
        sqlAdpt.Fill(ds);
        string[] cntName = new string[ds.Tables[0].Rows.Count];
        int i = 0;
        try
        {
            foreach (DataRow rdr in ds.Tables[0].Rows)
            {
                cntName.SetValue(rdr["Search"].ToString(), i);
                i++;
            }
        }
        catch { }
        finally
        {
            conn.Close();

        }
        return cntName;
    }
    [WebMethod]
    public string[] GetEDIRegnNo(string prefixText)
    {
        DataSet ds = new DataSet();
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Constr"].ConnectionString);
        string strSql = "Select Distinct EDIRegnNo from M_LicenceMaster  where EDIRegnNo LIKE '%" + prefixText + "%'";
        SqlCommand sqlComd = new SqlCommand(strSql, conn);
        conn.Open();
        SqlDataAdapter sqlAdpt = new SqlDataAdapter();
        sqlAdpt.SelectCommand = sqlComd;
        sqlAdpt.Fill(ds);
        string[] cntName = new string[ds.Tables[0].Rows.Count];
        int i = 0;
        try
        {
            foreach (DataRow rdr in ds.Tables[0].Rows)
            {
                cntName.SetValue(rdr["EDIRegnNo"].ToString(), i);
                i++;
            }
        }
        catch { }
        finally
        {
            conn.Close();

        }
        return cntName;
    }
    [WebMethod]
    public string[] UOMList(string prefixText)
    {
        DataSet ds = new DataSet();
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Constr"].ConnectionString);
        string strSql = "Select Distinct UnitShort from M_Unit  where UnitShort LIKE '%" + prefixText + "%'";
        SqlCommand sqlComd = new SqlCommand(strSql, conn);
        conn.Open();
        SqlDataAdapter sqlAdpt = new SqlDataAdapter();
        sqlAdpt.SelectCommand = sqlComd;
        sqlAdpt.Fill(ds);
        string[] cntName = new string[ds.Tables[0].Rows.Count];
        int i = 0;
        try
        {
            foreach (DataRow rdr in ds.Tables[0].Rows)
            {
                cntName.SetValue(rdr["UnitShort"].ToString(), i);
                i++;
            }
        }
        catch { }
        finally
        {
            conn.Close();

        }
        return cntName;
    }
    
    [WebMethod]
    public string[] GetShipmentPage(string prefixText)
    {
        DataSet ds = new DataSet();
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Constr"].ConnectionString);
        string strSql = "Select Distinct AccountName from View_AccountMaster where   AccountName  LIKE '" + prefixText + "%'";
        SqlCommand sqlComd = new SqlCommand(strSql, conn);
        conn.Open();
        SqlDataAdapter sqlAdpt = new SqlDataAdapter();
        sqlAdpt.SelectCommand = sqlComd;
        sqlAdpt.Fill(ds);
        string[] cntName = new string[ds.Tables[0].Rows.Count];
        int i = 0;
        try
        {
            foreach (DataRow rdr in ds.Tables[0].Rows)
            {
                cntName.SetValue(rdr["AccountName"].ToString(), i);
                i++;
            }
        }
        catch { }
        finally
        {
            conn.Close();

        }
        return cntName;
    }

    [WebMethod]
    public string[] GetShipmentCFS(string prefixText)
    {
        string AccountType = "CFS";
        DataSet ds = new DataSet();
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Constr"].ConnectionString);
        string strSql = "Select Distinct AccountName from View_AccountMaster where   AccountName  LIKE '" + prefixText + "%' and AccountType='" + AccountType + "'";
        SqlCommand sqlComd = new SqlCommand(strSql, conn);
        conn.Open();
        SqlDataAdapter sqlAdpt = new SqlDataAdapter();
        sqlAdpt.SelectCommand = sqlComd;
        sqlAdpt.Fill(ds);
        string[] cntName = new string[ds.Tables[0].Rows.Count];
        int i = 0;
        try
        {
            foreach (DataRow rdr in ds.Tables[0].Rows)
            {
                cntName.SetValue(rdr["AccountName"].ToString(), i);
                i++;
            }
        }
        catch { }
        finally
        {
            conn.Close();

        }
        return cntName;
    }

    [WebMethod]
    public string[] GetShipmentFF(string prefixText)
    {
        string AccountType = "FF";
        DataSet ds = new DataSet();
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Constr"].ConnectionString);
        string strSql = "Select Distinct AccountName from View_AccountMaster where   AccountName  LIKE '" + prefixText + "%' and AccountType='" + AccountType + "'";
        SqlCommand sqlComd = new SqlCommand(strSql, conn);
        conn.Open();
        SqlDataAdapter sqlAdpt = new SqlDataAdapter();
        sqlAdpt.SelectCommand = sqlComd;
        sqlAdpt.Fill(ds);
        string[] cntName = new string[ds.Tables[0].Rows.Count];
        int i = 0;
        try
        {
            foreach (DataRow rdr in ds.Tables[0].Rows)
            {
                cntName.SetValue(rdr["AccountName"].ToString(), i);
                i++;
            }
        }
        catch { }
        finally
        {
            conn.Close();

        }
        return cntName;
    }

    [WebMethod(EnableSession = true)]
    public string[] GetInvoiceNo(string prefixText)
    {
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Constr"].ConnectionString);
        // string strconn = (string)ConfigurationSettings.AppSettings["Connectionefrieght"];
        // SqlConnection conn = new SqlConnection(strconn);
        conn.Open();
        SqlCommand Cmd = new SqlCommand("Select invoice From View_VoucherNo where invoice LIKE '" + prefixText + "%'", conn);
        SqlDataAdapter da = new SqlDataAdapter(Cmd);
        DataSet ds = new DataSet();
        //ds = (DataSet)Session["LinqDataSet"];
        da.Fill(ds);
        string[] PortCode = new string[ds.Tables[0].Rows.Count];
        int i = 0;
        foreach (DataRow rdr in ds.Tables[0].Rows)
        {
            string BCode = rdr["invoice"].ToString();
            PortCode.SetValue(rdr["invoice"].ToString(), i);
            i++;
        }
        conn.Close();
        //Session["BranchCode"] = ds;
        return PortCode;
    }

    [WebMethod(EnableSession = true)]
    public string[] GetCostCenter(string prefixText)
    {
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Constr"].ConnectionString);
        conn.Open();
        SqlCommand Cmd = new SqlCommand("Select CostCenterName From M_CostCenter where CostCenterName LIKE '" + prefixText + "%'", conn);
        SqlDataAdapter da = new SqlDataAdapter(Cmd);
        DataSet ds = new DataSet();
        da.Fill(ds);
        string[] PortCode = new string[ds.Tables[0].Rows.Count];
        int i = 0;
        foreach (DataRow rdr in ds.Tables[0].Rows)
        {
            string BCode = rdr["CostCenterName"].ToString();
            PortCode.SetValue(rdr["CostCenterName"].ToString(), i);
            i++;
        }
        conn.Close();
        return PortCode;
    }
    //Search consignee
    [WebMethod]
    public string[] GetConsignee(string prefixText)
    {
        DataSet ds = new DataSet();
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Constr"].ConnectionString);
        //string strSql = "Select Distinct Search from View_AccountMaster where AccountType = 'Consignor' And  Search LIKE '%" + prefixText + "%'";
        string strSql = "Select Distinct AccountName  from View_AccountMaster where AccountType = 'Consignee' And  AccountName LIKE '%" + prefixText + "%'";
        SqlCommand sqlComd = new SqlCommand(strSql, conn);
        conn.Open();
        SqlDataAdapter sqlAdpt = new SqlDataAdapter();
        sqlAdpt.SelectCommand = sqlComd;
        sqlAdpt.Fill(ds);
        string[] cntName = new string[ds.Tables[0].Rows.Count];
        int i = 0;
        try
        {
            foreach (DataRow rdr in ds.Tables[0].Rows)
            {
                cntName.SetValue(rdr["AccountName"].ToString(), i);
                i++;
            }
        }
        catch { }
        finally
        {
            conn.Close();

        }
        return cntName;
    }
    //Search consignor
    [WebMethod]
    public string[] GetConsignor(string prefixText)
    {
        DataSet ds = new DataSet();
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Constr"].ConnectionString);
        //string strSql = "Select Distinct Search from View_AccountMaster where AccountType = 'Consignor' And  Search LIKE '%" + prefixText + "%'";
        string strSql = "Select Distinct AccountName from View_AccountMaster where AccountType = 'Consignor' And  AccountName LIKE '%" + prefixText + "%'";
        SqlCommand sqlComd = new SqlCommand(strSql, conn);
        conn.Open();
        SqlDataAdapter sqlAdpt = new SqlDataAdapter();
        sqlAdpt.SelectCommand = sqlComd;
        sqlAdpt.Fill(ds);
        string[] cntName = new string[ds.Tables[0].Rows.Count];
        int i = 0;
        try
        {
            foreach (DataRow rdr in ds.Tables[0].Rows)
            {
                cntName.SetValue(rdr["AccountName"].ToString(), i);
                i++;
            }
        }
        catch { }
        finally
        {
            conn.Close();

        }
        return cntName;
    }

    [WebMethod]
    public string[] GetSubParty(string prefixText, string contextKey)
    {
        DataSet ds = new DataSet();
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Constr"].ConnectionString);

        if (prefixText == "")
        {
            prefixText = prefixText;
        }
        else
        {
            prefixText = prefixText.Replace("'", "");
            
        }

        if (contextKey == "")
        {
            contextKey = contextKey;
        }
        else
        {
            contextKey = contextKey.Replace("'", "");

        }


        //string strSql = "Select Distinct Search from View_AccountMaster where AccountType = 'Consignor' And  Search LIKE '%" + prefixText + "%'";
        string strSql = "select Distinct AccountName from M_AccountMaster where AccountName Like '%" + prefixText + "%' and  Acc_Group ='" + contextKey + "' ";
        SqlCommand sqlComd = new SqlCommand(strSql, conn);        
        conn.Open();
        SqlDataAdapter sqlAdpt = new SqlDataAdapter();
        sqlAdpt.SelectCommand = sqlComd;
        sqlAdpt.Fill(ds);
        string[] cntName = new string[ds.Tables[0].Rows.Count];
        int i = 0;
        try
        {
            foreach (DataRow rdr in ds.Tables[0].Rows)
            {
                cntName.SetValue(rdr["AccountName"].ToString(), i);
                i++;
            }
        }
        catch { }
        finally
        {
            conn.Close();

        }
        return cntName;
    }



    [WebMethod]
    public string[] GetParty(string prefixText, string contextKey)
    {
        DataSet ds = new DataSet();
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Constr"].ConnectionString);

        if (prefixText == "")
        {
            prefixText = prefixText;
        }
        else
        {
            prefixText = prefixText.Replace("'", "");

        }
        
        //string strSql = "Select Distinct Search from View_AccountMaster where AccountType = 'Consignor' And  Search LIKE '%" + prefixText + "%'";
        string strSql = "select Distinct AccountName from M_AccountMaster where AccountName Like '%" + prefixText + "%' ";
        SqlCommand sqlComd = new SqlCommand(strSql, conn);
        conn.Open();
        SqlDataAdapter sqlAdpt = new SqlDataAdapter();
        sqlAdpt.SelectCommand = sqlComd;
        sqlAdpt.Fill(ds);
        string[] cntName = new string[ds.Tables[0].Rows.Count];
        int i = 0;
        try
        {
            foreach (DataRow rdr in ds.Tables[0].Rows)
            {
                cntName.SetValue(rdr["AccountName"].ToString(), i);
                i++;
            }
        }
        catch { }
        finally
        {
            conn.Close();

        }
        return cntName;
    }
















    [WebMethod]
    public string[] GetInBondJobNo(string prefixText)
    {
        DataSet ds = new DataSet();
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Constr"].ConnectionString);
        string strSql = "Select Distinct JobNo from T_JobCreation where   BEType='W' ";
        SqlCommand sqlComd = new SqlCommand(strSql, conn);
        conn.Open();
        SqlDataAdapter sqlAdpt = new SqlDataAdapter();
        sqlAdpt.SelectCommand = sqlComd;
        sqlAdpt.Fill(ds);
        string[] cntName = new string[ds.Tables[0].Rows.Count];
        int i = 0;
        try
        {
            foreach (DataRow rdr in ds.Tables[0].Rows)
            {
                cntName.SetValue(rdr["JobNo"].ToString(), i);
                i++;
            }
        }
        catch { }
        finally
        {
            conn.Close();

        }
        return cntName;
    }


    //[WebMethod]
    //public string[] GetMySQLJob(string prefixText)
    //{
    //    DataSet ds = new DataSet();

    //    MySqlConnection conn = new MySqlConnection((string)ConfigurationManager.AppSettings["ConnectionVisual"]);

    //    //string strSql = "Select Distinct Search from View_AccountMaster where AccountType = 'Consignor' And  Search LIKE '%" + prefixText + "%'";
    //    string strSql = "Select Job_no from Iworkreg where WrkBlk ='" + (string)Session["WorkBlk"] + "' ";
    //    MySqlCommand sqlComd = new SqlCommand(strSql, conn);
    //    conn.Open();
    //    MySqlDataAdapter sqlAdpt = new SqlDataAdapter();
    //    sqlAdpt.SelectCommand = sqlComd;
    //    sqlAdpt.Fill(ds);
    //    string[] cntName = new string[ds.Tables[0].Rows.Count];
    //    int i = 0;
    //    try
    //    {
    //        foreach (DataRow rdr in ds.Tables[0].Rows)
    //        {
    //            cntName.SetValue(rdr["Job_no"].ToString(), i);
    //            i++;
    //        }
    //    }
    //    catch { }
    //    finally
    //    {
    //        conn.Close();

    //    }
    //    return cntName;
    //}

}


