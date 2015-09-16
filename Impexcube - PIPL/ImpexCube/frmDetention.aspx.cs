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
    public partial class frmDetention : System.Web.UI.Page
    {
        string strconn1 = (string)ConfigurationManager.AppSettings["ConnectionVisual"];
        string strconn = (string)ConfigurationManager.AppSettings["ConnectionAdmin"];
        string Constr = (string)ConfigurationManager.AppSettings["connectionJSU"];


        DateTime IMG_Date;
        #region
        DateTime BL_Date;
        string Bill;
        string Dly;
        string QE;
        string DPD;
        string COC;
        string QC;
        string FBOE;
        string CDID;
        string CLC;
        string QOO;
        string DRD;
        string DocRD;
        string ODRD;
        string PTCFS;
        string EDOD;
        string ASMT;
        string CDO;
        string EOC;
        string SCN;
        string FAC;
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack == false)
            {
                PLHead.Visible = true;
                PLDET.Visible = false;
                //  BtnPreview.Enabled = false;
            }
        }

        protected void BtnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/PIPL/JobReports/Index.aspx");
        }

        protected void BTNFind_Jobs_Click(object sender, EventArgs e)
        {
            string JNO = txtJobNO.Text;
            string FY = (string)Session["FYear"];

            //string jobno = "IMP/" + JNO + "/" + FY;
            //Session["JOB_NO"] = jobno;
            SqlConnection conn0 = new SqlConnection(strconn);
            string sqlQuery0 = " select * from Detention_mstN where JNo='" + JNO + "' and jobNo like '%" + FY + "%'";

            SqlDataAdapter da0 = new SqlDataAdapter(sqlQuery0, conn0);
            DataSet ds0 = new DataSet();
            da0.Fill(ds0, "iGround");
            if (ds0.Tables["iGround"].Rows.Count != 0)
            {
                //Already entered jobs
                GetDetention(JNO);
                PLDET.Visible = true;
                PLHead.Visible = false;
                BtnSubmit.Enabled = true;
                BtnSubmit.Text = "UPDATE";
                Session["FLAG"] = "1";
            }
            else
            {
                BtnSubmit.Enabled = true;
                BtnSubmit.Text = "Save";
                MySqlConnection conn = new MySqlConnection(strconn1);
                string sqlQuery = " select * from iworkreg where JobsNo='" + JNO + "' and job_no like '%" + FY + "'";

                MySqlDataAdapter da = new MySqlDataAdapter(sqlQuery, conn);
                DataSet ds = new DataSet();
                da.Fill(ds, "iJobs");
                if (ds.Tables["iJobs"].Rows.Count != 0)
                {
                    PLDET.Visible = true;
                    PLHead.Visible = false;
                    DataRowView row = ds.Tables["iJobs"].DefaultView[0];
                    string JOB_NO = row["JOB_NO"].ToString();
                    DateTime JOB_DATE = Convert.ToDateTime(row["Doc_recd"].ToString());
                    Session["JOB_NO"] = JOB_NO;
                    Session["JOB_DATE"] = JOB_DATE.ToString("yyyy-MM-dd");

                    //to get BL No

                    string sqlQuery1 = " select * from ishp_dtl where Job_No='" + JOB_NO + "'";

                    MySqlDataAdapter da1 = new MySqlDataAdapter(sqlQuery1, conn);
                    DataSet ds1 = new DataSet();
                    da1.Fill(ds1, "iBLNO");
                    string BLNO = "";
                    string bldate = "";
                    if (ds1.Tables["iBLNO"].Rows.Count != 0)
                    {
                        DataRowView row1 = ds1.Tables["iBLNO"].DefaultView[0];
                         BLNO = row1["MAWB_NO"].ToString();
                         bldate = row1["MAWB_Date"].ToString();
                         //DateTime BL_Date = Convert.ToDateTime(row1["MAWB_Date"].ToString());
                        string IMGNO = row1["Gateway_IGMNo"].ToString();
                        string ImgDate = row1["Gateway_IGMDate"].ToString();
                        if (ImgDate != "")
                        {
                            IMG_Date = Convert.ToDateTime(ImgDate);
                            txtIGM.Text = IMGNO + " dt." + IMG_Date.ToString("dd/MM/yyyy");
                        }
                        else
                        {
                            txtIGM.Text = "";
                        }
                    }
                    if (bldate != "")
                    {
                        BL_Date = Convert.ToDateTime(bldate);
                    }
                    string sqlQuery2 = " SELECT c.cnsr_name FROM iinv_dtl i,cnsr_mst c " +
                                       " where i.cnsr_code=c.cnsr_code and i.job_no='" + JOB_NO + "'";


                    MySqlDataAdapter da2 = new MySqlDataAdapter(sqlQuery2, conn);
                    DataSet ds2 = new DataSet();
                    da2.Fill(ds2, "iSupplier");
                    if (ds2.Tables["iSupplier"].Rows.Count == 0)
                    {
                        Response.Write("<script>alert('Consigner Name does not update properly... Please check Master Database')</script>");
                        PLDET.Visible = false;
                        PLHead.Visible = true;
                    }
                    else
                    {
                        DataRowView row2 = ds2.Tables["iSupplier"].DefaultView[0];
                        string Consr_Name = row2["cnsr_name"].ToString();
                        string mysqlquery = "select * from ijobstages";
                        MySqlConnection conjob = new MySqlConnection(Constr);
                        MySqlDataAdapter Jda = new MySqlDataAdapter(mysqlquery, conjob);
                        DataSet Jds = new DataSet();
                        Jda.Fill(Jds, "JobStages");
                        //DataRowView jsrow = ds.Tables[0].Rows;
                        foreach (DataRow jsrow in Jds.Tables[0].Rows)
                        {
                            string SrNo = jsrow["Sr_No"].ToString();
                            if (SrNo == "1")
                            {
                                DocRD = jsrow["StageID"].ToString();
                            }
                            if (SrNo == "2")
                            {
                                QOO = jsrow["StageID"].ToString();
                            }
                            if (SrNo == "3")
                            {
                                CLC = jsrow["StageID"].ToString();
                            }
                            if (SrNo == "4")
                            {
                                FBOE = jsrow["StageID"].ToString();
                            }
                            if (SrNo == "5")
                            {
                                QC = jsrow["StageID"].ToString();
                            }
                            if (SrNo == "6")
                            {
                                ASMT = jsrow["StageID"].ToString();
                            }
                            if (SrNo == "7")
                            {
                                CDID = jsrow["StageID"].ToString();
                            }
                            if (SrNo == "8")
                            {
                                PTCFS = jsrow["StageID"].ToString();
                            }
                            if (SrNo == "9")
                            {
                                ODRD = jsrow["StageID"].ToString();
                            }
                            if (SrNo == "10")
                            {
                                DRD = jsrow["StageID"].ToString();
                            }
                            if (SrNo == "11")
                            {
                                DPD = jsrow["StageID"].ToString();
                            }
                            if (SrNo == "12")
                            {
                                CDO = jsrow["StageID"].ToString();
                            }
                            if (SrNo == "13")
                            {
                                QE = jsrow["StageID"].ToString();
                            }
                            if (SrNo == "14")
                            {
                                EOC = jsrow["StageID"].ToString();
                            }
                            if (SrNo == "15")
                            {
                                COC = jsrow["StageID"].ToString();
                            }
                            if (SrNo == "16")
                            {
                                EDOD = jsrow["StageID"].ToString();
                            }
                            if (SrNo == "17")
                            {
                                Dly = jsrow["StageID"].ToString();
                            }
                            if (SrNo == "18")
                            {
                                FAC = jsrow["StageID"].ToString();
                            }
                            if (SrNo == "19")
                            {
                                SCN = jsrow["StageID"].ToString();
                            }
                            if (SrNo == "20")
                            {
                                Bill = jsrow["StageID"].ToString();
                            }
                        }
                        String JS = "DRDATE000E";

                        GetDocumenetReceived(JOB_NO, JS);

                        GetODRD(JOB_NO, ODRD);

                        GetBOE(JOB_NO, FBOE);

                        GetPortCFS(JOB_NO, PTCFS);

                        GetDID(JOB_NO, CDID);

                        GetDRD(JOB_NO, DRD);

                        GetEOC(JOB_NO, EOC);

                        txtRNO.Text = JNO;
                        txtDetails.Text = BLNO + " dt." + BL_Date.ToString("dd/MM/yyyy");
                        txtSupplier.Text = Consr_Name;
                        txtRASD.Text = Session["DOC_DATE"].ToString();
                        txtODOC.Text = Session["ODRD"].ToString();
                        txtMoveCFS.Text = Session["PTCFS"].ToString();


                        DataSet dsDG = new DataSet();
                        dsDG.ReadXml(Server.MapPath("~\\XML\\Detention.xml"));

                        gvDetail.DataSource = dsDG;
                        gvDetail.DataMember = "Detail";
                        gvDetail.DataBind();

                        int i = 0;
                        foreach (GridViewRow Row in gvDetail.Rows)
                        {
                            if (i == 0)
                            {
                                TextBox txtDOC = (TextBox)Row.FindControl("txtRmks");
                                txtDOC.Text = Session["DOC_DATE"].ToString();
                                //Row.Cells[1].Text = Session["DOC_DATE"].ToString();
                            }
                            if (i == 1)
                            {
                                TextBox txtOrDOC = (TextBox)Row.FindControl("txtRmks");
                                txtOrDOC.Text = Session["ODRD"].ToString();
                            }
                            if (i == 2)
                            {
                                //TextBox txtDOC = (TextBox)Row.FindControl("txtRmks");
                                //txtDOC.Text = Session["DOC_DATE"].ToString();
                            }
                            if (i == 3)
                            {
                                TextBox txtDID = (TextBox)Row.FindControl("txtRmks");
                                txtDID.Text = Session["DID"].ToString();
                            }
                            if (i == 4)
                            {
                                TextBox txtDRD = (TextBox)Row.FindControl("txtRmks");
                                txtDRD.Text = Session["DRD"].ToString();
                            }
                            if (i == 5)
                            {
                                TextBox txtEOC = (TextBox)Row.FindControl("txtRmks");
                                txtEOC.Text = Session["EOC"].ToString();
                            }
                            if (i == 6)
                            {
                                //TextBox txtDOC = (TextBox)Row.FindControl("txtRmks");
                                //txtDOC.Text = Session["DOC_DATE"].ToString();
                            }
                            if (i == 7)
                            {
                                //TextBox txtDOC = (TextBox)Row.FindControl("txtRmks");
                                //txtDOC.Text = Session["DOC_DATE"].ToString();
                            }
                            i++;
                        }

                    }
                }
                else
                {
                    Response.Write("<script>alert('Given Number has Not Found... Please Check JOBNO')</script>");

                }
            }
        }

        private void GetEOC(String JNO, String JStage)
        {
            MySqlConnection conn = new MySqlConnection(strconn1);
            string sqlQuery = " select * from impjobstage where Job_No='" + JNO + "' and job_stage='" + JStage + "'";

            MySqlDataAdapter da = new MySqlDataAdapter(sqlQuery, conn);
            DataSet ds = new DataSet();
            da.Fill(ds, "iJobStage");
            if (ds.Tables["iJobStage"].Rows.Count == 0)
            {
                Session["EOC"] = "nill";
            }
            else
            {
                DataRowView row = ds.Tables["iJobStage"].DefaultView[0];
                //DateTime Eoc = Convert.ToDateTime(row["Date"].ToString());
                //Session["EOC"] = Eoc.ToString("dd/MM/yyyy");
                string EOC = row["Date"].ToString();
                if (EOC != "")
                {
                    DateTime Eoc = Convert.ToDateTime(EOC);
                    Session["EOC"] = Eoc.ToString("dd/MM/yyyy");
                }
                else
                {
                    Session["EOC"] = "";
                }
            }
        }

        private void GetDRD(String JNO, String JStage)
        {
            MySqlConnection conn = new MySqlConnection(strconn1);
            string sqlQuery = " select * from impjobstage where Job_No='" + JNO + "' and job_stage='" + JStage + "'";

            MySqlDataAdapter da = new MySqlDataAdapter(sqlQuery, conn);
            DataSet ds = new DataSet();
            da.Fill(ds, "iJobStage");
            if (ds.Tables["iJobStage"].Rows.Count == 0)
            {
                Session["DRD"] = "nill";
            }
            else
            {
                DataRowView row = ds.Tables["iJobStage"].DefaultView[0];
                //DateTime DRD = Convert.ToDateTime(row["Date"].ToString());
                //Session["DRD"] = DRD.ToString("dd/MM/yyyy");
                string DRD = row["Date"].ToString();
                if (DRD != "")
                {
                    DateTime DrD = Convert.ToDateTime(DRD);
                    Session["DRD"] = DrD.ToString("dd/MM/yyyy");
                }
                else
                {
                    Session["DRD"] = "";
                }
            }
        }

        private void GetDID(String JNO, String JStage)
        {
            MySqlConnection conn = new MySqlConnection(strconn1);
            string sqlQuery = " select * from impjobstage where Job_No='" + JNO + "' and job_stage='" + JStage + "'";

            MySqlDataAdapter da = new MySqlDataAdapter(sqlQuery, conn);
            DataSet ds = new DataSet();
            da.Fill(ds, "iJobStage");
            if (ds.Tables["iJobStage"].Rows.Count == 0)
            {
                Session["DID"] = "null";
            }
            else
            {
                DataRowView row = ds.Tables["iJobStage"].DefaultView[0];
                //DateTime DID = Convert.ToDateTime(row["Date"].ToString());
                //Session["DID"] = DID.ToString("dd/MM/yyyy");
                string DID = row["Date"].ToString();
                if (DID != "")
                {
                    DateTime DD = Convert.ToDateTime(DID);
                    Session["DID"] = DD.ToString("dd/MM/yyyy");
                }
                else
                {
                    Session["DID"] = "";
                }
            }
        }

        private void GetBOE(String JNO, String JStage)
        {
            MySqlConnection conn = new MySqlConnection(strconn1);
            string sqlQuery = " select * from impjobstage where Job_No='" + JNO + "' and job_stage='" + JStage + "'";

            MySqlDataAdapter da = new MySqlDataAdapter(sqlQuery, conn);
            DataSet ds = new DataSet();
            da.Fill(ds, "iJobStage");
            if (ds.Tables["iJobStage"].Rows.Count == 0)
            {
                Session["BOE"] = "null";
            }
            else
            {
                DataRowView row = ds.Tables["iJobStage"].DefaultView[0];
                //DateTime BOE = Convert.ToDateTime(row["Date"].ToString());
                //Session["BOE"] = BOE.ToString("dd/MM/yyyy");
                string BOE = row["Date"].ToString();
                if (BOE != "")
                {
                    DateTime BE = Convert.ToDateTime(BOE);
                    Session["BOE"] = BE.ToString("dd/MM/yyyy");
                }
                else
                {
                    Session["BOE"] = "";
                }
            }
        }

        private void GetPortCFS(String JNO, String JStage)
        {
            MySqlConnection conn = new MySqlConnection(strconn1);
            string sqlQuery = " select * from impjobstage where Job_No='" + JNO + "' and job_stage='" + JStage + "'";

            MySqlDataAdapter da = new MySqlDataAdapter(sqlQuery, conn);
            DataSet ds = new DataSet();
            da.Fill(ds, "iJobStage");
            if (ds.Tables["iJobStage"].Rows.Count == 0)
            {
                Session["PTCFS"] = "null";
            }
            else
            {
                DataRowView row = ds.Tables["iJobStage"].DefaultView[0];
                //DateTime CFS = Convert.ToDateTime(row["Date"].ToString());
                //Session["PTCFS"] = CFS.ToString("dd/MM/yyyy");
                string CFS = row["Date"].ToString();
                if (CFS != "")
                {
                    DateTime ptcfs = Convert.ToDateTime(CFS);
                    Session["PTCFS"] = ptcfs.ToString("dd/MM/yyyy");
                }
                else
                {
                    Session["PTCFS"] = "";
                }
            }
        }

        private void GetODRD(String JNO, String JStage)
        {
            MySqlConnection conn = new MySqlConnection(strconn1);
            string sqlQuery = " select * from impjobstage where Job_No='" + JNO + "' and job_stage='" + JStage + "'";

            MySqlDataAdapter da = new MySqlDataAdapter(sqlQuery, conn);
            DataSet ds = new DataSet();
            da.Fill(ds, "iJobStage");
            if (ds.Tables["iJobStage"].Rows.Count == 0)
            {
                Session["ODRD"] = "null";
            }
            else
            {
                DataRowView row = ds.Tables["iJobStage"].DefaultView[0];
                string ORDT = row["Date"].ToString();
                if (ORDT != "")
                {
                    DateTime ODRD = Convert.ToDateTime(ORDT);
                    Session["ODRD"] = ODRD.ToString("dd/MM/yyyy");
                }
                else
                {
                    Session["ODRD"] = "";
                }
            }
        }

        protected void GetDocumenetReceived(String JNO, String JStage)
        {
            MySqlConnection conn = new MySqlConnection(strconn1);
            string sqlQuery = " select * from impjobstage where Job_No='" + JNO + "' and job_stage='" + JStage + "'";

            MySqlDataAdapter da = new MySqlDataAdapter(sqlQuery, conn);
            DataSet ds = new DataSet();
            da.Fill(ds, "iJobStage");
            if (ds.Tables["iJobStage"].Rows.Count == 0)
            {
                Session["DOC_DATE"] = "null";
            }
            else
            {
                DataRowView row = ds.Tables["iJobStage"].DefaultView[0];
                //DateTime DOC_DATE = Convert.ToDateTime(row["Date"].ToString());
                //Session["DOC_DATE"] = DOC_DATE.ToString("dd/MM/yyyy");
                string DOC_DATE = row["Date"].ToString();
                if (DOC_DATE != "")
                {
                    DateTime DocDate = Convert.ToDateTime(DOC_DATE);
                    Session["DOC_DATE"] = DocDate.ToString("dd/MM/yyyy");
                }
                else
                {
                    Session["DOC_DATE"] = "";
                }
            }
        }

        public DataSet GetData(string JNO)
        {
            MySqlConnection conn = new MySqlConnection(strconn1);
            string str = "select j.sr_no,i.job_no,i.job_stage,i.date,i.remark from impjobstage i,ijobstages j " +
                       " where i.job_stage=j.stageid and i.job_no='" + JNO + "' order by j.sr_no";
            MySqlDataAdapter da = new MySqlDataAdapter(str, conn);
            DataSet ds = new DataSet();
            da.Fill(ds, "jobstatus");
            return (ds);
        }

        protected void BtnSubmit_Click(object sender, EventArgs e)
        {

            String jno = txtRNO.Text;
            String jobno = Session["JOB_NO"].ToString();

            String Details = txtDetails.Text;
            String Consr_name = txtSupplier.Text;
            String DOCDATE = txtRASD.Text;
            String ODOC_Date = txtODOC.Text;
            String BOV = txtBVessel.Text;
            String IGM = txtIGM.Text;
            String BOE = txtBOE.Text;
            String GRupto = txtGRent.Text;
            String Detent_Free = txtDetention.Text;
            String CFS = txtMoveCFS.Text;
            String Plant = txtMovePlant.Text;
            String Cont_Return = txtContReturn.Text;
            String BPT = txtBPTDamrage.Text;
            String Detent_charge = txtDEt_charge.Text;
            String Damage_charge = txtDamage_Charge.Text;

            SqlConnection conn0 = new SqlConnection(strconn);
            string sqlQuery0 = " select * from Detention_mstN where JobNo='" + jobno + "'";

            SqlDataAdapter da0 = new SqlDataAdapter(sqlQuery0, conn0);
            DataSet ds0 = new DataSet();
            da0.Fill(ds0, "iJobStage");
            if (ds0.Tables["iJobStage"].Rows.Count != 0)
            {
                UpdateDetention();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Given job:" + jobno + " Updated Successfully ');", true);
                //Response.Write("<script>alert('Given Job:' " + jobno + " 'Updated Successfully')</script>");
            }
            else
            {
                String JobDate = Session["JOB_DATE"].ToString();
                SqlConnection conn = new SqlConnection(strconn);
                string lstrdrp = "insert into Detention_mstN(jno,jobno,jobDate,particulars,supplier,doc_date,Odoc_date," +
                                 "Berth_of_vessel,igm_no,boe_date,Ground_rent_upto,Detention_free,move_port_cfs," +
                                 "move_cfs_plant,empty_cont_return_date,BPT_Damrage,Detention_charge,Damage_charge) " +
                                 "values('" + jno + "','" + jobno + "','" + JobDate + "','" + Details + "','" + Consr_name + "','" + DOCDATE + "'," +
                                 "'" + ODOC_Date + "','" + BOV + "','" + IGM + "','" + BOE + "','" + GRupto + "','" + Detent_Free + "'," +
                                 "'" + CFS + "','" + Plant + "','" + Cont_Return + "','" + BPT + "','" + Detent_charge + "','" + Damage_charge + "')";
                conn.Open();
                SqlDataAdapter dap = new SqlDataAdapter();
                SqlCommand cmdp = new SqlCommand(lstrdrp, conn);
                cmdp.CommandText = lstrdrp;
                cmdp.Connection = conn;
                dap.SelectCommand = cmdp;

                int result = cmdp.ExecuteNonQuery();
                conn.Close();
                int i = 0;
                foreach (GridViewRow Row in gvDetail.Rows)
                {

                    //string add1 = ((System.Web.UI.WebControls.TextBox)(Row.Cells[3].Controls[0])).Text;
                    //TextBox MyTextBox1=Row.Cells[1].Controls[1];
                    // System.Web.UI.WebControls.TextBox myTextBox1 = (System.Web.UI.WebControls.TextBox)(Row.Cells[0].Controls[1]);
                    System.Web.UI.WebControls.TextBox myTextBox2 = (System.Web.UI.WebControls.TextBox)(Row.Cells[1].Controls[1]);
                    // System.Web.UI.WebControls.TextBox myTextBox3 = (System.Web.UI.WebControls.TextBox)(Row.Cells[2].Controls[1]);
                    String Stage = Row.Cells[0].Text;
                    String Remark = myTextBox2.Text;
                    // String RDate = myTextBox3.Text;


                    i = i + 1;

                    SqlConnection conn1 = new SqlConnection(strconn);
                    string lstrdrp1 = "insert into detention_dtl(jno,jobno,job_stage,Remark) " +
                                      "values('" + jno + "','" + jobno + "','" + Stage + "','" + Remark + "')";

                    conn1.Open();
                    SqlDataAdapter dap1 = new SqlDataAdapter();
                    SqlCommand cmdp1 = new SqlCommand(lstrdrp1, conn1);
                    cmdp1.CommandText = lstrdrp1;
                    cmdp1.Connection = conn1;
                    dap1.SelectCommand = cmdp1;

                    int result1 = cmdp1.ExecuteNonQuery();

                    // this command is to use for multiple data  


                    string lstrdrp2 = "update Detention_mstN set t" + i + "='" + Remark + "' " +
                                     "where jobno='" + jobno + "'";

                    //conn1.Open();
                    SqlDataAdapter dap2 = new SqlDataAdapter();
                    SqlCommand cmdp2 = new SqlCommand(lstrdrp2, conn1);
                    cmdp2.CommandText = lstrdrp2;
                    cmdp2.Connection = conn1;
                    dap2.SelectCommand = cmdp2;

                    int result2 = cmdp2.ExecuteNonQuery();
                    conn1.Close();
                    BtnSubmit.Enabled = false;
                    //  BtnPreview.Enabled = true;
                }
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Given job:" + jobno + " .Saved Successfully ');", true);
            }
            Response.Redirect("frmDetention.aspx");
        }

        protected void GetDetention(string JNO)
        {
            SqlConnection conn0 = new SqlConnection(strconn);
            string sqlQuery0 = " select * from Detention_mstN where JNo='" + JNO + "'";

            SqlDataAdapter da0 = new SqlDataAdapter(sqlQuery0, conn0);
            DataSet ds0 = new DataSet();
            da0.Fill(ds0, "iGround");
            DataRowView Row = ds0.Tables["iGround"].DefaultView[0];
            txtRNO.Text = Row["jno"].ToString();
            string jobno = Row["jobno"].ToString();
            Session["JOB_NO"] = jobno;
            txtDetails.Text = Row["particulars"].ToString();
            txtSupplier.Text = Row["supplier"].ToString();
            txtRASD.Text = Row["doc_date"].ToString();
            txtODOC.Text = Row["odoc_date"].ToString();
            txtBVessel.Text = Row["berth_of_Vessel"].ToString();
            txtIGM.Text = Row["igm_no"].ToString();
            txtBOE.Text = Row["boe_date"].ToString();
            txtGRent.Text = Row["Ground_rent_upto"].ToString();
            txtDetention.Text = Row["Detention_free"].ToString();
            txtMoveCFS.Text = Row["move_port_cfs"].ToString();
            txtMovePlant.Text = Row["move_cfs_plant"].ToString();
            txtContReturn.Text = Row["empty_cont_return_date"].ToString();
            txtBPTDamrage.Text = Row["BPT_Damrage"].ToString();
            txtDEt_charge.Text = Row["Detention_charge"].ToString();
            txtDamage_Charge.Text = Row["Damage_charge"].ToString();

            DataGrid1.DataSource = GetDataUpdate(jobno);
            DataGrid1.DataBind();

        }

        public DataSet GetDataUpdate(string JNO)
        {
            SqlConnection conn = new SqlConnection(strconn);
            string str = "select * from detention_dtl " +
                       " where jobno='" + JNO + "'";
            SqlDataAdapter da = new SqlDataAdapter(str, conn);
            DataSet ds = new DataSet();
            da.Fill(ds, "jobstatus");
            return (ds);
        }

        protected void UpdateDetention()
        {
            String jno = txtRNO.Text;
            String jobno = Session["JOB_NO"].ToString();
            String ODOC_Date = txtODOC.Text;
            String BOV = txtBVessel.Text;
            String IGM = txtIGM.Text;
            String BOE = txtBOE.Text;
            String GRupto = txtGRent.Text;
            String Detent_Free = txtDetention.Text;
            String CFS = txtMoveCFS.Text;
            String Plant = txtMovePlant.Text;
            String Cont_Return = txtContReturn.Text;
            String BPT = txtBPTDamrage.Text;
            String Detent_charge = txtDEt_charge.Text;
            String Damage_charge = txtDamage_Charge.Text;

            SqlConnection conn = new SqlConnection(strconn);
            string lstrdrp = "update Detention_mstN set Odoc_date='" + ODOC_Date + "',Berth_of_vessel='" + BOV + "'," +
                             "igm_no='" + IGM + "',boe_date='" + BOE + "',Ground_rent_upto='" + GRupto + "',Detention_free='" + Detent_Free + "', " +
                             "move_port_cfs='" + CFS + "',move_cfs_plant='" + Plant + "',empty_cont_return_date='" + Cont_Return + "'," +
                             "BPT_Damrage='" + BPT + "',Detention_charge='" + Detent_charge + "',Damage_charge='" + Damage_charge + "' " +
                             "where jobNo='" + jobno + "' ";

            conn.Open();
            SqlDataAdapter dap = new SqlDataAdapter();
            SqlCommand cmdp = new SqlCommand(lstrdrp, conn);
            cmdp.CommandText = lstrdrp;
            cmdp.Connection = conn;
            dap.SelectCommand = cmdp;

            int result = cmdp.ExecuteNonQuery();
            conn.Close();
            int i = 0;
            foreach (DataGridItem Row in DataGrid1.Items)
            {

                //string add1 = ((System.Web.UI.WebControls.TextBox)(Row.Cells[3].Controls[0])).Text;
                //TextBox MyTextBox1=Row.Cells[1].Controls[1];
                // System.Web.UI.WebControls.TextBox myTextBox1 = (System.Web.UI.WebControls.TextBox)(Row.Cells[0].Controls[1]);
                System.Web.UI.WebControls.TextBox myTextBox2 = (System.Web.UI.WebControls.TextBox)(Row.Cells[1].Controls[1]);
                //  System.Web.UI.WebControls.TextBox myTextBox3 = (System.Web.UI.WebControls.TextBox)(Row.Cells[2].Controls[1]);
                String Stage = Row.Cells[0].Text;
                String Remark = myTextBox2.Text;
                // String RDate = myTextBox3.Text;
                //DateTime DD;
                //String RD = "";
                //if (RDate == "")
                //    RD = "";
                //else
                //{
                //    DD = Convert.ToDateTime(RDate);
                //    RD = DD.ToString("dd/MM/yyyy");
                //}
                //String Reason = Remark + " " + RD;
                i = i + 1;

                SqlConnection conn1 = new SqlConnection(strconn);
                string lstrdrp1 = "update detention_dtl set Remark='" + Remark + "'" +
                                  "where jobno='" + jobno + "' and job_stage='" + Stage + "'";

                conn1.Open();
                SqlDataAdapter dap1 = new SqlDataAdapter();
                SqlCommand cmdp1 = new SqlCommand(lstrdrp1, conn1);
                cmdp1.CommandText = lstrdrp1;
                cmdp1.Connection = conn1;
                dap1.SelectCommand = cmdp1;

                int result1 = cmdp1.ExecuteNonQuery();

                // this command is to use for multiple data  


                string lstrdrp2 = "update Detention_mstN set t" + i + "='" + Remark + "' " +
                                 "where jobno='" + jobno + "'";

                //conn1.Open();
                SqlDataAdapter dap2 = new SqlDataAdapter();
                SqlCommand cmdp2 = new SqlCommand(lstrdrp2, conn1);
                cmdp2.CommandText = lstrdrp2;
                cmdp2.Connection = conn1;
                dap2.SelectCommand = cmdp2;

                int result2 = cmdp2.ExecuteNonQuery();
                conn1.Close();
                BtnSubmit.Enabled = false;
                //  BtnPreview.Enabled = true;
            }
        }
    }
}