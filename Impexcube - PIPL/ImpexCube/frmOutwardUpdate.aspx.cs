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
using System.Data.SqlClient;

namespace ImpexCube
{
    public partial class Dashboard_frmOutwardUpdate : System.Web.UI.Page
    {
        string strconn = (string)ConfigurationManager.AppSettings["ConnectionDashboard"];
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack == false)
            {

            }
        }
        protected void BtnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                string AwbNo = txtAWBNo.Text;
                SqlConnection Conn = new SqlConnection(strconn);
                string Query = "select * from TBL_Outward where awbNo='" + AwbNo + "'";
                SqlDataAdapter da2 = new SqlDataAdapter(Query, Conn);
                DataSet ds2 = new DataSet();
                da2.Fill(ds2, "Outward");
                if (ds2.Tables["Outward"].Rows.Count == 0)
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Please Give Valid Awb No');", true);
                else
                {
                    DataRowView row = ds2.Tables["Outward"].DefaultView[0];
                    txtConsignee.Text = row["Consignee"].ToString();
                    string dddate = row["Date"].ToString();
                    string[] dt = dddate.Split('/');
                    txtDate.Text = dt[2] + "/" + dt[1] + "/" + dt[0];
                    txtCity.Text = row["City"].ToString();
                    txtDDetails.Text = row["Details"].ToString();
                    txtSentBy.Text = row["sentby"].ToString();
                    txtJobs.Text = row["jobno"].ToString();

                    txtRmks.Text = row["remarks"].ToString();
                    txtRecBy.Text = row["ReceivedBy"].ToString();
                    txtRecDate.Text = row["ReceivedDate"].ToString();
                    txtRecRmks.Text = row["ReceivedRemark"].ToString();
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('" + ex.Message + "');", true);
            }
        }
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            string AwbNo = txtAWBNo.Text;
            if (AwbNo != "")
            {
                string[] DT = txtDate.Text.Split('/');

                string dates = DT[2] + "/" + DT[1] + "/" + DT[0];

                SqlConnection Conn = new SqlConnection(strconn);
                string Query = "update TBL_Outward set  Date='" + dates + "',ReceivedBy='" + txtRecBy.Text + "',ReceivedDate='" + txtRecDate.Text + "',ReceivedRemark='" + txtRecRmks.Text + "' where awbNo='" + AwbNo + "'";
                Conn.Open();
                SqlDataAdapter da = new SqlDataAdapter();
                SqlCommand cmd = new SqlCommand(Query, Conn);
                cmd.CommandText = Query;
                cmd.Connection = Conn;
                da.SelectCommand = cmd;
                int result = cmd.ExecuteNonQuery();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Courier Information has been updated successfully...');", true);
            }
            else
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Record Not found...');", true);

        }

    }
}