using MockarooLibrary.Model;
using System.Text;

namespace MockarooLibrary.Processors
{
    /// <summary>
    /// Реализует логику генерации csv данных
    /// </summary>
    public class CsvProcessor : MockProcessor
    {
        public CsvProcessor() : base(null)
        {
        }

        /// <summary>
        /// Генерирует содержимое xml документа.
        /// </summary>
        /// <param name="curentTable"></param>
        /// <returns></returns>
        protected override async Task<string> GenerateData(Table currentTable)
        {
            var sb = new StringBuilder();
            string header = await GenerateHeader(currentTable);
            string body = await GenerateBody(currentTable);
            sb.AppendLine(header).AppendLine(body);
            return sb.ToString();
        }

        /// <summary>
        /// Создает заголовок xml документа, в котором выводятся названия столбцов.
        /// </summary>
        /// <param name="currentTable">Таблица данных</param>
        /// <returns></returns>
        private async Task<string> GenerateHeader(Table currentTable)
        {
            var sb = new StringBuilder();
            foreach (var item in currentTable.TableEntitySchema)
            {
                if (item == currentTable.TableEntitySchema.Last())
                    sb.Append($"{item.NameInTable.ToString()}");
                else
                    sb.Append($"{item.NameInTable.ToString()}");
            }
            return sb.ToString();
        }

        /// <summary>
        /// СОздает тело xml документа
        /// </summary>
        /// <param name="currentTable">Таблица данных</param>
        /// <returns></returns>
        private async Task<string> GenerateBody(Table currentTable)
        {
            var sb = new StringBuilder();

            for (int i = 0; i < currentTable.RowsCount; i++)
            {
                foreach (var item in currentTable.TableEntities)
                {
                    if (item.FirstOrDefault() != item.Last())
                        sb.Append($"{item.FirstOrDefault().Value},");
                    else
                        sb.Append(item.FirstOrDefault());
                    item.Remove(item.FirstOrDefault());
                }
                sb.Append('\n');
            }
            return sb.ToString();
        }
    }
}