using HeyRed.Mime;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Hosting.Internal;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.IO;
using static Microsoft.AspNetCore.Hosting.Internal.HostingApplication;

namespace AurigainLoanERP.Shared.Common
{
    public class FileHelper
    {
        static IHostingEnvironment _env;
        public FileHelper(IHostingEnvironment environment)
        {
            _env = environment;
        }

        /// <summary>
        /// Save File from base64 string
        /// </summary>
        /// <param name="base64str">base64 string of File</param>
        /// <param name="filePath">save location file path</param>
        /// <param name="fileName">file name if required custom name</param>
        /// <returns></returns>
        public string Save(string base64str, string filePath, string fileName = null)

        {
            try
            {
                if (!string.IsNullOrEmpty(base64str) && !string.IsNullOrEmpty(filePath))
                {
                    string[] Fileinfo = base64str.Split(';');
                    byte[] byteArr = Convert.FromBase64String(Fileinfo[1].Substring(Fileinfo[1].IndexOf(',') + 1));

                    //  saveFile = filePath;
                    string path = GetPhysicalPath(filePath);
                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }
                    fileName = string.IsNullOrEmpty(fileName) ? Guid.NewGuid().ToString() + GetFileExtension(base64str) : fileName.Replace(" ","_");
                    File.WriteAllBytes(Path.Combine(path, fileName), byteArr);

                    return fileName;

                }
            }
            catch
            {
                throw;
            }
            return null;
        }

        public string Save(IFormFile file, string filePath, string fileName = null)
        {

            try
            {
                if (file != null && !string.IsNullOrEmpty(filePath))
                {
                    string path = GetPhysicalPath(filePath);

                    fileName = string.IsNullOrEmpty(fileName) ? Path.GetFileName(file.FileName) : fileName.Replace(" ","_");

                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }

                    using (FileStream stream = new FileStream(Path.Combine(path, fileName), FileMode.Create))
                    {
                        file.CopyTo(stream);

                    }
                    return fileName;
                }

            }
            catch (Exception)
            {
            }
            return null;

        }

        public string Get(string filePath)
        {
            string base64 = string.Empty;
            try
            {



                filePath = GetPhysicalPath(filePath);
                _env = new HostingEnvironment();

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

        public bool Delete(string filePath)
        {

            try
            {
                filePath = GetPhysicalPath(filePath);

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

        public string GetFileExtension(string base64String)
        {
            string ext = string.Empty;
            try
            {
                string mime = (base64String.Split(';')[0]).Split(':')[1];
                ext = MimeTypesMap.GetExtension(mime);
                ext = string.Concat(".", ext);
            }
            catch (Exception)
            {

                throw;
            }
            return ext;

        }

        private string GetPhysicalPath(string path)
        {

            try
            {
                return string.Concat(_env.ContentRootPath, path.Replace("~", "\\"));

            }
            catch (Exception)
            {

                throw;
            }


        }

        public string GetMimeType(string filePath)
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

        //public  string GetAbsoluteUrl( string path)
        //{
        //    if (string.IsNullOrWhiteSpace(_env.WebRootPath))
        //    {
        //        return string.Concat(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot"), path.Replace("~/", "/"));
        //    }

        //    return string.Concat(_env.WebRootPath, path.Replace("~/", "/"));

        //}



    }
}
