using System;
namespace OnLineTest.Model
{
	/// <summary>
	/// 搜索的热词实例
	/// </summary>
	[Serializable]
	public partial class SuggestionKeyword
	{
		public SuggestionKeyword()
		{}
		#region Model
		private int _suggestionkeywordsid;
		private string _suggestionkeywords;
		private DateTime _suggestionkeywordscreatetime= DateTime.Now;
		private int _suggestionkeywordsnum;
		/// <summary>
		/// int identity(1,1) primary key,-----ID
		/// </summary>
		public int SuggestionKeywordsId
		{
			set{ _suggestionkeywordsid=value;}
			get{return _suggestionkeywordsid;}
		}
		/// <summary>
		/// varchar(100) not null,-----热词//这里要注意长度的问题,varchar100,只能存50个字符,nvarchar100可以存100个
		/// </summary>
		public string SuggestionKeywords
		{
			set{ _suggestionkeywords=value;}
			get{return _suggestionkeywords;}
		}
		/// <summary>
		/// datetime not null default getdate(),-----创建时间
		/// </summary>
		public DateTime SuggestionKeywordsCreateTime
		{
			set{ _suggestionkeywordscreatetime=value;}
			get{return _suggestionkeywordscreatetime;}
		}
		/// <summary>
		/// int not null-----搜索次数
		/// </summary>
		public int SuggestionKeywordsNum
		{
			set{ _suggestionkeywordsnum=value;}
			get{return _suggestionkeywordsnum;}
		}
		#endregion Model

	}
}

