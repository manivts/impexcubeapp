// -----------------------------------------------------------------------
// <copyright file="StatusMasterBL.cs" company="">
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
    public class StatusMasterBL
    {
        VTS.ImpexCube.Data.StatusMasterDL objStatus = new VTS.ImpexCube.Data.StatusMasterDL();

        public DataSet SelectStatusId()
        {
            return objStatus.SelectStatusId();
        }

        public DataSet SelectStatusDetails()
        {
            return objStatus.SelectStatusDetails();
        }

        public DataSet SelectStage()
        {
            return objStatus.SelectStage();
        }

        public int InsertStatusDetails(string status, int stageid, string communication, string subject, string comment, string createdby, string createddate, string modifiedby, string modifieddate, string stagename)
        {
            return objStatus.InsertStatusDetails(status, stageid, communication, subject, comment, createdby, createddate, modifiedby, modifieddate, stagename);
        }

        public int UpdateStatusDetails(int id, string status, int stageid, string communication, string subject, string comment, string modifiedby, string modifieddate, string stagename)
        {
            return objStatus.UpdateStatusDetails(id, status, stageid, communication, subject, comment, modifiedby, modifieddate, stagename);
        }
    }
}
