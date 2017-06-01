<%@ WebHandler Language="C#" Class="DeleteComment" %>

using System;
using System.Web;
using OnLineTest.BLL;
using OnLineTest.Model;
using System.Web.SessionState;

public class DeleteComment : IHttpHandler, IRequiresSessionState
{
    //
    /// <summary>
    /// 根据CommentId删除Comment,返回值为bool型
    /// </summary>
    /// <param name="context"></param>
    public void ProcessRequest(HttpContext context)
    {
        bool result = false;
        int CommentId; CommentManager commentmanager = new CommentManager();
        if (Int32.TryParse(context.Request.Form["CommentId"], out CommentId) && context.Session["User"] != null && commentmanager.Exists(CommentId))
        {

            if (commentmanager.Delete(CommentId))
            {
                result = true;
            }
        }
        System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
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