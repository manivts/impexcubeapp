using System;
using System.Data;
using System.Configuration;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using MySql;
using MySql.Data.MySqlClient;
namespace VTS.ImpexCube.Data
{
   public class pJobDL
    {
       string strconnJSU = (string)ConfigurationManager.AppSettings["connectionJSU"];
       string strconnVI = (string)ConfigurationManager.AppSettings["ConnectionVisual"];
       
       //string con = (string)ConfigurationManager.ConnectionStrings["Constr"].ConnectionString;
       //string strconn = (string)ConfigurationManager.AppSettings["ConnectionVisual"];
       //string strconnJSU = (string)ConfigurationManager.AppSettings["connectionJSU"];

        public DataSet GetParty(string pcode)
        {
            MySqlConnection conn = new MySqlConnection(strconnVI);
            string sqlQuery = "select * from prt_mast where party_code='" + pcode + "'";
            MySqlDataAdapter da = new MySqlDataAdapter(sqlQuery, conn);
            DataSet ds = new DataSet();
            da.Fill(ds, "Party");
            return ds;
        }
        public DataSet GetiWorkreg(string jno, string strconnVI1,string fy)
        {
            MySqlConnection conn = new MySqlConnection(strconnVI1);
            string sqlQuery = "select * from iworkreg where job_no like '%" + jno + "%' and job_no like '%" + fy + "%'";
            MySqlDataAdapter da = new MySqlDataAdapter(sqlQuery, conn);
            DataSet ds = new DataSet();
            da.Fill(ds, "iworkreg");
            return ds;
        }
        public DataSet GetJobs(string sqlQuery, string strconnJSU1)
        {
            MySqlConnection conn = new MySqlConnection(strconnJSU1);
            conn.Open();
            MySqlDataAdapter da = new MySqlDataAdapter(sqlQuery, conn);
            DataSet ds = new DataSet();
            da.Fill(ds, "jobs");
            return ds;
            conn.Close();
        }
      
        public int SyncJobs(string sqlquery)
        {
            MySqlConnection conn = new MySqlConnection(strconnJSU);
            conn.Open();
            MySqlCommand cmd = new MySqlCommand(sqlquery, conn);
            MySqlDataAdapter da = new MySqlDataAdapter();
            MySqlCommand upcmd = new MySqlCommand(sqlquery, conn);
            upcmd.CommandText = sqlquery;
            upcmd.Connection = conn;
            da.SelectCommand = upcmd;
            int result = upcmd.ExecuteNonQuery();
            conn.Close();
            return result;

        }
    }
}
