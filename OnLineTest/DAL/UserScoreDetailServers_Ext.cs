
using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using OnLineTest.DBUtility;//Please add references
namespace OnLineTest.DAL
{
    /// <summary>
    /// 数据访问类:UserScoreDetailServers
    /// </summary>
    public partial class UserScoreDetailServers
    {
        /// <summary>
        /// 计算某个用户收入总和
        /// </summary>
        /// <param name="userid">用户id</param>
        /// <param name="num">总条数</param>
        /// <returns>收入总和</returns>
        public int shouru_total(int userid, out int num)
        {
            SqlParameter[] parameter ={
                                         new SqlParameter("@UserId",SqlDbType.Int),
                                         new SqlParameter("@shouru",SqlDbType.Int),
                                         new SqlParameter("@num",SqlDbType.Int)
                                     };
            parameter[0].Value = userid;
            parameter[1].Direction = ParameterDirection.Output;
            parameter[2].Direction = ParameterDirection.Output;
            try
            {
                SqlDataReader mydatareader = DbHelperSQL.RunProcedure("ShouruUserScoreDetailByUserId", parameter);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            num = Convert.ToInt32(parameter[2].Value);
            return Convert.IsDBNull(parameter[1].Value)?0:Convert.ToInt32(parameter[1].Value);
        }
        /// <summary>
        /// 计算某个用户支出总和
        /// </summary>
        /// <param name="userid">用户id</param>
        /// <param name="num">总条数</param>
        /// <returns>支出总和</returns>
        public int zhichu_total(int userid, out int num)
        {
            SqlParameter[] parameter ={
                                         new SqlParameter("@UserId",SqlDbType.Int),
                                         new SqlParameter("@shouru",SqlDbType.Int),
                                         new SqlParameter("@num",SqlDbType.Int)
                                     };
            parameter[0].Value = userid;
            parameter[1].Direction = ParameterDirection.Output;
            parameter[2].Direction = ParameterDirection.Output;
            try
            {
                SqlDataReader mydatareader = DbHelperSQL.RunProcedure("ZhichuUserScoreDetailByUserId", parameter);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            num = (int)parameter[2].Value;
            return Convert.IsDBNull(parameter[1].Value) ? 0 : Convert.ToInt32(parameter[1].Value);
        }
        /// <summary>
        /// 根据用户id，联合查询某个用户的得分明细，其中包括三个模型：userscoredetail,userauthority,authoriy
        /// </summary>
        /// <param name="userid">用户ID</param>
        /// <returns>返回一个联合查询结果的数据集</returns>
        public DataSet userscoredetail(int userid)
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            SqlParameter[] parameter ={
                                     new SqlParameter("@userid",DbType.Int32)
                                     };
            SqlDataAdapter mydataadapter = new SqlDataAdapter();
            try
            {
                SqlDataReader mydatareader = DbHelperSQL.RunProcedure("UserScoreDetailByUserId", parameter);
                dt.Load(mydatareader);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            ds.Tables.Add(dt);
            return ds;
        }
    }
}

