using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace ImpexCube
{
    public partial class frmCFSMaster : System.Web.UI.Page
    {
        VTS.ImpexCube.Business.CFSMasterBL objCFSMaster = new VTS.ImpexCube.Business.CFSMasterBL();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack == false)
            {
                GridCFSMaster();
                btnSave.Visible = true;
                btnUpdate.Visible = false;
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            int result = new int();
            string cfsname = txtCfsName.Text;
            string address = txtAddress.Text;
            string contact = txtContactPerson.Text;
            string email = txtEmail.Text;
            string favor = txtInfavor.Text;
            result = objCFSMaster.InsertCFSMaster(cfsname, address, contact, email, favor);
            if (result == 1)
            {
                GridCFSMaster();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "redirect", "alert('Saved Successfully'); window.location.href='frmCFSMaster.aspx';", true);
            }
        }

        private void GridCFSMaster()
        {
            DataSet ds = objCFSMaster.SelectCFSMaster();
            if (ds.Tables["cfs"].Rows.Count != 0)
            {               
                DataTable dt = ds.Tables["cfs"];
                //for (int i = 0; i < dt.Rows.Count - 1; i++)
                //{
                //    DataRowView rw = ds.Tables["cfs"].DefaultView[i];
                //    string id = rw["Id"].ToString();
                //    string name = rw["CFS"].ToString();
                //    string address = rw["Address"].ToString();
                //    string contact = rw["Contact"].ToString();
                //    string email = rw["Email"].ToString();
                //    string favor = rw["Favor"].ToString();

                //    gvCFSMaster.Rows[i].Cells[1].Visible = false;
                //    gvCFSMaster.Rows[i].Cells[1].Text = id;
                //    gvCFSMaster.Rows[i].Cells[2].Text = name;
                //    gvCFSMaster.Rows[i].Cells[3].Text = address;
                //    gvCFSMaster.Rows[i].Cells[4].Text = contact;
                //    gvCFSMaster.Rows[i].Cells[5].Text = email;
                //    gvCFSMaster.Rows[i].Cells[6].Text = favor;
                   

                //}
                gvCFSMaster.DataSource = ds;
                gvCFSMaster.DataBind();
            }
            else
            {
                gvCFSMaster.DataSource = null;
                gvCFSMaster.DataBind();
            }
        }

        protected void gvCFSMaster_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnSave.Visible = false;
            btnUpdate.Visible = true;
            if (gvCFSMaster.SelectedRow.Cells[2].Text != null)
            {
                Session["Id"] = gvCFSMaster.SelectedRow.Cells[1].Text.ToString();
                txtCfsName.Text = gvCFSMaster.SelectedRow.Cells[2].Text.ToString();
                txtAddress.Text = gvCFSMaster.SelectedRow.Cells[3].Text.ToString();
                txtContactPerson.Text = gvCFSMaster.SelectedRow.Cells[4].Text.ToString();
                txtEmail.Text = gvCFSMaster.SelectedRow.Cells[5].Text.ToString();
                txtInfavor.Text = gvCFSMaster.SelectedRow.Cells[6].Text.ToString();
            }

        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            int result = new int();
            int id=Convert.ToInt32(Session["Id"]);
            string cfsname = txtCfsName.Text;
            string address = txtAddress.Text;
            string contact = txtContactPerson.Text;
            string email = txtEmail.Text;
            string favor = txtInfavor.Text;
            result = objCFSMaster.UpdateCFSMaster(id,cfsname,address,contact,email,favor);
            if (result == 1)
            {
                GridCFSMaster();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "redirect", "alert('Updated Successfully'); window.location.href='frmCFSMaster.aspx';", true);
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "redirect", " window.location.href='frmCFSMaster.aspx';", true);
        }
    }
}