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

namespace ImpexCube.Accounts
{
    public partial class Journal : System.Web.UI.Page
    {
        Master ms = new Master();

        string strconn = (string)ConfigurationManager.ConnectionStrings["Constr"].ConnectionString;

        #region
        string Username = string.Empty;
        string form = "Journal Entry";
        string VchNo;
        string VchDt;
        string AcntName;       
        string Desc;
        string DrCR;        
        string amount;
        string narration;
        string CB;
        string CD;
        string Gid;
        string Branch;
        string Approved;
        string CC;
        string sno;
        string custCode = "JN";
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {           

            if (IsPostBack == false)
            {
                Username = (string)Session["UserName"];
                
                if (Request.QueryString["mode"] == "Edit")
                {
                   //Session["Journal"] = string.Empty;                    
                  //  UserVisibleControls(Username);
                    Accountname();                    
                    EditJournal();

                    btnUpdate.Visible = true;
                    btnSave.Visible = false;

                }
                else if (Request.QueryString["mode"] == "New")
                {
                   // Session["Journal"] = string.Empty;
                    Session["VGUID"] = Guid.NewGuid().ToString();
                   // UserVisibleControls(Username);                    
                    Accountname();
                    VoucherNo();
                    string dates = DateTime.Now.ToString("dd'/'MM'/'yyyy");
                    txtVchDate.Text = dates;

                    btnUpdate.Visible = false;
                    btnSave.Visible = true;
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
            }

            if (approval == "True")
            {
                chkApproved.Visible = true;
            }

        }

        private void VoucherNo()
        {
            txtVchNo.Text = Utility.GetNextAutoNo(custCode);//, strconn, (string)Session["FYear"], (string)Session["BranchCode"]);//GetNextAutoNo
        }

        private void UpdateVoucherNo()
        {
            int slno = Utility.GetAddAutoNo(custCode);//, strconn, (string)Session["FYear"], (string)Session["BranchCode"]);
            string Keyqry = "Update M_AutoGenerate set KeyCode=" + slno + " where KeyName='JN'";
            GetCommandIMP(Keyqry);
        }

        private void EditJournal()
        {
            string sqlQuery = "Select VoucherNo,VoucherDate,Narration,VGUID,Approved from Journal where VoucherNo='" + (string)Session["JournalDetails"] + "'";
            SqlConnection conn = new SqlConnection(strconn);
            conn.Open();
            SqlDataAdapter da = new SqlDataAdapter(sqlQuery, conn);
            DataSet ds = new DataSet();
            da.Fill(ds, "SQLTABLE");
            conn.Close();
            if (ds.Tables["SQLTABLE"].Rows.Count != 0)
            {
                DataRowView rv = ds.Tables["SQLTABLE"].DefaultView[0];

                txtVchNo.Text = rv["VoucherNo"].ToString();
                VchDt = rv["VoucherDate"].ToString();
                txtNarration.Text = rv["Narration"].ToString();
                Session["VGUID"] = rv["VGUID"].ToString();
                Approved = rv["Approved"].ToString();
                chkApproved.Checked = true;
                DateTime VDT = Convert.ToDateTime(VchDt);
                txtVchDate.Text = VDT.ToString("dd'/'MM'/'yyyy");
                if (Approved == "True")
                {
                    chkApproved.Checked = true;
                    btnUpdate.Enabled = false;
                    btnAdd.Visible = false;
                    chkApproved.Enabled = false;
                }
                else
                {
                    chkApproved.Checked = false;
                   
                }
            }
            else
            {
                txtVchNo.Text = (string)Session["JournalDetails"];
                gvJournalDetails.DataSource = null;
                gvJournalDetails.DataBind();
            }

                GridTemp();
                //Session["JournalDetails"] = "";
            
        }

        private void Accountname()
        {
            DataSet ds = new DataSet();
            ds = ms.AccountPDI();

            ddlAccountName.DataSource = ds;
            ddlAccountName.DataTextField = "AccountName";
            ddlAccountName.DataBind();
        }                

        protected void btnSave_Click(object sender, EventArgs e)
        {
            string dates = txtVchDate.Text;
            string[] DT = dates.Split('/');
            dates = DT[2] + "-" + DT[1] + "-" + DT[0];
            string narration = txtNarration.Text;

            double amtCr = 0;
            double amtDr = 0;            
            string Apd;
            if (chkApproved.Checked == true)
            {
                Apd = "1";
            }
            else
            {
                Apd = "0";
            }
            string Drqry = "Select Sum(Amount) as TotalDr from Journal Where VGUID='" + (string)Session["VGUID"] + "' and DrCr = 'Dr' ";
            DataSet Ddt = GetDataSQL(Drqry);
            double DrAmount = 0;
            DataRowView Drw1 = Ddt.Tables["SQLtable"].DefaultView[0];
            if (Drw1["TotalDr"].ToString() != "")
            {
                DrAmount = Convert.ToDouble(Drw1["TotalDr"].ToString());
            }
            //string DrAmount = Drw1["TotalDr"].ToString();
            if (DrAmount != 0)
            {
                amtDr = amtDr + Convert.ToDouble(DrAmount);
            }

            string Crqry = "Select Sum(Amount) as TotalCr from Journal Where VGUID='" + (string)Session["VGUID"] + "' and DrCr = 'Cr' ";
            DataSet Cdt = GetDataSQL(Crqry);
            double CrAmount = 0;
            DataRowView Crw1 = Cdt.Tables["SQLtable"].DefaultView[0];
            if (Crw1["TotalCr"].ToString() != "" )
            {
                CrAmount = Convert.ToDouble(Crw1["TotalCr"].ToString());
            }
            if (CrAmount != 0)
            {
                amtCr = amtCr + Convert.ToDouble(CrAmount);
            }
            if (amtDr == amtCr && Crw1["TotalCr"].ToString() != ""  &&  Drw1["TotalDr"].ToString() != "")
            {
                string Query = "Update Journal Set VoucherDate='" + dates + "',Narration='" + narration + "',Approved='" + Apd + "' where VoucherNo = '" + txtVchNo.Text + "'";
                GetCommandIMP(Query);
                ClassMsg.Show("Journal saved successfully");                    
            }
            else
            {
                ClassMsg.Show("Miss matched amount");                
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

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            string dates = txtVchDate.Text;
            string[] DT = dates.Split('/');
            dates = DT[2] + "-" + DT[1] + "-" + DT[0];
            string narration = txtNarration.Text;

            double amtCr = 0;
            double amtDr = 0;
            string Apd;
            if (chkApproved.Checked == true)
            {
                Apd = "1";
            }
            else
            {
                Apd = "0";
            }
            string Drqry = "Select Sum(Amount) as TotalDr from Journal Where VGUID='" + (string)Session["VGUID"] + "' and DrCr = 'Dr' ";
            DataSet Ddt = GetDataSQL(Drqry);
            DataRowView Drw1 = Ddt.Tables["SQLtable"].DefaultView[0];
            string DrAmount = Drw1["TotalDr"].ToString();
            if (DrAmount != "")
            {
                amtDr = amtDr + Convert.ToDouble(DrAmount);
            }

            string Crqry = "Select Sum(Amount) as TotalCr from Journal Where VGUID='" + (string)Session["VGUID"] + "' and DrCr = 'Cr' ";
            DataSet Cdt = GetDataSQL(Crqry);
            DataRowView Crw1 = Cdt.Tables["SQLtable"].DefaultView[0];
            string CrAmount = Crw1["TotalCr"].ToString();
            if (CrAmount != "")
            {
                amtCr = amtCr + Convert.ToDouble(CrAmount);
            }
            if (amtDr == amtCr)
            {
                string Query = "Update Journal Set VoucherDate='" + dates + "',Narration='" + narration + "',Approved='" + Apd + "' where VoucherNo = '" + txtVchNo.Text + "' ";
                GetCommandIMP(Query);
                ClassMsg.Show("Journal Updated successfully");
            }
            else
            {
                ClassMsg.Show("Miss matched amount");
            }            
        }

        protected void btnNew_Click(object sender, EventArgs e)
        {
            NewJournal();
        }

        private void NewJournal()
        {
            Session["mode"] = "New";
            Response.Redirect("~/Accounts/Journal.aspx?mode=New");
        }

        protected void btnExit_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Accounts/JournalDetails.aspx");
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
            string journalid = (string)Session["JournalId"];
            if (journalid == string.Empty || journalid == "" || journalid == null)
            {
                if (Chk.Checked == false)
                {
                    VoucherNo();
                    UpdateVoucherNo();
                    Chk.Checked = true;
                }
                InsertJournalDetails();
            }
            else
            {
                UpdateJournalDetails();
            }
        }

        private void InsertJournalDetails()
        {
            try
            {
                string VchNO = txtVchNo.Text;
                string dates = txtVchDate.Text;
                string[] DT = dates.Split('/');
                dates = DT[2] + "-" + DT[1] + "-" + DT[0];
                string narration = txtNarration.Text;

                double amt = Convert.ToDouble(txtamt1.Text);
                string AccName = ddlAccountName.SelectedItem.Text;
                string Madj = ddlMethod.SelectedValue;
                string Ref = txtDetails.Text;
                string cost = txtCost.Text;
                string DrCr = ddlDrCr.SelectedValue;

                string query = "Insert into Journal(VoucherNo,VoucherDate,BranchCode,CompanyCode,AccountCode,MethodOfAdj,DrCr,Reference,CostCenter,Amount,Narration,VGUID,Approved,Completed,CreatedBy,CreatedDate,ModifiedBy,ModifiedDate,FinanceYear)" +
                    "values('" + VchNO + "','" + dates + "','" + (string)Session["BranchCode"] + "','" + (string)Session["CompanyCode"] + "','" + AccName + "', '" + Madj + "','" + DrCr + "','" + Ref + "','" + cost + "'," + amt + "," +
                        " '" + narration + "','" + (string)Session["VGUID"] + "'," + 0 + "," + 0 + ",'" + (string)Session["UserName"] + "','" + DateTime.Now + "','" + (string)Session["UserName"] + "','" + DateTime.Now + "','" + (string)Session["FYear"] + "')";
                GetCommandIMP(query);
                
                GridTemp();
                Clear();
            }
            catch
            {
            }
        }

        private void UpdateJournalDetails()
        {
            double amt = Convert.ToDouble(txtamt1.Text);
            string query = "Update Journal Set AccountCode='" + ddlAccountName.SelectedItem.Text + "',MethodOfAdj= '" + ddlMethod.SelectedItem.Text + "',Reference='" + txtDetails.Text + "'," +
                "CostCenter='" + txtCost.Text + "',DrCr='" + ddlDrCr.SelectedItem.Text + "',Amount=" + amt + ",VGUID='" + (string)Session["VGUID"] + "' " +
                " Where TransId= '" + (string)Session["JournalId"] + "'";
            GetCommandIMP(query);
            Session["JournalId"] = "";
            GridTemp();
            Clear();
        }

        private void Clear()
        {
            Accountname();
            txtamt1.Text = txtCost.Text = txtDetails.Text = string.Empty;
        }

        private void GridTemp()
        {
            string query = string.Empty;
            query = "SELECT TransId,AccountCode,CostCenter,Reference,DrCr, Amount FROM Journal WHERE VGUID='" + (string)Session["VGUID"] + "'";
            DataSet ds = GetDataSQL(query);
            if (ds.Tables["SQLtable"].Rows.Count != 0)
            {
                gvJournalDetails.DataSource = ds;
                gvJournalDetails.DataBind();
            }
            else
            {
                gvJournalDetails.DataSource = null;
                gvJournalDetails.DataBind();
            }
        }

        protected void gvJournalDetails_SelectedIndexChanged(object sender, EventArgs e)
        {
            string chq = string.Empty;
            string Journal = gvJournalDetails.SelectedRow.Cells[1].Text;
            Session["JournalId"] = Journal;
                string query = "Select AccountCode,MethodOfAdj,Reference,CostCenter,DrCr,Amount from Journal where TransId= '" + Journal + "'";
                SqlConnection conn = new SqlConnection(strconn);
                conn.Open();
                SqlDataAdapter da = new SqlDataAdapter(query, conn);
                DataSet ds = new DataSet();
                da.Fill(ds, "SQLtable");
                DataTable dt = ds.Tables["SQLtable"];
                conn.Close();
                if (ds.Tables["SQLtable"].Rows.Count != 0)
                {
                    ddlMethod.DataBind();
                    ddlDrCr.DataBind();
                    DataRowView prw = ds.Tables["SQLtable"].DefaultView[0];
                    ddlAccountName.SelectedItem.Text = prw["AccountCode"].ToString();
                    ddlMethod.SelectedValue = prw["MethodOfAdj"].ToString();
                    txtDetails.Text = prw["Reference"].ToString();
                    txtCost.Text = prw["CostCenter"].ToString();
                    ddlDrCr.SelectedValue = prw["DrCr"].ToString();
                    txtamt1.Text = prw["Amount"].ToString();
                }
        }

        protected void btnPrevious_Click(object sender, EventArgs e)
        {
            string ab = txtVchNo.Text;
            string[] c = ab.Split('/');
            int d = Convert.ToInt32(c[3].ToString());
            d = d - 1;
            string value = c[0] + "/" + c[1] + "/" + c[2] + "/" + d;
            Session["JournalDetails"] = value;
            EditJournal();
        }

        protected void btnNext_Click(object sender, EventArgs e)
        {
            string ab = txtVchNo.Text;
            string[] c = ab.Split('/');
            int d = Convert.ToInt32(c[3].ToString());
            int no = Utility.GetNextNoJournal(c[0]);//, strconn, c[2], c[1]);
            int d1 = d + 1;
            string value = c[0] + "/" + c[1] + "/" + c[2] + "/" + d1;
            txtVchNo.Text = value;
            if (no <= d)
            {
                Clear();
                gvJournalDetails.DataBind();
                ClassMsg.Show("No Data Found");
            }
            else
            {
                
                Session["JournalDetails"] = value;
                EditJournal();
            }
        }
    }
}
