using Microsoft.AspNetCore.Mvc;
using Middleware_DatabaseAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using Middleware_Tool;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace Middleware_CoreWeb.Controllers.api
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ApiBaseController
    {
        private DB_User Get_UserpDb = new DB_User();

        [HttpGet]
        public JsonData<Userinfo> Get([FromQuery]int limit, int offset, string Name, int Group) => Get_UserpDb.GetList(limit, offset, Name, Group);

        [HttpPut("add")]
        public Basemessage Put([FromBody]Userinfo item)
        {
            if (Get_UserpDb.Get(item).Any())
            {
                return Error("账户已存在");
            }
            else
            {
                item.Imgurl = "user.svg";
                item.CreateTime = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                return Get_UserpDb.Add(item) ? succeed() : Error();
            }
        }

        [HttpDelete]
        public Basemessage delete([FromBody]List<Userinfo> itemArray) => Get_UserpDb.Delete(itemArray) ? succeed() : Error();

        [HttpPut("edit")]
        public Basemessage Putedit([FromBody]Userinfo item) => Get_UserpDb.Update(item) ? succeed() : Error();

        [HttpPut("{id}")]
        public async Task<Basemessage> UpdateShowNameAsync(int? id, [FromBody]Userinfo _user)
        {
            if ((id == 1 && string.IsNullOrWhiteSpace(_user.showName)) || (id == 2 && (string.IsNullOrWhiteSpace(_user.Pwd) || string.IsNullOrWhiteSpace(_user.originalPwd))))
                return Error("不能为空");
            var user = await appInfo.GetUserAsync(HttpContext);
            if (user != null)
            {
                if (id == 1)
                {
                    user.showName = _user.showName;
                }
                else if (id == 2)
                {
                    if (user.Pwd == _user.originalPwd)
                    {
                        user.Pwd = _user.Pwd;
                    }
                    else
                    {
                        return Error("原密码不正确");
                    }
                }
                else
                {
                    return Error("id错误");
                }
                return Get_UserpDb.Update(user) ? succeed() : Error();
            }
            return Error();
        }

        [HttpPost]
        public async Task<JsonResult> Post([FromServices]IWebHostEnvironment env, IFormCollection files)
        {
            var previewUrl = string.Empty;
            long fileSize = 0;
            var user = await HttpContext.GetUserAsync();
            var fileName = string.Empty;
            if (files.Files.Count > 0)
            {
                var uploadFile = files.Files[0];
                var webSiteUrl = "~/images/uploader/";
                fileName = $"{user.id}{Path.GetExtension(uploadFile.FileName)}";
                var filePath = Path.Combine(env.WebRootPath, webSiteUrl.Replace("~", string.Empty).Replace('/', Path.DirectorySeparatorChar).TrimStart(Path.DirectorySeparatorChar) + fileName);
                var fileFolder = Path.GetDirectoryName(filePath);
                fileSize = uploadFile.Length;
                if (!Directory.Exists(fileFolder)) Directory.CreateDirectory(fileFolder);
                using (var fs = new FileStream(filePath, FileMode.Create))
                {
                    await uploadFile.CopyToAsync(fs);
                }
                var iconName = $"{fileName}?v={DateTime.Now.Ticks}";
                previewUrl = Url.Content($"{webSiteUrl}{iconName}");
                user.Imgurl = fileName;
                Get_UserpDb.Update(user);
            }
            return new JsonResult(new
            {
                initialPreview = new string[] { previewUrl },
                initialPreviewConfig = new object[] {
                    new { caption = "新头像", size = fileSize, showZoom = true, key = fileName }
                },
                append = false
            });
        }
    }
}