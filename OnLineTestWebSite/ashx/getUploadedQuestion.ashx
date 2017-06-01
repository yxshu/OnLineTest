<%@ WebHandler Language="C#" Class="getUploadedQuestion" %>

using System;
using System.Web;
using System.Web.SessionState;
using OnLineTest.Model;
using OnLineTest.BLL;
using System.Web.Script.Serialization;
using System.Collections.Generic;
public class getUploadedQuestion : IHttpHandler, IRequiresSessionState
{

    public void ProcessRequest(HttpContext context)
    {
        int pagenum;//提交的页码值

        int userid = context.Session["User"] != null ? ((Users)context.Session["User"]).UserId : -1;//当前用户ID
        if (!Int32.TryParse(context.Request.Form["pagenum"], out pagenum))
        {
            pagenum = 0;
        };
        List<Dictionary<string, object>> result = null;//每一个DICTIONARY是一个QUESTION，其中包含所有的属性（string:属性名，object:属性值）
        JavaScriptSerializer serializer = new JavaScriptSerializer();
        QuestionManager questionmanager = new QuestionManager();
        if (userid > 0)
        {
            result = questionmanager.GetAllUpLoadedQuestionByPage(10, pagenum, userid);
        }
        context.Response.ContentType = "text/plain";
        context.Response.Write(serializer.Serialize(result));
    }
    public bool IsReusable
    {
        get
        {
            return false;
        }
    }

}