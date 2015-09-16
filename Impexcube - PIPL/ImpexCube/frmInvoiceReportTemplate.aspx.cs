using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using System.Text;
using System.Data.SqlClient;

namespace ImpexCube
{
    public partial class frmInvoiceReportTemplate : System.Web.UI.Page
    {
        string strconn = (string)ConfigurationManager.ConnectionStrings["Constr"].ConnectionString;
        string List1 = "";
        string List2 = "";
        string DocDetl = "";
        StringBuilder sb = new StringBuilder();
        string FieldName = "";
        string ChargeList = "";
      
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack == false)
            {
                GridChargeLoad();
                GridFieldLoad();

            }
        }

        public void GridChargeLoad()
        {
            string selectquery = "select charge_desc from M_Charge WHERE cCode IS NULL";
            DataSet ds = new DataSet();
            SqlConnection sqlConn = new SqlConnection(strconn);
            sqlConn.Open();
            SqlDataAdapter da = new SqlDataAdapter(selectquery, sqlConn);

            da.Fill(ds, "Document");
            sqlConn.Close();
            if (ds.Tables["Document"].Rows.Count != 0)
            {
                gvChargelist.DataSource = ds;
                gvChargelist.DataBind();
            }

        }

        public void GridFieldLoad()
        {
            string selectquery = "select ReportName,TableField from T_Invoice_Heading";
            DataSet ds = new DataSet();
            SqlConnection sqlConn = new SqlConnection(strconn);
            sqlConn.Open();
            SqlDataAdapter da = new SqlDataAdapter(selectquery, sqlConn);

            da.Fill(ds, "Document");
            sqlConn.Close();
            if (ds.Tables["Document"].Rows.Count != 0)
            {
                gvFieldName.DataSource = ds;
                gvFieldName.DataBind();
            }
        }

        protected void chkField_CheckedChanged(object sender, EventArgs e)
        {
           // GetFieldName();
        }

        protected void GetFieldName()
        {
            try
            {
                foreach (GridViewRow row1 in gvFieldName.Rows)
                {
                    CheckBox chk1 = (CheckBox)row1.FindControl("chkField");                    
                    if (chk1.Checked)
                    {
                        
                        string dtl1 ="["+ row1.Cells[2].Text+"]";
                        List1 = List1 + dtl1 +",";
                        
                    }
                }

              
                //DocDetl = (string)Session["DocDetl"];
                //sb.Append(DocDetl);
               // sb.Append(List1);
              //  sb.Append(List2);

               Session["FieldName"] =List1 + "charge_desc"+","+ "amount"+",";

            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('" + ex.Message + "');", true);
            }
        }

        protected void chkcharge_CheckedChanged(object sender, EventArgs e)
        {
            //GetChargeList();
        }

        protected void GetChargeList()
        {
            try
            {
                foreach (GridViewRow row1 in gvChargelist.Rows)
                {
                    CheckBox chk1 = (CheckBox)row1.FindControl("chkcharge");
                    if (chk1.Checked)
                    {
                        string dtl1 ="["+ row1.Cells[1].Text+"]";
                        List2 = List2 + dtl1 + ",";
                    }
                }


                //DocDetl = (string)Session["DocDetl"];
                //sb.Append(DocDetl);
                //sb.Append(List1);
               // sb.Append(List2);

                Session["ChargeList"] = List2;

            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('" + ex.Message + "');", true);
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (txtTemplateName.Text != "")
            {
                GetFieldName();
                GetChargeList();
                string chargelis = (string)Session["ChargeList"];
                string FieldNam = (string)Session["FieldName"];
                chargelis = chargelis.TrimEnd(',');
                FieldNam = FieldNam.TrimEnd(',');

                string query = "insert into T_InvoiceReportTemplate(TemplateName,ChargeList,FieldList) values('" + txtTemplateName.Text + "','" + chargelis + "','" + FieldNam + "')";

                SqlConnection con = new SqlConnection(strconn);
                con.Open();
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.ExecuteNonQuery();
                con.Close();
                Clear();
                Response.Write("<script>alert('Successfully Saved')</script>");
            }
            else
            {
                Response.Write("<script>alert('Please enter the Template Name')</script>");
            }
        }

        protected void chkFieldAll_CheckedChanged(object sender, EventArgs e)
        {
            
        }

        public void Clear()
        {
            gvChargelist.DataBind();
            gvFieldName.DataBind();
        }

       
    }
}