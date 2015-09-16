using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
using System.Text;
//using MySql;
//using MySql.Data.MySqlClient;

namespace ImpexCube
{
    public partial class frmOutward : System.Web.UI.Page
    {
        string strconn = (string)ConfigurationManager.ConnectionStrings["Constr"].ConnectionString;
        //string strconn = (string)ConfigurationManager.AppSettings["ConnectionDashboard"];
        //string strconn1 = (string)ConfigurationManager.AppSettings["connectionJSU"];
        #region
        string FYEAR = "";
        string tagID = "";
        string List1 = "";
        string List2 = "";
        string DocDetl = "";
        string DocDetl_ml = "";
        StringBuilder sb = new StringBuilder();
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack == false)
            {
              
                Session["INOUT"] = "OUT";
                Session["DocDetl"] = "";
                try
                {
                    
                    FYEAR = (string)Session["FYEAR"];
                    string sqlQuery = "select * from T_JobCreation order by jobno";
                    drJobNo.DataSource = GetData(sqlQuery);
                    drJobNo.DataTextField = "jobno";
                    drJobNo.DataValueField = "jobno";
                    drJobNo.DataBind();
                    drJobNo.Items.Insert(0, new ListItem("~select~", "0"));
                    txtDateS.Text = System.DateTime.Now.ToString("dd/MM/yyyy");
                    txtDate.Text = System.DateTime.Now.ToString("dd/MM/yyyy");
                    GetXML();

                    ////Multi
                    string Query = "select distinct AccountName,AccountCode from M_AccountMaster order by AccountName";
                                   
                    drConsignee.DataSource = GetData(Query);
                    drConsignee.DataTextField = "AccountName";
                    drConsignee.DataValueField = "AccountCode";
                    drConsignee.DataBind();
                    drConsignee.Items.Insert(0, new ListItem("~select~", "0"));
                }
                catch (Exception ex)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('" + ex.Message + "');", true);
                }
            }

        }
        protected void GetXML()
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
                GridView1.DataSource = ds;
                GridView1.DataBind();
                GridView3.DataSource = ds;
                GridView3.DataBind();
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

        protected void BtnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                SqlConnection Conn = new SqlConnection(strconn);
                string str1 = "select * from tbl_outward order by sno desc";
                SqlDataAdapter da2 = new SqlDataAdapter(str1, Conn);
                DataSet ds2 = new DataSet();
                da2.Fill(ds2, "Outward");


                DataRowView row = ds2.Tables["Outward"].DefaultView[0];
                Int32 InNo = Convert.ToInt32(row["sno"].ToString());
                Int32 INO = InNo + 1;

                string Details = txtDDetails.Text;
                string rmk2 = txtRmks.Text;
                string dt = txtDate.Text;
                string[] DT = dt.Split('/');

                string dates = DT[2] + "/" + DT[1] + "/" + DT[0];
                Details = Details.Replace("'", " ");
                rmk2 = rmk2.Replace("'", " ");
                string cName = drConsignee.SelectedItem.Text;
                GetTag(cName);
                tagID = tagID + INO.ToString();


                string sqlQuery = "insert into tbl_outward(sno,tagID,jobNo,date,consignee,city,Details,Awbno,Remarks,empcode,sentby) " +
                                  "values('" + INO + "','" + tagID + "','" + txtJobs.Text + "','" + dates + "','" + drConsignee.SelectedItem.Text + "','" + txtCity.Text + "'," +
                                  "'" + Details + "','" + txtAWB.Text + "','" + rmk2 + "'," +
                                  "'" + Session["USER-NAME"] + "','" + txtSentBy.Text + "')";
                Conn.Open();
                SqlDataAdapter da = new SqlDataAdapter();
                SqlCommand cmd = new SqlCommand(sqlQuery, Conn);
                cmd.CommandText = sqlQuery;
                cmd.Connection = Conn;
                da2.SelectCommand = cmd;
                int result = cmd.ExecuteNonQuery();
                Session["ADD"] = "0";
                tagID = "";
               
                Session["RESULT"] = "Outward Record has been Stored Successfully ...";

                ClearField();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Outward Record has been Stored Successfully');", true);
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('" + ex.Message + "');", true);
            }

        }
        protected void btn_search_Click(object sender, EventArgs e)
        {
            FYEAR = Session["FYEAR"].ToString();
            string JobNo = drJobNo.SelectedValue;
            txtCityS.Text = string.Empty;
            if (JobNo == "0")
            {
                Response.Write("<script>alert('Please Select Job No')</script>");

            }
            else
            {
                //VisibleForm();
                try
                {
                    SqlConnection conn = new SqlConnection(strconn);

                    string sqlQuery = "select i.jobno,p.ExporterName,p.city " +
                                    "from T_JobCreation i, T_Importer p" +
                                    "where i.jobno=p.jobno and" +
                                    "i.jobno='" + JobNo + "' " ;
                                    

                    SqlDataAdapter da = new SqlDataAdapter(sqlQuery, conn);
                    DataSet ds = new DataSet();
                    da.Fill(ds, "iJobs");
                    if (ds.Tables["iJobs"].Rows.Count != 0)
                    {
                        DataRowView row = ds.Tables["iJobs"].DefaultView[0];
                        string City = row["city"].ToString();
                        string PName = row["Importer"].ToString();
                        txtConsignee.Text = PName;
                        txtCityS.Text = City;
                    }

                }
                catch (Exception ex)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('" + ex.Message + "');", true);
                }
            }
        }
        protected void Btn_SubmitS_Click(object sender, EventArgs e)
        {
            try
            {
                SqlConnection Conn = new SqlConnection(strconn);
                string str1 = "select * from tbl_outward order by sno desc";
                SqlDataAdapter da2 = new SqlDataAdapter(str1, Conn);
                DataSet ds2 = new DataSet();
                da2.Fill(ds2, "Outward");
                 Int32 InNo=0;
                 Int32 INO = 0;
                 if (ds2.Tables["Outward"].Rows.Count != 0)
                 {
                     DataRowView row = ds2.Tables["Outward"].DefaultView[0];
                     InNo = Convert.ToInt32(row["sno"].ToString());
                     INO = InNo + 1;
                 }
                 else
                 {
                     INO = InNo + 1;
                 }
                string cName = txtConsignee.Text;
                string Details = txtDDetailsS.Text;
                string rmk2 = txtRmkSS.Text;
                string dt = txtDateS.Text;
                string[] DT = dt.Split('/');

                string dates = DT[2] + "/" + DT[1] + "/" + DT[0];
                Details = Details.Replace("'", " ");
                rmk2 = rmk2.Replace("'", " ");
                GetTag(cName);
                tagID = tagID + INO.ToString();


                string sqlQuery = "insert into tbl_outward(sno,tagID,jobNo,date,consignee,city,Details,Awbno,Remarks,empcode,sentby) " +
                                  "values('" + INO + "','" + tagID + "','" + drJobNo.SelectedItem.Text + "','" + dates + "','" + txtConsignee.Text + "','" + txtCityS.Text + "'," +
                                  "'" + Details + "','" + txtAWBS.Text + "','" + rmk2 + "'," +
                                  "'" + Session["USER-NAME"] + "','" + txtSentByS.Text + "')";
                Conn.Open();
                SqlDataAdapter da = new SqlDataAdapter();
                SqlCommand cmd = new SqlCommand(sqlQuery, Conn);
                cmd.CommandText = sqlQuery;
                cmd.Connection = Conn;
                da2.SelectCommand = cmd;
                int result = cmd.ExecuteNonQuery();
                Session["MUL"] = "0";
                tagID = "";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Outward Record has been Stored Successfully ...');", true);
              
                ClearField();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('" + ex.Message + "');", true);
            }

        }
        protected void ClearField()
        {
            if ((string)Session["ADD"] == "0")
            {
                txtDDetails.Text = "";
                txtCity.Text = "";
                txtAWB.Text = "";
                txtSentBy.Text = "";
                txtRmks.Text = "";
                txtJobs.Text = "";
                drConsignee.SelectedValue = "0";

            }
            else
            {

                txtDDetailsS.Text = "";
                txtCityS.Text = "";
                txtAWBS.Text = "";
                txtSentByS.Text = "";
                txtRmkSS.Text = "";
                txtConsignee.Text = "";
                drJobNo.SelectedValue = "0";
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
                cname = cname.Replace('&', ' ');

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
        protected void BtnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/frmDashboardMain.aspx", false);
        }
        protected void Btn_cancelS_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/frmDashboardMain.aspx", false);
        }
        protected void chkSel1_CheckedChanged(object sender, EventArgs e)
        {
            string invDtl = (string)Session["DocDetl"];
            GetDocuments(invDtl);
        }

        protected void chkSel2_CheckedChanged(object sender, EventArgs e)
        {
            string invDtl = (string)Session["DocDetl"];
            GetDocuments(invDtl);
        }
        protected void chkSel1S_CheckedChanged(object sender, EventArgs e)
        {
            GetDocumentsS();
        }

        protected void chkSel2S_CheckedChanged(object sender, EventArgs e)
        {

            GetDocumentsS();
        }
        protected void GetDocuments(string invDtl)
        {
            try
            {

                foreach (GridViewRow row1 in GridView1.Rows)
                {
                    CheckBox chk1 = (CheckBox)row1.FindControl("chkSel1");
                    if (chk1.Checked)
                    {
                        string dtl1 = row1.Cells[1].Text;
                        List1 = List1 + dtl1 + ",\r";
                    }
                }

                foreach (GridViewRow row1 in GridView2.Rows)
                {
                    CheckBox chk1 = (CheckBox)row1.FindControl("chkSel2");
                    if (chk1.Checked)
                    {
                        string dtl1 = row1.Cells[1].Text;
                        List2 = List2 + dtl1 + ",\r";
                    }
                }

                sb.Append(invDtl);
                sb.Append(List1);
                sb.Append(List2);

                txtDDetails.Text = sb.ToString();

            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('" + ex.Message + "');", true);
            }
        }
        protected void GetDocumentsS()
        {
            try
            {
                foreach (GridViewRow row1 in GridView3.Rows)
                {
                    CheckBox chk1 = (CheckBox)row1.FindControl("chkSel1S");
                    if (chk1.Checked)
                    {
                        string dtl1 = row1.Cells[1].Text;
                        List1 = List1 + dtl1 + ",\r";
                    }
                }

                foreach (GridViewRow row1 in GridView4.Rows)
                {
                    CheckBox chk1 = (CheckBox)row1.FindControl("chkSel2S");
                    if (chk1.Checked)
                    {
                        string dtl1 = row1.Cells[1].Text;
                        List2 = List2 + dtl1 + ",\r";
                    }
                }
                DocDetl = (string)Session["DocDetl"];
                sb.Append(DocDetl);
                sb.Append(List1);
                sb.Append(List2);

                txtDDetailsS.Text = sb.ToString();

            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('" + ex.Message + "');", true);
            }
        }
        protected void drConsignee_SelectedIndexChanged(object sender, EventArgs e)
        {
            FYEAR = Session["FYEAR"].ToString();
            string pCode = drConsignee.SelectedValue;
            txtCity.Text = "";
            txtJobs.Text = "";

            if (pCode == "0")
            {
                Response.Write("<script>alert('Please Select Job No')</script>");

            }
            else
            {
               
                SqlConnection conn = new SqlConnection(strconn);
                try
                {
                    string sqlQuery = "select distinct city " +
                                    "from M_AccountMaster " +
                                    "order by city";


                   
                    cbCity.DataSource = GetData(sqlQuery);
                    cbCity.DataTextField = "city";
                    cbCity.DataValueField = "city";
                    cbCity.DataBind();

                    Session["PCODE"] = pCode;
                    string Query = "select jobno from T_JobCreation  " +
                                      " order by jobno";
                    cbJobs.DataSource = GetData(Query);
                    cbJobs.DataTextField = "jobno";
                    cbJobs.DataValueField = "jobno";
                    cbJobs.DataBind();
                }
                catch (Exception ex)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('" + ex.Message + "');", true);
                }
            }
        }
        protected void drJobNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            FYEAR = Session["FYEAR"].ToString();
            string JobNo = drJobNo.SelectedValue;
            txtCityS.Text = string.Empty;

            if (JobNo == "0")
            {
                Response.Write("<script>alert('Please Select Job No')</script>");

            }
            else
            {
               
                try
                {
                    SqlConnection conn = new SqlConnection(strconn);

                    string sqlQuery = "select jobno,Importer,city" +
                                    "from T_JobCreation " +
                                    "where  jobno='" + JobNo + "'  ";
                                   
                                   

                    SqlDataAdapter da = new SqlDataAdapter(sqlQuery, conn);
                    DataSet ds = new DataSet();
                    da.Fill(ds, "iJobs");

                    DataRowView row = ds.Tables["iJobs"].DefaultView[0];
                    
                    string City = row["city"].ToString();
                    string PName = row["Importer"].ToString();

                    txtConsignee.Text = PName;
                    txtCityS.Text = City;

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
                            txtDDetailsS.Text = DocDetl;
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
        protected void txtJobs_TextChanged(object sender, EventArgs e)
        {

        }
        protected void btnInvoice_Click(object sender, EventArgs e)
        {
            try
            {
                string JobNo = txtJobs.Text;
                FYEAR = (string)Session["FYEAR"];
                string lstrsql = "select * from M_iec_invoiceNew where jobno in(" + JobNo + ")";
                DataSet dsInv = GetData(lstrsql);
                DataTable dt = dsInv.Tables[0];
                int j = 0;
                foreach (DataRow row in dt.Rows)
                {
                    if (dsInv.Tables["jobstatus"].Rows.Count != 0)
                    {
                        DataRowView rowInv = dsInv.Tables["jobstatus"].DefaultView[j];
                        string INNO = rowInv["invoice"].ToString();
                        string INDT = rowInv["invoiceDate"].ToString();
                        string DBNo = "";
                        string DBDT = "";

                        string Debitquery = "select * from M_iec_debit where jobno in (" + JobNo + ") ";
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
                            DocDetl_ml = DocDetl_ml + "INV NO." + INNO + " dt." + INDT + ",\n" + "DB NO." + DBNo + " dt." + DBDT + ",\n";

                        }
                    }
                    j = j + 1;
                }

                Session["DocDetl"] = DocDetl_ml;
                txtDDetails.Text = DocDetl_ml;
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('" + ex.Message + "');", true);
            }
        }

        protected void rbTypeofOutward_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (rbTypeofOutward.SelectedValue == "1")
            {
                Label16.Visible = false;
                drJobNo.Visible = false;
            }
            else
            {
                Label16.Visible = true;
                drJobNo.Visible = true;
            }
        }


    }
}