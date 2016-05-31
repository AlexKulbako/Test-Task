using System.Collections.Generic;
using ProjectCore.Model;

namespace ProjectCore.Interfaces
{
    public interface IFileSystemProvider
    {
        IList<string> GetAllDrives();
        IList<DirectoryInfoModel> GetAllSubDirectories(string path);
        IList<FileInfoModel> GetAllFiles(string path);
        FileCountModel GetFileCount(string path);
    }
}
