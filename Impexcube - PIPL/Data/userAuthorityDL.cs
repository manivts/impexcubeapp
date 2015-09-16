using System;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;

namespace VTS.ImpexCube.Data
{
   public class userAuthorityDL
    {
       string strconn = (string)ConfigurationManager.ConnectionStrings["Constr"].ConnectionString;

       public DataSet GetData(string name)
       {
           string sqlQuery="";
           SqlConnection conn = new SqlConnection(strconn);
           if(name=="VTS")
               sqlQuery = "select * from employee";
           else
               sqlQuery = "select * from employee where empName !='VTS'";
           SqlDataAdapter da = new SqlDataAdapter(sqlQuery, conn);
           DataSet ds = new DataSet();
           da.Fill(ds, "emp");
           return ds;
       }
       public DataSet GetBranch()
       {

           SqlConnection conn = new SqlConnection(strconn);
           string sqlQuery = "select distinct city from tbl_branch";
           SqlDataAdapter da = new SqlDataAdapter(sqlQuery, conn);
           DataSet ds = new DataSet();
           da.Fill(ds, "branch");
           return ds;
       }
       public DataSet GetEMPName(string eName)
       {
           SqlConnection conn = new SqlConnection(strconn);
           string sqlQuery = "select * from employee where empName='" + eName + "'";
           SqlDataAdapter da = new SqlDataAdapter(sqlQuery, conn);
           DataSet ds = new DataSet();
           da.Fill(ds, "emp");

           return ds;

       }
       public DataSet GetEMP(string UID)
       {
           SqlConnection conn = new SqlConnection(strconn);
           string sqlQuery = "select * from employee where uid='" + UID + "'";
           SqlDataAdapter da = new SqlDataAdapter(sqlQuery, conn);
           DataSet ds = new DataSet();
           da.Fill(ds, "emp");

           return ds;

       }
       public DataSet GetUser(string EID)
       {
        SqlConnection conn = new SqlConnection(strconn);
        string sqlQuery = "select * from M_user where empid='" + EID + "'";
        SqlDataAdapter da = new SqlDataAdapter(sqlQuery, conn);
        DataSet ds = new DataSet();
        da.Fill(ds, "auth");
        return ds;
       }
       public DataSet GetUser(string EID, string formCode)
       {
           SqlConnection conn = new SqlConnection(strconn);
           string sqlQuery = "select * from M_user where empid='" + EID + "' and formID='" + formCode + "'";
           SqlDataAdapter da = new SqlDataAdapter(sqlQuery, conn);
           DataSet ds = new DataSet();
           da.Fill(ds, "Forms");
           return ds;
           
       }
       public DataSet GetUserAuth(Int32 strUID)
       {
           //Delete User Authorization Table
           SqlConnection conn = new SqlConnection(strconn);
           string sqlQuery = "select * from employee where uid='" + strUID + "'";
           SqlDataAdapter da = new SqlDataAdapter(sqlQuery, conn);
           DataSet ds = new DataSet();
           da.Fill(ds, "empN");
           return ds;
       }
       public DataSet GetForms(string CMP,string eid,string F1,string F2)
       {
           string sqlQuery;
           SqlConnection conn = new SqlConnection(strconn);
           if (CMP == "PIPL")
           {
               sqlQuery = "select * from M_user where empid='" + eid + "' ";
           }
           else
           {
               sqlQuery = "select * from M_user where empid='" + eid + "' " +
                          "and formid !='" + F1 + "' and formid !='" + F2 + "'";
           }
           SqlDataAdapter da = new SqlDataAdapter(sqlQuery, conn);
           DataSet ds = new DataSet();
           da.Fill(ds, "Forms");
           return ds;
       }
       public int createNewUser(string strPass, string strName, string strZone, string strGrade, string strMail, string empName)
       {
           SqlConnection conn = new SqlConnection(strconn);

           string sqlQuery = "insert into employee(empID,empNAME,employeeName,Zone,Grade, mail) " +
                             "VALUES('" + strPass + "','" + strName + "','" + empName + "','" + strZone + "','" + strGrade + "','" + strMail + "') ";

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
       public int createUserAuthority(string formID,string formName,string ename,string EID,string Branch,string DIS,string READ)
       {
           SqlConnection conn = new SqlConnection(strconn); 
           string sqlQuery = "Insert into M_user(formid,formName,empName,empid,branch,disable,ReadOnly)" +
                             " values('" + formID + "','" + formName + "','" + ename + "','" + EID + "','" + Branch + "','" + DIS + "','" + READ + "')";



           conn.Open();
           SqlDataAdapter da = new SqlDataAdapter();
           SqlCommand cmd = new SqlCommand(sqlQuery, conn);
           cmd.CommandText = sqlQuery;
           cmd.Connection = conn;
           da.SelectCommand = cmd;

           int result= cmd.ExecuteNonQuery();
           conn.Close();
           return result;
       }

       public int updateDisableForms(string strDisable, string userAuth, string formName)
       {
           SqlConnection conn = new SqlConnection(strconn);

           string sqlQuery = " Update M_user set disable='" + strDisable + "'" +
                   " where empid='" + userAuth + "' and formName='" + formName + "'";

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

       public int updateReadOnlyForms(string strREAD, string userAuth, string formName)
       {
           SqlConnection conn = new SqlConnection(strconn);

           string sqlQuery = " Update M_user set ReadOnly='" + strREAD + "'" +
                              " where empid='" + userAuth + "' and formName='" + formName + "'";

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
       public int deleteUser(Int32 strUID)
       {
           
           SqlConnection conn = new SqlConnection(strconn);
           
           string lstrsql = "delete from employee where uid='" + strUID + "'";
           conn.Open();
           SqlCommand cmd = new SqlCommand(lstrsql, conn);
           SqlDataAdapter da = new SqlDataAdapter();
           cmd.CommandText = lstrsql;
           cmd.Connection = conn;
           da.SelectCommand = cmd;

           int result = cmd.ExecuteNonQuery();
           conn.Close();

           return result;
       }
       
          
       public int deleteUserAuth(string EName)
       {
           SqlConnection conn = new SqlConnection(strconn);
           string lstrsql = "delete from M_user where EmpName='" + EName + "'";
           conn.Open();
           SqlCommand cmd = new SqlCommand(lstrsql, conn);
           SqlDataAdapter da = new SqlDataAdapter();
           cmd.CommandText = lstrsql;
           cmd.Connection = conn;
           da.SelectCommand = cmd;

           int result = cmd.ExecuteNonQuery();
           conn.Close();

           return result;

       }
       public int updataPassword(Int32 userId, string userName, string pwd)
       {
           SqlConnection conn = new SqlConnection(strconn);
           string sqlQuery = " update employee set empid='" + pwd + "'" +
                              " where empname='" + userName + "' and uid='" + userId + "'";

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
