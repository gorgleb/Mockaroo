using MockarooLibrary.Model;
using MockarooLibrary.Model.Enums;
using MockarooLibrary.Model.Helpers;
using MockarooLibrary.Processors.Interfaces;
using MockarooLibrary.Repository.Interfaces;
using System.Text;

namespace MockarooLibrary.Processors
{
    /// <summary>
    /// Прдеоставляет функционал для генерации тестовых данных и возмоджность переопределения методов, для производных классов,
    /// которые нужны для обработки данных в конкретные форматы, по типу xml, json, sql и т.д.
    /// </summary>
    public class MockProcessor : IMockProcessor
    {
        private ITableEntityRepository tableEntityRepository;

        public MockProcessor(ITableEntityRepository tableEntityRepository)
        {
            this.tableEntityRepository = tableEntityRepository;
        }

        public async Task<DownloadData?> ProcessDataGeneration(Table? currentTable, Dictionary<string, string>? parameters, OutputDataTypes type)
        {
            try
            {
                await IsValid(currentTable);
                await FillTable(currentTable);
                var data = new DownloadData();
                string result = "";
                switch (type)
                {
                    case OutputDataTypes.CSV:
                        var csvProcessor = new CsvProcessor();
                        result = await csvProcessor.GenerateData(currentTable, parameters);
                        data.FileExtensionName = "csv";
                        break;

                    case OutputDataTypes.XML:
                        var xmlProcessor = new XmlProcessor();
                        parameters["rootName"] = "root";
                        parameters["recordName"] = "record";
                        result = await xmlProcessor.GenerateData(currentTable, parameters);
                        data.FileExtensionName = "xml";
                        break;

                    case OutputDataTypes.JSON:
                        var jsonProcessor = new JsonProcessor();
                        result = await jsonProcessor.GenerateData(currentTable, parameters);
                        data.FileExtensionName = "json";
                        break;

                    case OutputDataTypes.SQL:
                        var sqlProcessor = new SqlProcessor();
                        result = await sqlProcessor.GenerateData(currentTable, parameters);
                        data.FileExtensionName = "sql";
                        break;

                    default:
                        break;
                }
                data.Content = result;
                data.FileStream = await GenerateFileStream(result);
                return data;
            }
            catch (Exception ex)
            {
                var e = ex.Message;
                return null;
            }
        }

        /// <summary>
        /// Проверяет таблицу на валидность
        /// </summary>
        /// <returns></returns>
        protected async Task<bool> IsValid(Table? currentTable)
        {
            if (currentTable == null)
            {
                throw new ArgumentNullException("Таблица данных равна null!");
            }

            if (currentTable.TableEntitySchema == null || currentTable.TableEntitySchema.Count == 0)
                throw new ArgumentException("В таблице данных отсутствуют столбцы");
            return true;
        }

        /// <summary>
        /// Преобразует строку с данными в поток данных
        /// </summary>
        /// <returns></returns>
        protected async Task<Stream> GenerateFileStream(string data)
        {
            var byteArray = Encoding.ASCII.GetBytes(data);
            var fileStream = new MemoryStream(byteArray);
            return fileStream;
        }

        private async Task FillTable(Table currentTable)
        {
            foreach (var item in currentTable.TableEntitySchema)
            {
                currentTable.AddNewEntityToTable(tableEntityRepository.GetEntities(currentTable.RowsCount, item.GetType()));
            }
        }
    }

    public abstract class Generator
    {
        public abstract Task<string> GenerateData(Table curentTable, Dictionary<string, string>? parameters);
    }
}