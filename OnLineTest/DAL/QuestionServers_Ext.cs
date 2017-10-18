
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
    /// 数据访问类:QuestionServers
    /// </summary>
    public partial class QuestionServers
    {
        /// <summary>
        /// 利用联合查询按要求查取question并将其中的外链实例化
        /// 其中获取的试题是用户上传的，已经审核通过的，没有被删除的数据
        /// </summary>
        /// <param name="pagesize">每次取的数量</param>
        /// <param name="orderby">排序方式</param>
        /// <param name="pagenum">页码值（每次取值从pagesize*pagenum的位置开始）</param>
        /// <returns>返回查询到的dataset，如果没有值，则返回NULL</returns>
        public DataSet GetList(int pagesize, int pagenum, int userid)
        {
            DataSet ds = new DataSet();
            string sql = "select top(@top) o.* from (select ROW_NUMBER() over(order by IsSupported desc) as rownumber,  q.QuestionId,q.QuestionTitle,q.AnswerA,q.AnswerB,q.AnswerC,q.AnswerD,q.CorrectAnswer,q.ImageAddress,q.UpLoadTime,q.VerifyTimes,q.IsVerified,q.IsDelte,q.IsSupported,q.IsDeSupported,q.PaperCodeId,q.TextBookId,q.ChapterId,q.PastExamPaperId,q.Explain,q.Remark,u.UserId,u.UserName,u.UserChineseName,u.UserImageName,u.UserEmail,u.IsValidate,u.Tel,u.UserScore,u.UserRegisterDatetime,g.UserGroupId,g.UserGroupName,g.UserGroupRemark,d.DifficultyDescrip,d.DifficultyId,d.DifficultyRatio,d.DifficultyRemark from Question as q left join Users as u on u.UserId=q.UserId left join UserGroup as g on g.UserGroupId=u.UserGroupId left join Difficulty as d on d.DifficultyId=q.DifficultyId where q.IsVerified='true' and q.IsDelte='false' and q.userid=@userid) as o  where rownumber >=@startnum order by IsSupported desc";
            SqlParameter[] parameter ={
                                     new SqlParameter("@top",SqlDbType.Int),
                                     new SqlParameter("@startnum",SqlDbType.Int),
                                     new SqlParameter("@userid",SqlDbType.Int)
                                     };
            parameter[0].Value = pagesize;
            parameter[1].Value = pagenum * pagesize;
            parameter[2].Value = userid;
            ds = DbHelperSQL.Query(sql, parameter);
            return ds;
        }
        /// <summary>
        /// 获取用户上传的所有试题
        /// </summary>
        /// <param name="pagesize">取的数量</param>
        /// <param name="pagenum">页码值</param>
        /// <param name="userid">用户ID</param>
        /// <returns></returns>
        public DataSet GetAllUpLoadedQuestionByPage(int pagesize, int pagenum, int userid)
        {
            DataSet ds = new DataSet();
            StringBuilder sb = new StringBuilder();
            sb.Append("select top(@top) o.* from");
            sb.Append("(select ROW_NUMBER() over(order by UpLoadTime desc) as rownumber, ");
            sb.Append("q.QuestionId,q.QuestionTitle,q.AnswerA,q.AnswerB,q.AnswerC,q.AnswerD,q.CorrectAnswer,q.ImageAddress,q.UpLoadTime,q.VerifyTimes,q.IsVerified,q.IsDelte,q.IsSupported,q.IsDeSupported,q.PastExamQuestionId,q.Explain,q.Remark,");
            sb.Append(" u.UserId,u.UserName,u.UserChineseName,u.UserImageName,u.UserEmail,u.IsValidate,u.Tel,u.UserScore,u.UserRegisterDatetime,");
            sb.Append(" g.UserGroupId,g.UserGroupName,g.UserGroupRemark,");
            sb.Append(" d.DifficultyId,d.DifficultyRatio,d.DifficultyDescrip,d.DifficultyRemark,");
            sb.Append(" p.PaperCodeId,p.PaperCode,p.ChineseName,p.PaperCodePassScore,p.PaperCodeTotalScore,p.TimeRange,p.PaperCodeRemark,");
            sb.Append(" s.SubjectId,s.SubjectName,s.SubjectRemark,");
            sb.Append(" t.TextBookId,t.TextBookName,t.ISBN,");
            sb.Append(" chp.ChapterId,chp.ChapterName,chp.ChapterParentNo,chp.ChapterRemark,chp.ChapterDeep,");
            sb.Append(" pep.PastExamPaperId,pep.PastExamPaperPeriodNo,pep.PastExamPaperDatetime,");
            sb.Append(" zone.ExamZoneId,zone.ExamZoneName");
            sb.Append("  from Question as q ");
            sb.Append("  left join Users as u on u.UserId=q.UserId ");
            sb.Append("  left join UserGroup as g on g.UserGroupId=u.UserGroupId ");
            sb.Append("  left join Difficulty as d on d.DifficultyId=q.DifficultyId ");
            sb.Append("  left join PaperCodes as p on p.PaperCodeId=q.PaperCodeId");
            sb.Append("  left join Subject as s on s.SubjectId=p.SubjectId");
            sb.Append("  left join TextBook as t on t.TextBookId=q.TextBookId");
            sb.Append("  left join Chapter as chp on chp.ChapterId=q.ChapterId");
            sb.Append("  left join PastExamPaper as pep on pep.PastExamPaperId=q.PastExamPaperId");
            sb.Append("  left join ExamZone as zone on zone.ExamZoneId=pep.ExamZoneId");
            sb.Append("  where  q.userid=@userid) ");
            sb.Append("  as o  where rownumber >=@startnum order by rownumber");
            string sql = sb.ToString();
            SqlParameter[] parameter ={
                                     new SqlParameter("@top",SqlDbType.Int),
                                     new SqlParameter("@startnum",SqlDbType.Int),
                                     new SqlParameter("@userid",SqlDbType.Int)
                                     };
            parameter[0].Value = pagesize;
            parameter[1].Value = pagenum * pagesize;
            parameter[2].Value = userid;
            ds = DbHelperSQL.Query(sql, parameter);
            return ds;
        }

        /// <summary>
        /// 根据ID获取试题并实例化所有外链
        /// </summary>
        /// <param name="QuestionId"></param>
        /// <returns></returns>
        public DataSet GetQuestionAndInstantiationById(int QuestionId)
        {
            DataSet ds = new DataSet();
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("select ");
            sb.AppendLine("q.QuestionId,q.QuestionTitle,q.AnswerA,q.AnswerB,q.AnswerC,q.AnswerD,q.CorrectAnswer,q.ImageAddress,q.UpLoadTime,q.VerifyTimes,q.IsVerified,q.IsDelte,q.IsSupported,q.IsDeSupported,q.PastExamQuestionId,q.Explain,q.Remark,");
            sb.AppendLine("u.UserId,u.UserName,u.UserChineseName,u.UserImageName,u.UserEmail,u.IsValidate,u.Tel,u.UserScore,u.UserRegisterDatetime,");
            sb.AppendLine("g.UserGroupId,g.UserGroupName,g.UserGroupRemark,");
            sb.AppendLine("d.DifficultyId,d.DifficultyRatio,d.DifficultyDescrip,d.DifficultyRemark,");
            sb.AppendLine("p.PaperCodeId,p.PaperCode,p.ChineseName,p.PaperCodePassScore,p.PaperCodeTotalScore,p.TimeRange,p.PaperCodeRemark,");
            sb.AppendLine("s.SubjectId,s.SubjectName,s.SubjectRemark,");
            sb.AppendLine("t.TextBookId,t.TextBookName,t.ISBN,");
            sb.AppendLine("chp.ChapterId,chp.ChapterName,chp.ChapterParentNo,chp.ChapterRemark,chp.ChapterDeep,");
            sb.AppendLine(" pep.PastExamPaperId,pep.PastExamPaperPeriodNo,pep.PastExamPaperDatetime,");
            sb.AppendLine(" zone.ExamZoneId,zone.ExamZoneName");
            sb.AppendLine(" from Question as q");
            sb.AppendLine("left join  Difficulty as d on q.DifficultyId=d.DifficultyId");
            sb.AppendLine("left join Users as u on q.UserId=u.UserId");
            sb.AppendLine("left join UserGroup as g on u.UserId=g.UserGroupId");
            sb.AppendLine("left join PaperCodes as p on q.PaperCodeId=p.PaperCodeId");
            sb.AppendLine("left join Subject as s on p.SubjectId=s.SubjectId");
            sb.AppendLine("left join TextBook as t on q.TextBookId=t.TextBookId");
            sb.AppendLine("left join Chapter as chp on q.ChapterId=chp.ChapterId");
            sb.AppendLine("left join PastExamPaper as pep on q.PastExamPaperId=pep.PastExamPaperId");
            sb.AppendLine("left join ExamZone as zone on zone.ExamZoneId=pep.ExamZoneId");
            sb.AppendLine("where QuestionId=@QuestionId");
            string sql = sb.ToString();
            SqlParameter[] para ={
                                new SqlParameter("@QuestionId",SqlDbType.VarChar)
                                };
            para[0].Value = QuestionId;
            ds = DbHelperSQL.Query(sql, para);
            return ds;
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
            System.Collections.Hashtable hashtable = new System.Collections.Hashtable();
            //SQL语句的哈希表（key为sql语句，value是该语句的SqlParameter[]）
            string updatequestion = "update Question set VerifyTimes=VerifyTimes+1,IsVerified=@Passable where QuestionId=@QuestionId";
            SqlParameter[] param1 = {
                                        new SqlParameter("@Passable",SqlDbType.Bit),
                                        new SqlParameter("@QuestionId",SqlDbType.Int)
                                    };
            param1[0].Value = passable;
            param1[1].Value = questionId;
            string insertVerifyStatus = "insert into VerifyStatus(QuestionId,UserId,VerifyTimes,IsPass) select @QuestionId,@UserId,VerifyTimes+1,@Passable from Question where QuestionId=@QuestionId";
            SqlParameter[] param2 = { 
                                    new SqlParameter("@QuestionId",SqlDbType.Int),
                                    new SqlParameter("@UserId",SqlDbType.Int),
                                    new SqlParameter("@Passable",SqlDbType.Bit)
                                    };
            param2[0].Value = questionId;
            param2[1].Value = userid;
            param2[2].Value = passable;
            hashtable.Add(updatequestion, param1);
            hashtable.Add(insertVerifyStatus, param2);
            try
            {
                DbHelperSQL.ExecuteSqlTran(hashtable);
                result = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return result;
        }

        /// <summary>
        /// 生成一个模拟试卷的试题
        /// </summary>
        /// <param name="textbookid">试卷适用的科目</param>
        /// <param name="dic">一个字典，其中包括章节id和每个章节要选取的试题数量</param>
        /// <param name="difficulty">难度系数</param>
        /// <returns>返回一个有多个datatable的dataset</returns>
        public DataSet CreateSimulatePaper(int textbookid, Dictionary<Chapter, int> dic, int difficulty)
        {
            DataSet ds = new DataSet();//返回的结果
            foreach (System.Collections.Generic.KeyValuePair<Chapter, int> kv in dic)
            {
                //此处值得注意的地方：其中parm[1]和parm[2]是一直不被的，为什么还被包含在循环体内？为什么每次都new sqlparameter()？
                //因为sqlparametercollection的怪脾气，又不想去给dbhelpersql添加一行代码 sqlcommand.parameter.clear(),所以才会出现这样的情况
                SqlParameter[] parm = {
            //后面的部分开始填充sql参数数组
            new SqlParameter("textbookid", SqlDbType.Int),
             new SqlParameter("difficulty", SqlDbType.Int),
             new SqlParameter("chapterid",SqlDbType.Int),
             new SqlParameter("ratio",SqlDbType.Int)
        };
                parm[0].Value = textbookid;
                parm[1].Value = difficulty;
                parm[2].Value = kv.Key.ChapterId;
                parm[3].Value = kv.Value;
                //填充参数数组结束
                SqlDataReader reader = null;//reader
                try
                {
                    reader = DbHelperSQL.RunProcedure("Proc_CreateSimulatePaper", parm);//访问数据库 ,此处要求在数据库中保存一个存储过程“Proc_CreateSimulatePaper”
                    ds.Tables.Add(Common.ConvertDataReaderToDataTable(reader));//利用datareader填充dataset
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    if (reader != null && !reader.IsClosed)
                        reader.Close();
                }
            }
            return ds;
        }
        /// <summary>
        /// 根据给定的papercodeid随机抽取一条记录的id
        /// </summary>
        /// <returns></returns>
        public int getQuestionByPapercodeidANDRand(int papercodeid)
        {
            int questionid;
            SqlParameter[] param = { new SqlParameter("@papercodeid", SqlDbType.Int) };
            param[0].Value = papercodeid;
            object obj = DbHelperSQL.GetSingle("select top 1 QuestionId from Question where PaperCodeId=@papercodeid and IsVerified=1 and IsDelte=0 order by NEWID()", param);
            if (obj != null)
            {
                if (Int32.TryParse(obj.ToString(), out questionid))
                    return questionid;
                else return -1;
            }
            else
                return -1;
        }
        /// <summary>
        /// 根据用户提交的upORdown以及QUESTIONID修改试题的顶或踩的值,upORdown=1为顶，upORdown=-1为踩
        /// </summary>
        /// <param name="questionid">试题id</param>
        /// <param name="upORdown">顶或者踩</param>
        /// <returns></returns>
        public bool handlerupORdown(int questionid, int upORdown)
        {
            bool result = false;
            string sql = string.Empty;
            if (upORdown == 1)
                sql = "update Question set IsSupported+=1 where QuestionId=@questionid";
            else if (upORdown == -1)
                sql = "update Question set IsDeSupported-=1 where QuestionId=@questionid";
            if (!string.IsNullOrEmpty(sql))
            {
                SqlParameter[] para = { 
                                          new SqlParameter("@questionid",SqlDbType.Int,4)
                                      };
                para[0].Value = questionid;
                if (DbHelperSQL.ExecuteSql(sql, para) == 1)
                    result = true;
            }
            return result;
        }
    }
}

