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
using System.Data.SqlClient;

public partial class frmChargeDtl : System.Web.UI.Page
{
   // string strconn = (string)ConfigurationManager.AppSettings["ConnectionImpex"];
    string strconn = (string)ConfigurationManager.ConnectionStrings["Constr"].ConnectionString;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack == false)
        {
            string sqlQuery = "select * from M_Charge";
            GridView1.DataSource = GetData(sqlQuery);
            GridView1.DataBind();
            GridView1.Visible = true;
           
        }
    }
    public DataSet GetData(string sqlQuery)
    {
        SqlConnection conn = new SqlConnection(strconn);

        SqlDataAdapter da = new SqlDataAdapter(sqlQuery, conn);
        DataSet ds = new DataSet();
        da.Fill(ds, "table");
        return ds;
    }
    protected void GetCode()
    {
        SqlConnection conn = new SqlConnection(strconn);
        string Query = "select max(charge_code)+ 1 as Code from M_Charge";
        SqlDataAdapter da = new SqlDataAdapter(Query, conn);
        try
        {
            DataSet ds = new DataSet();
            da.Fill(ds, "charge");
            if (ds.Tables["charge"].Rows.Count == 0)
            {
                Session["Code"] = "101";
            }
            else
            {
                DataRowView row = ds.Tables["charge"].DefaultView[0];
                string CCODE = row["Code"].ToString();
                Session["Code"] = CCODE;
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('" + ex.Message + "');", true);
        }

    }
    protected void BtnSubmit_Click(object sender, EventArgs e)
    {
        string cCode = txtcCode.Text;
        string cDesc = txtcDesc.Text;
        string Query = "";
      
        try
        {
            SqlConnection conn = new SqlConnection(strconn);
            Query = "select * from M_Charge where charge_desc='" + cDesc + "'";
            SqlDataAdapter da = new SqlDataAdapter(Query, conn);
            DataSet ds = new DataSet();
            da.Fill(ds, "table");
            if (ds.Tables["table"].Rows.Count == 0)
            {
                GetValue(cCode, cDesc);
                txtcDesc.Text = "";
                txtcCode.Text = "";
             
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Charge Details has stored Successfully');", true);
                BTNADD.Enabled = true;
            }
            else
            {
               
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Given Charges Description has stored already....');", true);
               
                txtcDesc.Text = "";
            }

        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('" + ex.Message + "');", true);
        }
    }
    protected void GetValue(string cCode, string cDesc)
    {
        try
        {
            SqlConnection conn = new SqlConnection(strconn);
            string Query = "select * from M_Charge where charge_code=" + cCode + "";
            SqlDataAdapter da = new SqlDataAdapter(Query, conn);
            DataSet ds = new DataSet();
            da.Fill(ds, "table");
            if (ds.Tables["table"].Rows.Count == 0)
            {

                Query = "insert into M_Charge(charge_code,charge_desc) values('" + cCode + "','" + cDesc + "')";
                GetCommand(Query);

               
                txtcCode.Text = "";
                txtcDesc.Text = "";
            }
            else
            {
                Response.Write("<script>alert('Given Charges Code is unique ... ')</script>");
                txtcCode.Text = "";
               

            }
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
    }

    protected void GridView1_selectChanged(object sender, EventArgs e)
    {
        string cCode = Convert.ToString(GridView1.SelectedDataKey.Value);
        SqlConnection conn = new SqlConnection(strconn);
        string Query = "select * from M_Charge where charge_code=" + cCode + "";
        SqlDataAdapter da = new SqlDataAdapter(Query, conn);
        try
        {
            DataSet ds = new DataSet();
            da.Fill(ds, "table");
            if (ds.Tables["table"].Rows.Count != 0)
            {
                DataRowView row = ds.Tables["table"].DefaultView[0];
                string chargeDes = row["charge_desc"].ToString();
                txtcCode.Text = cCode;
                txtcDesc.Text = chargeDes;
                GridView1.Visible = false;
                grdTbl.Visible = false;
            }
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('" + ex.Message + "');", true);
        }
    }
    protected void BTNView_Click(object sender, EventArgs e)
    {
        string sqlQuery = "select * from M_Charge";
        GridView1.DataSource = GetData(sqlQuery);
        GridView1.DataBind();
        GridView1.Visible = true;
        grdTbl.Visible = true;
        Panel1.Visible = true;
    }
    protected void BtnDelete_Click(object sender, EventArgs e)
    {
        string cCode=txtcCode.Text;
         SqlConnection conn = new SqlConnection(strconn);
         string Query = "select * from M_Charge where charge_code=" + cCode + "";
        SqlDataAdapter da = new SqlDataAdapter(Query, conn);
        try
        {
            DataSet ds = new DataSet();
            da.Fill(ds, "table");
            if (ds.Tables["table"].Rows.Count != 0)
            {
                string sqlQuery = "delete from M_Charge where charge_code=" + cCode + "";
                GetCommand(sqlQuery);
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Successfully Deleted');", true);
            }
            else
            {
                Response.Write("<script>alert('Which Charge Details want to be Delete ')</script>");

            }
            txtcCode.Text = "";
            txtcDesc.Text = "";
        }
        catch (Exception ex)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('" + ex.Message + "');", true);
        }

    }
    protected void BTNADD_Click(object sender, EventArgs e)
    {
        GetCode();
        txtcCode.Text = (string)Session["Code"];
        txtcDesc.Text = "";
        BTNADD.Enabled = false;
    }
    protected void BTNClear_Click(object sender, EventArgs e)
    {
        txtcCode.Text = "";
        txtcDesc.Text = "";
        BTNADD.Enabled = true;
    }
    protected void BTNEXIT_Click(object sender, EventArgs e)
    {
      
    }
   
}
