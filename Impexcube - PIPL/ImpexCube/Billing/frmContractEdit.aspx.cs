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
using MySql;
using MySql.Data.MySqlClient;
using System.Globalization;

public partial class frmContractEdit : System.Web.UI.Page
{
    string strPIPL = (string)ConfigurationManager.AppSettings["ConnectionImpex"];
    string strconn1 = (string)ConfigurationManager.AppSettings["ConnectionVisual"];
    string strconn = (string)ConfigurationManager.AppSettings["ConnectionImpex"];
    #region
    string sqlQuery = "";
    string ssStr;
    string EndStr;
    string brk = "";

    #endregion
    protected void Page_Load(object sender, EventArgs e)
    {
        string Query = "";
        //   string status = "In Active";
        if (IsPostBack == false)
        {
            //lblUser.Text =(string)Session["USER-NAME"];
            //lblDate.Text = System.DateTime.Now.ToString("dd-MMM-yyyy");
            //lblTime.Text = DateTime.Now.ToLongTimeString();
            //if (lblUser.Text == "")
            //{
            //    Response.Redirect("~/PIPL.aspx");
            //}
            txtContrName.Visible = false;
            // Label18.Visible = false;
            tbHeader.Visible = false;
            //if ((string)Session["PAGE"] != "")
            //{
            //    string Message = Session["PAGE"].ToString();
            //    Response.Write("<script>alert('" + Message + "')</script>");
            //}
            if ((string)Session["CONTRACT"] == "Renewal")
            {
                Label1.Text = "Renewal Contract Information for";
                Query = "select distinct customer_name from contract_mst";
                tblRenewal.Visible = true;
                txtApproved.ReadOnly = txtFrom.ReadOnly = txtTo.ReadOnly = true;
                myNewRowGridView.Enabled = false;
            }
            else
            {
                Query = "select distinct customer_name from contract_mst";
                tblRenewal.Visible = false;
            }
            drCustomer.DataSource = GetData(Query);
            drCustomer.DataTextField = "customer_name";
            drCustomer.DataValueField = "customer_name";
            drCustomer.DataBind();
            drCustomer.Items.Insert(0, new ListItem("~select~", "0"));
            //  newRow.Visible = false;
            Session["flag"] = "";

            gridID.Visible = false;
            btnID.Visible = false;

            SqlConnection conn = new SqlConnection(strPIPL);
            string sqlQuery = "select  * from AppDetails";
            SqlDataAdapter da = new SqlDataAdapter(sqlQuery, conn);
            DataSet ds = new DataSet();
            da.Fill(ds, "name");

            if (ds.Tables["name"].Rows.Count != 0)
            {
                DataRowView row = ds.Tables["name"].DefaultView[0];
                lblShortName.Text = row["ShortName"].ToString();
                //lblshortname1.Text = row["ShortName"].ToString();
                //lblshortname2.Text = row["ShortName"].ToString();
            }


        }

        //if (brk == "yes")
        //{
        //    BtnSubmit.Attributes.Add("onclick", "return validate()");
        //}
        // BtnSubmit.Attributes.Add("onclick", "return javascript:validate();");

        //  GetExpiryContract();
    }


    public DataSet GetData(string Query)
    {
        SqlConnection conn = new SqlConnection(strconn);

        SqlDataAdapter da = new SqlDataAdapter(Query, conn);
        DataSet ds = new DataSet();
        da.Fill(ds, "table");
        return ds;
    }
    public DataSet GetCharge()
    {
        SqlConnection conn = new SqlConnection(strconn);
        string Query = "select * from charge_mst where cCode is null";
        SqlDataAdapter da = new SqlDataAdapter(Query, conn);
        DataSet ds = new DataSet();
        da.Fill(ds, "table");
        return ds;
    }
    protected void GetXMLADD()
    {


        foreach (GridViewRow row in myNewRowGridView.Rows)
        {

            DropDownList ddlCharge = (DropDownList)row.FindControl("drCharge");

            DropDownList ddlProduct = (DropDownList)row.FindControl("drProduct");
            DropDownList ddlUnit = (DropDownList)row.FindControl("drUnit");

            DataSet dsProduct = new DataSet();
            DataSet dsUnit = new DataSet();

            ddlCharge.DataSource = GetCharge();
            ddlCharge.DataTextField = "charge_desc";
            ddlCharge.DataValueField = "charge_desc";
            ddlCharge.DataBind();

            dsProduct.ReadXml(Server.MapPath("XML\\product.xml"));
            {
                ddlProduct.DataSource = dsProduct;
                ddlProduct.DataMember = "Detail";
                ddlProduct.DataTextField = "t1";
                ddlProduct.DataValueField = "t1";
                ddlProduct.DataBind();
            }

            dsUnit.ReadXml(Server.MapPath("XML\\units.xml"));
            {
                ddlUnit.DataSource = dsUnit;
                ddlUnit.DataMember = "Detail";
                ddlUnit.DataTextField = "t1";
                ddlUnit.DataValueField = "t1";
                ddlUnit.DataBind();
            }
        }


    }
    protected void BtnSubmit_Click(object sender, EventArgs e)
    {
        string cCode = drContract.SelectedValue;
        string cName = drCustomer.SelectedValue;
        string approved = txtApproved.Text;
        string FD = txtFrom.Text;
        string fmm = FD.Substring(3, 2);
        string fdd = FD.Substring(0, 2);
        string fyy = FD.Substring(6, 4);
        string FDate = fyy + "-" + fmm + "-" + fdd;
        string TD = txtTo.Text;
        string tmm = TD.Substring(3, 2);
        string tdd = TD.Substring(0, 2);
        string tyy = TD.Substring(6, 4);
        string TDate = tyy + "-" + tmm + "-" + tdd;
        //string FDate = txtFrom.Text;
        //string TDate = txtTo.Text;
        string entry_Date = System.DateTime.Now.ToShortDateString();
        string status = drStatus.SelectedValue;
        string remark = "Renewal Contract";
        string ContrName = txtContrName.Text;
        //   string message = "";
        int nwRow = myNewRowGridView.NewRowCount;
        int oldRow = myNewRowGridView.Rows.Count;
        if ((string)Session["CONTRACT"] == "Edit")
        {
            UpdateGridview();
            InsertNewGridViewRow();

            if (brk == "yes")
            {
                BtnSubmit.Attributes.Add("onclick", "return validate()");
                //  Label18.Visible = false;
                BtnSubmit.Enabled = true;
                //     message = " Contract details has been Updated on ";

                // break;

            }
            else
            {
                BtnSubmit.Enabled = false;
            }
            Hide();
            Session["Message"] = "Contract Information has been Updated Successfully ";
            Response.Redirect("~/frmMessage.aspx", false);
        }
        else
        {
            approved = txtRenewalBy.Text;
            FD = txtRenFrom.Text;
            TD = txtRenTo.Text;
            if (approved != "" && FD != "" && TD != "")
            {
                fmm = FD.Substring(3, 2);
                fdd = FD.Substring(0, 2);
                fyy = FD.Substring(6, 4);
                FDate = fyy + "-" + fmm + "-" + fdd;



                tmm = TD.Substring(3, 2);
                tdd = TD.Substring(0, 2);
                tyy = TD.Substring(6, 4);
                TDate = tyy + "-" + tmm + "-" + tdd;

                sqlQuery = "update contract_mst set contr_name='" + ContrName + "',approved_by='" + approved + "',contr_valid_from='" + FDate + "'," +
                           "contr_valid_to='" + TDate + "',entry_date='" + entry_Date + "',contr_status='" + status + "' " +
                           "where contr_code='" + cCode + "' and customer_name='" + cName + "'";
                GetCommand(sqlQuery);

                sqlQuery = "insert into contract_mst_head(contr_code,contr_name,customer_name,approved_by," +
                           "contr_valid_from,contr_valid_to,entry_date,contr_status,remarks) values('" + cCode + "'," +
                           "'" + ContrName + "','" + cName + "','" + approved + "','" + FDate + "','" + TDate + "'," +
                           "'" + entry_Date + "','" + status + "','" + remark + "')";
                GetCommand(sqlQuery);
                //  message = " Contract Renewal has been Updated on ";
                Hide();
                Session["Message"] = "Contract Information has been Renewal Successfully ";
                Response.Redirect("~/frmMessage.aspx", false);
            }
            else
                Response.Write("<script>alert('Please Give Renewal Information')</script>");

        }
        // BtnSubmit.Enabled = false;

        //string dates = System.DateTime.Now.ToShortDateString();
        //Session["PAGE"] = cName + message + dates;
        //Response.Redirect("frmContractEdit.aspx");
    }
    protected void Hide()
    {
        // drContract.SelectedValue = "0";
        // drCustomer.SelectedValue = "0";
        myNewRowGridView.Visible = false;
        drStatus.SelectedValue = "0";
        txtApproved.Text = "";
        txtFrom.Text = "";
        txtTo.Text = "";
        txtContrName.Text = "";
        Response.Write("<script>alert('Contract Information has been Updated Successfully')</script>");
    }
    protected void UpdateGridview()
    {
        int sno = 1;
        string cCode = drContract.SelectedValue;
        string cName = drCustomer.SelectedValue;
        foreach (GridViewRow row in myNewRowGridView.Rows)
        {
            DropDownList ddlCharge = (DropDownList)row.FindControl("drCharge");
            DropDownList ddlProduct = (DropDownList)row.FindControl("drProduct");
            DropDownList ddlUnit = (DropDownList)row.FindControl("drUnit");

            TextBox txtAir = (TextBox)row.FindControl("txtAIR");
            TextBox txtBB = (TextBox)row.FindControl("txtBB");
            TextBox txtLCL = (TextBox)row.FindControl("txtLCL");
            TextBox txt20 = (TextBox)row.FindControl("txt20");
            TextBox txt40 = (TextBox)row.FindControl("txt40");

            CheckBox chksb = (CheckBox)row.FindControl("chkSB");
            CheckBox chkdb = (CheckBox)row.FindControl("chkDB");

            Label lsno = (Label)row.FindControl("lblsno");

            string des = ddlCharge.SelectedValue;

            string product = ddlProduct.SelectedValue;
            string strUnit = ddlUnit.SelectedValue;

            string AIR = txtAir.Text;
            string BB = txtBB.Text;
            string LCL = txtLCL.Text;
            string T20 = txt20.Text;
            string T40 = txt40.Text;

            string sb = "";
            if (chksb.Checked)
                sb = "YES";
            else
                sb = "NO";
            string db = "";
            if (chkdb.Checked)
                db = "YES";
            else
                db = "NO";

            if (AIR == "")
                AIR = "null";
            if (BB == "")
                BB = "null";
            if (LCL == "")
                LCL = "null";
            if (T20 == "")
                T20 = "null";
            if (T40 == "")
                T40 = "null";
            if (des != "0")
            {

                sqlQuery = "update contract_dtl set charge_desc='" + des + "',product='" + product + "',unit='" + strUnit + "'," +
                           "AIR=" + AIR + ",break_bulk=" + BB + ",LCL=" + LCL + ",ft20=" + T20 + ",ft40=" + T40 + ",sb='" + sb + "',db='" + db + "' " +
                           "where contr_code='" + cCode + "' and sno=" + sno + "";
                GetCommand(sqlQuery);
                sno = sno + 1;


            }



        }
    }
    protected void InsertNewGridViewRow()
    {
        // int sno = 1;
        int nwRow = myNewRowGridView.NewRowCount;
        int oldRow = myNewRowGridView.Rows.Count;
        string cCode = drContract.SelectedValue;
        string cName = drCustomer.SelectedValue;
        //int nnrr = myNewRowGridView.
        foreach (GridViewRow row in myNewRowGridView.NewRows)
        {
            DropDownList ddlCharge = (DropDownList)row.FindControl("drCharge");
            DropDownList ddlProduct = (DropDownList)row.FindControl("drProduct");
            DropDownList ddlUnit = (DropDownList)row.FindControl("drUnit");

            TextBox txtAir = (TextBox)row.FindControl("txtAIR");
            TextBox txtBB = (TextBox)row.FindControl("txtBB");
            TextBox txtLCL = (TextBox)row.FindControl("txtLCL");
            TextBox txt20 = (TextBox)row.FindControl("txt20");
            TextBox txt40 = (TextBox)row.FindControl("txt40");

            CheckBox chksb = (CheckBox)row.FindControl("chkSB");
            CheckBox chkdb = (CheckBox)row.FindControl("chkDB");

            Label lsno = (Label)row.FindControl("lblsno");
            string sno = lsno.Text;
            string des = ddlCharge.SelectedValue;
            if (des == "0")
            {
                brk = "yes";
                //BtnSubmit.Attributes.Add("onclick", "return javascript:validate();");
                // BtnSubmit.Attributes.Add("onclick", "return validate()");
                break;
            }


            string product = ddlProduct.SelectedValue;
            string strUnit = ddlUnit.SelectedValue;

            string AIR = txtAir.Text;
            string BB = txtBB.Text;
            string LCL = txtLCL.Text;
            string T20 = txt20.Text;
            string T40 = txt40.Text;

            string sb = "";
            if (chksb.Checked)
                sb = "YES";
            else
                sb = "NO";
            string db = "";
            if (chkdb.Checked)
                db = "YES";
            else
                db = "NO";

            if (AIR == "")
                AIR = "null";
            if (BB == "")
                BB = "null";
            if (LCL == "")
                LCL = "null";
            if (T20 == "")
                T20 = "null";
            if (T40 == "")
                T40 = "null";
            if (des != "0")
            {


                oldRow = oldRow + 1;

                string newQuery = "insert into contract_dtl(contr_code,sno,charge_desc,product,unit,AIR,break_bulk,LCL,ft20,ft40,SB,DB) " +
                            "values('" + cCode + "'," + oldRow + ",'" + des + "','" + product + "','" + strUnit + "'," + AIR + "," + BB + "," + LCL + "," + T20 + "," + T40 + ",'" + sb + "','" + db + "')";

                GetCommand(newQuery);

            }
            else
                break;

        }
    }

    protected void GetCustomerCode()
    {

        string Code = drCustomer.SelectedValue;

        string[] str = null;
        str = Code.Split(' ');
        Int32 i = 0;

        for (i = 0; i <= str.Length - 1; i++)
        {
            if (str.Length - 1 == str.Length - 1)
            {
                ssStr += str[i].Substring(0, 1);
                EndStr = str[i].Remove(0, 1);
            }

        }


        string strComp = ssStr + EndStr;
        string strUpp = strComp.ToUpper();
        string strPCode = strUpp.Substring(0, 4);

        SqlConnection conn = new SqlConnection(strconn);
        string Query = "select * from contract_dtl where contr_code like '" + strPCode + "%'";
        SqlDataAdapter da = new SqlDataAdapter(Query, conn);
        DataSet ds = new DataSet();
        da.Fill(ds, "table");
        if (ds.Tables["table"].Rows.Count == 0)
            Session["strPCode"] = strPCode + "001";
        else
        {
            string sql = "select max(substring(contr_code,5,3))+ 1 as cnt from contract_mst";
            SqlDataAdapter da1 = new SqlDataAdapter(sql, conn);
            DataSet ds1 = new DataSet();
            da1.Fill(ds1, "MaxCCode");
            DataRowView row = ds1.Tables["MaxCCode"].DefaultView[0];
            int code = Convert.ToInt32(row["cnt"].ToString());
            if (code < 10)
                Session["strPCode"] = strPCode + "00" + code;
            else
                Session["strPCode"] = strPCode + "0" + code;
        }
        // Response.Write("<script>alert('Party Name is : " + Code + "  Code : " + strPCode + "')</script>");

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

    protected void Button1_Click(object sender, EventArgs e)
    {
        //Session.Abandon();
        //Session.Clear();
        //Response.Write("<script>window.close();</script>");
    }
    protected void BtnSearch_Click(object sender, EventArgs e)
    {
        BtnSubmit.Enabled = true;
        Session["Message"] = "";
        string CName = drCustomer.SelectedValue;
        string CID = drContract.SelectedValue;
        Session["CCode"] = CID;
        SqlConnection conn = new SqlConnection(strconn);
        string Query1 = "select * from contract_mst m,contract_dtl s " +
                     "where m.contr_code=s.contr_code and m.customer_name='" + CName + "' and  " +
                     "m.contr_code='" + CID + "' order by s.sno";
        SqlDataAdapter da = new SqlDataAdapter(Query1, conn);
        DataSet ds = new DataSet();
        da.Fill(ds, "table");
        DataTable dt = ds.Tables[0];
        if (ds.Tables["table"].Rows.Count != 0)
        {
            // newRow.Visible = true;
            DataRowView row = ds.Tables["table"].DefaultView[0];
            string ContrName = row["contr_name"].ToString();
            string approved = row["approved_by"].ToString();
            string cv_from = row["contr_valid_from"].ToString();

            string cv_to = row["contr_valid_to"].ToString();
            string status = row["contr_status"].ToString();

            DateTime CVFROM = Convert.ToDateTime(cv_from);
            DateTime CVTO = Convert.ToDateTime(cv_to);

            txtContrName.Text = ContrName;
            txtApproved.Text = approved;
            txtFrom.Text = CVFROM.ToString("dd/MM/yyyy");
            txtTo.Text = CVTO.ToString("dd/MM/yyyy");
            drStatus.SelectedValue = status;
            myNewRowGridView.Visible = true;
            gridID.Visible = true;
            btnID.Visible = true;
        }
        else
            Response.Write("<script>" + "alert('Record Not Found for Given Contract ');" + "</script>");

        myNewRowGridView.DataSource = dt;
        myNewRowGridView.DataBind();
        GetXMLADD();
        GetContractValueADD(CID, CName);
        NewDRXML();
    }

    protected void GetContractValueADD(string CID, string CName)
    {
        foreach (GridViewRow row1 in myNewRowGridView.Rows)
        {
            CheckBox chkSB = (CheckBox)row1.FindControl("chkSB");
            CheckBox chkDB = (CheckBox)row1.FindControl("chkDB");
            Label lblsno = (Label)row1.FindControl("lblsno");

            DropDownList ddlCharge = (DropDownList)row1.FindControl("drCharge");

            DropDownList ddlProduct = (DropDownList)row1.FindControl("drProduct");
            DropDownList ddlUnit = (DropDownList)row1.FindControl("drUnit");


            string sno = lblsno.Text;
            //string product=row1.Cells[1].Text;

            string Query1 = "select * from contract_mst m,contract_dtl s " +
                         "where m.contr_code=s.contr_code and m.customer_name='" + CName + "' and  " +
                         "s.contr_code='" + CID + "' and s.sno='" + sno + "'";
            SqlConnection conn = new SqlConnection(strconn);
            SqlDataAdapter da = new SqlDataAdapter(Query1, conn);
            DataSet ds = new DataSet();
            da.Fill(ds, "table");
            if (ds.Tables["table"].Rows.Count != 0)
            {
                DataRowView row = ds.Tables["table"].DefaultView[0];
                string sb = row["SB"].ToString();
                string db = row["DB"].ToString();
                string chargeDesc = row["charge_desc"].ToString();
                string product = row["product"].ToString();
                string units = row["unit"].ToString();
                if (sb == "YES")
                    chkSB.Checked = true;
                else
                    chkSB.Checked = false;

                if (db == "YES")
                    chkDB.Checked = true;
                else
                    chkDB.Checked = false;
                ddlProduct.SelectedValue = product;
                ddlUnit.SelectedValue = units;
                ddlCharge.SelectedValue = chargeDesc;

            }

        }

    }
    protected void NewDRXML()
    {

        int newr = myNewRowGridView.NewRowCount;
        int rc = myNewRowGridView.Rows.Count;
        int inccrow = newr + rc;



        foreach (GridViewRow row1 in myNewRowGridView.NewRows)
        {
            CheckBox chkSB = (CheckBox)row1.FindControl("chkSB");
            CheckBox chkDB = (CheckBox)row1.FindControl("chkDB");
            // Label lblsno = (Label)row1.FindControl("lblsno");

            DropDownList ddlCharge = (DropDownList)row1.FindControl("drCharge");

            DropDownList ddlProduct = (DropDownList)row1.FindControl("drProduct");
            DropDownList ddlUnit = (DropDownList)row1.FindControl("drUnit");

            DataSet ds = new DataSet();
            DataSet ds1 = new DataSet();

            ddlCharge.DataSource = GetCharge();
            ddlCharge.DataTextField = "charge_desc";
            ddlCharge.DataValueField = "charge_desc";
            ddlCharge.DataBind();
            ddlCharge.Items.Insert(0, new ListItem("", "0"));

            ds.ReadXml(Server.MapPath("XML\\Product.xml"));
            {

                ddlProduct.DataSource = ds;
                ddlProduct.DataMember = "detail";
                ddlProduct.DataTextField = "t1";
                ddlProduct.DataValueField = "t1";
                ddlProduct.DataBind();
            }
            ds1.ReadXml(Server.MapPath("XML\\units.xml"));
            {

                ddlUnit.DataSource = ds1;
                ddlUnit.DataMember = "detail";
                ddlUnit.DataTextField = "t1";
                ddlUnit.DataValueField = "t1";
                ddlUnit.DataBind();
            }
        }
    }
    protected void drCustomer_SelectedIndexChanged(object sender, EventArgs e)
    {
        string CName = drCustomer.SelectedValue;
        string Query = "";
        // string status="In Active";
        if ((string)Session["CONTRACT"] == "Edit")
            Query = "select * from contract_mst where customer_name='" + CName + "'";
        else
            Query = "select * from contract_mst where customer_name='" + CName + "' ";
        drContract.DataSource = GetData(Query);
        drContract.DataTextField = "contr_name";
        drContract.DataValueField = "contr_code";
        drContract.DataBind();
    }



}
