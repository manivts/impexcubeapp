using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using System.Web.UI;
using System.IO;

namespace ImpexCube
{
    public class GridViewExportUtil
    {

        public static void ForcedDownload(HttpResponse response, byte[] buffer, string fileName)
        {

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="fileName"></param>
        /// <param name="gv"></param>

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

        public static void ExportExcellNEW(string fileName, GridView gv)
        {
            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.Buffer = true;

            HttpContext.Current.Response.AddHeader("content-disposition", string.Format("attachment; filename={0}", fileName));
            HttpContext.Current.Response.Charset = "";
            HttpContext.Current.Response.ContentType = "application/vnd.ms-excel";
            StringWriter sw = new StringWriter();
            HtmlTextWriter hw = new HtmlTextWriter(sw);

            PrepareForExport(gv);
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

        public static void Export(string fileName, GridView gv)
        {
            HttpContext.Current.Response.Clear();
            HttpContext.Current.Response.AddHeader(
                "content-disposition", string.Format("attachment; filename={0}", fileName));
            HttpContext.Current.Response.ContentType = "application/ms-excel";

            using (StringWriter sw = new StringWriter())
            {
                using (HtmlTextWriter htw = new HtmlTextWriter(sw))
                {
                    //  Create a form to contain the grid
                    Table table = new Table();
                    table.GridLines = gv.GridLines;

                    //  add the header row to the table
                    if (gv.HeaderRow != null)
                    {
                        GridViewExportUtil.PrepareControlForExport(gv.HeaderRow);
                        table.Rows.Add(gv.HeaderRow);
                    }

                    //  add each of the data rows to the table
                    foreach (GridViewRow row in gv.Rows)
                    {
                        GridViewExportUtil.PrepareControlForExport(row);
                        table.Rows.Add(row);
                    }

                    //  add the footer row to the table
                    if (gv.FooterRow != null)
                    {
                        GridViewExportUtil.PrepareControlForExport(gv.FooterRow);
                        table.Rows.Add(gv.FooterRow);
                    }

                    //  render the table into the htmlwriter
                    table.RenderControl(htw);

                    //  render the htmlwriter into the response
                    HttpContext.Current.Response.Write(sw.ToString());
                    HttpContext.Current.Response.End();
                }
            }
        }

        /// <summary>
        /// Replace any of the contained controls with literals
        /// </summary>
        /// <param name="control"></param>
        private static void PrepareControlForExport(Control control)
        {
            for (int i = 0; i < control.Controls.Count; i++)
            {
                Control current = control.Controls[i];
                if (current is LinkButton)
                {
                    control.Controls.Remove(current);
                    control.Controls.AddAt(i, new LiteralControl((current as LinkButton).Text));
                }
                else if (current is ImageButton)
                {
                    control.Controls.Remove(current);
                    control.Controls.AddAt(i, new LiteralControl((current as ImageButton).AlternateText));
                }
                else if (current is HyperLink)
                {
                    control.Controls.Remove(current);
                    control.Controls.AddAt(i, new LiteralControl((current as HyperLink).Text));
                }
                else if (current is DropDownList)
                {
                    control.Controls.Remove(current);
                    control.Controls.AddAt(i, new LiteralControl((current as DropDownList).SelectedItem.Text));
                }
                else if (current is CheckBox)
                {
                    control.Controls.Remove(current);
                    control.Controls.AddAt(i, new LiteralControl((current as CheckBox).Checked ? "True" : "False"));
                }

                if (current.HasControls())
                {
                    GridViewExportUtil.PrepareControlForExport(current);
                }
            }
        }
        
        private static void PrepareForExport(GridView Gridview)
        {
            Byte[] myBytes13 = { 10 };
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

                string myStr13 = System.Text.Encoding.ASCII.GetString(myBytes13);
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

    }
}