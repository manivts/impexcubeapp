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


namespace ImpexCube
{
    public partial class frmInward : System.Web.UI.Page
    {
        string strconn = (string)ConfigurationManager.ConnectionStrings["Constr"].ConnectionString;
        //string strconn = (string)ConfigurationManager.AppSettings["ConnectionDashboard"];
        //string strconn1 = (string)ConfigurationManager.AppSettings["connectionJSU"];
        #region
        string FYEAR = "";
        string tagID = "";
        string List1 = "";
        string List2 = "";
        StringBuilder sb = new StringBuilder();
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack == false)
            {
                Session["INOUT"] = "IN";
                FYEAR = (string)Session["FYEAR"];
                txtDateS.Text = System.DateTime.Now.ToString("dd/MM/yyyy");
                txtDate.Text = System.DateTime.Now.ToString("dd/MM/yyyy");
                txtDateF.Text = System.DateTime.Now.ToString("dd/MM/yyyy");

                string sqlQuery = "select * from T_JobCreation  order by jobno";
                drJobNo.DataSource = GetData(sqlQuery);
                drJobNo.DataTextField = "jobno";
                drJobNo.DataValueField = "jobno";
                drJobNo.DataBind();

                drJobNo.Items.Insert(0, new ListItem("~select~", "0"));
                //string Query = "select distinct j.party_name,j.party_code from iworkreg i, prt_mast j " +
                //                "where i.party_code=j.party_code and i.job_no like '%" + FYEAR + "%' order by j.party_name;";

            
                GetXML();
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
                GridView5.DataSource = ds;
                GridView5.DataBind();
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
        protected void btn_search_Click(object sender, EventArgs e)
        {
            FYEAR = Session["FYEAR"].ToString();
            string JobNo = drJobNo.SelectedValue;

            if (JobNo == "0")
            {
                Response.Write("<script>alert('Please Select Job No')</script>");
            }
            else
            {
                SqlConnection conn = new SqlConnection(strconn);
                try
                {
                    //string sqlQuery = "select i.job_no,i.jobsno,p.party_name,ad.city " +
                    //                 "from iworkreg i, prt_mast p,prt_addr ad " +
                    //                 "where i.job_no like '%" + FYEAR + "%' and " +
                    //                 "i.job_no='" + JobNo + "' and " +
                    //                 "i.party_code=p.party_code and p.party_code=ad.party_code";

                    string sqlQuery = "select i.jobno,p.ExporterName,p.city " +
                                    "from T_JobCreation i, T_Importer p" +
                                    "where i.jobno=p.jobno and" +
                                    "i.jobno='" + JobNo + "' ";
                                    
                    SqlDataAdapter da = new SqlDataAdapter(sqlQuery, conn);
                    DataSet ds = new DataSet();
                    da.Fill(ds, "iJobs");

                    DataRowView row = ds.Tables["iJobs"].DefaultView[0];
                
                    string City = row["city"].ToString();
                    string PName = row["Importer"].ToString();
                    txtConsignee.Text = PName;
                    //string Query = "select distinct ad.city " +
                    //           "from iworkreg i, prt_mast p,prt_addr ad " +
                    //           "where i.job_no like '%" + FYEAR + "%' and " +
                    //           "i.job_no='" + JobNo + "' and " +
                    //           "i.party_code=p.party_code and p.party_code=ad.party_code";
                   
                }
                catch (Exception ex)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('" + ex.Message + "');", true);
                }

            }
        }
        protected void BtnSubmitS_Click(object sender, EventArgs e)
        {

            try
            {
                string dt = txtDateS.Text;
                string jno = drJobNo.SelectedItem.Text;
                string consignee = txtConsignee.Text;
                string city = txtCityS.Text;
                string details = txtDDetailsS.Text;
                string awbno = txtAWBS.Text;
                string rec_by = txtReceivedByS.Text;
                string rec_time = txtTimeS.Text;
                string remarks = txtRmksS.Text;
                string UserName = (string)Session["USER-NAME"];
                GetInwardRecord(dt, jno, consignee, city, details, awbno, rec_by, rec_time, remarks, UserName);
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
                string dt = txtDate.Text;
                string jno = txtJobs.Text;
                string consignee = drConsignee.Text;
                string city = txtCity.Text;
                string details = txtDDetails.Text;
                string awbno = txtAWB.Text;
                string rec_by = txtReceivedBy.Text;
                string rec_time = txtTime.Text;
                string remarks = txtRmks.Text;
                string UserName = (string)Session["USER-NAME"];
                GetInwardRecord(dt, jno, consignee, city, details, awbno, rec_by, rec_time, remarks, UserName);
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('" + ex.Message + "');", true);
            }
        }
        protected void BtnSubmitF_Click(object sender, EventArgs e)
        {
            try
            {
                string dt = txtDateF.Text;
                string jno = txtJobNoF.Text;
                string consignee = txtConsigneeF.Text;
                string city = txtCityF.Text;
                string details = txtDDetailsF.Text;
                string awbno = txtAWBF.Text;
                string rec_by = txtReceivedByF.Text;
                string rec_time = txtTimeF.Text;
                string remarks = txtRmksF.Text;
                string UserName = (string)Session["USER-NAME"];
                GetInwardRecord(dt, jno, consignee, city, details, awbno, rec_by, rec_time, remarks, UserName);
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('" + ex.Message + "');", true);
            }
        }
        protected void GetInwardRecord(string dt, string jno, string consignee, string city, string Details, string AWBNo, string receivedBY, string rec_time, string rmk2, string eName)
        {
            SqlConnection Conn = new SqlConnection(strconn);
            string str1 = "select * from tbl_Inward order by sno desc";
            SqlDataAdapter da2 = new SqlDataAdapter(str1, Conn);
            DataSet ds2 = new DataSet();
            da2.Fill(ds2, "Inward");
                Int32 InNo=0;
                 Int32 INO = 0;
                 if (ds2.Tables["Inward"].Rows.Count != 0)
                 {
            DataRowView row = ds2.Tables["Inward"].DefaultView[0];
             InNo = Convert.ToInt32(row["sno"].ToString());
             INO = InNo + 1;
                 }
               else
               {
                   INO = InNo + 1;
               }

            string[] DT = dt.Split('/');

            string dates = DT[2] + "/" + DT[1] + "/" + DT[0];
            Details = Details.Replace("'", " ");
            rmk2 = rmk2.Replace("'", " ");

            GetTag(consignee);
            tagID = tagID + INO.ToString();
            try
            {
                string sqlQuery = "insert into tbl_Inward(sno,date,tagId,jobNo,consignee,city,Details,Awbno,ReceivedBy,Rec_Time,Remarks,empcode) " +
                                  "values('" + INO + "','" + dates + "','" + tagID + "','" + jno + "','" + consignee + "','" + city + "','" + Details + "'," +
                                  "'" + AWBNo + "','" + receivedBY + "','" + rec_time + "','" + rmk2 + "','" + Session["USER-NAME"] + "')";

                Conn.Open();
                SqlDataAdapter da = new SqlDataAdapter();
                SqlCommand cmd = new SqlCommand(sqlQuery, Conn);
                cmd.CommandText = sqlQuery;
                cmd.Connection = Conn;
                da2.SelectCommand = cmd;
                int result = cmd.ExecuteNonQuery();

                Session["ADD"] = "0";
                tagID = "";
             
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Inward Record has been Stored Successfully ...');", true);

                ClearField();
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
        protected void chkSel1S_CheckedChanged(object sender, EventArgs e)
        {
            GetDocumentsS();
        }

        protected void chkSel2S_CheckedChanged(object sender, EventArgs e)
        {
            GetDocumentsS();
        }
        protected void chkSel1_CheckedChanged(object sender, EventArgs e)
        {
            GetDocuments();
        }

        protected void chkSel2_CheckedChanged(object sender, EventArgs e)
        {
            GetDocuments();
        }
        protected void chkSel1F_CheckedChanged(object sender, EventArgs e)
        {
            GetDocumentsF();
        }

        protected void chkSel2F_CheckedChanged(object sender, EventArgs e)
        {
            GetDocumentsF();
        }
        protected void GetDocuments()
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


            sb.Append(List1);
            sb.Append(List2);

            txtDDetails.Text = sb.ToString();

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


                sb.Append(List1);
                sb.Append(List2);

                txtDDetailsS.Text = sb.ToString();

            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('" + ex.Message + "');", true);
            }
        }
        protected void GetDocumentsF()
        {
            try
            {
                foreach (GridViewRow row1 in GridView5.Rows)
                {
                    CheckBox chk1 = (CheckBox)row1.FindControl("chkSel1F");
                    if (chk1.Checked)
                    {
                        string dtl1 = row1.Cells[1].Text;
                        List1 = List1 + dtl1 + ",\r";
                    }
                }

                foreach (GridViewRow row1 in GridView6.Rows)
                {
                    CheckBox chk1 = (CheckBox)row1.FindControl("chkSel2F");
                    if (chk1.Checked)
                    {
                        string dtl1 = row1.Cells[1].Text;
                        List2 = List2 + dtl1 + ",\r";
                    }
                }


                sb.Append(List1);
                sb.Append(List2);

                txtDDetailsF.Text = sb.ToString();

            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('" + ex.Message + "');", true);
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

        protected void ClearField()
        {
            if ((string)Session["ADD"] == "0")
            {
                txtDDetails.Text = "";
                txtCity.Text = "";
                txtAWB.Text = "";
                txtReceivedBy.Text = "";

                txtRmks.Text = "";
                txtJobs.Text = "";
                drConsignee.Text = "";
            }
            else
            {

                txtDDetailsS.Text = "";
                txtCityS.Text = "";
                txtAWBS.Text = "";
                txtReceivedByS.Text = "";
                txtRmksS.Text = "";
                txtConsignee.Text = "";
                drJobNo.SelectedValue = "0";
            }
        }
        protected void drJobNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            FYEAR = Session["FYEAR"].ToString();
            string JobNo = drJobNo.SelectedValue;

            if (JobNo == "0")
            {
                Response.Write("<script>alert('Please Select Job No')</script>");
            }
            else
            {
                SqlConnection conn = new SqlConnection(strconn);
                try
                {
                    //string sqlQuery = "select i.job_no,i.jobsno,p.party_name,ad.city " +
                    //                 "from iworkreg i, prt_mast p,prt_addr ad " +
                    //                 "where i.job_no like '%" + FYEAR + "%' and " +
                    //                 "i.job_no='" + JobNo + "' and " +
                    //                 "i.party_code=p.party_code and p.party_code=ad.party_code";
                   
                    string sqlQuery = "select jobno,Importer,city " +
                                "from T_Importer " +
                                "where  jobno='" + JobNo + "'  ";
                                   
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
                    //string Query = "select distinct ad.city " +
                    //           "from iworkreg i, prt_mast p,prt_addr ad " +
                    //           "where i.job_no like '%" + FYEAR + "%' and " +
                    //           "i.job_no='" + JobNo + "' and " +
                    //           "i.party_code=p.party_code and p.party_code=ad.party_code";

                }
                catch (Exception ex)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('" + ex.Message + "');", true);
                }

            }
        }

        protected void BtnCancelF_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Dashboard/frmDashboardMain.aspx", false);
        }
        protected void BtnFind_Click(object sender, EventArgs e)
        {
            string src = "";
            try
            {
                string BL = txtBL.Text;
                if (rbSearch.SelectedItem.Text == "BL No")
                    src = "and j.mawb_no ='" + BL + "'";
                else if (rbSearch.SelectedItem.Text == "Invoice")
                    src = "and n.inv_no ='" + BL + "'";
                else if (rbSearch.SelectedItem.Text == "Consignor")
                    src = "and n.cnsr_name like '%" + BL + "%'";
                string sqlQuery = "select i.jobsno,m.party_name,p.city,j.mawb_no,j.hawb_no,n.inv_no " +
                               "from iworkreg i,prt_addr p,ishp_dtl j,prt_mast m,iinv_dtl n " +
                               "where i.job_no=j.job_no and i.job_no=n.job_no and i.party_code=p.party_code and i.party_code=m.party_code " +
                               " " + src + " and i.party_addr=p.addr_code ";

                SqlConnection conn = new SqlConnection(strconn);
                SqlDataAdapter da = new SqlDataAdapter(sqlQuery, conn);
                DataSet ds = new DataSet();
                da.Fill(ds, "iJobs");
                if (ds.Tables["ijobs"].Rows.Count != 0)
                {
                    DataRowView row = ds.Tables["iJobs"].DefaultView[0];
                 
                    txtJobNoF.Text = row["jobsno"].ToString();
                    txtConsigneeF.Text = row["party_name"].ToString();
                    txtCityF.Text = row["city"].ToString();
                    lblFResult.Visible = false;
                }
                else
                {
                    string Query = "select i.jobsno,m.party_name,p.city,j.mawb_no,j.hawb_no " +
                                   "from iworkreg i,prt_addr p,ishp_dtl j,prt_mast m " +
                                   "where i.job_no=j.job_no and i.party_code=p.party_code and i.party_code=m.party_code " +
                                   "and j.hawb_no ='" + BL + "' and i.party_addr=p.addr_code ";
                    SqlConnection connH = new SqlConnection(strconn);
                    SqlDataAdapter daH = new SqlDataAdapter(Query, connH);
                    DataSet dsH = new DataSet();
                    daH.Fill(dsH, "iJobs");
                    if (dsH.Tables["ijobs"].Rows.Count != 0)
                    {
                        DataRowView rowH = dsH.Tables["iJobs"].DefaultView[0];
                      
                        txtJobNoF.Text = rowH["jobsno"].ToString();
                        txtConsigneeF.Text = rowH["party_name"].ToString();
                        txtCityF.Text = rowH["city"].ToString();
                        lblFResult.Visible = false;
                    }
                    else
                    {
                        lblFResult.Text = "** Record Not Found for Given AWB/BL.No";
                        lblFResult.Visible = true;
                        txtJobNoF.Text = "";
                        txtConsigneeF.Text = "";
                        txtCityF.Text = "";
                    }
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('" + ex.Message + "');", true);
            }
        }

        protected void drConsignee_TextChanged(object sender, EventArgs e)
        {

            FYEAR = Session["FYEAR"].ToString();
            string pCode = drConsignee.Text;

            if (pCode == "0")
            {
                Response.Write("<script>alert('Please Select Job No')</script>");

            }
            else
            {
               
                SqlConnection conn = new SqlConnection(strconn);
                try
                {
                    string sqlQuery = "select distinct ad.city " +
                                    "from iworkreg i, prt_mast p,prt_addr ad " +
                                    "where i.job_no like '%" + FYEAR + "%' and " +
                                    "p.party_name='" + pCode + "' and " +
                                    "i.party_code=p.party_code and p.party_code=ad.party_code";

                    string Query = "select i.jobsno,i.job_no from iworkreg i, impjobstage j,prt_mast p " +
                                      "where i.job_no=j.job_no and i.party_code=p.party_code and i.job_no like '%" + FYEAR + "%' and p.party_code='" + pCode + "' " +
                                      " order by i.jobsno";

                }
                catch (Exception ex)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('" + ex.Message + "');", true);
                }
            }

        }
        protected void rbSearch_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblH.Text = rbSearch.SelectedValue;
        }
    }
}