using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace ImpexCube.OPReport
{
    public partial class frmLicenceUtilization : System.Web.UI.Page
    {
        string strcon = (string)ConfigurationManager.ConnectionStrings["Constr"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btngenerate_Click(object sender, EventArgs e)
        {
            StringBuilder Query = new StringBuilder();
            if (txtLicrefno.Text != "")
            {
                Query.Append(" and  ='" + txtLicrefno.Text + "'");
            }
            if (ddlReporttype.SelectedItem.Text != "-Select-")
            {
                Query.Append(" and  = '" + ddlReporttype.SelectedItem.Text + "'");
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
                DataRowView row1 = ds.Tables["Table"].DefaultView[0];
               lblowner.Text=row1[""].ToString();
               lbllicenceno.Text=row1[""].ToString();
               lblediregnno.Text=row1[""].ToString();
               lbltype.Text=row1[""].ToString();
               lblLicenceDate.Text=row1[""].ToString();
               lblregndate.Text=row1[""].ToString();
               lblavailablevalue.Text=row1[""].ToString();
               lblExpirydate.Text=row1[""].ToString();
               lblportofregn.Text = row1[""].ToString();
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "JavaScript", "alert('No Data Found');", true);
                
            }
        }
    }
}