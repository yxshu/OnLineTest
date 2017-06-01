
using System;
using System.Data;
using System.Collections.Generic;
using Maticsoft.Common;
using OnLineTest.Model;
namespace OnLineTest.BLL
{
    /// <summary>
    /// 试题实例
    /// </summary>
    public partial class QuestionManager
    {
        /// <summary>
        /// 通过联合查询的方式获得question，并将其中的外链进行实例化
        /// 其中获取的试题是用户上传的，已经审核通过的，没有被删除的数据
        /// </summary>
        /// <param name="pagesize">每次取得的数据条数</param>
        /// <param name="orderby">排序</param>
        /// <param name="pagenum">页码值（其中取值是从pagesize*pagenum开始取值）</param>
        /// <returns></returns>
        public List<Dictionary<string, object>> GetListByPage(int pagesize, int pagenum, int userid)
        {
            DataSet ds = dal.GetList(pagesize, pagenum, userid);
            return common.DataSetToList(ds);

        }
        /// <summary>
        /// 通过联合查询的方式获得question，并将其中的外链进行实例化
        /// 获取用户上传的所有，包括审核未通过的，没有审核的，已经软删除的等等
        /// </summary>
        /// <param name="pagesize">每次取得的数据条数</param>
        /// <param name="pagenum">页码值，即从哪一条开始取值</param>
        /// <param name="userid">用户</param>
        /// <returns></returns>
        public List<Dictionary<string, object>> GetAllUpLoadedQuestionByPage(int pagesize, int pagenum, int userid)
        {
            DataSet ds = dal.GetAllUpLoadedQuestionByPage(pagesize, pagenum, userid);
            return common.DataSetToList(ds);
        }
        /// <summary>
        /// 根据ID获取试题并实例化所有外链
        /// </summary>
        /// <param name="QuestionId">id</param>
        /// <returns>返回一个字典</returns>
        public Dictionary<string, object> GetQuestionAndInstantiationById(int QuestionId)
        {
            Dictionary<string, object> dic = new Dictionary<string, object>();
            DataSet ds = dal.GetQuestionAndInstantiationById(QuestionId);
            dic = common.DataRowTODictionary(ds.Tables[0].Rows[0]);
            return dic;
        }
        /// <summary>
        /// 利用事务更新试题的最终审核状态
        /// 其中一、更新Question表的状态
        ///     二、在Verify表中插入一条新的数据
        /// </summary>
        /// <param name="questionId">试题ID</param>
        /// <param name="passable">审核的结果，是否通过</param>
        /// <param name="userid">审核用户ID</param>
        /// <returns></returns>
        public bool UpdataQuestionFinalVerifyByTransaction(int questionId, bool passable, int userid)
        {
            bool result = false;
            if (new QuestionManager().Exists(questionId) && new UsersManager().Exists(userid))
            {
                result = dal.UpdataQuestionFinalVerifyByTransaction(questionId, passable, userid);
            }
            return result;
        }
        /// <summary>
        /// 生成一个模拟试卷的试题
        /// </summary>
        /// <param name="textbookid">试卷适用的教材</param>
        /// <param name="dic">一个字典，包括教材中每章节所选取试题的数量</param>
        /// <param name="difficulty">难度系数</param>
        /// <returns></returns>
        public List<Question> CreateSimulatePaper(int textbookid, Dictionary<Chapter, int> dic, int difficulty)
        {
            List<Question> result = new List<Question>();
            DataSet ds = dal.CreateSimulatePaper(textbookid, dic, difficulty);
            if (ds.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < ds.Tables.Count; i++)
                {
                    for (int j = 0; j < ds.Tables[i].Rows.Count; j++)
                    {
                        result.Add(dal.DataRowToModel(ds.Tables[i].Rows[j]));
                    }
                }
            }
            return result;
        }
        /// <summary>
        /// 根据给定的papercodeid随机抽取一条记录并实例化,并新增一个记录进入了logpractice
        /// </summary>
        /// <param name="papercodeid">你懂的</param>
        /// <param name="logpractice">新增的测试记录</param>
        /// <returns></returns>
        public Dictionary<string, object> getQuestionByPapercodeidANDRand(int papercodeid, int userid, out LogPractice logpractice)
        {
            int questionid = dal.getQuestionByPapercodeidANDRand(papercodeid);
            logpractice = new LogPractice();
            if (Exists(questionid))
            {
                LogPracticeManager manager = new LogPracticeManager();
                logpractice.QuestionId = questionid;
                logpractice.LogPracticeTime = DateTime.Now;
                logpractice.userId = userid;
                logpractice.LogPracticeId = manager.Add(logpractice);
                return GetQuestionAndInstantiationById(questionid);
            }
            else
                return null;
        }
        /// <summary>
        /// 根据用户提交的upORdown以及QUESTIONID修改试题的顶或踩的值,upORdown=1为顶，upORdown=-1为踩
        /// </summary>
        /// <param name="questionid">试题id</param>
        /// <param name="upORdown">顶或者踩</param>
        /// <returns></returns>
        public bool handlerupORdown(int questionid, int upORdown)
        {
            if (new QuestionManager().Exists(questionid))
            {
                return dal.handlerupORdown(questionid, upORdown);
            }
            else return false;
        }
    }
}

