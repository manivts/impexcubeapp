using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using System.Web.Services;
using System.Web.Script.Services;

namespace ImpexCube.JSONServices
{
    public class ImpexCubeServices
    {
        private static DataSet GetData(string cmd)
        {
            string strcon = (string)ConfigurationManager.ConnectionStrings["Constr"].ConnectionString;
            SqlConnection con = new SqlConnection(strcon);
            con.Open();
            SqlDataAdapter sd = new SqlDataAdapter(cmd, con);
            DataSet ds = new DataSet();
            sd.Fill(ds, "data");
            con.Close();
            return ds;

        }

        //Read the Exchange Rate
        public class Currency
        {
            public string IMPCurrencyRate { get; set; }

        }
        [WebMethod]
        public static Currency[] BindDatatoDropdownemail(string CurrencyShortName)
        {
            List<Currency> empobj = new List<Currency>();
            string Query = "Select IMPCurrencyRate from M_Currency where CurrencyShortName='" + CurrencyShortName + "'";
            DataSet ds = GetData(Query);
            DataTable dt = ds.Tables[0];
            foreach (DataRow item in ds.Tables[0].Rows)
            {
                Currency pro = new Currency();
                pro.IMPCurrencyRate = item["IMPCurrencyRate"].ToString();
                empobj.Add(pro);
            }
            return empobj.ToArray();
        }

        //JobDetails Read
        public class Jobdetails
        {
            public string JobNo { get; set; }
            public string JobReceivedDate { get; set; }
            public string Mode { get; set; }
            public string Custom { get; set; }
            public string BEType { get; set; }
            public string DocFillingStatus { get; set; }
            public string BENo { get; set; }
            public string BEDate { get; set; }
            public string Portofshipment { get; set; }
            public string CountryofShipment { get; set; }
            public string CountryofOrgin { get; set; }
            public string Currency { get; set; }
        }
        [WebMethod]
        public static Jobdetails[] BindJobDetails(string JobNo)
        {
            List<Jobdetails> ReadJob = new List<Jobdetails>();
            string Query = "SELECT DISTINCT JobNo, JobReceivedDate, Mode, Custom, BEType, DocFillingStatus,  BENo, BEDate,Portofshipment, CountryofShipment, CountryofOrgin,Currency FROM View_JobImporterShipment where JobNo='" + JobNo + "'";
            DataSet ds = GetData(Query);
            DataTable dt = ds.Tables[0];
            foreach (DataRow item in ds.Tables[0].Rows)
            {
                Jobdetails jdts = new Jobdetails();
                //DataRowView row = ds.Tables[0].DefaultView[0];
                jdts.JobNo = item["JobNo"].ToString();
                jdts.JobReceivedDate = item["JobReceivedDate"].ToString();
                jdts.Mode = item["Mode"].ToString();
                jdts.Custom = item["Custom"].ToString();
                jdts.BEType = item["BEType"].ToString();
                jdts.DocFillingStatus = item["DocFillingStatus"].ToString();
                jdts.BENo = item["BENo"].ToString();
                jdts.BEDate = item["BEDate"].ToString();
                jdts.Portofshipment = item["Portofshipment"].ToString();
                jdts.CountryofShipment = item["CountryofShipment"].ToString();
                jdts.CountryofOrgin = item["CountryofOrgin"].ToString();
                jdts.Currency = item["Currency"].ToString();
                ReadJob.Add(jdts);
            }
            return ReadJob.ToArray();
        }

        //JobNo drop Down
        public class JobNo
        {
            public string jobno1 { get; set; }
        }
        [WebMethod]
        public static JobNo[] GetJobNo()
        {
            //public static List<JobNo> GetJobNo()
            //{
            List<JobNo> Jobnum = new List<JobNo>();
            string Query = "Select JobNo from T_JobCreation";
            DataSet ds = GetData(Query);
            DataTable dt = ds.Tables[0];
            int i = 0;
            foreach (DataRow item in ds.Tables[0].Rows)
            {
                JobNo jdts = new JobNo();
                jdts.jobno1 = item["JobNo"].ToString();
                //jdts.CountryId = item["JobNo"].ToString();
                //jdts.CountryName = item["JobNo"].ToString();
                //details.Add(country);
                Jobnum.Add(jdts);
                i++;
            }
            return Jobnum.ToArray();
        }

        //Importer Details Read
        public class Importerdetails
        {
            public string JobNo { get; set; }
            public string Importer { get; set; }
            public string ImporterType { get; set; }
            public string IECodeNo { get; set; }
            public string BranchSno { get; set; }
            public string Address { get; set; }
            public string City { get; set; }
            public string State { get; set; }
            public string ZipCode { get; set; }
            public string ImporterRefNo { get; set; }
            public string Portofshipment { get; set; }
            public string CountryofOrgin { get; set; }
            public string CountryofShipment { get; set; }

            public string BEHeading { get; set; }
            public string CommericalState { get; set; }
            public string CommericalTaxType { get; set; }
            public string CommericalRegnNo { get; set; }
            public string Consignor { get; set; }
            public string ConsignorAddress { get; set; }
            public string ConsignorCity { get; set; }

            public string ConsignorCountry { get; set; }
            public string SellerName { get; set; }
            public string HighIECode { get; set; }
            public string HighBranchSno { get; set; }
            public string HighAddress { get; set; }
            public string HighCity { get; set; }
            public string HighState { get; set; }
            public string HighZipCode { get; set; }
            public string SingleConsignor { get; set; }

            public string HighSeaSale { get; set; }
            public string ChkUnderSec46 { get; set; }
            public string underSec46 { get; set; }
            public string ChkFirstChk { get; set; }
            public string FirstChk { get; set; }
            public string ChkGreen { get; set; }
            public string GreenChannel { get; set; }
            public string ChkKachha { get; set; }
            public string Kachha { get; set; }
            public string ChkUnderSec48 { get; set; }
            public string UnderSec48 { get; set; }

        }
        [WebMethod]
        public static Importerdetails[] BindImporterDetails(string JobNo)
        {
            List<Importerdetails> ReadImp = new List<Importerdetails>();
            string Query = "SELECT DISTINCT JobNo, Importer, ImporterType, IECodeNo, BranchSno, Address, City, State, ZipCode, ImporterRefNo, Portofshipment, CountryofOrgin, CountryofShipment, BEHeading, CommericalState, CommericalTaxType, CommericalRegnNo, Consignor, ConsignorAddress, ConsignorCity, ConsignorCountry, SellerName, HighIECode,HighBranchSno, HighAddress, HighCity, HighState, HighZipCode, SingleConsignor, HighSeaSale, ChkUnderSec46, underSec46, ChkFirstChk,FirstChk, ChkGreen, GreenChannel, ChkKachha, Kachha, ChkUnderSec48, UnderSec48   FROM T_Importer where JobNo='" + JobNo + "'";
            DataSet ds = GetData(Query);
            // DataTable dt = ds.Tables[0];
            //foreach (DataRow item in ds.Tables[0].Rows)
            //{
            if (ds.Tables["data"].Rows.Count != 0)
            {
                DataRowView item = ds.Tables[0].DefaultView[0];
                Importerdetails impdts = new Importerdetails();
                impdts.JobNo = item["JobNo"].ToString();
                impdts.Importer = item["Importer"].ToString();
                impdts.ImporterType = item["ImporterType"].ToString();
                impdts.IECodeNo = item["IECodeNo"].ToString();
                impdts.BranchSno = item["BranchSno"].ToString();
                impdts.Address = item["Address"].ToString();
                impdts.City = item["City"].ToString();
                impdts.State = item["State"].ToString();
                impdts.ZipCode = item["ZipCode"].ToString();
                impdts.ImporterRefNo = item["ImporterRefNo"].ToString();

                impdts.Portofshipment = item["Portofshipment"].ToString();
                impdts.CountryofOrgin = item["CountryofOrgin"].ToString();
                impdts.CountryofShipment = item["CountryofShipment"].ToString();

                impdts.BEHeading = item["BEHeading"].ToString();
                impdts.CommericalState = item["CommericalState"].ToString();
                impdts.CommericalTaxType = item["CommericalTaxType"].ToString();
                impdts.CommericalRegnNo = item["CommericalRegnNo"].ToString();
                impdts.Consignor = item["Consignor"].ToString();
                impdts.ConsignorAddress = item["ConsignorAddress"].ToString();
                impdts.ConsignorCity = item["ConsignorCity"].ToString();

                impdts.SellerName = item["SellerName"].ToString();
                impdts.HighIECode = item["HighIECode"].ToString();
                impdts.HighBranchSno = item["HighBranchSno"].ToString();
                impdts.HighAddress = item["HighAddress"].ToString();
                impdts.HighCity = item["HighCity"].ToString();
                impdts.HighState = item["HighState"].ToString();
                impdts.HighZipCode = item["HighZipCode"].ToString();
                impdts.SingleConsignor = item["SingleConsignor"].ToString();

                impdts.HighSeaSale = item["HighSeaSale"].ToString();
                impdts.ChkUnderSec46 = item["ChkUnderSec46"].ToString();
                impdts.underSec46 = item["underSec46"].ToString();
                impdts.ChkFirstChk = item["ChkFirstChk"].ToString();
                impdts.FirstChk = item["FirstChk"].ToString();
                impdts.ChkGreen = item["ChkGreen"].ToString();
                impdts.GreenChannel = item["GreenChannel"].ToString();
                impdts.ChkKachha = item["ChkKachha"].ToString();
                impdts.Kachha = item["Kachha"].ToString();
                impdts.ChkUnderSec48 = item["ChkUnderSec48"].ToString();
                impdts.UnderSec48 = item["UnderSec48"].ToString();
                ReadImp.Add(impdts);
                //}
            }
            return ReadImp.ToArray();
        }

        //Country Details Read
        public class CountryDetails
        {
            public string CountryId { get; set; }
            public string CountryName { get; set; }
        }
        [WebMethod]
        public static CountryDetails[] BindCountry()
        {
            List<CountryDetails> details = new List<CountryDetails>();
            string Query = "Select CountryName,CountryCode from M_Country Order By CountryName";
            DataSet ds = GetData(Query);
            DataTable dt = ds.Tables[0];
            {
                foreach (DataRow dtrow in dt.Rows)
                {
                    CountryDetails country = new CountryDetails();
                    //country.CountryId = Convert.ToInt32(dtrow["CountryCode"].ToString());
                    country.CountryId = dtrow["CountryCode"].ToString();
                    country.CountryName = dtrow["CountryName"].ToString();
                    details.Add(country);
                }
            }
            return details.ToArray();
        }
        public class PortDetails
        {
            public string PortId { get; set; }
            public string PortName { get; set; }
        }
        [WebMethod]
        public static PortDetails[] BindPort(string CountryCode)
        {
            List<PortDetails> details1 = new List<PortDetails>();
            string Query = "";
            Query = "Select PortCode,PortName from M_Port where CountryCode='" + CountryCode + "' Order By PortName";
            DataSet ds = GetData(Query);
            DataTable dt = ds.Tables[0];
            {
                foreach (DataRow dtrow in dt.Rows)
                {
                    PortDetails Port = new PortDetails();
                    Port.PortId = dtrow["PortCode"].ToString();
                    Port.PortName = dtrow["PortName"].ToString();
                    details1.Add(Port);
                }
            }

            return details1.ToArray();
        }

        public class AccountMaster
        {
            public string Importer { get; set; }
            public string IECodeNo { get; set; }
            public string BranchSno { get; set; }
            public string Address { get; set; }
            public string City { get; set; }
            public string State { get; set; }
            public string ZipCode { get; set; }
            public string ShortName { get; set; }

        }
        [WebMethod]
        public static AccountMaster[] BindAccountMaster(string SearchAccName)
        {
            List<AccountMaster> ReadImp = new List<AccountMaster>();
            string Query = "SELECT DISTINCT AccountCode, AccountName, ShortName, BranchId, BranchName, Address1, City, State, Pincode,IECode FROM  View_AccountMaster where  Search='" + SearchAccName + "'";
            DataSet ds = GetData(Query);
            if (ds.Tables["data"].Rows.Count != 0)
            {
                DataRowView item = ds.Tables[0].DefaultView[0];
                AccountMaster impdts = new AccountMaster();
                impdts.Importer = item["AccountName"].ToString();
                impdts.IECodeNo = item["IECode"].ToString();
                impdts.BranchSno = item["BranchId"].ToString();
                impdts.Address = item["Address1"].ToString();
                impdts.City = item["City"].ToString();
                impdts.State = item["State"].ToString();
                impdts.ZipCode = item["Pincode"].ToString();
                impdts.ShortName = item["ShortName"].ToString();
                ReadImp.Add(impdts);
            }
            return ReadImp.ToArray();
        }
    }
}