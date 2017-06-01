<%@ WebHandler Language="C#" Class="GetLogTest" %>

using System;
using System.Web;
using OnLineTest.Model;
using OnLineTest.BLL;
using System.Web.Script.Serialization;
using System.Collections.Generic;

public class GetLogTest : IHttpHandler
{

    public void ProcessRequest(HttpContext context)
    {
        LogTest logtest = new LogTest();
        LogTestManager logtestmanager = new LogTestManager();
        System.Collections.Generic.List<Dictionary<string, object>> list = new System.Collections.Generic.List<Dictionary<string, object>>();
        Users user = common.GetCurrnetUser(context);
        int num = 5;
        if (context.Request.Form["num"] != null)
        {
            num = Convert.ToInt32(context.Request.Form["num"]);
        }
        if (user != null)
        {
            list = logtestmanager.GetModelsbyJoin(num, "logteststarttime", user.UserId);
        }
        context.Response.ContentType = "text/plain";
        context.Response.Write(new JavaScriptSerializer().Serialize(list));
    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }

}