using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;

namespace ImpexCube.OPReport
{
    public partial class frmLicenceExpiry : System.Web.UI.Page
    {
        string strcon = (string)ConfigurationManager.ConnectionStrings["Constr"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {

        }
       

        protected void btngenerate_Click(object sender, EventArgs e)
        {
            StringBuilder Query = new StringBuilder();            
            if (txtlicencesexpire.Text != "")
            {
                Query.Append(" and  ='" + txtlicencesexpire.Text + "'");
            }
            string WhereQry = Query.ToString();
            string Qry = " where completed=0 and  1=1";
            Qry = Qry + WhereQry;
            SqlConnection conn = new SqlConnection(strcon);
            conn.Open();
            SqlDataAdapter da = new SqlDataAdapter(Qry, conn);
            DataSet ds = new DataSet();
            da.Fill(ds, "Table");
            if (ds.Tables[0].Rows.Count != 0)
            {
                conn.Close();
                gvLicenceExpiry.DataSource = ds;
                gvLicenceExpiry.DataBind();
                gvLicenceExpiry.Visible = true;
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "JavaScript", "alert('No Data Found');", true);
                gvLicenceExpiry.Visible = false;
            }
        }
    }
}