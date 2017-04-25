using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CheckList.Utils {

    public interface IFileOperations {
        void SaveText (string filename, string text);
        string LoadText (string filename);
        bool FileExists (string filename);
        string CreatePathToFile (string filename);
    }
}
