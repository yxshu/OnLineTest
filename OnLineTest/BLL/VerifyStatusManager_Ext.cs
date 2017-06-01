
using System;
using System.Data;
using System.Collections.Generic;
using Maticsoft.Common;
using OnLineTest.Model;
namespace OnLineTest.BLL
{
    /// <summary>
    /// 试题在审核过程中的状
    /// </summary>
    public partial class VerifyStatusManager
    {
        /// <summary>
        /// 新增一条审核记录
        /// 此处应 新增一条审核记录 同时 修改 Question表中相应记录的 VerifyTimes 记录
        /// 故 此处采用 事务处理
        /// </summary>
        /// <param name="v">insert VerifyStatus</param>
        /// <param name="q">updata Question</param>
        /// <returns>新增的审核记录的VerifyStatusId</returns>
        public bool Add(VerifyStatus v, Question q)
        {
            return dal.Add(v, q);
        }
        public List<Dictionary<string, object>> GetVerifyStatusAndInstantiationByQuestionId(int QuestionId)
        {
            List<Dictionary<string, object>> list = new List<Dictionary<string, object>>();
            DataSet ds = new DataSet();
            ds = dal.GetVerifyStatusAndInstantiationByQuestionId(QuestionId);
            if (ds != null && ds.Tables.Count > 0)
            {
                for (int i = 0; i < ds.Tables.Count; i++)
                {
                    for (int j = 0; j < ds.Tables[i].Rows.Count; j++)
                    {
                        list.Add(common.DataRowTODictionary(ds.Tables[i].Rows[j]));
                    }
                }
            }
            return list;
        }
    }
}
