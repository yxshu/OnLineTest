
using System;
using System.Data;
using System.Collections.Generic;
using Maticsoft.Common;
using OnLineTest.Model;
namespace OnLineTest.BLL
{
    /// <summary>
    /// 用户权限实例
    /// </summary>
    public partial class UserAuthorityManager
    {
        /// <summary>
        /// 查询该用户是否存在某项权限
        /// </summary>
        /// <param name="user"></param>
        /// <param name="authority"></param>
        /// <returns></returns>
        public bool Exists(Users user, Authority authority)
        {
            UserGroup usergroup = new UserGroupManager().GetModel(user.UserGroupId);
            UserRank userrank = new UserRankManager().GetModel(user);
            return dal.Exists(usergroup, userrank, authority);
        }

        /// <summary>
        /// 查询某条记录是否存在
        /// </summary>
        /// <param name="user"></param>
        /// <param name="authority"></param>
        /// <returns></returns>
        public bool Exists(int usergroupid,int userrankid,int authorityid)
        {
            return dal.Exists(usergroupid, userrankid, authorityid);
        }
        /// <summary>
        /// 查询某项权限是否被引用
        /// </summary>
        /// <param name="authority"></param>
        /// <returns></returns>
        public bool Exists(Authority authority)
        {
            return dal.Exists(authority);
        }
        /// <summary>
        /// 获得用户权限列表并向右联合查询到对应的Authority
        /// </summary>
        public DataSet GetListLeftOuterJoinAuthority(string strWhere)
        {
            DataSet ds = dal.GetListLeftOuterJoinAuthority(strWhere);
            return ds;
        }
    }
}

