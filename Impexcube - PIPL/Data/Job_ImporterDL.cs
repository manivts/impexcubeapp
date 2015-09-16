using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace VTS.ImpexCube.Data
{
    public class Job_ImporterDL
    {
        string strconn = (string)ConfigurationManager.ConnectionStrings["Constr"].ConnectionString;

        public DataSet GetImporterDet()
        {
            StringBuilder Query = new StringBuilder();
            DataSet ds = new DataSet();
            try
            {
                Query.Append("SELECT [Imp_ID],([Imp_Name]+ ',' +[Imp_City]) as Importer FROM ImporterDetails");

                string Qry = Query.ToString();
                SqlConnection sqlConn = new SqlConnection(strconn);
                sqlConn.Open();
                SqlDataAdapter da = new SqlDataAdapter(Qry, sqlConn);

                da.Fill(ds, "Imp");
                sqlConn.Close();
            }
            catch (Exception ex)
            {
                string Msg = ex.Message.ToString();
            }
            return ds;
        }

        public DataSet GetImporterDet_ID(string ImpID)
        {
            StringBuilder Query = new StringBuilder();
            DataSet ds = new DataSet();
            try
            {
                Query.Append("SELECT * FROM ImporterDetails where [IMP_ID]='" + ImpID + "'");

                string Qry = Query.ToString();
                SqlConnection sqlConn = new SqlConnection(strconn);
                sqlConn.Open();
                SqlDataAdapter da = new SqlDataAdapter(Qry, sqlConn);

                da.Fill(ds, "ImporterID");
                sqlConn.Close();
            }
            catch (Exception ex)
            {
                string Msg = ex.Message.ToString();
            }
            return ds;
        }

        public int InsertJobImport(string JobNo,string DocRecd,string TransMode,string JobCity,string JobPort,string JobBE,string JobOPDoc,string ImpName,string ImpAddress,
            string ImpCity,string ImpContact,string ImpState,string ImpPortal,string ImpEmail,string ImpPhone,string ImpFax,string ImpBin,string ImpIECode)
        {
            StringBuilder Query = new StringBuilder();
            int result = new int();
            SqlConnection sqlConn = new SqlConnection(strconn);

            try
            {
                Query.Append("INSERT INTO Job_Import ([JobNo],[Job_DocRecdDate],[Job_TransportMode],[Job_City],[Job_Port],[Job_BEType],[Job_OPDoc],[Job_Importer],[Job_ImpAddress],");
                Query.Append("[Job_ImpCity],[Job_ImpState],[Job_ImpPortalCode],[Job_ImpContact],[Job_ImpPhone],[Job_ImpFax],[Job_ImpEmail],[Job_ImpBinNo],[Job_ImpIECode])");
                Query.Append("VALUES (");
                Query.Append(" '" + JobNo + "','" + frmdatesplit(DocRecd) + "', '" + TransMode + "', '" + JobCity + "','" + JobPort + "','" + JobBE + "','" + JobOPDoc + "','" + ImpName + "',");
                Query.Append(" '" + ImpAddress + "','" + ImpCity + "','" + ImpState + "' , '" + ImpPortal + "','" + ImpContact + "','" + ImpPhone + "',");
                Query.Append(" '" + ImpFax + "','" + ImpEmail + "','" + ImpBin + "','" + ImpIECode + "' )");

                sqlConn.Open();
                SqlCommand cmd = new SqlCommand(Query.ToString(), sqlConn);
                SqlDataAdapter da = new SqlDataAdapter();

                cmd.CommandText = Query.ToString();
                cmd.Connection = sqlConn;
                da.SelectCommand = cmd;
                result = cmd.ExecuteNonQuery();               
            }
            catch (Exception ex)
            {
                string Msg = ex.Message.ToString();
            }
            finally
            {
                sqlConn.Close();               
            }
            return result;
        }

          private string frmdatesplit(string Jobdate)
        {
            string[] Jobdate1 = Jobdate.Split('/');
            string Jobdate2 = Jobdate1[1] + '/' + Jobdate1[0] + '/' + Jobdate1[2];
            return Jobdate2;
        }
    }
}
