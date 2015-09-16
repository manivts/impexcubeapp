using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using VTS.ImpexCube.Data;

namespace VTS.ImpexCube.Business
{
   public class userAuthorityBL
    {
       VTS.ImpexCube.Data.userAuthorityDL da = new Data.userAuthorityDL();
       //DataAccess.userAuthorityDL da = new DataAccess.userAuthorityDL();

       public DataSet GetData(string name)
       {
           DataSet ds = new DataSet();
           ds = da.GetData(name);
           return ds;
       }
       public DataSet GetBranch()
       {
           DataSet ds = new DataSet();
           ds = da.GetBranch();
           return ds;
       }
       public DataSet GetEMPName(string eName)
       {
           DataSet ds = new DataSet();
           ds = da.GetEMPName(eName);
           return ds;
       }
       public DataSet GetEMP(string UID)
       {
           DataSet ds = new DataSet();
           ds = da.GetEMP(UID);
           return ds;
       }
       public DataSet GetUser(string EID)
       {
           DataSet ds = new DataSet();
           ds = da.GetUser(EID);
           return ds;
       }
       public DataSet GetUser(string EID, string formCode)
       {
           DataSet ds = new DataSet();
           ds = da.GetUser(EID, formCode);
           return ds;
       }
       public DataSet GetForms(string CMP, string eid, string F1, string F2)
       {
           DataSet ds = new DataSet();
           ds = da.GetForms(CMP, eid, F1, F2);
           return ds;
       }
       public DataSet GetUserAuth(Int32 strUID)
       {
           DataSet ds = new DataSet();
           ds = da.GetUserAuth(strUID);
           return ds;
       }
       public int deleteUserAuth(string EName)
       {
           return da.deleteUserAuth(EName);
       }
       public int createNewUser(string strPass, string strName, string strZone, string strGrade, string strMail, string empName)
       {
           return da.createNewUser(strPass, strName, strZone, strGrade, strMail,empName);
       }
       public int createUserAuthority(string formID, string formName, string ename, string EID, string Branch, string DIS, string READ)
       {
           return da.createUserAuthority(formID, formName, ename, EID, Branch, DIS, READ);
       }
       public int updateDisableForms(string strDisable, string userAuth, string formName)
       {
           return da.updateDisableForms(strDisable, userAuth, formName);
       }
       public int updateReadOnlyForms(string strREAD, string userAuth, string formName)
       {
           return da.updateReadOnlyForms(strREAD, userAuth, formName);
       }
       public int deleteUser(Int32 strUID)
       {
           return da.deleteUser(strUID);
       }
       public int updataPassword(Int32 userId, string userName, string pwd)
       {
           return da.updataPassword(userId, userName, pwd);
       }
    }
}
