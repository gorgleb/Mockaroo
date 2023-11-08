namespace MockarooLibrary.Model.DataTypes
{
    public class PhoneNumber : TableEntity
    {
        public PhoneNumber(string Value) : base(Value)
        {
        }

        public PhoneNumber(string NameInTable, string Value) : base(NameInTable, Value)
        {
        }

        public PhoneNumber() : base()
        {
        }
    }
}