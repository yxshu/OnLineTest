<%@ WebHandler Language="C#" Class="getJsonQuestionbyRandom" %>

using System;
using System.Web;

public class getJsonQuestionbyRandom : IHttpHandler
{

    public void ProcessRequest(HttpContext context)
    {
        System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
        OnLineTest.BLL.QuestionManager manager = new OnLineTest.BLL.QuestionManager();
        OnLineTest.Model.Question question = manager.getQuestionbyRandom();
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