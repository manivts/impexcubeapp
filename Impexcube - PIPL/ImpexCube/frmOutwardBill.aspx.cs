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
//using MySql;
//using MySql.Data.MySqlClient;
using System.Data.SqlClient;

namespace ImpexCube
{
    public partial class Dashboard_frmOutwardBill : System.Web.UI.Page
    {
        string strconn = (string)ConfigurationManager.ConnectionStrings["Constr"].ConnectionString;
        //string strconn = (string)ConfigurationManager.AppSettings["ConnectionDashboard"];
        //string strconn1 = (string)ConfigurationManager.AppSettings["connectionJSU"];
        //string strconnJsu = (string)ConfigurationManager.AppSettings["connectionJSU"];

        private string DocDetl = "";
        private string tagID = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack == false)
            {

                tlHead.Visible = false;
                txtFrom.Text = System.DateTime.Now.ToString("dd/MM/yyyy");
                txtTo.Text = System.DateTime.Now.ToString("dd/MM/yyyy");
                GetCustomer();

            }
        }
        public DataSet GetData(string Query)
        {
            SqlConnection conn = new SqlConnection(strconn);
            SqlDataAdapter da = new SqlDataAdapter(Query, conn);
            DataSet ds = new DataSet();
            da.Fill(ds, "jobstatus");
            return (ds);
        }
        public DataSet GetData(string FDate, string TDate)
        {

            SqlConnection conn = new SqlConnection(strconn);
            string sqlQuery = "select distinct compName from M_iec_invoiceNew j " +
                         "where " +
                         " invoiceDate between '" + FDate + "' and '" + TDate + "' order by compName";
            SqlDataAdapter da = new SqlDataAdapter(sqlQuery, conn);
            DataSet ds = new DataSet();
            da.Fill(ds, "PartyMaster");
            return ds;

        }
        protected void txtTo_TextChanged(object sender, EventArgs e)
        {
            GetCustomer();
        }
        protected void txtFrom_TextChanged(object sender, EventArgs e)
        {
            GetCustomer();
        }
        protected void GetCustomer()
        {
            try
            {
                string TDate = "";
                string FDate = "";
                string fd = txtFrom.Text;
                string td = txtTo.Text;
                string[] FD = fd.Split('/');
                string[] TD = td.Split('/');


                FDate = FD[2] + "-" + FD[1] + "-" + FD[0];
                TDate = TD[2] + "-" + TD[1] + "-" + TD[0];

                drConsignee.DataSource = GetData(FDate, TDate);
                drConsignee.DataTextField = "compName";
                drConsignee.DataValueField = "compName";
                drConsignee.DataBind();
                drConsignee.Items.Insert(0, new ListItem("~select~", "0"));
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('" + ex.Message + "');", true);
            }

        }
        protected void BtnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                GetReports();

            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('" + ex.Message + "');", true);
            }
        }
        protected void GetReports()
        {
            string TDate = "";
            string FDate = "";
            string fd = txtFrom.Text;
            string td = txtTo.Text;
            string[] FD = fd.Split('/');
            string[] TD = td.Split('/');
            string cName = drConsignee.SelectedValue;
            string strQuery = "";
            FDate = FD[2] + "-" + FD[1] + "-" + FD[0];
            TDate = TD[2] + "-" + TD[1] + "-" + TD[0];


            if (FDate == "")
                Response.Write("<script>alert('Please Give Doc Date') </script>");
            else
            {

                if (cName == "0")
                {
                    //strQuery = "select * from iworkreg i,prt_mast p,iworkreg_jobstatus j " +
                    //           "where i.job_no=j.job_no and i.party_code=p.party_code and j.comp_jobstage='Billing' " +
                    //           "and comp_jobdate between '" + FDate + "' and '" + TDate + "' order by i.job_no";

                    strQuery = "select * from M_iec_invoiceNew " +
                               "where " +
                               " invoiceDate between '" + FDate + "' and '" + TDate + "' order by jobno";

                }
                else
                {
                    //strQuery = "select * from iworkreg i,prt_mast p,iworkreg_jobstatus j " +
                    //           "where i.job_no=j.job_no and i.party_code=p.party_code and j.comp_jobstage='Billing' " +
                    //           "and comp_jobdate between '" + FDate + "' and '" + TDate + "' and p.party_name='" + cName + "' order by i.job_no";

                    strQuery = "select * from M_iec_invoiceNew " +
                           "where " +
                           " invoiceDate between '" + FDate + "' and '" + TDate + "'  and compName='" + cName + "'  order by jobno";
                }

                try
                {
                    GridView1.DataSource = GetData(strQuery);
                    GridView1.DataBind();
                    tlHead.Visible = true;
                }
                catch (Exception ex)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('" + ex.Message + "');", true);
                }
            }
        }
        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string JobNo = GridView1.DataKeys[e.Row.RowIndex].Value.ToString();
                TextBox txtDate = (TextBox)e.Row.FindControl("txtDate");
                TextBox txtDDetails = (TextBox)e.Row.FindControl("txtDDetails");
                try
                {
                    txtDate.Text = System.DateTime.Now.ToString("dd/MM/yyyy");
                    string lstrsql = "select * from M_iec_invoiceNew where jobno='" + JobNo + "'";
                    DataSet dsInv = GetData(lstrsql);
                    if (dsInv.Tables["jobstatus"].Rows.Count != 0)
                    {
                        DataRowView rowInv = dsInv.Tables["jobstatus"].DefaultView[0];
                        string INNO = rowInv["invoice"].ToString();
                        string INDT = rowInv["invoiceDate"].ToString();
                        string DBNo = "";
                        string DBDT = "";

                        string Debitquery = "select * from M_iec_debit where jobno='" + JobNo + "'";
                        DataSet dsDebit = GetData(Debitquery);
                        if (dsDebit.Tables["jobstatus"].Rows.Count != 0)
                        {
                            DataRowView rowDebit = dsDebit.Tables["jobstatus"].DefaultView[0];
                            DBNo = rowDebit["invoice"].ToString();
                            DBDT = rowDebit["invoiceDate"].ToString();
                        }

                        if (INNO != "")
                        {
                            if (INDT != "")
                            {
                                DateTime iDT = Convert.ToDateTime(INDT);
                                INDT = iDT.ToString("dd/MM/yyyy");

                            }
                            if (DBDT != "")
                            {
                                DateTime iDT = Convert.ToDateTime(DBDT);
                                DBDT = iDT.ToString("dd/MM/yyyy");

                            }
                            DocDetl = "INV NO." + INNO + " dt." + INDT + ",\n" + "DB NO." + DBNo + " dt." + DBDT + ",\n";
                            Session["DocDetl"] = DocDetl;
                           
                        }
                    }
                    else
                        Session["DocDetl"] = "";
                }
                catch (Exception ex)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('" + ex.Message + "');", true);
                }
            }
        }
        protected void BtnCancel_Click(object sender, EventArgs e)
        {
            //Response.Redirect("~/frmDashboardMain.aspx", false);
        }


        protected void BtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (GridViewRow row in GridView1.Rows)
                {
                    Label lblImp = (Label)row.FindControl("lblImporter");
                    TextBox txtDate = (TextBox)row.FindControl("txtDate");
                    TextBox txtDDetails = (TextBox)row.FindControl("txtDDetails");
                    TextBox txtCity = (TextBox)row.FindControl("txtCity");
                    TextBox txtAddRmks = (TextBox)row.FindControl("txtAddRmks");
                    TextBox txtAWBNo = (TextBox)row.FindControl("txtAWBNo");
                    TextBox txtSentBy = (TextBox)row.FindControl("txtSentBy");
                    TextBox txtRmks = (TextBox)row.FindControl("txtRmks");
                    CheckBox chk1 = (CheckBox)row.FindControl("chkSel1S");

                    string jno = row.Cells[0].Text;
                    string pName = lblImp.Text;
                    string dt = txtDate.Text;
                    string docDet = txtDDetails.Text;
                    string city = txtCity.Text;
                    string courier = txtAddRmks.Text;
                    string awbno = txtAWBNo.Text;
                    string sentBy = txtSentBy.Text;
                    string remark = txtRmks.Text;

                    if (chk1.Checked)
                    {
                        GetRecord(jno, pName, city, dt, awbno, courier, sentBy, docDet, remark);

                    }

                }
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Outward Record has been Stored Successfully');", true);
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('" + ex.Message + "');", true);
            }
        }
        protected void GetRecord(string jno, string cName, string city, string dt, string awbno, string courier, string sentBy, string Details, string rmk2)
        {
            SqlConnection Conn = new SqlConnection(strconn);
            string str1 = "select * from tbl_outward order by sno desc";
            SqlDataAdapter da2 = new SqlDataAdapter(str1, Conn);
            DataSet ds2 = new DataSet();
            da2.Fill(ds2, "Outward");


            DataRowView row = ds2.Tables["Outward"].DefaultView[0];
            Int32 InNo = Convert.ToInt32(row["sno"].ToString());
            Int32 INO = InNo + 1;

         
            string[] DT = dt.Split('/');

            string dates = DT[2] + "/" + DT[1] + "/" + DT[0]; //System.DateTime.Now.ToString("yyyy/MM/dd");
            Details = Details.Replace("'", " ");
            rmk2 = rmk2.Replace("'", " ");
        
            GetTag(cName);
            tagID = tagID + INO.ToString();

            try
            {
                string sqlQuery = "insert into tbl_outward(sno,tagID,jobNo,date,consignee,city,Details,Awbno,Remarks,add_rmks,empcode,sentby) " +
                                  "values('" + INO + "','" + tagID + "','" + jno + "','" + dates + "','" + cName + "','" + city + "'," +
                                  "'" + Details + "','" + awbno + "','" + courier + "','" + rmk2 + "'," +
                                  "'" + Session["USER-NAME"] + "','" + sentBy + "')";
                Conn.Open();
                SqlDataAdapter da = new SqlDataAdapter();
                SqlCommand cmd = new SqlCommand(sqlQuery, Conn);
                cmd.CommandText = sqlQuery;
                cmd.Connection = Conn;
                da2.SelectCommand = cmd;
                int result = cmd.ExecuteNonQuery();
                Session["ADD"] = "0";
                tagID = "";
                
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('" + ex.Message + "');", true);
            }
            finally
            {
                
                Session["RESULT"] = "Outward Record has been Stored Successfully ...";
                //Response.Redirect("~/frmDashboardMain.aspx", false);
            }

            
        }
        protected void GetTag(string cname)
        {
            string ssStr = "";
            string EndStr = "";
            try
            {

                
                cname = cname.TrimEnd(' ');
                cname = cname.Replace('(', ' ');
                cname = cname.Replace(')', ' ');
                cname = cname.Replace('.', ' ');
                cname = cname.Replace(',', ' ');

                if (cname != string.Empty)
                {
                    string[] str = null;
                    str = cname.Split(' ');
                    Int32 i = 0;

                    for (i = 0; i <= str.Length - 1; i++)
                    {
                        if (str[i] == "")
                        {
                            str[i].TrimEnd(' ');
                        }

                        else if (str.Length - 1 == str.Length - 1)
                        {
                            ssStr += str[i].Substring(0, 1);
                            EndStr = str[i].Remove(0, 1);
                        }

                    }


                    string strComp = ssStr + EndStr;
                    string strUpp = strComp.ToUpper();

                    if (strUpp.Length > 3)
                    {
                        string strPCode = strUpp.Substring(0, 4);
                        tagID = strPCode;
                    }
                    else if (strUpp.Length == 3)
                    {
                        string strPCode = strUpp.Substring(0, 3);
                        tagID = strPCode + strPCode.Substring(0, 1);
                    }
                    else if (strUpp.Length <= 2)
                    {
                        string strPCode = strUpp.Substring(0, 2);
                        tagID = strPCode + strPCode.Substring(0, 2);
                    }
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('" + ex.Message + "');", true);
            }
        }
    }
}