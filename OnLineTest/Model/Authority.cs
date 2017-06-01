
using System;
namespace OnLineTest.Model
{
	/// <summary>
	/// 权限实例
	/// </summary>
	[Serializable]
	public partial class Authority
	{
		public Authority()
		{}
		#region Model
		private int _authorityid;
		private string _authorityname;
		private int _authoritydeep;
		private int _authorityparentid;
		private int _authorityscore;
		private string _authorityhandlerpage;
		private int? _authorityordernum=0;
		private string _authorityremark;
		/// <summary>
		/// int identity(1,1) primary key,-----权限ID
		/// </summary>
		public int AuthorityId
		{
			set{ _authorityid=value;}
			get{return _authorityid;}
		}
		/// <summary>
		/// nvarchar(20) not null,-----此项权限的中文名称
		/// </summary>
		public string AuthorityName
		{
			set{ _authorityname=value;}
			get{return _authorityname;}
		}
		/// <summary>
		/// int not null,-----此权限的深度(第一级定义为0) 
		/// </summary>
		public int AuthorityDeep
		{
			set{ _authoritydeep=value;}
			get{return _authoritydeep;}
		}
		/// <summary>
		/// int not null,-----此项权限的父编号(第一级的父编号为0)
		/// </summary>
		public int AuthorityParentId
		{
			set{ _authorityparentid=value;}
			get{return _authorityparentid;}
		}
		/// <summary>
		///  int  not null,-----此项权限（动作）所对应的分值(可以为负值)
		/// </summary>
		public int AuthorityScore
		{
			set{ _authorityscore=value;}
			get{return _authorityscore;}
		}
		/// <summary>
		///  varchar(50) not null,-----此项权限的处理页面
		/// </summary>
		public string AuthorityHandlerPage
		{
			set{ _authorityhandlerpage=value;}
			get{return _authorityhandlerpage;}
		}
		/// <summary>
		/// int default 0,-------此项权限的排序号
		/// </summary>
		public int? AuthorityOrderNum
		{
			set{ _authorityordernum=value;}
			get{return _authorityordernum;}
		}
		/// <summary>
		/// text not null,-----此项权限所的备注
		/// </summary>
		public string AuthorityRemark
		{
			set{ _authorityremark=value;}
			get{return _authorityremark;}
		}
		#endregion Model

	}
}

