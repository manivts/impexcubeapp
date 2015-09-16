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

public partial class index : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack == false)
        {
            //lblUser.Text = (string)Session["USER-NAME"];
            //lblDate.Text = System.DateTime.Now.ToString("dd-MMM-yyyy");
            //lblTime.Text = DateTime.Now.ToLongTimeString();
            //if (lblUser.Text == "")
            //{
            //    Response.Redirect("~/PIPL.aspx");
            //}
        }
    }
    

}
