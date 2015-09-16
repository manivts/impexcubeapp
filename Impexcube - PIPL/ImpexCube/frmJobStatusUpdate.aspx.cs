using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Data;
using System.Net.Mail;
using System.IO;
using System.Net.Mime;
using System.Web.UI.WebControls.WebParts;
using System.Data.SqlClient;
using System.Configuration;

namespace ImpexCube
{
    public partial class frmJobStatusUpdate : System.Web.UI.Page
    {
        string strconn = (string)ConfigurationManager.ConnectionStrings["Constr"].ConnectionString;
        VTS.ImpexCube.Business.JobStageUpdateBL objJobStage = new VTS.ImpexCube.Business.JobStageUpdateBL();
        string FilePath = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                myfile.Value = null;
                BindJobDetails();
                DropStageDetails();
                btnUpdate.Visible = false;
                txtStatusDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                if ((string)Session["Grade"] == "A")
                {
                    //lkSettings.Visible = true;
                }
                else
                {
                    //lkSettings.Visible = false;
                }
            }
        }

        private void DropStageDetails()
        {
            DataSet ds = objJobStage.SelectStage();
            if (ds.Tables["stage"].Rows.Count != 0)
            {
                ddlJobStages.DataSource = ds;
                ddlJobStages.DataTextField = "StageName";
                ddlJobStages.DataValueField = "StageId";
                ddlJobStages.DataBind();
                ddlJobStages.Items.Insert(0, new ListItem("-Select-", "0"));
            }
        }

        private void BindJobDetails()
        {
            DataSet ds = objJobStage.GetJobStatusList((string)Session["JobStatus"]);
            if (ds.Tables["status"].Rows.Count != 0)
            {
                DataRowView dr = ds.Tables["status"].DefaultView[0];
                txtJobNo.Text = dr["Job"].ToString();
                txtJobDate.Text = dr["Job Date"].ToString();
                txtImporter.Text = dr["Importer"].ToString();
                //txtBEType.Text = dr["BE Type"].ToString();
                txtShipmentType.Text = dr["Shipment Type"].ToString();
               // txtInvoice.Text = dr["InvDtl"].ToString();
                txtBENo.Text = dr["BE No"].ToString();
                txtBEDate.Text = dr["BE Date"].ToString();

                DataSet gds = objJobStage.GetJobStatusList((string)Session["JobStatus"]);
                if (gds.Tables["status"].Rows.Count != 0)
                {
                    DataRowView gdr = gds.Tables["status"].DefaultView[0];
                    string status = gdr["Status"].ToString();
                    string stage = gdr["Stage"].ToString();
                    if (stage != "" && status != "")
                    {
                        gvJobStageStatus.DataSource = gds;
                        gvJobStageStatus.DataBind();
                    }
                    else
                    {
                        gvJobStageStatus.DataSource = null;
                        gvJobStageStatus.DataBind();
                    }
                }
                else
                {
                    gvJobStageStatus.DataSource = null;
                    gvJobStageStatus.DataBind();
                }
            }
        }

        protected void btnMail_Click(object sender, ImageClickEventArgs e)
        {
            if (ddlJobStages.SelectedItem.Text != "-Select-" && ddlJobStageStatus.SelectedItem.Text != "")
            {
                if (ddlJobStageStatus.SelectedItem.Text != "-Select-")
                {
                    if ((string)Session["mode"] == "Update")
                    {
                        update();

                    }
                    else
                    {
                        Add();

                    }

                    Session["mode"] = null;
                    Session["JobNo"] = txtJobNo.Text;
                    Session["Party"] = txtImporter.Text;
                    Session["Stage"] = ddlJobStages.SelectedItem.Text;
                    Session["Status"] = ddlJobStageStatus.SelectedItem.Text;
                    Session["StatusDate"] = txtStatusDate.Text;
                    Session["Remarks"] = txtRemarks.Text;
                    txtSubject.Text = (string)Session["JobNo"] + "-" + (string)Session["Party"];
                    txtComment.Text += "Job No          :" + (string)Session["JobNo"] + Environment.NewLine;
                    txtComment.Text += "Party           :" + (string)Session["Party"] + Environment.NewLine;
                    txtComment.Text += "Stage           :" + (string)Session["Stage"] + Environment.NewLine;
                    txtComment.Text += "Status          :" + (string)Session["Status"] + Environment.NewLine;
                    txtComment.Text += "Remarks         :" + (string)Session["Remarks"] + Environment.NewLine;
                    txtComment.Text += "Status Date     :" + (string)Session["StatusDate"] + Environment.NewLine;
                    btnSend.Enabled = true;
                    clear();
                    //ScriptManager.RegisterStartupScript(this, this.GetType(), "popup", "window.open('frmMailingJobUpdate.aspx','_blank','menubar=no, scrollbars=yes, toolbar=no, location=no, resizable=yes, left=20, top=20');", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Please select the status');", true);
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Please select the stage and status');", true);
            }
        }

        public void clear()
        {
            ddlJobStages.SelectedValue = "0";
            ddlJobStageStatus.SelectedValue = "0";
            txtRemarks.Text = "";
            txtStatusDate.Text = "";
        }

        public void MailClear()
        {
            txtFrom.Text = "";
            txtTo.Text = "";
            txtCC.Text = "";
            txtSubject.Text = "";
            txtAttach.Text = "";
            txtComment.Text = "";
        }


        protected void ddlJobStages_SelectedIndexChanged(object sender, EventArgs e)
        {
            int stageid = Convert.ToInt32(ddlJobStages.SelectedValue);
            DataSet ds = objJobStage.SelectStageStatus(stageid);
            if (ds.Tables["status"].Rows.Count != 0)
            {
                ddlJobStageStatus.DataSource = ds;
                ddlJobStageStatus.DataTextField = "StatusName";
                ddlJobStageStatus.DataValueField = "Id";
                ddlJobStageStatus.DataBind();
                ddlJobStageStatus.Items.Insert(0, new ListItem("-Select-", "0"));
            }
            else
            {
                ddlJobStageStatus.Items.Clear();
                ddlJobStageStatus.DataSource = null;
                ddlJobStageStatus.DataBind();
            }
        }

        protected void btnAdd_Click(object sender, ImageClickEventArgs e)
        {
            Add();
            clear();
            // Response.Redirect("frmJobStageStatusUpdate.aspx");
        }

        public void Add()
        {
            string modified = string.Empty;
            int result = new int();
            try
            {
                if (ddlJobStages.SelectedItem.Text != "-Select-" && ddlJobStageStatus.SelectedItem.Text != "")
                {
                    if (ddlJobStageStatus.SelectedItem.Text != "-Select-")
                    {
                        //Check JobNo In JobCreation Table.
                        DataSet dsbe = objJobStage.SelectJobNoBeNo(txtJobNo.Text);
                        if (dsbe.Tables["BENo"].Rows.Count != 0)
                        {
                            //Update BENO and Date.
                            int updatebe = objJobStage.UpdateBEDeatils(txtJobNo.Text, txtBENo.Text, txtBEDate.Text);
                        }
                        //DataSet ds = objJobStage.GetStageStatusDetails(txtJobNo.Text, ddlJobStages.SelectedItem.Text, ddlJobStageStatus.SelectedItem.Text);
                        //if (ds.Tables["status"].Rows.Count != 0)
                        //{
                        //    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Job Stage and Status has been already saved');", true);
                        //}
                        //else
                        //{

                        //Check Job Present or not in T_JobStageUpdate Table
                        DataSet jds = objJobStage.SelectJobDetails(txtJobNo.Text);
                        if (jds.Tables["status"].Rows.Count == 0)
                        {
                            modified = "1";
                            string Importer = txtImporter.Text.Replace(",", "").Replace("'", "");
                            result = objJobStage.InsertJodStageStatus(txtJobNo.Text, Importer, ddlJobStages.SelectedItem.Text,
                                ddlJobStageStatus.SelectedItem.Text, txtStatusDate.Text, txtRemarks.Text, modified);

                        }
                        else
                        {
                            string change = "0";
                            int modify = objJobStage.ModifyJobStageDetails(txtJobNo.Text, change);
                            modified = "1";
                            result = objJobStage.InsertJodStageStatus(txtJobNo.Text, txtImporter.Text, ddlJobStages.SelectedItem.Text,
                                ddlJobStageStatus.SelectedItem.Text, txtStatusDate.Text, txtRemarks.Text, modified);
                        }
                        //  }
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Please select the status');", true);
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Please select the stage and status');", true);
                }
            }
            catch (Exception ex)
            {
                string msg = ex.Message.ToString();
            }
            if (result == 1)
            {
                GridJobDetails();
                Session["JobStatus"] = txtJobNo.Text;
                //Response.Redirect("frmJobStageStatusUpdate.aspx");
            }
        }



        private void GridJobDetails()
        {
            DataSet gds = objJobStage.GetJobStatusList(txtJobNo.Text);
            if (gds.Tables["status"].Rows.Count != 0)
            {
                gvJobStageStatus.DataSource = gds;
                gvJobStageStatus.DataBind();
            }
            else
            {
                gvJobStageStatus.DataSource = null;
                gvJobStageStatus.DataBind();
            }
        }

        protected void gvJobStageStatus_SelectedIndexChanged(object sender, EventArgs e)
        {
            Session["mode"] = "Update";
            btnAdd.Visible = false;
            btnUpdate.Visible = true;
            Session["Id"] = Convert.ToInt32(gvJobStageStatus.SelectedRow.Cells[1].Text);
            ddlJobStages.SelectedIndex = ddlJobStages.Items.IndexOf(ddlJobStages.Items.FindByText(gvJobStageStatus.SelectedRow.Cells[2].Text));
            string stages = ddlJobStages.SelectedItem.Text;
            GetStageId(stages);
            ddlJobStages.SelectedValue = (string)Session["stageid"];
            int stageid = Convert.ToInt32(Session["stageid"]);
            DataSet ds = objJobStage.SelectStageStatus(stageid);
            if (ds.Tables["status"].Rows.Count != 0)
            {
                ddlJobStageStatus.DataSource = ds;
                ddlJobStageStatus.DataTextField = "StatusName";
                ddlJobStageStatus.DataValueField = "Id";
                ddlJobStageStatus.DataBind();
                ddlJobStageStatus.Items.Insert(0, new ListItem("-Select-", "0"));
            }
            ddlJobStageStatus.SelectedIndex = ddlJobStageStatus.Items.IndexOf(ddlJobStageStatus.Items.FindByText(gvJobStageStatus.SelectedRow.Cells[3].Text));
            string remarks = gvJobStageStatus.SelectedRow.Cells[4].Text;
            if (remarks == "" || remarks == "&nbsp;")
            {
                txtRemarks.Text = string.Empty;
            }
            else
            {
                txtRemarks.Text = remarks;
            }
            txtStatusDate.Text = gvJobStageStatus.SelectedRow.Cells[5].Text;
        }

        private void GetStageId(string stages)
        {
            DataSet ds = objJobStage.GetStageId(stages);
            if (ds.Tables["stage"].Rows.Count != 0)
            {
                DataRowView dr = ds.Tables["stage"].DefaultView[0];
                Session["stageid"] = dr["StageId"].ToString();
            }
        }

        protected void btnUpdate_Click(object sender, ImageClickEventArgs e)
        {
            update();
            clear();
            // Response.Redirect("frmJobStageStatusUpdate.aspx");
        }

        public void update()
        {
            int id = Convert.ToInt32(Session["Id"]);
            int result = new int();
            try
            {
                if (ddlJobStages.SelectedItem.Text != "-Select-" && ddlJobStageStatus.SelectedItem.Text != "")
                {
                    if (ddlJobStageStatus.SelectedItem.Text != "-Select-")
                    {
                        int updatebe = objJobStage.UpdateBEDeatils(txtJobNo.Text, txtBENo.Text, txtBEDate.Text);
                        result = objJobStage.UpdateJodStageStatus(id, txtJobNo.Text, txtImporter.Text, ddlJobStages.SelectedItem.Text,
                            ddlJobStageStatus.SelectedItem.Text, txtStatusDate.Text, txtRemarks.Text);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Please select the status');", true);
                    }
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Please select the stage and status');", true);
                }
            }
            catch (Exception ex)
            {
                string msg = ex.Message.ToString();
            }
            if (result == 1)
            {
                GridJobDetails();
                Session["JobStatus"] = txtJobNo.Text;
                //Response.Redirect("frmJobStageStatusUpdate.aspx");
            }
        }

        protected void btnClose_Click(object sender, EventArgs e)
        {
            Page.RegisterStartupScript("as", "<script language='javascript'>window.close();</script>");
            //Response.Redirect("frmStageListView.aspx");
        }

        protected void btnSend_Click(object sender, EventArgs e)
        {
            string jobno = string.Empty;
            string jobstage = string.Empty;
            string jobstatus = string.Empty;
            string date = DateTime.Now.ToString("dd/MM/yyyy");

            if (txtFrom.Text != "" && txtTo.Text != "")
            {
                if (fuAttach.HasFile)
                {
                    myfile.Value = null;
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "redirect", "alert('Please attach the file uploaded');", true);
                }
                else
                {

                    int result = objJobStage.InsertJobStatusMail(txtJobNo.Text, ddlJobStages.SelectedItem.Text, ddlJobStageStatus.SelectedItem.Text, txtFrom.Text, txtTo.Text, txtCC.Text, txtSubject.Text,
                        txtComment.Text, txtAttach.Text, (string)Session["USER-NAME"], date, (string)Session["USER-NAME"], date);
                    if (result == 1)
                    {

                        string To = txtTo.Text;
                        string subject = txtSubject.Text;
                        string cc = txtCC.Text;
                        string body = txtComment.Text;
                        hdnTo.Value = To;
                        hdnSubject.Value = subject;
                        hdnCC.Value = cc;
                        hdnComment.Value = body;

                        Page.ClientScript.RegisterStartupScript(this.GetType(), "Job Status Mail", "SendAttach();", true);
                        txtFrom.Text = txtTo.Text = txtCC.Text = txtSubject.Text = txtComment.Text = txtAttach.Text = string.Empty;
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "redirect", "alert('Mail Sent Successfully');", true);
                    }
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "redirect", "alert('Please fill from and to address');", true);
            }
        }

        private void SendMail()
        {
             SqlConnection conn = new SqlConnection(strconn);
            string Query = "select * from M_MailDetails";
            SqlDataAdapter da = new SqlDataAdapter(Query, conn);
            DataSet ds = new DataSet();
            da.Fill(ds, "mail");
            if (ds.Tables["mail"].Rows.Count != 0)
            {
                DataRowView row = ds.Tables["mail"].DefaultView[0];

                string smtp = row["SMTP"].ToString();
                int PortCode = Convert.ToInt16(row["PortCode"]);
                string CredentialsID = row["CredentialsID"].ToString();
                string Password = row["Password"].ToString();


                string from = txtFrom.Text;
                string To = txtTo.Text;
                string subject = txtSubject.Text;
                string cc = txtCC.Text;
                string body = txtComment.Text;
                string mAttached = "";

                MailMessage Message = new MailMessage();
                Message.From = new MailAddress(from);
                Message.Subject = subject;
                Message.Body = body.ToString();
                Message.IsBodyHtml = false;
                if (To != "" && To != string.Empty)
                {
                    Message.To.Add(To);
                    if (cc != "" && cc != string.Empty)
                    {
                        Message.CC.Add(cc);
                    }
                }
                else if (To == "" && To == string.Empty)
                {
                    Message.To.Add("e.vivek@vts.in");
                }

                mAttached = Convert.ToString(Session["Attach"]);

                if (mAttached != "" | mAttached != string.Empty)
                {
                    string[] strAtt = mAttached.Split(',');
                    foreach (string strThisMatt in strAtt)
                    {
                        strThisMatt.Trim();
                        Attachment atts = new Attachment(strThisMatt);
                        Message.Attachments.Add(atts);
                    }
                }

                SmtpClient mySmtpClient = new SmtpClient(smtp, PortCode);

                mySmtpClient.UseDefaultCredentials = false;
                mySmtpClient.Credentials = new System.Net.NetworkCredential(CredentialsID, Password);
                mySmtpClient.EnableSsl = true;
                mySmtpClient.Send(Message);
            }

            else
            {
                Response.Write("<script>alert('Please Give CredentialsID Details')</script>");
            }
            //CSharp.OutlookMail oMail = new CSharp.OutlookMail();
            //Console.WriteLine("CSharp.OutlookMail instantiated");
            //oMail.addToOutBox(txtTo.Text, txtSubject.Text, txtComment.Text, txtCC.Text, (string)Session["Attach"]);
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            txtFrom.Text = txtTo.Text = txtCC.Text = txtSubject.Text = txtComment.Text = string.Empty;
            btnSend.Enabled = false;
        }

        protected void btnAttach_Click(object sender, EventArgs e)
        {
            string path = "";
            string serverPath = "";


            string strAttach = "";
            string strShow = "";
            if (fuAttach.HasFile)
            {
                string jobno = txtJobNo.Text;
                //string[] no = txtJobNo.Text.Split('/');

                //string jobno = no[1];                

                serverPath = Server.MapPath("~") + "Uploads" + "\\" + jobno;
                path = serverPath + "\\" + Path.GetFileName(fuAttach.PostedFile.FileName);
                //Directory.Delete(MapPath(".") + "\\" + txtDir.Text, chkRecurse.Checked);
                if (!Directory.Exists(serverPath))
                {
                    Directory.CreateDirectory(serverPath);
                }
                if (File.Exists(path))
                {
                    File.Delete(path);
                }
                fuAttach.SaveAs(serverPath + "\\" + Path.GetFileName(fuAttach.PostedFile.FileName));
                FilePath += path + ",";
            }

            if (fuAttach.FileName != "")
            {
                strAttach = serverPath + "\\" + fuAttach.FileName;
                strShow = fuAttach.FileName;

                txtAttach.Text = FilePath.TrimEnd(',');
                Session["Attach"] = FilePath.TrimEnd(',');
                myfile.Value = FilePath.TrimEnd(',');
            }
            else if (fuAttach.FileName == "")
            {
                Session["Attach"] = string.Empty;
            }
        }
    }
}