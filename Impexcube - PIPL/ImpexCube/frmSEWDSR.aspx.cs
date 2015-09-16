using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VTS.ImpexCube.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Text;
using System.Data;

namespace ImpexCube
{
    public partial class frmSEWDSR : System.Web.UI.Page
    {
        CommonDL objCommonDL = new CommonDL();
        string strcon = (string)ConfigurationManager.ConnectionStrings["Constr"].ConnectionString;

        SqlCommand cmd;
        SqlDataAdapter da;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DropImporter();
                //txtFrom.Text = (string)Session["fdate"];
                //txtTo.Text = DateTime.Now.ToString("dd/MM/yyyy");
                //DropImporter();
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

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            StringBuilder Query = new StringBuilder();
            StringBuilder Condition = new StringBuilder();

            Condition.Append("");
            if (!string.IsNullOrEmpty(txtJobNo.Text))
            {
                Condition.Append(" and dbo.T_JobCreation.JobNo =  '" + txtJobNo.Text + "'");
            }
            if (ddlImporter.SelectedValue != "~Select~")
            {
                Condition.Append(" and Importer = '" + ddlImporter.SelectedValue + "'");
            }
            Condition.Append(" and dbo.T_JobCreation.Status_Job='N' ");//Unbilled Jobs

            Query.Append("Select dbo.T_JobCreation.JobNo, dbo.T_JobCreation.Mode, dbo.T_ShipmentDetails.MasterBLNo +' dt. '+ dbo.T_ShipmentDetails.MasterBLDate as MasterBLNo , dbo.T_ShipmentDetails.HouseBLNo,");
            Query.Append("dbo.T_ShipmentDetails.HouseBLDate, dbo.T_JobCreation.BENo, dbo.T_Importer.Importer, dbo.T_ShipmentDetails.FFName+dbo.T_ShipmentDetails.AgentName+dbo.T_ShipmentDetails.ShippingLine as FFName, ");
            Query.Append(" dbo.T_ShipmentDetails.MarksNos, dbo.T_ShipmentDetails.NetWeight, convert(varchar(11),dbo.T_ShipmentDetails.NoOfPackages)+' '+ dbo.T_ShipmentDetails.PackagesUnit as NoOfPackages, ");
            Query.Append(" dbo.T_ShipmentDetails.NetUint,convert(varchar(11),dbo.T_ShipmentDetails.GrossWeight) +' '+ dbo.T_ShipmentDetails.GrossWeightUnit as GrossWeight , dbo.T_ShipmentDetails.ETA, ");
            Query.Append("dbo.T_ShipmentDetails.GLDInwardDate, dbo.T_ShipmentDetails.ShipmentPort, dbo.T_JobCreation.JobReceivedDate, dbo.T_JobCreation.TotalDuty,");
            Query.Append("dbo.T_JobCreation.TotalAssVal, dbo.T_Importer.BEHeading ");
            Query.Append(" FROM dbo.T_JobCreation INNER JOIN");
            Query.Append(" dbo.T_Importer ON dbo.T_JobCreation.JobNo = dbo.T_Importer.JobNo INNER JOIN ");
            Query.Append(" dbo.T_ShipmentDetails ON dbo.T_JobCreation.JobNo = dbo.T_ShipmentDetails.JobNo  Where 1=1  " + Condition.ToString() + " ");
            DataSet SEWDSR = objCommonDL.GetDataSet(Query.ToString());
            if (SEWDSR.Tables["Table"].Rows.Count != 0)
            {
                gvSEW.DataSource = SEWDSR;
                gvSEW.DataBind();
            }
            else
            {
                gvSEW.DataSource = null;
                gvSEW.DataBind();
            }

        }

        protected void gvSEW_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblEmpID = (Label)e.Row.FindControl("lblJobNo");

                GridView gv_Child = (GridView)e.Row.FindControl("gvInvoice");
                GridView gv_Shipment = (GridView)e.Row.FindControl("gvContainer");
                GridView gv_Status = (GridView)e.Row.FindControl("gvStatus");
                string JobNO = lblEmpID.Text;
                DataSet ds = new DataSet();
                DataSet Conds = new DataSet();
                DataSet Statusds = new DataSet();
                SqlConnection conn = new SqlConnection(strcon);
                conn.Open();
                string InvQuery = "Select distinct invoiceno+' dt.'+invoicedate as Invoice,InvoiceProductValues as [TOTAL INVOICE VALUE]  from T_InvoiceDetails where JobNo=@JobNo";
                string ContainerQuery = "select jobno,container,ContainerNo,loadtype,COUNT(container)as a,(convert(varchar(10),COUNT(container))+'X '+Container+'-'+LoadType) as b from T_ShipmentContainerInfo where JobNo=@JobNo group by jobno,ContainerNo,container,loadtype order by jobno ";
                string StatusQuery = "Select Remarks,convert(varchar(11),StatusDate,103) as StatusDate  from T_JobStageUpdate where JobNo=@JobNo";
                SqlCommand cmd = new SqlCommand(InvQuery, conn);
                cmd.Parameters.AddWithValue("@JobNo", JobNO);
                SqlDataAdapter adp = new SqlDataAdapter(cmd);
                adp.Fill(ds, "Table");
                conn.Close();
                if (ds.Tables["Table"].Rows.Count != 0)
                {
                    gv_Child.DataSource = ds;
                    gv_Child.DataBind();
                }

                try
                {
                    double TotalAmt = 0;
                    int i = 0;
                    foreach (GridViewRow gv in gv_Child.Rows)
                    {
                        string amt = gv_Child.Rows[i].Cells[1].Text;
                        TotalAmt = TotalAmt + Convert.ToDouble(amt);
                        i++;
                    }
                    gv_Child.FooterRow.Cells[0].Text = "Total Inv Value ";
                    gv_Child.FooterRow.Cells[1].Text = Convert.ToString(TotalAmt);
                    //txtMiscelTotalAmount.Text = Convert.ToString(TotalAmtINR);
                    //lblOtherCharges.Text = Convert.ToString(TotalAmtINR);
                }
                catch (Exception ex)
                {

                }

                Container(gv_Shipment, JobNO, Conds, conn,  ref ContainerQuery, ref cmd, ref da);
                Status(gv_Status, JobNO, Statusds, conn,  ref StatusQuery, ref cmd, ref da);
            }
        }

        private static void Container(GridView gv_Shipment, string JobNO, DataSet ds , SqlConnection conn,  ref string ContainerQuery, ref SqlCommand cmd1, ref SqlDataAdapter da)
        {
            //ContainerQuery = "Select distinct ContainerNo,Container +' '+ LoadType as  [CONTAINER DETAILS] from   T_ShipmentContainerInfo  where JobNo=@JobNo";
            SqlCommand CMD1 = new SqlCommand(ContainerQuery, conn);
            CMD1.Parameters.AddWithValue("@JobNo", JobNO);
            da = new SqlDataAdapter(CMD1);
            da.Fill(ds, "Table");
            conn.Close();
            if (ds.Tables["Table"].Rows.Count != 0)
            {
                gv_Shipment.DataSource = ds;
                gv_Shipment.DataBind();
            }
        }


        private static void Status(GridView gv_Status, string JobNO, DataSet ds, SqlConnection conn, ref string StatusQuery, ref SqlCommand cmd1, ref SqlDataAdapter da)
        {
            //ContainerQuery = "Select distinct ContainerNo,Container +' '+ LoadType as  [CONTAINER DETAILS] from   T_ShipmentContainerInfo  where JobNo=@JobNo";
            SqlCommand CMD1 = new SqlCommand(StatusQuery, conn);
            CMD1.Parameters.AddWithValue("@JobNo", JobNO);
            da = new SqlDataAdapter(CMD1);
            da.Fill(ds, "Table");
            conn.Close();
            if (ds.Tables["Table"].Rows.Count != 0)
            {
                gv_Status.DataSource = ds;
                gv_Status.DataBind();
            }
        }

        public override void VerifyRenderingInServerForm(Control control)
        {
            return;
        }

        protected void btnexporttoexc_Click(object sender, EventArgs e)
        {
            string strFileName = txtJobNo.Text + ".xls";
            try
            {
                if ((gvSEW.Rows.Count != 0) || (gvSEW.Rows.Count != 0))
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