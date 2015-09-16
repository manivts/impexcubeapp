using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
//using MySql;
//using MySql.Data.MySqlClient;
using System.Data.SqlClient;

namespace ImpexCube.OPReport
{
    public partial class frmImportLetter : System.Web.UI.Page
    {
        //VTS.ImpexCube.Business.Importer objimporter = new VTS.ImpexCube.Business.Importer();
        string strconn = (string)ConfigurationManager.AppSettings["ConnectionAdmin"];
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                PnlDutyCallLetter.Visible = false;
                PnlStampLetter.Visible = false;
                PnlImportDocket.Visible = false;
                PnlSmallDeclaration.Visible = false;
            }           
        }

        protected void btnGenerate_Click(object sender, EventArgs e)
        {
            string LetterName = ddlLetter.SelectedValue;
            string JobNo = txtJobNo.Text;

            if (LetterName == "Duty Call Letter")
            {
                PnlDutyCallLetter.Visible = true;
                PnlStampLetter.Visible = false;
                PnlImportDocket.Visible = false;
                PnlSmallDeclaration.Visible = false;

                lblRefNo.Text = txtJobNo.Text;
                
                SqlConnection con = new SqlConnection(strconn);
                con.Open();
                string qry = "Select JobReceivedDate,BENo,BEDate from T_JobCreation where JobNo='" + JobNo + "'";
                SqlDataAdapter da = new SqlDataAdapter(qry, con);
                DataSet ds = new DataSet();
                da.Fill(ds, "data");
                if (ds.Tables["data"].Rows.Count != 0)
                {
                    DataRowView row = ds.Tables["data"].DefaultView[0];
                    lbljobdate.Text = row["JobReceivedDate"].ToString();
                }
                string qry1 = "Select FreightAmount,FreightExchangeRate,FreightINRAmount,InsuranceINRAmount,MisINRAmount from T_InvoiceDetails where JobNo='" + JobNo + "'";
                SqlDataAdapter da1 = new SqlDataAdapter(qry1, con);
                DataSet ds1 = new DataSet();
                da1.Fill(ds1, "data1");
                if (ds1.Tables["data1"].Rows.Count != 0)
                {
                    DataRowView row1 = ds1.Tables["data1"].DefaultView[0];
                    lblUSD.Text = row1["FreightAmount"].ToString();
                    lblExrate.Text = row1["FreightExchangeRate"].ToString();
                    lblFreiRs.Text = row1["FreightINRAmount"].ToString();
                    lblIasRs.Text = row1["InsuranceINRAmount"].ToString();
                    lblTotAccsValue.Text = row1["MisINRAmount"].ToString();
                }
                string qry2 = "Select BasicDutyAmount,TotalCVDAmt,EduCessAmount,SADAmt,TotalDutyAmt,ExHealthCessAmount from T_Product where  JobNo='" + JobNo + "'";
                SqlDataAdapter da2 = new SqlDataAdapter(qry2, con);
                DataSet ds2 = new DataSet();
                da2.Fill(ds2, "data2");
                if (ds2.Tables["data2"].Rows.Count != 0)
                {
                    DataRowView row2 = ds2.Tables["data2"].DefaultView[0];
                    lblBasic.Text = row2["BasicDutyAmount"].ToString();
                    lblCVD.Text = row2["TotalCVDAmt"].ToString();
                    lblEdCess.Text = row2["ExHealthCessAmount"].ToString();
                    lblSAD.Text = row2["SADAmt"].ToString();
                    lblTotDuty.Text = row2["TotalDutyAmt"].ToString();
                    lblHEdCess1.Text = row2["ExHealthCessAmount"].ToString();
                    lblCess.Text = row2["EduCessAmount"].ToString();
                }
            }

            else if (LetterName == "Stamp Letter")
            {
                PnlStampLetter.Visible = true;
                PnlImportDocket.Visible = false;
                PnlSmallDeclaration.Visible = false;
                PnlDutyCallLetter.Visible = false;

                lblChaNameAddr.Text = "PI Logistics (India) Pvt. Ltd. PLOT No.E-17 SECTOR N.U.4 SAPNA NAGAR,GANDHIDHAM KUTCH,GUJARAT,370201";

                SqlConnection con = new SqlConnection(strconn);
                con.Open();
                string qry = "Select VesselName,LocalIGMNo,LocalIGMDate,NetWeight from T_ShipmentDetails where JobNo='" + JobNo + "'";
                SqlDataAdapter da = new SqlDataAdapter(qry, con);
                DataSet ds = new DataSet();
                da.Fill(ds, "data");
                if (ds.Tables["data"].Rows.Count != 0)
                {
                    DataRowView row = ds.Tables["data"].DefaultView[0];
                    lblIgmNoDate.Text = row["LocalIGMNo"].ToString() +"/ "+ row["LocalIGMDate"].ToString();
                    lblVesselname.Text = row["VesselName"].ToString();
                    lblManiWt.Text = row["NetWeight"].ToString();
                }

                string qry1 = "Select Importer from T_Importer where JobNo='" + JobNo + "'";
                SqlDataAdapter da1 = new SqlDataAdapter(qry1, con);
                DataSet ds1 = new DataSet();
                da1.Fill(ds1, "data1");
                if (ds1.Tables["data1"].Rows.Count != 0)
                {
                    DataRowView row1 = ds1.Tables["data1"].DefaultView[0];
                    lblImpExpNameAddr.Text = row1["Importer"].ToString();
                }

                string qry2 = "Select AssableValue,CVDDutyAmtPer,EduCessAmount from T_Product where JobNo='" + JobNo + "'";
                SqlDataAdapter da2 = new SqlDataAdapter(qry2, con);
                DataSet ds2 = new DataSet();
                da2.Fill(ds2, "data2");
                if (ds2.Tables["data2"].Rows.Count != 0)
                {
                    DataRowView row2 = ds2.Tables["data2"].DefaultView[0];
                    lblAssessableval.Text = row2["AssableValue"].ToString();
                    lblCustomDuty.Text = row2["CVDDutyAmtPer"].ToString();
                    lblEduCess.Text = row2["EduCessAmount"].ToString();
                }

                string qry3 = "Select ValueDebited from T_Schemes where JobNo='" + JobNo + "'";
                SqlDataAdapter da3 = new SqlDataAdapter(qry3, con);
                DataSet ds3 = new DataSet();
                da3.Fill(ds3, "data3");
                if (ds3.Tables["data3"].Rows.Count != 0)
                {
                    DataRowView row3= ds3.Tables["data2"].DefaultView[0];
                    lblDebAmnt.Text = row3["ValueDebited"].ToString();                   
                }
            }

            else if (LetterName == "Import Docket")
            {
                PnlImportDocket.Visible = true;
                PnlStampLetter.Visible = false;
                PnlSmallDeclaration.Visible = false;
                PnlDutyCallLetter.Visible = false;

                lbljobno.Text = txtJobNo.Text;
                SqlConnection con = new SqlConnection(strconn);
                con.Open();
                string qry = "Select JobReceivedDate,Mode,BEType,JobDate from T_JobCreation where JobNo='" + JobNo + "'";
                SqlDataAdapter da = new SqlDataAdapter(qry, con);
                DataSet ds = new DataSet();
                da.Fill(ds, "data");
                if (ds.Tables["data"].Rows.Count != 0)
                {
                    DataRowView row = ds.Tables["data"].DefaultView[0];
                    lblreceiveddate.Text = row["JobReceivedDate"].ToString();
                    lblairorsea.Text = row["Mode"].ToString();
                    lbltypeofBe.Text = row["BEType"].ToString();
                    lbldate.Text = row["JobDate"].ToString();
                }

                string qry1 = "Select Importer,Address from T_Importer where JobNo='" + JobNo + "'";
                SqlDataAdapter da1 = new SqlDataAdapter(qry1, con);
                DataSet ds1 = new DataSet();
                da1.Fill(ds1, "data1");
                if (ds1.Tables["data1"].Rows.Count != 0)
                {
                    DataRowView row1 = ds1.Tables["data1"].DefaultView[0];
                    lblImportername.Text = row1["Importer"].ToString();
                    lbladdress.Text = row1["Address"].ToString();
                }

                string qry2 = "Select MasterBLNo,MasterBLDate,HouseBLNo,HouseBLDate,NoOfPackages,GrossWeight from T_ShipmentDetails where JobNo='" + JobNo + "'";
                SqlDataAdapter da2 = new SqlDataAdapter(qry2, con);
                DataSet ds2 = new DataSet();
                da2.Fill(ds2, "data2");
                if (ds2.Tables["data2"].Rows.Count != 0)
                {
                    DataRowView row2 = ds2.Tables["data2"].DefaultView[0];
                    lblHawbno.Text = row2["HouseBLNo"].ToString();
                    lblhawbdated.Text = row2["HouseBLDate"].ToString();
                    lblBLAWBNo.Text = row2["MasterBLNo"].ToString();
                    lblDatedBLAWBNo.Text = row2["MasterBLDate"].ToString();
                }

                string qry3 = "Select InvoiceNo,InvoiceDate,InvoiceExchangeRates,InvoiceProductValues from T_InvoiceDetails where JobNo='" + JobNo + "'";
                SqlDataAdapter da3 = new SqlDataAdapter(qry3, con);
                DataSet ds3 = new DataSet();
                da3.Fill(ds3, "data3");
                if (ds3.Tables["data3"].Rows.Count != 0)
                {
                    DataRowView row3 = ds3.Tables["data3"].DefaultView[0];
                    lblInvNo.Text = row3["InvoiceNo"].ToString();
                    lblinvoicedated.Text = row3["InvoiceDate"].ToString();
                    lblcontaininginvoicevalue.Text = row3["InvoiceProductValues"].ToString();
                    lblexchrate.Text = row3["InvoiceExchangeRates"].ToString();
                }

            }

            else if (LetterName == "Small Declaration")
            {
                PnlSmallDeclaration.Visible = true;
                PnlImportDocket.Visible = false;
                PnlStampLetter.Visible = false;
                PnlDutyCallLetter.Visible = false;
            }
        }
    }
}