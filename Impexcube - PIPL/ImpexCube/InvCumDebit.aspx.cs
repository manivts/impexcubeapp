﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ImpexCube
{
    public partial class InvCumDebit : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ObjectDataSource1.FilterExpression = "invoice='" + (string)Session["InvNo"] + "'";//" +(string)Session["InvNo"]+ "
                ObjectDataSource1.DataBind();
                ObjectDataSource2.DataBind();
                ReportViewer1.LocalReport.Refresh();
            }
        }
    }
}