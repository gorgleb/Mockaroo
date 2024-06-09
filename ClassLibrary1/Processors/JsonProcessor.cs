using MockarooLibrary.Model;
using System.Runtime.Serialization.Formatters;
using System.Security;
using System.Text;

namespace MockarooLibrary.Processors
{
    /// <summary>
    /// Реализует логику генерации json данных
    /// </summary>
    public class JsonProcessor : Generator
    {
        public JsonProcessor()
        {
        }

        /// <summary>
        /// Генерирует содержимое json документа.
        /// </summary>
        /// <param name="curentTable"></param>
        /// <returns></returns>
        public override async Task<string> GenerateData(Table currentTable, Dictionary<string, string>? parameters)
        {
            var sb = new StringBuilder();
            sb.Append("[");
            for (int i = 0; i < currentTable.RowsCount; i++)
            {
                sb.Append("{");
                for (int j = 0; j < currentTable.TableEntitySchema.Count; j++)
                {
                    var value = await GetValue(j, currentTable);
                    sb.Append($"\"{currentTable.TableEntitySchema[j].NameInTable}\":\"{value}\",");
                }

                if (i < currentTable.RowsCount - 1)
                    sb.Remove(sb.Length - 1, 1)
                    .Append("},")
                    .Append("\n");
                else
                    sb.Remove(sb.Length - 1, 1)
                    .Append("}")
                    .Append("]");
            }

            return sb.ToString();
        }

        /// <summary>
        /// Получает значение из тпблицы данных для конкретного поля
        /// </summary>
        /// <param name="index">Индекс того столбца, с которым мы работаем</param>
        /// <param name="currentTable">Таблица данных</param>
        /// <returns></returns>
        public async Task<string> GetValue(int index, Table currentTable)
        {
            var value = currentTable.TableEntities[index].FirstOrDefault();
            currentTable.TableEntities[index].Remove(value);
            return value.Value;
        }
    }
}