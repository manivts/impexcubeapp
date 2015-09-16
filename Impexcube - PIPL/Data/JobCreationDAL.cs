// -----------------------------------------------------------------------
// <copyright file="JobCreationDAL.cs" company="">
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
    using System.Configuration;
    using System.Data.SqlClient;
    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class JobCreationDAL
    {
        string con = (string)ConfigurationManager.ConnectionStrings["Constr"].ConnectionString;

        public DataSet JobNo()
        {
            DataSet ds = new DataSet();
            try
            {
                string Query = "select * from M_AutoGenerate where keyname='JobNo' ";
                
                SqlConnection sqlConn = new SqlConnection(con);
                sqlConn.Open();
                SqlDataAdapter da = new SqlDataAdapter(Query, sqlConn);

                da.Fill(ds, "JobNo");
                sqlConn.Close();
            }
            catch (Exception ex)
            {
                string Msg = ex.Message.ToString();
            }
            return ds;
        }
        public DataSet Importer(string jobno)
        {
            DataSet ds = new DataSet();
            try
            {
                string Query = "SELECT Importer, IECodeNo, Portofshipment, CountryofOrgin, CountryofShipment FROM T_Importer Where JobNo='" + jobno + "'";

                SqlConnection sqlConn = new SqlConnection(con);
                sqlConn.Open();
                SqlDataAdapter da = new SqlDataAdapter(Query, sqlConn);

                da.Fill(ds, "JobNo");
                sqlConn.Close();
            }
            catch (Exception ex)
            {
                string Msg = ex.Message.ToString();
            }
            return ds;
        }
        public DataSet JobNoList(string JobNo)
        {

            DataSet ds = new DataSet();
            try
            {
                string Query = "SELECT TOP (10) a.JobCreationId as ID, a.JobNo, a.JobReceivedDate, a.Mode, a.Custom, a.BEType,a.BENo,a.BEDate,b.Importer as ImporterName FROM  T_JobCreation a,T_Importer b Where a.JobNo like '%" + JobNo + "%' and a.Jobno = b.Jobno Order By a.JobCreationId DESC  ";

                SqlConnection sqlConn = new SqlConnection(con);
                sqlConn.Open();
                SqlDataAdapter da = new SqlDataAdapter(Query, sqlConn);

                da.Fill(ds, "JobNoList");
                sqlConn.Close();
            }
            catch (Exception ex)
            {
                string Msg = ex.Message.ToString();
            }
            return ds;
        }

        public DataSet SelectJobNo(string JobNo)
        {
            DataSet ds = new DataSet();
            try
            {
                string Query = "select * from T_JobCreation where JobNo='"+JobNo+"' ";

                SqlConnection sqlConn = new SqlConnection(con);
                sqlConn.Open();
                SqlDataAdapter da = new SqlDataAdapter(Query, sqlConn);

                da.Fill(ds, "SelectJobNo");
                sqlConn.Close();
            }
            catch (Exception ex)
            {
                string Msg = ex.Message.ToString();
            }
            return ds;
        }

        public int insert(string Jobno, string JobReceDate, string Mode, string custom, string BEtype, string docfilling, string filling, string BENo, string BEdate, string Fyear, string username, string TotalNoofInvoice, string TotalInvoiceValue, string Currency, bool BondApply, string CustomName, string Chklisttype)
        {
            int result = new int();
            try
            {
                string insertJobNo = "insert into T_JobCreation (JobNo,JobReceivedDate,Mode,Custom,BEType,DocFillingStatus,Filling,BENo,BEDate,FYear,TotalNoofInvoice,TotalInvoiceValue, CreatedBy, CreatedDate, ModifiedBy, ModifiedDate,Currency,BondApply,CustomName, Chklisttype) " +
                       " values ('" + Jobno + "','" + JobReceDate + "','" + Mode + "','" + custom + "','" + BEtype + "','" + docfilling + "','" + filling + "','" + BENo + "','" + BEdate + "','" + Fyear + "','" + TotalNoofInvoice + "','" + TotalInvoiceValue + "','" + username + "','" + DateTime.Now + "','" + username + "','" + DateTime.Now + "','" + Currency + "','" + BondApply + "','" + CustomName + "','"+ Chklisttype+"');" +
                       " insert into T_Importer (JobNo) values ('" + Jobno + "')";
                SqlConnection sqlConn = new SqlConnection(con);
                sqlConn.Open();
                SqlCommand cmd = new SqlCommand(insertJobNo, sqlConn);
                result = cmd.ExecuteNonQuery();
                sqlConn.Close();
            }
            catch
            {
            }
            return result;
        }

        public int Update(string Jobno, string JobReceDate, string Mode, string custom, string BEtype, string docfilling, string filling, string BENo, string BEdate, string TotalNoofInvoice, string TotalInvoiceValue, string Currency, bool BondApply, string jobid, string CustomName, string Chklisttype)
        {

            int result = new int();
            string UpdateJobNo = "";
            //if (BEdate != "")
            //{
            UpdateJobNo = "update T_JobCreation set JobNo='" + Jobno + "',JobReceivedDate='" + JobReceDate + "',Mode='" + Mode + "',Custom='" + custom + "',BEType='" + BEtype + "',DocFillingStatus='" + docfilling + "',Filling='" + filling + "',BENo='" + BENo + "',BEDate='" + BEdate + "',TotalNoofInvoice='" + TotalNoofInvoice + "',TotalInvoiceValue='" + TotalInvoiceValue + "',Currency='" + Currency + "',BondApply='" + BondApply + "', CustomName='" + CustomName + "',Chklisttype='" + Chklisttype + "'  where JobCreationId='" + jobid + "' ";
              
            //}
            //else
            //{
            //    UpdateJobNo = "update T_JobCreation set JobReceivedDate='" + frmdatesplit(JobReceDate) + "',Mode='" + Mode + "',Custom='" + custom + "',BEType='" + BEtype + "',DocFillingStatus='" + docfilling + "',Filling='" + filling + "',BENo='" + BENo + "',TotalNoofInvoice='" + TotalNoofInvoice + "',TotalInvoiceValue='" + TotalInvoiceValue + "',Currency='" + Currency + "'  where JobNo='" + Jobno + "' ";
            //}
            SqlConnection sqlConn = new SqlConnection(con);
            sqlConn.Open();
            SqlCommand cmd = new SqlCommand(UpdateJobNo, sqlConn);
            SqlDataAdapter da = new SqlDataAdapter();

            cmd.CommandText = UpdateJobNo;
            cmd.Connection = sqlConn;
            da.SelectCommand = cmd;
            result = cmd.ExecuteNonQuery();
            sqlConn.Close();
            return result;
        }
        public int UpdateAssable(string Jobno, double AssableValue,double TotInvAmt, double TotDutyAmt)
        {
            //SUM(ProdAmtRs) AS TotalInvAmount, SUM(AssableValue) AS AssableValue, SUM(TotalDutyAmt) AS TotalDutyAmt
            int result = new int();
            string UpdateJobNo = "update T_JobCreation set TotalInvoice='" + TotInvAmt + "', TotalAssVal='" + AssableValue + "', TotalDuty='" + TotDutyAmt + "'  where JobNo='" + Jobno + "'";
            SqlConnection sqlConn = new SqlConnection(con);
            sqlConn.Open();
            SqlCommand cmd = new SqlCommand(UpdateJobNo, sqlConn);
            result = cmd.ExecuteNonQuery();
            sqlConn.Close();
            return result;
        }
        public int updateautono( string keycode)
        {
            int result = new int();
            string updateautono = "update M_AutoGenerate set KeyCode='" + keycode + "' where keyname='JobNo' ";
            SqlConnection sqlConn = new SqlConnection(con);
            sqlConn.Open();
            SqlCommand cmd = new SqlCommand(updateautono, sqlConn);
            SqlDataAdapter da = new SqlDataAdapter();

            cmd.CommandText = updateautono;
            cmd.Connection = sqlConn;
            da.SelectCommand = cmd;
            result = cmd.ExecuteNonQuery();
            sqlConn.Close();
            return result;
        }
        private string frmdatesplit(string Jobdate)
        {
            string[] Jobdate1 = Jobdate.Split('/');
            string Jobdate2 = Jobdate1[1] + '/' + Jobdate1[0] + '/' + Jobdate1[2];
            return Jobdate2;
        }

        public DataSet SelectBondType()
        {
            DataSet ds = new DataSet();
            try
            {
                string Query = "Select BondCode,BondType from M_BondType ";

                SqlConnection sqlConn = new SqlConnection(con);
                sqlConn.Open();
                SqlDataAdapter da = new SqlDataAdapter(Query, sqlConn);

                da.Fill(ds, "SelectBond");
                sqlConn.Close();
            }
            catch (Exception ex)
            {
                string Msg = ex.Message.ToString();
            }
            return ds;
        }

        public DataSet BindCustomHouse(string AirSea)
        {
            DataSet ds = new DataSet();
            try
            {
                string Query = "Select Port, UNECECode from M_HomePort where AirSea='" + AirSea + "' ";

                SqlConnection sqlConn = new SqlConnection(con);
                sqlConn.Open();
                SqlDataAdapter da = new SqlDataAdapter(Query, sqlConn);

                da.Fill(ds, "SelectCustom");
                sqlConn.Close();
            }
            catch (Exception ex)
            {
                string Msg = ex.Message.ToString();
            }
            return ds;
        } 
        public int InsertBondType(string JobNo, string BondType, string BondNumber, string RegisteredDate)
        {
            int result = new int();
            try
            {
                string InsertBond = "insert into T_ImportBondReg (JobNo,BondType,BondNumber,RegisteredDate) " +
                       " values ('" + JobNo + "','" + BondType + "','" + BondNumber + "','" + RegisteredDate + "')";
                SqlConnection sqlConn = new SqlConnection(con);
                sqlConn.Open();
                SqlCommand cmd = new SqlCommand(InsertBond, sqlConn);
                result = cmd.ExecuteNonQuery();
                sqlConn.Close();
            }
            catch
            {
            }
            return result;
        }

        public int UpdateBondType(string Id, string JobNo, string BondType, string BondNumber, string RegisteredDate)
        {
            int result = new int();
            try
            {
                string UpdateBond = "Update T_ImportBondReg Set JobNo ='" + JobNo + "',BondType = '" + BondType + "'," +
                    "BondNumber = '" + BondNumber + "',RegisteredDate = '" + RegisteredDate + "' Where TransId = '" + Id + "'";                       
                SqlConnection sqlConn = new SqlConnection(con);
                sqlConn.Open();
                SqlCommand cmd = new SqlCommand(UpdateBond, sqlConn);
                result = cmd.ExecuteNonQuery();
                sqlConn.Close();
            }
            catch
            {
            }
            return result;
        }

        public DataSet SelectBond(string JobNo)
        {
            DataSet ds = new DataSet();
            try
            {
                string Query = "Select TransId,JobNo,BondType,BondNumber,RegisteredDate from T_ImportBondReg where  JobNo = '" + JobNo + "'";
                SqlConnection sqlConn = new SqlConnection(con);
                sqlConn.Open();
                SqlDataAdapter da = new SqlDataAdapter(Query, sqlConn);

                da.Fill(ds, "data");
                sqlConn.Close();
            }
            catch (Exception ex)
            {
                string Msg = ex.Message.ToString();
            }
            return ds;
        }

        public int InsertBondCertification(string JobNo, string CertificateNo, string CertificateType, string CertificateDate, string Commissionerrate, string Division, string Range)
        {
            int result = new int();
            try
            {
                string InsertCertification = "insert into T_ImportBondCertificate (JobNo,CertificateNo,CertificateType,CertificateDate,Commissionerrate,Division,Range) " +
                       " values ('" + JobNo + "','" + CertificateNo + "','" + CertificateType + "','" + CertificateDate + "','" + Commissionerrate + "'," +
                       " '" + Division + "', '" + Range + "')";
                SqlConnection sqlConn = new SqlConnection(con);
                sqlConn.Open();
                SqlCommand cmd = new SqlCommand(InsertCertification, sqlConn);
                result = cmd.ExecuteNonQuery();
                sqlConn.Close();
            }
            catch
            {
            }
            return result;
        }

        public int UpdateBondCertification(string Id, string JobNo, string CertificateNo, string CertificateType, string CertificateDate, string Commissionerrate, string Division, string Range)
        {
            int result = new int();
            try
            {
                string UpdateCertification = "Update T_ImportBondCertificate Set JobNo ='" + JobNo + "',CertificateNo = '" + CertificateNo + "'," +
                    "CertificateType = '" + CertificateType + "',CertificateDate = '" + CertificateDate + "', Commissionerrate='" + Commissionerrate + "',"+
                    "Division ='" + Division + "', Range='" + Range + "' Where TransId = '" + Id + "'";
                SqlConnection sqlConn = new SqlConnection(con);
                sqlConn.Open();
                SqlCommand cmd = new SqlCommand(UpdateCertification, sqlConn);
                result = cmd.ExecuteNonQuery();
                sqlConn.Close();
            }
            catch
            {
            }
            return result;
        }

        public int DeleteBondCertification(string Id)
        {
            int result = new int();
            try
            {
                string UpdateCertification = "Delete from  T_ImportBondCertificate  Where TransId = '" + Id + "'";
                SqlConnection sqlConn = new SqlConnection(con);
                sqlConn.Open();
                SqlCommand cmd = new SqlCommand(UpdateCertification, sqlConn);
                result = cmd.ExecuteNonQuery();
                sqlConn.Close();
            }
            catch
            {
            }
            return result;
        }
        public DataSet SelectCertification(string JobNo)
        {
            DataSet ds = new DataSet();
            try
            {
                string Query = "Select TransId,JobNo ,CertificateNo,CertificateType,CertificateDate,Commissionerrate,Division,Range from T_ImportBondCertificate where  JobNo = '" + JobNo + "'";
                SqlConnection sqlConn = new SqlConnection(con);
                sqlConn.Open();
                SqlDataAdapter da = new SqlDataAdapter(Query, sqlConn);

                da.Fill(ds, "data");
                sqlConn.Close();
            }
            catch (Exception ex)
            {
                string Msg = ex.Message.ToString();
            }
            return ds;
        }


        public int InsertEXBond(string JobNo,string ExBondFDate, string ExBondTDate, string EXBondJobNo, string EXBondBLNO, string EXBondBLDate, string EXBondNo, string EXBondDate, string EXBondExpiryDate, string EXWareHouse, string ExCode)
        {
            int result = new int();
            try
            {
                string InsertBond = "insert into T_ImportExBondDetails (JobNo, ExBondFDate, ExBondTDate, EXBondJobNo, EXBondBLNO, EXBondBLDate, EXBondNo, EXBondDate, EXBondExpiryDate, EXWareHouse, ExCode) " +
                       " values ('" + JobNo + "','" + ExBondFDate + "','" + ExBondTDate + "','" + EXBondJobNo + "','" + EXBondBLNO + "','" + EXBondBLDate + "','" + EXBondNo + "','" + EXBondDate + "','" + EXBondExpiryDate + "','" + EXWareHouse + "','"+ExCode+"')";
                SqlConnection sqlConn = new SqlConnection(con);
                sqlConn.Open();
                SqlCommand cmd = new SqlCommand(InsertBond, sqlConn);
                result = cmd.ExecuteNonQuery();
                sqlConn.Close();
            }
            catch
            {
            }
            return result;
        }

        public int UpdateEXBond(int TransId,string JobNo,string ExBondFDate, string ExBondTDate, string EXBondJobNo, string EXBondBLNO, string EXBondBLDate, string EXBondNo, string EXBondDate, string EXBondExpiryDate, string EXWareHouse, string ExCode)
        {
            int result = new int();
            try
            {
                string UpdateBond = "Update T_ImportExBondDetails SET  ExBondFDate='" + ExBondFDate + "', ExBondTDate='" + ExBondTDate + "', EXBondJobNo='" + EXBondJobNo + "', EXBondBLNO='" + EXBondBLNO + "', EXBondBLDate='" + EXBondBLDate + "', EXBondNo='" + EXBondNo + "', EXBondDate='" + EXBondDate + "', EXBondExpiryDate='" + EXBondExpiryDate + "', EXWareHouse='" + EXWareHouse + "', ExCode='" + ExCode + "' Where TransId='"+TransId+"'";
                    
                SqlConnection sqlConn = new SqlConnection(con);
                sqlConn.Open();
                SqlCommand cmd = new SqlCommand(UpdateBond, sqlConn);
                result = cmd.ExecuteNonQuery();
                sqlConn.Close();
            }
            catch
            {
            }
            return result;
        }

        public DataSet SelectEXBond(string JobNo)
        {
            DataSet ds = new DataSet();
            try
            {
                string Query = "Select *  from T_ImportExBondDetails where  JobNo = '" + JobNo + "'";
                SqlConnection sqlConn = new SqlConnection(con);
                sqlConn.Open();
                SqlDataAdapter da = new SqlDataAdapter(Query, sqlConn);

                da.Fill(ds, "data");
                sqlConn.Close();
            }
            catch (Exception ex)
            {
                string Msg = ex.Message.ToString();
            }
            return ds;
        }




        public int InbondJobCopy(string OldJobNo,string DuplicateJobNo)
        {
            int result=0;

            SqlConnection conn = new SqlConnection(con);
            conn.Open();
            SqlCommand cmd = new SqlCommand("InbondJobCopy", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add(new SqlParameter("@JobNo", OldJobNo));
            cmd.Parameters.Add(new SqlParameter("@DuplicateJobNo", DuplicateJobNo));
            cmd.ExecuteNonQuery();
            conn.Close();

            //SqlConnection conn = new SqlConnection(con);
            //string qry = "Exec InbondJobCopy '" + DuplicateJobNo + "','" + OldJobNo + "' ";
            //SqlCommand cmd = new SqlCommand(qry, conn);
            //cmd.CommandType = CommandType.StoredProcedure;
            //conn.Open();
            //cmd.ExecuteNonQuery();


            return result;
        }



    }

    
}
