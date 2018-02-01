<%@ WebHandler Language="C#" Class="getPaperCode" %>

using System;
using System.Web;
using OnLineTest.Model;
using OnLineTest.BLL;
using System.Web.Script.Serialization;
using System.Collections.Generic;
public class getPaperCode : IHttpHandler
{

    public void ProcessRequest(HttpContext context)
    {
        JavaScriptSerializer serializer = new JavaScriptSerializer();
        PaperCodesManager manager = new PaperCodesManager();
        List<PaperCodes> list = manager.DataTableToList(manager.GetAllList().Tables[0]);
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