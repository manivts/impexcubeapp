using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

namespace ImpexCube
{
    public partial class frmSVBRef : System.Web.UI.Page
    {
        string strcon = (string)ConfigurationManager.ConnectionStrings["Constr"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindDrop();
                Gridload();
            }
        }

        protected void btnSaveRelation_Click(object sender, EventArgs e)
        {
            int result=0;
            string insert = "insert into M_SVBRef (ConsignorName,ConsignorAddress,ConsignorCountry,ConsigneeName,ConsigneeAddress,ConsigneeCountry,BuyerSellerRelated,Relation, "+
            " Base,Condition,SVBLoad,SVBRefOn,SVBRefDate,CustomHouse,LoadingOn,LoadingRateAssb,LoadingRateAssbStatus,LoadingRateDuty,LoadingRateDutyStatus,CreatedBy,CreatedDate) "+
            " values ('"+txtConsignorName.Text+"','"+txtConsignorAddress.Text+"','"+ddlConsignorCountry.SelectedValue+"','"+txtConsigneeName.Text+"','"+txtConsigneeAddress.Text+"', "+
            " '"+ddlConsigneeCountry.SelectedValue+"','"+chkBuyer.Checked+"','"+txtRelation.Text+"','"+txtRelationBase.Text+"','"+txtRelationCondition.Text+"','"+chkSVB.Checked+"', "+
            " '"+txtSVBRelation.Text+"','"+txtSVBDate.Text+"','"+txtCustomHouse.Text+"','"+ddlLoadingOn.SelectedValue+"','"+txtLoadingRateAssbl.Text+"','"+ddlLoadingAssblStatus.SelectedValue+"', "+
            " '" + txtLoadingDuty.Text + "','" + ddlLoadingDutyStatus.SelectedValue + "','" + (string)Session["USER-NAME"] + "','"+System.DateTime.Now+"')";
            SqlConnection sqlConn = new SqlConnection(strcon);
            sqlConn.Open();
            SqlCommand cmd = new SqlCommand(insert, sqlConn);
            result= cmd.ExecuteNonQuery();
            sqlConn.Close();
            if (result == 1)
            {
                Gridload();
                Clear();
                string mess = "Successfully Saved";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('" + mess + "');", true);
            }
        }

        protected void btnCancelRelation_Click(object sender, EventArgs e)
        {

        }

        protected void chkBuyer_CheckedChanged(object sender, EventArgs e)
        {
            if (chkBuyer.Checked == true)
            {
                txtRelation.Enabled = true;
                txtRelationBase.Enabled = true;
                txtRelationCondition.Enabled = true;
            }
            else
            {
                txtRelation.Enabled = false;
                txtRelationBase.Enabled = false;
                txtRelationCondition.Enabled = false;
            }
        }

        protected void chkSVB_CheckedChanged(object sender, EventArgs e)
        {
            if (chkSVB.Checked == true)
            {
                txtSVBRelation.Enabled = true;
                txtSVBDate.Enabled = true;
                txtCustomHouse.Enabled = true;
                ddlLoadingOn.Enabled = true;
                txtLoadingRateAssbl.Enabled = true;
                ddlLoadingAssblStatus.Enabled = true;
                txtLoadingDuty.Enabled = true;
                ddlLoadingDutyStatus.Enabled = true;
            }
            else
            {
                txtSVBRelation.Enabled = false;
                txtSVBDate.Enabled = false;
                txtCustomHouse.Enabled = false;
                ddlLoadingOn.Enabled = false;
                txtLoadingRateAssbl.Enabled = false;
                ddlLoadingAssblStatus.Enabled = false;
                txtLoadingDuty.Enabled = false;
                ddlLoadingDutyStatus.Enabled = false;
            }
        }

        public void BindDrop()
        {
            SqlConnection con = new SqlConnection(strcon);
            string query = "select CountryCode,CountryName from M_Country order by CountryName asc";
            SqlDataAdapter da = new SqlDataAdapter(query, con);
            DataSet ds = new DataSet();
            da.Fill(ds, "Country");
            if (ds.Tables["Country"].Rows.Count != 0)
            {
                ddlConsigneeCountry.DataSource = ds;
                ddlConsigneeCountry.DataTextField = "CountryName";
                ddlConsigneeCountry.DataValueField = "CountryCode";
                ddlConsigneeCountry.DataBind();

                ddlConsignorCountry.DataSource = ds;
                ddlConsignorCountry.DataTextField = "CountryName";
                ddlConsignorCountry.DataValueField = "CountryCode";
                ddlConsignorCountry.DataBind();
            }
        }

        public void Gridload()
        { 
         SqlConnection con = new SqlConnection(strcon);
         string query = "select ID,ConsignorName,ConsignorAddress,ConsignorCountry,ConsigneeName,ConsigneeAddress,ConsigneeCountry,BuyerSellerRelated,Relation, " +
            " Base,Condition,SVBLoad,SVBRefOn,SVBRefDate,CustomHouse,LoadingOn,LoadingRateAssb,LoadingRateAssbStatus,LoadingRateDuty,LoadingRateDutyStatus from M_SVBRef";
            SqlDataAdapter da = new SqlDataAdapter(query, con);
            DataSet ds = new DataSet();
            da.Fill(ds, "Country");
            if (ds.Tables["Country"].Rows.Count != 0)
            {
                gvSVBRef.DataSource = ds;
                gvSVBRef.DataBind();
            }
        }

        protected void gvSVBRef_SelectedIndexChanged(object sender, EventArgs e)
        {
            Session["ID"] = gvSVBRef.SelectedRow.Cells[1].Text;
            txtConsignorName.Text = gvSVBRef.SelectedRow.Cells[2].Text;
            txtConsignorAddress.Text = gvSVBRef.SelectedRow.Cells[3].Text;
            ddlConsignorCountry.SelectedValue = gvSVBRef.SelectedRow.Cells[4].Text;
            txtConsigneeName.Text = gvSVBRef.SelectedRow.Cells[5].Text;
            txtConsigneeAddress.Text = gvSVBRef.SelectedRow.Cells[6].Text;
            ddlConsigneeCountry.SelectedValue = gvSVBRef.SelectedRow.Cells[7].Text;
            chkBuyer.Checked = Convert.ToBoolean(gvSVBRef.SelectedRow.Cells[8].Text);
            txtRelation.Text = gvSVBRef.SelectedRow.Cells[9].Text;
            txtRelationBase.Text = gvSVBRef.SelectedRow.Cells[10].Text;
            txtRelationCondition.Text = gvSVBRef.SelectedRow.Cells[11].Text;
            chkSVB.Checked = Convert.ToBoolean(gvSVBRef.SelectedRow.Cells[12].Text);
            txtSVBRelation.Text = gvSVBRef.SelectedRow.Cells[13].Text;
            txtSVBDate.Text = gvSVBRef.SelectedRow.Cells[14].Text;
            txtCustomHouse.Text = gvSVBRef.SelectedRow.Cells[15].Text;
            ddlLoadingOn.SelectedValue= gvSVBRef.SelectedRow.Cells[16].Text;
            txtLoadingRateAssbl.Text = gvSVBRef.SelectedRow.Cells[17].Text;
            ddlLoadingAssblStatus.SelectedValue = gvSVBRef.SelectedRow.Cells[18].Text;
            txtLoadingDuty.Text = gvSVBRef.SelectedRow.Cells[19].Text;
            ddlLoadingDutyStatus.SelectedValue = gvSVBRef.SelectedRow.Cells[20].Text;
            btnSaveRelation.Visible = false;
            btnUpdate.Visible = true;
            chkBuyer_CheckedChanged(sender, e);
            chkSVB_CheckedChanged(sender, e);
        }

        public void Clear()
        {
            Session["ID"] = null;
            txtConsignorName.Text = "";
            txtConsignorAddress.Text ="";
            ddlConsignorCountry.SelectedValue = "~Select~";
            txtConsigneeName.Text = "";
            txtConsigneeAddress.Text = "";
            ddlConsigneeCountry.SelectedValue = "~Select~";
            chkBuyer.Checked = false;
            txtRelation.Text = "";
            txtRelationBase.Text = "";
            txtRelationCondition.Text = "";
            chkSVB.Checked = false;
            txtSVBRelation.Text = "";
            txtSVBDate.Text = "";
            txtCustomHouse.Text ="";
            ddlLoadingOn.SelectedValue = "~Select~";
            txtLoadingRateAssbl.Text = "";
            ddlLoadingAssblStatus.SelectedValue = "~Select~";
            txtLoadingDuty.Text = "";
            ddlLoadingDutyStatus.SelectedValue = "~Select~";
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            int result = 0;
            string update = "update M_SVBRef set ConsignorName='" + txtConsignorName.Text + "',ConsignorAddress='" + txtConsignorAddress.Text + "',ConsignorCountry='" + ddlConsignorCountry.SelectedValue + "', " +
            " ConsigneeName='" + txtConsigneeName.Text + "',ConsigneeAddress='" + txtConsigneeAddress.Text + "',ConsigneeCountry='" + ddlConsigneeCountry.SelectedValue + "',BuyerSellerRelated='" + chkBuyer.Checked + "',Relation='" + txtRelation.Text + "', " +
            " Base='" + txtRelationBase.Text + "',Condition='" + txtRelationCondition.Text + "',SVBLoad='" + chkSVB.Checked + "',SVBRefOn='" + txtSVBRelation.Text + "',SVBRefDate='" + txtSVBDate.Text + "', " +
            " CustomHouse='" + txtCustomHouse.Text + "',LoadingOn='" + ddlLoadingOn.SelectedValue + "',LoadingRateAssb='" + txtLoadingRateAssbl.Text + "',LoadingRateAssbStatus='" + ddlLoadingAssblStatus.SelectedValue + "', " +
            " LoadingRateDuty='" + txtLoadingDuty.Text + "',LoadingRateDutyStatus='" + ddlLoadingDutyStatus.SelectedValue + "',ModifiedBy='" + (string)Session["USER-NAME"] + "',ModifiedDate='" + System.DateTime.Now + "' where ID='"+(string)Session["ID"]+"' " ;
           
            SqlConnection sqlConn = new SqlConnection(strcon);
            sqlConn.Open();
            SqlCommand cmd = new SqlCommand(update, sqlConn);
            result = cmd.ExecuteNonQuery();
            sqlConn.Close();
            if (result == 1)
            {
                Gridload();
                Clear();
                btnUpdate.Visible = false;
                btnSaveRelation.Visible = true;
                string mess = "Successfully Updated";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('" + mess + "');", true);
            }
        }

        protected void txtConsignorName_TextChanged(object sender, EventArgs e)
        {

            string Consignor = txtConsignorName.Text;
            SqlConnection conn = new SqlConnection(strcon);
            conn.Open();
            string qry = "select Address1,Country,CountryCode from View_AccountMaster where AccountName = '" + Consignor + "'";
            SqlDataAdapter da = new SqlDataAdapter(qry, conn);
            DataSet ds = new DataSet();
            da.Fill(ds, "DATA");
            if (ds.Tables["DATA"].Rows.Count != 0)
            {
                DataRowView row1 = ds.Tables["DATA"].DefaultView[0];
                txtConsignorAddress.Text = row1["Address1"].ToString();
                ddlConsignorCountry.SelectedValue = row1["CountryCode"].ToString();
            }
            conn.Close();
        }

        protected void txtConsigneeName_TextChanged(object sender, EventArgs e)
        {
            string Consigne = txtConsigneeName.Text;
            string Consignee;
            SqlConnection conn = new SqlConnection(strcon);
            conn.Open();
            string qry = "select Distinct Address1,Country,CountryCode from View_AccountMaster where AccountName = '" + Consigne + "' and AccountType= 'Customer' ";
            SqlDataAdapter da = new SqlDataAdapter(qry, conn);
            DataSet ds = new DataSet();
            da.Fill(ds, "DATA");
            if (ds.Tables["DATA"].Rows.Count != 0)
            {
                DataRowView row1 = ds.Tables["DATA"].DefaultView[0];
                txtConsigneeAddress.Text = row1["Address1"].ToString();
                ddlConsigneeCountry.SelectedValue = row1["CountryCode"].ToString();
            }
            conn.Close();
        }
    }
}