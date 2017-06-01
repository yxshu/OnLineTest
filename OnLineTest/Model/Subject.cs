using System;
namespace OnLineTest.Model
{
	/// <summary>
	/// 科目实例
	/// </summary>
	[Serializable]
	public partial class Subject
	{
		public Subject()
		{}
		#region Model
		private int _subjectid;
		private string _subjectname;
		private string _subjectremark;
		private bool _isverified= false;
		/// <summary>
		/// 科目ID  int identity(1,1) primary key
		/// </summary>
		public int SubjectId
		{
			set{ _subjectid=value;}
			get{return _subjectid;}
		}
		/// <summary>
		/// 科目名称 nvarchar(50) not null
		/// </summary>
		public string SubjectName
		{
			set{ _subjectname=value;}
			get{return _subjectname;}
		}
		/// <summary>
		/// 备注 text null
		/// </summary>
		public string SubjectRemark
		{
			set{ _subjectremark=value;}
			get{return _subjectremark;}
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

