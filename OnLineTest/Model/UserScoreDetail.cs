using System;
namespace OnLineTest.Model
{
	/// <summary>
	/// 用户论坛分的得分详细记录实例
	/// </summary>
	[Serializable]
	public partial class UserScoreDetail
	{
		public UserScoreDetail()
		{}
		#region Model
		private int _userscoredetailid;
		private int _userid;
		private int _userauthorityid;
		private DateTime _userscoredetailtime= DateTime.Now;
		/// <summary>
		/// ID int identity(1,1) primary key
		/// </summary>
		public int UserScoreDetailId
		{
			set{ _userscoredetailid=value;}
			get{return _userscoredetailid;}
		}
		/// <summary>
		/// 外键,得分的用户 int not null references Users(UserID)
		/// </summary>
		public int UserId
		{
			set{ _userid=value;}
			get{return _userid;}
		}
		/// <summary>
		/// 外键,用户权限,也即用户干什么事情得分 int not null references UserAuthority(UserAuthorityId)
		/// </summary>
		public int UserAuthorityId
		{
			set{ _userauthorityid=value;}
			get{return _userauthorityid;}
		}
		/// <summary>
		/// 执行操作的时间 datetime not null default getdate()
		/// </summary>
		public DateTime UserScoreDetailTime
		{
			set{ _userscoredetailtime=value;}
			get{return _userscoredetailtime;}
		}
		#endregion Model

	}
}

