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
    public partial class frmPortMaster : System.Web.UI.Page
    {
        string strconn = (string)ConfigurationManager.AppSettings["ConnectionDashboard"];

        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack == false)
            {
                DropDown();
                //Gridload();
                btnUpdate.Visible = false;
            }
        }

        private void DropDown()
        {

            SqlConnection CON = new SqlConnection(strconn);
            CON.Open();
            string QUERY1 = "SELECT CountryName,CountryCode FROM M_Country";
            SqlDataAdapter SD = new SqlDataAdapter(QUERY1, CON);
            DataSet DS = new DataSet();
            SD.Fill(DS, "DATA");
            ddlCountry.DataSource = DS;
            ddlCountry.DataTextField = "CountryName";
            ddlCountry.DataValueField = "CountryCode";
            ddlCountry.DataBind();
            CON.Close();
            ddlCountry.Items.Insert(0, new ListItem("~Select~", "0"));
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            SqlConnection CON = new SqlConnection(strconn);
            CON.Open();
            string Query = "INSERT INTO  M_Port( PortCode, PortName, CountryCode, UneceCode)VALUES('" + txtPortCode.Text + "','" + txtPortName.Text + "','"+ddlCountry.SelectedValue+"','"+txtUNECE.Text+"')";
            SqlCommand CMD3 = new SqlCommand(Query, CON);
            int Result = CMD3.ExecuteNonQuery();
            btnNew.Visible = false;
            CON.Close();
            if (Result == 1)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "redirect", "alert('Saved Successfully'); window.location.href='frmPortMaster.aspx';", true);
            }
        }

        private void Clear()
        {
            txtPortCode.Text = "";
            txtPortName.Text = "";
            txtUNECE.Text = "";
        }

        protected void gvPort_SelectedIndexChanged(object sender, EventArgs e)
        {
            hdnPortID.Value = gvPort.SelectedRow.Cells[1].Text;
            SqlConnection CON = new SqlConnection(strconn);
            CON.Open();
            string QUERY1 = "SELECT * FROM M_Port where PortId ='" + hdnPortID.Value + "' ";
            SqlDataAdapter SD = new SqlDataAdapter(QUERY1, CON);
            DataSet DS = new DataSet();
            SD.Fill(DS, "DATA");
            DataRowView dr = DS.Tables["DATA"].DefaultView[0];
            txtPortCode.Text = dr["PortCode"].ToString();
            txtPortName.Text = dr["PortName"].ToString();
            txtUNECE.Text = dr["UneceCode"].ToString();
            ddlCountry.SelectedValue = dr["CountryCode"].ToString();
            btnSave.Visible = false;
            btnNew.Visible = true;
            btnUpdate.Visible = true;
            btnDiscard.Visible = true;
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            SqlConnection CON = new SqlConnection(strconn);
            CON.Open();
            string QUERY8 = " UPDATE M_Port SET PortCode='" + txtPortCode.Text + "',PortName='" + txtPortName.Text + "',CountryCode='" + ddlCountry.SelectedValue + "',UneceCode='" + txtUNECE.Text + "'  WHERE PortId='" + hdnPortID.Value + "'";
            SqlCommand CMD5 = new SqlCommand(QUERY8, CON);
            btnNew.Visible = true;
            int Result = CMD5.ExecuteNonQuery();
            CON.Close();
            if (Result == 1)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "redirect", "alert('Updated Successfully'); window.location.href='frmPortMaster.aspx';", true);
            }
        }

        protected void ddlCountry_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtPortCode.Enabled = true;
            txtPortName.Enabled = true;
            txtUNECE.Enabled = true;
            EachCountry();
        }

        private void EachCountry()
        {
            gvPort.DataBind();
            Session["query"] = string.Empty;
            string QUERY1;
            SqlConnection CON = new SqlConnection(strconn);
            QUERY1 = "SELECT PortId, PortCode, PortName, CountryCode, UneceCode FROM M_Port where countrycode='" + ddlCountry.SelectedValue + "'";
            Session["query"] = QUERY1;
            SqlDataAdapter SD = new SqlDataAdapter(QUERY1, CON);
            DataSet DS = new DataSet();
            SD.Fill(DS, "DATA");
            if (DS.Tables["DATA"].Rows.Count != 0)
            {
                gvPort.DataSource = DS;
                gvPort.DataBind();
            }
            else
            {
                gvPort.DataSource = null;
                gvPort.DataBind();
            }
        }

        protected void btnDiscard_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/HomePage.aspx");
        }

        protected void btnNew_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/frmPortMaster.aspx");
        }


    }
}