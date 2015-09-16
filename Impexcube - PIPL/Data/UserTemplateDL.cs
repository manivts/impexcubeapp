// -----------------------------------------------------------------------
// <copyright file="UserTemplateDL.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace VTS.ImpexCube.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Configuration;
    using System.Data;
    using System.Data.SqlClient;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class UserTemplateDL
    {
        string con = (string)ConfigurationManager.ConnectionStrings["Constr"].ConnectionString;

        public DataSet SelectImportName()
        {
            DataSet ds = new DataSet();
            try
            {
                string Query = "select CustomerName as [Importer] from M_Customer";

                SqlConnection sqlConn = new SqlConnection(con);
                sqlConn.Open();
                SqlDataAdapter da = new SqlDataAdapter(Query, sqlConn);

                da.Fill(ds, "importer");
                sqlConn.Close();
            }
            catch (Exception ex)
            {
                string Msg = ex.Message.ToString();
            }
            return ds;
        }

        public int InsertReportTemplates(string partyname, string template, string fields, string createdby, string createddate, string modifiedby, string modifieddate)
        {
            int result = new int();
            string insertusertemplates = "Insert Into T_UserReportTemplates(PartyName,TemplateName, CustomField,Createdby,CreatedDate,ModifiedBy,ModifiedDate)" +
                "Values ('" + ConvertAmp(partyname) + "','" + ConvertAmp(template) + "','" + fields + "'," +
               " '" + createdby + "','" + frmdatesplit(createddate) + "','" + modifiedby + "','" + frmdatesplit(modifieddate) + "')";

            SqlConnection sqlConn = new SqlConnection(con);
            sqlConn.Open();
            SqlCommand cmd = new SqlCommand(insertusertemplates, sqlConn);
            SqlDataAdapter da = new SqlDataAdapter();

            cmd.CommandText = insertusertemplates;
            cmd.Connection = sqlConn;
            da.SelectCommand = cmd;
            result = cmd.ExecuteNonQuery();
            sqlConn.Close();
            return result;
        }

        public DataSet SelectUserReportTemplate()
        {
            DataSet ds = new DataSet();
            try
            {
                string Query = "select TransId as [Id], TemplateName as [Template], CustomField as [Fields] from T_UserReportTemplates";

                SqlConnection sqlConn = new SqlConnection(con);
                sqlConn.Open();
                SqlDataAdapter da = new SqlDataAdapter(Query, sqlConn);

                da.Fill(ds, "templates");
                sqlConn.Close();
            }
            catch (Exception ex)
            {
                string Msg = ex.Message.ToString();
            }
            return ds;
        }

        public DataSet SelectJobFields()
        {
            DataSet ds = new DataSet();
            try
            {
                string qry = "select JobFields,Test from T_JobFields ";
                SqlConnection sqlConn = new SqlConnection(con);
                sqlConn.Open();
                SqlDataAdapter da = new SqlDataAdapter(qry, sqlConn);

                da.Fill(ds, "fields");
                sqlConn.Close();

            }
            catch (Exception ex)
            {
                string Msg = ex.Message.ToString();
            }
            return ds;
        }

        public int UpdateReportTemplates(int id, string partyname, string template, string fields, string modifiedby, string modifieddate)
        {
            int result = new int();
            string updateusertemplates = "Update T_UserReportTemplates Set PartyName = '" + ConvertAmp(partyname) + "',TemplateName = '" + ConvertAmp(template) + "',"+
                " CustomField = '" + fields + "',ModifiedBy='" + modifiedby + "',ModifiedDate='" + frmdatesplit(modifieddate) + "' where TransId = '" + id + "'";                

            SqlConnection sqlConn = new SqlConnection(con);
            sqlConn.Open();
            SqlCommand cmd = new SqlCommand(updateusertemplates, sqlConn);
            SqlDataAdapter da = new SqlDataAdapter();

            cmd.CommandText = updateusertemplates;
            cmd.Connection = sqlConn;
            da.SelectCommand = cmd;
            result = cmd.ExecuteNonQuery();
            sqlConn.Close();
            return result;
        }

        private string frmdatesplit(string frmdate)
        {
            string[] frmdate1 = frmdate.Split('/');
            string frmdate2 = frmdate1[1] + '/' + frmdate1[0] + '/' + frmdate1[2];
            return frmdate2;
        }

        public static string ConvertAmp(string st)
        {
            string res = "";
            int i;
            for (i = 0; (i <= (st.Length - 1)); i++)
            {
                if (st[i].ToString() == "&")
                {
                    res = (res + "&amp;");
                }
                else if ((st[i].ToString() == "'"))
                {
                    res = (res + "''");
                }
                else
                {
                    res = (res + st[i].ToString());
                }
            }
            return res;
        }

    }
}
