using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CN.Common.Contracts.IServices
{
    public interface IFileManager
    {
        string[] ReadFromFile(string fileName);
        void WriteToFileArr(string fileName, string[] itemsData);
        void ClearFile(string fileName);
        void WriteToFile(string fileName, string itemData);
        bool FileExists(string fileName);
    }
}
