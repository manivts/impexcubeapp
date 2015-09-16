using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using System.Data.Odbc;
using MySql;
using MySql.Data.MySqlClient;
using VTS.ImpexCube.Data;

namespace AccountsManagement
{
    public partial class Sync : System.Web.UI.Page
    {
        string strconn = (string)ConfigurationManager.ConnectionStrings["Constr"].ConnectionString;
        string strconnmysql = (string)ConfigurationManager.ConnectionStrings["ConnectionJSU"].ConnectionString;
        CommonDL objCommonDL = new CommonDL();
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        protected void btnCharge_Click(object sender, EventArgs e)
        {
            int i = 0;
            string sqlQuery = "Select Distinct charge_desc from View_CustomerAndCharges Where VGUID is not null And CCCompleted=0 And charge_desc is Not Null";
            SqlConnection conn = new SqlConnection(strconn);
            conn.Open();
            SqlDataAdapter da = new SqlDataAdapter(sqlQuery, conn);
            DataSet ds = new DataSet();
            da.Fill(ds, "SQLTABLE");
            conn.Close();
            DataTable dt = ds.Tables["SQLTABLE"];
            foreach (DataRow row in dt.Rows)
            {
                string chargdesc = row["charge_desc"].ToString();
                string insertMaster = "insert into M_AccountMaster(AccountCode,AccountName,Acc_Group,CreatedBy,CreatedDate) values ('" + chargdesc + "','" + chargdesc + "','Sales Accounts','1','0','1','0','" + (string)Session["UserName"] + "','" + DateTime.Now + "') ";
                string insertDetails = "insert into M_AccountDetails(AccountCode,AccountName,Acc_Group,CostCenter,BillWiseOn,Approved,Completed,CreatedBy,CreatedDate) values ('" + chargdesc + "','Sales Accounts','1','0','1','0','" + (string)Session["UserName"] + "','" + DateTime.Now + "') ";

                GetCommand(insertMaster);
                i++;
            }
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Charges Details has been Successfully Inserted');", true);
        }

        private void Updatesno(string keyname, string keycode)
        {
            string AutoQuery = "update M_AutoGenerate set keycode='" + keycode + "' where Keyname='" + keyname + "'";
            objCommonDL.ExecuteNonQuery(AutoQuery);
        }
        private void GetCommand(string sqlQuery)
        {
            try
            {

                SqlConnection conn = new SqlConnection(strconn);
                conn.Open();
                SqlCommand cmd = new SqlCommand(sqlQuery, conn);
                SqlDataAdapter da = new SqlDataAdapter();
                cmd.CommandText = sqlQuery;
                cmd.Connection = conn;
                da.SelectCommand = cmd;
                int result = cmd.ExecuteNonQuery();
                conn.Close();
            }
            catch
            {
            }
        }
                
        protected void btnParty_Click(object sender, EventArgs e)
        {
            int i = 0;
            string sqlQuery = "Select Distinct CompName from View_CustomerAndCharges Where VGUID is not null And CCCompleted=0";
            SqlConnection conn = new SqlConnection(strconn);
            conn.Open();
            SqlDataAdapter da = new SqlDataAdapter(sqlQuery, conn);
            DataSet ds = new DataSet();
            da.Fill(ds, "SQLTABLE");
            conn.Close();
            DataTable dt = ds.Tables["SQLTABLE"];
            foreach (DataRow row in dt.Rows)
            {
                string chargdesc = row["CompName"].ToString();
                string shname = chargdesc;
                string[] shn = shname.Split(' ');
                shname = shn[0];
                string insert = "insert into AccountMaster(AccountCode,AccountName,ShortName,Acc_Group,CostCenter,BillWiseOn,Approved,Completed,CreatedBy,CreatedDate) values ('" + chargdesc + "','" + chargdesc + "','" + shname + "','Sundry Debtors','0','1','1','0','" + (string)Session["UserName"] + "','" + DateTime.Now + "') ";
                GetCommand(insert);
                i++;
            }
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Customer Details has been Successfully Inserted');", true);
        }

        protected void ButtonJobNo_Click(object sender, EventArgs e)
        {
            //DateTime from=Convert.ToDateTime("1-1-2012");
            //DateTime to=Convert.ToDateTime("10-5-2013");
          MySqlConnection con=new MySqlConnection(strconnmysql);
            con.Open();
            string Query = "Select I.JOB_NO,I.PARTY_CODE,J.PARTY_NAME from iworkreg I JOIN prt_mast J ON I.PARTY_CODE=J.PARTY_CODE"; //where I.DOC_RECD between '"+ from +"' and '"+ to  +"' ";
            MySqlDataAdapter sd=new MySqlDataAdapter(Query,con);
            DataSet ds=new DataSet();
            sd.Fill(ds,"data");
            DataTable dt = ds.Tables["data"];
            con.Close();
           int i=0;
            foreach(DataRow row in dt.Rows)
            {
                string JOB_NO = row["JOB_NO"].ToString();
               
                string PARTY_CODE = row["PARTY_CODE"].ToString();
                string PARTY_NAME = row["PARTY_NAME"].ToString();
                string insert = "insert into JobNo_CostCenter(JobNo,PartyName,PartyCode,Approved,Completed,CreatedBy,CreatedDate,ModifiedBy,ModifiedDate) values ('" + JOB_NO + "','" + PARTY_CODE + "','"+PARTY_NAME+"','0','0','" + (string)Session["UserName"] + "','" + DateTime.Now + "','" + (string)Session["UserName"] + "','" + DateTime.Now + "') ";
                GetCommand(insert);
                i++;
            }
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "alert('Job Details has been Successfully Inserted')", true);
        }
    }
}