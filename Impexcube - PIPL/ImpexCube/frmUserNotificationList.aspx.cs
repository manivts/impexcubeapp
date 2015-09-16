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
    public partial class frmUserNotificationList : System.Web.UI.Page
    {
        string strconn = (string)ConfigurationManager.AppSettings["ConnectionDashboard"];
        protected void Page_Load(object sender, EventArgs e)
        {
            Label pagename;
            pagename = (Label)Master.FindControl("lblName");
            pagename.Text = "User Notification Master";
            if (!IsPostBack)
            {
                Gridload();
                btnUdate.Visible = false;
            }
        }

        public static string removespace(string dates)
        {
            string[] dat = dates.Split(' ');
            string da = dat[0];
            return da;
        }
        public static string ChangeDate(string date)
        {
            string[] dates = date.Split('/');
            string da = dates[0] + '/' + dates[1] + '/' + dates[2];
            return removespace(da);
        }
        private static string datereplace(string date)
        {
            string[] a = date.Split('/');
            date = a[1] + "-" + a[0] + "-" + a[2];
            return date;
        }

        protected void btnsave_Click(object sender, EventArgs e)
        {
            string dutyName = ddlDutyName.SelectedItem.Text;
            string chaterNo = ddlchaterno.SelectedItem.Text;
            string Notification = txtnot.Text;
            string serialno = txtseralno.Text;
            string duty = txtduty.Text;
            string remarks = txtremarks.Text;
            string effectivedate = datereplace(txteffdate.Text);
            SqlConnection CON = new SqlConnection(strconn);
            CON.Open();
            string QUERY6 = "INSERT INTO M_UserNotification(DutyName,ChapterNo,Notification,SerialNo,Duty,EffectiveDate,Remarks,CreatedBy,CreatedDate)VALUES('" + dutyName + "','" + chaterNo + "','" + Notification + "','" + serialno + "','" + duty + "','" + effectivedate+ "','" + remarks + "','" + (string)Session["USER-NAME"] + "','" +DateTime.Now.ToString()+"')";
            SqlCommand CMD3 = new SqlCommand(QUERY6, CON);
            int Result = CMD3.ExecuteNonQuery();
            CON.Close();
            if (Result == 1)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "redirect", "alert('Saved Successfully'); window.location.href='frmUserNotificationList.aspx';", true);
            }
            Textclear();
            Gridload();
        }

        private void Textclear()
        {
            txtnot.Text = "";
            txtseralno.Text = "";
            txtduty.Text = "";
            txtremarks.Text = "";
            txteffdate.Text = "";
        }

        private void Gridload()
        {
            gvuserNotify.DataBind();
            Session["query"] = string.Empty;
            string QUERY1;
            SqlConnection CON = new SqlConnection(strconn);
            if(txtkeyfield.Text!="")
                QUERY1 = "SELECT TransId,DutyName,ChapterNo,Notification,SerialNo,Duty,EffectiveDate,Remarks FROM M_UserNotification where DutyName='" + txtkeyfield.Text + "' or Notification ='" + txtkeyfield.Text + "' ";
                
            else
                QUERY1 = "SELECT TransId,DutyName,ChapterNo,Notification,SerialNo,Duty,EffectiveDate,Remarks FROM M_UserNotification";
            Session["query"] = QUERY1;
            SqlDataAdapter SD = new SqlDataAdapter(QUERY1, CON);
            DataSet DS = new DataSet();
            SD.Fill(DS, "DATA");
            if (DS.Tables["DATA"].Rows.Count != 0)
            {
                gvuserNotify.DataSource = DS;
                gvuserNotify.DataBind();
            }
            else
            {
                gvuserNotify.DataSource = null;
                gvuserNotify.DataBind();
            }
        }

        protected void btnUdate_Click(object sender, EventArgs e)
        {
            SqlConnection CON = new SqlConnection(strconn);
            CON.Open();
            string QUERY8 = " UPDATE M_UserNotification SET DutyName='" + ddlDutyName.SelectedItem.Text + "', ChapterNo='" + ddlchaterno.SelectedItem.Text + "',Duty='" + txtduty.Text + "',EffectiveDate = '" + datereplace(txteffdate.Text) + "',Notification='" + txtnot.Text + "',Remarks = '" + txtremarks.Text + "',SerialNo = '" + txtseralno.Text + "',ModifiedBy='" + (string)Session["USER-NAME"] + "',ModifiedDate='" + DateTime.Now.ToString() + "'   WHERE TransId ='" + (string)Session["TransId"] + "'";
            SqlCommand CMD5 = new SqlCommand(QUERY8, CON);
            btnsave.Visible = true;
            int Result = CMD5.ExecuteNonQuery();
            CON.Close();
            Textclear();
            Gridload();
            if (Result == 1)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "redirect", "alert('Updated Successfully'); window.location.href='frmUserNotificationList.aspx';", true);
            }
        }

       
       
        protected void gvuserNotify_SelectedIndexChanged(object sender, EventArgs e)
        {
            Session["TransId"] = gvuserNotify.SelectedRow.Cells[1].Text;
            SqlConnection CON = new SqlConnection(strconn);
            CON.Open();
            string QUERY1 = "SELECT * FROM M_UserNotification where TransId ='" + (string)Session["TransId"] + "' ";
            SqlDataAdapter SD = new SqlDataAdapter(QUERY1, CON);
            DataSet DS = new DataSet();
            SD.Fill(DS, "DATA");
            DataRowView dr = DS.Tables["DATA"].DefaultView[0];
            ddlDutyName.SelectedItem.Text = dr["DutyName"].ToString();
            ddlchaterno.SelectedItem.Text = dr["ChapterNo"].ToString();
            txtduty.Text = dr["Duty"].ToString();
            txteffdate.Text = dr["EffectiveDate"].ToString();
            txtnot.Text = dr["Notification"].ToString();
            txtremarks.Text = dr["Remarks"].ToString();
            txtseralno.Text = dr["SerialNo"].ToString();
            btnsave.Visible = false;
            btnUdate.Visible = true;
        }

        protected void btnNew_Click(object sender, EventArgs e)
        {
            ddlDutyName.ClearSelection();
            ddlchaterno.ClearSelection();
            txtduty.Text = "";
            txteffdate.Text = "";
            txtnot.Text = "";
            txtremarks.Text = "";
            txtseralno.Text = "";
            txtkeyfield.Text = "";
            btnsave.Visible = true;
            Gridload();
        }
    }
}