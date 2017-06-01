<%@ WebHandler Language="C#" Class="GetAllExamZone" %>

using System;
using System.Web;
using System.Web.Script.Serialization;
using System.Collections.Generic;
using OnLineTest.Model;
using OnLineTest.BLL;
/// <summary>
/// 获取所有考区信息
/// </summary>
public class GetAllExamZone : IHttpHandler
{

    public void ProcessRequest(HttpContext context)
    {
        JavaScriptSerializer serializer = new JavaScriptSerializer();
        List<ExamZone> result = new List<ExamZone>();
        ExamZoneManager manager = new ExamZoneManager();
        result = manager.GetModelList("");
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