using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using System.Configuration;

namespace ImpexCube.OPReport
{
    public partial class frmImpJobStatus : System.Web.UI.Page
    {
        string con = (string)ConfigurationManager.ConnectionStrings["Constr"].ConnectionString;
        VTS.ImpexCube.Business.InvoiceDetailsBL invBL = new VTS.ImpexCube.Business.InvoiceDetailsBL();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DropJobNo();
            }
            
        }

        private void DropJobNo()
        {
            DataSet ds = invBL.GetJobNo();
            if (ds.Tables["jobno"].Rows.Count != 0)
            {
                ddlJobNo.DataSource = ds;
                ddlJobNo.DataTextField = "JobNo";
                ddlJobNo.DataValueField = "JobNo";
                ddlJobNo.DataBind();
                // ddlJobNo.Items.Insert(0, new ListItem("-Select-", "0"));

            }
            else
            {
                ddlJobNo.DataSource = null;
                ddlJobNo.DataBind();
            }
        }

        protected void btnGenerate_Click(object sender, EventArgs e)
        {
            try
            {
                StringBuilder Query = new StringBuilder();

                Query.Append("SELECT dbo.T_JobCreation.JobNo, dbo.T_JobCreation.JobReceivedDate, dbo.T_Importer.Importer, dbo.T_Importer.Consignor, dbo.T_Importer.ConsignorAddress,");
                Query.Append("dbo.T_Importer.ImporterCode, dbo.T_Importer.Address, dbo.T_ShipmentDetails.CountryOrigin, dbo.T_ShipmentDetails.ShipmentPort, dbo.T_JobCreation.BEType, ");
                Query.Append("dbo.T_JobCreation.BENo, dbo.T_JobCreation.BEDate, dbo.T_Product.BasDutyAmtPer, dbo.T_ShipmentDetails.HouseBLNo, dbo.T_ShipmentDetails.HouseBLDate, ");
                Query.Append("dbo.T_ShipmentDetails.GatewayIGMNo, dbo.T_ShipmentDetails.GatewayIGMDate, dbo.T_ShipmentDetails.GrossWeight, dbo.T_ShipmentDetails.NoOfPackages, ");
                Query.Append("dbo.T_ShipmentDetails.MarksNos, dbo.T_ShipmentDetails.VesselName, dbo.T_InvoiceDetails.InvoiceNo, dbo.T_InvoiceDetails.InvoiceDate, ");
                Query.Append("dbo.T_InvoiceDetails.InvoiceProductINRValues, dbo.T_InvoiceDetails.FreightTyCurrency, dbo.T_InvoiceDetails.FreightTyExRate, ");
                Query.Append("dbo.T_InvoiceDetails.InsuranceTyCurrency, dbo.T_InvoiceDetails.InsuranceTyExRate,dbo.T_Product.AssableValue ");
                Query.Append("FROM dbo.T_JobCreation INNER JOIN ");
                Query.Append("dbo.T_Importer ON dbo.T_JobCreation.JobNo = dbo.T_Importer.JobNo INNER JOIN ");
                Query.Append("dbo.T_ShipmentDetails ON dbo.T_JobCreation.JobNo = dbo.T_ShipmentDetails.JobNo INNER JOIN ");
                Query.Append("dbo.T_InvoiceDetails ON dbo.T_JobCreation.JobNo = dbo.T_InvoiceDetails.JobNo INNER JOIN ");
                Query.Append("dbo.T_Product ON dbo.T_JobCreation.JobNo = dbo.T_Product.JobNo where dbo.T_JobCreation.JobNo= '" + ddlJobNo.SelectedValue + "'");
                DataSet ds = new DataSet();
                SqlConnection sqlConn = new SqlConnection(con);
                sqlConn.Open();
                SqlDataAdapter da = new SqlDataAdapter(Query.ToString(), sqlConn);
                da.Fill(ds, "Table");
                sqlConn.Close();

                if (ds.Tables["Table"].Rows.Count != 0)
                {
                    string Branch = "Chennai";
                    DataRowView row = ds.Tables["Table"].DefaultView[0];
                    lblJobNo.Text = row["JobNo"].ToString();
                    lblDocRecd.Text = row["JobReceivedDate"].ToString();
                    lblImporter.Text = row["Importer"].ToString();
                    lblImporterAddr.Text = row["Address"].ToString();
                    lblConsignor.Text = row["Consignor"].ToString();
                    lblConsignorAddr.Text = row["ConsignorAddress"].ToString();
                    lblPortOfLoading.Text = row["CountryOrigin"].ToString();
                    lblPortOfDestination.Text = row["ShipmentPort"].ToString();
                    lblBEType.Text = row["BEType"].ToString();
                    lblBENo.Text = row["BENo"].ToString() + row["BEDate"].ToString();
                    lblDutyAmount.Text = row["BasDutyAmtPer"].ToString();
                    lblBLNo.Text = row["HouseBLNo"].ToString() + row["HouseBLDate"].ToString();
                    lblIGMNo.Text = row["GatewayIGMNo"].ToString() + row["GatewayIGMDate"].ToString();
                    lblGrossWt.Text = row["GrossWeight"].ToString();
                    lblNoOfPackages.Text = row["NoOfPackages"].ToString();
                    lblMarks.Text = row["MarksNos"].ToString();
                    lblVessel.Text = row["VesselName"].ToString();
                    lblInvoiceNo.Text = row["InvoiceNo"].ToString() + " " + row["InvoiceDate"].ToString();
                    lblInValue.Text = row["InvoiceProductINRValues"].ToString();
                    lblFreight.Text = row["FreightTyCurrency"].ToString() + " " + row["FreightTyExRate"].ToString();
                    lblInsurance.Text = row["InsuranceTyCurrency"].ToString() + " " + row["InsuranceTyExRate"].ToString();
                    lblAssblValue.Text = row["AssableValue"].ToString();
                    lblDate1.Text = DateTime.Now.ToString("dd-MMMM-yyyy hh:mm tt");
                    lblDate.Text = DateTime.Now.ToString("dd-MMMM-yyyy");
                    GetCompanyDetails(Branch);
                }
                else
                {
                    lblJobNo.Text = "";
                    lblDocRecd.Text = ""; ;
                    lblImporter.Text ="";
                    lblImporterAddr.Text = "";
                    lblConsignor.Text = "";
                    lblConsignorAddr.Text = "";
                    lblPortOfLoading.Text = "";
                    lblPortOfDestination.Text = "";
                    lblBEType.Text = "";
                    lblBENo.Text = "";
                    lblDutyAmount.Text = "";
                    lblBLNo.Text = "";
                    lblIGMNo.Text = "";
                    lblGrossWt.Text = "";
                    lblNoOfPackages.Text = "";
                    lblMarks.Text = "";
                    lblVessel.Text = "";
                    lblInvoiceNo.Text = "";
                    lblInValue.Text = "";
                    lblFreight.Text = "";
                    lblInsurance.Text = "";
                    lblAssblValue.Text = ""; 
                    lblDate1.Text = DateTime.Now.ToString("dd-MMMM-yyyy hh:mm tt");
                    lblDate.Text = DateTime.Now.ToString("dd-MMMM-yyyy");
                }
            }
            catch (Exception ex)
            {
                string Message = ex.Message;
            }
        }

        private void GetCompanyDetails(string Branch)
        {
            DataSet ds = new DataSet();
            SqlConnection sqlcon = new SqlConnection(con);
            sqlcon.Open();
            string CompQuery = "select [CompanyName] from [M_CompanyDetails]";
            SqlDataAdapter da = new SqlDataAdapter(CompQuery, sqlcon);
            da.Fill(ds, "Comp");
            sqlcon.Close();
            if(ds.Tables["Comp"].Rows.Count!=0)
            {
                DataRowView row = ds.Tables["Comp"].DefaultView[0];
                lblCompName.Text = row["CompanyName"].ToString();
            }
        }
    }
}