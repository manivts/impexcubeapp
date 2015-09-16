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
using VTS.ImpexCube.Data;

namespace ImpexCube
{
    public partial class frmUnit : System.Web.UI.Page
    {
        string strconn = (string)ConfigurationManager.AppSettings["ConnectionDashboard"];
        CommonDL objCommonDL = new CommonDL();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack == false)
            {
                //UnitBind();
                NoOfUnit();
                Gridload();
                btnNew.Visible = true;
                btnUpdate.Visible = false;
                btnSave.Visible = true;
                btnDiscard.Visible = true;
            }
        }
        
        private void Gridload()
        {
            gvUnit.DataBind();
            Session["query"] = string.Empty;
            string QUERY1;
            SqlConnection CON = new SqlConnection(strconn);
            QUERY1 = "SELECT distinct TransId,UnitDesc,UnitShort,UnitCode,EDICode,UnitType,UnitConv,BaseUnit,NumDesc,UneceCode FROM M_Unit";
            Session["query"] = QUERY1;
            SqlDataAdapter SD = new SqlDataAdapter(QUERY1, CON);
            DataSet DS = new DataSet();
            SD.Fill(DS, "DATA");
            if (DS.Tables["DATA"].Rows.Count != 0)
            {
                gvUnit.DataSource = DS;
                gvUnit.DataBind();
            }
            else
            {
                gvUnit.DataSource = null;
                gvUnit.DataBind();
            }
            


        }
        private void Textclear()
        {
            //ddlDesc.SelectedItem.Text = "~Select~";
            txtShortname.Text = "";
            txtUnitCode.Text = "";
            txtEDICode.Text = "";
            ddlTypeOfUnit.SelectedItem.Text = "~Select~";
            ddlConvFact.SelectedItem.Text = "~Select~";
            ddlNoOfPieces.SelectedItem.Text = "~Select~";
            txtNoOfDecimals.Text = "";
            txtUnece.Text = "";            
        }
            //private void UnitBind()
            //{
             
            //SqlConnection CON = new SqlConnection(strconn);
            //CON.Open();
            //string QUERY1 = "SELECT UnitDesc FROM M_Unit";
            //SqlDataAdapter SD = new SqlDataAdapter(QUERY1, CON);
            //DataSet DS = new DataSet();
            //SD.Fill(DS, "DATA");
            //ddlDesc.DataSource = DS;
            //ddlDesc.DataTextField = "UnitDesc";
            //ddlDesc.DataValueField = "UnitDesc";
            //ddlDesc.DataBind();
            //CON.Close();
            //ddlDesc.Items.Insert(0, new ListItem("~Select~", "0"));
            //}
            private void NoOfUnit()
            {

                SqlConnection CON = new SqlConnection(strconn);
                CON.Open();
                string QUERY1 = "SELECT distinct UnitConv FROM M_Unit";
                SqlDataAdapter SD = new SqlDataAdapter(QUERY1, CON);
                DataSet DS = new DataSet();
                SD.Fill(DS, "DATA");
                ddlConvFact.DataSource = DS;
                ddlConvFact.DataTextField = "UnitConv";
                ddlConvFact.DataValueField = "UnitConv";
                ddlConvFact.DataBind();
                CON.Close();
                ddlConvFact.Items.Insert(0, new ListItem("~Select~", "0"));
            }
            protected void btnSave_Click(object sender, EventArgs e)
            {
                Session["TransID"] = string.Empty;
                string UnitDesc = txtDesc.Text;
                string UnitShort = txtShortname.Text;
                string UnitCode = txtUnitCode.Text;
                string EDICode = txtEDICode.Text;
                string UnitType = ddlTypeOfUnit.SelectedValue;
                string UnitConv = ddlConvFact.SelectedValue;
                string BaseUnit = ddlNoOfPieces.SelectedValue;
                string NumDesc = txtNoOfDecimals.Text;
                string UneceCode = txtUnece.Text;                
                SqlConnection CON = new SqlConnection(strconn);
                CON.Open();
                string QUERY6 = "INSERT INTO M_Unit(UnitDesc,UnitShort,UnitCode,EDICode,UnitType,UnitConv,BaseUnit,NumDesc,UneceCode)VALUES('" + UnitDesc + "','" + UnitShort + "','" + UnitCode + "','" + EDICode + "','" + UnitType + "','" + UnitConv + "','" + BaseUnit + "','" + NumDesc + "','" + UneceCode + "')";
                SqlCommand CMD3 = new SqlCommand(QUERY6, CON);
                int Result = CMD3.ExecuteNonQuery();
                btnNew.Visible = false;
                CON.Close();               
                if (Result == 1)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "redirect", "alert('Saved Successfully'); window.location.href='frmUnit.aspx';", true);
                }               
            }

            protected void btnUpdate_Click(object sender, EventArgs e)
            {
                SqlConnection CON = new SqlConnection(strconn);
                CON.Open();
                string QUERY8 = " UPDATE M_Unit SET UnitDesc='" + txtDesc.Text + "',UnitShort='" + txtShortname.Text + "',UnitCode = '" + txtUnitCode.Text + "',EDICode='" + txtEDICode.Text + "',UnitType='" + ddlTypeOfUnit.SelectedValue + "',UnitConv='" + ddlConvFact.SelectedValue + "',BaseUnit='" + ddlNoOfPieces.SelectedValue + "',NumDesc='" + txtNoOfDecimals.Text + "',UneceCode='" + txtUnece.Text + "'  WHERE TransId='" + (string)Session["TransId"] + "'";
                SqlCommand CMD5 = new SqlCommand(QUERY8, CON);
                btnNew.Visible = true;
                int Result = CMD5.ExecuteNonQuery();
                CON.Close();
                Textclear();
                if (Result == 1)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "redirect", "alert('Updated Successfully'); window.location.href='frmUnit.aspx';", true);
                }
            }

            protected void btnNew_Click(object sender, EventArgs e)
            {
                Response.Redirect("~/frmUnit.aspx");
            }

            protected void btnDiscard_Click(object sender, EventArgs e)
            {
                Response.Redirect("~/HomePage.aspx");
            }

            protected void gvUnit_SelectedIndexChanged(object sender, EventArgs e)
            {
                Session["TransId"] = string.Empty;
                Session["TransId"] = gvUnit.SelectedRow.Cells[1].Text;
                SqlConnection CON = new SqlConnection(strconn);
                CON.Open();
                string QUERY1 = "SELECT * FROM M_Unit where TransId ='" + (string)Session["TransId"] + "' ";
                SqlDataAdapter SD = new SqlDataAdapter(QUERY1, CON);
                DataSet DS = new DataSet();
                SD.Fill(DS, "DATA");
                DataRowView dr = DS.Tables["DATA"].DefaultView[0];
                txtDesc.Text = dr["UnitDesc"].ToString();                
                txtShortname.Text = dr["UnitShort"].ToString();
                txtUnitCode.Text = dr["UnitCode"].ToString();
                txtEDICode.Text = dr["EDICode"].ToString();
                ddlTypeOfUnit.SelectedValue = dr["UnitType"].ToString();
                ddlConvFact.SelectedValue = dr["UnitConv"].ToString();
                ddlNoOfPieces.SelectedValue = dr["BaseUnit"].ToString();
                txtNoOfDecimals.Text = dr["NumDesc"].ToString();
                txtUnece.Text = dr["UneceCode"].ToString();                
                btnSave.Visible = false;
                btnNew.Visible = true;
                btnUpdate.Visible = true;
                btnDiscard.Visible = true;
                
            }

            protected void gvUnit_PageIndexChanging(object sender, GridViewPageEventArgs e)
            {
            gvUnit.PageIndex = e.NewPageIndex;
            pageindexgrid();            
            }
            private void pageindexgrid()
            {
                string sqlQuery = "Select UnitDesc,UnitShort,UnitCode,EDICode,UnitType,UnitConv,BaseUnit,NumDesc,UneceCode FROM M_Unit";
                SqlConnection conn = new SqlConnection(strconn);
                conn.Open();
                SqlDataAdapter da = new SqlDataAdapter(sqlQuery, conn);
                DataSet ds = new DataSet();
                da.Fill(ds, "SQLTABLE");
                gvUnit.DataSource = ds;
                gvUnit.DataBind();
            }

            protected void btnSearch_Click(object sender, EventArgs e)
            {
                string quer = string.Empty;
                DataSet ds = new DataSet();
                quer = "select * from M_Unit where ((UnitDesc LIKE '%" + txtSearch.Text + "%') OR (UnitShort LIKE '%" + txtSearch.Text + "%')) order by Transid desc";
                ds = objCommonDL.GetDataSet(quer);
                if (ds.Tables["Table"].Rows.Count != 0)
                {
                    gvUnit.DataSource = ds;
                    gvUnit.DataBind();
                }
                else
                {
                    gvUnit.DataSource = null;
                    gvUnit.DataBind();
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('No Data Found');", true);
                }
            }


        }
    }
