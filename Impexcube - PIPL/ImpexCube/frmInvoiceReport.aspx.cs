using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.IO;
using System.Web.UI.HtmlControls;

namespace ImpexCube
{
    public partial class frmInvoiceReport : System.Web.UI.Page
    {
        string strconn = (string)ConfigurationManager.ConnectionStrings["Constr"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack == false)
            {
                txtFdate.Text = (string)Session["fdate"];
                string todate = System.DateTime.Now.ToString("dd/MM/yyyy");
                txtTdate.Text = todate;
                GetReportName();
            }
        }

        public void GetReportName()
        { 
        SqlConnection con= new SqlConnection(strconn);
        con.Open();
        string query = "select TemplateName from T_InvoiceReportTemplate";
        DataSet ds = new DataSet();
        SqlDataAdapter da = new SqlDataAdapter(query, con);
        da.Fill(ds, "ReportName");
        con.Close();
        if (ds.Tables["ReportName"].Rows.Count != 0) 
        {
            ddlReportName.DataSource = ds;
            ddlReportName.DataTextField = "TemplateName";
            ddlReportName.DataValueField = "TemplateName";
            ddlReportName.DataBind();
        }
        }

        protected void ExportPage_Click(object sender, EventArgs e)
        {
            try
            {
                string sysDates = DateTime.Now.ToString("dd/MM/yyyy");
                string FileName = ddlReportName.SelectedValue + sysDates;
                string strFileName = FileName + ".xls";
                btnSearch_Click(sender, e);
                //GridViewExportUtil.ExportExcell(strFileName, Grdiworkreg);

                string attachment = "attachment; filename=" + strFileName + " ";
                Response.ClearContent();
                Response.AddHeader("content-disposition", attachment);
                Response.ContentType = "application/ms-excel";

                StringWriter sw = new StringWriter();
                HtmlTextWriter htw = new HtmlTextWriter(sw);

                HtmlForm frm = new HtmlForm();
                gvInvoice.AllowPaging = false;

                gvInvoice.Parent.Controls.Add(frm);
                frm.Attributes["runat"] = "server";
                frm.Controls.Add(gvInvoice);
                frm.RenderControl(htw);

                Response.Write(sw.ToString());
                Response.End();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('" + ex.Message + "');", true);
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {                            
                    if (ddlReportName.SelectedValue != "~Select~")
                    {
                        try
                        {
                            string FD = txtFdate.Text;
                            string TD = txtTdate.Text;

                            string sMM = FD.Substring(3, 2);
                            string sDD = FD.Substring(0, 2);
                            string sYY = FD.Substring(6, 4);
                            string RPT = ddlReportName.SelectedValue;
                            FD = sMM + "/" + sDD + "/" + sYY;
                            Session["QUERY"] = "";

                            string eMM = TD.Substring(3, 2);
                            string eDD = TD.Substring(0, 2);
                            string eYY = TD.Substring(6, 4);
                            TD = eMM + "/" + eDD + "/" + eYY;

                            DateTime fd = Convert.ToDateTime(FD);
                            DateTime td = Convert.ToDateTime(TD);

                            string fDate = fd.ToString("yyyy-MM-dd");
                            string tDate = td.ToString("yyyy-MM-dd");

                            string pName = txtPName.Text;
                            string fieldname = "";
                            string ChargeName = "";

                            string Reportquery = "select * from T_InvoiceReportTemplate where TemplateName='" + ddlReportName.SelectedValue + "' ";
                            SqlConnection conn = new SqlConnection(strconn);
                            conn.Open();
                            SqlDataAdapter da1 = new SqlDataAdapter(Reportquery, conn);

                            DataSet ds1 = new DataSet();

                            da1.Fill(ds1, "data");
                            conn.Close();
                            if (ds1.Tables["data"].Rows.Count != 0)
                            {
                                DataRowView row = ds1.Tables["data"].DefaultView[0];
                                fieldname = row["FieldList"].ToString();
                                ChargeName = row["ChargeList"].ToString();
                            }
                            string sqlQuery = "";
                            string sqlQueryVal = "";
                            string compName= txtPName.Text;
                            string TransportMode = ddlMode.SelectedValue;

                            if (tDate == "")
                                tDate = fDate;

                            if (fDate == "")
                            {
                                Response.Write("<script>" + "alert('Please Give  Date Values Or Job No');" + "</script>");


                            }
                            else if (fDate != "")
                            {

                              
                                    if (compName == "" && TransportMode == "~Select~")
                                        sqlQuery = "  ";

                                    else if (compName != "" && TransportMode == "~Select~")
                                        sqlQuery = "and ( View_Invoice.compName='" + compName + "')  ";


                                    else if (compName == "" && TransportMode != "~Select~")
                                        sqlQuery = "and ( View_Invoice.TransportMode='" + TransportMode + "') ";


                                    else
                                        sqlQuery = "and (View_Invoice.TransportMode='" + TransportMode + "') and (View_Invoice.compName='" + compName + "') ";

                              
                              

                            }
         
            if (sqlQuery != "")
            {

                 sqlQueryVal = " Select * from (select " + fieldname + " from View_Invoice where invoiceDate>='" + fDate + "' and invoiceDate<='" + tDate + "'  "+sqlQuery+" ) " +
                            " as s pivot(sum(amount) for charge_desc in (" + ChargeName + ")) as pivottable  ";

              

                gvInvoice.DataSource = GetData(sqlQueryVal);
                gvInvoice.DataBind();
            }
                          
                            if (gvInvoice.PageCount == 0)
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
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Please select Report Name');", true);
                    }                         


        }

      

        protected void GetInvoiceReport(string fDate, string tDate, string JNO, string pName)
        {
            string sqlQuery = "";
            string sqlQueryVal = "";
            string DutyRequest = "DutyIntimation";
            string DutyPaid = "DutyPaid";
            string month = DateTime.Now.Month.ToString();

            if (txtPName.Text !=""  && ddlReportName.SelectedValue != "~Select~")
                sqlQuery = "doc_received_date >='" + fDate + "' and doc_received_date <= '" + tDate + "'  and party_name='" + pName + "'";
            else if (txtPName.Text != "" && ddlReportName.SelectedValue == "~Select~")
                sqlQuery = "doc_received_date >='" + fDate + "' and doc_received_date <= '" + tDate + "' and party_name='" + pName + "'";
            else if (txtPName.Text == "" && ddlReportName.SelectedValue != "~Select~")
                sqlQuery = "doc_received_date >='" + fDate + "' and doc_received_date <= '" + tDate + "' and job_no='" + JNO + "'";
            else
                sqlQuery = "doc_received_date >='" + fDate + "' and doc_received_date <= '" + tDate + "'";



            sqlQueryVal = " Select * from  (SELECT distinct job_no ,convert(varchar(10),doc_received_date,103) as doc_received_date ,  " +
                        " cont_orig  ,carrier ,toi ,no_of_pkg ,gross_wt , gross_unit, " +
                        "  mawb_no,hawb_no ," +
                        " convert(varchar(10),hawb_date,103) as  hawb_date,convert(varchar(10),eta,103) as eta  ,be_no , " +
                        " be_date ,tot_ass_vl ,Remarks,ID ,convert(varchar(10),StatusDate,103) as StatusDate,Gateway_IGMNo, " +
                        " tot_duty,PortOfShipment,convert(varchar(10),GLD,103) as GLD,VESSEL_NAME,mawb_date,month(convert(datetime,eta,103)) as ETAMonth,datepart(week,(convert(datetime,eta,103))) as JobWeek,currentStatus,cleared  " +
                        " FROM View_GKNReport " +
                          "where  " +
                        " " + sqlQuery + " and status_job ='N' and  transport_mode='S' and cleared is null ) " +
                        " as s pivot(Max(statusdate) for ID in ([16],[17],[21],[26])) as pivottable ";

            Session["QUERY"] = sqlQueryVal;



        }
        public DataSet GetData(string sqlQuery)
        {

            SqlConnection conn = new SqlConnection(strconn);

            SqlDataAdapter da1 = new SqlDataAdapter(sqlQuery, conn);

            DataSet ds1 = new DataSet();

            da1.Fill(ds1, "data");
            return ds1;
        }

        protected void btnExit_Click(object sender, EventArgs e)
        {

        }

     
      
    }
}