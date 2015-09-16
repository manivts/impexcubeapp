using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace ImpexCube.CRM
{
    public partial class frmStandardRate1 : System.Web.UI.Page
    {
        string con = (string)ConfigurationManager.ConnectionStrings["Constr"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack==false)
            {
                LoadDescript();
                //GridLoad();
                FillGvAir();
                FillGvLcl();
                FillGv20feet();
                FillGv40Feet();
                btnSave.Visible = false;
                btnUpdate.Visible = false;
            }
        }

        public void LoadDescript()
        {           
            SqlConnection sqlcon = new SqlConnection(con);
            sqlcon.Open();
            string query = "SELECT * FROM M_Charge WHERE(cCode <> 'BHEL') OR(cCode IS NULL)";
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
        //public void GridLoad()
        //{
        //    DataSet ds = new DataSet();
        //    SqlConnection sqlcon = new SqlConnection(con);
        //    string query = "Select ID,ShipMode,Description,Type,Unit,ActualRate,MinRate,VarRate,VarType,MaxRate,FixRate from M_StandardRate";
        //    SqlDataAdapter da = new SqlDataAdapter(query, sqlcon);
        //    da.Fill(ds, "StandardRateDetails");
        //    GvStandard.DataSource = ds;
        //    GvStandard.DataBind();
        //    sqlcon.Close();            

        //}
        //public void GridLoadSR()
        //{           
        //    SqlConnection sqlcon = new SqlConnection(con);
        //    string query = "Select ID,ShipMode,Description,Type,Unit,ActualRate,MinRate,VarRate,VarType,MaxRate,FixRate from M_StandardRate where Description='" + ddl20FeetCharges.SelectedValue + "'";
        //    SqlDataAdapter da = new SqlDataAdapter(query, sqlcon);
        //    DataSet ds = new DataSet();
        //    da.Fill(ds, "StandardRateDetails");
        //    GvStandard.DataSource = ds;
        //    GvStandard.DataBind();
        //    sqlcon.Close();  
        //}
        public void clear()
        {
            ddl20FeetCharges.SelectedValue = "~Select~";
            ddl40FeetCharges.SelectedValue = "~Select~";
            ddlLCLCharges.SelectedValue = "~Select~";
            ddlAIRCharges.SelectedValue = "~Select~";
            ddl20FeetUnit.SelectedValue = ddl40feetUnit.SelectedValue = ddlLCLUnit.SelectedValue = ddlAirUnit.SelectedValue = "~Select~";
            ddlvarair.SelectedValue = ddlvarlcl.SelectedValue = ddlvar40.SelectedValue = ddlvar20.SelectedValue = "~Select~";
            //txt20FeetAtActual.Text = txt40FeetAtActual.Text = txtLCLAtActual.Text = txtairAtAcutal.Text = "";
            txt20feetMinimum.Text = txt40FeetMinimum.Text = txtLCLMinimum.Text = txtairminimum.Text = "";
            txt20feetVariable.Text = txt40FeetVariable.Text = txtLCLVariable.Text = txtairVariable.Text = "";
            txt20feetMaximum.Text = txt40FeetMaximum.Text = txtLCLMaximum.Text = txtairMaximum.Text = "";
            txt20feetFixed.Text = txt40FeetFixed.Text = txtLCLFixed.Text = txtairFixed.Text = "";
           
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            clear();
            btnSave.Visible = false;
            btnUpdate.Visible = false;
        }
        private string frmdatesplit(string frmdate)
        {
            string[] frmdate1 = frmdate.Split('/');
            string frmdate2 = frmdate1[1] + '/' + frmdate1[0] + '/' + frmdate1[2];
            return frmdate2;
        }

        //protected void ddl20FeetCharges_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    string chargedesc = ddl20FeetCharges.SelectedValue;
        //    ddl40FeetCharges.SelectedValue = chargedesc;
        //    ddlLCLCharges.SelectedValue = chargedesc;
        //    ddlAIRCharges.SelectedValue = chargedesc;
        //    GridLoadSR();
        //}

        //protected void btnSave_Click(object sender, EventArgs e)
        //{
        //    if ((ddlAirUnit.SelectedItem.Text != "~Select~" || txtairFixed.Text != "") || (ddlLCLUnit.SelectedItem.Text != "~Select~" || txtLCLFixed.Text != "") || (ddl20FeetUnit.SelectedItem.Text != "~Select~" || txt20feetFixed.Text != "") || (ddl40feetUnit.SelectedItem.Text != "~Select~" || txt40FeetFixed.Text != ""))
        //    {
        //        string date = DateTime.Now.ToString("dd/MM/yyyy");
        //        SqlConnection sqlConn = new SqlConnection(con);
        //        sqlConn.Open();
        //        string qry1 = "Select ID,Description,Type,Unit,ActualRate,MinRate,VarRate,MaxRate,FixRate,ShipMode from M_StandardRate where Description='" + ddl20FeetCharges.SelectedValue + "' and ShipMode ='" + ddlShipModesir.SelectedItem.Text + "'";
        //        SqlDataAdapter sa = new SqlDataAdapter(qry1, con);
        //        DataSet ds2 = new DataSet();
        //        sa.Fill(ds2, "data");

        //        if (ds2.Tables["data"].Rows.Count == 0)
        //        {
        //            string vartypeAir="";
        //            string vartypeLCL="";
        //            string vartype20="";
        //            string vartype40="";
        //            if (ddlVarTypeair.SelectedValue == "~Select~")
        //            {
        //                vartypeAir = "";
        //            }
        //            else
        //            {
        //                vartypeAir = ddlVarTypeair.SelectedValue;
        //            }
        //            if (ddlVarTypelcl.SelectedValue == "~Select~")
        //            {
        //                vartypeLCL = "";
        //            }
        //            else
        //            {
        //                vartypeLCL = ddlVarTypelcl.SelectedValue;
        //            }
        //            if (ddlVarType20.SelectedValue == "~Select~")
        //            {
        //                vartype20 = "";
        //            }
        //            else
        //            {
        //                vartype20 = ddlVarType20.SelectedValue;
        //            }
        //            if (ddlVarType40.SelectedValue == "~Select~")
        //            {
        //                vartype40 = "";
        //            }
        //            else
        //            {
        //                vartype40 = ddlVarType40.SelectedValue;
        //            }

        //            string insert20feet = "Insert into M_StandardRate(ShipMode,Description,Type,Unit,ActualRate,MinRate,VarRate,MaxRate,FixRate,CreatedBy,CreatedDate,VarType)" +
        //                                 "values('" + ddlShipMode20.SelectedValue + "','" + ddl20FeetCharges.SelectedValue + "','20Feet','" + ddl20FeetUnit.SelectedValue + "','" + txt20FeetAtActual.Text + "','" + txt20feetMinimum.Text + "','" + txt20feetVariable.Text + "','" + txt20feetMaximum.Text + "','" + txt20feetFixed.Text + "','" + (string)Session["USER-NAME"] + "','" + frmdatesplit(date) + "','" + vartype20+ "')";
        //            string insert40feet = "Insert into M_StandardRate(ShipMode,Description,Type,Unit,ActualRate,MinRate,VarRate,MaxRate,FixRate,CreatedBy,CreatedDate,VarType)" +
        //                                 "values('" + ddlShipMode40.SelectedValue + "','" + ddl40FeetCharges.SelectedValue + "','40Feet','" + ddl40feetUnit.SelectedValue + "','" + txt40FeetAtActual.Text + "','" + txt40FeetMinimum.Text + "','" + txt40FeetVariable.Text + "','" + txt40FeetMaximum.Text + "','" + txt40FeetFixed.Text + "','" + (string)Session["USER-NAME"] + "','" + frmdatesplit(date) + "','" + vartype40 + "')";
        //            string insertlcl = "Insert into M_StandardRate(ShipMode,Description,Type,Unit,ActualRate,MinRate,VarRate,MaxRate,FixRate,CreatedBy,CreatedDate,VarType)" +
        //                                 "values('" + ddlShipModelcl.SelectedValue + "','" + ddlLCLCharges.SelectedValue + "','LCL','" + ddlLCLUnit.SelectedValue + "','" + txtLCLAtActual.Text + "','" + txtLCLMinimum.Text + "','" + txtLCLVariable.Text + "','" + txtLCLMaximum.Text + "','" + txtLCLFixed.Text + "','" + (string)Session["USER-NAME"] + "','" + frmdatesplit(date) + "','" + vartypeLCL + "')";
        //            string insertair = "Insert into M_StandardRate(ShipMode,Description,Type,Unit,ActualRate,MinRate,VarRate,MaxRate,FixRate,CreatedBy,CreatedDate,VarType)" +
        //                                 "values('" + ddlShipModesir.SelectedValue + "','" + ddlAIRCharges.SelectedValue + "','AIR','" + ddlAirUnit.SelectedValue + "','" + txtairAtAcutal.Text + "','" + txtairminimum.Text + "','" + txtairVariable.Text + "','" + txtairMaximum.Text + "','" + txtairFixed.Text + "','" + (string)Session["USER-NAME"] + "','" + frmdatesplit(date) + "','" + vartypeAir + "')";

        //            SqlCommand cmd = new SqlCommand(insert20feet, sqlConn);
        //            SqlDataAdapter da = new SqlDataAdapter();
        //            cmd.ExecuteNonQuery();

        //            SqlCommand cmd1 = new SqlCommand(insert40feet, sqlConn);
        //            SqlDataAdapter da1 = new SqlDataAdapter();
        //            //da.SelectCommand = cmd1;
        //            cmd1.ExecuteNonQuery();

        //            SqlCommand cmd2 = new SqlCommand(insertlcl, sqlConn);
        //            SqlDataAdapter da2 = new SqlDataAdapter();
        //            //da.SelectCommand = cmd;
        //            cmd2.ExecuteNonQuery();

        //            SqlCommand cmd3 = new SqlCommand(insertair, sqlConn);
        //            SqlDataAdapter da3 = new SqlDataAdapter();
        //            //da.SelectCommand = cmd;
        //            cmd3.ExecuteNonQuery();

        //            sqlConn.Close();
        //            GridLoad();
        //            clear();
        //            btnSave.Visible = true;
        //            btnUpdate.Visible = false;
        //            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Successfully Saved');", true);
        //        }
        //        else
        //        {
        //            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Charge has been already saved ');", true);
        //        }
        //    }
        //    else
        //    {
        //        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Please Select Charges');", true);
        //    }
        //}

        //protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    Session["Type"] = "";
        //    string typecont=GvStandard.SelectedRow.Cells[4].Text;
        //    Session["Type"]=typecont;

        //    if (typecont == "20Feet")
        //    {
        //        ddlShipMode20.SelectedValue = GvStandard.SelectedRow.Cells[2].Text;
        //        ddl20FeetCharges.SelectedValue =GvStandard.SelectedRow.Cells[3].Text;
        //        ddl20FeetUnit.SelectedValue = GvStandard.SelectedRow.Cells[5].Text;
        //        txt20FeetAtActual.Text = GvStandard.SelectedRow.Cells[6].Text;
        //        txt20feetMinimum.Text = GvStandard.SelectedRow.Cells[7].Text;
        //        txt20feetVariable.Text =GvStandard.SelectedRow.Cells[8].Text;
        //        ddlVarType20.SelectedValue = GvStandard.SelectedRow.Cells[9].Text;
        //        txt20feetMaximum.Text = GvStandard.SelectedRow.Cells[10].Text;
        //        txt20feetFixed.Text = GvStandard.SelectedRow.Cells[11].Text;

        //        if (txt20feetFixed.Text == "&nbsp;")
        //        {
        //            txt20feetFixed.Text = "";
        //        }
        //        if (txt20feetMinimum.Text == "&nbsp;")
        //        {
        //            txt20feetMinimum.Text = "";
        //        }
        //        if (txt20feetVariable.Text == "&nbsp;")
        //        {
        //            txt20feetVariable.Text = "";
        //        }
        //        if (txt20feetMaximum.Text == "&nbsp;")
        //        {
        //            txt20feetMaximum.Text = "";
        //        }

        //        pnl20feet.Visible = true;
        //        pnl40feet.Visible = false;
        //        pnlair.Visible = false;
        //        pnllcl.Visible = false;
        //    }
        //    else if (typecont == "40Feet")
        //    {
        //        ddlShipMode40.SelectedValue = GvStandard.SelectedRow.Cells[2].Text;
        //        ddl40FeetCharges.SelectedValue = GvStandard.SelectedRow.Cells[3].Text;
        //        ddl40feetUnit.SelectedValue = GvStandard.SelectedRow.Cells[5].Text;
        //        txt40FeetAtActual.Text = GvStandard.SelectedRow.Cells[6].Text;
        //        txt40FeetMinimum.Text = GvStandard.SelectedRow.Cells[7].Text;
        //        txt40FeetVariable.Text = GvStandard.SelectedRow.Cells[8].Text;
        //        ddlVarType40.SelectedValue = GvStandard.SelectedRow.Cells[9].Text;
        //        txt40FeetMaximum.Text = GvStandard.SelectedRow.Cells[10].Text;
        //        txt40FeetFixed.Text = GvStandard.SelectedRow.Cells[11].Text;
        //        pnl20feet.Visible = false;
        //        pnl40feet.Visible = true;
        //        pnlair.Visible = false;
        //        pnllcl.Visible = false;

        //        if (txt40FeetFixed.Text == "&nbsp;")
        //        {
        //            txt40FeetFixed.Text = "";
        //        }
        //        if (txt40FeetMinimum.Text == "&nbsp;")
        //        {
        //            txt40FeetMinimum.Text = "";
        //        }
        //        if (txt40FeetVariable.Text == "&nbsp;")
        //        {
        //            txt40FeetVariable.Text = "";
        //        }
        //        if (txt40FeetMaximum.Text == "&nbsp;")
        //        {
        //            txt40FeetMaximum.Text = "";
        //        }

        //    }
        //    else if (typecont == "LCL")
        //    {
        //        ddlShipModelcl.SelectedValue = GvStandard.SelectedRow.Cells[2].Text;
        //        ddlLCLCharges.SelectedValue = GvStandard.SelectedRow.Cells[3].Text;
        //        ddlLCLUnit.SelectedValue = GvStandard.SelectedRow.Cells[5].Text;
        //        txtLCLAtActual.Text = GvStandard.SelectedRow.Cells[6].Text;
        //        txtLCLMinimum.Text = GvStandard.SelectedRow.Cells[7].Text;
        //        txtLCLVariable.Text = GvStandard.SelectedRow.Cells[8].Text;
        //        ddlVarTypelcl.SelectedValue = GvStandard.SelectedRow.Cells[9].Text;
        //        txtLCLMaximum.Text = GvStandard.SelectedRow.Cells[10].Text;
        //        txtLCLFixed.Text = GvStandard.SelectedRow.Cells[11].Text;
        //        pnl20feet.Visible = false;
        //        pnl40feet.Visible = false;
        //        pnlair.Visible = false;
        //        pnllcl.Visible = true;

        //        if (txtLCLFixed.Text == "&nbsp;")
        //        {
        //            txtLCLFixed.Text = "";
        //        }
        //        if (txtLCLMinimum.Text == "&nbsp;")
        //        {
        //            txtLCLMinimum.Text = "";
        //        }
        //        if (txtLCLVariable.Text == "&nbsp;")
        //        {
        //            txtLCLVariable.Text = "";
        //        }
        //        if (txtLCLMaximum.Text == "&nbsp;")
        //        {
        //            txtLCLMaximum.Text = "";
        //        }

        //    }
        //    else if (typecont == "AIR")
        //    {
        //        ddlShipModesir.SelectedValue = GvStandard.SelectedRow.Cells[2].Text;
        //        ddlAIRCharges.SelectedValue = GvStandard.SelectedRow.Cells[3].Text;
        //        ddlAirUnit.SelectedValue = GvStandard.SelectedRow.Cells[5].Text;
        //        txtairAtAcutal.Text = GvStandard.SelectedRow.Cells[6].Text;
        //        txtairminimum.Text = GvStandard.SelectedRow.Cells[7].Text;
        //        txtairVariable.Text = GvStandard.SelectedRow.Cells[8].Text;
        //        ddlVarTypeair.SelectedValue = GvStandard.SelectedRow.Cells[9].Text;
        //        txtairMaximum.Text = GvStandard.SelectedRow.Cells[10].Text;
        //        txtairFixed.Text = GvStandard.SelectedRow.Cells[11].Text;
        //        pnl20feet.Visible = false;
        //        pnl40feet.Visible = false;
        //        pnlair.Visible = true;
        //        pnllcl.Visible = false;

        //        if (txtairFixed.Text == "&nbsp;")
        //        {
        //            txtairFixed.Text = "";
        //        }
        //        if (txtairminimum.Text == "&nbsp;")
        //        {
        //            txtairminimum.Text = "";
        //        }
        //        if (txtairVariable.Text == "&nbsp;")
        //        {
        //            txtairVariable.Text = "";
        //        }
        //        if (txtairMaximum.Text == "&nbsp;")
        //        {
        //            txtairMaximum.Text = "";
        //        }

        //    }
        //    btnSave.Visible = false;
        //    btnUpdate.Visible = true;           
        //}

        //protected void btnUpdate_Click(object sender, EventArgs e)
        //{
        //    string date = DateTime.Now.ToString("dd/MM/yyyy");
        //    SqlConnection sqlConn = new SqlConnection(con);
        //    sqlConn.Open();
        //    Session["Id"] = GvStandard.SelectedRow.Cells[1].Text;
        //    string query = "";
                 
        //    if ((string)Session["Type"] == "20Feet")
        //    {
        //        query = "Update M_StandardRate set ShipMode='" + ddlShipMode20.SelectedValue + "',Description='" + ddl20FeetCharges.SelectedValue + "',Type='20Feet',Unit='" + ddl20FeetUnit.SelectedValue + "',ActualRate='" + txt20FeetAtActual.Text + "',MinRate='" + txt20feetMinimum.Text + "',VarRate='" + txt20feetVariable.Text + "',MaxRate='" + txt20feetMaximum.Text + "',FixRate='" + txt20feetFixed.Text + "',CreatedBy='" + (string)Session["USER-NAME"] + "',CreatedDate='" + frmdatesplit(date) + "',VarType='" + ddlVarType20.SelectedValue + "' where ID='" + (String)Session["Id"] + "' ";
        //    }
        //    else if ((string)Session["Type"] == "40Feet")
        //    {
        //        query = "Update M_StandardRate set ShipMode='" + ddlShipMode20.SelectedValue + "',Description='" + ddl40FeetCharges.SelectedValue + "',Type='40Feet',Unit='" + ddl40feetUnit.SelectedValue + "',ActualRate='" + txt40FeetAtActual.Text + "',MinRate='" + txt40FeetMinimum.Text + "',VarRate='" + txt40FeetVariable.Text + "',MaxRate='" + txt40FeetMaximum.Text + "',FixRate='" + txt40FeetFixed.Text + "',CreatedBy='" + (string)Session["USER-NAME"] + "',CreatedDate='" + frmdatesplit(date) + "',VarType='" + ddlVarType40.SelectedValue + "' where ID='" + (String)Session["Id"] + "'";
        //    }
        //     else if ((string)Session["Type"] == "LCL")
        //    {
        //        query = "Update M_StandardRate set ShipMode='" + ddlShipMode20.SelectedValue + "',Description='" + ddlLCLCharges.SelectedValue + "',Type='LCL',Unit='" + ddlLCLUnit.SelectedValue + "',ActualRate='" + txtLCLAtActual.Text + "',MinRate='" + txtLCLMinimum.Text + "',VarRate='" + txtLCLVariable.Text + "',MaxRate='" + txtLCLMaximum.Text + "',FixRate='" + txtLCLFixed.Text + "',CreatedBy='" + (string)Session["USER-NAME"] + "',CreatedDate='" + frmdatesplit(date) + "',VarType='" + ddlVarTypelcl.SelectedValue + "' where Id='" + (String)Session["Id"] + "'";
        //    }
        //    else if ((string)Session["Type"] == "AIR")
        //    {
        //        query = "Update M_StandardRate set ShipMode='" + ddlShipMode20.SelectedValue + "',Description='" + ddlAIRCharges.SelectedValue + "',Type='AIR',Unit='" + ddlAirUnit.SelectedValue + "',ActualRate='" + txtairAtAcutal.Text + "',MinRate='" + txtairminimum.Text + "',VarRate='" + txtairVariable.Text + "',MaxRate='" + txtairMaximum.Text + "',FixRate='" + txtairFixed.Text + "',CreatedBy='" + (string)Session["USER-NAME"] + "',CreatedDate='" + frmdatesplit(date) + "',VarType='" + ddlVarTypeair.SelectedValue + "' where Id='" + (String)Session["Id"] + "'";
        //    }
                     
        //    SqlCommand cmd = new SqlCommand(query, sqlConn);           
        //    cmd.ExecuteNonQuery();


        //    sqlConn.Close();
        //    GridLoad();
        //    clear();
        //    btnSave.Visible = false;
        //    btnUpdate.Visible = true;
        //    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Successfully Updated');", true);
        //}

        protected void ddlAirUnit_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlAirUnit.SelectedValue == "At Actual")
            {
                txtairFixed.Enabled = false;
                txtairminimum.Enabled = false;
                txtairVariable.Enabled = false;
                txtairMaximum.Enabled = false;
                //ddlVarTypeair.Enabled = false;
                //txtairAtAcutal.Text = "At Actual";
            }
            else
            {
                //txtairAtAcutal.Text = "~Select~";
                txtairFixed.Enabled = true;
                txtairminimum.Enabled = true;
                txtairVariable.Enabled = true;
                txtairMaximum.Enabled = true;
                //ddlVarTypeair.Enabled = true;
            }
        }

        protected void txtairFixed_TextChanged(object sender, EventArgs e)
        {
            if (txtairFixed.Text != "")
            {
                txtairminimum.Enabled = false;
                txtairVariable.Enabled = false;
                txtairMaximum.Enabled = false;
                //ddlVarTypeair.Enabled = false;
                txtairminimum.Text = "";
                txtairVariable.Text = "";
                txtairMaximum.Text = "";
                //ddlVarTypeair.SelectedItem.Text = "~Select~";

            }
            else
            {
                txtairminimum.Enabled = true;
                txtairVariable.Enabled = true;
                txtairMaximum.Enabled = true;
                //ddlVarTypeair.Enabled = true;
            }
        }

        protected void txtairminimum_TextChanged(object sender, EventArgs e)
        {
            if (txtairminimum.Text != "")
            {
                txtairFixed.Enabled = false;
            }
            else
            {
                txtairFixed.Enabled = true;
            }
            
        }

        protected void ddlLCLUnit_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlLCLUnit.SelectedValue == "At Actual")
            {
                txtLCLFixed.Enabled = false;
                txtLCLMinimum.Enabled = false;
                txtLCLVariable.Enabled = false;
                txtLCLMaximum.Enabled = false;
                //ddlVarTypelcl.Enabled = false;
                //txtLCLAtActual.Text = "At Actual";
            }
            else
            {
                //txtLCLAtActual.Text = "~Select~";
                txtLCLFixed.Enabled = true;
                txtLCLMinimum.Enabled = true;
                txtLCLVariable.Enabled = true;
                txtLCLMaximum.Enabled = true;
               // ddlVarTypelcl.Enabled = true;
            }
        }

        protected void txtLCLFixed_TextChanged(object sender, EventArgs e)
        {

            if (txtLCLFixed.Text != "")
            {
                txtLCLMinimum.Enabled = false;
                txtLCLVariable.Enabled = false;
                txtLCLMaximum.Enabled = false;
                //ddlVarTypelcl.Enabled = false;
                txtLCLMinimum.Text = "";
                txtLCLVariable.Text = "";
                txtLCLMaximum.Text = "";
               // ddlVarTypelcl.SelectedItem.Text = "~Select~";

            }
            else
            {
                txtLCLMinimum.Enabled = true;
                txtLCLVariable.Enabled = true;
                txtLCLMaximum.Enabled = true;
                //ddlVarTypelcl.Enabled = true;
            }
        }

        protected void txtLCLMinimum_TextChanged(object sender, EventArgs e)
        {
            if (txtLCLMinimum.Text != "")
            {
                txtLCLFixed.Enabled = false;
            }
            else
            {
                txtLCLFixed.Enabled = true;
            }
            
        }

        protected void ddl20FeetUnit_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddl20FeetUnit.SelectedValue == "At Actual")
            {
                txt20feetFixed.Enabled = false;
                txt20feetMinimum.Enabled = false;
                txt20feetVariable.Enabled = false;
                txt20feetMaximum.Enabled = false;
                //ddlVarType20.Enabled = false;
                //txt20FeetAtActual.Text = "At Actual";
            }
            else
            {
                //txt20FeetAtActual.Text = "~Select~";
                txt20feetFixed.Enabled = true;
                txt20feetMinimum.Enabled = true;
                txt20feetVariable.Enabled = true;
                txt20feetMaximum.Enabled = true;
                //ddlVarType20.Enabled = true;
            }
        }

        protected void txt20feetFixed_TextChanged(object sender, EventArgs e)
        {

            if (txt20feetFixed.Text != "")
            {
                txt20feetMinimum.Enabled = false;
                txt20feetVariable.Enabled = false;
                txt20feetMaximum.Enabled = false;
                //ddlVarType20.Enabled = false;
                txt20feetMinimum.Text = "";
                txt20feetVariable.Text = "";
                txt20feetMaximum.Text = "";
                //ddlVarType20.SelectedItem.Text = "~Select~";

            }
            else
            {
                txt20feetMinimum.Enabled = true;
                txt20feetVariable.Enabled = true;
                txt20feetMaximum.Enabled = true;
                //ddlVarType20.Enabled = true;
            }
        }

        protected void txt20feetMinimum_TextChanged(object sender, EventArgs e)
        {
            if (txt20feetMinimum.Text != "")
            {
                txt20feetFixed.Enabled = false;
            }
            else
            {
                txt20feetFixed.Enabled = true;
            }
        }

        protected void ddl40feetUnit_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddl40feetUnit.SelectedValue == "At Actual")
            {
                txt40FeetFixed.Enabled = false;
                txt40FeetMinimum.Enabled = false;
                txt40FeetVariable.Enabled = false;
                txt40FeetMaximum.Enabled = false;
                //ddlVarType40.Enabled = false;
                //txt40FeetAtActual.Text = "At Actual";
            }
            else
            {
                //txt40FeetAtActual.Text = "~Select~";
                txt40FeetFixed.Enabled = true;
                txt40FeetMinimum.Enabled = true;
                txt40FeetVariable.Enabled = true;
                txt40FeetMaximum.Enabled = true;
                //ddlVarType40.Enabled = true;
            }
        }

        protected void txt40FeetFixed_TextChanged(object sender, EventArgs e)
        {

            if (txt40FeetFixed.Text != "")
            {
                txt40FeetMinimum.Enabled = false;
                txt40FeetVariable.Enabled = false;
                txt40FeetMaximum.Enabled = false;
               // ddlVarType40.Enabled = false;
                txt40FeetMinimum.Text = "";
                txt40FeetVariable.Text = "";
                txt40FeetMaximum.Text = "";
               // ddlVarType40.SelectedItem.Text = "~Select~";

            }
            else
            {
                txt40FeetMinimum.Enabled = true;
                txt40FeetVariable.Enabled = true;
                txt40FeetMaximum.Enabled = true;
                //ddlVarType40.Enabled = true;
            }
        }

        protected void txt40FeetMinimum_TextChanged(object sender, EventArgs e)
        {
            if (txt40FeetMinimum.Text != "")
            {
                txt40FeetFixed.Enabled = false;
            }
            else
            {
                txt40FeetFixed.Enabled = true;
            }
            
        }

        protected void ddlAIRCharges_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlAIRCharges.SelectedItem.Text == "~Select~")
            {
                //GridLoad();
                ddl40FeetCharges.SelectedValue = "~Select~";
                ddlLCLCharges.SelectedValue = "~Select~";
                ddl20FeetCharges.SelectedValue = "~Select~";
            }
            else
            { 
            string shipmode = ddlShipModesir.SelectedValue;
            string chargedesc = ddlAIRCharges.SelectedValue;
            ddl40FeetCharges.SelectedValue = chargedesc;
            ddlLCLCharges.SelectedValue = chargedesc;
            ddl20FeetCharges.SelectedValue = chargedesc;
            //GridLoadSR();
            ddlShipModelcl.SelectedValue = shipmode;
            ddlShipMode20.SelectedValue = shipmode;
            ddlShipMode40.SelectedValue = shipmode;
            }
        }

        //protected void GvStandard_PageIndexChanging(object sender, GridViewPageEventArgs e)
        //{
        //    DataSet ds = new DataSet();
        //    SqlConnection sqlcon = new SqlConnection(con);
        //    string query = "Select ID,ShipMode,Description,Type,Unit,ActualRate,MinRate,VarRate,VarType,MaxRate,FixRate from M_StandardRate";
        //    SqlDataAdapter da = new SqlDataAdapter(query, sqlcon);
        //    da.Fill(ds, "StandardRateDetails");
        //    GvStandard.DataSource = ds;
        //    GvStandard.DataBind();
        //    GvStandard.PageIndex = e.NewPageIndex;
        //    GvStandard.DataBind();
        //    sqlcon.Close();  
        //}

        protected void btnair_Click(object sender, EventArgs e)
        {
            if (ddlShipModesir.SelectedValue != "~Select~" && ddlAIRCharges.SelectedValue != "~Select~" && ddlAirUnit.SelectedValue != "~Select~")
            {
                string date = DateTime.Now.ToString("dd/MM/yyyy");
                SqlConnection sqlConn = new SqlConnection(con);
                sqlConn.Open();
                string qry1 = "Select ID,Description,Type,Unit,ActualRate,MinRate,VarRate,MaxRate,FixRate,ShipMode from M_StandardRate where Description='" + ddlAIRCharges.SelectedValue + "' and ShipMode ='" + ddlShipModesir.SelectedItem.Text + "' and Type='AIR'";
                SqlDataAdapter sa = new SqlDataAdapter(qry1, con);
                DataSet ds2 = new DataSet();
                sa.Fill(ds2, "data");

                if (ds2.Tables["data"].Rows.Count == 0)
                {
                    string vartypeAir = "";
                    if (ddlvarair.SelectedValue == "~Select~")
                    {
                        vartypeAir = "";
                    }
                    else
                    {
                        vartypeAir = ddlvarair.SelectedValue;
                    }

                    string insertair = "Insert into M_StandardRate(ShipMode,Description,Type,Unit,MinRate,VarRate,MaxRate,FixRate,CreatedBy,CreatedDate,VarType)" +
                                                 "values('" + ddlShipModesir.SelectedValue + "','" + ddlAIRCharges.SelectedValue + "','AIR','" + ddlAirUnit.SelectedValue + "','" + txtairminimum.Text + "','" + txtairVariable.Text + "','" + txtairMaximum.Text + "','" + txtairFixed.Text + "','" + (string)Session["USER-NAME"] + "','" + frmdatesplit(date) + "','" + vartypeAir + "')";//,ActualRate,'" + txtairAtAcutal.Text + "',
                    SqlCommand cmd3 = new SqlCommand(insertair, sqlConn);
                    SqlDataAdapter da3 = new SqlDataAdapter();
                    //da.SelectCommand = cmd;
                    cmd3.ExecuteNonQuery();
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Successfully Saved');", true);
                    FillGvAir();
                    clearAir();
                }
                else
                {
                    string vartypeAir = "";
                    if (ddlvarair.SelectedValue == "~Select~")
                    {
                        vartypeAir = "";
                    }
                    else
                    {
                        vartypeAir = ddlvarair.SelectedValue;
                    }

                    string query = "Update M_StandardRate set ShipMode='" + ddlShipModesir.SelectedValue + "',Description='" + ddlAIRCharges.SelectedValue + "',Type='AIR',Unit='" + ddlAirUnit.SelectedValue + "',MinRate='" + txtairminimum.Text + "',VarRate='" + txtairVariable.Text + "',MaxRate='" + txtairMaximum.Text + "',FixRate='" + txtairFixed.Text + "',CreatedBy='" + (string)Session["USER-NAME"] + "',CreatedDate='" + frmdatesplit(date) + "',VarType='" + vartypeAir + "' where (Type='AIR' and Description='" + ddlAIRCharges.SelectedValue + "' and ShipMode='" + ddlShipModesir.SelectedValue + "') or ID='" + Session["AirId"] + "'";   //Id='" + (String)Session["Id"] + "'

                    SqlCommand cmd = new SqlCommand(query, sqlConn);
                    cmd.ExecuteNonQuery();

                    sqlConn.Close();
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Successfully Updated');", true);
                    FillGvAir();
                    clearAir();
                }
            }
            else
            {
                string mssg="";
                if (ddlShipModesir.SelectedValue == "~Select~")
                {
                    mssg = "ShipMentMode";
                }
                else if (ddlAIRCharges.SelectedValue == "~Select~")
                {
                    mssg = "Description";
                }
                else
                {
                    mssg = "Unit";
                }
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Select " + mssg + "');", true);
            }
        }

        protected void btnlcl_Click(object sender, EventArgs e)
        {
            if (ddlShipModelcl.SelectedValue != "~Select~" && ddlLCLCharges.SelectedValue != "~Select~" && ddlLCLUnit.SelectedValue != "~Select~")
            {
                string date = DateTime.Now.ToString("dd/MM/yyyy");
                SqlConnection sqlConn = new SqlConnection(con);
                sqlConn.Open();
                string qry1 = "Select ID,Description,Type,Unit,ActualRate,MinRate,VarRate,MaxRate,FixRate,ShipMode from M_StandardRate where Description='" + ddlLCLCharges.SelectedValue + "' and ShipMode ='" + ddlShipModelcl.SelectedItem.Text + "' and Type='LCL'";
                SqlDataAdapter sa = new SqlDataAdapter(qry1, con);
                DataSet ds2 = new DataSet();
                sa.Fill(ds2, "data");

                if (ds2.Tables["data"].Rows.Count == 0)
                {
                    string vartypeLCL = "";
                    if (ddlvarlcl.SelectedValue == "~Select~")
                    {
                        vartypeLCL = "";
                    }
                    else
                    {
                        vartypeLCL = ddlvarlcl.SelectedValue;
                    }

                    string insertlcl = "Insert into M_StandardRate(ShipMode,Description,Type,Unit,MinRate,VarRate,MaxRate,FixRate,CreatedBy,CreatedDate,VarType)" +
                                             "values('" + ddlShipModelcl.SelectedValue + "','" + ddlLCLCharges.SelectedValue + "','LCL','" + ddlLCLUnit.SelectedValue + "','" + txtLCLMinimum.Text + "','" + txtLCLVariable.Text + "','" + txtLCLMaximum.Text + "','" + txtLCLFixed.Text + "','" + (string)Session["USER-NAME"] + "','" + frmdatesplit(date) + "','" + vartypeLCL + "')";
                    SqlCommand cmd3 = new SqlCommand(insertlcl, sqlConn);
                    SqlDataAdapter da3 = new SqlDataAdapter();
                    //da.SelectCommand = cmd;
                    cmd3.ExecuteNonQuery();
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Successfully saved');", true);
                    FillGvLcl();
                    clearLcl();
                }
                else
                {
                    string vartypeLCL = "";
                    if (ddlvarlcl.SelectedValue == "~Select~")
                    {
                        vartypeLCL = "";
                    }
                    else
                    {
                        vartypeLCL = ddlvarlcl.SelectedValue;
                    }

                    string query = "Update M_StandardRate set ShipMode='" + ddlShipModelcl.SelectedValue + "',Description='" + ddlLCLCharges.SelectedValue + "',Type='LCL',Unit='" + ddlLCLUnit.SelectedValue + "',MinRate='" + txtLCLMinimum.Text + "',VarRate='" + txtLCLVariable.Text + "',MaxRate='" + txtLCLMaximum.Text + "',FixRate='" + txtLCLFixed.Text + "',CreatedBy='" + (string)Session["USER-NAME"] + "',CreatedDate='" + frmdatesplit(date) + "',VarType='" + vartypeLCL + "' where (Type='LCL' and Description='" + ddlLCLCharges.SelectedValue + "' and ShipMode='" + ddlShipModelcl.SelectedValue + "') or ID='" + (String)Session["LclId"] + "'";

                    SqlCommand cmd = new SqlCommand(query, sqlConn);
                    cmd.ExecuteNonQuery();

                    sqlConn.Close();
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Successfully Updated');", true);
                    FillGvLcl();
                    clearLcl();
                }
            }
            else
            {
                string mssg = "";
                if (ddlShipModelcl.SelectedValue == "~Select~")
                {
                    mssg = "ShipMentMode";
                }
                else if (ddlLCLCharges.SelectedValue == "~Select~")
                {
                    mssg = "Description";
                }
                else
                {
                    mssg = "Unit";
                }

                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Select " + mssg + "');", true);
            }
        }

        protected void btnAdd20feet_Click(object sender, EventArgs e)
        {
            if (ddlShipMode20.SelectedValue != "~Select~" && ddl20FeetCharges.SelectedValue != "~Select~" && ddl20FeetUnit.SelectedValue != "~Select~")
            {
                string date = DateTime.Now.ToString("dd/MM/yyyy");
                SqlConnection sqlConn = new SqlConnection(con);
                sqlConn.Open();
                string qry1 = "Select ID,Description,Type,Unit,ActualRate,MinRate,VarRate,MaxRate,FixRate,ShipMode from M_StandardRate where Description='" + ddl20FeetCharges.SelectedValue + "' and ShipMode ='" + ddlShipMode20.SelectedItem.Text + "' and Type='20Feet'";
                SqlDataAdapter sa = new SqlDataAdapter(qry1, con);
                DataSet ds2 = new DataSet();
                sa.Fill(ds2, "data");

                if (ds2.Tables["data"].Rows.Count == 0)
                {
                    string vartype20 = "";
                    if (ddlvar20.SelectedValue == "~Select~")
                    {
                        vartype20 = "";
                    }
                    else
                    {
                        vartype20 = ddlvar20.SelectedValue;
                    }

                    string insert20feet = "Insert into M_StandardRate(ShipMode,Description,Type,Unit,MinRate,VarRate,MaxRate,FixRate,CreatedBy,CreatedDate,VarType)" +
                                                 "values('" + ddlShipMode20.SelectedValue + "','" + ddl20FeetCharges.SelectedValue + "','20Feet','" + ddl20FeetUnit.SelectedValue + "','" + txt20feetMinimum.Text + "','" + txt20feetVariable.Text + "','" + txt20feetMaximum.Text + "','" + txt20feetFixed.Text + "','" + (string)Session["USER-NAME"] + "','" + frmdatesplit(date) + "','" + vartype20 + "')";
                    SqlCommand cmd3 = new SqlCommand(insert20feet, sqlConn);
                    SqlDataAdapter da3 = new SqlDataAdapter();
                    //da.SelectCommand = cmd;
                    cmd3.ExecuteNonQuery();
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Successfully Saved');", true);
                    FillGv20feet();
                    clear20Fe();
                }
                else
                {
                    string vartype20 = "";
                    if (ddlvar20.SelectedValue == "~Select~")
                    {
                        vartype20 = "";
                    }
                    else
                    {
                        vartype20 = ddlvar20.SelectedValue;
                    }

                    string query = "Update M_StandardRate set ShipMode='" + ddlShipMode20.SelectedValue + "',Description='" + ddl20FeetCharges.SelectedValue + "',Type='20Feet',Unit='" + ddl20FeetUnit.SelectedValue + "',MinRate='" + txt20feetMinimum.Text + "',VarRate='" + txt20feetVariable.Text + "',MaxRate='" + txt20feetMaximum.Text + "',FixRate='" + txt20feetFixed.Text + "',CreatedBy='" + (string)Session["USER-NAME"] + "',CreatedDate='" + frmdatesplit(date) + "',VarType='" + vartype20 + "' where (Type='20Feet' and Description='" + ddl20FeetCharges.SelectedValue + "' and ShipMode='" + ddlShipMode20.SelectedValue + "') or ID='" + (String)Session["Fe20Id"] + "' ";

                    SqlCommand cmd = new SqlCommand(query, sqlConn);
                    cmd.ExecuteNonQuery();

                    sqlConn.Close();
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Successfully Updated');", true);
                    FillGv20feet();
                    clear20Fe();
                }
            }
            else
            {
                string mssg = "";
                if (ddlShipMode20.SelectedValue == "~Select~")
                {
                    mssg = "ShipMentMode";
                }
                else if (ddl20FeetCharges.SelectedValue == "~Select~")
                {
                    mssg = "Description";
                }
                else
                {
                    mssg = "Unit";
                }

                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Select " + mssg + "');", true);
            }

        }

        protected void btnAdd40feet_Click(object sender, EventArgs e)
        {
            if (ddlShipMode40.SelectedValue != "~Select~" && ddl40FeetCharges.SelectedValue != "~Select~" && ddl40feetUnit.SelectedValue != "~Select~")
            {
                string date = DateTime.Now.ToString("dd/MM/yyyy");
                SqlConnection sqlConn = new SqlConnection(con);
                sqlConn.Open();
                string qry1 = "Select ID,Description,Type,Unit,ActualRate,MinRate,VarRate,MaxRate,FixRate,ShipMode from M_StandardRate where Description='" + ddl40FeetCharges.SelectedValue + "' and ShipMode ='" + ddlShipMode40.SelectedItem.Text + "' and Type='40Feet'";
                SqlDataAdapter sa = new SqlDataAdapter(qry1, con);
                DataSet ds2 = new DataSet();
                sa.Fill(ds2, "data");

                if (ds2.Tables["data"].Rows.Count == 0)
                {
                    string vartype40 = "";
                    if (ddlvar40.SelectedValue == "~Select~")
                    {
                        vartype40 = "";
                    }
                    else
                    {
                        vartype40 = ddlvar40.SelectedValue;
                    }

                    string insert40feet = "Insert into M_StandardRate(ShipMode,Description,Type,Unit,MinRate,VarRate,MaxRate,FixRate,CreatedBy,CreatedDate,VarType)" +
                                             "values('" + ddlShipMode40.SelectedValue + "','" + ddl40FeetCharges.SelectedValue + "','40Feet','" + ddl40feetUnit.SelectedValue + "','" + txt40FeetMinimum.Text + "','" + txt40FeetVariable.Text + "','" + txt40FeetMaximum.Text + "','" + txt40FeetFixed.Text + "','" + (string)Session["USER-NAME"] + "','" + frmdatesplit(date) + "','" + vartype40 + "')";
                    SqlCommand cmd3 = new SqlCommand(insert40feet, sqlConn);
                    SqlDataAdapter da3 = new SqlDataAdapter();
                    //da.SelectCommand = cmd;
                    cmd3.ExecuteNonQuery();
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Successfully Saved');", true);
                    FillGv40Feet();
                    clear40Fe();
                }
                else
                {
                    string vartype40 = "";
                    if (ddlvar40.SelectedValue == "~Select~")
                    {
                        vartype40 = "";
                    }
                    else
                    {
                        vartype40 = ddlvar40.SelectedValue;
                    }

                    string query = "Update M_StandardRate set ShipMode='" + ddlShipMode40.SelectedValue + "',Description='" + ddl40FeetCharges.SelectedValue + "',Type='40Feet',Unit='" + ddl40feetUnit.SelectedValue + "',MinRate='" + txt40FeetMinimum.Text + "',VarRate='" + txt40FeetVariable.Text + "',MaxRate='" + txt40FeetMaximum.Text + "',FixRate='" + txt40FeetFixed.Text + "',CreatedBy='" + (string)Session["USER-NAME"] + "',CreatedDate='" + frmdatesplit(date) + "',VarType='" + vartype40 + "' where (Type='40Feet' and Description='" + ddl40FeetCharges.SelectedValue + "' and ShipMode='" + ddlShipMode40.SelectedValue + "') or ID='" + (String)Session["Fe40Id"] + "'";

                    SqlCommand cmd = new SqlCommand(query, sqlConn);
                    cmd.ExecuteNonQuery();

                    sqlConn.Close();
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Successfully Updated');", true);
                    FillGv40Feet();
                    clear40Fe();
                }
            }
            else
            {
                string mssg = "";
                if (ddlShipMode40.SelectedValue == "~Select~")
                {
                    mssg = "ShipMentMode";
                }
                else if (ddl40FeetCharges.SelectedValue == "~Select~")
                {
                    mssg = "Description";
                }
                else
                {
                    mssg = "Unit";
                }

                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Select " + mssg + "');", true);
            }
        }

        public void FillGvAir()
        {
            SqlConnection sqlConn = new SqlConnection(con);
            sqlConn.Open();
            string qry1 = "Select ID,Description,Type,Unit,FixRate,MinRate,VarRate,VarType,MaxRate,ShipMode from M_StandardRate where Type='AIR'";
            SqlDataAdapter sa = new SqlDataAdapter(qry1, con);
            DataSet ds2 = new DataSet();
            sa.Fill(ds2, "data");

            GvAir.DataSource = ds2;
            GvAir.DataBind();
        }
        public void FillGvLcl()
        {
            SqlConnection sqlConn = new SqlConnection(con);
            sqlConn.Open();
            string qry1 = "Select ID,Description,Type,Unit,FixRate,MinRate,VarRate,VarType,MaxRate,ShipMode from M_StandardRate where Type='LCL'";
            SqlDataAdapter sa = new SqlDataAdapter(qry1, con);
            DataSet ds2 = new DataSet();
            sa.Fill(ds2, "data");

            GvLcl.DataSource = ds2;
            GvLcl.DataBind();
        }
        public void FillGv20feet()
        {
            SqlConnection sqlConn = new SqlConnection(con);
            sqlConn.Open();
            string qry1 = "Select ID,Description,Type,Unit,FixRate,MinRate,VarRate,VarType,MaxRate,ShipMode from M_StandardRate where Type='20Feet'";
            SqlDataAdapter sa = new SqlDataAdapter(qry1, con);
            DataSet ds2 = new DataSet();
            sa.Fill(ds2, "data");

            Gv20feet.DataSource = ds2;
            Gv20feet.DataBind();
        }
        public void FillGv40Feet()
        {
            SqlConnection sqlConn = new SqlConnection(con);
            sqlConn.Open();
            string qry1 = "Select ID,Description,Type,Unit,FixRate,MinRate,VarRate,VarType,MaxRate,ShipMode from M_StandardRate where Type='40Feet'";
            SqlDataAdapter sa = new SqlDataAdapter(qry1, con);
            DataSet ds2 = new DataSet();
            sa.Fill(ds2, "data");

            Gv40Feet.DataSource = ds2;
            Gv40Feet.DataBind();
        }

        protected void GvAir_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadDescript();
            GridViewRow row = GvAir.SelectedRow;

            Label rrdp = (Label)row.FindControl("lblId");
            TextBox mod = (TextBox)row.FindControl("ddlAirmode");
            TextBox charge = (TextBox)row.FindControl("txtDescrip");
            TextBox uni = (TextBox)row.FindControl("txtUni");
            TextBox Fixrat = (TextBox)row.FindControl("txtFixRate");
            TextBox minrat = (TextBox)row.FindControl("txtMinRate");
            TextBox maxxrat = (TextBox)row.FindControl("txtMaxRate");
            TextBox airvar = (TextBox)row.FindControl("txtVarRate");
            TextBox airvartyp = (TextBox)row.FindControl("txtvartype");

            string airtyp = "";
            if (airvartyp.Text.ToString() == "")
            {
                airtyp = "~Select~";
            }
            else
            {
                airtyp = airvartyp.Text.ToString();
            }
            Session["AirId"] = rrdp.Text.ToString();
            ddlShipModesir.SelectedValue = mod.Text.ToString();
            ddlAIRCharges.SelectedValue = charge.Text.ToString();
            ddlAirUnit.SelectedValue = uni.Text.ToString();
            txtairFixed.Text = Fixrat.Text.ToString();
            txtairminimum.Text = minrat.Text.ToString();
            txtairMaximum.Text = maxxrat.Text.ToString();
            txtairVariable.Text = airvar.Text.ToString();
            ddlvarair.SelectedValue = airtyp;
        }

        protected void GvLcl_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow row = GvLcl.SelectedRow;

            Label rrdp = (Label)row.FindControl("lblId");
            TextBox mod = (TextBox)row.FindControl("ddllclmode");
            TextBox charge = (TextBox)row.FindControl("txtDescrip");
            TextBox uni = (TextBox)row.FindControl("txtUni");
            TextBox Fixrat = (TextBox)row.FindControl("txtFixRate");
            TextBox minrat = (TextBox)row.FindControl("txtMinRate");
            TextBox maxxrat = (TextBox)row.FindControl("txtMaxRate");
            TextBox airvar = (TextBox)row.FindControl("txtVarRate");
            TextBox airvartyp = (TextBox)row.FindControl("txtvartype");

            string lcltyp = "";
            if (airvartyp.Text.ToString() == "")
            {
                lcltyp = "~Select~";
            }
            else
            {
                lcltyp = airvartyp.Text.ToString();
            }

            Session["LclId"] = rrdp.Text.ToString();
            ddlShipModelcl.SelectedValue = mod.Text.ToString();
            ddlLCLCharges.SelectedValue = charge.Text.ToString();
            ddlLCLUnit.SelectedValue = uni.Text.ToString();
            txtLCLFixed.Text = Fixrat.Text.ToString();
            txtLCLMinimum.Text = minrat.Text.ToString();
            txtLCLMaximum.Text = maxxrat.Text.ToString();
            txtLCLVariable.Text = airvar.Text.ToString();
            ddlvarlcl.SelectedValue = lcltyp;
        }

        protected void Gv20feet_SelectedIndexChanged(object sender, EventArgs e)
        {
            GridViewRow row = Gv20feet.SelectedRow;

            Label rrdp = (Label)row.FindControl("lblId");
            TextBox mod = (TextBox)row.FindControl("ddl20femode");
            TextBox charge = (TextBox)row.FindControl("txtDescrip");
            TextBox uni = (TextBox)row.FindControl("txtUni");
            TextBox Fixrat = (TextBox)row.FindControl("txtFixRate");
            TextBox minrat = (TextBox)row.FindControl("txtMinRate");
            TextBox maxxrat = (TextBox)row.FindControl("txtMaxRate");
            TextBox airvar = (TextBox)row.FindControl("txtVarRate");
            TextBox airvartyp = (TextBox)row.FindControl("txtvartype");

            string Fe20typ = "";
            if (airvartyp.Text.ToString() == "")
            {
                Fe20typ = "~Select~";
            }
            else
            {
                Fe20typ = airvartyp.Text.ToString();
            }

            Session["Fe20Id"] = rrdp.Text.ToString();
            ddlShipMode20.SelectedValue = mod.Text.ToString();
            ddl20FeetCharges.SelectedValue = charge.Text.ToString();
            ddl20FeetUnit.SelectedValue = uni.Text.ToString();
            txt20feetFixed.Text = Fixrat.Text.ToString();
            txt20feetMinimum.Text = minrat.Text.ToString();
            txt20feetMaximum.Text = maxxrat.Text.ToString();
            txt20feetVariable.Text = airvar.Text.ToString();
            ddlvar20.SelectedValue = Fe20typ;
        }

        protected void Gv40Feet_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadDescript();
            GridViewRow row = Gv40Feet.SelectedRow;
           
            Label rrdp = (Label)row.FindControl("lblId");
            TextBox mod = (TextBox)row.FindControl("ddl40femode");
            TextBox charge = (TextBox)row.FindControl("txtDescrip");
            TextBox uni = (TextBox)row.FindControl("txtUni");
            TextBox Fixrat = (TextBox)row.FindControl("txtFixRate");
            TextBox minrat = (TextBox)row.FindControl("txtMinRate");
            TextBox maxxrat = (TextBox)row.FindControl("txtMaxRate");
            TextBox airvar = (TextBox)row.FindControl("txtVarRate");
            TextBox airvartyp = (TextBox)row.FindControl("txtvartype");

            string Fe40typ = "";
            if (airvartyp.Text.ToString() == "")
            {
                Fe40typ = "~Select~";
            }
            else
            {
                Fe40typ = airvartyp.Text.ToString();
            }


            Session["Fe40Id"] = rrdp.Text.ToString();
            ddlShipMode40.SelectedValue = mod.Text.ToString();
            ddl40FeetCharges.SelectedValue = charge.Text.ToString();
            ddl40feetUnit.SelectedValue = uni.Text.ToString();
            txt40FeetFixed.Text = Fixrat.Text.ToString();
            txt40FeetMinimum.Text = minrat.Text.ToString();
            txt40FeetMaximum.Text = maxxrat.Text.ToString();
            txt40FeetVariable.Text = airvar.Text.ToString();
            ddlvar40.SelectedValue = Fe40typ;
        }

        public void clearAir()
        {
            ddlShipModesir.SelectedValue = "~Select~";
            ddlAIRCharges.SelectedValue = "~Select~";
            ddlAirUnit.SelectedValue = "~Select~";
            txtairFixed.Text="";
            txtairminimum.Text="";
            txtairMaximum.Text="";
            txtairVariable.Text="";
            ddlvarair.SelectedValue = "~Select~";
        }
        public void clearLcl()
        {
            ddlShipModelcl.SelectedValue = "~Select~";
            ddlLCLCharges.SelectedValue = "~Select~";
            ddlLCLUnit.SelectedValue = "~Select~";
            txtLCLFixed.Text = "";
            txtLCLMinimum.Text = "";
            txtLCLMaximum.Text = "";
            txtLCLVariable.Text = "";
            ddlvarlcl.SelectedValue = "~Select~";
        }
        public void clear20Fe()
        {
            ddlShipMode20.SelectedValue = "~Select~";
            ddl20FeetCharges.SelectedValue = "~Select~";
            ddl20FeetUnit.SelectedValue = "~Select~";
            txt20feetFixed.Text = "";
            txt20feetMinimum.Text = "";
            txt20feetMaximum.Text = "";
            txt20feetVariable.Text = "";
            ddlvar20.SelectedValue = "~Select~";
        }
        public void clear40Fe()
        {
            ddlShipMode40.SelectedValue = "~Select~";
            ddl40FeetCharges.SelectedValue = "~Select~";
            ddl40feetUnit.SelectedValue = "~Select~";
            txt40FeetFixed.Text = "";
            txt40FeetMinimum.Text = "";
            txt40FeetMaximum.Text = "";
            txt40FeetVariable.Text = "";
            ddlvar40.Text = "~Select~";
        }
       
    }
}