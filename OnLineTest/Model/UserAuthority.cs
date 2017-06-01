using System;
namespace OnLineTest.Model
{
	/// <summary>
	/// 用户权限实例
	/// </summary>
	[Serializable]
	public partial class UserAuthority
	{
		public UserAuthority()
		{}
		#region Model
		private int _userauthorityid;
		private int _authorityid;
		private int _usergroupid;
		private int _userrankid;
		private string _userauthoriryremark;
		/// <summary>
		/// 用户权限Id int identity(1,1) primary key
		/// </summary>
		public int UserAuthorityId
		{
			set{ _userauthorityid=value;}
			get{return _userauthorityid;}
		}
		/// <summary>
		/// 外键，用户权限int  not null references Authority(AuthorityId),
		/// </summary>
		public int AuthorityId
		{
			set{ _authorityid=value;}
			get{return _authorityid;}
		}
		/// <summary>
		/// 外键 用户权限所对应的用户组ID int not null references UserGroup(UserGroupId)
		/// </summary>
		public int UserGroupId
		{
			set{ _usergroupid=value;}
			get{return _usergroupid;}
		}
		/// <summary>
		/// 外键 权限所对应的用户等级ID int not null references UserRank(UserRankId)
		/// </summary>
		public int UserRankId
		{
			set{ _userrankid=value;}
			get{return _userrankid;}
		}
		/// <summary>
		/// 备注（此项权限所的备注） text not null
		/// </summary>
		public string UserAuthoriryRemark
		{
			set{ _userauthoriryremark=value;}
			get{return _userauthoriryremark;}
		}
		#endregion Model

	}
}

