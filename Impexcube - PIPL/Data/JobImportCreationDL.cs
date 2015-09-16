using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;


namespace VTS.ImpexCube.Data
{
    

    public class JobImportCreationDL
    {
        string con = (string)ConfigurationManager.ConnectionStrings["Constr"].ConnectionString;

        public int insertJobImportCreation(string JobNo,string JobDate,string ShipmentType,string TypeofShipment,string ImpxExpName,string ImpExpCode,string ImpExpBranchCode,
            string ImpExpAddress,string ImpExpClassType,string PortOfOrigin,string OriginPortCode,string OriginStateCode,string OriginCountryCode,string PortOfDestination,
            string DestinationPortCode, string DestinationStateCode, string DestinationCountryCode, string InvoiceNo, string InvoiceDate, string ConsigneeAddress, string ForeignExchangeBankCode,string FYear, string CreatedBy, string CreatedDate, string ModifiedBy, string ModifiedDate)
        {
            int result = new int();
            string InsertQuery = "";
            if (InvoiceDate != "")
            {

                InsertQuery = "Insert into M_JobImportCreation ([JobNo],[JobDate],[ShipmentType],[TypeOfShipment],[ImpExpName],[ImpExpCode],[ImpExpBranchCode], " +
                 " [ImpExpAddress],[ImpExpCassType],[PortOfOrigin],[OriginPortCode],[OriginStateCode],[OriginCountryCode],[PortOfDestination],[DestinationPortCode], " +
                 "[DestinationStateCode],[DestinationCountryCode],[InvoiceNo],[InvoiceDate],[ConsigneeAddress],[ForeignExchangeBankCode],[FYear],[CreatedBy],[CreatedDate],[ModifiedBy],[ModifiedDate]) values" +
                 " ('" + JobNo + "','" + frmdatesplit(JobDate) + "','" + ShipmentType + "','" + TypeofShipment + "','" + ImpxExpName + "','" + ImpExpCode + "','" + ImpExpBranchCode + "', " +
                 " '" + ImpExpAddress + "','" + ImpExpClassType + "','" + PortOfOrigin + "','" + OriginPortCode + "','" + OriginStateCode + "','" + OriginCountryCode + "','" + PortOfDestination + "', " +
                 " '" + DestinationPortCode + "','" + DestinationStateCode + "','" + DestinationCountryCode + "','" + InvoiceNo + "','" + frmdatesplit(InvoiceDate) + "','" + ConsigneeAddress + "','" + ForeignExchangeBankCode + "','"+FYear+"','" + CreatedBy + "','" + frmdatesplit(CreatedDate) + "','" + ModifiedBy + "','" + frmdatesplit(ModifiedDate) + "' )";


            }
            else
            {

                InsertQuery = "Insert into M_JobImportCreation ([JobNo],[JobDate],[ShipmentType],[TypeOfShipment],[ImpExpName],[ImpExpCode],[ImpExpBranchCode], " +
                 " [ImpExpAddress],[ImpExpCassType],[PortOfOrigin],[OriginPortCode],[OriginStateCode],[OriginCountryCode],[PortOfDestination],[DestinationPortCode], " +
                 "[DestinationStateCode],[DestinationCountryCode],[InvoiceNo],[ConsigneeAddress],[ForeignExchangeBankCode],[FYear],[CreatedBy],[CreatedDate],[ModifiedBy],[ModifiedDate]) values" +
                 " ('" + JobNo + "','" + frmdatesplit(JobDate) + "','" + ShipmentType + "','" + TypeofShipment + "','" + ImpxExpName + "','" + ImpExpCode + "','" + ImpExpBranchCode + "', " +
                 " '" + ImpExpAddress + "','" + ImpExpClassType + "','" + PortOfOrigin + "','" + OriginPortCode + "','" + OriginStateCode + "','" + OriginCountryCode + "','" + PortOfDestination + "', " +
                 " '" + DestinationPortCode + "','" + DestinationStateCode + "','" + DestinationCountryCode + "','" + InvoiceNo + "','" + ConsigneeAddress + "','" + ForeignExchangeBankCode + "','" + FYear + "','" + CreatedBy + "','" + frmdatesplit(CreatedDate) + "','" + ModifiedBy + "','" + frmdatesplit(ModifiedDate) + "' )";
            }

            SqlConnection sqlcon = new SqlConnection(con);
            sqlcon.Open();
            SqlCommand cmd = new SqlCommand(InsertQuery,sqlcon);
            result = cmd.ExecuteNonQuery();
            sqlcon.Close(); 

            return result;
        
        }

        public DataSet GridJobCreation()
        {
            DataSet ds=new DataSet();
            SqlConnection sqlcon=new SqlConnection(con);
            string query = "select A.JobNo,A.JobReceivedDate,B.Importer,C.InvoiceNo,C.InvoiceDate from T_JobCreation as A inner join T_Importer as B on A.JobNo=B.JobNo inner join T_InvoiceDetails as C on A.JobNo=C.JobNo";
            SqlDataAdapter da = new SqlDataAdapter(query, sqlcon);
            da.Fill(ds, "jobcreation");
            sqlcon.Close();
            return ds;
            }


        public DataSet GridJobNo()
        {
            
            DataSet ds = new DataSet();
            SqlConnection sqlcon = new SqlConnection(con);
            string query = "select * from M_JobImportCreation";
            SqlDataAdapter da = new SqlDataAdapter(query,sqlcon);
            da.Fill(ds, "Jobdetails");
            sqlcon.Close();
            return ds;
         
        }
        public DataSet SelectJobNo(string JobNo)
        {

            DataSet ds = new DataSet();
            SqlConnection sqlcon = new SqlConnection(con);
            string query = "select * from M_JobImportCreation where JobNo='"+JobNo+"'";
            SqlDataAdapter da = new SqlDataAdapter(query, sqlcon);
            da.Fill(ds, "Jobdetails");
            sqlcon.Close();
            return ds;
        }

        public DataSet SelectJobCreated(string jobDate)
        {
            DataSet ds = new DataSet();
            SqlConnection sqlcon = new SqlConnection(con);
            string query = "select JobNo,Convert(Varchar(12),JobDate,103) As [Job Date], ImpExpName as [Importer]  from M_JobImportCreation where JobDate='" + frmdatesplit(jobDate) + "'";
            SqlDataAdapter da = new SqlDataAdapter(query, sqlcon);
            da.Fill(ds, "Jobdetails");
            sqlcon.Close();
            return ds;
        }

        public int UpdateJobImportCreation(string JobNo, string JobDate, string ShipmentType, string TypeofShipment, string ImpxExpName, string ImpExpCode, string ImpExpBranchCode,
          string ImpExpAddress, string ImpExpClassType, string PortOfOrigin, string OriginPortCode, string OriginStateCode, string OriginCountryCode, string PortOfDestination,
          string DestinationPortCode, string DestinationStateCode, string DestinationCountryCode, string InvoiceNo, string InvoiceDate, string ConsigneeAddress, string ForeignExchangeBankCode, string ModifiedBy, string ModifiedDate,string TransId)
        {
            int result = new int();
            string UpdateQuery = "";
            if (InvoiceDate != "")
            {
                 UpdateQuery = "Update M_JobImportCreation set JobNo='" + JobNo + "',JobDate='" + frmdatesplit(JobDate) + "',ShipmentType='" + ShipmentType + "',TypeOfShipment='" + TypeofShipment + "', " +
                    " ImpExpName='" + ImpxExpName + "',ImpExpCode='" + ImpExpCode + "',ImpExpBranchCode='" + ImpExpBranchCode + "', " +
                    " ImpExpAddress='" + ImpExpAddress + "',ImpExpCassType='" + ImpExpClassType + "',PortOfOrigin='" + PortOfOrigin + "',OriginPortCode='" + OriginPortCode + "', " +
                    " OriginStateCode='" + OriginStateCode + "',OriginCountryCode='" + OriginCountryCode + "',PortOfDestination='" + PortOfDestination + "',DestinationPortCode='" + DestinationPortCode + "', " +
                    " DestinationStateCode='" + DestinationStateCode + "',DestinationCountryCode='" + DestinationCountryCode + "',InvoiceNo='" + InvoiceNo + "',InvoiceDate='" + frmdatesplit(InvoiceDate) + "',ConsigneeAddress='" + ConsigneeAddress + "', " +
                    " ForeignExchangeBankCode='" + ForeignExchangeBankCode + "',ModifiedBy='" + ModifiedBy + "',ModifiedDate='" + frmdatesplit(ModifiedDate) + "' where TransId='" + TransId + "' ";
            }
            else
            {
                 UpdateQuery = "Update M_JobImportCreation set JobNo='" + JobNo + "',JobDate='" + frmdatesplit(JobDate) + "',ShipmentType='" + ShipmentType + "',TypeOfShipment='" + TypeofShipment + "', " +
                     " ImpExpName='" + ImpxExpName + "',ImpExpCode='" + ImpExpCode + "',ImpExpBranchCode='" + ImpExpBranchCode + "', " +
                     " ImpExpAddress='" + ImpExpAddress + "',ImpExpCassType='" + ImpExpClassType + "',PortOfOrigin='" + PortOfOrigin + "',OriginPortCode='" + OriginPortCode + "', " +
                     " OriginStateCode='" + OriginStateCode + "',OriginCountryCode='" + OriginCountryCode + "',PortOfDestination='" + PortOfDestination + "',DestinationPortCode='" + DestinationPortCode + "', " +
                     " DestinationStateCode='" + DestinationStateCode + "',DestinationCountryCode='" + DestinationCountryCode + "',InvoiceNo='" + InvoiceNo + "',ConsigneeAddress='" + ConsigneeAddress + "', " +
                     " ForeignExchangeBankCode='" + ForeignExchangeBankCode + "',ModifiedBy='" + ModifiedBy + "',ModifiedDate='" + frmdatesplit(ModifiedDate) + "' where TransId='" + TransId + "' ";
            }
        

            SqlConnection sqlcon = new SqlConnection(con);
            sqlcon.Open();
            SqlCommand cmd = new SqlCommand(UpdateQuery, sqlcon);
            result = cmd.ExecuteNonQuery();
            sqlcon.Close();

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
