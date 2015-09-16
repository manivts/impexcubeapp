using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using VTS.ImpexCube.Business;
namespace ImpexCube
{
    public partial class frmProductPopup : System.Web.UI.Page
    {
        string strcon = (string)ConfigurationManager.ConnectionStrings["Constr"].ConnectionString;
        VTS.ImpexCube.Business.ProductDetailsBL obj = new VTS.ImpexCube.Business.ProductDetailsBL();
        VTS.ImpexCube.Business.CommonBL obj1 = new VTS.ImpexCube.Business.CommonBL();
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            DataSet ds = obj1.GetProductPopup(txtSearch.Text);
            GridView1.DataSource = ds;
            GridView1.DataBind();
        }
        protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string productCode = GridView1.SelectedRow.Cells[1].Text;
            string productName = GridView1.SelectedRow.Cells[2].Text;
            string RITCCode = GridView1.SelectedRow.Cells[3].Text;
            string CTHNo = GridView1.SelectedRow.Cells[3].Text;
            Session["ProductCode"] = GridView1.SelectedRow.Cells[1].Text;
            Session["RITCCode"] = GridView1.SelectedRow.Cells[3].Text;
            string mode = Request.QueryString["mode"];
            if (mode == "ProductName")
            {
                ClientScript.RegisterStartupScript(typeof(GridView), "JavaScript", "GetProductExp('" + productName + "');", true);
            }
            else
            {
                ClientScript.RegisterStartupScript(typeof(GridView), "JavaScript", "GetProduct('" + productName + "');", true);
            }
        }

        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView1.DataSource = obj1.GetProductPopup(txtSearch.Text);
            GridView1.DataBind();
            GridView1.PageIndex = e.NewPageIndex;
            GridView1.DataBind();
        }
    }
}