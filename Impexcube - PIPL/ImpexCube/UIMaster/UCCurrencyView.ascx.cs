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
    public partial class UCCurrencyView : System.Web.UI.UserControl
    {
        string strconn = (string)ConfigurationManager.AppSettings["ConnectionDashboard"];
        protected void Page_Load(object sender, EventArgs e)
        {
            Gridload();
        }
        private void Gridload()
        {
            gvCurrency.DataBind();
            Session["query"] = string.Empty;
            string QUERY1;
            SqlConnection CON = new SqlConnection(strconn);

            QUERY1 = "SELECT distinct CurrencyName,CurrencyCode,CurrencyShortName,IMPCurrencyRate,LastChange,EXPCurrencyRate,EDICode,ConvFact,StdCurnc,BECode,CurrencyUnit FROM M_Currency";
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

        //    ddlCurrency.SelectedItem.Text = gvCurrency.SelectedRow.Cells[1].Text;
        //    SqlConnection CON = new SqlConnection(strconn);
        //    CON.Open();
        //    string QUERY1 = "SELECT * FROM M_Currency where CurrencyName ='" + ddlCurrency.SelectedItem.Text + "' ";
        //    SqlDataAdapter SD = new SqlDataAdapter(QUERY1, CON);
        //    DataSet DS = new DataSet();
        //    SD.Fill(DS, "DATA");
        //    DataRowView dr = DS.Tables["DATA"].DefaultView[0];
        //    ddlCurrency.SelectedItem.Text = dr["CurrencyName"].ToString();
        //    txtCurencycode.Text = dr["EDICode"].ToString();
        //    txtShortname.Text = dr["CurrencyShortName"].ToString();
        //    txtexchangeimp.Text = dr["IMPCurrencyRate"].ToString();
        //    txtExchangeExp.Text = dr["EXPCurrencyRate"].ToString();
        //    txtEffectiveFrom.Text = dr["LastChange"].ToString();
        //    txtCurrency.Text = dr["CurrencyCode"].ToString();
        //    txtConversion.Text = dr["ConvFact"].ToString();
        //    txtCurrencyUnit.Text = dr["CurrencyUnit"].ToString();
        //    ddlstandardcurrency.SelectedItem.Text = dr["StdCurnc"].ToString();
        //    txtCurrencyBe.Text = dr["BECode"].ToString();
        //    btnSave.Visible = false;
        //    btnNew.Visible = true;
        //    btnUpdate.Visible = true;
        //    btnDiscard.Visible = true;
        }
    }
}