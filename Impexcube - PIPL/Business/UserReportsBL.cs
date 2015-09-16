// -----------------------------------------------------------------------
// <copyright file="UserReportsBL.cs" company="">
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
    public class UserReportsBL
    {
        VTS.ImpexCube.Data.UserReportsDL objUserReports = new VTS.ImpexCube.Data.UserReportsDL();

        public DataSet SelectStage()
        {
            return objUserReports.SelectStage();
        }

        public DataSet SelectStageStatus(int StageId)
        {
            return objUserReports.SelectStageStatus(StageId);
        }

        public DataSet SearchJobReportList(string From, string To, string Importer, string Jobno, string stage, string status, string shipment, string shipmentType)
        {
            return objUserReports.SearchJobReportList(From, To, Importer, Jobno, stage, status, shipment, shipmentType);
        }

    }
}
