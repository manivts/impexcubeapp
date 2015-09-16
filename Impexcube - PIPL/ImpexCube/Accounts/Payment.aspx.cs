using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Xml.Linq;
using System.Net.Mail;
using System.Data.SqlClient;

namespace ImpexCube.Accounts
{
    public partial class Payment : System.Web.UI.Page
    {
        Master ms = new Master();
        string strconn = (string)ConfigurationManager.ConnectionStrings["Constr"].ConnectionString;
        //DataLayer.PageErrLog obj1 = new DataLayer.PageErrLog();

        #region Gobal variable declaration Only current form
        string Username = string.Empty;
        string form = "Payment Entry";
        string payment = "";
        string VchDt;
       
        string AcntCR;
        string Approved;
      
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (IsPostBack == false)
                {
                    Session["custCode"] = string.Empty;
                    Username = (string)Session["UserName"];
                    #region  Cash Payment New Mode
                    if (Request.QueryString["mode"] == "CashNew")
                    {
                        Session["Payment"] = "";
                        Session["VGUID"] = Guid.NewGuid().ToString();
                        AccountCash();
                        Session["custCode"] = "CP";
                        payment = "Cash Payment";
                        AccountDr();
                        VoucherNo();

                        string dates = DateTime.Now.ToString("dd'/'MM'/'yyyy");
                        txtVchDate.Text = dates;
                        btnUpdate.Visible = false;
                        btnSave.Visible = true;
                        ButtonPrint.Enabled = false;
                        LabelPayment.Text = "Cash Payment";
                        //rwGridViewDetails.Visible = false;
                    }
                    #endregion

                    #region  Bank Payment New Mode
                    else if (Request.QueryString["mode"] == "BankNew")
                    {
                        // UserPermission();
                        Session["Payment"] = "";
                        Session["VGUID"] = Guid.NewGuid().ToString();
                        AccountBank();

                        Session["custCode"] = "BP";
                        payment = "Bank Payment";
                        AccountDr();
                        VoucherNo();
                        string dates = DateTime.Now.ToString("dd'/'MM'/'yyyy");
                        txtVchDate.Text = dates;
                        txtChqNo.Enabled = true;
                        txtChqDate.Enabled = true;
                        lblChequeNo.Enabled = true;
                        lblChequeDate.Enabled = true;
                        btnUpdate.Visible = false;
                        btnSave.Visible = true;
                        ButtonPrint.Enabled = false;
                        LabelPayment.Text = "Bank Payment";

                        //rwGridViewDetails.Visible = false;
                    }
                    #endregion

                    #region  Cash Payment Edit Mode
                    else if (Request.QueryString["mode"] == "CashEdit")
                    {
                        // UserPermission();
                        Session["Payment"] = "";
                        Session["Mode"] = "Cash Payment";
                        AccountCash();
                        AccountDr();
                        EditPayment();
                        btnSave.Visible = false;
                        ButtonPrint.Visible = true;
                        LabelPayment.Text = "Cash Payment";
                    }
                    #endregion

                    #region  Bank Payment Edit Mode
                    else if (Request.QueryString["mode"] == "BankEdit")
                    {
                        // UserPermission();
                        Session["Payment"] = "";
                        Session["Mode"] = "Bank Payment";
                        AccountBank();
                        AccountDr();
                        EditPayment();
                        btnSave.Visible = false;
                        ButtonPrint.Visible = true;
                        LabelPayment.Text = "Bank Payment";
                    }
                    #endregion

                    #region  Bank Contra New Mode
                    else if (Request.QueryString["mode"] == "ContraNew")
                    {
                        Session["Payment"] = "";
                        Session["VGUID"] = Guid.NewGuid().ToString();
                        AccountContra();
                        Session["custCode"] = "CN";
                        payment = "Contra";
                        VoucherNo();
                        string dates = DateTime.Now.ToString("dd'/'MM'/'yyyy");
                        txtVchDate.Text = dates;
                        txtChqNo.Enabled = true;
                        txtChqDate.Enabled = true;
                        lblChequeNo.Enabled = true;
                        lblChequeDate.Enabled = true;
                        btnUpdate.Visible = false;
                        btnSave.Visible = true;
                        ButtonPrint.Enabled = false;
                        LabelPayment.Text = "Contra";
                    }
                    #endregion

                    #region  Bank Contra Edit Mode

                    else if (Request.QueryString["mode"] == "ContraEdit")
                    {
                        Session["Payment"] = "";
                        Session["Mode"] = "Contra";
                        AccountContra();
                        EditPayment();
                        btnSave.Visible = false;
                        ButtonPrint.Visible = true;
                        LabelPayment.Text = "Contra";
                    }
                    #endregion
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "javascript", "visiblehidden();", true);
                    //  ClientScript.RegisterStartupScript((typeof(GridView)), "JavaScript", "GetGridValue();", tr
                }
            }
            catch (Exception ex)
            {
            }
        }

        #region  Function User Permission
        private void UserPermission()
        {
            bool approvalform = Utility.ApprovalEntryForm(strconn, form, (string)Session["UserName"]);
            if (approvalform == true)
            {
                chkApproved.Visible = true;
            }
        }
        #endregion

        public void AccountCash()
        {
             DataSet ds = new DataSet();
             ds = ms.AccountCash();
             ddlAccountCr.DataSource = ds;
             ddlAccountCr.DataTextField = "AccountName";
             ddlAccountCr.DataValueField = "AccountName";
             ddlAccountCr.DataBind();
        }
        public void AccountBank()
        {
            DataSet ds = new DataSet();
            ds = ms.AccountBank();
            ddlAccountCr.DataSource = ds;
            ddlAccountCr.DataTextField = "AccountName";
            ddlAccountCr.DataValueField = "AccountName";
            ddlAccountCr.DataBind();
        }
        public void AccountContra()
        {
            DataSet ds = new DataSet();
            ds = ms.AccountContra();
            ddlAccountCr.DataSource = ds;
            ddlAccountCr.DataTextField = "AccountName";
            ddlAccountCr.DataValueField = "AccountName";
            ddlAccountCr.DataBind();

            ddlAccountDr.DataSource = ds;
            ddlAccountDr.DataTextField = "AccountName";
            ddlAccountDr.DataValueField = "AccountName";
            ddlAccountDr.DataBind();
        }

        private void AccountDr()
        {
            DataSet ds = new DataSet();
            ds = ms.Particulars();
            ddlAccountDr.DataSource = ds;
            ddlAccountDr.DataTextField = "AccountName";
            ddlAccountDr.DataValueField = "AccountName";
            ddlAccountDr.DataBind();
        }
        #region  Function Reference [Against Reference Bill]
        private void Reference()
        {
            string Query = "Select Distinct FundRequestNo from T_FundRequest where ImporterName='" + ddlAccountDr.SelectedItem.Text + "'";
            SqlConnection conn = new SqlConnection(strconn);
            conn.Open();
            SqlCommand cmd = new SqlCommand(Query, conn);
            SqlDataReader dr = cmd.ExecuteReader();
        }
        private void CostCenter()
        {
            string Query = "Select Distinct CostCenterName from M_CostCenter where AccountCode='" + ddlAccountDr.SelectedItem.Text + "'";
            SqlConnection conn = new SqlConnection(strconn);
            conn.Open();
            SqlCommand cmd = new SqlCommand(Query, conn);
            SqlDataReader dr = cmd.ExecuteReader();
        }
        #endregion

        private void VoucherNo()
        {
            txtVchNo.Text = Utility.GetNextAutoNo((string)Session["custCode"]);//, strconn, (string)Session["FYear"], (string)Session["BranchCode"]);
        }
        private void UpdateVoucherNo()
        {
            int slno = Utility.GetAddAutoNo((string)Session["custCode"]);//, strconn, (string)Session["FYear"], (string)Session["BranchCode"]);
            string qry1 = "Update M_AutoGenerate set KeyCode=" + slno + " where KeyName='" + (string)Session["custCode"] + "'";// And FYear='" + (string)Session["FYear"] + "' And BranchCode='" + (string)Session["BranchCode"] + "'";
            GetCommandIMP(qry1);
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (Convert.ToDouble(txtamt1.Text) == 0)
                {
                    string VchNO = txtVchNo.Text;
                    string dates = txtVchDate.Text;
                    string narration = txtNarration.Text;
                    string Apd;
                    if (chkApproved.Checked == true)
                    {
                        Apd = "1";
                    }
                    else
                    {
                        Apd = "0";
                    }
                    string qry = "Update T_PaymentMaster set Approved='" + Apd + "', Narration='" + narration + "'  Where VoucherNo='" + VchNO + "'";
                    GetCommandIMP(qry);

                    btnSave.Enabled = false;
                    ButtonPrint.Visible = true;
                    ButtonPrint.Enabled = true;
                    //mailsend();
                    ClassMsg.Show("Payment Saved Successfully");
                }
                else
                {
                    ClassMsg.Show("Sorry!!! Your Dr/Cr amount are Mismatch.");
                }
            }
            catch
            {
            }
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            string VchNO = txtVchNo.Text;
            string chdates = txtChqDate.Text;
            string dates = txtVchDate.Text;
            if (Convert.ToDouble(txtamt1.Text) == 0)
            {

                string[] DT = dates.Split('/');
                dates = DT[2] + "-" + DT[1] + "-" + DT[0];
                if (txtChqDate.Text != "")
                {
                    chdates = txtChqDate.Text;
                    string[] DT1 = chdates.Split('-');
                    chdates = DT1[2] + "-" + DT1[1] + "-" + DT1[0];
                }
                else
                {
                    chdates = string.Empty;
                }

                string AccCr = ddlAccountCr.SelectedItem.Text;
                string narration = txtNarration.Text;
                string Apd;
                if (chkApproved.Checked == true)
                {
                    Apd = "1";
                }
                else
                {
                    Apd = "0";
                }
                string SqlQuery = "Update T_PaymentMaster Set VoucherDate='" + dates + "',BranchCode='" + (string)Session["BranchCode"] + "',CompanyCode='" + (string)Session["CompanyCode"] + "'," +
                                  " AccountCrCode='" + AccCr + "',AccountCrName='" + AccCr + "',AmountCr='" + txtamt2.Text + "',Chq_No='" + txtChqNo.Text + "',Chq_Date='" + chdates + "',Narration='" + txtNarration.Text + "' ," +
                                  " Approved='" + Apd + "',Completed= 0 ,ModifiedBy='" + (string)Session["UserName"] + "',ModifiedDate='" + DateTime.Now + "', VGUID='" + (string)Session["VGUID"] + "' " +
                                  " where VoucherNo='" + txtVchNo.Text + "' ";
                GetCommandIMP(SqlQuery);
                ClassMsg.Show("Payment updated successfully");
            }
            else
            {
                ClassMsg.Show("Sorry!!! Your Dr/Cr amount are Mismatch.");
            }
        }

        private void EditPayment()
        {
            string sqlQuery = "Select VoucherNo,VoucherDate,AccountCrName,Narration,Approved,VGUID,Chq_No,Chq_Date,AmountCr from T_PaymentMaster where VoucherNo='" + (string)Session["PaymentDetails"] + "'";
            SqlConnection conn = new SqlConnection(strconn);
            conn.Open();
            SqlCommand cmd = new SqlCommand(sqlQuery, conn);
            SqlDataReader rv = cmd.ExecuteReader();
            if (rv.Read())
            {
                txtVchNo.Text = rv["VoucherNo"].ToString();
                VchDt = rv["VoucherDate"].ToString();
                AcntCR = rv["AccountCrName"].ToString();
                txtNarration.Text = rv["Narration"].ToString();
                Approved = rv["Approved"].ToString();
                Session["VGUID"] = rv["VGUID"].ToString();
                DateTime VDT = Convert.ToDateTime(VchDt);
                txtVchDate.Text = VDT.ToString("dd'/'MM'/'yyyy");
                ddlAccountCr.SelectedItem.Text = AcntCR;
                txtChqNo.Text = rv["Chq_No"].ToString();
                txtChqDate.Text = rv["Chq_Date"].ToString();
                txtamt2.Text = rv["AmountCr"].ToString();
                txtNarration.Text = rv["Narration"].ToString();
                Chk.Checked = true;
                if (Approved == "True")
                {
                    chkApproved.Checked = true;
                    btnUpdate.Visible = false;
                    btnAdd.Visible = false;
                    chkApproved.Enabled = false;
                 }
                else
                {
                    btnSave.Visible = false;
                    chkApproved.Checked = false;
                    btnUpdate.Visible = true;
                }
            }
            conn.Close();
            GridTemp();
        }   
     
        private void GetCommandIMP(string sqlQuery)
        {
            SqlConnection conn = new SqlConnection(strconn);
            conn.Open();
            SqlCommand cmd = new SqlCommand(sqlQuery, conn);
            SqlDataAdapter da = new SqlDataAdapter();
            cmd.CommandText = sqlQuery;
            cmd.Connection = conn;
            da.SelectCommand = cmd;
            int result = cmd.ExecuteNonQuery();
            conn.Close();
        }

        protected void btnExit_Click(object sender, EventArgs e)
        {
            if (Request.QueryString["mode"] == "CashNew" || Request.QueryString["mode"] == "CashEdit")
            {
                Response.Redirect("~/Accounts/PaymentDetails.aspx?mode=Cash");
            }
            else if (Request.QueryString["mode"] == "BankNew" || Request.QueryString["mode"] == "BankEdit")
            {
                Response.Redirect("~/Accounts/PaymentDetails.aspx?mode=Bank");
            }
            else if (Request.QueryString["mode"] == "ContraNew" || Request.QueryString["mode"] == "ContraEdit")
            {
                Response.Redirect("~/Accounts/PaymentDetails.aspx?mode=Contra");
            }
        }

        protected void btnNew_Click(object sender, EventArgs e)
        {
            NewPayment();
        }

        private void NewPayment()
        {
            if (Request.QueryString["mode"] == "CashNew" || Request.QueryString["mode"] == "CashEdit")
            {
                Response.Redirect("~/Accounts/Payment.aspx?mode=CashNew");
            }
            else if (Request.QueryString["mode"] == "BankNew" || Request.QueryString["mode"] == "BankEdit")
            {
                Response.Redirect("~/Accounts/Payment.aspx?mode=BankNew");
            }
            else if (Request.QueryString["mode"] == "ContraNew" || Request.QueryString["mode"] == "ContraEdit")
            {
                Response.Redirect("~/Accounts/Payment.aspx?mode=ContraNew");
            }    
        }

        public DataSet GetDataSQL(string SQLQuery)
        {
            SqlConnection conn = new SqlConnection(strconn);
            conn.Open();
            SqlDataAdapter da = new SqlDataAdapter(SQLQuery, conn);
            DataSet ds = new DataSet();
            da.Fill(ds, "SQLtable");
            conn.Close();
            return ds;
        }

        protected void btnAdd_Click1(object sender, EventArgs e)
        {
            string payid=(string)Session["PaymentId"];
            if (payid == string.Empty || payid == "" || payid==null)
                {
                    if (Chk.Checked == false)
                    {
                        SaveMaster();
                    }
                    SaveDetails();
                    btnSave.Enabled = true;
                }
                else
                {
                    UpdateDetails();
                }
        }

        private void SaveMaster()
        {
            try
            {
                string dates = txtVchDate.Text;
                string chdates = txtChqDate.Text;
                string Apd;
                string[] DT = dates.Split('/');
                dates = DT[2] + "-" + DT[1] + "-" + DT[0];

                if (txtChqDate.Text != "")
                {
                    chdates = txtChqDate.Text;
                    string[] dt1 = chdates.Split('/');
                    chdates = dt1[2] + "-" + dt1[1] + "-" + dt1[0];
                }
                else
                    chdates = string.Empty;
                if (chkApproved.Checked == true)
                {
                    Apd = "1";
                }
                else
                {
                    Apd = "0";
                }
                VoucherNo();
                UpdateVoucherNo();
                string Query = "Insert into T_PaymentMaster(VoucherNo,VoucherDate,BranchCode,CompanyCode,AccountCrCode,AccountCrName,AmountCr,Chq_No,Chq_Date,Narration,VCH_Type,VGUID,Approved,Completed,CreatedBy,CreatedDate,ModifiedBy,ModifiedDate,FinanceYear)" +
                 "values('" + txtVchNo.Text + "','" + dates + "','" + (string)Session["BranchCode"] + "','" + (string)Session["CompanyCode"] + "','" + ddlAccountCr.SelectedValue + "','" + ddlAccountCr.SelectedItem.Text + "'," +
                                   " " + txtamt2.Text + ",'" + txtChqNo.Text + "','" + chdates + "','" + txtNarration.Text + "','" + LabelPayment.Text + "','" + (string)Session["VGUID"] + "'," +
                                   " " + Apd + "," + 0 + ",'" + (string)Session["UserName"] + "','" + DateTime.Now + "','" + (string)Session["UserName"] + "','" + DateTime.Now + "','" + (string)Session["FYear"] + "')";
                GetCommandIMP(Query);
                Chk.Checked = true;
            }
            catch
            {
                Chk.Checked = true;
            }
        }

        private void SaveDetails()
        {
            string dates = txtVchDate.Text;
            string chdates = txtChqDate.Text;
            string[] DT = dates.Split('/');
            dates = DT[2] + "-" + DT[1] + "-" + DT[0];
            string Query = "Insert into T_PaymentDetails(VoucherNo,VoucherDate,AccountDrCode,AccountDrName,AmountDr,MethodOfAdj,Reference,CostCenter,CreatedBy,CreatedDate,ModifiedBy,ModifiedDate,VGUID)" +
                            "values('" + txtVchNo.Text + "','" + dates + "','" + ddlAccountDr.SelectedValue + "','" + ddlAccountDr.SelectedItem.Text + "'," + txtamt1.Text + ", " +
                            " '" + ddlMethod.SelectedValue + "','" + txtDetails.Text + "','" + txtCost.Text + "','" + (string)Session["UserName"] + "','" + DateTime.Now + "','" + (string)Session["UserName"] + "','" + DateTime.Now + "','" + (string)Session["VGUID"] + "')";
            GetCommandIMP(Query);
            GridTemp();
        }

        private void UpdateDetails()
        {
            string Query = "Update T_PaymentDetails Set AccountDrCode='" + ddlAccountDr.SelectedValue + "',AccountDrName='" + ddlAccountDr.SelectedItem.Text + "',AmountDr='" + txtamt1.Text + "',MethodOfAdj='" + ddlMethod.SelectedValue + "',Reference='" + txtDetails.Text + "',CostCenter='" + txtCost.Text + "',ModifiedBy='" + (string)Session["UserName"] + "',ModifiedDate='" + DateTime.Now + "' Where TransId='" + (string)Session["PaymentId"] + "'";
            GetCommandIMP(Query);
            Session["PaymentId"] = "";
            GridTemp();
        }
        private void GridTemp()
        {
            string query = string.Empty;
            query = "SELECT TransId,AccountDrName, MethodOfAdj, CostCenter, Reference, AmountDr FROM T_PaymentDetails where VGUID='" + (string)Session["VGUID"] + "'";//VoucherNo='" + txtVchNo.Text + "'";
            DataSet ds = GetDataSQL(query);
            if (ds.Tables["SQLtable"].Rows.Count != 0)
            {
                rwGridViewDetails.Visible = true;
                gvPaymentDetails.DataSource = ds;
                gvPaymentDetails.DataBind();
                TallyCrAmount();
            }
            ////else
            ////{
            ////    gvPaymentDetails.DataSource = null;
            ////    gvPaymentDetails.DataBind();
            ////    rwGridViewDetails.Visible = false;
            ////}
        }

        private void TallyCrAmount()
        {
                double Totbalamt = Convert.ToDouble(txtamt1.Text);
                if (Totbalamt == 0)
                {
                    ddlAccountDr.Enabled = false;
                    ddlMethod.Enabled = false;
                    txtDetails.Enabled = false;
                    txtCost.Enabled = false;
                    txtamt1.Enabled = false;
                    btnAdd.Enabled = false;
                    btnSave.Enabled = true;
                }
                else
                {
                    ddlAccountDr.Enabled = true;
                    ddlMethod.Enabled = true;
                    txtDetails.Enabled = true;
                    txtCost.Enabled = true;
                    txtamt1.Enabled = true;
                    btnAdd.Enabled = true;
                    btnSave.Enabled = false;
                }
           // }
        }

        private void Clear()
        {
            txtamt1.Text = string.Empty;
            txtamt2.Text = string.Empty;
        }

        protected void ButtonPrint_Click(object sender, EventArgs e)
        {
            Session["VchNo"] = txtVchNo.Text;
            Response.Redirect("frmPrint_Payment.aspx");    
        }              
   
        protected void gvPaymentDetails_SelectedIndexChanged(object sender, EventArgs e)
        {
            string chq = string.Empty;
            string payment = gvPaymentDetails.SelectedRow.Cells[1].Text;
            Session["PaymentId"] = payment;
            Session["Payment"] = "View";
            string query = "SELECT TransId,AccountDrName, MethodOfAdj, CostCenter, Reference, AmountDr FROM T_PaymentDetails WHERE TransId = '" + payment + "'";
                SqlConnection conn = new SqlConnection(strconn);
                conn.Open();
                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataReader prw = cmd.ExecuteReader();
                if (prw.Read())
                {
                    ddlAccountDr.SelectedItem.Text = prw["AccountDrName"].ToString();
                    ddlMethod.SelectedValue = prw["MethodOfAdj"].ToString();
                    txtCost.Text = prw["CostCenter"].ToString();
                    txtDetails.Text = prw["Reference"].ToString();
                    txtamt1.Text = prw["AmountDr"].ToString();
                }
               conn.Close();
                TallyCrAmount();
        }
        protected void ddlAccountDr_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlAccountDr.SelectedValue != ddlAccountCr.SelectedValue)
            {
                Session["AccountDr"] = ddlAccountDr.SelectedItem.Text;
                Reference();
                CostCenter();
            }
            else
            {
                ClassMsg.Show("Dr. Name and Cr.Name are Same");
            }
        }

        public static string datereplace(string date)
        {
            string[] dat = date.Split(' ');
            string dats = dat[0].ToString();
            return dats;
        }

        public void mailsend()
        {
            try
            {
                string Msg = "";
                Msg += "<table width=100%  color=red><tr><td>";


                SqlConnection con = new SqlConnection(strconn);
                con.Open();
                SqlDataAdapter sd1b = new SqlDataAdapter("Select  Acc_DrCode,Amount,VoucherDate,Approved from T_PaymentMaster where VoucherNo='" + txtVchNo.Text + "'", con);
                DataSet ds1b = new DataSet();
                sd1b.Fill(ds1b, "data");
                con.Close();
                if (ds1b.Tables["data"].Rows.Count != 0)
                {
                    DataRowView dd = ds1b.Tables["data"].DefaultView[0];
                    string Accdrcode = dd["Acc_DrCode"].ToString();
                    string Amount = dd["Amount"].ToString();
                    string date = dd["VoucherDate"].ToString();
                    string Voucherdate = datereplace(date);
                    string Approved = dd["Approved"].ToString();
                    string app = "";
                    if (Approved == "True")
                    {
                        app = "With Approve";
                    }
                    else
                    {
                        app = "Without Approve";
                    }
                    Msg += "<html><head><title>Messages :- </title></head><body><table width='100%' bordercolor='#ff0000'><tr><td><font align='center' color='000099'>Dear sir,</td></tr><tr><td><br /><font align='center' color='000099'>Mr/Mrs, '" + Accdrcode + "' Has Given the  COST To'" + ddlAccountCr.SelectedItem.Text + "' Please see the Below Details <table border='0' cellpadding='20%' width='70%'><tr bgcolor='d8e4f8'><td><p><font color='006600'>Accdrcode </td><td><b><font color='red'>" + Accdrcode + "</td></tr><tr bgcolor='d8e4f8'><td><p><font color='006600'>Amount </td><td><b><font color='blue'>" + Amount + "</td></tr><tr bgcolor='d8e4f8'><td><p><font color='006600'>Voucherdate </td><td><b><font color='blue'>" + Voucherdate + "</td></tr><tr bgcolor='d8e4f8'><td><p><font color='006600'>Approved </td><td><b><font color='blue'>" + app + "</td></tr></table><br></td></tr></table></body></html>";
                }
               // ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Mail Successfully Send')", true);

                string From = "";
                string Tomail = "";
                SqlConnection co = new SqlConnection(strconn);
                co.Open();
                SqlCommand cmde = new SqlCommand("select EmailID from M_AccountMaster where AccountName='" + ddlAccountDr.SelectedItem.Text + "'", co);
                SqlDataReader sde = cmde.ExecuteReader();
                if (sde.Read() == true)
                {
                    Tomail = sde["EmailID"].ToString();
                }
                co.Close();
                SqlConnection cons = new SqlConnection(strconn);
                cons.Open();
                SqlCommand cmd = new SqlCommand("select EmailId from tbl_User where EmployeeName='" + (string)Session["UserName"] + "'", cons);
                SqlDataReader sd = cmd.ExecuteReader();
                if (sd.Read() == true)
                {
                    From = sd["EmailId"].ToString();
                }
                cons.Close();
                MailMessage Message = new MailMessage();
                Message.From = new MailAddress(From);

                Message.To.Add(Tomail);

                Message.Body = Msg;
                string subjects = "Test";
                Message.Subject = (subjects);
                
                Message.IsBodyHtml = true;
                SmtpClient mySmtpClient = new SmtpClient("smtpauth.translink.in", 25);
                
                mySmtpClient.Credentials = new System.Net.NetworkCredential("mktg01@translink.in", "Bala123");
                mySmtpClient.Send(Message);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Mail Successfully Send')", true);
            }
               catch
               {
                }

        }

        protected void gvPaymentDetails_DataBound(object sender, EventArgs e)
        {
            double TotalAmt = 0;
            int i = 0;
            foreach (GridViewRow gv in gvPaymentDetails.Rows)
            {
                string amt = gvPaymentDetails.Rows[i].Cells[6].Text;
                TotalAmt = TotalAmt + Convert.ToDouble(amt);
                i++;
            }
            if (gvPaymentDetails.Rows.Count != 0)
            {
                gvPaymentDetails.FooterRow.Cells[4].Text = "Total Amount ";
                gvPaymentDetails.FooterRow.Cells[5].Text = Convert.ToString(TotalAmt);
                double dramt = Convert.ToDouble(txtamt2.Text);
                txtamt1.Text = Convert.ToString(dramt - TotalAmt);
               // double BalanceAmount = 0;
                //gvPaymentDetails.FooterRow.Cells[6].Text = "Balance Amount ";
                //txtamt1.Text=Convert.ToString(BalanceAmount);
                ////Session["rowcount"] = gvPaymentDetails.Rows.Count;
            }
          
        }

        protected void btnPrevious_Click(object sender, EventArgs e)
        {
            gvPaymentDetails.DataBind();
            string ab = txtVchNo.Text;
            string[] c = ab.Split('/');
            int d = Convert.ToInt32(c[3].ToString());
            d = d - 1;
            string value = c[0] + "/" + c[1] + "/" + c[2] + "/" + d;
            Session["PaymentDetails"] = value;
            txtVchNo.Text = value;
            EditPayment();
            
        }

        protected void btnNext_Click(object sender, EventArgs e)
        {
            string ab = txtVchNo.Text;
            string[] c = ab.Split('/');
            int d = Convert.ToInt32(c[3].ToString());
            int no = Utility.GetNextNo(c[0]);//, strconn, c[2], c[1]);
            int d1 = d + 1;
            string value = c[0] + "/" + c[1] + "/" + c[2] + "/" + d1;
            txtVchNo.Text = value;
            if (no <= d)
            {
                
                Clear();
                gvPaymentDetails.DataBind();
                ClassMsg.Show("No Data Found");
            }
            else
            {
                Session["PaymentDetails"] = value;
                EditPayment();
            }
        }

              
        
                       
    }
}
