<%@ WebHandler Language="C#" Class="getQuestionByRand" %>

using System;
using System.Web;
using System.Web.SessionState;
using OnLineTest.Model;
using OnLineTest.BLL;
using System.Web.Script.Serialization;
/// <summary>
///  随机抽取一条试题
/// 要求1、已经审核通过；2、没有被软删除
/// </summary>
public class getQuestionByRand : IHttpHandler, IRequiresSessionState
{

    public void ProcessRequest(HttpContext context)
    {
        Users user = context.Session["User"] == null ? null : (Users)context.Session["User"];
        if (user == null)
        {
            context.Response.Write("请先登录……");
            context.Response.Flush();
            return;
        }
        QuestionManager questionManager = new QuestionManager();
        Question question = questionManager.getQuestionbyRandom(user.UserId);
        System.Collections.Generic.List<object> list = questionManager.GetQuestionAndInstantiationModelById(question.QuestionId);
        context.Response.ContentType = "text/plain";
        context.Response.Write(new JavaScriptSerializer().Serialize(list));

    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }

}