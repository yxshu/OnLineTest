
using System;
using System.Data;
using System.Text;
using System.Data.SqlClient;
using OnLineTest.DBUtility;//Please add references
namespace OnLineTest.DAL
{
    /// <summary>
    /// 数据访问类:Message_tableServers
    /// </summary>
    public partial class Message_tableServers
    {
        /// <summary>
        /// 查询某个id的发信数量
        /// </summary>
        /// <param name="userid">发件人</param>
        /// <returns>发信的数量</returns>
        public int sendNum(int userid)
        {
            return (int)DbHelperSQL.GetSingle("select count(*) from message_table where messagefrom=" + userid);
        }
        /// <summary>
        /// 收信数量
        /// </summary>
        /// <param name="userid">收件人</param>
        /// <returns>收件数量</returns>
        public int recerveNum(int userid)
        {
            return (int)DbHelperSQL.GetSingle("select count(*) from message_table where messageto=" + userid);
        }
        /// <summary>
        /// 未读信数量
        /// </summary>
        /// <param name="userid">收件人</param>
        /// <returns>未读信数量</returns>
        public int NotReadMessageNum(int userid)
        {
            return (int)DbHelperSQL.GetSingle("select Count(*) from message_table where messageto=" + userid + " and messageisread=0");
        }
        /// <summary>
        ///填充外链的方式获取数据集
        /// </summary>
        /// <returns></returns>
        public DataSet getReceiveList(int count, int pagenum, int receiveuserid)
        {
            DataSet ds = new DataSet();
            //string sql = "select top(@count)*, ROW_NUMBER() over(order by m.MessageSendTime desc)  'RowNumber' from Message_table as m left join Users as u on m.MessageFrom=u.UserId where MessageTO=@userid and RowNumber>=10*@pagenum";
            string sql = " select top(@count)* from  ( select *,ROW_NUMBER() over (order by MessageSendTime desc) as rownumber from Message_table as m left join Users as u on m.MessageFrom=u.UserId where m.MessageTO=@userid) as a  where a.rownumber>10*@pagenum";
            SqlParameter[] parm = { 
                                  new SqlParameter("@count",SqlDbType.Int),
                                  new SqlParameter("@pagenum",SqlDbType.Int),
                                  new SqlParameter("@userid",SqlDbType.Int)
                                  };
            parm[0].Value = count;
            parm[1].Value = pagenum;
            parm[2].Value = receiveuserid;
            ds = DbHelperSQL.Query(sql, parm);
            return ds;
        }
        public DataSet getSendList(int count, int pagenum, int receiveuserid)
        {
            DataSet ds = new DataSet();
            //string sql = "select top(@count)*, ROW_NUMBER() over(order by m.MessageSendTime desc)  'RowNumber' from Message_table as m left join Users as u on m.MessageFrom=u.UserId where MessageFrom=@userid and RowNumber>=10*@pagenum";
            string sql = "select top(@count)* from  ( select *,ROW_NUMBER() over (order by MessageSendTime desc) as rownumber from Message_table as m left join Users as u on m.MessageTo=u.UserId where m.MessageFrom=@userid) as a  where a.rownumber>10*@pagenum";
            SqlParameter[] parm = { 
                                  new SqlParameter("@count",SqlDbType.Int),
                                  new SqlParameter("@pagenum",SqlDbType.Int),
                                  new SqlParameter("@userid",SqlDbType.Int)
                                  };
            parm[0].Value = count;
            parm[1].Value = pagenum;
            parm[2].Value = receiveuserid;
            ds = DbHelperSQL.Query(sql, parm);
            return ds;
        }

        public DataSet getLastContact(OnLineTest.Model.Users users){
            DataSet ds = new DataSet();
            string sql = "select top(10) MessageTO,MessageFrom,u.UserId as messagetoid,u.UserName as messagetoname,u2.UserId as messagefromid,u2.UserName as messagefromname from Message_table as m left join Users as u on u.UserId=m.MessageTO left join Users as u2 on u2.UserId=m.MessageFrom  where MessageTO=@userid or MessageFrom=@userid   order by MessageSendTime desc";
            SqlParameter[] parm = {new SqlParameter("@userid",SqlDbType.Int) };
            parm[0].Value = users.UserId;
            ds = DbHelperSQL.Query(sql, parm);
            return ds;
        }
    }
}

