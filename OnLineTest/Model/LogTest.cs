using System;
namespace OnLineTest.Model
{
	/// <summary>
	/// 保存平时测试信息实例(不包含测试试题)
	/// </summary>
	[Serializable]
	public partial class LogTest
	{
		public LogTest()
		{}
		#region Model
		private int _logtestid;
		private int _userid;
		private DateTime _logteststarttime= DateTime.Now;
		private DateTime? _logtestendtime;
		private int _papercodeid;
		private int _difficultyid;
		private int _logtestscore=0;
		/// <summary>
		/// ID int identity(1,1) primary key
		/// </summary>
		public int LogTestId
		{
			set{ _logtestid=value;}
			get{return _logtestid;}
		}
		/// <summary>
		/// 外键,进行测试的用户 int not null references Users(UserId)
		/// </summary>
		public int UserId
		{
			set{ _userid=value;}
			get{return _userid;}
		}
		/// <summary>
		/// 测试开始时间 datetime not null default getdate()
		/// </summary>
		public DateTime LogTestStartTime
		{
			set{ _logteststarttime=value;}
			get{return _logteststarttime;}
		}
		/// <summary>
		/// 测试结束时间(如果为null则表明试卷提交错误或没有正常提交) datetime null
		/// </summary>
		public DateTime? LogTestEndTime
		{
			set{ _logtestendtime=value;}
			get{return _logtestendtime;}
		}
		/// <summary>
		/// 外键,测试的科目 int not null references PaperCodes(PaperCodeId)
		/// </summary>
		public int PaperCodeId
		{
			set{ _papercodeid=value;}
			get{return _papercodeid;}
		}
		/// <summary>
		/// 外键,测试的难度系数 int not null references Difficulty(DifficultyId)
		/// </summary>
		public int DifficultyId
		{
			set{ _difficultyid=value;}
			get{return _difficultyid;}
		}
		/// <summary>
		/// 测试的得分 int not null default 0
		/// </summary>
		public int LogTestScore
		{
			set{ _logtestscore=value;}
			get{return _logtestscore;}
		}
		#endregion Model

	}
}

