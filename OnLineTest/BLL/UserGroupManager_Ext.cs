
using System;
using System.Data;
using System.Collections.Generic;
using Maticsoft.Common;
using OnLineTest.Model;
namespace OnLineTest.BLL
{
	/// <summary>
	/// 用户组实例
	/// </summary>
	public partial class UserGroupManager
	{
        /// <summary>
        /// 是否存在该记录
        /// </summary>
        public bool Exists(int UserGroupId)
        {
            return dal.Exists(UserGroupId);
        }

	}
}

