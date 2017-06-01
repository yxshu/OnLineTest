<%@ WebHandler Language="C#" Class="UserGroupHandler" %>

using System;
using System.Web;
using OnLineTest.BLL;
using OnLineTest.Model;
using System.Web.Script.Serialization;

public class UserGroupHandler : IHttpHandler
{

    //处理UsreRank对象
    string result = string.Empty;
    UserGroupManager UGM = new UserGroupManager();
    JavaScriptSerializer serializer = new JavaScriptSerializer();
    OnLineTest.Model.UserGroup usergroup = null;
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
        int usergroupid;
        if (Int32.TryParse(context.Request.Form["id"], out usergroupid))
        {
            usergroup = UGM.GetModel(usergroupid);
            result = serializer.Serialize(usergroup);
        }
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