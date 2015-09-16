using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace VTS.ImpexCube.Business
{
    public class EnquiryBL
    {
        VTS.ImpexCube.Data.EnquiryDL objEnquiry = new VTS.ImpexCube.Data.EnquiryDL();


        public int InsertEnquiry(string CustomerName, string PhoneNo, string Address, string EmailId, string ContactPerson,
            string ModeOfEnquiry, string RITCCode, string Commodity, string IFSCode, string Pol, string pod,
            string Air, string lcl, string feet20, string feet40, string AQuantiy, string AUom, string AGrossWeight, string AChargeableWeight,
            string Avolume, string FContainerType, string FContainerNos, string LQuantiy, string LUom,
            string LGrossWeight, string LChargeableWeight,
            string Lvolume, string FinalDest, string Clearance, string Cutofdate, bool LoctransInclude, string ShipmentMode, string ContTyp40, string contno,string AirType,string LclType,string CusTyp,string QuoteCompleted,string Enqkey)
        {

            return objEnquiry.InsertEnquiry(CustomerName, PhoneNo, Address, EmailId, ContactPerson, ModeOfEnquiry, RITCCode, Commodity, IFSCode, Pol, pod, Air, lcl, feet20, feet40, AQuantiy, AUom, AGrossWeight, AChargeableWeight, Avolume, FContainerType, FContainerNos, LQuantiy, LUom, LGrossWeight, LChargeableWeight, Lvolume, FinalDest, Clearance, Cutofdate, LoctransInclude, ShipmentMode, ContTyp40, contno, AirType, LclType,CusTyp, QuoteCompleted,Enqkey);


        }


        public int UpdateEnquiry(string CustomerName, string PhoneNo, string Address, string EmailId, string ContactPerson,
           string ModeOfEnquiry, string RITCCode, string Commodity, string IFSCode, string Pol, string pod,
           string air, string lcl, string feet20,string feet40, string AQuantiy, string AUom, string AGrossWeight, string AChargeableWeight,
           string Avolume, string FContainerType, string FContainerNos, string LQuantiy, string LUom,
           string LGrossWeight, string LChargeableWeight,
           string Lvolume, int id, string FinalDest, string Clearance, string Cutofdate, bool LoctransInclude, string ShipmentMode, string ContTyp40, string contno,string AirType,string LclType,string CusTyp)
        {
            return objEnquiry.UpdateEnquiry(CustomerName, PhoneNo, Address, EmailId, ContactPerson, ModeOfEnquiry, RITCCode, Commodity, IFSCode, Pol, pod, air, lcl, feet20, feet40, AQuantiy, AUom, AGrossWeight, AChargeableWeight, Avolume, FContainerType, FContainerNos, LQuantiy, LUom, LGrossWeight, LChargeableWeight, Lvolume, id, FinalDest, Clearance, Cutofdate, LoctransInclude, ShipmentMode, ContTyp40, contno, AirType, LclType,CusTyp);

        }

        public DataSet SelectByCustomerID(int id)
        {
            return objEnquiry.SelectByCustomerID(id);
        }


    }
}
