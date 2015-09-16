using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using VTS.ImpexCube.Utlities;
using System.Text.RegularExpressions;

namespace ImpexCube.CRM
{
    public partial class frmQuote1 : System.Web.UI.Page
    {
        string con = (string)ConfigurationManager.ConnectionStrings["Constr"].ConnectionString;
        int i = 0;
        string keyname = "QtNo";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack == false)
            {
                string fyear = (string)Session["FYear"];
                Session["key"] = "";

                Session["key"] = keyname + "/" + fyear + "/" + Convert.ToString(Utility.GetNextAutoNo(keyname, 0, Utility.GetConnectionString()));
                Session["Keycode"] = Convert.ToString(Utility.GetNextAutoNo(keyname, 0, Utility.GetConnectionString()));
                lblQuoteNo.Text = (string)Session["key"];
                LoadDescript();
                LoadCustomer();
                //LoadGrid();
                btnSave.Visible = true;
                btnUpdate.Visible = false;
                pnlEnqDetails.Visible = false;
                pnlGridEnquiry.Visible = false;
                pnl20feet.Visible = false;
                pnl40feet.Visible = false;
                pnllcl.Visible = false;
                pnlair.Visible = false;
                pnlGridQuote.Visible = false;
                pnlButtons.Visible = false;
                pnlCustName.Visible = false;
                pnlMain.Visible = false;
                pnlRepTem.Visible = false;

                txt1.Text = "The above rate includes normal operational/handlink expenses involved till delivery.";
                txt2.Text = "Service tax will be extra, as applicable.";
                txt3.Text = "Halting charges at site, if any, will be extra.";
                txt4.Text = "The above rates are not aplicable for any other cargo/ SECOND HAND MACHINERY.";
                txt5.Text = "All statutory payments,i.e. Customs duty, Demurrage charges(if any), CFS charges, Port Charges & Transportation charges will be charged at actuals, supported by receipts, to be given in advance.";
                txt6.Text = "Payment: In 10 days from the date of submission of bills";

                
                string month = DateTime.Now.ToString("MMMM");
                string year = DateTime.Now.ToString("yyyy");
                txt7.Text = "Validity: End " + month + ","+year+".";
                LoadCustomer();
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

        public void LoadCustomer()
        {
            SqlConnection sqlcon = new SqlConnection(con);
            string query = "select CustomerName from M_Enquriy";
            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter(query, sqlcon);
            da.Fill(ds, "Customer");
            ddlCustNam.DataSource = ds;
            ddlCustNam.DataTextField = "CustomerName";
            ddlCustNam.DataValueField = "CustomerName";
            ddlCustNam.DataBind();

            //ddlCusName.DataSource = ds;
            //ddlCusName.DataTextField = "CustomerName";
            //ddlCusName.DataValueField = "CustomerName";
            //ddlCusName.DataBind();
            sqlcon.Close();

        }

        public void LoadGrid()
        {
            DataSet ds = new DataSet();
            SqlConnection sqlcon = new SqlConnection(con);
            string query = "Select EnqId,CustomerName,Commodity,Air,Feet20,Feet40,Lcl from View_QuoteDet";
            SqlDataAdapter da = new SqlDataAdapter(query, sqlcon);
            da.Fill(ds, "StandardRateDetails");
            if (ds.Tables["StandardRateDetails"].Rows.Count != 0)
            {
                GvQuote.DataSource = ds;
                GvQuote.DataBind();
            }
            sqlcon.Close();

        }

        protected void ddl20FeetCharges_SelectedIndexChanged(object sender, EventArgs e)
        {
            string chargedesc = ddl20FeetCharges.SelectedValue;
            ddl40FeetCharges.SelectedValue = chargedesc;
            ddlLCLCharges.SelectedValue = chargedesc;
            ddlAIRCharges.SelectedValue = chargedesc;
            SqlConnection sqlcon = new SqlConnection(con);
            sqlcon.Open();
            SqlDataAdapter sd = new SqlDataAdapter("Select ID,Description,Type,Unit,ActualRate,MinRate,VarRate,VarType,MaxRate,FixRate from M_StandardRate where Description='"+ddl20FeetCharges.SelectedValue+"' and Type='20Feet'", con);
            DataSet ds = new DataSet();
            sd.Fill(ds, "data");
            sqlcon.Close();
            if (ds.Tables["data"].Rows.Count != 0)
            {
                DataRowView row = ds.Tables["data"].DefaultView[0];
                ddl20FeetUnit.SelectedValue = row["Unit"].ToString();
                //txt20FeetAtActual.SelectedValue = row["ActualRate"].ToString();
                txt20feetMinimum.Text = row["MinRate"].ToString();
                txt20feetVariable.Text = row["VarRate"].ToString();
                ddlvar20.SelectedValue = row["VarType"].ToString();
                txt20feetMaximum.Text = row["MaxRate"].ToString();
                txt20feetFixed.Text = row["FixRate"].ToString();

            }
            SqlDataAdapter sd1 = new SqlDataAdapter("Select ID,Description,Type,Unit,ActualRate,MinRate,VarRate,VarType,MaxRate,FixRate from M_StandardRate where Description='" + ddl20FeetCharges.SelectedValue + "' and Type='40Feet'", con);
            DataSet ds1 = new DataSet();
            sd1.Fill(ds1, "data");
            sqlcon.Close();
            if (ds1.Tables["data"].Rows.Count != 0)
            {
                DataRowView row = ds1.Tables["data"].DefaultView[0];
                ddl40feetUnit.SelectedValue = row["Unit"].ToString();
                //txt40FeetAtActual.SelectedValue = row["ActualRate"].ToString();
                txt40FeetMinimum.Text = row["MinRate"].ToString();
                txt40FeetVariable.Text = row["VarRate"].ToString();
                ddlvar40.SelectedValue = row["VarType"].ToString();
                txt40FeetMaximum.Text = row["MaxRate"].ToString();
                txt40FeetFixed.Text = row["FixRate"].ToString();

            }
            SqlDataAdapter sd2 = new SqlDataAdapter("Select ID,Description,Type,Unit,ActualRate,MinRate,VarRate,VarType,MaxRate,FixRate from M_StandardRate where Description='" + ddl20FeetCharges.SelectedValue + "' and Type='LCL'", con);
            DataSet ds2 = new DataSet();
            sd2.Fill(ds2, "data");
            sqlcon.Close();
            if (ds2.Tables["data"].Rows.Count != 0)
            {
                DataRowView row = ds2.Tables["data"].DefaultView[0];
                ddlLCLUnit.SelectedValue = row["Unit"].ToString();
                //txtLCLAtActual.SelectedValue = row["ActualRate"].ToString();
                txtLCLMinimum.Text = row["MinRate"].ToString();
                txtLCLVariable.Text = row["VarRate"].ToString();
                ddlvarlcl.SelectedValue = row["VarType"].ToString();
                txtLCLMaximum.Text = row["MaxRate"].ToString();
                txtLCLFixed.Text = row["FixRate"].ToString();

            }
            SqlDataAdapter sd3 = new SqlDataAdapter("Select ID,Description,Type,Unit,ActualRate,MinRate,VarRate,VarType,MaxRate,FixRate from M_StandardRate where Description='" + ddl20FeetCharges.SelectedValue + "' and Type='AIR'", con);
            DataSet ds3 = new DataSet();
            sd3.Fill(ds3, "data");
            sqlcon.Close();
            if (ds3.Tables["data"].Rows.Count != 0)
            {
                DataRowView row = ds3.Tables["data"].DefaultView[0];
                ddlAirUnit.SelectedValue = row["Unit"].ToString();
                //txtairAtAcutal.SelectedValue = row["ActualRate"].ToString();
                txtairminimum.Text = row["MinRate"].ToString();
                txtairVariable.Text = row["VarRate"].ToString();
                ddlvarair.SelectedValue = row["VarType"].ToString();
                txtairMaximum.Text = row["MaxRate"].ToString();
                txtairFixed.Text = row["FixRate"].ToString();

            }
        }


        protected void btnSave_Click(object sender, EventArgs e)
        {
            Save();
            bool IsAllchecked = false;          

            foreach (GridViewRow row in Gv20feet.Rows)
            {
              
                CheckBox chkCharge20feet = (CheckBox)row.FindControl("chkCharge");
               
                if (chkCharge20feet.Checked)
                {
                    //TextBox typ = (TextBox)row.FindControl("txtTyp");
                    IsAllchecked = true;
                    string typ = "";
                    TextBox ddl20feetdesc = (TextBox)row.FindControl("txtDescrip");
                  //  TextBox ActRate = (TextBox)row.FindControl("ddlActualRate");
                    TextBox ddlunit = (TextBox)row.FindControl("txtUni");
                    TextBox MinRate = (TextBox)row.FindControl("txtMinRate");
                    TextBox VarRate = (TextBox)row.FindControl("txtVarRate");
                    //DropDownList ddlVartype = (DropDownList)row.FindControl("ddlVarType");
                    TextBox txtVartype = (TextBox)row.FindControl("txtVartype");
                    TextBox MaxRate = (TextBox)row.FindControl("txtMaxRate");
                    TextBox FixRate = (TextBox)row.FindControl("txtFixRate");
                    typ = "20Feet";
                    string test = MinRate.Text;
                    string date = DateTime.Now.ToString("dd/MM/yyyy");
                    SqlConnection sqlConn = new SqlConnection(con);
                    sqlConn.Open();

                    string qry1 = "Select ID,CustomerName,Description,Type,Unit,ActualRate,MinRate,VarRate,MaxRate,FixRate from M_Quote where Description='" + ddl20feetdesc.Text + "' and CustomerName='" + txtCustName.Text + "' and Type='" + typ + "'";
                SqlDataAdapter sa = new SqlDataAdapter(qry1, con);
                DataSet ds2 = new DataSet();
                sa.Fill(ds2, "data");

                if (ds2.Tables["data"].Rows.Count == 0)
                {
                    string insert20feet = "Insert into M_Quote(EnqId,CustomerName,Description,Type,Unit,MinRate,VarRate,VarType,MaxRate,FixRate,CreatedBy,CreatedDate,QuoteNo)" +
                                         "values('" + txtEnqId.Text + "','" + txtCustName.Text + "','" + ddl20feetdesc.Text + "','20Feet','" + ddlunit.Text + "','" + MinRate.Text + "','" + VarRate.Text + "','" + txtVartype.Text + "','" + MaxRate.Text + "','" + FixRate.Text + "','" + (string)Session["USER-NAME"] + "','" + frmdatesplit(date) + "','" + (String)Session["key"] + "')";  //,'" + reportfield + "'
                    SqlCommand cmd = new SqlCommand(insert20feet, sqlConn);
                    SqlDataAdapter da = new SqlDataAdapter();
                    cmd.ExecuteNonQuery();

                    string AddEnqCom = "Update M_Enquriy set QuoteCompleted='1' where TransId='" + txtEnqId.Text + "'";
                    SqlCommand cmd1 = new SqlCommand(AddEnqCom, sqlConn);
                    SqlDataAdapter da1 = new SqlDataAdapter();
                    cmd1.ExecuteNonQuery();
                    sqlConn.Close();
                }
                }
            }



            foreach (GridViewRow row in Gv40Feet.Rows)
        {            

            CheckBox chkCharge1 = (CheckBox)row.FindControl("chkCharge40feet");

            if (chkCharge1.Checked)
            {
                string typ = "";
                IsAllchecked = true;
                //TextBox typ = (TextBox)row.FindControl("txtTyp");
                TextBox ddl20feetdesc = (TextBox)row.FindControl("txtDescrip");
               // TextBox ActRate = (TextBox)row.FindControl("ddlActualRate");
                TextBox ddlunit = (TextBox)row.FindControl("txtUni");
                TextBox MinRate = (TextBox)row.FindControl("txtMinRate");
                TextBox VarRate = (TextBox)row.FindControl("txtVarRate");
                TextBox txtVartype = (TextBox)row.FindControl("txtVartype");
                TextBox MaxRate = (TextBox)row.FindControl("txtMaxRate");
                TextBox FixRate = (TextBox)row.FindControl("txtFixRate");
                typ = "40Feet";
            string date = DateTime.Now.ToString("dd/MM/yyyy");
            SqlConnection sqlConn = new SqlConnection(con);
            sqlConn.Open();
            string sdf = txtEnqId.Text;
            string qry1 = "Select ID,CustomerName,Description,Type,Unit,ActualRate,MinRate,VarRate,MaxRate,FixRate from M_Quote where Description='" + ddl20feetdesc.Text + "' and CustomerName='" + txtCustName.Text + "' and Type='" + typ + "' ";
            SqlDataAdapter sa = new SqlDataAdapter(qry1, con);
                DataSet ds2 = new DataSet();
                sa.Fill(ds2, "data");

                if (ds2.Tables["data"].Rows.Count == 0)
                {
                    string insert40feet = "Insert into M_Quote(EnqId,CustomerName,Description,Type,Unit,MinRate,VarRate,VarType,MaxRate,FixRate,CreatedBy,CreatedDate,QuoteNo)" +
                             "values('" + txtEnqId.Text + "','" + txtCustName.Text + "','" + ddl20feetdesc.Text + "','"+typ+"','" + ddlunit.Text + "','" + MinRate.Text + "', "+
                          " '" + VarRate.Text + "','" + txtVartype.Text + "','" + MaxRate.Text + "','" + FixRate.Text + "','" + (string)Session["USER-NAME"] + "','" + frmdatesplit(date) + "','" + (String)Session["key"] + "')";
                    SqlCommand cmd1 = new SqlCommand(insert40feet, sqlConn);
                    SqlDataAdapter da1 = new SqlDataAdapter();
                    cmd1.ExecuteNonQuery();

                    string AddEnqCom = "Update M_Enquriy set QuoteCompleted='1' where TransId='" + txtEnqId.Text + "'";
                    SqlCommand cmd2 = new SqlCommand(AddEnqCom, sqlConn);
                    SqlDataAdapter da2 = new SqlDataAdapter();
                    cmd2.ExecuteNonQuery();
                    sqlConn.Close();
                }
           }
            }



            foreach (GridViewRow row in GvLcl.Rows)
        {           

            CheckBox chkCharge2 = (CheckBox)row.FindControl("chkChargelcl");
            if (chkCharge2.Checked)
            {
                string typ = "";
                IsAllchecked = true;
                //TextBox typ = (TextBox)row.FindControl("txtTyp");
                TextBox ddl20feetdesc = (TextBox)row.FindControl("txtDescrip");
              //TextBox ActRate = (TextBox)row.FindControl("ddlActualRate");
                TextBox ddlunit = (TextBox)row.FindControl("txtUni");
                TextBox MinRate = (TextBox)row.FindControl("txtMinRate");
                TextBox VarRate = (TextBox)row.FindControl("txtVarRate");
                //DropDownList ddlVartype = (DropDownList)row.FindControl("ddlVarType");
                TextBox txtVartype = (TextBox)row.FindControl("txtVartype");
                TextBox MaxRate = (TextBox)row.FindControl("txtMaxRate");
                TextBox FixRate = (TextBox)row.FindControl("txtFixRate");
                typ = "LCL";
            string date = DateTime.Now.ToString("dd/MM/yyyy");
            SqlConnection sqlConn = new SqlConnection(con);
            sqlConn.Open();

            string qry1 = "Select ID,CustomerName,Description,Type,Unit,ActualRate,MinRate,VarRate,MaxRate,FixRate from M_Quote where Description='" + ddl20feetdesc.Text + "' and CustomerName='" + txtCustName.Text + "' and Type='" + typ + "'";
            SqlDataAdapter sa = new SqlDataAdapter(qry1, con);
                DataSet ds2 = new DataSet();
                sa.Fill(ds2, "data");

                if (ds2.Tables["data"].Rows.Count == 0)
                {
                    string insertlcl = "Insert into M_Quote(EnqId,CustomerName,Description,Type,Unit,MinRate,VarRate,VarType,MaxRate,FixRate,CreatedBy,CreatedDate,QuoteNo)" +
                                                 "values('" + txtEnqId.Text + "','" + txtCustName.Text + "','" + ddl20feetdesc.Text + "','LCL','" + ddlunit.Text + "','" + MinRate.Text + "','" + VarRate.Text + "','" + txtVartype.Text + "','" + MaxRate.Text + "','" + FixRate.Text + "','" + (string)Session["USER-NAME"] + "','" + frmdatesplit(date) + "','" + (String)Session["key"] + "')";
                    SqlCommand cmd2 = new SqlCommand(insertlcl, sqlConn);
                    SqlDataAdapter da2 = new SqlDataAdapter();
                    cmd2.ExecuteNonQuery();

                    string AddEnqCom = "Update M_Enquriy set QuoteCompleted='1' where TransId='" + txtEnqId.Text + "'";
                    SqlCommand cmd3 = new SqlCommand(AddEnqCom, sqlConn);
                    SqlDataAdapter da3 = new SqlDataAdapter();
                    cmd3.ExecuteNonQuery();
                    sqlConn.Close();
                }
           }
            }

            foreach (GridViewRow row in GvAir.Rows)
        {
            CheckBox chkCharges3 = (CheckBox)row.FindControl("chkChargeair");
            if (chkCharges3.Checked)
            {
                string typ = "";
                IsAllchecked = true;
             //   TextBox typ=(TextBox)row.FindControl("txtTyp");
                TextBox ddl20feetdesc = (TextBox)row.FindControl("txtDescrip");
                //TextBox ActRate = (TextBox)row.FindControl("ddlActualRate");
                TextBox ddlunit = (TextBox)row.FindControl("txtUni");
                TextBox MinRate = (TextBox)row.FindControl("txtMinRate");
                TextBox VarRate = (TextBox)row.FindControl("txtVarRate");
                //DropDownList ddlVartype = (DropDownList)row.FindControl("ddlVarType");
                TextBox txtVartype = (TextBox)row.FindControl("txtVartype");
                TextBox MaxRate = (TextBox)row.FindControl("txtMaxRate");
                TextBox FixRate = (TextBox)row.FindControl("txtFixRate");
            string date = DateTime.Now.ToString("dd/MM/yyyy");
            SqlConnection sqlConn = new SqlConnection(con);
            sqlConn.Open();
            typ = "AIR";
            string qry1 = "Select ID,CustomerName,Description,Type,Unit,ActualRate,MinRate,VarRate,MaxRate,FixRate from M_Quote where Description='" + ddl20feetdesc.Text + "' and CustomerName='" + txtCustName.Text + "' and Type='" + typ + "'";
            SqlDataAdapter sa = new SqlDataAdapter(qry1, con);
                DataSet ds2 = new DataSet();
                sa.Fill(ds2, "data");

                if (ds2.Tables["data"].Rows.Count == 0)
                {
                    string insertair = "Insert into M_Quote(EnqId,CustomerName,Description,Type,Unit,MinRate,VarRate,VarType,MaxRate,FixRate,CreatedBy,CreatedDate,QuoteNo)" +
                                                 "values('" + txtEnqId.Text + "','" + txtCustName.Text + "','" + ddl20feetdesc.Text + "','AIR','" + ddlunit.Text + "','" + MinRate.Text + "','" + VarRate.Text + "','" + txtVartype.Text + "','" + MaxRate.Text + "','" + FixRate.Text + "','" + (string)Session["USER-NAME"] + "','" + frmdatesplit(date) + "','"+(String)Session["key"]+"')";

                    SqlCommand cmd3 = new SqlCommand(insertair, sqlConn);
                    SqlDataAdapter da3 = new SqlDataAdapter();
                    cmd3.ExecuteNonQuery();

                    string AddEnqCom = "Update M_Enquriy set QuoteCompleted='1' where TransId='" + txtEnqId.Text + "'";
                    SqlCommand cmd4 = new SqlCommand(AddEnqCom, sqlConn);
                    SqlDataAdapter da4 = new SqlDataAdapter();
                    cmd4.ExecuteNonQuery();
                    sqlConn.Close();
                }
           }
                 }


            SaveTemplates();
            
           // LoadGrid();
            clear();
            btnSave.Visible = true;
            btnUpdate.Visible = false;            
            
            btnPrint.Visible = true;

            int Update = new int();
            SqlConnection conq = new SqlConnection(con);
            conq.Open();
            string queryQ = "Select * from M_Quote where QuoteNo='"+(String)Session["key"]+"'";
            SqlDataAdapter daq = new SqlDataAdapter(queryQ, con);
            DataSet dsq = new DataSet();
            daq.Fill(dsq, "data");
            conq.Close();
            if(dsq.Tables["data"].Rows.Count!=0)
            {
                
                string keycode = (string)Session["Keycode"];
                Update = Utility.UpdateAutoNo(keyname, Convert.ToInt32(keycode), Utility.GetConnectionString());
            }


            if (IsAllchecked == false)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Please Select any Charge');", true);
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Successfully Saved');", true);
            }
            //FillEnquiry();
        }
        public void SaveTemplates()
        {
            int i = 0;
            string text1 = "";
            if (chk1.Checked)
            {
                i = i + 1;
                text1 =i+"."+ txt1.Text + System.Environment.NewLine.ToString();
            }
            string text2 = "";
            if (chk2.Checked)
            {
                i = i + 1;
                text2 = i + "." + txt2.Text + System.Environment.NewLine;
            }
            string text3 = "";
            if (chk3.Checked)
            {
                i = i + 1;
                text3 = i + "." + txt3.Text + System.Environment.NewLine;
            }
            string text4 = "";
            if (chk4.Checked)
            {
                i = i + 1;
                text4 = i + "." + txt4.Text + System.Environment.NewLine;
            }
            string text5 = "";
            if (chk5.Checked)
            {
                i = i + 1;
                text5 = i + "." + txt5.Text + System.Environment.NewLine;
            }
            string text6 = "";
            if (chk6.Checked)
            {
                i = i + 1;
                text6 = i + "." + txt6.Text + System.Environment.NewLine;
            }
            string text7 = "";
            if (chk7.Checked)
            {
                i = i + 1;
                text7 = i + "." + txt7.Text + System.Environment.NewLine;
            }
            string text8 = "";
            if (txtRem.Text != "")
            {
                i = i + 1;
                text8 = i + "." + txtRem.Text;
            }

            string EnId = "";
            if (txtEnqId.Text == "")
            {
                EnId = (String)Session["EnId"];
            }
            else
            {
                EnId = txtEnqId.Text;
            }
            string CName="";
            if (txtCustName.Text == "")
            {
                CName = (String)Session["CusNam"];
            }
            else
            {
                CName = txtCustName.Text;
            }


            SqlConnection con1 = new SqlConnection(con);
            con1.Open();
            string qry = "Select CustName,EnqId,RepTemp,Remarks from M_QuoteReportTemp where EnqId='" + EnId + "'";
            SqlDataAdapter da = new SqlDataAdapter(qry, con1);
            DataSet ds = new DataSet();
            da.Fill(ds, "data");
            //con1.Close();
            string reportfield = text1 + text2 + text3 + text4 + text5 + text6 + text7 + text8;
          
            if (ds.Tables["data"].Rows.Count == 0)
            {
               
                //string qry1 = "Insert into M_QuoteReportTemp(CustName,EnqId,RepTemp)values('" + CName + "','" + EnId + "','" + text1 + "'+'" + text2 + "'+'" + text3 + "'+'" + text4 + "'+'" + text5 + "'+'" + text6 + "'+'" + text7 + "'+'" + txtRem.Text + "')";
                string qry1 = "Insert into M_QuoteReportTemp(CustName,EnqId,RepTemp,Remarks)values('" + CName + "','" + EnId + "','" + reportfield + "','" + txtRem.Text + "')";
                SqlCommand cmd = new SqlCommand(qry1, con1);
                cmd.ExecuteNonQuery();
                con1.Close();
            }
            else
            {
                string qry1 = "Update M_QuoteReportTemp set CustName='" + CName + "',EnqId='" + EnId + "',RepTemp='" + reportfield + "',Remarks='" + txtRem.Text + "' where EnqId='" + EnId + "'";
                SqlCommand cmd = new SqlCommand(qry1, con1);
                cmd.ExecuteNonQuery();
                con1.Close();
            }
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            string date = DateTime.Now.ToString("dd/MM/yyyy");
            //SqlConnection sqlConn = new SqlConnection(con);
            //sqlConn.Open();
            //Session["Id"] = GvQuote.SelectedRow.Cells[1].Text.ToString();

            //int i = 0;
            //string text1 = "";
            //if (chk1.Checked)
            //{
            //    i = i + 1;
            //    text1 = i + "." + txt1.Text + System.Environment.NewLine.ToString();
            //}
            //string text2 = "";
            //if (chk2.Checked)
            //{
            //    i = i + 1;
            //    text2 = i + "." + txt2.Text + System.Environment.NewLine;
            //}
            //string text3 = "";
            //if (chk3.Checked)
            //{
            //    i = i + 1;
            //    text3 = i + "." + txt3.Text + System.Environment.NewLine;
            //}
            //string text4 = "";
            //if (chk4.Checked)
            //{
            //    i = i + 1;
            //    text4 = i + "." + txt4.Text + System.Environment.NewLine;
            //}
            //string text5 = "";
            //if (chk5.Checked)
            //{
            //    i = i + 1;
            //    text5 = i + "." + txt5.Text + System.Environment.NewLine;
            //}
            //string text6 = "";
            //if (chk6.Checked)
            //{
            //    i = i + 1;
            //    text6 = i + "." + txt6.Text + System.Environment.NewLine;
            //}
            //string text7 = "";
            //if (chk7.Checked)
            //{
            //    i = i + 1;
            //    text7 = i + "." + txt7.Text + System.Environment.NewLine;
            //}
            //string text8 = "";
            //if (txtRem.Text != "")
            //{
            //    i = i + 1;
            //    text8 = i + "." + txtRem.Text;
            //}

            //string EnId = "";
            //if (txtEnqId.Text == "")
            //{
            //    EnId = (String)Session["EnId"];
            //}
            //else
            //{
            //    EnId = txtEnqId.Text;
            //}
            //string CName = "";
            //if (txtCustName.Text == "")
            //{
            //    CName = (String)Session["CusNam"];
            //}
            //else
            //{
            //    CName = txtCustName.Text;
            //}

            //string reportfield = text1 + text2 + text3 + text4 + text5 + text6 + text7 + text8;

            
                foreach (GridViewRow row in Gv20feet.Rows)
                {
                    //string jno = Gv20feet.DataKeys[row.RowIndex].Value.ToString();
                    CheckBox chkCharge20feet = (CheckBox)row.FindControl("chkCharge");
                    Session["Id"] = GvQuote.SelectedRow.Cells[1].Text.ToString();

                    if (chkCharge20feet.Checked)
                    {
                        Label lblId = (Label)row.FindControl("lblId");
                        Session["Id"] = lblId.Text;
                        TextBox ddl20feetdesc = (TextBox)row.FindControl("txtDescrip");
                       //TextBox ActRate = (TextBox)row.FindControl("ddlActualRate");
                        TextBox ddlunit = (TextBox)row.FindControl("txtUni");
                        TextBox MinRate = (TextBox)row.FindControl("txtMinRate");
                        TextBox VarRate = (TextBox)row.FindControl("txtVarRate");
                       // DropDownList ddlVartype = (DropDownList)row.FindControl("ddlVarType");
                        TextBox txtVartype = (TextBox)row.FindControl("txtVartype");
                        TextBox MaxRate = (TextBox)row.FindControl("txtMaxRate");
                        TextBox FixRate = (TextBox)row.FindControl("txtFixRate");

                        string test = MinRate.Text;
                        //string date = DateTime.Now.ToString("dd/MM/yyyy");
                        SqlConnection sqlConn1 = new SqlConnection(con);
                        sqlConn1.Open();
                        string Update20feet = "Update M_Quote set Description='" + ddl20feetdesc.Text + "',Type='20Feet',Unit='" + ddlunit.Text + "',MinRate='" + MinRate.Text + "',VarRate='" + VarRate.Text + "',VarType='" + txtVartype.Text + "',MaxRate='" + MaxRate.Text + "',FixRate='" + FixRate.Text + "',CreatedBy='" + (string)Session["USER-NAME"] + "',CreatedDate='" + frmdatesplit(date) + "' where ID='" + (String)Session["Id"] + "' "; //,RepTemp='" + reportfield + "'
                        SqlCommand cmd = new SqlCommand(Update20feet, sqlConn1);
                        SqlDataAdapter da = new SqlDataAdapter();
                        cmd.ExecuteNonQuery();
                      //lConn1.Close();
                    }

                }
           
                foreach (GridViewRow row in Gv40Feet.Rows)
                {                    
                    CheckBox Charge40feet = (CheckBox)row.FindControl("chkCharge40feet");

                    if (Charge40feet.Checked)
                    {
                        Label lblId = (Label)row.FindControl("lblId");
                        Session["Id"] = lblId.Text;
                        TextBox ddl20feetdesc = (TextBox)row.FindControl("txtDescrip");
                      //  TextBox ActRate = (TextBox)row.FindControl("ddlActualRate");
                        TextBox ddlunit = (TextBox)row.FindControl("txtUni");
                        TextBox MinRate = (TextBox)row.FindControl("txtMinRate");
                        TextBox VarRate = (TextBox)row.FindControl("txtVarRate");
                        //DropDownList ddlVartype = (DropDownList)row.FindControl("ddlVarType");
                        TextBox txtVartype = (TextBox)row.FindControl("txtVartype");
                        TextBox MaxRate = (TextBox)row.FindControl("txtMaxRate");
                        TextBox FixRate = (TextBox)row.FindControl("txtFixRate");

                        string test = MinRate.Text;
                        //string date = DateTime.Now.ToString("dd/MM/yyyy");
                        SqlConnection sqlConn1 = new SqlConnection(con);
                        sqlConn1.Open();
                        string Update40feet = "Update M_Quote set Description='" + ddl20feetdesc.Text + "',Type='40Feet',Unit='" + ddlunit.Text + "',MinRate='" + MinRate.Text + "',VarRate='" + VarRate.Text + "',VarType='" + txtVartype.Text + "',MaxRate='" + MaxRate.Text + "',FixRate='" + FixRate.Text + "',CreatedBy='" + (string)Session["USER-NAME"] + "',CreatedDate='" + frmdatesplit(date) + "' where ID='" + (String)Session["Id"] + "'";
                        SqlCommand cmd = new SqlCommand(Update40feet, sqlConn1);
                        SqlDataAdapter da = new SqlDataAdapter();
                        cmd.ExecuteNonQuery();
                       //sqlConn1.close();
                    }

                }


                foreach (GridViewRow row in GvLcl.Rows)
                {
                    CheckBox Charge40feet = (CheckBox)row.FindControl("chkChargelcl");
                    
                    if (Charge40feet.Checked)
                    {                      
                        Label lblId = (Label)row.FindControl("lblId");
                        Session["Id"] = lblId.Text;
                        TextBox ddl20feetdesc = (TextBox)row.FindControl("txtDescrip");
                        //TextBox ActRate = (TextBox)row.FindControl("ddlActualRate");
                        TextBox ddlunit = (TextBox)row.FindControl("txtUni");
                        TextBox MinRate = (TextBox)row.FindControl("txtMinRate");
                        TextBox VarRate = (TextBox)row.FindControl("txtVarRate");
                       // DropDownList ddlVartype = (DropDownList)row.FindControl("ddlVarType");
                        TextBox txtVartype = (TextBox)row.FindControl("txtVartype");
                        TextBox MaxRate = (TextBox)row.FindControl("txtMaxRate");
                        TextBox FixRate = (TextBox)row.FindControl("txtFixRate");

                        string test = MinRate.Text;
                        //string date = DateTime.Now.ToString("dd/MM/yyyy");
                        SqlConnection sqlConn1 = new SqlConnection(con);
                        sqlConn1.Open();
                        string Updatelcl = "Update M_Quote set Description='" + ddl20feetdesc.Text + "',Type='LCL',Unit='" + ddlunit.Text + "',MinRate='" + MinRate.Text + "',VarRate='" + VarRate.Text + "',VarType='" + txtVartype.Text + "',MaxRate='" + MaxRate.Text + "',FixRate='" + FixRate.Text + "',CreatedBy='" + (string)Session["USER-NAME"] + "',CreatedDate='" + frmdatesplit(date) + "' where Id='" + (String)Session["Id"] + "'";
                        SqlCommand cmd = new SqlCommand(Updatelcl, sqlConn1);
                        SqlDataAdapter da = new SqlDataAdapter();
                        cmd.ExecuteNonQuery();
                        //lConn1.close();
                    }

                }


                foreach (GridViewRow row in GvAir.Rows)
                {
                    CheckBox Charge40feet = (CheckBox)row.FindControl("chkChargeair");

                    if (Charge40feet.Checked)
                    {
                        Label lblId = (Label)row.FindControl("lblId");
                        Session["Id"] = lblId.Text;
                        TextBox ddl20feetdesc = (TextBox)row.FindControl("txtDescrip");
                        //TextBox ActRate = (TextBox)row.FindControl("ddlActualRate");
                        TextBox ddlunit = (TextBox)row.FindControl("txtUni");
                        TextBox MinRate = (TextBox)row.FindControl("txtMinRate");
                        TextBox VarRate = (TextBox)row.FindControl("txtVarRate");
                       // DropDownList ddlVartype = (DropDownList)row.FindControl("ddlVarType");
                        TextBox txtVartype = (TextBox)row.FindControl("txtVartype");
                        TextBox MaxRate = (TextBox)row.FindControl("txtMaxRate");
                        TextBox FixRate = (TextBox)row.FindControl("txtFixRate");

                        string test = MinRate.Text;
                        //string date = DateTime.Now.ToString("dd/MM/yyyy");
                        SqlConnection sqlConn1 = new SqlConnection(con);
                        sqlConn1.Open();
                        string Updateair = "Update M_Quote set Description='" + ddl20feetdesc.Text + "',Type='AIR',Unit='" + ddlunit.Text + "',MinRate='" + MinRate.Text + "',VarRate='" + VarRate.Text + "',VarType='" + txtVartype.Text + "',MaxRate='" + MaxRate.Text + "',FixRate='" + FixRate.Text + "',CreatedBy='" + (string)Session["USER-NAME"] + "',CreatedDate='" + frmdatesplit(date) + "' where Id='" + (String)Session["Id"] + "'";
                        SqlCommand cmd = new SqlCommand(Updateair, sqlConn1);
                        SqlDataAdapter da = new SqlDataAdapter();
                        //lConn1.close();
                        cmd.ExecuteNonQuery();
                       
                    }

                }
            SaveTemplates();
            LoadGrid();
            clear();
            btnSave.Visible = false;
            btnUpdate.Visible = true;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Successfully Updated');", true);
        }

        protected void GvQuote_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            ddlCustNam.SelectedValue = GvQuote.SelectedRow.Cells[2].Text;
            Session["CustomerName"] = GvQuote.SelectedRow.Cells[2].Text;
            Session["enqid"] = GvQuote.SelectedRow.Cells[1].Text;

             SqlConnection sqlConn = new SqlConnection(con);
            sqlConn.Open();

            string qry1 = "Select TransId,CustomerName,Air,Feet20,Feet40,Lcl,ShipmentMode from M_Enquriy where TransId='" + (string)Session["enqid"] + "'";
            SqlDataAdapter da = new SqlDataAdapter(qry1, sqlConn);
            DataSet ds = new DataSet();
            da.Fill(ds, "data");

            if (ds.Tables["data"].Rows.Count != 0)
            {
                DataRowView rw = ds.Tables["data"].DefaultView[0];

                string typ20 = rw["Feet20"].ToString();
                string typ40 = rw["Feet40"].ToString();
                string typlcl = rw["Lcl"].ToString();
                string typair = rw["Air"].ToString();
                string shipmentmode = rw["ShipmentMode"].ToString();
                //string reptemplates = rw["RepTemp"].ToString();
                //Session["Type"]=typecont;
                pnlButtons.Visible = true;
                btnSave.Visible = false;
                btnUpdate.Visible = true;

                pnlCustName.Visible = true;
                pnlGridQuote.Visible = false;
                pnlMain.Visible = true;

                if (typ20 == "True")
                {
                    Grid20FeetQuote();
                    if (shipmentmode == "Import")
                    {
                        lbl20FeetShipment.Text = "Imp";
                    }
                    else if (shipmentmode == "Export")
                    {
                        lbl20FeetShipment.Text = "Exp";
                    }
                    else if (shipmentmode == "Both")
                    {
                        lbl20FeetShipment.Text = "Both";
                    }
                    pnl20feet.Visible = true;
                    Pane1.Visible = true;
                    pnlChecking.Visible = false;
                }
                else
                {
                    Pane1.Visible = false;
                }

                if (typ40 == "True")
                {

                    Grid40FeetQuote();
                    if (shipmentmode == "Import")
                    {
                        lbl40feetShipment.Text = "Imp";
                    }
                    else if (shipmentmode == "Export")
                    {
                        lbl40feetShipment.Text = "Exp";
                    }
                    else if (shipmentmode == "Both")
                    {
                        lbl40feetShipment.Text = "Both";
                    }
                    pnl40feet.Visible = true;
                    Pane2.Visible = true;
                    pnlChecking.Visible = false;
                }
                else
                {
                    Pane2.Visible = false;
                }

                if (typlcl == "True")
                {

                    GridLclQuote();
                    if (shipmentmode == "Import")
                    {
                        lblLCLShipment.Text = "Imp";
                    }
                    else if (shipmentmode == "Export")
                    {
                        lblLCLShipment.Text = "Exp";
                    }
                    else if (shipmentmode == "Both")
                    {
                        lblLCLShipment.Text = "Both";
                    }
                    pnllcl.Visible = true;
                    Pane3.Visible = true;
                    pnlChecking.Visible = false;
                }
                else
                {
                    Pane3.Visible = false;
                }

                if (typair == "True")
                {

                    GridAirQuote();
                    if (shipmentmode == "Import")
                    {
                        lblAirShipment.Text = "Imp";
                    }
                    else if (shipmentmode == "Export")
                    {
                        lblAirShipment.Text = "Exp";
                    }
                    else if (shipmentmode == "Both")
                    {
                        lblAirShipment.Text = "Both";
                    }
                    pnlair.Visible = true;
                    Pane4.Visible = true;
                    pnlChecking.Visible = false;
                }
                else
                {
                    Pane4.Visible = false;
                }
                }

                //string reptemplates = (String)Session["Templates"];

                //string[] words = Regex.Split(reptemplates, "\r\n");
                //foreach (string word in words)
                //{

                //    if (word != "")
                //    {
                //        string wo = word.Remove(0, 2);

                //        Console.WriteLine(wo);
                //        if (txt1.Text == wo)
                //        {
                //            chk1.Checked = true;
                //        }
                //        else if (txt2.Text == wo)
                //        {
                //            chk2.Checked = true;
                //        }
                //        else if (txt3.Text == wo)
                //        {
                //            chk3.Checked = true;
                //        }
                //        else if (txt4.Text == wo)
                //        {
                //            chk4.Checked = true;
                //        }
                //        else if (txt5.Text == wo)
                //        {
                //            chk5.Checked = true;
                //        }
                //        else if (txt6.Text == wo)
                //        {
                //            chk6.Checked = true;
                //        }
                //        else if (txt7.Text == wo)
                //        {
                //            chk7.Checked = true;
                //        }
                //        else
                //        {
                //            txtRem.Text = wo;
                //        }
                //    }
                //    else
                //    {

                //    }
                //}

           // }
            FillRepTemp();
            btnPrint.Visible = true;
            pnlRepTem.Visible = true;
            Button2.Visible = true;
        }

        public void FillRepTemp()
    {
        SqlConnection con1 = new SqlConnection(con);
        con1.Open();
        string qry = "Select repTemp,Remarks from M_QuoteReportTemp where EnqId='" + (string)Session["enqid"] + "'";
        SqlDataAdapter da = new SqlDataAdapter(qry, con1);
        DataSet ds = new DataSet();
        da.Fill(ds, "data");
        if (ds.Tables["data"].Rows.Count != 0)
        {
            DataRowView row = ds.Tables["data"].DefaultView[0];
            txtRem.Text = row["Remarks"].ToString();
            string reptemplates = row["RepTemp"].ToString();            
            //
            // Split string on spaces.
            // ... This will separate all the words.
            //
            string[] words = Regex.Split(reptemplates, "\r\n");
            foreach (string word in words)
            {

                if (word != "")
                { 
                string wo = word.Remove(0, 2);
               
                    Console.WriteLine(wo);
                    if (txt1.Text == wo)
                    {
                        chk1.Checked = true;
                    }
                    else if (txt2.Text == wo)
                    {
                        chk2.Checked = true;
                    }
                    else if (txt3.Text == wo)
                    {
                        chk3.Checked = true;
                    }
                    else if (txt4.Text == wo)
                    {
                        chk4.Checked = true;
                    }
                    else if (txt5.Text == wo)
                    {
                        chk5.Checked = true;
                    }
                    else if (txt6.Text == wo)
                    {
                        chk6.Checked = true;
                    }
                    else if (txt7.Text == wo)
                    {
                        chk7.Checked = true;
                    }
                    else
                    {
                        txtRem.Text = wo;
                    }
                }
                else
                {

                }
            }


           

        }


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
            btnSave.Visible = false;
            btnUpdate.Visible = false;
            pnlMain.Visible = false;
            pnlEnqDetails.Visible = false;
            btnPrint.Visible = false;
            Button2.Visible = false;
            pnlRepTem.Visible = false;
            //FillEnquiry();
        }

        public void clear()
        {
            ddl20FeetCharges.SelectedValue = "~Select~";
            ddl40FeetCharges.SelectedValue = "~Select~";
            ddlLCLCharges.SelectedValue = "~Select~";
            ddlAIRCharges.SelectedValue = "~Select~";
            ddl20FeetUnit.SelectedValue = ddl40feetUnit.SelectedValue = ddlLCLUnit.SelectedValue = ddlAirUnit.SelectedValue = "~Select~";
          //  txt20FeetAtActual.SelectedValue = txt40FeetAtActual.SelectedValue = txtLCLAtActual.SelectedValue = txtairAtAcutal.SelectedValue = "~Select~";
            txt20feetMinimum.Text = txt40FeetMinimum.Text = txtLCLMinimum.Text = txtairminimum.Text = "";
            txt20feetVariable.Text = txt40FeetVariable.Text = txtLCLVariable.Text = txtairVariable.Text = "";
            ddlvar20.SelectedValue = ddlvar40.SelectedValue = ddlvarlcl.SelectedValue = ddlvarair.SelectedValue = "~Select~";
            txt20feetMaximum.Text = txt40FeetMaximum.Text = txtLCLMaximum.Text = txtairMaximum.Text = "";
            txt20feetFixed.Text = txt40FeetFixed.Text = txtLCLFixed.Text = txtairFixed.Text = "";

        }

        protected void btnPrint_Click(object sender, EventArgs e)
        {
            //Session["EnqId"] = txtEnqId.Text;
            Response.Redirect("frmQuotereport.aspx");
        }       

        public void FillEnquiry()
        {
            DataSet ds = new DataSet();
            string query;
            query = "Select Distinct TransId,CustomerName,Commodity,PhoneNo,Address,ModeOfEnquiry,FinalDest,Pol,Pod,ShipmentMode from M_Enquriy where QuoteCompleted='0'";
            SqlConnection sqlConn = new SqlConnection(con);
            sqlConn.Open();
            SqlDataAdapter da = new SqlDataAdapter(query, sqlConn);
            da.Fill(ds, "M_Enquiry");
            if (ds.Tables["M_Enquiry"].Rows.Count != 0)
            {
                GcEnquiry.DataSource = ds;
                GcEnquiry.DataBind();
            }
            else
            {
                GcEnquiry.DataSource = null;
                GcEnquiry.DataBind();
                //LoadGrid();
                ScriptManager.RegisterStartupScript(this, this.GetType(), "redirect", "alert('There is no Pending Enquiry'); window.location.href='frmQuote.aspx';", true);
            }
        }       

        protected void chkPendEnq_CheckedChanged(object sender, EventArgs e)
        {
            FillEnquiry();
            pnlChecking.Visible = false;
            pnlGridEnquiry.Visible = true;
            pnlGridQuote.Visible = false;
            pnlCustName.Visible = false;
            pnlMain.Visible = false;
        }

        protected void chkQuotechrgs_CheckedChanged(object sender, EventArgs e)
        {
            LoadGrid();
            pnlGridQuote.Visible = false;
            btnSave.Visible = false;
            btnUpdate.Visible = true;            
            pnlCustName.Visible = true;
            pnlChecking.Visible = false;
            pnlMain.Visible = false;
        }

        protected void GcEnquiry_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            pnlEnqDetails.Visible = true;
            txtEnqId.Text = GcEnquiry.SelectedRow.Cells[1].Text;
            txtCustName.Text = GcEnquiry.SelectedRow.Cells[2].Text;
            txtCommodity.Text = GcEnquiry.SelectedRow.Cells[3].Text;
            txtPol.Text = GcEnquiry.SelectedRow.Cells[9].Text;
            txtPod.Text = GcEnquiry.SelectedRow.Cells[8].Text;
            txtFinDest.Text = GcEnquiry.SelectedRow.Cells[7].Text;
            Session["mod"] = GcEnquiry.SelectedRow.Cells[10].Text;
            DataSet ds = new DataSet();
            string query;
            query = "Select TransId,CustomerName,Air,Feet20,Feet40,Lcl,ShipmentMode from M_Enquriy where CustomerName='" + txtCustName.Text + "'";
            SqlConnection sqlConn = new SqlConnection(con);
            sqlConn.Open();
            SqlDataAdapter da = new SqlDataAdapter(query, sqlConn);
            da.Fill(ds, "data");
            
            if (ds.Tables["data"].Rows.Count != 0)
            {
                DataRowView rw = ds.Tables["data"].DefaultView[0];
                string typeAir = rw["Air"].ToString();
                string typeFeet20 = rw["Feet20"].ToString();
                string typeFeet40 = rw["Feet40"].ToString();
                string typeLcl = rw["Lcl"].ToString();
                Session["CusName"] = rw["CustomerName"].ToString();
                Session["EnqId"] = txtEnqId.Text;
                string shipmentmode = rw["ShipmentMode"].ToString();
                Session["ShipMode"] = rw["ShipmentMode"].ToString();
                if (typeAir == "True")
                {
                    pnlair.Visible = true;
                    //Session["lblair"] = lblAirShipment.Text;
                    Pane4.Visible = true;
                    if (shipmentmode == "Imp")
                    {
                        lblAirShipment.Text = "Imp";
                    }
                    else if (shipmentmode == "Exp")
                    {
                        lblAirShipment.Text = "Exp";
                    }
                    else if (shipmentmode == "Both")
                    {
                        lblAirShipment.Text = "Both";
                    }
                    GridAir();
                   
                }
                else if (typeAir == "False")
                {
                    pnlair.Visible = false;
                    Pane4.Visible = false;
                }

                if (typeFeet20 == "True")
                {
                    pnl20feet.Visible = true;
                    //Session["lbl20feet"] = lbl20FeetShipment.Text;
                    Pane1.Visible = true;
                    if (shipmentmode == "Imp")
                    {
                        lbl20FeetShipment.Text = "Imp";
                    }
                    else if (shipmentmode == "Exp")
                    {
                        lbl20FeetShipment.Text = "Exp";
                    }
                    else if (shipmentmode == "Both")
                    {
                        lbl20FeetShipment.Text = "Both";
                    }
                    Grid20Feet();
                }
                else if (typeFeet20 == "False")
                {
                    pnl20feet.Visible = false;
                    Pane1.Visible = false;
                }

                if (typeFeet40 == "True")
                {
                    pnl40feet.Visible = true;
                   // Session["lbl40feet"] = lbl40feetShipment.Text;
                    Pane2.Visible = true;
                    if (shipmentmode == "Imp")
                    {
                        lbl40feetShipment.Text = "Imp";
                    }
                    else if (shipmentmode == "Exp")
                    {
                        lbl40feetShipment.Text = "Exp";
                    }
                    else if (shipmentmode == "Both")
                    {
                        lbl40feetShipment.Text = "Both";
                    }
                    Grid40Feet();
                }
                else if (typeFeet40 == "False")
                {
                    pnl40feet.Visible = false;
                    Pane2.Visible = false;
                }

                if (typeLcl == "True")
                {
                    pnllcl.Visible = true;
                    //Session["lbllcl"] = lblLCLShipment.Text;
                    Pane3.Visible = true;
                    if (shipmentmode == "Imp")
                    {
                        lblLCLShipment.Text = "Imp";
                    }
                    else if (shipmentmode == "Exp")
                    {
                        lblLCLShipment.Text = "Exp";
                    }
                    else if (shipmentmode == "Both")
                    {
                        lblLCLShipment.Text = "Both";
                    }
                    GridLcl();
                }
                else if (typeLcl == "False")
                {
                    pnllcl.Visible = false;
                    Pane3.Visible = false;
                }
            }
            pnlButtons.Visible = true;
            btnSave.Visible = true;
            btnUpdate.Visible = false;
            pnlMain.Visible = true;
            pnlRepTem.Visible = true;
            btnPrint.Visible = false;
            Button2.Visible = true;
        }

        public void Grid20Feet()
        {
            DataSet ds = new DataSet();
            SqlConnection sqlcon = new SqlConnection(con);
            sqlcon.Open();
            if (Session["mod"] == "Both")
            {
                string query = "Select ID,Description,Type,Unit,ActualRate,MinRate,VarRate,MaxRate,FixRate,VarType,ShipMode from M_StandardRate where Type='20Feet'";
                SqlDataAdapter da = new SqlDataAdapter(query, sqlcon);
                da.Fill(ds, "StandardRateDetails");
                Gv20feet.DataSource = ds;
                Gv20feet.DataBind();
            }
            else
            {
                string query = "Select ID,Description,Type,Unit,ActualRate,MinRate,VarRate,MaxRate,FixRate,VarType,ShipMode from M_StandardRate where Type='20Feet' and (ShipMode='" + (String)Session["mod"] + "' or ShipMode='Both')";
                SqlDataAdapter da = new SqlDataAdapter(query, sqlcon);
                da.Fill(ds, "StandardRateDetails");
                Gv20feet.DataSource = ds;
                Gv20feet.DataBind();
            }
            foreach (GridViewRow row in Gv20feet.Rows)
            {
                TextBox FixRate = (TextBox)row.FindControl("txtFixRate");
                TextBox MinRate = (TextBox)row.FindControl("txtMinRate");
                TextBox MaxRate = (TextBox)row.FindControl("txtMaxRate");
                TextBox VarRate = (TextBox)row.FindControl("txtVarRate");
                TextBox txtvartype = (TextBox)row.FindControl("txtvartype");
                if (FixRate.Text != "")
                {
                    MinRate.Enabled = false;
                    MaxRate.Enabled = false;
                    VarRate.Enabled = false;
                    txtvartype.Enabled = false;
                }
                else
                {
                    FixRate.Enabled = false;
                }

            }
            sqlcon.Close();
        }

        public void Grid40Feet()
        {
            DataSet ds = new DataSet();
            SqlConnection sqlcon = new SqlConnection(con);
            sqlcon.Open();
            if (Session["mod"] == "Both")
            {
                string query = "Select ID,Description,Type,Unit,ActualRate,MinRate,VarRate,MaxRate,FixRate,VarType,ShipMode from M_StandardRate where Type='40Feet'";
                SqlDataAdapter da = new SqlDataAdapter(query, sqlcon);
                da.Fill(ds, "StandardRateDetails");
                Gv40Feet.DataSource = ds;
                Gv40Feet.DataBind();
            }
            else
            {
                string query = "Select ID,Description,Type,Unit,ActualRate,MinRate,VarRate,MaxRate,FixRate,VarType,ShipMode from M_StandardRate where Type='40Feet' and (ShipMode='" + (String)Session["mod"] + "' or ShipMode='Both')";
                SqlDataAdapter da = new SqlDataAdapter(query, sqlcon);
                da.Fill(ds, "StandardRateDetails");
                Gv40Feet.DataSource = ds;
                Gv40Feet.DataBind();
            }
            foreach (GridViewRow row in Gv40Feet.Rows)
            {
                TextBox FixRate = (TextBox)row.FindControl("txtFixRate");
                TextBox MinRate = (TextBox)row.FindControl("txtMinRate");
                TextBox MaxRate = (TextBox)row.FindControl("txtMaxRate");
                TextBox VarRate = (TextBox)row.FindControl("txtVarRate");
                TextBox txtvartype = (TextBox)row.FindControl("txtvartype");
                if (FixRate.Text != "")
                {
                    MinRate.Enabled = false;
                    MaxRate.Enabled = false;
                    VarRate.Enabled = false;
                    txtvartype.Enabled = false;
                }
                else
                {
                    FixRate.Enabled = false;
                }

            }

            sqlcon.Close();
        }

        public void GridAir()
        {
            DataSet ds = new DataSet();
            SqlConnection sqlcon = new SqlConnection(con);
            sqlcon.Open();

            if (Session["mod"] == "Both")
            {
                string query = "Select ID,Description,Type,Unit,ActualRate,MinRate,VarRate,MaxRate,FixRate,VarType,ShipMode from M_StandardRate where Type='AIR'";
                SqlDataAdapter da = new SqlDataAdapter(query, sqlcon);
                da.Fill(ds, "StandardRateDetails");
                GvAir.DataSource = ds;
                GvAir.DataBind();
               
            }
            else
            {
                string query = "Select ID,Description,Type,Unit,ActualRate,MinRate,VarRate,MaxRate,FixRate,VarType,ShipMode from M_StandardRate where Type='AIR' and (ShipMode='" + (String)Session["mod"] + "' or ShipMode='Both')";
                SqlDataAdapter da = new SqlDataAdapter(query, sqlcon);
                da.Fill(ds, "StandardRateDetails");
                GvAir.DataSource = ds;
                GvAir.DataBind();
            }
            foreach (GridViewRow row in GvAir.Rows)
            {
                TextBox FixRate = (TextBox)row.FindControl("txtFixRate");
                TextBox MinRate = (TextBox)row.FindControl("txtMinRate");
                TextBox MaxRate = (TextBox)row.FindControl("txtMaxRate");
                TextBox VarRate = (TextBox)row.FindControl("txtVarRate");
                TextBox txtvartype = (TextBox)row.FindControl("txtvartype");
                if (FixRate.Text != "")
                {
                    MinRate.Enabled = false;
                    MaxRate.Enabled = false;
                    VarRate.Enabled = false;
                    txtvartype.Enabled = false;
                }
                else
                {
                    FixRate.Enabled = false;
                }

            }
            sqlcon.Close();
        }
         
        public void GridLcl()
        {
            DataSet ds = new DataSet();
            SqlConnection sqlcon = new SqlConnection(con);
            sqlcon.Open();
            if (Session["mod"] == "Both")
            {
                string query = "Select ID,Description,Type,Unit,ActualRate,MinRate,VarRate,MaxRate,FixRate,VarType,ShipMode from M_StandardRate where Type='LCL'";
                SqlDataAdapter da = new SqlDataAdapter(query, sqlcon);
                da.Fill(ds, "StandardRateDetails");
                GvLcl.DataSource = ds;
                GvLcl.DataBind();
            }
            else
            {
                string query = "Select ID,Description,Type,Unit,ActualRate,MinRate,VarRate,MaxRate,FixRate,VarType,ShipMode from M_StandardRate where Type='LCL' and (ShipMode='" + (String)Session["mod"] + "' or ShipMode='Both')";
                SqlDataAdapter da = new SqlDataAdapter(query, sqlcon);
                da.Fill(ds, "StandardRateDetails");
                GvLcl.DataSource = ds;
                GvLcl.DataBind();
            }
            foreach (GridViewRow row in GvLcl.Rows)
            {
                TextBox FixRate = (TextBox)row.FindControl("txtFixRate");
                TextBox MinRate = (TextBox)row.FindControl("txtMinRate");
                TextBox MaxRate = (TextBox)row.FindControl("txtMaxRate");
                TextBox VarRate = (TextBox)row.FindControl("txtVarRate");
                TextBox txtvartype = (TextBox)row.FindControl("txtvartype");
                if (FixRate.Text != "")
                {
                    MinRate.Enabled = false;
                    MaxRate.Enabled = false;
                    VarRate.Enabled = false;
                    txtvartype.Enabled = false;
                }
                else
                {
                    FixRate.Enabled = false;
                }

            }
            sqlcon.Close();
        }

        public void Grid20FeetQuote()
        {
            DataSet ds = new DataSet();
            SqlConnection sqlcon = new SqlConnection(con);
            string query = "Select ID,EnqId,CustomerName,Description,Type,Unit,ActualRate,MinRate,VarRate,MaxRate,FixRate,VarType from M_Quote where Type='20Feet' and CustomerName='" + (string)Session["CustomerName"] + "' and EnqId='" + (string)Session["enqid"] + "'";
            SqlDataAdapter da = new SqlDataAdapter(query, sqlcon);
            da.Fill(ds, "StandardRateDetails");
            Gv20feet.DataSource = ds;
            Gv20feet.DataBind();
            if (ds.Tables["StandardRateDetails"].Rows.Count != 0)
            {
                DataRowView row = ds.Tables["StandardRateDetails"].DefaultView[0];
                Session["EnId"] = row["EnqId"].ToString();
                Session["CusNam"] = row["CustomerName"].ToString();
               
            }
            sqlcon.Close();
        }

        public void Grid40FeetQuote()
        {
            DataSet ds = new DataSet();
            SqlConnection sqlcon = new SqlConnection(con);
            string query = "Select ID,EnqId,CustomerName,Description,Type,Unit,ActualRate,MinRate,VarRate,MaxRate,FixRate,VarType from M_Quote where Type='40Feet' and CustomerName='" + (string)Session["CustomerName"] + "' and EnqId='" + (string)Session["enqid"] + "'";
            SqlDataAdapter da = new SqlDataAdapter(query, sqlcon);
            da.Fill(ds, "StandardRateDetails");
            Gv40Feet.DataSource = ds;
            Gv40Feet.DataBind();
            if (ds.Tables["StandardRateDetails"].Rows.Count != 0)
            {
                DataRowView row = ds.Tables["StandardRateDetails"].DefaultView[0];
                Session["EnId"] = row["EnqId"].ToString();
                Session["CusNam"] = row["CustomerName"].ToString();
               
            }
            sqlcon.Close();
        }

        public void GridLclQuote()
        {
            DataSet ds = new DataSet();
            SqlConnection sqlcon = new SqlConnection(con);
            string query = "Select ID,EnqId,CustomerName,Description,Type,Unit,ActualRate,MinRate,VarRate,MaxRate,FixRate,VarType from M_Quote where Type='LCL' and CustomerName='" + (string)Session["CustomerName"] + "' and EnqId='" + (string)Session["enqid"] + "'";
            SqlDataAdapter da = new SqlDataAdapter(query, sqlcon);
            da.Fill(ds, "StandardRateDetails");
            GvLcl.DataSource = ds;
            GvLcl.DataBind();
            if (ds.Tables["StandardRateDetails"].Rows.Count != 0)
            {
                DataRowView row = ds.Tables["StandardRateDetails"].DefaultView[0];
                Session["EnId"] = row["EnqId"].ToString();
                Session["CusNam"] = row["CustomerName"].ToString();
               
            }
            sqlcon.Close();
        }

        public void GridAirQuote()
        {
            DataSet ds = new DataSet();
            SqlConnection sqlcon = new SqlConnection(con);
            string query = "Select ID,EnqId,CustomerName,Description,Type,Unit,ActualRate,MinRate,VarRate,MaxRate,FixRate,VarType from M_Quote where Type='AIR' and CustomerName='" + (string)Session["CustomerName"] + "' and EnqId='" + (string)Session["enqid"] + "'";
            SqlDataAdapter da = new SqlDataAdapter(query, sqlcon);
            da.Fill(ds, "StandardRateDetails");
            GvAir.DataSource = ds;
            GvAir.DataBind();
            if (ds.Tables["StandardRateDetails"].Rows.Count != 0)
            {
                DataRowView row = ds.Tables["StandardRateDetails"].DefaultView[0];
                Session["EnId"] = row["EnqId"].ToString();
                Session["CusNam"] = row["CustomerName"].ToString();
              
            }
            sqlcon.Close();
        }

        protected void btnAdd20feet_Click(object sender, EventArgs e)
        {
            string date = DateTime.Now.ToString("dd/MM/yyyy");
            SqlConnection sqlConn = new SqlConnection(con);
            sqlConn.Open();
            string qry1 = "Select ID,Description,Type,Unit,ActualRate,MinRate,VarRate,MaxRate,FixRate from M_StandardRate where Description='" + ddl20FeetCharges.SelectedValue + "' and Type='20Feet' and ShipMode='" + (String)Session["mod"] + "' ";
            SqlDataAdapter sa = new SqlDataAdapter(qry1, con);
                DataSet ds2 = new DataSet();
                sa.Fill(ds2, "data");

                if (ds2.Tables["data"].Rows.Count == 0)
                {
                    string insert20feet = "Insert into M_StandardRate(ShipMode,Description,Type,Unit,MinRate,VarRate,MaxRate,FixRate,CreatedBy,CreatedDate)" +
                                                "values('" + (String)Session["mod"] + "','" + ddl20FeetCharges.SelectedValue + "','20Feet','" + ddl20FeetUnit.SelectedValue + "','" + txt20feetMinimum.Text + "','" + txt20feetVariable.Text + "','" + txt20feetMaximum.Text + "','" + txt20feetFixed.Text + "','" + (string)Session["USER-NAME"] + "','" + frmdatesplit(date) + "')";
                    SqlCommand cmd = new SqlCommand(insert20feet, sqlConn);
                    SqlDataAdapter da = new SqlDataAdapter();
                    cmd.ExecuteNonQuery();
                    Grid20Feet();
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Successfully Added');", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Already Exists');", true);
                }
                lbl20FeetShipment.Text = txt20feetFixed.Text = txt20feetMinimum.Text = txt20feetMaximum.Text = "";
                ddl20FeetCharges.SelectedValue = ddl20FeetUnit.SelectedValue = ddlvar20.SelectedValue = "~Select~";
                txt20feetVariable.Text = "0";

        }

        protected void btnAdd40feet_Click(object sender, EventArgs e)
        {
            string date = DateTime.Now.ToString("dd/MM/yyyy");
            SqlConnection sqlConn = new SqlConnection(con);
            sqlConn.Open();
            string qry1 = "Select ID,Description,Type,Unit,ActualRate,MinRate,VarRate,MaxRate,FixRate,ShipMode from M_StandardRate where Description='" + ddl40FeetCharges.SelectedValue + "' and Type='40Feet' and ShipMode='" + (String)Session["mod"] + "'";
            SqlDataAdapter sa = new SqlDataAdapter(qry1, con);
                DataSet ds2 = new DataSet();
                sa.Fill(ds2, "data");

                if (ds2.Tables["data"].Rows.Count == 0)
                {
                    string insert40feet = "Insert into M_StandardRate(ShipMode,Description,Type,Unit,MinRate,VarRate,MaxRate,FixRate,CreatedBy,CreatedDate)" +
                                         "values('" + (String)Session["mod"] + "','" + ddl40FeetCharges.SelectedValue + "','40Feet','" + ddl40feetUnit.SelectedValue + "','" + txt40FeetMinimum.Text + "','" + txt40FeetVariable.Text + "','" + txt40FeetMaximum.Text + "','" + txt40FeetFixed.Text + "','" + (string)Session["USER-NAME"] + "','" + frmdatesplit(date) + "')";
                    SqlCommand cmd = new SqlCommand(insert40feet, sqlConn);
                    SqlDataAdapter da = new SqlDataAdapter();
                    cmd.ExecuteNonQuery();
                    Grid40Feet();
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Successfully Added');", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Already Exists');", true);
                }

                lbl40feetShipment.Text = txt40FeetFixed.Text = txt40FeetMinimum.Text = txt40FeetMaximum.Text = "";
                ddl40FeetCharges.SelectedValue = ddl40feetUnit.SelectedValue = ddlvar40.SelectedValue = "~Select~";
                txt40FeetVariable.Text = "0";
        
           }

        protected void btnlcl_Click(object sender, EventArgs e)
        {
            string date = DateTime.Now.ToString("dd/MM/yyyy");
            SqlConnection sqlConn = new SqlConnection(con);
            sqlConn.Open();
            string qry1 = "Select ID,Description,Type,Unit,ActualRate,MinRate,VarRate,MaxRate,FixRate from M_StandardRate where Description='" + ddl20FeetCharges.SelectedValue + "' and Type='LCL'  and ShipMode='" + (String)Session["mod"] + "'";
            SqlDataAdapter sa = new SqlDataAdapter(qry1, con);
                DataSet ds2 = new DataSet();
                sa.Fill(ds2, "data");

                if (ds2.Tables["data"].Rows.Count == 0)
                {
                    string insertlcl = "Insert into M_StandardRate(ShipMode,Description,Type,Unit,MinRate,VarRate,MaxRate,FixRate,CreatedBy,CreatedDate)" +
                                         "values('" + (String)Session["mod"] + "','" + ddlLCLCharges.SelectedValue + "','LCL','" + ddlLCLUnit.SelectedValue + "','" + txtLCLMinimum.Text + "','" + txtLCLVariable.Text + "','" + txtLCLMaximum.Text + "','" + txtLCLFixed.Text + "','" + (string)Session["USER-NAME"] + "','" + frmdatesplit(date) + "')";
                    SqlCommand cmd = new SqlCommand(insertlcl, sqlConn);
                    SqlDataAdapter da = new SqlDataAdapter();
                    cmd.ExecuteNonQuery();
                    GridLcl();
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Successfully Added');", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Already Exists');", true);
                }
                lblLCLShipment.Text = txtLCLFixed.Text = txtLCLMinimum.Text = txtLCLMaximum.Text = "";
                ddlLCLCharges.SelectedValue = ddlLCLUnit.SelectedValue = ddlvarlcl.SelectedValue = "~Select~";
                txtLCLVariable.Text = "0";

        }

        protected void btnair_Click(object sender, EventArgs e)
        {
            string date = DateTime.Now.ToString("dd/MM/yyyy");
            SqlConnection sqlConn = new SqlConnection(con);
            sqlConn.Open();
            string qry1 = "Select ID,Description,Type,Unit,ActualRate,MinRate,VarRate,MaxRate,FixRate from M_StandardRate where Description='" + ddlAIRCharges.SelectedValue + "' and Type='AIR' and ShipMode='" + (String)Session["mod"] + "'";
            SqlDataAdapter sa = new SqlDataAdapter(qry1, con);
                DataSet ds2 = new DataSet();
                sa.Fill(ds2, "data");

                if (ds2.Tables["data"].Rows.Count == 0)
                {
                    string insertair = "Insert into M_StandardRate(ShipMode,Description,Type,Unit,MinRate,VarRate,MaxRate,FixRate,CreatedBy,CreatedDate)" +
                                         "values('" + (String)Session["mod"] + "','" + ddlAIRCharges.SelectedValue + "','AIR','" + ddlAirUnit.SelectedValue + "','" + txtairminimum.Text + "','" + txtairVariable.Text + "','" + txtairMaximum.Text + "','" + txtairFixed.Text + "','" + (string)Session["USER-NAME"] + "','" + frmdatesplit(date) + "')";
                    SqlCommand cmd3 = new SqlCommand(insertair, sqlConn);
                    SqlDataAdapter da3 = new SqlDataAdapter();
                    //da.SelectCommand = cmd;
                    cmd3.ExecuteNonQuery();
                    GridAir();
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Successfully Added');", true);
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Already Exists');", true);
                }

            lblAirShipment.Text=txtairFixed.Text=txtairminimum.Text=txtairMaximum.Text="";
            ddlAIRCharges.SelectedValue=ddlAirUnit.SelectedValue=ddlvarair.SelectedValue="~Select~";
            txtairVariable.Text = "0";

        }

        protected void GvQuote_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            DataSet ds = new DataSet();
            SqlConnection sqlcon = new SqlConnection(con);
            string query = "Select ID,CustomerName,Description,Type,Unit,ActualRate,MinRate,VarRate,VarType,MaxRate,FixRate from M_Quote";
            SqlDataAdapter da = new SqlDataAdapter(query, sqlcon);
            da.Fill(ds, "StandardRateDetails");
            GvQuote.DataSource = ds;
            GvQuote.DataBind();
            GvQuote.PageIndex = e.NewPageIndex;
            GvQuote.DataBind();
            sqlcon.Close();
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            DataSet ds = new DataSet();
            SqlConnection sqlcon = new SqlConnection(con);
            string query = "Select Distinct EnqId,CustomerName,Commodity,Air,Feet20,Feet40,Lcl from View_QuoteDet where CustomerName='" + ddlCustNam.Text + "'";
            SqlDataAdapter da = new SqlDataAdapter(query, sqlcon);
            da.Fill(ds, "StandardRateDetails");
            if (ds.Tables["StandardRateDetails"].Rows.Count != 0)
            {
                GvQuote.DataSource = ds;
                GvQuote.DataBind();
                pnlGridQuote.Visible = true;
            }
            else
            {
                pnlGridQuote.Visible = false;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('There is no Quote Available for " + ddlCustNam.Text + "');", true);
            }
            sqlcon.Close();
            pnlMain.Visible = false;
            btnPrint.Visible = false;
            btnUpdate.Visible = false;
            pnlRepTem.Visible = false;
        }

        protected void GvAir_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void Gv20feet_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            DataSet ds = new DataSet();
            SqlConnection sqlcon = new SqlConnection(con);
            string query = "Select ID,Description,Type,Unit,ActualRate,MinRate,VarRate,MaxRate,FixRate,VarType from M_StandardRate where Type='20Feet'";
            SqlDataAdapter da = new SqlDataAdapter(query, sqlcon);
            da.Fill(ds, "StandardRateDetails");
            Gv20feet.DataSource = ds;
            Gv20feet.DataBind();
            Gv20feet.PageIndex = e.NewPageIndex;
            Gv20feet.DataBind();
            sqlcon.Close();
        }

        protected void Gv40Feet_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            DataSet ds = new DataSet();
            SqlConnection sqlcon = new SqlConnection(con);
            string query = "Select ID,Description,Type,Unit,ActualRate,MinRate,VarRate,MaxRate,FixRate,VarType from M_StandardRate where Type='40Feet'";
            SqlDataAdapter da = new SqlDataAdapter(query, sqlcon);
            da.Fill(ds, "StandardRateDetails");
            Gv40Feet.DataSource = ds;
            Gv40Feet.DataBind();
            Gv40Feet.PageIndex = e.NewPageIndex;
            Gv40Feet.DataBind();
            sqlcon.Close();
        }

        protected void GvLcl_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            DataSet ds = new DataSet();
            SqlConnection sqlcon = new SqlConnection(con);
            string query = "Select ID,Description,Type,Unit,ActualRate,MinRate,VarRate,MaxRate,FixRate,VarType from M_StandardRate where Type='LCL'";
            SqlDataAdapter da = new SqlDataAdapter(query, sqlcon);
            da.Fill(ds, "StandardRateDetails");
            GvLcl.DataSource = ds;
            GvLcl.DataBind();
            GvLcl.PageIndex = e.NewPageIndex;
            GvLcl.DataBind();
            sqlcon.Close();
        }

        protected void GvAir_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {

            DataSet ds = new DataSet();
            SqlConnection sqlcon = new SqlConnection(con);
            string query = "Select ID,Description,Type,Unit,ActualRate,MinRate,VarRate,MaxRate,FixRate,VarType from M_StandardRate where Type='AIR'";
            SqlDataAdapter da = new SqlDataAdapter(query, sqlcon);
            da.Fill(ds, "StandardRateDetails");
            GvAir.DataSource = ds;
            GvAir.DataBind();
            GvAir.PageIndex = e.NewPageIndex;
            GvAir.DataBind();
            sqlcon.Close();
        }

        protected void GcEnquiry_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            DataSet ds = new DataSet();
            string query;
            query = "Select TransId,CustomerName,Commodity,PhoneNo,Address,ModeOfEnquiry,FinalDest,Pol,Pod from M_Enquriy";
            SqlConnection sqlConn = new SqlConnection(con);
            sqlConn.Open();
            SqlDataAdapter da = new SqlDataAdapter(query, sqlConn);
            da.Fill(ds, "M_Enquiry");
            GcEnquiry.DataSource = ds;
            GcEnquiry.DataBind();
            GcEnquiry.PageIndex = e.NewPageIndex;
            GcEnquiry.DataBind();           
        }

        protected void ddlAirUnit_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlAirUnit.SelectedValue == "At Actual")
            {
              
                txtairFixed.Enabled = false;
                txtairminimum.Enabled = false;
                txtairVariable.Enabled = false;
                ddlvarair.Enabled = false;
                txtairMaximum.Enabled = false;
            }
            else
            {
              
                txtairFixed.Enabled = true;
                txtairminimum.Enabled = true;
                txtairVariable.Enabled = true;
                ddlvarair.Enabled = true;
                txtairMaximum.Enabled = true;
            }
        }

        protected void txtairFixed_TextChanged(object sender, EventArgs e)
        {
            if (txtairFixed.Text.Length != 0)    //Request.Form["txtairFixed"].Length
            {
                txtairminimum.Enabled = false;
                txtairVariable.Enabled = false;
                ddlvarair.Enabled = false;
                txtairMaximum.Enabled = false;
            }
            else
            {
                txtairminimum.Enabled = true;
                txtairVariable.Enabled = true;
                ddlvarair.Enabled = true;
                txtairMaximum.Enabled = true;
            }
        }

        protected void txtairminimum_TextChanged(object sender, EventArgs e)
        {
            if (txtairminimum.Text.Length != 0)
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
                //txtLCLAtActual.SelectedValue = "At Actual";
                txtLCLFixed.Enabled = false;
                txtLCLMinimum.Enabled = false;
                txtLCLVariable.Enabled = false;
                ddlvarlcl.Enabled = false;
                txtLCLMaximum.Enabled = false;
            }
            else
            {
                //txtLCLAtActual.SelectedValue = "~Select~";
                txtLCLFixed.Enabled = true;
                txtLCLMinimum.Enabled = true;
                txtLCLVariable.Enabled = true;
                ddlvarlcl.Enabled = true;
                txtLCLMaximum.Enabled = true;
            }
        }

        protected void txtLCLFixed_TextChanged(object sender, EventArgs e)
        {
            if (txtLCLFixed.Text.Length != 0)
            {
                txtLCLMinimum.Enabled = false;
                txtLCLVariable.Enabled = false;
                ddlvarlcl.Enabled = false;
                txtLCLMaximum.Enabled = false;
            }
            else
            {
                txtLCLMinimum.Enabled = true;
                txtLCLVariable.Enabled = true;
                ddlvarlcl.Enabled = true;
                txtLCLMaximum.Enabled = true;
            }
        }

        protected void txtLCLMinimum_TextChanged(object sender, EventArgs e)
        {
            if (txtLCLMinimum.Text.Length != 0)
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
                //txt20FeetAtActual.SelectedValue = "At Actual";
                txt20feetFixed.Enabled = false;
                txt20feetMinimum.Enabled = false;
                txt20feetVariable.Enabled = false;
                ddlvar20.Enabled = false;
                txt20feetMaximum.Enabled = false;
                //txt20FeetAtActual.Enabled = false;
            }
            else
            {
                //txt20FeetAtActual.SelectedValue = "~Select~";
                txt20feetFixed.Enabled = true;
                txt20feetMinimum.Enabled = true;
                txt20feetVariable.Enabled = true;
                ddlvar20.Enabled = true;
                txt20feetMaximum.Enabled = true;
                //txt20FeetAtActual.Enabled = false;
            }
        }

        protected void txt20feetFixed_TextChanged(object sender, EventArgs e)
        {
            if (txt20feetFixed.Text.Length != 0)
            {
                txt20feetMinimum.Enabled = false;
                txt20feetVariable.Enabled = false;
                ddlvar20.Enabled = false;
                txt20feetMaximum.Enabled = false;
               // txt20FeetAtActual.Enabled = false;
            }
            else
            {
                txt20feetMinimum.Enabled = true;
                txt20feetVariable.Enabled = true;
                ddlvar20.Enabled = true;
                txt20feetMaximum.Enabled = true;
                //txt20FeetAtActual.Enabled = true;
            }
        }

        protected void txt20feetMinimum_TextChanged(object sender, EventArgs e)
        {
            if (txt20feetMinimum.Text.Length != 0)
            {
                txt20feetFixed.Enabled = false;
                //txt20FeetAtActual.Enabled = false;
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
                //txt40FeetAtActual.SelectedValue = "At Actual";
                txt40FeetFixed.Enabled = false;
                txt40FeetMinimum.Enabled = false;
                txt40FeetVariable.Enabled = false;
                ddlvar40.Enabled = false;
                txt40FeetMaximum.Enabled = false;
                //txt40FeetAtActual.Enabled = false;
            }
            else
            {
               // txt40FeetAtActual.SelectedValue = "~Select~";
                txt40FeetFixed.Enabled = true;
                txt40FeetMinimum.Enabled = true;
                txt40FeetVariable.Enabled = true;
                ddlvar40.Enabled = true;
                txt40FeetMaximum.Enabled = true;
               // txt40FeetAtActual.Enabled = false;
            }
        }

        protected void txt40FeetFixed_TextChanged(object sender, EventArgs e)
        {
            if (txt40FeetFixed.Text.Length != 0)
            {
                txt40FeetMinimum.Enabled = false;
                txt40FeetVariable.Enabled = false;
                ddlvar40.Enabled = false;
                txt40FeetMaximum.Enabled = false;
               // txt40FeetAtActual.Enabled = false;
            }
            else
            {
                txt40FeetMinimum.Enabled = true;
                txt40FeetVariable.Enabled = true;
                ddlvar40.Enabled = true;
                txt40FeetMaximum.Enabled = true;
                //txt40FeetAtActual.Enabled = true;
            }
        }

        protected void txt40FeetMinimum_TextChanged(object sender, EventArgs e)
        {
            if (txt40FeetMinimum.Text.Length != 0)
            {
                txt40FeetFixed.Enabled = false;
               // txt40FeetAtActual.Enabled = false;
            }
            else
            {
                txt40FeetFixed.Enabled = true;
            }
        }

        protected void GvAir_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {


                //for (int j = 1; j < e.Row.Cells.Count; j++)
                //{
                    
                    //if (e.Row.Cells[3].Text != "")
                    //{
                       // e.Row.Cells[3].Enabled = false;
                   // }
               // }
            }
            //    DataSet ds = new DataSet();
            //    SqlConnection sqlcon = new SqlConnection(con);
            //    string query = "Select ID,Description,Type,Unit,ActualRate,MinRate,VarRate,MaxRate,FixRate,VarType from M_StandardRate where Type='AIR'";
            //    SqlDataAdapter da = new SqlDataAdapter(query, sqlcon);
            //    da.Fill(ds, "StandardRateDetails");
            //    sqlcon.Close();

            //    if (ds.Tables["StandardRateDetails"].Rows.Count != 0)
            //    {
            //        if (i <= (ds.Tables["StandardRateDetails"].Rows.Count - 1))
            //        {
            //            DataRowView row1 = ds.Tables["StandardRateDetails"].DefaultView[i];

            //            ddlVarType.SelectedValue = row1["VarType"].ToString();
            //            ddlAir.SelectedValue = row1["unit"].ToString();
            //            lblId.Text = row1["ID"].ToString();
            //            txtDescrip.Text = row1["Description"].ToString();
            //            txtFixRate.Text = row1["FixRate"].ToString();
            //            txtMinRate.Text = row1["MinRate"].ToString();
            //            txtMaxRate.Text = row1["MaxRate"].ToString();
            //            txtVarRate.Text = row1["VarRate"].ToString();

            //        }

            //    }
            //   i++;
            //    }
                
            //}
        }      

        protected void GvLcl_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }

        protected void Gv20feet_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }

        protected void Gv40Feet_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }

        protected void txtUni_TextChanged(object sender, EventArgs e)
        {
            //foreach (GridViewRow row in GvAir.SelectedRow)
            //{
            //    TextBox FixRate = (TextBox)row.FindControl("txtFixRate");
            //    TextBox MinRate = (TextBox)row.FindControl("txtMinRate");
            //    TextBox MaxRate = (TextBox)row.FindControl("txtMaxRate");
            //    TextBox VarRate = (TextBox)row.FindControl("txtVarRate");
            //    TextBox txtvartype = (TextBox)row.FindControl("txtvartype");
            //}

               
        }

        protected void GcEnquiry_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                for (int j = 1; j < e.Row.Cells.Count; j++)
                {
                    string encoded = e.Row.Cells[j].Text;
                    e.Row.Cells[j].Text = Context.Server.HtmlDecode(encoded);
                }
            }
        }

        protected void GvQuote_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                for (int j = 1; j < e.Row.Cells.Count; j++)
                {
                    string encoded = e.Row.Cells[j].Text;
                    e.Row.Cells[j].Text = Context.Server.HtmlDecode(encoded);
                }
            }
        }

        protected void btnSavEnq_Click(object sender, EventArgs e)
        {
            Save();
        }

        public void Save()
        {
            SqlConnection con1 = new SqlConnection(con);
            con1.Open();
            string qry = "Update M_Enquriy set CustomerName='" + txtCustName.Text + "',Pol='" + txtPod.Text + "',Pod='" + txtPol.Text + "',Commodity='" + txtCommodity.Text + "', FinalDest='" + txtFinDest.Text + "' where TransId='" + txtEnqId.Text + "'";
            SqlCommand cmd = new SqlCommand(qry, con1);
            cmd.ExecuteNonQuery();
            con1.Close();

        }

        protected void txtRem_TextChanged(object sender, EventArgs e)
        {

        }            
            
    }
}