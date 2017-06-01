<%@ WebHandler Language="C#" Class="UserRankHandler" %>

using System;
using System.Web;
using System.Collections.Generic;
using System.Web.Script.Serialization;
using OnLineTest.BLL;
using OnLineTest.Model;
public class UserRankHandler : IHttpHandler
{
    //处理UsreRank对象
    string result=string.Empty;
    UserRankManager URM = new UserRankManager();
    OnLineTest.Model.UserRank userrank = null;
    JavaScriptSerializer serializer = new JavaScriptSerializer();
    public void ProcessRequest(HttpContext context)
    {
        string type = string.IsNullOrEmpty(context.Request.Form["type"]) ? "Query" : context.Request.Form["type"];//默认动作为 查询
        switch (type)
        {
            case "Add": 
                Add(context);
                break;
            case "Delete":
                Delete(context); 
                break;
            case "Update":
                Updata(context); 
                break;
            case "Query":
                Query(context);
                break;
            default: break;
        }
        context.Response.ContentType = "text/plain";
        context.Response.Write(result);
    }
    
    
    //下面自定义方法，处理各种请求
    private object Add(HttpContext context)
    {
        return result;
    }
    private object Delete(HttpContext context)
    {
        return result;
    }
    private object Updata(HttpContext context)
    {
        return result;
    }
    private object Query(HttpContext context)
    {
        int userrankid;
        if (Int32.TryParse(context.Request.Form["id"], out userrankid)) {
            userrank = URM.GetModel(userrankid);
            result = serializer.Serialize(userrank);
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