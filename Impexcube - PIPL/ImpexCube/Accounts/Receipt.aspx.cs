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
using System.Data.SqlClient;
using System.Reflection;
using System.Globalization;

namespace ImpexCube.Accounts
{
    public partial class Receipt : System.Web.UI.Page
    {
        Master ms = new Master();
        string strconn = (string)ConfigurationManager.ConnectionStrings["Constr"].ConnectionString;

        #region
        string Username = string.Empty;
        string form = "Receipt Entry";
        string VchNo;
        string VchDt;
        string AcntDR;
        string narration;
        string remarks;
        string CD;
        string CB;
        string CC;
        string Branch;
        string Approved;
        string VchType;
        string custCode = "RC";
        string mode = "New";
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack == false)
            {
                Username = (string)Session["UserName"];
                if (mode == Request.QueryString["mode"])
                {
                   // UserVisibleControls(Username);
                    Session["Receipt"] = "";
                    Session["VGUID"] = Guid.NewGuid().ToString();
                    AccountCr();
                    AccountDr();
                    VoucherNo();
                    string dates = DateTime.Now.ToString("dd'/'MM'/'yyyy");
                    txtVchDate.Text = dates;
                    btnUpdate.Visible = false;
                    btnSave.Visible = true;
                    btnPrint.Enabled = false;
                }
                else
                {
                   // UserVisibleControls(Username);
                    Session["Receipt"] = "";
                    AccountCr();
                    AccountDr();
                    EditReceipt();
                    btnUpdate.Visible = true;
                    btnSave.Visible = false;
                    btnPrint.Enabled = true;
                }
            }
        }

        private void UserVisibleControls(string username)
        {
            string query = "SELECT [UserEntryForm],[ApprovalEntryForm],[UserReadOnlyForm]  FROM UserAuthorizationForms where UserAuthorizationForm = '" + form + "' and UserName = '" + (string)Session["UserName"] + "'";
            SqlConnection conn = new SqlConnection(strconn);
            conn.Open();
            SqlDataAdapter da = new SqlDataAdapter(query, conn);
            DataSet ds = new DataSet();
            da.Fill(ds, "SQLTABLE");

            DataRowView rv = ds.Tables["SQLTABLE"].DefaultView[0];
            string userentry = rv["UserEntryForm"].ToString();
            string approval = rv["ApprovalEntryForm"].ToString();

            if (userentry == "True")
            {
                chkApproved.Visible = false;
                rwRemarks.Visible = false;
            }

            if (approval == "True")
            {
                chkApproved.Visible = true;
                rwRemarks.Visible = true;
            }

        }

        private void VoucherNo()
        {
            txtVchNo.Text = Utility.GetNextAutoNo(custCode);//, strconn, (string)Session["FYear"], (string)Session["BranchCode"]);//GetNextAutoNo
        }

        private void UpdateVoucherNo()
        {
            int slno = Utility.GetAddAutoNo(custCode);//, strconn, (string)Session["FYear"], (string)Session["BranchCode"]);
            string qry1 = "Update M_AutoGenerate set KeyCode=" + slno + " where KeyName='" + custCode + "'";// And FYear='" + (string)Session["FYear"] + "' And BranchCode='" + (string)Session["BranchCode"] + "'";
            GetCommandIMP(qry1);
        }

        private void EditReceipt()
        {
            string sqlQuery = "Select VoucherNo,VoucherDate,AccountDrName,Narration,Approved,VGUID,Chq_No,Chq_Date,AmountDr from T_ReceiptMaster where VoucherNo='" + (string)Session["ReceiptDetails"] + "'";
            SqlConnection conn = new SqlConnection(strconn);
            conn.Open();
            SqlCommand cmd = new SqlCommand(sqlQuery, conn);
            SqlDataReader rv = cmd.ExecuteReader();
            if (rv.Read())
            {
                txtVchNo.Text = rv["VoucherNo"].ToString();
                VchDt = rv["VoucherDate"].ToString();
                AcntDR = rv["AccountDrName"].ToString();
                txtNarration.Text = rv["Narration"].ToString();
                Approved = rv["Approved"].ToString();
                Session["VGUID"] = rv["VGUID"].ToString();
                DateTime VDT = Convert.ToDateTime(VchDt);
                txtVchDate.Text = VDT.ToString("dd'/'MM'/'yyyy");
                ddlAccountDr.SelectedItem.Text = AcntDR;
                txtChqNo.Text = rv["Chq_No"].ToString();
                txtamt2.Text = rv["AmountDr"].ToString();
                txtChqDate.Text = rv["Chq_Date"].ToString();
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
                    chkApproved.Checked = false;
                    btnUpdate.Visible = true;
                    btnSave.Visible = false;
                }
            }
            conn.Close();
            GridTemp();
        }

        private void AccountDr()
        {
            DataSet ds = new DataSet();
            ds = ms.Account();
            ddlAccountDr.DataSource = ds;
            ddlAccountDr.DataTextField = "AccountName";
            ddlAccountDr.DataValueField = "AccountName";
            ddlAccountDr.DataBind();
        }

        private void AccountCr()
        {
            DataSet ds = new DataSet();
            ds = ms.PartyName();
            ddlAccountCr.DataSource = ds;
            ddlAccountCr.DataTextField = "AccountName";
            ddlAccountCr.DataValueField = "AccountName";
            ddlAccountCr.DataBind();
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
                    string qry = "Update T_ReceiptMaster set Approved='" + Apd + "' ,Narration='" + narration + "'  Where VoucherNo='" + VchNO + "'";
                    GetCommandIMP(qry);
                    btnSave.Enabled = false;
                    btnPrint.Visible = true;
                    btnPrint.Enabled = true;
                    //mailsend();
                    ClassMsg.Show("Receipt Saved Successfully");
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
            Response.Redirect("~/Accounts/ReceiptDetails.aspx");
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            string VchNO = txtVchNo.Text;
            string dates = txtVchDate.Text;
            string[] DT = dates.Split('/');
            dates = DT[2] + "-" + DT[1] + "-" + DT[0];

            string chdates = txtChqDate.Text;
            string[] DT1 = chdates.Split('/');
            chdates = DT1[2] + "-" + DT1[1] + "-" + DT1[0];

            string AccDr = ddlAccountCr.SelectedItem.Text;
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
            string SqlQuery = "Update T_ReceiptMaster Set VoucherDate='" + dates + "',BranchCode='" + (string)Session["BranchCode"] + "',CompanyCode='" + (string)Session["CompanyCode"] + "'," +
                              " AccountDrCode='" + AccDr + "',AccountDrName='" + AccDr + "',AmountDr='" + txtamt2.Text + "',Chq_No='" + txtChqNo.Text + "',Chq_Date='" + chdates + "',Narration='" + txtNarration.Text + "' ," +
                              " Approved=" + Apd + ",Completed=" + 0 + ",ModifiedBy='" + (string)Session["UserName"] + "',ModifiedDate='" + DateTime.Now + "', VGUID='" + (string)Session["VGUID"] + "' " +
                              " where VoucherNo='" + txtVchNo.Text + "' ";
            GetCommandIMP(SqlQuery);
            ClassMsg.Show("Payment updated successfully");
        }

        protected void btnNew_Click(object sender, EventArgs e)
        {
            NewReceipt();
        }

        private void NewReceipt()
        {
            Response.Redirect("~/Accounts/Receipt.aspx?mode=New");
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

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            string recid=(string)Session["ReceiptId"];
            if (recid == string.Empty || recid == "" || recid==null)
                {
                if (Chk.Checked == false)
                {
                    SaveMaster();
                }
                SaveDetails();
               // btnSave.Enabled = true;
                 }
                else
                {
                    UpdateDetails();
                }
            
        }

        private void SaveDetails()
        {
            try
            {
                string dates = txtVchDate.Text;
                string chdates = txtChqDate.Text;
                string[] DT = dates.Split('/');
                dates = DT[2] + "-" + DT[1] + "-" + DT[0];
                string Query = "Insert into T_ReceiptDetails(VoucherNo,VoucherDate,AccountCrCode,AccountCrName,AmountCr,MethodOfAdj,Reference,CostCenter,CreatedBy,CreatedDate,ModifiedBy,ModifiedDate,VGUID)" +
                                "values('" + txtVchNo.Text + "','" + dates + "','" + ddlAccountCr.SelectedValue + "','" + ddlAccountCr.SelectedItem.Text + "'," + txtamt1.Text + ", " +
                                " '" + ddlMethod.SelectedValue + "','" + txtDetails.Text + "','" + txtCost.Text + "','" + (string)Session["UserName"] + "','" + DateTime.Now + "','" + (string)Session["UserName"] + "','" + DateTime.Now + "','" + (string)Session["VGUID"] + "')";
                GetCommandIMP(Query);
                GridTemp();
            }
            catch
            {
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
                string receipt = "Receipt";
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
                string Query = "Insert into T_ReceiptMaster(VoucherNo,VoucherDate,BranchCode,CompanyCode,AccountDrCode,AccountDrName,AmountDr,Chq_No,Chq_Date,Narration,VCH_Type,VGUID,Approved,Completed,CreatedBy,CreatedDate,ModifiedBy,ModifiedDate,FinanceYear)" +
                                      "values('" + txtVchNo.Text + "','" + dates + "','" + (string)Session["BranchCode"] + "','" + (string)Session["CompanyCode"] + "','" + ddlAccountDr.SelectedValue + "','" + ddlAccountDr.SelectedItem.Text + "'," +
                                   " " + txtamt2.Text + ",'" + txtChqNo.Text + "','" + chdates + "','" + txtNarration.Text + "','" + receipt + "','" + (string)Session["VGUID"] + "'," +
                                   " " + Apd + "," + 0 + ",'" + (string)Session["UserName"] + "','" + DateTime.Now + "','" + (string)Session["UserName"] + "','" + DateTime.Now + "','" + (string)Session["FYear"] + "')";
                GetCommandIMP(Query);
                Chk.Checked = true;
            }
            catch
            {
                Chk.Checked = true;
            }
        }

        private void UpdateDetails()
        {
            string Query = "Update T_ReceiptDetails Set AccountCrCode='" + ddlAccountCr.SelectedValue + "',AccountCrName='" + ddlAccountCr.SelectedItem.Text + "',AmountCr='" + txtamt1.Text + "',MethodOfAdj='" + ddlMethod.SelectedValue + "',Reference='" + txtDetails.Text + "',CostCenter='" + txtCost.Text + "',ModifiedBy='" + (string)Session["UserName"] + "',ModifiedDate='" + DateTime.Now + "' Where TransId='" + (string)Session["ReceiptId"] + "'";
            GetCommandIMP(Query);
            Session["ReceiptId"] = "";
            GridTemp();
        }

        private void GridTemp()
        {            
            string query = string.Empty;
            query = "SELECT [TransId], [AccountCrName],  [MethodOfAdj], [CostCenter], [Reference],[AmountCr] FROM [T_ReceiptDetails] Where VGUID='" + (string)Session["VGUID"] + "'";//VoucherNo='" + txtVchNo.Text + "'";// [Chq_No], [Chq_Date],
            DataSet ds = GetDataSQL(query);
            if (ds.Tables["SQLtable"].Rows.Count != 0)
            {
               // rwGridViewDetails.Visible = true;
                gvReceiptDetails.DataSource = ds;
                gvReceiptDetails.DataBind();
                TallyCrAmount();
            }
            //else
            //{
            //    gvReceiptDetails.DataSource = null;
            //    gvReceiptDetails.DataBind();
            //}
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
            txtamt1.Text = txtChqDate.Text = txtChqNo.Text = string.Empty;
        }

        protected void btnPrint_Click(object sender, EventArgs e)
        {
            Session["VchNo"] = txtVchNo.Text;
            Response.Redirect("frmPrint_Receipt.aspx");    
        }

        protected void gvReceiptDetails_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string chq = string.Empty;
                string Receipt = gvReceiptDetails.SelectedRow.Cells[1].Text;
                Session["ReceiptId"] = Receipt;
                Session["Receipt"] = "View";
                string query = "SELECT TransId,AccountCrName, MethodOfAdj, CostCenter, Reference, AmountCr FROM T_ReceiptDetails WHERE TransId = '" + Receipt + "'";
                SqlConnection conn = new SqlConnection(strconn);
                conn.Open();
                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataReader prw = cmd.ExecuteReader();
                if (prw.Read())
                {
                    ddlAccountCr.SelectedItem.Text = prw["AccountCrName"].ToString();
                    ddlMethod.SelectedValue = prw["MethodOfAdj"].ToString();
                    txtDetails.Text = prw["Reference"].ToString();
                    txtCost.Text = prw["CostCenter"].ToString();
                    txtamt1.Text = prw["AmountCr"].ToString();
                }
                conn.Close();
                TallyCrAmount();
            }
            catch (Exception ex)
            {
                string Message = ex.Message;
            }
        }

        protected void ddlAccountCr_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                Session["AccountDr"] = ddlAccountCr.SelectedItem.Text;
                Reference();
                CostCenter();
            }
            catch
            {

            }
        }

        protected void chkApproved_CheckedChanged(object sender, EventArgs e)
        {
            if (chkApproved.Checked == true)
            {
                txtRemarks.Enabled = true;
            }
            else
            {
                txtRemarks.Enabled = false;
            }
        }

        private void Reference()
        {
            string query = "Select Distinct FundRequestNo from T_FundRequest where ImporterName='" + ddlAccountCr.SelectedItem.Text + "'";
            SqlConnection conn = new SqlConnection(strconn);
            conn.Open();
            SqlCommand cmd = new SqlCommand(query, conn);
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                txtDetails.Text = dr["FundRequestNo"].ToString();
            }            
        }

        private void BillAmt()
        {
            try
            {
                string Query = "Select Distinct Net_Total from View_InvoiceDebitNote where InvoiceNo='" + txtDetails.Text + "'";
                SqlConnection conn = new SqlConnection(strconn);
                conn.Open();
                SqlCommand cmd = new SqlCommand(Query, conn);
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.Read())
                {
                    txtamt1.Text = dr["Net_Total"].ToString();
                }
            }
            catch
            {
            }
          }

        private void CostCenter()
        {
            try
            {
               
                string Query = "Select Distinct CostCenterName from M_CostCenter where AccountName='" + ddlAccountCr.SelectedItem.Text + "'";
                SqlConnection conn = new SqlConnection(strconn);
                conn.Open();
                SqlCommand cmd = new SqlCommand(Query, conn);
                SqlDataReader dr = cmd.ExecuteReader();
                //if (dr.Read())
                //{
                //    txtCost.DataSource = dr;
                //    txtCost.DataTextField = "CostCenter";
                //    txtCost.DataValueField = "CostCenter";
                //    txtCost.DataBind();
                //}
            }
            catch
            {
            }

        }

        protected void txtDetails_TextChanged(object sender, EventArgs e)
        {
            BillAmt();
           // CostCenter();
        }

        protected void gvReceiptDetails_DataBound(object sender, EventArgs e)
        {
            
            double TotalAmt = 0;
            int i = 0;
            foreach (GridViewRow gv in gvReceiptDetails.Rows)
            {
                string amt = gvReceiptDetails.Rows[i].Cells[6].Text;
                TotalAmt = TotalAmt + Convert.ToDouble(amt);
                i++;
            }
            if (gvReceiptDetails.Rows.Count != 0)
            {
                gvReceiptDetails.FooterRow.Cells[4].Text = "Total Amounts";
                gvReceiptDetails.FooterRow.Cells[5].Text = Convert.ToString(TotalAmt);
            }
            double dramt = Convert.ToDouble(txtamt2.Text);
            txtamt1.Text = Convert.ToString(dramt - TotalAmt);
            if (Convert.ToDouble(gvReceiptDetails.FooterRow.Cells[5].Text) == Convert.ToDouble(txtamt2.Text))
            {
                btnSave.Enabled = true;
            }
            else
            {
                btnSave.Enabled = false;
            }
        }

        protected void btnPrevious_Click(object sender, EventArgs e)
        {
            string ab = txtVchNo.Text;
            string[] c = ab.Split('/');
            int d = Convert.ToInt32(c[3].ToString());
            d = d - 1;
            string value = c[0] + "/" + c[1] + "/" + c[2] + "/" + d;
            Session["ReceiptDetails"] = value;
            txtVchNo.Text = value;
            EditReceipt();

        }

        protected void btnNext_Click(object sender, EventArgs e)
        {
            string ab = txtVchNo.Text;
            string[] c = ab.Split('/');
            int d = Convert.ToInt32(c[3].ToString());
            int no = Utility.GetNextNoReceipt(c[0]);//, strconn, c[2], c[1]);
            int d1 = d + 1;
            string value = c[0] + "/" + c[1] + "/" + c[2] + "/" + d1;
            txtVchNo.Text = value;
            if (no <= d)
            {
                Clear();
                gvReceiptDetails.DataBind();
                ClassMsg.Show("No Data Found");
            }
            else
            {
                Session["ReceiptDetails"] = value;
                EditReceipt();
            }
            
        }

        protected void txtamt1_TextChanged(object sender, EventArgs e)
        {

        }

        
    }
}
