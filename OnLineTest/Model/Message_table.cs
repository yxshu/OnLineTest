using System;
namespace OnLineTest.Model
{
	/// <summary>
	/// 站内信实例
	/// </summary>
	[Serializable]
	public partial class Message_table
	{
		public Message_table()
		{}
		#region Model
		private int _messageid;
		private int? _messageparentid;
		private int _messageto;
		private int _messagefrom;
		private string _messagecontent;
		private DateTime? _messagesendtime= DateTime.Now;
		private bool _messageisread= false;
		private DateTime? _messagereadtime;
		/// <summary>
		/// ID int identity(1,1) primary key
		/// </summary>
		public int MessageId
		{
			set{ _messageid=value;}
			get{return _messageid;}
		}
		/// <summary>
		/// 自身的外键,如果是回复的话,标明回复哪一条站内信 int null 
		/// </summary>
		public int? MessageParentId
		{
			set{ _messageparentid=value;}
			get{return _messageparentid;}
		}
		/// <summary>
		/// 外键,收信用户 int not null references Users(UserId)
		/// </summary>
		public int MessageTO
		{
			set{ _messageto=value;}
			get{return _messageto;}
		}
		/// <summary>
		/// 外键,发信用户 int not null references Users(UserId)
		/// </summary>
		public int MessageFrom
		{
			set{ _messagefrom=value;}
			get{return _messagefrom;}
		}
		/// <summary>
		/// 信件内容 text not null
		/// </summary>
		public string MessageContent
		{
			set{ _messagecontent=value;}
			get{return _messagecontent;}
		}
		/// <summary>
		/// 发送时间 datetime default getdate()
		/// </summary>
		public DateTime? MessageSendTime
		{
			set{ _messagesendtime=value;}
			get{return _messagesendtime;}
		}
		/// <summary>
		/// 是否阅读标记 bit not null default 0
		/// </summary>
		public bool MessageIsRead
		{
			set{ _messageisread=value;}
			get{return _messageisread;}
		}
		/// <summary>
		/// 如果阅读,阅读时间 datetime null
		/// </summary>
		public DateTime? MessageReadTime
		{
			set{ _messagereadtime=value;}
			get{return _messagereadtime;}
		}
		#endregion Model

	}
}

