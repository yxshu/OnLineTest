<%@ WebHandler Language="C#" Class="getDifficulty" %>

using System;
using System.Web;
using System.Web.Script.Serialization;
using System.Collections.Generic;
using OnLineTest.BLL;
using OnLineTest.Model;
public class getDifficulty : IHttpHandler
{
    /// <summary>
    /// 给用户返回所有的难度系数数据
    /// </summary>
    /// <param name="context"></param>
    public void ProcessRequest(HttpContext context)
    {
        List<Difficulty> list = new List<Difficulty>();
        DifficultyManager manager = new DifficultyManager();
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