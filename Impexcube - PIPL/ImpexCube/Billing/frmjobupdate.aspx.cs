using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using MySql;
using MySql.Data.MySqlClient;
using System.Data.SqlClient;


public partial class frmjobupdate : System.Web.UI.Page
{
    string strconnJSU = (string)ConfigurationManager.AppSettings["ConnectionJobStages"];
    string strconn = (string)ConfigurationManager.AppSettings["ConnectionImpex"];
    string strconnImpexcube = (string)ConfigurationManager.AppSettings["ConnectionImpexCube"];

    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btnupdate_Click(object sender, EventArgs e)
    {
       
        if ((lbjobno.Items.Count == 0))
        {
            Response.Write("<script>alert('Please add Job no') </script>");

        }
        else
        {

            for (int i = 0; i <= lbjobno.Items.Count - 1; i++)
            {
                lbjobno.Items[i].Selected = true;
                string jobno = lbjobno.Items[i].Text;
                string dates = txtinvdate.Text;
                //string[] DT = dates.Split('/');
                //dates = DT[2] + "-" + DT[1] + "-" + DT[0];

                string billstatus = "update iworkreg_jobstatus set status_job='Y',bill_no ='" + ddlinvoiceno.Text + "',bill_date ='" + dates + "' where jobsno='" + jobno + "'";

                GetCommandMy(billstatus, strconnJSU);
                GetCommandSQL(billstatus, strconnImpexcube);

            }
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Successfully Updated');", true);
        }

      
      
    }

    protected void GetCommandMy(string Query, string connSTR)
    {

        MySqlConnection conn = new MySqlConnection(connSTR);
        conn.Open();
        MySqlCommand cmd = new MySqlCommand(Query, conn);
        cmd.CommandText = Query;
        cmd.Connection = conn;
        int res = cmd.ExecuteNonQuery();

    }

    protected void GetCommandSQL(string Query, string connSTR)
    {

        SqlConnection conn = new SqlConnection(connSTR);
        conn.Open();
        SqlCommand cmd = new SqlCommand(Query, conn);
        cmd.CommandText = Query;
        cmd.Connection = conn;
        int res = cmd.ExecuteNonQuery();

    }
    protected void btnexit_Click(object sender, EventArgs e)
    {
        Response.Redirect("index.aspx");
    }
    protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
    {

        if (ddlinvoiceno.SelectedValue != "~Select~")
            {
                SqlConnection cnn = new SqlConnection(strconn);
                cnn.Open();
                string query = "select * from iec_invoiceNew where invoice='" + ddlinvoiceno.SelectedValue + "'";
                SqlDataAdapter da = new SqlDataAdapter(query, cnn);
                DataSet ds = new DataSet();
                da.Fill(ds, "invno");
                DataRowView row = ds.Tables["invno"].DefaultView[0];
                txtinvdate.Text = row["invoiceDate"].ToString();
            }
            else
            {
                txtinvdate.Text = "";
            }
     
    }
    protected void btnadd_Click(object sender, EventArgs e)
    {
        MySqlConnection cnn = new MySqlConnection(strconnJSU);
        cnn.Open();
        string query = "select * from iworkreg_jobstatus where jobsno='" + txtjobno.Text + "'";
        MySqlDataAdapter da = new MySqlDataAdapter(query, cnn);
        DataSet ds = new DataSet();
        da.Fill(ds, "jobno");
        if (ds.Tables["jobno"].Rows.Count != 0)
        {

            if (txtjobno.Text != "")
            {
                lbjobno.Items.Insert(0,txtjobno.Text);
            }
            else
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Please enter the job no');", true);
        }
        else
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Please enter the correct Job no');", true);
    }
}
