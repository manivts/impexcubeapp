// -----------------------------------------------------------------------
// <copyright file="Importer.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace VTS.ImpexCube.Business
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Data;
    using VTS.ImpexCube.Data;
    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class Importer
    {
        VTS.ImpexCube.Data.Importer objimporter = new VTS.ImpexCube.Data.Importer();

        public DataSet jobno(string jobno)
        {
            return objimporter.jobno(jobno);
        }
        

        public DataSet importer()
        {
            return objimporter.importer();
        }
        
        public DataSet branchsno(string importer)
        {
            return objimporter.branchsno(importer);
        }
        public DataSet branchdetails(string importer, string branch)
        {
            return objimporter.branchdetails(importer,branch);
        }
        public DataSet checksave(string importer)
        {
            return objimporter.checksave(importer);
        }


        public int insert(string JobNo,string Importer, string IE_Code_No, string Branch_Sno, string Address, string City, string State, string Importer_Ref_No,
          string BE_Heading, string Consignor,string  ConsignorShName,string Consignor_Address, string Consignor_City, string Consignor_Country, string Seller_Name, string ShortSellerName, string High_IE_Code, string High_Branch_Sno, string High_Address, string High_City, string High_State, string High_ZipCode, string SingleConsignor, string HighSeaSale, string UnderSec46ck, string underSec46, string Kachhack, string Kachha, string UnderSec48ck, string UnderSec48, string FirstChkck, string FirstChk, string Greenck, string Green, string ImporterCode, string ImporterType, string ImpShortName, string HighShortName, string ZipCode)
        {
            return objimporter.insert(JobNo, Importer, IE_Code_No, Branch_Sno, Address, City, State, Importer_Ref_No, BE_Heading, Consignor,ConsignorShName, Consignor_Address, Consignor_City, Consignor_Country, Seller_Name, ShortSellerName, High_IE_Code, High_Branch_Sno, High_Address, High_City, High_State, High_ZipCode, SingleConsignor, HighSeaSale, UnderSec46ck, underSec46, Kachhack, Kachha, UnderSec48ck, UnderSec48, FirstChkck, FirstChk, Greenck, Green, ImporterCode, ImporterType, ImpShortName, HighShortName, ZipCode);
              
                
        }
        public int Update(string JobNo, string Importer, string ImporterType, string IE_Code_No, string Branch_Sno, string Address, string City, string State, string Importer_Ref_No,
         string BE_Heading, string Consignor, string ConsignorShName, string Consignor_Address, string Consignor_City, string Consignor_Country, string Seller_Name, string ShortSellerName, string High_IE_Code, string High_Branch_Sno, 
         string High_Address, string High_City, string High_State, string High_ZipCode, string SingleConsignor, string HighSeaSale, string UnderSec46ck, string underSec46, string Kachhack, string Kachha, string UnderSec48ck, string UnderSec48, 
         string FirstChkck, string FirstChk, string Greenck, string Green,string ImpShortName,string ZipCode)
        {
            return objimporter.Update(JobNo,Importer,ImporterType, IE_Code_No, Branch_Sno, Address, City, State,  Importer_Ref_No,  BE_Heading, Consignor,ConsignorShName, Consignor_Address, Consignor_City, Consignor_Country, Seller_Name, ShortSellerName,High_IE_Code, High_Branch_Sno, High_Address, High_City, High_State, High_ZipCode, SingleConsignor, HighSeaSale, UnderSec46ck, underSec46, Kachhack, Kachha, UnderSec48ck, UnderSec48, FirstChkck, FirstChk, Greenck, Green,ImpShortName, ZipCode);
        }

        public DataSet GetImporterDetails(string JobNo)
        {
            return objimporter.GetImporterDetails(JobNo);
        }
   
    }
}
