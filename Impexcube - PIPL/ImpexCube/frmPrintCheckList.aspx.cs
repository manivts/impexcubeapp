using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using Microsoft.Reporting.Common;
using Microsoft.Reporting.WebForms;
using System.Reflection;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using VTS.ImpexCube.Business;

namespace ImpexCube
{
    public partial class frmPrintCheckList : System.Web.UI.Page
    {
        string strcon = (string)ConfigurationManager.ConnectionStrings["Constr"].ConnectionString;
        VTS.ImpexCube.Business.CommonBL obj1 = new VTS.ImpexCube.Business.CommonBL();
        VTS.ImpexCube.Business.JobCreationBAL obj2 = new VTS.ImpexCube.Business.JobCreationBAL();

        protected void Page_Load(object sender, EventArgs e)
        {
            Label pagename;
            pagename = (Label)Master.FindControl("lblName");
            pagename.Text = "Check List";
            if (!IsPostBack)
            {
                JobNo();
                ddlJobNo.SelectedValue = (string)Session["JobNo"];
                Assable((string)Session["JobNo"]);
                ObjectDataSource1.FilterExpression = "JobNo='" + (string)Session["JobNo"] + "'";
                ObjectDataSource2.FilterExpression = "JobNo='" + (string)Session["JobNo"] + "'";
                ObjectDataSource3.FilterExpression = "JobNo='" + (string)Session["JobNo"] + "'";
                ObjectDataSource4.FilterExpression = "JobNo='" + (string)Session["JobNo"] + "'";
                ObjectDataSource5.FilterExpression = "JobNo='" + (string)Session["JobNo"] + "'";
                ObjectDataSource6.FilterExpression = "JobNo='" + (string)Session["JobNo"] + "'";
                ObjectDataSource7.FilterExpression = "JobNo='" + (string)Session["JobNo"] + "'";
                ObjectDataSource8.FilterExpression = "JobNo='" + (string)Session["JobNo"] + "'";
                ObjectDataSource1.DataBind();
                //ObjectDataSource2.DataBind();
                //this.ReportViewer1.LocalReport.LoadSubreportDefinition("Print_CR_CheckList.rdlc", Print_CR_CheckList);
                //ReportViewer1.LocalReport.SubreportProcessing += new SubreportProcessingEventHandler(SetSubDataSource);
                ReportViewer1.LocalReport.SubreportProcessing += new SubreportProcessingEventHandler(SetSubDataSource);
                ReportViewer1.LocalReport.SubreportProcessing += new SubreportProcessingEventHandler(SetSchemeDataSource);
                ReportViewer1.LocalReport.SubreportProcessing += new SubreportProcessingEventHandler(SetRSP);
                ReportViewer1.LocalReport.Refresh();
            }

            if (Session["JobNo"]!="")
            {
               
            }
        }
        public void SetSubDataSource(object sender, SubreportProcessingEventArgs e)
        {
            e.DataSources.Add(new ReportDataSource("DataSet1", "ObjectDataSource2"));
        }
        public void SetSchemeDataSource(object sender, SubreportProcessingEventArgs e)
        {
            e.DataSources.Add(new ReportDataSource("DataSetIMPScheme", "ObjectDataSource3"));
        }
        public void SetSubContDataSource(object sender, SubreportProcessingEventArgs e)
        {
            e.DataSources.Add(new ReportDataSource("DataSetContainerDetails", "ObjectDataSource3"));
        }
        public void SetRSP(object sender, SubreportProcessingEventArgs e)
        {
            e.DataSources.Add(new ReportDataSource("DataSetRSP", "ObjectDataSource6"));
        }
        protected void btnBack1_Click(object sender, EventArgs e)
        {
            Response.Redirect("frmProductMainPage.aspx?Mode=Invoice");
        }
        protected void btnBack2_Click(object sender, EventArgs e)
        {
            Response.Redirect("frmProductMainPage.aspx?Mode=Invoice");
        }
        public void JobNo()
        {
            DataSet dt = obj1.GetJobNo();
            ddlJobNo.DataSource = dt;
            ddlJobNo.DataValueField = "JobNo";
            ddlJobNo.DataTextField = "JobNo";
            ddlJobNo.DataBind();
        }
        public void  Assable(string JobNo)
        {
            try
            {
                double AssableValue = 0;
                double TotInvAmt = 0;
                double TotDutyAmt = 0;
                SqlConnection conn = new SqlConnection(strcon);
                conn.Open();
                string qry = "select SUM(ProdAmtRs) AS TotalInvAmt, SUM(AssableValue) AS AssableValue, SUM(TotalDutyAmt) AS TotalDutyAmt from T_Product where JobNo = '" + JobNo + "'";
                SqlDataAdapter da = new SqlDataAdapter(qry, conn);
                DataSet ds = new DataSet();
                da.Fill(ds, "DATA");
                if (ds.Tables["DATA"].Rows.Count != 0)
                {
                    DataRowView row1 = ds.Tables["DATA"].DefaultView[0];
                    AssableValue = Convert.ToDouble(row1["AssableValue"].ToString());
                    TotInvAmt = Convert.ToDouble(row1["TotalInvAmt"].ToString());
                    TotDutyAmt = Convert.ToDouble(row1["TotalDutyAmt"].ToString());
                }
                conn.Close();
                obj2.UpdateAssable(ddlJobNo.SelectedValue, AssableValue, TotInvAmt, TotDutyAmt);
            }
            catch
            {
            }
        }
        protected void btnGenereate_Click(object sender, EventArgs e)
        {
            Assable(ddlJobNo.SelectedValue);
            Session["JobNo"] = ddlJobNo.SelectedValue;
            ObjectDataSource1.FilterExpression = "JobNo='" + (string)Session["JobNo"] + "'";
            ObjectDataSource2.FilterExpression = "JobNo='" + (string)Session["JobNo"] + "'";
            ObjectDataSource3.FilterExpression = "JobNo='" + (string)Session["JobNo"] + "'";
            ObjectDataSource4.FilterExpression = "JobNo='" + (string)Session["JobNo"] + "'";
            ObjectDataSource5.FilterExpression = "JobNo='" + (string)Session["JobNo"] + "'";
            ObjectDataSource6.FilterExpression = "JobNo='" + (string)Session["JobNo"] + "'";
            ObjectDataSource7.FilterExpression = "JobNo='" + (string)Session["JobNo"] + "'";
            ObjectDataSource8.FilterExpression = "JobNo='" + (string)Session["JobNo"] + "'";
            ObjectDataSource1.DataBind();
            //ObjectDataSource2.DataBind();
            //this.ReportViewer1.LocalReport.LoadSubreportDefinition("Print_CR_CheckList.rdlc", Print_CR_CheckList);
            //ReportViewer1.LocalReport.SubreportProcessing += new SubreportProcessingEventHandler(SetSubDataSource);
            ReportViewer1.LocalReport.SubreportProcessing += new SubreportProcessingEventHandler(SetSubDataSource);
            ReportViewer1.LocalReport.SubreportProcessing += new SubreportProcessingEventHandler(SetSchemeDataSource);
            ReportViewer1.LocalReport.SubreportProcessing += new SubreportProcessingEventHandler(SetRSP);
            ReportViewer1.LocalReport.Refresh();
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

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "popup", "window.open('frmPopupSearch.aspx','_blank','width=850,height=720, menubar=no, scrollbars=yes, toolbar=no, location=no, resizable=no, left=10, top=120');", true);
        }

    }
}