using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Data.SqlClient;
using System.IO;
using System.Net.Mail;
using System.Net.Mime;
using System.Reflection;
using System.Drawing;// to use Missing.Value
using Microsoft.Office;
using Microsoft.Office.Core;
using System.Runtime.InteropServices;
using Outlook = Microsoft.Office.Interop.Outlook;

namespace ImpexCube
{
    public partial class frmDocument : System.Web.UI.Page
    {
        VTS.ImpexCube.Business.InvoiceDetailsBL invBL = new VTS.ImpexCube.Business.InvoiceDetailsBL();
       // string strconn = (string)ConfigurationSettings.AppSettings["Connectionefrieght"];
        string strconn = (string)ConfigurationManager.AppSettings["ConnectionDashboard"];
        string JobNumber;
        string PartyName;
        string TransportType;
        string LCLFCl;
        string Dg;
        private string FilePath = "";
      //  BussinessAccess.BANote cc = new BussinessAccess.BANote();

        protected void Page_Load(object sender, EventArgs e)
        {
            
            if (!IsPostBack)
            {

                DateTime rightNow = DateTime.Now;
            
                //if (ddlJobNo.SelectedItem.Text != "~Select~")
                //{
                  
                //    SqlConnection con = new SqlConnection(strconn);
                //    con.Open();

                //    SqlDataAdapter cmd = new SqlDataAdapter("select  jobno,filetype,filename from tbl_documents where Jobno='" + jonno + "' ", con);
                //    DataSet ds = new DataSet();
                //    cmd.Fill(ds, "doc");

                //    gvdocument.DataSource = ds;
                //    gvdocument.DataBind();

                //    btnupdate.Visible = false;
                //    if (Session["Edit"] == "View")
                //    {
                //        Session["Edit"] = null;
                //        //ddfiletype.SelectedValue = (string)Session["filetype"];
                //        btnupdate.Visible = true;
                //        btnSave.Visible = false;
                //    }
                //    else
                //    {
                //        btnupdate.Visible = false;
                //        btnSave.Visible = true;
                //    }
                //}
                DropJobNo();
            }
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
        protected void btnSave_Click(object sender, EventArgs e)
        {
            int coun = 0;
            if (Session["JobNo"] != null)
            {
                string filename = Path.GetFileName(FileUpload1.FileName);
                if (filename != "")
                {

                    string path = "";
                    if (FileUpload1.HasFile)
                    {
                        string paths = AppDomain.CurrentDomain.BaseDirectory;
                        string pathdir = Path.Combine(paths, @"pdf\");
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
                        FilePath += path + ",";
                    }

                    Session["Attach"] = FilePath.TrimEnd(',');


                    if (FileUpload1.HasFile)
                    {
                        string jkj = "fsdf";
                        string fdf = "dfd";
                        string sd = fdf;
                    }

                    string name = FileUpload1.FileName;
                    filename = Path.GetFileName(FileUpload1.FileName);
                    if (FileUpload1.HasFile)
                    {

                        {
                            DataSet ds = new DataSet();

                            SqlConnection con = new SqlConnection(strconn);
                            con.Open();
                            string quer = "select * from tbl_documents where jobno= '" + ddlJobNo.SelectedValue + "' and filetype='" + ddfiletype.SelectedValue + "' ";
                            SqlDataAdapter cmd = new SqlDataAdapter(quer, con);
                            cmd.Fill(ds, "count");
                            if (ds.Tables["count"].Rows.Count != 0)
                            {
                                coun = ds.Tables["count"].Rows.Count;
                                coun = coun + 1;

                            }

                            string naame = FileUpload1.FileName;
                            string strFileName = Path.GetFileName(FileUpload1.PostedFile.FileName);
                            string strFileExtension = Path.GetExtension(FileUpload1.PostedFile.FileName);

                            int intlength = FileUpload1.PostedFile.ContentLength;
                            Byte[] byteData = new Byte[intlength];
                            FileUpload1.PostedFile.InputStream.Read(byteData, 0, intlength);

                            SqlConnection objConn = new SqlConnection(strconn);
                            SqlCommand objCmd = new SqlCommand("Documents", objConn);
                            objCmd.CommandType = CommandType.StoredProcedure;

                            SqlParameter pExtension1 = new SqlParameter("jobno", SqlDbType.VarChar);
                            pExtension1.Size = 35;
                            pExtension1.Direction = ParameterDirection.Input;
                            pExtension1.Value = ddlJobNo.SelectedValue;


                            SqlParameter pExtension2 = new SqlParameter("filename", SqlDbType.VarChar);
                            pExtension2.Size = 35;
                            pExtension2.Direction = ParameterDirection.Input;
                            pExtension2.Value = strFileName;

                            SqlParameter pExtension3 = new SqlParameter("extension", SqlDbType.VarChar);
                            pExtension3.Size = 8000;
                            pExtension3.Direction = ParameterDirection.Input;
                            pExtension3.Value = strFileExtension.ToLower();

                            SqlParameter pFileContent = new SqlParameter("FileContent", SqlDbType.VarBinary);
                            pFileContent.Size = byteData.Length;
                            pFileContent.Direction = ParameterDirection.Input;
                            pFileContent.Value = byteData;


                            SqlParameter pExtension5 = new SqlParameter("filetype", SqlDbType.VarChar);
                            pExtension5.Size = 35;
                            pExtension5.Direction = ParameterDirection.Input;
                            pExtension5.Value = ddfiletype.SelectedValue;

                            SqlParameter pfilecount = new SqlParameter("filecount", SqlDbType.VarChar);
                            pfilecount.Size = 35;
                            pfilecount.Direction = ParameterDirection.Input;
                            pfilecount.Value = coun;

                            SqlParameter pfilecount7 = new SqlParameter("DocumentGiven", SqlDbType.VarChar);
                            pfilecount7.Size = 250;
                            pfilecount7.Direction = ParameterDirection.Input;
                            pfilecount7.Value = txtDocumentGiven.Text;

                            SqlParameter pfilecount8 = new SqlParameter("DocumentName", SqlDbType.VarChar);
                            pfilecount8.Size = 250;
                            pfilecount8.Direction = ParameterDirection.Input;
                            pfilecount8.Value = ddFileCategory.SelectedValue;

                            objCmd.Parameters.Add(pExtension1);
                            objCmd.Parameters.Add(pExtension2);
                            objCmd.Parameters.Add(pExtension3);
                            objCmd.Parameters.Add(pFileContent);
                            objCmd.Parameters.Add(pExtension5);
                            objCmd.Parameters.Add(pfilecount7);
                            objCmd.Parameters.Add(pfilecount);
                            objCmd.Parameters.Add(pfilecount8);

                            objConn.Open();
                            objCmd.ExecuteNonQuery();
                            objConn.Close();
                            Session["JobNo"] = "";
                            Session["partyname"] = "";
                            Session["filetype"] = "";                            
                            gvdocument.DataBind();
                            
                            Response.Write("<script>alert('Successfully Saved')</script>");
                        }
                    }
                }
            }
            else
                Response.Write("<script>alert('Please select the Job No.')</script>");
        }

        protected void GetCommand(string Query)
        {

            SqlConnection conn = new SqlConnection(strconn);
            conn.Open();
            SqlCommand cmd = new SqlCommand(Query, conn);
            cmd.CommandText = Query;
            cmd.Connection = conn;
            int res = cmd.ExecuteNonQuery();
            conn.Close();
        }

        public void Commonexecute(string qry)
        {
            SqlConnection conn = new SqlConnection(strconn);
            conn.Open();
            SqlCommand cmd = new SqlCommand(qry, conn);
            cmd.ExecuteNonQuery();
            conn.Close();
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            Session["JobNo"] = null;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "JavaScript", "NewJobCreationDetails();", true);


        }
        public DataSet IsJobFinished(string JobNo)
        {

            DataSet ds = new DataSet();
            try
            {

                SqlConnection con = new SqlConnection(strconn);
                con.Open();

                SqlDataAdapter cmd = new SqlDataAdapter("select Distinct jobno,filetype,filename from tbl_documents where [jobno]='" + JobNo + "'", con);
                cmd.Fill(ds, "Isfinished");

            }
            catch (Exception ex)
            {
                string message = ex.Message;
            }
            return ds;
        }

        public DataSet JobStage()
        {

            DataSet ds = new DataSet();
            SqlConnection con = new SqlConnection(strconn);
            con.Open();
            SqlDataAdapter cmd = new SqlDataAdapter("select Distinct stagename,sno from tbl_jobstageExport order by sno", con);
            cmd.Fill(ds, "stage");
            return ds;
        }





        protected void btnView_Click(object sender, EventArgs e)
        {
            Session["JobNo"] = null;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "JavaScript", "ViewJobCreationDetails();", true);

        }
        protected void btnOpen_Click(object sender, EventArgs e)
        {
            SqlConnection objConn = new SqlConnection(strconn);
            SqlCommand objCmd = new SqlCommand("SELECT [FileName], [Extension], [FileContent] From tbl_form13 Where [jobno] = '" + ddlJobNo.SelectedValue + "'", objConn);
            objConn.Open();
            SqlDataReader dr = objCmd.ExecuteReader();
            dr.Read();
            string strFileName = dr.GetString(0);
            string strFileExtension = dr.GetString(1);
            Byte[] byteDoc = new Byte[(dr.GetBytes(2, 0, null, 0, int.MaxValue))];
            dr.GetBytes(2, 0, byteDoc, 0, byteDoc.Length);
            dr.Close();
            objConn.Close();
            //
            Response.Clear();
            Response.Buffer = true;
            if (strFileExtension == ".doc" || strFileExtension == ".docx")
            {
                Response.ContentType = "application/vnd.ms-word";
                Response.AddHeader("content-disposition", "attachment;filename=" + strFileName + "." + strFileExtension);
            }
            else if (strFileExtension == ".xls" || strFileExtension == ".xlsx")
            {
                Response.ContentType = "application/vnd.ms-excel";
                Response.AddHeader("content-disposition", "attachment;filename=" + strFileName + "." + strFileExtension);
            }
            else if (strFileExtension == ".pdf" || strFileExtension == ".PDF")
            {
                Response.ContentType = "application/pdf";
                Response.AddHeader("content-disposition", "attachment;filename=" + strFileName + "." + strFileExtension);
            }

            Response.Charset = "";
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.BinaryWrite(byteDoc);
            Response.Flush();
            Response.End();

        }

        protected void btnrfq_Click1(object sender, EventArgs e)
        {
            Response.Redirect("Bookingreq.aspx", false);
        }
        protected void btnquote_Click1(object sender, EventArgs e)
        {
            Response.Redirect("PlotPermission.aspx", false);
        }
        protected void Button1_Click(object sender, EventArgs e)
        {
            Response.Redirect("Exportstageupdate.aspx", false);
        }
        protected void Button2_Click(object sender, EventArgs e)
        {
            Response.Redirect("form13.aspx", false);
        }
        protected void Button3_Click(object sender, EventArgs e)
        {

        }
        protected void Button4_Click1(object sender, EventArgs e)
        {
            Response.Redirect("paymenttoliner.aspx", false);

        }
        protected void Button5_Click(object sender, EventArgs e)
        {
            Response.Redirect("AdditionalExpenses.aspx", false);
        }
        protected void Button6_Click(object sender, EventArgs e)
        {
            Response.Redirect("vesselupdate.aspx", false);
        }

        protected void btnview1_Click(object sender, EventArgs e)
        {
            DataRowView runningDr = null;

            if (Session["runningDr"] != null)
            {
                runningDr = (DataRowView)Session["runningDr"];
            }

            string path = "";
            string name1;
            if (FileUpload1.HasFile)
            {
                name1 = Path.GetFileName(FileUpload1.PostedFile.FileName);
                path = runningDr["pdfPath"].ToString() + Path.GetFileName(FileUpload1.PostedFile.FileName);


                if (!Directory.Exists(runningDr["pdfPath"].ToString()))
                {
                    Directory.CreateDirectory(runningDr["pdfPath"].ToString());
                }
                if (File.Exists(path))
                {
                    File.Delete(path);
                }
                FileUpload1.SaveAs(runningDr["pdfPath"].ToString() + Path.GetFileName(FileUpload1.PostedFile.FileName));
                FilePath += path + ",";
            }

            Session["Attach"] = FilePath.TrimEnd(',');



            if (FileUpload1.HasFile)
            {
                string jkj = "fsdf";
                string fdf = "dfd";
                string sd = fdf;
            }
            string name = FileUpload1.FileName;

            path = FilePath.TrimEnd(',');

            //frame1.Attributes.Add("src", "../pdf/" + name);

        }

        protected void gvdocument_SelectedIndexChanged(object sender, EventArgs e)
        {
            string name = gvdocument.SelectedRow.Cells[2].Text;
            SqlConnection objConn = new SqlConnection(strconn);
            SqlCommand objCmd = new SqlCommand("SELECT [FileName], [Extension], [FileContent] From tbl_documents  where Jobno='" + ddlJobNo.SelectedValue + "' and Filename='" + name + "'", objConn);
            objConn.Open();
            SqlDataReader dr = objCmd.ExecuteReader();
            dr.Read();
            string strFileName = dr.GetString(0);
            string strFileExtension = dr.GetString(1);
            Byte[] byteDoc = new Byte[(dr.GetBytes(2, 0, null, 0, int.MaxValue))];
            dr.GetBytes(2, 0, byteDoc, 0, byteDoc.Length);
            dr.Close();
            objConn.Close();
            //
            Response.Clear();
            Response.Buffer = true;
            if (strFileExtension == ".doc" || strFileExtension == ".docx" || strFileExtension == ".txt")
            {
                Response.ContentType = "application/vnd.ms-word";
                Response.AddHeader("content-disposition", "attachment;filename=" + strFileName + "." + strFileExtension);
            }
            else if (strFileExtension == ".xls" || strFileExtension == ".xlsx")
            {
                Response.ContentType = "application/vnd.ms-excel";
                Response.AddHeader("content-disposition", "attachment;filename=" + strFileName + "." + strFileExtension);
            }
            else if (strFileExtension == ".pdf" || strFileExtension == ".PDF")
            {
                Response.ContentType = "application/pdf";
                Response.AddHeader("content-disposition", "attachment;filename=" + strFileName + "." + strFileExtension);
            }
            else if (strFileExtension == ".png")
            {
                Response.ContentType = "image/png";
                Response.AddHeader("content-disposition", "attachment;filename=" + strFileName + "." + strFileExtension);
            }
            Response.Charset = "";
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.BinaryWrite(byteDoc);
            Response.Flush();
            Response.End();
        }

        protected void btnupdate_Click(object sender, EventArgs e)
        {
            int coun = 0;
            if (Session["JobNo"] != null)
            {
                string filename = Path.GetFileName(FileUpload1.FileName);
                if (filename != "")
                {
                    DataSet ds = new DataSet();

                    SqlConnection con = new SqlConnection(strconn);
                    con.Open();
                    string quer = "select * from tbl_documents where jobno= '" + Session["JobNo"] + "' and filetype='" + Session["filetype"] + "' and filecount='" + Session["count"] + "' ";
                    SqlDataAdapter cmd = new SqlDataAdapter(quer, con);
                    cmd.Fill(ds, "count");
                    if (ds.Tables["count"].Rows.Count != 0)
                    {
                        DataRowView row = ds.Tables["count"].DefaultView[0];
                        coun = Convert.ToInt16(row["filecount"]);
                        string query = "delete from tbl_documents where jobno ='" + Session["JobNo"] + "' and filetype='" + ddfiletype.SelectedValue + "' and filecount='" + Session["count"] + "' ";
                        GetCommand(query);
                    }
                    string path = "";
                    if (FileUpload1.HasFile)
                    {
                        string paths = AppDomain.CurrentDomain.BaseDirectory;
                        string pathdir = Path.Combine(paths, @"pdf\");
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
                        FilePath += path + ",";
                    }

                    Session["Attach"] = FilePath.TrimEnd(',');


                    if (FileUpload1.HasFile)
                    {
                        string jkj = "fsdf";
                        string fdf = "dfd";
                        string sd = fdf;
                    }

                    string name = FileUpload1.FileName;
                    filename = Path.GetFileName(FileUpload1.FileName);
                    if (FileUpload1.HasFile)
                    {

                        {

                            string naame = FileUpload1.FileName;

                            string strFileName = Path.GetFileName(FileUpload1.PostedFile.FileName);

                            string strFileExtension = Path.GetExtension(FileUpload1.PostedFile.FileName);
                            int intlength = FileUpload1.PostedFile.ContentLength;
                            Byte[] byteData = new Byte[intlength];
                            FileUpload1.PostedFile.InputStream.Read(byteData, 0, intlength);

                            SqlConnection objConn = new SqlConnection(strconn);
                            SqlCommand objCmd = new SqlCommand("Documents", objConn);
                            objCmd.CommandType = CommandType.StoredProcedure;

                            SqlParameter pExtension1 = new SqlParameter("jobno", SqlDbType.VarChar);
                            pExtension1.Size = 35;
                            pExtension1.Direction = ParameterDirection.Input;
                            pExtension1.Value = Session["JobNo"];


                            SqlParameter pExtension2 = new SqlParameter("filename", SqlDbType.VarChar);
                            pExtension2.Size = 35;
                            pExtension2.Direction = ParameterDirection.Input;
                            pExtension2.Value = strFileName;

                            SqlParameter pExtension3 = new SqlParameter("extension", SqlDbType.VarChar);
                            pExtension3.Size = 8000;
                            pExtension3.Direction = ParameterDirection.Input;
                            pExtension3.Value = strFileExtension.ToLower();

                            SqlParameter pFileContent = new SqlParameter("FileContent", SqlDbType.VarBinary);
                            pFileContent.Size = byteData.Length;
                            pFileContent.Direction = ParameterDirection.Input;
                            pFileContent.Value = byteData;


                            SqlParameter pExtension5 = new SqlParameter("filetype", SqlDbType.VarChar);
                            pExtension5.Size = 35;
                            pExtension5.Direction = ParameterDirection.Input;
                            pExtension5.Value = ddfiletype.SelectedValue;

                            SqlParameter pfilecount = new SqlParameter("filecount", SqlDbType.VarChar);
                            pfilecount.Size = 35;
                            pfilecount.Direction = ParameterDirection.Input;
                            pfilecount.Value = coun;

                            SqlParameter pfilecount7 = new SqlParameter("BranchId", SqlDbType.VarChar);
                            pfilecount7.Size = 35;
                            pfilecount7.Direction = ParameterDirection.Input;
                            pfilecount7.Value = (string)Session["Short"];

                            objCmd.Parameters.Add(pExtension1);
                            objCmd.Parameters.Add(pExtension2);
                            objCmd.Parameters.Add(pExtension3);
                            objCmd.Parameters.Add(pFileContent);
                            objCmd.Parameters.Add(pExtension5);
                            objCmd.Parameters.Add(pfilecount);
                            objCmd.Parameters.Add(pfilecount7);
                            objConn.Open();
                            objCmd.ExecuteNonQuery();
                            objConn.Close();

                            Response.Write("<script>alert('Successfully Updated')</script>");
                        }
                    }
                }
                SqlConnection co = new SqlConnection(strconn);
                SqlDataAdapter cmds = new SqlDataAdapter("select  jobno,filetype,filename from tbl_documents where Jobno='" + (string)Session["JobNo"] + "' ", co);
                DataSet dss = new DataSet();
                cmds.Fill(dss, "doc");

                gvdocument.DataSource = dss;
                gvdocument.DataBind();
            }
            else
                Response.Write("<script>alert('Please select the Job No.')</script>");
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

        protected void ddlJobNo_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlJobNo.SelectedItem.Text != "~Select~")
            {

                SqlConnection con = new SqlConnection(strconn);
                con.Open();

                SqlDataAdapter cmd = new SqlDataAdapter("select  jobno,filetype,filename from tbl_documents where Jobno='" + ddlJobNo.SelectedValue + "' ", con);
                DataSet ds = new DataSet();
                cmd.Fill(ds, "doc");

                gvdocument.DataSource = ds;
                gvdocument.DataBind();

                btnupdate.Visible = false;
                //if (Session["Edit"] == "View")
                //{
                //    Session["Edit"] = null;
                //    //ddfiletype.SelectedValue = (string)Session["filetype"];
                //    btnupdate.Visible = true;
                //    btnSave.Visible = false;
                //}
                //else
                //{
                //    btnupdate.Visible = false;
                //    btnSave.Visible = true;
                //}
            }
        }

    

      
    }

}