
using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using OnLineTest.DBUtility;//Please add references
namespace OnLineTest.DAL
{
    /// <summary>
    /// 数据访问类:PaperCodesServers
    /// </summary>
    public partial class PaperCodesServers
    {
        /// <summary>
        /// 按strWhere的要求查询papercodes,并将其中的外链进行实例化
        /// </summary>
        /// <param name="strWhere">查询条件,也可以是排序如： order by chinesename</param>
        /// <returns>返回dataset，如果没有值，则返回null</returns>
        public DataSet GetPaperCodeAndInstantiation(string strWhere)
        {
            //select PaperCodeId,PaperCode,ChineseName,PaperCodePassScore,PaperCodeTotalScore,TimeRange,PaperCodeRemark,p.IsVerified as  PaperCodeIsVerifyed,s.SubjectId,SubjectName,SubjectRemark,s.IsVerified as SubjectIsVerified from PaperCodes as p
            //left join Subject as s 
            //on p.SubjectId=s.SubjectId 
            //where PaperCodeId>3
            DataSet ds = null;
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("select PaperCodeId,PaperCode,ChineseName,PaperCodePassScore,PaperCodeTotalScore,TimeRange,PaperCodeRemark,p.IsVerified as  PaperCodeIsVerifyed,s.SubjectId,SubjectName,SubjectRemark,s.IsVerified as SubjectIsVerified from PaperCodes as p");
            sb.AppendLine("left join Subject as s ");
            sb.AppendLine("on p.SubjectId=s.SubjectId ");
            if (!String.IsNullOrEmpty(strWhere.Trim()))
            {
                sb.AppendLine(strWhere);
            }
            ds = DbHelperSQL.Query(sb.ToString());
            return ds;
        }
        public bool Exists(int PaperCodeId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from PaperCodes");
            strSql.Append(" where PaperCodeId=@PaperCodeId ");
            SqlParameter[] parameters = {
					new SqlParameter("@PaperCodeId", SqlDbType.Int,4)
                                        };
            parameters[0].Value = PaperCodeId;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }
        /// <summary>
        /// 根据SubjectId查询相应的PaperCodes,并按papercodeid排序
        /// </summary>
        /// <param name="SubjectId"></param>
        /// <returns>dataset</returns>
        public DataSet GetModelListBySubjectId(int SubjectId)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("select * from PaperCodes where SubjectId=@subjectid ");
            sb.AppendLine("order by PaperCodeId");
            SqlParameter[] param = { 
                                   new SqlParameter("@subjectid",SqlDbType.Int)
                                   };
            param[0].Value = SubjectId;
            return DbHelperSQL.Query(sb.ToString(), param);
        }

    }
}

