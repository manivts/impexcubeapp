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
    public partial class frmCustomMaster : System.Web.UI.Page
    {
        string strconn = (string)ConfigurationManager.AppSettings["ConnectionDashboard"];
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack == false)
            {
                hdnCustomID.Value = "";
                Gridload();
                btnUpdate.Visible = false;
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            SqlConnection CON = new SqlConnection(strconn);
            CON.Open();
            string Query = "INSERT INTO M_Custom(Custom,Branch)VALUES('" + txtCustom.Text + "','" + txtBranch.Text + "')";
            SqlCommand CMD3 = new SqlCommand(Query, CON);
            int Result = CMD3.ExecuteNonQuery();
            btnNew.Visible = false;
            CON.Close();
            if (Result == 1)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "redirect", "alert('Saved Successfully'); window.location.href='frmCustomMaster.aspx';", true);
                clear();
                Gridload();
            }
        }

        private void clear()
        {
            txtBranch.Text = "";
            txtCustom.Text = "";
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            SqlConnection CON = new SqlConnection(strconn);
            CON.Open();
            string QUERY8 = " UPDATE M_Custom SET Custom='" + txtCustom.Text + "',Branch='" + txtBranch.Text + "'  WHERE TransId='" + hdnCustomID.Value +"'";
            SqlCommand CMD5 = new SqlCommand(QUERY8, CON);
            btnNew.Visible = true;
            int Result = CMD5.ExecuteNonQuery();
            CON.Close();
            
            if (Result == 1)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "redirect", "alert('Updated Successfully'); window.location.href='frmCustomMaster.aspx';", true);
            }
        }

        private void Gridload()
        {
            gvCustom.DataBind();
            Session["query"] = string.Empty;
            string QUERY1;
            SqlConnection CON = new SqlConnection(strconn);
            QUERY1 = "SELECT TransId,Custom,Branch FROM M_Custom";
            Session["query"] = QUERY1;
            SqlDataAdapter SD = new SqlDataAdapter(QUERY1, CON);
            DataSet DS = new DataSet();
            SD.Fill(DS, "DATA");
            if (DS.Tables["DATA"].Rows.Count != 0)
            {
                gvCustom.DataSource = DS;
                gvCustom.DataBind();
            }
            else
            {
                gvCustom.DataSource = null;
                gvCustom.DataBind();
            }
        }

        protected void gvCustom_SelectedIndexChanged(object sender, EventArgs e)
        {
           hdnCustomID.Value = gvCustom.SelectedRow.Cells[1].Text;
            SqlConnection CON = new SqlConnection(strconn);
            CON.Open();
            string QUERY1 = "SELECT * FROM M_Custom where TransId ='" + hdnCustomID.Value + "' ";
            SqlDataAdapter SD = new SqlDataAdapter(QUERY1, CON);
            DataSet DS = new DataSet();
            SD.Fill(DS, "DATA");
            DataRowView dr = DS.Tables["DATA"].DefaultView[0];
            txtCustom.Text = dr["Custom"].ToString();
            txtBranch.Text = dr["Branch"].ToString();
            btnSave.Visible = false;
            btnNew.Visible = true;
            btnUpdate.Visible = true;
            btnDiscard.Visible = true;
        }

        protected void btnDiscard_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/HomePage.aspx");
        }

        protected void btnNew_Click(object sender, EventArgs e)
        {
            clear();
        }

    }
}