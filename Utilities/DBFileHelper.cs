using System;
using System.IO;

//[assembly: System.Runtime.CompilerServices.Dependency(typeof(DBFileHelper))]
namespace MileageManagerForms.Utilities
{
    public class DBFileHelper : IFileHelper
    {
        public string GetLocalFilePath(string filename)
        {
            string docFolder = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            string libFolder = Path.Combine(docFolder, "..", "Library", "Databases");

            if (!Directory.Exists(libFolder))
            {
                Directory.CreateDirectory(libFolder);
            }

            return Path.Combine(libFolder, filename);
        }
    }
}

public interface IFileHelper
{
    string GetLocalFilePath(string filename);
}