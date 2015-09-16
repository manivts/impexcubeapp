using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using VTS.ImpexCube.Data;

namespace VTS.ImpexCube.Business
{
   public class pChangePassBL
    {
       VTS.ImpexCube.Data.pChangePassDL da = new VTS.ImpexCube.Data.pChangePassDL();
       //DataAccess.pChangePassDL da = new DataAccess.pChangePassDL();

       public DataSet GetData(string userName)
       {
           DataSet ds = new DataSet();
           ds = da.GetData(userName);
           return ds;
       }
       public int updataPassword(string NewPwd, string userName, string pwd)
       {
           return da.updataPassword(NewPwd, userName, pwd);
       }
       
    }
}
