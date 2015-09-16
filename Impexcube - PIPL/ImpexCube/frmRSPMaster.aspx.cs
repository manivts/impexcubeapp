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
    public partial class frmRSPMaster : System.Web.UI.Page
    {
        string strconn = (string)ConfigurationManager.AppSettings["ConnectionDashboard"];
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
               Gridload();
                btnupdate.Visible = false;
            }
        }

        protected void btnsave_Click(object sender, EventArgs e)
        {
            string CETH = txtceth.Text;
            string EFFDate = txteffdate.Text;
            string EndDate = txtenddate.Text;
            string RTA = txtrta.Text;
            SqlConnection CON = new SqlConnection(strconn);
            CON.Open();
            
            string QUERY1 = "SELECT CETH,EffDt,EndDT,ABETRTA FROM M_RSP_Duty where CETH='"+ CETH +"'";            
            SqlDataAdapter SD = new SqlDataAdapter(QUERY1, CON);
            DataSet DS = new DataSet();
            SD.Fill(DS, "DATA");
            if (DS.Tables["DATA"].Rows.Count == 0)
            {
                string QUERY6 = "INSERT INTO M_RSP_Duty(CETH,EffDt,EndDT,ABETRTA)VALUES('" + CETH + "','" + EFFDate + "','" + EndDate + "','" + RTA + "')";
                SqlCommand CMD3 = new SqlCommand(QUERY6, CON);
                int Result = CMD3.ExecuteNonQuery();
                CON.Close();
                if (Result == 1)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "redirect", "alert('Saved Successfully'); window.location.href='frmRSPMaster.aspx';", true);
                }
                Textclear();
                Gridload();
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "JavaScript", "alert('CETH already Exists');", true);
                txtceth.Text = "";
            }
        }

        private void Textclear()
        {
            txtceth.Text = "";
            txteffdate.Text = "";
            txtenddate.Text = "";
            txtrta.Text = "";
        }

        private void Gridload()
        {
            gvrspmaster.DataBind();
            Session["query"] = string.Empty;
            string QUERY1;
            SqlConnection CON = new SqlConnection(strconn);
            QUERY1 = "SELECT TOP(10) CETH,EffDt,EndDT,ABETRTA FROM M_RSP_Duty";
            Session["query"] = QUERY1;
            SqlDataAdapter SD = new SqlDataAdapter(QUERY1, CON);
            DataSet DS = new DataSet();
            SD.Fill(DS, "DATA");
            if (DS.Tables["DATA"].Rows.Count != 0)
            {
                gvrspmaster.DataSource = DS;
                gvrspmaster.DataBind();
            }
            else
            {
                gvrspmaster.DataSource = null;
                gvrspmaster.DataBind();
            }
        }
        protected void btnupdate_Click(object sender, EventArgs e)
        {
            SqlConnection CON = new SqlConnection(strconn);
            CON.Open();
            string QUERY8 = " UPDATE M_RSP_Duty SET CETH='" + txtceth.Text + "',EffDt = '" + txteffdate.Text + "',EndDT='" + txtenddate.Text + "',ABETRTA = '" + txtrta.Text + "'  WHERE  CETH='" + txtceth.Text + "'";
            SqlCommand CMD5 = new SqlCommand(QUERY8, CON);
            btnsave.Visible = true;
            int Result = CMD5.ExecuteNonQuery();
            CON.Close();
            Textclear();
            Gridload();
            if (Result == 1)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "redirect", "alert('Updated Successfully'); window.location.href='frmRSPMaster.aspx';", true);
            }
        }

        protected void gvrspmaster_SelectedIndexChanged(object sender, EventArgs e)
        {
            Session["ceth"] = gvrspmaster.SelectedRow.Cells[1].Text;
            SqlConnection CON = new SqlConnection(strconn);
            CON.Open();
            string QUERY1 = "SELECT * FROM M_RSP_Duty where CETH ='" + (string)Session["ceth"] + "' ";
            SqlDataAdapter SD = new SqlDataAdapter(QUERY1, CON);
            DataSet DS = new DataSet();
            SD.Fill(DS, "DATA");
            DataRowView dr = DS.Tables["DATA"].DefaultView[0];
            txtceth.Text = dr["CETH"].ToString();
            txteffdate.Text = dr["EffDt"].ToString();
            txtenddate.Text = dr["EndDT"].ToString();
            txtrta.Text = dr["ABETRTA"].ToString();
            btnsave.Visible = false;
            btnupdate.Visible = true; 
        }
    }
}