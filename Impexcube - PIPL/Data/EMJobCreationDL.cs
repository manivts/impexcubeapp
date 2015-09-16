using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
namespace VTS.ImpexCube.Data
{
    public class EMJobCreationDL
    {
        CommonDL CommonDL = new CommonDL();

        public int JobCreationSave(string JobNo, string JobDate, string JobReceivedOn, string TransportMode, string CustomHouse, string Filling, string TotNoInv, string TotInvValue, string Currency, string CreatedBy, string CreatedDate, string Fyear)
        {
            StringBuilder Query = new StringBuilder();
            string Message = string.Empty;
            int Result = 0;
            try
            {
                Query.Append("INSERT INTO [E_M_JobCreation]");
                Query.Append("([JobNo],[JobDate],[JobReceivedOn],[TransportMode],[CustomHouse],[Filling],[TotalNoofInvoice],[TotalInvoiceValue],[Currency],[CreatedBy],[CreatedDate],[FYear])");
                Query.Append("VALUES(");
                Query.Append("'" + JobNo + "','" + JobDate + "','" + JobReceivedOn + "','" + TransportMode + "','" + CustomHouse + "','" + Filling + "','"+TotNoInv+"','"+TotInvValue+"','"+Currency+"','" + CreatedBy + "','" + CreatedDate + "','" + Fyear + "')");
                Result = CommonDL.ExecuteNonQuery(Query.ToString());
            }
            catch (Exception ex)
            {
                Message = ex.Message;
            }
            return Result;
        }

        public int JobCreationUpdate(string JobNo, string JobDate, string JobReceivedOn, string TransportMode, string CustomHouse, string Filling, string TotNoInv, string TotInvValue, string Currency, string ModifiedBy, string ModifiedDate)
        {
            StringBuilder Query = new StringBuilder();
            string Message = string.Empty;
            int Result = 0;
            try
            {
                Query.Append(" UPDATE [E_M_JobCreation] ");
                Query.Append(" SET [JobDate] ='" + JobDate + "',[JobReceivedOn] ='" + JobReceivedOn + "',[TransportMode] ='" + TransportMode + "',[CustomHouse] ='" + CustomHouse + "',[Filling] ='" + Filling + "',[TotalNoofInvoice] ='" + TotNoInv + "',[TotalInvoiceValue] ='" + TotInvValue + "',[Currency] ='" + Currency + "',");
                Query.Append("[ModifiedBy] ='" + ModifiedBy + "',[ModifiedDate] ='" + ModifiedDate + "'");
                Query.Append("  where [JobNo]='" + JobNo + "'");
                Result = CommonDL.ExecuteNonQuery(Query.ToString());
            }
            catch (Exception ex)
            {
                Message = ex.Message;
            }
            return Result;
        }

        public DataSet GetData(string JobNo)
        {
            StringBuilder Query = new StringBuilder();
            string Message = string.Empty;
            DataSet ds = new DataSet();
            try
            {
                Query.Append("SELECT top(10) a.[ID],a.[JobNo],a.[JobDate],a.[JobReceivedOn],a.[TransportMode],a.[CustomHouse],b.ExporterName ");
                Query.Append("FROM [E_M_JobCreation] a,E_T_Exporter b where a.jobno = b.jobno and a.[JobNo] like '%" + JobNo + "%' order by a.ID desc" );

                ds =  CommonDL.GetDataSet(Query.ToString());
            }
            catch (Exception ex)
            {
                Message = ex.Message;
            }
            return ds;
        }

        public DataSet GetGridData(string fyear)
        {
            StringBuilder Query = new StringBuilder();
            string Message = string.Empty;
            DataSet ds = new DataSet();
            try
            {
                Query.Append("SELECT top(10) a.[ID],a.[JobNo],a.[JobDate],a.[JobReceivedOn],a.[TransportMode],a.[CustomHouse],b.ExporterName ");
                Query.Append("FROM [E_M_JobCreation] a,E_T_Exporter b where a.fyear='" + fyear +"' and a.jobno = b.jobno order by jobno desc");

                ds =  CommonDL.GetDataSet(Query.ToString());
            }
            catch (Exception ex)
            {
                Message = ex.Message;
            }
            return ds;
        }

        public DataSet JobNo()
        {
            DataSet ds = new DataSet();
            try
            {
                string Query = "select * from M_AutoGenerate where keyname='EJobNo' ";

                ds =  CommonDL.GetDataSet(Query.ToString());
            }
            catch (Exception ex)
            {
                string Msg = ex.Message.ToString();
            }
            return ds;
        }

        public DataSet CustomHouse()
        {
            DataSet ds = new DataSet();
            try
            {
                string Query = "select custom from M_Custom";

                ds = CommonDL.GetDataSet(Query.ToString());
            }
            catch (Exception ex)
            {
                string Msg = ex.Message.ToString();
            }
            return ds;
        }


        public int updateautono(int keycode)
        {

            int result = new int();
            string Query = "update M_AutoGenerate set KeyCode='" + keycode + "' where keyname='EJobNo' ";
            result = CommonDL.ExecuteNonQuery(Query.ToString());
            return result;
        }

    }
}
