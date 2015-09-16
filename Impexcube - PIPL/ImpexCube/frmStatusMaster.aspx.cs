using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace ImpexCube
{
    public partial class frmStatusMaster : System.Web.UI.Page
    {
        VTS.ImpexCube.Business.StatusMasterBL objStatus = new VTS.ImpexCube.Business.StatusMasterBL();
        string strconn = (string)ConfigurationManager.AppSettings["ConnectionDashboard"];

        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack == false)
            {
                BindGridStatus();
                GetStatusId();
                DropStage();
                Session["mode"] = string.Empty;
                btnSave.Text = "Save";
            }
        }

        private void DropStage()
        {
            DataSet ds = objStatus.SelectStage();
            if (ds.Tables["stage"].Rows.Count != 0)
            {
                ddlStage.DataSource = ds;
                ddlStage.DataTextField = "StageName";
                ddlStage.DataValueField = "StageId";
                ddlStage.DataBind();
                ddlStage.Items.Insert(0, new ListItem("-Select-", "0"));
            }
        }

        private void GetStatusId()
        {
            DataSet ds = objStatus.SelectStatusId();
            if (ds.Tables["status"].Rows.Count != 0)
            {
                DataRowView dr = ds.Tables["status"].DefaultView[0];
                string statusid = dr["Id"].ToString();
                int Id = Convert.ToInt32(statusid) + 1;
                txtStatusId.Text = Id.ToString();
            }
        }

        private void BindGridStatus()
        {
            Session["mode"] = string.Empty;
            DataSet ds = objStatus.SelectStatusDetails();
            if (ds.Tables["status"].Rows.Count != 0)
            {
                gvStatusDetails.DataSource = ds;
                gvStatusDetails.DataBind();
            }
            else
            {
                gvStatusDetails.DataSource = null;
                gvStatusDetails.DataBind();
            }
        }

        protected void gvStatusDetails_SelectedIndexChanged(object sender, EventArgs e)
        {
            Session["mode"] = "Edit";
            btnSave.Text = "Update";
            Session["Id"] = gvStatusDetails.SelectedRow.Cells[1].Text;
            txtStatusId.Text = gvStatusDetails.SelectedRow.Cells[1].Text;
            ddlStage.SelectedIndex = ddlStage.Items.IndexOf(ddlStage.Items.FindByText(gvStatusDetails.SelectedRow.Cells[2].Text));
            string status = gvStatusDetails.SelectedRow.Cells[3].Text;
            if (status == "" || status == "&nbsp;")
            {
                txtStatusName.Text = string.Empty;
            }
            else
            {
                txtStatusName.Text = status;
            }
            string Communication = gvStatusDetails.SelectedRow.Cells[4].Text;
            if (Communication == "Internal,External,Customer,")
            {
                chkInternal.Checked = true;
                chkExternal.Checked = true;
                chkCustomer.Checked = true;
            }
            else if (Communication == "Internal,External,")
            {
                chkInternal.Checked = true;
                chkExternal.Checked = true;
                chkCustomer.Checked = false;
            }
            else if (Communication == "Internal,Customer,")
            {
                chkInternal.Checked = true;
                chkExternal.Checked = false;
                chkCustomer.Checked = true;
            }
            else if (Communication == "External,Customer,")
            {
                chkInternal.Checked = false;
                chkExternal.Checked = true;
                chkCustomer.Checked = true;
            }

            else if (Communication == "Internal,")
            {
                chkInternal.Checked = true;
                chkExternal.Checked = false;
                chkCustomer.Checked = false;
            }
            else if (Communication == "External,")
            {
                chkInternal.Checked = false;
                chkExternal.Checked = true;
                chkCustomer.Checked = false;
            }
            else if (Communication == "Customer,")
            {
                chkInternal.Checked = false;
                chkExternal.Checked = false;
                chkCustomer.Checked = true;
            }
            try
            {
                SqlConnection CON = new SqlConnection(strconn);
                CON.Open();
                string QUERY1 = "SELECT * FROM T_StageStatus where Id ='" + (string)Session["Id"] + "' ";
                SqlDataAdapter SD = new SqlDataAdapter(QUERY1, CON);
                DataSet DS = new DataSet();
                SD.Fill(DS, "DATA");
                if (DS.Tables["DATA"].Rows.Count != 0)
                {
                    DataRowView dr = DS.Tables["DATA"].DefaultView[0];
                    txtSubject.Text = dr.Row["MailSubject"].ToString();
                    txtComment.Text = dr.Row["MailComment"].ToString();
                    CON.Close();
                }
            }
            catch (Exception ex)
            {
                string message = ex.Message.ToString();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Database Error : " + message + "');", true);
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            int result = new int();
            try
            {
                string date = DateTime.Now.ToString("dd/MM/yyyy");
                string status = txtStatusName.Text;
                string subject = txtSubject.Text;
                string comment = txtComment.Text;
                string communication = string.Empty;
                int stageid = Convert.ToInt32(ddlStage.SelectedValue);
                string stagename = ddlStage.SelectedItem.Text;
                if (chkInternal.Checked == true)
                {
                    communication += chkInternal.Text + ",";
                }
                if (chkExternal.Checked == true)
                {
                    communication += chkExternal.Text + ",";
                }
                if (chkCustomer.Checked == true)
                {
                     communication += chkCustomer.Text + ",";
                }
                if (communication != "")
                {
                    communication.TrimEnd(',');
                }                
                if ((string)Session["mode"] == "")
                {
                    result = objStatus.InsertStatusDetails(status, stageid, communication, subject, comment, (string)Session["UserName"], date, (string)Session["UserName"], date, stagename);
                    if (result == 1)
                    {
                        BindGridStatus();
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "redirect", "alert('Saved Successfully'); window.location.href='frmStatusMaster.aspx';", true);
                    }
                }
                if ((string)Session["mode"] == "Edit")
                {
                    int id = Convert.ToInt32(Session["Id"]);
                    result = objStatus.UpdateStatusDetails(id, status, stageid, communication, subject, comment, (string)Session["UserName"], date, stagename);
                    if (result == 1)
                    {
                        BindGridStatus();
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "redirect", "alert('Updated Successfully'); window.location.href='frmStatusMaster.aspx';", true);
                    }
                }
            }
            catch (Exception ex)
            {
                string Msg = ex.Message.ToString();
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/HomePage.aspx");
        }

        protected void btnNew_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/frmStatusMaster.aspx");
        }

    }
}