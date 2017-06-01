
using System;
using System.Data;
using System.Collections.Generic;
using Maticsoft.Common;
using OnLineTest.Model;
namespace OnLineTest.BLL
{
    /// <summary>
    /// 用户对试题的评论实例
    /// </summary>
    public partial class CommentManager
    {
        /// <summary>
        /// 联合查询用户发表的评论,并将外链实例化以后,将每一条记录填充到一个字典当中,最终汇总到一个list当中,如果没有评论,则返回null
        /// </summary>
        /// <param name="pagesize">前多少条记录</param>
        /// <param name="userid">用户ID</param>
        /// <param name="step">页码值，注意从0开始计数</param>
        /// <returns>有可能为NULL</returns>
        public List<Dictionary<string, object>> GetList(int pagesize, int userid, int step)
        {
            List<Dictionary<string, object>> list = new List<Dictionary<string, object>>();
            DataSet ds = dal.GetList(pagesize, userid, step);
            list = DsToList(ds, userid);
            return list;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="pagesize"></param>
        /// <param name="QuestionId"></param>
        /// <param name="pagenum"></param>
        /// <param name="userid"></param>
        /// <returns></returns>
        public List<Dictionary<string, object>> GetListByQuestionId(int pagesize, int QuestionId, int pagenum, int userid)
        {
            List<Dictionary<string, object>> list = new List<Dictionary<string, object>>();
            DataSet ds = dal.GetListByQuestionId(pagesize, QuestionId, pagenum);
            list = DsToList(ds, userid);
            return list;
        }
        /// <summary>
        /// 根据给定的questionid获取试题评论
        /// </summary>
        /// <param name="pagesize">页容量</param>
        /// <param name="questionid">questionid</param>
        /// <param name="pagenum">页码</param>
        /// <returns>dataset</returns>
        public List<Dictionary<string, object>> GetListByQuestionId(int pagesize, int questionid, int pagenum)
        {
            DataSet ds = dal.GetListByQuestionId(pagesize, questionid, pagenum);
            return common.DataSetToList(ds);
        }
        /// <summary>
        /// 将ds变换成LIST
        /// </summary>
        /// <param name="ds"></param>
        /// <param name="userid"></param>
        /// <returns></returns>
        public List<Dictionary<string, object>> DsToList(DataSet ds, int userid)
        {
            List<Dictionary<string, object>> list = new List<Dictionary<string, object>>();
            if (ds.Tables.Count != 0 && ds.Tables[0].Rows.Count != 0)
            {
                for (int i = 0; i < ds.Tables.Count; i++)
                {
                    for (int j = 0; j < ds.Tables[i].Rows.Count; j++)
                    {
                        Dictionary<string, object> dic = new Dictionary<string, object>();
                        bool adddelbtn = false;
                        if (userid == (int)ds.Tables[i].Rows[j]["UserId"])
                        {
                            adddelbtn = true;
                        }
                        dic.Add("AddDelBTN", adddelbtn);//添加一个是否添加删除按钮的标志
                        for (int k = 0; k < ds.Tables[i].Columns.Count; k++)
                        {
                            string name = ds.Tables[i].Columns[k].ColumnName;
                            object value = ds.Tables[i].Rows[j][k];
                            dic.Add(name, value);
                        }
                        list.Add(dic);
                    }
                }
            }
            else
            {
                list = null;
            }
            return list;
        }

    }
}

