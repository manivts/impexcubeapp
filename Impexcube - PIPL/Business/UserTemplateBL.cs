// -----------------------------------------------------------------------
// <copyright file="UserTemplateBL.cs" company="">
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
    public class UserTemplateBL
    {
        VTS.ImpexCube.Data.UserTemplateDL objUserTemplate = new VTS.ImpexCube.Data.UserTemplateDL();

        public DataSet SelectImportName()
        {
            return objUserTemplate.SelectImportName();
        }

        public DataSet SelectJobFields()
        {
            return objUserTemplate.SelectJobFields();
        }

        public int InsertReportTemplates(string partyname, string template, string fields, string createdby, string createddate, string modifiedby, string modifieddate)
        {
            return objUserTemplate.InsertReportTemplates(partyname, template, fields, createdby, createddate, modifiedby, modifieddate);
        }

        public int UpdateReportTemplates(int id, string partyname, string template, string fields, string modifiedby, string modifieddate)
        {
            return objUserTemplate.UpdateReportTemplates(id, partyname, template, fields, modifiedby, modifieddate);
        }

        public DataSet SelectUserReportTemplate()
        {
            return objUserTemplate.SelectUserReportTemplate();
        }
    }
}
