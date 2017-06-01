
using System;
using System.Data;
using System.Collections.Generic;
using Maticsoft.Common;
using OnLineTest.Model;
using OnLineTest.DAL;
namespace OnLineTest.BLL
{
    /// <summary>
    /// 用户论坛分的得分详细
    /// </summary>
    public partial class UserScoreDetailManager
    {
        /// <summary>
        /// 计算某个用户收入总和
        /// </summary>
        /// <param name="userid">用户id</param>
        /// <param name="num">总条数</param>
        /// <returns>收入总和</returns>
        public int shouru_total(int userid, out int num) { 
        
            return dal.shouru_total(userid,out num );
        }
        /// <summary>
        /// 计算某个用户收入总和
        /// </summary>
        /// <param name="userid">用户id</param>
        /// <returns>收入总和</returns>
        public int shouru_total(int userid)
        {
            int num;
            return dal.shouru_total(userid, out num);
        }
        /// <summary>
        /// 计算某个用户支出总和
        /// </summary>
        /// <param name="userid">用户id</param>
        /// <param name="num">总条数</param>
        /// <returns>支出总和</returns>
        public int zhichu_total(int userid, out int num)
        {
            return dal.zhichu_total(userid, out num);
        }
        /// <summary>
        /// 计算某个用户支出总和
        /// </summary>
        /// <param name="userid">用户id</param>
        /// <returns>支出总和</returns>
        public int zhichu_total(int userid)
        {
            int num;
            return dal.zhichu_total(userid, out num);
        }
        /// <summary>
        /// 根据用户id，联合查询某个用户的得分明细，其中包括三个模型：userscoredetail,userauthority,authoriy
        /// </summary>
        /// <param name="userid">用户ID</param>
        /// <returns>返回一个联合查询结果的数据集</returns>
        public DataSet userscoredetail(int userid)
        {
            return dal.userscoredetail(userid);
        }
    }
}

