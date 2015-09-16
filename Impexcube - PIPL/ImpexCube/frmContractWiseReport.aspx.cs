using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Drawing;
using System.IO;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

namespace ImpexCube
{
    public partial class frmContractWiseReport : System.Web.UI.Page
    {
        string strconn = (string)ConfigurationManager.ConnectionStrings["Constr"].ConnectionString;
        Double Total;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack == false)
            {
                txtFdate.Text = (string)Session["fdate"];
                string todate = System.DateTime.Now.ToString("dd/MM/yyyy");
                txtTdate.Text = todate;
               
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            if (ddlContractSea.SelectedValue != "~Select~" || ddlContractAir.SelectedValue != "~Select~")
            {
                try
                {
                    string FD = txtFdate.Text;
                    string TD = txtTdate.Text;

                    string sMM = FD.Substring(3, 2);
                    string sDD = FD.Substring(0, 2);
                    string sYY = FD.Substring(6, 4);
                    // string RPT = ddlReportName.SelectedValue;
                    FD = sMM + "/" + sDD + "/" + sYY;
                    Session["QUERY"] = "";

                    string eMM = TD.Substring(3, 2);
                    string eDD = TD.Substring(0, 2);
                    string eYY = TD.Substring(6, 4);
                    TD = eMM + "/" + eDD + "/" + eYY;

                    DateTime fd = Convert.ToDateTime(FD);
                    DateTime td = Convert.ToDateTime(TD);

                    string fDate = fd.ToString("yyyy-MM-dd");
                    string tDate = td.ToString("yyyy-MM-dd");

                    string pName = txtPName.Text;

                    string query = " ";

                    if (tDate == "")
                        tDate = fDate;

                    if (fDate == "")
                    {
                        Response.Write("<script>" + "alert('Please Give  Date Values Or Job No');" + "</script>");


                    }
                    else
                    {
                        if (ddlContractAir.SelectedValue == "AIRMINI1800")
                        {
                            query = "SELECT DISTINCT invoice, invoiceDate, compName, jobno, blno, BEnoDate, importItem, Quantity, ass_Value, SubTotalTax, STaxPercent," +
                                " Service_tax, Edu_Cess, SEC_Chess, Grand_Total FROM View_Invoice WHERE TransportMode = '" + ddlMode.SelectedValue + "' AND compName = '" + txtPName.Text + "' " +
                                " AND invoiceDate >= '" + fDate + "' AND invoiceDate <= '" + tDate + "' AND ((convert(numeric(18,2),ass_value)) * 0.3 / 100 <= '1800') ";
                        }
                        else if (ddlContractAir.SelectedValue == "AIR30")
                        {
                            query = "SELECT DISTINCT invoice, invoiceDate, compName, jobno, blno, BEnoDate, importItem, Quantity, ass_Value, SubTotalTax, STaxPercent," +
                                   " Service_tax, Edu_Cess, SEC_Chess, Grand_Total FROM View_Invoice WHERE TransportMode = '" + ddlMode.SelectedValue + "' AND compName = '" + txtPName.Text + "' " +
                                   " AND invoiceDate >= '" + fDate + "' AND invoiceDate <= '" + tDate + "' AND ((convert(numeric(18,2),ass_value)) * 0.3 / 100 >= '1801') and ((convert(numeric(18,2),ass_value)) * 0.3 / 100 <= '7499' ) ";
                        }
                        else if (ddlContractAir.SelectedValue == "AIRMaxi7500")
                        {
                            query = "SELECT DISTINCT invoice, invoiceDate, compName, jobno, blno, BEnoDate, importItem, Quantity, ass_Value, SubTotalTax, STaxPercent," +
                                   " Service_tax, Edu_Cess, SEC_Chess, Grand_Total FROM View_Invoice WHERE TransportMode = '" + ddlMode.SelectedValue + "' AND compName = '" + txtPName.Text + "' " +
                                   " AND invoiceDate >= '" + fDate + "' AND invoiceDate <= '" + tDate + "'  and ((convert(numeric(18,2),ass_value)) * 0.3 / 100 >= '7500' ) ";
                        }
                        else if (ddlContractSea.SelectedValue == "SEAMINI1800")
                        {
                            query = "SELECT DISTINCT invoice, invoiceDate, compName, jobno, blno, BEnoDate, importItem, Quantity, ass_Value, SubTotalTax, STaxPercent," +
                                   " Service_tax, Edu_Cess, SEC_Chess, Grand_Total FROM View_Invoice WHERE TransportMode = '" + ddlMode.SelectedValue + "' AND compName = '" + txtPName.Text + "' " +
                                   " AND invoiceDate >= '" + fDate + "' AND invoiceDate <= '" + tDate + "'  and ((convert(numeric(18,2),ass_value)) * 0.3 / 100 <= '1800' and container_no like '%LCL%') ";
                        }
                        else if (ddlContractSea.SelectedValue == "SEA30")
                        {
                            query = "SELECT DISTINCT invoice, invoiceDate, compName, jobno, blno, BEnoDate, importItem, Quantity, ass_Value, SubTotalTax, STaxPercent," +
                                   " Service_tax, Edu_Cess, SEC_Chess, Grand_Total FROM View_Invoice WHERE TransportMode = '" + ddlMode.SelectedValue + "' AND compName = '" + txtPName.Text + "' " +
                                   " AND invoiceDate >= '" + fDate + "' AND invoiceDate <= '" + tDate + "' AND ((convert(numeric(18,2),ass_value)) * 0.3 / 100 >= '1801') and ((convert(numeric(18,2),ass_value)) * 0.3 / 100 <= '7499' and container_no like '%LCL%') ";
                        }
                        else if (ddlContractSea.SelectedValue == "SEAMaxi7500")
                        {
                            query = "SELECT DISTINCT invoice, invoiceDate, compName, jobno, blno, BEnoDate, importItem, Quantity, ass_Value, SubTotalTax, STaxPercent," +
                                   " Service_tax, Edu_Cess, SEC_Chess, Grand_Total FROM View_Invoice WHERE TransportMode = '" + ddlMode.SelectedValue + "' AND compName = '" + txtPName.Text + "' " +
                                   " AND invoiceDate >= '" + fDate + "' AND invoiceDate <= '" + tDate + "'  and ((convert(numeric(18,2),ass_value)) * 0.3 / 100 >= '7500' and container_no like '%LCL%') ";
                        }
                        else if (ddlContractSea.SelectedValue == "SEAFCL20")
                        {
                            query = "SELECT DISTINCT invoice, invoiceDate, compName, jobno, blno, BEnoDate, importItem, Quantity, ass_Value, SubTotalTax, STaxPercent," +
                                   " Service_tax, Edu_Cess, SEC_Chess, Grand_Total FROM View_Invoice WHERE TransportMode = '" + ddlMode.SelectedValue + "' AND compName = '" + txtPName.Text + "' " +
                                   " AND invoiceDate >= '" + fDate + "' AND invoiceDate <= '" + tDate + "'  and ((convert(numeric(18,2),ass_value)) * 0.3 / 100 >= '7500' and container_no like '%20%FCL%') ";
                        }
                        else if (ddlContractSea.SelectedValue == "SEAFCL40")
                        {
                            query = "SELECT DISTINCT invoice, invoiceDate, compName, jobno, blno, BEnoDate, importItem, Quantity, ass_Value, SubTotalTax, STaxPercent," +
                                   " Service_tax, Edu_Cess, SEC_Chess, Grand_Total FROM View_Invoice WHERE TransportMode = '" + ddlMode.SelectedValue + "' AND compName = '" + txtPName.Text + "' " +
                                   " AND invoiceDate >= '" + fDate + "' AND invoiceDate <= '" + tDate + "'  and ((convert(numeric(18,2),ass_value)) * 0.3 / 100 >= '9000') and container_no like '%40%FCL%' ";
                        }
                        SqlConnection conn1 = new SqlConnection(strconn);

                        SqlDataAdapter da11 = new SqlDataAdapter(query, conn1);
                        conn1.Open();
                        DataSet ds11 = new DataSet();

                        da11.Fill(ds11, "data");
                        conn1.Close();
                        if (ds11.Tables["data"].Rows.Count != 0)
                        {
                            gvContractReport.DataSource = ds11;
                            gvContractReport.DataBind();
                        }
                        else
                        {
                            gvContractReport.DataBind();
                        }

                    }
                    //sqlQueryVal = (string)Session["QUERY"];
                    //if (sqlQueryVal != "")
                    //{
                    //    try
                    //    {
                    //        gvContractReport.DataSource = GetData(sqlQueryVal);
                    //        gvContractReport.DataBind();

                    //        gvInvoice.Visible = true;
                    //    }
                    //    catch (Exception ex)
                    //    {
                    //        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('" + ex.Message + "');", true);
                    //    }

                    //}



                    if (gvContractReport.PageCount == 0)
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Records are not found for the given values');", true);

                    else
                    {

                        ExportPage.Visible = true;

                    }
                }
                catch (Exception ex)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('" + ex.Message + "');", true);
                }
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Please Select the Contract Detail');", true);
            }
        }

        public DataSet GetData(string sqlQuery)
        {

            SqlConnection conn = new SqlConnection(strconn);

            SqlDataAdapter da1 = new SqlDataAdapter(sqlQuery, conn);

            DataSet ds1 = new DataSet();

            da1.Fill(ds1, "data");
            return ds1;
        }

        protected void ExportPage_Click(object sender, EventArgs e)
        {
            try
            {
                string sysDates = DateTime.Now.ToString("dd/MM/yyyy");
                string FileName = "GKNReport" + sysDates;
                string strFileName = FileName + ".xls";
                btnSearch_Click(sender, e);
                

                string attachment = "attachment; filename=" + strFileName + " ";
              //  ExportExcell(strFileName, gvContractReport);

                Response.ClearContent();
                Response.AddHeader("content-disposition", attachment);
                Response.ContentType = "application/ms-excel";

                StringWriter sw = new StringWriter();
                HtmlTextWriter htw = new HtmlTextWriter(sw);

                HtmlForm frm = new HtmlForm();
                gvContractReport.AllowPaging = false;

                gvContractReport.Parent.Controls.Add(frm);
                frm.Attributes["runat"] = "server";
                frm.Controls.Add(gvContractReport);
                frm.RenderControl(htw);

                Response.Write(sw.ToString());
                Response.End();
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('" + ex.Message + "');", true);
            }
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

         

            Table tb = new Table();
            TableRow tr1 = new TableRow();
            TableCell cell1 = new TableCell();
            cell1.Controls.Add(gv);
            tr1.Cells.Add(cell1);
           
            TableCell cell2 = new TableCell();
            cell2.Text = "&nbsp;";
          
            TableRow tr2 = new TableRow();
            tr2.Cells.Add(cell2);
          
            tb.Rows.Add(tr1);
            tb.Rows.Add(tr2);
         
            tb.RenderControl(hw);

            //style to format numbers to string
            string style = @"<style> .textmode { mso-number-format:\@; } </style>";
            HttpContext.Current.Response.Write(style);
            HttpContext.Current.Response.Output.Write(sw.ToString());
            HttpContext.Current.Response.Flush();
            HttpContext.Current.Response.End();
        }

        protected void btnExit_Click(object sender, EventArgs e)
        {

        }

        protected void ddlMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlMode.SelectedValue == "A")
            {
                ddlContractAir.Visible = true;
                ddlContractSea.Visible = false;
                ddlContractSea.SelectedValue = "~Select~";
            }
            else if (ddlMode.SelectedValue == "S")
            {
                ddlContractSea.Visible = true;
                ddlContractAir.Visible = false;
                ddlContractAir.SelectedValue = "~Select~";
                
            }
        }

        protected void gvContractReport_RowDataBound(object sender, GridViewRowEventArgs e)
        {
          
            //if (e.Row.RowType == DataControlRowType.DataRow)
            //{
            //    Double amt = Convert.ToDouble(e.Row.Cells[8].Text);
            //    e.Row.Cells[8].Text = amt.ToString("#0.00");
            //    Total = Total + amt;
            //}
            //if (e.Row.RowType == DataControlRowType.Footer)
            //{
            //    e.Row.Cells[8].Text = Total.ToString("#0.00");
            //    e.Row.Cells[7].Text = "Total";
            //}
        }
    }
}