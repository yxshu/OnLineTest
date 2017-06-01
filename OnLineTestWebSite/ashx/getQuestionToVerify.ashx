<%@ WebHandler Language="C#" Class="getQuestionToVerify" %>

using System;
using System.Web;
using System.Web.SessionState;
using OnLineTest.Model;
using OnLineTest.BLL;
using System.Collections.Generic;
using System.Web.Script.Serialization;

/// <summary>
/// 随机产生试题用于审核
/// 要求：
///     1、试题的IsVerity==false&&VerityTimes不大于3&&IsDeleted==false;
///     2、产生的试题不是当前用户提交的；
///     3、每个试题每个用户最多只能审核一次；
///     4、试题要求进行实例化
/// </summary>
public class getQuestionToVerify : IHttpHandler, IReadOnlySessionState
{
    public void ProcessRequest(HttpContext context)
    {
        Users user = null;//用于保存当前用户
        QuestionManager manager = new QuestionManager();
        Question question = new Question();//临时存放QuestionId;
        question.QuestionId = -1;//默认值
        int looptime = 50;//循环的最大次数
        Dictionary<string, object> result = null;//返回给前台的结果，有两种结果，1、null;2、实例化的question
        JavaScriptSerializer serializer = new JavaScriptSerializer();//序列化工具
        //取得当前用户
        if (context.Session["User"] != null)
        {
            user = (Users)context.Session["User"];
        }
        //生成随机数
        for (int i = 0; i < looptime; i++)
        {
            int id = new Random().Next(manager.GetMaxId());
            if (user != null && QuestionIdisUseable(user, id))
            {
                question.QuestionId = id;
                break;
            }
        }
        //如果进行最多10次循环产生的questionid能用，则进行实例化，产生有用的结果
        if (question.QuestionId > 0)
        {
            result = manager.GetQuestionAndInstantiationById(question.QuestionId);
        }
        context.Response.ContentType = "text/plain";
        context.Response.Write(serializer.Serialize(result));
    }

    //检测随机生成的QuestionId是否是满足要求的
    public static bool QuestionIdisUseable(Users user, int id)
    {
        bool result = false;
        QuestionManager manager = new QuestionManager();
        Question question = new Question();
        if (manager.Exists(id))//id对应的试题存在
        {
            question = manager.GetModel(id);
            if (question.UserId != user.UserId && question.IsVerified == false && question.VerifyTimes < 3 && question.IsDelte == false)//id对应的试题不是当前用户上传的，试题没有审核通过，试题审核次数不大于3次，试题没有被软删除
            {
                VerifyStatusManager Vmanager = new VerifyStatusManager();
                List<VerifyStatus> list = Vmanager.GetModelList("QuestionId=" + id);
                if (list.Count > 0)//试题没有被当前用户审核过
                {
                    for (int i = 0; i < list.Count; i++)
                    {
                        if (list[i].UserId == user.UserId)
                        {
                            break;
                        }
                        else if (i == list.Count-1)
                        {
                            result = true;
                        }
                    }
                }
                else
                {//试题还没有被审核过
                    result = true;
                }
            }
        }
        return result;
    }
    public bool IsReusable
    {
        get
        {
            return false;
        }
    }

}