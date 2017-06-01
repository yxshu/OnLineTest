<%@ WebHandler Language="C#" Class="GetComment" %>

using System;
using System.Web;
using System.Web.Script.Serialization;
using System.Collections.Generic;
using OnLineTest.Model;
using OnLineTest.BLL;
using System.Data;
/// <summary>
/// 查询评论
/// 如果默认是获取当前用户发表的评论
/// 如果用户提交了“QuestionId”，则获取关于此试题的评论
/// 另外个参数是“step”，表示当前的页码值，每次给10条评论
/// </summary>
public class GetComment : IHttpHandler, System.Web.SessionState.IRequiresSessionState
{
    /// <summary>
    /// 查询当前用户发表的评论,每次取10条记录
    /// </summary>
    /// <param name="context"></param>
    public void ProcessRequest(HttpContext context)
    {
        int step = 0, QuestionId = -1;//用户表示查询的序号，第一次为0，每次取10个集合id为1-10
        Users CurrentUser = null;
        if (!int.TryParse(context.Request.Form["step"].ToString(), out step)) return;
        JavaScriptSerializer serializer = new JavaScriptSerializer();
        List<Dictionary<string, object>> result = new List<Dictionary<string, object>>();
        Comment comment = new Comment();
        CommentManager commentmanager = new CommentManager();
        if (string.IsNullOrEmpty(context.Request.Form["QuestionId"]))
        {
            CurrentUser = context.Session["User"] != null ? (Users)context.Session["User"] : null;//获取当前登录的用户
            result = commentmanager.GetList(10, CurrentUser.UserId, step);//参数说明：10表示每次取几个结果，userid不说了，step表示从第几个开始取值
        }
        else
        {
            if (Int32.TryParse(context.Request.Form["QuestionId"], out QuestionId) && new QuestionManager().Exists(QuestionId))
            {
                result = commentmanager.GetListByQuestionId(10, QuestionId, step);
            }
            else
            {
                return;
            }
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