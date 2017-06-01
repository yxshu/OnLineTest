<%@ WebHandler Language="C#" Class="UpLoadImage" %>

using System;
using System.Web;
using System.Text.RegularExpressions;
using System.IO;
using System.Web.SessionState;
using OnLineTest.Model;
using OnLineTest.BLL;
using System.Data;
using log4net;

public class UpLoadImage : IHttpHandler, IReadOnlySessionState
{
    private static ILog logger = LogManager.GetLogger(typeof(UpLoadImage));
    public void ProcessRequest(HttpContext context)
    {
        HttpPostedFile file = context.Request.Files[0];
        string Type = context.Request.Form["Type"];//因为次文件为多个地方接收图像，所以要标识图片的类型
        int filelength = file.ContentLength / 1024;
        string filename = file.FileName;
        string newfilename = null;
        Users user = (Users)context.Session["User"];
        QuestionManager manager = new QuestionManager();
        if (filelength <= 1024 && new Regex("^\\w+\\.(jpg|gif|bmp|png|jpeg)$", RegexOptions.IgnoreCase).IsMatch(filename) && context.Session["User"] != null)
        {
            switch (Type)
            {
                case "Question":
                    newfilename = SaveImg(user, file);
                    break;
                case "UpdataQuestion":
                    int QuestionId = -1;
                    if (int.TryParse(context.Request.Form["QuestionId"].ToString(), out QuestionId) && manager.Exists(QuestionId))
                    {
                        Question question = manager.GetModel(QuestionId);
                        if (question.UserId == user.UserId)
                        {
                            newfilename = SaveImg(user, file);//将图片保存
                            question.ImageAddress = newfilename;
                            if (manager.Update(question)&&filename!="Default.jpg")
                            {
                                try
                                {
                                    File.Delete(Path.Combine(System.Web.HttpContext.Current.Server.MapPath("~\\QuestionImages"), filename));
                                }
                                catch (Exception ex)
                                {
                                    logger.Error("删除图片错误", ex);
                                }
                            }
                        }
                    }
                    break;
            }
        }
        context.Response.ContentType = "text/plain";
        context.Response.Write(newfilename);
    }
    /// <summary>
    /// 保存图片，并返回保存的图片的新的名称
    /// </summary>
    /// <param name="user">当前用户，命名的时候用到</param>
    /// <param name="file">要保存的图片流</param>
    /// <returns>成功则返回图片新的名称，不成功则返回  字符串"false"</returns>
    public string SaveImg(Users user, HttpPostedFile file)
    {
        string result = "false";
        string newfilename = user.UserId.ToString() + "_" + DateTime.Now.Ticks + Path.GetExtension(file.FileName);
        string path = Path.Combine(System.Web.HttpContext.Current.Server.MapPath("~\\QuestionImages"), newfilename);
        try
        {
            file.SaveAs(path);
            result = newfilename;
        }
        catch (Exception ex)
        {
            logger.Error("保存图片错误", ex);
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