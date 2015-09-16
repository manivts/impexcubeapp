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
    public partial class frmAirLine : System.Web.UI.Page
    {
        string strconn = (string)ConfigurationManager.AppSettings["ConnectionDashboard"];
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack == false)
            {
                AirlineBind();
                Gridload();
                btnNew.Visible = true;
                btnUpdate.Visible = false;
                btnSave.Visible = true;
                btnDiscard.Visible = true;
            }
        }
        private void Gridload()
        {
            gvAirLine.DataBind();
            Session["query"] = string.Empty;
            string QUERY1;
            SqlConnection CON = new SqlConnection(strconn);
            QUERY1 = "SELECT distinct Airline,AirlineCode,AirlinePrefix FROM M_Airline";
            Session["query"] = QUERY1;
            SqlDataAdapter SD = new SqlDataAdapter(QUERY1, CON);
            DataSet DS = new DataSet();
            SD.Fill(DS, "DATA");
            if (DS.Tables["DATA"].Rows.Count != 0)
            {
                gvAirLine.DataSource = DS;
                gvAirLine.DataBind();
            }
            else
            {
                gvAirLine.DataSource = null;
                gvAirLine.DataBind();
            }


        }
        private void cleartext()
        {
            ddlAirLine.SelectedItem.Text = "~Select~";            
            txtPrefix.Text = "";
            txtAirlineCode.Text = "";           
        }
        private void AirlineBind()
        {

            SqlConnection CON = new SqlConnection(strconn);
            CON.Open();
            string QUERY1 = "SELECT Airline FROM M_Airline";
            SqlDataAdapter SD = new SqlDataAdapter(QUERY1, CON);
            DataSet DS = new DataSet();
            SD.Fill(DS, "DATA");
            ddlAirLine.DataSource = DS;
            ddlAirLine.DataTextField = "Airline";
            ddlAirLine.DataValueField = "Airline";
            ddlAirLine.DataBind();
            CON.Close();
            ddlAirLine.Items.Insert(0, new ListItem("~Select~", "0"));
        }
        private void CodeBind()
        {
            SqlConnection con = new SqlConnection(strconn);
            con.Open();
            SqlDataAdapter sdsa = new SqlDataAdapter("Select distinct AirlineCode,AirlinePrefix FROM M_Airline where Airline = '" + ddlAirLine.SelectedValue + "'", con);
            DataSet ds = new DataSet();
            sdsa.Fill(ds, "data");
            if (ds.Tables["data"].Rows.Count != 0)
            {
                DataRowView row = ds.Tables["data"].DefaultView[0];
                txtAirlineCode.Text = row["AirlineCode"].ToString();
                txtPrefix.Text = row["AirlinePrefix"].ToString();

            }

            con.Close();
            
        }

        protected void btnNew_Click(object sender, EventArgs e)
        {
            cleartext();
            btnSave.Visible = true;
            btnUpdate.Visible = false;
        }

        protected void btnDiscard_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/frmAirLine.aspx");
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            string Airline = ddlAirLine.SelectedItem.Text;
            string AirlineCode = txtAirlineCode.Text;
            string AirlinePrefix = txtPrefix.Text;           
            SqlConnection CON = new SqlConnection(strconn);
            CON.Open();
            string QUERY6 = "INSERT INTO M_Airline(Airline,AirlineCode,AirlinePrefix)VALUES('" + Airline + "','" + AirlineCode + "','" + AirlinePrefix + "')";
            SqlCommand CMD3 = new SqlCommand(QUERY6, CON);
            int Result = CMD3.ExecuteNonQuery();
            btnNew.Visible = false;
            CON.Close();
            //txtclear();
            if (Result == 1)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Added Sucessfully');", true);

            }
           
            cleartext();
            Gridload();
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            SqlConnection CON = new SqlConnection(strconn);
            CON.Open();
            string QUERY8 = " UPDATE M_Airline SET Airline='" + ddlAirLine.SelectedItem.Text + "',AirlineCode='" + txtAirlineCode.Text + "',AirlinePrefix = '" + txtPrefix.Text + "'  WHERE Airline='" + ddlAirLine.SelectedItem.Text + "'";
            SqlCommand CMD5 = new SqlCommand(QUERY8, CON);
            btnNew.Visible = true;
            int Result = CMD5.ExecuteNonQuery();
            CON.Close();
            cleartext();
            if (Result == 1)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Updated Sucessfully');", true);

            }
            Gridload();

        }      

        protected void gvAirLine_SelectedIndexChanged(object sender, EventArgs e)
        {
            ddlAirLine.SelectedItem.Text = gvAirLine.SelectedRow.Cells[1].Text;
            SqlConnection CON = new SqlConnection(strconn);
            CON.Open();
            string QUERY1 = "SELECT * FROM M_Airline where Airline ='" + ddlAirLine.SelectedItem.Text + "' ";
            SqlDataAdapter SD = new SqlDataAdapter(QUERY1, CON);
            DataSet DS = new DataSet();
            SD.Fill(DS, "DATA");
            DataRowView dr = DS.Tables["DATA"].DefaultView[0];
            ddlAirLine.SelectedItem.Text = dr["Airline"].ToString();
            txtAirlineCode.Text = dr["AirlineCode"].ToString();
            txtPrefix.Text = dr["AirlinePrefix"].ToString();            
            btnSave.Visible = false;
            btnNew.Visible = true;
            btnUpdate.Visible = true;
            btnDiscard.Visible = true;
        }

        protected void ddlAirLine_SelectedIndexChanged(object sender, EventArgs e)
        {
            CodeBind();
        }
    }
}