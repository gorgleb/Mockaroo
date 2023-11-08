using Microsoft.Identity.Client;

namespace MockarooLibrary.Model.DataTypes
{
    public class Name : TableEntity
    {
        public Name(string NameInTable, string Value) : base(NameInTable, Value)
        {
        }

        public Name(string Value) : base(Value)
        {
        }

        public Name() : base()
        {
        }
    }
}