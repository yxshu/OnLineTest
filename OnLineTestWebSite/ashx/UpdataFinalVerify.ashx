<%@ WebHandler Language="C#" Class="UpdataFinalVerify" %>

using System;
using System.Web;
using OnLineTest.Model;
using OnLineTest.BLL;
using System.Web.SessionState;

public class UpdataFinalVerify : IHttpHandler, IReadOnlySessionState
{
    /// <summary>
    /// 处理用户提交的 最终审核试题 结果
    /// 提交的数据包括：{ judge: judge, QuestionId: QuestionId }
    /// 其中  judge=true?false,questionid：你懂的
    /// 要执行的动作包括：1、更新Question表，VerifyTime++和IsVerify=1;
    ///                   2、在VerifyStatus表中新增一条记录  
    /// </summary>
    /// <param name="context"></param>
    public void ProcessRequest(HttpContext context)
    {
        bool result = false;//最终的结果，默认值为false;
        bool passable;
        string judge = context.Request.Form.HasKeys() ? context.Request.Form["judge"] : null;//取值：用户的判断结果
        int QuestionId = context.Request.Form.HasKeys() ? Convert.ToInt32(context.Request.Form["QuestionId"]) : -1;//取值：用户审核的试题编号
        Users user = null;
        if (context.Session["User"] != null)
        {
            user = (Users)context.Session["User"];
        }
        if (!string.IsNullOrEmpty(judge) && QuestionId > 0 && user != null)//正确的获取到数据
        {
            QuestionManager QManager = new QuestionManager();
            if (QManager.Exists(QuestionId) && Boolean.TryParse(judge, out passable))//试题记录存在
            {
                Question Question = QManager.GetModel(QuestionId);
                if (Question.IsVerified == false && Question.VerifyTimes > 2 && Question.IsDelte == false && user.UserId != Question.UserId)//判断用户提交的试题是否满足要求
                {
                    //下面提交一个事务进行处理
                    //1、更新Question表，VerifyTime++和IsVerify=1;
                    //2、在VerifyStatus表中新增一条记录
                    result = QManager.UpdataQuestionFinalVerifyByTransaction(QuestionId, passable, user.UserId);
                }
            }
        }
        context.Response.ContentType = "text/plain";
        context.Response.Write(new System.Web.Script.Serialization.JavaScriptSerializer().Serialize(result));
    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }

}