
using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using OnLineTest.DBUtility;//Please add references
using OnLineTest.Model;

namespace OnLineTest.DAL
{
    /// <summary>
    /// 数据访问类:UsersServers
    /// </summary>
    public partial class UsersServers
    {

        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public OnLineTest.Model.Users GetModel(string UserName)
        {

            StringBuilder strSql = new StringBuilder();
            strSql.Append("select  top 1 UserId,UserName,UserPassword,UserChineseName,UserImageName,UserEmail,IsValidate,Tel,UserScore,UserRegisterDatetime,UserGroupId from Users ");
            if (System.Text.RegularExpressions.Regex.IsMatch(UserName, "^[a-z]([a-z0-9]*[-_]?[a-z0-9]+)*@([a-z0-9]*[-_]?[a-z0-9]+)+[\\.][a-z]{2,3}([\\.][a-z]{2})?$", System.Text.RegularExpressions.RegexOptions.IgnorePatternWhitespace))
            {
                strSql.Append(" where UserEmail=@UserName");
            }
            else
            {
                strSql.Append(" where UserName=@UserName ");
            }
            SqlParameter[] parameters = {
					new SqlParameter("@UserName", SqlDbType.VarChar,20)
			};
            parameters[0].Value = UserName;

            OnLineTest.Model.Users model = new OnLineTest.Model.Users();
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
        /// 得到当前用户的分数排名
        /// </summary>
        /// <param name="userid">当前用户</param>
        /// <returns>排名</returns>
        public int paiming(int userid)
        {
            Users user = (new UsersServers()).GetModel(userid);
            return (int)DbHelperSQL.GetSingle("select count(*) from users where userscore>=" + user.UserScore);
        }
        /// <summary>
        /// 是否存在该用户实例，并得到存在的数量
        /// </summary>
        public bool Exists(string UserName, string MD5PassWord, out int getCount)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from Users");
            if (System.Text.RegularExpressions.Regex.IsMatch(UserName, "^\\s*([A-Za-z0-9_-]+(\\.\\w+)*@(\\w+\\.)+\\w{2,5})\\s*$", System.Text.RegularExpressions.RegexOptions.IgnorePatternWhitespace))
            {
                strSql.Append(" where UserEmail=@UserName and UserPassword=@MD5PassWord ");
            }
            else
            {
                strSql.Append(" where UserName=@UserName and UserPassword=@MD5PassWord ");
            }
            SqlParameter[] parameters = {
					new SqlParameter("@UserName", SqlDbType.VarChar,20),
					new SqlParameter("@MD5PassWord", SqlDbType.VarChar,200)			
                                        };
            parameters[0].Value = UserName;
            parameters[1].Value = MD5PassWord;

            return DbHelperSQL.Exists(strSql.ToString(), out getCount, parameters);
        }
        /// <summary>
        /// 根据userId判断用户是否存在
        /// </summary>
        /// <param name="userid"></param>
        /// <returns></returns>
        public bool Exists(int userid)
        {
            string sql = "select * from users where userid=@userid";
            SqlParameter[] parameter = { 
                                       new SqlParameter("@userid",SqlDbType.Int)
                                       };
            parameter[0].Value = userid;
            return DbHelperSQL.Exists(sql, parameter);
        }
        public bool Exists(string username)
        {
            string sql = "select * from users where username=@username";
            SqlParameter[] parameter = { 
                                       new SqlParameter("@username",SqlDbType.NVarChar)
                                       };
            parameter[0].Value = username;
            return DbHelperSQL.Exists(sql, parameter);
        }
    }
}