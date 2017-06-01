using System;
namespace OnLineTest.Model
{
	/// <summary>
	/// 教材章节表
	/// </summary>
	[Serializable]
	public partial class Chapter
	{
		public Chapter()
		{}
		#region Model
		private int _chapterid;
		private int _textbookid;
		private string _chaptername;
		private int _chapterparentno;
		private int _chapterdeep;
		private string _chapterremark;
		private bool _isverified= false;
		/// <summary>
		/// 章节ID int identity(1,1) primary key
		/// </summary>
		public int ChapterId
		{
			set{ _chapterid=value;}
			get{return _chapterid;}
		}
		/// <summary>
		/// 外键,章节所对应的教材ID int not null references TextBook(TextBookId)
		/// </summary>
		public int TextBookId
		{
			set{ _textbookid=value;}
			get{return _textbookid;}
		}
		/// <summary>
		/// 章节名称 nvarchar(200) not null
		/// </summary>
		public string ChapterName
		{
			set{ _chaptername=value;}
			get{return _chaptername;}
		}
		/// <summary>
		/// 章节的父节点编号 int not null
		/// </summary>
		public int ChapterParentNo
		{
			set{ _chapterparentno=value;}
			get{return _chapterparentno;}
		}
		/// <summary>
		/// 章节深度 int not null
		/// </summary>
		public int ChapterDeep
		{
			set{ _chapterdeep=value;}
			get{return _chapterdeep;}
		}
		/// <summary>
		/// 备注 text null
		/// </summary>
		public string ChapterRemark
		{
			set{ _chapterremark=value;}
			get{return _chapterremark;}
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

