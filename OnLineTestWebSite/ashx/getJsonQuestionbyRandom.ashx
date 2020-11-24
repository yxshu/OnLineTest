<%@ WebHandler Language="C#" Class="getJsonQuestionbyRandom" %>

using System;
using System.Web;
using OnLineTest.Model;

public class getJsonQuestionbyRandom : IHttpHandler
{

    public void ProcessRequest(HttpContext context)
    {
        Users user = context.Session["User"] == null ? null : (Users)context.Session["User"];
        if (user == null) context.Response.Write("请先登录……"); context.Response.Flush();
        System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
        OnLineTest.BLL.QuestionManager manager = new OnLineTest.BLL.QuestionManager();
        OnLineTest.Model.Question question = manager.getQuestionbyRandom(user.UserId);
        //Console.WriteLine(question.QuestionId);
        context.Response.ContentType = "text/plain";
        context.Response.Write(serializer.Serialize(question));
        question = null;
        context.Response.Flush();
    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }

}