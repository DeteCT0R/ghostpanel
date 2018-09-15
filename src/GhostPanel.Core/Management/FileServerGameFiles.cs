using GhostPanel.Core.Managment;
using System;
using System.Diagnostics;
using System.IO;
using System.IO.Compression;

namespace GhostPanel.Core.Management
{
    class FileServerGameFiles : IGameFileManager
    {
        private readonly string _installDir;
        private readonly string _archiveName;
        private readonly string _filePath = @"W:\Server Files";

        public FileServerGameFiles(string installDir, string archiveName)
        {
            _installDir = installDir;
            _archiveName = archiveName;
        }

        public void DeleteGameServerFiles(string dir)
        {
            throw new NotImplementedException();
        }

        public void DownloadGameServerFiles()
        {
            var fullSourcePath = Path.Combine(_filePath, _archiveName);
            var fullDestPath = Path.Combine(_installDir, _archiveName);
            File.Copy(fullSourcePath, fullDestPath);
            if (File.Exists(fullDestPath))
            {
                ZipFile.ExtractToDirectory(fullDestPath, _installDir);
            }
        }

        public void UpdateGameServerFiles()
        {
            throw new NotImplementedException();
        }



    }
}
