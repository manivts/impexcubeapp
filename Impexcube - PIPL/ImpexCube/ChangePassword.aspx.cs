using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;

namespace ImpexCube
{
    public partial class ChangePassword : System.Web.UI.Page
    {
        string strconn = ""; //(string)ConfigurationManager.ConnectionStrings["VTSConstr"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (IsPostBack == false)
                {
                    plMessage.Visible = false;
                    txtNewpwd.Text = string.Empty;
                    txtCNpwd.Text = string.Empty;
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('DataBase Error: " + ex.Message + "');", true);
            }
        }

        protected void BtnChangepwd_Click(object sender, EventArgs e)
        {
            try
            {
                string userName = (string)Session["UserName"];
                string NewPwd = txtCNpwd.Text;

                SqlConnection conn = new SqlConnection(strconn);
                conn.Open();
                string sqlString = "select UserName from UserAccess where UserName='" + userName + "'  ";
                SqlDataAdapter da = new SqlDataAdapter(sqlString, conn);
                DataSet dsUser = new DataSet();
                da.Fill(dsUser, "Login");
                conn.Close();
                if (dsUser.Tables["Login"].Rows.Count == 0)
                {
                    lblError.Text = "Incorrect UserName";
                }
                else
                {
                    try
                    {
                        DataRowView row = dsUser.Tables["Login"].DefaultView[0];
                        int res = updataPassword(NewPwd, userName);
                        if (res == 1)
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "redirect", "alert('Updated Successfully'); window.location.href='http://192.168.1.51/PIPL/frmLogin.aspx';", true);
                        }
                    }
                    catch (Exception ex)
                    {
                        Response.Write("<script>alert('" + ex.Message + "')</script>");
                    }
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('DataBase Error: " + ex.Message + "');", true);
            }
        }

        private int updataPassword(string NewPwd, string userName)
        {
            SqlConnection conn = new SqlConnection(strconn);
            string sqlQuery = " update UserAccess set Password='" + NewPwd + "'" +
                               " where UserName='" + userName + "'  ";
            conn.Open();
            SqlDataAdapter da = new SqlDataAdapter();
            SqlCommand cmd = new SqlCommand(sqlQuery, conn);
            cmd.CommandText = sqlQuery;
            cmd.Connection = conn;
            da.SelectCommand = cmd;
            int result = cmd.ExecuteNonQuery();
            conn.Close();
            return result;
        }

        protected void BtnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("HomePage.aspx");
        }
    }
}