namespace MockarooLibrary.Services.Interfaces
{
    public interface IUserDataSetsService
    {
        Task<List<string>> GetUserDataSetNames();

        Task DeleteUserDataSet(string datasetName);

        Task UploadUserDataSet(string dataSetName, string dataSetValue);
    }
}