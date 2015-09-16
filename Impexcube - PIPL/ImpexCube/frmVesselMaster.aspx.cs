using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.IO;

namespace ImpexCube
{
    public partial class frmVesselMaster : System.Web.UI.Page
    {
        string strconn = (string)ConfigurationManager.AppSettings["ConnectionDashboard"];
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack == false)
            {
                //UnitBind();
                Gridload();
                btnNew.Visible = true;
                btnUpdate.Visible = false;
                btnSave.Visible = true;
                btnDiscard.Visible = true;
            }
        }
        private void Gridload()
        {
            gvVesselMaster.DataBind();
            Session["query"] = string.Empty;
            string QUERY1;
            SqlConnection CON = new SqlConnection(strconn);
            QUERY1 = "SELECT TransId,VesselCode,VesselName FROM M_VesselMaster";
            Session["query"] = QUERY1;
            SqlDataAdapter SD = new SqlDataAdapter(QUERY1, CON);
            DataSet DS = new DataSet();
            SD.Fill(DS, "DATA");
            if (DS.Tables["DATA"].Rows.Count != 0)
            {
                gvVesselMaster.DataSource = DS;
                gvVesselMaster.DataBind();
            }
            else
            {
                gvVesselMaster.DataSource = null;
                gvVesselMaster.DataBind();
            }
        }
        private void Textclear()
        {
            txtVesselcode.Text = "";
            txtVesselName.Text = "";
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            string VesselCode = txtVesselcode.Text;
            string VesselName = txtVesselName.Text;
            SqlConnection CON = new SqlConnection(strconn);
            CON.Open();
            string QUERY6 = "INSERT INTO M_VesselMaster(VesselCode,VesselName)VALUES('" + VesselCode + "','" + VesselName + "')";
            SqlCommand CMD3 = new SqlCommand(QUERY6, CON);
            int Result = CMD3.ExecuteNonQuery();
            CON.Close();
            if (Result == 1)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "redirect", "alert('Saved Successfully'); window.location.href='frmVesselMaster.aspx';", true);
            }
            Textclear();
            Gridload();
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            
            SqlConnection CON = new SqlConnection(strconn);
            CON.Open();
            string QUERY8 = " UPDATE M_VesselMaster SET VesselCode='" + txtVesselcode.Text + "',VesselName = '" + txtVesselName.Text + "'  WHERE TransId ='" + (string)Session["TransId"] + "'";
            SqlCommand CMD5 = new SqlCommand(QUERY8, CON);
            btnNew.Visible = true;
            int Result = CMD5.ExecuteNonQuery();
            CON.Close();
            Textclear();
            Gridload();
            if (Result == 1)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "redirect", "alert('Updated Successfully'); window.location.href='frmVesselMaster.aspx';", true);
            }

        }

        protected void gvVesselMaster_SelectedIndexChanged(object sender, EventArgs e)
        {
            Session["TransId"] = gvVesselMaster.SelectedRow.Cells[1].Text;
            SqlConnection CON = new SqlConnection(strconn);
            CON.Open();
            string QUERY1 = "SELECT * FROM M_VesselMaster where TransId ='" + (string)Session["TransId"] + "' ";
            SqlDataAdapter SD = new SqlDataAdapter(QUERY1, CON);
            DataSet DS = new DataSet();
            SD.Fill(DS, "DATA");
            DataRowView dr = DS.Tables["DATA"].DefaultView[0];
            txtVesselcode.Text = dr["VesselCode"].ToString();
            txtVesselName.Text = dr["VesselName"].ToString();
            btnSave.Visible = false;
            btnNew.Visible = true;
            btnUpdate.Visible = true;
            btnDiscard.Visible = true;
        }

        protected void btnNew_Click(object sender, EventArgs e)
        {
            Textclear();
            Response.Redirect("~/frmVesselMaster.aspx");
        }

       
        protected void btnDiscard_Click1(object sender, EventArgs e)
        {
            Response.Redirect("~/HomePage.aspx");
        }
    }
}