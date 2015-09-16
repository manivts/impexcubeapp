using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using System.Text;
using System.IO;
namespace ImpexCube
{
    public partial class frmsb : System.Web.UI.Page
    {
        string strcon = (string)ConfigurationManager.ConnectionStrings["Constr"].ConnectionString;
        static DataSet dsrun = new DataSet();
        static DataRowView dvrun = null;
    
        private string FILEPATH = "";
        private string filePath = "";
        StringBuilder sbrunfile = new StringBuilder();
     
        VTS.ImpexCube.Business.CommonBL obj1 = new VTS.ImpexCube.Business.CommonBL();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                JobNo();
                ddlJobNo.SelectedValue = (string)Session["JobNo"];
            }
        }
        public void JobNo()
        {
            DataSet dt = obj1.GetExportJobNo();
            ddlJobNo.DataSource = dt;
            ddlJobNo.DataValueField = "JobNo";
            ddlJobNo.DataTextField = "JobNo";
            ddlJobNo.DataBind();
        }
        public void sbfile()
        {
            try
            {
                SqlConnection conns = new SqlConnection(strcon);
                conns.Open();

                string querys = "select HText,TText,zz,SenderID,ReceiverID,VersionNo,fType,MessageID from tbl_FileDetails";

                SqlDataAdapter sda = new SqlDataAdapter(querys, conns);
                sda.Fill(dsrun, "running");
                conns.Close();
                if (dsrun.Tables["running"].Rows.Count != 0)
                {
                    dvrun = dsrun.Tables["running"].DefaultView[0];
                }
                string jno = ddlJobNo.SelectedValue;//(string)Session["JobNo"];

                string HText = dvrun["HText"].ToString();
                string TText = dvrun["TText"].ToString();
                string zz = dvrun["zz"].ToString();

                string SenderID = dvrun["SenderID"].ToString();
                 sbrunfile.AppendLine("Sender ID : " + SenderID);

                string ReceiverID = dvrun["ReceiverID"].ToString();
                  sbrunfile.AppendLine("Receiver ID : " + ReceiverID);


                string VersionNo = dvrun["VersionNo"].ToString();
                sbrunfile.AppendLine("Version No : " + VersionNo);


                string fType = dvrun["fType"].ToString();

                string MessageID = dvrun["MessageID"].ToString();

                string SequenceId = jno;//jno.Substring(13, 6);
                  sbrunfile.AppendLine("Job No : " + SequenceId);


                string jdate = DateTime.Now.Date.ToString("dd/MM/yyyy");
                   sbrunfile.AppendLine("Job Date : " + jdate);


                string jTime = DateTime.Now.ToString("HH:mm");
                 sbrunfile.AppendLine("Job Time : " + jTime);
                 txtBeFile.Text = sbrunfile.ToString();

                GetFSIO(jno, SenderID, ReceiverID, VersionNo, fType, MessageID, SequenceId, HText, TText, zz);

                sbrunfile.AppendLine("Successfully Created SB File.");
                txtBeFile.Text = sbrunfile.ToString();

               
            }
            catch
            {
            }
        }

        protected void GetFSIO(string jno, string SenderID, string ReceiverID, string VersionNo, string fType, string MessageID, string SequenceId, string HText, string TText, string zz)
        {
            try
            {
                string PartyNameDirectory = "";
                string Messagetype = "F";
                string jobdate = "";
                string sbNo = "";
                string sbDate = "";
               
                int unicode = 28;
                string impexpcode = "";
                string Expbranchsrno = "";
                string Expname = "";
                string expadd1 = "";
                string expadd2 = "";
                string expcity = "";
                string expstate = "";
                string expin = "";
                string typeofexporter = "";
                string exporterclass = "";
                string stateoforiginexporter = "";
                string authdealercode = "";
                string epzcode = "";
                string consigneename = "";
                string consadd1 = "";
                string consadd2 = "";
                string consadd3 = "";
                string consadd4 = "";
                string concntry = "";
                string categoryofnfei = "";
                string rbiwaivernumber = "";
                string rbiwaiverdate = "";
                string portofloading = "";
                string portoffinaldest = "";
                string cntryfinaldest = "";
                string cntrydischarge = "";
                string portdischarge = "";
                string sealype = "";
                string naturecargo = "";
                string gweight = "";
                string netweight = "";
                string unitofmeasure = "";
                string totalnumberpackage = "";
                string marksnumber = "";
                string noofloosepckts = "";
                string noofcntainer = "";
                string mawbno = "";
                string hawbno = "";
                string amttype = "";
                string amentno = "";
                string amentdate = "";
                string chalicence = "";
                char character = (char)unicode;
                string Asc28 = character.ToString();

                int unicode1 = 10;
                char character1 = (char)unicode1;
                string nLine = character1.ToString();
                string jdate = DateTime.Now.Date.ToString("dd/MM/yyyy");
                string jTime = DateTime.Now.ToString("HH:mm");
                string sb = "<TABLE>SB";
                string invoice = "<TABLE>INVOICE";
                string exch = "<TABLE>EXCHANGE";
                string items = "<TABLE>ITEMS";
                string itemaccess = "<TABLE>ITEMACCESS";
                string thirdparty = "<TABLE>THIRDPARTY";
                string cess = "<TABLE>CESS";
                string dbk = "<TABLE>DBK";
                string ITEMRAWMTRL = "<TABLE>ITEMRAWMTRL";
                string DEPB = "<TABLE>DEPB";
                string DEPBPARENT = "<TABLE>DEPBPARENT";
                string LICENCE = "<TABLE>LICENCE";
                string DFIA = "<TABLE>DFIA";
                string JOBWORK = "<TABLE>JOBWORK";
                string AR4 = "<TABLE>AR4";
                string PACKINGLIST = "<TABLE>PACKINGLIST";
                string ROTATION = "<TABLE>ROTATION";
                string EOU = "<TABLE>EOU";
                string STUFF = "<TABLE>STUFF";
                string CONTAINER = "<TABLE>CONTAINER";
                string CARGOBACK = "<TABLE>CARGOBACK";
                string PCKGBACK = "<TABLE>PCKGBACK";
                string CONTAINERBACK = "<TABLE>CONTAINERBACK";
                string AMENDHISTORY = "<TABLE>AMENDHISTORY";
                string endsb = "<END-SB>";
                SqlConnection conn1 = new SqlConnection(strcon);
                conn1.Open();
                string sqlQuery1 = "select * from View_ExpJobDetails where jobNo='" + jno + "'";

                SqlDataAdapter da1 = new SqlDataAdapter(sqlQuery1, conn1);
                DataSet ds1 = new DataSet();
                da1.Fill(ds1, "Jobs");
                conn1.Close();
                if (ds1.Tables["Jobs"].Rows.Count != 0)
                {
                    DataRowView row = ds1.Tables["Jobs"].DefaultView[0];
                     chalicence = "";
                     sbNo = row["SBNo"].ToString();
                     sbDate = row["SBDate"].ToString();
                     impexpcode = row["IECodeNo"].ToString();
                     Expbranchsrno = row["BranchSno"].ToString();
                     Expname = row["ExporterName"].ToString();
                     expadd1 = row["ExporterAddress"].ToString();
                     expadd2 = row["ExporterAddress1"].ToString();
                     expcity = "";
                     expstate = row["StateProvince"].ToString();
                     expin = "";
                     typeofexporter = row["ExporterType"].ToString();
                     exporterclass = "";
                     stateoforiginexporter = row["StateOfOrigin"].ToString();
                     authdealercode = "";
                     epzcode = row["EPZCode"].ToString();
                     consigneename = row["Consignee"].ToString();
                     consadd1 = row["ConsigneeAddress"].ToString();
                     consadd2 = row["ConsigneeAddress1"].ToString();
                     consadd3 = "";
                     consadd4 = "";
                     concntry = row["ConsigneeCountry"].ToString();
                     categoryofnfei = "";
                     rbiwaivernumber = row["RBIWaiverNo"].ToString();
                     rbiwaiverdate = row["RBIWavierDate"].ToString();
                     portofloading = row["DischargePort"].ToString();
                     portoffinaldest = row["DestinationPort"].ToString();
                     cntryfinaldest = row["DestinationCountry"].ToString();
                     cntrydischarge = row["DischargeCountry"].ToString();
                     portdischarge = row["DischargePort"].ToString();
                     sealype = row["SealType"].ToString();
                     naturecargo = row["NatureofCargo"].ToString();
                     gweight = row["GrossWeight"].ToString();
                     netweight = row["NetWeight"].ToString();
                     unitofmeasure = row["NetWeightUnit"].ToString();
                     totalnumberpackage = row["TotalNoofPkgs"].ToString();
                     marksnumber = row["MarksNos"].ToString();
                     noofloosepckts = row["LoosePkgs"].ToString();
                     noofcntainer = row["NoofContainers"].ToString();
                     mawbno = row["MBLNo"].ToString();
                     hawbno = row["HBLNo"].ToString();
                     amttype ="";
                     amentno = "";
                     amentdate = "";
                }
                string file = string.Empty;
                string mn = DateTime.Now.ToString("MM");
                string dd = DateTime.Now.ToString("dd");
                string datetime = DateTime.Now.Year.ToString() + mn + dd + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString();
                string serverPaths = ""; ;
                string[] a = Environment.GetLogicalDrives();
                for (int i = 0; i < a.Count(); i++)
                {
                   
                   // serverPaths = "E:\\MMK\\Application\\PIPL Application\\Impex\\ImpexCube C#\\ImpexCube\\ImpexCube\\TempFile\\";
                    // serverPaths = "   E:\\Kishor\\ImpexCube\\ImpexCube\\TempFile\\";
                    serverPaths = "\\SERVER-PC\\ImpexcubePIPL\\TempFile\\";
                    //serverPaths = "\\VTSLAP1\\Impexcube\\TempFile\\";
                
                 
                }
                serverPaths = serverPaths + "SBFile";
                string dATE = mn + dd + DateTime.Now.Year.ToString();

                if (Directory.Exists(serverPaths))
                {
                    PartyNameDirectory = serverPaths + "\\" + "FATFile";
                    if (Directory.Exists(PartyNameDirectory))
                    {
                        file = PartyNameDirectory + "/" + SequenceId + ".sb";
                    }
                    else
                    {
                        Directory.CreateDirectory(PartyNameDirectory);
                        file = PartyNameDirectory + "/" + SequenceId + ".sb";
                    }

                }
                else
                {
                    Directory.CreateDirectory(serverPaths);
                    PartyNameDirectory = serverPaths + "\\" + "FATFile";
                    if (Directory.Exists(PartyNameDirectory))
                    {
                        file = PartyNameDirectory + "\\" + "EXP-" + "\\" + "I" + "00" + SequenceId + ".sb"; ;
                    }
                    else
                    {
                        Directory.CreateDirectory(PartyNameDirectory);
                        file = PartyNameDirectory + "\\" + "EXP-" + "\\" + "I" + "00" + SequenceId + ".sb"; ;
                    }

                }

                FILEPATH = file;


                FileStream fs = new FileStream(@file, FileMode.OpenOrCreate, FileAccess.ReadWrite);
                StreamWriter tw = new StreamWriter(fs);


                filePath = file;
                tw.Write(HText); tw.Write(Asc28); tw.Write(zz); tw.Write(Asc28); tw.Write(SenderID); tw.Write(Asc28); tw.Write(zz); tw.Write(Asc28);
                tw.Write(ReceiverID); tw.Write(Asc28); tw.Write(VersionNo); tw.Write(Asc28); tw.Write(fType); tw.Write(Asc28); tw.Write(Asc28);
                tw.Write(MessageID); tw.Write(Asc28); tw.Write(SequenceId); tw.Write(Asc28); tw.Write(jdate); tw.Write(Asc28); tw.Write(jTime);
                tw.Write(nLine);
                tw.Write(sbNo );
                tw.Write(nLine);
                tw.Write(Messagetype); tw.Write(Asc28); tw.Write(ReceiverID); tw.Write(Asc28); tw.Write(SequenceId); tw.Write(Asc28); tw.Write(jobdate); tw.Write(Asc28); tw.Write(sbNo); tw.Write(Asc28);
                tw.Write(sbDate); tw.Write(Asc28); tw.Write(chalicence); tw.Write(Asc28); tw.Write(impexpcode); tw.Write(Asc28); tw.Write(Expbranchsrno); tw.Write(Asc28); tw.Write(Expname); tw.Write(Asc28); tw.Write(expadd1); tw.Write(Asc28);
                tw.Write(expadd2); tw.Write(Asc28); tw.Write(expcity); tw.Write(Asc28); tw.Write(expstate); tw.Write(Asc28); tw.Write(expin); tw.Write(Asc28); tw.Write(typeofexporter); tw.Write(Asc28);
                tw.Write(exporterclass); tw.Write(Asc28); tw.Write(stateoforiginexporter); tw.Write(Asc28); tw.Write(authdealercode); tw.Write(Asc28); tw.Write(epzcode); tw.Write(Asc28); tw.Write(consigneename); tw.Write(Asc28);
                tw.Write(consadd1); tw.Write(Asc28); tw.Write(consadd2); tw.Write(Asc28); tw.Write(consadd3); tw.Write(Asc28); tw.Write(consadd4); tw.Write(Asc28); tw.Write(concntry); tw.Write(Asc28);
                tw.Write(categoryofnfei); tw.Write(Asc28); tw.Write(rbiwaivernumber); tw.Write(Asc28); tw.Write(rbiwaiverdate); tw.Write(Asc28); tw.Write(portofloading); tw.Write(Asc28); tw.Write(portoffinaldest); tw.Write(Asc28);
                tw.Write(cntryfinaldest); tw.Write(Asc28); tw.Write(cntrydischarge); tw.Write(Asc28); tw.Write(portdischarge); tw.Write(Asc28); tw.Write(sealype); tw.Write(Asc28); tw.Write(naturecargo ); tw.Write(Asc28);
                tw.Write(gweight); tw.Write(Asc28); tw.Write(netweight); tw.Write(Asc28); tw.Write(unitofmeasure); tw.Write(Asc28); tw.Write(totalnumberpackage); tw.Write(Asc28); tw.Write(marksnumber); tw.Write(Asc28);
                tw.Write(noofloosepckts); tw.Write(Asc28); tw.Write(noofcntainer); tw.Write(Asc28); tw.Write(mawbno); tw.Write(Asc28); tw.Write(hawbno); tw.Write(Asc28); tw.Write(amttype); tw.Write(Asc28);
                tw.Write(amentno); tw.Write(Asc28); tw.Write(amentdate); tw.Write(Asc28);
                tw.Write(nLine);

                 SqlConnection conn2 = new SqlConnection(strcon);
                conn2.Open();
                string sqlQuery2 = "select * from View_ExpJobDetails  where jobNo='" + jno + "'";

                SqlDataAdapter da2 = new SqlDataAdapter(sqlQuery2, conn2);
                DataSet ds2 = new DataSet();
                da2.Fill(ds2, "Jobs");
                DataTable dt2 = ds2.Tables["Jobs"];
                conn2.Close();
                if (ds2.Tables["Jobs"].Rows.Count != 0)
                {
                    DataRowView row = ds2.Tables["Jobs"].DefaultView[0];
                    string invsrno = row["InvoiceNo"].ToString();
                    string actualinvno = row["InvoiceNo"].ToString();
                    string invdate = row["InvoiceDate"].ToString();
                    string invcur = row["Currency"].ToString();
                    string natureofcontract = "";
                    string buyername = row["Buyer"].ToString();
                    string buyeradd1 = row["BuyerAddress"].ToString();
                    string buyeradd2 = row["BuyerAddress1"].ToString();
                    string buyeradd3 = "";
                    string buyeradd4 = "";
                    string freightcur = row["FreightCurrency"].ToString();
                    string freightamt = row["FreightAmount"].ToString();
                    string insurancerate = row["InsuranceRate"].ToString();
                    string insurancecur = row["InsuranceCurrency"].ToString();
                    string insuranceamount = row["InsuranceAmount"].ToString();
                    string commisionrate = row["CommissionRate"].ToString();
                    string commisioncur = row["CommissionCurrency"].ToString();
                    string commisionamount = row["CommissionAmount"].ToString();
                    string discountfob = "";
                    string discountcur = row["PackingFOBChargesCurrency"].ToString();
                    string discountamt ="";
                    string otherdeductions ="";
                    string otherdeductioncur = row["OtherDeductionCurrency"].ToString();
                    string otherdeductionamt = row["OtherDeductionAmount"].ToString();
                    string addfreight ="";
                    string packingcharge ="";
                    string exportercontractnumber = row["ExportContractNo"].ToString();
                    string natureofpayment = row["NatureOfPayment"].ToString();
                    string periodofpaymentdays = row["PaymentPeriod"].ToString();

                    tw.Write(Messagetype); tw.Write(Asc28); tw.Write(ReceiverID); tw.Write(Asc28); tw.Write(SequenceId); tw.Write(Asc28); tw.Write(jobdate); tw.Write(Asc28); tw.Write(sbNo); tw.Write(Asc28);
                    tw.Write(sbDate); tw.Write(Asc28); tw.Write(invsrno); tw.Write(Asc28); tw.Write(actualinvno); tw.Write(Asc28); tw.Write(invdate); tw.Write(Asc28); tw.Write(invcur); tw.Write(Asc28);
                    tw.Write(natureofcontract); tw.Write(Asc28); tw.Write(buyername); tw.Write(Asc28); tw.Write(buyeradd1); tw.Write(Asc28); tw.Write(buyeradd2); tw.Write(Asc28); tw.Write(buyeradd3); tw.Write(Asc28);
                    tw.Write(buyeradd4); tw.Write(Asc28); tw.Write(freightcur); tw.Write(Asc28); tw.Write(freightamt); tw.Write(Asc28); tw.Write(insurancerate); tw.Write(Asc28); tw.Write(insurancecur); tw.Write(Asc28); 
                    tw.Write(insuranceamount); tw.Write(Asc28);tw.Write(commisionrate); tw.Write(Asc28); tw.Write(commisioncur); tw.Write(Asc28); tw.Write(commisionamount); tw.Write(Asc28); tw.Write(discountfob); tw.Write(Asc28);
                    tw.Write(discountcur); tw.Write(Asc28); tw.Write(discountamt); tw.Write(Asc28); tw.Write(otherdeductions); tw.Write(Asc28); tw.Write(otherdeductioncur); tw.Write(Asc28); tw.Write(otherdeductionamt); tw.Write(Asc28);
                    tw.Write(addfreight); tw.Write(Asc28); tw.Write(packingcharge); tw.Write(Asc28); tw.Write(exportercontractnumber); tw.Write(Asc28); tw.Write(natureofpayment); tw.Write(Asc28); tw.Write(periodofpaymentdays); tw.Write(Asc28); 
                    tw.Write(amttype ); tw.Write(Asc28); tw.Write(amentno ); tw.Write(Asc28); tw.Write(amentdate ); tw.Write(Asc28);
                }
                tw.Write(nLine);
                //Exchange details
                 SqlConnection conn3 = new SqlConnection(strcon);
                conn3.Open();
                string sqlQuery3 = "select * from E_T_Invoice  where jobNo='" + jno + "'";

                SqlDataAdapter da3 = new SqlDataAdapter(sqlQuery3, conn3);
                DataSet ds3 = new DataSet();
                da3.Fill(ds3, "Jobs");
                DataTable dt3 = ds3.Tables["Jobs"];
                conn3.Close();
                if (ds3.Tables["Jobs"].Rows.Count != 0)
                {
                    DataRowView row = ds3.Tables["Jobs"].DefaultView[0];
                    string invcurcode = "";
                    string currencyname = row["Currency"].ToString();
                    string unitinrs = "";
                    string rate = row["CurrencyRate"].ToString();
                    string effectivedate = "";
                    string standardcurornot ="";

                    tw.Write(Messagetype); tw.Write(Asc28); tw.Write(ReceiverID); tw.Write(Asc28); tw.Write(SequenceId); tw.Write(Asc28); tw.Write(jobdate); tw.Write(Asc28); tw.Write(sbNo); tw.Write(Asc28);
                    tw.Write(sbDate); tw.Write(Asc28); tw.Write(invcurcode); tw.Write(Asc28); tw.Write(currencyname); tw.Write(Asc28); tw.Write(unitinrs); tw.Write(Asc28); tw.Write(rate); tw.Write(Asc28);
                    tw.Write(effectivedate); tw.Write(Asc28); tw.Write(standardcurornot); tw.Write(Asc28); tw.Write(amttype); tw.Write(Asc28); tw.Write(amentno); tw.Write(Asc28); tw.Write(amentdate); tw.Write(Asc28);
                }
                tw.Write(nLine);
                //ITEM DETAILS
                 SqlConnection conn4 = new SqlConnection(strcon);
                conn4.Open();
                string sqlQuery4 = "select * from View_ExpJobDetails  where jobNo='" + jno + "'";

                SqlDataAdapter da4 = new SqlDataAdapter(sqlQuery4, conn4);
                DataSet ds4 = new DataSet();
                da4.Fill(ds4, "Jobs");
                DataTable dt4 = ds4.Tables["Jobs"];
                conn4.Close();
                if (ds4.Tables["Jobs"].Rows.Count != 0)
                {
                    DataRowView row = ds4.Tables["Jobs"].DefaultView[0];
                    string invsrno = row["InvoiceSNo"].ToString();
                    string itemsrnoininvoice = row["ItemSNo"].ToString();
                    string schemecode = "";
                    string ritccodeoritchscode = row["RITCCode"].ToString();
                    string desgoods1 = row["Description"].ToString();
                    string desgoods2 = row["Description"].ToString();
                    string desgoods3 = row["Description"].ToString();
                    string uomesure ="";
                    string qty = row["Quantity"].ToString();
                    string unitprice = row["UnitPrice"].ToString();
                    string unitrate ="";
                    string noofunit = "";
                    string presentmarketvalue = "";
                    string jobwrknotifynno ="";
                    string thirdpartys = row["Manufacturer"].ToString();
                    string rewarditem = row["CurrencyRate"].ToString();

                    tw.Write(Messagetype); tw.Write(Asc28); tw.Write(ReceiverID); tw.Write(Asc28); tw.Write(SequenceId); tw.Write(Asc28); tw.Write(jobdate); tw.Write(Asc28); tw.Write(sbNo); tw.Write(Asc28);
                    tw.Write(sbDate); tw.Write(Asc28); tw.Write(invsrno); tw.Write(Asc28); tw.Write(itemsrnoininvoice); tw.Write(Asc28); tw.Write(schemecode); tw.Write(Asc28); tw.Write(ritccodeoritchscode); tw.Write(Asc28);
                    tw.Write(desgoods1); tw.Write(Asc28); tw.Write(desgoods2); tw.Write(Asc28); tw.Write(desgoods3); tw.Write(Asc28); tw.Write(uomesure); tw.Write(Asc28); tw.Write(qty); tw.Write(Asc28);
                    tw.Write(unitprice); tw.Write(Asc28); tw.Write(unitrate); tw.Write(Asc28); tw.Write(noofunit); tw.Write(Asc28); tw.Write(presentmarketvalue); tw.Write(Asc28); tw.Write(jobwrknotifynno); tw.Write(Asc28);
                    tw.Write(thirdpartys); tw.Write(Asc28); tw.Write(rewarditem); tw.Write(Asc28); tw.Write(amttype); tw.Write(Asc28); tw.Write(amentno); tw.Write(Asc28); tw.Write(amentdate); tw.Write(Asc28);
                }
                tw.Write(nLine);

                //ITEMACCESS
                     SqlConnection conn5 = new SqlConnection(strcon);
                conn5.Open();
                string sqlQuery5 = "select * from E_T_Product  where jobNo='" + jno + "'";

                SqlDataAdapter da5 = new SqlDataAdapter(sqlQuery5, conn5);
                DataSet ds5 = new DataSet();
                da5.Fill(ds5, "Jobs");
                DataTable dt5 = ds5.Tables["Jobs"];
                conn5.Close();
                if (ds5.Tables["Jobs"].Rows.Count != 0)
                {
                    DataRowView row = ds5.Tables["Jobs"].DefaultView[0];
                    string invsrno = row["InvoiceSNo"].ToString();
                    string itemsrnoininvoice = row["ItemSNo"].ToString();
                    string descriptionofaccess = row["Description"].ToString();
                    tw.Write(Messagetype); tw.Write(Asc28); tw.Write(ReceiverID); tw.Write(Asc28); tw.Write(SequenceId); tw.Write(Asc28); tw.Write(jobdate); tw.Write(Asc28); tw.Write(sbNo); tw.Write(Asc28);
                    tw.Write(sbDate); tw.Write(Asc28); tw.Write(invsrno); tw.Write(Asc28); tw.Write(itemsrnoininvoice); tw.Write(Asc28); tw.Write(descriptionofaccess); tw.Write(Asc28); 
 
                }
                tw.Write(nLine);
                //Third Party
               SqlConnection conn6 = new SqlConnection(strcon);
                conn6.Open();
                string sqlQuery6 = "select * from View_ExpJobDetails  where jobNo='" + jno + "'";

                SqlDataAdapter da6 = new SqlDataAdapter(sqlQuery6, conn6);
                DataSet ds6 = new DataSet();
                da6.Fill(ds6, "Jobs");
                DataTable dt6 = ds6.Tables["Jobs"];
                conn6.Close();
                if (ds6.Tables["Jobs"].Rows.Count != 0)
                {
                    DataRowView row = ds6.Tables["Jobs"].DefaultView[0];
                    string invsrno = row["InvoiceSNo"].ToString();
                    string itemsrnoininv = row["ItemSNo"].ToString();
                    string iec = row["IECodeNo"].ToString();
                    string branchserialno = row["BranchSno"].ToString();
                    string exportername = row["ExporterName"].ToString();
                    string exporteradd1 = row["ExporterAddress"].ToString();
                    string exporteradd2 = row["ExporterAddress1"].ToString();
                    string exportercity = "";
                    string exporterpin = "";

                    tw.Write(Messagetype); tw.Write(Asc28); tw.Write(ReceiverID); tw.Write(Asc28); tw.Write(SequenceId); tw.Write(Asc28); tw.Write(jobdate); tw.Write(Asc28); tw.Write(sbNo); tw.Write(Asc28);
                    tw.Write(sbDate); tw.Write(Asc28); tw.Write(invsrno); tw.Write(Asc28); tw.Write(itemsrnoininv); tw.Write(Asc28); tw.Write(iec); tw.Write(Asc28); tw.Write(branchserialno); tw.Write(Asc28);
                    tw.Write(exportername); tw.Write(Asc28); tw.Write(exporteradd1); tw.Write(Asc28); tw.Write(exporteradd2); tw.Write(Asc28); tw.Write(exportercity); tw.Write(Asc28); tw.Write(exporterpin); tw.Write(Asc28);
                    tw.Write(amttype); tw.Write(Asc28); tw.Write(amentno); tw.Write(Asc28); tw.Write(amentdate); tw.Write(Asc28); 


                }
                tw.Write(nLine);
                //CESS
                 SqlConnection conn7 = new SqlConnection(strcon);
                conn7.Open();
                string sqlQuery7 = "select * from View_ExpJobDetails  where jobNo='" + jno + "'";

                SqlDataAdapter da7 = new SqlDataAdapter(sqlQuery7, conn7);
                DataSet ds7 = new DataSet();
                da7.Fill(ds7, "Jobs");
                DataTable dt7 = ds7.Tables["Jobs"];
                conn7.Close();
                if (ds7.Tables["Jobs"].Rows.Count != 0)
                {
                    DataRowView row = ds7.Tables["Jobs"].DefaultView[0];
                    string invsrno = row["InvoiceSNo"].ToString();
                    string itemsrnoininv = row["ItemSNo"].ToString();
                    string SRNO = "";
                    string cessactcodeoritmsrnoinexporttariff = "";
                    string qty = row["Quantity"].ToString();

                    tw.Write(Messagetype); tw.Write(Asc28); tw.Write(ReceiverID); tw.Write(Asc28); tw.Write(SequenceId); tw.Write(Asc28); tw.Write(jobdate); tw.Write(Asc28); tw.Write(sbNo); tw.Write(Asc28);
                    tw.Write(sbDate); tw.Write(Asc28); tw.Write(invsrno); tw.Write(Asc28); tw.Write(itemsrnoininv); tw.Write(Asc28); tw.Write(SRNO); tw.Write(Asc28); tw.Write(cessactcodeoritmsrnoinexporttariff); tw.Write(Asc28);
                    tw.Write(qty); tw.Write(Asc28); tw.Write(amttype); tw.Write(Asc28); tw.Write(amentno); tw.Write(Asc28); tw.Write(amentdate); tw.Write(Asc28);

                }
                tw.Write(nLine);
                //DBK
                SqlConnection conn8 = new SqlConnection(strcon);
                conn8.Open();
                string sqlQuery8 = "select * from View_ExpJobDetails  where jobNo='" + jno + "'";

                SqlDataAdapter da8 = new SqlDataAdapter(sqlQuery8, conn8);
                DataSet ds8 = new DataSet();
                da8.Fill(ds8, "Jobs");
                DataTable dt8 = ds8.Tables["Jobs"];
                conn8.Close();
                if (ds8.Tables["Jobs"].Rows.Count != 0)
                {
                    DataRowView row = ds8.Tables["Jobs"].DefaultView[0];
                    string invsrno = row["InvoiceSNo"].ToString();
                    string itemsrnoininv = row["ItemSNo"].ToString();
                    string dbkschedulednumber = "";
                    string drawbackqty = "";
                    tw.Write(Messagetype); tw.Write(Asc28); tw.Write(ReceiverID); tw.Write(Asc28); tw.Write(SequenceId); tw.Write(Asc28); tw.Write(jobdate); tw.Write(Asc28); tw.Write(sbNo); tw.Write(Asc28);
                    tw.Write(sbDate); tw.Write(Asc28); tw.Write(invsrno); tw.Write(Asc28); tw.Write(itemsrnoininv); tw.Write(Asc28); tw.Write(dbkschedulednumber); tw.Write(Asc28); tw.Write(drawbackqty); tw.Write(Asc28);
                    tw.Write(amttype); tw.Write(Asc28); tw.Write(amentno); tw.Write(Asc28); tw.Write(amentdate); tw.Write(Asc28);
                }
                tw.Write(nLine);
                //ITEMRAWMTRL
                SqlConnection conn9 = new SqlConnection(strcon);
                conn9.Open();
                string sqlQuery9 = "select * from View_ExpJobDetails  where jobNo='" + jno + "'";

                SqlDataAdapter da9 = new SqlDataAdapter(sqlQuery9, conn9);
                DataSet ds9 = new DataSet();
                da9.Fill(ds9, "Jobs");
                DataTable dt9 = ds9.Tables["Jobs"];
                conn9.Close();
                if (ds9.Tables["Jobs"].Rows.Count != 0)
                {
                    DataRowView row = ds9.Tables["Jobs"].DefaultView[0];
                    string invsrno = row["InvoiceSNo"].ToString();
                    string itemsrnoininv = row["ItemSNo"].ToString();
                    string rawmaterialcode = "";
                    string qty = row["Quantity"].ToString();
                    tw.Write(Messagetype); tw.Write(Asc28); tw.Write(ReceiverID); tw.Write(Asc28); tw.Write(SequenceId); tw.Write(Asc28); tw.Write(jobdate); tw.Write(Asc28); tw.Write(sbNo); tw.Write(Asc28);
                    tw.Write(sbDate); tw.Write(Asc28); tw.Write(invsrno); tw.Write(Asc28); tw.Write(itemsrnoininv); tw.Write(Asc28); tw.Write(rawmaterialcode); tw.Write(Asc28); tw.Write(qty); tw.Write(Asc28);
                    tw.Write(amttype); tw.Write(Asc28); tw.Write(amentno); tw.Write(Asc28); tw.Write(amentdate); tw.Write(Asc28);
                }
                tw.Write(nLine);

                //DEPB
                SqlConnection conn10 = new SqlConnection(strcon);
                conn10.Open();
                string sqlQuery10 = "select * from View_ExpJobDetails  where jobNo='" + jno + "'";

                SqlDataAdapter da10 = new SqlDataAdapter(sqlQuery10, conn10);
                DataSet ds10 = new DataSet();
                da10.Fill(ds10, "Jobs");
                DataTable dt10 = ds10.Tables["Jobs"];
                conn10.Close();
                if (ds10.Tables["Jobs"].Rows.Count != 0)
                {
                    DataRowView row = ds10.Tables["Jobs"].DefaultView[0];
                    string invsrno = row["InvoiceSNo"].ToString();
                    string itemsrnoininv = row["ItemSNo"].ToString();
                    string groupcode = "";
                    string itemcode = "";
                    string qty = row["Quantity"].ToString();
                    tw.Write(Messagetype); tw.Write(Asc28); tw.Write(ReceiverID); tw.Write(Asc28); tw.Write(SequenceId); tw.Write(Asc28); tw.Write(jobdate); tw.Write(Asc28); tw.Write(sbNo); tw.Write(Asc28);
                    tw.Write(sbDate); tw.Write(Asc28); tw.Write(invsrno); tw.Write(Asc28); tw.Write(itemsrnoininv); tw.Write(Asc28); tw.Write(groupcode); tw.Write(Asc28); tw.Write(itemcode); tw.Write(Asc28);
                    tw.Write(qty); tw.Write(Asc28); tw.Write(amttype); tw.Write(Asc28); tw.Write(amentno); tw.Write(Asc28); tw.Write(amentdate); tw.Write(Asc28);
                }
                tw.Write(nLine);

                //DEPBPARENT
                SqlConnection conn11 = new SqlConnection(strcon);
                conn11.Open();
                string sqlQuery11 = "select * from View_ExpJobDetails  where jobNo='" + jno + "'";

                SqlDataAdapter da11 = new SqlDataAdapter(sqlQuery11, conn11);
                DataSet ds11 = new DataSet();
                da11.Fill(ds11, "Jobs");
                DataTable dt11 = ds11.Tables["Jobs"];
                conn11.Close();
                if (ds11.Tables["Jobs"].Rows.Count != 0)
                {
                    DataRowView row = ds11.Tables["Jobs"].DefaultView[0];
                    string invsrno = row["InvoiceSNo"].ToString();
                    string itemsrnoininv = row["ItemSNo"].ToString();
                    string srno = "";
                    string groupcode = "";
                    string itemcode = "";
                    string qty = row["Quantity"].ToString();
                    string unitqtycode = "";
                    string qtypercentage = "";
                    tw.Write(Messagetype); tw.Write(Asc28); tw.Write(ReceiverID); tw.Write(Asc28); tw.Write(SequenceId); tw.Write(Asc28); tw.Write(jobdate); tw.Write(Asc28); tw.Write(sbNo); tw.Write(Asc28);
                    tw.Write(sbDate); tw.Write(Asc28); tw.Write(invsrno); tw.Write(Asc28); tw.Write(itemsrnoininv); tw.Write(Asc28); tw.Write(srno); tw.Write(Asc28); tw.Write(groupcode); tw.Write(Asc28);
                    tw.Write(itemcode); tw.Write(Asc28); tw.Write(qty); tw.Write(Asc28); tw.Write(unitqtycode); tw.Write(Asc28); tw.Write(qtypercentage); tw.Write(Asc28); tw.Write(amttype); tw.Write(Asc28);
                    tw.Write(amentno); tw.Write(Asc28); tw.Write(amentdate); tw.Write(Asc28);
                } tw.Write(nLine);
                //>LICENCE
                SqlConnection conn12 = new SqlConnection(strcon);
                conn12.Open();
                string sqlQuery12 = "select * from View_ExpJobDetails  where jobNo='" + jno + "'";

                SqlDataAdapter da12 = new SqlDataAdapter(sqlQuery12, conn12);
                DataSet ds12 = new DataSet();
                da12.Fill(ds12, "Jobs");
                DataTable dt12 = ds12.Tables["Jobs"];
                conn12.Close();
                if (ds12.Tables["Jobs"].Rows.Count != 0)
                {
                    DataRowView row = ds12.Tables["Jobs"].DefaultView[0];
                    string invsrno = row["InvoiceSNo"].ToString();
                    string itemsrnoininv = row["ItemSNo"].ToString();
                    string srno = "";
                    string registerationnumber = "";
                    string registerationdate = "";
                    string itemsrnoinpartE = "";
                    string itemsrnoinpartC = "";
                    string qty = row["Quantity"].ToString(); 
                    string Exportqty = "";
                    string whetherindigenous = "";
                    tw.Write(Messagetype); tw.Write(Asc28); tw.Write(ReceiverID); tw.Write(Asc28); tw.Write(SequenceId); tw.Write(Asc28); tw.Write(jobdate); tw.Write(Asc28); tw.Write(sbNo); tw.Write(Asc28);
                    tw.Write(sbDate); tw.Write(Asc28); tw.Write(invsrno); tw.Write(Asc28); tw.Write(itemsrnoininv); tw.Write(Asc28); tw.Write(srno); tw.Write(Asc28); tw.Write(registerationnumber); tw.Write(Asc28);
                    tw.Write(registerationdate); tw.Write(Asc28); tw.Write(itemsrnoinpartE); tw.Write(Asc28); tw.Write(itemsrnoinpartC); tw.Write(Asc28); tw.Write(qty); tw.Write(Asc28); tw.Write(Exportqty); tw.Write(Asc28);
                    tw.Write(whetherindigenous); tw.Write(Asc28); tw.Write(amentno); tw.Write(Asc28); tw.Write(amentdate); tw.Write(Asc28);
                }
                tw.Write(nLine);

                //DFIA
                SqlConnection conn13 = new SqlConnection(strcon);
                conn13.Open();
                string sqlQuery13 = "select * from View_ExpJobDetails  where jobNo='" + jno + "'";

                SqlDataAdapter da13 = new SqlDataAdapter(sqlQuery13, conn13);
                DataSet ds13 = new DataSet();
                da13.Fill(ds13, "Jobs");
                DataTable dt13 = ds13.Tables["Jobs"];
                conn13.Close();
                if (ds13.Tables["Jobs"].Rows.Count != 0)
                {
                    DataRowView row = ds13.Tables["Jobs"].DefaultView[0];
                    string invsrno = row["InvoiceSNo"].ToString();
                    string itemsrnoininv = row["ItemSNo"].ToString();
                    string srno = "";
                    string siongroupcode = "";
                    string sionserilanumber = "";
                    string sionIONORMserialno = "";
                    string qty = row["Quantity"].ToString();
                    string unitofmeasures = "";
                    string itemdescription = "";
                    string technicalcharacterstics = "";
                    string filenumber = "";
                    string licencenumber = "";
                    tw.Write(Messagetype); tw.Write(Asc28); tw.Write(ReceiverID); tw.Write(Asc28); tw.Write(SequenceId); tw.Write(Asc28); tw.Write(jobdate); tw.Write(Asc28); tw.Write(sbNo); tw.Write(Asc28);
                    tw.Write(sbDate); tw.Write(Asc28); tw.Write(Asc28); tw.Write(invsrno); tw.Write(Asc28); tw.Write(itemsrnoininv); tw.Write(Asc28); tw.Write(srno); tw.Write(Asc28); tw.Write(siongroupcode); tw.Write(Asc28);
                    tw.Write(sionserilanumber); tw.Write(Asc28); tw.Write(sionIONORMserialno); tw.Write(Asc28); tw.Write(qty); tw.Write(Asc28); tw.Write(unitofmeasures); tw.Write(Asc28); tw.Write(itemdescription); tw.Write(Asc28);
                    tw.Write(technicalcharacterstics); tw.Write(Asc28); tw.Write(filenumber); tw.Write(Asc28); tw.Write(licencenumber); tw.Write(Asc28); tw.Write(amttype); tw.Write(Asc28); tw.Write(amentno); tw.Write(Asc28);
                    tw.Write(amentdate); tw.Write(Asc28);
                } 
                tw.Write(nLine);

                //JOBWORK
                SqlConnection conn14 = new SqlConnection(strcon);
                conn14.Open();
                string sqlQuery14 = "select * from View_ExpJobDetails  where jobNo='" + jno + "'";

                SqlDataAdapter da14 = new SqlDataAdapter(sqlQuery14, conn14);
                DataSet ds14 = new DataSet();
                da14.Fill(ds14, "Jobs");
                DataTable dt14 = ds14.Tables["Jobs"];
                conn14.Close();
                if (ds14.Tables["Jobs"].Rows.Count != 0)
                {
                    DataRowView row = ds14.Tables["Jobs"].DefaultView[0];
                    string invsrno = row["InvoiceSNo"].ToString();
                    string itemsrnoininv = row["ItemSNo"].ToString();
                    string srno = "";
                    string beno = row["BENo"].ToString();
                    string bedate = row["BEDate"].ToString();
                    string beinvoiceserialno = row["InvoiceSNo"].ToString();
                    string beinvoiceno = "";
                    string beitemno = "";
                    string beportcode = "";
                    string beqtyused = "";
                    string qtyunits = "";


                    tw.Write(Messagetype); tw.Write(Asc28); tw.Write(ReceiverID); tw.Write(Asc28); tw.Write(SequenceId); tw.Write(Asc28); tw.Write(jobdate); tw.Write(Asc28); tw.Write(sbNo); tw.Write(Asc28);
                    tw.Write(sbDate); tw.Write(Asc28); tw.Write(sbDate); tw.Write(Asc28); tw.Write(invsrno); tw.Write(Asc28); tw.Write(itemsrnoininv); tw.Write(Asc28); tw.Write(srno); tw.Write(Asc28); tw.Write(beno); tw.Write(Asc28);
                    tw.Write(bedate); tw.Write(Asc28); tw.Write(beinvoiceserialno); tw.Write(Asc28); tw.Write(beinvoiceno); tw.Write(Asc28); tw.Write(beitemno); tw.Write(Asc28); tw.Write(beportcode); tw.Write(Asc28);
                    tw.Write(beqtyused); tw.Write(Asc28); tw.Write(qtyunits); tw.Write(Asc28); tw.Write(amttype); tw.Write(Asc28); tw.Write(amentno); tw.Write(Asc28); tw.Write(amentdate); tw.Write(Asc28);
                }
                tw.Write(nLine);

                //AR4
                SqlConnection conn15 = new SqlConnection(strcon);
                conn15.Open();
                string sqlQuery15 = "select * from View_ExpJobDetails  where jobNo='" + jno + "'";

                SqlDataAdapter da15 = new SqlDataAdapter(sqlQuery15, conn15);
                DataSet ds15 = new DataSet();
                da15.Fill(ds15, "Jobs");
                DataTable dt15 = ds15.Tables["Jobs"];
                conn15.Close();
                if (ds15.Tables["Jobs"].Rows.Count != 0)
                {
                    DataRowView row = ds15.Tables["Jobs"].DefaultView[0];
                    string invsrno = row["InvoiceSNo"].ToString();
                    string itemsrnoininv = row["ItemSNo"].ToString();
                    string AR4no = "";
                    string AR4Date = "";
                    string commissionrate = "";
                    string division = "";
                    string Range = "";
                    string Remarks = "";
                    tw.Write(Messagetype); tw.Write(Asc28); tw.Write(ReceiverID); tw.Write(Asc28); tw.Write(SequenceId); tw.Write(Asc28); tw.Write(jobdate); tw.Write(Asc28); tw.Write(sbNo); tw.Write(Asc28);
                    tw.Write(sbDate); tw.Write(Asc28); tw.Write(sbDate); tw.Write(Asc28); tw.Write(invsrno); tw.Write(Asc28); tw.Write(itemsrnoininv); tw.Write(Asc28); tw.Write(AR4no); tw.Write(Asc28); tw.Write(AR4Date); tw.Write(Asc28);
                    tw.Write(commissionrate); tw.Write(Asc28); tw.Write(division); tw.Write(Asc28); tw.Write(Range); tw.Write(Asc28); tw.Write(Remarks); tw.Write(Asc28); 
                }
                tw.Write(nLine);
                //PACKINGLIST
                SqlConnection conn16 = new SqlConnection(strcon);
                conn16.Open();
                string sqlQuery16 = "select * from E_T_Invoice  where jobNo='" + jno + "'";

                SqlDataAdapter da16 = new SqlDataAdapter(sqlQuery16, conn16);
                DataSet ds16 = new DataSet();
                da16.Fill(ds16, "Jobs");
                DataTable dt16 = ds16.Tables["Jobs"];
                conn16.Close();
                if (ds16.Tables["Jobs"].Rows.Count != 0)
                {
                    string packingnumberfrom = "";
                    string packingnumberto = "";
                    string Packingcode = "";
                    tw.Write(Messagetype); tw.Write(Asc28); tw.Write(ReceiverID); tw.Write(Asc28); tw.Write(SequenceId); tw.Write(Asc28); tw.Write(jobdate); tw.Write(Asc28);
                    tw.Write(packingnumberfrom); tw.Write(Asc28); tw.Write(packingnumberto); tw.Write(Asc28); tw.Write(Packingcode); tw.Write(Asc28); 
                }
                tw.Write(nLine);

                //ROTATION
                SqlConnection conn17 = new SqlConnection(strcon);
                conn17.Open();
                string sqlQuery17 = "select * from E_T_Invoice  where jobNo='" + jno + "'";

                SqlDataAdapter da17 = new SqlDataAdapter(sqlQuery17, conn17);
                DataSet ds17 = new DataSet();
                da17.Fill(ds17, "Jobs");
                DataTable dt17 = ds17.Tables["Jobs"];
                conn17.Close();
                if (ds17.Tables["Jobs"].Rows.Count != 0)
                {
                    string rodationdate = "";
                    string rotationnumber = "";
                    tw.Write(Messagetype); tw.Write(Asc28); tw.Write(ReceiverID); tw.Write(Asc28); tw.Write(SequenceId); tw.Write(Asc28); tw.Write(jobdate); tw.Write(Asc28);
                    tw.Write(rodationdate); tw.Write(Asc28); tw.Write(rotationnumber); tw.Write(Asc28);
                }
                tw.Write(nLine);

                //EOU
                SqlConnection conn18 = new SqlConnection(strcon);
                conn18.Open();
                string sqlQuery18 = "select * from E_T_Invoice  where jobNo='" + jno + "'";

                SqlDataAdapter da18 = new SqlDataAdapter(sqlQuery18, conn18);
                DataSet ds18 = new DataSet();
                da18.Fill(ds18, "Jobs");
                DataTable dt18 = ds18.Tables["Jobs"];
                conn18.Close();
                if (ds18.Tables["Jobs"].Rows.Count != 0)
                {
                    string iecodeofeou = "";
                    string branchsrnoofie = "";
                    string examinationdate = "";
                    string examingofficename = "";
                    string examingofficerdesignation = "";
                    string supervisorofficername = "";
                    string suppervisorofficerdesignation = "";
                    string commisionrate = "";
                    string division = "";
                    string range = "";
                    string sealno = "";
                    string itemvaluesverifiedbyexamingofficer = "";
                    string sampleforwarded = "";
                    tw.Write(Messagetype); tw.Write(Asc28); tw.Write(ReceiverID); tw.Write(Asc28); tw.Write(SequenceId); tw.Write(Asc28); tw.Write(jobdate); tw.Write(Asc28); tw.Write(sbNo); tw.Write(Asc28);
                    tw.Write(sbDate); tw.Write(Asc28); tw.Write(iecodeofeou); tw.Write(Asc28); tw.Write(branchsrnoofie); tw.Write(Asc28); tw.Write(examinationdate); tw.Write(Asc28); tw.Write(examingofficename); tw.Write(Asc28);
                    tw.Write(examingofficerdesignation); tw.Write(Asc28); tw.Write(supervisorofficername); tw.Write(Asc28); tw.Write(suppervisorofficerdesignation); tw.Write(Asc28); tw.Write(commisionrate); tw.Write(Asc28); tw.Write(division); tw.Write(Asc28);
                    tw.Write(range); tw.Write(Asc28); tw.Write(sealno); tw.Write(Asc28); tw.Write(itemvaluesverifiedbyexamingofficer); tw.Write(Asc28); tw.Write(sampleforwarded); tw.Write(Asc28); tw.Write(amttype ); tw.Write(Asc28);
                    tw.Write(amentno); tw.Write(Asc28); tw.Write(amentdate); tw.Write(Asc28);

                }
                tw.Write(nLine);
                //STUFF
                 SqlConnection conn19 = new SqlConnection(strcon);
                conn19.Open();
                string sqlQuery19 = "select * from E_T_Invoice  where jobNo='" + jno + "'";

                SqlDataAdapter da19 = new SqlDataAdapter(sqlQuery19, conn19);
                DataSet ds19 = new DataSet();
                da19.Fill(ds19, "Jobs");
                DataTable dt19 = ds19.Tables["Jobs"];
                conn19.Close();
                if (ds19.Tables["Jobs"].Rows.Count != 0)
                {
                    string factorysuffered = "";
                    string sampleaccomapanied = "";
                    tw.Write(Messagetype); tw.Write(Asc28); tw.Write(ReceiverID); tw.Write(Asc28); tw.Write(SequenceId); tw.Write(Asc28); tw.Write(jobdate); tw.Write(Asc28);
                    tw.Write(factorysuffered); tw.Write(Asc28); tw.Write(sampleaccomapanied); tw.Write(Asc28);
                }
                tw.Write(nLine);

                //container
                SqlConnection conn20 = new SqlConnection(strcon);
                conn20.Open();
                string sqlQuery20 = "select * from E_T_Invoice  where jobNo='" + jno + "'";

                SqlDataAdapter da20 = new SqlDataAdapter(sqlQuery20, conn20);
                DataSet ds20 = new DataSet();
                da20.Fill(ds20, "Jobs");
                DataTable dt20 = ds20.Tables["Jobs"];
                conn20.Close();
                if (ds20.Tables["Jobs"].Rows.Count != 0)
                {
                    string containernumber = "";
                    string containersize = "";
                    string excisesealno = "";
                    string sealdate = "";
                    tw.Write(Messagetype); tw.Write(Asc28); tw.Write(ReceiverID); tw.Write(Asc28); tw.Write(SequenceId); tw.Write(Asc28); tw.Write(jobdate); tw.Write(Asc28);
                    tw.Write(containernumber); tw.Write(Asc28); tw.Write(containersize); tw.Write(Asc28); tw.Write(excisesealno); tw.Write(Asc28); tw.Write(sealdate); tw.Write(Asc28); 
                }
                tw.Write(nLine);

                //cargoback
                SqlConnection conn21 = new SqlConnection(strcon);
                conn21.Open();
                string sqlQuery21 = "select * from View_ExpJobDetails  where jobNo='" + jno + "'";

                SqlDataAdapter da21 = new SqlDataAdapter(sqlQuery21, conn21);
                DataSet ds21 = new DataSet();
                da21.Fill(ds21, "Jobs");
                DataTable dt21 = ds21.Tables["Jobs"];
                conn21.Close();
                if (ds21.Tables["Jobs"].Rows.Count != 0)
                {
                    DataRowView row = ds21.Tables["Jobs"].DefaultView[0];
                    string natureofcargo = row["NatureofCargo"].ToString();
                    string loosepacketsleft = row["LoosePkgs"].ToString();
                    string totalpacketsleft = row["TotalNoofPkgs"].ToString();
                    string noofcontainerleft ="";
                    string grosswtleft = row["GrossWeight"].ToString();
                    string netweightleft = row["NetWeight"].ToString();
                    string uniqueqtycode = "";
                    tw.Write(Messagetype); tw.Write(Asc28); tw.Write(ReceiverID); tw.Write(Asc28); tw.Write(sbNo); tw.Write(Asc28); tw.Write(sbDate); tw.Write(Asc28); tw.Write(natureofcargo); tw.Write(Asc28);
                    tw.Write(loosepacketsleft); tw.Write(Asc28); tw.Write(totalpacketsleft); tw.Write(Asc28); tw.Write(noofcontainerleft); tw.Write(Asc28); tw.Write(grosswtleft); tw.Write(Asc28); tw.Write(netweightleft); tw.Write(Asc28);
                    tw.Write(uniqueqtycode); tw.Write(Asc28); tw.Write(amttype); tw.Write(Asc28); tw.Write(amentno); tw.Write(Asc28); tw.Write(amentdate); tw.Write(Asc28);
                }
                tw.Write(nLine);

                //PCKGBACK
                SqlConnection conn22 = new SqlConnection(strcon);
                conn22.Open();
                string sqlQuery22 = "select * from E_T_Invoice  where jobNo='" + jno + "'";

                SqlDataAdapter da22 = new SqlDataAdapter(sqlQuery22, conn22);
                DataSet ds22 = new DataSet();
                da22.Fill(ds22, "Jobs");
                DataTable dt22 = ds22.Tables["Jobs"];
                conn22.Close();
                if (ds22.Tables["Jobs"].Rows.Count != 0)
                {
                    string startingpackno = "";
                    string endingpackno = "";
                    string uniqueqtycode = "";
                    tw.Write(Messagetype); tw.Write(Asc28); tw.Write(ReceiverID); tw.Write(Asc28); tw.Write(sbNo); tw.Write(Asc28); tw.Write(sbDate); tw.Write(Asc28); tw.Write(startingpackno); tw.Write(Asc28);
                    tw.Write(endingpackno); tw.Write(Asc28); tw.Write(uniqueqtycode); tw.Write(Asc28); tw.Write(amttype); tw.Write(Asc28); tw.Write(amentno); tw.Write(Asc28); tw.Write(amentdate); tw.Write(Asc28);
                }
                tw.Write(nLine);


                //conatinerback
                SqlConnection conn23 = new SqlConnection(strcon);
                conn23.Open();
                string sqlQuery23 = "select * from E_T_Invoice  where jobNo='" + jno + "'";

                SqlDataAdapter da23 = new SqlDataAdapter(sqlQuery23, conn23);
                DataSet ds23 = new DataSet();
                da23.Fill(ds23, "Jobs");
                DataTable dt23 = ds23.Tables["Jobs"];
                conn23.Close();
                if (ds23.Tables["Jobs"].Rows.Count != 0)
                {
                    string containerno = "";
                    string containersize = "";
                    tw.Write(Messagetype); tw.Write(Asc28); tw.Write(ReceiverID); tw.Write(Asc28); tw.Write(sbNo); tw.Write(Asc28); tw.Write(sbDate); tw.Write(Asc28); tw.Write(containerno); tw.Write(Asc28);
                    tw.Write(containersize); tw.Write(Asc28);  tw.Write(amttype); tw.Write(Asc28); tw.Write(amentno); tw.Write(Asc28); tw.Write(amentdate); tw.Write(Asc28);
                }
                tw.Write(nLine);
                //STR
                SqlConnection conn24 = new SqlConnection(strcon);
                conn24.Open();
                string sqlQuery24 = "select * from View_ExpJobDetails  where jobNo='" + jno + "'";

                SqlDataAdapter da24 = new SqlDataAdapter(sqlQuery24, conn24);
                DataSet ds24 = new DataSet();
                da24.Fill(ds24, "Jobs");
                DataTable dt24 = ds24.Tables["Jobs"];
                conn24.Close();
                if (ds24.Tables["Jobs"].Rows.Count != 0)
                {
                    DataRowView row = ds24.Tables["Jobs"].DefaultView[0];
                    string invsrno = row["InvoiceSNo"].ToString();
                    string itemsrnoininv = row["ItemSNo"].ToString();
                    string SRNO = "";
                    string itemcodeasperstrdirectory = "";
                    tw.Write(Messagetype); tw.Write(Asc28); tw.Write(ReceiverID); tw.Write(Asc28); tw.Write(SequenceId); tw.Write(Asc28); tw.Write(jobdate); tw.Write(Asc28); tw.Write(sbNo); tw.Write(Asc28);
                    tw.Write(sbDate); tw.Write(Asc28); tw.Write(invsrno); tw.Write(Asc28); tw.Write(itemsrnoininv); tw.Write(Asc28); tw.Write(SRNO); tw.Write(Asc28); tw.Write(itemcodeasperstrdirectory); tw.Write(Asc28);
                    tw.Write(amttype); tw.Write(Asc28); tw.Write(amentno); tw.Write(Asc28); tw.Write(amentdate); tw.Write(Asc28);
                }
                tw.Write(nLine);
                //STR
                SqlConnection conn25 = new SqlConnection(strcon);
                conn25.Open();
                string sqlQuery25 = "select * from E_T_Invoice  where jobNo='" + jno + "'";

                SqlDataAdapter da25 = new SqlDataAdapter(sqlQuery25, conn25);
                DataSet ds25 = new DataSet();
                da25.Fill(ds25, "Jobs");
                DataTable dt25 = ds25.Tables["Jobs"];
                conn25.Close();
                if (ds25.Tables["Jobs"].Rows.Count != 0)
                {
                    string siteid = "";
                    string requestdate = "";
                    string requestletternumber = "";
                    string indicatetypeofamentment = "";
                    string reasonforamentment = "";
                    string amentmentstatus = "";
                    string uniquenumbergenerateforeachamenttype = "";
                  
                    tw.Write(Messagetype); tw.Write(Asc28); tw.Write(siteid); tw.Write(Asc28); tw.Write(sbNo); tw.Write(Asc28);
                    tw.Write(sbDate); tw.Write(Asc28); tw.Write(requestdate); tw.Write(Asc28); tw.Write(requestletternumber); tw.Write(Asc28); tw.Write(indicatetypeofamentment); tw.Write(Asc28); tw.Write(reasonforamentment); tw.Write(Asc28);
                    tw.Write(amentmentstatus); tw.Write(Asc28); tw.Write(uniquenumbergenerateforeachamenttype); tw.Write(Asc28);tw.Write(amentdate); tw.Write(Asc28);
                }
                tw.Write(nLine);
                tw.Write(endsb );
                tw.Write(TText);
                tw.Write(SequenceId);
                tw.Flush();
                tw.Close();
            }
            catch(Exception e)
            {
                Response.Write(e.Message);
            }
        }
        protected void btnGenerate_Click(object sender, EventArgs e)
        {
            sbfile();
        }
    }
}