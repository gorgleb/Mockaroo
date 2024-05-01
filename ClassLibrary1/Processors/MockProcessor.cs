using MockarooLibrary.Model;
using MockarooLibrary.Model.Enums;
using MockarooLibrary.Model.Helpers;
using MockarooLibrary.Repository;
using System.Data;
using System.Text;

namespace MockarooLibrary.Processors
{
    /// <summary>
    /// Прдеоставляет функционал для генерации тестовых данных и возмоджность переопределения методов, для производных классов,
    /// которые нужны для обработки данных в конкретные форматы, по типу xml, json, sql и т.д.
    /// </summary>
    public abstract class MockProcessor
    {
        private ITableEntityRepository tableEntityRepository;

        public MockProcessor(ITableEntityRepository tableEntityRepository)
        {
            this.tableEntityRepository = tableEntityRepository;
        }

        public async Task<DownloadData?> ProcessDataGeneration(Table? currentTable, OutputDataTypes type)
        {
            return null;
            await IsValid(currentTable);
            switch (type)
            {
                case OutputDataTypes.CSV:
                    break;

                case OutputDataTypes.XML:
                    break;

                case OutputDataTypes.JSON:
                    break;

                case OutputDataTypes.SQL:
                    break;

                default:
                    break;
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

            if (currentTable.TableEntities == null || currentTable.TableEntities.Count == 0)
                throw new ArgumentException("В таблице данных отсутствуют столбцы");
            return true;
        }

        protected abstract Task<string> GenerateData(Table curentTable);

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
}