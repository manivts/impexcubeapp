// -----------------------------------------------------------------------
// <copyright file="StageMasterBL.cs" company="">
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
    public class StageMasterBL
    {
        VTS.ImpexCube.Data.StageMasterDL objStage = new VTS.ImpexCube.Data.StageMasterDL();

        public DataSet SelectStageId()
        {
            return objStage.SelectStageId();
        }

        public DataSet SelectStage()
        {
            return objStage.SelectStage();
        }

        public int InsertStageDetails(string Stage, string createdby, string createddate, string modifiedby, string modifieddate)
        {
            return objStage.InsertStageDetails(Stage, createdby, createddate, modifiedby, modifieddate);
        }

        public int UpdateStageDetails(int id, string Stage, string modifiedby, string modifieddate)
        {
            return objStage.UpdateStageDetails(id, Stage, modifiedby, modifieddate);
        }
    }
}
