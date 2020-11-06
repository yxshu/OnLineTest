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
        System.Collections.Generic.List<object> list = new System.Collections.Generic.List<object>();
        Question question = new Question();
        Difficulty difficulty = new Difficulty();
        Users users = new Users();
        PaperCodes paperCodes = new PaperCodes();
        PastExamPaper pastExamPaper = new PastExamPaper();
        QuestionManager manager = new QuestionManager();
        //System.Text.StringBuilder sb = new System.Text.StringBuilder();
        JavaScriptSerializer serializer = new JavaScriptSerializer();
        question = manager.getQuestionbyRandom();
        difficulty = new DifficultyManager().GetModel(question.DifficultyId);
        users = new UsersManager().GetModel(question.UserId);
        paperCodes = new PaperCodesManager().GetModel(question.PaperCodeId);
        list.Add(question);
        list.Add(difficulty);
        list.Add(users);
        list.Add(paperCodes);
        //sb.Append(serializer.Serialize(question)).Append(serializer.Serialize(difficulty)).Append(serializer.Serialize(users)).Append(serializer.Serialize(paperCodes));
        context.Response.ContentType = "text/plain";
        context.Response.Write(serializer.Serialize(list));
    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }

}