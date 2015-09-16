using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;

namespace ImpexCube
{
    public partial class frmUserReports : System.Web.UI.Page
    {
        VTS.ImpexCube.Business.UserReportsBL objUserReports = new VTS.ImpexCube.Business.UserReportsBL();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack == false)
            {
                string formID = "Job Report";
                Authenticate.Forms(formID);
                string Validate = (string)Session["DISABLE"];
                if (Validate == "True")
                {
                if ((string)Session["FYear"] == (string)Session["CurrentFinancial"])
                {
                    txtForm.Text = (string)Session["fdate"];
                    txtTo.Text = DateTime.Now.ToString("dd/MM/yyyy");
                }
                else
                {
                    txtForm.Text = (string)Session["fdate"];
                    txtTo.Text = (string)Session["edate"];
                }
                DropStageDetails();
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "redirect", "alert('You have not Authorized for this Page'); window.location.href='HomePage.aspx';", true);

                }

            }
        }

        private void DropStageDetails()
        {
            DataSet ds = objUserReports.SelectStage();
            if (ds.Tables["stage"].Rows.Count != 0)
            {
                ddlStage.DataSource = ds;
                ddlStage.DataTextField = "StageName";
                ddlStage.DataValueField = "StageId";
                ddlStage.DataBind();
                ddlStage.Items.Insert(0, new ListItem("-Select-", "0"));
            }
        }

        protected void ddlStage_SelectedIndexChanged(object sender, EventArgs e)
        {
            int stageid = Convert.ToInt32(ddlStage.SelectedValue);
            DataSet ds = objUserReports.SelectStageStatus(stageid);
            if (ds.Tables["status"].Rows.Count != 0)
            {
                ddlStatus.DataSource = ds;
                ddlStatus.DataTextField = "StatusName";
                ddlStatus.DataValueField = "Id";
                ddlStatus.DataBind();
                ddlStatus.Items.Insert(0, new ListItem("-Select-", "0"));
            }
            else
            {
                ddlStatus.Items.Clear();
                ddlStatus.DataSource = null;
                ddlStatus.DataBind();
            }
        }

        protected void ddlShipmentType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlShipmentType.SelectedItem.Text == "Sea")
            {
                ListItem[] items = new ListItem[3];
                items[0] = new ListItem("-Select-", "0");
                items[1] = new ListItem("LCL", "1");
                items[2] = new ListItem("FCL", "2");
                ddlShipmentLoad.Items.AddRange(items);
                ddlShipmentLoad.DataBind();
            }
            else if (ddlShipmentType.SelectedItem.Text == "Air" || ddlShipmentType.SelectedItem.Text == "-Select-" )
            {
                ddlShipmentLoad.Items.Clear();
                ddlShipmentLoad.DataSource = null;
                ddlShipmentLoad.DataBind();
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            string stage = ddlStage.SelectedItem.Text;
            if (stage == "-Select-")
            {
                stage = string.Empty;
            }
            else
                stage = ddlStage.SelectedItem.Text;
            string status = string.Empty;
            status = ddlStatus.SelectedValue;
            if (status == "0" || status == "")
            {
                status = string.Empty;
            }
            else
                status = ddlStatus.SelectedItem.Text;

            string shipment = ddlShipmentType.SelectedItem.Text;
            if (shipment == "-Select-")
            {
                shipment = string.Empty;
            }
            else
                shipment = ddlShipmentType.SelectedItem.Text;
            string shipmentType = string.Empty;
            shipmentType = ddlShipmentLoad.SelectedValue;
            if (shipmentType == "0" || shipmentType == "")
            {
                shipmentType = string.Empty;
            }
            else
                shipmentType = ddlShipmentLoad.SelectedItem.Text;

            string form = txtForm.Text;
            string to = txtTo.Text;
            string importer = txtImporter.Text;
            string jno = txtJNO.Text;

            DataSet ds = objUserReports.SearchJobReportList(form, to, importer, jno, stage, status, shipment, shipmentType);
            if (ds.Tables["reports"].Rows.Count != 0)
            {
                DataTable dt = ds.Tables["reports"];
                Session["UserReport"] = dt;
                gvReportDetails.DataSource = ds;
                gvReportDetails.DataBind();
                GridDetails.Visible = true;
                btnExport.Visible = true;
            }
            else
            {
                gvReportDetails.DataSource = null;
                gvReportDetails.DataBind();
                GridDetails.Visible = false;
                btnExport.Visible = false;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('No records found');", true);
            }
        }

        protected void btnExportExcel_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dt = (DataTable)Session["UserReport"];
                if (dt.Rows.Count != 0)
                {
                    gvReportDetails.AllowPaging = false;
                    gvReportDetails.DataSource = dt;
                    gvReportDetails.DataBind();


                    string na = "JobReport" + ".xls";
                    string ExcelExport = na;

                    Export(ExcelExport, gvReportDetails);
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

        private void Export(string fileName, GridView gv)
        {            
            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.AddHeader("content-disposition", string.Format("attachment; filename={0}", fileName));
            HttpContext.Current.Response.ContentType = "application/ms-excel";

            using (StringWriter sw = new StringWriter())
            {
                using (HtmlTextWriter htw = new HtmlTextWriter(sw))
                {
                    //  Create a form to contain the grid
                    Table table = new Table();
                    table.GridLines = gv.GridLines;
                    //  add the header row to the table                   
                    if (gv.HeaderRow != null)
                    {
                        PrepareControlForExport(gv.HeaderRow);                        
                        table.Rows.Add(gv.HeaderRow);
                    }
                    //  add each of the data rows to the table
                    foreach (GridViewRow row in gv.Rows)
                    {
                        PrepareControlForExport(row);
                        row.Cells[0].Visible = true;
                        table.Rows.Add(row);
                    }

                    //  add the footer row to the table
                    if (gv.FooterRow != null)
                    {
                        PrepareControlForExport(gv.FooterRow);
                        table.Rows.Add(gv.FooterRow);
                    }

                    //  render the table into the htmlwriter
                    table.RenderControl(htw);

                    //  render the htmlwriter into the response
                    HttpContext.Current.Response.Write(sw.ToString());
                    HttpContext.Current.Response.End();
                }
            }
        }

        private void PrepareControlForExport(Control control)
        {
            for (int i = 0; i < control.Controls.Count; i++)
            {
                Literal l = new Literal();
                Control current = control.Controls[i];
                if (current is LinkButton)
                {
                    control.Controls.Remove(current);
                    control.Controls.AddAt(i, new LiteralControl((current as LinkButton).Text));
                }
                else if (current is ImageButton)
                {
                    control.Controls.Remove(current);
                    current.Visible = false;
                    //control.Controls.AddAt(i,l);
                }
                else if (current is HyperLink)
                {
                    control.Controls.Remove(current);
                    control.Controls.AddAt(i, new LiteralControl((current as HyperLink).Text));
                }
                else if (current is DropDownList)
                {
                    control.Controls.Remove(current);
                    control.Controls.AddAt(i, new LiteralControl((current as DropDownList).SelectedItem.Text));
                }
                else if (current is CheckBox)
                {
                    control.Controls.Remove(current);
                    control.Controls.AddAt(i, new LiteralControl((current as CheckBox).Checked ? "True" : "False"));
                }

                if (current.HasControls())
                {
                    PrepareControlForExport(current);
                }
            }
        }

    }
}