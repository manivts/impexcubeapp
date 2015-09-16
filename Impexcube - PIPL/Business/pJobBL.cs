using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using  VTS.ImpexCube.Data;

namespace VTS.ImpexCube.Business
{
   public class pJobBL
    {
       VTS.ImpexCube.Data.pJobDL da = new VTS.ImpexCube.Data.pJobDL();

       public DataSet GetParty(string pcode)
       {
           DataSet ds = new DataSet();
           ds = da.GetParty(pcode);
           return ds;
       }
       public DataSet GetiWorkreg(string jno, string strconnVI1,string fy)
       {
           DataSet ds = new DataSet();
           ds = da.GetiWorkreg(jno, strconnVI1,fy);
           return ds;
       }
       public DataSet GetJobs(string sqlQuery,string strconnJSU)
       {
           DataSet ds = new DataSet();
           ds = da.GetJobs(sqlQuery,strconnJSU);
           return ds;
       }

     

       public int SyncJobs(string sqlquery)
       {
           return da.SyncJobs(sqlquery);
       }
    }
}
