<%@ WebHandler Language="C#" Class="InsertComment" %>

using System;
using System.Web;
using System.Web.SessionState;
using OnLineTest.Model;
using OnLineTest.BLL;
using System.Web.Script.Serialization;
/// <summary>
/// 处理用户提交的试题评论
///  "Comment": comment_val, "QuestionId": $("#questionid").text()
/// </summary>
public class InsertComment : IHttpHandler, IReadOnlySessionState
{

    public void ProcessRequest(HttpContext context)
    {
        Users user = new Users();
        if (context.Session["User"] != null)
            user = (Users)context.Session["User"];
        JavaScriptSerializer serializer = new JavaScriptSerializer();
        int QuoteCommentId = -1;
        string CommentContent = context.Server.HtmlEncode(context.Request.Form["Comment"].Trim());//用户提交的评论内容
        int QuestionId = -1;//评论所对应的试题编号
        QuestionManager Qmanager = new QuestionManager();
        UsersManager Umanager = new UsersManager();
        if (!Int32.TryParse(context.Request.Form["QuestionId"], out QuestionId) && Qmanager.Exists(QuestionId) && Umanager.Exists(user.UserId) && string.IsNullOrEmpty(CommentContent)) return;//提取用户提交的值
        Int32.TryParse(context.Request.Form["QuoteCommentId"], out QuoteCommentId);
        Comment comment = new Comment();
        CommentManager manager = new CommentManager();
        comment.CommentContent = CommentContent;
        comment.QuestionId = QuestionId;
        comment.UserId = user.UserId;
        if (QuoteCommentId > 0 && manager.Exists(QuoteCommentId))
        {
            comment.QuoteCommentId = QuoteCommentId;
        }
        comment.CommentId = manager.Add(comment);
        common.CreateIndexofCommnet(new System.IO.DirectoryInfo(System.Configuration.ConfigurationManager.AppSettings["CreateIndexDirectionPath"]), comment);//将评论生成索引保存
        context.Response.ContentType = "text/plain";
        context.Response.Write(serializer.Serialize(comment.CommentId));
    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }

}