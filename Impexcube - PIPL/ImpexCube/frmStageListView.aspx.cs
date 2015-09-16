using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace ImpexCube
{
    public partial class frmStageListView : System.Web.UI.Page
    {
        VTS.ImpexCube.Business.JobStageUpdateBL objJobStage = new VTS.ImpexCube.Business.JobStageUpdateBL();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack == false)
            {         
                //string formID = "JSU";
                //Authenticate.Forms(formID);
                //string Validate = (string)Session["DISABLE"];
                //if (Validate == "1")
                //{
                //if ((string)Session["FYear"] == (string)Session["CurrentFinancial"])
                //{
                    txtDCOForm.Text = (string)Session["fdate"];
                    txtTo.Text = DateTime.Now.ToString("dd/MM/yyyy");
                //}
                //else
                //{
                //    txtDCOForm.Text = (string)Session["fdate"];
                //    txtTo.Text = (string)Session["edate"];
                //}
                DropDownStage();
               
                //}
                //else
                //{
                //    ScriptManager.RegisterStartupScript(this, this.GetType(), "redirect", "alert('You have not Authorized for this Page'); window.location.href='HomePage.aspx';", true);
                //}
            }
        }

        private void DropDownStage()
        {
            DataSet ds = objJobStage.SelectStage();
            if (ds.Tables["stage"].Rows.Count != 0)
            {
                ddlJobStages.DataSource = ds;
                ddlJobStages.DataTextField = "StageName";
                ddlJobStages.DataValueField = "StageId";
                ddlJobStages.DataBind();
                ddlJobStages.Items.Insert(0, new ListItem("-Select-", "0"));
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            string stage = ddlJobStages.SelectedItem.Text;
            if (stage == "-Select-")
            {
                stage = string.Empty;
            }
            else
                stage = ddlJobStages.SelectedItem.Text;     
            string status = string.Empty;
            status = ddlJobStageStatus.SelectedValue;
            if (status == "0" || status == "")
            {
                status = string.Empty;
            }
            else
                status = ddlJobStageStatus.SelectedItem.Text;

            string docform = txtDCOForm.Text;
            string to = txtTo.Text;
            string importer = txtImporter.Text;
            string jno = txtJNO.Text;
            Session["DocForm"] = docform;
            Session["To"] = to;
            Session["Importer"] = importer;
            Session["jno"] = jno;
            Session["Status"] = status;
            Session["Stage"] = stage;

            DataSet ds = objJobStage.SearchJobStatusList(docform, to, importer, jno, stage, status);
            if (ds.Tables["status"].Rows.Count != 0)
            {
                gvJobStageStatus.DataSource = ds;
                gvJobStageStatus.DataBind();
            }
            else
            {
                gvJobStageStatus.DataSource = null;
                gvJobStageStatus.DataBind();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('No records found');", true);
            }
        }

        protected void ddlJobStages_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlJobStages.SelectedItem.Text != "-Select-")
            {
                int stageid = Convert.ToInt32(ddlJobStages.SelectedValue);
                DataSet ds = objJobStage.SelectStageStatus(stageid);
                if (ds.Tables["status"].Rows.Count != 0)
                {
                    ddlJobStageStatus.DataSource = ds;
                    ddlJobStageStatus.DataTextField = "StatusName";
                    ddlJobStageStatus.DataValueField = "Id";
                    ddlJobStageStatus.DataBind();
                    ddlJobStageStatus.Items.Insert(0, new ListItem("-Select-", "0"));
                }
            }
            else
            {
                ddlJobStageStatus.Items.Clear();
                ddlJobStageStatus.DataSource = null;                
                ddlJobStageStatus.DataBind();                
            }
        }

        protected void gvJobStageStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (gvJobStageStatus.SelectedRow.Cells[1].Text != null)
            {
                Session["JobStatus"] = gvJobStageStatus.SelectedRow.Cells[1].Text.ToString();
                Response.Write("<script>window.open('frmJobStatusUpdate.aspx','popup','width=1100,height=730, menubar=no, scrollbars=yes, toolbar=no, location=no, resizable=yes, left=140, top=0');</script>");
                //Response.Redirect("frmJobStageStatusUpdate.aspx");
            }
        }

        protected void gvJobStageStatus_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            DataSet ds = objJobStage.SearchJobStatusList((string)Session["DocForm"], (string)Session["To"], (string)Session["Importer"], (string)Session["jno"], (string)Session["Stage"], (string)Session["Status"]);
            gvJobStageStatus.DataSource = ds;
            gvJobStageStatus.DataBind();
            gvJobStageStatus.PageIndex = e.NewPageIndex;
            gvJobStageStatus.DataBind();
        }

        protected void btnExit_Click(object sender, EventArgs e)
        {
            Response.Redirect("HomePage.aspx");
        }

    }
}