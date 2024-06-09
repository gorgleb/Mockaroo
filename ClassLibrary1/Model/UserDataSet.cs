namespace MockarooLibrary.Model
{
    public class UserDataSet
    {
        public UserDataSet(string Name, List<string> Value)
        {
            this.Name = Name;
            this.Value = Value;
        }

        public string Name { get; set; }
        public List<string> Value { get; set; }
    }
}