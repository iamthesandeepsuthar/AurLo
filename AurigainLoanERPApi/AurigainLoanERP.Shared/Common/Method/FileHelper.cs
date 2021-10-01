using HeyRed.Mime;
using Microsoft.AspNetCore.Hosting.Internal;
using Microsoft.AspNetCore.Http;
using System;
using System.IO;

namespace AurigainLoanERP.Shared.Common
{
    public static class FileHelper
    {

        private static HostingEnvironment _env = new HostingEnvironment();

        /// <summary>
        /// Save File from base64 string
        /// </summary>
        /// <param name="base64str">base64 string of File</param>
        /// <param name="filePath">save location file path</param>
        /// <param name="fileName">file name if required custom name</param>
        /// <returns></returns>
        public static string Save(string base64str, string filePath, string fileName=null)

        {
            string saveFile = null;
            try
            {
                if (!string.IsNullOrEmpty(base64str) && !string.IsNullOrEmpty(filePath))
                {
                    string[] Fileinfo = base64str.Split(';');
                    byte[] byteArr = Convert.FromBase64String(Fileinfo[1].Substring(Fileinfo[1].IndexOf(',') + 1));

                    saveFile = filePath;
                    filePath = filePath.GetPhysicalPath();
                    if (!Directory.Exists(filePath))
                    {
                        Directory.CreateDirectory(filePath);
                    }
                    fileName = string.IsNullOrEmpty(fileName) ? Guid.NewGuid().ToString() + filePath.GetFileExtension() : fileName;
                    File.WriteAllBytes(filePath + fileName, byteArr);
                    saveFile = string.Concat(saveFile, "/", fileName);

                    return fileName;
                }
            }
            catch
            {
                throw;
            }
            return saveFile;
        }

        public static string Save(IFormFile file, string filePath, string fileName = null)
        {

            try
            {
                if (file != null && !string.IsNullOrEmpty(filePath))
                {
                    string path = Path.Combine(_env.WebRootPath, filePath);

                    fileName = string.IsNullOrEmpty(fileName) ? Path.GetFileName(file.FileName) : fileName;

                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }

                    using (FileStream stream = new FileStream(Path.Combine(path, fileName), FileMode.Create))
                    {
                        file.CopyTo(stream);

                    }
                    return  fileName;
                }

            }
            catch (Exception)
            { 
            }
            return null;

        }

        public static string Get(string filePath)
        {
            string base64 = string.Empty;
            try
            {
                filePath = filePath.GetPhysicalPath();

                if (File.Exists(filePath))
                {
                    base64 = "Data:" + GetMimeType(filePath) + ";base64,";
                    ;
                    byte[] bytarr = File.ReadAllBytes(Path.Combine(_env.ContentRootPath, filePath));
                    base64 += Convert.ToBase64String(bytarr);
                }
            }
            catch
            {
                throw;
            }
            return base64;
        }

        public static bool Delete(string filePath)
        {

            try
            {
                filePath = filePath.GetPhysicalPath();

                if (File.Exists(filePath))
                {
                    File.Delete(filePath);
                    return true;
                }
            }
            catch
            {

            }

            return false;
        }

        public static string GetFileExtension(this string base64String)
        {
            string ext = string.Empty;
            try
            {
                string mime = (base64String.Split(';')[0]).Split(':')[1];
                ext = MimeTypesMap.GetExtension(mime);

            }
            catch (Exception)
            {

                throw;
            }
            return ext;

        }

        private static string GetPhysicalPath(this string path)
        {

            try
            {
                return Path.Combine(_env.ContentRootPath, path.Replace("~", ""));

            }
            catch (Exception)
            {

                throw;
            }


        }

        public static string GetMimeType(string filePath)
        {
            try
            {
                string[] Path = filePath.Split('\\');
                return MimeTypesMap.GetMimeType(Path[Path.Length - 1]);
            }
            catch (Exception)
            {

                throw;
            }
        }



    }
}
