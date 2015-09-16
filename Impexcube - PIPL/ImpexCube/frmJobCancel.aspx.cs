using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using MySql;
using MySql.Data.MySqlClient;
using System.Data.SqlClient;

namespace ImpexCube
{
    public partial class frmJobCancel : System.Web.UI.Page
    {
        string strconnJSU = (string)ConfigurationManager.AppSettings["connectionJSU"];
        string con = (string)ConfigurationManager.ConnectionStrings["Constr"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {


        }

        protected void btnJobCancel_Click(object sender, EventArgs e)
        {
            if (txtJobNo.Text != "")
            {
                string dat = System.DateTime.Now.ToString("dd/MM/yyyy 00.00");
                string[] dt5 = dat.Split('/');
                string date = dt5[2].Substring(0, 4) + "-" + dt5[1] + "-" + dt5[0];
                string sysdate = Convert.ToDateTime(date).ToString("yyyy-MM-dd 00:00:00");
                string datesql = System.DateTime.Now.ToString("dd/MM/yyyy");

                string update = "update iworkreg_jobstatus set status_job='CANC',Modified_By='" + (string)Session["USER-NAME"] + "',Modified_Date='" + sysdate + "' where job_no='" + txtJobNo.Text + "' ";
                updatemysql(update);
                UpdateSql(update);
                string UpdateJob = "update T_JobDetails set status_job='CANC',ModifiedBy='" + (string)Session["USER-NAME"] + "',ModifiedDate='" + sysdate + "' where jobno='" + txtJobNo.Text + "' ";
                UpdateSql(UpdateJob);

                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Job Cancelled');", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Please enter the Job No');", true);
            }
        }

        public int updatemysql(string sqlquery)
        {
            MySqlConnection conn = new MySqlConnection(strconnJSU);
            conn.Open();
            MySqlCommand cmd = new MySqlCommand(sqlquery, conn);
            MySqlDataAdapter da = new MySqlDataAdapter();
            MySqlCommand upcmd = new MySqlCommand(sqlquery, conn);
            upcmd.CommandText = sqlquery;
            upcmd.Connection = conn;
            da.SelectCommand = upcmd;
            int result = upcmd.ExecuteNonQuery();
            conn.Close();
            return result;

        }

        public void UpdateSql(string SqlQuery)
        {
            SqlConnection sqlConn = new SqlConnection(con);
            sqlConn.Open();
            SqlCommand cmd = new SqlCommand(SqlQuery, sqlConn);
            SqlDataAdapter da = new SqlDataAdapter();

            cmd.CommandText = SqlQuery;
            cmd.Connection = sqlConn;
            da.SelectCommand = cmd;
            int result = cmd.ExecuteNonQuery();
            sqlConn.Close();
        }

        protected void txtJobNo_TextChanged(object sender, EventArgs e)
        {
            DataSet ds = new DataSet();
            try
            {
                string Query = "select JOB_NO,party_name from iworkreg_jobstatus where JOB_NO like '%" + txtJobNo.Text + "%'";

                MySqlConnection con = new MySqlConnection(strconnJSU);
                con.Open();
                MySqlDataAdapter sd = new MySqlDataAdapter(Query, con);
                sd.Fill(ds, "jobno");
                con.Close();
                if (ds.Tables["jobno"].Rows.Count != 0)
                {
                    DataRowView row = ds.Tables["jobno"].DefaultView[0];
                    lblPartyName.Text=row["party_name"].ToString();
                 
                }
            }
            catch (Exception ex)
            {
                string Msg = ex.Message.ToString();
            }
        }
    }
}