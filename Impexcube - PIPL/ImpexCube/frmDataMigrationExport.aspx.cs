using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using MySql;
using MySql.Data.MySqlClient;


namespace ImpexCube
{
    
    public partial class frmDataMigrationExport : System.Web.UI.Page
    {
        string strcon = (string)ConfigurationManager.ConnectionStrings["Constr"].ConnectionString;
        string strMycon = ConfigurationManager.AppSettings["ConnectionVisual"];
        string jobno = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            string fyear = (string)Session["FinancialYear"];
        }

        public DataSet GetSqlData(string query)
        {
            SqlConnection con = new SqlConnection(strcon);
            con.Open();
            SqlDataAdapter da = new SqlDataAdapter(query, con);
            DataSet ds = new DataSet();
            da.Fill(ds, "Data");
            con.Close();
            return ds;
        }

        public DataSet GetMyData(string query)
        {
            MySqlConnection con = new MySqlConnection(strMycon);
            con.Open();
            MySqlDataAdapter da = new MySqlDataAdapter(query, con);
            DataSet ds = new DataSet();
            da.Fill(ds, "MyData");
            con.Close();
            return ds;
        }

        public int InsertData(string query)
        {
            SqlConnection con = new SqlConnection(strcon);
            con.Open();
            SqlCommand cmd = new SqlCommand(query, con);
            int result=cmd.ExecuteNonQuery();
            con.Close();
            return result;
        
        }

        protected void btnJobSync_Click(object sender, EventArgs e)
        {
            //if (txtJobNo.Text != "")
            //{
            string fyear = (string)Session["FinancialYear"];
            lblmsg.Text = "";
            jobno = txtJobNo.Text;
            string NjobNo = "EXP/" + jobno + "/" + ddlFyear.SelectedValue;//+ fyear;
            lblJobNo.Text = "Job No : " + NjobNo;
            Session["ExpJob"] = NjobNo;
            string query = "select * from eworkreg where Job_No='" + (string)Session["ExpJob"] + "' ";
                DataSet ds = GetMyData(query);
                if ((ds.Tables["MyData"].Rows.Count != 0))// && (jobno.Length==5))
                {
                    jobno = txtJobNo.Text;
                    //string NjobNo = "EXP/" + jobno +"/"+ fyear;
                    string[] JNO = NjobNo.Split('/');
                    jobno = JNO[1];
                    DeleteJob();
                    JobCreation();
                    Shipment();
                    ContainerDetails();
                    PartyDetails();
                    //ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Successfully Saved');", true);
                    lblmsg.Text="Successfully Saved";
                }
                else
                {
                    //ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Please enter the Correct Job No');", true);
                    lblmsg.Text = "Please enter the Correct Job No";
                }
           // }
            //else
            //{
            //    ScriptManager.RegisterStartupScript(this, GetType(), "alert", "alert('Please enter the Job No');", true);
            //}
            
        }

        public void DeleteJob()
        {
            string query = "Exec RemoveExportJobDetails '" + jobno + "' ";
            InsertData(query);
        }

        public void JobCreation()
        {
            try
            {
                string query = "select * from eworkreg where Job_No='" + (string)Session["ExpJob"] + "' ";
                DataSet ds = GetMyData(query);
                if (ds.Tables["MyData"].Rows.Count != 0)
                {
                    DataRowView row = ds.Tables["MyData"].DefaultView[0];
                    string JobNo = row["Job_No"].ToString();
                    string JobReceDate = row["DOC_RECD"].ToString();
                    string TransMode = row["Transport_mode"].ToString();
                    string InvoiceDetail = row["INV_DTL"].ToString();
                    string AssVal = row["FOB_VAL"].ToString();
                    string TotalDuty = row["tot_duty"].ToString();
                    Session["PartyCode"] = row["party_code"].ToString();

                    string insert = "insert into E_M_JobCreation (JobNo,JobDate,TransportMode,InvoiceDetail,AssVal,TotalDuty) values " +
                        " ('" + jobno + "','" + JobReceDate + "','" + TransMode + "','" + InvoiceDetail + "','" + AssVal + "','" + TotalDuty + "')";
                    int result = InsertData(insert);
                }
            }
            catch (Exception ex)
            {
                Response.Write("Error in JobCreations"+ex.Message);
            }
        }

        public void Shipment()
        {
            try
            {
                string query = "select * from eshipdtl where Job_No='" + (string)Session["ExpJob"] + "' ";
                DataSet ds = GetMyData(query);
                if (ds.Tables["MyData"].Rows.Count != 0)
                {
                    DataRowView row = ds.Tables["MyData"].DefaultView[0];
                    string JobNo = row["Job_No"].ToString();
                    string bl = row["bl_no"].ToString();
                    string BLDate = row["bl_dt"].ToString();
                    if (BLDate != "" && BLDate != null)
                    {
                        BLDate = Convert.ToDateTime(row["bl_dt"]).ToString("dd/MM/yyyy");
                    }

                    string hbl = row["hbl_no"].ToString();
                    string HBLDate = row["hbl_date"].ToString();
                    if (HBLDate != "" && HBLDate != null)
                    {
                        HBLDate = Convert.ToDateTime(row["hbl_date"]).ToString("dd/MM/yyyy");
                    }
                    string pkg = row["no_of_pkg"].ToString();
                    string pkg_unit = row["pkg_unit"].ToString();
                    string gross = row["gr_wt"].ToString();
                    string gross_unit = row["gr_unit"].ToString();

                    string insert = "insert into E_T_Shipment (JobNo,MBLNo,MBLDate,HBLNo,HBLDate,TotalNoofPkgs,TotalNoofPkgsUnit,GrossWeight,GrossWeightUnit) values " +
                        " ('" + jobno + "','" + bl + "','" + BLDate + "','" + hbl + "','" + HBLDate + "','" + pkg + "','" + pkg_unit + "','" + gross + "','" + gross_unit + "')";
                    int result = InsertData(insert);
                }
            }
            catch (Exception ex)
            {
                Response.Write("Error in Shipment" + ex.Message);
            }
        }

        public void ContainerDetails()
        {
            try
            {
                string query = "select * from expcontdtl where Job_No='" + (string)Session["ExpJob"] + "' order by sr_no ";
                DataSet ds = GetMyData(query);
                if (ds.Tables["MyData"].Rows.Count != 0)
                {
                    DataRowView row = ds.Tables["MyData"].DefaultView[0];
                    DataTable dt3 = ds.Tables[0];
                    string cno = "";
                    string snos = "";
                    string cTyp = "";
                    string cSize = "";
                    foreach (DataRow row3 in dt3.Rows)
                    {
                        snos = row3["sr_no"].ToString();
                        cno = row3["cont_no"].ToString();
                        cTyp = row3["cont_type"].ToString();
                        cSize = row3["cont_size"].ToString();
                        string insert = "insert into E_T_Container (JobNo,ContainerNo,Type,size) values ('"+jobno+"','" + cno + "','" + cTyp + "','" + cSize + "')";
                        int result = InsertData(insert);
                    }
                }
            }
            catch (Exception ex)
            {
                Response.Write("Error in ContainerDetails" + ex.Message);
            }
        }

        public void PartyDetails()
        {
            try
            {
                string query = "select *  from prt_mast m,prt_addr a " +
                                   "where m.party_code=a.party_code and  m.party_code='" + (string)Session["PartyCode"] + "'";
                DataSet ds = GetMyData(query);
                if (ds.Tables["MyData"].Rows.Count != 0)
                {
                    DataRowView row = ds.Tables["MyData"].DefaultView[0];
                    string cCode = row["group_id"].ToString();
                    //Session["cCode"] = cCode;
                    //if (cCode == "")
                    //{
                        string PartyName = row["party_name"].ToString();
                        string addr = row["address"].ToString();
                        addr = addr.Replace("'", " ");
                        string city = row["city"].ToString();
                        string pin = row["pin"].ToString();
                        string State = row["state"].ToString();

                        string Insert = "insert into E_T_Exporter (JobNo,ExporterName,ExporterAddress1,City,Pincode,StateProvince) values " +
                            " ('" + jobno + "','" + PartyName + "','" + addr + "','" + city + "','" + pin + "','" + State + "') ";
                        int result = InsertData(Insert);
                    //}
                }
            }
            catch (Exception ex)
            {
                Response.Write("Error in PartyDetails" + ex.Message);
            }
        }

    }
}