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
using MySql;
using MySql.Data.MySqlClient;
using System.Data.SqlClient;

namespace ImpexCube
{
    public partial class frmPortStatusHC : System.Web.UI.Page
    {
        //string strconn = (string)ConfigurationManager.AppSettings["ConnectionVimpex"];
        string strconn1 = (string)ConfigurationManager.AppSettings["ConnectionVisual"];
        // string strconn1 = (string)ConfigurationManager.AppSettings["ConnectionVimpex"];
        #region
        Int32 i;
        string PName;
        //    string sysDates;
        string jobNo;
        //    string FileName;
        string QrySt;
        int lngcol;
        string lstTxt;

        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack == false)
            {

                lblerror.Visible = false;
                string FY = (string)Session["FinancialYear"];
                drYEar.SelectedValue = FY;
                // User Authentication Code 
                //   txtPName.Text = "select";

                // Authenticate.Forms(formID);


            }
        }



        protected void Search_Click(object sender, EventArgs e)
        {
            try
            {

                PName = txtPName.Text;
                string FYear = drYEar.SelectedValue;

                MySqlConnection conn = new MySqlConnection(strconn1);
                string sqlQuery = " select * from prt_mast where party_name='" + PName + "'";

                MySqlDataAdapter da = new MySqlDataAdapter(sqlQuery, conn);
                DataSet ds = new DataSet();
                da.Fill(ds, "iParty");
                DataRowView row = ds.Tables["iParty"].DefaultView[0];
                string PCode = row["party_code"].ToString();

                Session["PCODE"] = PCode;
                Session["FYEAR"] = FYear;




                lstShowField.DataSource = GetiJobs(PCode, FYear);
                lstShowField.DataTextField = "jobsno";
                lstShowField.DataValueField = "jobsno";

                lstShowField.DataBind();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('" + ex.Message + "');", true);
            }

        }
        public DataSet GetiJobs(string party, string FY)
        {



            MySqlConnection conn1 = new MySqlConnection(strconn1);
            string lstrsql = "";
            lstrsql = " select iworkreg.jobsno from iworkreg " +
                      " where iworkreg.party_Code='" + party + "' and " +
                      " iworkreg.job_no like '%" + FY + "%' order by iworkreg.jobsno";


            MySqlDataAdapter da = new MySqlDataAdapter(lstrsql, conn1);
            DataSet ds = new DataSet();
            da.Fill(ds, "iworkReg");
            return ds;
        }
        protected void BtnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                string str = DateTime.Now.ToString("yyyy/MM/dd hh:mm:ss");
                string lstr1 = lstShowField.Text;
                if (lstr1 == "")
                {
                    Response.Write("<script>alert('Which Data Want to be Add from  List')</script>");

                }
                else
                {
                    for (int iLoop = 0; iLoop <= lstShowField.Items.Count - 1; iLoop++)
                    {
                        if (lstShowField.Items[iLoop].Selected)
                        {

                            lstView.Items.Add(lstShowField.Items[iLoop].Text);
                        }
                    }
                }
                lstView.DataBind();
                GrdAllJob.Visible = false;
                ExportPage.Visible = false;
                // RBLExp.Visible = false;
                lblerror.Visible = false;
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('" + ex.Message + "');", true);
            }
        }
        protected void BtnRemove_Click(object sender, EventArgs e)
        {
            try
            {
                string lstr2 = lstView.Text;
                if (lstr2 == "")
                {
                    Response.Write("<script>alert('Which Data Want to be Remove from  List')</script>");

                }
                else
                {
                    for (int i = 0; i <= lstView.Items.Count - 1; i++)
                    {
                        if (lstView.Items[i].Selected)
                        {
                            lstView.Items.Remove(lstView.Items[i].Text);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('" + ex.Message + "');", true);
            }
        }
        protected void BtnAddAll_Click(object sender, EventArgs e)
        {
            try
            {
                lstView.Items.Clear();
                string lstr1 = lstShowField.Text;
                lngcol = 0;
                for (i = 0; i <= lstShowField.Items.Count - 1; i++)
                {
                    lstShowField.Items[i].Selected = true;
                    string lstTxt = lstShowField.Items[i].Text;
                    lstView.Items.Add(lstTxt);
                    lngcol = lngcol + 1;
                }
                BtnAddAll.Enabled = false;
                GrdAllJob.Visible = false;
                //  RBLExp.Visible = false;
                ExportPage.Visible = false;
                lblerror.Visible = false;
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('" + ex.Message + "');", true);
            }
        }

        protected void BtnRemoveAll_Click(object sender, EventArgs e)
        {
            try
            {
                string lstr2 = lstView.Text;
                lngcol = 0;
                lstView.Items.Clear();
                BtnAddAll.Enabled = true;
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
                if (lstView.Items.Count == 0)
                {
                    Response.Write("<script>alert('Please Give Reference Number') </script>");
                }
                else
                {
                    GrdAllJob.Visible = true;
                    RBLExp.Visible = true;
                    ExportPage.Visible = true;

                    GrdAllJob.DataSource = GetiWorkreg();
                    GrdAllJob.DataBind();

                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('" + ex.Message + "');", true);
            }
        }
        public DataSet GetiWorkreg()
        {


            // shipment movement Delivery status function
            GetiMovement();

            // shipment expected date of delivery function
            GetexDateDelivery();

            // Actual Delivery Date function
            GetDelivery();

            //Document Received Date Functions
            GetDocReceived();

            //Original Documents Received Date Functions
            GetOrginalDocReceived();

            QrySt = "";
            lngcol = 0;
            if ((lstView.Items.Count == 0))
            {
                Response.Write("<script>alert('Which Data Item want to be listed') </script>");

            }
            else
            {

                for (i = 0; i <= lstView.Items.Count - 1; i++)
                {
                    lstView.Items[i].Selected = true;
                    lstTxt = lstView.Items[i].Text;
                    lngcol = lngcol + 1;
                    lstTxt = lstTxt + ",";
                    QrySt = QrySt + lstTxt;
                }
            }
            jobNo = QrySt.TrimEnd(',');
            string FY = drYEar.SelectedValue;
            MySqlConnection conn1 = new MySqlConnection(strconn1);
            string lstrsql = "";
            lstrsql = " select iworkreg.job_no,iworkreg.jobsno,iworkreg.inv_dtl,ishp_dtl.rotn_no,ishp_dtl.rotn_date,ijob_pos.be_no,ijob_pos.be_date," +
                      " cnsr_mst.cnsr_name,iinv_dtl.inv_no,iinv_dtl.inv_date,tmpiMovement.mDate,tmpexDelivery.exDate,tmpDelivery.aDate,tmpDelivery.remark,tmpiDocReceived.adrDate,tmpiOrgDocReceived.orDate " +
                      " from iworkreg,ishp_dtl,iinv_dtl,cnsr_mst,ijob_pos,tmpiMovement,tmpexDelivery,tmpDelivery,tmpiDocReceived,tmpiOrgDocReceived " +
                      " where iworkreg.job_no=ishp_dtl.job_no and " +
                      " iworkreg.job_no=ijob_pos.job_no and " +
                      " iinv_dtl.cnsr_code=cnsr_mst.cnsr_code and " +
                      " ishp_dtl.job_no=iinv_dtl.job_no and " +
                      " iworkreg.job_no=tmpiMovement.job_no and " +
                      " iworkreg.job_no=tmpexDelivery.job_no and " +
                      " iworkreg.job_no=tmpDelivery.job_no and " +
                      " iworkreg.job_no=tmpiDocReceived.job_no and" +
                      " iworkreg.job_no=tmpiOrgDocReceived.job_no and" +
                      " iworkreg.job_no like '%" + FY + "%' and " +
                      " iworkreg.jobsno in(" + jobNo + ") order by iworkreg.jobsno";


            MySqlDataAdapter da = new MySqlDataAdapter(lstrsql, conn1);
            DataSet ds = new DataSet();
            da.Fill(ds, "iworkReg");
            return ds;
        }

        protected void GetiMovement()
        {
            MySqlConnection conn = new MySqlConnection(strconn1);
            string lstrdrp = "drop table if exists tmpiMovement";
            conn.Open();
            MySqlDataAdapter dap = new MySqlDataAdapter();
            MySqlCommand cmdp = new MySqlCommand(lstrdrp, conn);
            cmdp.CommandText = lstrdrp;
            cmdp.Connection = conn;
            dap.SelectCommand = cmdp;

            int result1 = cmdp.ExecuteNonQuery();

            QrySt = "";
            string FY = drYEar.SelectedValue;

            lngcol = 0;
            if ((lstView.Items.Count == 0))
            {
                Response.Write("<script>alert('Which Data Item want to be listed') </script>");

            }
            else
            {

                for (i = 0; i <= lstView.Items.Count - 1; i++)
                {
                    lstView.Items[i].Selected = true;
                    lstTxt = lstView.Items[i].Text;
                    lngcol = lngcol + 1;

                    //to get String Count 
                    string strLeng = "";
                    strLeng = lstTxt.Length.ToString();
                    if (strLeng == "1")
                        lstTxt = "0000" + lstTxt;
                    else if (strLeng == "2")
                        lstTxt = "000" + lstTxt;
                    else if (strLeng == "3")
                        lstTxt = "00" + lstTxt;
                    else if (strLeng == "4")
                        lstTxt = "0" + lstTxt;

                    lstTxt = "'IMP/" + lstTxt + "/" + FY + "'";
                    lstTxt = lstTxt + ",";
                    QrySt = QrySt + lstTxt;
                }
            }
            string mJobNo = QrySt.TrimEnd(',');

            string sg = "movement00";
            MySqlConnection conn1 = new MySqlConnection(strconn1);
            string lstrsql = "";
            lstrsql = "create table tmpiMovement as select job_no,date as mDate from impjobstage " +
                      " where job_stage='" + sg + "' and " +
                      " job_no in(" + mJobNo + ")";
            conn1.Open();
            MySqlDataAdapter da = new MySqlDataAdapter();
            MySqlCommand cmd = new MySqlCommand(lstrsql, conn1);
            cmd.CommandText = lstrsql;
            cmd.Connection = conn1;
            da.SelectCommand = cmd;


            int result = cmd.ExecuteNonQuery();
            conn1.Close();

        }

        protected void GetexDateDelivery()
        {
            MySqlConnection conn = new MySqlConnection(strconn1);
            string lstrdrp = "drop table if exists tmpexDelivery";
            conn.Open();
            MySqlDataAdapter dap = new MySqlDataAdapter();
            MySqlCommand cmdp = new MySqlCommand(lstrdrp, conn);
            cmdp.CommandText = lstrdrp;
            cmdp.Connection = conn;
            dap.SelectCommand = cmdp;

            int result1 = cmdp.ExecuteNonQuery();

            QrySt = "";
            string FY = drYEar.SelectedValue;
            lngcol = 0;
            if ((lstView.Items.Count == 0))
            {
                Response.Write("<script>alert('Which Data Item want to be listed') </script>");

            }
            else
            {

                for (i = 0; i <= lstView.Items.Count - 1; i++)
                {
                    lstView.Items[i].Selected = true;
                    lstTxt = lstView.Items[i].Text;
                    lngcol = lngcol + 1;

                    //to get String Count 
                    string strLeng = "";
                    strLeng = lstTxt.Length.ToString();
                    if (strLeng == "1")
                        lstTxt = "0000" + lstTxt;
                    else if (strLeng == "2")
                        lstTxt = "000" + lstTxt;
                    else if (strLeng == "3")
                        lstTxt = "00" + lstTxt;
                    else if (strLeng == "4")
                        lstTxt = "0" + lstTxt;
                    lstTxt = "'IMP/" + lstTxt + "/" + FY + "'";
                    lstTxt = lstTxt + ",";
                    QrySt = QrySt + lstTxt;
                }
            }
            string eJobNo = QrySt.TrimEnd(',');

            string sg = "EDODELIVER";
            MySqlConnection conn1 = new MySqlConnection(strconn1);
            string lstrsql = "";
            lstrsql = "create table tmpexDelivery as select job_no,date as exDate from impjobstage " +
                      " where job_stage='" + sg + "' and " +
                      " job_no in(" + eJobNo + ")";
            conn1.Open();
            MySqlDataAdapter da = new MySqlDataAdapter();
            MySqlCommand cmd = new MySqlCommand(lstrsql, conn1);
            cmd.CommandText = lstrsql;
            cmd.Connection = conn1;
            da.SelectCommand = cmd;


            int result = cmd.ExecuteNonQuery();
            conn1.Close();

        }

        protected void GetDelivery()
        {
            MySqlConnection conn = new MySqlConnection(strconn1);
            string lstrdrp = "drop table if exists tmpDelivery";
            conn.Open();
            MySqlDataAdapter dap = new MySqlDataAdapter();
            MySqlCommand cmdp = new MySqlCommand(lstrdrp, conn);
            cmdp.CommandText = lstrdrp;
            cmdp.Connection = conn;
            dap.SelectCommand = cmdp;

            int result1 = cmdp.ExecuteNonQuery();

            QrySt = "";
            string FY = drYEar.SelectedValue;
            lngcol = 0;
            if ((lstView.Items.Count == 0))
            {
                Response.Write("<script>alert('Which Data Item want to be listed') </script>");

            }
            else
            {

                for (i = 0; i <= lstView.Items.Count - 1; i++)
                {
                    lstView.Items[i].Selected = true;
                    lstTxt = lstView.Items[i].Text;
                    lngcol = lngcol + 1;

                    //to get String Count 
                    string strLeng = "";
                    strLeng = lstTxt.Length.ToString();
                    if (strLeng == "1")
                        lstTxt = "0000" + lstTxt;
                    else if (strLeng == "2")
                        lstTxt = "000" + lstTxt;
                    else if (strLeng == "3")
                        lstTxt = "00" + lstTxt;
                    else if (strLeng == "4")
                        lstTxt = "0" + lstTxt;

                    lstTxt = "'IMP/" + lstTxt + "/" + FY + "'";
                    lstTxt = lstTxt + ",";
                    QrySt = QrySt + lstTxt;
                }
            }
            string aJobNo = QrySt.TrimEnd(',');

            string sg = "Delivery";
            MySqlConnection conn1 = new MySqlConnection(strconn1);
            string lstrsql = "";
            lstrsql = "create table tmpDelivery as select job_no,remark,date as aDate from impjobstage " +
                      " where job_stage='" + sg + "' and " +
                      " job_no in(" + aJobNo + ")";
            conn1.Open();
            MySqlDataAdapter da = new MySqlDataAdapter();
            MySqlCommand cmd = new MySqlCommand(lstrsql, conn1);
            cmd.CommandText = lstrsql;
            cmd.Connection = conn1;
            da.SelectCommand = cmd;


            int result = cmd.ExecuteNonQuery();
            conn1.Close();

        }

        protected void GetDocReceived()
        {
            MySqlConnection conn = new MySqlConnection(strconn1);
            string lstrdrp = "drop table if exists tmpiDocReceived";
            conn.Open();
            MySqlDataAdapter dap = new MySqlDataAdapter();
            MySqlCommand cmdp = new MySqlCommand(lstrdrp, conn);
            cmdp.CommandText = lstrdrp;
            cmdp.Connection = conn;
            dap.SelectCommand = cmdp;

            int result1 = cmdp.ExecuteNonQuery();

            QrySt = "";
            string FY = drYEar.SelectedValue;
            lngcol = 0;
            if ((lstView.Items.Count == 0))
            {
                Response.Write("<script>alert('Which Data Item want to be listed') </script>");

            }
            else
            {

                for (i = 0; i <= lstView.Items.Count - 1; i++)
                {
                    lstView.Items[i].Selected = true;
                    lstTxt = lstView.Items[i].Text;
                    lngcol = lngcol + 1;

                    //to get String Count 
                    string strLeng = "";
                    strLeng = lstTxt.Length.ToString();
                    if (strLeng == "1")
                        lstTxt = "0000" + lstTxt;
                    else if (strLeng == "2")
                        lstTxt = "000" + lstTxt;
                    else if (strLeng == "3")
                        lstTxt = "00" + lstTxt;
                    else if (strLeng == "4")
                        lstTxt = "0" + lstTxt;

                    lstTxt = "'IMP/" + lstTxt + "/" + FY + "'";
                    lstTxt = lstTxt + ",";
                    QrySt = QrySt + lstTxt;
                }
            }
            string mJobNo = QrySt.TrimEnd(',');

            string sg = "DRDATE000E";
            MySqlConnection conn1 = new MySqlConnection(strconn1);
            string lstrsql = "";
            lstrsql = "create table tmpiDocReceived as select job_no,date as adrDate from impjobstage " +
                      " where job_stage='" + sg + "' and " +
                      " job_no in(" + mJobNo + ")";
            conn1.Open();
            MySqlDataAdapter da = new MySqlDataAdapter();
            MySqlCommand cmd = new MySqlCommand(lstrsql, conn1);
            cmd.CommandText = lstrsql;
            cmd.Connection = conn1;
            da.SelectCommand = cmd;


            int result = cmd.ExecuteNonQuery();
            conn1.Close();

        }

        protected void GetOrginalDocReceived()
        {
            MySqlConnection conn = new MySqlConnection(strconn1);
            string lstrdrp = "drop table if exists tmpiOrgDocReceived";
            conn.Open();
            MySqlDataAdapter dap = new MySqlDataAdapter();
            MySqlCommand cmdp = new MySqlCommand(lstrdrp, conn);
            cmdp.CommandText = lstrdrp;
            cmdp.Connection = conn;
            dap.SelectCommand = cmdp;

            int result1 = cmdp.ExecuteNonQuery();

            QrySt = "";
            string FY = drYEar.SelectedValue;
            lngcol = 0;
            if ((lstView.Items.Count == 0))
            {
                Response.Write("<script>alert('Which Data Item want to be listed') </script>");

            }
            else
            {

                for (i = 0; i <= lstView.Items.Count - 1; i++)
                {
                    lstView.Items[i].Selected = true;
                    lstTxt = lstView.Items[i].Text;
                    lngcol = lngcol + 1;

                    //to get String Count 
                    string strLeng = "";
                    strLeng = lstTxt.Length.ToString();
                    if (strLeng == "1")
                        lstTxt = "0000" + lstTxt;
                    else if (strLeng == "2")
                        lstTxt = "000" + lstTxt;
                    else if (strLeng == "3")
                        lstTxt = "00" + lstTxt;
                    else if (strLeng == "4")
                        lstTxt = "0" + lstTxt;

                    lstTxt = "'IMP/" + lstTxt + "/" + FY + "'";
                    lstTxt = lstTxt + ",";
                    QrySt = QrySt + lstTxt;
                }
            }
            string mJobNo = QrySt.TrimEnd(',');

            string sg = "ODRDATE000";
            MySqlConnection conn1 = new MySqlConnection(strconn1);
            string lstrsql = "";
            lstrsql = "create table tmpiOrgDocReceived as select job_no,date as orDate from impjobstage " +
                      " where job_stage='" + sg + "' and " +
                      " job_no in(" + mJobNo + ")";
            conn1.Open();
            MySqlDataAdapter da = new MySqlDataAdapter();
            MySqlCommand cmd = new MySqlCommand(lstrsql, conn1);
            cmd.CommandText = lstrsql;
            cmd.Connection = conn1;
            da.SelectCommand = cmd;


            int result = cmd.ExecuteNonQuery();
            conn1.Close();

        }

        protected void GrdAllJob_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                BtnSearch_Click(sender, e);
                //CreateTempTable();
                //GrdAllJob.DataSource = SortDataTable(CType(GrdAllJob.DataSource, DataTable), True)
                GrdAllJob.PageIndex = e.NewPageIndex;
                GrdAllJob.DataBind();
                //string formID = "UJOBFm";
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('" + ex.Message + "');", true);
            }

        }

        protected void BtnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/PIPL/JobReports/Index.aspx");

        }

        protected void GrdAllJob_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                e.Row.Attributes.Add("onmouseover", "MouseEvents(this, event)");
                e.Row.Attributes.Add("onmouseout", "MouseEvents(this, event)");

                if (e.Row.Cells[5].Text != "&nbsp;")
                {
                    DateTime bDate = Convert.ToDateTime(e.Row.Cells[5].Text);
                    e.Row.Cells[5].Text = bDate.ToString("MM/dd/yyyy");
                }
                if (e.Row.Cells[6].Text != "&nbsp;")
                {
                    DateTime etaDate = Convert.ToDateTime(e.Row.Cells[6].Text);
                    e.Row.Cells[6].Text = etaDate.ToString("MM/dd/yyyy");
                }
                if (e.Row.Cells[7].Text != "&nbsp;")
                {
                    DateTime etaDate = Convert.ToDateTime(e.Row.Cells[7].Text);
                    e.Row.Cells[7].Text = etaDate.ToString("MM/dd/yyyy");
                }
                if (e.Row.Cells[9].Text != "&nbsp;")
                {
                    DateTime beDate = Convert.ToDateTime(e.Row.Cells[9].Text);
                    e.Row.Cells[9].Text = beDate.ToString("MM/dd/yyyy");
                }

                if (e.Row.Cells[11].Text != "&nbsp;")
                {
                    DateTime beDate = Convert.ToDateTime(e.Row.Cells[11].Text);
                    e.Row.Cells[11].Text = beDate.ToString("MM/dd/yyyy");
                }
                if (e.Row.Cells[12].Text != "&nbsp;")
                {
                    DateTime beDate = Convert.ToDateTime(e.Row.Cells[12].Text);
                    e.Row.Cells[12].Text = beDate.ToString("MM/dd/yyyy");
                }
                if (e.Row.Cells[13].Text != "&nbsp;")
                {
                    DateTime beDate = Convert.ToDateTime(e.Row.Cells[13].Text);
                    e.Row.Cells[13].Text = beDate.ToString("MM/dd/yyyy");
                }
                if (e.Row.Cells[14].Text != "&nbsp;")
                {
                    DateTime beDate = Convert.ToDateTime(e.Row.Cells[14].Text);
                    e.Row.Cells[14].Text = beDate.ToString("MM/dd/yyyy");
                }
            }
        }

        protected void ExportPage_Click(object sender, EventArgs e)
        {
            try
            {
                BtnSearch_Click(sender, e);
                string sysDates = DateTime.Now.ToString("dd-MMM-yyyy");
                string FileName = "PortStatusHC" + sysDates;
                string strFileName = FileName + ".xls";
                if (RBLExp.SelectedIndex == 0)
                {
                    GridViewExportDet.ExportExcell(strFileName, GrdAllJob);
                }
                else if (RBLExp.SelectedIndex == 1)
                {
                    GrdAllJob.AllowPaging = false;
                    GrdAllJob.DataBind();
                    GridViewExportDet.ExportExcell(strFileName, GrdAllJob);
                }

                else
                {
                    Response.Write("<script> alert('Which Page do you want Export')</script>");
                    // ScriptManager.RegisterStartupScript(Page, GetType(), "Mail", "alert('Which Page do you want Export')", true);

                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('" + ex.Message + "');", true);
            }
        }
    }
}