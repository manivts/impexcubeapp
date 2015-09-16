using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace VTS.ImpexCube.Data
{
   
    public class ETShipmentDL
    {
        string Message = string.Empty;
        CommonDL CommonDL = new CommonDL();

        public int SaveShipmentMain(string JobNo, string DischargeCountry, string DischargePort, string DestinationCountry, string DestinationPort, string VoyageNo, string ShippingLine, string VesselNo,
                         string  SailingDate,string  EGMNo,string  EGMDate,string  MBLNo,string  MBLDate,string  HBLNo,string  HBLDate,string  PreCarriageby,string  PlaceofReceipt,
                         string  StateOfOrigin,string  AnnexureCDetails,string  NatureofCargo,string  TotalNoofPkgs,string  TotalNoofPkgsUnit,string  LoosePkgs,string  NoofContainers,
                         string GrossWeight, string GrossWeightUnit, string NetWeight, string NetWeightUnit, string MarksNos, string Airlinecode, string Airline, string FlightNo, string FlightDate, string CreatedBy, string CreatedDate)
        {
            StringBuilder Query = new StringBuilder();
            int Result = 0;
            try
            {
                Query.Append("INSERT INTO E_T_Shipment");
                Query.Append("([JobNo],[DischargeCountry],[DischargePort],[DestinationCountry],[DestinationPort],[VoyageNo],[ShippingLine],[VesselNo],");
                Query.Append("[SailingDate],[EGMNo],[EGMDate],[MBLNo],[MBLDate],[HBLNo],[HBLDate],[PreCarriageby],[PlaceofReceipt],");
                Query.Append("[StateOfOrigin],[AnnexureCDetails],[NatureofCargo],[TotalNoofPkgs],[TotalNoofPkgsUnit],[LoosePkgs],[NoofContainers],");
                Query.Append("[GrossWeight],[GrossWeightUnit],[NetWeight],[NetWeightUnit],[MarksNos],Airlinecode, Airline, FlightNo, FlightDate,[CreatedBy],[CreatedDate])");
                Query.Append("Values(");
                Query.Append("'" + JobNo + "','" + DischargeCountry + "','" + DischargePort + "','" + DestinationCountry + "','" + DestinationPort + "','" + VoyageNo + "','" + ShippingLine + "','" + VesselNo + "',");
                Query.Append("'" + SailingDate + "','" + EGMNo + "','" + EGMDate + "','" + MBLNo + "','" + MBLDate + "','" + HBLNo + "','" + HBLDate + "','" + PreCarriageby + "','" + PlaceofReceipt + "',");
                Query.Append("'" + StateOfOrigin + "','" + AnnexureCDetails + "','" + NatureofCargo + "','" + TotalNoofPkgs + "','" + TotalNoofPkgsUnit + "','" + LoosePkgs + "','" + NoofContainers + "',");
                Query.Append("'" + GrossWeight + "','" + GrossWeightUnit + "','" + NetWeight + "','" + NetWeightUnit + "','" + MarksNos + "','"+Airlinecode+"','"+Airline+"','"+FlightNo+"','"+FlightDate+"','" + CreatedBy + "','" + CreatedDate + "')");
                Result = CommonDL.ExecuteNonQuery(Query.ToString());
            }
            catch (Exception ex)
            {
                string Msg = ex.Message;
            }
            return Result;
        }

        public int UpdateShipmentMain(string JobNo, string DischargeCountry, string DischargePort, string DestinationCountry, string DestinationPort, string VoyageNo, string ShippingLine, string VesselNo,
                         string SailingDate, string EGMNo, string EGMDate, string MBLNo, string MBLDate, string HBLNo, string HBLDate, string PreCarriageby, string PlaceofReceipt,
                         string StateOfOrigin, string AnnexureCDetails, string NatureofCargo, string TotalNoofPkgs, string TotalNoofPkgsUnit, string LoosePkgs, string NoofContainers,
                         string GrossWeight, string GrossWeightUnit, string NetWeight, string NetWeightUnit, string MarksNos,string Airlinecode,string Airline,string FlightNo,string FlightDate, string ModifiedBy, string ModifiedDate)
        {
             StringBuilder Query = new StringBuilder();
             int Result = 0;
             try
             {
                 Query.Append("UPDATE [E_T_Shipment] SET ");
                 Query.Append(" [DischargeCountry] = '" + DischargeCountry + "',[NatureofCargo] = '" + NatureofCargo + "',[DischargePort] = '" + DischargePort + "',");
                 Query.Append("[TotalNoofPkgs] = '" + TotalNoofPkgs + "',[TotalNoofPkgsUnit] = '" + TotalNoofPkgsUnit + "',[DestinationCountry] = '" + DestinationCountry + "',[LoosePkgs] = '" + LoosePkgs + "',");
                 Query.Append("[DestinationPort] = '" + DestinationPort + "',[NoofContainers] = '" + NoofContainers + "',[VoyageNo] = '" + VoyageNo + "',[ShippingLine] = '" + ShippingLine + "',");
                 Query.Append("[GrossWeight] = '" + GrossWeight + "',[GrossWeightUnit] = '" + GrossWeightUnit + "',[VesselNo] = '" + VesselNo + "',[SailingDate] = '" + SailingDate + "',[NetWeight] = '" + NetWeight + "',");
                 Query.Append("[NetWeightUnit] = '" + NetWeightUnit + "',[EGMNo] = '" + EGMNo + "',[EGMDate] = '" + EGMDate + "',[MarksNos] = '" + MarksNos + "',[MBLNo] = '" + MBLNo + "',[MBLDate] = '" + MBLDate + "',[HBLNo] = '" + HBLNo + "',[HBLDate] = '" + HBLDate + "',");
                 Query.Append("[PreCarriageby] = '" + PreCarriageby + "',[PlaceofReceipt] = '" + PlaceofReceipt + "',[StateOfOrigin] = '" + StateOfOrigin + "',[AnnexureCDetails] = '" + AnnexureCDetails + "',Airlinecode='"+Airlinecode+"',Airline='"+Airline+"',FlightNo='"+FlightNo+"',FlightDate='"+FlightDate+"',");
                 Query.Append("[ModifiedBy] = '" + ModifiedBy + "',[ModifiedDate] = '" + ModifiedDate + "' ");
                 Query.Append(" where [JobNo]='" + JobNo + "'");
                 Result = CommonDL.ExecuteNonQuery(Query.ToString());
             }
             catch (Exception ex)
             {
                 string Msg = ex.Message;
             }
             return Result;
        }


        public int UpdateShipmentStuffingDetails(string JobNo, string GoodsStuffedAt, string SampleAccompanied, string FactoryAddress, string SealType, string SealNo, string AgencyName, string ModifiedBy, string ModifiedDate)
        {
            StringBuilder Query = new StringBuilder();
            int Result = 0;
            try
            {
                Query.Append("UPDATE [E_T_Shipment] SET ");
                Query.Append("[GoodsStuffedAt] = '" + GoodsStuffedAt + "',[SampleAccompanied] = '" + SampleAccompanied + "',[FactoryAddress] = '" + FactoryAddress + "',[SealType] = '" + SealType + "',");
                Query.Append("[SealNo] = '" + SealNo + "',[AgencyName] = '" + AgencyName + "',");
                Query.Append("[ModifiedBy] = '" + ModifiedBy + "',[ModifiedDate] = '" + ModifiedDate + "' ");
                Query.Append(" where [JobNo]='" + JobNo + "'");
                Result = CommonDL.ExecuteNonQuery(Query.ToString());
            }
            catch (Exception ex)
            {
                string Msg = ex.Message;
            }
            return Result;
        }

        public int UpdateShipmentInvoicePrinting(string JobNo, string BuyersOrderNo, string BuyersOrderDate, string OtherReferences, string TermsofDeliveryPayment, string OriginCountry, string InvoiceHeader, string ModifiedBy, string ModifiedDate)
        {
            StringBuilder Query = new StringBuilder();
            int Result = 0;
            try
            {
                Query.Append("UPDATE [E_T_Shipment] SET ");
                Query.Append("[BuyersOrderNo] = '" + BuyersOrderNo + "',[BuyersOrderDate] = '" + BuyersOrderDate + "',");
                Query.Append("[OtherReferences] = '" + OtherReferences + "',[TermsofDeliveryPayment] = '" + TermsofDeliveryPayment + "',[OriginCountry] = '" + OriginCountry + "',[InvoiceHeader] = '" + InvoiceHeader + "',");
                Query.Append("[ModifiedBy] = '" + ModifiedBy + "',[ModifiedDate] = '" + ModifiedDate + "' ");
                Query.Append(" where [JobNo]='" + JobNo + "'");
                Result = CommonDL.ExecuteNonQuery(Query.ToString());
            }
            catch (Exception ex)
            {
                string Msg = ex.Message;
            }
            return Result;
        }

        public int UpdateShipmentBillPrinting(string JobNo, string QCertNoDate, string ExportTradeControl, string TypeofShipment, string ExportUnder, string SBType, string SBHeading, string SBbottomarea, string ModifiedBy, string ModifiedDate)
        {
            StringBuilder Query = new StringBuilder();
            int Result = 0;
            try
            {
                Query.Append("UPDATE [E_T_Shipment] SET ");
                Query.Append("[QCertNoDate] = '" + QCertNoDate + "',[ExportTradeControl] = '" + ExportTradeControl + "',[TypeofShipment] = '" + TypeofShipment + "',[ExportUnder] = '" + ExportUnder + "',");
                Query.Append("[SBType] = '" + SBType + "',[SBHeading] = '" + SBHeading + "',[SBbottomarea] = '" + SBbottomarea + "',");
                Query.Append("[ModifiedBy] = '" + ModifiedBy + "',[ModifiedDate] = '" + ModifiedDate + "' ");
                Query.Append(" where [JobNo]='" + JobNo + "'");
                Result = CommonDL.ExecuteNonQuery(Query.ToString());
            }
            catch (Exception ex)
            {
                string Msg = ex.Message;
            }
            return Result;
        }

        public DataSet GetShipmentData(string JobNo)
        {
            DataSet ds = new DataSet();
            StringBuilder Query = new StringBuilder();
            try
            {
                Query.Append("Select ");
                Query.Append("[JobNo],[DischargeCountry],[NatureofCargo],[DischargePort],[TotalNoofPkgs],[TotalNoofPkgsUnit],[DestinationCountry],[LoosePkgs],[DestinationPort],[NoofContainers],");
                Query.Append("[VoyageNo],[ShippingLine],[GrossWeight],[GrossWeightUnit],[VesselNo],[SailingDate],[NetWeight],[NetWeightUnit],[EGMNo],[EGMDate],[MarksNos],");
                Query.Append("[MBLNo],[MBLDate],[HBLNo],[HBLDate],[PreCarriageby],[PlaceofReceipt],[StateOfOrigin],[AnnexureCDetails],[GoodsStuffedAt],[SampleAccompanied],[FactoryAddress],");
                Query.Append("[SealType],[SealNo],[AgencyName],[BuyersOrderNo],[BuyersOrderDate],[OtherReferences],[TermsofDeliveryPayment],[OriginCountry],[InvoiceHeader],[QCertNoDate],");
                Query.Append("[ExportTradeControl],[TypeofShipment],[ExportUnder],[SBType],[SBHeading],[SBbottomarea],[CreatedBy],[CreatedDate],[ModifiedBy],[ModifiedDate] ");
                Query.Append("  FROM [E_T_Shipment] where [JobNo]='" + JobNo + "'");

                ds = CommonDL.GetDataSet(Query.ToString());
            }
            catch (Exception ex)
            {
                string Msg = ex.Message;
            }
            return ds;
        }

        public int SaveContainer(string JobNo, string ContainerNo, string SealNo, string SealDate, string Size, string Type, string NoofPktsStuffed,
            string Transporter, string CreatedBy, string CreatedDate, string ModifiedBy, string ModifiedDate)
        {
            StringBuilder Query = new StringBuilder();
            int Result = 0;
            try
            {
                Query.Append("INSERT INTO E_T_Container");
                Query.Append("([JobNo],[ContainerNo],[SealNo],[SealDate],[Size],[Type],[NoofPktsStuffed],[Transporter],[CreatedBy],[CreatedDate],[ModifiedBy],[ModifiedDate])");
                Query.Append("Values(");
                Query.Append("'" + JobNo + "','" + ContainerNo + "','" + SealNo + "','" + SealDate + "','" + Size + "','" + Type + "','" + NoofPktsStuffed + "',");
                Query.Append("'" + Transporter + "','" + CreatedBy + "','" + CreatedDate + "','" + ModifiedBy + "','" + ModifiedDate + "')");
                Result = CommonDL.ExecuteNonQuery(Query.ToString());
            }
            catch (Exception ex)
            {
                string Mesg = ex.Message;
            }
            return Result;
        }

        public int UpdateContainer(string TransId,string JobNo, string ContainerNo, string SealNo, string SealDate, string Size, string Type, string NoofPktsStuffed,
            string Transporter, string ModifiedBy, string ModifiedDate)
        {
            StringBuilder Query = new StringBuilder();
            int Result = 0;
            try
            {
                Query.Append("Update E_T_Container Set ");
                Query.Append("[ContainerNo]='" + ContainerNo + "',[SealNo]='" + SealNo + "',[SealDate]='" + SealDate + "',");
                Query.Append("[Size]= '" + Size + "',[Type]='" + Type + "',[NoofPktsStuffed]='" + NoofPktsStuffed + "',[Transporter]='" + Transporter + "',");
                Query.Append("[ModifiedBy]= '" + ModifiedBy + "',[ModifiedDate]= '" + ModifiedDate + "' where [JobNo]='" + JobNo + "' and ID = '" + TransId + "'");                
                Result = CommonDL.ExecuteNonQuery(Query.ToString());
            }
            catch (Exception ex)
            {
                string Mesg = ex.Message;
            }
            return Result;
        }

        public DataSet SelectContainer(string JobNo)
        {
            DataSet ds = new DataSet();
            StringBuilder Query = new StringBuilder();
            try
            {
                Query.Append("Select ");
                Query.Append("[ID],[JobNo],[ContainerNo],[SealNo],[SealDate],[Size],[Type],[NoofPktsStuffed],[Transporter]");
                Query.Append("  FROM [E_T_Container] where [JobNo]='" + JobNo + "'");

                ds = CommonDL.GetDataSet(Query.ToString());
            }
            catch (Exception ex)
            {
                string Msg = ex.Message;
            }

            return ds;
        }
      
    }
}
