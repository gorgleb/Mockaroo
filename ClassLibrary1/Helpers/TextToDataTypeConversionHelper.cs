using MockarooLibrary.Model;
using MockarooLibrary.Model.DataTypes;

namespace MockarooLibrary.Helpers
{
    public static class TextToDataTypeConversionHelper
    {
        public static TableEntity ConvertClassNameToDataClass(string nameInTable, string objectType)
        {
            //switch (objectName)
            //{
            //    case "Address":
            //        return new Address(nameInTable, null);

            //    case "Name":
            //        return new Name(nameInTable, null);

            //    case "PhoneNumber":
            //        return new PhoneNumber(nameInTable, null);

            //    case "Surname":
            //        return new Surname(nameInTable, null);

            //    default:
            //        return null;
            //        break;
            //}
            var typeName = "MockarooLibrary.Model.DataTypes." + objectType;
            Type type = Type.GetType(typeName, true);
            TableEntity entity = ((TableEntity)Activator.CreateInstance(type));
            entity.NameInTable = nameInTable;
            return entity;
        }
    }
}