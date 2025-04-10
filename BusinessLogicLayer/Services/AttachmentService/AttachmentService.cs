using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Services.AttachmentService
{
    public class AttachmentService : IAttachmentService
    {
        List<string> AllowedExtensions = [".png", ".jpg", ".jpeg"];
        const int MaxSize = 2_097_152;

        public string? Upload(IFormFile file, string FolderName)
        {

            //1.Check Extension if allowed or not 
            var extension = Path.GetExtension(file.FileName);
            if (!AllowedExtensions.Contains(extension)) return null;

            //2.Check Size
            if (file.Length==0||file.Length>MaxSize) return null;

            //3.Get Located Folder Path to put the file inside this folder
            var folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Files", FolderName);

            //4.Make Attachment Name Unique-- GUID
            // using guid to make because each file in the folder must be unique
            var fileName =$"{Guid.NewGuid()}-{file.FileName}";


            //5.Get File Path
            // using it to open the stream on it to put the data that will be uploaded into this file
            var filePath=Path.Combine(folderPath,fileName);

            //6.Create File Stream To Copy File[Unmanaged]
            // it is unmanaged so we used "using" to close the connection after finish dealing with this obj
            using FileStream fs = new FileStream(filePath, FileMode.Create);

            //7.Use Stream To Copy File
            file.CopyTo(fs);


            //8.Return FileName To Store it In Database
            return fileName;
        }


        public bool Delete(string FilePath)
        {
            if(!File.Exists(FilePath)) return false;
            else
            {
                File.Delete(FilePath);
                return true;
            }
        }

    }
}
