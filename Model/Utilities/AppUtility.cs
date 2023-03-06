using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http.Headers;

namespace Model.Utilities
{
    public static class AppUtility
    {
        public static string NullToDash(string value)
        {
            if (string.IsNullOrEmpty(value) || string.IsNullOrWhiteSpace(value)) return "--";
            return value;
        }

        public static string DateToView(DateTime? date)
        {
            if (date == null) return "--";
            var stringDate = date?.ToString("dd-MMM-yyyy");
            return stringDate;
        }

        public static string DateTimeToView(DateTime? dateTime)
        {
            if (dateTime == null) return "--";
            var stringDate = dateTime?.ToString("dd-MMM-yyyy h:mm:ss tt");
            return stringDate;
        }

        public static FilePathModel GetFileUrl(IFormFile file)
        {
            var folderName = Path.Combine("Resources", "Test");
            var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);
            var filePath = new FilePathModel();
            if (file == null || file.Length <= 0) return filePath;

            var name = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName;
            if (name != null)
            {
                var fileName = name.Trim('"');
                var uniqueCode = GetUniqueCode();
                var newFileName = uniqueCode + "_" + fileName.Replace(" ", "_");
                filePath.DbPath = Path.Combine(folderName, newFileName);
                filePath.FullPath = Path.Combine(pathToSave, newFileName);
            }

            filePath.File = file;
            return filePath;
        }

        public static bool SaveFileToFolder(FilePathModel filePathModel)
        {
            if (filePathModel == null || string.IsNullOrEmpty(filePathModel.FullPath)) return false;
            using (var stream = new FileStream(filePathModel.FullPath, FileMode.Create))
            {
                filePathModel.File.CopyTo(stream);
            }
            return true;
        }

        public static bool DeleteFileFromFolder(string url)
        {
            FileInfo file = new FileInfo(url);
            if (!file.Exists) return false;
            file.Delete();
            return true;

        }

        public static string GetUniqueCode()
        {
            var dateNumber = DateTime.Now.ToString("ddMMyymmssff");
            var randomParam = DateTime.Now.ToString("ssff");
            var rand = new Random(Convert.ToInt32(randomParam));
            var randomNumber = rand.Next(000000000, 999999999);


            var joinUniqueNumber = randomNumber.ToString() + "_" + dateNumber.ToString();
            return joinUniqueNumber;
        }

        public static int ForEach<T>(this IEnumerable<T> list, Action<T, int> action) { if (action == null) throw new ArgumentNullException(nameof(action)); var i = 0; foreach (var elem in list) action(elem, i++); return i; }

        public static void ForEach<T>(this IEnumerable<T> list, Action<T> action) { if (action == null) throw new ArgumentNullException(nameof(action)); foreach (var elem in list) action(elem); }

    }
}
