using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ImpexCube
{
    public partial class frmQuotereport : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ObjectDataSource1.FilterExpression = "EnqId='" + (string)Session["enqid"] + "' or EnqId='" + (string)Session["EnqId"] + "'";
                ObjectDataSource1.DataBind();
                ReportViewer1.LocalReport.Refresh();
            }
        }
    }
}