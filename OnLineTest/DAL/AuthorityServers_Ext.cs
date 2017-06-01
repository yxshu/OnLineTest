using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using OnLineTest.DBUtility;//Please add references
namespace OnLineTest.DAL
{
    /// <summary>
    /// 数据访问类:AuthorityServers
    /// </summary>
    public partial class AuthorityServers
    {
        /// <summary>
        /// 根据处理页面获得权限实例
        /// </summary>
        /// <param name="AuthorityHandlerPage">处理权限的页面</param>
        /// <returns>如果存在，则返回权限实例，否则返回NULL</returns>
        public OnLineTest.Model.Authority GetModel(string AuthorityHandlerPage)
        {
            StringBuilder strSql = new StringBuilder();
            //strSql.Append("select  top 1 AuthorityId,AuthorityName,AuthorityDeep,AuthorityParentId,AuthorityScore,AuthorityHandlerPage,AuthorityClickNum,AuthoriryRemark from Authority ");
            strSql.Append("select  top 1 * from Authority ");
            strSql.Append(" where AuthorityHandlerPage=@AuthorityHandlerPage");
            SqlParameter[] parameters = {
					new SqlParameter("@AuthorityHandlerPage",SqlDbType.NVarChar,50)
			};
            parameters[0].Value = AuthorityHandlerPage;

            OnLineTest.Model.Authority model = new OnLineTest.Model.Authority();
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
        /// 根据父权限ID和排序获得权限实例
        /// </summary>
        /// <param name="AuthorityHandlerPage">处理权限的页面</param>
        /// <returns>如果存在，则返回权限实例，否则返回NULL</returns>
        public OnLineTest.Model.Authority GetModel(int AuthorityParentId, int? AuthorityOrderNum)
        {
            StringBuilder strSql = new StringBuilder();
            //strSql.Append("select  top 1 AuthorityId,AuthorityName,AuthorityDeep,AuthorityParentId,AuthorityScore,AuthorityHandlerPage,AuthorityClickNum,AuthoriryRemark from Authority ");
            strSql.Append("select  top 1 * from Authority ");
            strSql.Append(" where AuthorityParentId=@AuthorityParentId and AuthorityOrderNum=@AuthorityOrderNum");
            SqlParameter[] parameters = {
					new SqlParameter("@AuthorityParentId",SqlDbType.Int,4),
                    new SqlParameter("@AuthorityOrderNum",SqlDbType.Int,4)
			};
            parameters[0].Value = AuthorityParentId;
            parameters[1].Value = AuthorityOrderNum;

            OnLineTest.Model.Authority model = new OnLineTest.Model.Authority();
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
        /// 得到最小ID,如果不成功则返回-1
        /// </summary>
        public int GetMinId()
        {
            int result;
            if (Int32.TryParse(DbHelperSQL.GetSingle("select MIN(AuthorityId)from Authority").ToString(), out result))
                return result;
            else
                return -1;
        }
        /// <summary>
        /// 该记录是否存在子权限
        /// </summary>
        public bool ExistsChild(int AuthorityParentId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from Authority");
            strSql.Append(" where AuthorityParentId=@AuthorityId ");
            SqlParameter[] parameters = {
					new SqlParameter("@AuthorityId", SqlDbType.Int,4)			};
            parameters[0].Value = AuthorityParentId;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }
        /// <summary>
        /// 该记录是否存在子权限
        /// </summary>
        public bool Exists(int AuthorityId)
        {
            StringBuilder strSql = new StringBuilder();
            strSql.Append("select count(1) from Authority");
            strSql.Append(" where AuthorityId=@AuthorityId ");
            SqlParameter[] parameters = {
					new SqlParameter("@AuthorityId", SqlDbType.Int,4)			};
            parameters[0].Value = AuthorityId;

            return DbHelperSQL.Exists(strSql.ToString(), parameters);
        }

        /// <summary>
        /// 利用存储过程来处理Authority的增、删、改操作
        /// model:add,delete,update
        /// </summary>
        
        public int OperateAuthoritybyTran(OnLineTest.Model.Authority authority, string model, out int affectrows)
        {
            SqlParameter[] parameters = {
                    new SqlParameter("@model", SqlDbType.NVarChar,20),
                    new SqlParameter("@AuthorityId", SqlDbType.Int,4),
					new SqlParameter("@AuthorityName", SqlDbType.NVarChar,20),
					new SqlParameter("@AuthorityDeep", SqlDbType.Int,4),
					new SqlParameter("@AuthorityParentId", SqlDbType.Int,4),
					new SqlParameter("@AuthorityScore", SqlDbType.Int,4),
					new SqlParameter("@AuthorityHandlerPage", SqlDbType.VarChar,50),
					new SqlParameter("@AuthorityOrderNum", SqlDbType.Int,4),
					new SqlParameter("@AuthorityRemark", SqlDbType.Text)
                                        };
            parameters[0].Value = model;
            parameters[1].Value = authority.AuthorityId;
            parameters[2].Value = authority.AuthorityName;
            parameters[3].Value = authority.AuthorityDeep;
            parameters[4].Value = authority.AuthorityParentId;
            parameters[5].Value = authority.AuthorityScore;
            parameters[6].Value = authority.AuthorityHandlerPage;
            parameters[7].Value = authority.AuthorityOrderNum;
            parameters[8].Value = authority.AuthorityRemark;
            //result行号，affectrows受影响的行数
            int result = DbHelperSQL.RunProcedure("OperateAuthoritybyTran", parameters, out affectrows);
            if (affectrows == 0)
            {
                return 0;
            }
            else
            {
                return result;
            }
        }
    }
}

