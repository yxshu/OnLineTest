<%@ WebHandler Language="C#" Class="GetPastExamPaperPeriodNoByExamZoneId" %>

using System;
using System.Web;
using System.Web.Script.Serialization;
using System.Collections.Generic;
using OnLineTest.Model;
using OnLineTest.BLL;
/// <summary>
/// //根据考区和试卷代码获取考试期数
/// </summary>
public class GetPastExamPaperPeriodNoByExamZoneId : IHttpHandler
{

    public void ProcessRequest(HttpContext context)
    {
        List<PastExamPaper> result = new List<PastExamPaper>();
        JavaScriptSerializer serializer = new JavaScriptSerializer();
        Int32 ExamZoneId = -1, PaperCodeId = -1;
        if (Int32.TryParse(context.Request.Form["ExamZoneId"], out ExamZoneId) && Int32.TryParse(context.Request.Form["PaperCodeId"], out PaperCodeId) && ExamZoneId > 0 && PaperCodeId > 0)
        {
            PastExamPaperManager manager = new PastExamPaperManager();
            try
            {
                result = manager.GetModelList("PaperCodeId=" + PaperCodeId + " and ExamZoneId=" + ExamZoneId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
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