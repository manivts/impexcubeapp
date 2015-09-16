using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;


namespace VTS.ImpexCube.Data.CRM
{
    
    public class StandardRateDL
    {
        string con = (string)ConfigurationManager.ConnectionStrings["Constr"].ConnectionString;

        public int InsertStandardRate(string Description,string twentyFeetUnit,string twentyFeetRate,string FourtyFeetUnit,string FourtyFeetRate,
            string LCLUnit,string LCLRate,string FCLUnit,string FCLRate,string CreatedBy,string CreatedDate)
        {
            

            int result = new int();
            string insertStandard = " insert into M_StandardRate(Description,[20FeetUnit],[20FeetRate],[40FeetUnit],[40FeetRate],LCLUnit,LCLRate,FCLUnit,FCLRate,CreatedBy,CreatedDate)" +
                                    " values ('" + Description + "','" + twentyFeetUnit + "','" + twentyFeetRate + "','" + FourtyFeetUnit + "','" + FourtyFeetRate + "' " +
                                    " ,'" + LCLUnit + "','" + LCLRate + "','" + FCLUnit + "','" + FCLRate + "','" + CreatedBy + "','" +frmdatesplit(CreatedDate) + "') ";
        
            SqlConnection sqlConn = new SqlConnection(con);
            sqlConn.Open();
            SqlCommand cmd = new SqlCommand(insertStandard, sqlConn);
            SqlDataAdapter da = new SqlDataAdapter();

            cmd.CommandText = insertStandard;
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

        public DataSet LoadDescription()
        {

            DataSet ds = new DataSet();
            SqlConnection sqlcon = new SqlConnection(con);
            string query = "select * from M_Charge";
            SqlDataAdapter da = new SqlDataAdapter(query, sqlcon);
            da.Fill(ds, "Charges");
            sqlcon.Close();
            return ds;

        }

        public DataSet GridLoad()
        {

            DataSet ds = new DataSet();
            SqlConnection sqlcon = new SqlConnection(con);
            string query = "select ID,Description,[20FeetUnit],[20FeetRate],[40FeetUnit],[40FeetRate],LCLUnit,LCLRate,FCLUnit,FCLRate from M_StandardRate";
            SqlDataAdapter da = new SqlDataAdapter(query, sqlcon);
            da.Fill(ds, "StandardRateDetails");
            sqlcon.Close();
            return ds;

        }


        public int UpdateStandardRate(string Description, string twentyFeetUnit, string twentyFeetRate, string FourtyFeetUnit, string FourtyFeetRate,
            string LCLUnit, string LCLRate, string FCLUnit, string FCLRate, string ModifiedBy, string ModifiedDate,string ID)
        {
            int result = new int();
            
            string UpdateStandard = " update  M_StandardRate set Description='" + Description + "', [20FeetUnit]='" + twentyFeetUnit + "', [20FeetRate]='" + twentyFeetRate + "', "+
                " [40FeetUnit]='" + FourtyFeetUnit + "',[40FeetRate]='" + FourtyFeetRate + "', LCLUnit='" + LCLUnit + "', LCLRate='" + LCLRate + "',FCLUnit='" + FCLUnit + "',FCLRate='" + FCLRate + "',ModifiedBy='" + ModifiedBy + "',ModifiedDate='" + frmdatesplit(ModifiedDate) + "' where ID='"+ID+"' ";

            SqlConnection sqlConn = new SqlConnection(con);
            sqlConn.Open();
            SqlCommand cmd = new SqlCommand(UpdateStandard, sqlConn);
            SqlDataAdapter da = new SqlDataAdapter();

            cmd.CommandText = UpdateStandard;
            cmd.Connection = sqlConn;
            da.SelectCommand = cmd;
            result = cmd.ExecuteNonQuery();
            sqlConn.Close();
            return result;
        }

    }
}
