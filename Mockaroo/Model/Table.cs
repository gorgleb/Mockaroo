namespace Mockaroo.Model
{
    public class Table
    {
        public string Name { get; private set; }
        public int RowsCount { get; private set; }
        public List<List<TableEntity>> TableEntities { get; private set; }

        public Table(string name, int rowsCount, List<List<TableEntity>> tableEntities)
        {
            Name = name;
            RowsCount = rowsCount;
            TableEntities = tableEntities;
        }
    }
}