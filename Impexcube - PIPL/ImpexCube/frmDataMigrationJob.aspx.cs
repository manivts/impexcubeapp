using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Web.UI;
using MySql.Data.MySqlClient;
using Org.BouncyCastle.Math;
using VTS.ImpexCube.Data;
using System.Web.UI.WebControls;

namespace ImpexCube
{
    public partial class frmDataMigrationJob : System.Web.UI.Page
    {
        //string Impstrconn = (string)ConfigurationManager.AppSettings["Constr"];
        private readonly string Impstrconn = ConfigurationManager.AppSettings["ConnectionDashboard"];
        private readonly string VIstrconn = ConfigurationManager.AppSettings["ConnectionVisual"];
        public string WorkBlk;
        private CommonDL objCommonDL = new CommonDL();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                var pagename = (Label)Master.FindControl("lblName");
                pagename.Text = "Import Job Sync";

                if ((string) Session["FinancialYear"] == "2014-2015")
                {
                    WorkBlk = "WP000009";
                }
                else if ((string) Session["FinancialYear"] == "2013-2014")
                {
                    WorkBlk = "WP000008";
                }
                else if ((string) Session["FinancialYear"] == "2012-2013")
                {
                    WorkBlk = "WP000007";
                }
                Session["WorkBlk"] = WorkBlk;
                btnJobCreation.Visible = true;
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
                var Msg = ex.Message;
            }
            return ds;
        }

        protected void btnJobCreation_Click(object sender, EventArgs e)
        {
            var JobNo = "IMP/"+txtJobNo.Text+"/"+ddlFyear.SelectedValue;
            lblJobNo.Text = JobNo;
            Session["JobCon"] = JobNo;
            var CheckJob = "select JOB_NO from iworkreg where Job_No = '" + JobNo + "'";//and wrkblk='" + (string)Session["WorkBlk"] + "' 
            var ds = GetDataMy(CheckJob);
            if (ds.Tables["data"].Rows.Count==0)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alert","alert('Please Check The Job No');", true);
            }
            else
            {
                try
                {
                    var Job =JobNo.Split('/');
                    JobNo = Job[1];
                    var conn = new SqlConnection(Impstrconn);
                    conn.Open();
                    var cmd = new SqlCommand("RemoveJobDetails", conn) {CommandType = CommandType.StoredProcedure};
                    cmd.Parameters.Add(new SqlParameter("@JobNo", JobNo));
                    cmd.ExecuteNonQuery();
                    conn.Close();
                    JobCreation();
                    Shipment();
                    Shipmentcontainer();
                    Invoice();
                    Product();
                }
                catch (Exception ex)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Error in Store Procedure ');", true);
                }
            }
        }

        protected void btnShipment_Click(object sender, EventArgs e)
        {
           
            Shipment();
        }

        protected void btnShipmentCon_Click(object sender, EventArgs e)
        {
            Shipmentcontainer();
        }

        protected void btnInvoice_Click(object sender, EventArgs e)
        {
            Invoice();
        }

        protected void btnProduct_Click(object sender, EventArgs e)
        {
            Product();
        }
        public void insertv(string query)
        {
            var con = new SqlConnection(Impstrconn);
            con.Open();
            var cmd = new SqlCommand("insert into tbl_error(error)values('" + query + "')", con);
            cmd.ExecuteNonQuery();
            con.Close();
        }
        public void JobCreation()
        {
            try
            {
                var Query = new StringBuilder();
                Query.Append("select distinct i.JOB_NO,DATE_FORMAT(i.DOC_RECD,'%d/%m/%Y') as DOC_RECD,i.TOT_ASS_VL,i.TOT_DUTY,");
                Query.Append("i.TRANSPORT_MODE,i.BE_TYPE,im.ADV_POST,p.BE_NO,DATE_FORMAT(p.BE_DATE,'%d/%m/%Y') as BE_DATE,pm.PARTY_CODE,");
                Query.Append("pm.Comp_Type,pm.Party_Name,pm.IE_CODE_NO,pa.ADDRESS,pa.CITY,pa.STATE,pa.PIN,pa.BranchSNo,pa.ADDR_NUM,im.HI_SEA,im.Und_Sec46,");
                Query.Append("im.SEC46_REM,im.FIRST_CHK,im.FCHK_REM,im.GREEN_CHNL,im.GCHNL_REM,im.Kachha_BE,");
                Query.Append("im.KBE_REM,im.UND_SEC48,im.SEC48_REM,i.INV_DTL,i.IMPX_PCODE from iworkreg i,imp_addl im,ijob_pos p,prt_addr pa,prt_mast pm");
                Query.Append(" where i.job_no=im.job_no and i.job_no=p.job_no and i.party_code=pm.party_code");
                Query.Append(" and i.party_code=pa.party_code and i.party_addr=pa.addr_code and i.JOB_NO like '%" + (string)Session["JobCon"] + "%' order by i.job_no asc");//and i.wrkblk='" + (string) Session["WorkBlk"] + "' 
                var ds = GetDataMy(Query.ToString());
                var dt = ds.Tables["data"];
                var i = 0;
                try
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        Session["jobno"] = string.Empty;
                        var Result = 0;
                        var JobNo = string.Empty;
                        var JobReceivedDate = string.Empty;
                        var Mode = string.Empty;
                        var Custom = string.Empty;
                        var BEType = string.Empty;
                        var DocFillingStatus = string.Empty;
                        var Filling = string.Empty;
                        var BENo = string.Empty;
                        var FYear = string.Empty;
                        var BEDate = string.Empty;
                        var JobDate = string.Empty;
                        var Currency = string.Empty;
                        var TotalAssVal = string.Empty;
                        var TotalDuty = string.Empty;
                        var TotalInvoice = 0.00;
                        var TotalNoofInvoice = 0.00;
                        var CreatedBy = (string)Session["User-Name"];
                        var CreatedDate = DateTime.Now.ToString();
                        var ModifiedBy = (string)Session["User-Name"];
                        var ModifiedDate = DateTime.Now.ToString();
                        JobNo = dt.Rows[i]["JOB_NO"].ToString();
                        var Job = JobNo.Split('/');
                        JobNo = Job[1];
                        FYear = Job[2];
                        Session["jobno"] = JobNo;
                        JobReceivedDate = dt.Rows[i]["DOC_RECD"].ToString();
                        Mode = dt.Rows[i]["TRANSPORT_MODE"].ToString();
                        if (Mode == "A")
                        {
                            Mode = "Air";
                        }
                        else if (Mode == "S")
                        {
                            Mode = "Sea";
                        }
                        if (dt.Rows[i]["BE_TYPE"].ToString() == "Home")
                        {
                            BEType = "H";
                        }
                        else if (dt.Rows[i]["BE_TYPE"].ToString() == "In-Bond")
                        {
                            BEType = "W";
                        }
                        else if (dt.Rows[i]["BE_TYPE"].ToString() == "Ex-Bond")
                        {
                            BEType = "X";
                        }
                        else
                        {
                            BEType = "H";
                        }
                        if (dt.Rows[i]["ADV_POST"].ToString() == "Normal")
                        {
                            DocFillingStatus = "N";
                        }
                        else if (dt.Rows[i]["ADV_POST"].ToString() == "Prior")
                        {
                            DocFillingStatus = "Y";
                        }
                        else if (dt.Rows[i]["ADV_POST"].ToString() == "Advance")
                        {
                            DocFillingStatus = "A";
                        }
                        else
                        {
                            DocFillingStatus = "N";
                        }

                        BENo = dt.Rows[i]["BE_NO"].ToString();
                        BEDate = dt.Rows[i]["BE_DATE"].ToString();
                        Filling = "Online";
                        var InvValueQuery = "select sum(INV_VALUE) as INVALUE from Iinv_dtl where JOB_NO ='" + (string)Session["JobCon"] + "'";
                        var dsInv = GetDataMy(InvValueQuery);
                        //if (dsInv.Tables["data"].Rows.Count != 0)
                        //{
                        //    DataRowView row1 = dsInv.Tables["data"].DefaultView[0];
                        //    if (dt.Rows[i]["INVALUE"] != DBNull.Value)
                        //    {
                        //        TotalInvoice = Convert.ToDouble(row1["INVALUE"].ToString());
                        //    }
                        //    else
                        //    {
                        //        TotalInvoice = 0.00;
                        //    }
                        //}

                        TotalAssVal = dt.Rows[i]["TOT_ASS_VL"].ToString();
                        TotalDuty = dt.Rows[i]["TOT_DUTY"].ToString();

                        //Invoice Count
                        var NoOfInv = "select  COUNT(*) as TotNoOfInv  from Iinv_dtl where JOB_NO ='" + (string)Session["JobCon"] + "'";
                        var dsNoInv = GetDataMy(NoOfInv);
                        //if (dsNoInv.Tables["data"].Rows.Count != 0)
                        //{
                        //    DataRowView row1 = dsNoInv.Tables["data"].DefaultView[0];
                        //    if (row1["TotNoOfInv"].ToString() != "0")
                        //    {
                        //        TotalNoofInvoice = Convert.ToDouble(row1["TotNoOfInv"].ToString());
                        //    }
                        //}

                        var CustomName = string.Empty;
                        var CustomQry = "select  UNECE_CODE,PORT  from homeport where PORT_ID ='" + dt.Rows[i]["IMPX_PCODE"].ToString() + "'";
                        var dsCustom = GetDataMy(CustomQry);
                        if (dsCustom.Tables["data"].Rows.Count != 0)
                        {
                            var rowCustom = dsCustom.Tables["data"].DefaultView[0];
                            Custom = rowCustom["UNECE_CODE"].ToString();
                            CustomName = rowCustom["PORT"].ToString();
                        }
                        var Chklisttype = string.Empty;
                        if (BEType == "H")
                        {
                            Chklisttype = "Check List-BILL OF ENTRY FOR HOME CONSUMPTION";
                        }
                        else if (BEType == "W")
                        {
                            Chklisttype = "CheckList - BILL OF ENTRY FOR WAREHOUSING";
                        }
                        else if (BEType == "X")
                        {
                            Chklisttype = "CheckList - BILL OF ENTRY FOR EX BOND CLEARANCE";
                        }
                        var IMPQuery = new StringBuilder();
                        
                        IMPQuery.Append(
                            "Insert into T_JobCreation(JobNo,JobReceivedDate,Mode,BEType,FYear,DocFillingStatus,Filling,BENo,BEDate,Currency,TotalNoofInvoice,TotalAssVal,TotalDuty,TotalInvoice,TotalInvoiceValue,CreatedBy,CreatedDate,ModifiedBy,ModifiedDate,Custom,CustomName,Chklisttype)");
                        IMPQuery.Append(
                            " Values(@JobNo,@JobReceivedDate,@Mode,@BEType,@FYear,@DocFillingStatus,@Filling,@BENo,@BEDate,@Currency,@TotalNoofInvoice,@TotalAssVal,@TotalDuty,@TotalInvoice,@TotalInvoiceValue,@CreatedBy,@CreatedDate,@ModifiedBy,@ModifiedDate,@Custom,@CustomName,@Chklisttype)");

                        var conn = new SqlConnection(Impstrconn);
                        conn.Open();
                        var da = new SqlDataAdapter();
                        var cmd = new SqlCommand(IMPQuery.ToString(), conn);

                        cmd.Parameters.AddWithValue("@JobNo", JobNo);
                        cmd.Parameters.AddWithValue("@JobReceivedDate", JobReceivedDate);
                        cmd.Parameters.AddWithValue("@Mode", Mode);
                        cmd.Parameters.AddWithValue("@Custom", Custom.Trim());
                        cmd.Parameters.AddWithValue("@BEType", BEType);
                        cmd.Parameters.AddWithValue("@FYear", FYear);
                        cmd.Parameters.AddWithValue("@DocFillingStatus", DocFillingStatus);
                        cmd.Parameters.AddWithValue("@Filling", Filling);
                        cmd.Parameters.AddWithValue("@BENo", BENo);
                        cmd.Parameters.AddWithValue("@BEDate", BEDate);
                        cmd.Parameters.AddWithValue("@Currency", "~Select~");
                        cmd.Parameters.AddWithValue("@TotalNoofInvoice", TotalNoofInvoice);
                        cmd.Parameters.AddWithValue("@TotalAssVal", TotalAssVal);
                        cmd.Parameters.AddWithValue("@TotalDuty", TotalDuty);
                        cmd.Parameters.AddWithValue("@TotalInvoice", TotalInvoice);
                        cmd.Parameters.AddWithValue("@TotalInvoiceValue", TotalInvoice);
                        cmd.Parameters.AddWithValue("@CreatedBy", CreatedBy);
                        cmd.Parameters.AddWithValue("@CreatedDate", CreatedDate);
                        cmd.Parameters.AddWithValue("@ModifiedBy", CreatedBy);
                        cmd.Parameters.AddWithValue("@ModifiedDate", ModifiedDate);
                        cmd.Parameters.AddWithValue("@CustomName", CustomName);
                        cmd.Parameters.AddWithValue("@Chklisttype", Chklisttype);
                        try
                        {
                            Result = cmd.ExecuteNonQuery();
                            conn.Close();
                        }
                        catch (Exception ex)
                        {
                            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Error in Job Creation  "+ ex.Message+ "' ); ", true);
                        }

                        var ImporterCode = string.Empty;
                        var Importer = string.Empty;
                        var Consignor = string.Empty;
                        var ConsignorShName = string.Empty;
                        var ConsignorAddress = string.Empty;
                        var ConsignorCountry = string.Empty;
                        var ConsignorCity = string.Empty;
                        var SellerName = string.Empty;
                        var HighSeaSale = true;
                        var ChkUnderSec46 = string.Empty;
                        var underSec46 = string.Empty;
                        var ChkFirstChk = string.Empty;
                        var FirstChk = string.Empty;
                        var ChkGreen = string.Empty;
                        var GreenChannel = string.Empty;
                        var ChkKachha = string.Empty;
                        var Kachha = string.Empty;
                        var ChkUnderSec48 = string.Empty;
                        var UnderSec48 = string.Empty;

                        JobNo = Job[1];
                        Importer = dt.Rows[i]["Party_Name"].ToString();
                        var ImporterType = dt.Rows[i]["COMP_TYPE"].ToString();
                        var IECodeNo = dt.Rows[i]["IE_CODE_NO"].ToString();
                        var ImpShortName = dt.Rows[i]["PARTY_CODE"].ToString();
                        var BranchSno = dt.Rows[i]["ADDR_NUM"].ToString();
                        var Address = dt.Rows[i]["ADDRESS"].ToString();
                        var City = dt.Rows[i]["CITY"].ToString();
                        var State = dt.Rows[i]["STATE"].ToString();
                        var ZipCode = dt.Rows[i]["PIN"].ToString();
                        var BEHeading = dt.Rows[i]["INV_DTL"].ToString();
                        ChkUnderSec46 = dt.Rows[i]["Und_Sec46"].ToString();
                        if (ChkUnderSec46 == "-1")
                        {
                            ChkUnderSec46 = "Yes";
                        }
                        else
                        {
                            ChkUnderSec46 = "No";
                        }
                        underSec46 = dt.Rows[i]["SEC46_REM"].ToString();
                        ChkFirstChk = dt.Rows[i]["FIRST_CHK"].ToString();
                        ChkFirstChk = ChkFirstChk == "-1" ? "Yes" : "No";
                        FirstChk = dt.Rows[i]["FCHK_REM"].ToString();
                        ChkGreen = dt.Rows[i]["GREEN_CHNL"].ToString();
                        ChkGreen = ChkGreen == "-1" ? "Yes" : "No";
                        GreenChannel = dt.Rows[i]["GCHNL_REM"].ToString();
                        ChkKachha = dt.Rows[i]["Kachha_BE"].ToString();
                        ChkKachha = ChkKachha == "-1" ? "Yes" : "No";
                        Kachha = dt.Rows[i]["KBE_REM"].ToString();
                        ChkUnderSec48 = dt.Rows[i]["UND_SEC48"].ToString();
                        ChkUnderSec48 = ChkUnderSec48 == "-1" ? "Yes" : "No";
                        UnderSec48 = dt.Rows[i]["SEC48_REM"].ToString();
                        HighSeaSale = Convert.ToBoolean(dt.Rows[i]["HI_SEA"]);
                        var BEHeading1 = string.Empty;
                        if (!string.IsNullOrEmpty(BEHeading) && BEHeading.Length > 5)
                        {
                            BEHeading1 = BEHeading.Substring(0, 4);
                        }
                        else
                        {
                            BEHeading1 = "";
                        }
                        
                        var IMPQuery1 = new StringBuilder();

                        IMPQuery1.Append(
                            "Insert into T_Importer(JobNo,Importer,ImporterType,IECodeNo,ImpShortName,BranchSno,Address,City,State,ZipCode,BEHeading,");
                        IMPQuery1.Append(
                            "ChkUnderSec46,underSec46,ChkFirstChk,FirstChk,ChkGreen,GreenChannel,ChkKachha,Kachha,ChkUnderSec48,UnderSec48,HighSeaSale,CreatedBy,CreatedDate,ModifiedDate)");

                        IMPQuery1.Append(
                            "Values (@JobNo,@Importer,@ImporterType,@IECodeNo,@ImpShortName,@BranchSno,@Address,@City,@State,@ZipCode,@BEHeading,");
                        IMPQuery1.Append(
                            "@ChkUnderSec46,@underSec46,@ChkFirstChk,@FirstChk,@ChkGreen,@GreenChannel,@ChkKachha,@Kachha,@ChkUnderSec48,@UnderSec48,@HighSeaSale,@CreatedBy,@CreatedDate,@ModifiedDate)");

                        var conn1 = new SqlConnection(Impstrconn);
                        conn1.Open();
                        var da1 = new SqlDataAdapter();
                        var cmd1 = new SqlCommand(IMPQuery1.ToString(), conn1);

                        cmd1.Parameters.AddWithValue("@JobNo", JobNo);
                        cmd1.Parameters.AddWithValue("@Importer", Importer.Replace("'", ""));
                        cmd1.Parameters.AddWithValue("@ImporterType", ImporterType);
                        cmd1.Parameters.AddWithValue("@IECodeNo", IECodeNo);
                        cmd1.Parameters.AddWithValue("@ImpShortName", ImpShortName);
                        cmd1.Parameters.AddWithValue("@BranchSno", BranchSno);
                        cmd1.Parameters.AddWithValue("@Address", Address.Replace("'", ""));
                        cmd1.Parameters.AddWithValue("@City", City);
                        cmd1.Parameters.AddWithValue("@State", State);
                        cmd1.Parameters.AddWithValue("@ZipCode", ZipCode);
                        cmd1.Parameters.AddWithValue("@BEHeading", BEHeading1);
                        cmd1.Parameters.AddWithValue("@HighSeaSale", HighSeaSale);
                        cmd1.Parameters.AddWithValue("@ChkUnderSec46", ChkUnderSec46);
                        cmd1.Parameters.AddWithValue("@underSec46", underSec46);
                        cmd1.Parameters.AddWithValue("@ChkFirstChk", ChkFirstChk);
                        cmd1.Parameters.AddWithValue("@FirstChk", FirstChk);
                        cmd1.Parameters.AddWithValue("@ChkGreen", ChkGreen);
                        cmd1.Parameters.AddWithValue("@GreenChannel", GreenChannel);
                        cmd1.Parameters.AddWithValue("@ChkKachha", ChkKachha);
                        cmd1.Parameters.AddWithValue("@Kachha", Kachha);
                        cmd1.Parameters.AddWithValue("@ChkUnderSec48", ChkUnderSec48);
                        cmd1.Parameters.AddWithValue("@UnderSec48", UnderSec48);
                        cmd1.Parameters.AddWithValue("@CreatedBy", CreatedBy);
                        cmd1.Parameters.AddWithValue("@CreatedDate", CreatedDate);
                        cmd1.Parameters.AddWithValue("@ModifiedDate", ModifiedDate);
                        try
                        {
                            cmd1.ExecuteNonQuery();
                            conn1.Close();
                        }
                        catch (Exception ex)
                        {
                            ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Error in Importer '" + ex.Message+ ");", true);
                        }

                        var query1 =
                            "Select Job_No,CNSR_NAME,CNSR_CODE,CNSR_CNTRY,SELLER_NAME FROM iinv_dtl  where job_no LIKE  '%" + (string)Session["JobCon"] + "%'";//and wrkblk='" + (string) Session["WorkBlk"] + "' 
                        var ds1 = GetDataMy(query1);
                        if (ds1.Tables["data"].Rows.Count != 0)
                        {
                            var row1 = ds1.Tables["data"].DefaultView[0];
                            ConsignorShName = row1["CNSR_CODE"].ToString();
                            Consignor = row1["CNSR_NAME"].ToString();
                        }
                      
                        if ((ConsignorShName == ""))
                        {
                            if ((Consignor != ""))
                            {
                                var Cons = Consignor.Split(',');
                                if (Cons.Length != 0)
                                {
                                    Consignor = Cons[0];
                                    ConsignorAddress = Cons[1] + Cons[2];
                                }
                            }
                            const string UpdateCons = "Update T_Importer set Consignor=@Consignor,ConsignorShName=@ConsignorShName,ConsignorAddress=@ConsignorAddress,ConsignorCity=@ConsignorCity,ConsignorCountry=@ConsignorCountry WHERE JobNo=@JobNo ";

                            var conn2 = new SqlConnection(Impstrconn);
                            conn2.Open();
                            var da2 = new SqlDataAdapter();
                            var cmd2 = new SqlCommand(UpdateCons, conn2);
                            cmd2.Parameters.AddWithValue("@JobNo", JobNo);
                            cmd2.Parameters.AddWithValue("@Consignor", Consignor);
                            cmd2.Parameters.AddWithValue("@ConsignorShName", ConsignorShName);
                            cmd2.Parameters.AddWithValue("@ConsignorCountry", ConsignorCountry);
                            cmd2.Parameters.AddWithValue("@ConsignorAddress", ConsignorAddress);
                            cmd2.Parameters.AddWithValue("@ConsignorCity", ConsignorCity);
                            Result = cmd2.ExecuteNonQuery();
                            if (Result == 1)
                            {
                                ScriptManager.RegisterStartupScript(this, GetType(), "alert",
                                    "alert('Saved Successfully ');", true);
                                btnJobCreation.Visible = true;
                                //btnShipment.Visible = true;
                                //btnShipmentCon.Visible = true;
                                //btnInvoice.Visible = true;
                                //btnProduct.Visible = true;
                            }
                            conn2.Close();
                        }

                        var ConsQuery =
                            "select ca.ADD_NUM,ca.Address,ca.CITY,ca.COUNTRY,ca.PIN,cm.cnsr_code ,cm.cnsr_name from cnsr_add ca,cnsr_mst cm where ca.CNSR_CODE = cm.CNSR_CODE and cm.CNSR_CODE ='" + ConsignorShName + "'";
                        var ds3 = GetDataMy(ConsQuery);
                        if (ds3.Tables["data"].Rows.Count != 0)
                        {
                            var row2 = ds3.Tables["data"].DefaultView[0];
                            Consignor = row2["cnsr_name"].ToString();
                            ConsignorShName = row2["CNSR_CODE"].ToString();
                            ConsignorAddress = row2["Address"].ToString();
                            ConsignorCity = row2["CITY"].ToString();
                            ConsignorCountry = row2["COUNTRY"].ToString();

                            if (ConsignorCountry.Length == 2)
                            {
                                var CountryQuery = "Select CountryName From M_Country Where CountryCode = '" + ConsignorCountry + "'";
                                var ds5 = objCommonDL.GetDataSet(CountryQuery);
                                if (ds5.Tables["Table"].Rows.Count != 0)
                                {
                                    var row4 = ds5.Tables["Table"].DefaultView[0];
                                    ConsignorCountry = row4["CountryName"].ToString();
                                }
                            }

                            var UpdateCons =
                                "Update T_Importer set Consignor=@Consignor,ConsignorShName=@ConsignorShName,ConsignorAddress=@ConsignorAddress,ConsignorCity=@ConsignorCity,ConsignorCountry=@ConsignorCountry WHERE JobNo=@JobNo ";

                            var conn2 = new SqlConnection(Impstrconn);
                            conn2.Open();
                            var da2 = new SqlDataAdapter();
                            var cmd2 = new SqlCommand(UpdateCons, conn2);
                            cmd2.Parameters.AddWithValue("@JobNo", JobNo);
                            cmd2.Parameters.AddWithValue("@Consignor", Consignor);
                            cmd2.Parameters.AddWithValue("@ConsignorShName", ConsignorShName);
                            cmd2.Parameters.AddWithValue("@ConsignorCountry", ConsignorCountry);
                            cmd2.Parameters.AddWithValue("@ConsignorAddress", ConsignorAddress);
                            cmd2.Parameters.AddWithValue("@ConsignorCity", ConsignorCity);
                            Result = cmd2.ExecuteNonQuery();
                            if (Result == 1)
                            {
                                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Saved Successfully ');", true);
                                btnJobCreation.Visible = true;
                                //btnShipment.Visible = true;
                                //btnShipmentCon.Visible = true;
                                //btnInvoice.Visible = true;
                                //btnProduct.Visible = true;
                            }
                            conn2.Close();
                        }
                        i++;
                    }
                }
                catch (Exception ex)
                {
                    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Error in Job Creation " + ex.Message + "');", true);
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Error in Job Creation " + ex.Message + "');", true);
            }
        }

        public void Shipment()
        {
            try
            {
                var Query = new StringBuilder();

                Query.Append("SELECT  i.JOB_NO,i.VESSEL_NAME,i.VOYAGE_NO,i.TR_VESSEL,DATE_FORMAT(i.ETA,'%d/%m/%Y') as  ETA, DATE_FORMAT(GLD,'%d/%m/%Y') as GLD,i.Carrier,");
                Query.Append("i.GATEWAY_IGMNO,DATE_FORMAT(i.GATEWAY_IGMDate,'%d/%m/%Y') as GATEWAY_IGMDate,i.MAWB_NO,DATE_FORMAT(i.MAWB_DATE,'%d/%m/%Y') as MAWB_DATE,i.HAWB_NO,DATE_FORMAT(i.HAWB_DATE,'%d/%m/%Y') as HAWB_DATE,");
                Query.Append("i.LINE_NO,i.PORT_REPT,i.GROSS_WT,i.GROSS_UNIT,i.NO_OF_PKG,i.PKG_UNIT,i.STC,i.STC_UNIT,i.STC2,i.STC2_UNIT,");
                Query.Append("DATE_FORMAT(j.DOC_RECD,'%d/%m/%Y') as DOC_RECD,j.SPORT_CODE,j.UNECE_SPORT_CODE,j.CONT_OF_SH,i.ROTN_NO,DATE_FORMAT(i.ROTN_DATE,'%d/%m/%Y') as  ROTN_DATE,");
                Query.Append("j.PORT_OF_SH,j.CONT_ORIG,j.ActNoOfPkgs,j.ActNoOfPkgUnit,j.WARE_HOUSE,j.MARKS,j.wrkblk FROM ishp_dtl i INNER JOIN IWORKREG as j on i.JOB_NO = j.JOB_NO");
                Query.Append(" where   i.JOB_NO like '" + (string)Session["JobCon"] + "%' order by i.Job_No asc;");//j.wrkblk='"+(string)Session["WorkBlk"] + "' and

                var ds = GetDataMy(Query.ToString());
                var dt = ds.Tables["data"];

                var i = 0;
                foreach (DataRow row in dt.Rows)
                {
                    try
                    {
                        Session["jobno"] = string.Empty;
                        var JobNo = string.Empty;
                        var JobDate = string.Empty;
                        var ShipmentPortCode = string.Empty;
                        var ShipmentUneceCode = string.Empty;
                        var CountryOriginCode = string.Empty;
                        var ShipmentCountry = string.Empty;
                        var ShipmentPort = string.Empty;
                        var PortOfOrigin = string.Empty;
                        var CountryOrigin = string.Empty;
                        var VesselName = string.Empty;
                        var VoyageNo = string.Empty;
                        var TransitVessel = string.Empty;
                        var ETA = string.Empty;
                        var GLDInwardDate = string.Empty;
                        var ShippingLine = string.Empty;
                        var LocalIGMNo = string.Empty;
                        var LocalIGMDate = string.Empty;
                        var MasterBLNo = string.Empty;
                        var MasterBLDate = string.Empty;
                        var HouseBLNo = string.Empty;
                        var HouseBLDate = string.Empty;
                        var GatewayIGMNo = string.Empty;
                        var GatewayIGMDate = string.Empty;
                        var ShipLineNo = string.Empty;
                        var ReportingPort = string.Empty;
                        var Container20Feet = "0";
                        var Total20FeetContainer = "0";
                        var Container40Feet = "0";
                        var Total40FeetContainer = "0";
                        var GrossWeight = string.Empty;
                        var GrossWeightUnit = string.Empty;
                        var NetWeight = string.Empty;
                        var NetUint = string.Empty;
                        var NoOfPackages = string.Empty;
                        var PackagesUnit = string.Empty;
                        var STC = string.Empty;
                        var STCUnit = string.Empty;
                        var STC1 = string.Empty;
                        var STCUnit1 = string.Empty;
                        var CFSName = string.Empty;
                        var AgentName = string.Empty;
                        var FFName = string.Empty;
                        var GIGMNO = string.Empty;
                        var GIGMDATE = string.Empty;
                        var MarksNos = string.Empty;
                        var CreatedBy = (string)Session["User-Name"];
                        var CreatedDate = DateTime.Now.ToString();
                        //string CreatedBy = "VTSADMIN";
                        //string CreatedDate = DateTime.Now.ToString();
                        var ModifiedDate = DateTime.Now.ToString();


                        JobNo = dt.Rows[i]["JOB_NO"].ToString();
                        var Job = JobNo.Split('/');
                        JobNo = Job[1];
                        Session["jobno"] = JobNo;
                        JobDate = dt.Rows[i]["DOC_RECD"].ToString();
                        //ShipmentCountryCode = dt.Rows[i]["JOB_NO"].ToString();
                        ShipmentPortCode = dt.Rows[i]["SPORT_CODE"].ToString();
                        ShipmentUneceCode = dt.Rows[i]["UNECE_SPORT_CODE"].ToString();
                        ShipmentCountry = dt.Rows[i]["CONT_OF_SH"].ToString();
                        ShipmentPort = dt.Rows[i]["PORT_OF_SH"].ToString();
                        CountryOrigin = dt.Rows[i]["CONT_ORIG"].ToString();

                        var CountryQuery = "select countrycode from M_Country where CountryName ='" + CountryOrigin + "'";
                        var Countryds = objCommonDL.GetDataSet(CountryQuery);
                        if (Countryds.Tables["Table"].Rows.Count != 0)
                        {
                            var row1 = Countryds.Tables["Table"].DefaultView[0];
                            CountryOriginCode = row1["CountryCode"].ToString();
                        }

                        //PortOfOrigin = dt.Rows[i]["JOB_NO"].ToString();                 
                        VesselName = dt.Rows[i]["VESSEL_NAME"].ToString();
                        VoyageNo = dt.Rows[i]["VOYAGE_NO"].ToString();
                        TransitVessel = dt.Rows[i]["TR_VESSEL"].ToString();
                        ETA = dt.Rows[i]["ETA"].ToString();
                        GLDInwardDate = dt.Rows[i]["GLD"].ToString();
                        ShippingLine = dt.Rows[i]["Carrier"].ToString();
                        //LocalIGMNo = dt.Rows[i]["JOB_NO"].ToString();
                        //LocalIGMDate = dt.Rows[i]["JOB_NO"].ToString();
                        MasterBLNo = dt.Rows[i]["MAWB_NO"].ToString();
                        MasterBLDate = dt.Rows[i]["MAWB_DATE"].ToString();
                        HouseBLNo = dt.Rows[i]["HAWB_NO"].ToString();
                        HouseBLDate = dt.Rows[i]["HAWB_DATE"].ToString();
                        GatewayIGMNo = dt.Rows[i]["ROTN_NO"].ToString();
                        GatewayIGMDate = dt.Rows[i]["ROTN_DATE"].ToString();

                        GIGMNO = dt.Rows[i]["GateWay_IGMNo"].ToString();
                        GIGMDATE = dt.Rows[i]["Gateway_IGMDate"].ToString();

                        ShipLineNo = dt.Rows[i]["LINE_NO"].ToString();
                        ReportingPort = dt.Rows[i]["PORT_REPT"].ToString();
                        //Container20Feet = dt.Rows[i]["JOB_NO"].ToString();
                        //Total20FeetContainer = dt.Rows[i]["JOB_NO"].ToString();
                        //Container40Feet = dt.Rows[i]["JOB_NO"].ToString();
                        //Total40FeetContainer = dt.Rows[i]["JOB_NO"].ToString();
                        GrossWeight = dt.Rows[i]["GROSS_WT"].ToString();
                        GrossWeightUnit = dt.Rows[i]["GROSS_UNIT"].ToString();
                        NetWeight = "";//dt.Rows[i]["ActNoOfPkgs"].ToString();
                        NetUint = "";// dt.Rows[i]["ActNoOfPkgUnit"].ToString();
                        NoOfPackages = dt.Rows[i]["NO_OF_PKG"].ToString();
                        PackagesUnit = dt.Rows[i]["PKG_UNIT"].ToString();
                        STC = dt.Rows[i]["STC"].ToString();
                        STCUnit = dt.Rows[i]["STC_UNIT"] != DBNull.Value ? dt.Rows[i]["STC_UNIT"].ToString() : "~Select~";
                        STC1 = dt.Rows[i]["STC2"].ToString();
                        STCUnit1 = dt.Rows[i]["STC2_Unit"] != DBNull.Value ? dt.Rows[i]["STC2_UNIT"].ToString() : "~Select~";
                        CFSName = dt.Rows[i]["WARE_HOUSE"].ToString();
                        //AgentName = dt.Rows[i]["JOB_NO"].ToString();
                        //FFName = dt.Rows[i]["JOB_NO"].ToString();
                        MarksNos = dt.Rows[i]["MARKS"].ToString();
                        //CreatedBy = dt.Rows[i]["JOB_NO"].ToString();                 
                        //CreatedDate = dt.Rows[i]["JOB_NO"].ToString();
                        //ModifiedBy = dt.Rows[i]["JOB_NO"].ToString();
                        //ModifiedDate = dt.Rows[i]["JOB_NO"].ToString();                 

                        var IMPQuery = new StringBuilder();

                        IMPQuery.Append(
                            "Insert into T_ShipmentDetails(JobNo, JobDate, ShipmentCountryCode, ShipmentPortCode,ShipmentUneceCode, CountryOriginCode, ShipmentCountry, ShipmentPort, CountryOrigin, PortOfOrigin, ");
                        IMPQuery.Append(
                            "VesselName, VoyageNo, TransitVessel, ETA, GLDInwardDate, ShippingLine, LocalIGMNo, LocalIGMDate, MasterBLNo, MasterBLDate, HouseBLNo, HouseBLDate, ");
                        IMPQuery.Append("GatewayIGMNo, GatewayIGMDate, ShipLineNo, ReportingPort,  GrossWeight, ");
                        IMPQuery.Append(
                            "GrossWeightUnit, NetWeight, NetUint, NoOfPackages, PackagesUnit, STC, STCUnit, STC1, STCUnit1, CFSName, AgentName, FFName, MarksNos,Container20Feet,Container40Feet, CreatedBy, ");
                        IMPQuery.Append("CreatedDate, ModifiedBy, ModifiedDate)");


                        IMPQuery.Append(
                            " Values (@JobNo, @JobDate, @ShipmentCountryCode, @ShipmentPortCode,@ShipmentUneceCode, @CountryOriginCode, @ShipmentCountry, @ShipmentPort, @CountryOrigin, @PortOfOrigin, ");
                        IMPQuery.Append(
                            "@VesselName, @VoyageNo, @TransitVessel, @ETA, @GLDInwardDate, @ShippingLine, @LocalIGMNo, @LocalIGMDate, @MasterBLNo, @MasterBLDate, @HouseBLNo, @HouseBLDate, ");
                        IMPQuery.Append("@GatewayIGMNo, @GatewayIGMDate, @ShipLineNo, @ReportingPort,@GrossWeight, ");
                        IMPQuery.Append(
                            "@GrossWeightUnit, @NetWeight, @NetUint, @NoOfPackages, @PackagesUnit, @STC, @STCUnit, @STC1, @STCUnit1, @CFSName, @AgentName, @FFName, @MarksNos,@Container20Feet,@Container40Feet, @CreatedBy, ");
                        IMPQuery.Append("@CreatedDate, @ModifiedBy, @ModifiedDate)");

                        var conn = new SqlConnection(Impstrconn);
                        conn.Open();
                        var da = new SqlDataAdapter();
                        var cmd = new SqlCommand(IMPQuery.ToString(), conn);


                        cmd.Parameters.AddWithValue("@JobNo", JobNo);
                        cmd.Parameters.AddWithValue("@JobDate", JobDate);
                        cmd.Parameters.AddWithValue("@ShipmentCountryCode", CountryOriginCode);
                        cmd.Parameters.AddWithValue("@ShipmentPortCode", ShipmentPortCode);
                        cmd.Parameters.AddWithValue("@ShipmentUneceCode", ShipmentUneceCode);
                        cmd.Parameters.AddWithValue("@CountryOriginCode", CountryOriginCode);
                        cmd.Parameters.AddWithValue("@ShipmentCountry", ShipmentCountry);
                        cmd.Parameters.AddWithValue("@ShipmentPort", ShipmentPort);
                        cmd.Parameters.AddWithValue("@CountryOrigin", CountryOrigin);
                        cmd.Parameters.AddWithValue("@PortOfOrigin", PortOfOrigin);
                        cmd.Parameters.AddWithValue("@VesselName", VesselName);
                        cmd.Parameters.AddWithValue("@VoyageNo", VoyageNo);
                        cmd.Parameters.AddWithValue("@TransitVessel", TransitVessel);
                        cmd.Parameters.AddWithValue("@ETA", ETA);
                        cmd.Parameters.AddWithValue("@GLDInwardDate", GLDInwardDate);
                        cmd.Parameters.AddWithValue("@ShippingLine", ShippingLine);
                        cmd.Parameters.AddWithValue("@LocalIGMNo", GatewayIGMNo);
                        cmd.Parameters.AddWithValue("@LocalIGMDate", GatewayIGMDate);
                        cmd.Parameters.AddWithValue("@MasterBLNo", MasterBLNo);
                        cmd.Parameters.AddWithValue("@MasterBLDate", MasterBLDate);
                        cmd.Parameters.AddWithValue("@HouseBLNo", HouseBLNo);
                        cmd.Parameters.AddWithValue("@HouseBLDate", HouseBLDate);
                        cmd.Parameters.AddWithValue("@GatewayIGMNo", GIGMNO);
                        cmd.Parameters.AddWithValue("@GatewayIGMDate", GIGMDATE);
                        cmd.Parameters.AddWithValue("@ShipLineNo", ShipLineNo);
                        cmd.Parameters.AddWithValue("@ReportingPort", ReportingPort);
                        cmd.Parameters.AddWithValue("@Container20Feet", Container20Feet);
                        //cmd.Parameters.AddWithValue("@Total20FeetContainer", Total20FeetContainer);
                        cmd.Parameters.AddWithValue("@Container40Feet", Container40Feet);
                        //cmd.Parameters.AddWithValue("@Total40FeetContainer", Total40FeetContainer);
                        cmd.Parameters.AddWithValue("@GrossWeight", GrossWeight);
                        cmd.Parameters.AddWithValue("@GrossWeightUnit", GrossWeightUnit);
                        cmd.Parameters.AddWithValue("@NetWeight", NetWeight);
                        cmd.Parameters.AddWithValue("@NetUint", NetUint);
                        cmd.Parameters.AddWithValue("@NoOfPackages", NoOfPackages);
                        cmd.Parameters.AddWithValue("@PackagesUnit", PackagesUnit);
                        cmd.Parameters.AddWithValue("@STC", STC);
                        cmd.Parameters.AddWithValue("@STCUnit", STCUnit);
                        cmd.Parameters.AddWithValue("@STC1", STC1);
                        cmd.Parameters.AddWithValue("@STCUnit1", STCUnit1);
                        cmd.Parameters.AddWithValue("@CFSName", CFSName);
                        cmd.Parameters.AddWithValue("@AgentName", AgentName);
                        cmd.Parameters.AddWithValue("@FFName", FFName);
                        cmd.Parameters.AddWithValue("@MarksNos", MarksNos);
                        cmd.Parameters.AddWithValue("@CreatedBy", CreatedBy);
                        cmd.Parameters.AddWithValue("@CreatedDate", CreatedDate);
                        cmd.Parameters.AddWithValue("@ModifiedBy", CreatedBy);
                        cmd.Parameters.AddWithValue("@ModifiedDate", ModifiedDate);

                        var Result = cmd.ExecuteNonQuery();
                        if (Result == 1)
                        {
                            ScriptManager.RegisterStartupScript(this, GetType(), "alert",
                                "alert('Saved Successfully ');", true);
                            btnJobCreation.Visible = true;
                            //btnShipment.Visible = true;
                            //btnShipmentCon.Visible = true;
                            //btnInvoice.Visible = true;
                            //btnProduct.Visible = true;
                        }
                        conn.Close();
                        i++;
                    }
                    catch (Exception ex)
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "alert",
                            "alert('DataBase Error: " + ex.Message + "  " + (string)Session["jobno"] + "');", true);
                    }
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Error in Shipment ');", true);
            }
        }

        public void Shipmentcontainer()
        {
            try
            {
                var Query = new StringBuilder();

                Query.Append("SELECT JOB_NO,CONT_SIZE,CONT_TYPE,CONT_NO,SEAL_NO,CONT_TYPE from impcontdet where JOB_NO like '" + (string)Session["JobCon"] + "%';");//wrkblk='" +(string)Session["WorkBlk"] + "' and

                var ds = GetDataMy(Query.ToString());
                var dt = ds.Tables["data"];
                var i = 0;

                //Access Datatable to check whether the form is present or not.
                foreach (DataRow row in dt.Rows)
                {
                    try
                    {
                        Session["jobno"] = string.Empty;
                        var JobNo = string.Empty;
                        var ShipTransID = 0;
                        var Container = string.Empty;
                        var ContainerType = string.Empty;
                        var ContainerNo = string.Empty;
                        var SealNo = string.Empty;
                        var LoadType = string.Empty;
                        var CreatedBy = (string)Session["User-Name"];
                        var CreatedDate = DateTime.Now.ToString();
                        //string CreatedBy = "VTSADMIN";
                        //string CreatedDate = DateTime.Now.ToString();
                        var ModifiedDate = DateTime.Now.ToString();

                        JobNo = dt.Rows[i]["JOB_NO"].ToString();
                        var Job = JobNo.Split('/');
                        JobNo = Job[1];
                        Session["jobno"] = JobNo;
                        var ShipTransQuery = "Select [TransId] from [T_ShipmentDetails] where [JobNo] = '" + JobNo + "'";
                        var ds1 = objCommonDL.GetDataSet(ShipTransQuery);
                        if (ds1.Tables["Table"].Rows.Count != 0)
                        {
                            var row1 = ds1.Tables["Table"].DefaultView[0];
                            ShipTransID = Convert.ToInt32(row1["TransId"]);
                        }
                        Container = dt.Rows[i]["CONT_SIZE"].ToString();
                        ContainerType = dt.Rows[i]["CONT_TYPE"].ToString();
                        ContainerNo = dt.Rows[i]["CONT_NO"].ToString();
                        SealNo = dt.Rows[i]["SEAL_NO"].ToString();
                        LoadType = dt.Rows[i]["CONT_TYPE"].ToString();

                        var IMPQuery = new StringBuilder();

                        IMPQuery.Append(
                            "Insert into T_ShipmentContainerInfo(JobNo,ShipTransID, Container, ContainerType, ContainerNo, SealNo, LoadType,CreatedBy, ");
                        IMPQuery.Append("CreatedDate, ModifiedBy, ModifiedDate)");

                        IMPQuery.Append(
                            " Values (@JobNo,@ShipTransID, @Container, @ContainerType, @ContainerNo, @SealNo, @LoadType, @CreatedBy, ");
                        IMPQuery.Append("@CreatedDate, @ModifiedBy, @ModifiedDate)");

                        var conn = new SqlConnection(Impstrconn);
                        conn.Open();
                        var cmd = new SqlCommand(IMPQuery.ToString(), conn);

                        cmd.Parameters.AddWithValue("@JobNo", JobNo);
                        cmd.Parameters.AddWithValue("@ShipTransID", ShipTransID);
                        cmd.Parameters.AddWithValue("@Container", Container);
                        cmd.Parameters.AddWithValue("@ContainerType", Container);
                        cmd.Parameters.AddWithValue("@ContainerNo", ContainerNo);
                        cmd.Parameters.AddWithValue("@SealNo", SealNo);
                        cmd.Parameters.AddWithValue("@LoadType", LoadType);
                        cmd.Parameters.AddWithValue("@CreatedBy", CreatedBy);
                        cmd.Parameters.AddWithValue("@CreatedDate", CreatedDate);
                        cmd.Parameters.AddWithValue("@ModifiedBy", CreatedBy);
                        cmd.Parameters.AddWithValue("@ModifiedDate", ModifiedDate);

                        var result = cmd.ExecuteNonQuery();
                        if (result == 1)
                        {
                            ScriptManager.RegisterStartupScript(this, GetType(), "alert",
                                "alert('Saved Successfully ');", true);
                            btnJobCreation.Visible = true;
                            //btnShipment.Visible = true;
                            //btnShipmentCon.Visible = true;
                            //btnInvoice.Visible = true;
                            //btnProduct.Visible = true;
                        }
                        conn.Close();
                        i++;
                    }

                    catch (Exception ex)
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "alert",
                            "alert('DataBase Error: " + ex.Message + "  " + (string)Session["jobno"] + "');", true);
                    }
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Error in Shipment Container');", true);
            }
        }

        public void Invoice()
        {
            try
            {
                var Query = new StringBuilder();
                Query.Append("SELECT JOB_NO,FREIGHT,FRE_CUR,FRE_CRATE,FRE_ACTUAL,INS_CUR,INS_CRATE,MISC_CURNC,MIS_CRATE,MISC_CHARG,INV_NO,DATE_FORMAT(INV_DATE,'%d/%m/%Y') as INV_DATE,TOI,PYMT_TERMS,TRAN_TYPE,CURRENCY,EXCH_RATE,PROD_VALUE,INV_VALUE,FRE_CUR,FRE_PERCE,FRE_CRATE,INS_CRATE,INS_ACTUAL,INS_PERCE,CNSR_NAME,CNSR_ADD,Cnsr_Cntry,Seller_Cntry,SALE_CONDN,SVB_ADDLDG,SVB_REF,SVB_REFDT,CUS_HOUSE,SVB_ON,PUR_ORDNO,PUR_ORDDT,CNTRCT_NO,CNTRCT_DT,LOC_NO,LOC_DATE,MISC_CURNC,MISC_BASIS,AGY_CURNC,AGY_CRATE,AGY_AMT,LDG_CURNC,LDG_CRATE,LDG_AMT,ProdCount,INSURANCE,FRT_CERT,AGY_CERT from iinv_dtl where JOB_NO like '" + (string)Session["JobCon"] + "%' order by job_no asc;");// wrkblk='" +(string) Session["WorkBlk"] + "' and

                var ds = GetDataMy(Query.ToString());
                var dt = ds.Tables["data"];

                var i = 0;

                //Access Datatable to check whether the form is present or not.
                foreach (DataRow row in dt.Rows)
                {
                    try
                    {
                        Session["jobno"] = string.Empty;
                        var JobNo = string.Empty;
                        var ImporterName = string.Empty;
                        var FreightType = "Single";
                        var FreightTyCurrency = string.Empty;
                        var FreightTyExRate = 0.00;
                        var FreightTyAmount = 0.00;
                        var FreightTyAmountINR = 0.00;
                        var InsuranceType = "Single";
                        var InsuranceTyCurrency = string.Empty;
                        var InsuranceTyExRate = 0.00;
                        var InsuranceTyAmount = 0.00;
                        var InsuranceTyAmountINR = 0.00;
                        var MiscellaneousType = "Single";
                        var MiscellaneousTyCurrency = string.Empty;
                        var MiscellaneousTyExRate = 0.00;
                        var MiscellaneousTyAmt = 0.00;
                        var MiscellaneousTyAmtINR = 0.00;
                        var InvoiceNo = string.Empty;
                        var InvoiceDate = string.Empty;
                        var InvoiceTerms = string.Empty;
                        var InvoiceFreightType = "Single";
                        var InvoicePaymentTerms = string.Empty;
                        var InvoiceNatureofTrans = string.Empty;
                        var InvoiceCurrency = string.Empty;
                        var InvoiceExchangeRates = 0.00;
                        var InvoiceProductValues = 0.00;
                        var InvoiceProductINRValues = 0.00;
                        var FreightCurrency = string.Empty;
                        var FreightExchangeRate = 0.00;
                        var FreightRate = 0.00;
                        var FreightAmount = 0.00;
                        var FreightINRAmount = 0.00;
                        var InsuranceCurrency = string.Empty;
                        var InsuranceExchangeRate = 0.00;
                        var InsuranceRate = 0.00;
                        var InsuranceAmount = 0.00;
                        var InsuranceINRAmount = 0.00;
                        var ConsignorName = string.Empty;
                        var ConsignorNameAddress = string.Empty;
                        var ConsignorCountry = string.Empty;
                        var SellerName = string.Empty;
                        var SellerNameAddress = string.Empty;
                        var SellerCountry = string.Empty;
                        var BrokerName = string.Empty;
                        var BrokerNameAddress = string.Empty;
                        var BrokerCountry = "~Select~";
                        var BuyerSeller = true;
                        var Relation = string.Empty;
                        var Base = string.Empty;
                        var Condition = string.Empty;
                        var SVB = true;
                        var SVBRefNo = string.Empty;
                        var SVBRefDate = string.Empty;
                        var CustomHouse = string.Empty;
                        var LoadingOn = string.Empty;
                        var AssableLoadingRate = 0.00;
                        var AssableStatus = string.Empty;
                        var DutyLoadingRate = 0.00;
                        var DutyStatus = string.Empty;
                        var PONo = string.Empty;
                        var PODate = string.Empty;
                        var ContractNo = string.Empty;
                        var ContractDate = string.Empty;
                        var LCNo = string.Empty;
                        var LCDate = string.Empty;
                        var ValuationMethod = "Rule 4";
                        var MisCurrency = string.Empty;
                        var MisExchRate = 0.00;
                        var MisRate = 0.00;
                        var MisAmount = 0.00;
                        var MisINRAmount = 0.00;
                        var AgencyCurrency = string.Empty;
                        var AgencyExchRate = 0.00;
                        var AgencyRate = 0.00;
                        var AgencyAmount = 0.00;
                        var AgencyINRAmount = 0.00;
                        var LoadingCurrency = string.Empty;
                        var LoadingExchRate = 0.00;
                        var LoadingRate = 0.00;
                        var LoadingAmount = 0.00;
                        var HighSeaCurrency = "~Select~";
                        var HighSeaExRate = 0.00;
                        var HighSeaRate = 0.00;
                        var HighSeaAmt = 0.00;
                        var HighSeaAmtINR = 0.00;
                        var SaleCondition = string.Empty;
                        var Remarks = string.Empty;
                        var SinglePO = true;
                        var NoofProduct = 0;
                        var CreatedBy = (string)Session["User-Name"];
                        var CreatedDate = DateTime.Now.ToString();
                        var ModifiedBy = "VTSADMIN";
                        var ModifiedDate = DateTime.Now.ToString();
                        var DiscountCurrency = "~Select~";


                        JobNo = dt.Rows[i]["JOB_NO"].ToString();
                        var Job = JobNo.Split('/');
                        JobNo = Job[1];
                        Session["jobno"] = JobNo;
                        //ImporterName =dt.Rows[i]["PROD_CODE"].ToString();
                        if (dt.Rows[i]["FRE_CUR"] != DBNull.Value)
                        {
                            FreightTyCurrency = dt.Rows[i]["FRE_CUR"].ToString();
                        }
                        else
                        {
                            FreightTyCurrency = "~Select~";
                        }
                        if (dt.Rows[i]["FRE_CRATE"] != DBNull.Value)
                        {
                            FreightTyExRate = Convert.ToDouble(dt.Rows[i]["FRE_CRATE"]);
                        }
                        if (dt.Rows[i]["FREIGHT"] != DBNull.Value)
                        {
                            FreightTyAmount = Convert.ToDouble(dt.Rows[i]["FREIGHT"]);
                        }
                        FreightTyAmountINR = FreightTyAmount * FreightTyExRate;
                        //InsuranceType =dt.Rows[i]["PROD_CODE"].ToString();
                        InsuranceTyCurrency = dt.Rows[i]["INS_CUR"] != DBNull.Value ? dt.Rows[i]["INS_CUR"].ToString() : "~Select~";
                        if (dt.Rows[i]["INS_CRATE"] != DBNull.Value)
                        {
                            InsuranceTyExRate = Convert.ToDouble(dt.Rows[i]["INS_CRATE"]);
                        }
                        if (dt.Rows[i]["INSURANCE"] != DBNull.Value)
                        {
                            InsuranceTyAmount = Convert.ToDouble(dt.Rows[i]["INSURANCE"]);
                        }
                        InsuranceTyAmountINR = InsuranceTyExRate * InsuranceTyAmount;
                        //MiscellaneousType =dt.Rows[i]["PROD_CODE"].ToString();
                        MiscellaneousTyCurrency = dt.Rows[i]["MISC_CURNC"] != DBNull.Value ? dt.Rows[i]["MISC_CURNC"].ToString() : "~Select~";
                        if (dt.Rows[i]["MIS_CRATE"] != DBNull.Value)
                        {
                            MiscellaneousTyExRate = Convert.ToDouble(dt.Rows[i]["MIS_CRATE"]);
                        }
                        if (dt.Rows[i]["MISC_CHARG"] != DBNull.Value)
                        {
                            MiscellaneousTyAmt = Convert.ToDouble(dt.Rows[i]["MISC_CHARG"]);
                        }
                        MiscellaneousTyAmtINR = MiscellaneousTyExRate * MiscellaneousTyAmt;

                        InvoiceNo = dt.Rows[i]["INV_NO"].ToString();
                        InvoiceDate = dt.Rows[i]["INV_DATE"].ToString();
                        
                        if (dt.Rows[i]["TOI"] != DBNull.Value)
                        {
                            InvoiceTerms = dt.Rows[i]["TOI"].ToString();
                            if (InvoiceTerms.ToString() == "FOB")
                            {
                                InvoiceTerms = "FOB";
                            }
                            else if (InvoiceTerms.ToString() == "C&F")
                            {
                                InvoiceTerms = "CF";
                            }
                            else if (InvoiceTerms.ToString() == "CIF")
                            {
                                InvoiceTerms = "CIF";
                            }
                            else if (InvoiceTerms.ToString() == "C&I")
                            {
                                InvoiceTerms = "CI";
                            }
                        }
                        // InvoiceFreightType =dt.Rows[i]["PROD_CODE"].ToString();
                        if (dt.Rows[i]["PYMT_TERMS"] != DBNull.Value)
                        {
                            InvoicePaymentTerms = dt.Rows[i]["PYMT_TERMS"].ToString();
                        }
                        if (dt.Rows[i]["TRAN_TYPE"] != DBNull.Value)
                        {
                            InvoiceNatureofTrans = dt.Rows[i]["TRAN_TYPE"].ToString();
                            if (InvoiceNatureofTrans == "Sale")
                            {
                                InvoiceNatureofTrans = "S";
                            }
                            else if (InvoiceNatureofTrans == "Free of cost")
                            {
                                InvoiceNatureofTrans = "F";
                            }
                            else if (InvoiceNatureofTrans == "Sale on Consignment basis")
                            {
                                InvoiceNatureofTrans = "C";
                            }
                            else if (InvoiceNatureofTrans == "Rent")
                            {
                                InvoiceNatureofTrans = "R";
                            }
                            else
                            {
                                InvoiceNatureofTrans = "S";
                            }
                        }
                        else
                        {
                            InvoiceNatureofTrans = "S";
                        }
                        if (dt.Rows[i]["CURRENCY"] != DBNull.Value)
                        {
                            InvoiceCurrency = dt.Rows[i]["CURRENCY"].ToString();
                        }
                        else
                        {
                            InvoiceCurrency = "~Select~";
                        }
                        if (dt.Rows[i]["EXCH_RATE"] != DBNull.Value)
                        {
                            InvoiceExchangeRates = Convert.ToDouble(dt.Rows[i]["EXCH_RATE"]);
                        }
                        if (dt.Rows[i]["INV_VALUE"] != DBNull.Value)
                        {
                            InvoiceProductValues = Convert.ToDouble(dt.Rows[i]["INV_VALUE"]);
                        }

                        InvoiceProductINRValues = InvoiceExchangeRates * InvoiceProductValues;

                        //InvoiceProductINRValues = Convert.ToDouble(dt.Rows[i]["INV_VALUE"]);
                        if (dt.Rows[i]["FRE_CUR"] != DBNull.Value)
                        {
                            FreightCurrency = dt.Rows[i]["FRE_CUR"].ToString();
                        }
                        else
                        {
                            FreightCurrency = "~Select~";
                        }
                        if (dt.Rows[i]["FRE_CRATE"] != DBNull.Value)
                        {
                            FreightExchangeRate = Convert.ToDouble(dt.Rows[i]["FRE_CRATE"]);
                        }
                        if (dt.Rows[i]["FRE_PERCE"] != DBNull.Value)
                        {
                            FreightRate = Convert.ToDouble(dt.Rows[i]["FRE_PERCE"]);
                        }
                        if (dt.Rows[i]["FREIGHT"] != DBNull.Value)
                        {
                            FreightAmount = Convert.ToDouble(dt.Rows[i]["FREIGHT"]);
                        }
                        FreightINRAmount = FreightAmount * FreightExchangeRate;
                        if (dt.Rows[i]["INS_CUR"] != DBNull.Value)
                        {
                            InsuranceCurrency = dt.Rows[i]["INS_CUR"].ToString();
                        }
                        else
                        {
                            InsuranceCurrency = "~Select~";
                        }
                        if (dt.Rows[i]["INS_CRATE"] != DBNull.Value)
                        {
                            InsuranceExchangeRate = Convert.ToDouble(dt.Rows[i]["INS_CRATE"].ToString());
                        }

                        //InsuranceExchangeRate =dt.Rows[i][""].ToString();
                        if (dt.Rows[i]["INS_PERCE"] != DBNull.Value)
                        {
                            InsuranceRate = Convert.ToDouble(dt.Rows[i]["INS_PERCE"]);
                        }
                        if (dt.Rows[i]["INSURANCE"] != DBNull.Value)
                        {
                            InsuranceAmount = Convert.ToDouble(dt.Rows[i]["INSURANCE"]);
                        }
                        InsuranceINRAmount = InsuranceExchangeRate * InsuranceAmount;
                        ConsignorName = dt.Rows[i]["CNSR_NAME"].ToString();
                        var Cons = ConsignorName.Split(',');
                        ConsignorName = Cons[0];
                        if (Cons.Length >= 3)
                        {
                            ConsignorNameAddress = Cons[1] + " " + Cons[2];
                        }
                        ConsignorCountry = dt.Rows[i]["Cnsr_Cntry"].ToString();
                        //SellerName =dt.Rows[i][""].ToString();
                        //SellerNameAddress =dt.Rows[i]["PROD_CODE"].ToString();
                        if ((dt.Rows[i]["Seller_Cntry"] != DBNull.Value) || ((string)dt.Rows[i]["Seller_Cntry"] != string.Empty))
                        {
                            SellerCountry = dt.Rows[i]["Seller_Cntry"].ToString();
                        }
                        else
                        {
                            SellerCountry = "~Select~";
                        }
                        //BrokerName =dt.Rows[i]["PROD_CODE"].ToString();
                        //BrokerNameAddress =dt.Rows[i]["PROD_CODE"].ToString();
                        //BrokerCountry =dt.Rows[i]["PROD_CODE"].ToString();
                        //BuyerSeller =dt.Rows[i]["PROD_CODE"].ToString();
                        //Relation =dt.Rows[i]["PROD_CODE"].ToString();
                        //Base =dt.Rows[i]["PROD_CODE"].ToString();
                        Condition = dt.Rows[i]["SALE_CONDN"].ToString();
                        if (dt.Rows[i]["SVB_ADDLDG"] != DBNull.Value)
                        {
                            SVB = Convert.ToBoolean(dt.Rows[i]["SVB_ADDLDG"]);
                        }
                        if (dt.Rows[i]["SVB_REF"] != DBNull.Value)
                        {
                            SVBRefNo = dt.Rows[i]["SVB_REF"].ToString();
                        }
                        if (dt.Rows[i]["SVB_REFDT"] != DBNull.Value)
                        {
                            SVBRefDate = dt.Rows[i]["SVB_REFDT"].ToString();
                        }
                        if (dt.Rows[i]["CUS_HOUSE"] != DBNull.Value)
                        {
                            CustomHouse = dt.Rows[i]["CUS_HOUSE"].ToString();
                        }
                        if (dt.Rows[i]["SVB_ON"] != DBNull.Value)
                        {
                            LoadingOn = dt.Rows[i]["SVB_ON"].ToString();
                        }
                        //AssableLoadingRate =dt.Rows[i]["PROD_CODE"].ToString();
                        //AssableStatus =dt.Rows[i]["PROD_CODE"].ToString();
                        //DutyLoadingRate =dt.Rows[i]["PROD_CODE"].ToString();
                        //DutyStatus =dt.Rows[i]["PROD_CODE"].ToString();
                        PONo = dt.Rows[i]["PUR_ORDNO"].ToString();
                        if (dt.Rows[i]["PUR_ORDDT"] != DBNull.Value)
                        {
                            PODate = dt.Rows[i]["PUR_ORDDT"].ToString();
                        }
                        ContractNo = dt.Rows[i]["CNTRCT_NO"].ToString();
                        if (dt.Rows[i]["CNTRCT_DT"] != DBNull.Value)
                        {
                            ContractDate = dt.Rows[i]["CNTRCT_DT"].ToString();
                        }
                        LCNo = dt.Rows[i]["LOC_NO"].ToString();
                        if (dt.Rows[i]["LOC_DATE"] != DBNull.Value)
                        {
                            LCDate = dt.Rows[i]["LOC_DATE"].ToString();
                        }
                        //ValuationMethod =dt.Rows[i]["PROD_CODE"].ToString();
                        if (dt.Rows[i]["MISC_CURNC"] != DBNull.Value)
                        {
                            MisCurrency = dt.Rows[i]["MISC_CURNC"].ToString();
                        }
                        else
                        {
                            MisCurrency = "~Select~";
                        }
                        if (dt.Rows[i]["MIS_CRATE"] != DBNull.Value)
                        {
                            MisExchRate = Convert.ToDouble(dt.Rows[i]["MIS_CRATE"]);
                        }
                        //MisRate =dt.Rows[i]["PROD_CODE"].ToString();
                        if (dt.Rows[i]["MISC_CHARG"] != DBNull.Value)
                        {
                            MisAmount = Convert.ToDouble(dt.Rows[i]["MISC_CHARG"]);
                        }
                        MisINRAmount = MisExchRate * MisAmount;
                        if (dt.Rows[i]["AGY_CURNC"] != DBNull.Value)
                        {
                            AgencyCurrency = dt.Rows[i]["AGY_CURNC"].ToString();
                        }
                        else
                        {
                            AgencyCurrency = "~Select~";
                        }
                        //AgencyExchRate =dt.Rows[i]["PROD_CODE"].ToString();
                        if (dt.Rows[i]["AGY_CERT"] != DBNull.Value)
                        {
                            if ((dt.Rows[i]["AGY_CERT"] != ""))
                            {
                                AgencyRate = Convert.ToDouble(dt.Rows[i]["AGY_CERT"]);
                            }
                        }
                        if (dt.Rows[i]["AGY_AMT"] != DBNull.Value)
                        {
                            AgencyAmount = Convert.ToDouble(dt.Rows[i]["AGY_AMT"]);
                        }
                        AgencyINRAmount = AgencyRate * AgencyRate;
                        if (dt.Rows[i]["LDG_CURNC"] != DBNull.Value)
                        {
                            LoadingCurrency = dt.Rows[i]["LDG_CURNC"].ToString();
                        }
                        else
                        {
                            LoadingCurrency = "~Select~";
                        }
                        if (dt.Rows[i]["LDG_CRATE"] != DBNull.Value)
                        {
                            LoadingExchRate = Convert.ToDouble(dt.Rows[i]["LDG_CRATE"]);
                        }
                        //LoadingRate =dt.Rows[i]["PROD_CODE"].ToString();
                        if (dt.Rows[i]["LDG_AMT"] != DBNull.Value)
                        {
                            LoadingAmount = Convert.ToDouble(dt.Rows[i]["LDG_AMT"]);
                        }
                        // HighSeaCurrency =dt.Rows[i]["PROD_CODE"].ToString();
                        //HighSeaExRate =dt.Rows[i]["PROD_CODE"].ToString();
                        //HighSeaRate =dt.Rows[i]["PROD_CODE"].ToString();
                        //HighSeaAmt =dt.Rows[i]["PROD_CODE"].ToString();
                        //HighSeaAmtINR =dt.Rows[i]["PROD_CODE"].ToString();
                        //SaleCondition =dt.Rows[i]["PROD_CODE"].ToString();
                        //Remarks =dt.Rows[i]["PROD_CODE"].ToString();
                        //SinglePO =dt.Rows[i]["PROD_CODE"].ToString();
                        if (dt.Rows[i]["ProdCount"] != DBNull.Value)
                        {
                            NoofProduct = Convert.ToInt32(dt.Rows[i]["ProdCount"]);
                        }

                        //DiscountCurrency
                        var IMPQuery = new StringBuilder();

                        IMPQuery.Append(
                            "Insert into T_InvoiceDetails(JobNo ,ImporterName ,FreightType ,FreightTyCurrency ,FreightTyExRate ,FreightTyAmount ,FreightTyAmountINR ,InsuranceType ,InsuranceTyCurrency,");
                        IMPQuery.Append(
                            "InsuranceTyExRate,InsuranceTyAmount,InsuranceTyAmountINR,MiscellaneousType, MiscellaneousTyCurrency,MiscellaneousTyExRate,MiscellaneousTyAmt,MiscellaneousTyAmtINR ,InvoiceNo ,InvoiceDate ,");
                        IMPQuery.Append(
                            "InvoiceTerms ,InvoiceFreightType ,InvoicePaymentTerms ,InvoiceNatureofTrans ,InvoiceCurrency ,InvoiceExchangeRates ,InvoiceProductValues ,InvoiceProductINRValues ,FreightCurrency,");
                        IMPQuery.Append(
                            "FreightExchangeRate ,FreightRate ,FreightAmount ,FreightINRAmount ,InsuranceCurrency ,InsuranceExchangeRate ,InsuranceRate ,InsuranceAmount,InsuranceINRAmount ,ConsignorName ,ConsignorNameAddress ,");
                        IMPQuery.Append(
                            "ConsignorCountry ,SellerName ,SellerNameAddress ,SellerCountry ,BrokerName ,BrokerNameAddress ,BrokerCountry ,BuyerSeller,Relation ,Base ,Condition ,SVB,");
                        IMPQuery.Append(
                            "SVBRefNo ,SVBRefDate ,CustomHouse ,LoadingOn ,AssableLoadingRate ,AssableStatus ,DutyLoadingRate ,DutyStatus ,PONo ,PODate ,ContractNo ,ContractDate ,LCNo ,LCDate ,ValuationMethod ,MisCurrency ,MisExchRate ,MisRate ,");
                        IMPQuery.Append(
                            "MisAmount ,MisINRAmount ,AgencyCurrency ,AgencyExchRate ,AgencyRate ,AgencyAmount ,AgencyINRAmount ,LoadingCurrency ,LoadingExchRate ,LoadingRate ,LoadingAmount ,HighSeaCurrency ,HighSeaExRate ,HighSeaRate ,HighSeaAmt ,HighSeaAmtINR ,");
                        IMPQuery.Append(
                            "SaleCondition ,Remarks ,SinglePO,NoofProduct,DiscountCurrency ,CreatedBy,CreatedDate,ModifiedBy,ModifiedDate)");

                        IMPQuery.Append(
                            "Values (@JobNo ,@ImporterName ,@FreightType ,@FreightTyCurrency ,@FreightTyExRate ,@FreightTyAmount ,@FreightTyAmountINR ,@InsuranceType ,@InsuranceTyCurrency,@InsuranceTyExRate,@InsuranceTyAmount,@InsuranceTyAmountINR,@MiscellaneousType,@MiscellaneousTyCurrency,@MiscellaneousTyExRate,@MiscellaneousTyAmt,@MiscellaneousTyAmtINR ,@InvoiceNo ,@InvoiceDate ,@InvoiceTerms ,@InvoiceFreightType ,@InvoicePaymentTerms ,@InvoiceNatureofTrans ,@InvoiceCurrency ,@InvoiceExchangeRates ,@InvoiceProductValues ,@InvoiceProductINRValues ,@FreightCurrency,");
                        IMPQuery.Append(
                            "@FreightExchangeRate ,@FreightRate ,@FreightAmount ,@FreightINRAmount ,@InsuranceCurrency ,@InsuranceExchangeRate ,@InsuranceRate ,@InsuranceAmount,@InsuranceINRAmount ,@ConsignorName ,@ConsignorNameAddress,");
                        IMPQuery.Append(
                            "@ConsignorCountry ,@SellerName ,@SellerNameAddress ,@SellerCountry ,@BrokerName ,@BrokerNameAddress ,@BrokerCountry ,@BuyerSeller,@Relation ,@Base ,@Condition ,@SVB,");
                        IMPQuery.Append(
                            "@SVBRefNo ,@SVBRefDate ,@CustomHouse ,@LoadingOn ,@AssableLoadingRate ,@AssableStatus ,@DutyLoadingRate ,@DutyStatus ,@PONo ,@PODate ,@ContractNo ,@ContractDate ,@LCNo ,@LCDate ,@ValuationMethod ,@MisCurrency ,@MisExchRate ,@MisRate ,");
                        IMPQuery.Append(
                            "@MisAmount ,@MisINRAmount ,@AgencyCurrency ,@AgencyExchRate ,@AgencyRate ,@AgencyAmount ,@AgencyINRAmount ,@LoadingCurrency ,@LoadingExchRate ,@LoadingRate ,@LoadingAmount ,@HighSeaCurrency ,@HighSeaExRate ,@HighSeaRate ,@HighSeaAmt ,@HighSeaAmtINR,");
                        IMPQuery.Append(
                            "@SaleCondition ,@Remarks ,@SinglePO,@NoofProduct,@DiscountCurrency ,@CreatedBy,@CreatedDate,@ModifiedBy,@ModifiedDate)");

                        var conn = new SqlConnection(Impstrconn);
                        conn.Open();
                        var da = new SqlDataAdapter();
                        var cmd = new SqlCommand(IMPQuery.ToString(), conn);

                        cmd.Parameters.AddWithValue("@JobNo", JobNo);
                        cmd.Parameters.AddWithValue("@ImporterName", ImporterName);
                        cmd.Parameters.AddWithValue("@FreightType", FreightType);
                        cmd.Parameters.AddWithValue("@FreightTyCurrency", FreightTyCurrency);
                        cmd.Parameters.AddWithValue("@FreightTyExRate", FreightTyExRate);
                        cmd.Parameters.AddWithValue("@FreightTyAmount", FreightTyAmount);
                        cmd.Parameters.AddWithValue("@FreightTyAmountINR", FreightTyAmountINR);
                        cmd.Parameters.AddWithValue("@InsuranceType", InsuranceType);
                        cmd.Parameters.AddWithValue("@InsuranceTyCurrency ", InsuranceTyCurrency);
                        cmd.Parameters.AddWithValue("@InsuranceTyExRate ", InsuranceTyExRate);
                        cmd.Parameters.AddWithValue("@InsuranceTyAmount ", InsuranceTyAmount);
                        cmd.Parameters.AddWithValue("@InsuranceTyAmountINR", InsuranceTyAmountINR);
                        cmd.Parameters.AddWithValue("@MiscellaneousType ", MiscellaneousType);
                        cmd.Parameters.AddWithValue("@MiscellaneousTyCurrency", MiscellaneousTyCurrency);
                        cmd.Parameters.AddWithValue("@MiscellaneousTyExRate ", MiscellaneousTyExRate);
                        cmd.Parameters.AddWithValue("@MiscellaneousTyAmt ", MiscellaneousTyAmt);
                        cmd.Parameters.AddWithValue("@MiscellaneousTyAmtINR ", MiscellaneousTyAmtINR);
                        cmd.Parameters.AddWithValue("@InvoiceNo ", InvoiceNo);
                        cmd.Parameters.AddWithValue("@InvoiceDate ", InvoiceDate);
                        cmd.Parameters.AddWithValue("@InvoiceTerms ", InvoiceTerms);
                        cmd.Parameters.AddWithValue("@InvoiceFreightType ", InvoiceFreightType);
                        cmd.Parameters.AddWithValue("@InvoicePaymentTerms ", InvoicePaymentTerms);
                        cmd.Parameters.AddWithValue("@InvoiceNatureofTrans ", InvoiceNatureofTrans);
                        cmd.Parameters.AddWithValue("@InvoiceCurrency ", InvoiceCurrency);
                        cmd.Parameters.AddWithValue("@InvoiceExchangeRates ", InvoiceExchangeRates);
                        cmd.Parameters.AddWithValue("@InvoiceProductValues ", InvoiceProductValues);
                        cmd.Parameters.AddWithValue("@InvoiceProductINRValues ", InvoiceProductINRValues);
                        cmd.Parameters.AddWithValue("@FreightCurrency ", FreightCurrency);
                        cmd.Parameters.AddWithValue("@FreightExchangeRate ", FreightExchangeRate);
                        cmd.Parameters.AddWithValue("@FreightRate ", FreightRate);
                        cmd.Parameters.AddWithValue("@FreightAmount", FreightAmount);
                        cmd.Parameters.AddWithValue("@FreightINRAmount ", FreightINRAmount);
                        cmd.Parameters.AddWithValue("@InsuranceCurrency ", InsuranceCurrency);
                        cmd.Parameters.AddWithValue("@InsuranceExchangeRate ", InsuranceExchangeRate);
                        cmd.Parameters.AddWithValue("@InsuranceRate ", InsuranceRate);
                        cmd.Parameters.AddWithValue("@InsuranceAmount", InsuranceAmount);
                        cmd.Parameters.AddWithValue("@InsuranceINRAmount ", InsuranceINRAmount);
                        cmd.Parameters.AddWithValue("@ConsignorName ", ConsignorName);
                        cmd.Parameters.AddWithValue("@ConsignorNameAddress ", ConsignorNameAddress);
                        cmd.Parameters.AddWithValue("@ConsignorCountry ", ConsignorCountry);
                        cmd.Parameters.AddWithValue("@SellerName ", SellerName);
                        cmd.Parameters.AddWithValue("@SellerNameAddress ", SellerNameAddress);
                        cmd.Parameters.AddWithValue("@SellerCountry ", SellerCountry);
                        cmd.Parameters.AddWithValue("@BrokerName ", BrokerName);
                        cmd.Parameters.AddWithValue("@BrokerNameAddress ", BrokerNameAddress);
                        cmd.Parameters.AddWithValue("@BrokerCountry ", BrokerCountry);
                        cmd.Parameters.AddWithValue("@BuyerSeller ", BuyerSeller);
                        cmd.Parameters.AddWithValue("@Relation ", Relation);
                        cmd.Parameters.AddWithValue("@Base ", Base);
                        cmd.Parameters.AddWithValue("@Condition ", Condition);
                        cmd.Parameters.AddWithValue("@SVB ", SVB);
                        cmd.Parameters.AddWithValue("@SVBRefNo ", SVBRefNo);
                        cmd.Parameters.AddWithValue("@SVBRefDate", SVBRefDate);
                        cmd.Parameters.AddWithValue("@CustomHouse", CustomHouse);
                        cmd.Parameters.AddWithValue("@LoadingOn ", LoadingOn);
                        cmd.Parameters.AddWithValue("@AssableLoadingRate ", AssableLoadingRate);
                        cmd.Parameters.AddWithValue("@AssableStatus ", AssableStatus);
                        cmd.Parameters.AddWithValue("@DutyLoadingRate", DutyLoadingRate);
                        cmd.Parameters.AddWithValue("@DutyStatus ", DutyStatus);
                        cmd.Parameters.AddWithValue("@PONo ", PONo);
                        cmd.Parameters.AddWithValue("@PODate", PODate);
                        cmd.Parameters.AddWithValue("@ContractNo ", ContractNo);
                        cmd.Parameters.AddWithValue("@ContractDate", ContractDate);
                        cmd.Parameters.AddWithValue("@LCNo ", LCNo);
                        cmd.Parameters.AddWithValue("@LCDate", LCDate);
                        cmd.Parameters.AddWithValue("@ValuationMethod ", ValuationMethod);
                        cmd.Parameters.AddWithValue("@MisCurrency ", MisCurrency);
                        cmd.Parameters.AddWithValue("@MisExchRate ", MisExchRate);
                        cmd.Parameters.AddWithValue("@MisRate ", MisRate);
                        cmd.Parameters.AddWithValue("@MisAmount", MisAmount);
                        cmd.Parameters.AddWithValue("@MisINRAmount ", MisINRAmount);
                        cmd.Parameters.AddWithValue("@AgencyCurrency", AgencyCurrency);
                        cmd.Parameters.AddWithValue("@AgencyExchRate ", AgencyExchRate);
                        cmd.Parameters.AddWithValue("@AgencyRate ", AgencyRate);
                        cmd.Parameters.AddWithValue("@AgencyAmount", AgencyAmount);
                        cmd.Parameters.AddWithValue("@AgencyINRAmount ", AgencyINRAmount);
                        cmd.Parameters.AddWithValue("@LoadingCurrency ", LoadingCurrency);
                        cmd.Parameters.AddWithValue("@LoadingExchRate ", LoadingExchRate);
                        cmd.Parameters.AddWithValue("@LoadingRate ", LoadingRate);
                        cmd.Parameters.AddWithValue("@LoadingAmount", LoadingAmount);
                        cmd.Parameters.AddWithValue("@HighSeaCurrency ", HighSeaCurrency);
                        cmd.Parameters.AddWithValue("@HighSeaExRate ", HighSeaExRate);
                        cmd.Parameters.AddWithValue("@HighSeaRate ", HighSeaRate);
                        cmd.Parameters.AddWithValue("@HighSeaAmt ", HighSeaAmt);
                        cmd.Parameters.AddWithValue("@HighSeaAmtINR ", HighSeaAmtINR);
                        cmd.Parameters.AddWithValue("@SaleCondition ", SaleCondition);
                        cmd.Parameters.AddWithValue("@Remarks ", Remarks);
                        cmd.Parameters.AddWithValue("@SinglePO ", SinglePO);
                        cmd.Parameters.AddWithValue("@NoofProduct ", NoofProduct);
                        cmd.Parameters.AddWithValue("@CreatedBy", CreatedBy);
                        cmd.Parameters.AddWithValue("@CreatedDate", CreatedDate);
                        cmd.Parameters.AddWithValue("@ModifiedBy", CreatedBy);
                        cmd.Parameters.AddWithValue("@ModifiedDate", ModifiedDate);
                        cmd.Parameters.AddWithValue("@DiscountCurrency", DiscountCurrency);
                        var Result = cmd.ExecuteNonQuery();
                        if (Result == 1)
                        {
                            ScriptManager.RegisterStartupScript(this, GetType(), "alert",
                                "alert('Saved Successfully ');", true);
                            btnJobCreation.Visible = true;
                            //btnShipment.Visible = true;
                            //btnShipmentCon.Visible = true;
                            //btnInvoice.Visible = true;
                            //btnProduct.Visible = true;
                        }
                        conn.Close();
                        i++;
                    }
                    catch (Exception ex)
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "alert",
                            "alert('DataBase Error: " + ex.Message + "  " + (string)Session["jobno"] + "');", true);
                    }
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Error in Invoice ');", true);
            }
        }

        public void Product()
        {

            var Query = new StringBuilder();

            //Query.Append("SELECT JOB_NO,INV_ID,PROD_CODE,PROD_SN,PROD_DESC,PROD_TYPE,QTY,UNIT,UNIT_PRICE,AMOUNT,QTY2,UNIT2,QTY3,UNIT3,MASTER_PROD,ITC_LOCN,ITCHS_CODE,POLICYPARA,POLICY_YR,LOADING,LDG_PERCE,LDG_BASIS,LDG_AMT,EXIM_Code,Scheme_Notn,CTH_SNo,RITC_NO,BAS_NOTN,BAS_SNO,BAS_DUTY,BAS_DFLAG,BAS_AMT,BAS_UNIT,AddlDuty_NOTN,AddlDuty_SNO,EDU_CESS_NOTN,EDU_CESS_SNO,EDU_CESS_RATE,NCD_Notn,NCD_SNo,NCD_Rate,NCD_DFlag,NCD_Amt,NCD_Unit,SAPTA_Notn,SAPTA_SNo,Tariff_Notn,Tariff_SNo,Tariff_Qty,Tariff_Amt,Tariff_CRate,Tariff_Cur,CET_NO,MRPSNo,MRP,MRPUnit,Abatement,AddlDuty_NOTN,AddlDuty_SNO,AddlDuty_RATE,AddlDutyAmt,HLTH_Notn,HLTH_SNo,HLTH_Rate,HLTH_DFlag,HLTH_Amt,HLTH_Unit,SADAmt,GEN_DESC,MANUFACTURER,BRAND,MODEL,ACCESSORY,END_USE,CNTRY_ORIG,ProdAmt,ProdAmtRs,Freight,Insurance,Miscellaneous,LandingChrg,ASS_VAL,CIFValue,SVBLdg,BasDutyAmtPer,BasDutyAmtQty,AntiDumpDutyAmt,SCDDutyAmt,SurchargeDutyAmt,AuxDutyAmt,TotBasicDutyAmt,CVDDutyAmtPer,CVDDutyAmtQty,ACVDDutyAmt,ACS2DutyAmt,SCVDDutyAmt,CESSDutyAmt,TotalCVDAmt,SADAmt,TotalDutyAmt from iproddtl where wrkblk='WP000008';");
            Query.Append(
                "SELECT i.JOB_NO,j.INV_NO,i.INV_ID,j.PUR_ORDNO,j.PUR_ORDDT,i.PROD_CODE,i.PROD_SN,i.PROD_DESC,i.PROD_TYPE,i.QTY,i.UNIT,i.UNIT_PRICE,i.AMOUNT,i.QTY2,i.UNIT2,i.QTY3,i.UNIT3,i.MASTER_PROD,i.ITC_LOCN,i.ITCHS_CODE");
            Query.Append(
                ",i.POLICYPARA,i.POLICY_YR,i.LOADING,i.LDG_PERCE,i.LDG_BASIS,i.LDG_AMT,i.EXIM_Code,i.Scheme_Notn,i.CTH_SNo,i.RITC_NO,i.CTH_NO,i.BAS_NOTN,i.BAS_SNO,i.BAS_DUTY,i.BAS_DFLAG");
            Query.Append(
                ",i.BAS_AMT,i.BAS_UNIT,i.AddlDuty_NOTN,i.AddlDuty_SNO,i.EDU_CESS_NOTN,i.EDU_CESS_SNO,i.EDU_CESS_RATE,i.NCD_Notn,i.NCD_SNo,i.NCD_Rate,i.NCD_DFlag,i.NCD_Amt,i.NCD_Unit,i.SAPTA_Notn");
            Query.Append(
                ",i.SAPTA_SNo,i.Tariff_Notn,i.Tariff_SNo,i.Tariff_Qty,i.Tariff_Amt,i.Tariff_CRate,i.Tariff_Cur,i.CET_NO,i.MRPSNo,i.MRP,i.MRPUnit,i.Abatement,i.AddlDuty_NOTN,i.AddlDuty_SNO");
            Query.Append(
                ",i.AddlDuty_RATE,i.AddlDutyAmt,i.HLTH_Notn,i.HLTH_SNo,i.HLTH_Rate,i.HLTH_DFlag,i.HLTH_Amt,i.HLTH_Unit,i.GEN_DESC,i.MANUFACTURER,i.BRAND,i.MODEL,i.ACCESSORY");
            Query.Append(
                ",i.END_USE,i.CNTRY_ORIG,i.ProdAmt,i.ProdAmtRs,i.Freight,i.Insurance,i.Miscellaneous,i.LandingChrg,i.ASS_VAL,i.CIFValue,i.SVBLdg,i.BasDutyAmtPer,i.BasDutyAmtQty");
            Query.Append(
                ",i.AntiDumpDutyAmt,i.SCDDutyAmt,i.SurchargeDutyAmt,i.AuxDutyAmt,i.TotBasicDutyAmt,i.CVDDutyAmtPer,i.CVDDutyAmtQty,i.ACVDDutyAmt,i.ACS2DutyAmt,i.SCVDDutyAmt,i.CESSDutyAmt,i.SAD_NOTN,i.SAD_SNO,i.SAD_RATE");
            Query.Append(
                ",i.TotalCVDAmt,i.SADAmt,i.TotalDutyAmt,i.CVD_NOTN,i.CVD_SNO,i.CVD_DUTY,i.CVD_DFLAG,i.CVD_AMT,i.CVD_UNIT,i.SCVD_DFLAG,i.ACVD_DFLAG,i.CESS_DFLAG from iproddtl i Inner Join iinv_dtl j On i.JOB_NO=j.JOB_NO and i.inv_id=j.inv_id   where  i.JOB_NO = '" + (string)Session["JobCon"] + "';");//i.wrkblk='" +(string)Session["WorkBlk"] + "' and //i.JOB_NO=j.JOB_NO and i.inv_id=j.inv_id and
            var ds = GetDataMy(Query.ToString());
            var dt = ds.Tables["data"];

            var i = 0;

            //Access Datatable to check whether the form is present or not.
            foreach (DataRow row in dt.Rows)
            {
                try
                {
                    Session["jobno"] = string.Empty;
                    var JobNo = string.Empty;
                    var InvSrNo = 0;
                    var InvoiceNo = string.Empty;
                    var ProductFamily = string.Empty;
                    var ProductCode = string.Empty;
                    var ProductSNo = 0;
                    var ProductDesc = string.Empty;
                    var PONo = string.Empty;
                    var PODate = string.Empty;
                    var ProType = string.Empty;
                    var Qty = 0.00;
                    var Unit = string.Empty;
                    var UnitPrice = 0.00;
                    var Amount = 0.00;
                    var AQty1 = 0.00;
                    var AQty1Unit = string.Empty;
                    var AQty2 = 0.00;
                    var AQty2Unit = string.Empty;
                    var MasterProduct = string.Empty;
                    var ITCLocation = string.Empty;
                    var ITCHSCode = string.Empty;
                    var PolicyPara = string.Empty;
                    var PolicyYear = string.Empty;
                    var Loading = 0.00;
                    var LodingOn = string.Empty;
                    var LoadingCurrency = string.Empty;
                    var LoadingRate = 0.00;
                    var LoadingUnit = string.Empty;
                    var LoadingAmount = 0.00;
                    var EximSchCode = string.Empty;
                    var EximSchDesc = string.Empty;
                    var SchNoten = string.Empty;
                    var SchNotenUnit = string.Empty;
                    var SchNotenDesc = string.Empty;
                    var CTHNo = string.Empty;
                    var RITCNo = string.Empty;
                    var RateType = "S";
                    var BasicDutyNotn = string.Empty;
                    var BasicDutySno = string.Empty;
                    var BasicDuty = string.Empty;
                    var BasicDutyRate = 0.00;
                    var BasicDutyFlag = string.Empty;
                    var BasicDutyAmount = 0.00;
                    var BasicDutyUnit = string.Empty;
                    var AddlNotn = string.Empty;
                    var AddlSNo = string.Empty;
                    var EduCessNotn = string.Empty;
                    var EduCessSNo = string.Empty;
                    var EduCessRate = 0.00;
                    var EduCessAmount = 0.00;
                    var SHECessAmount = 0.00;
                    var SHECessRate = 1.00;
                    var NCDNotn = string.Empty;
                    var NCDSno = string.Empty;
                    var NCDRate = 0.00;
                    var NCDDFlag = string.Empty;
                    var NCDAmount = 0.00;
                    var NCDUnit = string.Empty;
                    var NCDRule = string.Empty;
                    var SurNotn = string.Empty;
                    var SurSno = string.Empty;
                    var SurRate = 0.00;
                    var SAPTANotn = string.Empty;
                    var SAPTASNo = string.Empty;
                    var SAPTADesc = string.Empty;
                    var TariffValNotn = string.Empty;
                    var TariffValSNo = string.Empty;
                    var TariffUnitQty = 0.00;
                    var TariffUnit = string.Empty;
                    var TariffRate = 0.00;
                    var TariffAmount = 0.00;
                    var CETNo = string.Empty;
                    var MRPDuty = true;
                    var MRPSNo = string.Empty;
                    var MRP = 0.00;
                    var MRPUnit = string.Empty;
                    var MRPAbatement = 0.00;
                    var  AddlExNotn	= string.Empty;
                    var AddlExSlNo	= string.Empty;
                    var AddlExRate	= 0.00;
                    var AddlExFlag	= string.Empty;
                    var AddlExAmount = 0.00;
                    var AddlExUnit	= string.Empty;
                    var ExCVDNotn	= string.Empty;
                    var ExCVDSlNo	= string.Empty;
                    var EXCVDRate = 0.00;
                    var ExGSIAddlDutyNotn = string.Empty;
                    var ExGSIAddlDutySlNo = string.Empty;
                    var ExGSIAddlDutyRate = 0.00;
                    var ExGSIAddlDutyFlag = string.Empty;
                    var ExGSIAddlDutyAmount = 0.00;
                    var ExGSIAddlDutyUnit = string.Empty;
                    var ExSPLExDutyNotn = string.Empty;
                    var ExSPLExDutySlNo = string.Empty;
                    var ExSPLExDutyRate = 0.00;
                    var ExSPLExDutyFlag = string.Empty;
                    var ExSPLExDutyAmount = 0.00;
                    var ExSPLExDutyUnit = string.Empty;
                    var ExTTAAddlDutyNotn = string.Empty;
                    var ExTTAAddlDutySlNo = string.Empty;
                    var ExTTAAddlDutyRate = 0.00;
                    var ExTTAAddlDutyFlag = string.Empty;
                    var ExTTAAddlDutyAmount = 0.00;
                    var ExTTAAddlDutyUnit = string.Empty;
                    var ExHealthCessNotn = string.Empty;
                    var ExHealthCessSlNo = string.Empty;
                    var ExHealthCessRate = 0.00;
                    var ExHealthCessFlag = string.Empty;
                    var ExHealthCessAmount = 0.00;
                    var ExHealthCessUnit = string.Empty;
                    var ExCessNotn = string.Empty;
                    var ExCessSlNo = string.Empty;
                    var ExCessRate = 0.00;
                    var ExCessFlag = string.Empty;
                    var ExCessAmount = 0.00;
                    var ExCessUnit = string.Empty;
                    var ExSADNotn = string.Empty;
                    var ExSADSlno = string.Empty;
                    var ExSADRate = 0.00;
                    var GenericDesc = string.Empty;
                    var Manufacturer = string.Empty;
                    var Brand = string.Empty;
                    var Model = string.Empty;
                    var Accessories = string.Empty;
                    var EndUse = string.Empty;
                    var CountryofOrigin = string.Empty;
                    var ProdAmt = 0.00;
                    var ProdAmtRs = 0.00;
                    var Freight = 0.00;
                    var Insurance = 0.00;
                    var AgencyCharge = 0.00;
                    var Miscellaneous = 0.00;
                    var LandingChrg = 0.00;
                    var AssableValue = 0.00;
                    var TotalValue = 0.00;
                    var SVBLdg = 0.00;
                    var BasDutyAmtPer = 0.00;
                    var BasDutyAmtQty = 0.00;
                    var AntiDumpDutyAmt = 0.00;
                    var SCDDutyAmt = 0.00;
                    var SurchargeDutyAmt = 0.00;
                    var AuxDutyAmt = 0.00;
                    var TotBasicDutyAmt = 0.00;
                    var CVDDutyAmtPer = 0.00;
                    var CVDDutyAmtQty = 0.00;
                    var ACVDDutyAmt = 0.00;
                    var ACS2DutyAmt = 0.00;
                    var SCVDDutyAmt = 0.00;
                    var CESSDutyAmt = 0.00;
                    var TotalCVDAmt = 0.00;
                    var SADAmt = 0.00;
                    var TotalDutyAmt = 0.00;
                    var ExEduCessNotn = "013/2012";
                    var ExEduCessSlNo = "1";
                    var CreatedBy = (string)Session["User-Name"];
                    var CreatedDate = DateTime.Now.ToString();
                    var ModifiedBy = "VTSADMIN";
                    var ModifiedDate = DateTime.Now.ToString();

                    JobNo = dt.Rows[i]["JOB_NO"].ToString();
                    var Job = JobNo.Split('/');
                    JobNo = Job[1];
                    Session["jobno"] = JobNo;
                    //InvSrNo = Convert.ToInt32(dt.Rows[i]["INV_ID"]);
                    InvoiceNo = dt.Rows[i]["INV_NO"].ToString();
                    //ProductFamily = dt.Rows[i]["JOB_NO"].ToString();
                    if (dt.Rows[i]["PROD_CODE"] != DBNull.Value)
                    {
                        ProductCode = dt.Rows[i]["PROD_CODE"].ToString();
                    }
                    if (dt.Rows[i]["PROD_SN"] != DBNull.Value)
                    {
                        ProductSNo = Convert.ToInt32(dt.Rows[i]["PROD_SN"]);
                    }
                    if (dt.Rows[i]["PROD_DESC"] != DBNull.Value)
                    {
                        ProductDesc = dt.Rows[i]["PROD_DESC"].ToString();
                    }
                    if (dt.Rows[i]["PUR_ORDNO"] != DBNull.Value)
                    {
                        PONo = dt.Rows[i]["PUR_ORDNO"].ToString();
                    }
                    if (dt.Rows[i]["PUR_ORDDT"] != DBNull.Value)
                    {
                        PODate = dt.Rows[i]["PUR_ORDDT"].ToString();
                    }
                    if (dt.Rows[i]["PROD_TYPE"] != DBNull.Value)
                    {
                        ProType = dt.Rows[i]["PROD_TYPE"].ToString();
                    }
                    if (dt.Rows[i]["QTY"] != DBNull.Value)
                    {
                        Qty = Convert.ToDouble(dt.Rows[i]["QTY"]);
                    }
                    if (dt.Rows[i]["UNIT"] != DBNull.Value)
                    {
                        Unit = dt.Rows[i]["UNIT"].ToString();
                    }
                    if (dt.Rows[i]["UNIT_PRICE"] != DBNull.Value)
                    {
                        UnitPrice = Convert.ToDouble(dt.Rows[i]["UNIT_PRICE"]);
                    }
                    if (dt.Rows[i]["AMOUNT"] != DBNull.Value)
                    {
                        Amount = Convert.ToDouble(dt.Rows[i]["AMOUNT"]);
                    }
                    if (dt.Rows[i]["QTY2"] != DBNull.Value)
                    {
                        AQty1 = Convert.ToDouble(dt.Rows[i]["QTY2"]);
                    }
                    if (dt.Rows[i]["UNIT2"] != DBNull.Value)
                    {
                        AQty1Unit = dt.Rows[i]["UNIT2"].ToString();
                    }
                    if (dt.Rows[i]["QTY3"] != DBNull.Value)
                    {
                        AQty2 = Convert.ToDouble(dt.Rows[i]["QTY3"]);
                    }
                    if (dt.Rows[i]["UNIT3"] != DBNull.Value)
                    {
                        AQty2Unit = dt.Rows[i]["UNIT3"].ToString();
                    }
                    if (dt.Rows[i]["MASTER_PROD"] != DBNull.Value)
                    {
                        MasterProduct = dt.Rows[i]["MASTER_PROD"].ToString();
                    }
                    if (dt.Rows[i]["ITC_LOCN"] != DBNull.Value)
                    {
                        ITCLocation = dt.Rows[i]["ITC_LOCN"].ToString();
                    }
                    if (dt.Rows[i]["ITCHS_CODE"] != DBNull.Value)
                    {
                        ITCHSCode = dt.Rows[i]["ITCHS_CODE"].ToString();
                    }
                    if (dt.Rows[i]["POLICYPARA"] != DBNull.Value)
                    {
                        PolicyPara = dt.Rows[i]["POLICYPARA"].ToString();
                    }
                    if (dt.Rows[i]["POLICY_YR"] != DBNull.Value)
                    {
                        PolicyYear = dt.Rows[i]["POLICY_YR"].ToString();
                    }
                    if (dt.Rows[i]["LOADING"] != DBNull.Value)
                    {
                        Loading = Convert.ToDouble(dt.Rows[i]["LOADING"]);
                    }
                    //LodingOn = dt.Rows[i]["JOB_NO"].ToString();
                    //LoadingCurrency = dt.Rows[i]["JOB_NO"].ToString();
                    if (dt.Rows[i]["LDG_PERCE"] != DBNull.Value)
                    {
                        LoadingRate = Convert.ToDouble(dt.Rows[i]["LDG_PERCE"]);
                    }
                    //if (dt.Rows[i]["LDG_BASIS"] != DBNull.Value)
                    //{
                    //    LoadingUnit = null;
                    //}
                    if (dt.Rows[i]["LDG_AMT"] != DBNull.Value)
                    {
                        LoadingAmount = Convert.ToDouble(dt.Rows[i]["LDG_AMT"]);
                    }
                    if (dt.Rows[i]["EXIM_Code"] != DBNull.Value)
                    {
                        EximSchCode = dt.Rows[i]["EXIM_Code"].ToString();
                    }
                    if (dt.Rows[i]["Scheme_Notn"] != DBNull.Value)
                    {
                        //EximSchDesc = dt.Rows[i][""].ToString();
                        SchNoten = dt.Rows[i]["Scheme_Notn"].ToString();
                    }
                    //SchNotenUnit = dt.Rows[i]["JOB_NO"].ToString();
                    //SchNotenDesc = dt.Rows[i]["JOB_NO"].ToString();
                    if (dt.Rows[i]["CTH_NO"] != DBNull.Value)
                    {
                        CTHNo = dt.Rows[i]["CTH_NO"].ToString();
                    }
                    if (dt.Rows[i]["RITC_NO"] != DBNull.Value)
                    {
                        RITCNo = dt.Rows[i]["RITC_NO"].ToString();
                    }
                    if (dt.Rows[i]["BAS_NOTN"] != DBNull.Value)
                    {
                        //RateType = dt.Rows[i]["JOB_NO"].ToString();
                        BasicDutyNotn = dt.Rows[i]["BAS_NOTN"].ToString();
                    }
                    if (dt.Rows[i]["BAS_SNO"] != DBNull.Value)
                    {
                        BasicDutySno = dt.Rows[i]["BAS_SNO"].ToString();
                    }
                    if (dt.Rows[i]["BAS_DUTY"] != DBNull.Value)
                    {
                        BasicDuty = dt.Rows[i]["BAS_DUTY"].ToString();
                    }
                    if (dt.Rows[i]["BAS_DFLAG"] != DBNull.Value)
                    {
                        BasicDutyFlag = dt.Rows[i]["BAS_DFLAG"].ToString();
                        switch (BasicDutyFlag)
                        {
                            case "+":
                                BasicDutyFlag = "Plus";
                                break;
                            case "-":
                                BasicDutyFlag = "Minus";
                                break;
                            case "H":
                                BasicDutyFlag = "Higher";
                                break;
                            case "L":
                                BasicDutyFlag = "Lower";
                                break;
                            default:
                                BasicDutyFlag = "Plus";
                                break;
                        }
                    }
                    if (dt.Rows[i]["BAS_AMT"] != DBNull.Value)
                    {
                        BasicDutyAmount = Convert.ToDouble(dt.Rows[i]["BAS_AMT"]);
                    }
                    if (dt.Rows[i]["BAS_UNIT"] != DBNull.Value)
                    {
                        BasicDutyUnit = dt.Rows[i]["BAS_UNIT"].ToString();
                    }
                    if (dt.Rows[i]["AddlDuty_NOTN"] != DBNull.Value)
                    {
                        AddlNotn = dt.Rows[i]["AddlDuty_NOTN"].ToString();
                    }
                    if (dt.Rows[i]["AddlDuty_SNO"] != DBNull.Value)
                    {
                        AddlSNo = dt.Rows[i]["AddlDuty_SNO"].ToString();
                    }
                    if (dt.Rows[i]["EDU_CESS_NOTN"] != DBNull.Value)
                    {
                        EduCessNotn = dt.Rows[i]["EDU_CESS_NOTN"].ToString();
                    }
                    if (dt.Rows[i]["EDU_CESS_SNO"] != DBNull.Value)
                    {
                        EduCessSNo = dt.Rows[i]["EDU_CESS_SNO"].ToString();
                    }
                    if (dt.Rows[i]["EDU_CESS_RATE"] != DBNull.Value)
                    {
                        EduCessRate = Convert.ToDouble(dt.Rows[i]["EDU_CESS_RATE"]);
                    }
                    if (dt.Rows[i]["NCD_Notn"] != DBNull.Value)
                    {
                        NCDNotn = dt.Rows[i]["NCD_Notn"].ToString();
                    }
                    if (dt.Rows[i]["NCD_SNo"] != DBNull.Value)
                    {
                        NCDSno = dt.Rows[i]["NCD_SNo"].ToString();
                    }
                    if (dt.Rows[i]["NCD_Rate"] != DBNull.Value)
                    {
                        NCDRate = Convert.ToDouble(dt.Rows[i]["NCD_Rate"]);
                    }
                    if (dt.Rows[i]["NCD_DFlag"] != DBNull.Value)
                    {
                        NCDDFlag = dt.Rows[i]["NCD_DFlag"].ToString();
                        switch (NCDDFlag)
                        {
                            case "+":
                                NCDDFlag = "Plus";
                                break;
                            case "-":
                                NCDDFlag = "Minus";
                                break;
                            case "H":
                                NCDDFlag = "Higher";
                                break;
                            case "L":
                                NCDDFlag = "Lower";
                                break;
                            default:
                                NCDDFlag = "Plus";
                                break;
                        }
                    }
                    if (dt.Rows[i]["NCD_Amt"] != DBNull.Value)
                    {
                        NCDAmount = Convert.ToDouble(dt.Rows[i]["NCD_Amt"]);
                    }
                    if (dt.Rows[i]["NCD_Unit"] != DBNull.Value)
                    {
                        NCDUnit = dt.Rows[i]["NCD_Unit"].ToString();
                    }
                    if (dt.Rows[i]["SAPTA_Notn"] != DBNull.Value)
                    {
                        //NCDRule = dt.Rows[i][""].ToString();
                        //SurNotn = dt.Rows[i][""].ToString();
                        //SurSno = dt.Rows[i]["JOB_NO"].ToString();
                        //SurRate = dt.Rows[i]["JOB_NO"].ToString();
                        SAPTANotn = dt.Rows[i]["SAPTA_Notn"].ToString();
                    }
                    if (dt.Rows[i]["SAPTA_SNo"] != DBNull.Value)
                    {
                        SAPTASNo = dt.Rows[i]["SAPTA_SNo"].ToString();
                    }
                    if (dt.Rows[i]["Tariff_Notn"] != DBNull.Value)
                    {
                        //SAPTADesc = dt.Rows[i]["JOB_NO"].ToString();
                        TariffValNotn = dt.Rows[i]["Tariff_Notn"].ToString();
                    }
                    if (dt.Rows[i]["Tariff_SNo"] != DBNull.Value)
                    {
                        TariffValSNo = dt.Rows[i]["Tariff_SNo"].ToString();
                    }
                    if (dt.Rows[i]["Tariff_Qty"] != DBNull.Value)
                    {
                        TariffUnitQty = Convert.ToDouble(dt.Rows[i]["Tariff_Qty"]);
                    }
                    if (dt.Rows[i]["Tariff_Cur"] != DBNull.Value)
                    {
                        TariffUnit = dt.Rows[i]["Tariff_Cur"].ToString();
                    }
                    if (dt.Rows[i]["Tariff_CRate"] != DBNull.Value)
                    {
                        TariffRate = Convert.ToDouble(dt.Rows[i]["Tariff_CRate"]);
                    }
                    if (dt.Rows[i]["Tariff_Amt"] != DBNull.Value)
                    {
                        TariffAmount = Convert.ToDouble(dt.Rows[i]["Tariff_Amt"]);
                    }
                    if (dt.Rows[i]["CET_NO"] != DBNull.Value)
                    {
                        CETNo = dt.Rows[i]["CET_NO"].ToString();
                    }
                    var abt = Convert.ToDouble(dt.Rows[i]["Abatement"]);
                    if (abt > 0)
                    {
                        MRPDuty = true;
                    }
                    else
                    {
                        MRPDuty = false;
                    }
                    if (dt.Rows[i]["MRPSNo"] != DBNull.Value)
                    {
                        MRPSNo = dt.Rows[i]["MRPSNo"].ToString();
                    }
                    if (dt.Rows[i]["MRP"] != DBNull.Value)
                    {
                        MRP = Convert.ToDouble(dt.Rows[i]["MRP"]);
                    }
                    if (dt.Rows[i]["MRPUnit"] != DBNull.Value)
                    {
                        MRPUnit = dt.Rows[i]["MRPUnit"].ToString();
                    }
                    if (dt.Rows[i]["Abatement"] != DBNull.Value)
                    {
                        MRPAbatement = Convert.ToDouble(dt.Rows[i]["Abatement"]);
                    }
                    ///////////////////////////////////////////////////////////////////////
                    if (dt.Rows[i]["CVD_NOTN"] != DBNull.Value)
                    {
                        AddlExNotn = dt.Rows[i]["CVD_NOTN"].ToString();
                    }
                    if (dt.Rows[i]["CVD_SNO"] != DBNull.Value)
                    {
                        AddlExSlNo = dt.Rows[i]["CVD_SNO"].ToString();
                    }
                    if (dt.Rows[i]["CVD_DUTY"] != DBNull.Value)
                    {
                        AddlExRate = Convert.ToDouble(dt.Rows[i]["CVD_DUTY"]);
                    }
                    if (dt.Rows[i]["CVD_DFLAG"] != DBNull.Value)
                    {
                        AddlExFlag = dt.Rows[i]["CVD_DFLAG"].ToString();
                        switch (AddlExFlag)
                        {
                            case "+":
                                AddlExFlag = "Plus";
                                break;
                            case "-":
                                AddlExFlag = "Minus";
                                break;
                            case "H":
                                AddlExFlag = "Higher";
                                break;
                            case "L":
                                AddlExFlag = "Lower";
                                break;
                            default:
                                AddlExFlag = "Plus";
                                break;
                        }
                    }
                    if (dt.Rows[i]["CVD_AMT"] != DBNull.Value)
                    {
                        AddlExAmount = Convert.ToDouble(dt.Rows[i]["CVD_AMT"]);
                    }
                    if (dt.Rows[i]["CVD_UNIT"] != DBNull.Value)
                    {
                        AddlExUnit = dt.Rows[i]["CVD_UNIT"].ToString();
                    }
                    //SAD
                    if (dt.Rows[i]["AddlDuty_NOTN"] != DBNull.Value)
                    {
                        ExCVDNotn = dt.Rows[i]["AddlDuty_NOTN"].ToString();
                    }
                    if (dt.Rows[i]["AddlDuty_SNO"] != DBNull.Value)
                    {
                        ExCVDSlNo = dt.Rows[i]["AddlDuty_SNO"].ToString();
                    }
                    if (dt.Rows[i]["AddlDuty_RATE"] != DBNull.Value)
                    {
                        EXCVDRate = Convert.ToDouble(dt.Rows[i]["AddlDuty_RATE"].ToString());
                    }
                    //if (dt.Rows[i]["AddlDuty_NOTN"] != DBNull.Value)
                    //{
                    //    ExGSIAddlDutyNotn = dt.Rows[i]["AddlDuty_NOTN"].ToString();
                    //}
                    //if (dt.Rows[i]["AddlDuty_SNO"] != DBNull.Value)
                    //{
                    //    ExGSIAddlDutySlNo = dt.Rows[i]["AddlDuty_SNO"].ToString();
                    //}
                    //if (dt.Rows[i]["AddlDuty_RATE"] != DBNull.Value)
                    //{
                    //    ExGSIAddlDutyRate = Convert.ToDouble(dt.Rows[i]["AddlDuty_RATE"]);
                    //}
                    ExGSIAddlDutyFlag = "Plus";
                    //if (dt.Rows[i]["AddlDutyAmt"] != DBNull.Value)
                    //{
                        
                    //    //ExGSIAddlDutyAmount = Convert.ToDouble(dt.Rows[i]["AddlDutyAmt"]);
                    //}

                    //ExGSIAddlDutyUnit = dt.Rows[i]["JOB_NO"].ToString();
                    //ExSPLExDutyNotn = dt.Rows[i]["JOB_NO"].ToString();
                    //ExSPLExDutySlNo = dt.Rows[i]["JOB_NO"].ToString();
                    //ExSPLExDutyRate = dt.Rows[i]["JOB_NO"].ToString();

                    if (dt.Rows[i]["SCVD_DFLAG"] != DBNull.Value)
                    {
                        ExSPLExDutyFlag = dt.Rows[i]["SCVD_DFLAG"].ToString();
                        switch (ExSPLExDutyFlag)
                        {
                            case "+":
                                ExSPLExDutyFlag = "Plus";
                                break;
                            case "-":
                                ExSPLExDutyFlag = "Minus";
                                break;
                            case "H":
                                ExSPLExDutyFlag = "Higher";
                                break;
                            case "L":
                                ExSPLExDutyFlag = "Lower";
                                break;
                            default:
                                ExSPLExDutyFlag = "Plus";
                                break;
                        }
                    }
                   // ExSPLExDutyFlag = dt.Rows[i]["JOB_NO"].ToString();
                    //ExSPLExDutyAmount = dt.Rows[i]["JOB_NO"].ToString();
                    //ExSPLExDutyUnit = dt.Rows[i]["JOB_NO"].ToString();
                    if (dt.Rows[i]["HLTH_Notn"] != DBNull.Value)
                    {
                        ExTTAAddlDutyNotn = dt.Rows[i]["HLTH_Notn"].ToString();
                    }
                    //ExTTAAddlDutySlNo = dt.Rows[i]["JOB_NO"].ToString();
                    //ExTTAAddlDutyRate = dt.Rows[i]["JOB_NO"].ToString();
                    if (dt.Rows[i]["ACVD_DFLAG"] != DBNull.Value)
                    {
                        ExTTAAddlDutyFlag = dt.Rows[i]["ACVD_DFLAG"].ToString();
                        switch (ExTTAAddlDutyFlag)
                        {
                            case "+":
                                ExTTAAddlDutyFlag = "Plus";
                                break;
                            case "-":
                                ExTTAAddlDutyFlag = "Minus";
                                break;
                            case "H":
                                ExTTAAddlDutyFlag = "Higher";
                                break;
                            case "L":
                                ExTTAAddlDutyFlag = "Lower";
                                break;
                            default:
                                ExTTAAddlDutyFlag = "Plus";
                                break;
                        }
                    }
                    //ExTTAAddlDutyFlag = dt.Rows[i]["JOB_NO"].ToString();
                    //ExTTAAddlDutyAmount = dt.Rows[i]["JOB_NO"].ToString();
                    //ExTTAAddlDutyUnit = dt.Rows[i]["JOB_NO"].ToString();

                    //ExHealthCessNotn = dt.Rows[i]["JOB_NO"].ToString();
                    if (dt.Rows[i]["HLTH_SNo"] != DBNull.Value)
                    {
                        ExHealthCessSlNo = dt.Rows[i]["HLTH_SNo"].ToString();
                    }
                    if (dt.Rows[i]["HLTH_Rate"] != DBNull.Value)
                    {
                        ExHealthCessRate = Convert.ToDouble(dt.Rows[i]["HLTH_Rate"]);
                    }
                    if (dt.Rows[i]["HLTH_DFlag"] != DBNull.Value)
                    {
                        ExHealthCessFlag = dt.Rows[i]["HLTH_DFlag"].ToString();
                        switch (ExHealthCessFlag)
                        {
                            case "+":
                                ExHealthCessFlag = "Plus";
                                break;
                            case "-":
                                ExHealthCessFlag = "Minus";
                                break;
                            case "H":
                                ExHealthCessFlag = "Higher";
                                break;
                            case "L":
                                ExHealthCessFlag = "Lower";
                                break;
                            default:
                                ExHealthCessFlag = "Plus";
                                break;
                        }
                    }
                    //if (dt.Rows[i]["HLTH_DFlag"] != DBNull.Value)
                    //{
                    //    ExHealthCessFlag = dt.Rows[i]["HLTH_DFlag"].ToString();
                    //}
                    if (dt.Rows[i]["HLTH_Amt"] != DBNull.Value)
                    {
                        ExHealthCessAmount = Convert.ToDouble(dt.Rows[i]["HLTH_Amt"]);
                    }
                    if (dt.Rows[i]["HLTH_Unit"] != DBNull.Value)
                    {
                        ExHealthCessUnit = dt.Rows[i]["HLTH_Unit"].ToString();
                    }

                    //ExCessNotn = dt.Rows[i][""].ToString();
                    //ExCessSlNo = dt.Rows[i]["JOB_NO"].ToString();
                    //ExCessRate = dt.Rows[i]["JOB_NO"].ToString();
                    if (dt.Rows[i]["CESS_DFLAG"] != DBNull.Value)
                    {
                        ExCessFlag = dt.Rows[i]["CESS_DFLAG"].ToString();
                        switch (ExCessFlag)
                        {
                            case "+":
                                ExCessFlag = "Plus";
                                break;
                            case "-":
                                ExCessFlag = "Minus";
                                break;
                            case "H":
                                ExCessFlag = "Higher";
                                break;
                            case "L":
                                ExCessFlag = "Lower";
                                break;
                            default:
                                ExCessFlag = "Plus";
                                break;
                        }
                    }
                    //ExCessFlag = dt.Rows[i]["JOB_NO"].ToString();
                    //ExCessAmount = dt.Rows[i]["JOB_NO"].ToString();
                    //ExCessUnit = dt.Rows[i]["JOB_NO"].ToString();
                    //ExSADNotn = dt.Rows[i]["JOB_NO"].ToString();
                    //ExSADSlno = dt.Rows[i]["JOB_NO"].ToString();
                    if (dt.Rows[i]["AddlDutyAmt"] != DBNull.Value)
                    {
                        ExSADRate = Convert.ToDouble(dt.Rows[i]["AddlDutyAmt"]);
                    }
                    if (dt.Rows[i]["GEN_DESC"] != DBNull.Value)
                    {
                        GenericDesc = dt.Rows[i]["GEN_DESC"].ToString();
                    }
                    if (dt.Rows[i]["MANUFACTURER"] != DBNull.Value)
                    {
                        Manufacturer = dt.Rows[i]["MANUFACTURER"].ToString();
                    }
                    if (dt.Rows[i]["BRAND"] != DBNull.Value)
                    {
                        Brand = dt.Rows[i]["BRAND"].ToString();
                    }
                    if (dt.Rows[i]["MODEL"] != DBNull.Value)
                    {
                        Model = dt.Rows[i]["MODEL"].ToString();
                    }
                    if (dt.Rows[i]["ACCESSORY"] != DBNull.Value)
                    {
                        Accessories = dt.Rows[i]["ACCESSORY"].ToString();
                    }
                    if (dt.Rows[i]["END_USE"] != DBNull.Value)
                    {
                        EndUse = dt.Rows[i]["END_USE"].ToString();
                    }
                    if (dt.Rows[i]["CNTRY_ORIG"] != DBNull.Value)
                    {
                        CountryofOrigin = dt.Rows[i]["CNTRY_ORIG"].ToString();
                    }
                    if (dt.Rows[i]["ProdAmt"] != DBNull.Value)
                    {
                        ProdAmt = Convert.ToDouble(dt.Rows[i]["ProdAmt"]);
                    }
                    if (dt.Rows[i]["ProdAmtRs"] != DBNull.Value)
                    {
                        ProdAmtRs = Convert.ToDouble(dt.Rows[i]["ProdAmtRs"]);
                    }
                    if (dt.Rows[i]["Freight"] != DBNull.Value)
                    {
                        Freight = Convert.ToDouble(dt.Rows[i]["Freight"]);
                    }
                    if (dt.Rows[i]["Insurance"] != DBNull.Value)
                    {
                        Insurance = Convert.ToDouble(dt.Rows[i]["Insurance"]);
                    }
                    if (dt.Rows[i]["Miscellaneous"] != DBNull.Value)
                    {
                        //AgencyCharge = dt.Rows[i][""].ToString();
                        Miscellaneous = Convert.ToDouble(dt.Rows[i]["Miscellaneous"]);
                    }
                    if (dt.Rows[i]["LandingChrg"] != DBNull.Value)
                    {
                        LandingChrg = Convert.ToDouble(dt.Rows[i]["LandingChrg"]);
                    }
                    if (dt.Rows[i]["ASS_VAL"] != DBNull.Value)
                    {
                        AssableValue = Convert.ToDouble(dt.Rows[i]["ASS_VAL"]);
                    }
                    if (dt.Rows[i]["CIFValue"] != DBNull.Value)
                    {
                        TotalValue = Convert.ToDouble(dt.Rows[i]["CIFValue"]);
                    }
                    if (dt.Rows[i]["SVBLdg"] != DBNull.Value)
                    {
                        SVBLdg = Convert.ToDouble(dt.Rows[i]["SVBLdg"]);
                    }
                    if (dt.Rows[i]["BasDutyAmtPer"] != DBNull.Value)
                    {
                        BasDutyAmtPer = Convert.ToDouble(dt.Rows[i]["BasDutyAmtPer"]);
                    }
                    if (dt.Rows[i]["BasDutyAmtQty"] != DBNull.Value)
                    {
                        BasDutyAmtQty = Convert.ToDouble(dt.Rows[i]["BasDutyAmtQty"]);
                    }
                    // AntiDumpDutyAmt = dt.Rows[i]["JOB_NO"]);
                    if (dt.Rows[i]["SCDDutyAmt"] != DBNull.Value)
                    {
                        SCDDutyAmt = Convert.ToDouble(dt.Rows[i]["SCDDutyAmt"]);
                    }
                    if (dt.Rows[i]["SurchargeDutyAmt"] != DBNull.Value)
                    {
                        SurchargeDutyAmt = Convert.ToDouble(dt.Rows[i]["SurchargeDutyAmt"]);
                    }
                    if (dt.Rows[i]["AuxDutyAmt"] != DBNull.Value)
                    {
                        AuxDutyAmt = Convert.ToDouble(dt.Rows[i]["AuxDutyAmt"]);
                    }
                    if (dt.Rows[i]["TotBasicDutyAmt"] != DBNull.Value)
                    {
                        TotBasicDutyAmt = Convert.ToDouble(dt.Rows[i]["TotBasicDutyAmt"]);
                    }
                    if (dt.Rows[i]["CVDDutyAmtPer"] != DBNull.Value)
                    {
                        CVDDutyAmtPer = Convert.ToDouble(dt.Rows[i]["CVDDutyAmtPer"]);
                    }
                    if (dt.Rows[i]["CVDDutyAmtQty"] != DBNull.Value)
                    {
                        CVDDutyAmtQty = Convert.ToDouble(dt.Rows[i]["CVDDutyAmtQty"]);
                    }
                    if (dt.Rows[i]["ACVDDutyAmt"] != DBNull.Value)
                    {
                        ACVDDutyAmt = Convert.ToDouble(dt.Rows[i]["ACVDDutyAmt"]);
                    }
                    if (dt.Rows[i]["ACS2DutyAmt"] != DBNull.Value)
                    {
                        ACS2DutyAmt = Convert.ToDouble(dt.Rows[i]["ACS2DutyAmt"]);
                    }
                    if (dt.Rows[i]["SCVDDutyAmt"] != DBNull.Value)
                    {
                        SCVDDutyAmt = Convert.ToDouble(dt.Rows[i]["SCVDDutyAmt"]);
                    }
                    if (dt.Rows[i]["CESSDutyAmt"] != DBNull.Value)
                    {
                        CESSDutyAmt = Convert.ToDouble(dt.Rows[i]["CESSDutyAmt"]);
                    }
                    if (dt.Rows[i]["TotalCVDAmt"] != DBNull.Value)
                    {
                        TotalCVDAmt = Convert.ToDouble(dt.Rows[i]["TotalCVDAmt"]);
                    }
                    if (dt.Rows[i]["AddlDutyAmt"] != DBNull.Value)
                    {
                        SADAmt = Convert.ToDouble(dt.Rows[i]["AddlDutyAmt"]);
                    }
                    if (dt.Rows[i]["TotalDutyAmt"] != DBNull.Value)
                    {
                        TotalDutyAmt = Convert.ToDouble(dt.Rows[i]["TotalDutyAmt"]);
                    }
                    EduCessAmount = ((TotBasicDutyAmt + TotalCVDAmt) * EduCessRate) / 100;

                    SHECessAmount = ((TotBasicDutyAmt + TotalCVDAmt) * SHECessRate) / 100;

                    var IMPQuery = new StringBuilder();

                    IMPQuery.Append(
                        "Insert into T_Product(JobNo,InvSrNo,InvoiceNo,ProductFamily,ProductCode,ProductSNo,ProductDesc,PONo,PODate,ProType,Qty,Unit,UnitPrice,Amount,AQty1,AQty1Unit,AQty2,AQty2Unit,MasterProduct,");
                    IMPQuery.Append(
                        "ITCLocation,ITCHSCode,PolicyPara,PolicyYear,Loading,LodingOn,LoadingCurrency,LoadingRate,LoadingUnit,LoadingAmount,EximSchCode,EximSchDesc,SchNoten,SchNotenUnit,");
                    IMPQuery.Append(
                        "SchNotenDesc,CTHNo,RITCNo,RateType,BasicDutyNotn,BasicDutySno,BasicDuty,BasicDutyRate,BasicDutyFlag,BasicDutyAmount,BasicDutyUnit,AddlNotn,AddlSNo,EduCessNotn,");
                    IMPQuery.Append(
                        "EduCessSNo,EduCessRate,EduCessAmount,SHECessAmount,NCDNotn,NCDSno,NCDRate,NCDDFlag,NCDAmount,NCDUnit,NCDRule,SurNotn,SurSno,SurRate,SAPTANotn,SAPTASNo,SAPTADesc,TariffValNotn,TariffValSNo,");
                    IMPQuery.Append(
                        "TariffUnitQty,TariffUnit,TariffRate,TariffAmount,CETNo,MRPDuty,MRPSNo,MRP,MRPUnit,MRPAbatement,ExGSIAddlDutyNotn,ExGSIAddlDutySlNo,ExGSIAddlDutyRate,ExGSIAddlDutyFlag,");
                    IMPQuery.Append(
                        "ExGSIAddlDutyAmount,ExGSIAddlDutyUnit,ExSPLExDutyNotn,ExSPLExDutySlNo,ExSPLExDutyRate,ExSPLExDutyFlag,ExSPLExDutyAmount,ExSPLExDutyUnit,ExTTAAddlDutyNotn,");
                    IMPQuery.Append(
                        "ExTTAAddlDutySlNo,ExTTAAddlDutyRate,ExTTAAddlDutyFlag,ExTTAAddlDutyAmount,ExTTAAddlDutyUnit,ExHealthCessNotn,ExHealthCessSlNo,ExHealthCessRate,ExHealthCessFlag,");
                    IMPQuery.Append(
                        "ExHealthCessAmount,ExHealthCessUnit,ExCessNotn,ExCessSlNo,ExCessRate,ExCessFlag,ExCessAmount,ExCessUnit,ExSADNotn,ExSADSlno,ExSADRate,GenericDesc,Manufacturer,");
                    IMPQuery.Append(
                        "Brand,Model,Accessories,EndUse,CountryofOrigin,ProdAmt,ProdAmtRs,Freight,Insurance,AgencyCharge,Miscellaneous,LandingChrg,AssableValue,TotalValue,SVBLdg,BasDutyAmtPer,");
                    IMPQuery.Append(
                        "BasDutyAmtQty,AntiDumpDutyAmt,SCDDutyAmt,SurchargeDutyAmt,AuxDutyAmt,TotBasicDutyAmt,CVDDutyAmtPer,CVDDutyAmtQty,ACVDDutyAmt,ACS2DutyAmt,SCVDDutyAmt,");
                    IMPQuery.Append("CESSDutyAmt,TotalCVDAmt,SADAmt,TotalDutyAmt,AddlExNotn, AddlExSlNo, AddlExRate, AddlExFlag, AddlExAmount,AddlExUnit,ExCVDNotn,ExCVDSlNo,EXCVDRate,ExEduCessNotn,ExEduCessSlNo,SHECessRate,CreatedBy,");
                    IMPQuery.Append("CreatedDate, ModifiedBy, ModifiedDate)");

                    IMPQuery.Append(
                        "Values (@JobNo,@InvSrNo,@InvoiceNo,@ProductFamily,@ProductCode,@ProductSNo,@ProductDesc,@PONo,@PODate,@ProType,@Qty,@Unit,@UnitPrice,@Amount,@AQty1,@AQty1Unit,@AQty2,@AQty2Unit,@MasterProduct");
                    IMPQuery.Append(
                        ",@ITCLocation,@ITCHSCode,@PolicyPara,@PolicyYear,@Loading,@LodingOn,@LoadingCurrency,@LoadingRate,@LoadingUnit,@LoadingAmount,@EximSchCode,@EximSchDesc,@SchNoten,@SchNotenUnit");
                    IMPQuery.Append(
                        ",@SchNotenDesc,@CTHNo,@RITCNo,@RateType,@BasicDutyNotn,@BasicDutySno,@BasicDuty,@BasicDutyRate,@BasicDutyFlag,@BasicDutyAmount,@BasicDutyUnit,@AddlNotn,@AddlSNo,@EduCessNotn");
                    IMPQuery.Append(
                        ",@EduCessSNo,@EduCessRate,@EduCessAmount,@SHECessAmount,@NCDNotn,@NCDSno,@NCDRate,@NCDDFlag,@NCDAmount,@NCDUnit,@NCDRule,@SurNotn,@SurSno,@SurRate,@SAPTANotn,@SAPTASNo,@SAPTADesc,@TariffValNotn,@TariffValSNo");
                    IMPQuery.Append(
                        ",@TariffUnitQty,@TariffUnit,@TariffRate,@TariffAmount,@CETNo,@MRPDuty,@MRPSNo,@MRP,@MRPUnit,@MRPAbatement,@ExGSIAddlDutyNotn,@ExGSIAddlDutySlNo,@ExGSIAddlDutyRate,@ExGSIAddlDutyFlag");
                    IMPQuery.Append(
                        ",@ExGSIAddlDutyAmount,@ExGSIAddlDutyUnit,@ExSPLExDutyNotn,@ExSPLExDutySlNo,@ExSPLExDutyRate,@ExSPLExDutyFlag,@ExSPLExDutyAmount,@ExSPLExDutyUnit,@ExTTAAddlDutyNotn");
                    IMPQuery.Append(
                        ",@ExTTAAddlDutySlNo,@ExTTAAddlDutyRate,@ExTTAAddlDutyFlag,@ExTTAAddlDutyAmount,@ExTTAAddlDutyUnit,@ExHealthCessNotn,@ExHealthCessSlNo,@ExHealthCessRate,@ExHealthCessFlag");
                    IMPQuery.Append(
                        ",@ExHealthCessAmount,@ExHealthCessUnit,@ExCessNotn,@ExCessSlNo,@ExCessRate,@ExCessFlag,@ExCessAmount,@ExCessUnit,@ExSADNotn,@ExSADSlno,@ExSADRate,@GenericDesc,@Manufacturer");
                    IMPQuery.Append(
                        ",@Brand,@Model,@Accessories,@EndUse,@CountryofOrigin,@ProdAmt,@ProdAmtRs,@Freight,@Insurance,@AgencyCharge,@Miscellaneous,@LandingChrg,@AssableValue,@TotalValue,@SVBLdg,@BasDutyAmtPer");
                    IMPQuery.Append(
                        ",@BasDutyAmtQty,@AntiDumpDutyAmt,@SCDDutyAmt,@SurchargeDutyAmt,@AuxDutyAmt,@TotBasicDutyAmt,@CVDDutyAmtPer,@CVDDutyAmtQty,@ACVDDutyAmt,@ACS2DutyAmt,@SCVDDutyAmt");

                    IMPQuery.Append(",@CESSDutyAmt,@TotalCVDAmt,@SADAmt,@TotalDutyAmt,@AddlExNotn, @AddlExSlNo, @AddlExRate, @AddlExFlag, @AddlExAmount,@AddlExUnit,@ExCVDNotn,@ExCVDSlNo,@EXCVDRate,@ExEduCessNotn,@ExEduCessSlNo,@SHECessRate,@CreatedBy");
                    IMPQuery.Append(",@CreatedDate, @ModifiedBy, @ModifiedDate)");

                    var conn = new SqlConnection(Impstrconn);
                    conn.Open();
                    var da = new SqlDataAdapter();
                    var cmd = new SqlCommand(IMPQuery.ToString(), conn);
                    cmd.Parameters.AddWithValue("@JobNo", JobNo);
                    cmd.Parameters.AddWithValue("@InvSrNo", InvSrNo);
                    cmd.Parameters.AddWithValue("@InvoiceNo", InvoiceNo);
                    cmd.Parameters.AddWithValue("@ProductFamily", ProductFamily);
                    cmd.Parameters.AddWithValue("@ProductCode", ProductCode);
                    cmd.Parameters.AddWithValue("@ProductSNo", ProductSNo);
                    cmd.Parameters.AddWithValue("@ProductDesc", ProductDesc);
                    cmd.Parameters.AddWithValue("@PONo", PONo);
                    cmd.Parameters.AddWithValue("@PODate", PODate);
                    cmd.Parameters.AddWithValue("@ProType", ProType);
                    cmd.Parameters.AddWithValue("@Qty", Qty);
                    cmd.Parameters.AddWithValue("@Unit", Unit);
                    cmd.Parameters.AddWithValue("@UnitPrice", UnitPrice);
                    cmd.Parameters.AddWithValue("@Amount", Amount);
                    cmd.Parameters.AddWithValue("@AQty1", AQty1);
                    cmd.Parameters.AddWithValue("@AQty1Unit", AQty1Unit);
                    cmd.Parameters.AddWithValue("@AQty2", AQty2);
                    cmd.Parameters.AddWithValue("@AQty2Unit", AQty2Unit);
                    cmd.Parameters.AddWithValue("@MasterProduct", MasterProduct);
                    cmd.Parameters.AddWithValue("@ITCLocation", ITCLocation);
                    cmd.Parameters.AddWithValue("@ITCHSCode", ITCHSCode);
                    cmd.Parameters.AddWithValue("@PolicyPara", PolicyPara);
                    cmd.Parameters.AddWithValue("@PolicyYear", PolicyYear);
                    cmd.Parameters.AddWithValue("@Loading", Loading);
                    cmd.Parameters.AddWithValue("@LodingOn", LodingOn);
                    cmd.Parameters.AddWithValue("@LoadingCurrency", LoadingCurrency);
                    cmd.Parameters.AddWithValue("@LoadingRate", LoadingRate);
                    cmd.Parameters.AddWithValue("@LoadingUnit", LoadingUnit);
                    cmd.Parameters.AddWithValue("@LoadingAmount", LoadingAmount);
                    cmd.Parameters.AddWithValue("@EximSchCode", EximSchCode);
                    cmd.Parameters.AddWithValue("@EximSchDesc", EximSchDesc);
                    cmd.Parameters.AddWithValue("@SchNoten", SchNoten);
                    cmd.Parameters.AddWithValue("@SchNotenUnit", SchNotenUnit);
                    cmd.Parameters.AddWithValue("@SchNotenDesc", SchNotenDesc);
                    cmd.Parameters.AddWithValue("@CTHNo", CTHNo);
                    cmd.Parameters.AddWithValue("@RITCNo", RITCNo);
                    cmd.Parameters.AddWithValue("@RateType", RateType);
                    cmd.Parameters.AddWithValue("@BasicDutyNotn", BasicDutyNotn);
                    cmd.Parameters.AddWithValue("@BasicDutySno", BasicDutySno);
                    cmd.Parameters.AddWithValue("@BasicDuty", BasicDuty);
                    cmd.Parameters.AddWithValue("@BasicDutyRate", BasicDutyRate);
                    cmd.Parameters.AddWithValue("@BasicDutyFlag", BasicDutyFlag);
                    cmd.Parameters.AddWithValue("@BasicDutyAmount", BasicDutyAmount);
                    cmd.Parameters.AddWithValue("@BasicDutyUnit", BasicDutyUnit);
                    cmd.Parameters.AddWithValue("@AddlNotn", AddlNotn);
                    cmd.Parameters.AddWithValue("@AddlSNo", AddlSNo);
                    cmd.Parameters.AddWithValue("@EduCessNotn", EduCessNotn);
                    cmd.Parameters.AddWithValue("@EduCessSNo", EduCessSNo);
                    cmd.Parameters.AddWithValue("@EduCessRate", EduCessRate);

                    cmd.Parameters.AddWithValue("@EduCessAmount", EduCessAmount);
                    cmd.Parameters.AddWithValue("@SHECessAmount", SHECessAmount);

                    //SHECess
                    cmd.Parameters.AddWithValue("@SHECessRate", SHECessRate);
                    //NCD
                    cmd.Parameters.AddWithValue("@NCDNotn", NCDNotn);
                    cmd.Parameters.AddWithValue("@NCDSno", NCDSno);
                    cmd.Parameters.AddWithValue("@NCDRate", NCDRate);
                    cmd.Parameters.AddWithValue("@NCDDFlag", NCDDFlag);
                    cmd.Parameters.AddWithValue("@NCDAmount", NCDAmount);
                    cmd.Parameters.AddWithValue("@NCDUnit", NCDUnit);
                    cmd.Parameters.AddWithValue("@NCDRule", NCDRule);
                    cmd.Parameters.AddWithValue("@SurNotn", SurNotn);
                    cmd.Parameters.AddWithValue("@SurSno", SurSno);
                    cmd.Parameters.AddWithValue("@SurRate", SurRate);
                    cmd.Parameters.AddWithValue("@SAPTANotn", SAPTANotn);
                    cmd.Parameters.AddWithValue("@SAPTASNo", SAPTASNo);
                    cmd.Parameters.AddWithValue("@SAPTADesc", SAPTADesc);
                    cmd.Parameters.AddWithValue("@TariffValNotn", TariffValNotn);
                    cmd.Parameters.AddWithValue("@TariffValSNo", TariffValSNo);
                    cmd.Parameters.AddWithValue("@TariffUnitQty", TariffUnitQty);
                    cmd.Parameters.AddWithValue("@TariffUnit", TariffUnit);
                    cmd.Parameters.AddWithValue("@TariffRate", TariffRate);
                    cmd.Parameters.AddWithValue("@TariffAmount", TariffAmount);
                    cmd.Parameters.AddWithValue("@CETNo", CETNo);
                    cmd.Parameters.AddWithValue("@MRPDuty", MRPDuty);
                    cmd.Parameters.AddWithValue("@MRPSNo", MRPSNo);
                    cmd.Parameters.AddWithValue("@MRP", MRP);
                    cmd.Parameters.AddWithValue("@MRPUnit", MRPUnit);
                    cmd.Parameters.AddWithValue("@MRPAbatement", MRPAbatement);
                    //CVD
                    cmd.Parameters.AddWithValue("@AddlExNotn", AddlExNotn);
                    cmd.Parameters.AddWithValue("@AddlExSlNo", AddlExSlNo);
                    cmd.Parameters.AddWithValue("@AddlExRate", AddlExRate);
                    cmd.Parameters.AddWithValue("@AddlExFlag", AddlExFlag);
                    cmd.Parameters.AddWithValue("@AddlExAmount", AddlExAmount);
                    cmd.Parameters.AddWithValue("@AddlExUnit", AddlExUnit);
                    //SAD
                    cmd.Parameters.AddWithValue("@ExCVDNotn", ExCVDNotn);
                    cmd.Parameters.AddWithValue("@ExCVDSlNo", ExCVDSlNo);
                    cmd.Parameters.AddWithValue("@EXCVDRate", EXCVDRate);

                    cmd.Parameters.AddWithValue("@ExGSIAddlDutyNotn", ExGSIAddlDutyNotn);
                    cmd.Parameters.AddWithValue("@ExGSIAddlDutySlNo", ExGSIAddlDutySlNo);
                    cmd.Parameters.AddWithValue("@ExGSIAddlDutyRate", ExGSIAddlDutyRate);
                    cmd.Parameters.AddWithValue("@ExGSIAddlDutyFlag", ExGSIAddlDutyFlag);
                    cmd.Parameters.AddWithValue("@ExGSIAddlDutyAmount", ExGSIAddlDutyAmount);
                    cmd.Parameters.AddWithValue("@ExGSIAddlDutyUnit", ExGSIAddlDutyUnit);
                    cmd.Parameters.AddWithValue("@ExSPLExDutyNotn", ExSPLExDutyNotn);
                    cmd.Parameters.AddWithValue("@ExSPLExDutySlNo", ExSPLExDutySlNo);
                    cmd.Parameters.AddWithValue("@ExSPLExDutyRate", ExSPLExDutyRate);
                    cmd.Parameters.AddWithValue("@ExSPLExDutyFlag", ExSPLExDutyFlag);
                    cmd.Parameters.AddWithValue("@ExSPLExDutyAmount", ExSPLExDutyAmount);
                    cmd.Parameters.AddWithValue("@ExSPLExDutyUnit", ExSPLExDutyUnit);
                    cmd.Parameters.AddWithValue("@ExTTAAddlDutyNotn", ExTTAAddlDutyNotn);
                    cmd.Parameters.AddWithValue("@ExTTAAddlDutySlNo", ExTTAAddlDutySlNo);
                    cmd.Parameters.AddWithValue("@ExTTAAddlDutyRate", ExTTAAddlDutyRate);
                    cmd.Parameters.AddWithValue("@ExTTAAddlDutyFlag", ExTTAAddlDutyFlag);
                    cmd.Parameters.AddWithValue("@ExTTAAddlDutyAmount", ExTTAAddlDutyAmount);
                    cmd.Parameters.AddWithValue("@ExTTAAddlDutyUnit", ExTTAAddlDutyUnit);
                    cmd.Parameters.AddWithValue("@ExHealthCessNotn", ExHealthCessNotn);
                    cmd.Parameters.AddWithValue("@ExHealthCessSlNo", ExHealthCessSlNo);
                    cmd.Parameters.AddWithValue("@ExHealthCessRate", ExHealthCessRate);
                    cmd.Parameters.AddWithValue("@ExHealthCessFlag", ExHealthCessFlag);
                    cmd.Parameters.AddWithValue("@ExHealthCessAmount", ExHealthCessAmount);
                    cmd.Parameters.AddWithValue("@ExHealthCessUnit", ExHealthCessUnit);
                    cmd.Parameters.AddWithValue("@ExCessNotn", ExCessNotn);
                    cmd.Parameters.AddWithValue("@ExCessSlNo", ExCessSlNo);
                    cmd.Parameters.AddWithValue("@ExCessRate", ExCessRate);
                    cmd.Parameters.AddWithValue("@ExCessFlag", ExCessFlag);
                    cmd.Parameters.AddWithValue("@ExCessAmount", ExCessAmount);
                    cmd.Parameters.AddWithValue("@ExCessUnit", ExCessUnit);
                    cmd.Parameters.AddWithValue("@ExSADNotn", ExSADNotn);
                    cmd.Parameters.AddWithValue("@ExSADSlno", ExSADSlno);
                    cmd.Parameters.AddWithValue("@ExSADRate", ExSADRate);
                    cmd.Parameters.AddWithValue("@GenericDesc", GenericDesc);
                    cmd.Parameters.AddWithValue("@Manufacturer", Manufacturer);
                    cmd.Parameters.AddWithValue("@Brand", Brand);
                    cmd.Parameters.AddWithValue("@Model", Model);
                    cmd.Parameters.AddWithValue("@Accessories", Accessories);
                    cmd.Parameters.AddWithValue("@EndUse", EndUse);
                    cmd.Parameters.AddWithValue("@CountryofOrigin", CountryofOrigin);
                    cmd.Parameters.AddWithValue("@ProdAmt", ProdAmt);
                    cmd.Parameters.AddWithValue("@ProdAmtRs", ProdAmtRs);
                    cmd.Parameters.AddWithValue("@Freight", Freight);
                    cmd.Parameters.AddWithValue("@Insurance", Insurance);
                    cmd.Parameters.AddWithValue("@AgencyCharge", AgencyCharge);
                    cmd.Parameters.AddWithValue("@Miscellaneous", Miscellaneous);
                    cmd.Parameters.AddWithValue("@LandingChrg", LandingChrg);
                    cmd.Parameters.AddWithValue("@AssableValue", AssableValue);
                    cmd.Parameters.AddWithValue("@TotalValue", TotalValue);
                    cmd.Parameters.AddWithValue("@SVBLdg", SVBLdg);
                    cmd.Parameters.AddWithValue("@BasDutyAmtPer", BasDutyAmtPer);
                    cmd.Parameters.AddWithValue("@BasDutyAmtQty", BasDutyAmtQty);
                    cmd.Parameters.AddWithValue("@AntiDumpDutyAmt", AntiDumpDutyAmt);
                    cmd.Parameters.AddWithValue("@SCDDutyAmt", SCDDutyAmt);
                    cmd.Parameters.AddWithValue("@SurchargeDutyAmt", SurchargeDutyAmt);
                    cmd.Parameters.AddWithValue("@AuxDutyAmt", AuxDutyAmt);
                    cmd.Parameters.AddWithValue("@TotBasicDutyAmt", TotBasicDutyAmt);
                    cmd.Parameters.AddWithValue("@CVDDutyAmtPer", CVDDutyAmtPer);
                    cmd.Parameters.AddWithValue("@CVDDutyAmtQty", CVDDutyAmtQty);
                    cmd.Parameters.AddWithValue("@ACVDDutyAmt", ACVDDutyAmt);
                    cmd.Parameters.AddWithValue("@ACS2DutyAmt", ACS2DutyAmt);
                    cmd.Parameters.AddWithValue("@SCVDDutyAmt", SCVDDutyAmt);
                    cmd.Parameters.AddWithValue("@CESSDutyAmt", CESSDutyAmt);
                    cmd.Parameters.AddWithValue("@TotalCVDAmt", TotalCVDAmt);
                    cmd.Parameters.AddWithValue("@SADAmt", SADAmt);
                    cmd.Parameters.AddWithValue("@ExEduCessNotn", ExEduCessNotn);
                    cmd.Parameters.AddWithValue("@ExEduCessSlNo", ExEduCessSlNo);
                    cmd.Parameters.AddWithValue("@TotalDutyAmt", TotalDutyAmt);
                    cmd.Parameters.AddWithValue("@CreatedBy", CreatedBy);
                    cmd.Parameters.AddWithValue("@CreatedDate", CreatedDate);
                    cmd.Parameters.AddWithValue("@ModifiedBy", CreatedBy);
                    cmd.Parameters.AddWithValue("@ModifiedDate", ModifiedDate);

                    var Result = cmd.ExecuteNonQuery();
                    if (Result == 1)
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "redirect", "alert('Saved Successfully'); window.location.href='frmDataMigrationJob.aspx';", true);
                        btnJobCreation.Visible = true;
                        //btnShipment.Visible = true;
                        //btnShipmentCon.Visible = true;
                        //btnInvoice.Visible = true;
                        //btnProduct.Visible = true;
                    }
                    conn.Close();
                    i++;
                }
                catch (Exception ex)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "redirect", "alert('Error in Product'); ", true);
                }
            }
        }
    }
}