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
    public partial class frmCountryMaster : System.Web.UI.Page
    {
        string strconn = (string)ConfigurationManager.AppSettings["ConnectionDashboard"];
        CommonDL objCommonDL = new CommonDL();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack == false)
            { 
                Gridload();
                gvCountry.DataBind();
                btnNew.Visible = true;
                BtnUpdate.Visible = false;
                btnSave.Visible = true;
                btnDiscard.Visible = true;
            }
        }

        private void Gridload()
        {
            gvCountry.DataBind();
            Session["query"] = string.Empty;
            string QUERY1;            
            SqlConnection CON = new SqlConnection(strconn);
            QUERY1 = "SELECT distinct CountryId,CountryName,CountryCode,Currency,CurrencyCode FROM M_Country";           
            Session["query"] = QUERY1;
            SqlDataAdapter SD = new SqlDataAdapter(QUERY1, CON);
            DataSet DS = new DataSet();
            SD.Fill(DS, "DATA");
            if (DS.Tables["DATA"].Rows.Count != 0)
            {
                gvCountry.DataSource = DS;
                gvCountry.DataBind();
            }
            else
            {
                gvCountry.DataSource = null;
                gvCountry.DataBind();
            }
            

        }

        private void cleartext()
        {
            txtCountry.Text = "";
            txtCapital.Text = "";
            txtCountryCode.Text = "";
            txtCurrency.Text = "";
            txtCurrencyCode.Text = "";
            txtLanguage.Text = "";
            txtShortCode.Text = "";
        }




        protected void btnSave_Click(object sender, EventArgs e)
        {
            string CountryName = txtCountry.Text;
            string CountryCode = txtCountryCode.Text;
            string CodeNo = txtShortCode.Text;
            string CaptialCity = txtCapital.Text;
            string Language = txtLanguage.Text;
            string Currency = txtCurrency.Text;
            string CurrencyCode = txtCurrencyCode.Text;
            SqlConnection CON = new SqlConnection(strconn);
            CON.Open();
            string QUERY6 = "INSERT INTO M_Country(CountryName,CountryCode,CodeNo,CaptialCity,Language,Currency,CurrencyCode)VALUES('" + CountryName + "','" + CountryCode + "','" + CodeNo + "','" + CaptialCity + "','" + Language + "','" + Currency + "','" + CurrencyCode + "')";
            SqlCommand CMD3 = new SqlCommand(QUERY6, CON);
            int Result = CMD3.ExecuteNonQuery();
            CON.Close();
            //txtclear();
            if (Result == 1)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "redirect", "alert('Saved Successfully'); window.location.href='frmCountryMaster.aspx';", true);
            }
        }

protected void btnDiscard_Click(object sender, EventArgs e)
{
    Response.Redirect("~/frmCountryMaster.aspx");
}

protected void btnNew_Click(object sender, EventArgs e)
{
    cleartext();
    btnSave.Visible = true;
    BtnUpdate.Visible = false;

}

protected void BtnUpdate_Click(object sender, EventArgs e)
{
    SqlConnection CON = new SqlConnection(strconn);
    CON.Open();
    string QUERY8 = " UPDATE M_Country SET CountryName='" + txtCountry.Text + "',CountryCode='" + txtCountryCode.Text + "',CodeNo = '" + txtShortCode.Text + "',CaptialCity='" + txtCapital.Text + "',Language='" + txtLanguage.Text + "',Currency='" + txtCurrency.Text + "',CurrencyCode='" + txtCurrencyCode.Text + "'  WHERE CountryId='" + (string)Session["countryid"] + "'";
    SqlCommand CMD5 = new SqlCommand(QUERY8, CON);
    btnNew.Visible = true;
    int Result = CMD5.ExecuteNonQuery();
    CON.Close();
    cleartext();
    if (Result == 1)
    {
        ScriptManager.RegisterStartupScript(this, this.GetType(), "redirect", "alert('Updated Successfully'); window.location.href='frmCountryMaster.aspx';", true);
    }
    Gridload();
}

protected void gvCountry_SelectedIndexChanged1(object sender, EventArgs e)
{
    Session["countryid"] = string.Empty;
    Session["countryid"] = gvCountry.SelectedRow.Cells[1].Text;

            SqlConnection CON = new SqlConnection(strconn);
            CON.Open();
            string QUERY1 = "SELECT * FROM M_Country where CountryId ='" + (string)Session["countryid"] + "' ";
            SqlDataAdapter SD = new SqlDataAdapter(QUERY1, CON);
            DataSet DS = new DataSet();
            SD.Fill(DS, "DATA");
            DataRowView dr = DS.Tables["DATA"].DefaultView[0];
            txtCountry.Text = dr["CountryName"].ToString();
            txtCountryCode.Text = dr["CountryCode"].ToString();
            txtShortCode.Text = dr["CodeNo"].ToString();
            txtCapital.Text = dr["CaptialCity"].ToString();
            txtLanguage.Text = dr["Language"].ToString();
            txtCurrency.Text = dr["Currency"].ToString();
            txtCurrencyCode.Text = dr["CurrencyCode"].ToString();               
            btnSave.Visible = false;
            btnNew.Visible = true;
            BtnUpdate.Visible = true;
            btnDiscard.Visible = true;
}

            protected void gvCountry_PageIndexChanging(object sender, GridViewPageEventArgs e)
                {
                    pageindexgrid(); 
                    gvCountry.PageIndex = e.NewPageIndex;
            }
            private void pageindexgrid()
            {
                string sqlQuery = "Select CountryName,CountryCode,CodeNo,CaptialCity,Language,Currency,CurrencyCode FROM M_Country";
                SqlConnection conn = new SqlConnection(strconn);
                conn.Open();
                SqlDataAdapter da = new SqlDataAdapter(sqlQuery, conn);
                DataSet ds = new DataSet();
                da.Fill(ds, "SQLTABLE");
                gvCountry.DataSource = ds;
                gvCountry.DataBind();
            }

            protected void btnSearch_Click(object sender, EventArgs e)
            {
                string quer = string.Empty;
                DataSet ds = new DataSet();
                quer = "select CountryId,CountryName,CountryCode,Currency,CurrencyCode from M_Country where CountryName like '%" + txtSearch.Text + "%' order by CountryId desc";
                ds = objCommonDL.GetDataSet(quer);
                if (ds.Tables["Table"].Rows.Count != 0)
                {
                    gvCountry.DataSource = ds;
                    gvCountry.DataBind();
                }
                else
                {
                    gvCountry.DataSource = null;
                    gvCountry.DataBind();
                }
            }
}     
    }

