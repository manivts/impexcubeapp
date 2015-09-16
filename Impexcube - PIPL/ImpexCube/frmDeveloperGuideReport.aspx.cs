using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;

namespace ImpexCube
{
    public partial class frmDeveloperGuideReport : System.Web.UI.Page
    {
        string con = (string)ConfigurationManager.ConnectionStrings["Constr"].ConnectionString;
        VTS.ImpexCube.Business.JobStageUpdateBL objJobStage = new VTS.ImpexCube.Business.JobStageUpdateBL();
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {                
                developersname();
                modulename();
                txttodate.Text = DateTime.Now.ToString("dd/MM/yyyy");
            }
        }
              

        public void developersname()
        {
            DataSet ds = new DataSet();
            string Query = "select distinct developername from T_DeveloperGuide";
            SqlConnection sqlConn = new SqlConnection(con);
            sqlConn.Open();
            SqlDataAdapter da = new SqlDataAdapter(Query, sqlConn);

            da.Fill(ds, "dname");
            sqlConn.Close();
            if (ds.Tables["dname"].Rows.Count != 0)
            {
                ddldevelopername.DataSource = ds;
                ddldevelopername.DataTextField = "developername";
                ddldevelopername.DataValueField = "developername";
                ddldevelopername.DataBind();
                ddldevelopername.Items.Insert(0, new ListItem("ALL", "ALL"));
                ddlmodulename.Enabled = true;
            }
        }

        public void modulename()
        {
            string dn = ddldevelopername.SelectedItem.Text;
            DataSet ds = new DataSet();
            string Query = "select distinct ModuleName from T_DeveloperGuide";
            SqlConnection sqlconn = new SqlConnection(con);
            sqlconn.Open();
            SqlDataAdapter da = new SqlDataAdapter(Query, sqlconn);
            da.Fill(ds, "mname");
            sqlconn.Close();
            if (ds.Tables["mname"].Rows.Count != 0)
            {
                ddlmodulename.DataSource = ds;
                ddlmodulename.DataTextField = "ModuleName";
                ddlmodulename.DataValueField = "ModuleName";
                ddlmodulename.DataBind();
                //ddlmodulename.Items.Insert(0, new ListItem("-ALL-", "0"));



            }
        }

        public void formname()
        {
            string fn = ddlmodulename.SelectedItem.Text;
            DataSet ds = new DataSet();
            string Query = "select distinct formname from T_DeveloperGuide where modulename='" + fn + "'";
            SqlConnection sqlconn = new SqlConnection(con);
            sqlconn.Open();
            SqlDataAdapter da = new SqlDataAdapter(Query, sqlconn);
            da.Fill(ds, "fname");
            sqlconn.Close();
            sqlconn.Close();
            if (ds.Tables["fname"].Rows.Count != 0)
            {
                ddlfromname.DataSource = ds;
                ddlfromname.DataTextField = "formname";
                ddlfromname.DataValueField = "formname";
                ddlfromname.DataBind();

            }



        }
        protected void ddlmodulename_SelectedIndexChanged(object sender, EventArgs e)
        {


            if (ddlmodulename.SelectedValue == "ALL")
            {
                ddlfromname.Items.Clear();
                ddlfromname.Items.Insert(0, new ListItem("ALL", "ALL"));
            }
            else
            {
                formname();
            }
        }

        public string changedate(string date)
        {
            if (date != "")
            {
                string[] a = date.Split('/');
                date = a[1] + "/" + a[0] + "/" + a[2];
                
            }
            return date;
        }
        protected void btngetreport_Click(object sender, EventArgs e)
        {           
            string fromdate=txtfromdate.Text;
            string todate=txttodate.Text;
            if (txtfromdate.Text != "" && txttodate.Text != "")
            {
                if ( ddldevelopername.SelectedValue!="ALL" && ddlmodulename.SelectedValue != "ALL" && ddlfromname.SelectedValue != "ALL")
                {
                    string dn = ddldevelopername.SelectedItem.Text;
                    string mn = ddlmodulename.SelectedItem.Text;
                    string fn = ddlfromname.SelectedItem.Text;
                    DateTime from = Convert.ToDateTime(changedate(fromdate));
                    DateTime to = Convert.ToDateTime(changedate(todate));
                    DataSet ds = new DataSet();
                    string Query = "select transid,developername,ModuleName,formname,transid,description,convert(varchar(11),TransDate,103) as TransDate from T_DeveloperGuide where developername='"+dn+"' and modulename='" + mn + "' and formname='" + fn + "' and Transdate between'" + from + "' and '" + to + "'";
                    SqlConnection sqlconn = new SqlConnection(con);
                    sqlconn.Open();
                    SqlDataAdapter da = new SqlDataAdapter(Query, sqlconn);
                    da.Fill(ds, "data");
                    sqlconn.Close();
                    sqlconn.Close();
                    if (ds.Tables["data"].Rows.Count != 0)
                    {
                        gvdevelopers.DataSource = ds;
                        gvdevelopers.DataBind();

                    }
                    else
                    {
                        gvdevelopers.DataSource = null;
                        gvdevelopers.DataBind();
                        Response.Write("<script>alert('No data between the date')</script>");
                    }
                }

            //}
                else
                {
                    DateTime from = Convert.ToDateTime(changedate(fromdate));
                    DateTime to = Convert.ToDateTime(changedate(todate));
                    DataSet ds = new DataSet();
                    string Query = "select developername,ModuleName,formname,convert(varchar(11),TransDate,103) as TransDate,transid,description from T_DeveloperGuide where  Transdate between'" + from + "' and '" + to + "'";
                    SqlConnection sqlconn = new SqlConnection(con);
                    sqlconn.Open();
                    SqlDataAdapter da = new SqlDataAdapter(Query, sqlconn);
                    da.Fill(ds, "data");
                    sqlconn.Close();
                    sqlconn.Close();
                    if (ds.Tables["data"].Rows.Count != 0)
                    {
                        gvdevelopers.DataSource = ds;
                        gvdevelopers.DataBind();

                    }
                    else
                    {
                        gvdevelopers.DataSource = null;
                        gvdevelopers.DataBind();
                        Response.Write("<script>alert('No data between the date')</script>");
                    }
                }
            }
            else
            {
                if (txtfromdate.Text == "" || txttodate.Text == "")
                {
                    DataSet ds = new DataSet();
                    string Query = "select developername,ModuleName,formname,convert(varchar(11),TransDate,103) as TransDate,transid,description from T_DeveloperGuide";
                    SqlConnection sqlconn = new SqlConnection(con);
                    sqlconn.Open();
                    SqlDataAdapter da = new SqlDataAdapter(Query, sqlconn);
                    da.Fill(ds, "data");
                    sqlconn.Close();
                    sqlconn.Close();
                    if (ds.Tables["data"].Rows.Count != 0)
                    {
                        gvdevelopers.DataSource = ds;
                        gvdevelopers.DataBind();

                    }
                    else
                    {
                        gvdevelopers.DataSource = null;
                        gvdevelopers.DataBind();
                        Response.Write("<script>alert('No data between the date')</script>");
                    }



                }


            }

        }

       
        protected void btnexport_Click(object sender, EventArgs e)
        {
             string ExcelExport = "DevelopersGuide" + ".xls";
                    GridViewExportUtil.Export(ExcelExport, gvdevelopers);
        }
        protected void ddldevelopername_SelectedIndexChanged(object sender, EventArgs e)
        {


        }


    }


}
//string Query = "select developername,ModuleName,formname,TransDate,transid,description from T_DeveloperGuide where developername='" + dn + "'and modulename='" + mn + "' and formname='" + fn + "' and Transdate between'" + from  + "' and '" + to + "'";