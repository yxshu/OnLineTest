using System;
namespace OnLineTest.Model
{
	/// <summary>
	/// 试卷代码实例
	/// </summary>
	[Serializable]
	public partial class PaperCodes
	{
		public PaperCodes()
		{}
		#region Model
		private int _papercodeid;
		private int _subjectid;
		private int _papercode;
		private string _chinesename;
		private int _papercodepassscore;
		private int _papercodetotalscore;
		private int _timerange;
		private string _papercoderemark;
		private bool _isverified= false;
		/// <summary>
		/// 试卷代码ID int identity(1,1) primary key
		/// </summary>
		public int PaperCodeId
		{
			set{ _papercodeid=value;}
			get{return _papercodeid;}
		}
		/// <summary>
		/// 外键，试卷代码所对应的科目ID int not null references Subject(SubjectId)
		/// </summary>
		public int SubjectId
		{
			set{ _subjectid=value;}
			get{return _subjectid;}
		}
		/// <summary>
		/// 试卷代码 int not null unique
		/// </summary>
		public int PaperCode
		{
			set{ _papercode=value;}
			get{return _papercode;}
		}
		/// <summary>
		/// 试卷代码所对应的中文名称 nvarchar(100) not null
		/// </summary>
		public string ChineseName
		{
			set{ _chinesename=value;}
			get{return _chinesename;}
		}
		/// <summary>
		/// 试卷代码的及格分数线 int not null PaperCodePassScore>0  PaperCodeTotalScore>PaperCodePassScore
		/// </summary>
		public int PaperCodePassScore
		{
			set{ _papercodepassscore=value;}
			get{return _papercodepassscore;}
		}
		/// <summary>
		/// 试卷代码的总分数 int not null PaperCodeTotalScore>0 PaperCodeTotalScore>PaperCodePassScore
		/// </summary>
		public int PaperCodeTotalScore
		{
			set{ _papercodetotalscore=value;}
			get{return _papercodetotalscore;}
		}
		/// <summary>
		/// 试卷代码的考试时长 int not null TimeRange>0
		/// </summary>
		public int TimeRange
		{
			set{ _timerange=value;}
			get{return _timerange;}
		}
		/// <summary>
		/// 试卷代码的备注 text null
		/// </summary>
		public string PaperCodeRemark
		{
			set{ _papercoderemark=value;}
			get{return _papercoderemark;}
		}
		/// <summary>
		/// 试卷代码是否通过审核标记 bit not null default(0)
		/// </summary>
		public bool IsVerified
		{
			set{ _isverified=value;}
			get{return _isverified;}
		}
		#endregion Model

	}
}

