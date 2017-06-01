<%@ WebHandler Language="C#" Class="getQuestion" %>

using System;
using System.Web;
using OnLineTest.Model;
using OnLineTest.BLL;
using System.Web.Script.Serialization;
using System.Collections.Generic;
using System.Web.SessionState;
public class getQuestion : IHttpHandler, IRequiresSessionState
{
    //
    /// <summary>
    /// 根据当前登录的用户
    /// 每次生成10条记录 order by "IsSupported" where IsVerified=true and IsDelete=false
    /// 其中获取的试题是 用户上传的，已经审核通过的，没有被删除的数据
    /// 其中提交的数据要求包含“int pagenum”
    /// </summary>
    /// <param name="context"></param>
    public void ProcessRequest(HttpContext context)
    {
        int pagenum;
        int userid = context.Session != null ? ((Users)context.Session["User"]).UserId : -1;//当前用户ID
        if (!Int32.TryParse(context.Request.Form["pagenum"], out pagenum))
        {
            pagenum = 0;
        };
        List<Dictionary<string, object>> result = null;
        JavaScriptSerializer serializer = new JavaScriptSerializer();
        QuestionManager questionmanager = new QuestionManager();
        if (userid > 0)
        {
            result = questionmanager.GetListByPage(10, pagenum, userid);
        };
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