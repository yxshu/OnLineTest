using System;
namespace OnLineTest.Model
{
	/// <summary>
	/// 试题的难度系数实例
	/// </summary>
	[Serializable]
	public partial class Difficulty
	{
		public Difficulty()
		{}
		#region Model
		private int _difficultyid;
		private int _difficultyratio=5;
		private string _difficultydescrip;
		private string _difficultyremark;
		/// <summary>
		/// 难度系数ID int identity(1,1) primary key
		/// </summary>
		public int DifficultyId
		{
			set{ _difficultyid=value;}
			get{return _difficultyid;}
		}
		/// <summary>
		/// 难度系数0-10(0最容易,10最难) int not null default 5 check( DifficultyRatio>=0 and DifficultyRatio<=10)
		/// </summary>
		public int DifficultyRatio
		{
			set{ _difficultyratio=value;}
			get{return _difficultyratio;}
		}
		/// <summary>
		/// 难度系数对应的描述 nvarchar(20) not null
		/// </summary>
		public string DifficultyDescrip
		{
			set{ _difficultydescrip=value;}
			get{return _difficultydescrip;}
		}
		/// <summary>
		/// 备注 text null
		/// </summary>
		public string DifficultyRemark
		{
			set{ _difficultyremark=value;}
			get{return _difficultyremark;}
		}
		#endregion Model

	}
}

