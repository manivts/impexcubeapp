using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using VTS.ImpexCube.Business;
using System.Drawing;
using System.IO;
using System.Text;
using VTS.ImpexCube.Data;

namespace ImpexCube
{
    public partial class frmBEFile : System.Web.UI.Page
    {
        string strcon = (string)ConfigurationManager.ConnectionStrings["Constr"].ConnectionString;
        VTS.ImpexCube.Business.ProductDetailsBL obj = new VTS.ImpexCube.Business.ProductDetailsBL();
        VTS.ImpexCube.Business.CommonBL obj1 = new VTS.ImpexCube.Business.CommonBL();
        VTS.ImpexCube.Utlities.Utility joblog = new VTS.ImpexCube.Utlities.Utility();
        private CommonDL objCommonDL = new CommonDL();
     
        StringBuilder berunfile = new StringBuilder();
        StringBuilder bemanfile = new StringBuilder();
        private string fields = "";
        private string FILEPATH = "";
        private string filePath = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            Label pagename;
            pagename = (Label)Master.FindControl("lblName");
            pagename.Text = "BE FILE";
            if (!IsPostBack)
            {
                JobNo();
                ddlJobNo.SelectedValue = (string)Session["JobNo"];
            }
           
        }

        protected void btnGenerate_Click(object sender, EventArgs e)
        {
            txtBeFile.Text = "";
            string mode = "";
            SqlConnection conns = new SqlConnection(strcon);
                conns.Open();
                string sqlQuery1 = "select Mode from View_JobImporterShipment where jobNo='" + ddlJobNo.SelectedValue  + "'";

                SqlDataAdapter da1 = new SqlDataAdapter(sqlQuery1, conns);
                DataSet ds1 = new DataSet();
                da1.Fill(ds1, "Jobs");
                conns.Close();
                if (ds1.Tables["Jobs"].Rows.Count != 0)
                {
                    mode = ds1.Tables["jobs"].Rows[0]["Mode"].ToString();
                }
            befile(mode);
        }
        public void JobNo()
        {
            DataSet dt = obj1.GetJobNo();
            ddlJobNo.DataSource = dt;
            ddlJobNo.DataValueField = "JobNo";
            ddlJobNo.DataTextField = "JobNo";
            ddlJobNo.DataBind();
        }
        public void befile(string mode)
        {
            try 
            {
                SqlConnection conns = new SqlConnection(strcon);
                conns.Open();

                berunfile.AppendLine("User Name : " + (string)Session["USER-NAME"]);

                berunfile.AppendLine("Date Time : " + DateTime.Now);

                string querys = "select HText,TText,zz,SenderID,ReceiverID,VersionNo,fType,MessageID from tbl_FileDetails where Mode='"+mode +"'";

                SqlDataAdapter sda = new SqlDataAdapter(querys, conns);
                DataSet dsrun = new DataSet();
                DataRowView dvrun = null;
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
                berunfile.AppendLine("Sender ID : " + SenderID);

                string ReceiverID = dvrun["ReceiverID"].ToString();
                berunfile.AppendLine("Receiver ID : " + ReceiverID);


                string VersionNo = dvrun["VersionNo"].ToString();
                berunfile.AppendLine("Version No : " + VersionNo);


                string fType = dvrun["fType"].ToString();

                string MessageID = dvrun["MessageID"].ToString();

                int SequenceId = Convert.ToInt32(jno);//jno.Substring(13, 6);
                berunfile.AppendLine("Job No : " + SequenceId);


                string jdate = DateTime.Now.Date.ToString("dd/MM/yyyy");
                berunfile.AppendLine("Job Date : " + jdate);


                string jTime = DateTime.Now.ToString("HH:mm");
                berunfile.AppendLine("Job Time : " + jTime);
                txtBeFile.Text = berunfile.ToString();

                GetFSIO(jno, SenderID, ReceiverID, VersionNo, fType, MessageID, SequenceId, HText, TText, zz);
                
                // ScriptManager.RegisterStartupScript(this, this.GetType(), "redirect", "alert('Successfully Created BE File.');", true);
            }
            catch
            {
            }
        }
        public string  convertdate(string jdate)
        {
            if (jdate != "")
            {
                string[] jD = jdate.Split('/');
                int jdd = jD[0].Length;
                int jmm = jD[1].Length;
                if (jdd <= 1)
                    jD[0] = "0" + jD[0];
                if (jmm <= 1)
                    jD[1] = "0" + jD[1];
                jdate = jD[2].Substring(0, 4) + jD[1] + jD[0];
            }
            return jdate;
        }
        public string splituptodotwithslasallow(string address)
        {
            string value = string.Empty;
            int n = address.Length;
            if (address!="")
            {
                for (int i = 0; i < n; i++)
                {
                   address= address.Replace('a', '1');
                }
            }
            return address;
        }
        private void updatejob()
        {
            SqlConnection con = new SqlConnection(strcon);
            con.Open();
            SqlCommand cmd = new SqlCommand("update T_JobCreation set Completed =1 where JobNo='" + ddlJobNo.SelectedValue + "'", con);
            cmd.ExecuteNonQuery();
            con.Close();
        }
        private void JobCreationUpdate()
        {
            //To Update TotalInvoice, TotalAssVal, TotalDuty, InvoiceDetail
            SqlConnection con = new SqlConnection(strcon);
            con.Open();
            string qry = "SELECT Sum(TotalDutyAmt) As TotalDutyAmt FROM T_Product Where JobNo='" + ddlJobNo.SelectedValue + "'";
            SqlDataAdapter sda = new SqlDataAdapter(qry, con);
            DataSet dsrun = new DataSet();
            sda.Fill(dsrun, "JobCreation");
            con.Close();
            DataRowView dvrun = dsrun.Tables["JobCreation"].DefaultView[0];
            //if (dsrun.Tables["running"].Rows.Count != 0)
            //{

           // }
            double TotalDutyAmt = Convert.ToDouble(dvrun["TotalDutyAmt"].ToString());

        }
        protected void GetFSIO(string jno, string SenderID, string ReceiverID, string VersionNo, string fType, string MessageID, int SequenceId, string HText, string TText, string zz)
        {
            try
            {
                SqlConnection conn1 = new SqlConnection(strcon);
                conn1.Open();
                string querysRecId = "select Custom from T_Jobcreation where JobNo='" + jno + "'";
                SqlDataAdapter sdaRecId = new SqlDataAdapter(querysRecId, conn1);
                DataSet dsrunRecId = new DataSet();
               // DataRowView dvrunRecId = null;
                sdaRecId.Fill(dsrunRecId, "running");
                conn1.Close();
                if (dsrunRecId.Tables["running"].Rows.Count != 0)
                {
                    DataRowView dvrunRecId = dsrunRecId.Tables["running"].DefaultView[0];
                    ReceiverID = dvrunRecId["Custom"].ToString();
                }

                string Messagetype = "F";
                string jobdate = "";
                string BeNo = "";
                string BeDate = "";
                string Betype = "";
                string Ieccode = "";
                string branchsrno = "";
                string importer = "";
                string address1 = "";
                string address2 = "";
                string city = "";
                string state = "";
                string pin = "";
                string classs = "";
                string modetran = "";
                string imptype = "";
                string kanbe = "";
                string saleflag = "";
                string origin = "";
                string chacode = "";
                string cntryorigin = "";
                string cntryconsignment = "";
                string portshipment = "";
                string grennchannel = "";
                string sec48 = "";
                string priorbe = "";
                string dealcode = "";
                string firstcheck = "";
                string wmscode = "";
                string wmscustomsid = "";
                string wmsbeno = "";
                string wmsbedate = "";
                string pkgs = "";
                string pkgcode = "";
                string gwt = "";
                string uom = "";
                string xbond = "";
                string misnloadrate = "";
                int unicode = 29;
                char character = (char)unicode;
                string Asc28 = character.ToString();
                string SVBflag = "";
                string DOCTYPE = "";
                int unicode1 = 10;
                char character1 = (char)unicode1;
                string nLine = character1.ToString();
                //string jdate = DateTime.Now.Date.ToString("dd/MM/yyyy");


                //jdate = convertdate(jdate);

                string jTime = DateTime.Now.ToString("HH:mm");
                string[] jT = jTime.Split(':');
                jTime = jT[0] + jT[1];
                string be = "<TABLE>BE";
                string exch = "<TABLE>EXCHANGE";
                string Permission = "<TABLE>PERMISSION";
                string invoice = "<TABLE>INVOICE";
                string misc_ch = "<TABLE>MISC_CH";
                string items = "<TABLE>ITEMS";
                string licence = "<TABLE>LICENCE";
                string rsp = "<TABLE>RSP";
                string debp = "<TABLE>DEPB";
                string bond = "<TABLE>BOND";
                string cert = "<TABLE>CERT";
                string hss = "<TABLE>HSS";
                string reimport = "<TABLE>REIMPORT";
                string sbeduty = "<TABLE>SBEDUTY";
                string igms = "<TABLE>IGMS";
                string container = "<TABLE>CONTAINER";
                string ctx = "<TABLE>CTX";
                string amend = "<TABLE>AMEND";

                string endbe = "<END-BE>";


                //***********************************************************************************
                //****************************** Table Name BE **************************************
                //***********************************************************************************
              //  SqlConnection conn1 = new SqlConnection(strcon);
                conn1.Open();
                string sqlQuery1 = "select * from View_JobImporterShipment where jobNo='" + jno + "'";

                SqlDataAdapter da1 = new SqlDataAdapter(sqlQuery1, conn1);
                DataSet ds1 = new DataSet();
                da1.Fill(ds1, "Jobs");
                conn1.Close();
                if (ds1.Tables["Jobs"].Rows.Count != 0)
                {
                    bemanfile.AppendLine("Be Mandatory Fields");
                    jobdate = ds1.Tables["Jobs"].Rows[0]["JobReceivedDate"].ToString();
                    jobdate = convertdate(jobdate);
                    berunfile.AppendLine("Job Received Date : " + jobdate);
                    BeNo = ds1.Tables["Jobs"].Rows[0]["BeNo"].ToString();
                    BeDate = ds1.Tables["Jobs"].Rows[0]["BeDate"].ToString();
                    BeDate = convertdate(BeDate);
                    Betype = ds1.Tables["Jobs"].Rows[0]["BeType"].ToString();
                    DOCTYPE = ds1.Tables["Jobs"].Rows[0]["Docfillingstatus"].ToString();
                    Ieccode = ds1.Tables["Jobs"].Rows[0]["IECodeNo"].ToString();
                    if (Ieccode == "")
                    {
                        bemanfile.AppendLine("IecCode");
                        fields = "1";
                    }
                    berunfile.AppendLine("IE Code : " + Ieccode);
                    branchsrno = ds1.Tables["Jobs"].Rows[0]["BranchSno"].ToString();
                    if (branchsrno == "")
                    {
                        bemanfile.AppendLine("Branch SerialNo");
                        fields = "1";
                    }
                    berunfile.AppendLine("Branch Sno : " + branchsrno);
                    importer = ds1.Tables["Jobs"].Rows[0]["Importer"].ToString();
                    berunfile.AppendLine("Job Received Date : " + jobdate);
                    string address = ds1.Tables["Jobs"].Rows[0]["Address"].ToString();
                    address = address.Trim();
                    if (address.Length > 35)
                    {
                        address = address.Replace("\n", "");
                        address1 = address.Substring(0, 35);
                        address = address.Remove(0, 35);
                        if (address.Length > 35)
                        {
                            address2 = address.Substring(0, 35);
                        }
                        else
                        {
                            address2 = address;
                        }
                    }
                    berunfile.AppendLine("Address : " + address1);
                    city = ds1.Tables["Jobs"].Rows[0]["City"].ToString();
                    berunfile.AppendLine("City : " + city);
                    state = ds1.Tables["Jobs"].Rows[0]["State"].ToString();
                    berunfile.AppendLine("State : " + state);
                    pin = ds1.Tables["Jobs"].Rows[0]["ZipCode"].ToString();
                    berunfile.AppendLine("ZipCode : " + pin);
                    txtBeFile.Text = berunfile.ToString();

                    classs = "N";
                    if (ds1.Tables["Jobs"].Rows[0]["Mode"].ToString() == "Air")
                    {
                        modetran = "A";
                    }
                    else if (ds1.Tables["Jobs"].Rows[0]["Mode"].ToString() == "Sea")
                    {
                        modetran = "S";
                    }
                    else if (ds1.Tables["Jobs"].Rows[0]["Mode"].ToString() == "Land")
                    {
                        modetran = "L";
                    }
                    else
                    {
                        bemanfile.AppendLine("Mode of Transport");
                        fields = "1";
                    }
                    imptype = ds1.Tables["Jobs"].Rows[0]["ImporterType"].ToString();
                    berunfile.AppendLine("Importer Type : " + imptype);

                    if (ds1.Tables["Jobs"].Rows[0]["ChkKachha"].ToString() == "Yes")
                    {
                        kanbe = "Y";
                    }
                    else if (ds1.Tables["Jobs"].Rows[0]["ChkKachha"].ToString() == "No")
                    {
                        kanbe = "N";
                    }
                    else
                    {
                        bemanfile.AppendLine("Kachcha Be");
                        fields = "1";

                    }
                    if (ds1.Tables["Jobs"].Rows[0]["HighSeaSale"].ToString() == "True")
                    {
                        saleflag = "Y";
                    }
                    else if (ds1.Tables["Jobs"].Rows[0]["HighSeaSale"].ToString() == "False")
                    {
                        saleflag = "N";
                    }
                    else
                    {
                        bemanfile.AppendLine("High Sea Sale flag");
                        fields = "1";
                    }
                    //ShipmentPortCode
                    if (modetran == "S")
                    {
                        origin = ds1.Tables["Jobs"].Rows[0]["ShipmentUneceCode"].ToString();
                    }
                    else
                    {
                        origin = ds1.Tables["Jobs"].Rows[0]["ShipmentPortCode"].ToString();
                    }
                  
                    if (origin == "")
                    {
                        bemanfile.AppendLine("Port of Origin");
                        fields = "1";
                    }
                    berunfile.AppendLine("Port Of Origin : " + origin);

                    string CHAQuery = "SELECT Chano FROM M_CompanyInfo where Branch ='" + (string)Session["ZONE"] + "'";
                    DataSet dsCha = objCommonDL.GetDataSet(CHAQuery);
                    if (dsCha.Tables["Table"].Rows.Count != 0)
                    {
                        DataRowView row1 = dsCha.Tables["Table"].DefaultView[0];
                        chacode = row1["Chano"].ToString();
                    }

                    //chacode = "AACCP4978KCH004";
                    cntryorigin = ds1.Tables["Jobs"].Rows[0]["CountryOrigincode"].ToString();
                    if (cntryorigin == "")
                    {
                        bemanfile.AppendLine("CountryOrigin");
                        fields = "1";
                    }
                    berunfile.AppendLine("Country Origin: " + cntryorigin);

                    cntryconsignment = ds1.Tables["Jobs"].Rows[0]["Shipmentcountrycode"].ToString();
                    if (cntryconsignment == "")
                    {
                        bemanfile.AppendLine("Country of Consignment");
                        fields = "1";
                    }
                    berunfile.AppendLine("Country of Shipment : " + cntryconsignment);

                    if (modetran == "S")
                    {
                        portshipment = ds1.Tables["Jobs"].Rows[0]["ShipmentUneceCode"].ToString();
                    }
                    else
                    {
                        portshipment = ds1.Tables["Jobs"].Rows[0]["ShipmentPortCode"].ToString();
                    }
                   
                    if (portshipment == "")
                    {
                        bemanfile.AppendLine("Port of shipment");
                        fields = "1";
                    }
                    berunfile.AppendLine("Port of shipment : " + portshipment);

                    if (ds1.Tables["Jobs"].Rows[0]["ChkGreen"].ToString() == "Yes")
                    {
                        grennchannel = "Y";
                    }
                    else
                    {
                        grennchannel = "N";
                    }
                    if (ds1.Tables["Jobs"].Rows[0]["ChkUnderSec48"].ToString() == "Yes")
                    {
                        sec48 = "Y";
                    }
                    else
                    {
                        sec48 = "N";
                    }
                    priorbe = ds1.Tables["Jobs"].Rows[0]["DocFillingStatus"].ToString();
                    if (priorbe == "Advance")
                    {
                        priorbe = "A";
                    }
                    else if (priorbe == "Normal")
                    {
                        priorbe = "N";
                    }
                   // dealcode = "0240029";
                    dealcode = "";
                    if (ds1.Tables["Jobs"].Rows[0]["ChkFirstChk"].ToString() == "Yes")
                    {
                        firstcheck = "Y";
                    }
                    else
                    {
                        firstcheck = "N";
                    }

                    // ********************** Exbond Details **********************************
                    conn1.Open();
                    string sqlQueryExbond = "select EXBondJobNo, EXBondBLNO, EXBondBLDate,EXWareHouse, ExCode from T_ImportExBondDetails where JobNo='" + jno + "'";
                    SqlDataAdapter daExbond = new SqlDataAdapter(sqlQueryExbond, conn1);
                    DataSet dsExbond = new DataSet();
                    daExbond.Fill(dsExbond, "Jobs");
                    conn1.Close();
                    if (dsExbond.Tables["Jobs"].Rows.Count != 0)
                    {
                        wmscode = dsExbond.Tables["Jobs"].Rows[0]["ExCode"].ToString();
                        wmscustomsid = ReceiverID;
                        wmsbeno = dsExbond.Tables["Jobs"].Rows[0]["EXBondBLNO"].ToString();
                        wmsbedate = dsExbond.Tables["Jobs"].Rows[0]["EXBondBLDate"].ToString();
                        wmsbedate = convertdate(wmsbedate);

                        pkgs = ds1.Tables["Jobs"].Rows[0]["NoOfPackages"].ToString();
                        uom = ds1.Tables["Jobs"].Rows[0]["GrossWeightUnit"].ToString();
                        gwt = ds1.Tables["Jobs"].Rows[0]["GrossWeight"].ToString();

                    }
                    ////pkgs = ds1.Tables["Jobs"].Rows[0]["NoOfPackages"].ToString();
                    //pkgs = "";
                    //berunfile.AppendLine("No Of Packages : " + pkgs);

                    //pkgcode = "";
                    ////gwt = ds1.Tables["Jobs"].Rows[0]["GrossWeight"].ToString();
                    //gwt = "";
                    //berunfile.AppendLine("Gross Weight : " + gwt);

                    ////uom = ds1.Tables["Jobs"].Rows[0]["GrossWeightUnit"].ToString();
                    //uom = "";
                    //berunfile.AppendLine("Gross Weight Unit : " + uom);
                    txtBeFile.Text = berunfile.ToString();
                    txtBeFile.Focus();

                }

                string file = string.Empty;
                string mn = DateTime.Now.ToString("MM");
                string dd = DateTime.Now.ToString("dd");
                string datetime = DateTime.Now.Year.ToString() + mn + dd + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString();

                string pathdir = "00" + jno + "14" + ".be";

                //To remove the old be file ////////////////////////////////
                string paths = AppDomain.CurrentDomain.BaseDirectory;

                string pathdir1 = Path.Combine(paths, @"TempFile\");
                //path1 = pathdir + Path.GetFileName(FileUpload1.PostedFile.FileName);

                string[] filePaths = Directory.GetFiles(@pathdir1);

                
                System.IO.File.WriteAllText(AppDomain.CurrentDomain.BaseDirectory + @"TempFile\" + pathdir, "");
                file = AppDomain.CurrentDomain.BaseDirectory + @"TempFile\" + pathdir;

                FILEPATH = file;

                FileStream fs = new FileStream(@file, FileMode.OpenOrCreate, FileAccess.ReadWrite);
                StreamWriter tw = new StreamWriter(fs);
                //string pathdir = "00" + SequenceId + "14" + ".be";
              
                //System.IO.File.WriteAllText(AppDomain.CurrentDomain.BaseDirectory + @"TempFile\" + pathdir,"");
                //file = AppDomain.CurrentDomain.BaseDirectory + @"TempFile\" + pathdir;

                //FILEPATH = file;

                //FileStream fs = new FileStream(@file, FileMode.OpenOrCreate, FileAccess.ReadWrite);
                //StreamWriter tw = new StreamWriter(fs);


                filePath = file;
                tw.Write(HText); tw.Write(Asc28); tw.Write(zz);  tw.Write(SenderID); tw.Write(Asc28); tw.Write(zz); tw.Write(Asc28);
                tw.Write(ReceiverID); tw.Write(Asc28); tw.Write(VersionNo); tw.Write(Asc28); tw.Write(fType); 
                tw.Write(MessageID); tw.Write(Asc28); tw.Write(SequenceId); tw.Write(Asc28); tw.Write(jobdate); tw.Write(Asc28); tw.Write(jTime);
                  tw.Write(tw.NewLine);
                tw.Write(be);
                  tw.Write(tw.NewLine);
                tw.Write(Messagetype); tw.Write(Asc28); tw.Write(ReceiverID); tw.Write(Asc28); tw.Write(SequenceId); tw.Write(Asc28); tw.Write(jobdate); tw.Write(Asc28); tw.Write(BeNo); tw.Write(Asc28);
                tw.Write(BeDate); tw.Write(Asc28); tw.Write(Betype); tw.Write(Asc28); tw.Write(Ieccode); tw.Write(Asc28); tw.Write(branchsrno); tw.Write(Asc28); tw.Write(importer); tw.Write(Asc28);
                tw.Write(address1); tw.Write(Asc28); tw.Write(address2); tw.Write(Asc28); tw.Write(city); tw.Write(Asc28); tw.Write(state); tw.Write(Asc28); tw.Write(pin); tw.Write(Asc28);
                tw.Write(classs); tw.Write(Asc28); tw.Write(modetran); tw.Write(Asc28); tw.Write(imptype); tw.Write(Asc28); tw.Write(kanbe); tw.Write(Asc28); tw.Write(saleflag); tw.Write(Asc28);
                tw.Write(origin); tw.Write(Asc28); tw.Write(chacode); tw.Write(Asc28); tw.Write(cntryorigin); tw.Write(Asc28); tw.Write(cntryconsignment); tw.Write(Asc28); tw.Write(portshipment); tw.Write(Asc28);
                tw.Write(grennchannel); tw.Write(Asc28); tw.Write(sec48); tw.Write(Asc28); tw.Write(priorbe); tw.Write(Asc28); tw.Write(dealcode); tw.Write(Asc28); tw.Write(firstcheck); tw.Write(Asc28);
                tw.Write(wmscode); tw.Write(Asc28); tw.Write(wmscustomsid); tw.Write(Asc28); tw.Write(wmsbeno); tw.Write(Asc28); tw.Write(wmsbedate); tw.Write(Asc28); tw.Write(pkgs); tw.Write(Asc28);
                tw.Write(pkgcode); tw.Write(Asc28); tw.Write(gwt); tw.Write(Asc28); tw.Write(uom); tw.Write(Asc28); tw.Write(xbond); tw.Write(Asc28); tw.Write(misnloadrate);


                //***********************************************************************************
                //****************************** Table Name EXCHANGE **************************************
                //***********************************************************************************
                SqlConnection connCur = new SqlConnection(strcon);
                connCur.Open();
                string sqlQueryCur = "select Distinct Currency from View_ImpInvoiceCurrency  where jobNo='" + jno + "'";
                SqlDataAdapter dacur = new SqlDataAdapter(sqlQueryCur, connCur);
                DataSet dsCur = new DataSet();
                dacur.Fill(dsCur, "Jobs");
                DataTable dtCur = dsCur.Tables["Jobs"];
                connCur.Close();
                if (dsCur.Tables["Jobs"].Rows.Count != 0)
                {
                    tw.Write(tw.NewLine);
                    tw.Write(exch);
                    int i = 0;
                    foreach (DataRow rss in dtCur.Rows)
                    {
                        DataRowView r = dsCur.Tables["Jobs"].DefaultView[i];
                        string cur = r.Row["Currency"].ToString();
                            string stdardcur = "Y";
                            string Unitrs = "";
                            string Rates = "";
                            string effect = "";
                            string bankname = "";
                            string certnum = "";
                            string certdate = "";
                            tw.Write(tw.NewLine);
                            tw.Write(Messagetype); tw.Write(Asc28); tw.Write(ReceiverID); tw.Write(Asc28); tw.Write(SequenceId); tw.Write(Asc28); tw.Write(jobdate); tw.Write(Asc28); tw.Write(BeNo); tw.Write(Asc28);
                            tw.Write(BeDate); tw.Write(Asc28); tw.Write(cur); tw.Write(Asc28); tw.Write(stdardcur); tw.Write(Asc28); tw.Write(Unitrs); tw.Write(Asc28); tw.Write(Rates); tw.Write(Asc28);
                            tw.Write(effect); tw.Write(Asc28); tw.Write(bankname); tw.Write(Asc28); tw.Write(certnum); tw.Write(Asc28); tw.Write(certdate);
                            i++;
                    }
                }



                //***********************************************************************************
                //****************************** Table Name INVOICE *********************************
                //***********************************************************************************
                SqlConnection conn2 = new SqlConnection(strcon);
                conn2.Open();
                string sqlQuery2 = "select * from T_InvoiceDetails  where jobNo='" + jno + "'";
                SqlDataAdapter da2 = new SqlDataAdapter(sqlQuery2, conn2);
                DataSet ds2 = new DataSet();
                da2.Fill(ds2, "Jobs");
                DataTable dt2 = ds2.Tables["Jobs"];
                conn2.Close();
                if (ds2.Tables["Jobs"].Rows.Count != 0)
                {
                    tw.Write(tw.NewLine);
                    tw.Write(invoice);
                    bemanfile.AppendLine("");
                    bemanfile.AppendLine("Invoice Table Details");
                    int i = 0;
                    foreach (DataRow rss in dt2.Rows)
                    {
                        tw.Write(tw.NewLine);
                        tw.Write(Messagetype); tw.Write(Asc28); tw.Write(ReceiverID); tw.Write(Asc28); tw.Write(SequenceId); tw.Write(Asc28); tw.Write(jobdate); tw.Write(Asc28);
                        if (BeNo != "")
                        {
                            tw.Write(BeNo);
                           
                        }
                        tw.Write(Asc28);
                        if (BeDate != "")
                        {
                            tw.Write(BeDate);
                            
                        }
                        tw.Write(Asc28);
                        DataRowView r = ds2.Tables["Jobs"].DefaultView[i];
                        string invsrno = (i + 1).ToString();
                        if (invsrno != "")
                        {
                            tw.Write(invsrno);
                         
                        }
                       
                            tw.Write(Asc28);
                       
                        string invdate = r.Row["InvoiceDate"].ToString();
                        invdate = convertdate(invdate);
                        if (invdate != "")
                        {
                            tw.Write(invdate);
                           
                        }
                      
                            tw.Write(Asc28);
                        
                        berunfile.AppendLine("Invoice Date : " + invdate);
                        string pono = "";
                        if (pono != "")
                        {
                            tw.Write(pono);
                            
                        }
                        
                            tw.Write(Asc28);
                      
                        string podate = r.Row["PODate"].ToString();
                        podate = convertdate(podate);
                        if (podate != "")
                        {
                            tw.Write(podate);
                           
                        }
                       
                            tw.Write(Asc28);
                       
                        string ctrctno = r.Row["ContractNo"].ToString();
                        if (ctrctno != "")
                        {
                            tw.Write(ctrctno);
                         
                        }
                       
                            tw.Write(Asc28);
                     
                        string ctrctdate = r.Row["ContractDate"].ToString();
                        ctrctdate = convertdate(ctrctdate);
                        if (ctrctdate != "")
                        {
                            tw.Write(ctrctdate);
                            
                        }
                        tw.Write(Asc28);
                      
                        string lcno = r.Row["LCNo"].ToString();
                        if (lcno != "")
                        {
                            tw.Write(lcno);
                            
                        }
                            tw.Write(Asc28);
                       
                        berunfile.AppendLine("LC No : " + lcno);

                        string lcdate = r.Row["LCDate"].ToString();
                        lcdate = convertdate(lcdate);
                        if (lcdate != "")
                        {
                            tw.Write(lcdate);
                           
                        }
                            tw.Write(Asc28);
                          
                        berunfile.AppendLine("LC Date : " + lcdate);

                        string svbno = r.Row["SVBRefNo"].ToString();
                        if (svbno != "")
                        {
                            tw.Write(svbno);
                          
                        }
                     
                            tw.Write(Asc28);
                      
                        berunfile.AppendLine("SVB Ref No : " + svbno);

                        string svbdate = r.Row["SVBRefDate"].ToString();
                        svbdate = convertdate(svbdate);
                        if (svbdate != "")
                        {
                            tw.Write(svbdate);
                           
                        }
                            tw.Write(Asc28);
                       
                        berunfile.AppendLine("SVB Ref Date : " + svbdate);
                        string prosvbassblevalue = r.Row["AssableStatus"].ToString();
                        string svbassblevalue = r.Row["AssableLoadingRate"].ToString();
                       // if (svbassblevalue != "0.00000")
                        if (prosvbassblevalue != "")//prosvbassblevalue != "~Select~" || prosvbassblevalue != null ||
                        {
                            if (prosvbassblevalue != "~Select~")
                            {
                                if (prosvbassblevalue != null)
                                {
                                    tw.Write(svbassblevalue);
                                }
                            }
                        }
                            tw.Write(Asc28);
                        string SVBduty = r.Row["DutyLoadingRate"].ToString();
                        if (SVBduty != "0.00000")
                        {
                            tw.Write(SVBduty);
                        }
                            tw.Write(Asc28);
                        SVBflag = r.Row["Loadingon"].ToString();
                        if (SVBflag != "")
                        {
                            if (SVBflag != "~Select~")
                            {
                                tw.Write(SVBflag);
                            }
                        }
                     
                            tw.Write(Asc28);
                    
                      
                        if (prosvbassblevalue != "~Select~")
                        {
                            tw.Write(prosvbassblevalue);
                        }
                            tw.Write(Asc28);

                            string proSVBduty = r.Row["DutyStatus"].ToString();
                            if (proSVBduty != "~Select~")
                        {
                            tw.Write(proSVBduty);
                        }
                            tw.Write(Asc28);
                        string customcode = r.Row["CustomHouse"].ToString();
                        if (customcode != "")
                        {
                            tw.Write(customcode);
                         
                        }
                            tw.Write(Asc28);
                        berunfile.AppendLine("Custom House : " + customcode);

                        string supname = r.Row["ConsignorName"].ToString();
                        if (supname != "")
                        {
                            tw.Write(supname);
                        }
                        else
                        {
                            bemanfile.AppendLine("Supplier Name");
                            fields = "1";
                        }
                            tw.Write(Asc28);
                        string supadd1="";
                        string supadde = r.Row["ConsignorNameaddress"].ToString();
                        if (supadde != "")
                        {
                            supadde = supadde.Replace("\n", "");
                            if (supadde.Length > 35)
                            {
                                 supadd1 = supadde.Substring(0, 35);
                                supadde = supadde.Remove(0, 35);
                                tw.Write(supadd1);
                            }
                            else
                            {
                                 supadd1=supadde;
                                 if (supadd1.Length > 0)
                                 {
                                     tw.Write(supadd1);
                                     int n = supadd1.Length;
                                     supadde = supadde.Remove(0, n);
                                 }
                            }
                          
                        }
                     
                            tw.Write(Asc28);

                            string supadd2 = "";
                            if (supadde.Length > 35)
                            {
                                 supadd2 = supadde.Substring(0, 35);
                                supadde = supadde.Remove(0, 35);
                                tw.Write(supadd2);
                            }
                            else
                            {
                                supadd2 = supadde;
                                if (supadd2.Length > 0)
                                {
                                    tw.Write(supadd2);
                                    int n = supadd2.Length;
                                    supadde = supadde.Remove(0, n);
                                }
                            }
                        tw.Write(Asc28);
                        string supadd3 = "";
                        if (supadde.Length > 35)
                        {
                            supadd3 = supadde.Substring(0, 35);
                            int n = supadd3.Length;
                            supadde = supadde.Remove(0, n);
                            tw.Write(supadd3);
                        }
                        else
                        {
                            supadd3 = supadde;
                            if (supadd3.Length > 0)
                            {
                                tw.Write(supadd3);
                            }
                        }
                        tw.Write(Asc28);
                        string supcntry = r.Row["ConsignorCountry"].ToString();
                        if (supcntry != "")
                        {
                            tw.Write(supcntry);
                         
                        }
                    
                            tw.Write(Asc28);
                       
                        string suppin = "";
                        if (suppin != "")
                        {
                            tw.Write(suppin);
                           
                        }
                       
                            tw.Write(Asc28);
                       
                        string seelername = r.Row["SellerName"].ToString();
                        if (seelername != "")
                        {

                            tw.Write(seelername);
                           
                        }
                     
                            tw.Write(Asc28);
                       
                        berunfile.AppendLine("Seller Name : " + seelername);
                          string seladd1 ="";
                        string seladd = r.Row["SellerNameaddress"].ToString();
                        if (seladd != "")
                        {
                            seladd = seladd.Replace("\n", "");
                            if (seladd.Length > 35)
                            {
                                 seladd1 = seladd.Substring(0, 35);
                                seladd = seladd.Remove(0, 35);
                                tw.Write(seladd1);
                            }
                            else
                            {
                               seladd1 = seladd;
                               if (seladd1.Length > 0)
                               {
                                   tw.Write(seladd1);
                                   int n = seladd1.Length;
                                   seladd = seladd.Remove(0, n);
                               }
                            }
                           
                        }
                       
                            tw.Write(Asc28);
                        string seladd2 = "";
                            if (seladd.Length > 35)
                            {
                                 seladd2 = seladd.Substring(0, 35);
                                seladd = seladd.Remove(0, 35);
                                
                                    tw.Write(seladd2);
                               
                            }
                            else
                            {
                                seladd2 = seladd;
                                if (seladd2.Length > 0)
                                {
                                    tw.Write(seladd2);
                                    int n = seladd2.Length;
                                    seladd = seladd.Remove(0, n);
                                }
                            }
                        berunfile.AppendLine("Seller Name address : " + seladd1);

                            tw.Write(Asc28);
                        
                        string seladd3 = "";
                        if (seladd3 != "")
                        {
                            tw.Write(seladd3);
                            
                        }
                    
                            tw.Write(Asc28);
                        
                        string selcntry = "";

                        if (r.Row["SellerCountry"].ToString() != "~Select~")
                        {
                            selcntry = r.Row["SellerCountry"].ToString();
                            if (selcntry != "~Select~")
                            {
                                tw.Write(selcntry);
                            }
                        }
                        tw.Write(Asc28);
                        berunfile.AppendLine("Seller Country : " + selcntry);

                        string selpin = "";
                        if (selpin != "")
                        {
                            tw.Write(selpin);
                        }
                            tw.Write(Asc28);
                      
                        string brokername = r.Row["BrokerName"].ToString();
                        if (brokername != "")
                        {
                            tw.Write(brokername);
                        }
                      
                            tw.Write(Asc28);
                       
                        berunfile.AppendLine("Broker Name : " + brokername);
                             string brkadd1="";
                        string brkadd = r.Row["BrokerNameaddress"].ToString();
                      if (brkadd != "")
                        {
                            brkadd = brkadd.Replace("\n", "");
                            if (brkadd.Length > 35)
                            {
                                 brkadd1 = brkadd.Substring(0, 35);
                                brkadd = brkadd.Remove(0, 35);
                                tw.Write(brkadd1);
                            }
                            else
                            {
                               brkadd1 = brkadd;
                               brkadd = brkadd.Remove(0, 35);
                                 tw.Write(brkadd1);
                            }
                           
                        }
                       
                            tw.Write(Asc28);
                        string brkadd2 = "";
                            if (brkadd.Length > 35)
                            {
                                 brkadd2 = brkadd.Substring(0, 35);
                                brkadd = brkadd.Remove(0, 35);
                                tw.Write(brkadd2);
                            }
                            else
                            {
                                brkadd2 = brkadd;
                                if (brkadd2.Length > 0)
                                {
                                    tw.Write(brkadd2);
                                }
                            }

                        berunfile.AppendLine("Broker Name address : " + brkadd1);
                            tw.Write(Asc28);
                       
                        string brkadd3 = "";
                        if (brkadd3 != "")
                        {
                            tw.Write(brkadd3);
                          
                        }
                     
                            tw.Write(Asc28);
                      
                        string brkcntry = "";
                        if (r.Row["BrokerCountry"].ToString() != "~Select~")
                        {
                            brkcntry = r.Row["BrokerCountry"].ToString();
                            if (brkcntry != "~Select~")
                            {
                                tw.Write(brkcntry);
                               
                            }
 
                        }
                        tw.Write(Asc28);
                        berunfile.AppendLine("Broker Country : " + brkcntry);

                        string brkpin = "";
                        if (brkpin != "")
                        {
                            tw.Write(brkpin);
                           
                        }
                      
                            tw.Write(Asc28);
                       
                        string invvalue = r.Row["InvoiceProductValues"].ToString();
                        if (invvalue != "")
                        {
                            tw.Write(invvalue);
                           
                        }
                        else
                        {
                            bemanfile.AppendLine("Invoice Values");
                            fields = "1";
                        }
                            tw.Write(Asc28);
                       
                        berunfile.AppendLine("Invoice Product Values : " + invvalue);

                        string inco = r.Row["InvoiceTerms"].ToString();
                        if (inco != "")
                        {
                            tw.Write(inco);

                        }
                        else
                        {
                            bemanfile.AppendLine("Invoice Terms");
                            fields = "1";
                        }
                            tw.Write(Asc28);
                       
                        berunfile.AppendLine("Invoice Terms : " + inco);

                        string invcur = r.Row["InvoiceCurrency"].ToString();
                        if (invcur != "")
                        {
                            tw.Write(invcur);

                        }
                        else
                        {
                            bemanfile.AppendLine("Invoice Currency");
                            fields = "1";
                        }
                            tw.Write(Asc28);
                      
                        berunfile.AppendLine("Invoice Currency : " + invcur);

                        string NatureofDiscount = "";
                        if (NatureofDiscount != "")
                        {
                            tw.Write(NatureofDiscount);
                        }
                    
                            tw.Write(Asc28);
                       
                        string disrate = r.Row["DiscountRate"].ToString();
                        if (disrate != "0.00")
                        {
                            tw.Write(disrate);
                           
                        }
                   
                            tw.Write(Asc28);
                      
                        berunfile.AppendLine("Discount Rate : " + disrate);

                        string disamount = r.Row["DiscountAmount"].ToString();
                        if (disamount != "0.00")
                        {
                            tw.Write(disamount);
                           
                        }
                      
                            tw.Write(Asc28);
                       
                        berunfile.AppendLine("Discount Rate : " + disamount);

                        string hssloadrate = "";
                        if (hssloadrate != "")
                        {
                            tw.Write(hssloadrate);
                           
                        }
                     
                            tw.Write(Asc28);
                      
                        string hssloadamt = "";
                        if (hssloadamt != "")
                        {
                            tw.Write(hssloadamt);
                           
                        }
                     
                            tw.Write(Asc28);
                     
                        string freightvalue = r.Row["FreightAmount"].ToString();
                        if (freightvalue != "0.00")
                        {
                            tw.Write(freightvalue);
                          
                        }
                    
                            tw.Write(Asc28);
                       
                        berunfile.AppendLine("Freight Amount : " + freightvalue);

                        string freightrateage = "";// r.Row["FreightRate"].ToString();
                        //if (freightrateage != "0.00")
                        //{
                        //    tw.Write(freightrateage);
                           
                        //}
                            tw.Write(Asc28);
                      
                        berunfile.AppendLine("Freight Rate : " + freightrateage);
                        string freightactual = "";
                        //if (freightrateage == "20.00")
                        //{
                        if (Convert.ToDouble(freightvalue) != 0)
                        {
                            freightactual = "Y";
                        }

                        //}
                       
                        //if (freightactual != "")
                        //{
                            tw.Write(freightactual);
                            
                        //}
                            tw.Write(Asc28);
                       
                        string frecur = r.Row["FreightCurrency"].ToString();
                        if (frecur != "" && frecur != "~Select~")
                        {
                            tw.Write(frecur);
                           
                        }
                      
                            tw.Write(Asc28);
                      
                        berunfile.AppendLine("Freight Currency : " + frecur);

                        string instvalue = "";// r.Row["InsuranceAmount"].ToString();
                        //string instvalue = "0.00";
                        //if (instvalue != "0.00")
                        //{
                        //    tw.Write(instvalue);
                            
                        //}
                      
                            tw.Write(Asc28);
                   
                        berunfile.AppendLine("Insurance Amount : " + instvalue);

                        string insrate = r.Row["InsuranceRate"].ToString();
                        berunfile.AppendLine("Insurance Rate : " + insrate);
                        if (insrate != "0.0000")
                       // if (insrate != "0.00")
                        {
                            tw.Write(insrate);

                        }
                        
                            tw.Write(Asc28);


                            string inscur = "";// r.Row["InsuranceCurrency"].ToString();
                            //string inscur = "";
                        berunfile.AppendLine("Insurance Currency : " + inscur);
                        if (inscur != "~Select~" && inscur != "")
                        {
                            tw.Write(inscur);
                        }
                      
                            tw.Write(Asc28);
                        
                        string misccharge = r.Row["MisAmount"].ToString();
                        berunfile.AppendLine("Mis Amount : " + misccharge);
                        if (misccharge != "0.00")
                        {
                            tw.Write(misccharge);
                            
                        }
                        
                            tw.Write(Asc28);
                       
                        string misccurrency = r.Row["MisCurrency"].ToString();
                        berunfile.AppendLine("Mis Currency : " + misccurrency);
                        if (misccurrency != "~Select~" && misccurrency != "")
                        {
                            tw.Write(misccurrency);
                           
                        }
                       
                            tw.Write(Asc28);


                            string misrate = "";// r.Row["MisRate"].ToString();
                        //berunfile.AppendLine("Mis Rate : " + misrate);
                        //if (misrate != "0.0000")
                        //{
                        //    tw.Write(misrate);
                           
                        //}
                    
                            tw.Write(Asc28);
                            string loadrate = r.Row["LandingRate"].ToString();
                            berunfile.AppendLine("Landing Rate : " + loadrate);
                            if (loadrate != "0.0000")
                            {
                                tw.Write(loadrate);
                            }

                            tw.Write(Asc28);
                            string landrate = r.Row["LoadingAmount"].ToString();
                        if (landrate != "0.0000")
                        {
                            tw.Write(landrate);
                           
                        }
                     
                            tw.Write(Asc28);
                            
                      
                           

                        string loadcurrency = r.Row["LoadingCurrency"].ToString();
                        berunfile.AppendLine("Loading Currency : " + loadcurrency);
                        if (loadcurrency != "~Select~" && loadcurrency != "")
                        {
                            tw.Write(loadcurrency);
                           
                        }
                      
                            tw.Write(Asc28);


                            string loadcharge = r.Row["LoadingAmount"].ToString();
                            berunfile.AppendLine("Loading Amount : " + loadcharge);
                            if (loadcharge != "0.0000")
                            {
                                tw.Write(loadcharge);

                            }

                            tw.Write(Asc28);

                        string agencycommcharge = r.Row["AgencyAmount"].ToString();
                        berunfile.AppendLine("Agency Amount : " + agencycommcharge);
                        if (agencycommcharge != "0.0000")
                        {
                            tw.Write(agencycommcharge);
                            
                        }
                     
                            tw.Write(Asc28);

                        string agencycommcurrency = r.Row["AgencyCurrency"].ToString();
                        berunfile.AppendLine("Agency Currency : " + agencycommcurrency);
                        if (agencycommcurrency != "~Select~" && agencycommcurrency != "")
                        {
                            tw.Write(agencycommcurrency);
                            
                        }
                      
                            tw.Write(Asc28);

                        string agencycommrate = r.Row["AgencyRate"].ToString();
                        berunfile.AppendLine("Agency Rate : " + agencycommrate);
                        if (agencycommrate != "0.0000")
                        {
                            tw.Write(agencycommrate);
                            
                        }
                            tw.Write(Asc28);

                        string naturetran = r.Row["InvoiceNatureofTrans"].ToString();
                        berunfile.AppendLine("Invoice Nature of Trans : " + naturetran);
                        if (naturetran != "")
                        {
                             tw.Write(naturetran);
                        }
                        else
                        {
                                bemanfile.AppendLine("Nature of Transaction");
                                fields = "1";
                            }

                            tw.Write(Asc28);
                      

                        string paymentterm = r.Row["InvoicePaymentTerms"].ToString();
                     
                            tw.Write(paymentterm);
               
                            tw.Write(Asc28);
                        string condattachsale1 = "";
                        if (condattachsale1 != "")
                        {
                            tw.Write(condattachsale1);
                            
                        }
                      
                            tw.Write(Asc28);
                      
                        string condattachsale2 = "";
                        if (condattachsale2 != "")
                        {
                            tw.Write(condattachsale2);
                           
                        }
                            tw.Write(Asc28);
                       
                        string condattachsale3 = "";
                        if (condattachsale3 != "")
                        {
                            tw.Write(condattachsale3);
                            
                        }
                            tw.Write(Asc28);
                        string condattachsale4 = "";
                        if (condattachsale4 != "")
                        {
                            tw.Write(condattachsale4);
                            
                        }
                            tw.Write(Asc28);
                        
                        string condattachsale5 = "";
                        if (condattachsale5 != "")
                        {
                            tw.Write(condattachsale5);
                           
                        }
                            tw.Write(Asc28);

                            string valumethod = r.Row["ValuationMethod"].ToString();
                        
                            tw.Write(valumethod);
                            
                    
                            tw.Write(Asc28);
                        
                        string actualinvno = r.Row["InvoiceNo"].ToString();
                        if (actualinvno != "")
                        {
                            tw.Write(actualinvno);
                            
                        }
                        tw.Write(Asc28);
                        
                        txtBeFile.Text = berunfile.ToString();
                        txtBeFile.Focus();

                        string others = "";
                        if (others != "")
                        {
                            tw.Write(others);
                          
                        }
                       
                        i++;
                    }
                }

                //***********************************************************************************
                //****************************** Table Name ITEMS *********************************
                //***********************************************************************************
                if (ds2.Tables["Jobs"].Rows.Count != 0)
                {
                    tw.Write(tw.NewLine);
                    tw.Write(items);
                    bemanfile.AppendLine("");
                    //bemanfile.AppendLine("Payment Table Fields");
                    bemanfile.AppendLine("Item Table Fields");
                    int i = 0;

                    foreach (DataRow rs in dt2.Rows)
                    {
                        DataRowView r = ds2.Tables["Jobs"].DefaultView[i];
                        string InvoiceNo = r.Row["InvoiceNo"].ToString();
                        SqlConnection conn3 = new SqlConnection(strcon);
                        conn3.Open();
                        string sqlQuery3 = "Select * from T_Product  where JobNo='" + jno + "' And InvoiceNo='" + InvoiceNo + "'";

                        SqlDataAdapter da3 = new SqlDataAdapter(sqlQuery3, conn3);
                        DataSet ds3 = new DataSet();
                        da3.Fill(ds3, "Jobs");
                        DataTable dt3 = ds3.Tables["Jobs"];
                        conn3.Close();
                        if (ds3.Tables["Jobs"].Rows.Count != 0)
                        {
                            int j = 0;
                            foreach (DataRow rw in dt3.Rows)
                            {
                                DataRowView row = ds3.Tables["Jobs"].DefaultView[j];
                                string invsrno = (i + 1).ToString();
                                string itemsrno = (j + 1).ToString();
                                string item1 = "";
                                string items1 = row.Row["ProductDesc"].ToString();
                                if (items1 == "")
                                {
                                    
                                    bemanfile.AppendLine("Item Description");
                                    fields = "1";
                                }
                                if (items1.Length > 60)
                                {
                                    item1 = items1.Substring(0, 60);
                                    items1 = items1.Remove(0, 60);
                                }
                                else
                                {
                                    item1 = items1;
                                    int n = item1.Length;
                                    items1 = items1.Remove(0, n);
                                }
                                berunfile.AppendLine("Product Description: " + item1);

                                string Ritc = row.Row["RITCNo"].ToString();
                                if (Ritc == "")
                                {
                                    bemanfile.AppendLine("Ritc Code");
                                    fields = "1";
                                }
                                berunfile.AppendLine("RITCNo : " + Ritc);

                                string qty = row.Row["Qty"].ToString();
                                if (qty == "")
                                {
                                    bemanfile.AppendLine("Qty");
                                    fields = "1";
                                }
                                berunfile.AppendLine("Qty : " + qty);

                                string unitqtycode = row.Row["Unit"].ToString();
                                if (unitqtycode == "")
                                {
                                    bemanfile.AppendLine("Qty Measurement");
                                    fields = "1";
                                }
                                berunfile.AppendLine("Unit of Mesurement: " + unitqtycode);
                                txtBeFile.Text = berunfile.ToString();
                                txtBeFile.Focus();

                                string item2 = "";
                                if (items1.Length > 60)
                                {
                                    item2 = items1.Substring(0, 60);
                                    items1 = items1.Remove(0, 60);

                                }
                                else
                                {
                                    item2 = items1;
                                   
                                }
                                string itemcat = "";
                                string descriptionitem1 = row.Row["GenericDesc"].ToString();
                                if(descriptionitem1.Length > 60)
                                {
                                    descriptionitem1 = descriptionitem1.Substring(0, 59);
                                }
                                
                                descriptionitem1 = descriptionitem1.Replace("\n", "");
                                berunfile.AppendLine("Generic Description: " + descriptionitem1);
                                //string descriptionitem2 = item2;
                                string accitem = row.Row["Accessories"].ToString();
                                string manname = row.Row["Manufacturer"].ToString();
                                if (manname == "")
                                {
                                    manname = "N.A.";
                                }
                                string brandname = row.Row["Brand"].ToString();
                                if (brandname == "")
                                {
                                    brandname = "N.A.";
                                }
                                string model = row.Row["Model"].ToString();
                                if (model == "")
                                {
                                    model = "N.A.";
                                }
                                string enduseitem = "";
                                string cntryoriginitem = cntryorigin;
                                if (cntryoriginitem == "")
                                {
                                    bemanfile.AppendLine("Country of Origin");
                                    fields = "1";
                                }
                                string cth = row.Row["CTHNo"].ToString();
                                if (cth == "")
                                {
                                    bemanfile.AppendLine("CTHNo");
                                    fields = "1";
                                }
                                string preferntialstandard = row.Row["RateType"].ToString();
                                //if (preferntialstandard == "Standard" || preferntialstandard=="S")
                                //{
                                //    preferntialstandard = "S";
                                //}
                                //else if (preferntialstandard == "Preferential" || preferntialstandard=="P")
                                //{
                                //    preferntialstandard = "P";
                                //}
                                //else
                                //{
                                //    bemanfile.AppendLine("Rate Type");
                                //    fields = "1";
                                //}
                                string ceth = row.Row["CETNo"].ToString();
                                if (ceth == "")
                                {
                                    bemanfile.AppendLine("CETH No");
                                    fields = "1";
                                }
                                string bcdnotn = row.Row["BasicDutyNotn"].ToString();
                                string bcdnotnsrno = row.Row["BasicDutySno"].ToString();
                                string cvdnotn = row.Row["AddlExNotn"].ToString();
                                string cvdnotnsrno = row.Row["AddlExSlNo"].ToString();
                                string addnnotn = "";
                                string addnnotnsrno = "";
                                string addnnotn1 = "";
                                string addnnotnsrno1 = "";
                                string othnotn = "";
                                string othnotnsrno = "";
                                string sadnotmumber = "";
                                string sadnotnsrno = "";

                                string CEXEDUCESSNotn = row.Row["ExEduCessNotn"].ToString();
                                string CEXEDUCESSNotnSlno = row.Row["ExEduCessSlNo"].ToString();

                                string ncdnotn = "";
                                string ncdnotnsrno = "";
                                string antydumpingdutynotn = "";
                                string antydumpingdutysrno = "";
                                string cthserialno = "";
                                string supserialno = "";
                                string qtyasperantynotnno = "";
                                string tarrifvaluenotn = "";
                                string tarrifvalueitemsrno = "";
                                string qtyaspertarrifnontn = "";
                                string saptanotn = row.Row["SAPTANotn"].ToString();
                                string saptasrno = row.Row["SAPTASNo"].ToString();
                                string healthnotn = "";
                                string healthnotnsrno = "";
                                string addcvdnotn = row.Row["ExCVDNotn"].ToString();
                                string addcvdsrno = row.Row["ExCVDSlNo"].ToString();
                                string aaggrecatedutynotn = "";
                                string aggrecateduthnotnsrno = "";
                                string safeguarddutynotn = "";
                                string safeguarddutynotnsrno = "";
                                string unitpriceinvoice = row.Row["UnitPrice"].ToString();
                                if(unitpriceinvoice=="0.000000")
                                {
                                    bemanfile.AppendLine("Invoice Value");
                                    fields = "1";
                                }
                                string discountrate = "";
                                string discountamt ="";
                                string qtyaspercth = "";
                                string qtyaspercth2 = "";
                                string svbrefnumber = "";
                                string svbrefdate = "";
                                string svbloadasseblevalue = "";
                                string svbloadonduty = "";
                                string svbflag = "";
                                string whetherloadfinalprovisionanlonassessblevalue = "";
                                string whetherloadfinalprovisionanlondutyvalue = "";
                                string customehousecode = "";
                                string policyparano = row.Row["PolicyPara"].ToString();//PolicyPara, PolicyYear
                                string policyyear = row.Row["PolicyYear"].ToString();
                                //if (policyparano!="")
                                string rspdeclared = row.Row["MRPDuty"].ToString();
                                if (rspdeclared == "True")
                                {
                                    rspdeclared = "Y";
                                }
                                else
                                {
                                    rspdeclared = "N";
                                }
                                string reimportitem = row.Row["MRPDuty"].ToString();
                                if (reimportitem == "True")
                                {
                                    reimportitem = "Y";
                                }
                                else
                                {
                                    reimportitem = "N";
                                }
                                string previousbeo = "";
                                string previousbedate = "";
                                string previousunitprice = "";
                                string previouscurrencycode = "";
                                string previouscustomesite = "";

                                  tw.Write(tw.NewLine);

                                tw.Write(Messagetype); tw.Write(Asc28); tw.Write(ReceiverID); tw.Write(Asc28); tw.Write(SequenceId); tw.Write(Asc28); tw.Write(jobdate); tw.Write(Asc28); tw.Write(BeNo); tw.Write(Asc28);
                                tw.Write(BeDate); tw.Write(Asc28); tw.Write(invsrno); tw.Write(Asc28); tw.Write(itemsrno); tw.Write(Asc28); tw.Write(qty); tw.Write(Asc28); tw.Write(unitqtycode); tw.Write(Asc28);
                                tw.Write(Ritc); tw.Write(Asc28); tw.Write(item1); tw.Write(Asc28); tw.Write(item2); tw.Write(Asc28); tw.Write(itemcat); tw.Write(Asc28); tw.Write(descriptionitem1); tw.Write(Asc28); 

                                tw.Write(accitem); tw.Write(Asc28); tw.Write(manname); tw.Write(Asc28); tw.Write(brandname); tw.Write(Asc28); tw.Write(model); tw.Write(Asc28); tw.Write(enduseitem); tw.Write(Asc28);
                                tw.Write(cntryorigin); tw.Write(Asc28); tw.Write(cth); tw.Write(Asc28); tw.Write(preferntialstandard); tw.Write(Asc28); tw.Write(ceth); tw.Write(Asc28); tw.Write(bcdnotn); tw.Write(Asc28);

                                tw.Write(bcdnotnsrno); tw.Write(Asc28); tw.Write(cvdnotn); tw.Write(Asc28); tw.Write(cvdnotnsrno); tw.Write(Asc28); tw.Write(addnnotn); tw.Write(Asc28); tw.Write(addnnotnsrno); tw.Write(Asc28);

                                tw.Write(addnnotn1); tw.Write(Asc28); tw.Write(addnnotnsrno1); tw.Write(Asc28); tw.Write(othnotn); tw.Write(Asc28); tw.Write(othnotnsrno); tw.Write(Asc28); tw.Write(sadnotmumber); tw.Write(Asc28);

                                tw.Write(sadnotnsrno); tw.Write(Asc28); tw.Write(CEXEDUCESSNotn); tw.Write(Asc28); tw.Write(CEXEDUCESSNotnSlno); tw.Write(Asc28);//To add the CVD EDU Notification And Serial No.
                                tw.Write(ncdnotn); tw.Write(Asc28); tw.Write(ncdnotnsrno); tw.Write(Asc28); tw.Write(antydumpingdutynotn); tw.Write(Asc28); tw.Write(antydumpingdutysrno); tw.Write(Asc28);

                                tw.Write(cthserialno); tw.Write(Asc28); tw.Write(supserialno); tw.Write(Asc28); tw.Write(qtyasperantynotnno); tw.Write(Asc28); tw.Write(tarrifvaluenotn); tw.Write(Asc28); tw.Write(tarrifvalueitemsrno); tw.Write(Asc28);

                                tw.Write(qtyaspertarrifnontn); tw.Write(Asc28); tw.Write(saptanotn); tw.Write(Asc28); tw.Write(saptasrno); tw.Write(Asc28); tw.Write(healthnotn); tw.Write(Asc28); tw.Write(healthnotnsrno); tw.Write(Asc28);

                                tw.Write(addcvdnotn); tw.Write(Asc28); tw.Write(addcvdsrno); tw.Write(Asc28); tw.Write(aaggrecatedutynotn); tw.Write(Asc28); tw.Write(aggrecateduthnotnsrno); tw.Write(Asc28); tw.Write(safeguarddutynotn); tw.Write(Asc28);

                                tw.Write(safeguarddutynotnsrno); tw.Write(Asc28); tw.Write(unitpriceinvoice); tw.Write(Asc28); tw.Write(discountrate); tw.Write(Asc28); tw.Write(discountamt); tw.Write(Asc28); tw.Write(qtyaspercth); tw.Write(Asc28);

                                tw.Write(qtyaspercth2); tw.Write(Asc28); tw.Write(svbrefnumber); tw.Write(Asc28); tw.Write(svbrefdate); tw.Write(Asc28); tw.Write(svbloadasseblevalue); tw.Write(Asc28); tw.Write(svbloadonduty); tw.Write(Asc28);

                                tw.Write(svbflag); tw.Write(Asc28); tw.Write(whetherloadfinalprovisionanlonassessblevalue); tw.Write(Asc28); tw.Write(whetherloadfinalprovisionanlondutyvalue); tw.Write(Asc28); tw.Write(customehousecode); tw.Write(Asc28); tw.Write(policyparano); tw.Write(Asc28);

                                tw.Write(policyyear); tw.Write(Asc28); tw.Write(rspdeclared); tw.Write(Asc28); tw.Write(reimportitem); tw.Write(Asc28); tw.Write(previousbeo); tw.Write(Asc28); tw.Write(previousbedate); tw.Write(Asc28);

                                tw.Write(previousunitprice); tw.Write(Asc28); tw.Write(previouscurrencycode); tw.Write(Asc28); tw.Write(previouscustomesite); 

                                j++;
                            }
                        }


                        i++;
                    }
                }
                 


                SqlConnection connlic = new SqlConnection(strcon);
                conn2.Open();
                string sqlQuerylic = "select * from T_Schemes  where jobNo='" + jno + "'";

                SqlDataAdapter dalic = new SqlDataAdapter(sqlQuerylic, connlic);
                DataSet dslic = new DataSet();
                dalic.Fill(dslic, "Jobs");
                DataTable dtlic = dslic.Tables["Jobs"];
                connlic.Close();
                if (dslic.Tables["Jobs"].Rows.Count != 0)
                {
                    tw.Write(tw.NewLine);
                    tw.Write(licence);
                    bemanfile.AppendLine("");
                    bemanfile.AppendLine("Licence Table Fields");
                    int j = 0;
                    foreach (DataRow ra in dtlic.Rows)
                    {
                        DataRowView r = dslic.Tables["Jobs"].DefaultView[j];
                        string invsrno = "";
                        string itemsrnoininv = "";
                        string itemsrnoinlicence = "";
                        string debitvalue = r.Row["DebitValue"].ToString(); ;
                        string debitqty = r.Row["Quantity"].ToString(); ;
                        string debitunitmeasure = "";
                        string licregno = r.Row["SchemeLicNo"].ToString();
                        if (licregno == "")
                        {
                            bemanfile.AppendLine("Licence Registered Number");
                        }
                        string licregdate = r.Row["SchemeLicDate"].ToString();
                        if (licregdate == "")
                        {
                            bemanfile.AppendLine("Licence Registered Date");
                        }
                        licregdate = convertdate(licregdate);
                        string liccode = r.Row["SchemeType"].ToString();
                        if (liccode == "")
                        {
                            bemanfile.AppendLine("Licence Code");
                        }
                        string ralicregport = r.Row["RegPort"].ToString(); 
                        if (ralicregport == "")
                        {
                            bemanfile.AppendLine("Registered Port");
                        }
                          tw.Write(tw.NewLine);
                        tw.Write(Messagetype); tw.Write(Asc28); tw.Write(ReceiverID); tw.Write(Asc28); tw.Write(SequenceId); tw.Write(Asc28); tw.Write(jobdate); tw.Write(Asc28); tw.Write(BeNo); tw.Write(Asc28);
                        tw.Write(BeDate); tw.Write(Asc28); tw.Write(invsrno); tw.Write(Asc28); tw.Write(itemsrnoininv); tw.Write(Asc28); tw.Write(itemsrnoinlicence); tw.Write(Asc28); tw.Write(debitvalue); tw.Write(Asc28);
                        tw.Write(debitqty); tw.Write(Asc28); tw.Write(debitunitmeasure); tw.Write(Asc28); tw.Write(licregno); tw.Write(Asc28); tw.Write(licregdate); tw.Write(Asc28); tw.Write(liccode); tw.Write(Asc28); tw.Write(ralicregport); 
                        j++;
                    }
                }
                 
                //retailsalew

                SqlConnection connrsp = new SqlConnection(strcon);
                connrsp.Open();
                string sqlQueryrsp = "select * from T_Product  where jobNo='" + jno + "'";

                SqlDataAdapter darsp = new SqlDataAdapter(sqlQueryrsp, connrsp);
                DataSet dsrsp = new DataSet();
                darsp.Fill(dsrsp, "Jobs");
                DataTable dtrsp = dsrsp.Tables["Jobs"];
                connrsp.Close();
                if (dsrsp.Tables["Jobs"].Rows.Count != 0)
                {
                    tw.Write(tw.NewLine);
                    tw.Write(rsp);
                    bemanfile.AppendLine();
                    bemanfile.AppendLine("RSP table Fields");
                    int j = 0;
                    foreach (DataRow rs in dtrsp.Rows)
                    {
                        DataRowView r = dsrsp.Tables["Jobs"].DefaultView[j];
                        string rspdeclared = r["MRPDuty"].ToString();
                        if (rspdeclared == "True")
                        {
                            string invsrnorsp = "";
                            string itemsrnoininvrsp = "";
                            string itemsrnoinrsp = r.Row["MRPSNo"].ToString();
                            string rsprs = r.Row["MRP"].ToString();
                            if (rsprs == "")
                            {
                                bemanfile.AppendLine("RSP");
                            }
                            string qtyrsp = r.Row["Qty"].ToString();
                            //Qty has no decimal so i split the qty with decimal points
                            //string[] qtyarr = qtyrsp.Split('.');
                            if (qtyrsp == "")
                            {
                                bemanfile.AppendLine("Quantity");
                            }
                            string desitemrsp = r.Row["ProductDesc"].ToString();
                            if (desitemrsp == "")
                            {
                                bemanfile.AppendLine("Description of Item");
                            }
                            string rspnotn = "";
                            string rspnotnsrno = "";

                            tw.Write(tw.NewLine);
                            tw.Write(Messagetype); tw.Write(Asc28); tw.Write(ReceiverID); tw.Write(Asc28); tw.Write(SequenceId); tw.Write(Asc28); tw.Write(jobdate); tw.Write(Asc28); tw.Write(BeNo); tw.Write(Asc28);
                            tw.Write(BeDate); tw.Write(Asc28); tw.Write(invsrnorsp); tw.Write(Asc28); tw.Write(itemsrnoininvrsp); tw.Write(Asc28); tw.Write(itemsrnoinrsp); tw.Write(Asc28); tw.Write(rsprs); tw.Write(Asc28);
                            tw.Write(qtyrsp); tw.Write(Asc28); tw.Write(desitemrsp); tw.Write(Asc28); tw.Write(rspnotn); tw.Write(Asc28); tw.Write(rspnotnsrno);
                            j++;
                        }
                    }
                }
                  

                ////DEPB


                //SqlConnection conndep = new SqlConnection(strcon);
                //conndep.Open();
                //string sqlQuerydep = "select * from T_Product  where jobNo='" + jno + "' and BCD Notification is not null and ";

                //SqlDataAdapter dadep = new SqlDataAdapter(sqlQuerydep, conndep);
                //DataSet dsdep = new DataSet();
                //dadep.Fill(dsdep, "Jobs");
                //DataTable dtdep = dsdep.Tables["Jobs"];
                //conndep.Close();
                //if (dsdep.Tables["Jobs"].Rows.Count != 0)
                //{
                //    tw.Write(tw.NewLine);
                //    tw.Write(debp);
                //    bemanfile.AppendLine();
                //    bemanfile.AppendLine("DEPB table Fields");
                //    int j = 0;
                //    foreach (DataRow r1 in dtdep.Rows)
                //    {
                //        DataRowView r = dsdep.Tables["Jobs"].DefaultView[j];
                //        string invsrnodep = "";
                //        string itemsrnoininvdep = "";
                //        string exemptionreq = "";

                //        string bcdnotn = "";
                //        if (bcdnotn == "")
                //        {
                //            bemanfile.AppendLine("BCD Notification");
                //        }
                //        string bcdnotnsrno = "";
                //        if(bcdnotnsrno=="")
                //        {
                //            bemanfile.AppendLine("BCD Notification Srno");
                //        }


                //        tw.Write(tw.NewLine);
                //        tw.Write(Messagetype); tw.Write(Asc28); tw.Write(ReceiverID); tw.Write(Asc28); tw.Write(SequenceId); tw.Write(Asc28); tw.Write(jobdate); tw.Write(Asc28); tw.Write(BeNo); tw.Write(Asc28);
                //        tw.Write(BeDate); tw.Write(Asc28); tw.Write(invsrnodep); tw.Write(Asc28); tw.Write(itemsrnoininvdep); tw.Write(Asc28); tw.Write(exemptionreq); tw.Write(Asc28); tw.Write(bcdnotn); tw.Write(Asc28);
                //        tw.Write(bcdnotnsrno);
                //        j++;
                //    }
                //}
                  
                //bond


                SqlConnection connbond = new SqlConnection(strcon);
                connbond.Open();
                string sqlQuerybond = "select * from T_ImportBondReg  where jobNo='" + jno + "'";

                SqlDataAdapter dabond = new SqlDataAdapter(sqlQuerybond, connbond);
                DataSet dsbond = new DataSet();
                dabond.Fill(dsbond, "Jobs");
                DataTable dtbond = dsbond.Tables["Jobs"];
                connbond.Close();
                if (dsbond.Tables["Jobs"].Rows.Count != 0)
                {
                    tw.Write(tw.NewLine);
                    tw.Write(bond);
                    bemanfile.AppendLine();
                    bemanfile.AppendLine("Bond table fields");
                    int j = 0;
                    foreach (DataRow r1 in dtbond.Rows)
                    {
                        DataRowView r = dsbond.Tables["Jobs"].DefaultView[j];
                        string bondnumber = r.Row["BondNumber"].ToString();
                        if (bondnumber == "")
                        {
                            bemanfile.AppendLine("Bondnumber");
                        }
                        string bondcode = r.Row["BondType"].ToString();
                        if (bondcode == "")
                        {
                            bemanfile.AppendLine("BondCode");
                        }
                        string bondport = ReceiverID;// "";
                        if (bondport == "")
                        {
                            bemanfile.AppendLine("BondPort");
                        }


                        tw.Write(tw.NewLine);
                        tw.Write(Messagetype); tw.Write(Asc28); tw.Write(ReceiverID); tw.Write(Asc28); tw.Write(SequenceId); tw.Write(Asc28); tw.Write(jobdate); tw.Write(Asc28); tw.Write(BeNo); tw.Write(Asc28);
                        tw.Write(BeDate); tw.Write(Asc28); tw.Write(bondnumber); tw.Write(Asc28); tw.Write(bondcode); tw.Write(Asc28); tw.Write(bondport); 
                        j++;
                    }
                }
                  
                //cerificate
                SqlConnection conncert = new SqlConnection(strcon);
                conncert.Open();
                string sqlQuerycert = "select * from T_ImportBondCertificate  where jobNo='" + jno + "'";

                SqlDataAdapter dacert = new SqlDataAdapter(sqlQuerycert, conncert);
                DataSet dscert = new DataSet();
                dacert.Fill(dscert, "Jobs");
                DataTable dtcert = dscert.Tables["Jobs"];
                conncert.Close();
                if (dscert.Tables["Jobs"].Rows.Count != 0)
                {
                    tw.Write(tw.NewLine);
                    tw.Write(cert);
                    bemanfile.AppendLine();
                    bemanfile.AppendLine("Certificate");

                    int j = 0;
                    foreach (DataRow r1 in dtcert.Rows)
                    {
                        DataRowView r = dscert.Tables["Jobs"].DefaultView[j];
                        string certnumber = r.Row["CertificateNo"].ToString();
                        if (certnumber == "")
                        {
                            bemanfile.AppendLine("Certificate Number");
                        }
                        string certdate = r.Row["CertificateDate"].ToString();
                        if (certdate == "")
                        {
                            bemanfile.AppendLine("Certificate Date");
                        }
                        else
                        {
                            certdate = convertdate(certdate);
                        }
                        string certtype = r.Row["CertificateType"].ToString();

                        if (certtype == "")
                        {
                            bemanfile.AppendLine("Certificate Type");
                        }
                          tw.Write(tw.NewLine);
                        tw.Write(Messagetype); tw.Write(Asc28); tw.Write(ReceiverID); tw.Write(Asc28); tw.Write(SequenceId); tw.Write(Asc28); tw.Write(jobdate); tw.Write(Asc28); tw.Write(BeNo); tw.Write(Asc28);
                        tw.Write(BeDate); tw.Write(Asc28); tw.Write(certnumber); tw.Write(Asc28); tw.Write(certdate); tw.Write(Asc28); tw.Write(certtype); 
                        j++;
                    }
                }
                  
                //customehousecodeimporter High Sea Sale Details.
                SqlConnection connhss = new SqlConnection(strcon);
                connhss.Open();
                string sqlQueryhss = "select * from T_Importer  where jobNo='" + jno + "'";

                SqlDataAdapter dahss = new SqlDataAdapter(sqlQueryhss, connhss);
                DataSet dshss = new DataSet();
                dahss.Fill(dshss, "Jobs");
                DataTable dthss = dshss.Tables["Jobs"];
                connhss.Close();
                if (saleflag != "N")
                {
                    if (dshss.Tables["Jobs"].Rows.Count != 0)
                    {
                        tw.Write(tw.NewLine);
                        tw.Write(hss);
                        bemanfile.AppendLine();
                        bemanfile.AppendLine("HSS table fields");

                        int j = 0;
                        foreach (DataRow r1 in dthss.Rows)
                        {
                            DataRowView r = dshss.Tables["Jobs"].DefaultView[j];
                            string iec = r.Row["HighIECode"].ToString();
                            if (iec == "")
                            {
                                bemanfile.AppendLine("IEC");
                            }
                            string branchserialnumber = r.Row["BranchSno"].ToString();
                            if (branchserialnumber == "")
                            {
                                bemanfile.AppendLine("Branch Serial Number");
                            }
                            string importername = r.Row["SellerName"].ToString();
                            string importeraddress1 = r.Row["HighAddress"].ToString();
                            string importeraddress2 = r.Row["HighState"].ToString();
                            string importercity = r.Row["HighCity"].ToString();
                            string importerpin = r.Row["HighZipCode"].ToString();
                            tw.Write(tw.NewLine);
                            tw.Write(Messagetype); tw.Write(Asc28); tw.Write(ReceiverID); tw.Write(Asc28); tw.Write(SequenceId); tw.Write(Asc28); tw.Write(jobdate); tw.Write(Asc28); tw.Write(BeNo); tw.Write(Asc28);
                            tw.Write(BeDate); tw.Write(Asc28); tw.Write(iec); tw.Write(Asc28); tw.Write(branchserialnumber); tw.Write(Asc28); tw.Write(importername); tw.Write(Asc28); tw.Write(importeraddress1); tw.Write(Asc28);
                            tw.Write(importeraddress2); tw.Write(Asc28); tw.Write(importercity); tw.Write(Asc28); tw.Write(importerpin);
                            j++;
                        }
                    }
                }
                 
                //reimport
                SqlConnection connrimp = new SqlConnection(strcon);
                connrimp.Open();
                string sqlQueryrimp = "select * from T_ProductITCLicence  where jobNo='" + jno + "'";

                SqlDataAdapter darimp = new SqlDataAdapter(sqlQueryrimp, connrimp);
                DataSet dsrimp = new DataSet();
                darimp.Fill(dsrimp, "Jobs");
                DataTable dtrimp = dsrimp.Tables["Jobs"];
                connrimp.Close();
                if (dsrimp.Tables["Jobs"].Rows.Count != 0)
                {
                    tw.Write(tw.NewLine);
                    tw.Write(reimport);
                    bemanfile.AppendLine();
                    bemanfile.AppendLine("REIMPORT");
                    int j = 0;
                    foreach (DataRow ra in dtrimp.Rows)
                    {
                        DataRowView r = dsrimp.Tables["Jobs"].DefaultView[j];
                        string invsrnorimp = "";
                        string itemsrnoininvimp = "";
                        string shipbillnoimp = "";
                        if (shipbillnoimp == "")
                        {
                            bemanfile.AppendLine("Shipping Bill No.");
                        }
                        string shipbilldatteimp = "";
                        if (shipbilldatteimp == "")
                        {
                            bemanfile.AppendLine("Shipping Bill Date.");
                        }
                        string portexportimp = "";
                        if (portexportimp == "")
                        {
                            bemanfile.AppendLine("Port of Export");
                        }
                        string invnosb = "";
                        string itemnosb = "";
                        string notnoimp = "";
                        if (notnoimp == "")
                        {
                            bemanfile.AppendLine("Notification No");
                        }
                        string notsrnoimp = "";
                        if (notsrnoimp == "")
                        {
                            bemanfile.AppendLine("Notification Sr No");
                        }
                        string exportfreight = "";
                        string exportinsurance = "";
                        string customsduty = "";
                        string exciseduty = "";

                          tw.Write(tw.NewLine);
                        tw.Write(Messagetype); tw.Write(Asc28); tw.Write(ReceiverID); tw.Write(Asc28); tw.Write(SequenceId); tw.Write(Asc28); tw.Write(jobdate); tw.Write(Asc28); tw.Write(BeNo); tw.Write(Asc28);
                        tw.Write(BeDate); tw.Write(Asc28); tw.Write(invsrnorimp); tw.Write(Asc28); tw.Write(itemsrnoininvimp); tw.Write(Asc28); tw.Write(shipbillnoimp); tw.Write(Asc28); tw.Write(shipbilldatteimp); tw.Write(Asc28);
                        tw.Write(portexportimp); tw.Write(Asc28); tw.Write(invnosb); tw.Write(Asc28); tw.Write(itemnosb); tw.Write(Asc28); tw.Write(notnoimp); tw.Write(Asc28); tw.Write(notsrnoimp); tw.Write(Asc28);
                        tw.Write(exportfreight); tw.Write(Asc28); tw.Write(exportinsurance); tw.Write(Asc28); tw.Write(customsduty); tw.Write(Asc28); tw.Write(exciseduty); 
                        j++;
                    }
                }
                 
                //sbeduty
                SqlConnection connsbe = new SqlConnection(strcon);
                connsbe.Open();
                string sqlQuerysbe = "select * from T_ProductITCLicence  where jobNo='" + jno + "'";

                SqlDataAdapter dasbe = new SqlDataAdapter(sqlQuerysbe, connsbe);
                DataSet dssbe = new DataSet();
                dasbe.Fill(dssbe, "Jobs");
                DataTable dtsbe = dssbe.Tables["Jobs"];
                connsbe.Close();
                if (dssbe.Tables["Jobs"].Rows.Count != 0)
                {
                    tw.Write(tw.NewLine);
                    tw.Write(sbeduty);
                    int j = 0;
                    foreach (DataRow ra in dtsbe.Rows)
                    {
                        DataRowView r = dssbe.Tables["Jobs"].DefaultView[j];
                        string invsrnosbe = "";
                        string itemsrnoininvsbe = "";

                        string notnosbe = "";
                        string notsrnosbe = "";
                        string dutytypesbe = "";
                        string aadldutyflagsbe = "";

                          tw.Write(tw.NewLine);
                        tw.Write(Messagetype); tw.Write(Asc28); tw.Write(ReceiverID); tw.Write(Asc28); tw.Write(SequenceId); tw.Write(Asc28); tw.Write(jobdate); tw.Write(Asc28); tw.Write(BeNo); tw.Write(Asc28);
                        tw.Write(BeDate); tw.Write(Asc28); tw.Write(invsrnosbe); tw.Write(Asc28); tw.Write(itemsrnoininvsbe); tw.Write(Asc28); tw.Write(notnosbe); tw.Write(Asc28); tw.Write(notsrnosbe); tw.Write(Asc28);
                        tw.Write(dutytypesbe); tw.Write(Asc28); tw.Write(aadldutyflagsbe);
                        j++;
                    }
                }

                //***********************************************************************************
                //****************************** Table Name IGM *********************************
                //***********************************************************************************

                string igmno = "";
                string igmdate = "";
                SqlConnection connigm = new SqlConnection(strcon);
                connigm.Open();
                string sqlQueryigm = "select LocalIGMNo,LocalIGMDate,GLDInwardDate,GatewayIGMNo, GatewayIGMDate,ShipmentPortCode, MasterBLNo, MasterBLDate, HouseBLNo, HouseBLDate,NoOfPackages,GrossWeight,GrossWeightUnit,PackagesUnit,MarksNos from  T_ShipmentDetails  where JobNo='" + jno + "'";
                SqlDataAdapter daigm = new SqlDataAdapter(sqlQueryigm, connigm);
                DataSet dsigm = new DataSet();
                daigm.Fill(dsigm, "Jobs");
                DataTable dtigm = dsigm.Tables["Jobs"];
                connigm.Close();
                if (dsigm.Tables["Jobs"].Rows.Count != 0)
                {
                    tw.Write(tw.NewLine);
                    tw.Write(igms);
                    bemanfile.AppendLine("");
                    bemanfile.AppendLine("IGM Table Fields");
                    int j = 0;
                    foreach (DataRow ra in dtigm.Rows)
                    {
                        DataRowView r = dsigm.Tables["Jobs"].DefaultView[j];
                         igmno = r.Row["LocalIGMNo"].ToString();
                         //if (igmno == "")
                         //{
                         //    if (DOCTYPE != "A")
                         //    {
                                 //bemanfile.AppendLine("IGM No");
                                 //fields = "1";
                            // }
                         //}
                         igmdate = r.Row["LocalIGMDate"].ToString();
                         //if (igmdate == "")
                         //{
                         //    if (DOCTYPE != "A")
                         //    {
                         //        bemanfile.AppendLine("IGM Date");
                         //        fields = "1";
                         //    }
                         //}
                        igmdate = convertdate(igmdate);
                        string inwarddate = r.Row["GLDInwardDate"].ToString();
                        inwarddate = convertdate(inwarddate);
                        //if (inwarddate == "")
                        //{
                        //    if (DOCTYPE != "A" && SVBflag != "Y")
                        //    {
                        //        bemanfile.AppendLine("Inwarddate Date");
                        //        fields = "1";
                        //    }
                        //}
                        string gatewayigmno = r.Row["GatewayIGMNo"].ToString();
                        //if (gatewayigmno == "")
                        //{
                        //    bemanfile.AppendLine("GatewayIGM No");
                        //    fields="1";
                        //}
                        string gatewayigmdate = r.Row["GatewayIGMDate"].ToString();
                        gatewayigmdate = convertdate(gatewayigmdate);
                        //if (gatewayigmdate == "")
                        //{
                        //    bemanfile.AppendLine("GatewayIGM Date");
                        //    fields = "1";
                        //}
                        string portofreporting = "";//r.Row["ShipmentPortCode"].ToString();
                        //if (portofreporting == "")
                        //{
                        //    if (DOCTYPE != "A")
                        //    {
                        //        bemanfile.AppendLine("Port of Reporting");
                        //        fields = "1";
                        //    }
                        //}
                        string mawblno = r.Row["MasterBLNo"].ToString();
                        string mawbldate = r.Row["MasterBLDate"].ToString();
                        mawbldate = convertdate(mawbldate);
                       // mawbldate = "";
                        string hawblno = r.Row["HouseBLNo"].ToString();
                        string hawbdate = r.Row["HouseBLDate"].ToString();
                        hawbdate = convertdate(hawbdate);
                        string totalpkgs = r.Row["NoOfPackages"].ToString();

                        if (totalpkgs == "")
                        {
                            bemanfile.AppendLine("Total No of Packages");
                            fields = "1";
                        }
                        string grosswt = r.Row["GrossWeight"].ToString();
                        //if (grosswt == "")
                        //{
                        //    bemanfile.AppendLine("Gross Weight");
                        //    fields = "1";
                        //}
                        //else
                        //{
                        //    string[] gross = grosswt.Split('.');
                        //    int n = gross[1].Length;
                        //    if (n != 3)
                        //    {
                        //        bemanfile.AppendLine("Gross Weight must be 3 digit decimal");
                        //        fields = "1";
                        //    }
                        //}
                        string unitqtycode = r.Row["GrossWeightUnit"].ToString();
                        if (unitqtycode == "~Select~")
                        {
                            unitqtycode = "";
                        }
                        string packcode = r.Row["PackagesUnit"].ToString();
                        if (packcode == "~Select~")
                        {
                            packcode = "";
                        }
                        string marksnmumber1 = r.Row["MarksNos"].ToString();
                        string marksnmumber2 = "";
                        string marksnmumber3 = "";
                          tw.Write(tw.NewLine);
                        tw.Write(Messagetype); tw.Write(Asc28); tw.Write(ReceiverID); tw.Write(Asc28); tw.Write(SequenceId); tw.Write(Asc28); tw.Write(jobdate); tw.Write(Asc28); tw.Write(BeNo); tw.Write(Asc28);
                        tw.Write(BeDate); tw.Write(Asc28); tw.Write(igmno); tw.Write(Asc28); tw.Write(igmdate); tw.Write(Asc28); tw.Write(inwarddate); tw.Write(Asc28); tw.Write(gatewayigmno); tw.Write(Asc28);
                        tw.Write(gatewayigmdate); tw.Write(Asc28); tw.Write(portofreporting); tw.Write(Asc28); tw.Write(mawblno); tw.Write(Asc28); tw.Write(mawbldate); tw.Write(Asc28); tw.Write(hawblno); tw.Write(Asc28);
                        tw.Write(hawbdate); tw.Write(Asc28); tw.Write(totalpkgs); tw.Write(Asc28); tw.Write(grosswt); tw.Write(Asc28); tw.Write(unitqtycode); tw.Write(Asc28); tw.Write(packcode); tw.Write(Asc28);
                        tw.Write(marksnmumber1); tw.Write(Asc28); tw.Write(marksnmumber2); tw.Write(Asc28); tw.Write(marksnmumber3); 
                        j++;
                    }
                }
                  
                //container


                SqlConnection conncont = new SqlConnection(strcon);
                conncont.Open();
                string sqlQuerycont = "select ContainerType, ContainerNo, SealNo,LoadType from T_ShipmentContainerInfo  where JobNo='" + jno + "'";

                SqlDataAdapter dacont = new SqlDataAdapter(sqlQuerycont, conncont);
                DataSet dscont = new DataSet();
                dacont.Fill(dscont, "Jobs");
                DataTable dtcont = dscont.Tables["Jobs"];
                conncont.Close();
                if (dscont.Tables["Jobs"].Rows.Count != 0)
                {
                    tw.Write(tw.NewLine);
                    bemanfile.AppendLine();
                    bemanfile.AppendLine("Container table fields");
                    tw.Write(container);
                    int j = 0;
                    foreach (DataRow ra in dtcont.Rows)
                    {
                        DataRowView r = dscont.Tables["Jobs"].DefaultView[j];
                        string igmnocont = igmno;
                        string igmdatecont = igmdate;

                        string conttyyy = r.Row["LoadType"].ToString();
                        conttyyy = conttyyy.Substring(0, 1);
                        string containerno = r.Row["ContainerNo"].ToString();
                        string sealno = r.Row["SealNo"].ToString();
                        if (sealno =="&nbsp;" || sealno=="")
                        {
                            sealno = "N.A.";
                        }

                          tw.Write(tw.NewLine);
                        tw.Write(Messagetype); tw.Write(Asc28); tw.Write(ReceiverID); tw.Write(Asc28); tw.Write(SequenceId); tw.Write(Asc28); tw.Write(jobdate); tw.Write(Asc28); tw.Write(BeNo); tw.Write(Asc28);
                        tw.Write(BeDate); tw.Write(Asc28); tw.Write(igmnocont); tw.Write(Asc28); tw.Write(igmdatecont); tw.Write(Asc28); tw.Write(conttyyy); tw.Write(Asc28); tw.Write(containerno); tw.Write(Asc28);
                        tw.Write(sealno); tw.Write(Asc28);
                        j++;
                    }
                }
                  
                //ctx


                SqlConnection connctx = new SqlConnection(strcon);
                connctx.Open();
                string sqlQueryctx = "select * from  T_CommercialTax  where jobNo='" + jno + "'";

                SqlDataAdapter dactx = new SqlDataAdapter(sqlQueryctx, connctx);
                DataSet dsctx = new DataSet();
                dactx.Fill(dsctx, "Jobs");
                DataTable dtctx = dsctx.Tables["Jobs"];
                connctx.Close();
                if (dsctx.Tables["Jobs"].Rows.Count != 0)
                {
                    tw.Write(tw.NewLine);
                    tw.Write(ctx);
                    int j = 0;
                    foreach (DataRow ra in dtctx.Rows)
                    {
                        DataRowView r = dsctx.Tables["Jobs"].DefaultView[j];
                        string satecode = r.Row["Ctax_StateCode"].ToString();
                        string commercialtaxtype = r.Row["Ctax_Type"].ToString();
                        string commercialtaxregno = r.Row["Ctax_RegNo"].ToString();
                          tw.Write(tw.NewLine);
                        tw.Write(Messagetype); tw.Write(Asc28); tw.Write(ReceiverID); tw.Write(Asc28); tw.Write(SequenceId); tw.Write(Asc28); tw.Write(jobdate); tw.Write(Asc28); tw.Write(BeNo); tw.Write(Asc28);
                        tw.Write(BeDate); tw.Write(Asc28); tw.Write(satecode); tw.Write(Asc28); tw.Write(commercialtaxtype); tw.Write(Asc28); tw.Write(commercialtaxregno); tw.Write(Asc28);
                        j++;
                    }
                }
                
                //amend


                SqlConnection connamd = new SqlConnection(strcon);
                connamd.Open();
                string sqlQueryamd = "select * from T_ProductITCLicence  where jobNo='" + jno + "'";

                SqlDataAdapter daamd = new SqlDataAdapter(sqlQueryamd, connamd);
                DataSet dsamd = new DataSet();
                daamd.Fill(dsamd, "Jobs");
                DataTable dtamd = dsamd.Tables["Jobs"];
                connamd.Close();
                if (dsamd.Tables["Jobs"].Rows.Count != 0)
                {
                    tw.Write(tw.NewLine);
                    tw.Write(amend);
                    int j = 0;
                    foreach (DataRow ra in dtamd.Rows)
                    {
                        DataRowView r = dsamd.Tables["Jobs"].DefaultView[j];
                        string ammendcode = "";
                        string reasonforamend = "";

                        string requestletternumber = "";
                        string requestdate = "";

                        tw.Write(tw.NewLine);
                        tw.Write(Messagetype); tw.Write(Asc28); tw.Write(ReceiverID); tw.Write(Asc28); tw.Write(SequenceId); tw.Write(Asc28); tw.Write(jobdate); tw.Write(Asc28); tw.Write(BeNo); tw.Write(Asc28);
                        tw.Write(BeDate); tw.Write(Asc28); tw.Write(ammendcode); tw.Write(Asc28); tw.Write(reasonforamend); tw.Write(Asc28); tw.Write(requestletternumber); tw.Write(Asc28); tw.Write(requestdate); tw.Write(Asc28);
                        tw.Write(ammendcode); tw.Write(Asc28);
                        j++;
                    }
                }
                  tw.Write(tw.NewLine);
                tw.Write(endbe);
                tw.Write(tw.NewLine);
                tw.Write(TText);
                tw.Write(Asc28);
                tw.Write(SequenceId);
                tw.Write(tw.NewLine);
                if (fields!="1")
                {
                    tw.Flush();
                    tw.Close();
                    berunfile.AppendLine("Successfully Created BE File in following Location: '" + FILEPATH + "'");
                    txtBeFile.Text = berunfile.ToString();
                    updatejob();
                }
                else
                {
                    tw.Flush();
                    tw.Close();
                    string[] filePaths11 = Directory.GetFiles(@pathdir1);
                    foreach (string filePathss in filePaths11)
                    {
                        //if (!CheckIfFileIsBeingUsed(filePathss))
                        //{
                        if (filePathss == @pathdir1 + pathdir)
                        {
                            File.Delete(filePathss);
                        }
                        // }

                    }
                    txtBeFile.Text = bemanfile.ToString();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        //protected void ValidateJobNo(string jno)
        //{
        //    try
        //    {

        //        string Messagetype = "F";
        //        string jobdate = "";
        //        string BeNo = "";
        //        string BeDate = "";
        //        string Betype = "";
        //        string Ieccode = "";
        //        string branchsrno = "";
        //        string importer = "";
        //        string address1 = "";
        //        string address2 = "";
        //        string city = "";
        //        string state = "";
        //        string pin = "";
        //        string classs = "";
        //        string modetran = "";
        //        string imptype = "";
        //        string kanbe = "";
        //        string saleflag = "";
        //        string origin = "";
        //        string chacode = "";
        //        string cntryorigin = "";
        //        string cntryconsignment = "";
        //        string portshipment = "";
        //        string grennchannel = "";
        //        string sec48 = "";
        //        string priorbe = "";
        //        string dealcode = "";
        //        string firstcheck = "";
        //        string wmscode = "";
        //        string wmscustomsid = "";
        //        string wmsbeno = "";
        //        string wmsbedate = "";
        //        string pkgs = "";
        //        string pkgcode = "";
        //        string gwt = "";
        //        string uom = "";
        //        string xbond = "";
        //        string misnloadrate = "";
        //        int unicode = 29;
        //        char character = (char)unicode;
        //        string Asc28 = character.ToString();
        //        string SVBflag = "";
        //        string DOCTYPE = "";
        //        int unicode1 = 10;
        //        char character1 = (char)unicode1;
        //        string nLine = character1.ToString();
        //        //string jdate = DateTime.Now.Date.ToString("dd/MM/yyyy");


        //        //jdate = convertdate(jdate);

        //        string jTime = DateTime.Now.ToString("HH:mm");
        //        string[] jT = jTime.Split(':');
        //        jTime = jT[0] + jT[1];
        //        string be = "<TABLE>BE";
        //        string exch = "<TABLE>EXCHANGE";
        //        string Permission = "<TABLE>PERMISSION";
        //        string invoice = "<TABLE>INVOICE";
        //        string misc_ch = "<TABLE>MISC_CH";
        //        string items = "<TABLE>ITEMS";
        //        string licence = "<TABLE>LICENCE";
        //        string rsp = "<TABLE>RSP";
        //        string debp = "<TABLE>DEPB";
        //        string bond = "<TABLE>BOND";
        //        string cert = "<TABLE>CERT";
        //        string hss = "<TABLE>HSS";
        //        string reimport = "<TABLE>REIMPORT";
        //        string sbeduty = "<TABLE>SBEDUTY";
        //        string igms = "<TABLE>IGMS";
        //        string container = "<TABLE>CONTAINER";
        //        string ctx = "<TABLE>CTX";
        //        string amend = "<TABLE>AMEND";

        //        string endbe = "<END-BE>";

        //        //***********************************************************************************
        //        //****************************** Table Name BE **************************************
        //        //***********************************************************************************
        //        SqlConnection conn1 = new SqlConnection(strcon);
        //        conn1.Open();
        //        string sqlQuery1 = "select * from View_JobImporterShipment where jobNo='" + jno + "'";
        //        SqlDataAdapter da1 = new SqlDataAdapter(sqlQuery1, conn1);
        //        DataSet ds1 = new DataSet();
        //        da1.Fill(ds1, "Jobs");
        //        conn1.Close();
        //        if (ds1.Tables["Jobs"].Rows.Count != 0)
        //        {
        //            bemanfile.AppendLine("Be Mandatory Fields");
        //            jobdate = ds1.Tables["Jobs"].Rows[0]["JobReceivedDate"].ToString();
        //            jobdate = convertdate(jobdate);
        //            berunfile.AppendLine("Job Received Date : " + jobdate);
        //            BeNo = ds1.Tables["Jobs"].Rows[0]["BeNo"].ToString();
        //            BeDate = ds1.Tables["Jobs"].Rows[0]["BeDate"].ToString();
        //            BeDate = convertdate(BeDate);
        //            Betype = ds1.Tables["Jobs"].Rows[0]["BeType"].ToString();
        //            DOCTYPE = ds1.Tables["Jobs"].Rows[0]["Docfillingstatus"].ToString();
        //            Ieccode = ds1.Tables["Jobs"].Rows[0]["IECodeNo"].ToString();
        //            if (Ieccode == "")
        //            {
        //                bemanfile.AppendLine("IecCode");
        //                fields = "1";
        //            }
        //            berunfile.AppendLine("IE Code : " + Ieccode);
        //            branchsrno = ds1.Tables["Jobs"].Rows[0]["BranchSno"].ToString();
        //            if (branchsrno == "")
        //            {
        //                bemanfile.AppendLine("Branch SerialNo");
        //                fields = "1";
        //            }
        //            berunfile.AppendLine("Branch Sno : " + branchsrno);
        //            importer = ds1.Tables["Jobs"].Rows[0]["Importer"].ToString();
        //            berunfile.AppendLine("Job Received Date : " + jobdate);
        //            string address = ds1.Tables["Jobs"].Rows[0]["Address"].ToString();
        //            address = address.Trim();
        //            if (address.Length > 35)
        //            {
        //                address = address.Replace("\n", "");
        //                address1 = address.Substring(0, 35);
        //                address = address.Remove(0, 35);
        //                if (address.Length > 35)
        //                {
        //                    address2 = address.Substring(0, 35);
        //                }
        //                else
        //                {
        //                    address2 = address;
        //                }
        //            }
        //            berunfile.AppendLine("Address : " + address1);
        //            city = ds1.Tables["Jobs"].Rows[0]["City"].ToString();
        //            berunfile.AppendLine("City : " + city);
        //            state = ds1.Tables["Jobs"].Rows[0]["State"].ToString();
        //            berunfile.AppendLine("State : " + state);
        //            pin = ds1.Tables["Jobs"].Rows[0]["ZipCode"].ToString();
        //            berunfile.AppendLine("ZipCode : " + pin);
        //            txtBeFile.Text = berunfile.ToString();

        //            classs = "N";
        //            if (ds1.Tables["Jobs"].Rows[0]["Mode"].ToString() == "Air")
        //            {
        //                modetran = "A";
        //            }
        //            else if (ds1.Tables["Jobs"].Rows[0]["Mode"].ToString() == "Sea")
        //            {
        //                modetran = "S";
        //            }
        //            else if (ds1.Tables["Jobs"].Rows[0]["Mode"].ToString() == "Land")
        //            {
        //                modetran = "L";
        //            }
        //            else
        //            {
        //                bemanfile.AppendLine("Mode of Transport");
        //                fields = "1";
        //            }
        //            imptype = ds1.Tables["Jobs"].Rows[0]["ImporterType"].ToString();
        //            berunfile.AppendLine("Importer Type : " + imptype);

        //            if (ds1.Tables["Jobs"].Rows[0]["ChkKachha"].ToString() == "Yes")
        //            {
        //                kanbe = "Y";
        //            }
        //            else if (ds1.Tables["Jobs"].Rows[0]["ChkKachha"].ToString() == "No")
        //            {
        //                kanbe = "N";
        //            }
        //            else
        //            {
        //                bemanfile.AppendLine("Kachcha Be");
        //                fields = "1";

        //            }
        //            if (ds1.Tables["Jobs"].Rows[0]["HighSeaSale"].ToString() == "True")
        //            {
        //                saleflag = "Y";
        //            }
        //            else if (ds1.Tables["Jobs"].Rows[0]["HighSeaSale"].ToString() == "False")
        //            {
        //                saleflag = "N";
        //            }
        //            else
        //            {
        //                bemanfile.AppendLine("High Sea Sale flag");
        //                fields = "1";
        //            }
        //            origin = ds1.Tables["Jobs"].Rows[0]["ShipmentUneceCode"].ToString();
        //            if (origin == "")
        //            {
        //                bemanfile.AppendLine("Port of Origin");
        //                fields = "1";
        //            }
        //            berunfile.AppendLine("Port Of Origin : " + origin);

        //            string CHAQuery = "SELECT Chano FROM M_CompanyInfo where Branch ='" + (string)Session["ZONE"] + "'";
        //            DataSet dsCha = objCommonDL.GetDataSet(CHAQuery);
        //            if (dsCha.Tables["Table"].Rows.Count != 0)
        //            {
        //                DataRowView row1 = dsCha.Tables["Table"].DefaultView[0];
        //                chacode = row1["Chano"].ToString();
        //            }

        //            //chacode = "AACCP4978KCH004";
        //            cntryorigin = ds1.Tables["Jobs"].Rows[0]["CountryOrigincode"].ToString();
        //            if (cntryorigin == "")
        //            {
        //                bemanfile.AppendLine("CountryOrigin");
        //                fields = "1";
        //            }
        //            berunfile.AppendLine("Country Origin: " + cntryorigin);

        //            cntryconsignment = ds1.Tables["Jobs"].Rows[0]["Shipmentcountrycode"].ToString();
        //            if (cntryconsignment == "")
        //            {
        //                bemanfile.AppendLine("Country of Consignment");
        //                fields = "1";
        //            }
        //            berunfile.AppendLine("Country of Shipment : " + cntryconsignment);

        //            if (modetran == "S")
        //            {
        //                portshipment = ds1.Tables["Jobs"].Rows[0]["ShipmentUneceCode"].ToString();
        //            }
        //            else
        //            {
        //                portshipment = ds1.Tables["Jobs"].Rows[0]["Shipmentcountrycode"].ToString();
        //            }

        //            if (portshipment == "")
        //            {
        //                bemanfile.AppendLine("Port of shipment");
        //                fields = "1";
        //            }
        //            berunfile.AppendLine("Port of shipment : " + portshipment);

        //            if (ds1.Tables["Jobs"].Rows[0]["ChkGreen"].ToString() == "True")
        //            {
        //                grennchannel = "Y";
        //            }
        //            else
        //            {
        //                grennchannel = "N";
        //            }
        //            if (ds1.Tables["Jobs"].Rows[0]["ChkUnderSec48"].ToString() == "True")
        //            {
        //                sec48 = "Y";
        //            }
        //            else
        //            {
        //                sec48 = "N";
        //            }
        //            priorbe = ds1.Tables["Jobs"].Rows[0]["DocFillingStatus"].ToString();
        //            if (priorbe == "Advance")
        //            {
        //                priorbe = "A";
        //            }
        //            else if (priorbe == "Normal")
        //            {
        //                priorbe = "N";
        //            }
        //            dealcode = "0240029";
        //            if (ds1.Tables["Jobs"].Rows[0]["ChkFirstChk"].ToString() == "True")
        //            {
        //                firstcheck = "Y";
        //            }
        //            else
        //            {
        //                firstcheck = "N";
        //            }
        //            //pkgs = ds1.Tables["Jobs"].Rows[0]["NoOfPackages"].ToString();
        //            pkgs = "";
        //            berunfile.AppendLine("No Of Packages : " + pkgs);

        //            pkgcode = "";
        //            //gwt = ds1.Tables["Jobs"].Rows[0]["GrossWeight"].ToString();
        //            gwt = "";
        //            berunfile.AppendLine("Gross Weight : " + gwt);

        //            //uom = ds1.Tables["Jobs"].Rows[0]["GrossWeightUnit"].ToString();
        //            uom = "";
        //            berunfile.AppendLine("Gross Weight Unit : " + uom);
        //            txtBeFile.Text = berunfile.ToString();
        //            txtBeFile.Focus();

        //        }

        //        string file = string.Empty;
        //        string mn = DateTime.Now.ToString("MM");
        //        string dd = DateTime.Now.ToString("dd");
        //        string datetime = DateTime.Now.Year.ToString() + mn + dd + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString();

        //        string pathdir = "00" + jno + "14" + ".be";

        //        //To remove the old be file ////////////////////////////////
        //        string paths = AppDomain.CurrentDomain.BaseDirectory;

        //        string pathdir1 = Path.Combine(paths, @"TempFile\");
        //        //path1 = pathdir + Path.GetFileName(FileUpload1.PostedFile.FileName);

        //        string[] filePaths = Directory.GetFiles(@pathdir1);


        //        System.IO.File.WriteAllText(AppDomain.CurrentDomain.BaseDirectory + @"TempFile\" + pathdir, "");
        //        file = AppDomain.CurrentDomain.BaseDirectory + @"TempFile\" + pathdir;

        //        FILEPATH = file;

        //        FileStream fs = new FileStream(@file, FileMode.OpenOrCreate, FileAccess.ReadWrite);
        //        StreamWriter tw = new StreamWriter(fs);
        //        //string pathdir = "00" + SequenceId + "14" + ".be";

        //        //System.IO.File.WriteAllText(AppDomain.CurrentDomain.BaseDirectory + @"TempFile\" + pathdir,"");
        //        //file = AppDomain.CurrentDomain.BaseDirectory + @"TempFile\" + pathdir;

        //        //FILEPATH = file;

        //        //FileStream fs = new FileStream(@file, FileMode.OpenOrCreate, FileAccess.ReadWrite);
        //        //StreamWriter tw = new StreamWriter(fs);


        //        filePath = file;
        //        tw.Write(HText); tw.Write(Asc28); tw.Write(zz); tw.Write(SenderID); tw.Write(Asc28); tw.Write(zz); tw.Write(Asc28);
        //        tw.Write(ReceiverID); tw.Write(Asc28); tw.Write(VersionNo); tw.Write(Asc28); tw.Write(fType);
        //        tw.Write(MessageID); tw.Write(Asc28); tw.Write(SequenceId); tw.Write(Asc28); tw.Write(jobdate); tw.Write(Asc28); tw.Write(jTime);
        //        tw.Write(tw.NewLine);
        //        tw.Write(be);
        //        tw.Write(tw.NewLine);
        //        tw.Write(Messagetype); tw.Write(Asc28); tw.Write(ReceiverID); tw.Write(Asc28); tw.Write(SequenceId); tw.Write(Asc28); tw.Write(jobdate); tw.Write(Asc28); tw.Write(BeNo); tw.Write(Asc28);
        //        tw.Write(BeDate); tw.Write(Asc28); tw.Write(Betype); tw.Write(Asc28); tw.Write(Ieccode); tw.Write(Asc28); tw.Write(branchsrno); tw.Write(Asc28); tw.Write(importer); tw.Write(Asc28);
        //        tw.Write(address1); tw.Write(Asc28); tw.Write(address2); tw.Write(Asc28); tw.Write(city); tw.Write(Asc28); tw.Write(state); tw.Write(Asc28); tw.Write(pin); tw.Write(Asc28);
        //        tw.Write(classs); tw.Write(Asc28); tw.Write(modetran); tw.Write(Asc28); tw.Write(imptype); tw.Write(Asc28); tw.Write(kanbe); tw.Write(Asc28); tw.Write(saleflag); tw.Write(Asc28);
        //        tw.Write(origin); tw.Write(Asc28); tw.Write(chacode); tw.Write(Asc28); tw.Write(cntryorigin); tw.Write(Asc28); tw.Write(cntryconsignment); tw.Write(Asc28); tw.Write(portshipment); tw.Write(Asc28);
        //        tw.Write(grennchannel); tw.Write(Asc28); tw.Write(sec48); tw.Write(Asc28); tw.Write(priorbe); tw.Write(Asc28); tw.Write(dealcode); tw.Write(Asc28); tw.Write(firstcheck); tw.Write(Asc28);
        //        tw.Write(wmscode); tw.Write(Asc28); tw.Write(wmscustomsid); tw.Write(Asc28); tw.Write(wmsbeno); tw.Write(Asc28); tw.Write(wmsbedate); tw.Write(Asc28); tw.Write(pkgs); tw.Write(Asc28);
        //        tw.Write(pkgcode); tw.Write(Asc28); tw.Write(gwt); tw.Write(Asc28); tw.Write(uom); tw.Write(Asc28); tw.Write(xbond); tw.Write(Asc28); tw.Write(misnloadrate);


        //        //***********************************************************************************
        //        //****************************** Table Name EXCHANGE **************************************
        //        //***********************************************************************************

        //        SqlConnection conn2 = new SqlConnection(strcon);
        //        conn2.Open();
        //        string sqlQuery2 = "select * from T_InvoiceDetails  where jobNo='" + jno + "'";
        //        SqlDataAdapter da2 = new SqlDataAdapter(sqlQuery2, conn2);
        //        DataSet ds2 = new DataSet();
        //        da2.Fill(ds2, "Jobs");
        //        DataTable dt2 = ds2.Tables["Jobs"];
        //        conn2.Close();
        //        if (ds2.Tables["Jobs"].Rows.Count != 0)
        //        {
        //            tw.Write(tw.NewLine);
        //            tw.Write(exch);
        //            int yt = 0;
        //            foreach (DataRow rss in dt2.Rows)
        //            {

        //                string invcur = dt2.Rows[yt]["InvoiceCurrency"].ToString();
        //                string frecur = dt2.Rows[yt]["FreightCurrency"].ToString();
        //                if (invcur == "~Select~")
        //                {
        //                    invcur = "";
        //                }
        //                if (frecur == "~Select~")
        //                {
        //                    frecur = "";
        //                }
        //                berunfile.AppendLine("Invoice Currency : " + invcur);
        //                int n = 1;
        //                string[] cur = new string[2];
        //                if (invcur == frecur || frecur == "")
        //                {
        //                    n = 1;
        //                    cur[0] = invcur;
        //                }
        //                else
        //                {
        //                    n = 2;
        //                    cur[0] = invcur;
        //                    cur[1] = frecur;
        //                }
        //                for (int i = 0; i < n; i++)
        //                {
        //                    string stdardcur = "Y";
        //                    string Unitrs = "";
        //                    string Rates = "";
        //                    string effect = "";
        //                    string bankname = "";
        //                    string certnum = "";
        //                    string certdate = "";
        //                    tw.Write(tw.NewLine);
        //                    tw.Write(Messagetype); tw.Write(Asc28); tw.Write(ReceiverID); tw.Write(Asc28); tw.Write(SequenceId); tw.Write(Asc28); tw.Write(jobdate); tw.Write(Asc28); tw.Write(BeNo); tw.Write(Asc28);
        //                    tw.Write(BeDate); tw.Write(Asc28); tw.Write(cur[i]); tw.Write(Asc28); tw.Write(stdardcur); tw.Write(Asc28); tw.Write(Unitrs); tw.Write(Asc28); tw.Write(Rates); tw.Write(Asc28);
        //                    tw.Write(effect); tw.Write(Asc28); tw.Write(bankname); tw.Write(Asc28); tw.Write(certnum); tw.Write(Asc28); tw.Write(certdate);
        //                }
        //            }
        //        }

        //        //***********************************************************************************
        //        //****************************** Table Name INVOICE *********************************
        //        //***********************************************************************************
        //        if (ds2.Tables["Jobs"].Rows.Count != 0)
        //        {
        //            tw.Write(tw.NewLine);
        //            tw.Write(invoice);
        //            bemanfile.AppendLine("");
        //            bemanfile.AppendLine("Invoice Table Details");
        //            int i = 0;
        //            foreach (DataRow rss in dt2.Rows)
        //            {
        //                tw.Write(tw.NewLine);
        //                tw.Write(Messagetype); tw.Write(Asc28); tw.Write(ReceiverID); tw.Write(Asc28); tw.Write(SequenceId); tw.Write(Asc28); tw.Write(jobdate); tw.Write(Asc28);
        //                if (BeNo != "")
        //                {
        //                    tw.Write(BeNo);

        //                }
        //                tw.Write(Asc28);
        //                if (BeDate != "")
        //                {
        //                    tw.Write(BeDate);

        //                }
        //                tw.Write(Asc28);
        //                DataRowView r = ds2.Tables["Jobs"].DefaultView[i];
        //                string invsrno = (i + 1).ToString();
        //                if (invsrno != "")
        //                {
        //                    tw.Write(invsrno);

        //                }

        //                tw.Write(Asc28);

        //                string invdate = r.Row["InvoiceDate"].ToString();
        //                invdate = convertdate(invdate);
        //                if (invdate != "")
        //                {
        //                    tw.Write(invdate);

        //                }

        //                tw.Write(Asc28);

        //                berunfile.AppendLine("Invoice Date : " + invdate);
        //                string pono = "";
        //                if (pono != "")
        //                {
        //                    tw.Write(pono);

        //                }

        //                tw.Write(Asc28);

        //                string podate = r.Row["PODate"].ToString();
        //                podate = convertdate(podate);
        //                if (podate != "")
        //                {
        //                    tw.Write(podate);

        //                }

        //                tw.Write(Asc28);

        //                string ctrctno = r.Row["ContractNo"].ToString();
        //                if (ctrctno != "")
        //                {
        //                    tw.Write(ctrctno);

        //                }

        //                tw.Write(Asc28);

        //                string ctrctdate = r.Row["ContractDate"].ToString();
        //                ctrctdate = convertdate(ctrctdate);
        //                if (ctrctdate != "")
        //                {
        //                    tw.Write(ctrctdate);

        //                }
        //                tw.Write(Asc28);

        //                string lcno = r.Row["LCNo"].ToString();
        //                if (lcno != "")
        //                {
        //                    tw.Write(lcno);

        //                }
        //                tw.Write(Asc28);

        //                berunfile.AppendLine("LC No : " + lcno);

        //                string lcdate = r.Row["LCDate"].ToString();
        //                lcdate = convertdate(lcdate);
        //                if (lcdate != "")
        //                {
        //                    tw.Write(lcdate);

        //                }
        //                tw.Write(Asc28);

        //                berunfile.AppendLine("LC Date : " + lcdate);

        //                string svbno = r.Row["SVBRefNo"].ToString();
        //                if (svbno != "")
        //                {
        //                    tw.Write(svbno);

        //                }

        //                tw.Write(Asc28);

        //                berunfile.AppendLine("SVB Ref No : " + svbno);

        //                string svbdate = r.Row["SVBRefDate"].ToString();
        //                svbdate = convertdate(svbdate);
        //                if (svbdate != "")
        //                {
        //                    tw.Write(svbdate);

        //                }
        //                tw.Write(Asc28);

        //                berunfile.AppendLine("SVB Ref Date : " + svbdate);
        //                string prosvbassblevalue = r.Row["AssableStatus"].ToString();
        //                string svbassblevalue = r.Row["AssableLoadingRate"].ToString();
        //                // if (svbassblevalue != "0.00000")
        //                if (prosvbassblevalue != "")//prosvbassblevalue != "~Select~" || prosvbassblevalue != null ||
        //                {
        //                    if (prosvbassblevalue != "~Select~")
        //                    {
        //                        if (prosvbassblevalue != null)
        //                        {
        //                            tw.Write(svbassblevalue);
        //                        }
        //                    }
        //                }
        //                tw.Write(Asc28);
        //                string SVBduty = r.Row["DutyLoadingRate"].ToString();
        //                if (SVBduty != "0.00000")
        //                {
        //                    tw.Write(SVBduty);
        //                }
        //                tw.Write(Asc28);
        //                SVBflag = r.Row["Loadingon"].ToString();
        //                if (SVBflag != "")
        //                {
        //                    tw.Write(SVBflag);
        //                }

        //                tw.Write(Asc28);


        //                if (prosvbassblevalue != "~Select~")
        //                {
        //                    tw.Write(prosvbassblevalue);
        //                }
        //                tw.Write(Asc28);

        //                string proSVBduty = r.Row["DutyStatus"].ToString();
        //                if (proSVBduty != "~Select~")
        //                {
        //                    tw.Write(proSVBduty);
        //                }
        //                tw.Write(Asc28);
        //                string customcode = r.Row["CustomHouse"].ToString();
        //                if (customcode != "")
        //                {
        //                    tw.Write(customcode);

        //                }
        //                tw.Write(Asc28);
        //                berunfile.AppendLine("Custom House : " + customcode);

        //                string supname = r.Row["ConsignorName"].ToString();
        //                if (supname != "")
        //                {
        //                    tw.Write(supname);
        //                }
        //                else
        //                {
        //                    bemanfile.AppendLine("Supplier Name");
        //                    fields = "1";
        //                }
        //                tw.Write(Asc28);
        //                string supadd1 = "";
        //                string supadde = r.Row["ConsignorNameaddress"].ToString();
        //                if (supadde != "")
        //                {
        //                    supadde = supadde.Replace("\n", "");
        //                    if (supadde.Length > 35)
        //                    {
        //                        supadd1 = supadde.Substring(0, 35);
        //                        supadde = supadde.Remove(0, 35);
        //                        tw.Write(supadd1);
        //                    }
        //                    else
        //                    {
        //                        supadd1 = supadde;
        //                        if (supadd1.Length > 0)
        //                        {
        //                            tw.Write(supadd1);
        //                            int n = supadd1.Length;
        //                            supadde = supadde.Remove(0, n);
        //                        }
        //                    }

        //                }

        //                tw.Write(Asc28);

        //                string supadd2 = "";
        //                if (supadde.Length > 35)
        //                {
        //                    supadd2 = supadde.Substring(0, 35);
        //                    supadde = supadde.Remove(0, 35);
        //                    tw.Write(supadd2);
        //                }
        //                else
        //                {
        //                    supadd2 = supadde;
        //                    if (supadd2.Length > 0)
        //                    {
        //                        tw.Write(supadd2);
        //                        int n = supadd2.Length;
        //                        supadde = supadde.Remove(0, n);
        //                    }
        //                }
        //                tw.Write(Asc28);
        //                string supadd3 = "";
        //                if (supadde.Length > 35)
        //                {
        //                    supadd3 = supadde.Substring(0, 35);
        //                    int n = supadd3.Length;
        //                    supadde = supadde.Remove(0, n);
        //                    tw.Write(supadd3);
        //                }
        //                else
        //                {
        //                    supadd3 = supadde;
        //                    if (supadd3.Length > 0)
        //                    {
        //                        tw.Write(supadd3);
        //                    }
        //                }
        //                tw.Write(Asc28);
        //                string supcntry = r.Row["ConsignorCountry"].ToString();
        //                if (supcntry != "")
        //                {
        //                    tw.Write(supcntry);

        //                }

        //                tw.Write(Asc28);

        //                string suppin = "";
        //                if (suppin != "")
        //                {
        //                    tw.Write(suppin);

        //                }

        //                tw.Write(Asc28);

        //                string seelername = r.Row["SellerName"].ToString();
        //                if (seelername != "")
        //                {

        //                    tw.Write(seelername);

        //                }

        //                tw.Write(Asc28);

        //                berunfile.AppendLine("Seller Name : " + seelername);
        //                string seladd1 = "";
        //                string seladd = r.Row["SellerNameaddress"].ToString();
        //                if (seladd != "")
        //                {
        //                    seladd = seladd.Replace("\n", "");
        //                    if (seladd.Length > 35)
        //                    {
        //                        seladd1 = seladd.Substring(0, 35);
        //                        seladd = seladd.Remove(0, 35);
        //                        tw.Write(seladd1);
        //                    }
        //                    else
        //                    {
        //                        seladd1 = seladd;
        //                        if (seladd1.Length > 0)
        //                        {
        //                            tw.Write(seladd1);
        //                            int n = seladd1.Length;
        //                            seladd = seladd.Remove(0, n);
        //                        }
        //                    }

        //                }

        //                tw.Write(Asc28);
        //                string seladd2 = "";
        //                if (seladd.Length > 35)
        //                {
        //                    seladd2 = seladd.Substring(0, 35);
        //                    seladd = seladd.Remove(0, 35);

        //                    tw.Write(seladd2);

        //                }
        //                else
        //                {
        //                    seladd2 = seladd;
        //                    if (seladd2.Length > 0)
        //                    {
        //                        tw.Write(seladd2);
        //                        int n = seladd2.Length;
        //                        seladd = seladd.Remove(0, n);
        //                    }
        //                }
        //                berunfile.AppendLine("Seller Name address : " + seladd1);

        //                tw.Write(Asc28);

        //                string seladd3 = "";
        //                if (seladd3 != "")
        //                {
        //                    tw.Write(seladd3);

        //                }

        //                tw.Write(Asc28);

        //                string selcntry = "";

        //                if (r.Row["SellerCountry"].ToString() != "~Select~")
        //                {
        //                    selcntry = r.Row["SellerCountry"].ToString();
        //                    if (selcntry != "~Select~")
        //                    {
        //                        tw.Write(selcntry);
        //                    }
        //                }
        //                tw.Write(Asc28);
        //                berunfile.AppendLine("Seller Country : " + selcntry);

        //                string selpin = "";
        //                if (selpin != "")
        //                {
        //                    tw.Write(selpin);
        //                }
        //                tw.Write(Asc28);

        //                string brokername = r.Row["BrokerName"].ToString();
        //                if (brokername != "")
        //                {
        //                    tw.Write(brokername);
        //                }

        //                tw.Write(Asc28);

        //                berunfile.AppendLine("Broker Name : " + brokername);
        //                string brkadd1 = "";
        //                string brkadd = r.Row["BrokerNameaddress"].ToString();
        //                if (brkadd != "")
        //                {
        //                    brkadd = brkadd.Replace("\n", "");
        //                    if (brkadd.Length > 35)
        //                    {
        //                        brkadd1 = brkadd.Substring(0, 35);
        //                        brkadd = brkadd.Remove(0, 35);
        //                        tw.Write(brkadd1);
        //                    }
        //                    else
        //                    {
        //                        brkadd1 = brkadd;
        //                        brkadd = brkadd.Remove(0, 35);
        //                        tw.Write(brkadd1);
        //                    }

        //                }

        //                tw.Write(Asc28);
        //                string brkadd2 = "";
        //                if (brkadd.Length > 35)
        //                {
        //                    brkadd2 = brkadd.Substring(0, 35);
        //                    brkadd = brkadd.Remove(0, 35);
        //                    tw.Write(brkadd2);
        //                }
        //                else
        //                {
        //                    brkadd2 = brkadd;
        //                    if (brkadd2.Length > 0)
        //                    {
        //                        tw.Write(brkadd2);
        //                    }
        //                }

        //                berunfile.AppendLine("Broker Name address : " + brkadd1);
        //                tw.Write(Asc28);

        //                string brkadd3 = "";
        //                if (brkadd3 != "")
        //                {
        //                    tw.Write(brkadd3);

        //                }

        //                tw.Write(Asc28);

        //                string brkcntry = "";
        //                if (r.Row["BrokerCountry"].ToString() != "~Select~")
        //                {
        //                    brkcntry = r.Row["BrokerCountry"].ToString();
        //                    if (brkcntry != "~Select~")
        //                    {
        //                        tw.Write(brkcntry);

        //                    }

        //                }
        //                tw.Write(Asc28);
        //                berunfile.AppendLine("Broker Country : " + brkcntry);

        //                string brkpin = "";
        //                if (brkpin != "")
        //                {
        //                    tw.Write(brkpin);

        //                }

        //                tw.Write(Asc28);

        //                string invvalue = r.Row["InvoiceProductValues"].ToString();
        //                if (invvalue != "")
        //                {
        //                    tw.Write(invvalue);

        //                }
        //                else
        //                {
        //                    bemanfile.AppendLine("Invoice Values");
        //                    fields = "1";
        //                }
        //                tw.Write(Asc28);

        //                berunfile.AppendLine("Invoice Product Values : " + invvalue);

        //                string inco = r.Row["InvoiceTerms"].ToString();
        //                if (inco != "")
        //                {
        //                    tw.Write(inco);

        //                }
        //                else
        //                {
        //                    bemanfile.AppendLine("Invoice Terms");
        //                    fields = "1";
        //                }
        //                tw.Write(Asc28);

        //                berunfile.AppendLine("Invoice Terms : " + inco);

        //                string invcur = r.Row["InvoiceCurrency"].ToString();
        //                if (invcur != "")
        //                {
        //                    tw.Write(invcur);

        //                }
        //                else
        //                {
        //                    bemanfile.AppendLine("Invoice Currency");
        //                    fields = "1";
        //                }
        //                tw.Write(Asc28);

        //                berunfile.AppendLine("Invoice Currency : " + invcur);

        //                string NatureofDiscount = "";
        //                if (NatureofDiscount != "")
        //                {
        //                    tw.Write(NatureofDiscount);
        //                }

        //                tw.Write(Asc28);

        //                string disrate = r.Row["DiscountRate"].ToString();
        //                if (disrate != "0.00")
        //                {
        //                    tw.Write(disrate);

        //                }

        //                tw.Write(Asc28);

        //                berunfile.AppendLine("Discount Rate : " + disrate);

        //                string disamount = r.Row["DiscountAmount"].ToString();
        //                if (disamount != "0.00")
        //                {
        //                    tw.Write(disamount);

        //                }

        //                tw.Write(Asc28);

        //                berunfile.AppendLine("Discount Rate : " + disamount);

        //                string hssloadrate = "";
        //                if (hssloadrate != "")
        //                {
        //                    tw.Write(hssloadrate);

        //                }

        //                tw.Write(Asc28);

        //                string hssloadamt = "";
        //                if (hssloadamt != "")
        //                {
        //                    tw.Write(hssloadamt);

        //                }

        //                tw.Write(Asc28);

        //                string freightvalue = r.Row["FreightAmount"].ToString();
        //                if (freightvalue != "0.00")
        //                {
        //                    tw.Write(freightvalue);

        //                }

        //                tw.Write(Asc28);

        //                berunfile.AppendLine("Freight Amount : " + freightvalue);

        //                string freightrateage = "";// r.Row["FreightRate"].ToString();
        //                //if (freightrateage != "0.00")
        //                //{
        //                //    tw.Write(freightrateage);

        //                //}
        //                tw.Write(Asc28);

        //                berunfile.AppendLine("Freight Rate : " + freightrateage);
        //                string freightactual = "";
        //                //if (freightrateage == "20.00")
        //                //{
        //                if (Convert.ToDouble(freightvalue) != 0)
        //                {
        //                    freightactual = "Y";
        //                }

        //                //}

        //                //if (freightactual != "")
        //                //{
        //                tw.Write(freightactual);

        //                //}
        //                tw.Write(Asc28);

        //                string frecur = r.Row["FreightCurrency"].ToString();
        //                if (frecur != "" && frecur != "~Select~")
        //                {
        //                    tw.Write(frecur);

        //                }

        //                tw.Write(Asc28);

        //                berunfile.AppendLine("Freight Currency : " + frecur);

        //                string instvalue = "";// r.Row["InsuranceAmount"].ToString();
        //                //string instvalue = "0.00";
        //                //if (instvalue != "0.00")
        //                //{
        //                //    tw.Write(instvalue);

        //                //}

        //                tw.Write(Asc28);

        //                berunfile.AppendLine("Insurance Amount : " + instvalue);

        //                string insrate = r.Row["InsuranceRate"].ToString();
        //                berunfile.AppendLine("Insurance Rate : " + insrate);
        //                if (insrate != "0.0000")
        //                // if (insrate != "0.00")
        //                {
        //                    tw.Write(insrate);

        //                }

        //                tw.Write(Asc28);


        //                string inscur = "";// r.Row["InsuranceCurrency"].ToString();
        //                //string inscur = "";
        //                berunfile.AppendLine("Insurance Currency : " + inscur);
        //                if (inscur != "~Select~" && inscur != "")
        //                {
        //                    tw.Write(inscur);
        //                }

        //                tw.Write(Asc28);

        //                string misccharge = r.Row["MisAmount"].ToString();
        //                berunfile.AppendLine("Mis Amount : " + misccharge);
        //                if (misccharge != "0.00")
        //                {
        //                    tw.Write(misccharge);

        //                }

        //                tw.Write(Asc28);

        //                string misccurrency = r.Row["MisCurrency"].ToString();
        //                berunfile.AppendLine("Mis Currency : " + misccurrency);
        //                if (misccurrency != "~Select~" && misccurrency != "")
        //                {
        //                    tw.Write(misccurrency);

        //                }

        //                tw.Write(Asc28);


        //                string misrate = "";// r.Row["MisRate"].ToString();
        //                //berunfile.AppendLine("Mis Rate : " + misrate);
        //                //if (misrate != "0.0000")
        //                //{
        //                //    tw.Write(misrate);

        //                //}

        //                tw.Write(Asc28);
        //                string loadrate = r.Row["LandingRate"].ToString();
        //                berunfile.AppendLine("Landing Rate : " + loadrate);
        //                if (loadrate != "0.0000")
        //                {
        //                    tw.Write(loadrate);
        //                }

        //                tw.Write(Asc28);
        //                string landrate = r.Row["LoadingAmount"].ToString();
        //                if (landrate != "0.0000")
        //                {
        //                    tw.Write(landrate);

        //                }

        //                tw.Write(Asc28);




        //                string loadcurrency = r.Row["LoadingCurrency"].ToString();
        //                berunfile.AppendLine("Loading Currency : " + loadcurrency);
        //                if (loadcurrency != "~Select~" && loadcurrency != "")
        //                {
        //                    tw.Write(loadcurrency);

        //                }

        //                tw.Write(Asc28);


        //                string loadcharge = r.Row["LoadingAmount"].ToString();
        //                berunfile.AppendLine("Loading Amount : " + loadcharge);
        //                if (loadcharge != "0.0000")
        //                {
        //                    tw.Write(loadcharge);

        //                }

        //                tw.Write(Asc28);

        //                string agencycommcharge = r.Row["AgencyAmount"].ToString();
        //                berunfile.AppendLine("Agency Amount : " + agencycommcharge);
        //                if (agencycommcharge != "0.0000")
        //                {
        //                    tw.Write(agencycommcharge);

        //                }

        //                tw.Write(Asc28);

        //                string agencycommcurrency = r.Row["AgencyCurrency"].ToString();
        //                berunfile.AppendLine("Agency Currency : " + agencycommcurrency);
        //                if (agencycommcurrency != "~Select~" && agencycommcurrency != "")
        //                {
        //                    tw.Write(agencycommcurrency);

        //                }

        //                tw.Write(Asc28);

        //                string agencycommrate = r.Row["AgencyRate"].ToString();
        //                berunfile.AppendLine("Agency Rate : " + agencycommrate);
        //                if (agencycommrate != "0.0000")
        //                {
        //                    tw.Write(agencycommrate);

        //                }
        //                tw.Write(Asc28);

        //                string naturetran = r.Row["InvoiceNatureofTrans"].ToString();
        //                berunfile.AppendLine("Invoice Nature of Trans : " + naturetran);
        //                if (naturetran != "")
        //                {
        //                    tw.Write(naturetran);
        //                }
        //                else
        //                {
        //                    bemanfile.AppendLine("Nature of Transaction");
        //                    fields = "1";
        //                }

        //                tw.Write(Asc28);


        //                string paymentterm = r.Row["InvoicePaymentTerms"].ToString();

        //                tw.Write(paymentterm);

        //                tw.Write(Asc28);
        //                string condattachsale1 = "";
        //                if (condattachsale1 != "")
        //                {
        //                    tw.Write(condattachsale1);

        //                }

        //                tw.Write(Asc28);

        //                string condattachsale2 = "";
        //                if (condattachsale2 != "")
        //                {
        //                    tw.Write(condattachsale2);

        //                }
        //                tw.Write(Asc28);

        //                string condattachsale3 = "";
        //                if (condattachsale3 != "")
        //                {
        //                    tw.Write(condattachsale3);

        //                }
        //                tw.Write(Asc28);
        //                string condattachsale4 = "";
        //                if (condattachsale4 != "")
        //                {
        //                    tw.Write(condattachsale4);

        //                }
        //                tw.Write(Asc28);

        //                string condattachsale5 = "";
        //                if (condattachsale5 != "")
        //                {
        //                    tw.Write(condattachsale5);

        //                }
        //                tw.Write(Asc28);

        //                string valumethod = r.Row["ValuationMethod"].ToString();

        //                tw.Write(valumethod);


        //                tw.Write(Asc28);

        //                string actualinvno = r.Row["InvoiceNo"].ToString();
        //                if (actualinvno != "")
        //                {
        //                    tw.Write(actualinvno);

        //                }
        //                tw.Write(Asc28);

        //                txtBeFile.Text = berunfile.ToString();
        //                txtBeFile.Focus();

        //                string others = "";
        //                if (others != "")
        //                {
        //                    tw.Write(others);

        //                }

        //                i++;
        //            }
        //        }

        //        //***********************************************************************************
        //        //****************************** Table Name ITEMS *********************************
        //        //***********************************************************************************
        //        if (ds2.Tables["Jobs"].Rows.Count != 0)
        //        {
        //            tw.Write(tw.NewLine);
        //            tw.Write(items);
        //            bemanfile.AppendLine("");
        //            //bemanfile.AppendLine("Payment Table Fields");
        //            bemanfile.AppendLine("Item Table Fields");
        //            int i = 0;

        //            foreach (DataRow rs in dt2.Rows)
        //            {
        //                DataRowView r = ds2.Tables["Jobs"].DefaultView[i];
        //                string InvoiceNo = r.Row["InvoiceNo"].ToString();
        //                SqlConnection conn3 = new SqlConnection(strcon);
        //                conn3.Open();
        //                string sqlQuery3 = "Select * from T_Product  where JobNo='" + jno + "' And InvoiceNo='" + InvoiceNo + "'";

        //                SqlDataAdapter da3 = new SqlDataAdapter(sqlQuery3, conn3);
        //                DataSet ds3 = new DataSet();
        //                da3.Fill(ds3, "Jobs");
        //                DataTable dt3 = ds3.Tables["Jobs"];
        //                conn3.Close();
        //                if (ds3.Tables["Jobs"].Rows.Count != 0)
        //                {
        //                    int j = 0;
        //                    foreach (DataRow rw in dt3.Rows)
        //                    {
        //                        DataRowView row = ds3.Tables["Jobs"].DefaultView[j];
        //                        string invsrno = (i + 1).ToString();
        //                        string itemsrno = (j + 1).ToString();
        //                        string item1 = "";
        //                        string items1 = row.Row["ProductDesc"].ToString();
        //                        if (items1 == "")
        //                        {

        //                            bemanfile.AppendLine("Item Description");
        //                            fields = "1";
        //                        }
        //                        if (items1.Length > 60)
        //                        {
        //                            item1 = items1.Substring(0, 60);
        //                            items1 = items1.Remove(0, 60);
        //                        }
        //                        else
        //                        {
        //                            item1 = items1;
        //                            int n = item1.Length;
        //                            items1 = items1.Remove(0, n);
        //                        }
        //                        berunfile.AppendLine("Product Description: " + item1);

        //                        string Ritc = row.Row["RITCNo"].ToString();
        //                        if (Ritc == "")
        //                        {
        //                            bemanfile.AppendLine("Ritc Code");
        //                            fields = "1";
        //                        }
        //                        berunfile.AppendLine("RITCNo : " + Ritc);

        //                        string qty = row.Row["Qty"].ToString();
        //                        if (qty == "")
        //                        {
        //                            bemanfile.AppendLine("Qty");
        //                            fields = "1";
        //                        }
        //                        berunfile.AppendLine("Qty : " + qty);

        //                        string unitqtycode = row.Row["Unit"].ToString();
        //                        if (unitqtycode == "")
        //                        {
        //                            bemanfile.AppendLine("Qty Measurement");
        //                            fields = "1";
        //                        }
        //                        berunfile.AppendLine("Unit of Mesurement: " + unitqtycode);
        //                        txtBeFile.Text = berunfile.ToString();
        //                        txtBeFile.Focus();

        //                        string item2 = "";
        //                        if (items1.Length > 60)
        //                        {
        //                            item2 = items1.Substring(0, 60);
        //                            items1 = items1.Remove(0, 60);

        //                        }
        //                        else
        //                        {
        //                            item2 = items1;

        //                        }
        //                        string itemcat = "";
        //                        string descriptionitem1 = row.Row["GenericDesc"].ToString();
        //                        if (descriptionitem1.Length > 60)
        //                        {
        //                            descriptionitem1 = descriptionitem1.Substring(0, 59);
        //                        }

        //                        descriptionitem1 = descriptionitem1.Replace("\n", "");
        //                        berunfile.AppendLine("Generic Description: " + descriptionitem1);
        //                        //string descriptionitem2 = item2;
        //                        string accitem = row.Row["Accessories"].ToString();
        //                        string manname = row.Row["Manufacturer"].ToString();
        //                        if (manname == "")
        //                        {
        //                            manname = "N.A.";
        //                        }
        //                        string brandname = row.Row["Brand"].ToString();
        //                        if (brandname == "")
        //                        {
        //                            brandname = "N.A.";
        //                        }
        //                        string model = row.Row["Model"].ToString();
        //                        if (model == "")
        //                        {
        //                            model = "N.A.";
        //                        }
        //                        string enduseitem = "";
        //                        string cntryoriginitem = cntryorigin;
        //                        if (cntryoriginitem == "")
        //                        {
        //                            bemanfile.AppendLine("Country of Origin");
        //                            fields = "1";
        //                        }
        //                        string cth = row.Row["CTHNo"].ToString();
        //                        if (cth == "")
        //                        {
        //                            bemanfile.AppendLine("CTHNo");
        //                            fields = "1";
        //                        }
        //                        string preferntialstandard = row.Row["RateType"].ToString();
        //                        //if (preferntialstandard == "Standard" || preferntialstandard=="S")
        //                        //{
        //                        //    preferntialstandard = "S";
        //                        //}
        //                        //else if (preferntialstandard == "Preferential" || preferntialstandard=="P")
        //                        //{
        //                        //    preferntialstandard = "P";
        //                        //}
        //                        //else
        //                        //{
        //                        //    bemanfile.AppendLine("Rate Type");
        //                        //    fields = "1";
        //                        //}
        //                        string ceth = row.Row["CETNo"].ToString();
        //                        if (ceth == "")
        //                        {
        //                            bemanfile.AppendLine("CETH No");
        //                            fields = "1";
        //                        }
        //                        string bcdnotn = row.Row["BasicDutyNotn"].ToString();
        //                        string bcdnotnsrno = row.Row["BasicDutySno"].ToString();
        //                        string cvdnotn = row.Row["AddlExNotn"].ToString();
        //                        string cvdnotnsrno = row.Row["AddlExSlNo"].ToString();
        //                        string addnnotn = "";
        //                        string addnnotnsrno = "";
        //                        string addnnotn1 = "";
        //                        string addnnotnsrno1 = "";
        //                        string othnotn = "";
        //                        string othnotnsrno = "";
        //                        string sadnotmumber = "";
        //                        string sadnotnsrno = "";
        //                        string ncdnotn = "";
        //                        string ncdnotnsrno = "";
        //                        string antydumpingdutynotn = "";
        //                        string antydumpingdutysrno = "";
        //                        string cthserialno = "";
        //                        string supserialno = "";
        //                        string qtyasperantynotnno = "";
        //                        string tarrifvaluenotn = "";
        //                        string tarrifvalueitemsrno = "";
        //                        string qtyaspertarrifnontn = "";
        //                        string saptanotn = row.Row["SAPTANotn"].ToString();
        //                        string saptasrno = row.Row["SAPTASNo"].ToString();
        //                        string healthnotn = "";
        //                        string healthnotnsrno = "";
        //                        string addcvdnotn = row.Row["ExCVDNotn"].ToString();
        //                        string addcvdsrno = row.Row["ExCVDSlNo"].ToString();
        //                        string aaggrecatedutynotn = "";
        //                        string aggrecateduthnotnsrno = "";
        //                        string safeguarddutynotn = "";
        //                        string safeguarddutynotnsrno = "";
        //                        string unitpriceinvoice = row.Row["UnitPrice"].ToString();
        //                        if (unitpriceinvoice == "0.000000")
        //                        {
        //                            bemanfile.AppendLine("Invoice Value");
        //                            fields = "1";
        //                        }
        //                        string discountrate = "";
        //                        string discountamt = "";
        //                        string qtyaspercth = "";
        //                        string qtyaspercth2 = "";
        //                        string svbrefnumber = "";
        //                        string svbrefdate = "";
        //                        string svbloadasseblevalue = "";
        //                        string svbloadonduty = "";
        //                        string svbflag = "";
        //                        string whetherloadfinalprovisionanlonassessblevalue = "";
        //                        string whetherloadfinalprovisionanlondutyvalue = "";
        //                        string customehousecode = "";
        //                        string policyparano = "";
        //                        string policyyear = "";
        //                        string rspdeclared = row.Row["MRPDuty"].ToString();
        //                        if (rspdeclared == "True")
        //                        {
        //                            rspdeclared = "Y";
        //                        }
        //                        else
        //                        {
        //                            rspdeclared = "N";
        //                        }
        //                        string reimportitem = row.Row["MRPDuty"].ToString();
        //                        if (reimportitem == "True")
        //                        {
        //                            reimportitem = "Y";
        //                        }
        //                        else
        //                        {
        //                            reimportitem = "N";
        //                        }
        //                        string previousbeo = "";
        //                        string previousbedate = "";
        //                        string previousunitprice = "";
        //                        string previouscurrencycode = "";
        //                        string previouscustomesite = "";

        //                        tw.Write(tw.NewLine);

        //                        tw.Write(Messagetype); tw.Write(Asc28); tw.Write(ReceiverID); tw.Write(Asc28); tw.Write(SequenceId); tw.Write(Asc28); tw.Write(jobdate); tw.Write(Asc28); tw.Write(BeNo); tw.Write(Asc28);
        //                        tw.Write(BeDate); tw.Write(Asc28); tw.Write(invsrno); tw.Write(Asc28); tw.Write(itemsrno); tw.Write(Asc28); tw.Write(qty); tw.Write(Asc28); tw.Write(unitqtycode); tw.Write(Asc28);
        //                        tw.Write(Ritc); tw.Write(Asc28); tw.Write(item1); tw.Write(Asc28); tw.Write(item2); tw.Write(Asc28); tw.Write(itemcat); tw.Write(Asc28); tw.Write(descriptionitem1); tw.Write(Asc28);

        //                        tw.Write(accitem); tw.Write(Asc28); tw.Write(manname); tw.Write(Asc28); tw.Write(brandname); tw.Write(Asc28); tw.Write(model); tw.Write(Asc28); tw.Write(enduseitem); tw.Write(Asc28);
        //                        tw.Write(cntryorigin); tw.Write(Asc28); tw.Write(cth); tw.Write(Asc28); tw.Write(preferntialstandard); tw.Write(Asc28); tw.Write(ceth); tw.Write(Asc28); tw.Write(bcdnotn); tw.Write(Asc28);

        //                        tw.Write(bcdnotnsrno); tw.Write(Asc28); tw.Write(cvdnotn); tw.Write(Asc28); tw.Write(cvdnotnsrno); tw.Write(Asc28); tw.Write(addnnotn); tw.Write(Asc28); tw.Write(addnnotnsrno); tw.Write(Asc28);

        //                        tw.Write(addnnotn1); tw.Write(Asc28); tw.Write(addnnotnsrno1); tw.Write(Asc28); tw.Write(othnotn); tw.Write(Asc28); tw.Write(othnotnsrno); tw.Write(Asc28); tw.Write(sadnotmumber); tw.Write(Asc28);

        //                        tw.Write(sadnotnsrno); tw.Write(Asc28); tw.Write(ncdnotn); tw.Write(Asc28); tw.Write(ncdnotnsrno); tw.Write(Asc28); tw.Write(antydumpingdutynotn); tw.Write(Asc28); tw.Write(antydumpingdutysrno); tw.Write(Asc28);

        //                        tw.Write(cthserialno); tw.Write(Asc28); tw.Write(supserialno); tw.Write(Asc28); tw.Write(qtyasperantynotnno); tw.Write(Asc28); tw.Write(tarrifvaluenotn); tw.Write(Asc28); tw.Write(tarrifvalueitemsrno); tw.Write(Asc28);

        //                        tw.Write(qtyaspertarrifnontn); tw.Write(Asc28); tw.Write(saptanotn); tw.Write(Asc28); tw.Write(saptasrno); tw.Write(Asc28); tw.Write(healthnotn); tw.Write(Asc28); tw.Write(healthnotnsrno); tw.Write(Asc28);

        //                        tw.Write(addcvdnotn); tw.Write(Asc28); tw.Write(addcvdsrno); tw.Write(Asc28); tw.Write(aaggrecatedutynotn); tw.Write(Asc28); tw.Write(aggrecateduthnotnsrno); tw.Write(Asc28); tw.Write(safeguarddutynotn); tw.Write(Asc28);

        //                        tw.Write(safeguarddutynotnsrno); tw.Write(Asc28); tw.Write(unitpriceinvoice); tw.Write(Asc28); tw.Write(discountrate); tw.Write(Asc28); tw.Write(discountamt); tw.Write(Asc28); tw.Write(qtyaspercth); tw.Write(Asc28);

        //                        tw.Write(qtyaspercth2); tw.Write(Asc28); tw.Write(svbrefnumber); tw.Write(Asc28); tw.Write(svbrefdate); tw.Write(Asc28); tw.Write(svbloadasseblevalue); tw.Write(Asc28); tw.Write(svbloadonduty); tw.Write(Asc28);

        //                        tw.Write(svbflag); tw.Write(Asc28); tw.Write(whetherloadfinalprovisionanlonassessblevalue); tw.Write(Asc28); tw.Write(whetherloadfinalprovisionanlondutyvalue); tw.Write(Asc28); tw.Write(customehousecode); tw.Write(Asc28); tw.Write(policyparano); tw.Write(Asc28);

        //                        tw.Write(policyyear); tw.Write(Asc28); tw.Write(rspdeclared); tw.Write(Asc28); tw.Write(reimportitem); tw.Write(Asc28); tw.Write(previousbeo); tw.Write(Asc28); tw.Write(previousbedate); tw.Write(Asc28);

        //                        tw.Write(previousunitprice); tw.Write(Asc28); tw.Write(previouscurrencycode); tw.Write(Asc28); tw.Write(previouscustomesite);

        //                        j++;
        //                    }
        //                }


        //                i++;
        //            }
        //        }



        //        SqlConnection connlic = new SqlConnection(strcon);
        //        conn2.Open();
        //        string sqlQuerylic = "select * from T_Schemes  where jobNo='" + jno + "'";

        //        SqlDataAdapter dalic = new SqlDataAdapter(sqlQuerylic, connlic);
        //        DataSet dslic = new DataSet();
        //        dalic.Fill(dslic, "Jobs");
        //        DataTable dtlic = dslic.Tables["Jobs"];
        //        connlic.Close();
        //        if (dslic.Tables["Jobs"].Rows.Count != 0)
        //        {
        //            tw.Write(tw.NewLine);
        //            tw.Write(licence);
        //            bemanfile.AppendLine("");
        //            bemanfile.AppendLine("Licence Table Fields");
        //            int j = 0;
        //            foreach (DataRow ra in dtlic.Rows)
        //            {
        //                DataRowView r = dslic.Tables["Jobs"].DefaultView[j];
        //                string invsrno = "";
        //                string itemsrnoininv = "";
        //                string itemsrnoinlicence = "";
        //                string debitvalue = r.Row["DebitValue"].ToString(); ;
        //                string debitqty = r.Row["Quantity"].ToString(); ;
        //                string debitunitmeasure = "";
        //                string licregno = r.Row["SchemeLicNo"].ToString();
        //                if (licregno == "")
        //                {
        //                    bemanfile.AppendLine("Licence Registered Number");
        //                }
        //                string licregdate = r.Row["SchemeLicDate"].ToString();
        //                if (licregdate == "")
        //                {
        //                    bemanfile.AppendLine("Licence Registered Date");
        //                }
        //                licregdate = convertdate(licregdate);
        //                string liccode = r.Row["SchemeType"].ToString();
        //                if (liccode == "")
        //                {
        //                    bemanfile.AppendLine("Licence Code");
        //                }
        //                string ralicregport = r.Row["RegPort"].ToString();
        //                if (ralicregport == "")
        //                {
        //                    bemanfile.AppendLine("Registered Port");
        //                }
        //                tw.Write(tw.NewLine);
        //                tw.Write(Messagetype); tw.Write(Asc28); tw.Write(ReceiverID); tw.Write(Asc28); tw.Write(SequenceId); tw.Write(Asc28); tw.Write(jobdate); tw.Write(Asc28); tw.Write(BeNo); tw.Write(Asc28);
        //                tw.Write(BeDate); tw.Write(Asc28); tw.Write(invsrno); tw.Write(Asc28); tw.Write(itemsrnoininv); tw.Write(Asc28); tw.Write(itemsrnoinlicence); tw.Write(Asc28); tw.Write(debitvalue); tw.Write(Asc28);
        //                tw.Write(debitqty); tw.Write(Asc28); tw.Write(debitunitmeasure); tw.Write(Asc28); tw.Write(licregno); tw.Write(Asc28); tw.Write(licregdate); tw.Write(Asc28); tw.Write(liccode); tw.Write(Asc28); tw.Write(ralicregport);
        //                j++;
        //            }
        //        }

        //        //retailsalew


        //        //SqlConnection connrsp = new SqlConnection(strcon);
        //        //connrsp.Open();
        //        //string sqlQueryrsp = "select * from T_Product  where jobNo='" + jno + "'";

        //        //SqlDataAdapter darsp = new SqlDataAdapter(sqlQueryrsp, connrsp);
        //        //DataSet dsrsp = new DataSet();
        //        //darsp.Fill(dsrsp, "Jobs");
        //        //DataTable dtrsp = dsrsp.Tables["Jobs"];
        //        //connrsp.Close();
        //        //if (dsrsp.Tables["Jobs"].Rows.Count != 0)
        //        //{
        //        //    tw.Write(tw.NewLine);
        //        //    tw.Write(rsp);
        //        //    bemanfile.AppendLine();
        //        //    bemanfile.AppendLine("RSP table Fields");
        //        //    int j = 0;
        //        //    foreach (DataRow rs in dtrsp.Rows)
        //        //    {
        //        //        DataRowView r = dsrsp.Tables["Jobs"].DefaultView[j];
        //        //        string invsrnorsp = "";
        //        //        string itemsrnoininvrsp = "";
        //        //        string itemsrnoinrsp = r.Row["MRPSNo"].ToString(); 
        //        //        string rsprs = r.Row["MRP"].ToString();
        //        //        if (rsprs == "")
        //        //        {
        //        //            bemanfile.AppendLine("RSP");
        //        //        }
        //        //        string qtyrsp = r.Row["Qty"].ToString();
        //        //        if (qtyrsp == "")
        //        //        {
        //        //            bemanfile.AppendLine("Quantity");
        //        //        }
        //        //        string desitemrsp = r.Row["ProductDesc"].ToString();
        //        //        if (desitemrsp == "")
        //        //        {
        //        //            bemanfile.AppendLine("Description of Item");
        //        //        }
        //        //        string rspnotn = "";
        //        //        string rspnotnsrno = "";


        //        //          tw.Write(tw.NewLine);
        //        //        tw.Write(Messagetype); tw.Write(Asc28); tw.Write(ReceiverID); tw.Write(Asc28); tw.Write(SequenceId); tw.Write(Asc28); tw.Write(jobdate); tw.Write(Asc28); tw.Write(BeNo); tw.Write(Asc28);
        //        //        tw.Write(BeDate); tw.Write(Asc28); tw.Write(invsrnorsp); tw.Write(Asc28); tw.Write(itemsrnoininvrsp); tw.Write(Asc28); tw.Write(itemsrnoinrsp); tw.Write(Asc28); tw.Write(rsprs); tw.Write(Asc28);
        //        //        tw.Write(qtyrsp); tw.Write(Asc28); tw.Write(desitemrsp); tw.Write(Asc28); tw.Write(rspnotn); tw.Write(Asc28); tw.Write(rspnotnsrno); 
        //        //        j++;
        //        //    }
        //        //}


        //        ////DEPB


        //        //SqlConnection conndep = new SqlConnection(strcon);
        //        //conndep.Open();
        //        //string sqlQuerydep = "select * from T_Product  where jobNo='" + jno + "' and BCD Notification is not null and ";

        //        //SqlDataAdapter dadep = new SqlDataAdapter(sqlQuerydep, conndep);
        //        //DataSet dsdep = new DataSet();
        //        //dadep.Fill(dsdep, "Jobs");
        //        //DataTable dtdep = dsdep.Tables["Jobs"];
        //        //conndep.Close();
        //        //if (dsdep.Tables["Jobs"].Rows.Count != 0)
        //        //{
        //        //    tw.Write(tw.NewLine);
        //        //    tw.Write(debp);
        //        //    bemanfile.AppendLine();
        //        //    bemanfile.AppendLine("DEPB table Fields");
        //        //    int j = 0;
        //        //    foreach (DataRow r1 in dtdep.Rows)
        //        //    {
        //        //        DataRowView r = dsdep.Tables["Jobs"].DefaultView[j];
        //        //        string invsrnodep = "";
        //        //        string itemsrnoininvdep = "";
        //        //        string exemptionreq = "";

        //        //        string bcdnotn = "";
        //        //        if (bcdnotn == "")
        //        //        {
        //        //            bemanfile.AppendLine("BCD Notification");
        //        //        }
        //        //        string bcdnotnsrno = "";
        //        //        if(bcdnotnsrno=="")
        //        //        {
        //        //            bemanfile.AppendLine("BCD Notification Srno");
        //        //        }


        //        //        tw.Write(tw.NewLine);
        //        //        tw.Write(Messagetype); tw.Write(Asc28); tw.Write(ReceiverID); tw.Write(Asc28); tw.Write(SequenceId); tw.Write(Asc28); tw.Write(jobdate); tw.Write(Asc28); tw.Write(BeNo); tw.Write(Asc28);
        //        //        tw.Write(BeDate); tw.Write(Asc28); tw.Write(invsrnodep); tw.Write(Asc28); tw.Write(itemsrnoininvdep); tw.Write(Asc28); tw.Write(exemptionreq); tw.Write(Asc28); tw.Write(bcdnotn); tw.Write(Asc28);
        //        //        tw.Write(bcdnotnsrno);
        //        //        j++;
        //        //    }
        //        //}

        //        //bond


        //        SqlConnection connbond = new SqlConnection(strcon);
        //        connbond.Open();
        //        string sqlQuerybond = "select * from T_ImportBondReg  where jobNo='" + jno + "'";

        //        SqlDataAdapter dabond = new SqlDataAdapter(sqlQuerybond, connbond);
        //        DataSet dsbond = new DataSet();
        //        dabond.Fill(dsbond, "Jobs");
        //        DataTable dtbond = dsbond.Tables["Jobs"];
        //        connbond.Close();
        //        if (dsbond.Tables["Jobs"].Rows.Count != 0)
        //        {
        //            tw.Write(tw.NewLine);
        //            tw.Write(bond);
        //            bemanfile.AppendLine();
        //            bemanfile.AppendLine("Bond table fields");
        //            int j = 0;
        //            foreach (DataRow r1 in dtbond.Rows)
        //            {
        //                DataRowView r = dsbond.Tables["Jobs"].DefaultView[j];
        //                string bondnumber = r.Row["BondNumber"].ToString();
        //                if (bondnumber == "")
        //                {
        //                    bemanfile.AppendLine("Bondnumber");
        //                }
        //                string bondcode = r.Row["BondType"].ToString();
        //                if (bondcode == "")
        //                {
        //                    bemanfile.AppendLine("BondCode");
        //                }
        //                string bondport = "";
        //                if (bondport == "")
        //                {
        //                    bemanfile.AppendLine("BondPort");
        //                }


        //                tw.Write(tw.NewLine);
        //                tw.Write(Messagetype); tw.Write(Asc28); tw.Write(ReceiverID); tw.Write(Asc28); tw.Write(SequenceId); tw.Write(Asc28); tw.Write(jobdate); tw.Write(Asc28); tw.Write(BeNo); tw.Write(Asc28);
        //                tw.Write(BeDate); tw.Write(Asc28); tw.Write(bondnumber); tw.Write(Asc28); tw.Write(bondcode); tw.Write(Asc28); tw.Write(bondport);
        //                j++;
        //            }
        //        }

        //        //cerificate


        //        SqlConnection conncert = new SqlConnection(strcon);
        //        conncert.Open();
        //        string sqlQuerycert = "select * from T_ImportBondCertificate  where jobNo='" + jno + "'";

        //        SqlDataAdapter dacert = new SqlDataAdapter(sqlQuerycert, conncert);
        //        DataSet dscert = new DataSet();
        //        dacert.Fill(dscert, "Jobs");
        //        DataTable dtcert = dscert.Tables["Jobs"];
        //        conncert.Close();
        //        if (dscert.Tables["Jobs"].Rows.Count != 0)
        //        {
        //            tw.Write(tw.NewLine);
        //            tw.Write(cert);
        //            bemanfile.AppendLine();
        //            bemanfile.AppendLine("Certificate");

        //            int j = 0;
        //            foreach (DataRow r1 in dtcert.Rows)
        //            {
        //                DataRowView r = dscert.Tables["Jobs"].DefaultView[j];
        //                string certnumber = r.Row["CertificateNo"].ToString();
        //                if (certnumber == "")
        //                {
        //                    bemanfile.AppendLine("Certificate Number");
        //                }
        //                string certdate = r.Row["CertificateDate"].ToString();
        //                if (certdate == "")
        //                {
        //                    bemanfile.AppendLine("Certificate Date");
        //                }
        //                else
        //                {
        //                    certdate = convertdate(certdate);
        //                }
        //                string certtype = r.Row["CertificateType"].ToString();

        //                if (certtype == "")
        //                {
        //                    bemanfile.AppendLine("Certificate Type");
        //                }
        //                tw.Write(tw.NewLine);
        //                tw.Write(Messagetype); tw.Write(Asc28); tw.Write(ReceiverID); tw.Write(Asc28); tw.Write(SequenceId); tw.Write(Asc28); tw.Write(jobdate); tw.Write(Asc28); tw.Write(BeNo); tw.Write(Asc28);
        //                tw.Write(BeDate); tw.Write(Asc28); tw.Write(certnumber); tw.Write(Asc28); tw.Write(certdate); tw.Write(Asc28); tw.Write(certtype);
        //                j++;
        //            }
        //        }

        //        //customehousecodeimporter


        //        SqlConnection connhss = new SqlConnection(strcon);
        //        connhss.Open();
        //        string sqlQueryhss = "select * from T_Importer  where jobNo='" + jno + "'";

        //        SqlDataAdapter dahss = new SqlDataAdapter(sqlQueryhss, connhss);
        //        DataSet dshss = new DataSet();
        //        dahss.Fill(dshss, "Jobs");
        //        DataTable dthss = dshss.Tables["Jobs"];
        //        connhss.Close();
        //        if (saleflag != "N")
        //        {
        //            if (dshss.Tables["Jobs"].Rows.Count != 0)
        //            {
        //                tw.Write(tw.NewLine);
        //                tw.Write(hss);
        //                bemanfile.AppendLine();
        //                bemanfile.AppendLine("HSS table fields");

        //                int j = 0;
        //                foreach (DataRow r1 in dthss.Rows)
        //                {
        //                    DataRowView r = dshss.Tables["Jobs"].DefaultView[j];
        //                    string iec = r.Row["IECodeNo"].ToString();
        //                    if (iec == "")
        //                    {
        //                        bemanfile.AppendLine("IEC");
        //                    }
        //                    string branchserialnumber = r.Row["BranchSno"].ToString();
        //                    if (branchserialnumber == "")
        //                    {
        //                        bemanfile.AppendLine("Branch Serial Number");
        //                    }
        //                    string importername = r.Row["Importer"].ToString();
        //                    string importeraddress1 = "";
        //                    string importeraddress2 = "";
        //                    string importercity = "";
        //                    string importerpin = "";
        //                    tw.Write(tw.NewLine);
        //                    tw.Write(Messagetype); tw.Write(Asc28); tw.Write(ReceiverID); tw.Write(Asc28); tw.Write(SequenceId); tw.Write(Asc28); tw.Write(jobdate); tw.Write(Asc28); tw.Write(BeNo); tw.Write(Asc28);
        //                    tw.Write(BeDate); tw.Write(Asc28); tw.Write(iec); tw.Write(Asc28); tw.Write(branchserialnumber); tw.Write(Asc28); tw.Write(importername); tw.Write(Asc28); tw.Write(importeraddress1); tw.Write(Asc28);
        //                    tw.Write(importeraddress2); tw.Write(Asc28); tw.Write(importercity); tw.Write(Asc28); tw.Write(importerpin);
        //                    j++;
        //                }
        //            }
        //        }

        //        //reimport
        //        SqlConnection connrimp = new SqlConnection(strcon);
        //        connrimp.Open();
        //        string sqlQueryrimp = "select * from T_ProductITCLicence  where jobNo='" + jno + "'";

        //        SqlDataAdapter darimp = new SqlDataAdapter(sqlQueryrimp, connrimp);
        //        DataSet dsrimp = new DataSet();
        //        darimp.Fill(dsrimp, "Jobs");
        //        DataTable dtrimp = dsrimp.Tables["Jobs"];
        //        connrimp.Close();
        //        if (dsrimp.Tables["Jobs"].Rows.Count != 0)
        //        {
        //            tw.Write(tw.NewLine);
        //            tw.Write(reimport);
        //            bemanfile.AppendLine();
        //            bemanfile.AppendLine("REIMPORT");
        //            int j = 0;
        //            foreach (DataRow ra in dtrimp.Rows)
        //            {
        //                DataRowView r = dsrimp.Tables["Jobs"].DefaultView[j];
        //                string invsrnorimp = "";
        //                string itemsrnoininvimp = "";
        //                string shipbillnoimp = "";
        //                if (shipbillnoimp == "")
        //                {
        //                    bemanfile.AppendLine("Shipping Bill No.");
        //                }
        //                string shipbilldatteimp = "";
        //                if (shipbilldatteimp == "")
        //                {
        //                    bemanfile.AppendLine("Shipping Bill Date.");
        //                }
        //                string portexportimp = "";
        //                if (portexportimp == "")
        //                {
        //                    bemanfile.AppendLine("Port of Export");
        //                }
        //                string invnosb = "";
        //                string itemnosb = "";
        //                string notnoimp = "";
        //                if (notnoimp == "")
        //                {
        //                    bemanfile.AppendLine("Notification No");
        //                }
        //                string notsrnoimp = "";
        //                if (notsrnoimp == "")
        //                {
        //                    bemanfile.AppendLine("Notification Sr No");
        //                }
        //                string exportfreight = "";
        //                string exportinsurance = "";
        //                string customsduty = "";
        //                string exciseduty = "";

        //                tw.Write(tw.NewLine);
        //                tw.Write(Messagetype); tw.Write(Asc28); tw.Write(ReceiverID); tw.Write(Asc28); tw.Write(SequenceId); tw.Write(Asc28); tw.Write(jobdate); tw.Write(Asc28); tw.Write(BeNo); tw.Write(Asc28);
        //                tw.Write(BeDate); tw.Write(Asc28); tw.Write(invsrnorimp); tw.Write(Asc28); tw.Write(itemsrnoininvimp); tw.Write(Asc28); tw.Write(shipbillnoimp); tw.Write(Asc28); tw.Write(shipbilldatteimp); tw.Write(Asc28);
        //                tw.Write(portexportimp); tw.Write(Asc28); tw.Write(invnosb); tw.Write(Asc28); tw.Write(itemnosb); tw.Write(Asc28); tw.Write(notnoimp); tw.Write(Asc28); tw.Write(notsrnoimp); tw.Write(Asc28);
        //                tw.Write(exportfreight); tw.Write(Asc28); tw.Write(exportinsurance); tw.Write(Asc28); tw.Write(customsduty); tw.Write(Asc28); tw.Write(exciseduty);
        //                j++;
        //            }
        //        }

        //        //sbeduty
        //        SqlConnection connsbe = new SqlConnection(strcon);
        //        connsbe.Open();
        //        string sqlQuerysbe = "select * from T_ProductITCLicence  where jobNo='" + jno + "'";

        //        SqlDataAdapter dasbe = new SqlDataAdapter(sqlQuerysbe, connsbe);
        //        DataSet dssbe = new DataSet();
        //        dasbe.Fill(dssbe, "Jobs");
        //        DataTable dtsbe = dssbe.Tables["Jobs"];
        //        connsbe.Close();
        //        if (dssbe.Tables["Jobs"].Rows.Count != 0)
        //        {
        //            tw.Write(tw.NewLine);
        //            tw.Write(sbeduty);
        //            int j = 0;
        //            foreach (DataRow ra in dtsbe.Rows)
        //            {
        //                DataRowView r = dssbe.Tables["Jobs"].DefaultView[j];
        //                string invsrnosbe = "";
        //                string itemsrnoininvsbe = "";

        //                string notnosbe = "";
        //                string notsrnosbe = "";
        //                string dutytypesbe = "";
        //                string aadldutyflagsbe = "";

        //                tw.Write(tw.NewLine);
        //                tw.Write(Messagetype); tw.Write(Asc28); tw.Write(ReceiverID); tw.Write(Asc28); tw.Write(SequenceId); tw.Write(Asc28); tw.Write(jobdate); tw.Write(Asc28); tw.Write(BeNo); tw.Write(Asc28);
        //                tw.Write(BeDate); tw.Write(Asc28); tw.Write(invsrnosbe); tw.Write(Asc28); tw.Write(itemsrnoininvsbe); tw.Write(Asc28); tw.Write(notnosbe); tw.Write(Asc28); tw.Write(notsrnosbe); tw.Write(Asc28);
        //                tw.Write(dutytypesbe); tw.Write(Asc28); tw.Write(aadldutyflagsbe);
        //                j++;
        //            }
        //        }

        //        //***********************************************************************************
        //        //****************************** Table Name IGM *********************************
        //        //***********************************************************************************

        //        string igmno = "";
        //        string igmdate = "";
        //        SqlConnection connigm = new SqlConnection(strcon);
        //        connigm.Open();
        //        string sqlQueryigm = "select LocalIGMNo,LocalIGMDate,GLDInwardDate,GatewayIGMNo, GatewayIGMDate,ShipmentPortCode, MasterBLNo, MasterBLDate, HouseBLNo, HouseBLDate,NoOfPackages,GrossWeight,GrossWeightUnit,PackagesUnit,MarksNos from  T_ShipmentDetails  where JobNo='" + jno + "'";
        //        SqlDataAdapter daigm = new SqlDataAdapter(sqlQueryigm, connigm);
        //        DataSet dsigm = new DataSet();
        //        daigm.Fill(dsigm, "Jobs");
        //        DataTable dtigm = dsigm.Tables["Jobs"];
        //        connigm.Close();
        //        if (dsigm.Tables["Jobs"].Rows.Count != 0)
        //        {
        //            tw.Write(tw.NewLine);
        //            tw.Write(igms);
        //            bemanfile.AppendLine("");
        //            bemanfile.AppendLine("IGM Table Fields");
        //            int j = 0;
        //            foreach (DataRow ra in dtigm.Rows)
        //            {
        //                DataRowView r = dsigm.Tables["Jobs"].DefaultView[j];
        //                igmno = r.Row["LocalIGMNo"].ToString();
        //                //if (igmno == "")
        //                //{
        //                //    if (DOCTYPE != "A")
        //                //    {
        //                //bemanfile.AppendLine("IGM No");
        //                //fields = "1";
        //                // }
        //                //}
        //                igmdate = r.Row["LocalIGMDate"].ToString();
        //                //if (igmdate == "")
        //                //{
        //                //    if (DOCTYPE != "A")
        //                //    {
        //                //        bemanfile.AppendLine("IGM Date");
        //                //        fields = "1";
        //                //    }
        //                //}
        //                igmdate = convertdate(igmdate);
        //                string inwarddate = r.Row["GLDInwardDate"].ToString();
        //                inwarddate = convertdate(inwarddate);
        //                //if (inwarddate == "")
        //                //{
        //                //    if (DOCTYPE != "A" && SVBflag != "Y")
        //                //    {
        //                //        bemanfile.AppendLine("Inwarddate Date");
        //                //        fields = "1";
        //                //    }
        //                //}
        //                string gatewayigmno = r.Row["GatewayIGMNo"].ToString();
        //                //if (gatewayigmno == "")
        //                //{
        //                //    bemanfile.AppendLine("GatewayIGM No");
        //                //    fields="1";
        //                //}
        //                string gatewayigmdate = r.Row["GatewayIGMDate"].ToString();
        //                gatewayigmdate = convertdate(gatewayigmdate);
        //                //if (gatewayigmdate == "")
        //                //{
        //                //    bemanfile.AppendLine("GatewayIGM Date");
        //                //    fields = "1";
        //                //}
        //                string portofreporting = "";//r.Row["ShipmentPortCode"].ToString();
        //                //if (portofreporting == "")
        //                //{
        //                //    if (DOCTYPE != "A")
        //                //    {
        //                //        bemanfile.AppendLine("Port of Reporting");
        //                //        fields = "1";
        //                //    }
        //                //}
        //                string mawblno = r.Row["MasterBLNo"].ToString();
        //                string mawbldate = r.Row["MasterBLDate"].ToString();
        //                mawbldate = convertdate(mawbldate);
        //                // mawbldate = "";
        //                string hawblno = r.Row["HouseBLNo"].ToString();
        //                string hawbdate = r.Row["HouseBLDate"].ToString();
        //                hawbdate = convertdate(hawbdate);
        //                string totalpkgs = r.Row["NoOfPackages"].ToString();

        //                if (totalpkgs == "")
        //                {
        //                    bemanfile.AppendLine("Total No of Packages");
        //                    fields = "1";
        //                }
        //                string grosswt = r.Row["GrossWeight"].ToString();
        //                //if (grosswt == "")
        //                //{
        //                //    bemanfile.AppendLine("Gross Weight");
        //                //    fields = "1";
        //                //}
        //                //else
        //                //{
        //                //    string[] gross = grosswt.Split('.');
        //                //    int n = gross[1].Length;
        //                //    if (n != 3)
        //                //    {
        //                //        bemanfile.AppendLine("Gross Weight must be 3 digit decimal");
        //                //        fields = "1";
        //                //    }
        //                //}
        //                string unitqtycode = r.Row["GrossWeightUnit"].ToString();
        //                if (unitqtycode == "~Select~")
        //                {
        //                    unitqtycode = "";
        //                }
        //                string packcode = r.Row["PackagesUnit"].ToString();
        //                if (packcode == "~Select~")
        //                {
        //                    packcode = "";
        //                }
        //                string marksnmumber1 = r.Row["MarksNos"].ToString();
        //                string marksnmumber2 = "";
        //                string marksnmumber3 = "";
        //                tw.Write(tw.NewLine);
        //                tw.Write(Messagetype); tw.Write(Asc28); tw.Write(ReceiverID); tw.Write(Asc28); tw.Write(SequenceId); tw.Write(Asc28); tw.Write(jobdate); tw.Write(Asc28); tw.Write(BeNo); tw.Write(Asc28);
        //                tw.Write(BeDate); tw.Write(Asc28); tw.Write(igmno); tw.Write(Asc28); tw.Write(igmdate); tw.Write(Asc28); tw.Write(inwarddate); tw.Write(Asc28); tw.Write(gatewayigmno); tw.Write(Asc28);
        //                tw.Write(gatewayigmdate); tw.Write(Asc28); tw.Write(portofreporting); tw.Write(Asc28); tw.Write(mawblno); tw.Write(Asc28); tw.Write(mawbldate); tw.Write(Asc28); tw.Write(hawblno); tw.Write(Asc28);
        //                tw.Write(hawbdate); tw.Write(Asc28); tw.Write(totalpkgs); tw.Write(Asc28); tw.Write(grosswt); tw.Write(Asc28); tw.Write(unitqtycode); tw.Write(Asc28); tw.Write(packcode); tw.Write(Asc28);
        //                tw.Write(marksnmumber1); tw.Write(Asc28); tw.Write(marksnmumber2); tw.Write(Asc28); tw.Write(marksnmumber3);
        //                j++;
        //            }
        //        }

        //        //container


        //        SqlConnection conncont = new SqlConnection(strcon);
        //        conncont.Open();
        //        string sqlQuerycont = "select ContainerType, ContainerNo, SealNo,LoadType from T_ShipmentContainerInfo  where JobNo='" + jno + "'";

        //        SqlDataAdapter dacont = new SqlDataAdapter(sqlQuerycont, conncont);
        //        DataSet dscont = new DataSet();
        //        dacont.Fill(dscont, "Jobs");
        //        DataTable dtcont = dscont.Tables["Jobs"];
        //        conncont.Close();
        //        if (dscont.Tables["Jobs"].Rows.Count != 0)
        //        {
        //            tw.Write(tw.NewLine);
        //            bemanfile.AppendLine();
        //            bemanfile.AppendLine("Container table fields");
        //            tw.Write(container);
        //            int j = 0;
        //            foreach (DataRow ra in dtcont.Rows)
        //            {
        //                DataRowView r = dscont.Tables["Jobs"].DefaultView[j];
        //                string igmnocont = igmno;
        //                string igmdatecont = igmdate;

        //                string conttyyy = r.Row["LoadType"].ToString();
        //                conttyyy = conttyyy.Substring(0, 1);
        //                string containerno = r.Row["ContainerNo"].ToString();
        //                string sealno = r.Row["SealNo"].ToString();
        //                if (sealno == "&nbsp;" || sealno == "")
        //                {
        //                    sealno = "N.A.";
        //                }

        //                tw.Write(tw.NewLine);
        //                tw.Write(Messagetype); tw.Write(Asc28); tw.Write(ReceiverID); tw.Write(Asc28); tw.Write(SequenceId); tw.Write(Asc28); tw.Write(jobdate); tw.Write(Asc28); tw.Write(BeNo); tw.Write(Asc28);
        //                tw.Write(BeDate); tw.Write(Asc28); tw.Write(igmnocont); tw.Write(Asc28); tw.Write(igmdatecont); tw.Write(Asc28); tw.Write(conttyyy); tw.Write(Asc28); tw.Write(containerno); tw.Write(Asc28);
        //                tw.Write(sealno);
        //                j++;
        //            }
        //        }

        //        //ctx


        //        SqlConnection connctx = new SqlConnection(strcon);
        //        connctx.Open();
        //        string sqlQueryctx = "select * from T_ProductITCLicence  where jobNo='" + jno + "'";

        //        SqlDataAdapter dactx = new SqlDataAdapter(sqlQueryctx, connctx);
        //        DataSet dsctx = new DataSet();
        //        dactx.Fill(dsctx, "Jobs");
        //        DataTable dtctx = dsctx.Tables["Jobs"];
        //        connctx.Close();
        //        if (dsctx.Tables["Jobs"].Rows.Count != 0)
        //        {
        //            tw.Write(tw.NewLine);
        //            tw.Write(ctx);
        //            int j = 0;
        //            foreach (DataRow ra in dtctx.Rows)
        //            {
        //                DataRowView r = dsctx.Tables["Jobs"].DefaultView[j];
        //                string satecode = "";
        //                string commercialtaxtype = "";

        //                string commercialtaxregno = "";

        //                tw.Write(tw.NewLine);
        //                tw.Write(Messagetype); tw.Write(Asc28); tw.Write(ReceiverID); tw.Write(Asc28); tw.Write(SequenceId); tw.Write(Asc28); tw.Write(jobdate); tw.Write(Asc28); tw.Write(BeNo); tw.Write(Asc28);
        //                tw.Write(BeDate); tw.Write(Asc28); tw.Write(satecode); tw.Write(Asc28); tw.Write(commercialtaxtype); tw.Write(Asc28); tw.Write(commercialtaxregno); tw.Write(Asc28);
        //                j++;
        //            }
        //        }

        //        //amend


        //        SqlConnection connamd = new SqlConnection(strcon);
        //        connamd.Open();
        //        string sqlQueryamd = "select * from T_ProductITCLicence  where jobNo='" + jno + "'";

        //        SqlDataAdapter daamd = new SqlDataAdapter(sqlQueryamd, connamd);
        //        DataSet dsamd = new DataSet();
        //        daamd.Fill(dsamd, "Jobs");
        //        DataTable dtamd = dsamd.Tables["Jobs"];
        //        connamd.Close();
        //        if (dsamd.Tables["Jobs"].Rows.Count != 0)
        //        {
        //            tw.Write(tw.NewLine);
        //            tw.Write(amend);
        //            int j = 0;
        //            foreach (DataRow ra in dtamd.Rows)
        //            {
        //                DataRowView r = dsamd.Tables["Jobs"].DefaultView[j];
        //                string ammendcode = "";
        //                string reasonforamend = "";

        //                string requestletternumber = "";
        //                string requestdate = "";

        //                tw.Write(tw.NewLine);
        //                tw.Write(Messagetype); tw.Write(Asc28); tw.Write(ReceiverID); tw.Write(Asc28); tw.Write(SequenceId); tw.Write(Asc28); tw.Write(jobdate); tw.Write(Asc28); tw.Write(BeNo); tw.Write(Asc28);
        //                tw.Write(BeDate); tw.Write(Asc28); tw.Write(ammendcode); tw.Write(Asc28); tw.Write(reasonforamend); tw.Write(Asc28); tw.Write(requestletternumber); tw.Write(Asc28); tw.Write(requestdate); tw.Write(Asc28);
        //                tw.Write(ammendcode); tw.Write(Asc28);
        //                j++;
        //            }
        //        }
        //        tw.Write(tw.NewLine);
        //        tw.Write(endbe);
        //        tw.Write(tw.NewLine);
        //        tw.Write(TText);
        //        tw.Write(Asc28);
        //        tw.Write(SequenceId);
        //        tw.Write(tw.NewLine);
        //        if (fields != "1")
        //        {
        //            tw.Flush();
        //            tw.Close();
        //            berunfile.AppendLine("Successfully Created BE File in following Location: '" + FILEPATH + "'");
        //            txtBeFile.Text = berunfile.ToString();
        //            updatejob();
        //        }
        //        else
        //        {
        //            tw.Flush();
        //            tw.Close();
        //            string[] filePaths11 = Directory.GetFiles(@pathdir1);
        //            foreach (string filePathss in filePaths11)
        //            {
        //                //if (!CheckIfFileIsBeingUsed(filePathss))
        //                //{
        //                if (filePathss == @pathdir1 + pathdir)
        //                {
        //                    File.Delete(filePathss);
        //                }
        //                // }

        //            }
        //            txtBeFile.Text = bemanfile.ToString();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }

        //}

       
    }
}