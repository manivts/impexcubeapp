using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.DataVisualization.Charting;
using System.Drawing;
using System.Configuration;
using VTS.ImpexCube.Data;

namespace VTS.ImpexCube.Web
{
    public partial class HomePage : System.Web.UI.Page
    {
        string strPIPL = (string)ConfigurationManager.ConnectionStrings["Constr"].ConnectionString;
        VTS.ImpexCube.Business.DashBoardBL objDashBoard = new VTS.ImpexCube.Business.DashBoardBL();
        VTS.ImpexCube.Business.FundRequest objFundRequest = new VTS.ImpexCube.Business.FundRequest();
        CommonDL objCommon = new CommonDL();

        #region Global  
        string Uid = string.Empty;
        string Uname = string.Empty;
        string Grade = string.Empty;
        string Query = string.Empty;
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            Uid = (string)Session["UID"];
            Uname = (string)Session["USER-NAME"];
            Grade = (string)Session["Grade"];
            if (IsPostBack == false)
            {
                lblUserDashBoard.Text = (string)Session["USER-NAME"]+" DashBoard";
                Uid = (string)Session["UID"];
                Uname = (string)Session["USER-NAME"];
                Grade = (string)Session["Grade"];
                UserPermission();
                Import();
                Export();
                FundRequest();
                billing();
            }
            Label pagename;
            pagename = (Label)Master.FindControl("lblName");
            pagename.Text = "Impex Cube";
        }

        public void UserPermission()
        {
            try
            {
                SqlConnection conn = new SqlConnection(strPIPL);
                string qry = "Exec UserPermission @columns = '0',@convert = '0',@EmpId='" + (string)Session["UID"] + "'";
                SqlCommand cmd = new SqlCommand(qry, conn);
                cmd.CommandType = CommandType.StoredProcedure;
                conn.Open();
                SqlDataAdapter da = new SqlDataAdapter(qry, conn);
                DataSet ds = new DataSet();
                da.Fill(ds, "SQLTABLE");
                conn.Close();
                if (ds.Tables["SQLTABLE"].Rows.Count != 0)
                {
                    DataRowView dr = ds.Tables["SQLTABLE"].DefaultView[0];
                    tblImport.Visible = Convert.ToBoolean(Convert.ToInt16(dr["tblIMport"].ToString()));
                    tblExport.Visible = Convert.ToBoolean(Convert.ToInt16(dr["tblExport"].ToString()));
                    tblCRM.Visible = Convert.ToBoolean(Convert.ToInt16(dr["tblCRM"].ToString()));
                    tblFund.Visible = Convert.ToBoolean(Convert.ToInt16(dr["tblFund"].ToString()));
                    //tblAccounts.Visible = Convert.ToBoolean(Convert.ToInt16(dr["Accounts"].ToString()));
                    //tblBilling.Visible = Convert.ToBoolean(Convert.ToInt16(dr["Billing"].ToString()));
                }
            }
            catch (Exception ex)
            {
                //ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('DataBase Error: " + ex.Message + "');", true);
            }
        }

        public void Import()
        {
            ImpJob();
            ImpShipMent();
            ImpInvoice();
            ImpProduct();
            ImpBEFile();
        }

        public void Export()
        {
            ExpJob();
            ExpShipMent();
            ExpInvoice();
            ExpProduct();
            ExpSBFile();
        }

        public void FundRequest()
        {
            Fund();
            Operation();
            Accounts();
        }

        public void ImpJob()
        {
            DataSet ds = objDashBoard.SelectImpJob(Uid,Uname,Grade);
            if (ds.Tables["Table"].Rows.Count != 0)
            {
                DataRowView dr = ds.Tables["Table"].DefaultView[0];
                lblImpJob.Text = "( " + dr["JobNo"].ToString() + " )";
            }
            else
            {
                lblImpJob.Text = "(0)";
            }
        }

        public void ImpShipMent()
        {
            DataSet ds = objDashBoard.SelectImpShipMent(Uid, Uname, Grade);
            if (ds.Tables["Table"].Rows.Count != 0)
            {
                DataRowView dr = ds.Tables["Table"].DefaultView[0];
                lblImpShip.Text = "( " + dr["JobNo"].ToString() + " )";
            }
            else
            {
                lblImpShip.Text = "(0)";
            }
            //lblImpShip.Text = "(" + Convert.ToString(ds.Tables["Table"].Rows.Count) + ")";
        }

        public void ImpInvoice()
        {
            DataSet ds = objDashBoard.SelectImpInvoice(Uid, Uname, Grade);
            if (ds.Tables["Table"].Rows.Count != 0)
            {
                DataRowView dr = ds.Tables["Table"].DefaultView[0];
                lblImpInvoice.Text = "( " + dr["JobNo"].ToString() + " )";
            }
            else
            {
                lblImpInvoice.Text = "(0)";
            }
            //lblImpInvoice.Text = "(" + Convert.ToString(ds.Tables["Table"].Rows.Count) + ")";
        }

        public void ImpProduct()
        {
            DataSet ds = objDashBoard.SelectImpProduct(Uid, Uname, Grade);
            if (ds.Tables["Table"].Rows.Count != 0)
            {
                DataRowView dr = ds.Tables["Table"].DefaultView[0];
                lblImpProduct.Text = "( " + dr["JobNo"].ToString() + " )";
            }
            else
            {
                lblImpProduct.Text = "(0)";
            }
            //lblImpProduct.Text = "(" + Convert.ToString(ds.Tables["Table"].Rows.Count) + ")";
        }

        public void ImpBEFile()
        {
            DataSet ds = objDashBoard.SelectBEFile(Uid, Uname, Grade);
            if (ds.Tables["Table"].Rows.Count != 0)
            {
                DataRowView dr = ds.Tables["Table"].DefaultView[0];
                lblImpBE.Text = "( " + dr["JobNo"].ToString() + " )";
            }
            else
            {
                lblImpBE.Text = "(0)";
            }
            //lblImpBE.Text = "(" + Convert.ToString(ds.Tables["Table"].Rows.Count) + ")";
        }

        public void ExpJob()
        {
            DataSet ds = objDashBoard.SelectExpJob(Uid, Uname, Grade);
            if (ds.Tables["Table"].Rows.Count != 0)
            {
                DataRowView dr = ds.Tables["Table"].DefaultView[0];
                lblExpJob.Text = "( " + dr["JobNo"].ToString() + " )";
            }
            else
            {
                lblExpJob.Text = "(0)";
            }
            //lblExpJob.Text = "(" + Convert.ToString(ds.Tables["Table"].Rows.Count) + ")";
        }

        public void ExpShipMent()
        {
            DataSet ds = objDashBoard.SelectExpShipMent(Uid, Uname, Grade);
            if (ds.Tables["Table"].Rows.Count != 0)
            {
                DataRowView dr = ds.Tables["Table"].DefaultView[0];
                lblExpShipment.Text = "( " + dr["JobNo"].ToString() + " )";
            }
            else
            {
                lblExpShipment.Text = "(0)";
            }
            //lblExpShipment.Text = "(" + Convert.ToString(ds.Tables["Table"].Rows.Count) + ")";
        }

        public void ExpInvoice()
        {
            DataSet ds = objDashBoard.SelectExpInvoice(Uid, Uname, Grade);
            if (ds.Tables["Table"].Rows.Count != 0)
            {
                DataRowView dr = ds.Tables["Table"].DefaultView[0];
                lblExpInvoice.Text = "( " + dr["JobNo"].ToString() + " )";
            }
            else
            {
                lblExpInvoice.Text = "(0)";
            }
            //lblExpInvoice.Text = "(" + Convert.ToString(ds.Tables["Table"].Rows.Count) + ")";
        }

        public void ExpProduct()
        {
            DataSet ds = objDashBoard.SelectExpProduct(Uid, Uname, Grade);
            if (ds.Tables["Table"].Rows.Count != 0)
            {
                DataRowView dr = ds.Tables["Table"].DefaultView[0];
                lblExpProduct.Text = "( " + dr["JobNo"].ToString() + " )";
            }
            else
            {
                lblExpProduct.Text = "(0)";
            }
            //lblExpProduct.Text = "(" + Convert.ToString(ds.Tables["Table"].Rows.Count) + ")";
        }

        public void ExpSBFile()
        {
            DataSet ds = objDashBoard.SelectSBFile(Uid, Uname, Grade);
            lblExpSB.Text = "(" + Convert.ToString(ds.Tables["Table"].Rows.Count) + ")";
        }

        public void Fund()
        {
            string user = (string)Session["USER-NAME"];
            DataSet ds = objFundRequest.FundRequestPending(user);
            if (ds.Tables["fund"].Rows.Count != 0)
            {
                DataRowView dr = ds.Tables["fund"].DefaultView[0];
                //lblFund.Text = dr["JobNo"].ToString();
            }
            else
            {
                lblFund.Text = "(0)";
            }
            //lblFund.Text = "(" + Convert.ToString(ds.Tables["fund"].Rows.Count) + ")";
        }

        public void Operation()
        {
            DataSet ds = objDashBoard.SelectOperation(Uid, Uname, Grade);
            if (ds.Tables["Table"].Rows.Count != 0)
            {
                DataRowView dr = ds.Tables["Table"].DefaultView[0];
                //lblOperation.Text = dr["JobNo"].ToString();
            }
            else
            {
                lblOperation.Text = "(0)";
            }
            //lblOperation.Text = "(" + Convert.ToString(ds.Tables["Table"].Rows.Count) + ")";
        }

        public void Accounts()
        {
            DataSet ds = objDashBoard.SelectAccounts(Uid, Uname, Grade);
            if (ds.Tables["Table"].Rows.Count != 0)
            {
                DataRowView dr = ds.Tables["Table"].DefaultView[0];
                //lblAccMgr.Text = dr["JobNo"].ToString();
            }
            else
            {
                lblAccMgr.Text = "(0)";
            }
            //lblAccMgr.Text = "(" + Convert.ToString(ds.Tables["Table"].Rows.Count) + ")";
        }

        public void billing()
        {
            DataSet ds = objDashBoard.SelectAccounts(Uid, Uname, Grade);
            if (ds.Tables["Table"].Rows.Count != 0)
            {
                DataRowView dr = ds.Tables["Table"].DefaultView[0];
                //lblAccMgr.Text = dr["JobNo"].ToString();
            }
            else
            {
                lblAccMgr.Text = "(0)";
            }
            //lblAccMgr.Text = "(" + Convert.ToString(ds.Tables["Table"].Rows.Count) + ")";
        }

        public void lnkImpJob_Click(object sender, EventArgs e)
        {
            gvDashBoard.DataBind();
            DataSet dsIJob = objDashBoard.ViewImpJob(Uid, Uname, Grade);
            gvDashBoard.DataSource = dsIJob;
            gvDashBoard.DataBind();
        }

        protected void lnkImpShip_Click(object sender, EventArgs e)
        {
            gvDashBoard.DataBind();
            DataSet dsImpShip = objDashBoard.ViewImpShipMent(Uid, Uname, Grade);
            gvDashBoard.DataSource = dsImpShip;
            gvDashBoard.DataBind();
        }

        protected void lnkImpInvoice_Click(object sender, EventArgs e)
        {
            gvDashBoard.DataBind();
            DataSet dsImpInvoice = objDashBoard.ViewImpInvoice(Uid, Uname, Grade);
            gvDashBoard.DataSource = dsImpInvoice;
            gvDashBoard.DataBind();
        }

        protected void lnkImpProduct_Click(object sender, EventArgs e)
        {
            gvDashBoard.DataBind();
            DataSet dsImpProduct = objDashBoard.ViewImpProduct(Uid, Uname, Grade);
            gvDashBoard.DataSource = dsImpProduct;
            gvDashBoard.DataBind();
        }

        protected void lnkImpBE_Click(object sender, EventArgs e)
        {
            gvDashBoard.DataBind();
            DataSet dsImpBE = objDashBoard.SelectBEFile(Uid, Uname, Grade);
            gvDashBoard.DataSource = dsImpBE;
            gvDashBoard.DataBind();
        }

        protected void lnkExpJob_Click(object sender, EventArgs e)
        {
            gvDashBoard.DataBind();
            DataSet dsExpJob = objDashBoard.ViewExpJob(Uid, Uname, Grade);
            gvDashBoard.DataSource = dsExpJob;
            gvDashBoard.DataBind();
        }

        protected void lnkExpShipment_Click(object sender, EventArgs e)
        {
            gvDashBoard.DataBind();
            DataSet dsExpShip = objDashBoard.ViewExpShipMent(Uid, Uname, Grade);
            gvDashBoard.DataSource = dsExpShip;
            gvDashBoard.DataBind();
        }

        protected void lnkExpInvoice_Click(object sender, EventArgs e)
        {
            gvDashBoard.DataBind();
            DataSet dsExpInvoice = objDashBoard.ViewExpInvoice(Uid, Uname, Grade);
            gvDashBoard.DataSource = dsExpInvoice;
            gvDashBoard.DataBind();
        }

        protected void lnkExpProduct_Click(object sender, EventArgs e)
        {
            gvDashBoard.DataBind();
            DataSet dsExpProduct = objDashBoard.ViewExpProduct(Uid, Uname, Grade);
            gvDashBoard.DataSource = dsExpProduct;
            gvDashBoard.DataBind();
        }

        protected void lnkExpSB_Click(object sender, EventArgs e)
        {
            gvDashBoard.DataBind();
            DataSet dsExpSB = objDashBoard.SelectSBFile(Uid, Uname, Grade);
            gvDashBoard.DataSource = dsExpSB;
            gvDashBoard.DataBind();
        }

        protected void lnkEnquiry_Click(object sender, EventArgs e)
        {

        }

        protected void lnkFundReq_Click(object sender, EventArgs e)
        {
            gvDashBoard.DataBind();
            string user = (string)Session["USER-NAME"];
            DataSet dsFund = objFundRequest.FundRequestPending(user);
            gvDashBoard.DataSource = dsFund;
            gvDashBoard.DataBind();

        }

        protected void lnkOpMgr_Click(object sender, EventArgs e)
        {
            gvDashBoard.DataBind();
            DataSet dsOpMgr = objDashBoard.SelectOperation(Uid, Uname, Grade);
            gvDashBoard.DataSource = dsOpMgr;
            gvDashBoard.DataBind();
        }

        protected void lnkAccMgr_Click(object sender, EventArgs e)
        {
            gvDashBoard.DataBind();
            DataSet dsAccMgr = objDashBoard.SelectAccounts(Uid, Uname, Grade);
            gvDashBoard.DataSource = dsAccMgr;
            gvDashBoard.DataBind();
        }
    }
}