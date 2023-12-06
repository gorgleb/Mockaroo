using MockarooLibrary.Model;

namespace MockarooLibrary.Services.Interfaces
{
    public interface IBaseGenerateService
    {
        Task<string> GenerateData(Table currentTable);

        Task<Stream> GenerateFileStream(Table currentTable);
    }
}