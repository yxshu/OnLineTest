<%@ WebHandler Language="C#" Class="getPaperCodeBySubjectId" %>

using System;
using System.Web;
using System.Collections.Generic;
using System.Web.Script.Serialization;
using OnLineTest.Model;
using OnLineTest.BLL;

public class getPaperCodeBySubjectId : IHttpHandler
{
    /// <summary>
    /// 根据用户提交的SubjectId参数，查询papercodes集
    /// </summary>
    /// <param name="context"></param>
    public void ProcessRequest(HttpContext context)
    {
        List<PaperCodes> list = new List<PaperCodes>();
        int subjectid = -1;
        if (context.Request.Form.HasKeys())
        {
            if (!Int32.TryParse(context.Request.Form["SubjectId"].ToString(), out subjectid))
                return;
        }
        SubjectManager smanager = new SubjectManager();
        if (subjectid > 0 && smanager.Exists(subjectid))
        {
            PaperCodesManager manager = new PaperCodesManager();
            list = manager.GetModelListBySubjectId(subjectid);
        }
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