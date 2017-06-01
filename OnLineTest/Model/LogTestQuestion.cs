using System;
namespace OnLineTest.Model
{
	/// <summary>
	/// 保存平时测试试题实例
	/// </summary>
	[Serializable]
	public partial class LogTestQuestion
	{
		public LogTestQuestion()
		{}
		#region Model
		private int _logtestquestionid;
		private int _logtestid;
		private int _questionid;
		private int? _logtestquestionanswer;
		private string _logtestquestionremark;
		/// <summary>
		/// ID int identity(1,1) primary key
		/// </summary>
		public int LogTestQuestionId
		{
			set{ _logtestquestionid=value;}
			get{return _logtestquestionid;}
		}
		/// <summary>
		/// 外键,对应的测试信息 int not null references LogTest(LogTestId)
		/// </summary>
		public int LogTestId
		{
			set{ _logtestid=value;}
			get{return _logtestid;}
		}
		/// <summary>
		/// 外键,对应的试题实例 int not null references Question(QuestionId)
		/// </summary>
		public int QuestionId
		{
			set{ _questionid=value;}
			get{return _questionid;}
		}
		/// <summary>
		/// 测试时给出的答案 int null check(LogTestQuestionAnswer in(1,2,3,4))
		/// </summary>
		public int? LogTestQuestionAnswer
		{
			set{ _logtestquestionanswer=value;}
			get{return _logtestquestionanswer;}
		}
		/// <summary>
		/// 测试时用户给出的备注 text null
		/// </summary>
		public string LogTestQuestionRemark
		{
			set{ _logtestquestionremark=value;}
			get{return _logtestquestionremark;}
		}
		#endregion Model

	}
}

