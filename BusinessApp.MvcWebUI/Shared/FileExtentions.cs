using Microsoft.AspNetCore.Hosting;
using MimeKit.Cryptography;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using BusinessApp.CarpetWash.MvcWebUI.Models;
using Microsoft.AspNetCore.Http;

namespace BusinessApp.CarpetWash.MvcWebUI.Shared
{
    public class FileExtentions
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        public string _rootImageDirectory;

        public FileExtentions() { }
        public FileExtentions(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
            _rootImageDirectory = Path.Combine(_webHostEnvironment.WebRootPath, "images/");

        }
        public string UploadedFile(IFormFile formFile, string fileType)
        {
            string uniqueFileName = null;

            if (formFile != null)
            {
                string uploadsFolder = Path.Combine(_rootImageDirectory + fileType);
                
                if (!Directory.Exists(uploadsFolder))
                {
                    DirectoryInfo di = Directory.CreateDirectory(uploadsFolder);
                }

                //uniqueFileName = Guid.NewGuid().ToString() + "_" + formFile.FileName;
                uniqueFileName = formFile.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    formFile.CopyTo(fileStream);
                }
            }
            return uniqueFileName;
        }

        public void DeleteFile(string rootFolder)
        {
            File.Delete(rootFolder);
        }
    }
}
