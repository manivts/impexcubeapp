using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Data.OleDb;
using System.Data;
using MySql.Data.MySqlClient;
using System.Text;
using System.Windows.Forms;

namespace ImpexCube
{
    public partial class frmAccessDBUpload : System.Web.UI.Page
    {
        string strconn = (string)ConfigurationManager.AppSettings["ConnectionDashboard"];
        string strcon = (string)ConfigurationManager.ConnectionStrings["Constr"].ConnectionString;
        private string FilePath = "";
        OleDbConnection oledbConn;
        System.Data.DataTable dt = null;
        //string FY = "";
        int Result = 0;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
            }
        }

        public void loads(string SlnoAbbreviation)
        {
            string path = System.IO.Path.GetFullPath(@SlnoAbbreviation);

            oledbConn = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + path + "");
            oledbConn.Open();
            OleDbCommand cmd = new OleDbCommand();
            OleDbDataAdapter oleda = new OleDbDataAdapter();
            DataSet ds = new DataSet();


            cmd.Connection = oledbConn;
            cmd.CommandType = CommandType.Text;

            dt = oledbConn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);

            if (dt == null)
            {

            }

            String[] excelSheets = new String[dt.Rows.Count];
            int i = 0;

            foreach (DataRow row in dt.Rows)
            {
                excelSheets[i] = row["TABLE_NAME"].ToString();
                i++;
            }
            for (int j = 0; j < excelSheets.Length; j++)
            {
                ddlTable.Items.Add(excelSheets[j]);
            }
            ddlTable.Visible = true;
            ddlTable.Items.Insert(0, new ListItem("~Select~", "0"));
            ddlTable.Items.Remove("_xlnm#_FilterDatabase");
        }


        public bool CheckIfFileIsBeingUsed(string fileName)
        {
            try
            {
                FileStream fs;
                using (fs = new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.None))
                {
                    fs.Dispose();
                    fs.Close();
                }
                //if (!Directory.Exists(fileName))
                //{

                //}
                //else
                //{
                //    Directory.Delete(fileName);
                //}
            }

            catch (Exception exp)
            {
                return true;
            }

            return false;
        }

        protected void btnRead_Click(object sender, EventArgs e)
        {
            lblMsg.Text = FileUpload1.PostedFile.FileName;
            string names = FileUpload1.PostedFile.FileName;
            string CheckExtension = Path.GetExtension(names);
            string path = "";
            if (FileUpload1.HasFile)
            {
                FileUpload1.Enabled = false;
                string paths = AppDomain.CurrentDomain.BaseDirectory;
                string pathdir = Path.Combine(paths, @"TempFile\");
                path = pathdir + Path.GetFileName(FileUpload1.PostedFile.FileName);
                string[] filePaths = Directory.GetFiles(@pathdir);
                string TestFileName = FileUpload1.FileName;

                foreach (string filePath in filePaths)
                {
                    if (!CheckIfFileIsBeingUsed(filePath))
                    {
                        File.Delete(filePath);
                    }
                    // The file is not locked
                }
                FileUpload1.SaveAs(pathdir + Path.GetFileName(FileUpload1.PostedFile.FileName));
                FilePath += path;
            }
            string name = FileUpload1.FileName;
            string filename = Path.GetFileName(FileUpload1.FileName);
            Session["file"] = "";
            Session["file"] = path;
            Session["filename"] = "";
            if (CheckExtension == ".mdb")
            {
                try
                {
                    loads(path);
                }
                catch (Exception ex)
                {

                }
            }
        }

        protected void btnLoad_Click(object sender, EventArgs e)
        {
            string path = "";

            string FileN = (lblMsg.Text);
            string FileExt = Path.GetExtension(lblMsg.Text);
            string filename = Path.GetFileName(FileN);
            Session["ExcelName"] = filename;
            int result = 0;
            if (FileExt == ".mdb")
            {
                string path1 = (string)Session["file"];
                if ((ddlTable.SelectedValue != "_xlnm#_FilterDatabase") || (ddlTable.SelectedValue != "~Select~"))
                {
                    if (ddlTable.SelectedValue == "Notn_Sln")
                    {
                        result = GenerateExcelData(path1, ddlTable.SelectedValue);
                    }
                    else if (ddlTable.SelectedValue == "BCD_Duty")
                    {
                         result = GenerateBCD(path1, ddlTable.SelectedValue);
                    }
                    else if (ddlTable.SelectedValue == "CVD_Duty")
                    {
                        result = GenerateCVD(path1, ddlTable.SelectedValue);
                    }
                    else if(ddlTable.SelectedValue=="RSP_Duty")
                    {
                        result = GenerateRSP(path1, ddlTable.SelectedValue);
                    }
                    
                    
                    if (result == 1)
                    {
                        //InsertExcelData(result);
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "redirect", "alert('" + ddlTable.SelectedValue + "  Table Saved Successfully'); window.location.href='frmAccessDBUpload.aspx';", true);
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "redirect", "alert('Please Select Correct Sheet');", true);
                }
            }
        }


        public int GenerateExcelData(string SlnoAbbreviation, string sheetname)
        {
            string NOTN;
            string NOTN_TYPE;

            DateTime NOTN_DT = DateTime.Now;

            string SLNO;
            string CTH;

            double RTA = 0;

            double AMTS = 0;

            string UQC;
            string FLG;
            string COND;

            double CVD_RTA = 0; ;
            double CVD_AMTS = 0; ;

            string CVD_UQC;
            string CVD_FLG;
            string AMND_REF;
            DateTime NOTN_ENDT = DateTime.Now;

            string A_NOTN;

            DateTime A_NOTN_DT = DateTime.Now;

            string A_SLNO;
            string STATUS;
            string AD_FLG;
            string AMEND_BY;

            DateTime AMEND_DT = DateTime.Now;

            string ENTRY_BY;

            DateTime ENTRY_DT = DateTime.Now;

            double NotnYr = 0;

            double NotnNo = 0;

            double NotnSlNo = 0;


            string NotnSubSNo;
            bool UserNotn=false;
            try
            {
                string path = System.IO.Path.GetFullPath(@SlnoAbbreviation);
                oledbConn = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + path + "");
                oledbConn.Open();
                OleDbCommand cmd = new OleDbCommand();
                OleDbDataAdapter oleda = new OleDbDataAdapter();
                DataSet ds = new DataSet();
                cmd.Connection = oledbConn;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT * FROM [" + sheetname + "]";
                cmd.Parameters.AddWithValue("Slno_Abbreviation", SlnoAbbreviation);
                oleda = new OleDbDataAdapter(cmd);
                oleda.Fill(ds, "SQLTABLE");
                int i = 0;
                foreach (DataRow row1 in ds.Tables["SQLTABLE"].Rows)
                {
                     NOTN = row1["NOTN"].ToString();
                     NOTN_TYPE = row1["NOTN_TYPE"].ToString();
                     if (row1["NOTN_DT"] != DBNull.Value)
                     {
                         NOTN_DT = Convert.ToDateTime(row1["NOTN_DT"].ToString());
                     }            
                     SLNO = row1["SLNO"].ToString();
                     CTH = row1["CTH"].ToString();
                     if (row1["RTA"] != DBNull.Value)
                     {
                         RTA = Convert.ToDouble(row1["RTA"].ToString());
                     }
                     if (row1["AMTS"] != DBNull.Value)
                     {
                         AMTS = Convert.ToDouble(row1["AMTS"].ToString());
                     }

                     UQC = row1["UQC"].ToString();
                     FLG = row1["FLG"].ToString();
                     COND = row1["COND"].ToString();
                     if (row1["CVD_RTA"] != DBNull.Value)
                     {
                         CVD_RTA = Convert.ToDouble(row1["CVD_RTA"].ToString());
                     }
                     if (row1["CVD_AMTS"] != DBNull.Value)
                     {
                         CVD_AMTS = Convert.ToDouble(row1["CVD_AMTS"].ToString());
                     }

                     CVD_UQC = row1["CVD_UQC"].ToString();
                     CVD_FLG = row1["CVD_FLG"].ToString();
                     AMND_REF = row1["AMND_REF"].ToString();
                     if (row1["NOTN_ENDT"] != DBNull.Value)
                     {
                         NOTN_ENDT = Convert.ToDateTime(row1["NOTN_ENDT"]);
                     }

                     A_NOTN = row1["A_NOTN"].ToString();
                     if (row1["A_NOTN_DT"] != DBNull.Value)
                     {
                         A_NOTN_DT = Convert.ToDateTime(row1["A_NOTN_DT"]);
                     }

                     A_SLNO = row1["A_SLNO"].ToString();
                     STATUS = row1["STATUS"].ToString();
                     AD_FLG = row1["AD_FLG"].ToString();
                     AMEND_BY = row1["AMEND_BY"].ToString();
                     if (row1["AMEND_DT"] != DBNull.Value)
                     {
                         AMEND_DT = Convert.ToDateTime(row1["AMEND_DT"]);
                     }

                     ENTRY_BY = row1["ENTRY_BY"].ToString();
                     if (row1["ENTRY_DT"] != DBNull.Value)
                     {
                         ENTRY_DT = Convert.ToDateTime(row1["ENTRY_DT"]);
                     }
                     if (row1["NotnYr"] != DBNull.Value)
                     {
                         NotnYr = Convert.ToDouble(row1["NotnYr"].ToString());
                     }
                     if (row1["NotnNo"] != DBNull.Value)
                     {
                         NotnNo = Convert.ToDouble(row1["NotnNo"].ToString());
                     }
                     if (row1["NotnSlNo"] != DBNull.Value)
                     {
                         NotnSlNo = Convert.ToDouble(row1["NotnSlNo"].ToString());
                     }


                     NotnSubSNo = row1["NotnSubSNo"].ToString();
                     UserNotn = Convert.ToBoolean(row1["UserNotn"]);

                    StringBuilder Query = new StringBuilder();
                    string Message = string.Empty;
                    DataSet ds1 = new DataSet();
                    try
                    {
                        using (SqlConnection con = new SqlConnection(strcon))
                        {
                            con.Open();
                            Query.Append("INSERT INTO [TempNotn] (Notn,NOTN_TYPE,NOTN_DT,SLNO,CTH,RTA,AMTS,UQC,FLG,COND,CVD_RTA,CVD_AMTS,CVD_UQC,CVD_FLG,AMND_REF,NOTN_ENDT,A_NOTN,A_NOTN_DT,A_SLNO,STATUS,AD_FLG,AMEND_BY,AMEND_DT,ENTRY_BY,ENTRY_DT,NotnYr,NotnNo,NotnSlNo,NotnSubSNo,UserNotn)");
                            Query.Append("values(@Notn,@NOTN_TYPE,@NOTN_DT,@SLNO,@CTH,@RTA,@AMTS,@UQC,@FLG,@COND,@CVD_RTA,@CVD_AMTS,@CVD_UQC,@CVD_FLG,@AMND_REF,@NOTN_ENDT,@A_NOTN,@A_NOTN_DT,@A_SLNO,@STATUS,@AD_FLG,@AMEND_BY,@AMEND_DT,@ENTRY_BY,@ENTRY_DT,@NotnYr,@NotnNo,@NotnSlNo,@NotnSubSNo,@UserNotn)");
                            SqlCommand cmd1 = new SqlCommand(Query.ToString(), con);

                            cmd1.Parameters.AddWithValue("@NOTN", NOTN);
                            cmd1.Parameters.AddWithValue("@NOTN_TYPE", NOTN_TYPE);
                            cmd1.Parameters.AddWithValue("@NOTN_DT", NOTN_DT);
                            cmd1.Parameters.AddWithValue("@SLNO", SLNO);
                            cmd1.Parameters.AddWithValue("@CTH", CTH);
                            cmd1.Parameters.AddWithValue("@RTA", RTA);
                            cmd1.Parameters.AddWithValue("@AMTS", AMTS);
                            cmd1.Parameters.AddWithValue("@UQC", UQC);
                            cmd1.Parameters.AddWithValue("@FLG", FLG);
                            cmd1.Parameters.AddWithValue("@COND", COND);
                            cmd1.Parameters.AddWithValue("@CVD_RTA", CVD_RTA);
                            cmd1.Parameters.AddWithValue("@CVD_AMTS", CVD_AMTS);
                            cmd1.Parameters.AddWithValue("@CVD_UQC", CVD_UQC);
                            cmd1.Parameters.AddWithValue("@CVD_FLG", CVD_FLG);
                            cmd1.Parameters.AddWithValue("@AMND_REF", AMND_REF);
                            cmd1.Parameters.AddWithValue("@NOTN_ENDT", NOTN_ENDT);
                            cmd1.Parameters.AddWithValue("@A_NOTN", A_NOTN);
                            cmd1.Parameters.AddWithValue("@A_NOTN_DT", A_NOTN_DT);
                            cmd1.Parameters.AddWithValue("@A_SLNO", A_SLNO);
                            cmd1.Parameters.AddWithValue("@STATUS", STATUS);
                            cmd1.Parameters.AddWithValue("@AD_FLG", AD_FLG);
                            cmd1.Parameters.AddWithValue("@AMEND_BY", AMEND_BY);
                            cmd1.Parameters.AddWithValue("@AMEND_DT", AMEND_DT);
                            cmd1.Parameters.AddWithValue("@ENTRY_BY", ENTRY_BY);
                            cmd1.Parameters.AddWithValue("@ENTRY_DT", ENTRY_DT);
                            cmd1.Parameters.AddWithValue("@NotnYr", NotnYr);
                            cmd1.Parameters.AddWithValue("@NotnNo", NotnNo);
                            cmd1.Parameters.AddWithValue("@NotnSlNo", NotnSlNo);
                            cmd1.Parameters.AddWithValue("@NotnSubSNo", NotnSubSNo);
                            cmd1.Parameters.AddWithValue("@UserNotn", UserNotn);

                            Result = cmd1.ExecuteNonQuery();
                            con.Close();
                        }
                    }
                    catch (Exception ex)
                    {
                        string Mes = ex.Message;
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return Result;
        }

        public int GenerateBCD(string SlnoAbbreviation, string sheetname)
        {
            string CTH;            
            string FLG;
            

            double RTA = 0;

            double AMTS = 0;

            string UQC;
            string Pref;
            string PFLG;

            double PRTA = 0; 
            double PAMTS = 0; 

            string PUQC;            
            string AmndRef;
            DateTime EFFDT = DateTime.Now;

            string ANOTN;

            DateTime ENDDT = DateTime.Now;
            DateTime ANOTNDT = DateTime.Now;
            DateTime AmendDt=DateTime.Now;
            string AmendBy;
            string Status;
            string EntryBy;
            DateTime EntryDt = DateTime.Now;                        
            try
            {
                string path = System.IO.Path.GetFullPath(@SlnoAbbreviation);
                oledbConn = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + path + "");
                oledbConn.Open();
                OleDbCommand cmd = new OleDbCommand();
                OleDbDataAdapter oleda = new OleDbDataAdapter();
                DataSet ds = new DataSet();
                cmd.Connection = oledbConn;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT * FROM [" + sheetname + "]";
                cmd.Parameters.AddWithValue("Slno_Abbreviation", SlnoAbbreviation);
                oleda = new OleDbDataAdapter(cmd);
                oleda.Fill(ds, "SQLTABLE");
                int i = 0;
                foreach (DataRow row1 in ds.Tables["SQLTABLE"].Rows)
                {
                    CTH = row1["CTH"].ToString();
                    if (row1["END_DT"] != DBNull.Value)
                    {
                        ENDDT = Convert.ToDateTime(row1["END_DT"].ToString());
                    }
                    FLG = row1["FLG"].ToString();
                    UQC = row1["UQC"].ToString();
                    if (row1["RTA"] != DBNull.Value)
                    {
                        RTA = Convert.ToDouble(row1["RTA"].ToString());
                    }
                    if (row1["AMTS"] != DBNull.Value)
                    {
                        AMTS = Convert.ToDouble(row1["AMTS"].ToString());
                    }

                    Pref = row1["PREF"].ToString();
                    PFLG = row1["PFLG"].ToString();
                    PUQC = row1["PUQC"].ToString();
                    if (row1["PRTA"] != DBNull.Value)
                    {
                        PRTA = Convert.ToDouble(row1["PRTA"].ToString());
                    }
                    if (row1["PAMTS"] != DBNull.Value)
                    {
                        PAMTS = Convert.ToDouble(row1["PAMTS"].ToString());
                    }

                    AmndRef = row1["AMND_REF"].ToString();
                    ANOTN = row1["A_NOTN"].ToString();
                    if (row1["A_NOTN_DT"] != DBNull.Value)
                    {
                        ANOTNDT = Convert.ToDateTime(row1["A_NOTN_DT"]);
                    }

                    AmendBy = row1["AMEND_BY"].ToString();
                    if (row1["EFF_DT"] != DBNull.Value)
                    {
                        EFFDT = Convert.ToDateTime(row1["EFF_DT"]);
                    }

                    Status = row1["STATUS"].ToString();
                    EntryBy = row1["ENTRY_BY"].ToString();

                    if (row1["AMEND_DT"] != DBNull.Value)
                    {
                        AmendDt = Convert.ToDateTime(row1["AMEND_DT"]);
                    }

                    if (row1["ENTRY_DT"] != DBNull.Value)
                    {
                        EntryDt = Convert.ToDateTime(row1["ENTRY_DT"]);
                    }
                   
                   

                    StringBuilder Query = new StringBuilder();
                    string Message = string.Empty;
                    DataSet ds1 = new DataSet();
                    try
                    {
                        using (SqlConnection con = new SqlConnection(strcon))
                        {
                            con.Open();
                            Query.Append("INSERT INTO [TempBCD] (CTH,FLG,RTA,AMTS,UQC,Pref,PFLG,PRTA,PAMTS,PUQC,EFFDT,ENDDT,AmndRef,ANOTN,ANOTNDT,AmendDt,AmendBy,Status,EntryBy,EntryDt)");
                            Query.Append("values(@CTH,@FLG,@RTA,@AMTS,@UQC,@Pref,@PFLG,@PRTA,@PAMTS,@PUQC,@EFFDT,@ENDDT,@AmndRef,@ANOTN,@ANOTNDT,@AmendDt,@AmendBy,@Status,@EntryBy,@EntryDt)");
                            SqlCommand cmd1 = new SqlCommand(Query.ToString(), con);

                            cmd1.Parameters.AddWithValue("@CTH", CTH);                            
                            cmd1.Parameters.AddWithValue("@FLG", FLG);
                            cmd1.Parameters.AddWithValue("@RTA", RTA);
                            cmd1.Parameters.AddWithValue("@AMTS", AMTS);
                            cmd1.Parameters.AddWithValue("@UQC", UQC);
                            cmd1.Parameters.AddWithValue("@Pref", Pref);
                            cmd1.Parameters.AddWithValue("@PFLG", PFLG);
                            cmd1.Parameters.AddWithValue("@PRTA", PRTA);
                            cmd1.Parameters.AddWithValue("@PAMTS", PAMTS);
                            cmd1.Parameters.AddWithValue("@PUQC", PUQC);
                            cmd1.Parameters.AddWithValue("@EFFDT", EFFDT);
                            cmd1.Parameters.AddWithValue("@ENDDT", ENDDT);
                            cmd1.Parameters.AddWithValue("@AmndRef", AmndRef);
                            cmd1.Parameters.AddWithValue("@ANOTN", ANOTN);
                            cmd1.Parameters.AddWithValue("@ANOTNDT", ANOTNDT);
                            cmd1.Parameters.AddWithValue("@AmendDt", AmendDt);
                            cmd1.Parameters.AddWithValue("@AmendBy", AmendBy);
                            cmd1.Parameters.AddWithValue("@Status", Status);
                            cmd1.Parameters.AddWithValue("@EntryBy", EntryBy);
                            cmd1.Parameters.AddWithValue("@EntryDt", EntryDt);                           

                            Result = cmd1.ExecuteNonQuery();
                            con.Close();
                        }
                    }
                    catch (Exception ex)
                    {
                        string Mes = ex.Message;
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return Result;
        }

        public int GenerateCVD(string SlnoAbbreviation, string sheetname)
        {
            string CETH;
            string CVDF;
            string CVDUQC;
            double CVDAMTS = 0;
            double CVDRTA = 0;
            string AD1F;
            double AD1AMTS = 0;
            double AD1RTA = 0;
            string AD1UQC;
            string AD2F;
            double AD2AMTS = 0;
            double AD2RTA = 0;
            string AD2UQC;
            string OTHF;
            double OTHAMTS=0;
            double OTHRTA=0;
            string OTHUQC;
            string CessF;
            double CessAMTS=0;
            double CessRTA = 0;
            string CessUQC;
            string Item;
            DateTime EffDT = DateTime.Now;
            DateTime EndDT = DateTime.Now;
            string AmndRef;
            string ANOTN;
            DateTime ANOTNDT = DateTime.Now;
            DateTime AmendDt = DateTime.Now;
            string AmendBy;
            string Status;
            string EntryBy;
            DateTime EntryDt = DateTime.Now;
            string HLTHF;
            double HLTHAMTS = 0;
            double HLTHRTA = 0;
            string HLTHUQC;
            try
            {
                string path = System.IO.Path.GetFullPath(@SlnoAbbreviation);
                oledbConn = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + path + "");
                oledbConn.Open();
                OleDbCommand cmd = new OleDbCommand();
                OleDbDataAdapter oleda = new OleDbDataAdapter();
                DataSet ds = new DataSet();
                cmd.Connection = oledbConn;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT * FROM [" + sheetname + "]";
                cmd.Parameters.AddWithValue("Slno_Abbreviation", SlnoAbbreviation);
                oleda = new OleDbDataAdapter(cmd);
                oleda.Fill(ds, "SQLTABLE");
                int i = 0;
                foreach (DataRow row1 in ds.Tables["SQLTABLE"].Rows)
                {
                    CETH = row1["CETH"].ToString();
                    CVDF = row1["CVD_F"].ToString();
                    if (row1["CVD_AMTS"] != DBNull.Value)
                    {
                        CVDAMTS = Convert.ToDouble(row1["CVD_AMTS"].ToString());
                    }
                    if (row1["CVD_RTA"] != DBNull.Value)
                    {
                        CVDRTA = Convert.ToDouble(row1["CVD_RTA"].ToString());
                    }
                    CVDUQC = row1["CVD_UQC"].ToString();
                    AD1F = row1["AD1_F"].ToString();
                    if (row1["AD1_AMTS"] != DBNull.Value)
                    {
                        AD1AMTS = Convert.ToDouble(row1["AD1_AMTS"].ToString());
                    }
                    if (row1["AD1_RTA"] != DBNull.Value)
                    {
                        AD1RTA = Convert.ToDouble(row1["AD1_RTA"].ToString());
                    }

                    AD1UQC = row1["AD1_UQC"].ToString();
                    AD2F = row1["AD2_F"].ToString();
                    if (row1["AD2_AMTS"] != DBNull.Value)
                    {
                        AD2AMTS = Convert.ToDouble(row1["AD2_AMTS"].ToString());
                    }
                    if (row1["AD2_RTA"] != DBNull.Value)
                    {
                        AD2RTA = Convert.ToDouble(row1["AD2_RTA"].ToString());
                    }
                        AD2UQC = row1["AD2_UQC"].ToString();
                       
                        
                        OTHF = row1["OTH_F"].ToString();
                        if (row1["OTH_AMTS"] != DBNull.Value)
                        {
                            OTHAMTS = Convert.ToDouble(row1["OTH_AMTS"]);
                        }

                        if (row1["OTH_RTA"] != DBNull.Value)
                        {
                            OTHRTA = Convert.ToDouble(row1["OTH_RTA"]);
                        }
                        OTHUQC = row1["OTH_UQC"].ToString();
                        CessF = row1["Cess_F"].ToString();
                        if (row1["Cess_AMTS"] != DBNull.Value)
                        {
                            CessAMTS = Convert.ToDouble(row1["Cess_AMTS"]);
                        }
                        if (row1["Cess_RTA"] != DBNull.Value)
                        {
                            CessRTA = Convert.ToDouble(row1["Cess_RTA"]);
                        }

                        CessUQC = row1["Cess_UQC"].ToString();
                        if (row1["EFF_DT"] != DBNull.Value)
                        {
                            EffDT = Convert.ToDateTime(row1["EFF_DT"].ToString());
                        }
                        if (row1["END_DT"] != DBNull.Value)
                        {
                            EndDT = Convert.ToDateTime(row1["END_DT"].ToString());
                        }

                        AmndRef = row1["AMND_REF"].ToString();
                        ANOTN = row1["A_NOTN"].ToString();
                        if (row1["A_NOTN_DT"] != DBNull.Value)
                        {
                            ANOTNDT = Convert.ToDateTime(row1["A_NOTN_DT"].ToString());
                        }
                        if (row1["AMEND_DT"] != DBNull.Value)
                        {
                            AmendDt = Convert.ToDateTime(row1["AMEND_DT"].ToString());
                        }
                        AmendBy = row1["AMEND_BY"].ToString();
                        Status = row1["STATUS"].ToString();
                        HLTHF = row1["HLTH_F"].ToString();
                        EntryBy = row1["ENTRY_BY"].ToString();
                        if (row1["ENTRY_DT"] != DBNull.Value)
                        {
                            EntryDt = Convert.ToDateTime(row1["ENTRY_DT"].ToString());
                        }
                        if (row1["HLTH_AMTS"] != DBNull.Value)
                        {
                            HLTHAMTS = Convert.ToDouble(row1["HLTH_AMTS"].ToString());
                        }
                        if (row1["HLTH_RTA"] != DBNull.Value)
                        {
                            HLTHRTA = Convert.ToDouble(row1["HLTH_RTA"].ToString());
                        }
                        HLTHUQC = row1["HLTH_UQC"].ToString();
                    StringBuilder Query = new StringBuilder();
                    string Message = string.Empty;
                    DataSet ds1 = new DataSet();
                    try
                    {
                        using (SqlConnection con = new SqlConnection(strcon))
                        {
                            con.Open();
                            Query.Append("INSERT INTO [TempCVD] (CETH,CVDF,CVDAMTS,CVDRTA,CVDUQC,AD1F,AD1AMTS,AD1RTA,AD1UQC,AD2F,AD2AMTS,AD2RTA,AD2UQC,OTHF,OTHAMTS,OTHRTA,OTHUQC,CessF,CessAMTS,CessRTA,CessUQC,EffDT,EndDT,AmndRef,ANOTN,ANOTNDT,AmendDt,AmendBy,Status,EntryBy,EntryDt,HLTHF,HLTHAMTS,HLTHRTA,HLTHUQC)");
                            Query.Append("values(@CETH,@CVDF,@CVDAMTS,@CVDRTA,@CVDUQC,@AD1F,@AD1AMTS,@AD1RTA,@AD1UQC,@AD2F,@AD2AMTS,@AD2RTA,@AD2UQC,@OTHF,@OTHAMTS,@OTHRTA,@OTHUQC,@CessF,@CessAMTS,@CessRTA,@CessUQC,@EffDT,@EndDT,@AmndRef,@ANOTN,@ANOTNDT,@AmendDt,@AmendBy,@Status,@EntryBy,@EntryDt,@HLTHF,@HLTHAMTS,@HLTHRTA,@HLTHUQC)");
                            SqlCommand cmd1 = new SqlCommand(Query.ToString(), con);

                            cmd1.Parameters.AddWithValue("@CETH", CETH);
                            cmd1.Parameters.AddWithValue("@CVDF", CVDF);
                            cmd1.Parameters.AddWithValue("@CVDAMTS", CVDAMTS);
                            cmd1.Parameters.AddWithValue("@CVDRTA", CVDRTA);
                            cmd1.Parameters.AddWithValue("@CVDUQC", CVDUQC);
                            cmd1.Parameters.AddWithValue("@AD1F", AD1F);
                            cmd1.Parameters.AddWithValue("@AD1AMTS", AD1AMTS);
                            cmd1.Parameters.AddWithValue("@AD1RTA", AD1RTA);
                            cmd1.Parameters.AddWithValue("@AD1UQC", AD1UQC);
                            cmd1.Parameters.AddWithValue("@AD2F", AD2F);
                            cmd1.Parameters.AddWithValue("@AD2AMTS", AD2AMTS);
                            cmd1.Parameters.AddWithValue("@AD2RTA", AD2RTA);
                            cmd1.Parameters.AddWithValue("@AD2UQC", AD2UQC);
                            cmd1.Parameters.AddWithValue("@OTHF", OTHF);
                            cmd1.Parameters.AddWithValue("@OTHAMTS", OTHAMTS);
                            cmd1.Parameters.AddWithValue("@OTHRTA", OTHRTA);
                            cmd1.Parameters.AddWithValue("@OTHUQC", OTHUQC);
                            cmd1.Parameters.AddWithValue("@CessF", CessF);
                            cmd1.Parameters.AddWithValue("@CessAMTS", CessAMTS);
                            cmd1.Parameters.AddWithValue("@CessRTA", CessRTA);
                            cmd1.Parameters.AddWithValue("@CessUQC", CessUQC);                            
                            cmd1.Parameters.AddWithValue("@EffDT", EffDT);
                            cmd1.Parameters.AddWithValue("@EndDT", EndDT);
                            cmd1.Parameters.AddWithValue("@AmndRef", AmndRef);
                            cmd1.Parameters.AddWithValue("@ANOTN", ANOTN);
                            cmd1.Parameters.AddWithValue("@ANOTNDT", ANOTNDT);
                            cmd1.Parameters.AddWithValue("@AmendDt", AmendDt);
                            cmd1.Parameters.AddWithValue("@AmendBy", AmendBy);
                            cmd1.Parameters.AddWithValue("@Status", Status);
                            cmd1.Parameters.AddWithValue("@EntryBy", EntryBy);
                            cmd1.Parameters.AddWithValue("@EntryDt", EntryDt);
                            cmd1.Parameters.AddWithValue("@HLTHF", HLTHF);
                            cmd1.Parameters.AddWithValue("@HLTHAMTS", HLTHAMTS);
                            cmd1.Parameters.AddWithValue("@HLTHRTA", HLTHRTA);
                            cmd1.Parameters.AddWithValue("@HLTHUQC", HLTHUQC);

                            Result = cmd1.ExecuteNonQuery();
                            con.Close();
                        }
                    }
                    catch (Exception ex)
                    {
                        string Mes = ex.Message;
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return Result;
        }

        public int GenerateRSP(string SlnoAbbreviation, string sheetname)
        {
            string CETH;
            DateTime EffDT = DateTime.Now;
            DateTime EndDT = DateTime.Now;
            double ABETRTA = 0;
            string EntryBy;
            DateTime EntryDt = DateTime.Now;
            string AmendBy;
            DateTime AmendDt = DateTime.Now;
            string AmndRef;
            string ANOTN;
            DateTime ANOTNDT = DateTime.Now;
            string NOTN;           
            string SLNO;            
            try
            {
                string path = System.IO.Path.GetFullPath(@SlnoAbbreviation);
                oledbConn = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + path + "");
                oledbConn.Open();
                OleDbCommand cmd = new OleDbCommand();
                OleDbDataAdapter oleda = new OleDbDataAdapter();
                DataSet ds = new DataSet();
                cmd.Connection = oledbConn;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT * FROM [" + sheetname + "]";
                cmd.Parameters.AddWithValue("Slno_Abbreviation", SlnoAbbreviation);
                oleda = new OleDbDataAdapter(cmd);
                oleda.Fill(ds, "SQLTABLE");
                int i = 0;
                foreach (DataRow row1 in ds.Tables["SQLTABLE"].Rows)
                {
                    CETH = row1["CETH"].ToString();
                    if (row1["EFF_DT"] != DBNull.Value)
                    {
                        EffDT = Convert.ToDateTime(row1["EFF_DT"].ToString());
                    }
                    if (row1["END_DT"] != DBNull.Value)
                    {
                        EndDT = Convert.ToDateTime(row1["END_DT"].ToString());
                    }
                    if (row1["ABET_RTA"] != DBNull.Value)
                    {
                        ABETRTA = Convert.ToDouble(row1["ABET_RTA"].ToString());
                    }
                    EntryBy = row1["ENTRY_BY"].ToString();                    
                    if (row1["ENTRY_DT"] != DBNull.Value)
                    {
                        EntryDt = Convert.ToDateTime(row1["ENTRY_DT"].ToString());
                    }                                        
                    SLNO = row1["SLNO"].ToString();
                    
                    StringBuilder Query = new StringBuilder();
                    string Message = string.Empty;
                    DataSet ds1 = new DataSet();
                    try
                    {
                        using (SqlConnection con = new SqlConnection(strcon))
                        {
                            con.Open();
                            Query.Append("INSERT INTO [TempRSP] (CETH,EffDt,EndDT,ABETRTA,EntryBy,EntryDt,SLNO)");
                            Query.Append("values(@CETH,@EffDt,@EndDT,@ABETRTA,@EntryBy,@EntryDt,@SLNO)");
                            SqlCommand cmd1 = new SqlCommand(Query.ToString(), con);

                            cmd1.Parameters.AddWithValue("@CETH", CETH);
                            cmd1.Parameters.AddWithValue("@EffDt", EffDT);
                            cmd1.Parameters.AddWithValue("@EndDT", EndDT);
                            cmd1.Parameters.AddWithValue("@ABETRTA", ABETRTA);
                            cmd1.Parameters.AddWithValue("@EntryBy", EntryBy);
                            cmd1.Parameters.AddWithValue("@EntryDt", EntryDt);                                                       
                            cmd1.Parameters.AddWithValue("@SLNO", SLNO);
                            
                            Result = cmd1.ExecuteNonQuery();
                            con.Close();
                        }
                    }
                    catch (Exception ex)
                    {
                        string Mes = ex.Message;
                    }
                }
            }
            catch (Exception ex)
            {
                string message = ex.Message;
            }
            return Result;
        }

    }
}