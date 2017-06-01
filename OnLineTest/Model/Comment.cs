using System;
namespace OnLineTest.Model
{
	/// <summary>
	/// 用户对试题的评论实例
	/// </summary>
	[Serializable]
	public partial class Comment
	{
		public Comment()
		{}
		#region Model
		private int _commentid;
		private int _questionid;
		private int _userid;
		private string _commentcontent;
		private DateTime _commenttime= DateTime.Now;
		private int? _quotecommentid;
		private bool _isdeleted= false;
		private int? _deleteuserid;
		private DateTime? _deletecommenttime;
		/// <summary>
		/// ID int identity(1,1) primary key
		/// </summary>
		public int CommentId
		{
			set{ _commentid=value;}
			get{return _commentid;}
		}
		/// <summary>
		/// 外键,评论所对应的试题 int not null references Question(QuestionId)
		/// </summary>
		public int QuestionId
		{
			set{ _questionid=value;}
			get{return _questionid;}
		}
		/// <summary>
		/// 外键,发表评论的用户 int not null references Users(UserId)
		/// </summary>
		public int UserId
		{
			set{ _userid=value;}
			get{return _userid;}
		}
		/// <summary>
		/// 评论内容 text not null
		/// </summary>
		public string CommentContent
		{
			set{ _commentcontent=value;}
			get{return _commentcontent;}
		}
		/// <summary>
		/// 评论时间 datetime not null default getdate()
		/// </summary>
		public DateTime CommentTime
		{
			set{ _commenttime=value;}
			get{return _commenttime;}
		}
		/// <summary>
		/// 引用的评论 int null references Comment(CommentId)
		/// </summary>
		public int? QuoteCommentId
		{
			set{ _quotecommentid=value;}
			get{return _quotecommentid;}
		}
		/// <summary>
		/// 是否删除 bit not null default 0
		/// </summary>
		public bool IsDeleted
		{
			set{ _isdeleted=value;}
			get{return _isdeleted;}
		}
		/// <summary>
		/// 外键 删除评论人 int null references Users(UserId)
		/// </summary>
		public int? DeleteUserId
		{
			set{ _deleteuserid=value;}
			get{return _deleteuserid;}
		}
		/// <summary>
		/// 删除评论时间 datetime null
		/// </summary>
		public DateTime? DeleteCommentTime
		{
			set{ _deletecommenttime=value;}
			get{return _deletecommenttime;}
		}
		#endregion Model

	}
}

