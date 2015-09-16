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
    public partial class frmJobStatusGroupWiseReport : System.Web.UI.Page
    {

        
        string strconn = (string)ConfigurationManager.ConnectionStrings["Constr"].ConnectionString;
        string strconnJSU = (string)ConfigurationManager.AppSettings["connectionJSU"];
        string strconnVI = (string)ConfigurationManager.AppSettings["ConnectionVisual"];
        string stageGroup = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack == false)
            {
                RBStage.SelectedValue = "1";
                rbMode.SelectedValue = "S";
                string formID = "Group Wise Report";
                Authenticate.Forms(formID);
                 string Validate = (string)Session["DISABLE"];
                 if (Validate == "True")
                 {
                     txtFdate.Text = (string)Session["fdate"];
                     string todate = System.DateTime.Now.ToString("dd/MM/yyyy");
                     txtTdate.Text = todate;
                     string sqlQuery = "SELECT * FROM istagegroup where groupname !='air' ";
                     drStageGroup.DataSource = GetDataistate(sqlQuery);
                     drStageGroup.DataTextField = "groupname";
                     drStageGroup.DataValueField = "groupmembers";
                     drStageGroup.DataBind();
                     drStageGroup.Items.Insert(0, new ListItem("~select~", "0"));
                 }
                 else
                 {
                     ScriptManager.RegisterStartupScript(this, this.GetType(), "redirect", "alert('You have not Authorized for this Page'); window.location.href='HomePage.aspx';", true);

                 }

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

        public DataSet GetDataistate(string sqlQuery)
        {
            MySqlConnection conn = new MySqlConnection(strconnJSU);

            MySqlDataAdapter da = new MySqlDataAdapter(sqlQuery, conn);

            DataSet ds = new DataSet();

            da.Fill(ds, "iworkreg");

            return ds;
        }


        public DataSet GetPartyName(string sqlQuery)
        {

            MySqlConnection conn = new MySqlConnection(strconnVI);

            MySqlDataAdapter da = new MySqlDataAdapter(sqlQuery, conn);

            DataSet ds = new DataSet();

            da.Fill(ds, "iworkreg");

            return ds;
        }

        protected void BtnSubmit_Click(object sender, EventArgs e)
        {
            /* Date Conversions*/

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

            string fDate = fd.ToString("yyyy-MM-dd");
            string tDate = td.ToString("yyyy-MM-dd");
            string sMode = rbMode.SelectedValue;
            string stGroup = drStageGroup.SelectedValue;
            string stGroupID = "";
            string sqlQuery = "";
            string sqlQueryVal = "";
            string[] sg = stGroup.Split(',');
            foreach (string strThisst in sg)
            {
                stageGroup = stageGroup + "'" + strThisst + "',";
            }

            stGroupID = stageGroup.TrimEnd(',');
            if (tDate == "")
                tDate = fDate;

            if (fDate == "")
            {
                Response.Write("<script>" + "alert('Please Give  Date Values');" + "</script>");
              

            }
            else
            {
                if (sMode == "A")
                {
                    if (stGroup == "0")
                        sqlQuery = "job_date >='" + fDate + "' and job_date <= '" + tDate + "' and transport_mode='" + sMode + "' and status_job='Y' ";
                    else
                    {
                        if (RBStage.SelectedValue == "0")
                            sqlQuery = "job_date >='" + fDate + "' and job_date <= '" + tDate + "' and transport_mode='" + sMode + "' and pend_jobstageID in (" + stGroupID + ") and status_job='N'";
                        else
                            sqlQuery = "job_date >='" + fDate + "' and job_date <= '" + tDate + "' and transport_mode='" + sMode + "' and comp_jobstageID in (" + stGroupID + ") and status_job='Y'";

                    }
                    sqlQueryVal = "SELECT job_no,Convert(varchar(10),job_date,103) as job_date,JobStage,JobStatus,Remarks,pend_jobstage,pend_remark,jobsno,inv_dtl,cont_orig,transport_mode,party_name,carrier," +
                                 "mawb_no,mawb_date,PKG_UNIT,NO_OF_PKG,eta,GROSS_UNIT,GROSS_WT,be_no,be_date,bill_no " +
                                 "FROM View_JobStageWiseDetails js " +
                                 "where  " +
                                 "  " + sqlQuery + " order by job_no";
                }
                else if (sMode == "S")
                {
                    if (stGroup == "0")
                        sqlQuery = "job_date >='" + fDate + "' and job_date <= '" + tDate + "' and transport_mode='" + sMode + "' and status_job='Y'  ";
                    else
                    {
                        if (RBStage.SelectedValue == "0")
                            sqlQuery = "job_date >='" + fDate + "' and job_date <= '" + tDate + "' and transport_mode='" + sMode + "' and pend_jobstageID in (" + stGroupID + ") and status_job='N'";
                        else
                            sqlQuery = "job_date >='" + fDate + "' and job_date <= '" + tDate + "' and transport_mode='" + sMode + "' and comp_jobstageID in (" + stGroupID + ") and status_job='Y'";
                        
                    }


                    sqlQueryVal = "SELECT job_no,Convert(varchar(10),job_date,103) as job_date,JobStage,JobStatus,Remarks,pend_jobstage,pend_remark,jobsno,inv_dtl,cont_orig,transport_mode,party_name,carrier, " +
                                "mawb_no,mawb_date,PKG_UNIT,NO_OF_PKG,eta,GROSS_UNIT,GROSS_WT,be_no,be_date,bill_no " +
                                " FROM View_JobStageWiseDetails js " +
                                " where" +

                                " " + sqlQuery + " order by job_no";
                }
                else
                {
                    if (stGroup == "0")
                        sqlQuery = "job_date >='" + fDate + "' and job_date <= '" + tDate + "' ";
                    else
                    {
                        if (RBStage.SelectedValue == "0")
                            sqlQuery = "job_date >='" + fDate + "' and job_date <= '" + tDate + "' and i.transport_mode='" + sMode + "' and pend_jobstageID in (" + stGroupID + ")";
                        else
                            sqlQuery = "job_date >='" + fDate + "' and job_date <= '" + tDate + "' and i.transport_mode='" + sMode + "' and comp_jobstageID in (" + stGroupID + ")";

                    }
                    sqlQueryVal = "SELECT job_no,Convert(varchar(10),job_date,103) as job_date,JobStage,JobStatus,Remarks,pend_jobstage,pend_remark,jobsno,inv_dtl,cont_orig,transport_mode,party_name,carrier," +
                                 "mawb_no,mawb_date,PKG_UNIT,NO_OF_PKG,eta,GROSS_UNIT,GROSS_WT,be_no,be_date " +
                                 "FROM View_JobStageWiseDetails js " +
                                 "where  " +
                                 
                                 " " + sqlQuery + " order by job_no" ;
                }

            }


            if (sqlQueryVal != "")
            {
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

        protected void ExportPage_Click(object sender, EventArgs e)
        {
            string sysDates = DateTime.Now.ToString("dd-MMM-yyyy");
            string FileName = "ReportGroupWise" + sysDates;
            string strFileName = FileName + ".xls";
            BtnSubmit_Click(sender, e);
            GridViewExportUtil.ExportExcell(strFileName, Grdiworkreg);
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

        protected void Grdiworkreg_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            

            if (e.Row.RowType == DataControlRowType.DataRow)
            {

               
                //if (e.Row.Cells[2].Text != "&nbsp;")
                //{
                //    DateTime bDate = Convert.ToDateTime(e.Row.Cells[2].Text);
                //    e.Row.Cells[2].Text = bDate.ToString("dd/MM/yyyy");
                //}
                //if (e.Row.Cells[7].Text != "&nbsp;")
                //{
                //    DateTime bDate = Convert.ToDateTime(e.Row.Cells[7].Text);
                //    e.Row.Cells[7].Text = bDate.ToString("dd/MM/yyyy");
                //}
                //if (e.Row.Cells[12].Text != "&nbsp;")
                //{
                //    DateTime bDate = Convert.ToDateTime(e.Row.Cells[12].Text);
                //    e.Row.Cells[12].Text = bDate.ToString("dd/MM/yyyy");
                //}
                //if (e.Row.Cells[14].Text != "&nbsp;")
                //{
                //    DateTime bDate = Convert.ToDateTime(e.Row.Cells[14].Text);
                //    e.Row.Cells[14].Text = bDate.ToString("dd/MM/yyyy");
                //}

               
            }
        }
        protected void GETCONTR(string sqlQuery)
        {
            MySqlConnection conn = new MySqlConnection(strconn);
            MySqlDataAdapter da = new MySqlDataAdapter(sqlQuery, conn);
            DataSet ds = new DataSet();
            da.Fill(ds, "impcontdet");
            if (ds.Tables["impcontdet"].Rows.Count != 0)
            {
                DataRowView row = ds.Tables["impcontdet"].DefaultView[0];
                string cType = row["cont_type"].ToString();
                Session["CTYPE"] = cType;
            }
        }
   
    }
}