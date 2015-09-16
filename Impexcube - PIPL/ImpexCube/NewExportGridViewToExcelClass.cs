using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.IO;

/// <summary>
/// Summary description for NewExportGridViewToExcelClass
/// </summary>
public class NewExportGridViewToExcelClass
{
    public static void Export(string fileName, GridView gv)
    {
        HttpContext.Current.Response.Clear();
        HttpContext.Current.Response.AddHeader(
            "content-disposition", string.Format("attachment; filename={0}", fileName));
        HttpContext.Current.Response.ContentType = "application/ms-excel";

        using (StringWriter sw = new StringWriter())
        {
            Export(sw, gv);
            //  render the htmlwriter into the response
            HttpContext.Current.Response.Write(sw.ToString());
            HttpContext.Current.Response.End();
        }
    }

    public static void ExportToFile(string fileName, GridView gv)
    {
        using (StreamWriter streamWriter = new StreamWriter(fileName))
        {
            using (StringWriter sw = new StringWriter())
            {
                Export(sw, gv);
                streamWriter.Write(sw.ToString());
            }
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="sw"></param>
    public static void Export(StringWriter sw, GridView gv)
    {
        using (HtmlTextWriter htw = new HtmlTextWriter(sw))
        {
            //  Create a table to contain the grid
            Table table = new Table();

            //  include the gridline settings
            table.GridLines = gv.GridLines;

            //  add the header row to the table
            if (gv.HeaderRow != null)
            {
               
                table.Rows.Add(gv.HeaderRow);
            }

            //  add each of the data rows to the table
            foreach (GridViewRow row in gv.Rows)
            {
               
                table.Rows.Add(row);
            }

            //  add the footer row to the table
            if (gv.FooterRow != null)
            {
               
                table.Rows.Add(gv.FooterRow);
            }

            //  render the table into the htmlwriter
            table.RenderControl(htw);
        }
    }

    /// <summary>
    /// Replace any of the contained controls with literals
    /// </summary>
    /// <param name="control"></param>
    private static void PrepareControlForExport(Control control)
    {
        // code unchanged
    }

}
