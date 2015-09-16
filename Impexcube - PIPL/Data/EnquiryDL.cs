using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;

namespace VTS.ImpexCube.Data
{
    public class EnquiryDL
    {
        string con = (string)ConfigurationManager.ConnectionStrings["Constr"].ConnectionString;

        public int InsertEnquiry(string CustomerName, string PhoneNo, string Address, string EmailId, string ContactPerson, 
            string ModeOfEnquiry, string RITCCode, string Commodity, string IFSCode,string Pol,string pod,
            string Air, string lcl, string feet20,string feet40,string AQuantiy,string AUom,string AGrossWeight, string AChargeableWeight,
            string Avolume, string FContainerType, string FContainerNos, string LQuantiy, string LUom, 
            string LGrossWeight, string LChargeableWeight,
            string Lvolume, string FinalDest, string Clearance, string Cutofdate, bool LoctransInclude, string ShipmentMode, string ContTyp40, string contno, string AirType, string LclType,string CusTyp, string QuoteCompleted,string Enqkey)
        {
            int result = new int();
            string insertEnquiry = "";

            insertEnquiry = "insert into M_Enquriy (CustomerName ,PhoneNo ,Address,EmailId,ContactPerson,ModeOfEnquiry,SalesPerson,Commodity,ReferredBy,Pol,Pod,Air,Lcl,Feet20,Feet40,AQuantity,AUom,AGrossWeight,AChargeableWeight,AVolume ,FContainerType,FContainerNos,LQuantity,LUom,LGrossWeight,LChargeableWeight,LVolume,FinalDest,Clearance,Cutofdate,LoctransInclude,ShipmentMode,ContTyp40,ContNo,AType,LType,CustTyp,QuoteCompleted,EnquiryNo) " +
                   " values ('" + CustomerName + "','" + PhoneNo + "','" + Address + "','" + EmailId + "','" + ContactPerson + "','" + ModeOfEnquiry + "','" + RITCCode + "','" + Commodity + "','" + IFSCode + "','" + Pol + "','" + pod + "'," + Air + ",'" + lcl + "','" + feet20 + "','" + feet40 + "','" + AQuantiy + "','" + AUom + "','" + AGrossWeight + "','" + AChargeableWeight + "','" + Avolume + "','" + FContainerType + "','" + FContainerNos + "','" + LQuantiy + "','" + LUom + "','" + LGrossWeight + "','" + LChargeableWeight + "','" + Lvolume + "','" + FinalDest + "','" + Clearance + "','" + Cutofdate + "', '" + LoctransInclude + "','" + ShipmentMode + "','" + ContTyp40 + "','" + contno + "','" + AirType + "','" + LclType + "','"+CusTyp+"','0','"+Enqkey+"') ";
            SqlConnection sqlConn = new SqlConnection(con);
            sqlConn.Open();
            SqlCommand cmd = new SqlCommand(insertEnquiry, sqlConn);
            SqlDataAdapter da = new SqlDataAdapter();
            cmd.CommandText = insertEnquiry;
            cmd.Connection = sqlConn;
            da.SelectCommand = cmd;
            result = cmd.ExecuteNonQuery();
            sqlConn.Close();
            return result;
        }

        public int UpdateEnquiry(string CustomerName, string PhoneNo, string Address, string EmailId, string ContactPerson,
           string ModeOfEnquiry, string RITCCode, string Commodity, string IFSCode, string Pol, string pod,
           string air,string lcl,string feet20,string feet40, string AQuantiy, string AUom, string AGrossWeight, string AChargeableWeight,
           string Avolume, string FContainerType, string FContainerNos, string LQuantiy, string LUom,
           string LGrossWeight, string LChargeableWeight,
           string Lvolume, int id, string FinalDest, string Clearance, string Cutofdate, bool LoctransInclude,string ShipmentMode, string ContTyp40, string contno,string AirType,string LclType,string CusTyp)
        {
            
            int result = new int();
            string UpdateEnquiryDetails = "";

            UpdateEnquiryDetails = "update M_Enquriy set CustomerName='" + CustomerName + "',PhoneNo='" + PhoneNo + "',Address='" + Address + "',EmailId='" + EmailId + "',ContactPerson='" + ContactPerson + "',ModeOfEnquiry='" + ModeOfEnquiry + "',SalesPerson='" + RITCCode + "',Commodity='" + Commodity + "',ReferredBy='" + IFSCode + "',Pol='" + Pol + "',Pod='" + pod + "',Air=" + air + ",Lcl=" + lcl + ",Feet20=" + feet20 + ",Feet40=" + feet40 + ",AQuantity='" + AQuantiy + "',AUom='" + AUom + "',AGrossWeight='" + AGrossWeight + "',AChargeableWeight='" + AChargeableWeight + "',AVolume='" + Avolume + "',FContainerType='" + FContainerType + "',FContainerNos='" + FContainerNos + "',LQuantity='" + LQuantiy + "',LUom='" + LUom + "',LGrossWeight='" + LGrossWeight + "',LChargeableWeight='" + LChargeableWeight + "',LVolume='" + Lvolume + "',FinalDest='" + FinalDest + "',Clearance='" + Clearance + "',Cutofdate='" + Cutofdate + "', LoctransInclude='" + LoctransInclude + "',ShipmentMode='" + ShipmentMode + "',ContTyp40='"+ ContTyp40+"',ContNo='"+ contno+"',AType='"+ AirType+"',LType='"+ LclType+"',CustTyp='"+CusTyp+"' where TransId='" + id + "' ";
            SqlConnection sqlConn = new SqlConnection(con);
            sqlConn.Open();
            SqlCommand cmd = new SqlCommand(UpdateEnquiryDetails, sqlConn);
            SqlDataAdapter da = new SqlDataAdapter();

            cmd.CommandText = UpdateEnquiryDetails;
            cmd.Connection = sqlConn;
            da.SelectCommand = cmd;
            result = cmd.ExecuteNonQuery();
            sqlConn.Close();
            return result;

        }

        public DataSet SelectByCustomerID(int id)
        {
            DataSet ds = new DataSet();
            try
            {
                string query = "SELECT CustomerName,PhoneNo,Address,EmailId,ContactPerson,ModeOfEnquiry,SalesPerson,Commodity,ReferredBy,Pol,Pod," +
                    " Air,Feet20,Feet40,Lcl,AQuantity,AUom,AGrossWeight,AChargeableWeight,AVolume,FContainerType,FContainerNos,LQuantity," +
                    " LUom,LGrossWeight,LChargeableWeight,LVolume,FinalDest,Clearance,Cutofdate,LoctransInclude,ShipmentMode,ContTyp40,ContNo,AType,LType,CustTyp FROM M_Enquriy where TransId= '" + id + "'";
                SqlConnection sqlConn = new SqlConnection(con);
                sqlConn.Open();
                SqlDataAdapter da = new SqlDataAdapter(query, sqlConn);

                da.Fill(ds, "customer");
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
