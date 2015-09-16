using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Data.OleDb;
using System.Data;
using MySql.Data.MySqlClient;
using System.Text;

namespace ImpexCube
{
    public partial class CSVLoad : System.Web.UI.Page
    {
        string strconn = (string)ConfigurationManager.AppSettings["ConnectionDashboard"];
        string strcon = (string)ConfigurationManager.ConnectionStrings["Constr"].ConnectionString;
        private string FilePath = "";
        OleDbConnection oledbConn;
        System.Data.DataTable dt = null;
        //string FY = "";
        int Result = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            Label pagename;
            pagename = (Label)Master.FindControl("lblName");
            pagename.Text = "SEW Excel Upload";

           // FY = (string)Session["FYear"];
            if (!IsPostBack)
            {
                BindJobNo();
            }
        }

        private void BindJobNo()
        {
            string Query = "Select Distinct JOBNO From T_InvoiceDetails";
            DataSet ds = GetDataMy(Query);
            if (ds.Tables["data"].Rows.Count != 0)
            {
                ddlJobNo.DataSource = ds;
                ddlJobNo.DataTextField = "JOBNO";
                ddlJobNo.DataValueField = "JOBNO";
                ddlJobNo.DataBind();
                ddlJobNo.Items.Insert(0, new ListItem("~Select~", "0"));
            }
            else
            {
                ddlJobNo.DataSource = null;
                ddlJobNo.DataBind();
            }
        }

        public DataSet GetDataMy(string Query)
        {
            DataSet ds = new DataSet();
            try
            {
                SqlConnection con = new SqlConnection(strconn);
                con.Open();
                SqlDataAdapter sd = new SqlDataAdapter(Query, con);
                sd.Fill(ds, "data");
                con.Close();
            }
            catch (Exception ex)
            {
                string Msg = ex.Message.ToString();
            }
            return ds;
        }

        protected void btnLoad_Click(object sender, EventArgs e)
        {
            string path = "";

                string FileN = (lblMsg.Text);
                string FileExt = Path.GetExtension(lblMsg.Text );
                string filename = Path.GetFileName(FileN);
                Session["ExcelName"] = filename;
                if (FileExt == ".xlsx")
                {
                    string path1 = (string)Session["file"];
                    if ((drfile.SelectedValue != "_xlnm#_FilterDatabase") || (drfile.SelectedValue != "~Select~"))
                    {
                        int result = GenerateExcelData(path1, drfile.SelectedValue);
                        if (result == 1)
                        {
                           InsertExcelData(result);
                           ScriptManager.RegisterStartupScript(this, this.GetType(), "redirect", "alert('" + lblMsg.Text + "  File Saved Successfully'); window.location.href='CSVLoad.aspx';", true);
                        }
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "redirect", "alert('Please Select Correct Sheet');", true);
                    }
                }
        }

        private void InsertExcelData(int result)
        {
            int res;
            if (result != 0)
            {
                try
                {
                    string query = "Select ExcelName from M_ExcelRead where JobNo = '" + ddlJobNo.SelectedValue + "' and ExcelName = '" + (string)Session["ExcelName"] + "' ";
                    DataSet ds = GetData(query);
                    if (ds.Tables["data"].Rows.Count == 0)
                    {
                        InsertExcelData();
                        res = DeleteTempData();
                        if (res != 0)
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "redirect", "alert('Saved Successfully'); window.location.href='CSVLoad.aspx';", true);
                        }
                    }
                    else
                    {
                        res = DeleteTempData();
                        if (res != 0)
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Excel selected is already Uploaded'); window.location.href='CSVLoad.aspx';", true);
                        }
                    }
                }
                catch (Exception ex)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert(' " + ex.Message + " ');", true);
                }
            }
        }

        private int DeleteTempData()
        {
            string query = "Truncate Table TempData";
            int result = GetCommand(query);
            return result;
        }

        private void InsertExcelData()
        {
            string InvSrNo = string.Empty;
            string ProductCode = string.Empty;
            string Invoicenumber = string.Empty;
            string InvoiceItem = string.Empty;
            string DeliveryNumber = string.Empty;
            string PurchaseOrderNumber = string.Empty;
            string PurchaseOrderItem = string.Empty;
            string Material = string.Empty;
            string Description = string.Empty;
            string Quantity = string.Empty;
            string QuantityUnit = string.Empty;
            string NetValue = string.Empty;
            string TotalValue = string.Empty;
            string ValueUnit = string.Empty;
            string TotalWeight = string.Empty;
            string WeightUnit = string.Empty;
            string CommodityCode = string.Empty;
            string CountryOfOrigin = string.Empty;
            string Preference = string.Empty;
            string MateriaDescription = string.Empty;
            string Addition = string.Empty;
            string UnitPrice = string.Empty;
            string PoSrNo = string.Empty;

            string query = "SELECT [InvoiceSrNo],[Invoicenumber],[InvoiceItem],[DeliveryNumber],[PoSrNo],[PurchaseOrderNumber],[PurchaseOrderItem],[Material],[ProductCode],[Description],[UnitPrice],[Quantity],[QuantityUnit]," +
                "[NetValue],[TotalValue],[ValueUnit],[TotalWeight],[WeightUnit],[CommodityCode],[CountryOfOrigin],[Preference],[MateriaDescription],[Addition] FROM TempData ";
            DataSet ds = GetData(query);
            DataTable dt = ds.Tables["data"];
            if (ds.Tables["data"].Rows.Count != 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    InvSrNo = row["InvoiceSrNo"].ToString().Trim();
                    ProductCode = row["ProductCode"].ToString().Trim();
                    Invoicenumber = row["Invoicenumber"].ToString().Trim();
                    InvoiceItem = row["InvoiceItem"].ToString().Trim();
                    DeliveryNumber = row["DeliveryNumber"].ToString().Trim();
                    PurchaseOrderNumber = row["PurchaseOrderNumber"].ToString().Trim();
                    PurchaseOrderItem = row["PurchaseOrderItem"].ToString().Trim();
                    Material = row["Material"].ToString().Trim();
                    Description = row["Description"].ToString().Trim();
                    Quantity = row["Quantity"].ToString().Trim();
                    QuantityUnit = row["QuantityUnit"].ToString().Trim();
                    NetValue = row["NetValue"].ToString().Trim();
                    TotalValue = row["TotalValue"].ToString().Trim();
                    ValueUnit = row["ValueUnit"].ToString().Trim();
                    TotalWeight = row["TotalWeight"].ToString().Trim();
                    WeightUnit = row["WeightUnit"].ToString().Trim();
                    CommodityCode = row["CommodityCode"].ToString().Trim();
                    CountryOfOrigin = row["CountryOfOrigin"].ToString().Trim();
                    Preference = row["Preference"].ToString().Trim();
                    MateriaDescription = row["MateriaDescription"].ToString().Trim();
                    Addition = row["Addition"].ToString().Trim();
                    UnitPrice = row["UnitPrice"].ToString().Trim();
                    PoSrNo = row["PoSrNo"].ToString().Trim();

                    StringBuilder insertquery = new StringBuilder();

                    insertquery.Append("INSERT INTO M_ExcelRead([JobNo],[InvoiceSrNo],[Invoicenumber],[InvoiceItem],[DeliveryNumber],[PoSrNo],[PurchaseOrderNumber],[PurchaseOrderItem],");
                    insertquery.Append("[Material],[ProductCode],[Description],[UnitPrice],[Quantity],[QuantityUnit],[NetValue],[TotalValue],[ValueUnit],[TotalWeight],[WeightUnit],[CommodityCode],[CountryOfOrigin],");
                    insertquery.Append("[Preference],[MateriaDescription],[ExcelName],[Addition],[SEWRitc],[CreatedBy],[CreatedDate],[ModifiedBy],[ModifiedDate])");
                    insertquery.Append("Values (@JobNo,@InvoiceSrNo,@Invoicenumber,@InvoiceItem,@DeliveryNumber,@PoSrNo,@PurchaseOrderNumber,@PurchaseOrderItem,");
                    insertquery.Append("@Material,@ProductCode,@Description,@UnitPrice,@Quantity,@QuantityUnit,@NetValue,@TotalValue,@ValueUnit,@TotalWeight,@WeightUnit,@CommodityCode,@CountryOfOrigin,");
                    insertquery.Append("@Preference,@MateriaDescription,@ExcelName,@Addition,@SEWRitc,@CreatedBy,@CreatedDate,@ModifiedBy,@ModifiedDate)");
                  
                    SqlConnection con = new SqlConnection(strcon);
                    con.Open();
                    SqlCommand cmd = new SqlCommand(insertquery.ToString(), con);

                    cmd.Parameters.AddWithValue("@JobNo", ddlJobNo.SelectedValue.Trim());
                    cmd.Parameters.AddWithValue("@InvoiceSrNo", InvSrNo.Trim());
                    cmd.Parameters.AddWithValue("@Invoicenumber", Invoicenumber.Trim());
                    cmd.Parameters.AddWithValue("@InvoiceItem", InvoiceItem.Trim());
                    cmd.Parameters.AddWithValue("@DeliveryNumber", DeliveryNumber.Trim());
                    cmd.Parameters.AddWithValue("@PoSrNo", PoSrNo.Trim());
                    cmd.Parameters.AddWithValue("@PurchaseOrderNumber", PurchaseOrderNumber.Trim());
                    cmd.Parameters.AddWithValue("@PurchaseOrderItem", PurchaseOrderItem.Trim());
                    cmd.Parameters.AddWithValue("@Material", Material.Trim());
                    cmd.Parameters.AddWithValue("@ProductCode", ProductCode.Trim());
                    cmd.Parameters.AddWithValue("@Description", Description.Trim());
                    cmd.Parameters.AddWithValue("@UnitPrice", UnitPrice.Trim());
                    cmd.Parameters.AddWithValue("@Quantity", Quantity.Trim());
                    cmd.Parameters.AddWithValue("@QuantityUnit", QuantityUnit.Trim());
                    cmd.Parameters.AddWithValue("@NetValue", NetValue.Trim());
                    cmd.Parameters.AddWithValue("@TotalValue", TotalValue.Trim());
                    cmd.Parameters.AddWithValue("@ValueUnit", ValueUnit.Trim());
                    cmd.Parameters.AddWithValue("@TotalWeight", TotalWeight.Trim());
                    cmd.Parameters.AddWithValue("@WeightUnit", WeightUnit.Trim());
                    cmd.Parameters.AddWithValue("@CommodityCode", CommodityCode.Trim());
                    cmd.Parameters.AddWithValue("@CountryOfOrigin", CountryOfOrigin.Trim());
                    cmd.Parameters.AddWithValue("@Preference", Preference.Trim());
                    cmd.Parameters.AddWithValue("@MateriaDescription", MateriaDescription.Trim());
                    cmd.Parameters.AddWithValue("@ExcelName",(string)Session["ExcelName"]);
                    cmd.Parameters.AddWithValue("@Addition", Addition.Trim());
                    cmd.Parameters.AddWithValue("@SEWRitc", SEWRITCCode(ProductCode.Trim()));
                    cmd.Parameters.AddWithValue("@CreatedBy",(string)Session["USER-NAME"]);
                    cmd.Parameters.AddWithValue("@CreatedDate",DateTime.Now);
                    cmd.Parameters.AddWithValue("@ModifiedBy", (string)Session["USER-NAME"]);
                    cmd.Parameters.AddWithValue("@ModifiedDate",DateTime.Now);

                    cmd.ExecuteNonQuery();
                    con.Close();
                    //string insertquery = "" +
                    //   "" +
                    //   "" +
                    //   " '"+InvSrNo+"','" + Invoicenumber + "', '" + InvoiceItem + "', '" + DeliveryNumber + "', '" + PurchaseOrderNumber + "','" + PurchaseOrderItem + "'," +
                    //   " '" + Material + "','"+ProductCode+"', '" + Description + "' ,'"+UnitPrice+"', '" + Quantity + "', '" + QuantityUnit + "', '" + NetValue + "', '" + TotalValue + "'," +
                    //   " '" + ValueUnit + "', '" + TotalWeight + "', '" + WeightUnit + "', '" + CommodityCode + "', '" + CountryOfOrigin + "','" + Preference + "', " +
                    //   " '" + MateriaDescription + "', '" + (string)Session["ExcelName"] + "','"+Addition+"', '" + (string)Session["USER-NAME"] + "', '" + DateTime.Now + "'," +
                    //   " '" + (string)Session["USER-NAME"] + "', '" + DateTime.Now + "' )";

                    //GetCommand(insertquery);
                }
            }
        }
        public string  SEWRITCCode(string ProductCode)
        {
            string qry = "Select ChapterID from T_Traiff where Material='" + ProductCode + "'";
           DataSet ds= GetData(qry);
           string sewritc = string.Empty;
           if (ds.Tables["data"].Rows.Count > 0)
           {
               DataRowView row = ds.Tables["data"].DefaultView[0];
               sewritc = row["ChapterID"].ToString();
           }
           return sewritc;
        }
        public string  SEWUOM(string RITCCode)
        {
            string qry = "Select ChapterID from T_Traiff where Material='" + RITCCode + "'";
           DataSet ds= GetData(qry);
           string sewritc = string.Empty;
           if (ds.Tables["data"].Rows.Count > 0)
           {
               DataRowView row = ds.Tables["data"].DefaultView[0];
               sewritc = row["RITCCode"].ToString();
           }
           return sewritc;
        }
        public int GetCommand(string Query)
        {
            SqlConnection con = new SqlConnection(strcon);
            con.Open();
            SqlDataAdapter da = new SqlDataAdapter();
            SqlCommand cmd = new SqlCommand(Query, con);
            cmd.CommandText = Query;
            cmd.Connection = con;
            da.SelectCommand = cmd;
            int result = cmd.ExecuteNonQuery();
            con.Close();
            return result;
        }

        public DataSet GetData(string Query)
        {
            DataSet ds = new DataSet();
            try
            {
                SqlConnection sqlConn = new SqlConnection(strcon);
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

        public bool CheckIfFileIsBeingUsed(string fileName)
        {
            try
            {
                FileStream fs;
                using(fs = new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.None))
                {
                    fs.Dispose();
                    fs.Close();
                }
                //if (!Directory.Exists(fileName))
                //{
                    
                //}
                //else
                //{
                //    Directory.Delete(fileName);
                //}
            }

            catch (Exception exp)
            {
                return true;
            }

            return false;

        }

        protected void btnRead_Click(object sender, EventArgs e)
        {
            lblMsg.Text = FileUpload1.PostedFile.FileName;
            string names = FileUpload1.PostedFile.FileName;
            string CheckExtension = Path.GetExtension(names);
            string path = "";
            if (FileUpload1.HasFile)
            {
                FileUpload1.Enabled = false;
                string paths = AppDomain.CurrentDomain.BaseDirectory;
                string pathdir = Path.Combine(paths, @"TempFile\");
                path = pathdir + Path.GetFileName(FileUpload1.PostedFile.FileName);
                string[] filePaths = Directory.GetFiles(@pathdir);
                string TestFileName = FileUpload1.FileName;

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

            string name = FileUpload1.FileName;
            string filename = Path.GetFileName(FileUpload1.FileName);
            Session["file"] = "";
            Session["file"] = path;
            Session["filename"] = "";
            if (CheckExtension == ".xlsx")
            {
                try
                {
                    loads(path);
                    btnRead.Visible = false;
                    FileUpload1.Visible = false;
                }
                catch (Exception ex)
                {

                }
            }
        }

        public void loads(string SlnoAbbreviation)
        {
            string path = System.IO.Path.GetFullPath(@SlnoAbbreviation);

            oledbConn = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" +
              path + ";Extended Properties='Excel 12.0;HDR=YES;IMEX=1;';");
            oledbConn.Open();
            OleDbCommand cmd = new OleDbCommand();
            OleDbDataAdapter oleda = new OleDbDataAdapter();
            DataSet ds = new DataSet();


            cmd.Connection = oledbConn;
            cmd.CommandType = CommandType.Text;

            dt = oledbConn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);

            if (dt == null)
            {

            }

            String[] excelSheets = new String[dt.Rows.Count];
            int i = 0;


            foreach (DataRow row in dt.Rows)
            {
                excelSheets[i] = row["TABLE_NAME"].ToString();
                i++;
            }
            for (int j = 0; j < excelSheets.Length; j++)
            {
                drfile.Items.Add(excelSheets[j]);
            }
            drfile.Visible = true;
            drfile.Items.Insert(0, new ListItem("~Select~", "0"));
            drfile.Items.Remove("_xlnm#_FilterDatabase");
            
        }

        public int  GenerateExcelData(string SlnoAbbreviation, string sheetname)
        {
            try
            {
                string path = System.IO.Path.GetFullPath(@SlnoAbbreviation);
                oledbConn = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" +
                  path + ";Extended Properties='Excel 12.0;HDR=YES;IMEX=1;';");
                oledbConn.Open();
                OleDbCommand cmd = new OleDbCommand();
                OleDbDataAdapter oleda = new OleDbDataAdapter();
                DataSet ds = new DataSet();
                cmd.Connection = oledbConn;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT * FROM [" + sheetname + "]";
                cmd.Parameters.AddWithValue("Slno_Abbreviation", SlnoAbbreviation);
                oleda = new OleDbDataAdapter(cmd);
                oleda.Fill(ds, "SQLTABLE");

                foreach (DataRow row1 in ds.Tables["SQLTABLE"].Rows)
                {
                    string InvNo = row1["Inv# No#"].ToString();
                    string InvSrNo = row1["Inv# Sr# No#"].ToString();
                    string PLNO = row1["P/L No#"].ToString();
                    string PoNo = row1["PO No#"].ToString();
                    string PoSrNo = row1["PO Sr# No#"].ToString();
                    string PartNo = row1["Part No#"].ToString();
                    string ItemDesc = row1["Item Description"].ToString();
                    string QtyNo = row1["Qty No# "].ToString();
                    string UM1 = row1["U/M"].ToString();
                    string UnitPrice = row1["Per Qty Amt"].ToString();
                    string TotalAmt = row1["Total Amt"].ToString();
                    string Currency = row1["Currency"].ToString();
                    string TotalNetWeight = row1["Total Net Weight"].ToString();
                    string UM2 = row1["U/M1"].ToString();
                    string HSCode = row1["HS Code"].ToString();
                    string CountryOfOrigin = row1["Country of Origin"].ToString();
                    string ValueUnit = row1["Currency"].ToString();
                    string Addition = row1["Addition"].ToString();
                    StringBuilder Query = new StringBuilder();
                    string Message = string.Empty;
                    DataSet ds1 = new DataSet();
                    try
                    {
                        using (SqlConnection con = new SqlConnection(strcon))
                        {
                            con.Open();
                            Query.Append("INSERT INTO [TempData] ([Invoicenumber],[InvoiceSrNo],[PoSrNo],[PurchaseOrderNumber],[ProductCode],[Description],[UnitPrice],[Quantity],[QuantityUnit],[TotalValue],[TotalWeight], [WeightUnit],[CommodityCode],[CountryOfOrigin],ValueUnit,Addition)");
                            Query.Append("values(@Invoicenumber,@InvoiceSrNo,@PoSrNo,@PurchaseOrderNumber,@ProductCode,@Description,@UnitPrice,@Quantity,@QuantityUnit,@TotalValue,@TotalWeight,@WeightUnit,@CommodityCode,@CountryOfOrigin,@ValueUnit,@Addition)");
                            SqlCommand cmd1 = new SqlCommand(Query.ToString(), con);

                            cmd1.Parameters.AddWithValue("@Invoicenumber", InvNo.Trim());
                            cmd1.Parameters.AddWithValue("@InvoiceSrNo",InvSrNo.Trim());
                            cmd1.Parameters.AddWithValue("@PurchaseOrderNumber", PoNo.Trim());
                            cmd1.Parameters.AddWithValue("@ProductCode", PartNo.Trim());
                            cmd1.Parameters.AddWithValue("@UnitPrice", UnitPrice.Trim());
                            cmd1.Parameters.AddWithValue("@Description", ItemDesc.ToUpper().Trim());
                            cmd1.Parameters.AddWithValue("@Quantity", QtyNo.Trim());
                            cmd1.Parameters.AddWithValue("@QuantityUnit", UM1.Trim());
                            cmd1.Parameters.AddWithValue("@TotalValue", TotalAmt.Trim());
                            cmd1.Parameters.AddWithValue("@TotalWeight", TotalNetWeight.Trim());
                            cmd1.Parameters.AddWithValue("@WeightUnit", UM2.Trim());
                            cmd1.Parameters.AddWithValue("@CommodityCode", HSCode.Trim());
                            cmd1.Parameters.AddWithValue("@CountryOfOrigin", CountryOfOrigin.Trim());
                            cmd1.Parameters.AddWithValue("@ValueUnit", ValueUnit.Trim());
                            cmd1.Parameters.AddWithValue("@Addition", Addition.ToUpper().Trim());
                            cmd1.Parameters.AddWithValue("@PoSrNo", PoSrNo.Trim());
                            Result = cmd1.ExecuteNonQuery();
                            con.Close();
                        }
                    }
                    catch (Exception ex)
                    {
                        string Mes = ex.Message;
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return Result;
        }


    }
}