
using System;
using System.Data;
using System.Collections.Generic;
using Maticsoft.Common;
using OnLineTest.Model;
namespace OnLineTest.BLL
{
	/// <summary>
	/// 用户登录记录实例
	/// </summary>
	public partial class LogLoginManager
	{
        public LogLogin GetCurrentLoglogin(int userid) {
           return dal.GetCurrentLoglogin(userid);
        }
	}
}

