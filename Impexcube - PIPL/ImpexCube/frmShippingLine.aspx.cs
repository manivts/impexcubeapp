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
    public partial class frmShippingLine : System.Web.UI.Page
    {
        string strconn = (string)ConfigurationManager.AppSettings["ConnectionDashboard"];
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack == false)
            {
                UnitBind();                
                Gridload();
                btnNew.Visible = true;
                btnUpdate.Visible = false;
                btnSave.Visible = true;
                btnDiscard.Visible = true;
            }
        }
        private void Gridload()
        {
            gvShippingLine.DataBind();
            Session["query"] = string.Empty;
            string QUERY1;
            SqlConnection CON = new SqlConnection(strconn);
            QUERY1 = "SELECT distinct ShipperName,Address,ContactPerson,Email,Telephone,Fax FROM M_ShippingLineMaster";
            Session["query"] = QUERY1;
            SqlDataAdapter SD = new SqlDataAdapter(QUERY1, CON);
            DataSet DS = new DataSet();
            SD.Fill(DS, "DATA");
            if (DS.Tables["DATA"].Rows.Count != 0)
            {
                gvShippingLine.DataSource = DS;
                gvShippingLine.DataBind();
            }
            else
            {
                gvShippingLine.DataSource = null;
                gvShippingLine.DataBind();
            }
        }
        private void Textclear()
        {
            ddlShippingLine.SelectedItem.Text = "";
            txtAddress.Text = "";
            txtContactPerson.Text = "";
            txtEmail.Text = "";
            txtTelephone.Text = "";
            txtFax.Text = "";           
        }
        private void UnitBind()
        {

            SqlConnection CON = new SqlConnection(strconn);
            CON.Open();
            string QUERY1 = "SELECT ShipperName FROM M_ShippingLineMaster";
            SqlDataAdapter SD = new SqlDataAdapter(QUERY1, CON);
            DataSet DS = new DataSet();
            SD.Fill(DS, "DATA");
            ddlShippingLine.DataSource = DS;
            ddlShippingLine.DataTextField = "ShipperName";
            ddlShippingLine.DataValueField = "ShipperName";
            ddlShippingLine.DataBind();
            CON.Close();
            ddlShippingLine.Items.Insert(0, new ListItem("~Select~", "0"));
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {            
            string ShipperName = ddlShippingLine.SelectedItem.Text;
            string Address = txtAddress.Text;
            string ContactPerson = txtContactPerson.Text;
            string Email = txtEmail.Text;
            string Telephone = txtTelephone.Text;
            string Fax = txtFax.Text;            
            SqlConnection CON = new SqlConnection(strconn);
            CON.Open();
            string QUERY6 = "INSERT INTO M_ShippingLineMaster(ShipperName,Address,ContactPerson,Email,Telephone,Fax)VALUES('" + ShipperName + "','" + Address + "','" + ContactPerson + "','" + Email + "','" + Telephone + "','" + Fax + "')";
            SqlCommand CMD3 = new SqlCommand(QUERY6, CON);
            int Result = CMD3.ExecuteNonQuery();            
            CON.Close();
            if (Result == 1)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Added Sucessfully');", true);

            }

            Textclear();
            Gridload();
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            SqlConnection CON = new SqlConnection(strconn);
            CON.Open();
            string QUERY8 = " UPDATE M_ShippingLineMaster SET ShipperName='" + ddlShippingLine.SelectedItem.Text + "',Address='" + txtAddress.Text + "',ContactPerson = '" + txtContactPerson.Text + "',Email='" + txtEmail.Text + "',Telephone='" + txtTelephone.Text + "',Fax='" + txtFax.Text + "'  WHERE ShipperName='" + ddlShippingLine.SelectedItem.Text + "'";
            SqlCommand CMD5 = new SqlCommand(QUERY8, CON);
            btnNew.Visible = true;
            int Result = CMD5.ExecuteNonQuery();
            CON.Close();
            Textclear();
            Gridload(); 
            if (Result == 1)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Updated Sucessfully');", true);

            }
           
        }

        protected void btnNew_Click(object sender, EventArgs e)
        {
            Textclear();
            btnSave.Visible = true;
            btnUpdate.Visible = false;
        }

        protected void btnDiscard_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/frmShippingLine.aspx");
        }

        protected void gvShippingLine_SelectedIndexChanged(object sender, EventArgs e)
        {            
            ddlShippingLine.SelectedItem.Text = gvShippingLine.SelectedRow.Cells[1].Text;            
            SqlConnection CON = new SqlConnection(strconn);
            CON.Open();
            string QUERY1 = "SELECT * FROM M_ShippingLineMaster where ShipperName ='" + ddlShippingLine.SelectedItem.Text + "' ";
            SqlDataAdapter SD = new SqlDataAdapter(QUERY1, CON);
            DataSet DS = new DataSet();
            SD.Fill(DS, "DATA");
            DataRowView dr = DS.Tables["DATA"].DefaultView[0];
            ddlShippingLine.SelectedItem.Text = dr["ShipperName"].ToString();
            txtAddress.Text = dr["Address"].ToString();
            txtContactPerson.Text = dr["ContactPerson"].ToString();
            txtEmail.Text = dr["Email"].ToString();
            txtTelephone.Text = dr["Telephone"].ToString();
            txtFax.Text = dr["Fax"].ToString();           
            btnSave.Visible = false;
            btnNew.Visible = true;
            btnUpdate.Visible = true;
            btnDiscard.Visible = true;
        }
    }

}