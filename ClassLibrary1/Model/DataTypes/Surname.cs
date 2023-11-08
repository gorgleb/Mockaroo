namespace MockarooLibrary.Model.DataTypes
{
    public class Surname : TableEntity
    {
        public Surname(string NameInTable, string Value) : base(NameInTable, Value)
        {
        }

        public Surname(string Value) : base(Value)
        {
        }

        public Surname() : base()
        {
        }
    }
}