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
    public partial class ContraEntry : System.Web.UI.Page
    {
        Master ms = new Master();

        string strconn = (string)ConfigurationManager.ConnectionStrings["Constr"].ConnectionString;

        #region
        string Username = string.Empty;
        string form = "Contra Entry";
        string VchNo;
        string VchDt;
        string AcntCR;
        string AcntDR;
        string sno;
        string Desc;
        string Chqno;
        string Chqdt;
        string amount;
        string narration;
        string CB;
        string CD;
        string CC;
        string Gid;
        string Branch;
        string Approved;
        string VchType;
        string custCode = "CN";
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack == false)
            {
                Username = (string)Session["UserName"];
                if (Request.QueryString["mode"] == "Edit")
                {
                   // UserVisibleControls(Username);
                    Session["Contra"] = string.Empty;
                    AccountCr();
                    AccountDr();
                    EditContraDetails();

                    btnUpdate.Visible = true;
                    btnSave.Visible = false;
                }
                else if (Request.QueryString["mode"] == "New")
                {
                   // UserVisibleControls(Username);
                    Session["VGUID"] = Guid.NewGuid().ToString();
                    Session["Contra"] = string.Empty;                    
                    AccountCr();
                    AccountDr();
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
            txtVchNo.Text = Utility.GetNextAutoNo(custCode);//, strconn, (string)Session["FYear"], (string)Session["BranchCode"]);
        }

        private void EditContraDetails()
        {
            string sqlQuery = "Select * from T_Contra where VoucherNo='" + (string)Session["ContraDetails"] + "'";
            SqlConnection conn = new SqlConnection(strconn);
            conn.Open();
            SqlDataAdapter da = new SqlDataAdapter(sqlQuery, conn);
            DataSet ds = new DataSet();
            da.Fill(ds, "SQLTABLE");
            if (ds.Tables["SQLTABLE"].Rows.Count != 0)
            {
                DataRowView rv = ds.Tables["SQLTABLE"].DefaultView[0];
                conn.Close();
                VchNo = rv["VoucherNo"].ToString();
                Session["VchNo"] = VchNo;
                VchDt = rv["VoucherDate"].ToString();
                AcntCR = rv["Acc_CrCode"].ToString();
                narration = rv["Narration"].ToString();
                CB = rv["CreatedBy"].ToString();
                CD = rv["CreatedDate"].ToString();
                Session["GUID"] = rv["VGUID"].ToString();
                Session["branch"] = rv["BranchCode"].ToString();
                Approved = rv["Approved"].ToString();
                Session["company"] = rv["CompanyCode"].ToString();

                DateTime VDT = Convert.ToDateTime(VchDt);

                txtVchNo.Text = VchNo;
                txtVchDate.Text = VDT.ToString("dd'/'MM'/'yyyy");
                ddlAccountCr.SelectedValue = AcntCR;
                txtNarration.Text = narration;
                if (Approved == "True")
                {
                    chkApproved.Checked = true;
                    btnAdd.Visible = false;
                    btnUpdate.Enabled = false;
                }
                else
                {
                    chkApproved.Checked = false;
                }
            }
                GridTemp();
            }
        

        private void AccountDr()
        {
            DataSet ds = new DataSet();
            ds = ms.Particulars();
            ddlAccountDr.DataSource = ds.Tables[0];
            ddlAccountDr.DataTextField = "AccountName";
            ddlAccountDr.DataBind();
        }

        private void AccountCr()
        {


            DataSet ds = new DataSet();
            ds = ms.Account();


            ddlAccountCr.DataSource = ds.Tables[0];
            ddlAccountCr.DataTextField = "AccountName";            
            ddlAccountCr.DataBind(); 
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            VoucherNo();
            int i = 0;
            //int count = 1;
            string VchNO = txtVchNo.Text;
            string dates = txtVchDate.Text;           

            string[] DT = dates.Split('/');
            dates = DT[2] + "-" + DT[1] + "-" + DT[0];

            string AccCr = ddlAccountCr.SelectedValue;
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
            string query = "select * from T_ContraTemp where VGUID='" + (string)Session["VGUID"] + "' ";
            DataSet Rds = GetDataSQL(query);
            DataTable Rdt = Rds.Tables["SQLtable"];

            foreach (DataRow row in Rdt.Rows)
            {
                DataRowView rw = Rds.Tables["SQLtable"].DefaultView[i];
                string AccDr = rw["AccCode"].ToString();                
                string Ref = rw["Reference"].ToString();                
                string ChqNo = rw["Chq_No"].ToString();
                string chqDate = rw["Chq_Date"].ToString();
                string amount = rw["Amount"].ToString();
                chqDate = DT[2] + "-" + DT[1] + "-" + DT[0];
                double amt = Convert.ToDouble(amount);
                string Query = "Insert into T_Contra(VoucherNo,VoucherDate,BranchCode,CompanyCode,Acc_DrCode,Acc_CrCode,Reference,Amount,Chq_No,Chq_Date,Narration,VGUID,Approved,Completed,CreatedBy,CreatedDate,ModifiedBy,ModifiedDate)" +
                        "values('" + VchNO + "','" + dates + "','" + (string)Session["BranchCode"] + "','" + (string)Session["CompanyCode"] + "','" + AccDr + "','" + AccCr + "','" + Ref + "'," + amt + ",'" + ChqNo + "'," +
                        "'" + chqDate + "','" + narration + "','" + (string)Session["VGUID"] + "'," + Apd + "," + 0 + ",'" + (string)Session["Username"] + "','" + DateTime.Now + "','" + (string)Session["Username"] + "','" + DateTime.Now + "')";

                GetCommandIMP(Query);                
                i++;
            }
            string SQLQuery = "select VoucherNo from T_Contra where VoucherNo = '" + VchNO + "'";
            DataSet ds = GetDataSQL(SQLQuery);
            if (ds.Tables[0].Rows.Count != 0)
            {
                int slno = Utility.GetAddAutoNo(custCode);//, strconn, (string)Session["FYear"], (string)Session["BranchCode"]);
                string qry1 = "Update M_AutoGenerate set KeyCode=" + slno + " where KeyName='" + custCode + "'";// And FYear='" + (string)Session["FYear"] + "' And BranchCode='" + (string)Session["BranchCode"] + "'";
                GetCommandIMP(qry1);

                string query1 = "Delete from T_ContraTemp where VGUID='" + (string)Session["VGUID"]  + "'";                
                DataSet Pds = GetDataSQL(query1);
                ClassMsg.Show("Contra details saved successfully");                
            }
            else
            {
                ClassMsg.Show("Please verify contra details once again");
            }
            //NewContraEntry();
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
            Response.Redirect("~/Accounts/ContraDetails.aspx");
        }        

        protected void btnNew_Click(object sender, EventArgs e)
        {
            NewContraEntry();           
        }

        private void NewContraEntry()
        {
            Response.Redirect("~/Accounts/ContraEntry.aspx?mode=New");
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            //int count = 1;
            //string VchNO = txtVchNo.Text;
            //string dates = txtVchDate.Text;
            //string cb = "VTS";

            //string[] DT = dates.Split('/');
            //dates = DT[2] + "-" + DT[1] + "-" + DT[0];

            //string AccCr = ddlAccountCr.SelectedValue;
            //string narration = txtNarration.Text;
            //foreach (GridViewRow Rows in gvContra.Rows)
            //{
            //    Label lbsno = (Label)Rows.FindControl("lblSno");
            //    DropDownList ddlAccDr = (DropDownList)Rows.FindControl("ddlAccountDr");
            //    TextBox txtDesc = (TextBox)Rows.FindControl("txtDetails");
            //    TextBox txtChqNoDet = (TextBox)Rows.FindControl("txtChqNo");
            //    TextBox txtChqDateDet = (TextBox)Rows.FindControl("txtChqDate");
            //    TextBox txtAmtDet = (TextBox)Rows.FindControl("txtamt1");

            //    string Sno = lbsno.Text;
            //    string AccDr = ddlAccDr.SelectedValue;
            //    string Desc = txtDesc.Text;
            //    string ChqNo = txtChqNoDet.Text;
            //    string chqDate = txtChqDateDet.Text;
            //    chqDate = DT[2] + "-" + DT[1] + "-" + DT[0];
            //    string Apd;

            //    if (chkApproved.Checked == true)
            //    {
            //        Apd = "1";
            //    }
            //    else
            //    {
            //        Apd = "0";
            //    }

            //    double amt = Convert.ToDouble(txtAmtDet.Text);
            //    if (Desc != "" && amt != null)
            //    {
            //        string Query = "Update Contra Set VoucherNo='" + VchNO + "',VoucherDate='" + dates + "',BranchCode='" + Branch + "',CompanyCode='" + CC + "',Slno = '" + Sno + "',Acc_DrCode='" + AccDr + "'," +
            //            "Acc_CrCode='" + AccCr + "',Reference='" + Desc + "',Amount=" + amt + "," +
            //            "Narration='" + narration + "',VGUID='" + Gid + "',Approved=" + Apd + "," +
            //            "Completed=" + 0 + ",CreatedBy='" + CB + "',CreatedDate='" + CD + "',"+
            //            " ModifiedBy='" + (string)Session["UserName"] + "',ModifiedDate='" + DateTime.Now + "' where VoucherNo='" + VchNO + "' and Slno ='" + Sno + "'";

            //        GetCommandIMP(Query);
            //        count = count + 1;
            //        //flag = 1;
            //    }
            //    else
            //    {
            //        Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "alert('Enter Required Fields');", true);
            //        //Response.Write("<script>" + "alert('Enter the Required Field')" + "</script>");
            //    }
            //}
            //Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "alert('Contra Updated Successfully');", true);
            ////Response.Write("<script>" + "alert('Contra Updated Successfully');" + "</script>");
            //NewContraEntry();
        }

        protected void btnAdd_Click(object sender, ImageClickEventArgs e)
        {
            string dates = string.Empty;
            string chqDate = string.Empty;
            if (txtChqDate.Text != "")
            {
                dates = txtChqDate.Text;

                string[] DT = dates.Split('/');
                dates = DT[2] + "-" + DT[1] + "-" + DT[0];

                chqDate = txtChqDate.Text;
                chqDate = DT[2] + "-" + DT[1] + "-" + DT[0];
            }
            else
                chqDate = string.Empty;

            if (Request.QueryString["mode"] == "New")
            
            {
                if ((string)Session["Contra"] == "")
                {
                    string SqlQuery = "Insert into T_ContraTemp(AccCode, Reference, Chq_No, Chq_Date, Amount, VGUID)" +
                        "Values('" + ddlAccountDr.SelectedItem.Text + "','" + txtDetails.Text + "'," +
                        "'" + txtChqNo.Text + "','" + chqDate + "'," + txtamt1.Text + ",'" + (string)Session["VGUID"] + "' )";
                    GetCommandIMP(SqlQuery);
                    GridTemp();
                    gvContra.DataBind();
                    Clear();
                }
                else
                {
                    string SqlQuery = "Update T_ContraTemp Set AccCode ='" + ddlAccountDr.SelectedItem.Text + "', Reference= '" + txtDetails.Text + "'," +
                        "Chq_No='" + txtChqNo.Text + "', Chq_Date='" + chqDate + "', Amount=" + txtamt1.Text + "," +
                        "VGUID='" + (string)Session["VGUID"] + "' where ContraId = '" + (string)Session["ContraId"] + "' ";
                    GetCommandIMP(SqlQuery);
                    GridTemp();                    
                    Clear();
                }
            }
            else 
            {
                if ((string)Session["Contra"] != "")
                {
                    UpdatePaymentDetails();
                    Session["Contra"] = string.Empty;
                    Clear();
                }
                else
                {
                    InsertPaymentDetails();
                    Session["Contra"] = string.Empty;
                    Clear();
                }
            }
        }

        private void GridTemp()
        {
            string query = string.Empty;
            if (Request.QueryString["mode"] == "New")
            {
                if ((string)Session["Contra"] == "")
                {
                    query = "SELECT [ContraId], [AccCode], [Reference], [Amount], [Chq_No], [Chq_Date] FROM T_ContraTemp where VGUID='" + (string)Session["VGUID"] + "'";
                }
                else
                {
                    query = "SELECT ContraId, AccCode, Reference,Chq_No,Chq_Date, Amount FROM [T_ContraTemp] WHERE ContraId='" + (string)Session["ContraId"] + "'";
                }
            }
            else
            {
                query = "SELECT TransId as ContraId,Acc_DrCode As AccCode,Reference,Chq_no,Chq_Date, Amount FROM [T_Contra] WHERE VoucherNo='" + (string)Session["ContraDetails"] + "'";
            }
            DataSet ds = GetDataSQL(query);
            if (ds.Tables["SQLtable"].Rows.Count != 0)
            {
                gvContra.DataSource = ds;
                gvContra.DataBind();
            }
            else
            {
                gvContra.DataSource = null;
                gvContra.DataBind();
            }
        }

        private void UpdatePaymentDetails()
        {
            string VchNO = txtVchNo.Text;
            string dates = txtVchDate.Text;
            //string cb = "VTS";

            string[] DT = dates.Split('/');
            dates = DT[2] + "-" + DT[1] + "-" + DT[0];

            string chqDate = string.Empty;
            if (txtChqDate.Text != "")
            {
                dates = txtChqDate.Text;

                string[] UDT = dates.Split('/');
                dates = UDT[2] + "-" + UDT[1] + "-" + UDT[0];

                chqDate = txtChqDate.Text;
                chqDate = UDT[2] + "-" + UDT[1] + "-" + UDT[0];
            }
            else
                chqDate = string.Empty;

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

            string SqlQuery = "Update T_Contra Set VoucherNo= '" + (string)Session["VchNo"] + "', VoucherDate='" + dates + "',BranchCode='" + (string)Session["BranchCode"] + "',CompanyCode='" + (string)Session["CompanyCode"] + "',Acc_CrCode='" + AccCr + "'," +
                        "Acc_DrCode ='" + ddlAccountDr.SelectedItem.Text + "', Reference= '" + txtDetails.Text + "'," +
                        "Chq_No='" + txtChqNo.Text + "', Chq_Date='" + chqDate + "', Amount=" + txtamt1.Text + "," +
                        "Narration='" + narration + "', Approved=" + Apd + ",Completed=" + 0 + "," +
                        "ModifiedBy='" + (string)Session["UserName"] + "',ModifiedDate='" + DateTime.Now + "', VGUID='" + (string)Session["VGUID"] + "' " +
                        "where TransId = '" + (string)Session["ContraId"] + "'  and VoucherNo='" + VchNO + "' ";
            GetCommandIMP(SqlQuery);
            GridTemp();            
        }

        private void InsertPaymentDetails()
        {
            string VchNO = txtVchNo.Text;
            string dates = txtVchDate.Text;

            string[] DT = dates.Split('/');
            dates = DT[2] + "-" + DT[1] + "-" + DT[0];

            string AccCr = ddlAccountCr.SelectedValue;
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
            string chqDate = string.Empty;
            if (txtChqNo.Text != "")
            {
                dates = txtChqDate.Text;

                string[] UDT = dates.Split('/');
                dates = UDT[2] + "-" + UDT[1] + "-" + UDT[0];

                chqDate = txtChqDate.Text;
                chqDate = UDT[2] + "-" + UDT[1] + "-" + UDT[0];
            }

            double amt = Convert.ToDouble(txtamt1.Text);
            if (amt != null)
            {
                string Query = "Insert into T_Contra(VoucherNo,VoucherDate,BranchCode,CompanyCode,Acc_DrCode,Acc_CrCode,Reference,Amount,Chq_No,Chq_Date,Narration,VGUID,Approved,Completed,CreatedBy,CreatedDate,ModifiedBy,ModifiedDate)" +
                   "values('" + VchNO + "','" + dates + "','" + (string)Session["BranchCode"] + "','" + (string)Session["CompanyCode"] + "','" + ddlAccountDr.SelectedItem.Text + "','" + AccCr + "'," +
                " '" + txtDetails.Text + "'," + amt + ",'" + txtChqNo.Text + "','" + chqDate + "','" + narration + "'," +
                " '" + (string)Session["GUID"] + "'," + Apd + "," + 0 + ",'" + (string)Session["UserName"] + "','" + DateTime.Now + "','" + (string)Session["UserName"] + "','" + DateTime.Now + "')";

                GetCommandIMP(Query);
                GridTemp();                
            }
        }

        private void Clear()
        {
            txtamt1.Text = txtChqNo.Text = txtChqDate.Text = txtDetails.Text = string.Empty;
            ddlAccountDr.SelectedItem.Text = "~Select~";
        }        

        protected void gvContra_SelectedIndexChanged(object sender, EventArgs e)
        {
            string chq = string.Empty;
            string contra = gvContra.SelectedRow.Cells[1].Text;
            Session["ContraId"] = contra;
            if (Request.QueryString["mode"] == "New")
            {
                Session["Contra"] = "View";
                string query = "Select * from T_ContraTemp where ContraId= '" + contra + "'";
                SqlConnection conn = new SqlConnection(strconn);
                conn.Open();
                SqlDataAdapter da = new SqlDataAdapter(query, conn);
                DataSet ds = new DataSet();
                da.Fill(ds, "SQLtable");
                DataTable dt = ds.Tables["SQLtable"];
                conn.Close();
                if (ds.Tables["SQLtable"].Rows.Count != 0)
                {
                    DataRowView prw = ds.Tables["SQLtable"].DefaultView[0];
                    ddlAccountDr.SelectedItem.Text = prw["AccCode"].ToString();                    
                    txtDetails.Text = prw["Reference"].ToString();                    
                    txtChqNo.Text = prw["Chq_No"].ToString();
                    if (txtChqNo.Text != "")
                    {
                        chq = prw["Chq_Date"].ToString();
                        DateTime CDT = Convert.ToDateTime(chq);
                        txtChqDate.Text = CDT.ToString("dd'/'MM'/'yyyy");
                    }                    
                    txtamt1.Text = prw["Amount"].ToString();
                }
            }
            if (Request.QueryString["mode"] == "Edit")
            {
                Session["Contra"] = "View";
                string query = "Select Acc_DrCode,Reference,Convert(Varchar(12),Chq_Date,103) As [Chq Date],Chq_No,Amount from T_Contra where TransId= '" + contra + "'";
                SqlConnection conn = new SqlConnection(strconn);
                conn.Open();
                SqlDataAdapter da = new SqlDataAdapter(query, conn);
                DataSet ds = new DataSet();
                da.Fill(ds, "SQLtable");
                DataTable dt = ds.Tables["SQLtable"];
                conn.Close();
                if (ds.Tables["SQLtable"].Rows.Count != 0)
                {
                    DataRowView prw = ds.Tables["SQLtable"].DefaultView[0];
                    ddlAccountDr.SelectedItem.Text = prw["Acc_DrCode"].ToString();                    
                    txtDetails.Text = prw["Reference"].ToString();                    
                    txtChqNo.Text = prw["Chq_No"].ToString();
                    txtChqDate.Text = prw["Chq Date"].ToString();
                    txtamt1.Text = prw["Amount"].ToString();
                }
            }
        }
        protected void btnPrevius_Click(object sender, EventArgs e)
        {
            string ab = txtVchNo.Text;
            string[] c = ab.Split('/');
            int d = Convert.ToInt32(c[3].ToString());
            d = d - 1;
            string value = c[0] + "/" + c[1] + "/" + c[2] + "/" + d;
            Session["ContraDetails"] = value;
            EditContraDetails();
        }
        protected void btnNext_Click(object sender, EventArgs e)
        {
           string ab = txtVchNo.Text;
            string[] c = ab.Split('/');
            int d = Convert.ToInt32(c[3].ToString());
            int no = Utility.GetNextNoContra(c[0]);//, strconn, c[2], c[1]);
            int d1 = d + 1;
            string value = c[0] + "/" + c[1] + "/" + c[2] + "/" + d1;
            txtVchNo.Text = value;
            if (no <= d)
            {
                Clear();
                gvContra.DataBind();
                ClassMsg.Show("No Data Found");
            }
            else
            {
                Session["ContraDetails"] = value;
                EditContraDetails();
            }
        }
        protected void ddlAccountCr_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

    }
}
