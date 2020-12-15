
using System;
namespace OnLineTest.Model
{
	/// <summary>
	/// 用户登录记录实例
	/// </summary>
	[Serializable]
	public partial class LogLogin
	{
		public LogLogin()
		{}
		#region Model
		private int _logloginid;
		private int _userid;
		private DateTime _loglogintime= DateTime.Now;
		private DateTime? _loglogouttime;
		private string _logloginip="127.0.0.1";
		private string _logloginoperatiionsystem;
		private string _logloginwebserverclient;
		private string _remark;
		/// <summary>
		/// ID int identity(1,1) primary key
		/// </summary>
		public int LogLoginId
		{
			set{ _logloginid=value;}
			get{return _logloginid;}
		}
		/// <summary>
		/// 外键,登录的用户 int not null references Users(UserID)
		/// </summary>
		public int UserId
		{
			set{ _userid=value;}
			get{return _userid;}
		}
		/// <summary>
		/// 用户登录的时间 datetime not null default getdate()
		/// </summary>
		public DateTime LogLoginTime
		{
			set{ _loglogintime=value;}
			get{return _loglogintime;}
		}
		/// <summary>
		/// 用户退出的时间 datetime not null default getdate()
		/// </summary>
		public DateTime? LogLogoutTime
		{
			set{ _loglogouttime=value;}
			get{return _loglogouttime;}
		}
		/// <summary>
		/// 用户登录的IP varchar(20) not null default('127.0.0.1')
		/// </summary>
		public string LogLoginIp
		{
			set{ _logloginip=value;}
			get{return _logloginip;}
		}
		/// <summary>
		/// nvarchar(200) null,-----登录时所用的操作系统
		/// </summary>
		public string LogLoginOperatiionSystem
		{
			set{ _logloginoperatiionsystem=value;}
			get{return _logloginoperatiionsystem;}
		}
		/// <summary>
		/// varchar(100) null-----登录时所用的浏览器类型
		/// </summary>
		public string LogLoginWebServerClient
		{
			set{ _logloginwebserverclient=value;}
			get{return _logloginwebserverclient;}
		}
		/// <summary>
        /// varchar(100) null-----备注,0成功登录,1成功退出,2登录超时，系统退出
		/// </summary>
		public string Remark
		{
			set{ _remark=value;}
			get{return _remark;}
		}
		#endregion Model

	}
}

