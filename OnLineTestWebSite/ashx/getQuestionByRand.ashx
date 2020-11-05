<%@ WebHandler Language="C#" Class="getQuestionByRand" %>

using System;
using System.Web;
using OnLineTest.Model;
using OnLineTest.BLL;
/// <summary>
///  随机抽取一条试题
/// 要求1、已经审核通过；2、没有被软删除
/// </summary>
public class getQuestionByRand : IHttpHandler
{

    public void ProcessRequest(HttpContext context)
    {
        Question question = new Question();
        QuestionManager manager = new QuestionManager();
        question = manager.getQuestionbyRandom();
        context.Response.ContentType = "text/plain";
        context.Response.Write(new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(question));
    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }

}