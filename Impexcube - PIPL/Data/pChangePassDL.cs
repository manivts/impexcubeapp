using System;
using System.Data;
using System.Configuration;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using System.Web;
using System.Data.SqlClient;

namespace VTS.ImpexCube.Data
{
  public class pChangePassDL
    {
      string strconn = (string)ConfigurationManager.ConnectionStrings["Constr"].ConnectionString;
      

      public DataSet GetData(string userName)
      {
          SqlConnection conn = new SqlConnection(strconn);
          string sqlString = "select * from employee where empname='" + userName + "'";
          SqlDataAdapter da = new SqlDataAdapter(sqlString, conn);
          DataSet ds = new DataSet();
          da.Fill(ds, "Emp");

          return ds;
      }

      public int updataPassword(string NewPwd, string userName,string pwd)
      {
          SqlConnection conn = new SqlConnection(strconn);
          string sqlQuery = " update employee set empid='" + NewPwd + "'" +
                             " where empname='" + userName + "' and empid='" + pwd + "'";

          conn.Open();
          SqlDataAdapter da1 = new SqlDataAdapter();
          SqlCommand cmd = new SqlCommand(sqlQuery, conn);
          cmd.CommandText = sqlQuery;
          cmd.Connection = conn;
          da1.SelectCommand = cmd;

          int result = cmd.ExecuteNonQuery();
          conn.Close();
          return result;
      }
    }
}
