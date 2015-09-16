using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Net;
using System.Net.Mail;
using System.IO;

using System.Data.SqlClient;
using VTS.ImpexCube.Data;

namespace ImpexCube
{
    public partial class frmUserCreation : System.Web.UI.Page
    {
        string strconn = (string)ConfigurationManager.ConnectionStrings["Constr"].ConnectionString;
        //string strconn1 = (string)ConfigurationManager.AppSettings["ConnectionAdmin"];

        string EID = "";

        VTS.ImpexCube.Business.userAuthorityBL ba = new VTS.ImpexCube.Business.userAuthorityBL();
        CommonDL objCommonDL = new CommonDL();

        #region
        private string mTo;

        private string mFrom;
        private string mSubject;


        private string mMsg;

        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack == false)
            {
                string name = Request.QueryString["eName"];
                string uName = (string)Session["USER-NAME"];

                DropDownBind();
                    

                GrdUser.DataSource = ba.GetData(uName);
                GrdUser.DataBind();

                foreach (GridViewRow Row in GrdUser.Rows)
                {
                    string ID = Row.Cells[2].Text;
                    if (ID == "A")
                    {
                        Row.Cells[5].Enabled = false;
                    }
                }


                drBranch.DataSource = ba.GetBranch();
                drBranch.DataTextField = "city";
                drBranch.DataValueField = "city";
                drBranch.DataBind();
                drBranch.Items.Insert(0, new ListItem("~select~", "-1"));
                drGrade.Items.Insert(0, new ListItem("~select~", "-1"));
                drGrade.Items.Insert(1, new ListItem("A"));
                drGrade.Items.Insert(2, new ListItem("B"));
                drGrade.Items.Insert(3, new ListItem("C"));
                //drGrade.Items.Insert(4, new ListItem("D"));
            }
            plUser.Visible = false;
            PLNewUser.Visible = false;
            MouseOverOption();
        }

        private void DropDownBind()
        {
            string MenuQuery = "select Distinct GroupName from M_FormName ";
            DataSet dsMenu = new DataSet();
            dsMenu = objCommonDL.GetDataSet(MenuQuery);
            
            if (dsMenu.Tables["Table"].Rows.Count != 0)
            {
                ddlMenu.DataSource = dsMenu;
                ddlMenu.DataTextField = "GroupName";
                ddlMenu.DataValueField = "GroupName";
                ddlMenu.DataBind();
            }
        }

        protected void MouseOverOption()
        {
            lkUser.Attributes.Add("onmouseover", "this.style.color = '#ff6699'");
            lkUser.Attributes.Add("onmouseout", "this.style.color = '#2461bf'");
          
            lkNewUser.Attributes.Add("onmouseover", "this.style.color = '#ff6699'");
            lkNewUser.Attributes.Add("onmouseout", "this.style.color = '#2461bf'");
            lkExit.Attributes.Add("onmouseover", "this.style.color = '#ff6699'");
            lkExit.Attributes.Add("onmouseout", "this.style.color = '#2461bf'");         
        }

        protected void lkUser_Click(object sender, EventArgs e)
        {
            GrdUser.Visible = true;
            plUser.Visible = false;
            PLNewUser.Visible = false;
        }
        protected void GrdUser_SelectedIndexChanged(object sender, EventArgs e)
        {
            plUser.Visible = true;
            GrdUser.Visible = false;
            string UD = Convert.ToString(GrdUser.SelectedDataKey.Value);
            try
            {
                DataSet dsEmp = new DataSet();
                dsEmp = ba.GetEMP(UD); // Variable UD to pass to ba

                DataRowView row = dsEmp.Tables["emp"].DefaultView[0];

                string EName = row["empname"].ToString();
                string EID = row["UID"].ToString();
                string Branch = row["Zone"].ToString();

                lblEName.Text = EName;
                lblZone.Text = Branch;

                Session["Auth-ENAME"] = EName;
                Session["Auth-EID"] = EID;
                Session["Auth-Branch"] = Branch;

               
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('" + ex.Message + "');", true);
            }


        }
        public DataSet GetForms(string CMP, string eid, string Menu)
        {
            string sqlQuery;
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Constr"].ConnectionString);

            sqlQuery = "select * from M_user where empid='" + eid + "' and formID='"+Menu+"' ";
            //sqlQuery = "select * from M_user where empid='" + eid + "' and formID !='" + F1 + "' ";

            SqlDataAdapter da = new SqlDataAdapter(sqlQuery, conn);
            DataSet ds = new DataSet();
            da.Fill(ds, "Forms");
            return ds;
        }
        protected void BtnSubmit_Click(object sender, EventArgs e)
        {
            string formName;
            string formID;
            string EID = (string)Session["Auth-EID"];
            string ename = (string)Session["Auth-ENAME"];
            string Branch = (string)Session["Auth-Branch"];
            string Status = string.Empty;
            string Access = string.Empty;
            string Menu = ddlMenu.SelectedValue;
            string sqlQuery = string.Empty;
            try
            {

                foreach (GridViewRow row in GrdForms.Rows)
                {
                    formID = GrdForms.DataKeys[row.RowIndex].Value.ToString();
                    formName = row.Cells[2].Text;
                    System.Web.UI.WebControls.CheckBox chkDis = (System.Web.UI.WebControls.CheckBox)row.FindControl("chkDisable");
                    
                    if (chkDis.Checked)
                    {
                         Status = "1";
                         Access = "1";
                    }
                    else
                    {
                        Status = "0";
                        Access = "0";
                    }

                    SqlConnection conn = new SqlConnection(strconn);
                    DataSet ds = new DataSet();
                    //To Check Whether The Form Already Exits Or Not
                    string checkformname = "Select empid,formid,formname from M_User where empid='" + EID + "' and formid='" + formID + "' and formname='" + formName + "'";
                    SqlDataAdapter da1 = new SqlDataAdapter(checkformname, conn);
                    da1.Fill(ds, "emp");

                    if (ds.Tables["emp"].Rows.Count == 0)
                    {
                        sqlQuery = "Insert into M_user(formid,formName,empName,empid,branch,Status,Access)" +
                                      " values('" + formID + "','" + formName + "','" + ename + "','" + EID + "','" + Branch + "','" + Status + "','" + Access + "')";
                    }
                    else if (ds.Tables["emp"].Rows.Count != 0)
                    {
                        sqlQuery = "update M_user SET Status='" + Status + "' ,Access='" + Access + "' where EmpId='" + EID + "' and  FormName='" + formName + "' and formid='" + formID + "' ";

                    }
                    conn.Open();
                    SqlDataAdapter da = new SqlDataAdapter();
                    SqlCommand cmd = new SqlCommand(sqlQuery, conn);
                    cmd.CommandText = sqlQuery;
                    cmd.Connection = conn;
                    da.SelectCommand = cmd;
                    int result = cmd.ExecuteNonQuery();
                    conn.Close();
                }
                GrdUser.Visible = true;
                plUser.Visible = false;
                PLNewUser.Visible = false;
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('" + ex.Message + "');", true);
            }
        }
        protected void createUserAuthority(string formID, string formName, string ename, string EID, string Branch, string DIS, string READ)
        {
            SqlConnection conn = new SqlConnection(ConfigurationManager.AppSettings["ConnectionImpex"]);

            string sqlQuery = "Insert into M_user(formid,formName,empName,empid,branch,disable,ReadOnly)" +
                              " values('" + formID + "','" + formName + "','" + ename + "','" + EID + "','" + Branch + "','" + DIS + "','" + READ + "')";
            try
            {
                conn.Open();
                SqlDataAdapter da = new SqlDataAdapter();
                SqlCommand cmd = new SqlCommand(sqlQuery, conn);
                cmd.CommandText = sqlQuery;
                cmd.Connection = conn;
                da.SelectCommand = cmd;

                int result = cmd.ExecuteNonQuery();
                conn.Close();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('" + ex.Message + "');", true);
            }
        }

        protected void GrdUser_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            string uName = (string)Session["USER-NAME"];
            try
            {
                GrdUser.DataSource = ba.GetData(uName);
                GrdUser.DataBind();
                GrdUser.PageIndex = e.NewPageIndex;
                GrdUser.DataBind();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('" + ex.Message + "');", true);
            }
        }
        protected void GrdUser_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                Int32 UD = Convert.ToInt32(GrdUser.DataKeys[e.RowIndex].Value);
                //Delete user 
                DataSet ds = new DataSet();
                ds = ba.GetUserAuth(UD); // variable UD to pass to ba

                DataRowView row = ds.Tables["empN"].DefaultView[0];
                string ename = row["empName"].ToString();
                int delID = ba.deleteUserAuth(ename); // variable ename to pass to ba
                int intUID = ba.deleteUser(UD);// Variable UD to pass to ba
                string uName = (string)Session["USER-NAME"];
                GrdUser.DataSource = ba.GetData(uName);
                GrdUser.DataBind();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('" + ex.Message + "');", true);
            }
        }
        protected void lkNewUser_Click(object sender, EventArgs e)
        {
            plUser.Visible = false;
            PLNewUser.Visible = true;
            GrdUser.Visible = false;
            txtName.Text = "";
            txtmail.Text = "";
            drBranch.Text = "-1";
            drGrade.Text = "-1";
        }
        protected void BtnNew_Click(object sender, EventArgs e)
        {
            string strZone = drBranch.SelectedValue;
            string strGrade = drGrade.SelectedValue;
            string EmpName = txtEmpname.Text;
            string strDept = drDept.SelectedValue;
            string strName = txtName.Text;
            string strMail = txtmail.Text;
            string strPass;

            if (strZone == "-1")
            {
                Response.Write("<script>alert('Please select User Zone')</script>");
                PLNewUser.Visible = true;
            }
            else
            {
                if (strGrade == "-1")
                {
                    Response.Write("<script>alert('Please select User Grade')</script>");
                    PLNewUser.Visible = true;
                }
                else
                {
                    //Int32 i = 7;
                    //string pwd = RandomPassword.CreateRandomPassword(i);
                    string pwd = "12345";
                   // string val = pwd;
                   // string pass = EcnryptionTest.base64Encode(val);
                    strPass = pwd ;
                    Session["NAME"] = strName;
                    Session["MAIL"] = strMail;
                    Session["NEWPASS"] = pwd;

                    // Create New User
                    try
                    {
                        int Us = ba.createNewUser(strPass, strName, strZone, strGrade, strMail, EmpName);

                        string uName = (string)Session["USER-NAME"];
                        GrdUser.DataSource = ba.GetData(uName);
                        GrdUser.DataBind();
                        //SendMail();
                        GrdUser.Visible = true;
                        plUser.Visible = false;
                        PLNewUser.Visible = false;

            //Insert All Forms Set Access and Status To False.

                        SqlConnection conn1 = new SqlConnection(strconn);
                        string sqlQuery1 = "select Distinct FormCode,FormName from M_FormName ";
                        SqlDataAdapter da1 = new SqlDataAdapter(sqlQuery1, conn1);
                        DataSet ds3 = new DataSet();
                        da1.Fill(ds3, "FormName");

                        //Get User Name Anu UID 

                        SqlConnection conn2 = new SqlConnection(strconn);
                        string sqlQuery2 = "select UID, EmpName, employeeName, Zone, Grade FROM EMPLOYEE WHERE EmpName='" + EmpName + "' AND  employeeName='" +strName + "' AND ZONE='" + strZone+ "' AND Grade='"+strGrade+"' ";
                        SqlDataAdapter da2 = new SqlDataAdapter(sqlQuery2, conn2);
                        DataSet ds4 = new DataSet();
                        da2.Fill(ds4, "EMPNAME");
                        string EmpUID  = string.Empty;
                        string EmpUName = string.Empty;
                        string EmpBranch = string.Empty;
                        if (ds4.Tables["EMPNAME"].Rows.Count != 0)
                        {
                            DataRowView row = ds4.Tables["EMPNAME"].DefaultView[0];
                            EmpUID = row["UID"].ToString();
                            EmpUName = row["EmpName"].ToString();
                            EmpBranch = row["Zone"].ToString();
                        }
                        
                        DataTable dt = ds3.Tables["FormName"];
                        dt.Columns.Add();
                        dt.Columns.Add();
                        dt.Columns.Add();
                        dt.Columns[2].ColumnName = "EmpId";
                        dt.Columns[3].ColumnName = "EmpName";
                        dt.Columns[4].ColumnName = "Branch";
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            dt.Rows[i]["EmpId"] = EmpUID;
                            dt.Rows[i]["EmpName"] = EmpUName;
                            dt.Rows[i]["Branch"] = EmpBranch;
                        }
                        dt.AcceptChanges();
                        int j=0;
                        foreach (DataRow row in dt.Rows)
                        {
                            string NFormId = dt.Rows[j]["FormCode"].ToString();
                            string NFormName =dt.Rows[j]["FormName"].ToString();
                            string NEmpId = dt.Rows[j]["EmpId"].ToString();
                            string NEmpName = dt.Rows[j]["EmpName"].ToString();
                            string NEmpBranch = dt.Rows[j]["Branch"].ToString();
                            string Status = "0";
                            string Access = "0";

                            SqlConnection conn = new SqlConnection(strconn);

                            string sqlQuery = "Insert into M_user(formid,formName,empName,empid,branch,Status,Access)" +
                                              " values('" + NFormId + "','" + NFormName + "','" + NEmpName + "','" + NEmpId + "','" + NEmpBranch + "','" + Status + "','" + Access + "')";
                            try
                            {
                                conn.Open();
                                SqlDataAdapter da = new SqlDataAdapter();
                                SqlCommand cmd = new SqlCommand(sqlQuery, conn);
                                cmd.CommandText = sqlQuery;
                                cmd.Connection = conn;
                                da.SelectCommand = cmd;

                                int result = cmd.ExecuteNonQuery();
                                conn.Close();
                            }
                            catch (Exception ex)
                            {
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('" + ex.Message + "');", true);
                            }
                            j++;
                        }
                        
                        //Create new user for Billing...
                     
                        getDEPT(strName, strDept);
                    }
                    catch (Exception ex)
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('" + ex.Message + "');", true);
                    }
                }
            }
        }
        protected void getDEPT(string uName, string dept)
        {

            string Query = "update employee set dept='" + dept + "' where empName='" + uName + "'";
            SqlConnection conn = new SqlConnection(strconn);
            conn.Open();
            SqlCommand cmd = new SqlCommand(Query, conn);
            cmd.CommandText = Query;
            cmd.Connection = conn;
            int res = cmd.ExecuteNonQuery();
            conn.Close();

        }
        protected void GetUserBilling()
        {
            GetEmpID();
            string username = txtName.Text;
            string password = username.Substring(0, 4);
            string Dept = drDept.SelectedValue;
            EID = "P" + EID;
            SqlConnection conn = new SqlConnection(strconn);
            SqlCommand command = new SqlCommand("InsertUser", conn);
            command.CommandType = CommandType.StoredProcedure;
            command.Parameters.Add("@ID", SqlDbType.VarChar).Value = EID;
            command.Parameters.Add("@Username", SqlDbType.VarChar).Value = username;
            command.Parameters.Add("@Password", SqlDbType.VarChar).Value = password;
            command.Parameters.Add("@Dept", SqlDbType.VarChar).Value = Dept;

            conn.Open();
            int rows = command.ExecuteNonQuery();
            conn.Close();
            if (rows == 1)
            {
                //Response.Write("<script>alert('New User has been created successfully...') </script>");
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('New User has been created successfully...');", true);
                txtName.Text = "";
             
                drDept.SelectedValue = "0";
            }
            else
            {
                //Response.Write("New USer has been Created Failed..");
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('New User Creation has been Failed..');", true);
            }
        }
        protected void GetEmpID()
        {
            SqlConnection conn = new SqlConnection(strconn);
            string Query = "select max(substring(empid,2,4))+1 as nos from iec_emp";
            SqlDataAdapter da = new SqlDataAdapter(Query, conn);
            DataSet ds = new DataSet();
            da.Fill(ds, "emp");
            DataRowView row = ds.Tables["emp"].DefaultView[0];
            EID = row["nos"].ToString();

        }
     
        protected void SendMail()
        {
            mTo = Session["MAIL"].ToString();
            mTo.Trim();


            mFrom = "admin@impexcube.in";
            mFrom.Trim();
            string CreateDate = System.DateTime.Now.ToString("dd/MM/yyyy");

            mSubject = "Re: Welcome To ImpexCube System";
            mMsg += "Dear " + (string)Session["NAME"] + "," + "\n\n";

            mMsg += "      We have created login ID & password for ImpexCube System..." + "\n";
            mMsg += "      User Name :" + (string)Session["NAME"] + "\n";
            mMsg += "      Password  :" + (string)Session["NEWPASS"] + "\n\n";
        //    mMsg += " link is internet: http://www.pigroup.in/impexcube/" + "\n\n";
            mMsg += " link is intranet: http://server-r2/impexcube/" + "\n\n";

            mMsg += " Thank you," + "\n";
            mMsg += " ADMIN " + "\n";


            if (mFrom == "" || mTo == "")
            {
             
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Give the Correct E-Mail Address');", true);
            }
            else
            {
                try
                {
                    MailMessage message = new MailMessage();
                    message.From = new MailAddress(mFrom);
                  
                    message.Subject = mSubject;
                    message.Body = mMsg;
                  

                    if (mTo != "" | mTo != string.Empty)
                    {
                        string[] strTo = mTo.Split(';');
                        foreach (string strThisTo in strTo)
                        {
                            strThisTo.Trim();
                            message.To.Add(strThisTo);
                        }
                    }

                    SmtpClient mySmtpClient = new SmtpClient("smtpauth.translink.in", 587);
                    mySmtpClient.Credentials = new System.Net.NetworkCredential("mktg01@translink.in", "Bala123");
                    mySmtpClient.Send(message);

                }
                catch (FormatException ex)
                {
                    Response.Write("<script>alert('Format Exception: " + ex.Message + "')</script>");
                 
                }
                catch (SmtpException ex)
                {
                    Response.Write("<script>alert('SMTP Exception: " + ex.Message + "')</script>");

                  
                }
                catch (Exception ex)
                {
                    Response.Write("<script>alert('General Exception: " + ex.Message + "')</script>");
                

                }
                finally
                {
                  
                }

            }

        }

        protected void BtnSUB_Exit_Click(object sender, EventArgs e)
        {
            GrdUser.Visible = true;
            plUser.Visible = false;
            PLNewUser.Visible = false;
        }
        protected void BtnNew_Exit_Click(object sender, EventArgs e)
        {
            GrdUser.Visible = true;
            plUser.Visible = false;
            PLNewUser.Visible = false;
        }


        protected void lkExit_Click(object sender, EventArgs e)
        {
            Response.Write("<script>window.close();</script>");
        }
        protected void GrdForms_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

            }
        }
        protected void lkImporter_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/PIPL/Jobs/frmUserRegister.aspx");

        }
        protected void GrdUser_RowUpdating(object sender, GridViewUpdateEventArgs e)
        {
            Int32 i = 7;
            string pwd = RandomPassword.CreateRandomPassword(i);
            string strPass = "";
            string val = pwd;
            string pass = EcnryptionTest.base64Encode(val);
            strPass = pwd + pass;

            Int32 UD = Convert.ToInt32(GrdUser.DataKeys[e.RowIndex].Value);
            //Delete user 
            DataSet ds = new DataSet();
            ds = ba.GetUserAuth(UD); // variable UD to pass to ba

            DataRowView row = ds.Tables["empN"].DefaultView[0];

            string ename = row["empName"].ToString();
            string mail = row["mail"].ToString();

            int res = ba.updataPassword(UD, ename, strPass);
            SendMailResetPassword(mail, ename, pwd);


        }
        protected void SendMailResetPassword(string uTo, string uName, string uPass)
        {
            mTo = uTo;
            mTo.Trim();


            mFrom = "admin@impexcube.in";
            mFrom.Trim();
            string CreateDate = System.DateTime.Now.ToString("dd/MM/yyyy");

            mSubject = "Re: Welcome To ImpexCube System";
            mMsg += "Dear " + uName + "," + "\n\n";

            mMsg += "      We have Reset your password for ImpexCube System..." + "\n";
            mMsg += "      User Name :" + uName + "\n";
            mMsg += "      Password  :" + uPass + "\n\n";
            mMsg += " link is internet: http://www.pigroup.in/impexcube/" + "\n\n";
           // mMsg += " link is intranet: http://192.168.1.50:8081/pimpex" + "\n";

            mMsg += " Thank you," + "\n";
            mMsg += " ADMIN " + "\n";


            if (mFrom == "" || mTo == "")
            {
                Response.Write("<script>alert('Give the Correct E-Mail Address') </script>");
            }
            else
            {
                try
                {
                    MailMessage message = new MailMessage();
                    message.From = new MailAddress(mFrom);
                  
                    message.Subject = mSubject;
                    message.Body = mMsg;
                   

                    if (mTo != "" | mTo != string.Empty)
                    {
                        string[] strTo = mTo.Split(';');
                        foreach (string strThisTo in strTo)
                        {
                            strThisTo.Trim();
                            message.To.Add(strThisTo);
                        }
                    }

                    SmtpClient mySmtpClient = new SmtpClient("smtpauth.translink.in", 587);
                    mySmtpClient.Credentials = new System.Net.NetworkCredential("mktg01@translink.in", "Bala123");
                    mySmtpClient.Send(message);
                }
                catch (FormatException ex)
                {
                    Response.Write("<script>alert('Format Exception: " + ex.Message + "')</script>");
                   
                }
                catch (SmtpException ex)
                {
                    Response.Write("<script>alert('SMTP Exception: " + ex.Message + "')</script>");

                  
                }
                catch (Exception ex)
                {
                    Response.Write("<script>alert('General Exception: " + ex.Message + "')</script>");
                   

                }
                finally
                {
                    
                }

            }

        }

        protected void ddlMenu_SelectedIndexChanged(object sender, EventArgs e)
        {
            GrdForms.DataBind();
            string EID = (string)Session["Auth-EID"];
            //  string F1 = "JSU";
            string CMP = (string)Session["COMP"];
            string Menu = ddlMenu.SelectedValue;

            GrdForms.DataSource = GetMForms(Menu);
            GrdForms.DataBind();

            foreach (GridViewRow row1 in GrdForms.Rows)
            {
                System.Web.UI.WebControls.CheckBox chkDis = (System.Web.UI.WebControls.CheckBox)row1.FindControl("chkDisable");
               // System.Web.UI.WebControls.CheckBox chkRead = (System.Web.UI.WebControls.CheckBox)row1.FindControl("chkRead");
                string formCode = GrdForms.DataKeys[row1.RowIndex].Value.ToString();

                DataSet dsUser = new DataSet();
                dsUser = ba.GetUser(EID, formCode);

                if (dsUser.Tables["Forms"].Rows.Count != 0)
                {
                    DataRowView row2 = dsUser.Tables["Forms"].DefaultView[0];

                    string Status = row2["Status"].ToString();
                    string Access = row2["Access"].ToString();
                    if (Status == "True")
                    {
                        chkDis.Checked = true;
                    }
                    else
                    {
                        chkDis.Checked = false;
                    }
                    if (Access == "1")
                    {
                        chkDis.Checked = true;
                    }
                    else
                    {
                        chkDis.Checked = false;
                    }
                }
            }
            plUser.Visible = true;
        }

        public DataSet GetMForms(string Menu)
        {
            string sqlQuery;
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Constr"].ConnectionString);
            sqlQuery = "select formid,formcode,formname from M_FormName where groupname='" + Menu + "'" ;
            //sqlQuery = "select * from M_user where empid='" + eid + "' and formID !='" + F1 + "' ";

            SqlDataAdapter da = new SqlDataAdapter(sqlQuery, conn);
            DataSet ds = new DataSet();
            da.Fill(ds, "Forms");
            return ds;
        }
    }
}