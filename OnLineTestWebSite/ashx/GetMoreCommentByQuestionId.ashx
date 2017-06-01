<%@ WebHandler Language="C#" Class="GetMoreCommentByQuestionId" %>

using System;
using System.Web;
using System.Collections.Generic;
public class GetMoreCommentByQuestionId : IHttpHandler, System.Web.SessionState.IRequiresSessionState
{
    /// <summary>
    /// 根据QuestionID查询comment的内容,每次只返回10条记录,提交的参数包括:QuestionId和PgeNum
    /// </summary>
    /// <param name="context"></param>
    public void ProcessRequest(HttpContext context)
    {
        int QuestionId, pagenum;
        List<Dictionary<string, object>> result = new List<Dictionary<string, object>>();
        if (Int32.TryParse(context.Request.Form["QuestionId"], out QuestionId) && Int32.TryParse(context.Request.Form["PageNum"], out pagenum) && context.Session["User"] != null)
        {
            OnLineTest.BLL.CommentManager commentnamager = new OnLineTest.BLL.CommentManager();
            result = commentnamager.GetListByQuestionId(10, QuestionId, pagenum, ((OnLineTest.Model.Users)context.Session["User"]).UserId);
        }
        else
        {
            result = null;
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