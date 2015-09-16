// -----------------------------------------------------------------------
// <copyright file="CustomerMasterDL.cs" company="">
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
    using System.Data.SqlClient;
using System.Data;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class CustomerMasterDL
    {
        string con = (string)ConfigurationManager.ConnectionStrings["Constr"].ConnectionString;

        public int InsertCustomerDetails(string code, string name, string address, string city, string zip, string state, string country,
            string ieccode, string tinno, string itpan, string binno, string adcode, string company, string imptype, string exptype, string impexptype,
            string creditlimit, string days, string salesrep, string createdby, string createddate, string modifiedby, string modifieddate)
        {
            int result = new int();
            string insertcustomer = "Insert Into M_Customer(CustomerCode, CustomerName, Address, City, ZipCode, State, Country," +
                "IecCode, TinNo, ITPan, BinNo, AdCodeNo, CompanyType, ImporterType, ExporterType, ImpExpType, CreditLimit, NoofDays," +
                "SalesRepresetative,CreatedBy, CreatedDate, ModifiedBy, ModifiedDate)" +
                "Values ('" + code + "','" + ConvertAmp(name) + "','" + ConvertAmp(address) + "','" + city + "','" + zip + "','" + state + "'," +
                " '" + country + "', '" + ieccode + "', '" + tinno + "', '" + itpan + "', '" + binno + "', '" + adcode + "' ," +
                " '" + company + "','" + imptype + "', '" + exptype + "', '" + impexptype + "', '" + creditlimit + "', '" + days + "'," +
                " '" + salesrep + "', '" + createdby + "', '" + frmdatesplit(createddate) + "','" + modifiedby + "','" + frmdatesplit(modifieddate) + "')";

            SqlConnection sqlConn = new SqlConnection(con);
            sqlConn.Open();
            SqlCommand cmd = new SqlCommand(insertcustomer, sqlConn);
            SqlDataAdapter da = new SqlDataAdapter();

            cmd.CommandText = insertcustomer;
            cmd.Connection = sqlConn;
            da.SelectCommand = cmd;
            result = cmd.ExecuteNonQuery();
            sqlConn.Close();
            return result;            
        }

        public int InsertCustomerBranch(string code, string name, string branch, string branchcode,string street, string area, string city, string state, string country,
           string createdby, string createddate, string modifiedby, string modifieddate)
        {
            int result = new int();
            string insertcustomerbranch = "Insert Into T_CustomerBranch(CustomerCode, CustomerName, Branch, BranchCode, Street, Area, City, State, Country," +
                    "CreatedBy, CreatedDate, ModifiedBy, ModifiedDate)" +
                    "Values ('" + code + "','" + ConvertAmp(name) + "','" + branch + "', '" + branchcode + "' ,'" + ConvertAmp(street) + "','" + ConvertAmp(area) + "' ,'" + city + "','" + state + "'," +
                    " '" + country + "', '" + createdby + "', '" + frmdatesplit(createddate) + "','" + modifiedby + "','" + frmdatesplit(modifieddate) + "')";

            SqlConnection sqlConn = new SqlConnection(con);
            sqlConn.Open();
            SqlCommand cmd = new SqlCommand(insertcustomerbranch, sqlConn);
            SqlDataAdapter da = new SqlDataAdapter();

            cmd.CommandText = insertcustomerbranch;
            cmd.Connection = sqlConn;
            da.SelectCommand = cmd;
            result = cmd.ExecuteNonQuery();
            sqlConn.Close();                            
            return result;
        }

        public int InsertCustomerContacts(string code, string name, string branch, string department, string contactname, string designation, string phone, string mobile,
           string email, string website, string fax,string createdby, string createddate, string modifiedby, string modifieddate)
        {
            int result = new int();
            string insertcustomerbranch = "Insert Into T_CustomerContacts(CustomerCode, CustomerName, Branch, Department, ContactName, Designation, PhoneNo, Mobile," +
                    "Email, Website , Fax, CreatedBy, CreatedDate, ModifiedBy, ModifiedDate)" +
                    "Values ('" + code + "','" + ConvertAmp(name) + "','" + branch + "','" + ConvertAmp(department) + "','" + ConvertAmp(contactname) + "' ,'" + designation + "','" + phone + "'," +
                    " '" + mobile + "', '" + email + "', '" + website + "', '" + fax + "' , '" + createdby + "', '" + frmdatesplit(createddate) + "','" + modifiedby + "','" + frmdatesplit(modifieddate) + "')";

            SqlConnection sqlConn = new SqlConnection(con);
            sqlConn.Open();
            SqlCommand cmd = new SqlCommand(insertcustomerbranch, sqlConn);
            SqlDataAdapter da = new SqlDataAdapter();

            cmd.CommandText = insertcustomerbranch;
            cmd.Connection = sqlConn;
            da.SelectCommand = cmd;
            result = cmd.ExecuteNonQuery();
            sqlConn.Close();
            return result;
        }

        public int InsertCustomerUserTemplates(string code, string name, string automail, string timings, string department, string report, string employee,
            string createdby, string createddate, string modifiedby, string modifieddate)
        {
            int result = new int();
            string insertcustomerusertemplates= "Insert into T_CustomerUserTemplates(CustomerCode, CustomerName, AutoMail, Timings, Department, ReportType,"+
                "EmployeeName, CreatedBy, CreatedDate, ModifiedBy, ModifiedDate)"+
                "Values ( '" + code + "', '" + ConvertAmp(name) + "', " + automail + ", '" + timings + "','" + department + "',"+
                " '" + report + "', '" + employee + "', '" + createdby + "', '" + frmdatesplit(createddate) + "','" + modifiedby + "','" + frmdatesplit(modifieddate) + "')";

            SqlConnection sqlConn = new SqlConnection(con);
            sqlConn.Open();
            SqlCommand cmd = new SqlCommand(insertcustomerusertemplates, sqlConn);
            SqlDataAdapter da = new SqlDataAdapter();

            cmd.CommandText = insertcustomerusertemplates;
            cmd.Connection = sqlConn;
            da.SelectCommand = cmd;
            result = cmd.ExecuteNonQuery();
            sqlConn.Close();
            return result;
        }

        public DataSet SelectCustomer()
        {
            DataSet ds = new DataSet();
            try
            {
                string qry = "Select CustomerCode as [Code], CustomerName as [Name]  from M_Customer ";
                SqlConnection sqlConn = new SqlConnection(con);
                sqlConn.Open();
                SqlDataAdapter da = new SqlDataAdapter(qry, sqlConn);

                da.Fill(ds, "customer");
                sqlConn.Close();
            }
            catch (Exception ex)
            {
                string Msg = ex.Message.ToString();
            }
            return ds;
        }

        public DataSet SelectCustomerDetails(string code, string name)
        {
            DataSet ds = new DataSet();
            try
            {
                string qry = "Select TransId As [Id],CustomerCode as [Code], CustomerName as [Name], Address, City, ZipCode as [Zip], State, Country, IecCode as [IEC], TinNo as [TIN],ITPan as [Pan], BinNo as [Bin]," +
                            " AdCodeNo as [AdCode], CompanyType as [Company], ImporterType as [Importer], ExporterType as [Exporter],ImpExpType as [ImpExp], CreditLimit as [Credit], NoofDays as [Days]," +
                            " SalesRepresetative as [SalesRep]  from M_Customer Where CustomerCode = '" + code + "' and CustomerName='" + name + "'";
                SqlConnection sqlConn = new SqlConnection(con);
                sqlConn.Open();
                SqlDataAdapter da = new SqlDataAdapter(qry, sqlConn);

                da.Fill(ds, "customer");
                sqlConn.Close();

            }
            catch (Exception ex)
            {
                string Msg = ex.Message.ToString();
            }
            return ds;
        }

        public DataSet SelectCustomerBranch(string code, string name)
        {
            DataSet ds = new DataSet();
            try
            {
                string qry = "Select TransId As [Id],CustomerCode as [Code], CustomerName as [Name], Branch, Street, Area, City, State, Country  from T_CustomerBranch Where CustomerCode = '" + code + "' and CustomerName='" + name + "'";
                SqlConnection sqlConn = new SqlConnection(con);
                sqlConn.Open();
                SqlDataAdapter da = new SqlDataAdapter(qry, sqlConn);

                da.Fill(ds, "customer");
                sqlConn.Close();

            }
            catch (Exception ex)
            {
                string Msg = ex.Message.ToString();
            }
            return ds;
        }

        public DataSet SelectCustomerContacts(string code, string name)
        {
            DataSet ds = new DataSet();
            try
            {
                string qry = "Select TransId As [Id],CustomerCode as [Code], CustomerName as [Name], Branch, Department, ContactName as [Name], Designation, PhoneNo as [Phone], Mobile, Email, Website, Fax from T_CustomerContacts Where CustomerCode = '" + code + "' and CustomerName='" + name + "'";
                SqlConnection sqlConn = new SqlConnection(con);
                sqlConn.Open();
                SqlDataAdapter da = new SqlDataAdapter(qry, sqlConn);

                da.Fill(ds, "contacts");
                sqlConn.Close();

            }
            catch (Exception ex)
            {
                string Msg = ex.Message.ToString();
            }
            return ds;
        }

        public DataSet SelectCustomerBrachcode()
        {
            DataSet ds = new DataSet();
            try
            {
                string qry = "select max(BranchCode) as BranchCode from T_CustomerBranch";
                SqlConnection sqlConn = new SqlConnection(con);
                sqlConn.Open();
                SqlDataAdapter da = new SqlDataAdapter(qry, sqlConn);

                da.Fill(ds, "branch");
                sqlConn.Close();

            }
            catch (Exception ex)
            {
                string Msg = ex.Message.ToString();
            }
            return ds;
        }

        public DataSet SelectCustomerBrachDetails(string code, string name)
        {
            DataSet ds = new DataSet();
            try
            {
                string qry = "select BranchCode, Branch from T_CustomerBranch where CustomerCode = '" + code + "' and CustomerName='" + name + "' ";
                SqlConnection sqlConn = new SqlConnection(con);
                sqlConn.Open();
                SqlDataAdapter da = new SqlDataAdapter(qry, sqlConn);

                da.Fill(ds, "branch");
                sqlConn.Close();

            }
            catch (Exception ex)
            {
                string Msg = ex.Message.ToString();
            }
            return ds;
        }

        public DataSet SelectCustomerEmployeeContacts(string code, string name)
        {
            DataSet ds = new DataSet();
            try
            {
                string qry = "select ContactName as [Employee] from T_CustomerContacts where CustomerCode = '" + code + "' and CustomerName='" + name + "' ";
                SqlConnection sqlConn = new SqlConnection(con);
                sqlConn.Open();
                SqlDataAdapter da = new SqlDataAdapter(qry, sqlConn);

                da.Fill(ds, "employee");
                sqlConn.Close();

            }
            catch (Exception ex)
            {
                string Msg = ex.Message.ToString();
            }
            return ds;
        }

        public DataSet SelectCustomerEmployee(string code, string name)
        {
            DataSet ds = new DataSet();
            try
            {
                string qry = "select Replace(EmployeeName, ',', '') as [Employee] from T_CustomerUserTemplates where CustomerCode = '" + code + "' and CustomerName='" + name + "' ";
                SqlConnection sqlConn = new SqlConnection(con);
                sqlConn.Open();
                SqlDataAdapter da = new SqlDataAdapter(qry, sqlConn);

                da.Fill(ds, "employee");
                sqlConn.Close();

            }
            catch (Exception ex)
            {
                string Msg = ex.Message.ToString();
            }
            return ds;
        }

        public DataSet SelectCustomerUserTemplates(string code, string name)
        {
            DataSet ds = new DataSet();
            try
            {
                string qry = "select TransId as [Id], AutoMail, Timings, Department, ReportType, EmployeeName as [Employee] from T_CustomerUserTemplates where CustomerCode = '" + code + "' and CustomerName='" + name + "' ";
                SqlConnection sqlConn = new SqlConnection(con);
                sqlConn.Open();
                SqlDataAdapter da = new SqlDataAdapter(qry, sqlConn);

                da.Fill(ds, "templates");
                sqlConn.Close();

            }
            catch (Exception ex)
            {
                string Msg = ex.Message.ToString();
            }
            return ds;
        }

        public int UpdateCustomerDetails(string code, string name, string address, string city, string zip, string state, string country,
            string ieccode, string tinno, string itpan, string binno, string adcode, string company, string imptype, string exptype, string impexptype,
            string creditlimit, string days, string salesrep, string modifiedby, string modifieddate)
        {
            int result = new int();
            string Updatecustomer = "Update M_Customer set CustomerCode='" + code + "' , CustomerName='" + ConvertAmp(name) + "', Address='" + ConvertAmp(address) + "'," +
                "City='" + city + "', ZipCode='" + zip + "', State='" + state + "', Country='" + country + "'," +
                "IecCode= '" + ieccode + "', TinNo='" + tinno + "', ITPan='" + itpan + "', BinNo='" + binno + "', AdCodeNo='" + adcode + "' ," +
                "CompanyType='" + company + "', ImporterType='" + imptype + "', ExporterType='" + exptype + "', ImpExpType='" + impexptype + "'," +
                "CreditLimit='" + creditlimit + "', NoofDays='" + days + "', SalesRepresetative='" + salesrep + "'," +
                "ModifiedBy='" + modifiedby + "', ModifiedDate='" + frmdatesplit(modifieddate) + "' where CustomerCode='" + code + "' and CustomerName='" + ConvertAmp(name) + "'";                

            SqlConnection sqlConn = new SqlConnection(con);
            sqlConn.Open();
            SqlCommand cmd = new SqlCommand(Updatecustomer, sqlConn);
            SqlDataAdapter da = new SqlDataAdapter();

            cmd.CommandText = Updatecustomer;
            cmd.Connection = sqlConn;
            da.SelectCommand = cmd;
            result = cmd.ExecuteNonQuery();
            sqlConn.Close();
            return result;
        }

        public int UpdateCustomerBranch(int id,string code, string name, string branch, string street, string area, string city, string state, string country,
           string modifiedby, string modifieddate)
        {
            int result = new int();
            string Updatecustomerbranch = "Update T_CustomerBranch set CustomerCode='" + code + "' , CustomerName='" + ConvertAmp(name) + "', Branch='" + branch + "',"+
                " Street='" + ConvertAmp(street) + "', Area='" + ConvertAmp(area) + "' , City='" + city + "', State='" + state + "', Country='" + country + "'," +
                    "ModifiedBy='" + modifiedby + "', ModifiedDate='" + frmdatesplit(modifieddate) + "' where TransId= '" + id + "'";
                    

            SqlConnection sqlConn = new SqlConnection(con);
            sqlConn.Open();
            SqlCommand cmd = new SqlCommand(Updatecustomerbranch, sqlConn);
            SqlDataAdapter da = new SqlDataAdapter();

            cmd.CommandText = Updatecustomerbranch;
            cmd.Connection = sqlConn;
            da.SelectCommand = cmd;
            result = cmd.ExecuteNonQuery();
            sqlConn.Close();
            return result;
        }

        public int UpdateCustomerContacts(int id, string code, string name, string branch, string department, string contactname, string designation, string phone, 
            string mobile, string email, string website, string fax, string modifiedby, string modifieddate)
        {
            int result = new int();
            string Updatecustomerbranch = "Update T_CustomerContacts set CustomerCode='" + code + "' , CustomerName='" + ConvertAmp(name) + "', Branch='" + branch + "', Department='" + department + "' ," +
                "ContactName='" + ConvertAmp(contactname) + "', Designation='" + designation + "' , PhoneNo='" + phone + "', Mobile='" + mobile + "', Email='" + email + "'," +
                " Website= '" + website + "', Fax= '" + fax + "' ,ModifiedBy='" + modifiedby + "', ModifiedDate='" + frmdatesplit(modifieddate) + "' where TransId= '" + id + "'";


            SqlConnection sqlConn = new SqlConnection(con);
            sqlConn.Open();
            SqlCommand cmd = new SqlCommand(Updatecustomerbranch, sqlConn);
            SqlDataAdapter da = new SqlDataAdapter();

            cmd.CommandText = Updatecustomerbranch;
            cmd.Connection = sqlConn;
            da.SelectCommand = cmd;
            result = cmd.ExecuteNonQuery();
            sqlConn.Close();
            return result;
        }

        public int UpdateCustomerUserTemplates(int id, string code, string name, string automail, string timings, string department, string report, string employee,
            string modifiedby, string modifieddate)
        {
            int result = new int();
            string updatecustomerusertemplates = "update T_CustomerUserTemplates set CustomerCode = '" + code + "', CustomerName = '" + ConvertAmp(name) + "', AutoMail= " + automail + ", " +
                " Timings='" + timings + "', Department='" + department + "', ReportType = '" + report + "'," +
                " EmployeeName='" + employee + "', ModifiedBy='" + modifiedby + "', "+
                " ModifiedDate = '" + frmdatesplit(modifieddate) + "' where TransId= '" + id + "' and CustomerCode = '" + code + "' and CustomerName = '" + ConvertAmp(name) + "'";

            SqlConnection sqlConn = new SqlConnection(con);
            sqlConn.Open();
            SqlCommand cmd = new SqlCommand(updatecustomerusertemplates, sqlConn);
            SqlDataAdapter da = new SqlDataAdapter();

            cmd.CommandText = updatecustomerusertemplates;
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
