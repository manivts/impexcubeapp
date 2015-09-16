using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Text;
using System.Web.Security;
using VTS.ImpexCube.Utlities;

namespace VTS.ImpexCube.Web
{
    public partial class HomeMaster : System.Web.UI.MasterPage
    {
        VTS.ImpexCube.Utlities.Utility joblog = new VTS.ImpexCube.Utlities.Utility();
        string strconvts = ""; //(string)ConfigurationManager.ConnectionStrings["VTSConstr"].ConnectionString;
        string strconn =""; //(string)ConfigurationManager.ConnectionStrings["AccConstr"].ConnectionString;
        string strconn1 = (string)ConfigurationManager.AppSettings["connectionstring"];
        string strPIPL = (string)ConfigurationManager.ConnectionStrings["Constr"].ConnectionString;

        string UserType = string.Empty;
        string name = string.Empty;
        string fyear = string.Empty;
        string fdate = string.Empty;
        string edate = string.Empty;
        string branch = string.Empty;
        string company = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                FormTest();
                if (Request.QueryString["ename"] != null)
                {
                    //string declaration from request.querystring
                    #region
                    if (Request.QueryString["ename"] != null)
                    {
                        name = Request.QueryString["ename"];
                    }
                    else
                    {
                        name = (string)Session["USER-NAME"];
                    }
                    if (Request.QueryString["FYear"] != null)
                    {
                        fyear = Request.QueryString["FYear"];
                    }
                    else
                    {
                        fyear = (string)Session["FinancialYear"];
                    }
                    if (Request.QueryString["fdate"] != null)
                    {
                        fdate = Request.QueryString["fdate"];
                    }
                    else
                    {
                        fdate = (string)Session["fdate"];
                    }
                    if (Request.QueryString["edate"] != null)
                    {
                        edate = Request.QueryString["edate"];
                    }
                    else
                    {
                        edate = (string)Session["edate"];
                    }
                    if (Request.QueryString["Branch"] != null)
                    {
                        branch = Request.QueryString["Branch"];
                    }
                    else
                    {
                        branch = (string)Session["BranchCode"];
                    }
                    if (Request.QueryString["UserType"] != null)
                    {
                        UserType = Request.QueryString["UserType"];
                    }
                    else
                    {
                        UserType = (string)Session["UserType"];
                    }
                    if (Request.QueryString["Company"] != null)
                    {
                        company = Request.QueryString["Company"];
                    }
                    else
                    {
                        company = (string)Session["Company"];
                    }
                    #endregion
                    // session values declaration
                    #region
                    Session["USER-NAME"] = name;
                    Session["FinancialYear"] = fyear;
                    Session["fdate"] = fdate;
                    Session["edate"] = edate;
                    Session["BranchCode"] = branch;
                    Session["UserType"] = UserType;
                    Session["Company"] = company;
                    #endregion
                }
                
            }
            if ((string)Session["UID"] != null)
            {
                GetUserPermission();
            }
            else
            {
                Response.Redirect("frmLogin.aspx");
            }
           
        }
        public void GetUserPermission()
        {
            try
            {
                SqlConnection conn = new SqlConnection(strPIPL);
                string qry = "Exec UserPermission @columns = '0',@convert = '0',@EmpId='" + (string)Session["UID"] + "'";
                SqlCommand cmd = new SqlCommand(qry, conn);
                cmd.CommandType = CommandType.StoredProcedure;
                conn.Open();
                SqlDataAdapter da = new SqlDataAdapter(qry, conn);
                DataSet ds = new DataSet();
                da.Fill(ds, "SQLTABLE");
                conn.Close();
                if (ds.Tables["SQLTABLE"].Rows.Count != 0)
                {
                    DataRowView dr = ds.Tables["SQLTABLE"].DefaultView[0];
                    //RepJobStatusStage.Visible = Convert.ToBoolean(Convert.ToInt16(dr["RepJobStatusStage"].ToString()));
                    IMPImport.Visible = Convert.ToBoolean(Convert.ToInt16(dr["IMPImport"].ToString()));
                    EXPExport.Visible = Convert.ToBoolean(Convert.ToInt16(dr["EXPExport"].ToString()));
                    CRM.Visible = Convert.ToBoolean(Convert.ToInt16(dr["CRM"].ToString()));
                    MASMaster.Visible = Convert.ToBoolean(Convert.ToInt16(dr["MASMaster"].ToString()));
                    FundTransfer.Visible = Convert.ToBoolean(Convert.ToInt16(dr["FundTransfer"].ToString()));
                    Accounts.Visible = Convert.ToBoolean(Convert.ToInt16(dr["Accounts"].ToString()));
                    Reports.Visible = Convert.ToBoolean(Convert.ToInt16(dr["Reports"].ToString()));
                    Billing.Visible = Convert.ToBoolean(Convert.ToInt16(dr["Billing"].ToString()));
                    Utilities.Visible = Convert.ToBoolean(Convert.ToInt16(dr["Utilities"].ToString()));

                    //User Restrictions
                    //Import
                    IMPJobCreation.Visible = Convert.ToBoolean(Convert.ToInt16(dr["IMPJobCreation"].ToString()));
                    IMPShipment.Visible = Convert.ToBoolean(Convert.ToInt16(dr["IMPShipment"].ToString()));
                    IMPInvoice.Visible = Convert.ToBoolean(Convert.ToInt16(dr["IMPInvoice"].ToString()));
                    IMPProduct.Visible = Convert.ToBoolean(Convert.ToInt16(dr["IMPProduct"].ToString()));
                    IMPCheckList.Visible = Convert.ToBoolean(Convert.ToInt16(dr["IMPCheckList"].ToString()));
                    IMPBEFile.Visible = Convert.ToBoolean(Convert.ToInt16(dr["IMPBEFile"].ToString()));
                    IMPFileUpload.Visible = Convert.ToBoolean(Convert.ToInt16(dr["IMPFileUpload"].ToString()));
                    IMPStageList.Visible = Convert.ToBoolean(Convert.ToInt16(dr["IMPStageList"].ToString()));
                    IMPDocUpload.Visible = Convert.ToBoolean(Convert.ToInt16(dr["IMPDocUpload"].ToString()));
                    IMPJobSearch.Visible = Convert.ToBoolean(Convert.ToInt16(dr["IMPJobSearch"].ToString()));

                    //Export
                    EXPJobCreation.Visible = Convert.ToBoolean(Convert.ToInt16(dr["EXPJobCreation"].ToString()));
                    EXPShipment.Visible = Convert.ToBoolean(Convert.ToInt16(dr["EXPShipment"].ToString()));
                    EXPInvoice.Visible = Convert.ToBoolean(Convert.ToInt16(dr["EXPInvoice"].ToString()));
                    EXPProduct.Visible = Convert.ToBoolean(Convert.ToInt16(dr["EXPProduct"].ToString()));
                    EXPCheckList.Visible = Convert.ToBoolean(Convert.ToInt16(dr["EXPCheckList"].ToString()));
                    EXPSBFile.Visible = Convert.ToBoolean(Convert.ToInt16(dr["EXPSBFile"].ToString()));
                    EXPFileUpload.Visible = Convert.ToBoolean(Convert.ToInt16(dr["EXPFileUpload"].ToString()));

                    ////CRM
                    CRMEnq.Visible = Convert.ToBoolean(Convert.ToInt16(dr["CRMEnq"].ToString()));
                    CRMStandard.Visible = Convert.ToBoolean(Convert.ToInt16(dr["CRMStandard"].ToString()));
                    CRMQuote.Visible = Convert.ToBoolean(Convert.ToInt16(dr["CRMQuote"].ToString()));

                    ////Master
                    MASAirLine.Visible = Convert.ToBoolean(Convert.ToInt16(dr["MASAirline"].ToString()));
                    MASBank.Visible = Convert.ToBoolean(Convert.ToInt16(dr["MASBank"].ToString()));
                    MASCFS.Visible = Convert.ToBoolean(Convert.ToInt16(dr["MASCFS"].ToString()));
                    MASConType.Visible = Convert.ToBoolean(Convert.ToInt16(dr["MASConType"].ToString()));
                    MASCountry.Visible = Convert.ToBoolean(Convert.ToInt16(dr["MASCountry"].ToString()));
                    MASCurrency.Visible = Convert.ToBoolean(Convert.ToInt16(dr["MASCurrency"].ToString()));
                    MASCustom.Visible = Convert.ToBoolean(Convert.ToInt16(dr["MASCustom"].ToString()));
                    MASCustomer.Visible = Convert.ToBoolean(Convert.ToInt16(dr["MASCustomer"].ToString()));
                    MASConsignor.Visible = Convert.ToBoolean(Convert.ToInt16(dr["MASConsignor"].ToString()));
                    MASDocument.Visible = Convert.ToBoolean(Convert.ToInt16(dr["MASDocument"].ToString()));
                    MASFF.Visible = Convert.ToBoolean(Convert.ToInt16(dr["MASFF"].ToString()));
                    MASPort.Visible = Convert.ToBoolean(Convert.ToInt16(dr["MASPort"].ToString()));
                    MASProduct.Visible = Convert.ToBoolean(Convert.ToInt16(dr["MASProduct"].ToString()));

                    MASStage.Visible = Convert.ToBoolean(Convert.ToInt16(dr["MASStage"].ToString()));
                    MASStatus.Visible = Convert.ToBoolean(Convert.ToInt16(dr["MASStatus"].ToString()));
                    MASShipping.Visible = Convert.ToBoolean(Convert.ToInt16(dr["MASShipping"].ToString()));
                    MASUser.Visible = Convert.ToBoolean(Convert.ToInt16(dr["MASUser"].ToString()));
                    MASUOM.Visible = Convert.ToBoolean(Convert.ToInt16(dr["MASUOM"].ToString()));
                    MASAuth.Visible = Convert.ToBoolean(Convert.ToInt16(dr["MASAuth"].ToString()));
                    MASAirLine.Visible = Convert.ToBoolean(Convert.ToInt16(dr["MASVessel"].ToString()));
                    MASLicence.Visible = Convert.ToBoolean(Convert.ToInt16(dr["MASLicence"].ToString()));
                    MASSVBRef.Visible = Convert.ToBoolean(Convert.ToInt16(dr["MASSVBRef"].ToString()));
                    MASSAPTA.Visible = Convert.ToBoolean(Convert.ToInt16(dr["MASSAPTA"].ToString()));
                    MASUNOTN.Visible = Convert.ToBoolean(Convert.ToInt16(dr["MASUNOTN"].ToString()));

                    //Fund Request
                    FUNReq.Visible = Convert.ToBoolean(Convert.ToInt16(dr["FUNReq"].ToString()));
                    FUNAppr.Visible = Convert.ToBoolean(Convert.ToInt16(dr["FUNAppr"].ToString()));
                    FUNAccount.Visible = Convert.ToBoolean(Convert.ToInt16(dr["FUNAccount"].ToString()));

                    //Reports
                    REPJobStatusReport.Visible = Convert.ToBoolean(Convert.ToInt16(dr["REPJobStatusReport"].ToString()));
                    REPJobStageHistory.Visible = Convert.ToBoolean(Convert.ToInt16(dr["REPJobStageHistory"].ToString()));
                    REPGIT.Visible = Convert.ToBoolean(Convert.ToInt16(dr["REPGIT"].ToString()));
                    REPGKNDSR.Visible = Convert.ToBoolean(Convert.ToInt16(dr["REPGKNDSR"].ToString()));
                    REPUserReport.Visible = Convert.ToBoolean(Convert.ToInt16(dr["REPUserReport"].ToString()));
                    REPDSR.Visible = Convert.ToBoolean(Convert.ToInt16(dr["REPDSR"].ToString()));
                    REPAgeing.Visible = Convert.ToBoolean(Convert.ToInt16(dr["REPAgeing"].ToString()));
                    REPUserTemp.Visible = Convert.ToBoolean(Convert.ToInt16(dr["REPUserTemp"].ToString()));
                    REPInvoiceTemp.Visible = Convert.ToBoolean(Convert.ToInt16(dr["REPInvoiceTemp"].ToString()));
                    REPInvoice.Visible = Convert.ToBoolean(Convert.ToInt16(dr["REPInvoice"].ToString()));
                    REPConntractWise.Visible = Convert.ToBoolean(Convert.ToInt16(dr["REPConntractWise"].ToString()));
                    REPInvDelStatus.Visible = Convert.ToBoolean(Convert.ToInt16(dr["REPInvDelStatus"].ToString()));
                    REPSEWCsv.Visible = Convert.ToBoolean(Convert.ToInt16(dr["REPSEWCsv"].ToString()));
                    REPSEWDSR.Visible = Convert.ToBoolean(Convert.ToInt16(dr["REPSEWDSR"].ToString()));

                    //Utilities
                    UserCreation.Visible = Convert.ToBoolean(Convert.ToInt16(dr["UserCreation"].ToString()));
                    CSVLoad.Visible = Convert.ToBoolean(Convert.ToInt16(dr["CSVLoad"].ToString()));
                    ProductLoad.Visible = Convert.ToBoolean(Convert.ToInt16(dr["ProductLoad"].ToString()));
                    JobSync.Visible = Convert.ToBoolean(Convert.ToInt16(dr["JobSync"].ToString()));
                    ExpJobSync.Visible = Convert.ToBoolean(Convert.ToInt16(dr["ExpJobSync"].ToString()));
                    DevGuide.Visible = Convert.ToBoolean(Convert.ToInt16(dr["DevGuide"].ToString()));
                    DevGudReport.Visible = Convert.ToBoolean(Convert.ToInt16(dr["DevGudReport"].ToString()));
                    AccessDb.Visible = Convert.ToBoolean(Convert.ToInt16(dr["AccessDb"].ToString()));

                    //Billing
                    Billing.Visible = Convert.ToBoolean(Convert.ToInt16(dr["Billing"].ToString()));

                    //FrontOffice              
                    FrontOffice.Visible = Convert.ToBoolean(Convert.ToInt16(dr["FrontOffice"].ToString()));
                    DocumentMaster.Visible = Convert.ToBoolean(Convert.ToInt16(dr["DocumentMaster"].ToString()));
                    Outward.Visible = Convert.ToBoolean(Convert.ToInt16(dr["Outward"].ToString()));
                    OutwardBill.Visible = Convert.ToBoolean(Convert.ToInt16(dr["OutwardBill"].ToString()));
                    OutwardUpdate.Visible = Convert.ToBoolean(Convert.ToInt16(dr["OutwardUpdate"].ToString()));
                    OutwardDetails.Visible = Convert.ToBoolean(Convert.ToInt16(dr["OutwardDetails"].ToString()));
                    Inward.Visible = Convert.ToBoolean(Convert.ToInt16(dr["Inward"].ToString()));
                    InwardUpdate.Visible = Convert.ToBoolean(Convert.ToInt16(dr["InwardUpdate"].ToString()));
                    InwardDetails.Visible = Convert.ToBoolean(Convert.ToInt16(dr["InwardDetails"].ToString()));
                }
            }

            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('DateBase Error :  " + ex.Message + " ');", true);
            }
            //finally
            //{
            //    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('DateBase Error :  Manikandan');", true);
            //}
            }


        public void FormTest()
        {
            try
            {
                SqlConnection conn1 = new SqlConnection(strPIPL);
                string sqlQuery1 = "select Distinct FormCode,FormName from M_FormName ";
                SqlDataAdapter da1 = new SqlDataAdapter(sqlQuery1, conn1);
                DataSet ds3 = new DataSet();
                da1.Fill(ds3, "FormName");

                //Get User Name Anu UID 
                string EmpName = (string)Session["User-Name"];
                string UID = (string)Session["UID"];
                string Zone = (string)Session["ZONE"];

                DataTable dt = ds3.Tables["FormName"];
                dt.Columns.Add();
                dt.Columns.Add();
                dt.Columns.Add();
                dt.Columns[2].ColumnName = "EmpId";
                dt.Columns[3].ColumnName = "EmpName";
                dt.Columns[4].ColumnName = "Branch";
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    dt.Rows[i]["EmpId"] = UID;
                    dt.Rows[i]["EmpName"] = EmpName;
                    dt.Rows[i]["Branch"] = Zone;
                }
                dt.AcceptChanges();
                int j = 0;
                //Access Datatable to check whether the form is present or not.
                foreach (DataRow row in dt.Rows)
                {
                    string NEmpId = dt.Rows[j]["EmpId"].ToString();
                    string NEmpName = dt.Rows[j]["EmpName"].ToString();
                    string NFormId = dt.Rows[j]["FormCode"].ToString();
                    string NFormName = dt.Rows[j]["FormName"].ToString();
                    string NEmpBranch = dt.Rows[j]["Branch"].ToString();
                    string Status = "0";
                    string Access = "0";

                    SqlConnection conn = new SqlConnection(strPIPL);
                    DataSet ds = new DataSet();
                    string sqlQuery = string.Empty;
                    //To Check Whether The Form Already Exits Or Not
                    string checkformname = "Select empid,EmpName,formid,formname,branch from M_User where EmpName='" + NEmpName + "' and empid='" + NEmpId + "' and formid='" + NFormId + "' and formname='" + NFormName + "' and branch ='" + NEmpBranch + "' ";
                    SqlDataAdapter da2 = new SqlDataAdapter(checkformname, conn);
                    da2.Fill(ds, "emp");
                    if (ds.Tables["emp"].Rows.Count == 0)
                    {
                        sqlQuery = "Insert into M_user(formid,formName,empName,empid,branch,Status,Access)" +
                                      " values('" + NFormId + "','" + NFormName + "','" + NEmpName + "','" + NEmpId + "','" + NEmpBranch + "','" + Status + "','" + Access + "')";
                        conn.Open();
                        SqlDataAdapter da = new SqlDataAdapter();
                        SqlCommand cmd = new SqlCommand(sqlQuery, conn);
                        cmd.CommandText = sqlQuery;
                        cmd.Connection = conn;
                        da.SelectCommand = cmd;
                        int result = cmd.ExecuteNonQuery();
                        conn.Close();
                    }
                    j++;
                }
            }
            catch (Exception ex)
            {

            }
        }
            
        private void GetModule()
        {
            string currentmodule = "ImpexCube";
            SqlConnection conn = new SqlConnection(strconvts);
            string sqlQuery = "select distinct Module from UserAccess where UserName = '" + (string)Session["USER-NAME"] + "' and Branch = '" + (string)Session["BranchCode"] + "' " +
                             " and Module not like '%" + currentmodule + "%' ";
            SqlDataAdapter da = new SqlDataAdapter(sqlQuery, conn);
            DataSet ds = new DataSet();
            da.Fill(ds, "type");
            DataTable dt = ds.Tables["type"];
        }

        protected void lnkBilling_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Billing/index.aspx");
        }

       

        public DataSet GETEMP(string Username, string BranchName)
        {
            SqlConnection conn = new SqlConnection(strconn1);
            StringBuilder Query = new StringBuilder();
            Query.Append("select * from UserAccess where userName='" + Username + "'");

            if (Username.ToLower() != "admin" && Username.ToLower() != "superadmin")
            {
                Query.Append(" and Branch='" + BranchName + "'");
            }
            SqlDataAdapter da = new SqlDataAdapter(Query.ToString(), conn);
            DataSet ds = new DataSet();
            da.Fill(ds, "User");
            return ds;
        }

        private void GetCompanyName()
        {
            try
            {
                string qry = "Select CompanyName from CompanyMaster where CompanyCode='" + (string)Session["CompanyCode"] + "'";
                SqlConnection conn = new SqlConnection(strconn);
                conn.Open();
                SqlDataAdapter da = new SqlDataAdapter(qry, conn);
                DataSet ds = new DataSet();
                da.Fill(ds, "Company");
                conn.Close();
                if (ds.Tables["Company"].Rows.Count != 0)
                {
                    DataRowView row = ds.Tables["Company"].DefaultView[0];
                    Session["CompanyName"] = row["CompanyName"].ToString();
                }
            }
            catch (Exception)
            {
            }
        }

        protected void LinkButtonSignOut_Click(object sender, EventArgs e)
        {
            string query = "";
            string date = DateTime.Now.ToString();
            SqlConnection con = new SqlConnection(strPIPL);
            con.Open();
            query = "update M_ImpexCubeLog set OutTime='" + date + "',Login='" + false + "' where UserName ='" + (string)Session["USER-NAME"] + "'  ";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.ExecuteNonQuery();
            con.Close();
            joblog.UpdateJobLog((string)Session["USER-NAME"]);
            Response.Redirect("frmLogin.aspx", false);
        }

        public void BranchName()
        {
            SqlConnection conn = new SqlConnection(strconn);
            conn.Open();
            string sqlQuery = "select * from BranchMaster where BranchName='" + (string)Session["BranchCode"] + "'";
            SqlDataAdapter da = new SqlDataAdapter(sqlQuery, conn);
            DataSet ds = new DataSet();
            da.Fill(ds, "branch");
            conn.Close();
            if (ds.Tables["branch"].Rows.Count != 0)
            {
                DataRowView row = ds.Tables["branch"].DefaultView[0];
                Session["BranchShortName"] = row["BranchCode"].ToString();
            }
        }

        protected void lnkChangePassword_Click(object sender, EventArgs e)
        {
            Response.Redirect("ChangePassword.aspx");
        }

        protected void btnJob_Click(object sender, ImageClickEventArgs e)
        {
          
            Response.Redirect("frmJobCreation.aspx?Name=Job Creation");                           
        }

        protected void btnShipment_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("frmShipment.aspx?Mode=Direct");
           
        }

        protected void btnInvoice_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("frmInvoiceDetails.aspx?Mode=Direct");
           
        }

        protected void btnProduct_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("frmProductMainPage.aspx?Mode=Direct");
          
        }

        protected void btnCheckList_Click(object sender, ImageClickEventArgs e)
        {
            Response.Redirect("frmPrintCheckList.aspx");
        }

        protected void btnJobSearch_Click(object sender, ImageClickEventArgs e)
        {

        }

    }
}