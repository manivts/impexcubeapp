using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

namespace ImpexCube
{
    public partial class frmSaptaNotification : System.Web.UI.Page
    {
        string strconn = (string)ConfigurationManager.AppSettings["ConnectionDashboard"];
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
            Gridload();
            }
        }

        protected void btnsave_Click(object sender, EventArgs e)
        {
            string SaptaDescription = txtSaptaDesc.Text;
            string Notification = txtNotification.Text;
            string Serialno=txtserialno.Text;
            string DutyRate=txtdutyrate.Text;
            SqlConnection CON = new SqlConnection(strconn);
            CON.Open();
            string QUERY6 = "INSERT INTO M_SAPTANotification(Description,Notification,SerialNo,DutyRate)VALUES('" + SaptaDescription + "','" + Notification + "','" + Serialno + "','" + DutyRate + "')";
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

        private void Textclear()
        {
            txtSaptaDesc.Text = "";
            txtNotification.Text = "";
            txtserialno.Text = "";
            txtdutyrate.Text = "";
        }

        private void Gridload()
        {
            gvsaptaNotification.DataBind();
            Session["query"] = string.Empty;
            string QUERY1;
            SqlConnection CON = new SqlConnection(strconn);
            QUERY1 = "SELECT TransId,Description,Notification,SerialNo,DutyRate FROM M_SAPTANotification";
            Session["query"] = QUERY1;
            SqlDataAdapter SD = new SqlDataAdapter(QUERY1, CON);
            DataSet DS = new DataSet();
            SD.Fill(DS, "DATA");
            if (DS.Tables["DATA"].Rows.Count != 0)
            {
                gvsaptaNotification.DataSource = DS;
                gvsaptaNotification.DataBind();
            }
            else
            {
                gvsaptaNotification.DataSource = null;
                gvsaptaNotification.DataBind();
            }
        }

        protected void btnupdate_Click(object sender, EventArgs e)
        {
           
            SqlConnection CON = new SqlConnection(strconn);
            CON.Open();
            string QUERY8 = " UPDATE M_SAPTANotification SET Description='" + txtSaptaDesc.Text + "',Notification = '" + txtNotification.Text + "',DutyRate='" + txtdutyrate.Text + "',SerialNo = '" + txtserialno.Text + "'  WHERE TransId ='" + (string)Session["TransId"] + "'";
            SqlCommand CMD5 = new SqlCommand(QUERY8, CON);
            btnsave.Visible = true;
            int Result = CMD5.ExecuteNonQuery();
            CON.Close();
            Textclear();
            Gridload();
            if (Result == 1)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "redirect", "alert('Updated Successfully'); window.location.href='frmSaptaNotification.aspx';", true);
            }
        }

        protected void gvsaptaNotification_SelectedIndexChanged(object sender, EventArgs e)
        {
            Session["TransId"] = gvsaptaNotification.SelectedRow.Cells[1].Text;
            SqlConnection CON = new SqlConnection(strconn);
            CON.Open();
            string QUERY1 = "SELECT * FROM M_SAPTANotification where TransId ='" + (string)Session["TransId"] + "' ";
            SqlDataAdapter SD = new SqlDataAdapter(QUERY1, CON);
            DataSet DS = new DataSet();
            SD.Fill(DS, "DATA");
            DataRowView dr = DS.Tables["DATA"].DefaultView[0];
            txtSaptaDesc.Text = dr["Description"].ToString();
            txtNotification.Text = dr["Notification"].ToString();
            txtserialno.Text = dr["SerialNo"].ToString();
            txtdutyrate.Text = dr["DutyRate"].ToString();
            btnsave.Visible = false;            
            btnupdate.Visible = true;            
        }
    }
}