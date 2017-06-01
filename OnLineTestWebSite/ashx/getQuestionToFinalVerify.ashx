<%@ WebHandler Language="C#" Class="getQuestionToFinalVerify" %>

using System;
using System.Web;
using System.Web.Script.Serialization;
using OnLineTest.Model;
using OnLineTest.BLL;
using System.Collections.Generic;
using System.Web.SessionState;
using System.Web.Script.Serialization;

/// <summary> 
/// /// 随机产生试题用于最终审核
/// 要求：
///     1、试题的IsVerity==false&&VerityTimes>2&&IsDeleted==false;
///     2、试题不是当前用户提交的
///     2、试题要求进行实例化
///     3、要求将前两次的审核结果一并显示出来
/// </summary>
public class getQuestionToFinalVerify : IHttpHandler, IReadOnlySessionState
{
    JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
    public void ProcessRequest(HttpContext context)
    {
        Users user = null;
        QuestionManager Questionmanager = new QuestionManager();//后面要用
        VerifyStatusManager Veritystatusmanager = new VerifyStatusManager();
        int looptime = 50;
        List<object> result = new List<object>();//用于保存结果
        if (context.Session["User"] != null)//取得当前用户
        {
            user = (Users)context.Session["User"];
        }

        //Dictionary<string, object> result_question = new Dictionary<string, object>();//用于保存结果
        int QuestionId = -1;
        for (int i = 0; i < looptime; i++)//利用十次机会随机产生ID，并验证产生的ID是否满足要求
        {
            QuestionId = new Random().Next(Questionmanager.GetMaxId());
            if (QuestionId > 0 && Questionmanager.Exists(QuestionId))
            {
                Question question = Questionmanager.GetModel(QuestionId);
                if (question.IsVerified == false && question.VerifyTimes > 2 && question.IsDelte == false && user != null && question.UserId != user.UserId)
                {
                    break;
                }
                else
                {
                    QuestionId = -1;
                }
            }
            else
            {
                QuestionId = -1;
            }
        }
        if (user != null && QuestionId > 0)
        {
            result.Add(Questionmanager.GetQuestionAndInstantiationById(QuestionId));//添加Question
            result.Add(Veritystatusmanager.GetVerifyStatusAndInstantiationByQuestionId(QuestionId));//添加审核状态
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