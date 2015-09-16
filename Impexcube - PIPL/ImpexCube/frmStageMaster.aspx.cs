using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace ImpexCube
{
    public partial class frmStageMaster : System.Web.UI.Page
    {
        VTS.ImpexCube.Business.StageMasterBL objStage = new VTS.ImpexCube.Business.StageMasterBL();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack == false)
            {
                Session["mode"] = string.Empty;
                BindGridStage();
                GetStageId();
                Session["mode"] = string.Empty;
                btnSave.Text = "Save";
            }
        }

        private void GetStageId()
        {
            DataSet ds = objStage.SelectStageId();
            if (ds.Tables["stage"].Rows.Count != 0)
            {
                
                DataRowView dr = ds.Tables["stage"].DefaultView[0];
                string stageid = dr["StageId"].ToString();
                int Id = Convert.ToInt32(stageid) + 1;
                txtStageId.Text = Id.ToString();
            }
        }

        private void BindGridStage()
        {
            
            DataSet ds = objStage.SelectStage();

          

            if (ds.Tables["stage"].Rows.Count != 0)
            {
                
                gvStageDetails.DataSource = ds;
                gvStageDetails.DataBind();
            }
            else
            {
                gvStageDetails.DataSource = null;
                gvStageDetails.DataBind();
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            
            int result = new int();
            
            try
            {
                string date = DateTime.Now.ToString("dd/MM/yyyy");
                if ((string)Session["mode"] == "")
                {
                   
                    result = objStage.InsertStageDetails(txtStageName.Text, (string)Session["UserName"], date, (string)Session["UserName"], date);
                    if (result == 1)
                    {
                        BindGridStage();
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "redirect", "alert('Saved Successfully'); window.location.href='frmStageMaster.aspx';", true);
                    }
                }
                if ((string)Session["mode"] == "Edit")
                {
                    int id = Convert.ToInt32(Session["Id"]);
                    result = objStage.UpdateStageDetails( id, txtStageName.Text, (string)Session["UserName"], date);
                    if (result == 1)
                    {
                        BindGridStage();
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "redirect", "alert('Updated Successfully'); window.location.href='frmStageMaster.aspx';", true);
                    }
                }
            }
            catch (Exception ex)
            {
                string Msg = ex.Message.ToString();
            }            
        }

        protected void gvStageDetails_SelectedIndexChanged(object sender, EventArgs e)
        {
            Session["mode"] = "Edit";
            btnSave.Text = "Update";
            Session["Id"] = Convert.ToInt32(gvStageDetails.SelectedRow.Cells[1].Text);
            txtStageId.Text = gvStageDetails.SelectedRow.Cells[1].Text;
            txtStageName.Text = gvStageDetails.SelectedRow.Cells[2].Text;
        }

        protected void gvStageDetails_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

        }

        protected void btnNew_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/frmStageMaster.aspx");
        }

        protected void btnExit_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/HomePage.aspx");
        }

        
    }
}