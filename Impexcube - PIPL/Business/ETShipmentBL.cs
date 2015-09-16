using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace VTS.ImpexCube.Business
{
    public class ETShipmentBL
    {
        VTS.ImpexCube.Data.ETShipmentDL ETShipment = new VTS.ImpexCube.Data.ETShipmentDL();


        public int SaveShipmentMain(string JobNo, string DischargeCountry, string DischargePort, string DestinationCountry, string DestinationPort, string VoyageNo, string ShippingLine, string VesselNo,
                     string SailingDate, string EGMNo, string EGMDate, string MBLNo, string MBLDate, string HBLNo, string HBLDate, string PreCarriageby, string PlaceofReceipt,
                     string StateOfOrigin, string AnnexureCDetails, string NatureofCargo, string TotalNoofPkgs, string TotalNoofPkgsUnit, string LoosePkgs, string NoofContainers,
                     string GrossWeight, string GrossWeightUnit, string NetWeight, string NetWeightUnit, string MarksNos,string Airlinecode,string Airline, string FlightNo, string FlightDate, string CreatedBy, string CreatedDate)
        {
            return  ETShipment.SaveShipmentMain(JobNo, DischargeCountry, DischargePort, DestinationCountry, DestinationPort, VoyageNo, ShippingLine, VesselNo,
                      SailingDate, EGMNo, EGMDate, MBLNo, MBLDate, HBLNo, HBLDate, PreCarriageby, PlaceofReceipt,
                      StateOfOrigin, AnnexureCDetails, NatureofCargo, TotalNoofPkgs, TotalNoofPkgsUnit, LoosePkgs, NoofContainers,
                      GrossWeight, GrossWeightUnit, NetWeight, NetWeightUnit, MarksNos,Airlinecode, Airline, FlightNo, FlightDate, CreatedBy, CreatedDate);
        }

        public int UpdateShipmentMain(string JobNo, string DischargeCountry, string DischargePort, string DestinationCountry, string DestinationPort, string VoyageNo, string ShippingLine, string VesselNo,
                    string SailingDate, string EGMNo, string EGMDate, string MBLNo, string MBLDate, string HBLNo, string HBLDate, string PreCarriageby, string PlaceofReceipt,
                    string StateOfOrigin, string AnnexureCDetails, string NatureofCargo, string TotalNoofPkgs, string TotalNoofPkgsUnit, string LoosePkgs, string NoofContainers,
                    string GrossWeight, string GrossWeightUnit, string NetWeight, string NetWeightUnit, string MarksNos,string Airlinecode,string Airline,string FlightNo,string FlightDate, string ModifiedBy, string ModifiedDate)
        {
            return ETShipment.UpdateShipmentMain(JobNo, DischargeCountry, DischargePort, DestinationCountry, DestinationPort, VoyageNo, ShippingLine, VesselNo,
                     SailingDate, EGMNo, EGMDate, MBLNo, MBLDate, HBLNo, HBLDate, PreCarriageby, PlaceofReceipt,
                     StateOfOrigin, AnnexureCDetails, NatureofCargo, TotalNoofPkgs, TotalNoofPkgsUnit, LoosePkgs, NoofContainers,
                     GrossWeight, GrossWeightUnit, NetWeight, NetWeightUnit, MarksNos,Airlinecode,Airline,FlightNo,FlightDate, ModifiedBy, ModifiedDate);
        }

        public int UpdateShipmentStuffingDetails(string JobNo, string GoodsStuffedAt, string SampleAccompanied, string FactoryAddress, string SealType, string SealNo, string AgencyName, string ModifiedBy, string ModifiedDate)
        {
            return ETShipment.UpdateShipmentStuffingDetails(JobNo, GoodsStuffedAt, SampleAccompanied, FactoryAddress, SealType, SealNo, AgencyName, ModifiedBy, ModifiedDate);
        }

        public int UpdateShipmentInvoicePrinting(string JobNo, string BuyersOrderNo, string BuyersOrderDate, string OtherReferences, string TermsofDeliveryPayment, string OriginCountry, string InvoiceHeader, string ModifiedBy, string ModifiedDate)
        {
            return ETShipment.UpdateShipmentInvoicePrinting(JobNo, BuyersOrderNo, BuyersOrderDate, OtherReferences, TermsofDeliveryPayment, OriginCountry, InvoiceHeader, ModifiedBy, ModifiedDate);
        }

        public int UpdateShipmentBillPrinting(string JobNo, string QCertNoDate, string ExportTradeControl, string TypeofShipment, string ExportUnder, string SBType, string SBHeading, string SBbottomarea, string ModifiedBy, string ModifiedDate)
        {
            return ETShipment.UpdateShipmentBillPrinting(JobNo, QCertNoDate, ExportTradeControl, TypeofShipment, ExportUnder, SBType, SBHeading, SBbottomarea, ModifiedBy, ModifiedDate);
        }

        public DataSet GetShipmentData(string JobNo)
        {
            return ETShipment.GetShipmentData(JobNo);
        }

        public int SaveContainer(string JobNo, string ContainerNo, string SealNo, string SealDate, string Size, string Type, string NoofPktsStuffed,
            string Transporter, string CreatedBy, string CreatedDate, string ModifiedBy, string ModifiedDate)
        {
            return ETShipment.SaveContainer(JobNo, ContainerNo, SealNo, SealDate, Size, Type, NoofPktsStuffed, Transporter, CreatedBy, CreatedDate, ModifiedBy, ModifiedDate);
        }

        public int UpdateContainer(string TransId, string JobNo, string ContainerNo, string SealNo, string SealDate, string Size, string Type, string NoofPktsStuffed,
            string Transporter, string ModifiedBy, string ModifiedDate)
        {
            return ETShipment.UpdateContainer(TransId, JobNo, ContainerNo, SealNo, SealDate, Size, Type, NoofPktsStuffed, Transporter, ModifiedBy, ModifiedDate);
        }

        public DataSet SelectContainer(string JobNo)
        {
            return ETShipment.SelectContainer(JobNo);
        }
    }
}
   

