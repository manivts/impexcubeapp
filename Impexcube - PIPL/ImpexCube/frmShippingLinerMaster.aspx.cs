using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;

namespace ImpexCube
{
    public partial class frmShippingLinerMaster : System.Web.UI.Page
    {
        VTS.ImpexCube.Business.ShippingLineMasterBL objShipingMaster = new VTS.ImpexCube.Business.ShippingLineMasterBL();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack == false)
            {
                GridShippingMaster();
                btnSave.Visible = true;
                btnUpdate.Visible = false;
            }
        }

        private void GridShippingMaster()
        {
            DataSet ds = objShipingMaster.SelectShipingMaster();
            if (ds.Tables["shipper"].Rows.Count != 0)
            {
                //for (int i = 0; i < ds.Tables["shipper"].Rows.Count - 1; i++)
                //{
                //    DataRowView rw = ds.Tables["shipper"].DefaultView[i];
                //    string id = rw["Id"].ToString();
                //    string name = rw["shipper"].ToString();
                //    string address = rw["Address"].ToString();
                //    string contact = rw["Contact"].ToString();
                //    string email = rw["Email"].ToString();
                //    string favor = rw["Favor"].ToString();

                //    gvShipperMaster.Rows[i].Cells[1].Visible = false;
                //    gvShipperMaster.Rows[i].Cells[1].Text = id;
                //    gvShipperMaster.Rows[i].Cells[2].Text = name;
                //    gvShipperMaster.Rows[i].Cells[3].Text = address;
                //    gvShipperMaster.Rows[i].Cells[4].Text = contact;
                //    gvShipperMaster.Rows[i].Cells[5].Text = email;
                //    gvShipperMaster.Rows[i].Cells[6].Text = favor;


                //}
                gvShipperMaster.DataSource = ds;
                gvShipperMaster.DataBind();
            }
            else
            {
                gvShipperMaster.DataSource = null;
                gvShipperMaster.DataBind();
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            int result = new int();
            string shipper = txtShipperName.Text;
            string address = txtAddress.Text;
            string contact = txtContactPerson.Text;
            string email = txtEmail.Text;
            string favor = txtInfavor.Text;
            result =objShipingMaster.InsertShipingMaster(shipper, address, contact, email, favor);
            if (result == 1)
            {
                GridShippingMaster();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "redirect", "alert('Saved Successfully'); window.location.href='frmShippingLinerMaster.aspx';", true);
            }
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            int result = new int();
            int id = Convert.ToInt32(Session["Id"]);
            string shipper = txtShipperName.Text;
            string address = txtAddress.Text;
            string contact = txtContactPerson.Text;
            string email = txtEmail.Text;
            string favor = txtInfavor.Text;
            result = objShipingMaster.UpdateShipingMaster(id, shipper, address, contact, email, favor);
            if (result == 1)
            {
                GridShippingMaster();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "redirect", "alert('Updated Successfully'); window.location.href='frmShippingLinerMaster.aspx';", true);
            }
        }

        protected void gvShipperMaster_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnSave.Visible = false;
            btnUpdate.Visible = true;
            if (gvShipperMaster.SelectedRow.Cells[2].Text != null)
            {
                Session["Id"] = gvShipperMaster.SelectedRow.Cells[1].Text.ToString();
                txtShipperName.Text = gvShipperMaster.SelectedRow.Cells[2].Text.ToString();
                txtAddress.Text = gvShipperMaster.SelectedRow.Cells[3].Text.ToString();
                txtContactPerson.Text = gvShipperMaster.SelectedRow.Cells[4].Text.ToString();
                txtEmail.Text = gvShipperMaster.SelectedRow.Cells[5].Text.ToString();
                txtInfavor.Text = gvShipperMaster.SelectedRow.Cells[6].Text.ToString();
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "redirect", " window.location.href='frmShippingLinerMaster.aspx';", true);
        }

    }
}