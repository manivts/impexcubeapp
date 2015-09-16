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
    public partial class CurrencyMaster : System.Web.UI.Page
    {
      string strconn = (string)ConfigurationManager.AppSettings["ConnectionDashboard"];
      CommonDL objCommonDL = new CommonDL();
      protected void Page_Load(object sender, EventArgs e)
      {
          if (IsPostBack == false)
          {
              Gridload();
              btnExit.Visible = true;
              btnSave.Visible = true;
              btnUpdate.Visible = false;
              btnNew.Visible = true;
          }
      }

      public string changedate(string date)
      {
          if (date != "")
          {
              string[] a = date.Split('/');
              date = a[1] + "/" + a[0] + "/" + a[2];

          }
          return date;
      }

      protected void btnSave_Click(object sender, EventArgs e)
      {
          string CurrencyName = txtCurrencymaster.Text;
          string CurrencyCode = txtCurrency.Text;
          string CurrencyShortName = txtShortname.Text;
          string IMPCurrencyRate = txtexchangeimp.Text;
          string LastChange = txtEffectiveFrom.Text;
          string EXPCurrencyRate = txtExchangeExp.Text;
          string EDICode = txtCurencycode.Text;
          string ConvFact = txtConversion.Text;
          string CurrencyUnit = txtCurrencyUnit.Text;
          string StdCurnc = ddlstandardcurrency.SelectedValue;
          string BECode = txtCurrencyBe.Text;
          SqlConnection CON = new SqlConnection(strconn);
          CON.Open();
          string QUERY6 = "INSERT INTO M_Currency(CurrencyName,CurrencyCode,CurrencyShortName,IMPCurrencyRate,LastChange,EXPCurrencyRate,EDICode,ConvFact,CurrencyUnit,StdCurnc,BECode)VALUES('" + CurrencyName + "','" + CurrencyCode + "','" + CurrencyShortName + "','" + IMPCurrencyRate + "','" + LastChange + "','" + EXPCurrencyRate + "','" + EDICode + "','" + ConvFact + "','" + CurrencyUnit + "','" + StdCurnc + "','" + BECode + "')";
          SqlCommand CMD3 = new SqlCommand(QUERY6, CON);
          int Result = CMD3.ExecuteNonQuery();
          btnNew.Visible = false;
          CON.Close();
          //txtclear();
          if (Result == 1)
          {
              //ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Added Sucessfully');", true);
              ScriptManager.RegisterStartupScript(this, this.GetType(), "redirect", "alert('Added Sucessfully'); window.location.href='CurrencyMaster.aspx';", true);
          }
          //Gridload();

          ClearText();
          Gridload();
      }

      private void ClearText()
      {
          txtCurrencymaster.Text = "";
          txtShortname.Text = "";
          txtCurrency.Text = "";
          txtexchangeimp.Text = "";
          txtEffectiveFrom.Text = "";
          txtExchangeExp.Text = "";
          txtCurencycode.Text = "";
          txtConversion.Text = "";
          txtCurrencyUnit.Text = "";
          ddlstandardcurrency.SelectedValue = "~Select~";
          txtCurrencyBe.Text = "";
      }

      protected void btnUpdate_Click(object sender, EventArgs e)
      {
          try
          {
              SqlConnection CON = new SqlConnection(strconn);
              CON.Open();
              string QUERY8 = " UPDATE M_Currency SET CurrencyName='" + txtCurrencymaster.Text + "',CurrencyUnit='" + txtCurrencyUnit.Text + "',CurrencyCode = '" + txtCurrency.Text + "',CurrencyShortName='" + txtShortname.Text + "',IMPCurrencyRate='" + txtexchangeimp.Text + "',LastChange='" +changedate( txtEffectiveFrom.Text )+ "',EXPCurrencyRate='" + txtExchangeExp.Text + "',EDICode='" + txtCurencycode.Text + "',ConvFact='" + txtConversion.Text + "', StdCurnc='" + ddlstandardcurrency.SelectedValue + "',BECode='" + txtCurrencyBe.Text + "'  WHERE CurrencyId='" + (string)Session["currencyid"] + "'";
              SqlCommand CMD5 = new SqlCommand(QUERY8, CON);
              btnNew.Visible = true;
              int Result = CMD5.ExecuteNonQuery();
              CON.Close();

              if (Result == 1)
              {
                  ScriptManager.RegisterStartupScript(this, this.GetType(), "redirect", "alert('Updated Sucessfully'); window.location.href='CurrencyMaster.aspx';", true);
                  Gridload();
              }
          }
          catch (SqlException ex)
          {
              ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('DataBase Error: " + ex.Message + "');", true);
          }
      }

      protected void btnNew_Click(object sender, EventArgs e)
      {
          ClearText();
          btnSave.Visible = true;
          btnUpdate.Visible = false;
      }

      protected void btnDiscard_Click(object sender, EventArgs e)
      {
          Response.Redirect("~/CurrencyMaster.aspx");
      }
      private void Gridload()
      {
          gvCurrency.DataBind();
          Session["query"] = string.Empty;
          string QUERY1;
          //string ApplicationName = ddlappname.SelectedItem.Text;
          SqlConnection CON = new SqlConnection(strconn);

          QUERY1 = "SELECT CurrencyId,CurrencyName,CurrencyShortName,Convert(varchar(10),LastChange,103) As LastChange ,IMPCurrencyRate,EXPCurrencyRate FROM M_Currency";
          Session["query"] = QUERY1;
          SqlDataAdapter SD = new SqlDataAdapter(QUERY1, CON);
          DataSet DS = new DataSet();
          SD.Fill(DS, "DATA");
          if (DS.Tables["DATA"].Rows.Count != 0)
          {
              gvCurrency.DataSource = DS;
              gvCurrency.DataBind();
          }
          else
          {
              gvCurrency.DataSource = null;
              gvCurrency.DataBind();
          }


      }
      protected void gvCurrency_SelectedIndexChanged(object sender, EventArgs e)
      {
          Session["currencyid"] = gvCurrency.SelectedRow.Cells[1].Text;
          SqlConnection CON = new SqlConnection(strconn);
          CON.Open();
          string QUERY1 = "SELECT CurrencyName,EDICode,CurrencyShortName,IMPCurrencyRate,EXPCurrencyRate,Convert(varchar(10),LastChange,103) AS LastChange,CurrencyCode,ConvFact,CurrencyUnit,StdCurnc,BECode FROM M_Currency where CurrencyId ='" + (string)Session["currencyid"] + "' ";
          SqlDataAdapter SD = new SqlDataAdapter(QUERY1, CON);
          DataSet DS = new DataSet();
          SD.Fill(DS, "DATA");
          DataRowView dr = DS.Tables["DATA"].DefaultView[0];
          txtCurrencymaster.Text = dr["CurrencyName"].ToString();
          txtCurencycode.Text = dr["EDICode"].ToString();
          txtShortname.Text = dr["CurrencyShortName"].ToString();
          txtexchangeimp.Text = dr["IMPCurrencyRate"].ToString();
          txtExchangeExp.Text = dr["EXPCurrencyRate"].ToString();
          txtEffectiveFrom.Text = dr["LastChange"].ToString();
          txtCurrency.Text = dr["CurrencyCode"].ToString();
          txtConversion.Text = dr["ConvFact"].ToString();
          txtCurrencyUnit.Text = dr["CurrencyUnit"].ToString();
          ddlstandardcurrency.SelectedValue = dr["StdCurnc"].ToString();
          txtCurrencyBe.Text = dr["BECode"].ToString();
          btnSave.Visible = false;
          btnNew.Visible = true;
          btnUpdate.Visible = true;
          btnExit.Visible = true;   
      }

      protected void btnExit_Click(object sender, EventArgs e)
      {
          Response.Redirect("~/HomePage.aspx");
      }

      protected void btnSearch_Click(object sender, EventArgs e)
      {
          string quer = string.Empty;
          DataSet ds = new DataSet();
          if (ddlstandCurySearch.SelectedValue == "~Select~")
          {
              quer = "select CurrencyId,CurrencyName,CurrencyShortName,Convert(varchar(10),LastChange,103) As LastChange ,CurrencyCode,IMPCurrencyRate,EXPCurrencyRate from M_Currency where ((CurrencyName LIKE '%" + txtSearch.Text + "%') OR (CurrencyShortName LIKE '%" + txtSearch.Text + "%')) order by CurrencyId desc";
              
          }
          else
          {
              quer = "select CurrencyId,CurrencyName,CurrencyShortName,Convert(varchar(10),LastChange,103) As LastChange ,CurrencyCode,IMPCurrencyRate,EXPCurrencyRate from M_Currency where ((CurrencyName LIKE '%" + txtSearch.Text + "%') OR (CurrencyShortName LIKE '%" + txtSearch.Text + "%')) and StdCurnc='" + ddlstandCurySearch.SelectedValue + "' order by CurrencyId desc";
          }
          ds = objCommonDL.GetDataSet(quer);
          if (ds.Tables["Table"].Rows.Count != 0)
          {
              gvCurrency.DataSource = ds;
              gvCurrency.DataBind();
          }
          else
          {
              gvCurrency.DataSource = null;
              gvCurrency.DataBind();
              ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('No Data Found');", true);
          }
      }


    }
    }
