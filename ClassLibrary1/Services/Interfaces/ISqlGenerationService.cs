using MockarooLibrary.Model;

namespace MockarooLibrary.Services.Interfaces
{
    public interface ISqlGenerationService
    {
        Task<string> GenerateData(Table currentTable);

        Task<Stream> GenerateFileStream(Table currentTable);
    }
}