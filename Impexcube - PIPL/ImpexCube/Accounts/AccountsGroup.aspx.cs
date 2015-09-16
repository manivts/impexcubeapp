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
using System.Text;

namespace ImpexCube.Accounts
{
    public partial class AccountsGroup : System.Web.UI.Page
    {
        string strconn = (string)ConfigurationManager.ConnectionStrings["constr"].ConnectionString;
        
        #region Public Variable Declarition  
        //string gname;
        //string gcode;
        //string unGroup;
        //string CB;
        //string CD;
       // string mode = "New";
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack == false)
            {
              if (Request.QueryString["mode"] =="New" )
                {
                    //Session["custCode"] = "GC";
                    BindGridView(); 
                    DropUnderGroup();
                   //GroupNo();
                    btnUpdate.Visible = false;
                    btnSave.Visible = true;
                }
              else if (Request.QueryString["mode"] == "Edit")
                {
                    BindGridView(); 
                    DropUnderGroup();
                    EditGroup();
                    txtGroupCode.Enabled = false;
                    btnUpdate.Visible = true;
                    btnSave.Visible = false;
                }                
              }
        }

        private void BindGridView()
        {
            string sqlQuery = "Select GroupCode,GroupName,UnderGroup from M_AccountsGroup ";
            //string sqlQuery = "declare @columns varchar(max)  declare @convert varchar(max) select   @columns = stuff (( select distinct'],[' +  PayName  from  View_Pay_Sheet  for xml path('')), 1, 2, '') + ']'  set @convert ='select * from (select PayName,EmpId,EmpName,DOB ,Date_of_Joining, DesigDesc,DeptDesc,BranchName,LopDays,LeaveDays,AccountNumber,PFAccountNo, ESINO,PANNo, NetAmount,GrossAmount,GrossDeduction,  EarnedAmount from  View_Pay_Sheet) SalesRpt    pivot(sum(EarnedAmount) for PayName    in ('+@columns+')) as pivottable' execute (@convert)";
            SqlConnection conn = new SqlConnection(strconn);
            conn.Open();
            SqlDataAdapter da = new SqlDataAdapter(sqlQuery, conn);
            DataSet ds = new DataSet();
            da.Fill(ds, "SQLTABLE");
            conn.Close();
            grdGroup.DataSource = ds;
            grdGroup.DataBind();
        }

        private void EditGroup()
        {
            string sqlQuery = "Select GroupCode,GroupName,UnderGroup from M_AccountsGroup where GroupCode='" + (string)Session["GroupDetails"] + "'";
            SqlConnection conn = new SqlConnection(strconn);
            conn.Open();
            SqlDataAdapter da = new SqlDataAdapter(sqlQuery, conn);
            DataSet ds = new DataSet();
            da.Fill(ds, "SQLTABLE");
            conn.Close();
//            StringBuilder qry=new StringBuilder();
//            qry.Append("declare @columns varchar(max)");
//            qry.Append("declare @convert varchar(max)");


//select   @columns = stuff (( select distinct'],[' +  PayName from  View_Pay_Sheet
//                    for xml path('')), 1, 2, '') + ']'
// set @convert =
//'select * from (select PayName,EmpId,EmpName,DOB ,Date_of_Joining, DesigDesc,DeptDesc,BranchName,LopDays,LeaveDays,AccountNumber,PFAccountNo, ESINO,PANNo, NetAmount,GrossAmount,GrossDeduction,  EarnedAmount from  View_Pay_Sheet) SalesRpt
//    pivot(sum(EarnedAmount) for PayName
//    in ('+@columns+')) as pivottable'
// execute (@convert)



            if (ds.Tables["SQLTABLE"].Rows.Count != 0)
            {
                DataRowView rv = ds.Tables["SQLTABLE"].DefaultView[0];
                txtGroupCode.Text = rv["GroupCode"].ToString();
                txtGroupName.Text = rv["GroupName"].ToString();
                string undergroup = rv["UnderGroup"].ToString();

                ddlUndergroup.SelectedItem.Text = undergroup;
            }
            else
            {
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "alert('Please select once again');", true);
            }
        }

        private void GroupNo()
        {
            txtGroupCode.Text = Utility.GetNextAutoNo((string)Session["custCode"]);//, strconn, (string)Session["FYear"], (string)Session["BranchCode"]);
        }

        private void DropUnderGroup()
        {

            string Query = "Select GroupCode,GroupName from M_AccountsGroup";
            //string Query = "declare @columns varchar(max)  declare @convert varchar(max) select   @columns = stuff (( select distinct'],[' +  PayName  from  View_Pay_Sheet  for xml path('')), 1, 2, '') + ']'  set @convert ='select * from (select PayName,EmpId,EmpName,DOB ,Date_of_Joining, DesigDesc,DeptDesc,BranchName,LopDays,LeaveDays,AccountNumber,PFAccountNo, ESINO,PANNo, NetAmount,GrossAmount,GrossDeduction,  EarnedAmount from  View_Pay_Sheet) SalesRpt    pivot(sum(EarnedAmount) for PayName    in ('+@columns+')) as pivottable' execute (@convert)";
            SqlConnection conn = new SqlConnection(strconn);
            conn.Open();
            SqlCommand cmd = new SqlCommand(Query, conn);
            SqlDataReader dr = cmd.ExecuteReader();
            if (dr.Read())
            {
                ddlUndergroup.DataSource = dr;
                ddlUndergroup.DataTextField = "GroupName";
                ddlUndergroup.DataValueField = "GroupName";
                ddlUndergroup.DataBind();
            }

        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
           // GroupNo();
            //string cb = (string)Session["UserName"];
            //if (txtGroupName.Text != "" && ddlUndergroup.SelectedItem.Text != "0")
            //{
            //    SqlConnection conn = new SqlConnection(strconn);
            //    string query = "select GroupName From AccountsGroup where GroupName = '" + txtGroupName.Text + "' ";
            //    SqlDataAdapter da = new SqlDataAdapter(query,conn);
            //    DataSet ds = new DataSet();
            //    da.Fill(ds, "GrName");
            //    if (ds.Tables["GrName"].Rows.Count == 0)
            //    {
            try
            {
                string Query = "Insert into M_AccountsGroup(GroupCode,GroupName,UnderGroup,CreatedBy,CreatedDate,ModifiedBy,ModifiedDate)values('" + txtGroupCode.Text + "','" + txtGroupName.Text + "','" + ddlUndergroup.SelectedItem.Text  + "','" + (string)Session["UserName"] + "','" + DateTime.Now + "','" + (string)Session["UserName"] + "','" + DateTime.Now + "')";
                GetCommandIMP(Query);
                txtGroupName.Enabled = false;
                btnSave.Enabled = false;
                ClassMsg.Show("Accounts Group Saved Successfully");       
            }
            catch
            {
                ClassMsg.Show("GroupName already exists");
            }
                                
             //    }
             //   else
             //   {
             //       ClassMsg.Show("Groupname already exists");
             //   }
             //}            
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

        protected void btnNew_Click(object sender, EventArgs e)
        {
            NewAccountsGroup();
        }

        private void NewAccountsGroup()
        {
            Response.Redirect("AccountsGroup.aspx?mode=New");
        }

        protected void btnExit_Click(object sender, EventArgs e)
        {
            Response.Redirect("MainMenu.aspx");
        }

        protected void grdGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (grdGroup.SelectedRow.Cells[1].Text != null)
            {
                Session["GroupDetails"] = grdGroup.SelectedRow.Cells[1].Text.ToString();
                Response.Redirect("AccountsGroup.aspx?mode=Edit");
            }
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {            
            if (txtGroupName.Text != "")
            {
                string Query = "Update M_AccountsGroup Set GroupName='" + txtGroupName.Text + "',UnderGroup='" + ddlUndergroup.SelectedItem.Text + "',Completed='0',ModifiedBy='" + (string)Session["UserName"] + "',ModifiedDate='" + DateTime.Now + "' where GroupCode='" + txtGroupCode.Text + "'";

                GetCommandIMP(Query);
                btnUpdate.Enabled = false;
                ClassMsg.Show("Accounts Group Updated Successfully");               
            }            
        }        

        protected void grdGroup_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

            string sqlQuery = "Select GroupCode,GroupName,UnderGroup from M_AccountsGroup ";
            SqlConnection conn = new SqlConnection(strconn);
            conn.Open();
            SqlDataAdapter da = new SqlDataAdapter(sqlQuery, conn);
            DataSet ds = new DataSet();
            da.Fill(ds, "SQLTABLE");
            conn.Close();
            grdGroup.DataSource = ds;
            grdGroup.DataBind();
            grdGroup.PageIndex = e.NewPageIndex;
            grdGroup.DataBind();
        }

        protected void grdGroup_Sorting(object sender, GridViewSortEventArgs e)
        {
            string sortingDirection = string.Empty;
            if (dir == SortDirection.Ascending)
            {
                dir = SortDirection.Descending;
                sortingDirection = "Desc";
            }
            else
            {
                dir = SortDirection.Ascending;
                sortingDirection = "Asc";
            }

            DataView sortedView = new DataView(BindGrid());
            sortedView.Sort = e.SortExpression + " " + sortingDirection;
            grdGroup.DataSource = sortedView;
            grdGroup.DataBind();
        }

        public DataTable BindGrid()
        {
            string sqlQuery = "Select GroupCode,GroupName,UnderGroup from M_AccountsGroup ";
            SqlConnection conn = new SqlConnection(strconn);
            conn.Open();
            SqlDataAdapter da = new SqlDataAdapter(sqlQuery, conn);
            DataTable dt = new DataTable();
            da.Fill(dt);
            conn.Close();
            return dt;
        }

        public SortDirection dir
        {
            get
            {
                if (ViewState["dirState"] == null)
                {
                    ViewState["dirState"] = SortDirection.Ascending;
                }
                return (SortDirection)ViewState["dirState"];
            }
            set
            {
                ViewState["dirState"] = value;
            }
        }
    }
}
