using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Reporting.Common;
using Microsoft.Reporting.WebForms;
using System.Reflection;

namespace ImpexCube
{
    public partial class frmDebit : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ObjectDataSource1.FilterExpression = "invoice='"+(string)Session["InvNo"]+"'";
                ObjectDataSource1.DataBind();
                ObjectDataSource2.DataBind();
                ReportViewer1.LocalReport.Refresh();
                               
            }
        }
        public void DisableUnwantedExportFormats(LocalReport rvServer)
        {
            RenderingExtension[] extensio = rvServer.ListRenderingExtensions();

            foreach (RenderingExtension extension in rvServer.ListRenderingExtensions())
            {
                if (extension.Name == "XML" || extension.Name == "WORD" || extension.Name == "MHTML" || extension.Name == "EXCEL" || extension.Name == "Excel" || extension.Name == "CSV")
                {
                    ReflectivelySetVisibilityFalse(extension);
                }
            }
        }


        private void ReflectivelySetVisibilityFalse(RenderingExtension extension)
        {
            FieldInfo info = extension.GetType().GetField("m_isVisible", BindingFlags.NonPublic | BindingFlags.Instance);


            if (info != null)
            {
                info.SetValue(extension, false);
            }
        }

        protected void ReportViewer1_PreRender(object sender, EventArgs e)
        {
            DisableUnwantedExportFormats(ReportViewer1.LocalReport);
        }


    }
}