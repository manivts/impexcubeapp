using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using VTS.ImpexCube.Utlities;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace ImpexCube
{
    public partial class frmImpLicsenceMaster : System.Web.UI.Page
    {
        string sqlconn = (string)ConfigurationManager.ConnectionStrings["Constr"].ConnectionString;
        string keyname = "LRN";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack == false)
            {
                var pagename = (Label)Master.FindControl("lblName");
                pagename.Text = "Licence Master";
                string fyear = (string)Session["FYear"];
                txtLicenceRefNo.Text = keyname + "/" + fyear + "/" + Convert.ToString(Utility.GetNextAutoNo(keyname, 0, Utility.GetConnectionString()));
                Session["Keycode"] = Convert.ToString(Utility.GetNextAutoNo(keyname, 0, Utility.GetConnectionString()));
                txtLicenceDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                BindSchemeType();
                BindCurrency();
                BindUnit();
                BindPort();
            }
        }

        private void BindPort()
        {
            string query = "Select PortCode,PortName,UneceCode from M_Port WHERE UneceCode LIKE 'IN%' ";
            DataSet ds = GetDataSet(query);
            if (ds.Tables["data"].Rows.Count != 0)
            {
                ddlPortRegn.DataSource = ds;
                ddlPortRegn.DataTextField = "PortName";// + "(" + "UneceCode"+ ")"
                ddlPortRegn.DataValueField = "PortCode";
                ddlPortRegn.DataBind();
                ddlPortRegn.Items.Insert(0, new ListItem("~Select~", "0"));
            }
            else
            {
                ddlPortRegn.DataSource = null;
                ddlPortRegn.DataBind();
            }
        }
        
        private void BindUnit()
        {
            string query = "Select UnitShort from  M_Unit";
            DataSet ds = GetDataSet(query);
            if (ds.Tables["data"].Rows.Count != 0)
            {
                ddlUnit.DataSource = ds;
                ddlUnit.DataTextField = "UnitShort";
                ddlUnit.DataValueField = "UnitShort";
                ddlUnit.DataBind();
                ddlUnit.Items.Insert(0, new ListItem("~Select~", "0"));

                ddlImpUnit.DataSource = ds;
                ddlImpUnit.DataTextField = "UnitShort";
                ddlImpUnit.DataValueField = "UnitShort";
                ddlImpUnit.DataBind();
                ddlImpUnit.Items.Insert(0, new ListItem("~Select~", "0"));
            }
            else
            {
                ddlUnit.DataSource = null;
                ddlUnit.DataBind();

                ddlImpUnit.DataSource = null;
                ddlImpUnit.DataBind();
            }
        }

        private void BindCurrency()
        {
            string query = "Select CurrencyShortName from  M_Currency";
            DataSet ds = GetDataSet(query);
            if (ds.Tables["data"].Rows.Count != 0)
            {
                ddlCurency.DataSource = ds;
                ddlCurency.DataTextField = "CurrencyShortName";
                ddlCurency.DataValueField = "CurrencyShortName";
                ddlCurency.DataBind();
                ddlCurency.Items.Insert(0, new ListItem("~Select~", "0"));
            }
            else
            {
                ddlCurency.DataSource = null;
                ddlCurency.DataBind();
            }
        }

        private void BindSchemeType()
        {
           // string query = "Select Distinct a.ApplicableImpSchemes as [Type],b.SchemeDescription as [Desc] from M_EximSchm a, M_ImpSchemeDesc b where a.ApplicableImpSchemes = b.ImportScheme";
            string query = "SELECT DISTINCT ApplicableImpSchemes FROM  M_EximSchm WHERE  (ApplicableImpSchemes NOT IN ('', '.'))";
            DataSet ds = GetDataSet(query);
            if (ds.Tables["data"].Rows.Count != 0)
            {
                ddlSchemeType.DataSource = ds;
                ddlSchemeType.DataTextField = "ApplicableImpSchemes";//"Type" + "(" + "Desc" + ")";
                ddlSchemeType.DataValueField = "ApplicableImpSchemes";
                ddlSchemeType.DataBind();
                ddlSchemeType.Items.Insert(0, new ListItem("~Select~", "0"));
            }
            else
            {
                ddlSchemeType.DataSource = null;
                ddlSchemeType.DataBind();
            }
        }

        private DataSet GetDataSet(string Query)
        {
            SqlConnection con = new SqlConnection(sqlconn);
            con.Open();
            SqlDataAdapter da = new SqlDataAdapter(Query, con);
            DataSet ds = new DataSet();
            da.Fill(ds, "data");
            con.Close();
            return ds;
        }

        protected void ddlSchemeType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlSchemeType.SelectedIndex != 0)
            {
                BindSchemeNotn();
                if (ddlSchemeType.SelectedValue == "DEEC" || ddlSchemeType.SelectedValue == "EPCG")
                {
                   // rwExport.Visible = true;
                    pnlExport.Visible = true;
                    rwExportItems.Visible = true;
                   // rwImport.Visible = true;
                    rwImportItems.Visible = true;
                    rwValue.Visible = false;
                }
                else if (ddlSchemeType.SelectedValue == "DEPB")
                {
                  //  rwExport.Visible = false;
                    pnlExport.Visible = false;
                    rwExportItems.Visible = false;
                  //  rwImport.Visible = false;
                    rwImportItems.Visible = false;
                    rwValue.Visible = true;
                }
                else
                {
                   // rwExport.Visible = false;
                    pnlExport.Visible = false;
                    rwExportItems.Visible = false;
                   // rwImport.Visible = true;
                    rwImportItems.Visible = true;
                    rwValue.Visible = false;
                }
            }
        }

        private void BindSchemeNotn()
        {
            string query = "SELECT NOTN,SchemeName FROM M_SchemeNotn where SchemeName= '" + ddlSchemeType.SelectedValue + "' ";
            DataSet ds = GetDataSet(query);
            if (ds.Tables["data"].Rows.Count != 0)
            {
                ddlSchemeNotn.DataSource = ds;
                ddlSchemeNotn.DataTextField = "NOTN";
                ddlSchemeNotn.DataValueField = "NOTN";
                ddlSchemeNotn.DataBind();
                ddlSchemeNotn.Items.Insert(0, new ListItem("~Select~", "0"));
            }
            else
            {
                ddlSchemeNotn.DataSource = null;
                ddlSchemeNotn.DataBind();
            }
        }

        protected void ddlSchemeNotn_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlSchemeNotn.SelectedIndex != 0)
            {
                BindSchemeNotnNo();
            }
        }

        private void BindSchemeNotnNo()
        {
            string query = "SELECT Distinct SLNO FROM M_SchemeNotn where NOTN= '" + ddlSchemeNotn.SelectedValue + "' ";
            DataSet ds = GetDataSet(query);
            if (ds.Tables["data"].Rows.Count != 0)
            {
                ddlSubSchemeNotn.DataSource = ds;
                ddlSubSchemeNotn.DataTextField = "SLNO";
                ddlSubSchemeNotn.DataValueField = "SLNO";
                ddlSubSchemeNotn.DataBind();
                ddlSubSchemeNotn.Items.Insert(0, new ListItem("~Select~", "0"));
            }
            else
            {
                ddlSubSchemeNotn.DataSource = null;
                ddlSubSchemeNotn.DataBind();
            }
        }

        protected void btnAdd_Click(object sender, ImageClickEventArgs e)
        {
            string lrno = txtLicenceRefNo.Text;
            string licDate = txtLicenceDate.Text;
            string desc = txtDescription.Text;
            string FobInr = txtFobValueINR.Text;
            string FobFc = txtFobValueFC.Text;
            string qty = txtQuantity.Text;
            string unit = ddlUnit.Text;
            string opInr = txtOPBalINR.Text;
            string opFc = txtOPBalFC.Text;
            string Opqty = txtOPBalQty.Text;
            string opUnit = txtUnit0.Text;
            string created = (string)Session["USER-NAME"];
            string createdate = DateTime.Now.ToString();//"dd/MM/yyyy"
            int result = 0;

            try
            {
                string query = "Insert Into M_LicenceExport([LicenceRefNo],[Description],[FobValueINR],[FobValueFC],[Quantity]," +
                    "[FOBUnit],[OpBalINR],[OpBalFC],[OpBalQty],[OPUnit],[Active],[CreatedBy],[CreatedDate],[ModifiedBy],[ModifiedDate])" +
                    "Values('" + lrno + "','" + desc + "','" + FobInr + "','" + FobFc + "','" + qty + "','" + unit + "','" + opInr + "','" + opFc + "','" + Opqty + "','" + opUnit + "',  '"+ true +"'     " +
                    " '" + created + "','" + createdate + "','" + created + "','" + createdate + "')";

                result = GetSqlCommand(query);
                if (result != 0)
                {
                    ClearExpItem();
                    BindGridExpItems();
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "redirect", "alert('"+ex.Message+"');", true);
            }
        }

        private void ClearExpItem()
        {
            txtDescription.Text = string.Empty;
            txtFobValueINR.Text = string.Empty;
            txtOPBalINR.Text = string.Empty;
            txtFobValueFC.Text = string.Empty;
            txtOPBalFC.Text = string.Empty;
            txtQuantity.Text = string.Empty;
            txtOPBalQty.Text = string.Empty;
            ddlUnit.SelectedIndex = 0;
            txtUnit0.Text = string.Empty;
            btnAdd.Visible = true;
            btnUpdate.Visible = false;
        }

        private void BindGridExpItems()
        {
            string query = "Select LicenceExportID,[Description],[FobValueINR],[FobValueFC],[Quantity]," +
                    "[FobUnit],[OpBalINR],[OpBalFC],[OpBalQty],[OPUnit] from M_LicenceExport" +
                    " Where [LicenceRefNo]='" + txtLicenceRefNo.Text + "'";
            DataSet ds = GetDataSet(query);
            if (ds.Tables["data"].Rows.Count != 0)
            {
                gvExportItems.DataSource = ds;
                gvExportItems.DataBind();
            }
            else
            {
                gvExportItems.DataSource = null;
                gvExportItems.DataBind();
            }
        }

        private int GetSqlCommand(string Query)
        {
            SqlConnection sqlConn = new SqlConnection(sqlconn);
            sqlConn.Open();
            SqlCommand cmd = new SqlCommand(Query, sqlConn);
            SqlDataAdapter da = new SqlDataAdapter();

            cmd.CommandText = Query;
            cmd.Connection = sqlConn;
            da.SelectCommand = cmd;
            int result;
            result = cmd.ExecuteNonQuery();
            sqlConn.Close();
            return result;
        }

        protected void btnUpdate_Click(object sender, ImageClickEventArgs e)
        {
            string id = (string)Session["ExpId"];
            string lrno = txtLicenceRefNo.Text;
            string licDate = txtLicenceDate.Text;
            string desc = txtDescription.Text;
            string FobInr = txtFobValueINR.Text;
            string FobFc = txtFobValueFC.Text;
            string qty = txtQuantity.Text;
            string unit = ddlUnit.Text;
            string opInr = txtOPBalINR.Text;
            string opFc = txtOPBalFC.Text;
            string Opqty = txtOPBalQty.Text;
            string opUnit = txtUnit0.Text;
            string created = (string)Session["USER-NAME"];
            string createdate = DateTime.Now.ToString("dd/MM/yyyy");
            int result = 0;

            try
            {
                string query = "Update M_LicenceExport Set [Description] = '" + desc + "',[FobValueINR]= '" + FobInr + "',[FobValueFC]=" + FobFc + "',[Quantity]='" + qty + "'," +
                    "[FobUnit]='" + unit + "',[OpBalINR]='" + opInr + "',[OpBalFC]='" + opFc + "',[OpBalQty]='" + Opqty + "',[OPUnit]='" + opUnit + "'," +
                    "[ModifiedBy]='" + created + "',[ModifiedDate]='" + createdate + "' Where TransId= '" + id + "'   " ;                    

                result = GetSqlCommand(query);
                if (result != 0)
                {
                    ClearExpItem();
                    BindGridExpItems();
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "redirect", "alert('" + ex.Message + "');", true);
            }
        }

        protected void btnImpAdd_Click(object sender, ImageClickEventArgs e)
        {
            string lrno = txtLicenceRefNo.Text;
            string licDate = txtLicenceDate.Text;
            string desc = txtImpDesc.Text;
            string CIFInr = txtCIFValueINR.Text;
            string CIFFc = txtCIFValueFC.Text;
            string qty = txtImpQuantity.Text;
            string unit = ddlImpUnit.Text;
            string opInr = txtImpOPBalINR.Text;
            string opFc = txtImpOpBalFC.Text;
            string Opqty = txtImpOpBalQty.Text;
            string opUnit = txtImpUnit0.Text;
            string created = (string)Session["USER-NAME"];
            string createdate = DateTime.Now.ToString("dd/MM/yyyy");
            int result = 0;
            bool Active = true;
            try
            {
                string query = "Insert Into M_LicenceImport([LicenceRefNo],[Description],[CIFValueINR],[CIFValueFC],[Quantity]," +
                    "[CIFUnit],[OpBalINR],[OpBalFC],[OpBalQty],[OPUnit],[Active],[CreatedBy],[CreatedDate],[ModifiedBy],[ModifiedDate])" +
                    "Values('" + lrno + "','" + desc + "','" + CIFInr + "','" + CIFFc + "','" + qty + "','" + unit + "','" + opInr + "','" + opFc + "','" + Opqty + "','" + opUnit + "',  '" + true + "'    " +
                    ", '" + created + "','" + createdate + "','" + created + "','" + createdate + "')";

                result = GetSqlCommand(query);
                if (result != 0)
                {
                    ClearImpItem();
                    BindGridImpItems();
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "redirect", "alert('" + ex.Message + "');", true);
            }
        }

        private void ClearImpItem()
        {
            txtImpDesc.Text = string.Empty;
            txtCIFValueINR.Text = string.Empty;
            txtImpOPBalINR.Text = string.Empty;
            txtCIFValueFC.Text = string.Empty;
            txtImpOpBalFC.Text = string.Empty;
            txtImpQuantity.Text = string.Empty;
            ddlImpUnit.SelectedIndex = 0;
            txtImpUnit0.Text = string.Empty;
            btnImpUpdate.Visible = false;
            btnImpAdd.Visible = true;
        }

        private void BindGridImpItems()
        {
            string query = "Select LicenceImportID,[Description],[CIFValueINR],[CIFValueFC],[Quantity]," +
                    "[CIFUnit],[OpBalINR],[OpBalFC],[OpBalQty],[OPUnit] from M_LicenceImport" +
                    " Where [LicenceRefNo]='" + txtLicenceRefNo.Text + "'";
            DataSet ds = GetDataSet(query);
            if (ds.Tables["data"].Rows.Count != 0)
            {
                gvImportItems.DataSource = ds;
                gvImportItems.DataBind();
            }
            else
            {
                gvImportItems.DataSource = null;
                gvImportItems.DataBind();
            }
        }

        protected void btnImpUpdate_Click(object sender, ImageClickEventArgs e)
        {
            string id = (string)Session["ImpId"];
            string lrno = txtLicenceRefNo.Text;
            string licDate = txtLicenceDate.Text;
            string desc = txtImpDesc.Text;
            string CIFInr = txtCIFValueINR.Text;
            string CIFFc = txtCIFValueFC.Text;
            string qty = txtImpQuantity.Text;
            string unit = ddlImpUnit.Text;
            string opInr = txtImpOPBalINR.Text;
            string opFc = txtImpOpBalFC.Text;
            string Opqty = txtImpOpBalQty.Text;
            string opUnit = txtImpUnit0.Text;
            string created = (string)Session["USER-NAME"];
            string createdate = DateTime.Now.ToString("dd/MM/yyyy");
            int result = 0;

            try
            {
                string query = "Update M_LicenceImport Set [Description] = '" + desc + "',[CIFValueINR]= '" + CIFInr + "',[CIFValueFC]='" + CIFFc + "',[Quantity]='" + qty + "'," +
                    "[CIFUnit]='" + unit + "',[OpBalINR]='" + opInr + "',[OpBalFC]='" + opFc + "',[OpBalQty]='" + Opqty + "',[OPUnit]='" + opUnit + "'," +
                    "[ModifiedBy]='" + created + "',[ModifiedDate]='" + createdate + "' Where LicenceImportID = '" + id + "'";

                result = GetSqlCommand(query);
                if (result != 0)
                {
                    ClearImpItem();
                    BindGridExpItems();
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "redirect", "alert('" + ex.Message + "');", true);
            }
        }

        protected void txtCIFValueINR_TextChanged(object sender, EventArgs e)
        {
            double cifvalue = Convert.ToDouble(txtCIFValueINR.Text);
            txtImpOPBalINR.Text = cifvalue.ToString();
        }

        protected void txtCIFValueFC_TextChanged(object sender, EventArgs e)
        {
            double cifvalue = Convert.ToDouble(txtCIFValueFC.Text);
            txtImpOpBalFC.Text = cifvalue.ToString();
        }

        protected void txtImpQuantity_TextChanged(object sender, EventArgs e)
        {
            double cifvalue = Convert.ToDouble(txtImpQuantity.Text);
            txtImpOpBalQty.Text = cifvalue.ToString();
        }

        protected void ddlImpUnit_SelectedIndexChanged(object sender, EventArgs e)
        {
            string cifvalue = ddlImpUnit.SelectedValue;
            txtImpUnit0.Text = cifvalue;
        }

        protected void txtFobValueINR_TextChanged(object sender, EventArgs e)
        {
            double Fobvalue = Convert.ToDouble(txtFobValueINR.Text);
            txtOPBalINR.Text = Fobvalue.ToString();
        }

        protected void txtFobValueFC_TextChanged(object sender, EventArgs e)
        {
            double Fobvalue = Convert.ToDouble(txtFobValueFC.Text);
            txtOPBalFC.Text = Fobvalue.ToString();
        }

        protected void txtQuantity_TextChanged(object sender, EventArgs e)
        {
            double Fobvalue = Convert.ToDouble(txtQuantity.Text);
            txtOPBalQty.Text = Fobvalue.ToString();
        }

        protected void ddlUnit_SelectedIndexChanged(object sender, EventArgs e)
        {
            double Fobvalue = Convert.ToDouble(ddlUnit.SelectedValue);
            txtUnit0.Text = Fobvalue.ToString();
        }

        protected void btnNew_Click(object sender, EventArgs e)
        {
            txtDescription.Text = string.Empty;
            txtFobValueINR.Text = string.Empty;
            txtOPBalINR.Text = string.Empty;
            txtFobValueFC.Text = string.Empty;
            txtOPBalFC.Text = string.Empty;
            txtQuantity.Text = string.Empty;
            txtOPBalQty.Text = string.Empty;
            ddlUnit.SelectedIndex = 0;
            txtUnit0.Text = string.Empty;
            btnAdd.Visible = true;
            btnUpdate.Visible = false;
        }

        protected void btnImpNew_Click(object sender, EventArgs e)
        {
            txtImpDesc.Text = string.Empty;
            txtCIFValueINR.Text = string.Empty;
            txtImpOPBalINR.Text = string.Empty;
            txtCIFValueFC.Text = string.Empty;
            txtImpOpBalFC.Text = string.Empty;
            txtImpQuantity.Text = string.Empty;
            ddlImpUnit.SelectedIndex = 0;
            txtImpUnit0.Text = string.Empty;
            btnImpUpdate.Visible = false;
            btnImpAdd.Visible = true;
        }

        protected void gvExportItems_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (gvExportItems.SelectedRow.Cells[2].Text != null)
            {
                btnUpdate.Visible = true;
                btnAdd.Visible = false;
                Session["ExpId"] = gvExportItems.SelectedRow.Cells[2].Text;
                string desc = gvExportItems.SelectedRow.Cells[3].Text;
                string FobInr = gvExportItems.SelectedRow.Cells[3].Text;
                string FobFc = gvExportItems.SelectedRow.Cells[4].Text;
                string qty = gvExportItems.SelectedRow.Cells[5].Text;
                string unit = gvExportItems.SelectedRow.Cells[6].Text;
                string opInr = gvExportItems.SelectedRow.Cells[7].Text;
                string opFc = gvExportItems.SelectedRow.Cells[8].Text;
                string Opqty = gvExportItems.SelectedRow.Cells[9].Text;
                string opUnit = gvExportItems.SelectedRow.Cells[10].Text;

                txtDescription.Text = desc;
                txtFobValueINR.Text = FobInr;
                txtOPBalINR.Text = opInr;
                txtFobValueFC.Text = FobFc;
                txtOPBalFC.Text = opFc;
                txtQuantity.Text = qty;
                txtOPBalQty.Text = Opqty;
                ddlUnit.SelectedValue = unit;
                txtUnit0.Text = opUnit;
            }
        }

        protected void gvImportItems_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (gvImportItems.SelectedRow.Cells[2].Text != null)
            {
                btnImpUpdate.Visible = true;
                btnImpAdd.Visible = false;
                Session["ImpId"] = gvImportItems.SelectedRow.Cells[2].Text;
                string desc = gvImportItems.SelectedRow.Cells[3].Text;
                string CIFInr = gvImportItems.SelectedRow.Cells[4].Text;
                string CIFFc = gvImportItems.SelectedRow.Cells[5].Text;
                string qty = gvImportItems.SelectedRow.Cells[6].Text;
                string unit = gvImportItems.SelectedRow.Cells[7].Text;
                string opInr = gvImportItems.SelectedRow.Cells[8].Text;
                string opFc = gvImportItems.SelectedRow.Cells[9].Text;
                string Opqty = gvImportItems.SelectedRow.Cells[10].Text;
                string opUnit = gvImportItems.SelectedRow.Cells[11].Text;

                txtImpDesc.Text = desc;
                txtCIFValueINR.Text = CIFInr;
                txtImpOPBalINR.Text = opInr;
                txtCIFValueFC.Text = CIFFc;
                txtImpOpBalFC.Text = opFc;
                txtImpQuantity.Text = qty;
                txtOPBalQty.Text = Opqty;
                ddlImpUnit.SelectedValue = unit;
                txtImpUnit0.Text = opUnit;
            }
        }

        protected void btnDelete_Click(object sender, ImageClickEventArgs e)
        {
            ImageButton btndel = sender as ImageButton;
            GridViewRow row = (GridViewRow)btndel.NamingContainer;
            string TransId = row.Cells[2].Text;
            string qry = "Delete from M_LicenceExport where TransId = '" + TransId + "' ";
            int i = GetSqlCommand(qry);
            if (i == 1)
            {
                BindGridExpItems();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "redirect", "alert('Deleted Successfully');", true);
            }
        }

        protected void btnImpDelete_Click(object sender, ImageClickEventArgs e)
        {
            ImageButton btndel = sender as ImageButton;
            GridViewRow row = (GridViewRow)btndel.NamingContainer;
            string TransId = row.Cells[2].Text;
            string qry = "Delete from M_LicenceImport where TransId = '" + TransId + "' ";
            int i = GetSqlCommand(qry);
            if (i == 1)
            {
                BindGridImpItems();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "redirect", "alert('Deleted Successfully');", true);
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            string lrn = txtLicenceRefNo.Text;
            string ln = txtLicenceNo.Text;
            string lrnDate = txtLicenceDate.Text;
            string type = ddlSchemeType.SelectedValue;
            string schnotn = ddlSchemeNotn.SelectedValue;
            string slno = ddlSubSchemeNotn.SelectedValue;
            string customer = txtOrganization.Text;
            string expDate = txtExpiryDate.Text;
            string surrenderdate = txtSurrenderDate.Text;
            string regn = txtEdiRegn.Text;
            string edidate = txtEDIDate.Text;
            string currency = ddlCurency.SelectedValue;
            string port = ddlPortRegn.SelectedValue;
            string active = "True";
            string created = (string)Session["USER-NAME"];
            string Createdby = DateTime.Now.ToString();

           double totalvalue;
            double opBalance;

            if (txtTotalValue.Text == "")
            {
                totalvalue=0;
            }
            else
            {
                totalvalue = Convert.ToDouble(txtTotalValue.Text);
            }

            if (txtOpBalance.Text == "")
            {
                opBalance = 0;
            }
            else
            {
                opBalance = Convert.ToDouble(txtOpBalance.Text);
            }
            

            try
            {
                int Update = new int();
                string qry = "Insert Into [M_LicenceMaster]([LicenceRefNo],[Type],[SchemeNotn],[SchemeNotnNo],[LicenceNo],[LicenceDate]," +
                        "[LicenceExpiry],[Organization],[DateofSurrender],[EDIRegnNo],[EDIRegnDate],[Currency],[PortofRegn]," +
                        "[CreatedBy],[CreatedDate],[ModifiedBy],[ModifiedDate]) Values ( '" + lrn + "','" + type + "','" + schnotn + "','" + slno + "'," +
                        "'" + ln + "','" + lrnDate + "','" + expDate + "','" + customer + "','" + surrenderdate + "','" + regn + "','" + edidate + "','" + currency + "'," +
                        "'" + port + "','" + created + "','" + Createdby + "','" + created + "','" + Createdby + "')";
                int result = GetSqlCommand(qry);
                if (result != 0)
                {
                    string keycode = (string)Session["Keycode"];
                    Update = Utility.UpdateAutoNo(keyname, Convert.ToInt32(keycode), Utility.GetConnectionString());
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "redirect", "alert('Saved Successfully'); window.location.href='frmImpLicsenceMaster.aspx';", true);
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "redirect", "alert('" + ex.Message + "');", true);
            }
        }

    }
}