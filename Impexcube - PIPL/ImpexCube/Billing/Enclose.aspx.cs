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

public partial class Enclose : System.Web.UI.Page
{
    string strconn = (string)ConfigurationManager.AppSettings["ConnectionImpex"];
    string ecCode = "";
    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack == false)
        {
            BtnPrint.Visible=false;
           // Print.Visible=false;
            //lblUser.Text = (string)Session["USER-NAME"];
            //lblDate.Text = System.DateTime.Now.ToString("dd-MMM-yyyy");
            //lblTime.Text = DateTime.Now.ToLongTimeString();
            txtDate.Text = System.DateTime.Now.ToString("dd/MM/yyyy");
            //if (lblUser.Text == "")
            //{
            //    Response.Redirect("~/PIPL.aspx");
            //}
            DataSet ds1 = new DataSet();
            ds1.ReadXml(Server.MapPath("XML\\encloser1.xml"));
            {
                GridView1.DataSource = ds1;
                GridView1.DataMember = "Detail";
                //ddlProduct.DataTextField = "t1";
                //ddlProduct.DataValueField = "t1";
                GridView1.DataBind();
            }
            DataSet ds2 = new DataSet();
            ds2.ReadXml(Server.MapPath("XML\\encloser2.xml"));
            {
                GridView2.DataSource = ds2;
                GridView2.DataMember = "Detail";
                //ddlProduct.DataTextField = "t1";
                //ddlProduct.DataValueField = "t1";
                GridView2.DataBind();
            }
            SqlConnection conn = new SqlConnection(strconn);
            string sqlQuery = "select  * from AppDetails";
            SqlDataAdapter da = new SqlDataAdapter(sqlQuery, conn);
            DataSet ds = new DataSet();
            da.Fill(ds, "Table");

            if (ds.Tables["Table"].Rows.Count != 0)
            {
                DataRowView row = ds.Tables["Table"].DefaultView[0];
                lblCompName.Text = row["CompanyName"].ToString();
                lblCHANO.Text = row["CHANo"].ToString();
                lblSTRegno.Text = row["STRegNo"].ToString();
                lbladdress.Text = row["address"].ToString();
                lbladdress1.Text = row["address1"].ToString();
                lblCompName1.Text = row["CompanyName"].ToString();
            }
        }
    }

   

    protected void BtnSubmit_Click(object sender, EventArgs e)
    {
        string pName=txtPName.Text;
        string KindAttn=txtKindAttn.Text;
        string eD=txtDate.Text;
        string[] ED = eD.Split('/');
        eD = ED[2] + "-" + ED[1] + "-" + ED[0];
        
        DateTime eDate=Convert.ToDateTime(eD);
        string sType=txtStype.Text;
        string Commodity=txtCommodity.Text;
        string consignee=txtConsignee.Text;
        string invNo= txtInvNo.Text;
        string invDate=txtInvDate.Text;
        string invJob= txtInvJob.Text;
        string dBNo= txtDebit.Text;
        string dBDate=txtDebitDate.Text;
        string dBJob=txtDebitJob.Text;
        string TR6=txtTRNo.Text;
        string DTL1="";
        string DTL2="";
       
        string SCC="";
        string sqlQuery="";
       foreach (GridViewRow row1 in GridView1.Rows)
        {
           CheckBox chk1=(CheckBox)row1.FindControl("chkSel");
           if(chk1.Checked)
           {
                string dtl1=row1.Cells[1].Text;
                DTL1 = DTL1 + dtl1 + ",";
           }
        }
        foreach (GridViewRow row2 in GridView2.Rows)
        {
           CheckBox chk2=(CheckBox)row2.FindControl("chkSel");
            if(chk2.Checked)
            {
                string dtl2=row2.Cells[1].Text;
                DTL2 = DTL2 + dtl2 + ",";
            }
        }
        DTL1=DTL1.TrimEnd(',');
        DTL2=DTL2.TrimEnd(',');
        try
        {
            SqlConnection conn = new SqlConnection(strconn);
            string strST = "select max(enclose_Id) as cno from  enclose_mst";
            SqlDataAdapter da = new SqlDataAdapter(strST, conn);
            DataSet ds = new DataSet();
            da.Fill(ds, "encl");
            if (ds.Tables["encl"].Rows.Count == 0)
            {
                ecCode = "ECE00001";
            }
            else
            {
                DataRowView Row1 = ds.Tables["encl"].DefaultView[0];
                string code = Row1["cno"].ToString();
                string sCode = code.Substring(3, 5);
                int sc = Convert.ToInt32(sCode) + 1;
                SCC = Convert.ToString(sc);
                if (sc <= 9)
                    SCC = "ECE0000" + SCC;
                else if (sc > 9)
                    SCC = "ECE000" + SCC;
                else if (sc < 10 && sc > 99)
                    SCC = "ECE00" + SCC;
                else if (sc < 100 && sc > 999)
                    SCC = "ECE0" + SCC;
                else
                    SCC = "ECE" + SCC;
            }

            Session["ECECODE"] = SCC;
            sqlQuery = "insert into enclose_mst(enclose_Id,eDate,party_name,KindAttn,Commodity,stype,invoiceNO,invoice_date," +
                       "inv_jobNo,Debit_noteNO,debit_date,db_jobNo,tr6_No,dtl1,dtl2) " +
                       "values('" + SCC + "','" + eDate + "','" + pName + "','" + KindAttn + "','" + Commodity + "','" + sType + "'," +
                       "'" + invNo + "','" + invDate + "','" + invJob + "','" + dBNo + "','" + dBDate + "','" + dBJob + "','" + TR6 + "','" + DTL1 + "','" + DTL2 + "')";
            GetCommand(sqlQuery);
            //BtnSubmit.Enabled = false;
            //BtnPrint.Visible = true;

        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('" + ex.Message + "');", true);
        }
    }
    protected void GetCommand(string Query)
    {

        SqlConnection conn = new SqlConnection(strconn);
        conn.Open();
        SqlCommand cmd = new SqlCommand(Query, conn);
        cmd.CommandText = Query;
        cmd.Connection = conn;
        int res = cmd.ExecuteNonQuery();
        conn.Close();
        //Response.Write("<script>alert('Enclose  has been Generated Successfully')</script>");
        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Enclose has been Generated Successfully');", true);
        Print.Visible = true;
        BtnSubmit.Visible = false;
    }
    protected void BtnPrint_Click(object sender, EventArgs e)
    {
       // Session["ECECODE"]="ECE00001";
        //Response.Redirect("cryEnclose.aspx",false);

    }
    protected void Print_Click(object sender, EventArgs e)
    {
       // string mcard = Session["CARDNO"].ToString();
        // BtnPrint.Attributes.Add("onclick", "return javascript:OpenPopup();");
      //  Response.Redirect("frmPrintOrderInfo.aspx?mid=" + mcard, false);
        Session["ctrl"] = PLDATA;
        ClientScript.RegisterStartupScript(this.GetType(), "onclick", "<script language=javascript>window.open('frmPrintEncl.aspx','PrintMe','height=800px,width=750px,scrollbars=1');</script>");
  
    }
}
