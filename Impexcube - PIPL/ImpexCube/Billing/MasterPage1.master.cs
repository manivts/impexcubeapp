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

public partial class MasterPage1 : System.Web.UI.MasterPage
{
    string strPIPL = (string)ConfigurationManager.ConnectionStrings["Constr"].ConnectionString;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack == false)
        {
            
            //lblUser.Text = (string)Session["USER-NAME"];
            ////lblDate.Text = System.DateTime.Now.ToString("dd-MMM-yyyy");
            ////lblTime.Text = DateTime.Now.ToLongTimeString();
            //if (lblUser.Text == "")
            //{
            //    Response.Redirect("~/PIPL.aspx");
            //}

            SqlConnection conn = new SqlConnection(strPIPL);
            string sqlQuery = "select  * from AppDetails";
            SqlDataAdapter da = new SqlDataAdapter(sqlQuery, conn);
            DataSet ds = new DataSet();
            da.Fill(ds, "name");

            if (ds.Tables["name"].Rows.Count != 0)
            {
                DataRowView row = ds.Tables["name"].DefaultView[0];
                lblCompName.Text = row["CompanyName"].ToString();
                //lblshortname1.Text = row["ShortName"].ToString();
                //lblshortname2.Text = row["ShortName"].ToString();
            }

        }
    }
    protected void LB_Logout_Click(object sender, EventArgs e)
    {
        try
        {
            Response.Write("<script>window.close();</script>");
            //FormsAuthentication.SignOut();
            //Session["USER-NAME"] = "";
            //Session.Abandon();
            //Session.Clear();

            //Response.Redirect("~/PIPL.aspx", false);//All one has to do is set the endResponse property of Response.Redirect to be false.
            // avoid for thread abort exception error
        }
        catch (Exception ex)
        {
            Response.Write("<script>alert('" + ex.Message + "')</script>");

        }
    }
  
}
