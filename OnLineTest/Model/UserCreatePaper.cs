using System;
namespace OnLineTest.Model
{
	/// <summary>
	/// 保存用户生存试卷信息(不包含试题)实例
	/// </summary>
	[Serializable]
	public partial class UserCreatePaper
	{
		public UserCreatePaper()
		{}
		#region Model
		private int _usercreatepaperid;
		private int _userid;
		private int _papercodeid;
		private int _difficultyid;
		private DateTime _usercreatepapertime= DateTime.Now;
		/// <summary>
		/// ID int identity(1,1) primary key
		/// </summary>
		public int UserCreatePaperId
		{
			set{ _usercreatepaperid=value;}
			get{return _usercreatepaperid;}
		}
		/// <summary>
		/// 外键,生成试卷的用户 int not null references Users(UserId)
		/// </summary>
		public int UserId
		{
			set{ _userid=value;}
			get{return _userid;}
		}
		/// <summary>
		/// 外键,生成试卷的试卷代码 int not null references PaperCodes(PaperCodeId)
		/// </summary>
		public int PaperCodeId
		{
			set{ _papercodeid=value;}
			get{return _papercodeid;}
		}
		/// <summary>
		/// 外键,生成试卷的难度系数 int not null references Difficulty(DifficultyId)
		/// </summary>
		public int DifficultyId
		{
			set{ _difficultyid=value;}
			get{return _difficultyid;}
		}
		/// <summary>
		/// 生成时间 datetime not null default getdate()
		/// </summary>
		public DateTime UserCreatePaperTime
		{
			set{ _usercreatepapertime=value;}
			get{return _usercreatepapertime;}
		}
		#endregion Model

	}
}

