// -----------------------------------------------------------------------
// <copyright file="UserReportsDL.cs" company="">
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
    public class UserReportsDL
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
                string Query = "select Id, StatusName from T_StageStatus where StageId='" + StageId + "'";

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

        public DataSet SearchJobReportList(string From, string To, string Importer, string Jobno, string stage, string status, string shipment, string shipmentType)
        {
            string ismodified = null;
            StringBuilder Query = new StringBuilder();
            string condition = string.Empty;
            string condition1 = string.Empty;
            if (!string.IsNullOrEmpty(From))
            {
                Query.Append(" and StatusDate >= '" + frmdatesplit(From) + "'");
            }
            if (!string.IsNullOrEmpty(To))
            {
                Query.Append(" and StatusDate <= '" + frmdatesplit(To) + "'");
            }
            if (!string.IsNullOrEmpty(Importer))
            {
                Query.Append(" and ImpExpName like '" + Importer + "%'");
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
            if (!string.IsNullOrEmpty(shipment))
            {
                Query.Append(" and ShipmentType like '" + shipment + "%'");
            }
            if (!string.IsNullOrEmpty(shipmentType))
            {
                Query.Append(" and TypeOfShipment like '" + shipmentType + "%'");
            }
            // and IsModified =1 or IsModified=Null 
            string condition2 = " And IsModified = 0";
            string condition3 = " And IsModified is null";
            condition = Query.ToString() + condition2;
            condition1 = Query.ToString() + condition3;
            DataSet ds = new DataSet();
            try
            {
                string qry = "Select JobNo as [Job], JobStage as [Stage],JobStatus as [Status], Remarks as [Remarks],ImpExpName as [Importer], Convert(Varchar(12),StatusDate,103) As [Date], ShipmentType as [Shipment], TypeOfShipment as [ShipmentType] from View_JobReports Where 1=1 " + condition + " Or 1=1" + condition1 + "";    //"; and IsModified = '0' or IsModified is Null"            
                SqlConnection sqlConn = new SqlConnection(con);
                sqlConn.Open();
                SqlDataAdapter da = new SqlDataAdapter(qry, sqlConn);

                da.Fill(ds, "reports");
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

    }
}
