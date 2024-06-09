using MockarooLibrary.Repository.Interfaces;
using MockarooLibrary.Services.Interfaces;

namespace MockarooLibrary.Services
{
    /// <summary>
    /// Предоставляет функционал для работы с пользовательскими наборами данных
    /// </summary>
    public class UserDataSetsService : IUserDataSetsService
    {
        private IUserDataSetsRepository _dataSetRepository;

        public UserDataSetsService(IUserDataSetsRepository dataSetRepository)
        {
            _dataSetRepository = dataSetRepository;
        }

        /// <summary>
        /// Загружает пользовательский набор данных
        /// </summary>
        /// <param name="dataSetName">Название набора данных</param>
        /// <param name="dataSetValue">Значения набора данных</param>
        /// <returns></returns>
        public async Task UploadUserDataSet(string dataSetName, string dataSetValue)
        {
            await _dataSetRepository.SetUserDataSet(dataSetName, dataSetValue);
        }

        public async Task<List<string>> GetUserDataSet(string dataSetName)
        {
            var dataset = await _dataSetRepository.GetUserDataSet(dataSetName) ?? throw new Exception($"Набор данных {dataSetName} отсутствует или пуст");
            var datasetList = dataset.Split('\n').ToList();
            return datasetList;
        }

        /// <summary>
        /// Удаляет пользовательский набор данных
        /// </summary>
        /// <param name="datasetName">Название набора данных</param>
        /// <returns></returns>
        public async Task DeleteUserDataSet(string datasetName)
        {
            await _dataSetRepository.DeleteUserDataSet(datasetName);
        }

        /// <summary>
        /// Получает имена всех пользовательских наборов данных.
        /// </summary>
        /// <returns></returns>
        public async Task<List<string>> GetUserDataSetNames()
        {
            return await _dataSetRepository.GetUserDataSetNames();
        }
    }
}