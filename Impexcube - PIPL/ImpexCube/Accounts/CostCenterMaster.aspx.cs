using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ImpexCube.Accounts
{
    public partial class CostCenterMaster : System.Web.UI.Page
    {
        string strconn = (string)ConfigurationManager.ConnectionStrings["Constr"].ConnectionString;
        string obj;

        #region
        string costCenterName;
        string costCentercode;
        string costCenterCategory;
        string costCenterUnder;
        string Department;
        string UserName;
        string CB;
        string CD;
        string mode = "New";
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack == false)
            {
                if (mode == Request.QueryString["mode"])
                {
                    BindGridView();
                    CostCenterCode();
                    btnUpdate.Visible = false;
                    btnSave.Visible = true;
                }
                else
                {
                    BindGridView();
                    EditGroup();
                    btnUpdate.Visible = true;
                    btnSave.Visible = false;
                }
            }
        }

        private void EditGroup()
        {
            try
            {
                string sqlQuery = "Select * from M_CostCenter where TransId='" + (string)Session["CostCenterDetails"].ToString() + "'";
                SqlConnection conn = new SqlConnection(strconn);
                conn.Open();
                SqlDataAdapter da = new SqlDataAdapter(sqlQuery, conn);
                DataSet ds = new DataSet();
                da.Fill(ds, "SQLTABLE");
                conn.Close();
                if (ds.Tables["SQLTABLE"].Rows.Count != 0)
                {
                    DataRowView rv = ds.Tables["SQLTABLE"].DefaultView[0];
                    costCentercode = rv["CostCenterCode"].ToString();
                    costCenterName = rv["CostCenterName"].ToString();
                    costCenterCategory = rv["CostCenterCategory"].ToString();
                    costCenterUnder = rv["CostCenterUnder"].ToString();
                    Department = rv["Department"].ToString();
                    UserName = rv["UserName"].ToString();
                    CB = rv["CreatedBy"].ToString();
                    CD = rv["CreatedDate"].ToString();

                    txtCostCenterCode.Text = costCentercode;
                    txtCostCenterName.Text = costCenterName;
                    txtCategory.Text = costCenterCategory;
                    txtCostUnder.Text = costCenterUnder;
                    txtDepartment.Text = Department;
                    txtUserName.Text = UserName;
                }
                else
                {
                    Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "alert('Please select Valid CostCenter Name');", true);
                }
            }
            catch
            {
            }
           
        }

        private void CostCenterCode()
        {
            string CostNumber = "Select Max(CostCenterCode)+1 As CostCode from M_CostCenter";
            SqlConnection conn = new SqlConnection(strconn);
            conn.Open();
            SqlDataAdapter da = new SqlDataAdapter(CostNumber, conn);
            DataSet ds = new DataSet();
            da.Fill(ds, "SQLTABLE");
            DataRowView dt = ds.Tables["SQLTABLE"].DefaultView[0];
            conn.Close();
            string CostNo = dt["CostCode"].ToString();
            if (CostNo != "")
            {
                txtCostCenterCode.Text = CostNo;
            }
            else
            {
                txtCostCenterCode.Text = "1";
            }
        }

        private void BindGridView()
        {
            //string sqlQuery = "Select CostCenterName As [Name],CostCenterCategory As [Category],CostCenterUnder As [Under] from CostCenter ";
            string sqlQuery = "Select  TransId,CostCenterName,Department,UserName from M_CostCenter ";
            SqlConnection conn = new SqlConnection(strconn);
            conn.Open();
            SqlDataAdapter da = new SqlDataAdapter(sqlQuery, conn);
            DataSet ds = new DataSet();
            da.Fill(ds, "SQLTABLE");
            conn.Close();
            if (ds.Tables["SQLTABLE"].Rows.Count != 0)
            {
                gvCostCenter.DataSource = ds;
                gvCostCenter.DataBind();
            }
            else
            {
                gvCostCenter.DataSource = null;
                gvCostCenter.DataBind();
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            string costcode = txtCostCenterCode.Text;
            string costname = txtCostCenterName.Text;
            string category = txtCategory.Text;
            string under = txtCostUnder.Text;            
            string cb = (string)Session["UserName"];//"VTS";

            if (costname != "")
            {
                string Query = "Insert into M_CostCenter(CostCenterCode,CostCenterName,CostCenterCategory,CostCenterUnder,CreatedBy,CreatedDate,ModifiedBy,ModifiedDate,Department,UserName)values('" + costcode + "','" + costname + "','" + category + "','" + under + "','" + cb + "','" + DateTime.Now + "','" + cb + "','" + DateTime.Now + "','" + txtDepartment.Text + "','" + txtUserName.Text + "')";

                GetCommandIMP(Query);
                //NewCostCenter();    
                BindGridView();
                txtCostCenterName.Enabled = false;
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "alert('Cost center Saved Successfully');", true);
                           
            }
            else
            {
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "alert('Please fill the mandatory fields');", true);
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

        protected void gvCostCenter_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (gvCostCenter.SelectedRow.Cells[1].Text != null)
            {
                Session["CostCenterDetails"] = gvCostCenter.SelectedRow.Cells[1].Text.ToString();
                Response.Redirect("~/Accounts/CostCenterMaster.aspx");
            }
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            string CostCode = txtCostCenterCode.Text;
            string CostName = txtCostCenterName.Text;
            string CostCategory = txtCategory.Text;
            string CostUnder = txtCostUnder.Text;            
            if (CostName != "")
            {
                string Query = "Update M_CostCenter Set CostCenterName='" + CostName + "', CostCenterCategory='" + CostCategory + "',CostCenterUnder='" + CostUnder + "',ModifiedBy='" + (string)Session["UserName"] + "',ModifiedDate='" + DateTime.Now + "' where CostCenterCode='" + CostCode + "'";

                GetCommandIMP(Query);
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "alert('Cost Center Updated Successfully');", true);
                //NewCostCenter();
            }
            else
            {
                Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "alert('Please fill the mandatory fields');", true);
            }
        }

        private void NewCostCenter()
        {
            Response.Redirect("~/Accounts/CostCenterMaster.aspx?mode=New");
        }

        protected void btnNew_Click(object sender, EventArgs e)
        {
            NewCostCenter();
        }

        protected void btnExit_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Accounts/MainMenu.aspx");
        }

        protected void gvCostCenter_Sorting(object sender, GridViewSortEventArgs e)
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
            gvCostCenter.DataSource = sortedView;
            gvCostCenter.DataBind();
        }

        private DataTable BindGrid()
        {
            //string sqlQuery = "Select CostCenterName As Name,CostCenterCategory As Category,CostCenterUnder As Under from CostCenter ";
            string sqlQuery = "Select  TransId, CostCenterName from M_CostCenter ";
            SqlConnection conn = new SqlConnection(strconn);
            conn.Open();
            SqlDataAdapter da = new SqlDataAdapter(sqlQuery, conn);
            DataTable dt = new DataTable();
            da.Fill(dt);
            conn.Close();
            return dt;
        }

        protected void gvCostCenter_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
           // string sqlQuery = "Select CostCenterName As Name,CostCenterCategory As Category,CostCenterUnder As Under from CostCenter ";
            string sqlQuery = "Select  TransId, CostCenterName from M_CostCenter ";
            SqlConnection conn = new SqlConnection(strconn);
            conn.Open();
            SqlDataAdapter da = new SqlDataAdapter(sqlQuery, conn);
            DataSet ds = new DataSet();
            da.Fill(ds, "SQLTABLE");
            conn.Close();
            gvCostCenter.DataSource = ds;
            gvCostCenter.DataBind();
            gvCostCenter.PageIndex = e.NewPageIndex;
            gvCostCenter.DataBind();
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