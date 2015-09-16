using System;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Net.Mail;
using System.IO;
using System.Net.Mime;
using System.Net;
using System.Collections;
using System.Collections.Generic;
//using MySql;
//using MySql.Data.MySqlClient;
using System.Data.SqlClient;

namespace ImpexCube
{
    public partial class frmJobImporterWiseReport : System.Web.UI.Page
    {
        VTS.ImpexCube.Business.pMailBL ba = new VTS.ImpexCube.Business.pMailBL();

        string strconn = (string)ConfigurationManager.ConnectionStrings["Constr"].ConnectionString;
      
       // string strconnVI = (string)ConfigurationManager.AppSettings["ConnectionVisual"];
        #region
        private string PNAME = "";
        private string mail;
        private string pc;
        private string pADD;
        private string rptVAL = "";
      
        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack == false)
            {
                //user authentication code
                //string formID = "Importer Wise Report";

                //Authenticate.Forms(formID);
                //string Validate = (string)Session["DISABLE"];
                //if (Validate == "True")
                //{

                    rbBill.SelectedValue = "DP";

                    trMail.Visible = false;
                    txtFdate.Text = (string)Session["fdate"];
                    string todate = System.DateTime.Now.ToString("dd/MM/yyyy");
                    txtTdate.Text = todate;

                    Session["FinancialYear"] = (string)Session["FYear"];
                    //to Get Party name from party Master Table
                    string strQuery = "select distinct Importer from T_Importer order by Importer";
                    drCustomer.DataSource = GetDataSql(strQuery);
                    drCustomer.DataTextField = "Importer";
                    drCustomer.DataValueField = "Importer";
                    drCustomer.DataBind();
                    drCustomer.Items.Insert(0, new ListItem("~select~", "0"));



                    string strQueryJS = "select distinct StageName from M_Stage ";

                    drPenStage.DataSource = GetDataSql(strQueryJS);
                    drPenStage.DataTextField = "StageName";
                    drPenStage.DataValueField = "StageName";
                    drPenStage.DataBind();
                    drPenStage.Items.Insert(0, new ListItem("~select~", "0"));
                    TemplateBind();

                //}
                //else
                //{
                //    ScriptManager.RegisterStartupScript(this, this.GetType(), "redirect", "alert('You have not Authorized for this Page'); window.location.href='HomePage.aspx';", true);

                //}
            }
        }

        public DataSet GetDataSql(string sqlQuery)
        {

            SqlConnection conn = new SqlConnection(strconn);

            SqlDataAdapter da = new SqlDataAdapter(sqlQuery, conn);

            DataSet ds = new DataSet();

            da.Fill(ds, "iworkreg");

            return ds;
        }

        public override void VerifyRenderingInServerForm(Control control)
        {
            return;
        }

        //public DataSet GetData(string sqlQuery)
        //{

        //    MySqlConnection conn = new MySqlConnection(strconnVI);
           
        //    MySqlDataAdapter da = new MySqlDataAdapter(sqlQuery, conn);

        //    DataSet ds = new DataSet();
           
        //    da.Fill(ds, "iworkreg");

        //    return ds;
        //}

        protected void BtnSubmit_Click(object sender, EventArgs e)
        {

            /* Date Conversions*/
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
                string shrtNAme = drClientRPT.SelectedValue;
                PNAME = drCustomer.SelectedItem.Text;

                DataTable dtRemoveDuplicate = new DataTable();

                //Comment this line for DataView method

                if (tDate == "")
                    tDate = fDate;

                if (fDate == "")
                {
                    Response.Write("<script>" + "alert('Please Give  Date Values Or Job No');" + "</script>");
                   

                }
                else if (fDate != "")
                {
                    if (pendPName != "0" && pendJStage == "0")
                    {
                        if (rbBill.SelectedValue == "DP")
                            sqlQuery = "(((JobReceivedDate)>='" + fDate + "') and ((JobReceivedDate)<='" + tDate + "') and (Importer='" + pendPName + "') And IsModified=1) ";
                        else
                            sqlQuery = "(((JobReceivedDate)>='" + fDate + "') and ((JobReceivedDate)<='" + tDate + "') and (Importer='" + pendPName + "')) ";
                    }
                    else if (pendPName != "0" && pendJStage != "0")
                    {
                        if (rbBill.SelectedValue == "DP")
                            sqlQuery = "(((JobReceivedDate)>='" + fDate + "') and ((JobReceivedDate)<='" + tDate + "') and (Importer ='" + pendPName + "') and (JobStage='" + pendDesc + "')) ";
                        else
                            sqlQuery = "(((JobReceivedDate)>='" + fDate + "') and ((JobReceivedDate)<='" + tDate + "') and (Importer ='" + pendPName + "') and (JobStage='" + pendDesc + "')) ";
                    }
                    else
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Please select Party Name ');", true);
                     


                    Session["PCODE"] = drCustomer.SelectedValue;

                }

                if (sqlQuery != "")
                {
                    if (shrtNAme == "0")
                    {
                        sqlQueryVal = " SELECT distinct jobno,Convert(varchar(11),JobReceivedDate,106) as JobReceivedDate,Importer,ShipmentCountry,ShippingLine,MasterBLNo,Convert(varchar(11),MasterBLDate,106) as MasterBLDate, " +
                                      " HouseBLNo,Convert(varchar(11),HouseBLDate,106) as HouseBLDate, " +
                                      "InvoiceDetail,NoOfPackages,PackagesUnit,GrossWeight,GrossWeightUnit,beno,Convert(varchar(11),bedate,106) as bedate,convert(varchar(11),eta,106) as eta,JobStage,JobStatus,Remarks" +
                                      " FROM View_ImportJobStatusUpdate  " +
                                      "  WHERE " + sqlQuery + " and status_job='N' " +
                                      "ORDER BY jobno";

                        DataSet dsRemove = GetDataSql(sqlQueryVal);
                        dtRemoveDuplicate = dsRemove.Tables[0];
                        dtRemoveDuplicate = DeleteDuplicateFromDataTable(dtRemoveDuplicate, "jobno");

                        Grdiworkreg.DataSource = dtRemoveDuplicate;
                        Grdiworkreg.DataBind();
                        // to check 
                        if (Grdiworkreg.PageCount == 0)
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Records Not Found for Given Values');", true);
                       
                        else
                        {
                            string sql = "select * from T_Importer where Importer='" + compPName + "'";
                            trMail.Visible = true;
                           // GetMail(sql);
                            GridView1.Visible = false;
                            Grdiworkreg.Visible = true;
                        }

                    }
                    else
                    {
                        getRPT(shrtNAme);
                     
                        sqlQueryVal = " SELECT  " + rptVAL + " " +
                                              "FROM View_ImportJobStatusUpdate  " +
                                              " WHERE " + sqlQuery + " and status_job='N' " +
                                              "ORDER BY jobno";

                        DataSet dsRemove = GetDataSql(sqlQueryVal);
                        dtRemoveDuplicate = dsRemove.Tables[0];
                        dtRemoveDuplicate = DeleteDuplicateFromDataTable(dtRemoveDuplicate, "jobno");

                        GridView1.DataSource = dtRemoveDuplicate;
                        GridView1.DataBind();
                        //to check
                        if (GridView1.PageCount == 0)
                            Response.Write("<script>" + "alert('Records Not Found for Given Values');" + "</script>");
                        else
                        {
                            string sql = "select * from T_Importer where Importer='" + compPName + "'";
                            trMail.Visible = true;
                           
                            GridView1.Visible = true;
                            Grdiworkreg.Visible = false;
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('" + ex.Message + "');", true);
            }
        }
        protected DataTable DeleteDuplicateFromDataTable(DataTable dtDuplicate, string columnName)
        {
            Hashtable hashT = new Hashtable();
            ArrayList arrDuplicate = new ArrayList();
            foreach (DataRow row in dtDuplicate.Rows)
            {
                if (hashT.Contains(row[columnName]))
                    arrDuplicate.Add(row);
                else
                    hashT.Add(row[columnName], string.Empty);
            }
            foreach (DataRow row in arrDuplicate)
                dtDuplicate.Rows.Remove(row);

            return dtDuplicate;
        }

        protected void GetParty(string jno)
        {
            SqlConnection conn = new SqlConnection(strconn);
            string sqlQuery = "select * from View_ImportJobStatusUpdate where jobno='" + jno + "'";
            SqlDataAdapter da = new SqlDataAdapter(sqlQuery, conn);
            DataSet ds = new DataSet();
            da.Fill(ds, "iworkreg");
            DataRowView row = ds.Tables["iworkreg"].DefaultView[0];
            pc = row["Importer"].ToString();
            Session["PCODE"] = pc;
            pADD = row["Address"].ToString();
           
        }

        protected void GetMail(string sqlQuery)
        {
            SqlConnection conn = new SqlConnection(strconn);
            SqlDataAdapter da = new SqlDataAdapter(sqlQuery, conn);
            DataSet ds = new DataSet();
            da.Fill(ds, "partyMail");
            DataRowView row = ds.Tables["partyMail"].DefaultView[0];
            mail = row["email"].ToString();
            TextBox1.Text = mail;
        }
        
        protected void GetFormLoad()
        {
            string sqlQuery = "select jobno,JobReceivedDate,InvoiceDetail,ShipmentCountry,Importer,ShippingLine,MasterBLNo,MasterBLDate,PackagesUnit,NoOfPackages,eta, "+
                              " GrossWeightUnit,GrossWeight,beno,bedate,Remarks " +
                              "from View_ImportJobStatusUpdate " +
                              "order by jobno";
            Grdiworkreg.DataSource = GetDataSql(sqlQuery);
            Grdiworkreg.DataBind();
        }

        protected void Grdiworkreg_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            string fy = (string)Session["FinancialYear"];
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                string jno = e.Row.Cells[1].Text;
                string query = "select distinct jobno,Consignor,Consignor from View_ImportJobStatusUpdate " +
                               " where jobno like '%" + jno + "%'";
                SqlConnection con = new SqlConnection(strconn);
                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = query;
                cmd.Connection = con;
                SqlDataAdapter dAdapter = new SqlDataAdapter(cmd);
                DataTable dtConsr = new DataTable();
                dAdapter.Fill(dtConsr);
                foreach (DataRow dtRow in dtConsr.Rows)
                {
                    Label lblconsr = (Label)e.Row.FindControl("lblSupplier");
                    lblconsr.Text = dtRow[2].ToString();
                }



                if (e.Row.Cells[8].Text == "01 Jan 1900")
                {

                    e.Row.Cells[8].Text = "";
                }
                if (e.Row.Cells[10].Text == "01 Jan 1900")
                {

                    e.Row.Cells[10].Text = "";
                }
                if (e.Row.Cells[15].Text == "01 Jan 1900")
                {

                    e.Row.Cells[15].Text = "";
                }
                if (e.Row.Cells[16].Text == "01 Jan 1900")
                {

                    e.Row.Cells[16].Text = "";
                }
            }
        }

        protected void BtnSendMail_Click(object sender, EventArgs e)
        {

            string MailId = "";
            MailId = TextBox1.Text;
            if (MailId == "")
            {
                Response.Write("<script>" + "alert('Please Give Mail id ....');" + "</script>");
                TextBox1.Focus();
            }

            else
            {
                try
                {
                    string pCode = (string)Session["PCODE"];
                    string path = GetServerPath(pCode);
                    string shrtNAme = drClientRPT.SelectedValue;
                    if (shrtNAme == "0")
                        NewExportGridViewToExcelClass.ExportToFile(path, Grdiworkreg);
                    else
                        NewExportGridViewToExcelClass.ExportToFile(path, GridView1);
                    string mTo = TextBox1.Text;
                    string mFrom = (string)Session["User-Mail"];
                    string mCC = "e.vivek@vts.in";
                    string mBcc = "kishor@vts.in";
                    string mSub = "Job Status Report ";
                    string mUser = (string)Session["USER-NAME"];
                    string mMessage = "Dear Sir \n Please find attached herewith for your Job Status .. \n\n Thanks And Regards\n " + mUser;

                    SendMail.CreateMessageWithAttachment(path, mTo, mFrom, mSub, mMessage);
                 
                    Response.Write("<script>" + "alert('Mail has been sent successfully ....');" + "</script>");
                    //insert mail status reports
                    SendMailStatus(mFrom, mTo, mCC, mBcc, mSub, mMessage, path);
                    trMail.Visible = false;
                    TextBox1.Text = "";

                }
                catch (SystemException ex)
                {
                    lblError.Text = ex.Message;
                }
            }
        }

        protected void SendMailStatus(string strFrom, string strTo, string strCc, string strBcc, string strSubject, string strMessage, string strAttach)
        {
        
            DateTime rightNow = DateTime.Now;
            string strTime = String.Format("{0:T}", rightNow);
            string strDate = System.DateTime.Now.ToString("MM/dd/yyyy");
            string strUser = (string)Session["USER-NAME"];
            string strCMP = (string)Session["COMP"];

            // variables to pass to ba
            int res = ba.SendMail(strFrom, strTo, strCc, strBcc, strSubject, strMessage, strAttach, strDate, strTime, strUser, strCMP);
        }

        public string GetServerPath(string PartyName)
        {
            string file = string.Empty;
            string datetime = DateTime.Now.Year.ToString() + "-" + DateTime.Now.Month.ToString() + "-" + DateTime.Now.Day.ToString() + "-" + DateTime.Now.Hour.ToString() + "-" + DateTime.Now.Minute.ToString() + "-" + DateTime.Now.Second.ToString();
            string serverPath = Server.MapPath("~") + "\\" + "JSR";

            if (Directory.Exists(serverPath))
            {
                string PartyNameDirectory = serverPath + "\\" + PartyName;
                if (Directory.Exists(PartyNameDirectory))
                {
                    file = PartyNameDirectory + "\\" + PartyName + datetime + ".xls";
                }
                else
                {
                    Directory.CreateDirectory(PartyNameDirectory);
                    file = PartyNameDirectory + "\\" + PartyName + datetime + ".xls";
                }

            }
            else
            {
                Directory.CreateDirectory(serverPath);
                string PartyNameDirectory = serverPath + "\\" + PartyName;
                if (Directory.Exists(PartyNameDirectory))
                {
                    file = PartyNameDirectory + "\\" + PartyName + datetime + ".xls"; ;
                }
                else
                {
                    Directory.CreateDirectory(PartyNameDirectory);
                    file = PartyNameDirectory + "\\" + PartyName + datetime + ".xls"; ;
                }

            }
            return file;
        }

        [System.Web.Services.WebMethodAttribute(), System.Web.Script.Services.ScriptMethodAttribute()]
        public static string[] GetCompletionList(string prefixText, int count, string contextKey)
        {
            return default(string[]);
        }



        protected void ExportPage_Click(object sender, EventArgs e)
        {
            string sysDates = DateTime.Now.ToString("dd-MM-yyyy");
            string custname = drCustomer.SelectedItem.Text;
            string FileName = custname.Replace(" ","") ;
            string strFileName = FileName + ".xls";
          //  BtnSubmit_Click(sender, e);
            try
            {
                if ((Grdiworkreg.Rows.Count!=0) || (GridView1.Rows.Count != 0))
                {
                    string na = "GoodsReceiptNote.xls";
                    string ExcelExport = na;
                    // Export(ExcelExport, GridView1);
                    Response.Clear();
                    Response.Buffer = true;
                    Response.AddHeader("content-disposition", "attachment;filename= " + strFileName);
                    Response.Charset = "";
                    Response.ContentType = "application/vnd.ms-excel";
                    System.IO.StringWriter stringWrite = new System.IO.StringWriter();
                    System.Web.UI.HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);
                    aa.RenderControl(htmlWrite);
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

            //string shrtNAme = drClientRPT.SelectedValue;
            //if (shrtNAme == "0")

            //    GridViewExportDet.ExportExcell(strFileName, Grdiworkreg);
            //else
            //    GridViewExportDet.ExportExcell(strFileName, GridView1);
        }
        protected void Button1_Click(object sender, EventArgs e)
        {

        }
        protected void getRPT(string RPT)
        {
            
            SqlConnection conn = new SqlConnection(strconn);
            string sqlQuery = "SELECT * FROM T_UserReportTemplates where TemplateName='" + RPT + "'";
            SqlDataAdapter da = new SqlDataAdapter(sqlQuery, conn);
            DataSet ds = new DataSet();
            da.Fill(ds, "RPT");
            DataRowView row = ds.Tables["RPT"].DefaultView[0];
            rptVAL = row["CustomField"].ToString();

        }
        protected void drCustomer_SelectedIndexChanged(object sender, EventArgs e)
        {
            string pcode = drCustomer.SelectedItem.Text;
           
        }

        public void TemplateBind()
        {
            string strQuery1 = "select * from T_UserReportTemplates";
            drClientRPT.DataSource = GetDataSql(strQuery1);
            drClientRPT.DataTextField = "TemplateName";
            drClientRPT.DataValueField = "TemplateName";
            drClientRPT.DataBind();
            drClientRPT.Items.Insert(0, new ListItem("~select~", "0"));
        }

        protected void rbBill_SelectedIndexChanged(object sender, EventArgs e)
        {
            string ContractType = rbBill.SelectedValue;
           
            if (ContractType == "DP")
            {
                drCustomer.Items.Clear();
                string Query = "select Distinct Importer from T_Importer order by Importer asc";
                drCustomer.DataSource = GetDataSql(Query);
                drCustomer.DataTextField = "Importer";
                drCustomer.DataValueField = "Importer";
                drCustomer.DataBind();
                drCustomer.Items.Insert(0, new ListItem("~select~", "0"));
            }
            else
            {
                drCustomer.Items.Clear();
                string Query = "SELECT DISTINCT Acc_group FROM  M_AccountMaster order by Acc_group asc";
                drCustomer.DataSource = GetDataSql(Query);
                drCustomer.DataTextField = "Acc_group";
                drCustomer.DataValueField = "Acc_group";
                drCustomer.DataBind();
                drCustomer.Items.Insert(0, new ListItem("~select~", "0"));
            }
        }
       
        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                int numCol = e.Row.Cells.Count;
                for (int col = 1; col < numCol; col++)
                {
                    //if ((GridView1.HeaderRow.Cells[col].Text.Contains("JobReceivedDate")) && (e.Row.Cells[col].Text != "&nbsp;"))
                    //{

                    //    DateTime bDate = Convert.ToDateTime(e.Row.Cells[col].Text);
                    //    e.Row.Cells[col].Text = bDate.ToString("dd/MM/yyyy");
                    //}
                    //if ((GridView1.HeaderRow.Cells[col].Text.Contains("MasterBLDate")) && (e.Row.Cells[col].Text != "&nbsp;"))
                    //{

                    //    DateTime bDate = Convert.ToDateTime(e.Row.Cells[col].Text);
                    //    e.Row.Cells[col].Text = bDate.ToString("dd/MM/yyyy");
                    //}
                    //if ((GridView1.HeaderRow.Cells[col].Text.Contains("HouseBLDate")) && (e.Row.Cells[col].Text != "&nbsp;"))
                    //{

                    //    DateTime bDate = Convert.ToDateTime(e.Row.Cells[col].Text);
                    //    e.Row.Cells[col].Text = bDate.ToString("dd/MM/yyyy");
                    //}
                    //if ((GridView1.HeaderRow.Cells[col].Text.Contains("bedate")) && (e.Row.Cells[col].Text != "&nbsp;"))
                    //{

                    //    DateTime bDate = Convert.ToDateTime(e.Row.Cells[col].Text);
                    //    e.Row.Cells[col].Text = bDate.ToString("dd/MM/yyyy");
                    //}
                    //if ((GridView1.HeaderRow.Cells[col].Text.Contains("eta")) && (e.Row.Cells[col].Text != "&nbsp;"))
                    //{

                    //    DateTime bDate = Convert.ToDateTime(e.Row.Cells[col].Text);
                    //    e.Row.Cells[col].Text = bDate.ToString("dd/MM/yyyy");
                    //}

                  

                    //if ((GridView1.HeaderRow.Cells[col].Text.Contains("INVOICEDATE")) && (e.Row.Cells[col].Text != "&nbsp;"))
                    //{

                    //    DateTime bDate = Convert.ToDateTime(e.Row.Cells[col].Text);
                    //    e.Row.Cells[col].Text = bDate.ToString("dd/MM/yyyy");
                    //}
                    //if ((GridView1.HeaderRow.Cells[col].Text.Contains("COMPLETE_DATE")) && (e.Row.Cells[col].Text != "&nbsp;"))
                    //{

                    //    DateTime bDate = Convert.ToDateTime(e.Row.Cells[col].Text);
                    //    e.Row.Cells[col].Text = bDate.ToString("dd/MM/yyyy");
                    //}
                    //if ((GridView1.HeaderRow.Cells[col].Text.Contains("PENDING_DATE")) && (e.Row.Cells[col].Text != "&nbsp;"))
                    //{

                    //    DateTime bDate = Convert.ToDateTime(e.Row.Cells[col].Text);
                    //    e.Row.Cells[col].Text = bDate.ToString("dd/MM/yyyy");
                    //}
                    //if ((GridView1.HeaderRow.Cells[col].Text.Contains("BEDATE")) && (e.Row.Cells[col].Text != "&nbsp;"))
                    //{

                    //    DateTime bDate = Convert.ToDateTime(e.Row.Cells[col].Text);
                    //    e.Row.Cells[col].Text = bDate.ToString("dd/MM/yyyy");
                    //}
                    //if ((GridView1.HeaderRow.Cells[col].Text.Contains("BILL_DATE")) && (e.Row.Cells[col].Text != "&nbsp;"))
                    //{

                    //    DateTime bDate = Convert.ToDateTime(e.Row.Cells[col].Text);
                    //    e.Row.Cells[col].Text = bDate.ToString("dd/MM/yyyy");
                    //}
                    //if ((GridView1.HeaderRow.Cells[col].Text.Contains("DBNOTE_DATE")) && (e.Row.Cells[col].Text != "&nbsp;"))
                    //{

                    //    DateTime bDate = Convert.ToDateTime(e.Row.Cells[col].Text);
                    //    e.Row.Cells[col].Text = bDate.ToString("dd/MM/yyyy");
                    //}
                    //if ((GridView1.HeaderRow.Cells[col].Text.Contains("GLD")) && (e.Row.Cells[col].Text != "&nbsp;"))
                    //{

                    //    DateTime bDate = Convert.ToDateTime(e.Row.Cells[col].Text);
                    //    e.Row.Cells[col].Text = bDate.ToString("dd/MM/yyyy");
                    //}


                }
            }
        }
    }
}