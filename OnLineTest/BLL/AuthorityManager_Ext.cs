using System;
using System.Data;
using System.Collections.Generic;
using Maticsoft.Common;
using OnLineTest.Model;
namespace OnLineTest.BLL
{
    /// <summary>
    /// 权限实例
    /// </summary>
    public partial class AuthorityManager
    {
        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public OnLineTest.Model.Authority GetModel(string AuthorityHandlerPage)
        {

            return dal.GetModel(AuthorityHandlerPage);
        }
        /// <summary>
        /// 得到一个对象实体
        /// </summary>
        public OnLineTest.Model.Authority GetModel(int AuthorityParentId, int? AuthorityOrderNum)
        {

            return dal.GetModel(AuthorityParentId, AuthorityOrderNum);
        }
        /// <summary>
        /// 该记录是否存在子权限
        /// </summary>
        public bool ExistsChild(int AuthorityParentId)
        {

            return dal.ExistsChild(AuthorityParentId);
        }
        /// <summary>
        /// 得到最小ID，如果不成功则返回-1
        /// </summary>
        public int GetMinId()
        {
            return dal.GetMinId();
        }
        /// <summary>
        /// 利用存储过程来处理Authority的增、删、改操作
        /// model:add,delete,update
        /// </summary>
        public int OperateAuthoritybyTran(OnLineTest.Model.Authority authority, string model, out int affectrows)
        {
            int rownum = dal.OperateAuthoritybyTran(authority, model, out affectrows);
            if (affectrows > 0)
                return rownum;
            else
                return -1;
        }

        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int AuthorityId)
        {
            return dal.Exists(AuthorityId);
        }
    }
}

