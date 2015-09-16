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

namespace ImpexCube.UIMaster
{
    public partial class UCCurrencyNew : System.Web.UI.UserControl
    {
        string strconn = (string)ConfigurationManager.AppSettings["ConnectionDashboard"];
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack == false)
            {
                Currency();
                btnNew.Visible = true;
                btnUpdate.Visible = false;
                btnSave.Visible = true;
                btnDiscard.Visible = true;
            }
        }
        private void Currency()
        {

            SqlConnection CON = new SqlConnection(strconn);
            CON.Open();
            string QUERY1 = "SELECT CurrencyName FROM M_Currency";
            SqlDataAdapter SD = new SqlDataAdapter(QUERY1, CON);
            DataSet DS = new DataSet();
            SD.Fill(DS, "DATA");
            ddlCurrency.DataSource = DS;
            ddlCurrency.DataTextField = "CurrencyName";
            ddlCurrency.DataValueField = "CurrencyName";
            ddlCurrency.DataBind();
            CON.Close();
            ddlCurrency.Items.Insert(0, new ListItem("~Select~", "0"));
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            string CurrencyName = ddlCurrency.SelectedItem.Text;
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
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Added Sucessfully');", true);

            }
            //Gridload();

            ClearText();
        }

        private void ClearText()
        {
            ddlCurrency.SelectedItem.Text = "";
            txtShortname.Text = "";
            txtCurrency.Text = "";
            txtexchangeimp.Text = "";
            txtEffectiveFrom.Text = "";
            txtExchangeExp.Text = "";
            txtCurencycode.Text = "";
            txtConversion.Text = "";
            txtCurrencyUnit.Text = "";
            ddlstandardcurrency.SelectedItem.Text = "";
            txtCurrencyBe.Text = "";
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            SqlConnection CON = new SqlConnection(strconn);
            CON.Open();
            string QUERY8 = " UPDATE M_Currency SET CurrencyName='" + ddlCurrency.SelectedItem.Text + "',CurrencyUnit='" + txtCurrencyUnit.Text + "'CurrencyCode = '" + txtCurrency.Text + "',CurrencyShortName='" + txtShortname.Text + "',IMPCurrencyRate='" + txtexchangeimp.Text + "',LastChange='" + txtEffectiveFrom.Text + "',EXPCurrencyRate='" + txtExchangeExp.Text + "',EDICode='" + txtCurencycode.Text + "',ConvFact='" + txtConversion.Text + "', StdCurnc='" + ddlstandardcurrency.SelectedValue + "',BECode='" + txtCurrencyBe.Text + "'  WHERE CurrencyName='" + ddlCurrency.SelectedItem.Text + "'";
            SqlCommand CMD5 = new SqlCommand(QUERY8, CON);
            btnNew.Visible = true;
            int Result = CMD5.ExecuteNonQuery();
            CON.Close();
            ClearText();
            if (Result == 1)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Updated Sucessfully');", true);
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
            Response.Redirect("~/Currency Master.aspx");
        }

       
    }
}