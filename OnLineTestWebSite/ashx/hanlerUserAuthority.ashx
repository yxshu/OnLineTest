<%@ WebHandler Language="C#" Class="hanlerUserAuthority" %>

using System;
using System.Web;
using OnLineTest.BLL;
using OnLineTest.Model;
public class hanlerUserAuthority : IHttpHandler
{
    //处理UserAuthoriry,结果：默认为0，删除成功1，删除失败-1，添加成功2，添加失败-2
    public void ProcessRequest(HttpContext context)
    {
        int result = 0;
        int UserAuthorityId, AuthorityId, UserGroupId, UserRankId;
        string UserAuthoriryRemark = context.Request.Form["UserAuthoriryRemark"].Trim();
        UserAuthorityManager userauthoritymanager = new UserAuthorityManager();
        if (Int32.TryParse(context.Request.Form["UserAuthorityId"].Trim(), out UserAuthorityId))
        {
            //如果存在，则为删除，否则为新增
            if (userauthoritymanager.Exists(UserAuthorityId))//存在即删除
            {
                if (userauthoritymanager.Delete(UserAuthorityId))
                    result = 1;
                else result = -1;
            }
        }
        else//不存在即为添加
        {
            if (
                Int32.TryParse(context.Request.Form["AuthorityId"].Trim(), out AuthorityId) &&
                Int32.TryParse(context.Request.Form["UserGroupId"].Trim(), out UserGroupId) &&
                Int32.TryParse(context.Request.Form["UserRankId"].Trim(), out UserRankId))
            {
                AuthorityManager authoritymanager = new AuthorityManager();
                UserGroupManager usergroupmanager = new UserGroupManager();
                UserRankManager userrankmanager = new UserRankManager();
                //判断提交的用户组，用户等级，权限和用户权限是否存在
                if (
                    authoritymanager.Exists(AuthorityId) &&
                    usergroupmanager.Exists(UserGroupId) &&
                    userrankmanager.Exists(UserRankId) &&
                    !userauthoritymanager.Exists(UserGroupId, UserRankId, AuthorityId)
                    )
                {
                    UserAuthority userauthority = new UserAuthority();
                    userauthority.AuthorityId = AuthorityId;
                    userauthority.UserGroupId = UserGroupId;
                    userauthority.UserRankId = UserRankId;
                    userauthority.UserAuthoriryRemark = string.IsNullOrEmpty(UserAuthoriryRemark) ? authoritymanager.GetModel(AuthorityId).AuthorityRemark : UserAuthoriryRemark;//备注用户权限中的备注
                    //以前考虑的是要添加的权限的父权限是否存在，如果不存在则添加（如果这样，则需要使用事务），后来决定不要添加父权限了，则要更新母版页了
                    //Authority pA = authoritymanager.GetModel(AuthorityId);
                    //if (!userauthoritymanager.Exists(UserGroupId, UserRankId, pA.AuthorityParentId))//如果此权限的父权限存在，则直接添加，否则要先加上其父权限
                    //{
                    //    UserAuthority pu = new UserAuthority();
                    //    pu.AuthorityId = pA.AuthorityParentId;
                    //    pu.UserGroupId = userauthority.UserGroupId;
                    //    pu.UserRankId = userauthority.UserRankId;
                    //    pu.UserAuthoriryRemark = pA.AuthorityRemark;
                    //    userauthoritymanager.Add(pu);
                    //}
                    if ((result = userauthoritymanager.Add(userauthority)) > 0)
                        result += 2;
                    else result = -2;

                }
            }
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