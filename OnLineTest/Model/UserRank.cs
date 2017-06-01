using System;
namespace OnLineTest.Model
{
	/// <summary>
	/// 用户等级实例
	/// </summary>
	[Serializable]
	public partial class UserRank
	{
		public UserRank()
		{}
		#region Model
		private int _userrankid;
		private string _userrankname;
		private int _minscore;
		private int _maxscore;
		/// <summary>
		/// 用户等级Id int identity(1,1) primary key
		/// </summary>
		public int UserRankId
		{
			set{ _userrankid=value;}
			get{return _userrankid;}
		}
		/// <summary>
		/// 用户等级对应的中文名称 nvarchar(20) not null unique
		/// </summary>
		public string UserRankName
		{
			set{ _userrankname=value;}
			get{return _userrankname;}
		}
		/// <summary>
		/// 等级所对应的最低分值 int not null check(MinScore>=0)
		/// </summary>
		public int MinScore
		{
			set{ _minscore=value;}
			get{return _minscore;}
		}
		/// <summary>
		/// 等级所对应的最高分值 int not null check(MaxScore>=0)
		/// </summary>
		public int MaxScore
		{
			set{ _maxscore=value;}
			get{return _maxscore;}
		}
		#endregion Model

	}
}

