using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Data.SqlClient;

public partial class frmContractReport : System.Web.UI.Page
{
    string strImpex = (string)ConfigurationManager.ConnectionStrings["Constr"].ConnectionString;
    //string strconn = (string)ConfigurationSettings.AppSettings["ConnectionImpex"];
    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack == false)
        {
            DivTag.Visible = false;
         
            string fy = (string)Session["FinancialYear"];
            getSDate(fy);
        }
    }
    protected void getSDate(string fy)
    {
        SqlConnection conn = new SqlConnection(strImpex);
        string Query = "select * from M_RunningNo where fyear='" + fy + "'";
        SqlDataAdapter da = new SqlDataAdapter(Query, conn);
        DataSet ds = new DataSet();
        da.Fill(ds, "FYEAR");
        DataRowView row = ds.Tables["FYEAR"].DefaultView[0];
        DateTime sDATES = Convert.ToDateTime(row["sDATE"].ToString());
        txtFrom.Text = sDATES.ToShortDateString();
        txtTo.Text = System.DateTime.Now.ToString("dd/MM/yyyy");

    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        string[] DT ;
        string dt = "";
        string Command = "";
        string From = txtFrom.Text;
        string Name = txtPName.Text;
        string To = txtTo.Text;
        string Status = chkStatus.SelectedValue;

        DT = From.Split('/');
        dt = DT[1] + "/" + DT[0] + "/" + DT[2];
        From = dt;

        DT = To.Split('/');
        dt = DT[1] + "/" + DT[0] + "/" + DT[2];
        To = dt;

        if (Status != "All")
        {
            if ((From != string.Empty) && (To != string.Empty) && (Name != string.Empty))
            {
                Command = "select * from contract_mst where (convert (datetime,entry_date,101)  between '" + From + "' and '" + To + "') and (customer_name='" + Name + "') and (contr_status='" + Status + "')";
                Report(Command);
            }
            else if ((From != string.Empty) && (To != string.Empty) && (Name == string.Empty))
            {
                Command = "select * from contract_mst where (convert (datetime,entry_date,101)  between '" + From + "' and '" + To + "') and (contr_status='" + Status + "')";
                Report(Command);
            }
            else if ((From != string.Empty) && (To == string.Empty) && (Name == string.Empty))
            {
                To = From;
                Command = "select * from contract_mst where (convert (datetime,entry_date,101)  between '" + From + "' and '" + To + "') and (contr_status='" + Status + "')";
                Report(Command);
            }
            else if ((From != string.Empty) && (To == string.Empty) && (Name != string.Empty))
            {
                To = From;
                Command = "select * from contract_mst where (convert (datetime,entry_date,101)  between '" + From + "' and '" + To + "') and (customer_name='" + Name + "') and (contr_status='" + Status + "')";
                Report(Command);
            }
            else if ((From == string.Empty) && (To == string.Empty) && (Name != string.Empty))
            {
                To = From;
                Command = "select * from contract_mst where (customer_name='" + Name + "') and (contr_status='" + Status + "')";
                Report(Command);
            }
            else if ((From == string.Empty) && (To != string.Empty) && (Name != string.Empty))
            {
                From = To;
                Command = "select * from contract_mst where (convert (datetime,entry_date,101)  between '" + From + "' and '" + To + "') and (customer_name='" + Name + "') and (contr_status='" + Status + "')";
                Report(Command);
            }
            else if ((From == string.Empty) && (To != string.Empty) && (Name == string.Empty))
            {
                From = To;
                Command = "select * from contract_mst where (convert (datetime,entry_date,101)  between '" + From + "' and '" + To + "') and (contr_status='" + Status + "')";
                Report(Command);
            }
        }
        else if(Status == "All")
        {
            if ((From != string.Empty) && (To != string.Empty) && (Name != string.Empty))
            {
                Command = "select * from contract_mst where (convert (datetime,entry_date,101)  between '" + From + "' and '" + To + "') and (customer_name='" + Name + "')";
                Report(Command);
            }
            else if ((From != string.Empty) && (To != string.Empty) && (Name == string.Empty))
            {
                Command = "select * from contract_mst where (convert (datetime,entry_date,101)  between '" + From + "' and '" + To + "')";
                Report(Command);
            }
            else if ((From != string.Empty) && (To == string.Empty) && (Name == string.Empty))
            {
                To = From;
                Command = "select * from contract_mst where (convert (datetime,entry_date,101)  between '" + From + "' and '" + To + "')";
                Report(Command);
            }
            else if ((From != string.Empty) && (To == string.Empty) && (Name != string.Empty))
            {
                To = From;
                Command = "select * from contract_mst where (convert (datetime,entry_date,101)  between '" + From + "' and '" + To + "') and (customer_name='" + Name + "')";
                Report(Command);
            }
            else if ((From == string.Empty) && (To == string.Empty) && (Name != string.Empty))
            {
                
                Command = "select * from contract_mst where (customer_name='" + Name + "')";
                Report(Command);
            }
            else if ((From == string.Empty) && (To != string.Empty) && (Name != string.Empty))
            {
                From = To;
                Command = "select * from contract_mst where (convert (datetime,entry_date,101)  between '" + From + "' and '" + To + "') and (customer_name='" + Name + "')";
                Report(Command);
            }
            else if ((From == string.Empty) && (To != string.Empty) && (Name == string.Empty))
            {
                From = To;
                Command = "select * from contract_mst where (convert (datetime,entry_date,101)  between '" + From + "' and '" + To + "')";
                Report(Command);
            }
        }
       
    }
   
    protected void Report(string Cmd)
    {
        SqlConnection conn = new SqlConnection(strImpex);
        conn.Open();
        SqlCommand com = new SqlCommand(Cmd, conn);
        SqlDataAdapter da = new SqlDataAdapter(com);
        DataSet ds = new DataSet();
        da.Fill(ds, "PARTY");
        if (ds.Tables[0].Rows.Count != 0)
        {
            gvReport.DataSource = ds;
            gvReport.DataBind();
            gvSelect.DataSource = ds;
            gvSelect.DataBind();
            DivTag.Visible = true;
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('There Is No Contract Details');", true);
        }

    }
    protected void btnExport_Click(object sender, EventArgs e)
    {
        txtFrom.Text = txtPName.Text = txtTo.Text = "";
        gvReport.Visible = true;
        gvSelect.Visible = false;
        GridViewExportDet.ExportExcell("Contract.xls", gvReport);
        gvReport.Visible =false;
        gvSelect.Visible = true;
    }
    protected void gvReport_SelectedIndexChanged(object sender, EventArgs e)
    {
        Session["ContractNo"] = gvSelect.SelectedRow.Cells[0].Text.ToString();
        ScriptManager.RegisterStartupScript(this, this.GetType(), "Error", "window.open('ContractPopupBox.aspx','_blank','width=800,height=500, menubar=no, scrollbars=yes, toolbar=no, location=no, resizable=No, left=150, top=175');", true);
    }
   
}
