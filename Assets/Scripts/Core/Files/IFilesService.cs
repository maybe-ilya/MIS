namespace mis.Core
{
    public interface IFilesService : IService
    {
        bool IsDirectoryExists(string path);
        void CreateDirectorySave(string path);
        bool IsFileExists(string path);
        string ReadTextFile(string path);
        void WriteTextFile(string path, string content);
    }
}