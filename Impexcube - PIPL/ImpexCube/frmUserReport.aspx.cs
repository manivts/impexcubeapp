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
using MySql;
using MySql.Data.MySqlClient;
using System.Data.SqlClient;

namespace ImpexCube
{
    public partial class frmUserReport : System.Web.UI.Page
    {
        string strconn = (string)ConfigurationManager.ConnectionStrings["Constr"].ConnectionString;
      
       // string strconnVI = (string)ConfigurationManager.AppSettings["ConnectionVisual"];

        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack == false)
            {
                txtJNO.Enabled = false;
                txtPName.Enabled = false;
                //string formID = "User Report";

                //Authenticate.Forms(formID);
                //string Validate = (string)Session["DISABLE"];
                //if (Validate == "True")
                //{
                    txtFdate.Text = (string)Session["fdate"];
                    string todate = System.DateTime.Now.ToString("dd/MM/yyyy");
                    txtTdate.Text = todate;

                    DataSet dsRpt = new DataSet();
                    dsRpt.ReadXml(Server.MapPath("XML\\reports.xml"));
                    {
                        drReport.DataSource = dsRpt;
                        drReport.DataMember = "Detail";
                        drReport.DataTextField = "item";
                        drReport.DataValueField = "value";
                        drReport.DataBind();
                        drReport.Items.Insert(0, new ListItem("~select~", "0"));
                    }

                //}
                //else
                //{
                //    ScriptManager.RegisterStartupScript(this, this.GetType(), "redirect", "alert('You have not Authorized for this Page'); window.location.href='HomePage.aspx';", true);

                //}
            }
        }
        public DataSet GetData(string sqlQuery)
        {

            SqlConnection conn = new SqlConnection(strconn);
         
            SqlDataAdapter da1 = new SqlDataAdapter(sqlQuery, conn);

            DataSet ds1 = new DataSet();
         
            da1.Fill(ds1, "data");
            return ds1;
        }

        protected void BtnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                string FD = txtFdate.Text;
                string TD = txtTdate.Text;

                string sMM = FD.Substring(3, 2);
                string sDD = FD.Substring(0, 2);
                string sYY = FD.Substring(6, 4);
                string RPT = drReport.SelectedValue;
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
                string JNO = txtJNO.Text;

                string sqlQueryVal = "";

                if (tDate == "")
                    tDate = fDate;

                if (fDate == "")
                {
                    Response.Write("<script>" + "alert('Please Give  Date Values Or Job No');" + "</script>");
                  

                }
                else
                {
                    if (RPT == "CPD")
                        GetCPD(fDate, tDate, JNO, pName);
                    else if (RPT == "FFB")
                        GetFFB(fDate, tDate, JNO, pName);
                    else if (RPT == "BSR")
                        GetBSR(fDate, tDate, JNO, pName);
                    else if (RPT == "TIP")
                        GetTIP(fDate, tDate, JNO, pName);
                    else if (RPT == "DSR")
                        GetDSR(fDate, tDate, JNO, pName);
                    else if (RPT == "TDR")
                        GetTIPDelivery(fDate, tDate, JNO, pName);
                }
                sqlQueryVal = (string)Session["QUERY"];
                if (sqlQueryVal != "")
                {
                    try
                    {
                        Grdiworkreg.DataSource = GetData(sqlQueryVal);
                        Grdiworkreg.DataBind();
                      
                        Grdiworkreg.Visible = true;
                    }
                    catch (Exception ex)
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('" + ex.Message + "');", true);
                    }

                }



                if (Grdiworkreg.PageCount == 0)
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Records are not found for the given values');", true);
             
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

        protected void GetCPD(string fDate, string tDate, string JNO, string pName)
        {
            string sqlQuery = "";
            string sqlQueryVal = "";
            string sts = "Billing";
            if (chkIMP.Checked == true && chkJNO.Checked == true)
                sqlQuery = "js.CP_DATE >='" + fDate + "' and js.CP_DATE <= '" + tDate + "' and js.job_no='" + JNO + "' and pm.party_name='" + pName + "'";
            else if (chkIMP.Checked == true && chkJNO.Checked == false)
                sqlQuery = "js.CP_DATE >='" + fDate + "' and js.CP_DATE <= '" + tDate + "' and pm.party_name='" + pName + "'";
            else if (chkIMP.Checked == false && chkJNO.Checked == true)
                sqlQuery = "js.CP_DATE >='" + fDate + "' and js.CP_DATE <= '" + tDate + "' and js.job_no='" + JNO + "'";
            else
                sqlQuery = "js.CP_DATE >='" + fDate + "' and js.CP_DATE <= '" + tDate + "'";


            sqlQueryVal = "SELECT distinct i.jobsno as JOBNO,pm.party_name AS IMPORTER,i.inv_dtl AS DESCRIPTION,js.no_cntr AS NO_OF_CNTR,js.cfs AS CFS,js.STAFF AS STAFF," +
                        "js.EXAM_DATE AS EXAMINATION,js.OOC_Date AS OOC,js.DELIVERY_DATE AS DELIVERY,js.CP_REMARKS AS REMARKS " +
                        "FROM iworkreg_jobstatus js,iworkreg i,prt_mast pm " +
                        "where js.job_no=i.job_no " +
                        "and i.party_code=pm.party_code " +
                        "and " + sqlQuery + " and js.comp_jobstage !='" + sts + "'  order by js.job_no";
            Session["QUERY"] = sqlQueryVal;



        }
        protected void GetFFB(string fDate, string tDate, string JNO, string pName)
        {
            string sqlQuery = "";
            string sqlQueryVal = "";
            string sts = "Billing";
            if (chkIMP.Checked == true && chkJNO.Checked == true)
                sqlQuery = "js.FTA_Date >='" + fDate + "' and js.FTA_Date <= '" + tDate + "' and js.job_no='" + JNO + "' and pm.party_name='" + pName + "'";
            else if (chkIMP.Checked == true && chkJNO.Checked == false)
                sqlQuery = "js.FTA_Date >='" + fDate + "' and js.FTA_Date <= '" + tDate + "' and pm.party_name='" + pName + "'";
            else if (chkIMP.Checked == false && chkJNO.Checked == true)
                sqlQuery = "js.FTA_Date >='" + fDate + "' and js.FTA_Date <= '" + tDate + "' and js.job_no='" + JNO + "'";
            else
                sqlQuery = "js.FTA_Date >='" + fDate + "' and js.FTA_Date <= '" + tDate + "'";

            sqlQueryVal = "SELECT distinct i.jobsno as JOBNO,pm.party_name AS IMPORTER,i.inv_dtl AS DESCRIPTION,js.no_cntr AS NO_OF_CNTR,js.cfs AS CFS,js.STAFF AS STAFF," +
                        "js.EXAM_DATE AS EXAMINATION,js.OOC_Date AS OOC,js.DELIVERY_DATE AS DELIVERY,js.BF_REMARKS AS REMARKS " +
                        "FROM iworkreg_jobstatus js,iworkreg i,prt_mast pm " +
                        "where js.job_no=i.job_no " +
                        "and i.party_code=pm.party_code " +
                        "and " + sqlQuery + " and js.comp_jobstage !='" + sts + "'  order by js.job_no";
            Session["QUERY"] = sqlQueryVal;

        }
        protected void GetBSR(string fDate, string tDate, string JNO, string pName)
        {
            string sqlQuery = "";
            string sqlQueryVal = "";
          
            if (chkIMP.Checked == true && chkJNO.Checked == true)
                sqlQuery = "js.FTA_Date >='" + fDate + "' and js.FTA_Date <= '" + tDate + "' and js.job_no='" + JNO + "' and pm.party_name='" + pName + "'";
            else if (chkIMP.Checked == true && chkJNO.Checked == false)
                sqlQuery = "js.FTA_Date >='" + fDate + "' and js.FTA_Date <= '" + tDate + "' and pm.party_name='" + pName + "'";
            else if (chkIMP.Checked == false && chkJNO.Checked == true)
                sqlQuery = "js.FTA_Date >='" + fDate + "' and js.FTA_Date <= '" + tDate + "' and js.job_no='" + JNO + "'";
            else
                sqlQuery = "js.FTA_Date >='" + fDate + "' and js.FTA_Date <= '" + tDate + "'";

            sqlQueryVal = "SELECT distinct i.jobsno as JOBNO,pm.party_name AS IMPORTER,i.inv_dtl AS DESCRIPTION,js.no_cntr AS NO_OF_CNTR,js.cfs AS CFS,js.STAFF AS STAFF," +
                        "js.EXAM_DATE AS EXAMINATION,js.OOC_Date AS OOC,js.DELIVERY_DATE AS DELIVERY,js.BF_REMARKS AS REMARKS,j.BILL_NO as INVOICE_NO,j.BILL_DATE AS INV_DATE,j.DB_NOTE_NO AS DB_NO,j.DB_DATE AS DB_DATE " +
                        "FROM iworkreg_jobstatus js,iworkreg i,prt_mast pm,ijob_pos j " +
                        "where js.job_no=i.job_no " +
                        "and i.job_no=j.job_no " +
                        "and i.party_code=pm.party_code " +
                        "and " + sqlQuery + "  order by js.job_no";
            Session["QUERY"] = sqlQueryVal;

        }
        protected void GetTIP(string fDate, string tDate, string JNO, string pName)
        {
            string sqlQuery = "";
            string sqlQueryVal = "";
            string sts = "Billing";
            if (chkIMP.Checked == true && chkJNO.Checked == true)
                sqlQuery = "js.truck_date >='" + fDate + "' and js.truck_date <= '" + tDate + "' and js.job_no='" + JNO + "' and pm.party_name='" + pName + "'";
            else if (chkIMP.Checked == true && chkJNO.Checked == false)
                sqlQuery = "js.truck_date >='" + fDate + "' and js.truck_date <= '" + tDate + "' and pm.party_name='" + pName + "'";
            else if (chkIMP.Checked == false && chkJNO.Checked == true)
                sqlQuery = "js.truck_date >='" + fDate + "' and js.truck_date <= '" + tDate + "' and js.job_no='" + JNO + "'";
            else
                sqlQuery = "js.truck_date >='" + fDate + "' and js.truck_date <= '" + tDate + "'";


            sqlQueryVal = "SELECT distinct i.jobsno as JOBNO,pm.party_name AS IMPORTER,js.no_cntr AS NO_OF_CNTR,concat(s.GROSS_WT,s.GROSS_UNIT) AS WEIGHT,js.CBM,js.cfs AS CFS,js.STAFF AS STAFF," +
                        "js.EXAM_DATE AS EXAMINATION,js.OOC_Date AS OOC,js.DELIVERY_DATE AS DELIVERY,js.delivery_address AS ADDRESS,js.transporter as Transporter ,js.DO_VALIDITY " +
                        "FROM iworkreg_jobstatus js,iworkreg i,prt_mast pm,ishp_dtl s " +
                        "where js.job_no=i.job_no and s.job_no=i.job_no " +
                        "and i.party_code=pm.party_code " +
                        "and " + sqlQuery + " and js.comp_jobstage !='" + sts + "'  order by js.job_no";
            Session["QUERY"] = sqlQueryVal;

        }
        protected void GetTIPDelivery(string fDate, string tDate, string JNO, string pName)
        {
            string sqlQuery = "";
            string sqlQueryVal = "";
            string sts = "Billing";
            if (chkIMP.Checked == true && chkJNO.Checked == true)
                sqlQuery = "js.truck_date >='" + fDate + "' and js.truck_date <= '" + tDate + "' and js.job_no='" + JNO + "' and pm.party_name='" + pName + "'";
            else if (chkIMP.Checked == true && chkJNO.Checked == false)
                sqlQuery = "js.truck_date >='" + fDate + "' and js.truck_date <= '" + tDate + "' and pm.party_name='" + pName + "'";
            else if (chkIMP.Checked == false && chkJNO.Checked == true)
                sqlQuery = "js.truck_date >='" + fDate + "' and js.truck_date <= '" + tDate + "' and js.job_no='" + JNO + "'";
            else
                sqlQuery = "js.truck_date >='" + fDate + "' and js.truck_date <= '" + tDate + "'";


            sqlQueryVal = "SELECT distinct i.jobsno as JOBNO,pm.party_name AS IMPORTER,js.TI_no_Cntr AS NO_OF_CNTR,js.TI_cntr_nos as CONTAINERS,js.TI_GrWt AS WEIGHT,js.ti_CBM as CBM," +
                        "js.TI_truck_no AS TRUCKNO,js.TI_lr_no AS LRNO,js.TI_Date_OF_Delivery AS DELIVERY,js.TI_Delivery_Time AS TIME,js.TI_empty_contr_DAte as EMPTY_DATE  " +
                        "FROM iworkreg_jobstatus js,iworkreg i,prt_mast pm " +
                        "where js.job_no=i.job_no  " +
                        "and i.party_code=pm.party_code " +
                        "and " + sqlQuery + " and js.comp_jobstage !='" + sts + "'  order by js.job_no";
            Session["QUERY"] = sqlQueryVal;

        }
        protected void GetDSR(string fDate, string tDate, string JNO, string pName)
        {
            string sqlQuery = "";
            string sqlQueryVal = "";
            string sts = "Billing";
            if (chkIMP.Checked == true && chkJNO.Checked == true)
                sqlQuery = "JobReceivedDate >='" + fDate + "' and JobReceivedDate <= '" + tDate + "' and jobno='" + JNO + "' and Importer='" + pName + "'";
            else if (chkIMP.Checked == true && chkJNO.Checked == false)
                sqlQuery = "JobReceivedDate >='" + fDate + "' and JobReceivedDate <= '" + tDate + "' and Importer='" + pName + "'";
            else if (chkIMP.Checked == false && chkJNO.Checked == true)
                sqlQuery = "JobReceivedDate >='" + fDate + "' and JobReceivedDate <= '" + tDate + "' and jobno='" + JNO + "'";
            else
                sqlQuery = "JobReceivedDate >='" + fDate + "' and JobReceivedDate <= '" + tDate + "'";


       


            string condition = string.Empty;
            string condition1 = string.Empty;
            string condition2 = " And IsModified = 1 and  status_job ='N'";
            string condition3 = " And IsModified is null and  status_job ='N'";
            condition = sqlQuery + condition2;
            condition1 = sqlQuery + condition3;




            sqlQueryVal = "SELECT distinct jobno as JOBNO,Mode as Mode,convert(varchar(11),ETA,103) as ETA,BETYPE,Importer AS IMPORTER,BEHeading AS BE_HEADING," +
                     "JobStage as Stage,JobStatus as Status,Remarks as Remarks  " +
                     "FROM View_ImportJobStatusUpdate  " +
                     "where  " +
                     " " + condition + " Or 1=1 and " + condition1 + " and " + sqlQuery + " and  1=1  ";



            Session["QUERY"] = sqlQueryVal;

        }
        public DataSet GetSP(DateTime fDate, DateTime tDate)
        {
            MySqlConnection conn = new MySqlConnection(strconn);
            conn.Open();
            MySqlCommand cmd = new MySqlCommand("spJOBStatusVAL", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("fdate", MySqlDbType.Datetime).Value = fDate;
            cmd.Parameters.Add("tdate", MySqlDbType.Datetime).Value = tDate;
            MySqlDataAdapter da = new MySqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            da.Fill(ds, "spJOBStatusVAL");
          
            conn.Close();
            return ds;
        }
        protected void ExportPage_Click(object sender, EventArgs e)
        {
            try
            {
                string sysDates = "DSR "+DateTime.Now.ToString("dd/MM/yyyy");
                string FileName = drReport.SelectedValue + sysDates;
                string strFileName = FileName + ".xls";
              //  BtnSubmit_Click(sender, e);

                ExportToExcel(strFileName);
                
                //strFileName = ExportToExcel(sysDates);
                // GridViewExportUtil.ExportExcell(strFileName, Grdiworkreg);
                //SqlConnection conn = new SqlConnection(strconn);
                //SqlDataAdapter da1 = new SqlDataAdapter((string)Session["QUERY"], conn);
                //DataSet ds = new DataSet();
                //da1.Fill(ds, "Export");
                //DataTable dt = ds.Tables["Export"].Copy();
               // ExportToExcel(Grdiworkreg);
          
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('" + ex.Message + "');", true);
            }
        }

        public override void VerifyRenderingInServerForm(Control control)
        {
            return;
        }
        private string ExportToExcel(string strFileName)
        {
            strFileName = "StatusHistory" + ".xls";
            try
            {
                if ((Grdiworkreg.Rows.Count != 0) || (Grdiworkreg.Rows.Count != 0))
                {
                    string na = "r.xls";
                    string ExcelExport = na;
                    Response.Clear();
                    Response.Buffer = true;
                    Response.AddHeader("content-disposition", "attachment;filename= " + strFileName + "");
                    Response.Charset = "";
                    Response.ContentType = "application/vnd.ms-excel";
                    System.IO.StringWriter stringWrite = new System.IO.StringWriter();
                    System.Web.UI.HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);
                    pnlGrid.RenderControl(htmlWrite);
                    string style = @"<style> TABLE { border: thin solid black; } TD { border: thin solid black; } </style> ";
                    Response.Write(style);
                    Response.Output.Write(stringWrite.ToString());
                    Response.Flush();
                    Response.End();
                }
                else
                {
                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "Key", "alert('No Records Found EXCEL Report Cannot be generated!.');", true);
                }
            }
            catch (Exception)
            {

            }
            return strFileName;
        }
        protected void GetCommand(string Query, string connSTR)
        {

            MySqlConnection conn = new MySqlConnection(connSTR);
            conn.Open();
            MySqlCommand cmd = new MySqlCommand(Query, conn);
            cmd.CommandText = Query;
            cmd.Connection = conn;
            int res = cmd.ExecuteNonQuery();
            conn.Close();
        }
        protected void GetFormLoad()
        {
            string sqlQuery = "select i.job_no,i.doc_recd,i.jobsno,i.INV_DTL,i.cont_orig,pm.party_name,ishp.carrier,ishp.mawb_no,ishp.mawb_date,ishp.PKG_UNIT,ishp.NO_OF_PKG,ishp.eta,ishp.GROSS_UNIT,ishp.GROSS_WT,ijp.be_no,ijp.be_date,ijs.pend_remark " +
                                "from iworkreg i,iworkreg_jobstatus ijs,ishp_dtl ishp,prt_mast pm,ijob_pos ijp " +
                                "where i.job_no=ijs.job_no and i.job_no=ishp.job_no and i.job_no=ijp.job_no and i.party_code=pm.party_code " +
                                "order by i.job_no";
            Grdiworkreg.DataSource = GetData(sqlQuery);
            Grdiworkreg.DataBind();
        }
        protected void Grdiworkreg_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            //if (e.Row.RowIndex == -1 && e.Row.RowType == DataControlRowType.Header)
            //{
                
            //    GridViewRow gvRow = new GridViewRow(-1, 0, DataControlRowType.DataRow, DataControlRowState.Insert);
            //    //for (int i = 0; i < e.Row.Cells.Count; i++)
            //    //{
            //        TableRow td = new TableRow();
            //        TableCell tCell = new TableCell();
            //        tCell.Text = " Legend H-Home W-WareHouse X-DeBond";
            //        gvRow.Cells.Add(tCell);
            //        Table tbl = e.Row.Parent as Table;
            //        tCell.ColumnSpan.ToString();
            //        tbl.Rows.Add(gvRow);
            //        //tCell.RowSpan.ToString();
                    
            //    //}
            //}

            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                if (e.Row.Cells[2].Text == "~Select~")
                {
                    e.Row.Visible = false;
                }

                if (e.Row.Cells[3].Text == "01/01/1900")
                {
                    e.Row.Cells[3].Text="";
                }

                int numCol = e.Row.Cells.Count;
                for (int col = 2; col < numCol; col++)
                {
                   

                }
                int numColm = e.Row.Cells.Count;
                for (int r = 2; r < numColm; r++)
                {
                    if ((Grdiworkreg.HeaderRow.Cells[r].Text.Contains("DB_NO")) && (e.Row.Cells[r].Text == "&nbsp;"))
                    {
                        
                        e.Row.Cells[r + 1].Text = "";
                    }

                }
               

            }
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
        protected void chkJNO_CheckedChanged(object sender, EventArgs e)
        {
            if (chkJNO.Checked)
                txtJNO.Enabled = true;
            else
                txtJNO.Enabled = false;
        }

   


    }
}