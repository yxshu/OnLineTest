<%@ WebHandler Language="C#" Class="getMessage_table" %>

using System;
using System.Web;
using System.Web.SessionState;
using System.Web.Script.Serialization;
using OnLineTest.Model;
using System.Collections.Generic;
using OnLineTest.BLL;
public class getMessage_table : IHttpHandler, IRequiresSessionState
{
    public static Message_tableManager messagemanager = new Message_tableManager();
    public void ProcessRequest(HttpContext context)
    {
        JavaScriptSerializer serializer = new JavaScriptSerializer();
        List<Dictionary<string, object>> result = null;
        Users CurrentUser = context.Session["User"] == null ? null : (Users)context.Session["User"];
        string model = string.IsNullOrEmpty(context.Request.Form["model"]) ? null : context.Request.Form["model"];//模式，有四个值：received,sended,create,null

        if (CurrentUser != null && model != null)
        {
            result = new List<Dictionary<string, object>>();
            switch (model)
            {
                case "sended": result = getSendedMessage(context, CurrentUser); break;//根据当前登录的用户，取得已经发送的信息
                case "received": result = getReceivedMessage(context, CurrentUser); break;//根据当前登录的用户，取得接收的信息
                case "create": result = createMessage(context, CurrentUser); break;//新建站内信息
                default: result = null; break;
            }
        }
        context.Response.ContentType = "text/plain";
        context.Response.Write(serializer.Serialize(result));
    }
    /// <summary>
    /// 获取已发件信息
    /// </summary>
    /// <param name="pagecount">页容量</param>
    /// <param name="pagenum">页码</param>
    /// <param name="sendeduser">发件人</param>
    /// <returns></returns>
    public List<Dictionary<string, object>> getSendedMessage(HttpContext context, Users sendeduser)
    {
        List<Dictionary<string, object>> result = null;
        int pageCount = context.Request.Form["page"] == null ? 10 : Convert.ToInt32(context.Request.Form["page"]);//页容量,默认值为10
        int pagenum = context.Request.Form["pagenum"] == null ? 0 : Convert.ToInt32(context.Request.Form["pagenum"]) - 1; //页码，默认值为0
        result = messagemanager.getDictionarySendList(pageCount, pagenum, sendeduser.UserId);
        return result;
    }
    /// <summary>
    /// 获取收件信息
    /// </summary>
    /// <param name="pagecount">页容量</param>
    /// <param name="pagenum">页码</param>
    /// <param name="receiveuser">收件人</param>
    /// <returns></returns>
    public List<Dictionary<string, object>> getReceivedMessage(HttpContext context, Users receiveuser)
    {
        List<Dictionary<string, object>> result = null;
        int pageCount = context.Request.Form["page"] == null ? 10 : Convert.ToInt32(context.Request.Form["page"]);//页容量,默认值为10
        int pagenum = context.Request.Form["pagenum"] == null ? 0 : Convert.ToInt32(context.Request.Form["pagenum"]) - 1; //页码，默认值为0
        result = messagemanager.getDictionaryReceiveList(pageCount, pagenum, receiveuser.UserId);
        return result;
    }
    /// <summary>
    /// 新建站内信(代码有雍余，太晚了， 不想改了，明天再说吧)
    /// </summary>
    /// <param name="context">用户发送的内容</param>
    /// <param name="createuser">当前用户</param>
    /// <returns></returns>
    public List<Dictionary<string, object>> createMessage(HttpContext context, Users createuser)
    {
        List<Dictionary<string, object>> result = new List<Dictionary<string, object>>();
        bool createsuccess = false;

        // 处理多个收件人的问题
        string username = context.Request.Form["messageto"] != null ? context.Request.Form["messageto"] : string.Empty;
        string[] usernames = username.Split(new char[] { ':', ';', '；', ',', '，' }, StringSplitOptions.RemoveEmptyEntries);//取得收件人列表，其中可能包含第一个“收件人”的提示
        UsersManager usermanager = new UsersManager();
        List<Users> messageto = new List<Users>();
        for (int i = 0; i < usernames.Length; i++)
        {
            if (!string.IsNullOrEmpty(usernames[i].Trim()) && usermanager.Exists(usernames[i].Trim()))
            {
                messageto.Add(usermanager.GetModel(usernames[i].Trim()));
            }
        }

        string content = HttpUtility.HtmlEncode(context.Request.Form["messagecontent"].Trim());//格式化信件内容
        int from = createuser.UserId;//处理发件人
        int messageparentid = -1;
        if (Int32.TryParse(context.Request.Form["messageparentid"], out messageparentid) && new Message_tableManager().Exists(messageparentid))//处理parentid
        { }
        else
        {
            messageparentid = -1;
        }
        for (int j = 0; j < messageto.Count; j++)
        {
            Message_table message = new Message_table();
            if (messageparentid > 0) { message.MessageParentId = messageparentid; }
            message.MessageTO = messageto[j].UserId;
            message.MessageFrom = from;
            message.MessageContent = content;

            message.MessageSendTime = DateTime.Now;
            message.MessageIsRead = false;
            message.MessageReadTime = null;
            try
            {
                message.MessageId = messagemanager.Add(message);
                createsuccess = true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            Dictionary<string, object> dic = new Dictionary<string, object>();
            dic.Add("result", createsuccess);
            result.Add(dic);
        }
        return result;
    }
    public bool IsReusable
    {
        get
        {
            return false;
        }
    }

}