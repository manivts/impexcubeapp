using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Collections.Generic;
using System.Data.SqlClient;
using VTS.ImpexCube.Data;
using System.Text;


namespace ImpexCube
{
    public partial class frmTestRow : System.Web.UI.Page
    {
        CommonDL objCommonDL = new CommonDL();
        string strcon = (string)ConfigurationManager.ConnectionStrings["Constr"].ConnectionString;

        SqlCommand cmd;
        SqlDataAdapter da;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                //txtFrom.Text = (string)Session["fdate"];
                //txtTo.Text = DateTime.Now.ToString("dd/MM/yyyy");
                DropImporter();
            }
        }

        private void DropImporter()
        {
            string Query = "Select Distinct Importer From T_Importer where Importer <>''";
            DataSet ds = objCommonDL.GetDataSet(Query);
            if (ds.Tables["Table"].Rows.Count != 0)
            {
                ddlImporter.DataSource = ds;
                ddlImporter.DataTextField = "Importer";
                ddlImporter.DataValueField = "Importer";
                ddlImporter.DataBind();
                ddlImporter.Items.Insert(0, new ListItem("~Select~", "~Select~"));
            }
        }

        private void BindGrid()
        {

            StringBuilder cmdstr = new StringBuilder();
            
            DataSet ds = new DataSet();
            SqlConnection con = new SqlConnection(strcon);
            con.Open();

            cmdstr.Append("SELECT dbo.T_JobCreation.JobNo, dbo.T_JobCreation.Mode, dbo.T_JobCreation.BENo, dbo.T_JobCreation.BEDate, dbo.T_Importer.Importer,");
            cmdstr.Append("dbo.T_ShipmentDetails.NoOfPackages, dbo.T_ShipmentDetails.NetWeight, dbo.T_ShipmentDetails.NetUint, dbo.T_ShipmentDetails.PackagesUnit, ");
            cmdstr.Append(" dbo.T_ShipmentDetails.MarksNos, dbo.T_ShipmentDetails.GrossWeight, dbo.T_ShipmentDetails.GrossWeightUnit from ");
            cmdstr.Append(" dbo.T_JobCreation INNER JOIN");
            cmdstr.Append(" dbo.T_Importer ON dbo.T_JobCreation.JobNo = dbo.T_Importer.JobNo INNER JOIN ");
            cmdstr.Append(" dbo.T_ShipmentDetails ON dbo.T_JobCreation.JobNo = dbo.T_ShipmentDetails.JobNo  Where 1=1 ");
            //string cmdstr = "SELECT Distinct JobNo FROM T_JobStageUpdate WHERE (JobNo = '00645')";
            SqlCommand cmd = new SqlCommand(cmdstr.ToString(), con);
            SqlDataAdapter adp = new SqlDataAdapter(cmd);
            adp.Fill(ds,"jobno");
            con.Close();
            if(ds.Tables["jobno"].Rows.Count!=0)
            {
            gvTest1.DataSource = ds;
            gvTest1.DataBind();
            }

        }

        protected void gvTest1_OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)

        {
            Label lblEmpID = (Label)e.Row.FindControl("lblJobNo");

            GridView gv_Child = (GridView)e.Row.FindControl("gvTest2"); 

            string txtempid = lblEmpID.Text; 

            DataSet ds = new DataSet();
            SqlConnection conn = new SqlConnection(strcon);
            conn.Open();
            string cmdstr = "Select Remarks,convert(varchar(11),StatusDate,103) as StatusDate  from T_JobStageUpdate where JobNo=@JobNo";
            SqlCommand cmd = new SqlCommand(cmdstr, conn);
            cmd.Parameters.AddWithValue("@JobNo", txtempid);
            SqlDataAdapter adp = new SqlDataAdapter(cmd);
            adp.Fill(ds);
            conn.Close();
            gv_Child.DataSource = ds;
            gv_Child.DataBind();
        }
        }
        public override void VerifyRenderingInServerForm(Control control)
        {
            return;
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            string strFileName = "StatusHistory" + ".xls";
            try
            {
                if ((gvTest1.Rows.Count!=0) || (gvTest1.Rows.Count != 0))
                {
                    string na = "GoodsReceiptNote.xls";
                    string ExcelExport = na;
                    // Export(ExcelExport, GridView1);
                    Response.Clear();
                    Response.Buffer = true;
                    Response.AddHeader("content-disposition", "attachment;filename= " + strFileName + "");
                    Response.Charset = "";
                    Response.ContentType = "application/vnd.ms-excel";
                    System.IO.StringWriter stringWrite = new System.IO.StringWriter();
                    System.Web.UI.HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);
                    pnlGrid.RenderControl(htmlWrite);
                    Response.Output.Write(stringWrite.ToString());
                    Response.Flush();
                    Response.End();
                }
                else
                {
                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "Key", "alert('No Records Found EXCEL Report Cannot be generated!.');", true);

                }

            }
            catch (Exception)
            {

            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            StringBuilder Query = new StringBuilder();
            string condition = string.Empty;
            string condition1 = string.Empty;
            if (!string.IsNullOrEmpty(txtFrom.Text))
            {
                Query.Append(" and JobReceivedDate >= '" + txtFrom.Text + "'");
            }
            if (!string.IsNullOrEmpty(txtTo.Text))
            {
                Query.Append(" and JobReceivedDate <= '" + txtTo.Text + "'");
            }
            if (ddlImporter.SelectedValue!="~Select~")
            {
                Query.Append(" and Importer = '" + ddlImporter.SelectedValue + "'");
            }
            if (!string.IsNullOrEmpty(txtJobNo.Text))
            {
                Query.Append(" and dbo.T_JobCreation.JobNo like '" + txtJobNo.Text + "'");
            }

            StringBuilder cmdstr = new StringBuilder();

            DataSet ds = new DataSet();
            SqlConnection con = new SqlConnection(strcon);
            con.Open();

            cmdstr.Append("SELECT dbo.T_JobCreation.JobNo,dbo.T_JobCreation.JobReceivedDate, dbo.T_JobCreation.Mode,dbo.T_JobCreation.BEType, dbo.T_JobCreation.BENo, dbo.T_JobCreation.BEDate,convert(varchar(11),dbo.T_ShipmentDetails.ETA,103) as ETA, dbo.T_Importer.Importer,");
            cmdstr.Append("dbo.T_ShipmentDetails.NoOfPackages, dbo.T_ShipmentDetails.NetWeight, dbo.T_ShipmentDetails.NetUint, dbo.T_ShipmentDetails.PackagesUnit, ");
            cmdstr.Append(" dbo.T_ShipmentDetails.MarksNos, dbo.T_ShipmentDetails.GrossWeight, dbo.T_ShipmentDetails.GrossWeightUnit,dbo.T_JobCreation.status_job from ");
            cmdstr.Append(" dbo.T_JobCreation INNER JOIN");
            cmdstr.Append(" dbo.T_Importer ON dbo.T_JobCreation.JobNo = dbo.T_Importer.JobNo INNER JOIN ");
            cmdstr.Append(" dbo.T_ShipmentDetails ON dbo.T_JobCreation.JobNo = dbo.T_ShipmentDetails.JobNo  Where 1=1  "+Query.ToString()+"");
            //string cmdstr = "SELECT Distinct JobNo FROM T_JobStageUpdate WHERE (JobNo = '00645')";
            SqlCommand cmd = new SqlCommand(cmdstr.ToString(), con);
            SqlDataAdapter adp = new SqlDataAdapter(cmd);
            adp.Fill(ds, "jobno");
            con.Close();
            if (ds.Tables["jobno"].Rows.Count != 0)
            {
                gvTest1.DataSource = ds;
                gvTest1.DataBind();
                btnExport.Visible = true;
            }
        }

        protected void btnExport_Click(object sender, EventArgs e)
        {
            string strFileName = "StatusHistory" + ".xls";
            try
            {
                if ((gvTest1.Rows.Count != 0) || (gvTest1.Rows.Count != 0))
                {
                    string na = "GoodsReceiptNote.xls";
                    string ExcelExport = na;
                    // Export(ExcelExport, GridView1);
                    Response.Clear();
                    Response.Buffer = true;
                    Response.AddHeader("content-disposition", "attachment;filename= " + strFileName + "");
                    Response.Charset = "";
                    Response.ContentType = "application/vnd.ms-excel";
                    System.IO.StringWriter stringWrite = new System.IO.StringWriter();
                    System.Web.UI.HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);
                    pnlGrid.RenderControl(htmlWrite);
                    Response.Output.Write(stringWrite.ToString());
                    Response.Flush();
                    Response.End();
                }
                else
                {
                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "Key", "alert('No Records Found EXCEL Report Cannot be generated!.');", true);
                }
            }
            catch (Exception)
            {

            }
        }
    }
}