// -----------------------------------------------------------------------
// <copyright file="CustomerMasterBL.cs" company="">
// TODO: Update copyright text.
// </copyright>
// -----------------------------------------------------------------------

namespace VTS.ImpexCube.Business
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
using System.Data;

    /// <summary>
    /// TODO: Update summary.
    /// </summary>
    public class CustomerMasterBL
    {
        VTS.ImpexCube.Data.CustomerMasterDL objCustomer = new VTS.ImpexCube.Data.CustomerMasterDL();

        public int InsertCustomerDetails(string code, string name, string address, string city, string zip, string state, string country,
            string ieccode, string tinno, string itpan, string binno, string adcode, string company, string imptype, string exptype, string impexptype,
            string creditlimit, string days, string salesrep, string createdby, string createddate, string modifiedby, string modifieddate)
        {
            return objCustomer.InsertCustomerDetails(code, name, address, city, zip, state, country, ieccode, tinno, itpan, binno, adcode,
                company, imptype, exptype, impexptype, creditlimit, days, salesrep, createdby, createddate, modifiedby, modifieddate);
        }

        public int InsertCustomerBranch(string code, string name, string branch, string branchcode, string street, string area, string city, string state, string country,
           string createdby, string createddate, string modifiedby, string modifieddate)
        {
            return objCustomer.InsertCustomerBranch(code, name, branch, branchcode, street, area, city, state, country, createdby, createddate, modifiedby, modifieddate);
        }

        public int InsertCustomerContacts(string code, string name, string branch, string department, string contactname, string designation, string phone, string mobile,
           string email, string website, string fax, string createdby, string createddate, string modifiedby, string modifieddate)
        {
            return objCustomer.InsertCustomerContacts(code, name, branch, department, contactname, designation, phone, mobile, email, website, fax, createdby, createddate,
                modifiedby, modifieddate);
        }

        public int InsertCustomerUserTemplates(string code, string name, string automail, string timings, string department, string report, string employee,
            string createdby, string createddate, string modifiedby, string modifieddate)
        {
            return objCustomer.InsertCustomerUserTemplates(code, name, automail, timings, department, report, employee, createdby, createddate, modifiedby, modifieddate);
        }

        public DataSet SelectCustomer()
        {
            return objCustomer.SelectCustomer();
        }

        public DataSet SelectCustomerDetails(string code, string name)
        {
            return objCustomer.SelectCustomerDetails(code, name);
        }

        public DataSet SelectCustomerBranch(string code, string name)
        {
            return objCustomer.SelectCustomerBranch(code, name);
        }

        public DataSet SelectCustomerContacts(string code, string name)
        {
            return objCustomer.SelectCustomerContacts(code, name);
        }

        public DataSet SelectCustomerBrachcode()
        {
            return objCustomer.SelectCustomerBrachcode();
        }

        public DataSet SelectCustomerBrachDetails(string code, string name)
        {
            return objCustomer.SelectCustomerBrachDetails(code, name);
        }

        public DataSet SelectCustomerEmployeeContacts(string code, string name)
        {            
            return objCustomer.SelectCustomerEmployeeContacts(code, name);
        }

        public DataSet SelectCustomerEmployee(string code, string name)
        {
            return objCustomer.SelectCustomerEmployee(code, name);
        }

        public DataSet SelectCustomerUserTemplates(string code, string name)
        {
            return objCustomer.SelectCustomerUserTemplates(code, name);
        }

        public int UpdateCustomerDetails(string code, string name, string address, string city, string zip, string state, string country,
            string ieccode, string tinno, string itpan, string binno, string adcode, string company, string imptype, string exptype, string impexptype,
            string creditlimit, string days, string salesrep, string modifiedby, string modifieddate)
        {
            return objCustomer.UpdateCustomerDetails(code, name, address, city, zip, state, country, ieccode, tinno, itpan, binno, adcode, company, imptype, exptype,
                impexptype, creditlimit, days, salesrep, modifiedby, modifieddate);
        }

        public int UpdateCustomerBranch(int id, string code, string name, string branch, string street, string area, string city, string state, string country,
           string modifiedby, string modifieddate)
        {
            return objCustomer.UpdateCustomerBranch(id, code, name, branch, street, area, city, state, country, modifiedby, modifieddate);
        }

        public int UpdateCustomerContacts(int id, string code, string name, string branch, string department, string contactname, string designation, string phone,
            string mobile, string email, string website, string fax, string modifiedby, string modifieddate)
        {
            return objCustomer.UpdateCustomerContacts(id, code, name, branch, department, contactname, designation, phone, mobile, email, website, fax,
                modifiedby, modifieddate);
        }

        public int UpdateCustomerUserTemplates(int id, string code, string name, string automail, string timings, string department, string report, string employee,
            string modifiedby, string modifieddate)
        {
            return objCustomer.UpdateCustomerUserTemplates(id, code, name, automail, timings, department, report, employee, modifiedby, modifieddate);
        }
    }
}
