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
    public partial class frmPackage : System.Web.UI.Page
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
            gvPackage.DataBind();
            Session["query"] = string.Empty;
            string QUERY1;
            SqlConnection CON = new SqlConnection(strconn);
            QUERY1 = "SELECT distinct PackageName,ShortName,PluralName,SaidToContain,UneceCode from M_Package";            
            Session["query"] = QUERY1;
            SqlDataAdapter SD = new SqlDataAdapter(QUERY1, CON);
            DataSet DS = new DataSet();
            SD.Fill(DS, "DATA");
            if (DS.Tables["DATA"].Rows.Count != 0)
            {
                gvPackage.DataSource = DS;
                gvPackage.DataBind();
            }
            else
            {
                gvPackage.DataSource = null;
                gvPackage.DataBind();
            }



        }
        private void Textclear()
        {
            ddlPackage.SelectedItem.Text = "";
            txtShortname.Text = "";
            txtPluralname.Text = "";
            txtsaidtocon.Text = "";
            txtUNECECode.Text = "";
            
        }
        private void UnitBind()
        {

            SqlConnection CON = new SqlConnection(strconn);
            CON.Open();
            //string package = ddlPackage.SelectedItem.Text;
            string QUERY1 = "SELECT UnitShort FROM M_Unit ";
            SqlDataAdapter SD = new SqlDataAdapter(QUERY1, CON);
            DataSet DS = new DataSet();
            SD.Fill(DS, "DATA");
            ddlPackage.DataSource = DS;
            ddlPackage.DataTextField = "UnitShort";
            ddlPackage.DataBind();
            CON.Close();
            
            ddlPackage.Items.Insert(0, new ListItem("~Select~", "0"));
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            string PackageName = ddlPackage.SelectedItem.Text;
            string ShortName = txtShortname.Text;
            string PluralName = txtPluralname.Text;
            string SaidToContain = txtsaidtocon.Text;
            string UneceCode = txtUNECECode.Text;            
            SqlConnection CON = new SqlConnection(strconn);
            CON.Open();
            string QUERY6 = "INSERT INTO M_Package(PackageName,ShortName,PluralName,SaidToContain,UneceCode)VALUES('" + PackageName + "','" + ShortName + "','" + PluralName + "','" + SaidToContain + "','" + UneceCode + "')";
            SqlCommand CMD3 = new SqlCommand(QUERY6, CON);
            int Result = CMD3.ExecuteNonQuery();
            btnNew.Visible = false;
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
            string QUERY8 = " UPDATE M_Package SET PackageName='" + ddlPackage.SelectedItem.Text + "',ShortName='" + txtShortname.Text + "',PluralName = '" + txtPluralname.Text + "',SaidToContain='" + txtsaidtocon.Text + "',UneceCode='" + txtUNECECode.Text + "'  WHERE PackageName='" + ddlPackage.SelectedItem.Text + "'";
            SqlCommand CMD5 = new SqlCommand(QUERY8, CON);
            btnNew.Visible = true;
            int Result = CMD5.ExecuteNonQuery();
            CON.Close();
            Textclear();
            if (Result == 1)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Updated Sucessfully');", true);

            }
            Gridload();
            Response.Redirect("~/frmPackage.aspx");
        }
        

        protected void btnNew_Click(object sender, EventArgs e)
        {
            Textclear();
        }

        protected void btnDiscard_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/frmPackage.aspx");
        }

        protected void gvPackage_SelectedIndexChanged1(object sender, EventArgs e)
        {
            ddlPackage.SelectedItem.Text = gvPackage.SelectedRow.Cells[1].Text;
            SqlConnection CON = new SqlConnection(strconn);
            CON.Open();
            string QUERY1 = "SELECT * FROM M_Package where PackageName ='" + ddlPackage.SelectedItem.Text + "' ";
            SqlDataAdapter SD = new SqlDataAdapter(QUERY1, CON);
            DataSet DS = new DataSet();
            SD.Fill(DS, "DATA");
            DataRowView dr = DS.Tables["DATA"].DefaultView[0];
            ddlPackage.SelectedItem.Text = dr["PackageName"].ToString();
            txtShortname.Text = dr["ShortName"].ToString();
            txtPluralname.Text = dr["PluralName"].ToString();
            txtsaidtocon.Text = dr["SaidToContain"].ToString();
            txtUNECECode.Text = dr["UneceCode"].ToString();            
            btnSave.Visible = false;
            btnNew.Visible = true;
            btnUpdate.Visible = true;
            btnDiscard.Visible = true; 
            //Session["ID"] = gvPackage.SelectedRow.Cells[0].Text;
            //ddlPackage.SelectedItem.Text = gvPackage.SelectedRow.Cells[1].Text;
            //txtShortname.Text = gvPackage.SelectedRow.Cells[2].Text;
            //txtPluralname.Text = gvPackage.SelectedRow.Cells[3].Text;
            //txtsaidtocon.Text = gvPackage.SelectedRow.Cells[4].Text;
            //txtUNECECode.Text = gvPackage.SelectedRow.Cells[5].Text;
            //btnSave.Visible = false;
            //btnNew.Visible = true;
            //btnUpdate.Visible = true;
            //btnDiscard.Visible = true;
            
        }

       
    }
}