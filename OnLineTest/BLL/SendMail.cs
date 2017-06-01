using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Mail;
using System.Configuration;
using OnLineTest.DBUtility;
namespace BLL
{
    public class Mail
    {
        /// <summary>
        /// 发送邮件方法
        /// </summary>
        /// <param name="to">收件人</param>
        /// <param name="subject">主题</param>
        /// <param name="body">邮件内容</param>
        /// <returns>成功为true,失败为false</returns>
        public bool Send(string to, string subject, string body)
        {
            return Send(to, subject, body, "SendMailClient");
        }
        /// <summary>
        /// 发送邮件方法
        /// </summary>
        /// <param name="to">收件人</param>
        /// <param name="subject">主题</param>
        /// <param name="body">邮件内容</param>
        /// <param name="appSettingsKey">关于发件人信息在config中的配置节点名称</param>
        /// <returns>成功为true,失败为false</returns>
        public bool Send(string to, string subject, string body, string appSettingsKey)
        {
            bool result = false;
            string from, fromname, username, password, server;
            //string from = "yxshu82@163.com", fromname = "船员在线考试系统官方", username = "yxshu82@163.com", password = "Ashulovejuan1", server = "smtp.163.com";
            string SendMailClient = DESEncrypt.Decrypt(ConfigurationManager.AppSettings[appSettingsKey]);
            //string SendMailClient = "yxshu82@163.com,船员在线考试系统,yxshu82@163.com,Ashulovejuan1,smtp.163.com";
            //From,FromName,UserName,Password,Server
            string[] sendmailmessage = SendMailClient.Split(',');
            if (sendmailmessage.Length == 5)
            {
                from = sendmailmessage[0];
                fromname = sendmailmessage[1];
                username = sendmailmessage[2];
                password = sendmailmessage[3];
                server = sendmailmessage[4];

                if (Send(from, fromname, to, subject, body, username, password, server, "") == "send ok")
                {
                    result = true;
                }
            }
            return result;
        }
        /// <summary> 
        /// 发送邮件程序 
        /// </summary> 
        /// <param name="from">发送人邮件地址</param> 
        /// <param name="fromname">发送人显示名称</param> 
        /// <param name="to">发送给谁（邮件地址）</param> 
        /// <param name="subject">标题</param> 
        /// <param name="body">内容</param> 
        /// <param name="username">邮件登录名</param> 
        /// <param name="password">邮件密码</param> 
        /// <param name="server">邮件服务器</param> 
        /// <param name="fujian">附件</param> 
        /// <returns>send ok</returns> 
        /// 调用方法 SendMail("abc@126.com", "某某人", "cba@126.com", "你好", "我测试下邮件", "邮箱登录名", "邮箱密码", "smtp.126.com", ""); 
        public string Send(string from, string fromname, string to, string subject, string body, string username, string password, string server, string fujian)
        {
            try
            {
                //邮件发送类 
                MailMessage mail = new MailMessage();
                //是谁发送的邮件 
                mail.From = new MailAddress(from, fromname);
                //发送给谁 
                mail.To.Add(to);
                //标题 
                mail.Subject = subject;
                //内容编码 
                mail.BodyEncoding = Encoding.Default;
                //发送优先级 
                mail.Priority = MailPriority.High;
                //邮件内容 
                mail.Body = body;
                //是否HTML形式发送 
                mail.IsBodyHtml = true;
                //附件 
                if (fujian.Length > 0)
                {
                    mail.Attachments.Add(new Attachment(fujian));
                }
                //邮件服务器和端口 
                SmtpClient smtp = new SmtpClient(server, 25);
                smtp.UseDefaultCredentials = true;
                //指定发送方式 
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                //指定登录名和密码 
                smtp.Credentials = new System.Net.NetworkCredential(username, password);
                //超时时间 
                smtp.Timeout = 1000;
                smtp.Send(mail);
                return "send ok";
            }
            catch (Exception exp)
            {
                return exp.Message;
            }
        }
    }
}






