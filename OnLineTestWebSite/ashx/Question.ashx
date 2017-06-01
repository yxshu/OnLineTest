<%@ WebHandler Language="C#" Class="QuestionHandler" %>

using System;
using System.Web;
using System.Web.Script.Serialization;
public class QuestionHandler : IHttpHandler
{
    //处理UsreRank对象
    string result = string.Empty;
    JavaScriptSerializer serializer = new JavaScriptSerializer();
    public void ProcessRequest(HttpContext context)
    {
        string type = string.IsNullOrEmpty(context.Request.Form["type"]) ? "Query" : context.Request.Form["type"];//默认动作为 查询
        switch (type)
        {
            case "Add":
                AddFunction(context);
                break;
            case "Delete":
                DeleteFunction(context);
                break;
            case "Update":
                UpdateFunction(context);
                break;
            case "Query":
                QueryFunction(context);
                break;
            default: break;
        }
        context.Response.ContentType = "text/plain";

        context.Response.Write(result);
    }


    //下面自定义方法，处理各种请求
    private object AddFunction(HttpContext context)
    {
        return result;
    }
    private object DeleteFunction(HttpContext context)
    {
        return result;
    }
    private object UpdateFunction(HttpContext context)
    {
        return result;
    }
    private object QueryFunction(HttpContext context)
    {
        return result;
    }


    public bool IsReusable
    {
        get
        {
            return false;
        }
    }

}
