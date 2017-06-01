
using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using OnLineTest.DBUtility;//Please add references
namespace OnLineTest.DAL
{
    /// <summary>
    /// 数据访问类:CommentServers
    /// </summary>
    public partial class CommentServers
    {
        /// <summary>
        /// 一个联合查询,根据用户ID取得用户所发表评论的前top行,并将外链实例化
        /// </summary>
        /// <param name="pagesize">每次取值的数目</param>
        /// <param name="userid"></param>
        /// <param name="step">页码值，注意，从0开始计算</param>
        /// <returns></returns>
        public DataSet GetList(int pagesize, int userid, int step)
        {
            DataSet ds = new DataSet();
            int start = step * pagesize;
            string sql = "select top  (@top) o.*  from (select ROW_NUMBER() over (order by CommentTime asc)as rownumber,c.CommentId,c.CommentContent,c.CommentTime,q.QuestionId,q.QuestionTitle,q.AnswerA,q.AnswerB,q.AnswerC,q.AnswerD,q.CorrectAnswer,q.ImageAddress,d.DifficultyId,d.DifficultyRatio,d.DifficultyDescrip,u.UserId,u.UserName,u.UserChineseName,u.UserImageName,u.UserEmail from Comment as c left join Users as u on c.userid=u.userid left join Question as q on c.QuestionId=q.QuestionId left join Difficulty as d on q.DifficultyId=d.DifficultyId where c.UserId=@userid ) as o where rownumber>@rownumber order by CommentTime desc";
            SqlParameter[] parameter = {
             new SqlParameter("@top", SqlDbType.Int),
             new SqlParameter("@userid",SqlDbType.Int),
             new SqlParameter("@rownumber",SqlDbType.Int)
                                       };
            parameter[0].Value = pagesize;
            parameter[1].Value = userid;
            parameter[2].Value = start;
            ds = DbHelperSQL.Query(sql, parameter);
            return ds;
        }
        /// <summary>
        /// 一个联合查询,根据questionid取得关于某个试题的所有评论的前top行,并外外链实例化
        /// </summary>
        /// <param name="pagesize">每次取得的数目</param>
        /// <param name="questionid">不说了</param>
        /// <param name="pagenum">当前页码</param>
        /// <returns>返回一个数据集,无值为null</returns>
        public DataSet GetListByQuestionId(int pagesize,int questionid,int pagenum) {
            DataSet ds = new DataSet();
            int start = pagesize * pagenum;
            string sql = "select top  (@top) o.*  from (select ROW_NUMBER() over (order by CommentTime asc)as rownumber,c.CommentId,c.CommentContent,c.CommentTime,q.QuestionId,q.QuestionTitle,q.AnswerA,q.AnswerB,q.AnswerC,q.AnswerD,q.CorrectAnswer,q.ImageAddress,d.DifficultyId,d.DifficultyRatio,d.DifficultyDescrip,u.UserId,u.UserName,u.UserChineseName,u.UserImageName,u.UserEmail from Comment as c left join Users as u on c.userid=u.userid left join Question as q on c.QuestionId=q.QuestionId left join Difficulty as d on q.DifficultyId=d.DifficultyId where c.QuestionId=@questionid ) as o where rownumber>@rownumber order by CommentTime desc";
            SqlParameter[] parameter = {
             new SqlParameter("@top", SqlDbType.Int),
             new SqlParameter("@questionid",SqlDbType.Int),
             new SqlParameter("@rownumber",SqlDbType.Int)
                                       };
            parameter[0].Value = pagesize;
            parameter[1].Value = questionid;
            parameter[2].Value = start;
            ds = DbHelperSQL.Query(sql, parameter);
            return ds;
        }
    }
}

