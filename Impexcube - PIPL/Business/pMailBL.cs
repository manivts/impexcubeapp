using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using VTS.ImpexCube.Data;

namespace VTS.ImpexCube.Business
{
  public class pMailBL
    {
      VTS.ImpexCube.Data.pMailDL da = new VTS.ImpexCube.Data.pMailDL();

      public DataSet GetData()
      {
          DataSet ds = new DataSet();
          ds = da.GetData();
          return ds;
      }
      public DataSet GetEmpMail()
      {
          DataSet ds = new DataSet();
          ds = da.GetEmpMail();
          return ds;
      }
      public DataSet GetMail(string sNO)
      {
          DataSet ds = new DataSet();
          ds = da.GetMail(sNO);
          return ds;
      }
      public int SendMail(string strFrom, string strTo, string strCc, string strBcc, string strSubject, string strMessage, string strAttach,  string strDate, string strTime, string strUser, string strCMP)
      {
       return da.SendMail(strFrom, strTo, strCc, strBcc, strSubject, strMessage, strAttach, strDate, strTime, strUser, strCMP);
        
      }
      public int deleteMail(int rNO)
      {
          return da.deleteMail(rNO);
      }
    }
}
