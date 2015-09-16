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
   public class pMailDL
    {
       string strconn = (string)ConfigurationManager.AppSettings["ConnectionImpex"];

       public DataSet GetData()
       {
           string userName = System.Web.HttpContext.Current.Session["USER-NAME"].ToString();
           string CMP = System.Web.HttpContext.Current.Session["COMP"].ToString();
               
           SqlConnection conn = new SqlConnection(strconn);
           string sqlQuery = "select * from tbl_sendMail where mCompany='" + CMP + "' and mUser='" + userName + "' order by sno desc";
           SqlDataAdapter da = new SqlDataAdapter(sqlQuery, conn);
           DataSet ds = new DataSet();
           da.Fill(ds, "SendMail");
           return ds;

       }
       public DataSet GetEmpMail()
       {
           string Gd = "B";
           SqlConnection conn = new SqlConnection(strconn);
           string sqlQuery = "select * from employee where Grade='" + Gd + "'";
           SqlDataAdapter da = new SqlDataAdapter(sqlQuery, conn);
           DataSet ds = new DataSet();
           da.Fill(ds, "SendMail");
           return ds;

       }
       public DataSet GetMail(string sNO)
       {
           SqlConnection conn = new SqlConnection(strconn);
           string sqlQuery = "select * from tbl_sendMail where sno='" + sNO + "'";
           SqlDataAdapter da = new SqlDataAdapter(sqlQuery, conn);
           DataSet ds = new DataSet();
           da.Fill(ds, "SendMail");
           return ds;
       }
       public int deleteMail(int rNO)
       {
           SqlConnection conn = new SqlConnection(strconn);
           string sqlQuery = "delete from tbl_sendMail where sno='" + rNO + "'";
           conn.Open();
           SqlDataAdapter da = new SqlDataAdapter();
           SqlCommand cmd = new SqlCommand(sqlQuery, conn);
           cmd.CommandText = sqlQuery;
           cmd.Connection = conn;
           da.SelectCommand = cmd;

           int result = cmd.ExecuteNonQuery();
           conn.Close();

           return result;
       }
       public int SendMail(string strFrom, string strTo, string strCc, string strBcc, string strSubject, string strMessage,string strAttach, string strDate, string strTime, string strUser, string strCMP)
       {
           SqlConnection conn = new SqlConnection(strconn);
           string sqlQuery = "insert into tbl_sendMail(mFrom,mTo,mCc,mBcc,mSubject,mMessage,mAttach,mDate,mTime,mUser,mCompany) " +
                             "Values('" + strFrom + "','" + strTo + "','" + strCc + "','" + strBcc + "','" + strSubject + "'," +
                             "'" + strMessage + "','" + strAttach + "','" + strDate + "','" + strTime + "','" + strUser + "','" + strCMP + "')";

           conn.Open();
           SqlDataAdapter da = new SqlDataAdapter();
           SqlCommand cmd = new SqlCommand(sqlQuery, conn);
           cmd.CommandText = sqlQuery;
           cmd.Connection = conn;
           da.SelectCommand = cmd;

           int result = cmd.ExecuteNonQuery();

           conn.Close();

           return result;

       }
    }
}
