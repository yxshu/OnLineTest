
using System;
using System.Data;
using System.Collections.Generic;
using Maticsoft.Common;
using OnLineTest.Model;
namespace OnLineTest.BLL
{
    /// <summary>
    /// 记录平时练习的习题实
    /// </summary>
    public partial class LogPracticeManager
    {
        /// <summary>
        /// 根据给出的id,要求获取同一个用户的上一条记录
        /// </summary>
        /// <param name="Currentlogpracticeid">当前ID</param>
        /// <param name="userid">用户ID</param>
        /// <returns>返回当前用户的上一次记录</returns>
        public LogPractice getLastModel(int Currentlogpracticeid, int userid)
        {
            LogPractice logpractice = new LogPractice();
            if (dal.Exists(Currentlogpracticeid))
            {
                logpractice = dal.getLastModel(Currentlogpracticeid, userid);
            }
            else
                logpractice = null;
            return logpractice;
        }
    }
}

