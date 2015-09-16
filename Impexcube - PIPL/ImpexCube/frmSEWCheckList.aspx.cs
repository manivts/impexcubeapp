using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.IO;
using System.Configuration;
using System.Text;

namespace ImpexCube
{
    public partial class frmSEWCheckList : System.Web.UI.Page
    {

        //string strcon = (string)ConfigurationManager.ConnectionStrings["Constr"].ConnectionString;
       string strconn1 = (string)ConfigurationManager.ConnectionStrings["Constr"].ConnectionString;

        #region
        private Double _TotalExpTotal = 0.00;
        string jobNo;
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack == false)
            {
                    // User Authentication Code 
                    rbExpType.SelectedValue = "CDD";
                    // Authenticate.Forms(formID);
                    //RBLExp.Visible = false;
                    ExportPage.Visible = false;
                    BtnExport_CSV.Visible = false;
            }
        }

        protected void BtnSearch_Click(object sender, EventArgs e)
        {
            string JNO = txtPName.Text;
            SqlConnection conn1 = new SqlConnection(strconn1);

            string lstrsql = " select * from T_Product where jobno='" + JNO + "'";

            SqlDataAdapter da = new SqlDataAdapter(lstrsql, conn1);
            DataSet ds = new DataSet();
            da.Fill(ds, "ipurchase");
            if (ds.Tables["ipurchase"].Rows.Count == 0)
            {
                Response.Write("<script>alert('Please Give purchase order number for this JOB')</script>");
                Session["NO_PO"] = "1";
                Session["message"] = "Please Give purchase order number for this JOB";
                //   Response.Redirect("~/PIPL/JobReports/frmSEW.aspx");
            }
            else
            {

                GrdAllJob.Visible = true;
                //RBLExp.Visible = true;
                ExportPage.Visible = true;
                BtnExport_CSV.Visible = true;
                string CessDuty = rbExpType.SelectedValue;
                //if (CessDuty == "CDE")
                //{

                GrdCESS.DataSource = GetiWorkreg();
                GrdCESS.DataBind();
                GrdCESS.Visible = true;
                GrdAllJob.Visible = false;
                //}
                //else
                //{
                    //GrdAllJob.DataSource = GetiWorkreg();
                    //GrdAllJob.DataBind();
                    //GrdAllJob.Visible = true;
                    //GrdCESS.Visible = false;
               // }
            }
        }

        public DataSet GetiWorkreg()
        {
            jobNo = txtPName.Text;

            SqlConnection conn1 = new SqlConnection(strconn1);
            string lstrsql = "";


            lstrsql = "Select * from View_ImpJobInvoiceProduct where JobNo='" + jobNo + "' ";

            //lstrsql = " select ipurchase_dtl.inv_id,ipurchase_dtl.prod_sn,ipurchase_dtl.totalCVDamt,ipurchase_dtl.totalDUTYamt," +
            //    "ipurchase_dtl.addlDutyAmt,ipurchase_dtl.edu_cvd,ipurchase_dtl.she_cvd,ipurchase_dtl.cus_edu_cess,ipurchase_dtl.cus_she_cess," +
            //  " ipurchase_dtl.nvd,iworkreg_jobstatus.job_no,iworkreg_jobstatus.be_no,iworkreg_jobstatus.be_date," +
            //  " iinv_dtl.inv_no,iinv_dtl.inv_date,iinv_dtl.currency,iinv_dtl.EXCH_RATE,iinv_dtl.inv_value, " +
            //  " ipurchase_dtl.prod_desc,ipurchase_dtl.qty,ipurchase_dtl.cess_duty,ipurchase_dtl.model,ipurchase_dtl.po_itemNo,ipurchase_dtl.pur_ordno,ipurchase_dtl.unit" +
            //  " from iinv_dtl,iworkreg_jobstatus,ipurchase_dtl " +
            //  " where ipurchase_dtl.job_no=iworkreg_jobstatus.job_no and " +
            //  " ipurchase_dtl.job_no=iinv_dtl.job_no and " +
            //  " ipurchase_dtl.inv_id=iinv_dtl.inv_id and " +
            //  " ipurchase_dtl.job_no='" + jobNo + "' " +
            //  " order by ipurchase_dtl.inv_id,ipurchase_dtl.prod_sn";


            SqlDataAdapter da = new SqlDataAdapter(lstrsql, conn1);
            DataSet ds = new DataSet();
            da.Fill(ds, "iworkReg");
            if (ds.Tables["iworkReg"].Rows.Count != 0)
            {

                DataRowView dr = ds.Tables["iworkReg"].DefaultView[0];
                lblJobNo.Text = dr["JobNo"].ToString();
                lblImpeName.Text = dr["Importer"].ToString();
            }
            return ds;
        }

        decimal TotalUnitPrice;

        decimal GetUnitPrice(decimal Price)
        {
            TotalUnitPrice += Price;
            return Price;
        }

        decimal GetTotal()
        {
            return TotalUnitPrice;
        }

        protected void GrdAllJob_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            BtnSearch_Click(sender, e);
            //CreateTempTable();
            //GrdAllJob.DataSource = SortDataTable(CType(GrdAllJob.DataSource, DataTable), True)
            GrdAllJob.PageIndex = e.NewPageIndex;
            GrdAllJob.DataBind();
            //string formID = "UJOBFm";
            //Authenticate.Forms(formID);
            string Dis = (string)Session["DISABLE"];
            string ROnly = (string)Session["ROnly"];
            if (ROnly == "1")
            {
                foreach (GridViewRow Row in GrdAllJob.Rows)
                {
                    Row.Cells[11].Enabled = false;
                }
                lbROnly.Visible = true;
            }
        }

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                e.Row.Attributes.Add("onmouseover", "MouseEvents(this, event)");
                e.Row.Attributes.Add("onmouseout", "MouseEvents(this, event)");

                Double Qty = Convert.ToDouble(e.Row.Cells[3].Text);
                Double Price = Convert.ToDouble(e.Row.Cells[5].Text);

                Double total = Qty * Price;
                Double TOT = Math.Round(total, 2);
                e.Row.Cells[6].Text = Convert.ToString(TOT);

                Double TotalExps = TOT;
                _TotalExpTotal += TotalExps;
            }
            GridViewRow footer = GridView1.FooterRow;

        }

        protected void ExportPage_Click(object sender, EventArgs e)
        {
            string jno = txtPName.Text;
            string JNO = jno;//.Substring(5, 5);
            // string sysDates = DateTime.Now.ToString("dd-MMM-yyyy");
            // string FileName = JNO;
            string strFileName = JNO + ".xls";

            ExportExcell(strFileName, GrdCESS);
        }
        public static void ExportExcell(string fileName, GridView gv)
        {
            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.Buffer = true;

            HttpContext.Current.Response.AddHeader("content-disposition", string.Format("attachment; filename={0}", fileName));
            HttpContext.Current.Response.Charset = "";
            HttpContext.Current.Response.ContentType = "application/vnd.ms-excel";
            StringWriter sw = new StringWriter();
            HtmlTextWriter hw = new HtmlTextWriter(sw);

            //PrepareForExport(gv);
            //PrepareForExport(GridView1);

            Table tb = new Table();
            TableRow tr1 = new TableRow();
            TableCell cell1 = new TableCell();
            cell1.Controls.Add(gv);
            tr1.Cells.Add(cell1);
            //TableCell cell3 = new TableCell();
            //cell3.Controls.Add(GridView1);
            TableCell cell2 = new TableCell();
            cell2.Text = "&nbsp;";
            //if (rbPreference.SelectedValue == "2")
            //{
            //tr1.Cells.Add(cell2);
            //tr1.Cells.Add(cell3);
            //tb.Rows.Add(tr1);
            //}
            //else
            //{
            TableRow tr2 = new TableRow();
            tr2.Cells.Add(cell2);
            //TableRow tr3 = new TableRow();
            //tr3.Cells.Add(cell3);
            tb.Rows.Add(tr1);
            tb.Rows.Add(tr2);
            //tb.Rows.Add(tr3);
            //}
            tb.RenderControl(hw);

            //style to format numbers to string
            string style = @"<style> .textmode { mso-number-format:\@; } </style>";
            HttpContext.Current.Response.Write(style);
            HttpContext.Current.Response.Output.Write(sw.ToString());
            HttpContext.Current.Response.Flush();
            HttpContext.Current.Response.End();
        }
        //protected void ExportExcell(string fileName)
        //{
        //    Response.Clear();
        //    Response.Buffer = true;

        //    Response.AddHeader("content-disposition", string.Format("attachment; filename={0}", fileName));
        //    Response.Charset = "";
        //    //  Response.ContentType = "application/vnd.ms-excel";
        //    Response.ContentType = "application/vnd.ms-excel";
        //    StringWriter sw = new StringWriter();
        //    HtmlTextWriter hw = new HtmlTextWriter(sw);

        //    //PrepareForExport(GrdAllJob);
        //    //  PrepareForExport(GridView1);

        //    Table tb = new Table();
        //    TableRow tr1 = new TableRow();
        //    TableCell cell1 = new TableCell();
        //    string cessDuty = rbExpType.SelectedValue;
        //    if (cessDuty == "CDD")
        //        cell1.Controls.Add(GrdAllJob);
        //    else
        //        cell1.Controls.Add(GrdCESS);
        //    tr1.Cells.Add(cell1);

        //    tb.Rows.Add(tr1);

        //    tb.RenderControl(hw);

        //    //style to format numbers to string
        //    string style = @"<style> .textmode { mso-number-format:\@; } </style>";
        //    Response.Write(style);
        //    Response.Output.Write(sw.ToString());
        //    Response.Flush();
        //    Response.End();
        //}

        protected void PrepareForExport(GridView Gridview)
        {
            //Gridview.AllowPaging = Convert.ToBoolean(RBLExp.SelectedItem.Value);
            //Gridview.DataBind();

            //Change the Header Row back to white color
            Gridview.HeaderRow.Style.Add("background-color", "#FFFFFF");

            //Apply style to Individual Cells
            for (int k = 0; k < Gridview.HeaderRow.Cells.Count; k++)
            {
                Gridview.HeaderRow.Cells[k].Style.Add("background-color", "green");
            }

            for (int i = 0; i < Gridview.Rows.Count; i++)
            {
                GridViewRow row = Gridview.Rows[i];

                //Change Color back to white
                row.BackColor = System.Drawing.Color.White;

                //Apply text style to each Row
                row.Attributes.Add("class", "textmode");

                //Apply style to Individual Cells of Alternating Row
                if (i % 2 != 0)
                {
                    for (int j = 0; j < Gridview.Rows[i].Cells.Count; j++)
                    {
                        row.Cells[j].Style.Add("background-color", "#C2D69B");
                    }
                }
            }
        }

        protected void GrdCESS_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            //if (e.Row.RowType == DataControlRowType.DataRow)
            //{

            //    e.Row.Attributes.Add("onmouseover", "MouseEvents(this, event)");
            //    e.Row.Attributes.Add("onmouseout", "MouseEvents(this, event)");
                
            //    if (e.Row.Cells[3].Text != "&nbsp;")
            //    {
            //        DateTime beDate = Convert.ToDateTime(e.Row.Cells[3].Text);
            //        e.Row.Cells[3].Text = beDate.ToString("dd/MM/yyyy");
            //    }
            //    int numCol = e.Row.Cells.Count;

            //    for (int col = 1; col < numCol; col++)
            //    {
            //        if ((GrdCESS.HeaderRow.Cells[col].Text.Contains("DESCRIPTION")) && (e.Row.Cells[col].Text != "&nbsp;"))
            //        {

            //            string desc = e.Row.Cells[col].Text;
            //            desc = desc.Replace(',', ' ');
            //            e.Row.Cells[col].Text = desc;
            //        }
            //    }

            //}
        }

        protected void GrdAllJob_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {

                e.Row.Attributes.Add("onmouseover", "MouseEvents(this, event)");
                e.Row.Attributes.Add("onmouseout", "MouseEvents(this, event)");
                
                if (e.Row.Cells[3].Text != "&nbsp;")
                {
                    DateTime beDate = Convert.ToDateTime(e.Row.Cells[3].Text);
                    e.Row.Cells[3].Text = beDate.ToString("dd/MM/yyyy");
                }
                int numCol = e.Row.Cells.Count;
                /*
                 for (int col = 1; col < numCol; col++)
                 {
                     if ((GrdCESS.HeaderRow.Cells[col].Text.Contains("DESCRIPTION")) && (e.Row.Cells[col].Text != "&nbsp;"))
                     {

                         string desc = e.Row.Cells[col].Text;
                         desc = desc.Replace(',', ' ');
                         e.Row.Cells[col].Text = desc;
                     }
                 }*/

            }

        }

        protected void BtnExport_CSV_Click(object sender, EventArgs e)
        {
            //string jno = txtPName.Text;
            //string JNO = jno;//.Substring(5, 5);
            //string strFileName = JNO + ".csv";
            //ExportExcell(strFileName, GrdCESS);


            string CessDuty = rbExpType.SelectedValue;
            string jno = txtPName.Text;
            //   string JNO = jno.Substring(4, 5);
            Response.Clear();
            Response.Buffer = true;
            string fileName = jno + ".csv";
            //    Response.AddHeader("content-disposition","attachment;filename=SEWFile.csv");
            Response.AddHeader("content-disposition", string.Format("attachment; filename={0}", fileName));
            Response.Charset = "";
            Response.ContentType = "application/text";
            //GridView1.AllowPaging = false;
            //GridView1.DataBind();

            StringBuilder sb = new StringBuilder();
            // 1st Table Value
            CessDuty = "CDE";
            if (CessDuty == "CDE")
            {
                for (int k = 0; k < GrdCESS.Columns.Count; k++)
                {
                    //add separator
                    sb.Append(GrdCESS.Columns[k].HeaderText + ',');
                }
                if (sb.Length > 0) sb.Remove(sb.Length - 1, 1).Append("\r\n");
                //append new line
                //   sb.Append("\r\n");
                // int rowscount = GridView1.Rows.Count;

                for (int i = 0; i < GrdCESS.Rows.Count; i++)
                {
                    for (int k = 0; k < GrdCESS.Columns.Count; k++)
                    {
                        //add separator
                        if (GrdCESS.Rows[i].Cells[k].Text == "&nbsp;")
                        {
                            sb.Append("" + ',');
                        }
                        if (GrdCESS.Rows[i].Cells[k].Text != "&nbsp;")
                        {
                            sb.Append(GrdCESS.Rows[i].Cells[k].Text + ',');
                        }
                    }
                    //append new line
                    if (sb.Length > 0) sb.Remove(sb.Length - 1, 1).Append("\r\n");

                    //    sb.Append("\r\n");
                }
            }
            else
            {
                for (int k = 0; k < GrdAllJob.Columns.Count; k++)
                {
                    //add separator
                    sb.Append(GrdAllJob.Columns[k].HeaderText + ',');
                }
                if (sb.Length > 0) sb.Remove(sb.Length - 1, 1).Append("\r\n");
                //append new line
                //   sb.Append("\r\n");
                // int rowscount = GridView1.Rows.Count;

                for (int i = 0; i < GrdAllJob.Rows.Count; i++)
                {
                    for (int k = 0; k < GrdAllJob.Columns.Count; k++)
                    {
                        //add separator
                        sb.Append(GrdAllJob.Rows[i].Cells[k].Text + ',');
                    }
                    //append new line
                    if (sb.Length > 0) sb.Remove(sb.Length - 1, 1).Append("\r\n");

                    //    sb.Append("\r\n");
                }
            }
            Response.Write(sb.ToString());
            Response.Flush();
            Response.End();
        }

        protected void BtnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("frmSEWCheckList.aspx");
        }

    }
}