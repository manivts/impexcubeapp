using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

namespace ImpexCube
{
    public partial class frmSEWUpdate : System.Web.UI.Page
    {
        string strconn1 = (string)ConfigurationManager.ConnectionStrings["Constr"].ConnectionString;
        #region
        string PCODE = "";
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack == false)
            {
                try
                {
                    string formID = "SEW Update";
                Authenticate.Forms(formID);
                string Validate = (string)Session["DISABLE"];
                if (Validate == "True")
                {
                    CHECK();
                    GetPCODE();

                    drParty.DataSource = GetParty(PCODE);
                    drParty.DataTextField = "PARTY_NAME";
                    drParty.DataValueField = "PARTY_CODE";
                    drParty.DataBind();
                    drParty.Items.Insert(0, new ListItem("select", "0"));

                    //drParty.SelectedItem.Text = "SEW EURODRIVE INDIA PVT LTD";
                    //drParty.SelectedValue = "SEIN";
                    //to get JobNo for customers
                    GetPartyJOBNO();
                    //   Session["flag"] = "0";
                    BtnCancel.Visible = false;
                    BtnSubmit.Visible = false;
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "redirect", "alert('You have not Authorized for this Page'); window.location.href='HomePage.aspx';", true);

                }

                }
                catch (Exception ex)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('" + ex.Message + "');", true);
                }
            }
        }

        protected void GetPCODE()
        {
            SqlConnection conn = new SqlConnection(strconn1);
            string str = "select distinct party_code from ipurchase_dtl ";

            SqlDataAdapter da = new SqlDataAdapter(str, conn);
            DataSet ds = new DataSet();
            da.Fill(ds, "ipurchase");
            DataTable dt = ds.Tables[0];
            foreach (DataRow row in dt.Rows)
            {
                string pcode = row["party_code"].ToString();
                PCODE += "'" + pcode + "'" + ",";
            }

            PCODE = PCODE.TrimEnd(',');
        }

        public DataSet GetParty(string pcode)
        {
            SqlConnection conn = new SqlConnection(strconn1);
            string str = "select distinct party_name,party_code from prt_mast where party_code in(" + pcode + ") order by party_name";

            SqlDataAdapter da = new SqlDataAdapter(str, conn);
            DataSet ds = new DataSet();
            da.Fill(ds, "party");
            return (ds);
        }

        public DataSet GetJOBNO(string pcode, string year)
        {
            SqlConnection conn = new SqlConnection(strconn1);
            string str = "select distinct jobsno,job_no from ipurchase_dtl where party_code='" + pcode + "' and " +
                         "job_no like '%" + year + "%' order by job_no";

            SqlDataAdapter da = new SqlDataAdapter(str, conn);
            DataSet ds = new DataSet();
            da.Fill(ds, "party");
            return (ds);
        }

        public DataSet GetData(string JNO)
        {
            SqlConnection conn = new SqlConnection(strconn1);

            string str = "SELECT * FROM iproddtl i ,iinv_dtl j " +
                         "where  i.job_no=j.job_no and i.inv_id=j.inv_id and i.job_no='" + JNO + "' order by i.inv_id,i.prod_sn ";
            SqlDataAdapter da = new SqlDataAdapter(str, conn);
            DataSet ds = new DataSet();
            da.Fill(ds, "product");
            return (ds);
        }

        public DataSet GetDataPurchase(string JNO)
        {
            SqlConnection conn = new SqlConnection(strconn1);            
            string str = "SELECT * FROM ipurchase_dtl i ,iinv_dtl j " +
                 "where  i.job_no=j.job_no and i.inv_id=j.inv_id and i.job_no='" + JNO + "' order by i.inv_id,i.prod_sn ";

            SqlDataAdapter da = new SqlDataAdapter(str, conn);
            DataSet ds = new DataSet();
            da.Fill(ds, "product");
            return (ds);
        }

        protected void BtnFind_Click(object sender, EventArgs e)
        {
            try
            {
                string jno = drJOBNO.SelectedValue;
                Session["JOBNOS"] = jno;

                SqlConnection conn = new SqlConnection(strconn1);
                string str = "select * from ipurchase_dtl " +
                           " where job_no='" + jno + "'";
                SqlDataAdapter da = new SqlDataAdapter(str, conn);
                DataSet ds = new DataSet();
                da.Fill(ds, "product");
                if (ds.Tables["product"].Rows.Count == 0)
                {
                    //DGDetail1.DataSource = GetData(jno);
                    //DGDetail1.DataBind();
                    //DataGrid1.Visible = false;
                    //DGDetail1.Visible = true;
                }
                else
                {
                    //DGDetail1.Visible = false;
                    DataGrid1.Visible = true;
                    DataGrid1.DataSource = GetDataPurchase(jno);
                    DataGrid1.DataBind();
                    BtnSubmit.Visible = true;
                    BtnSubmit.Enabled = true;
                    BtnCancel.Visible = true;
                }
                // Session["flag"] = "0";
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('" + ex.Message + "');", true);
            }
        }

        protected void BtnSubmit_Click(object sender, EventArgs e)
        {
            try
            {

                string jobno = Session["JOBNOS"].ToString();

                SqlConnection conn0 = new SqlConnection(strconn1);
                string sqlQuery0 = " select * from ipurchase_dtl where Job_No='" + jobno + "'";

                SqlDataAdapter da0 = new SqlDataAdapter(sqlQuery0, conn0);
                DataSet ds0 = new DataSet();
                da0.Fill(ds0, "iJobStage");
                if (ds0.Tables["iJobStage"].Rows.Count != 0)
                {
                    // Response.Redirect("~/PIPL/JobReports/frmSEW.aspx");
                    Response.Write("<script>alert('Given Job has updated Successfully...')</script>");
                    Session["UPDATED"] = "1";
                    GetUpdateSEW();
                }

            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('" + ex.Message + "');", true);
            }
        }

        protected void BtnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("frmSEWUpdate.aspx");
        }

        protected void drParty_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (drParty.SelectedItem.Selected == true)
            {
                try
                {
                    //Get JobNo for the customers
                    GetPartyJOBNO();
                }
                catch (Exception ex)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('" + ex.Message + "');", true);
                }
            }
        }

        protected void GetPartyJOBNO()
        {
            //if (drParty.SelectedItem.Selected == true)
            //{
            string pc = drParty.SelectedValue;
            string year = (string)Session["FinancialYear"];
            drJOBNO.DataSource = GetJOBNO(pc, year);
            drJOBNO.DataTextField = "jobsno";
            drJOBNO.DataValueField = "job_no";
            drJOBNO.DataBind();
            drJOBNO.Items.Insert(0, new ListItem("select", "0"));
            drJOBNO.Focus();
            //}
        }

        protected void GetUpdateSEW()
        {


            foreach (DataGridItem Row in DataGrid1.Items)
            {

                //string add1 = ((System.Web.UI.WebControls.TextBox)(Row.Cells[3].Controls[0])).Text;
                //TextBox MyTextBox1=Row.Cells[1].Controls[1];
                System.Web.UI.WebControls.TextBox myTextBox1 = (System.Web.UI.WebControls.TextBox)(Row.Cells[0].Controls[1]);
                System.Web.UI.WebControls.TextBox myTextBox2 = (System.Web.UI.WebControls.TextBox)(Row.Cells[2].Controls[1]);
                System.Web.UI.WebControls.TextBox myTextBox3 = (System.Web.UI.WebControls.TextBox)(Row.Cells[3].Controls[1]);
                System.Web.UI.WebControls.TextBox myTextBox4 = (System.Web.UI.WebControls.TextBox)(Row.Cells[4].Controls[1]);
                System.Web.UI.WebControls.TextBox myTextBox5 = (System.Web.UI.WebControls.TextBox)(Row.Cells[5].Controls[1]);
                System.Web.UI.WebControls.TextBox myTextBox8 = (System.Web.UI.WebControls.TextBox)(Row.Cells[8].Controls[1]);

                System.Web.UI.WebControls.TextBox myTextBox11 = (System.Web.UI.WebControls.TextBox)(Row.Cells[12].Controls[1]);


                String jno = myTextBox1.Text;
                String PO_no = myTextBox2.Text;
                String PO_ItemNo = myTextBox3.Text;
                String part_no = myTextBox4.Text;
                String Desc = myTextBox5.Text;


                String sno = myTextBox8.Text;

                String IDs = myTextBox11.Text;




                SqlConnection conn1 = new SqlConnection(strconn1);
                string lstrdrp1 = "update ipurchase_dtl set pur_ordno='" + PO_no + "',po_itemNo='" + PO_ItemNo + "',model='" + part_no + "',prod_desc='" + Desc + "' " +
                                  "where job_no='" + jno + "' and prod_sn='" + sno + "' and inv_id='" + IDs + "'";

                conn1.Open();
                SqlDataAdapter dap1 = new SqlDataAdapter();
                SqlCommand cmdp1 = new SqlCommand(lstrdrp1, conn1);
                cmdp1.CommandText = lstrdrp1;
                cmdp1.Connection = conn1;
                dap1.SelectCommand = cmdp1;

                int result1 = cmdp1.ExecuteNonQuery();
                conn1.Close();
            }
        }

        protected void CHECK()
        {
            SqlConnection conn = new SqlConnection(strconn1);
            string str = "select distinct job_no from ipurchase_dtl where party_code is null ";

            SqlDataAdapter da = new SqlDataAdapter(str, conn);
            DataSet ds = new DataSet();
            da.Fill(ds, "ipurchase");
            DataTable dt = ds.Tables[0];
            foreach (DataRow row in dt.Rows)
            {
                string jno = row["job_no"].ToString();
                SqlConnection conn1 = new SqlConnection(strconn1);
                string Query = "select * from iworkreg where job_no='" + jno + "'";
                conn1.Open();
                SqlDataAdapter da1 = new SqlDataAdapter(Query, conn1);
                DataSet ds1 = new DataSet();
                da1.Fill(ds1, "iworkreg");
                DataRowView ROWS = ds1.Tables["iworkreg"].DefaultView[0];
                string pc = ROWS["party_code"].ToString();
                string nos = ROWS["jobsno"].ToString();
                GetUP(jno, pc, nos);
                conn1.Close();
            }
        }

        protected void GetUP(string jno, string pc, string nos)
        {
            SqlConnection conn1 = new SqlConnection(strconn1);
            string lstrdrp1 = "update ipurchase_dtl set party_code='" + pc + "',jobsno='" + nos + "' where job_no='" + jno + "' ";

            conn1.Open();
            SqlDataAdapter dap1 = new SqlDataAdapter();
            SqlCommand cmdp1 = new SqlCommand(lstrdrp1, conn1);
            cmdp1.CommandText = lstrdrp1;
            cmdp1.Connection = conn1;
            dap1.SelectCommand = cmdp1;

            int result1 = cmdp1.ExecuteNonQuery();
            conn1.Close();
        }

    }
}