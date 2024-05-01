using Mockaroo.Constants;
using MockarooLibrary.Model;
using System.Text;

namespace MockarooLibrary.Processors
{
    /// <summary>
    /// Реализует логику генерации sql данных
    /// </summary>
    public class SqlProcessor : MockProcessor
    {
        public SqlProcessor() : base(null)
        {
        }

        /// <summary>
        /// Генерирует содержимое sql документа.
        /// </summary>
        /// <param name="curentTable"></param>
        /// <returns></returns>
        protected override async Task<string> GenerateData(Table currentTable)
        {
            StringBuilder queryBuilder = new StringBuilder();
            var columnNames = await GenerateColumnNames(currentTable);
            for (int i = 0; i < currentTable.RowsCount; i++)
            {
                queryBuilder.AppendLine(String.Format(SqlCommands.InsertStatement, currentTable.Name, columnNames, await GenerateValues(currentTable)));
            }
            return queryBuilder.ToString();
        }

        /// <summary>
        /// Генерирует имена колонок для sql выражения
        /// </summary>
        /// <param name="currentTable"></param>
        /// <returns></returns>
        private async Task<string> GenerateColumnNames(Table currentTable)
        {
            string result;
            List<string> columnNames = new List<string>();
            foreach (var item in currentTable.TableEntitySchema)
            {
                columnNames.Add(item.NameInTable);
            }
            StringBuilder sb = new StringBuilder();
            foreach (var item in columnNames)
            {
                if (item == columnNames.Last()) sb.Append("\"" + item + "\"");
                else sb.Append("\"" + item + "\"" + ",");
            }
            result = sb.ToString();
            result.TrimEnd(',');
            return result;
        }

        /// <summary>
        /// Генерирует конкретные значения для каждой строки sql выражения
        /// </summary>
        /// <param name="currentTable"></param>
        /// <returns></returns>
        private async Task<string> GenerateValues(Table currentTable)
        {
            string result;

            StringBuilder sb = new StringBuilder();
            foreach (var item in currentTable.TableEntities)
            {
                if (item.FirstOrDefault() == item.Last()) sb.Append("\'" + item.FirstOrDefault().Value + "\'");
                else sb.Append("\'" + item.FirstOrDefault().Value + "\'" + ',');
                item.Remove(item.FirstOrDefault());
            }
            result = sb.ToString();
            result = result.TrimEnd(',');
            return result;
        }
    }
}