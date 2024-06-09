namespace MockarooLibrary.Repository.Interfaces
{
    public interface IUserDataSetsRepository
    {
        Task<string?> GetUserDataSet(string datasetName);

        Task SetUserDataSet(string datasetName, string datasetValue);

        Task DeleteUserDataSet(string datasetName);

        Task<List<string>> GetUserDataSetNames();
    }
}