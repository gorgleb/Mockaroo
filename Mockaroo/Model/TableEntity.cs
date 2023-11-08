namespace Mockaroo.Model
{
    public abstract class TableEntity
    {
        public string NameInTable { get; private set; }
        public string Value { get; private set; }

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