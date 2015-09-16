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
using MySql.Data.MySqlClient;


namespace ImpexCube
{
    public partial class frmDetentionList : System.Web.UI.Page
    {
        string strconn = (string)ConfigurationManager.AppSettings["ConnectionAdmin"];
        string strconn1 = (string)ConfigurationManager.AppSettings["ConnectionVisual"];

        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack == false)
            {
                drParty.DataSource = GetParty();
                drParty.DataTextField = "CNSR_NAME";
                drParty.DataBind();
                drParty.Items.Insert(0, new ListItem("select", ""));

                //   GetStatus(); // To get reports status for Detention
                ExportPage.Visible = false;

                txtStartDate.Text = Session["fdate"].ToString();
                string todate = System.DateTime.Now.ToString("dd/MM/yyyy");
                txtEndDate.Text = todate;
            }
        }

        private DataSet GetParty()
        {
            MySqlConnection conn = new MySqlConnection(strconn1);
            string str = "select * from cnsr_mst order by CNSR_NAME";

            MySqlDataAdapter da = new MySqlDataAdapter(str, conn);
            DataSet ds = new DataSet();
            da.Fill(ds, "CNS");
            return (ds);
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
                string FD = txtStartDate.Text;
                string TD = txtEndDate.Text;
                string PN = drParty.SelectedValue;

                string sMM = FD.Substring(3, 2);
                string sDD = FD.Substring(0, 2);
                string sYY = FD.Substring(6, 4);
                FD = sMM + "/" + sDD + "/" + sYY;


                string eMM = TD.Substring(3, 2);
                string eDD = TD.Substring(0, 2);
                string eYY = TD.Substring(6, 4);
                TD = eMM + "/" + eDD + "/" + eYY;

                DateTime fd = Convert.ToDateTime(FD);
                DateTime td = Convert.ToDateTime(TD);

                string fDate = fd.ToString("yyyy-MM-dd");
                string tDate = td.ToString("yyyy-MM-dd");

                GetDetails(fDate, tDate, PN);

                // GridViewExportDet.Export(strFileName, this.GridView1);
                GridViewExportDet.ExportExcell(strFileName, GridView1);
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('" + ex.Message + "');", true);
            }
        }

        private void GetDetails(string fDate, string tDate, string PN)
        {
            SqlConnection conn = new SqlConnection(strconn);
            string str = "";
            if (fDate != "" && tDate != "" && PN != "")
            {
                str = "select * from Detention_mstN where jobDATE  between '" + fDate + "' and '" + tDate + "' and supplier='" + PN + "'";
            }
            else if (fDate != "" && tDate != "" && PN == "")
            {
                str = "select * from Detention_mstN where jobDATE  between '" + fDate + "' and '" + tDate + "'";
            }
            SqlDataAdapter da = new SqlDataAdapter(str, conn);
            DataSet ds = new DataSet();
            da.Fill(ds, "DET");
            DataTable dt = ds.Tables["DET"];

            GridView1.DataSource = Transpose(dt);
            GridView1.DataBind();
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

        protected void Search_Click(object sender, EventArgs e)
        {
            try
            {
                string FD = txtStartDate.Text;
                string TD = txtEndDate.Text;

                string sMM = FD.Substring(3, 2);
                string sDD = FD.Substring(0, 2);
                string sYY = FD.Substring(6, 4);
                FD = sMM + "/" + sDD + "/" + sYY;


                string eMM = TD.Substring(3, 2);
                string eDD = TD.Substring(0, 2);
                string eYY = TD.Substring(6, 4);
                TD = eMM + "/" + eDD + "/" + eYY;

                DateTime fd = Convert.ToDateTime(FD);
                DateTime td = Convert.ToDateTime(TD);

                string fDate = fd.ToString("yyyy-MM-dd");
                string tDate = td.ToString("yyyy-MM-dd");


                string PN = drParty.SelectedValue;

                SqlConnection conn = new SqlConnection(strconn);
                string str = "";
                if (fDate != "" && tDate != "" && PN != "")
                {
                    str = "select * from Detention_mstN where jobDATE  between '" + fDate + "' and '" + tDate + "' and supplier='" + PN + "'";
                }
                else if (fDate != "" && tDate != "")
                {
                    str = "select * from Detention_mstN where jobDATE  between '" + fDate + "' and '" + tDate + "'";
                }
                SqlDataAdapter da = new SqlDataAdapter(str, conn);
                DataSet ds = new DataSet();
                da.Fill(ds, "DET");
                DataTable dt = ds.Tables["DET"];

                if (ds.Tables["DET"].Rows.Count != 0)
                {
                    GridView1.DataSource = Transpose(dt);
                    GridView1.DataBind();
                    GridView1.Visible = true;
                    ExportPage.Visible = true;
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('There is no records for particular supplier');", true);
                    GridView1.Visible = false;
                    ExportPage.Visible = false;
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('" + ex.Message + "');", true);
            }
        }

        protected void BtnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/PIPL/JobReports/Index.aspx");

        }
    }
}