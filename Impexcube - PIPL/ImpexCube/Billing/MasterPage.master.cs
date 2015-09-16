using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml.Linq;
using System.Data.SqlClient;
using System.Text;

public partial class MasterPage : System.Web.UI.MasterPage
{
   // string strPIPL = (string)ConfigurationManager.AppSettings["ConnectionImpex"];
  
   // string strconvts = (string)ConfigurationManager.ConnectionStrings["VTSConstr"].ConnectionString;
   // string strconn1 = (string)ConfigurationManager.AppSettings["connectionstring"];
    string strImpex = (string)ConfigurationManager.ConnectionStrings["Constr"].ConnectionString;
    private string lfy = "";
    string Currenturl = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        //Page.Header.DataBind(); 
        if (IsPostBack == false)
        {

            //if (Request.QueryString["uName"] != null)
            //{
            //    //string declaration from request.querystring
            //    #region
            //    string Branch = Request.QueryString["Branch"];
            //    string branchshort = string.Empty;
            //    string ename = string.Empty;
            //    string CMP = string.Empty;
            //    string usertype = string.Empty;
            //    string mail = string.Empty;
            //    string EmpId = string.Empty;
            //    string fyear = Request.QueryString["FYear"];
                
            //    if (Request.QueryString["FYear"] != null)
            //    {
            //        fyear = Request.QueryString["FYear"];
            //    }
            //    else
            //    {
            //        fyear = (string)Session["FinYear"];
            //    }
            //    if (Request.QueryString["uName"] != null)
            //    {
            //        ename = Request.QueryString["uName"];
            //    }
            //    else
            //    {
            //        ename = (string)Session["USER-NAME"];
            //    }
            //    if (Request.QueryString["UserType"] != null)
            //    {
            //        usertype = Request.QueryString["UserType"];
            //    }
            //    else
            //    {
            //        usertype = (string)Session["DEPT"];
            //    }
            //    if (Request.QueryString["BranchShortName"] != null)
            //    {
            //        branchshort = Request.QueryString["BranchShortName"];
            //    }
            //    else
            //    {
            //        branchshort = (string)Session["BranchShortName"];
            //    }
            //    if (Request.QueryString["CMP"] != null)
            //    {
            //        CMP = Request.QueryString["CMP"];
            //    }
            //    else
            //    {
            //        CMP = (string)Session["CMP"];
            //    }
            //    if (Request.QueryString["Mail"] != null)
            //    {
            //        mail = Request.QueryString["Mail"];
            //    }
            //    else
            //    {
            //        mail = (string)Session["userMail"];
            //    }
            //    if (Request.QueryString["EmpId"] != null)
            //    {
            //        EmpId = Request.QueryString["EmpId"];
            //    }
            //    else
            //    {
            //        EmpId = (string)Session["EmpId"];
            //    }
            //    if (Request.QueryString["URL"] != null)
            //    {
            //        Currenturl = Request.QueryString["URL"];
            //    }
            //    else
            //    {
            //        Currenturl = (string)Session["presentUrl"];
            //    }
            //    #endregion
            //    // session values declaration
            //    #region
            //    Session["USER-NAME"] = ename;
            //    Session["DEPT"] = usertype;
            //    Session["FinancialYear"] = fyear;
            //    Session["ZONE"] = Branch;
            //    Session["BranchShortName"] = branchshort;
            //    Session["userMail"] = mail;
            //    Session["CMP"] = CMP;
            //    Session["EmpId"] = EmpId;
            //    Session["presentUrl"] = Currenturl;
            //    #endregion
            //}
            //else
            //{
            //    if ((string)Session["USER-NAME"] == null || (string)Session["USER-NAME"] == string.Empty)
            //    {

            //    }
            //}
           // GetUrlPath();
            lblUser.Text = (string)Session["USER-NAME"];
            //lblDate.Text = System.DateTime.Now.ToString("dd-MMM-yyyy");
            //lblTime.Text = DateTime.Now.ToLongTimeString();
            //if (lblUser.Text == "")
            //{
                //string presentUrl = (string)Session["presentUrl"];
                //string URLlink = string.Empty;
                //URLlink = (string)Session["URL"];
                //if (presentUrl == "server-r2")
                //{
                //    Response.Redirect(URLlink + "/PIPL/frmLogin.aspx", false);
                //}
                //else if (presentUrl == "192.168.1.101")
                //{
                //    Response.Redirect(URLlink + "/PIPL/frmLogin.aspx", false);
                //}
                //else if (presentUrl == "122.165.65.50")
                //{
                //    Response.Redirect(URLlink + "/PIPL/frmLogin.aspx", false);
                //}  
            //}
            SqlConnection conn = new SqlConnection(strImpex);
            string sqlQuery = "select  * from AppDetails";
            SqlDataAdapter da = new SqlDataAdapter(sqlQuery, conn);
            DataSet ds = new DataSet();
            da.Fill(ds, "Table");

            if (ds.Tables["Table"].Rows.Count != 0)
            {
                DataRowView row = ds.Tables["Table"].DefaultView[0];
                lblCompName.Text = row["CompanyName"].ToString();
                //lblshortname1.Text = row["ShortName"].ToString();
                //lblshortname2.Text = row["ShortName"].ToString();
            }
            //GetModule();
          
        }
    }

    //private void GetUrlPath()
    //{
    //    SqlConnection conn = new SqlConnection(strconvts);
    //    string query = "Select Path,Link from RequestUrl where link = '" + (string)Session["presentUrl"] + "' ";
    //    SqlDataAdapter da = new SqlDataAdapter(query, conn);
    //    DataSet ds = new DataSet();
    //    da.Fill(ds, "data");
    //    if (ds.Tables["data"].Rows.Count != 0)
    //    {
    //        DataRowView row = ds.Tables["data"].DefaultView[0];
    //        Session["URL"] = row["Path"].ToString();
    //    }
    //}

    //private void GetModule()
    //{
    //    string currentmodule = "PIPL Billing";
    //    SqlConnection conn = new SqlConnection(strconvts);
    //    string sqlQuery = "select distinct Module from UserAccess where UserName = '" + (string)Session["EmpId"] + "' and Branch = '" + (string)Session["Zone"] + "' " +
    //                     " and Module not like '%" + currentmodule + "%' ";
    //    SqlDataAdapter da = new SqlDataAdapter(sqlQuery, conn);
    //    DataSet ds = new DataSet();
    //    da.Fill(ds, "type");
    //    DataTable dt = ds.Tables["type"];
    //   // lnkAccounts.Visible = false;
    //    lnkImpexCube.Visible = false;
    //    lnkHRMS.Visible = false;
    //    if (ds.Tables["type"].Rows.Count != 0)
    //    {
    //        foreach (DataRow rw in dt.Rows)
    //        {
    //            string module = rw["Module"].ToString();

    //            if (module == "HRMS")
    //            {
    //                lnkHRMS.Visible = true;
    //            }
    //            //if (module == "Accounts")
    //            //{
    //            //    lnkAccounts.Visible = true;
    //            //}
    //            if (module == "ImpexCube")
    //            {
    //                lnkImpexCube.Visible = true;
    //            }
    //        }
    //    }
    //}
    protected void LB_Logout_Click(object sender, EventArgs e)
    {

        Response.Redirect("~/HomePage.aspx");
       // string presentUrl = (string)Session["presentUrl"];
       // presentUrl = presentUrl.ToLower();
       // string url = string.Empty;
       //// GetUrlPath();
       // url = (string)Session["URL"];
       // try
       // {
       //     if (presentUrl == "server-r2")
       //     {
       //         Response.Redirect(url + "/PIPL/frmLogin.aspx", false);
       //     }
       //     else if (presentUrl == "192.168.1.101")
       //     {
       //         Response.Redirect(url + "/PIPL/frmLogin.aspx", false);
       //     }
       //     else if (presentUrl == "122.165.65.50")
       //     {
       //         Response.Redirect(url + "/PIPL/frmLogin.aspx", false);
       //     }  
            
       // }
       // catch (Exception ex)
       // {
       //     Response.Write("<script>alert('" + ex.Message + "')</script>");

       // }
    }
    protected void Menu2_MenuItemClick(object sender, MenuEventArgs e)
    {
        MenuItem m1 = this.Menu2.FindItem("Exit");
        // MenuItem m2 = this.Menu2.FindItem("Enquiry OUT");
        MenuItem m3 = this.Menu2.FindItem("Renewal Contract");
        MenuItem m4 = this.Menu2.FindItem("Edit Contract");

        //MenuItem m3 = this.Menu2.FindItem("Out By Rererence");
        switch (e.Item.Value)
        {
            case "Exit":
                Response.Write("<script>window.close();</script>");
                break;
            case "Renewal Contract":
                Session["CONTRACT"] = "Renewal";
                Response.Redirect("~/Billing/frmContractEdit.aspx", false);
                break;
            case "Edit Contract":
                Session["CONTRACT"] = "Edit";
                Response.Redirect("~/Billing/frmContractEdit.aspx", false);
                break;



        }
    }
    protected void lnkImpexCube_Click(object sender, EventArgs e)
    {
        //string presentUrl = (string)Session["presentUrl"];
        //presentUrl = presentUrl.ToLower();
        //string url = string.Empty;
        ////GetUrlPath();
        //url = (string)Session["URL"];
        //string userName = (string)Session["EmpId"];
        //string FYear = (string)Session["FinancialYear"];
        //string fdate = string.Empty;
        //string edate = string.Empty;
        //SqlConnection conn1 = new SqlConnection(strImpex);
        //string sqlQuery1 = "select * from T_LoginYear where Fyear = '" + FYear + "' order by TransId desc";
        //SqlDataAdapter da1 = new SqlDataAdapter(sqlQuery1, conn1);
        //DataSet ds3 = new DataSet();
        //da1.Fill(ds3, "emp");
        //if (ds3.Tables["emp"].Rows.Count != 0)
        //{
        //    DataRowView row = ds3.Tables["emp"].DefaultView[0];
        //    Session["fdate"] = row["Fdate"].ToString();
        //    Session["edate"] = row["Edate"].ToString();
        //    Session["FYear"] = row["Fyear"].ToString();
        //    fdate = Session["fdate"].ToString();
        //    edate = Session["edate"].ToString();
        //}
        //GetFinancialYear(FYear);
        //try
        //{
        //    string zone = (string)Session["ZONE"];
        //    DataSet ds = GetUSERS(userName, zone);
        //    DataTable dt = ds.Tables[0];
        //    if (ds.Tables["INVOICE"].Rows.Count != 0)
        //    {
        //        DataRowView rw = ds.Tables["INVOICE"].DefaultView[0];
        //        Session["USER-NAME"] = rw["employeeName"].ToString();
        //        string UserName = (string)Session["USER-NAME"];
        //        string TYPES = rw["dept"].ToString();
        //        string branchshort = (string)Session["BranchShortName"];
        //        string grade = rw["Grade"].ToString();
        //        string mail = rw["mail"].ToString();
        //        if (grade == "A")
        //        {
        //            if (presentUrl == "server-r2")
        //            {
        //                Response.Redirect(url + "/ImpexCube/frmUserCreation.aspx?ename=" + UserName + "&FYear=" + FYear + "&fdate=" + fdate + "&edate=" + edate + "&Branch=" + zone + "&UserType=" + TYPES + "&BranchShortName=" + branchshort + "&Mail=" + mail + "&Grade=" + grade + "&URL=" + presentUrl + "&EmpId=" + userName, false);
        //            }
        //            else if (presentUrl == "192.168.1.101")
        //            {
        //                Response.Redirect(url + "/ImpexCube/frmUserCreation.aspx?ename=" + UserName + "&FYear=" + FYear + "&fdate=" + fdate + "&edate=" + edate + "&Branch=" + zone + "&UserType=" + TYPES + "&BranchShortName=" + branchshort + "&Mail=" + mail + "&Grade=" + grade + "&URL=" + presentUrl + "&EmpId=" + userName, false);
        //            }
        //            else if (presentUrl == "122.165.65.50")
        //            {
        //                Response.Redirect(url + "/ImpexCube/frmUserCreation.aspx?ename=" + UserName + "&FYear=" + FYear + "&fdate=" + fdate + "&edate=" + edate + "&Branch=" + zone + "&UserType=" + TYPES + "&BranchShortName=" + branchshort + "&Mail=" + mail + "&Grade=" + grade + "&URL=" + presentUrl + "&EmpId=" + userName, false);
        //            }
        //        }
        //        else
        //        {
        //            if (presentUrl == "server-r2")
        //            {
        //                Response.Redirect(url + "/ImpexCube/HomePage.aspx?ename=" + UserName + "&FYear=" + FYear + "&fdate=" + fdate + "&edate=" + edate + "&Branch=" + zone + "&UserType=" + TYPES + "&BranchShortName=" + branchshort + "&Mail=" + mail + "&Grade=" + grade + "&URL=" + presentUrl + "&EmpId=" + userName, false);
        //            }
        //            else if (presentUrl == "192.168.1.101")
        //            {
        //                Response.Redirect(url + "/ImpexCube/HomePage.aspx?ename=" + UserName + "&FYear=" + FYear + "&fdate=" + fdate + "&edate=" + edate + "&Branch=" + zone + "&UserType=" + TYPES + "&BranchShortName=" + branchshort + "&Mail=" + mail + "&Grade=" + grade + "&URL=" + presentUrl + "&EmpId=" + userName, false);
        //            }
        //            else if (presentUrl == "122.165.65.50")
        //            {
        //                Response.Redirect(url + "/ImpexCube/HomePage.aspx?ename=" + UserName + "&FYear=" + FYear + "&fdate=" + fdate + "&edate=" + edate + "&Branch=" + zone + "&UserType=" + TYPES + "&BranchShortName=" + branchshort + "&Mail=" + mail + "&Grade=" + grade + "&URL=" + presentUrl + "&EmpId=" + userName, false);
        //            }
        //        }
        //    }
        //}
        //catch (Exception ex)
        //{
        //    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('" + ex.Message + "');", true);
        //}
    }

    public DataSet GetUSERS(string userName, string zone)
    {
        SqlConnection conn2 = new SqlConnection(strImpex);
        string sqlString = "select * from employee where EmpName='" + userName + "' and Zone='" + zone + "'";
        SqlDataAdapter da2 = new SqlDataAdapter(sqlString, conn2);
        DataSet ds2 = new DataSet();
        da2.Fill(ds2, "INVOICE");
        return ds2;
    }

    private void GetFinancialYear(string FYear)
    {
        SqlConnection conn = new SqlConnection(strImpex);
        string sqlQuery = "select Max(Transid) As TransId from T_LoginYear";
        conn.Open();
        SqlCommand cmd = new SqlCommand(sqlQuery, conn);

        SqlDataReader dr = cmd.ExecuteReader();

        if (dr.Read())
        {
            int lastyear = Convert.ToInt32(dr["TransId"].ToString());
            int lfyear = lastyear - 1;
            conn.Close();
            string qry = "select Fyear from T_LoginYear where TransId=" + lfyear + "";
            //conn.Open();
            conn.Open();
            SqlCommand cmd1 = new SqlCommand(qry, conn);
            SqlDataReader dr1 = cmd1.ExecuteReader();
            if (dr1.Read())
            {
                lfy = dr1["Fyear"].ToString();
                Session["Lfyear"] = lfy;
            }
        }
        Session["FinancialYear"] = FYear;
    }

    protected void lnkHRMS_Click(object sender, EventArgs e)
    {
        //string presentUrl = (string)Session["presentUrl"];
        //presentUrl = presentUrl.ToLower();
        //string url = string.Empty;
        ////GetUrlPath();
        //url = (string)Session["URL"];
        //string Username = (string)Session["EmpId"];
        //string FinYear = (string)Session["FinancialYear"];
        //string BranchName = (string)Session["Zone"];

        //GetBranchId(BranchName);
        //DataSet DsUser = GETEMP(Username, BranchName);

        //if (DsUser.Tables["User"].Rows.Count != 0)
        //{
        //    DataRowView Row = DsUser.Tables["User"].DefaultView[0];
        //    string UNAME = Row["UserName"].ToString();
        //    string ECode = Row["EmpCode"].ToString();
        //    string UserType = Row["UserType"].ToString();
        //    string branchId = (string)Session["branchid"];
        //    if (UserType == "SuperAdmin" || UserType == "Admin" || UserType == "User")
        //    {
        //        if (presentUrl == "server-r2")
        //        {
        //            Response.Redirect(url + "/PIPLHrms/HomePageAdmin.aspx?ename=" + Username + "&usertype=" + UserType + "&FYear=" + FinYear + "&branch=" + BranchName + "&URL=" + presentUrl + "&branchid=" + branchId, false);
        //        }
        //        else if (presentUrl == "192.168.1.101")
        //        {
        //            Response.Redirect(url + "/PIPLHrms/HomePageAdmin.aspx?ename=" + Username + "&usertype=" + UserType + "&FYear=" + FinYear + "&branch=" + BranchName + "&URL=" + presentUrl + "&branchid=" + branchId, false);
        //        }
        //        else if (presentUrl == "122.165.65.50")
        //        {
        //            Response.Redirect(url + "/PIPLHrms/HomePageAdmin.aspx?ename=" + Username + "&usertype=" + UserType + "&FYear=" + FinYear + "&branch=" + BranchName + "&URL=" + presentUrl + "&branchid=" + branchId, false);
        //        }
        //    }
        //    else if (UserType == "Attendance")
        //    {
        //        if (presentUrl == "server-r2")
        //        {
        //            Response.Redirect(url + "/PIPLHrms/frmAttendanceLogin.aspx?ename=" + Username + "&usertype=" + UserType + "&FYear=" + FinYear + "&branch=" + BranchName + "&URL=" + presentUrl + "&branchid=" + branchId, false);
        //        }
        //        else if (presentUrl == "192.168.1.101")
        //        {
        //            Response.Redirect(url + "/PIPLHrms/frmAttendanceLogin.aspx?ename=" + Username + "&usertype=" + UserType + "&FYear=" + FinYear + "&branch=" + BranchName + "&URL=" + presentUrl + "&branchid=" + branchId, false);
        //        }
        //        else if (presentUrl == "122.165.65.50")
        //        {
        //            Response.Redirect(url + "/PIPLHrms/frmAttendanceLogin.aspx?ename=" + Username + "&usertype=" + UserType + "&FYear=" + FinYear + "&branch=" + BranchName + "&URL=" + presentUrl + "&branchid=" + branchId, false);
        //        }
        //    }
        //}
        //else
        //{
        //    if (presentUrl == "server-r2")
        //    {
        //        ScriptManager.RegisterStartupScript(this, this.GetType(), "redirect", "alert('Invalid UserName & Password'); window.location.href='http://" + presentUrl + "/PIPL/frmLogin.aspx';", true);
        //    }
        //    else if (presentUrl == "192.168.1.101")
        //    {
        //        ScriptManager.RegisterStartupScript(this, this.GetType(), "redirect", "alert('Invalid UserName & Password'); window.location.href='http://" + presentUrl + "/PIPL/frmLogin.aspx';", true);
        //    }
        //    else if (presentUrl == "122.165.65.50")
        //    {
        //        ScriptManager.RegisterStartupScript(this, this.GetType(), "redirect", "alert('Invalid UserName & Password'); window.location.href='http://" + presentUrl + "/PIPL/frmLogin.aspx';", true);
        //    } 
        //}
    }

    private void GetBranchId(string BranchName)
    {
        string query = "Select BranchId From Branch_Master where BranchName= '" + BranchName + "' ";
        DataSet ds = GetDataSet(query);
        if (ds.Tables["data"].Rows.Count != 0)
        {
            DataRowView row = ds.Tables["data"].DefaultView[0];
            string branchid = row["BranchId"].ToString();
            Session["branchid"] = branchid;
        }
    }

    public DataSet GetDataSet(string Query)
    {
        DataSet ds = new DataSet();
        try
        {
            SqlConnection sqlConn = new SqlConnection(strImpex);
            sqlConn.Open();
            SqlDataAdapter daa = new SqlDataAdapter(Query, sqlConn);
            daa.Fill(ds, "data");
            sqlConn.Close();
        }
        catch (Exception ex)
        {
            string Msg = ex.Message;
        }
        return ds;
    }

    public DataSet GETEMP(string Username, string BranchName)
    {
        SqlConnection conn = new SqlConnection(strImpex);
        StringBuilder Query = new StringBuilder();
        Query.Append("select * from employee where UserName='" + Username + "' and BranchName='" + BranchName + "' ");
        SqlDataAdapter da = new SqlDataAdapter(Query.ToString(), conn);
        DataSet ds = new DataSet();
        da.Fill(ds, "User");
        return ds;
    }
}
