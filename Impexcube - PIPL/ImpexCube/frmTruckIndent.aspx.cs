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
    public partial class frmTruckIndent : System.Web.UI.Page
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
        //    string formID = "UJOBFm";
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack == false)
            {

                lblerror.Visible = false;
                string FY = (string)Session["FinancialYear"];
                drYEar.SelectedValue = FY;
                // User Authentication Code 
                // txtPName.Text = "select";

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
                    //  RBLExp.Visible = true;
                    ExportPage.Visible = true;

                    GrdAllJob.DataSource = GetiWorkreg();

                    GrdAllJob.DataBind();

                    // Authenticate.Forms(formID);
                    string Dis = (string)Session["DISABLE"];
                    string ROnly = (string)Session["ROnly"];
                    if (ROnly == "1")
                    {
                        foreach (GridViewRow Row in GrdAllJob.Rows)
                        {
                            Row.Cells[11].Enabled = false;
                        }
                        lbROnly.Visible = true;
                    }
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('" + ex.Message + "');", true);
            }
        }
        public DataSet GetiWorkreg()
        {

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
            lstrsql = " select iworkreg.job_no,iworkreg.jobsno,iworkreg.inv_dtl,iworkreg.PORT_OF_SH,iworkreg.cont_of_sh,iworkreg.frt_bank," +
                      " cnsr_mst.cnsr_name,iinv_dtl.inv_no,iinv_dtl.inv_date " +
                      " from iworkreg,ishp_dtl,iinv_dtl,cnsr_mst " +
                      " where iworkreg.job_no=ishp_dtl.job_no and " +
                      " iinv_dtl.cnsr_code=cnsr_mst.cnsr_code and " +
                      " ishp_dtl.job_no=iinv_dtl.job_no and " +
                      " iworkreg.job_no like '%" + FY + "%' and " +
                      " iworkreg.jobsno in(" + jobNo + ") order by iworkreg.jobsno";


            MySqlDataAdapter da = new MySqlDataAdapter(lstrsql, conn1);
            DataSet ds = new DataSet();
            da.Fill(ds, "iworkReg");
            return ds;
        }



        protected void GrdAllJob_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            BtnSearch_Click(sender, e);
            // CreateTempTable();
            //GrdAllJob.DataSource = SortDataTable(CType(GrdAllJob.DataSource, DataTable), True)
            GrdAllJob.PageIndex = e.NewPageIndex;
            GrdAllJob.DataBind();
            //string formID = "UJOBFm";
            //Authenticate.Forms(formID);
            string Dis = (string)Session["DISABLE"];
            string ROnly = (string)Session["ROnly"];
            if (ROnly == "1")
            {
                foreach (GridViewRow Row in GrdAllJob.Rows)
                {
                    Row.Cells[11].Enabled = false;
                }
                lbROnly.Visible = true;
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
                    DateTime etaDate = Convert.ToDateTime(e.Row.Cells[5].Text);
                    e.Row.Cells[5].Text = etaDate.ToString("MM/dd/yyyy");
                }

            }
        }
        protected void ExportPage_Click(object sender, EventArgs e)
        {
            try
            {
                BtnSearch_Click(sender, e);
                string sysDates = DateTime.Now.ToString("dd-MMM-yyyy");
                string FileName = "TruckIndent" + sysDates;
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