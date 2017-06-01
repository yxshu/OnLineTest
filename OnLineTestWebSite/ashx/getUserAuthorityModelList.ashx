<%@ WebHandler Language="C#" Class="getUserAuthorityModelList" %>

using System;
using System.Web;
using OnLineTest.Model;
using OnLineTest.BLL;
using System.Collections.Generic;

public class getUserAuthorityModelList : IHttpHandler
{

    public void ProcessRequest(HttpContext context)
    {
        string result = string.Empty;
        int usergroupid, userrankid;
        if (Int32.TryParse(context.Request.Form["UserGroupId"], out usergroupid) && Int32.TryParse(context.Request.Form["UserRankId"], out userrankid))
        {
            List<Authority> list = null;
            AuthorityManager authoritymanager = new AuthorityManager();
            //select * from Authority where AuthorityId not in(select distinct(AuthorityId) from UserAuthority where UserGroupId=7 and UserRankId=21) and AuthorityParentId <>0
            list = authoritymanager.GetModelList("AuthorityId not in(select distinct(AuthorityId) from UserAuthority where UserGroupId=" + usergroupid + " and UserRankId=" + userrankid + ") and AuthorityParentId <>0");
            System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            result = serializer.Serialize(list);
        }
        context.Response.Write(result);
    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }

}