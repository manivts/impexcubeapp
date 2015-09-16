using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using VTS.ImpexCube.Data;

namespace ImpexCube
{
    public partial class frmJobChangeApproval : System.Web.UI.Page
    {
        CommonDL objCommonDL = new CommonDL();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DropDownJobNo();
                txtRequestBy.Text = (string)Session["USER-NAME"];
            }
        }

        private void DropDownJobNo()
        {
            string quer = string.Empty;
            if (Request.QueryString["Mode"] == "Import")
            {
                quer = "Select Distinct JobNo from T_JobCreation ";
                chkImpExp.Text = "Import";
            }
            else if (Request.QueryString["Mode"] == "Export")
            {
                quer = "select Distinct JobNo from E_M_JobCreation";
                chkImpExp.Text = "Export";                
            }
            DataSet ds = new DataSet();
            ds = objCommonDL.GetDataSet(quer);
            if (ds.Tables["Table"].Rows.Count != 0)
            {
                ddlJobno.DataSource = ds;
                ddlJobno.DataTextField = "JobNo";
                ddlJobno.DataValueField = "JobNo";
                ddlJobno.DataBind();
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            string communication = string.Empty;
            string jobno = ddlJobno.SelectedValue;
            string requestby = txtRequestBy.Text;
            string reason = txtReason.Text;
            string remarks = txtRemarks.Text;
            if (chkJobCreation.Checked == true)
            {
                communication += chkJobCreation.Text + ",";
            }
            if (chkImpExp.Checked == true)
            {
                communication += chkImpExp.Text + ",";
            }
            if (chkShipment.Checked == true)
            {
                communication += chkShipment.Text + ",";
            }
            if (chkInvoice.Checked == true)
            {
                communication += chkInvoice.Text + ",";
            }
            if (chkProduct.Checked == true)
            {
                communication += chkProduct.Text + ",";
            }
            if (communication != "")
            {
                communication = communication.Remove(communication.Trim().Length - 1);
            }
            string date = DateTime.Now.ToString();

            try
            {
                string qry = "Insert Into T_JobChangeApproval(JobNo,RequestBy,Reason,Remarks,ChangesIn,CreatedBy,CreatedDate,ModifiedBy,ModifiedDate)" +
                    "Values ('" + jobno + "', '" + requestby + "', '" + reason + "','" + remarks + "','" + communication + "'," +
                    "'" + (string)Session["USER-NAME"] + "','" + date + "','" + (string)Session["USER-NAME"] + "','" + date + "')";
                int result = objCommonDL.ExecuteNonQuery(qry);
                if (result == 1)
                {
                    ClearValues();
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Saved successfully');", true);   
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Please wait');", true);   
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('" + ex.Message + "');", true);   
            }
        }

        private void ClearValues()
        {
            txtReason.Text = string.Empty;
            txtRemarks.Text = string.Empty;
            txtRequestBy.Text = string.Empty;
            chkImpExp.Checked = false;
            chkInvoice.Checked = false;
            chkProduct.Checked = false;
            chkShipment.Checked = false;
            chkJobCreation.Checked = false;            
        }
    }
}