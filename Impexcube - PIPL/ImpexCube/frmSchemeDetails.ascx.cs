using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace ImpexCube
{
    public partial class frmSchemeDetails : System.Web.UI.UserControl
    {
        string con = (string)ConfigurationManager.ConnectionStrings["Constr"].ConnectionString;
        string EDIRegNo = string.Empty;
        string Date = string.Empty;
        string ItemSnoinLic = string.Empty;
        string SchemeLicNo = string.Empty;
        string SchemeLicDate = string.Empty;
        string SchemeType = string.Empty;
        string CIFValue = string.Empty;
        string Qty = string.Empty;
        string Unit = string.Empty;
        string ValueDebited = string.Empty;
        string RegPort = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                filldropdown();
                gridbind();

            }
        }

        private void gridbind()
        {
            DataSet ds = new DataSet();
            string Query = "select * from T_Schemes";
            SqlConnection sqlConn1 = new SqlConnection(con);
            sqlConn1.Open();
            SqlDataAdapter da1 = new SqlDataAdapter(Query, sqlConn1);
            da1.Fill(ds, "Scheme");
            sqlConn1.Close();
            gvScheme.DataSource = ds;
            gvScheme.DataBind();
        }

        private void filldropdown()
        {
            DataSet ds = new DataSet();

            string Query = "select [ProductDutyType] from [M_ProductDutyType]";
                SqlConnection sqlConn = new SqlConnection(con);
                sqlConn.Open();
                SqlDataAdapter da = new SqlDataAdapter(Query, sqlConn);
                da.Fill(ds, "ProductDutyType");
                sqlConn.Close();

                ddlSchemeName.DataSource = ds;
                ddlSchemeName.DataTextField = "ProductDutyType";
                ddlSchemeName.DataValueField = "ProductDutyType";
                ddlSchemeName.DataBind();
                ddlSchemeName.Items.Insert(0, new ListItem("-Select-", "0"));
            
        }
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            EDIRegNo = txtEDIRegNo.Text;
            Date = txtDate.Text;
            ItemSnoinLic = txtItemSnoinLic.Text;
            SchemeLicNo = txtSchemeLicNo.Text;
            SchemeLicDate = txtSchemeLicDate.Text;
            SchemeType = txtSchemeType.Text;
            CIFValue = txtCifValue.Text;
            Qty = txtQty.Text;
            Unit = txtUnit.Text;
            ValueDebited = txtValueDebited.Text;
            RegPort = txtRegPort.Text;

            int result;
            string insertscheme = "Insert into [T_Schemes]([EDIRegNo],[Date],[ItemSnoinLic],[SchemeLicNo],[SchemeLicDate],[SchemeType],[CIFValue],[Qty],[Unit],[ValueDebited],[RegPort]) " +
               " Values('" + EDIRegNo + "','" + Date + "','" + ItemSnoinLic + "','" + SchemeLicNo + "','" + SchemeLicDate + "','" + SchemeType + "','" + CIFValue + "','" + Qty + "','" + Unit + "','" + ValueDebited + "','" + RegPort + "') ";

            SqlConnection sqlConn = new SqlConnection(con);
            sqlConn.Open();
            SqlCommand cmd = new SqlCommand(insertscheme, sqlConn);
            SqlDataAdapter da = new SqlDataAdapter();

            cmd.CommandText = insertscheme;
            cmd.Connection = sqlConn;
            da.SelectCommand = cmd;
            result = cmd.ExecuteNonQuery();
            sqlConn.Close();

            gridbind();
            if (result == 1)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "redirect", "alert('Added Successfully');", true);
            }
            clear();

        }
        public void clear()
        {
            txtEDIRegNo.Text = "";
            txtDate.Text = "";
            txtItemSnoinLic.Text = "";
            txtSchemeLicDate.Text = "";
            txtSchemeType.Text = "";
            txtCifValue.Text = "";
            txtQty.Text = "";
            txtUnit.Text = "";
            txtValueDebited.Text = "";
            txtRegPort.Text = "";
        }

        protected void ddlSchemeName_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}