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
    public partial class frmDocumentMaster : System.Web.UI.Page
    {
        string strconn = (string)ConfigurationManager.AppSettings["ConnectionDashboard"];

        protected void Page_Load(object sender, EventArgs e)
        {
            GridBind();
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
                string InsertQuery = "insert into M_DocumentMaster(DocumentName)values('" + txtDocumentName.Text + "')";
                SqlConnection sqlConn = new SqlConnection(strconn);
                sqlConn.Open();
                SqlCommand cmd = new SqlCommand(InsertQuery, sqlConn);
                int result = cmd.ExecuteNonQuery();
                sqlConn.Close();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "redirect", "alert('Saved Successfully'); window.location.href='frmDocumentMaster.aspx';", true);
        }

        public void GridBind()
        {
            string selectquery = "select * from M_DocumentMaster";
                DataSet ds = new DataSet();
                SqlConnection sqlConn = new SqlConnection(strconn);
                sqlConn.Open();
                SqlDataAdapter da = new SqlDataAdapter(selectquery, sqlConn);

                da.Fill(ds, "Document");
                sqlConn.Close();
                if (ds.Tables["Document"].Rows.Count != 0)
                {
                    gvDocument.DataSource = ds;
                    gvDocument.DataBind();
                }

        }

        protected void gvDocument_SelectedIndexChanged(object sender, EventArgs e)
        {
            Session["ID"] = gvDocument.SelectedRow.Cells[1].Text;
            txtDocumentName.Text = gvDocument.SelectedRow.Cells[2].Text;
            btnUpdate.Visible = true;
            btnSave.Visible = false;
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            if (txtDocumentName.Text != "")
            {
            string UpdateQuery = "update M_DocumentMaster set DocumentName='" + txtDocumentName.Text + "' where ID='" + (string)Session["ID"] + "' ";
            SqlConnection sqlConn = new SqlConnection(strconn);
            sqlConn.Open();
            SqlCommand cmd = new SqlCommand(UpdateQuery, sqlConn);

            int result = cmd.ExecuteNonQuery();
            sqlConn.Close();

            ScriptManager.RegisterStartupScript(this, this.GetType(), "redirect", "alert('Updated Successfully'); window.location.href='frmDocumentMaster.aspx';", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "redirect", "alert('Please enter the Document Name');", true);
            }
        }


        protected void btnNew_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/frmDocumentMaster.aspx");
        }

        protected void btnExit_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/HomePage.aspx");
        }


    }
}