using Microsoft.Identity.Client;
using MockarooLibrary.Repository.Interfaces;
using StackExchange.Redis;

namespace MockarooLibrary.Repository
{
    public class UserDataSetsRepository : IUserDataSetsRepository
    {
        private readonly ConnectionMultiplexer _redis;

        private readonly IDatabase _database;

        public UserDataSetsRepository()
        {
            _redis = ConnectionMultiplexer.Connect("localhost:6379");
            _database = _redis.GetDatabase();
        }

        /// <summary>
        /// Получает пользовательский набор данных из редиса
        /// </summary>
        /// <param name="datasetName">название пользовательского набора данных</param>
        /// <returns></returns>
        public async Task<string?> GetUserDataSet(string datasetName)
        {
            try
            {
                string? dataset = await _database.StringGetAsync(datasetName);
                return dataset;
            }
            catch (Exception ex)
            {
                return "";
            }
        }

        /// <summary>
        /// Записывает пользовательский набор данных в редис
        /// </summary>
        /// <param name="datasetName">название пользовательского набора данных</param>
        /// <returns></returns>
        public async Task SetUserDataSet(string datasetName, string datasetValue)
        {
            try
            {
                string namespacePrefix = "userdatasets:";
                _database.StringSet(namespacePrefix + datasetName, datasetValue);
            }
            catch (Exception ex)
            {
            }
        }

        /// <summary>
        /// Удаляет пользовательский набор данных из редиса
        /// </summary>
        /// <param name="datasetName">название пользовательского набора данных</param>
        /// <returns></returns>
        public async Task DeleteUserDataSet(string datasetName)
        {
            try
            {
                await _database.KeyDeleteAsync(datasetName);
            }
            catch (Exception ex)
            {
                // Обработка исключения
            }
        }

        /// <summary>
        /// Возвращает все имена пользовательских наборов данных
        /// </summary>
        /// <returns></returns>
        public async Task<List<string>> GetUserDataSetNames()
        {
            var server = _redis.GetServer("localhost:6379");
            var keys = server.Keys();
            List<string> keyList = keys.Select(key => (string)key).ToList();
            return keyList;
        }
    }
}