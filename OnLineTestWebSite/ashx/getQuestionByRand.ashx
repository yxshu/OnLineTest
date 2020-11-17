<%@ WebHandler Language="C#" Class="getQuestionByRand" %>

using System;
using System.Web;
using OnLineTest.Model;
using OnLineTest.BLL;
using System.Web.Script.Serialization;
/// <summary>
///  随机抽取一条试题
/// 要求1、已经审核通过；2、没有被软删除
/// </summary>
public class getQuestionByRand : IHttpHandler
{

    public void ProcessRequest(HttpContext context)
    {
        QuestionManager questionManager = new QuestionManager();
        Question question = questionManager.getQuestionbyRandom();
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