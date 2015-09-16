using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;

namespace ImpexCube
{
    public partial class frmJobSync : System.Web.UI.Page
    {
        VTS.ImpexCube.Business.JobSyncBL objJobSync = new VTS.ImpexCube.Business.JobSyncBL();
        string con = (string)ConfigurationManager.ConnectionStrings["Constr"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack == false)
            {
                string formID = "Job Sync";
                Authenticate.Forms(formID);
                string Validate = (string)Session["DISABLE"];
                if (Validate == "True")
                {

                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "redirect", "alert('You have not Authorized for this Page'); window.location.href='HomePage.aspx';", true);

                }
            }

        }

        protected void btnJobSync_Click(object sender, EventArgs e)
        {
            int result = new int();
            string jobDate = string.Empty;
            try
            {
              
                DataSet ds = objJobSync.GetJobDetails((string)Session["FYear"]);
                if (ds.Tables["data"].Rows.Count != 0)
                {
                    DataTable dt = ds.Tables["data"];
                    int co = dt.Rows.Count;
                    for (int i = 0; i < co; i++)
                    {
                        DataRow row = dt.Rows[i];
                        string jobno = row["JobNo"].ToString();
                        string partycode = row["PartyCode"].ToString();
                        string party = row["PartyName"].ToString();
                        party = party.Replace("'", "");
                        party = party.Replace(",", "");
                        string date = DateTime.Now.ToString("dd/MM/yyyy");
                        string jobreceived = row["JobReceived"].ToString();
                        if (jobreceived == "" || jobreceived == null)
                        {
                            jobDate = string.Empty;
                        }
                        else
                        {
                            jobDate = Convert.ToDateTime(jobreceived).ToString("dd/MM/yyyy");
                        }
                        string mode = row["Mode"].ToString();
                        string Betype = row["BEType"].ToString();
                        string invdtl = row["InvoiceDetails"].ToString();
                        
                        invdtl = invdtl.Replace("'", "");

                     
                        party = party.Replace("'", "");
                        if (mode == "S")
                        {
                            mode = "Sea";
                        }
                        else if (mode == "A")
                        {
                            mode = "Air";
                        }

                        DataSet jds = objJobSync.SelectJobNo(jobno);
                        if (jds.Tables["data"].Rows.Count != 0)
                        {
                            result = objJobSync.UpdateJobDetails(jobno, jobDate, partycode, party, mode, invdtl, Betype, (string)Session["UserName"], date);
                        }
                        else
                        {
                          
                            {
                                result = objJobSync.InsertJobDetails(jobno, jobDate, partycode, party, mode, invdtl, Betype, (string)Session["UserName"], date, (string)Session["UserName"], date);
                            }
                        }
                    }

                    GetIWorkjobDetails();
                    GetIShipDetails();
                    GetPartyDetails();
                    GetIWorkRegDetails();
                    GetIJobPosDetails();

                    GetInvoiceChargeDetails();
                    GetProductDetails();
                    GetInvoiceDetails();

                    if (result == 1)
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "redirect", "alert('Synced Successfully'); window.location.href='frmJobSync.aspx';", true);
                    }
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('" + ex.Message + "');", true);
            }
        }

        private void GetIWorkjobDetails()
        {
           try
           {
                int result = new int();
                string jobDate = string.Empty;

                DataSet ds = objJobSync.GetIworkDetails((string)Session["FYear"]);
                if (ds.Tables["data"].Rows.Count != 0)
                {
                    DataTable dt = ds.Tables["data"];
                    int co = dt.Rows.Count;
                    for (int i = 0; i < co; i++)
                    {

                        DataRow row = dt.Rows[i];
                        string job_no = row["Job_No"].ToString();
                        string job_date = row["job_date"].ToString();
                        if (job_date == "" || job_date == null)
                        {
                            job_date = string.Empty;
                        }
                        else
                        {

                            job_date = Convert.ToDateTime(job_date).ToString("dd/MM/yyyy");
                        }
                        string comp_jobStage = row["comp_jobStage"].ToString();
                        string comp_jobDate = row["comp_jobDate"].ToString();
                        if (comp_jobDate == "" || comp_jobDate == null)
                        {
                            comp_jobDate = string.Empty;
                        }
                        else
                        {
                            comp_jobDate = Convert.ToDateTime(comp_jobDate).ToString("dd/MM/yyyy");
                        }
                        string comp_remark = row["comp_remark"].ToString();
                        string pend_jobstage = row["pend_jobstage"].ToString();
                        string pend_remark = row["pend_remark"].ToString();
                        pend_remark = pend_remark.Replace("'", "");
                        string PARTY_CODE = row["PARTY_CODE"].ToString();
                        string jobsno = row["jobsno"].ToString();
                        string INV_DTL = row["INV_DTL"].ToString();
                        INV_DTL = INV_DTL.Replace("'", "");
                        string cont_orig = row["cont_orig"].ToString();
                        cont_orig = cont_orig.Replace("'", "");
                        string ETA = row["ETA"].ToString();
                        if (ETA == "" || ETA == null)
                        {
                            ETA = string.Empty;
                        }
                        else
                        {
                            ETA = Convert.ToDateTime(ETA).ToString("dd/MM/yyyy");
                        }
                        string MAWB_NO = row["MAWB_NO"].ToString();
                        string MAWB_DATE = row["MAWB_DATE"].ToString();
                        if (MAWB_DATE == "" || MAWB_DATE == null)
                        {
                            MAWB_DATE = string.Empty;
                        }
                        else
                        {
                            MAWB_DATE = Convert.ToDateTime(MAWB_DATE).ToString("dd/MM/yyyy");
                        }
                        string NO_OF_PKG = row["NO_OF_PKG"].ToString();
                        string PKG_UNIT = row["PKG_UNIT"].ToString();
                        string GROSS_WT = row["GROSS_WT"].ToString();
                        string GROSS_UNIT = row["GROSS_UNIT"].ToString();
                        string Carrier = row["Carrier"].ToString();
                        Carrier = Carrier.Replace("'", "");
                        string BE_NO = row["BE_NO"].ToString();
                        string BE_DATE = row["BE_DATE"].ToString();
                        if (BE_DATE == "" || BE_DATE == null)
                        {
                            BE_DATE = string.Empty;
                        }
                        else
                        {
                            BE_DATE = Convert.ToDateTime(BE_DATE).ToString("dd/MM/yyyy");
                        }
                        string PARTY_NAME = row["PARTY_NAME"].ToString();
                        PARTY_NAME = PARTY_NAME.Replace("'", "");
                        string Status_job = row["Status_job"].ToString();
                        string transport_mode = row["transport_mode"].ToString();
                        string bill_no = row["bill_no"].ToString();
                        string org_doc_date = row["org_doc_date"].ToString();
                        if (org_doc_date == "" || org_doc_date == null)
                        {
                            org_doc_date = string.Empty;
                        }
                        else
                        {
                            org_doc_date = Convert.ToDateTime(org_doc_date).ToString("dd/MM/yyyy");
                        }
                        string bill_date = row["bill_date"].ToString();
                        if (bill_date == "" || bill_date == null)
                        {
                            bill_date = string.Empty;
                        }
                        else
                        {
                            bill_date = Convert.ToDateTime(bill_date).ToString("dd/MM/yyyy");
                        }

                        DataSet jds1 = objJobSync.SelectJobNo(job_no);
                        if (jds1.Tables["data"].Rows.Count != 0)
                        {
                            result = objJobSync.UpdateJobStatus(job_no, Status_job);
                        }
                        DataSet jds = objJobSync.SelectIworkJobNo(job_no);
                        if (jds.Tables["data"].Rows.Count != 0)
                        {
                            result = objJobSync.UpdateIworkDetails(job_no, job_date, comp_jobStage, comp_jobDate, comp_remark, pend_jobstage, pend_remark, PARTY_CODE, jobsno, INV_DTL,
                    cont_orig, ETA, MAWB_NO, MAWB_DATE, NO_OF_PKG, PKG_UNIT, GROSS_WT, GROSS_UNIT, Carrier, BE_NO, BE_DATE, PARTY_NAME, Status_job, transport_mode, bill_no, org_doc_date,bill_date);
                        }
                        else
                        {
                            result = objJobSync.InsertIworkDetails(job_no, job_date, comp_jobStage, comp_jobDate, comp_remark, pend_jobstage, pend_remark, PARTY_CODE, jobsno, INV_DTL,
                    cont_orig, ETA, MAWB_NO, MAWB_DATE, NO_OF_PKG, PKG_UNIT, GROSS_WT, GROSS_UNIT, Carrier, BE_NO, BE_DATE, PARTY_NAME, Status_job, transport_mode, bill_no, org_doc_date, bill_date);
                        }
                    }

                    //if (result == 1)
                    //{
                    //    ScriptManager.RegisterStartupScript(this, this.GetType(), "redirect", "alert('Synced Successfully'); window.location.href='frmJobSync.aspx';", true);
                    //}
                }
           }
           catch (Exception ex)
           {
               string iworkregJob = "iworkregJob";
               ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('" + ex.Message + "'+'"+iworkregJob+"');", true);
           }
            
        }

        private void GetIShipDetails()
        {
            try
            {
            int result = new int();
            string jobDate = string.Empty;

            DataSet ds = objJobSync.GetIShipDetails((string)Session["FYear"]);
            if (ds.Tables["data"].Rows.Count != 0)
            {
                DataTable dt = ds.Tables["data"];
                int co = dt.Rows.Count;
                for (int i = 0; i < co; i++)
                {

                    DataRow row = dt.Rows[i];
                    string job_no = row["Job_No"].ToString();
                    string hawb_no = row["hawb_no"].ToString();
                    string hawb_date = row["hawb_date"].ToString();
                    if (hawb_date == "" || hawb_date == null)
                    {
                        hawb_date = string.Empty;
                    }
                    else
                    {

                        hawb_date = Convert.ToDateTime(hawb_date).ToString("dd/MM/yyyy");
                    }
                    string GLD = row["GLD"].ToString();
                    if (GLD == "" || GLD == null)
                    {
                        GLD = string.Empty;
                    }
                    else
                    {

                        GLD = Convert.ToDateTime(GLD).ToString("dd/MM/yyyy");
                    }

                    DataSet jds = objJobSync.SelectIworkJobNo(job_no);
                    if (jds.Tables["data"].Rows.Count != 0)
                    {
                        result = objJobSync.UpdateIShipDetails(hawb_no, hawb_date, job_no, GLD);
                    }
                   
                }

               
            }
            }
            catch (Exception ex)
            {
                string ishipdtl = "ishipdtl";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('" + ex.Message + "'+'" + ishipdtl + "');", true);
            }
        }

        private void GetPartyDetails()
        {
            try
            {
            int result = new int();
            string jobDate = string.Empty;

            DataSet ds = objJobSync.GetPartyDetails();
            if (ds.Tables["data"].Rows.Count != 0)
            {
                DataTable dt = ds.Tables["data"];
                int co = dt.Rows.Count;
                for (int i = 0; i < co; i++)
                {

                    DataRow row = dt.Rows[i];
                    string PARTY_CODE = row["PARTY_CODE"].ToString();
                    string PARTY_NAME = row["PARTY_NAME"].ToString();
                    PARTY_NAME = PARTY_NAME.Replace("'", "").Replace(",","");
                    
                    string GROUP_ID = row["GROUP_ID"].ToString();



                    DataSet jds = objJobSync.SelectpartyCode(PARTY_CODE);
                    if (jds.Tables["data"].Rows.Count != 0)
                    {
                        result = objJobSync.UpdatePartyName(PARTY_CODE, PARTY_NAME, GROUP_ID);
                    }
                    else
                    {
                        result = objJobSync.InsertPartyName(PARTY_CODE, PARTY_NAME, GROUP_ID);
                    
                    }

                }

             
            }

            }
            catch (Exception ex)
            {
                string partydtl = "partydtl";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('" + ex.Message + "'+'" + partydtl + "');", true);
            }

        }

        private void GetIWorkRegDetails()
        {
            try
            {
            int result = new int();
            string jobDate = string.Empty;

            DataSet ds = objJobSync.GetIWorkRegDetails((string)Session["FYear"]);
            if (ds.Tables["data"].Rows.Count != 0)
            {
                DataTable dt = ds.Tables["data"];
                int co = dt.Rows.Count;
                for (int i = 0; i < co; i++)
                {

                    DataRow row = dt.Rows[i];
                    string JOB_NO = row["JOB_NO"].ToString();
                    string BE_TYPE = row["BE_TYPE"].ToString();
                    string TOT_ASS_VL = row["TOT_ASS_VL"].ToString();
                    string TOT_DUTY = row["TOT_DUTY"].ToString();
                    string doc_received_date = row["DOC_RECD"].ToString();
                    if (doc_received_date == "" || doc_received_date == null)
                    {
                        doc_received_date = string.Empty;
                    }
                    else
                    {

                        doc_received_date = Convert.ToDateTime(doc_received_date).ToString("dd/MM/yyyy");
                    }

                    DataSet jds = objJobSync.SelectIworkReg(JOB_NO);
                    if (jds.Tables["data"].Rows.Count != 0)
                    {
                        result = objJobSync.UpdateIWorkReg(JOB_NO, BE_TYPE, TOT_ASS_VL, TOT_DUTY, doc_received_date);
                    }
                    //else
                    //{
                    //    result = objJobSync.InsertPartyName(JOB_NO, BE_TYPE);

                    //}

                }


            }
            }
            catch (Exception ex)
            {
                string iworkreg = "iworkreg";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('" + ex.Message + "'+'" + iworkreg + "');", true);
            }
        }

        private void GetIJobPosDetails()
        {
            try
            {
            int result = new int();
            string jobDate = string.Empty;

            DataSet ds = objJobSync.GetIJobPosDetails((string)Session["FYear"]);
            if (ds.Tables["data"].Rows.Count != 0)
            {
                DataTable dt = ds.Tables["data"];
                int co = dt.Rows.Count;
                for (int i = 0; i < co; i++)
                {

                    DataRow row = dt.Rows[i];
                    string job_no = row["Job_No"].ToString();
                    string DB_NOTE_NO = row["DB_NOTE_NO"].ToString();
                    string DB_DATE = row["DB_DATE"].ToString();
                    if (DB_DATE == "" || DB_DATE == null)
                    {
                        DB_DATE = string.Empty;
                    }
                    else
                    {

                        DB_DATE = Convert.ToDateTime(DB_DATE).ToString("dd/MM/yyyy");
                    }
                  

                    DataSet jds = objJobSync.SelectIworkJobNo(job_no);
                    if (jds.Tables["data"].Rows.Count != 0)
                    {
                        result = objJobSync.UpdateIJobPosDetails(DB_NOTE_NO, DB_DATE, job_no);
                    }

                }


            }
                  }
            catch (Exception ex)
            {
                string ijobpos = "ijobpos";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('" + ex.Message + "'+'" + ijobpos + "');", true);
            }
        }

        private void GetInvoiceChargeDetails()
        {
            int result1 = new int();
            string job_no = string.Empty;

            DataSet ds = objJobSync.GetIInvoiceCharges((string)Session["FYear"]);
            if (ds.Tables["data"].Rows.Count != 0)
            {
                DataTable dt = ds.Tables["data"];
                DataSet ids = objJobSync.GetSInvoiceCharges();
                if (ids.Tables["data"].Rows.Count != 0)
                {
                    string query = "Truncate table iinv_chg";
                    SqlConnection sqlConn = new SqlConnection(con);
                    sqlConn.Open();
                    SqlCommand cmd = new SqlCommand(query, sqlConn);
                    SqlDataAdapter da = new SqlDataAdapter();

                    cmd.CommandText = query;
                    cmd.Connection = sqlConn;
                    da.SelectCommand = cmd;
                    result1 = cmd.ExecuteNonQuery();
                    sqlConn.Close();

                    using (SqlConnection conn = new SqlConnection(con))
                    {
                        conn.Open();
                        using (SqlBulkCopy s = new SqlBulkCopy(conn))
                        {
                            s.DestinationTableName = "dbo.iinv_chg";
                            foreach (DataColumn col in dt.Columns)
                            {
                                s.ColumnMappings.Add(col.ColumnName, col.ColumnName);
                            }
                            s.WriteToServer(dt);
                        }
                    }

                }
                else
                {
                    using (SqlConnection conn = new SqlConnection(con))
                    {
                        conn.Open();
                        using (SqlBulkCopy s = new SqlBulkCopy(conn))
                        {
                            s.DestinationTableName = "dbo.iinv_chg";
                            foreach (DataColumn col in dt.Columns)
                            {
                                s.ColumnMappings.Add(col.ColumnName, col.ColumnName);
                            }
                            s.WriteToServer(dt);
                        }
                    }
                }
            }
        }

        private void GetInvoiceDetails()
        {
            int result1 = new int();
            string job_no = string.Empty;

            DataSet ds = objJobSync.GetIInvoiceDetails((string)Session["FYear"]);
            if (ds.Tables["data"].Rows.Count != 0)
            {
                DataTable dt = ds.Tables["data"];
                DataSet ids = objJobSync.GetSInvoiceDetails();
                if (ids.Tables["data"].Rows.Count != 0)
                {
                    string query = "Truncate table iinv_dtl";
                    SqlConnection sqlConn = new SqlConnection(con);
                    sqlConn.Open();
                    SqlCommand cmd = new SqlCommand(query, sqlConn);
                    SqlDataAdapter da = new SqlDataAdapter();

                    cmd.CommandText = query;
                    cmd.Connection = sqlConn;
                    da.SelectCommand = cmd;
                    result1 = cmd.ExecuteNonQuery();
                    sqlConn.Close();

                    using (SqlConnection conn = new SqlConnection(con))
                    {
                        conn.Open();
                        using (SqlBulkCopy s = new SqlBulkCopy(conn))
                        {
                            s.DestinationTableName = "dbo.iinv_dtl";
                            foreach (DataColumn col in dt.Columns)
                            {
                                s.ColumnMappings.Add(col.ColumnName, col.ColumnName);
                            }
                            s.WriteToServer(dt);
                        }
                    }

                }
                else
                {
                    using (SqlConnection conn = new SqlConnection(con))
                    {
                        conn.Open();
                        using (SqlBulkCopy s = new SqlBulkCopy(conn))
                        {
                            s.DestinationTableName = "dbo.iinv_dtl";
                            foreach (DataColumn col in dt.Columns)
                            {
                                s.ColumnMappings.Add(col.ColumnName, col.ColumnName);
                            }
                            s.WriteToServer(dt);
                        }
                    }
                }
            }
        }

        private void GetProductDetails()
        {
            int result = new int();
            string job_no = string.Empty;

            DataSet ds = objJobSync.GetIProductDetails((string)Session["FYear"]);
            if (ds.Tables["data"].Rows.Count != 0)
            {
                DataTable dt = ds.Tables["data"];
                DataSet ids = objJobSync.GetSProductDetails();
                if (ids.Tables["data"].Rows.Count != 0)
                {
                    string query = "Truncate table iproddtl";
                    SqlConnection sqlConn = new SqlConnection(con);
                    sqlConn.Open();
                    SqlCommand cmd = new SqlCommand(query, sqlConn);
                    SqlDataAdapter da = new SqlDataAdapter();

                    cmd.CommandText = query;
                    cmd.Connection = sqlConn;
                    da.SelectCommand = cmd;
                    result = cmd.ExecuteNonQuery();
                    sqlConn.Close();

                    using (SqlConnection conn = new SqlConnection(con))
                    {
                        conn.Open();
                        using (SqlBulkCopy s = new SqlBulkCopy(conn))
                        {
                            s.DestinationTableName = "dbo.iproddtl";
                            foreach (DataColumn col in dt.Columns)
                            {
                                s.ColumnMappings.Add(col.ColumnName, col.ColumnName);
                            }
                            s.WriteToServer(dt);
                        }
                    }
                }
                else
                {
                    using (SqlConnection conn = new SqlConnection(con))
                    {
                        conn.Open();
                        using (SqlBulkCopy s = new SqlBulkCopy(conn))
                        {
                            s.DestinationTableName = "dbo.iproddtl";
                            foreach (DataColumn col in dt.Columns)
                            {
                                s.ColumnMappings.Add(col.ColumnName, col.ColumnName);
                            }
                            s.WriteToServer(dt);
                        }
                    }
                }
            }
        }
    }
}