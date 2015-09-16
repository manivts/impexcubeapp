using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

namespace ImpexCube
{
    public partial class frmSEW : System.Web.UI.Page
    {
        string strconn1 = (string)ConfigurationManager.ConnectionStrings["Constr"].ConnectionString;
        string flag;
        #region
        Double sldper;
        string mis_cur;
        Double mis_per;
        Double mis_amt;
        string SHECESS = "1";
        string frt_cur;
        Double frt_per;
        Double frt_amt;

        string ins_cur;
        Double ins_per;
        Double ins_amt;
        string ins_on;
        Double eX_rate;

        Double MIS_VAL;
        Double FRT_VAL;
        Double INS_VAL;
        Double tot_invVAL;
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack == false)
            {
                try
                {
                    string formID = "SEW Report";
                Authenticate.Forms(formID);
                string Validate = (string)Session["DISABLE"];
                if (Validate == "True")
                {
                    drParty.DataSource = GetParty();
                    drParty.DataTextField = "PARTY_NAME";
                    drParty.DataValueField = "PARTY_CODE";
                    drParty.DataBind();
                    drParty.Items.Insert(0, new ListItem("select", "0"));

                    drParty.SelectedItem.Text = "SEW EURODRIVE INDIA PVT LTD";
                    drParty.SelectedValue = "SEIN";
                    //to get JobNo for customers
                    GetPartyJOBNO();
                    //   Session["flag"] = "0";
                    BtnCancel.Visible = false;
                    BtnSubmit.Visible = false;

                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "redirect", "alert('You have not Authorized for this Page'); window.location.href='HomePage.aspx';", true);

                }
                }
                catch (Exception ex)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('" + ex.Message + "');", true);
                }
            }
        }

        protected void GetPartyJOBNO()
        {
            //if (drParty.SelectedItem.Selected == true)
            //{
            string pc = drParty.SelectedValue;
            string year = (string)Session["FinancialYear"];
            drJOBNO.DataSource = GetJOBNO(pc, year);
            drJOBNO.DataTextField = "jobsno";
            drJOBNO.DataValueField = "job_no";
            drJOBNO.DataBind();
            drJOBNO.Items.Insert(0, new ListItem("select", "0"));
            drJOBNO.Focus();
            //}
        }    

        public DataSet GetParty()
        {
            SqlConnection conn = new SqlConnection(strconn1);
            string str = "select * from prt_mast order by PARTY_NAME";

            SqlDataAdapter da = new SqlDataAdapter(str, conn);
            DataSet ds = new DataSet();
            da.Fill(ds, "party");
            return (ds);
        }

        public DataSet GetJOBNO(string pcode, string year)
        {
            SqlConnection conn = new SqlConnection(strconn1);
            string str = "select * from iworkreg_jobstatus where PARTY_CODE='" + pcode + "' and " +
                         "JOB_NO like '%" + year + "%' order by JOB_NO";

            SqlDataAdapter da = new SqlDataAdapter(str, conn);
            DataSet ds = new DataSet();
            da.Fill(ds, "party");
            return (ds);
        }

        public DataSet GetData(string JNO)
        {
            SqlConnection conn = new SqlConnection(strconn1);

            string str = "SELECT * FROM iproddtl i ,iinv_dtl j " +
                         "where  i.JOB_NO=j.JOB_NO and i.INV_ID=j.INV_ID and i.JOB_NO='" + JNO + "' order by i.INV_ID,i.PROD_SN ";
            SqlDataAdapter da = new SqlDataAdapter(str, conn);
            DataSet ds = new DataSet();
            da.Fill(ds, "product");
            return (ds);
        }

        protected void BtnFind_Click(object sender, EventArgs e)
        {
            try
            {
                string jno = drJOBNO.SelectedValue;
                Session["JOBNOS"] = jno;

                SqlConnection conn = new SqlConnection(strconn1);
                string str = "select * from ipurchase_dtl " +
                           " where job_no='" + jno + "'";
                SqlDataAdapter da = new SqlDataAdapter(str, conn);
                DataSet ds = new DataSet();
                da.Fill(ds, "product");
                if (ds.Tables["product"].Rows.Count == 0)
                {
                    DGDetail1.DataSource = GetData(jno);
                    DGDetail1.DataBind();
                    //DataGrid1.Visible = false;
                    DGDetail1.Visible = true;
                    BtnSubmit.Visible = true;
                    BtnSubmit.Enabled = true;
                    lblMessage.Visible = false;
                }
                else
                {
                    DGDetail1.Visible = false;
                    Response.Write("<script>alert('Given Job has Already Generated in CSV Formate....')</script>");
                    //lblMessage.Text = "Given Job has Already Generated in CSV Formate....";

                }
                // Session["flag"] = "0";

                BtnCancel.Visible = true;
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('" + ex.Message + "');", true);
            }
        }

        protected void BtnSubmit_Click(object sender, EventArgs e)
        {
            try
            {

                string jobno = Session["JOBNOS"].ToString();
                foreach (DataGridItem Row in DGDetail1.Items)
                {

                    //string add1 = ((System.Web.UI.WebControls.TextBox)(Row.Cells[3].Controls[0])).Text;
                    //TextBox MyTextBox1=Row.Cells[1].Controls[1];
                    System.Web.UI.WebControls.TextBox myTextBox1 = (System.Web.UI.WebControls.TextBox)(Row.Cells[0].Controls[1]);
                    System.Web.UI.WebControls.TextBox myTextBox2 = (System.Web.UI.WebControls.TextBox)(Row.Cells[2].Controls[1]);
                    System.Web.UI.WebControls.TextBox myTextBox3 = (System.Web.UI.WebControls.TextBox)(Row.Cells[3].Controls[1]);
                    System.Web.UI.WebControls.TextBox myTextBox4 = (System.Web.UI.WebControls.TextBox)(Row.Cells[4].Controls[1]);
                    System.Web.UI.WebControls.TextBox myTextBox5 = (System.Web.UI.WebControls.TextBox)(Row.Cells[5].Controls[1]);
                    System.Web.UI.WebControls.TextBox myTextBox6 = (System.Web.UI.WebControls.TextBox)(Row.Cells[6].Controls[1]);
                    System.Web.UI.WebControls.TextBox myTextBox7 = (System.Web.UI.WebControls.TextBox)(Row.Cells[7].Controls[1]);
                    System.Web.UI.WebControls.TextBox myTextBox8 = (System.Web.UI.WebControls.TextBox)(Row.Cells[8].Controls[1]);
                    System.Web.UI.WebControls.TextBox myTextBox9 = (System.Web.UI.WebControls.TextBox)(Row.Cells[9].Controls[1]);
                    System.Web.UI.WebControls.TextBox myTextBox10 = (System.Web.UI.WebControls.TextBox)(Row.Cells[10].Controls[1]);
                    System.Web.UI.WebControls.TextBox myTextBox11 = (System.Web.UI.WebControls.TextBox)(Row.Cells[11].Controls[1]);
                    System.Web.UI.WebControls.TextBox myTextBox12 = (System.Web.UI.WebControls.TextBox)(Row.Cells[12].Controls[1]);
                    System.Web.UI.WebControls.TextBox myTextBox13 = (System.Web.UI.WebControls.TextBox)(Row.Cells[13].Controls[1]);

                    TextBox txtASSVAL = (TextBox)Row.Cells[14].Controls[1];
                    TextBox txtbDUTYper = (TextBox)Row.Cells[15].Controls[1];
                    TextBox txtcDUTYper = (TextBox)Row.Cells[16].Controls[1];

                    TextBox txtMISCUR = (TextBox)Row.Cells[17].Controls[1];
                    TextBox txtMISPER = (TextBox)Row.Cells[18].Controls[1];
                    TextBox txtMISVAL = (TextBox)Row.Cells[19].Controls[1];

                    TextBox txtFRTCUR = (TextBox)Row.Cells[20].Controls[1];
                    TextBox txtFRTPER = (TextBox)Row.Cells[21].Controls[1];
                    TextBox txtFRTVAL = (TextBox)Row.Cells[22].Controls[1];

                    TextBox txtINSCUR = (TextBox)Row.Cells[23].Controls[1];
                    TextBox txtINSPER = (TextBox)Row.Cells[24].Controls[1];
                    TextBox txtINSVAL = (TextBox)Row.Cells[25].Controls[1];
                    TextBox txtInv_PRODAMT = (TextBox)Row.Cells[26].Controls[1];
                    TextBox txtPROD_Val = (TextBox)Row.Cells[27].Controls[1];

                    TextBox txteduCess = (TextBox)Row.Cells[28].Controls[1];
                    TextBox txteduCessCus = (TextBox)Row.Cells[29].Controls[1];
                    TextBox txtAdddutyRate = (TextBox)Row.Cells[30].Controls[1];
                    TextBox txtINVON = (TextBox)Row.Cells[31].Controls[1];
                    TextBox txtCessDuty = (TextBox)Row.Cells[32].Controls[1];

                    String jno = myTextBox1.Text;
                    String PO_no = myTextBox2.Text;
                    String PO_ItemNo = myTextBox3.Text;
                    String part_no = myTextBox4.Text;
                    String Desc = myTextBox5.Text;
                    String Qty = myTextBox6.Text;
                    String pcode = myTextBox7.Text;
                    String sno = myTextBox8.Text;
                    Double CVD = Convert.ToDouble(myTextBox9.Text);
                    String aDUTY = myTextBox10.Text;
                    String DUTY = myTextBox11.Text;
                    String IDs = myTextBox12.Text;
                    Double nvd = Convert.ToDouble(myTextBox13.Text);
                    Desc = Desc.Replace(",", " ");
                    Double eduCess = Convert.ToDouble(txteduCess.Text);
                    Double eduCess_Cus = Convert.ToDouble(txteduCessCus.Text);
                    //Double sheCESS = Convert.ToDouble(SHECESS);
                    Double addDutyPER = Convert.ToDouble(txtAdddutyRate.Text);

                    Double AssValue = Convert.ToDouble(txtASSVAL.Text);
                    if (txtCessDuty.Text == "")
                        txtCessDuty.Text = "0";
                    Double CessDuty = Convert.ToDouble(txtCessDuty.Text);


                    Double CessVal = (CVD * CessDuty / 100) * 10;

                    //Double eduCVD = totCVD * 2 / 100;
                    //Double sheCVD = totCVD * 1 / 100;

                    Double CVD_VAL = Convert.ToDouble(CVD) + CessVal;
                    Double edu_cvd = CVD_VAL * 2 / 100;
                    Double SHE_cvd = edu_cvd / 2;

                    Double cus_edu_Val = nvd + CVD + CessVal;
                    Double Cus_edu_cess = cus_edu_Val * eduCess_Cus / 100;
                    Double Cus_SHE_cess = Cus_edu_cess / 2;

                    Double total = nvd + CVD + Cus_edu_cess + Cus_SHE_cess + CessVal;

                    Double AddDUTY = (AssValue + total) * addDutyPER / 100;



                    Double totalDUTY = total + AddDUTY;



                    /* inser Query */
                    nvd = Math.Round(nvd, 1);
                    CVD = Math.Round(CVD, 1);
                    edu_cvd = Math.Round(edu_cvd, 1);
                    SHE_cvd = Math.Round(SHE_cvd, 1);

                    Cus_edu_cess = Math.Round(Cus_edu_cess, 1);
                    Cus_SHE_cess = Math.Round(Cus_SHE_cess, 1);
                    AddDUTY = Math.Round(AddDUTY, 1);
                    totalDUTY = Math.Round(totalDUTY, 1);

                    //Double ECESS=Convert.ToDouble(CVD) + Convert.ToDouble(nvd) + edu_cvd + SHE_cvd ;

                    //Double Cus_edu_cess = ECESS * 2 / 100;
                    //Double Cus_SHE_cess = Cus_edu_cess / 2;

                    string prCode = drParty.SelectedValue;
                    string jnos = drJOBNO.SelectedItem.Text;
                    try
                    {
                        SqlConnection conn1 = new SqlConnection(strconn1);
                        string lstrdrp1 = "insert into ipurchase_dtl(job_no,jobsno,party_code,prod_code,prod_sn,prod_desc,qty," +
                                          "pur_ordno,po_itemNo,model,totalCVDamt,totalDutyAmt,addlDutyamt,inv_id,nvd,edu_cvd,she_cvd,cus_edu_cess,cus_she_cess," +
                                          "prod_amt,misc_curr,misc_pers,misc_val,freight_curr,freight_pers,freight_val,insur_curr,insur_pers,insur_val,insur_on,ASS_VAL," +
                                          "BAS_DUTY,CVD_DUTY,EDU_CVD_per,SHE_CVD_per,CUS_EDU_CESS_per,CUS_SHE_CESS_per,CESS_DUTY,CESS_DUTY_PER,AddlDuty_per ) " +
                                          "values('" + jno + "','" + jnos + "','" + prCode + "','" + pcode + "','" + sno + "','" + Desc + "','" + Qty + "'," +
                                          "'" + PO_no + "','" + PO_ItemNo + "','" + part_no + "','" + CVD + "','" + totalDUTY + "','" + AddDUTY + "','" + IDs + "'," +
                                          "'" + nvd + "'," + edu_cvd + " ," + SHE_cvd + "," + Cus_edu_cess + "," + Cus_SHE_cess + " ," +
                                          "'" + txtPROD_Val.Text + "','" + txtMISCUR.Text + "','" + txtMISPER.Text + "','" + txtMISVAL.Text + "','" + txtFRTCUR.Text + "','" + txtFRTPER.Text + "','" + txtFRTVAL.Text + "'," +
                                          "'" + txtINSCUR.Text + "','" + txtINSPER.Text + "','" + txtINSVAL.Text + "','" + txtINVON.Text + "','" + txtASSVAL.Text + "'," +
                                          "'" + txtbDUTYper.Text + "','" + txtcDUTYper.Text + "','" + txteduCess.Text + "','" + SHECESS + "','" + txteduCessCus.Text + "','" + SHECESS + "'," + CessVal + ",'" + txtCessDuty.Text + "','" + txtAdddutyRate.Text + "')";

                        conn1.Open();
                        SqlDataAdapter dap1 = new SqlDataAdapter();
                        SqlCommand cmdp1 = new SqlCommand(lstrdrp1, conn1);
                        cmdp1.CommandText = lstrdrp1;
                        cmdp1.Connection = conn1;
                        dap1.SelectCommand = cmdp1;

                        int result1 = cmdp1.ExecuteNonQuery();

                        conn1.Close();
                        // this command is to use for multiple data  
                        flag = "0";
                        BtnSubmit.Enabled = false;
                    }
                    catch (Exception ex)
                    {
                        Response.Write("<script>alert('" + ex.Message + "')</script>");
                    }

                    //  BtnPreview.Enabled = true;
                }

                if (flag == "0")
                    Response.Write("<script>alert('The Given Record has been stored Successfully')</script>");

            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('" + ex.Message + "');", true);
            }
        }

        protected void BtnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("frmSEW.aspx");
        }

        protected void DGDetail1_ItemDataBound(object sender, DataGridItemEventArgs e)
        {
            try
            {
                if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
                {


                    TextBox txtJNO = (TextBox)e.Item.Cells[0].Controls[1];
                    TextBox txtPartNO = (TextBox)e.Item.Cells[4].Controls[1];
                    TextBox txtDESC = (TextBox)e.Item.Cells[5].Controls[1];
                    TextBox txtCVD = (TextBox)e.Item.Cells[9].Controls[1];
                    TextBox txtaDuty = (TextBox)e.Item.Cells[10].Controls[1];
                    TextBox txtDUTY = (TextBox)e.Item.Cells[11].Controls[1];
                    TextBox txtIDs = (TextBox)e.Item.Cells[12].Controls[1];
                    TextBox txtnvd = (TextBox)e.Item.Cells[13].Controls[1];
                    TextBox txtASSVAL = (TextBox)e.Item.Cells[14].Controls[1];
                    TextBox txtbDUTYper = (TextBox)e.Item.Cells[15].Controls[1];
                    TextBox txtcDUTYper = (TextBox)e.Item.Cells[16].Controls[1];

                    TextBox txtMISCUR = (TextBox)e.Item.Cells[17].Controls[1];
                    TextBox txtMISPER = (TextBox)e.Item.Cells[18].Controls[1];
                    TextBox txtMISVAL = (TextBox)e.Item.Cells[19].Controls[1];

                    TextBox txtFRTCUR = (TextBox)e.Item.Cells[20].Controls[1];
                    TextBox txtFRTPER = (TextBox)e.Item.Cells[21].Controls[1];
                    TextBox txtFRTVAL = (TextBox)e.Item.Cells[22].Controls[1];

                    TextBox txtINSCUR = (TextBox)e.Item.Cells[23].Controls[1];
                    TextBox txtINSPER = (TextBox)e.Item.Cells[24].Controls[1];
                    TextBox txtINSVAL = (TextBox)e.Item.Cells[25].Controls[1];
                    TextBox txtInv_PRODAMT = (TextBox)e.Item.Cells[26].Controls[1];
                    TextBox txtPROD_Val = (TextBox)e.Item.Cells[27].Controls[1];

                    string DESC = txtDESC.Text;


                    if (txtcDUTYper.Text == "" || txtcDUTYper.Text == string.Empty || txtcDUTYper.Text == "0.00" || txtcDUTYper.Text == null)
                        txtcDUTYper.Text = "0.00";

                    if (txtbDUTYper.Text == "" || txtbDUTYper.Text == string.Empty || txtbDUTYper.Text == "0.00" || txtbDUTYper.Text == null)
                        txtbDUTYper.Text = "0.00";

                    //Double CVD = Convert.ToDouble(txtCVD.Text);
                    //Double aDUTY = Convert.ToDouble(txtaDuty.Text);
                    //Double DUTY = Convert.ToDouble(txtDUTY.Text);
                    //Double IDs = Convert.ToDouble(txtIDs.Text);
                    //Double nvd = Convert.ToDouble(txtnvd.Text);
                    //Double assper;



                    //amount from iproduct details
                    //
                    Double Prod_AMT = Convert.ToDouble(txtInv_PRODAMT.Text);
                    Double prod_Val = Convert.ToDouble(txtPROD_Val.Text);

                    GetInvoiceValue(txtJNO.Text);

                    string ccInv = "misc_curnc";
                    string crInv = "mis_cRate";
                    string caInv = "misc_charg";

                    SqlConnection conn1 = new SqlConnection(strconn1);
                    string Query1 = "Select * from iworkreg where job_no ='" + txtJNO.Text + " '";
                    SqlDataAdapter da1 = new SqlDataAdapter(Query1, conn1);
                    DataSet ds1 = new DataSet();
                    da1.Fill(ds1, "iworkreg");
                    if (ds1.Tables["iworkreg"].Rows.Count != 0)
                    {
                        DataRowView row1 = ds1.Tables["iworkreg"].DefaultView[0];
                        string SFRT = row1["single_freight"].ToString();
                        if (SFRT == "0")
                        {
                            string cc = "chg_cur";
                            string cr = "chg_cRate";
                            string ca = "chg_amt";


                            string QueryMis = "Select * from iinv_chg where job_no ='" + txtJNO.Text + " ' and inv_id='" + txtIDs.Text + "'";
                            string QueryOC = "select * from iinv_dtl where job_no='" + txtJNO.Text + "' and inv_id='" + txtIDs.Text + "'";

                            SqlConnection connMis = new SqlConnection(strconn1);
                            SqlDataAdapter daMis = new SqlDataAdapter(QueryMis, connMis);
                            DataSet dsMis = new DataSet();
                            daMis.Fill(dsMis, "InvCharge");
                            if (dsMis.Tables["InvCharge"].Rows.Count == 0)
                                GetMisc(QueryOC, ccInv, crInv, caInv);
                            else
                                GetMisc(QueryMis, cc, cr, ca);

                            MIS_VAL = mis_amt / Prod_AMT * prod_Val;

                            GetFreight(QueryOC);
                            FRT_VAL = frt_amt / Prod_AMT * prod_Val;

                            GetiNSURANCE(QueryOC);
                            INS_VAL = ins_amt / Prod_AMT * prod_Val;

                        }
                        else
                        {
                            //string cc = "misc_curnc";
                            //string cr = "mis_cRate";
                            //string ca = "misc_charg";
                            string QueryChg = "Select * from iworkreg where job_no ='" + txtJNO.Text + " '";
                            GetMisc(QueryChg, ccInv, crInv, caInv);
                            MIS_VAL = mis_amt / tot_invVAL * prod_Val;

                            GetFreight(QueryChg);
                            FRT_VAL = frt_amt / tot_invVAL * prod_Val;

                            GetiNSURANCE(QueryChg);
                            INS_VAL = ins_amt / tot_invVAL * prod_Val;

                        }
                    }
                    txtMISCUR.Text = mis_cur;
                    txtMISPER.Text = mis_per.ToString("#0.00#");
                    txtMISVAL.Text = MIS_VAL.ToString("#0.00#");

                    txtFRTCUR.Text = frt_cur;
                    txtFRTPER.Text = frt_per.ToString("#0.00#");
                    txtFRTVAL.Text = FRT_VAL.ToString("#0.00#");


                    txtINSCUR.Text = ins_cur;
                    txtINSPER.Text = ins_per.ToString("#0.000#");
                    txtINSVAL.Text = INS_VAL.ToString("#0.00#");


                    /* Calculation parts for Check List preparing*/
                    SqlConnection conn = new SqlConnection(strconn1);
                    string Query = "Select * from iinv_dtl where job_no ='" + txtJNO.Text + " ' order by inv_id";
                    SqlDataAdapter da = new SqlDataAdapter(Query, conn);
                    DataSet ds = new DataSet();
                    da.Fill(ds, "IINV_DTL");
                    if (ds.Tables["IINV_DTL"].Rows.Count != 0)
                    {
                        DataRowView row = ds.Tables["IINV_DTL"].DefaultView[0];
                        string SLD = row["sldg_per"].ToString();
                        if (SLD == "" || SLD == string.Empty || SLD == null)
                            SLD = "0";
                        sldper = Convert.ToDouble(SLD);
                        ins_on = row["toi"].ToString();
                        string exRT = row["exch_Rate"].ToString();
                        eX_rate = Convert.ToDouble(exRT);
                        //exrt = Math.Round(exrt, 3);

                    }


                    // Caluculate Asseable Value
                    //Double inrVal = (prod_Val + mis_amt + frt_amt + ins_amt ) * eX_rate;
                    Double inrIns = 0;
                    Double inrmis = 0;
                    Double inrfrt = 0;
                    Double inrprod = prod_Val * eX_rate;
                    if (mis_cur == "INR")
                        inrmis = MIS_VAL;
                    else
                        inrmis = MIS_VAL * eX_rate;
                    if (frt_cur == "INR")
                        inrfrt = FRT_VAL;
                    else
                        inrfrt = FRT_VAL * eX_rate;

                    if (ins_cur == "INR")
                        inrIns = INS_VAL;
                    else
                        inrIns = INS_VAL * eX_rate;


                    //Double ASSVAL = prod_Val * eX_rate;
                    Double ASSVAL = inrprod + inrmis + inrfrt + inrIns;
                    Double assper = 0;
                    if (ins_on == "FOB")
                    {
                        assper = inrprod * sldper / 100;
                    }
                    else
                    {

                        ASSVAL = ASSVAL + ASSVAL * sldper / 100;

                    }

                    Double total = ASSVAL + assper;
                    Double OnePER = total * 1 / 100;
                    Double TotASSVAL = total + OnePER;

                    //Double ASSVAL = Convert.ToDouble(txtASSVAL.Text);
                    Double bDUTYper = Convert.ToDouble(txtbDUTYper.Text);
                    Double cDUTYper = Convert.ToDouble(txtcDUTYper.Text);





                    Double totNVD = TotASSVAL * bDUTYper / 100;
                    Double totCVD = (TotASSVAL + totNVD) * cDUTYper / 100;


                    txtASSVAL.Text = TotASSVAL.ToString("#0.00#");

                    txtnvd.Text = totNVD.ToString("#0.00#");
                    txtCVD.Text = totCVD.ToString("#0.00#");
                    sldper = 0;
                    //txtaDuty.Text = AddDUTY.ToString("#0.00#");
                }
            }
            catch (Exception ex)
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('" + ex.Message + "');", true);
            }
        }

        protected void GetMisc(string Query, string cc, string cr, string ca)
        {
            SqlConnection conn = new SqlConnection(strconn1);
            //string Query = "Select * from iworkreg where job_no ='" + jno + " ' ";
            SqlDataAdapter da = new SqlDataAdapter(Query, conn);
            DataSet ds = new DataSet();
            da.Fill(ds, "IINV_DTL");
            if (ds.Tables["IINV_DTL"].Rows.Count != 0)
            {
                DataRowView row = ds.Tables["IINV_DTL"].DefaultView[0];
                mis_cur = row["" + cc + ""].ToString();
                mis_per = Convert.ToDouble(row["" + cr + ""].ToString());
                mis_amt = Convert.ToDouble(row["" + ca + ""].ToString());


            }
            else
            {

                mis_cur = "";
                mis_per = 0;
                mis_amt = 0;
            }
        }

        protected void GetFreight(string Query)
        {
            SqlConnection conn = new SqlConnection(strconn1);
            //string Query = "Select * from iworkreg where job_no ='" + jno + " '";
            SqlDataAdapter da = new SqlDataAdapter(Query, conn);
            DataSet ds = new DataSet();
            da.Fill(ds, "IINV_DTL");
            if (ds.Tables["IINV_DTL"].Rows.Count != 0)
            {

                DataRowView row = ds.Tables["IINV_DTL"].DefaultView[0];
                frt_cur = row["fre_cur"].ToString();
                frt_per = Convert.ToDouble(row["fre_perce"].ToString());
                frt_amt = Convert.ToDouble(row["freight"].ToString());




            }
            else
            {
                frt_cur = "";
                frt_per = 0;
                frt_amt = 0;


            }
        }

        protected void GetiNSURANCE(string Query)
        {
            SqlConnection conn = new SqlConnection(strconn1);
            //string Query = "Select * from iworkreg where job_no ='" + jno + " ' ";
            SqlDataAdapter da = new SqlDataAdapter(Query, conn);
            DataSet ds = new DataSet();
            da.Fill(ds, "IINV_DTL");
            if (ds.Tables["IINV_DTL"].Rows.Count != 0)
            {

                DataRowView row = ds.Tables["IINV_DTL"].DefaultView[0];


                ins_cur = row["ins_cur"].ToString();
                ins_per = Convert.ToDouble(row["ins_perce"].ToString());
                ins_amt = Convert.ToDouble(row["insurance"].ToString());


            }
            else
            {

                ins_cur = "";
                ins_per = 0;
                ins_amt = 0;
            }
        }

        protected void GetInvoiceValue(string jno)
        {
            SqlConnection conn = new SqlConnection(strconn1);
            string Query = "select sum(prod_value) invVAl from iinv_dtl where job_no ='" + jno + " ' ";
            SqlDataAdapter da = new SqlDataAdapter(Query, conn);
            DataSet ds = new DataSet();
            da.Fill(ds, "IINV_DTL");
            if (ds.Tables["IINV_DTL"].Rows.Count != 0)
            {
                DataRowView row = ds.Tables["IINV_DTL"].DefaultView[0];
                tot_invVAL = Convert.ToDouble(row["invVAl"].ToString());


            }

        }

        protected void drParty_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (drParty.SelectedItem.Selected == true)
            {
                try
                {
                    //Get JobNo for the customers
                    GetPartyJOBNO();
                }
                catch (Exception ex)
                {
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('" + ex.Message + "');", true);
                }
            }
        }

    }
}