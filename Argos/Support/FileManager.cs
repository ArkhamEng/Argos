using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace Argos.Support
{
    public enum FileType
    {
        ProfilePicture
    }
    public class FileManager
    {
        public static string SaveFile(HttpPostedFileBase file, string parentId, FileType type)
        {
            try
            {
                var fileName = Path.GetFileName(file.FileName); //getting only file name(ex-ganesh.jpg)  
                var extension = Path.GetExtension(file.FileName); //getting the extension(ex-.jpg)

                string serverUrl = string.Empty;

                switch (type)
                {
                    case FileType.ProfilePicture:
                        serverUrl = Cons.UserPicturePath + "/" + parentId.ToString();
                        break;
                }

                var serverPath = HttpContext.Current.Server.MapPath(serverUrl);

                //si el archivo es una foto de perfil y ya existe un directorio, lo borro y lo vuelvo a crear
                //ya que solo se permite una foto de perfil por usuario
                if (type == FileType.ProfilePicture && Directory.Exists(serverPath))
                    Directory.Delete(serverPath, true);

                Directory.CreateDirectory(serverPath);

                var fullRealPath = Path.Combine(serverPath, fileName);
                var fullVirtualPath = serverUrl + "/" + fileName;

                file.SaveAs(fullRealPath);

                return fullVirtualPath;
            }
            catch (System.Exception ex)
            {
                return string.Empty;
            }
        }
        public static string SaveFile(HttpPostedFile file, string parentId, FileType type)
        {
            try
            {
                var fileName = Path.GetFileName(file.FileName); //getting only file name(ex-ganesh.jpg)  
                var extension = Path.GetExtension(file.FileName); //getting the extension(ex-.jpg)

                string serverUrl = string.Empty;

                switch (type)
                {
                    case FileType.ProfilePicture:
                        serverUrl = Cons.UserPicturePath + "/" + parentId.ToString();
                        break;
                }

                var serverPath = HttpContext.Current.Server.MapPath(serverUrl);

                if (type == FileType.ProfilePicture && Directory.Exists(serverPath))
                    Directory.Delete(serverPath, true);

                Directory.CreateDirectory(serverPath);

                var fullRealPath = Path.Combine(serverPath, fileName);
                var fullVirtualPath = serverUrl + "/" + fileName;

                file.SaveAs(fullRealPath);

                return fullVirtualPath;
            }
            catch (System.Exception ex)
            {
                return null;
            }
        }
    }
}