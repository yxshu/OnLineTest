using System;
namespace OnLineTest.Model
{
	/// <summary>
	/// 试题在审核过程中的状态实例
	/// </summary>
	[Serializable]
	public partial class VerifyStatus
	{
		public VerifyStatus()
		{}
        //VerifyStatusId int identity(1,1) primary key,-----审核状态ID
        //QuestionId int not null references Question(QuestionId),-----与试题库中对应的试题ID（要审核通过以后才能更新到试题库）
        //UserId int not null references Users(UserId),-----审核人ID
        //VerifyTimes int not null default 1,-----审核次数（三次为通过）
        //IsPass bit not null default 0,-----是否通过标记
        //VerifyTime datetime not null default getdate(),-----审核时间
		#region Model
		private int _verifystatusid;
		private int _questionid;
		private int _userid;
		private int _verifytimes=1;
		private bool _ispass= false;
		private DateTime _verifytime= DateTime.Now;
		/// <summary>
		/// ID int identity(1,1) primary key
		/// </summary>
		public int VerifyStatusId
		{
			set{ _verifystatusid=value;}
			get{return _verifystatusid;}
		}
		/// <summary>
		/// 外键,与试题实例ID相对应(要审核通过以后才能更新到试题库) int not null references Question(QuestionId)
		/// </summary>
		public int QuestionId
		{
			set{ _questionid=value;}
			get{return _questionid;}
		}
		/// <summary>
		/// 外键,审核人用户ID int not null references Users(UserId)
		/// </summary>
		public int UserId
		{
			set{ _userid=value;}
			get{return _userid;}
		}
		/// <summary>
		/// 审核次数 int not null default 1
		/// </summary>
		public int VerifyTimes
		{
			set{ _verifytimes=value;}
			get{return _verifytimes;}
		}
		/// <summary>
		/// 是否通过标记(通过时,更新此字段) bit not null default 0
		/// </summary>
		public bool IsPass
		{
			set{ _ispass=value;}
			get{return _ispass;}
		}
		/// <summary>
		/// 审核时间 datetime not null default getdate()
		/// </summary>
		public DateTime VerifyTime
		{
			set{ _verifytime=value;}
			get{return _verifytime;}
		}
		#endregion Model

	}
}

