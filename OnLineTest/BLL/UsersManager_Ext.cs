
using System;
using System.Data;
using System.Collections.Generic;
using Maticsoft.Common;
using OnLineTest.Model;
namespace OnLineTest.BLL
{
    /// <summary>
    /// 用户定义的BLL层内的用户管理 部分类
    /// </summary>
    public partial class UsersManager
    {
        /// <summary>
        /// 是否存在该用户实例，并得到存在的数量
        /// </summary>
        public bool Exists(string UserName, string MD5PassWord, out int getCount)
        {
            return dal.Exists(UserName, MD5PassWord, out getCount);
        }

        public bool Exists(int userid)
        {
            return dal.Exists(userid);
        }
        public bool Exists(string username)
        {
            return dal.Exists(username);
        }
        /// <summary>
        /// 得到用户实例，根据用户名
        /// </summary>
        /// <param name="UserName"></param>
        /// <returns></returns>
        public Users GetModel(string UserName)
        {

            return dal.GetModel(UserName);
        }

        /// <summary>
        /// 得到当前用户的分数排名
        /// </summary>
        /// <param name="userid">当前用户</param>
        /// <returns>排名</returns>
        public int paiming(int userid)
        {
            return dal.paiming(userid);
        }
    }
}
