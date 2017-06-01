
using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using OnLineTest.DBUtility;//Please add references
using OnLineTest.Model;
using System.Collections;
using System.Collections.Generic;
namespace OnLineTest.DAL
{
    /// <summary>
    /// 数据访问类:VerifyStatusServers
    /// </summary>
    public partial class VerifyStatusServers
    {
        public bool Add(VerifyStatus v, Question q)
        {
            Hashtable hashtable = new Hashtable();
            string sqlVerifyStatus = "insert into VerifyStatus(QuestionId,UserId,VerifyTimes,IsPass,VerifyTime) values(@QuestionId,@UserId,@VerifyTimes,@IsPass,@VerifyTime)";
            SqlParameter[] Vpara ={
                                 new SqlParameter("@QuestionId",SqlDbType.Int),
                                 new SqlParameter("@UserId",SqlDbType.Int),
                                 new SqlParameter("@VerifyTimes",SqlDbType.Int),
                                 new SqlParameter("@IsPass",SqlDbType.Bit),
                                 new SqlParameter("@VerifyTime",SqlDbType.DateTime),
                                 };
            Vpara[0].Value = v.QuestionId;
            Vpara[1].Value = v.UserId;
            Vpara[2].Value = v.VerifyTimes;
            Vpara[3].Value = v.IsPass;
            Vpara[4].Value = v.VerifyTime;
            string sqlQuestion = "UPDATE Question SET VerifyTimes =VerifyTimes+1 WHERE QuestionId = @QuestionId";
            SqlParameter[] Qpara = { 
                                   new SqlParameter("@QuestionId",SqlDbType.Int)
                                   };
            Qpara[0].Value = q.QuestionId;
            hashtable.Add(sqlVerifyStatus, Vpara);
            hashtable.Add(sqlQuestion, Qpara);
            DbHelperSQL.ExecuteSqlTran(hashtable);//SQL语句的哈希表（key为sql语句，value是该语句的SqlParameter[]）
            return true;
        }
        /// <summary>
        /// 根据用户提供的QuestionId返回所有关于本试题的评论信息，并进行实例化
        /// </summary>
        /// <param name="QuestionId"></param>
        /// <returns></returns>
        public DataSet GetVerifyStatusAndInstantiationByQuestionId(int QuestionId)
        {
            // Dictionary<string, object> dic = new Dictionary<string, object>();
            StringBuilder sb = new StringBuilder();
            //下面开始拼接查询语句
            //select 
            //v.VerifyStatusId,v.QuestionId,v.VerifyTimes,v.IsPass,v.VerifyTime,
            //u.UserId,u.UserName,u.UserChineseName,u.UserImageName,u.UserEmail,u.IsValidate,u.Tel,u.UserScore,u.UserChineseName,
            //g.UserGroupId,g.UserGroupName,g.UserGroupRemark,
            //r.UserRankId,r.UserRankName,r.MinScore,r.MaxScore 
            //from VerifyStatus as v 
            //left join Users as u on u.UserId=v.UserId 
            //left join UserGroup as g on u.UserGroupId=g.UserGroupId
            //left join UserRank as r on r.MinScore<u.UserScore and r.MaxScore>u.UserScore
            //where QuestionId=120
            sb.AppendLine("select");
            sb.AppendLine("v.VerifyStatusId,v.QuestionId,v.VerifyTimes,v.IsPass,v.VerifyTime,");
            sb.AppendLine("u.UserId,u.UserName,u.UserChineseName,u.UserImageName,u.UserEmail,u.IsValidate,u.Tel,u.UserScore,u.UserChineseName,");
            sb.AppendLine("g.UserGroupId,g.UserGroupName,g.UserGroupRemark,");
            sb.AppendLine("r.UserRankId,r.UserRankName,r.MinScore,r.MaxScore ");
            sb.AppendLine("from VerifyStatus as v ");
            sb.AppendLine("left join Users as u on u.UserId=v.UserId ");
            sb.AppendLine("left join UserGroup as g on u.UserGroupId=g.UserGroupId");
            sb.AppendLine("left join UserRank as r on r.MinScore<u.UserScore and r.MaxScore>u.UserScore");
            sb.AppendLine("where QuestionId=@QuestionId");
            SqlParameter[] para = { 
                                  new SqlParameter("@QuestionId",SqlDbType.Int)
                                  };
            para[0].Value = QuestionId;
            return DbHelperSQL.Query(sb.ToString(), para);
        }
    }
}

