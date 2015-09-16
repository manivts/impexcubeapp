using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VTS.ImpexCube.Data;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Text;

namespace ImpexCube
{
    public partial class frmDSRFormat1 : System.Web.UI.Page
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
        public override void VerifyRenderingInServerForm(Control control)
        {
            return;
        }

        protected void btnexporttoexc_Click(object sender, EventArgs e)
        {
            string strFileName = "Status" + ".xls";
            try
            {
                //if ((gvDSR.Rows.Count != 0) || (gvDSR.Rows.Count != 0))
                //{
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
                //}
                //else
                //{
                //    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "Key", "alert('No Records Found EXCEL Report Cannot be generated!.');", true);
                //}
            }
            catch (Exception)
            {

            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {


            if (rbFormat.SelectedValue == "Format1")
            {
                Format1();
            }
            else if (rbFormat.SelectedValue == "Format2")
            {
                Format2();
            }
            else if (rbFormat.SelectedValue == "Format3")
            {
                Format3();
            }

        }

        private void Format1()
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
            if (ddlJobStatus.SelectedValue == "Pending")
            {
                Condition.Append(" and dbo.T_JobCreation.Status_Job='N' ");//Unbilled Jobs
            }
            else if (ddlJobStatus.SelectedValue == "Completed")
            {
                Condition.Append(" and dbo.T_JobCreation.Status_Job='Y' ");//billed Jobs
            }
            else
            {
                Condition.Append(" OR dbo.T_JobCreation.Status_Job='N' OR dbo.T_JobCreation.Status_Job='Y' ");//Both Unbilled and Billed Jobs
            }

            Query.Append("SELECT  dbo.T_JobCreation.JobNo, dbo.T_JobCreation.Mode, dbo.T_ShipmentDetails.MasterBLNo + ' dt. ' + dbo.T_ShipmentDetails.MasterBLDate AS MasterBLNo, ");
            Query.Append("dbo.T_ShipmentDetails.HouseBLNo, dbo.T_ShipmentDetails.HouseBLDate, dbo.T_JobCreation.BENo+ ' dt. ' +dbo.T_JobCreation.BEDate as BENoDate, dbo.T_Importer.Importer,dbo.T_Importer.Consignor,dbo.T_Importer.ConsignorCountry, dbo.T_ShipmentDetails.FFName,  ");
            Query.Append("dbo.T_ShipmentDetails.AgentName, dbo.T_ShipmentDetails.MarksNos, dbo.T_ShipmentDetails.NetWeight, CONVERT(varchar(11), ");
            Query.Append("dbo.T_ShipmentDetails.NoOfPackages) + ' ' + dbo.T_ShipmentDetails.PackagesUnit AS NoOfPackages, dbo.T_ShipmentDetails.NetUint, CONVERT(varchar(11),  ");
            Query.Append("dbo.T_ShipmentDetails.GrossWeight) + ' ' + dbo.T_ShipmentDetails.GrossWeightUnit AS GrossWeight, dbo.T_ShipmentDetails.ETA,  ");
            Query.Append("dbo.T_ShipmentDetails.GLDInwardDate, dbo.T_ShipmentDetails.ShipmentPort, dbo.T_JobCreation.JobReceivedDate, dbo.T_JobCreation.TotalDuty, ");
            Query.Append("dbo.T_JobCreation.TotalAssVal, dbo.T_Importer.BEHeading, dbo.T_JobStageUpdate.IsModified, dbo.T_JobStageUpdate.Remarks ");
            Query.Append("FROM  dbo.T_JobCreation INNER JOIN ");
            Query.Append("dbo.T_Importer ON dbo.T_JobCreation.JobNo = dbo.T_Importer.JobNo INNER JOIN ");
            Query.Append("dbo.T_ShipmentDetails ON dbo.T_JobCreation.JobNo = dbo.T_ShipmentDetails.JobNo INNER JOIN ");
            Query.Append("dbo.T_JobStageUpdate ON dbo.T_JobCreation.JobNo = dbo.T_JobStageUpdate.JobNo ");
            Query.Append("WHERE     (dbo.T_JobStageUpdate.IsModified = 1) " + Condition.ToString() + " ");
            DataSet SEWDSR = objCommonDL.GetDataSet(Query.ToString());
            if (SEWDSR.Tables["Table"].Rows.Count != 0)
            {
                gvDSR.DataSource = SEWDSR;
                gvDSR.DataBind();
                gvSEW.DataBind();
                gvFormat3.DataBind();
            }
            else
            {
                gvDSR.DataSource = null;
                gvDSR.DataBind();
            }
        }

        private void Format2()
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
            if (ddlJobStatus.SelectedValue == "Pending")
            {
                Condition.Append(" and dbo.T_JobCreation.Status_Job='N' ");//Unbilled Jobs
            }
            else if (ddlJobStatus.SelectedValue == "Completed")
            {
                Condition.Append(" and dbo.T_JobCreation.Status_Job='Y' ");//billed Jobs
            }
            else
            {
                Condition.Append(" OR dbo.T_JobCreation.Status_Job='N' OR dbo.T_JobCreation.Status_Job='Y' ");//Both Unbilled and Billed Jobs
            }

            Query.Append("Select dbo.T_JobCreation.JobNo, dbo.T_JobCreation.Mode, dbo.T_ShipmentDetails.MasterBLNo +' dt. '+ dbo.T_ShipmentDetails.MasterBLDate as MasterBLNo , dbo.T_ShipmentDetails.HouseBLNo,");
            Query.Append("dbo.T_ShipmentDetails.HouseBLDate, dbo.T_JobCreation.BENo, dbo.T_Importer.Importer, dbo.T_ShipmentDetails.FFName+' / '+dbo.T_ShipmentDetails.AgentName+ ' / ' + dbo.T_ShipmentDetails.ShippingLine as FFName, ");
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
                gvDSR.DataBind();
                gvFormat3.DataBind();
            }
            else
            {
                gvSEW.DataSource = null;
                gvSEW.DataBind();
            }
        }

        private void Format3()
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
            if (ddlJobStatus.SelectedValue == "Pending")
            {
                Condition.Append(" and dbo.T_JobCreation.Status_Job='N' ");//Unbilled Jobs
            }
            else if (ddlJobStatus.SelectedValue == "Completed")
            {
                Condition.Append(" and dbo.T_JobCreation.Status_Job='Y' ");//billed Jobs
            }
            else
            {
                Condition.Append(" OR dbo.T_JobCreation.Status_Job='N' OR dbo.T_JobCreation.Status_Job='Y' ");//Both Unbilled and Billed Jobs
            }

            Query.Append("SELECT  dbo.T_JobCreation.JobNo, dbo.T_JobCreation.Mode, dbo.T_ShipmentDetails.MasterBLNo + ' dt. ' + dbo.T_ShipmentDetails.MasterBLDate AS MasterBLNo, ");
            Query.Append("dbo.T_ShipmentDetails.HouseBLNo, dbo.T_ShipmentDetails.HouseBLDate, dbo.T_JobCreation.BENo+ ' dt. ' +dbo.T_JobCreation.BEDate as BENoDate, dbo.T_Importer.Importer,dbo.T_Importer.Consignor,dbo.T_Importer.ConsignorCountry, dbo.T_ShipmentDetails.FFName,  ");
            Query.Append("dbo.T_ShipmentDetails.AgentName, dbo.T_ShipmentDetails.MarksNos, dbo.T_ShipmentDetails.NetWeight, CONVERT(varchar(11), ");
            Query.Append("dbo.T_ShipmentDetails.NoOfPackages) + ' ' + dbo.T_ShipmentDetails.PackagesUnit AS NoOfPackages, dbo.T_ShipmentDetails.NetUint, CONVERT(varchar(11),  ");
            Query.Append("dbo.T_ShipmentDetails.GrossWeight) + ' ' + dbo.T_ShipmentDetails.GrossWeightUnit AS GrossWeight, dbo.T_ShipmentDetails.ETA,  ");
            Query.Append("dbo.T_ShipmentDetails.GLDInwardDate, dbo.T_ShipmentDetails.ShipmentPort, dbo.T_JobCreation.JobReceivedDate, dbo.T_JobCreation.TotalDuty, ");
            Query.Append("dbo.T_JobCreation.TotalAssVal, dbo.T_Importer.BEHeading, dbo.T_JobStageUpdate.IsModified, dbo.T_JobStageUpdate.Remarks , dbo.T_ShipmentDetails.LocalIGMNo ");
            Query.Append("FROM  dbo.T_JobCreation INNER JOIN ");
            Query.Append("dbo.T_Importer ON dbo.T_JobCreation.JobNo = dbo.T_Importer.JobNo INNER JOIN ");
            Query.Append("dbo.T_ShipmentDetails ON dbo.T_JobCreation.JobNo = dbo.T_ShipmentDetails.JobNo INNER JOIN ");
            Query.Append("dbo.T_JobStageUpdate ON dbo.T_JobCreation.JobNo = dbo.T_JobStageUpdate.JobNo ");
            Query.Append("WHERE     (dbo.T_JobStageUpdate.IsModified = 1) " + Condition.ToString() + " ");
            DataSet Format3 = objCommonDL.GetDataSet(Query.ToString());
            if (Format3.Tables["Table"].Rows.Count != 0)
            {
                gvFormat3.DataSource = Format3;
                gvFormat3.DataBind();
                gvSEW.DataBind();
                gvDSR.DataBind();
            }
            else
            {
                gvDSR.DataSource = null;
                gvDSR.DataBind();
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

                Container(gv_Shipment, JobNO, Conds, conn, ref ContainerQuery, ref cmd, ref da);
                Status(gv_Status, JobNO, Statusds, conn, ref StatusQuery, ref cmd, ref da);
            }
        }

        private static void Container(GridView gv_Shipment, string JobNO, DataSet ds, SqlConnection conn, ref string ContainerQuery, ref SqlCommand cmd1, ref SqlDataAdapter da)
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

        protected void gvFormat3_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblEmpID = (Label)e.Row.FindControl("lblJobNo");

                GridView gv_Child = (GridView)e.Row.FindControl("gvInvoice1");
                GridView gv_Shipment = (GridView)e.Row.FindControl("gvContainer1");
                GridView gv_Status = (GridView)e.Row.FindControl("gvStatus1");
                string JobNO = lblEmpID.Text;
                DataSet ds = new DataSet();
                DataSet Conds = new DataSet();
                DataSet Statusds = new DataSet();
                SqlConnection conn = new SqlConnection(strcon);
                conn.Open();
                string InvQuery = "Select distinct invoiceno+' dt.'+invoicedate as Invoice,InvoiceProductValues as [TOTAL INVOICE VALUE]  from T_InvoiceDetails where JobNo=@JobNo";
                string ContainerQuery = "select jobno,container,ContainerNo,loadtype,COUNT(container)as a,(convert(varchar(10),COUNT(container))+'X '+Container+'-'+LoadType) as b from T_ShipmentContainerInfo where JobNo=@JobNo group by jobno,ContainerNo,container,loadtype order by jobno ";
                //string StatusQuery = "Select Remarks,convert(varchar(11),StatusDate,103) as StatusDate  from T_JobStageUpdate where JobNo=@JobNo";
                string StatusQuery = "SELECT STUFF((SELECT ' / ' + t2.Remarks  FROM T_JobStageUpdate t2 WHERE t2.JobNo = t1.JobNo FOR XML PATH ('')),1,2,'') AS Remark FROM T_JobCreation t1  where JobNo=@JobNo order by t1.JobNo asc  ";
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

                Container1(gv_Shipment, JobNO, Conds, conn, ref ContainerQuery, ref cmd, ref da);
                Status1(gv_Status, JobNO, Statusds, conn, ref StatusQuery, ref cmd, ref da);
            }
        }


        private static void Container1(GridView gv_Shipment, string JobNO, DataSet ds, SqlConnection conn, ref string ContainerQuery, ref SqlCommand cmd1, ref SqlDataAdapter da)
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


        private static void Status1(GridView gv_Status, string JobNO, DataSet ds, SqlConnection conn, ref string StatusQuery, ref SqlCommand cmd1, ref SqlDataAdapter da)
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
    }
}