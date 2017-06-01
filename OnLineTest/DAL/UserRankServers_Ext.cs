
using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using OnLineTest.DBUtility;//Please add references
using OnLineTest.Model;
namespace OnLineTest.DAL
{
    /// <summary>
    /// 数据访问类:UserRankServers
    /// </summary>
    public partial class UserRankServers
    {
        /// <summary>
        /// 根据用户的论坛分值得到用户的等级
        /// </summary>
        /// <param name="UserScore">用户论坛分值</param>
        /// <returns>用户等级实例</returns>
        public OnLineTest.Model.UserRank GetModel(Users user)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 UserRankId,UserRankName,MinScore,MaxScore from UserRank ");
            strSql.Append(" where @UserScore between MinScore and MaxScore");
            SqlParameter[] parameters = {
					new SqlParameter("@UserScore", SqlDbType.Int,5)
			};
            parameters[0].Value = user.UserScore;

            OnLineTest.Model.UserRank model = new OnLineTest.Model.UserRank();
            DataSet ds = DbHelperSQL.Query(strSql.ToString(), parameters);
            if (ds.Tables[0].Rows.Count > 0)
            {
                return DataRowToModel(ds.Tables[0].Rows[0]);
            }
            else
            {
                return null;
            }
        }
        /// <summary>
        /// 得到当前用户的下一个等级
        /// </summary>
        /// <param name="userid">当前用户</param>
        /// <returns>userrank</returns>
        public UserRank GetNextRank(int userid)
        {
            DataSet ds = DbHelperSQL.Query("select top 1 *  from userrank where minscore>(select userscore from users where userid=" + userid + ") order by minscore");
            UserRank userrank = new UserRank();
            if (ds.Tables[0].Rows.Count > 0)
            {
                return DataRowToModel(ds.Tables[0].Rows[0]);
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int UserRankId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from UserRank");
            strSql.Append(" where UserRankId=@UserRankId ");
            SqlParameter[] parameters = {
					new SqlParameter("@UserRankId", SqlDbType.Int,4)			};
            parameters[0].Value = UserRankId;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }
    }
}

