using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Web.UI;

//namespace TLPL
//{
    public class ClassMsg
    {
        public static void Show(string message)
        {
            Page page = HttpContext.Current.Handler as Page;

            if (page != null)
            {
                message = message.Replace("'", "\\");
               //ClientScriptManager .RegisterStartupScript(page, page.GetType(), "err_msg", "alert('" + message + "');", true);
                ScriptManager.RegisterStartupScript(page, page.GetType(), "err_msg", "alert('" + message + "');", true);
            }
           
        }
    }
//}