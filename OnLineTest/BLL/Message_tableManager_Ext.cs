
using System;
using System.Data;
using System.Collections.Generic;
using Maticsoft.Common;
using OnLineTest.Model;
namespace OnLineTest.BLL
{
    /// <summary>
    /// 站内信实例
    /// </summary>
    public partial class Message_tableManager
    {
        /// <summary>
        /// 查询某个id的发信数量
        /// </summary>
        /// <param name="userid">发件人</param>
        /// <returns>发信的数量</returns>
        public int sendNum(int userid)
        {
            return dal.sendNum(userid);
        }
        /// <summary>
        /// 收信数量
        /// </summary>
        /// <param name="userid">收件人</param>
        /// <returns>收件数量</returns>
        public int recerveNum(int userid)
        {
            return dal.recerveNum(userid);
        }
        /// <summary>
        /// 未读信数量
        /// </summary>
        /// <param name="userid">收件人</param>
        /// <returns>未读信数量</returns>
        public int NotReadMessageNum(int userid)
        {
            return dal.NotReadMessageNum(userid);
        }

        public List<Dictionary<string, object>> getDictionarySendList(int count, int pagenum, int userid)
        {

            DataSet ds = dal.getSendList(count, pagenum, userid);
            return common.DataSetToList(ds);
        }
        public List<Dictionary<string, object>> getDictionaryReceiveList(int count, int pagenum, int userid)
        {
            DataSet ds = dal.getReceiveList(count, pagenum, userid);
            return common.DataSetToList(ds);
        }
        public List<Dictionary<string, object>> getLastContact(Users user)
        {
            DataSet ds = dal.getLastContact(user);//原数据
            DataSet ds2 = new DataSet();//用于存放数据
            DataTable dt = new DataTable();
            DataColumn id = new DataColumn("id");
            DataColumn name = new DataColumn("name");
            dt.Columns.Add(id);
            dt.Columns.Add(name);
            ds2.Tables.Add(dt);
            for (int i = 0; i < ds.Tables.Count; i++)
            {
                for (int j = 0; j < ds.Tables[i].Rows.Count; j++)
                {
                    DataRow dr = dt.NewRow();
                    if (((Int32)ds.Tables[i].Rows[j]["MessageFrom"]) == user.UserId)
                    { //自己是发件人，联系人为收件人
                        dr["id"] = ds.Tables[i].Rows[j]["messagetoid"];
                        dr["name"] = ds.Tables[i].Rows[j]["messagetoname"];

                    }
                    else
                    {//自己是收件人，联系人为发件人
                        dr["id"] = ds.Tables[i].Rows[j]["messagefromid"];
                        dr["name"] = ds.Tables[i].Rows[j]["messagefromname"];
                    }
                    ds2.Tables[0].Rows.Add(dr);
                }
            }
            return common.DataSetToList(ds2);
        }
    }
}

