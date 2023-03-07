using System;
using System.IO;
using UnityEngine;
using mis.Core;

namespace mis.Files
{
    public sealed class FilesService : IFilesService
    {
        public bool IsDirectoryExists(string path) =>
            Directory.Exists(path);

        public void CreateDirectorySave(string path)
        {
            if (!IsDirectoryExists(path))
            {
                Directory.CreateDirectory(path);
            }
        }

        public bool IsFileExists(string path) =>
            File.Exists(path);

        public string ReadTextFile(string path) =>
            File.ReadAllText(path);

        public void WriteTextFile(string path, string content) =>
            File.WriteAllText(path, content);
    }
}