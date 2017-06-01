<%@ WebHandler Language="C#" Class="valid" %>

using System;
using System.Web;

public class valid : IHttpHandler, System.Web.SessionState.IRequiresSessionState
{
    //处理验证码是否过期
    public void ProcessRequest(HttpContext context)
    {
        object valid = context.Session["ValidCode"];
        string ValidCode = string.IsNullOrEmpty(context.Request.Form["validcode"]) ? context.Request.Form["name"] : context.Request.Form["validcode"];//获取提交的验证码，因为login和register两个地方用到，而且他们的名称不好统一，所以在这里作一个判断
        if (valid != null)
        {
            if (!string.IsNullOrEmpty(ValidCode) && valid.ToString().Replace('o', '0').Trim().ToLower() == ValidCode.Replace('o', '0').Trim().ToLower())
            {
                context.Response.Write("true");
            }
            else
            {
                context.Response.Write("false");
            }
        }
        else
        {
            context.Response.Write("验证码过期");
        }
    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }

}