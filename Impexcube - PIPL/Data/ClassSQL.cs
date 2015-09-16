using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Data.SqlClient;

namespace VTS.ImpexCube.Data
{
    public class ClassSQL
    {

        System.Data.SqlClient.SqlConnection objCon;
        public System.Data.SqlClient.SqlConnection clsData(string conStr)
        {
            objCon = new System.Data.SqlClient.SqlConnection(conStr);
             return objCon;
        }
        public int Execute(string qry)
        {
            System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
            cmd.Connection = objCon;
            int Rcnt = 0;
            cmd.CommandText = qry;
            if (objCon.State == ConnectionState.Closed)
                objCon.Open();
            Rcnt = cmd.ExecuteNonQuery();
            if (objCon.State == ConnectionState.Open)
                objCon.Close();
            return Rcnt;
        }
        public DataTable GetTable(string qry)
        {
            DataTable Tbl = new DataTable();
            System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand(qry, objCon);
            System.Data.SqlClient.SqlDataAdapter adp = new System.Data.SqlClient.SqlDataAdapter(cmd);
            adp.Fill(Tbl);
            return Tbl;
        }

        public int DoInsert(string Tbl, Hashtable HS)
        {

            IDictionaryEnumerator myEnumerator = HS.GetEnumerator();
            System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
            cmd.Connection = objCon;
            string qry = null;
            int Rcnt = 0;
            string colLst = "";
            string valLst = "";
            while (myEnumerator.MoveNext())
            {
                colLst += myEnumerator.Key + ",";
                valLst += "@" + myEnumerator.Key + ",";
                cmd.Parameters.AddWithValue("@" + myEnumerator.Key, Convert.ToString(myEnumerator.Value));

            }
            char qut = ',';

            colLst = colLst.Trim().TrimEnd(qut);
            valLst = valLst.Trim().TrimEnd(qut);
             qry = "insert into " + Tbl + " ( " + colLst + " ) values ( " + valLst + " ) ";
            cmd.CommandText = qry;
            if (objCon.State == ConnectionState.Closed)
                objCon.Open();
            Rcnt = cmd.ExecuteNonQuery();
            if (objCon.State == ConnectionState.Open)
                objCon.Close();
            return Rcnt;
        }

        public int DoUpdate(string Tbl, Hashtable HS, string strcondition)
        {

            IDictionaryEnumerator myEnumerator = HS.GetEnumerator();
            System.Data.SqlClient.SqlCommand cmd = new System.Data.SqlClient.SqlCommand();
            cmd.Connection = objCon;
            string qry = null;
            int Rcnt = 0;
            string strLst = "";
            while (myEnumerator.MoveNext())
            {
                strLst += myEnumerator.Key + "=@" + myEnumerator.Key + ",";
                //cmd.Parameters.Add("@" & myEnumerator.Key, CStr(myEnumerator.Value))
                cmd.Parameters.AddWithValue("@" + myEnumerator.Key, Convert.ToString(myEnumerator.Value));
            }
            char quto = ',';
            strLst = strLst.Trim().TrimEnd(quto);
            //strLst = strLst.Trim().TrimEnd(",");
            qry = "Update " + Tbl + " set  " + strLst + " where " + strcondition;
            cmd.CommandText = qry;
            if (objCon.State == ConnectionState.Closed)
                objCon.Open();
            Rcnt = cmd.ExecuteNonQuery();
            if (objCon.State == ConnectionState.Open)
                objCon.Close();
            return Rcnt;

        }
      
    }
}