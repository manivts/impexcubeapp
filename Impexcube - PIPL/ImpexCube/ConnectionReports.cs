using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.IO;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using System.Xml.Linq;
using System.Collections.Generic;
using Microsoft.ApplicationBlocks.Data;
using Microsoft.ApplicationBlocks.ExceptionManagement; 


namespace ImpexCube
{
    public class ConnectionReports
    {

        public static bool isTrueFalse;
        public static void FillDataSet(string strSqlStatement, DataSet DataSetName, string[] strTableName)
        {
            SqlHelper.FillDataset(GetConnectionString(isTrueFalse), CommandType.Text, strSqlStatement, DataSetName, strTableName);
        }
        public static string GetConnectionString(bool isTrueFalse)
        {
            if (isTrueFalse)
                return ConfigurationManager.AppSettings["ConnectionImpex"];

            else
                return ConfigurationManager.AppSettings["ConnectionImpex"];
        }
        public static void SetParameter(ReportDocument rptObject, string[] pstrSPParamArray)
        {
            try
            {
                int lintCtr = 0;
                foreach (string lstrParameterData in pstrSPParamArray)
                {
                    if (lstrParameterData != "")
                    {
                        rptObject.SetParameterValue(lintCtr, lstrParameterData);
                    }
                    lintCtr = lintCtr + 1;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static void ShowReport(ReportDocument rptObject, DataSet dataSetName, string forMat)
        {
            int lintCtr = 0;
            try
            {

                if (dataSetName.Tables[0].Rows.Count > 0)
                {
                    rptObject.Database.Tables[0].SetDataSource(dataSetName);

                    ShowReportFromStream(rptObject, forMat);
                }
            }

            catch (Exception ex)
            {
                if (ex.Message.ToString() != "Thread was being aborted.")
                {

                    Console.Write(ex);
                }
            }
            finally
            {
                rptObject.Dispose();
                rptObject = null;
                dataSetName.Dispose();
                dataSetName = null;
                GC.Collect();
                GC.WaitForPendingFinalizers();
                GC.Collect();
            }

        }
        public static void ShowReportWithParameter(ReportDocument rptObject, DataSet dataSetName, string[] parameters, string forMat)
        {
            //int lintCtr = 0;
            try
            {

                if (dataSetName.Tables[0].Rows.Count > 0)
                {
                    rptObject.Database.Tables[0].SetDataSource(dataSetName);
                    SetParameter(rptObject, parameters);
                    ShowReportFromStream(rptObject, forMat);
                }
            }

            catch (Exception ex)
            {
                if (ex.Message.ToString() != "Thread was being aborted.")
                {

                    Console.Write(ex);
                }
            }
            finally
            {
                rptObject.Dispose();
                rptObject = null;
                dataSetName.Dispose();
                dataSetName = null;
                GC.Collect();
                GC.WaitForPendingFinalizers();
                GC.Collect();
            }

        }
        public static void ShowReport(ReportDocument rptObject, string[] pstrSubRepName, DataSet dataSetName, string forMat)
        {
            int lintCtr = 0;
            try
            {

                if (dataSetName.Tables[0].Rows.Count > 0)
                {
                    rptObject.Database.Tables[0].SetDataSource(dataSetName);
                    for (lintCtr = 0; lintCtr < pstrSubRepName.GetLength(0); lintCtr++)
                    {
                        rptObject.OpenSubreport(pstrSubRepName[lintCtr]).SetDataSource(dataSetName);
                    }

                    ShowReportFromStream(rptObject, forMat);
                }
            }

            catch (Exception ex)
            {
                if (ex.Message.ToString() != "Thread was being aborted.")
                {

                    Console.Write(ex);
                }
            }
            finally
            {
                rptObject.Dispose();
                rptObject = null;
                dataSetName.Dispose();
                dataSetName = null;
                GC.Collect();
                GC.WaitForPendingFinalizers();
                GC.Collect();
            }

        }
        public static void ShowReportFromStream(ReportDocument rptObject, string forMat)
        {
            MemoryStream oStream = null;
            try
            {
                if (forMat == "PD")
                {
                    oStream = (MemoryStream)
                    rptObject.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
                    HttpContext.Current.Response.Clear();
                    HttpContext.Current.Response.Buffer = true;
                    HttpContext.Current.Response.ContentType = "application/pdf";
                }
                else
                {
                    //oStream = (MemoryStream)
                    //rptObject.ExportToStream(CrystalDecisions.Shared.ExportFormatType.WordForWindows);
                    //HttpContext.Current.Response.Clear();
                    //HttpContext.Current.Response.Buffer = true;
                    //HttpContext.Current.Response.ContentType = "application/msword";
                    HttpContext.Current.Response.Write("Please Select PDF Format.You Cannot Download this format...");
                }
                HttpContext.Current.Response.BinaryWrite(oStream.ToArray());
                HttpContext.Current.Response.Flush();
                HttpContext.Current.Response.Close();
                HttpContext.Current.Response.End();
            }
            catch (Exception ex)
            {
                Console.Write(ex);
            }
            finally
            {

                oStream.Flush();
                oStream.Close();
                oStream = null;
                rptObject.Dispose();
                rptObject = null;
                GC.Collect();
                GC.WaitForPendingFinalizers();
                GC.Collect();

            }
        }
    }
}