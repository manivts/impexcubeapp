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
using System.IO;

namespace ImpexCube
{
    public partial class frmGDIImportDSR : System.Web.UI.Page
    {
        string strconn = (string)ConfigurationManager.ConnectionStrings["Constr"].ConnectionString;
       

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

                string fDate = fd.ToString("yyyy/MM/dd");
                string tDate = td.ToString("yyyy/MM/dd");

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
                    if (RPT == "SeaGIT")
                        GetSeaGIT(fDate, tDate, JNO, pName);
                    else if (RPT == "SeaCleared")
                        GetSeaCleared(fDate, tDate, JNO, pName);
                    else if (RPT == "AirGIT")
                        GetAirGIT(fDate, tDate, JNO, pName);
                    else if (RPT == "AirCleared")
                        GetAirCleared(fDate, tDate, JNO, pName);

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

        protected void GetSeaGIT(string fDate, string tDate, string JNO, string pName)
        {
            string sqlQuery = "";
            string sqlQueryVal = "";
            string DutyRequest = "DutyIntimation";
            string DutyPaid = "DutyPaid";
            string month = DateTime.Now.Month.ToString();

            if (chkIMP.Checked == true && chkJNO.Checked == true)
                sqlQuery = "JobReceivedDate >='" + fDate + "' and JobReceivedDate <= '" + tDate + "' and jobno='" + JNO + "' and importer='" + pName + "'";
            else if (chkIMP.Checked == true && chkJNO.Checked == false)
                sqlQuery = "JobReceivedDate >='" + fDate + "' and JobReceivedDate <= '" + tDate + "' and importer='" + pName + "'";
            else if (chkIMP.Checked == false && chkJNO.Checked == true)
                sqlQuery = "JobReceivedDate >='" + fDate + "' and JobReceivedDate <= '" + tDate + "' and jobno='" + JNO + "'";
            else
                sqlQuery = "JobReceivedDate >='" + fDate + "' and JobReceivedDate <= '" + tDate + "'";



            //sqlQueryVal = " Select * from  (SELECT distinct job_no ,convert(varchar(11),cast(doc_received_date as datetime),113) as doc_received_date  ,  " +
            //            " cont_orig  ,carrier ,toi ,no_of_pkg ,gross_wt , gross_unit, " +
            //            "  mawb_no,hawb_no ," +
            //            " convert(varchar(11),CAST(hawb_date AS datetime), 113) as  hawb_date,CONVERT(VARCHAR(11), CAST(eta AS DATETIME), 113) as eta  ,be_no , " +
            //            " CONVERT(VARCHAR(11), CAST(be_date AS DATETIME), 113) as be_date ,tot_ass_vl ,Remarks,ID ,CONVERT(VARCHAR(11), CAST(StatusDate AS DATETIME), 113) as StatusDate,Gateway_IGMNo, " +
            //            " tot_duty,PortOfShipment,CONVERT(VARCHAR(11), CAST(GLD AS DATETIME), 113) as GLD,VESSEL_NAME,CONVERT(VARCHAR(11), CAST(mawb_date AS DATETIME), 113) as  mawb_date,month(convert(datetime,eta,103)) as ETAMonth,datepart(week,(convert(datetime,eta,103))) as JobWeek,currentStatus,cleared  " +
            //            " FROM View_GKNReport " +
            //              "where  " +
            //            " " + sqlQuery + " and status_job ='N' and  transport_mode='S' and cleared is null ) " +
            //            " as s pivot(Max(statusdate) for ID in ([16],[17],[21],[26],[46])) as pivottable ";

            sqlQueryVal = " Select * from  (SELECT distinct jobno ,convert(varchar(11),cast(JobReceivedDate as datetime),113) as JobReceivedDate  ,  " +
                       " CountryOrigin  ,ShippingLine ,InvoiceTerms ,NoOfPackages ,GrossWeight , GrossWeightUnit, " +
                       "  MasterBLNo,HouseBLNo ," +
                       " convert(varchar(11),CAST(HouseBLDate AS datetime), 113) as  HouseBLDate,CONVERT(VARCHAR(11), CAST(eta AS DATETIME), 113) as eta  ,BENo , " +
                       " CONVERT(VARCHAR(11), CAST(BEDate AS DATETIME), 113) as BEDate ,TotalAssVal ,Remarks,ID ,CONVERT(VARCHAR(11), CAST(StatusDate AS DATETIME), 113) as StatusDate,GatewayIGMNo, " +
                       " TotalDuty,PortOfShipment,CONVERT(VARCHAR(11), CAST(GLDInwardDate AS DATETIME), 113) as GLDInwardDate,VesselName,CONVERT(VARCHAR(11), CAST(MasterBLDate AS DATETIME), 113) as  MasterBLDate, " +
                       " month(convert(datetime,eta,103)) as ETAMonth,datepart(week,(convert(datetime,eta,103))) as JobWeek,currentStatus,cleared,VoyageNo,NetWeight  " +
                       " FROM View_GKNReportMax " +
                         "where  " +
                       " " + sqlQuery + " and status_job ='N' and  mode='Sea' and cleared is null ) " +
                       " as s pivot(Max(statusdate) for ID in ([16],[17],[21],[26],[46])) as pivottable ";


            Session["QUERY"] = sqlQueryVal;



        }
        protected void GetSeaCleared(string fDate, string tDate, string JNO, string pName)
        {
            string sqlQuery = "";
            string sqlQueryVal = "";
            string DutyRequest = "DutyIntimation";
            string DutyPaid = "DutyPaid";
            string month = DateTime.Now.Month.ToString();


            if (chkIMP.Checked == true && chkJNO.Checked == true)
                sqlQuery = "StatusDate >='" + fDate + "' and StatusDate <= '" + tDate + "' and jobno='" + JNO + "' and importer='" + pName + "'";
            else if (chkIMP.Checked == true && chkJNO.Checked == false)
                sqlQuery = "StatusDate >='" + fDate + "' and StatusDate <= '" + tDate + "' and importer='" + pName + "'";
            else if (chkIMP.Checked == false && chkJNO.Checked == true)
                sqlQuery = "StatusDate >='" + fDate + "' and StatusDate <= '" + tDate + "' and jobno='" + JNO + "'";
            else
                sqlQuery = "StatusDate >='" + fDate + "' and StatusDate <= '" + tDate + "'";


            //sqlQueryVal = " Select * from  (SELECT distinct job_no ,convert(varchar(11),cast(doc_received_date as datetime),113) as doc_received_date  ,  " +
            //         " cont_orig  ,carrier ,toi ,no_of_pkg ,gross_wt , gross_unit, " +
            //         "  mawb_no,hawb_no ," +
            //         " convert(varchar(11),CAST(hawb_date AS datetime), 113) as  hawb_date,CONVERT(VARCHAR(11), CAST(eta AS DATETIME), 113) as eta  ,be_no , " +
            //         " CONVERT(VARCHAR(11), CAST(be_date AS DATETIME), 113) as be_date ,tot_ass_vl ,Remarks,ID ,CONVERT(VARCHAR(11), CAST(StatusDate AS DATETIME), 113) as StatusDate,Gateway_IGMNo, " +
            //         " tot_duty,PortOfShipment,CONVERT(VARCHAR(11), CAST(GLD AS DATETIME), 113) as GLD,VESSEL_NAME,CONVERT(VARCHAR(11), CAST(mawb_date AS DATETIME), 113) as  mawb_date,month(convert(datetime,eta,103)) as ETAMonth,datepart(week,(convert(datetime,eta,103))) as JobWeek,currentStatus,cleared  " +
            //         " FROM View_GKNReport " +
            //           "where  " +
            //         " " + sqlQuery + " and  transport_mode='S' and  cleared='Delivered' ) " +
            //         " as s pivot(Max(statusdate) for ID in ([16],[17],[21],[26],[46])) as pivottable ";

            sqlQueryVal = " Select * from  (SELECT distinct jobno ,convert(varchar(11),cast(JobReceivedDate as datetime),113) as JobReceivedDate  ,  " +
                     " CountryOrigin  ,ShippingLine ,InvoiceTerms ,NoOfPackages ,GrossWeight , GrossWeightUnit, " +
                     "  MasterBLNo,HouseBLNo ," +
                     " convert(varchar(11),CAST(HouseBLDate AS datetime), 113) as  HouseBLDate,CONVERT(VARCHAR(11), CAST(eta AS DATETIME), 113) as eta  ,BENo , " +
                     " CONVERT(VARCHAR(11), CAST(BEDate AS DATETIME), 113) as BEDate ,TotalAssVal ,Remarks,ID ,CONVERT(VARCHAR(11), CAST(StatusDate AS DATETIME), 113) as StatusDate,GatewayIGMNo, " +
                     " TotalDuty,PortOfShipment,CONVERT(VARCHAR(11), CAST(GLDInwardDate AS DATETIME), 113) as GLDInwardDate,VesselName,CONVERT(VARCHAR(11), CAST(MasterBLDate AS DATETIME), 113) as  MasterBLDate, " +
                     " month(convert(datetime,eta,103)) as ETAMonth,datepart(week,(convert(datetime,eta,103))) as JobWeek,currentStatus,cleared,VoyageNo,NetWeight  " +
                     " FROM View_GKNReportMax " +
                       "where  " +
                     " " + sqlQuery + " and  mode='Sea' and cleared='Delivered' ) " +
                     " as s pivot(Max(statusdate) for ID in ([16],[17],[21],[26],[46])) as pivottable ";


            Session["QUERY"] = sqlQueryVal;

        }
        protected void GetAirGIT(string fDate, string tDate, string JNO, string pName)
        {
            string sqlQuery = "";
            string sqlQueryVal = "";
            string DutyRequest = "DutyIntimation";
            string DutyPaid = "DutyPaid";
            string month = DateTime.Now.Month.ToString();

            if (chkIMP.Checked == true && chkJNO.Checked == true)
                sqlQuery = "JobReceivedDate >='" + fDate + "' and JobReceivedDate <= '" + tDate + "' and jobno='" + JNO + "' and importer='" + pName + "'";
            else if (chkIMP.Checked == true && chkJNO.Checked == false)
                sqlQuery = "JobReceivedDate >='" + fDate + "' and JobReceivedDate <= '" + tDate + "' and importer='" + pName + "'";
            else if (chkIMP.Checked == false && chkJNO.Checked == true)
                sqlQuery = "JobReceivedDate >='" + fDate + "' and JobReceivedDate <= '" + tDate + "' and jobno='" + JNO + "'";
            else
                sqlQuery = "JobReceivedDate >='" + fDate + "' and JobReceivedDate <= '" + tDate + "'";


            //sqlQueryVal = " Select * from  (SELECT distinct job_no ,convert(varchar(11),cast(doc_received_date as datetime),113) as doc_received_date  ,  " +
            //        " cont_orig  ,carrier ,toi ,no_of_pkg ,gross_wt , gross_unit, " +
            //        "  mawb_no,hawb_no ," +
            //        " convert(varchar(11),CAST(hawb_date AS datetime), 113) as  hawb_date,CONVERT(VARCHAR(11), CAST(eta AS DATETIME), 113) as eta  ,be_no , " +
            //        " CONVERT(VARCHAR(11), CAST(be_date AS DATETIME), 113) as be_date ,tot_ass_vl ,Remarks,ID ,CONVERT(VARCHAR(11), CAST(StatusDate AS DATETIME), 113) as StatusDate,Gateway_IGMNo, " +
            //        " tot_duty,PortOfShipment,CONVERT(VARCHAR(11), CAST(GLD AS DATETIME), 113) as GLD,VESSEL_NAME,CONVERT(VARCHAR(11), CAST(mawb_date AS DATETIME), 113) as  mawb_date,month(convert(datetime,eta,103)) as ETAMonth,datepart(week,(convert(datetime,eta,103))) as JobWeek,currentStatus,cleared  " +
            //        " FROM View_GKNReport " +
            //          "where  " +
            //        " " + sqlQuery + " and   status_job ='N' and transport_mode='A' and cleared is null  ) " +
            //        " as s pivot(Max(statusdate) for ID in ([16],[17],[21],[26],[46])) as pivottable ";

            sqlQueryVal = " Select * from  (SELECT distinct jobno ,convert(varchar(11),cast(JobReceivedDate as datetime),113) as JobReceivedDate  ,  " +
                   " CountryOrigin  ,ShippingLine ,InvoiceTerms ,NoOfPackages ,GrossWeight , GrossWeightUnit, " +
                   "  MasterBLNo,HouseBLNo ," +
                   " convert(varchar(11),CAST(HouseBLDate AS datetime), 113) as  HouseBLDate,CONVERT(VARCHAR(11), CAST(eta AS DATETIME), 113) as eta  ,BENo , " +
                   " CONVERT(VARCHAR(11), CAST(BEDate AS DATETIME), 113) as BEDate ,TotalAssVal ,Remarks,ID ,CONVERT(VARCHAR(11), CAST(StatusDate AS DATETIME), 113) as StatusDate,GatewayIGMNo, " +
                   " TotalDuty,PortOfShipment,CONVERT(VARCHAR(11), CAST(GLDInwardDate AS DATETIME), 113) as GLDInwardDate,VesselName,CONVERT(VARCHAR(11), CAST(MasterBLDate AS DATETIME), 113) as  MasterBLDate, " +
                   " month(convert(datetime,eta,103)) as ETAMonth,datepart(week,(convert(datetime,eta,103))) as JobWeek,currentStatus,cleared,VoyageNo,NetWeight  " +
                   " FROM View_GKNReportMax " +
                     "where  " +
                   " " + sqlQuery + " and status_job ='N' and  mode='Air' and cleared is null ) " +
                   " as s pivot(Max(statusdate) for ID in ([16],[17],[21],[26],[46])) as pivottable ";

            Session["QUERY"] = sqlQueryVal;

        }
        protected void GetAirCleared(string fDate, string tDate, string JNO, string pName)
        {
            string sqlQuery = "";
            string sqlQueryVal = "";
            string DutyRequest = "DutyIntimation";
            string DutyPaid = "DutyPaid";
            string month = DateTime.Now.Month.ToString();

            if (chkIMP.Checked == true && chkJNO.Checked == true)
                sqlQuery = "StatusDate >='" + fDate + "' and StatusDate <= '" + tDate + "' and jobno='" + JNO + "' and importer='" + pName + "'";
            else if (chkIMP.Checked == true && chkJNO.Checked == false)
                sqlQuery = "StatusDate >='" + fDate + "' and StatusDate <= '" + tDate + "' and importer='" + pName + "'";
            else if (chkIMP.Checked == false && chkJNO.Checked == true)
                sqlQuery = "StatusDate >='" + fDate + "' and StatusDate <= '" + tDate + "' and jobno='" + JNO + "'";
            else
                sqlQuery = "StatusDate >='" + fDate + "' and StatusDate <= '" + tDate + "'";

            //sqlQueryVal = " Select * from  (SELECT distinct job_no ,convert(varchar(11),cast(doc_received_date as datetime),113) as doc_received_date  ,  " +
                    //" cont_orig  ,carrier ,toi ,no_of_pkg ,gross_wt , gross_unit, " +
                    //"  mawb_no,hawb_no ," +
                    //" convert(varchar(11),CAST(hawb_date AS datetime), 113) as  hawb_date,CONVERT(VARCHAR(11), CAST(eta AS DATETIME), 113) as eta  ,be_no , " +
                    //" CONVERT(VARCHAR(11), CAST(be_date AS DATETIME), 113) as be_date ,tot_ass_vl ,Remarks,ID ,CONVERT(VARCHAR(11), CAST(StatusDate AS DATETIME), 113) as StatusDate,Gateway_IGMNo, " +
                    //" tot_duty,PortOfShipment,CONVERT(VARCHAR(11), CAST(GLD AS DATETIME), 113) as GLD,VESSEL_NAME,CONVERT(VARCHAR(11), CAST(mawb_date AS DATETIME), 113) as  mawb_date,month(convert(datetime,eta,103)) as ETAMonth,datepart(week,(convert(datetime,eta,103))) as JobWeek,currentStatus,cleared  " +
                    //" FROM View_GKNReport " +
                    //  "where  " +
                    //" " + sqlQuery + " and  transport_mode='A' and  cleared='Delivered' ) " +
                    //" as s pivot(Max(statusdate) for ID in ([16],[17],[21],[26],[46])) as pivottable ";

            sqlQueryVal = " Select * from  (SELECT distinct jobno ,convert(varchar(11),cast(JobReceivedDate as datetime),113) as JobReceivedDate  ,  " +
                " CountryOrigin  ,ShippingLine ,InvoiceTerms ,NoOfPackages ,GrossWeight , GrossWeightUnit, " +
                "  MasterBLNo,HouseBLNo ," +
                " convert(varchar(11),CAST(HouseBLDate AS datetime), 113) as  HouseBLDate,CONVERT(VARCHAR(11), CAST(eta AS DATETIME), 113) as eta  ,BENo , " +
                " CONVERT(VARCHAR(11), CAST(BEDate AS DATETIME), 113) as BEDate ,TotalAssVal ,Remarks,ID ,CONVERT(VARCHAR(11), CAST(StatusDate AS DATETIME), 113) as StatusDate,GatewayIGMNo, " +
                " TotalDuty,PortOfShipment,CONVERT(VARCHAR(11), CAST(GLDInwardDate AS DATETIME), 113) as GLDInwardDate,VesselName,CONVERT(VARCHAR(11), CAST(MasterBLDate AS DATETIME), 113) as  MasterBLDate, " +
                " month(convert(datetime,eta,103)) as ETAMonth,datepart(week,(convert(datetime,eta,103))) as JobWeek,currentStatus,cleared,VoyageNo,NetWeight  " +
                " FROM View_GKNReportMax " +
                  "where  " +
                " " + sqlQuery + " and  mode='Air' and cleared='Delivered' ) " +
                " as s pivot(Max(statusdate) for ID in ([16],[17],[21],[26],[46])) as pivottable ";

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
                sqlQuery = "job_date >='" + fDate + "' and job_date <= '" + tDate + "' and job_no='" + JNO + "' and party_name='" + pName + "'";
            else if (chkIMP.Checked == true && chkJNO.Checked == false)
                sqlQuery = "job_date >='" + fDate + "' and job_date <= '" + tDate + "' and party_name='" + pName + "'";
            else if (chkIMP.Checked == false && chkJNO.Checked == true)
                sqlQuery = "job_date >='" + fDate + "' and job_date <= '" + tDate + "' and job_no='" + JNO + "'";
            else
                sqlQuery = "job_date >='" + fDate + "' and job_date <= '" + tDate + "'";

            string condition = string.Empty;
            string condition1 = string.Empty;
            string condition2 = " And IsModified = 1 and  status_job ='N'";
            string condition3 = " And IsModified is null and  status_job ='N'";
            condition = sqlQuery + condition2;
            condition1 = sqlQuery + condition3;

            sqlQueryVal = "SELECT distinct jobsno as JOBNO,transport_mode as Mode,ETA,BE_TYPE,party_name AS IMPORTER,inv_dtl AS BE_HEADING," +
                     "JobStage as Stage,JobStatus as Status,Remarks as Remarks,status_job  " +
                     "FROM View_JobStageWiseDetails  " +
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
                string sysDates = DateTime.Now.ToString("dd/MM/yyyy");
                string FileName = drReport.SelectedValue + sysDates;
                string strFileName = FileName + ".xls";
                BtnSubmit_Click(sender, e);

                string attachment = "attachment; filename="+strFileName+" ";
                Response.ClearContent();
                Response.AddHeader("content-disposition", attachment);
                Response.ContentType = "application/ms-excel";

                StringWriter sw = new StringWriter();
                HtmlTextWriter htw = new HtmlTextWriter(sw);

                HtmlForm frm = new HtmlForm();
                Grdiworkreg.AllowPaging = false;

                Grdiworkreg.Parent.Controls.Add(frm);
                frm.Attributes["runat"] = "server";
                frm.Controls.Add(Grdiworkreg);
                frm.RenderControl(htw);

                Response.Write(sw.ToString());
                Response.End();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('" + ex.Message + "');", true);
            }
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
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (e.Row.Cells[4].Text == "01-Jan-1900" || e.Row.Cells[4].Text == "01 Jan 1900")
                {

                    e.Row.Cells[4].Text = "";
                }
                if (e.Row.Cells[10].Text == "01-Jan-1900" || e.Row.Cells[10].Text == "01 Jan 1900")
                {

                    e.Row.Cells[10].Text = "";
                }
                if (e.Row.Cells[25].Text == "01-Jan-1900" || e.Row.Cells[25].Text == "01 Jan 1900")
                {

                    e.Row.Cells[25].Text = "";
                }
                if (e.Row.Cells[28].Text == "01-Jan-1900" || e.Row.Cells[28].Text == "01 Jan 1900")
                {

                    e.Row.Cells[28].Text = "";
                }
                if (e.Row.Cells[29].Text == "01-Jan-1900" || e.Row.Cells[29].Text == "01 Jan 1900")
                {

                    e.Row.Cells[29].Text = "";
                }
                if (e.Row.Cells[30].Text == "01-Jan-1900" || e.Row.Cells[30].Text == "01 Jan 1900")
                {

                    e.Row.Cells[30].Text = "";
                }
                if (e.Row.Cells[31].Text == "01-Jan-1900" || e.Row.Cells[31].Text == "01 Jan 1900")
                {

                    e.Row.Cells[31].Text = "";
                }
                if (e.Row.Cells[32].Text == "01-Jan-1900" || e.Row.Cells[32].Text == "01 Jan 1900")
                {

                    e.Row.Cells[32].Text = "";
                }
                if (e.Row.Cells[33].Text == "01-Jan-1900" || e.Row.Cells[33].Text == "01 Jan 1900")
                {

                    e.Row.Cells[33].Text = "";
                }
                if (e.Row.Cells[36].Text == "01-Jan-1900" || e.Row.Cells[36].Text == "01 Jan 1900")
                {

                    e.Row.Cells[36].Text = "";
                }
                if (e.Row.Cells[39].Text == "01-Jan-1900" || e.Row.Cells[39].Text == "01 Jan 1900")
                {

                    e.Row.Cells[39].Text = "";
                }
                if (e.Row.Cells[40].Text == "01-Jan-1900" || e.Row.Cells[40].Text == "01 Jan 1900")
                {

                    e.Row.Cells[40].Text = "";
                }
                if (e.Row.Cells[41].Text == "01-Jan-1900" || e.Row.Cells[41].Text == "01 Jan 1900")
                {

                    e.Row.Cells[41].Text = "";
                }

            }

            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    string jobno = e.Row.Cells[1].Text;
                    GridView gvSupplierName = (GridView)e.Row.FindControl("gvSupplierName");
                    GridView gvContDetails = (GridView)e.Row.FindControl("gvContDetails");
                    GridView gvInvoiceDetails = (GridView)e.Row.FindControl("gvInvoiceDetails");

                    Label lblInvoiceNo = (Label)e.Row.FindControl("lblInvoiceNo");
                    Label lblInvoiceDate = (Label)e.Row.FindControl("lblInvoiceDate");
                    Label lblCurrency = (Label)e.Row.FindControl("lblCurrency");
                    Label lblInvoiceValue = (Label)e.Row.FindControl("lblInvoiceValue");
                    Label lblDesc = (Label)e.Row.FindControl("lblDesc");

                    Label lblContType = (Label)e.Row.FindControl("lblContType");
                    Label lblContNo = (Label)e.Row.FindControl("lblContNo");
                    Label lblContSize = (Label)e.Row.FindControl("lblContSize");
                    Label lblCnsrName = (Label)e.Row.FindControl("lblCnsrName");
                   
                    //string Query = "SELECT distinct INV_NO,convert(varchar(11),cast(INV_DATE as datetime),113) as INV_DATE,CURRENCY,inv_value  FROM [View_GKNReport] where job_no='" + jobno + "'";
                    //string QueryProduct = "SELECT distinct PROD_DESC  FROM [View_GKNReport] where job_no='" + jobno + "'";
                    //string QueryCont = "SELECT distinct [CONT_NO], [CONT_SIZE], [CONT_TYPE] FROM [View_GKNReport] where job_no='" + jobno + "'";
                    //string QueryCnsr = "SELECT distinct CNSR_NAME FROM [View_GKNReport] where job_no='" + jobno + "'";

                    string Query = "SELECT distinct InvoiceNo,convert(varchar(11),cast(InvoiceDate as datetime),113) as InvoiceDate,InvoiceCurrency,InvoiceProductValues  FROM View_GKNReportMax where jobno='" + jobno + "'";
                    string QueryProduct = "SELECT distinct ProductDesc FROM View_GKNReportMax where jobno='" + jobno + "'";
                    string QueryCont = "SELECT distinct ContainerNo, ContainerType, LoadType FROM View_GKNReportMax where jobno='" + jobno + "'";
                    string QueryCnsr = "SELECT distinct Consignor FROM View_GKNReportMax where jobno='" + jobno + "'";
                    
                    DataSet ds = GetQueryData(Query);
                    DataTable dt = ds.Tables[0];
                   int  i = 1;
                   string InvNo = "";
                   string InvDate = "";
                   string Curry = "";
                   string InvValue = "";
                   foreach (DataRow row in dt.Rows)
                   {
                       string InvoiceNo = row["InvoiceNo"].ToString();
                       string InvoiceDate = row["InvoiceDate"].ToString();
                       string Currency = row["InvoiceCurrency"].ToString();
                       string InvoiceValue = row["InvoiceProductValues"].ToString();
                     
                       if (InvoiceNo != "")
                       {
                           if (InvoiceNo != "")
                               InvoiceNo = InvoiceNo.Replace("'", " ");
                           string b = "<br>";
                           InvNo = InvNo + InvoiceNo + "," ;
                           lblInvoiceNo.Text = InvNo.TrimEnd(',');
                       }

                       if (InvoiceDate != "")
                       {
                           if (InvoiceDate != "")
                               InvoiceDate = InvoiceDate.Replace("'", " ");
                           string b = "<br>";
                           InvDate = InvDate + InvoiceDate + "," ;
                           lblInvoiceDate.Text = InvDate.TrimEnd(',');
                       }

                       if (Currency != "")
                       {
                           if (Currency != "")
                               Currency = Currency.Replace("'", " ");
                           string b = "<br>";
                           Curry = Curry + Currency + "," ;
                           lblCurrency.Text = Curry.TrimEnd(',');

                       }
                       if (InvoiceValue != "")
                       {
                           if (InvoiceValue != "")
                               InvoiceValue = InvoiceValue.Replace("'", " ");
                           string b = "<br>";
                           InvValue = InvValue + InvoiceValue + ",";
                           lblInvoiceValue.Text = InvValue.TrimEnd(',');

                       }
                   }

                   DataSet ds1 = GetQueryData(QueryProduct);
                   DataTable dt1 = ds1.Tables[0];
                  
                   string Prod = "";
                   
                   foreach (DataRow row1 in dt1.Rows)
                   {
                       string ProductDesc = row1["ProductDesc"].ToString();

                       if (ProductDesc != "")
                       {
                           if (ProductDesc != "")
                               ProductDesc = ProductDesc.Replace("'", " ");
                           string b = "<br>";
                           Prod = Prod + ProductDesc +",";
                           lblDesc.Text = Prod.TrimEnd(',');
                       }

                   }

                   DataSet ds2 = GetQueryData(QueryCont);
                   DataTable dt2 = ds2.Tables[0];

                   string ContType = "";
                   string contSize = "";
                   string contNo = "";

                   foreach (DataRow row2 in dt2.Rows)
                   {
                       string ContainerNo = row2["ContainerNo"].ToString();
                       string containersize = row2["ContainerType"].ToString();
                       string containertype = row2["LoadType"].ToString();

                       if (ContainerNo != "")
                       {
                           if (ContainerNo != "")
                               ContainerNo = ContainerNo.Replace("'", " ");
                           string b = "<br>";
                           contNo = contNo + ContainerNo + ",";
                           lblContNo.Text = contNo.TrimEnd(',');
                       }

                       if (containersize != "")
                       {

                           if (containersize != "")
                               containersize = containersize.Replace("'", " ");
                           string b = "<br>";
                           contSize = contSize + containersize + ",";
                           lblContSize.Text = contSize.TrimEnd(',');
                       }

                       if (containertype != "")
                       {

                           if (containertype != "")
                               containertype = containertype.Replace("'", " ");
                           string b = "<br>";
                           ContType = ContType + containertype + ",";
                           lblContType.Text = ContType.TrimEnd(',');
                       } 

                   }


                   DataSet ds3 = GetQueryData(QueryCnsr);
                   DataTable dt3 = ds3.Tables[0];

                   string cnsrna = "";
                   
                   foreach (DataRow row3 in dt3.Rows)
                   {
                       string cnsrname = row3["Consignor"].ToString();

                       if (cnsrname != "")
                       {
                           if (cnsrname != "")
                               cnsrname = cnsrname.Replace("'", " ");
                           string b = "<br>";
                           cnsrna = cnsrna + cnsrname + ",";
                           lblCnsrName.Text = cnsrna.TrimEnd(',');
                       }

                   }
                }

                string RPT = drReport.SelectedValue;
                if (RPT == "SeaGIT")
                {
                    Grdiworkreg.Columns[44].Visible = false;
                    Grdiworkreg.Columns[45].Visible = false;
                    Grdiworkreg.Columns[46].Visible = false;

                }
                else if (RPT == "SeaCleared")
                {
                    Grdiworkreg.Columns[44].Visible = true;
                    Grdiworkreg.Columns[45].Visible = true;
                    Grdiworkreg.Columns[46].Visible = true;

                }
                else if (RPT == "AirGIT")
                {
                    Grdiworkreg.Columns[21].Visible = false;
                    Grdiworkreg.Columns[22].Visible = false;
                    Grdiworkreg.Columns[23].Visible = false;

                    Grdiworkreg.Columns[44].Visible = false;
                    Grdiworkreg.Columns[45].Visible = false;
                    Grdiworkreg.Columns[46].Visible = false;
                }
                else if (RPT == "AirCleared")
                {
                    Grdiworkreg.Columns[21].Visible = false;
                    Grdiworkreg.Columns[22].Visible = false;
                    Grdiworkreg.Columns[23].Visible = false;

                    Grdiworkreg.Columns[44].Visible = true;
                    Grdiworkreg.Columns[45].Visible = true;
                    Grdiworkreg.Columns[46].Visible = true;
                }

            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('" + ex.Message + "');", true);
            }

        }

        public DataSet GetQueryData(string Query)
        {
            DataSet ds = new DataSet();
            try
            {
             
                SqlConnection con = new SqlConnection(strconn);
                con.Open();
                SqlDataAdapter sd = new SqlDataAdapter(Query, con);
                sd.Fill(ds, "Product");
                con.Close();
                
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('" + ex.Message + "');", true);
            }
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
        protected void chkJNO_CheckedChanged(object sender, EventArgs e)
        {
            if (chkJNO.Checked)
                txtJNO.Enabled = true;
            else
                txtJNO.Enabled = false;
        }



    }
}