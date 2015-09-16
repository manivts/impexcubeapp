// -----------------------------------------------------------------------
// <copyright file="JobSyncDL.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace VTS.ImpexCube.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Configuration;
    using System.Data;
    using MySql;
    using MySql.Data.MySqlClient;
    using System.Data.SqlClient;
    using System.Data.SqlTypes;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class JobSyncDL
    {
        string con = (string)ConfigurationManager.ConnectionStrings["Constr"].ConnectionString;
        string strconn = (string)ConfigurationManager.AppSettings["ConnectionVisual"];
        string strconnJSU = (string)ConfigurationManager.AppSettings["connectionJSU"];

        public DataSet GetJobDetails(string jobno)
        {
            DataSet ds = new DataSet();
            try
            {
                MySqlConnection con = new MySqlConnection(strconn);
                con.Open();
                string Query = "Select i.JOB_NO as JobNo,i.PARTY_CODE as PartyCode,p.PARTY_NAME as PartyName, i.TRANSPORT_MODE as Mode ,i.DOC_RECD as JobReceived,i.BE_TYPE as BEType ,i.INV_DTL as InvoiceDetails from iworkreg i left join prt_mast p  on i.PARTY_CODE=p.PARTY_CODE where i.JOB_NO like '%" + jobno + "%' ";
                MySqlDataAdapter sd = new MySqlDataAdapter(Query, con);
                sd.Fill(ds, "data");
                con.Close();
            }
            catch (Exception ex)
            {
                string Msg = ex.Message.ToString();
            }
            return ds;
        }

        public DataSet GetIworkDetails(string jobno)
        {
            DataSet ds = new DataSet();
            try
            {
                MySqlConnection con = new MySqlConnection(strconnJSU);
                con.Open();
                string Query = "SELECT job_no,job_date, comp_jobStage," +
                                  "comp_jobDate,comp_remark,pend_jobstage, pend_remark," +
                                  "PARTY_CODE,jobsno, INV_DTL, cont_orig,ETA," +
                                  "MAWB_NO, MAWB_DATE, NO_OF_PKG, PKG_UNIT," +
                                  "GROSS_WT, GROSS_UNIT, Carrier," +
                                  "BE_NO, BE_DATE, PARTY_NAME,Status_job,transport_mode,bill_no,org_doc_date,bill_date " +
                                  "FROM iworkreg_jobstatus where job_no like '%" + jobno +"%' ";
                MySqlDataAdapter sd = new MySqlDataAdapter(Query, con);
                sd.Fill(ds, "data");
                con.Close();
            }
            catch (Exception ex)
            {
                string Msg = ex.Message.ToString();
            }
            return ds;
        }

        public DataSet GetIShipDetails(string jobno)
        {
            DataSet ds = new DataSet();
            try
            {
                MySqlConnection con = new MySqlConnection(strconnJSU);
                con.Open();
                string Query = "SELECT * FROM ishp_dtl where job_no like '%" + jobno + "%' ";
                                  
                                  
                MySqlDataAdapter sd = new MySqlDataAdapter(Query, con);
                sd.Fill(ds, "data");
                con.Close();
            }
            catch (Exception ex)
            {
                string Msg = ex.Message.ToString();
            }
            return ds;
        }

        public DataSet GetIJobPosDetails(string jobno)
        {
            DataSet ds = new DataSet();
            try
            {
                MySqlConnection con = new MySqlConnection(strconnJSU);
                con.Open();
                string Query = "SELECT * FROM ijob_pos where job_no like '%" + jobno + "%' ";


                MySqlDataAdapter sd = new MySqlDataAdapter(Query, con);
                sd.Fill(ds, "data");
                con.Close();
            }
            catch (Exception ex)
            {
                string Msg = ex.Message.ToString();
            }
            return ds;
        }

        public DataSet GetPartyDetails()
        {
            DataSet ds = new DataSet();
            try
            {
                MySqlConnection con = new MySqlConnection(strconnJSU);
                con.Open();
                string Query = "SELECT * FROM prt_mast";


                MySqlDataAdapter sd = new MySqlDataAdapter(Query, con);
                sd.Fill(ds, "data");
                con.Close();
            }
            catch (Exception ex)
            {
                string Msg = ex.Message.ToString();
            }
            return ds;
        }

        public DataSet GetIWorkRegDetails(string FYear)
        {
            DataSet ds = new DataSet();
            try
            {
                MySqlConnection con = new MySqlConnection(strconnJSU);
                con.Open();
                string Query = "SELECT * FROM iworkreg where Job_No like '%" + FYear + "%' ";


                MySqlDataAdapter sd = new MySqlDataAdapter(Query, con);
                sd.Fill(ds, "data");
                con.Close();
            }
            catch (Exception ex)
            {
                string Msg = ex.Message.ToString();
            }
            return ds;
        }

        public DataSet SelectJobNo(string jobno)
        {
            DataSet ds = new DataSet();
            try
            {
                string Query = "Select JobNo from T_JobDetails where JobNo = '" + jobno + "' ";
                SqlConnection sqlConn = new SqlConnection(con);
                sqlConn.Open();
                SqlDataAdapter da = new SqlDataAdapter(Query, sqlConn);

                da.Fill(ds, "data");
                sqlConn.Close();
            }
            catch (Exception ex)
            {
                string Msg = ex.Message.ToString();
            }
            return ds;
        }

        public DataSet SelectIworkJobNo(string jobno)
        {
            DataSet ds = new DataSet();
            try
            {
                string Query = "Select Job_No from iworkreg_jobstatus where Job_No = '" + jobno + "' ";
                SqlConnection sqlConn = new SqlConnection(con);
                sqlConn.Open();
                SqlDataAdapter da = new SqlDataAdapter(Query, sqlConn);

                da.Fill(ds, "data");
                sqlConn.Close();
            }
            catch (Exception ex)
            {
                string Msg = ex.Message.ToString();
            }
            return ds;
        }

        public DataSet SelectpartyCode(string PARTY_CODE)
        {
            DataSet ds = new DataSet();
            try
            {
                string Query = "Select * from prt_mast where PARTY_CODE = '" + PARTY_CODE + "' ";
                SqlConnection sqlConn = new SqlConnection(con);
                sqlConn.Open();
                SqlDataAdapter da = new SqlDataAdapter(Query, sqlConn);

                da.Fill(ds, "data");
                sqlConn.Close();
            }
            catch (Exception ex)
            {
                string Msg = ex.Message.ToString();
            }
            return ds;
        }

        public DataSet SelectIworkReg(string Job_no)
        {
            DataSet ds = new DataSet();
            try
            {
                string Query = "Select * from iworkreg_jobstatus where Job_no = '" + Job_no + "' ";
                SqlConnection sqlConn = new SqlConnection(con);
                sqlConn.Open();
                SqlDataAdapter da = new SqlDataAdapter(Query, sqlConn);

                da.Fill(ds, "data");
                sqlConn.Close();
            }
            catch (Exception ex)
            {
                string Msg = ex.Message.ToString();
            }
            return ds;
        }

        public int InsertJobDetails(string jobno, string JobReceived, string partycode, string partyname, string Mode,
            string invdtl, string betype, string createdby, string createddate, string modifiedby, string modifieddate)
        {
            int result = new int();
            string query = string.Empty;
            try
            {
                if (JobReceived == "" || JobReceived == null)
                {
                    query = "Insert into T_JobDetails(JobNo,PartyCode,PartyName,ShipmentType,InvoiceDetails,BEType,CreatedBy,CreatedDate,ModifiedBy,ModifiedDate)" +
                    "Values('" + jobno + "','" + partycode + "','" + partyname + "','" + Mode + "','" + invdtl + "'," +
                    " '" + betype + "' ,'" + createdby + "', '" + frmdatesplit(createddate) + "','" + modifiedby + "', '" + frmdatesplit(modifieddate) + "')";

                }
                else
                {
                    query = "Insert into T_JobDetails(JobNo,JobReceivedDate,PartyCode,PartyName,ShipmentType,InvoiceDetails,BEType,CreatedBy,CreatedDate,ModifiedBy,ModifiedDate)" +
                        "Values('" + jobno + "','" + frmdatesplit(JobReceived) + "','" + partycode + "','" + partyname + "','" + Mode + "','" + invdtl + "'," +
                        " '" + betype + "' ,'" + createdby + "', '" + frmdatesplit(createddate) + "','" + modifiedby + "', '" + frmdatesplit(modifieddate) + "')";
                }

                SqlConnection sqlConn = new SqlConnection(con);
                sqlConn.Open();
                SqlCommand cmd = new SqlCommand(query, sqlConn);
                SqlDataAdapter da = new SqlDataAdapter();

                cmd.CommandText = query;
                cmd.Connection = sqlConn;
                da.SelectCommand = cmd;
                result = cmd.ExecuteNonQuery();
                sqlConn.Close();
            }
            catch (Exception ex)
            {
                string msg = ex.Message.ToString();
            }
            return result;
        }

        public int InsertIworkDetails(string job_no, string job_date, string comp_jobStage, string comp_jobDate, string comp_remark,string pend_jobStage, string pend_remark,
            string party_code, string jobsno, string INV_DTL, string cont_orig, string ETA, string MAWB_NO, string MAWB_DATE, string NO_OF_PKG,
            string PKG_UNIT, string GROSS_WT, string GROSS_UNIT, string Carrier, string BE_NO, string BE_DATE, string PARTY_NAME, string Status_job, string transport_mode, string bill_no, string org_doc_date, string bill_date)
        {
            int result = new int();
            string query = string.Empty;
         
            try
            {
                if (bill_date != "" )
                {
                    query = "Insert into iworkreg_jobstatus(job_no,job_date,comp_jobStage,comp_jobDate,comp_remark,pend_jobStage,pend_remark,party_code,jobsno,inv_dtl," +
                       "cont_orig,ETA, mawb_no, mawb_date, no_of_pkg, pkg_unit, gross_wt, gross_unit, carrier, be_no, be_date, party_name,Status_job,transport_mode,bill_no,org_doc_date,bill_date)" +
                       "Values('" + job_no + "','" + frmdatesplit(job_date) + "','" + comp_jobStage + "','" + frmdatesplit(comp_jobDate) + "','" + comp_remark + "'," +
                       " '" + pend_jobStage + "','" + pend_remark + "', '" + party_code + "','" + jobsno + "', '" + INV_DTL + "', '" + cont_orig + "', '" + frmdatesplit(ETA) + "'," +
                       " '" + MAWB_NO + "', '" + frmdatesplit(MAWB_DATE) + "', '" + NO_OF_PKG + "','" + PKG_UNIT + "', '" + GROSS_WT + "', '" + GROSS_UNIT + "', " +
                       " '" + Carrier + "' ,'" + BE_NO + "', '" + frmdatesplit(BE_DATE) + "','" + PARTY_NAME + "','" + Status_job + "','" + transport_mode + "','" + bill_no + "','" + frmdatesplit(org_doc_date) + "','" + frmdatesplit(bill_date) + "')";
                }
                else
                {
                    query = "Insert into iworkreg_jobstatus(job_no,job_date,comp_jobStage,comp_jobDate,comp_remark,pend_jobStage,pend_remark,party_code,jobsno,inv_dtl," +
                          "cont_orig,ETA, mawb_no, mawb_date, no_of_pkg, pkg_unit, gross_wt, gross_unit, carrier, be_no, be_date, party_name,Status_job,transport_mode,bill_no,org_doc_date)" +
                          "Values('" + job_no + "','" + frmdatesplit(job_date) + "','" + comp_jobStage + "','" + frmdatesplit(comp_jobDate) + "','" + comp_remark + "'," +
                          " '" + pend_jobStage + "','" + pend_remark + "', '" + party_code + "','" + jobsno + "', '" + INV_DTL + "', '" + cont_orig + "', '" + frmdatesplit(ETA) + "'," +
                          " '" + MAWB_NO + "', '" + frmdatesplit(MAWB_DATE) + "', '" + NO_OF_PKG + "','" + PKG_UNIT + "', '" + GROSS_WT + "', '" + GROSS_UNIT + "', " +
                          " '" + Carrier + "' ,'" + BE_NO + "', '" + frmdatesplit(BE_DATE) + "','" + PARTY_NAME + "','" + Status_job + "','" + transport_mode + "','" + bill_no + "','" + frmdatesplit(org_doc_date) + "')";
                }
             
                SqlConnection sqlConn = new SqlConnection(con);
                sqlConn.Open();
                SqlCommand cmd = new SqlCommand(query, sqlConn);
                SqlDataAdapter da = new SqlDataAdapter();

                cmd.CommandText = query;
                cmd.Connection = sqlConn;
                da.SelectCommand = cmd;
                result = cmd.ExecuteNonQuery();
                sqlConn.Close();
            }
            catch (Exception ex)
            {
                string msg = ex.Message.ToString();
            }
            return result;
        }

        public int UpdateIworkDetails(string job_no, string job_date, string comp_jobStage, string comp_jobDate, string comp_remark, string pend_jobStage, string pend_remark,
         string party_code, string jobsno, string INV_DTL, string cont_orig, string ETA, string MAWB_NO, string MAWB_DATE, string NO_OF_PKG,
         string PKG_UNIT, string GROSS_WT, string GROSS_UNIT, string Carrier, string BE_NO, string BE_DATE, string PARTY_NAME, string Status_job, string transport_mode, string bill_no, string org_doc_date,string bill_date)
        {
            int result = new int();
            string query = string.Empty;
            try
            {
                if (bill_date != "" )
                {
                    query = "update iworkreg_jobstatus set [job_no]='" + job_no + "',[job_date]='" + frmdatesplit(job_date) + "',[comp_jobStage]='" + comp_jobStage + "',comp_jobDate= '" + comp_jobDate + "',comp_remark='" + comp_remark + "', " +
                            "pend_jobStage='" + pend_jobStage + "',pend_remark='" + pend_remark + "',party_code='" + party_code + "',jobsno='" + jobsno + "',INV_DTL='" + INV_DTL + "',cont_orig='" + cont_orig + "',ETA='" + ETA + "',MAWB_NO='" + MAWB_NO + "', " +
                            " MAWB_DATE='" + MAWB_DATE + "',NO_OF_PKG='" + NO_OF_PKG + "',PKG_UNIT='" + PKG_UNIT + "',GROSS_WT='" + GROSS_WT + "',GROSS_UNIT='" + GROSS_UNIT + "',Carrier='" + Carrier + "',BE_NO='" + BE_NO + "', " +
                            " BE_DATE='" + BE_DATE + "',PARTY_NAME='" + PARTY_NAME + "',Status_job='" + Status_job + "',transport_mode='" + transport_mode + "',bill_no='" + bill_no + "',org_doc_date='" + org_doc_date + "',bill_date='" + frmdatesplit(bill_date) + "' where job_no ='" + job_no + "' ";
                }
                else
                {
                    query = "update iworkreg_jobstatus set [job_no]='" + job_no + "',[job_date]='" + frmdatesplit(job_date) + "',[comp_jobStage]='" + comp_jobStage + "',comp_jobDate= '" + comp_jobDate + "',comp_remark='" + comp_remark + "', " +
                               "pend_jobStage='" + pend_jobStage + "',pend_remark='" + pend_remark + "',party_code='" + party_code + "',jobsno='" + jobsno + "',INV_DTL='" + INV_DTL + "',cont_orig='" + cont_orig + "',ETA='" + ETA + "',MAWB_NO='" + MAWB_NO + "', " +
                               " MAWB_DATE='" + MAWB_DATE + "',NO_OF_PKG='" + NO_OF_PKG + "',PKG_UNIT='" + PKG_UNIT + "',GROSS_WT='" + GROSS_WT + "',GROSS_UNIT='" + GROSS_UNIT + "',Carrier='" + Carrier + "',BE_NO='" + BE_NO + "', " +
                               " BE_DATE='" + BE_DATE + "',PARTY_NAME='" + PARTY_NAME + "',Status_job='" + Status_job + "',transport_mode='" + transport_mode + "',bill_no='" + bill_no + "',org_doc_date='" + org_doc_date + "' where job_no ='" + job_no + "' ";
                }
                SqlConnection sqlConn = new SqlConnection(con);
                sqlConn.Open();
                SqlCommand cmd = new SqlCommand(query, sqlConn);
                SqlDataAdapter da = new SqlDataAdapter();

                cmd.CommandText = query;
                cmd.Connection = sqlConn;
                da.SelectCommand = cmd;
                result = cmd.ExecuteNonQuery();
                sqlConn.Close();
            }
            catch (Exception ex)
            {
                string msg = ex.Message.ToString();
            }
            return result;
        }

      

        public int UpdateIShipDetails(string hawb_no, string hawb_date, string job_no, string GLD)
        {
            int result = new int();
            string query = string.Empty;
            try
            {

                query = "update iworkreg_jobstatus set hawb_no='" + hawb_no + "' , hawb_date='" + frmdatesplit(hawb_date) + "',GLD='"+frmdatesplit(GLD)+"' where job_no ='" + job_no + "'";

                SqlConnection sqlConn = new SqlConnection(con);
                sqlConn.Open();
                SqlCommand cmd = new SqlCommand(query, sqlConn);
                SqlDataAdapter da = new SqlDataAdapter();

                cmd.CommandText = query;
                cmd.Connection = sqlConn;
                da.SelectCommand = cmd;
                result = cmd.ExecuteNonQuery();
                sqlConn.Close();
            }
            catch (Exception ex)
            {
                string msg = ex.Message.ToString();
            }
            return result;
        }

        public int UpdateIJobPosDetails(string DB_NOTE_NO, string DB_DATE, string job_no)
        {
            int result = new int();
            string query = string.Empty;
            try
            {

                query = "update iworkreg_jobstatus set DB_NOTE_NO='" + DB_NOTE_NO + "' , DB_DATE='" + frmdatesplit(DB_DATE) + "' where job_no ='" + job_no + "'";

                SqlConnection sqlConn = new SqlConnection(con);
                sqlConn.Open();
                SqlCommand cmd = new SqlCommand(query, sqlConn);
                SqlDataAdapter da = new SqlDataAdapter();

                cmd.CommandText = query;
                cmd.Connection = sqlConn;
                da.SelectCommand = cmd;
                result = cmd.ExecuteNonQuery();
                sqlConn.Close();
            }
            catch (Exception ex)
            {
                string msg = ex.Message.ToString();
            }
            return result;
        }

        public int UpdatePartyName(string PARTY_CODE, string PARTY_NAME, string GROUP_ID)
        {
            int result = new int();
            string query = string.Empty;
            try
            {

                query = "update prt_mast set PARTY_CODE='" + PARTY_CODE + "' , PARTY_NAME='" + PARTY_NAME + "' where PARTY_CODE ='" + PARTY_CODE + "'";

                SqlConnection sqlConn = new SqlConnection(con);
                sqlConn.Open();
                SqlCommand cmd = new SqlCommand(query, sqlConn);
                SqlDataAdapter da = new SqlDataAdapter();

                cmd.CommandText = query;
                cmd.Connection = sqlConn;
                da.SelectCommand = cmd;
                result = cmd.ExecuteNonQuery();
                sqlConn.Close();
            }
            catch (Exception ex)
            {
                string msg = ex.Message.ToString();
            }
            return result;
        }

        public int UpdateIWorkReg(string JOB_NO, string BE_TYPE, string TOT_ASS_VL, string TOT_DUTY, string doc_received_date)
        {
            int result = new int();
            string query = string.Empty;
            try
            {

                query = "update iworkreg_jobstatus set  BE_TYPE='" + BE_TYPE + "',TOT_ASS_VL='" + TOT_ASS_VL + "',TOT_DUTY='" + TOT_DUTY + "',doc_received_date='" + frmdatesplit(doc_received_date) + "' where JOB_NO ='" + JOB_NO + "'";

                SqlConnection sqlConn = new SqlConnection(con);
                sqlConn.Open();
                SqlCommand cmd = new SqlCommand(query, sqlConn);
                SqlDataAdapter da = new SqlDataAdapter();

                cmd.CommandText = query;
                cmd.Connection = sqlConn;
                da.SelectCommand = cmd;
                result = cmd.ExecuteNonQuery();
                sqlConn.Close();
            }
            catch (Exception ex)
            {
                string msg = ex.Message.ToString();
            }
            return result;
        }

        public int InsertPartyName(string PARTY_CODE, string PARTY_NAME, string GROUP_ID)
        {
            int result = new int();
            string query = string.Empty;
            try
            {

                query = "insert into prt_mast (PARTY_CODE,PARTY_NAME,GROUP_ID)values('" + PARTY_CODE + "','" + PARTY_NAME + "','" + GROUP_ID + "')";

                SqlConnection sqlConn = new SqlConnection(con);
                sqlConn.Open();
                SqlCommand cmd = new SqlCommand(query, sqlConn);
                SqlDataAdapter da = new SqlDataAdapter();

                cmd.CommandText = query;
                cmd.Connection = sqlConn;
                da.SelectCommand = cmd;
                result = cmd.ExecuteNonQuery();
                sqlConn.Close();
            }
            catch (Exception ex)
            {
                string msg = ex.Message.ToString();
            }
            return result;
        }

   

        private string frmdatesplit(string frmdate)
        {
            string[] frmdate1;
            string frmdate2=string.Empty;
          
            if (frmdate != "")
            {
                frmdate1 = frmdate.Split('/');
                frmdate2 = frmdate1[2] + '/' + frmdate1[1] + '/' + frmdate1[0];
         
            }
            else
            {
                frmdate2 = null;
            
            }

            return frmdate2;
        }

        public int UpdateJobDetails(string jobno, string JobReceived, string partycode, string partyname, string Mode,
            string invdtl, string betype, string modifiedby, string modifieddate)
        {
            int result = new int();
            string query = string.Empty;
            try
            {
                if (JobReceived == "" || JobReceived == null)
                {
                    query = "Update T_JobDetails Set JobNo='" + jobno + "',PartyCode='" + partycode + "',PartyName='" + partyname + "'," +
                 " ShipmentType='" + Mode + "',InvoiceDetails='" + invdtl + "',BEType='" + betype + "',ModifiedBy='" + modifiedby + "',ModifiedDate='" + frmdatesplit(modifieddate) + "' where  JobNo = '" + jobno + "'";
                }
                else
                {
                    query = "Update T_JobDetails Set JobNo='" + jobno + "',JobReceivedDate='" + frmdatesplit(JobReceived) + "',PartyCode='" + partycode + "',PartyName='" + partyname + "'," +
                       " ShipmentType='" + Mode + "',InvoiceDetails='" + invdtl + "',BEType='" + betype + "',ModifiedBy='" + modifiedby + "',ModifiedDate='" + frmdatesplit(modifieddate) + "' where  JobNo = '" + jobno + "'";
                }

                SqlConnection sqlConn = new SqlConnection(con);
                sqlConn.Open();
                SqlCommand cmd = new SqlCommand(query, sqlConn);
                SqlDataAdapter da = new SqlDataAdapter();

                cmd.CommandText = query;
                cmd.Connection = sqlConn;
                da.SelectCommand = cmd;
                result = cmd.ExecuteNonQuery();
                sqlConn.Close();
            }
            catch (Exception ex)
            {
                string msg = ex.Message.ToString();
            }
            return result;
        }

        public int UpdateJobStatus(string jobno, string Status_Job)
        {
            int result = new int();
            string query = string.Empty;
            try
            {

                query = "Update T_JobDetails Set Status_Job='" + Status_Job + "' where  JobNo = '" + jobno + "'";
               

                SqlConnection sqlConn = new SqlConnection(con);
                sqlConn.Open();
                SqlCommand cmd = new SqlCommand(query, sqlConn);
                SqlDataAdapter da = new SqlDataAdapter();

                cmd.CommandText = query;
                cmd.Connection = sqlConn;
                da.SelectCommand = cmd;
                result = cmd.ExecuteNonQuery();
                sqlConn.Close();
            }
            catch (Exception ex)
            {
                string msg = ex.Message.ToString();
            }
            return result;
        }

        public DataSet GetIPurchaseDetails(string FYear)
        {
            DataSet ds = new DataSet();
            try
            {
                MySqlConnection con = new MySqlConnection(strconn);
                con.Open();
                string Query = "SELECT * FROM iproddtl i ,iinv_dtl j " +
                     "where  i.job_no=j.job_no and i.inv_id=j.inv_id and i.job_no like '%" + FYear + "%' order by i.inv_id,i.prod_sn ";

                MySqlDataAdapter sd = new MySqlDataAdapter(Query, con);
                sd.Fill(ds, "data");
                con.Close();
            }
            catch (Exception ex)
            {
                string Msg = ex.Message.ToString();
            }
            return ds;
        }

        public DataSet GetIProductDetails(string FYear)
        {
            DataSet ds = new DataSet();
            try
            {
                MySqlConnection con = new MySqlConnection(strconn);
                con.Open();
                string Query = "SELECT * FROM iproddtl where job_no like '%" + FYear + "%' ";

                MySqlDataAdapter sd = new MySqlDataAdapter(Query, con);
                sd.Fill(ds, "data");
                con.Close();
            }
            catch (Exception ex)
            {
                string Msg = ex.Message.ToString();
            }
            return ds;
        }

        public DataSet GetIInvoiceDetails(string FYear)
        {
            DataSet ds = new DataSet();
            try
            {
                MySqlConnection con = new MySqlConnection(strconn);
                con.Open();
                string Query = "SELECT * FROM iinv_dtl where job_no like '%" + FYear + "%' ";

                MySqlDataAdapter sd = new MySqlDataAdapter(Query, con);
                sd.Fill(ds, "data");
                con.Close();
            }
            catch (Exception ex)
            {
                string Msg = ex.Message.ToString();
            }
            return ds;
        }

        public DataSet GetSInvoiceDetails()
        {
            DataSet ds = new DataSet();
            try
            {
                string Query = "Select * from iinv_dtl ";
                SqlConnection sqlConn = new SqlConnection(con);
                sqlConn.Open();
                SqlDataAdapter da = new SqlDataAdapter(Query, sqlConn);

                da.Fill(ds, "data");
                sqlConn.Close();
            }
            catch (Exception ex)
            {
                string Msg = ex.Message.ToString();
            }
            return ds;
        }

        public DataSet GetIInvoiceCharges(string FYear)
        {
            DataSet ds = new DataSet();
            try
            {
                MySqlConnection con = new MySqlConnection(strconn);
                con.Open();
                string Query = "SELECT * FROM iinv_chg where job_no like '%" + FYear + "%' ";

                MySqlDataAdapter sd = new MySqlDataAdapter(Query, con);
                sd.Fill(ds, "data");
                con.Close();
            }
            catch (Exception ex)
            {
                string Msg = ex.Message.ToString();
            }
            return ds;
        }

        public DataSet GetSInvoiceCharges()
        {
            DataSet ds = new DataSet();
            try
            {
                string Query = "Select * from iinv_chg ";
                SqlConnection sqlConn = new SqlConnection(con);
                sqlConn.Open();
                SqlDataAdapter da = new SqlDataAdapter(Query, sqlConn);

                da.Fill(ds, "data");
                sqlConn.Close();
            }
            catch (Exception ex)
            {
                string Msg = ex.Message.ToString();
            }
            return ds;
        }

        public DataSet GetSProductDetails()
        {
            DataSet ds = new DataSet();
            try
            {
                string Query = "Select * from iproddtl ";
                SqlConnection sqlConn = new SqlConnection(con);
                sqlConn.Open();
                SqlDataAdapter da = new SqlDataAdapter(Query, sqlConn);

                da.Fill(ds, "data");
                sqlConn.Close();
            }
            catch (Exception ex)
            {
                string Msg = ex.Message.ToString();
            }
            return ds;
        }

        public DataSet SelectProductDetails(string jobNo)
        {
            DataSet ds = new DataSet();
            try
            {
                MySqlConnection con = new MySqlConnection(strconn);
                con.Open();
                string Query = "SELECT JOB_NO FROM iproddtl where job_no = '" + jobNo + "' ";

                MySqlDataAdapter sd = new MySqlDataAdapter(Query, con);
                sd.Fill(ds, "data");
                con.Close();
            }
            catch (Exception ex)
            {
                string Msg = ex.Message.ToString();
            }
            return ds;
        }

        public DataSet SelectInvoiceDetails(string jobNo)
        {
            DataSet ds = new DataSet();
            try
            {
                MySqlConnection con = new MySqlConnection(strconn);
                con.Open();
                string Query = "SELECT JOB_NO FROM iinv_dtl where job_no = '" + jobNo + "' ";

                MySqlDataAdapter sd = new MySqlDataAdapter(Query, con);
                sd.Fill(ds, "data");
                con.Close();
            }
            catch (Exception ex)
            {
                string Msg = ex.Message.ToString();
            }
            return ds;
        }

        public DataSet SelectIPurchaseDetails(string jobno)
        {
            DataSet ds = new DataSet();
            try
            {
                MySqlConnection con = new MySqlConnection(strconn);
                con.Open();
                string Query = "SELECT JOB_NO FROM ipurchase_dtl where Job_No = '" + jobno + "' ";

                MySqlDataAdapter sd = new MySqlDataAdapter(Query, con);
                sd.Fill(ds, "data");
                con.Close();
            }
            catch (Exception ex)
            {
                string Msg = ex.Message.ToString();
            }
            return ds;
        }

    }
}
