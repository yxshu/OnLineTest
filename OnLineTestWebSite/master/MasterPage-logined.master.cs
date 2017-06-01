using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using OnLineTest.BLL;
using OnLineTest.Model;
using System.Text;
using log4net;
using System.IO;

namespace OnLineTest.Web
{
    public partial class MasterPage_logined : System.Web.UI.MasterPage
    {
        protected Users user = new Users();
        protected UserGroup usergroup = new UserGroup();
        protected UserRank userrank = new UserRank();
        protected string userAuthority = string.Empty;
        ILog logger = LogManager.GetLogger(typeof(MasterPage_logined));
        AuthorityManager authoritymanager = new AuthorityManager();
        UserAuthorityManager userauthoirymanager = new UserAuthorityManager();
        UserRankManager userrankmanager = new UserRankManager();
        UserGroupManager usergroupmanager = new UserGroupManager();

        #region  页面载入,加载用户信息和用户权限树
        /// <summary>
        /// 页面载入,加载用户信息和用户权限树
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            //取得用户请求的页面名称
            string requestFileName = Request.FilePath.Remove(0, Request.FilePath.LastIndexOf("/") + 1);
            string RequestPath = Request.Path;
            //用户已经登录（要注意一个问题：用户是否有权限执行此操作？）
            if (Session["User"] != null)
            {
                //取得当前登录的用户实例
                user = (Users)Session["User"];
                try
                {
                    #region 得到用户所属于的组和等级
                    if (HttpRuntime.Cache[user.UserGroupId.ToString()] != null)
                    {

                        usergroup = (UserGroup)common.GetCacheBySql(user.UserGroupId.ToString());//通过缓存得到用户群组
                    }
                    else
                    {
                        usergroup = usergroupmanager.GetModel(user.UserGroupId);
                        common.SetCacheBySql(user.UserGroupId.ToString(), usergroup, common.initalSqlCacheDependency("UserGroup"));
                    }
                    if (HttpRuntime.Cache[user.UserScore.ToString()] != null)
                    {
                        userrank = (UserRank)common.GetCacheBySql(user.UserScore.ToString());//通过缓存得到用户等级
                    }
                    else
                    {
                        userrank = userrankmanager.GetModel(user);
                        common.SetCacheBySql(user.UserScore.ToString(), userrank, common.initalSqlCacheDependency("UserRank"));
                    }
                    #endregion

                    if (common.isAuthorized(user, requestFileName))//判断用户是否具有访问此页面的权限
                    {
                        userAuthority = initUserAuthority(user, usergroup, userrank);//初始化用户权限树，此处有庸余
                    }
                    else
                    {
                        common.ServerTransfer("error.aspx", 1006, "你无权访问此内容。", this.Page, string.Empty);//用户没有访问此页面的权限
                    }
                }
                catch (Exception ex)
                {
                    logger.Error(user.UserName + "用户信息和权限树初始化失败。", ex);
                    Session.Abandon();
                    common.ServerTransfer("error.aspx", 1005, ex, this.Page, RequestPath);
                }
            }
            //用户没有登录，执行跳转
            else
            {
                common.ServerTransfer("error.aspx", 1003, "请先登录。", this.Page, RequestPath);
            }
        }
        #endregion

        #region 初始化用户权限
        /// <summary>
        /// 初始化用户权限
        /// </summary>
        /// <param name="user"></param>
        /// <param name="usergroup"></param>
        /// <param name="userrank"></param>
        /// <returns></returns>
        private string initUserAuthority(Users user, UserGroup usergroup, UserRank userrank)
        {
            List<UserAuthority> userauthoritylist = new List<UserAuthority>();//用户权限，注意：权限是以ID表示的，没有进行实例化
            List<Authority> authoritylist = new List<Authority>();//进行实例化以后的用户权限

            #region 实例化userauthoritylist，得到用户权限列表
            if (HttpRuntime.Cache["usergroupid:" + usergroup.UserGroupId.ToString() + "userrankid:" + userrank.UserRankId.ToString()] != null)
            {
                userauthoritylist = (List<UserAuthority>)common.GetCacheBySql("usergroupid:" + usergroup.UserGroupId.ToString() + "userrankid:" + userrank.UserRankId.ToString());
                //通过缓存得到用户权限列表
            }
            else
            {
                if (usergroup.UserGroupName.Trim() != "超级管理员")
                {
                    userauthoritylist = userauthoirymanager.GetModelList(string.Format("UserGroupId='{0}' and UserRankId={1}", usergroup.UserGroupId, userrank.UserRankId));//根据用户的用户组和等级得到用户的权限
                }
                common.SetCacheBySql("usergroupid:" + usergroup.UserGroupId.ToString() + "userrankid:" + userrank.UserRankId.ToString(), userauthoritylist, common.initalSqlCacheDependency("UserAuthority"));
            }
            #endregion

            #region 实例化authoritylist并得到用户权限的最大深度
            int maxDeep;
            if (Cache["authoritylist:" + usergroup.UserGroupId + userrank.UserRankId] != null && Cache["maxdeep:" + usergroup.UserGroupId + userrank.UserRankId] != null)
            {
                authoritylist = (List<Authority>)Cache["authoritylist:" + usergroup.UserGroupId + userrank.UserRankId];//从缓存中得到数据
                maxDeep = Convert.ToInt32(Cache["maxdeep:" + usergroup.UserGroupId + userrank.UserRankId]);
            }
            else
            {
                if (usergroup.UserGroupName.Trim() != "超级管理员")
                {
                    authoritylist = getauthoritylistAndmaxDeep(userauthoritylist, out maxDeep);//得到用户的权限（是个外键）进行实例化
                }
                else
                {
                    authoritylist = authoritymanager.GetModelList("");
                    maxDeep = authoritylistMaxDeep(authoritylist);
                }
                common.SetCacheBySql("authoritylist:" + usergroup.UserGroupId + userrank.UserRankId, authoritylist, common.initalSqlCacheDependency("Authority"));
                Cache.Remove("maxdeep:" + usergroup.UserGroupId + userrank.UserRankId);
                Cache.Insert("maxdeep:" + usergroup.UserGroupId + userrank.UserRankId, maxDeep, null, System.Web.Caching.Cache.NoAbsoluteExpiration, System.Web.Caching.Cache.NoSlidingExpiration);
            }
            #endregion

            if (authoritylist.Count > 0)
            {
                List<Authority>[] arr = classifyAuthority(authoritylist, maxDeep);//将得到的用户权限按层级分类按点击次数排序到数组arr中，数组的下标即是层级
                return createUserAuthorityList(arr, maxDeep);//将用户的权限以html的形式返回
            }
            else
            {
                return "  用户权限为空";
            }
        }
        #endregion

        #region 取得权限列表的最大深度
        private int authoritylistMaxDeep(List<Authority> authoritylist)
        {
            int i = 0;
            foreach (Authority authority in authoritylist)
            {
                if (authority.AuthorityDeep > i)
                    i = authority.AuthorityDeep;
            }
            return i;
        }
        #endregion

        #region 根据权限生成前台可以使用的HTML代码
        /// <summary>
        /// 根据权限生成前台可以使用的HTML代码
        /// </summary>
        /// <param name="arr"></param>
        /// <param name="maxDeep"></param>
        /// <returns></returns>
        private string createUserAuthorityList(List<Authority>[] arr, int maxDeep)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < arr[0].Count; i++)
            {
                sb.AppendLine(string.Format("<div id='{0}' class='{1}'>", arr[0][i].AuthorityId, "authority"));
                sb.AppendLine(structAuthority(arr[0][i]));//将用户的权限进行格式化
                sb.AppendLine("<div class='authorityChilds'>");
                sb.AppendLine(findChildAuthority(arr[0][i], arr, maxDeep));//查找用户权限的子权限
                sb.AppendLine("</div>");
                sb.AppendLine("</div>");
            }
            return sb.ToString();
        }
        #endregion

        #region 将每一个权限生成特定的HTML代码以与CSS配合使用
        /// <summary>
        /// 将每一个权限生成特定的HTML代码以与CSS配合使用
        /// </summary>
        /// <param name="authority"></param>
        /// <returns></returns>
        private string structAuthority(Authority authority)
        {
            StringBuilder sb = new StringBuilder();
            if (authority.AuthorityDeep != 0)//子节点，无图标，有链接
            {

                object[] obj = new object[]{
                ResolveUrl(string.Format("~\\{0}", (authoritymanager.GetModel(authority.AuthorityParentId)).AuthorityHandlerPage+"\\"+authority.AuthorityHandlerPage)),
                authority.AuthorityName
                };

                sb.AppendLine(string.Format("<a  href='{0}'>{1}</a><br />", obj));
            }
            else //根目录，带图标的，无链接
            {
                string imageUrl = ResolveUrl(string.Format("~\\Images\\{0}.png", "DefaultAuthorityTitle"));
                if (File.Exists(Server.MapPath(string.Format("~\\Images\\{0}.png", authority.AuthorityHandlerPage))))
                {

                    imageUrl = ResolveUrl(string.Format("~\\Images\\{0}.png", authority.AuthorityHandlerPage));
                }
                object[] obj = new object[]{
                imageUrl,
                authority.AuthorityName,
                authority.AuthorityName,
                "authorityTitle",
                authority.AuthorityId
                };

                sb.AppendLine(string.Format("<div id='{4}Title' class='{3}'><img src='{0}' alt='{1}' />{2}", obj));
                sb.AppendLine("</div>");
            }
            return sb.ToString();
        }
        #endregion

        #region 递归的方法，查找权限的下一级
        /// <summary>
        /// 递归的方法，查找权限的下一级
        /// </summary>
        /// <param name="authority_parent"></param>
        /// <param name="arr"></param>
        /// <param name="maxDeep"></param>
        /// <returns></returns>
        private string findChildAuthority(Authority authority_parent, List<Authority>[] arr, int maxDeep)
        {
            StringBuilder sb = new StringBuilder();
            if (authority_parent.AuthorityDeep < maxDeep)//节点的深度
            {
                foreach (Authority childAuthority in arr[authority_parent.AuthorityDeep + 1])
                {
                    if (childAuthority.AuthorityParentId == authority_parent.AuthorityId)
                    {
                        sb.AppendLine(structAuthority(childAuthority));
                        findChildAuthority(childAuthority, arr, maxDeep);
                    }
                }
            }
            return sb.ToString();
        }
        #endregion

        #region 获取用户权限列表实例化以及通过输出参数得到用户权限列表的最大深度
        /// <summary>
        /// 获取用户权限列表以及通过输出参数得到用户权限列表的最大深度
        /// </summary>
        /// <param name="userauthoritylist">根据用户组别和等级得到的带有权限ID的权限列表</param>
        /// <param name="maxDeep">用户权限的深度(注意从0开始算起)</param>
        /// <returns>List<Authority>用户权限列表实例</returns>
        private List<Authority> getauthoritylistAndmaxDeep(List<UserAuthority> userauthoritylist, out int maxDeep)
        {
            maxDeep = 0;
            List<Authority> authoritylist = new List<Authority>();
            foreach (UserAuthority userauthority in userauthoritylist)
            {
                List<Authority> list = GetAuthorityAndParentAuthorityByUserAuthority(userauthority);
                for (int i = 0; i < list.Count; i++)
                {
                    int isExit = 0;
                    for (int k = 0; k < authoritylist.Count; k++)
                    {
                        if (list[i].AuthorityId == authoritylist[k].AuthorityId)
                        {
                            isExit = 1;
                            break;
                        }
                    }
                    if (isExit == 0)
                    {
                        authoritylist.Add(list[i]);
                        if (list[i].AuthorityDeep > maxDeep)
                        {
                            maxDeep = list[i].AuthorityDeep;
                        }
                    }
                }

            }
            return authoritylist;
        }
        //根据用户权限实例化并返回父权限
        private List<Authority> GetAuthorityAndParentAuthorityByUserAuthority(UserAuthority userauthority)
        {
            List<Authority> list = new List<Authority>();
            Authority authority = authoritymanager.GetModel(userauthority.AuthorityId);
            if (authority.AuthorityParentId != 0)
            {
                list.Add(authoritymanager.GetModel(authority.AuthorityParentId));
            }
            list.Add(authority);
            return list;
        }
        #endregion


        #region 将得到的用户权限按深度进行分类，并按被点击的次数进行排序，以方便后面查询
        /// <summary>
        /// 将得到的用户权限按深度进行分类，并按被点击的次数进行排序，以方便后面查询
        /// </summary>
        /// <param name="authoritylist">用户权限列表</param>
        /// <param name="maxDeep">权限的深度（注意：得到的值是从0开始算起）</param>
        /// <returns>按深度排序的用户权限数组</returns>
        private List<Authority>[] classifyAuthority(List<Authority> authoritylist, int maxDeep)
        {
            //int[] array=new int[3];
            //int[] array=new int{0,1,2};
            int listNum = maxDeep + 1;//数组的长度
            List<Authority>[] arr = new List<Authority>[listNum];
            for (int i = 0; i < listNum; i++)
            {
                List<Authority> list = new List<Authority>();
                arr[i] = list;
            }
            foreach (Authority authority in authoritylist)
            {
                arr[authority.AuthorityDeep].Add(authority);//这里的下标都是从0开始的
            }
            for (int i = 0; i < arr.Length; i++)
            {
                OrderAuthoryByOrderNum(arr[i]);//按照Authority.AuthorityOrderNum将每一组的值进行排序
            }
            return arr;
        }
        #endregion

        #region 排序用户权限
        /// <summary>
        /// 按照Authority.AuthorityOrderNum(从小大到大)将AuthorityList进行排序
        /// </summary>
        /// <param name="list"></param>
        private void OrderAuthoryByOrderNum(List<Authority> list)
        {
            if (list.Count > 0)
            {
                Authority min = list[0];
                int j = list.Count;
                while (j > 0)
                {
                    int k = 0;
                    for (int i = 0; i < j; i++)
                    {
                        if (list[i].AuthorityOrderNum > min.AuthorityOrderNum)
                        {
                            min = list[i];
                            k = i;
                        }
                    }
                    Authority temp = list[j - 1];
                    list[j - 1] = list[k];
                    list[k] = temp;
                    min = list[0];
                    j--;

                }
            }
        }
        #endregion
    }
}
