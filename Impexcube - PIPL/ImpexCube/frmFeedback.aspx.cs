using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;

namespace WMS
{
    public partial class frmFeedback : System.Web.UI.Page
    {
        string strconn = (string)ConfigurationManager.ConnectionStrings["Constr"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack == false)            {
                string dates = DateTime.Now.ToString("dd'/'MM'/'yyyy");
            }
        }
        public static int GetInsertCmd(string sqlQry, string strcon)
        {
            int result;
            SqlConnection conn = new SqlConnection(strcon);
            conn.Open();
            SqlCommand cmd = new SqlCommand(sqlQry, conn);
            SqlDataAdapter da = new SqlDataAdapter();
            cmd.CommandText = sqlQry;
            cmd.Connection = conn;
            da.SelectCommand = cmd;
            result = cmd.ExecuteNonQuery();
            conn.Close();
            return result;
        }
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                string query = "Insert into Feedback(FormName,Description,CreatedBy,CreatedDate)" +
                    "Values('" + txtForm.Text + "'," +
                    "'" + txtDescription.Text + "','" + (string)Session["USER-NAME"] + "','" + DateTime.Now + "')";
                GetInsertCmd(query, strconn);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "redirect", "alert('Your FeebBack is Send '); window.location.href='frmFeedback.aspx';", true);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private string frmdatesplit(string frmdate)
        {
            string[] frmdate1 = frmdate.Split('/');
            string frmdate2 = frmdate1[1] + '/' + frmdate1[0] + '/' + frmdate1[2];
            return frmdate2;
        }

       
    }
}