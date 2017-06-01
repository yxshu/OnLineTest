using System;
namespace OnLineTest.Model
{
	/// <summary>
	/// 考区信息实例
	/// </summary>
	[Serializable]
	public partial class ExamZone
	{
		public ExamZone()
		{}
		#region Model
		private int _examzoneid;
		private string _examzonename;
		private bool _isverified= false;
		/// <summary>
		/// 考区ID int identity(1,1) primary key
		/// </summary>
		public int ExamZoneId
		{
			set{ _examzoneid=value;}
			get{return _examzoneid;}
		}
		/// <summary>
		/// 考区名称 nvarchar(20) not null
		/// </summary>
		public string ExamZoneName
		{
			set{ _examzonename=value;}
			get{return _examzonename;}
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

