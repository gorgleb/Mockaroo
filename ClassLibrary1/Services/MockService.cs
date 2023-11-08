using Mockaroo.Constants;
using MockarooLibrary.Model;
using MockarooLibrary.Repository;
using System.Text;

namespace Mockaroo.Services
{
    public interface IMockService
    {
        Task<string> GenerateInsertStatement(Table currentTable);

        Task<Stream> GenerateFileStreamWithSqlStatements(Table currentTable);
    }

    public class MockService : IMockService
    {
        private Table CurrentTable;
        private ITableEntityRepository repos;

        public MockService(ITableEntityRepository repos)
        {
            this.repos = repos;
        }

        public async Task<Stream> GenerateFileStreamWithSqlStatements(Table currentTable)
        {
            this.CurrentTable = currentTable;
            var insertStatements = await GenerateInsertStatement(currentTable);
            var byteArray = Encoding.ASCII.GetBytes(insertStatements);
            var fileStream = new MemoryStream(byteArray);
            return fileStream;
        }

        public async Task<string> GenerateInsertStatement(Table currentTable)
        {
            this.CurrentTable = currentTable;
            StringBuilder queryBuilder = new StringBuilder();
            var columnNames = await GenerateColumnNames();
            FillTalbe();
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
            foreach (var item in CurrentTable.TableEntitySchema)
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

        private async Task<string> GenerateValues()
        {
            string result;

            StringBuilder sb = new StringBuilder();
            foreach (var item in CurrentTable.TableEntities)
            {
                if (item.FirstOrDefault() == item.Last()) sb.Append("\'" + item.FirstOrDefault().Value + "\'");
                else sb.Append("\'" + item.FirstOrDefault().Value + "\'" + ',');
                item.Remove(item.FirstOrDefault());
            }
            result = sb.ToString();
            result = result.TrimEnd(',');
            return result;
        }

        private void FillTalbe()
        {
            foreach (var item in this.CurrentTable.TableEntitySchema)
            {
                this.CurrentTable.AddNewEntityToTable(repos.GetEntities(this.CurrentTable.RowsCount, item.GetType()));
            }
        }
    }
}