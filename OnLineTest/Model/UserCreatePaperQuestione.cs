using System;
namespace OnLineTest.Model
{
	/// <summary>
	/// 保存用户生成试卷的试题实例
	/// </summary>
	[Serializable]
	public partial class UserCreatePaperQuestione
	{
		public UserCreatePaperQuestione()
		{}
		#region Model
		private int _usercreatepaperquestioneid;
		private int _usercreatepaperid;
		private int _questionid;
		/// <summary>
		/// ID  int identity(1,1) primary key
		/// </summary>
		public int UserCreatePaperQuestioneId
		{
			set{ _usercreatepaperquestioneid=value;}
			get{return _usercreatepaperquestioneid;}
		}
		/// <summary>
		/// 外键,试题所对应的生成试卷信息 int not null references UserCreatePaper(UserCreatePaperId)
		/// </summary>
		public int UserCreatePaperId
		{
			set{ _usercreatepaperid=value;}
			get{return _usercreatepaperid;}
		}
		/// <summary>
		/// 外键,对应的试题 int not null references Question(QuestionId)
		/// </summary>
		public int QuestionId
		{
			set{ _questionid=value;}
			get{return _questionid;}
		}
		#endregion Model

	}
}

