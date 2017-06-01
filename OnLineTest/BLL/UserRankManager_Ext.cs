
using System;
using System.Data;
using System.Collections.Generic;
using Maticsoft.Common;
using OnLineTest.Model;
using System.Web;
using log4net;
namespace OnLineTest.BLL
{
    /// <summary>
    /// 用户等级实例
    /// </summary>
    public partial class UserRankManager
    {
        ILog logger = LogManager.GetLogger(typeof(UserRankManager));
        /// <summary>
        /// 得到用户等级实例，根据用户的论坛分值
        /// </summary>
        /// <param name="UserScore">用户论坛分值</param>
        /// <returns>返回用户等级实例</returns>
        public OnLineTest.Model.UserRank GetModel(Users user)
        {
            UserRank userrank = new UserRank();
            if (HttpRuntime.Cache["userscore:" + user.UserScore.ToString() + "touserrank"] != null)
            {
                userrank = (UserRank)HttpRuntime.Cache["userscore:" + user.UserScore.ToString() + "touserrank"];
            }
            else
            {
                userrank = dal.GetModel(user);
                HttpRuntime.Cache.Insert("userscore:" + user.UserScore.ToString() + "touserrank", userrank, null, DateTime.Now.AddMinutes(30), TimeSpan.Zero);
            }
            return userrank;
        }

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int UserRankId)
        {
            return dal.Exists(UserRankId);
        }

                /// <summary>
        /// 得到当前用户的下一个等级
        /// </summary>
        /// <param name="userid">当前用户</param>
        /// <returns>userrank</returns>
        public UserRank GetNextRank(int userid)
        {
            return dal.GetNextRank(userid);
        }
    }
}

