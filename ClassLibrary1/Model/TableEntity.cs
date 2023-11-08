namespace MockarooLibrary.Model
{
    public class TableEntity
    {
        public int Id { get; private set; }
        public string NameInTable { get; set; }
        public string Value { get; set; }

        public TableEntity()
        {
        }

        public TableEntity(string NameInTable, string Value)
        {
            this.NameInTable = NameInTable;
            this.Value = Value;
        }

        public TableEntity(string Value)
        {
            this.Value = Value;
        }
    }
}