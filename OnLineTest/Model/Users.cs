using System;
namespace OnLineTest.Model
{
	/// <summary>
	/// 用户实例
	/// </summary>
	[Serializable]
	public partial class Users
	{
		public Users()
		{}
		#region Model
		private int _userid;
		private string _username;
		private string _userpassword;
		private string _userchinesename;
		private string _userimagename="default.jpg";
		private string _useremail;
		private bool _isvalidate= false;
		private string _tel;
		private int _userscore=0;
		private DateTime _userregisterdatetime= DateTime.Now;
		private int _usergroupid=1;
		/// <summary>
		/// 用户Id号 int identity(1,1) primary key
		/// </summary>
		public int UserId
		{
			set{ _userid=value;}
			get{return _userid;}
		}
		/// <summary>
		/// 用户名 varchar(20) not null unique
		/// </summary>
		public string UserName
		{
			set{ _username=value;}
			get{return _username;}
		}
		/// <summary>
		/// 用户密码（使用MD5加密） varchar(200) not null
		/// </summary>
		public string UserPassword
		{
			set{ _userpassword=value;}
			get{return _userpassword;}
		}
		/// <summary>
		/// 用户中文名 nvarchar(20) null
		/// </summary>
		public string UserChineseName
		{
			set{ _userchinesename=value;}
			get{return _userchinesename;}
		}
		/// <summary>
		/// 用户图像名称 nvarchar(100) not null default('default.jpg')
		/// </summary>
		public string UserImageName
		{
			set{ _userimagename=value;}
			get{return _userimagename;}
		}
		/// <summary>
		/// 用户电子邮件 varchar(50) not null
		/// </summary>
		public string UserEmail
		{
			set{ _useremail=value;}
			get{return _useremail;}
		}
		/// <summary>
		/// 用户是否通过验证标记 bit not null default 0
		/// </summary>
		public bool IsValidate
		{
			set{ _isvalidate=value;}
			get{return _isvalidate;}
		}
		/// <summary>
		/// 用户电话号码 varchar(20) null
		/// </summary>
		public string Tel
		{
			set{ _tel=value;}
			get{return _tel;}
		}
		/// <summary>
		/// 用户论坛分数 int not null default 0
		/// </summary>
		public int UserScore
		{
			set{ _userscore=value;}
			get{return _userscore;}
		}
		/// <summary>
		/// 用户注册时间 datetime not null default getdate()
		/// </summary>
		public DateTime UserRegisterDatetime
		{
			set{ _userregisterdatetime=value;}
			get{return _userregisterdatetime;}
		}
		/// <summary>
		/// 外键，用户所属的用户组 int not null default(1) references UserGroup(UserGroupId)
		/// </summary>
		public int UserGroupId
		{
			set{ _usergroupid=value;}
			get{return _usergroupid;}
		}
		#endregion Model

	}
}

