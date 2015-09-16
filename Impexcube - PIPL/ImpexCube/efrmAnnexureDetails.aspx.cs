using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using System.Drawing;
using VTS.ImpexCube.Data;

namespace ImpexCube
{
    public partial class efrmAnnexureDetails : System.Web.UI.Page
    {
        VTS.ImpexCube.Business.ETAnnexureBL objETAnnexture = new VTS.ImpexCube.Business.ETAnnexureBL();
        VTS.ImpexCube.Business.ETAnnexureDocumentsBL objAnnexdoc = new VTS.ImpexCube.Business.ETAnnexureDocumentsBL();
        CommonDL objCommonDL = new CommonDL();
        int result;
        string strcon = (string)ConfigurationManager.ConnectionStrings["Constr"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                JobNo();                                          
                btnupdateannexdoc.Visible = false;
                btnupdateannex.Visible = false;
                btnsaveannex.Visible = true;
                btnAdd.Visible = true;
            }
        }

        protected void btnsaveannex_Click(object sender, EventArgs e)
        {
           string JobNo=ddlJobnoAnnexture.SelectedItem.Text;
           string IECodeOfEOU=txtIECode.Text;
           string BranchSNo=txtBranchSl.Text;
           string ExaminationDate=txtExamDate.Text;
           string ExaminingOfficer=txtExaminingOfficer.Text;
           string ExaminingOfficerDesignation=txtDesignation.Text;
           string SupervisingOfficer=txtSupervising.Text;
           string SupervisingOfficerDesignation=txtSuperDesignation.Text;
           string Commissionerate=txtCommissionerate.Text;
           string Division=txtDivision.Text;
           string Range=txtRange.Text;
           string VerifiedbyExaminingOfficer=cbVerified.Checked.ToString();
           string SampleForwarded=cbSample.Checked.ToString();
           string SealNumber=txtSealNo.Text;
           string CreatedBy=(string)Session["USER-NAME"];
           string CreatedDate=System.DateTime.Now.ToString();
           result=objETAnnexture.AnnexureSave(JobNo, IECodeOfEOU, BranchSNo, ExaminationDate, ExaminingOfficer, ExaminingOfficerDesignation, SupervisingOfficer,
                        SupervisingOfficerDesignation, Commissionerate, Division, Range, VerifiedbyExaminingOfficer, SampleForwarded, SealNumber, CreatedBy, CreatedDate);
           if (result == 1)
           {
               ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Saved Successfully')", true);
           }
           clear();
           
        }

        protected void btnupdateannex_Click(object sender, EventArgs e)
        {
            string JobNo = ddlJobnoAnnexture.SelectedItem.Text;
            string IECodeOfEOU = txtIECode.Text;
            string BranchSNo = txtBranchSl.Text;
            string ExaminationDate = txtExamDate.Text;
            string ExaminingOfficer = txtExaminingOfficer.Text;
            string ExaminingOfficerDesignation = txtDesignation.Text;
            string SupervisingOfficer = txtSupervising.Text;
            string SupervisingOfficerDesignation = txtSuperDesignation.Text;
            string Commissionerate = txtCommissionerate.Text;
            string Division = txtDivision.Text;
            string Range = txtRange.Text;
            string VerifiedbyExaminingOfficer = cbVerified.Checked.ToString();
            string SampleForwarded = cbSample.Checked.ToString();
            string SealNumber = txtSealNo.Text;            
            string ModifiedBy = (string)Session["USER-NAME"];
            string ModifiedDate = System.DateTime.Now.ToString();
            result=objETAnnexture.AnnexureUpdate(JobNo, IECodeOfEOU, BranchSNo, ExaminationDate, ExaminingOfficer, ExaminingOfficerDesignation, SupervisingOfficer,
                         SupervisingOfficerDesignation, Commissionerate, Division, Range, VerifiedbyExaminingOfficer, SampleForwarded, SealNumber, ModifiedBy, ModifiedDate);
            if (result == 1)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Updated Successfully')", true);
            }
            clear();
            gvAnnexure.Visible = false;
            btnsaveannex.Visible = true;
            btnupdateannex.Visible = false;

        }

        protected void ddlJobnoshipmain_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataSet ds = new DataSet();      
            string query1 = "select JobNo, IECodeOfEOU, BranchSNo, ExaminationDate, ExaminingOfficer, ExaminingOfficerDesignation, SupervisingOfficer,SupervisingOfficerDesignation, Commissionerate, Division, Range, VerifiedbyExaminingOfficer, SampleForwarded, SealNumber from E_T_Annexure where JobNo = '" + ddlJobnoAnnexture.SelectedValue + "'";                       
            ds = objCommonDL.GetDataSet(query1);
           
 
            DataSet dsCount = new DataSet();
            dsCount = objAnnexdoc.GetAnnexure(ddlJobnoAnnexture.SelectedValue);
            int count = dsCount.Tables["Table"].Rows.Count;
            if (count == 0)
            {
                lblsno.Text = "1";
                gvAnnexure.Visible = false;
            }
            else
            {
                count = count + 1;
                lblsno.Text = count.ToString();
                gvAnnexure.DataSource = dsCount;
                gvAnnexure.DataBind();
                gvAnnexure.Visible = true;
            }
            if (ds.Tables["Table"].Rows.Count != 0)
            {
                jobbind();                
                DataRowView row = ds.Tables["Table"].DefaultView[0];
                ddlJobnoAnnexture.SelectedItem.Text = row["JobNo"].ToString();
                txtIECode.Text = row["IECodeOfEOU"].ToString();
                txtBranchSl.Text = row["BranchSNo"].ToString();
                txtExamDate.Text = row["ExaminationDate"].ToString();
                txtExaminingOfficer.Text = row["ExaminingOfficer"].ToString();
                txtDesignation.Text = row["ExaminingOfficerDesignation"].ToString();
                txtSupervising.Text = row["SupervisingOfficer"].ToString();
                txtSuperDesignation.Text = row["SupervisingOfficerDesignation"].ToString();
                txtCommissionerate.Text = row["Commissionerate"].ToString();
                txtDivision.Text = row["Division"].ToString();
                txtRange.Text = row["Range"].ToString();
                bool chkboxverifiedexam = Convert.ToBoolean(row["VerifiedbyExaminingOfficer"]);
                cbVerified.Checked = chkboxverifiedexam;
                bool chkboxsampleverify = Convert.ToBoolean(row["SampleForwarded"]);
                cbSample.Checked = chkboxsampleverify;
                
                txtSealNo.Text = row["SealNumber"].ToString();
                btnupdateannex.Visible = true;
                btnupdateannexdoc.Visible = false;
                btnsaveannex.Visible = false;
                btnAdd.Visible = true;
                
                //DataSet ds1 = new DataSet();
                //ds1 = objAnnexdoc.GetAnnexure(ddlJobnoAnnexture.SelectedValue);                
               // gvAnnexure.DataSource = ds1;
               // gvAnnexure.DataBind();
               // //int count = ds1.Tables["Table"].Rows.Count;
               // //count = count + 1;
               //// lblsno.Text = count.ToString();
               // gvAnnexure.Visible = true;

               
            }
            else
            {
                clear();               
                btnsaveannex.Visible = true;
                btnAdd.Visible = true;
                btnupdateannexdoc.Visible = false;
                btnupdateannex.Visible = false;
            }

            
        }
        //public void annexdocbind()
        //{            
        //    string query1 = "select Sno,DocumentName from E_T_AnnexureDocuments where JobNo like '%" + ddlJobnoAnnexture.SelectedValue + "%'";            
        //    DataSet ds = new DataSet();
        //    ds = objCommonDL.GetDataSet(query1);
        //    if (ds.Tables["Table"].Rows.Count != 0)
        //    {
        //        DataRowView row = ds.Tables["Table"].DefaultView[0];
        //        lbl.Text = row["Sno"].ToString();
        //        txtDocumentation.Text = row["DocumentName"].ToString();


        //    }
            
        //}
        protected void btnAdd_Click(object sender, EventArgs e)
        {
            string JobNo = ddlJobnoAnnexture.SelectedItem.Text;
            string Sno=lblsno.Text;
            string DocumentName=txtDocumentation.Text;
            string CreatedBy = (string)Session["USER-NAME"];
            string CreatedDate = System.DateTime.Now.ToString();
            DataSet ds = new DataSet();
            result = objAnnexdoc.SaveAnnexureDocuments(JobNo, Sno, DocumentName, CreatedBy, CreatedDate);

            Gridload();
            if (result == 1)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Add Successfully')", true);
            }
            clear1();
        }

        protected void btnupdateannexdoc_Click(object sender, EventArgs e)
        {
            btnsaveannex.Visible = false;
            string JobNo=ddlJobnoAnnexture.SelectedItem.Text;
            string Sno=lblsno.Text;
            string DocumentName=txtDocumentation.Text;
            string ModifiedBy=(string)Session["USER-NAME"];
            string ModifiedDate = System.DateTime.Now.ToString();
            DataSet ds = new DataSet();   
            result = objAnnexdoc.UpdateAnnexureDocuments(JobNo, Sno, DocumentName, ModifiedBy, ModifiedDate);                     
            btnupdateannexdoc.Visible = true;
            btnAdd.Visible = false;                       
            Gridload();
            if (result == 1)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Updated Successfully')", true);
            }
            clear1();
            //Response.Redirect("~/efrmAnnexureDetails.aspx");
            btnAdd.Visible = true;
            btnupdateannexdoc.Visible = false;
        }
        public void JobNo()
        {
            DataSet ds = new DataSet();
            string quer = "select * from E_M_JobCreation";                        
            ds=objCommonDL.GetDataSet(quer);
            if (ds.Tables["Table"].Rows.Count != 0)
            {                
                ddlJobnoAnnexture.DataSource = ds;
                ddlJobnoAnnexture.DataTextField = "JobNo";
                ddlJobnoAnnexture.DataValueField = "JobNo";
                ddlJobnoAnnexture.DataBind();
            }
        }
        public void jobbind()
        {
            DataSet ds = new DataSet();
            string query1 = "select JobNo,JobDate from E_M_JobCreation where JobNo like '%" + ddlJobnoAnnexture.SelectedValue + "%'";
            ds = objCommonDL.GetDataSet(query1);            
            if (ds.Tables["Table"].Rows.Count != 0)
            {
                DataRowView row = ds.Tables["Table"].DefaultView[0];
                ddlJobnoAnnexture.SelectedItem.Text = row["JobNo"].ToString();
                lblJobDate.Text = row["JobDate"].ToString();


            }

        }
        public void clear()
        {
            //ddlJobnoAnnexture.SelectedValue = "~Select~";
            lblJobDate.Text = "";
            txtIECode.Text = "";
            txtBranchSl.Text = "";
            txtExamDate.Text = "";
            txtExaminingOfficer.Text = "";
            txtDesignation.Text = "";
            txtSupervising.Text = "";
            txtSuperDesignation.Text = "";
            txtCommissionerate.Text = "";
            cbVerified.Checked = false;
            cbSample.Checked = false;
            txtDivision.Text = "";
            txtRange.Text = "";
            txtSealNo.Text = "";
        }
        public void clear1()
        {
            //txtSrNo.Text = "";
            txtDocumentation.Text = "";
        }

        protected void btnReturn_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/efrmExporterDetails.aspx");
        }
        private void Gridload()
        {
            DataSet DS = new DataSet();
            DS = objAnnexdoc.GetAnnexure(ddlJobnoAnnexture.SelectedValue);
            int count = DS.Tables["Table"].Rows.Count;
            count = count + 1;
            lblsno.Text = count.ToString();
     
            if (DS.Tables["Table"].Rows.Count != 0)
            {
                gvAnnexure.DataSource = DS;
                gvAnnexure.DataBind();
                gvAnnexure.Visible = true;
            }
            else
            {
                gvAnnexure.DataSource = null;
                gvAnnexure.DataBind();
            }
        }

        protected void gvAnnexure_SelectedIndexChanged(object sender, EventArgs e)
        {
            string JobNo = ddlJobnoAnnexture.SelectedValue;
            lblsno.Text = gvAnnexure.SelectedRow.Cells[1].Text;
            DataSet DS = new DataSet();
            string Sno = lblsno.Text;
            DS = objAnnexdoc.GetAnnexureData(Sno, JobNo);        
            DataRowView dr = DS.Tables["Table"].DefaultView[0];
            lblsno.Text = dr["Sno"].ToString();
            txtDocumentation.Text = dr["DocumentName"].ToString();
            btnAdd.Visible = false;            
            btnupdateannexdoc.Visible = true;                       
        }

        protected void btncancelannex_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/efrmAnnexureDetails.aspx");
        }

        protected void btncloseannex_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/HomePage.aspx");
        }
       
        
    }
}  
            
            
            
            
           