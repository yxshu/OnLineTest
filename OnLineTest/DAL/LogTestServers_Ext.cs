
using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using OnLineTest.DBUtility;//Please add references
using System.Collections.Generic;
using OnLineTest.Model;
namespace OnLineTest.DAL
{
    /// <summary>
    /// 数据访问类:LogTestServers
    /// </summary>
    public partial class LogTestServers
    {
        /// <summary>
        /// 按要求返回多个实例对象
        /// </summary>
        /// <param name="num">返回的对象数量</param>
        /// <param name="orderby">返回的是前几个对象，排序的原则</param>
        /// <returns>返回前几条对象</returns>
        public List<LogTest> GetModels(int num, string orderby, int userid)
        {
            List<LogTest> list = new List<LogTest>();
            string sql = "select top " + num + " * from logtest where userid=" + userid + " order by " + orderby + " desc";
            DataTable dt = new DataTable();
            dt.Load(DbHelperSQL.ExecuteReader(sql));
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    list.Add(DataRowToModel(dt.Rows[i]));
                }
            }
            return list;
        }
        /// <summary>
        /// 按要求查取一个logtest记录，其中所有的外键都实例化
        /// </summary>
        /// <param name="num">查询的条数</param>
        /// <param name="orderby">排序的方式</param>
        /// <param name="userid"></param>
        /// <returns></returns>
        public List<Dictionary<string, object>> GetModelsbyJoin(int num, string orderby, int userid)
        {
            List<Dictionary<string, object>> list = new List<Dictionary<string, object>>();
            string sql = "select top " + num + "logtestid,userid,logteststarttime,logtestendtime,logtestscore,b.papercodeid,papercode,chinesename,papercodepassscore,papercodetotalscore,timerange,papercoderemark,c.difficultyid,difficultyratio,difficultydescrip,difficultyremark,d.subjectid,subjectname,subjectremark from logtest as a  left join papercodes as b on a.papercodeid=b.papercodeid left join difficulty as c on a.difficultyid=c.difficultyid left join subject as d on d.subjectid=b.subjectid where a.userid=" + userid + " order by " + orderby + " desc";
            DataTable dt = new DataTable();
            dt.Load(DbHelperSQL.ExecuteReader(sql));
            if (dt.Rows.Count > 0)
            {

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    list.Add(Common.dr2dictionary(dt.Rows[i]));
                }
            }
            return list;
        }
    }
}


