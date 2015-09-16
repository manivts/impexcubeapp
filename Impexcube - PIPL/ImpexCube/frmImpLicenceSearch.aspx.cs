using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ImpexCube
{
    public partial class frmImpLicenceSearch : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                var pagename = (Label) Master.FindControl("lblName");
                pagename.Text = "Licence Search";
            }
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            Response.Redirect("frmImpLicsenceMaster.aspx");
        }
    }
}