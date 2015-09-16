using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace VTS.ImpexCube.Data
{

    public class DashBoardDL
    {

        string con = (string)ConfigurationManager.ConnectionStrings["Constr"].ConnectionString;
        CommonDL objCommon = new CommonDL();

        public DataSet SelectReportDetails()
        {
            DataSet ds = new DataSet();
            try
            {
                string Query = "select * from Report_Details ";

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

        public DataSet SelectBillingDelivery(string stage,string status, string fyear)
        {
            DataSet ds = new DataSet();
            try
            {
                string Query = "select job_no,JobStage, JobStatus,StatusDate,bill_date,bill_date as DeliveryDetail from View_JobStageWiseDetails where JobStage='" + stage + "' and JobStatus='" + status + "' and job_no like '%" + fyear + "%' ";

                SqlConnection sqlConn = new SqlConnection(con);
                sqlConn.Open();
                SqlDataAdapter da = new SqlDataAdapter(Query, sqlConn);

                da.Fill(ds, "stageDetails");
                sqlConn.Close();
            }
            catch (Exception ex)
            {
                string Msg = ex.Message.ToString();
            }
            return ds;
        }

        public int InsertDashboardDetails(string JobNo, string JobStage, string JobStatus, int NoofDays, int NoofCount, string Grade)
        {
            int result = new int();
            string insertDash = "insert into DashboardBillingDelivery(JobNo,JobStage,JobStatus,NoofDays,NoofCount,Grade) values ('" + JobNo + "','" + JobStage + "','" + JobStatus + "','" + NoofDays + "','" + NoofCount + "','" + Grade + "')";
            SqlConnection sqlConn = new SqlConnection(con);
            sqlConn.Open();
            SqlCommand cmd = new SqlCommand(insertDash, sqlConn);
            SqlDataAdapter da = new SqlDataAdapter();

            cmd.CommandText = insertDash;
            cmd.Connection = sqlConn;
            da.SelectCommand = cmd;
            result = cmd.ExecuteNonQuery();
            sqlConn.Close();
            return result;
        }

        public DataSet SelectBillingDeliveryDashboard( string fyear)
        {
            DataSet ds = new DataSet();
            try
            {
                string Query = "select * from DashboardBillingDelivery where jobno like '%" + fyear + "%' ";

                SqlConnection sqlConn = new SqlConnection(con);
                sqlConn.Open();
                SqlDataAdapter da = new SqlDataAdapter(Query, sqlConn);

                da.Fill(ds, "ReportDetails");
                sqlConn.Close();
            }
            catch (Exception ex)
            {
                string Msg = ex.Message.ToString();
            }
            return ds;
        }

        public int DeletDashboardDetails()
        {
            int result = new int();
            string DeleteDash = "delete  DashboardBillingDelivery";

            SqlConnection sqlConn = new SqlConnection(con);
            sqlConn.Open();
            SqlCommand cmd = new SqlCommand(DeleteDash, sqlConn);
            SqlDataAdapter da = new SqlDataAdapter();

            cmd.CommandText = DeleteDash;
            cmd.Connection = sqlConn;
            da.SelectCommand = cmd;
            result = cmd.ExecuteNonQuery();
            sqlConn.Close();
            return result;
        }
        
        public DataSet SelectImpJob(string Uid, string Uname,string Grade)
        {
            string Query = string.Empty;
            DataSet ds = new DataSet();
            try
            {
                if (Grade == "A")
                {
                    Query = "select count(JobNo) as JobNo from T_JobCreation";
                }
                else if (Grade == "B")
                {
                    Query = "select count(JobNo) as JobNo  from T_JobCreation";
                    //Query = "select count(JobNo) from T_JobCreation where createdby='" + Uname + "'  ";
                }
                else if (Grade == "C")
                {
                    Query = "select count(JobNo) as JobNo  from T_JobCreation where createdby='" + Uname + "'  ";
                }
                ds = objCommon.GetDataSet(Query.ToString());
            }
            catch (Exception ex)
            {
                string Msg = ex.Message.ToString();
            }
            return ds;
        }

        public DataSet SelectImpShipMent(string Uid, string Uname, string Grade)
        {
            string Query = string.Empty;
            DataSet ds = new DataSet();
            try
            {
                if (Grade == "A")
                {
                    Query = "select count(JobNo) as JobNo  from T_ShipmentDetails";
                }
                else if (Grade == "B")
                {
                    Query = "select count(JobNo) as JobNo  from T_ShipmentDetails";
                    //Query = "select count(JobNo) from T_ShipmentDetails where createdby='" + Uname + "'  ";
                }
                else if (Grade == "C")
                {
                    Query = "select count(JobNo) as JobNo  from T_ShipmentDetails where createdby='" + Uname + "'  ";
                }

               // string Query = "select JobNo, JobDate,  ShipmentCountry, ShipmentPort, CountryOrigin, PortOfOrigin from T_ShipmentDetails where createdby='" + Uname + "'  ";
                ds = objCommon.GetDataSet(Query.ToString());
            }
            catch (Exception ex)
            {
                string Msg = ex.Message.ToString();
            }
            return ds;
        }

        public DataSet SelectImpInvoice(string Uid, string Uname, string Grade)
        {
            string Query = string.Empty;
            DataSet ds = new DataSet();
            try
            {
                if (Grade == "A")
                {
                    Query = "select count(JobNo) as JobNo  from T_InvoiceDetails";
                }
                else if (Grade == "B")
                {
                    Query = "select count(JobNo) as JobNo  from T_InvoiceDetails";
                    //Query = "select count(JobNo) from T_InvoiceDetails where createdby='" + Uname + "'  ";
                }
                else if (Grade == "C")
                {
                    Query = "select count(JobNo) as JobNo  from T_InvoiceDetails where createdby='" + Uname + "'  ";
                }
                //string Query = "select JobNo, ImporterName, FreightType, FreightTyCurrency from  T_InvoiceDetails where createdby='" + Uname + "'  ";
                ds = objCommon.GetDataSet(Query.ToString());
            }
            catch (Exception ex)
            {
                string Msg = ex.Message.ToString();
            }
            return ds;
        }

        public DataSet SelectImpProduct(string Uid, string Uname, string Grade)
        {
            string Query = string.Empty;
            DataSet ds = new DataSet();
            try
            {
                if (Grade == "A")
                {
                    Query = "select count(JobNo) as JobNo from T_Product";
                }
                else if (Grade == "B")
                {
                    Query = "select count(JobNo) as JobNo from T_Product";
                    //Query = "select count(JobNo) from T_Product where createdby='" + Uname + "'  ";
                }
                else if (Grade == "C")
                {
                    Query = "select count(JobNo) as JobNo from T_Product where createdby='" + Uname + "'  ";
                }
                //string Query = "select JobNo from  T_Product where createdby='" + Uname + "'  ";
                ds = objCommon.GetDataSet(Query.ToString());
            }
            catch (Exception ex)
            {
                string Msg = ex.Message.ToString();
            }
            return ds;
        }

        public DataSet SelectBEFile(string Uid, string Uname, string Grade)
        {
            string Query = string.Empty;
            DataSet ds = new DataSet();
            try
            {
                if (Grade == "A")
                {
                    Query = "select count(JobNo) as JobNo  from T_Product";
                }
                else if (Grade == "B")
                {
                    Query = "select count(JobNo) as JobNo  from T_Product";
                    //Query = "select count(JobNo) from T_Product where createdby='" + Uname + "'  ";
                }
                else if (Grade == "C")
                {
                    Query = "select count(JobNo) as JobNo  from T_Product where createdby='" + Uname + "'  ";
                }
               // string Query = "select JobNo from  T_Product where createdby='" + Uname + "'  ";
                ds = objCommon.GetDataSet(Query.ToString());
            }
            catch (Exception ex)
            {
                string Msg = ex.Message.ToString();
            }
            return ds;
        }

        public DataSet SelectExpJob(string Uid, string Uname, string Grade)
        {
            string Query = string.Empty;
            DataSet ds = new DataSet();
            try
            {
                if (Grade == "A")
                {
                    Query = "select count(JobNo)  as JobNo from E_M_JobCreation";
                }
                else if (Grade == "B")
                {
                    Query = "select count(JobNo)  as JobNo from E_M_JobCreation";
                    //Query = "select count(JobNo) from E_M_JobCreation where createdby='" + Uname + "'  ";
                }
                else if (Grade == "C")
                {
                    Query = "select count(JobNo) as JobNo  from E_M_JobCreation where createdby='" + Uname + "'  ";
                }
                //string Query = "select JobNo,JobDate,TransportMode,TotalNoofInvoice, TotalInvoiceValue from  E_M_JobCreation where createdby='" + Uname + "'  ";
                ds = objCommon.GetDataSet(Query.ToString());
            }
            catch (Exception ex)
            {
                string Msg = ex.Message.ToString();
            }
            return ds;
        }

        public DataSet SelectExpShipMent(string Uid, string Uname, string Grade)
        {
            string Query = string.Empty;
            DataSet ds = new DataSet();
            try
            {
                if (Grade == "A")
                {
                    Query = "select count(JobNo) as JobNo  from E_T_Shipment";
                }
                else if (Grade == "B")
                {
                    Query = "select count(JobNo) as JobNo  from E_T_Shipment";
                    //Query = "select count(JobNo) from E_T_Shipment where createdby='" + Uname + "'  ";
                }
                else if (Grade == "C")
                {
                    Query = "select count(JobNo) as JobNo  from E_T_Shipment where createdby='" + Uname + "'  ";
                }
                //string Query = "select JobNo, DischargeCountry,DischargePort,DestinationCountry,DestinationPort from  E_T_Shipment where createdby='" + Uname + "'  ";
                ds = objCommon.GetDataSet(Query.ToString());
            }
            catch (Exception ex)
            {
                string Msg = ex.Message.ToString();
            }
            return ds;
        }

        public DataSet SelectExpInvoice(string Uid, string Uname, string Grade)
        {
            string Query = string.Empty;
            DataSet ds = new DataSet();
            try
            {
                if (Grade == "A")
                {
                    Query = "select count(JobNo) as JobNo  from E_T_Invoice";
                }
                else if (Grade == "B")
                {
                    Query = "select count(JobNo) as JobNo  from E_T_Invoice";
                    //Query = "select count(JobNo) from E_T_Invoice where createdby='" + Uname + "'  ";
                }
                else if (Grade == "C")
                {
                    Query = "select count(JobNo) as JobNo  from E_T_Invoice where createdby='" + Uname + "'  ";
                }
                //string Query = "select JobNo from E_T_Invoice where createdby='" + Uname + "'  ";
                ds = objCommon.GetDataSet(Query.ToString());
            }
            catch (Exception ex)
            {
                string Msg = ex.Message.ToString();
            }
            return ds;
        }

        public DataSet SelectExpProduct(string Uid, string Uname, string Grade)
        {
            string Query = string.Empty;
            DataSet ds = new DataSet();
            try
            {
                if (Grade == "A")
                {
                    Query = "select count(JobNo) as JobNo  from E_T_Product";
                }
                else if (Grade == "B")
                {
                    Query = "select count(JobNo) as JobNo  from E_T_Product";
                    //Query = "select count(JobNo) from E_T_Product where createdby='" + Uname + "'  ";
                }
                else if (Grade == "C")
                {
                    Query = "select count(JobNo) as JobNo  from E_T_Product where createdby='" + Uname + "'  ";
                }
                //string Query = "select JobNo from E_T_Product where createdby='" + Uname + "'  ";
                ds = objCommon.GetDataSet(Query.ToString());
            }
            catch (Exception ex)
            {
                string Msg = ex.Message.ToString();
            }
            return ds;
        }

        public DataSet SelectSBFile(string Uid, string Uname, string Grade)
        {
            string Query = string.Empty;
            DataSet ds = new DataSet();
            try
            {
                if (Grade == "A")
                {
                    Query = "select count(JobNo) as JobNo  from E_T_Product";
                }
                else if (Grade == "B")
                {
                    Query = "select count(JobNo) as JobNo  from E_T_Product";
                    //Query = "select count(JobNo) from E_T_Product where createdby='" + Uname + "'  ";
                }
                else if (Grade == "C")
                {
                    Query = "select count(JobNo) as JobNo  from E_T_Product where createdby='" + Uname + "'  ";
                }
                //string Query = "select JobNo from  E_T_Product where createdby='" + Uname + "'  ";
                ds = objCommon.GetDataSet(Query.ToString());
            }
            catch (Exception ex)
            {
                string Msg = ex.Message.ToString();
            }
            return ds;
        }

        public DataSet SelectOperation(string Uid, string Uname, string Grade)
        {
            DataSet ds = new DataSet();
            try
            {
                string Query = "select *  from T_FundRequest where Approved= 0 and Completed = 0 and Active=0";
                ds = objCommon.GetDataSet(Query.ToString());
            }
            catch (Exception ex)
            {
                string Msg = ex.Message.ToString();
            }
            return ds;
        }

        public DataSet SelectAccounts(string Uid, string Uname, string Grade)
        {
            DataSet ds = new DataSet();
            try
            {
                string Query = "select * from T_FundRequest where Approved = 1 and Completed = 0 and Active=0";
                ds = objCommon.GetDataSet(Query.ToString());
            }
            catch (Exception ex)
            {
                string Msg = ex.Message.ToString();
            }
            return ds;
        }

        public DataSet ViewImpJob(string Uid, string Uname, string Grade)
        {
            string Query = string.Empty;
            DataSet ds = new DataSet();
            try
            {
                if (Grade == "A")
                {
                    Query = "select JobNo,JobReceivedDate,Mode,TotalNoofInvoice,TotalInvoiceValue,CreatedBy from T_JobCreation order by JobNo,JobReceivedDate Desc ";
                }
                else if (Grade == "B")
                {
                    Query = "select JobNo,JobReceivedDate,Mode,TotalNoofInvoice,TotalInvoiceValue,CreatedBy from T_JobCreation order by JobNo,JobReceivedDate Desc ";
                    //Query = "select count(JobNo) from T_JobCreation where createdby='" + Uname + "'  ";
                }
                else if (Grade == "C")
                {
                    Query = "select JobNo,JobReceivedDate,Mode,TotalNoofInvoice,TotalInvoiceValue from T_JobCreation where createdby='" + Uname + "'  order by JobNo,JobReceivedDate Desc";
                }
                ds = objCommon.GetDataSet(Query.ToString());
            }
            catch (Exception ex)
            {
                string Msg = ex.Message.ToString();
            }
            return ds;
        }

        public DataSet ViewImpShipMent(string Uid, string Uname, string Grade)
        {
            string Query = string.Empty;
            DataSet ds = new DataSet();
            try
            {
                if (Grade == "A")
                {
                    Query = "select [JobNo],[JobDate],[ShipmentCountry],[ShipmentPort],[CountryOrigin]  from T_ShipmentDetails";
                }
                else if (Grade == "B")
                {
                    Query = "select [JobNo],[JobDate],[ShipmentCountry],[ShipmentPort],[CountryOrigin]  from T_ShipmentDetails";
                    //Query = "select count(JobNo) from T_ShipmentDetails where createdby='" + Uname + "'  ";
                }
                else if (Grade == "C")
                {
                    Query = "select [JobNo],[JobDate],[ShipmentCountry],[ShipmentPort],[CountryOrigin]  from T_ShipmentDetails where createdby='" + Uname + "'  ";
                }
                // string Query = "select JobNo, JobDate,  ShipmentCountry, ShipmentPort, CountryOrigin, PortOfOrigin from T_ShipmentDetails where createdby='" + Uname + "'  ";
                ds = objCommon.GetDataSet(Query.ToString());
            }
            catch (Exception ex)
            {
                string Msg = ex.Message.ToString();
            }
            return ds;
        }

        public DataSet ViewImpInvoice(string Uid, string Uname, string Grade)
        {
            string Query = string.Empty;
            DataSet ds = new DataSet();
            try
            {
                if (Grade == "A")
                {
                    Query = "select [JobNo],[InvoiceNo],[InvoiceDate],[InvoiceCurrency],[InvoiceExchangeRates],[InvoiceProductValues],[InvoiceProductINRValues] from T_InvoiceDetails";
                }
                else if (Grade == "B")
                {
                    Query = "select [JobNo],[InvoiceNo],[InvoiceDate],[InvoiceCurrency],[InvoiceExchangeRates],[InvoiceProductValues],[InvoiceProductINRValues] from T_InvoiceDetails";
                    //Query = "select count(JobNo) from T_InvoiceDetails where createdby='" + Uname + "'  ";
                }
                else if (Grade == "C")
                {
                    Query = "select [JobNo],[InvoiceNo],[InvoiceDate],[InvoiceCurrency],[InvoiceExchangeRates],[InvoiceProductValues],[InvoiceProductINRValues] from T_InvoiceDetails where createdby='" + Uname + "'  ";
                }
                //string Query = "select JobNo, ImporterName, FreightType, FreightTyCurrency from  T_InvoiceDetails where createdby='" + Uname + "'  ";
                ds = objCommon.GetDataSet(Query.ToString());
            }
            catch (Exception ex)
            {
                string Msg = ex.Message.ToString();
            }
            return ds;
        }

        public DataSet ViewImpProduct(string Uid, string Uname, string Grade)
        {
            string Query = string.Empty;
            DataSet ds = new DataSet();
            try
            {
                if (Grade == "A")
                {
                    Query = "select [JobNo],[InvoiceNo],[ProductDesc],[Qty],[Unit],[UnitPrice],[Amount] from T_Product";
                }
                else if (Grade == "B")
                {
                    Query = "select [JobNo],[InvoiceNo],[ProductDesc],[Qty],[Unit],[UnitPrice],[Amount] from T_Product";
                    //Query = "select count(JobNo) from T_Product where createdby='" + Uname + "'  ";
                }
                else if (Grade == "C")
                {
                    Query = "select [JobNo],[InvoiceNo],[ProductDesc],[Qty],[Unit],[UnitPrice],[Amount] from T_Product where createdby='" + Uname + "'";
                }
                //string Query = "select JobNo from  T_Product where createdby='" + Uname + "'  ";
                ds = objCommon.GetDataSet(Query.ToString());
            }
            catch (Exception ex)
            {
                string Msg = ex.Message.ToString();
            }
            return ds;
        }

        public DataSet ViewExpJob(string Uid, string Uname, string Grade)
        {
            string Query = string.Empty;
            DataSet ds = new DataSet();
            try
            {
                if (Grade == "A")
                {
                    Query = "select [JobNo],[JobDate],[TransportMode],[TotalNoofInvoice],[TotalInvoiceValue] from E_M_JobCreation";
                }
                else if (Grade == "B")
                {
                    Query = "select [JobNo],[JobDate],[TransportMode],[TotalNoofInvoice],[TotalInvoiceValue] from E_M_JobCreation";
                    //Query = "select count(JobNo) from E_M_JobCreation where createdby='" + Uname + "'  ";
                }
                else if (Grade == "C")
                {
                    Query = "select [JobNo],[JobDate],[TransportMode],[TotalNoofInvoice],[TotalInvoiceValue] from E_M_JobCreation where createdby='" + Uname + "'  ";
                }
                //string Query = "select JobNo,JobDate,TransportMode,TotalNoofInvoice, TotalInvoiceValue from  E_M_JobCreation where createdby='" + Uname + "'  ";
                ds = objCommon.GetDataSet(Query.ToString());
            }
            catch (Exception ex)
            {
                string Msg = ex.Message.ToString();
            }
            return ds;
        }

        public DataSet ViewExpShipMent(string Uid, string Uname, string Grade)
        {
            string Query = string.Empty;
            DataSet ds = new DataSet();
            try
            {
                if (Grade == "A")
                {
                    Query = "select [JobNo],[DischargeCountry],[TotalNoofPkgsUnit],[NoofContainers]  from E_T_Shipment";
                }
                else if (Grade == "B")
                {
                    Query = "select [JobNo],[DischargeCountry],[TotalNoofPkgsUnit],[NoofContainers]  from E_T_Shipment";
                    //Query = "select count(JobNo) from E_T_Shipment where createdby='" + Uname + "'  ";
                }
                else if (Grade == "C")
                {
                    Query = "[JobNo],[DischargeCountry],[TotalNoofPkgsUnit],[NoofContainers]  from E_T_Shipment where createdby='" + Uname + "'  ";
                }
                //string Query = "select JobNo, DischargeCountry,DischargePort,DestinationCountry,DestinationPort from  E_T_Shipment where createdby='" + Uname + "'  ";
                ds = objCommon.GetDataSet(Query.ToString());
            }
            catch (Exception ex)
            {
                string Msg = ex.Message.ToString();
            }
            return ds;
        }

        public DataSet ViewExpInvoice(string Uid, string Uname, string Grade)
        {
            string Query = string.Empty;
            DataSet ds = new DataSet();
            try
            {
                if (Grade == "A")
                {
                    Query = "select [JobNo],[InvoiceNo],[InvoiceDate],[Currency],[ProductValue],[InvoiceINRAmount]  from E_T_Invoice";
                }
                else if (Grade == "B")
                {
                    Query = "select [JobNo],[InvoiceNo],[InvoiceDate],[Currency],[ProductValue],[InvoiceINRAmount]  from E_T_Invoice";
                    //Query = "select count(JobNo) from E_T_Invoice where createdby='" + Uname + "'  ";
                }
                else if (Grade == "C")
                {
                    Query = "select [JobNo],[InvoiceNo],[InvoiceDate],[Currency],[ProductValue],[InvoiceINRAmount] where createdby='" + Uname + "'  ";
                }
                //string Query = "select JobNo from E_T_Invoice where createdby='" + Uname + "'  ";
                ds = objCommon.GetDataSet(Query.ToString());
            }
            catch (Exception ex)
            {
                string Msg = ex.Message.ToString();
            }
            return ds;
        }

        public DataSet ViewExpProduct(string Uid, string Uname, string Grade)
        {
            string Query = string.Empty;
            DataSet ds = new DataSet();
            try
            {
                if (Grade == "A")
                {
                    Query = "select [JobNo],[InvoiceNo],[ProductCode],[Description],[RITCCode]  from E_T_Product";
                }
                else if (Grade == "B")
                {
                    Query = "select [JobNo],[InvoiceNo],[ProductCode],[Description],[RITCCode]  from E_T_Product";
                    //Query = "select count(JobNo) from E_T_Product where createdby='" + Uname + "'  ";
                }
                else if (Grade == "C")
                {
                    Query = "select [JobNo],[InvoiceNo],[ProductCode],[Description],[RITCCode] where createdby='" + Uname + "'  ";
                }
                //string Query = "select JobNo from E_T_Product where createdby='" + Uname + "'  ";
                ds = objCommon.GetDataSet(Query.ToString());
            }
            catch (Exception ex)
            {
                string Msg = ex.Message.ToString();
            }
            return ds;
        }

    }
}
