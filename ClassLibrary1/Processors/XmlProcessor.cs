using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using MockarooLibrary.Model;
using System.Net.WebSockets;
using System.Text;
using System.Xml;

namespace MockarooLibrary.Processors
{
    /// <summary>
    /// Реализует логику генерации xml данных
    /// </summary>
    public class XmlProcessor : Generator
    {
        public XmlProcessor()
        {
        }

        /// <summary>
        /// Генерирует содержимое xml документа.
        /// </summary>
        /// <param name="curentTable"></param>
        /// <returns></returns>
        public override async Task<string> GenerateData(Table currentTable, Dictionary<string, string> parameters)
        {
            var rootName = parameters["rootName"];
            var recordName = parameters["recordName"];
            var doc = new XmlDocument();
            var root = doc.CreateElement(rootName);
            doc.AppendChild(root);

            for (int i = 0; i < currentTable.RowsCount; i++)
            {
                var record = doc.CreateElement(recordName);
                root.AppendChild(record);

                for (int j = 0; j < currentTable.TableEntitySchema.Count; j++)
                {
                    var child = doc.CreateElement(currentTable.TableEntitySchema[j].NameInTable);
                    var textNode = doc.CreateTextNode(await GetValue(j, currentTable));
                    child.AppendChild(textNode);
                    record.AppendChild(child);
                }
            }

            return doc.OuterXml;
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