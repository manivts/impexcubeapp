using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Collections;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.OleDb;
using System.Drawing;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Net;
using VTS.ImpexCube.Utlities;

namespace ImpexCube
{
    public partial class frmLogin : System.Web.UI.Page
    {
        VTS.ImpexCube.Utlities.Utility joblog = new VTS.ImpexCube.Utlities.Utility();
       // string strconn = (string)ConfigurationManager.AppSettings["ConnectionAdmin"];
        string strPIPL = (string)ConfigurationManager.ConnectionStrings["Constr"].ConnectionString;

        private string userName;
        private string pwd;
        private string lfy = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack == false)
            {
                DateTime sDate = System.DateTime.Now;
                drFinancial.DataSource = GetYear();
                drFinancial.DataTextField = "Fyear";
                drFinancial.DataValueField = "Fyear";
                drFinancial.DataBind();
                // drFinancial.Items.Insert(0, new ListItem("--Select--", "-1"));
                GetCurrentFinancialYear();               

                drBranch.DataSource = GetZone();
                drBranch.DataTextField = "zone";
                drBranch.DataValueField = "zone";
                drBranch.DataBind();
                //drBranch.Items.Insert(0, new ListItem("--Select--", "-1"));

                txtUser.Focus();


                SqlConnection conn = new SqlConnection(strPIPL);
                string sqlQuery = "select  * from T_AppDetails";
                SqlDataAdapter da = new SqlDataAdapter(sqlQuery, conn);
                DataSet ds = new DataSet();
                da.Fill(ds, "name");

                if (ds.Tables["name"].Rows.Count != 0)
                {
                    DataRowView row = ds.Tables["name"].DefaultView[0];
                    lblShortName1.Text = row["ShortName"].ToString();
                    lblShortName.Text = row["ShortName"].ToString();
                    drBranch.SelectedValue = row["Branch"].ToString();
                    Session["companyname"] = row["CompanyName"].ToString();
                    Session["ShortName"] = row["ShortName"].ToString();
                    lblshortname2.Text = row["ShortName"].ToString();
                    Session["CHAlicence"] = row["CHAlicence"].ToString();
                }
            }
        }

        private void GetCurrentFinancialYear()
        {
            int CurrentYear = DateTime.Today.Year;
            int PreviousYear = DateTime.Today.Year - 1;
            int NextYear = DateTime.Today.Year + 1;
            string PreYear = PreviousYear.ToString();
            string NexYear = NextYear.ToString();
            string CurYear = CurrentYear.ToString();
            string FinYear = null;

            if (DateTime.Today.Month > 3)
                FinYear = CurYear + "-" + NexYear;
            else
                FinYear = PreYear + "-" + CurYear;
            
            Session["CurrentFinancial"] = FinYear.Trim(); 
        }

        public DataSet GetUSERS(string userName, string zone)
        {
            SqlConnection conn2 = new SqlConnection(strPIPL);
            string sqlString = "select * from employee where empName='" + userName + "' and zone='" + zone + "'";
            SqlDataAdapter da2 = new SqlDataAdapter(sqlString, conn2);
            DataSet ds2 = new DataSet();
            da2.Fill(ds2, "INVOICE");
            return ds2;
        }

        public DataSet GetZone()
        {
            SqlConnection conn = new SqlConnection(strPIPL);
            string sqlQuery = "select distinct zone from employee";
            SqlDataAdapter da = new SqlDataAdapter(sqlQuery, conn);
            DataSet ds = new DataSet();
            da.Fill(ds, "emp");
            return ds;

        }

        public DataSet GetYear()
        {
            SqlConnection conn = new SqlConnection(strPIPL);
            string sqlQuery = "select Fyear from T_LoginYear order by Transid desc";
            SqlDataAdapter da = new SqlDataAdapter(sqlQuery, conn);
            DataSet ds = new DataSet();
            da.Fill(ds, "emp");
            return ds;
        }

        public void BranchName()
        {
            SqlConnection conn = new SqlConnection(strPIPL);
            conn.Open();
            string sqlQuery = "select * from Branch where Branch='" + drBranch.SelectedValue + "'";
            SqlDataAdapter da = new SqlDataAdapter(sqlQuery, conn);
            DataSet ds = new DataSet();
            da.Fill(ds, "branch");
            conn.Close();
            if (ds.Tables["branch"].Rows.Count != 0)
            {
                DataRowView row = ds.Tables["branch"].DefaultView[0];
                Session["BranchShortName"] = row["BranchShortName"].ToString();
            }
        }

        protected void BtnSubmit_Click(object sender, EventArgs e)
        {
            userName = txtUser.Text;
            pwd = txtPassword.Text;
            string zone = drBranch.SelectedValue;
            string dPass = "";
            string FYear = drFinancial.SelectedValue;
            SqlConnection conn1 = new SqlConnection(strPIPL);
            string sqlQuery1 = "select * from T_LoginYear where Fyear = '" + drFinancial.SelectedValue + "' order by Transid desc";
            SqlDataAdapter da1 = new SqlDataAdapter(sqlQuery1, conn1);
            DataSet ds3 = new DataSet();
            da1.Fill(ds3, "emp");
            if (ds3.Tables["emp"].Rows.Count != 0)
            {
                DataRowView row = ds3.Tables["emp"].DefaultView[0];
                Session["fdate"] = row["Fdate"].ToString();
                Session["edate"] = row["Edate"].ToString();
                Session["FYear"] = row["Fyear"].ToString();
            }
            GetFinancialYear(FYear);
            BranchName();
            try
            {

                DataSet ds = GetUSERS(userName, zone);
                DataTable dt = ds.Tables[0];
                foreach (DataRow row in dt.Rows)
                {
                    dPass = row["empid"].ToString();
                }
                string val = dPass;
                
                //if (val.Length > 7)
                //{
                //    val = val.Remove(0, 7);
                //    dPass = EcnryptionTest.base64Decode(val);                    
                //}

                if (pwd != dPass)
                {
                    txtUser.Text = "";
                    txtPassword.Text = "";

                    Response.Write("<script>alert('Invalid User Name & Password')</script>");

                }
                else
                {
                    Session["CMP"] = "PIPL";
                    Session["USER-NAME"] = txtUser.Text;
                    Session["ZONE"] = drBranch.SelectedValue;

                    userName = userName.ToUpper();
                    DataRowView row = ds.Tables["INVOICE"].DefaultView[0];
                    string TYPES = row["Dept"].ToString();
                    string mail = row["mail"].ToString();
                    string grade = row["Grade"].ToString();
                    string UID = row["UID"].ToString();
                    Session["Grade"] = grade;
                    Session["userMail"] = mail;
                    Session["DEPT"] = TYPES;
                    Session["UID"] = UID;
                    Session["Company"] = "Professional Impex Pvt. Ltd";
                  
                   // string usname=Application["USERNAME"].ToString();
                   
                  string date = DateTime.Now.ToString();
                    SqlConnection con = new SqlConnection(strPIPL);
                    con.Open();
                    string selectquery = "select * from M_ImpexCubeLog where UserName='" + txtUser.Text + "' and Login='" + true + "' ";
                    SqlDataAdapter da = new SqlDataAdapter(selectquery, con);
                    DataSet ds1 = new DataSet();
                    da.Fill(ds1, "login");
                    if (ds1.Tables["login"].Rows.Count != 0)
                    {
                        DataRowView row1 = ds1.Tables["login"].DefaultView[0];
                        string sysname = row1["SystemName"].ToString();
                        lblLoginDetails.Text = "User Name '" + txtUser.Text + "' has been already Logged in system '" + sysname + "'. Do you want re login? ";
                        Panel2.Visible = false;
                        Panel5.Visible = true;
                    }
                    else
                    {
                        //CheckLogin();
                        LogDetails();

                        //if (usname.Contains((string)Session["USER-NAME"]))
                        //{
                        //    Response.Write("<script>alert('Already login the user')</script>");
                        //}
                        //else
                        //{
                        // Application["USERNAME"] = (string)Application["USERNAME"] +" - "+ (string)Session["USER-NAME"];
                        if (grade == "A")
                            // Response.Redirect("frmUserCreation.aspx", false);
                            Response.Redirect("HomePage.aspx", false);
                        else
                        {
                            if (TYPES == "Accounts")
                                Response.Redirect("HomePage.aspx", false);
                            else
                                Response.Redirect("HomePage.aspx", false);
                        }

                        // }
                        //Response.Write("<script>window.open('index.aspx', 'popup0','width=900, height=600, menubar=no, scrollbars=no, toolbar=no, location=no, resizable=no, left=0');</script>");
                    }
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('" + ex.Message + "');", true);
            }
        }

        public void LogDetails()
        {
            string query = "";
            string SystemName = System.Environment.MachineName;
            string clientIPAddress = System.Net.Dns.GetHostAddresses(SystemName).GetValue(0).ToString();
            string ip = HttpContext.Current.Request.UserHostAddress;
            string date = DateTime.Now.ToString();
            SqlConnection con = new SqlConnection(strPIPL);
            con.Open();
            query = "insert into M_ImpexCubeLog (UserID,UserName,InTime,Login,SystemName,SystemIPAddress) values "+
                " ('" + txtUser.Text + "','" + txtUser.Text + "','" + date + "','" + true + "','" + SystemName + "','" + clientIPAddress + "')";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.ExecuteNonQuery();
            con.Close();
        }

        public void CheckLogin()
        {
            string query = "";
            string date = DateTime.Now.ToString();
            SqlConnection con = new SqlConnection(strPIPL);
            con.Open();
            string selectquery = "select * from M_ImpexCubeLog where UserName='" + txtUser.Text + "' and Login='" + true + "' ";
            SqlDataAdapter da = new SqlDataAdapter(selectquery, con);
            DataSet ds = new DataSet();
            da.Fill(ds, "login");
            if (ds.Tables["login"].Rows.Count != 0)
            {
                DataRowView row = ds.Tables["login"].DefaultView[0];
                string sysname= row["SystemName"].ToString();
                lblLoginDetails.Text = "User Name "+txtUser.Text+" has been already Logged in system "+sysname+". Do you want re login? ";
              
                 //query = "update M_ImpexCubeLog set OutTime='" + date + "',Login='" + false + "' where UserName ='" + txtUser.Text + "'  ";
                 //SqlCommand cmd = new SqlCommand(query, con);
                 //cmd.ExecuteNonQuery();
                 //con.Close();
            }
           
        }

        protected void GetFinancialYear(string FYear)
        {
            SqlConnection conn = new SqlConnection(strPIPL);
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

        protected void BtnExit_Click(object sender, EventArgs e)
        {
            Response.Write("<script>window.close();</script>");
        }

        protected void lbChangePassword_Click(object sender, EventArgs e)
        {
            Response.Write("<script>window.open('frmChangePassword.aspx', '_blank','width=510,height=300, menubar=no, scrollbars=no, toolbar=no, location=no, resizable=no, left=400, top=280');</script>");
        }

        protected void btnrelogin_Click(object sender, EventArgs e)
        {
            string query = "";
            string date = DateTime.Now.ToString();
            SqlConnection con = new SqlConnection(strPIPL);
            con.Open();
            query = "update M_ImpexCubeLog set OutTime='" + date + "',Login='" + false + "' where UserName ='" + txtUser.Text + "'  ";
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.ExecuteNonQuery();
            con.Close();
            LogDetails();
            joblog.UpdateJobLog(txtUser.Text);
            Response.Redirect("HomePage.aspx", false);
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("frmLogin.aspx");
        }

    }
}