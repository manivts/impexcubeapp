// -----------------------------------------------------------------------
// <copyright file="ShipmentDL.cs" company="">
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
    using System.Data.SqlClient;
    using System.Data;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class ShipmentDL
    {
        string con = (string)ConfigurationManager.ConnectionStrings["Constr"].ConnectionString;

        public int InsertShipmentDetails(string Jobno, string jobdate, string shipmentcountry, string shipmentport, string origin, string vessel, string voyage, string transit,
            string eta, string glddate, string line, string igmno, string igmdate, string mblno, string mbldate, string hblno, string hbldate, string gatewayno, string gatewaydate,
            string lineno, string reporting, double Tcontainer, double Fcontainer, string gross, string grossunit, string packages, string packageunit,
            string stc, string stcunit, string stc1, string stcunit1, string CFSName, string MarksNos, string NetWeight, string NetUint, string createdby, string createddate,
            string modifiedby, string modifieddate, string ShipmentCountryCode, string ShipmentPortCode, string CountryOriginCode, string AgentName, string FFName, string ShipmentPortUneceCode)
        {
            int result = new int();
            string query = "INSERT INTO T_ShipmentDetails(JobNo,JobDate,ShipmentCountry,ShipmentPort, CountryOrigin,VesselName,VoyageNo,TransitVessel,ETA,GLDInwardDate," +
                "ShippingLine,LocalIGMNo,LocalIGMDate,MasterBLNo,MasterBLDate,HouseBLNo,HouseBLDate,GatewayIGMNo,GatewayIGMDate,ShipLineNo,ReportingPort," +
                "Container20Feet,Container40Feet,GrossWeight,GrossWeightUnit,NoOfPackages,PackagesUnit,STC," +
                "STCUnit,STC1,STCUnit1,CFSName,MarksNos,NetWeight,NetUint,CreatedBy,CreatedDate,ModifiedBy,ModifiedDate,ShipmentCountryCode, ShipmentPortCode, CountryOriginCode,AgentName,FFName,ShipmentUneceCode) Values ('" + Jobno + "', '" + jobdate + "','" + shipmentcountry + "'," +
                "'" + shipmentport + "','" + origin + "','" + vessel + "','" + voyage + "','" + transit + "','" + eta + "','" + glddate + "','" + line + "','" + igmno + "'," +
                "'" + igmdate + "','" + mblno + "','" + mbldate + "','" + hblno + "','" + hbldate + "','" + gatewayno + "','" + gatewaydate + "'," +
                "'" + lineno + "','" + reporting + "','" + Tcontainer + "','" + Fcontainer + "','" + gross + "','" + grossunit + "','" + packages + "','" + packageunit + "'," +
                "'" + stc + "','" + stcunit + "','" + stc1 + "','" + stcunit1 + "','" + CFSName + "','" + MarksNos + "','" + NetWeight + "','" + NetUint + "','" + createdby + "', "+
                "'" + createddate + "','" + modifiedby + "','" + modifieddate + "','" + ShipmentCountryCode + "', '" + ShipmentPortCode + "', '" + CountryOriginCode + "','" + AgentName + "','" + FFName + "','" + ShipmentPortUneceCode + "')";

            SqlConnection sqlConn = new SqlConnection(con);
            sqlConn.Open();
            SqlCommand cmd = new SqlCommand(query, sqlConn);
            result = cmd.ExecuteNonQuery();
            sqlConn.Close();
            return result;
           
        }

        public int InsertShipmentContainerInfo(string ShipTransID, string Jobno, string jobdate,string container, string containertype, string containerno, string sealno, string loadtype,
            string createdby, string createddate, string modifiedby, string modifieddate)
        {
            int result = new int();
            string query = " INSERT INTO T_ShipmentContainerInfo(ShipTransID,JobNo,JobDate,container,ContainerType,ContainerNo,SealNo,LoadType,CreatedBy,CreatedDate,ModifiedBy,ModifiedDate)" +
                "Values ('" + ShipTransID + "','" + Jobno + "','" + jobdate + "','" +container+ "','" + containertype + "','" + containerno + "','" + sealno + "','" + loadtype + "','" + createdby + "','" + createddate + "'," +
                "'" + modifiedby + "','" + modifieddate + "')";
            
            SqlConnection sqlConn = new SqlConnection(con);
            sqlConn.Open();
            SqlCommand cmd = new SqlCommand(query, sqlConn);
            SqlDataAdapter da = new SqlDataAdapter();

            cmd.CommandText = query;
            cmd.Connection = sqlConn;
            da.SelectCommand = cmd;
            result = cmd.ExecuteNonQuery();
            sqlConn.Close();
            return result;
           
        }


        public int containerdts(string containertype, int shipmentid)
        {
            int result = new int();
            string str = "Select Count(*) from T_ShipmentContainerInfo where ShipTransID='" + shipmentid + "' And Container='" + containertype + "'";
            SqlConnection sqlConn = new SqlConnection(con);
            sqlConn.Open();
            SqlCommand cmd = new SqlCommand(str, sqlConn);
            SqlDataAdapter da = new SqlDataAdapter();

            cmd.CommandText = str;
            cmd.Connection = sqlConn;
            da.SelectCommand = cmd;
            result = Convert.ToInt32(cmd.ExecuteScalar());
            sqlConn.Close();
            return result;
        }
        private string frmdatesplit(string frmdate)
        {
            string[] frmdate1 = frmdate.Split('/');
            string frmdate2 = frmdate1[1] + '/' + frmdate1[0] + '/' + frmdate1[2];
            return frmdate2;
        }

        public DataSet GetJobNo()
        {            
            DataSet ds = new DataSet();
            try
            {
                string Query = "Select JobNo from T_JobCreation order By JobNo DESC ";

                SqlConnection sqlConn = new SqlConnection(con);
                sqlConn.Open();
                SqlDataAdapter da = new SqlDataAdapter(Query, sqlConn);

                da.Fill(ds, "jobno");
                sqlConn.Close();
            }
            catch (Exception ex)
            {
                string Msg = ex.Message.ToString();
            }
            return ds;
        }

        public DataSet GetCountry()
        {
            DataSet ds = new DataSet();
            try
            {
                string Query = "Select CountryName,CountryCode from M_Country order by CountryName asc";

                SqlConnection sqlConn = new SqlConnection(con);
                sqlConn.Open();
                SqlDataAdapter da = new SqlDataAdapter(Query, sqlConn);

                da.Fill(ds, "country");
                sqlConn.Close();
            }
            catch (Exception ex)
            {
                string Msg = ex.Message.ToString();
            }
            return ds;
        }
        public DataSet GetState()
        {
            DataSet ds = new DataSet();
            try
            {
                string Query = "SELECT StateCode, StateName FROM  M_State ";

                SqlConnection sqlConn = new SqlConnection(con);
                sqlConn.Open();
                SqlDataAdapter da = new SqlDataAdapter(Query, sqlConn);

                da.Fill(ds, "State");
                sqlConn.Close();
            }
            catch (Exception ex)
            {
                string Msg = ex.Message.ToString();
            }
            return ds;
        }
        public DataSet GetPort()
        {
            DataSet ds = new DataSet();
            try
            {
                string Query = "Select PortName from M_Port";

                SqlConnection sqlConn = new SqlConnection(con);
                sqlConn.Open();
                SqlDataAdapter da = new SqlDataAdapter(Query, sqlConn);

                da.Fill(ds, "port");
                sqlConn.Close();
            }
            catch (Exception ex)
            {
                string Msg = ex.Message.ToString();
            }
            return ds;
        }

        public DataSet GetPort(string Country)
        {
            DataSet ds = new DataSet();
            try
            {
                string Query = "";
                Query = "Select Distinct PortName,CountryCode,PortCode,(PortName+'~'+PortCode+'~'+UneceCode) as port from M_Port where CountryCode='" + Country + "' order by PortName asc";
                SqlConnection sqlConn = new SqlConnection(con);
                sqlConn.Open();
                SqlDataAdapter da = new SqlDataAdapter(Query, sqlConn);
                da.Fill(ds, "port");
                sqlConn.Close();
            }
            catch (Exception ex)
            {
                string Msg = ex.Message.ToString();
            }
            return ds;
        }
        public DataSet GetPort(string Country,string portcode)
        {
            DataSet ds = new DataSet();
            try
            {
                string Query = "";
                Query = "Select Distinct UneceCode from M_Port where CountryCode='" + Country + "' And PortCode='" + portcode + "'";
                SqlConnection sqlConn = new SqlConnection(con);
                sqlConn.Open();
                SqlDataAdapter da = new SqlDataAdapter(Query, sqlConn);
                da.Fill(ds, "port");
                sqlConn.Close();
            }
            catch (Exception ex)
            {
                string Msg = ex.Message.ToString();
            }
            return ds;
        }
        public string  GetCountryCode(string Country)
        {
            DataSet ds = new DataSet();
            string CountryCode = "";
            try
            {
                string Query = "";
               Query = "Select CountryName,CountryCode from M_Country where CountryName='" + Country + "'";

                SqlConnection sqlConn = new SqlConnection(con);
                sqlConn.Open();
                SqlDataAdapter da = new SqlDataAdapter(Query, sqlConn);

                da.Fill(ds, "Country");
                sqlConn.Close();
                if (ds.Tables["Country"].Rows.Count != 0)
                {
                    DataRowView row = ds.Tables["Country"].DefaultView[0];
                    CountryCode = row["CountryCode"].ToString();
                }
            }
            catch (Exception ex)
            {
                string Msg = ex.Message.ToString();
            }
            return CountryCode;
        }
        public DataSet SelectJobNo(string JobNo)
        {
            DataSet ds = new DataSet();
            try
            {
                string Query = "Select TransId,JobNo from T_ShipmentDetails where JobNo='" + JobNo + "' ";

                SqlConnection sqlConn = new SqlConnection(con);
                sqlConn.Open();
                SqlDataAdapter da = new SqlDataAdapter(Query, sqlConn);

                da.Fill(ds, "jobno");
                sqlConn.Close();
            }
            catch (Exception ex)
            {
                string Msg = ex.Message.ToString();
            }
            return ds;
        }

        public DataSet GetShipmentDetails(string TransId)
        {
            DataSet ds = new DataSet();
            try
            {
                string Query = "Select  TransId, JobNo, JobDate, ShipmentCountry, ShipmentPort, CountryOrigin, VesselName, VoyageNo, TransitVessel, ETA, GLDInwardDate, ShippingLine, LocalIGMNo, "+
                      " LocalIGMDate, MasterBLNo, MasterBLDate, HouseBLNo, HouseBLDate, GatewayIGMNo, GatewayIGMDate, ShipLineNo, ReportingPort, Container20Feet, "+
                      "  Container40Feet,  GrossWeight, GrossWeightUnit, NoOfPackages, PackagesUnit, STC, STCUnit, STC1, STCUnit1, "+
                      " CFSName, MarksNos,AgentName,NetWeight,NetUint,FFName,ShipmentCountryCode,NetWeight,NetUint,FFName,ShipmentPortCode,ShipmentCountryCode,CountryOriginCode,CFSName,FFName,ShipmentUneceCode  FROM  T_ShipmentDetails " +
                    " Where TransId = '" + TransId + "' ";

                SqlConnection sqlConn = new SqlConnection(con);
                sqlConn.Open();
                SqlDataAdapter da = new SqlDataAdapter(Query, sqlConn);

                da.Fill(ds, "jobno");
                sqlConn.Close();
            }
            catch (Exception ex)
            {
                string Msg = ex.Message.ToString();
            }
            return ds;
        }
        public DataSet GetShipmentDetailsGrid(string JobNo)
        {
            DataSet ds = new DataSet();
            try
            {
                string Query = "Select  TransId, JobNo, JobDate, ShipmentCountry, ShipmentPort, CountryOrigin, VesselName, VoyageNo, TransitVessel, ETA, GLDInwardDate, ShippingLine, LocalIGMNo, " +
                      " LocalIGMDate, MasterBLNo, MasterBLDate, HouseBLNo, HouseBLDate, GatewayIGMNo, GatewayIGMDate, ShipLineNo, ReportingPort, Container20Feet, " +
                      "  Container40Feet,  GrossWeight, GrossWeightUnit, NoOfPackages, PackagesUnit, STC, STCUnit, STC1, STCUnit1, " +
                      " CFSName, MarksNos,NetWeight,NetUint,FFName  FROM  T_ShipmentDetails " +
                    " Where JobNo = '" + JobNo + "' ";

                SqlConnection sqlConn = new SqlConnection(con);
                sqlConn.Open();
                SqlDataAdapter da = new SqlDataAdapter(Query, sqlConn);

                da.Fill(ds, "jobno");
                sqlConn.Close();
            }
            catch (Exception ex)
            {
                string Msg = ex.Message.ToString();
            }
            return ds;
        }

        public DataSet GetJobShipmentContainerInfo(string jobno,string shipmentID)
        {
            DataSet ds = new DataSet();
            try
            {
                string Query = "SELECT TransId,JobNo,JobDate,ContainerType,ContainerNo,SealNo,LoadType FROM T_ShipmentContainerInfo Where JobNo = '" + jobno + "' and ShipTransID='" + shipmentID + "' ";

                SqlConnection sqlConn = new SqlConnection(con);
                sqlConn.Open();
                SqlDataAdapter da = new SqlDataAdapter(Query, sqlConn);

                da.Fill(ds, "jobno");
                sqlConn.Close();
            }
            catch (Exception ex)
            {
                string Msg = ex.Message.ToString();
            }
            return ds;
        }

        public DataSet GetJobSummary(string jobno)
        {
            DataSet ds = new DataSet();
            try
            {
                string Query = "SELECT JobNo,JobReceivedDate,Mode,Custom,BEType,DocFillingStatus,Filling,BENo,BEDate  FROM T_JobCreation Where JobNo = '" + jobno + "' ";

                SqlConnection sqlConn = new SqlConnection(con);
                sqlConn.Open();
                SqlDataAdapter da = new SqlDataAdapter(Query, sqlConn);

                da.Fill(ds, "jobno");
                sqlConn.Close();
            }
            catch (Exception ex)
            {
                string Msg = ex.Message.ToString();
            }
            return ds;
        }

        public int UpdateShipmentDetails(string ShipmentID,string Jobno, string jobdate, string shipmentcountry, string shipmentport, string origin, string vessel, string voyage, string transit,
            string eta, string glddate, string line, string igmno, string igmdate, string mblno, string mbldate, string hblno, string hbldate, string gatewayno, string gatewaydate,
            string lineno, string reporting, double Tcontainer,  double Fcontainer,  string gross, string grossunit, string packages, string packageunit,
            string stc, string stcunit, string stc1, string stcunit1, string CFSName, string MarksNos, string NetWeight, string NetUint, string modifiedby, string modifieddate,
            string ShipmentCountryCode, string ShipmentPortCode, string CountryOriginCode, string AgentName, string FFName, string ShipmentPortUneceCode)
        {
            int result = new int();
            string query = "Update T_ShipmentDetails set JobNo = '" + Jobno + "',JobDate = '" + jobdate + "',ShipmentCountry='" + shipmentcountry + "',"+
                "ShipmentPort = '" + shipmentport + "', CountryOrigin= '" + origin + "',VesselName = '" + vessel + "',VoyageNo = '" + voyage + "',TransitVessel = '" + transit + "',"+
                "ETA = '" + eta + "',GLDInwardDate =  '" + glddate + "', ShippingLine= '" + line + "',LocalIGMNo = '" + igmno + "',LocalIGMDate = '" + igmdate + "',"+
                "MasterBLNo = '" + mblno + "',MasterBLDate = '" + mbldate + "',HouseBLNo = '" + hblno + "',HouseBLDate = '" + hbldate + "',"+
                "GatewayIGMNo='" + gatewayno + "',GatewayIGMDate= '" + gatewaydate + "',ShipLineNo = '" + lineno + "',ReportingPort = '" + reporting + "'," +
                "Container20Feet = " + Tcontainer + ",Container40Feet=" + Fcontainer + ","+
                "GrossWeight = '" + gross + "',GrossWeightUnit='" + grossunit + "',NoOfPackages='" + packages + "',PackagesUnit='" + packageunit + "',STC = '" + stc + "'," +
                "STCUnit='" + stcunit + "',STC1='" + stc1 + "',STCUnit1='" + stcunit1 + "',CFSName='" + CFSName + "',MarksNos='" + MarksNos + "',ModifiedBy = '" + modifiedby + "',ModifiedDate = '" + modifieddate + "', "+
                " NetWeight='" + NetWeight + "',NetUint='" + NetUint + "',ShipmentCountryCode='" + ShipmentCountryCode + "', ShipmentPortCode='" + ShipmentPortCode + "',"+
                " CountryOriginCode='" + CountryOriginCode + "',AgentName='" + AgentName + "',FFName='" + FFName + "',ShipmentUneceCode='" + ShipmentPortUneceCode + "'" +
                "Where TransId = '" + ShipmentID + "'";

            SqlConnection sqlConn = new SqlConnection(con);
            sqlConn.Open();
            SqlCommand cmd = new SqlCommand(query, sqlConn);
            SqlDataAdapter da = new SqlDataAdapter();

            cmd.CommandText = query;
            cmd.Connection = sqlConn;
            da.SelectCommand = cmd;
            result = cmd.ExecuteNonQuery();
            sqlConn.Close();
            return result;

        }

        public int UpdateShipmentContainerInfo( int id, string Jobno, string jobdate,string container, string containertype, string containerno, string sealno, string loadtype,
           string modifiedby, string modifieddate)
        {
            int result = new int();
            string query = " Update T_ShipmentContainerInfo set JobNo = '" + Jobno + "',JobDate = '" + jobdate + "',container='"+ container +"',ContainerType = '" + containertype + "'," +
                " ContainerNo = '" + containerno + "',SealNo = '" + sealno + "',LoadType = '" + loadtype + "',ModifiedBy='" + modifiedby + "',ModifiedDate = '" + modifieddate + "' " +
                " Where JobNo = '" + Jobno + "' and TransId = '" + id + "' ";                

            SqlConnection sqlConn = new SqlConnection(con);
            sqlConn.Open();
            SqlCommand cmd = new SqlCommand(query, sqlConn);
            SqlDataAdapter da = new SqlDataAdapter();

            cmd.CommandText = query;
            cmd.Connection = sqlConn;
            da.SelectCommand = cmd;
            result = cmd.ExecuteNonQuery();
            sqlConn.Close();
            return result;

        }
        public DataSet GetVesselName()
        {
            DataSet ds = new DataSet();
            try
            {
                string Query = "Select VesselCode,VesselName from M_VesselMaster";

                SqlConnection sqlConn = new SqlConnection(con);
                sqlConn.Open();
                SqlDataAdapter da = new SqlDataAdapter(Query, sqlConn);

                da.Fill(ds, "Vessel");
                sqlConn.Close();
            }
            catch (Exception ex)
            {
                string Msg = ex.Message.ToString();
            }
            return ds;
        }
        public DataSet GetShippingName()
        {
            DataSet ds = new DataSet();
            try
            {
                string Query = "Select distinct [AccountCode],[AccountName] from [M_AccountMaster] where AccountType='ShippingLine' order by AccountName asc";

                SqlConnection sqlConn = new SqlConnection(con);
                sqlConn.Open();
                SqlDataAdapter da = new SqlDataAdapter(Query, sqlConn);

                da.Fill(ds, "Shipper");
                sqlConn.Close();
            }
            catch (Exception ex)
            {
                string Msg = ex.Message.ToString();
            }
            return ds;
        }
        public DataSet GetAirName()
        {
            DataSet ds = new DataSet();
            try
            {
                string Query = "Select distinct AccountName from M_AccountMaster where AccountType='Airline' order by AccountName asc ";

                SqlConnection sqlConn = new SqlConnection(con);
                sqlConn.Open();
                SqlDataAdapter da = new SqlDataAdapter(Query, sqlConn);

                da.Fill(ds, "Air");
                sqlConn.Close();
            }
            catch (Exception ex)
            {
                string Msg = ex.Message.ToString();
            }
            return ds;
        }
        public DataSet GetAgentName()
        {
            DataSet ds = new DataSet();
            try
            {
                string Query = "Select distinct  AccountName from M_AccountMaster order by AccountName asc";

                SqlConnection sqlConn = new SqlConnection(con);
                sqlConn.Open();
                SqlDataAdapter da = new SqlDataAdapter(Query, sqlConn);

                da.Fill(ds, "Agent");
                sqlConn.Close();
            }
            catch (Exception ex)
            {
                string Msg = ex.Message.ToString();
            }
            return ds;
        }
        public DataSet GetCFSName()
        {
            DataSet ds = new DataSet();
            try
            {
                string Query = "Select AccountCode, AccountName from M_AccountMaster Where AccountType='CFS' order by AccountName asc";

                SqlConnection sqlConn = new SqlConnection(con);
                sqlConn.Open();
                SqlDataAdapter da = new SqlDataAdapter(Query, sqlConn);

                da.Fill(ds, "CFS");
                sqlConn.Close();
            }
            catch (Exception ex)
            {
                string Msg = ex.Message.ToString();
            }
            return ds;
        }
        public DataSet GetFFName(string AccountType)
        {
            DataSet ds = new DataSet();
            try
            {
                string Query = "Select AccountName from M_AccountMaster where AccountType='" + AccountType + "' order by AccountName asc";

                SqlConnection sqlConn = new SqlConnection(con);
                sqlConn.Open();
                SqlDataAdapter da = new SqlDataAdapter(Query, sqlConn);
                da.Fill(ds, "Dataset");
                sqlConn.Close();
            }
            catch (Exception ex)
            {
                string Msg = ex.Message.ToString();
            }
            return ds;
        }

        public int DeleteShipment(string TransID)
        {
            SqlConnection sqlConn = new SqlConnection(con);
            int result = new int();
            try
            {
                string Query = "Delete  from  T_ShipmentDetails where [TransId]='" + TransID + "'";
                sqlConn.Open();
                SqlCommand cmd = new SqlCommand(Query, sqlConn);
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

        public int DeleteContainerShipment(string TransID)
        {
            SqlConnection sqlConn = new SqlConnection(con);
            int result = new int();
            try
            {
                string Query = "Delete  from  T_ShipmentContainerInfo where [TransId]='" + TransID + "'";
                sqlConn.Open();
                SqlCommand cmd = new SqlCommand(Query, sqlConn);
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

        public string GetShipmentTransID(string JobNo)
        {
            DataSet ds = new DataSet();
            string TransID = "";
            try
            {
                string Query = "";
                Query = "Select [TransId],[JobNo] from T_ShipmentDetails where JobNo='" + JobNo + "'";

                SqlConnection sqlConn = new SqlConnection(con);
                sqlConn.Open();
                SqlDataAdapter da = new SqlDataAdapter(Query, sqlConn);

                da.Fill(ds, "TransID");
                sqlConn.Close();
                if (ds.Tables["TransID"].Rows.Count != 0)
                {
                    DataRowView row = ds.Tables["TransID"].DefaultView[0];
                    TransID = row["TransId"].ToString();
                }
            }
            catch (Exception ex)
            {
                string Msg = ex.Message.ToString();
            }
            return TransID;
        }

        public DataSet GetContainerType()
        {
            DataSet ds = new DataSet();
            try
            {
                string Query = "";

                Query = "Select Sno,Container,Containertype from M_ContainerType ";

                SqlConnection sqlConn = new SqlConnection(con);
                sqlConn.Open();
                SqlDataAdapter da = new SqlDataAdapter(Query, sqlConn);

                da.Fill(ds, "Container");
                sqlConn.Close();
            }
            catch (Exception ex)
            {
                string Msg = ex.Message.ToString();
            }
            return ds;
        }




        public int InsertCommercialTax(string Jobno,  string Ctax_StateCode, string Ctax_StateName, string Ctax_Type, string Ctax_RegNo,
            string createdby, string createddate, string modifiedby, string modifieddate)
        {
            int result = new int();
            string query = " INSERT INTO T_CommercialTax(JobNo, Ctax_StateCode, Ctax_StateName, Ctax_Type, Ctax_RegNo, CreatedBy, CreatedDate, ModifiedBy, ModifiedDate)" +
                "Values ('" + Jobno + "','" + Ctax_StateCode + "','" + Ctax_StateName + "','" + Ctax_Type + "','" + Ctax_RegNo + "','" + createdby + "','" + createddate + "'," +
                "'" + modifiedby + "','" + modifieddate + "')";

            SqlConnection sqlConn = new SqlConnection(con);
            sqlConn.Open();
            SqlCommand cmd = new SqlCommand(query, sqlConn);
            SqlDataAdapter da = new SqlDataAdapter();
            cmd.CommandText = query;
            cmd.Connection = sqlConn;
            da.SelectCommand = cmd;
            result = cmd.ExecuteNonQuery();
            sqlConn.Close();
            return result;
        }



        public int UpdateCommercialTax(string Jobno, string Ctax_StateCode, string Ctax_StateName, string Ctax_Type, string Ctax_RegNo,
           string modifiedby, string modifieddate)
        {
            string query = string.Empty;
            string TestJobNo = "select JobNo from T_CommercialTax Where JobNo  = '" + Jobno + "'";
            DataSet ds = new DataSet();
            SqlConnection sqlConn1 = new SqlConnection(con);
            sqlConn1.Open();
            SqlDataAdapter da1 = new SqlDataAdapter(TestJobNo, sqlConn1);

            da1.Fill(ds, "TransID");
            sqlConn1.Close();
            int TestJno = ds.Tables["TransID"].Rows.Count;

            int result;
            if (TestJno==0)
            {
                 query = " INSERT INTO T_CommercialTax(JobNo, Ctax_StateCode, Ctax_StateName, Ctax_Type, Ctax_RegNo, CreatedBy, CreatedDate, ModifiedBy, ModifiedDate)" +
                "Values ('" + Jobno + "','" + Ctax_StateCode + "','" + Ctax_StateName + "','" + Ctax_Type + "','" + Ctax_RegNo + "','" + modifiedby + "','" + modifieddate + "'," +
                "'" + modifiedby + "','" + modifieddate + "')";
            }
            else
            {
                query = " Update T_CommercialTax Set JobNo='" + Jobno + "'  , Ctax_StateCode='" + Ctax_StateCode + "', Ctax_StateName ='" + Ctax_StateName + "',Ctax_Type='" + Ctax_Type + "',Ctax_RegNo= '" + Ctax_RegNo + "', ModifiedBy='" + modifiedby + "', modifieddate='" + modifieddate + "' where JobNo='" + Jobno + "'";
            }
           
             
            SqlConnection sqlConn = new SqlConnection(con);
            sqlConn.Open();
            SqlCommand cmd = new SqlCommand(query, sqlConn);
            SqlDataAdapter da = new SqlDataAdapter();
            cmd.CommandText = query;
            cmd.Connection = sqlConn;
            da.SelectCommand = cmd;
            result = cmd.ExecuteNonQuery();
            sqlConn.Close();
            return result;
        }
    }
}
