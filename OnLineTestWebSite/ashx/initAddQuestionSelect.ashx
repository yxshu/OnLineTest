<%@ WebHandler Language="C#" Class="initAddQuestionSelect" %>

using System;
using System.Web;
using System.Web.SessionState;
using OnLineTest.Model;
using OnLineTest.BLL;
using System.Collections.Generic;
using System.Web.Script.Serialization;
public class initAddQuestionSelect : IHttpHandler, IReadOnlySessionState
{
    /// <summary>
    /// 初始化AddQuestion页面加载过程中所需要使用的数据，其中包括三个值：1、作者（UserName）;2、难度系数 List<Difficulty> diffucluty；3、试题类型（包括科目）List<Dictionary<string, object>> papercode
    /// </summary>
    /// <param name="context"></param>
    public void ProcessRequest(HttpContext context)
    {
        List<object> result = new List<object>();//用于存放结果集
        JavaScriptSerializer serializer = new JavaScriptSerializer();
        string UserName = string.Empty;//第一个数据：用户名
        List<Difficulty> diffucluty = new List<Difficulty>();//第二个数据：难度系数
        DifficultyManager dm = new DifficultyManager();
        List<Dictionary<string, object>> papercode = new List<Dictionary<string, object>>();//第三个数据：试题类型,因为要将其中的subjectid实例化，所以用字典存数据
        PaperCodesManager pm = new PaperCodesManager();
        if (context.Session["User"] != null)
        {
            UserName = ((Users)context.Session["User"]).UserName;
            result.Add(UserName);
        }
        try
        {
            diffucluty = dm.GetModelList("");
            papercode = pm.GetPaperCodeAndInstantiation(" order by ChineseName");
        }
        catch (Exception ex)
        {
            throw ex;
        }
        result.Add(diffucluty);
        result.Add(papercode);
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