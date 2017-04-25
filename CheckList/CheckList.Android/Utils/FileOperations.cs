using CheckList.Utils;
using com.kmd.Droid.Utils;
using System;
using System.IO;
using Xamarin.Forms;

[assembly: Dependency (typeof (FileOperations))]

namespace com.kmd.Droid.Utils {

    public class FileOperations : IFileOperations {
        public void SaveText (string filename, string text) {
            var filePath = CreatePathToFile (filename);
            System.IO.File.WriteAllText (filePath, text);
        }
        public string LoadText (string filename) {
            var filePath = CreatePathToFile (filename);
            if (FileExists (filePath)) {
                return System.IO.File.ReadAllText (filePath);
            }
            return null;
        }

        public bool FileExists (string filename) {
            return File.Exists (CreatePathToFile (filename));
        }

        public string CreatePathToFile (string filename) {
            var docsPath = Environment.GetFolderPath (Environment.SpecialFolder.Personal);
            return Path.Combine (docsPath, filename);
        }
    }
}