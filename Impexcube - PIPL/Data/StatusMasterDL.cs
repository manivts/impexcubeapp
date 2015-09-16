// -----------------------------------------------------------------------
// <copyright file="StatusMasterDL.cs" company="">
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
    using System.Data.SqlClient;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class StatusMasterDL
    {
        string con = (string)ConfigurationManager.ConnectionStrings["Constr"].ConnectionString;

        public DataSet SelectStatusId()
        {
            DataSet ds = new DataSet();
            try
            {
                string Query = "select MAX(Id) as [Id] from T_StageStatus";

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

        public DataSet SelectStatusDetails()
        {
            DataSet ds = new DataSet();
            try
            {
                string Query = "select s.Id as [Id] , s.StageName as [Stage] ,s.StatusName as [Status] , s.CommunicationType as [Communication],"+
                    " s.MailSubject as [Subject], s.MailComment as [Comment] from T_StageStatus s ";

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

        public int InsertStatusDetails(string status, int stageid, string communication, string subject, string comment, string createdby, string createddate, string modifiedby, string modifieddate, string stagename)
        {
            int result = new int();
            string insertstatus = "Insert Into T_StageStatus(StageId,StatusName,CommunicationType,MailSubject, MailComment,Createdby,CreatedDate,ModifiedBy,ModifiedDate,StageName)" +
                "Values ('" + stageid + "','" + ConvertAmp(status) + "', '" + communication + "' , '" + ConvertAmp(subject) + "', '" + ConvertAmp(comment) + "' ,"+
               " '" + createdby + "','" + frmdatesplit(createddate) + "','" + modifiedby + "','" + frmdatesplit(modifieddate) + "', '" + stagename + "')";

            SqlConnection sqlConn = new SqlConnection(con);
            sqlConn.Open();
            SqlCommand cmd = new SqlCommand(insertstatus, sqlConn);
            SqlDataAdapter da = new SqlDataAdapter();

            cmd.CommandText = insertstatus;
            cmd.Connection = sqlConn;
            da.SelectCommand = cmd;
            result = cmd.ExecuteNonQuery();
            sqlConn.Close();
            return result;
        }

        public int UpdateStatusDetails(int id, string status, int stageid, string communication, string subject, string comment, string modifiedby, string modifieddate, string stagename)
        {
            int result = new int();
            string updatestatus = "Update T_StageStatus set StageId = '" + stageid + "', StatusName= '" + ConvertAmp(status) + "',CommunicationType= '" + communication + "'," +
                " MailSubject = '" + ConvertAmp(subject) + "', MailComment = '" + ConvertAmp(comment) + "',ModifiedBy= '" + modifiedby + "'," +
                " ModifiedDate= '" + frmdatesplit(modifieddate) + "' , StageName = '" + stagename + "' where Id = '" + id + "'";               

            SqlConnection sqlConn = new SqlConnection(con);
            sqlConn.Open();
            SqlCommand cmd = new SqlCommand(updatestatus, sqlConn);
            SqlDataAdapter da = new SqlDataAdapter();

            cmd.CommandText = updatestatus;
            cmd.Connection = sqlConn;
            da.SelectCommand = cmd;
            result = cmd.ExecuteNonQuery();
            sqlConn.Close();
            return result;
        }

        private string frmdatesplit(string frmdate)
        {
            string[] frmdate1 = frmdate.Split('/');
            string frmdate2 = frmdate1[1] + '/' + frmdate1[0] + '/' + frmdate1[2];
            return frmdate2;
        }

        public static string ConvertAmp(string st)
        {
            string res = "";
            int i;
            for (i = 0; (i <= (st.Length - 1)); i++)
            {
                //if (st[i].ToString() == "&")
                //{
                //    res = (res + "&");
                //}
                if ((st[i].ToString() == "'"))
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
