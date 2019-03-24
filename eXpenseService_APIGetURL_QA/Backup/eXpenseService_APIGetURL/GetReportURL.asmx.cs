using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Services;
using System.Configuration;
using System.Data;

namespace eXpenseService_APIGetURL
{
    /// <summary>
    /// Summary description for GetReportURL
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]

    public class GetReportURL : System.Web.Services.WebService
    {
        string strConnection = ConfigurationSettings.AppSettings["SQL_API_Config"].ToString();
        [WebMethod]
        public string getReportURL(string EmpID)
        {
            string strURL = string.Empty;
            strURL += "https://app.powerbi.com/";
            strURL += "EmpId=" + EmpID + "&";
            DataAccess data = new DataAccess();
            string strQuery = @"  SELECT [Condition],[Values]
                                  FROM [API_WS_EXPENSE].[dbo].[CONFIG_API_WS_GETURL]
                                  WHERE [Active] = 1
                                  AND Condition in ('ReportID','WorkSpaceID')";


            DataTable dt = new DataTable();
            dt = data.SqlSelectCommand_table(strQuery, strConnection);
            if (dt != null && dt.Rows.Count == 2)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (dt.Rows[i]["Condition"].ToString() == "ReportID")
                    {
                        strURL += dt.Rows[i]["Condition"].ToString() + "=" + dt.Rows[i]["Values"].ToString() + "&";
                    }
                    else if (dt.Rows[i]["Condition"].ToString() == "WorkSpaceID")
                    {
                        strURL += dt.Rows[i]["Condition"].ToString() + "=" + dt.Rows[i]["Values"].ToString() + "&";
                    }
                }

                strURL = strURL.TrimEnd('&');
            }
            else
            {
                strURL += " Error;";
            }

            return strURL;
        }
    }
}
