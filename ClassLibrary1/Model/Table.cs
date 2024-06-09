using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace MockarooLibrary.Model
{
    public class Table
    {
        public string Name { get; private set; }
        public int RowsCount { get; private set; }
        public List<List<TableEntity>> TableEntities { get; private set; }
        public List<TableEntity> TableEntitySchema { get; private set; }

        public Table()
        {
        }

        public Table(string name, int rowsCount)
        {
            Name = name;
            RowsCount = rowsCount;
            TableEntities = new();
            TableEntitySchema = new();
        }

        public Table(string name, int rowsCount, List<List<TableEntity>> tableEntities)
        {
            Name = name;
            RowsCount = rowsCount;
            TableEntities = tableEntities;
            TableEntities = new();
            TableEntitySchema = new();
        }

        public void AddNewEntityToTableSchema(TableEntity entity)
        {
            TableEntitySchema.Add(entity);
        }

        public void AddNewEntityToTable(List<TableEntity> entity)
        {
            TableEntities.Add(entity);
        }
    }
}