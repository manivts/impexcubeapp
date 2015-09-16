

namespace VTS.ImpexCube.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Configuration;
    using System.Data;
    using System.Data.SqlClient;

   
    public class JobStageUpdateDL
    {
        string con = (string)ConfigurationManager.ConnectionStrings["Constr"].ConnectionString;

        public DataSet SelectStage()
        {
            DataSet ds = new DataSet();
            try
            {
                string Query = "select StageId, StageName from M_Stage";

                SqlConnection sqlConn = new SqlConnection(con);
                sqlConn.Open();
                SqlDataAdapter da = new SqlDataAdapter(Query, sqlConn);

                da.Fill(ds, "stage");
                sqlConn.Close();
            }
            catch (Exception ex)
            {
                string Msg = ex.Message.ToString();
            }
            return ds;
        }

        public DataSet SelectStageStatus(int StageId)
        {
            DataSet ds = new DataSet();
            try
            {
                string Query = "select Id, StatusName from T_StageStatus where StageId='"+StageId+"'";

                SqlConnection sqlConn = new SqlConnection(con);
                sqlConn.Open();
                SqlDataAdapter da = new SqlDataAdapter(Query, sqlConn);

                da.Fill(ds, "status");
                sqlConn.Close();
            }
            catch (Exception ex)
            {
                string Msg = ex.Message.ToString();
            }
            return ds;
        }

        public DataSet SearchJobStatusList(string DocFrom, string To, string Importer, string Jobno, string stage,string status)
        {
         
            StringBuilder Query = new StringBuilder();
            string condition = string.Empty;
           string condition1 = string.Empty;
           
           if (!string.IsNullOrEmpty(DocFrom))
           {
               Query.Append(" and JobReceivedDate >= '" + frmdatesplit(DocFrom) + "'");
           }
           if (!string.IsNullOrEmpty(To))
           {
               Query.Append(" and JobReceivedDate <= '" + frmdatesplit(To) + "'");
           }
           if (!string.IsNullOrEmpty(Importer))
           {
               Query.Append(" and Importer like '" + Importer + "%'");
           }
           if (!string.IsNullOrEmpty(Jobno))
           {
               Query.Append(" and JobNo like '" + Jobno + "%'");
           }
           if (!string.IsNullOrEmpty(stage))
           {
               Query.Append(" and JobStage like '" + stage + "%'");
           }
           if (!string.IsNullOrEmpty(status))
           {
               Query.Append(" and JobStatus like '" + status + "%'");
           }
           // and IsModified =1 or IsModified=Null 
           string condition2 = " And IsModified = 1";
           //string condition3 = " And IsModified is null";
           condition = Query.ToString() + condition2;
           condition1 = Query.ToString();
            DataSet ds = new DataSet();
            try
            {
                string qry = "Select JobNo as [Job], JobStage as [Stage],JobStatus as [Status], Remarks as [Remarks],Importer from View_JobUpdate Where 1=1 " + condition + " Or 1=1" + condition1 + "";    //"; and IsModified = '0' or IsModified is Null"       
               // string qry = "Select JobNo as [Job], JobStage as [Stage],JobStatus as [Status], Remarks as [Remarks],PartyName as [Importer] from View_JobStatus Where 1=1 " + condition + " Or 1=1" + condition1 + "";    //"; and IsModified = '0' or IsModified is Null"            
                
                SqlConnection sqlConn = new SqlConnection(con);
                sqlConn.Open();
                SqlDataAdapter da = new SqlDataAdapter(qry, sqlConn);

                da.Fill(ds, "status");
                sqlConn.Close();
                
            }                
            catch (Exception ex)
            {
                string Msg = ex.Message.ToString();
            }
            return ds;
        }

        public DataSet SearchJobStatuspending()
        {           
            DataSet ds = new DataSet();
            try
            {
                string qry = "Select JobNo as [Job], JobStage as [Stage],JobStatus as [Status], Remarks as [Remarks],Importer from View_JobUpdate Where IsModified is null or IsModified = 1 ";      
                SqlConnection sqlConn = new SqlConnection(con);
                sqlConn.Open();
                SqlDataAdapter da = new SqlDataAdapter(qry, sqlConn);

                da.Fill(ds, "status");
                sqlConn.Close();

            }
            catch (Exception ex)
            {
                string Msg = ex.Message.ToString();
            }
            return ds;
        }

        public DataSet GetJobStatusList(string jobno)
        {
            DataSet ds = new DataSet();
            try
            {
              
                string qry = "Select JobNo as [Job], JobStage as [Stage],JobStatus as [Status], Remarks as [Remarks],Importer, Convert(Varchar(12),JobReceivedDate,103) As [Job Date]," +
                    "Convert(Varchar(12),StatusDate,103) As [Status Date],Mode as [Shipment Type], Id,BENo as [BE No],Convert(Varchar(12), BEDate,103) as [BE Date] from View_JobUpdate Where JobNo = '" + jobno + "'" +
                    " Order by Id desc";
                SqlConnection sqlConn = new SqlConnection(con);
                sqlConn.Open();
                SqlDataAdapter da = new SqlDataAdapter(qry, sqlConn);

                da.Fill(ds, "status");
                sqlConn.Close();

            }
            catch (Exception ex)
            {
                string Msg = ex.Message.ToString();
            }
            return ds;
        }

        private string frmdatesplit(string frmdate)
        {
            string[] frmdate1 = frmdate.Split('/');
            string frmdate2 = frmdate1[1] + '/' + frmdate1[0] + '/' + frmdate1[2];
            return frmdate2;
        }

        public int InsertJobStatusMail(string jobno, string stage, string status,string from, string to, string cc, string subject, string comment, string MailAttach,
            string createdby, string createdDate, string modifiedby, string modifiedDate)
        {
            int result = new int();
            string insertMailStatus = "Insert Into T_JobStatusMail(JobNo, JobStage, JobStatus, MailFrom, MailTo, MailCC, MailSubject, MailComment,MailAttach, CreatedBy, CreatedDate, ModifiedBy, ModifiedDate)" +
                "Values ('" + jobno + "','" + stage + "','" + status + "','" + from + "','" + to + "','" + cc + "','" + subject + "'," +
                " '" + ConvertAmp(comment) + "','"+MailAttach+"', '" + createdby + "', '" + frmdatesplit(createdDate) + "','" + modifiedby + "','" + frmdatesplit(modifiedDate) + "')";

            SqlConnection sqlConn = new SqlConnection(con);
            sqlConn.Open();
            SqlCommand cmd = new SqlCommand(insertMailStatus, sqlConn);
            SqlDataAdapter da = new SqlDataAdapter();

            cmd.CommandText = insertMailStatus;
            cmd.Connection = sqlConn;
            da.SelectCommand = cmd;
            result = cmd.ExecuteNonQuery();
            sqlConn.Close();
            return result;
        }

        public int UpdateBEDeatils(string JobNo,string BENo,string BEDate)
        {
            int result = new int();
            string UpdateBE = "";
            if (BEDate != "")
            {
                //UpdateBE = "update T_JobCreation set BENo='" + BENo + "' , BEDate='" + frmdatesplit(BEDate) + "' where JobNo='" + JobNo + "' ";
                UpdateBE = "update T_JobCreation set BENo='" + BENo + "' , BEDate='" + BEDate + "' where JobNo='" + JobNo + "' ";
            }
            else
            {
                UpdateBE = "update T_JobCreation set BENo='" + BENo + "'  where JobNo='" + JobNo + "' ";
            }
            SqlConnection sqlConn = new SqlConnection(con);
            sqlConn.Open();
            SqlCommand cmd = new SqlCommand(UpdateBE, sqlConn);
            SqlDataAdapter da = new SqlDataAdapter();

            cmd.CommandText = UpdateBE;
            cmd.Connection = sqlConn;
            da.SelectCommand = cmd;
            result = cmd.ExecuteNonQuery();
            sqlConn.Close();
            return result;
        }

        public int InsertJodStageStatus(string jobno, string importer, string stage, string status, string statusdate, string remarks, string modified)
        {
            int result = new int();
            string query = "Insert into T_JobStageUpdate(JobNo, ImporterName, JobStage, JobStatus, Remarks, StatusDate,IsModified)" +
                "Values('" + jobno + "','" + importer + "','" + stage + "', '" + status + "', '" + ConvertAmp(remarks) + "','" + frmdatesplit(statusdate) + "', " + modified + ") ";

            SqlConnection sqlConn = new SqlConnection(con);
            sqlConn.Open();
            SqlCommand cmd = new SqlCommand(query, sqlConn);
            SqlDataAdapter da = new SqlDataAdapter();

            cmd.CommandText = query;
            cmd.Connection = sqlConn;
            da.SelectCommand = cmd;
            result = cmd.ExecuteNonQuery();
            sqlConn.Close();
            return result;
        }

        public int UpdateJodStageStatus(int Id, string jobno, string importer, string stage, string status, string statusdate, string remarks)
        {
            int result = new int();
            string query = "Update T_JobStageUpdate Set JobNo='" + jobno + "', ImporterName='" + importer + "', JobStage='" + stage + "', JobStatus='" + status + "'," +
            "Remarks='" + ConvertAmp(remarks) + "', StatusDate='" + frmdatesplit(statusdate) + "'  Where Id = '" + Id + "'";

            SqlConnection sqlConn = new SqlConnection(con);
            sqlConn.Open();
            SqlCommand cmd = new SqlCommand(query, sqlConn);
            SqlDataAdapter da = new SqlDataAdapter();

            cmd.CommandText = query;
            cmd.Connection = sqlConn;
            da.SelectCommand = cmd;
            result = cmd.ExecuteNonQuery();
            sqlConn.Close();
            return result;
        }

        public DataSet SelectJobStageDetails(int id)
        {
            DataSet ds = new DataSet();
            try
            {
                string qry = "Select JobNo as [Job], JobStage as [Stage],JobStatus as [Status], Remarks as [Remarks],Convert(Varchar(12),StatusDate,103) As [Status Date] from T_JobStageUpdate Where Id = '" + id + "' ";
                SqlConnection sqlConn = new SqlConnection(con);
                sqlConn.Open();
                SqlDataAdapter da = new SqlDataAdapter(qry, sqlConn);

                da.Fill(ds, "status");
                sqlConn.Close();

            }
            catch (Exception ex)
            {
                string Msg = ex.Message.ToString();
            }
            return ds;
        }

        public DataSet GetStageId(string stage)
        {
            DataSet ds = new DataSet();
            try
            {
                string Query = "select StageId, StageName from M_Stage Where StageName = '" + stage + "' ";

                SqlConnection sqlConn = new SqlConnection(con);
                sqlConn.Open();
                SqlDataAdapter da = new SqlDataAdapter(Query, sqlConn);

                da.Fill(ds, "stage");
                sqlConn.Close();
            }
            catch (Exception ex)
            {
                string Msg = ex.Message.ToString();
            }
            return ds;
        }

        public DataSet GetStageStatusDetails(string jobno, string stage, string status)
        {
            DataSet ds = new DataSet();
            try
            {
                string qry = "Select JobNo as [Job], JobStage as [Stage],JobStatus as [Status] from T_JobStageUpdate Where JobNo = '" + jobno + "' and JobStage='" + stage + "' and JobStatus= '" + status + "' ";
                SqlConnection sqlConn = new SqlConnection(con);
                sqlConn.Open();
                SqlDataAdapter da = new SqlDataAdapter(qry, sqlConn);

                da.Fill(ds, "status");
                sqlConn.Close();

            }
            catch (Exception ex)
            {
                string Msg = ex.Message.ToString();
            }
            return ds;
        }

        public int ModifyJobStageDetails(string jobno, string modified)
        {
            int result = new int();
            string query = "Update T_JobStageUpdate Set JobNo='" + jobno + "', IsModified= " + modified + "  Where JobNo = '" + jobno + "'";

            SqlConnection sqlConn = new SqlConnection(con);
            sqlConn.Open();
            SqlCommand cmd = new SqlCommand(query, sqlConn);
            SqlDataAdapter da = new SqlDataAdapter();

            cmd.CommandText = query;
            cmd.Connection = sqlConn;
            da.SelectCommand = cmd;
            result = cmd.ExecuteNonQuery();
            sqlConn.Close();
            return result;
        }

        public DataSet SelectJobDetails(string jobno)
        {
            DataSet ds = new DataSet();
            try
            {
                string qry = "Select JobNo as [Job] from T_JobStageUpdate Where JobNo = '" + jobno + "' ";
                SqlConnection sqlConn = new SqlConnection(con);
                sqlConn.Open();
                SqlDataAdapter da = new SqlDataAdapter(qry, sqlConn);

                da.Fill(ds, "status");
                sqlConn.Close();

            }
            catch (Exception ex)
            {
                string Msg = ex.Message.ToString();
            }
            return ds;
        }

        public DataSet SelectJobNoBeNo(string Jobno)
        {
            DataSet ds = new DataSet();
            try
            {
                string qry = "Select JobNo  from T_JobCreation Where JobNo = '" + Jobno + "' ";
                SqlConnection sqlConn = new SqlConnection(con);
                sqlConn.Open();
                SqlDataAdapter da = new SqlDataAdapter(qry, sqlConn);

                da.Fill(ds, "BENo");
                sqlConn.Close();

            }
            catch (Exception ex)
            {
                string Msg = ex.Message.ToString();
            }
            return ds;
        }

        public static string ConvertAmp(string st)
        {
            string res = "";
            int i;
            for (i = 0; (i <= (st.Length - 1)); i++)
            {
                if (st[i].ToString() == "&")
                {
                    res = (res + "&amp;");
                }
                else if ((st[i].ToString() == "'"))
                {
                    res = (res + "''");
                }
                else
                {
                    res = (res + st[i].ToString());
                }
            }
            return res;
        }
    }
}
