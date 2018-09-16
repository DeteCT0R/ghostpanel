using GhostPanel.Core.Managment.GameFiles;
using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.IO.Compression;

namespace GhostPanel.Core.Management.GameFiles
{
    class FileServerGameFiles : GameFilesBase, IGameFileManager
    {   
        private readonly string _installDir;
        private readonly string _archiveName;
        private readonly ILogger _logger;
        private readonly string _filePath = @"W:\Server Files";

        public FileServerGameFiles(string installDir, string archiveName, ILoggerFactory logger) : base(logger, installDir)
        {
            _installDir = installDir;
            _archiveName = archiveName;
            _logger = logger.CreateLogger<FileServerGameFiles>();
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

        public int GetInstallProgress()
        {
            throw new NotImplementedException();
        }

        public void UpdateGameServerFiles()
        {
            throw new NotImplementedException();
        }



    }
}
