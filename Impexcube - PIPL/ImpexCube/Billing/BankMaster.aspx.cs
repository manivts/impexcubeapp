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
using System.Collections.Generic;



public partial class Master_BankMaster : System.Web.UI.Page
{
    string strconn = (string)ConfigurationManager.ConnectionStrings["Constr"].ConnectionString;
    //string strconn = (string)ConfigurationManager.AppSettings["ConnectionImpex"];
    protected void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack == false)
        {
           
            if (Session["BankName"] != null)
            {
                int vsno =Convert.ToInt32(Session["BankName"]);
              

            }
            //txt_bname.ReadOnly = true;
            //txt_accno.ReadOnly = true;
            //txt_address.ReadOnly = true;
            //txt_city.ReadOnly = true;
            //txt_pincode.ReadOnly = true;
            GetBank();
        }
       // btn_view.Attributes.Add("onclick", "OpenPopup();");
    }
    protected void btn_add_Click(object sender, EventArgs e)
    {
        Session["Bank"] = "Add";
        txt_accno.Text = txt_bname.Text = txt_address.Text = txt_city.Text = txt_pincode.Text = "";
        txt_bname.Focus();
        txt_bname.ReadOnly = false;
        txt_accno.ReadOnly = false;
        txt_address.ReadOnly = false;
        txt_city.ReadOnly = false;
        txt_pincode.ReadOnly = false;
       

    }
    protected void btn_modify_Click(object sender, EventArgs e)
    {
        Session["Bank"] = "Modify";
        txt_bname.ReadOnly = false;
        txt_accno.ReadOnly = false;
        txt_address.ReadOnly = false;
        txt_city.ReadOnly = false;
        txt_pincode.ReadOnly = false;
        Session["ACCNo"] = txt_accno.Text;
     
    }
    protected void btn_save_Click(object sender, EventArgs e)
    {
        try
        {
            string Bank = (string)Session["Bank"];
            string ban = (string)Session["viewvalue"];
            if (Bank == "Add")
            {
                string bname = txt_bname.Text;
                decimal accno = Convert.ToDecimal(txt_accno.Text);
                string address = txt_address.Text;
                string city = txt_city.Text;
                string pincode = txt_pincode.Text;

                SqlConnection conn1 = new SqlConnection(strconn);
                string Query = "select AccNo from M_BankMaster where AccNo='" + accno + "'";
                SqlDataAdapter da1 = new SqlDataAdapter(Query, conn1);
                DataSet ds = new DataSet();
                da1.Fill(ds, "contact");

                if (ds.Tables["contact"].Rows.Count != 0)
                {
                    Response.Write("<script>alert('Your AccNo is already exited')</script>");

                }
                else
                {

                    SqlConnection conn = new SqlConnection(strconn);
                    string query = "insert into M_BankMaster(BankName,AccNo,Address,City,PinCode)values('" + bname + "'," + accno + ",'" + address + "','" + city + "','" + pincode + "')";
                    try
                    {
                        conn.Open();
                        SqlCommand cmd = new SqlCommand(query, conn);
                        SqlDataAdapter da = new SqlDataAdapter();
                        SqlCommand upcmd = new SqlCommand(query, conn);
                        upcmd.CommandText = query;
                        upcmd.Connection = conn;
                        da.SelectCommand = upcmd;

                        int result = upcmd.ExecuteNonQuery();
                        conn.Close();

                        Response.Write("<script>alert('Bank Details has been inserted successfully') </script>");
                        txt_accno.Text = txt_bname.Text = txt_address.Text = txt_city.Text = txt_pincode.Text = "";
                        //txt_bname.ReadOnly = true;
                        //txt_accno.ReadOnly = true;
                        //txt_address.ReadOnly = true;
                        //txt_city.ReadOnly = true;
                        //txt_pincode.ReadOnly = true;


                    }
                    catch (Exception ex)
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('" + ex.Message + "');", true);
                    }
                }
            }
            else if ((Bank == "Modify") && (ban == "Edit"))
            {
                string bname = txt_bname.Text;
                decimal accno = Convert.ToDecimal(txt_accno.Text);
                int sno = Convert.ToInt32(lbl.Text);
                string address = txt_address.Text;
                string city = txt_city.Text;
                string pincode = txt_pincode.Text;

                SqlConnection conn1 = new SqlConnection(strconn);
                string Query = "select AccNo from M_BankMaster where AccNo='" + accno + "'";
                SqlDataAdapter da1 = new SqlDataAdapter(Query, conn1);
                DataSet ds = new DataSet();
                da1.Fill(ds, "contact");

                if (ds.Tables["contact"].Rows.Count != 0)
                {


                    SqlConnection conn = new SqlConnection(strconn);
                    string update = "update M_BankMaster set BankName='" + bname + "',AccNo=" + accno + ",Address='" + address + "',City='" + city + "',PinCode='" + pincode + "' where TransId=" + sno + " ";

                    try
                    {
                        conn.Open();
                        SqlCommand cmd = new SqlCommand(update, conn);
                        SqlDataAdapter da = new SqlDataAdapter();
                        SqlCommand upcmd = new SqlCommand(update, conn);
                        upcmd.CommandText = update;
                        upcmd.Connection = conn;
                        da.SelectCommand = upcmd;

                        int result = upcmd.ExecuteNonQuery();
                        conn.Close();

                        Response.Write("<script>alert('Bank Details has been updated successfully') </script>");
                        txt_bname.Text = txt_accno.Text = txt_address.Text = txt_city.Text = txt_pincode.Text = lbl.Text = "";
                        //txt_bname.ReadOnly = true;
                        //txt_accno.ReadOnly = true;
                        //txt_address.ReadOnly = true;
                        //txt_city.ReadOnly = true;
                        //txt_pincode.ReadOnly = true;


                    }
                    catch (Exception ex)
                    {
                        lblresult.Text = ex.Message;


                    }
                }
            }
            GetBank();
        }
        catch (Exception ex)
        { 
         Response.Write(ex.Message);
        }
    }
    
   
    protected void btn_clear_Click(object sender, EventArgs e)
    {
        txt_accno.Text = "";
        txt_bname.Text = "";
        txt_address.Text = "";
        txt_city.Text = "";
        txt_pincode.Text = "";
        //txt_bname.ReadOnly = true;
        //txt_accno.ReadOnly = true;
        //txt_address.ReadOnly = true;
        //txt_city.ReadOnly = true;
        //txt_pincode.ReadOnly = true;
        
    }
    protected void btn_exit_Click(object sender, EventArgs e)
    {
       
    }

    protected void btn_view_Click(object sender, EventArgs e)
    {
        Session["Bank"] = "Edit";
        Session["viewvalue"] = "Edit";
        GetBank();

    }
  

    public void GetBank()
    {
        SqlConnection conn = new SqlConnection(strconn);
        string query = "select * from M_BankMaster";
        SqlDataAdapter da = new SqlDataAdapter(query, conn);
        DataSet ds = new DataSet();
        da.Fill(ds, "Master");
        gvBank.DataSource = ds;
        gvBank.DataBind();
    }

    protected void gvBank_SelectedIndexChanged(object sender, EventArgs e)
    {
        Session["Bank"] = "Modify";
        Session["viewvalue"] = "Edit";
        lbl.Text = gvBank.SelectedRow.Cells[1].Text;
        txt_bname.Text = gvBank.SelectedRow.Cells[2].Text;
        txt_accno.Text = gvBank.SelectedRow.Cells[3].Text;
        txt_address.Text = gvBank.SelectedRow.Cells[4].Text;
        txt_city.Text = gvBank.SelectedRow.Cells[5].Text;
        txt_pincode.Text = gvBank.SelectedRow.Cells[6].Text;
    }
 
}
