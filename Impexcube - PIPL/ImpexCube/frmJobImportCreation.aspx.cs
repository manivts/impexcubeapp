using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using System.Data.SqlClient;
using System.Configuration;
using VTS.ImpexCube.Data;
using System.Text;

namespace ImpexCube
{
    public partial class frmJobImportCreation : System.Web.UI.Page
    {
        string strconn = (string)ConfigurationManager.ConnectionStrings["Constr"].ConnectionString;
        VTS.ImpexCube.Business.JobImportCreationBL objJobImportCreation = new VTS.ImpexCube.Business.JobImportCreationBL();
        int Result = 0;

        private string chalicence;
        private string chalicences;
        private string customidair;
        private string customidsea;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack == false)
            {
                btnUpdate.Visible = false;
                btnSave.Enabled = true;
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if(rbjobstage.SelectedValue=="Import")
            {
            savejobcreation();
            saveimporter();
            saveshipment();
            saveinvoice();
            shipmentcontainerinfo();
            ScriptManager.RegisterStartupScript(this, this.GetType(), "redirect", "alert('Import Saved Successfully'); window.location.href='frmJobImportCreation.aspx';", true);
            }
            else if(rbjobstage.SelectedValue=="Export")
            {
                saveexjobcreation();
                saveexporter();                
                saveexinvoice();
                shipmentexcontainerinfo();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "redirect", "alert('Export Saved Successfully'); window.location.href='frmJobImportCreation.aspx';", true);

            }
            else
            {
             Response.Write("<script LANGUAGE='JavaScript' >alert('Please select Import or Export')</script>");
            }
        }
        public void saveexjobcreation()
        {
            StringBuilder Query = new StringBuilder();
            string Message = string.Empty;
            DataSet ds = new DataSet();
            using (SqlConnection con = new SqlConnection(strconn))
            {
                con.Open();
                Query.Append("INSERT INTO [E_M_JobCreation] (JobNo,JobReceivedOn,TransportMode)");
                Query.Append("values(@JobNo,@JobReceivedOn,@TransportMode)");
                SqlCommand cmd = new SqlCommand(Query.ToString(), con);
                cmd.Parameters.AddWithValue("@JobNo", txtJobNo.Text);
                cmd.Parameters.AddWithValue("@JobReceivedOn", txtJobDate.Text);
                cmd.Parameters.AddWithValue("@TransportMode", ddlShipmentType.SelectedValue);
                Result = cmd.ExecuteNonQuery();
                con.Close();
            }
        }

        public void saveexporter()
        {

             StringBuilder Query = new StringBuilder();
            string Message = string.Empty;
            DataSet ds = new DataSet();
            using (SqlConnection con = new SqlConnection(strconn))
            {
                con.Open();
                Query.Append("INSERT INTO [E_T_Exporter] (JobNo,ConsigneeAddress,ExporterName,ExporterAddress,BranchSno)");
                Query.Append("values(@JobNo,@ConsigneeAddress,@ExporterName,@ExporterAddress,@BranchSno)");
                SqlCommand cmd = new SqlCommand(Query.ToString(), con);
                cmd.Parameters.AddWithValue("@JobNo", txtJobNo.Text);
                cmd.Parameters.AddWithValue("@ConsigneeAddress", txtConsigneeAddress.Text);
                cmd.Parameters.AddWithValue("@ExporterName", txtImpExpName.Text);
                cmd.Parameters.AddWithValue("@ExporterAddress", txtImpExpAddress.Text);
                cmd.Parameters.AddWithValue("@BranchSno", txtImpExpBranchCode.Text);
                Result = cmd.ExecuteNonQuery();
                con.Close();
            }
        }

        public void shipmentexcontainerinfo()
        {

            StringBuilder Query = new StringBuilder();
            string Message = string.Empty;
            DataSet ds = new DataSet();
            using (SqlConnection con = new SqlConnection(strconn))
            {
                con.Open();
                Query.Append("INSERT INTO [E_T_Container] (JobNo,Type)");
                Query.Append("values(@JobNo,@Type)");
                SqlCommand cmd = new SqlCommand(Query.ToString(), con);
                cmd.Parameters.AddWithValue("@JobNo", txtJobNo.Text);
                cmd.Parameters.AddWithValue("@Type", ddlModeOfShipment.SelectedValue);
                Result = cmd.ExecuteNonQuery();
                con.Close();

            }



        }

        public void shipmentcontainerinfo()
        {
            StringBuilder Query = new StringBuilder();
            string Message = string.Empty;
            DataSet ds = new DataSet();
            using (SqlConnection con = new SqlConnection(strconn))
            {
                con.Open();
                Query.Append("INSERT INTO [T_ShipmentContainerInfo] (JobNo,LoadType)");
                Query.Append("values(@JobNo,@LoadType)");
                SqlCommand cmd = new SqlCommand(Query.ToString(), con);
                cmd.Parameters.AddWithValue("@JobNo", txtJobNo.Text);
                cmd.Parameters.AddWithValue("@LoadType", ddlModeOfShipment.SelectedValue);
                Result = cmd.ExecuteNonQuery();
                con.Close();
             
            }
        }

        public void savejobcreation()
        {
            StringBuilder Query = new StringBuilder();
            string Message = string.Empty;
            DataSet ds = new DataSet();
            using (SqlConnection con = new SqlConnection(strconn))
            {
                con.Open();
                Query.Append("INSERT INTO [T_JobCreation] (JobNo,JobReceivedDate)");
                Query.Append("values(@JobNo,@JobReceivedDate)");
                SqlCommand cmd = new SqlCommand(Query.ToString(), con);
                cmd.Parameters.AddWithValue("@JobNo", txtJobNo.Text);
                cmd.Parameters.AddWithValue("@JobReceivedDate", txtJobDate.Text);
                Result = cmd.ExecuteNonQuery();
                con.Close();
            }
        }

        public void saveimporter()
        {

            StringBuilder Query = new StringBuilder();
            string Message = string.Empty;
            DataSet ds = new DataSet();
            using (SqlConnection con = new SqlConnection(strconn))
            {
                con.Open();
                Query.Append("INSERT INTO [T_Importer] (JobNo,ConsignorAddress,Importer,ImporterCode,Address,ImporterType,BranchSno)");
                Query.Append("values(@JobNo,@ConsignorAddress,@Importer,@ImporterCode,@Address,@ImporterType,@BranchSno)");
                SqlCommand cmd = new SqlCommand(Query.ToString(), con);
                cmd.Parameters.AddWithValue("@JobNo", txtJobNo.Text);
                cmd.Parameters.AddWithValue("@ConsignorAddress", txtConsigneeAddress.Text);
                cmd.Parameters.AddWithValue("@Importer", txtImpExpName.Text);
                cmd.Parameters.AddWithValue("@ImporterCode", txtImpExpCode.Text);
                cmd.Parameters.AddWithValue("@Address", txtImpExpAddress.Text);
                cmd.Parameters.AddWithValue("@ImporterType", ddlImpExpClassType.SelectedValue);
                cmd.Parameters.AddWithValue("@BranchSno", txtImpExpBranchCode.Text);
                Result = cmd.ExecuteNonQuery();
                con.Close();

            }

        }

        public void saveshipment()
        {

            StringBuilder Query = new StringBuilder();
            string Message = string.Empty;
            DataSet ds = new DataSet();
            using (SqlConnection con = new SqlConnection(strconn))
            {
                con.Open();
                Query.Append("INSERT INTO [T_ShipmentDetails] (JobNo,PortOfOrigin,ShipmentPortCode,ShipmentUneceCode,CountryOriginCode)");
                Query.Append("values(@JobNo,@PortOfOrigin,@ShipmentPortCode,@ShipmentUneceCode,@CountryOriginCode)");
                SqlCommand cmd = new SqlCommand(Query.ToString(), con);
                cmd.Parameters.AddWithValue("@JobNo", txtJobNo.Text);
                cmd.Parameters.AddWithValue("@PortOfOrigin", txtPortOfOrigin.Text);
                cmd.Parameters.AddWithValue("@ShipmentPortCode", txtOriginPortCode.Text);
                cmd.Parameters.AddWithValue("@ShipmentUneceCode", txtOriginStateCode.Text);
                cmd.Parameters.AddWithValue("@CountryOriginCode", txtOriginCountryCode.Text);
                Result = cmd.ExecuteNonQuery();
                con.Close();

            }

        }

        public void saveexinvoice()
        {


            StringBuilder Query = new StringBuilder();
            string Message = string.Empty;
            DataSet ds = new DataSet();
            using (SqlConnection con = new SqlConnection(strconn))
            {
                con.Open();
                Query.Append("INSERT INTO [E_T_Invoice] (JobNo,InvoiceNo,InvoiceDate)");
                Query.Append("values(@JobNo,@InvoiceNo,@InvoiceDate)");
                SqlCommand cmd = new SqlCommand(Query.ToString(), con);
                cmd.Parameters.AddWithValue("@JobNo", txtJobNo.Text);
                cmd.Parameters.AddWithValue("@InvoiceNo", txtInvoiceNo.Text);
                cmd.Parameters.AddWithValue("@InvoiceDate", txtInvoiceDate.Text);
                Result = cmd.ExecuteNonQuery();
                con.Close();
            }
        }

        public void saveinvoice()
        {
            StringBuilder Query = new StringBuilder();
            string Message = string.Empty;
            DataSet ds = new DataSet();
            using (SqlConnection con = new SqlConnection(strconn))
            {
                con.Open();
                Query.Append("INSERT INTO [T_InvoiceDetails] (JobNo,InvoiceNo,InvoiceDate)");
                Query.Append("values(@JobNo,@InvoiceNo,@InvoiceDate)");
                SqlCommand cmd = new SqlCommand(Query.ToString(), con);
                cmd.Parameters.AddWithValue("@JobNo", txtJobNo.Text);
                cmd.Parameters.AddWithValue("@InvoiceNo", txtInvoiceNo.Text);
                cmd.Parameters.AddWithValue("@InvoiceDate", txtInvoiceDate.Text);
                Result = cmd.ExecuteNonQuery();
                con.Close();

            }
        }



        public void Save()
        {
            if (txtJobNo.Text != "")
            {
                if (txtJobDate.Text != "")
                {
                    int result = new int();
                    string date = DateTime.Now.ToString("dd/MM/yyyy");
                    result = objJobImportCreation.insertJobImportCreation(txtJobNo.Text, txtJobDate.Text, ddlShipmentType.SelectedValue, ddlModeOfShipment.SelectedValue, txtImpExpName.Text,
                        txtImpExpCode.Text, txtImpExpBranchCode.Text, txtImpExpAddress.Text, ddlImpExpClassType.SelectedValue, txtPortOfOrigin.Text, txtOriginPortCode.Text, txtOriginStateCode.Text,
                        txtOriginCountryCode.Text, txtPortOfDestination.Text, txtDestinationPortCode.Text, txtDestinationStateCode.Text, txtDestinationCountryCode.Text, txtInvoiceNo.Text, txtInvoiceDate.Text,
                        txtConsigneeAddress.Text, txtForeignExchangeBankCode.Text, (string)Session["FYear"], (string)Session["UserName"], date, (string)Session["UserName"], date);

                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Successfully Saved');", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Please enter Job Date');", true);
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Please enter Job No');", true);
            }
        }

        protected void gvJobNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtJobNo.Text = gvJobNo.SelectedRow.Cells[1].Text;
            DataSet ds = objJobImportCreation.SelectJobNo(txtJobNo.Text);
            if (ds.Tables["Jobdetails"].Rows.Count != 0)
            {
                DataRowView row = ds.Tables["Jobdetails"].DefaultView[0];
                Session["TransId"] = row["TransId"].ToString();
                DateTime JobDate = Convert.ToDateTime(row["JobDate"]);

                txtJobDate.Text = JobDate.ToString("dd/MM/yyyy");
                ddlShipmentType.SelectedValue = row["ShipmentType"].ToString();
                ddlModeOfShipment.SelectedValue = row["Mode"].ToString();
                txtImpExpName.Text = row["ImpExpName"].ToString();
                txtImpExpCode.Text = row["ImpExpCode"].ToString();
                txtImpExpBranchCode.Text = row["ImpExpBranchCode"].ToString();
                txtImpExpAddress.Text = row["ImpExpAddress"].ToString();
                ddlImpExpClassType.SelectedValue = row["ImpExpCassType"].ToString();
                txtPortOfOrigin.Text = row["PortOfOrigin"].ToString();
                txtOriginPortCode.Text = row["OriginPortCode"].ToString();
                txtOriginStateCode.Text = row["OriginStateCode"].ToString();
                txtOriginCountryCode.Text = row["OriginCountryCode"].ToString();
                txtPortOfDestination.Text = row["PortOfDestination"].ToString();
                txtDestinationPortCode.Text = row["DestinationPortCode"].ToString();
                txtDestinationStateCode.Text = row["DestinationStateCode"].ToString();
                txtDestinationCountryCode.Text = row["DestinationCountryCode"].ToString();
                txtInvoiceNo.Text = row["InvoiceNo"].ToString();
                string InvDate = row["InvoiceDate"].ToString();

                if (InvDate != "")
                {
                    DateTime InvoiceDate = Convert.ToDateTime(row["InvoiceDate"]);
                    txtInvoiceDate.Text = InvoiceDate.ToString("dd/MM/yyyy");
                }
                else
                {
                    txtInvoiceDate.Text = "";
                }
                txtConsigneeAddress.Text = row["ConsigneeAddress"].ToString();
                txtForeignExchangeBankCode.Text = row["ForeignExchangeBankCode"].ToString();
            }
            FillJobCreation();
            FillImporter();
            FillshipmentDetails();
            FillInvoiceDetails();
            FillShipmentContaineInfo();
            gvJobNo.Visible = false;
            txtJobNo.Enabled = false;
            btnSave.Visible = false;
            btnUpdate.Visible = true;
        }

        public void FillShipmentContaineInfo()
        {
            SqlConnection con = new SqlConnection(strconn);
            con.Open();
            string queryde = "select LoadType from T_ShipmentContainerInfo where JobNo='" + txtJobNo.Text + "'";
            DataSet ds = new DataSet();
            SqlDataAdapter sd = new SqlDataAdapter(queryde, con);
            sd.Fill(ds, "data");
            if (ds.Tables["data"].Rows.Count != 0)
            {
                DataRowView row = ds.Tables["data"].DefaultView[0];
                
                
                ddlModeOfShipment.SelectedValue = row["LoadType"].ToString();

            }

            con.Close();


        }

        public void FillJobCreation()
        {

            SqlConnection con = new SqlConnection(strconn);
            con.Open();
            string queryde = "select JobReceivedDate,Mode from T_JobCreation where JobNo='" + txtJobNo.Text + "'";
            DataSet ds = new DataSet();
            SqlDataAdapter sd = new SqlDataAdapter(queryde, con);
            sd.Fill(ds, "data");
            if (ds.Tables["data"].Rows.Count != 0)
            {
                DataRowView row = ds.Tables["data"].DefaultView[0];
                txtJobDate.Text = row["JobReceivedDate"].ToString();
                ddlShipmentType.SelectedValue = row["Mode"].ToString();
                

            }

            con.Close();
        }

        public void FillImporter()
        {
            SqlConnection con = new SqlConnection(strconn);
            con.Open();
            string queryde = "select ConsignorAddress,Importer,ImporterCode,Address,ImporterType,BranchSno from T_Importer where JobNo='" + txtJobNo.Text + "' ";
            DataSet ds = new DataSet();
            SqlDataAdapter sd = new SqlDataAdapter(queryde, con);
            sd.Fill(ds, "data");
            if (ds.Tables["data"].Rows.Count != 0)
            {
                DataRowView row = ds.Tables["data"].DefaultView[0];
                txtConsigneeAddress.Text = row["ConsignorAddress"].ToString();
                txtImpExpName.Text = row["Importer"].ToString();
                txtImpExpCode.Text = row["ImporterCode"].ToString();
                txtImpExpAddress.Text = row["Address"].ToString();
                ddlImpExpClassType.SelectedValue = row["ImporterType"].ToString();
                txtImpExpBranchCode.Text = row["BranchSno"].ToString();
            }

            con.Close();
        }

        public void FillshipmentDetails()
        {
            SqlConnection con = new SqlConnection(strconn);
            con.Open();
            string queryde = "select PortOfOrigin,ShipmentPortCode,ShipmentUneceCode,CountryOriginCode from T_ShipmentDetails where JobNo='" + txtJobNo.Text + "' ";
            DataSet ds = new DataSet();
            SqlDataAdapter sd = new SqlDataAdapter(queryde, con);
            sd.Fill(ds, "data");
            if (ds.Tables["data"].Rows.Count != 0)
            {
                DataRowView row = ds.Tables["data"].DefaultView[0];
                txtPortOfOrigin.Text = row["PortOfOrigin"].ToString();
                txtOriginPortCode.Text = row["ShipmentPortCode"].ToString();
                txtOriginStateCode.Text = row["ShipmentUneceCode"].ToString();
                txtOriginCountryCode.Text = row["CountryOriginCode"].ToString();
            }

            con.Close();
        }

        public void FillInvoiceDetails()
        {
            SqlConnection con = new SqlConnection(strconn);
            con.Open();
            string queryde = "select InvoiceNo,InvoiceDate from T_InvoiceDetails where JobNo='" + txtJobNo.Text + "' ";
            DataSet ds = new DataSet();
            SqlDataAdapter sd = new SqlDataAdapter(queryde, con);
            sd.Fill(ds, "data");
            if (ds.Tables["data"].Rows.Count != 0)
            {
                DataRowView row = ds.Tables["data"].DefaultView[0];
                txtInvoiceNo.Text = row["InvoiceNo"].ToString();
                txtInvoiceDate.Text = row["InvoiceDate"].ToString();
            }


            con.Close();
        }

        protected void btnView_Click(object sender, EventArgs e)
        {

            DataSet ds = objJobImportCreation.GridJobCreation();
            if (ds.Tables["jobcreation"].Rows.Count != 0)
            {
                btnSave.Visible = false;
                btnUpdate.Visible = true;
                gvJobNo.Visible = true;
                gvJobNo.DataSource = ds;
                gvJobNo.DataBind();
            }
        }

        protected void btnNew_Click(object sender, EventArgs e)
        {
            btnSave.Visible = true;
            Response.Redirect("frmJobImportCreation.aspx");
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            btnUpdate.Visible = false;
            btnSave.Visible = true;
            txtJobNo.Enabled = false;
            UpdateJobCreation();
            UpdateImporter();
            UpdateShipmentDetails();
            UpdateInvoiceDetails();
            UpdateShipmentContainerInfo();

            //int result = new int();
            //string date = DateTime.Now.ToString("dd/MM/yyyy");

            //result = objJobImportCreation.UpdateJobImportCreation(txtJobNo.Text, txtJobDate.Text, ddlShipmentType.SelectedValue, ddlModeOfShipment.SelectedValue, txtImpExpName.Text,
            //    txtImpExpCode.Text, txtImpExpBranchCode.Text, txtImpExpAddress.Text, ddlImpExpClassType.SelectedValue, txtPortOfOrigin.Text, txtOriginPortCode.Text, txtOriginStateCode.Text,
            //    txtOriginCountryCode.Text, txtPortOfDestination.Text, txtDestinationPortCode.Text, txtDestinationStateCode.Text, txtDestinationCountryCode.Text, txtInvoiceNo.Text, txtInvoiceDate.Text, txtConsigneeAddress.Text, txtForeignExchangeBankCode.Text, (string)Session["UserName"], date, (string)Session["TransId"]);
            //ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Updated Successfully');", true);
            ScriptManager.RegisterStartupScript(this, this.GetType(), "redirect", "alert('Updated Successfully'); window.location.href='frmJobImportCreation.aspx';", true);
            Clear();
        }

        public void UpdateShipmentContainerInfo()
        {
            StringBuilder Query = new StringBuilder();
            DataSet ds = new DataSet();
            using (SqlConnection con = new SqlConnection(strconn))
            {

                con.Open();
                Query.Append("UPDATE  [T_ShipmentContainerInfo] SET LoadType=@LoadType where JobNo='" + txtJobNo.Text + "' ");
                SqlCommand cmd = new SqlCommand(Query.ToString(), con);                
                cmd.Parameters.AddWithValue("@LoadType", ddlModeOfShipment.SelectedValue);
                Result = cmd.ExecuteNonQuery();
                con.Close();

            }

        }

        public void UpdateJobCreation()
        {
            StringBuilder Query = new StringBuilder();
            DataSet ds = new DataSet();
            using (SqlConnection con = new SqlConnection(strconn))
            {

                con.Open();
                Query.Append("UPDATE  [T_JobCreation] SET JobReceivedDate=@JobReceivedDate,Mode=@Mode where JobNo='" + txtJobNo.Text + "' ");
                SqlCommand cmd = new SqlCommand(Query.ToString(), con);
                cmd.Parameters.AddWithValue("@JobReceivedDate", txtJobDate.Text);
                cmd.Parameters.AddWithValue("@Mode", ddlShipmentType.SelectedValue);
                Result = cmd.ExecuteNonQuery();
                con.Close();

            }

        }

        public void UpdateImporter()
        {
            StringBuilder Query = new StringBuilder();
            DataSet ds = new DataSet();
            using (SqlConnection con = new SqlConnection(strconn))
            {

                con.Open();
                Query.Append("UPDATE [T_Importer] SET ConsignorAddress=@ConsignorAddress,Importer=@Importer,ImporterCode=@ImporterCode,Address=@Address,ImporterType=@ImporterType,BranchSno=@BranchSno where JobNo='" + txtJobNo.Text + "'");
                SqlCommand cmd = new SqlCommand(Query.ToString(), con);
                cmd.Parameters.AddWithValue("@ConsignorAddress", txtConsigneeAddress.Text);
                cmd.Parameters.AddWithValue("@Importer", txtImpExpName.Text);
                cmd.Parameters.AddWithValue("@ImporterCode", txtImpExpCode.Text);
                cmd.Parameters.AddWithValue("@Address", txtImpExpAddress.Text);
                cmd.Parameters.AddWithValue("@ImporterType", ddlImpExpClassType.SelectedValue);
                cmd.Parameters.AddWithValue("@BranchSno", txtImpExpBranchCode.Text);
                Result = cmd.ExecuteNonQuery();
                con.Close();


            }
        }

        public void UpdateShipmentDetails()
        {
            StringBuilder Query = new StringBuilder();
            DataSet ds = new DataSet();
            using (SqlConnection con = new SqlConnection(strconn))
            {
                con.Open();
                Query.Append("UPDATE [T_ShipmentDetails] SET PortOfOrigin=@PortOfOrigin,ShipmentPortCode=@ShipmentPortCode,ShipmentUneceCode=@ShipmentUneceCode,CountryOriginCode=@CountryOriginCode where JobNo='" + txtJobNo.Text + "'");
                SqlCommand cmd = new SqlCommand(Query.ToString(), con);
                cmd.Parameters.AddWithValue("@PortOfOrigin", txtPortOfOrigin.Text);
                cmd.Parameters.AddWithValue("@ShipmentPortCode", txtOriginPortCode.Text);
                cmd.Parameters.AddWithValue("@ShipmentUneceCode", txtOriginStateCode.Text);
                cmd.Parameters.AddWithValue("@CountryOriginCode", txtOriginCountryCode.Text);
                Result = cmd.ExecuteNonQuery();
                con.Close();

            }
        }

        public void UpdateInvoiceDetails()
        {
            StringBuilder Query = new StringBuilder();
            DataSet ds = new DataSet();
            using (SqlConnection con = new SqlConnection(strconn))
            {
                con.Open();
                Query.Append("UPDATE [T_InvoiceDetails] SET InvoiceNo=@InvoiceNo,InvoiceDate=@InvoiceDate where JobNo='" + txtJobNo.Text + "'");
                SqlCommand cmd = new SqlCommand(Query.ToString(), con);
                cmd.Parameters.AddWithValue("@InvoiceNo", txtInvoiceNo.Text);
                cmd.Parameters.AddWithValue("@InvoiceDate", txtInvoiceDate.Text);
                Result = cmd.ExecuteNonQuery();
                con.Close();



            }
        }

        public string splituptospace(string value)
        {
            string b = "";
            for (int i = 0; i < value.Length; i++)
            {
                if (Char.IsLetterOrDigit(value[i]))
                {
                    b += value[i];
                }
                else if (value[i] == ' ')
                {
                    b += value[i];
                }
                else if (Char.IsPunctuation(value[i]))
                {
                    b += value[i];

                }
                else
                {
                    i = value.Length;
                }
            }
            return b;
        }

        public void Clear()
        {
            txtJobNo.Text = "";
            txtJobDate.Text = "";
            ddlShipmentType.SelectedValue = "~Select~";
            ddlModeOfShipment.SelectedValue = "~Select~";
            txtImpExpName.Text = "";
            txtImpExpCode.Text = "";
            txtImpExpBranchCode.Text = "";
            txtImpExpAddress.Text = "";
            ddlImpExpClassType.SelectedValue = "~Select~";
            txtPortOfOrigin.Text = "";
            txtOriginPortCode.Text = "";
            txtOriginStateCode.Text = "";
            txtOriginCountryCode.Text = "";
            txtPortOfDestination.Text = "";
            txtDestinationPortCode.Text = "";
            txtDestinationStateCode.Text = "";
            txtDestinationCountryCode.Text = "";
            txtInvoiceNo.Text = "";
            txtInvoiceDate.Text = "";
            txtConsigneeAddress.Text = "";
            txtForeignExchangeBankCode.Text = "";
        }

        public void Excution(string query)
        {
            SqlConnection con = new SqlConnection(strconn);
            con.Open();
            SqlCommand cmd = new SqlCommand(query, con);
            cmd.ExecuteNonQuery();
            con.Close();
        }

        public void BEFileRead()
        {
            String line = "";

            SqlConnection con = new SqlConnection(strconn);
            con.Open();
            string queryde = "select ChaLicense,Chaidair,Chaidsea from tbl_details";
            DataSet ds = new DataSet();
            SqlDataAdapter sd = new SqlDataAdapter(queryde, con);
            sd.Fill(ds, "data");
            con.Close();
            if (ds.Tables["data"].Rows.Count != 0)
            {
                DataRowView row = ds.Tables["data"].DefaultView[0];
                chalicence = row["ChaLicense"].ToString();
                customidair = row["Chaidair"].ToString();
                customidsea = row["Chaidsea"].ToString();
            }


            string FilePath = "";

            string filename = System.IO.Path.GetFullPath(FileUpload1.PostedFile.FileName);
            if (filename != "")
            {
                string path = "";
                if (FileUpload1.HasFile)
                {
                    string paths = AppDomain.CurrentDomain.BaseDirectory;
                    string pathdir = Path.Combine(paths, @"TempFile\");
                    path = pathdir + Path.GetFileName(FileUpload1.PostedFile.FileName);
                    string[] filePaths = Directory.GetFiles(@pathdir);

                    foreach (string filePath in filePaths)
                    {
                        if (!CheckIfFileIsBeingUsed(filePath))
                        {
                            File.Delete(filePath);
                        }
                        // The file is not locked

                    }
                    FileUpload1.SaveAs(pathdir + Path.GetFileName(FileUpload1.PostedFile.FileName));
                    FilePath += path;
                }
                using (StreamReader sr = new StreamReader(FilePath))
                {
                    line = sr.ReadToEnd();
                    Console.WriteLine(line);

                }
            }
            string[] a = line.Split(new string[] { "<TABLE>" }, StringSplitOptions.RemoveEmptyEntries);
            int couunt = a.Length;
            string[] name = new string[17];
            string BE = "";
            string EXCHANGE = "";
            string PERMISSION = "";
            string INVOICE = "";
            string MISC_CH = "";
            string ITEMS = "";
            string LICENCE = "";
            string RSP = "";
            string DEPB = "";
            string BOND = "";
            string CERT = "";
            string HSS = "";
            string REIMPORT = "";
            string SBEDUTY = "";
            string IGMS = "";
            string CONTAINER = "";
            string AMEND = "";
            string head = a[0].TrimEnd('\n');
            string[] headers = head.Split(new string[] { "ZZ" }, StringSplitOptions.RemoveEmptyEntries);
            string heads = headers[0];
            string receiveid = headers[2].Substring(0, 7);
            string versionno = headers[2].Substring(7, 9);
            string msgid = headers[2].Substring(17, 9);
            string jobno = headers[2].Substring(27, 4);
            string jobdate = headers[2].Substring(32, 8);
            string jobtime = "";

            DataSet ds1 = new DataSet();
            SqlConnection sqlcon = new SqlConnection(strconn);
            string query12 = "select * from tbl_BEDetails where JobNo ='" + jobno + "'";
            SqlDataAdapter da = new SqlDataAdapter(query12, sqlcon);
            da.Fill(ds1, "Jobdetails");
            sqlcon.Close();
            if (ds1.Tables["Jobdetails"].Rows.Count == 0)
            {

                for (int i = 1; i < couunt; i++)
                {
                    if (a[i].Substring(0, 2) == "BE")
                    {
                        BE = a[i].TrimEnd('\n');
                    }
                    else if (a[i].Substring(0, 2) == "EX")
                    {
                        EXCHANGE = a[i].TrimEnd('\n');
                    }
                    else if (a[i].Substring(0, 2) == "PE")
                    {
                        PERMISSION = a[i].TrimEnd('\n');
                    }
                    else if (a[i].Substring(0, 2) == "IN")
                    {
                        INVOICE = a[i].TrimEnd('\n');
                    }
                    else if (a[i].Substring(0, 2) == "MI")
                    {
                        MISC_CH = a[i].TrimEnd('\n');
                    }
                    else if (a[i].Substring(0, 2) == "IT")
                    {
                        ITEMS = a[i].TrimEnd('\n');
                    }
                    else if (a[i].Substring(0, 2) == "LI")
                    {
                        LICENCE = a[i].TrimEnd('\n');
                    }
                    else if (a[i].Substring(0, 2) == "RS")
                    {
                        RSP = a[i].TrimEnd('\n');
                    }
                    else if (a[i].Substring(0, 2) == "DE")
                    {
                        DEPB = a[i].TrimEnd('\n');
                    }
                    else if (a[i].Substring(0, 2) == "BO")
                    {
                        BOND = a[i].TrimEnd('\n');
                    }
                    else if (a[i].Substring(0, 2) == "CE")
                    {
                        CERT = a[i].TrimEnd('\n');
                    }
                    else if (a[i].Substring(0, 2) == "HS")
                    {
                        HSS = a[i].TrimEnd('\n');
                    }
                    else if (a[i].Substring(0, 2) == "RE")
                    {
                        REIMPORT = a[i].TrimEnd('\n');
                    }
                    else if (a[i].Substring(0, 2) == "SB")
                    {
                        SBEDUTY = a[i].TrimEnd('\n');
                    }
                    else if (a[i].Substring(0, 2) == "IG")
                    {
                        IGMS = a[i].TrimEnd('\n');
                    }
                    else if (a[i].Substring(0, 2) == "CO")
                    {
                        CONTAINER = a[i].TrimEnd('\n');
                    }
                    else if (a[i].Substring(0, 2) == "AM")
                    {
                        AMEND = a[i].TrimEnd('\n');
                    }

                }
                //Be Details
                string jobs = "";
                string beno = "";
                string cntyorigin = "";
                string jobsdate = "";
                if (BE != "")
                {

                    string[] besplit = BE.Split(new string[] { receiveid }, StringSplitOptions.RemoveEmptyEntries);
                    besplit[1] = besplit[1].Remove(0, 1);
                    string jobnnn = splituptospace(besplit[1]);
                    besplit[1] = besplit[1].Remove(0, jobnnn.Length + 1);
                    jobsdate = splituptospace(besplit[1]);
                    jobs = jobnnn + jobsdate;
                    besplit[1] = besplit[1].Remove(0, jobsdate.Length + 1);

                    beno = splituptospace(besplit[1]);

                    besplit[1] = besplit[1].Remove(0, beno.Length + 1);
                    string bedate = splituptospace(besplit[1]);
                    besplit[1] = besplit[1].Remove(0, bedate.Length + 1);

                    string betype = splituptospace(besplit[1]);

                    besplit[1] = besplit[1].Remove(0, betype.Length + 1);

                    string ieccode = splituptospace(besplit[1]);

                    besplit[1] = besplit[1].Remove(0, ieccode.Length + 1);

                    string branchsrno = splituptospace(besplit[1]);

                    besplit[1] = besplit[1].Remove(0, branchsrno.Length + 1);

                    string impname = splituptospace(besplit[1]);
                    besplit[1] = besplit[1].Remove(0, impname.Length + 1);

                    string impaddress1 = splituptospace(besplit[1]);
                    besplit[1] = besplit[1].Remove(0, impaddress1.Length + 1);

                    string impaddress2 = splituptospace(besplit[1]);
                    besplit[1] = besplit[1].Remove(0, impaddress2.Length + 1);

                    string city = splituptospace(besplit[1]);
                    besplit[1] = besplit[1].Remove(0, city.Length + 1);

                    string state = splituptospace(besplit[1]);
                    besplit[1] = besplit[1].Remove(0, state.Length + 1);

                    string pin = splituptospace(besplit[1]);
                    besplit[1] = besplit[1].Remove(0, pin.Length + 1);

                    string clas = splituptospace(besplit[1]);
                    besplit[1] = besplit[1].Remove(0, clas.Length + 1);

                    string modetransfort = splituptospace(besplit[1]);
                    besplit[1] = besplit[1].Remove(0, modetransfort.Length + 1);

                    string imptype = splituptospace(besplit[1]);
                    besplit[1] = besplit[1].Remove(0, imptype.Length + 1);

                    string kbe = splituptospace(besplit[1]);
                    besplit[1] = besplit[1].Remove(0, kbe.Length + 1);

                    string seaflag = splituptospace(besplit[1]);
                    besplit[1] = besplit[1].Remove(0, seaflag.Length + 1);

                    string portorigin = splituptospace(besplit[1]);
                    besplit[1] = besplit[1].Remove(0, portorigin.Length + 1);

                    string spaces = splituptospace(besplit[1]);
                    besplit[1] = besplit[1].Remove(0, spaces.Length + 1);

                    cntyorigin = splituptospace(besplit[1]);
                    besplit[1] = besplit[1].Remove(0, cntyorigin.Length + 1);

                    string cntryconsignment = splituptospace(besplit[1]);
                    besplit[1] = besplit[1].Remove(0, cntryconsignment.Length + 1);

                    string Portship = splituptospace(besplit[1]);
                    besplit[1] = besplit[1].Remove(0, Portship.Length + 1);

                    string greenchanel = splituptospace(besplit[1]);
                    besplit[1] = besplit[1].Remove(0, greenchanel.Length + 1);

                    string sec48 = splituptospace(besplit[1]);
                    besplit[1] = besplit[1].Remove(0, sec48.Length + 1);

                    string priorbe = splituptospace(besplit[1]);
                    besplit[1] = besplit[1].Remove(0, priorbe.Length + 1);

                    string audealer = splituptospace(besplit[1]);
                    besplit[1] = besplit[1].Remove(0, audealer.Length + 1);

                    string firstcheck = splituptospace(besplit[1]);
                    besplit[1] = besplit[1].Remove(0, firstcheck.Length + 1);

                    string warehousecode = splituptospace(besplit[1]);
                    besplit[1] = besplit[1].Remove(0, warehousecode.Length + 1);

                    string warehousecodeid = splituptospace(besplit[1]);
                    besplit[1] = besplit[1].Remove(0, warehousecodeid.Length + 1);

                    string warehousebeno = splituptospace(besplit[1]);
                    besplit[1] = besplit[1].Remove(0, warehousebeno.Length + 1);

                    string warehousebedate = splituptospace(besplit[1]);
                    besplit[1] = besplit[1].Remove(0, warehousebedate.Length + 1);

                    string packreleased = splituptospace(besplit[1]);
                    besplit[1] = besplit[1].Remove(0, packreleased.Length + 1);

                    string packcode = splituptospace(besplit[1]);
                    besplit[1] = besplit[1].Remove(0, packcode.Length + 1);

                    string gweight = splituptospace(besplit[1]);
                    besplit[1] = besplit[1].Remove(0, gweight.Length + 1);

                    string unitmeasure = splituptospace(besplit[1]);
                    besplit[1] = besplit[1].Remove(0, unitmeasure.Length + 1);

                    string addlcharge = splituptospace(besplit[1]);
                    besplit[1] = besplit[1].Remove(0, addlcharge.Length + 1);

                    string miscload = splituptospace(besplit[1]);
                    besplit[1] = besplit[1].Remove(0, miscload.Length);



                    string query = "insert into tbl_BEDetails(JobNo,Jobdate,jobtime,Beno,BEDate,Betype,IecCode,BranchSrno,PortofOrigin,SeaFlag,kbe,ImpType,Transport,Class,ImporterName,Address1,Address2,city,state,pin,CountryofOrigin,CountryConsignment,GreenChanel,Section48,PriorBE,FirstCheck)" +
                        "values('" + jobno + "','" + jobsdate + "','" + jobtime + "','" + beno + "','" + bedate + "','" + betype + "','" + ieccode + "','" + branchsrno + "','" + portorigin + "','" + seaflag + "','" + kbe + "','" + imptype + "','" + modetransfort + "','" + clas + "'," +
                        "'" + impname + "','" + impaddress1 + "','" + impaddress2 + "','" + city + "','" + state + "','" + pin + "','" + cntyorigin + "','" + cntryconsignment + "','" + greenchanel + "','" + sec48 + "','" + priorbe + "','" + firstcheck + "')";

                    Excution(query);
                }
                if (EXCHANGE != "")
                {
                    //Exchange Details

                    string[] exchsplit = EXCHANGE.Split(new string[] { receiveid }, StringSplitOptions.RemoveEmptyEntries);
                    exchsplit[1] = exchsplit[1].Remove(0, 1);


                    string jobnos = splituptospace(exchsplit[1]);
                    exchsplit[1] = exchsplit[1].Remove(0, jobnos.Length + 1);
                    string jobdates = splituptospace(exchsplit[1]);
                    exchsplit[1] = exchsplit[1].Remove(0, jobdates.Length + 1);
                    string benos = splituptospace(exchsplit[1]);
                    exchsplit[1] = exchsplit[1].Remove(0, benos.Length + 1);
                    string bedates = splituptospace(exchsplit[1]);
                    exchsplit[1] = exchsplit[1].Remove(0, bedates.Length + 1);
                    string cur = splituptospace(exchsplit[1]);
                    exchsplit[1] = exchsplit[1].Remove(0, cur.Length + 1);
                    string standardcur = splituptospace(exchsplit[1]);
                    exchsplit[1] = exchsplit[1].Remove(0, standardcur.Length + 1);
                    string unit = splituptospace(exchsplit[1]);
                    exchsplit[1] = exchsplit[1].Remove(0, unit.Length + 1);
                    string rate = splituptospace(exchsplit[1]);
                    exchsplit[1] = exchsplit[1].Remove(0, rate.Length + 1);
                    string effectdate = splituptospace(exchsplit[1]);
                    exchsplit[1] = exchsplit[1].Remove(0, effectdate.Length + 1);
                    string bankname = splituptospace(exchsplit[1]);
                    exchsplit[1] = exchsplit[1].Remove(0, bankname.Length + 1);
                    string cernumber = splituptospace(exchsplit[1]);
                    exchsplit[1] = exchsplit[1].Remove(0, cernumber.Length + 1);
                    string cerdate = splituptospace(exchsplit[1]);
                    exchsplit[1] = exchsplit[1].Remove(0, cerdate.Length);


                    string query = "insert into tbl_BEExchangeDetails(JobNo,Jobdate,jobtime,Beno,Currency,standardCurrency,Unit,Rate,EffectiveDate)values('" + jobno + "','" + jobsdate + "','" + jobtime + "','" + beno + "','" + cur + "','" + standardcur + "','" + unit + "','" + rate + "','" + effectdate + "')";
                    Excution(query);
                }
                //Invoice
                string invs = "";
                if (INVOICE != "")
                {

                    string[] Invoice = INVOICE.Split(new string[] { receiveid }, StringSplitOptions.RemoveEmptyEntries);
                    int invcount = Invoice.Length;
                    invs = jobs + beno;
                    for (int i = 1; i < invcount; i++)
                    {
                        Invoice[i] = Invoice[i].Remove(0, 1);

                        string invoicedata = Invoice[1];
                        string jobnos = splituptospace(invoicedata);
                        invoicedata = invoicedata.Remove(0, jobnos.Length + 1);
                        string jobdates = splituptospace(invoicedata);
                        invoicedata = invoicedata.Remove(0, jobdates.Length + 1);
                        string benos = splituptospace(invoicedata);
                        invoicedata = invoicedata.Remove(0, benos.Length + 1);
                        string bedates = splituptospace(invoicedata);
                        invoicedata = invoicedata.Remove(0, bedates.Length + 1);
                        string invsrno = splituptospace(invoicedata);
                        invoicedata = invoicedata.Remove(0, invsrno.Length + 1);
                        string invdate = splituptospace(invoicedata);
                        invoicedata = invoicedata.Remove(0, invdate.Length + 1);
                        string porder = splituptospace(invoicedata);
                        invoicedata = invoicedata.Remove(0, porder.Length + 1);
                        string porderdate = splituptospace(invoicedata);
                        invoicedata = invoicedata.Remove(0, porderdate.Length + 1);
                        string contractnum = splituptospace(invoicedata);
                        invoicedata = invoicedata.Remove(0, contractnum.Length + 1);
                        string contractdate = splituptospace(invoicedata);
                        invoicedata = invoicedata.Remove(0, contractdate.Length + 1);
                        string LCNumber = splituptospace(invoicedata);
                        invoicedata = invoicedata.Remove(0, LCNumber.Length + 1);
                        string LCdate = splituptospace(invoicedata);
                        invoicedata = invoicedata.Remove(0, LCdate.Length + 1);
                        string svbref = splituptospace(invoicedata);
                        invoicedata = invoicedata.Remove(0, svbref.Length + 1);
                        string svbrefdate = splituptospace(invoicedata);
                        invoicedata = invoicedata.Remove(0, svbrefdate.Length + 1);
                        string svbrefloadassem = splituptospace(invoicedata);
                        invoicedata = invoicedata.Remove(0, svbrefloadassem.Length + 1);
                        string svbrefloadduty = splituptospace(invoicedata);
                        invoicedata = invoicedata.Remove(0, svbrefloadduty.Length + 1);
                        string svbflag = splituptospace(invoicedata);
                        invoicedata = invoicedata.Remove(0, svbflag.Length + 1);
                        string loadfinalass = splituptospace(invoicedata);
                        invoicedata = invoicedata.Remove(0, loadfinalass.Length + 1);
                        string loadfinalduty = splituptospace(invoicedata);
                        invoicedata = invoicedata.Remove(0, loadfinalduty.Length + 1);
                        string customhousecode = splituptospace(invoicedata);
                        invoicedata = invoicedata.Remove(0, customhousecode.Length + 1);

                        string suppname = splituptospace(invoicedata);
                        invoicedata = invoicedata.Remove(0, suppname.Length + 1);
                        string suppaddress1 = splituptospace(invoicedata);
                        invoicedata = invoicedata.Remove(0, suppaddress1.Length + 1);
                        string suppaddress2 = splituptospace(invoicedata);
                        invoicedata = invoicedata.Remove(0, suppaddress2.Length + 1);
                        string suppaddress3 = splituptospace(invoicedata);
                        invoicedata = invoicedata.Remove(0, suppaddress3.Length + 1);
                        string supcountry = splituptospace(invoicedata);
                        invoicedata = invoicedata.Remove(0, supcountry.Length + 1);

                        string PINZIP = splituptospace(invoicedata);
                        invoicedata = invoicedata.Remove(0, PINZIP.Length + 1);
                        string SellerName = splituptospace(invoicedata);
                        invoicedata = invoicedata.Remove(0, SellerName.Length + 1);
                        string saddress1 = splituptospace(invoicedata);
                        invoicedata = invoicedata.Remove(0, saddress1.Length + 1);
                        string saddress2 = splituptospace(invoicedata);
                        invoicedata = invoicedata.Remove(0, saddress2.Length + 1);
                        string saddress3 = splituptospace(invoicedata);
                        invoicedata = invoicedata.Remove(0, saddress3.Length + 1);
                        string scountry = splituptospace(invoicedata);
                        invoicedata = invoicedata.Remove(0, scountry.Length + 1);
                        string spin = splituptospace(invoicedata);
                        invoicedata = invoicedata.Remove(0, spin.Length + 1);
                        string Bname = splituptospace(invoicedata);
                        invoicedata = invoicedata.Remove(0, Bname.Length + 1);

                        string Baddress1 = splituptospace(invoicedata);
                        invoicedata = invoicedata.Remove(0, Baddress1.Length + 1);
                        string Baddress2 = splituptospace(invoicedata);
                        invoicedata = invoicedata.Remove(0, Baddress2.Length + 1);
                        string Baddress3 = splituptospace(invoicedata);
                        invoicedata = invoicedata.Remove(0, Baddress3.Length + 1);
                        string Bcntry = splituptospace(invoicedata);
                        invoicedata = invoicedata.Remove(0, Bcntry.Length + 1);
                        string Bpin = splituptospace(invoicedata);
                        invoicedata = invoicedata.Remove(0, Bpin.Length + 1);

                        string knownvale = "";

                        string invvalue = splituptospace(invoicedata);
                        invoicedata = invoicedata.Remove(0, invvalue.Length + 1);
                        string invterms = splituptospace(invoicedata);
                        invoicedata = invoicedata.Remove(0, invterms.Length + 1);

                        string invcur = splituptospace(invoicedata);
                        invoicedata = invoicedata.Remove(0, invcur.Length + 1);

                        string discount = splituptospace(invoicedata);
                        invoicedata = invoicedata.Remove(0, discount.Length + 1);
                        string Drate = splituptospace(invoicedata);
                        invoicedata = invoicedata.Remove(0, Drate.Length + 1);
                        string Damount = splituptospace(invoicedata);
                        invoicedata = invoicedata.Remove(0, Damount.Length + 1);
                        string HSSrate = splituptospace(invoicedata);
                        invoicedata = invoicedata.Remove(0, HSSrate.Length + 1);
                        string HSSAmount = splituptospace(invoicedata);
                        invoicedata = invoicedata.Remove(0, HSSAmount.Length + 1);
                        string FVALUE = splituptospace(invoicedata);
                        invoicedata = invoicedata.Remove(0, FVALUE.Length + 1);
                        string Fpercent = splituptospace(invoicedata);
                        invoicedata = invoicedata.Remove(0, Fpercent.Length + 1);
                        string fractual = splituptospace(invoicedata);
                        invoicedata = invoicedata.Remove(0, fractual.Length + 1);
                        string FCURR = splituptospace(invoicedata);
                        invoicedata = invoicedata.Remove(0, FCURR.Length + 1);
                        string insvalue = splituptospace(invoicedata);
                        invoicedata = invoicedata.Remove(0, insvalue.Length + 1);
                        string insrate = splituptospace(invoicedata);
                        invoicedata = invoicedata.Remove(0, insrate.Length + 1);
                        string inscur = splituptospace(invoicedata);
                        invoicedata = invoicedata.Remove(0, inscur.Length + 1);

                        string MISCCharge = splituptospace(invoicedata);
                        invoicedata = invoicedata.Remove(0, MISCCharge.Length + 1);

                        string MISCcur = splituptospace(invoicedata);
                        invoicedata = invoicedata.Remove(0, MISCcur.Length + 1);

                        string MISCpercent = splituptospace(invoicedata);
                        invoicedata = invoicedata.Remove(0, MISCpercent.Length + 1);
                        string landrate = splituptospace(invoicedata);
                        invoicedata = invoicedata.Remove(0, landrate.Length + 1);
                        string Loadcharge = splituptospace(invoicedata);
                        invoicedata = invoicedata.Remove(0, Loadcharge.Length + 1);
                        string Loadcur = splituptospace(invoicedata);
                        invoicedata = invoicedata.Remove(0, Loadcur.Length + 1);
                        string Loadrate = splituptospace(invoicedata);
                        invoicedata = invoicedata.Remove(0, Loadrate.Length + 1);
                        string Agencomm = splituptospace(invoicedata);
                        invoicedata = invoicedata.Remove(0, Agencomm.Length + 1);
                        string Agencommcur = splituptospace(invoicedata);
                        invoicedata = invoicedata.Remove(0, Agencommcur.Length + 1);

                        string Agencommrate = splituptospace(invoicedata);
                        invoicedata = invoicedata.Remove(0, Agencommrate.Length + 1);

                        string naturetransaction = splituptospace(invoicedata);
                        invoicedata = invoicedata.Remove(0, naturetransaction.Length + 1);
                        string payterms = splituptospace(invoicedata);
                        invoicedata = invoicedata.Remove(0, payterms.Length + 1);
                        string conattach1 = splituptospace(invoicedata);
                        invoicedata = invoicedata.Remove(0, conattach1.Length + 1);
                        string conattach2 = splituptospace(invoicedata);
                        invoicedata = invoicedata.Remove(0, conattach2.Length + 1);
                        string conattach3 = splituptospace(invoicedata);
                        invoicedata = invoicedata.Remove(0, conattach3.Length + 1);
                        string conattach4 = splituptospace(invoicedata);
                        invoicedata = invoicedata.Remove(0, conattach4.Length + 1);
                        string conattach5 = splituptospace(invoicedata);
                        invoicedata = invoicedata.Remove(0, conattach5.Length + 1);

                        string valuationmethod = splituptospace(invoicedata);
                        invoicedata = invoicedata.Remove(0, valuationmethod.Length + 1);
                        string invno = invoicedata;
                        string query = "insert into tbl_BEInvoiceDetails(JobNo,Jobdate,jobtime,Beno,InvSrno,InvDate,SupplierName,Address1,Address2,Address3,Country,UnknownValue,InvValue,Invterms,Invcurrency,naturetransaction,PaymentTerms,ValuationMethod,InvNo)" +
                            "values('" + jobno + "','" + jobsdate + "','" + jobtime + "','" + beno + "','" + invsrno + "','" + invdate + "','" + suppname + "','" + suppaddress1 + "','" + suppaddress2 + "','" + suppaddress3 + "','" + supcountry + "','" + knownvale + "','" + invvalue + "','" + invterms + "','" + invcur + "','" + naturetransaction + "','" + payterms + "','" + valuationmethod + "','" + invno + "')";
                        Excution(query);
                    }
                }
                //ITEM
                if (ITEMS != "")
                {

                    string[] itemsplit = ITEMS.Split(new string[] { receiveid }, StringSplitOptions.RemoveEmptyEntries);
                    int itemcount = itemsplit.Length;
                    for (int i = 1; i < itemcount - 1; i++)
                    {
                        itemsplit[i] = itemsplit[i].Remove(0, 1);
                        string itemdatadet = itemsplit[i];

                        string jobnos = splituptospace(itemdatadet);
                        itemdatadet = itemdatadet.Remove(0, jobnos.Length + 1);

                        string jobdates = splituptospace(itemdatadet);
                        itemdatadet = itemdatadet.Remove(0, jobdates.Length + 1);

                        string benos = splituptospace(itemdatadet);
                        itemdatadet = itemdatadet.Remove(0, benos.Length + 1);

                        string bedates = splituptospace(itemdatadet);
                        itemdatadet = itemdatadet.Remove(0, bedates.Length + 1);

                        string invoceitemsrno = splituptospace(itemdatadet);
                        itemdatadet = itemdatadet.Remove(0, invoceitemsrno.Length + 1);

                        string itemsrno = splituptospace(itemdatadet);
                        itemdatadet = itemdatadet.Remove(0, itemsrno.Length + 1);

                        string qty = splituptospace(itemdatadet);
                        itemdatadet = itemdatadet.Remove(0, qty.Length + 1);

                        string measure = splituptospace(itemdatadet);
                        itemdatadet = itemdatadet.Remove(0, measure.Length + 1);

                        string ritc = splituptospace(itemdatadet);
                        itemdatadet = itemdatadet.Remove(0, ritc.Length + 1);

                        string Item1 = splituptospace(itemdatadet);
                        itemdatadet = itemdatadet.Remove(0, Item1.Length + 1);

                        string Item2 = splituptospace(itemdatadet);
                        itemdatadet = itemdatadet.Remove(0, Item2.Length + 1);

                        string Itemcat = splituptospace(itemdatadet);
                        itemdatadet = itemdatadet.Remove(0, Itemcat.Length + 1);

                        string genericdesc = splituptospace(itemdatadet);
                        itemdatadet = itemdatadet.Remove(0, genericdesc.Length + 1);

                        string accitem = splituptospace(itemdatadet);
                        itemdatadet = itemdatadet.Remove(0, accitem.Length + 1);

                        string manname = splituptospace(itemdatadet);
                        itemdatadet = itemdatadet.Remove(0, manname.Length + 1);

                        string brandname = splituptospace(itemdatadet);
                        itemdatadet = itemdatadet.Remove(0, brandname.Length + 1);

                        string model = splituptospace(itemdatadet);
                        itemdatadet = itemdatadet.Remove(0, model.Length + 1);

                        string enduse = splituptospace(itemdatadet);
                        itemdatadet = itemdatadet.Remove(0, enduse.Length + 1);

                        string cntryorigins = splituptospace(itemdatadet);
                        itemdatadet = itemdatadet.Remove(0, cntryorigins.Length + 1);

                        string cth = splituptospace(itemdatadet);
                        itemdatadet = itemdatadet.Remove(0, cth.Length + 1);

                        string prefer = splituptospace(itemdatadet);
                        itemdatadet = itemdatadet.Remove(0, prefer.Length + 1);

                        string ceth = splituptospace(itemdatadet);
                        itemdatadet = itemdatadet.Remove(0, ceth.Length + 1);

                        string bcdnotifi = splituptospace(itemdatadet);
                        itemdatadet = itemdatadet.Remove(0, bcdnotifi.Length + 1);

                        string bcdnotifisrno = splituptospace(itemdatadet);
                        itemdatadet = itemdatadet.Remove(0, bcdnotifisrno.Length + 1);

                        string cvddnotifi = splituptospace(itemdatadet);
                        itemdatadet = itemdatadet.Remove(0, cvddnotifi.Length + 1);

                        string cvdnotifisrno = splituptospace(itemdatadet);
                        itemdatadet = itemdatadet.Remove(0, cvdnotifisrno.Length + 1);

                        string add1notifi = splituptospace(itemdatadet);
                        itemdatadet = itemdatadet.Remove(0, add1notifi.Length + 1);

                        string add1srno = splituptospace(itemdatadet);
                        itemdatadet = itemdatadet.Remove(0, add1srno.Length + 1);

                        string add2notifi = splituptospace(itemdatadet);
                        itemdatadet = itemdatadet.Remove(0, add2notifi.Length + 1);

                        string add2srno = splituptospace(itemdatadet);
                        itemdatadet = itemdatadet.Remove(0, add2srno.Length + 1);

                        string othernotify = splituptospace(itemdatadet);
                        itemdatadet = itemdatadet.Remove(0, othernotify.Length + 1);

                        string othernotifysrno = splituptospace(itemdatadet);
                        itemdatadet = itemdatadet.Remove(0, othernotifysrno.Length + 1);

                        string sadnotify = splituptospace(itemdatadet);
                        itemdatadet = itemdatadet.Remove(0, sadnotify.Length + 1);

                        string sadsrno = splituptospace(itemdatadet);
                        itemdatadet = itemdatadet.Remove(0, sadsrno.Length + 1);

                        string ncdnotify = splituptospace(itemdatadet);
                        itemdatadet = itemdatadet.Remove(0, ncdnotify.Length + 1);

                        string ncdsrno = splituptospace(itemdatadet);
                        itemdatadet = itemdatadet.Remove(0, ncdsrno.Length + 1);

                        string antidump = splituptospace(itemdatadet);
                        itemdatadet = itemdatadet.Remove(0, antidump.Length + 1);

                        string antidumpsrno = splituptospace(itemdatadet);
                        itemdatadet = itemdatadet.Remove(0, antidumpsrno.Length + 1);

                        string cthsrno = splituptospace(itemdatadet);
                        itemdatadet = itemdatadet.Remove(0, cthsrno.Length + 1);

                        string supserial = splituptospace(itemdatadet);
                        itemdatadet = itemdatadet.Remove(0, supserial.Length + 1);

                        string qtyanti = splituptospace(itemdatadet);
                        itemdatadet = itemdatadet.Remove(0, qtyanti.Length + 1);

                        string tarrifnotn = splituptospace(itemdatadet);
                        itemdatadet = itemdatadet.Remove(0, tarrifnotn.Length + 1);

                        string tarrifsrno = splituptospace(itemdatadet);
                        itemdatadet = itemdatadet.Remove(0, tarrifsrno.Length + 1);

                        string tarrifqty = splituptospace(itemdatadet);
                        itemdatadet = itemdatadet.Remove(0, tarrifqty.Length + 1);

                        string sapta = splituptospace(itemdatadet);
                        itemdatadet = itemdatadet.Remove(0, tarrifqty.Length + 1);

                        string saptasrno = splituptospace(itemdatadet);
                        itemdatadet = itemdatadet.Remove(0, saptasrno.Length + 1);

                        string healthnotn = splituptospace(itemdatadet);
                        itemdatadet = itemdatadet.Remove(0, healthnotn.Length + 1);

                        string healthsrno = splituptospace(itemdatadet);
                        itemdatadet = itemdatadet.Remove(0, healthsrno.Length + 1);

                        string addcvdnotn = splituptospace(itemdatadet);
                        itemdatadet = itemdatadet.Remove(0, addcvdnotn.Length + 1);

                        string addcvdnotnsrno = splituptospace(itemdatadet);
                        itemdatadet = itemdatadet.Remove(0, addcvdnotnsrno.Length + 1);

                        string aggduty = splituptospace(itemdatadet);
                        itemdatadet = itemdatadet.Remove(0, aggduty.Length + 1);

                        string aggdutysrno = splituptospace(itemdatadet);
                        itemdatadet = itemdatadet.Remove(0, aggdutysrno.Length + 1);

                        string safeduty = splituptospace(itemdatadet);
                        itemdatadet = itemdatadet.Remove(0, safeduty.Length + 1);

                        string safedutysrno = splituptospace(itemdatadet);
                        itemdatadet = itemdatadet.Remove(0, safedutysrno.Length + 1);

                        string unitprice = splituptospace(itemdatadet);
                        itemdatadet = itemdatadet.Remove(0, unitprice.Length + 1);

                        string disrate = splituptospace(itemdatadet);
                        itemdatadet = itemdatadet.Remove(0, disrate.Length + 1);

                        string disamount = splituptospace(itemdatadet);
                        itemdatadet = itemdatadet.Remove(0, disamount.Length + 1);

                        string cthasperqty = splituptospace(itemdatadet);
                        itemdatadet = itemdatadet.Remove(0, cthasperqty.Length + 1);

                        string cthasperqty2 = splituptospace(itemdatadet);
                        itemdatadet = itemdatadet.Remove(0, cthasperqty2.Length + 1);

                        string svbrefno = splituptospace(itemdatadet);
                        itemdatadet = itemdatadet.Remove(0, svbrefno.Length + 1);

                        string svbrefdate = splituptospace(itemdatadet);
                        itemdatadet = itemdatadet.Remove(0, svbrefdate.Length + 1);

                        string svbloadassessable = splituptospace(itemdatadet);
                        itemdatadet = itemdatadet.Remove(0, svbloadassessable.Length + 1);

                        string svbloadduty = splituptospace(itemdatadet);
                        itemdatadet = itemdatadet.Remove(0, svbloadduty.Length + 1);

                        string svbflag = splituptospace(itemdatadet);
                        itemdatadet = itemdatadet.Remove(0, svbflag.Length + 1);

                        string finalprofass = splituptospace(itemdatadet);
                        itemdatadet = itemdatadet.Remove(0, finalprofass.Length + 1);

                        string finalprofduty = splituptospace(itemdatadet);
                        itemdatadet = itemdatadet.Remove(0, finalprofduty.Length + 1);

                        string customcode = splituptospace(itemdatadet);
                        itemdatadet = itemdatadet.Remove(0, customcode.Length + 1);

                        string policyparano = splituptospace(itemdatadet);
                        itemdatadet = itemdatadet.Remove(0, policyparano.Length + 1);

                        string policyyear = splituptospace(itemdatadet);
                        itemdatadet = itemdatadet.Remove(0, policyyear.Length + 1);

                        string rspdeclared = splituptospace(itemdatadet);
                        itemdatadet = itemdatadet.Remove(0, rspdeclared.Length + 1);

                        string reimport = splituptospace(itemdatadet);
                        itemdatadet = itemdatadet.Remove(0, reimport.Length + 1);

                        string prebeno = splituptospace(itemdatadet);
                        itemdatadet = itemdatadet.Remove(0, prebeno.Length + 1);

                        string prebedate = splituptospace(itemdatadet);
                        itemdatadet = itemdatadet.Remove(0, prebedate.Length + 1);

                        string preunitprice = splituptospace(itemdatadet);
                        itemdatadet = itemdatadet.Remove(0, preunitprice.Length + 1);

                        string precurrency = splituptospace(itemdatadet);
                        itemdatadet = itemdatadet.Remove(0, precurrency.Length + 1);

                        string precustomsite = splituptospace(itemdatadet);
                        itemdatadet = itemdatadet.Remove(0, precustomsite.Length);

                        string query = "insert into tbl_BEItemDetails(JobNo,Jobdate,jobtime,Beno,InvoiceItemSrNo,Quantity,Measure,RITC,ITEM1,ITEM2,ItemCategory,GenericDesc,AccessoriesforItem,CTH,Prefer,CETH,CVD,InvoicePrice,RSP,Reimport)" +
                            "values('" + jobno + "','" + jobsdate + "','" + jobtime + "','" + beno + "','" + invoceitemsrno + "','" + qty + "','" + measure + "','" + ritc + "','" + Item1 + "','" + Item2 + "','" + Itemcat + "','" + genericdesc + "','" + accitem + "','" + cth + "','" + prefer + "','" + ceth + "','" + cvddnotifi + "','" + unitprice + "','" + rspdeclared + "','" + reimport + "')";
                        Excution(query);
                    }
                }
                //IGM
                string igmno = "";
                string igmdate = "";
                if (IGMS != "")
                {

                    string[] Igmsplit = IGMS.Split(new string[] { receiveid }, StringSplitOptions.RemoveEmptyEntries);
                    Igmsplit[1] = Igmsplit[1].Remove(0, 1);

                    string jobnos = splituptospace(Igmsplit[1]);
                    Igmsplit[1] = Igmsplit[1].Remove(0, jobnos.Length + 1);

                    string jobdates = splituptospace(Igmsplit[1]);
                    Igmsplit[1] = Igmsplit[1].Remove(0, jobdates.Length + 1);

                    string benos = splituptospace(Igmsplit[1]);
                    Igmsplit[1] = Igmsplit[1].Remove(0, benos.Length + 1);

                    string bedates = splituptospace(Igmsplit[1]);

                    Igmsplit[1] = Igmsplit[1].Remove(0, bedates.Length + 1);

                    igmno = splituptospace(Igmsplit[1]);
                    Igmsplit[1] = Igmsplit[1].Remove(0, igmno.Length + 1);

                    igmdate = splituptospace(Igmsplit[1]);
                    Igmsplit[1] = Igmsplit[1].Remove(0, igmdate.Length + 1);

                    string inwarddate = splituptospace(Igmsplit[1]);
                    Igmsplit[1] = Igmsplit[1].Remove(0, inwarddate.Length + 1);

                    string gigmno = splituptospace(Igmsplit[1]);
                    Igmsplit[1] = Igmsplit[1].Remove(0, gigmno.Length + 1);

                    string gigmdate = splituptospace(Igmsplit[1]);
                    Igmsplit[1] = Igmsplit[1].Remove(0, gigmdate.Length + 1);

                    string portreport = splituptospace(Igmsplit[1]);
                    Igmsplit[1] = Igmsplit[1].Remove(0, portreport.Length + 1);


                    string mawblno = splituptospace(Igmsplit[1]);
                    Igmsplit[1] = Igmsplit[1].Remove(0, mawblno.Length + 1);

                    string mawbdate = splituptospace(Igmsplit[1]);
                    Igmsplit[1] = Igmsplit[1].Remove(0, mawbdate.Length + 1);

                    string hawblno = splituptospace(Igmsplit[1]);
                    Igmsplit[1] = Igmsplit[1].Remove(0, hawblno.Length + 1);

                    string hawbdate = splituptospace(Igmsplit[1]);
                    Igmsplit[1] = Igmsplit[1].Remove(0, hawbdate.Length + 1);

                    string npkgs = splituptospace(Igmsplit[1]);
                    Igmsplit[1] = Igmsplit[1].Remove(0, npkgs.Length + 1);

                    string gweight = splituptospace(Igmsplit[1]);
                    Igmsplit[1] = Igmsplit[1].Remove(0, gweight.Length + 1);

                    string unitcode = splituptospace(Igmsplit[1]);
                    Igmsplit[1] = Igmsplit[1].Remove(0, unitcode.Length + 1);

                    string packcode = splituptospace(Igmsplit[1]);
                    Igmsplit[1] = Igmsplit[1].Remove(0, packcode.Length + 1);

                    string markaddress1 = splituptospace(Igmsplit[1]);
                    Igmsplit[1] = Igmsplit[1].Remove(0, markaddress1.Length + 1);

                    string markaddress2 = splituptospace(Igmsplit[1]);
                    Igmsplit[1] = Igmsplit[1].Remove(0, markaddress2.Length + 1);

                    string markaddress3 = splituptospace(Igmsplit[1]);
                    Igmsplit[1] = Igmsplit[1].Remove(0, markaddress3.Length);

                    string query = "insert into tbl_BEIGMDetails(JobNo,Jobdate,jobtime,Beno,IGMNo,IGMDate,InwardDate,MAWBLNo,MAWBDate,HAWBNo,HAWBDate,NoofPackages,GrossWeight,UnitCode,PackCode,MarkAddress)" +
                        "values('" + jobno + "','" + jobsdate + "','" + jobtime + "','" + beno + "','" + igmno + "','" + igmdate + "','" + inwarddate + "','" + mawblno + "','" + mawbdate + "','" + hawblno + "','" + hawbdate + "','" + npkgs + "','" + gweight + "','" + unitcode + "','" + packcode + "','" + markaddress1 + "')";
                    Excution(query);
                }
                //container
                if (CONTAINER != "")
                {

                    string[] container = CONTAINER.Split(new string[] { receiveid  }, StringSplitOptions.RemoveEmptyEntries);

                    container[1] = container[1].Remove(0, 1);

                    string jobnos = splituptospace(container[1]);
                    container[1] = container[1].Remove(0, jobnos.Length + 1);
                    string jobdates = splituptospace(container[1]);
                    container[1] = container[1].Remove(0, jobdates.Length + 1);
                    string benos = splituptospace(container[1]);
                    container[1] = container[1].Remove(0, benos.Length + 1);
                    string bedates = splituptospace(container[1]);

                    container[1] = container[1].Remove(0, bedates.Length + 1);
                    string Igmno = splituptospace(container[1]);

                    container[1] = container[1].Remove(0, Igmno.Length + 1);
                    string igmdates = splituptospace(container[1]);

                    container[1] = container[1].Remove(0, igmdates.Length + 1);

                    string lclfcl = splituptospace(container[1]);
                    container[1] = container[1].Remove(0, lclfcl.Length + 1);
                    string containeno = splituptospace(container[1]);
                    container[1] = container[1].Remove(0, containeno.Length + 1);
                    string sealno = splituptospace(container[1]);
                    container[1] = container[1].Remove(0, sealno.Length);
                    string query = "insert into tbl_BEContainerDetails(JobNo,Jobdate,jobtime,Beno,ContainerNo,LCLFCL)values('" + jobno + "','" + jobsdate + "','" + jobtime + "','" + beno + "','" + containeno + "','" + lclfcl + "')";
                    Excution(query);
                }
                Session["JobNoBE"] = jobno;
                JobBERead();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Data has been Imported Successfully');", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('This File has been already uploaded');", true);
            }
        }

        private void JobBERead()
        {
            DataSet ds = new DataSet();
            SqlConnection sqlcon = new SqlConnection(strconn);
            string query = "select distinct * from View_JobCreationBERead where JobNo ='" + (string)Session["JobNoBE"] + "'";
            SqlDataAdapter da = new SqlDataAdapter(query, sqlcon);
            da.Fill(ds, "Jobdetails");
            sqlcon.Close();
            if (ds.Tables["Jobdetails"].Rows.Count != 0)
            {
                DataRowView row = ds.Tables["Jobdetails"].DefaultView[0];
                txtJobNo.Text = row["Jobno"].ToString();
                string JobDate = row["JobDate"].ToString();
                if (JobDate != "")
                {
                    DateTime jobd = Convert.ToDateTime(JobDate);
                    txtJobDate.Text = jobd.ToString("dd/MM/yyyy");
                }
                txtInvoiceNo.Text = row["Invno"].ToString();
                string InvDate = row["InvDate"].ToString();
                if (InvDate != "")
                {
                    DateTime InvoiceDate = Convert.ToDateTime(InvDate);
                    txtInvoiceDate.Text = InvoiceDate.ToString("dd/MM/yyyy");
                }
                txtImpExpName.Text = row["ImporterName"].ToString();
                txtImpExpAddress.Text = row["Address1"].ToString();
                string transport = row["Transport"].ToString();
                if (transport == "S")
                {
                    ddlShipmentType.SelectedValue = "Sea";
                    ddlModeOfShipment.Enabled = true;
                }
                else if (transport == "A")
                {
                    ddlShipmentType.SelectedValue = "Air";
                    ddlModeOfShipment.Enabled = false;
                }
                string shipmentmode = row["LCLFCL"].ToString();
                if (shipmentmode == "L")
                {
                    ddlModeOfShipment.SelectedValue = "LCL";
                }
                else if (shipmentmode == "F")
                {
                    ddlModeOfShipment.SelectedValue = "FCL";
                }
                txtPortOfOrigin.Text = row["PortofOrigin"].ToString();
                txtImpExpBranchCode.Text = row["BranchSrno"].ToString();
            }
        }

        protected void btnRead_Click(object sender, EventArgs e)
        {
            string filenam = FileUpload1.PostedFile.FileName;
            string[] file = filenam.Split(new string[] { "." }, StringSplitOptions.None);
            string filetype = file[1];
            if (filetype == "sb")
            {
                Session["JobType"] = "EXP";
                //lblTypeofJob.Text = "Export";
                //lblName.Text = "Supplier Name";
                //lblAddress.Text = "Supplier Address";
                SBFileRead();
                //btnSave_Click(sender, e);
                //btnView_Click(sender, e);
                btnSave.Visible = true;
                btnUpdate.Visible = false;
            }
            else if (filetype == "be")
            {
                Session["JobType"] = "IMP";
                //lblTypeofJob.Text = "Import";
                BEFileRead();
                //btnSave_Click(sender, e);
               // btnView_Click(sender, e);
                btnSave.Visible = true;
                btnUpdate.Visible = false;
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Please Read the Correct file');", true);
            }
        }

        private void SBFileRead()
        {
            String line = "";
            SqlConnection con = new SqlConnection(strconn);
            con.Open();
            string queryde = "select ChaLicense,Chaidair,Chaidsea from tbl_details ";
            DataSet ds = new DataSet();
            SqlDataAdapter sd = new SqlDataAdapter(queryde, con);
            sd.Fill(ds, "data");
            con.Close();
            if (ds.Tables["data"].Rows.Count != 0)
            {
                DataRowView row = ds.Tables["data"].DefaultView[0];
                chalicences = row["ChaLicense"].ToString();
                customidair = row["Chaidair"].ToString();
                customidsea = row["Chaidsea"].ToString();
            }

            string FilePath = "";

            string filename = System.IO.Path.GetFullPath(FileUpload1.PostedFile.FileName);
            if (filename != "")
            {
                string path = "";
                if (FileUpload1.HasFile)
                {
                    string paths = AppDomain.CurrentDomain.BaseDirectory;
                    string pathdir = Path.Combine(paths, @"TempFile\");
                    path = pathdir + Path.GetFileName(FileUpload1.PostedFile.FileName);
                    string[] filePaths = Directory.GetFiles(@pathdir);

                    foreach (string filePath in filePaths)
                    {
                        if (!CheckIfFileIsBeingUsed(filePath))
                        {
                            File.Delete(filePath);
                        }
                        // The file is not locked
                    }
                    FileUpload1.SaveAs(pathdir + Path.GetFileName(FileUpload1.PostedFile.FileName));
                    FilePath += path;
                }
                using (StreamReader sr = new StreamReader(FilePath))
                {
                    line = sr.ReadToEnd();
                    Console.WriteLine(line);
                }
            }
            string[] a = line.Split(new string[] { "<TABLE>" }, StringSplitOptions.RemoveEmptyEntries);
            string head = a[0].TrimEnd('\n');

            string sb = a[1].TrimEnd('\n');
            string sbdata = sb.Remove(0, 3);

            string invoice = a[2].TrimEnd('\n');
            string invoicedata = invoice.Remove(0, 8);

            string EXCHANGE = a[3].TrimEnd('\n');
            string exchdata = EXCHANGE.Remove(0, 9);

            string ITEM = a[4].TrimEnd('\n');
            string itemdata = ITEM.Remove(0, 5);

            string[] headers = head.Split(new string[] { "ZZ" }, StringSplitOptions.RemoveEmptyEntries);
            string receiveid = headers[2].Substring(0, 7);
            string versionno = headers[2].Substring(7, 9);
            string msgid = headers[2].Substring(17, 9);
            string jobno = headers[2].Substring(27, 4);
            string jobdate = headers[2].Substring(32, 8);
            string jobtime = "";


            DataSet ds1 = new DataSet();
            SqlConnection sqlcon = new SqlConnection(strconn);
            string query = "select * from tbl_SBDetails where JobNo ='" + jobno + "'";
            SqlDataAdapter da = new SqlDataAdapter(query, sqlcon);
            da.Fill(ds1, "Jobdetails");
            sqlcon.Close();
            if (ds1.Tables["Jobdetails"].Rows.Count == 0)
            {

                sbdata = sbdata.Remove(0, 1);
                string msgtypesb = splituptospace(sbdata);
                sbdata = sbdata.Remove(0, msgtypesb.Length + 1);

                string customhousecode = splituptospace(sbdata);
                sbdata = sbdata.Remove(0, customhousecode.Length + 1);

                string jobnosb = splituptospace(sbdata);
                sbdata = sbdata.Remove(0, jobnosb.Length + 1);

                string jobnosbdate = splituptospace(sbdata);
                sbdata = sbdata.Remove(0, jobnosbdate.Length + 1);

                string sbno = splituptospace(sbdata);
                sbdata = sbdata.Remove(0, sbno.Length + 1);

                string sbdate = splituptospace(sbdata);
                sbdata = sbdata.Remove(0, sbdate.Length + 1);

                string chalicence = splituptospace(sbdata);
                sbdata = sbdata.Remove(0, chalicence.Length + 1);

                string impcode = splituptospace(sbdata);
                sbdata = sbdata.Remove(0, impcode.Length + 1);

                string impbranch = splituptospace(sbdata);
                sbdata = sbdata.Remove(0, impbranch.Length + 1);

                string impname = splituptospace(sbdata);
                sbdata = sbdata.Remove(0, impname.Length + 1);

                string impadd1 = splituptospace(sbdata);
                sbdata = sbdata.Remove(0, impadd1.Length + 1);

                string impadd2 = splituptospace(sbdata);
                sbdata = sbdata.Remove(0, impadd2.Length + 1);

                string impaddcity = splituptospace(sbdata);
                sbdata = sbdata.Remove(0, impaddcity.Length + 1);

                string impaddstate = splituptospace(sbdata);
                sbdata = sbdata.Remove(0, impaddstate.Length + 1);

                string impaddpin = splituptospace(sbdata);
                sbdata = sbdata.Remove(0, impaddpin.Length + 1);

                string type = splituptospace(sbdata);
                sbdata = sbdata.Remove(0, type.Length + 1);

                string classs = splituptospace(sbdata);
                sbdata = sbdata.Remove(0, classs.Length + 1);

                string stateorigincode = splituptospace(sbdata);
                sbdata = sbdata.Remove(0, stateorigincode.Length + 1);

                string dealercode = splituptospace(sbdata);
                sbdata = sbdata.Remove(0, dealercode.Length + 1);

                string Epzcode = splituptospace(sbdata);
                sbdata = sbdata.Remove(0, Epzcode.Length + 1);

                string consgneename = splituptospace(sbdata);
                sbdata = sbdata.Remove(0, consgneename.Length + 1);

                string consgneeadd1 = splituptospace(sbdata);
                sbdata = sbdata.Remove(0, consgneeadd1.Length + 1);

                string consgneeadd2 = splituptospace(sbdata);
                sbdata = sbdata.Remove(0, consgneeadd2.Length + 1);

                string consgneeadd3 = splituptospace(sbdata);
                sbdata = sbdata.Remove(0, consgneeadd3.Length + 1);

                string consgneeadd4 = splituptospace(sbdata);
                sbdata = sbdata.Remove(0, consgneeadd4.Length + 1);

                string consgneecntry = splituptospace(sbdata);
                sbdata = sbdata.Remove(0, consgneecntry.Length + 1);

                string NFEI = splituptospace(sbdata);
                sbdata = sbdata.Remove(0, NFEI.Length + 1);

                string RBInum = splituptospace(sbdata);
                sbdata = sbdata.Remove(0, RBInum.Length + 1);

                string RBIdate = splituptospace(sbdata);
                sbdata = sbdata.Remove(0, RBIdate.Length + 1);

                string removes = splituptospace(sbdata);
                sbdata = sbdata.Remove(0, removes.Length + 1);

                string POL = splituptospace(sbdata);
                sbdata = sbdata.Remove(0, POL.Length + 1);

                string POD = splituptospace(sbdata);
                sbdata = sbdata.Remove(0, POD.Length + 1);

                string cntrydescharge = splituptospace(sbdata);
                sbdata = sbdata.Remove(0, cntrydescharge.Length + 1);

                string portdescharge = splituptospace(sbdata);
                sbdata = sbdata.Remove(0, portdescharge.Length + 1);

                string sealtype = splituptospace(sbdata);
                sbdata = sbdata.Remove(0, sealtype.Length + 1);

                string naturecargo = splituptospace(sbdata);
                sbdata = sbdata.Remove(0, naturecargo.Length + 1);

                string Gweight = splituptospace(sbdata);
                sbdata = sbdata.Remove(0, Gweight.Length + 1);

                string Netweight = splituptospace(sbdata);
                sbdata = sbdata.Remove(0, Netweight.Length + 1);

                string Unitmeasure = splituptospace(sbdata);
                sbdata = sbdata.Remove(0, Unitmeasure.Length + 1);

                string totalpkgs = splituptospace(sbdata);
                sbdata = sbdata.Remove(0, totalpkgs.Length + 1);

                string marksnum = splituptospace(sbdata);
                sbdata = sbdata.Remove(0, marksnum.Length + 1);

                string loosepack = splituptospace(sbdata);
                sbdata = sbdata.Remove(0, loosepack.Length + 1);

                string noofcnt = splituptospace(sbdata);
                sbdata = sbdata.Remove(0, noofcnt.Length + 1);

                string MAWB = splituptospace(sbdata);
                sbdata = sbdata.Remove(0, MAWB.Length + 1);

                string HAWB = splituptospace(sbdata);
                sbdata = sbdata.Remove(0, HAWB.Length + 1);

                string amentype = splituptospace(sbdata);
                sbdata = sbdata.Remove(0, amentype.Length + 1);

                string amenno = splituptospace(sbdata);
                sbdata = sbdata.Remove(0, amenno.Length + 1);

                string amendate = splituptospace(sbdata);
                sbdata = sbdata.Remove(0, amendate.Length);

                //Sb Data
                string querysb = "insert into tbl_SBDetails(Jobno,JobDate,JobTime,SbNo,SbDate,ChalicenceNumber,Importerexportercode,Codebranch,ImpName,Address1,Address2,City,State,Pin,Type,Class,Stateorigincode,BankCode,EpZCode,Consigneename,Caddress1,Caddress2,Caddress3,Caddress4,ConsigneeCountrycode,NFEI,RBIDate,POL,POD,CountryDischarge,PortDischarge,SealType,NatureCargo,Gweight,NetWeight,UnitMeasure,TotalPkgs,MarksNumber,LoosePacking,NoOfContainer,MAWBNO,HAWBNO,AmentType,AmentNo,AmentDate)values(@Jobno,@JobDate,@JobTime,@SbNo,@SbDate,@ChalicenceNumber,@Importerexportercode,@Codebranch,@ImpName,@Address1,@Address2,@City,@State,@Pin,@Type,@Class,@Stateorigincode,@BankCode,@EpZCode,@Consigneename,@Caddress1,@Caddress2,@Caddress3,@Caddress4,@ConsigneeCountrycode,@NFEI,@RBIDate,@POL,@POD,@CountryDischarge,@PortDischarge,@SealType,@NatureCargo,@Gweight,@NetWeight,@UnitMeasure,@TotalPkgs,@MarksNumber,@LoosePacking,@NoOfContainer,@MAWBNO,@HAWBNO,@AmentType,@AmentNo,@AmentDate)";
                SqlConnection consb = new SqlConnection(strconn);
                consb.Open();
                SqlCommand cmdsb = new SqlCommand(querysb, consb);
                cmdsb.Parameters.AddWithValue("@Jobno", jobnosb);
                cmdsb.Parameters.AddWithValue("@JobDate", jobnosbdate);
                cmdsb.Parameters.AddWithValue("@JobTime", jobtime);
                cmdsb.Parameters.AddWithValue("@SbNo", sbno);
                cmdsb.Parameters.AddWithValue("@SbDate", sbdate);
                cmdsb.Parameters.AddWithValue("@ChalicenceNumber", chalicence);
                cmdsb.Parameters.AddWithValue("@Importerexportercode", impcode);
                cmdsb.Parameters.AddWithValue("@Codebranch", impbranch);
                cmdsb.Parameters.AddWithValue("@ImpName", impname);
                cmdsb.Parameters.AddWithValue("@Address1", impadd1);
                cmdsb.Parameters.AddWithValue("@Address2", impadd2);
                cmdsb.Parameters.AddWithValue("@City", impaddcity);
                cmdsb.Parameters.AddWithValue("@State", impaddstate);
                cmdsb.Parameters.AddWithValue("@Pin", impaddpin);
                cmdsb.Parameters.AddWithValue("@Type", type);
                cmdsb.Parameters.AddWithValue("@Class", classs);
                cmdsb.Parameters.AddWithValue("@Stateorigincode", stateorigincode);
                cmdsb.Parameters.AddWithValue("@BankCode", dealercode);
                cmdsb.Parameters.AddWithValue("@EpZCode", Epzcode);
                cmdsb.Parameters.AddWithValue("@Consigneename", consgneename);
                cmdsb.Parameters.AddWithValue("@Caddress1", consgneeadd1);
                cmdsb.Parameters.AddWithValue("@Caddress2", consgneeadd2);
                cmdsb.Parameters.AddWithValue("@Caddress3", consgneeadd3);
                cmdsb.Parameters.AddWithValue("@Caddress4", consgneeadd4);
                cmdsb.Parameters.AddWithValue("@ConsigneeCountrycode", consgneecntry);
                cmdsb.Parameters.AddWithValue("@NFEI", NFEI);
                cmdsb.Parameters.AddWithValue("@RBIDate", RBIdate);

                cmdsb.Parameters.AddWithValue("@POL", POL);
                cmdsb.Parameters.AddWithValue("@POD", POD);
                cmdsb.Parameters.AddWithValue("@CountryDischarge", cntrydescharge);
                cmdsb.Parameters.AddWithValue("@PortDischarge", portdescharge);
                cmdsb.Parameters.AddWithValue("@SealType", sealtype);
                cmdsb.Parameters.AddWithValue("@NatureCargo", naturecargo);
                cmdsb.Parameters.AddWithValue("@Gweight", Gweight);
                cmdsb.Parameters.AddWithValue("@NetWeight", Netweight);
                cmdsb.Parameters.AddWithValue("@UnitMeasure", Unitmeasure);
                cmdsb.Parameters.AddWithValue("@TotalPkgs", totalpkgs);
                cmdsb.Parameters.AddWithValue("@MarksNumber", marksnum);
                cmdsb.Parameters.AddWithValue("@LoosePacking", loosepack);
                cmdsb.Parameters.AddWithValue("@NoOfContainer", noofcnt);
                cmdsb.Parameters.AddWithValue("@MAWBNO", MAWB);
                cmdsb.Parameters.AddWithValue("@HAWBNO", HAWB);
                cmdsb.Parameters.AddWithValue("@AmentType", amentype);
                cmdsb.Parameters.AddWithValue("@AmentNo", amenno);
                cmdsb.Parameters.AddWithValue("@AmentDate", amendate);
                cmdsb.ExecuteNonQuery();
                consb.Close();

                //invoice
                invoicedata = invoicedata.Remove(0, 1);
                string msgtypeinv = splituptospace(invoicedata);
                invoicedata = invoicedata.Remove(0, msgtypeinv.Length + 1);

                string customhousecodeinv = splituptospace(invoicedata);
                invoicedata = invoicedata.Remove(0, customhousecodeinv.Length + 1);

                string jobnoinv = splituptospace(invoicedata);
                invoicedata = invoicedata.Remove(0, jobnoinv.Length + 1);

                string jobnoinvdate = splituptospace(invoicedata);
                invoicedata = invoicedata.Remove(0, jobnoinvdate.Length + 1);

                string sbinvno = splituptospace(invoicedata);
                invoicedata = invoicedata.Remove(0, sbinvno.Length + 1);

                string sbinvdate = splituptospace(invoicedata);
                invoicedata = invoicedata.Remove(0, sbinvdate.Length + 1);

                string srinvno = splituptospace(invoicedata);
                invoicedata = invoicedata.Remove(0, srinvno.Length + 1);

                string invno = splituptospace(invoicedata);
                invoicedata = invoicedata.Remove(0, invno.Length + 1);

                string invdate = splituptospace(invoicedata);
                invoicedata = invoicedata.Remove(0, invdate.Length + 1);

                string invcur = splituptospace(invoicedata);
                invoicedata = invoicedata.Remove(0, invcur.Length + 1);

                string naturecontract = splituptospace(invoicedata);
                invoicedata = invoicedata.Remove(0, naturecontract.Length + 1);

                string buyername = splituptospace(invoicedata);
                invoicedata = invoicedata.Remove(0, buyername.Length + 1);

                string buyeradd1 = splituptospace(invoicedata);
                invoicedata = invoicedata.Remove(0, buyeradd1.Length + 1);

                string buyeradd2 = splituptospace(invoicedata);
                invoicedata = invoicedata.Remove(0, buyeradd2.Length + 1);

                string buyeradd3 = splituptospace(invoicedata);
                invoicedata = invoicedata.Remove(0, buyeradd3.Length + 1);

                string buyeradd4 = splituptospace(invoicedata);
                invoicedata = invoicedata.Remove(0, buyeradd4.Length + 1);

                string freightcur = splituptospace(invoicedata);
                invoicedata = invoicedata.Remove(0, freightcur.Length + 1);

                string freightamount = splituptospace(invoicedata);
                invoicedata = invoicedata.Remove(0, freightamount.Length + 1);

                string insrate = splituptospace(invoicedata);
                invoicedata = invoicedata.Remove(0, insrate.Length + 1);

                string inscur = splituptospace(invoicedata);
                invoicedata = invoicedata.Remove(0, inscur.Length + 1);

                string insamount = splituptospace(invoicedata);
                invoicedata = invoicedata.Remove(0, insamount.Length + 1);

                string comrate = splituptospace(invoicedata);
                invoicedata = invoicedata.Remove(0, comrate.Length + 1);

                string comcur = splituptospace(invoicedata);
                invoicedata = invoicedata.Remove(0, comcur.Length + 1);

                string comamt = splituptospace(invoicedata);
                invoicedata = invoicedata.Remove(0, comamt.Length + 1);

                string disfob = splituptospace(invoicedata);
                invoicedata = invoicedata.Remove(0, disfob.Length + 1);

                string discur = splituptospace(invoicedata);
                invoicedata = invoicedata.Remove(0, discur.Length + 1);

                string disamt = splituptospace(invoicedata);
                invoicedata = invoicedata.Remove(0, disamt.Length + 1);

                string othdeduct = splituptospace(invoicedata);
                invoicedata = invoicedata.Remove(0, othdeduct.Length + 1);

                string othdeductamt = splituptospace(invoicedata);
                invoicedata = invoicedata.Remove(0, othdeductamt.Length + 1);

                string othdeductcur = splituptospace(invoicedata);
                invoicedata = invoicedata.Remove(0, othdeductcur.Length + 1);

                string addfreight = splituptospace(invoicedata);
                invoicedata = invoicedata.Remove(0, addfreight.Length + 1);

                string packcharge = splituptospace(invoicedata);
                invoicedata = invoicedata.Remove(0, packcharge.Length + 1);

                string expcontract = splituptospace(invoicedata);
                invoicedata = invoicedata.Remove(0, expcontract.Length + 1);

                string naturepayment = splituptospace(invoicedata);
                invoicedata = invoicedata.Remove(0, naturepayment.Length);


                string queryinv = "insert into tbl_InvoiceDetails(Jobno,JobDate,Invsno,Invno,InvDate,InvCur,InvTerms,Buyername,BuyerAdd1,BuyerAdd2,BuyerAdd3,BuyerAdd4,Freightamount,FreightCurrency,InsuranceRate,Insuranceamount,InsuranceCurrency,CommisionRate,CommisionCurrency,CommisionAmount,DiscountFOB,DiscountCurrency,DiscountAmount,OtherDeduct,OtherDeductCurrency,OtherDeductAmount,UnitPriceMentioned,PackingCharge,ExportContract,NatureOfPayment)values(@Jobno,@JobDate,@Invsno,@Invno,@InvDate,@InvCur,@InvTerms,@Buyername,@BuyerAdd1,@BuyerAdd2,@BuyerAdd3,@BuyerAdd4,@Freightamount,@FreightCurrency,@InsuranceRate,@Insuranceamount,@InsuranceCurrency,@CommisionRate,@CommisionCurrency,@CommisionAmount,@DiscountFOB,@DiscountCurrency,@DiscountAmount,@OtherDeduct,@OtherDeductCurrency,@OtherDeductAmount,@UnitPriceMentioned,@PackingCharge,@ExportContract,@NatureOfPayment)";
                SqlConnection coninv = new SqlConnection(strconn);
                coninv.Open();
                SqlCommand cmdinv = new SqlCommand(queryinv, coninv);
                cmdinv.Parameters.AddWithValue("@Jobno", jobnoinv);
                cmdinv.Parameters.AddWithValue("@JobDate", jobnoinvdate);
                cmdinv.Parameters.AddWithValue("@Invsno", srinvno);
                cmdinv.Parameters.AddWithValue("@Invno", invno);
                cmdinv.Parameters.AddWithValue("@InvDate", invdate);
                cmdinv.Parameters.AddWithValue("@InvCur", invcur);
                cmdinv.Parameters.AddWithValue("@InvTerms", naturecontract);
                cmdinv.Parameters.AddWithValue("@Buyername", buyername);
                cmdinv.Parameters.AddWithValue("@BuyerAdd1", buyeradd1);
                cmdinv.Parameters.AddWithValue("@BuyerAdd2", buyeradd2);
                cmdinv.Parameters.AddWithValue("@BuyerAdd3", buyeradd3);
                cmdinv.Parameters.AddWithValue("@BuyerAdd4", buyeradd4);
                cmdinv.Parameters.AddWithValue("@Freightamount", freightamount);
                cmdinv.Parameters.AddWithValue("@FreightCurrency", freightcur);
                cmdinv.Parameters.AddWithValue("@InsuranceRate", insrate);
                cmdinv.Parameters.AddWithValue("@Insuranceamount", insamount);
                cmdinv.Parameters.AddWithValue("@InsuranceCurrency", inscur);
                cmdinv.Parameters.AddWithValue("@CommisionRate", comrate);
                cmdinv.Parameters.AddWithValue("@CommisionCurrency", comcur);
                cmdinv.Parameters.AddWithValue("@CommisionAmount", comamt);
                cmdinv.Parameters.AddWithValue("@DiscountFOB", disfob);
                cmdinv.Parameters.AddWithValue("@DiscountCurrency", discur);
                cmdinv.Parameters.AddWithValue("@DiscountAmount", disamt);
                cmdinv.Parameters.AddWithValue("@OtherDeduct", othdeduct);
                cmdinv.Parameters.AddWithValue("@OtherDeductCurrency", othdeductcur);
                cmdinv.Parameters.AddWithValue("@OtherDeductAmount", othdeductamt);
                cmdinv.Parameters.AddWithValue("@UnitPriceMentioned", addfreight);
                cmdinv.Parameters.AddWithValue("@PackingCharge", packcharge);
                cmdinv.Parameters.AddWithValue("@ExportContract", expcontract);
                cmdinv.Parameters.AddWithValue("@NatureOfPayment", naturepayment);
                cmdinv.ExecuteNonQuery();
                coninv.Close();

                //exchange
                exchdata = exchdata.Remove(0, 1);
                string msgtypeexch = splituptospace(exchdata);
                exchdata = exchdata.Remove(0, msgtypeexch.Length + 1);

                string customhousecodeexch = splituptospace(exchdata);
                exchdata = exchdata.Remove(0, customhousecodeexch.Length + 1);

                string jobnoexch = splituptospace(exchdata);
                exchdata = exchdata.Remove(0, jobnoexch.Length + 1);

                string jobnoexchdate = splituptospace(exchdata);
                exchdata = exchdata.Remove(0, jobnoexchdate.Length + 1);

                string sbexchno = splituptospace(exchdata);
                exchdata = exchdata.Remove(0, sbexchno.Length + 1);

                string sbexchdate = splituptospace(exchdata);
                exchdata = exchdata.Remove(0, sbexchdate.Length + 1);

                string exckcur = splituptospace(exchdata);
                exchdata = exchdata.Remove(0, exckcur.Length + 1);

                string exchcurname = splituptospace(exchdata);
                exchdata = exchdata.Remove(0, exchcurname.Length + 1);

                string unitpriceinout = splituptospace(exchdata);
                exchdata = exchdata.Remove(0, unitpriceinout.Length + 1);

                string exchrate = splituptospace(exchdata);
                exchdata = exchdata.Remove(0, exchrate.Length + 1);

                string effectivedate = splituptospace(exchdata);
                exchdata = exchdata.Remove(0, effectivedate.Length + 1);

                string standardcurornot = splituptospace(exchdata);
                exchdata = exchdata.Remove(0, standardcurornot.Length + 1);

                string amenttype = splituptospace(exchdata);
                exchdata = exchdata.Remove(0, amenttype.Length + 1);

                string amentno = splituptospace(exchdata);
                exchdata = exchdata.Remove(0, amentno.Length + 1);

                string amentdate = splituptospace(exchdata);
                exchdata = exchdata.Remove(0, amentdate.Length);


                string queryexch = "insert into tbl_ExchangeDeatils(Jobno,JobDate,ExchCurr,ExchCurrName,UnitinPrice,Exchrate,EffectiveDate,StandardCurrency,AmentType,AmentNo,AmentDate)values(@Jobno,@JobDate,@ExchCurr,@ExchCurrName,@UnitinPrice,@Exchrate,@EffectiveDate,@StandardCurrency,@AmentType,@AmentNo,@AmentDate)";
                SqlConnection conexch = new SqlConnection(strconn);
                conexch.Open();
                SqlCommand cmdexch = new SqlCommand(queryexch, conexch);
                cmdexch.Parameters.AddWithValue("@Jobno", jobnoexch);
                cmdexch.Parameters.AddWithValue("@JobDate", jobnoexchdate);
                cmdexch.Parameters.AddWithValue("@ExchCurr", exckcur);
                cmdexch.Parameters.AddWithValue("@ExchCurrName", exchcurname);
                cmdexch.Parameters.AddWithValue("@UnitinPrice", unitpriceinout);
                cmdexch.Parameters.AddWithValue("@Exchrate", exchrate);
                cmdexch.Parameters.AddWithValue("@EffectiveDate", effectivedate);
                cmdexch.Parameters.AddWithValue("@StandardCurrency", standardcurornot);
                cmdexch.Parameters.AddWithValue("@AmentType", amenttype);
                cmdexch.Parameters.AddWithValue("@AmentNo", amentno);
                cmdexch.Parameters.AddWithValue("@AmentDate", amentdate);
                cmdexch.ExecuteNonQuery();
                conexch.Close();

                //ITEM
                string[] itemdetails = itemdata.Split(new string[] { customidair, customidsea }, StringSplitOptions.RemoveEmptyEntries);
                int itemcount = itemdetails.Length;
                for (int i = 1; i <= itemcount - 1; i++)
                {
                    itemdetails[i] = itemdetails[i].Remove(0, 1);
                    string itemdatadet = itemdetails[i];
                    string jobnoitem = splituptospace(itemdatadet);
                    itemdatadet = itemdatadet.Remove(0, jobnoitem.Length + 1);

                    string jobnoitemdate = splituptospace(itemdatadet);
                    itemdatadet = itemdatadet.Remove(0, jobnoitemdate.Length + 1);

                    string sbitemno = splituptospace(itemdatadet);
                    itemdatadet = itemdatadet.Remove(0, sbitemno.Length + 1);

                    string sbitemdate = splituptospace(itemdatadet);
                    itemdatadet = itemdatadet.Remove(0, sbitemdate.Length + 1);

                    string invocesrno = splituptospace(itemdatadet);
                    itemdatadet = itemdatadet.Remove(0, invocesrno.Length + 1);

                    string invoceitemsrno = splituptospace(itemdatadet);
                    itemdatadet = itemdatadet.Remove(0, invoceitemsrno.Length + 1);

                    string schemcode = splituptospace(itemdatadet);
                    itemdatadet = itemdatadet.Remove(0, schemcode.Length + 1);

                    string ritccode = splituptospace(itemdatadet);
                    itemdatadet = itemdatadet.Remove(0, ritccode.Length + 1);

                    string desgoods1 = splituptospace(itemdatadet);
                    itemdatadet = itemdatadet.Remove(0, desgoods1.Length + 1);

                    string desgoods2 = splituptospace(itemdatadet);
                    itemdatadet = itemdatadet.Remove(0, desgoods2.Length + 1);

                    string desgoods3 = splituptospace(itemdatadet);
                    itemdatadet = itemdatadet.Remove(0, desgoods3.Length + 1);

                    string unitmeasure = splituptospace(itemdatadet);
                    itemdatadet = itemdatadet.Remove(0, unitmeasure.Length + 1);

                    string qty = splituptospace(itemdatadet);
                    itemdatadet = itemdatadet.Remove(0, qty.Length + 1);

                    string unitprice = splituptospace(itemdatadet);
                    itemdatadet = itemdatadet.Remove(0, unitprice.Length + 1);

                    string unitrate = splituptospace(itemdatadet);
                    itemdatadet = itemdatadet.Remove(0, unitrate.Length + 1);

                    string nounit = splituptospace(itemdatadet);
                    itemdatadet = itemdatadet.Remove(0, nounit.Length + 1);

                    string presentrate = splituptospace(itemdatadet);
                    itemdatadet = itemdatadet.Remove(0, presentrate.Length + 1);

                    string jobwork = splituptospace(itemdatadet);
                    itemdatadet = itemdatadet.Remove(0, jobwork.Length + 1);

                    string thirdparty = splituptospace(itemdatadet);
                    itemdatadet = itemdatadet.Remove(0, thirdparty.Length + 1);

                    string reward = splituptospace(itemdatadet);
                    itemdatadet = itemdatadet.Remove(0, reward.Length + 1);

                    string amenttypeitem = splituptospace(itemdatadet);
                    itemdatadet = itemdatadet.Remove(0, amenttypeitem.Length + 1);

                    string amentnoitem = splituptospace(itemdatadet);
                    itemdatadet = itemdatadet.Remove(0, amentnoitem.Length + 1);

                    string amentdateitem = splituptospace(itemdatadet);
                    itemdatadet = itemdatadet.Remove(0, amentdateitem.Length);

                    string queryitem = "insert into tbl_ItemDetails(Jobno,JobDate,InvSrNo,ItemInvsrno,Schemecode,Ritccode,DiscriptinGoods1,DiscriptinGoods2,DiscriptinGoods3,UnitMeasure,Quantity,UnitPrice,Unitrate,NoofUnit,PresentMarket,JobWorkNotify,ThirdParty,Reward,AmentType,AmentNo,AmentDate)values(@Jobno,@JobDate,@InvSrNo,@ItemInvsrno,@Schemecode,@Ritccode,@DiscriptinGoods1,@DiscriptinGoods2,@DiscriptinGoods3,@UnitMeasure,@Quantity,@UnitPrice,@Unitrate,@NoofUnit,@PresentMarket,@JobWorkNotify,@ThirdParty,@Reward,@AmentType,@AmentNo,@AmentDate)";
                    SqlConnection conitem = new SqlConnection(strconn);
                    conitem.Open();
                    SqlCommand cmditem = new SqlCommand(queryitem, conitem);
                    cmditem.Parameters.AddWithValue("@Jobno", jobnoitem);
                    cmditem.Parameters.AddWithValue("@JobDate", jobnoitemdate);
                    cmditem.Parameters.AddWithValue("@InvSrNo", invocesrno);
                    cmditem.Parameters.AddWithValue("@ItemInvsrno", invoceitemsrno);
                    cmditem.Parameters.AddWithValue("@Schemecode", schemcode);
                    cmditem.Parameters.AddWithValue("@Ritccode", ritccode);
                    cmditem.Parameters.AddWithValue("@DiscriptinGoods1", desgoods1);
                    cmditem.Parameters.AddWithValue("@DiscriptinGoods2", desgoods2);
                    cmditem.Parameters.AddWithValue("@DiscriptinGoods3", desgoods3);
                    cmditem.Parameters.AddWithValue("@UnitMeasure", unitmeasure);
                    cmditem.Parameters.AddWithValue("@Quantity", qty);
                    cmditem.Parameters.AddWithValue("@UnitPrice", unitprice);
                    cmditem.Parameters.AddWithValue("@Unitrate", unitrate);
                    cmditem.Parameters.AddWithValue("@NoofUnit", nounit);
                    cmditem.Parameters.AddWithValue("@PresentMarket", presentrate);
                    cmditem.Parameters.AddWithValue("@JobWorkNotify", jobwork);
                    cmditem.Parameters.AddWithValue("@ThirdParty", thirdparty);
                    cmditem.Parameters.AddWithValue("@Reward", reward);
                    cmditem.Parameters.AddWithValue("@AmentType", amenttypeitem);
                    cmditem.Parameters.AddWithValue("@AmentNo", amentnoitem);
                    cmditem.Parameters.AddWithValue("@AmentDate", amentdateitem);
                    cmditem.ExecuteNonQuery();
                    conitem.Close();
                }
                Session["JobNoRead"] = jobno;
                JobRead();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Data has been Imported Successfully');", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('This File has been already uploaded');", true);
            }
        }

        public void JobRead()
        {
            DataSet ds = new DataSet();
            SqlConnection sqlcon = new SqlConnection(strconn);
            string query = "select * from View_JobCreationSBRead where JobNo ='" + (string)Session["JobNoRead"] + "'";
            SqlDataAdapter da = new SqlDataAdapter(query, sqlcon);
            da.Fill(ds, "Jobdetails");
            sqlcon.Close();
            if (ds.Tables["Jobdetails"].Rows.Count != 0)
            {
                DataRowView row = ds.Tables["Jobdetails"].DefaultView[0];
                txtJobNo.Text = row["Jobno"].ToString();
                string JobDate = (row["JobDate"].ToString());
                if (JobDate != "")
                {
                    DateTime jobd = Convert.ToDateTime(JobDate);
                    txtJobDate.Text = jobd.ToString("dd/MM/yyyy");
                }
                string shipmenttype = row["ShipmentType"].ToString();
                if (shipmenttype == "F")
                {
                    ddlShipmentType.SelectedValue = "Sea";
                    ddlModeOfShipment.SelectedValue = "FCL";
                    ddlModeOfShipment.Enabled = true;
                }
                else if (shipmenttype == "L")
                {
                    ddlShipmentType.SelectedValue = "Sea";
                    ddlModeOfShipment.SelectedValue = "LCL";
                    ddlModeOfShipment.Enabled = true;
                }
                txtImpExpName.Text = row["ImpName"].ToString();
                txtImpExpCode.Text = row["Importerexportercode"].ToString();
                txtImpExpBranchCode.Text = row["Codebranch"].ToString();
                txtImpExpAddress.Text = row["Address"].ToString();
                string ClassType = row["Class"].ToString();

              //  txtConsigneeName.Text = row["Consigneename"].ToString();
                txtConsigneeAddress.Text = row["Consigneeaddress"].ToString();
                if (ClassType == "P")
                {
                    ddlImpExpClassType.SelectedValue = "Private";
                }
                else if (ClassType == "G")
                {
                    ddlImpExpClassType.SelectedValue = "Government";
                }

                //txtPortOfOrigin.Text = row["Address"].ToString();
                //txtOriginPortCode.Text = row["Address"].ToString();
                txtOriginStateCode.Text = row["StatecodeOrigin"].ToString();
                //txtOriginCountryCode.Text = row["Address"].ToString();
                //txtPortOfDestination.Text = row["Address"].ToString();
                //txtDestinationPortCode.Text = row["Address"].ToString();
                //txtDestinationStateCode.Text = row["Address"].ToString();
                //txtDestinationCountryCode.Text = row["Address"].ToString();
                txtInvoiceNo.Text = row["Invno"].ToString();
                string InvDate = row["InvDate"].ToString();

                if (InvDate != "")
                {
                    DateTime InvoiceDate = Convert.ToDateTime(InvDate);
                    txtInvoiceDate.Text = InvoiceDate.ToString("dd/MM/yyyy");
                }
            }
        }

        public string invoicesplit(string invbase)
        {
            string srinvno = string.Empty;
            for (int i = 0; i < invbase.Length; i++)
            {
                if (char.IsLetterOrDigit(invbase[i]))
                {
                    if (Char.IsDigit(invbase[i]))
                    {

                        srinvno = invbase[i].ToString();
                        // TEMP=i;
                        i = invbase.Length;
                    }
                }
                else
                {
                    //i = consignee.Length;
                    srinvno = invbase[0].ToString();
                    // TEMP=i;

                }
            }
            return srinvno;
        }

        public string invoicenosplit(string invbase)
        {
            string srinvno = string.Empty;
            for (int i = 0; i < invbase.Length; i++)
            {
                if (char.IsLetterOrDigit(invbase[i]))
                {
                    srinvno = srinvno + invbase[i].ToString();
                    // TEMP=i;


                }
                else if (char.IsPunctuation(invbase[i]))
                {
                    srinvno = srinvno + invbase[i].ToString();
                }
                else
                {
                    //i = consignee.Length;

                    i = invbase.Length;
                    // TEMP=i;

                }
            }
            return srinvno;
        }

        public bool CheckIfFileIsBeingUsed(string fileName)
        {

            try
            {

                FileStream fs = File.Open(fileName, FileMode.Open, FileAccess.Read, FileShare.None);
                fs.Close();
                fs.Dispose();

            }

            catch (Exception exp)
            {

                return true;

            }

            return false;

        }

        public string checking(string numer, int num)
        {
            string value = string.Empty;
            int n = numer.Length;
            for (int i = 0; i < n; i++)
            {
                if (Char.IsPunctuation(numer[i]))
                {

                    value += numer[i];
                    n = i + num + 1;

                }
                else
                {
                    value += numer[i];

                }
            }
            return value;
        }

        public string numspliting(string numer)
        {
            string value = string.Empty;
            string com = string.Empty;
            int nvalue = 0;
            int n = numer.Length;
            for (int i = 0; i < n; i++)
            {
                if (Char.IsDigit(numer[i]))
                {
                    value += numer[i];
                    nvalue = i;


                }
                else if (numer[i] == '.')
                {
                    value = value + numer[i];
                    if (Char.IsDigit(numer[i + 2]) && Char.IsDigit(numer[i + 1]) && Char.IsDigit(numer[i + 3]))
                    {
                        n = i + 4;
                    }


                }
                else if (numer[i] == ' ')
                {
                    value = string.Empty;


                }
            }
            return value;
        }

        public string spacesplit(string invbase)
        {
            string srinvno = string.Empty;
            for (int i = 0; i < invbase.Length; i++)
            {
                if (char.IsLetterOrDigit(invbase[i]))
                {




                }
                else if (char.IsPunctuation(invbase[i]))
                {
                    srinvno = srinvno + invbase[i].ToString();
                }
                else if (invbase[i] == '.')
                {


                }
                else if (char.IsSeparator(invbase[i]))
                {


                }
                else if (char.IsSymbol(invbase[i]))
                {


                }
                else
                {


                    invbase.Remove(i);

                }
            }
            return invbase;
        }

        protected void ddlShipmentType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlShipmentType.SelectedValue == "Sea")
            {
                ddlModeOfShipment.Enabled = true;
            }
            else
            {
                ddlModeOfShipment.Enabled = false;
                ddlModeOfShipment.SelectedValue = "~Select~";
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Clear();
        }

        public string splituptodotwithdegitallow(string numer, int num)
        {
            string value = string.Empty;
            int n = numer.Length;
            for (int i = 0; i < n; i++)
            {
                if (Char.IsPunctuation(numer[i]))
                {

                    value += numer[i];
                    n = i + num + 1;

                }
                else
                {
                    value += numer[i];

                }
            }
            return value;
        }

        protected void btnsearch_Click(object sender, EventArgs e)
        {
        
                if(rbljobnosearch.SelectedValue!="")
                {


                    

                     if(rbljobnosearch.SelectedValue=="Import" && txtSearchjobno.Text=="")
                    {
                        SqlConnection con = new SqlConnection(strconn);
                        con.Open();
                        string quer = string.Empty;
                        DataSet ds = new DataSet();
                        quer = "select A.JobNo,A.JobReceivedDate as Date,B.Importer as Name,C.InvoiceNo,C.InvoiceDate from T_JobCreation as A inner join T_Importer as B on A.JobNo=B.JobNo inner join T_InvoiceDetails as C on A.JobNo=C.JobNo ";
                        SqlDataAdapter da = new SqlDataAdapter(quer, con);
                        da.Fill(ds, "data");
                        con.Close();

                        if (ds.Tables["data"].Rows.Count != 0)
                        {
                            gvJobNo.DataSource = ds;
                            gvJobNo.DataBind();
                        }

                        else
                        {                       
                            gvJobNo.DataSource = null;
                            gvJobNo.DataBind();
                        }
                        
                    }
                     else if (rbljobnosearch.SelectedValue == "Import")
                     {
                         SqlConnection con = new SqlConnection(strconn);
                         con.Open();
                         string quer = string.Empty;
                         DataSet ds = new DataSet();
                         quer = "select A.JobNo,A.JobReceivedDate as Date,B.Importer as Name,C.InvoiceNo,C.InvoiceDate from T_JobCreation as A inner join T_Importer as B on A.JobNo=B.JobNo inner join T_InvoiceDetails as C on A.JobNo=C.JobNo where A.JobNo='" + txtSearchjobno.Text + "'";
                         SqlDataAdapter da = new SqlDataAdapter(quer, con);
                         da.Fill(ds, "data");
                         con.Close();

                         if (ds.Tables["data"].Rows.Count != 0)
                         {
                             gvJobNo.DataSource = ds;
                             gvJobNo.DataBind();
                         }

                         else
                         {
                             Response.Write("<script LANGUAGE='JavaScript' >alert('Sorry this is not a Import JobNo')</script>");
                             gvJobNo.DataSource = null;
                             gvJobNo.DataBind();
                         }
                     }


                    
                    if(rbljobnosearch.SelectedValue=="Export" && txtSearchjobno.Text=="")
                    {
                        SqlConnection con = new SqlConnection(strconn);
                        con.Open();
                        string quer = string.Empty;
                        DataSet ds = new DataSet();
                        quer = "select A.JobNo,A.JobReceivedOn as Date,B.ExporterName as Name,C.InvoiceNo,C.InvoiceDate from E_M_JobCreation as A inner join E_T_Exporter as B on A.JobNo=B.JobNo inner join E_T_Invoice as C on A.JobNo=C.JobNo ";
                        SqlDataAdapter da = new SqlDataAdapter(quer, con);
                        da.Fill(ds, "data");
                        con.Close();

                        if (ds.Tables["data"].Rows.Count != 0)
                        {
                            gvJobNo.DataSource = ds;
                            gvJobNo.DataBind();
                        }

                        else
                        {
                            
                            gvJobNo.DataSource = null;
                            gvJobNo.DataBind();
                        }
                         }
                    else if (rbljobnosearch.SelectedValue == "Export")
                    {
                        SqlConnection con = new SqlConnection(strconn);
                        con.Open();
                        string quer = string.Empty;
                        DataSet ds = new DataSet();
                        quer = "select A.JobNo,A.JobReceivedOn as Date,B.ExporterName as Name,C.InvoiceNo,C.InvoiceDate from E_M_JobCreation as A inner join E_T_Exporter as B on A.JobNo=B.JobNo inner join E_T_Invoice as C on A.JobNo=C.JobNo where A.JobNo='" + txtSearchjobno.Text + "'";
                        SqlDataAdapter da = new SqlDataAdapter(quer, con);
                        da.Fill(ds, "data");
                        con.Close();

                        if (ds.Tables["data"].Rows.Count != 0)
                        {

                            gvJobNo.DataSource = ds;
                            gvJobNo.DataBind();
                        }

                        else
                        {
                            Response.Write("<script LANGUAGE='JavaScript' >alert('Sorry this is not a Export JobNo')</script>");
                            gvJobNo.DataSource = null;
                            gvJobNo.DataBind();
                        }
                    }


            }
            else
            {
                Response.Write("<script LANGUAGE='JavaScript' >alert('Please Select Import or Export')</script>");

            }

        }
    }
}