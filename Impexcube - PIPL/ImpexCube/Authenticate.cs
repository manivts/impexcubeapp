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
using System.Data.Sql;
using System.Data.SqlClient;
using System.Web.SessionState;


namespace ImpexCube
{
    public class Authenticate
    {

        public static void Forms(string FID)
        {
            try
            {
                SqlConnection conn = new SqlConnection((string)ConfigurationManager.ConnectionStrings["Constr"].ConnectionString);
                string ENAME = System.Web.HttpContext.Current.Session["UserType"].ToString();
                string branch = System.Web.HttpContext.Current.Session["BranchCode"].ToString();
                string sqlquery = "select * from UserAuthorizationForms where UserRole='" + ENAME + "' and FormName='" + FID + "' and BranchName= '" + branch + "' ";

                SqlDataAdapter da = new SqlDataAdapter(sqlquery, conn);
                DataSet ds = new DataSet();
                da.Fill(ds, "port");
                if (ds.Tables["port"].Rows.Count == 0)
                {
                    System.Web.HttpContext.Current.Session["PnotF"] = "NOTFOUND";
                    System.Web.HttpContext.Current.Response.Write("<script>alert('You have not Authorized for this Page')</script>");
                }
                else
                {
                    DataRowView row = ds.Tables["port"].DefaultView[0];
                    string read = row["Read"].ToString();
                    string write = row["Write"].ToString();
                    string apd = row["Approval"].ToString();
                    System.Web.HttpContext.Current.Session["DISABLE"] = write;
                    System.Web.HttpContext.Current.Session["ROnly"] = read;
                }
            }
            catch (Exception ex)
            {
                
                System.Web.HttpContext.Current.Session["PnotF"] = ex.Message;
                
            }

        }
        public static void FormDash(string FID)
        {

            SqlConnection conn = new SqlConnection((string)ConfigurationManager.ConnectionStrings["Constr"].ConnectionString);
            string ENAME = System.Web.HttpContext.Current.Session["UserType"].ToString();
            string branch = System.Web.HttpContext.Current.Session["BranchCode"].ToString();
            string sqlquery = "select * from UserAuthorizationForms where UserRole='" + ENAME + "' and FormName='" + FID + "' and BranchName= '" + branch + "' ";

            SqlDataAdapter da = new SqlDataAdapter(sqlquery, conn);
            DataSet ds = new DataSet();
            da.Fill(ds, "port");
            if (ds.Tables["port"].Rows.Count == 0)
            {
                System.Web.HttpContext.Current.Session["PnotF"] = "NOTFOUND";
                System.Web.HttpContext.Current.Response.Redirect("~/Dashboard/frmDashboardMain.aspx");
                
            }
            else
            {
                DataRowView row = ds.Tables["port"].DefaultView[0];
                string Dis = row["Read"].ToString();
                string ROnly = row["Write"].ToString();
                System.Web.HttpContext.Current.Session["DISABLE"] = Dis;
                System.Web.HttpContext.Current.Session["ROnly"] = ROnly;

            }

        }
    }
}