using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ImpexCube
{
    public partial class frmSessionClose : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void linkbtnLogin_Click(object sender, EventArgs e)
        {
            Response.Redirect("frmLogin.aspx");
            //Response.Redirect("~/frmLogin.aspx");
        }
    }
}