using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

namespace ImpexCube.OPReport
{
    public partial class frmImportItemReport : System.Web.UI.Page
    {
        string strcon = (string)ConfigurationManager.ConnectionStrings["Constr"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            txtjobdate.Text = "01/01/2014";
        }
        public static string removespace(string dates)
        {
            string[] dat = dates.Split(' ');
            string da = dat[0];
            return da;
        }
        public static string ChangeDate(string date)
        {
            string[] dates = date.Split('/');
            string da = dates[0] + '/' + dates[1] + '/' + dates[2];
            return removespace(da);
        }
        private static string datereplace(string date)
        {
            string[] a = date.Split('-');
            date = a[1] + "-" + a[0] + "-" + a[2];
            return date;
        }
        protected void btnGenerate_Click(object sender, EventArgs e)
        {
            
        StringBuilder Query = new StringBuilder();
            string fromd = txtjobdate.Text.Replace('/', '-');
            
            DateTime fromdate = DateTime.ParseExact(fromd, "dd-MM-yyyy", null).Date;                        
            string fromdates = datereplace(fromdate.ToString("dd-MM-yyyy"));
            if (txtjobdate.Text != "")
            {
                Query.Append(" and JobReceivedDate >='" + fromdates + "'");
            }
            if (txtjobno.Text != "")
            {
                Query.Append(" and dbo.T_JobCreation.JobNo = '" + txtjobno.Text + "' or Importer='" + txtjobno.Text + "'");
            }
            
            if (txtproductdessc.Text != "")
            {
                Query.Append(" or ProductDesc like '%" + txtproductdessc.Text + "'");
            }           
            
            string WhereQry = Query.ToString();
            string Qry = " SELECT dbo.T_JobCreation.BENo, dbo.T_JobCreation.BEDate,dbo.T_JobCreation.TotalInvoiceValue, dbo.T_Product.ProductDesc, dbo.T_Product.Qty, dbo.T_Product.Unit, dbo.T_Product.UnitPrice,dbo.T_Product.Amount, dbo.T_Product.AssableValue, dbo.T_Product.TotBasicDutyAmt, dbo.T_ShipmentDetails.MasterBLNo, dbo.T_ShipmentDetails.HouseBLNo,dbo.T_JobCreation.JobNo, dbo.T_Importer.Importer, dbo.T_JobCreation.JobReceivedDate FROM dbo.T_JobCreation INNER JOIN dbo.T_Product ON dbo.T_JobCreation.JobNo = dbo.T_Product.JobNo INNER JOIN dbo.T_ShipmentDetails ON dbo.T_JobCreation.JobNo = dbo.T_ShipmentDetails.JobNo INNER JOIN dbo.T_Importer ON dbo.T_JobCreation.JobNo = dbo.T_Importer.JobNo  and  1=1";
            Qry = Qry + WhereQry;
            SqlConnection conn = new SqlConnection(strcon);
            conn.Open();


            SqlDataAdapter da = new SqlDataAdapter(Qry, conn);
            DataSet ds = new DataSet();
            da.Fill(ds, "Table");
            if (ds.Tables[0].Rows.Count != 0)
            {
                conn.Close();
                gvimportitemreport.DataSource = ds;
                gvimportitemreport.DataBind();
                gvimportitemreport.Visible = true;
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "JavaScript", "alert('No Data Found');", true);
                gvimportitemreport.Visible = false;
            }
        }

        protected void ddlreportfor_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtjobno.Text = "";
            Session["type"] = "";
            Session["type"]=ddlreportfor.SelectedItem.Text;
        }
    }
}