using MockarooLibrary.Model;
using MockarooLibrary.Model.Enums;
using MockarooLibrary.Model.Helpers;

namespace MockarooLibrary.Processors.Interfaces
{
    public interface IMockProcessor
    {
        Task<DownloadData?> ProcessDataGeneration(Table? currentTable, Dictionary<string, string>? parameters, OutputDataTypes type);
    }
}