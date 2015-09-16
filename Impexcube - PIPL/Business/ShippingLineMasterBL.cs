using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace VTS.ImpexCube.Business
{
   public class ShippingLineMasterBL
    {
       VTS.ImpexCube.Data.ShippingLineMasterDL objShippingMaster = new VTS.ImpexCube.Data.ShippingLineMasterDL();

       public int InsertShipingMaster(string shipper, string address, string contact, string email, string favor)
        {
            return objShippingMaster.InsertShipingMaster(shipper, address, contact, email, favor);
        }

        public DataSet SelectShipingMaster()
        {
            return objShippingMaster.SelectShipingMaster();
        }

        public int UpdateShipingMaster(int id, string shipper, string address, string contact, string email, string favor)
        {
            return objShippingMaster.UpdateShipingMaster(id, shipper, address, contact, email, favor);
        }

    }
}
