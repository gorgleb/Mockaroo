namespace MockarooLibrary.Model.DataTypes
{
    public class Address : TableEntity
    {
        public Address(string NameInTable, string Value) : base(NameInTable, Value)
        {
        }

        public Address(string Value) : base(Value)
        {
        }

        public Address() : base()
        {
        }
    }
}