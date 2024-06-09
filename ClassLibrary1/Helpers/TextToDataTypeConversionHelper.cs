using MockarooLibrary.Exceptions;
using MockarooLibrary.Model;
using MockarooLibrary.Model.DataTypes;

namespace MockarooLibrary.Helpers
{
    public static class TextToDataTypeConversionHelper
    {
        public static TableEntity ConvertClassNameToDataClass(string nameInTable, string objectType)
        {
            try
            {
                var typeName = "MockarooLibrary.Model.DataTypes." + objectType;
                Type type = Type.GetType(typeName, true);
                TableEntity entity = ((TableEntity)Activator.CreateInstance(type));
                entity.NameInTable = nameInTable;
                return entity;
            }
            catch (Exception)
            {
                throw new TypeConversionException($"Переменную {nameInTable} типа {objectType} невозможно преобразовать");
            }
        }
    }
}