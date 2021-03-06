﻿using Argos.Common;
using Argos.Common.Enums;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace Argos.Support
{
  
    public class FileManager
    {
        public static bool DeleteImage(string filePath)
        {
            try
            {
                var serverPath = HttpContext.Current.Server.MapPath(filePath);
                if (File.Exists(serverPath))
                {
                    File.Delete(serverPath);
                    return true;
                }
                return true;
            }
            catch 
            {
                return false;
            }
        }

        public static string SaveFile(HttpPostedFileBase file, string parentId, FileType type)
        {
            try
            {
                var fileName = Path.GetFileName(file.FileName); //getting only file name(ex-ganesh.jpg)  
                var extension = Path.GetExtension(file.FileName); //getting the extension(ex-.jpg)

                string serverUrl = string.Empty;

                switch (type)
                {
                    case FileType.ProfileImage:
                        serverUrl = URis.Images.Profile + "/" + parentId.ToString();
                        break;
                    case FileType.ProductImage:
                        serverUrl = URis.Images.Product + "/" + parentId.ToString();
                        break;
                    case FileType.ClientImage:
                        serverUrl = URis.Images.Customer + "/" + parentId.ToString();
                        break;
                    case FileType.SupplierImage:
                        serverUrl = URis.Images.Supplier + "/" + parentId.ToString();
                        break;
                    case FileType.EmployeeImage:
                        serverUrl = URis.Images.Employee + "/" + parentId.ToString();
                        break;
                }

                var serverPath = HttpContext.Current.Server.MapPath(serverUrl);

                //si el archivo es una foto de perfil y ya existe un directorio, lo borro y lo vuelvo a crear
                //ya que solo se permite una foto de perfil por usuario
                if (type == FileType.ProfileImage && Directory.Exists(serverPath))
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
                    case FileType.ProfileImage:
                        serverUrl = URis.Images.Profile + "/" + parentId.ToString();
                        break;
                    case FileType.ProductImage:
                        serverUrl = URis.Images.Product + "/" + parentId.ToString();
                        break;
                    case FileType.ClientImage:
                        serverUrl = URis.Images.Customer + "/" + parentId.ToString();
                        break;
                    case FileType.SupplierImage:
                        serverUrl = URis.Images.Supplier + "/" + parentId.ToString();
                        break;
                    case FileType.EmployeeImage:
                        serverUrl = URis.Images.Employee + "/" + parentId.ToString();
                        break;
                }

                var serverPath = HttpContext.Current.Server.MapPath(serverUrl);

                if (type == FileType.ProfileImage && Directory.Exists(serverPath))
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
    }
}