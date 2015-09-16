using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.IO;
using System.Web.Util;
using System.Data.SqlClient;

namespace ImpexCube
{
    public partial class frmOutwardDetails : System.Web.UI.Page
    {
        string strconn = (string)ConfigurationManager.AppSettings["ConnectionDashboard"];
        string strQuery = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack == false)
            {

                string fdate = (string)Session["fdate"];
                txtFrom.Text = fdate;
                txtTo.Text = System.DateTime.Now.ToString("dd/MM/yyyy");
                GetCustomer();

            }
        }

        public DataSet GetData(string FDate, string TDate)
        {

            SqlConnection conn = new SqlConnection(strconn);
            string sqlQuery = "select distinct consignee from tbl_outward where Date between '" + FDate + "' and '" + TDate + "' ";
            SqlDataAdapter da = new SqlDataAdapter(sqlQuery, conn);
            DataSet ds = new DataSet();
            da.Fill(ds, "agent");
            return ds;

        }
        protected void BtnSearch_Click(object sender, EventArgs e)
        {


            try
            {
                GetReports();
                GrdRpt.DataSource = GetDataAll(strQuery);
                GrdRpt.DataBind();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('" + ex.Message + "');", true);
            }

            //}
        }
        protected void GetReports()
        {
            string TDate = "";
            string FDate = "";
            string fd = txtFrom.Text;
            string td = txtTo.Text;
            string[] FD = fd.Split('/');
            string[] TD = td.Split('/');
            string cName = drConsignee.SelectedValue;

            FDate = FD[2] + "/" + FD[1] + "/" + FD[0];
            TDate = TD[2] + "/" + TD[1] + "/" + TD[0];


            if (FDate == "")
                Response.Write("<script>alert('Please Give Doc Date') </script>");
            else
            {
                if (cName == "0")
                    strQuery = "select * from tbl_outward where Date between '" + FDate + "' and '" + TDate + "' ";
                else
                    strQuery = "select * from tbl_outward where Date between '" + FDate + "' and '" + TDate + "' and consignee='" + cName + "' ";

            }
        }
        public DataSet GetDataAll(string sqlQuery)
        {

            SqlConnection conn = new SqlConnection(strconn);
            //string sqlQuery = "select * from tbl_outward where Date between '" + FD + "' and '" + TD + "' ";
            SqlDataAdapter da = new SqlDataAdapter(sqlQuery, conn);
            DataSet ds = new DataSet();
            da.Fill(ds, "agent2");
            return ds;

        }


        protected void Export_Click(object sender, EventArgs e)
        {
            try
            {

                string sysDates = DateTime.Now.ToString("ddMMyyyy");
                string FileName = "OutWard" + sysDates;
                string strFileName = FileName + ".xls";
                BtnSearch_Click(sender, e);
                GridViewExportDet.ExportExcell(strFileName, GrdRpt);
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('" + ex.Message + "');", true);
            }
        }


        protected void BtnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Dashboard/frmDashboardMain.aspx", false);
        }
        protected void GrdRpt_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (e.Row.Cells[3].Text != "&nbsp;")
                {
                    DateTime bDate = Convert.ToDateTime(e.Row.Cells[3].Text);
                    e.Row.Cells[3].Text = bDate.ToString("dd/MM/yyyy");
                }
            }
        }
        protected void txtTo_TextChanged(object sender, EventArgs e)
        {
            GetCustomer();
        }
        protected void txtFrom_TextChanged(object sender, EventArgs e)
        {
            GetCustomer();
        }
        protected void GetCustomer()
        {
            try
            {
                string TDate = "";
                string FDate = "";
                string fd = txtFrom.Text;
                string td = txtTo.Text;
                string[] FD = fd.Split('/');
                string[] TD = td.Split('/');


                FDate = FD[2] + "/" + FD[1] + "/" + FD[0];
                TDate = TD[2] + "/" + TD[1] + "/" + TD[0];

                drConsignee.DataSource = GetData(FDate, TDate);
                drConsignee.DataTextField = "consignee";
                drConsignee.DataValueField = "consignee";
                drConsignee.DataBind();
                drConsignee.Items.Insert(0, new ListItem("~select~", "0"));
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('" + ex.Message + "');", true);
            }
        }
    }
}