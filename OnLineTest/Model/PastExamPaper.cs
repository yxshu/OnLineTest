using System;
namespace OnLineTest.Model
{
	/// <summary>
	/// 历年真题信息实例(仅介绍信息,相关试题不在实例)
	/// </summary>
	[Serializable]
	public partial class PastExamPaper
	{
		public PastExamPaper()
		{}
		#region Model
		private int _pastexampaperid;
		private int _papercodeid;
		private int _examzoneid;
		private int _pastexampaperperiodno;
		private DateTime? _pastexampaperdatetime;
		private bool _isverified= false;
		/// <summary>
		/// 历年真题信息ID int identity(1,1) primary key
		/// </summary>
		public int PastExamPaperId
		{
			set{ _pastexampaperid=value;}
			get{return _pastexampaperid;}
		}
		/// <summary>
		/// 外键,真题所对应的试卷代码ID int not null references PaperCodes(PaperCodeId)
		/// </summary>
		public int PaperCodeId
		{
			set{ _papercodeid=value;}
			get{return _papercodeid;}
		}
		/// <summary>
		/// 外键,真题所对应的考区ID int not null references ExamZone(ExamZoneId)
		/// </summary>
		public int ExamZoneId
		{
			set{ _examzoneid=value;}
			get{return _examzoneid;}
		}
		/// <summary>
		/// 真题对应的期数 int not null
		/// </summary>
		public int PastExamPaperPeriodNo
		{
			set{ _pastexampaperperiodno=value;}
			get{return _pastexampaperperiodno;}
		}
		/// <summary>
		/// 真题对应的考试时间 datetime null
		/// </summary>
		public DateTime? PastExamPaperDatetime
		{
			set{ _pastexampaperdatetime=value;}
			get{return _pastexampaperdatetime;}
		}
		/// <summary>
		/// 真题信息是否通过审核 bit not null default(0)
		/// </summary>
		public bool IsVerified
		{
			set{ _isverified=value;}
			get{return _isverified;}
		}
		#endregion Model

	}
}

