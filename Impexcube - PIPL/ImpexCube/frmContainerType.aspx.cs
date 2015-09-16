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
    public partial class frmContainerType : System.Web.UI.Page
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
            gvContainerType.DataBind();
            Session["query"] = string.Empty;
            string QUERY1;
            SqlConnection CON = new SqlConnection(strconn);
            QUERY1 = "SELECT distinct ContainerTypeId,ContainerType,Description FROM M_ContainerType";
            Session["query"] = QUERY1;
            SqlDataAdapter SD = new SqlDataAdapter(QUERY1, CON);
            DataSet DS = new DataSet();
            SD.Fill(DS, "DATA");
            if (DS.Tables["DATA"].Rows.Count != 0)
            {
                gvContainerType.DataSource = DS;
                gvContainerType.DataBind();
            }
            else
            {
                gvContainerType.DataSource = null;
                gvContainerType.DataBind();
            }
        }
        private void Textclear()
        {
            txtContainerType.Text = "";
            txtContainerDesc.Text = "";            
        }
        //private void UnitBind()
        //{

        //    SqlConnection CON = new SqlConnection(strconn);
        //    CON.Open();
        //    string QUERY1 = "SELECT ContainerType FROM M_ContainerType";
        //    SqlDataAdapter SD = new SqlDataAdapter(QUERY1, CON);
        //    DataSet DS = new DataSet();
        //    SD.Fill(DS, "DATA");
        //    ddlContainerType.DataSource = DS;
        //    ddlContainerType.DataTextField = "ContainerType";
        //    ddlContainerType.DataValueField = "ContainerType";
        //    ddlContainerType.DataBind();
        //    CON.Close();
        //    ddlContainerType.Items.Insert(0, new ListItem("~Select~", "0"));
        //}

        protected void btnSave_Click(object sender, EventArgs e)
        {            
            string ContainerType = txtContainerType.Text;
            string Description = txtContainerDesc.Text;            
            SqlConnection CON = new SqlConnection(strconn);
            CON.Open();
            string QUERY6 = "INSERT INTO M_ContainerType(ContainerType,Description)VALUES('" + ContainerType + "','" + Description + "')";
            SqlCommand CMD3 = new SqlCommand(QUERY6, CON);
            int Result = CMD3.ExecuteNonQuery();
            CON.Close();
            if (Result == 1)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "redirect", "alert('Saved Successfully'); window.location.href='frmContainerType.aspx';", true);

            }
            Textclear();
            Gridload();
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            SqlConnection CON = new SqlConnection(strconn);
            CON.Open();
            string QUERY8 = " UPDATE M_ContainerType SET ContainerType='" + txtContainerType.Text + "',Description = '" + txtContainerDesc.Text + "'  WHERE ContainerTypeId='" + (string)Session["ContainerTypeId"] + "'";
            SqlCommand CMD5 = new SqlCommand(QUERY8, CON);
            btnNew.Visible = true;
            int Result = CMD5.ExecuteNonQuery();
            CON.Close();
            Textclear();
            Gridload();
            if (Result == 1)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "redirect", "alert('Updated Successfully'); window.location.href='frmContainerType.aspx';", true);

            }
        }

        protected void btnNew_Click(object sender, EventArgs e)
        {
            Textclear();
        }

        protected void btnDiscard_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/HomePage.aspx");
        }

        protected void gvPackage_SelectedIndexChanged1(object sender, EventArgs e)
        {
            Session["ContainerTypeId"] = gvContainerType.SelectedRow.Cells[1].Text;
            SqlConnection CON = new SqlConnection(strconn);
            CON.Open();
            string QUERY1 = "SELECT * FROM M_ContainerType where ContainerTypeId ='" + (string)Session["ContainerTypeId"] + "' ";
            SqlDataAdapter SD = new SqlDataAdapter(QUERY1, CON);
            DataSet DS = new DataSet();
            SD.Fill(DS, "DATA");
            DataRowView dr = DS.Tables["DATA"].DefaultView[0];
            txtContainerType.Text = dr["ContainerType"].ToString();            
            txtContainerDesc.Text = dr["Description"].ToString();
            btnSave.Visible = false;
            btnNew.Visible = true;
            btnUpdate.Visible = true;
            btnDiscard.Visible = true;
        }

        protected void gvContainerType_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
             
            gvContainerType.PageIndex = e.NewPageIndex;
            pageindexgrid();            
            
            
        }
        private void pageindexgrid()
        {
            string sqlQuery = "Select ContainerType,Description FROM M_ContainerType";
            SqlConnection conn = new SqlConnection(strconn);
            conn.Open();
            SqlDataAdapter da = new SqlDataAdapter(sqlQuery, conn);
            DataSet ds = new DataSet();
            da.Fill(ds, "SQLTABLE");
            gvContainerType.DataSource = ds;
            gvContainerType.DataBind();
        }

    }
}