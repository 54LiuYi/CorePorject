using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace CorePorject.Api.Controllers
{
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UploadController : ControllerBase
    {
        private readonly IWebHostEnvironment webHostEnvironment;
        ///private readonly IImgaesServe imgaesServe;

        //,IImgaesServe imgaesServe
        public UploadController(IWebHostEnvironment webHostEnvironment)
        {
            this.webHostEnvironment = webHostEnvironment;
            //this.imgaesServe = imgaesServe;
        }

        // POST api
        [HttpPost]
        public JsonResult Post(IFormFile file)
        {
            if (file == null) { 
                return new JsonResult(new { code = -1, msg = "上传失败，未检测上传的文件信息~" });
            }

            //获取当前时间
            var currentDate = DateTime.Now;
            //获取系统的路径
            var webRootPath = webHostEnvironment.ContentRootPath;//>>>相当于HttpContext.Current.Server.MapPath("")  WebRootPath 
            try
            {

                var filePath = $"/Files/UploadFile/{currentDate:yyyyMMdd}/";
                //创建每日存储文件夹
                if (!Directory.Exists(webRootPath + filePath))
                {
                    //创建子目录
                    Directory.CreateDirectory(webRootPath + filePath);
                }
                //判断文件夹不为空时进入
                if (filePath != null)
                {
                    //文件后缀
                    var fileExtension = Path.GetExtension(file.FileName);//获取文件格式，拓展名
                    //判断文件大小
                    var fileSize = file.Length;
                    if (fileSize > 1024 * 1024 * 10) //10M TODO:(1mb=1024X1024b)
                    {
                        return new JsonResult(new { isSuccess = false, resultMsg = "上传的文件不能大于10M" });
                    }
                    //保存的文件名称(以名称和保存时间命名)
                    var saveName = file.FileName.Substring(0, file.FileName.LastIndexOf('.')) + "_" + currentDate.ToString("HHmmss") + fileExtension;


                    //进行文件刻录 从 原始路径刻录到新路径webRootPath + filePath + saveName
                    FileStream fs = new FileStream(webRootPath + filePath + saveName, FileMode.Create);
                    file.CopyTo(fs);

                    fs.Flush();
                    fs.Close();
                    //完整的文件路径
                    var completeFilePath = Path.Combine(filePath, saveName);

                    //保存到数据库 图片库表
                    //Image image = new Image();
                    //image.Title = saveName;
                    //image.Path = completeFilePath;
                    //image.Size = (int)fileSize;
                    //image.Extension = fileExtension.ToLower();
                    //image.Modular = 1;
                    //var img = imgaesServe.AddImage(image);
                    //imgID = img.ImgId ,
                    return new JsonResult(new { code = 1, msg = "上传成功", path = completeFilePath });
                }
                else
                {
                    return new JsonResult(new { code = -3, msg = "上传失败，文件存储异常~" });
                }
            }
            catch (Exception ex)
            {
                return new JsonResult(new { code = -2, msg = "文件保存失败，异常信息为：" + ex.Message });
            }
        }
    }
}
