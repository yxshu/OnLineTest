using System;
namespace OnLineTest.Model
{
	/// <summary>
	/// 用户组实例
	/// </summary>
	[Serializable]
	public partial class UserGroup
	{
		public UserGroup()
		{}
		#region Model
		private int _usergroupid;
		private string _usergroupname;
		private string _usergroupremark;
		/// <summary>
		/// 用户组ID号 int identity primary key
		/// </summary>
		public int UserGroupId
		{
			set{ _usergroupid=value;}
			get{return _usergroupid;}
		}
		/// <summary>
		/// 用户组名称 varchar(20) unique
		/// </summary>
		public string UserGroupName
		{
			set{ _usergroupname=value;}
			get{return _usergroupname;}
		}
		/// <summary>
		/// 用户组备注 text null
		/// </summary>
		public string UserGroupRemark
		{
			set{ _usergroupremark=value;}
			get{return _usergroupremark;}
		}
		#endregion Model

	}
}

