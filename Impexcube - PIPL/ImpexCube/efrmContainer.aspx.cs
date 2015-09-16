using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using VTS.ImpexCube.Business;
using VTS.ImpexCube.Data;
using System.Data;

namespace ImpexCube
{
    public partial class efrmContainer : System.Web.UI.Page
    {
        ETContainerBL objETContainer = new ETContainerBL();
        CommonDL objCommonDL = new CommonDL();

        string strconn = (string)ConfigurationManager.AppSettings["ConnectionDashboard"];

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Session["ID"] = string.Empty;
                JobNo();
                jobbind();
                btnUpdate.Visible = false;
            }
        }

        protected void gvContainer_SelectedIndexChanged(object sender, EventArgs e)
        {
            //btnUpdate.Visible = true;
            //btnAdd.Visible = false;
            //string JobNo = gvContainer.SelectedRow.Cells[1].Text;            
            //DataSet ds = new DataSet();
            //ds = objETContainer.GetContainerData(JobNo);
            //if (ds.Tables["Table"].Rows.Count != 0)
            //{
            //    //DataRowView row = ds.Tables["Table"].DefaultView[0];
            //    //txtContainerNo.Text = row["ContainerNo"].ToString();
            //    //txtSealNo.Text = row["SealNo"].ToString();
            //    //txtSealDate.Text = row["SealDate"].ToString();
            //    //txtType.Text = row["Type"].ToString();
            //    //txtPktsStuffed.Text = row["NoofPktsStuffed"].ToString();

            //    ddlJobNoContainer.SelectedItem.Text = gvContainer.SelectedRow.Cells[1].Text;
            //    DataSet DS = new DataSet();
            //    string QUERY1 = "SELECT JobNo,ContainerNo, SealNo, SealDate, Type, NoofPktsStuffed FROM E_T_Container where JobNo like '%" + ddlJobNoContainer.SelectedValue + "%'"; 
            //    DS = objCommonDL.GetDataSet(QUERY1);
            //    DataRowView dr = DS.Tables["Table"].DefaultView[0];
            //    txtContainerNo.Text = dr["ContainerNo"].ToString();
            //    txtSealNo.Text = dr["SealNo"].ToString();
            //    txtSealDate.Text = dr["SealDate"].ToString();
            //    txtType.Text = dr["Type"].ToString();
            //    txtPktsStuffed.Text = dr["NoofPktsStuffed"].ToString();                

            Session["ID"] = gvContainer.SelectedRow.Cells[1].Text;
            DataSet DS = new DataSet();
            string QUERY1 = "SELECT ID,JobNo,ContainerNo, SealNo, SealDate, Type, NoofPktsStuffed FROM E_T_Container where ID ='" + (string)Session["ID"] + "'";
            DS = objCommonDL.GetDataSet(QUERY1);
            DataRowView dr = DS.Tables["Table"].DefaultView[0];
            txtContainerNo.Text = dr["ContainerNo"].ToString();
            txtSealNo.Text = dr["SealNo"].ToString();
            txtSealDate.Text = dr["SealDate"].ToString();
            txtType.Text = dr["Type"].ToString();
            txtPktsStuffed.Text = dr["NoofPktsStuffed"].ToString();
            btnUpdate.Visible = true;
            btnAdd.Visible = false;



        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {

            string JobNo = ddlJobNoContainer.SelectedItem.Text;
            string ContainerNo = txtContainerNo.Text;
            string SealNo = txtSealNo.Text;
            string SealDate = txtSealDate.Text;
            string Type = txtType.Text;
            string NoofPktsStuffed = txtPktsStuffed.Text;
            string CreatedBy = (string)Session["USER-NAME"];
            string CreatedDate = System.DateTime.Now.ToString();

            objETContainer.SaveContainer(JobNo, ContainerNo, SealNo, SealDate, Type, NoofPktsStuffed, CreatedBy, CreatedDate);

            ScriptManager.RegisterStartupScript(this, this.GetType(), "redirect", "alert('Saved Successfully');", true);
            Gridload();
            clear();

        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {

            string JobNo = ddlJobNoContainer.SelectedItem.Text;
            string ContainerNo = txtContainerNo.Text;
            string SealNo = txtSealNo.Text;
            string SealDate = txtSealDate.Text;
            string Type = txtType.Text;
            string NoofPktsStuffed = txtPktsStuffed.Text;
            string ModifiedBy = (string)Session["USER-NAME"];
            string ModifiedDate = System.DateTime.Now.ToString();

            objETContainer.UpdateContainer((string)Session["ID"], JobNo, ContainerNo, SealNo, SealDate, Type, NoofPktsStuffed, ModifiedBy, ModifiedDate);

            ScriptManager.RegisterStartupScript(this, this.GetType(), "redirect", "alert('Updated Successfully'); ", true);
            Gridload();
            clear();
            btnUpdate.Visible = false;
            btnAdd.Visible = true;
        }


        protected void ddlJobNoContainer_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataSet ds = new DataSet();
            string quer = "select * from E_M_JobCreation where jobNo = '" + ddlJobNoContainer.SelectedItem.Text + "'";
            ds = objCommonDL.GetDataSet(quer);
            if (ds.Tables["Table"].Rows.Count != 0)
            {
                DataRowView row = ds.Tables["Table"].DefaultView[0];

                lblJobDate.Text = row["JobDate"].ToString();
            }
            Gridload();
            clear();
        }
       
        public void JobNo()
        {
            DataSet ds = new DataSet();
            string quer = "select * from E_M_JobCreation";
            ds = objCommonDL.GetDataSet(quer);
            if (ds.Tables["Table"].Rows.Count != 0)
            {
                ddlJobNoContainer.DataSource = ds;
                ddlJobNoContainer.DataTextField = "JobNo";
                ddlJobNoContainer.DataValueField = "JobNo";
                ddlJobNoContainer.DataBind();
            }
        }

        public void jobbind()
        {
            DataSet ds = new DataSet();
            string query1 = "select ID,JobNo,JobDate from E_M_JobCreation where JobNo like '%" + ddlJobNoContainer.SelectedValue + "%'";
            ds = objCommonDL.GetDataSet(query1);
            if (ds.Tables["Table"].Rows.Count != 0)
            {
                DataRowView row = ds.Tables["Table"].DefaultView[0];
                ddlJobNoContainer.SelectedItem.Text = row["JobNo"].ToString();
                lblJobDate.Text = row["JobDate"].ToString();


            }

        }

        private void Gridload()
        {
            gvContainer.DataBind();
            Session["query"] = string.Empty;
            string QUERY1;
            QUERY1 = "SELECT distinct ID,JobNo, ContainerNo,SealNo,SealDate,Type,NoofPktsStuffed FROM E_T_Container where JobNo like '%" + ddlJobNoContainer.SelectedValue + "%'";
            Session["query"] = QUERY1;
            DataSet DS = new DataSet();
            DS = objCommonDL.GetDataSet(QUERY1);
            if (DS.Tables["Table"].Rows.Count != 0)
            {
                gvContainer.DataSource = DS;
                gvContainer.DataBind();
            }
            else
            {
                gvContainer.DataSource = null;
                gvContainer.DataBind();
            }


        }

        protected void clear()
        {
            txtContainerNo.Text = "";
            txtSealNo.Text = "";
            txtSealDate.Text = "";
            txtType.Text = "";
            txtPktsStuffed.Text = "";
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            clear();
        }

        protected void btnClose_Click(object sender, EventArgs e)
        {
            Response.Redirect("efrmContainer.aspx");
        }

     

    }
}