// -----------------------------------------------------------------------
// <copyright file="StageMasterDL.cs" company="">
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
    public class StageMasterDL
    {
        string con = (string)ConfigurationManager.ConnectionStrings["Constr"].ConnectionString;

        public DataSet SelectStageId()
        {
            DataSet ds = new DataSet();
            try
            {
                string Query = "select MAX(StageId) as [StageId] from M_Stage";

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

        public DataSet SelectStage()
        {
            DataSet ds = new DataSet();
            try
            {
                string Query = "select StageId, StageName as [Stage] from M_Stage";

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

        public int InsertStageDetails(string Stage, string createdby, string createddate, string modifiedby, string modifieddate)
        {
            int result = new int();
            string insertstage = "Insert Into M_Stage(StageName,Createdby,CreatedDate,ModifiedBy,ModifiedDate)" +
                "Values ('" + ConvertAmp(Stage) + "','" + createdby + "','" + frmdatesplit(createddate) + "','" + modifiedby + "','" + frmdatesplit(modifieddate) + "')";

            SqlConnection sqlConn = new SqlConnection(con);
            sqlConn.Open();
            SqlCommand cmd = new SqlCommand(insertstage, sqlConn);
            SqlDataAdapter da = new SqlDataAdapter();

            cmd.CommandText = insertstage;
            cmd.Connection = sqlConn;
            da.SelectCommand = cmd;
            result = cmd.ExecuteNonQuery();
            sqlConn.Close();
            return result;
        }

        public int UpdateStageDetails(int id, string Stage, string modifiedby, string modifieddate)
        {
            int result = new int();
            string updatestage = "Update M_Stage set StageName= '" + ConvertAmp(Stage) + "' ,ModifiedBy ='" + modifiedby + "',ModifiedDate='" + frmdatesplit(modifieddate) + "' where StageId = '" + id + "'";                

            SqlConnection sqlConn = new SqlConnection(con);
            sqlConn.Open();
            SqlCommand cmd = new SqlCommand(updatestage, sqlConn);
            SqlDataAdapter da = new SqlDataAdapter();

            cmd.CommandText = updatestage;
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
                //    res = (res + "&amp;");
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
