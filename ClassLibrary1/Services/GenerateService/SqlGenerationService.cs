using Mockaroo.Constants;
using MockarooLibrary.Model;
using MockarooLibrary.Repository;
using MockarooLibrary.Repository.Interfaces;
using MockarooLibrary.Services.Interfaces;
using System.Text;

namespace MockarooLibrary.Services.GenerateService
{
    public class SqlGenerationService : IBaseGenerateService, ISqlGenerationService
    {
        private ITableEntityRepository tableEntityRepository;

        public SqlGenerationService(ITableEntityRepository tableEntityRepository)
        {
            this.tableEntityRepository = tableEntityRepository;
        }

        public async Task<string> GenerateData(Table currentTable)
        {
            StringBuilder queryBuilder = new StringBuilder();
            var columnNames = await GenerateColumnNames(currentTable);
            await FillTable(currentTable);
            for (int i = 0; i < currentTable.RowsCount; i++)
            {
                queryBuilder.AppendLine(String.Format(SqlCommands.InsertStatement, currentTable.Name, columnNames, await GenerateValues(currentTable)));
            }
            return queryBuilder.ToString();
        }

        public async Task<Stream> GenerateFileStream(Table currentTable)
        {
            var insertStatements = await GenerateData(currentTable);
            var byteArray = Encoding.ASCII.GetBytes(insertStatements);
            var fileStream = new MemoryStream(byteArray);
            return fileStream;
        }

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

        private async Task FillTable(Table currentTable)
        {
            foreach (var item in currentTable.TableEntitySchema)
            {
                currentTable.AddNewEntityToTable(tableEntityRepository.GetEntities(currentTable.RowsCount, item.GetType()));
            }
        }
    }
}