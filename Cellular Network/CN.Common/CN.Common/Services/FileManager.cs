using CN.Common.Contracts.IServices;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CN.Common.Services
{
    public class FileManager : IFileManager
    {
        public void ClearFile(string fileName)
        {
            File.Delete(fileName);
        }


        public string[] ReadFromFile(string fileName)
        {
            string filePath = Path.Combine(Directory.GetCurrentDirectory(), fileName);
            return File.ReadAllLines(filePath);

        }

        public void WriteToFileArr(string fileName, string[] itemsData)
        {
            string filePath = Path.Combine(Directory.GetCurrentDirectory(), fileName);
            File.WriteAllLines(filePath, itemsData);
        }
        public void WriteToFile(string fileName, string itemData)
        {
            string filePath = Path.Combine(Directory.GetCurrentDirectory(), fileName);
            string[] arr = new string[1];
            arr[0] = itemData;
            File.WriteAllLines(filePath, arr);
        }

        public bool FileExists(string fileName)
        {
            //checks if the file exists
            return File.Exists(fileName);
        }
    }
}
