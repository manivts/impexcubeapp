// -----------------------------------------------------------------------
// <copyright file="Importer.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace VTS.ImpexCube.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Data;
    using System.Data.SqlClient;
    using System.Configuration;
    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class Importer
    {
        string con = (string)ConfigurationManager.ConnectionStrings["Constr"].ConnectionString;

        public DataSet jobno(string jobno)
        {

            DataSet ds = new DataSet();
            try
            {
                string Query = "select * from T_JobCreation where jobno='"+jobno+"'";

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

        public DataSet importer()
        {

            DataSet ds = new DataSet();
            try
            {
                string Query = "select * from M_Importer ";

                SqlConnection sqlConn = new SqlConnection(con);
                sqlConn.Open();
                SqlDataAdapter da = new SqlDataAdapter(Query, sqlConn);

                da.Fill(ds, "importer");
                sqlConn.Close();
            }
            catch (Exception ex)
            {
                string Msg = ex.Message.ToString();
            }
            return ds;
        }

       
        public DataSet branchsno(string importer)
        {

            DataSet ds = new DataSet();
            try
            {
                string Query = "select * from M_ImporterDetails id,M_Importer i where id.PartyName=i.PartyName and  id.PartyName='" + importer + "' ";

                SqlConnection sqlConn = new SqlConnection(con);
                sqlConn.Open();
                SqlDataAdapter da = new SqlDataAdapter(Query, sqlConn);

                da.Fill(ds, "branchsno");
                sqlConn.Close();
            }
            catch (Exception ex)
            {
                string Msg = ex.Message.ToString();
            }
            return ds;
        }

        public DataSet branchdetails(string importer,string branch)
        {

            DataSet ds = new DataSet();
            try
            {
                string Query = "select * from M_ImporterDetails id,M_Importer i where id.PartyName=i.PartyName and  id.PartyName='" + importer + "'  and BranchSno='" + branch + "' ";

                SqlConnection sqlConn = new SqlConnection(con);
                sqlConn.Open();
                SqlDataAdapter da = new SqlDataAdapter(Query, sqlConn);

                da.Fill(ds, "branchsno");
                sqlConn.Close();
            }
            catch (Exception ex)
            {
                string Msg = ex.Message.ToString();
            }
            return ds;
        }

        public DataSet checksave(string importer)
        {

            DataSet ds = new DataSet();
            try
            {
                string Query = "select * from M_Importer where PartyName='" + importer + "'";

                SqlConnection sqlConn = new SqlConnection(con);
                sqlConn.Open();
                SqlDataAdapter da = new SqlDataAdapter(Query, sqlConn);

                da.Fill(ds, "branchsno");
                sqlConn.Close();
            }
            catch (Exception ex)
            {
                string Msg = ex.Message.ToString();
            }
            return ds;
        }


        public int insert(string JobNo,string Importer, string IE_Code_No, string Branch_Sno, string Address, string City, string State, string Importer_Ref_No,string BE_Heading, string Consignor,string ConsignorShName,
        string Consignor_Address, string Consignor_City, string Consignor_Country, string Seller_Name,string ShortSellerName, string High_IE_Code, string High_Branch_Sno, string High_Address, 
        string High_City, string High_State, string High_ZipCode, string SingleConsignor, string HighSeaSale, string UnderSec46ck, string underSec46, string Kachhack, string Kachha, string UnderSec48ck,
        string UnderSec48, string FirstChkck, string FirstChk, string Greenck, string Green,string ImporterCode,string ImporterType,string ImpShortName,string HighShortName,string ZipCode)
        {
            StringBuilder Query = new StringBuilder();

            int result = new int();//ImporterCode,ImporterType
           Query.Append(" insert into T_Importer(JobNo,Importer,IECodeNo,BranchSno,Address,City,State,ImporterRefNo,BEHeading," );
           Query.Append(" Consignor,ConsignorShName,ConsignorAddress,ConsignorCity,ConsignorCountry,SellerName,ShortSellerName,HighIECode,HighBranchSno,HighAddress,HighCity,HighState,HighZipCode,SingleConsignor,");
           Query.Append("HighSeaSale,ChkUnderSec46,underSec46,ChkKachha, Kachha,ChkUnderSec48, UnderSec48,ChkFirstChk, FirstChk,ChkGreen, GreenChannel,ImporterCode,ImporterType,ImpShortName, HighShortName, ZipCode)");
           Query.Append("Values(@JobNo,@Importer,@IE_Code_No,@Branch_Sno,@Address,@City,@State,@Importer_Ref_No,@BE_Heading,");
           Query.Append("@Consignor,@ConsignorShName,@Consignor_Address,@Consignor_City,@Consignor_Country,@Seller_Name,@ShortSellerName,@High_IE_Code,@High_Branch_Sno,@High_Address,@High_City,@High_State,@High_ZipCode,@SingleConsignor,@HighSeaSale,");
           Query.Append("@UnderSec46ck,@underSec46,@Kachhack,@Kachha,@UnderSec48ck,@UnderSec48,@FirstChkck,@FirstChk,@Greenck,@Green,@ImporterCode,@ImporterType,@ImpShortName,@HighShortName,@ZipCode)");

           SqlConnection sqlConn = new SqlConnection(con);
           sqlConn.Open();
           SqlCommand cmd = new SqlCommand(Query.ToString(), sqlConn);

           cmd.Parameters.AddWithValue("@JobNo",JobNo);
           cmd.Parameters.AddWithValue("@Importer",Importer);
           cmd.Parameters.AddWithValue("@IE_Code_No",IE_Code_No);
           cmd.Parameters.AddWithValue("@Branch_Sno",Branch_Sno);
           cmd.Parameters.AddWithValue("@Address",Address);
           cmd.Parameters.AddWithValue("@City",City);
           cmd.Parameters.AddWithValue("@State",State);
           cmd.Parameters.AddWithValue("@Importer_Ref_No",Importer_Ref_No);
           cmd.Parameters.AddWithValue("@BE_Heading",BE_Heading);
           cmd.Parameters.AddWithValue("@Consignor",Consignor);
           cmd.Parameters.AddWithValue("@ConsignorShName", ConsignorShName);
           cmd.Parameters.AddWithValue("@Consignor_Address",Consignor_Address);
           cmd.Parameters.AddWithValue("@Consignor_City",Consignor_City);
           cmd.Parameters.AddWithValue("@Consignor_Country",Consignor_Country);
           cmd.Parameters.AddWithValue("@Seller_Name",Seller_Name);
           cmd.Parameters.AddWithValue("@ShortSellerName",ShortSellerName);
           cmd.Parameters.AddWithValue("@High_IE_Code",High_IE_Code);
           cmd.Parameters.AddWithValue("@High_Branch_Sno",High_Branch_Sno);
           cmd.Parameters.AddWithValue("@High_Address",High_Address);
           cmd.Parameters.AddWithValue("@High_City",High_City);
           cmd.Parameters.AddWithValue("@High_State",High_State);
           cmd.Parameters.AddWithValue("@High_ZipCode",High_ZipCode);
           cmd.Parameters.AddWithValue("@SingleConsignor",SingleConsignor);
           cmd.Parameters.AddWithValue("@HighSeaSale",HighSeaSale);
           cmd.Parameters.AddWithValue("@UnderSec46ck",UnderSec46ck);
           cmd.Parameters.AddWithValue("@underSec46",underSec46);
           cmd.Parameters.AddWithValue("@Kachhack",Kachhack);
           cmd.Parameters.AddWithValue("@Kachha",Kachha);
           cmd.Parameters.AddWithValue("@UnderSec48ck",UnderSec48ck);
           cmd.Parameters.AddWithValue("@UnderSec48",UnderSec48);
           cmd.Parameters.AddWithValue("@FirstChkck",FirstChkck);
           cmd.Parameters.AddWithValue("@FirstChk",FirstChk);
           cmd.Parameters.AddWithValue("@Greenck",Greenck);
           cmd.Parameters.AddWithValue("@Green",Green);
           cmd.Parameters.AddWithValue("@ImporterCode",ImporterCode);
           cmd.Parameters.AddWithValue("@ImporterType",ImporterType);
           cmd.Parameters.AddWithValue("@ImpShortName",ImpShortName);
           cmd.Parameters.AddWithValue("@HighShortName",HighShortName);
           cmd.Parameters.AddWithValue("@ZipCode", ZipCode);
        //SqlDataAdapter da = new SqlDataAdapter();
        ////cmd.CommandText =insertimporter ;
        //cmd.Connection = sqlConn;
        //da.SelectCommand = cmd;
        result = cmd.ExecuteNonQuery();
        sqlConn.Close();
        return result;
        }

        //public int insertmast(string Importer,string ImpCode, string IECodeNo, string ImporterRefNo)
        //{
        //     int result = new int();
        //     string insertimporter = "insert into M_Importer(PartyName,PartyCode,IeCodeNo,RefCode)values ('" + Importer + "','" + ImpCode + "','" + IECodeNo + "','" + ImporterRefNo + "')";
        //     SqlConnection sqlConn = new SqlConnection(con);
        //     sqlConn.Open();
        //     SqlCommand cmd = new SqlCommand(insertimporter, sqlConn);
        //     SqlDataAdapter da = new SqlDataAdapter();

        //     cmd.CommandText = insertimporter;
        //     cmd.Connection = sqlConn;
        //     da.SelectCommand = cmd;
        //     result = cmd.ExecuteNonQuery();
        //     sqlConn.Close();
        //     return result;
        //}
        //public int insertaddmast(string ImpCode,string ImpBranchNo, string Address, string City, string StateImp)
        //{
        //    int result = new int();
        //    string insertimporter = "insert into M_ImporterDetails(PartyCode,BranchSno,Address,City,State)values('"+ ImpCode +"','" + ImpBranchNo + "','" + Address + "','" + City + "','" + StateImp + "')";
        //    SqlConnection sqlConn = new SqlConnection(con);
        //    sqlConn.Open();
        //    SqlCommand cmd = new SqlCommand(insertimporter, sqlConn);
        //    SqlDataAdapter da = new SqlDataAdapter();

        //    cmd.CommandText = insertimporter;
        //    cmd.Connection = sqlConn;
        //    da.SelectCommand = cmd;
        //    result = cmd.ExecuteNonQuery();
        //    sqlConn.Close();
        //    return result;
        //}

        //public int updatemast(string Importer, string IECodeNo, string ImporterRefNo)
        //{
        //     int result = new int();
        //     string insertimporter = "update M_Importer set  PartyName='" + Importer + "',IeCodeNo='" + IECodeNo + "',RefCode='" + ImporterRefNo + "' where PartyName='" + Importer + "'";

        //     SqlConnection sqlConn = new SqlConnection(con);
        //     sqlConn.Open();
        //     SqlCommand cmd = new SqlCommand(insertimporter, sqlConn);

        //     result = cmd.ExecuteNonQuery();
        //     sqlConn.Close();
        //     return result;
        //}
        //public int updateaddmast(string ImpBranchNo, string Address, string City, string StateImp,string PartCod)
        //{
        //    int result = new int();
        //    string insertimporter = "update M_ImporterDetails set BranchSno='" + ImpBranchNo + "',Address='" + Address + "',City='" + City + "',State='" + StateImp + "' where PartyCode='" + PartCod + "'";

        //    SqlConnection sqlConn = new SqlConnection(con);
        //    sqlConn.Open();
        //    SqlCommand cmd = new SqlCommand(insertimporter, sqlConn);

        //    result = cmd.ExecuteNonQuery();
        //    sqlConn.Close();
        //    return result;
        //}

        public int Update(string JobNo, string Importer, string ImporterType, string IE_Code_No, string Branch_Sno, string Address, string City, string State, string Importer_Ref_No, string BE_Heading, string Consignor, string ConsignorShName,
        string Consignor_Address, string Consignor_City, string Consignor_Country, string Seller_Name, string ShortSellerName,  string High_IE_Code, string High_Branch_Sno, string High_Address,
        string High_City, string High_State, string High_ZipCode,  string SingleConsignor, string HighSeaSale, string UnderSec46ck, string underSec46, string Kachhack, string Kachha,
        string UnderSec48ck, string UnderSec48, string FirstChkck, string FirstChk, string Greenck, string Green, string ImpShortName, string ZipCode)
        {
            StringBuilder Query = new StringBuilder();
            int result = new int();

            Query.Append("Update T_Importer set Importer=@Importer,ImporterType=@ImporterType,IECodeNo=@IE_Code_No,BranchSno=@Branch_Sno,Address=@Address,");
            Query.Append("City=@City,State=@State,ImporterRefNo=@Importer_Ref_No,BEHeading=@BE_Heading,Consignor=@Consignor,ConsignorShName=@ConsignorShName,");
            Query.Append("ConsignorAddress=@Consignor_Address,ConsignorCity=@Consignor_City,ConsignorCountry=@Consignor_Country,");
            Query.Append("SellerName=@Seller_Name,ShortSellerName=@ShortSellerName,HighIECode=@High_IE_Code,HighBranchSno=@High_Branch_Sno,");
            Query.Append("HighAddress=@High_Address,HighCity=@High_City,HighState=@High_State,HighZipCode=@High_ZipCode,SingleConsignor=@SingleConsignor,");
            Query.Append("HighSeaSale=@HighSeaSale,ChkUnderSec46=@UnderSec46ck,underSec46=@underSec46,ChkKachha=@Kachhack,Kachha=@Kachha,ChkUnderSec48=@UnderSec48ck,");
            Query.Append("UnderSec48=@UnderSec48,ChkFirstChk=@FirstChkck,FirstChk=@FirstChk,ChkGreen=@Greenck,GreenChannel=@Green,ImpShortName=@ImpShortName,ZipCode=@ZipCode where JobNo=@JobNo");

            SqlConnection sqlConn = new SqlConnection(con);
            sqlConn.Open();
            SqlCommand cmd = new SqlCommand(Query.ToString(), sqlConn);
            cmd.Parameters.AddWithValue("@JobNo", JobNo);
            cmd.Parameters.AddWithValue("@Importer", Importer);
            cmd.Parameters.AddWithValue("@ImporterType", ImporterType);
            cmd.Parameters.AddWithValue("@IE_Code_No",IE_Code_No);
            cmd.Parameters.AddWithValue("@Branch_Sno",Branch_Sno);
            cmd.Parameters.AddWithValue("@Address",Address);
            cmd.Parameters.AddWithValue("@City",City);
            cmd.Parameters.AddWithValue("@State",State);
            cmd.Parameters.AddWithValue("@Importer_Ref_No",Importer_Ref_No);
            cmd.Parameters.AddWithValue("@BE_Heading",BE_Heading);
            cmd.Parameters.AddWithValue("@Consignor",Consignor);
            cmd.Parameters.AddWithValue("@ConsignorShName", ConsignorShName);
            cmd.Parameters.AddWithValue("@Consignor_Address",Consignor_Address);
            cmd.Parameters.AddWithValue("@Consignor_City",Consignor_City);
            cmd.Parameters.AddWithValue("@Consignor_Country",Consignor_Country);
            cmd.Parameters.AddWithValue("@Seller_Name",Seller_Name);
            cmd.Parameters.AddWithValue("@ShortSellerName",ShortSellerName);
            cmd.Parameters.AddWithValue("@High_IE_Code",High_IE_Code);
            cmd.Parameters.AddWithValue("@High_Branch_Sno",High_Branch_Sno);
            cmd.Parameters.AddWithValue("@High_Address",High_Address);
            cmd.Parameters.AddWithValue("@High_City",High_City);
            cmd.Parameters.AddWithValue("@High_State",High_State);
            cmd.Parameters.AddWithValue("@High_ZipCode",High_ZipCode);
           
            cmd.Parameters.AddWithValue("@SingleConsignor",SingleConsignor);
            cmd.Parameters.AddWithValue("@HighSeaSale",HighSeaSale);
            cmd.Parameters.AddWithValue("@UnderSec46ck",UnderSec46ck);
            cmd.Parameters.AddWithValue("@underSec46",underSec46);
            cmd.Parameters.AddWithValue("@Kachhack",Kachhack);
            cmd.Parameters.AddWithValue("@Kachha",Kachha);
            cmd.Parameters.AddWithValue("@UnderSec48ck",UnderSec48ck);
            cmd.Parameters.AddWithValue("@UnderSec48",UnderSec48);
            cmd.Parameters.AddWithValue("@FirstChkck",FirstChkck);
            cmd.Parameters.AddWithValue("@FirstChk",FirstChk);
            cmd.Parameters.AddWithValue("@Greenck",Greenck);
            cmd.Parameters.AddWithValue("@Green",Green);
            cmd.Parameters.AddWithValue("@ImpShortName", ImpShortName);
            cmd.Parameters.AddWithValue("@ZipCode", ZipCode);
            result = cmd.ExecuteNonQuery();
            sqlConn.Close();
            return result;
        }

        public DataSet GetImporterDetails(string JobNo)
        {
            DataSet ds = new DataSet();
            try
            {
                string Query = "select * from T_Importer where JobNo='"+JobNo+"' ";
                SqlConnection sqlConn = new SqlConnection(con);
                sqlConn.Open();
                SqlDataAdapter da = new SqlDataAdapter(Query, sqlConn);
                da.Fill(ds, "ImportDetails");
                sqlConn.Close();
            }
            catch (Exception ex)
            {
                string Msg = ex.Message.ToString();
            }
            return ds;
        }
}
 }