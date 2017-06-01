<%@ WebHandler Language="C#" Class="getSubject" %>

using System;
using System.Web;
using OnLineTest.BLL;
using OnLineTest.Model;
using System.Collections.Generic;
using System.Web.Script.Serialization;
public class getSubject : IHttpHandler
{
    /// <summary>
    /// 给用户返回所有的学科数据
    /// </summary>
    /// <param name="context"></param>
    public void ProcessRequest(HttpContext context)
    {
        List<Subject> list = new List<Subject>();
        SubjectManager manager = new SubjectManager();
        try
        {
            list = manager.GetModelList("");
        }
        catch (Exception ex)
        {
            throw ex;
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