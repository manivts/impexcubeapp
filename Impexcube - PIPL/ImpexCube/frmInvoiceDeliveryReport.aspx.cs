using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Web.UI.HtmlControls;

namespace ImpexCube
{
    public partial class frmInvoiceDeliveryReport : System.Web.UI.Page
    {
        string strconn = (string)ConfigurationManager.AppSettings["ConnectionDashboard"];
        string strcon = (string)ConfigurationManager.ConnectionStrings["Constr"].ConnectionString;
        StringBuilder Query = new StringBuilder();
        protected void Page_Load(object sender, EventArgs e)
        {
            Label pagename;
            pagename = (Label)Master.FindControl("lblName");
            pagename.Text = "Delivery-Invoice Status";
            if (!IsPostBack)
            {
                txtfromdate.Text = (string)Session["fdate"];
                txttodate.Text = DateTime.Now.ToString("dd/MM/yyyy");
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            string FD = txtfromdate.Text;
            string TD = txttodate.Text;
            string sMM = FD.Substring(3, 2);
            string sDD = FD.Substring(0, 2);
            string sYY = FD.Substring(6, 4);            
            FD = sMM + "/" + sDD + "/" + sYY;
            Session["QUERY"] = "";

            string eMM = TD.Substring(3, 2);
            string eDD = TD.Substring(0, 2);
            string eYY = TD.Substring(6, 4);
            TD = eMM + "/" + eDD + "/" + eYY;

            DateTime fd = Convert.ToDateTime(FD);
            DateTime td = Convert.ToDateTime(TD);

            string fDate = fd.ToString("yyyy-MM-dd");
            string tDate = td.ToString("yyyy-MM-dd");
            SqlConnection conn = new SqlConnection(strconn);
            conn.Open();
            string qry = "Select Distinct JobNo,ImporterName,Mode,LoadType,JobStatus,Convert(varchar(10),StatusDate,103) as StatusDate,invoice,Convert(varchar(10),cast(invoiceDate as datetime),103) as invoiceDate from View_DeliveryInvoiceStatus where StatusDate>='" + fDate + "' and StatusDate<='" + tDate + "'  order by StatusDate asc ";
            SqlCommand Cmd = new SqlCommand(qry, conn);
            SqlDataAdapter da = new SqlDataAdapter(Cmd);
            DataSet ds = new DataSet();
            da.Fill(ds, "Table");
            if (ds.Tables[0].Rows.Count != 0)
            {
                conn.Close();
                GridView2.DataSource = ds;
                GridView2.DataBind();
            }
            else
            {
                GridView2.DataSource = null;
                GridView2.DataBind();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "JavaScript", "alert('No Data Found');", true);

            }
        }

        protected void btnexporttoexc_Click(object sender, EventArgs e)
        {
            try
            {
                string sysDates = DateTime.Now.ToString("dd/MM/yyyy");
                string FileName = sysDates;
                string strFileName = FileName + ".xls";
                btnSearch_Click(sender, e);
                //GridViewExportUtil.ExportExcell(strFileName, Grdiworkreg);

                string attachment = "attachment; filename=" + strFileName + " ";
                Response.ClearContent();
                Response.AddHeader("content-disposition", attachment);
                Response.ContentType = "application/ms-excel";

                StringWriter sw = new StringWriter();
                HtmlTextWriter htw = new HtmlTextWriter(sw);

                HtmlForm frm = new HtmlForm();
                GridView2.AllowPaging = false;

                GridView2.Parent.Controls.Add(frm);
                frm.Attributes["runat"] = "server";
                frm.Controls.Add(GridView2);
                frm.RenderControl(htw);

                Response.Write(sw.ToString());
                Response.End();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('" + ex.Message + "');", true);
            }
        }
    }
}