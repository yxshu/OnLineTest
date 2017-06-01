<%@ WebHandler Language="C#" Class="HandlerValidCode" %>

using System;
using System.Web;
using System.Web.SessionState;

public class HandlerValidCode : IHttpHandler, IRequiresSessionState
{
    //处理验证码
    log4net.ILog logger = log4net.LogManager.GetLogger(typeof(HandlerValidCode));
    public void ProcessRequest(HttpContext context)
    {
        System.IO.MemoryStream ms = new System.IO.MemoryStream();
        string ValidCode = string.Empty;
        int wordnum = 6; int height = 30;//这是默认值
        if (context.Request.QueryString.HasKeys())
        {
            //申请验证码的长度，默认为6位
            if (!string.IsNullOrEmpty(context.Request.QueryString["wordnum"]))
            {
                Int32.TryParse(context.Request.QueryString["wordnum"], out wordnum);
            }
            //申请验证码的高度值，默认为30px
            if (!string.IsNullOrEmpty(context.Request.QueryString["height"]))
            {
                Int32.TryParse(context.Request.QueryString["height"], out height);
            }
        }
        try
        {
            //生成验证码流
            ms = OnLineTest.BLL.common.CreateValidCode(wordnum, height, out ValidCode);
        }
        catch (Exception ex)
        {
            logger.Error("验证码流生成错误。", ex);
        }
        int length = (Int32)ms.Length;
        byte[] buffer = new byte[ms.Length];
        ms.Position = 0;
        if (ms != null && !string.IsNullOrEmpty(ValidCode))
        {
            try
            {
                int count = ms.Read(buffer, 0, length);
                context.Session["ValidCode"] = ValidCode;
            }
            catch (Exception ex)
            {
                logger.Error("将验证码流保存为二进制数组过程错误。", ex);
                throw ex;
            }
            finally
            {
                ms.Close();
            }
        }
        context.Response.ContentType = "image/gif";
        context.Response.BinaryWrite(buffer);
    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }

}