using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Data.SqlClient;

namespace ImpexCube.Accounts
{
    public class Master
    {
        public DataSet Account()
        {
//            SELECT AccountCode, AccountName FROM         View_AccountsMaster WHERE (GroupName IN ('Bank Accounts', 'Cash-in-Hand')) OR (UnderGroup IN ('Bank Accounts', 'Cash-in-Hand'))
            //string Query = "SELECT DISTINCT AccountName, AccountCode FROM tbl_CustomerDetails  ORDER BY AccountName";
            string Query = "SELECT DISTINCT AccountCode, AccountName FROM M_AccountMaster WHERE (Acc_Group IN ('Bank Accounts', 'Cash-in-Hand'))";// OR (UnderGroup IN ('Bank Accounts', 'Cash-in-Hand'))";
            //SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionTLPL"].ConnectionString);
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Constr"].ConnectionString);
            conn.Open();
            SqlDataAdapter da = new SqlDataAdapter(Query, conn);
            DataSet ds = new DataSet();
            da.Fill(ds, "SQLtable");
            conn.Close();
            return ds;
           
        }
        public DataSet AccountBank()
        {
            string Query = "SELECT DISTINCT AccountCode, AccountName FROM M_AccountMaster WHERE Acc_Group ='Bank Accounts'";
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Constr"].ConnectionString);
            conn.Open();
            SqlDataAdapter da = new SqlDataAdapter(Query, conn);
            DataSet ds = new DataSet();
            da.Fill(ds, "SQLtable");
            conn.Close();
            return ds;

        }
        public DataSet AccountCash()
        {
            string Query = "SELECT DISTINCT AccountCode, AccountName FROM M_AccountMaster WHERE Acc_Group='Cash-in-Hand'";
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Constr"].ConnectionString);
            conn.Open();
            SqlDataAdapter da = new SqlDataAdapter(Query, conn);
            DataSet ds = new DataSet();
            da.Fill(ds, "SQLtable");
            conn.Close();
            return ds;

        }

        public DataSet AccountContra()
        {
            string Query = "SELECT DISTINCT AccountCode, AccountName FROM M_AccountMaster WHERE Acc_Group in ('Cash-in-Hand','Bank Accounts')";
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Constr"].ConnectionString);
            conn.Open();
            SqlDataAdapter da = new SqlDataAdapter(Query, conn);
            DataSet ds = new DataSet();
            da.Fill(ds, "SQLtable");
            conn.Close();
            return ds;

        }
        //public void   AccountCash()
        //{
        //    string Query = "SELECT DISTINCT AccountCode, AccountName FROM AccountMaster WHERE Acc_Group='Cash-in-Hand'";
        //    SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Constr"].ConnectionString);
        //    conn.Open();
        //    SqlCommand cmd = new SqlCommand(Query, conn);
        //    SqlDataReader dr = cmd.ExecuteReader();
        //    dr.Read();
        //    return dr;
        //    //SqlDataAdapter da = new SqlDataAdapter(Query, conn);
        //    //DataSet ds = new DataSet();
        //    //da.Fill(ds, "SQLtable");
        //    //conn.Close();
        //    //return ds;

        //}
        public DataSet AccountPDI()
        {
            string Query = "SELECT DISTINCT AccountCode, AccountName FROM M_AccountMaster";// Where Acc_Group Not In('Charges')";
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Constr"].ConnectionString);
            conn.Open();
            SqlDataAdapter da = new SqlDataAdapter(Query, conn);
            DataSet ds = new DataSet();
            da.Fill(ds, "SQLtable");
            conn.Close();
            return ds;

        }
        public DataSet PartyName()
        {
            string Query = "SELECT DISTINCT AccountCode, AccountName FROM M_AccountMaster Where Acc_Group Not In('Charges')";
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Constr"].ConnectionString);
            conn.Open();
            SqlDataAdapter da = new SqlDataAdapter(Query, conn);
            DataSet ds = new DataSet();
            da.Fill(ds, "SQLtable");
            conn.Close();
            return ds;

        }
        public DataSet Particulars()
        {
            string Query = "SELECT DISTINCT AccountCode, AccountName FROM M_AccountMaster";// OR (UnderGroup IN ('Bank Accounts', 'Cash-in-Hand'))";
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Constr"].ConnectionString);
            conn.Open();
            SqlDataAdapter da = new SqlDataAdapter(Query, conn);
            DataSet ds = new DataSet();
            da.Fill(ds, "SQLtable");
            conn.Close();
            return ds;

        }
    }
}

