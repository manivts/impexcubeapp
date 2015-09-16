using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Drawing;
using System.Data.SqlClient;

namespace ImpexCube
{
    public partial class frmDetentionListJobNo : System.Web.UI.Page
    {
        string strconn = (string)ConfigurationManager.AppSettings["ConnectionAdmin"];
        #region
        Int32 i;
        int lngcol;
        string lstTxt;
        //    string PName;

        string jobNo;

        string QrySt;


        #endregion
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack == false)
            {
                try
                {
                    lstShowField.DataSource = GetiJobs();
                    lstShowField.DataTextField = "jno";
                    lstShowField.DataValueField = "jno";

                    lstShowField.DataBind();
                    //  GetStatus(); // To get reports status for Detention
                    ExportPage.Visible = false;
                }
                catch (Exception ex)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('" + ex.Message + "');", true);
                }
            }
        }
        public DataSet GetiJobs()
        {
            SqlConnection conn1 = new SqlConnection(strconn);
            string lstrsql = "";
            lstrsql = " select * from Detention_mstN ";
            SqlDataAdapter da = new SqlDataAdapter(lstrsql, conn1);
            DataSet ds = new DataSet();
            da.Fill(ds, "iworkReg");
            return ds;
        }

        protected void GetJobsWise(string jno)
        {
            SqlConnection conn = new SqlConnection(strconn);
            string str = "select * from Detention_mstN where jno in(" + jno + ") order by jno";
            SqlDataAdapter da = new SqlDataAdapter(str, conn);
            DataSet ds = new DataSet();
            da.Fill(ds, "DET");
            DataTable dt = ds.Tables["DET"];

            GridView1.DataSource = Transpose(dt);
            GridView1.DataBind();
        }
        private DataTable Transpose(DataTable dt)
        {

            DataTable dtNew = new DataTable();

            //adding columns	
            for (int i = 0; i <= dt.Rows.Count; i++)
            {
                dtNew.Columns.Add(i.ToString());
            }

            for (int i = 0; i < dt.Rows.Count; i++)
            {

                dtNew.Columns[i + 1].ColumnName = dt.Rows[i].ItemArray[0].ToString();
            }

            //Adding Row Data . . . 28=dt.Columns.Count
            for (int k = 1; k < 28; k++)
            {
                DataRow r = dtNew.NewRow();
                r[0] = dt.Columns[k].ToString();
                for (int j = 1; j <= dt.Rows.Count; j++)
                    r[j] = dt.Rows[j - 1][k];
                dtNew.Rows.Add(r);
            }

            return dtNew;
        }

        protected void ExportPage_Click(object sender, EventArgs e)
        {
            try
            {
                string sysDates = DateTime.Now.ToString("dd-MMM-yyyy");
                string FileName = "DetentionReport" + sysDates;
                string strFileName = FileName + ".xls";
                jobNo = Session["JNO"].ToString();
                GetJobsWise(jobNo);

                // GridViewExportDet.Export(strFileName, this.GridView1);
                GridViewExportDet.ExportExcellJobs(strFileName, GridView1);
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('" + ex.Message + "');", true);
            }
        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.RowType == DataControlRowType.Header)
            {
                e.Row.Cells[0].Text = "<b>Reference</b>";
            }
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                string VAL = e.Row.Cells[0].Text;
                switch (VAL)
                {

                    case "jobNo":
                        e.Row.Cells[0].Text = "<b>Job No</b>";
                        break;
                    case "jobDate":
                        e.Row.Cells[0].Text = "<b>Job Date</b>";
                        break;
                    case "Particulars":
                        e.Row.Cells[0].Text = "<b>Particulars</b>";
                        break;
                    case "supplier":
                        e.Row.Cells[0].Text = "<b>Supplier</b>";
                        break;
                    case "Doc_Date":
                        e.Row.Cells[0].Text = "<b>Receipt of Advance Set of Documents</b>";
                        break;
                    case "ODOC_Date":
                        e.Row.Cells[0].Text = "<b>Receipt Original Documents</b>";
                        break;
                    case "Berth_of_vessel":
                        e.Row.Cells[0].Text = "<b>Berthing of Vessel</b>";
                        break;
                    case "IGM_NO":
                        e.Row.Cells[0].Text = "<b>IGM No & Date</b>";
                        break;
                    case "BOE_Date":
                        e.Row.Cells[0].Text = "<b>BOE Noting Date</b>";
                        break;
                    case "Ground_Rent_Upto":
                        e.Row.Cells[0].Text = "<b>Ground Rent free upto</b>";
                        break;
                    case "Detention_Free":
                        e.Row.Cells[0].Text = "<b>Detention free upto</b>";
                        break;
                    case "Move_port_CFS":
                        e.Row.Cells[0].Text = "<b>Movement from Port to CFS</b>";
                        break;
                    case "Move_CFS_Plant":
                        e.Row.Cells[0].Text = "<b>Movement from CFS to Plant</b>";
                        break;
                    case "Empty_cont_Return_Date":
                        e.Row.Cells[0].Text = "<b>Empty Containers returned on</b>";
                        break;
                    case "BPT_Damrage":
                        e.Row.Cells[0].Text = "<b>Ground Rent CFS</b>";
                        break;
                    case "Detention_Charge":
                        e.Row.Cells[0].Text = "<b>Detention Charge/Shipping Line</b>";
                        break;
                    case "Damage_Charge":
                        e.Row.Cells[0].Text = "<b>Damage Charge</b>";
                        break;
                    case "Reasons":
                        e.Row.Cells[0].Text = "<b>Reasons:-</b>";
                        break;
                    case "t1":
                        e.Row.Cells[0].Text = "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Document Received-Date";
                        break;
                    case "t2":
                        e.Row.Cells[0].Text = "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Orgl Doc recd Date";
                        break;
                    case "t3":
                        e.Row.Cells[0].Text = "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Vessel Arrived On";
                        break;
                    case "t4":
                        e.Row.Cells[0].Text = "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Duty Informed Date";
                        break;
                    case "t5":
                        e.Row.Cells[0].Text = "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Duty Received Date";
                        break;
                    case "t6":
                        e.Row.Cells[0].Text = "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Examination Completed Date";
                        break;
                    case "t7":
                        e.Row.Cells[0].Text = "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Truck Indent Programmed Sent On";
                        break;
                    case "t8":
                        e.Row.Cells[0].Text = "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Remarks";
                        break;
                    case "t9":
                        e.Row.Cells[0].Text = "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;";
                        break;

                }

            }

        }
        protected void BtnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/PIPL/JobReports/Index.aspx");

        }
        protected void BtnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                string lstr1 = lstShowField.Text;
                if (lstr1 == "")
                {
                    Response.Write("<script>alert('Which Data Want to be Add from  List')</script>");

                }
                else
                {
                    for (int iLoop = 0; iLoop <= lstShowField.Items.Count - 1; iLoop++)
                    {
                        if (lstShowField.Items[iLoop].Selected)
                        {

                            lstView.Items.Add(lstShowField.Items[iLoop].Text);
                        }
                    }
                }
                lstView.DataBind();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('" + ex.Message + "');", true);
            }
        }
        protected void BtnAddAll_Click(object sender, EventArgs e)
        {
            try
            {
                lstView.Items.Clear();
                string lstr1 = lstShowField.Text;
                lngcol = 0;
                for (i = 0; i <= lstShowField.Items.Count - 1; i++)
                {
                    lstShowField.Items[i].Selected = true;
                    string lstTxt = lstShowField.Items[i].Text;
                    lstView.Items.Add(lstTxt);
                    lngcol = lngcol + 1;
                }
                BtnAddAll.Enabled = false;
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('" + ex.Message + "');", true);
            }
        }
        protected void BtnRemove_Click(object sender, EventArgs e)
        {
            try
            {
                string lstr2 = lstView.Text;
                if (lstr2 == "")
                {
                    Response.Write("<script>alert('Which Data Want to be Remove from  List')</script>");

                }
                else
                {
                    for (int i = 0; i <= lstView.Items.Count - 1; i++)
                    {
                        if (lstView.Items[i].Selected)
                        {
                            lstView.Items.Remove(lstView.Items[i].Text);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('" + ex.Message + "');", true);
            }
        }
        protected void BtnRemoveAll_Click(object sender, EventArgs e)
        {
            try
            {
                string lstr2 = lstView.Text;
                lngcol = 0;
                lstView.Items.Clear();
                BtnAddAll.Enabled = true;
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('" + ex.Message + "');", true);
            }
        }
        protected void BtnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                QrySt = "";
                lngcol = 0;
                if ((lstView.Items.Count == 0))
                {
                    Response.Write("<script>alert('Which Data Item want to be listed') </script>");

                }
                else
                {

                    for (i = 0; i <= lstView.Items.Count - 1; i++)
                    {
                        lstView.Items[i].Selected = true;
                        lstTxt = lstView.Items[i].Text;
                        lngcol = lngcol + 1;
                        lstTxt = lstTxt + ",";
                        QrySt = QrySt + lstTxt;
                    }
                }
                jobNo = QrySt.TrimEnd(',');
                Session["JNO"] = jobNo;
                GetJobsWise(jobNo);
                ExportPage.Visible = true;
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('" + ex.Message + "');", true);
            }
        }
    }
}