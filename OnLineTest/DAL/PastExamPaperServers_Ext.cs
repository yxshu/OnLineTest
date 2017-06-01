
using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using OnLineTest.DBUtility;//Please add references
using OnLineTest.Model;
namespace OnLineTest.DAL
{
    /// <summary>
    /// 数据访问类:PastExamPaperServers
    /// </summary>
    public partial class PastExamPaperServers
    {
        public bool Exists(int PaperCodeId, int ExamZoneId, int PastExamPaperPeriodNo)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from PastExamPaper");
            strSql.Append(" where PaperCodeId=@PaperCodeId and ExamZoneId=@ExamZoneId and PastExamPaperPeriodNo=@PastExamPaperPeriodNo ");
            SqlParameter[] parameters = {
					new SqlParameter("@PaperCodeId", SqlDbType.Int,4),
                    new SqlParameter("@ExamZoneId",SqlDbType.Int,4),
                    new SqlParameter("@PastExamPaperPeriodNo",SqlDbType.Int,4)
                                        };
            parameters[0].Value = PaperCodeId;
            parameters[1].Value = ExamZoneId;
            parameters[2].Value = PastExamPaperPeriodNo;
            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }
        public PastExamPaper GetModel(int PaperCodeId, int ExamZoneId, int PastExamPaperPeriodNo)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 PastExamPaperId,PaperCodeId,ExamZoneId,PastExamPaperPeriodNo,PastExamPaperDatetime,IsVerified from PastExamPaper ");
            strSql.Append(" where PaperCodeId=@PaperCodeId and ExamZoneId=@ExamZoneId and PastExamPaperPeriodNo=@PastExamPaperPeriodNo");
            SqlParameter[] parameters = {
					new SqlParameter("@PaperCodeId", SqlDbType.Int,4),
                    new SqlParameter("@ExamZoneId",SqlDbType.Int,4),
                    new SqlParameter("@PastExamPaperPeriodNo",SqlDbType.Int,4)
                                        };
            parameters[0].Value = PaperCodeId;
            parameters[1].Value = ExamZoneId;
            parameters[2].Value = PastExamPaperPeriodNo;
            OnLineTest.Model.PastExamPaper model = new OnLineTest.Model.PastExamPaper();
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
    }
}

