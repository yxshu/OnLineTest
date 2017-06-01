
using System;
using System.Data;
using System.Collections.Generic;
using Maticsoft.Common;
using OnLineTest.Model;
namespace OnLineTest.BLL
{
    /// <summary>
    /// 保存平时测试信息实例
    /// </summary>
    public partial class LogTestManager
    {
        /// <summary>
        /// 按要求返回多个实例对象
        /// </summary>
        /// <param name="num">返回的对象数量</param>
        /// <param name="orderby">返回的是前几个对象，排序的原则</param>
        /// <returns>返回前几条对象</returns>
        public List<LogTest> GetModels(int num, string orderby, int userid)
        {
            
            return dal.GetModels(num, orderby, userid);
        }
        /// <summary>
        /// 按要求返回多个实例，并将所有的外链填充
        /// </summary>
        /// <param name="num"></param>
        /// <param name="orderby"></param>
        /// <param name="userid"></param>
        /// <returns></returns>
        public List<Dictionary<string, object>> GetModelsbyJoin(int num, string orderby, int userid)
        {
            return dal.GetModelsbyJoin(num, orderby, userid);
        }
    }
}

