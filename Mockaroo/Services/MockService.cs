using Mockaroo.Constants;
using Mockaroo.Model;
using System.Text;

namespace Mockaroo.Services
{
    public class MockService
    {
        public Table CurrentTable { get; private set; }

        public MockService(Table currentTable)
        {
            CurrentTable = currentTable;
        }

        public async Task<string> GenerateInsertStatement()
        {
            StringBuilder queryBuilder = new StringBuilder();
            var columnNames = await GenerateColumnNames();

            for (int i = 0; i < CurrentTable.RowsCount; i++)
            {
                queryBuilder.AppendLine(String.Format(SqlCommands.InsertStatement, CurrentTable.Name, columnNames, await GenerateValues()));
            }
            return queryBuilder.ToString();
        }

        private async Task<string> GenerateColumnNames()
        {
            string result;
            List<string> columnNames = new List<string>();
            foreach (var item in CurrentTable.TableEntities)
            {
                columnNames.Add(item.FirstOrDefault().NameInTable);
            }
            StringBuilder sb = new StringBuilder();
            foreach (var item in columnNames)
            {
                sb.Append(item + ",");
            }
            result = sb.ToString();
            result.TrimEnd(',');
            return result;
        }

        private async Task<string> GenerateValues()
        {
            string result;
            StringBuilder sb = new StringBuilder();
            foreach (var item in CurrentTable.TableEntities)
            {
                sb.Append(item.FirstOrDefault().Value + ',');
                item.Remove(item.FirstOrDefault());
            }
            result = sb.ToString();
            result.TrimEnd(',');
            return result;
        }
    }
}