using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using VTS.ImpexCube.Utlities;
using System.Configuration;

namespace ImpexCube.CRM
{
    public partial class frmEnquiry : System.Web.UI.Page
    {

        VTS.ImpexCube.Business.EnquiryBL objEnquiry = new VTS.ImpexCube.Business.EnquiryBL();
        string con = (string)ConfigurationManager.ConnectionStrings["Constr"].ConnectionString;
        #region for gobal declaration
        string air = string.Empty;
        string lcl = string.Empty;
        string feet20 = string.Empty;
        string feet40 = string.Empty;
        #endregion

        string keyname = "EnqNo";
        string cfsname = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack == false)
            {
                string fyear = (string)Session["FYear"];
                Session["key"] = "";

                Session["key"] = keyname + "/" + fyear + "/" + Convert.ToString(Utility.GetNextAutoNo(keyname, 0, Utility.GetConnectionString()));
                Session["Keycode"] = Convert.ToString(Utility.GetNextAutoNo(keyname, 0, Utility.GetConnectionString()));
                lblEnquiryNo.Text = (string)Session["Key"];
                pnlAir.Visible = false;
                pnl20Feet.Visible = false;
                pnl40Feet.Visible = false;
                pnlLcl.Visible = false;
                btnUpdate.Visible = false;

                gridload();
            }


        }

        public  void gridload()
        {
            DataSet ds = new DataSet();
            string query;
            query = "Select TransId,CustomerName,PhoneNo,Address,ModeOfEnquiry,Pol,Pod from M_Enquriy";
            SqlConnection sqlConn = new SqlConnection(con);
            sqlConn.Open();
            SqlDataAdapter da = new SqlDataAdapter(query, sqlConn);
            da.Fill(ds, "M_Enquiry");
            if (ds.Tables["M_Enquiry"].Rows.Count != 0)
            {
                grdEnquiry.DataSource = ds;
                grdEnquiry.DataBind();
            }
            else
            {
                grdEnquiry.DataSource = null;
                grdEnquiry.DataBind();
            }


        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (txtCustomername.Text != "")
            {
                if (ddlShipMode.SelectedValue != "~Select~")
                {
                    if (chkAir.Checked != false || chkLcl.Checked != false || chk20Feet.Checked != false || chk40Feet.Checked != false)
                    {
                        if (chkAir.Checked == true)
                        {
                            air = "1";
                        }
                        else if (chkAir.Checked == false)
                        {
                            air = "0";
                        }
                        if (chk20Feet.Checked == true)
                        {
                            feet20 = "1";
                        }
                        else if (chk20Feet.Checked == false)
                        {
                            feet20 = "0";
                        }
                        if (chk40Feet.Checked == true)
                        {
                            feet40 = "1";
                        }
                        else if (chk40Feet.Checked == false)
                        {
                            feet40 = "0";
                        }
                        if (chkLcl.Checked == true)
                        {
                            lcl = "1";
                        }
                        else if (chkLcl.Checked == false)
                        {
                            lcl = "0";
                        }

                        int Update = new int();
                        string key = (String)Session["key"];
                        int result = objEnquiry.InsertEnquiry(txtCustomername.Text, txtPhoneNo.Text, txtAddress.Text, txtEmail.Text, txtContactPerson.Text, drModeOfEnquiry.SelectedValue, txtRitcCode.Text, txtCommodity.Text, txtIfsCode.Text, txtPol.Text, txtPod.Text, air, lcl, feet20, feet40, txtAirQuantity.Text, drAirUom.SelectedValue, txtAirGrossWeight.Text, txtAirChargeableWeight.Text, txtAirVolume.Text, drContainerType.SelectedValue, txtContainerNos.Text, txtLclQuantity.Text, drLclUom.SelectedValue, txtLclGrossWeight.Text, txtLclChargeableWeight.Text, txtLclVolume.Text, txtFinDest.Text, txtClearance.Text, txtCutofdate.Text, chkLoctrans.Checked, ddlShipMode.SelectedValue, ddl40feetcont.SelectedValue, txtContNo.Text, ddlQntTypAir.SelectedValue, ddlQntTyp.SelectedValue, ddlCusTyp.SelectedValue, "0", key);
                        if (result == 1)
                        {                           
                            string keycode = (string)Session["Keycode"];
                            Update = Utility.UpdateAutoNo(keyname, Convert.ToInt32(keycode), Utility.GetConnectionString());

                            ScriptManager.RegisterStartupScript(this, this.GetType(), "redirect", "alert('Successfully Saved '); window.location.href='frmEnquiry.aspx';", true);
                        }
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Please Select the Shipment Type');", true);
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Please Select the Shipment Mode');", true);
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Please Enter the Customer Name');", true);
            }

        }

        protected void chkAir_CheckedChanged(object sender, EventArgs e)
        {
            if (chkAir.Checked == true)
            {
                pnlAir.Visible = true;
            }
            else
            {
                pnlAir.Visible = false;
            }
            if (chk20Feet.Checked == true)
            {
                pnl20Feet.Visible = true;
            }
            else
            {
                pnl20Feet.Visible = false;
            }
            if (chk40Feet.Checked == true)
            {
                pnl40Feet.Visible = true;
            }
            else
            {
                pnl40Feet.Visible = false;
            }
             
            if (chkLcl.Checked == true)
             {
                 pnlLcl.Visible = true;
             }
            else
            {
                pnlLcl.Visible = false;
            }
        }

        protected void chkFcl_CheckedChanged(object sender, EventArgs e)
        {
            
            if (chkAir.Checked == true)
            {
                pnlAir.Visible = true;
            }
            else
            {
                pnlAir.Visible = false;
            }
            if (chk20Feet.Checked == true)
            {
                pnl20Feet.Visible = true;
            }
            else
            {
                pnl20Feet.Visible = false;
            }
            if (chk40Feet.Checked == true)
            {
                pnl40Feet.Visible = true;
            }
            else
            {
                pnl40Feet.Visible = false;
            }

            if (chkLcl.Checked == true)
            {
                pnlLcl.Visible = true;
            }
            else
            {
                pnlLcl.Visible = false;
            }
        }

        protected void chkLcl_CheckedChanged(object sender, EventArgs e)
        {
           
            if (chkAir.Checked == true)
            {
                pnlAir.Visible = true;
            }
            else
            {
                pnlAir.Visible = false;
            }
            if (chk20Feet.Checked == true)
            {
                pnl20Feet.Visible = true;
            }
            else
            {
                pnl20Feet.Visible = false;
            }
            if (chk40Feet.Checked == true)
            {
                pnl40Feet.Visible = true;
            }
            else
            {
                pnl40Feet.Visible = false;
            }

            if (chkLcl.Checked == true)
            {
                pnlLcl.Visible = true;
            }
            else
            {
                pnlLcl.Visible = false;
            }

        }

        protected void grdEnquiry_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnUpdate.Visible = true;
            btnSave.Visible = false;
            string  custId = grdEnquiry.SelectedRow.Cells[1].Text;
            Session["ID"] = custId; 
            int Id = Convert.ToInt32(custId);
            DataSet ds = objEnquiry.SelectByCustomerID(Id);
            if (ds.Tables["customer"].Rows.Count != 0)
            {
                DataRowView rw = ds.Tables["customer"].DefaultView[0];
                txtCustomername.Text = rw["CustomerName"].ToString();
                txtPhoneNo.Text = rw["PhoneNo"].ToString();
                txtAddress.Text = rw["Address"].ToString();
                txtEmail.Text = rw["EmailId"].ToString();
                txtContactPerson.Text = rw["ContactPerson"].ToString();
                drModeOfEnquiry.SelectedValue = rw["ModeOfEnquiry"].ToString();
                txtRitcCode.Text = rw["SalesPerson"].ToString();
                txtCommodity.Text = rw["Commodity"].ToString();
                txtIfsCode.Text = rw["ReferredBy"].ToString();
                txtPol.Text = rw["Pol"].ToString();
                txtPod.Text = rw["Pod"].ToString();
                txtAirQuantity.Text = rw["AQuantity"].ToString();
                drAirUom.SelectedValue = rw["AUom"].ToString();
                txtAirGrossWeight.Text = rw["AGrossWeight"].ToString();
                txtAirChargeableWeight.Text = rw["AChargeableWeight"].ToString();
                txtAirVolume.Text = rw["AVolume"].ToString();
                drContainerType.SelectedValue = rw["FContainerType"].ToString();
                txtContainerNos.Text = rw["FContainerNos"].ToString();
                ddl40feetcont.SelectedValue = rw["ContTyp40"].ToString();
                txtContNo.Text = rw["ContNo"].ToString();
                txtLclQuantity.Text = rw["LQuantity"].ToString();
                drLclUom.SelectedValue = rw["LUom"].ToString();
                txtLclGrossWeight.Text = rw["LGrossWeight"].ToString();
                txtLclChargeableWeight.Text = rw["LChargeableWeight"].ToString();
                txtLclVolume.Text = rw["LVolume"].ToString();
                txtClearance.Text = rw["Clearance"].ToString();
                txtFinDest.Text = rw["FinalDest"].ToString();
                txtCutofdate.Text = rw["Cutofdate"].ToString();
                chkLoctrans.Checked = Convert.ToBoolean(rw["LoctransInclude"]);
                ddlShipMode.SelectedValue = rw["ShipmentMode"].ToString();
                ddlQntTyp.SelectedValue = rw["LType"].ToString();
                ddlQntTypAir.SelectedValue = rw["AType"].ToString();
                ddlCusTyp.SelectedValue = rw["CustTyp"].ToString();
                string eair = rw["Air"].ToString();
                string e20feet = rw["Feet20"].ToString();
                string e40feet = rw["Feet40"].ToString();
                string elcl = rw["Lcl"].ToString();

                 if (eair == "True")
                 {
                     chkAir.Checked = true;
                     pnlAir.Visible = true;
                 }
                 else if (eair == "False")
                 {
                     chkAir.Checked = false;
                     pnlAir.Visible = false;
                 }
                 if (e20feet == "True")
                 {
                     chk20Feet.Checked = true;
                     pnl20Feet.Visible = true;
                 }
                 else if (e20feet == "False")
                 {
                     chk20Feet.Checked = false;
                     pnl20Feet.Visible = false;
                 }
                 if (e40feet == "True")
                 {
                     chk40Feet.Checked = true;
                     pnl40Feet.Visible = true;
                 }
                 else if (e40feet == "False")
                 {
                     chk40Feet.Checked = false;
                     pnl40Feet.Visible = false;
                 }
                 if (elcl == "True")
                 {
                     chkLcl.Checked = true;
                     pnlLcl.Visible = true;
                 }
                 else if (elcl == "False")
                 {
                     chkLcl.Checked = false;
                     pnlLcl.Visible = false;
                 }

            }            
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            if (chkAir.Checked == true)
            {
                air = "1";
            }
            else if (chkAir.Checked == false)
            {
                air = "0";
            }
            if (chk20Feet.Checked == true)
            {
                feet20 = "1";
            }
            else if (chk20Feet.Checked == false)
            {
                feet20 = "0";
            }
            if (chk40Feet.Checked == true)
            {
                feet40 = "1";
            }
            else if (chk40Feet.Checked == false)
            {
                feet40 = "0";
            }
            if (chkLcl.Checked == true)
            {
                lcl = "1";
            }
            else if (chkLcl.Checked == false)
            {
                lcl = "0";
            }

            int id = Convert.ToInt32(Session["ID"]);
            int result = objEnquiry.UpdateEnquiry(txtCustomername.Text,
                txtPhoneNo.Text, txtAddress.Text, txtEmail.Text, txtContactPerson.Text, drModeOfEnquiry.SelectedValue, txtRitcCode.Text, txtCommodity.Text, txtIfsCode.Text, txtPol.Text, txtPod.Text, air, lcl, feet20, feet40, txtAirQuantity.Text, drAirUom.SelectedValue, txtAirGrossWeight.Text, txtAirChargeableWeight.Text, txtAirVolume.Text, drContainerType.SelectedValue, txtContainerNos.Text, txtLclQuantity.Text, drLclUom.SelectedValue, txtLclGrossWeight.Text, txtLclChargeableWeight.Text, txtLclVolume.Text, id, txtFinDest.Text, txtClearance.Text, txtCutofdate.Text, chkLoctrans.Checked, ddlShipMode.SelectedValue, ddl40feetcont.SelectedValue, txtContNo.Text, ddlQntTypAir.SelectedValue, ddlQntTyp.SelectedValue, ddlCusTyp.SelectedValue);
            if (result == 1)
            {

                ScriptManager.RegisterStartupScript(this, this.GetType(), "redirect", "alert('Successfully Update'); window.location.href='frmEnquiry.aspx';", true);
            }


        }

        protected void CheckBox1_CheckedChanged(object sender, EventArgs e)
        {

        }

        protected void chk20Feet_CheckedChanged(object sender, EventArgs e)
        {
            if (chkAir.Checked == true)
            {
                pnlAir.Visible = true;
            }
            else
            {
                pnlAir.Visible = false;
            }
            if (chk20Feet.Checked == true)
            {
                pnl20Feet.Visible = true;
            }
            else
            {
                pnl20Feet.Visible = false;
            }
            if (chk40Feet.Checked == true)
            {
                pnl40Feet.Visible = true;
            }
            else
            {
                pnl40Feet.Visible = false;
            }

            if (chkLcl.Checked == true)
            {
                pnlLcl.Visible = true;
            }
            else
            {
                pnlLcl.Visible = false;
            }
        }

        protected void chk40Feet_CheckedChanged(object sender, EventArgs e)
        {
            if (chkAir.Checked == true)
            {
                pnlAir.Visible = true;
            }
            else
            {
                pnlAir.Visible = false;
            }
            if (chk20Feet.Checked == true)
            {
                pnl20Feet.Visible = true;
            }
            else
            {
                pnl20Feet.Visible = false;
            }
            if (chk40Feet.Checked == true)
            {
                pnl40Feet.Visible = true;
            }
            else
            {
                pnl40Feet.Visible = false;
            }

            if (chkLcl.Checked == true)
            {
                pnlLcl.Visible = true;
            }
            else
            {
                pnlLcl.Visible = false;
            }
        }

        protected void grdEnquiry_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            DataSet ds = new DataSet();
            string query;
            query = "Select TransId,CustomerName,PhoneNo,Address,ModeOfEnquiry,Pol,Pod from M_Enquriy";
            SqlConnection sqlConn = new SqlConnection(con);
            sqlConn.Open();
            SqlDataAdapter da = new SqlDataAdapter(query, sqlConn);
            da.Fill(ds, "M_Enquiry");
            if (ds.Tables["M_Enquiry"].Rows.Count != 0)
            {
                grdEnquiry.DataSource = ds;
                grdEnquiry.DataBind();
                grdEnquiry.PageIndex = e.NewPageIndex;
                grdEnquiry.DataBind();
            }
            else
            {
                grdEnquiry.DataSource = null;
                grdEnquiry.DataBind();
            }

        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("frmEnquiry.aspx");
        }

    }
}