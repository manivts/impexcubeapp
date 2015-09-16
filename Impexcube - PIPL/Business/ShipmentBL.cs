// -----------------------------------------------------------------------
// <copyright file="ShipmentBL.cs" company="">
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

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class ShipmentBL
    {
        VTS.ImpexCube.Data.ShipmentDL objShipment = new VTS.ImpexCube.Data.ShipmentDL();

        public int InsertShipmentDetails(string Jobno, string jobdate, string shipmentcountry, string shipmentport, string origin, string vessel, string voyage, string transit,
            string eta, string glddate, string line, string igmno, string igmdate, string mblno, string mbldate, string hblno, string hbldate, string gatewayno, string gatewaydate,
            string lineno, string reporting, double Tcontainer, double Fcontainer, string gross, string grossunit, string packages, string packageunit,
            string stc, string stcunit, string stc1, string stcunit1, string CFSName, string MarksNos,string NetWeight,string NetUint, string createdby, string createddate, string modifiedby,
            string modifieddate, string ShipmentCountryCode, string ShipmentPortCode, string CountryOriginCode, string AgentName, string FFName, string ShipmentPortUneceCode)
           // txtPort.Text, Tft, tot20ft, Fft, tot40ft, txtGrossWeight.Text, ddlGrossWeight.SelectedValue, txtPackages.Text, ddlPackages.SelectedValue, txtSTC.Text, ddlSTC.SelectedValue, txtSTC2.Text, ddlSTC2.SelectedValue,txtCFSName.Text,txtMarksNos.Text, (string)Session["USER-NAME"], date, (string)Session["USER-NAME"], date);
        {
            return objShipment.InsertShipmentDetails(Jobno, jobdate, shipmentcountry, shipmentport, origin, vessel, voyage, transit, eta, glddate, line, igmno, igmdate, mblno, mbldate, hblno, hbldate,
                gatewayno, gatewaydate, lineno, reporting, Tcontainer,  Fcontainer,  gross, grossunit, packages, packageunit, stc, stcunit, stc1, stcunit1,CFSName,MarksNos,NetWeight,NetUint, createdby, createddate,
                modifiedby, modifieddate, ShipmentCountryCode, ShipmentPortCode, CountryOriginCode, AgentName, FFName, ShipmentPortUneceCode);
        }

        public int InsertShipmentContainerInfo(string ShipTransID,string Jobno, string jobdate,string container, string containertype, string containerno, string sealno, string loadtype,
            string createdby, string createddate, string modifiedby, string modifieddate)
        {
            return objShipment.InsertShipmentContainerInfo(ShipTransID,Jobno, jobdate,container,containertype, containerno, sealno, loadtype, createdby, createddate, modifiedby, modifieddate);
        }

        public DataSet GetJobNo()
        {
            return objShipment.GetJobNo();
        }
        public DataSet GetState()
        {
            return objShipment.GetState();
        }
        public DataSet GetCountry()
        {
            return objShipment.GetCountry();
        }
        public DataSet GetPort()
        {
            return objShipment.GetPort();
        }

        public DataSet GetPort(string Country)
        {
            return objShipment.GetPort(Country);
        }
        public DataSet GetPort(string Country, string portcode)
        {
            return objShipment.GetPort(Country, portcode);
        }
        public string GetCountryCode(string Country)
        {
            return objShipment.GetCountryCode(Country);
        }
        
        public DataSet SelectJobNo(string JobNo)
        {
            return objShipment.SelectJobNo(JobNo);
        }

        public DataSet GetShipmentDetails(string TransId)
        {
            return objShipment.GetShipmentDetails(TransId);
        }
        public DataSet GetShipmentDetailsGrid(string JobNo)
        {
            return objShipment.GetShipmentDetailsGrid(JobNo);
        }
        public DataSet GetJobShipmentContainerInfo(string jobno, string shipmentID)
        {
            return objShipment.GetJobShipmentContainerInfo(jobno,shipmentID);
        }

        public DataSet GetJobSummary(string jobno)
        {
            return objShipment.GetJobSummary(jobno);
        }

        public int UpdateShipmentDetails(string ShipmentID, string Jobno, string jobdate, string shipmentcountry, string shipmentport, string origin, string vessel, string voyage, string transit,
            string eta, string glddate, string line, string igmno, string igmdate, string mblno, string mbldate, string hblno, string hbldate, string gatewayno, string gatewaydate,
            string lineno, string reporting, double Tcontainer,  double Fcontainer,  string gross, string grossunit, string packages, string packageunit,
            string stc, string stcunit, string stc1, string stcunit1, string CFSName, string MarksNos,string NetWeight,string NetUint, string modifiedby, string modifieddate,
            string ShipmentCountryCode, string ShipmentPortCode, string CountryOriginCode, string AgentName, string FFName, string ShipmentPortUneceCode)
        {
            return objShipment.UpdateShipmentDetails(ShipmentID,Jobno, jobdate, shipmentcountry, shipmentport, origin, vessel, voyage, transit, eta, glddate, line, igmno, igmdate, mblno, mbldate, hblno, hbldate,
                gatewayno, gatewaydate, lineno, reporting, Tcontainer, Fcontainer, gross, grossunit, packages, packageunit, stc, stcunit, stc1, stcunit1, CFSName, MarksNos, NetWeight, NetUint, modifiedby, modifieddate,
                ShipmentCountryCode, ShipmentPortCode, CountryOriginCode, AgentName, FFName, ShipmentPortUneceCode);
        }

        public int UpdateShipmentContainerInfo(int id, string Jobno, string jobdate,string container, string containertype, string containerno, string sealno, string loadtype,
            string modifiedby, string modifieddate)
        {
            return objShipment.UpdateShipmentContainerInfo(id,Jobno, jobdate, container,containertype, containerno, sealno, loadtype, modifiedby, modifieddate);
        }
        public DataSet GetVesselName()
        {
            return objShipment.GetVesselName();
        }
        public DataSet GetShippingName()
        {
            return objShipment.GetShippingName();
        }
        public DataSet GetAirName()
        {
            return objShipment.GetAirName();
        }
        public DataSet GetAgentName()
        {
            return objShipment.GetAgentName();
        }
        public DataSet GetCFSName()
        {
            return objShipment.GetCFSName();
        }
        public DataSet GetFFName(string AccountType)
        {
            return objShipment.GetFFName(AccountType);
        }
        public int DeleteShipment(string TransID)
        {
            return objShipment.DeleteShipment(TransID);
        }
        public int DeleteContainerShipment(string TransID)
        {
            return objShipment.DeleteContainerShipment(TransID);
        }

        public string GetShipmentTransID(string JobNo)
        {
            return objShipment.GetShipmentTransID(JobNo);
        }
        public DataSet GetContainerType()
        {
            return objShipment.GetContainerType();
        }
        public int containerdts(string containertype, int shipmentid)
        {
            return objShipment.containerdts(containertype, shipmentid);
        }


        public int InsertCommercialTax(string Jobno, string Ctax_StateCode, string Ctax_StateName, string Ctax_Type,
            string Ctax_RegNo,
            string createdby, string createddate, string modifiedby, string modifieddate)
        {
            return objShipment.InsertCommercialTax(Jobno, Ctax_StateCode, Ctax_StateName, Ctax_Type,
                Ctax_RegNo,
                createdby, createddate, modifiedby, modifieddate);
        }


        public int UpdateCommercialTax(string Jobno, string Ctax_StateCode, string Ctax_StateName, string Ctax_Type,
            string Ctax_RegNo,
            string modifiedby, string modifieddate)
        {
            return objShipment.UpdateCommercialTax( Jobno,  Ctax_StateCode,  Ctax_StateName,  Ctax_Type,Ctax_RegNo,modifiedby,  modifieddate);
        }

    }
}
