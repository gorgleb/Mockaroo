using MockarooLibrary.Model;

namespace MockarooLibrary.Services.GenerateService
{
    public interface ISqlGenerationService
    {
        Task<string> GenerateData(Table currentTable);
        Task<Stream> GenerateFileStream(Table currentTable);
    }
}