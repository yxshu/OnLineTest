<%@ WebHandler Language="C#" Class="AuthorityHandler" %>

using System;
using System.Web;
using System.Collections.Generic;
using System.Web.Script.Serialization;
using OnLineTest.BLL;
using OnLineTest.Model;
public class AuthorityHandler : IHttpHandler
{
    string result = string.Empty;
    AuthorityManager authoritymanager = new AuthorityManager();
    public void ProcessRequest(HttpContext context)
    {
        string type = string.IsNullOrEmpty(context.Request.Form["type"]) ? string.Empty : context.Request.Form["type"].ToString().Trim().ToUpper();//所有的请求都要求存在参数TYPE，用于识别用户意图
        switch (type)
        {
            case "ADD"://新增
                {
                    Authority authority = Add(context);
                    if (authority != null)
                        result = FormateRuselt(authority);
                    break;
                }
            case "DELETE"://删除
                {
                    Authority authority = Delete(context);
                    if (authority != null)
                        result = FormateRuselt(authority);
                    break;
                }
            case "UPDATE"://修改
                {
                    Authority authority = Update(context);
                    if (authority != null)
                        result = FormateRuselt(authority);
                    break;
                }
            case "QUERY"://查询
                {
                    Authority authority = Query(context);
                    if (authority != null)
                        result = FormateRuselt(authority);
                    break;
                }
            case "MAXID"://查询某个表中最大的ID
                result = MaxId(context).ToString();
                break;
            case "PREVMODEL"://根据ID查询某个authority,这里有两处情况，一、如果带有model参数，1、则查询此ID前面的一个authority，2、返回NULL，二、否则查询此ID的authority
                {
                    Authority authority = PrevModel(context);
                    if (authority != null)
                        result = FormateRuselt(authority);
                    break;
                }
            case "GETLISTBYDEEP"://根据深度获取权限列表
                {
                    List<Authority> authoritylist = GetListbyDeep(context);
                    if (authoritylist != null)
                        result = FormateRuselt(authoritylist);
                    break;
                }
            case "GETMAXORDERNUMBYCURRENTAUTHORITYPARENTID"://查询同一个父权限下所有子权限的最大序号
                result = GetMaxOrdernumByCurrentAuthorityParentId(context).ToString();
                break;
            case "ORDERUP"://序号向上调整一位
                {
                    Authority authority = OrderUp(context);
                    if (authority != null)
                        result = FormateRuselt(authority);
                    break;
                }
            case "ORDERDOWN"://序号向下调整一位
                {
                    Authority authority = OrderDown(context);
                    if (authority != null)
                        result = FormateRuselt(authority);
                    break;
                }
            default: break;//默认
        }
        context.Response.ContentType = "text/plain";
        context.Response.Write(result);
    }

    #region 初始化权限实例
    /// <summary>
    /// 从用户提交的数据中提取 用户权限 实例
    /// </summary>
    /// <param name="context"></param>
    /// <returns></returns>
    private Authority initial(HttpContext context)
    {
        Authority authority = new Authority();
        int id, deep, parentid, score, ordernum;
        authority.AuthorityId = (Int32.TryParse(context.Request["AuthorityId"], out  id) && id > 0) ? id : -1;
        authority.AuthorityName = !string.IsNullOrEmpty(context.Request["AuthorityName"]) && context.Request["AuthorityName"] != "请添加名称" ? context.Request["AuthorityName"] : string.Empty;
        authority.AuthorityDeep = Int32.TryParse(context.Request["AuthorityDeep"], out deep) && deep >= 0 ? deep : -1;
        authority.AuthorityParentId = Int32.TryParse(context.Request["AuthorityParentId"], out parentid) && parentid >= 0 ? parentid : -1;
        authority.AuthorityScore = Int32.TryParse(context.Request["AuthorityScore"], out score) ? score : 0;
        authority.AuthorityHandlerPage = !string.IsNullOrEmpty(context.Request["AuthorityHandlerPage"]) && context.Request["AuthorityHandlerPage"] != "请添加处理页面" ? context.Request["AuthorityHandlerPage"] : string.Empty;
        authority.AuthorityOrderNum = Int32.TryParse(context.Request["AuthorityOrderNum"], out ordernum) && ordernum > 0 ? ordernum : -1;
        authority.AuthorityRemark = context.Request["AuthorityRemark"];
        return authority;
    }
    #endregion

    #region 将结果格式化为字符串
    /// <summary>
    /// 将结果格式化为字符串
    /// </summary>
    /// <param name="obj"></param>
    /// <param name="result"></param>
    /// <returns></returns>
    private string FormateRuselt(object obj)
    {
        JavaScriptSerializer serializer = new JavaScriptSerializer();
        return serializer.Serialize(obj);
    }
    #endregion

    #region 新增
    private Authority Add(HttpContext context)
    {
        int rownum,//添加后的序号AuthorityId
         affectrows;//处理过程中，受影响的行数
        Authority authority = initial(context);
        rownum = authoritymanager.OperateAuthoritybyTran(authority, "add", out affectrows);
        if (rownum > 0)
            return authority = authoritymanager.GetModel(rownum);
        else return null;
    }
    #endregion

    #region 删除
    private Authority Delete(HttpContext context)
    {
        UserAuthorityManager userauthoritymanager = new UserAuthorityManager();
        Authority authority = initial(context);
        authority = authoritymanager.GetModel(authority.AuthorityId);
        if (!authoritymanager.ExistsChild(authority.AuthorityId) && !userauthoritymanager.Exists(authority))//如果不存在子项，且没有作为外键（主要是UserAuthority的外键）
        {
            int rownum,//添加后的序号AuthorityId
                affectrows;//处理过程中，受影响的行数
            rownum = authoritymanager.OperateAuthoritybyTran(authority, "delete", out affectrows);
            if (rownum <= 0)
                return null;
        }
        return authority;
    }
    #endregion

    #region 更新
    private Authority Update(HttpContext context)
    {
        int rownum,//添加后的序号AuthorityId
                affectrows;//处理过程中，受影响的行数
        Authority authority = initial(context);
        rownum = authoritymanager.OperateAuthoritybyTran(authority, "update", out affectrows);
        if (rownum > 0)
            return authoritymanager.GetModel(rownum);
        else return null;
    }
    #endregion

    #region 查询
    private Authority Query(HttpContext context)
    {
        Authority authority = initial(context);
        authority = authoritymanager.GetModel(authority.AuthorityId);
        return authority;
    }
    #endregion

    #region 查询某个表中最大的ID
    private Int32 MaxId(HttpContext context)
    {
        string tablename = context.Request["tablename"];
        Int32 result = -1;
        if (!string.IsNullOrEmpty(tablename))
        {
            result = authoritymanager.GetMaxId();
        }
        return result;
    }
    #endregion

    #region 根据ID查询某个authority,这里有两处情况，如果带有model参数，1、则查询此ID前面的一个authority，2、返回NULL，否则查询此ID的authority
    private Authority PrevModel(HttpContext context)
    {
        Authority prevmodel = null;
        Authority authority = initial(context);
        authority = authoritymanager.GetModel(authority.AuthorityId);
        if (context.Request["model"] == "prevmodel")//如果只有一条记录，则返回自身，否则如果存在多条记录，如果不是第一条，则返回前面一条，否则如果是第一条则返回后面的一条记录
        {
            List<Authority> list = authoritymanager.GetModelList("AuthorityParentId=" + authority.AuthorityParentId + "order by AuthorityOrderNum");
            if (list.Count > 0)
            {
                for (int i = 0; i < list.Count; i++)
                {
                    if (list[i].AuthorityId == authority.AuthorityId)
                    {
                        if (i > 0)//有多条记录，则返回前面的一条
                        {
                            prevmodel = list[i - 1];
                        }
                        else if (i == 0 && i + 1 < list.Count)//有多条记录，但此记录是第一条记录，则返回后面的一条
                        {
                            prevmodel = list[i + 1];
                        }
                        else//只有一条记录
                        {
                            if (authority.AuthorityParentId != 0)//此记录不是父元素，则返回此元素的父元素
                            {
                                prevmodel = authoritymanager.GetModel(authority.AuthorityParentId);
                            }
                            else//此记录是父元素，则返回自身
                            {
                                prevmodel = list[i];
                            }
                        }
                        break;
                    }
                }
            }
        }
        return prevmodel;
    }
    #endregion

    #region 根据深度获取权限列表
    /// <summary>
    /// 根据深度获取权限列表
    /// </summary>
    /// <param name="context"></param>
    /// <returns></returns>
    private List<Authority> GetListbyDeep(HttpContext context)
    {
        List<Authority> list = null;
        int deep;
        if (Int32.TryParse(context.Request["deep"], out  deep))
        {
            list = authoritymanager.GetModelList("AuthorityDeep=" + deep + "order by AuthorityOrderNum");
        }
        return list;
    }
    #endregion

    #region 查询同一个父权限下所有子权限的最大序号
    private Int32 GetMaxOrdernumByCurrentAuthorityParentId(HttpContext context)
    {
        string id = context.Request["parentid"];
        int parentid;
        int maxordernum = 0;
        if (!string.IsNullOrEmpty(id) && Int32.TryParse(id, out parentid))
        {
            List<Authority> list = new List<Authority>();
            list = authoritymanager.GetModelList("AuthorityParentId=" + parentid);
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i].AuthorityOrderNum > maxordernum)
                    maxordernum = (int)list[i].AuthorityOrderNum;
            }
        }
        return maxordernum;
    }
    #endregion

    #region 将权限实例在同样的父权限组下排序上升一位
    private Authority OrderUp(HttpContext context)
    {
        Authority temp = null;
        Authority authority = initial(context);
        authority = authoritymanager.GetModel(authority.AuthorityId);
        List<Authority> list = authoritymanager.GetModelList("AuthorityParentId=" + authority.AuthorityParentId + "order by AuthorityOrderNum");
        for (int i = 0; i < list.Count; i++)
        {
            if (list[i].AuthorityId == authority.AuthorityId && i - 1 >= 0)
            {
                temp = list[i - 1];
                break;
            }
        }
        if (temp != null)
        {
            int? order = temp.AuthorityOrderNum;
            temp.AuthorityOrderNum = authority.AuthorityOrderNum;
            authority.AuthorityOrderNum = order;
            if (authoritymanager.Update(temp) && authoritymanager.Update(authority))
                return authority;
            else return null;
        }
        else return null;
    }
    #endregion

    #region 将权限实例在同样的父权限组下排序下降一位
    private Authority OrderDown(HttpContext context)
    {
        Authority temp = null;
        Authority authority = initial(context);
        authority = authoritymanager.GetModel(authority.AuthorityId);
        List<Authority> list = authoritymanager.GetModelList("AuthorityParentId=" + authority.AuthorityParentId + "order by AuthorityOrderNum");
        for (int i = 0; i < list.Count; i++)
        {
            if (list[i].AuthorityId == authority.AuthorityId && i + 1 < list.Count)
            {
                temp = list[i + 1];
                break;
            }
        }
        if (temp != null)
        {
            int? order = temp.AuthorityOrderNum;
            temp.AuthorityOrderNum = authority.AuthorityOrderNum;
            authority.AuthorityOrderNum = order;
            if (authoritymanager.Update(temp) && authoritymanager.Update(authority))
                return authority;
            else return null;
        }
        else return null;
    }
    #endregion
    public bool IsReusable
    {
        get
        {
            return false;
        }
    }

}
