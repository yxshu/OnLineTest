<%@ WebHandler Language="C#" Class="getLastContact" %>

using System;
using System.Web;
using System.Web.SessionState;
using System.Web.Script.Serialization;
using System.Collections.Generic;
using OnLineTest.Model;
using OnLineTest.BLL;

public class getLastContact : IHttpHandler, IRequiresSessionState
{
    //获取站内信中的最近联系人
    public void ProcessRequest(HttpContext context)
    {
        JavaScriptSerializer serializer = new JavaScriptSerializer();
        List<Dictionary<string, object>> result = null;
        Message_tableManager manager = new Message_tableManager();
        Users currentuser = context.Session["User"] != null ? (Users)context.Session["User"] : null;
        result = manager.getLastContact(currentuser);
        if (result != null)
        {
            Int32[] array = new Int32[result.Count];
            for (int i = 0; i < result.Count - 1; i++)
            {
                for (int j = i + 1; j < result.Count; j++)
                {
                    if (result[i]["id"].Equals(result[j]["id"]))
                    {
                        array[i] = -1;
                        break;
                    }
                    else
                    {
                        array[i] = 0;
                    }
                }
            }
            for (int i = result.Count - 1; i > 0; i--)
            {
                if (array[i] < 0)
                {
                    result.Remove(result[i]);
                }
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