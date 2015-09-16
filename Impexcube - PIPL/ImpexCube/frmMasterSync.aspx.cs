using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using MySql.Data.MySqlClient;
using System.Configuration;
using System.Text;
using System.Data.SqlClient;
using VTS.ImpexCube.Data;

namespace ImpexCube
{
    public partial class frmMasterSync : System.Web.UI.Page
    {
        private readonly string Impstrconn = ConfigurationManager.AppSettings["ConnectionDashboard"];
        private readonly string VIstrconn = ConfigurationManager.AppSettings["ConnectionVisual"];
        CommonDL objCommonDL = new CommonDL();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {

            }
        }
        public DataSet GetDataMy(string Query)
        {
            var ds = new DataSet();
            try
            {
                var con = new MySqlConnection(VIstrconn);
                con.Open();
                var sd = new MySqlDataAdapter(Query, con);
                sd.Fill(ds, "data");
                con.Close();
            }
            catch (Exception ex)
            {
                string Msg = ex.Message;
            }
            return ds;
        }

        protected void btnSync_Click(object sender, EventArgs e)
        {
            int Result = 0;
            string MasterQuery = "SELECT * FROM cnsr_mst c,cnsr_add a where c.Cnsr_CODE=a.Cnsr_CODE ";//and wrkblk='" + (string)Session["WorkBlk"] + "' 
            DataSet ds = GetDataMy(MasterQuery);
            DataTable dt = ds.Tables["data"];
            int i = 0;
            foreach (DataRow row in dt.Rows)
            {
                string KeyName = "CR";
                    string AutoQuery = "select keycode from [M_AutoGenerate] where Keyname ='" + KeyName + "'";
                    int auto = 0;
                    DataSet autods = objCommonDL.GetDataSet(AutoQuery);
                    if (autods.Tables["Table"].Rows.Count != 0)
                    {
                        DataRowView dr = autods.Tables["Table"].DefaultView[0];
                        auto = Convert.ToInt32(dr["keycode"].ToString());
                        auto = auto + 1;
                        //txtAcountCode.Text = keyname + auto.ToString();
                    }
                string AccountCode = "CR" + Convert.ToString(auto);
                string AccountName = string.Empty;
                string ShortName = string.Empty;
                string AccountType = "Consignor";
                string Prefix = "M/S";
                string AccountGroup = "Sundry Debtors";
                string BranchId = string.Empty;
                string Address1 = string.Empty;
                string AddressCode = string.Empty;
                string BranchName = string.Empty;
                string City = string.Empty;
                string State = string.Empty;
                string Pincode = string.Empty;
                string Mobile = string.Empty;
                string EmailID = string.Empty;
                string TINNo = string.Empty;
                string Country = string.Empty;
                string Countrycode = string.Empty;
                string BranchSlNo = string.Empty;
                string IECode = string.Empty;
                string CreatedBy = (string)Session["USER-NAME"];
                string CreatedDate = DateTime.Now.ToString();

                AccountName =  dt.Rows[i]["CNSR_NAME"].ToString();
                ShortName = dt.Rows[i]["CNSR_CODE"].ToString();
                //AccountType = dt.Rows[i]["DOC_RECD"].ToString();
                //Prefix = dt.Rows[i]["DOC_RECD"].ToString();
                BranchId = dt.Rows[i]["ADD_NUM"].ToString();
                Address1 = dt.Rows[i]["ADDRESS"].ToString();
                AddressCode = dt.Rows[i]["ADDR_CODE"].ToString();
                BranchName = dt.Rows[i]["CNSR_NAME"].ToString();
                City = dt.Rows[i]["CITY"].ToString();
                Countrycode = dt.Rows[i]["COUNTRY"].ToString();
                //State = dt.Rows[i]["STATE"].ToString();
                Pincode = dt.Rows[i]["PIN"].ToString();
                Mobile= dt.Rows[i]["TEL_NO"].ToString();
                EmailID = dt.Rows[i]["EMAIL"].ToString();
                //TINNo = dt.Rows[i]["BIN_NO"].ToString();

                string CountryQuery = "Select CountryName From M_Country Where CountryCode = '" + Countrycode + "'";
                DataSet ds5 = objCommonDL.GetDataSet(CountryQuery);
                if (ds5.Tables["Table"].Rows.Count != 0)
                {
                    DataRowView row4 = ds5.Tables["Table"].DefaultView[0];
                    Country = row4["CountryName"].ToString();
                }

                //Country = dt.Rows[i]["DOC_RECD"].ToString();
                //Countrycode = dt.Rows[i]["DOC_RECD"].ToString();
                //IECode = dt.Rows[i]["IE_CODE_NO"].ToString();
                //BranchSlNo = dt.Rows[i]["BranchSNo"].ToString();
                if (BranchSlNo == null)
                {
                    BranchSlNo = "0";
                }

                StringBuilder Query = new StringBuilder();
                string Message = string.Empty;
                try
                {
                    using (SqlConnection con = new SqlConnection(Impstrconn))
                    {

                        con.Open();
                        try
                        {
                            Query.Append("INSERT INTO [M_AccountMaster] (AccountCode,AccountName,Acc_group, ShortName,AccountType, Prefix,BranchId,BranchName,Address1,");
                            Query.Append("City, State, Pincode, Mobile, EmailID,TINNo,Country,");
                            Query.Append("Countrycode,IECode,CreatedBy,CreatedDate)");
                            Query.Append("values(@AccountCode,@AccountName,@Acc_group,@ShortName,@AccountType, @Prefix,@BranchId,@BranchName,@Address1,@City,@State,@Pincode,@Mobile,@EmailID,@TINNo,");
                            Query.Append("@Country,@Countrycode,@IECode,@CreatedBy,@CreatedDate)");
                            SqlCommand cmd = new SqlCommand(Query.ToString(), con);
                            cmd.Parameters.AddWithValue("@AccountCode", "" + AccountCode + "");
                            cmd.Parameters.AddWithValue("@AccountName", "" + AccountName + "");
                            cmd.Parameters.AddWithValue("@Acc_group", "" + AccountGroup + "");
                            cmd.Parameters.AddWithValue("@ShortName", "" + ShortName + "");
                            cmd.Parameters.AddWithValue("@AccountType", "" + AccountType + "");
                            cmd.Parameters.AddWithValue("@Prefix", "" + Prefix + "");
                            cmd.Parameters.AddWithValue("@BranchId", "" + BranchId + "");
                            cmd.Parameters.AddWithValue("@BranchName", "" + BranchName + "");
                            cmd.Parameters.AddWithValue("@Address1", "" + Address1 + "");
                            cmd.Parameters.AddWithValue("@City", "" + City + "");
                            cmd.Parameters.AddWithValue("@State", "" + State + "");
                            cmd.Parameters.AddWithValue("@Pincode", "" + Pincode + "");
                            cmd.Parameters.AddWithValue("@Mobile", "" + Mobile + "");
                            cmd.Parameters.AddWithValue("@EmailID", "" + EmailID + "");
                            cmd.Parameters.AddWithValue("@TINNo", "" + TINNo + "");
                            cmd.Parameters.AddWithValue("@Country", "" + Country + "");
                            cmd.Parameters.AddWithValue("@Countrycode", "" + Countrycode + "");
                            cmd.Parameters.AddWithValue("@IECode", "" + IECode + "");
                            cmd.Parameters.AddWithValue("@CreatedBy", "" + CreatedBy + "");
                            cmd.Parameters.AddWithValue("@CreatedDate", "" + CreatedDate + "");
                            Result = cmd.ExecuteNonQuery();
                        }
                        catch(Exception ex)
                        {
                            string Messagess = ex.Message;
                        }
                        con.Close();
                        if (Result != 0)
                        {
                            SqlConnection con1 = new SqlConnection(Impstrconn);
                            con1.Open();
                            StringBuilder Query1 = new StringBuilder();
                            Query1.Append("INSERT INTO [M_AccountDetails] (AccountCode,AccountName, Acc_group,BranchId, BranchName, Address1,");
                            Query1.Append("City, State, Pincode, Mobile, EmailID,TINNo,Country,");
                            Query1.Append("Countrycode,IECode,CreatedBy,CreatedDate)");
                            Query1.Append("values(@AccountCode,@AccountName,@Acc_group,@BranchId,@BranchName,@Address1,@City,@State,@Pincode,@Mobile,@EmailID,@TINNo,");
                            Query1.Append("@Country,@Countrycode,@IECode,@CreatedBy,@CreatedDate)");
                            SqlCommand cmd1 = new SqlCommand(Query1.ToString(), con1);
                            cmd1.Parameters.AddWithValue("@AccountCode", "" + AccountCode + "");
                            cmd1.Parameters.AddWithValue("@AccountName", "" + AccountName + "");
                            cmd1.Parameters.AddWithValue("@Acc_group", "" + AccountGroup + "");
                            cmd1.Parameters.AddWithValue("@BranchId", "" + BranchSlNo + "");
                            cmd1.Parameters.AddWithValue("@BranchName", "" + BranchName + "");
                            cmd1.Parameters.AddWithValue("@Address1", "" + Address1 + "");
                            cmd1.Parameters.AddWithValue("@City", "" + City + "");
                            cmd1.Parameters.AddWithValue("@State", "" + State + "");
                            cmd1.Parameters.AddWithValue("@Pincode", "" + Pincode + "");
                            cmd1.Parameters.AddWithValue("@Mobile", "" + Mobile + "");
                            cmd1.Parameters.AddWithValue("@EmailID", "" + EmailID + "");
                            cmd1.Parameters.AddWithValue("@TINNo", "" + TINNo + "");
                            cmd1.Parameters.AddWithValue("@Country", "" + Country + "");
                            cmd1.Parameters.AddWithValue("@Countrycode", "" + Countrycode + "");
                            cmd1.Parameters.AddWithValue("@IECode", "" + IECode + "");
                            cmd1.Parameters.AddWithValue("@CreatedBy", "" + CreatedBy + "");
                            cmd1.Parameters.AddWithValue("@CreatedDate", "" + CreatedDate + "");
                            Result = cmd1.ExecuteNonQuery();
                            con1.Close();
                            string CU = "CR";
                            Updatesno(CU, Convert.ToString(auto));
                            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Saved Successfully ');", true);
                        }
                    }
                }
                catch (SqlException ex)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert(' " + ex.Message + " ');", true);
                }
                i++;
            }
        }



        private void Updatesno(string keyname, string keycode)
        {
            string AutoQuery = "update M_AutoGenerate set keycode='" + keycode + "' where Keyname='" + keyname + "'";
            objCommonDL.ExecuteNonQuery(AutoQuery);
        }


    }
}