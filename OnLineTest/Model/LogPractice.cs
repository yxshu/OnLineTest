using System;
namespace OnLineTest.Model
{
	/// <summary>
	/// 记录平时练习的习题实例
	/// </summary>
	[Serializable]
	public partial class LogPractice
	{
		public LogPractice()
		{}
		#region Model
		private int _logpracticeid;
		private int _userid;
		private int _questionid;
		private DateTime _logpracticetime= DateTime.Now;
		private int? _logpracticeanswer;
		private string _logpracetimeremark;
		/// <summary>
		/// ID int identity(1,1) primary key
		/// </summary>
		public int LogPracticeId
		{
			set{ _logpracticeid=value;}
			get{return _logpracticeid;}
		}
		/// <summary>
		/// 外键,练习的用户 int not null references Users(UserId)
		/// </summary>
		public int userId
		{
			set{ _userid=value;}
			get{return _userid;}
		}
		/// <summary>
		/// 外键,练习的试题 int not null references Question(QuestionId)
		/// </summary>
		public int QuestionId
		{
			set{ _questionid=value;}
			get{return _questionid;}
		}
		/// <summary>
		/// 练习的时间 datetime not null default getdate()
		/// </summary>
		public DateTime LogPracticeTime
		{
			set{ _logpracticetime=value;}
			get{return _logpracticetime;}
		}
		/// <summary>
		/// 练习时选择的答案 int null check(LogpracticeAnswer in(1,2,3,4))
		/// </summary>
		public int? LogPracticeAnswer
		{
			set{ _logpracticeanswer=value;}
			get{return _logpracticeanswer;}
		}
		/// <summary>
		/// 作者练习时写的备注 text null
		/// </summary>
		public string LogPracetimeRemark
		{
			set{ _logpracetimeremark=value;}
			get{return _logpracetimeremark;}
		}
		#endregion Model

	}
}

