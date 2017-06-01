using System;
namespace OnLineTest.Model
{
	/// <summary>
	/// 教材实例
	/// </summary>
	[Serializable]
	public partial class TextBook
	{
		public TextBook()
		{}
		#region Model
		private int _textbookid;
		private int _papercodeid;
		private string _textbookname;
		private string _isbn;
		private bool _isverified= false;
		/// <summary>
		/// 教材ID int identity(1,1) primary key
		/// </summary>
		public int TextBookId
		{
			set{ _textbookid=value;}
			get{return _textbookid;}
		}
		/// <summary>
		/// 外键,教材所对应的试卷代码ID(一门课程可以有多本教材) int not null
		/// </summary>
		public int PaperCodeId
		{
			set{ _papercodeid=value;}
			get{return _papercodeid;}
		}
		/// <summary>
		/// 教材名称 nvarchar(200) null
		/// </summary>
		public string TextBookName
		{
			set{ _textbookname=value;}
			get{return _textbookname;}
		}
		/// <summary>
		/// 书号 nvarchar(50) null
		/// </summary>
		public string ISBN
		{
			set{ _isbn=value;}
			get{return _isbn;}
		}
		/// <summary>
		/// 是否通过审核标记 bit not null default(0)
		/// </summary>
		public bool IsVerified
		{
			set{ _isverified=value;}
			get{return _isverified;}
		}
		#endregion Model

	}
}

