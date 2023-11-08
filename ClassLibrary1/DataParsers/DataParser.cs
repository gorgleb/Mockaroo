using MockarooLibrary.Model;
using MockarooLibrary.Model.DataTypes;
using MockarooLibrary.Repository;

namespace MockarooLibrary.DataParsers
{
    public class DataParser
    {
        private TableEntityRepository repos;

        public DataParser(string connectionString)
        {
            repos = new TableEntityRepository(connectionString);
        }

        public void ParseAndInsertSurnamesFromTxt(string path)
        {
            List<string> allLinesText = File.ReadAllLines(path).ToList();
            List<Surname> neutralSurnames = new List<Surname>();
            foreach (string line in allLinesText)
            {
                if (!line.EndsWith("ЕВА") & !line.EndsWith("ОВА") & !line.EndsWith("ИНА")
                  & !line.EndsWith("СКАЯ") & !line.EndsWith("ЦКАЯ") & !line.EndsWith("ОВ") & !line.EndsWith("ЕВ") & !line.EndsWith("ИН") & !line.EndsWith("ЫН")
                  & !line.EndsWith("АЯ") & !line.EndsWith("ИЙ"))
                {
                    neutralSurnames.Add(new Surname(line));
                }
            }
            foreach (var surname in neutralSurnames)
            {
                repos.Create(surname);
            }
        }

        public void ParseAndInsertNamesFromTxt(string path)
        {
            List<string> allLinesText = File.ReadAllLines(path).ToList();
            foreach (var item in allLinesText)
            {
                repos.Create(new Name(item));
            }
        }

        public void ParseAndInsertAddressFromTxt(string path)
        {
            List<string> allLinesText = File.ReadAllLines(path).ToList();
            foreach (var item in allLinesText)
            {
                repos.Create(new Address(item));
            }
        }

        public void GenerateAndInsertRandomPhoneNumber(int neededAmount)
        {
            List<PhoneNumber> numbers = new List<PhoneNumber>();
            for (int i = 0; i < neededAmount; i++)
            {
                numbers.Add(new PhoneNumber(GenerateRandomPhoneNumber()));
            }
            foreach (var item in numbers)
            {
                repos.Create(item);
            }
        }

        private string GenerateRandomPhoneNumber()
        {
            Random random = new Random();
            string phoneNumber = string.Empty;
            phoneNumber += "+";
            for (int i = 0; i < 10; i++)
            {
                phoneNumber += random.Next(0, 9).ToString();
            }

            return phoneNumber;
        }
    }
}