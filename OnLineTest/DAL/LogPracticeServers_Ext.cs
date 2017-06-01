
using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using OnLineTest.DBUtility;//Please add references
using OnLineTest.Model;

namespace OnLineTest.DAL
{
    /// <summary>
    /// 数据访问类:LogPracticeServers
    /// </summary>
    public partial class LogPracticeServers
    {
        /// <summary>
        /// 根据给出的id,要求获取同一个用户的上一条记录
        /// </summary>
        /// <param name="Currentlogpracticeid">当前ID</param>
        /// <param name="userid">用户ID</param>
        /// <returns>返回当前用户的上一次记录</returns>
        public LogPractice getLastModel(int Currentlogpracticeid, int userid)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("select top(1)* from LogPractice ");
            sb.Append("where userId=@userid and LogPracticeId<@logpracticeid order by LogPracticeId desc");
            SqlParameter[] para ={
                                    new SqlParameter("@userid",SqlDbType.Int,4),
                                    new SqlParameter("@logpracticeid",SqlDbType.Int,4)
                                };
            para[0].Value = userid;
            para[1].Value = Currentlogpracticeid;
            DataSet ds = DbHelperSQL.Query(sb.ToString(), para);
            if (ds.Tables[0].Rows.Count > 0)
            {
                return DataRowToModel(ds.Tables[0].Rows[0]);
            }
            else
            {
                return null;
            }
        }
    }
}

