using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace ImpexCube.Billing
{
    public partial class frmContract : System.Web.UI.Page
    {
        string con = (string)ConfigurationManager.ConnectionStrings["Constr"].ConnectionString;
        string keyname = "QtNo";
        Boolean vi;
        Boolean vc;
        Boolean tfeet;
        Boolean ffeet;
        Boolean LCL;
        Boolean AIR;
        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack == false)
            {
                string fyear = (string)Session["FYear"];
                Session["key"] = "";

                Session["key"] = keyname + "/" + fyear + "/" + Convert.ToString(GetNextAutoNo(keyname, 0, con));
                Session["Keycode"] = Convert.ToString(GetNextAutoNo(keyname, 0,con));
                lblQuoteNo.Text = (string)Session["key"];

                LoadDescript();
                LoadCustomer();                
                LoadGrid();
                btnSave.Visible = true;
                btnUpdate.Visible = false;
            }
        }
        public static int GetNextAutoNo(string custCode, int key, string con)
        {
            SqlConnection cnn = new SqlConnection(con);
            cnn.Open();
            string query = "select * from M_AutoGenerate where keyName='" + custCode + "'";
            SqlDataAdapter da = new SqlDataAdapter(query, cnn);
            DataSet ds = new DataSet();
            da.Fill(ds, "AutoNum");
            cnn.Close();

            if (ds.Tables["AutoNum"].Rows.Count != 0)
            {
                DataRowView row = ds.Tables["AutoNum"].DefaultView[0];
                key = Convert.ToInt16(row["keycode"]);
                key = key + 1;
            }
            return key;
        }

        public void LoadDescript()
        {
            SqlConnection sqlcon = new SqlConnection(con);
            sqlcon.Open();
            string query = "select * from M_Charge";
            SqlDataAdapter da = new SqlDataAdapter(query, sqlcon);
            DataSet ds = new DataSet();
            da.Fill(ds, "Charges");
            sqlcon.Close();

            ddl20FeetCharges.DataSource = ds;
            ddl20FeetCharges.DataTextField = "charge_desc";
            ddl20FeetCharges.DataValueField = "charge_desc";
            ddl20FeetCharges.DataBind();
            //ddl20FeetCharges.Items.Insert(0, new ListItem("~Select~", "0"));

            ddl40FeetCharges.DataSource = ds;
            ddl40FeetCharges.DataTextField = "charge_desc";
            ddl40FeetCharges.DataValueField = "charge_desc";
            ddl40FeetCharges.DataBind();
            //ddl40FeetCharges.Items.Insert(0, new ListItem("~Select~", "0"));

            ddlLCLCharges.DataSource = ds;
            ddlLCLCharges.DataTextField = "charge_desc";
            ddlLCLCharges.DataValueField = "charge_desc";
            ddlLCLCharges.DataBind();
            //ddlLCLCharges.Items.Insert(0, new ListItem("~Select~", "0"));

            ddlAIRCharges.DataSource = ds;
            ddlAIRCharges.DataTextField = "charge_desc";
            ddlAIRCharges.DataValueField = "charge_desc";
            ddlAIRCharges.DataBind();
            //ddlAIRCharges.Items.Insert(0, new ListItem("~Select~", "0"));
        }
        public void LoadCustomer()
        {
            SqlConnection sqlcon = new SqlConnection(con);
            string query = "select Distinct AccountName from M_AccountMaster";
            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter(query, sqlcon);
            da.Fill(ds, "Customer");
            ddlCusName.DataSource = ds;
            ddlCusName.DataTextField = "AccountName";
            ddlCusName.DataValueField = "AccountName";
            ddlCusName.DataBind();
            sqlcon.Close();

        }
        public void LoadGrid()
        {
            DataSet ds = new DataSet();
            SqlConnection sqlcon = new SqlConnection(con);
            string query = "Select ID,Description,CustomerName,Type,Unit,ActualRate,MinRate,VarRate,MaxRate,FixRate,QuoteNo from M_Quote";
            SqlDataAdapter da = new SqlDataAdapter(query, sqlcon);
            da.Fill(ds, "StandardRateDetails");
            GvQuote.DataSource = ds;
            GvQuote.DataBind();
            sqlcon.Close();

        }

        protected void ddl20FeetCharges_SelectedIndexChanged(object sender, EventArgs e)
        {
            string chargedesc = ddl20FeetCharges.SelectedValue;
            //ddl40FeetCharges.SelectedValue = chargedesc;
            //ddlLCLCharges.SelectedValue = chargedesc;
            //ddlAIRCharges.SelectedValue = chargedesc;
            SqlConnection sqlcon = new SqlConnection(con);
            sqlcon.Open();
            SqlDataAdapter sd = new SqlDataAdapter("Select ID,Description,Type,Unit,ActualRate,MinRate,VarRate,MaxRate,FixRate from M_StandardRate where Description='" + ddl20FeetCharges.SelectedValue + "' and Type='20Feet'", con);
            DataSet ds = new DataSet();
            sd.Fill(ds, "data");
            sqlcon.Close();
            if (ds.Tables["data"].Rows.Count != 0)
            {
                DataRowView row = ds.Tables["data"].DefaultView[0];
                ddl20FeetUnit.SelectedValue = row["Unit"].ToString();
                txt20FeetAtActual.Text = row["ActualRate"].ToString();
                txt20feetMinimum.Text = row["MinRate"].ToString();
                txt20feetVariable.Text = row["VarRate"].ToString();
                txt20feetMaximum.Text = row["MaxRate"].ToString();
                txt20feetFixed.Text = row["FixRate"].ToString();

            }
            SqlDataAdapter sd1 = new SqlDataAdapter("Select ID,Description,Type,Unit,ActualRate,MinRate,VarRate,MaxRate,FixRate from M_StandardRate where Description='" + ddl20FeetCharges.SelectedValue + "' and Type='40Feet'", con);
            DataSet ds1 = new DataSet();
            sd1.Fill(ds1, "data");
            sqlcon.Close();
            if (ds1.Tables["data"].Rows.Count != 0)
            {
                DataRowView row = ds1.Tables["data"].DefaultView[0];
                ddl40feetUnit.SelectedValue = row["Unit"].ToString();
                txt40FeetAtActual.Text = row["ActualRate"].ToString();
                txt40FeetMinimum.Text = row["MinRate"].ToString();
                txt40FeetVariable.Text = row["VarRate"].ToString();
                txt40FeetMaximum.Text = row["MaxRate"].ToString();
                txt40FeetFixed.Text = row["FixRate"].ToString();

            }
            SqlDataAdapter sd2 = new SqlDataAdapter("Select ID,Description,Type,Unit,ActualRate,MinRate,VarRate,MaxRate,FixRate from M_StandardRate where Description='" + ddl20FeetCharges.SelectedValue + "' and Type='LCL'", con);
            DataSet ds2 = new DataSet();
            sd2.Fill(ds2, "data");
            sqlcon.Close();
            if (ds2.Tables["data"].Rows.Count != 0)
            {
                DataRowView row = ds2.Tables["data"].DefaultView[0];
                ddlLCLUnit.SelectedValue = row["Unit"].ToString();
                txtLCLAtActual.Text = row["ActualRate"].ToString();
                txtLCLMinimum.Text = row["MinRate"].ToString();
                txtLCLVariable.Text = row["VarRate"].ToString();
                txtLCLMaximum.Text = row["MaxRate"].ToString();
                txtLCLFixed.Text = row["FixRate"].ToString();

            }
            SqlDataAdapter sd3 = new SqlDataAdapter("Select ID,Description,Type,Unit,ActualRate,MinRate,VarRate,MaxRate,FixRate from M_StandardRate where Description='" + ddl20FeetCharges.SelectedValue + "' and Type='AIR'", con);
            DataSet ds3 = new DataSet();
            sd3.Fill(ds3, "data");
            sqlcon.Close();
            if (ds3.Tables["data"].Rows.Count != 0)
            {
                DataRowView row = ds3.Tables["data"].DefaultView[0];
                ddlAirUnit.SelectedValue = row["Unit"].ToString();
                txtairAtAcutal.Text = row["ActualRate"].ToString();
                txtairminimum.Text = row["MinRate"].ToString();
                txtairVariable.Text = row["VarRate"].ToString();
                txtairMaximum.Text = row["MaxRate"].ToString();
                txtairFixed.Text = row["FixRate"].ToString();

            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
       {
           
            string date = DateTime.Now.ToString("dd/MM/yyyy");
            SqlConnection sqlConn = new SqlConnection(con);
            sqlConn.Open();
         
                string insert20feet = "Insert into M_Quote(QuoteNo,Description,CustomerName,Type,Unit,ActualRate,MinRate,VarRate,MaxRate,FixRate,CreatedBy,CreatedDate)" +
                                     "values('" + (string)Session["key"] + "','" + ddl20FeetCharges.SelectedValue + "','" + ddlCusName.SelectedValue + "','20Feet','" + ddl20FeetUnit.SelectedValue + "','" + txt20FeetAtActual.Text + "','" + txt20feetMinimum.Text + "','" + txt20feetVariable.Text + "','" + txt20feetMaximum.Text + "','" + txt20feetFixed.Text + "','" + (string)Session["USER-NAME"] + "','" + frmdatesplit(date) + "')";
                string insert40feet = "Insert into M_Quote(QuoteNo,Description,CustomerName,Type,Unit,ActualRate,MinRate,VarRate,MaxRate,FixRate,CreatedBy,CreatedDate)" +
                                     "values('" + (string)Session["key"] + "','" + ddl40FeetCharges.SelectedValue + "','" + ddlCusName.SelectedValue + "','40Feet','" + ddl40feetUnit.SelectedValue + "','" + txt40FeetAtActual.Text + "','" + txt40FeetMinimum.Text + "','" + txt40FeetVariable.Text + "','" + txt40FeetMaximum.Text + "','" + txt40FeetFixed.Text + "','" + (string)Session["USER-NAME"] + "','" + frmdatesplit(date) + "')";
                string insertlcl = "Insert into M_Quote(QuoteNo,Description,CustomerName,Type,Unit,ActualRate,MinRate,VarRate,MaxRate,FixRate,CreatedBy,CreatedDate)" +
                                     "values('" + (string)Session["key"] + "','" + ddlLCLCharges.SelectedValue + "','" + ddlCusName.SelectedValue + "','LCL','" + ddlLCLUnit.SelectedValue + "','" + txtLCLAtActual.Text + "','" + txtLCLMinimum.Text + "','" + txtLCLVariable.Text + "','" + txtLCLMaximum.Text + "','" + txtLCLFixed.Text + "','" + (string)Session["USER-NAME"] + "','" + frmdatesplit(date) + "')";
                string insertair = "Insert into M_Quote(QuoteNo,Description,CustomerName,Type,Unit,ActualRate,MinRate,VarRate,MaxRate,FixRate,CreatedBy,CreatedDate)" +
                                     "values('" + (string)Session["key"] + "','" + ddlAIRCharges.SelectedValue + "','" + ddlCusName.SelectedValue + "','AIR','" + ddlAirUnit.SelectedValue + "','" + txtairAtAcutal.Text + "','" + txtairminimum.Text + "','" + txtairVariable.Text + "','" + txtairMaximum.Text + "','" + txtairFixed.Text + "','" + (string)Session["USER-NAME"] + "','" + frmdatesplit(date) + "')";

                SqlCommand cmd = new SqlCommand(insert20feet, sqlConn);
                SqlDataAdapter da = new SqlDataAdapter();
                cmd.ExecuteNonQuery();

                SqlCommand cmd1 = new SqlCommand(insert40feet, sqlConn);
                SqlDataAdapter da1 = new SqlDataAdapter();
                //da.SelectCommand = cmd1;
                cmd1.ExecuteNonQuery();

                SqlCommand cmd2 = new SqlCommand(insertlcl, sqlConn);
                SqlDataAdapter da2 = new SqlDataAdapter();
                //da.SelectCommand = cmd;
                cmd2.ExecuteNonQuery();

                SqlCommand cmd3 = new SqlCommand(insertair, sqlConn);
                SqlDataAdapter da3 = new SqlDataAdapter();
                //da.SelectCommand = cmd;
                cmd3.ExecuteNonQuery();


                int code = Convert.ToInt32((string)Session["Keycode"]);
                string updateQuoteNo = "Update M_AutoGenerate set KeyCode='" + code + "' where KeyName='QtNo'";
                SqlCommand cmd4 = new SqlCommand(updateQuoteNo, sqlConn);
                cmd4.ExecuteNonQuery();

                sqlConn.Close();
                LoadGrid();
                clear();
                btnSave.Visible = true;
                btnUpdate.Visible = false;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Successfully Saved');", true);
            //}

            //else
            //{
            //}
        }

        public Boolean validateinputs()
        {
            if (ddlCusName.SelectedIndex == 0)
            {
                MessageBox.Show("Customer Name is not chosen", "Notification", MessageBoxButtons.OK);
                vi = true;
            }
            else
            {
                vi = false;
            }
            return vi;
        }

        public Boolean validateCharges()
        {
            if ((ddl20FeetCharges.SelectedIndex == 0) && (ddl40FeetCharges.SelectedIndex == 0) && (ddlLCLCharges.SelectedIndex == 0) && (ddlAIRCharges.SelectedIndex == 0))
            {
                MessageBox.Show("Select atleast one charge", "Notification", MessageBoxButtons.OK);
                vc = true;
            }
            else
            {
                vc = false;
            }
            return vc;
        }


        public Boolean tfeetCharge()
        {
            if ((txt20feetMinimum.Text != "") && (txt20feetMaximum.Text != ""))
            {
                if (Convert.ToInt32(txt20feetMinimum.Text) > Convert.ToInt32(txt20feetMaximum.Text))
                {
                    MessageBox.Show("Minimum value should be less than Maximum value in 20 feet charge", "Notification", MessageBoxButtons.OK);
                    tfeet = true;
                }
                else
                {

                }               
            }
            return tfeet;

        }
    public Boolean ffeetCharge()
    {

        if ((txt40FeetMinimum.Text != "") && (txt40FeetMaximum.Text != ""))
        {
            if (Convert.ToInt32(txt40FeetMinimum.Text) > Convert.ToInt32(txt40FeetMaximum.Text))
            {
                MessageBox.Show("Minimum value should be less than Maximum value in 40 feet charge", "Notification", MessageBoxButtons.OK);
                ffeet = true;
            }
            else
            {
                ffeet = false;
            }
            
        }
        return ffeet;
    }

    public Boolean LCLCharge()
    {
        if ((txtLCLMinimum.Text != "") && (txtLCLMaximum.Text != ""))
        {
            if (Convert.ToInt32(txtLCLMinimum.Text) > Convert.ToInt32(txtLCLMaximum.Text))
            {
                MessageBox.Show("Minimum value should be less than Maximum value in LCL", "Notification", MessageBoxButtons.OK);
                LCL = true;
            }
            else
            {
                LCL = false;
            }
           
        }
        return LCL;
    }
    public Boolean AIRCharge()
    {

        if ((txtairminimum.Text != "") && (txtairMaximum.Text != ""))
        {
            if (Convert.ToInt32(txtairminimum.Text) > Convert.ToInt32(txtairMaximum.Text))
            {
                MessageBox.Show("Minimum value should be less than Maximum value in AIR", "Notification", MessageBoxButtons.OK);
                AIR = true;
            }
            else
            {
                AIR = false;
            }
           
        }
        return AIR;
    }          

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            string date = DateTime.Now.ToString("dd/MM/yyyy");
            SqlConnection sqlConn = new SqlConnection(con);
            sqlConn.Open();
            Session["Id"] = GvQuote.SelectedRow.Cells[1].Text.ToString();
            string query = "";

            if ((string)Session["Type"] == "20Feet")
            {
                query = "Update M_Quote set Description='" + ddl20FeetCharges.SelectedValue + "',CustomerName='" + ddlCusName.SelectedValue + "',Type='20Feet',Unit='" + ddl20FeetUnit.SelectedValue + "',ActualRate='" + txt20FeetAtActual.Text + "',MinRate='" + txt20feetMinimum.Text + "',VarRate='" + txt20feetVariable.Text + "',MaxRate='" + txt20feetMaximum.Text + "',FixRate='" + txt20feetFixed.Text + "',CreatedBy='" + (string)Session["USER-NAME"] + "',CreatedDate='" + frmdatesplit(date) + "' where ID='" + (String)Session["Id"] + "' ";
            }
            else if ((string)Session["Type"] == "40Feet")
            {
                query = "Update M_Quote set Description='" + ddl40FeetCharges.SelectedValue + "',CustomerName='" + ddlCusName.SelectedValue + "',Type='40Feet',Unit='" + ddl40feetUnit.SelectedValue + "',ActualRate='" + txt40FeetAtActual.Text + "',MinRate='" + txt40FeetMinimum.Text + "',VarRate='" + txt40FeetVariable.Text + "',MaxRate='" + txt40FeetMaximum.Text + "',FixRate='" + txt40FeetFixed.Text + "',CreatedBy='" + (string)Session["USER-NAME"] + "',CreatedDate='" + frmdatesplit(date) + "' where ID='" + (String)Session["Id"] + "'";
            }
            else if ((string)Session["Type"] == "LCL")
            {
                query = "Update M_Quote set Description='" + ddlLCLCharges.SelectedValue + "',CustomerName='" + ddlCusName.SelectedValue + "',Type='LCL',Unit='" + ddlLCLUnit.SelectedValue + "',ActualRate='" + txtLCLAtActual.Text + "',MinRate='" + txtLCLMinimum.Text + "',VarRate='" + txtLCLVariable.Text + "',MaxRate='" + txtLCLMaximum.Text + "',FixRate='" + txtLCLFixed.Text + "',CreatedBy='" + (string)Session["USER-NAME"] + "',CreatedDate='" + frmdatesplit(date) + "' where Id='" + (String)Session["Id"] + "'";
            }
            else if ((string)Session["Type"] == "AIR")
            {
                query = "Update M_Quote set Description='" + ddlAIRCharges.SelectedValue + "',CustomerName='" + ddlCusName.SelectedValue + "',Type='AIR',Unit='" + ddlAirUnit.SelectedValue + "',ActualRate='" + txtairAtAcutal.Text + "',MinRate='" + txtairminimum.Text + "',VarRate='" + txtairVariable.Text + "',MaxRate='" + txtairMaximum.Text + "',FixRate='" + txtairFixed.Text + "',CreatedBy='" + (string)Session["USER-NAME"] + "',CreatedDate='" + frmdatesplit(date) + "' where Id='" + (String)Session["Id"] + "'";
            }

            SqlCommand cmd = new SqlCommand(query, sqlConn);
            cmd.ExecuteNonQuery();

            sqlConn.Close();
            LoadGrid();
            clear();
            btnSave.Visible = true;
            btnUpdate.Visible = false;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Successfully Updated');", true);
        }

        protected void GvQuote_SelectedIndexChanged(object sender, EventArgs e)
        {
            string typecont = GvQuote.SelectedRow.Cells[4].Text;
            Session["Type"] = typecont;

            if (typecont == "20Feet")
            {
                ddlCusName.SelectedValue = GvQuote.SelectedRow.Cells[3].Text;
                ddl20FeetCharges.SelectedValue = GvQuote.SelectedRow.Cells[2].Text;
                ddl20FeetUnit.SelectedValue = GvQuote.SelectedRow.Cells[5].Text;
                txt20FeetAtActual.Text = GvQuote.SelectedRow.Cells[6].Text;
                txt20feetMinimum.Text = GvQuote.SelectedRow.Cells[7].Text;
                txt20feetVariable.Text = GvQuote.SelectedRow.Cells[8].Text;
                txt20feetMaximum.Text = GvQuote.SelectedRow.Cells[9].Text;
                txt20feetFixed.Text = GvQuote.SelectedRow.Cells[10].Text;
                lblQuoteNo.Text = GvQuote.SelectedRow.Cells[11].Text;
            }
            else if (typecont == "40Feet")
            {
                ddlCusName.SelectedValue = GvQuote.SelectedRow.Cells[3].Text;
                ddl40FeetCharges.SelectedValue = GvQuote.SelectedRow.Cells[2].Text;
                ddl40feetUnit.SelectedValue = GvQuote.SelectedRow.Cells[5].Text;
                txt40FeetAtActual.Text = GvQuote.SelectedRow.Cells[6].Text;
                txt40FeetMinimum.Text = GvQuote.SelectedRow.Cells[7].Text;
                txt40FeetVariable.Text = GvQuote.SelectedRow.Cells[8].Text;
                txt40FeetMaximum.Text = GvQuote.SelectedRow.Cells[9].Text;
                txt40FeetFixed.Text = GvQuote.SelectedRow.Cells[10].Text;
                lblQuoteNo.Text = GvQuote.SelectedRow.Cells[11].Text;
            }
            else if (typecont == "LCL")
            {
                ddlCusName.SelectedValue = GvQuote.SelectedRow.Cells[3].Text;
                ddlLCLCharges.SelectedValue = GvQuote.SelectedRow.Cells[2].Text;
                ddlLCLUnit.SelectedValue = GvQuote.SelectedRow.Cells[5].Text;
                txtLCLAtActual.Text = GvQuote.SelectedRow.Cells[6].Text;
                txtLCLMinimum.Text = GvQuote.SelectedRow.Cells[7].Text;
                txtLCLVariable.Text = GvQuote.SelectedRow.Cells[8].Text;
                txtLCLMaximum.Text = GvQuote.SelectedRow.Cells[9].Text;
                txtLCLFixed.Text = GvQuote.SelectedRow.Cells[10].Text;
                lblQuoteNo.Text = GvQuote.SelectedRow.Cells[11].Text;
            }
            else if (typecont == "AIR")
            {
                ddlCusName.SelectedValue = GvQuote.SelectedRow.Cells[3].Text;
                ddlAIRCharges.SelectedValue = GvQuote.SelectedRow.Cells[2].Text;
                ddlAirUnit.SelectedValue = GvQuote.SelectedRow.Cells[5].Text;
                txtairAtAcutal.Text = GvQuote.SelectedRow.Cells[6].Text;
                txtairminimum.Text = GvQuote.SelectedRow.Cells[7].Text;
                txtairVariable.Text = GvQuote.SelectedRow.Cells[8].Text;
                txtairMaximum.Text = GvQuote.SelectedRow.Cells[9].Text;
                txtairFixed.Text = GvQuote.SelectedRow.Cells[10].Text;
                lblQuoteNo.Text = GvQuote.SelectedRow.Cells[11].Text;
            }
            btnSave.Visible = false;
            btnUpdate.Visible = true;
        }
        private string frmdatesplit(string frmdate)
        {
            string[] frmdate1 = frmdate.Split('/');
            string frmdate2 = frmdate1[1] + '/' + frmdate1[0] + '/' + frmdate1[2];
            return frmdate2;
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            clear();
            btnSave.Visible = true;
            btnUpdate.Visible = false;
        }
        public void clear()
        {
            ddl20FeetCharges.SelectedValue = "~Select~";
            ddl40FeetCharges.SelectedValue = "~Select~";
            ddlLCLCharges.SelectedValue = "~Select~";
            ddlAIRCharges.SelectedValue = "~Select~";
            ddl20FeetUnit.SelectedValue = ddl40feetUnit.SelectedValue = ddlLCLUnit.SelectedValue = ddlAirUnit.SelectedValue = "~Select~";
            txt20FeetAtActual.Text = txt40FeetAtActual.Text = txtLCLAtActual.Text = txtairAtAcutal.Text = "";
            txt20feetMinimum.Text = txt40FeetMinimum.Text = txtLCLMinimum.Text = txtairminimum.Text = "";
            txt20feetVariable.Text = txt40FeetVariable.Text = txtLCLVariable.Text = txtairVariable.Text = "";
            txt20feetMaximum.Text = txt40FeetMaximum.Text = txtLCLMaximum.Text = txtairMaximum.Text = "";
            txt20feetFixed.Text = txt40FeetFixed.Text = txtLCLFixed.Text = txtairFixed.Text = "";

        }

        protected void btnPrint_Click(object sender, EventArgs e)
        {
            Session["CustomerName"] = ddlCusName.SelectedItem.Text;
            Response.Redirect("frmQuotereport.aspx");
        }

        public void FillGrid()
        {
            DataSet ds = new DataSet();
            SqlConnection sqlcon = new SqlConnection(con);
            string query = "Select ID,Description,CustomerName,Type,Unit,ActualRate,MinRate,VarRate,MaxRate,FixRate,QuoteNo from M_Quote where CustomerName='" + ddlCusName.Text + "'";
            SqlDataAdapter da = new SqlDataAdapter(query, sqlcon);
            da.Fill(ds, "StandardRateDetails");
            GvQuote.DataSource = ds;
            GvQuote.DataBind();
            sqlcon.Close();
        }

        protected void ddlCusName_SelectedIndexChanged(object sender, EventArgs e)
        {
            FillGrid();
        }
    }
}