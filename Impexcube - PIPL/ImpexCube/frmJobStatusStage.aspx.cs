using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
//using MySql.Data.MySqlClient;
using System.Configuration;
using System.Data.SqlClient;

namespace ImpexCube
{
    public partial class frmJobStatusStage : System.Web.UI.Page
    {
        string strconn = (string)ConfigurationManager.ConnectionStrings["Constr"].ConnectionString;
    
        //string strconnVI = (string)ConfigurationManager.AppSettings["ConnectionVisual"];

        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack == false)
            {
                //string formID = "Job Stage Report";
                //Authenticate.Forms(formID);
                // string Validate = (string)Session["DISABLE"];
                // if (Validate == "True")
                // {
                     RBStage.SelectedValue = "0";

                     txtFdate.Text = (string)Session["fdate"];
                     string todate = System.DateTime.Now.ToString("dd/MM/yyyy");
                     txtTdate.Text = todate;


                     string strQuery = "select distinct importer,importercode from T_Importer order by Importer";
                     drCustomer.DataSource = GetData(strQuery);
                     drCustomer.DataTextField = "Importer";
                     drCustomer.DataValueField = "Importer";
                     drCustomer.DataBind();
                     drCustomer.Items.Insert(0, new ListItem("~select~", "0"));

                     string strQueryJS = "select distinct StageName from M_Stage ";

                     drPenStage.DataSource = GetData(strQueryJS);
                     drPenStage.DataTextField = "StageName";
                     drPenStage.DataValueField = "StageName";
                     drPenStage.DataBind();
                     drPenStage.Items.Insert(0, new ListItem("~select~", "0"));
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
           
            SqlDataAdapter da = new SqlDataAdapter(sqlQuery, conn);

            DataSet ds = new DataSet();
        
            da.Fill(ds, "iworkreg");

            return ds;
        }

        //public DataSet GetPartyName(string sqlQuery)
        //{

        //    MySqlConnection conn = new MySqlConnection(strconnVI);
           
        //    MySqlDataAdapter da = new MySqlDataAdapter(sqlQuery, conn);

        //    DataSet ds = new DataSet();
          
        //    da.Fill(ds, "iworkreg");

        //    return ds;
        //}


        protected void BtnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                string FD = txtFdate.Text;
                string TD = txtTdate.Text;

                string sMM = FD.Substring(3, 2);
                string sDD = FD.Substring(0, 2);
                string sYY = FD.Substring(6, 4);
                FD = sMM + "/" + sDD + "/" + sYY;


                string eMM = TD.Substring(3, 2);
                string eDD = TD.Substring(0, 2);
                string eYY = TD.Substring(6, 4);
                TD = eMM + "/" + eDD + "/" + eYY;

                DateTime fd = Convert.ToDateTime(FD);
                DateTime td = Convert.ToDateTime(TD);

                //string fDate = fd.ToString("yyyy-MM-dd");
                //string tDate = td.ToString("yyyy-MM-dd");

                string fDate = fd.ToString("MM/dd/yyyy");
                string tDate = td.ToString("MM/dd/yyyy");

                string pendPName = drCustomer.SelectedValue;
                string pendJStage = drPenStage.SelectedValue;
                string pendDesc = drPenStage.SelectedItem.Text;
                string compPName = drCustomer.SelectedValue;
                string compJStage = drPenStage.SelectedValue;
                string compDesc = drPenStage.SelectedItem.Text;

                string FYear = Session["FinancialYear"].ToString();
                string sqlQuery = "";
                string sqlQueryVal = "";





                if (tDate == "")
                    tDate = fDate;

                if (fDate == "")
                {
                    Response.Write("<script>" + "alert('Please Give  Date Values Or Job No');" + "</script>");


                }
                else if (fDate != "")
                {

                    if (RBStage.SelectedValue == "0")
                    {
                        if (pendPName == "0" && pendJStage == "0")

                            sqlQuery = "(((View_ImportJobStatusUpdate.JobReceivedDate)>='" + fDate + "') and ((View_ImportJobStatusUpdate.JobReceivedDate)<='" + tDate + "') and (View_ImportJobStatusUpdate.status_job ='N')) ";

                        else if (pendPName != "0" && pendJStage == "0")
                            sqlQuery = "(((View_ImportJobStatusUpdate.JobReceivedDate)>='" + fDate + "') and ((View_ImportJobStatusUpdate.JobReceivedDate)<='" + tDate + "') and (View_ImportJobStatusUpdate.Importer='" + pendPName + "') and (View_ImportJobStatusUpdate.status_job ='N')) ";


                        else if (pendPName == "0" && pendJStage != "0")
                            sqlQuery = "(((View_ImportJobStatusUpdate.JobReceivedDate)>='" + fDate + "') and ((View_ImportJobStatusUpdate.JobReceivedDate)<='" + tDate + "') and (View_ImportJobStatusUpdate.JobStage='" + pendDesc + "') and (View_ImportJobStatusUpdate.status_job ='N')) ";


                        else
                            sqlQuery = "(((View_ImportJobStatusUpdate.JobReceivedDate)>='" + fDate + "') and ((View_ImportJobStatusUpdate.JobReceivedDate)<='" + tDate + "') and (View_ImportJobStatusUpdate.JobStage='" + pendDesc + "') and (View_ImportJobStatusUpdate.Importer='" + pendPName + "')) ";



                    }
                    else if (RBStage.SelectedValue == "1")
                    {
                        if (compPName == "0" && compJStage == "0")
                            sqlQuery = "(((View_ImportJobStatusUpdate.JobReceivedDate)>='" + fDate + "') and ((View_ImportJobStatusUpdate.JobReceivedDate)<='" + tDate + "')) and ((View_ImportJobStatusUpdate.status_job)='Y')  ";

                        else if (compPName != "0" && compJStage == "0")
                            sqlQuery = "(((View_ImportJobStatusUpdate.JobReceivedDate)>='" + fDate + "') and ((View_ImportJobStatusUpdate.JobReceivedDate)<='" + tDate + "') and (View_ImportJobStatusUpdate.Importer='" + compPName + "') and (View_ImportJobStatusUpdate.status_job ='Y')) ";


                        else if (compPName == "0" && compJStage != "0")
                            sqlQuery = "(((View_ImportJobStatusUpdate.JobReceivedDate)>='" + fDate + "') and ((View_ImportJobStatusUpdate.JobReceivedDate)<='" + tDate + "') and (View_ImportJobStatusUpdate.JobStage='" + compDesc + "') and (View_ImportJobStatusUpdate.status_job ='Y')) ";


                        else
                            sqlQuery = "(((View_ImportJobStatusUpdate.JobReceivedDate)>='" + fDate + "') and ((View_ImportJobStatusUpdate.JobReceivedDate)<='" + tDate + "') and (View_ImportJobStatusUpdate.JobStage='" + compDesc + "') and (View_ImportJobStatusUpdate.Importer='" + compPName + "')) ";

                    }
                    else
                        sqlQuery = "(((View_ImportJobStatusUpdate.JobReceivedDate)>='" + fDate + "') and ((View_ImportJobStatusUpdate.JobReceivedDate)<='" + tDate + "')) ";

                }

                string condition = string.Empty;
                string condition1 = string.Empty;
                string condition2 = " And IsModified = 1";
                string condition3 = " And IsModified is null";
                condition = sqlQuery + condition2;
                condition1 = sqlQuery + condition3;
                string Mode = string.Empty;
                if (ddlMode.SelectedValue == "~Select~")
                {

                }
                else if (ddlMode.SelectedValue == "Air")
                {
                    Mode = "and Mode='Air'";
                }
                else if (ddlMode.SelectedValue == "Sea")
                {
                    Mode = "and Mode='Sea'";
                }
                else
                {
                    Mode = "and Mode like '%%'";
                }
                if (sqlQuery != "")
                {
                    sqlQueryVal = " SELECT distinct jobno,convert(varchar(11),cast(JobReceivedDate as datetime),113) as JobReceivedDate,Mode, JobStage,JobStatus," +
                                        "StatusDate,Remarks,JobStage, Remarks," +
                                        "ImporterCode, InvoiceDetail, ShipmentCountry, convert(varchar(11),CAST(ETA AS datetime), 113) as ETA," +
                                        "MasterBLNo, convert(varchar(11),CAST(MasterBLDate AS datetime), 113) as MasterBLDate, NoOfPackages, PackagesUnit," +
                                        "GrossWeight, GrossWeightUnit, ShippingLine," +
                                        "BENO,convert(varchar(11),CAST( BEDATE AS datetime), 113) as BEDATE, Importer,BEHeading " +
                                        "FROM View_ImportJobStatusUpdate " +
                                        "WHERE 1=1 and  " + condition + " Or 1=1 and " + condition1 + "  " +
                                        "ORDER BY jobno";

                    Grdiworkreg.DataSource = GetData(sqlQueryVal);
                    Grdiworkreg.DataBind();
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
                Response.Write(ex.Message);
            }
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
            string sysDates = DateTime.Now.ToString("dd-MMM-yyyy");
            string FileName = "ReportStageWise" + sysDates;
            string strFileName = FileName + ".xls";
            BtnSubmit_Click(sender, e);
            GridViewExportUtil.ExportExcell(strFileName, Grdiworkreg);            
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
        //    string sqlQuery = "select i.job_no,i.doc_recd,i.jobsno,i.INV_DTL,i.cont_orig,pm.party_name,ishp.carrier,ishp.mawb_no,ishp.mawb_date,ishp.PKG_UNIT,ishp.NO_OF_PKG,ishp.eta,ishp.GROSS_UNIT,ishp.GROSS_WT,ijp.be_no,ijp.be_date,ijs.pend_remark " +
        //                        "from iworkreg i,View_JobStageWiseDetails ijs,ishp_dtl ishp,prt_mast pm,ijob_pos ijp " +
        //                        "where i.job_no=ijs.job_no and i.job_no=ishp.job_no and i.job_no=ijp.job_no and i.party_code=pm.party_code " +
        //                        "order by i.job_no";
        //    Grdiworkreg.DataSource = GetData(sqlQuery);
        //    Grdiworkreg.DataBind();
        //}

        protected void Grdiworkreg_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if (ddlMode.SelectedValue == "Air")
                {
                    if ((e.Row.Cells[3].Text == "Sea") || (e.Row.Cells[3].Text == "Both"))
                    {
                        e.Row.Visible = false;
                    }
                }
                else if (ddlMode.SelectedValue == "Sea")
                {
                    if ((e.Row.Cells[3].Text == "Air") || (e.Row.Cells[3].Text == "Both"))
                    {
                        e.Row.Visible = false;
                    }
                }
                else if (ddlMode.SelectedValue == "Both")
                {
                    e.Row.Visible = true;
                }

                if (e.Row.Cells[7].Text == "01 Jan 1900")
                {

                    e.Row.Cells[7].Text = "";
                }

                if (e.Row.Cells[12].Text == "01 Jan 1900")
                {

                    e.Row.Cells[12].Text = "";
                }
                if (e.Row.Cells[14].Text == "01 Jan 1900")
                {

                    e.Row.Cells[14].Text = "";
                }

            }
            //if (e.Row.RowType == DataControlRowType.Header)
            //{
            //    if (RBStage.SelectedValue == "0")
            //    {
            //    }
            //    else if (RBStage.SelectedValue == "1")
            //    {
            //    }
            //}
            //if (e.Row.RowType == DataControlRowType.DataRow)
            //{
            //    if (RBStage.SelectedValue == "0")
            //    {
            //    }
            //    else if (RBStage.SelectedValue == "1")
            //    {
            //    }
            //}
        }

    }
}