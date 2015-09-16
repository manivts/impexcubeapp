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
using System.Data.SqlClient;
using MySql;
using MySql.Data.MySqlClient;
using VTS.ImpexCube.Business;


namespace ImpexCube
{
    public partial class frmSingleJobSync : System.Web.UI.Page
    {
        VTS.ImpexCube.Business.pJobBL ba = new VTS.ImpexCube.Business.pJobBL();
        string strconnJSU = (string)ConfigurationManager.AppSettings["connectionJSU"];
        string strconnVI = (string)ConfigurationManager.AppSettings["ConnectionVisual"];
        string strconnPIPL = (string)ConfigurationManager.AppSettings["ConnectionImpex"];


        VTS.ImpexCube.Business.JobSyncBL objJobSync = new VTS.ImpexCube.Business.JobSyncBL();
        string con = (string)ConfigurationManager.ConnectionStrings["Constr"].ConnectionString;
        String Database = "";


        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack == false)
            {
                string fyear = (string)Session["FYEAR"];
                MySqlConnectionStringBuilder Builder = new MySqlConnectionStringBuilder(strconnVI);
                Database = Builder.Database;
                Session["DBName"] = Database;
              
                string Query = "select * from iworkreg where job_no like '%" + fyear + "%' order by job_no";
                drJOBNO.DataSource = ba.GetJobs(Query, strconnVI);
                drJOBNO.DataTextField = "job_no";
                drJOBNO.DataValueField = "job_no";
                drJOBNO.DataBind();
                drJOBNO.Items.Insert(0, new ListItem("select", "0"));

                tr1.Visible = false;
                tr2.Visible = false;
                trGrid.Visible = false;
                trBtn.Visible = false;

                ld1.Visible = false;
                ld2.Visible = false;
                txtjDate.Visible = false;

              
            }
        }
        public DataSet GetJobStage(string BT, int i)
        {
            MySqlConnection conn = new MySqlConnection(strconnJSU);
            string sqlQuery = "select * from ijobstages where " + BT + "=" + i + " order by sr_no;";
            MySqlDataAdapter da = new MySqlDataAdapter(sqlQuery, conn);
            DataSet ds = new DataSet();
            da.Fill(ds, "ijobstages");
            return ds;
        }
        protected void insertiWorkreg(string fy)
        {

            string sqlQuery = "select * from `" + (string)Session["DBName"] + "`.iworkreg " +
                            "where job_no like '%" + fy + "%' and jobsno > '" + Session["JNO"] + "'";

            MySqlConnection conn = new MySqlConnection(strconnJSU);
            string strQuery = "insert into iworkreg " + sqlQuery + " ";
            MySqlDataAdapter da = new MySqlDataAdapter();
            MySqlCommand cmd = new MySqlCommand(strQuery, conn);
            cmd.CommandText = strQuery;
            cmd.Connection = conn;
            da.SelectCommand = cmd;
            conn.Open();
            int result1 = cmd.ExecuteNonQuery();
            conn.Close();


        }
        protected void GetBeType(string BEName)
        {
            MySqlConnection conn = new MySqlConnection(strconnJSU);
            string sqlQuery = "select * from BEtype where Name='" + BEName + "'";
            MySqlDataAdapter daBEtype = new MySqlDataAdapter(sqlQuery, conn);
            DataSet dsBEtype = new DataSet();
            daBEtype.Fill(dsBEtype, "BEtype");
            if (dsBEtype.Tables["BEtype"].Rows.Count != 0)
            {
                DataRowView rowBEtype = dsBEtype.Tables["BEtype"].DefaultView[0];
                string BeTypeID = rowBEtype["ID"].ToString();
                Session["BEType"] = BeTypeID;
            }
        }
        protected void GetCommand(string Query)
        {

            MySqlConnection conn = new MySqlConnection(strconnJSU);
            conn.Open();
            MySqlCommand cmd = new MySqlCommand(Query, conn);
            cmd.CommandText = Query;
            cmd.Connection = conn;
            int res = cmd.ExecuteNonQuery();
            conn.Close();
        }
        protected void GetCommandVI(string Query)
        {

            MySqlConnection conn = new MySqlConnection(strconnVI);
            conn.Open();
            MySqlCommand cmd = new MySqlCommand(Query, conn);
            cmd.CommandText = Query;
            cmd.Connection = conn;
            int res = cmd.ExecuteNonQuery();
            conn.Close();
        }
        protected void BtnClose_Click(object sender, EventArgs e)
        {
           
            Response.Redirect("~/PIPL/frmMessagePIPL.aspx", false);
        }
        protected void TmInterval_Tick(object sender, EventArgs e)
        {


            DateTime rightNow = DateTime.Now;

            string time = String.Format("{0:T}", rightNow);
            string dates = System.DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss");
            string sTime = "9:00:00 AM";
            string eTime = "9:00:00 PM";
            DateTime cTime = Convert.ToDateTime(sTime);
            DateTime dTime = Convert.ToDateTime(eTime);
            DateTime SysTime = Convert.ToDateTime(time);
           
            if ((cTime.TimeOfDay.Ticks <= SysTime.TimeOfDay.Ticks) && (dTime.TimeOfDay.Ticks >= SysTime.TimeOfDay.Ticks))
            {
                Label5.Visible = true;
                Label5.Text = time.ToString();
              

            }
            else
            {
                Label5.Visible = false;
              

            }

        }
        protected void BtnSync_Click(object sender, EventArgs e)
        {
            
            string JNO = drJOBNO.SelectedValue;
            string fy = (string)Session["FYEAR"];

            GetJSU(JNO, fy);
            update_docreceived_date(JNO);
            GetBills(JNO);
            update_iworkreg_jobstatus(JNO);
            btnJobSync();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "redirect", "alert('Synced Successfully'); window.location.href='frmSingleJobSync.aspx';", true);
           
        }
        protected void GetJSU(string jno, string fy)
        {

            DataSet ds = ba.GetiWorkreg(jno, strconnVI, fy);
            DataTable dt = ds.Tables[0];
            DataSet dsJSU = ba.GetiWorkreg(jno, strconnJSU, fy);
            if (ds.Tables["iworkreg"].Rows.Count != 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    string jobno = row["job_no"].ToString();
                    string TMode = row["transport_mode"].ToString();
                    string Btype = row["be_type"].ToString();
                    if (jobno != "")
                    {
                        try
                        {
                            //Sync every jobs 
                            SyncJOBS(jobno, TMode);

                            //Sync Job Status
                            SyncJOBS_Status(jobno);


                        }
                        catch (Exception ex)
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('" + ex.Message + "');", true);
                        }
                    }

                }
            }
            else
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Please Give Correct Job No.');", true);

        }
        protected void update_docreceived_date(string JNO)
        {
            string Doc_received = "";
            MySqlConnection connST = new MySqlConnection(strconnJSU);
            connST.Open();
            string QueryST = "select jobsno,job_no,doc_recd from iworkreg where job_no ='" + JNO + "' ";
            MySqlDataAdapter daST = new MySqlDataAdapter(QueryST, connST);
            DataSet dsST = new DataSet();
            daST.Fill(dsST, "docrec");
            connST.Close();
            if (dsST.Tables["docrec"].Rows.Count != 0)
            {
                DataRowView row = dsST.Tables["docrec"].DefaultView[0];
                string jobsno = row["jobsno"].ToString();
                string docreced = row["doc_recd"].ToString();
                if (docreced != string.Empty || docreced != "")
                {
                    Doc_received = Convert.ToDateTime(docreced).ToString("yyyy-MM-dd 00:00:00");
                    QueryST = "update iworkreg_jobstatus set doc_received_date ='" + Doc_received + "',jobsno='" + jobsno + "' where job_no ='" + JNO + "'";
                    GetCommandupdate(QueryST);
                }
            }
        }
        protected void GetBills(string JNO)
        {
            SqlConnection conn = new SqlConnection(strconnPIPL);
            string Query = "";
            string sqlQuery = "select * from iec_invoicenew where jobno='" + JNO + "' ";
            SqlDataAdapter da = new SqlDataAdapter(sqlQuery, conn);
            DataSet ds = new DataSet();
            da.Fill(ds, "IECBILL");
            DataTable dt = ds.Tables[0];
            if (ds.Tables["IECBILL"].Rows.Count != 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    DataRowView row1 = ds.Tables["IECBILL"].DefaultView[0];
                    string jobno = row["jobno"].ToString();
                  

                    Query = "update iworkreg_jobstatus set status_job='Y' where job_no='" + JNO + "'";

                    GetCommandupdate(Query);


                }
            }
        }
        protected void update_iworkreg_jobstatus(string JNO)
        {
            string bill_date = "";
            string be_date = "";
            MySqlConnection connST = new MySqlConnection(strconnJSU);
            connST.Open();
            string QueryST = "select i.job_no,i.transport_mode,i.cont_orig,j.be_no,j.be_date,j.bill_no,j.bill_date,pm.party_name,s.vessel_name,i.party_code from iworkreg i,ijob_pos j,prt_mast pm,ishp_dtl s where i.job_no ='" + JNO + "' and i.job_no=j.job_no and i.job_no=s.job_no and i.party_code=pm.party_code ";
            MySqlDataAdapter daST = new MySqlDataAdapter(QueryST, connST);
            DataSet dsST = new DataSet();
            daST.Fill(dsST, "docrec");
            connST.Close();
            DataTable dt = dsST.Tables[0];
            if (dsST.Tables["docrec"].Rows.Count != 0)
                foreach (DataRow row in dt.Rows)
                {
                    
                    string transport = row["transport_mode"].ToString();
                    string contorig = row["cont_orig"].ToString();
                    string contor = contorig.Replace("'", " ");
                    string billno = row["bill_no"].ToString();
                    string billdate = row["bill_date"].ToString();
                    string partyname = row["party_name"].ToString();
                    string party = partyname.Replace("'", " ");
                    string vesselname = row["vessel_name"].ToString();
                    string vessel = vesselname.Replace("'", " ");
                    string beno = row["be_no"].ToString();
                    string bedate = row["be_date"].ToString();
                    string partycode = row["party_code"].ToString();

                    if (billdate != string.Empty || billdate != "")
                    {
                        bill_date = Convert.ToDateTime(billdate).ToString("yyyy-MM-dd 00:00:00");
                        QueryST = "update iworkreg_jobstatus set vessel_name='" + vessel + "', bill_date ='" + bill_date + "',transport_mode='" + transport + "',cont_orig='" + contor + "',bill_no='" + billno + "',party_name='" + party + "',party_code='" + partycode + "' where job_no ='" + JNO + "'";
                        GetCommandupdate(QueryST);
                    }
                    if (bedate != string.Empty || bedate != "")
                    {
                        be_date = Convert.ToDateTime(bedate).ToString("yyyy-MM-dd 00:00:00");
                        QueryST = "update iworkreg_jobstatus set vessel_name='" + vessel + "', be_date ='" + be_date + "',transport_mode='" + transport + "',cont_orig='" + contor + "',be_no='" + beno + "',party_name='" + party + "',party_code='" + partycode + "' where job_no ='" + JNO + "'";
                        GetCommandupdate(QueryST);
                    }
                    else
                    {
                        QueryST = "update iworkreg_jobstatus set vessel_name='" + vessel + "', transport_mode='" + transport + "',cont_orig='" + contor + "',be_no='" + beno + "',bill_no='" + billno + "',party_name='" + party + "',party_code='" + partycode + "' where job_no ='" + JNO + "'";
                        GetCommandupdate(QueryST);
                    }
                }
        }
        protected void GetCommandupdate(string sqlQuery)
        {
            MySqlConnection conn = new MySqlConnection(strconnJSU);
            conn.Open();
            MySqlDataAdapter da1 = new MySqlDataAdapter();
            MySqlCommand cmd = new MySqlCommand(sqlQuery, conn);
            cmd.CommandText = sqlQuery;
            cmd.Connection = conn;
            da1.SelectCommand = cmd;

            int result1 = cmd.ExecuteNonQuery();
            conn.Close();
            conn.Dispose();

        }
        protected void SyncJOBS(string jobno, string tmode)
        {
            string Query = "select * from iworkreg where job_no like '%" + jobno + "%'";
            DataSet dsiW = ba.GetJobs(Query, strconnVI);
            if (dsiW.Tables["jobs"].Rows.Count != 0)
            {
                DataRowView row = dsiW.Tables["jobs"].DefaultView[0];
                string jdate = row["doc_recd"].ToString();
                string pcode = row["party_code"].ToString();
                string DOC_DATE = Convert.ToDateTime(jdate).ToString("yyyy-MM-dd 00:00:00");

                DataSet dsiwJSU = ba.GetJobs(Query, strconnJSU);
                if (dsiwJSU.Tables["jobs"].Rows.Count == 0)
                {
                    string QueryST = "insert into iworkreg_jobstatus(job_no,job_date,party_code) values('" + jobno + "','" + DOC_DATE + "','" + pcode + "')";
                    int js = ba.SyncJobs(QueryST);

                    //1.iworkreg table
                    string sqlQuery = "select * from `" + (string)Session["DBName"] + "`.iworkreg " +
                             "where job_no like '%" + jobno + "%'";
                    string strQuery = "insert into iworkreg " + sqlQuery + " ";
                    int resWorkreg = ba.SyncJobs(strQuery);

                    //2.ishp_dtl table
                    sqlQuery = "select * from `" + (string)Session["DBName"] + "`.ishp_dtl " +
                             "where job_no like '%" + jobno + "%'";
                    strQuery = "insert into ishp_dtl " + sqlQuery + " ";
                    int resShp = ba.SyncJobs(strQuery);

                    //3.ijob_pos table
                    sqlQuery = "select * from `" + (string)Session["DBName"] + "`.ijob_pos " +
                             "where job_no like '%" + jobno + "%'";
                    strQuery = "insert into ijob_pos " + sqlQuery + " ";
                    int resjobPos = ba.SyncJobs(strQuery);

                    //4.iinv_dtl table
                    sqlQuery = "select * from `" + (string)Session["DBName"] + "`.iinv_dtl " +
                             "where job_no like '%" + jobno + "%'";
                    strQuery = "insert into iinv_dtl " + sqlQuery + " ";
                    int resinv = ba.SyncJobs(strQuery);

                    if (tmode == "S")
                    {
                        //5.iinv_dtl table
                        sqlQuery = "select * from `" + (string)Session["DBName"] + "`.impcontdet " +
                                 "where job_no like '%" + jobno + "%'";
                        strQuery = "insert into impcontdet " + sqlQuery + " ";
                        int resimpcont = ba.SyncJobs(strQuery);
                    }


                }
               
            }
        }
        protected void SyncJOBS_Status(string jobno)
        {
            GetWorkBlok(jobno);
            string blk = Session["blk"].ToString();
            decimal expe = 0;

           
            string Query = "select i.job_stage,i.date,i.remark,i.person,j.sr_no from impjobstage i,ijobstages j where i.job_stage=j.stageid " +
                           "and i.job_no like '%" + jobno + "' order by j.sr_no ";
            string Query1 = "select * from iworkreg_dtl i,ijobstages j where i.job_stage=j.stageid " +
                           "and i.job_no like '%" + jobno + "' order by j.sr_no ";

       

            MySqlConnection con1 = new MySqlConnection(strconnVI);

            MySqlDataAdapter da1 = new MySqlDataAdapter(Query, con1);
            DataSet ds1 = new DataSet();
            da1.Fill(ds1, "jobs");

            DataSet dsiwJSU = ba.GetJobs(Query1, strconnJSU);
            if (ds1.Tables["jobs"].Rows.Count != 0)
            {
                DataTable dt = ds1.Tables[0];
                foreach (DataRow row in dt.Rows)
                {
                    string stageId = row["job_stage"].ToString();
                    string jobDate = row["date"].ToString();
                    string remark = row["remark"].ToString();
                    string person = row["person"].ToString();
                    string sno = row["sr_no"].ToString();
                    string sqlQuery1 = "";
                    remark = remark.Replace("'", " ");
                    if (dsiwJSU.Tables["jobs"].Rows.Count == 0)
                    {
                        if (stageId == "DRDATE000E")
                        {
                            sno = "1";
                            string DOC_DATE = Convert.ToDateTime(jobDate).ToString("yyyy-MM-dd 00:00:00");
                            sqlQuery1 = "Insert into iworkreg_dtl (job_no,job_stage,date,remark,person,wrkblk,Expense,sno)" +
                                              " values ('" + jobno + "','" + stageId + "','" + DOC_DATE + "','" + remark + "','" + person + "','" + blk + "'," + expe + "," + sno + ")";
                        }
                        else
                        {
                            if (jobDate == "" || jobDate == string.Empty)
                            {
                                sqlQuery1 = "Insert into iworkreg_dtl (job_no,job_stage,remark,person,wrkblk,Expense,sno)" +
                                                  " values ('" + jobno + "','" + stageId + "','" + remark + "','" + person + "','" + blk + "'," + expe + "," + sno + ")";
                            }
                            else
                            {
                                string DOC_DATE = Convert.ToDateTime(jobDate).ToString("yyyy-MM-dd 00:00:00");
                                sqlQuery1 = "Insert into iworkreg_dtl (job_no,job_stage,date,wrkblk,Expense,sno)" +
                                            " values ('" + jobno + "','" + stageId + "','" + DOC_DATE + "','" + blk + "'," + expe + "," + sno + ")";

                            }

                        }
                    }
                    else
                    {
                        if (stageId == "DRDATE000E")
                        {
                            sno = "1";
                            string DOC_DATE = Convert.ToDateTime(jobDate).ToString("yyyy-MM-dd 00:00:00");
                            sqlQuery1 = "update iworkreg_dtl set date='" + DOC_DATE + "',remark='" + remark + "',person='" + person + "',job_status='" + sno + "'" +
                                        " where job_no='" + jobno + "' and job_stage='" + stageId + "'";
                        }
                        else
                        {
                            if (jobDate == "" || jobDate == string.Empty)
                            {
                                sno = "0";
                                sqlQuery1 = "update iworkreg_dtl set remark='" + remark + "',person='" + person + "',job_status='" + sno + "'" +
                                            " where job_no='" + jobno + "' and job_stage='" + stageId + "'";
                            }
                            else
                            {
                                sno = "1";
                                string DOC_DATE = Convert.ToDateTime(jobDate).ToString("yyyy-MM-dd 00:00:00");
                                sqlQuery1 = "update iworkreg_dtl set date='" + DOC_DATE + "',remark='" + remark + "',person='" + person + "',job_status='" + sno + "'" +
                                            " where job_no='" + jobno + "' and job_stage='" + stageId + "'";

                            }

                        }
                    }
                    int resiwork = ba.SyncJobs(sqlQuery1);
                  
                }
            }
        }
        protected void GetWorkBlok(string jno)
        {
            string blk = "";
            string Fyear = jno.Substring(10, 9);
            if (Fyear == "2006-2007")
            {
                blk = "WP000001";
            }
            else if (Fyear == "2007-2008")
            {
                blk = "WP000002";
            }
            else if (Fyear == "2008-2009")
            {
                blk = "WP000003";
            }
            else if (Fyear == "2009-2010")
            {
                blk = "WP000004";
            }
            else if (Fyear == "2011-2012")
            {
                blk = "WP000006";
            }
            else if (Fyear == "2012-2013")
            {
                blk = "WP000007";
            }
            else if (Fyear == "2013-2014")
            {
                blk = "WP000008";
            }
            else
            {
                blk = "WP000005";
            }
            Session["blk"] = blk;
        }






        protected void btnJobSync()
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

                    //if (result == 1)
                    //{
                    //    ScriptManager.RegisterStartupScript(this, this.GetType(), "redirect", "alert('Synced Successfully'); window.location.href='frmJobSync.aspx';", true);
                    //}
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
                    cont_orig, ETA, MAWB_NO, MAWB_DATE, NO_OF_PKG, PKG_UNIT, GROSS_WT, GROSS_UNIT, Carrier, BE_NO, BE_DATE, PARTY_NAME, Status_job, transport_mode, bill_no, org_doc_date, bill_date);
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
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('" + ex.Message + "'+'" + iworkregJob + "');", true);
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
                        PARTY_NAME = PARTY_NAME.Replace("'", "").Replace(",", "");

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