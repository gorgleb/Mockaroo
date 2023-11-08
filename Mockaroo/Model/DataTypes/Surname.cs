namespace Mockaroo.Model
{
    public class Surname : TableEntity
    {
        public Surname(string Value) : base(Value)
        {
        }

        public Surname(string NameInTable, string Value) : base(NameInTable, Value)
        {
        }
    }
}