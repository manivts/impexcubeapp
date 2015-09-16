using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using VTS.ImpexCube.Data;
using System.Data;

namespace ImpexCube
{
    public partial class frmPopupSearch : System.Web.UI.Page
    {
        CommonDL objCommonDL = new CommonDL();
        string strcon = (string)ConfigurationManager.ConnectionStrings["Constr"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
               
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            string Query = "Select JobNo,BEHeading From T_Importer Where BEHeading Like '%"+txtSearch.Text+"%' ";
            DataSet ds = objCommonDL.GetDataSet(Query);

            if (ds.Tables["Table"].Rows.Count != 0)
            {
                gvSearch.DataSource = null;
                gvSearch.DataSource = ds;
                gvSearch.DataBind();
            }

        }

        protected void gvSearch_SelectedIndexChanged(object sender, EventArgs e)
        {
            string JobNo = gvSearch.SelectedRow.Cells[1].Text;
            Session["JobNo"] = JobNo;
            Response.Redirect("frmPrintCheckList.aspx");
           // System.Web.UI.ScriptManager.RegisterClientScriptBlock(Page, typeof(Page), "Script", "window.close();", true);
        }
    }
}