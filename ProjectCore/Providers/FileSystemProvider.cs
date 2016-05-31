using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using ProjectCore.Interfaces;
using ProjectCore.Model;


namespace ProjectCore.Providers
{
    public sealed class FileSystemProvider : IFileSystemProvider
    {
        public IList<string> GetAllDrives()
        {
            try
            {
                return DriveInfo.GetDrives().Where(d => d.DriveType == DriveType.Fixed).Select(d => d.Name).ToList();
            }
            catch
            {
                return null;
            }
        }
        public IList<DirectoryInfoModel> GetAllSubDirectories(string path)
        {
            if (string.IsNullOrEmpty(path))
                return null;
            var dirsInfo = new List<DirectoryInfoModel>();
            if (!Directory.Exists(path))
                return null;
            try
            {
                var dirs = Directory.GetDirectories(path);
                var parent = Directory.GetParent(path);
                if (dirs.Length == 0)
                    return new List<DirectoryInfoModel>()
                    {
                        (new DirectoryInfoModel {DirectoryName = "", FullPath = "", Parent = parent?.ToString()})
                    };

                dirsInfo.AddRange(dirs.Select(d => new DirectoryInfo(d)).Select(directoryInfo => new DirectoryInfoModel
                {
                    DirectoryName = directoryInfo.Name,
                    FullPath = directoryInfo.FullName,
                    Parent = parent?.ToString()

                }));
                return dirsInfo;
            }
            catch
            {
                return null;
            }
        }
        public IList<FileInfoModel> GetAllFiles(string path)
        {
            if (string.IsNullOrEmpty(path))
                return null;
            var filesInfo = new List<FileInfoModel>();
            if (!Directory.Exists(path))
                return null;
            try
            {
                var files = Directory.GetFiles(path);
                if (files.Length == 0)
                    return null;

                filesInfo.AddRange(files.Select(f => new FileInfo(f)).Select(fileInfo => new FileInfoModel
                {
                    FileName = fileInfo.Name,
                    Size = Math.Round(fileInfo.Length / 1048576.0, 2)
                }));

                return filesInfo;
            }
            catch
            {
                return null;
            }
        }
        public FileCountModel GetFileCount(string path)
        {
            if (string.IsNullOrEmpty(path))
                return null;
            var result = FindFileCountInCurrentDirectory(path) ?? new FileCountModel();
            GetFileCountInSubDirs(path, result);
            return result;
        }
        private FileCountModel FindFileCountInCurrentDirectory(string path)
        {
            var fileInfo = GetAllFiles(path);
            if (fileInfo == null)
                return null;
            var counter = new FileCountModel();
            foreach (var file in fileInfo)
            {
                if (file.Size < 10)
                    counter.Less += 1;
                if (file.Size >= 10 && file.Size <= 50)
                    counter.Middle += 1;
                if (file.Size > 100)
                    counter.More += 1;
            }
            return counter;
        }
        private void GetFileCountInSubDirs(string path, FileCountModel currCount)
        {
            if (string.IsNullOrEmpty(path))
                return;

            var allDirs = GetAllSubDirectories(path);
            if (allDirs == null)
                return;

            foreach (var dir in allDirs)
            {
                var result = FindFileCountInCurrentDirectory(dir.FullPath);
                if (result == null)
                    continue;
                currCount.Less += result.Less;
                currCount.Middle += result.Middle;
                currCount.More += result.More;
            }
        }


    }
}
