using System;
using System.Data;
using System.Data.Common;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
//using MySql;
//using MySql.Data.MySqlClient;
using System.Data.SqlClient;

namespace ImpexCube
{
    public partial class frmAgeingReport : System.Web.UI.Page
    {
        string strconn = (string)ConfigurationManager.ConnectionStrings["Constr"].ConnectionString;
      
        string strconnVI = (string)ConfigurationManager.AppSettings["ConnectionVisual"];
        string strconnDash = (string)ConfigurationManager.AppSettings["ConnectionDashboard"];
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack == false)
            {
                try
                {

                    txtPName.Enabled = false;
                    //string formID = "Ageing Report";

                    //Authenticate.Forms(formID);
                    //string Validate = (string)Session["DISABLE"];
                    //if (Validate == "True")
                    //{

                        txtFdate.Text = (string)Session["fdate"];
                        string todate = System.DateTime.Now.ToString("dd/MM/yyyy");
                        txtTdate.Text = todate;

                    //}
                    //else
                    //{
                    //    ScriptManager.RegisterStartupScript(this, this.GetType(), "redirect", "alert('You have not Authorized for this Page'); window.location.href='HomePage.aspx';", true);

                    //}
                }
                catch (Exception ex)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('" + ex.Message + "');", true);
                }


            }
        }
        //public DataSet GetData(string sqlQuery)
        //{

        //    MySqlConnection conn = new MySqlConnection(strconn);
         
        //    MySqlDataAdapter da = new MySqlDataAdapter(sqlQuery, conn);

        //    DataSet ds = new DataSet();
          
        //    da.Fill(ds, "iworkreg");

        //    return ds;
        //}

        public DataSet GetDataSql(string sqlQuery)
        {

            SqlConnection conn = new SqlConnection(strconn);

            SqlDataAdapter da = new SqlDataAdapter(sqlQuery, conn);

            DataSet ds = new DataSet();

            da.Fill(ds, "iworkreg");

            return ds;
        }

        protected void BtnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                ExportPage.Visible = true;
                string FD = txtFdate.Text;
                string TD = txtTdate.Text;

                string sMM = FD.Substring(3, 2);
                string sDD = FD.Substring(0, 2);
                string sYY = FD.Substring(6, 4);

                string sMode = drshipment.SelectedValue;
                string sStatus = drStatus.SelectedValue;
              
                FD = sMM + "/" + sDD + "/" + sYY;
                Session["QUERY"] = "";

                string eMM = TD.Substring(3, 2);
                string eDD = TD.Substring(0, 2);
                string eYY = TD.Substring(6, 4);
                TD = eMM + "/" + eDD + "/" + eYY;

                DateTime fd = Convert.ToDateTime(FD);
                DateTime td = Convert.ToDateTime(TD);

                string fDate = fd.ToString("MM/dd/yyyy");
                string tDate = td.ToString("MM/dd/yyyy");

                string pName = txtPName.Text;
               
                ExportPage.Visible = true;
                string sqlQueryVal = "";

                if (tDate == "")
                    tDate = fDate;

                if (fDate == "")
                {
                    Response.Write("<script>" + "alert('Please Give  Date Values Or Job No');" + "</script>");
                }
                else
                {
                    GetRPT(fDate, tDate, pName, sMode, sStatus);
                }
                sqlQueryVal = (string)Session["QUERY"];
                if (sqlQueryVal != "")
                {
                    Grdiworkreg.DataSource = GetDataSql(sqlQueryVal);
                    Grdiworkreg.DataBind();
                
                    Grdiworkreg.Visible = true;
                }

                if (Grdiworkreg.PageCount == 0)
                  
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Javascript", "alert('Records Not Found for Given Values');", true);
                else
                {
                    ExportPage.Visible = true;
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('" + ex.Message + "');", true);
            }
        }

        protected void GetRPT(string fDate, string tDate, string pName, string sMode, string status)
        {
            string sqlQuery = "";
            string sqlQueryVal = "";
            string sts = "";
            if (status == "1")
                sts = "status_job='Y'";
            else if (status == "-1")
                sts = "status_job ='N'";

            if (chkIMP.Checked == true && sMode == "0" && status == "0")
                sqlQuery = "JobReceivedDate >='" + fDate + "' and JobReceivedDate <= '" + tDate + "'  and Importer='" + pName + "'";
            else if (chkIMP.Checked == true && sMode != "0" && status == "0")
                sqlQuery = "JobReceivedDate >='" + fDate + "' and JobReceivedDate <= '" + tDate + "' and Importer='" + pName + "' and mode='" + sMode + "'";
            else if (chkIMP.Checked == true && sMode == "0" && status != "0")
                sqlQuery = "JobReceivedDate >='" + fDate + "' and JobReceivedDate <= '" + tDate + "' and Importer='" + pName + "' and " + sts + "";
            else if (chkIMP.Checked == true && sMode != "0" && status != "0")
                sqlQuery = "JobReceivedDate >='" + fDate + "' and JobReceivedDate <= '" + tDate + "' and Importer='" + pName + "' and " + sts + " and mode='" + sMode + "'";
            else if (chkIMP.Checked == false && sMode != "0" && status == "0")
                sqlQuery = "JobReceivedDate >='" + fDate + "' and JobReceivedDate <= '" + tDate + "' and mode='" + sMode + "'";
            else if (chkIMP.Checked == false && sMode == "0" && status != "0")
                sqlQuery = "JobReceivedDate >='" + fDate + "' and JobReceivedDate <= '" + tDate + "' and " + sts + "";
            else if (chkIMP.Checked == false && sMode != "0" && status != "0")
                sqlQuery = "JobReceivedDate >='" + fDate + "' and JobReceivedDate <= '" + tDate + "' and " + sts + " and mode='" + sMode + "'";
            else
                sqlQuery = "JobReceivedDate >='" + fDate + "' and JobReceivedDate <= '" + tDate + "'";

            sqlQueryVal = " Select * from  (SELECT distinct jobno,mode ,Importer,ProductDesc, " +
                         "TotalAssVal,TotalDuty,NoOfPackages ,PackagesUnit,JobReceivedDate, " +
                         " GLDInwardDate,BENO,BEDATE, " +
                         " invoice,invoiceDate,DebitNo,DebitDate,ID, " +
                         " convert(varchar(11),StatusDate,106) as StatusDate " +
                         " FROM View_ImportAgeingReport " +
                         "where  " +
                         " " + sqlQuery + " ) " +
                         " as s pivot(Max(statusdate) for ID in ([52],[17],[53],[26],[50],[44],[21])) as pivottable ";



            //sqlQueryVal = "select distinct jobno,mode ,Importer,InvoiceDetail," +
            //              "TotalAssVal,TotalDuty,NoOfPackages ,PackagesUnit,JobReceivedDate,GLDInwardDate,BENO,BEDATE," +
            //              "dutyInform_date,dutyRecd_date,dutyPaid_date,OOC_Date,DELIVERY_DATE,forwardAC_DATE," +
            //              "Billing_date,BILL_NO,BILL_DATE,DB_NOTE_NO,DB_DATE,cntr_move_date " +
            //              "from View_ImportJobStatusUpdate " +
            //              "where  " +
            //              " " + sqlQuery + "  ";


            Session["QUERY"] = sqlQueryVal;

        }

        //public DataSet GetSP(DateTime fDate, DateTime tDate)
        //{
        //    MySqlConnection conn = new MySqlConnection(strconn);
        //    conn.Open();
        //    MySqlCommand cmd = new MySqlCommand("spJOBStatusVAL", conn);
        //    cmd.CommandType = CommandType.StoredProcedure;
        //    cmd.Parameters.Add("fdate", MySqlDbType.Datetime).Value = fDate;
        //    cmd.Parameters.Add("tdate", MySqlDbType.Datetime).Value = tDate;
        //    MySqlDataAdapter da = new MySqlDataAdapter(cmd);
        //    DataSet ds = new DataSet();
        //    da.Fill(ds, "spJOBStatusVAL");
         
        //    conn.Close();
        //    return ds;
        //}
        protected void ExportPage_Click(object sender, EventArgs e)
        {
            try
            {
                string sysDates = DateTime.Now.ToString("ddMMyyyy");
                string FileName = "AgeingRPT" + sysDates;
                string strFileName = FileName + ".xls";
              
                if (Grdiworkreg.PageCount != 0)
                    GridViewExportUtil.ExportExcell(strFileName, Grdiworkreg);
                else
                    Response.Write("<script>" + "alert('Which Record want to be Export ???');" + "</script>");
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('" + ex.Message + "');", true);
            }
        }
        //protected void GetCommand(string Query, string connSTR)
        //{

        //    MySqlConnection conn = new MySqlConnection(connSTR);
        //    conn.Open();
        //    MySqlCommand cmd = new MySqlCommand(Query, conn);
        //    cmd.CommandText = Query;
        //    cmd.Connection = conn;
        //    int res = cmd.ExecuteNonQuery();
        //    conn.Close();
        //}
        //protected void GetFormLoad()
        //{
        //    string sqlQuery = "select i.job_no,i.doc_recd,i.jobsno,i.INV_DTL,i.cont_orig,pm.Importer,ishp.carrier,ishp.mawb_no,ishp.mawb_date,ishp.PKG_UNIT,ishp.NO_OF_PKG,ishp.eta,ishp.GROSS_UNIT,ishp.GROSS_WT,ijp.be_no,ijp.be_date,ijs.pend_remark " +
        //                        "from iworkreg i,iworkreg_jobstatus ijs,ishp_dtl ishp,prt_mast pm,ijob_pos ijp " +
        //                        "where i.job_no=ijs.job_no and i.job_no=ishp.job_no and i.job_no=ijp.job_no and i.party_code=pm.party_code " +
        //                        "order by i.job_no";
        //    Grdiworkreg.DataSource = GetData(sqlQuery);
        //    Grdiworkreg.DataBind();
        //}
        protected void Grdiworkreg_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (e.Row.Cells[9].Text == "01-Jan-1900" || e.Row.Cells[9].Text == "01 Jan 1900")
                {

                    e.Row.Cells[9].Text = "";
                }
                if (e.Row.Cells[10].Text == "01-Jan-1900" || e.Row.Cells[10].Text == "01 Jan 1900")
                {

                    e.Row.Cells[10].Text = "";
                }

                if (e.Row.Cells[12].Text == "01-Jan-1900" || e.Row.Cells[12].Text == "01 Jan 1900")
                {

                    e.Row.Cells[12].Text = "";
                }

                if (e.Row.Cells[13].Text == "01-Jan-1900" || e.Row.Cells[13].Text == "01 Jan 1900")
                {

                    e.Row.Cells[13].Text = "";
                }
                if (e.Row.Cells[14].Text == "01-Jan-1900" || e.Row.Cells[14].Text == "01 Jan 1900")
                {

                    e.Row.Cells[14].Text = "";
                }
                if (e.Row.Cells[15].Text == "01-Jan-1900" || e.Row.Cells[15].Text == "01 Jan 1900")
                {

                    e.Row.Cells[15].Text = "";
                }
                if (e.Row.Cells[16].Text == "01-Jan-1900" || e.Row.Cells[16].Text == "01 Jan 1900")
                {

                    e.Row.Cells[16].Text = "";
                }
                if (e.Row.Cells[17].Text == "01-Jan-1900" || e.Row.Cells[17].Text == "01 Jan 1900")
                {

                    e.Row.Cells[17].Text = "";
                }
                if (e.Row.Cells[18].Text == "01-Jan-1900" || e.Row.Cells[18].Text == "01 Jan 1900")
                {

                    e.Row.Cells[18].Text = "";
                }
                if (e.Row.Cells[19].Text == "01-Jan-1900" || e.Row.Cells[19].Text == "01 Jan 1900")
                {

                    e.Row.Cells[19].Text = "";
                }
                if (e.Row.Cells[21].Text == "01-Jan-1900" || e.Row.Cells[21].Text == "01 Jan 1900")
                {
                    e.Row.Cells[21].Text = "";
                }
            }

            try
            {
                DateTime start = new DateTime();
                DateTime end = new DateTime();
                string Dayss = "";
                string Res = "";
                if (e.Row.RowType == DataControlRowType.DataRow)
                {

                    // Ageing Report for Delivery and Billing days

                    string del_date = e.Row.Cells[17].Text;
                    string Bill_date = e.Row.Cells[21].Text;
                    if (del_date != "&nbsp;")
                    {

                        start = Convert.ToDateTime(del_date);
                        if (Bill_date != "&nbsp;")
                        {
                            //end = Convert.ToDateTime(Bill_date);

                            end = Convert.ToDateTime(Bill_date);
                            Res = "";
                        }
                        else
                        {
                            end = System.DateTime.Now;
                            Res = " - Bill Pending";
                        }
                        int workingDays = 0;
                        while (start < end)
                        {
                            if (start.DayOfWeek != DayOfWeek.Sunday)
                            {
                                workingDays += 1;
                            }
                            start = start.AddDays(1);
                            Dayss = Convert.ToString(workingDays);

                        }
                        if (Dayss != "")
                            e.Row.Cells[25].Text = Dayss + Res;
                    }
                    // Ageing Report for InWard and OOC Taken days

                    string InwardDate = e.Row.Cells[10].Text;
                    string OOCTakenI = e.Row.Cells[16].Text;
                    if (OOCTakenI != "&nbsp;" && InwardDate != "&nbsp;"&& OOCTakenI != "" && InwardDate != "")
                    {
                        start = Convert.ToDateTime(InwardDate);
                        if (OOCTakenI != "&nbsp;")
                        {
                            end = Convert.ToDateTime(OOCTakenI);
                            Res = "";
                        }
                        else
                        {
                            end = System.DateTime.Now;
                            Res = " - OOC Taken Pending";
                        }
                        int workingDays = 0;
                        while (start < end)
                        {
                            if (start.DayOfWeek != DayOfWeek.Sunday)
                            {
                                workingDays += 1;
                            }
                            start = start.AddDays(1);
                            Dayss = Convert.ToString(workingDays);

                        }
                        if (Dayss != "")
                            e.Row.Cells[26].Text = Dayss + Res;
                    }

                    // Ageing Report for BEDate and OOC Taken days

                    string BEDATE = e.Row.Cells[12].Text;
                    string OOCTakenB = e.Row.Cells[16].Text;
                    if (OOCTakenB != "&nbsp;" && BEDATE != "&nbsp;" && OOCTakenB != "" && BEDATE != "")
                    {
                        start = Convert.ToDateTime(BEDATE);
                        if (OOCTakenB != "&nbsp;")
                        {
                            end = Convert.ToDateTime(OOCTakenB);
                            Res = "";
                        }
                        else
                        {
                            end = System.DateTime.Now;
                            Res = " - OOC Taken Pending";
                        }
                        int workingDays = 0;
                        while (start < end)
                        {
                            if (start.DayOfWeek != DayOfWeek.Sunday)
                            {
                                workingDays += 1;
                            }
                            start = start.AddDays(1);
                            Dayss = Convert.ToString(workingDays);
                        }
                        if (Dayss != "")
                            e.Row.Cells[27].Text = Dayss + Res;
                    }
                    // Ageing Report for BEDate and OOC Taken days

                    string DutyPaid = e.Row.Cells[15].Text;
                    string OOCTakenD = e.Row.Cells[16].Text;
                    if (OOCTakenD != "&nbsp;" && DutyPaid != "&nbsp;" && OOCTakenD != "" && DutyPaid != "")
                    {
                        start = Convert.ToDateTime(DutyPaid);
                        if (OOCTakenD != "&nbsp;")
                        {
                            end = Convert.ToDateTime(OOCTakenD);
                            Res = "";
                        }
                        else
                        {
                            end = System.DateTime.Now;
                            Res = " - OOC Taken Pending";
                        }
                        int workingDays = 0;
                        while (start < end)
                        {
                            if (start.DayOfWeek != DayOfWeek.Sunday)
                            {
                                workingDays += 1;
                            }
                            start = start.AddDays(1);
                            Dayss = Convert.ToString(workingDays);

                        }
                        if (Dayss != "")
                            e.Row.Cells[28].Text = Dayss + Res;
                    }
                    // Ageing Report for Delivery and Billing days

                    string Cntr_Move = e.Row.Cells[24].Text;
                    string OOCTakenC = e.Row.Cells[16].Text;
                    if (OOCTakenC != "&nbsp;" && Cntr_Move != "&nbsp;" && OOCTakenC != "" && Cntr_Move != "")
                    {
                        start = Convert.ToDateTime(OOCTakenC);
                        if (Cntr_Move != "&nbsp;")
                        {
                            if (Cntr_Move != "")
                            {
                                end = Convert.ToDateTime(Cntr_Move);
                                Res = "";
                            }
                        }
                        else
                        {
                            end = System.DateTime.Now;
                            Res = " - OOC Taken Pending";
                        }
                        int workingDays = 0;
                        while (start < end)
                        {
                            if (start.DayOfWeek != DayOfWeek.Sunday)
                            {
                                workingDays += 1;
                            }
                            start = start.AddDays(1);
                            Dayss = Convert.ToString(workingDays);

                        }
                        if (Dayss != "")
                            e.Row.Cells[29].Text = Dayss + Res;
                    }
                    // Ageing Report for Doc Received and OOC Date days

                    string Doc_rece = e.Row.Cells[9].Text;
                    string OOCTakenDR = e.Row.Cells[16].Text;
                    if (OOCTakenC != "&nbsp;" && Doc_rece != "&nbsp;" && OOCTakenC != "" && Doc_rece != "")
                    {
                        start = Convert.ToDateTime(OOCTakenC);
                        if (Doc_rece != "&nbsp;")
                        {
                            end = Convert.ToDateTime(Doc_rece);
                            Res = "";
                        }
                        else
                        {
                            end = System.DateTime.Now;
                            Res = " - OOC Taken Pending";
                        }
                        int workingDays = 0;
                        while (start < end)
                        {
                            if (start.DayOfWeek != DayOfWeek.Sunday)
                            {
                                workingDays += 1;
                            }
                            start = start.AddDays(1);
                            Dayss = Convert.ToString(workingDays);

                        }
                        if (Dayss != "")
                            e.Row.Cells[30].Text = Dayss + Res;
                    }
                    //if (e.Row.Cells[25].Text != "&nbsp;")
                    //{
                    //    DateTime bDate = Convert.ToDateTime(e.Row.Cells[25].Text);
                    //    e.Row.Cells[25].Text = bDate.ToString("dd/MM/yyyy");
                    //}
                    //if (e.Row.Cells[9].Text != "&nbsp;")
                    //{
                    //    DateTime bDate = Convert.ToDateTime(e.Row.Cells[9].Text);
                    //    e.Row.Cells[9].Text = bDate.ToString("dd/MM/yyyy");
                    //}
                    //if (e.Row.Cells[10].Text != "&nbsp;")
                    //{
                    //    DateTime bDate = Convert.ToDateTime(e.Row.Cells[10].Text);
                    //    e.Row.Cells[10].Text = bDate.ToString("dd/MM/yyyy");
                    //}
                    //if (e.Row.Cells[12].Text != "&nbsp;")
                    //{
                    //    DateTime bDate = Convert.ToDateTime(e.Row.Cells[12].Text);
                    //    e.Row.Cells[12].Text = bDate.ToString("dd/MM/yyyy");
                    //}
                    //if (e.Row.Cells[13].Text != "&nbsp;")
                    //{
                    //    DateTime bDate = Convert.ToDateTime(e.Row.Cells[13].Text);
                    //    e.Row.Cells[13].Text = bDate.ToString("dd/MM/yyyy");
                    //}
                    //if (e.Row.Cells[14].Text != "&nbsp;")
                    //{
                    //    DateTime bDate = Convert.ToDateTime(e.Row.Cells[14].Text);
                    //    e.Row.Cells[14].Text = bDate.ToString("dd/MM/yyyy");
                    //}
                    //if (e.Row.Cells[15].Text != "&nbsp;")
                    //{
                    //    DateTime bDate = Convert.ToDateTime(e.Row.Cells[15].Text);
                    //    e.Row.Cells[15].Text = bDate.ToString("dd/MM/yyyy");
                    //}
                    //if (e.Row.Cells[16].Text != "&nbsp;")
                    //{
                    //    DateTime bDate = Convert.ToDateTime(e.Row.Cells[16].Text);
                    //    e.Row.Cells[16].Text = bDate.ToString("dd/MM/yyyy");
                    //}
                    //if (e.Row.Cells[17].Text != "&nbsp;")
                    //{
                    //    DateTime bDate = Convert.ToDateTime(e.Row.Cells[17].Text);
                    //    e.Row.Cells[17].Text = bDate.ToString("dd/MM/yyyy");
                    //}
                    //if (e.Row.Cells[18].Text != "&nbsp;")
                    //{
                    //    DateTime bDate = Convert.ToDateTime(e.Row.Cells[18].Text);
                    //    e.Row.Cells[18].Text = bDate.ToString("dd/MM/yyyy");
                    //}
                    //if (e.Row.Cells[19].Text != "&nbsp;")
                    //{
                    //    DateTime bDate = Convert.ToDateTime(e.Row.Cells[19].Text);
                    //    e.Row.Cells[19].Text = bDate.ToString("dd/MM/yyyy");
                    //}
                    //if (e.Row.Cells[22].Text != "&nbsp;")
                    //{
                    //    DateTime bDate = Convert.ToDateTime(e.Row.Cells[22].Text);
                    //    e.Row.Cells[22].Text = bDate.ToString("dd/MM/yyyy");
                    //}
                    //if (e.Row.Cells[24].Text != "&nbsp;")
                    //{
                    //    DateTime bDate = Convert.ToDateTime(e.Row.Cells[24].Text);
                    //    e.Row.Cells[24].Text = bDate.ToString("dd/MM/yyyy");
                    //}


                    string Jobno = e.Row.Cells[1].Text;
                    string despatchDate = e.Row.Cells[19].Text;

                    string[] JNO = Jobno.Split(',');
                    foreach (string strJNO in JNO)
                    {
                        Jobno = "'%" + strJNO + "%'" + ",";

                    }

                    Jobno = Jobno.TrimEnd(',');
                    //DataSet ds = GetDesPatch(Jobno);
                    //if (ds.Tables["Outward"].Rows.Count != 0)
                    //{
                    //    DataRowView row = ds.Tables["Outward"].DefaultView[0];
                    //    string dDate = row["date"].ToString();
                    //    string[] DT = dDate.Split('/');
                    //    dDate = DT[2] + "/" + DT[1] + "/" + DT[0];
                    //    e.Row.Cells[20].Text = dDate;
                    //}



                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('" + ex.Message + "');", true);
            }
        }

        public DataSet GetDesPatch(string jobno)
        {
            SqlConnection conn = new SqlConnection(strconnDash);
            string sqlQuery = "select * from tbl_outward where jobno like(" + jobno + ")";
          
            SqlDataAdapter da = new SqlDataAdapter(sqlQuery, conn);

            DataSet ds = new DataSet();
           
            da.Fill(ds, "Outward");

            return ds;
        }


        protected void Button1_Click(object sender, EventArgs e)
        {

        }
        protected void chkIMP_CheckedChanged(object sender, EventArgs e)
        {
            if (chkIMP.Checked)
                txtPName.Enabled = true;
            else
                txtPName.Enabled = false;
        }
    }
}