using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;


namespace VTS.ImpexCube.Web
{
    public class Global : System.Web.HttpApplication
    {

        protected void Application_Start(object sender, EventArgs e)
        {
            //Application["OnlineCounter"] = 0;
            //Application["USERNAME"] = "";
            Application["JobNo"] = "";
        }

        protected void Session_Start(object sender, EventArgs e)
        {
            Session["JobNo"] = "";
            //if (Application["OnlineCounter"] != null)
            //{
            //    Application.Lock();
            //    Application["OnlineCounter"] = ((int)(Application["OnlineCounter"])) + 1;
            //    Application.UnLock();
            //}
        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {
            //if (Application["UserName"] == Application["UserName"])
            //{
            //    Response.Redirect("~/frmLogin.aspx");
            //}
            //if (HttpContext.Current.Session["USER-NAME"] == null)
            //{
            //    HttpContext.Current.Response.Redirect("~/frmLogin.aspx");
            //}
        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {

        }
        protected void Session_End(object sender, EventArgs e)
        {
            // if ((string)Session["UID"]==null)
           if ((string)Session["USER-NAME"] == "")
            {
                Response.Redirect("frmLogin.aspx");
            }
          //  Session["JobNo"] = "";
          //  string appjobno=(string)Application["JobNo"];
          //  Application["JobNo"] = (appjobno.Replace((string)Session["JobNo"], ""));
            //if (Application["OnlineCounter"] != null)
            //{
            //    Application.Lock();
            //    Application["OnlineCounter"] = ((int)Application["OnlineCounter"]) - 1;
            //    Application.UnLock();
            //}
            //if ((string)Session["USER-NAME"] == null)
            //{
            //    Response.Redirect("~/frmSessionClose.aspx");
            //}
        }

        protected void Application_End(object sender, EventArgs e)
        {
           
        }
    }
}