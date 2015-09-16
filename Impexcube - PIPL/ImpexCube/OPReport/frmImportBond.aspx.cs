using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;

namespace ImpexCube
{
    public partial class frmImportBond : System.Web.UI.Page
    {
        string strcon = (string)ConfigurationManager.ConnectionStrings["Constr"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            pnlDBBond.Visible = false;
            pnlDoubleDutyBond.Visible = false;
            pnlHighSeaSaleContract.Visible = false;
            pnlagreementformat.Visible = false;
            pnlReexportBond.Visible = false;
            pnlRDBond.Visible = false;
        }

        protected void btnGenerate_Click(object sender, EventArgs e)
        {
            if (ddlbond.SelectedItem.Text != "Default")
            {
                if (ddlbond.SelectedItem.Text != "P.D.Bond")
                {
                    pnlDBBond.Visible=true;
                }
                else if (ddlbond.SelectedItem.Text != "Double Duty Bond")
                {
                    pnlDoubleDutyBond.Visible=true;
                }
                else if (ddlbond.SelectedItem.Text != "High Sea Sale Contract")
                {
                    pnlHighSeaSaleContract.Visible=true;
                }
                else if (ddlbond.SelectedItem.Text != "Agreement Format")
                {
                    pnlagreementformat.Visible=true;
                }
                else if (ddlbond.SelectedItem.Text != "ReExport Bond")
                {
                    pnlReexportBond.Visible=true;
                }
                else
                {
                    pnlRDBond.Visible = true;
                }
            }
            else
            {
            }
        }
    }
}