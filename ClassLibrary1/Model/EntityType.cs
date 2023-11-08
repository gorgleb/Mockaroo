namespace MockarooLibrary.Model
{
    public class EntityType : TableEntity
    {
        public EntityType(string NameInTable, string Value) : base(NameInTable, Value)
        {
        }

        public EntityType(string Value) : base(Value)
        {
        }
    }
}